using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Application.Intermedio.Actos;

/// <summary>
/// `CU-04009` — El retiro del trabajo por su dueño, acotado a `Borrador`, y sus dos rechazos.
/// </summary>
/// <remarks>
/// EL TRABAJO AJENO Y EL INEXISTENTE RESPONDEN IGUAL, que es `RN-04003` visto desde esta capa:
/// distinguirlos permitiría averiguar qué trabajos existen.
/// </remarks>
internal static class ActoEliminar
{
    internal static async Task EnBorradorAsync(Bitacora b, DeleteWorkUseCase caso, Guid alumna, Guid trabajo)
    {
        var r = await b.InvocarAsync(() => caso.ExecuteAsync(alumna, Role.Student, trabajo));
        b.Escribir($"[Retiro] Trabajo en Borrador por su dueno: {(r.Succeeded ? "retirado" : "rechazado " + r.ConditionCode)}");
    }

    internal static async Task EnPendienteAsync(Bitacora b, DeleteWorkUseCase caso, Guid alumna, Guid trabajo)
    {
        var r = await b.InvocarAsync(() => caso.ExecuteAsync(alumna, Role.Student, trabajo));
        b.Escribir($"[Retiro] Trabajo en Pendiente por su dueno: rechazado {r.ConditionCode}");
    }

    internal static async Task AjenoAsync(Bitacora b, DeleteWorkUseCase caso, Guid otra, Guid trabajo)
    {
        var r = await b.InvocarAsync(() => caso.ExecuteAsync(otra, Role.Student, trabajo));
        b.Escribir($"[Retiro] Trabajo ajeno: rechazado {r.ConditionCode}");
    }
}
