using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Lo que el laboratorio declara de sí mismo, sin sesión.
/// </summary>
/// <remarks>
/// SI SE BORRA ESTA CLASE deja de detectarse la familia de defectos que estuvo SEMANAS escondida:
/// las claves de protección efímeras, que rompían sesiones, formularios y componentes interactivos
/// en silencio. El único aviso vivía en un registro del anfitrión que estaba apagado.
/// </remarks>
public sealed class EstadoDelLaboratorioTests : PruebaE2E
{
    [Test]
    public async Task LaPaginaDeEstadoNoTraeNingunAvisoDeFalla()
    {
        await Page.GotoAsync("/estado", new() { WaitUntil = WaitUntilState.Load });

        // `class="failure"` ES EL MISMO ACUERDO QUE USA LA PUBLICACION para decidir si el sitio
        // quedó bien: que la suite mire lo mismo evita dos definiciones de «sano».
        await Expect(Page.Locator(".failure")).ToHaveCountAsync(0);
    }

    [Test]
    public async Task ElLaboratorioAlcanzaSuServicioDeDatos()
    {
        await Page.GotoAsync("/estado", new() { WaitUntil = WaitUntilState.Load });

        await Expect(Page.GetByText("Almacén preparado")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Versión del servicio")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LasClavesDeProteccionSobrevivenAlProceso()
    {
        await Page.GotoAsync("/estado", new() { WaitUntil = WaitUntilState.Load });

        await Expect(Page.GetByText("Persistidas")).ToBeVisibleAsync();
    }

    [Test]
    public async Task ElArranqueNoDejoAvisosSinDeclarar()
    {
        await Page.GotoAsync("/estado", new() { WaitUntil = WaitUntilState.Load });

        await Expect(Page.GetByText("SIN DECLARAR")).ToHaveCountAsync(0);
    }

    [Test]
    public async Task LaInteractividadEstaVivaEnElAnfitrion()
    {
        await Page.GotoAsync("/estado", new() { WaitUntil = WaitUntilState.Load });

        // ═══ SE MIRA EL MOMENTO DEL SERVIDOR, Y NO LA MARCA DE LECTURA ═══
        //
        // La marca de lectura se imprime con `HH:mm:ss` —resolución de UN SEGUNDO— y eso la vuelve
        // un testigo intermitente: contra el anfitrión remoto la ida y vuelta tarda más de un
        // segundo y el texto siempre cambia, pero contra un banco local la consulta entera entra
        // adentro del mismo segundo y el texto queda IDENTICO. La prueba fallaba entonces diciendo
        // «la interactividad no está viva» cuando lo que pasaba era que estaba demasiado viva.
        // Lo encontró el banco local el 2026-09-02.
        //
        // El momento del servidor se imprime con formato `O` —hasta la diezmillonésima de
        // segundo— y además lo produce EL SERVICIO DE DATOS, no la página: si cambia, el manejador
        // corrió del otro lado del circuito Y ADEMAS salió a buscar el dato. Es un testigo más
        // fuerte y no depende de cuánto tarde la red.
        var momentoDelServidor = Page.Locator("dl.health dd").Nth(2);
        var antes = await momentoDelServidor.InnerTextAsync();
        await (await ControlListoAsync("button.action")).ClickAsync();

        await Expect(momentoDelServidor).Not.ToHaveTextAsync(antes, new() { Timeout = EsperaDelCircuito });
    }
}
