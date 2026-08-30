namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>
/// Recuento con umbral CERO sobre el árbol de fuentes del sample: ni clave de firma, ni
/// contraseña real, ni ruta de almacén escritas en el código.
/// </summary>
/// <remarks>
/// UN UMBRAL CERO SIN CONDICIÓN DE MEDICIÓN ES UN CRITERIO MAL ESCRITO, y por eso esta clase
/// declara sobre qué cuenta: los archivos `.cs` del sample, copiados a la salida por el `csproj`.
/// Si el árbol no está, la inspección **falla en lugar de dar cero**: una medición que no
/// encontró qué medir y una medición que dio cero no son lo mismo, y confundirlas convierte un
/// umbral en un adorno.
///
/// POR QUÉ SOBRE ESTA CAPA Y NO SOBRE OTRA: es la que CONOCE el valor derivado de una credencial,
/// la clave de firma y la ruta del almacén. La prohibición de exponerlos no es estilo; es la
/// única forma de que la regla de exposición del contrato del producto siga siendo cierta.
/// </remarks>
internal static class Inspecciones
{
    private static string CarpetaDeFuentes => Path.Combine(AppContext.BaseDirectory, "Fuente");

    private static IReadOnlyList<(string Archivo, string Texto)> Fuentes()
    {
        if (!Directory.Exists(CarpetaDeFuentes))
        {
            throw new InvalidOperationException(
                $"La inspección no encontró el árbol de fuentes en `{CarpetaDeFuentes}`. "
                + "No se informa cero: no hubo medición.");
        }

        return Directory.GetFiles(CarpetaDeFuentes, "*.cs", SearchOption.AllDirectories)
            .Select(a => (Path.GetFileName(a), File.ReadAllText(a)))
            .ToList();
    }

    /// <summary>Ocurrencias de los tres secretos en la fuente del sample. Umbral: 0.</summary>
    internal static int SecretosEnLaFuente(string claveDeFirma, string contrasenaReal, string rutaDelAlmacen)
    {
        // LOS TRES VALORES SE PASAN COMO ARGUMENTO Y NO SE ESCRIBEN ACÁ, que es lo que hace que la
        // inspección pueda dar cero: los tres se producen o se leen en tiempo de ejecución. Una
        // inspección que llevara adentro el valor que busca lo estaría exponiendo ella misma.
        var buscados = new[] { claveDeFirma, contrasenaReal, rutaDelAlmacen }
            .Where(v => !string.IsNullOrWhiteSpace(v) && v.Length >= 6)
            .ToList();

        return Fuentes().Sum(f => buscados.Count(v => f.Texto.Contains(v, StringComparison.Ordinal)));
    }

    /// <summary>Ocurrencias de la contraseña en claro o del valor derivado en la salida. Umbral: 0.</summary>
    /// <remarks>
    /// SE CUENTA SOBRE LA SALIDA PRODUCIDA Y NO SOBRE LA ESPERADA. La salida esperada es un texto
    /// que alguien escribió; la producida es la que un operador va a ver en una consola o en un
    /// registro, que es donde el secreto haría daño.
    /// </remarks>
    internal static int ClaroEnLaTraza(IReadOnlyList<string> salida, string clara, string derivado)
    {
        var buscados = new[] { clara, derivado }.Where(v => v.Length >= 6).ToList();
        return salida.Sum(l => buscados.Count(v => l.Contains(v, StringComparison.Ordinal)));
    }

    /// <summary>
    /// Caminos alternativos de aleatoriedad en la fuente del componente que produce provisorias.
    /// Umbral: 0.
    /// </summary>
    /// <remarks>
    /// ES LA MITAD MEDIBLE DE LA REGLA QUE `D-2` NO PUEDE PROVOCAR. Que la fuente criptográfica
    /// falle no está al alcance del sample; que NO HAYA UN SEGUNDO CAMINO por el que la provisoria
    /// se componga igual, sí. Se busca `System.Random`, la hora como semilla y el identificador de
    /// la cuenta: los tres sustitutos con los que este defecto aparece en la vida real.
    /// </remarks>
    internal static int CaminosAlternativosDeAleatoriedad()
    {
        var componente = Path.Combine(AppContext.BaseDirectory, "Fuente");
        var texto = Directory.Exists(componente)
            ? string.Join('\n', Directory.GetFiles(componente, "ProvisionalPassword*.cs", SearchOption.AllDirectories)
                .Select(File.ReadAllText))
            : string.Empty;

        // El componente vive en el producto y no en el sample, así que la fuente que se lee es la
        // suya, resuelta desde el árbol del repositorio si está disponible.
        var enElProducto = BuscarFuenteDelComponente();
        if (enElProducto is not null) texto += '\n' + File.ReadAllText(enElProducto);

        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new InvalidOperationException(
                "La inspección no encontró la fuente de `ProvisionalPasswordFactory`. "
                + "No se informa cero: no hubo medición.");
        }

        string[] sustitutos = ["new Random(", "DateTime.Now.Ticks", "accountId", "Guid.NewGuid()"];
        return sustitutos.Count(s => texto.Contains(s, StringComparison.Ordinal));
    }

    private static string? BuscarFuenteDelComponente()
    {
        var carpeta = new DirectoryInfo(AppContext.BaseDirectory);
        while (carpeta is not null)
        {
            var candidato = Path.Combine(carpeta.FullName,
                "src", "GeometriaFactory.Infrastructure", "Security", "ProvisionalPasswordFactory.cs");
            if (File.Exists(candidato)) return candidato;
            carpeta = carpeta.Parent;
        }

        return null;
    }
}
