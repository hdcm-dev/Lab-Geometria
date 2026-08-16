using Microsoft.AspNetCore.Antiforgery;

namespace GeometriaFactory.Web.Services;

/// <summary>
/// Un envío que el marco no pudo verificar **no le muestra a la persona el error del marco**.
/// </summary>
/// <remarks>
/// QUÉ PASABA, Y CÓMO SE ENCONTRÓ. El Product Owner dejó abierta la pantalla de ingreso, la pieza
/// pública se reinició, y al enviar recibió esto, textual: «A valid antiforgery token was not
/// provided with the request. Add an antiforgery token, or disable antiforgery validation for this
/// endpoint.» En inglés, sin marca del producto, sin explicación y sin salida.
///
/// POR QUÉ OCURRE Y NO ES UN DEFECTO DE LA PANTALLA. El testigo que acompaña a cada formulario va
/// cifrado con las claves de protección de datos del proceso. **Esas claves no se conservan entre
/// arranques**, de modo que todo formulario servido antes del reinicio queda con un testigo que ya
/// nadie puede verificar. Le pasa a cualquiera que tenga la página abierta cuando el front se
/// despliega de nuevo, y en el hosting de este producto eso ocurre solo.
///
/// QUÉ HACE ESTE INTERMEDIARIO. Traduce ese fallo a lo que el producto ya sabe decir: **la página
/// quedó vieja, y se vuelve a intentar**. Reenvía a la misma dirección por `GET`, que es lo que
/// hace que el formulario se sirva de nuevo **con un testigo válido**, y deja el aviso para que la
/// superficie lo muestre.
///
/// NO REINTENTA EL ENVÍO POR SU CUENTA, y es deliberado: no sabe qué acción era. Reintentar un alta
/// o un desenlace sin que la persona lo pida sería decidir por ella sobre algo que escribe una vez.
/// Lo que ofrece es la pantalla en condiciones de volver a enviar, con lo que la persona ve.
///
/// Y NO SE APAGA LA VERIFICACIÓN. Que el mensaje sea malo no la vuelve prescindible: sin ella,
/// cualquier sitio podría enviar formularios en nombre de quien tenga sesión abierta.
/// </remarks>
public sealed class StaleFormMiddleware
{
    /// <summary>La marca en la dirección con la que la superficie se entera.</summary>
    public const string StaleParameter = "pagina-vieja";

    private readonly RequestDelegate _next;
    private readonly ILogger<StaleFormMiddleware> _log;

    public StaleFormMiddleware(RequestDelegate next, ILogger<StaleFormMiddleware> log)
    {
        ArgumentNullException.ThrowIfNull(next);
        ArgumentNullException.ThrowIfNull(log);

        _next = next;
        _log = log;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        // NO SE ATRAPA UNA EXCEPCIÓN, Y ESO SE DESCUBRIÓ PROBÁNDOLO. La verificación del marco **no
        // lanza**: corta la petición y escribe un `400` con su texto en inglés. De modo que lo que
        // hay que interceptar es la RESPUESTA, no un error.
        //
        // El cuerpo se escribe en un buffer propio para poder descartarlo: una vez que la respuesta
        // empezó a salir, ya no hay dónde poner el aviso.
        var original = context.Response.Body;
        using var buffer = new MemoryStream();
        context.Response.Body = buffer;

        try
        {
            await _next(context).ConfigureAwait(false);

            var invalid = context.Features.Get<IAntiforgeryValidationFeature>() is { IsValid: false };

            if (invalid && !context.Response.HasStarted)
            {
                // EL REGISTRO ANOTA LA DIRECCIÓN Y NADA MÁS: lo que la persona estaba enviando es suyo.
                _log.LogInformation(
                    "Envío no verificado en {Path}: la página se sirvió antes de un reinicio.",
                    context.Request.Path);

                context.Response.Body = original;
                context.Response.Clear();
                context.Response.Redirect($"{context.Request.Path}?{StaleParameter}=1");
                return;
            }

            buffer.Position = 0;
            context.Response.Body = original;
            await buffer.CopyToAsync(original).ConfigureAwait(false);
        }
        finally
        {
            context.Response.Body = original;
        }
    }
}
