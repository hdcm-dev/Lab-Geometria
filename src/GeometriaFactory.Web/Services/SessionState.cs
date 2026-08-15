using System.Globalization;
using System.Security.Claims;
using GeometriaFactory.Contracts.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// La sesión de quien entró, vista desde adentro del producto: identidad, papel y el testigo que
/// el cliente del servicio de datos adjunta (`Web ADR-03`).
/// </summary>
/// <remarks>
/// ES UNA FACHADA SOBRE DOS COSAS, Y NINGUNA DE LAS DOS ESTÁ EN EL NAVEGADOR ENTERA. La identidad
/// y el papel salen de la MARCA DE SESIÓN —<see cref="SessionCookieDefaults"/>—, que el navegador
/// sí conserva y que **no transporta el testigo**; el TESTIGO sale de
/// <see cref="SessionTokenStore"/>, que vive del lado del servidor con alcance de aplicación. La
/// marca es la llave y el almacén es lo que la llave abre: con una sola de las dos no hay sesión.
///
/// QUÉ CAMBIÓ RESPECTO DE LA ETAPA `c`, Y POR QUÉ. La etapa `c` guardaba todo en el estado del
/// circuito. Eso cumplía `ADR-03` §2 en su primera mitad —el testigo no llegaba al navegador— y
/// dejaba sin construir la segunda: la marca de sesión que la misma ADR nombra. La consecuencia
/// era medible y no teórica: recargar la página o abrir una pestaña nueva dejaba a la persona
/// afuera, y la página de ingreso, siendo interactiva, **no podía escribir ninguna marca**,
/// porque su respuesta viaja por el circuito y las cabeceras ya salieron. Acá el ingreso vuelve
/// a ser una petición de verdad, y por eso <see cref="OpenAsync"/> y <see cref="CloseAsync"/>
/// piden el contexto de esa petición: son las dos únicas operaciones que escriben o borran la
/// marca, y las dos ocurren mientras las cabeceras todavía se pueden escribir.
///
/// EL TESTIGO SIGUE SIN TENER POR DÓNDE SALIR. No se escribe en el marcado, no se pasa por
/// interoperabilidad de guion, no se guarda en el almacenamiento del navegador y **no está
/// adentro de la marca**, ni en claro ni cifrado. Que no aparezca en el navegador sigue siendo
/// criterio de aceptación verificable —métrica §8 de `ADR-03`, objetivo exactamente 0—, no una
/// aspiración.
/// </remarks>
public sealed class SessionState
{
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

    private readonly AuthenticationStateProvider _authentication;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SessionTokenStore _tokens;

    private ClaimsPrincipal? _loaded;

    public SessionState(
        AuthenticationStateProvider authentication,
        IHttpContextAccessor httpContextAccessor,
        SessionTokenStore tokens)
    {
        _authentication = authentication;
        _httpContextAccessor = httpContextAccessor;
        _tokens = tokens;
    }

    /// <summary>
    /// Trae la identidad de la marca de sesión para que las propiedades de abajo la puedan leer
    /// sin esperar.
    /// </summary>
    /// <remarks>
    /// POR QUÉ HAY QUE LLAMARLA, Y POR QUÉ NO SE PUEDE EVITAR. Bajo render estático la identidad
    /// está en el contexto de la petición y se lee sola; dentro de un circuito interactivo ese
    /// contexto **ya no existe**, y la única fuente es el proveedor de estado de autenticación,
    /// que responde de forma asincrónica. Una propiedad no puede esperar, así que la espera se
    /// hace una vez, acá, en el arranque del componente. Es idempotente.
    /// </remarks>
    public async Task LoadAsync()
    {
        if (_loaded is not null)
        {
            return;
        }

        var state = await _authentication.GetAuthenticationStateAsync().ConfigureAwait(false);
        _loaded = state.User;
    }

    /// <summary>
    /// El identificador OPACO de la sesión, que es lo que la marca del navegador lleva.
    /// </summary>
    /// <remarks>
    /// SE PUEDE LEER SIN RIESGO, Y ESA ES LA DIFERENCIA CON EL TESTIGO: no autoriza nada por sí
    /// solo. Fuera de este proceso es una cadena sin significado.
    /// </remarks>
    public string? SessionId => Principal.FindFirst(SessionClaims.SessionId)?.Value;

    /// <summary>
    /// Si hay una sesión de trabajo abierta.
    /// </summary>
    /// <remarks>
    /// SON LAS DOS MITADES A LA VEZ, Y ES DELIBERADO: marca presente **y** testigo guardado. Con
    /// la marca sola —el caso del reciclado del proceso— la sesión no está abierta, y decirlo acá
    /// es lo que evita que alguna superficie dibuje un panel que no tiene con qué llenar.
    /// </remarks>
    public bool IsOpen => SessionId is { } sessionId && _tokens.Contains(sessionId);

