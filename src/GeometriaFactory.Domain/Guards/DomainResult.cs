namespace GeometriaFactory.Domain.Guards;

/// <summary>
/// Resultado tipado de una operación del dominio que puede rechazar (`Domain ADR-02` §2).
/// </summary>
/// <remarks>
/// Las reglas de negocio NO viajan como excepción. La excepción queda reservada al defecto de
/// programación del consumidor —un valor ausente donde el contrato exige uno—, y por eso las
/// guardas de argumento nulo siguen lanzando.
///
/// El resultado no transporta texto de presentación: lleva el código de condición y nada más
/// (`Application ADR-06` §2, misma regla de forma).
/// </remarks>
public readonly record struct DomainResult
{
    private DomainResult(bool succeeded, string? conditionCode)
    {
        Succeeded = succeeded;
        ConditionCode = conditionCode;
    }

    /// <summary>Si el efecto se aplicó.</summary>
    public bool Succeeded { get; }

    /// <summary>La condición que lo impidió, o nulo si se aplicó.</summary>
    public string? ConditionCode { get; }

    public static DomainResult Applied() => new(true, null);

    public static DomainResult Rejected(string conditionCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(conditionCode);
        return new DomainResult(false, conditionCode);
    }
}

/// <summary>Resultado tipado que además devuelve la entidad constituida.</summary>
public readonly record struct DomainResult<TValue>
{
    private DomainResult(bool succeeded, TValue? value, string? conditionCode)
    {
        Succeeded = succeeded;
        Value = value;
        ConditionCode = conditionCode;
    }

    public bool Succeeded { get; }

    public TValue? Value { get; }

    public string? ConditionCode { get; }

    public static DomainResult<TValue> Applied(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new DomainResult<TValue>(true, value, null);
    }

    public static DomainResult<TValue> Rejected(string conditionCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(conditionCode);
        return new DomainResult<TValue>(false, default, conditionCode);
    }
}
