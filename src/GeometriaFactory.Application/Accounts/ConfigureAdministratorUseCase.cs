using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-10 — Configura la única cuenta con papel `Administrator` de la instancia.
/// </summary>
/// <remarks>
/// Es el segundo camino de alta del producto; el otro es el auto-registro del alumno, que es
/// CU-01 y llega con la etapa `d`. Los dos caminos tienen reglas opuestas y por eso son dos
/// contratos y no uno con un flujo alternativo.
///
/// LA CONTRASEÑA EN CLARO NO ATRAVIESA ESTA CAPA: el consumidor aporta el valor YA DERIVADO
/// (CU-10 §3). Acá no hay ningún parámetro que pueda llevarla.
/// </remarks>
public sealed class ConfigureAdministratorUseCase
{
    private readonly IAccountRepository _accounts;
    private readonly ISystemClock _clock;

    public ConfigureAdministratorUseCase(IAccountRepository accounts, ISystemClock clock)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        ArgumentNullException.ThrowIfNull(clock);

        _accounts = accounts;
        _clock = clock;
    }

    /// <summary>Ejecuta el flujo principal de CU-10 §4, en su orden.</summary>
    public async Task<ApplicationResult<AccountIdentity>> ExecuteAsync(
        string? email,
        string? firstName,
        string? lastName,
        string? passwordHash,
        CancellationToken cancellationToken = default)
    {
        // Paso 2 — ¿ya hay administrador? La ventana de alta se cierra con la primera
        // configuración y no vuelve a abrirse (FA-01). No se consulta el correo ni se escribe nada.
        if (await _accounts.AdministratorExistsAsync(cancellationToken).ConfigureAwait(false))
        {
            return ApplicationResult<AccountIdentity>.Rejected(ConditionCode.AdministratorAlreadyConfigured);
        }

        var normalizedEmail = EmailIdentity.Normalize(email);

        // Paso 3 — ¿el correo está libre? No se informa el papel ni el estado de la cuenta que lo ocupa.
        if (!string.IsNullOrWhiteSpace(normalizedEmail)
            && await _accounts.EmailIsRegisteredAsync(normalizedEmail, cancellationToken).ConfigureAwait(false))
        {
            return ApplicationResult<AccountIdentity>.Rejected(ApplicationConditionCode.EmailAlreadyRegistered);
        }

        // Paso 4 — el sello de alta sale del puerto de reloj, para que sea verificable en prueba.
        var createdAt = _clock.UtcNow;

        // Pasos 5 y 6 — el dominio constituye, declarando las dos comprobaciones ya hechas.
        var constitution = Account.ConfigureAdministrator(
            email,
            firstName,
            lastName,
            passwordHash,
            administratorAbsenceDeclared: true,
            emailUniquenessVerified: true,
            requestedStatus: AccountStatus.Enabled,
            createdAt: createdAt);

        if (!constitution.Succeeded)
        {
            return ApplicationResult<AccountIdentity>.Rejected(constitution.ConditionCode!);
        }

        var account = constitution.Value!;

        // Paso 7 — una única unidad de trabajo. Antes de esta línea no se escribió nada.
        await _accounts.AddAsync(account, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<AccountIdentity>.Applied(
            new AccountIdentity(account.Id, account.Email, account.Role));
    }
}
