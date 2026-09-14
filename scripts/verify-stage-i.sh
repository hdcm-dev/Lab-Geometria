#!/usr/bin/env bash
# ============================================================================
# verify-stage-i.sh — Puerta de los SEIS criterios de transición de la etapa
# `i` (`Roadmap-Producto.md` §5.2, `i` → `j…`), que es la del DESPLIEGUE REAL.
# La cita no lleva número de versión del roadmap: la llevaba, y quedó apuntando
# a una emisión con siete criterios y un flujo de FTP que ya no publica.
#
#   I-1  El front y el servicio de datos corren en contenedores del servidor
#        propio, desde su composición, con imágenes SELLADAS con la revisión que
#        se construyó, publicados por túnel bajo dominio propio, y la publicación
#        es REPRODUCIBLE.
#   I-2  El front alcanza al servicio de datos SERVIDOR A SERVIDOR, y la sesión
#        interactiva del navegador NO llega al servicio de datos.
#   I-3  `PT-05` MEDIDA DESDE LA RED DE LA FACULTAD, con su resultado documentado
#        SEA CUAL SEA (RN-B1).
#   I-4  El circuito completo se recorre SOBRE EL DESPLIEGUE REAL.
#   I-5  Las actualizaciones de esquema se aplican solas sobre la base del
#        servidor propio.
#   I-6  Los guiones de puerta de las fases anteriores SIGUEN PASANDO sin
#        correcciones sobre el árbol desplegado.
#
# QUÉ CAMBIÓ EN LA REESCRITURA (deuda `DD-R8-1` de `Audit/Mesa-2026-09-13-ciclo-2.md`,
# ampliada por `R-11` de `Audit/Mesa-2026-09-14.md`). La versión anterior medía la
# topología vieja y quedaba NO CONFORME POR CONSTRUCCIÓN: `I-1` daba OK sobre un
# flujo de FTP retirado, `I-2` exigía que la revisión sellada fuera `origin/main`
# —y la estrategia de versionado no lo exige—, e `I-7` sólo comprobaba que las
# puertas existieran.
#
# ESTA PUERTA ES DISTINTA DE LAS OCHO ANTERIORES, y conviene decir en qué. Las
# otras miden el producto contra sí mismo y corren enteras en la máquina de
# quien construye. Ésta mide UN DESPLIEGUE QUE EXISTE: sin las dos direcciones
# reales no hay nada que medir, y el guion lo dice en vez de pasar en verde.
#
# DOS CRITERIOS NO SON MECÁNICOS Y EL GUION NO SIMULA QUE SÍ. `I-3` necesita una
# persona en la red de la facultad e `I-4` un circuito recorrido por dos
# personas sobre el despliegue real. Se DECLARAN, como `verify-stage-h.sh` hace
# con su `H-7`, y lo único que este guion comprueba de ellos es que su registro
# EXISTA — porque el criterio de `I-4` no es que la medición dé bien, es que
# esté documentada sea cual sea su resultado.
#
# NINGUNA DIRECCIÓN REAL VIVE ACÁ, por el mismo motivo que no vive en
# ningún archivo versionado: llegan por entorno.
#
#   PUBLIC_URL          dirección pública del front, la que alcanza un alumno
#   API_URL             dirección pública del servicio de datos
#   REVISION_ESPERADA   opcional: el commit que se quiso desplegar. Sin ella, la
#                       revisión sellada tiene que ser ALCANZABLE desde `main`
#                       —estar en su historia—, porque cuál rige, si la etiqueta
#                       o `main`, es una decisión abierta del Product Owner
#                       (`Guia-Publicacion-Image-Docker.md` §2.2)
#
# USO:
#   PUBLIC_URL=https://… API_URL=https://… [REVISION_ESPERADA=<sha>] ./scripts/verify-stage-i.sh
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

fails=0
ok()      { printf '  \033[32mOK\033[0m      %s\n' "$1"; }
bad()     { printf '  \033[31mFALLA\033[0m   %s\n' "$1"; fails=$((fails + 1)); }
declara() { printf '  \033[33mDECLARA\033[0m %s\n' "$1"; }
nota()    { printf '          \033[2m%s\033[0m\n' "$1"; }
sinmedir=0
sin_medir() { printf '  \033[33mSIN MEDIR\033[0m %s\n' "$1"; sinmedir=$((sinmedir + 1)); }

