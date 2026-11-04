# syntax=docker/dockerfile:1
# Imagen del portal de documentación de Lab-Geometria (expediente 0003, folio 018 §8, PR-P7).
#
# Multi-etapa: la etapa `sitio` construye el portal con los MISMOS pasos y la MISMA imagen de Python que
# scripts/docs-portal.sh (dependencias con --require-hashes, preparar.py, referencia_api.py, mkdocs --strict y
# agentes.py) y corre sobre el resultado portal-negativo y portal-salida, que bloquean: si dan rojo, no hay imagen. La
# etapa final sólo sirve el sitio con nginx sin privilegios (usuario 101, puerto 8080), fijado por digest.
#
# Contexto: la raíz del repo. El archivo al lado (docker/docs/Dockerfile.dockerignore) deja pasar sólo
# README.md, docs-as-code/ y .docs-guard/ (lector de frontmatter del manifiesto), que es todo lo que lee el build; nada más del repo entra a la imagen.
#
#   docker build -f docker/docs/Dockerfile \
#     --build-arg REVISION="$(git rev-parse HEAD)" \
#     --build-arg SOURCE_DATE_EPOCH="$(git log -1 --format=%ct)" -t lab-geometria-docs:local .
#
# Correr: read-only con tmpfs en /tmp (pid y temporales de nginx), p. ej.
#   docker run --rm --read-only --tmpfs /tmp -p 8080:8080 lab-geometria-docs:local
# No se publica en ningún registro ni dominio: la exposición la decide el PO (folio 018, excepción).

FROM python:3.12.16-slim@sha256:b18b261e1b4974b4f321451ad5251ded85abb22c2b8b262da527dda1d031ab30 AS sitio
ARG SOURCE_DATE_EPOCH=0
ARG PORTAL_SITE_URL=http://localhost:8080/
ENV PIP_NO_CACHE_DIR=1 PIP_DISABLE_PIP_VERSION_CHECK=1 PYTHONDONTWRITEBYTECODE=1 NO_MKDOCS_2_WARNING=1 \
    SOURCE_DATE_EPOCH=${SOURCE_DATE_EPOCH} PORTAL_SITE_URL=${PORTAL_SITE_URL} PORTAL_DOCS_DIR=/tmp/contenido
WORKDIR /repo
COPY docs-as-code/portal/requirements-docs.txt /repo/docs-as-code/portal/requirements-docs.txt
RUN python -m venv /opt/venv \
 && /opt/venv/bin/pip install -q --require-hashes --no-deps --only-binary=:all: \
      -r /repo/docs-as-code/portal/requirements-docs.txt
COPY README.md /repo/README.md
COPY docs-as-code /repo/docs-as-code
COPY .docs-guard /repo/.docs-guard
RUN python -I docs-as-code/portal/preparar.py /tmp/contenido \
 && python -I docs-as-code/portal/referencia_api.py --raiz /repo /tmp/contenido \
 && /opt/venv/bin/mkdocs build --strict --clean -f docs-as-code/portal/mkdocs.yml -d /site \
 && python -I docs-as-code/portal/agentes.py --raiz /repo /site \
 && python -I docs-as-code/controles/portal_negativo.py /site \
 && python -I docs-as-code/controles/portal_salida.py /site

FROM nginxinc/nginx-unprivileged:1.28.0-alpine3.21@sha256:c97ff0bf7cbae369953c6da1232ec14ad9f971d66360c5698db0856a4cd657a0
ARG REVISION=desconocida
LABEL org.opencontainers.image.title="lab-geometria-docs" \
      org.opencontainers.image.description="Portal de documentación de Lab-Geometria (sitio estático)" \
      org.opencontainers.image.source="https://github.com/hdcm-dev/Lab-Geometria" \
      org.opencontainers.image.revision="${REVISION}"
USER 0
COPY docker/docs/nginx.conf /etc/nginx/conf.d/default.conf
# El docroot de la imagen base trae index.html y 50x.html: se vacía para servir sólo el sitio.
RUN rm -rf /usr/share/nginx/html && mkdir /usr/share/nginx/html
# La revisión se expone en la cabecera X-Portal-Revision (incluida por nginx.conf a nivel de server).
RUN printf 'add_header X-Portal-Revision "%s" always;\n' "$(printf '%s' "$REVISION" | tr -cd '0-9A-Za-z._-')" \
      > /etc/nginx/conf.d/revision.inc
COPY --from=sitio --chown=0:0 /site /usr/share/nginx/html
USER 101
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD wget -q -O /dev/null http://127.0.0.1:8080/llms.txt || exit 1
