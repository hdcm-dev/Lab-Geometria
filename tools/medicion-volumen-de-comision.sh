#!/usr/bin/env bash
# ============================================================================
# medicion-volumen-de-comision.sh — CUÁNTO AGUANTA EL LISTADO SIN PAGINACIÓN.
#
# POR QUÉ EXISTE. `D5` —el volumen de la comisión— es el único punto abierto del
# producto SIN EVENTO DE CIERRE: no hay medición que lo conteste, porque la
# pregunta escrita es «cuántos alumnos» y ése es un dato del Product Owner.
#
# Pero la pregunta que el PRODUCTO necesita contestada es otra, y ésa sí se mide:
#
#     ¿A PARTIR DE QUÉ VOLUMEN EL LISTADO DEJA DE SERVIR SIN PAGINACIÓN?
#
# El diseño de los dos listados —`Web/05` §11 `PA-06`, `Web/06` `PA-08`,
# `BT-10022`— declara que supone DECENAS Y NO CIENTOS, y por eso no incorpora
# paginación. Este instrumento mide dónde está el borde de esa suposición, para
# que la decisión del Product Owner se tome contra evidencia y no contra una
# estimación.
#
# QUÉ MIDE, Y CONTRA QUÉ UMBRAL. `GET /trabajos` pedido por el administrador, que
# es el listado de la comisión entera y el que no tiene tope. El umbral no lo
# inventa este guion: es el **p99 de 500 ms** de `PRODUCT-INTAKE` §22 `A-5`,
# CONFIRMADO por el Product Owner el 2026-08-26 con la decisión `D1`.
#
# Y MIDE UNA SEGUNDA COSA, QUE ES UNA VERIFICACIÓN Y NO UNA EXPLORACIÓN: que el
# peso POR FILA se mantenga constante. `A-5` declara un NFR estructural para
# `GeometriaFactory-Contracts` —que el payload de listado no lleve el
# `OriginalJson` ni los componentes de las piezas—. Si el peso por fila crece con
# el volumen, ese NFR no se está cumpliendo, y se vería acá antes que en ningún
# otro lado.
#
# QUÉ NO HACE. No decide `D5` ni la reemplaza. Si la comisión resulta ser más
# grande que el borde medido, lo que sale de acá no es una decisión sino una
# tarea técnica con fundamento.
#
# TODO CORRE EN CONTENEDOR y contra un almacén PROPIO y efímero, en un puerto
# propio: no toca el almacén de trabajo ni los contenedores del Product Owner
# (`scripts/store-path.sh`).
#
#   docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp --network host \
#     -v "$PWD:/repo" -w /repo mcr.microsoft.com/dotnet/sdk:10.0 \
#     bash tools/medicion-volumen-de-comision.sh
#
# CÓDIGOS DE SALIDA:
#   0  la medición se completó — el veredicto está en la tabla
#   2  no se pudo medir (el servicio no arrancó, o la siembra falló)
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

puerto="${GF_MEDICION_PUERTO:-5099}"
base="http://127.0.0.1:$puerto"
# Los cortes: decenas, cien, cientos y mil. El primero es lo que el diseño supone
# y el último es un orden de magnitud por encima de lo que nadie propuso.
cortes="${GF_MEDICION_CORTES:-30 100 300 1000}"
repeticiones="${GF_MEDICION_REPETICIONES:-30}"
por_alumno=3          # forma realista: la comisión son muchos alumnos con pocos trabajos
umbral_p99_ms=500     # `A-5`, confirmado por `D1` el 2026-08-26

trabajo="$(mktemp -d)"
almacen="$trabajo/medicion.db"
registro="$trabajo/servicio.log"
pid_servicio=""

limpiar() {
  [ -n "$pid_servicio" ] && kill "$pid_servicio" 2>/dev/null && wait "$pid_servicio" 2>/dev/null
  rm -rf "$trabajo"
}
trap limpiar EXIT

morir() { echo; echo "NO SE PUEDE MEDIR · $1" >&2; [ -s "$registro" ] && tail -5 "$registro" >&2; exit 2; }

