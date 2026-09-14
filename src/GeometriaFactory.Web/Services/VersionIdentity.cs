using System.Reflection;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// CMP-09 — El contrato de identidad de versión que consume el sello
/// (`Representacion-Sello-De-Version.md` §4), resuelto **una sola vez** en la composición.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`, R-14). El sello mostraba «Versión no
/// identificada» siempre, también en producción, donde la construcción sí trae su identidad: el
/// servicio de datos la publica en `/salud` desde `AssemblyInformationalVersion`. Esta clase la
/// lee del mismo lugar para el front.
///
/// LA VERSIÓN LLEGA FORMADA Y NO SE COMPONE. MinVer escribe `&lt;versión&gt;+&lt;sha&gt;`: lo anterior al `+`
/// es la versión legible, lo posterior el identificador de construcción, que es sólo para el
/// detalle. Una versión con sufijo de anticipo (`-alpha.0.15`) es **preliminar**: MinVer la calcula
/// sobre un commit sin etiqueta, que no se entrega (`Directory.Build.props`).
///
/// `0.0.0` ES LA AUSENCIA DE IDENTIDAD, NO UNA VERSIÓN. MinVer la calcula cuando la construcción no
/// tiene historial ni etiquetas, y mostrarla como versión sería inventar una. Se muestra el
/// marcador de origen indeterminado, igual que sin atributo.
/// </remarks>
public sealed record VersionIdentity(string? Version, string? BuildId, bool IsPreliminary)
{
    /// <summary>La identidad que no pudo derivarse de la construcción.</summary>
    public static VersionIdentity Indeterminate { get; } = new(null, null, false);

    /// <summary>Si el sello tiene que mostrar el marcador en lugar de una versión.</summary>
    public bool IsIndeterminate => Version is null;

    /// <summary>Interpreta una `AssemblyInformationalVersion` tal como la escribe la construcción.</summary>
    public static VersionIdentity From(string? informationalVersion)
    {
        if (string.IsNullOrWhiteSpace(informationalVersion))
        {
            return Indeterminate;
        }

        var plus = informationalVersion.IndexOf('+', StringComparison.Ordinal);
        var version = (plus < 0 ? informationalVersion : informationalVersion[..plus]).Trim();
        var build = plus < 0 ? string.Empty : informationalVersion[(plus + 1)..].Trim();

        if (version.Length == 0 || version.StartsWith("0.0.0", StringComparison.Ordinal))
        {
            return Indeterminate;
        }

        return new VersionIdentity(
            version,
            build.Length == 0 ? null : build,
            version.Contains('-', StringComparison.Ordinal));
    }

    /// <summary>La identidad del ensamblado que se está ejecutando.</summary>
    public static VersionIdentity OfAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return From(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
    }
}
