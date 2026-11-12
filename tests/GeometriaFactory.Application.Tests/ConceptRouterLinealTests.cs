using System.Reflection;
using System.Text.RegularExpressions;
using GeometriaFactory.Application.Concepts;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Juicio 2026-11-12, R1 (EXP-0004, act. 005): «¿Qué es un ortoedro?» devolvió 500 porque una expresión del
/// enrutador venció su tope de 250 ms de reloj bajo carga (<see cref="RegexMatchTimeoutException"/>). El
/// enrutador no depende del reloj: motor lineal, sin tope, y abstención si algo igual vence.
/// </summary>
public sealed class ConceptRouterLinealTests
{
    private const string FraseDelJuez = "¿Qué es un ortoedro?";

    private static readonly Role[] Both = [Role.Student, Role.Administrator];

    private static readonly ConceptCard[] Cards =
    [
        new("ortoedro", "Ortoedro", "Un cuerpo.", ["ortoedro", "paralelepipedo", "caja"], Both, "ejemplo-formato", "x"),
        new("area-total-vs-cara", "Área total", "La piel.", ["area total", "area de una cara"], Both, "trabajo-actual", "x"),
    ];

    public static TheoryData<string> Expresiones()
    {
        var data = new TheoryData<string>();
        foreach (var field in typeof(ConceptRouter).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                     .Where(f => f.FieldType == typeof(Regex)))
        {
            data.Add(field.Name);
        }

        return data;
    }

    /// <summary>Cada expresión es lineal (sin retroceso) y no tiene tope de reloj.</summary>
    [Theory]
    [MemberData(nameof(Expresiones))]
    public void CadaExpresion_EsLinealYSinReloj(string name)
    {
        var regex = (Regex)typeof(ConceptRouter).GetField(name, BindingFlags.NonPublic | BindingFlags.Static)!
            .GetValue(null)!;

        Assert.True(regex.Options.HasFlag(RegexOptions.NonBacktracking), $"{name} usa el motor con retroceso");
        Assert.Equal(Regex.InfiniteMatchTimeout, regex.MatchTimeout);
    }

    /// <summary>La reproducción: la frase del juez bajo contención (256 hilos) nunca lanza y siempre da la ficha.</summary>
    [Fact]
    public void FraseDelJuez_BajoCarga_NuncaLanzaYDaLaFicha()
    {
        var fallas = 0;
        var intents = new System.Collections.Concurrent.ConcurrentBag<string>();

        Parallel.For(0, 4000, new ParallelOptions { MaxDegreeOfParallelism = 256 }, _ =>
        {
            try
            {
                intents.Add(ConceptRouter.Route(FraseDelJuez, Cards).Intent);
            }
            catch (RegexMatchTimeoutException)
            {
                Interlocked.Increment(ref fallas);
            }
        });

        Assert.Equal(0, fallas);
        Assert.All(intents, intent => Assert.Equal("ortoedro", intent));
    }

    /// <summary>La mirada atrás que se quitó («se calcula» no es pedido) conserva su efecto.</summary>
    [Theory]
    [InlineData("cómo se calcula el área total", "area-total-vs-cara")]
    [InlineData("calculá el área total", ConceptRouter.Intents.AsksToSolve)]
    [InlineData("cómo se calcula el área total? calculalo vos", ConceptRouter.Intents.AsksToSolve)]
    public void SeCalcula_NoEsPedido_Calcula_Si(string question, string intent) =>
        Assert.Equal(intent, ConceptRouter.Route(question, Cards).Intent);
}
