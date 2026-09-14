namespace GeometriaFactory.Api.Composition;

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
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
///
/// LAS DOS RUTAS DE ACÁ QUEDAN FUERA DEL PREFIJO `/v1` DEL CONTRATO (`ADR-00010`, `BT-00032`).
/// Describen la superficie y no son parte de ella: cuando convivan dos `MAJOR`, un solo explorador
/// tiene que poder describir a los dos, y no puede hacerlo desde adentro de uno. El `v1` que
/// llevan el nombre del documento y su ruta es el del **documento** generado, que hoy coincide con
/// el del contrato; `Endpoints.ContractRoutePrefix` dice por qué se enumeran como exentas.
/// </remarks>
public static class ApiDocumentation
{
    /// <summary>Ruta del documento generado. La consume el explorador y cualquier herramienta.</summary>
    public const string DocumentRoute = "/openapi/v1.json";

    /// <summary>Ruta del explorador navegable.</summary>
    public const string ExplorerRoute = "/documentacion";

    /// <summary>Llave de configuración que habilita el explorador fuera de desarrollo.</summary>
    public const string PublishedSetting = "Documentacion:Publicada";

    /// <summary>El nombre del esquema de seguridad que el documento declara.</summary>
    public const string BearerScheme = "Bearer";

    /// <summary>Registra el generador del documento OpenAPI.</summary>
    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // EL DOCUMENTO DICE CÓMO SE AUTENTICA, Y EN QUÉ PUNTOS (mesa `SDD/Docs/Audit/Mesa-2026-09-14.md`,
        // R-08). La superficie es pública para otros clientes (`ADR-00009`), y sin esto un cliente que
        // lee el documento no sabía qué puntos piden un acceso firmado ni cómo se presenta. El
        // requisito se DERIVA de los metadatos de cada punto —`RequireAuthorization` sin
        // `AllowAnonymous`—, de modo que un punto nuevo queda bien descripto sin tocar este archivo.
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                document.Components.SecuritySchemes[BearerScheme] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Acceso firmado que devuelve `POST /v1/auth/token`, presentado como `Authorization: Bearer <acceso>`.",
                };

                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, _) =>
            {
                var metadata = context.Description.ActionDescriptor.EndpointMetadata;
                var requiresAccess = metadata.OfType<IAuthorizeData>().Any()
                    && !metadata.OfType<IAllowAnonymous>().Any();

                if (requiresAccess)
                {
                    operation.Security ??= [];
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference(BearerScheme, context.Document)] = [],
                    });
                }

                return Task.CompletedTask;
            });
        });

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
