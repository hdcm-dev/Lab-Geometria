using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Los tres casos de uso de la etapa `d` —registro, gobierno de cuentas y reseteo—, ejercidos
/// contra dobles de sus puertos.
/// </summary>
/// <remarks>
/// LOS DOBLES REEMPLAZAN A LOS ADAPTADORES Y A NADIE MÁS. El dominio es el de verdad, porque es
/// lo que se está verificando, y la producción de la provisoria y la derivación entran **como
/// función**, que es exactamente la forma que tienen en el producto: por eso el doble de la
/// producción puede fallar sin que haga falta un puerto nuevo.
/// </remarks>
public sealed class AccountLifecycleUseCaseTests
{
    private static readonly DateTimeOffset Moment = new(2026, 8, 15, 12, 0, 0, TimeSpan.Zero);

    private sealed class FixedClock : ISystemClock
    {
        public DateTimeOffset UtcNow => Moment;
    }

    private sealed class InMemoryAccounts : IAccountRepository
    {
        private readonly List<Account> _accounts = [];

        public int AddCount { get; private set; }

        public int UpdateCount { get; private set; }

        public int RemoveCount { get; private set; }

        public IReadOnlyList<Account> Accounts => _accounts;

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
            AddCount++;
            _accounts.Add(account);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            UpdateCount++;
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<Account>>([.. _accounts]);

        public Task RemoveAsync(Account account, CancellationToken cancellationToken = default)
        {
            RemoveCount++;
            _accounts.Remove(account);
            return Task.CompletedTask;
        }
    }

    /// <summary>Un productor de provisorias que devuelve valores distintos, uno por invocación.</summary>
    private sealed class SequencedProvisionals
    {
        private int _produced;

        public int Produced => _produced;

        public string? Produce() => $"provisoria-{++_produced:D2}";
    }

    private static string Derive(string plaintext) => $"derivado({plaintext})";

    private static Account AnAdministrator(InMemoryAccounts accounts) => accounts.Seed(
        Account.ConfigureAdministrator(
            "docente@frre.utn.edu.ar", "Ana", "Rossi", "derivado", true, true, AccountStatus.Enabled, Moment).Value!);

    private static Account AStudent(InMemoryAccounts accounts, string email = "alumna@frre.utn.edu.ar") =>
        accounts.Seed(Account.Register(email, "Ana", "Diaz", null, true, Role.Student, AccountStatus.Pending, Moment).Value!);

    // ---- CU-01 · el registro ----------------------------------------------------------------

    /// <summary>CU-01 CA-01 — la cuenta queda `Pending`, sin credencial y con el sello del reloj.</summary>
    [Fact]
    public async Task RegisteringAStudentMaterializesAPendingAccountWithoutCredential()
    {
        var accounts = new InMemoryAccounts();
        var useCase = new RegisterAccountUseCase(accounts, new FixedClock());

        var result = await useCase.ExecuteAsync("Ana.Perez@Ejemplo.Edu", "Ana", "Pérez");

        Assert.True(result.Succeeded);
        Assert.Equal(1, accounts.AddCount);
        Assert.Equal(AccountStatus.Pending, result.Value!.Status);
        Assert.Equal(Role.Student, result.Value.Role);
        Assert.Equal(Moment, result.Value.CreatedAt);
        Assert.False(result.Value.MustChangePassword);
        Assert.Null(accounts.Accounts[0].PasswordHash);
    }

