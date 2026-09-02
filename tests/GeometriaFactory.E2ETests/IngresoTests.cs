using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La puerta del laboratorio: entrar, y que no se entre.
/// </summary>
/// <remarks>
/// SI SE BORRA ESTA CLASE deja de detectarse el defecto que bloquea todo lo demás: un ingreso roto
/// hace inútil cualquier otra prueba, y hasta hoy sólo lo cubría una prueba por HTTP —que no ve la
/// pantalla ni la marca de sesión del navegador—.
/// </remarks>
public sealed class IngresoTests : PruebaE2E
{
    [Test]
    public async Task ElAdministradorEntraYAterrizaEnElListadoDeLaComision()
    {
        await IngresarComoAdministradorAsync();

        // EL DESTINO ES PARTE DEL CONTRATO, no un detalle: `NAV-09` declara que el administrador
        // aterriza en el listado de la comisión, y ahí es donde tiene su trabajo.
        await Expect(Page).ToHaveURLAsync(new Regex(@"/entrega-comision$"));
    }

    [Test]
    public async Task LaCredencialEquivocadaNoEntraYLaPantallaLoDice()
    {
        await IngresarAsync(ElLaboratorio.CorreoDelAdministrador, "esta-no-es-la-clave-2026");

        // SE COMPRUEBAN LAS DOS MITADES. Que no entre es lo importante; que LO DIGA es lo que
        // separa un rechazo de una pantalla que no hizo nada, y esa diferencia costó cuatro
        // reportes en este producto.
        await Expect(Page).ToHaveURLAsync(new Regex(@"/ingreso"));
        await Expect(Page.Locator(".gf-banner--error")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LaPortadaLlevaAlIngresoCuandoNoHaySesion()
    {
        await Page.GotoAsync("/", new() { WaitUntil = WaitUntilState.Load });

        await Expect(Page).ToHaveURLAsync(new Regex(@"/ingreso"));
    }
}
