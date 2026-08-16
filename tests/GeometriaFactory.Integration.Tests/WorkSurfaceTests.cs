using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Contracts.Errors;
using GeometriaFactory.Contracts.Works;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// EL TRABAJO SOBRE HTTP DE VERDAD Y CONTRA EL ALMACÉN DE VERDAD: los cinco criterios de
/// transición de la etapa `e` que le tocan a la superficie del servicio.
/// </summary>
/// <remarks>
/// NO SE REEMPLAZA NINGÚN SERVICIO (ver <see cref="DataServiceHarness"/>): el repositorio es el
/// de EF Core, el reloj es el del sistema y la guardia del acceso firmado es la del producto. Lo
/// que se verifica es el cableado real.
///
/// LAS PETICIONES SE FUERZAN CONTRA LA SUPERFICIE, QUE ES LO QUE EL INTAKE §17.5.P.6 EXIGE. Que
/// una pantalla no ofrezca el botón no prueba nada: la eliminación fuera de `Borrador` y la
/// eliminación de un trabajo ajeno se piden acá **armando la petición a mano**, con un acceso
/// firmado legítimo, sin pasar por ninguna interfaz.
///
/// LO QUE NO SE VE EN UNA RESPUESTA SE LEE DEL ALMACÉN, con una consulta directa: que el trabajo
/// **sigue existiendo** después de un rechazo, y que el texto guardado es el que se envió.
/// Creerle a la respuesta sobre esas dos cosas sería verificar lo que el producto dice de sí mismo.
///
/// LOS TRABAJOS FUERA DE `Borrador` SE CONSTITUYEN POR LAS TRANSICIONES DEL DOMINIO Y NO POR UN
/// ATAJO SOBRE EL ALMACÉN: la etapa `e` no tiene punto de acceso que saque un trabajo de
/// `Borrador` —el envío que interpreta el texto es de la etapa `f` y el desenlace es de la `h`—,
/// de modo que la preparación entra por el puerto de repositorio, con la entidad llevada al
/// estado por `Submit` y por `ApplyOutcome`. Escribir filas a mano habría verificado el esquema y
/// no el producto.
/// </remarks>
public sealed class WorkSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    /// <summary>
    /// El texto del escenario **`E-2`** del intake §20, con sus **dos comas finales** y su clave
    /// `Tapas`, transcripto carácter por carácter.
    /// </summary>
    /// <remarks>
    /// **[CORRECCIÓN DE LA ETAPA `f`, DECLARADA.]** Hasta hoy esta constante decía llamarse `E-2` y
    /// **no era el texto de `E-2`**: era un objeto con una clave `Figuras`, dimensiones en `Largo`,
    /// `Ancho` y `Alto`, y un `"Tapas": 2` numérico, que ninguna fuente transcribe. La etapa `e` lo
    /// escribió cuando el texto no se interpretaba y ninguna prueba podía notar la diferencia: lo
    /// único que se le pedía era llegar entero al almacén, y para eso cualquier cadena servía.
    ///
    /// Se reemplaza por el texto real porque **la etapa `f` sí lo interpreta**, y una fixture que
    /// dice ser un escenario del intake sin serlo es exactamente lo que la regla de delivery 5 de
    /// §15 prohíbe. Con el texto verdadero esta prueba pasa a ejercer también T1 y T2 de verdad.
    /// </remarks>
    private const string ScenarioE2 = """
        [
        {
          "Tipo": "Ortoedro",
          "Tapas":
          [
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 },
            { "Tipo": "Rectangulo", "Largo": 7.00, "Ancho": 7.00, "Area": 49.00 }
          ],
          "Laterales":
            [
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
              { "Tipo": "Rectangulo", "Largo": 21.00, "Ancho": 7.00, "Area": 147.00 },
            ],
          "Area": 686.00,
          "Volumen": 343.00
        },
        ]
        """;

    /// <summary>
    /// Un texto que **no verifica**: el del escenario `E-5`, con su figura 1 de tipo desconocido.
    /// </summary>
    /// <remarks>
    /// LAS PRUEBAS QUE NECESITAN UN TRABAJO EN `Borrador` NECESITAN UN TEXTO QUE NO VERIFIQUE, y
    /// desde la etapa `f` eso dejó de ser cualquiera. Hasta la `e` alcanzaba con cualquier cadena
    /// porque **ningún texto verificaba**: el validador no existía y `Submit` rechazaba siempre.
    /// Usar un escenario del intake en lugar de inventar un texto roto mantiene la regla de
    /// delivery 5 de §15. **[relevo de la etapa `f`, declarado.]**
    /// </remarks>
    private const string TextThatDoesNotVerify = Scenarios.E5;

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly HttpClient _client;

    public WorkSurfaceTests()
    {
        _dataService = new DataServiceHarness(_storePath);
        _client = _dataService.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    // ---- CRITERIO 1 · el trabajo se carga y recibe identificador y estado --------------------

    /// <summary>
    /// `A-10` — Nombre, fecha, descripción y texto entran, y salen **identificador propio y
    /// estado**. El texto queda guardado **idéntico carácter por carácter**, con sus dos comas
    /// finales, que es donde RN-08 se rompe sin que nada falle.
    /// </summary>
    [Fact]
    public async Task AWorkIsLoadedAndGetsItsOwnIdentifierAndStatus()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Post, "/trabajos", world.StudentToken,
            new WorkSubmissionRequest(null, "Entrega 1", "2026-08-09", "un ortoedro", ScenarioE2)));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = (await response.Content.ReadFromJsonAsync<WorkSubmissionResponse>())!;

        Assert.NotEqual(Guid.Empty, body.WorkId);

        // **[relevo de la etapa `f`.]** Con el texto real de `E-2` el trabajo PASA A `Pendiente`
        // con su advertencia asociada, que es lo que §20.E-2 punto 7 declara. Hasta la etapa `e`
        // acá decía `Borrador`, y era cierto por una razón que ya no vale: nadie interpretaba.
        Assert.Equal(nameof(WorkStatus.Submitted), body.Status);
        var warning = Assert.Single(body.Observations);
        Assert.Equal(nameof(ObservationKind.Warning), warning.Kind);
        Assert.Equal("Volumen", warning.Field);
        Assert.Equal(343.00, warning.DeclaredValue);
        Assert.Equal(1029.00, warning.DerivedValue!.Value, 2);
        Assert.NotEqual(default, body.RegisteredAt);

        // EL TEXTO SE LEE DEL ALMACÉN Y NO DE LA RESPUESTA.
        var stored = await StoredTextAsync(body.WorkId);
        Assert.Equal(ScenarioE2, stored);
        Assert.Equal(2, CountTrailingCommas(stored!));

        // Y el dueño es el solicitante, sin que ningún campo de la solicitud lo haya elegido.
        Assert.Equal(world.StudentId, await StoredOwnerAsync(body.WorkId));
    }

    // ---- LOS DOS DESENLACES DEL ENVÍO, QUE SON LO QUE LA TRANSICIÓN `f` -> `g` PIDE ----------

    /// <summary>
    /// `E-1`, el JSON semilla: el envío **verifica** y el trabajo pasa a `Pendiente`, con sus dos
    /// advertencias y el texto intacto.
    /// </summary>
    [Fact]
    public async Task AWorkWhoseTextVerifiesGoesToSubmittedWithItsWarnings()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Post,
            "/trabajos",
            world.StudentToken,
            new WorkSubmissionRequest(null, "Entrega 1", "2026-08-09", "el semilla", Scenarios.E1)));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = (await response.Content.ReadFromJsonAsync<WorkSubmissionResponse>())!;

        // TRES PIEZAS Y DOS ADVERTENCIAS, que es el resultado canónico del producto, visto desde
        // afuera y sobre HTTP real.
        Assert.Equal(nameof(WorkStatus.Submitted), body.Status);
        Assert.Equal(2, body.Observations.Count);
        Assert.All(body.Observations, o => Assert.Equal(nameof(ObservationKind.Warning), o.Kind));

        var area = Assert.Single(body.Observations, o => o.Field == "Area");
        Assert.Equal(1, area.PiecePosition);
        Assert.Equal(36.00, area.DeclaredValue);
        Assert.Equal(54.00, area.DerivedValue!.Value, 2);

        var volume = Assert.Single(body.Observations, o => o.Field == "Volumen");
        Assert.Equal(2, volume.PiecePosition);
        Assert.Equal(343.00, volume.DeclaredValue);
        Assert.Equal(1029.00, volume.DerivedValue!.Value, 2);

        // Y EL TEXTO SIGUE SIENDO EL DEL ALUMNO, carácter por carácter.
        Assert.Equal(Scenarios.E1, await StoredTextAsync(body.WorkId));
    }

    /// <summary>
    /// `E-5`, el tipo desconocido: el envío **no verifica**, el trabajo queda en `Borrador` y el
    /// error viene **ubicado en la figura 1 y en el campo `Tipo`**.
    /// </summary>
    [Fact]
    public async Task AWorkWhoseTextDoesNotVerifyStaysInDraftWithItsErrorLocated()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Post,
            "/trabajos",
            world.StudentToken,
            new WorkSubmissionRequest(null, "Entrega 2", "2026-08-09", "con una pirámide", Scenarios.E5)));

        // NO ES UN RECHAZO DE LA PETICIÓN, y es la confusión más cara de esta frontera: el trabajo
        // se guardó entero y lo que no verificó es su texto.
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = (await response.Content.ReadFromJsonAsync<WorkSubmissionResponse>())!;

        Assert.Equal(nameof(WorkStatus.Draft), body.Status);

        var error = Assert.Single(body.Observations);
        Assert.Equal(nameof(ObservationKind.ValidationError), error.Kind);
        Assert.Equal(1, error.PiecePosition);
        Assert.Equal("Tipo", error.Field);

        // EL ÍNDICE ES 1 Y NO 0: la primera figura del escenario es válida a propósito, y es lo
        // que comprueba que la ubicación se calcula en lugar de informar siempre la primera.
        Assert.Equal(Scenarios.E5, await StoredTextAsync(body.WorkId));
    }

    /// <summary>Falta el nombre: `400` nombrando el campo ausente, y nada se guarda.</summary>
    [Fact]
    public async Task AWorkWithoutANameIsRejectedNamingTheField()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Post, "/trabajos", world.StudentToken,
            new WorkSubmissionRequest(null, "", "2026-08-09", null, ScenarioE2)));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var error = (await response.Content.ReadFromJsonAsync<ErrorResponse>())!;
        Assert.Equal(ErrorCode.RequiredFieldMissing, error.Code);
        Assert.Contains(error.Details, detail => detail.Field == nameof(WorkSubmissionRequest.Name));

        Assert.Equal(0, await CountWorksAsync());
    }

    /// <summary>
    /// `A-10` y `A-11` exigen papel `Alumno`, y la negativa **no se resuelve escondiendo un botón**.
    /// </summary>
    [Fact]
    public async Task TheAdministratorCannotLoadAWork()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Post, "/trabajos", world.AdministratorToken,
            new WorkSubmissionRequest(null, "Entrega 1", "2026-08-09", null, ScenarioE2)));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.Equal(0, await CountWorksAsync());
    }

    // ---- CRITERIO 2 · queda en `Borrador` con el texto inválido y se reedita -----------------

    /// <summary>
    /// UN TEXTO QUE NO SIRVE **NO ES UN FALLO DE LA PETICIÓN**: se responde con éxito, el trabajo
    /// queda en `Borrador` con su texto conservado, y se reedita por `A-11` cuantas veces haga
    /// falta. Es la confusión más cara de esta capa y acá se mide que no ocurre.
    /// </summary>
    [Fact]
    public async Task AnInvalidTextLeavesTheWorkInDraftAndItIsEdited()
    {
        var world = await WorldAsync();

        const string Broken = "esto no es notación de objetos: es lo que salió del programa";

        var created = await LoadAsync(world.StudentToken, "Entrega 1", Broken);

        Assert.Equal(nameof(WorkStatus.Draft), created.Status);
        Assert.Equal(Broken, await StoredTextAsync(created.WorkId));

        // LA REEDICIÓN: el alumno corrige y vuelve a enviar. **Desde la etapa `f` el texto
        // corregido se interpreta y el trabajo pasa a `Pendiente`**, que es el circuito completo
        // que la persona vive: envía, ve qué falló, corrige y entrega. Hasta la etapa `e` esta
        // prueba terminaba en `Borrador` porque nadie interpretaba. **[relevo declarado.]**
        using var edited = await SendAsync(Authorized(
            HttpMethod.Post, $"/trabajos/{created.WorkId}", world.StudentToken,
            new WorkSubmissionRequest(created.WorkId, "Entrega 1 corregida", "2026-08-10", "ya va", ScenarioE2)));

        Assert.Equal(HttpStatusCode.OK, edited.StatusCode);

        var body = (await edited.Content.ReadFromJsonAsync<WorkSubmissionResponse>())!;
        Assert.Equal(created.WorkId, body.WorkId);
        Assert.Equal(nameof(WorkStatus.Submitted), body.Status);

        // El texto guardado es el nuevo, carácter por carácter, y sigue habiendo UN solo trabajo.
        Assert.Equal(ScenarioE2, await StoredTextAsync(created.WorkId));
        Assert.Equal(1, await CountWorksAsync());
    }

    /// <summary>
    /// La reedición forzada sobre un trabajo que ya no está en `Borrador` responde `409`
    /// **declarando el estado actual**, y **no reemplaza el texto guardado**.
    /// </summary>
    [Fact]
    public async Task ForcingAnEditOutsideDraftIsRejectedAndKeepsTheStoredText()
    {
        var world = await WorldAsync();
        var workId = await SeedWorkAsync(world.StudentId, "Entrega enviada", ScenarioE2, WorkStatus.Submitted);

        using var response = await SendAsync(Authorized(
            HttpMethod.Post, $"/trabajos/{workId}", world.StudentToken,
            new WorkSubmissionRequest(workId, "otro nombre", "2026-08-11", null, "{ \"reemplazo\": true }")));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var error = (await response.Content.ReadFromJsonAsync<ErrorResponse>())!;
        Assert.Equal(ErrorCode.StateForbidsUpdate, error.Code);
        Assert.Contains("Pendiente", error.Message, StringComparison.Ordinal);

        Assert.Equal(ScenarioE2, await StoredTextAsync(workId));
    }

    // ---- CRITERIO 3 · la eliminación del alumno, forzando la petición ------------------------

    /// <summary>
    /// EL ALUMNO ELIMINA SU BORRADOR: `204` y el trabajo deja de existir en el almacén.
    /// </summary>
    [Fact]
    public async Task TheStudentDeletesTheirOwnDraft()
    {
        var world = await WorldAsync();
        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var response = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{created.WorkId}", world.StudentToken));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(0, await CountWorksAsync());
    }

    /// <summary>
    /// CRITERIO 3, PRIMERA MITAD — **Sólo procede en `Borrador`**, verificado **forzando la
    /// petición al servicio de datos**: se arma el `DELETE` a mano, con un acceso firmado
    /// legítimo del dueño, sin pasar por ninguna pantalla. Los tres estados que no son `Borrador`
    /// responden `409` declarando el estado, y el trabajo **sigue existiendo**.
    /// </summary>
    [Theory]
    [InlineData(WorkStatus.Submitted, "Pendiente")]
    [InlineData(WorkStatus.Approved, "Finalizado")]
    [InlineData(WorkStatus.Rejected, "Rechazado")]
    public async Task ForcingTheDeletionOutsideDraftIsRejectedAndTheWorkSurvives(
        WorkStatus status, string label)
    {
        var world = await WorldAsync();
        var workId = await SeedWorkAsync(world.StudentId, "Entrega", TextThatDoesNotVerify, status);

        using var response = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{workId}", world.StudentToken));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var error = (await response.Content.ReadFromJsonAsync<ErrorResponse>())!;
        Assert.Equal(ErrorCode.StateForbidsDelete, error.Code);

        // «La respuesta declara el estado actual», y lo declara con la ETIQUETA que ve la persona.
        Assert.Contains(label, error.Message, StringComparison.Ordinal);

        // EL TRABAJO SIGUE ADENTRO. Se lee del almacén y no de la respuesta.
        Assert.Equal(1, await CountWorksAsync());
        Assert.Equal(status.ToString(), await StoredStatusAsync(workId));
    }

    /// <summary>
    /// CRITERIO 3, SEGUNDA MITAD — **Sólo sobre trabajos propios**, forzando la petición. El
    /// alumno B conoce el identificador del borrador del alumno A y pide eliminarlo: la respuesta
    /// es **exactamente igual** que ante un identificador inexistente, y el trabajo sigue ahí.
    /// </summary>
    [Fact]
    public async Task ForcingTheDeletionOfAnotherStudentsWorkAnswersLikeAMissingOne()
    {
        var world = await WorldAsync();
        var other = await EnrolStudentAsync(world.AdministratorToken, "otro@frre.utn.edu.ar");

        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var foreign = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{created.WorkId}", other.Token));
        using var missing = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{Guid.NewGuid()}", other.Token));

        Assert.Equal(HttpStatusCode.NotFound, foreign.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, missing.StatusCode);

        // CUERPOS IDÉNTICOS SALVO EL MOMENTO: 0 campos permiten distinguir los dos casos.
        var foreignBody = (await foreign.Content.ReadFromJsonAsync<ErrorResponse>())!;
        var missingBody = (await missing.Content.ReadFromJsonAsync<ErrorResponse>())!;

        Assert.Equal(missingBody.Code, foreignBody.Code);
        Assert.Equal(missingBody.Message, foreignBody.Message);
        Assert.Equal(missingBody.Details.Count, foreignBody.Details.Count);
        Assert.Equal(ErrorCode.WorkNotFound, foreignBody.Code);

        // Y NO ES `403`, QUE ES EL PUNTO: `403` confirmaría que ese trabajo existe.
        Assert.NotEqual(HttpStatusCode.Forbidden, foreign.StatusCode);

        Assert.Equal(1, await CountWorksAsync());
    }

    /// <summary>
    /// EL ADMINISTRADOR ELIMINA EN LOS TRES ESTADOS QUE VE, por el mismo punto y con la misma
    /// petición: lo que cambia es la regla que lo acota, y la regla vive adentro.
    /// </summary>
    [Theory]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public async Task TheAdministratorDeletesTheThreeStatusesTheySee(WorkStatus status)
    {
        var world = await WorldAsync();
        var workId = await SeedWorkAsync(world.StudentId, "Entrega", TextThatDoesNotVerify, status);

        using var response = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{workId}", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(0, await CountWorksAsync());
    }

    /// <summary>
    /// Y NO ELIMINA UN BORRADOR AJENO, porque no lo ve: responde igual que ante un identificador
    /// inexistente (RN-11 + RN-03).
    /// </summary>
    [Fact]
    public async Task TheAdministratorCannotDeleteADraft()
    {
        var world = await WorldAsync();
        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var response = await SendAsync(
            Authorized(HttpMethod.Delete, $"/trabajos/{created.WorkId}", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(1, await CountWorksAsync());
    }

    // ---- CRITERIO 4 · el trabajo de otro devuelve «no encontrado» ----------------------------

    /// <summary>
    /// CRITERIO 4 — Un alumno que **pide** el trabajo de otro recibe «no encontrado», con el
    /// mismo cuerpo que ante un identificador inexistente. **No es cortesía**: `403` confirmaría
    /// que ese trabajo existe, y esa información es la que RN-03 existe para no dar.
    /// </summary>
    [Fact]
    public async Task AStudentAskingForAnotherStudentsWorkGetsNotFound()
    {
        var world = await WorldAsync();
        var other = await EnrolStudentAsync(world.AdministratorToken, "otro@frre.utn.edu.ar");

        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var foreign = await SendAsync(
            Authorized(HttpMethod.Get, $"/trabajos/{created.WorkId}", other.Token));
        using var missing = await SendAsync(
            Authorized(HttpMethod.Get, $"/trabajos/{Guid.NewGuid()}", other.Token));

        Assert.Equal(HttpStatusCode.NotFound, foreign.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, missing.StatusCode);

        var foreignBody = (await foreign.Content.ReadFromJsonAsync<ErrorResponse>())!;
        var missingBody = (await missing.Content.ReadFromJsonAsync<ErrorResponse>())!;

        Assert.Equal(ErrorCode.WorkNotFound, foreignBody.Code);
        Assert.Equal(missingBody.Code, foreignBody.Code);
        Assert.Equal(missingBody.Message, foreignBody.Message);

        // EL CUERPO NO NOMBRA AL DUEÑO, NI SU CORREO, NI EL NOMBRE DEL TRABAJO.
        var raw = await foreign.Content.ReadAsStringAsync();
        Assert.DoesNotContain("alumna@frre.utn.edu.ar", raw, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Entrega 1", raw, StringComparison.Ordinal);
    }

    /// <summary>El dueño sí ve su trabajo entero, con el texto íntegro.</summary>
    [Fact]
    public async Task TheOwnerSeesTheirWorkWithTheWholeText()
    {
        var world = await WorldAsync();
        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var response = await SendAsync(
            Authorized(HttpMethod.Get, $"/trabajos/{created.WorkId}", world.StudentToken));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var detail = (await response.Content.ReadFromJsonAsync<WorkDetailResponse>())!;

        Assert.Equal(created.WorkId, detail.WorkId);
        Assert.Equal(TextThatDoesNotVerify, detail.OriginalJson);
        Assert.Equal(nameof(WorkStatus.Draft), detail.Status);
        Assert.Equal("alumna@frre.utn.edu.ar", detail.OwnerEmail);
        Assert.Null(detail.AdministratorComment);
    }

    /// <summary>
    /// EL ADMINISTRADOR NO ABRE UN BORRADOR AJENO, y su respuesta es la misma que la de un
    /// identificador inexistente (`Api CU-07` CA-08).
    /// </summary>
    [Fact]
    public async Task TheAdministratorOpeningADraftGetsNotFound()
    {
        var world = await WorldAsync();
        var created = await LoadAsync(world.StudentToken, "Entrega 1", TextThatDoesNotVerify);

        using var draft = await SendAsync(
            Authorized(HttpMethod.Get, $"/trabajos/{created.WorkId}", world.AdministratorToken));
        using var missing = await SendAsync(
            Authorized(HttpMethod.Get, $"/trabajos/{Guid.NewGuid()}", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.NotFound, draft.StatusCode);

        var draftBody = (await draft.Content.ReadFromJsonAsync<ErrorResponse>())!;
        var missingBody = (await missing.Content.ReadFromJsonAsync<ErrorResponse>())!;

        Assert.Equal(missingBody.Code, draftBody.Code);
        Assert.Equal(missingBody.Message, draftBody.Message);
    }

    // ---- CRITERIO 5 · el administrador agrupa y filtra, y no ve borradores -------------------

    /// <summary>
    /// CRITERIO 5 — El listado del administrador trae los trabajos de la comisión **agrupados por
    /// alumno**, y **NO incluye los que están en `Borrador`**. Se prueba con un borrador ajeno
    /// existiendo en el almacén y ausente del listado.
    /// </summary>
    [Fact]
    public async Task TheAdministratorListingIsGroupedByStudentAndExcludesDrafts()
    {
        var world = await WorldAsync();
        var second = await EnrolStudentAsync(world.AdministratorToken, "zulema@frre.utn.edu.ar");

        // Alumna A: un borrador y un enviado. Alumno Z: un borrador y un finalizado.
        var draftOfA = await LoadAsync(world.StudentToken, "Borrador de A", TextThatDoesNotVerify);
        await SeedWorkAsync(world.StudentId, "Enviado de A", ScenarioE2, WorkStatus.Submitted);
        var draftOfZ = await LoadAsync(second.Token, "Borrador de Z", TextThatDoesNotVerify);
        await SeedWorkAsync(second.Id, "Finalizado de Z", ScenarioE2, WorkStatus.Approved);

        // Los dos borradores EXISTEN en el almacén: si no existieran, el criterio no probaría nada.
        Assert.Equal(4, await CountWorksAsync());
        Assert.Equal(nameof(WorkStatus.Draft), await StoredStatusAsync(draftOfA.WorkId));
        Assert.Equal(nameof(WorkStatus.Draft), await StoredStatusAsync(draftOfZ.WorkId));

        using var response = await SendAsync(
            Authorized(HttpMethod.Get, "/trabajos", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var listing = (await response.Content.ReadFromJsonAsync<WorkListItem[]>())!;

        Assert.Equal(2, listing.Length);
        Assert.DoesNotContain(listing, item => item.Status == nameof(WorkStatus.Draft));
        Assert.DoesNotContain(listing, item => item.WorkId == draftOfA.WorkId || item.WorkId == draftOfZ.WorkId);

        // AGRUPADOS POR ALUMNO: los elementos del mismo dueño llegan contiguos, y por eso el
        // consumidor puede agrupar sin reordenar.
        var owners = listing.Select(item => item.OwnerId).ToArray();
        Assert.Equal(owners.Distinct().Count(), owners.Length == 0 ? 0 : CountRuns(owners));

        // Y CADA ELEMENTO TRAE EL DATO DE DUEÑO, sin una segunda solicitud.
        Assert.All(listing, item => Assert.False(string.IsNullOrWhiteSpace(item.OwnerEmail)));
    }

    /// <summary>El filtro por alumno acota, y el recorte de borradores sigue rigiendo dentro.</summary>
    [Fact]
    public async Task TheStudentFilterNarrowsWithinTheAdministratorScope()
    {
        var world = await WorldAsync();
        var second = await EnrolStudentAsync(world.AdministratorToken, "zulema@frre.utn.edu.ar");

        await LoadAsync(world.StudentToken, "Borrador de A", TextThatDoesNotVerify);
        await SeedWorkAsync(world.StudentId, "Enviado de A", ScenarioE2, WorkStatus.Submitted);
        await SeedWorkAsync(second.Id, "Enviado de Z", ScenarioE2, WorkStatus.Submitted);

        using var response = await SendAsync(Authorized(
            HttpMethod.Get, $"/trabajos?alumno={world.StudentId}", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var listing = (await response.Content.ReadFromJsonAsync<WorkListItem[]>())!;

        var only = Assert.Single(listing);
        Assert.Equal(world.StudentId, only.OwnerId);
        Assert.Equal(nameof(WorkStatus.Submitted), only.Status);
    }

    /// <summary>El filtro por un alumno que no existe responde `404`, y no una colección vacía.</summary>
    [Fact]
    public async Task AFilterOnAMissingStudentAnswersNotFound()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(Authorized(
            HttpMethod.Get, $"/trabajos?alumno={Guid.NewGuid()}", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var error = (await response.Content.ReadFromJsonAsync<ErrorResponse>())!;
        Assert.Equal(ErrorCode.StudentNotFound, error.Code);
    }

    /// <summary>
    /// EL ALUMNO VE SUS CUATRO ESTADOS, BORRADORES INCLUIDOS, y **0** trabajos del otro alumno.
    /// El parámetro de filtro **se ignora** para él: no hay valor que le amplíe el alcance.
    /// </summary>
    [Fact]
    public async Task TheStudentListingCoversTheirFourStatusesAndIgnoresTheFilter()
    {
        var world = await WorldAsync();
        var second = await EnrolStudentAsync(world.AdministratorToken, "zulema@frre.utn.edu.ar");

        await LoadAsync(world.StudentToken, "Borrador", TextThatDoesNotVerify);
        await SeedWorkAsync(world.StudentId, "Enviado", ScenarioE2, WorkStatus.Submitted);
        await SeedWorkAsync(world.StudentId, "Finalizado", ScenarioE2, WorkStatus.Approved);
        await SeedWorkAsync(world.StudentId, "Rechazado", ScenarioE2, WorkStatus.Rejected);
        await SeedWorkAsync(second.Id, "De la otra", ScenarioE2, WorkStatus.Submitted);

        using var response = await SendAsync(Authorized(
            HttpMethod.Get, $"/trabajos?alumno={second.Id}", world.StudentToken));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var listing = (await response.Content.ReadFromJsonAsync<WorkListItem[]>())!;

        Assert.Equal(4, listing.Length);
        Assert.All(listing, item => Assert.Equal(world.StudentId, item.OwnerId));
        Assert.Equal(4, listing.Select(item => item.Status).Distinct().Count());
    }

    /// <summary>
    /// LA PROYECCIÓN DE LISTADO NO ARRASTRA EL TEXTO ORIGINAL, y se comprueba sobre el tipo del
    /// contrato y sobre el cuerpo emitido: es un requisito estructural (intake §17.4.P.10).
    /// </summary>
    [Fact]
    public async Task TheListingProjectionCarriesNoOriginalText()
    {
        var world = await WorldAsync();
        await SeedWorkAsync(world.StudentId, "Entrega", ScenarioE2, WorkStatus.Submitted);

        // Sobre el tipo: 0 campos de texto original, de pieza, de componente y de comentario.
        var fields = typeof(WorkListItem).GetProperties().Select(property => property.Name).ToArray();

        Assert.DoesNotContain(fields, name =>
            name.Contains("Json", StringComparison.OrdinalIgnoreCase)
            || name.Contains("Piece", StringComparison.OrdinalIgnoreCase)
            || name.Contains("Component", StringComparison.OrdinalIgnoreCase)
            || name.Contains("Comment", StringComparison.OrdinalIgnoreCase));

        // Y sobre el cuerpo: el texto del alumno no aparece.
        using var response = await SendAsync(
            Authorized(HttpMethod.Get, "/trabajos", world.AdministratorToken));

        var raw = await response.Content.ReadAsStringAsync();
        Assert.DoesNotContain("Ortoedro", raw, StringComparison.Ordinal);
    }

    /// <summary>Un listado vacío es `200` con 0 elementos, y no un código de fallo.</summary>
    [Fact]
    public async Task AnEmptyListingIsASuccess()
    {
        var world = await WorldAsync();

        using var response = await SendAsync(
            Authorized(HttpMethod.Get, "/trabajos", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Empty((await response.Content.ReadFromJsonAsync<WorkListItem[]>())!);
    }

    // ---- La guardia sigue alcanzando a los cinco puntos nuevos --------------------------------

    /// <summary>
    /// LOS CINCO PUNTOS EXIGEN ACCESO FIRMADO. Se mide punto por punto, porque el modo de falla
    /// característico de esta clase de guardia es **no alcanzar a alguno**.
    /// </summary>
    [Fact]
    public async Task TheFiveWorkEndpointsRequireASignedAccess()
    {
        (HttpMethod Method, string Route)[] points =
        [
            (HttpMethod.Post, "/trabajos"),
            (HttpMethod.Get, "/trabajos"),
            (HttpMethod.Post, $"/trabajos/{Guid.NewGuid()}"),
            (HttpMethod.Get, $"/trabajos/{Guid.NewGuid()}"),
            (HttpMethod.Delete, $"/trabajos/{Guid.NewGuid()}"),
        ];

        foreach (var (method, route) in points)
        {
            using var request = new HttpRequestMessage(method, route);
            using var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    /// <summary>
    /// LA MARCA DE CAMBIO DE CONTRASEÑA PENDIENTE CORTA ANTES QUE CUALQUIER OTRA COSA, también
    /// sobre estos cinco puntos: una cuenta marcada **no lee ni escribe nada** (INV-09).
    /// </summary>
    /// <remarks>
    /// LA MARCA SE PONE **DESPUÉS** DE EMITIDO EL ACCESO, y ése es el caso que hay que probar: un
    /// acceso ya emitido no se invalida solo, y si la comprobación viviera en sus reclamos el
    /// alumno reseteado seguiría operando hasta que venciera. El otro camino —una cuenta marcada
    /// pidiendo un acceso— no llega ni a tener con qué golpear estos puntos, porque `A-01` no le
    /// emite ninguno (RN-13, `Api CU-01` §6), y esa mitad ya está cubierta por la batería de la
    /// etapa `d`.
    ///
    /// EL CRITERIO SE MIDE SOBRE EL ALMACÉN Y NO SOBRE LA RESPUESTA: lo que importa no es el
    /// número que volvió sino que **no se escribió nada**.
    /// </remarks>
    [Fact]
    public async Task AMarkedAccountReachesNoWorkEndpoint()
    {
        var world = await WorldAsync();

        // El acceso se emite con la cuenta sana y sigue siendo válido después del reseteo.
        using var reset = await SendAsync(Authorized(
            HttpMethod.Post, $"/cuentas/{world.StudentId}/reseteo-de-contrasena", world.AdministratorToken));

        Assert.Equal(HttpStatusCode.OK, reset.StatusCode);

        using var response = await SendAsync(Authorized(
            HttpMethod.Post, "/trabajos", world.StudentToken,
            new WorkSubmissionRequest(null, "Entrega 1", "2026-08-09", null, ScenarioE2)));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        var error = (await response.Content.ReadFromJsonAsync<ErrorResponse>())!;
        Assert.Equal(ErrorCode.PasswordChangeRequired, error.Code);

        Assert.Equal(0, await CountWorksAsync());
    }

    // ---- Andamiaje de escenario ---------------------------------------------------------------

    private sealed record World(string AdministratorToken, string StudentToken, Guid StudentId);

    private sealed record Student(Guid Id, string Token, string ProvisionalPassword);

    /// <summary>Un laboratorio con administrador y una alumna habilitada, lista para operar.</summary>
    private async Task<World> WorldAsync()
    {
        var administrator = await ConfigureAdministratorAsync();
        var student = await EnrolStudentAsync(administrator, "alumna@frre.utn.edu.ar");

        return new World(administrator, student.Token, student.Id);
    }

    private async Task<string> ConfigureAdministratorAsync()
    {
        using var setup = await SendAsync(new HttpRequestMessage(HttpMethod.Post, "/cuentas/administrador")
        {
            Content = JsonContent.Create(new AdministratorSetupRequest(
                AdministratorEmail, "Fernando", "Filipuzzi", AdministratorPassword)),
        });

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);

        return await TokenAsync(AdministratorEmail, AdministratorPassword);
    }

    /// <summary>
    /// Una alumna registrada, habilitada y **con su contraseña ya elegida**: sin ese último paso
    /// la marca de INV-09 le cerraría todos los puntos, que es lo que otra prueba verifica aparte.
    /// </summary>
    private async Task<Student> EnrolStudentAsync(string administratorToken, string email)
    {
        var registered = await RegisterAndEnableAsync(administratorToken, email);

        const string Chosen = "la-que-eligio-la-alumna";

        using var change = await SendAsync(new HttpRequestMessage(HttpMethod.Post, "/cuenta/contrasena")
        {
            Content = JsonContent.Create(
                new OwnPasswordChangeRequest(registered.ProvisionalPassword, Chosen, email)),
        });

        Assert.Equal(HttpStatusCode.OK, change.StatusCode);

        return registered with { Token = await TokenAsync(email, Chosen) };
    }

    private async Task<Student> RegisterAndEnableAsync(string administratorToken, string email)
    {
        using var registration = await SendAsync(new HttpRequestMessage(HttpMethod.Post, "/cuentas")
        {
            Content = JsonContent.Create(new AccountRegistrationRequest(email, "Ana", "Diaz")),
        });

        Assert.Equal(HttpStatusCode.Created, registration.StatusCode);

        var id = (await registration.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!.AccountId;

        using var enabled = await SendAsync(Authorized(
            HttpMethod.Post, $"/cuentas/{id}/situacion", administratorToken,
            new AccountStatusChangeRequest(id, nameof(AccountStatus.Enabled))));

        Assert.Equal(HttpStatusCode.OK, enabled.StatusCode);

        var provisional = (await enabled.Content.ReadFromJsonAsync<AccountStatusChangeResponse>())!
            .ProvisionalPassword!;

        return new Student(id, string.Empty, provisional);
    }

    private async Task<string> TokenAsync(string email, string password)
    {
        using var response = await SendAsync(new HttpRequestMessage(HttpMethod.Post, "/auth/token")
        {
            Content = JsonContent.Create(new CredentialExchangeRequest(email, password)),
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;
    }

    private async Task<WorkSubmissionResponse> LoadAsync(string token, string name, string text)
    {
        using var response = await SendAsync(Authorized(
            HttpMethod.Post, "/trabajos", token,
            new WorkSubmissionRequest(null, name, "2026-08-09", null, text)));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        return (await response.Content.ReadFromJsonAsync<WorkSubmissionResponse>())!;
    }

    /// <summary>
    /// Un trabajo llevado al estado pedido **por las transiciones del dominio y por el puerto del
    /// producto**, porque la etapa `e` no tiene punto de acceso que saque un trabajo de
    /// `Borrador`. No se escribe ninguna fila a mano: eso verificaría el esquema y no el producto.
    /// </summary>
    private async Task<Guid> SeedWorkAsync(Guid ownerId, string name, string text, WorkStatus status)
    {
        using var scope = _dataService.Services.CreateScope();
        var works = scope.ServiceProvider.GetRequiredService<IWorkRepository>();
        var clock = scope.ServiceProvider.GetRequiredService<ISystemClock>();

        var work = Work.Create(ownerId, name, "2026-08-09", null, text, true, clock.UtcNow).Value!;

        if (status != WorkStatus.Draft)
        {
            Assert.True(work.Submit(true, false, clock.UtcNow).Succeeded);
        }

        if (status is WorkStatus.Approved or WorkStatus.Rejected)
        {
            var outcome = status == WorkStatus.Approved ? WorkOutcome.Approve : WorkOutcome.Reject;
            Assert.True(work.ApplyOutcome(Role.Administrator, outcome, "comentario del docente", clock.UtcNow)
                .Succeeded);
        }

        await works.AddAsync(work);

        return work.Id;
    }

    private static HttpRequestMessage Authorized(
        HttpMethod method, string route, string token, object? body = null)
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

    // ---- Lecturas del almacén -----------------------------------------------------------------

    private async Task<object?> ScalarAsync(string sql, params (string Name, object Value)[] parameters)
    {
        using var connection = new SqliteConnection($"Data Source={_storePath}");
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;

        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value);
        }

        var result = await command.ExecuteScalarAsync();

        return result is DBNull ? null : result;
    }

    private async Task<int> CountWorksAsync() =>
        Convert.ToInt32(await ScalarAsync("select count(*) from Work"), System.Globalization.CultureInfo.InvariantCulture);

    private async Task<string?> StoredTextAsync(Guid workId) =>
        (await ScalarAsync("select OriginalJson from Work where Id = $id collate nocase", ("$id", workId.ToString())))?.ToString();

    private async Task<string?> StoredStatusAsync(Guid workId) =>
        (await ScalarAsync("select Status from Work where Id = $id collate nocase", ("$id", workId.ToString())))?.ToString();

    private async Task<Guid> StoredOwnerAsync(Guid workId) =>
        Guid.Parse((await ScalarAsync("select OwnerId from Work where Id = $id collate nocase", ("$id", workId.ToString())))!
            .ToString()!);

    /// <summary>Cuántas tandas contiguas del mismo dueño trae el listado.</summary>
    private static int CountRuns(Guid[] owners)
    {
        var runs = 1;

        for (var index = 1; index < owners.Length; index++)
        {
            if (owners[index] != owners[index - 1])
            {
                runs++;
            }
        }

        return runs;
    }

    private static int CountTrailingCommas(string text)
    {
        var trailing = 0;

        for (var index = 0; index < text.Length; index++)
        {
            if (text[index] != ',')
            {
                continue;
            }

            var next = index + 1;
            while (next < text.Length && char.IsWhiteSpace(text[next]))
            {
                next++;
            }

            if (next < text.Length && text[next] is '}' or ']')
            {
                trailing++;
            }
        }

        return trailing;
    }
}
