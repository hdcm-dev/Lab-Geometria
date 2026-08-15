using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-03 FA-03 y FA-05 — Reemplaza la credencial propia exigiendo la vigente.
/// </summary>
/// <remarks>
/// Es la ÚNICA excepción declarada de la primera comprobación de `Application ADR-04`: una
/// cuenta con la marca de cambio de contraseña pendiente no ejerce ninguna capacidad salvo ésta,
/// que es además lo único que la levanta (RN-13, INV-09).
///
/// LA CONTRASEÑA NUEVA SE DERIVA TARDE, Y ES DELIBERADO: la función de derivación se invoca
/// recién cuando la vigente ya verificó. Derivarla antes gastaría el coste de derivación en cada
/// intento fallido, que es exactamente lo que un intento por tanteo busca.
/// </remarks>
public sealed class ChangeOwnPasswordUseCase
{
    private readonly IAccountRepository _accounts;

    public ChangeOwnPasswordUseCase(IAccountRepository accounts)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        _accounts = accounts;
    }

    /// <param name="accountId">La cuenta que ejerce el cambio, tomada del acceso firmado.</param>
    /// <param name="verifyCurrentCredential">Comprobación de la contraseña vigente presentada.</param>
    /// <param name="deriveNewCredential">Derivación de la contraseña nueva, que se invoca al final.</param>
    public async Task<ApplicationResult> ExecuteAsync(
        Guid accountId,
        Func<string, CredentialCheck> verifyCurrentCredential,
        Func<string> deriveNewCredential,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(verifyCurrentCredential);
        ArgumentNullException.ThrowIfNull(deriveNewCredential);

        var account = await _accounts.FindByIdAsync(accountId, cancellationToken).ConfigureAwait(false);
        if (account is null)
        {
            return ApplicationResult.Rejected(ApplicationConditionCode.AccountNotFound);
        }

        if (string.IsNullOrWhiteSpace(account.PasswordHash))
        {
            return ApplicationResult.Rejected(ConditionCode.CurrentCredentialNotVerified);
        }

        var check = verifyCurrentCredential(account.PasswordHash);
        if (check != CredentialCheck.Matches)
        {
            return ApplicationResult.Rejected(
                check == CredentialCheck.Unreadable
                    ? InfrastructureConditionCode.UnreadablePasswordHash
                    : ConditionCode.CurrentCredentialNotVerified);
        }

        var replacement = account.ReplaceCredential(deriveNewCredential(), currentCredentialVerified: true);
        if (!replacement.Succeeded)
        {
            return ApplicationResult.Rejected(replacement.ConditionCode!);
        }

        await _accounts.UpdateAsync(account, cancellationToken).ConfigureAwait(false);

        return ApplicationResult.Applied();
    }
}
