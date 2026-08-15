using GeometriaFactory.Domain.Guards;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Domain.Entities;

/// <summary>
/// Cuenta de la comisión. Una fila por persona, sea alumno o administrador.
/// </summary>
/// <remarks>
/// ETAPA `c` (`Domain BT-06`): la entidad se modeló con los atributos de
/// `Definicion-Modelo-De-Dominio.md` §2.1 y con las tres operaciones que las capacidades `F-01`
/// y `F-05` ejercen — constituir la cuenta de administrador (CU-12), reemplazar la credencial
/// derivada (CU-03 FA-01 y FA-04) y evaluar la admisibilidad (CU-04)—.
///
/// ETAPA `d`: entran las tres que faltaban del ciclo de vida — constituir el auto-registro del
/// alumno (CU-01), gobernar las cuatro operaciones del administrador (CU-02) y resetear la
/// contraseña (CU-13)—. Lo único que sigue sin modelar es **el conjunto de trabajos**, que es
/// de la etapa `e`: por eso el arrastre de la baja y la conservación del reseteo se expresan
/// acá como **declaraciones que el consumidor aporta**, y no como una colección que el dominio
/// recorra.
///
/// INV-08 DEJA DE HACERSE CUMPLIR POR AUSENCIA Y PASA A SER EXPLÍCITO, que es exactamente lo que
/// la etapa `c` anunció que iba a pasar acá. Las cinco operaciones que un administrador ejerce
/// sobre una cuenta ajena —habilitar, bloquear, rehabilitar, dar de baja y resetear— comprueban
/// el papel de la cuenta destino ANTES que ninguna otra cosa y rechazan sobre la cuenta con
/// papel `Administrator` con un solo código (CU-02 §6, CU-13 §6).
///
/// LA MARCA DE INV-09 TIENE DOS FUENTES Y UNA SOLA SALIDA: la ponen la habilitación (RN-16) y el
/// reseteo (RN-14), y la levanta únicamente <see cref="ReplaceCredential"/>, que sólo ejerce la
/// propia cuenta (RN-13).
///
/// La contraseña NUNCA llega acá en claro: `PasswordHash` es el valor ya derivado, y el dominio
/// no conoce la función que lo produjo (intake §17.1.P.5).
/// </remarks>
public sealed class Account
{
    /// <summary>
    /// Constructor de materialización. Lo usa el motor de persistencia y nadie más: los dos
    /// caminos de alta del producto son <see cref="ConfigureAdministrator"/> y
    /// <see cref="Register"/>, y no hay un tercero.
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
    /// CU-01 — Constituye la cuenta de un alumno que se auto-registra.
    /// </summary>
    /// <remarks>
    /// ES EL OTRO CAMINO DE ALTA, y tiene las reglas opuestas al de <see cref="ConfigureAdministrator"/>:
    /// nace `Pending`, **sin credencial derivada** y con papel `Student`. Por eso son dos
    /// operaciones y no una con una bandera: una bandera habría dejado que el estado inicial y la
    /// credencial se eligieran desde afuera, que es precisamente lo que los dos rechazos de
    /// `ESTADO_INICIAL_NO_NEGOCIABLE` y `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` impiden.
    ///
    /// EL REGISTRO ES ANÓNIMO Y ASÍ DEBE SEGUIR (`PRODUCT-INTAKE` 1.15 §4.1): lo que RN-16
    /// suprimió es la escritura anónima **de credencial**, no toda escritura anónima. Que este
    /// camino no admita contraseña es lo que lo mantiene del lado correcto de esa distinción.
    /// </remarks>
    /// <param name="email">Correo escrito.</param>
    /// <param name="firstName">Nombre.</param>
    /// <param name="lastName">Apellido.</param>
    /// <param name="passwordHash">
    /// Credencial derivada. **Tiene que llegar sin valor**: el auto-registro no elige contraseña.
    /// El parámetro existe para poder rechazarla, no para poder aportarla (CU-01 CA-04).
    /// </param>
    /// <param name="emailUniquenessVerified">El consumidor declara que comprobó que el correo está libre (RN-02, INV-01).</param>
    /// <param name="requestedRole">Papel con el que se pide constituirla. Sólo `Student` procede (FA-01).</param>
    /// <param name="requestedStatus">Estado con el que se pide constituirla. Sólo `Pending` procede.</param>
    /// <param name="createdAt">Momento de alta, aportado por el consumidor.</param>
    public static DomainResult<Account> Register(
        string? email,
        string? firstName,
        string? lastName,
        string? passwordHash,
        bool emailUniquenessVerified,
        Role requestedRole,
        AccountStatus requestedStatus,
        DateTimeOffset createdAt)
    {
        // El orden es el del flujo principal de CU-01 §4, paso por paso.
        if (string.IsNullOrWhiteSpace(email)
            || string.IsNullOrWhiteSpace(firstName)
            || string.IsNullOrWhiteSpace(lastName))
        {
            return DomainResult<Account>.Rejected(ConditionCode.RequiredFieldMissing);
        }

        if (!emailUniquenessVerified)
        {
            return DomainResult<Account>.Rejected(ConditionCode.EmailUniquenessNotVerified);
        }

        if (!string.IsNullOrWhiteSpace(passwordHash))
        {
            return DomainResult<Account>.Rejected(ConditionCode.CredentialNotAllowedOnRegistration);
        }

        if (requestedRole != Role.Student)
        {
            return DomainResult<Account>.Rejected(ConditionCode.AdministratorRoleOutsideThisPath);
        }

        if (requestedStatus != AccountStatus.Pending)
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
            Role = Role.Student,
            Status = AccountStatus.Pending,
            // Sin credencial y sin marca: la credencial la recibe en el acto de habilitación,
            // con la provisoria que el sistema produce (RN-16).
            PasswordHash = null,
            MustChangePassword = false,
            CreatedAt = createdAt,
        };

