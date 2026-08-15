using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Domain.Tests;

/// <summary>
/// EL TRABAJO: constitución, reedición, envío, desenlace y las dos resoluciones de acceso.
/// Son los criterios de `Domain CU-05`, `CU-08`, `CU-09`, `CU-10` y `CU-11`.
/// </summary>
/// <remarks>
/// EL TEXTO SE COMPARA CARÁCTER POR CARÁCTER Y NO POR IGUALDAD DE «CONTENIDO». El escenario `E-2`
/// del intake trae **dos comas finales** y la clave `Tapas`; un texto que perdió una coma sigue
/// pareciéndose al original y ya no es el trabajo del alumno (RN-08).
/// </remarks>
public sealed class WorkTests
{
    /// <summary>
    /// El texto del escenario `E-2` del intake §20, con sus **2 comas finales** y su clave
    /// `Tapas`, tal como lo emite el programa del alumno. Se transcribe y **no se corrige**.
    /// </summary>
    private const string ScenarioE2 = """
        {
          "Figuras": [
            {
              "Tipo": "Ortoedro",
              "Largo": 7, "Ancho": 7, "Alto": 21,
              "Tapas": 2,
              "Area": 686.00,
              "Volumen": 343.00,
            }
          ],
        }
        """;

    private static readonly Guid StudentA = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid StudentB = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly DateTimeOffset Moment = new(2026, 4, 2, 10, 0, 0, TimeSpan.Zero);

    // ---- CU-05 · constitución -----------------------------------------------------------------

    /// <summary>
    /// CA-01 — El trabajo nace en `Draft`, con dueño y con el texto **idéntico carácter por
    /// carácter**, con sus dos comas finales incluidas.
    /// </summary>
    [Fact]
    public void CreatingLeavesTheWorkInDraftWithTheTextUntouched()
    {
        var result = Create();

        Assert.True(result.Succeeded);

        var work = result.Value!;

        Assert.Equal(WorkStatus.Draft, work.Status);
        Assert.Equal(StudentA, work.OwnerId);
        Assert.NotEqual(Guid.Empty, work.Id);

        // Carácter por carácter, y el recuento de comas finales medido sobre el dato guardado.
        Assert.Equal(ScenarioE2, work.OriginalJson, ignoreLineEndingDifferences: false);
        Assert.Equal(2, CountTrailingCommas(work.OriginalJson));

        // Sin interpretar: ninguna cantidad de figuras y ningún comentario.
        Assert.Null(work.RootFigureCount);
        Assert.Null(work.AdministratorComment);

        // Los dos sellos nacen iguales y los aporta el consumidor.
        Assert.Equal(Moment, work.CreatedAt);
        Assert.Equal(Moment, work.UpdatedAt);
    }

    /// <summary>CA-05 — Sin dueño no hay trabajo (INV-02).</summary>
    [Fact]
    public void CreatingWithoutOwnerIsRejected()
    {
        var result = Work.Create(
            Guid.Empty, "Entrega 1", "2026-08-09", null, ScenarioE2, true, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.WorkWithoutOwner, result.ConditionCode);
    }

    /// <summary>Falta el nombre o la fecha: `DATO_OBLIGATORIO_AUSENTE` (§6).</summary>
    [Theory]
    [InlineData(null, "2026-08-09")]
    [InlineData("  ", "2026-08-09")]
    [InlineData("Entrega 1", null)]
    [InlineData("Entrega 1", " ")]
    public void CreatingWithoutNameOrDeclaredDateIsRejected(string? name, string? declaredDate)
    {
        var result = Work.Create(StudentA, name, declaredDate, null, ScenarioE2, true, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.RequiredFieldMissing, result.ConditionCode);
    }

    /// <summary>
    /// La descripción admite ausencia y admite vacío, y ninguna de las dos es un dato faltante.
    /// </summary>
    [Fact]
    public void TheDescriptionIsOptional()
    {
        Assert.True(Work.Create(StudentA, "Entrega 1", "2026-08-09", null, ScenarioE2, true, Moment).Succeeded);
        Assert.True(Work.Create(StudentA, "Entrega 1", "2026-08-09", "", ScenarioE2, true, Moment).Succeeded);
    }

