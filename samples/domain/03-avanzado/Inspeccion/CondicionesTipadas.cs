using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

namespace GeometriaFactory.Samples.Domain.Avanzado.Inspeccion;

/// <summary>
/// Provoca las condiciones que el recorrido no provoca por sí solo, para completar las **doce**
/// que el snapshot declara, y verifica que **todas vuelvan por valor**.
/// </summary>
internal static class CondicionesTipadas
{
    internal static void Provocar(Bitacora bitacora, Guid alumna)
    {
        // Un trabajo sin dueño.
        bitacora.Provocar(() => Work.Create(Guid.Empty, "Sin dueño", "2026-08-29",
            null, "[]", true, Fabrica.Momento));

        // Un trabajo sin nombre.
        bitacora.Provocar(() => Work.Create(alumna, null, "2026-08-29", null, "[]",
            true, Fabrica.Momento));

        // Un trabajo sin texto.
        bitacora.Provocar(() => Work.Create(alumna, "Sin texto", "2026-08-29", null,
            null, true, Fabrica.Momento));

        // Un envío fuera de `Borrador`.
        var enviado = Fabrica.Trabajo(alumna, "Ya enviado", WorkStatus.Submitted);
        bitacora.Provocar(() => enviado.Submit(true, false, Fabrica.Momento));

        // Un desenlace sobre un trabajo que todavía está en `Borrador`.
        var borrador = Fabrica.Trabajo(alumna, "En borrador", WorkStatus.Draft);
        bitacora.Provocar(() => borrador.ApplyOutcome(Role.Administrator, WorkOutcome.Approve,
            null, Fabrica.Momento));

        // Un alcance de administrador pedido sin el papel.
        bitacora.Provocar(() => enviado.ResolveAdministratorScope(Role.Student, WorkOperation.View));

        // Un alcance de administrador sobre un borrador, que RN-02011 deja afuera.
        bitacora.Provocar(() => borrador.ResolveAdministratorScope(Role.Administrator, WorkOperation.View));

        // Una operación que el conjunto cerrado no tiene: se rechaza SIN evaluar la pertenencia,
        // que es el orden que `CU-02009` §6 declara.
        bitacora.Provocar(() => borrador.ResolveStudentAccess(alumna, (WorkOperation)99));
    }
}
