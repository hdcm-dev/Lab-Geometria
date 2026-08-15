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
