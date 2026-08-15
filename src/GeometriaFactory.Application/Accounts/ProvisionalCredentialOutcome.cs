using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Lo que devuelven los dos actos que producen una contraseña provisoria: la habilitación de
/// `CU-02` y el reseteo de `CU-11`.
/// </summary>
/// <remarks>
/// UN SOLO TIPO PARA LOS DOS, Y ES DELIBERADO. Desde **RN-16** la provisoria de la habilitación
/// y la del reseteo son **el mismo mecanismo con dos disparadores**: las dos las produce el
/// sistema, las dos viajan en claro una sola vez, las dos dejan la marca puesta y las dos se
/// cambian por el mismo camino (`Contracts CU-02` §10). Dos tipos habrían sugerido un
/// tratamiento distinto que no existe.
///
/// EL VALOR EN CLARO VIAJA UNA SOLA VEZ Y NO SE PERSISTE EN NINGUNA PARTE. Lo que se guarda es
/// su forma derivada, y esta capa **no lo conserva después de devolverlo** (`Application CU-02`
/// §7, `CU-11` §7). Tampoco entra en ninguna traza (`Infrastructure ADR-05` §7).
///
/// LA SITUACIÓN QUE DEVUELVE ES LA RESULTANTE, y en el reseteo eso significa **la misma que
/// tenía**: el reseteo no es una transición de la máquina de estados de cuenta (RN-15).
/// </remarks>
/// <param name="Status">Situación de la cuenta al terminar el acto.</param>
/// <param name="ProvisionalPassword">
/// El valor en claro de la provisoria, cuando el acto produjo una. **Nulo cuando no la hubo**,
/// que es el caso del bloqueo y el de habilitar una cuenta que ya estaba habilitada: esa
/// ausencia es la señal de que no hay nada que comunicar (`Contracts CU-02` FA-06).
/// </param>
/// <param name="MustChangePassword">Si la cuenta quedó con cambio de contraseña pendiente (INV-09).</param>
public sealed record ProvisionalCredentialOutcome(
    AccountStatus Status,
    string? ProvisionalPassword,
    bool MustChangePassword);
