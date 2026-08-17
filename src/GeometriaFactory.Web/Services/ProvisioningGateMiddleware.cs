namespace GeometriaFactory.Web.Services;

/// <summary>
/// EL GUARDIÁN 1 DE <c>Web ADR-03</c> §2, con sus DOS MITADES: «mientras no exista la cuenta de
/// administrador, cualquier ruta pedida desvía al aprovisionamiento inicial; una vez que existe,
/// esa ruta deja de armar formulario para siempre y desvía de forma neutra, sin explicar por qué».
/// </summary>
/// <remarks>
/// POR QUÉ ESTE ARCHIVO NO EXISTÍA HASTA HOY, Y NO ERA UN OLVIDO. `ADR-03` §2 declara cuatro
/// guardianes; la etapa `c` construyó el 3 y el 4 en las superficies, la sesión trajo el 2 en
/// <see cref="PanelSessionGateMiddleware"/>, y el 1 quedó sin construir **porque no se podía
/// construir**: la pieza pública no tenía **ningún punto de acceso con el que preguntar si el
/// laboratorio ya tiene administrador**. `A-03` configura —es escritura—, `A-16` responde por la
/// salud del servicio y `A-06` exige ser administrador; ninguno le sirve a quien todavía no se
/// identificó, que es exactamente quien pasa por acá. El faltante era **de la especificación**, y
/// se cerró agregando `A-17` a la superficie HTTP.
///
/// LO QUE HABÍA MIENTRAS TANTO, PARA QUE EL DEFECTO QUEDE DIMENSIONADO. Con un administrador ya
/// configurado, `/aprovisionamiento-inicial` seguía **sirviendo su formulario** —cinco campos, dos
/// de contraseña— a cualquiera que entrara sin identificarse. **No era un agujero de seguridad**:
/// el servicio de datos rechaza un segundo administrador con `ADMINISTRATOR_ALREADY_CONFIGURED`,
/// y eso está probado en las tres capas. Era un defecto **de superficie**: el producto ofrecía una
/// puerta que ya no lleva a ninguna parte.
///
/// ACOTA Y NO HACE CUMPLIR, IGUAL QUE LOS OTROS TRES (`ADR-03` §2 y §6.2). Esto decide **qué se
/// ofrece**; **no es control de acceso**. Quien impide el segundo administrador es el servicio de
/// datos, en cada solicitud, y **no se afloja ninguna comprobación de allá** por el hecho de que
/// este intermediario exista. Forzar la petición contra `A-03` sin pasar por la pantalla sigue
/// respondiendo `409`, y así se verifica.
///
/// POR QUÉ VA PRIMERO, ANTES QUE EL DE SESIÓN NO RESTABLECIBLE Y QUE EL GUARDIÁN 2. Porque su
/// primera mitad habla de **cualquier ruta pedida**, y las otras dos hablan de rutas concretas
/// bajo condiciones de sesión. Mientras no hay administrador **no hay ninguna cuenta**, de modo
/// que no puede haber sesión válida, ni marca huérfana que explicar, ni panel al que ofrecer
/// entrada: los otros dos intermediarios no tienen nada que decidir y su desvío a `/ingreso`
/// llevaría a una pantalla donde nadie puede entrar todavía. Al revés —con el guardián 2 primero—
/// una ruta del panel pedida en un laboratorio sin configurar terminaría en `/ingreso` en lugar de
/// en el aprovisionamiento, que es lo contrario de lo que `ADR-03` §2 declara para el guardián 1.
///
/// EL COSTO DE PREGUNTAR ESTÁ RESUELTO EN <see cref="ProvisioningStateProbe"/> Y NO ACÁ, y su
/// comentario explica la asimetría: el «sí» se recuerda para siempre —el estado es de ida y no
/// vuelve— y el «no» no se recuerda ni un segundo, porque la transición «no configurado →
/// configurado» ocurre **una sola vez en la vida de la instancia** y es la que un caché con
/// vencimiento rompe.
/// </remarks>
public sealed class ProvisioningGateMiddleware
{
    /// <summary>La ruta del aprovisionamiento inicial: destino de la mitad 1 y sujeto de la 2.</summary>
    private const string ProvisioningPath = "/aprovisionamiento-inicial";

