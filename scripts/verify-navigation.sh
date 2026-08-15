#!/usr/bin/env bash
# ============================================================================
# verify-navigation.sh — Puerta del primer criterio de transición de la etapa
# `b`: «Todas las rutas del mapa de navegación son alcanzables, con pantallas
# de marcador de posición» (`Roadmap-Producto.md`).
#
# Levanta el front y comprueba, contra el servidor de verdad:
#   C-1  Las trece rutas del producto responden 200 y devuelven su `<h1>`.
#   C-2  `SUP-08` y `SUP-11` NO tienen ruta —404— y sí aparecen alojada y
#        superpuesta donde la línea de base manda. El alojamiento de `SUP-08` se
#        reconoce por el encabezado de su bloque, no por su identificador: la
#        etapa `b` sacó los `SUP-XX` de la pantalla.
#   C-3  Cada superficie de trabajo dibuja los TRES destinos de su papel y
#        NINGUNO del otro; las de acceso no dibujan barra lateral.
#   C-4  La dirección que no existe devuelve 404 con la pantalla puesta.
#
# POR QUÉ ESTE GUION CORRE CON LA PUERTA DE SERVICIO PUESTA, Y QUÉ NO SIGNIFICA.
# Este guion se escribió en la etapa `b`, cuando no había sesión: era la forma
# de que el Product Owner se paseara por las trece pantallas y las aprobara. Hoy
# el guardián 2 de `Web ADR-03` §2 está CERRADO —«ninguna ruta del panel es
# accesible sin sesión»—, y sin salvedad las siete rutas del panel desviarían a
# `/ingreso` y este guion pasaría a exigir un guardián abierto, que es lo
# contrario de lo que la ADR manda.
#
# De modo que la pieza se levanta EN DESARROLLO y con la puerta de servicio
# puesta EXPLÍCITAMENTE —`PanelWalkthroughWithoutSession`—, que es la opción que
# `PanelSessionGateMiddleware` sólo honra en el entorno de desarrollo. **Que
# `C-1` devuelva 200 acá NO dice que el panel sea público**: dice que las trece
# pantallas dibujan, que es lo que este guion fue a comprobar. Quien verifica el
# guardián es `scripts/verify-stage-c.sh`, que corre como `Production` y donde
# esas mismas siete rutas desvían; y `PanelSessionGateTests`, que comprueba que
# la opción puesta en producción no abre nada.
#
# ESTE GUION SE QUEDA EN `Release` AUNQUE `run-web.sh` HAYA PASADO A `Debug`, Y LA
# ASIMETRÍA ESTÁ PUESTA A PROPÓSITO. El Product Owner decidió que en desarrollo se
# trabaja en `Debug`, y los guiones de EJECUCIÓN lo declaran así. Los de VERIFICACIÓN
# no: miden **lo que efectivamente se despliega**, que es `Release`. Verificar una
# salida distinta de la que sale a producción es el mismo defecto que la regla de
# configuración explícita erradicó, con otro disfraz. **QUE NADIE LA «CORRIJA» POR
# SIMETRÍA:** no es una inconsistencia olvidada entre guiones, es la decisión.
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

# LA CONFIGURACIÓN SE DECLARA EN LOS DOS LADOS (`Pipeline-Producto.md` §3.1). Hasta acá este
# guion construía sin decirla y levantaba sin decirla: acertaba POR OMISIÓN EN LOS DOS LADOS, no
# por decisión. Bastaba que alguien agregara `-c Release` de un solo lado para reproducir entero
# el defecto que `verify-stage-c.sh` documenta en su cabecera. Se declara `Release` para que este
# guion mida la misma salida que `build.sh`, `test.sh` y `verify-stage-c.sh`.
CONFIGURATION=${GF_CONFIGURATION:-Release}
WEB_PROJECT=src/GeometriaFactory.Web/GeometriaFactory.Web.csproj

# Y EL CÓDIGO DE SALIDA DE LA CONSTRUCCIÓN SE MIRA. Este guion corre con `set -uo pipefail` y
# SIN `-e` a propósito —los controles de más abajo cuentan fallas en vez de abortar—, de modo que
# la construcción canalizada a `tail` que había acá perdía su código de salida DOS VECES: si
# fallaba, el `--no-build` de más abajo levantaba tranquilamente el binario de la corrida
# anterior y lo que se verificaba era código viejo. La construcción pasa ahora por la red, que
# mira ese código, comprueba que el ensamblado de esta configuración exista, y no vuelve con 0
# de ninguna otra manera. `-warnaserror` viaja hasta la construcción sin cambiar de sentido.
scripts/assert-build-fresh.sh "$WEB_PROJECT" "$CONFIGURATION" -warnaserror || exit 1

