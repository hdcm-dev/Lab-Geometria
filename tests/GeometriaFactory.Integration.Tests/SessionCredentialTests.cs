using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Web.Components.Layout;
using GeometriaFactory.Web.Components.Pages;
using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// CUARTO CRITERIO DE TRANSICIÓN de la etapa `c`: **la credencial de sesión no es observable
/// desde el navegador**. Acá se demuestra, no se afirma.
/// </summary>
/// <remarks>
/// QUÉ SIGNIFICA CONCRETAMENTE BAJO INTERACTIVIDAD DE SERVIDOR. El navegador recibe exactamente
/// dos cosas: el documento prerrenderizado y, después, los **lotes de render** que el circuito
/// le manda por la conexión de tiempo real. Los dos son **el marcado que los componentes
/// producen**. De modo que la afirmación «el navegador no ve la credencial» es, palabra por
/// palabra, la afirmación «ningún componente emite la credencial en su marcado» —más «nadie la
/// escribe en una cookie ni en el almacenamiento del navegador», que es lo que cubren los
/// controles de abajo—.
///
/// CÓMO SE DEMUESTRA. Se abre una sesión **de verdad** con una credencial centinela, se dibujan
/// los componentes que tienen acceso a ella y se mira el marcado producido, que es byte por byte
/// lo que viajaría. Y se comprueba que la prueba NO ES VACÍA: el mismo marcado sí trae el correo
/// de la sesión, de modo que si el dibujo hubiera salido «sin sesión» la prueba lo delataría en
/// lugar de pasar por ausencia.
///
/// QUÉ NO DEMUESTRA, Y HAY QUE DECIRLO: no se manejó un navegador de verdad. No hay aquí una
/// sesión conducida por un navegador sin cabeza que inspeccione `document.cookie` y el
/// almacenamiento local en vivo. Lo que sí está cubierto por construcción es que **no existe
/// código que pudiera escribir ahí**: la pieza pública no tiene ningún guion propio ni ninguna
/// llamada de interoperabilidad, y eso se verifica abajo sobre los archivos.
/// </remarks>
public sealed class SessionCredentialTests
{
    /// <summary>Credencial centinela. No se parece a nada más del marcado, para que encontrarla sea concluyente.</summary>
    private const string SentinelToken = "CENTINELA-DE-CREDENCIAL-b7f4c1a9e2d640";

    private const string SessionEmail = "docente@frre.utn.edu.ar";