    /// <summary>
    /// Un texto que NO VERIFICA se adopta igual: `Draft` significa exactamente eso (FA-02). El
    /// producto no rechaza al alumno por lo que emitió su programa.
    /// </summary>
    [Fact]
    public void ATextThatDoesNotVerifyIsAdoptedAndLeavesTheWorkInDraft()
    {
        const string NotEvenJson = "esto no es notación de objetos, y es lo que el alumno pegó";

        var result = Work.Create(StudentA, "Entrega 1", "2026-08-09", null, NotEvenJson, true, Moment);

        Assert.True(result.Succeeded);
        Assert.Equal(WorkStatus.Draft, result.Value!.Status);
        Assert.Equal(NotEvenJson, result.Value.OriginalJson);
    }

    /// <summary>
    /// RN-08 — Un texto que el consumidor declara **corregido por el producto** se rechaza. El
    /// parámetro existe para poder rechazarlo, no para poder aportarlo.
    /// </summary>
    [Fact]
    public void CreatingWithACorrectedTextIsRejected()
    {
        var result = Work.Create(
            StudentA, "Entrega 1", "2026-08-09", null, ScenarioE2,
            originalJsonPreservedDeclared: false, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.OriginalJsonAltered, result.ConditionCode);
    }

    // ---- CU-05 FA-01 · reedición --------------------------------------------------------------

    /// <summary>
    /// CA-02 — La reedición conserva identificador, dueño y estado, y **descarta la
    /// interpretación anterior**.
    /// </summary>
    [Fact]
    public void EditingKeepsIdentityAndOwnerAndDiscardsThePreviousParse()
    {
        var work = Create().Value!;
        var id = work.Id;
        var later = Moment.AddHours(3);

        var edited = work.Edit("Entrega 1 corregida", "2026-08-10", "un ortoedro", "{}", true, later);

        Assert.True(edited.Succeeded);
        Assert.Equal(id, work.Id);
        Assert.Equal(StudentA, work.OwnerId);
        Assert.Equal(WorkStatus.Draft, work.Status);
        Assert.Equal("{}", work.OriginalJson);
        Assert.Null(work.RootFigureCount);

        // El sello de creación NO se reescribe; el de modificación sí.
        Assert.Equal(Moment, work.CreatedAt);
        Assert.Equal(later, work.UpdatedAt);
    }

    /// <summary>CA-03 y CA-04 — Fuera de `Draft` no se reedita, y el trabajo queda intacto.</summary>
    [Theory]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public void EditingOutsideDraftIsRejectedAndChangesNothing(WorkStatus status)
    {
        var work = InStatus(status);

        var edited = work.Edit("otro nombre", "2026-08-11", null, "{}", true, Moment.AddDays(1));

        Assert.False(edited.Succeeded);
        Assert.Equal(ConditionCode.EditOutsideDraft, edited.ConditionCode);
        Assert.Equal(ScenarioE2, work.OriginalJson);
        Assert.Equal(status, work.Status);
    }

    // ---- CU-08 · el envío ---------------------------------------------------------------------

    /// <summary>
    /// EN LA ETAPA `e` NINGÚN ENVÍO PROCEDE, y no es un defecto: el intérprete del texto es de la
    /// etapa `f`. Sin resultado de interpretación no hay nada que decidir.
    /// </summary>
    [Fact]
    public void SubmittingWithoutAParseResultIsRejected()
    {
        var work = Create().Value!;

        var submitted = work.Submit(parseResultDeclared: false, validationErrorsDeclared: false, Moment);

        Assert.False(submitted.Succeeded);
        Assert.Equal(ConditionCode.SubmissionWithoutParseResult, submitted.ConditionCode);
        Assert.Equal(WorkStatus.Draft, work.Status);
    }

    /// <summary>CA-01 y CA-03 — Sin errores de validación, el trabajo pasa a `Submitted`.</summary>
    [Fact]
    public void SubmittingWithoutValidationErrorsMovesToSubmitted()
    {
        var work = Create().Value!;

        var submitted = work.Submit(parseResultDeclared: true, validationErrorsDeclared: false, Moment.AddHours(1));

        Assert.True(submitted.Succeeded);
        Assert.Equal(WorkStatus.Submitted, work.Status);
    }

