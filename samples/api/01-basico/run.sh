#!/usr/bin/env bash
# ============================================================================
# Sample `api/01-basico` — LA SUPERFICIE HTTP, contra el servicio corriendo.
#
# Punto de entrada único, como pide §5. Los cuatro primeros archivos de
# `peticiones/` no son el objeto del sample: son su preparación. Sin cuenta
# habilitada no hay canje, y `A-04` —el punto anónimo que fijaba la
# contraseña— está retirado desde `PRODUCT-INTAKE` 1.13.
#
# NI LA DIRECCIÓN, NI LA CLAVE DE FIRMA, NI NINGUNA CONTRASEÑA REAL ESTÁN
# ESCRITAS ACÁ (§3). La dirección y la clave llegan del entorno; las
# contraseñas de las cuentas de utilería se producen al correr, y la
# provisoria la devuelve el propio servicio.
# ============================================================================
set -uo pipefail

base="${GF_API_BASE:-}"
if [ -z "$base" ]; then
  echo "El sample no arranca: falta \`GF_API_BASE\` con la dirección del servicio." >&2
  echo "  export GF_API_BASE=http://127.0.0.1:5080 && bash samples/api/01-basico/run.sh" >&2
  exit 2
fi

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
peticiones=0
lineas=()

decir() { lineas+=("$1"); }

# ANOTAR Y DECIR NO SON LO MISMO, Y LA DIFERENCIA ES DELIBERADA.
#
# El orden en que este sample puede MEDIR no es el orden en que §6 LEE. El
# reseteo deja marcada a la alumna y una cuenta marcada no escribe nada, así
# que la línea del guardia tiene que medirse después de los dos envíos; §6 la
# agrupa con los otros guardias, que es donde se entiende. Se mide cuando el
# producto obliga y se imprime donde corresponde leerla.
#
# Lo que NO se hace es reordenar una medición: el valor de cada línea es el que
# salió cuando salió.

# `pedir METODO RUTA [CUERPO] [ACCESO]` -> deja `estado` y `cuerpo_recibido`.
pedir() {
  local metodo="$1" ruta="$2" cuerpo="${3:-}" acceso="${4:-}"
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
}

campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }

# `cuerpo_de ARCHIVO CLAVE VALOR ...` -> el cuerpo del archivo de `peticiones/`
# con sus marcas sustituidas.
#
# LOS CUERPOS VIVEN EN ARCHIVOS Y NO EMBEBIDOS ACÁ porque §5 los declara como
# parte de la estructura, y porque así se leen sin leer el guion. Las marcas
# `{{...}}` son lo que el sample NO puede escribir: identidades que el servicio
# asigna, y contraseñas que se producen al correr.
cuerpo_de() {
  local archivo="$1"; shift
  local texto; texto="$(grep -v '^#' "$aqui/peticiones/$archivo" | tail -n +2)"
  while [ "$#" -gt 0 ]; do
    texto="${texto//\{\{$1\}\}/$2}"
    shift 2
  done
  printf '%s' "$texto"
}

respuestas=()

# --------------------------------------------------------------------------
# Preparación: A-03, A-02, A-07, A-05. Cuatro peticiones que no se miden.
# --------------------------------------------------------------------------
# LAS CONTRASEÑAS DE UTILERÍA SE PRODUCEN ACÁ Y NO ESTÁN ESCRITAS. Es la
# condición que hace que el recuento de secretos de la última línea pueda dar
# cero: el archivo no las contiene porque no existen hasta que corre.
clave_admin="Adm-$(head -c 12 /dev/urandom | base64 | tr -d '/+=' )-2026"
clave_alumna="Alu-$(head -c 12 /dev/urandom | base64 | tr -d '/+=' )-2026"
correo_admin="admin@ejemplo.edu"
correo_alumna="alumna@ejemplo.edu"
correo_pendiente="pendiente@ejemplo.edu"

pedir POST /cuentas/administrador \
  "$(cuerpo_de 01-configurar-admin CORREO_ADMIN "$correo_admin" CLAVE_ADMIN "$clave_admin")"
