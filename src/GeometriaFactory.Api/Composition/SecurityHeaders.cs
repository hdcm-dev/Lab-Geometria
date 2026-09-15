using Microsoft.Extensions.Primitives;

namespace GeometriaFactory.Api.Composition;

/// <summary>
/// Las cabeceras de seguridad que toda respuesta del servicio de datos lleva, puestas **en el origen** y no en
/// el borde (mesa `SDD/Docs/Audit/Mesa-2026-09-15.md`, decisión D-3 sobre `R-06` del 2026-09-14).
/// </summary>
/// <remarks>
/// POR QUÉ EN EL ORIGEN. El borde (el túnel y su proveedor) es del Product Owner y el agente no lo
/// alcanza; una cabecera puesta acá viaja por cualquier borde y se verifica con la batería. Lo que
/// el borde agregue encima no molesta.
///
/// QUÉ NO LLEVA, Y POR QUÉ. La `Content-Security-Policy` se limita a que nadie enmarque el servicio:
/// el explorador de la superficie (`/documentacion`, `ADR-08008`) carga sus guiones de un CDN, y una
/// política más cerrada lo rompería. Es una superficie sin marcado propio fuera de ese explorador. **HSTS corto y sin `preload` ni `includeSubDomains`**,
/// por la condición del refutador de la mesa del 2026-09-14, y sólo sobre `https`: en desarrollo y
/// en la batería, que sirven por `http`, no se emite.
/// </remarks>
public static class SecurityHeaders
{
    public const string ContentSecurityPolicy = "frame-ancestors 'none'";
    public const string StrictTransportSecurity = "max-age=86400";

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app) =>
        app.Use(async (context, next) =>
        {
            var headers = context.Response.Headers;
            headers["X-Content-Type-Options"] = "nosniff";
            headers["X-Frame-Options"] = "DENY";
            headers["Referrer-Policy"] = "no-referrer";
            headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
            headers["Content-Security-Policy"] = ContentSecurityPolicy;
            if (context.Request.IsHttps)
            {
                headers["Strict-Transport-Security"] = new StringValues(StrictTransportSecurity);
            }

            await next(context).ConfigureAwait(false);
        });
}
