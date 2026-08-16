#!/usr/bin/env bash
# Medición de la puerta técnica `PT-02` sobre el paquete construido, CON NAVEGADOR DE VERDAD.
#
# QUÉ MIDE. `PRODUCT-INTAKE` §12 y §17.7.P.10 la enuncian así: «sin degradación tras 10
# navegaciones de ida y vuelta entre trabajos: `destruir` libera geometrías, materiales y el
# contexto WebGL». Cada escena toma un contexto gráfico y el navegador permite pocos vivos —del
# orden de ocho a dieciséis—: si al salir no se libera, **el navegador descarta el más viejo sin
# avisar** y la escena se apaga sin error, sin excepción y sin nada en el registro. Diez está
# elegido por encima de ese límite, que es lo que hace aparecer el defecto si existe.
#
# POR QUÉ MIDE EL MECANISMO Y NO LA RUTA. Una navegación de ida y vuelta hace por dentro un par
# «crear, liberar», y ese par es lo que puede fallar. Ejercerlo diez veces mide lo mismo con menos
# piezas en el medio, y **sin levantar el producto entero**: esta medición no necesita base de
# datos, ni servicio de datos, ni sesión.
#
# POR QUÉ EL NAVEGADOR VIENE EN CONTENEDOR Y NO SE INSTALA. El entorno de desarrollo declarado no
# tiene navegador, y agregarle uno al `devcontainer` sería cambiar el entorno de todos para una
# medición que corre de vez en cuando. La imagen trae Chromium listo y se descarta al terminar.
#
# Y POR QUÉ LA BIBLIOTECA DE MANEJO NO ENTRA EN `package.json`: `PT-03` exige que el paquete lleve
# adentro lo que necesita para dibujar, y esta biblioteca **no es del producto sino de su
# medición**. Se instala dentro del contenedor, que se tira.
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

imagen="mcr.microsoft.com/playwright:v1.48.0-jammy"

# EL PAQUETE SE MIDE CONSTRUIDO, no en fuente: es lo que se sirve, y es donde la puerta puede
# fallar sin que nadie lo note.
if [ ! -f visor/dist/geometriafactory-visor.js ]; then
  printf 'El paquete no está construido. Corré primero: bash scripts/build-visor.sh\n' >&2
  exit 1
fi

docker run --rm -v "$raiz/visor":/v -w /v "$imagen" \
  bash -c 'npm install --no-save playwright@1.48.0 >/dev/null 2>&1 && node verification/lifecycle.mjs'
