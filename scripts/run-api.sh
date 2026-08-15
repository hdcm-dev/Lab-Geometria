#!/usr/bin/env bash
# Ejecuta el servicio de datos dentro del contenedor de desarrollo.
# Criterio de éxito declarado: arranca, aplica las transformaciones y el punto de salud responde
# (`Api/03 Guia-Onboarding-Developer.md` §3.2 y `DX-Developer-Experience.md`).
# En desarrollo escucha por HTTP SIN CERTIFICADO, para evitar la fricción del certificado de
# confianza dentro del contenedor (intake §17.5).
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root/src/GeometriaFactory.Api"

export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

# LA CLAVE DE FIRMA SE RECIBE Y NO SE BUSCA (`Infrastructure ADR-04` §2 punto 3; intake
# §17.3.P.5). Llega por variable de entorno y NO está en el repositorio ni en la imagen:
#
#   export AccessToken__SigningKey='...al menos 32 bytes...'
#
# Si no llega, el servicio ARRANCA igual y no emite ningún acceso: el canje responde error y la
# falla se ve en el primer intento. Generar una clave al vuelo dejaría el sistema aparentemente
# funcionando hasta que alguien falsifique un acceso, y por eso no se hace.
if [ -z "${AccessToken__SigningKey:-}" ]; then
  echo "AVISO: no hay clave de firma (AccessToken__SigningKey). El servicio arranca y NO emite accesos." >&2
fi

dotnet run --project GeometriaFactory.Api.csproj "$@"
