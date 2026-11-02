namespace GeometriaFactory.Application.Ports;

/// <summary>Los tres resultados del redactor de pistas (`ai-layer/01` §3.3).</summary>
public enum HintResultKind
{
    /// <summary>Hubo respuesta con los tres niveles y pasó el filtro.</summary>
    Ok = 1,

    /// <summary>Sin servidor, sin respuesta, fuera de tiempo o con el cupo ocupado.</summary>
    Degraded = 2,

    /// <summary>Hubo respuesta y el filtro la rechazó.</summary>
    Rejected = 3,
}
