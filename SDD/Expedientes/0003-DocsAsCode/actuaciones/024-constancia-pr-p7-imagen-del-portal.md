# Constancia — PR-P7 del portal: imagen con nginx sin privilegios, prueba con curl y controles del portal en bloquea

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §3, §7, §8 y plan de PRs, fila PR-P7) pide `docker/docs/Dockerfile`
multi-etapa (build en Python, servido con `nginxinc/nginx-unprivileged` en versión exacta y por digest), su
`nginx.conf` (`text/markdown`, `try_files`, 404, `charset utf-8`, `autoindex off`, cabeceras), un job que la construye
y la prueba con curl sin empujarla a ningún registro, y que los controles del portal pasen a bloquea. Sigue al folio 023.

**Qué se hizo.**

- `docker/docs/Dockerfile`. Etapa `sitio`: la misma imagen de Python fijada por digest que `scripts/docs-portal.sh`,
  dependencias con `--require-hashes` y los mismos pasos (`preparar.py`, `referencia_api.py`, `mkdocs build --strict`,
  `agentes.py`); al final corren `portal_negativo.py` y `portal_salida.py`, así que con un hallazgo no hay imagen.
  Etapa final: `nginxinc/nginx-unprivileged:1.28.0-alpine3.21@sha256:c97ff0bf…`, docroot vaciado (la imagen base trae
  `index.html` y `50x.html`), sitio copiado como root y servido con el usuario 101 en el puerto 8080, `HEALTHCHECK`
  sobre `/llms.txt` y la revisión expuesta en la etiqueta OCI y en la cabecera `X-Portal-Revision`.
  `docker/docs/Dockerfile.dockerignore` deja entrar al contexto sólo `README.md`, `docs-as-code/`, `.docs-guard/` (el
  lector de frontmatter que usa el manifiesto) y el `nginx.conf`.
- `docker/docs/nginx.conf`: `.md` como `text/markdown`, `charset utf-8`, `autoindex off`, `server_tokens off`; archivos
  ocultos en 404 salvo `/.well-known/`; `/.well-known/api-catalog` como `application/linkset+json`; un directorio con
  barra sirve sólo su `index.html` (sin él, 404 y no 403); la 404 es la del portal. Cabeceras a nivel de server, también
  en los 404: CSP (`default-src 'self'`, sin terceros), `nosniff`, `Referrer-Policy`, `X-Frame-Options`,
  `Permissions-Policy` y `Cross-Origin-Opener-Policy`. **Salvedad sobre el §3:** `try_files $uri $uri.html` se respeta,
  pero `/<ruta>` sin barra cuyo `index.html` existe redirige (301, relativo) a `/<ruta>/` en lugar de servirlo ahí,
  porque las páginas de MkDocs usan rutas relativas a su URL y servidas sin barra pierden estilos y enlaces.
- `docker/docs/probar.sh`: construye la imagen (o toma una dada), la corre como se va a correr (`--read-only`, tmpfs en
  `/tmp`, `--cap-drop ALL`, `no-new-privileges`, sólo en 127.0.0.1) y comprueba con curl el healthcheck,
  content-types, el JSON servido idéntico a la instantánea, los 404 (`/SDD/…`, `/.git/HEAD`,
  `/docs-as-code/controles/autoprueba.sh`, directorios sin índice, `50x.html`, `mcp/` y `agent-card.json`), la
  redirección de directorio, las cabeceras en 200 y en 404, `Server` sin versión, el usuario 101 y el sitio de sólo
  lectura.
- `docs.yml`: job nuevo `portal-imagen` que corre `probar.sh`. Sin push: no hay login a registros ni secretos.
- Controles en bloquea (folio 018 §7, después de corridas limpias desde PR-P4): `portal-manifiesto` y el paso `portal`
  (build, `portal-negativo` y `portal-salida`) en `scripts/docs-local.sh`, y el job `portal` de `docs.yml` deja de
  rebajar sus errores a avisos. `versiones-fijas` revisa también `docker/**/Dockerfile*` y sigue en avisa: sus
  hallazgos restantes están en `ci.yml`, `e2e.yml` y `deploy/Dockerfile*`, que este PR no toca.
- `docs-as-code/controles/README.md`: la tabla de modos y la línea de uso de `probar.sh`.

**Verificación local.** Imagen construida desde el árbol y `docker/docs/probar.sh` verde (todas las comprobaciones);
`scripts/docs-local.sh` verde con los controles del portal en bloquea; `versiones-fijas` sin hallazgos en
`docker/docs/Dockerfile`.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/` (incluidos sus `Dockerfile*`), `ai-layer/`, el
expediente 0002 ni `SDD/Docs/**`. Nada de la capa de IA.

**Efecto sobre el producto.** Ninguno al fusionar: `deploy-front-ftp.yml` sólo corre con `workflow_dispatch` y ningún
archivo tocado entra al build de producción (`deploy/`). La imagen sólo existe en el runner durante el job y se
descarta; no se publica en ningún registro ni dominio sin el ok del PO (folio 018, excepción).

Sigue: PR-P8, servicio del portal en el repositorio de despliegue ({servicio-privado}) · mantenimiento de docs-as-code (agente)
