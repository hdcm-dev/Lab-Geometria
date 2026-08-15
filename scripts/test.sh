#!/usr/bin/env bash
# Batería completa. Es EL MISMO GUION en la máquina de quien construye y en la canalización.
# Criterio de éxito declarado: la batería pasa entera, 0 rojas y 0 deshabilitadas sin motivo
# escrito (`QG-02` en Domain, Application, Infrastructure y Api; Contracts corre el mismo guion).
#
# LA CONFIGURACIÓN VA DECLARADA, y acá la razón es directa: se prueba `Release` porque `Release`
# es lo que `scripts/build.sh` produce y lo que las puertas verifican (`Pipeline-Producto.md`
# §3.1). Probar sin decirlo mediría la salida de `Debug` —otra salida, de otra antigüedad— y la
# batería quedaría verde sobre algo que no es lo que se despliega.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

dotnet test GeometriaFactory.sln --configuration Release