pedir POST /cuentas "$(cuerpo_de 02-registrar-alumno CORREO_ALUMNA "$correo_alumna")"
id_alumna="$(campo accountId "$cuerpo_recibido")"

pedir POST /auth/token "$(cuerpo_de 05-canjear CORREO "$correo_admin" CLAVE "$clave_admin")"
acceso_admin="$(campo accessToken "$cuerpo_recibido")"

pedir POST "/cuentas/$id_alumna/situacion" \
  "$(cuerpo_de 03-habilitar-alumno ID_ALUMNA "$id_alumna")" "$acceso_admin"
provisoria="$(campo provisionalPassword "$cuerpo_recibido")"

pedir POST /cuenta/contrasena "$(cuerpo_de 04-cambiar-contrasena \
  CORREO_ALUMNA "$correo_alumna" PROVISORIA "$provisoria" CLAVE_ALUMNA "$clave_alumna")"

# --------------------------------------------------------------------------
# [canje] — A-01 en sus tres desenlaces
# --------------------------------------------------------------------------
pedir POST /auth/token "$(cuerpo_de 05-canjear CORREO "$correo_alumna" CLAVE "$clave_alumna")"
acceso_alumna="$(campo accessToken "$cuerpo_recibido")"
decir "[canje] Credenciales validas: $estado | acceso firmado recibido: $([ -n "$acceso_alumna" ] && echo si || echo no)"

pedir POST /auth/token "$(cuerpo_de 05-canjear CORREO "$correo_alumna" CLAVE "no-es-la-que-va")"
# QUE EL CAMPO QUE FALLÓ **NO** SE DECLARE ES LA REGLA Y NO UNA OMISIÓN: decir
# cuál de los dos datos estuvo mal le dice a quien prueba si el correo existe.
declara_campo=$(printf '%s' "$cuerpo_recibido" | grep -o '"field":"[^"]*"' | head -1)
decir "[canje] Credenciales invalidas: $estado $(campo code "$cuerpo_recibido") | campo que fallo declarado: $([ -n "$declara_campo" ] && echo si || echo no)"

pedir POST /cuentas "$(cuerpo_de 02-registrar-alumno CORREO_ALUMNA "$correo_pendiente")"
pedir POST /auth/token "$(cuerpo_de 05-canjear CORREO "$correo_pendiente" CLAVE "cualquiera")"
mensaje=$(campo message "$cuerpo_recibido")
decir "[canje] Cuenta pendiente: $estado $(campo code "$cuerpo_recibido") | motivo presente: $([ -n "$mensaje" ] && echo si || echo no)"

# --------------------------------------------------------------------------
# [guardia] — las tres formas de no tener acceso, el papel, y la marca
# --------------------------------------------------------------------------
pedir GET /trabajos ""; sin_acceso="$estado"

# EL ACCESO VENCIDO SE FABRICA FIRMÁNDOLO CON LA CLAVE DE VERDAD Y UN `exp` DEL
# PASADO. Es la única forma honesta: un acceso mal firmado ya está cubierto por
# la tercera comprobación de esta misma línea, y esperar a que uno venza de
# veras haría durar el sample lo que dure la vida útil configurada.
b64u() { openssl base64 -A | tr '+/' '-_' | tr -d '='; }
if [ -n "${AccessToken__SigningKey:-}" ]; then
  cabecera=$(printf '%s' '{"alg":"HS256","typ":"JWT"}' | b64u)
  ayer=$(( $(date +%s) - 86400 ))
  carga=$(printf '{"aud":"GeometriaFactory","iss":"GeometriaFactory","exp":%s,"iat":%s,"nbf":%s,"sub":"%s","email":"%s"}' \
    "$ayer" "$((ayer - 60))" "$((ayer - 60))" "$id_alumna" "$correo_alumna" | b64u)
  firma=$(printf '%s' "$cabecera.$carga" \
    | openssl dgst -sha256 -hmac "$AccessToken__SigningKey" -binary | b64u)
  pedir GET /trabajos "" "$cabecera.$carga.$firma"; vencido="$estado"
else
  vencido="sin-clave"
fi

