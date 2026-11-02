namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Las tres pistas graduadas: dónde mirar, qué concepto interviene y cómo comprobarlo sin números
/// (`ai-layer/01` §5.2).
/// </summary>
public sealed record Hints(string Level1, string Level2, string Level3, HintOrigin Origin);
