using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Application.Works;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Los cuatro casos de uso de la etapa `e` —carga y reedición, consulta propia, revisión de la
/// comisión y eliminación—, ejercidos contra dobles de sus dos puertos.
/// </summary>
/// <remarks>
/// LOS DOBLES REEMPLAZAN A LOS ADAPTADORES Y A NADIE MÁS: el dominio es el de verdad, porque las
/// reglas que se verifican viven ahí.
///
/// LOS DOBLES CUENTAN LAS CONSULTAS, y no es decoración: dos criterios se predican de que el
/// repositorio **no se toque** —el listado de la comisión pedido sin papel de administrador y la
/// consulta sin solicitante—, y eso no se puede medir sobre la respuesta.
/// </remarks>
public sealed class WorkUseCaseTests
{
    private static readonly DateTimeOffset Moment = new(2026, 4, 2, 10, 0, 0, TimeSpan.Zero);

    /// <summary>
    /// Doble del puerto de validación de figuras. **Interpreta cero figuras y no observa nada.**
    /// </summary>
    /// <remarks>
    /// ESTE PROYECTO DE PRUEBAS NO CONOCE `GeometriaFactory-Infrastructure` Y NO TIENE QUE
    /// CONOCERLO: lo que se ejerce acá es la orquestación —que el texto se interprete, que el
    /// resultado se adopte y que **el estado lo resuelva el dominio**—, no la interpretación en sí,
    /// que tiene su propia batería obligatoria con los ocho escenarios del intake.
    ///
    /// SIN OBSERVACIONES DE ERROR, DE MODO QUE EL TRABAJO PASA A `Submitted`. Es lo que hace
    /// visible en estas pruebas el cambio que trae la etapa `f`: hasta la `e` el estado resultante
    /// era siempre `Draft`, porque nadie podía declarar un resultado de interpretación.
    /// </remarks>
    private sealed class EmptyValidator : IFigureValidator
    {
        public FigureInterpretation Interpret(string originalJson) =>
            FigureInterpretation.From(0, [], []);
    }

    private sealed class FixedClock : ISystemClock
    {
        public DateTimeOffset UtcNow => Moment;
    }

    private sealed class InMemoryAccounts : IAccountRepository
    {
        private readonly List<Account> _accounts = [];

        public Account Seed(Account account)
        {
            _accounts.Add(account);
            return account;
        }

        public Task<Account?> FindByNormalizedEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default) =>
            Task.FromResult(_accounts.FirstOrDefault(account =>
                string.Equals(account.NormalizedEmail, normalizedEmail, StringComparison.Ordinal)));

        public Task<Account?> FindByIdAsync(Guid accountId, CancellationToken cancellationToken = default) =>
            Task.FromResult(_accounts.FirstOrDefault(account => account.Id == accountId));

        public Task<bool> AdministratorExistsAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(_accounts.Any(account => account.Role == Role.Administrator));

        public Task<bool> EmailIsRegisteredAsync(string normalizedEmail, CancellationToken cancellationToken = default) =>
            Task.FromResult(_accounts.Any(account =>
                string.Equals(account.NormalizedEmail, normalizedEmail, StringComparison.Ordinal)));

        public Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            _accounts.Add(account);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<Account>>(_accounts);

