using GeometriaFactory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>
/// Abre el almacén SQLite REAL sobre el que corren los cinco actos, con las migraciones del
/// producto aplicadas por <see cref="StorePreparation"/>.
/// </summary>
/// <remarks>
/// LA RUTA LLEGA DE CONFIGURACIÓN Y NO ESTÁ ESCRITA ACÁ (`scripts/store-path.sh`): sin
/// `ConnectionStrings__Store` declarada, el sample se detiene en lugar de crear una base
/// en cualquier lado. Es la misma regla que `CompositionRoot` aplica al servicio.
///
/// PERO NO ESCRIBE EN EL ALMACÉN DE TRABAJO, Y ESTO ES UN APARTAMIENTO DECLARADO del paso 3
/// de la §3 del documento que gobierna la carpeta. **Dos de los cinco actos son destructivos**
/// —el retiro y el arrastre de la baja de cuenta— y el 2026-08-15 este producto ya perdió la
/// cuenta de administrador del Product Owner exactamente así: una rutina destructiva y la base
/// con la que alguien estaba trabajando compartiendo ruta. El sample usa un archivo HERMANO,
/// en el mismo directorio configurado, con su propio nombre, y lo borra al terminar. La regla
/// de configuración se sigue cumpliendo —la llave tiene que estar declarada—; lo que no se
/// hereda es la ruta del archivo.
/// </remarks>
internal static class Almacen
{
    internal const string LlaveDeConfiguracion = "ConnectionStrings__Store";

    /// <summary>Resuelve el archivo propio del sample, o nulo si la llave no está declarada.</summary>
    internal static string? ResolverArchivo()
    {
        var cadena = Environment.GetEnvironmentVariable(LlaveDeConfiguracion);
        if (string.IsNullOrWhiteSpace(cadena)) return null;

        var marca = "data source=";
        var desde = cadena.IndexOf(marca, StringComparison.OrdinalIgnoreCase);
        if (desde < 0) return null;

        var ruta = cadena[(desde + marca.Length)..].Split(';')[0].Trim();
        var directorio = Path.GetDirectoryName(Path.GetFullPath(ruta));
        if (string.IsNullOrEmpty(directorio)) return null;

        Directory.CreateDirectory(directorio);
        return Path.Combine(directorio, "geometriafactory-sample-infrastructure-02.db");
    }

    /// <summary>Deja el archivo en su estado de primer arranque y devuelve el contexto abierto.</summary>
    internal static async Task<GeometriaFactoryDbContext> AbrirEnPrimerArranqueAsync(string archivo)
    {
        // El primer arranque se obtiene borrando el archivo, no vaciando tablas: así el sample
        // ejercita las migraciones del producto y no una variante suya.
        if (File.Exists(archivo)) File.Delete(archivo);

        var opciones = new DbContextOptionsBuilder<GeometriaFactoryDbContext>()
            .UseSqlite($"Data Source={archivo}")
            .Options;

        var contexto = new GeometriaFactoryDbContext(opciones);
        await new StorePreparation(contexto).PrepareAsync().ConfigureAwait(false);
        return contexto;
    }
}
