#!/usr/bin/env bash
# ============================================================================
# coverage.sh — El informe de `TC-00037`: el instrumento de `QG-03` y `QG-04`.
#
# POR QUÉ EXISTE, y por qué no existía. `D1` volvió BLOQUEANTES a `QG-03` y
# `QG-04` el 2026-08-26, y hasta el 2026-08-27 NO HABÍA CON QUÉ MEDIRLOS: los
# tres proyectos de prueba no tenían recolector de cobertura anclado, y
# `test.sh` es un `dotnet test` pelado. Una puerta bloqueante sin instrumento
# no frena: pasa por omisión, que es peor que no tenerla.
#
#   QG-03 · cobertura de líneas y de ramas, POR PROYECTO DE CÓDIGO, contra los
#           umbrales del intake §22 que `D1` confirmó el 2026-08-26.
#   QG-04 · el reparto de la pirámide, 60 % integración / 40 % unitarias, que
#           `Estrategia-Calidad.md` §3.1 declara INVERTIDA a propósito.
#
# QUÉ NO HACE. No elige umbrales ni los discute: los transcribe de la fuente,
# una sola vez, en la tabla `UMBRAL` de abajo.
#
# UNA ADVERTENCIA SOBRE LAS RAMAS, sin la cual el número se lee mal. Coverlet
# cuenta también las ramas que genera el compilador —máquinas de estado de
# `async`, comprobaciones de nulo—, que ningún test escribe a propósito. El
# número de ramas es por lo tanto un PISO, y calibrar cuánto de la diferencia
# es código propio es trabajo de la primera corrida: exactamente lo que `D1`
# §0 declaró que nadie había hecho.
#
# CÓDIGOS DE SALIDA, con la convención de `verify-stage-g.sh` y `-i.sh`:
#   0  las dos puertas pasan
#   1  alguna puerta NO pasa
#   2  no se puede medir — la batería falló, o el recolector no dejó informe
#
# USO:  ./scripts/coverage.sh
#
# El análisis vive en `tools/informe-cobertura.cs`, que corre con `dotnet run` y
# NO agrega ninguna dependencia: el mismo kit que compila el producto.
# ============================================================================
set -uo pipefail
raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

rm -rf TestResults
if ! dotnet test GeometriaFactory.sln -c Release \
     --collect:"XPlat Code Coverage" --settings coverlet.runsettings --logger "trx" --results-directory ./TestResults; then
  echo
  echo "NO SE PUEDE MEDIR · la batería no pasó."
  echo "QG-02 frena antes que QG-03: no se mide cobertura sobre rojo."
  exit 2
fi

dotnet run tools/informe-cobertura.cs -- TestResults
salida=$?

echo
case "$salida" in
  0) echo "Las dos puertas pasan." ;;
  1) echo "Alguna puerta no pasa. El detalle está arriba, gate por gate." ;;
  2) echo "No se pudo medir. Nada de lo de arriba es un veredicto." ;;
esac
exit "$salida"
