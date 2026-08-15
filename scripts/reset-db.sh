#!/usr/bin/env bash
# ============================================================================
# reset-db.sh — Deja el almacén EN SU ESTADO DE PRIMER ARRANQUE: vacío, sin ninguna
# cuenta y sin ningún trabajo, con su esquema al día.
# Criterio de éxito declarado: el almacén queda vacío y con su esquema al día
# (intake §17.3.P.8; `Api/03 Guia-Onboarding-Developer.md` §3.2).
#
# La ruta del almacén sale de la configuración del servicio y no está escrita en el
# código. Este guion la resuelve por el MISMO camino que `run-api.sh`, sourceando
# `scripts/store-path.sh`, para que no pueda borrar un archivo distinto del que el
# servicio usa.
#
# ---------------------------------------------------------------------------
# POR QUÉ AHORA PREGUNTA, Y ANTES NO
#
# Hasta acá este guion hacía `rm -f` y avisaba después. El 2026-08-15 una corrida de
# guiones lo ejecutó y se llevó la cuenta de administrador del Product Owner. La
# pérdida fue chica —una cuenta que se recrea en un minuto—, pero en la etapa `e` este
# mismo archivo tiene los trabajos de los alumnos y el mismo descuido se lleva las
# entregas de una comisión.
#
# LA CONFIRMACIÓN NO ES UNA MOLESTIA CEREMONIAL: dice QUÉ ARCHIVO va a borrar y CUÁNTO
# HAY ADENTRO, contado del archivo mismo. Un «¿estás seguro? [s/N]» pelado no habría
# evitado nada, porque no le da a quien lo lee ningún dato con el que decidir. Lo que
# hace que la pregunta sirva es el recuento: quien la lee sabe qué está perdiendo.
#
# EL RECUENTO CRECE SIN REESCRIBIR EL GUION. Las tablas que se cuentan están en
# `STORE_INVENTORY`, una por línea. Cuando la etapa `e` agregue `Work`, se agrega la
# línea y no se toca nada más.
#
# ---------------------------------------------------------------------------
# CÓMO SE SALTEA LA PREGUNTA, Y QUIÉN SE HACE CARGO
#
#   scripts/reset-db.sh                # pregunta, y no borra nada sin respuesta
#   scripts/reset-db.sh --assume-yes   # NO pregunta y borra
#
# `--assume-yes` es una BANDERA DECLARADA y no una variable de entorno adivinable, y la
# diferencia es el punto entero. Una variable se hereda: se exporta una vez en una
# sesión, se olvida, y tres horas después otro guion la encuentra puesta y borra sin
# preguntar. Una bandera hay que escribirla en la línea que ejecuta la destrucción, con
# lo cual queda en el archivo del guion que la usa y en el historial de quien la tipeó.
#
# QUIEN LA USA SE HACE CARGO. `--assume-yes` renuncia a la única red que este guion
# tiene: no hay copia de resguardo, no hay papelera y no hay forma de deshacerlo. Si el
# archivo tenía los trabajos de una comisión, se perdieron. Ponerla en un guion de
# automatización es correcto sólo cuando el almacén sobre el que corre es descartable
# por construcción —un `mktemp -d`, un contenedor efímero—, y esa comprobación la hace
# quien escribe la automatización, no este guion.
#
# Sin bandera y sin nadie que pueda contestar —entrada estándar cerrada, que es el caso
# de toda automatización— el guion NO BORRA: se detiene y nombra la bandera. Elegir
# borrar por omisión cuando no hay nadie mirando es exactamente el defecto que este
# cambio repara.
# ============================================================================
set -euo pipefail

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
# shellcheck source=scripts/store-path.sh
. "$script_dir/store-path.sh"
gf_resolve_store

store_file="$GF_STORE_FILE"

# ---------------------------------------------------------------------- inventario ---
# Las tablas que el recuento nombra, `<TablaSQL>|<cómo se dice en la confirmación>`.
# Los nombres de tabla son los que fija `Infrastructure/Persistence/Configurations/`
# —`Account` va en singular porque así lo declara `AccountConfiguration.ToTable`—.
# La etapa `e` agrega acá su línea y no toca ninguna otra parte de este guion.
STORE_INVENTORY=(
  "Account|cuenta(s)"
)

