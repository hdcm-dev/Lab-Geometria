using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La base de todos los recorridos: contexto con la dirección del laboratorio, y el ingreso.
/// </summary>
public abstract class PruebaE2E : PageTest
{
    /// <summary>
    /// Cuánto se espera a que el circuito enganche antes de dar por muerto un control.
    /// </summary>
    /// <remarks>
    /// TREINTA SEGUNDOS, Y EL NUMERO SALE DE MEDIR. En el anfitrión real NO HAY WEBSOCKET —su
    /// negociación ofrece `ServerSentEvents` y `LongPolling` y nada más—, de modo que establecer el
    /// circuito cuesta unos 3 s en la práctica y bastante más con el proceso frío. Un margen corto
    /// convertiría cada arranque en frío en un rojo que no es del producto.
    /// </remarks>
    protected const int EsperaDelCircuito = 30_000;

    public override BrowserNewContextOptions ContextOptions() => new()
    {
        BaseURL = ElLaboratorio.UrlBase,
        Locale = "es-AR",
        TimezoneId = "America/Argentina/Buenos_Aires",

        // EL LABORATORIO SE SIRVE POR HTTPS CON CERTIFICADO VALIDO, así que esto NO se relaja. Si
        // alguna vez hace falta, es señal de que se está probando otra cosa.
        IgnoreHTTPSErrors = false,
    };

    /// <summary>Entra con una credencial y deja el navegador donde el producto lo mande.</summary>
    protected async Task IngresarAsync(string correo, string clave)
    {
        await Page.GotoAsync("/ingreso", new() { WaitUntil = WaitUntilState.Load });
        await Page.FillAsync("#signin-email", correo);
        await Page.FillAsync("#signin-password", clave);
        // SE APRIETA Y SE ESPERA EL ESTADO DE CARGA, y no se usa `RunAndWaitForNavigationAsync`:
        // está obsoleto en el binding y este proyecto trata las advertencias como errores. La forma
        // vigente es esperar la carga de la página que la navegación deja.
        await Page.ClickAsync("form:has(#signin-email) button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);
    }

    /// <summary>Entra como el administrador del laboratorio.</summary>
    protected Task IngresarComoAdministradorAsync() =>
        IngresarAsync(ElLaboratorio.CorreoDelAdministrador, ElLaboratorio.ClaveDelAdministrador);

    /// <summary>
    /// Espera a que un control esté REALMENTE disponible, no a que esté dibujado.
    /// </summary>
    /// <remarks>
    /// ES EL METODO QUE ESTA SUITE EXISTE PARA TENER. Entre que la página carga y que el circuito
    /// engancha hay unos segundos en que los controles se ven y no responden; el producto ahora los
    /// inhabilita y lo dice, y esto espera a que se habiliten.
    ///
    /// NO ES UN `Thread.Sleep` DISFRAZADO: espera una condición del producto —que el atributo
    /// `disabled` se vaya— y no un tiempo. Un tiempo fijo es lento cuando anda e intermitente
    /// cuando no, que es el antipatrón que la guía nombra.
    /// </remarks>
    protected async Task<ILocator> ControlListoAsync(string selector)
    {
        var control = Page.Locator($"{selector}:not([disabled])");
        await control.WaitForAsync(new() { Timeout = EsperaDelCircuito });
        return control;
    }
}
