using GeometriaFactory.Application;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Service;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza los cuatro puntos de acceso de `Api CU-03`: `A-02`, `A-03`, `A-05` y `A-17`.
/// </summary>
/// <remarks>
/// LOS CUATRO TIENEN EN COMÚN UN RASGO QUE NINGUNO DE LOS DEMÁS TIENE, y es el motivo por el que
/// están en un solo contrato: **se ejercen sin acceso firmado, o sin que el papel importe**.
///
/// `A-17` ENTRA ACÁ Y NO EN EL PUNTO DE SALUD, y el motivo está escrito en su propio comentario:
/// responde **si el laboratorio ya tiene administrador**, que es el dato sin el cual el guardián 1
/// de `Web ADR-03` §2 no se podía construir. Es hermano de `A-03` —misma ventana de alta, mirada
/// desde afuera— y por eso comparte contrato con él.
///
/// `A-02` REGISTRA UNA CUENTA DE ALUMNO **SIN CAMPO DE CONTRASEÑA**, y sigue siendo anónimo:
/// **RN-16** suprimió la escritura anónima **de credencial**, no toda escritura anónima. El
/// registro de cuenta es anónimo por diseño, porque es la puerta por la que el alumno entra al
/// laboratorio (`PRODUCT-INTAKE` 1.15 §4.1). La cuenta nace `Pending`, sin credencial, y
/// **todavía no obtiene acceso** (RN-06): la credencial la recibe al ser habilitada, por `A-07`.
///
/// EL PUNTO `A-04` QUEDÓ RETIRADO Y **NO SE RECICLA**. Exponía el establecimiento de la
/// contraseña en el primer ingreso y era **la única escritura de contraseña de esta superficie
/// que ocurría sin credencial**. Con RN-16 la persona llega a elegir la suya **ya autenticada**,
/// por `A-05`. De los cuatro puntos que no exigen acceso firmado, **ninguno fija una contraseña
/// sobre una cuenta existente**, y ésa es la ausencia que §7 de la definición de la superficie
/// declara como sostenida.
///
/// `A-03` configura la cuenta de administrador y **no exige acceso firmado**, porque en el primer
/// arranque no hay ninguna cuenta que pudiera emitirlo. No es un hueco: sólo procede mientras no
/// exista administrador, y esa condición la hace cumplir la capa de aplicación con el conjunto de
/// cuentas y el almacén con un índice único. Es además el único punto anónimo que escribe una
/// contraseña, y puede serlo porque no la escribe **sobre una cuenta existente** (RN-16, §7 de la
/// definición de la superficie).
///
/// `A-05` cambia la contraseña propia exigiendo la vigente, y admite DOS FORMAS DE AUTENTICARSE
/// (`PRODUCT-INTAKE` 1.34): con **acceso firmado** de cualquiera de los dos papeles —el cambio
/// corriente, que cualquier cuenta puede hacer cuando quiera—, y con **la contraseña actual**
/// —el cambio forzado, donde la provisoria que el administrador comunicó es la que autentica—.
/// Es la ÚNICA excepción declarada de la guardia de cambio pendiente, y la segunda forma es lo
/// que la vuelve alcanzable: RN-13 le niega la sesión de trabajo a la cuenta marcada, de modo
/// que exigirle acceso firmado la dejaba sin ninguna manera de llegar hasta acá.
///
/// LA SEGUNDA FORMA NO AFLOJA RN-13 Y NO ES UNA ESCRITURA SIN CREDENCIAL: exige la contraseña
/// vigente igual que la primera, sólo procede sobre una cuenta CON LA MARCA PUESTA y **no emite
/// ningún acceso**. Después del cambio la persona vuelve a canjear, y recién ahí obtiene sesión.
///
/// LA CONTRASEÑA EN CLARO MUERE ACÁ: se deriva y lo que sigue hacia adentro es el valor derivado.
/// </remarks>
public static class AccountEndpoints
{
    /// <summary>Ruta de `A-02`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string RegistrationRoute = "/cuentas";

    /// <summary>Ruta de `A-03`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string AdministratorSetupRoute = "/cuentas/administrador";

    /// <summary>Ruta de `A-05`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string OwnPasswordRoute = "/cuenta/contrasena";

    /// <summary>Ruta de `A-17`. [derivado] `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string ProvisioningStateRoute = "/aprovisionamiento";

    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapPost(RegistrationRoute, async (
            AccountRegistrationRequest request,
            RegisterAccountUseCase registerAccount,
            ISystemClock clock,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var now = clock.UtcNow;
            var log = loggerFactory.CreateLogger(typeof(AccountEndpoints));

            // La respuesta NOMBRA el campo ausente, que es un dato de la petición y no de la
            // cuenta. NO HAY CAMPO DE CONTRASEÑA QUE PUDIERA FALTAR: el tipo no lo declara.
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(request?.Email)) { missing.Add(nameof(AccountRegistrationRequest.Email)); }
            if (string.IsNullOrWhiteSpace(request?.FirstName)) { missing.Add(nameof(AccountRegistrationRequest.FirstName)); }
            if (string.IsNullOrWhiteSpace(request?.LastName)) { missing.Add(nameof(AccountRegistrationRequest.LastName)); }

            if (missing.Count > 0)
            {
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.RequiredFieldMissing, now, [.. missing]);
            }

            var result = await registerAccount
                .ExecuteAsync(request!.Email, request.FirstName, request.LastName, cancellationToken)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                log.LogInformation("Registro rechazado con el motivo {Condition}.", result.ConditionCode);
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            var account = result.Value!;
            log.LogInformation("Cuenta de alumno registrada: {AccountId}.", account.Id);

