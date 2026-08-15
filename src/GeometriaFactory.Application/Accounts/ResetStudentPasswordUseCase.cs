using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-11 — Orquesta el reseteo de la contraseña de un alumno por parte del administrador.
/// </summary>
/// <remarks>
/// ES LA OPERACIÓN QUE CIERRA UN AGUJERO DE DISEÑO: hasta que existió, el único remedio de un
/// olvido de contraseña era dar de baja y volver a dar de alta, y la baja arrastra **todos** los
/// trabajos (RN-07). Acá **no se arrastra nada**: la cuenta conserva su situación, su papel, su
/// identidad y todos sus trabajos con sus estados y comentarios (RN-12).
///
/// NO SE COMPRUEBA LA SITUACIÓN DE LA CUENTA, y la ausencia es la regla. El reseteo procede sobre
/// `Pending`, `Enabled` y `Blocked` sin cambiarles la situación: **no es una transición de la
/// máquina de estados** (RN-15). El administrador puede resetear y habilitar en el orden que
/// quiera, y los dos órdenes terminan en el mismo lugar (CU-11 CA-07).
///
/// TAMPOCO SE COMPRUEBA QUE LA CUENTA TENGA CREDENCIAL. Sobre la cuenta `Pending` que nunca fue
/// habilitada, el dominio **fija** en lugar de reemplazar, y procede (FA-04). El motivo que se
/// propagaba salió del catálogo con **RN-16** y **no se recicla**.
///
/// ACÁ NO SE DECLARA NINGUNA CREDENCIAL VIGENTE VERIFICADA, y es por lo que este caso de uso
/// invoca `Domain CU-13` y no el reemplazo de `Domain CU-03`: el administrador no conoce la
/// contraseña del alumno ni la conocerá. Lo que sostiene la operación es la **verificación de
/// facultad**, ejercida antes de invocar al dominio.
///
/// LA PROVISORIA NO LA ELIGE ESTA CAPA Y NO SE PERSISTE EN CLARO. Llega **producida** por una
/// función sin parámetros —la forma estructural de RN-14— y se guarda derivada. El valor en
/// claro se devuelve una sola vez y esta capa no lo conserva después de devolverlo.
/// </remarks>
public sealed class ResetStudentPasswordUseCase
{
    private readonly IAccountRepository _accounts;

    public ResetStudentPasswordUseCase(IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        _accounts = accounts;
    }

    /// <summary>Ejecuta el flujo principal de CU-11 §4, en su orden.</summary>
    /// <param name="requesterRole">Papel de quien pide el reseteo, tomado del acceso firmado.</param>
    /// <param name="accountId">Identidad de la cuenta destino.</param>
    /// <param name="produceProvisionalPassword">
    /// Producción de la provisoria. **Sin parámetros**: no puede derivar el valor de ningún dato
    /// de la cuenta ni del acto (RN-14, `Infrastructure ADR-05` §2). Devuelve nulo cuando la
    /// fuente de material impredecible no respondió, y entonces **no se completa el reseteo**.
    /// </param>
    /// <param name="deriveCredential">Derivación del valor en claro. El dominio nunca lo conoce.</param>
    public async Task<ApplicationResult<ProvisionalCredentialOutcome>> ExecuteAsync(
        Role requesterRole,
        Guid accountId,
        Func<string?> produceProvisionalPassword,
        Func<string, string?> deriveCredential,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(produceProvisionalPassword);
        ArgumentNullException.ThrowIfNull(deriveCredential);

        // Paso 2 — la facultad, sin recuperar la cuenta destino ni tocar ninguna credencial.
        if (requesterRole != Role.Administrator)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ApplicationConditionCode.AdministratorRoleRequired);
        }

        // Paso 3 — la cuenta destino. Acá no se oculta nada, porque la operación ya exigió la
        // facultad de administrador (CU-11 §6).
        var account = await _accounts.FindByIdAsync(accountId, cancellationToken).ConfigureAwait(false);
        if (account is null)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ApplicationConditionCode.AccountNotFound);
        }

        // Paso 4 — el acotamiento es DE PAPEL y no de situación de cuenta (FA-02, RN-15, INV-08).
        // Se comprueba antes de pedir la provisoria, para no gastar material impredecible en una
        // operación que no va a proceder.
        if (account.Role == Role.Administrator)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                ApplicationConditionCode.ResetLimitedToStudentAccounts);
        }

        var provisionalPassword = produceProvisionalPassword();
        if (provisionalPassword is null)
        {
            // Sin fuente no hay valor, y no se compone uno por otro medio: un reseteo que no se
            // completa es recuperable y una provisoria adivinable no se nota hasta que alguien
            // la usa (`Infrastructure CU-07` §6).
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(
                InfrastructureConditionCode.RandomnessSourceUnavailable);
        }

        // Paso 5 — el reseteo del dominio: reemplaza la credencial y pone la marca en un solo
        // acto, y **no declara ningún efecto sobre los trabajos ni sobre la situación** (RN-12).
        var reset = account.ResetPassword(deriveCredential(provisionalPassword), worksCascadeDeclared: false);
        if (!reset.Succeeded)
        {
            return ApplicationResult<ProvisionalCredentialOutcome>.Rejected(reset.ConditionCode!);
        }

        // Paso 6 — una única unidad de trabajo: credencial y marca, o nada.
        await _accounts.UpdateAsync(account, cancellationToken).ConfigureAwait(false);

        // Paso 7 — la situación que vuelve es la que la cuenta ya tenía: el reseteo no la cambió.
        return ApplicationResult<ProvisionalCredentialOutcome>.Applied(
            new ProvisionalCredentialOutcome(account.Status, provisionalPassword, account.MustChangePassword));
    }
}
