using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// El camino completo del alumno, por pantalla: registrarse, entrar, cargar, corregir y reenviar.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// SI SE BORRA ESTA CLASE deja de detectarse todo lo que le puede pasar a la persona que MAS USA
/// este producto. Hasta el 2026-09-02 ningún guion de navegador abría sesión de alumno: los
/// cuatro que había eran del docente o anónimos. El camino del alumno atraviesa SIETE superficies
/// y la más grande del producto —`WorkSubmission`, 714 líneas—, y sólo estaba cubierto por
/// pruebas HTTP, que es exactamente la clase de verde que ya se cobró cuatro reportes.
///
/// ═══════════════════ POR QUE ES UN SOLO CASO Y NO SIETE ═══════════════════
///
/// Porque cada paso NECESITA el anterior: no hay forma de probar la reedición de un borrador sin
/// haber creado el borrador, y no hay borrador sin alumno que pueda entrar. Partirlo en casos
/// independientes obligaría a sembrar por atajo lo que el caso anterior ya construyó, y entonces
/// el atajo —y no la pantalla— sería lo que queda probado.
///
/// **La guía advierte contra la prueba larguísima que recorre toda la aplicación, y con razón: es
/// el antipatrón.** La diferencia es que esto NO recorre la aplicación, recorre UN objetivo —que
/// un alumno pueda entregar—, y cuando falla dice en qué paso, porque cada paso deja su propio
/// aserto. Es la excepción declarada, no un descuido.
///
/// ═══════════════════ EL BORRADOR SE PROVOCA CON TEXTO QUE NO VERIFICA ═══════════════════
///
/// «Enviar es la única forma de guardar. Si el texto no verifica, queda en borrador y lo
/// reenviás», dice la propia pantalla. Qué texto NO verifica se estableció midiendo contra el
/// servicio de datos real el 2026-09-02, y no suponiéndolo:
///
///     una cara sin «Area»      → Submitted   (produce ADVERTENCIA, no error)
///     un área declarada mal    → Submitted   (ídem)
///     un texto que no es JSON  → Draft       ← el único que deja borrador
///
/// Sólo un ERROR de validación deja el trabajo en borrador; las discrepancias de valor son
/// advertencias y el trabajo entra igual. Es una distinción del producto que conviene tener
/// escrita, porque no se adivina.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public sealed class RecorridoDelAlumnoTests : PruebaE2E
{
    private const string NombreDelTrabajo = "E2E recorrido del alumno";

    private const string TextoQueNoVerifica = "esto todavia no es un trabajo";

    private const string TextoQueSiVerifica = """
        [ { "Tipo": "Cubo", "Caras": [
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 },
          { "Tipo":"Cuadrado", "Largo": 3.00, "Ancho": 3.00, "Area": 9.00 } ],
          "Area": 54.00, "Volumen": 27.00 } ]
        """;

    private string _correo = string.Empty;
    private Guid _cuenta;

    /// <summary>
    /// Borra la cuenta que el recorrido creó POR PANTALLA.
    /// </summary>
    /// <remarks>
    /// LA IDENTIDAD SE AVERIGUA DESPUES, y no antes: este recorrido no siembra el alumno con la
    /// siembra común porque **registrarse es el primer paso de lo que se está probando**. Se toma
    /// del listado del administrador cuando ya existe.
    /// </remarks>
    [OneTimeTearDown]
    public Task LimpiarAsync() =>
        _cuenta == Guid.Empty
            ? Task.CompletedTask
            : ElLaboratorio.LimpiarAsync(new ElLaboratorio.AlumnoSembrado(_cuenta, _correo, string.Empty));

    [Test]
    public async Task DeRegistrarseAEntregarYCorregirElTrabajo()
    {
        _correo = $"e2e-recorrido-{Guid.NewGuid():n}@prueba-automatica.invalid".ToLowerInvariant();

        // ---- 1 · REGISTRARSE, POR PANTALLA -----------------------------------------------
        await Page.GotoAsync("/registro-de-cuenta", new() { WaitUntil = WaitUntilState.Load });
        await Page.FillAsync("#registration-email", _correo);
        await Page.FillAsync("#registration-first-name", "Prueba");
        await Page.FillAsync("#registration-last-name", "Recorrido");
        await Page.ClickAsync("form:has(#registration-email) button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);

        // EL REGISTRO NO DA ACCESO: la cuenta queda a la espera de que el docente la habilite. Que
        // la pantalla lo diga es parte del contrato —si dijera «ya podés entrar», el alumno
        // intentaría y fracasaría sin entender—.
        await Expect(Page.Locator("body")).ToContainTextAsync(new Regex("docente|habilit", RegexOptions.IgnoreCase));

        // ---- 2 · EL DOCENTE HABILITA -----------------------------------------------------
        // ESTE PASO VA POR EL SERVICIO DE DATOS Y NO POR PANTALLA, a propósito: habilitar es un
        // acto del ADMINISTRADOR y su recorrido tiene su propia clase. Acá es preparación.
        var (cuenta, provisoria) = await ElLaboratorio.HabilitarPorCorreoAsync(_correo);
        _cuenta = cuenta;

        // ---- 3 · ENTRAR CON LA PROVISORIA, Y QUE LO OBLIGUE A CAMBIARLA ------------------
        await IngresarAsync(_correo, provisoria);
        await Expect(Page).ToHaveURLAsync(new Regex("/credencial-propia/cambio-obligado"));

        // ---- 4 · ELEGIR LA PROPIA --------------------------------------------------------
        var propia = $"E2e-{Guid.NewGuid():n}"[..20] + "-2026";

        // EL CAMPO DE CORREO SOLO APARECE SI LA PANTALLA NO LO SABE, y descubrirlo costó un rojo:
        // cuando el desvío viene del ingreso, el producto ya conoce el correo y lo manda OCULTO en
        // vez de hacérselo reescribir a la persona. Es mejor que lo que esta prueba suponía, así
        // que la prueba se adapta al producto y no al revés.
        if (await Page.Locator("#forced-email").CountAsync() > 0)
        {
            await Page.FillAsync("#forced-email", _correo);
        }

        await Page.FillAsync("#forced-provisional", provisoria);
        await Page.FillAsync("#forced-new", propia);
        await Page.FillAsync("#forced-new-repeat", propia);
        await Page.ClickAsync("form:has(#forced-new) button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);

        await IngresarAsync(_correo, propia);
        await Expect(Page).ToHaveURLAsync(new Regex("/mis-trabajos$"));

        // ---- 5 · CARGAR UN TRABAJO QUE NO VERIFICA, Y QUE QUEDE EN BORRADOR --------------
        await Page.GotoAsync("/trabajo-nuevo", new() { WaitUntil = WaitUntilState.Load });
        await Page.FillAsync("#submission-name", NombreDelTrabajo);
        await Page.FillAsync("#submission-date", "2026-08-30");
        await Page.FillAsync("#submission-text", TextoQueNoVerifica);
        await Page.ClickAsync("form:has(#submission-text) button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);

        await Page.GotoAsync("/mis-trabajos", new() { WaitUntil = WaitUntilState.Load });

        // SE BUSCA LA FILA POR SU PAPEL, NO EL TEXTO SUELTO, y eso no es preciosismo: el panel
        // dibuja CADA TRABAJO DOS VECES —la tabla para pantalla ancha y las tarjetas apiladas para
        // el teléfono— más un rótulo accesible por cada acción. Buscar el nombre a secas devuelve
        // OCHO coincidencias para un solo trabajo, y la prueba se cae por estricta.
        //
        // No es un defecto del producto: las dos formas son deliberadas, y los rótulos accesibles
        // nombran el trabajo porque el wireframe lo exige. Es la prueba la que tiene que apuntar.
        var encabezado = Page.GetByRole(AriaRole.Rowheader, new() { Name = NombreDelTrabajo });
        await Expect(encabezado).ToBeVisibleAsync();

        var fila = Page.Locator("tr", new() { Has = encabezado });
        await Expect(fila).ToContainTextAsync("Borrador");

        // ---- 6 · REEDITARLO, Y QUE LA FECHA VUELVA ---------------------------------------
        // ESTE ASERTO CUIDA UN DEFECTO ARREGLADO EL 2026-09-02: la reedición le pasaba al control
        // de fecha el valor guardado tal cual, el control lo descartaba EN SILENCIO, y al reenviar
        // la pantalla le decía al alumno que le faltaba la fecha QUE EL HABIA DECLARADO.
        // `.First` POR EL MISMO MOTIVO: el enlace de editar existe en la tabla y en la tarjeta.
        var editar = Page.Locator("a[href*='/editar']").First;
        await editar.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.Load);

        await Expect(Page.Locator("#submission-date")).ToHaveValueAsync("2026-08-30");
        await Expect(Page.Locator("#submission-name")).ToHaveValueAsync(NombreDelTrabajo);

        // ---- 7 · CORREGIR Y REENVIAR ------------------------------------------------------
        await Page.FillAsync("#submission-text", TextoQueSiVerifica);
        await Page.ClickAsync("form:has(#submission-text) button[type=submit]");
        await Page.WaitForLoadStateAsync(LoadState.Load);

        // ---- 8 · Y QUE EL SERVICIO DE DATOS LO CONFIRME -----------------------------------
        // NO ALCANZA CON QUE LA PANTALLA LO MUESTRE. Es la misma regla que en la resolución: la
        // afirmación que vale la da quien guarda el dato.
        var trabajo = await ElLaboratorio.UnicoTrabajoDeAsync(_cuenta);
        Assert.That(await ElLaboratorio.EstadoDelTrabajoAsync(trabajo), Is.EqualTo("Submitted"),
            "El trabajo corregido tiene que haber pasado de Borrador a Pendiente.");
    }
}
