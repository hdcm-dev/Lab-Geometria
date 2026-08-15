using GeometriaFactory.Web;
using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// LA CREDENCIAL DE SESIÓN VIVE ACÁ Y EN NINGÚN OTRO LADO (`Web ADR-03`). El alcance es el del
// CIRCUITO: bajo interactividad de servidor, un servicio con alcance de ámbito se resuelve una
// vez por circuito y vive en la memoria del servidor de la pieza pública, de modo que el
// navegador no tiene por dónde recibirlo. No es una cookie, no es almacenamiento del navegador
// y no se interpola en el marcado.
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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
