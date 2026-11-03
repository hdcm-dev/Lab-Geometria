# Constancia — tramo T-12: resumen y respuestas declaradas en la Api

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-12** (PR 12, ola 2) del plan del folio 003, con la decisión **H-09** en su
default (no tocar la Api en la ola 1; tocarla en este tramo):

- Los 18 puntos de la Api llevan `WithSummary` y declaran sus respuestas: la exitosa con su tipo del contrato
  (`Produces<T>`, o `204` sin cuerpo) y los errores que el punto puede producir.
- `src/GeometriaFactory.Api/Endpoints/ResponseDocumentation.cs` (nuevo) agrupa dos extensiones:
  `ProducesContractErrors` (errores con el cuerpo `ErrorResponse`) y `ProducesRateLimitRejection` (`429` sin cuerpo,
  que hereda todo el grupo `/v1`).
- El punto de pistas de la capa de IA (`ExplainObservations`, expediente 0002) sólo suma esas declaraciones: no cambia
  su ruta, su autorización ni su límite de tasa.

**Apartamiento declarado.** El plan nombra `ProducesProblem`. No se usa: declara un cuerpo `ProblemDetails` con tipo
`application/problem+json`, y esta superficie no lo emite. Sus errores son el `ErrorResponse` del contrato, escrito
como JSON común (`ContractTranslation.cs:347-354`, `PendingPasswordChangeGuard.cs:115`,
`ContractErrorHandler.cs:79`). Declarar `ProblemDetails` describiría un cuerpo que no existe; se declara el tipo real
con `Produces<ErrorResponse>`.

**Instantánea OpenAPI.** No se regenera en este PR: la regenera el job `instantanea-openapi` de `docs.yml` en el push
a `main` y abre su PR automático, que lleva la entrada de `CHANGELOG-API.md` (decisión K-07). El cambio esperado es
aditivo: aparecen `summary` y respuestas nuevas en las 18 operaciones; las rutas no cambian.

**Qué no se tocó.** Ni la Web, ni `Contracts/`, `tests/`, `deploy/`, `visor/`, `ai-layer/`, la composición de la Api,
su `appsettings.json` o su `.csproj`, los puertos y casos de uso de pistas, el expediente 0002 o `SDD/Docs/**`.

**Verificación.** La que se pudo correr en local; el detalle está en la descripción del PR.

**Efecto sobre el producto.** Cambio funcional en la Api que despliega en el próximo build manual del PO: cambia sólo
el documento OpenAPI publicado; ninguna respuesta cambia. Ninguna fusión dispara un despliegue
(`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: fin del plan de la ola 2 del folio 003 · mantenimiento de docs-as-code (agente)
