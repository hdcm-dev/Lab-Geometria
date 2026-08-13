namespace GeometriaFactory.Application.Ports;

/// <summary>
/// Repositorio de cuentas. Es el cuarto puerto, PROPUESTO por `Plan-Etapa-A.md` §1.5 (`P-4a`)
/// y pendiente de confirmación en el punto de control de la etapa `a` (`A-1` de `Handoff-Checkout.md` §6.2).
/// </summary>
/// <remarks>
/// Qué hace, declarado por `Application ADR-02` §2 punto 1 y §3.1: recuperar una cuenta por su
/// correo, responder si un correo ya está registrado, responder si ya existe una cuenta con papel
/// `Administrator`, y materializar el resultado incluida la marca de cambio de contraseña pendiente.
///
/// ETAPA `a`: el puerto se DECLARA y sus miembros NO se escriben. Las firmas transportan
/// atributos de la entidad `Account`, que el Product Owner ancló a la etapa `c` (`Domain BT-06`).
/// Escribirlas acá sería modelar la entidad por adelantado. Su adaptador es
/// `EfCoreAccountRepository` (`Infrastructure BT-09`, etapa `c`).
/// </remarks>
public interface IAccountRepository
{
}
