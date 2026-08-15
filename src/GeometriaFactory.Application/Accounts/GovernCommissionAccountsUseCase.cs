using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-02 — Orquesta el gobierno de las cuentas de la comisión: el listado y las cuatro
/// operaciones del administrador —habilitar, bloquear, rehabilitar y dar de baja—.
/// </summary>
/// <remarks>
/// LA VERIFICACIÓN DE FACULTAD SE EJERCE ACÁ, y no ocultando un control en la pantalla: las
/// cinco operaciones empiezan comprobando que quien las pide tenga papel `Administrator` (RN-01),
/// y ninguna recupera la cuenta destino antes de esa comprobación.
///
/// HABILITAR Y REHABILITAR PRODUCEN LA CONTRASEÑA PROVISORIA (RN-16), y son las dos únicas
/// operaciones de este contrato que escriben credencial. Bloquear conserva la credencial y la
/// marca tal como estaban; la baja se lleva la cuenta entera, marca incluida.
///
/// CÓMO ENTRAN LA PRODUCCIÓN Y LA DERIVACIÓN, Y POR QUÉ NO SON PUERTOS. Las dos llegan **como
/// función que el consumidor aporta**, exactamente igual que la comprobación de credencial de
/// `CU-03`. El valor en claro **no se guarda en esta capa** y el mecanismo **no entra**. Es lo
/// que sostiene que los puertos del producto sigan siendo cuatro, como `Infrastructure CU-07`
/// §10 declara al remitir a `Application` §8, y lo que hace cuadrar la puerta `QG-10`.
/// **`Application CU-02` §2 y §9 los enumeran como puertos**, y esa lectura contradice a las
/// otras dos; se resuelve del lado de las dos que coinciden y queda elevada al Product Owner.
///
/// EL LISTADO NO TIENE CONTRATO DE USO PROPIO EN ESTA CAPA. `Api CU-04` §4 paso 2 declara que se
/// le pide **a la capa de aplicación**, y ningún caso de uso de los once lo declara. Se ubica acá
/// —y no en un caso de uso nuevo— porque `Api CU-04` agrupa los tres puntos de gobierno en un
/// solo contrato y el título de este caso de uso es el gobierno de las cuentas de la comisión.
/// **Es una decisión derivada de la etapa `d` y está elevada al Product Owner.**
///
/// EL SELLO DE MODIFICACIÓN NO SE REGISTRA, Y NO ES UN OLVIDO. `CU-02` §2 declara que el reloj
/// provee un sello de modificación cuando la operación escribe credencial, y
/// `Modelo-Datos-Logico.md` §2.1 declara con todas las letras que **no hay columna de momento de
/// última modificación de la cuenta** y que sigue como punto abierto del Product Owner. Escribir
/// una columna que el modelo de datos declara ausente sería inventar esquema; se deja sin
/// registrar y se eleva.
/// </remarks>
public sealed class GovernCommissionAccountsUseCase
{
    private readonly IAccountRepository _accounts;

    public GovernCommissionAccountsUseCase(IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        _accounts = accounts;
    }

    /// <summary>
    /// El listado de cuentas de la comisión, con su situación y su marca (`Api CU-04` `A-06`).
    /// </summary>
    /// <remarks>
    /// UN LISTADO VACÍO NO ES UN FALLO: se devuelve una colección vacía y quien la consume
    /// distingue vacío de fallo por el tipo recibido y no por el conteo (`Api CU-04` FA-02).
    /// </remarks>
    public async Task<ApplicationResult<IReadOnlyList<AccountSnapshot>>> ListAsync(
        Role requesterRole,
        CancellationToken cancellationToken = default)
    {
        if (requesterRole != Role.Administrator)
        {
            return ApplicationResult<IReadOnlyList<AccountSnapshot>>.Rejected(
                ApplicationConditionCode.AdministratorRoleRequired);
        }

        var accounts = await _accounts.ListAsync(cancellationToken).ConfigureAwait(false);

        IReadOnlyList<AccountSnapshot> snapshots = [.. accounts.Select(AccountSnapshot.Of)];

        return ApplicationResult<IReadOnlyList<AccountSnapshot>>.Applied(snapshots);
    }

