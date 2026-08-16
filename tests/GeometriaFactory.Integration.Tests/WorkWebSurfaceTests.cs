using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// LOS TRABAJOS VISTOS DESDE LA INTERFAZ, SOBRE HTTP DE VERDAD y con las dos piezas levantadas.
/// </summary>
/// <remarks>
/// POR QUÉ NO SE ARMA NINGÚN COMPONENTE EN MEMORIA. Es la misma razón que
/// <see cref="AccountLifecycleWebSurfaceTests"/> declara en su cabecera: una batería que arma el
/// componente y le anota el estado a mano **por construcción no puede ver** lo que pasa al cruzar
/// una petición. Acá se pide por HTTP, se envían formularios con la antifalsificación puesta, y lo
/// que no se ve en una respuesta —que el trabajo sigue existiendo, que su texto es el que se
/// mandó— se lee **del almacén**.
///
/// LO QUE ESTA BATERÍA MIDE, Y ES LO QUE LA TRANSICIÓN `e` → `f` EXIGE DEL LADO DE LA INTERFAZ:
/// el alumno carga un trabajo por la pantalla y lo ve en su listado con su identificador y su
/// estado; reedita un borrador y el cambio se ve; **no puede** eliminar un trabajo que no está en
/// borrador, y esa protección se sostiene **forzando la petición** sin pasar por ninguna pantalla;
/// el alumno que pide el trabajo de otro recibe «no encontrado» y **la pantalla no dice nada más**;
/// el listado del administrador **no trae los borradores**, con borradores ajenos **existiendo de
/// verdad en el almacén**; y **ninguna respuesta servida lleva el testigo de sesión**, comparado
/// contra el testigo literal.
///
/// LOS TRABAJOS FUERA DE `Borrador` SE CONSTITUYEN POR LAS TRANSICIONES DEL DOMINIO, con el mismo
/// criterio y el mismo andamiaje que <see cref="WorkSurfaceTests"/>: la etapa `e` no tiene ningún
/// punto de acceso que saque un trabajo de `Borrador`, y escribir filas a mano verificaría el
/// esquema en lugar del producto.
/// </remarks>
public sealed class WorkWebSurfaceTests : IDisposable
{
    private const string AdministratorEmail = "docente@frre.utn.edu.ar";
    private const string AdministratorPassword = "la-que-eligio-el-docente";

    private const string StudentEmail = "alumna@frre.utn.edu.ar";
    private const string StudentFirstName = "Ana";
    private const string StudentLastName = "Diaz";

    private const string OtherStudentEmail = "otro-alumno@frre.utn.edu.ar";
    private const string OtherStudentFirstName = "Beto";
    private const string OtherStudentLastName = "Lopez";

    private const string ChosenPassword = "la-que-elijo-yo-ahora";

    private const string WorksRoute = "/mis-trabajos";
    private const string NewWorkRoute = "/trabajo-nuevo";
    private const string CommissionRoute = "/entrega-comision";

    /// <summary>
    /// El texto del escenario `E-2` del intake §20, con sus **dos comas finales** y su clave
    /// `Tapas`. Se transcribe tal como lo emite el programa del alumno y **no se corrige**: es el
    /// texto con el que se comprueba que la pantalla no lo toca.
    /// </summary>
    /// <summary>
    /// Un texto que **no verifica**: el del escenario `E-5`, con su figura 1 de tipo desconocido.
    /// </summary>
    /// <remarks>
    /// **[CORRECCIÓN Y RELEVO DE LA ETAPA `f`, DECLARADOS.]** Esta constante se llamaba `TextThatDoesNotVerify`
    /// y **no era el texto de `E-2`**: era un objeto inventado con una clave `Figuras` que ninguna
    /// fuente transcribe. Pasó desapercibido porque hasta la etapa `e` el texto no se interpretaba
    /// y a la fixture sólo se le pedía llegar entera al almacén.
    ///
    /// Se reemplaza por un escenario real, y se elige **el que no verifica** porque es lo que estas
    /// pruebas necesitan: casi todas siembran o esperan trabajos en `Borrador`, y desde la etapa
    /// `f` un texto correcto ya no queda en borrador.
    /// </remarks>
    private const string TextThatDoesNotVerify = Scenarios.E5;

    /// <summary>Forma de un acceso firmado: tres tramos separados por punto, el primero `eyJ`.</summary>
    private static readonly Regex SignedAccessShape =
        new(@"eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.", RegexOptions.None, TimeSpan.FromSeconds(1));

    private readonly string _storePath = DataServiceHarness.ReserveStorePath();
    private readonly DataServiceHarness _dataService;
    private readonly PublicPieceHarness _publicPiece;
    private readonly HttpClient _browser;
    private readonly ITestOutputHelper _output;

    /// <summary>La marca de antifalsificación, que se conserva entre peticiones como en un navegador.</summary>
    private string _antiforgeryMark = string.Empty;

