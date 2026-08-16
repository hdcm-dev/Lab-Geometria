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
# Y, después de los cuatro, DOS controles que NO son criterios de transición sino
# las puertas de dos guardianes de `Web ADR-03` §2. Van acá porque este guion es
# el único que levanta la pieza pública como `Production`, que es donde los
# guardianes rigen sin ninguna salvedad. `scripts/verify-navigation.sh` corre en
# desarrollo y con la puerta de servicio puesta, y por eso allá las mismas rutas
# responden 200.
#
#   guardián 2  «Ninguna ruta del panel es accesible sin sesión».
#   guardián 1  «Una vez que existe la cuenta de administrador, la ruta del
#               aprovisionamiento deja de armar formulario para siempre y desvía
#               de forma neutra, sin explicar por qué». Acá se mide LA SEGUNDA
#               MITAD, que es la que este guion puede medir: cuando el front se
#               levanta, `C-1` ya configuró el administrador. La primera mitad
#               —sin administrador toda ruta desvía al aprovisionamiento— y **la
#               transición entre las dos sin reiniciar la pieza pública** las mide
#               `ProvisioningGateTests`, que puede gobernar los dos estados.
#
# POR ESO `/aprovisionamiento-inicial` SALIÓ DE LA LISTA DE PÚBLICAS DE C-4 Y DEL
# GUARDIÁN 2, y no es que haya dejado de ser pública: es que con administrador
# configurado ya no tiene nada que ofrecer, y exigirle 200 sería exigir el defecto
# que el guardián 1 vino a cerrar. Hasta esta entrega este guion pedía esa ruta y
# esperaba 200 **con el administrador ya configurado**, es decir, medía y bendecía
# el formulario que se seguía sirviendo a cualquier anónimo.
#
# LAS DOS PIEZAS SE CORREN EN `Release`, Y NO ES UN DETALLE. Este guion construye la
# solución con `dotnet build -c Release` y hasta la etapa `d` la levantaba con
# `dotnet run --no-build` **sin decir la configuración**, con lo cual `dotnet run`
# resolvía la salida de `Debug`: el guion construía una cosa y verificaba otra, y
# lo que verificaba podía ser un binario viejo de cualquier antigüedad. El defecto
# no se notó mientras los cuatro criterios de la etapa `c` existieron en las dos
# salidas; se hizo visible al agregarse los puntos de la etapa `d`, que en `Debug`
# no estaban y respondían `404`. Se corrige agregando `-c Release` a los dos
# `dotnet run`, de modo que lo que se levanta sea exactamente lo que se construyó.
#
# ESTE GUION SE QUEDA EN `Release` AUNQUE `run-api.sh` Y `run-web.sh` HAYAN PASADO A
# `Debug`, Y LA ASIMETRÍA ESTÁ PUESTA A PROPÓSITO. El Product Owner decidió que en
# desarrollo se trabaja en `Debug`, y los guiones de ejecución lo declaran así. Los de
# VERIFICACIÓN no, porque su trabajo es distinto: miden **lo que efectivamente se
# despliega**, y lo que se despliega es `Release` —`deploy/Dockerfile` publica `-c
# Release` y el flujo del front transfiere `Release`—. Una puerta que verifica una
# salida distinta de la que sale a producción es el defecto que los párrafos de arriba
# documentan, con otro disfraz.
#
# QUE NADIE LA «CORRIJA» POR SIMETRÍA MÁS ADELANTE: no es una inconsistencia olvidada
# entre guiones, es la decisión. Ejecutar y verificar no miden lo mismo y no tienen por
# qué usar la misma salida.
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

# LA CONSTRUCCIÓN SE COMPRUEBA, NO SE CONFÍA. La forma anterior —`dotnet build … | tail -3`—
# perdía el código de salida DOS veces: por la tubería y porque este guion corre sin `-e`. Con
# la construcción fallada, los `dotnet run --no-build` de más abajo levantaban la salida
# `Release` de la corrida ANTERIOR: el proceso arrancaba, respondía, y lo que se medía era
# código que ya no estaba en el árbol. Es el mismo defecto que la cabecera de este guion
# documenta en su otra variante, y acá estaba en la mitad que nadie miró.
scripts/assert-build-fresh.sh src/GeometriaFactory.Api/GeometriaFactory.Api.csproj Release || exit 1
scripts/assert-build-fresh.sh src/GeometriaFactory.Web/GeometriaFactory.Web.csproj Release || exit 1
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
  dotnet run --project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj -c Release --no-build \
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
  dotnet run --project src/GeometriaFactory.Web/GeometriaFactory.Web.csproj -c Release --no-build \
  --urls "$WEB" > /tmp/web-c.log 2>&1 &
