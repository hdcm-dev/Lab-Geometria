using GeometriaFactory.Web;
using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ============================================================================================
// LAS CLAVES DE PROTECCIÓN DE DATOS TIENEN QUE SOBREVIVIR AL PROCESO, y hasta el 2026-09-01 NO
// LO HACÍAN. No había ninguna llamada a `AddDataProtection` en el producto, así que regía el
// comportamiento por omisión, y en el anfitrión real ese comportamiento es EL PEOR POSIBLE. Su
// propio registro lo venía diciendo, apagado, desde el primer día:
//
//     warn: EphemeralXmlRepository[50]
//           Using an in-memory repository. Keys will not be persisted to storage.
//     warn: XmlKeyManager[59]
//           Neither user profile nor HKLM registry available. Using an ephemeral key repository.
//
// QUÉ SIGNIFICA, MEDIDO Y NO SUPUESTO. El 2026-09-01 arrancaron CINCO procesos en cinco horas y
// media, cada uno con su propio juego de claves, y el registro trae TRES fallos con TRES claves
// distintas —dos de ellas generadas por procesos anteriores—:
//
//     fail: Antiforgery[7] The antiforgery token could not be decrypted.
//           The key {e226af34-…} was not found in the key ring.
//
// Eso no es un aviso teórico: es la prueba de que HAY CARGAS PROTEGIDAS POR UN PROCESO QUE SE
// PRESENTAN A OTRO. Y por ahí pasan TRES cosas del producto, no una:
//
//   1. Los testigos de antifalsificación — el envío de cualquier formulario falla.
//   2. La marca de sesión — la persona se encuentra afuera sin haber salido.
//   3. LOS DESCRIPTORES DE COMPONENTE DE BLAZOR, que viajan protegidos dentro del marcado
//      prerrenderizado. Si no se pueden descifrar al arrancar el circuito, EL COMPONENTE NUNCA
//      SE VUELVE INTERACTIVO y el navegador no muestra ningún error: los botones quedan
//      dibujados y muertos.
//
// EL DIRECTORIO VA FUERA DE `wwwroot` A PROPÓSITO. `App_Data` es la convención de IIS para datos
// del sitio que no se sirven, y además esta pieza sólo publica archivos estáticos desde
// `wwwroot`: las claves no tienen por dónde salir aunque el anfitrión se distraiga.
//
// SE FALLA AL ARRANCAR SI NO SE PUEDE PERSISTIR, y es deliberado. La alternativa es exactamente
// lo que ya pasó: seguir andando con claves efímeras, romper de a ratos y sin síntoma legible, y
// que nadie se entere durante semanas. Un almacén de claves que no se puede escribir es un
// defecto de despliegue, y un defecto de despliegue tiene que impedir el despliegue.
//
// `SetApplicationName` fija el propósito: sin él, dos despliegues que compartan carpeta se
// pisarían las claves.
// ============================================================================================
var directorioDeClaves = builder.Configuration["DataProtection:KeysDirectory"] is { Length: > 0 } declarado
    ? Path.IsPathRooted(declarado) ? declarado : Path.Combine(builder.Environment.ContentRootPath, declarado)
    : Path.Combine(builder.Environment.ContentRootPath, "App_Data", "claves");

try
{
    Directory.CreateDirectory(directorioDeClaves);
    var sonda = Path.Combine(directorioDeClaves, ".escritura");
    File.WriteAllText(sonda, string.Empty);
    File.Delete(sonda);
}
catch (Exception falla)
{
    throw new InvalidOperationException(
        $"No se puede escribir el almacén de claves en '{directorioDeClaves}'. Sin claves persistidas, " +
        "los testigos de antifalsificación, la marca de sesión y los componentes interactivos dejan de " +
        "funcionar cada vez que el anfitrión recicla el proceso, y lo hacen SIN MOSTRAR NINGÚN ERROR. " +
        "Se puede declarar otra ruta con 'DataProtection:KeysDirectory'.", falla);
}

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(directorioDeClaves))
    .SetApplicationName("GeometriaFactory.Web");

