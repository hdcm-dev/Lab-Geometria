using GeometriaFactory.Contracts.Errors;

namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// Declara en el documento OpenAPI las respuestas que cada punto produce (expediente 0003, tramo
/// T-12, decisión H-09). No cambia ninguna respuesta: sólo la describe.
/// </summary>
/// <remarks>
/// NO USA <c>ProducesProblem</c>, Y ES DELIBERADO. <c>ProducesProblem</c> declara un cuerpo
/// <c>ProblemDetails</c> con tipo <c>application/problem+json</c>, y esta superficie no lo emite:
/// sus errores son el <see cref="ErrorResponse"/> del contrato, escrito como JSON común por
/// <see cref="ContractTranslation.Problem"/>, por <see cref="PendingPasswordChangeGuard"/> y por
/// <see cref="Composition.ContractErrorHandler"/>. Declarar <c>ProblemDetails</c> describiría un
/// cuerpo que no existe.
///
/// HAY RECHAZOS SIN CUERPO: el desafío del acceso firmado (<c>401</c>), la petición ilegible
/// (<c>400</c>, `ContractErrorHandler`) y el límite de tasa (<c>429</c>). El documento declara el
/// cuerpo del contrato para el número cuando el punto puede producirlo con cuerpo.
/// </remarks>
internal static class ResponseDocumentation
{
    /// <summary>Los errores del contrato, con el cuerpo <see cref="ErrorResponse"/>.</summary>
    public static RouteHandlerBuilder ProducesContractErrors(
        this RouteHandlerBuilder builder, params int[] statusCodes)
    {
        ArgumentNullException.ThrowIfNull(builder);

        foreach (var statusCode in statusCodes)
        {
            builder.Produces<ErrorResponse>(statusCode, "application/json");
        }

        return builder;
    }

    /// <summary>
    /// El rechazo por límite de tasa: <c>429</c> sin cuerpo y con <c>Retry-After</c>
    /// (`ContractRateLimiting`). Lo hereda todo el grupo <c>/v1</c>.
    /// </summary>
    public static RouteHandlerBuilder ProducesRateLimitRejection(this RouteHandlerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.Produces(StatusCodes.Status429TooManyRequests);
    }
}