WEB_PID=$!
for _ in $(seq 1 90); do curl -s -o /dev/null "$WEB/" && break || sleep 1; done

# 1 · Ninguna respuesta que el navegador recibe trae algo con forma de acceso firmado,
#     ni SIN sesión ni CON sesión abierta.
jwt=0
for r in / /registro-de-cuenta /ingreso /mi-contrasena /entrega-comision /cuentas /mis-trabajos /estado; do
  if curl -s "$WEB$r" | grep -qE 'eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.'; then
    bad "$r trae algo con forma de acceso firmado"; jwt=1
  fi
done
[ "$jwt" -eq 0 ] && ok "0 ocurrencias con forma de acceso firmado en las ocho direcciones, sin sesión"

# 2 · LA MARCA DE SESIÓN, sobre la cabecera real. Reescrito: hasta que la sesión vivió sólo en
#     el estado del circuito, este control decía «cero cookies propias», y era una forma legítima
#     de comprobar que la credencial no bajaba al navegador. Con la marca de sesión construida
#     —`Web ADR-03` §2, «el navegador conserva una marca de sesión que NO LA TRANSPORTA»— esa
#     redacción ya no comprueba lo correcto: comprobaría la ausencia de una pieza que la ADR
#     manda tener. Lo que hay que comprobar, y es lo que este control hace ahora, es que la marca
#     EXISTA, que lleve los tres atributos que el intake §17.6 declara, y que NO LLEVE EL TESTIGO
#     —ni el literal exacto ni nada con su forma—.
printf '\n   -- se entra de verdad, para mirar la marca que el navegador recibe --\n'
TOKEN_2=$(token_for "$PASS_2")
curl -s -c /tmp/jar-web -o /tmp/ingreso.html "$WEB/ingreso"
AF_TOKEN=$(sed -n 's/.*name="__RequestVerificationToken" value="\([^"]*\)".*/\1/p' /tmp/ingreso.html | head -1)
AF_COOKIE=$(grep -i 'Antiforgery' /tmp/jar-web | awk '{print $6"="$7}')
[ -n "$AF_TOKEN" ] && ok "el formulario de ingreso trae su testigo de antifalsificación" \
                   || bad "el formulario de ingreso no trae testigo de antifalsificación"

curl -s -D /tmp/entrada.h -o /dev/null -X POST "$WEB/ingreso" \
  -H "Cookie: $AF_COOKIE" \
  --data-urlencode "_handler=sign-in" \
  --data-urlencode "__RequestVerificationToken=$AF_TOKEN" \
  --data-urlencode "Input.Email=$EMAIL" \
  --data-urlencode "Input.Password=$PASS_2"

same "$(sed -n '1s/.*[0-9]\.[0-9] \([0-9]*\).*/\1/p' /tmp/entrada.h)" 302 "el ingreso responde con una redirección de verdad"
SET_COOKIE=$(grep -i '^set-cookie: gf.session=' /tmp/entrada.h | sed 's/^[Ss]et-[Cc]ookie: *//')
MARCA=$(echo "$SET_COOKIE" | cut -d';' -f1)
echo "        cabecera cruda: $(echo "$SET_COOKIE" | cut -c1-60)…$(echo "$SET_COOKIE" | sed 's/^[^;]*//')"

[ -n "$MARCA" ] && ok "el navegador recibe la marca de sesión" || bad "no llegó ninguna marca de sesión"
for atributo in httponly secure 'samesite=strict'; do
  echo "$SET_COOKIE" | grep -qi "$atributo" && ok "la marca lleva '$atributo'" || bad "la marca NO lleva '$atributo'"
done
case "$SET_COOKIE" in *"$TOKEN_2"*) bad "EL TESTIGO ESTÁ ADENTRO DE LA MARCA";; *) ok "el testigo NO está adentro de la marca";; esac
echo "$SET_COOKIE" | grep -qE 'eyJ[A-Za-z0-9_-]{6,}\.' \
  && bad "la marca trae algo con forma de acceso firmado" \
  || ok "la marca no trae nada con forma de acceso firmado"

# Y la marca sirve: la sesión sobrevive a una PETICIÓN NUEVA, que es lo que una recarga hace.
curl -s -H "Cookie: $MARCA" -o /tmp/panel.html -w '' "$WEB/entrega-comision"
grep -q "$EMAIL" /tmp/panel.html && ok "la sesión sobrevive a una petición nueva (el panel dibuja $EMAIL)" \
                                 || bad "la sesión no sobrevivió a una petición nueva"
