namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Los códigos del catálogo de `GeometriaFactory-Infrastructure` que esta capa PROPAGA sin ser
/// suyos.
/// </summary>
/// <remarks>
/// Esta capa no puede referenciar la infraestructura —es la regla de dependencias— y sin embargo
/// tiene que poder nombrar la condición que le llega desde ella a través de la comprobación de
/// credencial. Se declara acá el mínimo indispensable, con su origen anotado, en lugar de
/// colapsarlo contra un código propio que diría otra cosa.
/// Identificador en inglés por `Norma-De-Nomenclatura.md` §6.8.3.
/// </remarks>
public static class InfrastructureConditionCode
{
    /// <summary>
    /// `CREDENCIAL_DERIVADA_ILEGIBLE` — `Infrastructure CU-06`. El valor guardado no permite
    /// comprobar: es un defecto del almacén y NO se responde «no coincide».
    /// </summary>
    public const string UnreadablePasswordHash = "UNREADABLE_PASSWORD_HASH";

    /// <summary>
    /// `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` — `Infrastructure CU-07`. La fuente de material
    /// impredecible no respondió y **no se produjo ninguna provisoria**. Se declara acá porque
    /// esta capa tiene que poder nombrar la condición sin componer el valor por otro medio: un
    /// reseteo que no se completa es recuperable, y una provisoria adivinable no se nota hasta
    /// que alguien la usa (`Infrastructure ADR-05` §6).
    /// </summary>
    public const string RandomnessSourceUnavailable = "RANDOMNESS_SOURCE_UNAVAILABLE";
}
