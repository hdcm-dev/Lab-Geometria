using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
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
/// DOS FORMAS DE DECIR QUÉ CUENTA CAMBIA, Y UNA SOLA REGLA DE CAMBIO. `PRODUCT-INTAKE` **1.34**
/// declara que la operación admite dos formas de autenticarse: **con sesión de trabajo** —el
/// cambio corriente, donde la cuenta la nombra el acceso firmado— y **con la contraseña actual**
/// —el cambio forzado, donde la cuenta la nombra su correo y la provisoria que el administrador
/// comunicó es la que autentica—. Las dos desembocan en <see cref="ApplyAsync"/>, que es donde
/// vive la regla: sin la vigente verificada no se cambia nada, en ninguna de las dos.
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

    /// <summary>
    /// Forma **con sesión de trabajo**: la cuenta que cambia es la que el acceso firmado nombra.
    /// </summary>
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

        return await ApplyAsync(
            account, verifyCurrentCredential, deriveNewCredential, requireMark: false, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Forma **con la contraseña actual**: el cambio forzado de una cuenta que todavía no tiene
    /// sesión de trabajo porque RN-13 se la niega. Es la que hace alcanzable la pantalla del
    /// cambio forzado (`PRODUCT-INTAKE` 1.34).
    /// </summary>
    /// <remarks>
    /// SÓLO PROCEDE SOBRE UNA CUENTA CON LA MARCA PUESTA, y es la acotación que mantiene esta
    /// forma en el tamaño exacto del problema que vino a resolver: la cuenta sin marca tiene
    /// sesión disponible y cambia por la otra forma. La cuenta sin marca, la inexistente y la que
    /// presenta una contraseña equivocada reciben **el mismo motivo**, de modo que desde afuera
    /// no se distingue cuál de las tres ocurrió y la marca de una cuenta ajena no es averiguable.
    ///
    /// EL ORDEN IMPORTA: primero se comprueba la credencial y después la marca. Al revés, el
    /// tiempo de respuesta delataría qué cuentas están marcadas, porque la comprobación de
    /// credencial es la parte cara.
    /// </remarks>
    /// <param name="email">Correo escrito de la cuenta que cambia, tal como llegó del formulario.</param>
    /// <param name="verifyCurrentCredential">Comprobación de la contraseña vigente —la provisoria— presentada.</param>
    /// <param name="deriveNewCredential">Derivación de la contraseña nueva, que se invoca al final.</param>
    public async Task<ApplicationResult> ExecuteWithCurrentCredentialAsync(
        string? email,
        Func<string, CredentialCheck> verifyCurrentCredential,
        Func<string> deriveNewCredential,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(verifyCurrentCredential);
        ArgumentNullException.ThrowIfNull(deriveNewCredential);

        var normalizedEmail = EmailIdentity.Normalize(email);
        var account = await _accounts
            .FindByNormalizedEmailAsync(normalizedEmail, cancellationToken)
            .ConfigureAwait(false);

        if (account is null)
        {
            // No se distingue hacia afuera de una contraseña equivocada, por el mismo motivo que
            // en el canje: distinguirlas permitiría averiguar qué correos están registrados.
            return ApplicationResult.Rejected(ApplicationConditionCode.AccountNotFound);
        }

        return await ApplyAsync(
            account, verifyCurrentCredential, deriveNewCredential, requireMark: true, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>La regla del cambio, que es la misma para las dos formas de autenticarse.</summary>
    private async Task<ApplicationResult> ApplyAsync(
        Account account,
        Func<string, CredentialCheck> verifyCurrentCredential,
        Func<string> deriveNewCredential,
        bool requireMark,
        CancellationToken cancellationToken)
    {
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

        if (requireMark && !account.MustChangePassword)
        {
            // La forma sin sesión es la del cambio FORZADO y nada más. Sin marca, el camino es
            // el otro, y el motivo es el mismo para que desde afuera no se note la diferencia.
            return ApplicationResult.Rejected(ConditionCode.CurrentCredentialNotVerified);
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
