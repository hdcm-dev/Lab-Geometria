#!/usr/bin/env bash
# ============================================================================
# Verificación del sample `api/04-cliente-http-basico` contra su snapshot.
#
# `bash verificar.sh <archivo-de-salida>` compara, renglón a renglón, las
# líneas que `run.sh` produce con `esperado/salida.txt`, que es la
# transcripción de §6 del documento que gobierna esta carpeta. `run.sh` lo
# invoca al final; también corre solo sobre una salida guardada.
#
# LAS DIVERGENCIAS DECLARADAS SE NOMBRAN UNA POR UNA, con el número de renglón
# y el motivo. El snapshot no se reescribe para que la corrida dé CONFORME:
# eso convertiría al sample en una copia de sí mismo.
# ============================================================================
set -uo pipefail

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
salida="${1:-}"
if [ -z "$salida" ] || [ ! -f "$salida" ]; then
  echo "Uso: bash verificar.sh <archivo con la salida de run.sh>" >&2
  exit 2
fi

# SIN DIVERGENCIAS DECLARADAS.
declare -A divergencias=()

mapfile -t esperadas < "$aqui/esperado/salida.txt"
mapfile -t obtenidas < "$salida"

declaradas=0; no_declaradas=0
detalle=""
total=${#esperadas[@]}
[ ${#obtenidas[@]} -gt "$total" ] && total=${#obtenidas[@]}

for ((i = 0; i < total; i++)); do
  e="${esperadas[i]:-(línea de más)}"
  p="${obtenidas[i]:-(línea ausente)}"
  [ "$e" = "$p" ] && continue
  n=$((i + 1))
  if [ -n "${divergencias[$n]:-}" ]; then
    declaradas=$((declaradas + 1))
    detalle+="  línea $n — DIVERGENCIA DECLARADA · ${divergencias[$n]}"$'\n'
    detalle+="    §6 dice:  $e"$'\n'
    detalle+="    el arbol: $p"$'\n'
  else
    no_declaradas=$((no_declaradas + 1))
    detalle+="  línea $n difiere y NO estaba declarada"$'\n'
    detalle+="    esperada: $e"$'\n'
    detalle+="    obtenida: $p"$'\n'
  fi
done

echo
echo "Verificación contra el snapshot de §6:"
printf '%s' "$detalle"
echo
coinciden=$((${#esperadas[@]} - declaradas - no_declaradas))
if [ "$no_declaradas" -eq 0 ]; then
  if [ "$declaradas" -eq 0 ]; then
    echo "  CONFORME · las ${#esperadas[@]} líneas coinciden con el snapshot de §6"
  else
    echo "  CONFORME CON DIVERGENCIAS DECLARADAS · $coinciden/${#esperadas[@]} líneas coinciden, $declaradas por motivo escrito"
  fi
  exit 0
fi
echo "  NO CONFORME · $no_declaradas línea(s) difieren sin motivo declarado"
exit 1
