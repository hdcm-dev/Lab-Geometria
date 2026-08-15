namespace GeometriaFactory.Web.Services;

/// <summary>
/// Los valores fijos de la MARCA DE SESIÓN del navegador (`Web ADR-03` §2; intake §17.6, `RT`
/// §9.2).
/// </summary>
/// <remarks>
/// QUÉ ES LA MARCA Y QUÉ NO ES. `ADR-03` §2 dice que el navegador conserva «una marca de sesión
/// que **no la transporta** y que no es legible por guion». Esto es esa marca: un IDENTIFICADOR
/// OPACO de sesión, más la identidad y el papel de quien entró. **El testigo firmado del servicio
/// de datos no está acá adentro**, ni en claro ni cifrado: vive en
/// <see cref="SessionTokenStore"/>, del lado del servidor, indexado por ese identificador.
///
/// LOS TRES ATRIBUTOS SON LA MITAD DE LA DECISIÓN. `HttpOnly` es lo que hace que la marca no sea
/// legible por guion —que es la frase textual de la ADR—; `Secure` es lo que impide que viaje en
/// claro; `SameSite=Strict` es lo que impide que la mande un sitio ajeno. Los tres los declara el
/// intake §17.6 y no son elección de esta implementación.
///
/// EL NOMBRE NO NOMBRA LA TECNOLOGÍA QUE LA EMITE, y es decisión declarada de esta etapa: el
/// nombre por defecto del marco anuncia con qué está construido el producto, que es información
/// que no le sirve a nadie salvo a quien busca por dónde entrar.
/// </remarks>
public static class SessionCookieDefaults
{
    /// <summary>Nombre del esquema de autenticación de la pieza pública.</summary>
    public const string Scheme = "gf-session";

    /// <summary>Nombre de la marca de sesión tal como el navegador la guarda.</summary>
    public const string CookieName = "gf.session";
}
