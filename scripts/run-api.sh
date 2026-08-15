#!/usr/bin/env bash
# Ejecuta el servicio de datos dentro del contenedor de desarrollo.
# Criterio de éxito declarado: arranca, aplica las transformaciones y el punto de salud responde
# (`Api/03 Guia-Onboarding-Developer.md` §3.2 y `DX-Developer-Experience.md`).
# En desarrollo escucha por HTTP SIN CERTIFICADO, para evitar la fricción del certificado de
# confianza dentro del contenedor (intake §17.5).
#
# LA CONFIGURACIÓN SE DECLARA, NO SE HEREDA (`Pipeline-Producto.md` §3.1). Hasta acá este guion
# corría `dotnet run` a secas, que resuelve `Debug`, mientras `scripts/build.sh` y
# `scripts/test.sh` construyen y prueban `Release`: quien construía y después levantaba estaba
# ejecutando binarios que la construcción nunca produjo. El ciclo de los guiones es `Release` de
# punta a punta; el ciclo del depurador es `Debug` de punta a punta y vive en `.vscode/`. Ninguno
# de los dos depende del valor por omisión.
#
#   GF_CONFIGURATION=Debug scripts/run-api.sh   # si hace falta la otra salida, se pide
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root/src/GeometriaFactory.Api"

configuration="${GF_CONFIGURATION:-Release}"

export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

# LA CLAVE DE FIRMA SE RECIBE Y NO SE BUSCA (`Infrastructure ADR-04` §2 punto 3; intake
# §17.3.P.5). Llega por variable de entorno y NO está en el repositorio ni en la imagen:
#
#   export AccessToken__SigningKey='...al menos 32 bytes...'
#
# SIN CLAVE EL SERVICIO NO ARRANCA, y este aviso decía lo contrario. La etapa `d` agregó una
# guardia de arranque en `CompositionRoot`: una pieza a la que le falta algo sin lo cual no puede
# cumplir su función se niega a arrancar, para que el defecto aparezca donde lo ve quien
# despliega y no con una persona adelante intentando entrar. Este guion venía diciendo «el
# servicio arranca igual y no emite accesos», que era verdad ANTES de esa guardia; hoy lo que
# pasa es una excepción de arranque con su traza. Se corrige acá porque un aviso que describe
# mal el comportamiento es exactamente la clase de dato con el que se sacan conclusiones
# equivocadas. Generar una clave al vuelo dejaría el sistema aparentemente funcionando hasta que
# alguien falsifique un acceso, y por eso no se hace.
if [ -z "${AccessToken__SigningKey:-}" ]; then
  cat >&2 <<'FIN'
No hay clave de firma y el servicio de datos NO va a arrancar: la guardia de arranque de
`CompositionRoot` lo detiene antes de atender nada. No es una falla de este guion.

La clave se recibe, no se busca. Poné una de al menos 32 bytes y volvé a correr:

  export AccessToken__SigningKey='...al menos 32 bytes...'
  scripts/run-api.sh
FIN
  exit 1
fi

echo "Configuración: $configuration"
dotnet run --project GeometriaFactory.Api.csproj --configuration "$configuration" "$@"