    /// <summary>
    /// A dónde desvía la mitad 2, y **el nombre dice neutro a propósito**.
    /// </summary>
    /// <remarks>
    /// `ADR-03` §2 y §6.4: el desvío del aprovisionamiento ya resuelto **no explica por qué**.
    /// De modo que acá no hay ningún `?estado=` ni ningún motivo colgado de la dirección, y la
    /// pantalla de destino no recibe nada que le permita decir «ya hay administrador». El destino
    /// es el que `Linea-Base-Visual.md` §5 le da a `NAV-03` —entrada con administrador constituido
    /// va a `Ingreso`—, y es además el único lugar razonable al que mandar a alguien que quería
    /// configurar un laboratorio que ya está configurado.
    ///
    /// QUE NADIE LE AGREGUE UN MOTIVO DESPUÉS «PARA QUE SE ENTIENDA»: entenderlo es precisamente
    /// lo que la ADR no quiere que se le regale a un anónimo.
    /// </remarks>
    private const string NeutralDestination = "/ingreso";

    /// <summary>
    /// Lo que este guardián **no** desvía nunca, por prefijo. Lista **cerrada**.
    /// </summary>
    /// <remarks>
    /// QUÉ ES «UNA RUTA PEDIDA» Y QUÉ NO. `ADR-03` §2 habla de rutas del producto: direcciones que
    /// una persona pide y que devuelven una pantalla. **Un recurso estático no es eso**, y
    /// desviarlo rompería la pantalla a la que el guardián manda: la hoja de estilos y el guion de
    /// interacción los pide el navegador **mientras dibuja el aprovisionamiento**, y devolverle un
    /// desvío a cada uno dejaría esa pantalla sin sistema visual. Peor todavía con `/_framework` y
    /// `/_blazor`: el aprovisionamiento es una superficie **interactiva de servidor**, su formulario
    /// **envía por el circuito** y no por una petición a su propia ruta, de modo que desviar el
    /// circuito dejaría el botón «Crear la cuenta de administrador» sin efecto — el guardián
    /// impediría salir del estado que él mismo obliga a resolver. Es la vuelta cerrada que `RN-13`
    /// prohíbe, con otro disfraz.
    ///
    /// LA LISTA ES DE EXCLUSIÓN, AL REVÉS QUE LA DE `PanelSessionGateMiddleware`, Y ES DELIBERADO
    /// EN LOS DOS CASOS. Allá el riesgo caro es que una ruta del panel nueva quede sin gatear, y
    /// por eso la lista es de inclusión: lo que no está nombrado pasa. Acá el riesgo caro es el
    /// opuesto —una ruta nueva del producto que quede **fuera** del guardián seguiría ofreciendo
    /// pantallas en un laboratorio sin configurar—, y por eso lo que no está nombrado **se
    /// desvía**. Cada guardián elige el riesgo del lado en que se nota.
    /// </remarks>
    private static readonly string[] ExemptPrefixes =
    [
        "/_framework", "/_blazor", "/css", "/interaction", "/js",
    ];

    private readonly RequestDelegate _next;
    private readonly ProvisioningStateProbe _provisioning;
    private readonly bool _walkthrough;

