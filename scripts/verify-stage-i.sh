#!/usr/bin/env bash
# ============================================================================
# verify-stage-i.sh — Puerta de los SIETE criterios de transición de la etapa
# `i` (`Roadmap-Producto.md` **1.9** §5.2, `i` → `j…`), que es la del PRIMER
# DESPLIEGUE REAL:
#
#   I-1  El front está publicado en el hosting por el flujo de FTP declarado, y
#        la publicación es REPRODUCIBLE: se corre dos veces y la segunda no
#        requiere ningún paso manual.
#   I-2  El servicio de datos corre en el servidor propio desde su composición,
#        con su imagen SELLADA CON LA REVISIÓN que efectivamente se construyó.
#   I-3  El front alcanza al servicio de datos SERVIDOR A SERVIDOR, y está
#        verificado que la sesión interactiva del navegador NO llega al
#        servicio de datos.
#   I-4  `PT-05` MEDIDA DESDE LA RED DE LA FACULTAD, con un alumno de verdad, y
#        su resultado documentado SEA CUAL SEA (RN-B1).
#   I-5  El circuito completo se recorre SOBRE EL DESPLIEGUE REAL: un alumno se
#        registra, el docente lo habilita, el alumno carga un trabajo, lo envía,
#        lo ve en tres dimensiones, y el docente lo aprueba con comentario.
#   I-6  Las actualizaciones de esquema se aplican solas sobre la base del
#        servidor propio, que hasta ese momento no existía.
#   I-7  Los guiones de puerta de las ocho fases anteriores SIGUEN PASANDO sobre
#        el árbol desplegado.
#
# ESTA PUERTA ES DISTINTA DE LAS OCHO ANTERIORES, y conviene decir en qué. Las
# otras miden el producto contra sí mismo y corren enteras en la máquina de
# quien construye. Ésta mide UN DESPLIEGUE QUE EXISTE: sin las dos direcciones
# reales no hay nada que medir, y el guion lo dice en vez de pasar en verde.
#
# DOS CRITERIOS NO SON MECÁNICOS Y EL GUION NO SIMULA QUE SÍ. `I-4` necesita una
# persona en la red de la facultad y `I-5` un circuito recorrido por dos
# personas sobre el despliegue real. Se DECLARAN, como `verify-stage-h.sh` hace
# con su `H-7`, y lo único que este guion comprueba de ellos es que su registro
# EXISTA — porque el criterio de `I-4` no es que la medición dé bien, es que
# esté documentada sea cual sea su resultado.
#
# NINGUNA DIRECCIÓN REAL VIVE ACÁ, por el mismo motivo que no vive en
# `deploy-front-ftp.yml`: llegan por entorno.
#
#   PUBLIC_URL   dirección pública del front, la que alcanza un alumno
#   API_URL      dirección del servicio de datos, tal como el front la usa
#
# USO:
#   PUBLIC_URL=https://… API_URL=http://… ./scripts/verify-stage-i.sh
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

fails=0
ok()      { printf '  \033[32mOK\033[0m      %s\n' "$1"; }
bad()     { printf '  \033[31mFALLA\033[0m   %s\n' "$1"; fails=$((fails + 1)); }
declara() { printf '  \033[33mDECLARA\033[0m %s\n' "$1"; }
nota()    { printf '          \033[2m%s\033[0m\n' "$1"; }

# `curl` con `--write-out` YA IMPRIME 000 cuando no llega, y además sale distinto
# de cero. Un `|| echo 000` encima imprimía «000000», que no es ningún código y
# se lee como un defecto del guion. La resolución vive acá y en un solo lugar.
codigo_http() {
  curl --silent --location --max-time 30 --output /dev/null \
       --write-out '%{http_code}' "$1" 2>/dev/null
}

echo "== Puerta de la etapa \`i\` → \`j…\` · siete criterios =="
echo

