using System.Reflection;
using GeometriaFactory.Api.Composition;
using GeometriaFactory.Contracts.Service;

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

        endpoints.MapGet(Route, (TwoPhaseStartup startup) =>
        {
            var health = new ServiceHealth(
                Ready: startup.StoreIsPrepared,
                Version: ReadVersion(),
                ServerTimeUtc: DateTimeOffset.UtcNow);

            // 200 con el servicio en condiciones, 503 si la preparación no terminó
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
