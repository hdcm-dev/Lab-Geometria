#!/usr/bin/env bash
# ============================================================================
# verify-solution-tree.sh — Puerta del árbol de solución (DC-5).
#
# LA REGLA, entera, en una línea: **todo proyecto del repositorio y toda carpeta
# de sample tienen nodo en `GeometriaFactory.sln`**, aunque sea un nodo que no
# construye nada. Es la decisión del Product Owner del 2026-09-11: «todos los
# proyectos quedan bajo el árbol de solución de Visual Studio, aunque sea bajo
# carpetas virtuales».
#
# POR QUÉ ES UNA PUERTA Y NO UNA RECOMENDACIÓN. La reestructuración del
# 2026-09-12 (`6cc6f86`) declaró en su mensaje que «todo entra al árbol de
# solución» y dejó afuera las diez carpetas de samples que no eran `.csproj`
# —`visor/0N`, `api/0N`, `contracts/0N`, `web/01-datos-seed`—, que la
# especificación §6.1 incluía por nombre. Al día siguiente entró
# `api/04-cliente-http-basico` y quedó afuera también, porque nada lo miraba.
# Once carpetas fuera del árbol, y ninguna construcción ni prueba que lo
# notara: una solución a la que le falta un nodo compila igual.
#
# SEIS CONTROLES, los seis de pasa/falla:
#
#   A-1  Todo `.csproj` bajo `src/`, `tests/` y `visor/` está en la solución.
#   A-2  Toda carpeta `samples/<segmento>/<NN-…>` tiene UN archivo de proyecto
#        y ese archivo está en la solución.
#   A-3  El nodo de cada sample cuelga de la carpeta de solución `<segmento>`,
#        y ésa de `samples`, como en el disco.
#   A-4  Todo proyecto que la solución nombra existe en el disco.
#   A-5  Los nodos `Microsoft.Build.NoTargets` son inertes: sin `Exec`, sin
#        `Target` y sin `ProjectReference` (P-7, opción D). Un `verify` de
#        Playwright enganchado a `Build` rompe la solución entera sin navegador.
#   A-6  La solución declara sólo `Debug|Any CPU` y `Release|Any CPU`:
#        `dotnet sln add` en el SDK 10 inyecta `x64`/`x86`, y ya pasó.
#
# CÓDIGOS DE SALIDA, con la convención de `verify-stage-g.sh` y `coverage.sh`:
#   0  el árbol está completo
#   1  algún control NO pasa
#   2  no se puede medir — no hay solución que leer
#
# Se corre desde la raíz del repositorio y no necesita .NET:
#   bash scripts/verify-solution-tree.sh
# La solución a leer se puede cambiar con `SOLUCION=<ruta>`: es lo que permite
# probar la puerta fallando sobre una copia, sin tocar la del repositorio.
# ============================================================================
set -uo pipefail
cd "$(dirname "$0")/.."

sln="${SOLUCION:-GeometriaFactory.sln}"
if [ ! -f "$sln" ]; then
  echo "NO SE PUEDE MEDIR · no existe la solución $sln"
  exit 2
fi

fails=0
banner() { printf '\n== %s ==\n' "$1"; }
falla() { printf '  FALLA  %s\n' "$1"; fails=$((fails + 1)); }

# La solución, leída una vez. Las rutas pasan a `/` y el retorno de carro se
# descarta: el archivo lo escribe Visual Studio o `dotnet sln` según quién toque.
limpio="$(sed 's/\r$//; s/\\/\//g' "$sln")"

readonly CARPETA='2150E333-8FDC-42A3-9474-1A3956D46DE8'

# tipo|nombre|ruta|guid, una línea por `Project(...)`.
proyectos="$(printf '%s\n' "$limpio" | sed -n 's/^Project("{\([^}]*\)}") = "\([^"]*\)", "\([^"]*\)", "{\([^}]*\)}"$/\1|\2|\3|\4/p')"
# hijo|padre, una línea por entrada de `NestedProjects`.
anidados="$(printf '%s\n' "$limpio" | awk '/GlobalSection\(NestedProjects\)/{f=1;next} /EndGlobalSection/{f=0} f' \
  | sed -n 's/^[[:space:]]*{\([^}]*\)} = {\([^}]*\)}$/\1|\2/p')"

