using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Acto 1 — `CU-06003`, la escritura: tres trabajos con todo lo que cuelga de ellos.</summary>
/// <remarks>
/// LO QUE SE MIRA NO ES QUE LA LLAMADA VUELVA, sino que **lo que se guardó se pueda contar
/// después, en otro contexto**. Por eso cada trabajo se cuenta releyéndolo del almacén con un
/// contexto nuevo, y no sobre la instancia que se acaba de escribir: contar sobre la instancia
/// en memoria mediría el grafo que armó el sample, no lo que el adaptador materializó.
/// </remarks>
internal static class ActoMaterializar
{
    internal static async Task<Guid> EjecutarAsync(
        Contexto contexto,
        Guid dueño,
        string escenario,
        string etiqueta,
        (int Figuras, IReadOnlyList<Piece> Piezas, IReadOnlyList<Observation> Observaciones) interpretacion,
        bool mostrarComponentes,
        bool mostrarTextoOriginal,
        Action<string> escribir)
    {
        var texto = File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Escenarios", $"{escenario}.txt"));

        var creado = Work.Create(
            ownerId: dueño,
            name: $"Trabajo de {etiqueta}",
            declaredDate: "2026-08-29",
            description: $"Materialización del escenario {etiqueta}",
            originalJson: texto,
            originalJsonPreservedDeclared: true,
            createdAt: contexto.Reloj.UtcNow);

        var trabajo = creado.Exigir($"El trabajo de {etiqueta}");
        trabajo.AdoptInterpretation(
            interpretacion.Figuras, interpretacion.Piezas, interpretacion.Observaciones, contexto.Reloj.UtcNow);

        // FA-01 de `Submit`: con errores de interpretación el trabajo se queda en `Draft`.
        var conErrores = interpretacion.Observaciones.Any(o => o.Kind == ObservationKind.ValidationError);
        trabajo.Submit(parseResultDeclared: true, validationErrorsDeclared: conErrores, contexto.Reloj.UtcNow);

        await contexto.EnTrabajos(r => r.AddAsync(trabajo)).ConfigureAwait(false);

        // La relectura, con un repositorio abierto sobre otro contexto.
        var releido = await contexto.EnTrabajos(r => r.FindByIdAsync(trabajo.Id)).ConfigureAwait(false);
        var piezas = releido!.Pieces.Count;
        var componentes = releido.Pieces.Sum(p => p.Components.Count);
        var observaciones = releido.Observations.Count;

        var literal = string.Equals(releido.OriginalJson, texto, StringComparison.Ordinal);

        // Qué lleva cada renglón lo fija el snapshot de §6 y no una regla del sample: el de `E-1`
        // lleva las tres cuentas y el texto, el de `E-2` las tres cuentas, y el de `E-5` sólo dos.
        var renglon = $"[1] Trabajo de {etiqueta} materializado: piezas={piezas}";
        if (mostrarComponentes) renglon += $" componentes={componentes}";
        renglon += $" observaciones={observaciones}";
        if (mostrarTextoOriginal) renglon += $" | texto original: {(literal ? "guardado literal" : "ALTERADO")}";
        escribir(renglon);

        return trabajo.Id;
    }
}

/// <summary>Acto 1, segunda mitad — `RN-06008` medida donde se puede violar.</summary>
internal static class ActoTextoOriginal
{
    internal static async Task EjecutarAsync(Contexto contexto, Guid dueño, Action<string> escribir)
    {
        var original = "{\"figuras\":[{\"Tipo\":\"Cubo\",\"Lado\":3}]}";
        var trabajo = Work.Create(dueño, "Trabajo con texto propio", "2026-08-29", null,
            original, originalJsonPreservedDeclared: true, contexto.Reloj.UtcNow)
            .Exigir("El trabajo con texto propio");
        await contexto.EnTrabajos(r => r.AddAsync(trabajo)).ConfigureAwait(false);

        // DIVERGENCIA D-4 CONTRA EL SNAPSHOT DE §6, de NOMBRE y de CAPA.
        // §6 espera `WRITE_REWRITES_ORIGINAL_JSON`; el que existe es `ORIGINAL_JSON_ALTERED`, y lo
        // devuelve `Work.Edit` en el dominio. La conducta que §6 describe se cumple entera —el
        // texto no se reemplaza, y la escritura que lo intenta se rechaza en lugar de aplicarse—;
        // lo que cambió es DÓNDE se rechaza. El adaptador nunca ve la operación.
        var intento = trabajo.Edit("Otro nombre", "2026-08-29", null,
            "{\"figuras\":[{\"Tipo\":\"Cubo\",\"Lado\":9}]}",
            originalJsonPreservedDeclared: false, contexto.Reloj.UtcNow);

        var enElAlmacen = await contexto.EnTrabajos(r => r.FindByIdAsync(trabajo.Id)).ConfigureAwait(false);
        var intacto = string.Equals(enElAlmacen!.OriginalJson, original, StringComparison.Ordinal);

        escribir($"[6] Escritura que reemplaza el texto original: rechazada {intento.ConditionCode} | "
            + $"texto en el almacen: {(intacto ? "intacto" : "REEMPLAZADO")}");
    }
}
