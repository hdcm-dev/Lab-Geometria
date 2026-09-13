#!/usr/bin/env bash
# Tres comprobaciones de Rules-Examples.md 6.6 §3.6 sobre un árbol ($1). Sale 1 si alguna falla.
R=${1:-.}; rc=0
echo "C-1 · toda carpeta samples/<seg>/<NN-…> tiene README.md"
n=0; for d in "$R"/samples/*/*/; do n=$((n+1)); [ -f "$d/README.md" ] || { echo "  FALTA README: ${d#$R/}"; rc=1; }; done; echo "  carpetas revisadas: $n"
echo "C-2 · ningún .csproj de sample tiene Exec o Target fuera de comentarios (verificación no enganchada a la construcción)"
for p in "$R"/samples/*/*/*.csproj; do if sed 's/<!--.*-->//' "$p" | perl -0pe 's/<!--.*?-->//gs' | grep -qE '<(Exec|Target)[ >]'; then echo "  ENGANCHE: ${p#$R/}"; rc=1; fi; done
echo "C-3 · los anfitriones del visor cargan el artefacto construido (visor/dist) y no una copia versionada"
for h in "$R"/samples/visor/*/index.html; do grep -q 'visor/dist/geometriafactory-visor.js' "$h" || { echo "  NO CARGA visor/dist: ${h#$R/}"; rc=1; }; done
c=$(git -C "$R" ls-files 'samples/**/geometriafactory-visor.js' | wc -l); [ "$c" -eq 0 ] || { echo "  COPIA VERSIONADA del bundle en samples: $c"; rc=1; }
echo "resultado: $([ $rc -eq 0 ] && echo CUMPLE || echo NO CUMPLE)"; exit $rc
