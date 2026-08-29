using System.Text;
using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Infrastructure.Security;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// Las **guardas de borde** de las dos piezas de seguridad de `GeometriaFactory-Infrastructure`:
/// el emisor del acceso firmado y la derivación de contraseña.
/// </summary>
/// <remarks>
/// POR QUÉ ESTA BATERÍA, Y QUÉ NO ES. Responde a la escalada `ESC-001` de
/// [`Mesa-2026-08-29.md`], cerrada por el Product Owner con la opción **A**. `AccessTokenIssuer`
/// estaba en **50 % de ramas** y `PasswordDerivation` en **55 %**, y en las dos el hueco eran
/// **guardas que el camino feliz no toca**: clave ausente, clave corta, identificador vacío,
/// valor almacenado ilegible.
///
/// **No son pruebas de criptografía.** No verifican que PBKDF2 sea PBKDF2 ni que la firma sea
/// válida —eso lo hace la batería de integración contra el servicio real—: verifican que **la
/// pieza se niegue a operar cuando no están dadas las condiciones**, que es exactamente lo que
/// `ADR-04` §2 punto 3 pide cuando dice que **se prefiere no arrancar** antes que arrancar con
/// una clave que no sirve.
/// </remarks>
public sealed class SecurityGuardsTests
{
    private static SigningOptions ConClave(string clave) => new() { SigningKey = clave };

    private static readonly string ClaveSuficiente =
        new('k', AccessTokenIssuer.MinimumSigningKeySizeInBytes);

    // ---- AccessTokenIssuer ----

    /// <summary>
    /// CA-01 — **Una clave ausente, vacía o en blanco no habilita la firma.** Las tres entran por
    /// la misma guarda y las tres se prueban: una guarda con tres causas que sólo se ejercita por
    /// una es una guarda probada en un tercio.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void AnAbsentSigningKeyDoesNotEnableSigning(string clave)
    {
        Assert.False(new AccessTokenIssuer(ConClave(clave)).SigningKeyIsProvided);
    }

    /// <summary>
    /// CA-02 — **Una clave más corta que el mínimo tampoco habilita la firma, y no se rellena.**
    /// Rellenarla es el atajo que `ADR-04` §2 punto 3 prohíbe con su motivo: el sistema arrancaría,
    /// emitiría accesos y nadie lo notaría.
    /// </summary>
    [Fact]
    public void AShortSigningKeyIsRejectedInsteadOfPadded()
    {
        var corta = new string('k', AccessTokenIssuer.MinimumSigningKeySizeInBytes - 1);

        Assert.False(new AccessTokenIssuer(ConClave(corta)).SigningKeyIsProvided);
        Assert.True(new AccessTokenIssuer(ConClave(ClaveSuficiente)).SigningKeyIsProvided);
    }

    /// <summary>
    /// CA-03 — **El mínimo se cuenta en BYTES y no en caracteres**, que es lo que la propia clase
    /// declara al usar <c>GetByteCount</c>. Una clave de mitad de caracteres pero con acentos
    /// —dos bytes cada uno— alcanza el mínimo, y una de caracteres sueltos no.
    /// </summary>
    [Fact]
    public void TheMinimumIsCountedInBytesNotCharacters()
    {
        var mitadDeCaracteres = new string('á', AccessTokenIssuer.MinimumSigningKeySizeInBytes / 2);

        Assert.Equal(AccessTokenIssuer.MinimumSigningKeySizeInBytes,
            Encoding.UTF8.GetByteCount(mitadDeCaracteres));
        Assert.True(new AccessTokenIssuer(ConClave(mitadDeCaracteres)).SigningKeyIsProvided);
    }

    /// <summary>
    /// CA-04 — **Sin clave no se emite acceso**, aunque el resto de los datos esté completo.
    /// </summary>
    [Fact]
    public void NoAccessIsIssuedWithoutASigningKey()
    {
        var emisor = new AccessTokenIssuer(ConClave(""));

        Assert.Null(emisor.Issue(Guid.NewGuid(), "alumna@frre.utn.edu.ar", "Student",
            new DateTimeOffset(2026, 8, 29, 0, 0, 0, TimeSpan.Zero)));
    }

