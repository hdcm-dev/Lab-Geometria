using System.Reflection;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Contracts.Service;
using GeometriaFactory.Infrastructure.Persistence;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Realiza el punto de acceso `A-16`, que responde por el estado del servicio.
/// </summary>
/// <remarks>
/// Es una de las CUATRO ausencias declaradas de la guardia —las otras tres son `A-01`, `A-02`
/// y `A-03`— y tiene que poder responder cuando nadie puede autenticarse (`US-29`).
///
/// La respuesta NO lleva dirección de servicio interno, ni ruta del almacén, ni traza
/// (`US-29` §3, tercer criterio).
///
/// LA RUTA. `Definicion-Superficie-HTTP.md` §3 la da como `/salud` y la marca
/// «[derivado; la fuente declara el punto y no su ruta]». Se usa la del corpus y no una propia:
/// la ruta definitiva es la decisión `A-4` / `Api BT-07`, del punto de control de la etapa `a`
/// (riesgo `R-04` de `Plan-Etapa-A.md` §7). El `healthcheck` de `deploy/compose.yaml`, la página
/// de estado del front y la comprobación de la publicación usan las tres esta misma ruta.
/// </remarks>
public static class HealthEndpoint
{
    /// <summary>Ruta del punto de salud. Derivada por `Definicion-Superficie-HTTP.md` §3.</summary>
    public const string Route = "/salud";

    public static IEndpointRouteBuilder MapHealthEndpoint(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapGet(Route, async (
            TwoPhaseStartup startup,
            StoreHealth store,
            ILoggerFactory registros,
            CancellationToken cancellationToken) =>
        {
            // ---- `Ready` SE EVALÚA, Y ANTES SE RECORDABA -------------------------------
            //
            // Hasta el 2026-08-31 esta línea publicaba `startup.StoreIsPrepared`, que
            // `TwoPhaseStartup` escribe UNA SOLA VEZ al terminar la preparación y que nada
            // vuelve a evaluar. El `healthcheck` de la composición sondea esta ruta cada 30
            // segundos: **estaba consultando un booleano inmutable**, y el servicio informaba
            // 200 «Ready» indefinidamente con el almacén borrado, de sólo lectura o corrupto.
            // Es `MI-09` de la mesa del 2026-08-31, votado 5-0.
            //
            // LA DECISIÓN NO CAMBIA: `ADR-00007` §2 puntos 4 y 5 ya declaran que este punto
            // responde por el ESTADO del servicio y no por el hecho de haber arrancado. Lo que
            // cambia es que ahora lo cumple.
            //
            // LAS DOS CONDICIONES SE CONJUGAN Y NO SE REEMPLAZAN. El arranque sigue contando:
            // mientras la fase 1 no terminó, no hay nada que preguntarle al almacén.
            var listo = startup.StoreIsPrepared
                && await store.IsUsableAsync(cancellationToken).ConfigureAwait(false);

            var health = new ServiceHealth(
                Ready: listo,
                Version: ReadVersion(),
                ServerTimeUtc: DateTimeOffset.UtcNow);

            // EL MOTIVO VA AL REGISTRO Y NO A LA RESPUESTA. `RA-03` gobierna lo que el
            // servicio dice, y este punto es anónimo: quien lo consulta puede ser cualquiera.
            // El operador mira el registro del servidor, que es donde el motivo sirve.
            if (!listo)
            {
                registros.CreateLogger(typeof(HealthEndpoint)).LogWarning(
                    "Salud en rojo. Preparación del arranque: {Preparada}. El almacén no respondió " +
                    "a la comprobación de esquema y escritura.", startup.StoreIsPrepared);
            }

            // 200 con el servicio en condiciones, 503 si no
            // (`Contratos-REST.md` §3 le da a `A-16` esos dos códigos y ningún otro).
            return health.Ready
                ? Results.Ok(health)
                : Results.Json(health, statusCode: StatusCodes.Status503ServiceUnavailable);
        })
        .WithName("Health")
        .AllowAnonymous();

        return endpoints;
    }

    private static string ReadVersion()
    {
        var assembly = typeof(HealthEndpoint).Assembly;

        return assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? assembly.GetName().Version?.ToString()
            ?? "0.0.0";
    }
}