    private sealed class StaticNavigationManager : NavigationManager
    {
        public StaticNavigationManager(string uri) => Initialize("http://localhost/", uri);

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            // La prueba no navega: dibujar es todo lo que hace.
        }
    }

    private static ServiceProvider BuildServices(string uri, out SessionState session)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<NavigationManager>(new StaticNavigationManager(uri));
        services.AddSingleton(new DataServiceClient(new HttpClient { BaseAddress = new Uri("http://el-servicio-de-datos/") }));

        var state = new SessionState();
        state.Open(new SessionResponse(SentinelToken, Guid.NewGuid(), SessionEmail, "Administrator"));
        services.AddSingleton(state);

        session = state;
        return services.BuildServiceProvider();
    }

    private static async Task<string> RenderAsync<TComponent>(string uri, Dictionary<string, object?>? parameters = null)
        where TComponent : IComponent
    {
        using var provider = BuildServices(uri, out var session);
        Assert.True(session.IsOpen);
        Assert.Equal(SentinelToken, session.UseAccessToken());

        await using var renderer = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());

        return await renderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await renderer.RenderComponentAsync<TComponent>(
                ParameterView.FromDictionary(parameters ?? []));

            return output.ToHtmlString();
        });
    }

    [Fact]
    public async Task TheSurfaceThatUsesTheCredentialDoesNotEmitItIntoItsMarkup()
    {
        // `/mi-contrasena` es la ÚNICA superficie de esta etapa que ejerce la credencial: se la
        // pide al custodio y se la pasa al cliente del servicio de datos. Si algún día alguien
        // la interpolara en el marcado, esta prueba se cae.
        var html = await RenderAsync<OwnCredentialChange>("http://localhost/mi-contrasena");

        Assert.DoesNotContain(SentinelToken, html, StringComparison.Ordinal);

        // Y la prueba NO es vacía: el componente se dibujó CON la sesión abierta, que es lo que
        // muestra el formulario de cambio en lugar del aviso de que hay que entrar.
        Assert.Contains("credential-current", html, StringComparison.Ordinal);
    }

    [Fact]
    public async Task TheWorkShellShowsTheIdentityAndNotTheCredential()
    {
        var body = (RenderFragment)(builder => builder.AddMarkupContent(0, "<h1>Cuerpo</h1>"));
        var html = await RenderAsync<WorkShell>(
            "http://localhost/entrega-comision",
            new Dictionary<string, object?> { ["Body"] = body });

        Assert.DoesNotContain(SentinelToken, html, StringComparison.Ordinal);

        // No vacía: el armazón dibujó la sesión —el correo y el papel— y no una pantalla anónima.
        Assert.Contains(SessionEmail, html, StringComparison.Ordinal);
        Assert.Contains("Administrador", html, StringComparison.Ordinal);
    }

    [Fact]
    public async Task TheSignInSurfaceNeitherReceivesNorEmitsTheCredential()
    {
        var html = await RenderAsync<SignIn>("http://localhost/ingreso?estado=sesion-cerrada");

        Assert.DoesNotContain(SentinelToken, html, StringComparison.Ordinal);
        Assert.Contains("Cerraste sesión", html, StringComparison.Ordinal);
    }

    [Fact]
    public void NoReadablePropertyOfTheCustodianYieldsTheCredential()
    {
        // POR QUÉ IMPORTA: una propiedad de lectura es interpolable en el marcado sin que nadie
        // lo note. El custodio expone la credencial SÓLO por un método, y esta prueba recorre
        // toda su superficie de lectura para comprobar que ninguna la devuelve.
        var session = new SessionState();
        session.Open(new SessionResponse(SentinelToken, Guid.NewGuid(), SessionEmail, "Administrator"));

        var readable = typeof(SessionState)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Where(property => property.CanRead)
            .Select(property => property.GetValue(session)?.ToString() ?? string.Empty)
            .ToArray();

        Assert.NotEmpty(readable);
        Assert.DoesNotContain(SentinelToken, readable);

        var fields = typeof(SessionState)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        Assert.Empty(fields);

        // Y cerrar la sesión la descarta de verdad.
        session.Close();
        Assert.Null(session.UseAccessToken());
        Assert.False(session.IsOpen);
    }

    [Fact]
    public void ThePublicPieceHasNoWayOfWritingAnythingIntoTheBrowser()
    {
        // `RA-01` y el cuarto criterio, vistos desde el otro lado: no hay guion propio que
        // pudiera guardar la credencial en el navegador, ni llamada de interoperabilidad que
        // pudiera pasársela a uno. El visor tridimensional es de la etapa `g` y su archivo se
        // genera; se lo excluye nombrándolo, para que la exclusión sea deliberada.
        var web = Path.Combine(RepositoryRoot(), "src", "GeometriaFactory.Web");

        string[] forbidden =
        [
            "localStorage", "sessionStorage", "document.cookie",
            "IJSRuntime", "InvokeVoidAsync", "InvokeAsync<", "JSInvokable",
        ];

        var offenders = new List<string>();

        foreach (var file in Directory.EnumerateFiles(web, "*.*", SearchOption.AllDirectories)
                     .Where(path => path.EndsWith(".razor", StringComparison.Ordinal)
                         || path.EndsWith(".cs", StringComparison.Ordinal))
                     .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.Ordinal)
                         && !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.Ordinal)))
        {
            var text = File.ReadAllText(file);
            offenders.AddRange(forbidden
                .Where(token => text.Contains(token, StringComparison.Ordinal))
                .Select(token => $"{Path.GetFileName(file)}: {token}"));
        }

        Assert.Empty(offenders);

        // Y no hay ninguna llamada de red desde el navegador: `RA-01` completo.
        var scripts = Directory.EnumerateFiles(Path.Combine(web, "wwwroot", "js"), "*.js", SearchOption.AllDirectories);
        foreach (var script in scripts)
        {
            var text = File.ReadAllText(script);
            Assert.DoesNotContain("localStorage", text, StringComparison.Ordinal);
            Assert.DoesNotContain("sessionStorage", text, StringComparison.Ordinal);
            Assert.DoesNotContain("document.cookie", text, StringComparison.Ordinal);
            Assert.False(Regex.IsMatch(text, @"\bfetch\s*\(|XMLHttpRequest|WebSocket\s*\("),
                $"{Path.GetFileName(script)} hace una llamada de red desde el navegador.");
        }
    }

    private static string RepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "GeometriaFactory.sln")))
        {
            directory = directory.Parent;
        }

        Assert.NotNull(directory);
        return directory!.FullName;
    }
}
