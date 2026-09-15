using Microsoft.Extensions.Primitives;

namespace GeometriaFactory.Web.Integration;

/// <summary>
/// Las cabeceras de seguridad que toda respuesta del front lleva, puestas **en el origen** y no en
/// el borde (mesa `SDD/Docs/Audit/Mesa-2026-09-15.md`, decisión D-3 sobre `R-06` del 2026-09-14).
/// </summary>
/// <remarks>
/// POR QUÉ EN EL ORIGEN. El borde (el túnel y su proveedor) es del Product Owner y el agente no lo
/// alcanza; una cabecera puesta acá viaja por cualquier borde y se verifica con la batería. Lo que
/// el borde agregue encima no molesta.
///
/// QUÉ NO LLEVA, Y POR QUÉ. **Ninguna directiva `script-src` ni `style-src`**: el marcado del front
/// lleva guiones en línea del propio marco y del sello del visor, y restringirlos sin `nonce` rompe
/// la pantalla. La `Content-Security-Policy` se limita a lo que no puede romper nada: nadie enmarca
/// el sitio, los formularios sólo se envían acá, y sin `base` ni `object`. Las directivas de guion
/// quedan como deuda con evento en la mesa. **HSTS corto y sin `preload` ni `includeSubDomains`**,
/// por la condición del refutador de la mesa del 2026-09-14, y sólo sobre `https`: en desarrollo y
/// en la batería, que sirven por `http`, no se emite.
/// </remarks>
public static class SecurityHeaders
{
    public const string ContentSecurityPolicy = "frame-ancestors 'none'; base-uri 'self'; form-action 'self'; object-src 'none'";
    public const string StrictTransportSecurity = "max-age=86400";

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app) =>
        app.Use(async (context, next) =>
        {
            var headers = context.Response.Headers;
            headers["X-Content-Type-Options"] = "nosniff";
            headers["X-Frame-Options"] = "DENY";
            headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
            headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
            headers["Content-Security-Policy"] = ContentSecurityPolicy;
            if (context.Request.IsHttps)
            {
                headers["Strict-Transport-Security"] = new StringValues(StrictTransportSecurity);
            }

            await next(context).ConfigureAwait(false);
        });
}
