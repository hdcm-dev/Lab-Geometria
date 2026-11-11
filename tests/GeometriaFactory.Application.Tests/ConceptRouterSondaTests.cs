using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Sonda acumulada del número en letras (EXP-0004): cada frase que alguna vez falló, con su esperado. La regla
/// es la función sintáctica del número (ver <see cref="ConceptRouter.SpokenValue"/>); las frases se suman por
/// ronda y ninguna se quita.
/// </summary>
public sealed class ConceptRouterSondaTests
{
    private static readonly Role[] Both = [Role.Student, Role.Administrator];

    private static readonly ConceptCard[] Cards =
    [
        new("area-total-vs-cara", "Área total", "La piel.", ["area total", "area de una cara"], Both, "trabajo-actual", "x"),
    ];

    /// <summary>Conceptos: el número cuenta un sustantivo o es partitivo; no hay medida con valor.</summary>
    [Theory]
    // Ronda 1.
    [InlineData("qué es el volumen de tres cubos iguales")]
    [InlineData("por qué el área de dos cilindros no es el doble")]
    [InlineData("qué es el área de dos triángulos")]
    [InlineData("qué es el volumen, uno de los dos conceptos del cuerpo")]
    [InlineData("por qué la superficie de un cubo son seis cuadrados")]
    // Ronda 2.
    [InlineData("qué relación hay entre el área de 6 cuadrados y el cubo")]
    [InlineData("cómo se calcula el área de las cuatro caras laterales")]
    [InlineData("por qué la fórmula del cilindro usa dos veces el círculo")]
    // Ronda 3.
    [InlineData("hay dos fórmulas para el área del cilindro?")]
    [InlineData("qué significa el dos en la fórmula del área del cilindro")]
    // Ronda 4.
    [InlineData("tengo dudas con las dos fórmulas del cono")]
    // Ronda 5.
    [InlineData("para qué sirven los dos radios en la fórmula")]
    [InlineData("en la fórmula de la esfera aparece cuatro tercios, de dónde sale")]
    public void Concepto_NoPideResolver(string question) =>
        Assert.NotEqual(ConceptRouter.Intents.AsksToSolve, ConceptRouter.Route(question, Cards).Intent);

    /// <summary>Pedidos: el número es valor de una medida.</summary>
    [Theory]
    // Ronda 1.
    [InlineData("con siete de lado, cuál sería el área")]
    [InlineData("tengo siete de radio y cinco de altura, el volumen")]
    [InlineData("un cuadrado tres, su área")]
    [InlineData("si el lado es el número tres el área")]
    // Ronda 2.
    [InlineData("un cuadrado con lado de once, área?")]
    [InlineData("quiero el resultado: esfera, radio veinte, volumen")]
    [InlineData("lado siete, área del cuadrado")]
    [InlineData("medida cuatro el lado, área del cuadrado")]
    [InlineData("hallar la diagonal de un cuadrado de lado uno")]
    // Ronda 3.
    [InlineData("cuánto ocupa una caja de dos por tres por cuatro")]
    [InlineData("hola, tengo un triángulo de catetos tres y cuatro, la hipotenusa?")]
    // Ronda 5.
    [InlineData("cuántos centímetros cuadrados tiene un cuadrado de cinco centímetros de lado")]
    public void Valor_PideResolver(string question) =>
        Assert.Equal(ConceptRouter.Intents.AsksToSolve, ConceptRouter.Route(question, Cards).Intent);
}
