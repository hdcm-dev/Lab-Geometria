using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Domain.Values;
using Microsoft.Data.Sqlite;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL CICLO DE VIDA DE LA CUENTA DE ALUMNO, SOBRE HTTP DE VERDAD Y CONTRA EL ALMACÉN DE VERDAD:
/// los diez criterios de transición de la etapa `d` que le tocan a la superficie del servicio.
/// </summary>
/// <remarks>
/// NO SE REEMPLAZA NINGÚN SERVICIO (ver <see cref="DataServiceHarness"/>): el repositorio es el de
/// EF Core, la derivación es PBKDF2, la producción de la provisoria es la del producto y la
/// guardia es la del producto. Lo que se verifica es el cableado real, y sustituir cualquiera de
/// esas piezas dejaría de verificarlo.
///
/// LAS PROPIEDADES QUE NO SE VEN EN UNA RESPUESTA SE LEEN DEL ALMACÉN, con una consulta directa:
/// que la provisoria **no quedó guardada en claro**, que el reseteo **no cambió la situación** y
/// que la marca está puesta o levantada. Creerle a la respuesta sobre esas tres cosas sería
/// verificar lo que el producto dice de sí mismo.
/// </remarks>
public sealed class AccountLifecycleSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string StudentFirstName = "Ana";
    private const string StudentLastName = "Diaz";

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public AccountLifecycleSurfaceTests(ITestOutputHelper output)
    {
        _output = output;
        _dataService = new DataServiceHarness(_storePath);
        _client = _dataService.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    // ---- CRITERIO 1 · el alumno se registra sin elegir contraseña ---------------------------

    /// <summary>
    /// `A-02` — tres campos, **cero de contraseña**, y la cuenta queda `Pending` **sin credencial
    /// derivada en el almacén**. La ausencia se mide sobre el tipo del contrato y sobre el dato.
    /// </summary>
    [Fact]
    public async Task AStudentRegistersWithEmailNameAndSurnameAndWithoutChoosingAPassword()
    {
        // La superficie del tipo: exactamente tres campos y ninguno de contraseña.
        var fields = typeof(AccountRegistrationRequest).GetProperties().Select(p => p.Name).Order().ToArray();
        Assert.Equal(["Email", "FirstName", "LastName"], fields);
        Assert.DoesNotContain(fields, name => name.Contains("Password", StringComparison.OrdinalIgnoreCase));

        var (registration, id) = await RegisterStudentAsync();

        Assert.Equal(nameof(AccountStatus.Pending), registration.Status);
        Assert.Equal(StudentEmail, registration.Email);

        // Y en el almacén: `Pending`, **credencial nula** y marca en falso.
        Assert.Equal(nameof(AccountStatus.Pending), await StatusOfAsync(StudentEmail));
        Assert.Null(await StoredHashOfAsync(StudentEmail));
        Assert.False(await MarkOfAsync(StudentEmail));
        Assert.NotEqual(Guid.Empty, id);
    }

    /// <summary>`Contracts CU-02` — el correo ocupado responde `409` y no declara nada de la cuenta que lo ocupa.</summary>
    [Fact]
    public async Task RegisteringAnAlreadyRegisteredEmailAnswersConflictWithoutRevealingTheAccount()
    {
        await RegisterStudentAsync();

        using var again = await _client.PostAsJsonAsync(
            "/cuentas", new AccountRegistrationRequest(StudentEmail.ToUpperInvariant(), "Otra", "Persona"));

        Assert.Equal(HttpStatusCode.Conflict, again.StatusCode);

        var body = await again.Content.ReadAsStringAsync();
        Assert.Contains(ErrorCode.EmailAlreadyRegistered, body, StringComparison.Ordinal);
        Assert.DoesNotContain(nameof(AccountStatus.Pending), body, StringComparison.Ordinal);
        Assert.DoesNotContain(nameof(Role.Student), body, StringComparison.Ordinal);
    }

    // ---- CRITERIO 2 · la cuenta `Pendiente` recibe aviso explícito ---------------------------

    /// <summary>
    /// RN-06 — una cuenta `Pending` que intenta entrar recibe `403` **con motivo**, distinto del
    /// rechazo genérico por credencial inválida, para que sepa en qué situación está.
    /// </summary>
    [Fact]
    public async Task ThePendingAccountIsToldItHasNotBeenEnabledYet()
    {
        await ConfigureAdministratorAsync();
        await RegisterStudentAsync();

        using var exchange = await _client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, "cualquier-cosa"));

        Assert.Equal(HttpStatusCode.Forbidden, exchange.StatusCode);

        var error = await exchange.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.Equal(ErrorCode.AccountNotEnabled, error!.Code);
        Assert.Contains("habilite", error.Message, StringComparison.OrdinalIgnoreCase);

        // Y NO es el rechazo genérico de credenciales, que es lo que el criterio distingue.
        Assert.NotEqual(ErrorCode.InvalidCredentials, error.Code);
    }

    // ---- CRITERIOS 3 y 4 · habilitar, bloquear, rehabilitar, dar de baja; y la provisoria ----

    /// <summary>
    /// EL RECORRIDO ENTERO DE `A-07` A `A-01`: el administrador habilita, el producto le muestra
    /// **una provisoria que él no escribió**, el alumno entra con ella y **queda obligado a
    /// cambiarla** antes de llegar a ninguna otra ruta.
    /// </summary>
    [Fact]
    public async Task EnablingProducesAProvisionalTheAdministratorDidNotWriteAndForcesItsChange()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();

        // LA SOLICITUD NO TIENE CAMPO DE CONTRASEÑA: el administrador no la escribe (RN-14).
        var requestFields = typeof(AccountStatusChangeRequest).GetProperties().Select(p => p.Name).Order().ToArray();
        Assert.Equal(["AccountId", "IntendedStatus"], requestFields);
        Assert.DoesNotContain(requestFields, name => name.Contains("Password", StringComparison.OrdinalIgnoreCase));

        var enabled = await EnableAsync(token, id);
        var provisional = enabled.ProvisionalPassword!;

        Assert.Equal(nameof(AccountStatus.Enabled), enabled.ResultingStatus);
        Assert.True(enabled.MustChangePassword);
        Assert.Equal(12, provisional.Length);

        // LA PROVISORIA NO SE GUARDA EN CLARO, leído del almacén.
        var stored = await StoredHashOfAsync(StudentEmail);
        Assert.NotNull(stored);
        Assert.DoesNotContain(provisional, stored!, StringComparison.Ordinal);
        Assert.StartsWith("PBKDF2-SHA256$", stored!, StringComparison.Ordinal);

        // EL ALUMNO SE AUTENTICA Y NO OBTIENE SESIÓN DE TRABAJO: el canje devuelve el desvío.
        using var diverted = await _client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, provisional));

        Assert.Equal(HttpStatusCode.Forbidden, diverted.StatusCode);
        var divertedBody = await diverted.Content.ReadAsStringAsync();
        Assert.Contains(ErrorCode.PasswordChangeRequired, divertedBody, StringComparison.Ordinal);
        Assert.DoesNotContain("accessToken", divertedBody, StringComparison.OrdinalIgnoreCase);

        // CAMBIA LA PROVISORIA POR `A-05`, presentándola como vigente, y la marca se levanta.
        using var changed = await _client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(provisional, "la-que-elijo-yo", StudentEmail));

        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);
        Assert.False(await MarkOfAsync(StudentEmail));

        // Y RECIÉN AHORA HAY SESIÓN DE TRABAJO.
        using var session = await _client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(StudentEmail, "la-que-elijo-yo"));

        Assert.Equal(HttpStatusCode.OK, session.StatusCode);
        Assert.NotNull((await session.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken);
    }

    /// <summary>
    /// Las cuatro operaciones del administrador, recorridas de punta a punta: habilitar, bloquear,
    /// rehabilitar —que trae **provisoria nueva y distinta**— y dar de baja.
    /// </summary>
    [Fact]
    public async Task TheAdministratorEnablesBlocksReEnablesAndDeletes()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();

        var enabled = await EnableAsync(token, id);
        Assert.Equal(nameof(AccountStatus.Enabled), await StatusOfAsync(StudentEmail));

        var blocked = await ChangeStatusAsync(token, id, AccountStatus.Blocked);
        Assert.Equal(HttpStatusCode.OK, blocked.Status);
        Assert.Equal(nameof(AccountStatus.Blocked), blocked.Body!.ResultingStatus);
        // EL BLOQUEO DEVUELVE **0 PROVISORIAS**: la ausencia es la señal de que no hay nada que comunicar.
        Assert.Null(blocked.Body.ProvisionalPassword);
        Assert.Equal(nameof(AccountStatus.Blocked), await StatusOfAsync(StudentEmail));

        var reEnabled = await EnableAsync(token, id);
        Assert.NotEqual(enabled.ProvisionalPassword, reEnabled.ProvisionalPassword);
        Assert.True(reEnabled.MustChangePassword);

        // LA BAJA EXIGE EL CORREO ESCRITO: con otro no procede y la cuenta sigue entera.
        using var mismatched = await DeleteAsync(token, id, "otra@frre.utn.edu.ar");
        Assert.Equal(HttpStatusCode.BadRequest, mismatched.StatusCode);
        Assert.Contains(
            ErrorCode.ConfirmationMismatch, await mismatched.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Equal(1, await CountOfAsync(StudentEmail));

        using var deleted = await DeleteAsync(token, id, StudentEmail);
        Assert.Equal(HttpStatusCode.NoContent, deleted.StatusCode);
        Assert.Equal(0, await CountOfAsync(StudentEmail));
    }

    /// <summary>
    /// `A-06` — el listado trae situación y marca, y **cero campos con la credencial** en
    /// cualquiera de sus formas. Se mide sobre el tipo y sobre el cuerpo recibido.
    /// </summary>
    [Fact]
    public async Task TheListingCarriesTheStatusAndTheMarkAndNoFormOfTheCredential()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        await EnableAsync(token, id);

        using var request = Authorized(HttpMethod.Get, "/cuentas", token);
        using var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        var listed = await response.Content.ReadFromJsonAsync<AccountListItem[]>();

        Assert.Equal(2, listed!.Length);
        var student = listed.Single(item => item.AccountId == id);
        Assert.Equal(nameof(AccountStatus.Enabled), student.Status);
        Assert.True(student.MustChangePassword);

        // NI EL TIPO NI EL CUERPO LLEVAN LA FORMA GUARDADA DE NINGUNA CONTRASEÑA.
        Assert.DoesNotContain(
            typeof(AccountListItem).GetProperties(),
            property => property.Name.Contains("Password", StringComparison.OrdinalIgnoreCase)
                        && property.Name != nameof(AccountListItem.MustChangePassword));
        Assert.DoesNotContain("PBKDF2", body, StringComparison.Ordinal);
        Assert.DoesNotContain((await StoredHashOfAsync(StudentEmail))!, body, StringComparison.Ordinal);
    }

    // ---- CRITERIO 5 · RN-16, ningún punto acepta correo y contraseña nueva sin credencial ----

    /// <summary>
    /// RN-16 SOBRE LA SUPERFICIE ENTERA, y es el criterio que se rompe **agregando** algo, no
    /// omitiéndolo. Se verifica de dos maneras que se complementan:
    ///
    ///  · **sobre el ensamblado de contratos**: ningún tipo declara una contraseña nueva sin
    ///    declarar también la vigente. El único que lo hacía —la solicitud de establecimiento—
    ///    se retiró con RN-16, y **agregar uno se rechaza aunque compile**;
    ///  · **sobre el comportamiento**: `A-05` sin la vigente no cambia nada, con una vigente
    ///    equivocada tampoco, y **la marca sigue puesta** en los dos casos.
    /// </summary>
    [Fact]
    public async Task NoAccessPointAcceptsAnEmailAndANewPasswordWithoutACredential()
    {
        var offenders = typeof(AccountRegistrationRequest).Assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract)
            .Select(type => new
            {
                Type = type,
                Fields = type.GetProperties().Select(property => property.Name).ToArray(),
            })
            .Where(candidate =>
                candidate.Fields.Any(name => name.Contains("NewPassword", StringComparison.OrdinalIgnoreCase))
                && !candidate.Fields.Any(name => name.Contains("CurrentPassword", StringComparison.OrdinalIgnoreCase)))
            .Select(candidate => candidate.Type.Name)
            .ToArray();

        _output.WriteLine($"Tipos del ensamblado con contraseña nueva y sin vigente: {offenders.Length}");
        Assert.Empty(offenders);

        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        var provisional = (await EnableAsync(token, id)).ProvisionalPassword!;
        var storedBefore = await StoredHashOfAsync(StudentEmail);

        // Sin la vigente: la petición no es utilizable y la respuesta nombra el campo.
        using var withoutCurrent = await _client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(null, "la-que-quiero", StudentEmail));
        Assert.Equal(HttpStatusCode.BadRequest, withoutCurrent.StatusCode);

        // Con una vigente equivocada: `401` neutro y **la marca sigue puesta**.
        using var wrongCurrent = await _client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest("no-es-la-provisoria", "la-que-quiero", StudentEmail));
        Assert.Equal(HttpStatusCode.Unauthorized, wrongCurrent.StatusCode);

        Assert.True(await MarkOfAsync(StudentEmail));
        Assert.Equal(storedBefore, await StoredHashOfAsync(StudentEmail));

        // Y con la provisoria correcta sí procede, para que la prueba no pase por ausencia.
        using var withCurrent = await _client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, "la-que-quiero", StudentEmail));
        Assert.Equal(HttpStatusCode.OK, withCurrent.StatusCode);
    }

    // ---- CRITERIOS 6 y 7 · el reseteo, y dos reseteos con provisorias distintas --------------

    /// <summary>
    /// `A-09` — el panel **no tiene campo de contraseña** y el producto le muestra al
    /// administrador una provisoria que él no escribió; **dos reseteos consecutivos producen
    /// provisorias distintas**, y ninguna es derivable del nombre, del correo ni de la fecha.
    /// </summary>
    [Fact]
    public async Task TwoConsecutiveResetsProduceDistinctProvisionalsDerivableFromNothing()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        await EnableAsync(token, id);

        // LA SOLICITUD DECLARA EXACTAMENTE UN CAMPO, y ninguno de contraseña ni de trabajos.
        var fields = typeof(PasswordResetRequest).GetProperties().Select(p => p.Name).ToArray();
        Assert.Equal(["AccountId"], fields);

        var first = await ResetAsync(token, id);
        var second = await ResetAsync(token, id);

        Assert.NotEqual(first.ProvisionalPassword, second.ProvisionalPassword);
        Assert.True(first.MustChangePassword);
        Assert.True(second.MustChangePassword);

        // NINGUNA SE DERIVA DE LOS DATOS DE LA CUENTA NI DE LA FECHA.
        string[] accountData =
        [
            StudentEmail, "alumna", "frre", StudentFirstName, StudentLastName,
            id.ToString(), DateTimeOffset.UtcNow.ToString("yyyyMMdd"),
        ];

        foreach (var provisional in new[] { first.ProvisionalPassword, second.ProvisionalPassword })
        {
            _output.WriteLine($"provisoria de {provisional.Length} caracteres");
            foreach (var datum in accountData)
            {
                Assert.DoesNotContain(datum, provisional, StringComparison.OrdinalIgnoreCase);
            }
        }

        // Y LA SEGUNDA REEMPLAZA A LA PRIMERA: la vieja ya no sirve para cambiar la contraseña, y
        // la nueva sí. Se mide sobre `A-05` y no sobre el canje, porque con la marca puesta el
        // canje devuelve el desvío **cualquiera sea la contraseña presentada**: la admisibilidad
        // de la cuenta se resuelve antes que la credencial (`Api CU-01` §4), de modo que ahí las
        // dos provisorias se verían iguales y la prueba no distinguiría nada.
        using var withTheOldOne = await _client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(first.ProvisionalPassword, "la-que-quiero", StudentEmail));
        Assert.Equal(HttpStatusCode.Unauthorized, withTheOldOne.StatusCode);
        Assert.True(await MarkOfAsync(StudentEmail));

        using var withTheNewOne = await _client.PostAsJsonAsync(
            "/cuenta/contrasena",
            new OwnPasswordChangeRequest(second.ProvisionalPassword, "la-que-quiero", StudentEmail));
        Assert.Equal(HttpStatusCode.OK, withTheNewOne.StatusCode);
        Assert.False(await MarkOfAsync(StudentEmail));
    }

    // ---- CRITERIO 8 · el reseteo sobre `Bloqueado` y `Pendiente`, y no sobre el administrador -

    /// <summary>
    /// RN-15 — el reseteo **procede sobre `Blocked` y sobre `Pending` sin cambiarles la
    /// situación**, leída del almacén **antes y después**. Sobre la `Pending` que nunca fue
    /// habilitada, **fija** la credencial en lugar de reemplazarla, y **0 respuestas de fallo**
    /// se producen por la ausencia de contraseña previa.
    /// </summary>
    [Theory]
    [InlineData(nameof(AccountStatus.Pending))]
    [InlineData(nameof(AccountStatus.Blocked))]
    public async Task ResettingProceedsOverBlockedAndPendingWithoutChangingTheStatus(string status)
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();

        if (status == nameof(AccountStatus.Blocked))
        {
            await EnableAsync(token, id);
            Assert.Equal(HttpStatusCode.OK, (await ChangeStatusAsync(token, id, AccountStatus.Blocked)).Status);
        }

        // ANTES, leído del almacén.
        Assert.Equal(status, await StatusOfAsync(StudentEmail));
        if (status == nameof(AccountStatus.Pending))
        {
            Assert.Null(await StoredHashOfAsync(StudentEmail));
        }

        var reset = await ResetAsync(token, id);

        // DESPUÉS, leído del almacén: la situación NO cambió, y la credencial quedó fijada.
        Assert.Equal(status, reset.Status);
        Assert.Equal(status, await StatusOfAsync(StudentEmail));
        Assert.NotNull(await StoredHashOfAsync(StudentEmail));
        Assert.True(await MarkOfAsync(StudentEmail));

        // Y la provisoria NO quedó en el almacén en claro.
        Assert.DoesNotContain(
            reset.ProvisionalPassword, (await StoredHashOfAsync(StudentEmail))!, StringComparison.Ordinal);
    }

    /// <summary>
    /// INV-08 — **la superficie rechaza las cinco operaciones sobre la cuenta de administrador**:
    /// el reseteo con su código propio y `409`, y las cuatro del ciclo de vida con el genérico y
    /// el mismo `409`. La cuenta queda **exactamente como estaba**, leída del almacén.
    /// </summary>
    [Fact]
    public async Task TheSurfaceRefusesEveryOperationOverTheAdministratorAccount()
    {
        var token = await ConfigureAdministratorAsync();
        var administratorId = await IdOfAsync(AdministratorEmail);

        var before = (
            Status: await StatusOfAsync(AdministratorEmail),
            Hash: await StoredHashOfAsync(AdministratorEmail),
            Mark: await MarkOfAsync(AdministratorEmail));

        // `A-09` · el reseteo, con su código propio del conjunto cerrado.
        using var reset = await SendAsync(
            Authorized(HttpMethod.Post, $"/cuentas/{administratorId}/reseteo-de-contrasena", token));
        Assert.Equal(HttpStatusCode.Conflict, reset.StatusCode);
        Assert.Contains(
            ErrorCode.ResetNotApplicableToAdministratorAccount,
            await reset.Content.ReadAsStringAsync(),
            StringComparison.Ordinal);

        // `A-07` · habilitar y bloquear, y `A-08` · la baja, aunque el correo de confirmación sea
        // el correcto: lo que no procede es la operación, no la confirmación.
        var enable = await ChangeStatusAsync(token, administratorId, AccountStatus.Enabled);
        var block = await ChangeStatusAsync(token, administratorId, AccountStatus.Blocked);
        using var delete = await DeleteAsync(token, administratorId, AdministratorEmail);

        Assert.Equal(HttpStatusCode.Conflict, enable.Status);
        Assert.Equal(HttpStatusCode.Conflict, block.Status);
        Assert.Equal(HttpStatusCode.Conflict, delete.StatusCode);

        // LA CUENTA NO CAMBIÓ EN NADA, leída del almacén.
        Assert.Equal(before.Status, await StatusOfAsync(AdministratorEmail));
        Assert.Equal(before.Hash, await StoredHashOfAsync(AdministratorEmail));
        Assert.Equal(before.Mark, await MarkOfAsync(AdministratorEmail));
        Assert.Equal(1, await CountOfAsync(AdministratorEmail));

        // Y SIGUE PUDIENDO ENTRAR: INV-08 no es sólo que no la borren, es que sigue gobernando.
        using var session = await _client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(AdministratorEmail, AdministratorPassword));
        Assert.Equal(HttpStatusCode.OK, session.StatusCode);
    }

    // ---- CRITERIO 9 · la cuenta reseteada se autentica y no obtiene sesión de trabajo --------

    /// <summary>
    /// RN-13 e INV-09 sobre la superficie — con la marca puesta, **ninguno de los puntos que
    /// exigen acceso responde**, ni siquiera con un acceso obtenido **antes** del reseteo: la
    /// marca corta aunque el acceso siga siendo válido (`Api CU-05` CA-06).
    /// </summary>
    [Fact]
    public async Task WithTheMarkSetNoGuardedPointAnswersEvenWithAnAccessObtainedBeforeTheReset()
    {
        var administratorToken = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        var provisional = (await EnableAsync(administratorToken, id)).ProvisionalPassword!;

        // El alumno cambia la provisoria, entra y obtiene un acceso válido.
        using var changed = await _client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, "la-que-elijo-yo", StudentEmail));
        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);

        var studentToken = await SignInAsync(StudentEmail, "la-que-elijo-yo");

        // Con ese acceso, un punto guardado responde: la prueba no va a pasar por ausencia.
        using var beforeTheReset = await SendAsync(Authorized(HttpMethod.Get, "/cuentas", studentToken));
        Assert.Equal(HttpStatusCode.Forbidden, beforeTheReset.StatusCode);
        Assert.Contains(
            ErrorCode.OperationAdminOnly,
            await beforeTheReset.Content.ReadAsStringAsync(),
            StringComparison.Ordinal);

        // AHORA EL ADMINISTRADOR LE RESETEA LA CONTRASEÑA, y el acceso del alumno sigue vigente.
        await ResetAsync(administratorToken, id);

        using var afterTheReset = await SendAsync(Authorized(HttpMethod.Get, "/cuentas", studentToken));
        Assert.Equal(HttpStatusCode.Forbidden, afterTheReset.StatusCode);

        var body = await afterTheReset.Content.ReadAsStringAsync();
        Assert.Contains(ErrorCode.PasswordChangeRequired, body, StringComparison.Ordinal);
        // UN SOLO CÓDIGO Y SIN NOMBRAR LA OPERACIÓN PEDIDA.
        Assert.DoesNotContain(ErrorCode.OperationAdminOnly, body, StringComparison.Ordinal);
        Assert.DoesNotContain("/cuentas", body, StringComparison.Ordinal);
    }

    /// <summary>
    /// `Api CU-04` CA-05 — con un acceso de papel `Alumno`, los cuatro puntos de administración
    /// responden `403` con el código de facultad, y **0 de ellos modifican nada**.
    /// </summary>
    [Fact]
    public async Task WithAStudentAccessTheFourAdministrationPointsRefuseAndModifyNothing()
    {
        var administratorToken = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        var provisional = (await EnableAsync(administratorToken, id)).ProvisionalPassword!;

        using var changed = await _client.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, "la-que-elijo-yo", StudentEmail));
        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);

        var studentToken = await SignInAsync(StudentEmail, "la-que-elijo-yo");
        var before = (
            Status: await StatusOfAsync(StudentEmail),
            Hash: await StoredHashOfAsync(StudentEmail),
            Count: await CountOfAsync(StudentEmail));

        var refusals = new List<HttpResponseMessage>
        {
            await SendAsync(Authorized(HttpMethod.Get, "/cuentas", studentToken)),
            await SendAsync(Authorized(HttpMethod.Post, $"/cuentas/{id}/situacion", studentToken,
                new AccountStatusChangeRequest(id, nameof(AccountStatus.Blocked)))),
            await SendAsync(Authorized(HttpMethod.Delete, $"/cuentas/{id}", studentToken,
                new AccountDeletionRequest(id, StudentEmail))),
            await SendAsync(Authorized(HttpMethod.Post, $"/cuentas/{id}/reseteo-de-contrasena", studentToken)),
        };

        foreach (var refusal in refusals)
        {
            Assert.Equal(HttpStatusCode.Forbidden, refusal.StatusCode);
            Assert.Contains(
                ErrorCode.OperationAdminOnly, await refusal.Content.ReadAsStringAsync(), StringComparison.Ordinal);
            refusal.Dispose();
        }

        // NADA CAMBIÓ EN EL ALMACÉN.
        Assert.Equal(before.Status, await StatusOfAsync(StudentEmail));
        Assert.Equal(before.Hash, await StoredHashOfAsync(StudentEmail));
        Assert.Equal(before.Count, await CountOfAsync(StudentEmail));
    }

    /// <summary>
    /// `Api CU-02` CA-02 — sin cabecera de autorización, los cuatro puntos responden `401` y en
    /// los cuatro **el almacén queda sin ningún cambio**.
    /// </summary>
    [Fact]
    public async Task WithoutASignedAccessTheFourAdministrationPointsAnswerUnauthorized()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        var before = await StatusOfAsync(StudentEmail);

        var refusals = new List<HttpResponseMessage>
        {
            await _client.GetAsync("/cuentas"),
            await _client.PostAsJsonAsync(
                $"/cuentas/{id}/situacion", new AccountStatusChangeRequest(id, nameof(AccountStatus.Enabled))),
            await SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"/cuentas/{id}")
            {
                Content = JsonContent.Create(new AccountDeletionRequest(id, StudentEmail)),
            }),
            await _client.PostAsync($"/cuentas/{id}/reseteo-de-contrasena", content: null),
        };

        foreach (var refusal in refusals)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, refusal.StatusCode);
            refusal.Dispose();
        }

        Assert.Equal(before, await StatusOfAsync(StudentEmail));
        Assert.Null(await StoredHashOfAsync(StudentEmail));

        // Y con el acceso del administrador el mismo punto sí responde: no pasa por ausencia.
        using var listed = await SendAsync(Authorized(HttpMethod.Get, "/cuentas", token));
        Assert.Equal(HttpStatusCode.OK, listed.StatusCode);
    }

    /// <summary>`Api CU-04` CA-06 — una cuenta que no existe responde `404` en los tres puntos que la referencian.</summary>
    [Fact]
    public async Task AnAccountThatDoesNotExistAnswersNotFound()
    {
        var token = await ConfigureAdministratorAsync();
        var missing = Guid.NewGuid();

        var responses = new List<HttpResponseMessage>
        {
            await SendAsync(Authorized(HttpMethod.Post, $"/cuentas/{missing}/situacion", token,
                new AccountStatusChangeRequest(missing, nameof(AccountStatus.Enabled)))),
            await SendAsync(Authorized(HttpMethod.Delete, $"/cuentas/{missing}", token,
                new AccountDeletionRequest(missing, StudentEmail))),
            await SendAsync(Authorized(HttpMethod.Post, $"/cuentas/{missing}/reseteo-de-contrasena", token)),
        };

        foreach (var response in responses)
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains(
                ErrorCode.StudentNotFound, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
            response.Dispose();
        }
    }

    // ---- CRITERIO 10 · los trabajos sobreviven al reseteo ------------------------------------

    /// <summary>
    /// LO QUE SÍ SE PUEDE VERIFICAR HOY DEL CRITERIO 10, Y LO QUE NO. El criterio pide que la
    /// cuenta reseteada conserve **identidad, situación y todos sus trabajos**, verificado sobre
    /// un alumno con trabajos en tres estados distintos y con sus comentarios.
    ///
    /// **LOS TRABAJOS NO EXISTEN TODAVÍA**: son de la etapa `e`, y no hay tabla, ni tipo de
    /// dominio, ni punto de acceso con el que cargarlos. Inventar un andamiaje que escribiera
    /// filas en una tabla que el producto no declara **no verificaría nada del producto**, de modo
    /// que el criterio queda **declarado como no verificable hasta la etapa `e`** y no se da por
    /// cumplido.
    ///
    /// LO QUE SÍ SE MIDE ACÁ ES LA OTRA MITAD, que es la que hoy tiene sustancia: **identidad y
    /// situación se conservan**, atributo por atributo y leídos del almacén. Y se mide además la
    /// propiedad estructural que sostiene la mitad que falta: **ningún tipo del circuito de
    /// reseteo declara un campo con el que un trabajo pudiera perderse**, que es exactamente la
    /// contracara de la solicitud de baja.
    /// </summary>
    [Fact]
    public async Task TheResetKeepsTheIdentityAndTheStatusAndDeclaresNoWayToLoseAWork()
    {
        var token = await ConfigureAdministratorAsync();
        var (_, id) = await RegisterStudentAsync();
        await EnableAsync(token, id);

        var before = await SnapshotOfAsync(StudentEmail);

        await ResetAsync(token, id);

        var after = await SnapshotOfAsync(StudentEmail);

        // IDENTIDAD Y SITUACIÓN: iguales. Lo único que cambió es la credencial y la marca.
        Assert.Equal(before.Id, after.Id);
        Assert.Equal(before.Email, after.Email);
        Assert.Equal(before.NormalizedEmail, after.NormalizedEmail);
        Assert.Equal(before.FirstName, after.FirstName);
        Assert.Equal(before.LastName, after.LastName);
        Assert.Equal(before.Role, after.Role);
        Assert.Equal(before.Status, after.Status);
        Assert.Equal(before.CreatedAt, after.CreatedAt);
        Assert.NotEqual(before.PasswordHash, after.PasswordHash);

        // LA CONTRACARA ESTRUCTURAL: la solicitud de baja declara su confirmación —y con ella el
        // arrastre— y la de reseteo **no declara ningún campo que referencie trabajos**.
        var resetFields = typeof(PasswordResetRequest).GetProperties().Select(p => p.Name).ToArray();
        var resultFields = typeof(PasswordResetResponse).GetProperties().Select(p => p.Name).ToArray();

        Assert.DoesNotContain(
            resetFields.Concat(resultFields),
            name => name.Contains("Work", StringComparison.OrdinalIgnoreCase)
                    || name.Contains("Trabajo", StringComparison.OrdinalIgnoreCase)
                    || name.Contains("Cascade", StringComparison.OrdinalIgnoreCase));

        Assert.Contains(nameof(AccountDeletionRequest.ConfirmationEmail), typeof(AccountDeletionRequest)
            .GetProperties().Select(p => p.Name));
    }

    // ---- Andamiaje --------------------------------------------------------------------------

    private async Task<string> ConfigureAdministratorAsync()
    {
        using var setup = await _client.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);

        return await SignInAsync(AdministratorEmail, AdministratorPassword);
    }

    private async Task<string> SignInAsync(string email, string password)
    {
        using var exchange = await _client.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(email, password));

        Assert.Equal(HttpStatusCode.OK, exchange.StatusCode);

        return (await exchange.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;
    }

    private async Task<(AccountRegistrationResponse Registration, Guid Id)> RegisterStudentAsync()
    {
        using var response = await _client.PostAsJsonAsync(
            "/cuentas", new AccountRegistrationRequest(StudentEmail, StudentFirstName, StudentLastName));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var registration = (await response.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!;

        return (registration, registration.AccountId);
    }

    private async Task<AccountStatusChangeResponse> EnableAsync(string token, Guid id)
    {
        var changed = await ChangeStatusAsync(token, id, AccountStatus.Enabled);

        Assert.Equal(HttpStatusCode.OK, changed.Status);
        Assert.NotNull(changed.Body!.ProvisionalPassword);

        return changed.Body;
    }

    private async Task<(HttpStatusCode Status, AccountStatusChangeResponse? Body)> ChangeStatusAsync(
        string token, Guid id, AccountStatus intendedStatus)
    {
        using var response = await SendAsync(Authorized(
            HttpMethod.Post, $"/cuentas/{id}/situacion", token,
            new AccountStatusChangeRequest(id, intendedStatus.ToString())));

        var body = response.StatusCode == HttpStatusCode.OK
            ? await response.Content.ReadFromJsonAsync<AccountStatusChangeResponse>()
            : null;

        return (response.StatusCode, body);
    }

    private async Task<PasswordResetResponse> ResetAsync(string token, Guid id)
    {
        using var response = await SendAsync(
            Authorized(HttpMethod.Post, $"/cuentas/{id}/reseteo-de-contrasena", token));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<PasswordResetResponse>())!;
    }

    private Task<HttpResponseMessage> DeleteAsync(string token, Guid id, string confirmationEmail) =>
        SendAsync(Authorized(
            HttpMethod.Delete, $"/cuentas/{id}", token, new AccountDeletionRequest(id, confirmationEmail)));

    private static HttpRequestMessage Authorized(HttpMethod method, string route, string token, object? body = null)
    {
        var request = new HttpRequestMessage(method, route)
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) },
        };

        if (body is not null)
        {
            request.Content = JsonContent.Create(body, body.GetType());
        }

        return request;
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        using (request)
        {
            return await _client.SendAsync(request);
        }
    }

    // ---- Lecturas del almacén, que es de donde se leen las propiedades que no se ven ---------

    private async Task<object?> ScalarAsync(string column, string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = $"select {column} from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        var value = await command.ExecuteScalarAsync();

        return value is DBNull ? null : value;
    }

    private async Task<string?> StatusOfAsync(string email) => (await ScalarAsync("Status", email))?.ToString();

    private async Task<string?> StoredHashOfAsync(string email) => (await ScalarAsync("PasswordHash", email))?.ToString();

    private async Task<bool> MarkOfAsync(string email) =>
        (await ScalarAsync("MustChangePassword", email))?.ToString() == "1";

    private async Task<Guid> IdOfAsync(string email) =>
        Guid.Parse((await ScalarAsync("Id", email))!.ToString()!);

    private async Task<int> CountOfAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Account where NormalizedEmail = $email";
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        return Convert.ToInt32(await command.ExecuteScalarAsync(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private sealed record StoredAccount(
        string Id,
        string Email,
        string NormalizedEmail,
        string FirstName,
        string LastName,
        string Role,
        string Status,
        string? PasswordHash,
        string CreatedAt);

    /// <summary>La cuenta entera, leída del almacén, para comparar atributo por atributo.</summary>
    private async Task<StoredAccount> SnapshotOfAsync(string email)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = """
            select Id, Email, NormalizedEmail, FirstName, LastName, Role, Status, PasswordHash, CreatedAt
            from Account where NormalizedEmail = $email
            """;
        command.Parameters.AddWithValue("$email", EmailIdentity.Normalize(email));

        using var reader = await command.ExecuteReaderAsync();
        Assert.True(await reader.ReadAsync());

        return new StoredAccount(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5),
            reader.GetString(6),
            reader.IsDBNull(7) ? null : reader.GetString(7),
            reader.GetString(8));
    }
}
