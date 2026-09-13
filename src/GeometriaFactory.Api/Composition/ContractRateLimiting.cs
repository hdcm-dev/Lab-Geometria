using System.Globalization;
using System.Threading.RateLimiting;
using GeometriaFactory.Api.Endpoints;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// El límite de tasa del contrato REST: **por persona autenticada** para quien presenta un acceso
/// firmado, y **por dirección de origen** para quien no lo presenta (`BT-00029`). El canje suma
/// además un límite de fallos **por cuenta**, que vive en <see cref="CredentialAttemptThrottle"/>
/// (`ADR-00011`).
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE, Y POR QUÉ RECIÉN AHORA. `ADR-00005` §2 aceptó no tener límite de caudal «porque
/// el único cliente legítimo es la pieza pública y el alcance es de aula». Esa premisa cayó con la
/// fase `k`: la superficie se publica en Internet bajo `/v1/` (`ADR-00010`) para aplicaciones
/// propias además del front (`ADR-00009`), y un cliente que insista puede saturar al **único
/// escritor** del almacén (`ADR-06002`). `05` §8.1 fija el caudal sostenido en 20 peticiones por
/// minuto para una comisión; lo que este archivo hace es que ninguna persona ni ningún origen pueda
/// consumir por sí solo lo que está previsto para todos.
///
/// EL SUJETO DEL LÍMITE ES LA PERSONA, NUNCA UNA CLAVE DE APLICACIÓN, porque no existe ninguna
/// (`ADR-00009`). Con un acceso firmado válido la partición es el reclamo `sub` de la credencial de
/// cuatro reclamos (`ADR-00003`), leído por <see cref="AuthenticationEndpoints.AccountIdOf"/>: dos
/// pestañas de la misma persona comparten cuota, dos personas detrás de la misma dirección no. Sin
/// acceso válido —los cuatro puntos anónimos, y también una petición a un punto bajo la guardia con
/// acceso ausente, vencido o mal firmado— la partición es la dirección de origen.
///
/// LOS UMBRALES SE DERIVAN DEL NFR Y SE DEJAN CONFIGURABLES, con sus valores por omisión en
/// <see cref="RateLimitingOptions"/> y en `appsettings.json` (`RateLimiting__*`). El fundamento de
/// cada cifra está sobre su propiedad. Cambiarlos es una decisión de quien despliega y no exige
/// recompilar; lo que no es configurable es el sujeto de la partición.
///
/// LA DIRECCIÓN DE ORIGEN LLEGA POR `X-Forwarded-For`, Y SÓLO SE LE CREE A UN PROXY CONOCIDO. El
/// servicio corre detrás del túnel de Cloudflare, y para el zócalo toda petición viene del
/// contenedor del túnel: sin `UseForwardedHeaders` la partición por dirección **colapsa en una sola**
/// y el límite por origen se aplica a Internet entero como si fuera un cliente. Las redes de los
/// proxies conocidos llegan por configuración —`ForwardedHeaders__KnownNetworks__0=172.23.0.0/16`,
/// la red de la composición del despliegue— y **no están escritas acá**: escribirlas sería
/// sellar en la imagen la topología de un host, que es lo que `deploy/compose.yaml` explica que no
/// se hace. Sin esa llave se conservan sólo las de bucle local del marco, y la cabecera de cualquier
/// otro origen se ignora: creerle a todos dejaría que un cliente eligiera su propia partición.
///
/// UN ORIGEN PUEDE SER UNA COMISIÓN ENTERA, Y POR ESO NINGÚN LÍMITE POR DIRECCIÓN PUEDE SER ESTRICTO.
/// `GeometriaFactory.Web` habla con este servicio servidor a servidor y no reenvía la dirección del
/// navegador: para la partición por origen, toda la comisión que entra por el front es **una**
/// dirección. Y aunque la reenviara, en la facultad los alumnos salen por un mismo NAT. `BT-00029`
/// lo sabía y aun así dejó el canje en treinta por minuto por origen: en producción el front agotó
/// ese cupo por la clase entera, y la comisión se quedó afuera por un límite pensado contra un
/// atacante, que choca con `RN-B1`. La corrección (`ADR-00011`) mueve la protección a donde apunta el
/// ataque —la cuenta— y deja los topes por origen **holgados**, del tamaño de una comisión detrás de
/// una sola dirección: sirven contra quien rocía muchas cuentas o inunda el servicio, no contra una
/// clase.
///
/// EL EXCESO RESPONDE `429` CON `Retry-After` EN SEGUNDOS Y SIN CUERPO. No lleva código del contrato
/// porque el conjunto cerrado no declara ninguno para una cuota y esta capa no inventa códigos
/// (`ADR-00004`): es la tercera respuesta sin código de `Contratos-REST.md` §5.1, al lado del `401`
/// de la guardia. Lo que el consumidor necesita saber —cuándo volver— lo dice la cabecera.
///
/// `/salud` QUEDA FUERA, junto con el explorador. Se aplica al grupo `/v1` de `Program.cs` y no
/// globalmente: el `healthcheck` de las dos composiciones sondea `/salud` cada 30 segundos desde
/// dentro del host, y un límite que lo alcanzara podría apagar un contenedor sano
/// (`ADR-00007` §2 punto 3, `ContractRoutePrefix.ExemptRoutes`).
/// </remarks>
public static class ContractRateLimiting
{
    /// <summary>
    /// La política del contrato: por persona con acceso válido, por dirección de origen sin él.
    /// Se aplica al grupo `/v1` entero.
    /// </summary>
    public const string ContractPolicy = "contrato";

