using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado.Semilla;

namespace GeometriaFactory.Samples.Application.Avanzado.Actos;

/// <summary>
/// `CU-04007` — La revisión de la comisión. **`RN-04011`: el administrador no ve los borradores.**
/// </summary>
/// <remarks>
/// LA EXCLUSIÓN DEL BORRADOR NO ES UN FILTRO DE PRESENTACIÓN: es una regla, y por eso el detalle
/// de un trabajo en borrador **también se rechaza**. Un listado que lo escondiera y un detalle que
/// lo mostrara dejarían la regla en la mitad.
/// </remarks>
internal static class ActoRevisarLaComision
{
    internal static async Task EjecutarAsync(
        Bitacora b, ReviewCommissionWorksUseCase caso, ComisionDeEjemplo comision)
    {
        var listado = await caso.ListAsync(Role.Administrator, comision.Alumna.Id);
        var entradas = listado.Value!;
        var borradores = entradas.Count(e => e.Status == WorkStatus.Draft);
        b.Escribir(
            $"[2] Listado de la comision: {entradas.Count} trabajos "
            + $"| borradores visibles: {borradores} (RN-04011)");

        var detalle = await caso.DetailAsync(Role.Administrator, comision.EnBorrador.Id);
        b.Escribir($"[2] Detalle de un trabajo en Borrador pedido por el administrador: {detalle.ConditionCode}");

        var porAlumno = await caso.ListAsync(Role.Student, null);
        b.Escribir($"[2] Listado de la comision pedido por un alumno: rechazado {porAlumno.ConditionCode}");
    }
}
