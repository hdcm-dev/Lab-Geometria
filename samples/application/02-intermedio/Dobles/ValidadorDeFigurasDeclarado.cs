using GeometriaFactory.Application.Ports;
using GeometriaFactory.Samples.Application.Intermedio.Escenarios;

namespace GeometriaFactory.Samples.Application.Intermedio.Dobles;

/// <summary>
/// El doble del puerto de validación: **devuelve el resultado declarado** para cada texto de
/// escenario, y no interpreta nada.
/// </summary>
/// <remarks>
/// ES LA FRONTERA QUE ESTE SAMPLE ENSEÑA. La capa de aplicación **no interpreta el texto**: se lo
/// pide al puerto y adopta lo que vuelve. Poner acá el intérprete real convertiría al sample en
/// una prueba de la infraestructura y taparía justamente lo que hay que ver — que la capa **no
/// sabe** cómo se interpreta, sólo cuándo pedirlo y qué hacer con el resultado.
///
/// Los resultados salen de `Escenarios/ResultadosDeclarados.cs`, compuestos a mano desde los ocho
/// textos del `PRODUCT-INTAKE` §20.
/// </remarks>
internal sealed class ValidadorDeFigurasDeclarado : IFigureValidator
{
    private readonly Dictionary<string, FigureInterpretation> _porTexto = [];

    internal void Declarar(string texto, FigureInterpretation resultado) => _porTexto[texto] = resultado;

    public FigureInterpretation Interpret(string originalJson) =>
        _porTexto.TryGetValue(originalJson, out var declarado)
            ? declarado
            // Un texto que el sample no declaró es un defecto del sample y no del sistema: se
            // devuelve un resultado vacío en vez de lanzar, para que la diferencia se vea en la
            // comparación contra el snapshot y no como una excepción sin contexto.
            : FigureInterpretation.From(0, [], [], null);
}