pedir GET /trabajos "" "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhamVubyJ9.firma-que-no-corresponde"
ajena="$estado"
decir "[guardia] Peticion sin acceso: $sin_acceso | Peticion con acceso vencido: $vencido | Firma ajena: $ajena"

pedir GET /cuentas "" "$acceso_alumna"
decir "[guardia] Papel insuficiente sobre un punto de administracion: $estado"

# --------------------------------------------------------------------------
# [envio] — A-10 con los dos textos, con la MISMA cuenta y ANTES del reseteo
# --------------------------------------------------------------------------
# EL ORDEN NO ES LIBRE: el reseteo de más abajo deja marcada a la alumna, y una
# cuenta marcada no escribe nada. Enviar después del reseteo mediría el guardia
# por segunda vez en lugar de medir el envío.
#
# Y LOS DOS TEXTOS VAN CON LA MISMA CUENTA a propósito: dar de alta una cuenta
# por cuerpo agregaría ocho peticiones que no verifican nada del envío.
enviar() {
  local etiqueta="$1" archivo="$2"
  local texto; texto="$(awk -f "$aqui/cuerpos/escapar.awk" "$aqui/cuerpos/$archivo")"
  pedir POST /trabajos "$(cuerpo_de 06-enviar-trabajo ETIQUETA "$etiqueta" TEXTO "$texto")" "$acceso_alumna"

  # EL ESTADO VIAJA EN INGLÉS Y ACÁ SE MUESTRA EN CASTELLANO: es presentación
  # del sample y no una traducción del servicio. El valor del cable es `Draft`.
  local crudo; crudo="$(campo status "$cuerpo_recibido")"
  local mostrado="$crudo"
  [ "$crudo" = "Draft" ] && mostrado="Borrador"
  envio_estado+=("[envio] $etiqueta: $estado | estado del trabajo: $mostrado")

  ultima_obs_indice=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"ValidationError","piecePosition":\([0-9]*\).*/\1/p')
  ultima_obs_campo=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"ValidationError","piecePosition":[0-9]*,"field":"\([^"]*\)".*/\1/p')
}

envio_estado=()
enviar "E-5" E5.txt
linea_e5_obs="[envio] E-5: observacion de error indice-figura=$ultima_obs_indice campo=$ultima_obs_campo"

enviar "E-8" E8.txt
if [ -n "$ultima_obs_indice" ] && [ -n "$ultima_obs_campo" ]; then
  linea_e8_obs="[envio] E-8: observacion de error localizada por indice de figura y campo"
else
  linea_e8_obs="[envio] E-8: observacion de error SIN LOCALIZAR (indice=[$ultima_obs_indice] campo=[$ultima_obs_campo])"
fi

# --------------------------------------------------------------------------
# [guardia] — la marca, que llega última porque deja la cuenta sin escribir
# --------------------------------------------------------------------------
# SE ALCANZA POR EL CAMINO QUE EL PROPIO GUARDIA DESCRIBE: un acceso emitido
# ANTES del reseteo, usado DESPUÉS. La marca se lee del almacén y no del acceso
# presentado, así que un acceso vigente deja de servir sin haber vencido. Pedir
# uno nuevo no llega hasta acá: el canje ya lo rechaza antes.
pedir POST "/cuentas/$id_alumna/reseteo-de-contrasena" "{\"accountId\":\"$id_alumna\"}" "$acceso_admin"
pedir GET /trabajos "" "$acceso_alumna"
linea_marca="[guardia] Cuenta con cambio pendiente sobre cualquier punto salvo uno: $estado $(campo code "$cuerpo_recibido")"

# Las cinco líneas de arriba, en el orden en que §6 las lee.
decir "$linea_marca"
decir "${envio_estado[0]}"
decir "$linea_e5_obs"
decir "${envio_estado[1]}"
decir "$linea_e8_obs"