    public ProvisioningGateMiddleware(
        RequestDelegate next,
        ProvisioningStateProbe provisioning,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(provisioning);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(environment);

        _next = next;
        _provisioning = provisioning;

        // ESTE GUARDIÁN RESPETA LA MISMA PUERTA DE SERVICIO QUE EL 2, Y LA DECISIÓN SE FUNDAMENTA.
        // `PanelWalkthroughWithoutSession` existe para que `scripts/verify-navigation.sh` recorra
        // las trece pantallas y el Product Owner las apruebe. Ese recorrido pide
        // `/aprovisionamiento-inicial` Y las otras doce, **sin servicio de datos levantado**: si
        // este guardián no honrara la puerta, o bien el laboratorio consultado no tendría
        // administrador y las doce restantes desviarían al aprovisionamiento, o bien lo tendría y
        // el aprovisionamiento desviaría a `/ingreso`. En los dos casos el paseo dejaría de ser un
        // paseo, y la puerta que se abrió para eso quedaría a medias.
        //
        // Se reusa **la misma opción** en lugar de declarar una segunda, porque es la misma
        // decisión —«dejá ver las pantallas sin las condiciones de producción»— y dos interruptores
        // para una decisión son dos lugares donde se puede quedar puesto el que no era.
        //
        // Y LA CONJUNCIÓN VA CON EL ENTORNO A LA IZQUIERDA, igual que allá: fuera de desarrollo el
        // guardián rige **sin excepción aunque la opción esté puesta**, y eso está probado y no
        // sólo comentado —`ProvisioningGateTests`—.
        _walkthrough = environment.IsDevelopment()
            && configuration.GetValue<bool>(PanelSessionGateMiddleware.WalkthroughSetting);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var path = context.Request.Path;

        if (_walkthrough || IsExempt(path))
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        var configured = await _provisioning
            .IsConfiguredAsync(context.RequestAborted)
            .ConfigureAwait(false);

        // «NO SE SABE» NO ES «NO», Y ACÁ NO SE DESVÍA NADA. Cuando el servicio de datos no
        // responde, la sonda devuelve nulo. Desviar sobre una suposición mandaría a todo el mundo
        // a un formulario que el servicio no va a poder atender, y taparía `/estado`, que es la
        // única pantalla desde la que se diagnostica que el servicio no responde. El guardián
        // ACOTA y no hace cumplir: dejar pasar cuando no sabe no abre ninguna regla, porque la
        // regla la sigue haciendo cumplir el otro lado.
        if (configured is null)
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        if (configured is false)
        {
            // MITAD 1 — sin administrador, cualquier ruta pedida desvía al aprovisionamiento.
            // MENOS LA PROPIA, que es la única salida del estado: desviarla a sí misma sería el
            // lazo cerrado, y el producto no tendría cómo configurarse nunca.
            if (!path.StartsWithSegments(ProvisioningPath, StringComparison.Ordinal))
            {
                context.Response.Redirect(ProvisioningPath);
                return;
            }
        }
        else if (path.StartsWithSegments(ProvisioningPath, StringComparison.Ordinal)
            || IsRoot(path))
        {
            // MITAD 2 — con administrador, el aprovisionamiento deja de armar formulario PARA
            // SIEMPRE y desvía de forma neutra. La respuesta no lleva ningún campo de formulario
            // y no lleva ningún texto que explique por qué: lo único que sale de acá es el desvío.
            //
            // Y LA RAÍZ DESVÍA IGUAL, que es `NAV-03` y la mitad que faltaba. `Linea-Base-Visual.md`
            // §5 declara **dos** filas cuyo disparador es la resolución del destino inicial: sin
            // administrador se va a configurarlo —`NAV-01`, la mitad 1 de arriba— y **con
            // administrador se va al ingreso**. Hasta acá sólo estaba la primera, de modo que quien
            // escribía `/` en un laboratorio ya configurado se quedaba mirando el marcador de
            // posición de la etapa `b` en lugar de entrar. **[completa la etapa `g`.]**
            //
            // LA RAÍZ NO ES UNA SUPERFICIE: es el punto donde corre este guardián. Que su página
            // exista es sólo para el caso en que el guardián **no pueda decidir**, que es el de
            // abajo.
            context.Response.Redirect(NeutralDestination);
            return;
        }

        await _next(context).ConfigureAwait(false);
    }

    /// <summary>
    /// La raíz, y sólo la raíz.
    /// </summary>
    /// <remarks>
    /// SE COMPARA EXACTO Y NO POR PREFIJO, porque `StartsWithSegments` sobre `/` da verdadero para
    /// **toda** dirección del producto: usarlo acá desviaría el sitio entero al ingreso.
    /// </remarks>
    private static bool IsRoot(PathString path) =>
        !path.HasValue || path.Value == "/";

    private static bool IsExempt(PathString path) =>
        ExemptPrefixes.Any(prefix => path.StartsWithSegments(prefix, StringComparison.Ordinal));
}