    /// <summary>
    /// Cambia la situación de una cuenta: habilitar, rehabilitar o bloquear (`Api CU-04` `A-07`).
    /// </summary>
    /// <remarks>
    /// EL ORDEN DEL FLUJO ES EL DE CU-02 §4, y el paso 4 sólo ocurre cuando la situación
    /// pretendida es habilitada: **el bloqueo no pide provisoria al productor**, y habilitar una
    /// cuenta que ya está habilitada tampoco (FA-05). Producir una sin que la haya pedido nadie
    /// dejaría al alumno fuera de su propia cuenta; para eso está el reseteo, que es explícito.
    ///
    /// SI EL PRODUCTOR NO ENTREGA VALOR, LA TRANSICIÓN SE INVOCA IGUAL Y EL DOMINIO LA RECHAZA
    /// con `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL`: es el motivo que CU-02 CA-07 exige, y hacerlo
    /// así garantiza que **0 cuentas** queden `Enabled` sin credencial por un camino de esta capa.
    /// </remarks>
    /// <param name="requesterRole">Papel de quien pide la operación, tomado del acceso firmado.</param>
    /// <param name="accountId">Identidad de la cuenta destino.</param>
    /// <param name="intendedStatus">
    /// Situación pretendida. `Enabled` habilita o rehabilita; `Blocked` bloquea. `Pending` no es
    /// destino de ninguna transición declarada y se rechaza.
    /// </param>
    /// <param name="produceProvisionalPassword">
    /// Producción de la contraseña provisoria. **No recibe ningún parámetro**, que es la forma
    /// estructural de RN-14: no puede derivar el valor de ningún dato de la cuenta. Devuelve
    /// nulo cuando la fuente de material impredecible no respondió.
    /// </param>
    /// <param name="deriveCredential">Derivación del valor en claro. El dominio nunca lo conoce.</param>
    public async Task<ApplicationResult<ProvisionalCredentialOutcome>> ChangeStatusAsync(
        Role requesterRole,
        Guid accountId,
        AccountStatus intendedStatus,
        Func<string?> produceProvisionalPassword,
        Func<string, string?> deriveCredential,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(produceProvisionalPassword);
        ArgumentNullException.ThrowIfNull(deriveCredential);

        // Paso 2 — la facultad, antes de recuperar la cuenta destino (FA-01).
        if (requesterRole != Role.Administrator)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ApplicationConditionCode.AdministratorRoleRequired);
        }

        // Paso 3 — la cuenta destino.
        var account = await _accounts.FindByIdAsync(accountId, cancellationToken).ConfigureAwait(false);
        if (account is null)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ApplicationConditionCode.AccountNotFound);
        }

        // Las cuatro operaciones están cerradas sobre la cuenta de administrador (INV-08), y se
        // comprueba ANTES de pedir ninguna provisoria: producir una que después se descarta sería
        // gastar material impredecible en una operación que no va a proceder. El dominio la
        // rechaza igual y por su cuenta; esta guarda no lo reemplaza, lo adelanta.
        if (account.Role == Role.Administrator)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ConditionCode.OperationNotApplicableToAdministratorAccount);
        }

        string? provisionalPassword = null;
        Domain.Guards.DomainResult transition;

        if (intendedStatus == AccountStatus.Enabled)
        {
            // Paso 4 — la provisoria, sólo cuando hay transición que la pida (FA-05).
            var alreadyEnabled = account.Status == AccountStatus.Enabled;

            string? derived = null;
            if (!alreadyEnabled)
            {
                provisionalPassword = produceProvisionalPassword();
                derived = provisionalPassword is null ? null : deriveCredential(provisionalPassword);
            }

            transition = account.Enable(derived);

            if (alreadyEnabled)
            {
                // Sin transición no hay provisoria que comunicar, y por eso no se devuelve ninguna.
                provisionalPassword = null;
            }
        }
        else if (intendedStatus == AccountStatus.Blocked)
        {
            transition = account.Block();
        }
        else
        {
            // `Pending` no es destino de ninguna transición declarada (CU-02 §4 paso 3).
            transition = Domain.Guards.DomainResult.Rejected(ConditionCode.AccountTransitionNotAllowed);
        }

        if (!transition.Succeeded)
        {
            // El valor en claro que se hubiera producido no se conserva ni se devuelve.
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(transition.ConditionCode!);
        }

        // Paso 6 — una única unidad de trabajo.
        await _accounts.UpdateAsync(account, cancellationToken).ConfigureAwait(false);

        // Paso 7 — el valor en claro, UNA SOLA VEZ, para que el consumidor se lo muestre al
        // administrador. Lo que quedó guardado es su forma derivada.
        return ApplicationResult<ProvisionalCredentialOutcome>.Applied(
            new ProvisionalCredentialOutcome(account.Status, provisionalPassword, account.MustChangePassword));
    }

    /// <summary>
    /// FA-02 — Da de baja una cuenta, con el correo escrito como confirmación (`Api CU-04` `A-08`).
    /// </summary>
    /// <remarks>
    /// ES LA ÚNICA OPERACIÓN DESTRUCTIVA DEL PRODUCTO. La confirmación escrita se compara acá,
    /// contra el correo de la **cuenta destino**, y si no coincide **la unidad de trabajo no se
    /// abre**: no se retira ningún trabajo ni la cuenta (RN-07).
    ///
    /// LA COMPARACIÓN ES SOBRE EL CORREO NORMALIZADO, que es la forma que decide la identidad
    /// (INV-01, `Infrastructure ADR-03`). Comparar el escrito habría hecho fallar una
    /// confirmación correcta por una mayúscula, que es exactamente el accidente que la guarda
    /// existe para evitar.
    ///
    /// LA BAJA DEJÓ DE SER EL REMEDIO DEL OLVIDO DE CONTRASEÑA: ése es el reseteo de `CU-11`,
    /// que conserva la cuenta y todos sus trabajos (RN-12).
    /// </remarks>
    public async Task<ApplicationResult> DeleteAsync(
        Role requesterRole,
        Guid accountId,
        string? confirmationEmail,
        CancellationToken cancellationToken = default)
    {
        if (requesterRole != Role.Administrator)
        {
            return ApplicationResult.Rejected(ApplicationConditionCode.AdministratorRoleRequired);
        }

        var account = await _accounts.FindByIdAsync(accountId, cancellationToken).ConfigureAwait(false);
        if (account is null)
        {
            return ApplicationResult.Rejected(ApplicationConditionCode.AccountNotFound);
        }

        // El dominio admite o rechaza la baja ANTES de que se compare nada más: la cuenta de
        // administrador no admite ninguna de las cuatro operaciones (INV-08), y admitir la
        // confirmación primero habría dejado que el correo del administrador la habilitara.
        var admission = account.AdmitDeletion(worksCascadeDeclared: true);
        if (!admission.Succeeded)
        {
            return ApplicationResult.Rejected(admission.ConditionCode!);
        }

        var written = EmailIdentity.Normalize(confirmationEmail);
        if (!string.Equals(written, account.NormalizedEmail, StringComparison.Ordinal))
        {
            return ApplicationResult.Rejected(ApplicationConditionCode.DeletionConfirmationMismatch);
        }

        // La cuenta y todos sus trabajos, en la misma unidad de trabajo: todo o nada.
        await _accounts.RemoveAsync(account, cancellationToken).ConfigureAwait(false);

        return ApplicationResult.Applied();
    }
}
