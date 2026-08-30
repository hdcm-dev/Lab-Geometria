#!/usr/bin/env bash
# ============================================================================
# Sample `web/01-datos-seed` — SIEMBRA Y VERIFICA.
#
# Deja armado el dato con el que después se demuestra el producto: un docente,
# dos alumnos, los ocho escenarios enviados y dos de ellos resueltos.
#
# NO HACE FALTA NAVEGADOR NI LA PIEZA PÚBLICA CONSTRUIDA, y es deliberado: lo que
# este sample deja es el DATO. Lo que la pieza pública hace con él es materia del
# guion de demostración.
#
# NI LA DIRECCIÓN NI NINGUNA CREDENCIAL REAL ESTÁN ESCRITAS ACÁ. La dirección
# llega en `GF_API_BASE` (`ADR-10007`); las contraseñas se producen al correr y
# la provisoria la devuelve el servicio. Las identidades ficticias salen de
# `identidades.env.ejemplo`.
# ============================================================================
set -uo pipefail

base="${GF_API_BASE:-}"
if [ -z "$base" ]; then
  echo "El sample no arranca: falta \`GF_API_BASE\` con la dirección del servicio de datos." >&2
  exit 2
fi

aqui="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
# shellcheck disable=SC1091
. "$aqui/identidades.env.ejemplo"

lineas=()
fallas=0
verificaciones=0
decir() { lineas+=("$1"); }

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
}

campo() { printf '%s' "$2" | sed -n "s/.*\"$1\":\"\([^\"]*\)\".*/\1/p"; }
contar() { printf '%s' "$2" | grep -o "$1" | wc -l | tr -d ' '; }
clave() { printf 'Sd-%s-2026' "$(head -c 12 /dev/urandom | base64 | tr -d '/+=')"; }
castellano() {
  case "$1" in
    Draft) echo Borrador ;; Submitted) echo Pendiente ;;
    Approved) echo Aprobado ;; Rejected) echo Rechazado ;; *) echo "$1" ;;
  esac
}
verificar() { verificaciones=$((verificaciones + 1)); [ "$1" = "$2" ] || fallas=$((fallas + 1)); }

# --------------------------------------------------------------------------
# Siembra
# --------------------------------------------------------------------------
clave_admin="$(clave)"
pedir POST /cuentas/administrador \
  "{\"email\":\"$GF_SEED_ADMIN_CORREO\",\"firstName\":\"$GF_SEED_ADMIN_NOMBRE\",\"lastName\":\"$GF_SEED_ADMIN_APELLIDO\",\"password\":\"$clave_admin\"}"
decir "[seed] Administrador configurado: $([ "$estado" = "201" ] && echo si || echo no)"

pedir POST /auth/token "{\"email\":\"$GF_SEED_ADMIN_CORREO\",\"password\":\"$clave_admin\"}"
acceso_admin="$(campo accessToken "$cuerpo_recibido")"

# EL ALTA Y LA HABILITACIÓN SON DOS ACTOS (RN-16): la cuenta nace `Pending` y sin
# credencial; la provisoria la produce el servicio, y el alumno la cambia.
habilitados=0
declare -A acceso_de=()
alta_de_alumno() {
  local correo="$1" nombre="$2" apellido="$3"
  local suya; suya="$(clave)"
  pedir POST /cuentas "{\"email\":\"$correo\",\"firstName\":\"$nombre\",\"lastName\":\"$apellido\"}"
  local id; id="$(campo accountId "$cuerpo_recibido")"
  pedir POST "/cuentas/$id/situacion" "{\"accountId\":\"$id\",\"intendedStatus\":\"Enabled\"}" "$acceso_admin"
  local provisoria; provisoria="$(campo provisionalPassword "$cuerpo_recibido")"
  [ "$estado" = "200" ] && habilitados=$((habilitados + 1))
  pedir POST /cuenta/contrasena \
    "{\"email\":\"$correo\",\"currentPassword\":\"$provisoria\",\"newPassword\":\"$suya\"}"
  pedir POST /auth/token "{\"email\":\"$correo\",\"password\":\"$suya\"}"
  acceso_de["$correo"]="$(campo accessToken "$cuerpo_recibido")"
}

