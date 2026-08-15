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
/// (`Domain BT-06`). Son CINCO y no más: los que las capacidades `F-01` y `F-05` ejercen. El
/// listado de la comisión, la baja y el reseteo llegan con la etapa `d`.
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
}
