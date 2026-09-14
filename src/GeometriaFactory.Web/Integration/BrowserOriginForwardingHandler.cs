namespace GeometriaFactory.Web.Integration;

/// <summary>
/// Le dice al servicio de datos **desde qué dirección llegó el navegador** en las peticiones que el
/// front hace en nombre de una persona que todavía no tiene sesión.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`, R-02). El servicio de datos limita el
/// canje y las peticiones anónimas **por dirección de origen** (`Api ADR-00011`). Detrás del front,
/// esa dirección era la del front para toda la comisión: un solo cliente anónimo que inundara el
/// ingreso agotaba la cuota compartida y dejaba a la clase entera sin entrar, que es lo que
/// `RN-B1` prohíbe. `Api ADR-00011` dejó explícitamente esta decisión del lado del front.
///
/// QUÉ HACE. Si la petición del front corre dentro de una petición HTTP del navegador —el ingreso,
/// el registro y el cambio obligado se procesan así—, agrega `X-Forwarded-For` con la dirección
/// del navegador tal como el front la ve. Esa dirección ya pasó por `UseForwardedHeaders` del
/// propio front, de modo que detrás del túnel es la real y no la del túnel.
///
/// QUÉ NO HACE. En el circuito interactivo no hay petición HTTP del navegador y no agrega nada:
/// esas llamadas llevan sesión y el servicio de datos las particiona **por persona**. Tampoco
/// reenvía un `X-Forwarded-For` que haya traído el navegador: escribe la dirección que el front
/// resolvió, así que un cliente no puede elegir su partición escribiendo la cabecera.
///
/// LA CABECERA SÓLO CUENTA SI EL SERVICIO DE DATOS CONFÍA EN EL FRONT: su composición tiene que
/// declarar la red del front en `ForwardedHeaders__KnownNetworks`. Sin eso la ignora, que es el
/// comportamiento seguro.
/// </remarks>
public sealed class BrowserOriginForwardingHandler : DelegatingHandler
{
    /// <summary>El nombre de la cabecera que el servicio de datos lee con `UseForwardedHeaders`.</summary>
    public const string HeaderName = "X-Forwarded-For";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public BrowserOriginForwardingHandler(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var browser = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

        if (browser is not null)
        {
            request.Headers.Remove(HeaderName);
            request.Headers.TryAddWithoutValidation(HeaderName, browser.ToString());
        }

        return base.SendAsync(request, cancellationToken);
    }
}
