#!/usr/bin/env bash
# ============================================================================
# verificar-resolucion-del-trabajo.sh — LEVANTA EL PRODUCTO Y APRIETA EL BOTÓN.
#
# Monta un laboratorio entero y efímero —servicio de datos, pieza pública y un
# almacén propio—, siembra un administrador, un alumno y un trabajo enviado, y
# le pasa el trabajo a `verificar-resolucion-del-trabajo.mjs`, que abre un
# navegador de verdad y aprieta «Aprobar».
#
# NO TOCA NADA DEL PRODUCT OWNER. Puertos propios —5199 y 5198—, almacén en un
# directorio temporal que se borra al salir, y contenedores con nombre propio
# que se destruyen en el `trap`. `gf-api`, `gf-web` y `lab-geometria-api` no se
# rozan.
#
# SE CORRE ASÍ, desde la raíz del repositorio:
#     tools/verificar-resolucion-del-trabajo.sh
#
# CÓDIGOS DE SALIDA:
#   0  los pasos pasaron
#   1  algún paso falló
#   2  no se pudo montar el laboratorio
# ============================================================================
set -uo pipefail
raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

puerto_api="${GF_VERIF_PUERTO_API:-5199}"
puerto_web="${GF_VERIF_PUERTO_WEB:-5198}"
base_api="http://127.0.0.1:$puerto_api"
base_web="http://127.0.0.1:$puerto_web"

imagen_sdk="mcr.microsoft.com/dotnet/sdk:10.0"
imagen_pw="mcr.microsoft.com/playwright:v1.48.0-jammy"

trabajo="$(mktemp -d)"
cid_api=""; cid_web=""
cache="${GF_VERIF_CACHE:-$HOME/.cache/geometria-factory-medicion/nuget}"
mkdir -p "$cache"

limpiar() {
  [ -n "$cid_api" ] && docker rm -f "$cid_api" >/dev/null 2>&1
  [ -n "$cid_web" ] && docker rm -f "$cid_web" >/dev/null 2>&1
  rm -rf "$trabajo"
}
trap limpiar EXIT
morir() { echo; echo "NO SE PUEDE VERIFICAR · $1" >&2; exit 2; }

echo "Compilando…"
docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget \
  -v "$cache:/nuget" -v "$raiz:/repo" -w /repo "$imagen_sdk" \
  dotnet build GeometriaFactory.sln -c Release -v q --nologo >"$trabajo/build.log" 2>&1 \
  || { tail -20 "$trabajo/build.log" >&2; morir "la solución no compila."; }

clave_firma="$(head -c 48 /dev/urandom | base64 | tr -d '\n')"

echo "Levantando el servicio de datos en $base_api…"
cid_api="$(docker run -d -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget --network host \
  -v "$cache:/nuget" -v "$raiz:/repo" -v "$trabajo:/trabajo" -w /repo \
  -e ConnectionStrings__Store="Data Source=/trabajo/verificacion.db" \
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

estado=""; cuerpo=""
pedir() {
  local m="$1" r="$2" c="${3:-}" a="${4:-}"
  local h=(-H 'Content-Type: application/json'); [ -n "$a" ] && h+=(-H "Authorization: Bearer $a")
  local d=(); [ -n "$c" ] && d=(--data-binary "$c")
  local s; s="$(curl -s -w $'\n%{http_code}' -X "$m" "$base_api$r" "${h[@]}" "${d[@]}")"
  estado="${s##*$'\n'}"; cuerpo="${s%$'\n'*}"
}
campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }
clave() { printf 'Vf-%s-2026' "$(head -c 12 /dev/urandom | base64 | tr -d '/+=')"; }

correo_admin="verif.admin@ejemplo.test"; clave_admin="$(clave)"
pedir POST /cuentas/administrador \
  "{\"email\":\"$correo_admin\",\"firstName\":\"Verif\",\"lastName\":\"Admin\",\"password\":\"$clave_admin\"}"
[ "$estado" = "201" ] || morir "no se pudo configurar el administrador (HTTP $estado)."
pedir POST /auth/token "{\"email\":\"$correo_admin\",\"password\":\"$clave_admin\"}"
acceso_admin="$(campo accessToken "$cuerpo")"

correo_alumno="verif.alumno@ejemplo.test"; clave_alumno="$(clave)"
pedir POST /cuentas "{\"email\":\"$correo_alumno\",\"firstName\":\"Ana\",\"lastName\":\"Diaz\"}"
id_alumno="$(campo accountId "$cuerpo")"
[ -n "$id_alumno" ] || morir "no se pudo dar de alta al alumno (HTTP $estado)."
pedir POST "/cuentas/$id_alumno/situacion" \
  "{\"accountId\":\"$id_alumno\",\"intendedStatus\":\"Enabled\"}" "$acceso_admin"
provisional="$(campo provisionalPassword "$cuerpo")"
pedir POST /cuenta/contrasena \
  "{\"email\":\"$correo_alumno\",\"currentPassword\":\"$provisional\",\"newPassword\":\"$clave_alumno\"}"
pedir POST /auth/token "{\"email\":\"$correo_alumno\",\"password\":\"$clave_alumno\"}"
acceso_alumno="$(campo accessToken "$cuerpo")"
[ -n "$acceso_alumno" ] || morir "el alumno no pudo entrar."

texto="$(awk -f "$raiz/samples/web/01-datos-seed/datos/escapar.awk" \
             "$raiz/samples/web/01-datos-seed/datos/E1.txt")"
nombre_trabajo="Cubo y ortoedro"
pedir POST /trabajos \
  "{\"name\":\"$nombre_trabajo\",\"declaredDate\":\"2026-08-30\",\"description\":null,\"originalJson\":$texto}" \
  "$acceso_alumno"
id_trabajo="$(campo workId "$cuerpo")"
estado_trabajo="$(campo status "$cuerpo")"
[ -n "$id_trabajo" ] || morir "no se pudo cargar el trabajo (HTTP $estado)."
[ "$estado_trabajo" = "Submitted" ] || morir "el trabajo quedó en '$estado_trabajo' y no en 'Submitted'."

echo
echo "Trabajo «$nombre_trabajo» en estado $estado_trabajo · $id_trabajo"
echo "---------------------------------------------------------------------------"

docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp --network host \
  -v "$raiz/tools:/t" -w /t "$imagen_pw" \
  bash -c "npm install --no-save playwright@1.48.0 >/dev/null 2>&1 && \
           node verificar-resolucion-del-trabajo.mjs '$base_web' '$correo_admin' '$clave_admin' \
                '$id_trabajo' '$nombre_trabajo'"
veredicto=$?

# ---- EL PASO 6 LO CONTESTA EL SERVICIO DE DATOS, NO LA PANTALLA ------------
# Que la pieza pública haya navegado bien no prueba que el desenlace se aplicó.
# Lo único que lo prueba es preguntárselo a quien guarda el dato.
pedir GET "/trabajos/$id_trabajo" "" "$acceso_admin"
final="$(campo status "$cuerpo")"
echo "---------------------------------------------------------------------------"
if [ "$final" = "Approved" ]; then
  echo "PASA  6. El servicio de datos dice que el trabajo quedó en $final —«Finalizado»—"
else
  echo "FALLA 6. El servicio de datos dice que el trabajo quedó en '$final', no en 'Approved'"
  veredicto=1
fi

echo
[ "$veredicto" -eq 0 ] && echo "CONFORME · el botón de aprobar hace lo que dice" \
                       || echo "NO CONFORME · ver los pasos de arriba"
exit "$veredicto"
