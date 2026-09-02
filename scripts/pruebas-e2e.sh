#!/usr/bin/env bash
# ============================================================================================
# CORRE LAS PRUEBAS DE EXTREMO A EXTREMO. Es el camino declarado, y hasta hoy no existía:
# la primera corrida de esta suite —2026-09-02— se hizo con una imagen armada a mano que nunca
# se versionó. La suite daba verde y el modo de correrla vivía en la memoria de una sesión.
#
#   scripts/pruebas-e2e.sh                    # banco local, chromium
#   scripts/pruebas-e2e.sh firefox            # banco local, firefox
#   scripts/pruebas-e2e.sh webkit
#
#   URL_BASE=https://... API_BASE_URL=http://... \
#   E2E_ADMIN_EMAIL=... E2E_ADMIN_PASSWORD=... scripts/pruebas-e2e.sh
#                                             # contra el laboratorio DESPLEGADO
#
# ============================================================================================
# LOS DOS MODOS, Y CUANDO USAR CADA UNO.
#
#   BANCO LOCAL (sin `URL_BASE`) — la suite publica y levanta el producto entero acá, con un
#   almacén de esta corrida y puertos que pide al sistema. No hace falta ningún secreto y NO SE
#   TOCA NINGUN DATO AJENO. Es el modo que se corre antes de empujar un cambio.
#
#   DESPLEGADO (con `URL_BASE`) — se abre el navegador contra el sitio publicado. Es el único
#   modo que puede decir «el sitio publicado anda», y el único que toca datos reales: siembra su
#   alumno y sus trabajos en el laboratorio del docente, y los borra al terminar.
#
# ============================================================================================
# TODO CORRE DENTRO DE UN CONTENEDOR, y no es una preferencia: el anfitrión de esta casa NO
# TIENE EL KIT DE DESARROLLO —decisión de la etapa `a`, intake §10— y tampoco tiene las
# bibliotecas de sistema que piden los navegadores. La imagen sale de `deploy/e2e/Dockerfile` y
# se construye sola la primera vez.
#
# NO SE TOCAN LOS CONTENEDORES DEL PRODUCT OWNER. `gf-api`, `gf-web`, `gf-back` y `gf-tunnel`
# son su despliegue local: acá el contenedor es efímero, sin nombre fijo, y el banco pide sus
# puertos al sistema en vez de fijarlos.
# ============================================================================================
set -euo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

navegador="${NAVEGADOR:-${1:-chromium}}"
imagen="${IMAGEN_E2E:-gf-e2e:local}"
cache="${GF_E2E_CACHE:-$HOME/.cache/geometria-factory-medicion/nuget}"

case "$navegador" in
  chromium|firefox|webkit) ;;
  *) echo "Navegador desconocido: «$navegador». Son chromium, firefox o webkit." >&2; exit 2 ;;
esac

mkdir -p "$cache"

if ! docker image inspect "$imagen" >/dev/null 2>&1; then
  echo "== Construyendo «$imagen» (sólo la primera vez) =="
  docker build -t "$imagen" "$raiz/deploy/e2e"
fi

# EL BUNDLE DEL VISOR SE GENERA ANTES, Y NO ES OPCIONAL. No se versiona —es artefacto—, y sin él
# la escena 3D no carga: los casos que la miran fallarían por una razón que no es del producto.
# `build-visor.sh` corre `npm ci` cuando hay candado, así que es reproducible.
if [ ! -f src/GeometriaFactory.Web/wwwroot/js/geometriafactory-visor.js ]; then
  echo "== Generando el bundle del visor =="
  docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp \
    -v "$raiz:/repo" -w /repo "$imagen" scripts/build-visor.sh
fi

echo "== Recorrido en $navegador =="

# `--network host` PORQUE EL BANCO LOCAL ESCUCHA EN `127.0.0.1` adentro del contenedor y el
# navegador corre en ese mismo contenedor: no hay nada que publicar hacia afuera. En modo
# desplegado la red del anfitrión es la que alcanza el sitio.
exec docker run --rm -i \
  --user "$(id -u):$(id -g)" \
  --ipc=host \
  --network host \
  --env HOME=/tmp \
  --env NUGET_PACKAGES=/nuget \
  --env DOTNET_CLI_TELEMETRY_OPTOUT=1 \
  --env DOTNET_NOLOGO=1 \
  --env URL_BASE="${URL_BASE:-}" \
  --env API_BASE_URL="${API_BASE_URL:-}" \
  --env E2E_ADMIN_EMAIL="${E2E_ADMIN_EMAIL:-}" \
  --env E2E_ADMIN_PASSWORD="${E2E_ADMIN_PASSWORD:-}" \
  --env TRAZAR="${TRAZAR:-true}" \
  --volume "$cache:/nuget" \
  --volume "$raiz:/repo" \
  --workdir /repo \
  "$imagen" bash -lc "
set -euo pipefail
dotnet test tests/GeometriaFactory.E2ETests \
  --configuration Release --nologo \
  --settings pruebas-e2e.runsettings \
  --logger 'trx;LogFileName=$navegador.trx' \
  -- Playwright.BrowserName=$navegador
"
