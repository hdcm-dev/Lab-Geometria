using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Application.Basico.Actos;

/// <summary>
/// `CU-04003` — La consulta de admisibilidad **con su motivo**, sobre la misma cuenta a medida que
/// avanza su ciclo de vida.
/// </summary>
/// <remarks>
/// LA GUARDA ES ÚNICA Y NO ESTÁ REPARTIDA: los tres desenlaces salen de la misma llamada al
/// dominio, y esta capa sólo la invoca y traduce su motivo.
/// </remarks>
internal static class ActoAdmisibilidad
{
    internal static void Ejecutar(Bitacora bitacora, Account pendiente, Account habilitada)
    {
        bitacora.Acto();

        var dePendiente = pendiente.EvaluateAdmission();
        bitacora.Rechazo(
            $"[3] Admisibilidad de la cuenta pendiente: no admisible motivo={dePendiente.Reason}");

        var conMarca = habilitada.EvaluateAdmission();
        bitacora.Rechazo(
            $"[3] Admisibilidad de la cuenta habilitada con marca: no admisible motivo={conMarca.Reason}");

        habilitada.ReplaceCredential("hash-elegido-por-la-cuenta", currentCredentialVerified: true);
        var sinMarca = habilitada.EvaluateAdmission();
        bitacora.Escribir(
            $"[3] Admisibilidad de la cuenta habilitada sin marca: "
            + $"{(sinMarca.IsAdmissible ? "admisible" : "no admisible")}");
    }
}
