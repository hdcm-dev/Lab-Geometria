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
}
