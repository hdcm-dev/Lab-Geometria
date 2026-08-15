using System.Security.Cryptography;

namespace GeometriaFactory.Infrastructure.Security;

/// <summary>
/// CU-07 — Produce el valor de la contraseña provisoria de la habilitación y del reseteo.
/// </summary>
/// <remarks>
/// ES LA DELEGACIÓN MÁS EXPLÍCITA DEL CORPUS. `RN-14` exige dos propiedades del valor —**no es
/// adivinable** y **no se repite** entre cuentas ni entre actos sobre la misma cuenta— y las tres
/// capas de arriba declaran que no las ejercen: la de aplicación porque el valor le llega ya
/// producido, la de contratos porque no declara mecanismo, y el dominio porque lo recibe ya
/// derivado. Acá es donde el valor nace, y por lo tanto el único lugar donde las dos propiedades
/// se pueden sostener.
///
/// LAS CINCO REGLAS DE `ADR-05` §2, Y LAS CINCO SON EXIGIBLES:
///  1. **Ningún carácter proviene de otra fuente**: ni del correo, ni del nombre, ni de la fecha,
///     ni de un contador, ni de la identidad de la cuenta.
///  2. **Doce caracteres** de un alfabeto de letras y dígitos **sin los pares que se confunden al
///     dictarlos** y **sin signos de puntuación**, porque el canal declarado es la voz: el
///     administrador se la comunica al alumno en persona.
///  3. **Si la fuente de material impredecible no responde, no se produce valor.** No se compone
///     por otro medio: un reseteo que no se completa es recuperable, y una provisoria adivinable
///     no se nota hasta que alguien la usa.
///  4. **El valor se devuelve una sola vez y no se registra en ninguna traza.** Lo que se guarda
///     es su forma derivada, por <see cref="PasswordDerivation"/>.
///  5. **La producción no recibe el estado de la cuenta.** Es la forma estructural de `RN-15`: no
///     puede comprobarlo, de modo que resetear no puede exigir que la cuenta esté habilitada.
///
/// LA OPERACIÓN NO DECLARA NINGÚN PARÁMETRO, y es una convención impuesta por `ADR-05` §7: si
/// recibiera uno, alguien terminaría derivando el valor de él. Es también lo que hace que la
/// producción **no pueda distinguir la habilitación del reseteo**, que es la forma en que RN-16
/// suma un segundo consumidor sin agregar mecanismo.
///
/// «NO SE REPITE» LO SOSTIENE LA IMPREDECIBILIDAD Y NO UN REGISTRO DE VALORES ANTERIORES
/// (`CU-07` §10, heredado por `ADR-05` §2). Conservar las provisorias anteriores para
/// compararlas exigiría guardar contraseñas en claro, que es exactamente lo que el producto no
/// hace. **La longitud y el alfabeto son derivación de `05-Arquitectura-Tecnica`** y siguen
/// elevados al Product Owner en `PA-06`: si los reemplaza, cambia una constante y su prueba.
///
/// ESTA CLASE NO RECIBE NINGÚN REGISTRADOR, por el mismo motivo que <see cref="PasswordDerivation"/>:
/// nada de lo que pasa por acá se registra.
/// </remarks>
public sealed class ProvisionalPasswordFactory
{
    /// <summary>
    /// Longitud fija, declarada por `ADR-05` §2 punto 2. **[derivación de `05-Arquitectura-Tecnica`,
    /// elevada al Product Owner en `PA-06`]**
    /// </summary>
    public const int Length = 12;

    /// <summary>
    /// El alfabeto, sin los caracteres que se confunden al dictarlos: **sin `0` ni `O`, sin `1`
    /// ni `l` ni `I`, y sin ningún signo de puntuación**, cuyo nombre hablado es ambiguo y cuya
    /// escritura depende de la disposición del teclado (`ADR-05` §2 punto 2 y §4).
    /// </summary>
    public const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

    /// <summary>
    /// Produce una contraseña provisoria, o **nulo** si la fuente de material impredecible del
    /// sistema no respondió.
    /// </summary>
    /// <remarks>
    /// EL NULO ES LA CONDICIÓN `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE`, y es la única de este
    /// contrato. **No se devuelve ningún valor parcial y no se compone uno por otro medio**: el
    /// atajo de completar con un contador o con la fecha es el que `ADR-05` declara explícitamente
    /// prohibido, porque produce una provisoria adivinable **y en silencio**.
    ///
    /// LA ELECCIÓN DE CADA CARÁCTER ES UNIFORME SOBRE EL ALFABETO. Se usa la selección sin sesgo
    /// que la plataforma provee, y no el resto de una división sobre un byte: ese resto favorece
    /// a los primeros caracteres del alfabeto cuando su tamaño no divide a 256, y un sesgo en la
    /// distribución es exactamente lo que reduce el espacio de valores sin que se note.
    /// </remarks>
    public string? Produce()
    {
        try
        {
            return RandomNumberGenerator.GetString(Alphabet, Length);
        }
        catch (CryptographicException)
        {
            // La fuente no respondió. No hay valor, y no hay segundo intento por otro medio.
            return null;
        }
    }
}
