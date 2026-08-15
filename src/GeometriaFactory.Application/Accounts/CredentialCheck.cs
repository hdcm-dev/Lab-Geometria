namespace GeometriaFactory.Application.Accounts;

/// <summary>
/// Resultado de comprobar una contraseña presentada contra la credencial derivada guardada.
/// </summary>
/// <remarks>
/// ESTA CAPA NO COMPARA CREDENCIALES, y este tipo es exactamente la forma de esa frontera: el
/// caso de uso recibe del consumidor una función de comprobación y la invoca sobre el valor
/// derivado que él recuperó, de modo que **el valor derivado no sale de esta capa** y **la
/// comparación no entra**. El mecanismo vive en `GeometriaFactory-Infrastructure` (su CU-06), y
/// los tres valores son los tres desenlaces que ese contrato declara.
///
/// PROPUESTA DE LA ETAPA `c` en cuanto a la forma —ninguna fuente declara cómo se le pasa la
/// comprobación al caso de uso—. Lo que sí está declarado, y es lo que esta forma satisface, es
/// que la capa de aplicación **exige que la verificación se declare al invocar** (`Domain CU-03`
/// §3) y que **no maneja secretos** (intake §17.2.P.5).
/// </remarks>
public enum CredentialCheck
{
    /// <summary>La contraseña presentada corresponde a la credencial derivada guardada.</summary>
    Matches = 1,

    /// <summary>No corresponde. Es el caso normal de una contraseña equivocada, no un defecto.</summary>
    DoesNotMatch = 2,

    /// <summary>
    /// El valor derivado guardado no permite comprobar. Es un defecto del almacén y NO se
    /// colapsa con «no coincide»: hacerlo dejaría la cuenta inaccesible sin diagnóstico
    /// (`Infrastructure ADR-04` §2 punto 1).
    /// </summary>
    Unreadable = 3,
}
