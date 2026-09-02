using System.Collections.Concurrent;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// Los avisos que el marco emite MIENTRAS LA APLICACIÓN ARRANCA, guardados para que el producto
/// pueda declararlos en vez de esperar a que alguien abra un archivo.
/// </summary>
/// <remarks>
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// POR QUÉ EXISTE, Y CUÁNTO COSTÓ NO TENERLO. El 2026-09-01 el anfitrión real estuvo semanas
/// corriendo con las claves de protección de datos EN MEMORIA. El marco lo decía en la PRIMERA
/// LÍNEA de cada arranque:
///
///     warn: EphemeralXmlRepository[50]
///           Using an in-memory repository. Keys will not be persisted to storage.
///
/// Y nadie lo leyó nunca, porque el registro de salida del anfitrión venía **apagado desde el
/// primer despliegue**. El defecto estuvo escrito todo el tiempo, en un archivo que no existía.
///
/// La mesa lo levantó como `AB-2` y lo dejó dicho así: **ningún control del producto mira el
/// registro del anfitrión**. Este archivo da vuelta la pregunta —en vez de ir a leer el registro
/// del anfitrión, que depende de un interruptor ajeno y de que alguien se acuerde, **el producto
/// escucha su propio marco y publica lo que oyó**—.
///
/// ═══════════════════ POR QUÉ SÓLO EL ARRANQUE, Y NO TODO EL TIEMPO ═══════════════════
///
/// Porque lo que se gana con cada uno es distinto, y mezclarlos rompería la compuerta.
///
/// Un aviso de ARRANQUE describe **cómo quedó configurado el proceso**: es estable, no depende de
/// quién esté navegando, y si aparece está mal siempre. Sirve como condición de despliegue.
///
/// Un fallo de FUNCIONAMIENTO —un testigo de antifalsificación vencido, por ejemplo— lo puede
/// producir una persona con una pestaña vieja, y es legítimo. Si contara para la compuerta, la
/// página de estado quedaría en rojo por una pestaña olvidada y **la publicación siguiente
/// fallaría por algo que no es un defecto**. Una compuerta que se pone en rojo sola es una
/// compuerta que se termina apagando: ya pasó con `C-3`, que estuvo catorce días informando un
/// defecto real que nadie leía.
///
/// ═══════════════════ POR QUÉ UNA LISTA CORTA Y NO TODO ═══════════════════
///
/// Escuchar todas las categorías dejaría la página de estado llena de ruido, y una compuerta
/// ruidosa se desactiva sola en la cabeza de quien la lee. Se escuchan **las que describen cómo
/// quedó armada la pieza**, que son las que producen fallos silenciosos y difusos.
/// ═══════════════════════════════════════════════════════════════════════════════════════════
/// </remarks>
public sealed class StartupObservations
{
    /// <summary>
    /// Las familias de categorías que se escuchan.
    /// </summary>
    /// <remarks>
    /// SON LAS TRES QUE FALLAN EN SILENCIO, y esa es la regla que las elige: cuando algo de esta
    /// familia se rompe, **no hay error visible en ninguna pantalla** —una sesión que se cae sola,
    /// un formulario rechazado, un componente que nunca se vuelve interactivo—. Es exactamente la
    /// clase de defecto que estuvo semanas escondido.
    ///
    /// SE PROBARON `Hosting` Y `Server` Y SE RETIRARON, medido el 2026-09-02: el servidor emite
    /// avisos que dependen del entorno y no del producto —«Overriding address(es)» aparece en un
    /// contenedor con la variable de entorno puesta y NO aparece bajo IIS, donde ese servidor ni
    /// siquiera se usa—. Una compuerta que se pone en rojo por el entorno se termina apagando: ya
    /// pasó con `C-3`. **Se prefiere una lista corta que se lee a una larga que se ignora.**
    /// </remarks>
    private static readonly string[] Escuchadas =
    [
        "Microsoft.AspNetCore.DataProtection",
        "Microsoft.AspNetCore.Antiforgery",
        "Microsoft.AspNetCore.Authentication",
    ];

    /// <summary>
    /// Tope de avisos guardados.
    /// </summary>
    /// <remarks>
    /// NO ES POR MEMORIA: es porque una lista larga no se lee. Con más de esto en el arranque, el
    /// problema ya no es cuál es el aviso.
    /// </remarks>
    private const int Tope = 12;

    private readonly ConcurrentQueue<Aviso> _avisos = new();
    private int _cerrado;

    /// <summary>Si la ventana de arranque ya se cerró.</summary>
    public bool Cerrado => Volatile.Read(ref _cerrado) == 1;