    /// <summary>
    /// CA-02 — Con al menos un error de validación **el envío procede igual** y el trabajo queda
    /// en `Draft`. No es un rechazo de la operación: es su resultado declarado (FA-01).
    /// </summary>
    [Fact]
    public void SubmittingWithValidationErrorsAppliesAndStaysInDraft()
    {
        var work = Create().Value!;

        var submitted = work.Submit(parseResultDeclared: true, validationErrorsDeclared: true, Moment.AddHours(1));

        Assert.True(submitted.Succeeded);
        Assert.Equal(WorkStatus.Draft, work.Status);
    }

    /// <summary>FA-03 — Un trabajo ya en `Submitted` no se reenvía.</summary>
    [Fact]
    public void SubmittingAnAlreadySubmittedWorkIsRejected()
    {
        var work = InStatus(WorkStatus.Submitted);

        var submitted = work.Submit(true, false, Moment);

        Assert.False(submitted.Succeeded);
        Assert.Equal(ConditionCode.SubmissionOutsideDraft, submitted.ConditionCode);
    }

    /// <summary>CA-04 y CA-05 — De los dos terminales no sale ninguna transición (INV-07).</summary>
    [Theory]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public void SubmittingFromATerminalStatusIsRejected(WorkStatus status)
    {
        var work = InStatus(status);

        var submitted = work.Submit(true, false, Moment);

        Assert.False(submitted.Succeeded);
        Assert.Equal(ConditionCode.TransitionFromTerminalStatus, submitted.ConditionCode);
        Assert.Equal(status, work.Status);
    }

    // ---- CU-10 · el desenlace -----------------------------------------------------------------

    /// <summary>CA-01 — Aprobar lleva a `Approved` y adopta el comentario.</summary>
    [Fact]
    public void ApprovingMovesToApprovedAndKeepsTheComment()
    {
        var work = InStatus(WorkStatus.Submitted);

        var outcome = work.ApplyOutcome(
            Role.Administrator, WorkOutcome.Approve, "revisá la fórmula del área del cubo", Moment);

        Assert.True(outcome.Succeeded);
        Assert.Equal(WorkStatus.Approved, work.Status);
        Assert.Equal("revisá la fórmula del área del cubo", work.AdministratorComment);
    }

    /// <summary>CA-02 — El comentario es opcional en los dos desenlaces.</summary>
    [Fact]
    public void RejectingWithoutACommentIsAdmitted()
    {
        var work = InStatus(WorkStatus.Submitted);

        var outcome = work.ApplyOutcome(Role.Administrator, WorkOutcome.Reject, null, Moment);

        Assert.True(outcome.Succeeded);
        Assert.Equal(WorkStatus.Rejected, work.Status);
        Assert.Null(work.AdministratorComment);
    }

    /// <summary>CA-03 — La facultad es exclusiva y no se delega, ni sobre el trabajo propio.</summary>
    [Fact]
    public void AStudentCannotApplyAnOutcome()
    {
        var work = InStatus(WorkStatus.Submitted);

        var outcome = work.ApplyOutcome(Role.Student, WorkOutcome.Approve, null, Moment);

        Assert.False(outcome.Succeeded);
        Assert.Equal(ConditionCode.OutcomeRequiresAdministratorRole, outcome.ConditionCode);
        Assert.Equal(WorkStatus.Submitted, work.Status);
    }

    /// <summary>CA-04 — Un terminal no admite un desenlace nuevo y conserva su comentario.</summary>
    [Fact]
    public void ATerminalWorkDoesNotAdmitANewOutcome()
    {
        var work = InStatus(WorkStatus.Submitted);
        work.ApplyOutcome(Role.Administrator, WorkOutcome.Approve, "aprobado", Moment);

        var second = work.ApplyOutcome(Role.Administrator, WorkOutcome.Reject, "me arrepentí", Moment);

        Assert.False(second.Succeeded);
        Assert.Equal(ConditionCode.TransitionFromTerminalStatus, second.ConditionCode);
        Assert.Equal(WorkStatus.Approved, work.Status);
        Assert.Equal("aprobado", work.AdministratorComment);
    }

