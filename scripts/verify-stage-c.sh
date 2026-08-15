#!/usr/bin/env bash
# ============================================================================
# verify-stage-c.sh — Puerta de los CUATRO criterios de transición de la etapa
# `c` (`Roadmap-Producto.md` §5.2, `c` → `d`), contra los dos servicios de
# verdad, corriendo, sobre un almacén que no existía:
#
#   C-1  El administrador se configura en el primer arranque y SÓLO mientras
#        no exista ninguno.
#   C-2  Entrar, cambiar contraseña exigiendo la actual y salir funcionan, y
#        el cambio PERSISTE ENTRE REINICIOS.
#   C-3  Las actualizaciones de esquema se aplican solas sobre una base
#        inexistente.
#   C-4  La credencial de sesión no es observable desde el navegador.
#
# LA CLAVE DE FIRMA SE RECIBE Y NO SE BUSCA. Este guion la toma de
# `ACCESS_TOKEN_SIGNING_KEY` y, si no llega, usa una de prueba y lo dice. En
# ningún caso hay una clave escrita en el repositorio para producción.
#
# Se corre dentro del contenedor del SDK, desde la raíz del repositorio:
#   docker run --rm -v "$PWD":/w -w /w mcr.microsoft.com/dotnet/sdk:10.0 \
#     bash scripts/verify-stage-c.sh
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."

API_PORT=${API_PORT:-5081}
WEB_PORT=${WEB_PORT:-5091}
API=http://127.0.0.1:$API_PORT
WEB=http://127.0.0.1:$WEB_PORT
STORE=$(mktemp -d)/geometriafactory.db
KEY=${ACCESS_TOKEN_SIGNING_KEY:-clave-de-firma-solo-para-esta-verificacion-32+}
EMAIL=docente@frre.utn.edu.ar
PASS_1=la-primera-que-elegi
PASS_2=la-que-elegi-despues
fails=0

ok()   { echo "   OK   · $1"; }
bad()  { echo "   FALLA · $1"; fails=$((fails + 1)); }
same() { [ "$1" = "$2" ] && ok "$3 ($1)" || bad "$3: esperado $2, obtenido $1"; }

start_api() {
  ASPNETCORE_ENVIRONMENT=Production \
  ConnectionStrings__Store="Data Source=$STORE" \
  AccessToken__SigningKey="$KEY" \
  dotnet run --project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj --no-build \
    --urls "$API" > /tmp/api.log 2>&1 &
  API_PID=$!
  for _ in $(seq 1 90); do curl -s -o /dev/null "$API/salud" && return 0; sleep 1; done
  return 1
}
stop_api() { kill "${API_PID:-0}" 2>/dev/null; wait "${API_PID:-0}" 2>/dev/null; }

code_of() { # método ruta cuerpo [acceso]
  local extra=()
  [ -n "${4:-}" ] && extra=(-H "Authorization: Bearer $4")
  curl -s -o /tmp/body.json -w '%{http_code}' -X "$1" "$API$2" \
    -H 'Content-Type: application/json' "${extra[@]}" -d "$3"
}

token_for() { # contraseña
  curl -s -X POST "$API/auth/token" -H 'Content-Type: application/json' \
    -d "{\"email\":\"$EMAIL\",\"password\":\"$1\"}" \
    | sed -n 's/.*"accessToken":"\([^"]*\)".*/\1/p'
}

dotnet build GeometriaFactory.sln -c Release | tail -3
trap 'stop_api; kill ${WEB_PID:-0} 2>/dev/null' EXIT

# ---------------------------------------------------------------- C-3 ------
printf '\n== C-3 · el esquema se aplica solo sobre una base inexistente ==\n'
[ -f "$STORE" ] && bad "el archivo del almacén ya existía" || ok "el archivo del almacén NO existe todavía: $STORE"
start_api || { bad "el servicio de datos no arrancó"; tail -20 /tmp/api.log; exit 1; }
same "$(curl -s -o /dev/null -w '%{http_code}' "$API/salud")" 200 "el punto de salud responde"
curl -s "$API/salud" | grep -q '"ready":true' && ok "el almacén quedó preparado antes de atender" || bad "el almacén no quedó preparado"
[ -f "$STORE" ] && ok "el archivo del almacén existe después del arranque" || bad "el almacén no se creó"
# El esquema se busca en el archivo Y en su bitácora de escritura anticipada: SQLite abre en
# modo WAL, y hasta el primer punto de control la definición vive en el `-wal` y no en el `.db`.
grep -qa 'CREATE TABLE "Account"' "$STORE"* && ok 'la tabla Account está escrita en el almacén' || bad 'la tabla Account no está'
grep -qa 'UX_Account_SingleAdministrator' "$STORE"* && ok 'el índice único de administrador está escrito' || bad 'falta el índice único de administrador'
grep -qa '__EFMigrationsHistory' "$STORE"* && ok 'la transformación quedó asentada en el registro de esquema' || bad 'no hay registro de transformaciones'

