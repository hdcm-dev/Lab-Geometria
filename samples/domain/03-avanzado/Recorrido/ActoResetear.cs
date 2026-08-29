using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>
/// `OP-13` — El reseteo de la credencial. **`RN-02012`: conserva la cuenta y sus trabajos**, y el
/// acto lo verifica contando los trabajos antes y después.
/// </summary>
internal static class ActoResetear
{
    internal static void Ejecutar(Bitacora bitacora, Account alumna, IReadOnlyList<Work> suyos)
    {
        var estadoAntes = alumna.Status;
        var antes = suyos.Count;

        bitacora.Provocar(() => alumna.ResetPassword(
            provisionalPasswordHash: "hash-de-la-provisoria-nueva",
            worksCascadeDeclared: false));

        var despues = suyos.Count;
        var sinCambio = estadoAntes == alumna.Status;

        bitacora.Escribir(
            $"[8] Reseteo: estado-de-cuenta={(sinCambio ? "sin-cambio" : "cambiado")} "
            + $"trabajos-antes={antes} trabajos-despues={despues}");
    }
}