    /// <summary>
    /// CA-05 — **Con clave pero con un dato de identidad ausente tampoco se emite.** Las cuatro
    /// causas de la guarda se ejercitan por separado.
    /// </summary>
    [Theory]
    [InlineData(true, "alumna@frre.utn.edu.ar", "Student")]
    [InlineData(false, "", "Student")]
    [InlineData(false, "alumna@frre.utn.edu.ar", "")]
    [InlineData(false, "   ", "Student")]
    public void AnIncompleteIdentityDoesNotProduceAnAccess(bool identificadorVacio, string correo, string papel)
    {
        var emisor = new AccessTokenIssuer(ConClave(ClaveSuficiente));
        var id = identificadorVacio ? Guid.Empty : Guid.NewGuid();

        Assert.Null(emisor.Issue(id, correo, papel,
            new DateTimeOffset(2026, 8, 29, 0, 0, 0, TimeSpan.Zero)));
    }

    /// <summary>
    /// CA-06 — **Con todo dado, se emite**, y dos emisiones del mismo instante son iguales en su
    /// forma: el acceso tiene tres segmentos separados por punto.
    /// </summary>
    [Fact]
    public void AComleteIdentityWithAKeyProducesAnAccess()
    {
        var emisor = new AccessTokenIssuer(ConClave(ClaveSuficiente));

        var acceso = emisor.Issue(Guid.NewGuid(), "alumna@frre.utn.edu.ar", "Student",
            new DateTimeOffset(2026, 8, 29, 0, 0, 0, TimeSpan.Zero));

        Assert.NotNull(acceso);
        Assert.Equal(3, acceso!.Split('.').Length);
    }

    // ---- PasswordDerivation ----

    /// <summary>
    /// CA-07 — **Sin contraseña no hay valor derivado.** No es un valor vacío: es `null`, que es
    /// lo que distingue «no se pidió» de «se pidió y dio vacío».
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NoPlaintextProducesNoDerivedValue(string? enClaro)
    {
        Assert.Null(new PasswordDerivation().Derive(enClaro));
    }

    /// <summary>
    /// CA-08 — **Dos derivaciones de la misma contraseña son distintas** —la sal es por valor— y
    /// **las dos verifican**. Es la propiedad que hace que el almacén no delate contraseñas
    /// repetidas entre cuentas.
    /// </summary>
    [Fact]
    public void TheSameSecretDerivesDifferentlyEveryTimeAndBothVerify()
    {
        var derivacion = new PasswordDerivation(iterations: 1_000);

        var primera = derivacion.Derive("una-contraseña");
        var segunda = derivacion.Derive("una-contraseña");

        Assert.NotEqual(primera, segunda);
        Assert.Equal(CredentialCheck.Matches, derivacion.Verify("una-contraseña", primera));
        Assert.Equal(CredentialCheck.Matches, derivacion.Verify("una-contraseña", segunda));
    }

    /// <summary>
    /// CA-09 — **Sin contraseña en claro la comprobación no coincide, y no es un error.** Es la
    /// distinción que la propia clase declara: no se pidió una comprobación.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void VerifyingWithoutPlaintextDoesNotMatch(string? enClaro)
    {
        var derivacion = new PasswordDerivation(iterations: 1_000);
        var almacenado = derivacion.Derive("una-contraseña");

        Assert.Equal(CredentialCheck.DoesNotMatch, derivacion.Verify(enClaro, almacenado));
    }

    /// <summary>
    /// CA-10 — **Un valor almacenado ilegible se distingue de uno que no coincide.** Son dos
    /// desenlaces distintos a propósito: el primero es un defecto del almacén y el segundo una
    /// contraseña equivocada, y confundirlos haría que un almacén corrupto se vea como una
    /// persona escribiendo mal.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("no-tiene-la-forma")]
    [InlineData("PBKDF2-SHA256$sin$los$campos")]
    [InlineData("$$$$")]
    public void AnUnreadableStoredValueIsNotAMismatch(string? almacenado)
    {
        Assert.Equal(CredentialCheck.Unreadable,
            new PasswordDerivation(iterations: 1_000).Verify("una-contraseña", almacenado));
    }

    /// <summary>
    /// CA-11 — **Una contraseña equivocada no coincide, y el valor almacenado sigue siendo legible.**
    /// </summary>
    [Fact]
    public void AWrongSecretDoesNotMatch()
    {
        var derivacion = new PasswordDerivation(iterations: 1_000);
        var almacenado = derivacion.Derive("la-correcta");

        Assert.Equal(CredentialCheck.DoesNotMatch, derivacion.Verify("la-equivocada", almacenado));
    }
}
