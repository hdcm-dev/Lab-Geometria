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
# `run-api.sh`: `dotnet run` a secas resolvía `Debug` SIN DECIRLO, y eso no se afloja: la
# configuración sigue yendo declarada en la invocación.
#
# LO QUE CAMBIÓ ES EL VALOR: `Debug` por omisión, por decisión del Product Owner —en desarrollo se
# trabaja en `Debug`—. Declarado, no heredado. Y sin desajuste posible, porque este guion
# construye y levanta la misma configuración en la misma invocación: no usa `--no-build`.
#
# LOS GUIONES DE VERIFICACIÓN SE QUEDAN EN `Release`, Y LA ASIMETRÍA ES A PROPÓSITO.
# `verify-navigation.sh` levanta esta misma pieza y lo hace en `Release`, porque mide lo que
# efectivamente se despliega; si midiera una salida distinta de la que sale a producción,
# volveríamos al defecto que se acaba de erradicar. QUE NADIE LA «CORRIJA» POR SIMETRÍA con este
# guion: no es una inconsistencia olvidada, es la decisión.
#
#   GF_CONFIGURATION=Release scripts/run-web.sh
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root/src/GeometriaFactory.Web"

export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

configuration="${GF_CONFIGURATION:-Debug}"

echo "Configuración: $configuration"
dotnet run --project GeometriaFactory.Web.csproj --configuration "$configuration" "$@"
