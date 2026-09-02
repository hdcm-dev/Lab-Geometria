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

        var antes = await Page.Locator(".read-at").InnerTextAsync();
        await (await ControlListoAsync("button.action")).ClickAsync();

        // LA MARCA DE LECTURA LA REESCRIBE EL SERVIDOR: si cambia, el manejador corrió allá y
        // volvió por el circuito. Es la prueba de vida más barata que este producto da de sí mismo,
        // y contesta una pregunta que nadie podía contestar antes del 2026-09-01.
        await Expect(Page.Locator(".read-at")).Not.ToHaveTextAsync(antes, new() { Timeout = EsperaDelCircuito });
    }
}
