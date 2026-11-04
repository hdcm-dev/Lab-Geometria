# Constancia — PR-P5 del portal: capa para agentes (`agentes.py`) y control `portal-salida`

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §3, §4, §5, §7 y plan de PRs, fila PR-P5) pide el Markdown crudo de
cada página, `llms.txt`, `llms-full.txt` con tope de 400 KB y `/.well-known/api-catalog` (RFC 9727), sin
`mcp/server-card.json` ni `agent-card.json`, y el control `portal_salida.py` con pares rojo/verde por caso. Sigue al
build del folio 021.

**Qué se hizo.**

- `docs-as-code/portal/agentes.py` (`python3 -I`, sin dependencias, única fuente el manifiesto `publicar.yml`). Corre
  después de `mkdocs build`, dentro del mismo contenedor, y escribe en el sitio:
  - el `.md` crudo de cada pieza: `ruta/x.md` (la página es `ruta/x/`) y, para un `README.md`, `index.md` en su
    carpeta. El frontmatter queda reducido a `doc_id`, `title` y `audience` (folio 018 §3). Cada enlace relativo pasa a
    URL absoluta: a una pieza publicada, su `.md` del portal; a cualquier otro archivo, su vista en el repositorio
    público, que ya es público. Los bloques de código no se tocan;
  - `llms.txt` con H1, resumen en `>` y una sección con el `.md` de cada pieza con `llms: true` (llmstxt.org);
  - `llms-full.txt`, la concatenación de esas piezas; si supera 400 KB sale con 1 y no lo escribe;
  - `.well-known/api-catalog`: linkset con `service-desc` a `api/openapi-v1.json` y `service-doc` a `api/`, ambos del
    portal y ninguno de una instancia viva. El JSON y la referencia los sirve PR-P6 y el content-type
    `application/linkset+json` lo pone el servidor de PR-P7; hasta entonces el linkset apunta a rutas que ese PR llena.
  - `mcp/` y `agent-card.json` no se escriben (folio 018 §5).
- `docs-as-code/controles/portal_salida.py`: hallazgos `sin-md-hermano`, `enlace-no-publicado` (URL del portal que no
  existe en el sitio, o enlace relativo sin reescribir), `llms-full-tope`, `linkset-invalido` (estructura de RFC 9727,
  relaciones obligatorias y URLs sólo del portal), `openapi-distinto` (sólo si el sitio sirve `api/openapi-v1.json`) y
  `anuncio-falso` (`mcp/` o `agent-card.json` en cualquier lugar del sitio).
- `docs-as-code/portal/pruebas/*.golden`: salida esperada de `agentes.py` sobre un repo de juguete que arma la
  autoprueba. Llevan sufijo `.golden` para que la compuerta no los lea como piezas del corpus.
- `autoprueba.sh`: los cinco dorados; verde de `portal-salida` y de `portal-negativo` sobre el sitio completado; rojo
  por página sin `.md`, `.well-known/mcp/server-card.json`, `.well-known/agent-card.json`, linkset que apunta fuera del
  portal, linkset sin contextos, `llms-full.txt` sobre el tope, enlace relativo, enlace a una pieza no publicada y
  openapi distinto (con su verde idéntico); error de uso; y `agentes.py` en rojo con una pieza que pasa el tope.
- `portal_negativo.py`: `.well-known/api-catalog` entra a la infraestructura permitida.
- `scripts/docs-portal.sh` corre `agentes.py` después del build y, al final, los dos controles; sale con el peor
  código. El job `portal` de `docs.yml` sigue en avisa también para `portal-salida`.

**Verificación local.** Autoprueba verde (incluidos los pares nuevos). Build del portal en el contenedor fijo con
`PORTAL_REPRODUCIBLE=si`: dos corridas sin diferencias, `portal-negativo` y `portal-salida` verdes sobre el sitio real.
`scripts/docs-local.sh` verde.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, el expediente 0002 ni
`SDD/Docs/**`. Nada de la capa de IA.

**Efecto sobre el producto.** Ninguno al fusionar: `deploy-front-ftp.yml` sólo corre con `workflow_dispatch` y ningún
archivo tocado entra al build de producción. El sitio sólo existe como artefacto del workflow; no se publica en ningún
dominio sin el ok del PO (folio 018, excepción).

Sigue: PR-P6, referencia de API desde la instantánea OpenAPI y Redoc vendorizado · mantenimiento de docs-as-code
(agente)
