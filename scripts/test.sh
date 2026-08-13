#!/usr/bin/env bash
# Batería completa. Es EL MISMO GUION en la máquina de quien construye y en la canalización.
# Criterio de éxito declarado: la batería pasa entera, 0 rojas y 0 deshabilitadas sin motivo
# escrito (`QG-02` en Domain, Application, Infrastructure y Api; Contracts corre el mismo guion).
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

dotnet test GeometriaFactory.sln --configuration Release
