#!/usr/bin/env bash
# ============================================================================
# assert-build-fresh.sh — LA RED QUE VA DEBAJO DE TODO `dotnet run --no-build`.
#
# Uso:  scripts/assert-build-fresh.sh <ruta-al-csproj> <configuracion> [args...]
#
# Construye el proyecto en la configuración DECLARADA y **no vuelve con 0 salvo
# que la salida de esa configuración exista y esté al día**. Lo que venga después
# de los dos primeros argumentos se le pasa tal cual a la construcción
# (`-warnaserror`, por ejemplo).
#
# QUÉ PROBLEMA RESUELVE, Y POR QUÉ NO ALCANZA CON DECLARAR LA CONFIGURACIÓN.
# Declarar `-c Release` en los dos lados arregla que se construya una cosa y se
# levante otra. NO arregla el otro caso, que es el que ya costó una conclusión
# equivocada sobre este producto: que la construcción FALLE y el guion siga
# adelante igual. Pasa solo, sin que nadie se distraiga: un guion con
# `set -uo pipefail` —sin `-e`— que además canaliza la construcción a `tail`
# pierde el código de salida dos veces, y el `dotnet run --no-build` de la línea
# siguiente levanta, obedientemente, el binario de la corrida anterior. El
# proceso arranca, el punto de salud responde, y lo que se mide es código viejo.
#
# POR QUÉ LA RED CONSTRUYE EN VEZ DE COMPARAR FECHAS.
# La primera versión de este guion comparaba la marca de tiempo del ensamblado
# contra la de cada fuente del cierre de dependencias. Se descartó porque DA
# FALSOS POSITIVOS QUE NO SE PUEDEN LIMPIAR, y se comprobó en este árbol: con
# `Deterministic` puesto en `Directory.Build.props`, tocar una fuente sin cambiar
# su contenido deja el ensamblado exactamente igual —la compilación produce el
# mismo resultado y la copia se saltea—, de modo que la fuente queda más nueva
# que la salida PARA SIEMPRE y reconstruir no lo arregla. Un `git checkout`
# vuelve a poner la fecha de todas las fuentes en «ahora» y reproduce lo mismo.
# Una red que se traba en rojo sin forma de destrabarla se termina salteando, y
# una red que se saltea no es una red.
#
# La construcción incremental de MSBuild YA SABE si la salida está al día, y lo
# sabe bien: mira contenidos y no sólo fechas. Este guion la usa como oráculo en
# lugar de reimplementarla peor. El costo cuando ya está todo construido son
# unos segundos; el beneficio es que **no hay forma de llegar al `--no-build` con
# una salida que no exista o que no corresponda a las fuentes**.
#
# POR QUÉ SE CONSERVA `--no-build` EN QUIEN LEVANTA. Porque separa las dos
# responsabilidades y deja la falla donde se entiende: acá falla la
# construcción, con su mensaje y su código de salida mirado; allá se levanta, y
# lo que se levanta es exactamente lo que acá se construyó. Un `dotnet run` que
# construye por su cuenta esconde la falla de construcción entre la salida del
# arranque y evalúa la puerta de advertencias en un momento distinto del que se
# declara.
#
# NO ES OPCIONAL Y NO SE PIDE CON UNA BANDERA: cada guion que levanta con
# `--no-build` lo llama antes, siempre, y `scripts/verify-explicit-configuration.sh`
# falla si aparece un `--no-build` que no lo haga.
# ============================================================================
set -uo pipefail

if [ $# -lt 2 ]; then
  echo "Uso: scripts/assert-build-fresh.sh <ruta-al-csproj> <configuracion> [args...]" >&2
  exit 2
fi

project="$1"
configuration="$2"
shift 2

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

# La configuración es un valor cerrado y se valida: un `-c relase` mal escrito produce una
# tercera salida vacía en vez de una falla, que es la peor de las dos cosas.
case "$configuration" in
  Debug | Release) ;;
  *)
    echo "assert-build-fresh: configuración no reconocida: '$configuration' (se espera Debug o Release)" >&2
    exit 2
    ;;
esac

if [ ! -f "$project" ]; then
  echo "assert-build-fresh: no existe el archivo de proyecto: $project" >&2
  exit 2
fi

log="$(mktemp)"
trap 'rm -f "$log"' EXIT

dotnet build "$project" -c "$configuration" "$@" > "$log" 2>&1
build_status=$?
tail -4 "$log"

if [ "$build_status" -ne 0 ]; then
  {
    echo "============================================================================"
    echo "CONSTRUCCIÓN FALLIDA — se aborta antes de levantar nada."
    echo
    echo "  proyecto:      $project"
    echo "  configuración: $configuration"
    echo "  código:        $build_status"
    echo
    # El texto dice \`--no-build\` y NO nombra el verbo que lo acompaña, a propósito: con el verbo
    # escrito, esta línea —que sólo imprime— la levantaba `verify-explicit-configuration.sh` como
    # una invocación sin configuración declarada. Es la misma técnica que esa puerta ya usa
    # consigo misma para no denunciarse por su propia prosa.
    echo "Sin esto, el arranque con \`--no-build\` de más abajo levantaría la salida de"
    echo "la corrida ANTERIOR: el proceso arranca, responde, y lo que se mide es"
    echo "código que ya no está en el árbol. Ese fue el defecto que esta red existe"
    echo "para no repetir."
    echo
    echo "Salida de la construcción:"
    echo "----------------------------------------------------------------------------"
    tail -40 "$log"
    echo "============================================================================"
  } >&2
  exit 1
fi

# Cinturón sobre los tirantes: que la construcción diga 0 y que el ensamblado de ESA
# configuración exista son dos cosas distintas, y la que le importa a `--no-build` es la segunda.
read_property() { # archivo etiqueta
  sed -n "s#.*<$2>\([^<]*\)</$2>.*#\1#p" "$1" | head -1
}

target_framework="$(read_property "$project" TargetFramework)"
[ -n "$target_framework" ] || target_framework="$(read_property "$repo_root/Directory.Build.props" TargetFramework)"

assembly_name="$(read_property "$project" AssemblyName)"
[ -n "$assembly_name" ] || assembly_name="$(basename "$project" .csproj)"

assembly="$(dirname "$project")/bin/$configuration/$target_framework/$assembly_name.dll"

if [ ! -f "$assembly" ]; then
  {
    echo "============================================================================"
    echo "SALIDA AUSENTE — se aborta antes de levantar nada."
    echo
    echo "  proyecto:      $project"
    echo "  configuración: $configuration"
    echo "  se buscaba:    $assembly"
    echo
    echo "La construcción terminó en 0 y aun así no hay ensamblado de \`$configuration\`."
    echo "Un \`--no-build\` acá levantaría la salida de otra configuración, o nada."
    echo "============================================================================"
  } >&2
  exit 1
fi

echo "assert-build-fresh · $assembly_name ($configuration) construido y al día: $assembly"