    /// <summary>CA-05 — Un `Draft` no se aprueba: el administrador ni siquiera lo ve (RN-11).</summary>
    [Fact]
    public void ADraftDoesNotAdmitAnOutcome()
    {
        var work = Create().Value!;

        var outcome = work.ApplyOutcome(Role.Administrator, WorkOutcome.Approve, null, Moment);

        Assert.False(outcome.Succeeded);
        Assert.Equal(ConditionCode.OutcomeOutsideSubmitted, outcome.ConditionCode);
    }

    // ---- CU-09 · la resolución del acceso del alumno ------------------------------------------

    /// <summary>CA-01 — El dueño elimina su borrador.</summary>
    [Fact]
    public void TheOwnerMayDeleteTheirDraft()
    {
        var work = Create().Value!;

        Assert.True(work.ResolveStudentAccess(StudentA, WorkOperation.Delete).Succeeded);
    }

    /// <summary>
    /// CA-02 — El alumno B pide ver el trabajo del alumno A y recibe el motivo que es
    /// **indistinguible de la inexistencia**.
    /// </summary>
    [Fact]
    public void AnotherStudentGetsTheIndistinguishableReason()
    {
        var work = Create().Value!;

        var access = work.ResolveStudentAccess(StudentB, WorkOperation.View);

        Assert.False(access.Succeeded);
        Assert.Equal(ConditionCode.WorkNotFoundForRequester, access.ConditionCode);
    }

    /// <summary>
    /// LA PERTENENCIA MANDA SOBRE EL ESTADO, y por eso el ajeno en `Submitted` devuelve el motivo
    /// de pertenencia y no el de estado: el motivo de estado admitiría que el trabajo existe.
    /// </summary>
    [Fact]
    public void ForAnotherStudentTheOwnershipReasonWinsOverTheStatusReason()
    {
        var work = InStatus(WorkStatus.Submitted);

        var access = work.ResolveStudentAccess(StudentB, WorkOperation.Delete);

        Assert.Equal(ConditionCode.WorkNotFoundForRequester, access.ConditionCode);
    }

    /// <summary>CA-03 y CA-04 — El dueño no reedita ni elimina fuera de `Draft`.</summary>
    [Theory]
    [InlineData(WorkStatus.Submitted, WorkOperation.Edit)]
    [InlineData(WorkStatus.Submitted, WorkOperation.Delete)]
    [InlineData(WorkStatus.Approved, WorkOperation.Delete)]
    [InlineData(WorkStatus.Rejected, WorkOperation.Delete)]
    public void TheOwnerMayNotOperateOutsideDraft(WorkStatus status, WorkOperation operation)
    {
        var work = InStatus(status);

        var access = work.ResolveStudentAccess(StudentA, operation);

        Assert.False(access.Succeeded);
        Assert.Equal(ConditionCode.OperationOutsideDraft, access.ConditionCode);
    }

    /// <summary>CA-05 — Ver no está acotado por estado: el alumno ve su propio desenlace.</summary>
    [Theory]
    [InlineData(WorkStatus.Draft)]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public void TheOwnerSeesTheirWorkInAllFourStatuses(WorkStatus status)
    {
        var work = InStatus(status);

        Assert.True(work.ResolveStudentAccess(StudentA, WorkOperation.View).Succeeded);
    }

    /// <summary>Una operación fuera del conjunto se rechaza **sin evaluar la pertenencia**.</summary>
    [Fact]
    public void AnUnknownOperationIsRejectedBeforeOwnership()
    {
        var work = Create().Value!;

        var access = work.ResolveStudentAccess(StudentB, (WorkOperation)99);

        Assert.Equal(ConditionCode.UnknownOperation, access.ConditionCode);
    }

    // ---- CU-11 · la resolución del alcance del administrador ----------------------------------

