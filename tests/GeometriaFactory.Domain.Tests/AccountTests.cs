using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Domain.Tests;

/// <summary>
/// La entidad `Account` de la etapa `c` (`Domain BT-06`), y los dos invariantes que el punto de
/// control mira con lupa: `INV-08` y `INV-09`.
/// </summary>
public sealed class AccountTests
{
    private static readonly DateTimeOffset Moment = new(2026, 8, 14, 12, 0, 0, TimeSpan.Zero);

    private static DomainResult<Account> ConfigureValidAdministrator(
        string email = "Docente@Frre.Utn.Edu.Ar",
        string? passwordHash = "PBKDF2-SHA256$1$c2FsdA==$ZGVyaXZlZA==",
        AccountStatus requestedStatus = AccountStatus.Enabled,
        bool administratorAbsenceDeclared = true,
        bool emailUniquenessVerified = true) =>
        Account.ConfigureAdministrator(
            email, "Ana", "Rossi", passwordHash,
            administratorAbsenceDeclared, emailUniquenessVerified, requestedStatus, Moment);

    [Fact]
    public void TheAdministratorIsBornEnabledWithoutThePendingMark()
    {
        // `CU-12` §4, y el invariante `INV-08` visto desde el nacimiento: la cuenta de
        // administrador nace habilitada, y no hay ningún camino que la deje de otra manera.
        var result = ConfigureValidAdministrator();

        Assert.True(result.Succeeded);
        var account = result.Value!;

        Assert.Equal(Role.Administrator, account.Role);
        Assert.Equal(AccountStatus.Enabled, account.Status);
        Assert.False(account.MustChangePassword);
        Assert.Equal(Moment, account.CreatedAt);
        Assert.NotEqual(Guid.Empty, account.Id);
    }

    [Fact]
    public void TheWrittenEmailIsKeptAndTheNormalizedOneDecidesTheIdentity()
    {
        // `INV-01` y `RN-02`: la forma que decide la identidad es la normalizada, y la escrita
        // se conserva porque es la que se muestra.
        var account = ConfigureValidAdministrator(email: "  Docente@Frre.Utn.Edu.Ar  ").Value!;

        Assert.Equal("Docente@Frre.Utn.Edu.Ar", account.Email);
        Assert.Equal("DOCENTE@FRRE.UTN.EDU.AR", account.NormalizedEmail);
        Assert.Equal(EmailIdentity.Normalize(account.Email), account.NormalizedEmail);
    }

    [Theory]
    [InlineData(null, "Ana", "Rossi")]
    [InlineData("  ", "Ana", "Rossi")]
    [InlineData("docente@frre.utn.edu.ar", null, "Rossi")]
    [InlineData("docente@frre.utn.edu.ar", "Ana", "   ")]
    public void ConfiguringWithoutTheRequiredFieldsIsRejected(string? email, string? firstName, string? lastName)
    {
        var result = Account.ConfigureAdministrator(
            email, firstName, lastName, "hash", true, true, AccountStatus.Enabled, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.RequiredFieldMissing, result.ConditionCode);
    }

