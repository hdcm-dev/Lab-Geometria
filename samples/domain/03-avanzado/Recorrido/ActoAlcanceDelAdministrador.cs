using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>
/// `OP-11` — El alcance del administrador sobre los trabajos de la comisión. **`RN-02011`: el
/// borrador queda afuera**, y es la única exclusión.
/// </summary>
internal static class ActoAlcanceDelAdministrador
{
    internal static void Ejecutar(Bitacora bitacora, IReadOnlyList<Work> comision)
    {
        var dentro = 0; var fuera = new List<WorkStatus>();
        foreach (var w in comision)
        {
            var r = w.ResolveAdministratorScope(Role.Administrator, WorkOperation.View);
            if (r.Succeeded) dentro++; else fuera.Add(w.Status);
        }

        bitacora.Escribir(
            $"[4] Alcance del administrador: en-alcance={dentro} fuera-de-alcance={fuera.Count} "
            + $"({string.Join(", ", fuera.Select(Vocabulario.De))})");
    }

    internal static void EliminacionAdmitida(Bitacora bitacora, IReadOnlyList<Work> comision)
    {
        var admitidos = comision
            .Where(w => w.ResolveAdministratorScope(Role.Administrator, WorkOperation.Delete).Succeeded)
            .Select(w => Vocabulario.De(w.Status));

        bitacora.Escribir($"[5] Eliminacion por el administrador admitida en: {string.Join(", ", admitidos)}");
    }
}