# `curl` con `--write-out` YA IMPRIME 000 cuando no llega, y además sale distinto
# de cero. Un `|| echo 000` encima imprimía «000000», que no es ningún código y
# se lee como un defecto del guion. La resolución vive acá y en un solo lugar.
codigo_http() {
  curl --silent --location --max-time 30 --output /dev/null \
       --write-out '%{http_code}' "$1" 2>/dev/null
}

echo "== Puerta de la etapa \`i\` → \`j…\` · seis criterios =="
echo

# ---------------------------------------------------------------------------
# LAS DOS DIRECCIONES. Sin ellas el guion no mide: se detiene nombrando la que
# falta, en vez de saltear en silencio los criterios que la necesitan.
#
# Un valor vacío que falla adentro de `curl` manda a buscar el problema al lugar
# equivocado.
# ---------------------------------------------------------------------------
faltan=""
[ -n "${PUBLIC_URL:-}" ] || faltan="${faltan} PUBLIC_URL"
[ -n "${API_URL:-}" ]    || faltan="${faltan} API_URL"

if [ -n "$faltan" ]; then
  printf '\033[31mNO SE PUEDE MEDIR\033[0m · falta(n) la(s) variable(s):%s\n' "$faltan"
  echo
  echo "Esta puerta mide un despliegue real. Sin sus direcciones no hay nada que"
  echo "verificar, y pasar en verde sería afirmar que se midió lo que no se miró."
  echo
  echo "  PUBLIC_URL=https://… API_URL=https://… ./scripts/verify-stage-i.sh"
  exit 2
fi

command -v curl >/dev/null 2>&1 || {
  printf '\033[33mSIN MEDIR\033[0m · no hay `curl` en esta máquina\n'
  exit 3
}

# ---------------------------------------------------------------------------
# I-1 · contenedores desde la composición, imágenes selladas, túnel y dominio
#
# QUÉ SE MIDE Y QUÉ NO. Que los contenedores corran desde la composición del
# servidor no se ve desde afuera; lo que sí se ve es su consecuencia: las dos
# direcciones públicas responden por el túnel, el servicio de datos informa una
# versión SELLADA con su revisión, y el front sella la MISMA versión, que es la
# prueba de que las dos imágenes salieron de la misma construcción. Que la
# publicación sea reproducible es un acto —correrla dos veces— y se declara.
# ---------------------------------------------------------------------------
echo "-- I-1 · las dos piezas publicadas, selladas con la misma revisión --"

estado="$(codigo_http "$PUBLIC_URL")"
case "$estado" in
  200) ok "la dirección pública del front responde 200" ;;
  000) bad "la dirección pública del front NO SE ALCANZÓ"; nota "$PUBLIC_URL" ;;
  *)   bad "la dirección pública del front respondió $estado"; nota "$PUBLIC_URL" ;;
esac

case "$PUBLIC_URL$API_URL" in
  *http://*) bad "alguna dirección no es https: el túnel publica bajo dominio propio con certificado" ;;
  *)         ok "las dos direcciones son https" ;;
esac

salud="$(curl --silent --show-error --location --max-time 20 "${API_URL%/}/salud" 2>/dev/null || true)"
version=""
preparado=0

if [ -z "$salud" ]; then
  bad "el punto de salud del servicio de datos no respondió"
  nota "${API_URL%/}/salud"
