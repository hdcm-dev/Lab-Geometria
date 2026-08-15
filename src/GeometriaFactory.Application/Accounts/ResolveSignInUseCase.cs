using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-03 — Resuelve si una cuenta admite el ingreso, y con qué motivo si no lo admite.
/// </summary>
/// <remarks>
/// NO emite el acceso y NO deriva la contraseña: los dos mecanismos pertenecen a las capas
/// externas. Lo que hace es ordenar las comprobaciones y devolver la identidad y el papel, que
/// es lo que el consumidor necesita para resolver el ingreso.
///
/// EL ORDEN ES EL DE `Api CU-01` §4, y no otro: primero la admisibilidad de la cuenta (paso 2) y
/// después la comprobación de la credencial (paso 4). Tiene una consecuencia que conviene tener
/// escrita: una cuenta que existe y no admite acceso responde con su motivo aunque la contraseña
/// presentada sea la equivocada. Es lo que el intake §17.5.P.5 pide —`403` **con motivo** ante
/// cuenta `Pending` o `Blocked`, para que la persona sepa en qué situación está—, y el precio
/// declarado es que ese `403` confirma que el correo existe. Invertir el orden protegería el
/// correo y rompería el criterio de aceptación, así que se respeta la fuente.
///
/// LA CREDENCIAL DERIVADA NO SALE DE ACÁ: el consumidor aporta la comprobación como función y
/// esta capa la invoca sobre el valor que ella misma recuperó.
/// </remarks>
public sealed class ResolveSignInUseCase
{
    private readonly IAccountRepository _accounts;

    public ResolveSignInUseCase(IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        _accounts = accounts;
    }

    /// <param name="email">Correo escrito, tal como llegó del formulario.</param>
    /// <param name="verifyCredential">
    /// Comprobación de la contraseña presentada contra el valor derivado guardado, que el
    /// consumidor provee y esta capa invoca. Recibe el valor derivado; no devuelve nada de él.
    /// </param>
    public async Task<ApplicationResult<AccountIdentity>> ExecuteAsync(
        string? email,
        Func<string, CredentialCheck> verifyCredential,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(verifyCredential);

        var normalizedEmail = EmailIdentity.Normalize(email);
        var account = await _accounts
            .FindByNormalizedEmailAsync(normalizedEmail, cancellationToken)
            .ConfigureAwait(false);

        if (account is null)
        {
            // No se distingue hacia afuera de una contraseña equivocada: distinguirlas permitiría
            // averiguar por tanteo qué correos están registrados (CU-03 §6, `Api CU-01` §6).
            return ApplicationResult<AccountIdentity>.Rejected(ApplicationConditionCode.AccountNotFound);
        }

        var admission = account.EvaluateAdmission();
        if (!admission.IsAdmissible)
        {
            return ApplicationResult<AccountIdentity>.Rejected(admission.Reason!);
        }

        // La cuenta admisible tiene credencial derivada: la única combinación que la dejaría sin
        // ella —`Enabled` sin credencial— dejó de ser posible con RN-16 (`Domain CU-04` §1).
        var check = verifyCredential(account.PasswordHash!);
        if (check != CredentialCheck.Matches)
        {
            return ApplicationResult<AccountIdentity>.Rejected(
                check == CredentialCheck.Unreadable
                    ? InfrastructureConditionCode.UnreadablePasswordHash
                    : ConditionCode.CurrentCredentialNotVerified);
        }

        return ApplicationResult<AccountIdentity>.Applied(
            new AccountIdentity(account.Id, account.Email, account.Role));
    }
}
