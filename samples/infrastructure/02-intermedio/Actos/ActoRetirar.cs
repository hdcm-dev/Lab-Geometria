namespace GeometriaFactory.Samples.Infrastructure.Intermedio;

/// <summary>Acto 3 — `CU-06004`, el retiro físico de un trabajo con todo lo que cuelga.</summary>
/// <remarks>
/// EL RETIRO ES COMPROBABLE POR AUSENCIA (`RE-15`): no hay marca de borrado lógico, no hay
/// papelera y no hay historial. Lo que se cuenta después no es un estado del trabajo: es cuántas
/// filas de piezas, componentes y observaciones quedaron en el archivo. Si el arrastre del retiro
/// no estuviera declarado en el esquema, esas filas quedarían huérfanas y el conteo lo diría.
/// </remarks>
internal static class ActoRetirar
{
    internal static async Task EjecutarAsync(Contexto contexto, Guid trabajo, Action<string> escribir)
    {
        // LEER Y RETIRAR VAN EN LA MISMA UNIDAD DE TRABAJO, Y NO ES UNA COMODIDAD.
        // Separarlos falla, y el mensaje lo dice entero: «the value of shadow key property
        // 'Observation.Id' is unknown ... shadow property values cannot be preserved when the
        // entity is not being tracked». Las observaciones y los componentes son colecciones
        // poseídas con clave sombra: existen en el esquema y NO en el tipo del dominio. Un
        // trabajo leído en otro contexto llega sin ellas, y el adaptador no tiene qué borrar.
        //
        // NO ES UN DEFECTO DEL ADAPTADOR: es la consecuencia de que el dominio no cargue con una
        // clave que sólo le sirve al almacén, que es lo que la clave sombra viene a permitir. El
        // servicio lee y escribe dentro del alcance de una petición, de modo que la condición se
        // cumple sola ahí. El sample tiene que declararla porque acá no hay petición que la dé.
        await contexto.EnTrabajos(async r =>
        {
            var elTrabajo = await r.FindByIdAsync(trabajo).ConfigureAwait(false);
            await r.RemoveAsync(elTrabajo!).ConfigureAwait(false);
        }).ConfigureAwait(false);

        var quedo = await contexto.EnTrabajos(r => r.FindByIdAsync(trabajo)).ConfigureAwait(false);
        var colgando = await contexto.ContarFilasColgandoDeAsync(trabajo).ConfigureAwait(false);

        escribir($"[3] Retiro de un trabajo: {(quedo is null ? "retirado" : "SIGUE")} | "
            + $"piezas, componentes y observaciones que quedaron: {colgando}");
    }
}
