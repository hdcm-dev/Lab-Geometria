#!/usr/bin/env bash
# ============================================================================
# verify-visual-system.sh — Puerta del segundo criterio de transición de la
# etapa `b`: «La interfaz usa el sistema visual adoptado, sin estilos
# improvisados fuera de él» (`Roadmap-Producto.md`).
#
# Cuatro controles, y los cuatro son de pasa/falla:
#
#   C-1  Ningún literal de color en `app.css` fuera del bloque `:root`.
#   C-2  Ningún atributo `style=` en línea en ningún `.razor`.
#   C-3  Toda clase `gf-*` que un `.razor` usa está definida en `app.css`.
#   C-4  Los tokens de `app.css` son EXACTAMENTE los de la maqueta aprobada,
#        nombre por nombre y valor por valor.
#
# Se corre desde la raíz del repositorio y no necesita .NET.
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."

CSS=src/GeometriaFactory.Web/wwwroot/css/app.css
MAQUETA=SDD/Maquetas/GeometriaFactory-Web/assets/css/Estilos-Maqueta.css
RAZOR=src/GeometriaFactory.Web
fails=0

banner() { printf '\n== %s ==\n' "$1"; }

banner "C-1 · literales de color fuera de la definición de tokens"
# El bloque de tokens es el `:root { ... }` que abre la hoja. Se recorta y se
# busca color en TODO lo demás, comentarios incluidos.
root_end=$(grep -n '^}' "$CSS" | head -1 | cut -d: -f1)
fuera=$(tail -n "+$((root_end + 1))" "$CSS" | grep -nE '#[0-9A-Fa-f]{3,8}\b|\brgba?\(|\bhsla?\(')
if [ -z "$fuera" ]; then
  echo "CONFORME · 0 literales de color después de la línea $root_end (fin del bloque \`:root\`)"
else
  echo "NO CONFORME:"; echo "$fuera"; fails=$((fails + 1))
fi

banner "C-2 · atributos \`style=\` en línea"
inline=$(grep -rn 'style="' --include='*.razor' "$RAZOR")
if [ -z "$inline" ]; then
  echo "CONFORME · 0 atributos \`style=\` en $(find "$RAZOR" -name '*.razor' | wc -l) archivos .razor"
else
  echo "NO CONFORME:"; echo "$inline"; fails=$((fails + 1))
fi

banner "C-3 · clases usadas contra clases definidas"
grep -oE '\.gf-[a-zA-Z0-9_-]+' "$CSS" | sed 's/^\.//' | sort -u > /tmp/gf-definidas.txt
grep -rhoE 'class="[^"]*"' --include='*.razor' "$RAZOR" \
  | sed 's/class="//;s/"//' | tr ' ' '\n' | grep -E '^gf-' | sort -u > /tmp/gf-usadas.txt
# Las clases que un componente recibe por parámetro (`SizeClass`) también cuentan.
grep -rhoE '"gf-[a-zA-Z0-9_-]+"' --include='*.razor' "$RAZOR" | tr -d '"' | sort -u >> /tmp/gf-usadas.txt
sort -u -o /tmp/gf-usadas.txt /tmp/gf-usadas.txt
huerfanas=$(comm -23 /tmp/gf-usadas.txt /tmp/gf-definidas.txt)
if [ -z "$huerfanas" ]; then
  echo "CONFORME · $(wc -l < /tmp/gf-usadas.txt) clases usadas, todas definidas entre las $(wc -l < /tmp/gf-definidas.txt) de la hoja"
else
  echo "NO CONFORME · clases usadas y no definidas:"; echo "$huerfanas"; fails=$((fails + 1))
fi

banner "C-4 · tokens idénticos a los de la maqueta aprobada"
tokens() { sed -n '/^:root {/,/^}/p' "$1" | grep -oE '^\s+--[a-z0-9-]+:\s*[^;]+;' | sed 's/^\s*//;s/\s\+/ /g'; }
tokens "$MAQUETA" > /tmp/tok-maqueta.txt
tokens "$CSS"     > /tmp/tok-producto.txt
if diff -u /tmp/tok-maqueta.txt /tmp/tok-producto.txt > /tmp/tok.diff; then
  echo "CONFORME · $(wc -l < /tmp/tok-producto.txt) tokens, idénticos a los de la maqueta"
else
  echo "NO CONFORME:"; cat /tmp/tok.diff; fails=$((fails + 1))
fi

printf '\n== RESULTADO ==\n'
if [ "$fails" -eq 0 ]; then echo "CONFORME · los cuatro controles pasan"; exit 0; fi
echo "NO CONFORME · $fails control(es) fallan"; exit 1
