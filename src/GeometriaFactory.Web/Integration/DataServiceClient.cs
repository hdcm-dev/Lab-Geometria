using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Contracts.Service;

namespace GeometriaFactory.Web.Integration;

/// <summary>
/// La ÚNICA salida del front hacia el servicio de datos (`Web/05` §3.1 capa 3, y §3.2 punto 3).
/// </summary>
/// <remarks>
/// Si aparece una segunda salida, `RA-01` —la sesión interactiva no llega al servicio de datos—
/// se queda sin un lugar donde verificarse. La dirección base llega por configuración
/// (`ApiBaseUrl`) y se inyecta en el <see cref="HttpClient"/>: acá no hay ninguna dirección escrita.
///
/// TODO LO QUE ESTA CLASE HACE OCURRE DEL LADO DEL SERVIDOR de la pieza pública. Es lo que hace
/// que la credencial de sesión no tenga por dónde llegar al navegador: quien la adjunta es este
/// código, en el mismo proceso donde vive el estado del circuito.
///
/// NINGÚN MENSAJE QUE ESTA CLASE DEVUELVE LLEVA LA DIRECCIÓN DEL SERVICIO (RA-03). Las
/// excepciones de transporte se convierten en el error de servicio no disponible del contrato,
/// con el texto de la maqueta, y no se propagan.
/// </remarks>
public sealed class DataServiceClient
{
    /// <summary>
    /// Ruta del punto de salud `A-16`, derivada por `Definicion-Superficie-HTTP.md` §3.
    /// Es la misma constante que `GeometriaFactory.Api.Endpoints.HealthEndpoint.Route`, y los dos
    /// lados se mueven juntos cuando el punto de control cierre `A-4` / `Api BT-07` (riesgo `R-04`).
    /// </summary>
    private const string HealthPath = "salud";

    /// <summary>Ruta del punto de canje `A-01`. Declarada por el intake §17.5.P.3.</summary>
    private const string TokenPath = "auth/token";

    /// <summary>Ruta del punto de configuración `A-03`. [derivado]</summary>
    private const string AdministratorSetupPath = "cuentas/administrador";

    /// <summary>Ruta del punto de cambio de la contraseña propia `A-05`. [derivado]</summary>
    private const string OwnPasswordPath = "cuenta/contrasena";

    /// <summary>
    /// Ruta del alta de cuenta `A-02` y raíz del listado `A-06`, del cambio de situación `A-07`,
    /// de la baja `A-08` y del reseteo `A-09`. Es la misma constante que
    /// `GeometriaFactory.Api.Endpoints.CommissionAccountEndpoints.AccountsRoute`, sin la barra
    /// inicial porque acá la dirección base la pone la configuración. [derivado]
    /// </summary>
    private const string AccountsPath = "cuentas";

    /// <summary>
    /// Texto del estado degradado, tomado literal de la maqueta aprobada.
    /// </summary>
    private const string ServiceUnavailableMessage =
        "El laboratorio está en pie, pero no está pudiendo llegar a tus datos en este momento.";

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

    /// <summary>`A-03` — Configura la cuenta de administrador del primer arranque.</summary>
    public Task<DataServiceOutcome<AccountSetupResponse>> ConfigureAdministratorAsync(
        AdministratorSetupRequest request,
        CancellationToken cancellationToken = default) =>
        PostAsync<AdministratorSetupRequest, AccountSetupResponse>(
            AdministratorSetupPath, request, accessToken: null, cancellationToken);

    /// <summary>`A-01` — Canjea correo y contraseña por la credencial de sesión.</summary>
    public Task<DataServiceOutcome<SessionResponse>> ExchangeCredentialsAsync(
        CredentialExchangeRequest request,
        CancellationToken cancellationToken = default) =>
        PostAsync<CredentialExchangeRequest, SessionResponse>(
            TokenPath, request, accessToken: null, cancellationToken);

    /// <summary>
    /// `A-05` — Cambia la contraseña propia exigiendo la vigente.
    /// </summary>
    /// <remarks>
    /// La credencial de sesión se adjunta ACÁ, del lado del servidor. Es el único lugar del
    /// producto donde la credencial se usa, y el navegador no participa.
    /// </remarks>
    public async Task<DataServiceOutcome<bool>> ChangeOwnPasswordAsync(
        OwnPasswordChangeRequest request,
        string? accessToken,
        CancellationToken cancellationToken = default)
    {
        var outcome = await PostAsync<OwnPasswordChangeRequest, object>(
            OwnPasswordPath, request, accessToken, cancellationToken).ConfigureAwait(false);

        return outcome.Succeeded
            ? DataServiceOutcome<bool>.Resolved(true)
            : DataServiceOutcome<bool>.Failed(outcome.Error!);
    }

