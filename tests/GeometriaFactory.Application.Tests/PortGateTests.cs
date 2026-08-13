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

    [Fact]
    public void TheFourPortsAreDeclared()
    {
        var ports = typeof(ISystemClock).Assembly
            .GetTypes()
            .Where(type => type.IsInterface && type.Namespace == "GeometriaFactory.Application.Ports")
            .Select(type => type.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(
            new[] { "IAccountRepository", "IFigureValidator", "ISystemClock", "IWorkRepository" },
            ports);
    }
}
