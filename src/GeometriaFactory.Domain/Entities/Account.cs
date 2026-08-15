using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Cuenta de la comisión. Una fila por persona, sea alumno o administrador.
/// </summary>
/// <remarks>
/// ETAPA `c` (`Domain BT-06`): la entidad se modela con los atributos de
/// `Definicion-Modelo-De-Dominio.md` §2.1 y con las tres operaciones que las capacidades `F-01`
/// y `F-05` ejercen — constituir la cuenta de administrador (CU-12), reemplazar la credencial
/// derivada (CU-03 FA-01 y FA-04) y evaluar la admisibilidad (CU-04)—.
///
/// LO QUE ESTA ETAPA NO MODELA, Y ESTÁ DECLARADO: el auto-registro del alumno (CU-01), el
/// gobierno del ciclo de vida de la cuenta (CU-02), el reseteo (CU-13) y el conjunto de trabajos.
/// Los cuatro son de la etapa `d` o posteriores, y escribirlos acá sería adelantar etapa.
///
/// INV-08 SE HACE CUMPLIR POR AUSENCIA, y es deliberado: la cuenta con papel `Administrator`
/// NACE `Enabled` —lo fija <see cref="ConfigureAdministrator"/>— y esta superficie pública NO
/// declara ninguna operación que cambie el estado de una cuenta. Ninguna operación la lleva a
/// otro estado porque ninguna operación existe. Cuando la etapa `d` traiga las transiciones,
/// la guarda de INV-08 pasa a ser explícita dentro de ellas.
///
/// La contraseña NUNCA llega acá en claro: `PasswordHash` es el valor ya derivado, y el dominio
/// no conoce la función que lo produjo (intake §17.1.P.5).
/// </remarks>
public sealed class Account
{
    /// <summary>
    /// Constructor de materialización. Lo usa el motor de persistencia y nadie más: el único
    /// camino de alta de esta etapa es <see cref="ConfigureAdministrator"/>.
    /// </summary>
    private Account()
    {
        Email = string.Empty;
        NormalizedEmail = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    /// <summary>Identidad propia de la cuenta. No se reutiliza.</summary>
    public Guid Id { get; private set; }

    /// <summary>Correo tal como la persona lo escribió. Es lo que se muestra.</summary>
    public string Email { get; private set; }

    /// <summary>Correo normalizado. Es lo que decide la identidad (INV-01, RN-02).</summary>
    public string NormalizedEmail { get; private set; }

    /// <summary>Nombre de pila declarado en el alta.</summary>
    public string FirstName { get; private set; }

    /// <summary>Apellido declarado en el alta.</summary>
    public string LastName { get; private set; }

    /// <summary>Papel de la cuenta. Conjunto cerrado de dos valores.</summary>
    public Role Role { get; private set; }

    /// <summary>Situación de la cuenta. Conjunto cerrado de tres valores.</summary>
    public AccountStatus Status { get; private set; }

    /// <summary>
    /// Credencial derivada. Nula mientras la cuenta está `Pending`; con valor desde el acto que
    /// la fija. El dominio la recibe YA DERIVADA y nunca en claro.
    /// </summary>
    public string? PasswordHash { get; private set; }

    /// <summary>
    /// Marca de cambio de contraseña pendiente (INV-09). La ponen las dos operaciones que
    /// producen una contraseña provisoria —habilitar (RN-16) y resetear (RN-14)—, y la levanta
    /// ÚNICAMENTE el reemplazo hecho por la propia cuenta.
    /// </summary>
    public bool MustChangePassword { get; private set; }

    /// <summary>Momento en que la cuenta se constituyó. Lo aporta el consumidor: el dominio no lee el reloj.</summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// CU-12 — Constituye la única cuenta con papel `Administrator` de la instancia.
    /// </summary>
    /// <param name="email">Correo escrito.</param>
    /// <param name="firstName">Nombre.</param>
    /// <param name="lastName">Apellido.</param>
    /// <param name="passwordHash">Credencial YA derivada. El dominio no recibe texto en claro.</param>
    /// <param name="administratorAbsenceDeclared">
    /// El consumidor declara que no existe ninguna cuenta con papel `Administrator` (RN-01, INV-05).
    /// El dominio no conoce el conjunto de cuentas y por eso exige la declaración.
    /// </param>
    /// <param name="emailUniquenessVerified">El consumidor declara que comprobó que el correo está libre (RN-02, INV-01).</param>
    /// <param name="requestedStatus">
    /// Estado con el que se pide constituirla. Sólo `Enabled` procede: es la cuenta que habilita
    /// a las demás y ninguna anterior podría habilitarla a ella.
    /// </param>
    /// <param name="createdAt">Momento de alta, aportado por el consumidor.</param>
    public static DomainResult<Account> ConfigureAdministrator(
        string? email,
        string? firstName,
        string? lastName,
        string? passwordHash,
        bool administratorAbsenceDeclared,
        bool emailUniquenessVerified,
        AccountStatus requestedStatus,
        DateTimeOffset createdAt)
    {
        // El orden es el del flujo principal de CU-12 §4, paso por paso.
        if (string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName))
        {
            return DomainResult<Account>.Rejected(ConditionCode.RequiredFieldMissing);
        }

        if (!administratorAbsenceDeclared)
        {
            return DomainResult<Account>.Rejected(ConditionCode.AdministratorAlreadyConfigured);
        }

        if (!emailUniquenessVerified)
        {
            return DomainResult<Account>.Rejected(ConditionCode.EmailUniquenessNotVerified);
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return DomainResult<Account>.Rejected(ConditionCode.SetupWithoutCredential);
        }

        if (requestedStatus != AccountStatus.Enabled)
        {
            return DomainResult<Account>.Rejected(ConditionCode.InitialStatusNotNegotiable);
        }

        var account = new Account
        {
            Id = Guid.NewGuid(),
            Email = email.Trim(),
            NormalizedEmail = EmailIdentity.Normalize(email),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Role = Role.Administrator,
            Status = AccountStatus.Enabled,
            PasswordHash = passwordHash,
            // Nace levantada: la contraseña de este camino la eligió la propia persona, no es
            // una provisoria que otro conozca (`Definicion-Modelo-De-Dominio.md` §2.1).
            MustChangePassword = false,
            CreatedAt = createdAt,
        };

        return DomainResult<Account>.Applied(account);
    }

