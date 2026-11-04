# Constancia — PR-P6 del portal: referencia de API desde la instantánea y Redoc vendorizado

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §3, §6, §7 y plan de PRs, fila PR-P6) pide la referencia de API en
`/api/`, con páginas Markdown por grupo de operaciones generadas en build desde `docs-as-code/api/openapi-v1.json`
(cada una con su `.md` crudo), Redoc con versión exacta y vendorizado como asset local, el JSON en
`/api/openapi-v1.json` idéntico byte a byte a la instantánea, el conteo de operaciones renderizadas igual al de
`recuento_openapi.py`, y que `recuento_openapi.py` pase a bloquea antes de publicar. Sigue al folio 022.

**Qué se hizo.**

- `docs-as-code/portal/referencia_api.py` (`python3 -I`, sin dependencias). Corre después de `preparar.py` y antes de
  `mkdocs build`, en el mismo contenedor, y escribe en el contenido `api/index.md`, una página por grupo
  (`api/<grupo>.md`; el grupo es el primer segmento después del prefijo de versión, en el orden de la instantánea:
  `salud`, `auth`, `cuentas`, `aprovisionamiento`, `cuenta`, `trabajos`, `interpretaciones`), la instantánea copiada
  byte a byte y `api/redoc.html`. Cada operación sale como `` ## `VERBO /ruta` `` con su resumen, `operationId`,
  seguridad, parámetros, cuerpo y respuestas tal como los trae la instantánea; donde la instantánea no declara
  seguridad, la página lo dice así, sin suponer nada.
- Redoc 2.5.4 vendorizado en `docs-as-code/portal/vendor/redoc/`: `redoc.standalone.js` y sus licencias (MIT), sin
  cambios, con el `integrity` del paquete npm y la huella sha256 de cada archivo en `VENDOR`. `referencia_api.py` se
  niega a copiarlo si la huella no coincide; el sitio lo sirve en `assets/vendor/redoc/`. `api/redoc.html` lleva una
  CSP que corta el único pedido externo del bundle (el logo de su pie, en el CDN del fabricante): la vista no pide
  nada a terceros.
- `mkdocs.yml`: sección «Referencia de API» con las ocho páginas; con `--strict`, si la instantánea gana o pierde un
  grupo, el build corta hasta que la lista lo refleje. `scripts/docs-portal.sh` corre `referencia_api.py`.
- `agentes.py`: escribe el `.md` crudo de esas páginas, con los enlaces como URLs absolutas del portal, y las suma en
  una sección «Referencia de API» de `llms.txt` y al final de `llms-full.txt`.
- `portal_negativo.py`: si el sitio sirve `api/openapi-v1.json`, las páginas y archivos de la referencia entran a lo
  permitido (derivados del JSON servido); el bundle vendorizado no se barre por patrones sólo si su huella es la de
  `VENDOR` (trae una IPv4 de muestra para su generador de ejemplos); con otra huella se barre como cualquier archivo.
- `portal_salida.py`: `api-recuento` (con la instantánea en el repo, el sitio sirve `api/openapi-v1.json` y
  `api/index.md`, y las operaciones renderizadas en las páginas de grupo son exactamente las que cuenta
  `recuento_openapi.py`) y `redoc-local` (`api/redoc.html` existe y sólo carga scripts del propio sitio).
- `recuento_openapi.py` pasa a bloquea en `scripts/docs-local.sh` (folio 018 §7). Hoy da un error en el contrato del
  SDD: el título de la §3 de `Contratos-REST.md` dice «diecisiete» y la instantánea trae 18 operaciones (las 18 rutas
  de §3.1 coinciden en las dos direcciones). Ese error ya se elevó (folios 009 y 016) y `SDD/Docs/**` no se corrige
  desde acá. Para que el paso a bloquea no deje la compuerta en rojo por algo que este repo no puede corregir, el
  control lee `docs-as-code/controles/recuento-elevados.txt`, una línea por hallazgo con
  `regla | mensaje exacto | folio | vence AAAA-MM-DD` (la misma forma que las excepciones de `enlaces-externos`).
  Vigente e idéntico, el error cuenta como aviso `elevado`; con otro mensaje (otra cifra) o vencido, vuelve a rojo; sin
  uso, avisa para borrarlo. Única línea: la cifra «diecisiete», vence 2027-02-03.
- `autoprueba.sh`: dorado `pruebas/api-cosas.md.golden` sobre una instantánea de juguete; verdes de `portal-salida` y
  `portal-negativo` con la referencia; rojos por operación sin renderizar, JSON ausente, JSON distinto, Redoc desde un
  CDN y bundle con otra huella; y, para el recuento, elevado vigente (verde), vencido y con otro mensaje (rojo) y línea
  mal formada (error de uso).

**Verificación local.** Autoprueba verde (79 casos). Build del portal en el contenedor fijo con
`PORTAL_REPRODUCIBLE=si`: dos corridas sin diferencias; `portal-negativo` y `portal-salida` verdes sobre el sitio real
(8 páginas de la referencia, 18 operaciones, igual que `recuento-openapi`); `api/openapi-v1.json` idéntico a la
instantánea (`cmp`). La vista Redoc, servida localmente, renderiza las 18 operaciones y el navegador no completa
ningún pedido fuera del sitio (la CSP bloquea el logo remoto). `scripts/docs-local.sh` verde con el recuento en
bloquea.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, el expediente 0002 ni
`SDD/Docs/**`. Nada de la capa de IA.

**Efecto sobre el producto.** Ninguno al fusionar: `deploy-front-ftp.yml` sólo corre con `workflow_dispatch` y ningún
archivo tocado entra al build de producción. El sitio sólo existe como artefacto del workflow; no se publica en ningún
dominio sin el ok del PO (folio 018, excepción).

**A elevar.** La cifra «diecisiete» de `Contratos-REST.md:75` sigue pendiente en el SDD; la línea de
`recuento-elevados.txt` vence el 2027-02-03 y entonces la compuerta vuelve a rojo si no se corrigió.

Sigue: PR-P7, imagen del portal con nginx y pruebas con curl · mantenimiento de docs-as-code (agente)