    /// <summary>`A-02` — Registra la cuenta de un alumno, con tres campos y SIN contraseña.</summary>
    /// <remarks>
    /// NO LLEVA CREDENCIAL, y es el registro de cuenta que `Api CU-03` declara anónimo por
    /// diseño: es la puerta por la que el alumno entra al laboratorio. La cuenta nace `Pending`
    /// y todavía no obtiene acceso (RN-06).
    /// </remarks>
    public Task<DataServiceOutcome<AccountRegistrationResponse>> RegisterAccountAsync(
        AccountRegistrationRequest request,
        CancellationToken cancellationToken = default) =>
        SendAsync<AccountRegistrationRequest, AccountRegistrationResponse>(
            HttpMethod.Post, AccountsPath, request, accessToken: null, cancellationToken);

    /// <summary>`A-06` — Trae el listado de cuentas de la comisión.</summary>
    /// <remarks>
    /// UN LISTADO VACÍO NO ES UN FALLO: el punto responde `200` con una colección vacía, y quien
    /// llama distingue vacío de fallo por <see cref="DataServiceOutcome{TValue}.Succeeded"/> y no
    /// por el conteo. La credencial se adjunta ACÁ, del lado del servidor.
    /// </remarks>
    public Task<DataServiceOutcome<AccountListItem[]>> ListAccountsAsync(
        string? accessToken,
        CancellationToken cancellationToken = default) =>
        SendAsync<object, AccountListItem[]>(
            HttpMethod.Get, AccountsPath, request: null, accessToken, cancellationToken);

    /// <summary>`A-07` — Habilita, rehabilita o bloquea una cuenta.</summary>
    /// <remarks>
    /// LA PROVISORIA VIENE EN LA RESPUESTA Y NO SE ESCRIBE ACÁ (RN-14, RN-16). Este método no
    /// tiene ningún parámetro de contraseña y no podría tenerlo: el panel no lleva campo de
    /// contraseña, y el valor lo produce el servicio.
    /// </remarks>
    public Task<DataServiceOutcome<AccountStatusChangeResponse>> ChangeAccountStatusAsync(
        Guid accountId,
        string intendedStatus,
        string? accessToken,
        CancellationToken cancellationToken = default) =>
        SendAsync<AccountStatusChangeRequest, AccountStatusChangeResponse>(
            HttpMethod.Post,
            $"{AccountsPath}/{accountId}/situacion",
            new AccountStatusChangeRequest(accountId, intendedStatus),
            accessToken,
            cancellationToken);

    /// <summary>`A-08` — Da de baja una cuenta con su confirmación escrita.</summary>
    /// <remarks>
    /// EL CORREO ESCRITO VIAJA EN EL CUERPO Y NO EN LA DIRECCIÓN, que es lo que el contrato
    /// declara: en la ruta o en la cadena de consulta quedaría escrito en el registro de acceso
    /// de cualquier intermediario. La pantalla **no es la última defensa**: el servicio compara y
    /// rechaza con `CONFIRMATION_MISMATCH` cuando no coincide.
    /// </remarks>
    public async Task<DataServiceOutcome<bool>> DeleteAccountAsync(
        Guid accountId,
        string confirmationEmail,
        string? accessToken,
        CancellationToken cancellationToken = default)
    {
        var outcome = await SendAsync<AccountDeletionRequest, object>(
            HttpMethod.Delete,
            $"{AccountsPath}/{accountId}",
            new AccountDeletionRequest(accountId, confirmationEmail),
            accessToken,
            cancellationToken).ConfigureAwait(false);

        return outcome.Succeeded
            ? DataServiceOutcome<bool>.Resolved(true)
            : DataServiceOutcome<bool>.Failed(outcome.Error!);
    }

