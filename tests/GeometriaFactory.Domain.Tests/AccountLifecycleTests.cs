using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using Xunit;

namespace GeometriaFactory.Domain.Tests;

/// <summary>
/// El ciclo de vida de la cuenta de alumno: el auto-registro (CU-01), las cuatro operaciones del
/// administrador (CU-02) y el reseteo de contraseña (CU-13).
/// </summary>
/// <remarks>
/// SIN DOBLES Y SIN INFRAESTRUCTURA, como toda la batería de dominio: lo que se verifica son las
/// reglas, y las reglas no necesitan un almacén para ser ciertas.
/// </remarks>
public sealed class AccountLifecycleTests
{
    private static readonly DateTimeOffset Moment = new(2026, 8, 15, 12, 0, 0, TimeSpan.Zero);

    private static Account AStudent(AccountStatus status = AccountStatus.Pending, string? credential = null)
    {
        var account = Account.Register(
            "alumna@frre.utn.edu.ar", "Ana", "Diaz", null, true, Role.Student, AccountStatus.Pending, Moment).Value!;

        // Se lo lleva al estado pedido POR LAS OPERACIONES DEL PROPIO CONTRATO y no escribiendo
        // atributos: un doble que fijara el estado a mano estaría verificando otra cosa.
        if (status is AccountStatus.Enabled or AccountStatus.Blocked)
        {
            Assert.True(account.Enable(credential ?? "derivado-provisorio").Succeeded);
        }

        if (status == AccountStatus.Blocked)
        {
            Assert.True(account.Block().Succeeded);
        }

        return account;
    }

    private static Account AnAdministrator() => Account.ConfigureAdministrator(
        "docente@frre.utn.edu.ar", "Ana", "Rossi", "derivado", true, true, AccountStatus.Enabled, Moment).Value!;

    // ---- CU-01 · el auto-registro del alumno ------------------------------------------------

    /// <summary>CU-01 CA-01 — nace `Pending`, sin credencial y sin marca.</summary>
    [Fact]
    public void RegisteringAStudentLeavesThePendingAccountWithoutCredential()
    {
        var result = Account.Register(
            "ana@example.com", " Ana ", " Rossi ", null, true, Role.Student, AccountStatus.Pending, Moment);

        Assert.True(result.Succeeded);

        var account = result.Value!;
        Assert.Equal(Role.Student, account.Role);
        Assert.Equal(AccountStatus.Pending, account.Status);
        Assert.Null(account.PasswordHash);
        Assert.False(account.MustChangePassword);
        Assert.Equal(Moment, account.CreatedAt);
        Assert.Equal("Ana", account.FirstName);
        Assert.Equal("ANA@EXAMPLE.COM", account.NormalizedEmail);
    }

    /// <summary>CU-01 CA-02 y CA-03 — los datos obligatorios y la unicidad declarada.</summary>
    [Theory]
    [InlineData("ana@example.com", "Ana", "", true, ConditionCode.RequiredFieldMissing)]
    [InlineData("", "Ana", "Rossi", true, ConditionCode.RequiredFieldMissing)]
    [InlineData("ana@example.com", "Ana", "Rossi", false, ConditionCode.EmailUniquenessNotVerified)]
    public void RegistrationIsRejectedWhenTheDataOrTheDeclarationIsMissing(
        string email, string firstName, string lastName, bool uniquenessVerified, string expected)
    {
        var result = Account.Register(
            email, firstName, lastName, null, uniquenessVerified, Role.Student, AccountStatus.Pending, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(expected, result.ConditionCode);
        Assert.Null(result.Value);
    }

    /// <summary>
    /// CU-01 CA-04 — **el auto-registro no admite contraseña**, y es el criterio uno del enunciado
    /// de la etapa: el alumno se registra con correo, nombre y apellido, **sin elegir contraseña**.
    /// </summary>
    [Fact]
    public void RegistrationWithACredentialIsRejected()
    {
        var result = Account.Register(
            "ana@example.com", "Ana", "Rossi", "un-derivado-de-64-caracteres",
            true, Role.Student, AccountStatus.Pending, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.CredentialNotAllowedOnRegistration, result.ConditionCode);
    }

    /// <summary>CU-01 CA-05 y CA-06 — ni el estado inicial ni el papel se eligen desde afuera.</summary>
    [Theory]
    [InlineData(Role.Administrator, AccountStatus.Pending, ConditionCode.AdministratorRoleOutsideThisPath)]
    [InlineData(Role.Student, AccountStatus.Enabled, ConditionCode.InitialStatusNotNegotiable)]
    [InlineData(Role.Student, AccountStatus.Blocked, ConditionCode.InitialStatusNotNegotiable)]
    public void RegistrationDoesNotNegotiateTheRoleNorTheInitialStatus(
        Role role, AccountStatus status, string expected)
    {
        var result = Account.Register("ana@example.com", "Ana", "Rossi", null, true, role, status, Moment);

        Assert.False(result.Succeeded);
        Assert.Equal(expected, result.ConditionCode);
    }

    // ---- CU-02 · habilitar, bloquear, rehabilitar y dar de baja -----------------------------

    /// <summary>
    /// CU-02 CA-01 — habilitar deja la cuenta `Enabled`, **con credencial** y **con la marca
    /// puesta**. Es RN-16, y los dos efectos son un solo acto.
    /// </summary>
    [Fact]
    public void EnablingFixesTheProvisionalCredentialAndSetsTheMark()
    {
        var account = AStudent();

        var result = account.Enable("derivado-de-la-provisoria");

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Enabled, account.Status);
        Assert.Equal("derivado-de-la-provisoria", account.PasswordHash);
        Assert.True(account.MustChangePassword);
    }