    /// <summary>Identidad de la cuenta que entró.</summary>
    public Guid AccountId =>
        Guid.TryParse(Principal.FindFirst(SessionClaims.AccountId)?.Value, out var accountId)
            ? accountId
            : Guid.Empty;

    /// <summary>Correo de la cuenta que entró. Es lo que la barra lateral muestra.</summary>
    public string Email => Principal.FindFirst(SessionClaims.Email)?.Value ?? string.Empty;

    /// <summary>Papel de la cuenta que entró. Es lo que decide qué destinos se dibujan.</summary>
    public string Role => Principal.FindFirst(SessionClaims.Role)?.Value ?? string.Empty;

    /// <summary>Si el papel de la sesión es el de administrador.</summary>
    public bool IsAdministrator => string.Equals(Role, "Administrator", StringComparison.Ordinal);

    /// <summary>
    /// Abre la sesión con lo que el canje devolvió. La credencial entra acá y no sale hacia
    /// ninguna superficie: la única forma de usarla es <see cref="UseAccessToken"/>, que sólo
    /// invoca el cliente del servicio de datos, del lado del servidor.
    /// </summary>
    /// <remarks>
    /// EL ORDEN IMPORTA Y ES ÉSTE: primero se guarda el testigo, después se escribe la marca. Al
    /// revés existiría un instante con marca válida y almacén vacío, que es exactamente el estado
    /// que el resto del producto interpreta como sesión no restablecible.
    ///
    /// EL TESTIGO NO ENTRA EN NINGUNA DECLARACIÓN. Lo que se firma en la marca es el
    /// identificador opaco, la identidad y el papel; el testigo se queda en el almacén. Es la
    /// diferencia entre esta implementación y la que `ADR-03` §4 descartó.
    /// </remarks>
    public async Task OpenAsync(HttpContext context, SessionResponse session)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(session);

        var sessionId = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
        _tokens.Keep(sessionId, session.AccessToken);

        var identity = new ClaimsIdentity(
            [
                new Claim(SessionClaims.SessionId, sessionId),
                new Claim(SessionClaims.AccountId, session.AccountId.ToString()),
                new Claim(SessionClaims.Email, session.Email),
                new Claim(SessionClaims.Role, session.Role),
            ],
            SessionCookieDefaults.Scheme);

        var principal = new ClaimsPrincipal(identity);
        await context.SignInAsync(SessionCookieDefaults.Scheme, principal).ConfigureAwait(false);

        _loaded = principal;

    }

    /// <summary>
    /// Cierra la sesión: descarta el testigo del almacén y borra la marca del navegador.
    /// </summary>
    /// <remarks>
    /// LAS DOS MITADES, Y EN ESTE ORDEN. Borrar sólo la marca dejaría el testigo vivo en el
    /// servidor sin nadie que lo reclame; descartar sólo el testigo dejaría al navegador
    /// creyendo que tiene sesión. Cerrar es un acto y no una decoración: es lo que hace que
    /// «Cerrar sesión» signifique algo del lado del servidor.
    /// </remarks>
    public async Task CloseAsync(HttpContext context)
    {

        ArgumentNullException.ThrowIfNull(context);

        if (SessionId is { } sessionId)
        {
            _tokens.Discard(sessionId);
        }

        await context.SignOutAsync(SessionCookieDefaults.Scheme).ConfigureAwait(false);
        _loaded = Anonymous;

    }

    /// <summary>
    /// Entrega el testigo a quien va a adjuntarlo en una petición hacia el servicio de datos.
    /// </summary>
    /// <remarks>
    /// NO ES UNA PROPIEDAD, Y ES DELIBERADO: una propiedad de lectura es interpolable en el
    /// marcado sin que nadie lo note, y el marcado es lo que se le manda al navegador. Un método
    /// con este nombre no se escribe por accidente dentro de una vista. Lo que devuelve sale del
    /// almacén del servidor, nunca de la marca.
    /// </remarks>
    public string? UseAccessToken() => SessionId is { } sessionId ? _tokens.Find(sessionId) : null;

    /// <summary>
    /// De dónde sale la identidad: del contexto de la petición mientras exista, y del proveedor
    /// de estado de autenticación cuando ya no —que es lo que pasa dentro de un circuito—.
    /// </summary>
    private ClaimsPrincipal Principal =>
        _loaded ?? _httpContextAccessor.HttpContext?.User ?? Anonymous;
}
