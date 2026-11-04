# Resolución — portal de documentación de Lab-Geometria

| Campo | Valor |
| --- | --- |
| Tipo | resolucion |
| Fecha | 2026-11-03 |
| Autor | presidencia de la mesa del portal de documentación (agente) |
| Corrige | — |
| Reintegra | ninguna |

**Caso.** Publicar la documentación pública del repositorio como un portal navegable, legible por personas y por
agentes (Markdown crudo, `llms.txt`), sin filtrar nada interno. La mesa tuvo cuatro especialistas (portales y
generadores, capa para agentes, seguridad, despliegue) y decidió todo, con **una excepción: el dominio y la
exposición pública los decide el Product Owner**. El portal se construye, se verifica y deja su contenedor listo,
pero no se expone sin ese ok.

**Hallazgo que cambia el pedido.** Filtrar por `delivery` público no filtra nada: en este repositorio `delivery`
vale siempre `[publico]` (D-18, `docs-as-code/esquema-frontmatter.md:37,46,50`, y el control
`docs-as-code/controles/delivery_publico.py`).

**Decisión.**

1. **Filtro por manifiesto explícito**, `docs-as-code/portal/publicar.yml`: lista permitida de rutas, sin globs
   amplios; lo no listado no se publica. Cada pieza lleva `llms: true|false`; una pieza con `audience` que contiene
   `docente` entra sólo con `portal_docente_ok: true`, decidido en el PR. No se toca D-18.
   - **Excluidos siempre:** `SDD/**`, `_legacy/**`, `ai-layer/**`, `.docs-guard/**`, `docs-as-code/controles/**` y
     toda solución del alumno (el alumno construye el JSON a mano).
   - **Alta inicial:** `README.md`, `docs-as-code/glosario.md`, `docs-as-code/como-intervenir.md`,
     `docs-as-code/api/**` y `docs-as-code/punto-de-uso/**`, cada pieza revisada al darla de alta.
2. **Generador: MkDocs Material** (3 de 4), versión exacta con hashes en `portal/requirements-docs.txt`
   (`pip install --require-hashes`), construido en una imagen `python:3.12.<x>-slim` fijada por digest. La compuerta
   ya usa Python (`scripts/docs-local.sh:5`), lee el Markdown tal cual y tiene poca cadena de suministro. Riesgo
   anotado: Material está en mantenimiento; con versión fija es aceptable y la capa para agentes es un guion propio,
   independiente del generador. Descartados: Docusaurus (Node, MDX v3 rompe `<` y `{`), Starlight (Node, mueve el
   contenido y rompe anclas), Mintlify y Vercel (SaaS sin versión fija), GitHub Pages (sin control de
   content-types), servir desde el front o la API viva (acopla releases).
3. **URLs:** `use_directory_urls: true`; página en `/<ruta>/` y su Markdown crudo en `/<ruta>.md` (también
   `/<ruta>/index.md`). El crudo es el mismo que ya es público en GitHub, con el frontmatter reducido a `doc_id`,
   `title` y `audience` y los enlaces relativos reescritos a `.md` absolutos del portal. nginx con `try_files`,
   `text/markdown`, `charset utf-8` y `autoindex off`. Referencia de API en `/api/` y el JSON en
   `/api/openapi-v1.json`, idéntico byte a byte a la instantánea.
4. **`llms.txt` y `llms-full.txt`** generados por `docs-as-code/portal/agentes.py` (`python3 -I`, sin
   dependencias), con el manifiesto (`llms: true`) como única fuente. `llms-full.txt` con tope de 400 KB: si lo
   supera, el build falla.
5. **`/.well-known`:** se publica `api-catalog` (linkset RFC 9727, `application/linkset+json`, `service-desc` a
   `/api/openapi-v1.json` y `service-doc` a `/api/`, nunca a la instancia viva). Se descartan
   `mcp/server-card.json` (no hay MCP: `ai-layer/01-Especificacion-V1-Pistas.md:111`) y `agent-card.json` (no hay
   agente A2A; 2 a 2, desempata no publicar nada falso). Si aparece un servicio real, se reabre con un PR propio.
6. **Referencia de API:** páginas Markdown por grupo de operaciones generadas desde
   `docs-as-code/api/openapi-v1.json` y Redoc con versión exacta, vendorizado, sin CDN. El conteo de operaciones
   renderizadas es igual al de `recuento_openapi.py`. El portal no expone autenticación de aplicaciones: la API
   autentica personas (ADR-00009).