// LA RUTA QUEDA DISPONIBLE PARA QUE LA PÁGINA DE ESTADO LA PUEDA DECLARAR: que esto funcione no
// se comprueba leyendo un registro que puede estar apagado —lo estuvo— sino mirando el sitio.
builder.Services.AddSingleton(new DataProtectionState(directorioDeClaves));

// INTERACTIVIDAD POR PÁGINA, Y NO GLOBAL. `App.razor` explica por qué: la superficie de ingreso
// tiene que poder escribir la marca de sesión, y eso sólo se puede hacer durante una petición
// HTTP de verdad. Dentro de un circuito las cabeceras ya salieron.
builder.Services.AddCascadingAuthenticationState();

// LA MARCA DE SESIÓN DEL NAVEGADOR (`Web ADR-03` §2; intake §17.6, `RT` §9.2). Los tres
// atributos no son elección de acá: los declara la fuente. `HttpOnly` es lo que hace que la
// marca «no sea legible por guion», que es la frase textual de la ADR.
builder.Services.AddAuthentication(SessionCookieDefaults.Scheme)
    .AddCookie(SessionCookieDefaults.Scheme, options =>
    {
        options.Cookie.Name = SessionCookieDefaults.CookieName;
        options.Cookie.HttpOnly = true;
        // `Secure` SIN EXCEPCIÓN en todo entorno que no sea el de desarrollo. En desarrollo
        // local la pieza se sirve por `http://`, y un navegador DESCARTA una cookie `Secure`
        // que llega en claro: sin esta salvedad, entrar no funcionaría en la máquina de quien
        // construye. La salvedad NO alcanza a producción, donde `somee` sirve por HTTPS y la
        // política vuelve a ser `Always`, que es lo que el intake §RT 9.2 exige.
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.SameAsRequest
            : CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        // La marca dura lo que dura la ventana del navegador: no se pide que sobreviva al cierre.
        options.Cookie.MaxAge = null;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;

        // Sin sesión no se ofrece nada del panel, y el desvío es a `Ingreso` (`ADR-03` §2,
        // guardián 2). Sigue ACOTANDO y no haciendo cumplir: quien verifica es el servicio de
        // datos, en cada solicitud.
        options.LoginPath = "/ingreso";
        options.LogoutPath = "/ingreso";
        options.AccessDeniedPath = "/ingreso";
    });

builder.Services.AddAuthorization();

// EL TESTIGO FIRMADO SE QUEDA DE ESTE LADO. Alcance de APLICACIÓN: es lo que hace que la sesión
// sobreviva a una recarga y a una pestaña nueva, sin que el testigo se acerque al navegador.
builder.Services.AddSingleton<SessionTokenStore>();

// De quién es el cambio forzado en curso, contra una marca opaca del navegador. Es lo que permite
// que la pantalla del cambio **no vuelva a pedir el correo** que la persona acaba de escribir.
builder.Services.AddSingleton<PendingCredentialChangeStore>();

// LA SONDA DEL APROVISIONAMIENTO, TAMBIÉN CON ALCANCE DE APLICACIÓN. Es lo que hace que el
// guardián 1 no cueste un viaje de red por navegación: recuerda el «sí» para siempre —el estado
// es de ida y no vuelve— y no recuerda el «no» ni un segundo, porque la transición ocurre una
// sola vez en la vida de la instancia y es la que un caché con vencimiento rompe. La asimetría
// está fundamentada en el comentario de `ProvisioningStateProbe`.
builder.Services.AddSingleton<ProvisioningStateProbe>();

// EL ESTADO DEL SERVICIO DE DATOS ES DEL LABORATORIO, NO DE UNA SESIÓN, y por eso es singleton:
// que dos personas mirando dos pestañas compartan la misma lectura es lo correcto, y es lo que
// evita una llamada de red por página dibujada. Ver `DataServiceReachability` para la decisión
// del Product Owner que lo origina —descartó el nombre público estable por ser un laboratorio
// académico, y pidió en su lugar que la desconexión SE VEA VENIR—.
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<DataServiceReachability>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionState>();

