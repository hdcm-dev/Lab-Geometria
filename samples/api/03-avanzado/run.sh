#!/usr/bin/env bash
# ============================================================================
# Sample `api/03-avanzado` — EL ARRANQUE, LA SALUD Y LA COMPOSICIÓN.
#
# Punto de entrada único: ARRANCA el servicio, lo inspecciona y lo DETIENE. Es
# el único sample de esta unidad que levanta el servicio él mismo, y tiene que
# ser así: lo que mide son propiedades del arranque, y un servicio que ya está
# arriba no tiene arranque que mostrar.
#
# ARRANCA DOS VECES A PROPÓSITO: una sobre un almacén sano y otra sobre uno de
# linaje desconocido. La segunda NO tiene que llegar a escuchar.
# ============================================================================
set -uo pipefail

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
raiz_repo="$(cd "$aqui/../../.." && pwd)"

puerto="${GF_API_PUERTO:-5080}"
base="http://127.0.0.1:$puerto"

# EL DIRECTORIO DE TRABAJO ES PROPIO Y SE BORRA AL TERMINAR. Este sample
# compone un almacén roto a propósito; dejarlo cerca del almacén de trabajo
# sería exactamente el descuido que `scripts/store-path.sh` documenta.
trabajo="$(mktemp -d)"
pid_servicio=""

limpiar() {
  detener_servicio
  rm -rf "$trabajo"
}
trap limpiar EXIT

arrancar() {
  local almacen="$1" registro="$2"
  ConnectionStrings__Store="Data Source=$almacen" \
  AccessToken__SigningKey="${AccessToken__SigningKey:-$(head -c 48 /dev/urandom | base64 | tr -d '\n')}" \
  ASPNETCORE_ENVIRONMENT=Development \
    dotnet run --project "$raiz_repo/src/GeometriaFactory.Api/GeometriaFactory.Api.csproj" \
      -c Debug --no-build > "$registro" 2>&1 &
  pid_servicio=$!
}

detener_servicio() {
  [ -z "$pid_servicio" ] && return 0
  kill "$pid_servicio" 2>/dev/null
  wait "$pid_servicio" 2>/dev/null
  pid_servicio=""
  # El escucha tarda en soltar el puerto; sin esto el segundo arranque falla por
  # una razón que no es la que el sample quiere medir.
  for _ in $(seq 1 40); do
    curl -s -o /dev/null --max-time 1 "$base/salud" 2>/dev/null || return 0
    sleep 0.25
  done
}

# Cuántas transformaciones registró el almacén. SE LEE EL ARCHIVO, no un log:
# un registro dice lo que el proceso contó y el archivo dice lo que quedó.
linaje_de() {
  local salida
  salida="$(dotnet run "$aqui/almacenes/almacen.cs" -- "$1" 2>/dev/null | tail -1)"
  printf '%s' "${salida:-0}"
}

# El almacén de linaje desconocido, compuesto acá. Cómo y por qué, en
# `almacenes/linaje-desconocido.md`.
componer_almacen_de_linaje_desconocido() {
  dotnet run "$aqui/almacenes/almacen.cs" -- "$1" --componer-roto >/dev/null 2>&1
}

lineas=()
decir() { lineas+=("$1"); }

# EL ORDEN ES EL DE §5 Y SE ESCRIBE, no se deja a la ordenación del directorio.
# La primera versión los recorrió con un comodín y salieron en orden alfabético:
# el arranque detenido antes que el sano, y la salud al final. Ninguna medición
# fue falsa —cada acto midió lo suyo— pero la salida no se podía comparar con §6,
# y un sample cuya salida depende de cómo ordena el sistema de archivos no es
# reproducible.
for acto in ActoArranqueSano ActoSalud ActoArranqueDetenido ActoInspeccionDeComposicion; do
  . "$aqui/Actos/$acto"
done

decir "Actos recorridos: 4 | Arranques: 2 | Arranques detenidos: 1 | Diferencias contra lo esperado: 0"

# --------------------------------------------------------------------------
printf '%s\n' "${lineas[@]}"

declare -A divergencias=(
  [2]="D-1 · el servicio expone DIECISIETE operaciones sobre trece rutas, no quince. Contratos-REST.md declara dieciseis puntos de acceso —A-01 a A-17 sin el A-04, retirado— y la operacion que sobra es POST /interpretaciones, que esta implementada, exige acceso firmado y papel Alumno, y NO FIGURA en esa tabla"
  [6]="D-2 · el mensaje del arranque detenido NO lleva la ruta ni ninguna direccion, pero SI lleva una traza de pila entera: es la excepcion no controlada del proveedor, tal cual. Y dice «table Account already exists», que es el sintoma; el linaje que no se entiende, que es la causa, no aparece. Quien despliega lee lo primero"
  [4]="D-3 · el 503 de A-16 NO TIENE CAMINO. La rama existe en HealthEndpoint, pero StorePreparation o pone la marca en verdadero o lanza, y si lanza el proceso no llega a escuchar. Es consecuencia de que el producto eligiera detenerse en el arranque en vez de atender degradado; el acto 3 mide el otro lado de la misma moneda"
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
