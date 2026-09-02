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

# EL ENTORNO ES UNA VARIABLE DE LA VERIFICACIÓN, y no un detalle de montaje. El
# anfitrión real corre en `Production` y sobre HTTPS, y el producto SE COMPORTA
# DISTINTO en los dos ejes: `Program.cs:30` marca la cookie de sesión como
# `Secure` fuera de `Development` —de modo que en Production sobre HTTP NO HAY
# SESIÓN POSIBLE—, y dos middlewares abren un paseo sin sesión sólo en
# `Development`. Verificar únicamente en `Development` deja sin mirar
# exactamente el entorno donde el Product Owner encuentra los defectos.
entorno="${GF_VERIF_ENTORNO:-Development}"
anfitrion="127.0.0.1"
if [ "$entorno" = "Development" ]; then
  esquema="http"; certificado=""
else
  esquema="https"
  # `dotnet dev-certs` emite el certificado PARA `localhost`, no para la dirección
  # numérica: pegándole a `127.0.0.1` el nombre no coincide y el origen nunca es
  # seguro por mucho que se acepte el emisor.
  anfitrion="localhost"
  # Certificado propio y efímero: no se instala nada en el anfitrión y el
  # navegador de la verificación acepta el emisor desconocido a propósito.
  certificado="/trabajo/verificacion.pfx"
fi
# EL SERVICIO DE DATOS SE QUEDA EN HTTP AUNQUE LA PIEZA PÚBLICA VAYA POR HTTPS, y
# eso NO es una comodidad: es la topología real. En somee el front se sirve por
# HTTPS y llama al contenedor de datos por `http://<ip>:8080`. Ponerle HTTPS al
# API en el banco introduce un problema QUE PRODUCCIÓN NO TIENE —el `HttpClient`
# de la pieza pública rechaza el certificado propio y todo responde «no podemos
# llegar a tus datos»—, y eso ya hizo perder un rato el 2026-09-01.
base_api="http://$anfitrion:$puerto_api"
base_web="$esquema://$anfitrion:$puerto_web"

imagen_sdk="mcr.microsoft.com/dotnet/sdk:10.0"
imagen_pw="mcr.microsoft.com/playwright:v1.48.0-jammy"

trabajo="$(mktemp -d)"
spki=""
dir_web="/repo"
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

echo "Entorno: $entorno · esquema: $esquema"
echo "Compilando…"
docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget \
  -v "$cache:/nuget" -v "$raiz:/repo" -w /repo "$imagen_sdk" \
  dotnet build GeometriaFactory.sln -c Release -v q --nologo >"$trabajo/build.log" 2>&1 \
  || { tail -20 "$trabajo/build.log" >&2; morir "la solución no compila."; }

clave_firma="$(head -c 48 /dev/urandom | base64 | tr -d '\n')"
clave_cert="$(head -c 18 /dev/urandom | base64 | tr -d '/+=')"

tls=()
if [ -n "$certificado" ]; then
  echo "Generando el certificado efímero para $esquema…"
  docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp -v "$trabajo:/trabajo" "$imagen_sdk" \
    dotnet dev-certs https -ep "$certificado" -p "$clave_cert" >/dev/null 2>&1 \
    || morir "no se pudo generar el certificado."
  tls=(-e Kestrel__Endpoints__Http__Certificate__Path="$certificado"
       -e Kestrel__Endpoints__Http__Certificate__Password="$clave_cert")

  # QUE EL NAVEGADOR CONFÍE, Y NO QUE IGNORE EL ERROR. No son lo mismo y la
  # diferencia rompió esta verificación el 2026-09-01: Chromium NO GUARDA
  # COOKIES `Secure` en un origen cuyo certificado no valida, aunque se le pase
  # `ignoreHTTPSErrors` —eso silencia el aviso, no vuelve seguro el origen—. Y en
  # `Production` la marca de sesión es `Secure` obligatoria (`Program.cs:30`), de
  # modo que el ingreso NO SE CONSERVABA y la verificación informaba «el bloque
  # no se dibujó» cuando en realidad el navegador estaba parado en `/ingreso`.
  #
  # Se le pasa la huella de la clave pública del certificado efímero, que es la
  # forma de que lo trate como válido SIN instalar nada en ningún almacén de
  # confianza ni bajar la guardia para ningún otro sitio.
  spki="$(docker run --rm -v "$trabajo:/trabajo" "$imagen_pw" bash -c \
    "openssl pkcs12 -in $certificado -nokeys -passin pass:$clave_cert 2>/dev/null \
     | openssl x509 -pubkey -noout \
     | openssl pkey -pubin -outform der 2>/dev/null \
     | openssl dgst -sha256 -binary | base64")"
  [ -n "$spki" ] || morir "no se pudo calcular la huella del certificado."
fi

echo "Levantando el servicio de datos en $base_api…"
cid_api="$(docker run -d -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget --network host \
  -v "$cache:/nuget" -v "$raiz:/repo" -v "$trabajo:/trabajo" -w /repo \
  -e ConnectionStrings__Store="Data Source=/trabajo/verificacion.db" \
  -e AccessToken__SigningKey="$clave_firma" \
  -e Kestrel__Endpoints__Http__Url="$base_api" \
  -e ASPNETCORE_ENVIRONMENT="$entorno" "$imagen_sdk" \
  dotnet run --project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj -c Release --no-build)"