    /// <summary>
    /// La política del canje de credenciales (`A-01`), **propia y siempre por dirección**: es el tope
    /// de intentos que un origen puede hacer contra cuentas cualesquiera, cada uno con una derivación
    /// anclada (`ADR-06004`) que el servicio paga aunque la contraseña sea incorrecta. Reemplaza a
    /// <see cref="ContractPolicy"/> sobre ese punto. **No es la defensa contra la fuerza bruta sobre
    /// una cuenta**: ésa es <see cref="CredentialAttemptThrottle"/>, porque un origen puede ser una
    /// comisión entera.
    /// </summary>
    public const string CredentialExchangePolicy = "canje";

    /// <summary>Nombre de la sección de configuración con las redes de los proxies conocidos.</summary>
    public const string KnownNetworksSetting = "ForwardedHeaders:KnownNetworks";

    /// <summary>
    /// Registra las dos políticas y la confianza en `X-Forwarded-For`. Lo que `Program.cs` conecta
    /// es `UseForwardedHeaders` al principio de la tubería y `UseRateLimiter` después de autenticar,
    /// que es lo que hace que la partición pueda leer el reclamo de identidad.
    /// </summary>
    public static IServiceCollection AddContractRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var options = new RateLimitingOptions();
        configuration.GetSection(RateLimitingOptions.SectionName).Bind(options);
        options.Validate();
        services.AddSingleton(options);

        // UNA SOLA INSTANCIA PARA TODO EL PROCESO: la cuota por cuenta es estado del servicio, y dos
        // instancias serían dos cuotas para la misma cuenta.
        services.AddSingleton<CredentialAttemptThrottle>();

        // LOS PROXIES CONOCIDOS SE SUMAN A LOS DEL MARCO Y NO LOS REEMPLAZAN: el bucle local sigue
        // siendo de confianza, que es lo que los guiones de desarrollo y los E2E usan.
        services.Configure<ForwardedHeadersOptions>(forwarded =>
        {
            forwarded.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            foreach (var network in configuration.GetSection(KnownNetworksSetting).Get<string[]>() ?? [])
            {
                if (!System.Net.IPNetwork.TryParse(network, out var parsed))
                {
                    throw new InvalidOperationException(
                        $"'{KnownNetworksSetting}' trae '{network}', que no es una red en notación " +
                        "CIDR (por ejemplo 172.23.0.0/16). Se detiene el arranque en lugar de " +
                        "ignorar la cabecera de un proxy que quien despliega quiso declarar.");
                }

                forwarded.KnownIPNetworks.Add(parsed);
            }
        });

        services.AddRateLimiter(limiter =>
        {
            limiter.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            limiter.OnRejected = (context, _) =>
            {
                context.HttpContext.Response.Headers.RetryAfter = RetryAfterSeconds(context.Lease, options.WindowSeconds);
                return ValueTask.CompletedTask;
            };

            limiter.AddPolicy(ContractPolicy, context =>
            {
                var accountId = AuthenticationEndpoints.AccountIdOf(context.User);

                return accountId is null
                    ? Window($"origen:{OriginOf(context)}", options.PermitsPerAddress, options)
                    : Window($"persona:{accountId}", options.PermitsPerPerson, options);
            });

            limiter.AddPolicy(CredentialExchangePolicy, context =>
                Window($"canje:{OriginOf(context)}", options.CredentialExchangePermitsPerAddress, options));
        });

