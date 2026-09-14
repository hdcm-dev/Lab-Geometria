using System.Collections.Concurrent;
using System.Globalization;
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
public sealed class CredentialAttemptThrottle
{
    /// <summary>
    /// A partir de cuántas cuentas con fallos anotados se barre lo vencido. Rociar correos
    /// inventados agrega entradas; el barrido las descarta cuando su ventana ya pasó, y los topes
    /// por origen acotan cuántas puede agregar un mismo origen por minuto.
    /// </summary>
    private const int SweepThreshold = 4096;

    private readonly ConcurrentDictionary<string, Queue<DateTimeOffset>> _failures = new(StringComparer.Ordinal);
    private readonly RateLimitingOptions _options;
    private readonly TimeProvider _time;

    public CredentialAttemptThrottle(RateLimitingOptions options)
        : this(options, TimeProvider.System)
    {
    }

    /// <remarks>
    /// POR QUÉ UNA VENTANA PROPIA Y NO `PartitionedRateLimiter` (mesa
    /// `SDD/Docs/Audit/Mesa-2026-09-14.md`, R-03). El limitador particionado del marco no permite
    /// quitar una partición, y sin eso el reseteo del docente no podía liberar a una cuenta
    /// limitada: la única salida era reiniciar el servicio, que las libera a todas. La ventana es
    /// la misma —tantos fallos por correo normalizado en tantos segundos, deslizante—, y lo que
    /// agrega es <see cref="Release"/>.
    /// </remarks>
    public CredentialAttemptThrottle(RateLimitingOptions options, TimeProvider time)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(time);

        _options = options;
        _time = time;
    }

    public IResult? Refusal(string? writtenEmail, HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!_failures.TryGetValue(EmailIdentity.Normalize(writtenEmail), out var failures))
        {
            return null;
        }

        var now = _time.GetUtcNow();
        var window = TimeSpan.FromSeconds(_options.CredentialFailureWindowSeconds);
        int count;
        DateTimeOffset oldest;

        lock (failures)
        {
            Prune(failures, now - window);
            count = failures.Count;
            oldest = count > 0 ? failures.Peek() : now;
        }

        if (count < _options.CredentialFailuresPerAccount)
        {
            return null;
        }

        // LA MISMA FORMA QUE EL RECHAZO DEL MIDDLEWARE: `429`, `Retry-After` en segundos y sin
        // cuerpo (`Contratos-REST.md` §4.1). Un consumidor no tiene por qué distinguir cuál de las
        // dos cuotas se agotó, y distinguirlo tampoco le diría nada sobre la cuenta. La espera es la
        // que falta para que venza el fallo más viejo, que es cuando vuelve un intento.
        var retryAfter = (int)Math.Ceiling((oldest + window - now).TotalSeconds);
        context.Response.Headers.RetryAfter = Math.Clamp(retryAfter, 1, _options.CredentialFailureWindowSeconds)
            .ToString(CultureInfo.InvariantCulture);

        return Results.StatusCode(StatusCodes.Status429TooManyRequests);
    }

    public void RecordFailure(string? writtenEmail)
    {
        var now = _time.GetUtcNow();
        var cutoff = now - TimeSpan.FromSeconds(_options.CredentialFailureWindowSeconds);
        var failures = _failures.GetOrAdd(EmailIdentity.Normalize(writtenEmail), _ => new Queue<DateTimeOffset>());

        lock (failures)
        {
            Prune(failures, cutoff);

            // UN FALLO NO SE ACUMULA MÁS ALLÁ DEL TOPE, igual que antes: el punto rechaza antes de
            // verificar, de modo que agotada la cuota no llega ningún fallo nuevo que alargue la espera.
            if (failures.Count < _options.CredentialFailuresPerAccount)
            {
                failures.Enqueue(now);
            }
        }

        if (_failures.Count > SweepThreshold)
        {
            Sweep(cutoff);
        }
    }

    /// <summary>
    /// Libera la cuota de intentos fallidos de una cuenta: la usa el reseteo del docente, que es la
    /// mitigación que `ADR-00011` §6 declara para una cuenta que un tercero dejó limitada.
    /// </summary>
    public void Release(string? email) => _failures.TryRemove(EmailIdentity.Normalize(email), out _);

    private static void Prune(Queue<DateTimeOffset> failures, DateTimeOffset cutoff)
    {
        while (failures.Count > 0 && failures.Peek() <= cutoff)
        {
            failures.Dequeue();
        }
    }

    private void Sweep(DateTimeOffset cutoff)
    {
        foreach (var (key, failures) in _failures)
        {
            bool empty;
            lock (failures)
            {
                Prune(failures, cutoff);
                empty = failures.Count == 0;
            }

            if (empty)
            {
                _failures.TryRemove(key, out _);
            }
        }
    }
}
