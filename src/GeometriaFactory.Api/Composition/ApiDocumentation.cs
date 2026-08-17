namespace GeometriaFactory.Api.Composition;

using Scalar.AspNetCore;

/// <summary>
/// Documentación navegable de la superficie HTTP: el documento OpenAPI y el explorador Scalar.
/// </summary>
/// <remarks>
/// ESTO REVIERTE UNA RENUNCIA DECLARADA, y por eso se explica acá. El intake decía «se renuncia a
/// un contrato descrito en OpenAPI» y `09-Devops/README.md` marcaba su guía como omitida. La
/// decisión la retoma el propietario del producto en la etapa `g`: el contrato pasa a estar
/// descrito y navegable. Lo que motivó la renuncia —que nadie escriba a mano un documento que se
/// desactualiza— **no se pierde**: el documento se GENERA desde los puntos ya declarados, así que
/// no hay una segunda fuente que pueda decir otra cosa que el código.
///
/// EL EXPLORADOR NO SE PUBLICA SOLO, y es lo único que este archivo decide. En desarrollo está
/// siempre. Fuera de desarrollo hace falta decir `Documentacion__Publicada=true`, porque este
/// servicio se expone a Internet y un explorador de la API enumera **todos** los puntos, sus
/// formas y sus verbos ante cualquiera que lo abra. Publicarlo puede ser correcto —es una API de
/// laboratorio— pero es una decisión de quien despliega, no un efecto secundario de agregar un
/// paquete.
///
/// NO CAMBIA NINGÚN PUNTO. `Definicion-Superficie-HTTP.md` sigue siendo la definición: acá no se
/// declara ni se altera ninguna ruta del producto, sólo se describen las que ya existen.
/// </remarks>
public static class ApiDocumentation
{
    /// <summary>Ruta del documento generado. La consume el explorador y cualquier herramienta.</summary>
    public const string DocumentRoute = "/openapi/v1.json";

    /// <summary>Ruta del explorador navegable.</summary>
    public const string ExplorerRoute = "/documentacion";

    /// <summary>Llave de configuración que habilita el explorador fuera de desarrollo.</summary>
    public const string PublishedSetting = "Documentacion:Publicada";

    /// <summary>Registra el generador del documento OpenAPI.</summary>
    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOpenApi();

        return services;
    }

    /// <summary>
    /// Expone el documento y el explorador **si corresponde**, y devuelve si los expuso.
    /// </summary>
    /// <remarks>
    /// VA DESPUÉS DE LA AUTORIZACIÓN Y NO EXIGE ACCESO: describir la forma de la superficie no
    /// revela ningún dato de ninguna cuenta ni de ningún trabajo. Lo que decide si se ve o no es
    /// la llave de configuración, que es una decisión de despliegue y no de sesión.
    /// </remarks>
    public static bool MapApiDocumentation(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var published = app.Environment.IsDevelopment()
            || app.Configuration.GetValue<bool>(PublishedSetting);

        if (!published)
        {
            return false;
        }

        app.MapOpenApi(DocumentRoute);

        app.MapScalarApiReference(ExplorerRoute, options =>
        {
            options.WithTitle("Fábrica de Geometría · superficie HTTP")
                   .AddDocument("v1", "v1", DocumentRoute);
        });

        return true;
    }
}
