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

    /// <summary>
    /// Si la ventana de alta del administrador ya se cerró, es decir, si el laboratorio ya está
    /// configurado. **De sólo lectura: no escribe nada y no puede fallar por estado.**
    /// </summary>
    /// <remarks>
    /// ES EL PASO 2 DE <see cref="ExecuteAsync"/>, EXTRAÍDO, Y NO UNA PREGUNTA NUEVA. El flujo
    /// principal de `CU-10` §4 empieza preguntando exactamente esto para decidir si procede; lo
    /// único que este miembro agrega es **poder preguntarlo sin intentar configurar nada**. Por
    /// eso vive acá y no en un caso de uso nuevo: el sujeto es el mismo, la ventana de alta es la
    /// misma, y darle un contrato aparte habría duplicado el concepto en dos lugares que después
    /// pueden responder distinto.
    ///
    /// QUIÉN LA NECESITA. El **guardián 1** de `Web ADR-03` §2, que hasta hoy no se podía
    /// construir porque la pieza pública no tenía con qué preguntarla. El punto de acceso `A-17`
    /// la expone, anónima y de sólo lectura.
    ///
    /// Y NO AFLOJA NADA DE LO QUE `FA-01` HACE CUMPLIR. Que alguien pregunte «¿ya hay
    /// administrador?» y reciba «no» **no le habilita nada**: la ventana la sigue cerrando
    /// <see cref="ExecuteAsync"/> con su propia comprobación, en su propia transacción, y el
    /// almacén con un índice único. Esta consulta **acota lo que la pantalla ofrece** y no hace
    /// cumplir ninguna regla, que es la misma distinción que `ADR-03` §2 declara para los cuatro
    /// guardianes.
    /// </remarks>
    public Task<bool> IsConfiguredAsync(CancellationToken cancellationToken = default) =>
        _accounts.AdministratorExistsAsync(cancellationToken);

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
