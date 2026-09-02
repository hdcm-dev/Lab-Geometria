using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// La base de todos los recorridos: contexto con la dirección del laboratorio, el ingreso, y la
/// evidencia de lo que falló.
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

    private readonly List<IBrowserContext> _contextosPropios = [];

    private bool _trazando;

    /// <summary>
    /// La traza se graba siempre y se conserva sólo cuando el caso falla. Se apaga con `TRAZAR=false`.
    /// </summary>
    /// <remarks>
    /// NO HAY `on-first-retry` EN EL BINDING DE .NET —no hay reintentos—, así que el ciclo de vida
    /// de la traza se maneja acá a mano. Y hace falta: un rojo de esta suite contra un despliegue
    /// no se reproduce apretando F5. La traza trae el DOM paso a paso, la red y la consola, y se
    /// abre con `playwright show-trace`.
    /// </remarks>
    private static bool TrazaHabilitada =>
        !string.Equals(Environment.GetEnvironmentVariable("TRAZAR"), "false", StringComparison.OrdinalIgnoreCase);

    public override BrowserNewContextOptions ContextOptions() => OpcionesDeContexto();

    /// <summary>Las opciones del contexto, para poder reusarlas al abrir uno de otra medida.</summary>
    private static BrowserNewContextOptions OpcionesDeContexto() => new()
    {
        BaseURL = ElLaboratorio.UrlBase,
        Locale = "es-AR",
        TimezoneId = "America/Argentina/Buenos_Aires",

        // EL LABORATORIO DESPLEGADO SE SIRVE POR HTTPS CON CERTIFICADO VALIDO, así que esto NO se
        // relaja. Si alguna vez hace falta, es señal de que se está probando otra cosa. El banco
        // local va por HTTP llano y a esta opción no le cambia nada.
        IgnoreHTTPSErrors = false,
    };

    [SetUp]
    public async Task EmpezarLaTrazaAsync()
    {
        if (!TrazaHabilitada)
        {
            return;
        }

        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true,
            Title = TestContext.CurrentContext.Test.Name,
        });

        _trazando = true;
    }

    /// <summary>Guarda traza y captura del caso que falló, y cierra los contextos que abrió el caso.</summary>
    /// <remarks>
    /// LA CAPTURA VA ADEMAS DE LA TRAZA, y no es redundancia: la traza hay que abrirla con una
    /// herramienta, y en esta casa la evidencia de lo visual se mira. El defecto de las 768 px se
    /// cerró una vez sin mirar un teléfono con filas, y al mirarlo aparecieron tres recortes que
    /// los conteos daban por buenos.
    /// </remarks>
    [TearDown]
    public async Task GuardarLaEvidenciaSiFalloAsync()
    {
        var fallo = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;

        if (fallo)
        {
            try
            {
                await CapturarAsync(Page, "pantalla");
            }
            catch (Exception falla)
            {
                Console.Error.WriteLine($"[E2E] No se pudo capturar la pantalla: {falla.GetType().Name}.");
            }
        }

        // LAS VENTANAS PROPIAS DEL CASO TAMBIEN DEJAN SU FOTO, y esto se agregó porque faltaba:
        // el primer rojo de un caso de la versión angosta capturó la ventana de escritorio —la
        // que `PageTest` crea y el caso no usa— y no mostraba nada de lo que había fallado.
        var ventana = 0;

        foreach (var contexto in _contextosPropios)
        {
            // EL DESMONTAJE NO PUEDE TAPAR EL VEREDICTO, y por eso se traga lo suyo. Cuando el
            // caso ya falló, la ventana puede estar cerrada o el navegador caído: pedirle una
            // captura o cerrarla lanza, y NUnit reporta ESA excepción encima de la que importa
            // —pasó, y el rojo que se leía era «Target page has been closed» en vez del aserto—.
            try
            {
                if (fallo)
                {
                    foreach (var pagina in contexto.Pages)
                    {
                        await CapturarAsync(pagina, $"ventana-{++ventana}");
                    }
                }

                await contexto.CloseAsync();
            }
            catch (Exception falla)
            {
                Console.Error.WriteLine($"[E2E] No se pudo cerrar una ventana propia: {falla.GetType().Name}.");
            }
        }

        _contextosPropios.Clear();

        if (!_trazando)
        {
            return;
        }

        _trazando = false;

        if (!fallo)
        {
            await Context.Tracing.StopAsync();
            return;
        }

        var archivo = Path.Combine(CarpetaDeEvidencia("trazas"), $"{NombreDeArchivo()}.zip");
        await Context.Tracing.StopAsync(new() { Path = archivo });
        TestContext.Progress.WriteLine($"Traza del caso fallido: {archivo}");
    }

    /// <summary>
    /// Abre una página en una ventana de la medida pedida, con el mismo laboratorio y el mismo idioma.
    /// </summary>
    /// <remarks>
    /// ES LO QUE HACE POSIBLE PROBAR LA VERSION ANGOSTA SIN UNA SEGUNDA CORRIDA. El binding de
    /// .NET fija el tamaño en el contexto, y el contexto de `PageTest` ya está creado cuando el
    /// caso empieza: la única forma de mirar dos medidas en un mismo caso —que es exactamente lo
    /// que hace falta para afirmar «acá se ve la tabla y allá las tarjetas»— es abrir un contexto
    /// propio. Se cierra solo en el desmontaje.
    ///
    /// NO SE USA UN DESCRIPTOR DE DISPOSITIVO —`Playwright.Devices["Pixel 7"]`— aunque sería más
    /// vistoso: lo que el sistema visual declara es UN ANCHO EN PIXELES, y emular un teléfono
    /// entero traería además factor de escala, agente de usuario y eventos táctiles, o sea tres
    /// variables más para explicar un rojo. Se prueba lo que la hoja de estilos dice.
    /// </remarks>
    protected async Task<IPage> AbrirVentanaAsync(int ancho, int alto)
    {
        var opciones = OpcionesDeContexto();
        opciones.ViewportSize = new() { Width = ancho, Height = alto };

        var contexto = await Browser.NewContextAsync(opciones);
        _contextosPropios.Add(contexto);

        return await contexto.NewPageAsync();
    }

    /// <summary>Entra con una credencial y deja el navegador donde el producto lo mande.</summary>
    protected Task IngresarAsync(string correo, string clave) => IngresarAsync(Page, correo, clave);

    /// <summary>Idem, en la página que se le pase: lo usan los casos que abren su propia ventana.</summary>
    protected static async Task IngresarAsync(IPage pagina, string correo, string clave)
    {
        await pagina.GotoAsync("/ingreso", new() { WaitUntil = WaitUntilState.Load });
        await pagina.FillAsync("#signin-email", correo);
        await pagina.FillAsync("#signin-password", clave);
        // SE APRIETA Y SE ESPERA EL ESTADO DE CARGA, y no se usa `RunAndWaitForNavigationAsync`:
        // está obsoleto en el binding y este proyecto trata las advertencias como errores. La forma
        // vigente es esperar la carga de la página que la navegación deja.
        await pagina.ClickAsync("form:has(#signin-email) button[type=submit]");
        await pagina.WaitForLoadStateAsync(LoadState.Load);
    }

    /// <summary>Entra como el administrador del laboratorio.</summary>
    protected Task IngresarComoAdministradorAsync() =>
        IngresarAsync(ElLaboratorio.CorreoDelAdministrador, ElLaboratorio.ClaveDelAdministrador);

    /// <summary>Idem, en la página que se le pase.</summary>
    protected static Task IngresarComoAdministradorAsync(IPage pagina) =>
        IngresarAsync(pagina, ElLaboratorio.CorreoDelAdministrador, ElLaboratorio.ClaveDelAdministrador);

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
    protected Task<ILocator> ControlListoAsync(string selector) => ControlListoAsync(Page, selector);

    /// <summary>Idem, en la página que se le pase.</summary>
    protected static async Task<ILocator> ControlListoAsync(IPage pagina, string selector)
    {
        var control = pagina.Locator($"{selector}:not([disabled])");
        await control.WaitForAsync(new() { Timeout = EsperaDelCircuito });
        return control;
    }

    /// <summary>Deja una captura de la página, con nombre del caso, en la carpeta de resultados.</summary>
    /// <remarks>
    /// TAMBIEN SE USA CON EL CASO EN VERDE, y ahí está la mitad del valor: los casos de la versión
    /// angosta dejan la foto de lo que afirmaron, que es lo que se mira cuando alguien pregunta
    /// «¿y en el teléfono cómo se ve?».
    /// </remarks>
    protected static async Task<string> CapturarAsync(IPage pagina, string sufijo)
    {
        var archivo = Path.Combine(CarpetaDeEvidencia("capturas"), $"{NombreDeArchivo()}-{sufijo}.png");
        await pagina.ScreenshotAsync(new() { Path = archivo, FullPage = true });
        TestContext.Progress.WriteLine($"Captura: {archivo}");
        return archivo;
    }

    /// <summary>
    /// La carpeta donde queda la evidencia de la corrida.
    /// </summary>
    /// <remarks>
    /// ES LA MISMA QUE DECLARA `pruebas-e2e.runsettings` para los informes, y no por casualidad:
    /// así el flujo sube todo con un único paso y nadie tiene que acordarse de agregar una ruta
    /// cuando se agrega una clase de evidencia.
    /// </remarks>
    private static string CarpetaDeEvidencia(string clase)
    {
        var carpeta = Path.Combine(
            Environment.GetEnvironmentVariable("CARPETA_RESULTADOS")
            ?? Path.Combine(CarpetaDelProyecto(), "resultados-e2e"),
            clase);

        Directory.CreateDirectory(carpeta);
        return carpeta;
    }

    /// <summary>
    /// La carpeta del proyecto de pruebas, buscada hacia arriba desde los binarios.
    /// </summary>
    /// <remarks>
    /// NO SE ESCRIBE AL LADO DE LOS BINARIOS —`bin/Release/net10.0/`—, y no es cuestión de gusto:
    /// esa carpeta la borra cualquier construcción limpia, y la evidencia de la corrida se
    /// perdería en el momento en que alguien intenta reproducir el rojo. Es la misma carpeta que
    /// el flujo sube como artefacto.
    /// </remarks>
    private static string CarpetaDelProyecto()
    {
        var directorio = new DirectoryInfo(AppContext.BaseDirectory);

        while (directorio is not null)
        {
            if (directorio.GetFiles("*.csproj").Length > 0)
            {
                return directorio.FullName;
            }

            directorio = directorio.Parent;
        }

        // Sin proyecto a la vista —una corrida desde los binarios sueltos— se escribe donde se
        // está y se sigue: perder la evidencia es malo, perder la corrida por la evidencia es peor.
        return AppContext.BaseDirectory;
    }

    private static string NombreDeArchivo()
    {
        var limpio = string.Join('_', TestContext.CurrentContext.Test.FullName.Split(Path.GetInvalidFileNameChars()));
        return limpio.Length <= 120 ? limpio : limpio[^120..];
    }
}
