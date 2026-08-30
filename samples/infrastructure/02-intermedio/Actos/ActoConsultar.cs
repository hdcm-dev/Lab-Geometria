namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Acto 2 — `CU-06003`, las DOS formas de lectura, y la consulta que no tiene camino.</summary>
internal static class ActoConsultar
{
    internal static async Task EjecutarAsync(Contexto contexto, Guid dueño, Guid trabajo, Action<string> escribir)
    {
        // ---- Proyección de listado ----
        // El tipo que vuelve es `WorkListEntry`, y NO LLEVA componentes ni texto original: no es
        // que el adaptador los omita al llenarlo, es que el registro no los declara. La diferencia
        // importa —una omisión se corrige con un descuido, una ausencia de campo no—.
        var listado = await contexto.EnTrabajos(r => r.ListOwnedByAsync(dueño)).ConfigureAwait(false);
        var tipo = typeof(GeometriaFactory.Application.Works.WorkListEntry);
        var traeComponentes = tipo.GetProperties().Count(p =>
            p.Name.Contains("Component", StringComparison.OrdinalIgnoreCase)
            || p.Name.Contains("Piece", StringComparison.OrdinalIgnoreCase));
        var traeTexto = tipo.GetProperties().Any(p =>
            p.Name.Contains("OriginalJson", StringComparison.OrdinalIgnoreCase));

        escribir($"[2] Consulta de listado: {listado.Count} trabajos | componentes en el resultado: "
            + $"{traeComponentes} | texto original en el resultado: {(traeTexto ? "si" : "no")}");

        // ---- Detalle completo ----
        var detalle = await contexto.EnTrabajos(r => r.FindByIdAsync(trabajo)).ConfigureAwait(false);
        var conPiezas = detalle!.Pieces.Count > 0 && detalle.Pieces.Sum(p => p.Components.Count) > 0;
        var conTexto = !string.IsNullOrEmpty(detalle.OriginalJson);
        escribir($"[2] Consulta de detalle: 1 trabajo | piezas y componentes presentes: "
            + $"{(conPiezas ? "si" : "no")} | texto original presente: {(conTexto ? "si" : "no")}");

        // ---- La consulta sin alcance declarado ----
        // DIVERGENCIA D-1 CONTRA EL SNAPSHOT DE §6, y es a favor del producto.
        //
        // §6 espera acá un rechazo tipado `QUERY_WITHOUT_DECLARED_SCOPE`. Ese código NO EXISTE en
        // el árbol, y su ausencia no es un olvido: `IWorkRepository` la declara por escrito. El
        // puerto no expone NINGUNA operación de listado sin recorte —las dos que hay lo llevan en
        // el nombre y en los parámetros—, de modo que la condición no tiene camino que la produzca.
        //
        // Un rechazo en tiempo de ejecución y una operación que no existe no son lo mismo: el
        // primero se puede alcanzar y hay que probarlo; la segunda no compila. El sample mide la
        // segunda, que es la que el producto eligió, y lo dice en lugar de fabricar el rechazo.
        var operaciones = typeof(GeometriaFactory.Application.Ports.IWorkRepository).GetMethods();
        var sinRecorte = operaciones.Count(m =>
            m.Name.StartsWith("List", StringComparison.Ordinal) && m.GetParameters().Length == 1);
        escribir($"[2] Consulta sin alcance declarado: sin camino en el puerto | "
            + $"operaciones de listado sin recorte: {sinRecorte}");
    }
}
