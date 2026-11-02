namespace GeometriaFactory.Application.Ports;

/// <summary>Sentido de la diferencia entre el valor declarado y el derivado (`ai-layer/01` §2.2).</summary>
public enum DiscrepancyDirection
{
    /// <summary>El declarado es mayor que el que corresponde.</summary>
    Above = 1,

    /// <summary>El declarado es menor que el que corresponde.</summary>
    Below = 2,
}
