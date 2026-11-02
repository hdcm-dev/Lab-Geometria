namespace GeometriaFactory.Infrastructure.AI;

/// <summary>
/// La sección <c>AI</c> de la configuración (`ADR-14007`, decisión D-FLAG del expediente 0002).
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

    /// <summary>Dirección del servidor de modelos. Vacía por omisión: capacidad apagada.</summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>Nombre del modelo en el servidor. Vacío por omisión.</summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>Tope de espera por llamada, en segundos (REQ-IA-10). Admite fracciones para las pruebas.</summary>
    public double TimeoutSeconds { get; set; } = 20;

    /// <summary>Tope de observaciones que van al modelo por pedido (REQ-IA-10).</summary>
    public int MaxObservations { get; set; } = 5;

    /// <summary>
    /// Lo lee la Web para mostrar el botón de pistas. <c>false</c> por omisión: con el flag apagado la
    /// pantalla no cambia y no pide nada.
    /// </summary>
    public bool HintsEnabled { get; set; }
}
