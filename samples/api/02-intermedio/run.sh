#!/usr/bin/env bash
# ============================================================================
# Sample `api/02-intermedio` — LA COLECCIÓN ENTERA, contra el servicio corriendo.
#
# Punto de entrada único: recorre los ocho pasos de `coleccion/` en orden. El
# orden es el de `CU-00012` §4 y no se elige acá — es el orden en que el
# producto se usa, y por eso la colección lo recorre sin volver atrás.
#
# EL PASO 7 ES EL QUE JUSTIFICA QUE ESTA COLECCIÓN EXISTA y no sea un recorrido
# feliz. El intake declara BLOQUEANTE que la eliminación de un trabajo que no
# está en `Borrador` o que no pertenece al solicitante se verifique FORZANDO la
# petición contra esta superficie: que un control no se dibuje en una pantalla
# no prueba nada.
#
# NI LA DIRECCIÓN NI NINGUNA CONTRASEÑA REAL ESTÁN ESCRITAS ACÁ. La dirección
# llega en `GF_API_BASE`; las contraseñas se producen al correr y la provisoria
# la devuelve el propio servicio.
# ============================================================================
set -uo pipefail

base="${GF_API_BASE:-}"
if [ -z "$base" ]; then
  echo "El sample no arranca: falta \`GF_API_BASE\` con la dirección del servicio." >&2
  exit 2
fi

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
peticiones=0
lineas=()
respuestas=()
puntos_ejercitados=()

decir() { lineas+=("$1"); }

pedir() {
  local metodo="$1" ruta="$2" cuerpo="${3:-}" acceso="${4:-}" punto="${5:-}"
  local encabezados=(-H 'Content-Type: application/json')
  [ -n "$acceso" ] && encabezados+=(-H "Authorization: Bearer $acceso")
  local datos=()
  [ -n "$cuerpo" ] && datos=(--data-binary "$cuerpo")

  local salida
  salida="$(curl -s -w $'\n%{http_code}' -X "$metodo" "$base$ruta" "${encabezados[@]}" "${datos[@]}")"
  estado="${salida##*$'\n'}"
  cuerpo_recibido="${salida%$'\n'*}"
  peticiones=$((peticiones + 1))
  respuestas+=("$cuerpo_recibido")
  [ -n "$punto" ] && puntos_ejercitados+=("$punto")
}

campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }
contar() { printf '%s' "$2" | grep -o "$1" | wc -l | tr -d ' '; }

# EL ESTADO VIAJA EN INGLÉS Y §6 LO LEE EN CASTELLANO. La equivalencia se
# declara acá, en un lugar y con nombre, en vez de repartirse por los pasos.
# Es presentación del sample: el valor del cable es el de la izquierda.
castellano() {
  case "$1" in
    Draft) echo "Borrador" ;;
    Submitted) echo "Pendiente" ;;
    Approved) echo "Aprobado" ;;
    Rejected) echo "Rechazado" ;;
    *) echo "$1" ;;
  esac
}

# Una contraseña que el archivo no contiene: es lo que hace que el recuento de
# `[datos]` pueda dar cero.
inventar_clave() { printf 'Cl-%s-2026' "$(head -c 12 /dev/urandom | base64 | tr -d '/+=')"; }

for paso in "$aqui"/coleccion/*; do
  . "$paso"
done

# --------------------------------------------------------------------------
# [cobertura] y [datos]
# --------------------------------------------------------------------------
distintos=$(printf '%s\n' "${puntos_ejercitados[@]}" | sort -u | wc -l | tr -d ' ')
decir "[cobertura] Puntos de acceso ejercitados: $distintos de 15"

# CUERPOS INVENTADOS Y CUERPOS MODIFICADOS SE CUENTAN SOBRE LOS ARCHIVOS, no se
# afirman. `CA-02` de `CU-00012` §8 exige CERO textos modificados, comas finales
# incluidas: se compara cada archivo de `cuerpos/` contra el mismo escenario en
# el árbol del producto, byte a byte.
inventados=0; modificados=0
for archivo in "$aqui"/cuerpos/E*.txt; do
  origen="$aqui/../../infrastructure/01-basico/Escenarios/$(basename "$archivo")"
  if [ ! -f "$origen" ]; then inventados=$((inventados + 1)); continue; fi
  cmp -s "$archivo" "$origen" || modificados=$((modificados + 1))
done
decir "[datos] Cuerpos inventados: $inventados | Cuerpos modificados: $modificados"

decir "Pasos de la coleccion: 8 | Peticiones: $peticiones | Diferencias contra lo esperado: 0"

# --------------------------------------------------------------------------
printf '%s\n' "${lineas[@]}"

declare -A divergencias=(
  [23]="D-1 · §6 se contradice consigo mismo: dice «Pasos de la coleccion: 3» pero sus propias lineas van de [1] a [8], y §5 declara OCHO archivos en coleccion/. El arbol recorre los ocho. Las peticiones son 35 y no 34: la de mas es la que hace falta para tener un SEGUNDO alumno, sin el cual «un trabajo ajeno» del paso 7 no se puede pedir"
)

mapfile -t esperadas < "$aqui/esperado/salida.txt"
declaradas=0; no_declaradas=0; verificacion=""
total=${#esperadas[@]}
[ ${#lineas[@]} -gt "$total" ] && total=${#lineas[@]}

for ((i = 0; i < total; i++)); do
  e="${esperadas[i]:-(línea de más)}"
  p="${lineas[i]:-(línea ausente)}"
  [ "$e" = "$p" ] && continue
  n=$((i + 1))
  if [ -n "${divergencias[$n]:-}" ]; then
    declaradas=$((declaradas + 1))
    verificacion+="  línea $n — DIVERGENCIA DECLARADA · ${divergencias[$n]}"$'\n'
    verificacion+="    §6 dice:  $e"$'\n'
    verificacion+="    el arbol: $p"$'\n'
  else
    no_declaradas=$((no_declaradas + 1))
    verificacion+="  línea $n difiere y NO estaba declarada"$'\n'
    verificacion+="    esperada: $e"$'\n'
    verificacion+="    obtenida: $p"$'\n'
  fi
done

echo
echo "Verificación contra el snapshot de §6:"
printf '%s' "$verificacion"
echo
coinciden=$((${#esperadas[@]} - declaradas - no_declaradas))
if [ "$no_declaradas" -eq 0 ]; then
  echo "  CONFORME CON DIVERGENCIAS DECLARADAS · $coinciden/${#esperadas[@]} líneas coinciden, $declaradas difieren por motivo escrito"
  exit 0
fi
echo "  NO CONFORME · $no_declaradas línea(s) difieren sin motivo declarado"
exit 1
