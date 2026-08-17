using GeometriaFactory.Api.Composition;
using GeometriaFactory.Api.Endpoints;
using GeometriaFactory.Contracts.Service;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Las puertas que cruzan proyectos y que la etapa `a` puede ejercer.
/// </summary>
public sealed class CompositionGateTests
{
    [Fact]
    public void ContractsDoesNotReferenceDomain()
    {
        // `QG-02` de `GeometriaFactory-Contracts`: CERO referencias hacia `GeometriaFactory-Domain`.
        // Es lo que impide que el front conozca las entidades.
        var referenced = typeof(ServiceHealth).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => name.StartsWith("GeometriaFactory", StringComparison.Ordinal))
            .ToArray();

        Assert.Empty(referenced);
    }

    [Fact]
    public void CompositionRootDeclaresTheFourPorts()
    {
        // Mitad izquierda de `QG-10`. La mitad derecha —un adaptador por puerto— se completa
        // entre las etapas `c` y `f`, y esta prueba crece con ella.
        Assert.Equal(4, CompositionRoot.DeclaredPorts.Count);
        Assert.Equal(4, CompositionRoot.DeclaredPorts.Distinct().Count());
        Assert.All(CompositionRoot.DeclaredPorts, port => Assert.True(port.IsInterface));
    }

    [Fact]
    public void StageFConnectsTheFourPortsAndNoneTwice()
    {
        // MITAD DERECHA DE `QG-10`, COMPLETA POR PRIMERA VEZ. La etapa `f` conecta el cuarto
        // puerto —el validador de figuras— y con eso los cuatro puertos declarados en la etapa `a`
        // tienen adaptador. El cuadre deja de tener una mitad incompleta, que es la condición que
        // esta prueba viene arrastrando desde entonces y que crecía con cada etapa.
        Assert.Equal(4, CompositionRoot.ConnectedPorts.Count);
        Assert.Equal(CompositionRoot.DeclaredPorts.Count, CompositionRoot.ConnectedPorts.Count);
        Assert.All(CompositionRoot.ConnectedPorts, pair => Assert.Contains(pair.Key, CompositionRoot.DeclaredPorts));
        Assert.All(CompositionRoot.ConnectedPorts, pair => Assert.True(pair.Key.IsAssignableFrom(pair.Value)));

        // Un adaptador por puerto: ningún adaptador aparece dos veces.
        Assert.Equal(
            CompositionRoot.ConnectedPorts.Count,
            CompositionRoot.ConnectedPorts.Values.Distinct().Count());
    }

    [Fact]
    public void TheHealthEndpointHasASingleRouteSharedWithTheFront()
    {
        Assert.Equal("/salud", HealthEndpoint.Route);
    }
}
