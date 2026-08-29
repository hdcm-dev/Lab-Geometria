using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>
/// `OP-10` — El desenlace de la revisión, con sus dos caminos y sus dos rechazos tipados.
/// </summary>
/// <remarks>
/// LOS DOS DESENLACES SON TERMINALES y ninguna transición sale de ellos: el acto `[7b]` lo
/// provoca a propósito sobre un trabajo ya resuelto.
/// </remarks>
internal static class ActoDesenlace
{
    internal static void Aprobar(Bitacora bitacora, Work trabajo)
    {
        bitacora.Provocar(() => trabajo.ApplyOutcome(Role.Administrator, WorkOutcome.Approve,
            comment: null, updatedAt: Fabrica.Momento));

        bitacora.Escribir(
            $"[6] Aprobar trabajo en Pendiente: estado={Vocabulario.De(trabajo.Status)} "
            + $"comentario={(trabajo.AdministratorComment is null ? "ausente" : "presente")}");
    }

    internal static void Rechazar(Bitacora bitacora, Work trabajo)
    {
        bitacora.Provocar(() => trabajo.ApplyOutcome(Role.Administrator, WorkOutcome.Reject,
            comment: "Revisá el área declarada de la segunda pieza.", updatedAt: Fabrica.Momento));

        bitacora.Escribir(
            $"[7] Rechazar trabajo en Pendiente: estado={Vocabulario.De(trabajo.Status)} "
            + $"comentario={(trabajo.AdministratorComment is null ? "ausente" : "presente")}");
    }

    internal static void SobreTerminal(Bitacora bitacora, Work yaResuelto)
    {
        var codigo = bitacora.Provocar(() => yaResuelto.ApplyOutcome(Role.Administrator,
            WorkOutcome.Approve, comment: null, updatedAt: Fabrica.Momento));

        bitacora.Escribir($"[7b] Desenlace sobre estado terminal: {codigo}");
    }

    internal static void SinPapel(Bitacora bitacora, Work enPendiente)
    {
        var codigo = bitacora.Provocar(() => enPendiente.ApplyOutcome(Role.Student,
            WorkOutcome.Approve, comment: null, updatedAt: Fabrica.Momento));

        bitacora.Escribir($"[7c] Desenlace sin papel de administrador: {codigo}");
    }
}
