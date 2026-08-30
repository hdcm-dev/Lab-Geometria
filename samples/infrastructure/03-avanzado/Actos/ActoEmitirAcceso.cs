using System.Security.Cryptography;
using GeometriaFactory.Infrastructure.Security;
using GeometriaFactory.Infrastructure.Time;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>Acto 3 — `CU-06008`: emitir un acceso, y las tres formas en que uno no vale.</summary>
internal static class ActoEmitirAcceso
{
    internal static async Task EjecutarAsync(Action<string> escribir)
    {
        // EL RELOJ DE ESTE ACTO ES EL DEL SISTEMA Y NO EL FIJADO DEL ACTO 4, y equivocarse cuesta
        // una hora: `ValidationParameters` trae `ValidateLifetime` con 30 segundos de tolerancia,
        // así que un acceso emitido con un sello fijo del futuro vuelve INVÁLIDO —no por la firma,
        // sino porque todavía no empezó a valer—. El acto 4 fija el reloj para mostrar que se
        // puede; éste necesita que el instante de emisión y el de verificación sean el mismo.
        var reloj = new UtcSystemClock();
        // LA CLAVE DE FIRMA SE PRODUCE ACÁ Y AHORA, y no está escrita en ningún lado del sample.
        // Es la condición que la inspección `SinSecretosEnLaFuente` cuenta con umbral cero: una
        // clave literal en la fuente de un ejemplo termina copiada en un despliegue.
        var clave = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(AccessTokenIssuer.MinimumSigningKeySizeInBytes * 2));

        var emisor = new AccessTokenIssuer(new SigningOptions { SigningKey = clave, LifetimeInMinutes = 480 });
        var cuenta = Guid.NewGuid();
        var acceso = emisor.Issue(cuenta, "alumna@ejemplo.edu", "Student", reloj.UtcNow)!;

        var leido = new JsonWebToken(acceso);
        var declarados = new[] { "sub", "email", JwtRegisteredClaimNames.Iss, JwtRegisteredClaimNames.Aud };
        var presentes = declarados.Count(c => leido.Claims.Any(r =>
            string.Equals(r.Type, c, StringComparison.Ordinal))
            || string.Equals(c, JwtRegisteredClaimNames.Iss, StringComparison.Ordinal) && leido.Issuer.Length > 0);

        var propio = await new JsonWebTokenHandler()
            .ValidateTokenAsync(acceso, emisor.ValidationParameters).ConfigureAwait(false);
        escribir($"[3] Acceso emitido: reclamos presentes={presentes} "
            + $"| verificacion del acceso propio: {(propio.IsValid ? "valida" : "invalida")}");

        // ---- Firma ajena y acceso vencido ----
        // SON DOS FALLAS DISTINTAS Y SE MIDEN JUNTAS A PROPÓSITO: las dos tienen que dar
        // «inválido», y si una sola de las dos lo diera, la línea seguiría pareciendo correcta
        // leída por arriba. Por eso el renglón las nombra por separado.
        var otra = new AccessTokenIssuer(new SigningOptions
        {
            SigningKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            LifetimeInMinutes = 480,
        });
        var conFirmaAjena = await new JsonWebTokenHandler()
            .ValidateTokenAsync(otra.Issue(cuenta, "alumna@ejemplo.edu", "Student", reloj.UtcNow),
                emisor.ValidationParameters).ConfigureAwait(false);

        var vencidoEmisor = new AccessTokenIssuer(new SigningOptions { SigningKey = clave, LifetimeInMinutes = 1 });
        var vencido = await new JsonWebTokenHandler()
            .ValidateTokenAsync(
                vencidoEmisor.Issue(cuenta, "alumna@ejemplo.edu", "Student", reloj.UtcNow.AddHours(-9)),
                emisor.ValidationParameters).ConfigureAwait(false);

        escribir($"[3] Acceso con firma ajena: {(conFirmaAjena.IsValid ? "VALIDO" : "invalido")} "
            + $"| Acceso vencido: {(vencido.IsValid ? "VALIDO" : "invalido")}");

        // DIVERGENCIA D-3 Y D-4 CONTRA §6, y las dos son LA MISMA y es la más seria del sample.
        //
        // §6 espera dos rechazos distintos: `SIGNING_KEY_MISSING` cuando falta la clave e
        // `INCOMPLETE_CLAIMS` cuando faltan reclamos. Ninguno de los dos códigos existe, y el
        // problema no es que falten los nombres: es que **`Issue` devuelve el MISMO `null` en los
        // dos casos**, junto con otros tres. Quien lo llama no puede distinguirlos.
        //
        // Y NO SON LA MISMA CLASE DE FALLA. Reclamos incompletos es un pedido mal armado: el
        // llamador tiene el defecto y lo puede corregir. Clave de firma ausente es un DESPLIEGUE
        // mal configurado: nadie que pida un acceso lo puede arreglar, y el servicio no debería
        // estar atendiendo. `scripts/store-path.sh` cuenta que este producto ya eligió detenerse
        // en el arranque ante configuración faltante, por este mismo motivo. Acá el arranque
        // sigue, y la falla aparece como un acceso que no se emite.
        var sinClave = new AccessTokenIssuer(new SigningOptions { SigningKey = "" });
        var porFaltaDeClave = sinClave.Issue(cuenta, "alumna@ejemplo.edu", "Student", reloj.UtcNow);
        var porReclamos = emisor.Issue(cuenta, email: null, role: null, reloj.UtcNow);

        escribir($"[3] Emision sin clave de firma: {(porFaltaDeClave is null ? "nulo, sin codigo tipado" : "EMITIO")} "
            + $"| accesos emitidos: {(porFaltaDeClave is null ? 0 : 1)}");
        escribir($"[3] Emision con reclamos incompletos: {(porReclamos is null ? "nulo, sin codigo tipado" : "EMITIO")} "
            + $"| distinguible de la anterior: no");
    }
}
