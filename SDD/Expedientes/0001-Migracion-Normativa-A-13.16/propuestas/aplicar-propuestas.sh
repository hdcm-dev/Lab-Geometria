#!/usr/bin/env bash
# Aplica las propuestas de EXP-0001 SÓLO con la aprobación explícita del Product Owner registrada en el expediente.
# Uso: bash SDD/Expedientes/0001-Migracion-Normativa-A-13.16/propuestas/aplicar-propuestas.sh
# Guardas: aborta antes de escribir si alguna propuesta no aplica limpia sobre el árbol actual.
set -euo pipefail
X="$(dirname "$0")"; cd "$(git -C "$X" rev-parse --show-toplevel)"
ORDEN=(P-01-intake-5.0 P-02-manifiesto-7.0 P-03-vista-producto-1.11 P-04-pipeline-producto-1.9 P-06-ciclo-de-origen-118-filas P-08-apartamientos)
for p in "${ORDEN[@]}"; do git apply --check "$X/$p.diff" || { echo "ABORTA: $p no aplica limpia"; exit 1; }; done
F=2026-09-13
for p in "${ORDEN[@]}"; do
  for f in $(git apply --numstat "$X/$p.diff" | awk '{print $3}'); do
    v=$(grep -m1 -oE '^\*\*Versión:\*\* [0-9.]+|^\| Versión \| (— \| )?[0-9.]+ \|' "$f" | grep -oE '[0-9]+\.[0-9]+' | tail -1)
    d="$(dirname "$f")/_legacy/$F"; n="$(basename "$f" .md)-v$v.md"
    mkdir -p "$d"; [ -e "$d/$n" ] || cp "$f" "$d/$n"
  done
done
for p in "${ORDEN[@]}"; do git apply "$X/$p.diff"; echo "aplicada $p"; done
bash scripts/verify-solution-tree.sh
