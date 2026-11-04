# Constancia — PR-P4 del portal: build con MkDocs Material, alta inicial y control `portal-negativo`

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §1, §2, §7, §8 y plan de PRs, fila PR-P4) pide el generador con
versión fija y hashes, el build en un contenedor de Python fijado por digest, el paso `portal` en la compuerta, un job
que deje el sitio como artefacto sin desplegar, el alta inicial del manifiesto (folio 020) y el control
`portal_negativo.py` con su par rojo/verde.

**Qué se hizo.**

- `docs-as-code/portal/requirements-docs.txt`: `mkdocs-material==9.8.0` (con `mkdocs==1.6.1`) y su árbol completo,
  29 paquetes con versión exacta y hashes de PyPI. Se instala con `--require-hashes --no-deps --only-binary=:all:`.
- `docs-as-code/portal/mkdocs.yml`: `use_directory_urls: true` y `strict: true` (folio 018 §3), tema Material sin
  Google Fonts, búsqueda en español. `site_url` sale de `PORTAL_SITE_URL` (por defecto una URL local): el dominio lo
  decide el PO. Los enlaces a piezas no publicadas se informan sin cortar el build (`validation` en `info`); su
  reescritura y `portal_salida.py` llegan con PR-P5.
- `docs-as-code/portal/preparar.py`: copia a un directorio de contenido sólo las piezas del manifiesto, después de
  correr `portal_manifiesto.py`; si el manifiesto tiene hallazgos, no copia nada.
- `scripts/docs-portal.sh`: build en `python:3.12.16-slim@sha256:b18b261e…`, con `docker run --rm`, el usuario del
  host, el repo en sólo lectura y el `/tmp` del contenedor en un directorio de trabajo del host. La fecha del build sale
  del último commit (`SOURCE_DATE_EPOCH`). Con `PORTAL_REPRODUCIBLE=si` construye dos veces y exige `diff -r` vacío.
  El sitio queda fuera del árbol y al final corre `portal_negativo.py`.
- `docs-as-code/portal/publicar.yml`, alta inicial: `README.md`, `docs-as-code/glosario.md`,
  `docs-as-code/como-intervenir.md`, `docs-as-code/api/CHANGELOG-API.md` (única pieza `.md` de `docs-as-code/api/`) y
  `docs-as-code/punto-de-uso/catalogo.md`, todas con `llms: true`. Las tres con `audience` `docente` (glosario, cómo
  intervenir y catálogo) entran con `portal_docente_ok: true`, decisión explícita de este PR: son guías de trabajo y de
  vocabulario, no soluciones del alumno.
- `docs-as-code/controles/portal_negativo.py`: sobre el sitio, hallazgo `fuera-del-manifiesto` (archivo que no sale
  de una pieza ni de la infraestructura del generador), `patron-prohibido` y `urls-distintas` (sitemap, índice de
  búsqueda y, si existe, `llms.txt`). Tres precisiones sobre la lista del folio 018 §7:
  - los nombres de máquinas y las rutas propias de la infraestructura no se escriben en este repo público: van en un
    archivo de patrones privado (`--privados` o `PORTAL_PATRONES_PRIVADOS`), fuera del repo. En el repo quedan los
    patrones genéricos: redes privadas (10/8, 172.16/12, 192.168/16, 100.64/10), `jump-host`, rutas de un anfitrión
    (`/home/…/`, `/root/…/`, …), correos y `SigningKey` con valor;
  - `Audit` y `SDD/Expedientes` se buscan en URLs y no en el texto, porque una guía publicada nombra la carpeta de casos
    (`docs-as-code/como-intervenir.md:31`) y la palabra «Auditoría» aparece en el catálogo;
  - los créditos de autor del código de terceros del generador bajo `assets/` no cuentan como correo.
- `autoprueba.sh`: verde con un `site/` limpio; rojo con `site/` sembrado con `SDD/Expedientes/x.md`, con una
  dirección de red privada, con un enlace a `SDD/Expedientes`, con un nombre de máquina de un archivo de patrones
  privados y con un sitemap con una página de más; error de uso con un
  sitio inexistente. El caso «verde manifiesto del repo» de `portal-manifiesto` pasa a medirse contra la raíz del repo,
  porque el manifiesto ya no está vacío.
- `scripts/docs-local.sh`: paso `portal · build y negativo` en **avisa**, después del recuento. Usa docker y red; con
  `DOCS_PORTAL=no` o sin docker queda «omitido».
- `.github/workflows/docs.yml`: el job `docs` corre con `DOCS_PORTAL=no` (sigue sin red) y el nuevo job `portal`
  construye dos veces, corre `portal-negativo` en avisa y sube el sitio como artefacto `portal-sitio` (7 días).
  `contents: read`, sin secretos, acciones con versión exacta.

**Verificación local.** Build `--strict` verde con las 5 piezas; dos corridas del mismo commit sin diferencias;
`portal-negativo` verde sobre el sitio (52 archivos, 5 páginas), también con el archivo de patrones privados de la
infraestructura; autoprueba verde; `scripts/docs-local.sh` verde.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, el expediente 0002 ni
`SDD/Docs/**`. Nada de la capa de IA.

**Efecto sobre el producto.** Ninguno al fusionar: `deploy-front-ftp.yml` sólo corre con `workflow_dispatch` y ningún
archivo tocado entra al build de producción. El sitio sólo existe como artefacto del workflow; no se publica en ningún
dominio sin el ok del PO (folio 018, excepción).

Sigue: PR-P5, Markdown crudo, `llms.txt`, `llms-full.txt`, `/.well-known/api-catalog` y `portal_salida.py` ·
mantenimiento de docs-as-code (agente)