        return DomainResult<Account>.Applied(account);
    }

    /// <summary>
    /// CU-02 — Habilita o rehabilita la cuenta, fijando la credencial derivada provisoria.
    /// </summary>
    /// <remarks>
    /// HABILITAR Y REHABILITAR SON LA MISMA TRANSICIÓN, y no hace falta distinguirlas: las dos
    /// llevan a `Enabled`, las dos exigen la provisoria y las dos dejan la marca puesta (RN-16,
    /// `Contracts CU-02` FA-05). Lo único que cambia es el estado de partida.
    ///
    /// FIJAR LA CREDENCIAL Y PONER LA MARCA SON UN SOLO ACTO. Separarlos produciría exactamente
    /// la ventana que RN-16 cierra: una cuenta `Enabled` sin credencial, alcanzable por
    /// cualquiera que conociera el correo (CU-02 §10).
    ///
    /// FA-02 — HABILITAR UNA CUENTA YA `Enabled` NO TIENE EFECTO Y NO SE RECHAZA: la operación es
    /// idempotente respecto del estado, y **no fija credencial ni pone marca**. Producir una
    /// provisoria nueva sin que la haya pedido nadie dejaría al alumno fuera de su propia cuenta.
    /// </remarks>
    /// <param name="provisionalPasswordHash">
    /// Credencial derivada de la contraseña provisoria que el sistema produjo. El dominio no la
    /// produce y nunca la conoce en claro (RN-14).
    /// </param>
    public DomainResult Enable(string? provisionalPasswordHash)
    {
        if (Role == Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.OperationNotApplicableToAdministratorAccount);
        }

        // FA-02: sin transición no hay provisoria nueva y no hay marca nueva.
        if (Status == AccountStatus.Enabled)
        {
            return DomainResult.Applied();
        }

        if (string.IsNullOrWhiteSpace(provisionalPasswordHash))
        {
            return DomainResult.Rejected(ConditionCode.EnableWithoutTemporaryCredential);
        }

        Status = AccountStatus.Enabled;
        PasswordHash = provisionalPasswordHash;
        // FA-04: si ya estaba puesta, sigue puesta. La marca no se acumula.
        MustChangePassword = true;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-02 — Bloquea la cuenta. Es la única de las cuatro operaciones que no toca la credencial.
    /// </summary>
    /// <remarks>
    /// FA-03 — BLOQUEAR UNA CUENTA `Pending` NO ESTÁ EN LA TABLA DE TRANSICIONES y se rechaza:
    /// el dominio no la infiere (CU-02 CA-03).
    ///
    /// BLOQUEAR UNA CUENTA YA `Blocked` TAMPOCO ESTÁ EN LA TABLA. **[decisión derivada de la
    /// etapa `d`, declarada]**: se rechaza con el mismo código, por el criterio de FA-03 —el par
    /// que la tabla no declara se rechaza y no se infiere—. La idempotencia de FA-02 es una
    /// excepción **declarada para habilitar** y no una regla general del contrato; extenderla
    /// acá habría sido inventar una segunda excepción que ninguna fuente enuncia.
    /// </remarks>
    public DomainResult Block()
    {
        if (Role == Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.OperationNotApplicableToAdministratorAccount);
        }

        if (Status != AccountStatus.Enabled)
        {
            return DomainResult.Rejected(ConditionCode.AccountTransitionNotAllowed);
        }

        Status = AccountStatus.Blocked;

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-02 FA-01 — Admite la baja física de la cuenta, con el arrastre de sus trabajos.
    /// </summary>
    /// <remarks>
    /// EL DOMINIO NO ELIMINA NADA, Y POR ESO ESTA OPERACIÓN NO CAMBIA NINGÚN ATRIBUTO: lo que
    /// hace es **admitir o rechazar** la baja, para que el consumidor la materialice como una
    /// sola unidad de trabajo. La baja es física y no un estado, de modo que no aparece en la
    /// máquina de estados como destino sino como salida del ciclo de vida (CU-02 §10).
    ///
    /// EL CONJUNTO DE TRABAJOS NO ESTÁ MODELADO TODAVÍA —es de la etapa `e`—, y por eso el
    /// arrastre llega **declarado** en lugar de recorrido. La condición que el dominio hace
    /// cumplir es la de RN-07: no se admite una baja que declare conservar los trabajos.
    ///
    /// LA CONFIRMACIÓN ESCRITA DEL CORREO NO SE COMPRUEBA ACÁ: el dominio la exige y quien la
    /// recoge y la compara es la capa de aplicación, que conoce el correo escrito (CU-02 §10).
    /// </remarks>
    /// <param name="worksCascadeDeclared">
    /// El consumidor declara que la baja arrastra **todos** los trabajos de la cuenta, en
    /// cualquier estado, incluidos los terminales (RN-07).
    /// </param>
    public DomainResult AdmitDeletion(bool worksCascadeDeclared)
    {
        if (Role == Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.OperationNotApplicableToAdministratorAccount);
        }

        if (!worksCascadeDeclared)
        {
            return DomainResult.Rejected(ConditionCode.DeletionWithoutWorkCascade);
        }

        return DomainResult.Applied();
    }

    /// <summary>
    /// CU-13 — Resetea la contraseña: fija la provisoria y pone la marca, sin tocar nada más.
    /// </summary>
    /// <remarks>
    /// NO ES UNA TRANSICIÓN DE LA MÁQUINA DE ESTADOS DE CUENTA (RN-15). Procede sobre `Pending`,
    /// `Enabled` y `Blocked`, y **el estado vuelve sin cambio**: acá no hay ninguna comprobación
    /// de estado, y su ausencia es la forma estructural de la regla. Una cuenta `Blocked`
    /// reseteada sigue sin obtener acceso, pero por INV-06 y no por este acto.
    ///
    /// FA-03 — SOBRE UNA CUENTA SIN CREDENCIAL, **FIJA** EN LUGAR DE REEMPLAZAR, y procede. Es el
    /// mismo acto con la misma postcondición: el rechazo `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA`
    /// quedó retirado en CU-13 1.3 y **no se recicla**.
    ///
    /// FA-01 — SOBRE UNA CUENTA YA MARCADA, PROCEDE Y LA MARCA SIGUE PUESTA: es el caso del
    /// alumno que también perdió la provisoria antes de usarla.
    ///
    /// RESETEAR NO ES DAR DE BAJA, y es la distinción que esta operación existe para hacer
    /// imposible de confundir: no toca los trabajos, no toca el estado y no dispara RN-07.
    /// </remarks>
    /// <param name="provisionalPasswordHash">Credencial derivada de la provisoria que el sistema produjo.</param>
    /// <param name="worksCascadeDeclared">
    /// Si el consumidor declara que el reseteo elimina los trabajos o cambia el estado de cuenta.
    /// **Tiene que llegar en falso**: el parámetro existe para poder rechazar esa declaración
    /// (RN-12, CU-13 CA-06), no para poder hacerla.
    /// </param>
    public DomainResult ResetPassword(string? provisionalPasswordHash, bool worksCascadeDeclared)
    {
        if (Role == Role.Administrator)
        {
            return DomainResult.Rejected(ConditionCode.OperationNotApplicableToAdministratorAccount);
        }

        if (worksCascadeDeclared)
        {
            return DomainResult.Rejected(ConditionCode.ResetWithWorkCascade);
        }

        if (string.IsNullOrWhiteSpace(provisionalPasswordHash))
        {
            return DomainResult.Rejected(ConditionCode.EmptyDerivedValue);
        }

        // Los dos efectos son un solo acto: no hay camino por el que uno ocurra sin el otro.
        PasswordHash = provisionalPasswordHash;
        MustChangePassword = true;

        // El estado de cuenta, el papel, la identidad y el correo quedan exactamente como estaban.
        return DomainResult.Applied();
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