    public WorkWebSurfaceTests(ITestOutputHelper output)
    {
        _output = output;
        _dataService = new DataServiceHarness(_storePath);
        _publicPiece = new PublicPieceHarness(_dataService.Server.CreateHandler());
        _browser = _publicPiece.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            HandleCookies = false,
            AllowAutoRedirect = false,
        });
    }

    public void Dispose()
    {
        _browser.Dispose();
        _publicPiece.Dispose();
        _dataService.Dispose();
        DataServiceHarness.DiscardStore(_storePath);
    }

    // ---- EL ALUMNO CARGA UN TRABAJO Y LO VE EN SU LISTADO, CON IDENTIFICADOR Y ESTADO ----

    /// <summary>
    /// La superficie de envío guarda con los cuatro campos, devuelve **identificador y estado**, y
    /// el trabajo aparece en el listado propio con su insignia. El texto queda en el almacén
    /// **idéntico carácter por carácter**, que es donde RN-08 se rompe sin que nada falle.
    /// </summary>
    [Fact]
    public async Task TheStudentLoadsAWorkAndSeesItInTheirListingWithItsIdentifierAndItsStatus()
    {
        var mark = await SignInAsStudentAsync();

        using var form = await GetAsync(NewWorkRoute, mark);
        var formHtml = Read(await form.Content.ReadAsStringAsync());

        Trace("1 · GET del envío", form);
        Assert.Equal(HttpStatusCode.OK, form.StatusCode);
        Assert.Contains("Trabajo nuevo", formHtml, StringComparison.Ordinal);
        Assert.Contains("submission-text", formHtml, StringComparison.Ordinal);

        // EL ÁREA DE TEXTO NO DEJA QUE EL NAVEGADOR TOQUE EL DATO DEL ALUMNO.
        Assert.Contains("spellcheck=\"false\"", formHtml, StringComparison.Ordinal);
        Assert.Contains("autocapitalize=\"off\"", formHtml, StringComparison.Ordinal);

        using var sent = await PostSubmissionAsync(
            form, formHtml, NewWorkRoute, mark, "Entrega 1", "09/08/2026", "un ortoedro", TextThatDoesNotVerify);
        var sentHtml = Read(await sent.Content.ReadAsStringAsync());

        Trace("2 · POST del envío", sent);
        Assert.Equal(HttpStatusCode.OK, sent.StatusCode);

        // EL IDENTIFICADOR Y EL ESTADO, los dos a la vista en la respuesta que la persona recibe.
        var workId = await OnlyWorkIdAsync();
        Assert.Contains(workId.ToString(), sentHtml, StringComparison.Ordinal);
        Assert.Contains("Tu trabajo quedó en estado", sentHtml, StringComparison.Ordinal);
        Assert.Contains("Borrador", sentHtml, StringComparison.Ordinal);

        // Y LA PANTALLA NO SIMULA UNA ENTREGA QUE NO OCURRE, ni acusa al texto de no verificar.
        Assert.DoesNotContain("Ya está entregado", sentHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("El texto no verificó", sentHtml, StringComparison.Ordinal);

        // **[relevo de la etapa `f`.]** Hasta la etapa `e` la pantalla decía «todos los trabajos
        // quedan en borrador», y era cierto porque nadie interpretaba. Ahora el texto **sí se
        // interpreta**, y lo que la persona lee es POR QUÉ el suyo quedó en borrador, con la figura
        // y el campo que lo impidieron. La pantalla sigue sin acusar a su programa de algo que el
        // laboratorio no miró: lo que declara es lo que no pudo interpretar.
        Assert.Contains("no se pudo interpretar entero", sentHtml, StringComparison.Ordinal);
        Assert.Contains("Figura 2 · posición 1 · campo «Tipo»", sentHtml, StringComparison.Ordinal);

        // EL TEXTO SE LEE DEL ALMACÉN Y NO DE LA RESPUESTA: la pantalla no lo tocó.
        Assert.Equal(TextThatDoesNotVerify, await StoredTextAsync(workId));
        Assert.Equal("Draft", await StoredStatusAsync(workId));

        // 3 · Y el listado propio lo trae, con su estado y sus tres acciones de borrador.
        using var listing = await GetAsync(WorksRoute, mark);
        var listingHtml = Read(await listing.Content.ReadAsStringAsync());

        Trace("3 · GET del listado propio", listing);
        Assert.Equal(HttpStatusCode.OK, listing.StatusCode);
        Assert.Contains("Entrega 1", listingHtml, StringComparison.Ordinal);
        Assert.Contains("09/08/2026", listingHtml, StringComparison.Ordinal);
        Assert.Contains("Borrador", listingHtml, StringComparison.Ordinal);
        Assert.Contains($"/trabajos/{workId}", listingHtml, StringComparison.Ordinal);
        Assert.Contains($"/trabajos/{workId}/editar", listingHtml, StringComparison.Ordinal);
        Assert.Contains($"{WorksRoute}?eliminar={workId}", listingHtml, StringComparison.Ordinal);

        // El listado NO arrastra el texto original: la proyección es pobre a propósito.
        Assert.DoesNotContain("Ortoedro", listingHtml, StringComparison.Ordinal);
    }

    // ---- EL ALUMNO REEDITA UN BORRADOR Y EL CAMBIO SE VE ----

    /// <summary>
    /// La reedición abre con **los datos y el texto tal como quedaron**, y lo reenviado reemplaza
    /// lo guardado: se comprueba en la pantalla y en el almacén.
    /// </summary>
    [Fact]
    public async Task TheStudentReeditsADraftAndTheChangeIsVisible()
    {
        var mark = await SignInAsStudentAsync();
        await LoadWorkThroughTheSurfaceAsync(mark, "Primer intento", TextThatDoesNotVerify);

        var workId = await OnlyWorkIdAsync();
        var editRoute = $"/trabajos/{workId}/editar";

        // 1 · El curso de reedición trae lo que había, sin ninguna transformación.
        using var edit = await GetAsync(editRoute, mark);
        var editHtml = Read(await edit.Content.ReadAsStringAsync());

        Trace("1 · GET de la reedición", edit);
        Assert.Equal(HttpStatusCode.OK, edit.StatusCode);
        Assert.Contains("Volver sobre tu borrador", editHtml, StringComparison.Ordinal);
        Assert.Contains("value=\"Primer intento\"", editHtml, StringComparison.Ordinal);
        Assert.Contains("\"Tipo\": \"Piramide\"", editHtml, StringComparison.Ordinal);

        // 2 · Se reenvía con otro nombre y otro texto. **[relevo de la etapa `f`.]** El texto
        // corregido es el del escenario `E-4` —el cubo de `Ejemplo2`—, que verifica sin una sola
        // observación: es el circuito entero que la persona vive, y hasta la etapa `e` no se podía
        // ejercer porque ningún texto verificaba.
        const string Corrected = Scenarios.E4;

        using var resent = await PostSubmissionAsync(
            edit, editHtml, editRoute, mark, "Primer intento corregido", "10/08/2026", "ahora un cubo", Corrected);
        var resentHtml = Read(await resent.Content.ReadAsStringAsync());

        Trace("2 · POST de la reedición", resent);
        Assert.Equal(HttpStatusCode.OK, resent.StatusCode);
        Assert.Contains("Tu trabajo quedó en estado", resentHtml, StringComparison.Ordinal);

        // Y EL DESENLACE CAMBIÓ: el texto corregido verifica, el trabajo queda entregado y **no
        // hay ninguna observación que mostrar**. Es el criterio negativo de `E-4`, visto por la
        // persona: la pantalla no dibuja «Observaciones (0)», que afirmaría que se interpretó y no
        // salió ninguna cuando lo cierto es que no salió ninguna porque estaba todo bien.
        Assert.Contains("Pendiente", resentHtml, StringComparison.Ordinal);
        Assert.Contains("El texto se interpretó y quedó entregado", resentHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Observaciones (", resentHtml, StringComparison.Ordinal);
        Assert.Equal("Submitted", await StoredStatusAsync(workId));

        // NO SE CONSTITUYÓ UN TRABAJO NUEVO: el identificador es el mismo y sigue habiendo uno solo.
        Assert.Equal(1, await CountWorksAsync());
        Assert.Equal(workId, await OnlyWorkIdAsync());

        // 3 · EL CAMBIO SE VE, en el almacén y en el listado.
        Assert.Equal(Corrected, await StoredTextAsync(workId));

        using var listing = await GetAsync(WorksRoute, mark);
        var listingHtml = Read(await listing.Content.ReadAsStringAsync());

        Trace("3 · GET del listado después de reeditar", listing);
        Assert.Contains("Primer intento corregido", listingHtml, StringComparison.Ordinal);
        Assert.Contains("10/08/2026", listingHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("<th scope=\"row\" class=\"gf-body-strong gf-th-plain gf-th-plain--body\">Primer intento</th>",
            listingHtml, StringComparison.Ordinal);
    }

    // ---- LA ELIMINACIÓN FUERA DE `Borrador`: LA PANTALLA ACOTA, EL SERVICIO HACE CUMPLIR ----

    /// <summary>
    /// Sobre un trabajo que no está en borrador la pantalla **no ofrece** eliminar —y tampoco
    /// editar—, y **la protección no depende de eso**: la misma eliminación pedida contra el
    /// servicio de datos con un acceso firmado legítimo, sin pasar por ninguna pantalla, se rechaza
    /// igual y el trabajo sigue existiendo.
    /// </summary>
    [Fact]
    public async Task TheStudentCannotDeleteAWorkOutsideDraftAndTheProtectionHoldsWhenTheRequestIsForced()
    {
        var mark = await SignInAsStudentAsync();
        var studentId = await IdOfAsync(StudentEmail);

        var draftId = await SeedWorkAsync(studentId, "Prueba 2", TextThatDoesNotVerify, WorkStatus.Draft);
        var submittedId = await SeedWorkAsync(studentId, "Cubo y ortoedro", TextThatDoesNotVerify, WorkStatus.Submitted);

        // 1 · LA PANTALLA ACOTA: sobre el borrador ofrece las tres acciones; sobre el entregado,
        //     sólo abrir. Lo que el estado no admite NO SE DIBUJA, ni siquiera inhabilitado.
        using var listing = await GetAsync(WorksRoute, mark);
        var listingHtml = Read(await listing.Content.ReadAsStringAsync());

        Trace("1 · GET del listado con los dos estados", listing);
        Assert.Equal(HttpStatusCode.OK, listing.StatusCode);
        Assert.Contains("Prueba 2", listingHtml, StringComparison.Ordinal);
        Assert.Contains("Cubo y ortoedro", listingHtml, StringComparison.Ordinal);

        Assert.Contains($"{WorksRoute}?eliminar={draftId}", listingHtml, StringComparison.Ordinal);
        Assert.DoesNotContain($"{WorksRoute}?eliminar={submittedId}", listingHtml, StringComparison.Ordinal);
        Assert.DoesNotContain($"/trabajos/{submittedId}/editar", listingHtml, StringComparison.Ordinal);

        // 2 · Y EL DIÁLOGO TAMPOCO SE ABRE con la dirección escrita a mano sobre el entregado.
        using var refusedDialog = await GetAsync($"{WorksRoute}?eliminar={submittedId}", mark);
        var refusedDialogHtml = Read(await refusedDialog.Content.ReadAsStringAsync());

        Trace("2 · GET del diálogo sobre el trabajo entregado", refusedDialog);
        Assert.Equal(HttpStatusCode.OK, refusedDialog.StatusCode);
        Assert.DoesNotContain("Eliminar «Cubo y ortoedro»", refusedDialogHtml, StringComparison.Ordinal);

        // Y sobre el borrador sí se abre, con el aviso de qué se destruye ANTES de confirmar.
        using var dialog = await GetAsync($"{WorksRoute}?eliminar={draftId}", mark);
        var dialogHtml = Read(await dialog.Content.ReadAsStringAsync());

        Assert.Contains("Eliminar «Prueba 2»", dialogHtml, StringComparison.Ordinal);
        Assert.Contains(
            "El trabajo deja de existir. Sólo se puede eliminar mientras está en borrador.",
            dialogHtml, StringComparison.Ordinal);

        // 3 · LA PROTECCIÓN DE VERDAD, FORZANDO LA PETICIÓN. Acceso firmado legítimo de la misma
        //     alumna, dueña del trabajo, contra el servicio de datos y sin pasar por la pantalla.
        using var data = _dataService.CreateClient();
        using var forced = new HttpRequestMessage(HttpMethod.Delete, $"/trabajos/{submittedId}");
        forced.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await StudentTokenAsync());

        using var rejection = await data.SendAsync(forced);
        _output.WriteLine($"eliminación forzada contra el servicio de datos: {(int)rejection.StatusCode}");

        Assert.Equal(HttpStatusCode.Conflict, rejection.StatusCode);

        // EL TRABAJO SIGUE EXISTIENDO, leído del almacén y no de ninguna respuesta.
        Assert.Equal("Submitted", await StoredStatusAsync(submittedId));

        // 4 · Y la eliminación que sí procede es la del borrador, por la pantalla.
        using var deleted = await PostDeletionAsync(dialog, dialogHtml, draftId, mark);
        var deletedHtml = Read(await deleted.Content.ReadAsStringAsync());

        Trace("4 · POST de la eliminación del borrador", deleted);
        Assert.Equal(HttpStatusCode.OK, deleted.StatusCode);
        Assert.Contains(
            "El trabajo dejó de existir. El listado se volvió a pedir y ya no figura.",
            deletedHtml, StringComparison.Ordinal);

        Assert.Null(await StoredStatusAsync(draftId));
        Assert.Equal("Submitted", await StoredStatusAsync(submittedId));
    }

    // ---- EL TRABAJO AJENO: «NO ENCONTRADO», Y NADA MÁS ----

    /// <summary>
    /// La pantalla **no puede** decir otra cosa: con el trabajo de otro alumno responde el mismo
    /// texto neutro que con un identificador inexistente, y **no dibuja ni el nombre del trabajo,
    /// ni el de su dueño, ni su correo, ni su texto, ni su estado**.
    /// </summary>
    [Fact]
    public async Task TheStudentAskingForSomeoneElsesWorkGetsNotFoundAndTheScreenSaysNothingElse()
    {
        var mark = await SignInAsStudentAsync();

        var otherId = await EnrolOtherStudentAsync();
        var foreignWorkId = await SeedWorkAsync(otherId, "Segundo intento", TextThatDoesNotVerify, WorkStatus.Draft);

        // EL TRABAJO AJENO EXISTE DE VERDAD EN EL ALMACÉN: esta prueba no pasa por ausencia.
        Assert.Equal("Draft", await StoredStatusAsync(foreignWorkId));

        using var foreign = await GetAsync($"/trabajos/{foreignWorkId}", mark);
        var foreignHtml = Read(await foreign.Content.ReadAsStringAsync());

        Trace("1 · GET del trabajo ajeno", foreign);
        Assert.Equal(HttpStatusCode.OK, foreign.StatusCode);
        Assert.Contains(
            "No encontramos ese trabajo. Volviste al listado desde el que lo pediste.",
            foreignHtml, StringComparison.Ordinal);

        // Y NADA MÁS: ni el trabajo, ni su dueño, ni su texto.
        Assert.DoesNotContain("Segundo intento", foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain(OtherStudentFirstName, foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain(OtherStudentLastName, foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain(OtherStudentEmail, foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Ortoedro", foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Borrador", foreignHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Datos del trabajo", foreignHtml, StringComparison.Ordinal);

        // 2 · Y EL INEXISTENTE RESPONDE EXACTAMENTE LO MISMO. Es la comprobación que hace que el
        //     mensaje sea indistinguible: se comparan los dos cuerpos entre sí.
        var missing = Guid.NewGuid();

        using var absent = await GetAsync($"/trabajos/{missing}", mark);
        var absentHtml = Read(await absent.Content.ReadAsStringAsync());

        Trace("2 · GET de un identificador inexistente", absent);
        Assert.Equal(foreign.StatusCode, absent.StatusCode);
        Assert.Equal(
            WithoutOpaqueRuns(foreignHtml).Replace(foreignWorkId.ToString(), "«id»", StringComparison.Ordinal),
            WithoutOpaqueRuns(absentHtml).Replace(missing.ToString(), "«id»", StringComparison.Ordinal));

        // 3 · Y la reedición del trabajo ajeno tampoco dice nada distinto ni ofrece formulario.
        using var edit = await GetAsync($"/trabajos/{foreignWorkId}/editar", mark);
        var editHtml = Read(await edit.Content.ReadAsStringAsync());

        Trace("3 · GET de la reedición del trabajo ajeno", edit);
        Assert.Equal(HttpStatusCode.OK, edit.StatusCode);
        Assert.Contains("No encontramos ese trabajo. Volviste a tu listado.", editHtml, StringComparison.Ordinal);
        Assert.DoesNotContain("Segundo intento", editHtml, StringComparison.Ordinal);
        Assert.Equal(0, CountOf(editHtml, "id=\"submission-text\""));

        // Y su listado propio no cambió: sigue vacío.
        using var listing = await GetAsync(WorksRoute, mark);
        var listingHtml = Read(await listing.Content.ReadAsStringAsync());

        Assert.Contains("Todavía no cargaste ningún trabajo", listingHtml, StringComparison.Ordinal);
    }

    // ---- EL LISTADO DEL ADMINISTRADOR NO TRAE LOS BORRADORES ----

    /// <summary>
    /// Con **borradores ajenos existiendo de verdad en el almacén**, el listado de la comisión no
    /// trae ninguno, no los cuenta y no insinúa que existan; y el trabajo entregado sí figura, que
    /// es lo que evita que esta prueba pase por una lista vacía.
    /// </summary>
    [Fact]
    public async Task TheAdministratorListingDoesNotBringTheDrafts()
    {
        var studentMark = await SignInAsStudentAsync();
        var studentId = await IdOfAsync(StudentEmail);
        var otherId = await EnrolOtherStudentAsync();

        // DOS BORRADORES DE DOS ALUMNOS DISTINTOS, Y UN ENTREGADO.
        var draftOfStudent = await SeedWorkAsync(studentId, "Prueba 2", TextThatDoesNotVerify, WorkStatus.Draft);
        var draftOfOther = await SeedWorkAsync(otherId, "Borrador de Beto", TextThatDoesNotVerify, WorkStatus.Draft);
        var submitted = await SeedWorkAsync(studentId, "Cubo y ortoedro", TextThatDoesNotVerify, WorkStatus.Submitted);

        // ESTÁN DE VERDAD, leído del almacén: la prueba no pasa por ausencia.
        Assert.Equal("Draft", await StoredStatusAsync(draftOfStudent));
        Assert.Equal("Draft", await StoredStatusAsync(draftOfOther));
        Assert.Equal(3, await CountWorksAsync());

        // Y el alumno SÍ ve el suyo, que es la contracara: el recorte es del papel y no del dato.
        using var own = await GetAsync(WorksRoute, studentMark);
        var ownHtml = Read(await own.Content.ReadAsStringAsync());
        Assert.Contains("Prueba 2", ownHtml, StringComparison.Ordinal);

        var administratorMark = await SignInAsAdministratorAsync();

        using var commission = await GetAsync(CommissionRoute, administratorMark);
        var commissionHtml = Read(await commission.Content.ReadAsStringAsync());

        Trace("GET del listado de la comisión", commission);
        Assert.Equal(HttpStatusCode.OK, commission.StatusCode);

        // EL ENTREGADO ESTÁ, agrupado bajo su alumno con su correo y su recuento.
        Assert.Contains("Cubo y ortoedro", commissionHtml, StringComparison.Ordinal);
        Assert.Contains($"{StudentFirstName} {StudentLastName}", commissionHtml, StringComparison.Ordinal);
        Assert.Contains(StudentEmail, commissionHtml, StringComparison.Ordinal);
        Assert.Contains("1 trabajo", commissionHtml, StringComparison.Ordinal);
        Assert.Contains($"/trabajos/{submitted}", commissionHtml, StringComparison.Ordinal);

        // LOS BORRADORES NO ESTÁN, POR RECUENTO CON UMBRAL 0: ni por nombre, ni por identificador,
        // ni por insignia, ni por el valor crudo del conjunto cerrado.
        Assert.Equal(0, CountOf(commissionHtml, "Prueba 2"));
        Assert.Equal(0, CountOf(commissionHtml, "Borrador de Beto"));
        Assert.Equal(0, CountOf(commissionHtml, draftOfStudent.ToString()));
        Assert.Equal(0, CountOf(commissionHtml, draftOfOther.ToString()));
        Assert.Equal(0, CountOf(commissionHtml, "Draft"));

        // Y LA PANTALLA NO INSINÚA QUE EXISTAN: el alumno que sólo tiene borradores no figura como
        // grupo, y el selector de estado no ofrece `Borrador`.
        Assert.DoesNotContain(OtherStudentEmail, commissionHtml, StringComparison.Ordinal);
        Assert.Equal(0, CountOf(commissionHtml, "<option value=\"Borrador\""));

        // Contraste que hace que el recuento anterior signifique algo: el listado del alumno SÍ
        // ofrece `Borrador` en su selector, con el mismo instrumento de medición.
        Assert.Equal(1, CountOf(ownHtml, "<option value=\"Borrador\""));
    }

    // ---- NINGUNA RESPUESTA SERVIDA AL NAVEGADOR CONTIENE EL TESTIGO DE SESIÓN ----

    /// <summary>
    /// Comparado contra el testigo LITERAL que el servicio de datos emitió, y no sólo contra una
    /// forma: la comparación por forma no distingue «no está» de «está escrito de otro modo».
    /// </summary>
    [Fact]
    public async Task NoResponseServedToTheBrowserCarriesTheSessionToken()
    {
        var mark = await SignInAsStudentAsync();
        var studentId = await IdOfAsync(StudentEmail);
        var draftId = await SeedWorkAsync(studentId, "Prueba 2", TextThatDoesNotVerify, WorkStatus.Draft);

        var token = SessionTokenOf(mark);
        _output.WriteLine($"testigo guardado del lado del servidor: {token.Length} caracteres");

        var seen = new List<(string Step, string Payload)>();

        void Record(string step, HttpResponseMessage response, string body)
        {
            seen.Add((step, body));
            seen.Add((step + " · cabeceras", HeadersOf(response)));
        }

        using (var listing = await GetAsync(WorksRoute, mark))
        {
            Record("listado propio", listing, Read(await listing.Content.ReadAsStringAsync()));
        }

        using (var form = await GetAsync(NewWorkRoute, mark))
        {
            var formHtml = Read(await form.Content.ReadAsStringAsync());
            Record("envío de trabajo", form, formHtml);

            using var sent = await PostSubmissionAsync(
                form, formHtml, NewWorkRoute, mark, "Entrega 1", "09/08/2026", null, TextThatDoesNotVerify);
            Record("resultado del envío", sent, Read(await sent.Content.ReadAsStringAsync()));
        }

        using (var detail = await GetAsync($"/trabajos/{draftId}", mark))
        {
            Record("vista del trabajo", detail, Read(await detail.Content.ReadAsStringAsync()));
        }

        using (var edit = await GetAsync($"/trabajos/{draftId}/editar", mark))
        {
            Record("reedición del borrador", edit, Read(await edit.Content.ReadAsStringAsync()));
        }

        using (var dialog = await GetAsync($"{WorksRoute}?eliminar={draftId}", mark))
        {
            var dialogHtml = Read(await dialog.Content.ReadAsStringAsync());
            Record("diálogo de eliminación", dialog, dialogHtml);

            using var deleted = await PostDeletionAsync(dialog, dialogHtml, draftId, mark);
            Record("eliminación aplicada", deleted, Read(await deleted.Content.ReadAsStringAsync()));
        }

        var administratorMark = await SignInAsAdministratorAsync();

        using (var commission = await GetAsync(CommissionRoute, administratorMark))
        {
            Record("listado de la comisión", commission, Read(await commission.Content.ReadAsStringAsync()));
        }

        var administratorToken = SessionTokenOf(administratorMark);

        foreach (var (step, payload) in seen)
        {
            _output.WriteLine($"{step,-40} {payload.Length} caracteres");
            Assert.DoesNotContain(token, payload, StringComparison.Ordinal);
            Assert.DoesNotContain(administratorToken, payload, StringComparison.Ordinal);
            Assert.DoesNotMatch(SignedAccessShape, payload);
            Assert.DoesNotContain("accessToken", payload, StringComparison.OrdinalIgnoreCase);
        }

        // LA PRUEBA NO PASA POR AUSENCIA: el mismo instrumento SÍ reconoce el testigo cuando está.
        Assert.Contains(token, $"lo que el almacén guarda es {token}", StringComparison.Ordinal);
    }

    // ---- Andamiaje ----

    private void Trace(string step, HttpResponseMessage response) =>
        _output.WriteLine($"{step,-46} {(int)response.StatusCode} {response.Headers.Location?.OriginalString}");

    private static string HeadersOf(HttpResponseMessage response) =>
        string.Join("\n", response.Headers.Select(header => $"{header.Key}: {string.Join(", ", header.Value)}"));

    private static int CountOf(string html, string needle)
    {
        var count = 0;
        var index = html.IndexOf(needle, StringComparison.Ordinal);

        while (index >= 0)
        {
            count++;
            index = html.IndexOf(needle, index + needle.Length, StringComparison.Ordinal);
        }

        return count;
    }

    /// <summary>
    /// El marcado sin sus tramos opacos —la marca de antifalsificación y el estado que el marco
    /// escribe—, que cambian en cada respuesta.
    /// </summary>
    /// <remarks>
    /// HACE FALTA PARA COMPARAR DOS CUERPOS ENTRE SÍ. Sin esto, la comparación entre la respuesta
    /// del trabajo ajeno y la del inexistente fallaría por los únicos fragmentos que **tienen** que
    /// cambiar entre dos respuestas, y no diría nada de lo que se quiere afirmar. El umbral es de
    /// sesenta caracteres seguidos sin espacio: ningún texto de producto tiene esa forma, de modo
    /// que **nada de lo que la pantalla le dice a la persona queda tapado por esta limpieza**.
    /// </remarks>
    private static string WithoutOpaqueRuns(string html) =>
        Regex.Replace(html, "[A-Za-z0-9+/=_-]{60,}", "«opaco»");

    // ---- Preparación del laboratorio ----

    /// <summary>
    /// El laboratorio con su administrador y la alumna habilitada **con su contraseña ya elegida**,
    /// y la sesión de la alumna abierta por la pantalla de ingreso.
    /// </summary>
    private async Task<string> SignInAsStudentAsync()
    {
        await ConfigureAdministratorAsync();
        await EnrolAsync(StudentEmail, StudentFirstName, StudentLastName);

        var (response, mark) = await SignInAsync(StudentEmail, ChosenPassword);
        using (response)
        {
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal("/mis-trabajos", response.Headers.Location?.AbsolutePath);
            Assert.NotEmpty(mark);
        }

        return mark;
    }

    private async Task<string> SignInAsAdministratorAsync()
    {
        var (response, mark) = await SignInAsync(AdministratorEmail, AdministratorPassword);
        using (response)
        {
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.NotEmpty(mark);
        }

        return mark;
    }

    private async Task<Guid> EnrolOtherStudentAsync()
    {
        await EnrolAsync(OtherStudentEmail, OtherStudentFirstName, OtherStudentLastName);
        return await IdOfAsync(OtherStudentEmail);
    }

    private async Task ConfigureAdministratorAsync()
    {
        using var data = _dataService.CreateClient();

        using var setup = await data.PostAsJsonAsync(
            "/cuentas/administrador",
            new AdministratorSetupRequest(AdministratorEmail, "Ana", "Rossi", AdministratorPassword));

        Assert.Equal(HttpStatusCode.Created, setup.StatusCode);
    }

    /// <summary>
    /// Una cuenta de alumno registrada, habilitada y con su contraseña elegida.
    /// </summary>
    /// <remarks>
    /// ESTO ES PREPARACIÓN Y NO LO QUE SE VERIFICA, y por eso entra por el servicio de datos: el
    /// ciclo de vida de la cuenta **por la interfaz** ya lo mide
    /// <see cref="AccountLifecycleWebSurfaceTests"/> entero, y repetirlo acá haría que un defecto
    /// del panel de cuentas rompiera las baterías de los trabajos.
    /// </remarks>
    private async Task EnrolAsync(string email, string firstName, string lastName)
    {
        using var data = _dataService.CreateClient();

        using var registration = await data.PostAsJsonAsync(
            "/cuentas", new AccountRegistrationRequest(email, firstName, lastName));

        Assert.Equal(HttpStatusCode.Created, registration.StatusCode);

        var accountId = (await registration.Content.ReadFromJsonAsync<AccountRegistrationResponse>())!.AccountId;

        using var enabling = new HttpRequestMessage(HttpMethod.Post, $"/cuentas/{accountId}/situacion")
        {
            Content = JsonContent.Create(new AccountStatusChangeRequest(accountId, nameof(AccountStatus.Enabled))),
        };
        enabling.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await AdministratorTokenAsync());

        using var enabled = await data.SendAsync(enabling);
        Assert.Equal(HttpStatusCode.OK, enabled.StatusCode);

        var provisional = (await enabled.Content.ReadFromJsonAsync<AccountStatusChangeResponse>())!
            .ProvisionalPassword!;

        using var change = await data.PostAsJsonAsync(
            "/cuenta/contrasena", new OwnPasswordChangeRequest(provisional, ChosenPassword, email));

        Assert.Equal(HttpStatusCode.OK, change.StatusCode);
    }

    private Task<string> AdministratorTokenAsync() => TokenAsync(AdministratorEmail, AdministratorPassword);

    private Task<string> StudentTokenAsync() => TokenAsync(StudentEmail, ChosenPassword);

    private async Task<string> TokenAsync(string email, string password)
    {
        using var data = _dataService.CreateClient();
        using var exchange = await data.PostAsJsonAsync(
            "/auth/token", new CredentialExchangeRequest(email, password));

        Assert.Equal(HttpStatusCode.OK, exchange.StatusCode);

        return (await exchange.Content.ReadFromJsonAsync<SessionResponse>())!.AccessToken;
    }

    /// <summary>
    /// Un trabajo llevado al estado pedido **por las transiciones del dominio y por el puerto del
    /// producto**: la etapa `e` no tiene ningún punto de acceso que saque un trabajo de `Borrador`.
    /// </summary>
    private async Task<Guid> SeedWorkAsync(Guid ownerId, string name, string text, WorkStatus status)
    {
        using var scope = _dataService.Services.CreateScope();
        var works = scope.ServiceProvider.GetRequiredService<IWorkRepository>();
        var clock = scope.ServiceProvider.GetRequiredService<ISystemClock>();

        var work = Work.Create(ownerId, name, "09/08/2026", null, text, true, clock.UtcNow).Value!;

        if (status != WorkStatus.Draft)
        {
            Assert.True(work.Submit(true, false, clock.UtcNow).Succeeded);
        }

        await works.AddAsync(work);

        return work.Id;
    }

    /// <summary>Carga un trabajo POR LA SUPERFICIE, que es el camino que se está verificando.</summary>
    private async Task LoadWorkThroughTheSurfaceAsync(string mark, string name, string text)
    {
        using var form = await GetAsync(NewWorkRoute, mark);
        var html = Read(await form.Content.ReadAsStringAsync());

        using var sent = await PostSubmissionAsync(form, html, NewWorkRoute, mark, name, "09/08/2026", null, text);
        Assert.Equal(HttpStatusCode.OK, sent.StatusCode);
    }

    // ---- Los envíos de formulario ----

    private Task<HttpResponseMessage> PostSubmissionAsync(
        HttpResponseMessage page,
        string html,
        string route,
        string mark,
        string name,
        string declaredDate,
        string? description,
        string originalJson) =>
        PostAsync(page, html, mark, route, "work-submission", new Dictionary<string, string>
        {
            ["Input.Name"] = name,
            ["Input.DeclaredDate"] = declaredDate,
            ["Input.Description"] = description ?? string.Empty,
            ["Input.OriginalJson"] = originalJson,
        });

    private Task<HttpResponseMessage> PostDeletionAsync(
        HttpResponseMessage page, string html, Guid workId, string mark) =>
        PostAsync(page, html, mark, $"{WorksRoute}?eliminar={workId}", "work-deletion",
            new Dictionary<string, string>());

    private async Task<HttpResponseMessage> PostAsync(
        HttpResponseMessage page,
        string html,
        string mark,
        string route,
        string handler,
        IReadOnlyDictionary<string, string> fields)
    {
        var payload = new Dictionary<string, string>
        {
            ["_handler"] = handler,
            ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
        };

        foreach (var (name, value) in fields)
        {
            payload[name] = value;
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, route)
        {
            Content = new FormUrlEncodedContent(payload),
        };

        // Las dos marcas van juntas: la de sesión y la de antifalsificación.
        request.Headers.Add("Cookie", $"{mark}; {AntiforgeryMarkOf(page)}");

        return await _browser.SendAsync(request);
    }

    private async Task<(HttpResponseMessage Response, string Mark)> SignInAsync(string email, string password)
    {
        using var page = await _browser.GetAsync("/ingreso");
        var html = Read(await page.Content.ReadAsStringAsync());

        using var request = new HttpRequestMessage(HttpMethod.Post, "/ingreso")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["_handler"] = "sign-in",
                ["__RequestVerificationToken"] = AntiforgeryTokenOf(html),
                ["Input.Email"] = email,
                ["Input.Password"] = password,
            }),
        };
        request.Headers.Add("Cookie", AntiforgeryMarkOf(page));

        var response = await _browser.SendAsync(request);
        var mark = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? cookies.Select(cookie => cookie.Split(';')[0])
                .FirstOrDefault(cookie => cookie.StartsWith(SessionCookieDefaults.CookieName + "=", StringComparison.Ordinal))
                ?? string.Empty
            : string.Empty;

        return (response, mark);
    }

    private async Task<HttpResponseMessage> GetAsync(string route, string? mark)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, route);

        if (!string.IsNullOrEmpty(mark))
        {
            request.Headers.Add("Cookie", mark);
        }

        return await _browser.SendAsync(request);
    }

    private static string AntiforgeryTokenOf(string html) =>
        Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"(?<token>[^\"]+)\"").Groups["token"].Value;

    private string AntiforgeryMarkOf(HttpResponseMessage response)
    {
        var emitted = response.Headers.TryGetValues("Set-Cookie", out var cookies)
            ? string.Join("; ", cookies.Select(cookie => cookie.Split(';')[0])
                .Where(cookie => cookie.Contains("Antiforgery", StringComparison.Ordinal)))
            : string.Empty;

        if (!string.IsNullOrEmpty(emitted))
        {
            _antiforgeryMark = emitted;
        }

        return _antiforgeryMark;
    }

    /// <summary>
    /// El texto tal como la persona lo lee, y no como el marcado lo transporta: el render estático
    /// escapa las no-ASCII y comparar contra el marcado crudo mediría la codificación.
    /// </summary>
    private static string Read(string html) => WebUtility.HtmlDecode(html);

    /// <summary>El testigo firmado que el almacén del servidor guarda para esta sesión.</summary>
    private string SessionTokenOf(string mark)
    {
        var value = mark[(SessionCookieDefaults.CookieName.Length + 1)..];

        var options = _publicPiece.Services
            .GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>()
            .Get(SessionCookieDefaults.Scheme);

        var ticket = options.TicketDataFormat.Unprotect(value);
        Assert.NotNull(ticket);

        var sessionId = ticket!.Principal.FindFirst(SessionClaims.SessionId)?.Value;
        Assert.False(string.IsNullOrEmpty(sessionId));

        var token = _publicPiece.Tokens.Find(sessionId!);
        Assert.False(string.IsNullOrEmpty(token));

        return token!;
    }

    // ---- Lo que se lee DEL ALMACÉN, y nunca de una respuesta ----

    private async Task<Guid> IdOfAsync(string email) =>
        Guid.Parse((await ScalarAsync(
            "select Id from Account where NormalizedEmail = $email",
            ("$email", EmailIdentity.Normalize(email))))!.ToString()!);

    private async Task<int> CountWorksAsync() =>
        Convert.ToInt32(await ScalarAsync("select count(*) from Work"), System.Globalization.CultureInfo.InvariantCulture);

    private async Task<Guid> OnlyWorkIdAsync() =>
        Guid.Parse((await ScalarAsync("select Id from Work"))!.ToString()!);

    private async Task<string?> StoredTextAsync(Guid workId) =>
        (await ScalarAsync(
            "select OriginalJson from Work where Id = $id collate nocase", ("$id", workId.ToString())))?.ToString();

    private async Task<string?> StoredStatusAsync(Guid workId) =>
        (await ScalarAsync(
            "select Status from Work where Id = $id collate nocase", ("$id", workId.ToString())))?.ToString();

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
}