alta_de_alumno "$GF_SEED_ALUMNO1_CORREO" "$GF_SEED_ALUMNO1_NOMBRE" "$GF_SEED_ALUMNO1_APELLIDO"
alta_de_alumno "$GF_SEED_ALUMNO2_CORREO" "$GF_SEED_ALUMNO2_NOMBRE" "$GF_SEED_ALUMNO2_APELLIDO"
decir "[seed] Cuentas de alumno habilitadas: $habilitados"

# LOS OCHO VAN A LA MISMA CUENTA. El listado propio del alumno 1 tiene que dar 8,
# y la segunda cuenta existe para que la comisión no sea de una sola persona.
acceso_alumno1="${acceso_de[$GF_SEED_ALUMNO1_CORREO]}"
declare -A trabajo_de=()
declare -A texto_de=()
enviados=0
for e in E1 E2 E3 E4 E5 E6 E7 E8; do
  # EL TEXTO SE TRANSPORTA CARÁCTER POR CARÁCTER. `escapar.awk` escapa lo que JSON
  # exige y NADA MÁS: no reordena, no compacta y no reindenta. La fila `SD-10036`
  # declara deriva mayor, sin gradación, ante cualquier normalización.
  texto="$(awk -f "$aqui/datos/escapar.awk" "$aqui/datos/$e.txt")"
  texto_de[$e]="$texto"
  pedir POST /trabajos \
    "{\"name\":\"Escenario $e\",\"declaredDate\":\"2026-08-30\",\"description\":null,\"originalJson\":$texto}" \
    "$acceso_alumno1"
  [ "$estado" = "201" ] && enviados=$((enviados + 1))
  trabajo_de[$e]="$(campo workId "$cuerpo_recibido")"
done
decir "[seed] Trabajos enviados con los ocho escenarios: $enviados"

# DOS RESUELTOS Y NO NINGUNO: sin un aprobado y un rechazado, la pieza pública no
# tiene con qué mostrar el desenlace, y el seed dejaría un hueco justo donde el
# producto se demuestra.
pedir POST "/trabajos/${trabajo_de[E1]}/desenlace" \
  "{\"workId\":\"${trabajo_de[E1]}\",\"outcome\":\"Approve\",\"comment\":\"Prolijo y completo.\"}" "$acceso_admin"
pedir POST "/trabajos/${trabajo_de[E2]}/desenlace" \
  "{\"workId\":\"${trabajo_de[E2]}\",\"outcome\":\"Reject\",\"comment\":null}" "$acceso_admin"

