using GeometriaFactory.Infrastructure.Security;

namespace GeometriaFactory.Samples.Infrastructure.Avanzado;

/// <summary>Acto 2 — `CU-06007`: cien provisorias, y la regla de detenerse dicha con números.</summary>
internal static class ActoProducirProvisoria
{
    private const int Cuantas = 100;

    internal static void Ejecutar(string correoDeLaCuenta, string apellidoDeLaCuenta, Action<string> escribir)
    {
        var fabrica = new ProvisionalPasswordFactory();
        var producidas = Enumerable.Range(0, Cuantas).Select(_ => fabrica.Produce()!).ToList();

        var repetidas = Cuantas - producidas.Distinct(StringComparer.Ordinal).Count();

        // «DERIVADA DE UN DATO DE LA CUENTA» SE MIDE, no se afirma. Una provisoria que arrastrara
        // el correo o el apellido sería adivinable por quien conoce a la persona, que es
        // exactamente contra quien la provisoria protege. Se busca el dato entero y también sus
        // fragmentos, porque «contiene el apellido» no exige que lo contenga completo.
        var partes = Fragmentos(correoDeLaCuenta).Concat(Fragmentos(apellidoDeLaCuenta)).ToList();
        var conRastro = producidas.Count(p =>
            partes.Any(f => p.Contains(f, StringComparison.OrdinalIgnoreCase)));

        escribir($"[2] Provisorias producidas: {producidas.Count} | repetidas: {repetidas} "
            + $"| derivadas de un dato de la cuenta: {(conRastro > 0 ? $"SI ({conRastro})" : "no")}");

        // DIVERGENCIA D-2 CONTRA §6, y ésta es de MEDICIÓN y no de nombre.
        //
        // §6 espera `RANDOMNESS_SOURCE_UNAVAILABLE | valores producidos: 0`. El código EXISTE
        // —es uno de los dos que esta capa declara— pero **el sample no puede provocar la
        // condición**: la fuente es el generador criptográfico del sistema operativo, y hacerlo
        // fallar desde adentro del proceso no está a su alcance. Fabricar la falla con un doble
        // mediría el doble.
        //
        // LO QUE SÍ SE PUEDE MEDIR ES QUE NO HAY SEGUNDO CAMINO, y es la mitad que importa. La
        // regla no es «avisar cuando la fuente falla»: es NO COMPONER LA PROVISORIA POR OTRO
        // MEDIO. Eso se cuenta sobre la fuente del componente, y el umbral es cero.
        var caminos = Inspecciones.CaminosAlternativosDeAleatoriedad();
        escribir($"[2] Produccion sin fuente de aleatoriedad: no provocable desde el sample "
            + $"| caminos alternativos en la fuente del componente: {caminos}");
    }

    /// <summary>El dato entero y cada tramo suyo de cuatro caracteres o más.</summary>
    private static IEnumerable<string> Fragmentos(string dato)
    {
        var limpio = new string(dato.Where(char.IsLetterOrDigit).ToArray());
        yield return limpio;
        for (var largo = 4; largo <= limpio.Length; largo++)
        {
            for (var desde = 0; desde + largo <= limpio.Length; desde++)
            {
                yield return limpio.Substring(desde, largo);
            }
        }
    }
}
