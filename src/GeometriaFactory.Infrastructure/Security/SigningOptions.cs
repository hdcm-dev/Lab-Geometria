namespace GeometriaFactory.Infrastructure.Security;

/// <summary>
/// Lo que el acceso firmado necesita para existir: la clave y la vigencia.
/// </summary>
/// <remarks>
/// LA CLAVE SE RECIBE Y NO SE BUSCA (`Infrastructure ADR-04` §2 punto 3): llega por variable de
/// entorno o por archivo montado, **fuera del repositorio de código y fuera de la imagen**
/// (intake §17.3.P.5, §17.5.P.5). Si no llega, no se emite nada y no se genera ninguna al vuelo.
///
/// LA VIGENCIA ES CORTA Y SIN ACCESO DE REFRESCO. El criterio de `ADR-04` §2 punto 5 es que
/// **caduque dentro de la sesión de trabajo de una clase** y que la renovación sea reingreso. El
/// número concreto sigue siendo punto abierto: **ocho horas es la propuesta de la etapa `c`**,
/// que es lo que dura una jornada de laboratorio sin obligar a reingresar en el medio.
/// </remarks>
public sealed class SigningOptions
{
    /// <summary>Nombre de la sección de configuración.</summary>
    public const string SectionName = "AccessToken";

    /// <summary>Clave simétrica de firma. Vacía significa ausente, y ausente detiene la emisión.</summary>
    public string SigningKey { get; set; } = string.Empty;

    /// <summary>Vigencia del acceso emitido, en minutos.</summary>
    public int LifetimeInMinutes { get; set; } = 480;

    /// <summary>Emisor declarado en el acceso.</summary>
    public string Issuer { get; set; } = "GeometriaFactory";

    /// <summary>Audiencia declarada en el acceso.</summary>
    public string Audience { get; set; } = "GeometriaFactory";
}