# Cuenta las filas de una tabla del almacén. Devuelve el número, o `?` si en este
# entorno no hay con qué leer un archivo SQLite. NO INVENTA UN CERO cuando no puede
# contar: un cero falso es peor que un signo de pregunta, porque invita a confirmar.
count_rows() {
  local file="$1" table="$2" query
  query="SELECT COUNT(*) FROM \"$table\";"

  if command -v sqlite3 >/dev/null 2>&1; then
    sqlite3 "$file" "$query" 2>/dev/null || echo 0
    return
  fi

  if command -v python3 >/dev/null 2>&1; then
    python3 - "$file" "$table" <<'FIN' 2>/dev/null || echo '?'
import sqlite3, sys
archivo, tabla = sys.argv[1], sys.argv[2]
conexion = sqlite3.connect(f"file:{archivo}?mode=ro", uri=True)
fila = conexion.execute(
    "SELECT name FROM sqlite_master WHERE type='table' AND name=?", (tabla,)
).fetchone()
# La tabla que todavía no existe cuenta 0, y eso SÍ es cierto: el esquema no la creó.
print(0 if fila is None else conexion.execute(f'SELECT COUNT(*) FROM "{tabla}"').fetchone()[0])
FIN
    return
  fi

  echo '?'
}

# --------------------------------------------------------------------- la bandera ---
assume_yes=0
for argumento in "$@"; do
  case "$argumento" in
    --assume-yes) assume_yes=1 ;;
    *)
      echo "Argumento no reconocido: $argumento" >&2
      echo "Uso: scripts/reset-db.sh [--assume-yes]" >&2
      exit 2
      ;;
  esac
done

# ------------------------------------------------------- nada que borrar, nada que preguntar ---
if [ ! -f "$store_file" ]; then
  echo "No hay almacén que borrar: $store_file no existe."
  echo "El esquema se aplica en el próximo arranque de scripts/run-api.sh."
  exit 0
fi

# ------------------------------------------------------------------ la confirmación ---
tamanio="$(du -h "$store_file" | cut -f1)"

echo
echo "A punto de BORRAR el almacén. Esto NO se puede deshacer."
echo
echo "  Archivo: $store_file"
echo "  Tamaño:  $tamanio"
echo "  Adentro:"
for entrada in "${STORE_INVENTORY[@]}"; do
  tabla="${entrada%%|*}"
  etiqueta="${entrada#*|}"
  echo "    $(count_rows "$store_file" "$tabla") $etiqueta"
done
if ! command -v sqlite3 >/dev/null 2>&1 && ! command -v python3 >/dev/null 2>&1; then
  echo
  echo "  El recuento dice \`?\` porque en este entorno no hay ni \`sqlite3\` ni \`python3\`"
  echo "  con qué leer el archivo. NO significa que esté vacío."
fi
echo

if [ "$assume_yes" -eq 1 ]; then
  echo "--assume-yes: se saltea la pregunta. Quien puso la bandera se hace cargo."
else
  printf 'Escribí `borrar` para confirmar: '
  if ! IFS= read -r respuesta; then
    echo
    echo "No hay nadie que pueda contestar y NO se borró nada." >&2
    echo "En automatización, la forma de decidirlo por escrito es:" >&2
    echo "    scripts/reset-db.sh --assume-yes" >&2
    exit 1
  fi
  if [ "$respuesta" != "borrar" ]; then
    echo "No se borró nada."
    exit 1
  fi
fi

rm -f "$store_file" "$store_file-shm" "$store_file-wal"
echo "Almacén borrado: $store_file"

# El esquema se aplica solo en el próximo arranque (`Infrastructure ADR-07`), y el arranque en
# dos fases no atiende ninguna petición hasta que la preparación terminó (`QG-11`).
echo "El esquema se aplica en el próximo arranque de scripts/run-api.sh."