            // `201`: se constituyó algo que antes no existía. La respuesta declara la situación
            // inicial, que es lo que sostiene el aviso explícito de RN-06, y NO devuelve
            // credencial de sesión: registrarse no es entrar.
            return Results.Created(
                $"{RegistrationRoute}/{account.Id}",
                new AccountRegistrationResponse(account.Id, account.Email, account.Status.ToString()));
        })
        .WithName("RegisterAccount")
        .AllowAnonymous();

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

        // `A-17` — SI EL LABORATORIO YA TIENE ADMINISTRADOR. Es el punto que faltaba, y su ausencia
        // es la razón por la que el **guardián 1** de `Web ADR-03` §2 nunca se construyó: la pieza
        // pública no tenía con qué preguntarlo. `A-03` configura —es escritura—, `A-16` responde
        // por la salud del servicio y `A-06` exige ser administrador; ninguno le sirve a un
        // visitante anónimo, y el guardián corre justamente antes de que nadie se haya identificado.
        //
        // ANÓNIMO Y DE SÓLO LECTURA, Y RESPONDE UN SOLO DATO. No hay correo, no hay nombre, no hay
        // fecha y no hay cantidad de cuentas: lo que se necesita saber es si la ventana de alta
        // está abierta. Lo que este punto revela **ya es observable en cuanto el guardián empieza a
        // desviar** —el desvío mismo lo delata—, y la neutralidad que `ADR-03` §6.4 exige es sobre
        // **no explicar por qué** en la pantalla, no sobre ocultar el hecho.
        //
        // POR QUÉ NO SE LE METIÓ EL DATO A `A-16`. La salud la consume el `healthcheck` de
        // `deploy/compose.yaml`: mezclarle un hecho del producto acopla dos cosas que cambian por
        // motivos distintos. Son dos preguntas y son dos puntos.
        //
        // `200` Y NADA MÁS EN SU TABLA. No hay `503`: este punto no invoca la preparación del
        // almacén como `A-16` —lee, y si el almacén no atiende la respuesta la produce el manejo
        // genérico, sin traza ni dirección interna (RA-03)— y no hay ningún estado del laboratorio
        // que lo haga inválido: «todavía no» es una respuesta legítima y no un fallo.
        endpoints.MapGet(ProvisioningStateRoute, async (
            ConfigureAdministratorUseCase configureAdministrator,
            CancellationToken cancellationToken) =>
        {
            var configured = await configureAdministrator
                .IsConfiguredAsync(cancellationToken)
                .ConfigureAwait(false);

            return Results.Ok(new LaboratoryProvisioning(configured));
        })
        .WithName("LaboratoryProvisioning")
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

            // CUÁL DE LAS DOS FORMAS ES: si la petición trae un acceso firmado utilizable, la
            // cuenta que cambia es la que ese acceso nombra y el correo del cuerpo NO SE MIRA.
            // Sin acceso, la cuenta la nombra el correo y quien autentica es la contraseña
            // vigente, que es el cambio forzado.
            var accountId = AuthenticationEndpoints.AccountIdOf(context.User);

            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(request?.CurrentPassword)) { missing.Add(nameof(OwnPasswordChangeRequest.CurrentPassword)); }
            if (string.IsNullOrWhiteSpace(request?.NewPassword)) { missing.Add(nameof(OwnPasswordChangeRequest.NewPassword)); }

            if (missing.Count > 0)
            {
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.RequiredFieldMissing, now, [.. missing]);
            }

            ApplicationResult result;

            if (accountId is not null)
            {
                result = await changeOwnPassword
                    .ExecuteAsync(
                        accountId.Value,
                        storedValue => credentials.Verify(request!.CurrentPassword, storedValue),
                        // La contraseña nueva se deriva RECIÉN cuando la vigente ya verificó.
                        () => credentials.Derive(request!.NewPassword) ?? string.Empty,
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (string.IsNullOrWhiteSpace(request!.Email))
            {
                // Ni acceso firmado ni correo: NADA identifica a la cuenta que querría cambiar.
                // No es un campo ausente —el correo sólo hace falta en la forma sin sesión— sino
                // la respuesta neutra de siempre, la misma que recibe una credencial que no
                // corresponde. Responde como respondía la guardia: `401`.
                log.LogInformation("Cambio de contraseña rechazado: la petición no identifica ninguna cuenta.");
                return ContractTranslation.Problem(
                    Domain.Values.ConditionCode.CurrentCredentialNotVerified, now);
            }
            else
            {
                result = await changeOwnPassword
                    .ExecuteWithCurrentCredentialAsync(
                        request.Email,
                        storedValue => credentials.Verify(request.CurrentPassword, storedValue),
                        () => credentials.Derive(request.NewPassword) ?? string.Empty,
                        cancellationToken)
                    .ConfigureAwait(false);
            }

            if (!result.Succeeded)
            {
                log.LogInformation("Cambio de contraseña rechazado con el motivo {Condition}.", result.ConditionCode);
                return ContractTranslation.Problem(result.ConditionCode, now);
            }

            log.LogInformation("Contraseña reemplazada por la forma {Form}.", accountId is not null ? "de sesión" : "de credencial");

            // `200` sin cuerpo de sesión: el cambio no emite un acceso nuevo, en NINGUNA de las
            // dos formas. Con sesión, el que la persona ya tenía sigue sirviendo hasta que venza;
            // sin sesión, la persona vuelve a canjear, y es ahí donde obtiene la suya (RN-13).
            return Results.Ok();
        })
        .WithName("ChangeOwnPassword")
        // NO lleva `RequireAuthorization`, y es la decisión del intake 1.34: exigir acceso firmado
        // acá dejaba la pantalla del cambio forzado inalcanzable. La autenticación de la segunda
        // forma la hace la contraseña vigente, dentro del caso de uso.
        .AllowAnonymous();

        return endpoints;
    }
}