    /// <summary>`A-09` — Resetea la contraseña de una cuenta de alumno.</summary>
    /// <remarks>
    /// TAMPOCO ACÁ HAY PARÁMETRO DE CONTRASEÑA. La provisoria llega en la respuesta, se muestra
    /// una vez y no se guarda en ninguna parte de esta pieza.
    /// </remarks>
    public Task<DataServiceOutcome<PasswordResetResponse>> ResetAccountPasswordAsync(
        Guid accountId,
        string? accessToken,
        CancellationToken cancellationToken = default) =>
        SendAsync<PasswordResetRequest, PasswordResetResponse>(
            HttpMethod.Post,
            $"{AccountsPath}/{accountId}/reseteo-de-contrasena",
            new PasswordResetRequest(accountId),
            accessToken,
            cancellationToken);

    private Task<DataServiceOutcome<TResponse>> PostAsync<TRequest, TResponse>(
        string path,
        TRequest request,
        string? accessToken,
        CancellationToken cancellationToken) =>
        SendAsync<TRequest, TResponse>(HttpMethod.Post, path, request, accessToken, cancellationToken);

    /// <summary>
    /// El ÚNICO lugar por donde sale una solicitud hacia el servicio de datos, cualquiera sea su
    /// verbo.
    /// </summary>
    /// <remarks>
    /// LOS CUATRO VERBOS PASAN POR ACÁ, y es lo que hace que RA-03 se cumpla sin depender de que
    /// cada método se acuerde: el manejo de la excepción de transporte, la lectura del error del
    /// contrato y el reemplazo del texto que lleva la dirección del servicio están escritos una
    /// sola vez. Un cuerpo nulo no se envía —es el caso del `GET`— y un `DELETE` **sí** lo lleva,
    /// porque el correo escrito como confirmación tiene que viajar en el cuerpo.
    /// </remarks>
    private async Task<DataServiceOutcome<TResponse>> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string path,
        TRequest? request,
        string? accessToken,
        CancellationToken cancellationToken)
    {
        try
        {
            using var message = new HttpRequestMessage(method, path)
            {
                Content = request is null ? null : JsonContent.Create(request),
            };

            if (!string.IsNullOrEmpty(accessToken))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            using var response = await _httpClient
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                // `A-05` responde `200` sin cuerpo y `A-08` responde `204`: no hay nada que leer
                // y no es un fallo. Los dos se piden con `object` como tipo de respuesta.
                if (typeof(TResponse) == typeof(object))
                {
                    return DataServiceOutcome<TResponse>.Resolved(default!);
                }

                var value = await response.Content
                    .ReadFromJsonAsync<TResponse>(cancellationToken)
                    .ConfigureAwait(false);

                return value is null
                    ? DataServiceOutcome<TResponse>.Failed(Unavailable())
                    : DataServiceOutcome<TResponse>.Resolved(value);
            }

            var error = await ReadErrorAsync(response, cancellationToken).ConfigureAwait(false);
            return DataServiceOutcome<TResponse>.Failed(error);
        }
        catch (HttpRequestException)
        {
            // El mensaje de la excepción lleva la dirección del servicio: no se propaga (RA-03).
            return DataServiceOutcome<TResponse>.Failed(Unavailable());
        }
        catch (TaskCanceledException)
        {
            return DataServiceOutcome<TResponse>.Failed(Unavailable());
        }
    }

    private static async Task<ErrorResponse> ReadErrorAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        try
        {
            var error = await response.Content
                .ReadFromJsonAsync<ErrorResponse>(cancellationToken)
                .ConfigureAwait(false);

            if (error is not null && !string.IsNullOrWhiteSpace(error.Code))
            {
                return error;
            }
        }
        catch (Exception exception) when (exception is HttpRequestException or System.Text.Json.JsonException)
        {
            // Una respuesta que no es del contrato no se muestra tal cual: se reemplaza.
        }

        // El `401` de la guardia no lleva código del contrato, y es deliberado (`Api CU-02` §6):
        // acá se lo convierte en el código de credencial inválida, que es lo que la pieza pública
        // necesita para volver a pedir credenciales.
        return response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            ? new ErrorResponse(
                ErrorCode.InvalidCredentials,
                "El correo o la contraseña no corresponden.",
                [],
                DateTimeOffset.UtcNow)
            : Unavailable();
    }

    private static ErrorResponse Unavailable() =>
        new(ErrorCode.ServiceUnavailable, ServiceUnavailableMessage, [], DateTimeOffset.UtcNow);
}
