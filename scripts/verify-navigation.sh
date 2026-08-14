#!/usr/bin/env bash
# ============================================================================
# verify-navigation.sh — Puerta del primer criterio de transición de la etapa
# `b`: «Todas las rutas del mapa de navegación son alcanzables, con pantallas
# de marcador de posición» (`Roadmap-Producto.md`).
#
# Levanta el front y comprueba, contra el servidor de verdad:
#   C-1  Las trece rutas del producto responden 200 y devuelven su `<h1>`.
#   C-2  `SUP-08` y `SUP-11` NO tienen ruta —404— y sí aparecen alojada y
#        superpuesta donde la línea de base manda.
#   C-3  Cada superficie de trabajo dibuja los TRES destinos de su papel y
#        NINGUNO del otro; las de acceso no dibujan barra lateral.
#   C-4  La dirección que no existe devuelve 404 con la pantalla puesta.
#
# Se corre dentro del contenedor del SDK, desde la raíz del repositorio:
#   docker run --rm -v "$PWD":/w -w /w mcr.microsoft.com/dotnet/sdk:10.0 \
#     bash scripts/verify-navigation.sh
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."
PORT=${PORT:-5099}
BASE=http://127.0.0.1:$PORT
fails=0

dotnet build src/GeometriaFactory.Web/GeometriaFactory.Web.csproj -warnaserror | tail -4
dotnet run --project src/GeometriaFactory.Web/GeometriaFactory.Web.csproj --no-build --urls "$BASE" > /tmp/web.log 2>&1 &
SRV=$!
trap 'kill $SRV 2>/dev/null' EXIT
for _ in $(seq 1 90); do curl -s -o /dev/null "$BASE/" && break || sleep 1; done

printf '\n== C-1 · las rutas del producto ==\n'
printf '%-8s %-10s %-36s %s\n' SUP CODIGO RUTA '<h1>'
check() { # SUP  ruta  esperado
  code=$(curl -s -o /tmp/b.html -w '%{http_code}' "$BASE$2")
  h1=$(sed -n 's/.*<h1[^>]*>\([^<]*\)<\/h1>.*/\1/p' /tmp/b.html | head -1)
  printf '%-8s %-10s %-36s %s\n' "$1" "$code" "$2" "$h1"
  [ "$code" = "$3" ] || fails=$((fails+1))
}
check —      /                                  200
check SUP-01 /aprovisionamiento-inicial         200
check SUP-02 /registro-de-cuenta                200
check SUP-03 /ingreso                           200
check SUP-04 /credencial-propia/establecer      200
check SUP-04 /credencial-propia/cambio-obligado 200
check SUP-04 /mi-contrasena                     200
check SUP-05 /mis-trabajos                      200
check SUP-06 /trabajo-nuevo                     200
check SUP-06 /trabajos/T-1/editar               200
check SUP-07 /trabajos/T-1                      200
check SUP-09 /cuentas                           200
check SUP-10 /entrega-comision                  200
check —      /estado                            200

printf '\n== C-2 · las dos superficies SIN ruta ==\n'
check SUP-08 /resolucion-del-trabajo            404
check SUP-11 /estado-degradado-y-reconexion     404
n=$(curl -s "$BASE/trabajos/T-1" | grep -c 'SUP-08')
echo "SUP-08 alojada dentro de SUP-07 (/trabajos/T-1): $n ocurrencia(s)"; [ "$n" -ge 1 ] || fails=$((fails+1))

printf '\n== C-3 · los tres destinos por papel ==\n'
destinos() { curl -s "$BASE$1" | tr '\n' ' ' | grep -oE '<ul class="gf-nav[^"]*">.*</ul>' \
             | grep -oE '</svg>[^<]+</a>' | sed 's|</svg>||;s|</a>||' | tr '\n' '|'; }
for r in /mis-trabajos /trabajo-nuevo '/mi-contrasena?papel=alumno' /trabajos/T-1; do
  d=$(destinos "$r"); printf '%-38s %s\n' "$r" "${d:-(sin barra lateral)}"
  case "$d" in *Cuentas*|*comisi*) echo "   FALLA: dibuja destinos del administrador"; fails=$((fails+1));; esac
done
for r in /entrega-comision /cuentas '/mi-contrasena?papel=administrador'; do
  d=$(destinos "$r"); printf '%-38s %s\n' "$r" "${d:-(sin barra lateral)}"
  case "$d" in *"Mis trabajos"*|*"Trabajo nuevo"*) echo "   FALLA: dibuja destinos del alumno"; fails=$((fails+1));; esac
done
for r in /ingreso /registro-de-cuenta /aprovisionamiento-inicial /credencial-propia/establecer; do
  d=$(destinos "$r"); printf '%-38s %s\n' "$r" "${d:-(sin barra lateral · correcto)}"
  [ -z "$d" ] || { echo "   FALLA: el shell de acceso no lleva navegación"; fails=$((fails+1)); }
done

printf '\n== C-4 · la dirección que no existe ==\n'
code=$(curl -s -o /tmp/nf.html -w '%{http_code}' "$BASE/ruta-que-no-existe")
h1=$(sed -n 's/.*<h1[^>]*>\([^<]*\)<\/h1>.*/\1/p' /tmp/nf.html | head -1)
echo "codigo=$code  h1=$h1"
{ [ "$code" = "404" ] && [ -n "$h1" ]; } || fails=$((fails+1))

printf '\n== RESULTADO ==\n'
[ "$fails" -eq 0 ] && { echo "CONFORME · los cuatro controles pasan"; exit 0; }
echo "NO CONFORME · $fails comprobacion(es) fallan"; exit 1
