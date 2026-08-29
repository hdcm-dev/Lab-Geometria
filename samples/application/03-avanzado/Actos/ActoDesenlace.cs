using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Avanzado.Semilla;

namespace GeometriaFactory.Samples.Application.Avanzado.Actos;

/// <summary>
/// `CU-04008` — El desenlace de la revisión: aprobar y rechazar, con el comentario **opcional**, y
/// sus dos rechazos.
/// </summary>
internal static class ActoDesenlace
{
    internal static async Task EjecutarAsync(
        Bitacora b, ResolveWorkUseCase caso, ComisionDeEjemplo comision, Guid paraRechazar, Guid paraElAlumno)
    {
        var aprobado = await caso.ExecuteAsync(Role.Administrator, comision.EnPendiente.Id,
            nameof(WorkOutcome.Approve), "Buen trabajo.");
        b.Escribir(
            $"[3] Aprobar desde Pendiente con comentario: "
            + $"{Vocabulario.De(comision.EnPendiente.Status)}");

        var rechazado = await caso.ExecuteAsync(Role.Administrator, paraRechazar,
            nameof(WorkOutcome.Reject), null);
        b.Escribir(
            $"[3] Rechazar desde Pendiente sin comentario: "
            + $"{Vocabulario.De(WorkStatus.Rejected)} (el comentario es opcional)");

        var sobreTerminal = await caso.ExecuteAsync(Role.Administrator, comision.Aprobado.Id,
            nameof(WorkOutcome.Approve), null);
        b.Escribir($"[3] Desenlace sobre un trabajo ya Aprobado: rechazado {sobreTerminal.ConditionCode}");

        // SOBRE UN TRABAJO EN `Pendiente` Y NO SOBRE UNO TERMINAL: con uno terminal corta antes la
        // guarda de terminalidad y el rechazo que se ve es otro. La primera versión de este acto
        // usaba el trabajo ya rechazado y obtenía `TRANSITION_FROM_TERMINAL_STATUS`.
        var porAlumno = await caso.ExecuteAsync(Role.Student, paraElAlumno,
            nameof(WorkOutcome.Approve), null);
        b.Escribir($"[3] Desenlace pedido por un alumno: rechazado {porAlumno.ConditionCode}");
    }
}