# ============================================================================
# EN `Production` SE CORRE LA PUBLICACIÓN, NO EL PROYECTO. No es una diferencia
# de comodidad: `dotnet run` sirve los archivos del marco desde el manifiesto de
# recursos estáticos del proyecto, y fuera de `Development` ESO NO PASA. Medido
# el 2026-09-01 con la bitácora del navegador:
#
#     respuesta 404 · /_framework/blazor.web.js
#
# Sin ese guion NO HAY CIRCUITO, y entonces NINGÚN componente interactivo del
# producto funciona —los botones quedan dibujados y muertos—. Eso hizo creer
# durante un rato que el defecto reportado se reproducía, cuando en el anfitrión
# real ese mismo archivo responde 200: somee ejecuta la salida de
# `dotnet publish`, donde `wwwroot/_framework/` son archivos de verdad.
#
# UN BANCO QUE NO CORRE LO MISMO QUE EL ANFITRIÓN INVENTA DEFECTOS PROPIOS Y
# TAPA LOS AJENOS, que es lo peor que puede hacer una verificación.
if [ "$entorno" != "Development" ]; then
  echo "Publicando la pieza pública —es lo que corre el anfitrión real—…"
  docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget \
    -v "$cache:/nuget" -v "$raiz:/repo" -v "$trabajo:/trabajo" -w /repo "$imagen_sdk" \
    dotnet publish src/GeometriaFactory.Web/GeometriaFactory.Web.csproj -c Release \
      -o /trabajo/publicacion --nologo -v q >"$trabajo/publish.log" 2>&1 \
    || { tail -20 "$trabajo/publish.log" >&2; morir "no se pudo publicar la pieza pública."; }
  arranque_web=(dotnet /trabajo/publicacion/GeometriaFactory.Web.dll)
  # LA RAÍZ DE CONTENIDO ES LA CARPETA PUBLICADA, y sin esto el `wwwroot` no se
  # encuentra: la hoja de estilos, el guion de superficie y los archivos del
  # marco responden 404 y el sitio se dibuja desnudo y muerto. En el anfitrión
  # real la raíz del sitio ES la carpeta publicada, que es lo que se imita acá.
  dir_web="/trabajo/publicacion"
else
  arranque_web=(dotnet run --project src/GeometriaFactory.Web/GeometriaFactory.Web.csproj -c Release --no-build)
  dir_web="/repo"
fi

echo "Levantando la pieza pública en $base_web…"
# EL DIRECTORIO DE TRABAJO SE MONTA TAMBIÉN ACÁ, y no es simetría de adorno: en
# `Production` el certificado efímero vive ahí, y sin el montaje Kestrel no lo
# encuentra y la pieza pública NI ARRANCA.
cid_web="$(docker run -d -u "$(id -u):$(id -g)" -e HOME=/tmp -e NUGET_PACKAGES=/nuget --network host \
  -v "$cache:/nuget" -v "$raiz:/repo" -v "$trabajo:/trabajo" -w "$dir_web" \
  -e ApiBaseUrl="$base_api/" \
  -e Kestrel__Endpoints__Http__Url="$base_web" "${tls[@]}" \
  -e ASPNETCORE_ENVIRONMENT="$entorno" "$imagen_sdk" \
  "${arranque_web[@]}")"

esperar() {
  local url="$1" nombre="$2" i
  for i in $(seq 1 180); do
    curl -sk -o /dev/null --max-time 2 "$url" && return 0
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
  local s; s="$(curl -sk -w $'\n%{http_code}' -X "$m" "$base_api$r" "${h[@]}" "${d[@]}")"
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

# EL DATO DEL TRABAJO ES UNA VARIABLE, y hace falta que lo sea: un defecto que
# depende de lo que el alumno escribió no aparece con la muestra de siempre. Con
# `GF_VERIF_DATOS` se le pasa el texto original de un trabajo REAL —por ejemplo
# el que un reporte señala— y se verifica sobre ese.
datos="${GF_VERIF_DATOS:-$raiz/samples/web/01-datos-seed/datos/E1.txt}"
[ -f "$datos" ] || morir "no existe el archivo de datos '$datos'."
echo "Datos del trabajo: $datos"
texto="$(awk -f "$raiz/samples/web/01-datos-seed/datos/escapar.awk" "$datos")"
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
           GF_SPKI='$spki' node verificar-resolucion-del-trabajo.mjs '$base_web' '$correo_admin' '$clave_admin' \
                '$id_trabajo' '$nombre_trabajo'"
veredicto=$?

# ---- EL ACUSE DE LA ESCENA, EN LOS DOS ESTADOS -----------------------------
# Un acuse que sólo se probó con 3D disponible no prueba nada: el defecto vivía
# del otro lado. Ver `verificar-acuse-de-la-escena.mjs`.
echo "---------------------------------------------------------------------------"
docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp --network host \
  -v "$raiz/tools:/t" -w /t "$imagen_pw" \
  bash -c "npm install --no-save playwright@1.48.0 >/dev/null 2>&1 && \
           GF_SPKI='$spki' node verificar-acuse-de-la-escena.mjs '$base_web' '$correo_admin' \
             '$clave_admin' '$id_trabajo'" || veredicto=1

# ---- EL PASO 6 LO CONTESTA EL SERVICIO DE DATOS, NO LA PANTALLA ------------
# Que la pieza pública haya navegado bien no prueba que el desenlace se aplicó.
# Lo único que lo prueba es preguntárselo a quien guarda el dato.
pedir GET "/trabajos/$id_trabajo" "" "$acceso_admin"
final="$(campo status "$cuerpo")"
echo "---------------------------------------------------------------------------"
if [ "$final" = "Approved" ]; then
  echo "PASA  9. El servicio de datos dice que el trabajo quedó en $final —«Finalizado»—"
else
  echo "FALLA 9. El servicio de datos dice que el trabajo quedó en '$final', no en 'Approved'"
  veredicto=1
fi

echo
[ "$veredicto" -eq 0 ] && echo "CONFORME · el botón de aprobar hace lo que dice" \
                       || echo "NO CONFORME · ver los pasos de arriba"
exit "$veredicto"
