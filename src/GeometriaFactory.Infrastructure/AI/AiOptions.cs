namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// La sección <c>AI</c> de la configuración (`ADR-14007`, decisión D-FLAG del expediente 0002;
/// `ADR-14012` para el tiempo, la permanencia del modelo y la precarga).
/// </summary>
/// <remarks>
/// <para>
/// SIN VALOR POR OMISIÓN PARA LA DIRECCIÓN (REQ-IA-15). Si <see cref="Endpoint"/> no llega, no se
/// registra ningún cliente de modelo y el redactor devuelve la plantilla desde el arranque: la
/// capacidad queda **apagada** y el producto se comporta como antes de la capa de IA.
/// </para>
/// <para>
/// La dirección llega por variable de entorno (<c>AI__Endpoint</c>) o por un archivo montado, igual
/// que la clave de firma. El único ejemplo que el repositorio admite es <c>http://localhost:11434</c>.
/// </para>
/// </remarks>
public sealed class AiOptions
{
    public const string SectionName = "AI";

    /// <summary>
    /// Permanencia del modelo en memoria cuando nadie lo usa, si la configuración no la fija o trae una
    /// forma que el servidor no entiende (`ADR-14012`, actuación 010 del expediente 0002).
    /// </summary>
    public const string DefaultKeepAlive = "30m";

    /// <summary>Dirección del servidor de modelos. Vacía por omisión: capacidad apagada.</summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>Nombre del modelo en el servidor. Vacío por omisión.</summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Tope de espera por llamada, en segundos (REQ-IA-10). Admite fracciones para las pruebas.
    /// </summary>
    /// <remarks>
    /// 20 s = p95 en caliente del modelo elegido (12,6 s) más 50 % de margen, redondeado
    /// (evaluación completa del 2026-11-03). El arranque en frío del mismo modelo (13,3 s) también entra.
    /// </remarks>
    public double TimeoutSeconds { get; set; } = 20;

    /// <summary>Tope de observaciones que van al modelo por pedido (REQ-IA-10).</summary>
    public int MaxObservations { get; set; } = 5;

    /// <summary>
    /// Cuánto tiempo el servidor mantiene el modelo cargado después del último pedido (REQ-IA-18). Es la
    /// forma de duración del servidor de modelos: <c>30m</c>, <c>8h</c>, <c>-1</c> (sin límite).
    /// </summary>
    /// <remarks>
    /// VIAJA EN CADA PEDIDO, NO SE CONFIGURA EN EL SERVIDOR: así el producto no depende de cómo esté
    /// configurado un servidor compartido. Un valor que no tiene forma de duración se reemplaza por
    /// <see cref="DefaultKeepAlive"/> y se avisa nombrando la llave, nunca el valor.
    /// </remarks>
    public string KeepAlive { get; set; } = DefaultKeepAlive;

    /// <summary>
    /// Lo lee la Web para mostrar el botón de pistas. <c>false</c> por omisión: con el flag apagado la
    /// pantalla no cambia y no pide nada.
    /// </summary>
    public bool HintsEnabled { get; set; }
}