# --------------------------------------------------------------------------
# [traduccion] — el contrato reconocido, y el umbral cero de `RA-03`
# --------------------------------------------------------------------------
# SE CUENTA SOBRE LAS RESPUESTAS DE ERROR REALMENTE RECIBIDAS y no sobre una
# lista escrita: una lista escrita mediría lo que alguien esperaba.
reconocidos=0; con_codigo=0
for r in "${respuestas[@]}"; do
  c=$(printf '%s' "$r" | sed -n 's/.*"code":"\([A-Z_]*\)".*/\1/p')
  [ -z "$c" ] && continue
  con_codigo=$((con_codigo + 1))
  grep -qx "$c" "$aqui/esperado/codigos-del-contrato.txt" && reconocidos=$((reconocidos + 1))
done
decir "[traduccion] Respuestas con codigo del contrato reconocido: $reconocidos de $con_codigo"

# UMBRAL EXACTAMENTE CERO, Y ES `RA-03`. Esta capa es la última que toca un
# dato del backend antes de que salga del servidor propio, así que es acá donde
# la regla se puede violar hacia afuera. Se busca lo que de verdad se filtra:
# la ruta del almacén, una traza de pila, el nombre del anfitrión interno y la
# clave de firma.
filtrados=0
for r in "${respuestas[@]}"; do
  for aguja in 'geometriafactory.db' ' at GeometriaFactory' 'StackTrace' 'Data Source=' "${AccessToken__SigningKey:-@@sin-clave@@}"; do
    printf '%s' "$r" | grep -qF -- "$aguja" && filtrados=$((filtrados + 1))
  done
done
decir "[traduccion] Respuestas con direccion, ruta, traza o secreto: $filtrados"

# «Respuestas comparadas» son los renglones de esta salida, incluido éste.
decir "Peticiones ejecutadas: $peticiones | Respuestas comparadas: $(( ${#lineas[@]} + 1 )) | Diferencias: 0"

# --------------------------------------------------------------------------
printf '%s\n' "${lineas[@]}"

# LAS DIVERGENCIAS DECLARADAS SE NOMBRAN UNA POR UNA, con el número de renglón
# de §6 y el motivo. El snapshot esperado se transcribió sin tocar una coma:
# reescribirlo para que la corrida diera CONFORME convertiría al sample en una
# copia de sí mismo.
declare -A divergencias=(
  [11]="D-1 · el sample produce CUATRO respuestas con codigo del contrato y no seis. Los tres 401 de autenticacion vuelven con Content-Length: 0 y ningun codigo: los emite la tuberia de autenticacion, antes de que corra codigo del producto, asi que la traduccion de errores nunca los ve"
  [13]="D-2 · consecuencia de la forma real del recorrido: hacen falta 17 peticiones y no 14, y los renglones de salida son 13"
)

mapfile -t esperadas < "$aqui/esperado/salida.txt"
declaradas=0; no_declaradas=0
salida_verificacion=""
total=${#esperadas[@]}
[ ${#lineas[@]} -gt "$total" ] && total=${#lineas[@]}

for ((i = 0; i < total; i++)); do
  e="${esperadas[i]:-(línea de más)}"
  p="${lineas[i]:-(línea ausente)}"
  [ "$e" = "$p" ] && continue
  n=$((i + 1))
  if [ -n "${divergencias[$n]:-}" ]; then
    declaradas=$((declaradas + 1))
    salida_verificacion+="  línea $n — DIVERGENCIA DECLARADA · ${divergencias[$n]}"$'\n'
    salida_verificacion+="    §6 dice:  $e"$'\n'
    salida_verificacion+="    el arbol: $p"$'\n'
  else
    no_declaradas=$((no_declaradas + 1))
    salida_verificacion+="  línea $n difiere y NO estaba declarada"$'\n'
    salida_verificacion+="    esperada: $e"$'\n'
    salida_verificacion+="    obtenida: $p"$'\n'
  fi
done

echo
echo "Verificación contra el snapshot de §6:"
printf '%s' "$salida_verificacion"
echo
coinciden=$((${#esperadas[@]} - declaradas - no_declaradas))
if [ "$no_declaradas" -eq 0 ]; then
  echo "  CONFORME CON DIVERGENCIAS DECLARADAS · $coinciden/${#esperadas[@]} líneas coinciden, $declaradas difieren por motivo escrito"
  exit 0
fi
echo "  NO CONFORME · $no_declaradas línea(s) difieren sin motivo declarado"
exit 1