# El entorno y la puerta van EXPLÍCITOS y no heredados: si el entorno se colara
# como cualquier otra cosa, la puerta no regiría y `C-1` fallaría en las siete
# rutas del panel sin que quede claro por qué.
# Y el puerto también va explícito: `appsettings.Development.json` fija un
# endpoint de Kestrel que, en desarrollo, le gana a `--urls`.
ASPNETCORE_ENVIRONMENT=Development \
PanelWalkthroughWithoutSession=true \
Kestrel__Endpoints__Http__Url="$BASE" \
dotnet run --project "$WEB_PROJECT" -c "$CONFIGURATION" --no-build --urls "$BASE" > /tmp/web.log 2>&1 &
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
# ---------------------------------------------------------------------------
# EL ALOJAMIENTO DE SUP-08 SE COMPRUEBA POR INSPECCIÓN DE LA FUENTE, Y ESTE CONTROL SE REESCRIBIÓ
# EN LA ETAPA `e`. EL MOTIVO IMPORTA MÁS QUE LA REDACCIÓN.
#
# Hasta la etapa `d` este control pedía `/trabajos/T-1?papel=administrador` y exigía que el cuerpo
# trajera el encabezado del bloque de resolución. Eso era correcto **mientras la superficie era un
# marcador de posición**: sin dato, la única condición del wireframe que se podía evaluar era el
# papel, y el papel salía de la dirección.
#
# LA ETAPA `e` LE DA DATO A ESA SUPERFICIE, y con eso la condición del wireframe se puede construir
# entera: el bloque se dibuja cuando quien mira es el administrador **y** el trabajo está en estado
# `Pendiente`, las dos a la vez. Este guion corre **sin sesión** y con la puerta de servicio puesta,
# de modo que `/trabajos/T-1` no trae ningún trabajo, y `T-1` ni siquiera tiene forma de
# identificador: no hay ningún papel ni ninguna dirección que pueda hacer aparecer el bloque, y
# **eso es lo correcto**. Dibujarlo igual sería ofrecerle a alguien decidir sobre un trabajo que no
# está a la vista, que es exactamente la superficie que miente que la etapa `e` vino a cerrar.
#
# QUÉ SE MIDE AHORA, Y POR QUÉ SIGUE SIENDO LA MISMA PUERTA. Lo que este control existe para
# impedir es que alguien le dé **ruta propia** a `SUP-08` —`Linea-Base-Visual.md` §2: «un sistema
# construido que le dé ruta propia es deriva mayor»—. Eso se mide en dos mitades que no dependen de
# ningún dato: el `404` de `/resolucion-del-trabajo` de acá arriba, y la fuente misma —el componente
# no declara `@page` y la superficie que lo aloja lo invoca—. **No se afloja nada**: se mide lo que
# la regla dice, en lugar de medir un efecto que la etapa anterior producía por no tener datos.
BLOQUE=src/GeometriaFactory.Web/Components/Work/WorkResolution.razor
ANFITRION=src/GeometriaFactory.Web/Components/Pages/WorkView.razor

rutas=$(grep -c '^@page' "$BLOQUE")
echo "SUP-08 sin ruta propia (declaraciones \`@page\` en $BLOQUE): $rutas"
[ "$rutas" -eq 0 ] || { echo "   FALLA: SUP-08 declara una ruta propia"; fails=$((fails+1)); }

alojado=$(grep -c '<WorkResolution' "$ANFITRION")
echo "SUP-08 alojada dentro de SUP-07 (invocaciones en $ANFITRION): $alojado"
[ "$alojado" -ge 1 ] || { echo "   FALLA: SUP-07 no aloja el bloque de resolución"; fails=$((fails+1)); }

# Y NINGUNA OTRA SUPERFICIE LO ALOJA: `SUP-08` vive en una sola.
otros=$(grep -rl '<WorkResolution' --include='*.razor' src/GeometriaFactory.Web | grep -cv "^$ANFITRION$")
echo "SUP-08 alojada en UNA sola superficie (otras que la invocan): $otros"
[ "$otros" -eq 0 ] || { echo "   FALLA: más de una superficie aloja el bloque"; fails=$((fails+1)); }

# Y SIN SESIÓN NO SE DIBUJA POR NINGUNA DIRECCIÓN, con ni sin `papel=administrador`: la condición
# del wireframe se evalúa sobre el trabajo, y sin sesión no hay trabajo que traer.
for r in "/trabajos/T-1" "/trabajos/T-1?papel=administrador"; do
  m=$(curl -s "$BASE$r" | grep -c 'Resolver esta entrega')
  echo "SUP-08 NO se dibuja sin sesión ($r): $m ocurrencia(s)"
  [ "$m" -eq 0 ] || { echo "   FALLA: el bloque se dibuja sin trabajo a la vista"; fails=$((fails+1)); }
done

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
