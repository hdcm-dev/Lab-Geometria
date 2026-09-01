#!/usr/bin/env bash
# ============================================================================
# restaurar-almacen.sh — LA OTRA MITAD, Y LA QUE SE HACE CON MIEDO.
#
# POR QUÉ EXISTE SEPARADO DEL RESPALDO. Un respaldo que nadie restituyó nunca no
# es un respaldo: es un archivo. `Entornos-Deploy.md` §11.1 declara la restitución
# como el único camino de vuelta sobre datos, y hasta el 2026-08-31 no había con
# qué hacerla. Hallazgo `MI-01` de la mesa del 2026-08-31.
#
# TRES PROPIEDADES, Y LA TERCERA ES LA QUE IMPORTA:
#
#   1. EXIGE EL SERVICIO DETENIDO, Y LO COMPRUEBA POR EL ARCHIVO. Se mira si
#      existe un `-wal` con contenido, no si hay un proceso corriendo: el proceso
#      puede estar en otro contenedor, en otra máquina o con otro nombre, y el
#      archivo dice la verdad en todos esos casos. Restituir bajo un escritor vivo
#      produce un almacén que el servicio pisa en el siguiente `checkpoint`.
#   2. VERIFICA LA COPIA ANTES DE INSTALARLA. Instalar primero y verificar después
#      es cambiar una pérdida por dos.
#   3. NO BORRA EL ALMACÉN ANTERIOR: LO APARTA CON SELLO. Restituir es la
#      operación que se hace con miedo, de apuro y con el laboratorio caído. Un
#      guion que además destruye el estado actual convierte un error de elección
#      de archivo en una SEGUNDA pérdida — y ésa es exactamente la lección del
#      2026-08-15, cuando una corrida de guiones se llevó la cuenta de
#      administrador del Product Owner y `store-path.sh` la dejó escrita.
#
# DOS FORMAS DE USO, y la segunda faltaba. Sin ella el ciclo respaldo -> restauración era
# IMPOSIBLE en la máquina del docente, que es el único lugar donde importa: el respaldo tenía
# modo contenedor y esto no, de modo que se podía tomar la copia y no se podía devolver. Lo
# levantó la mesa del 2026-09-01 —`grep -c 'desde-contenedor' restaurar-almacen.sh` daba **0**—.
#
#   scripts/restaurar-almacen.sh <archivo-de-copia>
#       Local. Resuelve el almacén por `store-path.sh`.
#
#   scripts/restaurar-almacen.sh --desde-contenedor <servicio> <archivo-de-copia>
#       La del destino real. **Detiene el servicio**, instala la copia en el volumen y lo vuelve a
#       levantar. Se corre desde el directorio donde vive la composición de despliegue.
#       Detener y levantar es parte de la operación y no un descuido: restituir bajo un escritor
#       vivo produce un almacén que el servicio pisa en el siguiente `checkpoint`, y la ventana de
#       indisponibilidad es la que `ADR-00007` §6 punto 3 ya acepta.
#
# CÓDIGOS DE SALIDA, con la convención de `coverage.sh`:
#   0  restituido, y el almacén anterior quedó apartado
#   1  la copia no sirve, o no se pudo instalar. EL ALMACÉN NO SE TOCÓ
#   2  no se puede: el servicio está arriba, falta `sqlite3`, o faltan argumentos
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

morir() { echo "NO SE RESTITUYÓ · $1" >&2; exit "${2:-1}"; }

modo_contenedor=0
servicio=""
if [ "${1:-}" = "--desde-contenedor" ]; then
  modo_contenedor=1; servicio="${2:-}"; copia="${3:-}"
  [ -n "$servicio" ] && [ -n "$copia" ] || {
    echo "uso: restaurar-almacen.sh --desde-contenedor <servicio> <archivo-de-copia>" >&2; exit 2; }
else
  copia="${1:-}"
fi
[ -n "$copia" ] || { echo "uso: restaurar-almacen.sh [--desde-contenedor <servicio>] <archivo-de-copia>" >&2; exit 2; }
[ -f "$copia" ] || morir "no existe el archivo '$copia'." 2
copia="$(cd "$(dirname "$copia")" && pwd)/$(basename "$copia")"