# ---------------------------------------------------------------------------
# LAS DOS DIRECCIONES. Sin ellas el guion no mide: se detiene nombrando la que
# falta, en vez de saltear en silencio los criterios que la necesitan.
#
# Es la misma corrección que `deploy-front-ftp.yml` incorporó el 2026-08-18: un
# secreto vacío que falla adentro de `curl` manda a buscar el problema al lugar
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
  echo "  PUBLIC_URL=https://… API_URL=http://… ./scripts/verify-stage-i.sh"
  exit 2
fi

command -v curl >/dev/null 2>&1 || {
  printf '\033[33mSIN MEDIR\033[0m · no hay `curl` en esta máquina\n'
  exit 3
}

# ---------------------------------------------------------------------------
# I-1 · el front publicado, por el flujo declarado y sin paso manual
#
# QUÉ SE MIDE Y QUÉ NO. Que la publicación sea reproducible se demuestra
# CORRIÉNDOLA DOS VECES, y eso es un acto, no una comprobación. Lo que sí es
# mecánico son sus dos condiciones: que la dirección pública responda, y que el
# flujo no tenga NINGÚN paso que espere a una persona. Un flujo con una
# aprobación intermedia no es reproducible por más veces que se lo corra.
# ---------------------------------------------------------------------------
echo "-- I-1 · el front publicado, y el flujo sin paso manual --"

flujo=".github/workflows/deploy-front-ftp.yml"

if [ -f "$flujo" ]; then
  ok "el flujo declarado existe: $flujo"
else
  bad "no existe $flujo, que es el instrumento que el criterio nombra"
fi

# `environment:` en un job es el mecanismo de GitHub para exigir una aprobación
# humana antes de seguir. Su presencia es exactamente lo que rompe I-1.
if [ -f "$flujo" ] && grep -qE '^\s{4}environment:' "$flujo"; then
  bad "el flujo declara un \`environment:\`, que puede exigir aprobación manual"
else
  ok "ningún paso del flujo espera a una persona"
fi

# Las dos puertas bloqueantes entraron el 2026-08-18 y son parte de que la
# publicación signifique algo: sin ellas sube igual con la batería en rojo.
for guion in "scripts/build.sh" "scripts/test.sh"; do
  if [ -f "$flujo" ] && grep -q "$guion" "$flujo"; then
    ok "el flujo corre $guion antes de subir"
  else
    bad "el flujo NO corre $guion (QG-01/QG-02, \`Pipeline-CI-CD.md\` §2.1)"
  fi
done

estado="$(codigo_http "$PUBLIC_URL")"

if [ "$estado" = "200" ]; then
  ok "la dirección pública responde 200"
elif [ "$estado" = "000" ]; then
  bad "la dirección pública NO SE ALCANZÓ, así que I-1 no se midió"
  nota "$PUBLIC_URL"
else
  bad "la dirección pública respondió $estado"
  nota "$PUBLIC_URL"
fi

echo

# ---------------------------------------------------------------------------
# I-2 · el servicio en el servidor propio, sellado con la revisión que corre
#
# ES EL CRITERIO QUE NACIÓ DE UN DEFECTO REAL. Hasta el 2026-08-16 la revisión
# entraba por un argumento escrito a mano: el código se actualizaba y el sello
# no, y `/salud` informaba una revisión que no era la suya SIN NINGÚN SÍNTOMA.
# Por eso acá no alcanza con que el servicio responda: hay que comparar lo que
# dice que corre contra lo que `main` tiene.
# ---------------------------------------------------------------------------
echo "-- I-2 · el servicio de datos, sellado con su revisión --"

salud="$(curl --silent --show-error --location --max-time 20 "${API_URL%/}/salud" 2>/dev/null || true)"

if [ -z "$salud" ]; then
  bad "el punto de salud no respondió"
  nota "${API_URL%/}/salud"
