namespace GeometriaFactory.Api.Endpoints;

/// <summary>
/// El prefijo de versión bajo el que se publica **todo** punto del contrato REST: `/v{MAJOR}/`.
/// </summary>
/// <remarks>
/// ES EL ÚNICO LUGAR DONDE LA VERSIÓN SE ESCRIBE, y por eso es una constante y no un literal
/// repartido por los cinco contratos de punto. `ADR-00010` decidió que la superficie pública lleva
/// la versión en la ruta, **sólo el `MAJOR`**, y que ese `MAJOR` es el del producto: cuando el
/// producto salga de `1.x`, acá cambia una cifra y `Program.cs` decide cuánto tiempo conviven las
/// dos (`Estrategia-Versionado.md`, `BT-00035`). Ningún otro archivo del servicio sabe qué versión
/// es: los contratos de punto declaran su ruta **relativa** —`/cuentas`, `/trabajos`— y es el grupo
/// de `Program.cs` el que les antepone esto.
///
/// TRES RUTAS QUEDAN AFUERA, Y NO ES OLVIDO. `/salud` (`A-16`) es del **arranque y salud** y no
/// del contrato: lo consumen el `healthcheck` de las dos composiciones —la de verificación de
/// `deploy/compose.yaml` y la de despliegue— y la página de estado del front, y tiene que responder
/// cuando nadie puede autenticarse (`ADR-00007` §2 punto 3). Atarlo al `MAJOR` del contrato haría
/// que estrenar `/v2/` moviera lo que decide si el contenedor está vivo, que es exactamente el
/// acoplamiento que `Definicion-Superficie-HTTP.md` §3 rechazó cuando no le agregó a `A-16` el dato
/// del aprovisionamiento. Y es además el lugar donde se informa la versión del producto: `ADR-00010`
/// §8 lo compara **contra** el prefijo, de modo que no puede vivir adentro de él. `/openapi/v1.json`
/// y `/documentacion` (`ADR-08008`) **describen** la superficie y no son parte de ella: durante la
/// convivencia de dos `MAJOR` el explorador tiene que poder describir a los dos, y no puede hacerlo
/// desde adentro de uno.
///
/// SIN PREFIJO NO HAY CONTRATO. Una petición a `/cuentas` —la ruta que valió hasta el 2026-09-12—
/// responde `404` como cualquier ruta que no existe: **sin redirección** y sin cuerpo del
/// contrato. Redirigir sería mantener viva la forma sin versión bajo otro nombre, que es lo que
/// `ADR-00010` §2.1 punto 5 excluye («una petición sin prefijo de versión a un punto del contrato
/// no es una petición al contrato»). Lo verifica `ContractRoutePrefixTests`.
/// </remarks>
public static class ContractRoutePrefix
{
    /// <summary>
    /// El `MAJOR` del producto, que es el que va en la ruta (`ADR-00010` §2.1 punto 2). Es una sola
    /// cifra a propósito: `MINOR` y `PATCH` no se ven en la superficie.
    /// </summary>
    public const int Major = 1;

    /// <summary>El prefijo tal como se antepone a cada ruta del contrato: `/v1`.</summary>
    public const string Value = "/v1";

    /// <summary>
    /// Las rutas que se publican **fuera** del prefijo, enumeradas para que la prueba de inspección
    /// pueda decir «toda ruta mapeada lleva el prefijo salvo estas tres» y falle si aparece una
    /// cuarta. Los motivos están arriba.
    /// </summary>
    public static readonly IReadOnlyList<string> ExemptRoutes =
    [
        HealthEndpoint.Route,
        Composition.ApiDocumentation.DocumentRoute,
        Composition.ApiDocumentation.ExplorerRoute,
    ];
}