# ---------------------------------------------------------------------------
# Cliente
# ---------------------------------------------------------------------------
estado=""; cuerpo_recibido=""
pedir() {
  local metodo="$1" ruta="$2" cuerpo="${3:-}" acceso="${4:-}"
  local encabezados=(-H 'Content-Type: application/json')
  [ -n "$acceso" ] && encabezados+=(-H "Authorization: Bearer $acceso")
  local datos=(); [ -n "$cuerpo" ] && datos=(--data-binary "$cuerpo")
  local salida
  salida="$(curl -s -w $'\n%{http_code}' -X "$metodo" "$base$ruta" "${encabezados[@]}" "${datos[@]}")"
  estado="${salida##*$'\n'}"; cuerpo_recibido="${salida%$'\n'*}"
}
campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }
clave() { printf 'Md-%s-2026' "$(head -c 12 /dev/urandom | base64 | tr -d '/+=')"; }

# ---------------------------------------------------------------------------
# Servicio
# ---------------------------------------------------------------------------
echo "Compilando…"
dotnet build "$raiz/GeometriaFactory.sln" -c Release -v q --nologo >"$trabajo/build.log" 2>&1 \
  || { cat "$trabajo/build.log" >&2; morir "la solución no compila."; }

echo "Levantando el servicio en $base con almacén propio…"
ConnectionStrings__Store="Data Source=$almacen" \
AccessToken__SigningKey="$(head -c 48 /dev/urandom | base64 | tr -d '\n')" \
Kestrel__Endpoints__Http__Url="$base" \
ASPNETCORE_ENVIRONMENT=Development \
  dotnet run --project "$raiz/src/GeometriaFactory.Api/GeometriaFactory.Api.csproj" \
    -c Release --no-build >"$registro" 2>&1 &
pid_servicio=$!

listo=0
for _ in $(seq 1 120); do
  curl -s -o /dev/null --max-time 1 "$base/salud" && { listo=1; break; }
  sleep 0.5
done
[ "$listo" = 1 ] || morir "el servicio no respondió en 60 s."

# ---------------------------------------------------------------------------
# Siembra
# ---------------------------------------------------------------------------
clave_admin="$(clave)"
pedir POST /cuentas/administrador \
  "{\"email\":\"medicion.admin@ejemplo.test\",\"firstName\":\"Medicion\",\"lastName\":\"Admin\",\"password\":\"$clave_admin\"}"
[ "$estado" = "201" ] || morir "no se pudo configurar el administrador (HTTP $estado)."
pedir POST /auth/token "{\"email\":\"medicion.admin@ejemplo.test\",\"password\":\"$clave_admin\"}"
acceso_admin="$(campo accessToken "$cuerpo_recibido")"
[ -n "$acceso_admin" ] || morir "el administrador no obtuvo acceso."

# El texto del trabajo es el escenario E1 del seed: un dato REAL del producto, no
# una cadena inventada. Va el mismo en todos, y es deliberado: lo que se mide es
# el tamaño del LISTADO, que por `A-5` no debe llevar el `originalJson`. Si el
# peso por fila cambiara con el contenido, ese NFR estaría roto.
texto="$(awk -f "$raiz/samples/web/01-datos-seed/datos/escapar.awk" \
             "$raiz/samples/web/01-datos-seed/datos/E1.txt")"

creados=0
alta_y_envios() {
  local n="$1"
  # En un solo `local` bash expande TODAS las palabras antes de asignar ninguna,
  # de modo que `${n}` en la misma línea sale sin definir y `set -u` detiene el guion.
  local correo="alumno${n}@ejemplo.test"
  local suya; suya="$(clave)"
  pedir POST /cuentas "{\"email\":\"$correo\",\"firstName\":\"Alumno\",\"lastName\":\"Numero$n\"}"
  local id; id="$(campo accountId "$cuerpo_recibido")"
  [ -n "$id" ] || return 1
  pedir POST "/cuentas/$id/situacion" "{\"accountId\":\"$id\",\"intendedStatus\":\"Enabled\"}" "$acceso_admin"
  local provisoria; provisoria="$(campo provisionalPassword "$cuerpo_recibido")"
  pedir POST /cuenta/contrasena \
    "{\"email\":\"$correo\",\"currentPassword\":\"$provisoria\",\"newPassword\":\"$suya\"}"
  pedir POST /auth/token "{\"email\":\"$correo\",\"password\":\"$suya\"}"
  local acceso; acceso="$(campo accessToken "$cuerpo_recibido")"
  [ -n "$acceso" ] || return 1
  local i
  for i in $(seq 1 "$por_alumno"); do
    pedir POST /trabajos \
      "{\"name\":\"Trabajo $n-$i\",\"declaredDate\":\"2026-08-30\",\"description\":null,\"originalJson\":$texto}" \
      "$acceso"
    [ "$estado" = "201" ] && creados=$((creados + 1))
  done
  printf '%s' "$acceso" > "$trabajo/ultimo-acceso-alumno"
}

