using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Pedido de pistas sobre una advertencia (`ai-layer/01` §2.2). Se arma sólo con un tipo de figura,
/// un campo de la lista permitida, un número y un sentido: **nunca con el texto del alumno**.
/// </summary>
/// <remarks>
/// <para>
/// AL MODELO VIAJAN CUATRO DATOS: <see cref="FigureType"/>, <see cref="Field"/>,
/// <see cref="DeclaredValue"/> y <see cref="Direction"/>.
/// </para>
/// <para>
/// <see cref="DerivedValue"/> NO VIAJA. Lo lleva el pedido porque el filtro posterior lo necesita
/// para rechazar una respuesta que lo nombre (REQ-IA-05); el adaptador nunca lo escribe en un
/// mensaje, y `HintRequest_CarriesOnlyTypeFieldDeclaredDirection` lo comprueba con un doble.
/// </para>
/// </remarks>
public sealed record ObservationHintRequest(
    FigureType FigureType,
    string Field,
    double DeclaredValue,
    DiscrepancyDirection Direction,
    double DerivedValue);
