namespace GeometriaFactory.Application;

/// <summary>
/// Resultado tipado de un caso de uso (`Application ADR-06` §2).
/// </summary>
/// <remarks>
/// Toda condición prevista de esta capa viaja como resultado con su código estable, NUNCA como
/// excepción; la excepción queda reservada al defecto de programación del consumidor. El
/// resultado no transporta texto de presentación: la composición del mensaje es de quien expone
/// y la traducción a respuesta de protocolo es de `GeometriaFactory.Api`.
/// </remarks>
public readonly record struct ApplicationResult
{
    private ApplicationResult(bool succeeded, string? conditionCode)
    {
        Succeeded = succeeded;
        ConditionCode = conditionCode;
    }

    public bool Succeeded { get; }

    public string? ConditionCode { get; }

    public static ApplicationResult Applied() => new(true, null);

    public static ApplicationResult Rejected(string conditionCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(conditionCode);
        return new ApplicationResult(false, conditionCode);
    }
}

/// <summary>Resultado tipado que además devuelve el dato que el consumidor necesita.</summary>
public readonly record struct ApplicationResult<TValue>
{
    private ApplicationResult(bool succeeded, TValue? value, string? conditionCode)
    {
        Succeeded = succeeded;
        Value = value;
        ConditionCode = conditionCode;
    }

    public bool Succeeded { get; }

    public TValue? Value { get; }

    public string? ConditionCode { get; }

    public static ApplicationResult<TValue> Applied(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new ApplicationResult<TValue>(true, value, null);
    }

    public static ApplicationResult<TValue> Rejected(string conditionCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(conditionCode);
        return new ApplicationResult<TValue>(false, default, conditionCode);
    }
}
