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

    /// <summary>
    /// Rechazo que ADEMÁS transporta el dato que la respuesta tiene que declarar.
    /// </summary>
    /// <remarks>
    /// ENTRA EN LA ETAPA `e` Y POR UNA EXIGENCIA CONCRETA, no por generalidad: la superficie HTTP
    /// declara que el rechazo por estado de un trabajo **declara el estado actual**
    /// (`Definicion-Superficie-HTTP.md` §6, filas de `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` y
    /// `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`). Sin esto, quien expone tendría que volver a leer
    /// el trabajo del almacén para saber qué estado nombrar, que es una segunda lectura y un
    /// segundo lugar donde el dato puede diferir del que decidió el rechazo.
    ///
    /// EL CÓDIGO SIGUE SIENDO LO QUE DECIDE: <see cref="Succeeded"/> es falso y el valor es
    /// **contexto del rechazo**, no un resultado. Quien lo consuma sin mirar el código estaría
    /// leyendo un éxito que no ocurrió.
    /// </remarks>
    public static ApplicationResult<TValue> Rejected(string conditionCode, TValue context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(conditionCode);
        return new ApplicationResult<TValue>(false, context, conditionCode);
    }
}
