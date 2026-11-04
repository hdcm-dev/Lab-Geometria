#!/usr/bin/env bash
# Build del portal de documentación de Lab-Geometria (expediente 0003, folio 018 §2 y §8, PR-P4).
#
# Construye el sitio estático con MkDocs Material DENTRO de una imagen de Python fijada por digest, con las
# dependencias de docs-as-code/portal/requirements-docs.txt instaladas con --require-hashes. No publica ni despliega:
# deja el sitio en un directorio fuera del árbol y corre sobre él el control portal_negativo.py.
#
# Uso:  scripts/docs-portal.sh                       # sitio en un directorio temporal (se informa al final)
#       PORTAL_SALIDA=DIR scripts/docs-portal.sh      # sitio en DIR/site (DIR fuera del árbol del repo)
#       PORTAL_REPRODUCIBLE=si scripts/docs-portal.sh # construye dos veces y exige que no haya diferencias
#       PORTAL_PATRONES_PRIVADOS=ARCHIVO …            # suma a portal-negativo patrones privados (fuera del repo)
#
# Requisitos: bash, docker y red para bajar los paquetes de PyPI (los hashes los fijan). El contenedor corre con
# --rm, con el usuario del host, el repo montado en sólo lectura y su /tmp en un directorio de trabajo del host
# (que sigue a TMPDIR). La fecha del build sale del último commit (SOURCE_DATE_EPOCH): dos corridas del mismo commit
# dan el mismo sitio.
# Código de salida: 0 sitio construido y portal-negativo verde; 1 build o control en rojo; 2 error de uso.
set -euo pipefail

IMAGEN='python:3.12.16-slim@sha256:b18b261e1b4974b4f321451ad5251ded85abb22c2b8b262da527dda1d031ab30'
RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
command -v docker >/dev/null || { echo "docs-portal: falta docker" >&2; exit 2; }

SALIDA="${PORTAL_SALIDA:-$(mktemp -d)}"
mkdir -p "$SALIDA"
SALIDA="$(cd "$SALIDA" && pwd)"
case "$SALIDA/" in "$RAIZ"/*) echo "docs-portal: PORTAL_SALIDA no puede quedar dentro del repo" >&2; exit 2 ;; esac
TRABAJO="$(mktemp -d)"
trap 'rm -rf "$TRABAJO"' EXIT
EPOCA="$(git -C "$RAIZ" log -1 --format=%ct 2>/dev/null || echo 0)"

construir() {  # construir DESTINO — sitio en DESTINO (directorio del host)
  local destino="$1"
  rm -rf "$destino" "$TRABAJO/tmp"
  mkdir -p "$destino" "$TRABAJO/tmp"
  docker run --rm \
    --user "$(id -u):$(id -g)" \
    -e HOME=/tmp -e NO_MKDOCS_2_WARNING=1 \
    -e PIP_NO_CACHE_DIR=1 -e PIP_DISABLE_PIP_VERSION_CHECK=1 -e PYTHONDONTWRITEBYTECODE=1 \
    -e SOURCE_DATE_EPOCH="$EPOCA" -e PORTAL_DOCS_DIR=/tmp/contenido \
    ${PORTAL_SITE_URL:+-e PORTAL_SITE_URL="$PORTAL_SITE_URL"} \
    -v "$RAIZ":/repo:ro -v "$TRABAJO/tmp":/tmp -v "$destino":/salida \
    "$IMAGEN" sh -euc '
      python -m venv /tmp/venv
      /tmp/venv/bin/pip install -q --require-hashes --no-deps --only-binary=:all: \
        -r /repo/docs-as-code/portal/requirements-docs.txt
      python -I /repo/docs-as-code/portal/preparar.py /tmp/contenido
      /tmp/venv/bin/mkdocs build --strict --clean -f /repo/docs-as-code/portal/mkdocs.yml -d /salida
    '
}

construir "$SALIDA/site"
echo "docs-portal: sitio en $SALIDA/site"

if [ "${PORTAL_REPRODUCIBLE:-no}" = si ]; then
  construir "$TRABAJO/segunda"
  if diff -r "$SALIDA/site" "$TRABAJO/segunda" >/dev/null; then
    echo "docs-portal: reproducible (dos corridas sin diferencias)"
  else
    diff -rq "$SALIDA/site" "$TRABAJO/segunda" || true
    echo "docs-portal: ROJO, dos corridas del mismo commit dieron sitios distintos" >&2
    exit 1
  fi
fi

python3 -I "$RAIZ/docs-as-code/controles/portal_negativo.py" "$SALIDA/site"
