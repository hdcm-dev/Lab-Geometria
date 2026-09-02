using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La navegabilidad del laboratorio: que cada destino exista, que se llegue, que el que no
/// corresponde no aparezca, y que una dirección inventada termine en la pantalla que lo dice.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// SI SE BORRA ESTA CLASE deja de mirarse el armazón, que es la única parte del producto que se
/// dibuja en TODAS las pantallas. Un destino roto en la barra lateral no lo ve ninguna de las
/// otras clases de esta suite: todas entran por dirección escrita, no por el menú.
///
/// Y ES EL DEFECTO QUE MAS BARATO SE COLA: `WorkShell` decide sus destinos por papel, y el
/// producto declara que **ninguna barra muestra el destino del otro papel, ni siquiera
/// deshabilitado** —`Experiencia-De-Uso.md` §3.2—. Eso es una afirmación sobre lo que NO está,
/// y lo que no está no lo prueba nadie por accidente.
///
/// NO SE PRUEBA CON EL ALUMNO PORQUE NO HACE FALTA SEMBRARLO: el recorrido del alumno ya tiene
/// su clase, que atraviesa el registro entero. Acá alcanza con el administrador, que existe en
/// los dos modos de corrida —el desplegado y el banco local—.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public sealed class NavegacionTests : PruebaE2E
{
    /// <summary>
    /// Las rutas que el guardián 2 protege.
    /// </summary>
    /// <remarks>
    /// SON LAS MISMAS SEIS QUE ENUMERA `PanelSessionGateMiddleware`, copiadas a mano y a
    /// propósito: si alguien agrega una pantalla al panel y no la agrega al guardián, esta lista
    /// no se entera —y ahí está el valor—. Una prueba que lee la misma constante que el producto
    /// no puede contradecirlo nunca.
    /// </remarks>
    private static readonly string[] RutasDelPanel =
    [
        "/mis-trabajos", "/trabajo-nuevo", "/cuentas", "/entrega-comision", "/mi-contrasena",
    ];

    [Test]
    [TestCaseSource(nameof(RutasDelPanel))]
    public async Task SinSesionLasRutasDelPanelDesvianAlIngreso(string ruta)
    {
        await Page.GotoAsync(ruta, new() { WaitUntil = WaitUntilState.Load });

        // EL DESVIO ES A `/ingreso` Y NO A UN 403: el guardián ACOTA, no hace cumplir —quien
        // verifica de verdad es el servicio de datos, en cada solicitud—.
        await Expect(Page).ToHaveURLAsync(new Regex(@"/ingreso"));
    }

    [Test]
    public async Task UnaDireccionQueNoExisteLlegaALaPantallaQueLoDice()
    {
        var respuesta = await Page.GotoAsync("/esta-direccion-no-existe-2026", new() { WaitUntil = WaitUntilState.Load });

        // EL CODIGO SE CONSERVA, y ese es el punto de la reejecución: sin ella el cuerpo llegaba
        // vacío, y con un 200 la dirección inventada quedaría indexada como una página buena.
        Assert.That(respuesta?.Status, Is.EqualTo(404), "La dirección inventada tiene que responder 404.");
        await Expect(Page.Locator("h1")).ToHaveTextAsync("No encontramos esa dirección");
        // SE BUSCA DENTRO DEL CONTENIDO, y no en el documento entero: el armazón trae además el
        // enlace «Volver a entrar» del aviso de reconexión, que apunta a la misma dirección y
        // está oculto. Sin acotar, el localizador resuelve a dos elementos y la prueba se cae por
        // estricta —lo hizo, y está bien que sea estricta: elegir uno al azar sería mirar otra
        // cosa la mitad de las veces—.
        await Expect(Page.Locator("main a[href='/']")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LaBarraDelAdministradorTraeSusTresDestinosYNingunoDelAlumno()
    {
        await IngresarComoAdministradorAsync();

        var barra = Page.Locator("nav.gf-shell-sidebar");
        await Expect(barra.Locator("a[href='/entrega-comision']")).ToBeVisibleAsync();
        await Expect(barra.Locator("a[href='/cuentas']")).ToBeVisibleAsync();
        await Expect(barra.Locator("a[href='/mi-contrasena?papel=administrador']")).ToBeVisibleAsync();

        // LO QUE NO TIENE QUE ESTAR, que es la mitad que se olvida: ni visible ni deshabilitado.
        await Expect(barra.Locator("a[href='/mis-trabajos']")).ToHaveCountAsync(0);
        await Expect(barra.Locator("a[href='/trabajo-nuevo']")).ToHaveCountAsync(0);
    }

    [Test]
    public async Task DesdeLaBarraSeLlegaACadaDestinoDelAdministrador()
    {
        await IngresarComoAdministradorAsync();

        await Page.ClickAsync("nav.gf-shell-sidebar a[href='/cuentas']");
        await Page.WaitForLoadStateAsync(LoadState.Load);
        await Expect(Page).ToHaveURLAsync(new Regex(@"/cuentas$"));
        await Expect(Page.Locator("h1")).ToHaveTextAsync("Cuentas de la comisión");

        await Page.ClickAsync("nav.gf-shell-sidebar a[href='/entrega-comision']");
        await Page.WaitForLoadStateAsync(LoadState.Load);
        await Expect(Page).ToHaveURLAsync(new Regex(@"/entrega-comision$"));
        await Expect(Page.Locator("h1")).ToHaveTextAsync("Entrega de la comisión");
    }

    [Test]
    public async Task ElDestinoEnCursoSeMarcaYLosOtrosNo()
    {
        await IngresarComoAdministradorAsync();
        await Page.GotoAsync("/cuentas", new() { WaitUntil = WaitUntilState.Load });

        var barra = Page.Locator("nav.gf-shell-sidebar");

        // `aria-current="page"` ES A LA VEZ EL ANUNCIO PARA QUIEN NAVEGA CON LECTOR Y EL GANCHO DE
        // ESTILO —la hoja pinta `.gf-nav a[aria-current="page"]`—: es una sola marca haciendo las
        // dos cosas, y por eso perderla se nota poco y cuesta doble.
        await Expect(barra.Locator("a[href='/cuentas']")).ToHaveAttributeAsync("aria-current", "page");
        await Expect(barra.Locator("a[href='/entrega-comision']")).Not.ToHaveAttributeAsync("aria-current", "page");
    }

    [Test]
    public async Task CerrarSesionDevuelveAlIngresoYElPanelDejaDeAbrirse()
    {
        await IngresarComoAdministradorAsync();

        // EL CIERRE ES UN ENVIO DE VERDAD Y NO UN ENLACE, porque borrar la marca del navegador es
        // escribir una cabecera y dentro del circuito las cabeceras ya salieron. Si alguien lo
        // volviera un `@onclick`, la sesión se cerraría del lado del servidor y la marca quedaría
        // puesta: la pantalla diría que salió y la próxima navegación entraría igual. Eso es
        // exactamente lo que comprueba la segunda mitad de este caso.
        await Page.ClickAsync("nav.gf-shell-sidebar form[method=post] button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);
        await Expect(Page).ToHaveURLAsync(new Regex(@"/ingreso"));

        await Page.GotoAsync("/cuentas", new() { WaitUntil = WaitUntilState.Load });
        await Expect(Page).ToHaveURLAsync(new Regex(@"/ingreso"));
    }
}
