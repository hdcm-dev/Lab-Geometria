using GeometriaFactory.Web.Integration;
using GeometriaFactory.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Levanta la pieza PÚBLICA de verdad, en memoria, y la deja hablando con la pieza de datos
/// también de verdad.
/// </summary>
/// <remarks>
/// POR QUÉ LAS DOS PIEZAS Y NO UNA SIMULADA. Lo que se verifica acá es que la marca de sesión
/// del navegador **no transporta el testigo** y que la sesión sobrevive a una petición nueva. Con
/// un canje simulado el testigo sería un literal de prueba y la afirmación no diría nada del
/// producto: hace falta el testigo firmado real, emitido por el servicio de datos.
///
/// LO ÚNICO QUE SE SUSTITUYE ES EL TRANSPORTE ENTRE LAS DOS. La dirección del servicio de datos
/// llega por configuración —que es como llega en producción— y el mensajero se enchufa al
/// servidor en memoria de la otra pieza. Ni la autenticación, ni el almacén de testigos, ni la
/// antifalsificación, ni el encaminamiento se tocan: son los del producto.
/// </remarks>
public sealed class PublicPieceHarness : WebApplicationFactory<SessionTokenStore>
{
    private readonly HttpMessageHandler _dataServiceHandler;

    public PublicPieceHarness(HttpMessageHandler dataServiceHandler) =>
        _dataServiceHandler = dataServiceHandler;

    /// <summary>El almacén de testigos del proceso, para poder ejercitar el reciclado.</summary>
    public SessionTokenStore Tokens => Services.GetRequiredService<SessionTokenStore>();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // EL ENTORNO SE FIJA, Y NO SE HEREDA. El andamiaje del marco levanta la pieza en
        // `Development` si nadie dice otra cosa, y en `Development` la pieza tiene DOS salvedades
        // que sólo existen ahí: la marca de sesión deja de ser `Secure` —porque el desarrollo se
        // sirve por `http://`— y el guardián 2 admite la puerta de servicio del paseo sin sesión.
        // Comprobar el producto contra un entorno con salvedades no comprueba el producto: estas
        // baterías miran la pieza como la sirve `somee`. Quien necesita el otro entorno lo pide
        // explícito, y `PanelSessionGateTests` lo hace para ejercer justamente el límite.
        builder.UseEnvironment("Production");

        builder.UseSetting("ApiBaseUrl", "http://el-servicio-de-datos/");

        builder.ConfigureTestServices(services =>
            services.AddHttpClient<DataServiceClient>(client =>
                    client.BaseAddress = new Uri("http://el-servicio-de-datos/", UriKind.Absolute))
                .ConfigurePrimaryHttpMessageHandler(() => _dataServiceHandler));
    }
}
