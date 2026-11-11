using GeometriaFactory.Application.Ports;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Las puertas de arquitectura de `GeometriaFactory-Application` en la etapa `a`:
/// una sola dependencia saliente y los cuatro puertos como única frontera.
/// </summary>
public sealed class PortGateTests
{
    [Fact]
    public void ApplicationReferencesOnlyDomainAmongProductAssemblies()
    {
        var referenced = typeof(ISystemClock).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => name.StartsWith("GeometriaFactory", StringComparison.Ordinal))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        // ETAPA `c`: la comprobación pasa a ser **exacta**, como el comentario de la etapa `a`
        // anunció. Ahora `Application` usa tipos de `Domain` —la entidad `Account` y sus
        // valores—, de modo que la referencia está en el ensamblado y la dirección positiva
        // ya es verificable: exactamente uno, y es `GeometriaFactory.Domain`.
        Assert.Equal(new[] { "GeometriaFactory.Domain" }, referenced);
    }

    [Fact]
    public void ApplicationReferencesNeitherWebNorDataAccessFramework()
    {
        string[] forbidden = ["Microsoft.EntityFrameworkCore", "Microsoft.AspNetCore"];

        var offenders = typeof(ISystemClock).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => forbidden.Any(prefix => name.StartsWith(prefix, StringComparison.Ordinal)))
            .ToArray();

        Assert.Empty(offenders);
    }

    /// <remarks>
    /// ALTA DECLARATIVA DEL EXPEDIENTE 0002 (tramo T2): el quinto puerto es
    /// <c>IObservationHintWriter</c>, el redactor de pistas de la capa de IA (`ADR-14006`). Los
    /// cuatro de antes siguen siendo exactamente los mismos.
    /// </remarks>
    [Fact]
    public void TheEightPortsAreDeclared()
    {
        var ports = typeof(ISystemClock).Assembly
            .GetTypes()
            .Where(type => type.IsInterface && type.Namespace == "GeometriaFactory.Application.Ports")
            .Select(type => type.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(
            new[] { "IAccountRepository", "IConceptAnswerWriter", "IConceptCorpus", "IConceptTraceStore", "IFigureValidator", "IObservationHintWriter", "ISystemClock", "IWorkRepository" },
            ports);
    }

    /// <summary>
    /// REQ-IA-03 · El validador sigue sincrónico y sin red (G-6): ningún método de su puerto
    /// devuelve una tarea. La capa de IA va por un puerto aparte (`ADR-14006`).
    /// </summary>
    [Fact]
    public void FigureValidatorPortStaysSynchronous()
    {
        var asynchronous = typeof(IFigureValidator)
            .GetMethods()
            .Where(method => typeof(Task).IsAssignableFrom(method.ReturnType)
                || method.ReturnType == typeof(ValueTask)
                || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>)))
            .Select(method => method.Name)
            .ToArray();

        Assert.Empty(asynchronous);
    }

    /// <summary>REQ-IA-01/02 · Application no referencia el SDK de IA ni un SDK de modelos.</summary>
    [Fact]
    public void ApplicationReferencesNoAiSdk()
    {
        string[] forbidden = ["Microsoft.Extensions.AI", "OllamaSharp", "OpenAI"];

        var offenders = typeof(ISystemClock).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => forbidden.Any(prefix => name.StartsWith(prefix, StringComparison.Ordinal)))
            .ToArray();

        Assert.Empty(offenders);
    }
}
