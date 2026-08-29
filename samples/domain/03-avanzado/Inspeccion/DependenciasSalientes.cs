using System.Xml.Linq;

namespace GeometriaFactory.Samples.Domain.Avanzado.Inspeccion;

/// <summary>
/// Cuenta las referencias salientes del proyecto de código del dominio, **leyendo su archivo de
/// proyecto** y no afirmándolo.
/// </summary>
/// <remarks>
/// ES LA DIFERENCIA ENTRE DECIR Y DEMOSTRAR. Que el dominio no dependa de nada es una afirmación
/// que el corpus repite en varios lugares; acá se **verifica sobre el archivo**, que es lo que
/// `D9` pide de toda afirmación sobre el estado del sistema.
/// </remarks>
internal static class DependenciasSalientes
{
    /// <summary>Lo que delataría persistencia o transporte si el dominio los arrastrara.</summary>
    private static readonly string[] Delatoras =
        ["EntityFramework", "Dapper", "Npgsql", "Sqlite", "SqlClient", "Http", "Grpc", "RabbitMQ", "Redis"];

    internal static (int Salientes, int Infraestructura) Medir(string rutaDelProyecto)
    {
        var doc = XDocument.Load(rutaDelProyecto);

        var paquetes = doc.Descendants("PackageReference")
            .Select(x => (string?)x.Attribute("Include") ?? "").ToArray();
        var proyectos = doc.Descendants("ProjectReference")
            .Select(x => (string?)x.Attribute("Include") ?? "").ToArray();

        var infraestructura = paquetes.Count(
            p => Delatoras.Any(d => p.Contains(d, StringComparison.OrdinalIgnoreCase)));

        return (paquetes.Length + proyectos.Length, infraestructura);
    }
}
