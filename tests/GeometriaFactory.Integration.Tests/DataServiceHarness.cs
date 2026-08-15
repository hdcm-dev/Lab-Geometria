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
/// </remarks>
public sealed class DataServiceHarness : WebApplicationFactory<TwoPhaseStartup>
{
    /// <summary>Clave de firma de prueba. No es la de ningún entorno: llega por configuración, como la real.</summary>
    public const string SigningKey = "clave-de-firma-solo-para-la-bateria-de-pruebas-32+";

    private readonly string _storePath;

    public DataServiceHarness(string storePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storePath);
        _storePath = storePath;
    }

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseSetting("ConnectionStrings:Store", $"Data Source={_storePath}");
        builder.UseSetting($"AccessToken:{nameof(SigningKey)}", SigningKey);
        builder.UseSetting("PasswordDerivation:Iterations", "1");
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