grep -qE 'eyJ[A-Za-z0-9_-]{6,}\.[A-Za-z0-9_-]{6,}\.' /tmp/panel.html \
  && bad "el panel con sesión trae algo con forma de acceso firmado" \
  || ok "el panel con sesión no trae nada con forma de acceso firmado"
grep -q "$TOKEN_2" /tmp/panel.html && bad "el testigo literal está en el cuerpo servido" \
                                   || ok "el testigo literal NO está en el cuerpo servido"

# Y sin la marca no hay sesión: la misma dirección no dibuja identidad de nadie.
curl -s -o /tmp/anonimo.html "$WEB/entrega-comision"
grep -q "$EMAIL" /tmp/anonimo.html && bad "sin marca sigue dibujando la identidad" \
                                   || ok "sin la marca no se dibuja ninguna identidad"

# 3 · EL GUION PROPIO DEL NAVEGADOR: INVENTARIO CERRADO Y ALCANCE ACOTADO.
#
# ESTE CONTROL SE REESCRIBIÓ, Y EL MOTIVO IMPORTA MÁS QUE LA REDACCIÓN. Hasta la etapa `d` decía
# «cero guiones propios», con UMBRAL 0, y era una forma legítima de comprobar que nada del lado del
# navegador podía guardar la credencial ni hablar con el servicio de datos. Ese umbral tenía un
# costo que se hizo visible al construir el panel: **cuatro apartamientos declarados del panel de
# cuentas y del registro no se podían cerrar sin un guion** —copiar la provisoria en un gesto,
# dibujar el estado en curso, mantener la acción destructiva inhabilitada hasta que lo escrito
# coincide, y cerrar los diálogos con la tecla de escape confinando el foco—. El Product Owner
# autorizó un guion ACOTADO A ESAS CUATRO COSAS, con lo cual el umbral 0 dejó de medir lo correcto:
# mediría la ausencia de una pieza que el producto ahora manda tener.
#
# QUÉ MIDE AHORA, Y POR QUÉ SIGUE SIENDO UNA PUERTA Y NO UN PERMISO ABIERTO. Lo que el umbral 0
# impedía era que apareciera **lógica de producto o de identidad del lado del navegador**, y eso
# sigue impedido, por cuatro cuadres que no dependen de la buena voluntad de quien escriba el guion:
#
#   3.a  EL INVENTARIO ES CERRADO. Hay UN solo guion propio autorizado y se lo nombra literalmente.
#        Cualquier otro `<script src>` que no sea el tiempo de ejecución del marco falla. Un guion
#        nuevo no se puede colar: hay que agregarlo acá.
#   3.b  EL ARCHIVO NO PUEDE SALIR A LA RED NI TOCAR EL NAVEGADOR PERSISTENTE. `RA-01` y
#        `Web ADR-03` §2 se cuadran contra el texto del archivo, con umbral 0 cada uno. No hay
#        forma de que ese guion pida nada al servicio de datos ni guarde nada en ninguna parte.
#   3.c  EL ALCANCE ES UNA LISTA CERRADA DE NUEVE ATRIBUTOS, la de `Norma-De-Nomenclatura.md`
#        §6.17.3. El guion no busca ninguna superficie por nombre: hace lo que esos nueve atributos
#        le piden. Un comportamiento nuevo exige un atributo nuevo, y un atributo nuevo falla acá
#        hasta que se lo agregue **a la norma y a esta lista**, que es exactamente la puerta.
#   3.d  Y EL CUADRE VA EN LAS DOS DIRECCIONES: los atributos que el marcado declara también tienen
#        que estar en la lista. Así no puede quedar un lado prometiendo lo que el otro no hace.
#
# Lo que este control NO afloja: la lista no admite «cualquier cosa vale». Sigue siendo el mismo
# criterio de antes —nada de producto ni de identidad en el navegador—, expresado sobre la
# superficie que ahora existe en lugar de sobre su ausencia.
GUION=/interaction/surface-interaction.js
ARCHIVO=src/GeometriaFactory.Web/wwwroot$GUION
# Los NUEVE de la etapa `d` más los DOS que agrega la etapa `g`: el atributo con el que la
# pantalla le baja las piezas al guion, y la marca con la que el guion recuerda que ya dibujó esa
# escena. Los dos son **marcado servido**, no una vía nueva: el guion sigue sin pedir nada.
AUTORIZADOS='data-gf-copy-source data-gf-copy-label data-gf-copy-done data-gf-copy-unavailable
data-gf-pending data-gf-match-input data-gf-match-value data-gf-dialog data-gf-dialog-dismiss
data-gf-viewer-pieces data-gf-viewer-drawn data-gf-piece-node data-gf-piece-node-bound
data-gf-motion data-gf-motion-bound'

