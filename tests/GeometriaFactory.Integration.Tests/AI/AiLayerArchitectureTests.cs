using System.Text.RegularExpressions;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Service;
using GeometriaFactory.Infrastructure.AI;
using GeometriaFactory.Infrastructure.Figures;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Compuertas de arquitectura de la capa de IA (REQ-IA-02, REQ-IA-03, REQ-IA-15; `ADR-14008`).
/// </summary>
public sealed class AiLayerArchitectureTests : IDisposable
{
    private static readonly Regex AiSdkUse = new(
        @"\b(Microsoft\.Extensions\.AI|OllamaSharp)\b",
        RegexOptions.CultureInvariant,
        TimeSpan.FromSeconds(1));

    /// <summary>Las dos carpetas donde el SDK de IA puede aparecer, relativas a <c>src/</c>.</summary>
    private static readonly string[] AllowedFolders =
    [
        "GeometriaFactory.Infrastructure/AI/",
        "GeometriaFactory.Api/Composition/",
    ];

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();

    public void Dispose() => DataServiceHarness.DiscardStore(_storePath);

    /// <summary>
    /// REQ-IA-02 · El SDK de IA sólo vive en <c>Infrastructure/AI</c> y en la composición de la Api.
    /// Se lee el código fuente de <c>src/</c> entero —<c>.cs</c> y <c>.razor</c>—, porque Persistence
    /// y la carpeta de IA comparten ensamblado y una prueba por referencias no las separa.
    /// </summary>
    /// <remarks>
    /// MUTADA A MANO ANTES DE CONFIAR EN ELLA: con un <c>using Microsoft.Extensions.AI;</c> agregado a
    /// un archivo de <c>Infrastructure/Persistence</c> da rojo y nombra el archivo (constancia del
    /// tramo T4, expediente 0002).
    /// </remarks>
    [Fact]
    public void AiSdkOnlyInInfrastructureAiAndComposition()
    {
        var src = Path.Combine(RepositoryRoot(), "src");

        var offenders = Directory
            .EnumerateFiles(src, "*.*", SearchOption.AllDirectories)
            .Where(path => path.EndsWith(".cs", StringComparison.Ordinal) || path.EndsWith(".razor", StringComparison.Ordinal))
            .Select(path => Path.GetRelativePath(src, path).Replace('\\', '/'))
            .Where(relative => !relative.Contains("/obj/", StringComparison.Ordinal) && !relative.Contains("/bin/", StringComparison.Ordinal))
            .Where(relative => !AllowedFolders.Any(folder => relative.StartsWith(folder, StringComparison.Ordinal)))
            .Where(relative => AiSdkUse.IsMatch(File.ReadAllText(Path.Combine(src, relative))))
            .ToArray();

        Assert.True(
            offenders.Length == 0,
            "El SDK de IA aparece fuera de Infrastructure/AI y de Api/Composition: " + string.Join(", ", offenders));
    }

    [Fact]
    public void TheProductAssembliesOutsideInfrastructureAndApiDoNotReferenceTheAiSdk()
    {
        var assemblies = new[]
        {
            typeof(IObservationHintWriter).Assembly,
            typeof(ServiceHealth).Assembly,
            typeof(GeometriaFactory.Web.Services.SessionState).Assembly,
        };

        var offenders = assemblies
            .SelectMany(assembly => assembly.GetReferencedAssemblies().Select(reference => $"{assembly.GetName().Name} → {reference.Name}"))
            .Where(edge => edge.Contains("Microsoft.Extensions.AI", StringComparison.Ordinal) || edge.Contains("OllamaSharp", StringComparison.Ordinal))
            .ToArray();

        Assert.Empty(offenders);
    }

    /// <summary>REQ-IA-03, la mitad del adaptador: el validador no depende de nada de la capa de IA.</summary>
    [Fact]
    public void TheLocalValidatorDoesNotDependOnTheAiLayer()
    {
        var flags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static
            | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
        var validator = typeof(LocalFigureValidator);

        var touched = validator.GetFields(flags).Select(f => f.FieldType)
            .Concat(validator.GetConstructors(flags).SelectMany(c => c.GetParameters()).Select(p => p.ParameterType))
            .Where(type => type.Namespace is { } ns
                && (ns.StartsWith("GeometriaFactory.Infrastructure.AI", StringComparison.Ordinal)
                    || ns.StartsWith("Microsoft.Extensions.AI", StringComparison.Ordinal)))
            .ToArray();

        Assert.Empty(touched);
    }

    /// <summary>
    /// REQ-IA-15 · Sin dirección del servidor, el servicio arranca, no hay cliente de modelo y el
    /// puerto devuelve degradado con la plantilla. Es el flag apagado por defecto que el despliegue exige.
    /// </summary>
    [Fact]
    public async Task NoModelEndpoint_StartsDegraded()
    {
        using var harness = new DataServiceHarness(_storePath);
        using var scope = harness.Services.CreateScope();

        Assert.Null(scope.ServiceProvider.GetService<IChatClient>());
        Assert.False(harness.Services.GetRequiredService<AiOptions>().HintsEnabled);

        var result = await scope.ServiceProvider.GetRequiredService<IObservationHintWriter>().WriteAsync(
            new ObservationHintRequest(Domain.Values.FigureType.Cube, "Area", 36, DiscrepancyDirection.Below, 54));

        Assert.Equal(HintResultKind.Degraded, result.Kind);
        Assert.Equal("sin-servidor", result.Reason);
        Assert.Equal(HintOrigin.Template, result.Hints.Origin);
    }

    [Theory]
    [InlineData("no-es-una-direccion", "un-modelo")]
    [InlineData("ftp://localhost/", "un-modelo")]
    [InlineData("http://localhost:11434", "")]
    public void AnInvalidEndpointNeverBreaksTheStartAndRegistersNoClient(string endpoint, string model)
    {
        using var harness = new DataServiceHarness(_storePath, new Dictionary<string, string?>
        {
            ["AI:Endpoint"] = endpoint,
            ["AI:Model"] = model,
        });

        Assert.Null(harness.Services.GetService<IChatClient>());
    }

    [Fact]
    public void AValidEndpointRegistersTheClientWithoutCallingIt()
    {
        using var harness = new DataServiceHarness(_storePath, new Dictionary<string, string?>
        {
            ["AI:Endpoint"] = "http://localhost:11434",
            ["AI:Model"] = "modelo-de-ejemplo",
        });

        Assert.NotNull(harness.Services.GetService<IChatClient>());
    }

    /// <summary>REQ-IA-10 · El cupo es el mismo objeto desde dos ámbitos: limita por instancia.</summary>
    [Fact]
    public void TheGateIsTheSameInstanceFromTwoScopes()
    {
        using var harness = new DataServiceHarness(_storePath);
        using var first = harness.Services.CreateScope();
        using var second = harness.Services.CreateScope();

        Assert.Same(
            first.ServiceProvider.GetRequiredService<HintConcurrencyGate>(),
            second.ServiceProvider.GetRequiredService<HintConcurrencyGate>());
    }

    internal static string RepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "GeometriaFactory.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("No se encontró la raíz del repositorio.");
    }
}
