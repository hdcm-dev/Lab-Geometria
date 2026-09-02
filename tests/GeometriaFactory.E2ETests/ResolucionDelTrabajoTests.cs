using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Resolver una entrega: el recorrido que el docente hace todos los días.
/// </summary>
/// <remarks>
/// EL DESENLACE SE CONFIRMA CONTRA EL SERVICIO DE DATOS, no contra la pantalla. Una prueba que
/// termina mirando la pantalla que ella misma provocó puede dar verde con el dato intacto: pasó el
/// 2026-09-01, con el botón dibujado y el trabajo en `Pendiente`.
///
/// TODO LO QUE SE RESUELVE ACA ES DE UN ALUMNO SEMBRADO POR ESTA CLASE. Aprobar el trabajo de un
/// alumno de verdad para «ver si el botón anda» sería tomar una decisión pedagógica que no le
/// corresponde a una prueba.
/// </remarks>
public sealed class ResolucionDelTrabajoTests : PruebaE2E
{
    private ElLaboratorio.AlumnoSembrado? _alumno;

    [OneTimeSetUp]
    public async Task SembrarAsync() => _alumno = await ElLaboratorio.SembrarAlumnoAsync("resolucion");

    [OneTimeTearDown]
    public Task LimpiarAsync() => ElLaboratorio.LimpiarAsync(_alumno);

    [Test]
    public async Task AprobarPideConfirmacionYAplicaElDesenlace()
    {
        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno!, "E2E aprobar");
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        await (await ControlListoAsync("#resolution-comment")).FillAsync("Aprobado por la suite de extremo a extremo.");
        await (await ControlListoAsync("[data-gf-outcome='Approve']")).ClickAsync();

        // EL DIALOGO ES PARTE DEL CONTRATO: el wireframe lo declara y el desenlace es irreversible.
        var dialogo = Page.Locator("dialog[data-gf-dialog]");
        await Expect(dialogo).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });
        await Expect(dialogo).ToContainTextAsync("Es definitivo");
        await Expect(dialogo).ToContainTextAsync("Aprobado por la suite de extremo a extremo.");

        await Page.ClickAsync("[data-gf-confirm-outcome='Approve']");
        await Page.WaitForURLAsync(new Regex(@"/entrega-comision"), new() { Timeout = EsperaDelCircuito });

        // EL DOCENTE TIENE QUE ENTERARSE DE QUE PASO. Hasta el 2026-09-02 la operacion se aplicaba
        // y el listado volvia mudo: habia que buscar la fila para saber si habia salido. Es el
        // reporte original del Product Owner —«el boton no funciona»— sobreviviendo a un producto
        // que ya funciona, y por eso la afirmacion del acuse va JUNTO a la del dato.
        //
        // SE BUSCA EL TEXTO CON SU TILDE, Y ESO NO ES UN DETALLE DE ORTOGRAFIA. Este aserto decia
        // «quedo finalizado» y el producto emite «quedó finalizado» —`ClassSubmissionList.razor`
        // §345—: nunca pudo coincidir. Entro en `main` el 2026-09-02 junto con el acuse que
        // verifica, en la misma unidad, sin que la suite se volviera a correr contra un producto
        // que ya lo emitia. Lo encontro el banco local el mismo dia. La coincidencia de texto NO
        // normaliza acentos: «quedo» y «quedó» son dos cadenas distintas.
        await Expect(Page.GetByText("quedó finalizado")).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });

        // LA AFIRMACION QUE VALE.
        Assert.That(await ElLaboratorio.EstadoDelTrabajoAsync(trabajo), Is.EqualTo("Approved"));
    }

    [Test]
    public async Task CancelarEnElDialogoNoAplicaNada()
    {
        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno!, "E2E cancelar");
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        await (await ControlListoAsync("[data-gf-outcome='Approve']")).ClickAsync();
        await Expect(Page.Locator("dialog[data-gf-dialog]")).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });
        await Page.ClickAsync("[data-gf-dialog-dismiss]");

        await Expect(Page.Locator("[data-gf-outcome='Approve']")).ToBeVisibleAsync();
        Assert.That(await ElLaboratorio.EstadoDelTrabajoAsync(trabajo), Is.EqualTo("Submitted"));
    }

    [Test]
    public async Task ElTrabajoResueltoDeclaraSuDesenlaceYSigueOfreciendoRetirar()
    {
        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno!, "E2E resuelto");
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        await (await ControlListoAsync("[data-gf-outcome='Reject']")).ClickAsync();
        await Expect(Page.Locator("dialog[data-gf-dialog]")).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });
        await Page.ClickAsync("[data-gf-confirm-outcome='Reject']");
        await Page.WaitForURLAsync(new Regex(@"/entrega-comision"), new() { Timeout = EsperaDelCircuito });

        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // HASTA EL 2026-09-02 EL BLOQUE ENTERO DESAPARECIA al resolver, y con él la acción de
        // retirar, que el wireframe declara disponible en los tres estados visibles.
        await Expect(Page.GetByText("Esta entrega ya tiene desenlace")).ToBeVisibleAsync();
        await Expect(Page.Locator("[data-gf-withdraw]")).ToBeVisibleAsync();
        await Expect(Page.Locator("[data-gf-outcome='Approve']")).ToHaveCountAsync(0);
    }
}
