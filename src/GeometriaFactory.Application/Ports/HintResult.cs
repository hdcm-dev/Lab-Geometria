namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Resultado del redactor de pistas. **Siempre trae pistas**: las del modelo si es
/// <see cref="HintResultKind.Ok"/>, la plantilla en los otros dos casos (REQ-IA-07).
/// </summary>
/// <remarks>
/// EL MOTIVO ES UNA CATEGORÍA Y NUNCA EL TEXTO RECHAZADO: va a la traza, y una respuesta rechazada
/// por traer el valor correcto no se copia a ningún lado.
/// </remarks>
public sealed class HintResult
{
    private HintResult(HintResultKind kind, Hints hints, string? reason)
    {
        Kind = kind;
        Hints = hints;
        Reason = reason;
    }

    public HintResultKind Kind { get; }

    public Hints Hints { get; }

    public string? Reason { get; }

    public static HintResult Ok(Hints hints)
    {
        ArgumentNullException.ThrowIfNull(hints);
        return new(HintResultKind.Ok, hints, null);
    }

    public static HintResult Degraded(Hints template, string reason)
    {
        ArgumentNullException.ThrowIfNull(template);
        return new(HintResultKind.Degraded, template, reason);
    }

    public static HintResult Rejected(Hints template, string reason)
    {
        ArgumentNullException.ThrowIfNull(template);
        return new(HintResultKind.Rejected, template, reason);
    }
}
