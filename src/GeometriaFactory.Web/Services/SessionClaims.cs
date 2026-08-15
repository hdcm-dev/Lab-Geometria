namespace GeometriaFactory.Web.Services;

/// <summary>
/// Lo ÚNICO que la marca de sesión lleva adentro: identidad y papel (`Web ADR-03` §2).
/// </summary>
/// <remarks>
/// NO HAY UNA DECLARACIÓN PARA EL TESTIGO, Y LA AUSENCIA ES LA DECISIÓN. Si existiera, alguien
/// podría ponerlo ahí sin darse cuenta de que lo está mandando al navegador. La métrica §8 de
/// `ADR-03` —apariciones del testigo en el navegador, objetivo exactamente 0— se sostiene sobre
/// esta ausencia y sobre <see cref="SessionTokenStore"/>, que es donde el testigo sí está.
/// </remarks>
public static class SessionClaims
{
    /// <summary>Identificador OPACO de la sesión. Es la llave del almacén del servidor.</summary>
    public const string SessionId = "gf:session";

    /// <summary>Identidad de la cuenta que entró.</summary>
    public const string AccountId = "gf:account";

    /// <summary>Correo de la cuenta que entró. Es lo que la barra lateral muestra.</summary>
    public const string Email = "gf:email";

    /// <summary>Papel de la cuenta que entró. Es lo que decide qué destinos se dibujan.</summary>
    public const string Role = "gf:role";
}
