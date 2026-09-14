using System.Net;
using GeometriaFactory.Contracts.Accounts;
using GeometriaFactory.Web.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// R-02 — EL FRONT LE DICE AL SERVICIO DE DATOS DESDE QUÉ DIRECCIÓN LLEGÓ EL NAVEGADOR, y no le
/// presta la suya a toda la comisión (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`).
/// </summary>
/// <remarks>
/// POR QUÉ HACE FALTA. El servicio de datos limita el canje y lo anónimo por dirección de origen
/// (`Api ADR-00011`). Si el front no dice de dónde vino el navegador, todas sus peticiones llegan
/// con la dirección del front y comparten una sola cuota: un cliente anónimo que inunde el ingreso
/// deja a la clase entera afuera.
///
/// QUÉ SE MIRA. Lo que el front ENVÍA al servicio de datos, capturado en el manejador que hace de
/// servicio. El cliente se resuelve del contenedor del front, de modo que la prueba ejerce la
/// cadena de manejadores tal como `Program.cs` la arma, y no una construida a mano.
/// </remarks>
public sealed class BrowserOriginForwardingTests : IDisposable
{
    private readonly RecordingDataService _dataService = new();
    private readonly PublicPieceHarness _publicPiece;

    public BrowserOriginForwardingTests() => _publicPiece = new PublicPieceHarness(_dataService);

    public void Dispose() => _publicPiece.Dispose();

    [Fact]
    public async Task InsideABrowserRequestTheBrowserAddressTravelsToTheDataService()
    {
        using var scope = _publicPiece.Services.CreateScope();
        var accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        accessor.HttpContext = new DefaultHttpContext
        {
            Connection = { RemoteIpAddress = IPAddress.Parse("198.51.100.77") },
        };

        try
        {
            var client = scope.ServiceProvider.GetRequiredService<DataServiceClient>();
            await client.RegisterAccountAsync(new AccountRegistrationRequest("alumna@frre.utn.edu.ar", "Ana", "Diaz"));
        }
        finally
        {
            accessor.HttpContext = null;
        }

        var sent = Assert.Single(_dataService.Requests);
        Assert.Equal("198.51.100.77", Assert.Single(sent.ForwardedFor));
    }

    [Fact]
    public async Task ABrowserCannotChooseItsPartitionByWritingTheHeaderItself()
    {
        using var scope = _publicPiece.Services.CreateScope();
        var accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        var context = new DefaultHttpContext
        {
            Connection = { RemoteIpAddress = IPAddress.Parse("198.51.100.78") },
        };
        context.Request.Headers["X-Forwarded-For"] = "203.0.113.250";
        accessor.HttpContext = context;

        try
        {
            var client = scope.ServiceProvider.GetRequiredService<DataServiceClient>();
            await client.RegisterAccountAsync(new AccountRegistrationRequest("alumno@frre.utn.edu.ar", "Juan", "Perez"));
        }
        finally
        {
            accessor.HttpContext = null;
        }

        // SE ENVÍA LA DIRECCIÓN QUE EL FRONT RESOLVIÓ, Y SÓLO ÉSA: la que el navegador escribió en
        // la cabecera no viaja (sin proxy declarado en el front, `UseForwardedHeaders` la ignora).
        var sent = Assert.Single(_dataService.Requests);
        Assert.Equal("198.51.100.78", Assert.Single(sent.ForwardedFor));
    }

    [Fact]
    public async Task OutsideABrowserRequestNoAddressIsInvented()
    {
        using var scope = _publicPiece.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext = null;

        var client = scope.ServiceProvider.GetRequiredService<DataServiceClient>();
        await client.RegisterAccountAsync(new AccountRegistrationRequest("otra@frre.utn.edu.ar", "Eva", "Luna"));

        var sent = Assert.Single(_dataService.Requests);
        Assert.Empty(sent.ForwardedFor);
    }

    /// <summary>Hace de servicio de datos: anota lo que recibe y responde un error cualquiera.</summary>
    private sealed class RecordingDataService : HttpMessageHandler
    {
        public List<(string Path, string[] ForwardedFor)> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var forwarded = request.Headers.TryGetValues("X-Forwarded-For", out var values) ? values.ToArray() : [];
            lock (Requests)
            {
                Requests.Add((request.RequestUri!.AbsolutePath, forwarded));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
        }
    }
}
