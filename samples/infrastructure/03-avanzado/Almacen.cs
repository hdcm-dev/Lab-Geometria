namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>De dónde sale el directorio en el que el acto 5 crea y borra sus almacenes.</summary>
/// <remarks>
/// LA RUTA LLEGA DE CONFIGURACIÓN Y NO ESTÁ ESCRITA ACÁ, por dos motivos que se suman. El primero
/// es la regla del repositorio (`scripts/store-path.sh`): sin `ConnectionStrings__Store` declarada
/// el sample se detiene, igual que el servicio. El segundo es la inspección de umbral cero: una
/// ruta literal en la fuente sería, ella misma, una de las ocurrencias que la inspección cuenta.
///
/// SE USA EL DIRECTORIO Y NO EL ARCHIVO. El acto 5 crea dos almacenes propios y los borra; el de
/// trabajo no se toca. Es la misma decisión que el sample `02` y por el mismo motivo.
/// </remarks>
internal static class Almacen
{
    internal const string LlaveDeConfiguracion = "ConnectionStrings__Store";

    internal static string? ResolverDirectorio()
    {
        var cadena = Environment.GetEnvironmentVariable(LlaveDeConfiguracion);
        if (string.IsNullOrWhiteSpace(cadena)) return null;

        const string marca = "data source=";
        var desde = cadena.IndexOf(marca, StringComparison.OrdinalIgnoreCase);
        if (desde < 0) return null;

        var ruta = cadena[(desde + marca.Length)..].Split(';')[0].Trim();
        var directorio = Path.GetDirectoryName(Path.GetFullPath(ruta));
        if (string.IsNullOrEmpty(directorio)) return null;

        Directory.CreateDirectory(directorio);
        return directorio;
    }
}
