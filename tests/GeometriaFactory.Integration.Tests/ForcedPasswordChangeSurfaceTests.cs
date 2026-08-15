using GeometriaFactory.Web.Components.Pages;
using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// LA PANTALLA DEL CAMBIO FORZADO ES ALCANZABLE Y ESTÁ ARMADA, que es lo que la contradicción
/// entre `Api CU-01` §6 y `Definicion-Superficie-HTTP.md` §3 impedía hasta `PRODUCT-INTAKE` 1.34.
/// </summary>
/// <remarks>
/// SE DIBUJA, NO SE AFIRMA. Se arma el componente con el desvío anotado —que es lo que el ingreso
/// deja al recibir el código de cambio requerido— y se mira el marcado producido, que es lo que
/// viajaría al navegador. Y se comprueba el caso contrario, para que la prueba no pase por
/// ausencia: sin desvío anotado la pantalla no ofrece el formulario, ofrece volver al ingreso.
///
/// LO QUE NO SE DEMUESTRA ACÁ: el envío. Ése se ejercita contra la pieza de datos real en
/// <see cref="ForcedPasswordChangeTests"/>, de punta a punta.
/// </remarks>
public sealed class ForcedPasswordChangeSurfaceTests
{
    private const string StudentEmail = "alumna@frre.utn.edu.ar";

    private sealed class StaticNavigationManager : NavigationManager
    {
        public StaticNavigationManager(string uri) => Initialize("http://localhost/", uri);

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            // La prueba no navega: dibujar es todo lo que hace.
        }
    }

    private static async Task<string> RenderAsync(bool diverted)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<NavigationManager>(
            new StaticNavigationManager("http://localhost/credencial-propia/cambio-obligado"));
        services.AddSingleton(new DataServiceClient(
            new HttpClient { BaseAddress = new Uri("http://el-servicio-de-datos/") }));

        var session = new SessionState();
        if (diverted)
        {
            session.BeginPasswordChange(StudentEmail);
        }

        services.AddSingleton(session);

        using var provider = services.BuildServiceProvider();
        await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

        return await renderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await renderer.RenderComponentAsync<OwnCredentialForcedChange>();
            return output.ToHtmlString();
        });
    }

    [Fact]
    public async Task TheDivertedAccountGetsTheThreeFieldsAndTheWayBack()
    {
        var html = await RenderAsync(diverted: true);

        // Los TRES campos del wireframe: la provisoria, la nueva y su repetición.
        Assert.Contains("forced-provisional", html, StringComparison.Ordinal);
        Assert.Contains("forced-new", html, StringComparison.Ordinal);
        Assert.Contains("forced-new-repeat", html, StringComparison.Ordinal);

        // Los textos son los del wireframe y NO hablan en jerga.
        Assert.Contains("Elegí una contraseña nueva", html, StringComparison.Ordinal);
        Assert.Contains("Volver al ingreso", html, StringComparison.Ordinal);
        Assert.DoesNotContain("SUP-", html, StringComparison.Ordinal);
        Assert.DoesNotContain("CMP-", html, StringComparison.Ordinal);
        Assert.DoesNotContain("NAV-", html, StringComparison.Ordinal);

        // NO LLEVA «CANCELAR» y no dibuja barra lateral: no hay sesión ni estado previo.
        Assert.DoesNotContain("Cancelar", html, StringComparison.Ordinal);

        // Y el correo de la cuenta derivada no se escribe en el marcado: lo sabe el servidor.
        Assert.DoesNotContain(StudentEmail, html, StringComparison.Ordinal);
    }

    [Fact]
    public async Task WithoutTheDiversionTheSurfaceOffersTheWayInAndNoForm()
    {
        var html = await RenderAsync(diverted: false);

        Assert.DoesNotContain("forced-provisional", html, StringComparison.Ordinal);
        Assert.Contains("Ir a ingresar", html, StringComparison.Ordinal);

        // La pantalla existe igual y responde: es alcanzable, aunque no haya nada que cambiar.
        Assert.Contains("Elegí una contraseña nueva", html, StringComparison.Ordinal);
    }
}
