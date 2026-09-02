using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La ventana muerta: los segundos entre que la pantalla se dibuja y el circuito engancha.
/// </summary>
/// <remarks>
/// ES EL DEFECTO QUE COSTO CUATRO REPORTES, y por eso tiene clase propia.
///
/// En este anfitrión NO HAY WEBSOCKET —la negociación ofrece `ServerSentEvents` y `LongPolling`—,
/// así que establecer el circuito lleva segundos. Medido el 2026-09-02: la página del trabajo carga
/// a los 0.9 s y su botón recién responde a los 6.4 s. En el medio, la pantalla PARECE lista.
///
/// El Product Owner apretaba ahí, cuatro veces, y reportaba «no hace nada». Tenía razón: no hacía
/// nada, y NADA SE LO DECIA.
///
/// SI SE BORRA ESTA CLASE, la próxima vez que alguien agregue una superficie interactiva sin la
/// guarda, el defecto vuelve y nadie se entera hasta que un docente lo sufra.
/// </remarks>
public sealed class VentanaMuertaTests : PruebaE2E
{
    private ElLaboratorio.AlumnoSembrado? _alumno;
    private Guid _trabajo;

    [OneTimeSetUp]
    public async Task SembrarAsync()
    {
        _alumno = await ElLaboratorio.SembrarAlumnoAsync("ventana");
        _trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(_alumno, "E2E ventana muerta");
    }

    [OneTimeTearDown]
    public Task LimpiarAsync() => ElLaboratorio.LimpiarAsync(_alumno);

    [Test]
    public async Task AlAbrirUnTrabajoLosControlesEstanInhabilitadosYLaPantallaLoDice()
    {
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync($"/trabajos/{_trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // NO SE ESPERA NADA ANTES DE MIRAR, y ese es el punto: se mira lo que ve una persona que
        // llega a la pantalla. Poner una espera acá volvería la prueba ciega justo al defecto.
        await Expect(Page.Locator("[data-gf-outcome='Approve']")).ToBeDisabledAsync();
        await Expect(Page.Locator("[data-gf-preparando]")).ToBeVisibleAsync();
    }

    [Test]
    public async Task CuandoElCircuitoEnganchaLosControlesSeHabilitanSolos()
    {
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync($"/trabajos/{_trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // SIN RECARGAR Y SIN TOCAR NADA: la pantalla tiene que resolverse sola.
        await ControlListoAsync("[data-gf-outcome='Approve']");
        await Expect(Page.Locator("[data-gf-preparando]")).ToHaveCountAsync(0);
    }
}
