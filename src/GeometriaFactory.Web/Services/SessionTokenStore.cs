using System.Collections.Concurrent;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// EL TESTIGO FIRMADO DEL SERVICIO DE DATOS VIVE ACÁ, Y EN NINGÚN OTRO LADO (`Web ADR-03` §2).
/// </summary>
/// <remarks>
/// POR QUÉ EL ALCANCE ES DE APLICACIÓN Y NO DE CIRCUITO. La etapa `c` custodiaba el testigo en el
/// estado del circuito, y eso hacía que la sesión no sobreviviera ni a una recarga de la página ni
/// a una pestaña nueva: cada circuito arrancaba vacío. Con alcance de aplicación el testigo
/// sobrevive a las dos cosas, y sigue **sin tener por dónde llegar al navegador**, que es lo que
/// `ADR-03` exige. Lo que el navegador conserva es la llave —el identificador opaco de sesión de
/// <see cref="SessionClaims.SessionId"/>—, y una llave no es lo que abre: sin este almacén no vale
/// nada.
///
/// LO QUE SE PIERDE CON EL RECICLADO DEL PROCESO, Y ESTÁ ACEPTADO POR ESCRITO. `ADR-03` §6.1:
/// «se acepta que la sesión se pierda cuando el proceso del hosting recicla». Con la marca de
/// sesión la situación cambia de forma pero no de fondo: la marca sobrevive al reciclado y el
/// almacén no, de modo que queda **marca presente y testigo ausente**. Ese caso NO es una
/// excepción ni una pantalla rota: lo atiende <see cref="UnrestorableSessionMiddleware"/>,
/// borrando la marca y devolviendo a `/ingreso` con el motivo declarado, que es el estado
/// «sesión no restablecible» que la categoría 03 tiene diseñado (`EST-34`).
///
/// SIN PERSISTENCIA, Y ES `Web ADR-02`. Esto es memoria del proceso: no hay archivo, no hay base
/// y no hay estado propio que respaldar.
/// </remarks>
public sealed class SessionTokenStore
{
    private readonly ConcurrentDictionary<string, string> _tokens = new(StringComparer.Ordinal);

    /// <summary>Guarda el testigo de una sesión recién abierta, bajo su identificador opaco.</summary>
    public void Keep(string sessionId, string accessToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sessionId);
        ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

        _tokens[sessionId] = accessToken;
    }

    /// <summary>
    /// Devuelve el testigo de una sesión, o <c>null</c> si no está.
    /// </summary>
    /// <remarks>
    /// QUE DEVUELVA <c>null</c> EN LUGAR DE FALLAR ES LA DECISIÓN. La ausencia no es un error del
    /// programa: es el reciclado del proceso, que `ADR-03` §6.1 aceptó. Quien la recibe la trata
    /// como sesión no restablecible.
    /// </remarks>
    public string? Find(string sessionId) =>
        _tokens.TryGetValue(sessionId, out var token) ? token : null;

    /// <summary>Si el almacén todavía tiene el testigo de esa sesión.</summary>
    public bool Contains(string sessionId) => _tokens.ContainsKey(sessionId);

    /// <summary>Descarta el testigo. Es la mitad de servidor del cierre de sesión.</summary>
    public void Discard(string sessionId) => _tokens.TryRemove(sessionId, out _);

    /// <summary>
    /// Vacía el almacén entero.
    /// </summary>
    /// <remarks>
    /// PROPUESTA DE ESTA ETAPA, Y SE DECLARA PORQUE NINGUNA FUENTE LA PIDE: el reciclado del
    /// proceso hace exactamente esto y no avisa. Que exista como operación es lo que permite
    /// **ejercitar** el caso de `ADR-03` §6.1 en una prueba, en lugar de razonarlo.
    /// </remarks>
    public void Clear() => _tokens.Clear();
}
