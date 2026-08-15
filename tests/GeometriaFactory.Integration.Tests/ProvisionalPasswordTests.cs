using GeometriaFactory.Infrastructure.Security;
using Xunit;

namespace GeometriaFactory.Integration.Tests;

/// <summary>
/// `Infrastructure CU-07` y `ADR-05` — la producción de la contraseña provisoria, medida sobre el
/// mecanismo y sin almacén.
/// </summary>
/// <remarks>
/// ES DONDE `RN-14` SE VERIFICA, y es el único lugar donde se puede: las tres capas de arriba
/// reciben el valor **ya producido** y no tienen contra qué comprobar sus dos propiedades. Acá el
/// valor nace, de modo que acá se mide que **no sea adivinable** y que **no se repita**.
/// </remarks>
public sealed class ProvisionalPasswordTests
{
    private static readonly ProvisionalPasswordFactory Provisionals = new();

    /// <summary>
    /// `ADR-05` §8 — **cero parámetros**, medido sobre la superficie. Es la forma estructural de
    /// RN-14 y de RN-15 a la vez: si recibiera uno, alguien terminaría derivando el valor de él,
    /// y si recibiera el estado de la cuenta, resetear podría exigir que estuviera habilitada.
    /// </summary>
    [Fact]
    public void TheProductionOperationDeclaresNoParameters()
    {
        var produce = typeof(ProvisionalPasswordFactory).GetMethod(nameof(ProvisionalPasswordFactory.Produce));

        Assert.NotNull(produce);
        Assert.Empty(produce.GetParameters());
    }

    /// <summary>`ADR-05` §8 — longitud exactamente 12, medida sobre veinte producciones.</summary>
    [Fact]
    public void EveryProvisionalIsTwelveCharactersLong()
    {
        var produced = Enumerable.Range(0, 20).Select(_ => Provisionals.Produce()).ToArray();

        Assert.All(produced, value =>
        {
            Assert.NotNull(value);
            Assert.Equal(ProvisionalPasswordFactory.Length, value!.Length);
        });
    }

    /// <summary>
    /// `ADR-05` §8 — **cero caracteres ambiguos en el alfabeto**, y cero signos de puntuación. El
    /// canal declarado es la voz: el docente se la comunica al alumno en persona, y un carácter
    /// que se dicta mal termina en un ingreso fallido que parece un problema del sistema.
    /// </summary>
    [Fact]
    public void TheAlphabetHasNoAmbiguousCharactersAndNoPunctuation()
    {
        var alphabet = ProvisionalPasswordFactory.Alphabet;

        Assert.DoesNotContain('0', alphabet);
        Assert.DoesNotContain('O', alphabet);
        Assert.DoesNotContain('1', alphabet);
        Assert.DoesNotContain('l', alphabet);
        Assert.DoesNotContain('I', alphabet);
        Assert.All(alphabet, character => Assert.True(char.IsAsciiLetterOrDigit(character)));

        // Y ningún carácter repetido, que sesgaría la distribución sin que se note.
        Assert.Equal(alphabet.Length, alphabet.Distinct().Count());

        // Todo valor producido sale de ese alfabeto y de ningún otro lado.
        var value = Provisionals.Produce();
        Assert.NotNull(value);
        Assert.All(value!, character => Assert.Contains(character, alphabet));
    }

    /// <summary>
    /// `Infrastructure CU-07` CA-02 — **mil provisorias y ninguna repetida**. Es la propiedad de
    /// RN-14 expresada como prueba, sostenida por la impredecibilidad y no por un registro de
    /// valores anteriores, que exigiría conservar contraseñas en claro (`CU-07` §10).
    /// </summary>
    [Fact]
    public void AThousandProvisionalsHaveNoRepetition()
    {
        var produced = Enumerable.Range(0, 1000).Select(_ => Provisionals.Produce()!).ToArray();

        Assert.Equal(1000, produced.Distinct(StringComparer.Ordinal).Count());
    }

    /// <summary>
    /// `Infrastructure CU-07` CA-03 y CA-04 — **el valor no se deriva de ningún dato de la cuenta
    /// ni del reloj**, y la forma de garantizarlo es estructural: la invocación no recibe ninguno
    /// de esos datos. Se mide además que dos producciones del mismo instante observable difieren.
    /// </summary>
    [Fact]
    public void NoProvisionalDerivesFromAccountDataNorFromTheClock()
    {
        // LOS FRAGMENTOS SON DE CUATRO CARACTERES O MÁS, y es a propósito: sobre un alfabeto de
        // 56 caracteres, tres caracteres seguidos aparecen por azar con probabilidad suficiente
        // como para hacer intermitente la prueba, y una prueba intermitente no verifica nada. Lo
        // que garantiza la propiedad no es este barrido sino que **la invocación no recibe
        // ninguno de estos datos**; el barrido es la comprobación de segunda mano.
        string[] accountData =
        [
            "alumna", "frre", "Diaz", "utn.edu.ar", "alumna@frre.utn.edu.ar",
            DateTimeOffset.UtcNow.ToString("O"), DateTimeOffset.UtcNow.ToString("yyyyMMdd"),
        ];

        var produced = Enumerable.Range(0, 200).Select(_ => Provisionals.Produce()!).ToArray();

        foreach (var value in produced)
        {
            foreach (var datum in accountData)
            {
                Assert.DoesNotContain(datum, value, StringComparison.OrdinalIgnoreCase);
            }
        }

        // Dos producciones en el mismo instante observable son distintas: el momento no interviene.
        Assert.NotEqual(Provisionals.Produce(), Provisionals.Produce());
    }
}
