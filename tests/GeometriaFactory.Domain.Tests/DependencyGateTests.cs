using System.Runtime.CompilerServices;
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
            // LOS TIPOS QUE GENERA EL COMPILADOR NO SON ENTIDADES. Aparecen en el espacio de
            // nombres desde que la etapa `f` escribió expresiones lambda dentro de una entidad, y
            // contarlos haría fallar la prueba por una decisión de sintaxis. **[relevo de la etapa
            // `f`, declarado: lo que la prueba afirma —cinco entidades y ninguna más— no cambia.]**
            .Where(type => !Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute)))
            .Where(type => type.Namespace == "GeometriaFactory.Domain.Entities")
            .Select(type => type.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(new[] { "Account", "Component", "Observation", "Piece", "Work" }, entities);
    }

    [Fact]
    public void TheSixClosedSetsHaveTheDeclaredValueCount()
    {
        Assert.Equal(2, Enum.GetValues<GeometriaFactory.Domain.Values.Role>().Length);
        Assert.Equal(3, Enum.GetValues<GeometriaFactory.Domain.Values.AccountStatus>().Length);
        Assert.Equal(4, Enum.GetValues<GeometriaFactory.Domain.Values.WorkStatus>().Length);
        Assert.Equal(2, Enum.GetValues<GeometriaFactory.Domain.Values.ObservationKind>().Length);

        // Los dos que agrega la etapa `f`, declarados como atributos por
        // `Definicion-Modelo-De-Dominio.md` §2.3 «Tipo» y §2.4 «Papel». **Siete y no seis**: el
        // rectángulo desarrollado es el séptimo discriminante y NO es un tipo de pieza.
        Assert.Equal(7, Enum.GetValues<GeometriaFactory.Domain.Values.FigureType>().Length);
        Assert.Equal(5, Enum.GetValues<GeometriaFactory.Domain.Values.ComponentRole>().Length);
    }

    [Fact]
    public void StageFModelsTheLastThreeEntitiesAndCompletesTheFive()
    {
        // RELEVO DECLARADO, POR TERCERA VEZ Y CON EL MISMO CRITERIO, y **es el último**. Hasta la
        // etapa `b` esta prueba exigía CERO atributos en las cinco entidades; la `c` modeló
        // `Account` y pasó a exigirlo de las otras cuatro; la `e` modeló `Work` y pasó a exigirlo
        // de las tres que interpretan el texto del alumno. La etapa `f` modela esas tres
        // (`Domain BT-06`), de modo que **el conjunto de entidades sin modelar queda vacío** y la
        // prueba deja de tener una mitad que sostener.
        //
        // LA PUERTA HIZO SU TRABAJO Y SE NOTA EN QUE FALLÓ: al escribir los atributos de `Piece`,
        // esta prueba se puso en rojo antes que ninguna otra. Es exactamente lo que su comentario
        // anterior prometía —«la única forma de que el orden del roadmap se rompa haciendo fallar
        // algo»— y la razón por la que se relevó a mano, en lugar de descubrir la etapa cambiada
        // tres commits después.
        Type[] modelled = [typeof(Account), typeof(Work), typeof(Piece), typeof(Component), typeof(Observation)];

        foreach (var entity in modelled)
        {
            Assert.NotEmpty(entity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        }
    }

    [Fact]
    public void TheThreeEntitiesOfTheStudentTextAreWrittenOnlyByTheirOwnFactories()
    {
        // LO QUE REEMPLAZA A LA MITAD QUE EL RELEVO DEJÓ SIN CONTENIDO. Una entidad que el
        // adaptador pudiera mutar desde afuera haría inverificable la garantía G-1: el texto del
        // alumno no se reescribe, y lo que se reconstruye desde él tampoco.
        Type[] interpreted = [typeof(Piece), typeof(Component), typeof(Observation)];

        foreach (var entity in interpreted)
        {
            var settable = entity
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(p => p.SetMethod is { IsPublic: true });

            Assert.Empty(settable);
        }
    }

    [Fact]
    public void TheObservationCarriesDataAndNotARedactedMessage()
    {
        // **[decisión de la etapa `f`, declarada.]** `Definicion-Modelo-De-Dominio.md` §2.5 declara
        // CUATRO atributos y ninguno es una frase: la observación lleva la especie, la posición, el
        // campo y los dos valores, y quien redacta el texto para la persona es la pieza pública.
        // Guardar acá una frase la ataría al idioma y a la redacción del día en que se escribió.
        var properties = typeof(Observation)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Select(p => p.Name);

        Assert.DoesNotContain("Message", properties);
        Assert.DoesNotContain("Text", properties);
    }
}
