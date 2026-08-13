using System.Net.Http.Json;
using GeometriaFactory.Contracts.Service;

namespace GeometriaFactory.Web.Integration;

/// <summary>
/// La ÚNICA salida del front hacia el servicio de datos (`Web/05` §3.1 capa 3, y §3.2 punto 3).
/// </summary>
/// <remarks>
/// Si aparece una segunda salida, `RA-01` —la sesión interactiva no llega al servicio de datos—
/// se queda sin un lugar donde verificarse. La dirección base llega por configuración
/// (`ApiBaseUrl`) y se inyecta en el <see cref="HttpClient"/>: acá no hay ninguna dirección escrita.
/// </remarks>
public sealed class DataServiceClient
{
    /// <summary>
    /// Ruta del punto de salud `A-16`, derivada por `Definicion-Superficie-HTTP.md` §3.
    /// Es la misma constante que `GeometriaFactory.Api.Endpoints.HealthEndpoint.Route`, y los dos
    /// lados se mueven juntos cuando el punto de control cierre `A-4` / `Api BT-07` (riesgo `R-04`).
    /// </summary>
    private const string HealthPath = "salud";

    private readonly HttpClient _httpClient;

    public DataServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Consulta el punto de salud del servicio de datos y devuelve lo que el servidor respondió.
    /// </summary>
    /// <remarks>
    /// No traduce ni maquilla la falla: si el servicio está detenido, la excepción sube y la
    /// página de estado la muestra. Es lo que hace demostrable el criterio 2 de la transición
    /// `a` → `b`, que exige recorrer los dos casos —servicio corriendo y servicio detenido—
    /// para probar que el dato es real y no un literal.
    /// </remarks>
    public async Task<ServiceHealth?> GetServiceHealthAsync(CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient
            .GetAsync(HealthPath, cancellationToken)
            .ConfigureAwait(false);

        // El punto responde 200 o 503, y el cuerpo es el mismo en los dos casos
        // (`Contratos-REST.md` §3). El 503 no es un error de transporte: es un dato.
        return await response.Content
            .ReadFromJsonAsync<ServiceHealth>(cancellationToken)
            .ConfigureAwait(false);
    }
}
