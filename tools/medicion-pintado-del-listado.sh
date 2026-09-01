#!/usr/bin/env bash
# ============================================================================
# medicion-pintado-del-listado.sh — LO QUE LA MEDICIÓN DEL SERVICIO NO CONTESTA.
#
# POR QUÉ EXISTE. `tools/medicion-volumen-de-comision.sh` midió el servicio de
# datos y declaró expresamente que **no mide la pieza pública**, que es donde el
# diseño puso su suposición: el «supone decenas y no cientos» de `Web/05` §11
# `PA-06` es sobre LA SUPERFICIE, no sobre el JSON. Este guion mide esa mitad.
#
# QUÉ MIDE, y son dos cosas que no se promedian:
#
#   1. PRIMERA PINTURA — desde que se pide `/entrega-comision` hasta que están
#      en el documento los grupos y las filas que la siembra dejó. Es lo que ve
#      el docente al entrar.
#   2. FILTRO POR ALUMNO — volver a pedir la colección acotada a un alumno.
#
# Y HAY UNA SUPOSICIÓN QUE PARECÍA OBVIA Y ES FALSA, que conviene leer antes que
# los números: ESTA SUPERFICIE NO USA EL CIRCUITO. `ClassSubmissionList.razor` no
# declara `@rendermode` —sólo SEIS componentes lo hacen, `ADR-10001` §2.1— y su
# filtro es un `<form method="get">`. El propio código lo dice: «los dos viajan
# por la dirección porque esta superficie es de render estático».
#
# LA CONSECUENCIA ES BUENA: el comportamiento de esta pantalla ante el volumen
# NO DEPENDE DEL TRANSPORTE. Que el hosting repliegue a long polling no cambia
# nada acá, porque la sesión interactiva no participa. Lo medido en local es
# REPRESENTATIVO —salvo la latencia de red hasta el hosting— y no un piso.
#
# Y NO CIERRA NADA. La suposición de «decenas y no cientos» es del diseño de la
# superficie y sólo el uso real la valida — eso es `PT-05`, fase `i`.
#
# TODO CORRE EN CONTENEDOR, contra un almacén propio y efímero y en puertos
# propios: no toca el almacén de trabajo ni los contenedores del Product Owner.
#
#   bash tools/medicion-pintado-del-listado.sh
#
# CÓDIGOS DE SALIDA:  0 se midió · 2 no se pudo medir
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

puerto_api="${GF_MEDICION_PUERTO_API:-5099}"
puerto_web="${GF_MEDICION_PUERTO_WEB:-5098}"
base_api="http://127.0.0.1:$puerto_api"
base_web="http://127.0.0.1:$puerto_web"
cortes="${GF_MEDICION_CORTES:-30 100 300}"
por_alumno=3
imagen_sdk="mcr.microsoft.com/dotnet/sdk:10.0"
imagen_pw="mcr.microsoft.com/playwright:v1.48.0-jammy"

trabajo="$(mktemp -d)"
almacen="$trabajo/medicion.db"
cid_api=""; cid_web=""

# LA CACHÉ DE PAQUETES SE COMPARTE ENTRE COMPILAR Y EJECUTAR, y no es una optimización.
# El manifiesto de activos estáticos que genera la compilación de la pieza pública guarda
# RUTAS ABSOLUTAS a la caché; si el contenedor que ejecuta tiene otra, el arranque muere con
# un `DirectoryNotFoundException` sobre `_framework/` que no dice nada de lo que pasa. Con la
# caché montada del anfitrión, las dos ven la misma ruta. Se conserva entre corridas.
cache="${GF_MEDICION_CACHE:-$HOME/.cache/geometria-factory-medicion/nuget}"
mkdir -p "$cache"

limpiar() {
  [ -n "$cid_api" ] && docker rm -f "$cid_api" >/dev/null 2>&1
  [ -n "$cid_web" ] && docker rm -f "$cid_web" >/dev/null 2>&1
  rm -rf "$trabajo"
}
trap limpiar EXIT
morir() { echo; echo "NO SE PUEDE MEDIR · $1" >&2; exit 2; }

