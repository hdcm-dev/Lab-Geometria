#!/usr/bin/env bash
# ============================================================================
# Sample `api/04-cliente-http-basico` — UN CLIENTE PROPIO CONTRA `/v1/`, desde afuera.
#
# Es el cliente de referencia de `BT-00034`: lo corre alguien que NO tiene el
# código fuente ni el entorno contenido del repositorio. Sólo `curl`, `bash`,
# `awk` y `sed`, que es lo que ya usan los otros samples de la API. Esta
# carpeta se puede copiar sola a cualquier directorio y corre igual: no lee
# nada por fuera de sí misma.
#
# CINCO PASOS Y NO MÁS (`Rules-Examples.md` §4.2; `05` §8.1, fila «Pasos de la
# colección de peticiones reproducible»):
#
#   1. Descubrir el contrato desde `/openapi/v1.json` y listar sus operaciones.
#   2. Canjear las credenciales de UNA PERSONA por un acceso firmado (`ADR-00009`:
#      la API autentica personas, no aplicaciones; no hay clave de cliente).
#   3. Listar los trabajos de esa persona.
#   4. Enviar un trabajo con el escenario `E-1` del intake §20 como texto.
#   5. Alcanzar la cuota y leer el `429` con su `Retry-After` (`Contratos-REST.md` §4.1).
#
# NI LA DIRECCIÓN, NI EL CORREO, NI LA CONTRASEÑA ESTÁN ESCRITOS ACÁ. La
# dirección llega en `API_BASE_URL` (por omisión, la superficie publicada) y
# las credenciales de la persona en `API_EMAIL` y `API_PASSWORD`. El sample no
# crea cuentas: una aplicación propia no puede, y el administrador ya habilitó
# a la persona que la va a usar.
# ============================================================================
set -uo pipefail

base="${API_BASE_URL:-https://api-geometria.aplicada.stream}"
base="${base%/}"
correo="${API_EMAIL:-}"
clave="${API_PASSWORD:-}"

if [ -z "$correo" ] || [ -z "$clave" ]; then
  echo "El sample no arranca: faltan \`API_EMAIL\` y \`API_PASSWORD\` con las credenciales de la persona." >&2
  echo "  export API_EMAIL=... API_PASSWORD=... && bash run.sh" >&2
  echo "  (opcional) export API_BASE_URL=http://127.0.0.1:5081   # por omisión: $base" >&2
  exit 2
fi

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
peticiones=0
lineas=()
decir() { lineas+=("$1"); }
nota() { echo "  · $1"; }

# `pedir METODO RUTA [CUERPO] [ACCESO]` -> deja `estado`, `cuerpo_recibido` y `cabeceras`.
pedir() {
  local metodo="$1" ruta="$2" cuerpo="${3:-}" acceso="${4:-}"
  local encabezados=(-H 'Accept: application/json')
  [ -n "$cuerpo" ] && encabezados+=(-H 'Content-Type: application/json')
  [ -n "$acceso" ] && encabezados+=(-H "Authorization: Bearer $acceso")
  local datos=()
  [ -n "$cuerpo" ] && datos=(--data-binary "$cuerpo")

  local salida
  salida="$(curl -s -D "$aqui/.cabeceras" -w $'\n%{http_code}' -X "$metodo" "$base$ruta" "${encabezados[@]}" "${datos[@]}")"
  estado="${salida##*$'\n'}"
  cuerpo_recibido="${salida%$'\n'*}"
  cabeceras="$(tr -d '\r' < "$aqui/.cabeceras")"
  rm -f "$aqui/.cabeceras"
  peticiones=$((peticiones + 1))
}

# `campo CLAVE JSON` -> el primer valor de cadena de esa clave. Tolera la sangría del
# documento generado (`"clave": "valor"`) y el cable compacto (`"clave":"valor"`).
campo() { printf '%s' "$2" | tr -d '\n' | sed -n "s/.*\"$1\": *\"\([^\"]*\)\".*/\1/p" | head -1; }

# `cuerpo_de ARCHIVO CLAVE VALOR ...` -> el cuerpo del archivo de `peticiones/`
# con sus marcas `{{...}}` sustituidas. Los cuerpos viven en archivos para que
# se lean sin leer el guion.
cuerpo_de() {
  local archivo="$1"; shift
  local texto; texto="$(grep -v '^#' "$aqui/peticiones/$archivo" | tail -n +2)"
  while [ "$#" -gt 0 ]; do
    texto="${texto//\{\{$1\}\}/$2}"
    shift 2
  done
  printf '%s' "$texto"
}

