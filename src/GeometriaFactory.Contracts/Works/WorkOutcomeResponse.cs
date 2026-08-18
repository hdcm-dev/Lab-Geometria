namespace GeometriaFactory.Contracts.Works;

/// <summary>
/// El resultado del desenlace: el estado que el trabajo alcanzó (`Contracts CU-08007` §4, paso 4).
/// </summary>
/// <remarks>
/// TRAE EL ESTADO ALCANZADO Y NO EL DESENLACE PEDIDO, y es la diferencia que le da valor: quien
/// pidió ya sabe qué pidió. Lo que no sabe —y lo que el listado va a mostrar— es en qué estado
/// quedó el trabajo, y eso lo decide el dominio.
///
/// EL ESTADO VIAJA POR SU NOMBRE, con el mismo criterio que el resto del contrato de trabajos.
///
/// NO DEVUELVE EL COMENTARIO. Quien lo escribió lo tiene, y el único tipo del ensamblado que lo
/// transporta hacia el alumno es <see cref="WorkDetailResponse"/> (`CU-08007` §4, paso 6): dos
/// lugares por donde volviera abrirían dos versiones del mismo texto.
/// </remarks>
/// <param name="WorkId">Identidad del trabajo resuelto.</param>
/// <param name="Status">Estado alcanzado: `Approved` si se aprobó, `Rejected` si se rechazó.</param>
/// <param name="ResolvedAt">Momento del desenlace, en tiempo universal coordinado.</param>
public sealed record WorkOutcomeResponse(
    Guid WorkId,
    string Status,
    DateTimeOffset ResolvedAt);
