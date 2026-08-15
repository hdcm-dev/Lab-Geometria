#!/usr/bin/env bash
# Ejecuta la pieza pública dentro del contenedor de desarrollo.
#
# [PROPUESTA SIN BASE DECLARADA] — `Plan-Etapa-A.md` §4 lo registra: el intake §16 enumera este
# guion y NO declara su contenido, y ningún otro documento del corpus lo menciona. Lo que hace
# acá es lo que el plan propuso: que el front arranque y su página de estado responda.
#
# La dirección del servicio de datos llega por configuración (`ApiBaseUrl`) y nunca embebida.
#
# LA CONFIGURACIÓN SE DECLARA, NO SE HEREDA (`Pipeline-Producto.md` §3.1). Vale lo mismo que en
# `run-api.sh`: `dotnet run` a secas resolvía `Debug` mientras `build.sh` y `test.sh` trabajan
# sobre `Release`, y quien encadenaba los dos levantaba binarios que la construcción nunca
# produjo. El ciclo de los guiones es `Release` de punta a punta.
#
#   GF_CONFIGURATION=Debug scripts/run-web.sh
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root/src/GeometriaFactory.Web"

export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

configuration="${GF_CONFIGURATION:-Release}"

echo "Configuración: $configuration"
dotnet run --project GeometriaFactory.Web.csproj --configuration "$configuration" "$@"
