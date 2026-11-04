using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Las pistas de la capa de IA desde el trabajo guardado, con un modelo que no contesta (mesa UX del
/// 2026-11-03, G1 a G6).
/// </summary>
/// <remarks>
/// SÓLO EN EL BANCO LOCAL: es el único donde la suite controla el servidor de modelos
/// (`ModeloQueNoContesta`) y el flag de pistas. Contra un despliegue, el flag sigue apagado en
/// `main` y el caso se declara no aplicable.
/// </remarks>
public sealed class PistasDeLaCapaDeIaTests : PruebaE2E
{
    /// <summary>Un cubo de lado 3 que declara área 36: la geometría da 54, una advertencia.</summary>
    private const string TextoConAdvertencia = """
        [ { "Tipo": "Cubo", "Caras": [
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 } ],
          "Area": 36.00, "Volumen": 27.00 } ]
        """;

    private ElLaboratorio.AlumnoSembrado? _alumno;

    [OneTimeSetUp]
    public async Task SembrarAsync()
    {
        if (ElLaboratorio.ModoBancoLocal)
        {
            _alumno = await ElLaboratorio.SembrarAlumnoAsync("pistas");
        }
    }

    [OneTimeTearDown]
    public Task LimpiarAsync() => ElLaboratorio.LimpiarAsync(_alumno);

    /// <summary>
    /// Desde el detalle del trabajo guardado: «Ver pistas» anuncia la espera, el modelo no contesta,
    /// y dentro de los 10 s de la Web llega la pista general del laboratorio, con el foco en ella, la
    /// advertencia a la vista y la línea de qué hacer. Ningún trabajo nuevo en el almacén.
    /// </summary>
    [Test]
    public async Task DesdeElDetalle_ConElModeloLento_LlegaLaPistaGeneralSinReenviar()
    {
        if (!ElLaboratorio.ModoBancoLocal)
        {
            Assert.Ignore("Sólo en el banco local: el flag de pistas está apagado en los despliegues.");
        }

        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno!, "E2E pistas", TextoConAdvertencia);
        await IngresarAsync(_alumno!.Correo, _alumno.Clave);
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        var verPistas = Page.GetByRole(AriaRole.Button, new() { Name = "Ver pistas" });
        await Expect(verPistas).ToHaveCountAsync(1, new() { Timeout = EsperaDelCircuito });

        // G5: la espera se anuncia en la región viva del formulario, que ya estaba en el marcado.
        var region = Page.Locator("#gf-hints [role=status]");
        await Expect(region).ToHaveCountAsync(1);

        // LO ANUNCIADO SE ANOTA EN LA PESTAÑA, porque la página se reemplaza al llegar la respuesta y
        // con ella se iría la evidencia. El almacenamiento de sesión sobrevive a esa navegación.
        await Page.EvaluateAsync("""
            () => {
                const region = document.querySelector('#gf-hints [role=status]');
                new MutationObserver(() => sessionStorage.setItem('gf-e2e-anuncio', region.textContent))
                    .observe(region, { childList: true, characterData: true, subtree: true });
            }
            """);

        // SE MIDE EL TRAMO DEL SERVIDOR, NO EL REPINTADO NI EL ARNÉS. La garantía (REQ-IA-07) es que
        // la Web tenga la plantilla antes de su propio corte de 10 s, y la Web contesta el POST del
        // formulario recién cuando el servicio de datos le devolvió las pistas. Por eso el plazo sale
        // de los tiempos de red del propio navegador (del envío del pedido al primer byte de la
        // respuesta). Un cronómetro alrededor del clic sumaba además el clic de Playwright, el aviso
        // al proceso de la prueba y el pintado de la página nueva: en la CI eso dio 10,3 s con el
        // servicio de datos cortando a tiempo.
        var respuesta = await Page.RunAndWaitForResponseAsync(
            () => verPistas.ClickAsync(),
            r => r.Request.Method == "POST" && r.Url.Contains($"/trabajos/{trabajo}"),
            new() { Timeout = 15_000 });
        var tiempos = respuesta.Request.Timing;
        var delServidor = TimeSpan.FromMilliseconds(tiempos.ResponseStart - tiempos.RequestStart);

        // G1: el servicio de datos corta antes que la Web; llega la pista general, no el error.
        var rotulo = Page.GetByText("Pista general del laboratorio", new() { Exact = true });
        await Expect(rotulo).ToBeVisibleAsync(new() { Timeout = 15_000 });
        TestContext.Progress.WriteLine(
            $"La Web respondió las pistas a los {delServidor.TotalSeconds:0.00} s (HTTP {respuesta.Status}).");

        var anunciado = await Page.EvaluateAsync<string?>("() => sessionStorage.getItem('gf-e2e-anuncio')");
        Assert.That(anunciado, Is.EqualTo("Buscando una pista…"), "La espera tiene que anunciarse en la región viva.");

        Assert.That(delServidor, Is.LessThan(TimeSpan.FromSeconds(10)),
            "La Web tiene que tener la pista antes de su corte (10 s).");
        // Y NO ANTES DE UNOS SEGUNDOS: si llegara al instante, el pedido no habría pasado por el modelo
        // lento y la prueba no estaría midiendo el corte del servicio de datos (8 s por omisión).
        Assert.That(delServidor, Is.GreaterThan(TimeSpan.FromSeconds(5)),
            "La pista llegó demasiado rápido: el pedido no esperó al modelo.");
        await Expect(Page.GetByText("No pudimos traer pistas")).ToHaveCountAsync(0);

        // G3: la advertencia, con sus dos valores, sigue a la vista y arriba de la pista.
        await Expect(Page.GetByText("La geometría da")).ToBeVisibleAsync();

        // G5: el foco quedó en la pista nueva.
        var enfocado = await Page.EvaluateAsync<string>("document.activeElement && document.activeElement.id");
        Assert.That(enfocado, Does.Match("^hints-\\d+-level-1$"));

        // G6: la línea fija de qué hacer.
        await Expect(Page.GetByText(new Regex("Usá la pista para revisar tu programa"))).ToBeVisibleAsync();

        // G2: pedir pistas no reenvió nada.
        Assert.That(await ElLaboratorio.UnicoTrabajoDeAsync(_alumno.Id), Is.EqualTo(trabajo));
        await CapturarAsync(Page, "pistas-desde-el-detalle");
    }
}
