using System.Security.Claims;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Infrastructure.Security;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza el punto de acceso `A-01`, que canjea correo y contraseña por un acceso firmado
/// (`Api CU-01`).
/// </summary>
/// <remarks>
/// ES LA ÚNICA RUTA DE ESTA SUPERFICIE QUE DECLARA UNA FUENTE: `POST /auth/token`, intake
/// §17.5.P.3. Las demás son derivación de `Definicion-Superficie-HTTP.md` §3.
///
/// NO GUARDA EL ACCESO DE ESTE LADO: la superficie es sin estado. Quien lo conserva es el
/// circuito de la pieza pública, del lado de su servidor, y el navegador no lo ve nunca.
///
/// NO DEJA RASTRO DE LA CONTRASEÑA RECIBIDA: ni en la respuesta ni en el registro del servidor.
/// El registro anota el intento y su desenlace, nunca lo que la persona escribió.
/// </remarks>
public static class AuthenticationEndpoints
{
    /// <summary>Ruta del punto de canje. Declarada por la fuente.</summary>
    public const string TokenRoute = "/auth/token";

    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapPost(TokenRoute, async (
            CredentialExchangeRequest request,
            ResolveSignInUseCase resolveSignIn,
            PasswordDerivation credentials,
            AccessTokenIssuer accessTokens,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(AuthenticationEndpoints));

            // Paso 1 — la petición tiene que ser utilizable. La respuesta NOMBRA el campo
            // ausente, que es un dato de la petición y no de la cuenta.
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(request?.Email)) { missing.Add(nameof(CredentialExchangeRequest.Email)); }
            if (string.IsNullOrWhiteSpace(request?.Password)) { missing.Add(nameof(CredentialExchangeRequest.Password)); }

            if (missing.Count > 0)
            {
                log.LogInformation("Canje rechazado: la petición llegó incompleta.");
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.RequiredFieldMissing, now, [.. missing]);
            }

            // Pasos 2 a 4 — admisibilidad primero y comprobación de credencial después, que es el
            // orden de `Api CU-01` §4. La comprobación se le pasa a la capa de aplicación como
            // función: el valor derivado no sale de ahí y la comparación no entra.
            var resolution = await resolveSignIn
                .ExecuteAsync(
                    request!.Email,
                    storedValue => credentials.Verify(request.Password, storedValue),
                    cancellationToken)
                .ConfigureAwait(false);

            if (!resolution.Succeeded)
            {
                log.LogInformation("Canje rechazado con el motivo {Condition}.", resolution.ConditionCode);
                return ContractTranslation.Problem(resolution.ConditionCode, now);
            }

            var identity = resolution.Value!;

            // Paso 5 — la emisión. Sin clave de firma no se emite nada, y no se genera una al
            // vuelo: un acceso sin firma verificable es peor que ningún acceso.
            var accessToken = accessTokens.Issue(identity.Id, identity.Email, identity.Role.ToString(), now);
            if (accessToken is null)
            {
                log.LogError("No se pudo emitir el acceso firmado. Revisar la provisión de la clave de firma.");
                return ContractTranslation.Problem(conditionCode: null, now);
            }

            log.LogInformation("Canje resuelto para la cuenta {AccountId}.", identity.Id);

            // Paso 6 — respuesta de sesión con sus CUATRO campos y ninguno más.
            return Results.Ok(new SessionResponse(
                accessToken,
                identity.Id,
                identity.Email,
                identity.Role.ToString()));
        })
        .WithName("ExchangeCredentials")
        .AllowAnonymous();

        return endpoints;
    }

    /// <summary>
    /// La identidad de la cuenta que presenta el acceso, tomada de sus reclamos.
    /// </summary>
    /// <remarks>
    /// La guardia no agrega ningún dato a la petición: lo que el caso de uso recibe es lo que el
    /// acceso ya traía (`Api CU-02` §7).
    /// </remarks>
    public static Guid? AccountIdOf(ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        var subject = principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(subject, out var accountId) ? accountId : null;
    }
}
