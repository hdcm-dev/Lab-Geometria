using GeometriaFactory.Domain.Entities;

namespace GeometriaFactory.Samples.Domain.Basico.Recorrido;

/// <summary>
/// `OP-02` y `OP-03` — Habilita la cuenta fijándole la credencial provisoria, y después
/// deja que la propia cuenta la reemplace. Son dos actos y no uno: la marca de cambio
/// pendiente la pone la habilitación y **sólo la levanta el reemplazo hecho por la cuenta**.
/// </summary>
internal static class ActoHabilitarConProvisoria
{
    internal static void Habilitar(Bitacora bitacora, Account alumna)
    {
        // El dominio NO produce la provisoria y nunca la conoce en claro (RN-14): recibe
        // la credencial ya derivada. Acá el valor es de mentira porque el sample no deriva.
        bitacora.Invocar(() => alumna.Enable("hash-de-la-provisoria"));

        bitacora.Escribir(
            $"[4] Cuenta habilitada: estado={Vocabulario.De(alumna.Status)} "
            + $"credencial={Vocabulario.Credencial(alumna.PasswordHash)} "
            + $"cambio-pendiente={Vocabulario.Marca(alumna.MustChangePassword)}");
    }

    internal static void ReemplazarCredencial(Bitacora bitacora, Account alumna)
    {
        bitacora.Invocar(() => alumna.ReplaceCredential(
            newPasswordHash: "hash-de-la-credencial-elegida",
            currentCredentialVerified: true));

        bitacora.Escribir(
            $"[6] Credencial reemplazada por la propia cuenta: "
            + $"cambio-pendiente={Vocabulario.Marca(alumna.MustChangePassword)}");
    }
}
