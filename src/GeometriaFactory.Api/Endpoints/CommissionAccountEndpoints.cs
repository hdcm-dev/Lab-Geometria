using System.Security.Claims;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza los puntos `A-06`, `A-07` y `A-08` (`Api CU-04`) y el punto `A-09` (`Api CU-05`).
/// </summary>
/// <remarks>
/// LOS CUATRO EXIGEN ACCESO FIRMADO Y LOS CUATRO ESTÁN BAJO LA GUARDIA de
/// <see cref="PendingPasswordChangeGuard"/>. El papel **no se exige en la tubería sino en la capa
/// de aplicación**, y es deliberado: la negativa por facultad tiene código propio en el contrato
/// desde `PRODUCT-INTAKE` 1.29 —`OPERATION_ADMIN_ONLY`, `403`— y resolverla con la política de
/// autorización del marco habría devuelto un `403` sin cuerpo, o sea sin código del contrato.
///
/// `A-08` ES LA ÚNICA OPERACIÓN DESTRUCTIVA DE TODA ESTA SUPERFICIE: elimina la cuenta **y todos
/// sus trabajos**, y no se deshace. Por eso su solicitud transporta un campo que ningún otro
/// punto tiene —el correo escrito como confirmación— y por eso este contrato declara qué pasa
/// cuando ese campo no coincide antes que ninguna otra cosa.
///
/// `A-09` ES EL ÚNICO PUNTO DE ESTA SUPERFICIE QUE DEVUELVE UN VALOR DE CREDENCIAL, y `A-07` lo
/// hace cuando la situación pretendida es habilitada. Las dos provisorias **son el mismo
/// mecanismo con dos disparadores** (`Api CU-04` §10): el administrador no las escribe, la
/// respuesta las devuelve **una sola vez** y **no entran al registro del servidor**. El registro
/// de este punto es la excepción declarada de la observabilidad del producto: lo que se excluye
/// es **el valor producido**, no el hecho de que hubo un reseteo.
///
/// LA SITUACIÓN DE LA CUENTA NO SE CONSULTA NI SE CAMBIA EN `A-09`: no declara ningún parámetro
/// de situación y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**,
/// porque esa causa no existe (RN-15, `Api CU-05` CA-09).
///
/// EL IDENTIFICADOR DE LA CUENTA LO MANDA LA RUTA Y NO EL CUERPO. Las tres solicitudes del
/// ensamblado lo declaran —y así tienen que declararlo, porque el contrato es el mismo para los
/// dos extremos—, pero acá la ruta ya lo trae y es la que gobierna: aceptar dos fuentes para el
/// mismo dato habría creado un lugar donde pueden no coincidir. **[decisión de la etapa `d`,
/// declarada]**
///
/// `A-08` LLEVA CUERPO AUNQUE SEA UN `DELETE`, y el cuerpo se declara explícitamente porque el
/// marco no lo infiere para ese verbo. No es una comodidad: **el correo escrito como confirmación
/// tiene que viajar**, y ponerlo en la ruta o en la cadena de consulta lo dejaría escrito en el
/// registro de acceso de cualquier intermediario, que es exactamente donde el producto no quiere
/// los datos de sus personas.
/// </remarks>
public static class CommissionAccountEndpoints
{
    /// <summary>Ruta de `A-06` y raíz de `A-07`, `A-08` y `A-09`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string AccountsRoute = "/cuentas";

    /// <summary>Ruta de `A-07`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string StatusRoute = "/cuentas/{id:guid}/situacion";

    /// <summary>Ruta de `A-08`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string DeletionRoute = "/cuentas/{id:guid}";

    /// <summary>Ruta de `A-09`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string PasswordResetRoute = "/cuentas/{id:guid}/reseteo-de-contrasena";

