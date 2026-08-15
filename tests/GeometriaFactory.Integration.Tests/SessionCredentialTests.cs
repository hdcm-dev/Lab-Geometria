using System.Security.Claims;
using System.Text.RegularExpressions;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// CUARTO CRITERIO DE TRANSICIÓN de la etapa `c`: **la credencial de sesión no es observable
/// desde el navegador**. Acá se demuestra sobre el custodio; en
/// <see cref="SessionCookieTests"/> se demuestra sobre las cabeceras y los cuerpos reales.
/// </summary>
/// <remarks>
/// QUÉ SIGNIFICA CONCRETAMENTE DESDE QUE HAY MARCA DE SESIÓN. El navegador recibe tres cosas: el
/// documento dibujado, los lotes de render que el circuito le manda, y **la marca de sesión**. De
/// las tres, las dos primeras son marcado que los componentes producen y la tercera es una
/// cabecera. La afirmación «el navegador no ve el testigo» es entonces tres afirmaciones: ningún
/// componente lo emite, la marca no lo lleva adentro, y no hay código que lo escriba en el
/// almacenamiento del navegador. Las dos primeras las ejerce <see cref="SessionCookieTests"/>
/// sobre las dos piezas corriendo; la tercera se verifica acá, sobre los archivos.
///
/// LO QUE ESTA CLASE CUIDA es la superficie del custodio: que ninguna propiedad de lectura
/// devuelva el testigo, porque una propiedad de lectura es interpolable en el marcado sin que
/// nadie lo note.
/// </remarks>
public sealed class SessionCredentialTests
{
    /// <summary>Testigo centinela. No se parece a nada más, para que encontrarlo sea concluyente.</summary>
    private const string SentinelToken = "CENTINELA-DE-CREDENCIAL-b7f4c1a9e2d640";

    private const string SessionEmail = "docente@frre.utn.edu.ar";

    /// <summary>Una identidad ya autenticada, como la que la marca de sesión reconstituye.</summary>
    private sealed class FixedAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _state;

        public FixedAuthenticationStateProvider(ClaimsPrincipal principal) =>
            _state = new AuthenticationState(principal);

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(_state);
    }

    private static SessionState OpenSession(out SessionTokenStore tokens, out string sessionId)
    {
        var identifier = Guid.NewGuid().ToString("N");
        var store = new SessionTokenStore();
        store.Keep(identifier, SentinelToken);

        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(SessionClaims.SessionId, identifier),
                new Claim(SessionClaims.AccountId, Guid.NewGuid().ToString()),
                new Claim(SessionClaims.Email, SessionEmail),
                new Claim(SessionClaims.Role, "Administrator"),
            ],
            SessionCookieDefaults.Scheme));

        tokens = store;
        sessionId = identifier;

        return new SessionState(
            new FixedAuthenticationStateProvider(principal),
            new HttpContextAccessor(),
            store);
    }

    [Fact]
    public async Task NoReadablePropertyOfTheCustodianYieldsTheCredential()
    {
        // POR QUÉ IMPORTA: una propiedad de lectura es interpolable en el marcado sin que nadie
        // lo note. El custodio expone el testigo SÓLO por un método, y esta prueba recorre toda
        // su superficie de lectura para comprobar que ninguna lo devuelve.
        var session = OpenSession(out var tokens, out var sessionId);
        await session.LoadAsync();

        Assert.True(session.IsOpen);
        Assert.Equal(SentinelToken, session.UseAccessToken());

        var readable = typeof(SessionState)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Where(property => property.CanRead)
            .Select(property => property.GetValue(session)?.ToString() ?? string.Empty)
            .ToArray();

        Assert.NotEmpty(readable);
        Assert.DoesNotContain(SentinelToken, readable);

        // Y la que sí se puede leer —el identificador de sesión— es lo que la marca lleva: no
        // autoriza nada por sí sola, y fuera de este proceso es una cadena sin significado.
        Assert.Equal(sessionId, session.SessionId);

        var fields = typeof(SessionState)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        Assert.Empty(fields);

        // Descartar el testigo del almacén cierra la sesión de verdad, aunque la marca siga.
        tokens.Discard(sessionId);
        Assert.Null(session.UseAccessToken());
        Assert.False(session.IsOpen);
    }

    [Fact]
    public void TheMarkOfSessionHasNoPlaceToPutTheCredential()
    {
        // LA AUSENCIA ES LA DECISIÓN: no hay una declaración para el testigo, de modo que nadie
        // puede ponerlo en la marca «sin darse cuenta». Lo que la marca lleva es identidad, papel
        // y un identificador opaco, y nada de eso sirve contra el servicio de datos.
        var declared = typeof(SessionClaims)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Select(field => (string)field.GetValue(null)!)
            .ToArray();

        Assert.Equal(4, declared.Length);
        Assert.All(declared, name => Assert.DoesNotContain("token", name, StringComparison.OrdinalIgnoreCase));

        // Y el testigo tiene un solo lugar donde vivir, que es del lado del servidor.
        var store = new SessionTokenStore();
        store.Keep("una-sesion", SentinelToken);
        Assert.True(store.Contains("una-sesion"));

        // El reciclado del proceso hace exactamente esto, y no avisa (`ADR-03` §6.1).
        store.Clear();
        Assert.False(store.Contains("una-sesion"));
        Assert.Null(store.Find("una-sesion"));
    }

    [Fact]
    public void TheContractOfTheExchangeStillCarriesTheCredentialServerToServer()
    {
        // El canje devuelve el testigo, y eso no cambia: lo que cambió es dónde queda. Si el
        // contrato dejara de traerlo, el almacén no tendría qué guardar.
        var session = new SessionResponse(SentinelToken, Guid.NewGuid(), SessionEmail, "Administrator");

        Assert.Equal(SentinelToken, session.AccessToken);
        Assert.Equal(SessionEmail, session.Email);
    }

    [Fact]
    public void ThePublicPieceHasNoWayOfWritingAnythingIntoTheBrowser()
    {
        // `RA-01` y el cuarto criterio, vistos desde el otro lado: no hay guion propio que
        // pudiera guardar el testigo en el navegador, ni llamada de interoperabilidad que
        // pudiera pasárselo a uno. El visor tridimensional es de la etapa `g` y su archivo se
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
