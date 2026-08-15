namespace GeometriaFactory.Web.Services;

/// <summary>
/// EL GUARDIÁN 2 DE <c>Web ADR-03</c> §2: «ninguna ruta del panel es accesible sin sesión».
/// Sin marca de sesión, las rutas del panel desvían a <c>/ingreso</c>.
/// </summary>
/// <remarks>
/// ACOTA Y NO HACE CUMPLIR, Y LA DISTINCIÓN NO ES RETÓRICA (`ADR-03` §2 y §6.2). Esto decide
/// **qué se le ofrece** a quien pide una dirección sin sesión; **no es control de acceso**. Quien
/// verifica de verdad la pertenencia y el papel es el servicio de datos, **en cada solicitud**, y
/// esta pieza no lo reemplaza: si alguien fuerza la solicitud sin pasar por la pantalla, la
/// negativa la tiene que producir el otro lado. Por eso **no se saca ni se afloja ninguna
/// comprobación de allá** por el hecho de que este intermediario exista. El navegador no es
/// confiable, y este archivo corre del lado del servidor pero decide sobre lo que el navegador
/// pidió.
///
/// QUÉ ES «DEL PANEL» Y QUÉ NO. Del panel son las siete rutas de trabajo, que es donde hay algo
/// de alguien. **No son del panel** —y siguen siendo públicas— el destino inicial, el
/// aprovisionamiento, el registro, el ingreso, las dos superficies de credencial propia, el
/// estado, la pantalla de no encontrado y los recursos estáticos. La lista de acá es de
/// **inclusión** y no de exclusión, y es deliberado: lo que no está nombrado pasa, de modo que
/// agregar una superficie pública nueva no exige acordarse de este archivo, y agregar una del
/// panel sí. El riesgo se eligió del lado en que se nota.
///
/// `/credencial-propia/cambio-obligado` NO SE GATEA, Y ES `INV-09`. El cambio forzado de
/// contraseña se llega **sin sesión de trabajo**: la cuenta con la marca puesta se autentica y no
/// obtiene sesión (`ADR-03` §2, guardián 4). Gatearlo dejaría a esa persona sin ninguna ruta a la
/// que ir —el guardián 2 la mandaría a `Ingreso` y el 4 la traería de vuelta acá—, que es
/// exactamente la vuelta cerrada que `RN-13` prohíbe.
///
/// CÓMO SE COORDINA CON <see cref="UnrestorableSessionMiddleware"/>, QUE CORRE ANTES. Los dos
/// pueden aplicar a la misma petición, y el orden decide cuál habla. El otro atiende «marca
/// presente, testigo ausente» —el reciclado del proceso de `ADR-03` §6.1—: borra la marca y
/// desvía a `/ingreso?estado=sesion-no-restablecible`, y **corta ahí**, de modo que este guardián
/// no llega a correr y la persona lee **por qué** volvió. Si corriera al revés, la misma persona
/// vería un `/ingreso` mudo y la marca muerta seguiría puesta, repitiendo el desvío en cada
/// petición. Cuando la marca directamente no está —que es la mayoría de los casos— el otro no
/// hace nada y éste desvía, sin motivo declarado, porque no hay ninguno que declarar: nunca hubo
/// sesión.
/// </remarks>
public sealed class PanelSessionGateMiddleware
{
    /// <summary>
    /// La opción de configuración que habilita el paseo sin sesión, y que **sólo tiene efecto en
    /// el entorno de desarrollo**.
    /// </summary>
    /// <remarks>
    /// LA MISMA FORMA QUE `Secure` EN `Program.cs`, Y POR EL MISMO MOTIVO. Ahí la política de la
    /// marca se afloja en desarrollo porque la pieza se sirve por `http://` y sin esa salvedad no
    /// se podría entrar en la máquina de quien construye; acá el guardián se abre en desarrollo
    /// porque `scripts/verify-navigation.sh` recorre las trece pantallas para que el Product Owner
    /// las apruebe, y ese recorrido es anterior a que hubiera sesión. **La salvedad NO alcanza a
    /// ningún otro entorno**: en producción el guardián rige sin excepción **aunque la opción esté
    /// puesta**, y eso está probado y no sólo comentado —`PanelSessionGateTests`—. Ponerla en
    /// producción no abre nada; a lo sumo deja una constancia de que alguien lo intentó.
    /// </remarks>
    public const string WalkthroughSetting = "PanelWalkthroughWithoutSession";

    /// <summary>A dónde se desvía sin sesión, sin motivo declarado: nunca hubo sesión que perder.</summary>
    private const string SignInPath = "/ingreso";

    /// <summary>
    /// Las siete rutas del panel, por prefijo de segmento. `/trabajos` cubre `/trabajos/{id}` y
    /// `/trabajos/{id}/editar`, que son la misma pertenencia.
    /// </summary>
    private static readonly string[] PanelRoutes =
    [
        "/mis-trabajos", "/trabajo-nuevo", "/trabajos", "/cuentas", "/entrega-comision", "/mi-contrasena",
    ];

    private readonly RequestDelegate _next;
    private readonly bool _walkthrough;

    public PanelSessionGateMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(environment);

        _next = next;

        // LA CONJUNCIÓN SE RESUELVE ACÁ, UNA SOLA VEZ, Y CON EL ENTORNO A LA IZQUIERDA. Que el
        // entorno mande y la opción sólo pueda acompañarlo es lo que hace que la puerta no exista
        // fuera de desarrollo: no hay ningún camino por el que la configuración sola la abra.
        _walkthrough = environment.IsDevelopment()
            && configuration.GetValue<bool>(WalkthroughSetting);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!_walkthrough && IsOfThePanel(context.Request.Path) && !HasSession(context))
        {
            context.Response.Redirect(SignInPath);
            return;
        }

        await _next(context).ConfigureAwait(false);
    }

    /// <summary>
    /// Hay sesión cuando la marca trae el identificador opaco, y no cuando el marco dice que la
    /// petición está autenticada: son dos cosas distintas y la que importa es la primera.
    /// </summary>
    private static bool HasSession(HttpContext context) =>
        context.User.FindFirst(SessionClaims.SessionId) is not null;

    private static bool IsOfThePanel(PathString path) =>
        PanelRoutes.Any(route => path.StartsWithSegments(route, StringComparison.Ordinal));
}
