using System.Reflection;
using GeometriaFactory.Domain.Entities;
using Xunit;

namespace GeometriaFactory.Domain.Tests;

/// <summary>
/// La puerta `BT-04` de `GeometriaFactory-Domain`: CERO dependencias salientes.
/// </summary>
/// <remarks>
/// No es una regla de negocio —la etapa `a` no introduce ninguna—: es la puerta de arquitectura
/// que el proyecto declara para esta etapa, verificada sobre el ensamblado compilado y no sobre
/// el archivo de proyecto, que es donde la puerta puede fallar sin que nadie lo note (`RI-06`).
/// </remarks>
public sealed class DependencyGateTests
{
    [Fact]
    public void DomainReferencesNoProductAssembly()
    {
        var referenced = typeof(Account).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => name.StartsWith("GeometriaFactory", StringComparison.Ordinal))
            .ToArray();

        Assert.Empty(referenced);
    }

    [Fact]
    public void DomainReferencesNoThirdPartyLibrary()
    {
        string[] platformPrefixes = ["System", "Microsoft", "netstandard", "mscorlib", "WindowsBase"];

        var thirdParty = typeof(Account).Assembly
            .GetReferencedAssemblies()
            .Select(reference => reference.Name ?? string.Empty)
            .Where(name => !platformPrefixes.Any(prefix => name.StartsWith(prefix, StringComparison.Ordinal)))
            .ToArray();

        Assert.Empty(thirdParty);
    }

    [Fact]
    public void TheFiveModelEntitiesExist()
    {
        var entities = typeof(Account).Assembly
            .GetTypes()
            .Where(type => type.Namespace == "GeometriaFactory.Domain.Entities")
            .Select(type => type.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(new[] { "Account", "Component", "Observation", "Piece", "Work" }, entities);
    }

    [Fact]
    public void TheFourClosedSetsHaveTheDeclaredValueCount()
    {
        Assert.Equal(2, Enum.GetValues<GeometriaFactory.Domain.Values.Role>().Length);
        Assert.Equal(3, Enum.GetValues<GeometriaFactory.Domain.Values.AccountStatus>().Length);
        Assert.Equal(4, Enum.GetValues<GeometriaFactory.Domain.Values.WorkStatus>().Length);
        Assert.Equal(2, Enum.GetValues<GeometriaFactory.Domain.Values.ObservationKind>().Length);
    }

    [Fact]
    public void StageEModelsAccountAndWorkAndLeavesTheOtherThreeWithoutAttributes()
    {
        // RELEVO DECLARADO, POR SEGUNDA VEZ Y CON EL MISMO CRITERIO. Hasta la etapa `b` esta
        // prueba exigía CERO atributos en las cinco entidades; la `c` modeló `Account` y la
        // prueba pasó a exigirlo de las otras cuatro. La etapa `e` modela `Work` (`Domain BT-06`
        // para sus atributos y `BT-12` para su máquina de estados), y lo que queda en pie es la
        // misma mitad de siempre: **las tres entidades de la etapa `f` siguen sin modelar**. Es
        // lo que impide adelantar etapa sin que nadie lo note, y es la única forma de que el
        // orden del roadmap se rompa haciendo fallar algo.
        Type[] modelled = [typeof(Account), typeof(Work)];

        foreach (var entity in modelled)
        {
            Assert.NotEmpty(entity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        }

        // Las tres que interpreta el texto del alumno: son de la etapa `f`.
        Type[] notYetModelled = [typeof(Piece), typeof(Component), typeof(Observation)];

        foreach (var entity in notYetModelled)
        {
            Assert.Empty(entity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        }
    }
}
