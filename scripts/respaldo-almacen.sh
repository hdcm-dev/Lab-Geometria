#!/usr/bin/env bash
# ============================================================================
# respaldo-almacen.sh — LA COPIA QUE EL PRODUCTO DECLARABA Y NO TENÍA.
#
# POR QUÉ EXISTE. `Entornos-Deploy.md` §11.1 declara: «el respaldo es el único
# mecanismo del producto para volver atrás sobre datos», y
# `Guia-Publicacion-Image-Docker.md` §4: «volver a la etiqueta no deshace el
# esquema del almacén […] lo único que restituye datos es el respaldo». Hasta el
# 2026-08-31 ese mecanismo NO EXISTÍA en ningún árbol: el barrido de `scripts/`,
# `deploy/` y `.github/` devolvía una sola coincidencia, `reset-db.sh`, que es el
# guion que VACÍA el almacén. Es el hallazgo `MI-01` de la mesa del 2026-08-31.
#
# QUÉ NO DECIDE, y es deliberado. NO fija frecuencia, NI directorio por omisión,
# NI retención. Eso es `PD-04` y es del Product Owner —el intake lo declara «a
# definir por el docente»—. **Un número puesto acá se propaga como si fuera del
# producto.** El mecanismo y la política son cosas distintas.
#
# CUATRO PROPIEDADES QUE NO SE NEGOCIAN:
#
#   1. `VACUUM INTO` Y NO `cp`. Copiar el archivo con el proceso escribiendo
#      produce una copia que CASI SIEMPRE FUNCIONA y falla el día que hace falta.
#      `VACUUM INTO` toma una instantánea transaccionalmente consistente.
#   2. NO DETIENE EL SERVICIO. Un respaldo que exige parada es un respaldo que no
#      se toma. Con el almacén en WAL —que `StorePreparation` fija y comprueba
#      desde el 2026-08-31— los lectores no bloquean al escritor.
#   3. SE NIEGA si el destino es el directorio del almacén. Escribir la copia al
#      lado del original es la forma más común de perder las dos cosas juntas.
#   4. `PRAGMA integrity_check` SOBRE LA COPIA, y si no da `ok` LA BORRA y sale 1.
#      Una copia corrupta es peor que ninguna: ocupa el lugar de la que falta.
#
# DOS FORMAS DE USO:
#
#   scripts/respaldo-almacen.sh <directorio-destino>
#       Local. Resuelve el almacén por `store-path.sh`, igual que `run-api.sh` y
#       `reset-db.sh`, para que no pueda copiar un archivo distinto del que el
#       servicio usa.
#
#   scripts/respaldo-almacen.sh --desde-contenedor <servicio> <directorio-destino>
#       La del destino real. Corre `sqlite3` DENTRO del contenedor —por eso entra
#       en `deploy/Dockerfile`— y deja la copia en el directorio del anfitrión.
#       Se ejecuta desde el directorio donde vive la composición de despliegue;
#       este repositorio no la tiene.
#
# CÓDIGOS DE SALIDA, con la convención de `coverage.sh`:
#   0  la copia se tomó y VERIFICÓ
#   1  no se pudo tomar, o se tomó y no verificó (en cuyo caso se borró)
#   2  falta con qué: no hay `sqlite3`, o no hay almacén, o faltan argumentos
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

morir() { echo "RESPALDO NO TOMADO · $1" >&2; exit "${2:-1}"; }
uso() {
  echo "uso: respaldo-almacen.sh <directorio-destino>" >&2
  echo "     respaldo-almacen.sh --desde-contenedor <servicio> <directorio-destino>" >&2
  exit 2
}

modo_contenedor=0
servicio=""
if [ "${1:-}" = "--desde-contenedor" ]; then
  modo_contenedor=1
  servicio="${2:-}"; destino="${3:-}"
  [ -n "$servicio" ] && [ -n "$destino" ] || uso
else
  destino="${1:-}"
  [ -n "$destino" ] || uso
fi

# El sello va en el nombre y no en un subdirectorio: una copia que no dice cuándo se
# tomó obliga a mirar la fecha del archivo, que cualquier copia posterior altera.
sello="$(date -u +%Y%m%dT%H%M%SZ)"
nombre="geometriafactory-$sello.db"

mkdir -p "$destino" || morir "no se pudo crear el directorio de destino '$destino'." 2
destino="$(cd "$destino" && pwd)"

