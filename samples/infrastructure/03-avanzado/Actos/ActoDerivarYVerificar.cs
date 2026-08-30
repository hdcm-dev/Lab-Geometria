using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>Acto 1 — `CU-06006`: derivar una credencial y comprobarla, con sus TRES desenlaces.</summary>
/// <remarks>
/// EL TERCER DESENLACE ES EL QUE JUSTIFICA EL ACTO. «No coincide» es una respuesta legítima del
/// mecanismo; «ilegible» es un almacén en el que no se puede confiar. Colapsarlos haría que un
/// dato corrupto se leyera como contraseña equivocada, y la cuenta quedaría inaccesible sin que
/// nadie supiera por qué. Por eso `CredentialCheck` tiene tres valores y no un booleano.
/// </remarks>
internal static class ActoDerivarYVerificar
{
    internal static (string Clara, string Derivado) Ejecutar(string clara, Action<string> escribir)
    {
        var derivacion = new PasswordDerivation();

        var derivado = derivacion.Derive(clara)!;

        // QUE LA CONTRASEÑA NO ESTÉ GUARDADA SE COMPRUEBA MIRANDO EL VALOR, no confiando en que
        // el algoritmo la haya consumido. El derivado lleva función, iteraciones, sal y resultado;
        // si la clara apareciera ahí adentro, el mecanismo entero no serviría de nada.
        var claraAdentro = derivado.Contains(clara, StringComparison.Ordinal);
        escribir($"[1] Derivacion de contrasena: {(derivado.Length > 0 ? "valor derivado producido" : "SIN VALOR")} "
            + $"| contrasena en claro guardada: {(claraAdentro ? "SI" : "no")}");

        escribir($"[1] Verificacion con la credencial correcta: "
            + $"{(derivacion.Verify(clara, derivado) == CredentialCheck.Matches ? "verdadera" : "falsa")}");

        escribir($"[1] Verificacion con la credencial incorrecta: "
            + $"{(derivacion.Verify(clara + "x", derivado) == CredentialCheck.Matches ? "verdadera" : "falsa")}");

        // El valor ilegible se fabrica rompiendo la FORMA del derivado, no su contenido: cuatro
        // campos separados por `$` es lo que `TryRead` espera, y esto tiene uno solo.
        var ilegible = derivacion.Verify(clara, "esto-no-es-un-derivado");
        escribir($"[1] Verificacion contra un derivado ilegible: "
            + $"{(ilegible == CredentialCheck.Unreadable ? InfrastructureConditionCode.UnreadablePasswordHash : ilegible.ToString())} "
            + $"(distinto de falsa)");

        // DIVERGENCIA D-1 CONTRA §6, y es la primera de un patrón que se repite cinco veces.
        //
        // §6 espera `rechazada PLAINTEXT_PASSWORD_MISSING`. Ese código NO EXISTE en el árbol:
        // `Derive` devuelve `null` y no dice por qué. Los ÚNICOS dos códigos tipados que esta capa
        // declara son `UNREADABLE_PASSWORD_HASH` y `RANDOMNESS_SOURCE_UNAVAILABLE`; todo el resto
        // de sus fallas viaja como nulo.
        //
        // LA CONDUCTA SE CUMPLE —sin contraseña no se produce nada—; lo que falta es el nombre.
        // Y hay una huella de que el nombre estuvo pensado: el comentario de `Verify` cita
        // `CONTRASENA_EN_CLARO_AUSENTE`, en prosa, para una constante que nadie escribió.
        var sinClara = derivacion.Derive(null);
        escribir($"[1] Derivacion sin contrasena en claro: {(sinClara is null ? "nulo, sin codigo tipado" : "PRODUJO ALGO")}");

        return (clara, derivado);
    }
}
