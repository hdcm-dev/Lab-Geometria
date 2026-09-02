using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using NUnit.Framework;

namespace GeometriaFactory.E2ETests;

/// <summary>
/// Levanta un laboratorio entero y efímero —servicio de datos, pieza pública y almacén propio—
/// para que la suite se pueda correr sin depender de un despliegue ni de un secreto.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// POR QUE HACE FALTA, Y NO ES COMODIDAD. Hasta el 2026-09-02 esta suite SOLO SABIA CORRER
/// CONTRA EL ANFITRION PUBLICO: exigía cuatro variables de entorno, dos de ellas secretas, y
/// sembraba cuentas en el laboratorio donde el docente tiene alumnos de verdad. Eso tiene tres
/// consecuencias, y las tres se pagaron:
///
///   1. **Nadie podía correrla en su máquina** sin la credencial del administrador del
///      laboratorio real. Una prueba que sólo se puede correr con un secreto de producción es
///      una prueba que no se corre antes de empujar el cambio.
///   2. **El rojo del anfitrión se confundía con el rojo del producto.** Trece casos rojos en la
///      primera corrida, ninguno del producto: el anfitrión no atendía.
///   3. **Cada corrida tocaba datos reales**, y por eso la suite tuvo que crecer una disciplina
///      entera de siembra y limpieza para no romper nada ajeno.
///
/// El banco local no reemplaza al modo desplegado —que sigue siendo el único que puede decir
/// «el sitio publicado anda»—, PERO ES EL QUE SE CORRE ANTES DE EMPUJAR: mismo código de
/// prueba, mismo navegador, laboratorio propio y descartable.
///
/// ═══════════════════ QUE CORRE, Y POR QUE ASI ═══════════════════
///
/// **SE CORRE LA PUBLICACION, NO EL PROYECTO**, y es la lección que `tools/verificar-
/// resolucion-del-trabajo.sh` dejó escrita el 2026-09-01: `dotnet run` sirve los archivos del
/// marco desde el manifiesto de recursos estáticos del proyecto, y una publicación los sirve
/// como archivos de verdad —que es lo que hace el anfitrión—. Un banco que no corre lo mismo
/// que el anfitrión inventa defectos propios y tapa los ajenos.
///
/// **EL ENTORNO ES `Development` Y EL TRANSPORTE ES HTTP LLANO**, y acá hay un apartamiento que
/// se declara en vez de disimularse: en `Production` la cookie de sesión es `Secure`
/// (`Web/Program.cs` §115-127) y sobre HTTP el navegador la descarta, de modo que NO HABRIA
/// SESION POSIBLE. Montar HTTPS con un certificado efímero es posible —el guion de verificación
/// lo hace, y le costó un día entender que Chromium tampoco guarda cookies `Secure` en un
/// origen que no valida—, pero pedirle eso a la corrida de cada día la volvería frágil. Lo que
/// el banco local NO PUEDE ver, entonces, es un defecto que dependa de la marca `Secure`; para
/// eso está el modo desplegado, que corre contra el anfitrión de verdad.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public static class BancoLocal
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    private static Process? _servicioDeDatos;
    private static Process? _piezaPublica;
    private static string _trabajo = string.Empty;

    /// <summary>Está en pie: lo usan el desmontaje y los mensajes de la corrida.</summary>
    public static bool EnPie => _servicioDeDatos is not null || _piezaPublica is not null;

    /// <summary>Levanta el banco y devuelve cómo entrar: dirección pública, servicio de datos y administrador.</summary>
    public static async Task<(string UrlBase, string UrlApi, string Correo, string Clave)> LevantarAsync()
    {
        var raiz = RaizDelRepositorio();
        _trabajo = Directory.CreateTempSubdirectory("gf-e2e-").FullName;

        AvisarSiFaltaElBundleDelVisor(raiz);

        var puertoApi = PuertoLibre();
        var puertoWeb = PuertoLibre();
        var urlApi = $"http://127.0.0.1:{puertoApi}";
        var urlWeb = $"http://127.0.0.1:{puertoWeb}";

        var api = Path.Combine(_trabajo, "servicio-de-datos");
        var web = Path.Combine(_trabajo, "pieza-publica");

        TestContext.Progress.WriteLine($"Banco local: publicando en «{_trabajo}»…");
        await PublicarAsync(raiz, Path.Combine("src", "GeometriaFactory.Api", "GeometriaFactory.Api.csproj"), api);
        await PublicarAsync(raiz, Path.Combine("src", "GeometriaFactory.Web", "GeometriaFactory.Web.csproj"), web);

        // LA CLAVE DE FIRMA SE GENERA POR CORRIDA Y NO SE ESCRIBE EN NINGUN LADO. El servicio de
        // datos no arranca sin ella —lo hace cumplir una guardia de arranque—, y una clave fija
        // en el árbol de fuentes sería exactamente la clase de secreto que este repositorio se
        // niega a versionar.
        var claveDeFirma = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(48));

        _servicioDeDatos = Arrancar(api, "GeometriaFactory.Api.dll", new()
        {
            ["ConnectionStrings__Store"] = $"Data Source={Path.Combine(_trabajo, "banco.db")}",
            ["AccessToken__SigningKey"] = claveDeFirma,
            ["Kestrel__Endpoints__Http__Url"] = urlApi,
        });

        await EsperarAsync($"{urlApi}/salud", "el servicio de datos", _servicioDeDatos);

        _piezaPublica = Arrancar(web, "GeometriaFactory.Web.dll", new()
        {
            // LA BARRA FINAL NO ES DECORACION: la pieza pública compone las rutas relativas
            // sobre esta dirección, y sin ella `Uri` se come el último segmento.
            ["ApiBaseUrl"] = urlApi + "/",
            ["Kestrel__Endpoints__Http__Url"] = urlWeb,
        });

        await EsperarAsync($"{urlWeb}/ingreso", "la pieza pública", _piezaPublica);

        var (correo, clave) = await SembrarAdministradorAsync(urlApi);

        TestContext.Progress.WriteLine($"Banco local en pie · pública {urlWeb} · datos {urlApi}");
        return (urlWeb, urlApi, correo, clave);
    }

    /// <summary>Baja las dos piezas y borra el almacén y la publicación.</summary>
    /// <remarks>
    /// SE TRAGA LOS ERRORES A PROPOSITO, igual que la limpieza de la siembra: corre después de
    /// que la corrida ya dio su veredicto, y un fallo acá taparía el resultado real con un error
    /// de plomería.
    /// </remarks>
    public static void Bajar()
    {
        Matar(ref _piezaPublica);
        Matar(ref _servicioDeDatos);

        if (_trabajo.Length == 0)
        {
            return;
        }

        try
        {
            Directory.Delete(_trabajo, recursive: true);
        }
        catch (Exception falla)
        {
            Console.Error.WriteLine($"[E2E] No se pudo borrar «{_trabajo}»: {falla.GetType().Name}.");
        }

        _trabajo = string.Empty;
    }

    // ---- EL MONTAJE ---------------------------------------------------------------------------

    /// <summary>
    /// El administrador del banco: se crea acá porque el almacén nace vacío.
    /// </summary>
    /// <remarks>
    /// ES LA DIFERENCIA MAS GRANDE CON EL MODO DESPLEGADO, y conviene tenerla a la vista: contra
    /// el laboratorio real la suite NO PUEDE crear su administrador —el almacén tiene unicidad
    /// sobre el papel y responde `UNIQUE constraint failed: Account.Role`—, así que entra con la
    /// credencial del docente. Acá el almacén es de esta corrida y no hay nadie: el
    /// aprovisionamiento inicial es lo primero que pasa, como en un laboratorio recién montado.
    /// </remarks>
    private static async Task<(string Correo, string Clave)> SembrarAdministradorAsync(string urlApi)
    {
        var correo = "e2e.admin@prueba-automatica.invalid";
        var clave = $"E2e-{Guid.NewGuid():n}"[..20] + "-2026";

        using var cliente = new HttpClient { BaseAddress = new Uri(urlApi + "/") };
        var alta = await cliente.PostAsJsonAsync("cuentas/administrador",
            new { email = correo, firstName = "Prueba", lastName = "Administracion", password = clave });

        if (alta.StatusCode != HttpStatusCode.Created)
        {
            var detalle = await alta.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"El banco local no pudo configurar su administrador: HTTP {(int)alta.StatusCode} · {detalle}");
        }

        return (correo, clave);
    }

    private static Process Arrancar(string carpeta, string ensamblado, Dictionary<string, string> entorno)
    {
        var arranque = new ProcessStartInfo("dotnet")
        {
            // LA RAIZ DE CONTENIDO ES LA CARPETA PUBLICADA. Sin esto no se encuentra `wwwroot` y
            // la hoja de estilos, el guion de superficie y los archivos del marco responden 404:
            // el sitio se dibuja desnudo y muerto, y las pruebas fallan hablando de otra cosa.
            WorkingDirectory = carpeta,
            UseShellExecute = false,
        };

        arranque.ArgumentList.Add(Path.Combine(carpeta, ensamblado));
        arranque.Environment["ASPNETCORE_ENVIRONMENT"] = "Development";
        arranque.Environment["Logging__LogLevel__Default"] = "Warning";

        foreach (var (llave, valor) in entorno)
        {
            arranque.Environment[llave] = valor;
        }

        return Process.Start(arranque)
            ?? throw new InvalidOperationException($"No se pudo arrancar «{ensamblado}».");
    }

    private static async Task PublicarAsync(string raiz, string proyecto, string destino)
    {
        var inicio = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = raiz,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        foreach (var argumento in new[]
                 {
                     "publish", proyecto, "--configuration", "Release", "--output", destino, "--nologo",
                 })
        {
            inicio.ArgumentList.Add(argumento);
        }

        using var proceso = Process.Start(inicio)
            ?? throw new InvalidOperationException(
                "No se pudo ejecutar `dotnet`: el banco local necesita el kit de desarrollo en el PATH.");

        // LAS DOS SALIDAS SE LEEN A LA VEZ: esperar a una con el búfer de la otra lleno traba el
        // proceso, y el síntoma es una corrida que no avanza y no dice por qué.
        var salida = proceso.StandardOutput.ReadToEndAsync();
        var error = proceso.StandardError.ReadToEndAsync();
        await proceso.WaitForExitAsync();

        if (proceso.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"`dotnet publish {proyecto}` terminó con código {proceso.ExitCode}." +
                $"{Environment.NewLine}{await salida}{Environment.NewLine}{await error}");
        }
    }

    private static async Task EsperarAsync(string url, string quien, Process? proceso)
    {
        using var cliente = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        var limite = DateTime.UtcNow.AddSeconds(120);

        while (DateTime.UtcNow < limite)
        {
            if (proceso?.HasExited == true)
            {
                throw new InvalidOperationException(
                    $"{quien} terminó solo con código {proceso.ExitCode} antes de atender. " +
                    "Suele ser una llave de configuración que falta: la guardia de arranque " +
                    "detiene la pieza antes de que atienda nada.");
            }

            try
            {
                using var respuesta = await cliente.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    return;
                }
            }
            catch (HttpRequestException)
            {
                // Todavía no escucha.
            }
            catch (TaskCanceledException)
            {
                // Tardó más que el margen de este intento.
            }

            await Task.Delay(500);
        }

        throw new TimeoutException($"{quien} no respondió en {url} en 120 segundos.");
    }

    /// <summary>
    /// Avisa —fuerte— si el bundle del visor no está.
    /// </summary>
    /// <remarks>
    /// NO SE CONSTRUYE EL BUNDLE DESDE ACA, Y ES DELIBERADO: empaquetarlo pide Node y una
    /// instalación de dependencias, que es una segunda cadena de herramientas metida adentro de
    /// un fixture de prueba. Lo hace `scripts/pruebas-e2e.sh`, que es el camino declarado.
    ///
    /// PERO SE AVISA, porque el modo de fallar sin aviso es pésimo: el visor no carga, la escena
    /// no aparece, y los casos que la miran fallan hablando del producto cuando lo que falta es
    /// un artefacto de construcción que ni siquiera se versiona.
    /// </remarks>
    private static void AvisarSiFaltaElBundleDelVisor(string raiz)
    {
        var bundle = Path.Combine(raiz, "src", "GeometriaFactory.Web", "wwwroot", "js", "geometriafactory-visor.js");

        if (File.Exists(bundle))
        {
            return;
        }

        Console.Error.WriteLine(
            "[E2E] NO ESTA EL BUNDLE DEL VISOR (`wwwroot/js/geometriafactory-visor.js`). No se " +
            "versiona: lo genera `scripts/build-visor.sh`, y `scripts/pruebas-e2e.sh` lo corre " +
            "antes de probar. Sin él la escena 3D no carga y los casos que la miran van a fallar " +
            "por una razón que NO es del producto.");
    }

    private static void Matar(ref Process? proceso)
    {
        if (proceso is null)
        {
            return;
        }

        try
        {
            if (!proceso.HasExited)
            {
                proceso.Kill(entireProcessTree: true);
                proceso.WaitForExit(10_000);
            }
        }
        catch (Exception falla)
        {
            Console.Error.WriteLine($"[E2E] No se pudo bajar un proceso del banco: {falla.GetType().Name}.");
        }
        finally
        {
            proceso.Dispose();
            proceso = null;
        }
    }

    /// <summary>
    /// Un puerto que ahora mismo está libre.
    /// </summary>
    /// <remarks>
    /// SE PIDE AL SISTEMA EN VEZ DE FIJAR UN NUMERO, y el motivo es la regla de esta casa: los
    /// contenedores del Product Owner —`gf-api`, `gf-web`, `gf-back`— no se rozan. Un puerto fijo
    /// en un guion de prueba es una colisión esperando a que las dos cosas corran juntas.
    ///
    /// QUEDA UNA VENTANA DE CARRERA entre que se suelta el puerto y que la pieza lo toma; es
    /// chica y el modo de fallar es ruidoso —la pieza no arranca y lo dice—, que es preferible a
    /// pisar un servicio ajeno.
    /// </remarks>
    private static int PuertoLibre()
    {
        using var sonda = new TcpListener(IPAddress.Loopback, 0);
        sonda.Start();
        var puerto = ((IPEndPoint)sonda.LocalEndpoint).Port;
        sonda.Stop();
        return puerto;
    }

    private static string RaizDelRepositorio()
    {
        var directorio = new DirectoryInfo(AppContext.BaseDirectory);

        while (directorio is not null)
        {
            if (directorio.GetFiles("*.sln").Length > 0)
            {
                return directorio.FullName;
            }

            directorio = directorio.Parent;
        }

        throw new DirectoryNotFoundException(
            $"No se encontró la raíz del repositorio desde «{AppContext.BaseDirectory}».");
    }
}
