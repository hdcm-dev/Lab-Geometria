using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Acto 4 — `CU-06004`, la baja de cuenta con arrastre: la unidad de trabajo vista de afuera.</summary>
/// <remarks>
/// LAS DOS LÍNEAS SON LA MISMA REGLA, MEDIDA DE LOS DOS LADOS (`ADR-06002`). Cuando el arrastre se
/// completa quedan CERO trabajos de esa cuenta; cuando se interrumpe quedan los DOS que había. No
/// hay tercer resultado, y la ausencia de estado intermedio observable es lo que hay que ver.
/// </remarks>
internal static class ActoArrastrar
{
    internal static async Task EjecutarAsync(Contexto contexto, Action<string> escribir)
    {
        // ---- La baja que se completa ----
        var conDos = await Fixture.CuentaConDosTrabajosAsync(contexto, "arrastre.completo@ejemplo.edu").ConfigureAwait(false);
        await contexto.EnCuentas(r => r.RemoveAsync(conDos)).ConfigureAwait(false);
        var quedaron = await contexto.ContarTrabajosDeAsync(conDos.Id).ConfigureAwait(false);
        escribir($"[4] Baja de la cuenta con 2 trabajos: arrastre aplicado | "
            + $"trabajos que quedaron de esa cuenta: {quedaron}");

        // ---- La baja que no procede ----
        // DIVERGENCIA D-2 CONTRA EL SNAPSHOT DE §6, y es de NOMBRE Y DE CAPA, no de conducta.
        //
        // §6 espera `PARTIAL_DELETION_NOT_ALLOWED`. Ese código no existe. El que sí existe es
        // `DELETION_WITHOUT_WORK_CASCADE`, y no vive en esta capa: lo devuelve
        // `Account.AdmitDeletion` en el DOMINIO, cuando quien pide la baja no declaró el arrastre.
        //
        // Y LA DIFERENCIA DE CAPA EXPLICA LA DE NOMBRE. El snapshot lo escribió esperando que el
        // adaptador rechazara una baja a medias; el producto hizo que la baja a medias NO SE PUEDA
        // PEDIR: el arrastre es una sola llamada y una sola confirmación, y quien no lo declara no
        // llega al adaptador. La condición se corrió hacia adentro y se quedó con el nombre de lo
        // que impide —una baja sin arrastre declarado— en lugar del de lo que evita.
        var conDosMas = await Fixture.CuentaConDosTrabajosAsync(contexto, "arrastre.interrumpido@ejemplo.edu").ConfigureAwait(false);
        var admision = conDosMas.AdmitDeletion(worksCascadeDeclared: false);
        var siguen = await contexto.ContarTrabajosDeAsync(conDosMas.Id).ConfigureAwait(false);
        escribir($"[4] Arrastre no declarado: {admision.ConditionCode} | trabajos que quedaron: {siguen}");
    }
}

/// <summary>Cuentas y trabajos de utilería, para los actos que necesitan un estado de partida.</summary>
internal static class Fixture
{
    internal static async Task<Account> CuentaConDosTrabajosAsync(Contexto contexto, string correo)
    {
        var cuenta = Account.Register(correo, "Ana", "Duarte", passwordHash: null,
            emailUniquenessVerified: true, Role.Student, AccountStatus.Pending, contexto.Reloj.UtcNow)
            .Exigir($"El alta de utilería {correo}");
        cuenta.Enable("hash-provisorio");
        await contexto.EnCuentas(r => r.AddAsync(cuenta)).ConfigureAwait(false);

        for (var i = 0; i < 2; i++)
        {
            var trabajo = Work.Create(cuenta.Id, $"Utilería {i + 1}", "2026-08-29", "Trabajo de utilería",
                "{\"figuras\":[]}", originalJsonPreservedDeclared: true, contexto.Reloj.UtcNow)
                .Exigir($"El trabajo de utilería {i + 1}");
            await contexto.EnTrabajos(r => r.AddAsync(trabajo)).ConfigureAwait(false);
        }

        return cuenta;
    }
}