# ---------------------------------------------------------------------------
# Modo contenedor: la copia se verifica y se instala DENTRO, con el servicio detenido.
# ---------------------------------------------------------------------------
if [ "$modo_contenedor" = 1 ]; then
  command -v docker >/dev/null 2>&1 || morir "no hay 'docker' en esta máquina." 2
  img="$(docker compose images -q "$servicio" 2>/dev/null | head -1)"
  [ -n "$img" ] || morir "no se pudo resolver la imagen del servicio '$servicio'." 2

  interna="$(docker compose exec -T "$servicio" sh -lc \
      'printf %s "${ConnectionStrings__Store#*[Dd]ata [Ss]ource=}"' 2>/dev/null | sed 's/;.*//')"
  [ -n "$interna" ] || morir "el servicio '$servicio' no declara 'ConnectionStrings__Store'." 2

  # SE VERIFICA ANTES DE DETENER NADA. Detener el laboratorio para descubrir después que la copia
  # no servía es cambiar una pérdida por dos.
  ver="$(docker run --rm --entrypoint sh -v "$(dirname "$copia"):/c" "$img" \
        -lc "sqlite3 '/c/$(basename "$copia")' 'PRAGMA integrity_check;'" 2>&1 | head -1)"
  [ "$ver" = "ok" ] || morir "la copia no verifica ('$ver'). El servicio NO se detuvo y el almacén NO se tocó."

  echo "Copia verificada. Deteniendo '$servicio'…"
  docker compose stop "$servicio" >/dev/null || morir "no se pudo detener el servicio. Nada se tocó."

  sello="$(date -u +%Y%m%dT%H%M%SZ)"
  docker compose run --rm --entrypoint sh -v "$(dirname "$copia"):/c" "$servicio" -lc "
      set -e
      if [ -f '$interna' ]; then
        mv '$interna' '$interna.apartado-$sello'
        for s in -wal -shm; do [ -f '$interna'\$s ] && mv '$interna'\$s '$interna.apartado-$sello'\$s; done
      fi
      cp '/c/$(basename "$copia")' '$interna'" >/dev/null \
    || { docker compose start "$servicio" >/dev/null; morir "no se pudo instalar la copia. El servicio se volvió a levantar."; }

  docker compose start "$servicio" >/dev/null
  echo "Restituido dentro del contenedor."
  echo "  desde   : $copia"
  echo "  almacén : $interna (en '$servicio')"
  echo "  anterior: $interna.apartado-$sello — NO SE BORRÓ"
  echo
  echo "El servicio vuelve a fijar el diario en WAL al arrancar (\`StorePreparation\`)."
  exit 0
fi

command -v sqlite3 >/dev/null 2>&1 \
  || morir "no hay 'sqlite3' en esta máquina. Corré dentro del contenedor de desarrollo, o usá --desde-contenedor." 2

# shellcheck disable=SC1091
. "$raiz/scripts/store-path.sh"
gf_resolve_store
gf_ensure_store_directory

# --- PROPIEDAD 1 -----------------------------------------------------------
# El `-wal` con contenido significa que hay un escritor con transacciones sin
# consolidar. Un `-wal` de cero bytes queda después de un cierre limpio y no estorba.
if [ -s "$GF_STORE_FILE-wal" ]; then
  morir "el servicio parece estar arriba: '$GF_STORE_FILE-wal' tiene contenido. Detenelo y volvé a intentar." 2
fi

# --- PROPIEDAD 2 -----------------------------------------------------------
# Se verifica ANTES de tocar nada. Dos comprobaciones y no una: la integridad
# física del archivo, y que sea un almacén de ESTE producto y no cualquier SQLite.
veredicto="$(sqlite3 "$copia" 'PRAGMA integrity_check;' 2>&1 | head -1)"
[ "$veredicto" = "ok" ] \
  || morir "la copia no verifica ('${veredicto:-sin respuesta}'). El almacén NO se tocó."

tablas="$(sqlite3 "$copia" "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name IN ('Account','Work');" 2>/dev/null)"
[ "$tablas" = "2" ] \
  || morir "la copia verifica pero NO es un almacén de este producto: no tiene las tablas 'Account' y 'Work'. El almacén NO se tocó."

trabajos="$(sqlite3 "$copia" 'SELECT COUNT(*) FROM "Work";' 2>/dev/null || echo '?')"
cuentas="$(sqlite3 "$copia" 'SELECT COUNT(*) FROM "Account";' 2>/dev/null || echo '?')"

# --- PROPIEDAD 3 -----------------------------------------------------------
apartado=""
if [ -f "$GF_STORE_FILE" ]; then
  apartado="$GF_STORE_FILE.apartado-$(date -u +%Y%m%dT%H%M%SZ)"
  mv "$GF_STORE_FILE" "$apartado" || morir "no se pudo apartar el almacén actual. Nada se tocó."
  # Los acompañantes del diario se apartan con él: dejarlos sueltos junto a un
  # almacén nuevo es la forma de que SQLite lea mitad de uno y mitad del otro.
  for sufijo in -wal -shm; do
    [ -f "$GF_STORE_FILE$sufijo" ] && mv "$GF_STORE_FILE$sufijo" "$apartado$sufijo"
  done
fi

if ! cp "$copia" "$GF_STORE_FILE"; then
  [ -n "$apartado" ] && mv "$apartado" "$GF_STORE_FILE"
  morir "no se pudo instalar la copia. El almacén anterior se devolvió a su lugar."
fi

echo "Restituido."
echo "  desde   : $copia"
echo "  almacén : $GF_STORE_FILE"
echo "  contiene: $cuentas cuenta(s) · $trabajos trabajo(s)"
if [ -n "$apartado" ]; then
  echo "  anterior: $apartado"
  echo
  echo "EL ALMACÉN ANTERIOR NO SE BORRÓ. Comprobá que lo restituido es lo que esperabas"
  echo "ANTES de borrarlo, y borralo a mano cuando estés seguro."
fi
echo
echo "El servicio vuelve a fijar el diario en WAL al arrancar (\`StorePreparation\`)."
