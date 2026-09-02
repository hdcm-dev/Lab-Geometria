using Microsoft.Playwright;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Una figura cuyas claves el laboratorio no reconoce: qué le dice el producto al alumno.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// ESTE CASO NACE DE UNA CONTRADICCION ENTRE TRES OBSERVADORES, y ninguno estaba equivocado.
/// En la mesa UX/UI del 2026-09-02 dos usuarios estandar afirmaron que el producto no dibuja y no
/// senala el error de formula, y una comision de especialidad afirmo que si. El peritaje resolvio
/// la contradiccion con un experimento de DOS envios que cambia UNA sola variable:
///
///     mismo cubo mal declarado, claves del contrato  → dibuja  ·  «declara 99.00 / geometria da 27.00»
///     mismo cubo mal declarado, clave «Lado»         → vacio   ·  «no salio ninguna observacion»
///                                                                y «Se dibujaron las 1 figuras»
///
/// O sea que el producto HACE LO QUE PROMETE cuando puede leer la figura, y lo que fallaba era
/// decir la verdad cuando no podia: emitia tres senales de exito —se interpreto, sin
/// observaciones, se dibujo— y ninguna de aviso. La tercera, ademas, era literalmente falsa.
///
/// ═══════════════ POR QUE ESTA PRUEBA EXISTE Y NO ALCANZABA CON LAS OTRAS ═══════════════
///
/// Las 372 pruebas de integracion llegan al punto de acceso del servicio de datos, donde este
/// caso NO ES UN DEFECTO: no derivar un valor que el texto no permite derivar es la regla
/// correcta, y esta bien fundada en `LocalFigureValidator.Discrepant`. El defecto vive
/// exclusivamente en lo que la PANTALLA afirma sobre ese resultado, y eso solo se ve abriendo la
/// pantalla.
///
/// ═══════════════ SE PROBO FALLANDO ═══════════════
///
/// Contra el laboratorio publicado ANTES del arreglo, los dos asertos de
/// `LaPantallaNoAfirmaHaberDibujadoLoQueNoDibujo` fallan: la pagina dice «Se dibujaron las 1
/// figuras del trabajo.» con el area de dibujo vacia. Una prueba que nunca se vio en rojo no es
/// una prueba.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public sealed class FiguraQueNoSePudoLeerTests : PruebaE2E
{
    /// <summary>
    /// El cubo del intake con una clave que el producto no reconoce, y el volumen mal a proposito.
    /// </summary>
    /// <remarks>
    /// `Lado` NO ES UNA CLAVE DEL CONTRATO —las dimensiones se llaman `Largo`, `Ancho`, `Radio` y
    /// `Altura`—, y sin `Caras` no hay componente del que sacar la arista. El resultado es una
    /// pieza reconstruida SIN NINGUNA DIMENSION: no hay con que dibujarla ni con que verificarla.
    /// Un cubo de lado 4 tiene volumen 64, de modo que el 48 declarado esta mal; que el producto
    /// NO lo advierta es correcto —no pudo derivar el 64— y es justamente lo que hay que decirle
    /// a la persona en vez de callarlo.
    /// </remarks>
    private const string TextoConClaveQueNoSeReconoce =
        """[ { "Tipo": "Cubo", "Lado": 4, "Area": 96, "Volumen": 48 } ]""";

    private ElLaboratorio.AlumnoSembrado? _alumno;

    [OneTimeSetUp]
    public async Task SembrarAsync() => _alumno = await ElLaboratorio.SembrarAlumnoAsync("ilegible");

    [OneTimeTearDown]
    public Task LimpiarAsync() => ElLaboratorio.LimpiarAsync(_alumno);

    [Test]
    public async Task LaPantallaNoAfirmaHaberDibujadoLoQueNoDibujo()
    {
        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(
            _alumno!, "E2E figura ilegible", TextoConClaveQueNoSeReconoce);

        await IngresarAsync(_alumno!.Correo, _alumno.Clave);
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // LO QUE NO PUEDE DECIR. Es el aserto que estaba en rojo antes del arreglo.
        await Expect(Page.GetByText("Se dibujaron")).ToHaveCountAsync(0);

        // LO QUE TIENE QUE DECIR: que esa figura no se dibujo, nombrandola.
        await Expect(Page.GetByText("No dibujada")).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });
    }

    [Test]
    public async Task LaAusenciaDeObservacionesNoSeConfundeConHaberVerificado()
    {
        var trabajo = await ElLaboratorio.SembrarTrabajoEnviadoAsync(
            _alumno!, "E2E sin verificar", TextoConClaveQueNoSeReconoce);

        await IngresarAsync(_alumno!.Correo, _alumno.Clave);
        await Page.GotoAsync($"/trabajos/{trabajo}", new() { WaitUntil = WaitUntilState.Load });

        // EL AVISO QUE FALTABA. Sin el, «no salio ninguna observacion» se lee como «tus numeros
        // estan bien», que es lo contrario de lo que ocurrio.
        await Expect(Page.GetByText("no se compararon")).ToBeVisibleAsync(new() { Timeout = EsperaDelCircuito });
    }
}