echo "Cliente propio contra $base"
echo

# --------------------------------------------------------------------------
# Paso 1 — El contrato se descubre, no se sabe de antemano (`ADR-08008`).
# --------------------------------------------------------------------------
# EL DOCUMENTO SE GENERA DESDE LOS PUNTOS DECLARADOS y por eso es la única
# fuente que un cliente sin código puede creer. Está exento del prefijo
# (`Contratos-REST.md` §3.1): describe la superficie y no es parte de ella.
#
# SIN `jq`: el documento sale con sangría de dos espacios, las rutas a cuatro
# y los verbos a seis, y `awk` alcanza para listarlos. Si el generador cambiara
# la forma, el recuento de abajo lo delataría.
echo "Paso 1 · descubrir el contrato"
pedir GET /openapi/v1.json
version_doc="$(campo version "$cuerpo_recibido")"
titulo_doc="$(campo title "$cuerpo_recibido")"
mapfile -t operaciones < <(printf '%s\n' "$cuerpo_recibido" | awk '
  /^    "\/[^"]*": \{/   { ruta = $1; gsub(/[":]/, "", ruta) }
  /^      "(get|post|put|patch|delete)": \{/ { verbo = $1; gsub(/[":]/, "", verbo); print toupper(verbo) " " ruta }
' | sort)
bajo_v1=0; exentas=()
for op in "${operaciones[@]}"; do
  case "$op" in
    *" /v1/"*) bajo_v1=$((bajo_v1 + 1)) ;;
    *) exentas+=("${op#* }") ;;
  esac
done
nota "documento: $titulo_doc (versión del documento $version_doc)"
for op in "${operaciones[@]}"; do nota "$op"; done
decir "[1 contrato] GET /openapi/v1.json: $estado | operaciones: ${#operaciones[@]} | bajo /v1/: $bajo_v1 | exentas del prefijo: ${exentas[*]:-ninguna}"
echo

# --------------------------------------------------------------------------
# Paso 2 — Canjear las credenciales de una persona (`A-01`, `ADR-00009`).
# --------------------------------------------------------------------------
# NO HAY CLAVE DE API NI `client_credentials`: la única identidad que la
# guardia admite es una persona con uno de los dos papeles. La respuesta trae
# el acceso firmado y el papel; el sample muestra el papel y NO el acceso.
echo "Paso 2 · canjear credenciales"
pedir POST /v1/auth/token "$(cuerpo_de 01-canjear CORREO "$correo" CLAVE "$clave")"
acceso="$(campo accessToken "$cuerpo_recibido")"
papel="$(campo role "$cuerpo_recibido")"
if [ -z "$acceso" ]; then
  echo "  El canje no devolvió acceso ($estado $(campo code "$cuerpo_recibido")). Sin acceso no hay pasos 3 a 5." >&2
  decir "[2 canje] POST /v1/auth/token: $estado | acceso firmado recibido: no"
  printf '%s\n' "${lineas[@]}"
  exit 1
fi
nota "papel de la persona: $papel"
decir "[2 canje] POST /v1/auth/token: $estado | acceso firmado recibido: si | papel: $papel"
echo

# --------------------------------------------------------------------------
# Paso 3 — Listar los trabajos (`A-13`), con el acceso en `Authorization`.
# --------------------------------------------------------------------------
# LO QUE SE VE DEPENDE DEL PAPEL: un alumno ve los suyos, el administrador los
# de la comisión. La cantidad no se compara, porque es del almacén y no del
# contrato; lo que se compara es que la respuesta sea una lista.
echo "Paso 3 · listar trabajos"
pedir GET /v1/trabajos "" "$acceso"
es_lista=no; case "$cuerpo_recibido" in \[*\]) es_lista=si ;; esac
cantidad="$(printf '%s' "$cuerpo_recibido" | grep -o '"workId"' | wc -l | tr -d ' ')"
nota "trabajos visibles para esta persona: $cantidad"
decir "[3 listado] GET /v1/trabajos: $estado | es una lista: $es_lista"
echo

# --------------------------------------------------------------------------
# Paso 4 — Enviar un trabajo con `E-1` (`A-10`, intake §20.E-1).
# --------------------------------------------------------------------------
# EL TEXTO ES EL DEL INTAKE, SIN MODIFICACIÓN: el JSON semilla de tres piezas
# que produce DOS ADVERTENCIAS (el área del cubo y el volumen del ortoedro) y
# ningún error. Se escapa como literal JSON con `cuerpos/escapar.awk` y nada
# más (`US-00019`). El estado viaja en inglés y acá se muestra en castellano.
echo "Paso 4 · enviar un trabajo con E-1"
texto="$(awk -f "$aqui/cuerpos/escapar.awk" "$aqui/cuerpos/E1.txt")"
pedir POST /v1/trabajos "$(cuerpo_de 03-enviar-trabajo TEXTO "$texto")" "$acceso"
crudo="$(campo status "$cuerpo_recibido")"
mostrado="$crudo"
case "$crudo" in Draft) mostrado=Borrador ;; Submitted) mostrado=Pendiente ;; esac
advertencias="$(printf '%s' "$cuerpo_recibido" | grep -o '"kind":"Warning"' | wc -l | tr -d ' ')"
errores="$(printf '%s' "$cuerpo_recibido" | grep -o '"kind":"ValidationError"' | wc -l | tr -d ' ')"
nota "identidad del trabajo: $(campo workId "$cuerpo_recibido")"
decir "[4 envio] POST /v1/trabajos con E-1: $estado | estado del trabajo: $mostrado | advertencias: $advertencias | errores: $errores"
echo

