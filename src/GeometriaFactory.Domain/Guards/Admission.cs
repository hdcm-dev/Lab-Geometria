namespace GeometriaFactory.Domain.Guards;

/// <summary>
/// Resultado de la evaluación de admisibilidad de una cuenta (`Domain CU-04`).
/// </summary>
/// <remarks>
/// La evaluación SIEMPRE devuelve un resultado y nunca cambia la cuenta: es la guarda única por
/// la que pasan INV-06 e INV-09 (`Domain ADR-05`). Admisible lleva CERO motivos; no admisible
/// lleva EXACTAMENTE UNO.
/// </remarks>
public readonly record struct Admission
{
    private Admission(bool isAdmissible, string? reason)
    {
        IsAdmissible = isAdmissible;
        Reason = reason;
    }

    /// <summary>Si la cuenta admite acceso al laboratorio.</summary>
    public bool IsAdmissible { get; }

    /// <summary>El único motivo de la negativa, o nulo si admite.</summary>
    public string? Reason { get; }

    public static Admission Admissible() => new(true, null);

    public static Admission NotAdmissible(string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        return new Admission(false, reason);
    }
}
