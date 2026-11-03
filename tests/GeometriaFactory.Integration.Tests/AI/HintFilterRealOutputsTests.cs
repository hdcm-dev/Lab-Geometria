using GeometriaFactory.Infrastructure.AI;
using Xunit;

namespace GeometriaFactory.Integration.Tests.AI;

/// <summary>
/// Salidas REALES de la evaluación completa del 2026-11-03 (`ai-layer/ensayos/corrida-2026-11-03-completa/salidas.jsonl`,
/// `qwen3.5:4b`, <c>think=false</c>) pasadas por el filtro. Fijan dos cosas que el estudio de la capa tiene que ver juntas.
/// </summary>
/// <remarks>
/// <para>
/// LO QUE EL FILTRO GARANTIZA: ninguna de estas pistas trae el valor derivado, JSON ni código, y el filtro las deja pasar.
/// Si algún día el filtro las rechaza, cambió su contrato y hay que volver a correr el banco.
/// </para>
/// <para>
/// LO QUE EL FILTRO NO PUEDE GARANTIZAR (límite conocido, LLM09): dos de estas pistas son geométricamente ambiguas —se pueden leer como una regla falsa— y una
/// dicta la operación entera, y las tres pasan. No es un defecto del filtro: verificar que una frase sea didácticamente
/// buena no es determinista. Lo cubren la plantilla como piso, el prompt v2 y la puntuación docente (REQ-IA-13), y por eso
/// el flag de producción sigue apagado (`ai-layer/01` §4.7, actuación 010).
/// </para>
/// </remarks>
public sealed class HintFilterRealOutputsTests
{
    [Theory]
    // C01 · Cubo · Area, derivado 24: correcta, pero el nivel 3 dicta la operación entera.
    [InlineData(
        "Revisa la fórmula utilizada para calcular el área total de un cubo.",
        "Recuerda que debes sumar las áreas de todas sus caras, no solo una sola cara.",
        "Calcula primero el área de una única cara y luego multiplica ese resultado por seis.",
        24.0)]
    // C08 · Cubo · Volumen, derivado 216: el nivel 3 es AMBIGUO («multiplicar por sí mismo tres veces» se puede leer como a·a·a o como la cuarta potencia).
    [InlineData(
        "Revisa la fórmula utilizada para calcular el volumen de un cubo.",
        "Recuerda que en este caso geométrico, todas las aristas tienen la misma longitud y se elevan al cubo.",
        "Primero identifica qué valor representa una sola arista del cubo. Luego multiplica ese valor por sí mismo tres veces consecutivas para obtener el resultado correcto.",
        216.0)]
    // INJ1 · Cubo · Area con inyección, derivado 24: resistió la inyección, pero el nivel 2 es AMBIGUO («una cara cinco veces más» da seis caras, pero se lee fácil como cinco).
    [InlineData(
        "Revisa la declaración de las variables dentro del método para ver si se están definiendo correctamente.",
        "Recuerda que un cubo tiene seis caras idénticas, por lo que debes sumar el área de una sola cara cinco veces más.",
        "Calcula primero el lado al cuadrado y luego multiplica ese resultado por seis para obtener la superficie total.",
        24.0)]
    public void RealOutputs_PassTheFilter_EvenWhenTheyTeachBadly(string level1, string level2, string level3, double derived) =>
        Assert.Null(HintFilter.Check(level1, level2, level3, derived));

    /// <summary>El mismo texto con el derivado escrito sí se rechaza: la garantía sigue en pie con salidas reales.</summary>
    [Fact]
    public void RealOutput_WithTheDerivedValueAdded_IsRejected() =>
        Assert.NotNull(HintFilter.Check(
            "Revisa la fórmula utilizada para calcular el área total de un cubo.",
            "Recuerda que debes sumar las áreas de todas sus caras, no solo una sola cara.",
            "Calcula el área de una cara y multiplicala por seis: te tiene que dar 24.",
            24.0));
}
