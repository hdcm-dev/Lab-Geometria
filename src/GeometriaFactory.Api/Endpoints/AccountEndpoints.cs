using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza los puntos de acceso `A-03` y `A-05` (`Api CU-03`).
/// </summary>
/// <remarks>
/// `A-03` configura la cuenta de administrador y **no exige acceso firmado**, porque en el primer
/// arranque no hay ninguna cuenta que pudiera emitirlo. No es un hueco: sólo procede mientras no
/// exista administrador, y esa condición la hace cumplir la capa de aplicación con el conjunto de
/// cuentas y el almacén con un índice único. Es además el único punto anónimo que escribe una
/// contraseña, y puede serlo porque no la escribe **sobre una cuenta existente** (RN-16, §7 de la
/// definición de la superficie).
///
/// `A-05` cambia la contraseña propia exigiendo la vigente, con acceso firmado de cualquiera de
/// los dos papeles. Es la ÚNICA excepción declarada de la guardia de cambio pendiente.
///
/// LA CONTRASEÑA EN CLARO MUERE ACÁ: se deriva y lo que sigue hacia adentro es el valor derivado.
/// </remarks>
public static class AccountEndpoints
{
    /// <summary>Ruta de `A-03`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string AdministratorSetupRoute = "/cuentas/administrador";

    /// <summary>Ruta de `A-05`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string OwnPasswordRoute = "/cuenta/contrasena";

    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapPost(AdministratorSetupRoute, async (
            AdministratorSetupRequest request,
            ConfigureAdministratorUseCase configureAdministrator,
            PasswordDerivation credentials,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(AccountEndpoints));

            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(request?.Email)) { missing.Add(nameof(AdministratorSetupRequest.Email)); }
            if (string.IsNullOrWhiteSpace(request?.FirstName)) { missing.Add(nameof(AdministratorSetupRequest.FirstName)); }
            if (string.IsNullOrWhiteSpace(request?.LastName)) { missing.Add(nameof(AdministratorSetupRequest.LastName)); }
            if (string.IsNullOrWhiteSpace(request?.Password)) { missing.Add(nameof(AdministratorSetupRequest.Password)); }

            if (missing.Count > 0)
            {
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.RequiredFieldMissing, now, [.. missing]);
            }

            var result = await configureAdministrator
                .ExecuteAsync(
                    request!.Email,
                    request.FirstName,
                    request.LastName,
                    credentials.Derive(request.Password),
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation("Configuración rechazada con el motivo {Condition}.", result.ConditionCode);
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            var identity = result.Value!;
            log.LogInformation("Cuenta de administrador constituida: {AccountId}.", identity.Id);

            // `201`: se constituyó algo que antes no existía. No devuelve credencial de sesión:
            // configurar no es entrar.
            return Results.Created(
                $"{AdministratorSetupRoute}/{identity.Id}",
                new AccountSetupResponse(identity.Id, identity.Email, identity.Role.ToString()));
        })
        .WithName("ConfigureAdministrator")
        .AllowAnonymous();

        endpoints.MapPost(OwnPasswordRoute, async (
            OwnPasswordChangeRequest request,
            HttpContext context,
            ChangeOwnPasswordUseCase changeOwnPassword,
            PasswordDerivation credentials,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(AccountEndpoints));

            var accountId = AuthenticationEndpoints.AccountIdOf(context.User);
            if (accountId is null)
            {
                // El acceso no trae identificador utilizable. Responde como la guardia y sin
                // código del contrato: el conjunto cerrado no declara ninguno para esta causa.
                return Results.Unauthorized();
            }

            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(request?.CurrentPassword)) { missing.Add(nameof(OwnPasswordChangeRequest.CurrentPassword)); }
            if (string.IsNullOrWhiteSpace(request?.NewPassword)) { missing.Add(nameof(OwnPasswordChangeRequest.NewPassword)); }

            if (missing.Count > 0)
            {
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.RequiredFieldMissing, now, [.. missing]);
            }

            var result = await changeOwnPassword
                .ExecuteAsync(
                    accountId.Value,
                    storedValue => credentials.Verify(request!.CurrentPassword, storedValue),
                    // La contraseña nueva se deriva RECIÉN cuando la vigente ya verificó.
                    () => credentials.Derive(request!.NewPassword) ?? string.Empty,
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation("Cambio de contraseña rechazado con el motivo {Condition}.", result.ConditionCode);
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            log.LogInformation("Contraseña reemplazada para la cuenta {AccountId}.", accountId.Value);

            // `200` sin cuerpo de sesión: el cambio no emite un acceso nuevo. El que la persona
            // ya tenía sigue sirviendo hasta que venza, y la renovación es por reingreso.
            return Results.Ok();
        })
        .WithName("ChangeOwnPassword")
        .RequireAuthorization();

        return endpoints;
    }
}
