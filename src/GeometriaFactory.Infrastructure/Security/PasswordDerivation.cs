using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using GeometriaFactory.Application.Accounts;

namespace GeometriaFactory.Infrastructure.Security;

/// <summary>
/// CU-06 — Convierte una contraseña en claro en el valor derivado que el producto guarda, y
/// responde si una contraseña en claro se corresponde con un valor derivado ya guardado.
/// </summary>
/// <remarks>
/// ES EL ÚLTIMO PUNTO DEL RECORRIDO DE LA CONTRASEÑA EN CLARO: de acá para adentro sólo circula
/// el valor derivado. Nada de lo que pasa por acá se registra —ni la contraseña, ni el valor
/// derivado, ni la sal—, y por eso esta clase no recibe ningún registrador.
///
/// QUÉ FUNCIÓN DE DERIVACIÓN SE ANCLA, Y QUIÉN LO DECIDIÓ. El intake §17.3.P.1 y §17.3.P.5
/// declaran «PBKDF2 o Argon2» y NO eligen; `Infrastructure ADR-04` §2 deja la elección abierta y
/// fija el criterio con el que hay que tomarla: **la que la plataforma base provea sin agregar
/// una dependencia nueva al proyecto de código; si las dos lo hacen, la de mayor resistencia a
/// hardware dedicado**. Aplicado el criterio: **PBKDF2**, que la plataforma provee en
/// `System.Security.Cryptography.Rfc2898DeriveBytes`, contra Argon2, que en este entorno exige
/// un paquete de terceros. **La elección la toma la etapa `c` y va al punto de control**, con la
/// constancia de que el criterio la determina por completo y no queda margen de gusto.
///
/// LOS PARÁMETROS VIAJAN CON EL VALOR GUARDADO (`ADR-04` §2), y la verificación los lee de ahí y
/// no de la configuración vigente. Es lo que permite subir el coste sin invalidar ninguna
/// credencial existente, y lo que hace diagnosticable el valor ilegible.
///
/// LA FORMA DEL VALOR GUARDADO ES PARTE DEL CONTRATO DEL DATO —`ADR-04` §6 punto 1— y es:
/// <c>PBKDF2-SHA256$&lt;iteraciones&gt;$&lt;sal en base64&gt;$&lt;derivado en base64&gt;</c>.
/// </remarks>
public sealed class PasswordDerivation
{
    /// <summary>Etiqueta de la función anclada. Un valor con otra etiqueta es ilegible, no «no coincide».</summary>
    public const string AnchoredFunction = "PBKDF2-SHA256";

    /// <summary>
    /// Coste anclado por la etapa `c`. `ADR-04` §7 manda calibrarlo midiendo en el equipo
    /// objetivo, y esta cifra es la medición: ver el informe de la etapa. Es configurable
    /// justamente porque subirla no invalida ninguna credencial ya guardada.
    /// </summary>
    public const int AnchoredIterations = 210_000;

    private const int SaltSizeInBytes = 16;
    private const int DerivedSizeInBytes = 32;

    private readonly int _iterations;

    public PasswordDerivation(int iterations = AnchoredIterations)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(iterations, 1);
        _iterations = iterations;
    }

    /// <summary>
    /// Deriva una contraseña en claro. Devuelve nulo si no hay contraseña que derivar, que es la
    /// condición `CONTRASENA_EN_CLARO_AUSENTE`: la cadena vacía NO se deriva, porque produciría
    /// un valor válido para una credencial que nadie eligió (`ADR-04` §2 punto 2).
    /// </summary>
    public string? Derive(string? plaintextPassword)
    {
        if (string.IsNullOrEmpty(plaintextPassword))
        {
            return null;
        }

        var salt = RandomNumberGenerator.GetBytes(SaltSizeInBytes);
        var derived = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(plaintextPassword),
            salt,
            _iterations,
            HashAlgorithmName.SHA256,
            DerivedSizeInBytes);

        return string.Create(
            CultureInfo.InvariantCulture,
            $"{AnchoredFunction}${_iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(derived)}");
    }

    /// <summary>
    /// Comprueba una contraseña en claro contra un valor derivado guardado.
    /// </summary>
    /// <remarks>
    /// Los TRES desenlaces son los de CU-06: coincide, no coincide, y **valor ilegible**, que no
    /// se colapsa con «no coincide» porque hacerlo dejaría una cuenta inaccesible sin que nadie
    /// supiera por qué (`ADR-04` §2 punto 1).
    ///
    /// La comparación final es de **tiempo fijo**: comparar byte a byte con salida temprana
    /// filtra por cuánto tarda cuánto acertó quien prueba.
    /// </remarks>
    public CredentialCheck Verify(string? plaintextPassword, string? storedValue)
    {
        if (string.IsNullOrEmpty(plaintextPassword))
        {
            // Sin contraseña no hay nada que comprobar. No es «no coincide»: es que no se pidió
            // una comprobación (`CONTRASENA_EN_CLARO_AUSENTE`).
            return CredentialCheck.DoesNotMatch;
        }

        if (!TryRead(storedValue, out var iterations, out var salt, out var expected))
        {
            return CredentialCheck.Unreadable;
        }

        var actual = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(plaintextPassword),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            expected.Length);

        return CryptographicOperations.FixedTimeEquals(actual, expected)
            ? CredentialCheck.Matches
            : CredentialCheck.DoesNotMatch;
    }

    private static bool TryRead(string? storedValue, out int iterations, out byte[] salt, out byte[] derived)
    {
        iterations = 0;
        salt = [];
        derived = [];

        if (string.IsNullOrWhiteSpace(storedValue))
        {
            return false;
        }

        var parts = storedValue.Split('$');
        if (parts.Length != 4
            || !string.Equals(parts[0], AnchoredFunction, StringComparison.Ordinal)
            || !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out iterations)
            || iterations < 1)
        {
            return false;
        }

        try
        {
            salt = Convert.FromBase64String(parts[2]);
            derived = Convert.FromBase64String(parts[3]);
        }
        catch (FormatException)
        {
            return false;
        }

        return salt.Length > 0 && derived.Length > 0;
    }
}