7. **Controles nuevos**, con par rojo/verde en `docs-as-code/controles/autoprueba.sh`, en avisa hasta una corrida
   limpia:
   - `portal_manifiesto.py`: rojo si una pieza del manifiesto cae fuera de lo permitido o es `docente` sin
     `portal_docente_ok`.
   - `portal_negativo.py`: rojo si `site/` tiene un archivo que no sale del manifiesto, si aparecen patrones
     prohibidos (rutas de `SDD/Expedientes`, `Audit`, `_legacy`, nombres de máquinas y rangos de red internos,
     rutas del host, correos, claves con valor) o si sitemap, `llms.txt` e índice de búsqueda difieren en URLs.
   - `portal_salida.py`: rojo si una página no tiene su `.md` hermano, si un `.md` enlaza una pieza no publicada,
     si `llms-full.txt` supera el tope, si el linkset es inválido, si el openapi servido difiere de la instantánea o
     si existen `mcp/` o `agent-card.json`.
   - `versiones_fijas.py`: rojo ante `uses: x@vN` sin parche, versiones `x.x` flotantes o imágenes sin digest. Rojo
     con el `deploy-front-ftp.yml` de hoy (`.github/workflows/deploy-front-ftp.yml:52-62`).
   `recuento_openapi.py` pasa a bloquea antes de publicar.
8. **CI y publicación:** `scripts/docs-local.sh` suma un paso `portal` después de `recuento` (`mkdocs build
   --strict`, `agentes.py`, controles del portal). `docs.yml` construye sitio e imagen en cada PR y sube el
   artefacto; **no despliega**; las actions quedan fijadas como hoy (`.github/workflows/docs.yml:39`,
   `checkout@v4.2.2`). Imagen multi-stage servida con `nginx-unprivileged` fijada por digest, `read_only` con tmpfs,
   CSP, `nosniff`, healthcheck sobre `/llms.txt` y revisión expuesta. El servicio de contenedor vive en
   {servicio-privado}; nada de ese repositorio entra al build público. Despliegue con etiqueta de rollback, build
   con el servicio arriba, `up -d` y verificación; prueba negativa en vivo (rutas `SDD/`, `.git/HEAD`, controles y
   directorios sin índice dan 404). **La regla de ingress y el hostname quedan pendientes del ok del Product
   Owner.**

**Veredicto por criterio.**

| Criterio | Veredicto | Observable |
| --- | --- | --- |
| Nada interno se publica | cumple por diseño | manifiesto como lista permitida; `portal_manifiesto.py` y `portal_negativo.py` |
| D-18 queda intacto | cumple | `esquema-frontmatter.md` sin cambios en el plan |
| No se anuncia lo que no existe | cumple | `portal_salida.py`: sin `mcp/` ni `agent-card.json` |
| Versiones fijas en build y CI | declarado | `versiones_fijas.py` (PR-P2) |
| Fusionar no despliega | cumple por diseño | `docs.yml` sube artefacto; `deploy-front-ftp.yml` es sólo `workflow_dispatch` |
| Exposición pública | pendiente del Product Owner | PR-P10 no se abre sin su ok |
| No se edita `SDD/Docs/**` | cumple | ningún PR del plan lo toca |

**Plan** (cada PR se fusiona por separado y lleva su control rojo/verde).

| PR | Repositorio | Contenido | Prueba |
| --- | --- | --- | --- |
| PR-P1 | Lab-Geometria | esta resolución, folio 018 | compuerta de docs en verde; sólo docs |
| PR-P2 | Lab-Geometria | `versiones_fijas.py` y versiones fijas en `deploy-front-ftp.yml` | rojo con el archivo de hoy, verde fijado |
| PR-P3 | Lab-Geometria | `docs-as-code/portal/publicar.yml` vacío y `portal_manifiesto.py` en avisa | par en `autoprueba.sh` |
| PR-P4 | Lab-Geometria | `mkdocs.yml`, requirements con hashes, `scripts/docs-portal.sh`, paso `portal`, job de artefacto, alta inicial y `portal_negativo.py` | `--strict`, dos builds sin diferencias, par de `portal_negativo` |
| PR-P5 | Lab-Geometria | `agentes.py` (`.md` crudos, `llms.txt`, `llms-full.txt`, `api-catalog`) y `portal_salida.py` | golden files, linkset válido, sin `mcp/` ni `agent-card` |
| PR-P6 | Lab-Geometria | referencia de API y Redoc vendorizado; `recuento_openapi` a bloquea | conteo igual, JSON idéntico |
| PR-P7 | Lab-Geometria | `docker/docs/Dockerfile` y `nginx.conf`, job que construye y prueba con curl, sin push; controles del portal a bloquea | curl en CI |
| PR-P8 | {servicio-privado} | servicio del portal en compose, sincronía, CHANGELOG y guía | `docker compose config` y healthcheck |
| PR-P9 | {servicio-privado} | contenedor levantado sin ingress, en red interna | verificación interna y rollback probado |
| PR-P10 | {servicio-privado} y túnel | **con ok del Product Owner:** hostname, ingress, prueba negativa en vivo y capturas | curl externo, 404 en rutas internas |

**Efecto sobre el producto.** Ninguno en este folio. El plan no toca `src/`, `Contracts/`, `tests/`, `visor/`,
`deploy/Dockerfile*` ni la capa de IA del expediente 0002.

Sigue: PR-P2, versiones fijas con `versiones_fijas.py` · mantenimiento de docs-as-code (agente)
