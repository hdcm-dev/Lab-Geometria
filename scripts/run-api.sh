#!/usr/bin/env bash
# Ejecuta el servicio de datos dentro del contenedor de desarrollo.
# Criterio de éxito declarado: arranca, aplica las transformaciones y el punto de salud responde
# (`Api/03 Guia-Onboarding-Developer.md` §3.2 y `DX-Developer-Experience.md`).
# En desarrollo escucha por HTTP SIN CERTIFICADO, para evitar la fricción del certificado de
# confianza dentro del contenedor (intake §17.5).
#
# LA CONFIGURACIÓN SE DECLARA, NO SE HEREDA (`Pipeline-Producto.md` §3.1). Hasta acá este guion
# corría `dotnet run` a secas, que resuelve `Debug` sin decirlo: se levantaban binarios que la
# construcción declarada nunca produjo. Eso no se afloja y este guion sigue declarando su
# configuración en la línea de invocación.
#
# LO QUE SÍ CAMBIÓ ES EL VALOR: `Debug` por omisión, por decisión del Product Owner —en
# desarrollo se trabaja en `Debug`—. NO ES UNA VUELTA ATRÁS de la regla, y la diferencia está en
# una palabra: `Debug` acá está DECLARADO, no heredado del valor por omisión de `dotnet`. Y no
# reintroduce el desajuste original, porque el desajuste vivía en `--no-build`: este guion
# CONSTRUYE Y LEVANTA la misma configuración en la misma invocación, de modo que lo que se
# levanta es siempre lo que se acaba de construir.
#
# LOS GUIONES DE VERIFICACIÓN NO ACOMPAÑAN ESTE CAMBIO, Y LA ASIMETRÍA ES A PROPÓSITO.
# `verify-stage-c.sh` y `verify-navigation.sh` siguen en `Release` porque su trabajo es medir lo
# que efectivamente se despliega. Una puerta que verifica una salida distinta de la que sale a
# producción es exactamente el defecto que se acaba de erradicar. QUE NADIE LA «CORRIJA» POR
# SIMETRÍA: no es una inconsistencia olvidada, es la decisión.
#
#   GF_CONFIGURATION=Release scripts/run-api.sh   # si hace falta la otra salida, se pide
#
# ---------------------------------------------------------------------------
# EL CONTENEDOR DE DESARROLLO `gf-back` HAY QUE RELANZARLO UNA VEZ.
#
# Corre con `HOME=/tmp` y con el repositorio montado en `/w`, y hasta acá el almacén le salía del
# valor por omisión relativo de `appsettings.json`: se creaba adentro del árbol montado. Ese valor
# por omisión ya no existe, y sin la variable el servicio se detiene en el arranque diciendo qué
# llave le falta. El directorio del anfitrión donde ahora vive el almacén tiene que estar montado,
# porque `HOME=/tmp` adentro del contenedor es efímero:
#
#   docker rm -f gf-back
#   docker run -d --name gf-back --network gfnet -w /w \
#     -v /home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria:/w \
#     -v "$HOME/.local/share/geometria-factory":/datos \
#     -e HOME=/tmp \
#     -e "AccessToken__SigningKey=$(cat ~/.config/geometria-factory/access-token.key)" \
#     -e 'ConnectionStrings__Store=Data Source=/datos/geometriafactory.db' \
#     mcr.microsoft.com/dotnet/sdk:10.0 \
#     bash -lc 'cd src/GeometriaFactory.Api && ASPNETCORE_URLS=http://0.0.0.0:5080 dotnet run -c Debug --no-build'
#
# El punto de montaje se llama `/datos` a propósito: es el mismo nombre que `deploy/Dockerfile`
# usa para su volumen, de modo que la ruta de adentro del contenedor es la misma en desarrollo y
# en el despliegue, y sólo cambia qué hay del lado del anfitrión.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

# EL ALMACÉN VIVE FUERA DEL ÁRBOL DEL REPOSITORIO, y la ruta llega por configuración. El porqué,
# la ubicación elegida y las alternativas descartadas están en `scripts/store-path.sh`.
# shellcheck source=scripts/store-path.sh
. "$repo_root/scripts/store-path.sh"
gf_resolve_store
gf_ensure_store_directory

cd "$repo_root/src/GeometriaFactory.Api"

configuration="${GF_CONFIGURATION:-Debug}"

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
echo "Almacén: $GF_STORE_FILE"
dotnet run --project GeometriaFactory.Api.csproj --configuration "$configuration" "$@"
