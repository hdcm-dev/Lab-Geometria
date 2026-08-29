using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Actos;

/// <summary>
/// `CU-04004` — La carga del trabajo, que en esta capa **interpreta y envía en el mismo acto**, y
/// la reedición, que sólo procede en `Borrador`.
/// </summary>
internal static class ActoCargarYReeditar
{
    internal static async Task<WorkOutcomeSnapshot> CargarAsync(
        Bitacora bitacora, LoadAndEditOwnWorkUseCase caso, Guid alumna, string escenario, string texto)
    {
        var r = await bitacora.InvocarAsync(() => caso.LoadAsync(
            alumna, $"Trabajo {escenario}", "2026-08-29", null, texto));

        var s = r.Value!;
        bitacora.ContarEscenario(s.Status);
        return s;
    }

    internal static async Task ReeditarFueraDeBorradorAsync(
        Bitacora bitacora, LoadAndEditOwnWorkUseCase caso, Guid alumna, Guid trabajo, string textoOriginal,
        Func<Guid, Task<string>> leerTextoGuardado)
    {
        var r = await bitacora.InvocarAsync(() => caso.EditAsync(
            alumna, trabajo, "Nombre nuevo", "2026-08-30", null, "[]"));

        var guardado = await leerTextoGuardado(trabajo);
        var intacto = string.Equals(guardado, textoOriginal, StringComparison.Ordinal);

        bitacora.Escribir(
            $"[Reedicion] Trabajo fuera de Borrador: rechazado {r.ConditionCode} "
            + $"| texto-original-intacto={(intacto ? "si" : "no")}");
    }
}