# ---------------------------------------------------------------------------
# Los dos servicios, cada uno en su contenedor
# ---------------------------------------------------------------------------
echo "Compilando…"
docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget \
  -v "$cache:/nuget" -v "$raiz:/repo" -w /repo "$imagen_sdk" \
  dotnet build GeometriaFactory.sln -c Release -v q --nologo >"$trabajo/build.log" 2>&1 \
  || { tail -20 "$trabajo/build.log" >&2; morir "la solución no compila."; }

clave_firma="$(head -c 48 /dev/urandom | base64 | tr -d '\n')"

echo "Levantando el servicio de datos en $base_api…"
cid_api="$(docker run -d -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget --network host \
  -v "$cache:/nuget" -v "$raiz:/repo" -v "$trabajo:/trabajo" -w /repo \
  -e ConnectionStrings__Store="Data Source=/trabajo/medicion.db" \
  -e AccessToken__SigningKey="$clave_firma" \
  -e Kestrel__Endpoints__Http__Url="$base_api" \
  -e ASPNETCORE_ENVIRONMENT=Development "$imagen_sdk" \
  dotnet run --project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj -c Release --no-build)"

echo "Levantando la pieza pública en $base_web…"
cid_web="$(docker run -d -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget --network host \
  -v "$cache:/nuget" -v "$raiz:/repo" -w /repo \
  -e ApiBaseUrl="$base_api/" \
  -e Kestrel__Endpoints__Http__Url="$base_web" \
  -e ASPNETCORE_ENVIRONMENT=Development "$imagen_sdk" \
  dotnet run --project src/GeometriaFactory.Web/GeometriaFactory.Web.csproj -c Release --no-build)"

esperar() {
  local url="$1" nombre="$2" i
  for i in $(seq 1 180); do
    curl -s -o /dev/null --max-time 2 "$url" && return 0
    sleep 0.5
  done
  docker logs "$3" 2>&1 | tail -12 >&2
  morir "$nombre no respondió en 90 s."
}
esperar "$base_api/salud" "el servicio de datos" "$cid_api"
esperar "$base_web/ingreso" "la pieza pública" "$cid_web"

# ---------------------------------------------------------------------------
# Siembra, por el circuito real del producto
# ---------------------------------------------------------------------------
estado=""; cuerpo=""
pedir() {
  local m="$1" r="$2" c="${3:-}" a="${4:-}"
  local h=(-H 'Content-Type: application/json'); [ -n "$a" ] && h+=(-H "Authorization: Bearer $a")
  local d=(); [ -n "$c" ] && d=(--data-binary "$c")
  local s; s="$(curl -s -w $'\n%{http_code}' -X "$m" "$base_api$r" "${h[@]}" "${d[@]}")"
  estado="${s##*$'\n'}"; cuerpo="${s%$'\n'*}"
}
campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }
clave() { printf 'Md-%s-2026' "$(head -c 12 /dev/urandom | base64 | tr -d '/+=')"; }

correo_admin="medicion.admin@ejemplo.test"; clave_admin="$(clave)"
pedir POST /cuentas/administrador \
  "{\"email\":\"$correo_admin\",\"firstName\":\"Medicion\",\"lastName\":\"Admin\",\"password\":\"$clave_admin\"}"
[ "$estado" = "201" ] || morir "no se pudo configurar el administrador (HTTP $estado)."
pedir POST /auth/token "{\"email\":\"$correo_admin\",\"password\":\"$clave_admin\"}"
acceso_admin="$(campo accessToken "$cuerpo")"

texto="$(awk -f "$raiz/samples/web/01-datos-seed/datos/escapar.awk" \
             "$raiz/samples/web/01-datos-seed/datos/E1.txt")"

