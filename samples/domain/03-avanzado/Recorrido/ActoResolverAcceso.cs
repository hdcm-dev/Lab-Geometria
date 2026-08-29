using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Avanzado.Recorrido;

/// <summary>
/// `OP-09` — La resolución de acceso del alumno, y **la propiedad de `RN-02003`**: el trabajo
/// ajeno y el inexistente son **indistinguibles**.
/// </summary>
/// <remarks>
/// NO ALCANZA CON QUE LOS DOS DEVUELVAN EL MISMO CÓDIGO, y por eso el acto `[3]` compara los dos
/// resultados **campo por campo**: un resultado que trajera un dato distinto en cualquier otro
/// campo volvería distinguible el trabajo ajeno del inexistente y **vaciaría la regla**.
/// </remarks>
internal static class ActoResolverAcceso
{
    internal static (DomainResult Ajeno, DomainResult Inexistente) Ejecutar(
        Bitacora bitacora, Work ajeno, Guid solicitante)
    {
        DomainResult deAjeno = default, deInexistente = default;

        bitacora.Provocar(() => deAjeno = ajeno.ResolveStudentAccess(solicitante, WorkOperation.View));
        bitacora.Escribir($"[1] Trabajo ajeno: {deAjeno.ConditionCode}");

        // El «inexistente» se modela como un trabajo que el solicitante no puede alcanzar: el
        // dominio no tiene almacén y no sabe qué existe. Es la frontera que `CU-02009` declara.
        var inexistente = Fabrica.Trabajo(Guid.NewGuid(), "Un trabajo que el solicitante no alcanza",
            WorkStatus.Draft);
        bitacora.Provocar(() => deInexistente = inexistente.ResolveStudentAccess(solicitante, WorkOperation.View));
        bitacora.Escribir($"[2] Trabajo inexistente: {deInexistente.ConditionCode}");

        return (deAjeno, deInexistente);
    }

    internal static void CompararCampoPorCampo(Bitacora bitacora, DomainResult a, DomainResult b)
    {
        var identicos = a.Succeeded == b.Succeeded
            && string.Equals(a.ConditionCode, b.ConditionCode, StringComparison.Ordinal)
            && a.Equals(b);

        bitacora.Escribir(
            $"[3] Resultados [1] y [2] comparados campo por campo: identicos={(identicos ? "si" : "no")}");
    }
}