    /// <summary>
    /// Los avisos que este producto YA MIRÓ, decidió, y no vuelve a tratar como novedad.
    /// </summary>
    /// <remarks>
    /// ═══ POR QUÉ HAY UNA LISTA DE DECLARADOS, Y POR QUÉ NO ES UNA LISTA DE SILENCIADOS ═══
    ///
    /// Un aviso declarado **se sigue mostrando**. Lo único que cambia es que no tumba la
    /// publicación, porque ya se decidió qué hacer con él y la respuesta fue «así queda, por este
    /// motivo». La diferencia con silenciarlo es toda: silenciado no se ve, declarado se lee junto
    /// con su razón.
    ///
    /// Y ES CORTA A PROPÓSITO. Cada entrada acá es una decisión que alguien tomó y firmó; una
    /// lista que crece sin que nadie la mire vuelve a ser el archivo apagado del que salimos.
    /// </remarks>
    private static readonly (string Fragmento, string Motivo)[] Declarados =
    [
        ("No XML encryptor configured",
         "Las claves quedan sin cifrar en disco. Se intentó cifrarlas y no se pudo: el anfitrión " +
         "no expone perfil de usuario —lo dice su propio registro—, de modo que el cifrado del " +
         "sistema falla al crear la clave, y no hay certificado en este laboratorio. Decisión " +
         "declarada del Product Owner: proyecto académico en infraestructura hogareña."),
    ];

    /// <summary>Los avisos observados, en el orden en que llegaron.</summary>
    public IReadOnlyList<Aviso> Avisos => _avisos.ToArray();

    /// <summary>Los avisos que nadie declaró: éstos sí son novedad, y éstos sí bloquean.</summary>
    public IReadOnlyList<Aviso> SinDeclarar => _avisos.Where(a => a.Motivo is null).ToArray();

    /// <summary>Si la categoría es una de las que se escuchan.</summary>
    public static bool Interesa(string categoria) =>
        Escuchadas.Any(e => categoria.StartsWith(e, StringComparison.Ordinal));

    /// <summary>Anota un aviso, si la ventana de arranque sigue abierta.</summary>
    public void Anotar(string categoria, string mensaje)
    {
        if (Cerrado || _avisos.Count >= Tope)
        {
            return;
        }

        var motivo = Declarados
            .FirstOrDefault(d => mensaje.Contains(d.Fragmento, StringComparison.Ordinal))
            .Motivo;

        _avisos.Enqueue(new Aviso(categoria, mensaje, motivo));
    }

    /// <summary>
    /// Cierra la ventana. Se llama cuando el servidor terminó de arrancar.
    /// </summary>
    /// <remarks>
    /// A PARTIR DE ACÁ NO SE ANOTA MÁS, y eso es lo que mantiene honesta la compuerta: lo que
    /// venga después es funcionamiento, y el funcionamiento tiene causas legítimas que un
    /// despliegue no debería tomar por defectos.
    /// </remarks>
    public void Cerrar() => Interlocked.Exchange(ref _cerrado, 1);

    /// <param name="Categoria">Quién lo dijo, sin el prefijo largo.</param>
    /// <param name="Mensaje">Qué dijo, recortado a lo que se puede leer de un vistazo.</param>
    /// <param name="Motivo">Por qué se acepta, si es un apartamiento declarado. Nulo si es novedad.</param>
    public sealed record Aviso(string Categoria, string Mensaje, string? Motivo)
    {
        /// <summary>El nombre corto de la categoría, que es lo que se muestra.</summary>
        public string NombreCorto => Categoria.Split('.').LastOrDefault() ?? Categoria;
    }
}

/// <summary>
/// El proveedor de registro que alimenta a <see cref="StartupObservations"/>.
/// </summary>
/// <remarks>
/// NO REEMPLAZA A NINGÚN OTRO PROVEEDOR NI SILENCIA NADA: se suma. Lo que el marco escriba sigue
/// yendo a donde iba —incluido el registro del anfitrión, si alguien lo enciende—; esto sólo se
/// queda con una copia de lo que importa, para que el producto la pueda declarar.
/// </remarks>
public sealed class StartupObservationsProvider(StartupObservations observaciones) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) =>
        StartupObservations.Interesa(categoryName)
            ? new Escucha(observaciones, categoryName)
            : NullLogger.Instancia;

    public void Dispose()
    {
    }

    private sealed class Escucha(StartupObservations observaciones, string categoria) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        // SÓLO AVISO Y PEOR. Un mensaje informativo del arranque no describe un defecto, y
        // anotarlo llenaría la lista corta con lo que no hay que mirar.
        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Warning;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var texto = formatter(state, exception);

            // SE RECORTA ACÁ Y NO AL DIBUJAR: lo que se guarda es lo que se va a mostrar, y así
            // no hay dos versiones del mismo mensaje según quién lo lea.
            observaciones.Anotar(categoria, texto.Length > 200 ? texto[..200] + "…" : texto);
        }
    }

    private sealed class NullLogger : ILogger
    {
        public static readonly NullLogger Instancia = new();

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(
            LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
        }
    }
}
