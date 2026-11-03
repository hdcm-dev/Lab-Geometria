# Constancia — tramo T-6: oasdiff, Spectral y recuento OpenAPI

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-6** (PR 6, ola 2) del plan del folio 003: los controles OpenAPI sobre la
instantánea `docs-as-code/api/openapi-v1.json`, todos en **avisa** y en el workflow de docs, no en `ci.yml`.

- `docs-as-code/controles/recuento_openapi.py`: extensión `recuento-openapi`. Compara la instantánea con
  `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`: la cifra del título de
  la §3 (línea 75), cada ruta de la tabla de §3.1 (líneas 112-122) en las dos direcciones y los códigos que declara
  la tabla de §3 (líneas 79-98). Corre en `scripts/docs-local.sh`, sin red, con su par rojo/verde en `autoprueba.sh`.
- `docs-as-code/controles/openapi.sh` y `docs-as-code/controles/spectral.yaml`: oasdiff `breaking` de Tufin
  (`tufin/oasdiff:v1.11.4`) de la instantánea de la base contra la del árbol, y Spectral (`stoplight/spectral:6.15.0`)
  con tres reglas propias sin `extends` (`operacion-con-id`, `operacion-con-resumen`,
  `operacion-con-error-documentado`). El guion demuestra su par rojo/verde antes de mirar la instantánea.
- Job `openapi` en `.github/workflows/docs.yml`: `contents: read` heredado, sin secretos, sin `paths`, acciones con
  versión fija. En un PR compara contra la rama base; en push, contra el commit anterior.

**Línea de base (sobre `main` en ab1d721).**

| Control | Resultado |
| --- | --- |
| `recuento-openapi` | 1 error y 17 avisos. Error `recuento-cifra`: el título de la §3 (`Contratos-REST.md:75`) dice «diecisiete» y la instantánea trae 18 operaciones; el propio contrato ya lo reconoce en prosa (`:102`, `:124`). Las 18 rutas de §3.1 coinciden con la instantánea en las dos direcciones. Avisos `recuento-codigos`: 17 de los 18 puntos declaran códigos (201, 204, 4xx, 503) que la instantánea no documenta (todas sus operaciones documentan sólo `200`) |
| oasdiff | sin cambios que rompan frente a `main`. `POST /v1/interpretaciones/pistas` ya está en la línea de base: contra una base sin ella, oasdiff la marca `endpoint-added` (informativo) |
| Spectral | 36 hallazgos: `operacion-con-resumen` y `operacion-con-error-documentado` en las 18 operaciones; `operacion-con-id` en orden |

**Elevación al orquestador SDD (sin edición).** La cifra del título de la §3 de `Contratos-REST.md` (línea 75)
quedó en diecisiete después del alta de `IA-V1`. `SDD/Docs/**` es generado: **se eleva, no se corrige**
(`AGENTS.md:61`). Los códigos sin documentar y los resúmenes faltantes son lo que resuelve el tramo de
`WithSummary`/`ProducesProblem` (PR 12, decisión H-09), que usará este recuento como verificación.

**Qué no se tocó.** `src/`, `ci.yml`, la capa de IA del expediente 0002 (incluida la ruta de pistas), `SDD/Docs/**`
ni la instantánea, que sólo regenera el job `instantanea-openapi`.

**Verificación.** `scripts/docs-local.sh` en verde (autoprueba con el par nuevo del recuento) y
`docs-as-code/controles/openapi.sh` en verde con su autoprueba (seis casos), corridos en local con Docker.

**Efecto sobre el producto.** Sin cambio funcional: nada de esto entra en una imagen y ningún workflow despliega al
fusionar (`deploy-front-ftp.yml:39` es sólo `workflow_dispatch`).

Sigue: tramo T-7 (`docs-programado.yml`, enlaces externos) del plan del folio 003 · mantenimiento de docs-as-code (agente)
