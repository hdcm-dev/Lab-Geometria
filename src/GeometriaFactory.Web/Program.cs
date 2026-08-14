using GeometriaFactory.Web;
using GeometriaFactory.Web.Integration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
