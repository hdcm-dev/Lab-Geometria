#!/usr/bin/env bash
# Aplica las propuestas de EXP-0001. NO corre sin la actuación de aprobación explícita del Product Owner.
#
#   bash …/propuestas/aplicar-propuestas.sh --aprobacion NNN --fase intake      # M2: P-01 (intake 4.6 → 5.0)
#   bash …/propuestas/aplicar-propuestas.sh --aprobacion NNN --fase manifiesto  # M3: P-02, después de confirmar el manifiesto
#   bash …/propuestas/aplicar-propuestas.sh --aprobacion NNN --fase docs        # M4: P-03, P-04, P-06, P-06b, P-08
#   … [--fecha AAAA-MM-DD]   fecha de la escritura (por omisión, hoy): va a las filas de control de cambios, a las marcas
#                            [CORREGIDO …] y a las carpetas _legacy/<fecha>/ de los snapshots; los nombres de archivo
#                            fechados (Mesa-2026-09-13.md, etc.) no se tocan.
#
# Guardas: aborta antes de escribir si falta la actuación (actuaciones/NNN-testimonio-aprobacion-*.md, con la
# palabra «aprob» y la fecha), si una fase anterior no está aplicada, o si un diff no aplica limpio.
set -euo pipefail
X="$(cd "$(dirname "$0")" && pwd)"; cd "$(git -C "$X" rev-parse --show-toplevel)"
APR=""; FASE=""; F="$(date +%F)"
while [ $# -gt 0 ]; do case "$1" in --aprobacion) APR="$2"; shift 2;; --fase) FASE="$2"; shift 2;; --fecha) F="$2"; shift 2;; *) echo "argumento desconocido: $1"; exit 2;; esac; done
echo "$F" | grep -qE "^20[0-9]{2}-[0-9]{2}-[0-9]{2}$" || { echo "ABORTA: --fecha inválida: $F"; exit 2; }
[ -n "$APR" ] && [ -n "$FASE" ] || { echo "ABORTA: hacen falta --aprobacion NNN y --fase intake|manifiesto|docs"; exit 2; }
ACT=$( (ls "$X"/../actuaciones/"$APR"-testimonio-aprobacion-*.md 2>/dev/null || true) | head -1)
[ -n "$ACT" ] || { echo "ABORTA: no existe la actuación $APR de aprobación del Product Owner (actuaciones/$APR-testimonio-aprobacion-*.md)"; exit 1; }
grep -qiE "\baprobad[oa]s?\b|\bapruebo\b|\baprobación\b" "$ACT" || { echo "ABORTA: la actuación $APR no contiene una aprobación"; exit 1; }
grep -qiE "\bno (lo |la |los |las )?apruebo\b|\bno aprobad[oa]s?\b|\bno se aprueba\b|\brechaz" "$ACT" && { echo "ABORTA: la actuación $APR contiene un rechazo o una negación de la aprobación"; exit 1; }
grep -qE "20[0-9]{2}-[0-9]{2}-[0-9]{2}" "$ACT" || { echo "ABORTA: la actuación $APR no está fechada"; exit 1; }
TMP="$(mktemp -d)"; trap 'rm -rf "$TMP"' EXIT
fechar() { sed -E "/^\+/ { s#_legacy/2026-09-13/#_legacy/$F/#g; s#\| 2026-09-13 \|#| $F |#g; s#CORREGIDO 2026-09-13#CORREGIDO $F#g }" "$X/$1.diff" > "$TMP/$1.diff"; echo "$TMP/$1.diff"; }
snap() { local f v d n; for f in $(git apply --numstat "$1" | awk '{print $3}'); do
  v=$(grep -m1 -oE '^\*\*Versión:\*\* [0-9.]+|^\| Versión \| (— \| )?[0-9.]+ \|' "$f" | grep -oE '[0-9]+\.[0-9]+' | tail -1)
  d="$(dirname "$f")/_legacy/$F"; n="$(basename "$f" .md)-v$v.md"; mkdir -p "$d"; [ -e "$d/$n" ] || cp "$f" "$d/$n"; done; }
aplicar() { local pd; pd="$(fechar "$1")"; git apply --check "$pd" || { echo "ABORTA: $1 no aplica limpio"; exit 1; }; snap "$pd"; git apply "$pd"; echo "aplicada $1 (fecha $F)"; }
case "$FASE" in
  intake)
    aplicar P-01-intake-5.0
    sed -i "s/{{ACTUACION_APROBACION}}/$APR/" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md
    grep -q "{{" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md && { echo "ABORTA: quedó un marcador sin completar"; exit 1; } || true ;;
  manifiesto)
    grep -q "^| Versión | 5.0 |" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md || { echo "ABORTA: el intake 5.0 no está aplicado (fase intake primero)"; exit 1; }
    aplicar P-02-manifiesto-7.0 ;;
  docs)
    grep -q "^| Versión | — | 7.0 |" SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md || { echo "ABORTA: el manifiesto 7.0 no está aplicado (fase manifiesto primero)"; exit 1; }
    for p in P-03-vista-producto-1.11 P-04-pipeline-producto-1.9 P-06-ciclo-de-origen-118-filas P-06b-ciclo-de-origen-8-cu P-08-apartamientos; do aplicar $p; done
    bash scripts/verify-solution-tree.sh ;;
  *) echo "fase desconocida: $FASE"; exit 2;;
esac
