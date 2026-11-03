# Constancia — tramo T-5: instantánea OpenAPI

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-5** (PR 5, ola 2) del plan del folio 003, decisiones K-07 y K-12:

- `docs-as-code/api/openapi-v1.json`: instantánea de línea de base (14 rutas, 18 operaciones).
- `docs-as-code/api/extraer-openapi.sh`: extracción aislada; compila la Api y la levanta como proceso local con almacén
  efímero en un directorio temporal, clave de firma descartable y puerto propio (5199 en `127.0.0.1`), en `Production`
  con `Documentacion__Publicada=true` (`src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:109-110`). Falla si el
  registro de la Api nombra un contenedor `lab-geometria-*`.
- `docs-as-code/api/CHANGELOG-API.md` (`FIC-GEO-007`), en Keep a Changelog 1.1.0.
- Job `instantanea-openapi` en `.github/workflows/docs.yml`, aparte de la compuerta: sólo en push (`if`), único con
  `contents: write` y `pull-requests: write`; si el documento cambió, empuja `automatico/instantanea-openapi` y abre
  o actualiza un PR automático.

**Qué no se tocó.** `src/`, `ci.yml`, la capa de IA del expediente 0002 ni `SDD/Docs/**`. El job de la compuerta
sigue con `contents: read`. La tarea nunca recae sobre un PR de producto ni sobre Guía 16.

**Observación.** Un PR abierto con el token de Actions no dispara workflows; los checks del PR automático se obtienen
con un push o un cierre y reapertura. Si el repositorio no permite que Actions abra PRs, el job avisa y queda verde.

**Verificación.** Extracción corrida en un contenedor del SDK de .NET 10 (30 s, sin Docker adentro);
`scripts/docs-local.sh` en verde.

**Efecto sobre el producto.** Sin cambio funcional: ningún workflow despliega al fusionar (`deploy-front-ftp.yml:39`
es sólo `workflow_dispatch`).

Sigue: tramo T-6 (oasdiff, Spectral y recuento, en avisa) del plan del folio 003 · mantenimiento de docs-as-code (agente)