    /// <summary>
    /// CU-03 FA-01 y FA-04 — Reemplaza la credencial derivada exigiendo la vigente verificada.
    /// </summary>
    /// <remarks>
    /// Es el ÚNICO acto que levanta la marca de cambio de contraseña pendiente, y sólo lo ejerce
    /// la propia cuenta. Los dos efectos —credencial nueva y marca levantada— son un solo acto:
    /// no hay camino por el que uno ocurra sin el otro (CU-03 §7).
    ///
    /// La FIJACIÓN por primera vez —el otro camino de CU-03— no está acá: la ejerce la
    /// habilitación de CU-02, que es de la etapa `d`. La cuenta de administrador no la usa nunca,
    /// porque su credencial nace fijada (CU-03 FA-03).
    /// </remarks>
    public DomainResult ReplaceCredential(string? newPasswordHash, bool currentCredentialVerified)
    {
        if (Status != AccountStatus.Enabled)
        {
            return DomainResult.Rejected(ConditionCode.AccountNotEnabledForCredential);
        }

        if (string.IsNullOrWhiteSpace(PasswordHash))
        {
            // Reemplazar exige que haya algo que reemplazar. Fijar por primera vez es el otro
            // camino de CU-03 y lo ejerce la habilitación.
            return DomainResult.Rejected(ConditionCode.CurrentCredentialNotVerified);
        }

        if (!currentCredentialVerified)
        {
            return DomainResult.Rejected(ConditionCode.CurrentCredentialNotVerified);
        }

        if (string.IsNullOrWhiteSpace(newPasswordHash))
        {
            return DomainResult.Rejected(ConditionCode.EmptyDerivedValue);
        }

        PasswordHash = newPasswordHash;
        MustChangePassword = false;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-04 — Evalúa si la cuenta admite acceso, y con qué motivo si no lo admite.
    /// </summary>
    /// <remarks>
    /// La evaluación es sobre la CUENTA y no sobre la credencial, y no tiene efecto: es la guarda
    /// única por la que se hacen cumplir INV-06 e INV-09 (`Domain ADR-05`). Se resuelve por
    /// estado y por marca, nunca por papel: la autorización por papel es de la capa que expone
    /// los puntos de acceso (CU-04 FA-02).
    /// </remarks>
    public Admission EvaluateAdmission() => Status switch
    {
        AccountStatus.Pending => Admission.NotAdmissible(ConditionCode.AccountPending),
        AccountStatus.Blocked => Admission.NotAdmissible(ConditionCode.AccountBlocked),
        _ when MustChangePassword => Admission.NotAdmissible(ConditionCode.PasswordChangePending),
        _ => Admission.Admissible(),
    };

}
