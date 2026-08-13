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

dotnet run --project GeometriaFactory.Api.csproj "$@"