    [Fact]
    public void ConfiguringWithoutTheDeclaredAbsenceOfAnAdministratorIsRejected()
    {
        // `RN-01` e `INV-05`. El dominio no conoce el conjunto de cuentas: exige la declaración.
        var result = ConfigureValidAdministrator(administratorAbsenceDeclared: false);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.AdministratorAlreadyConfigured, result.ConditionCode);
    }

    [Fact]
    public void ConfiguringWithoutTheVerifiedEmailUniquenessIsRejected()
    {
        var result = ConfigureValidAdministrator(emailUniquenessVerified: false);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.EmailUniquenessNotVerified, result.ConditionCode);
    }

    [Fact]
    public void ConfiguringWithoutACredentialIsRejected()
    {
        var result = ConfigureValidAdministrator(passwordHash: null);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.SetupWithoutCredential, result.ConditionCode);
    }

    [Theory]
    [InlineData(AccountStatus.Pending)]
    [InlineData(AccountStatus.Blocked)]
    public void TheAdministratorCannotBeConstitutedInAnyStateOtherThanEnabled(AccountStatus requested)
    {
        // `INV-08`, mirado desde el único acto de alta que esta etapa tiene: no hay forma de
        // pedir que la cuenta de administrador nazca deshabilitada.
        var result = ConfigureValidAdministrator(requestedStatus: requested);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.InitialStatusNotNegotiable, result.ConditionCode);
    }

    [Fact]
    public void TheSurfaceOfTheAccountIsExactlyTheOperationsTheContractsDeclare()
    {
        // HASTA LA ETAPA `c` ESTA PRUEBA HACÍA CUMPLIR `INV-08` POR AUSENCIA: la superficie
        // pública no declaraba ninguna operación que escribiera `Status`, y su cabecera anunciaba
        // que **si la etapa `d` agregaba transiciones, la prueba se caía y obligaba a escribir la
        // guarda**. La etapa `d` las agregó, la prueba se cayó y la guarda está escrita: es
        // `TheFiveOperationsAreClosedOverTheAdministratorAccount` de `AccountLifecycleTests`.
        //
        // LO QUE ESTA PRUEBA SIGUE HACIENDO ES LO QUE NADIE MÁS HACE: contar la superficie. Un
        // atributo con escritura pública o una operación de más no rompen ninguna otra prueba, y
        // acá se ven. Las siete son las que declaran `CU-12`, `CU-01`, `CU-02` —tres de sus
        // cuatro operaciones, porque habilitar y rehabilitar son la misma—, `CU-03`, `CU-04` y
        // `CU-13`, y **no hay ninguna octava**.
        var writable = typeof(Account)
            .GetProperties()
            .Where(property => property.CanWrite && (property.SetMethod?.IsPublic ?? false))
            .Select(property => property.Name)
            .ToArray();

        Assert.Empty(writable);

        var operations = typeof(Account)
            .GetMethods(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly)
            .Where(method => !method.IsSpecialName)
            .Select(method => method.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(
            [
                "AdmitDeletion",
                "Block",
                "ConfigureAdministrator",
                "Enable",
                "EvaluateAdmission",
                "Register",
                "ReplaceCredential",
                "ResetPassword",
            ],
            operations);
    }

    [Fact]
    public void ReplacingTheCredentialWithTheCurrentOneVerifiedAppliesAndLiftsTheMark()
    {
        // `CU-03` FA-01 y FA-04: los dos efectos son un solo acto.
        var account = ConfigureValidAdministrator().Value!;

        var result = account.ReplaceCredential("nuevo-derivado", currentCredentialVerified: true);

        Assert.True(result.Succeeded);
        Assert.Equal("nuevo-derivado", account.PasswordHash);
        Assert.False(account.MustChangePassword);
    }

    [Fact]
    public void ReplacingTheCredentialWithoutVerifyingTheCurrentOneIsRejectedAndChangesNothing()
    {
        var account = ConfigureValidAdministrator().Value!;
        var before = account.PasswordHash;

        var result = account.ReplaceCredential("nuevo-derivado", currentCredentialVerified: false);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.CurrentCredentialNotVerified, result.ConditionCode);
        Assert.Equal(before, account.PasswordHash);
    }

    [Fact]
    public void ReplacingTheCredentialWithAnEmptyDerivedValueIsRejectedAndChangesNothing()
    {
        var account = ConfigureValidAdministrator().Value!;
        var before = account.PasswordHash;

        var result = account.ReplaceCredential("   ", currentCredentialVerified: true);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.EmptyDerivedValue, result.ConditionCode);
        Assert.Equal(before, account.PasswordHash);
    }

    [Fact]
    public void TheAdministratorAdmitsAccess()
    {
        // `CU-04`: la evaluación es sobre la cuenta y no tiene efecto.
        var account = ConfigureValidAdministrator().Value!;

        var admission = account.EvaluateAdmission();

        Assert.True(admission.IsAdmissible);
        Assert.Null(admission.Reason);
    }
}
