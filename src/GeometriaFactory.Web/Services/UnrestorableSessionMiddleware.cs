using Microsoft.AspNetCore.Authentication;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// El estado «sesión no restablecible»: marca de sesión presente y testigo ausente
/// (`Web ADR-03` §6.1; `EST-34` de `Linea-Base-Visual.md` §2).
/// </summary>
/// <remarks>
/// CUÁNDO OCURRE, EXACTAMENTE. El proceso del hosting recicla. La marca de sesión vive en el
/// navegador y sobrevive; <see cref="SessionTokenStore"/> vive en la memoria del proceso y no.
/// La persona vuelve con una marca que ya no abre nada. `ADR-03` §6.1 aceptó este costo por
/// escrito —«se acepta que la sesión se pierda cuando el proceso del hosting recicla»— y la
/// categoría 03 le tiene diseñado su tratamiento: **se vuelve a `Ingreso` con el motivo
/// declarado**, no a una pantalla rota ni a un error arbitrario en una acción cualquiera.
///
/// POR QUÉ ES UN INTERMEDIARIO Y NO UNA COMPROBACIÓN EN CADA SUPERFICIE. Porque tiene que correr
/// **antes** de que se dibuje nada y en **toda** petición, incluida la primera de un circuito
/// nuevo; y porque borrar la marca exige escribir una cabecera, que después de empezar a dibujar
/// ya no se puede. Repartido por las superficies sería once oportunidades de olvidarlo.
///
/// NO ES UN GUARDIÁN DE RUTA Y NO HACE CUMPLIR NADA. Los cuatro guardianes de `ADR-03` §2 acotan
/// lo que se ofrece; esto ni siquiera acota: repara un estado imposible —una llave sin
/// cerradura— y devuelve a la persona a donde puede volver a entrar.
/// </remarks>
public sealed class UnrestorableSessionMiddleware
{
    /// <summary>
    /// A dónde se vuelve, con el motivo declarado. El parámetro es el mismo `EST-34` que la
    /// maqueta aprobada usa en `Ingreso.html`.
    /// </summary>
    private const string SignInWithReason = "/ingreso?estado=sesion-no-restablecible";

    /// <summary>
    /// Lo que no se mira, porque no dibuja superficie: los recursos del marco y la propia
    /// superficie de ingreso, que es el destino.
    /// </summary>
    private static readonly string[] Untouched = ["/_blazor", "/_framework", "/css", "/js", "/ingreso"];

    private readonly RequestDelegate _next;

    public UnrestorableSessionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, SessionTokenStore tokens)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(tokens);

        var sessionId = context.User.FindFirst(SessionClaims.SessionId)?.Value;

        if (sessionId is not null && !tokens.Contains(sessionId) && !IsUntouched(context.Request.Path))
        {
            // La marca que ya no abre nada se borra acá mismo: dejarla puesta repetiría el desvío
            // en cada petición siguiente y la persona no entendería por qué.
            await context.SignOutAsync(SessionCookieDefaults.Scheme).ConfigureAwait(false);
            context.Response.Redirect(SignInWithReason);
            return;
        }

        await _next(context).ConfigureAwait(false);
    }

    private static bool IsUntouched(PathString path) =>
        Untouched.Any(prefix => path.StartsWithSegments(prefix, StringComparison.Ordinal));
}