pedir GET /trabajos "" "$acceso_alumno1"
listado_propio="$cuerpo_recibido"
# SE CUENTA CADA ESTADO POR SU LITERAL Y NO PARTIENDO EL LISTADO EN FILAS. La
# primera versión partía por `},{` y leía el estado de cada trozo con una
# expresión codiciosa: cuando un trozo traía dos elementos, se contaba uno solo.
# Daba `Aprobado=0` mientras el renglón de al lado —que sí contaba por literal—
# decía que había dos trabajos con desenlace. Dos mediciones de lo mismo que no
# coincidían, y la que estaba mal era la más elaborada.
declare -A cuantos=()
for par in Submitted:Pendiente Draft:Borrador Approved:Aprobado Rejected:Rechazado; do
  cuantos[${par#*:}]=$(contar "\"status\":\"${par%%:*}\"" "$listado_propio")
done
decir "[seed] Estados resultantes: Pendiente=${cuantos[Pendiente]} Borrador=${cuantos[Borrador]} Aprobado=${cuantos[Aprobado]} Rechazado=${cuantos[Rechazado]}"

# --------------------------------------------------------------------------
# Verificación
# --------------------------------------------------------------------------
propios=$(contar '"workId":' "$listado_propio")
verificar "$propios" 8
decir "[verif] Listado propio del alumno 1: $propios trabajos"

con_desenlace=$(( $(contar '"status":"Approved"' "$listado_propio") + $(contar '"status":"Rejected"' "$listado_propio") ))
verificar "$con_desenlace" 2
decir "[verif] Trabajos con desenlace visible en el listado propio: $con_desenlace"

# LAS DOS LÍNEAS QUE SIGUEN SON LA RAZÓN DE SER DEL SEED. El alumno ve 8 y el
# administrador 6: la diferencia son exactamente los dos borradores, y `RN-10011`
# no admite que se vea ninguno. Un seed que dejara los ocho en el mismo estado
# haría invisible esa diferencia justo en el dato con el que se demuestra.
pedir GET /trabajos "" "$acceso_admin"
de_la_comision=$(contar '"workId":' "$cuerpo_recibido")
verificar "$de_la_comision" 6
decir "[verif] Listado de la comision pedido por el administrador: $de_la_comision trabajos"

borradores=$(contar '"status":"Draft"' "$cuerpo_recibido")
verificar "$borradores" 0
decir "[verif] Borradores visibles en el listado de la comision: $borradores"

# `RN-10008` MEDIDO DONDE SE PUEDE MEDIR: se envía el texto y se lo vuelve a leer
# del detalle. Si algo lo normalizó en el camino —el borde del servicio, el
# transporte o el propio sample—, esta línea lo dice antes de que nadie abra una
# pantalla. Se compara contra el mismo literal que se mandó, no contra el archivo.
pedir GET "/trabajos/${trabajo_de[E2]}" "" "$acceso_alumno1"
leido="$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"originalJson":\("\(\\.\|[^"\\]\)*"\).*/\1/p')"
identico=no
[ "$leido" = "${texto_de[E2]}" ] && identico=si
verificar "$identico" si
decir "[verif] Detalle de un trabajo: texto original identico al enviado: $identico"

# EL PAR `E-3` CONTRA `E-4` ESTÁ EN EL SEED A PROPÓSITO: el mismo cubo de lado 3
# emitido por los dos ejemplos de la cátedra. El primero advierte con su par de
# valores y el segundo produce CERO. La fila `SD-10033` exige que los dos valores
# se muestren sin reformatear, y acá se leen tal como el servicio los devuelve.
dos_decimales() { awk -v v="$1" 'BEGIN{printf "%.2f", v}'; }
observaciones_de() { pedir GET "/trabajos/${trabajo_de[$1]}" "" "$acceso_alumno1"; }

observaciones_de E3
declarado=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"Warning","piecePosition":[0-9]*,"field":"Area","declaredValue":\([0-9.]*\).*/\1/p' | head -1)
derivado=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"Warning","piecePosition":[0-9]*,"field":"Area","declaredValue":[0-9.]*,"derivedValue":\([0-9.]*\).*/\1/p' | head -1)
# LAS OBSERVACIONES SE CUENTAN POR `piecePosition`, que es suyo y sólo suyo. La
# primera versión contaba `"kind":` y daba treinta y seis: el detalle trae también
# el árbol del texto, y sus nodos llevan un `kind` propio. Contar una clave que
# dos estructuras comparten mide las dos.
cuantas3=$(contar '"piecePosition":' "$cuerpo_recibido")
verificar "$cuantas3" 1
decir "[verif] Observaciones de E-3: $cuantas3 advertencia de area declarado=$(dos_decimales "$declarado") derivado=$(dos_decimales "$derivado")"

observaciones_de E4
cuantas4=$(contar '"piecePosition":' "$cuerpo_recibido")
verificar "$cuantas4" 0
decir "[verif] Observaciones de E-4: $cuantas4"

observaciones_de E5
cuantas5=$(contar '"piecePosition":' "$cuerpo_recibido")
indice5=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"ValidationError","piecePosition":\([0-9]*\).*/\1/p' | head -1)
campo5=$(printf '%s' "$cuerpo_recibido" | sed -n 's/.*"kind":"ValidationError","piecePosition":[0-9]*,"field":"\([^"]*\)".*/\1/p' | head -1)
verificar "$cuantas5" 1
decir "[verif] Observaciones de E-5: $cuantas5 error indice-figura=$indice5 campo=$campo5"

decir "Seed completo | Trabajos: $enviados | Verificaciones: $verificaciones | Fallas: $fallas"

# --------------------------------------------------------------------------
printf '%s\n' "${lineas[@]}"

declare -A divergencias=()

mapfile -t esperadas < "$aqui/verificacion/esperado.txt"
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
if [ "$no_declaradas" -eq 0 ] && [ "$fallas" -eq 0 ]; then
  if [ "$declaradas" -eq 0 ]; then
    echo "  CONFORME · las ${#esperadas[@]} líneas coinciden con el snapshot de §6"
  else
    echo "  CONFORME CON DIVERGENCIAS DECLARADAS · $coinciden/${#esperadas[@]} líneas coinciden, $declaradas por motivo escrito"
  fi
  exit 0
fi
echo "  NO CONFORME · $no_declaradas línea(s) sin motivo declarado, $fallas verificación(es) fallada(s)"
exit 1