else
  ok "el punto de salud del servicio de datos responde"
  printf '%s' "$salud" | grep -qiE '"ready"\s*:\s*true' && preparado=1
  version="$(printf '%s' "$salud" | grep -oiP '"version"\s*:\s*"\K[^"]+' || true)"
fi

if [ -n "$version" ]; then
  revision="${version##*+}"
  if [ "$revision" = "$version" ] || [ "$revision" = "desconocida" ]; then
    bad "la imagen del servicio de datos NO lleva revisión sellada: informa «$version»"
    nota "falta BUILDKIT_CONTEXT_KEEP_GIT_DIR=1 en la construcción, o el .git no llegó"
  elif [ -n "${REVISION_ESPERADA:-}" ]; then
    if [ "${revision:0:${#REVISION_ESPERADA}}" = "$REVISION_ESPERADA" ] || [ "${REVISION_ESPERADA:0:${#revision}}" = "$revision" ]; then
      ok "la revisión sellada es la esperada: ${revision:0:12}"
    else
      bad "la revisión sellada NO es la esperada: corre ${revision:0:12}, se esperaba ${REVISION_ESPERADA:0:12}"
    fi
  else
    git fetch --quiet origin main 2>/dev/null || true
    base="$(git rev-parse --verify --quiet origin/main || git rev-parse --verify --quiet main || true)"
    if [ -z "$base" ]; then
      declara "la imagen informa la revisión ${revision:0:12}, sin \`main\` local contra qué ubicarla"
    elif git merge-base --is-ancestor "$revision" "$base" 2>/dev/null; then
      ok "la revisión sellada ${revision:0:12} está en la historia de \`main\`"
      [ "$revision" = "$base" ] || nota "\`main\` ya avanzó a ${base:0:12}: lo desplegado es anterior, y eso no es una falla"
    else
      bad "la revisión sellada ${revision:0:12} NO está en la historia de \`main\`"
      nota "o se desplegó una rama sin fusionar, o falta traer \`main\`"
    fi
  fi

  # El front sella su versión en el marcado (`VersionSeal`, R-14). Tiene que ser la
  # misma que el servicio de datos: las dos imágenes se construyen de la misma revisión.
  legible="${version%%+*}"
  marcado_ingreso="$(curl --silent --location --max-time 30 "${PUBLIC_URL%/}/ingreso" 2>/dev/null || true)"
  sello="$(printf '%s' "$marcado_ingreso" | grep -oP 'Versión \K[0-9][^<[:space:]]*' | head -1 || true)"
  if [ -z "$sello" ]; then
    bad "el front no sella ninguna versión en \`/ingreso\`"
  elif [ "$sello" = "$legible" ]; then
    ok "el front sella la misma versión que el servicio de datos: $sello"
  else
    bad "el front sella $sello y el servicio de datos $legible: no salieron de la misma construcción"
  fi
elif [ -n "$salud" ]; then
  bad "la salud no informa versión, así que la imagen no está sellada"
fi

declara "que la publicación es reproducible se demuestra corriéndola dos veces (\`Guia-Publicacion-Image-Docker.md\` §2.2)"

echo

# ---------------------------------------------------------------------------
# I-2 · servidor a servidor, y el navegador sin acceso al servicio de datos
#
# La dirección que el front usa para hablar con el servicio de datos es de su
# servidor. El navegador no la recibe: ni en un archivo servido ni en el marcado.
# ---------------------------------------------------------------------------
echo "-- I-2 · el navegador no llega al servicio de datos por el front --"

if find src/GeometriaFactory.Web/wwwroot -name 'appsettings*.json' 2>/dev/null | grep -q .; then
  bad "hay un appsettings bajo \`wwwroot/\`, que se sirve al navegador"
else
  ok "no hay appsettings bajo \`wwwroot/\` en el fuente"
fi

codigo_cfg="$(codigo_http "${PUBLIC_URL%/}/appsettings.json")"
if [ "$codigo_cfg" = "000" ]; then
  bad "\`/appsettings.json\` NO SE PUDO MEDIR: la dirección pública no responde"
elif [ "$codigo_cfg" = "200" ]; then
  bad "\`/appsettings.json\` se sirve al navegador (respondió 200)"
else
  ok "\`/appsettings.json\` no se sirve al navegador (respondió $codigo_cfg)"
fi

maquina_api="$(printf '%s' "$API_URL" | sed -E 's#^[a-z]+://##; s#/.*$##; s#:.*$##')"
marcado="$(curl --silent --location --max-time 30 "$PUBLIC_URL" 2>/dev/null || true)"
if [ -z "$marcado" ]; then
  bad "no se pudo leer el marcado servido, y sin él este criterio no se mide"
elif printf '%s' "$marcado" | grep -qF "$maquina_api"; then
  bad "el marcado servido nombra al servicio de datos ($maquina_api)"
else
  ok "el marcado servido no nombra al servicio de datos"
fi

echo

# ---------------------------------------------------------------------------
# I-5 · el esquema se aplica solo
#
# `ready: true` significa que la fase 1 del arranque terminó, y la fase 1 es
# aplicar las transformaciones de esquema (`ADR-00007`).
# ---------------------------------------------------------------------------
echo "-- I-5 · el esquema aplicado sobre la base del servidor propio --"

if [ "$preparado" -eq 1 ]; then
  ok "la preparación del almacén terminó (\`ready\`: true), que es lo que I-5 afirma"
else
  bad "sin \`ready\`: true no hay evidencia de que el esquema se haya aplicado"
fi

echo

# ---------------------------------------------------------------------------
# I-6 · las puertas anteriores SIGUEN PASANDO, y se corren
#
# SE DELEGA Y NO SE REIMPLEMENTA: cada puerta sabe medir su etapa. Lo que cambió
# con `R-11` es que ahora SE CORREN: comprobar que existen no dice que pasen.
# NO SE PUDO MEDIR NO ES FALLA: sin `dotnet` o sin `docker` la puerta se declara
# sin medir, y el resultado de esta puerta queda INCOMPLETO, no conforme.
# ---------------------------------------------------------------------------
echo "-- I-6 · las puertas anteriores, corridas sobre este árbol --"

tiene() { command -v "$1" >/dev/null 2>&1; }

correr_puerta() {
  local guion="$1"; shift
  local falta=""
  for herramienta in "$@"; do tiene "$herramienta" || falta="$falta $herramienta"; done
  if [ -n "$falta" ]; then
    sin_medir "$guion: este entorno no tiene$falta"
    return
  fi
  local registro="/tmp/puerta-i-$(basename "$guion" .sh).log"
  bash "scripts/$guion" >"$registro" 2>&1
  case $? in
    0) ok "$guion pasa" ;;
    2|3) sin_medir "$guion no pudo medir; ver $registro" ;;
    *) bad "$guion NO pasa; ver $registro" ;;
  esac
}

