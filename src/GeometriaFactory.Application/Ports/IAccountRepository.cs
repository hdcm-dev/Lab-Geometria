using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Repositorio de cuentas. Cuarto puerto del producto, PROPUESTO por `Plan-Etapa-A.md` §1.5 (`P-4a`).
/// </summary>
/// <remarks>
/// Qué hace, declarado por `Application ADR-02` §2 punto 1 y §3.1: recuperar una cuenta por su
/// correo, responder si un correo ya está registrado, responder si ya existe una cuenta con papel
/// `Administrator`, y materializar el resultado incluida la marca de cambio de contraseña pendiente.
///
/// ETAPA `c`: los miembros se escriben, porque la entidad `Account` ya está modelada
/// (`Domain BT-06`). Fueron SEIS: los que las capacidades `F-01` y `F-05` ejercen.
///
/// ETAPA `d`: entran los DOS que faltaban —el **listado de la comisión** que `Api CU-04` `A-06`
/// consume y el **retiro** de la baja de `Application CU-02` FA-02—, y el puerto queda en ocho.
/// No entra ningún puerto nuevo: la producción de la contraseña provisoria y la derivación
/// cruzan la frontera **como función que el consumidor aporta**, igual que la comprobación de
/// credencial de la etapa `c`, de modo que los puertos del producto siguen siendo **cuatro**
/// (`Infrastructure CU-07` §10; `Application` §8) y la puerta `QG-10` sigue cuadrando.
///
/// LA RECUPERACIÓN ES POR CORREO NORMALIZADO y no por el escrito: es la forma que decide la
/// identidad (`Infrastructure ADR-03`, `Modelo-Datos-Logico.md` §2.1). Quien normaliza es
/// `GeometriaFactory.Domain.Values.EmailIdentity`, para que el adaptador y el caso de uso no
/// puedan normalizar distinto.
/// </remarks>
public interface IAccountRepository
{
    /// <summary>Recupera la cuenta de un correo normalizado, o nulo si no existe ninguna.</summary>
    Task<Account?> FindByNormalizedEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    /// <summary>Recupera la cuenta por su identidad, o nulo si no existe.</summary>
    Task<Account?> FindByIdAsync(Guid accountId, CancellationToken cancellationToken = default);

    /// <summary>Si ya existe una cuenta con papel `Administrator` (RN-01, INV-05).</summary>
    Task<bool> AdministratorExistsAsync(CancellationToken cancellationToken = default);

    /// <summary>Si el correo normalizado ya pertenece a una cuenta (RN-02, INV-01).</summary>
    Task<bool> EmailIsRegisteredAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    /// <summary>Materializa una cuenta recién constituida, en una única unidad de trabajo.</summary>
    Task AddAsync(Account account, CancellationToken cancellationToken = default);

    /// <summary>Materializa el cambio sobre una cuenta ya existente, en una única unidad de trabajo.</summary>
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);

    /// <summary>
    /// Todas las cuentas de la comisión, para el listado del administrador (`Api CU-04` `A-06`).
    /// </summary>
    /// <remarks>
    /// DEVUELVE LAS CUENTAS ENTERAS Y NO UN TIPO DE LISTADO: quién recorta qué se muestra es el
    /// caso de uso, y el adaptador no decide sobre el contenido. La credencial derivada no sale
    /// de la capa de aplicación porque el tipo que ésta devuelve hacia afuera no la declara.
    /// </remarks>
    Task<IReadOnlyList<Account>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retira una cuenta **y todos sus trabajos**, en una única unidad de trabajo (RN-07).
    /// </summary>
    /// <remarks>
    /// EL ARRASTRE ES PARTE DE LA OPERACIÓN Y NO UN EFECTO POSTERIOR: no existe una baja que deje
    /// trabajos sin dueño, y por eso el retiro es una sola llamada y no dos. Mientras los
    /// trabajos no estén modelados —etapa `e`— el adaptador retira la cuenta sola, y el día que
    /// existan el arrastre entra acá adentro y no en el caso de uso.
    /// </remarks>
    Task RemoveAsync(Account account, CancellationToken cancellationToken = default);
}
