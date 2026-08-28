using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Samples.Domain.Basico.Recorrido;

/// <summary>
/// `OP-12` — Configura la única cuenta de administrador, y después intenta configurar
/// una segunda para mostrar que el rechazo llega TIPADO y no como excepción.
/// </summary>
internal static class ActoConfigurarAdministrador
{
    internal static Account Ejecutar(Bitacora bitacora, DateTimeOffset momento)
    {
        var configurado = bitacora.Invocar(() => Account.ConfigureAdministrator(
            email: "docente@frre.utn.edu.ar",
            firstName: "Docente",
            lastName: "Titular",
            passwordHash: "hash-de-la-credencial-inicial",
            administratorAbsenceDeclared: true,
            emailUniquenessVerified: true,
            requestedStatus: AccountStatus.Enabled,
            createdAt: momento));

        var administrador = configurado.Value!;
        bitacora.Escribir(
            $"[1] Administrador configurado: papel={Vocabulario.De(administrador.Role)} "
            + $"estado={Vocabulario.De(administrador.Status)} "
            + $"credencial={Vocabulario.Credencial(administrador.PasswordHash)}");

        // El consumidor declara que YA HAY administrador: la guarda lo rechaza sin excepción.
        var segundo = bitacora.Invocar(() => Account.ConfigureAdministrator(
            email: "otro@frre.utn.edu.ar",
            firstName: "Otro",
            lastName: "Docente",
            passwordHash: "hash",
            administratorAbsenceDeclared: false,
            emailUniquenessVerified: true,
            requestedStatus: AccountStatus.Enabled,
            createdAt: momento));

        bitacora.Rechazo($"[1b] Segundo administrador rechazado: {segundo.ConditionCode}");
        return administrador;
    }
}