alta_y_envios() {
  local n="$1"
  local correo="alumno${n}@ejemplo.test"
  local suya; suya="$(clave)"
  pedir POST /cuentas "{\"email\":\"$correo\",\"firstName\":\"Alumno\",\"lastName\":\"Numero$n\"}"
  local id; id="$(campo accountId "$cuerpo")"; [ -n "$id" ] || return 1
  pedir POST "/cuentas/$id/situacion" "{\"accountId\":\"$id\",\"intendedStatus\":\"Enabled\"}" "$acceso_admin"
  local prov; prov="$(campo provisionalPassword "$cuerpo")"
  pedir POST /cuenta/contrasena "{\"email\":\"$correo\",\"currentPassword\":\"$prov\",\"newPassword\":\"$suya\"}"
  pedir POST /auth/token "{\"email\":\"$correo\",\"password\":\"$suya\"}"
  local acceso; acceso="$(campo accessToken "$cuerpo")"; [ -n "$acceso" ] || return 1
  local i
  for i in $(seq 1 "$por_alumno"); do
    pedir POST /trabajos \
      "{\"name\":\"Trabajo $n-$i\",\"declaredDate\":\"2026-08-30\",\"description\":null,\"originalJson\":$texto}" \
      "$acceso"
  done
}

# ---------------------------------------------------------------------------
# Medición
# ---------------------------------------------------------------------------
echo
printf '%-20s %-7s %-8s %-12s %-10s %-12s %s\n' SUPERFICIE FILAS ALUMNOS "1a PINTURA" "FILTRO" "MARCADO" "TRANSFERIDO"
printf '%s\n' "---------------------------------------------------------------------------------------------"

alumnos=0
for corte in $cortes; do
  objetivo=$(( (corte + por_alumno - 1) / por_alumno ))
  while [ "$alumnos" -lt "$objetivo" ]; do
    alumnos=$((alumnos + 1)); alta_y_envios "$alumnos" || morir "falló el alta del alumno $alumnos."
  done
  filas=$((alumnos * por_alumno))
  # `/cuentas` espera UNA FILA POR ALUMNO y no una más: el panel lista **cuentas de
  # alumno** y la del administrador no figura —«Todavía no hay ninguna cuenta de alumno»
  # es su vacío—. La primera corrida esperó `alumnos + 1` y falló con 10 de 11: la
  # expectativa estaba mal, no el producto. Y no agrupa, por eso el 0.
  for superficie in "entrega-comision:$alumnos:$filas" "cuentas:0:$alumnos"; do
    ruta="/${superficie%%:*}"; resto="${superficie#*:}"
    g="${resto%%:*}"; f="${resto##*:}"
    salida="$(docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp --network host \
      -v "$raiz/tools:/t" -w /t "$imagen_pw" \
      bash -c "npm install --no-save playwright@1.48.0 >/dev/null 2>&1 && \
               node medicion-pintado-del-listado.mjs '$base_web' '$correo_admin' '$clave_admin' '$ruta' $g $f" \
      2>/dev/null | tail -1)"
    err="$(printf '%s' "$salida" | sed -n 's/.*"error":"\([^"]*\)".*/\1/p')"
    [ -n "$err" ] && morir "el navegador no pudo medir $ruta con $f fila(s): $err"
    leer() { printf '%s' "$salida" | sed -n "s/.*\"$1\":\([^,}]*\).*/\1/p"; }
    filtro="$(leer filtro_ms)"
    printf '%-20s %-7s %-8s %-12s %-10s %-12s %s\n' \
      "$ruta" "$(leer filas)" "$alumnos" "$(leer pintura_ms) ms" \
      "$( [ "$filtro" = "null" ] && echo '--' || echo "$filtro ms" )" \
      "$(numfmt --to=iec "$(leer marcado_emitido)" 2>/dev/null || leer marcado_emitido)" \
      "$(numfmt --to=iec "$(leer bytes_transferidos)" 2>/dev/null || leer bytes_transferidos)"
  done
done

echo
echo "Esta superficie es de RENDER ESTÁTICO: no usa la sesión interactiva, y su filtro es una"
echo "navegación. El repliegue a long polling del hosting NO LA AFECTA, y lo medido acá es"
echo "representativo salvo la latencia de red."
echo "No cierra la suposición de «decenas y no cientos»: eso es \`PT-05\`, en la fase \`i\`."
