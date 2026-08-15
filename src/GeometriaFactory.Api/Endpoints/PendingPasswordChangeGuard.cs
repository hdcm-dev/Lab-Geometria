using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Errors;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// CU-02 paso 5 — La comprobación del cambio de contraseña pendiente, aplicada a **todo** punto
/// que exija acceso firmado salvo uno.
/// </summary>
/// <remarks>
/// SU DEFECTO CARACTERÍSTICO NO ES HACER MAL LO QUE HACE, SINO NO ALCANZAR A ALGUNO. Se rompe
/// agregando un punto de acceso nuevo y olvidándose, y cuando eso pasa **nada falla**. Por eso
/// está escrita como **intermediario y no como filtro por punto**: un filtro hay que acordarse de
/// ponerlo en cada `MapPost`, y el olvido no se nota; el intermediario alcanza a todo lo que
/// pase por la tubería, y lo que hay que declarar explícitamente es **la excepción**, que es una
/// sola y está acá abajo con nombre.
///
/// LA EXCEPCIÓN ES `A-05` Y ES UNA (CU-02 FA-02 y FA-04): cambiar la propia contraseña es lo
/// único que una cuenta marcada puede hacer, y es además lo único que levanta la marca. Cubre
/// los **dos orígenes** de la marca —la habilitación de RN-16 y el reseteo de F-26—, que desde
/// `PRODUCT-INTAKE` 1.13 recorren el mismo camino.
///
/// EL PASO CORTA ANTES QUE CUALQUIER OTRA COSA QUE EL PUNTO VAYA A HACER: una cuenta marcada **no
/// lee ni escribe nada**. Es INV-09, y el criterio con el que se verifica es el estado del
/// almacén después del rechazo, no la respuesta.
///
/// LA MARCA SE LEE DEL ALMACÉN Y NO DEL ACCESO PRESENTADO, y es lo que hace que **la marca corte
/// aunque el acceso siga siendo válido** (`Api CU-05` CA-06): un acceso emitido antes del reseteo
/// no se invalida solo, y si la comprobación viviera en sus reclamos el alumno reseteado seguiría
/// operando hasta que venciera.
///
/// NO AUTORIZA. Verificar que el acceso trae papel `Administrator` no es lo mismo que verificar
/// que quien pide puede operar sobre ese dato: la comprobación sobre el dato recuperado es de la
/// capa de aplicación, y el intake §17.5.P.5 lo dice en una línea — **el rol no alcanza**.
/// </remarks>
public sealed class PendingPasswordChangeGuard
{
    /// <summary>
    /// El nombre del único punto exento. Se compara contra el nombre del punto y no contra su
    /// ruta: la ruta es derivada y puede cambiar en el punto de control, y el nombre no.
    /// </summary>
    public const string ExemptEndpointName = "ChangeOwnPassword";

    private readonly RequestDelegate _next;

    public PendingPasswordChangeGuard(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAccountRepository accounts, ISystemClock clock)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(accounts);
        ArgumentNullException.ThrowIfNull(clock);

        // Los cuatro puntos que no exigen acceso no pasan por acá, y su ausencia no es un hueco:
        // el canje, el registro, la configuración del administrador y la salud se ejercen sin
        // acceso por construcción (CU-02 FA-03).
        if (context.User.Identity?.IsAuthenticated != true
            || context.GetEndpoint()?.Metadata.GetMetadata<IEndpointNameMetadata>()?.EndpointName
                == ExemptEndpointName)
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        var accountId = AuthenticationEndpoints.AccountIdOf(context.User);
        var account = accountId is null
            ? null
            : await accounts.FindByIdAsync(accountId.Value, context.RequestAborted).ConfigureAwait(false);

        if (account?.MustChangePassword != true)
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        // UN SOLO CÓDIGO PARA TODAS LAS OPERACIONES BLOQUEADAS, y sin nombrar la operación
        // pedida: lo que le queda por hacer al consumidor es siempre lo mismo, derivar al cambio
        // de contraseña (CU-02 CA-05, `Contracts CU-08` §10).
        var translation = ContractTranslation.Translate(Domain.Values.ConditionCode.PasswordChangePending);

        context.Response.StatusCode = translation.StatusCode;
        await context.Response
            .WriteAsJsonAsync(new ErrorResponse(translation.Code, translation.Message, [], clock.UtcNow))
            .ConfigureAwait(false);
    }
}
