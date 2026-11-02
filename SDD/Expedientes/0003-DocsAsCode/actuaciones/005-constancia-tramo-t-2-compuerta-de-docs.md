# Constancia — tramo T-2: compuerta de docs

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-2** del plan del folio 003: la copia con huella de `docs-guard` en
`.docs-guard/` (`dg.py` y sus tres esquemas, literales, con el commit de origen y su SHA-256 en `source.lock`), el
registro de prefijos `FIC`, `AUD`, `RD`, `PU` y `DOC` (A-02), las excepciones vacías (A-08), el workflow propio
`.github/workflows/docs.yml` (A-07), su guion `scripts/docs-local.sh` y la carpeta `docs-as-code/controles/` con las
extensiones `delivery-publico` y `enlaces-internos`, la autoprueba del par rojo/verde y su README (A-13,
`FIC-GEO-006`). Todo arranca en avisa (D-10).

**Endurecimiento (K-11).** `docs.yml` corre en `pull_request` y en push a `main`, sin `paths`, con
`permissions: contents: read`, sin secretos, sin `pull_request_target`, con la acción de checkout fijada a una versión
exacta y sin compilar la Api (K-06).

**Apartamiento parcial avisado (K-13).** Además de las extensiones propias, `enlaces-internos` reemplaza a lychee, que
es lo que usa `docs-guard` para ese control: la compuerta corre sólo con el `python3` del runner y no baja herramientas.

**Verificación.** Corrida local sobre `origin/main` b55710b más este tramo: huella y autoprueba en verde; los cinco
controles de `docs-as-code/**` con 0 hallazgos (un aviso `traces-vacio` en la ficha); par rojo/verde plantado en
`docs-as-code/` con `DOCS_MODO_DOCS_AS_CODE=bloquea` (un `doc_id` duplicado, un enlace roto y un
`delivery: [restringido]` dan rojo; retirados, verde). La línea de base de lo heredado va en la descripción del PR.

**Efecto sobre el producto.** Ninguno funcional: ninguno de estos paths entra en una imagen y ningún workflow despliega
al fusionar (`deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`).

Sigue: tramo del punto de uso del plan del folio 003 · mantenimiento de docs-as-code (agente)
