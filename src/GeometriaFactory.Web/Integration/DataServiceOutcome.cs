using GeometriaFactory.Contracts.Errors;

namespace GeometriaFactory.Web.Integration;

/// <summary>
/// Lo que una llamada al servicio de datos devuelve: el resultado, o el error del contrato.
/// </summary>
/// <remarks>
/// LA FALLA NO VIAJA COMO EXCEPCIÓN HASTA LA SUPERFICIE. `Web CU-10` exige estado degradado
/// explícito y **nunca una excepción sin manejar**, y `Contracts CU-06` FA-02 declara que cuando
/// el servicio no responde **el tipo de error lo produce la propia pieza pública**. Esta forma es
/// lo que hace que las dos cosas ocurran siempre, en lugar de depender de que cada superficie se
/// acuerde de envolver su llamada.
///
/// EL TEXTO DEL ERROR DE TRANSPORTE NO SALE DE LA EXCEPCIÓN, y es la razón de fondo: el mensaje
/// de una excepción de red **lleva la dirección del servicio interno**, y RA-03 prohíbe que eso
/// llegue al navegador. Se reemplaza por el texto de la maqueta aprobada.
/// </remarks>
public sealed record DataServiceOutcome<TValue>
{
    private DataServiceOutcome(TValue? value, ErrorResponse? error)
    {
        Value = value;
        Error = error;
    }

    /// <summary>Si la llamada se resolvió.</summary>
    public bool Succeeded => Error is null;

    /// <summary>El resultado, cuando la llamada se resolvió.</summary>
    public TValue? Value { get; }

    /// <summary>El error del contrato, cuando no se resolvió.</summary>
    public ErrorResponse? Error { get; }

    public static DataServiceOutcome<TValue> Resolved(TValue value) => new(value, null);

    public static DataServiceOutcome<TValue> Failed(ErrorResponse error) => new(default, error);
}