        public Task RemoveAsync(Account account, CancellationToken cancellationToken = default)
        {
            _accounts.Remove(account);
            return Task.CompletedTask;
        }
    }

    private sealed class InMemoryWorks : IWorkRepository
    {
        private readonly List<Work> _works = [];
        private readonly InMemoryAccounts _accounts;

        public InMemoryWorks(InMemoryAccounts accounts) => _accounts = accounts;

        public int AddCount { get; private set; }

        public int UpdateCount { get; private set; }

        public int RemoveCount { get; private set; }

        /// <summary>Cuántas veces se consultó el almacén, de la forma que sea.</summary>
        public int QueryCount { get; private set; }

        public IReadOnlyList<Work> Works => _works;

        public Work Seed(Work work)
        {
            _works.Add(work);
            return work;
        }

        public Task<Work?> FindByIdAsync(Guid workId, CancellationToken cancellationToken = default)
        {
            QueryCount++;
            return Task.FromResult(_works.FirstOrDefault(work => work.Id == workId));
        }

        public Task AddAsync(Work work, CancellationToken cancellationToken = default)
        {
            AddCount++;
            _works.Add(work);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Work work, CancellationToken cancellationToken = default)
        {
            UpdateCount++;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Work work, CancellationToken cancellationToken = default)
        {
            RemoveCount++;
            _works.Remove(work);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<WorkListEntry>> ListOwnedByAsync(
            Guid ownerId, CancellationToken cancellationToken = default)
        {
            QueryCount++;
            return Project(_works.Where(work => work.OwnerId == ownerId));
        }

        public Task<IReadOnlyList<WorkListEntry>> ListInAdministratorScopeAsync(
            Guid? ownerFilter, CancellationToken cancellationToken = default)
        {
            QueryCount++;

            var scoped = _works.Where(work => work.Status != Work.StatusOutsideAdministratorScope);

            if (ownerFilter is { } filter)
            {
                scoped = scoped.Where(work => work.OwnerId == filter);
            }

            return Project(scoped);
        }

        private Task<IReadOnlyList<WorkListEntry>> Project(IEnumerable<Work> works)
        {
            IReadOnlyList<WorkListEntry> entries =
            [
                .. works.Select(work =>
                {
                    var owner = _accounts.FindByIdAsync(work.OwnerId).Result!;

                    return new WorkListEntry(
                        work.Id, work.Name, work.DeclaredDate, work.Status,
                        owner.Id, owner.Email, owner.FirstName, owner.LastName);
                })
            ];

            return Task.FromResult(entries);
        }
    }

    // ---- CU-04 · carga y reedición ------------------------------------------------------------

    /// <summary>
    /// CA-01 — La carga toma el sello del reloj, atribuye el trabajo al solicitante y deja el
    /// texto **idéntico carácter por carácter**.
    /// </summary>
    [Fact]
    public async Task LoadingStampsTheClockAndKeepsTheTextIntact()
    {
        var (accounts, works, _) = World();
        var student = SeedStudent(accounts, "alumna@frre.utn.edu.ar");
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        const string Text = "{ \"Figuras\": [ ], }";

        var result = await useCase.LoadAsync(student.Id, "Entrega 1", "2026-08-09", null, Text);

        Assert.True(result.Succeeded);

        // RELEVO DE LA ETAPA `f`, DECLARADO. Hasta la etapa `e` esta prueba exigía `Draft`, y era
        // cierto: nadie podía declarar un resultado de interpretación y `Submit` rechazaba siempre.
        // La etapa `f` conecta el validador, de modo que **el estado pasa a depender de lo que la
        // interpretación devuelve**: sin observaciones de error, el trabajo entra en `Submitted`.
        // Es el cambio que la transición `f` → `g` pide, visto desde la orquestación.
        Assert.Equal(WorkStatus.Submitted, result.Value!.Status);
        Assert.Equal(Moment, result.Value.RegisteredAt);
        Assert.Equal(1, works.AddCount);

        var stored = Assert.Single(works.Works);
        Assert.Equal(student.Id, stored.OwnerId);
        Assert.Equal(Text, stored.OriginalJson);
    }

    /// <summary>CA-05 — Sin nombre, **el repositorio no recibe ninguna escritura**.</summary>
    [Fact]
    public async Task LoadingWithoutANameWritesNothing()
    {
        var (accounts, works, _) = World();
        var student = SeedStudent(accounts, "alumna@frre.utn.edu.ar");
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        var result = await useCase.LoadAsync(student.Id, null, "2026-08-09", null, "{}");

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.RequiredFieldMissing, result.ConditionCode);
        Assert.Equal(0, works.AddCount);
        Assert.Empty(works.Works);
    }

    /// <summary>CA-02 — El alumno B no reedita el trabajo del alumno A.</summary>
    [Fact]
    public async Task AnotherStudentCannotEdit()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        var result = await useCase.EditAsync(b.Id, work.Id, "otro", "2026-08-10", null, "{ }");

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.WorkNotFoundForRequester, result.ConditionCode);
        Assert.Equal(0, works.UpdateCount);
        Assert.Equal("{}", work.OriginalJson);
    }

    /// <summary>
    /// UN IDENTIFICADOR INEXISTENTE Y UNO AJENO DEVUELVEN EL MISMO MOTIVO. Es la mitad de RN-03
    /// que se verifica adentro, antes de que la traducción a «no encontrado» pueda taparlo.
    /// </summary>
    [Fact]
    public async Task AMissingIdAndAnotherStudentsWorkGiveTheSameReason()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        var foreign = await useCase.EditAsync(b.Id, work.Id, "x", "2026-08-10", null, "{}");
        var missing = await useCase.EditAsync(b.Id, Guid.NewGuid(), "x", "2026-08-10", null, "{}");

        Assert.Equal(foreign.ConditionCode, missing.ConditionCode);
        Assert.Equal(ConditionCode.WorkNotFoundForRequester, foreign.ConditionCode);
    }

    /// <summary>
    /// CA-03 — Fuera de `Draft` no se reedita, **el texto guardado sigue siendo el anterior** y
    /// el rechazo transporta el estado actual, que es lo que la respuesta tiene que declarar.
    /// </summary>
    [Fact]
    public async Task EditingOutsideDraftIsRejectedAndCarriesTheCurrentStatus()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(Submitted(NewWork(a.Id, "{ \"original\": true }")));
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        var result = await useCase.EditAsync(a.Id, work.Id, "x", "2026-08-10", null, "{ \"nuevo\": true }");

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.OperationOutsideDraft, result.ConditionCode);
        Assert.Equal(WorkStatus.Submitted, result.Value!.Status);
        Assert.Equal("{ \"original\": true }", work.OriginalJson);
        Assert.Equal(0, works.UpdateCount);
    }

    /// <summary>CA-04 — La reedición deja el trabajo sin interpretación anterior.</summary>
    [Fact]
    public async Task EditingDiscardsThePreviousParse()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new LoadAndEditOwnWorkUseCase(works, new FixedClock(), new EmptyValidator());

        var result = await useCase.EditAsync(a.Id, work.Id, "Entrega 1", "2026-08-10", "otra cosa", "{ \"x\": 1 }");

        Assert.True(result.Succeeded);
        Assert.Equal(1, works.UpdateCount);

        // RELEVO DE LA ETAPA `f`, DECLARADO. La prueba exigía que la interpretación anterior
        // quedara DESCARTADA, y lo comprobaba contra `null` porque en la etapa `e` no había una
        // segunda interpretación que adoptar. Sigue exigiendo lo mismo, y ahora comprueba lo que
        // de verdad pasa: la anterior se descarta y **se adopta la del texto nuevo**, que es la
        // única que puede describir el texto que quedó guardado.
        Assert.Equal(0, work.RootFigureCount);
        Assert.Empty(work.Pieces);
        Assert.Empty(work.Observations);
        Assert.Equal("{ \"x\": 1 }", work.OriginalJson);
    }

    // ---- CU-06 · la consulta del alumno -------------------------------------------------------

    /// <summary>CA-01 — El alumno A ve sus 4 trabajos y **0** de los 2 del alumno B.</summary>
    [Fact]
    public async Task TheStudentSeesOnlyTheirOwnWorks()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");

        works.Seed(NewWork(a.Id, "{}"));
        works.Seed(Submitted(NewWork(a.Id, "{}")));
        works.Seed(WithOutcome(NewWork(a.Id, "{}"), WorkOutcome.Approve, null));
        works.Seed(WithOutcome(NewWork(a.Id, "{}"), WorkOutcome.Reject, "revisá el área"));
        works.Seed(NewWork(b.Id, "{}"));
        works.Seed(Submitted(NewWork(b.Id, "{}")));

        var useCase = new ConsultOwnWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(a.Id);

        Assert.True(result.Succeeded);
        Assert.Equal(4, result.Value!.Count);
        Assert.All(result.Value, entry => Assert.Equal(a.Id, entry.OwnerId));

        // LOS CUATRO ESTADOS SON DISTINGUIBLES SIN ABRIR EL DETALLE.
        Assert.Equal(4, result.Value.Select(entry => entry.Status).Distinct().Count());
    }

    /// <summary>CA-05 — Un alumno sin trabajos recibe 0 elementos y **ningún motivo de error**.</summary>
    [Fact]
    public async Task AStudentWithoutWorksGetsAnEmptyList()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var useCase = new ConsultOwnWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(a.Id);

        Assert.True(result.Succeeded);
        Assert.Empty(result.Value!);
        Assert.Null(result.ConditionCode);
    }

    /// <summary>
    /// §6 — Sin solicitante declarado **no se consulta el repositorio**. Se mide sobre el doble
    /// y no sobre la respuesta.
    /// </summary>
    [Fact]
    public async Task ListingWithoutARequesterDoesNotTouchTheRepository()
    {
        var (accounts, works, _) = World();
        var useCase = new ConsultOwnWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(Guid.Empty);

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.RequesterNotDeclared, result.ConditionCode);
        Assert.Equal(0, works.QueryCount);
    }

    /// <summary>CA-04 — El detalle de un rechazado trae su estado y su comentario.</summary>
    [Fact]
    public async Task TheDetailCarriesTheOutcomeAndTheComment()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(WithOutcome(NewWork(a.Id, "{}"), WorkOutcome.Reject, "Revisá el área del cubo"));
        var useCase = new ConsultOwnWorksUseCase(works, accounts);

        var result = await useCase.DetailAsync(a.Id, work.Id);

        Assert.True(result.Succeeded);
        Assert.Equal(WorkStatus.Rejected, result.Value!.Status);
        Assert.Equal("Revisá el área del cubo", result.Value.AdministratorComment);
        Assert.Equal("a@frre.utn.edu.ar", result.Value.OwnerEmail);
    }

    /// <summary>CA-03 — El detalle ajeno y el inexistente devuelven **el mismo motivo**.</summary>
    [Fact]
    public async Task TheForeignDetailAndTheMissingOneGiveTheSameReason()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new ConsultOwnWorksUseCase(works, accounts);

        var foreign = await useCase.DetailAsync(b.Id, work.Id);
        var missing = await useCase.DetailAsync(b.Id, Guid.NewGuid());

        Assert.Equal(ConditionCode.WorkNotFoundForRequester, foreign.ConditionCode);
        Assert.Equal(foreign.ConditionCode, missing.ConditionCode);
    }

    // ---- CU-07 · la revisión de la comisión ---------------------------------------------------

    /// <summary>
    /// CA-01 — El listado del administrador trae los que no están en `Draft` y **0 borradores**,
    /// con el dato de dueño en cada elemento.
    /// </summary>
    [Fact]
    public async Task TheAdministratorListingExcludesDrafts()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");

        works.Seed(NewWork(a.Id, "{}"));
        works.Seed(Submitted(NewWork(a.Id, "{}")));
        works.Seed(NewWork(b.Id, "{}"));
        works.Seed(WithOutcome(NewWork(b.Id, "{}"), WorkOutcome.Approve, null));

        var useCase = new ReviewCommissionWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(Role.Administrator, null);

        Assert.True(result.Succeeded);
        Assert.Equal(2, result.Value!.Count);
        Assert.DoesNotContain(result.Value, entry => entry.Status == WorkStatus.Draft);
        Assert.All(result.Value, entry => Assert.False(string.IsNullOrWhiteSpace(entry.OwnerEmail)));
    }

    /// <summary>CA-02 — El filtro por alumno acota, y el recorte de borradores sigue rigiendo.</summary>
    [Fact]
    public async Task TheStudentFilterNarrowsAndTheDraftCutStillApplies()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");

        works.Seed(NewWork(a.Id, "{}"));
        works.Seed(Submitted(NewWork(a.Id, "{}")));
        works.Seed(Submitted(NewWork(b.Id, "{}")));

        var useCase = new ReviewCommissionWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(Role.Administrator, a.Id);

        Assert.True(result.Succeeded);
        var only = Assert.Single(result.Value!);
        Assert.Equal(a.Id, only.OwnerId);
        Assert.Equal(WorkStatus.Submitted, only.Status);
    }

    /// <summary>
    /// CA-03 — Sin papel de administrador, **el repositorio registra 0 consultas**.
    /// </summary>
    [Fact]
    public async Task AStudentAskingForTheCommissionListingTouchesNothing()
    {
        var (accounts, works, _) = World();
        var useCase = new ReviewCommissionWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(Role.Student, null);

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.AdministratorRoleRequired, result.ConditionCode);
        Assert.Equal(0, works.QueryCount);
    }

    /// <summary>El filtro por un alumno que no existe se distingue del alumno sin entregas.</summary>
    [Fact]
    public async Task AFilterOnAMissingStudentIsRejected()
    {
        var (accounts, works, _) = World();
        var useCase = new ReviewCommissionWorksUseCase(works, accounts);

        var result = await useCase.ListAsync(Role.Administrator, Guid.NewGuid());

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.AccountNotFound, result.ConditionCode);
    }

    /// <summary>CA-04 — El detalle de un borrador queda fuera de su alcance.</summary>
    [Fact]
    public async Task TheAdministratorCannotOpenADraft()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new ReviewCommissionWorksUseCase(works, accounts);

        var result = await useCase.DetailAsync(Role.Administrator, work.Id);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.WorkOutsideAdministratorScope, result.ConditionCode);
    }

    /// <summary>
    /// CA-05 — El detalle que ve el administrador es **el mismo** que ve el alumno, campo por
    /// campo.
    /// </summary>
    [Fact]
    public async Task TheDetailIsTheSameForBothRoles()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(Submitted(NewWork(a.Id, "{ \"Figuras\": [], }")));

        var student = await new ConsultOwnWorksUseCase(works, accounts).DetailAsync(a.Id, work.Id);
        var admin = await new ReviewCommissionWorksUseCase(works, accounts)
            .DetailAsync(Role.Administrator, work.Id);

        Assert.True(student.Succeeded);
        Assert.True(admin.Succeeded);
        Assert.Equal(student.Value, admin.Value);
    }

    // ---- CU-09 · la eliminación ---------------------------------------------------------------

    /// <summary>CA-01 — El alumno retira su borrador y el trabajo deja de existir.</summary>
    [Fact]
    public async Task TheStudentDeletesTheirDraft()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new DeleteWorkUseCase(works);

        var result = await useCase.ExecuteAsync(a.Id, Role.Student, work.Id);

        Assert.True(result.Succeeded);
        Assert.Equal(1, works.RemoveCount);
        Assert.Empty(works.Works);
    }

    /// <summary>
    /// CA-02 — Fuera de `Draft` el alumno no elimina, **el trabajo sigue existiendo** y el
    /// rechazo transporta el estado actual.
    /// </summary>
    [Theory]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public async Task TheStudentCannotDeleteOutsideDraft(WorkStatus status)
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(InStatus(NewWork(a.Id, "{}"), status));
        var useCase = new DeleteWorkUseCase(works);

        var result = await useCase.ExecuteAsync(a.Id, Role.Student, work.Id);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.OperationOutsideDraft, result.ConditionCode);
        Assert.Equal(status, result.Value);
        Assert.Equal(0, works.RemoveCount);
        Assert.Single(works.Works);
    }

    /// <summary>CA-03 — El trabajo ajeno responde como el inexistente.</summary>
    [Fact]
    public async Task TheStudentCannotDeleteSomeoneElsesWork()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var b = SeedStudent(accounts, "b@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new DeleteWorkUseCase(works);

        var foreign = await useCase.ExecuteAsync(b.Id, Role.Student, work.Id);
        var missing = await useCase.ExecuteAsync(b.Id, Role.Student, Guid.NewGuid());

        Assert.Equal(ConditionCode.WorkNotFoundForRequester, foreign.ConditionCode);
        Assert.Equal(foreign.ConditionCode, missing.ConditionCode);
        Assert.Equal(0, works.RemoveCount);
    }

    /// <summary>CA-04 — El administrador elimina **3 de 3** de los estados que ve.</summary>
    [Theory]
    [InlineData(WorkStatus.Submitted)]
    [InlineData(WorkStatus.Approved)]
    [InlineData(WorkStatus.Rejected)]
    public async Task TheAdministratorDeletesTheThreeStatusesTheySee(WorkStatus status)
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(InStatus(NewWork(a.Id, "{}"), status));
        var useCase = new DeleteWorkUseCase(works);

        var result = await useCase.ExecuteAsync(Guid.NewGuid(), Role.Administrator, work.Id);

        Assert.True(result.Succeeded);
        Assert.Empty(works.Works);
    }

    /// <summary>CA-05 — El administrador no elimina un borrador: no lo ve.</summary>
    [Fact]
    public async Task TheAdministratorCannotDeleteADraft()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var work = works.Seed(NewWork(a.Id, "{}"));
        var useCase = new DeleteWorkUseCase(works);

        var result = await useCase.ExecuteAsync(Guid.NewGuid(), Role.Administrator, work.Id);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.WorkOutsideAdministratorScope, result.ConditionCode);
        Assert.Single(works.Works);
    }

    /// <summary>
    /// LOS DOS ALCANCES SON OPUESTOS Y NO SE SOLAPAN: sobre los cuatro estados, cada trabajo lo
    /// puede eliminar **exactamente uno** de los dos papeles.
    /// </summary>
    [Fact]
    public async Task TheTwoScopesPartitionTheFourStatuses()
    {
        var (accounts, works, _) = World();
        var a = SeedStudent(accounts, "a@frre.utn.edu.ar");
        var useCase = new DeleteWorkUseCase(works);

        foreach (var status in Enum.GetValues<WorkStatus>())
        {
            var forStudent = works.Seed(InStatus(NewWork(a.Id, "{}"), status));
            var byStudent = await useCase.ExecuteAsync(a.Id, Role.Student, forStudent.Id);

            var forAdmin = works.Seed(InStatus(NewWork(a.Id, "{}"), status));
            var byAdmin = await useCase.ExecuteAsync(Guid.NewGuid(), Role.Administrator, forAdmin.Id);

            Assert.True(
                byStudent.Succeeded ^ byAdmin.Succeeded,
                $"Sobre {status} tienen que poder eliminarlo exactamente uno de los dos papeles.");
        }
    }

    // ---- Andamiaje ----------------------------------------------------------------------------

    private static (InMemoryAccounts Accounts, InMemoryWorks Works, FixedClock Clock) World()
    {
        var accounts = new InMemoryAccounts();
        return (accounts, new InMemoryWorks(accounts), new FixedClock());
    }

    private static Account SeedStudent(InMemoryAccounts accounts, string email)
    {
        var account = Account.Register(email, "Ana", "Diaz", null, true, Role.Student, AccountStatus.Pending, Moment)
            .Value!;
        account.Enable("credencial-derivada-de-prueba");

        return accounts.Seed(account);
    }

    private static Work NewWork(Guid ownerId, string originalJson) =>
        Work.Create(ownerId, "Entrega", "2026-08-09", null, originalJson, true, Moment).Value!;

    private static Work Submitted(Work work)
    {
        work.Submit(parseResultDeclared: true, validationErrorsDeclared: false, Moment);
        return work;
    }

    private static Work WithOutcome(Work work, WorkOutcome outcome, string? comment)
    {
        Submitted(work);
        work.ApplyOutcome(Role.Administrator, outcome, comment, Moment);
        return work;
    }

    private static Work InStatus(Work work, WorkStatus status) => status switch
    {
        WorkStatus.Draft => work,
        WorkStatus.Submitted => Submitted(work),
        WorkStatus.Approved => WithOutcome(work, WorkOutcome.Approve, null),
        _ => WithOutcome(work, WorkOutcome.Reject, null),
    };
}