# ---------------------------------------------------------------- C-1 ------
printf '\n== C-1 · el administrador se configura, y sólo mientras no exista ninguno ==\n'
SETUP="{\"email\":\"$EMAIL\",\"firstName\":\"Ana\",\"lastName\":\"Rossi\",\"password\":\"$PASS_1\"}"
same "$(code_of POST /cuentas/administrador "$SETUP")" 201 "primera configuración"
echo "        cuerpo: $(cat /tmp/body.json)"
OTRO="{\"email\":\"otro@frre.utn.edu.ar\",\"firstName\":\"Otro\",\"lastName\":\"Docente\",\"password\":\"$PASS_1\"}"
same "$(code_of POST /cuentas/administrador "$OTRO")" 409 "segunda configuración, con otro correo"
echo "        cuerpo: $(cat /tmp/body.json)"

# ---------------------------------------------------------------- C-2 ------
printf '\n== C-2 · entrar, cambiar exigiendo la actual, salir, y que persista ==\n'
TOKEN=$(token_for "$PASS_1")
[ -n "$TOKEN" ] && ok "el canje devolvió credencial de sesión" || bad "el canje no devolvió credencial"
CAMBIO="{\"currentPassword\":\"$PASS_1\",\"newPassword\":\"$PASS_2\"}"
MAL="{\"currentPassword\":\"no-es-la-mia\",\"newPassword\":\"$PASS_2\"}"
same "$(code_of POST /cuenta/contrasena "$CAMBIO")"          401 "cambio SIN credencial de sesión"
same "$(code_of POST /cuenta/contrasena "$MAL" "$TOKEN")"    401 "cambio con la actual equivocada"
same "$(code_of POST /cuenta/contrasena "$CAMBIO" "$TOKEN")" 200 "cambio con la actual correcta"
[ -z "$(token_for "$PASS_1")" ] && ok "la contraseña anterior dejó de servir" || bad "la anterior sigue sirviendo"
[ -n "$(token_for "$PASS_2")" ] && ok "la contraseña nueva sirve" || bad "la nueva no sirve"

echo "   -- reinicio del servicio de datos, mismo archivo de almacén --"
stop_api
start_api || { bad "el servicio no volvió a arrancar"; exit 1; }
[ -z "$(token_for "$PASS_1")" ] && ok "tras el reinicio, la anterior sigue sin servir" || bad "tras el reinicio la anterior sirve"
[ -n "$(token_for "$PASS_2")" ] && ok "tras el reinicio, la nueva sigue sirviendo" || bad "tras el reinicio la nueva no sirve"
same "$(code_of POST /cuentas/administrador "$OTRO")" 409 "tras el reinicio sigue habiendo administrador"

# ---------------------------------------------------------------- C-4 ------
printf '\n== C-4 · la credencial de sesión no es observable desde el navegador ==\n'
ApiBaseUrl="$API/" ASPNETCORE_ENVIRONMENT=Production \
  dotnet run --project src/GeometriaFactory.Web/GeometriaFactory.Web.csproj --no-build \
  --urls "$WEB" > /tmp/web-c.log 2>&1 &
WEB_PID=$!
for _ in $(seq 1 90); do curl -s -o /dev/null "$WEB/" && break || sleep 1; done

# 1 · Ninguna respuesta que el navegador recibe trae algo con forma de acceso firmado.
jwt=0
for r in / /aprovisionamiento-inicial /ingreso /mi-contrasena /entrega-comision /cuentas /mis-trabajos /estado; do
  if curl -s "$WEB$r" | grep -qE 'eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.'; then
    bad "$r trae algo con forma de acceso firmado"; jwt=1
  fi
done
[ "$jwt" -eq 0 ] && ok "0 ocurrencias con forma de acceso firmado en las ocho direcciones"

# 2 · Ninguna cookie propia. La única admitida es la de antifalsificación del marco.
cookies=$(curl -s -D - -o /dev/null "$WEB/ingreso" | grep -i '^set-cookie:' | sed 's/^[Ss]et-[Cc]ookie: *//')
propias=$(echo "$cookies" | grep -v '^$' | grep -viE 'Antiforgery' | wc -l)
same "$propias" 0 "cookies propias en la superficie de ingreso"
[ -n "$cookies" ] && echo "        cookies emitidas: $(echo "$cookies" | tr '\n' ' ')"

# 3 · El navegador no tiene NINGÚN guion propio que pudiera guardar nada.
guiones=$(curl -s "$WEB/ingreso" | grep -oE '<script[^>]*src="[^"]*"' | sed 's/.*src="//;s/"//' | tr '\n' ' ')
echo "        guiones que la página carga: $guiones"
case "$guiones" in *geometriafactory-visor.js*) bad "carga un guion propio en la superficie de ingreso";; *) ok "sólo el tiempo de ejecución del marco";; esac

# 4 · Y no hay una línea de código de la pieza pública que pudiera escribir en el navegador.
escrituras=$(grep -rnE 'localStorage|sessionStorage|document\.cookie|IJSRuntime|InvokeVoidAsync' \
  --include='*.razor' --include='*.cs' src/GeometriaFactory.Web | wc -l)
same "$escrituras" 0 "líneas de la pieza pública que podrían escribir en el navegador"

printf '\n== RESULTADO ==\n'
[ "$fails" -eq 0 ] && { echo "CONFORME · los cuatro criterios de transición de la etapa \`c\` pasan"; exit 0; }
echo "NO CONFORME · $fails comprobacion(es) fallan"; exit 1