correr_puerta verify-navigation.sh dotnet curl
correr_puerta verify-visual-system.sh
correr_puerta verify-stage-c.sh dotnet curl
correr_puerta verify-stage-d.sh dotnet
correr_puerta verify-stage-e.sh dotnet
correr_puerta verify-stage-f.sh dotnet docker
correr_puerta verify-stage-g.sh dotnet docker
correr_puerta verify-stage-h.sh dotnet

echo

# ---------------------------------------------------------------------------
# I-3 e I-4 · lo que no es mecánico
#
# EL CRITERIO DE I-3 NO ES QUE LA MEDICIÓN DÉ BIEN: el resultado se documenta
# SEA CUAL SEA. Lo único que se comprueba acá es que EL REGISTRO EXISTA y ya no
# esté en blanco.
# ---------------------------------------------------------------------------
echo "-- I-3 e I-4 · los dos criterios no mecánicos --"

registro="SDD/Docs/Audit/Medicion-PT-05.md"

if [ -f "$registro" ] && grep -qi 'SIN MEDIR' "$registro"; then
  bad "$registro existe pero sigue en \`SIN MEDIR\`"
  nota "I-3 pide el resultado, y el formulario todavía no lo tiene"
elif [ -f "$registro" ]; then
  ok "el registro de \`PT-05\` existe y ya no dice \`SIN MEDIR\`: $registro"
  declara "que su contenido sea la medición real es del Product Owner"
else
  bad "no existe $registro, y I-3 exige el resultado documentado sea cual sea"
fi

declara "I-4 · el circuito completo sobre el despliegue real lo recorren dos personas"
nota "registrarse, habilitar, cargar, enviar, ver en 3D y aprobar con comentario"

echo

# ---------------------------------------------------------------------------
if [ "$fails" -eq 0 ] && [ "$sinmedir" -gt 0 ]; then
  printf 'INCOMPLETA · ninguna comprobación falla, y %s quedaron sin medir en este entorno\n' "$sinmedir"
  exit 2
fi

if [ "$fails" -eq 0 ]; then
  printf 'CONFORME · los criterios mecánicos de la transición de la etapa \`i\` se verifican\n'
  printf '           I-3 e I-4 quedan DECLARADOS: son actos, no comprobaciones\n'
  exit 0
fi

printf 'NO CONFORME · %s comprobación(es) fallan\n' "$fails"
exit 1