printf '   -- 3.a · inventario cerrado de guiones propios, sobre las ocho direcciones --\n'
ajenos=0
for r in / /registro-de-cuenta /ingreso /mi-contrasena /entrega-comision /cuentas /mis-trabajos /estado; do
  for s in $(curl -s "$WEB$r" | grep -oE '<script[^>]*src="[^"]*"' | sed 's/.*src="//;s/"//'); do
    case "$s" in
      _framework/*|/_framework/*) ;;
      interaction/surface-interaction.js|"$GUION") ;;
      *) bad "$r carga un guion propio NO autorizado: $s"; ajenos=$((ajenos + 1)) ;;
    esac
  done
done
[ "$ajenos" -eq 0 ] && ok "0 guiones propios fuera del único autorizado ($GUION) en las ocho direcciones"
curl -s -o /tmp/guion.js -w '' "$WEB$GUION"
[ -s /tmp/guion.js ] && ok "el guion autorizado se sirve ($(wc -c < /tmp/guion.js) bytes)" \
                     || bad "el guion autorizado NO se sirve"
# Y en el árbol tampoco hay otro: los `.js` de `wwwroot/js/` son el bundle del visor, artefacto
# generado que este guion no versiona ni sirve desde estas superficies.
otros=$(find src/GeometriaFactory.Web/wwwroot -name '*.js' -not -path '*/js/*' -not -path "*$GUION" | wc -l)
same "$otros" 0 "archivos de guion propios en el árbol fuera del autorizado"

printf '   -- 3.b · el guion no sale a la red y no toca el navegador persistente --\n'
for prohibido in 'fetch *\(' 'XMLHttpRequest' 'WebSocket' 'EventSource' 'sendBeacon' 'import *\(' \
                 'localStorage' 'sessionStorage' 'document\.cookie' 'Authorization' 'Bearer' \
                 'accessToken' 'ApiBaseUrl' '\.src *=' 'eval *\('; do
  n=$(grep -cE "$prohibido" "$ARCHIVO")
  same "$n" 0 "el guion no contiene \`$prohibido\`"
done

printf '   -- 3.c · el alcance del guion son los QUINCE atributos autorizados --\n'
usados=$(grep -oE 'data-gf-[a-z-]+' "$ARCHIVO" | sort -u)
echo "        atributos que el guion lee: $(echo "$usados" | tr '\n' ' ')"
fuera=$(comm -23 <(echo "$usados") <(echo "$AUTORIZADOS" | tr ' ' '\n' | sort -u))
if [ -z "$fuera" ]; then
  ok "$(echo "$usados" | wc -l) atributos leídos, todos dentro de los quince autorizados"
else
  bad "el guion lee atributos NO autorizados: $(echo "$fuera" | tr '\n' ' ')"
fi

printf '   -- 3.d · y el marcado no declara ninguno que la lista no tenga --\n'
declarados=$(grep -rhoE 'data-gf-[a-z-]+' --include='*.razor' src/GeometriaFactory.Web | sort -u)
sobrantes=$(comm -23 <(echo "$declarados") <(echo "$AUTORIZADOS" | tr ' ' '\n' | sort -u))
if [ -z "$sobrantes" ]; then
  ok "$(echo "$declarados" | wc -l) atributos declarados en el marcado, todos autorizados"
else
  bad "el marcado declara atributos NO autorizados: $(echo "$sobrantes" | tr '\n' ' ')"
fi

# 4 · Y NO HAY UNA LÍNEA DE CÓDIGO DE LA PIEZA PÚBLICA QUE PUDIERA ESCRIBIR EN EL NAVEGADOR.
#     ESTE CONTROL NO CAMBIA, Y NO TENÍA POR QUÉ: el guion autorizado es marcado servido, no
#     interoperación desde el servidor. La pieza pública sigue sin poder escribir en el navegador
#     por su cuenta, que es la mitad del umbral 0 que sigue vigente tal cual.
escrituras=$(grep -rnE 'localStorage|sessionStorage|document\.cookie|IJSRuntime|InvokeVoidAsync' \
  --include='*.razor' --include='*.cs' src/GeometriaFactory.Web | wc -l)
same "$escrituras" 0 "líneas de la pieza pública que podrían escribir en el navegador"

# ------------------------------------------------ guardián 2 de `ADR-03` §2 ---
printf '\n== GUARDIÁN 2 · ninguna ruta del panel es accesible sin sesión ==\n'
# La pieza corre como `Production`: acá el guardián rige sin excepción, y la
# opción `PanelWalkthroughWithoutSession` no está puesta ni haría nada si lo
# estuviera. Esto ACOTA lo que se ofrece y no hace cumplir nada: quien verifica
# la pertenencia y el papel sigue siendo el servicio de datos, en cada solicitud.
PANEL='/mis-trabajos /trabajo-nuevo /trabajos/T-1 /trabajos/T-1/editar /cuentas /entrega-comision /mi-contrasena'
PUBLICAS='/ /registro-de-cuenta /ingreso /credencial-propia/establecer /credencial-propia/cambio-obligado /estado /no-encontrado'

printf '   -- sin marca: las siete del panel desvían a /ingreso --\n'
for r in $PANEL; do
  code=$(curl -s -o /dev/null -w '%{http_code}' "$WEB$r")
  dest=$(curl -s -o /dev/null -D - "$WEB$r" | sed -n 's/^[Ll]ocation: *//p' | tr -d '\r')
  if [ "$code" = "302" ] && [ "$dest" = "/ingreso" ]; then ok "$r desvía a /ingreso ($code)"
  else bad "$r: esperado 302 → /ingreso, obtenido $code → ${dest:-(sin Location)}"; fi