# ---------------------------------------------------------------------------
if [ "$modo_contenedor" = 1 ]; then
  command -v docker >/dev/null 2>&1 || morir "no hay 'docker' en esta máquina." 2
  # La ruta del almacén DENTRO del contenedor sale de su propia configuración, no de
  # una constante de este guion: `deploy/Dockerfile` la declara en `ConnectionStrings__Store`.
  interna="$(docker compose exec -T "$servicio" sh -lc \
      'printf %s "${ConnectionStrings__Store#*[Dd]ata [Ss]ource=}"' 2>/dev/null | sed 's/;.*//')"
  [ -n "$interna" ] || morir "el servicio '$servicio' no declara 'ConnectionStrings__Store'." 2

  docker compose exec -T "$servicio" sh -lc \
      "command -v sqlite3 >/dev/null 2>&1" \
    || morir "el contenedor '$servicio' no trae 'sqlite3'. La imagen lo instala desde el 2026-08-31: relanzalo." 2

  docker compose exec -T "$servicio" sh -lc \
      "sqlite3 '$interna' \"VACUUM INTO '/tmp/$nombre';\"" \
    || morir "el motor no pudo tomar la instantánea dentro del contenedor."
  docker compose cp "$servicio:/tmp/$nombre" "$destino/$nombre" >/dev/null \
    || morir "la instantánea se tomó y no se pudo traer al anfitrión."
  docker compose exec -T "$servicio" sh -lc "rm -f '/tmp/$nombre'" >/dev/null 2>&1
else
  command -v sqlite3 >/dev/null 2>&1 \
    || morir "no hay 'sqlite3' en esta máquina. Corré dentro del contenedor de desarrollo." 2

  # shellcheck disable=SC1091
  . "$raiz/scripts/store-path.sh"
  gf_resolve_store
  [ -f "$GF_STORE_FILE" ] || morir "no hay almacén en '$GF_STORE_FILE'." 2

  # PROPIEDAD 3. Se compara el directorio RESUELTO, no la cadena: `.` y una ruta
  # relativa apuntan al mismo lugar y una comparación de texto no lo ve.
  if [ "$destino" = "$(cd "$(dirname "$GF_STORE_FILE")" && pwd)" ]; then
    morir "el destino es el directorio del almacén. La copia al lado del original se pierde con él."
  fi

  sqlite3 "$GF_STORE_FILE" "VACUUM INTO '$destino/$nombre';" \
    || morir "el motor no pudo tomar la instantánea. Si dice que el archivo existe, ya hay una copia con este sello."
fi

# ---------------------------------------------------------------------------
# PROPIEDAD 4. Se verifica la COPIA, no el original: lo que hay que poder restituir
# es ésta. Si no verifica se borra, porque una copia corrupta ocupa el lugar de la
# que falta y nadie descubre el hueco hasta que la necesita.
copia="$destino/$nombre"
[ -s "$copia" ] || morir "la copia quedó vacía."

# «NO PUDE VERIFICAR» Y «NO VERIFICÓ» SON COSAS DISTINTAS, y confundirlas destruyó copias sanas.
#
# La emisión del 2026-08-31 tenía una sola rama: si el veredicto no decía `ok`, borraba. Y el
# verificador del modo contenedor **no arrancaba nunca**: hacía `docker run` sobre una imagen cuyo
# `ENTRYPOINT` es `["dotnet","GeometriaFactory.Api.dll"]`, sin `--entrypoint sh`. El `2>/dev/null`
# se tragaba el error, el veredicto quedaba vacío, y la copia recién tomada **se borraba**. Corrido
# de punta a punta por la mesa del 2026-09-01: «RESPALDO NO TOMADO · la copia NO verificó ('sin
# respuesta') y se borró», con el destino vacío.
#
# ES EL PEOR MODO DE FALLA POSIBLE PARA UN RESPALDO: creés que lo tenés y no lo tenés.
#
# Ahora son tres desenlaces y no dos: verificó y sale 0; NO verificó y se borra, porque una copia
# corrupta ocupa el lugar de la que falta; y NO SE PUDO verificar, que **conserva la copia** y sale
# 1 diciendo por qué. Ante la duda se guarda: una copia sin verificar sirve más que ninguna.
verificar_copia() {
  if command -v sqlite3 >/dev/null 2>&1; then
    sqlite3 "$copia" 'PRAGMA integrity_check;' 2>&1 | head -1
    return
  fi
  # Sin `sqlite3` en el anfitrión se usa el del contenedor, que la imagen instala desde el
  # 2026-08-31. `--entrypoint sh` es obligatorio: sin él se ejecuta el servicio, no el comando.
  local img; img="$(docker compose images -q "$servicio" 2>/dev/null | head -1)"
  [ -n "$img" ] || { echo "SIN-VERIFICADOR: no se pudo resolver la imagen del servicio '$servicio'"; return; }
  docker run --rm --entrypoint sh -v "$destino:/c" -w /c "$img" \
    -lc "sqlite3 '/c/$nombre' 'PRAGMA integrity_check;'" 2>&1 | head -1
}
veredicto="$(verificar_copia)"

case "$veredicto" in
  ok)
    ;;
  SIN-VERIFICADOR:*|*"could not be loaded"*|*"not found"*|*"executable file"*|'')
    echo "COPIA TOMADA Y SIN VERIFICAR · $copia" >&2
    echo "  No se pudo correr la comprobación de integridad: ${veredicto:-sin respuesta}." >&2
    echo "  LA COPIA NO SE BORRÓ. Verificala a mano con:" >&2
    echo "    sqlite3 '$copia' 'PRAGMA integrity_check;'" >&2
    exit 1
    ;;
  *)
    rm -f "$copia"
    morir "la copia NO verificó ('$veredicto') y se borró. El almacén original no se tocó."
    ;;
esac

tamano="$(du -h "$copia" | cut -f1)"
echo "Copia verificada."
echo "  archivo : $copia"
echo "  tamaño  : $tamano"
echo "  integridad: ok"
echo
echo "La frecuencia, el directorio y la retención NO las decide este guion: son \`PD-04\`,"
echo "y el intake las declara «a definir por el docente»."