    /// <summary>CU-01 CA-02 — el correo ocupado no constituye nada y no declara nada de la cuenta que lo ocupa.</summary>
    [Fact]
    public async Task RegisteringAnAlreadyRegisteredEmailIsRejected()
    {
        var accounts = new InMemoryAccounts();
        AStudent(accounts, "ana.perez@ejemplo.edu");
        var useCase = new RegisterAccountUseCase(accounts, new FixedClock());

        var result = await useCase.ExecuteAsync("ANA.PEREZ@ejemplo.edu", "Otra", "Persona");

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.EmailAlreadyRegistered, result.ConditionCode);
        Assert.Equal(0, accounts.AddCount);
    }

    // ---- CU-02 · el gobierno de las cuentas -------------------------------------------------

    /// <summary>
    /// CU-02 CA-01 — habilitar devuelve **1 provisoria en claro**, deja la marca puesta y
    /// **0 provisorias persistidas en claro**: lo que quedó guardado es su forma derivada.
    /// </summary>
    [Fact]
    public async Task EnablingReturnsOneProvisionalInClearAndStoresOnlyItsDerivedForm()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var provisionals = new SequencedProvisionals();
        var useCase = new GovernCommissionAccountsUseCase(accounts);

        var result = await useCase.ChangeStatusAsync(
            Role.Administrator, student.Id, AccountStatus.Enabled, provisionals.Produce, Derive);

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Enabled, result.Value!.Status);
        Assert.Equal("provisoria-01", result.Value.ProvisionalPassword);
        Assert.True(result.Value.MustChangePassword);
        Assert.Equal(1, accounts.UpdateCount);

        // EL VALOR EN CLARO NO ESTÁ EN EL ALMACÉN: lo que se guardó es su derivado.
        Assert.Equal("derivado(provisoria-01)", student.PasswordHash);
        Assert.NotEqual("provisoria-01", student.PasswordHash);
    }

    /// <summary>
    /// CU-02 CA-06 — dos habilitaciones y una rehabilitación producen **3 provisorias distintas**,
    /// y el bloqueo devuelve **0**: sólo habilitar y rehabilitar producen una (RN-14, RN-16).
    /// </summary>
    [Fact]
    public async Task ThreeEnablementsProduceThreeDistinctProvisionalsAndBlockingProducesNone()
    {
        var accounts = new InMemoryAccounts();
        var first = AStudent(accounts, "una@ejemplo.edu");
        var second = AStudent(accounts, "otra@ejemplo.edu");
        var provisionals = new SequencedProvisionals();
        var useCase = new GovernCommissionAccountsUseCase(accounts);

        var one = await useCase.ChangeStatusAsync(Role.Administrator, first.Id, AccountStatus.Enabled, provisionals.Produce, Derive);
        var two = await useCase.ChangeStatusAsync(Role.Administrator, second.Id, AccountStatus.Enabled, provisionals.Produce, Derive);
        var blocked = await useCase.ChangeStatusAsync(Role.Administrator, first.Id, AccountStatus.Blocked, provisionals.Produce, Derive);
        var three = await useCase.ChangeStatusAsync(Role.Administrator, first.Id, AccountStatus.Enabled, provisionals.Produce, Derive);

        var produced = new[] { one, two, three }
            .Select(result => result.Value!.ProvisionalPassword!)
            .ToArray();

        Assert.Equal(3, produced.Distinct(StringComparer.Ordinal).Count());
        Assert.Null(blocked.Value!.ProvisionalPassword);

        // Y el productor se invocó EXACTAMENTE tres veces: el bloqueo no le pidió ninguna.
        Assert.Equal(3, provisionals.Produced);
    }

    /// <summary>
    /// CU-02 CA-07 — con el productor fallando, la habilitación se rechaza, la cuenta sigue
    /// `Pending` y **0 cuentas** quedan `Enabled` sin credencial derivada.
    /// </summary>
    [Fact]
    public async Task EnablingWithoutAProvisionalLeavesZeroAccountsEnabledWithoutCredential()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var useCase = new GovernCommissionAccountsUseCase(accounts);

        var result = await useCase.ChangeStatusAsync(
            Role.Administrator, student.Id, AccountStatus.Enabled, () => null, Derive);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.EnableWithoutTemporaryCredential, result.ConditionCode);
        Assert.Equal(AccountStatus.Pending, student.Status);
        Assert.Equal(0, accounts.UpdateCount);
        Assert.DoesNotContain(accounts.Accounts, account =>
            account.Status == AccountStatus.Enabled && string.IsNullOrWhiteSpace(account.PasswordHash));
    }

    /// <summary>
    /// CU-02 CA-02 y CU-11 CA-04 — quien no tiene el papel no ejerce ninguna de las cinco
    /// operaciones, y **nada se lee ni se escribe**.
    /// </summary>
    [Fact]
    public async Task WithoutTheAdministratorRoleNoneOfTheOperationsProceeds()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var govern = new GovernCommissionAccountsUseCase(accounts);
        var reset = new ResetStudentPasswordUseCase(accounts);
        var provisionals = new SequencedProvisionals();

        var listed = await govern.ListAsync(Role.Student);
        var changed = await govern.ChangeStatusAsync(Role.Student, student.Id, AccountStatus.Enabled, provisionals.Produce, Derive);
        var deleted = await govern.DeleteAsync(Role.Student, student.Id, student.Email);
        var wasReset = await reset.ExecuteAsync(Role.Student, student.Id, provisionals.Produce, Derive);

        Assert.Equal(ApplicationConditionCode.AdministratorRoleRequired, listed.ConditionCode);
        Assert.Equal(ApplicationConditionCode.AdministratorRoleRequired, changed.ConditionCode);
        Assert.Equal(ApplicationConditionCode.AdministratorRoleRequired, deleted.ConditionCode);
        Assert.Equal(ApplicationConditionCode.AdministratorRoleRequired, wasReset.ConditionCode);

        Assert.Equal(AccountStatus.Pending, student.Status);
        Assert.Null(student.PasswordHash);
        Assert.Equal(0, accounts.UpdateCount);
        Assert.Equal(0, accounts.RemoveCount);
        Assert.Equal(0, provisionals.Produced);
    }

    /// <summary>CU-02 CA-03 y CA-04 — la baja con el correo correcto retira; con otro no abre la unidad de trabajo.</summary>
    [Fact]
    public async Task DeletionRequiresTheWrittenEmailOfTheAccount()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts, "ana.perez@ejemplo.edu");
        var useCase = new GovernCommissionAccountsUseCase(accounts);

        var mismatched = await useCase.DeleteAsync(Role.Administrator, student.Id, "ana.perez@ejemplo.com");

        Assert.False(mismatched.Succeeded);
        Assert.Equal(ApplicationConditionCode.DeletionConfirmationMismatch, mismatched.ConditionCode);
        Assert.Equal(0, accounts.RemoveCount);
        Assert.Contains(student, accounts.Accounts);

        // La comparación es sobre el correo NORMALIZADO: una mayúscula no hace fallar una
        // confirmación correcta, que es justo el accidente que la guarda existe para evitar.
        var matched = await useCase.DeleteAsync(Role.Administrator, student.Id, "Ana.Perez@Ejemplo.Edu");

        Assert.True(matched.Succeeded);
        Assert.Equal(1, accounts.RemoveCount);
        Assert.DoesNotContain(student, accounts.Accounts);
    }

    /// <summary>
    /// INV-08 — CU-02 CA-05, CA-06 y CA-07: **las cuatro operaciones cerradas sobre la cuenta de
    /// administrador**, incluso cuando el propio administrador escribe su correo como confirmación.
    /// </summary>
    [Fact]
    public async Task TheAdministratorAccountAdmitsNoneOfTheFourOperations()
    {
        var accounts = new InMemoryAccounts();
        var administrator = AnAdministrator(accounts);
        var provisionals = new SequencedProvisionals();
        var useCase = new GovernCommissionAccountsUseCase(accounts);

        var enabled = await useCase.ChangeStatusAsync(
            Role.Administrator, administrator.Id, AccountStatus.Enabled, provisionals.Produce, Derive);
        var blocked = await useCase.ChangeStatusAsync(
            Role.Administrator, administrator.Id, AccountStatus.Blocked, provisionals.Produce, Derive);
        var deleted = await useCase.DeleteAsync(Role.Administrator, administrator.Id, administrator.Email);

        Assert.Equal(ConditionCode.OperationNotApplicableToAdministratorAccount, enabled.ConditionCode);
        Assert.Equal(ConditionCode.OperationNotApplicableToAdministratorAccount, blocked.ConditionCode);
        Assert.Equal(ConditionCode.OperationNotApplicableToAdministratorAccount, deleted.ConditionCode);

        // La cuenta no cambió, no se retiró y **no se gastó ninguna provisoria** en el intento.
        Assert.Equal(AccountStatus.Enabled, administrator.Status);
        Assert.Equal("derivado", administrator.PasswordHash);
        Assert.False(administrator.MustChangePassword);
        Assert.Equal(0, accounts.RemoveCount);
        Assert.Equal(0, provisionals.Produced);
    }

    /// <summary>El listado del administrador no transporta ninguna forma de la credencial.</summary>
    [Fact]
    public async Task TheListingCarriesTheStatusAndTheMarkAndNoFormOfTheCredential()
    {
        var accounts = new InMemoryAccounts();
        AnAdministrator(accounts);
        var student = AStudent(accounts);
        var useCase = new GovernCommissionAccountsUseCase(accounts);
        Assert.True((await useCase.ChangeStatusAsync(
            Role.Administrator, student.Id, AccountStatus.Enabled, () => "provisoria", Derive)).Succeeded);

        var result = await useCase.ListAsync(Role.Administrator);

        Assert.True(result.Succeeded);
        Assert.Equal(2, result.Value!.Count);

        var listed = result.Value.Single(item => item.Id == student.Id);
        Assert.Equal(AccountStatus.Enabled, listed.Status);
        Assert.True(listed.MustChangePassword);

        // NINGÚN MIEMBRO DEL TIPO PUEDE LLEVAR LA CREDENCIAL: se comprueba sobre la superficie y
        // no sobre el valor, que es lo que lo vuelve imposible en lugar de improbable.
        Assert.DoesNotContain(typeof(AccountSnapshot).GetProperties(), property =>
            property.Name.Contains("Password", StringComparison.OrdinalIgnoreCase)
            && property.Name != nameof(AccountSnapshot.MustChangePassword));
    }

    // ---- CU-11 · el reseteo -----------------------------------------------------------------

    /// <summary>
    /// CU-11 CA-01 — el reseteo procede, deja la marca, **conserva la situación** y lo que queda
    /// guardado es la forma derivada de la provisoria y no su valor en claro.
    /// </summary>
    [Fact]
    public async Task ResettingKeepsTheStatusAndStoresOnlyTheDerivedForm()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var govern = new GovernCommissionAccountsUseCase(accounts);
        Assert.True((await govern.ChangeStatusAsync(
            Role.Administrator, student.Id, AccountStatus.Enabled, () => "primera", Derive)).Succeeded);

        var useCase = new ResetStudentPasswordUseCase(accounts);
        var result = await useCase.ExecuteAsync(Role.Administrator, student.Id, () => "la-del-reseteo", Derive);

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Enabled, result.Value!.Status);
        Assert.Equal("la-del-reseteo", result.Value.ProvisionalPassword);
        Assert.True(result.Value.MustChangePassword);
        Assert.Equal("derivado(la-del-reseteo)", student.PasswordHash);
    }

    /// <summary>
    /// CU-11 CA-06 y CA-08 — el reseteo **procede sobre `Blocked` y sobre `Pending`**, devuelve la
    /// situación **sin cambio** y produce **0 motivos de rechazo** por la situación ni por la
    /// ausencia de credencial previa (RN-15, FA-04 y FA-05).
    /// </summary>
    [Theory]
    [InlineData(AccountStatus.Pending)]
    [InlineData(AccountStatus.Blocked)]
    public async Task ResettingProceedsOverPendingAndBlockedWithoutChangingTheStatus(AccountStatus status)
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var govern = new GovernCommissionAccountsUseCase(accounts);

        if (status == AccountStatus.Blocked)
        {
            Assert.True((await govern.ChangeStatusAsync(
                Role.Administrator, student.Id, AccountStatus.Enabled, () => "primera", Derive)).Succeeded);
            Assert.True((await govern.ChangeStatusAsync(
                Role.Administrator, student.Id, AccountStatus.Blocked, () => "no-deberia-pedirse", Derive)).Succeeded);
        }

        Assert.Equal(status, student.Status);

        var useCase = new ResetStudentPasswordUseCase(accounts);
        var result = await useCase.ExecuteAsync(Role.Administrator, student.Id, () => "la-del-reseteo", Derive);

        Assert.True(result.Succeeded);
        Assert.Equal(status, result.Value!.Status);
        Assert.Equal(status, student.Status);
        Assert.True(student.MustChangePassword);
    }

    /// <summary>
    /// CU-11 CA-07 — **habilitar y resetear en cualquiera de los dos órdenes terminan igual**: el
    /// administrador no tiene que acordarse de ninguna secuencia (RN-15).
    /// </summary>
    [Fact]
    public async Task EnablingAndResettingInEitherOrderEndInTheSamePlace()
    {
        static async Task<(AccountStatus Status, bool Mark, bool HasCredential)> RunAsync(bool resetFirst)
        {
            var accounts = new InMemoryAccounts();
            var student = AStudent(accounts);
            var govern = new GovernCommissionAccountsUseCase(accounts);
            var reset = new ResetStudentPasswordUseCase(accounts);
            var provisionals = new SequencedProvisionals();

            if (resetFirst)
            {
                Assert.True((await reset.ExecuteAsync(Role.Administrator, student.Id, provisionals.Produce, Derive)).Succeeded);
                Assert.True((await govern.ChangeStatusAsync(
                    Role.Administrator, student.Id, AccountStatus.Enabled, provisionals.Produce, Derive)).Succeeded);
            }
            else
            {
                Assert.True((await govern.ChangeStatusAsync(
                    Role.Administrator, student.Id, AccountStatus.Enabled, provisionals.Produce, Derive)).Succeeded);
                Assert.True((await reset.ExecuteAsync(Role.Administrator, student.Id, provisionals.Produce, Derive)).Succeeded);
            }

            return (student.Status, student.MustChangePassword, !string.IsNullOrWhiteSpace(student.PasswordHash));
        }

        Assert.Equal(await RunAsync(resetFirst: true), await RunAsync(resetFirst: false));
        Assert.Equal((AccountStatus.Enabled, true, true), await RunAsync(resetFirst: true));
    }

    /// <summary>
    /// CU-11 CA-05 — el reseteo **no procede sobre la cuenta de administrador**, no toca su
    /// credencial, no pone ninguna marca y **no gasta ninguna provisoria** en el intento (INV-08).
    /// </summary>
    [Fact]
    public async Task ResettingTheAdministratorAccountDoesNotProceed()
    {
        var accounts = new InMemoryAccounts();
        var administrator = AnAdministrator(accounts);
        var provisionals = new SequencedProvisionals();
        var useCase = new ResetStudentPasswordUseCase(accounts);

        var result = await useCase.ExecuteAsync(
            Role.Administrator, administrator.Id, provisionals.Produce, Derive);

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.ResetLimitedToStudentAccounts, result.ConditionCode);
        Assert.Equal("derivado", administrator.PasswordHash);
        Assert.False(administrator.MustChangePassword);
        Assert.Equal(0, accounts.UpdateCount);
        Assert.Equal(0, provisionals.Produced);
    }

    /// <summary>
    /// `Infrastructure CU-07` §6 — sin fuente de material impredecible **no se produce ningún
    /// valor y el reseteo no se completa**. No se compone uno por otro medio: un reseteo que no
    /// se completa es recuperable y una provisoria adivinable no se nota hasta que alguien la usa.
    /// </summary>
    [Fact]
    public async Task WithoutARandomnessSourceTheResetDoesNotCompleteAndTheOldCredentialStays()
    {
        var accounts = new InMemoryAccounts();
        var student = AStudent(accounts);
        var govern = new GovernCommissionAccountsUseCase(accounts);
        Assert.True((await govern.ChangeStatusAsync(
            Role.Administrator, student.Id, AccountStatus.Enabled, () => "primera", Derive)).Succeeded);

        var useCase = new ResetStudentPasswordUseCase(accounts);
        var result = await useCase.ExecuteAsync(Role.Administrator, student.Id, () => null, Derive);

        Assert.False(result.Succeeded);
        Assert.Equal(InfrastructureConditionCode.RandomnessSourceUnavailable, result.ConditionCode);
        Assert.Equal("derivado(primera)", student.PasswordHash);
    }

    /// <summary>CU-11 §6 — la cuenta que no existe termina sin efecto.</summary>
    [Fact]
    public async Task ResettingAnAccountThatDoesNotExistTerminatesWithoutEffect()
    {
        var accounts = new InMemoryAccounts();
        var useCase = new ResetStudentPasswordUseCase(accounts);

        var result = await useCase.ExecuteAsync(Role.Administrator, Guid.NewGuid(), () => "x", Derive);

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.AccountNotFound, result.ConditionCode);
        Assert.Equal(0, accounts.UpdateCount);
    }
}