        return services;
    }

    /// <summary>
    /// Ventana deslizante y no fija: con la fija, quien agota la cuota en el último segundo de una
    /// ventana vuelve a tenerla entera en el primero de la siguiente, y el pico real es el doble del
    /// declarado. Los seis segmentos son la granularidad con la que la ventana se desplaza; la cola
    /// es cero porque una petición que excede la cuota se rechaza y no se retiene: el consumidor
    /// decide cuándo volver con `Retry-After`.
    /// </summary>
    private static RateLimitPartition<string> Window(string key, int permits, RateLimitingOptions options) =>
        RateLimitPartition.GetSlidingWindowLimiter(key, _ => new SlidingWindowRateLimiterOptions
        {
            PermitLimit = permits,
            Window = TimeSpan.FromSeconds(options.WindowSeconds),
            SegmentsPerWindow = 6,
            QueueLimit = 0,
            AutoReplenishment = true,
        });

    /// <summary>
    /// La dirección después de `UseForwardedHeaders`: la del navegador si la puso un proxy conocido,
    /// la del zócalo si no. Una petición sin dirección —el servidor de pruebas en memoria no la
    /// tiene— cae en una partición propia y no en la de nadie.
    /// </summary>
    private static string OriginOf(HttpContext context) =>
        context.Connection.RemoteIpAddress?.ToString() ?? "desconocido";

    /// <summary>
    /// Segundos enteros hasta que la cuota vuelve a tener lugar, redondeados hacia arriba: decir un
    /// segundo de menos manda al consumidor a recibir otro `429`. Si el limitador no supo decirlo,
    /// se responde la ventana entera, que es la espera que seguro alcanza.
    /// </summary>
    /// <remarks>
    /// LA VENTANA DESLIZANTE DEL MARCO NO INFORMA LA ESPERA, y se midió: su arrendamiento rechazado no
    /// trae el metadato, así que en la práctica la respuesta es la ventana entera —60 en el contrato,
    /// la de fallos en la cuenta—. Se deja la lectura del metadato para el limitador que sí lo traiga.
    /// </remarks>
    internal static string RetryAfterSeconds(RateLimitLease lease, int windowSeconds)
    {
        var seconds = lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter) && retryAfter > TimeSpan.Zero
            ? Math.Max(1, (int)Math.Ceiling(retryAfter.TotalSeconds))
            : windowSeconds;

        return seconds.ToString(CultureInfo.InvariantCulture);
    }
}

/// <summary>
/// Los umbrales del límite de tasa. Llegan por la sección `RateLimiting` de la configuración
/// (`RateLimiting__PermitsPerPerson`, etc.) y **éstos son sus valores por omisión**.
/// </summary>
/// <remarks>
/// LAS CIFRAS SALEN DEL NFR Y NO DE UNA MEDICIÓN, y se declara. `05` §8.1 fija el caudal sostenido en
/// **20 peticiones por minuto para una comisión** —rotulado [ASUNCIÓN] y provisorio desde que el
/// volumen de la comisión se cerró como incognoscible—. Cada umbral de abajo es un múltiplo de esa
/// cifra elegido con un criterio escrito, y cuando `PT-05` mida el uso real se ajustan por
/// configuración, sin tocar este archivo.
///
/// LOS TOPES POR ORIGEN NO SE DERIVAN DE ESA CIFRA SINO DE UNA COMISIÓN DETRÁS DE UNA DIRECCIÓN, y
/// desde `ADR-00011`. El tamaño de la comisión **no tiene cifra**: el Product Owner cerró `D5` por
/// incognoscible el 2026-08-20 (`SDD/Docs/README.md` §8). La que se usa es la mayor que el producto
/// midió: el corte de **334 alumnos** de `Audit/Medicion-Volumen-De-Comision-2026-08-31.md`, que
/// declara que el servicio sobra a ese volumen. Redondeada a trescientos, es una comisión de
/// «cientos» —el escenario más grande que esa medición contempla— entrando junta en el mismo minuto.
/// </remarks>
public sealed class RateLimitingOptions
{
    /// <summary>La sección de configuración que gobierna a esta clase.</summary>
    public const string SectionName = "RateLimiting";