    public static IEndpointRouteBuilder MapCommissionAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // ---- A-06 · el listado de la comisión ------------------------------------------------
        endpoints.MapGet(AccountsRoute, async (
            HttpContext context,
            GovernCommissionAccountsUseCase governAccounts,
            ISystemClock clock,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;

            var result = await governAccounts
                .ListAsync(RoleOf(context.User), cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            // UN LISTADO VACÍO NO ES UN FALLO: se responde `200` con una colección vacía, y la
            // pieza pública distingue vacío de fallo por el tipo recibido y no por el conteo.
            return Results.Ok(result.Value!.Select(account => new AccountListItem(
                account.Id,
                account.Email,
                account.FirstName,
                account.LastName,
                account.Status.ToString(),
                account.CreatedAt,
                account.MustChangePassword)).ToArray());
        })
        .WithName("ListCommissionAccounts")
        .RequireAuthorization();

        // ---- A-07 · el cambio de situación ---------------------------------------------------
        endpoints.MapPost(StatusRoute, async (
            Guid id,
            AccountStatusChangeRequest request,
            HttpContext context,
            GovernCommissionAccountsUseCase governAccounts,
            ProvisionalPasswordFactory provisionalPasswords,
            PasswordDerivation credentials,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(CommissionAccountEndpoints));

            if (string.IsNullOrWhiteSpace(request?.IntendedStatus))
            {
                return ContractTranslation.Problem(
                    ConditionCode.RequiredFieldMissing, now, nameof(AccountStatusChangeRequest.IntendedStatus));
            }

            // Un valor fuera del conjunto cerrado que el contrato declara es una petición que no
            // es utilizable, y se responde `400` nombrando el campo
            // (`Definicion-Superficie-HTTP.md` §4).
            if (!Enum.TryParse<AccountStatus>(request.IntendedStatus, ignoreCase: false, out var intendedStatus)
                || !Enum.IsDefined(intendedStatus))
            {
                return ContractTranslation.Problem(
                    ConditionCode.RequiredFieldMissing, now, nameof(AccountStatusChangeRequest.IntendedStatus));
            }

            var result = await governAccounts
                .ChangeStatusAsync(
                    RoleOf(context.User),
                    id,
                    intendedStatus,
                    // LA PRODUCCIÓN NO RECIBE NINGÚN PARÁMETRO: no puede derivar el valor de
                    // ningún dato de la cuenta ni distinguir la habilitación del reseteo (RN-14).
                    provisionalPasswords.Produce,
                    credentials.Derive,
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                // EL REGISTRO ANOTA EL MOTIVO Y NUNCA UN VALOR DE CREDENCIAL.
                log.LogInformation("Cambio de situación rechazado con el motivo {Condition}.", result.ConditionCode);

                return result.ConditionCode == ApplicationConditionCode.AccountNotFound
                    ? ContractTranslation.AccountNotFound(now)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            var outcome = result.Value!;
            log.LogInformation(
                "Situación de la cuenta {AccountId} resuelta en {Status}.", id, outcome.Status);

            // LA PROVISORIA VIAJA EN EL CUERPO Y EN NINGÚN OTRO LADO. Cuando no hubo, el campo
            // queda sin valor, y esa ausencia es la señal de que no hay nada que comunicar.
            return Results.Ok(new AccountStatusChangeResponse(
                outcome.Status.ToString(),
                outcome.ProvisionalPassword,
                outcome.MustChangePassword));
        })
        .WithName("ChangeAccountStatus")
        .RequireAuthorization();

        // ---- A-08 · la baja física -----------------------------------------------------------
        endpoints.MapDelete(DeletionRoute, async (
            Guid id,
            [Microsoft.AspNetCore.Mvc.FromBody] AccountDeletionRequest request,
            HttpContext context,
            GovernCommissionAccountsUseCase governAccounts,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(CommissionAccountEndpoints));

            if (string.IsNullOrWhiteSpace(request?.ConfirmationEmail))
            {
                return ContractTranslation.Problem(
                    ConditionCode.RequiredFieldMissing, now, nameof(AccountDeletionRequest.ConfirmationEmail));
            }

            var result = await governAccounts
                .DeleteAsync(RoleOf(context.User), id, request.ConfirmationEmail, cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation("Baja rechazada con el motivo {Condition}.", result.ConditionCode);

                return result.ConditionCode == ApplicationConditionCode.AccountNotFound
                    ? ContractTranslation.AccountNotFound(now)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            log.LogInformation("Cuenta {AccountId} dada de baja con sus trabajos.", id);

            // `204`: se retiró algo y no hay cuerpo que devolver. NO HAY NINGUNA RESPUESTA DE
            // ESTA SUPERFICIE QUE SIGNIFIQUE «se borró una parte»: el adaptador escribe todo o
            // no escribe nada.
            return Results.NoContent();
        })
        .WithName("DeleteAccount")
        .RequireAuthorization();

        // ---- A-09 · el reseteo de la contraseña ----------------------------------------------
        endpoints.MapPost(PasswordResetRoute, async (
            Guid id,
            HttpContext context,
            ResetStudentPasswordUseCase resetPassword,
            ProvisionalPasswordFactory provisionalPasswords,
            PasswordDerivation credentials,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(CommissionAccountEndpoints));

            var result = await resetPassword
                .ExecuteAsync(
                    RoleOf(context.User),
                    id,
                    provisionalPasswords.Produce,
                    credentials.Derive,
                    cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation("Reseteo rechazado con el motivo {Condition}.", result.ConditionCode);

                return result.ConditionCode == ApplicationConditionCode.AccountNotFound
                    ? ContractTranslation.AccountNotFound(now)
                    : ContractTranslation.Problem(result.ConditionCode, now);
            }

            var outcome = result.Value!;

            // EL REGISTRO ANOTA QUE HUBO UN RESETEO Y NUNCA EL VALOR PRODUCIDO (RA-03, y la
            // exigencia propia de `Api CU-05` §9).
            log.LogInformation(
                "Contraseña reseteada para la cuenta {AccountId}, que sigue en {Status}.", id, outcome.Status);

            return Results.Ok(new PasswordResetResponse(
                outcome.Status.ToString(),
                outcome.ProvisionalPassword!,
                outcome.MustChangePassword));
        })
        .WithName("ResetAccountPassword")
        .RequireAuthorization();

        return endpoints;
    }

    /// <summary>
    /// El papel que el acceso firmado declara. Un acceso sin papel reconocible se trata como
    /// `Student`, que es el papel que **no puede** ejercer ninguna de estas cuatro operaciones:
    /// ante un reclamo que no se entiende, la negativa es la respuesta segura.
    /// </summary>
    private static Role RoleOf(ClaimsPrincipal principal)
    {
        // El reclamo es el que `AccessTokenIssuer` escribe, y se lo nombra desde ahí para que la
        // emisión y la lectura no puedan usar dos nombres distintos.
        var claim = principal.FindFirstValue(AccessTokenIssuer.RoleClaim);

        return Enum.TryParse<Role>(claim, ignoreCase: false, out var role) && Enum.IsDefined(role)
            ? role
            : Role.Student;
    }
}
