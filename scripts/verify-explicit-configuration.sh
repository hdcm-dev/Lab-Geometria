#!/usr/bin/env bash
# ============================================================================
# verify-explicit-configuration.sh — Puerta de la regla de configuración
# explícita del repositorio.
#
# LA REGLA, entera, en una línea: **la configuración de compilación se declara
# siempre y en los dos lados —el que construye y el que ejecuta— aunque el valor
# por omisión coincida.**
#
# POR QUÉ ES UNA PUERTA Y NO UNA RECOMENDACIÓN. El defecto que la motiva ya
# costó una conclusión equivocada sobre el producto. `verify-stage-c.sh`
# construía con `-c Release` y levantaba las dos piezas con `--no-build` sin
# decir la configuración, de modo que se levantaba la salida de `Debug` —de
# cualquier antigüedad— en vez de lo que se acababa de construir. No falló
# mientras lo medido existió en las dos salidas; el día que se agregó una
# guardia de arranque nueva, el binario viejo arrancó igual y se concluyó
# durante un rato que la guardia no funcionaba. Funcionaba.
#
# La coherencia POR OMISIÓN no cuenta como cumplimiento. Un guion que construye
# sin decir la configuración y ejecuta sin decir la configuración hoy acierta;
# el día que alguien agregue `-c Release` de un solo lado, reproduce el defecto
# entero sin que nada avise.
#
# DOS CONTROLES, los dos de pasa/falla:
#
#   C-1  Toda invocación que construya, ejecute, pruebe o publique declara su
#        configuración (`-c`, `--configuration` o `-p:Configuration=`).
#   C-2  Todo `--no-build` está precedido, en el mismo archivo, por una llamada
#        a `scripts/assert-build-fresh.sh`, que es la red contra levantar una
#        salida más vieja que las fuentes.
#
# Se corre desde la raíz del repositorio y no necesita .NET:
#   bash scripts/verify-explicit-configuration.sh
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."

fails=0
banner() { printf '\n== %s ==\n' "$1"; }

# El alcance son los archivos VERSIONADOS que ejecutan algo. La documentación queda afuera a
# propósito: un ejemplo dentro de un `.md` no construye ni levanta nada, y meterlo acá volvería
# la puerta ruidosa sin volverla más segura.
targets() {
  git ls-files \
    'scripts/*.sh' \
    '.github/workflows/*' \
    'deploy/Dockerfile' \
    'deploy/*.yaml' \
    '.vscode/*.json' \
    '.devcontainer/*.json' 2>/dev/null | sort
}

# Las invocaciones se parten en varias líneas físicas con `\` al final. Se reconstruyen en
# líneas lógicas antes de mirarlas, o `--configuration` en la línea siguiente se leería como
# ausente y la puerta gritaría por algo que está bien.
logical_lines() { # archivo  ->  <linea-inicial>:<contenido>
  awk '
    {
      line = $0
      sub(/[ \t]*$/, "", line)
      if (buffer == "") start = NR
      if (line ~ /\\$/) { sub(/\\$/, "", line); buffer = buffer line " "; next }
      print start ":" buffer line
      buffer = ""
    }
    END { if (buffer != "") print start ":" buffer }
  ' "$1"
}

is_comment() { case "$1" in [[:space:]]*\#* | \#*) return 0 ;; *) return 1 ;; esac; }

# ------------------------------------------------------------------ C-1 -----
banner "C-1 · la configuración va declarada en toda invocación"

# El verbo se arma por partes a propósito: así este archivo no contiene ninguna de las cadenas
# que busca y no se denuncia a sí mismo.
verb='dotnet[[:space:]]+(build|run|test|publish|ef)([[:space:]]|$)'
# El valor puede venir literal (`Release`), entrecomillado o por variable (`"$CONFIGURATION"`):
# lo que la puerta exige es que la configuración esté DICHA, no que esté escrita a mano.
declared="(^|[[:space:]])(-c|--configuration)[[:space:]]+[\"'\$A-Za-z]|(^|[[:space:]])-p:Configuration=|[Cc]onfiguration=[\$\"A-Za-z]"

scanned=0
c1_findings=""
for file in $(targets); do
  while IFS= read -r entry; do
    number="${entry%%:*}"
    content="${entry#*:}"
    is_comment "$content" && continue
    echo "$content" | grep -qE "$verb" || continue
    scanned=$((scanned + 1))
    echo "$content" | grep -qE "$declared" && continue
    c1_findings="${c1_findings}${file}:${number}: ${content}"$'\n'
  done < <(logical_lines "$file")
done

if [ -z "$c1_findings" ]; then
  echo "CONFORME · $scanned invocación(es) en $(targets | wc -l) archivo(s), todas con su configuración declarada"
else
  echo "NO CONFORME · invocaciones sin configuración declarada:"
  printf '%s' "$c1_findings" | sed 's/^/    /'
  echo "    Agregá \`-c Debug\` o \`-c Release\` según cuál sea la salida que esa invocación produce o consume."
  fails=$((fails + 1))
fi

# ------------------------------------------------------------------ C-2 -----
banner "C-2 · todo \`--no-build\` con su red puesta"

c2_findings=""
guarded=0
for file in $(targets); do
  first_no_build=""
  guard=""
  while IFS= read -r entry; do
    number="${entry%%:*}"
    content="${entry#*:}"
    is_comment "$content" && continue
    case "$content" in
      *assert-build-fresh.sh*) [ -z "$guard" ] && guard="$number" ;;
    esac
    case "$content" in
      *--no-build*) [ -z "$first_no_build" ] && first_no_build="$number" ;;
    esac
  done < <(logical_lines "$file")

  [ -z "$first_no_build" ] && continue
  if [ -n "$guard" ] && [ "$guard" -lt "$first_no_build" ]; then
    guarded=$((guarded + 1))
  else
    c2_findings="${c2_findings}${file}:${first_no_build}: \`--no-build\` sin llamada previa a scripts/assert-build-fresh.sh"$'\n'
  fi
done

if [ -z "$c2_findings" ]; then
  echo "CONFORME · $guarded archivo(s) levantan con \`--no-build\` y los $guarded pasan antes por la red"
else
  echo "NO CONFORME · \`--no-build\` sin red:"
  printf '%s' "$c2_findings" | sed 's/^/    /'
  echo "    Agregá, antes de levantar:"
  echo "        scripts/assert-build-fresh.sh <ruta-al-csproj> <configuracion> || exit 1"
  fails=$((fails + 1))
fi

printf '\n== RESULTADO ==\n'
if [ "$fails" -eq 0 ]; then echo "CONFORME · los dos controles pasan"; exit 0; fi
echo "NO CONFORME · $fails control(es) fallan"; exit 1