    /// <summary>
    /// El largo de la ventana, en segundos. **Un minuto**, porque es la unidad en la que el NFR está
    /// escrito y en la que un consumidor razona un `Retry-After`.
    /// </summary>
    public int WindowSeconds { get; set; } = 60;

    /// <summary>
    /// Peticiones por ventana para una persona con acceso válido. **60**: el triple del caudal
    /// previsto para la comisión entera, que es una petición por segundo sostenida durante un minuto.
    /// Una persona operando la pantalla a mano no llega —cada acción del front son una o dos
    /// peticiones— y un guion que reintenta en bucle sí, que es lo que se quiere cortar. Una cifra
    /// igual al NFR habría dejado a un alumno que edita con entusiasmo a un clic del `429`.
    /// </summary>
    public int PermitsPerPerson { get; set; } = 60;

    /// <summary>
    /// Peticiones por ventana para una dirección de origen sin acceso válido. **1200**: trescientos
    /// alumnos detrás de una dirección —el front, o el NAT de la facultad—, cuatro peticiones anónimas
    /// cada uno en el minuto en que entran: la sonda del aprovisionamiento, el registro, el cambio de
    /// la contraseña provisoria y un reintento. Era **120** hasta `ADR-00011`, y con la comisión
    /// entera detrás del front eso eran treinta alumnos. Lo que un origen anónimo hace acá son
    /// lecturas baratas y altas; los dos puntos que comprueban contraseña tienen además la cuota por
    /// cuenta, que es la que para la fuerza bruta.
    /// </summary>
    public int PermitsPerAddress { get; set; } = 1200;

    /// <summary>
    /// Canjes de credenciales por ventana para una dirección de origen, contra cuentas cualesquiera.
    /// **600**: trescientos alumnos detrás de una dirección, con un error de tipeo cada uno. Era **30**
    /// hasta `ADR-00011`, y ese número dejaba entrar a treinta alumnos por minuto a la comisión entera.
    /// El tope ya no es la defensa de una cuenta —ésa es <see cref="CredentialFailuresPerAccount"/>—
    /// sino la del servicio contra quien rocía cuentas: seiscientas derivaciones por minuto desde un
    /// origen es lo máximo que ese origen le puede hacer pagar al procesador.
    /// </summary>
    public int CredentialExchangePermitsPerAddress { get; set; } = 600;

    /// <summary>
    /// Intentos **fallidos** por cuenta en su ventana, desde cualquier origen. **10**: alcanza para
    /// equivocarse varias veces al tipear sin quedar afuera, y con la ventana de abajo acota un
    /// diccionario a cuarenta contraseñas por hora por cuenta, que contra `ADR-06004` no es un ataque.
    /// Los ingresos correctos no cuentan. Es la cifra baja que OWASP pide para el *login throttling*
    /// por cuenta; no hay NFR del que derivarla, y se deja configurable.
    /// </summary>
    public int CredentialFailuresPerAccount { get; set; } = 10;

    /// <summary>
    /// El largo de la ventana de fallos por cuenta, en segundos. **900**, quince minutos: una ventana
    /// de un minuto devolvería los diez intentos cada minuto —seiscientos por hora por cuenta—, y la
    /// cifra de arriba dejaría de ser baja. Quien se trabó espera un cuarto de hora o le pide al
    /// docente el reseteo de la contraseña.
    /// </summary>
    public int CredentialFailureWindowSeconds { get; set; } = 900;

    /// <summary>
    /// Un umbral en cero o negativo no es «sin límite»: es una configuración que no puede querer
    /// nadie, y se detiene el arranque nombrando la llave, igual que con la clave de firma.
    /// </summary>
    public void Validate()
    {
        Require(WindowSeconds, nameof(WindowSeconds));
        Require(PermitsPerPerson, nameof(PermitsPerPerson));
        Require(PermitsPerAddress, nameof(PermitsPerAddress));
        Require(CredentialExchangePermitsPerAddress, nameof(CredentialExchangePermitsPerAddress));
        Require(CredentialFailuresPerAccount, nameof(CredentialFailuresPerAccount));
        Require(CredentialFailureWindowSeconds, nameof(CredentialFailureWindowSeconds));
    }

    private static void Require(int value, string name)
    {
        if (value < 1)
        {
            throw new InvalidOperationException(
                $"'{SectionName}:{name}' vale {value} y tiene que ser un entero mayor que cero. " +
                "El límite de tasa no admite «sin límite»: se detiene el arranque en lugar de " +
                "atender con una cuota que no se puede cumplir.");
        }
    }
}
