#!/usr/bin/env bash
# ============================================================================
# lib-puerta.sh — La forma común de las puertas de etapa `d`, `e` y `f`.
#
# POR QUÉ EXISTE, Y POR QUÉ NO LA USAN `c`, `g` NI `h`. Las tres puertas nuevas
# hacen exactamente lo mismo: correr las pruebas que cubren cada criterio y
# decir cuál cubre qué. Escribir ese bucle tres veces habría creado tres lugares
# donde el formato de salida y el manejo de fallas pueden divergir.
#
# Las otras tres NO la usan y no se las migra: `verify-stage-c.sh` levanta los
# dos servicios y los reinicia, `verify-stage-g.sh` corre un navegador en
# contenedor y `verify-stage-h.sh` declara un criterio no mecánico. Forzarlas a
# esta forma habría hecho la forma más grande que el problema, que es cómo una
# biblioteca compartida se vuelve el lugar donde nadie quiere tocar nada.
#
# CADA CRITERIO NOMBRA SUS PRUEBAS UNA POR UNA. Es lo que vuelve auditable la
# puerta: la lista es el mapa entre el roadmap y la batería, y un filtro por
# clase habría pasado igual sin decir qué criterio cubre qué.
# ============================================================================

_puerta_fallas=0
_puerta_criterios=0
_puerta_pruebas=0

puerta_abre() {
  printf '== Puerta de la etapa `%s` → `%s` · %s ==\n\n' "$1" "${2#* → }" "$3"
}

# criterio <rótulo> <prueba> [prueba...]
criterio() {
  local rotulo="$1"; shift
  local filtro=""

  for prueba in "$@"; do
    filtro="${filtro}${filtro:+|}FullyQualifiedName~${prueba}"
  done

  _puerta_criterios=$((_puerta_criterios + 1))
  local registro="/tmp/puerta-$$-${_puerta_criterios}.log"

  if dotnet test tests/GeometriaFactory.Integration.Tests --configuration Release \
       --filter "$filtro" >"$registro" 2>&1; then
    local pasadas
    pasadas="$(grep -oP 'Passed:\s+\K\d+' "$registro" | tail -1)"
    _puerta_pruebas=$((_puerta_pruebas + ${pasadas:-0}))

    # EL RECUENTO SE COMPARA CONTRA LO PEDIDO. Una prueba que se renombra deja de
    # existir para el filtro y la corrida pasaría en verde SIN HABERLA CORRIDO:
    # es el modo de falla que un filtro por nombre tiene y que hay que cerrar.
    if [ "${pasadas:-0}" -lt "$#" ]; then
      printf '  \033[31mFALLA\033[0m %s\n' "$rotulo"
      printf '        se pidieron %s pruebas y corrieron %s: alguna no existe con ese nombre\n' "$#" "${pasadas:-0}"
      _puerta_fallas=$((_puerta_fallas + 1))
      return
    fi

    printf '  \033[32mOK\033[0m   %s \033[2m(%s pruebas)\033[0m\n' "$rotulo" "${pasadas:-0}"
  else
    printf '  \033[31mFALLA\033[0m %s\n' "$rotulo"
    printf '        ver %s\n' "$registro"
    grep -E '^\s+(Failed|Error Message)' "$registro" | head -6 | sed 's/^/        /'
    _puerta_fallas=$((_puerta_fallas + 1))
  fi
}

puerta_cierra() {
  printf '\n'

  if [ "$_puerta_fallas" -eq 0 ]; then
    printf 'CONFORME · %s de transición de la etapa `%s` se verifican\n' "$2" "$1"
    printf '           %s criterios, %s pruebas\n' "$_puerta_criterios" "$_puerta_pruebas"
    exit 0
  fi

  printf 'NO CONFORME · %s criterio(s) fallan\n' "$_puerta_fallas"
  exit 1
}