// La dirección del servicio de datos llega POR CONFIGURACIÓN y nunca embebida en el código
// (intake §17.6; `Web ADR-07`). El valor real vive como secreto del repositorio y no se versiona.
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException(
        "Falta 'ApiBaseUrl'. La dirección del servicio de datos llega por configuración.");

builder.Services.AddHttpClient<DataServiceClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl, UriKind.Absolute);
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// La dirección que no existe se reejecuta contra `/no-encontrado` CONSERVANDO el código 404:
// sin esto el cuerpo llega vacío, porque el `<NotFound>` del enrutador no gobierna el render
// estático del servidor. La pantalla es `NotFoundSurface`, propuesta declarada de la etapa `b`.
app.UseStatusCodePagesWithReExecute("/no-encontrado");

app.UseStaticFiles();
app.UseRouting();

// EL GUARDIÁN 1 DE `Web ADR-03` §2, Y VA PRIMERO DE LOS TRES A PROPÓSITO. Sin administrador
// configurado, cualquier ruta pedida desvía al aprovisionamiento inicial; con administrador, esa
// misma ruta deja de armar formulario y desvía de forma neutra. Va **antes** que el intermediario
// de sesión no restablecible y que el guardián 2 porque su primera mitad habla de **cualquier ruta
// pedida** y las de ellos hablan de rutas concretas bajo condiciones de sesión: mientras no hay
// administrador **no hay ninguna cuenta**, de modo que no puede haber sesión válida ni marca
// huérfana, ellos no tienen nada que decidir, y su desvío a `/ingreso` llevaría a una pantalla
// donde nadie puede entrar todavía. Al revés, una ruta del panel pedida en un laboratorio sin
// configurar terminaría en `/ingreso` en lugar de en el aprovisionamiento, que es lo contrario de
// lo que la ADR declara para el guardián 1.
//
// NO NECESITA AUTENTICAR Y POR ESO CORRE ANTES DE `UseAuthentication`: su decisión no mira quién
// pide, mira el estado del laboratorio. Y corre **después** de `UseStaticFiles`, que es lo que
// deja los recursos estáticos fuera de su alcance sin que haga falta nombrarlos uno por uno.
// Sigue ACOTANDO y no haciendo cumplir: quien impide el segundo administrador es el servicio de
// datos, que responde `409` a `A-03` aunque nadie pase por la pantalla.
app.UseMiddleware<ProvisioningGateMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// DESPUÉS de autenticar y ANTES de dibujar: es el único momento en que se sabe que la marca es
// válida y todavía se la puede borrar. Atiende el caso «marca presente, testigo ausente» que
// deja el reciclado del proceso (`Web ADR-03` §6.1).
app.UseMiddleware<UnrestorableSessionMiddleware>();

// EL GUARDIÁN 2 DE `Web ADR-03` §2, Y VA DESPUÉS DEL OTRO A PROPÓSITO. Sin sesión, las rutas del
// panel desvían a `/ingreso`. Corre después de autenticar —antes no se sabe si hay marca— y
// después del intermediario de sesión no restablecible, porque cuando los dos aplican tiene que
// hablar el otro: «marca presente, testigo ausente» desvía con el motivo declarado y corta, de
// modo que acá no se llega. Sigue ACOTANDO y no haciendo cumplir: quien verifica de verdad es el
// servicio de datos, en cada solicitud.
app.UseMiddleware<PanelSessionGateMiddleware>();

// TRADUCE EL FALLO DE VERIFICACIÓN ANTES DE QUE SALGA CRUDO. Va **antes** de la verificación
// porque tiene que envolverla: lo que atrapa lo lanza ella.
app.UseMiddleware<StaleFormMiddleware>();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