    /// <summary>
    /// CA-01 — Sobre un alumno con un `Draft` y un `Submitted`, el alcance procede **sobre 1 de 2**.
    /// </summary>
    [Fact]
    public void TheAdministratorScopeCoversOneOfTheTwo()
    {
        Work[] works = [Create().Value!, InStatus(WorkStatus.Submitted)];

        var inScope = works
            .Count(work => work.ResolveAdministratorScope(Role.Administrator, WorkOperation.View).Succeeded);

        Assert.Equal(1, inScope);
    }

    /// <summary>CA-02 — El borrador queda fuera de su alcance, también para eliminar (RN-11).</summary>
    [Fact]
    public void TheAdministratorDoesNotSeeADraft()
    {
        var work = Create().Value!;

        var scope = work.ResolveAdministratorScope(Role.Administrator, WorkOperation.Delete);

        Assert.False(scope.Succeeded);
        Assert.Equal(ConditionCode.WorkOutsideAdministratorScope, scope.ConditionCode);
    }

    /// <summary>CA-03 y CA-05 — Los 3 estados que ve admiten eliminación, terminales incluidos.</summary>
    [Theory]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public void TheAdministratorMayDeleteTheThreeStatusesTheySee(WorkStatus status)
    {
        var work = InStatus(status);

        Assert.True(work.ResolveAdministratorScope(Role.Administrator, WorkOperation.Delete).Succeeded);
    }

    /// <summary>CA-04 — Sin papel de administrador no se evalúa el estado.</summary>
    [Fact]
    public void WithoutTheAdministratorRoleTheScopeIsRejectedBeforeTheStatus()
    {
        var work = InStatus(WorkStatus.Submitted);

        var scope = work.ResolveAdministratorScope(Role.Student, WorkOperation.Delete);

        Assert.False(scope.Succeeded);
        Assert.Equal(ConditionCode.ScopeRequiresAdministratorRole, scope.ConditionCode);
    }

    /// <summary>La reedición no está en su conjunto de operaciones: el administrador no edita.</summary>
    [Fact]
    public void TheAdministratorScopeDoesNotCoverEditing()
    {
        var work = InStatus(WorkStatus.Submitted);

        var scope = work.ResolveAdministratorScope(Role.Administrator, WorkOperation.Edit);

        Assert.Equal(ConditionCode.UnknownOperation, scope.ConditionCode);
    }

    /// <summary>
    /// EL PREDICADO DE ALCANCE TIENE UNA SOLA FUENTE, y ésta es la prueba que lo fija: el estado
    /// que el adaptador de datos excluye de la consulta es el que declara el dominio.
    /// </summary>
    [Fact]
    public void TheScopePredicateExcludesExactlyDraft()
    {
        Assert.Equal(WorkStatus.Draft, Work.StatusOutsideAdministratorScope);
    }

    // ---- Andamiaje ----------------------------------------------------------------------------

    private static Guards.DomainResult<Work> Create() =>
        Work.Create(StudentA, "Entrega 1", "2026-08-09", null, ScenarioE2, true, Moment);

    /// <summary>
    /// Un trabajo llevado al estado pedido **por las transiciones del dominio**, y no por un
    /// atajo: si el estado se pudiera fijar desde afuera, estas pruebas no probarían la máquina.
    /// </summary>
    private static Work InStatus(WorkStatus status)
    {
        var work = Create().Value!;

        if (status == WorkStatus.Draft)
        {
            return work;
        }

        Assert.True(work.Submit(parseResultDeclared: true, validationErrorsDeclared: false, Moment).Succeeded);

        if (status == WorkStatus.Submitted)
        {
            return work;
        }

        var outcome = status == WorkStatus.Approved ? WorkOutcome.Approve : WorkOutcome.Reject;
        Assert.True(work.ApplyOutcome(Role.Administrator, outcome, null, Moment).Succeeded);

        return work;
    }

    /// <summary>
    /// Cuántas comas del texto están seguidas —salteando espacios— por un cierre de objeto o de
    /// arreglo. Es la definición de coma final, y es lo que hace que este recuento mida el rasgo
    /// del escenario `E-2` y no la cantidad de comas del texto.
    /// </summary>
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