done

printf '   -- sin marca: las públicas siguen respondiendo 200 --\n'
# `/credencial-propia/cambio-obligado` está en esta lista a propósito: se llega
# SIN sesión de trabajo (`INV-09`), y gatearla rompería `RN-13`.
for r in $PUBLICAS; do
  same "$(curl -s -o /dev/null -w '%{http_code}' "$WEB$r")" 200 "$r sigue siendo pública"
done

printf '   -- con marca válida: las siete del panel responden 200 --\n'
for r in $PANEL; do
  same "$(curl -s -H "Cookie: $MARCA" -o /dev/null -w '%{http_code}' "$WEB$r")" 200 "$r con sesión"
done

# ------------------------------------------------ guardián 1 de `ADR-03` §2 ---
printf '\n== GUARDIÁN 1 · con administrador, el aprovisionamiento deja de armar formulario ==\n'
# El administrador quedó configurado en `C-1`, de modo que acá rige la SEGUNDA MITAD.
# Esto ACOTA lo que se ofrece y no hace cumplir nada: la negativa al segundo
# administrador la sigue produciendo el servicio de datos, y este mismo guion ya la
# comprobó dos veces —`C-1` y después del reinicio— forzando la petición sin pasar
# por la pantalla.
APROV=/aprovisionamiento-inicial
code=$(curl -s -o /tmp/aprov.html -w '%{http_code}' "$WEB$APROV")
dest=$(curl -s -o /dev/null -D - "$WEB$APROV" | sed -n 's/^[Ll]ocation: *//p' | tr -d '\r')
same "$code" 302 "$APROV desvía en lugar de servirse"
same "$dest" /ingreso "el destino del desvío"

printf '   -- y el desvío es NEUTRO: ni formulario ni explicación --\n'
# El cuerpo de un desvío no lleva pantalla, y eso es exactamente lo que se verifica:
# CERO campos de formulario y CERO textos que digan por qué.
same "$(grep -ci '<input' /tmp/aprov.html)"  0 "campos de formulario en la respuesta"
same "$(grep -ci '<form' /tmp/aprov.html)"   0 "formularios en la respuesta"
same "$(grep -ci 'administrador' /tmp/aprov.html)" 0 "menciones al administrador en la respuesta"
case "$dest" in *\?*) bad "el destino lleva un motivo colgado de la dirección: $dest";; *) ok "el destino no lleva ningún motivo colgado";; esac

printf '   -- el recurso estático y el guion del navegador NO se desvían --\n'
for r in /css/app.css /interaction/surface-interaction.js; do
  same "$(curl -s -o /dev/null -w '%{http_code}' "$WEB$r")" 200 "$r se sirve"
done

printf '\n== RESULTADO ==\n'
[ "$fails" -eq 0 ] && { echo "CONFORME · los cuatro criterios de transición de la etapa \`c\` y los dos guardianes pasan"; exit 0; }
echo "NO CONFORME · $fails comprobacion(es) fallan"; exit 1