else
  ok "el punto de salud responde"

  # `Ready` es lo que `A-16` publica sobre el almacén: 200 con la preparación
  # terminada, 503 mientras no. Es también la evidencia de I-6.
  if printf '%s' "$salud" | grep -qiE '"ready"\s*:\s*true'; then
    ok "el almacén está preparado (\`ready\`: true)"
    preparado=1
  else
    bad "el servicio responde pero el almacén NO está preparado"
    preparado=0
  fi

  version="$(printf '%s' "$salud" | grep -oiP '"version"\s*:\s*"\K[^"]+' || true)"

  if [ -z "$version" ]; then
    bad "la salud no informa versión, así que la imagen no está sellada"
  else
    revision="${version##*+}"

    if [ "$revision" = "$version" ] || [ "$revision" = "desconocida" ]; then
      bad "la imagen NO lleva revisión sellada: informa «$version»"
      nota "falta BUILDKIT_CONTEXT_KEEP_GIT_DIR=1 en la construcción, o el .git no llegó"
    else
      esperado="$(git rev-parse origin/main 2>/dev/null || git rev-parse main 2>/dev/null || true)"

      if [ -z "$esperado" ]; then
        declara "la imagen informa la revisión $revision, sin \`main\` local contra qué compararla"
      elif [ "$revision" = "$esperado" ]; then
        ok "la revisión sellada es la de \`main\`: ${revision:0:12}"
      else
        bad "la revisión sellada NO es la de \`main\`"
        nota "corre ${revision:0:12} y \`main\` está en ${esperado:0:12}"
      fi
    fi
  fi
fi

echo

# ---------------------------------------------------------------------------
# I-3 · servidor a servidor, y el navegador sin acceso al servicio de datos
#
# LA PROPIEDAD SE SOSTIENE POR CONSTRUCCIÓN Y AUN ASÍ SE MIDE. El flujo inyecta
# la dirección en `publish/appsettings.json`, que es del lado del servidor:
# `wwwroot/` es lo único que el hosting sirve al navegador. Que hoy sea cierto
# no es motivo para no comprobarlo — mover ese archivo un directorio lo rompe
# sin que nada falle, y el síntoma sería que la dirección del servicio de datos
# queda a la vista de cualquiera.
# ---------------------------------------------------------------------------
echo "-- I-3 · el navegador no llega al servicio de datos --"

# El archivo de configuración no puede estar bajo `wwwroot/` en el fuente.
if find src/GeometriaFactory.Web/wwwroot -name 'appsettings*.json' 2>/dev/null | grep -q .; then
  bad "hay un appsettings bajo \`wwwroot/\`, que el hosting sirve al navegador"
else
  ok "no hay appsettings bajo \`wwwroot/\` en el fuente"
fi

# Y la comprobación que importa, contra el despliegue vivo: pedirlo como lo
# pediría cualquiera.
codigo_cfg="$(codigo_http "${PUBLIC_URL%/}/appsettings.json")"

# UN HOST QUE NO CONTESTA NO VERIFICA NADA, y darlo por bueno sería el defecto
# simétrico del que la etapa `f` corrigió en sus guiones: allá «sin medir» se
# reportaba como falla, acá se reportaría como conforme. El segundo es peor,
# porque afirma sobre el producto algo que nadie miró.
if [ "$codigo_cfg" = "000" ]; then
  bad "\`/appsettings.json\` NO SE PUDO MEDIR: la dirección pública no responde"
elif [ "$codigo_cfg" = "200" ]; then
  bad "\`/appsettings.json\` se sirve al navegador (respondió 200)"
  nota "ahí viaja la dirección del servicio de datos"
else
  ok "\`/appsettings.json\` no se sirve al navegador (respondió $codigo_cfg)"
fi

# El marcado que llega al navegador no debe nombrar al servicio de datos.
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
# I-6 · el esquema se aplica solo sobre una base que no existía
#
# NO TIENE COMPROBACIÓN PROPIA, Y ES CORRECTO QUE NO LA TENGA. `ready: true`
# significa que la fase 1 del arranque terminó, y la fase 1 es exactamente
# aplicar las transformaciones de esquema. Inventarle una segunda medición
# —listar tablas, contar migraciones— habría necesitado alcanzar la base del
# servidor propio desde acá, que es justo lo que la topología no permite.
# ---------------------------------------------------------------------------
echo "-- I-6 · el esquema aplicado sobre la base del servidor propio --"

if [ "${preparado:-0}" -eq 1 ]; then
  ok "la preparación del almacén terminó, que es lo que I-6 afirma"
  nota "evidencia: \`ready\`: true en el punto de salud, ya medido en I-2"
else
  bad "sin \`ready\`: true no hay evidencia de que el esquema se haya aplicado"
fi

echo

# ---------------------------------------------------------------------------
# I-7 · las ocho puertas anteriores siguen pasando
#
# SE DELEGA Y NO SE REIMPLEMENTA. Cada puerta sabe medir su etapa; repetir sus
# criterios acá habría creado un segundo lugar donde el criterio puede decir
# otra cosa, que es el defecto que `lib-puerta.sh` existe para no cometer.
# ---------------------------------------------------------------------------
echo "-- I-7 · las ocho puertas anteriores, sobre este árbol --"

puertas=(c d e f g h)
faltantes=0

for etapa in "${puertas[@]}"; do
  if [ ! -x "scripts/verify-stage-${etapa}.sh" ]; then
    bad "falta o no es ejecutable scripts/verify-stage-${etapa}.sh"
    faltantes=$((faltantes + 1))
  fi
done

# Las etapas `a` y `b` no tienen guion propio y nunca lo tuvieron: sus criterios
# los cubren `verify-navigation.sh` y `verify-visual-system.sh`, que son los que
# el registro de cambios nombra. Se comprueban por su nombre real.
for guion in verify-navigation verify-visual-system; do
  if [ ! -x "scripts/${guion}.sh" ]; then
    bad "falta o no es ejecutable scripts/${guion}.sh"
    faltantes=$((faltantes + 1))
  fi
done

if [ "$faltantes" -eq 0 ]; then
  ok "los ocho guiones de puerta existen y son ejecutables"
  declara "correrlos es el acto de I-7, y este guion NO los corre por vos"
  nota "son minutos de batería y un navegador en contenedor; se corren aparte"
fi

echo

# ---------------------------------------------------------------------------
# I-4 e I-5 · lo que no es mecánico
#
# EL CRITERIO DE I-4 NO ES QUE LA MEDICIÓN DÉ BIEN. El roadmap lo dice con todas
# las letras: el resultado se documenta SEA CUAL SEA, y si el acceso no funciona
# el número se registra igual y la topología se revisa. Una puerta que sólo
# admitiera el resultado bueno no mediría un riesgo: lo escondería.
#
# Por eso lo único que se comprueba acá es que EL REGISTRO EXISTA.
# ---------------------------------------------------------------------------
echo "-- I-4 e I-5 · los dos criterios no mecánicos --"

registro="SDD/Docs/Audit/Medicion-PT-05.md"

if [ -f "$registro" ] && grep -qi 'SIN MEDIR' "$registro"; then
  # EL FORMULARIO EN BLANCO NO PASA. Que el archivo exista prueba que la pregunta
  # está hecha, no que esté contestada, y I-4 pide el resultado documentado.
  bad "$registro existe pero sigue en \`SIN MEDIR\`"
  nota "I-4 pide el resultado, y el formulario todavía no lo tiene"
elif [ -f "$registro" ]; then
  ok "el registro de \`PT-05\` existe y ya no dice \`SIN MEDIR\`: $registro"
  declara "que su contenido sea la medición real es del Product Owner"
else
  bad "no existe $registro, y I-4 exige el resultado documentado sea cual sea"
  nota "la dirección usada y su fecha van adentro (ADR-14003 §consecuencias)"
fi

declara "I-5 · el circuito completo sobre el despliegue real lo recorren dos personas"
nota "registrarse, habilitar, cargar, enviar, ver en 3D y aprobar con comentario"

echo

# ---------------------------------------------------------------------------
if [ "$fails" -eq 0 ]; then
  printf 'CONFORME · los criterios mecánicos de la transición de la etapa \`i\` se verifican\n'
  printf '           I-4 e I-5 quedan DECLARADOS: son actos, no comprobaciones\n'
  exit 0
fi

printf 'NO CONFORME · %s comprobación(es) fallan\n' "$fails"
exit 1