    /// <summary>
    /// CU-02 CA-08 — habilitar **sin** aportar la credencial provisoria se rechaza, la cuenta
    /// sigue `Pending` y **0 cuentas** quedan `Enabled` sin credencial.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void EnablingWithoutTheProvisionalCredentialIsRejected(string? credential)
    {
        var account = AStudent();

        var result = account.Enable(credential);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.EnableWithoutTemporaryCredential, result.ConditionCode);
        Assert.Equal(AccountStatus.Pending, account.Status);
        Assert.Null(account.PasswordHash);
        Assert.False(account.MustChangePassword);
    }

    /// <summary>
    /// CU-02 CA-09 — **rehabilitar es habilitar a los efectos de RN-16**: la credencial no es la
    /// anterior y la marca queda puesta.
    /// </summary>
    [Fact]
    public void ReEnablingABlockedAccountProducesANewCredentialAndSetsTheMark()
    {
        // Se recorre el camino entero: habilitar, cambiar la provisoria —que LEVANTA la marca— y
        // bloquear. Así la prueba mide que la rehabilitación la vuelve a poner, y no que ya estaba.
        var account = AStudent(AccountStatus.Enabled, "derivado-viejo");
        Assert.True(account.ReplaceCredential("derivado-elegido", currentCredentialVerified: true).Succeeded);
        Assert.True(account.Block().Succeeded);
        Assert.False(account.MustChangePassword);

        var result = account.Enable("derivado-nuevo");

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Enabled, account.Status);
        Assert.Equal("derivado-nuevo", account.PasswordHash);
        Assert.NotEqual("derivado-elegido", account.PasswordHash);
        Assert.True(account.MustChangePassword);
    }

    /// <summary>
    /// CU-02 FA-02 — habilitar una cuenta que **ya está** `Enabled` no tiene efecto: no rechaza,
    /// no fija credencial nueva y no pone marca. Producir una provisoria que nadie pidió dejaría
    /// al alumno fuera de su propia cuenta.
    /// </summary>
    [Fact]
    public void EnablingAnAlreadyEnabledAccountHasNoEffect()
    {
        var account = AStudent(AccountStatus.Enabled, "derivado-provisorio");
        Assert.True(account.ReplaceCredential("derivado-elegido", true).Succeeded);
        Assert.False(account.MustChangePassword);

        var result = account.Enable("derivado-que-nadie-pidio");

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Enabled, account.Status);
        Assert.Equal("derivado-elegido", account.PasswordHash);
        Assert.False(account.MustChangePassword);
    }

    /// <summary>CU-02 CA-02 — bloquear no toca la credencial ni la marca.</summary>
    [Fact]
    public void BlockingChangesTheStatusAndNothingElse()
    {
        var account = AStudent(AccountStatus.Enabled, "derivado-provisorio");

        var result = account.Block();

        Assert.True(result.Succeeded);
        Assert.Equal(AccountStatus.Blocked, account.Status);
        Assert.Equal("derivado-provisorio", account.PasswordHash);
        Assert.True(account.MustChangePassword);
    }

    /// <summary>
    /// CU-02 CA-03 y FA-03 — bloquear una cuenta `Pending` no está en la tabla de transiciones y
    /// **no se infiere**. La segunda fila es la decisión derivada de la etapa `d`, declarada en
    /// la cabecera de la operación: bloquear una ya `Blocked` tampoco está en la tabla.
    /// </summary>
    [Theory]
    [InlineData(AccountStatus.Pending)]
    [InlineData(AccountStatus.Blocked)]
    public void BlockingFromAStatusTheTableDoesNotDeclareIsRejected(AccountStatus status)
    {
        var account = AStudent(status, "derivado-provisorio");
        var before = account.Status;

        var result = account.Block();

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.AccountTransitionNotAllowed, result.ConditionCode);
        Assert.Equal(before, account.Status);
    }

    /// <summary>CU-02 CA-04 — una baja que declare conservar los trabajos se rechaza (RN-07).</summary>
    [Fact]
    public void DeletionThatDoesNotCarryTheWorksIsRejected()
    {
        var account = AStudent(AccountStatus.Blocked, "derivado-provisorio");

        var refused = account.AdmitDeletion(worksCascadeDeclared: false);
        Assert.False(refused.Succeeded);
        Assert.Equal(ConditionCode.DeletionWithoutWorkCascade, refused.ConditionCode);

        var admitted = account.AdmitDeletion(worksCascadeDeclared: true);
        Assert.True(admitted.Succeeded);
    }

    /// <summary>
    /// INV-08 — CU-02 CA-05, CA-06 y CA-07, y CU-13 CA-04 juntos: **las cinco operaciones están
    /// cerradas sobre la cuenta de administrador**, con un solo código, y la cuenta no cambia.
    /// </summary>
    [Fact]
    public void TheFiveOperationsAreClosedOverTheAdministratorAccount()
    {
        var administrator = AnAdministrator();

        var refusals = new[]
        {
            administrator.Enable("derivado-provisorio"),
            administrator.Block(),
            administrator.AdmitDeletion(worksCascadeDeclared: true),
            administrator.ResetPassword("derivado-provisorio", worksCascadeDeclared: false),
        };

        Assert.All(refusals, refusal =>
        {
            Assert.False(refusal.Succeeded);
            Assert.Equal(ConditionCode.OperationNotApplicableToAdministratorAccount, refusal.ConditionCode);
        });

        // La cuenta sigue habilitada, con su credencial y sin marca: INV-08 e INV-09 intactos.
        Assert.Equal(AccountStatus.Enabled, administrator.Status);
        Assert.Equal("derivado", administrator.PasswordHash);
        Assert.False(administrator.MustChangePassword);
        Assert.True(administrator.EvaluateAdmission().IsAdmissible);
    }

    // ---- CU-13 · el reseteo de la contraseña ------------------------------------------------

    /// <summary>
    /// CU-13 CA-01 — el reseteo fija la provisoria y pone la marca, **sin cambiar ningún otro
    /// atributo**: ni la situación, ni el papel, ni la identidad, ni el correo.
    /// </summary>
    [Fact]
    public void ResettingReplacesTheCredentialAndSetsTheMarkAndTouchesNothingElse()
    {
        var account = AStudent(AccountStatus.Enabled, "derivado-provisorio");
        Assert.True(account.ReplaceCredential("derivado-elegido", true).Succeeded);

        var (id, email, normalized, role, status, createdAt) =
            (account.Id, account.Email, account.NormalizedEmail, account.Role, account.Status, account.CreatedAt);

        var result = account.ResetPassword("derivado-de-la-provisoria", worksCascadeDeclared: false);

        Assert.True(result.Succeeded);
        Assert.Equal("derivado-de-la-provisoria", account.PasswordHash);
        Assert.True(account.MustChangePassword);
        Assert.Equal(id, account.Id);
        Assert.Equal(email, account.Email);
        Assert.Equal(normalized, account.NormalizedEmail);
        Assert.Equal(role, account.Role);
        Assert.Equal(status, account.Status);
        Assert.Equal(createdAt, account.CreatedAt);
    }

    /// <summary>
    /// RN-15, CU-13 FA-02 y CA-05 — **el reseteo procede sobre las tres situaciones y no cambia
    /// ninguna**. Sobre la `Pending` que nunca fue habilitada, **fija** en lugar de reemplazar y
    /// **0 rechazos** se producen por la ausencia de credencial previa (FA-03).
    /// </summary>
    [Theory]
    [InlineData(AccountStatus.Pending)]
    [InlineData(AccountStatus.Enabled)]
    [InlineData(AccountStatus.Blocked)]
    public void ResettingProceedsOverEveryStatusAndLeavesItUnchanged(AccountStatus status)
    {
        var account = status == AccountStatus.Pending
            ? AStudent()
            : AStudent(status, "derivado-provisorio");

        if (status == AccountStatus.Pending)
        {
            Assert.Null(account.PasswordHash);
        }

        var result = account.ResetPassword("derivado-nuevo", worksCascadeDeclared: false);

        Assert.True(result.Succeeded);
        Assert.Equal(status, account.Status);
        Assert.Equal("derivado-nuevo", account.PasswordHash);
        Assert.True(account.MustChangePassword);
    }

    /// <summary>CU-13 FA-01 — el segundo reseteo procede y la marca sigue puesta, sin acumularse.</summary>
    [Fact]
    public void ASecondResetProceedsAndTheMarkStaysSet()
    {
        var account = AStudent(AccountStatus.Enabled, "derivado-provisorio");

        Assert.True(account.ResetPassword("derivado-primero", false).Succeeded);
        Assert.True(account.ResetPassword("derivado-segundo", false).Succeeded);

        Assert.Equal("derivado-segundo", account.PasswordHash);
        Assert.True(account.MustChangePassword);
    }

    /// <summary>CU-13 CA-06 — un reseteo que declara arrastre se rechaza (RN-12). Resetear no es dar de baja.</summary>
    [Fact]
    public void ResettingThatDeclaresAWorkCascadeIsRejected()
    {
        var account = AStudent(AccountStatus.Blocked, "derivado-provisorio");

        var result = account.ResetPassword("derivado-nuevo", worksCascadeDeclared: true);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.ResetWithWorkCascade, result.ConditionCode);
        Assert.Equal("derivado-provisorio", account.PasswordHash);
        Assert.Equal(AccountStatus.Blocked, account.Status);
    }

    /// <summary>CU-13 §6 — el valor derivado vacío se rechaza y la credencial anterior se conserva.</summary>
    [Fact]
    public void ResettingWithAnEmptyDerivedValueIsRejected()
    {
        var account = AStudent(AccountStatus.Enabled, "derivado-provisorio");

        var result = account.ResetPassword("   ", worksCascadeDeclared: false);

        Assert.False(result.Succeeded);
        Assert.Equal(ConditionCode.EmptyDerivedValue, result.ConditionCode);
        Assert.Equal("derivado-provisorio", account.PasswordHash);
    }

    // ---- INV-09 · la marca, sus dos fuentes y su única salida -------------------------------

    /// <summary>
    /// CU-04 CA-04 y CA-05 — la cuenta **recién habilitada** y la **reseteada** llegan al mismo
    /// motivo por el mismo camino, y sólo el reemplazo hecho por la propia cuenta lo levanta.
    /// </summary>
    [Fact]
    public void BothOriginsOfTheMarkLeadToTheSameAdmissionReasonAndOnlyTheChangeLiftsIt()
    {
        var justEnabled = AStudent(AccountStatus.Enabled, "derivado-provisorio");
        var justReset = AStudent(AccountStatus.Enabled, "derivado-provisorio");
        Assert.True(justReset.ReplaceCredential("derivado-elegido", true).Succeeded);
        Assert.True(justReset.ResetPassword("derivado-de-la-provisoria", false).Succeeded);

        foreach (var account in new[] { justEnabled, justReset })
        {
            var admission = account.EvaluateAdmission();
            Assert.False(admission.IsAdmissible);
            Assert.Equal(ConditionCode.PasswordChangePending, admission.Reason);

            // Un reemplazo rechazado DEJA LA MARCA PUESTA: 0 caminos la levantan sin cambio efectivo.
            Assert.False(account.ReplaceCredential("derivado-elegido", currentCredentialVerified: false).Succeeded);
            Assert.True(account.MustChangePassword);

            Assert.True(account.ReplaceCredential("derivado-elegido", currentCredentialVerified: true).Succeeded);
            Assert.False(account.MustChangePassword);
            Assert.True(account.EvaluateAdmission().IsAdmissible);
        }
    }
}
