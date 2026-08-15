using GeometriaFactory.Api.Composition;
using GeometriaFactory.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// El arranque se detiene cuando la clave de firma no llegó, y no un momento después.
/// </summary>
/// <remarks>
/// POR QUÉ EXISTE ESTA BATERÍA. En el despliegue del 2026-08-15 el servicio de datos se levantó
/// sin la variable de entorno de la clave, respondió el punto de salud como si estuviera sano, y
/// el fallo apareció recién cuando una persona intentó entrar: en pantalla, un error genérico;
/// el motivo real, sólo en el registro del servidor. Nada del producto impedía ese estado.
///
/// Lo que estas pruebas fijan no es un mensaje sino un MOMENTO: el defecto tiene que aparecer
/// donde lo ve quien despliega. La pieza pública ya aplicaba el mismo criterio a la dirección
/// del servicio de datos; acá se lo aplica a la clave de firma.
/// </remarks>
public sealed class SigningKeyStartupTests
{
    private static IServiceCollection Compose(string? signingKey)
    {
        // La ruta del almacén va siempre: su propia guardia de arranque corre ANTES que la de la
        // clave, y sin ella estas pruebas medirían el fallo equivocado. Que exista esa otra
        // guardia es justamente el precedente que faltaba aplicar a la clave de firma.
        var values = new Dictionary<string, string?>
        {
            [$"ConnectionStrings:{CompositionRoot.StoreConnectionName}"] = "Data Source=:memory:",
        };

        if (signingKey is not null)
        {
            values[$"{SigningOptions.SectionName}:{nameof(SigningOptions.SigningKey)}"] = signingKey;
        }

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(values).Build();

        return new ServiceCollection().AddCompositionRoot(configuration);
    }

    [Fact]
    public void TheStartupStopsWhenTheSigningKeyNeverArrived()
    {
        var failure = Assert.Throws<InvalidOperationException>(() => Compose(signingKey: null));

        // El mensaje nombra la llave de configuración, que es lo único que le sirve a quien
        // despliega: qué falta y dónde se pone.
        Assert.Contains(SigningOptions.SectionName, failure.Message, StringComparison.Ordinal);
        Assert.Contains(nameof(SigningOptions.SigningKey), failure.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ABlankKeyCountsAsAbsentAndAlsoStopsTheStartup(string blank)
        => Assert.Throws<InvalidOperationException>(() => Compose(blank));

    [Fact]
    public void TheStartupStopsWhenTheKeyIsShorterThanTheAlgorithmAccepts()
    {
        var tooShort = new string('k', AccessTokenIssuer.MinimumSigningKeySizeInBytes - 1);

        var failure = Assert.Throws<InvalidOperationException>(() => Compose(tooShort));

        // Una clave corta emite accesos que después no se pueden verificar: detener el arranque
        // es preferible a emitir sesiones inválidas.
        Assert.Contains(
            AccessTokenIssuer.MinimumSigningKeySizeInBytes.ToString(System.Globalization.CultureInfo.InvariantCulture),
            failure.Message,
            StringComparison.Ordinal);

        // Y NO FILTRA LA CLAVE: ni entera ni en fragmentos.
        Assert.DoesNotContain(tooShort, failure.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("kkkk", failure.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AKeyOfTheAcceptedSizeLetsTheStartupThrough()
    {
        var accepted = new string('k', AccessTokenIssuer.MinimumSigningKeySizeInBytes);

        var services = Compose(accepted);

        // La guardia no es un obstáculo: con la clave provista, la composición entrega el
        // material de firma como siempre.
        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(SigningOptions));
    }
}
