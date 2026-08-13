#!/usr/bin/env bash
# Construcción del producto: encadena la generación del bundle con la compilación de la solución.
# Criterio de éxito declarado: termina en 0 y SIN ADVERTENCIAS (`QG-01` en Domain, Contracts,
# Application, Infrastructure y Api). La puerta la impone `Directory.Build.props` con
# TreatWarningsAsErrors, de modo que una advertencia detiene la construcción y no se arrastra
# (`Api/03 Guia-Onboarding-Developer.md` §2).
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

"$repo_root/scripts/build-visor.sh"

cd "$repo_root"
dotnet restore GeometriaFactory.sln
dotnet build GeometriaFactory.sln --configuration Release --no-restore

echo "Construcción terminada."
