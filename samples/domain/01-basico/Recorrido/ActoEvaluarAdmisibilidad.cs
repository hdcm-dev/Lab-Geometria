using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Domain.Basico.Recorrido;

/// <summary>
/// `OP-04` — La guarda única de admisibilidad, invocada TRES VECES sobre la misma cuenta
/// a medida que avanza su ciclo de vida. Es el punto del sample: los tres desenlaces de
/// `CU-02004` salen de la misma llamada y no de tres comprobaciones repartidas.
/// </summary>
internal static class ActoEvaluarAdmisibilidad
{
    internal static void Ejecutar(Bitacora bitacora, Account cuenta, string rotulo)
    {
        var admision = bitacora.Invocar(cuenta.EvaluateAdmission);
        var motivos = admision.IsAdmissible ? "0" : admision.Reason!;
        var desenlace = admision.IsAdmissible ? "admisible" : "no-admisible";
        bitacora.Escribir($"{rotulo}: {desenlace} motivos={motivos}");
    }
}