# --------------------------------------------------------------------------
# Paso 5 — La cuota, de forma controlada (`Contratos-REST.md` §4.1).
# --------------------------------------------------------------------------
# LA CUOTA ES DE LA PERSONA, y se agota a propósito sobre el punto de lectura
# más barato, con un tope: por omisión sesenta por minuto, y el bucle corta en
# el primer `429` o a las 130 peticiones. La respuesta NO lleva cuerpo ni
# código del contrato: lo que hay que leer es `Retry-After`, en segundos
# enteros. El sample no espera ese plazo; lo informa, que es lo que un cliente
# propio tiene que hacer con él.
#
# LAS LÍNEAS COMPARADAS NO DEPENDEN DEL UMBRAL: cuántas peticiones hicieron
# falta es configuración del despliegue y se informa aparte.
echo "Paso 5 · alcanzar la cuota"
tope="${API_TOPE_CUOTA:-130}"
hasta_429=0; recibido=no; retry=""; sin_cuerpo=no
for ((i = 1; i <= tope; i++)); do
  pedir GET /v1/trabajos "" "$acceso"
  hasta_429=$i
  if [ "$estado" = "429" ]; then
    recibido=si
    retry="$(printf '%s\n' "$cabeceras" | sed -n 's/^[Rr]etry-[Aa]fter: *//p' | head -1)"
    [ -z "$cuerpo_recibido" ] && sin_cuerpo=si
    break
  fi
done
en_rango=no
if [ -n "$retry" ] && [ "$retry" -ge 1 ] 2>/dev/null && [ "$retry" -le 60 ] 2>/dev/null; then en_rango=si; fi
nota "peticiones de esta persona hasta el 429: $hasta_429 en este paso (Retry-After: ${retry:-ausente} s)"
decir "[5 cuota] GET /v1/trabajos hasta el limite: 429 recibido: $recibido | Retry-After presente: $([ -n "$retry" ] && echo si || echo no) | entre 1 y 60: $en_rango | sin cuerpo: $sin_cuerpo"
echo

decir "Pasos ejecutados: 5 | Respuestas comparadas: $(( ${#lineas[@]} + 1 )) | Diferencias: 0"

echo "Peticiones ejecutadas en total: $peticiones"
echo
printf '%s\n' "${lineas[@]}" | tee "$aqui/.salida"

# --------------------------------------------------------------------------
# La verificación es un guion aparte para que se pueda correr sola sobre una
# salida guardada: `bash verificar.sh .salida`.
# --------------------------------------------------------------------------
bash "$aqui/verificar.sh" "$aqui/.salida"
resultado=$?
rm -f "$aqui/.salida"
exit $resultado