# ---------------------------------------------------------------------------
# Medición
# ---------------------------------------------------------------------------
# El percentil se calcula sobre los tiempos ORDENADOS y no sobre el promedio: un
# listado que casi siempre responde rápido y de vez en cuando no, es exactamente
# el caso que el umbral de `A-5` quiere atrapar, y un promedio lo esconde.
percentiles() {   # entrada: tiempos en ms, uno por línea. salida: "p50 p99 max"
  sort -n | awk '
    {v[NR]=$1}
    END{
      if(NR==0){print "0 0 0"; exit}
      p50=v[int((NR+1)*0.50+0.5)>NR?NR:int((NR+1)*0.50+0.5)]
      p99=v[int((NR+1)*0.99+0.5)>NR?NR:int((NR+1)*0.99+0.5)]
      printf "%.1f %.1f %.1f", p50, p99, v[NR]
    }'
}

medir() {   # $1 = token, $2 = etiqueta ; deja en `res_*`
  local acceso="$1" i t
  local tiempos="$trabajo/tiempos.txt"; : > "$tiempos"
  # Dos llamadas de calentamiento que NO se cuentan: la primera pide el plan de
  # consulta y el primer viaje mide el arranque, no el listado.
  for i in 1 2; do curl -s -o /dev/null -H "Authorization: Bearer $acceso" "$base/trabajos"; done
  for i in $(seq 1 "$repeticiones"); do
    t="$(curl -s -o "$trabajo/cuerpo.json" -w '%{time_total}' -H "Authorization: Bearer $acceso" "$base/trabajos")"
    awk -v t="$t" 'BEGIN{printf "%.3f\n", t*1000}' >> "$tiempos"
  done
  read -r res_p50 res_p99 res_max <<<"$(percentiles < "$tiempos")"
  res_bytes="$(wc -c < "$trabajo/cuerpo.json" | tr -d ' ')"
  res_filas="$(grep -o '"workId":' "$trabajo/cuerpo.json" | wc -l | tr -d ' ')"
}

echo
printf '%-8s %-8s %-10s %-10s %-10s %-12s %-10s %s\n' \
  TRABAJOS ALUMNOS "p50 (ms)" "p99 (ms)" "max (ms)" "PESO" "B/FILA" "p99 vs 500 ms"
printf '%s\n' "------------------------------------------------------------------------------------------"

alumnos=0
filas_informe=()
for corte in $cortes; do
  objetivo_alumnos=$(( (corte + por_alumno - 1) / por_alumno ))
  while [ "$alumnos" -lt "$objetivo_alumnos" ]; do
    alumnos=$((alumnos + 1))
    alta_y_envios "$alumnos" || morir "falló el alta del alumno $alumnos."
  done
  medir "$acceso_admin"
  por_fila=$(( res_filas > 0 ? res_bytes / res_filas : 0 ))
  veredicto="$(awk -v p="$res_p99" -v u="$umbral_p99_ms" 'BEGIN{print (p<=u)?"PASA":"NO PASA"}')"
  printf '%-8s %-8s %-10s %-10s %-10s %-12s %-10s %s\n' \
    "$res_filas" "$alumnos" "$res_p50" "$res_p99" "$res_max" "$(numfmt --to=iec "$res_bytes" 2>/dev/null || echo "$res_bytes")" "$por_fila" "$veredicto"
  filas_informe+=("$res_filas|$alumnos|$res_p50|$res_p99|$res_bytes|$por_fila|$veredicto")
done

# El listado PROPIO del alumno como control: tiene tope natural —los trabajos de
# una persona— y por eso debe quedarse quieto mientras el de la comisión crece.
# Si los dos crecieran igual, el problema no sería el volumen sino la consulta.
medir "$(cat "$trabajo/ultimo-acceso-alumno")"
echo
echo "Control · listado propio de un alumno ($res_filas trabajos): p50 ${res_p50} ms · p99 ${res_p99} ms · ${res_bytes} B"
echo
echo "Trabajos creados: $creados · almacén efímero en $almacen (se borra al salir)"
echo "Umbral: p99 ≤ ${umbral_p99_ms} ms — \`PRODUCT-INTAKE\` §22 \`A-5\`, confirmado por \`D1\` el 2026-08-26."
printf '%s\n' "${filas_informe[@]}" > "${GF_MEDICION_SALIDA:-/dev/null}"
