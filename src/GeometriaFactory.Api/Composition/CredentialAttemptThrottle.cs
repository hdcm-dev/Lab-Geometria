using System.Threading.RateLimiting;
using GeometriaFactory.Domain.Values;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// El límite de los intentos fallidos **por cuenta**: los puntos que comprueban una contraseña
/// sin acceso firmado —el canje `A-01` y la forma sin sesión del cambio propio `A-05`— lo consultan
/// antes de derivar y le anotan cada fallo (`ADR-00011`).
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE. `BT-00029` puso la cuota del canje **por dirección de origen**, y en producción
/// eso castigó a quien no atacaba: el front habla con este servicio servidor a servidor y no reenvía
/// la dirección del navegador, así que para el zócalo toda la comisión que entra por el front es el
/// contenedor del front; y aunque la reenviara, en la facultad los alumnos salen por un mismo NAT.
/// Treinta canjes por minuto para una clase entera choca con `RN-B1`, que es restricción dura: sin
/// acceso desde la facultad el laboratorio no existe. La fuerza bruta contra una contraseña apunta a
/// **una cuenta**, y es la cuenta la que se protege (OWASP Authentication Cheat Sheet, *login
/// throttling*); el origen conserva un tope, holgado, contra quien rocía muchas cuentas a la vez.
///
/// LA PARTICIÓN ES EL CORREO NORMALIZADO, CON LA MISMA FUNCIÓN CON LA QUE EL DOMINIO LO COMPARA.
/// <see cref="EmailIdentity.Normalize"/> es la regla de identidad (`ADR-06003`): si la partición
/// usara otra, `Ana@frre.utn.edu.ar` y ` ana@frre.utn.edu.ar ` serían dos cuotas para una sola cuenta
/// y el límite se esquivaría cambiando mayúsculas.
///
/// VIVE EN EL PUNTO Y NO EN LA POLÍTICA DE `UseRateLimiter`, y es la decisión técnica de esta clase.
/// La partición del middleware se calcula antes de que el enlace de la petición lea el cuerpo, y el
/// correo viaja en el cuerpo: leerlo ahí obliga a habilitar el búfer y a deserializar dos veces una
/// entrada que todavía nadie validó. Adentro del punto el cuerpo **ya está enlazado** al tipo del
/// contrato, se lee una vez, y la consulta ocurre antes de lo único caro, la derivación anclada de
/// `ADR-06004`. Hay una segunda razón, y pesa igual: el middleware sólo sabe contar peticiones, y acá
/// lo que se cuenta son **fallos**.
///
/// CUENTA SÓLO LOS FALLOS, NO LOS INGRESOS. Una persona que entra bien diez veces no está atacando a
/// nadie, y un guion propio del docente —o el arnés de extremo a extremo— canjea muchas veces la
/// misma credencial correcta. Contar ingresos habría convertido el uso legítimo en bloqueo.
///
/// CONSULTAR Y ANOTAR SON DOS PASOS, Y ENTRE LOS DOS HAY UNA CARRERA QUE SE ACEPTA. La consulta pide
/// cero permisos —no gasta—, y el fallo gasta uno después de derivar. Intentos concurrentes sobre la
/// misma cuenta pueden pasar la consulta juntos antes de que el primero anote, así que el exceso
/// posible es la concurrencia en vuelo y no más: la ventana se llena igual y los siguientes reciben
/// `429`. Reservar el permiso antes de derivar lo cerraría, pero haría que un ingreso correcto
/// gastara cuota, que es exactamente lo que el párrafo anterior descarta. Y la concurrencia en vuelo
/// la acota el tope por origen.
///
/// EL RECHAZO NO DICE SI LA CUENTA EXISTE. La partición se forma con el correo escrito, exista o no
/// una cuenta con él, y un correo inexistente gasta un permiso por fallo igual que una contraseña
/// equivocada: el `429` llega en el mismo intento, con la misma cabecera y sin cuerpo en los dos
/// casos. Es la misma neutralidad que `Api CU-01` §6 exige al `401`.
/// </remarks>
public sealed class CredentialAttemptThrottle : IDisposable
{
    private readonly PartitionedRateLimiter<string> _failures;
    private readonly RateLimitingOptions _options;

    public CredentialAttemptThrottle(RateLimitingOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;

        // Quince segmentos: la cuota vuelve de a un minuto sobre la ventana de quince por omisión,
        // en vez de volver entera de golpe cuando la ventana pasa. Las particiones inactivas las
        // descarta el propio limitador particionado, de modo que rociar correos inventados no deja
        // un limitador vivo por cada uno.
        _failures = PartitionedRateLimiter.Create<string, string>(account =>
            RateLimitPartition.GetSlidingWindowLimiter(account, _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = options.CredentialFailuresPerAccount,
                Window = TimeSpan.FromSeconds(options.CredentialFailureWindowSeconds),
                SegmentsPerWindow = 15,
                QueueLimit = 0,
                AutoReplenishment = true,
            }));
    }

    /// <summary>
    /// El `429` si la cuenta agotó sus fallos en la ventana, y nulo si todavía puede intentar. **No
    /// gasta nada**: pide cero permisos.
    /// </summary>
    public IResult? Refusal(string? writtenEmail, HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var lease = _failures.AttemptAcquire(EmailIdentity.Normalize(writtenEmail), permitCount: 0);
        if (lease.IsAcquired)
        {
            return null;
        }

        // LA MISMA FORMA QUE EL RECHAZO DEL MIDDLEWARE: `429`, `Retry-After` en segundos y sin
        // cuerpo (`Contratos-REST.md` §4.1). Un consumidor no tiene por qué distinguir cuál de las
        // dos cuotas se agotó, y distinguirlo tampoco le diría nada sobre la cuenta.
        context.Response.Headers.RetryAfter =
            ContractRateLimiting.RetryAfterSeconds(lease, _options.CredentialFailureWindowSeconds);

        return Results.StatusCode(StatusCodes.Status429TooManyRequests);
    }

    /// <summary>Anota un intento fallido contra la cuenta que el correo escrito nombra, exista o no.</summary>
    public void RecordFailure(string? writtenEmail)
    {
        using var _ = _failures.AttemptAcquire(EmailIdentity.Normalize(writtenEmail), permitCount: 1);
    }

    public void Dispose() => _failures.Dispose();
}