guid_de_ruta() { printf '%s\n' "$proyectos" | awk -F'|' -v r="$1" '$1 != "'"$CARPETA"'" && $3 == r {print $4; exit}'; }
nombre_de_guid() { printf '%s\n' "$proyectos" | awk -F'|' -v g="$1" '$4 == g {print $2; exit}'; }
padre_de() { printf '%s\n' "$anidados" | awk -F'|' -v g="$1" '$1 == g {print $2; exit}'; }

banner "A-1 · proyectos de src/, tests/ y visor/"
while IFS= read -r csproj; do
  [ -n "$(guid_de_ruta "$csproj")" ] || falla "$csproj no tiene nodo en $sln"
done < <(find src tests visor \( -name bin -o -name obj -o -name node_modules \) -prune -o -name '*.csproj' -print | sort)

banner "A-2 y A-3 · carpetas de samples/<segmento>/<NN-…>"
muestras=0
while IFS= read -r carpeta; do
  muestras=$((muestras + 1))
  segmento="$(basename "$(dirname "$carpeta")")"
  mapfile -t archivos < <(find "$carpeta" -maxdepth 1 -name '*.csproj' | sort)
  if [ "${#archivos[@]}" -ne 1 ]; then
    falla "$carpeta tiene ${#archivos[@]} archivos de proyecto; tiene que tener uno (un nodo inerte si el sample no es .NET)"
    continue
  fi
  guid="$(guid_de_ruta "${archivos[0]}")"
  if [ -z "$guid" ]; then
    falla "${archivos[0]} no tiene nodo en $sln"
    continue
  fi
  carpeta_sln="$(padre_de "$guid")"
  if [ -z "$carpeta_sln" ] || [ "$(nombre_de_guid "$carpeta_sln")" != "$segmento" ] \
     || [ "$(nombre_de_guid "$(padre_de "$carpeta_sln")")" != "samples" ]; then
    falla "${archivos[0]} está en la solución pero no cuelga de samples/$segmento"
  fi
done < <(find samples -mindepth 2 -maxdepth 2 -type d -name '[0-9][0-9]-*' | sort)
printf '  %d carpetas de sample revisadas\n' "$muestras"

banner "A-4 · todo nodo nombra un archivo que existe"
while IFS='|' read -r tipo nombre ruta guid; do
  [ "$tipo" = "$CARPETA" ] && continue
  [ -f "$ruta" ] || falla "$sln nombra $ruta ($nombre), que no existe"
done <<< "$proyectos"

banner "A-5 · los nodos NoTargets son inertes"
while IFS='|' read -r tipo nombre ruta guid; do
  [ "$tipo" = "$CARPETA" ] && continue
  [ -f "$ruta" ] || continue
  grep -q 'Sdk="Microsoft.Build.NoTargets/' "$ruta" || continue
  # Los comentarios nombran `Exec` y `ProjectReference` para decir que no están: se leen sin ellos.
  sin_comentarios="$(perl -0pe 's/<!--.*?-->//gs' "$ruta")"
  if printf '%s' "$sin_comentarios" | grep -Eq '<(Exec|Target|ProjectReference)[[:space:]>/]'; then
    falla "$ruta es NoTargets y declara Exec, Target o ProjectReference"
  fi
done <<< "$proyectos"

banner "A-6 · configuraciones de la solución"
plataformas="$(printf '%s\n' "$limpio" | awk '/GlobalSection\(SolutionConfigurationPlatforms\)/{f=1;next} /EndGlobalSection/{f=0} f' \
  | sed 's/^[[:space:]]*//' | sort | tr '\n' ';')"
esperadas='Debug|Any CPU = Debug|Any CPU;Release|Any CPU = Release|Any CPU;'
[ "$plataformas" = "$esperadas" ] || falla "configuraciones de solución inesperadas: $plataformas"
if printf '%s\n' "$limpio" | grep -Eq '\|(x64|x86)'; then
  falla "la solución arrastra plataformas x64/x86 (las inyecta dotnet sln add)"
fi

echo
if [ "$fails" -eq 0 ]; then
  echo "El árbol de solución está completo."
  exit 0
fi
echo "$fails control(es) no pasan. Cada línea FALLA de arriba nombra qué falta."
exit 1
