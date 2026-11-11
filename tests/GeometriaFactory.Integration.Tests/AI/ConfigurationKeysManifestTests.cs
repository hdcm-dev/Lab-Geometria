using System.Text.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Infrastructure.AI;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Criterio 31 · K7 R5.4 y K7-AP-8: cada clave <c>AI__*</c> que el código lee figura con ese nombre exacto en el
/// <c>appsettings.json</c> de su pieza y en <c>deploy/manifiesto-claves-ia.json</c>, el manifiesto que el repositorio
/// del despliegue (otro repo) contrasta con su plantilla. En PushDispatch una clave con otro nombre dejó la IA apagada.
/// </summary>
public sealed class ConfigurationKeysManifestTests
{
    private static readonly Lazy<string> Root = new(() =>
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (File.Exists(Path.Combine(dir.FullName, "deploy", "manifiesto-claves-ia.json")))
                return dir.FullName;
        }

        throw new InvalidOperationException("No se encontró deploy/manifiesto-claves-ia.json subiendo desde la prueba.");
    });

    /// <summary>Las claves que lee el servicio de datos: las propiedades de <see cref="AiOptions"/>.</summary>
    private static SortedSet<string> ApiKeys() =>
        new(typeof(AiOptions).GetProperties().Where(p => p.CanWrite).Select(p => p.Name), StringComparer.Ordinal);

    /// <summary>Las claves que lee la pieza pública: cada literal <c>"AI:Clave"</c> de su código.</summary>
    private static SortedSet<string> WebKeys()
    {
        var src = Path.Combine(Root.Value, "src", "GeometriaFactory.Web");
        var keys = new SortedSet<string>(StringComparer.Ordinal);

        foreach (var file in Directory.EnumerateFiles(src, "*.*", SearchOption.AllDirectories)
                     .Where(f => (f.EndsWith(".cs", StringComparison.Ordinal) || f.EndsWith(".razor", StringComparison.Ordinal))
                                 && !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.Ordinal)
                                 && !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.Ordinal)))
        {
            foreach (Match m in Regex.Matches(File.ReadAllText(file), "\"AI:(?<k>[A-Za-z]+)\""))
            {
                keys.Add(m.Groups["k"].Value);
            }
        }

        return keys;
    }

    private static SortedSet<string> AppSettingsAiKeys(string piece)
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(Path.Combine(Root.Value, "src", piece, "appsettings.json")));
        return new(doc.RootElement.GetProperty("AI").EnumerateObject().Select(p => p.Name), StringComparer.Ordinal);
    }

    private static SortedSet<string> ManifestKeys(string piece)
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(Path.Combine(Root.Value, "deploy", "manifiesto-claves-ia.json")));
        return new(doc.RootElement.GetProperty("piezas").GetProperty(piece).EnumerateObject()
            .Select(p => p.Name.StartsWith("AI__", StringComparison.Ordinal) ? p.Name[4..] : "!" + p.Name), StringComparer.Ordinal);
    }

    [Fact]
    public void Claves_Api_EnAppsettingsYEnElManifiesto()
    {
        var code = ApiKeys();
        Assert.Equal(code, AppSettingsAiKeys("GeometriaFactory.Api"));
        Assert.Equal(code, ManifestKeys("api"));
        Assert.Equal(code, new SortedSet<string>(AiEnablement.KnownKeys, StringComparer.Ordinal));
    }

    [Fact]
    public void Claves_Web_EnAppsettingsYEnElManifiesto()
    {
        var code = WebKeys();
        Assert.Contains("ConceptsEnabled", code);
        Assert.Contains("HintsEnabled", code);
        Assert.Equal(code, AppSettingsAiKeys("GeometriaFactory.Web"));
        Assert.Equal(code, ManifestKeys("web"));
    }

    /// <summary>Fusionar no es encender, y la dirección no viaja en el repo: los valores por omisión lo dicen.</summary>
    [Fact]
    public void Manifiesto_ApagadoYSinDireccion()
    {
        var text = File.ReadAllText(Path.Combine(Root.Value, "deploy", "manifiesto-claves-ia.json"));
        using var doc = JsonDocument.Parse(text);
        var pieces = doc.RootElement.GetProperty("piezas");

        foreach (var piece in pieces.EnumerateObject())
        {
            foreach (var key in piece.Value.EnumerateObject())
            {
                var fallback = key.Value.GetProperty("omision").GetString();
                if (key.Name.EndsWith("Enabled", StringComparison.Ordinal) || key.Name.EndsWith("Writer", StringComparison.Ordinal))
                    Assert.Equal("false", fallback);
            }
        }

        Assert.Equal(string.Empty, pieces.GetProperty("api").GetProperty("AI__Endpoint").GetProperty("omision").GetString());
        Assert.Equal(string.Empty, pieces.GetProperty("api").GetProperty("AI__Model").GetProperty("omision").GetString());
        Assert.DoesNotMatch(@"https?://|\d+\.\d+\.\d+\.\d+|:11434", text);
    }
}
