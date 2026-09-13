using GeometriaFactory.Api.Composition;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Levanta la pieza de datos DE VERDAD, en memoria, sobre un almacén propio de cada prueba.
/// </summary>
/// <remarks>
/// NO SE REEMPLAZA NINGÚN SERVICIO. El repositorio es el de EF Core, el reloj es el del sistema,
/// la derivación es PBKDF2 y la guardia del acceso firmado es la del producto: lo que se
/// verifica es el cableado que `CompositionRoot` arma, y sustituir cualquiera de esas piezas
/// dejaría de verificarlo.
///
/// LO ÚNICO QUE CAMBIA ES CONFIGURACIÓN, y son las tres cosas que por diseño LLEGAN por
/// configuración: la cadena de conexión, la clave de firma —que «se recibe y no se busca»— y el
/// coste de la derivación. El coste baja a una iteración porque la batería ejercita el camino,
/// no la resistencia del parámetro: la resistencia es una medición y vive en otro lado.
///
/// POR QUÉ EL PUNTO DE ENTRADA SE NOMBRA CON `TwoPhaseStartup` Y NO CON `Program`. Las dos
/// piezas desplegables tienen su propio `Program` en el espacio de nombres global, y esta
/// batería referencia a las dos: nombrar `Program` sería ambiguo. Lo que el andamiaje necesita
/// es un tipo CUALQUIERA del ensamblado de arranque, y se elige el que hace de arranque.
///
/// EL ALMACÉN ES UN ARCHIVO Y NO MEMORIA, y es deliberado: el segundo criterio de transición de
/// la etapa `c` exige que el cambio **persista entre reinicios**, y un almacén en memoria no
/// sobrevive al reinicio por definición, con lo cual la prueba no probaría nada.
///
/// ADMITE CONFIGURACIÓN ADICIONAL, Y NADA MÁS. Las llaves que una batería pase por el constructor
/// se aplican después de las tres de arriba, por el mismo camino por el que llegan en el despliegue:
/// es lo que le permite a `RateLimitingTests` declarar una red de proxy conocida o un umbral en cero
/// sin sustituir ningún servicio. No es `sealed` por el mismo motivo: esa batería necesita simular
/// la dirección del zócalo, que el servidor en memoria no tiene, y lo hace derivando de acá.
/// </remarks>
public class DataServiceHarness : WebApplicationFactory<TwoPhaseStartup>
{
    /// <summary>Clave de firma de prueba. No es la de ningún entorno: llega por configuración, como la real.</summary>
    public const string SigningKey = "clave-de-firma-solo-para-la-bateria-de-pruebas-32+";

    private readonly string _storePath;
    private readonly IReadOnlyDictionary<string, string?> _settings;

    public DataServiceHarness(string storePath, IReadOnlyDictionary<string, string?>? settings = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storePath);
        _storePath = storePath;
        _settings = settings ?? new Dictionary<string, string?>();
    }

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseSetting("ConnectionStrings:Store", $"Data Source={_storePath}");
        builder.UseSetting($"AccessToken:{nameof(SigningKey)}", SigningKey);
        builder.UseSetting("PasswordDerivation:Iterations", "1");

        foreach (var (key, value) in _settings)
        {
            builder.UseSetting(key, value);
        }
    }

    /// <summary>Una carpeta temporal propia, con un archivo de almacén que TODAVÍA NO EXISTE.</summary>
    public static string ReserveStorePath()
    {
        var directory = Path.Combine(Path.GetTempPath(), "gf-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, "geometriafactory.db");
    }

    public static void DiscardStore(string storePath)
    {
        var directory = Path.GetDirectoryName(storePath);
        if (directory is not null && Directory.Exists(directory))
        {
            Directory.Delete(directory, recursive: true);
        }
    }
}
