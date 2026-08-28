using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Basico.Recorrido;

/// <summary>
/// `OP-01` — Constituye la cuenta del alumno, que nace `Pendiente` y SIN credencial, y
/// muestra el rechazo tipado del alta sin correo.
/// </summary>
internal static class ActoAltaDeAlumno
{
    internal static Account Ejecutar(Bitacora bitacora, DateTimeOffset momento)
    {
        var alta = bitacora.Invocar(() => Account.Register(
            email: "alumna@frre.utn.edu.ar",
            firstName: "Alumna",
            lastName: "Ejemplo",
            passwordHash: null,
            emailUniquenessVerified: true,
            requestedRole: Role.Student,
            requestedStatus: AccountStatus.Pending,
            createdAt: momento));

        var alumna = alta.Value!;
        bitacora.Escribir(
            $"[2] Alumno constituido: papel={Vocabulario.De(alumna.Role)} "
            + $"estado={Vocabulario.De(alumna.Status)} "
            + $"credencial={Vocabulario.Credencial(alumna.PasswordHash)}");

        var sinCorreo = bitacora.Invocar(() => Account.Register(
            email: null,
            firstName: "Sin",
            lastName: "Correo",
            passwordHash: null,
            emailUniquenessVerified: true,
            requestedRole: Role.Student,
            requestedStatus: AccountStatus.Pending,
            createdAt: momento));

        bitacora.Rechazo($"[2b] Alta sin correo rechazada: {sinCorreo.ConditionCode}");
        return alumna;
    }
}
