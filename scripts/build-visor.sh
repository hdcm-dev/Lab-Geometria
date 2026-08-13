#!/usr/bin/env bash
# Ciclo corto del visor: instalación reproducible → empaquetado → copia al front.
# Criterio de éxito declarado: el bundle se genera sin errores (`QG-01` de `GeometriaFactory-Visor`).
# Fuente: intake §17.7.P.8; `Visor/09 Pipeline-CI-CD.md` §2 y `Guia-Publicacion-Bundle-Visor.md`.
#
# TODO CORRE DENTRO DEL CONTENEDOR DE DESARROLLO. El host no tiene el kit de desarrollo
# (intake, encabezado de la Parte C y §10; `Domain/09 Entornos-Deploy.md` §2).
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
visor_dir="$repo_root/visor"
front_js_dir="$repo_root/src/GeometriaFactory.Web/wwwroot/js"

cd "$visor_dir"

if [ -f package-lock.json ]; then
  npm ci
else
  # La etapa `a` deja el árbol sin `package-lock.json` porque no hubo red para resolverlo.
  # La primera corrida lo genera, y a partir de ahí `npm ci` es lo que corre.
  echo "AVISO: no hay package-lock.json. Se resuelve con 'npm install' y se versiona el resultado."
  npm install
fi

npm run build

mkdir -p "$front_js_dir"
cp dist/geometriafactory-visor.js "$front_js_dir/"
if [ -f dist/geometriafactory-visor.js.map ]; then
  cp dist/geometriafactory-visor.js.map "$front_js_dir/"
fi

echo "Bundle generado y copiado a src/GeometriaFactory.Web/wwwroot/js/"
