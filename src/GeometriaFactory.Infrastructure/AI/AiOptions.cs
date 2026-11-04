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
    /// Tope por omisión de <see cref="TimeoutSeconds"/>, en segundos (G1 de la mesa UX del 2026-11-03).
    /// </summary>
    public const double DefaultTimeoutSeconds = 8;

    /// <summary>
    /// Tope de espera de UN PEDIDO DE PISTAS, en segundos, sumadas todas sus llamadas al modelo
    /// (REQ-IA-10). Admite fracciones para las pruebas.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 8 s Y NO 20: EL SERVICIO DE DATOS CORTA ANTES QUE LA WEB. La Web espera al servicio de datos
    /// 10 s (<c>DataServiceClient.RequestTimeout</c>); con 20 s por llamada la Web se cansaba primero y
    /// la alumna recibía «No pudimos traer pistas» en lugar de la plantilla, que es lo que REQ-IA-07
    /// le garantiza (hallazgo F3 de la mesa UX del 2026-11-03, 6 de 6 intentos sin pista). Con 8 s la
    /// plantilla llega siempre dentro de la ventana de 10 s. Consecuencia declarada: con el modelo en
    /// CPU compartida la pista sale casi siempre de plantilla, hasta que GEO-05 fije modelo y hardware.
    /// </para>
    /// <para>
    /// Supera el valor de 20 s de la evaluación completa del 2026-11-03 (`ADR-14012`, enmendado). Es
    /// configuración y se revierte con <c>AI__TimeoutSeconds</c>, siempre por debajo del tope de la Web.
    /// </para>
    /// </remarks>
    public double TimeoutSeconds { get; set; } = DefaultTimeoutSeconds;

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
