using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Application.Tests;

/// <summary>
/// Los seis casos de uso de cuentas —los tres de la etapa `c` y los tres de la `d`—, ejercidos
/// contra dobles de sus puertos.
/// </summary>
/// <remarks>
/// LOS DOBLES REEMPLAZAN A LOS ADAPTADORES Y A NADIE MÁS (intake §17.2.P.11 punto 3): el
/// repositorio y el reloj. El dominio es el de verdad, porque es lo que se está verificando.
/// </remarks>
public sealed class AccountUseCaseTests
{
    private static readonly DateTimeOffset Moment = new(2026, 8, 14, 12, 0, 0, TimeSpan.Zero);

    private sealed class FixedClock : ISystemClock
    {
        public DateTimeOffset UtcNow => Moment;
    }

    private sealed class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = [];

        public int AddCount { get; private set; }

        public int UpdateCount { get; private set; }

        public IReadOnlyList<Account> Accounts => _accounts;

        public void Seed(Account account) => _accounts.Add(account);

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

        public int RemoveCount { get; private set; }
    }

    private static Account AnAdministrator(string email = "docente@frre.utn.edu.ar", string hash = "derivado-vigente") =>
        Account.ConfigureAdministrator(email, "Ana", "Rossi", hash, true, true, AccountStatus.Enabled, Moment).Value!;

    // ---- CU-10 · configurar la cuenta de administrador -----------------------------------

    [Fact]
    public async Task ConfiguringTheAdministratorOnAnEmptyStoreMaterializesExactlyOneAccount()
    {
        var accounts = new InMemoryAccountRepository();
        var useCase = new ConfigureAdministratorUseCase(accounts, new FixedClock());

        var result = await useCase.ExecuteAsync("Docente@Frre.Utn.Edu.Ar", "Ana", "Rossi", "derivado");

        Assert.True(result.Succeeded);
        Assert.Equal(1, accounts.AddCount);
        Assert.Equal(Role.Administrator, result.Value!.Role);
        Assert.Equal("Docente@Frre.Utn.Edu.Ar", result.Value.Email);
        Assert.Equal(Moment, Assert.Single(accounts.Accounts).CreatedAt);
    }

    [Fact]
    public async Task ConfiguringASecondAdministratorIsRejectedAndWritesNothing()
    {
        // PRIMER CRITERIO DE TRANSICIÓN de la etapa `c`, mitad «sólo mientras no exista ninguno»,
        // visto en la capa que lo hace cumplir primero (`RN-01`, `INV-05`).
        var accounts = new InMemoryAccountRepository();
        accounts.Seed(AnAdministrator());
        var useCase = new ConfigureAdministratorUseCase(accounts, new FixedClock());

        var result = await useCase.ExecuteAsync("otro@frre.utn.edu.ar", "Otro", "Docente", "derivado");

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.AdministratorAlreadyConfigured, result.ConditionCode);
        Assert.Equal(0, accounts.AddCount);
    }

    [Fact]
    public async Task ConfiguringWithAnEmailThatIsAlreadyRegisteredIsRejected()
    {
        var accounts = new InMemoryAccountRepository();
        var useCase = new ConfigureAdministratorUseCase(accounts, new FixedClock());
        Assert.True((await useCase.ExecuteAsync("docente@frre.utn.edu.ar", "Ana", "Rossi", "derivado")).Succeeded);

        // Se saca al administrador de en medio para llegar al control de correo, que en el
        // camino natural queda detrás del de administrador único.
        var onlyStudentsRepository = new InMemoryAccountRepository();
        var registered = accounts.Accounts[0];
        onlyStudentsRepository.Seed(registered);

        var second = new ConfigureAdministratorUseCase(onlyStudentsRepository, new FixedClock());
        var result = await second.ExecuteAsync(registered.Email, "Ana", "Rossi", "derivado");

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.AdministratorAlreadyConfigured, result.ConditionCode);
    }

    // ---- CU-04 · resolver el ingreso -----------------------------------------------------

    [Fact]
    public async Task SignInResolvesWithTheIdentityWhenTheAccountAdmitsAndTheCredentialMatches()
    {
        var accounts = new InMemoryAccountRepository();
        var administrator = AnAdministrator();
        accounts.Seed(administrator);
        var useCase = new ResolveSignInUseCase(accounts);

        // La recuperación es POR CORREO NORMALIZADO: se escribe con otra caja a propósito.
        var result = await useCase.ExecuteAsync("DOCENTE@frre.UTN.edu.ar", _ => CredentialCheck.Matches);

        Assert.True(result.Succeeded);
        Assert.Equal(administrator.Id, result.Value!.Id);
        Assert.Equal(Role.Administrator, result.Value.Role);
    }

    [Fact]
    public async Task SignInWithAnUnknownEmailAndSignInWithAWrongPasswordAreIndistinguishableToTheCaller()
    {
        // Los dos motivos son distintos adentro y la superficie los traduce al MISMO código:
        // acá se verifica que ninguno de los dos devuelva identidad.
        var accounts = new InMemoryAccountRepository();
        accounts.Seed(AnAdministrator());
        var useCase = new ResolveSignInUseCase(accounts);

        var unknown = await useCase.ExecuteAsync("nadie@frre.utn.edu.ar", _ => CredentialCheck.Matches);
        var wrong = await useCase.ExecuteAsync("docente@frre.utn.edu.ar", _ => CredentialCheck.DoesNotMatch);

        Assert.False(unknown.Succeeded);
        Assert.False(wrong.Succeeded);
        Assert.Null(unknown.Value);
        Assert.Null(wrong.Value);
        Assert.Equal(ApplicationConditionCode.AccountNotFound, unknown.ConditionCode);
        Assert.Equal(ConditionCode.CurrentCredentialNotVerified, wrong.ConditionCode);
    }

    [Fact]
    public async Task AnUnreadableStoredValueIsNotCollapsedIntoDoesNotMatch()
    {
        var accounts = new InMemoryAccountRepository();
        accounts.Seed(AnAdministrator());
        var useCase = new ResolveSignInUseCase(accounts);

        var result = await useCase.ExecuteAsync("docente@frre.utn.edu.ar", _ => CredentialCheck.Unreadable);

        Assert.False(result.Succeeded);
        Assert.Equal(InfrastructureConditionCode.UnreadablePasswordHash, result.ConditionCode);
    }

    [Fact]
    public async Task TheDerivedValueNeverLeavesTheUseCase()
    {
        // La comparación ENTRA como función y el valor derivado NO SALE: lo que el punto de
        // acceso recibe es un desenlace, nunca el valor guardado.
        var accounts = new InMemoryAccountRepository();
        accounts.Seed(AnAdministrator(hash: "el-derivado-guardado"));
        var useCase = new ResolveSignInUseCase(accounts);

        string? observed = null;
        var result = await useCase.ExecuteAsync("docente@frre.utn.edu.ar", stored =>
        {
            observed = stored;
            return CredentialCheck.Matches;
        });

        Assert.Equal("el-derivado-guardado", observed);
        Assert.True(result.Succeeded);
        Assert.DoesNotContain(
            "el-derivado-guardado",
            string.Join('|', result.Value!.Id, result.Value.Email, result.Value.Role),
            StringComparison.Ordinal);
    }

    // ---- CU-03 · cambiar la contraseña propia --------------------------------------------

    [Fact]
    public async Task ChangingTheOwnPasswordWithTheCurrentOneVerifiedMaterializesTheChange()
    {
        var accounts = new InMemoryAccountRepository();
        var administrator = AnAdministrator();
        accounts.Seed(administrator);
        var useCase = new ChangeOwnPasswordUseCase(accounts);

        var result = await useCase.ExecuteAsync(
            administrator.Id, _ => CredentialCheck.Matches, () => "derivado-nuevo");

        Assert.True(result.Succeeded);
        Assert.Equal(1, accounts.UpdateCount);
        Assert.Equal("derivado-nuevo", administrator.PasswordHash);
    }

    [Fact]
    public async Task TheNewCredentialIsNotDerivedUntilTheCurrentOneHasBeenVerified()
    {
        // Si la vigente no verifica, la nueva NO se deriva: no se gasta trabajo criptográfico
        // en una petición que se va a rechazar, y sobre todo no se produce un valor derivado
        // de una contraseña que nadie autorizó a fijar.
        var accounts = new InMemoryAccountRepository();
        var administrator = AnAdministrator();
        accounts.Seed(administrator);
        var useCase = new ChangeOwnPasswordUseCase(accounts);

        var derived = 0;
        var result = await useCase.ExecuteAsync(
            administrator.Id,
            _ => CredentialCheck.DoesNotMatch,
            () => { derived++; return "derivado-nuevo"; });

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.CurrentCredentialNotVerified, result.ConditionCode);
        Assert.Equal(0, derived);
        Assert.Equal(0, accounts.UpdateCount);
        Assert.Equal("derivado-vigente", administrator.PasswordHash);
    }

    [Fact]
    public async Task ChangingThePasswordOfAnAccountThatDoesNotExistIsRejected()
    {
        var accounts = new InMemoryAccountRepository();
        var useCase = new ChangeOwnPasswordUseCase(accounts);

        var result = await useCase.ExecuteAsync(Guid.NewGuid(), _ => CredentialCheck.Matches, () => "x");

        Assert.False(result.Succeeded);
        Assert.Equal(ApplicationConditionCode.AccountNotFound, result.ConditionCode);
        Assert.Equal(0, accounts.UpdateCount);
    }
}
