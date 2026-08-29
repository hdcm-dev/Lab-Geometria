using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Domain.Intermedio.Recorrido;

/// <summary>
/// `OP-05` — Constituye el trabajo con el texto del alumno **tal cual**, y verifica que el
/// dominio lo conserva íntegro.
/// </summary>
/// <remarks>
/// EL TEXTO SE COMPRUEBA POR PRESENCIA Y NO POR FORMA, que es lo que `Borrador` significa: un
/// trabajo nace en borrador **aunque su texto no verifique**, y eso es exactamente lo que los
/// escenarios `E-5` y `E-8` ejercitan.
/// </remarks>
internal static class ActoConstituirTrabajo
{
    internal static Work Ejecutar(Bitacora bitacora, Guid alumna, string escenario, string texto,
        DateTimeOffset momento, bool anunciar)
    {
        var creado = bitacora.Invocar(() => Work.Create(
            ownerId: alumna,
            name: $"Trabajo del escenario {escenario}",
            declaredDate: "2026-08-29",
            description: null,
            originalJson: texto,
            originalJsonPreservedDeclared: true,
            createdAt: momento));

        var trabajo = creado.Value!;
        bitacora.ContarTrabajo();

        if (anunciar)
        {
            var identico = string.Equals(trabajo.OriginalJson, texto, StringComparison.Ordinal);
            bitacora.Escribir(
                $"[{escenario}] Trabajo constituido: texto-identico={(identico ? "si" : "no")} "
                + $"estado={Vocabulario.De(trabajo.Status)}");
        }

        return trabajo;
    }
}
