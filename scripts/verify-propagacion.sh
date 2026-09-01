#!/usr/bin/env bash
# ============================================================================
# verify-propagacion.sh — LA COMPUERTA DE LA DIRECCIÓN INVERSA.
#
# LA PREGUNTA QUE ESTE PRODUCTO NO SABÍA HACERSE. Todas sus verificaciones
# corren hacia adelante: si lo declarado está bien escrito, si las clases usadas
# están definidas, si los enlaces resuelven, si la cobertura alcanza. Ninguna
# pregunta lo inverso: **¿quedó algo diciendo lo contrario de lo que se decidió?**
#
# Está medido CINCO VECES en dos días, y ninguna es un error de juicio —las cinco
# decisiones son correctas, fechadas y registradas—:
#
#   D1  confirmada 08-26 -> 35 filas «Condicionado» en tres documentos
#   D3  reformulada 08-26 -> 7 apariciones de «no está declarado por ninguna fuente»
#   D4  decidida    08-20 -> 15 apariciones de «se ancla en la etapa a», cerrada el 08-13
#   D8  decidida    08-20 -> 17 apariciones de «exigible todavía»
#   PT-01.a medida  08-13 -> 10 apariciones en 9 documentos
#
# Es el hueco que el reporte 24 elevó al framework: `Root-Rules.md` §12.1 y §12.2
# obligan a declarar POR QUÉ EVENTO SE VOLVERÁ, y nada obliga al evento a volver.
#
# QUÉ LEE. `SDD/Docs/Audit/Decisiones-Y-Frases-Retiradas.md` §2, una fila por
# frase retirada, con su patrón, dónde puede vivir legítimamente y su estado.
#
# POR QUÉ EL `estado`, Y NO ES UN DETALLE. Sólo las filas `vigilada` hacen fallar
# la compuerta; las `pendiente` se informan. Sin esa distinción el guion nacería
# en rojo —las barridas todavía no se hicieron— y **una compuerta en rojo
# permanente es una compuerta apagada**: `C-3` estuvo CATORCE DÍAS informando un
# defecto real que nadie leía. Cada unidad documental pasa su fila a `vigilada`
# cuando termina su barrida, y a partir de ahí la frase no puede volver.
#
# QUÉ NO CUENTA COMO REAPARICIÓN, y por eso hay tres exclusiones:
#   · las filas de control de cambios, que son registro histórico y dicen lo que
#     se afirmó, no lo que rige;
#   · el texto tachado con `~~`, que es la forma en que este corpus deja ver un
#     desenlace sin borrar la pregunta;
#   · las rutas que la propia fila declara legítimas —típicamente `Audit/`—,
#     porque un informe que CITA la frase retirada para explicar que se retiró
#     no es una reaparición. Es el mismo falso positivo que la compuerta de
#     enlaces produce con `[etiqueta](destino)`, y que crece cada vez que alguien
#     lo documenta.
#
# Se corre desde la raíz del repositorio y no necesita .NET.
#
# CÓDIGOS DE SALIDA:
#   0  ninguna frase vigilada reapareció
#   1  al menos una reapareció
#   2  no se puede leer el registro
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."

REGISTRO=SDD/Docs/Audit/Decisiones-Y-Frases-Retiradas.md
[ -f "$REGISTRO" ] || { echo "NO SE PUEDE VERIFICAR · falta $REGISTRO" >&2; exit 2; }

fails=0
vigiladas=0
pendientes=0

# El corpus vivo: sin `_legacy/`, sin `PROMPTs/` —que es del Product Owner—, y sin
# el propio registro, que por definición nombra todas las frases.
corpus() {
  grep -rn -E "$1" SDD/Docs SDD/Intake --include='*.md' 2>/dev/null \
    | grep -v '_legacy' \
    | grep -v '/PROMPTs/' \
    | grep -v "^$REGISTRO:" \
    | grep -vE ':\| [0-9]+\.[0-9]+ \| 2026' \
    | grep -v '~~'
}

printf '== Frases retiradas, contra el corpus vivo ==\n\n'

# Las filas del registro: `| RE-NN | decisión | patrón | dónde | estado | dueño |`.
# Se leen con awk sobre el separador de celdas, y se limpian los acentos graves.
while IFS='|' read -r _ id _ patron permitido estado _; do
  id="$(printf '%s' "$id" | tr -d ' `')"
  patron="$(printf '%s' "$patron" | sed 's/^ *//;s/ *$//;s/^`//;s/`$//')"
  permitido="$(printf '%s' "$permitido" | tr -d ' `')"
  estado="$(printf '%s' "$estado" | tr -d ' `*')"

  [ -z "$patron" ] && continue

  hallazgos="$(corpus "$patron")"
  if [ -n "$permitido" ]; then
    hallazgos="$(printf '%s' "$hallazgos" | grep -v "^$permitido" || true)"
  fi
  cuantas="$(printf '%s' "$hallazgos" | grep -c . || true)"

  if [ "$estado" = "vigilada" ]; then
    vigiladas=$((vigiladas + 1))
    if [ "$cuantas" -eq 0 ]; then
      printf '  %-7s VIGILADA · sin reapariciones\n' "$id"
    else
      printf '  %-7s VIGILADA · REAPARECIÓ en %s lugar(es):\n' "$id" "$cuantas"
      printf '%s\n' "$hallazgos" | sed 's/^/            /'
      fails=$((fails + 1))
    fi
  else
    pendientes=$((pendientes + 1))
    printf '  %-7s pendiente · %s aparición(es) todavía vivas — las retira %s\n' \
      "$id" "$cuantas" "$(printf '%s' "$estado" | sed 's/pendiente//')"
  fi
done < <(grep -E '^\| `RE-[0-9]+`' "$REGISTRO")

printf '\n== RESULTADO ==\n'
printf '%s frase(s) vigilada(s), %s pendiente(s) de barrida.\n' "$vigiladas" "$pendientes"
if [ "$fails" -eq 0 ]; then
  echo "CONFORME · ninguna frase retirada volvió al corpus"
  exit 0
fi
echo "NO CONFORME · $fails frase(s) vigilada(s) reaparecieron"
echo "Una decisión tomada volvió a tener quien la contradiga. Ver $REGISTRO §1."
exit 1
