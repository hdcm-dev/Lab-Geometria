using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// CU-01 — Orquesta el auto-registro de un alumno.
/// </summary>
/// <remarks>
/// ESTE CASO DE USO NO FIJA CREDENCIAL, y no es un olvido: desde **RN-16** la credencial inicial
/// la produce el sistema **al habilitar la cuenta** (CU-02), no el alumno al entrar. La cuenta
/// nace `Pending` y sin credencial derivada, y la persona elige la suya cambiando la provisoria.
///
/// NO CONSTITUYE LA CUENTA DEL ADMINISTRADOR. El producto tiene **dos caminos de alta** con
/// reglas opuestas, y el otro es <see cref="ConfigureAdministratorUseCase"/>.
///
/// LA UNICIDAD DEL CORREO SE VERIFICA ACÁ Y NO EN EL DOMINIO, porque exige conocer el conjunto
/// de cuentas y el dominio verifica sobre una entidad. El motivo `EMAIL_UNIQUENESS_NOT_VERIFIED`
/// que el dominio declara **no es alcanzable desde este caso de uso por construcción**: el paso
/// 4 declara siempre la verificación que el paso 2 hizo (CU-01 §10).
///
/// ESTE CASO DE USO NO VERIFICA PERTENENCIA NI FACULTAD: el auto-registro lo ejerce una persona
/// que todavía no tiene cuenta, y el registro es anónimo por diseño (`PRODUCT-INTAKE` 1.15 §4.1).
/// </remarks>
public sealed class RegisterAccountUseCase
{
    private readonly IAccountRepository _accounts;
    private readonly ISystemClock _clock;
    private readonly RegistrationPolicy _policy;

    /// <param name="policy">Los dominios admitidos. Sin política, cualquiera (<see cref="RegistrationPolicy.Unrestricted"/>).</param>
    public RegisterAccountUseCase(IAccountRepository accounts, ISystemClock clock, RegistrationPolicy? policy = null)
    {
        ArgumentNullException.ThrowIfNull(accounts);
        ArgumentNullException.ThrowIfNull(clock);

        _accounts = accounts;
        _clock = clock;
        _policy = policy ?? RegistrationPolicy.Unrestricted;
    }

    /// <summary>Ejecuta el flujo principal de CU-01 §4, en su orden.</summary>
    /// <param name="email">Correo escrito, tal como llegó del formulario.</param>
    /// <param name="firstName">Nombre.</param>
    /// <param name="lastName">Apellido.</param>
    public async Task<ApplicationResult<AccountSnapshot>> ExecuteAsync(
        string? email,
        string? firstName,
        string? lastName,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = EmailIdentity.Normalize(email);

        // R-07 — ¿el dominio está admitido? ANTES de consultar el conjunto: la respuesta a un
        // dominio no admitido no dice si ese correo ya tenía cuenta (`Api ADR-00012`). El correo
        // vacío sigue de largo y lo rechaza el dominio por campo ausente.
        if (!string.IsNullOrWhiteSpace(normalizedEmail) && !_policy.Admits(normalizedEmail))
        {
            return ApplicationResult<AccountSnapshot>.Rejected(ApplicationConditionCode.EmailDomainNotAdmitted);
        }

        // Paso 2 — ¿el correo está libre? No se informa el estado ni el papel de la cuenta que
        // lo ocupa (RN-02, INV-01).

        if (!string.IsNullOrWhiteSpace(normalizedEmail)
            && await _accounts.EmailIsRegisteredAsync(normalizedEmail, cancellationToken).ConfigureAwait(false))
        {
            return ApplicationResult<AccountSnapshot>.Rejected(ApplicationConditionCode.EmailAlreadyRegistered);
        }

        // Paso 3 — el sello de alta sale del puerto de reloj, para que sea verificable en prueba.
        var createdAt = _clock.UtcNow;

        // Pasos 4 y 5 — el dominio constituye. NO SE LE APORTA CREDENCIAL y no se le pide papel
        // ni estado distintos de los del auto-registro: los dos rechazos que los cubren existen
        // para el consumidor que lo intente, no para éste.
        var constitution = Account.Register(
            email,
            firstName,
            lastName,
            passwordHash: null,
            emailUniquenessVerified: true,
            requestedRole: Role.Student,
            requestedStatus: AccountStatus.Pending,
            createdAt: createdAt);

        if (!constitution.Succeeded)
        {
            return ApplicationResult<AccountSnapshot>.Rejected(constitution.ConditionCode!);
        }

        var account = constitution.Value!;

        // Paso 6 — una única unidad de trabajo. Antes de esta línea no se escribió nada.
        await _accounts.AddAsync(account, cancellationToken).ConfigureAwait(false);

        return ApplicationResult<AccountSnapshot>.Applied(AccountSnapshot.Of(account));
    }
}
