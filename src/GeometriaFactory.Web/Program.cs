using GeometriaFactory.Web;
using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// INTERACTIVIDAD POR PÁGINA, Y NO GLOBAL. `App.razor` explica por qué: la superficie de ingreso
// tiene que poder escribir la marca de sesión, y eso sólo se puede hacer durante una petición
// HTTP de verdad. Dentro de un circuito las cabeceras ya salieron.
builder.Services.AddCascadingAuthenticationState();

// LA MARCA DE SESIÓN DEL NAVEGADOR (`Web ADR-03` §2; intake §17.6, `RT` §9.2). Los tres
// atributos no son elección de acá: los declara la fuente. `HttpOnly` es lo que hace que la
// marca «no sea legible por guion», que es la frase textual de la ADR.
builder.Services.AddAuthentication(SessionCookieDefaults.Scheme)
    .AddCookie(SessionCookieDefaults.Scheme, options =>
    {
        options.Cookie.Name = SessionCookieDefaults.CookieName;
        options.Cookie.HttpOnly = true;
        // `Secure` SIN EXCEPCIÓN en todo entorno que no sea el de desarrollo. En desarrollo
        // local la pieza se sirve por `http://`, y un navegador DESCARTA una cookie `Secure`
        // que llega en claro: sin esta salvedad, entrar no funcionaría en la máquina de quien
        // construye. La salvedad NO alcanza a producción, donde `somee` sirve por HTTPS y la
        // política vuelve a ser `Always`, que es lo que el intake §RT 9.2 exige.
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.SameAsRequest
            : CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        // La marca dura lo que dura la ventana del navegador: no se pide que sobreviva al cierre.
        options.Cookie.MaxAge = null;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;

        // Sin sesión no se ofrece nada del panel, y el desvío es a `Ingreso` (`ADR-03` §2,
        // guardián 2). Sigue ACOTANDO y no haciendo cumplir: quien verifica es el servicio de
        // datos, en cada solicitud.
        options.LoginPath = "/ingreso";
        options.LogoutPath = "/ingreso";
        options.AccessDeniedPath = "/ingreso";
    });

builder.Services.AddAuthorization();

// EL TESTIGO FIRMADO SE QUEDA DE ESTE LADO. Alcance de APLICACIÓN: es lo que hace que la sesión
// sobreviva a una recarga y a una pestaña nueva, sin que el testigo se acerque al navegador.
builder.Services.AddSingleton<SessionTokenStore>();

// LA SONDA DEL APROVISIONAMIENTO, TAMBIÉN CON ALCANCE DE APLICACIÓN. Es lo que hace que el
// guardián 1 no cueste un viaje de red por navegación: recuerda el «sí» para siempre —el estado
// es de ida y no vuelve— y no recuerda el «no» ni un segundo, porque la transición ocurre una
// sola vez en la vida de la instancia y es la que un caché con vencimiento rompe. La asimetría
// está fundamentada en el comentario de `ProvisioningStateProbe`.
builder.Services.AddSingleton<ProvisioningStateProbe>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionState>();

// La dirección del servicio de datos llega POR CONFIGURACIÓN y nunca embebida en el código
// (intake §17.6; `Web ADR-07`). El valor real vive como secreto del repositorio y no se versiona.
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException(
        "Falta 'ApiBaseUrl'. La dirección del servicio de datos llega por configuración.");

builder.Services.AddHttpClient<DataServiceClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl, UriKind.Absolute);
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// La dirección que no existe se reejecuta contra `/no-encontrado` CONSERVANDO el código 404:
// sin esto el cuerpo llega vacío, porque el `<NotFound>` del enrutador no gobierna el render
// estático del servidor. La pantalla es `NotFoundSurface`, propuesta declarada de la etapa `b`.
app.UseStatusCodePagesWithReExecute("/no-encontrado");

app.UseStaticFiles();
app.UseRouting();

// EL GUARDIÁN 1 DE `Web ADR-03` §2, Y VA PRIMERO DE LOS TRES A PROPÓSITO. Sin administrador
// configurado, cualquier ruta pedida desvía al aprovisionamiento inicial; con administrador, esa
// misma ruta deja de armar formulario y desvía de forma neutra. Va **antes** que el intermediario
// de sesión no restablecible y que el guardián 2 porque su primera mitad habla de **cualquier ruta
// pedida** y las de ellos hablan de rutas concretas bajo condiciones de sesión: mientras no hay
// administrador **no hay ninguna cuenta**, de modo que no puede haber sesión válida ni marca
// huérfana, ellos no tienen nada que decidir, y su desvío a `/ingreso` llevaría a una pantalla
// donde nadie puede entrar todavía. Al revés, una ruta del panel pedida en un laboratorio sin
// configurar terminaría en `/ingreso` en lugar de en el aprovisionamiento, que es lo contrario de
// lo que la ADR declara para el guardián 1.
//
// NO NECESITA AUTENTICAR Y POR ESO CORRE ANTES DE `UseAuthentication`: su decisión no mira quién
// pide, mira el estado del laboratorio. Y corre **después** de `UseStaticFiles`, que es lo que
// deja los recursos estáticos fuera de su alcance sin que haga falta nombrarlos uno por uno.
// Sigue ACOTANDO y no haciendo cumplir: quien impide el segundo administrador es el servicio de
// datos, que responde `409` a `A-03` aunque nadie pase por la pantalla.
app.UseMiddleware<ProvisioningGateMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// DESPUÉS de autenticar y ANTES de dibujar: es el único momento en que se sabe que la marca es
// válida y todavía se la puede borrar. Atiende el caso «marca presente, testigo ausente» que
// deja el reciclado del proceso (`Web ADR-03` §6.1).
app.UseMiddleware<UnrestorableSessionMiddleware>();

// EL GUARDIÁN 2 DE `Web ADR-03` §2, Y VA DESPUÉS DEL OTRO A PROPÓSITO. Sin sesión, las rutas del
// panel desvían a `/ingreso`. Corre después de autenticar —antes no se sabe si hay marca— y
// después del intermediario de sesión no restablecible, porque cuando los dos aplican tiene que
// hablar el otro: «marca presente, testigo ausente» desvía con el motivo declarado y corta, de
// modo que acá no se llega. Sigue ACOTANDO y no haciendo cumplir: quien verifica de verdad es el
// servicio de datos, en cada solicitud.
app.UseMiddleware<PanelSessionGateMiddleware>();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
