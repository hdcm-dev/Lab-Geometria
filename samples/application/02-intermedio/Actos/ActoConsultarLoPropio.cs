using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Actos;

/// <summary>
/// `CU-04006` — El listado propio y el detalle. **La diferencia entre los dos es el punto**: el
/// detalle trae las piezas **con sus componentes** y el listado no las trae en absoluto.
/// </summary>
/// <remarks>
/// EL LISTADO NO ARRASTRA EL CONTENIDO DE CADA TRABAJO, y es una decisión de contrato y no una
/// omisión: un panel que trae las piezas de todos los trabajos para mostrar una tabla de nombres
/// paga el detalle entero por cada fila.
/// </remarks>
internal static class ActoConsultarLoPropio
{
    internal static async Task DetalleContraListadoAsync(
        Bitacora bitacora, ConsultOwnWorksUseCase caso, Guid alumna, Guid trabajo)
    {
        var detalle = await bitacora.InvocarAsync(() => caso.DetailAsync(alumna, trabajo));
        var piezas = detalle.Value!.Pieces;
        var conComponentes = piezas.Count;

        var listado = await bitacora.InvocarAsync(() => caso.ListAsync(alumna));
        // El tipo del listado NO TIENE campo de piezas: la ausencia es del contrato y se declara
        // contándola como cero, que es lo que un consumidor observa.
        _ = listado.Value!;

        bitacora.Escribir(
            $"[E-7] Detalle: {conComponentes} piezas con componentes "
            + $"| Listado: {conComponentes} piezas sin componentes");
    }

    internal static async Task ListadoPropioAsync(
        Bitacora bitacora, ConsultOwnWorksUseCase caso, Guid alumna)
    {
        var r = await bitacora.InvocarAsync(() => caso.ListAsync(alumna));
        var entradas = r.Value!;
        int Contar(WorkStatus e) => entradas.Count(x => x.Status == e);

        bitacora.Escribir(
            $"[Consulta] Listado propio: {entradas.Count} trabajos "
            + $"| Pendiente={Contar(WorkStatus.Submitted)} Borrador={Contar(WorkStatus.Draft)} "
            + $"Aprobado={Contar(WorkStatus.Approved)} Rechazado={Contar(WorkStatus.Rejected)}");
    }
}
