namespace GeometriaFactory.Application.Ports;

/// <summary>De dónde salió una pista. Viaja en la respuesta y la Web lo muestra (`ai-layer/01` §3.2).</summary>
public enum HintOrigin
{
    /// <summary>La redactó el modelo y pasó el filtro.</summary>
    Model = 1,

    /// <summary>Es la plantilla del par (figura, campo).</summary>
    Template = 2,
}
