#!/usr/bin/env bash
# GENERA una transformación de esquema nueva durante el desarrollo. NO la aplica.
#
# [PROPUESTA SIN BASE DECLARADA] — `Plan-Etapa-A.md` §4 lo declara sin taparlo: es el único de
# los siete guiones que ninguna fuente del corpus menciona fuera del árbol del intake §16. Y hay
# un motivo por el que su propósito no es obvio: las transformaciones SE APLICAN SOLAS AL
# ARRANCAR (intake §17.3.P.4 y §17.5.P.4; `Infrastructure ADR-07`), de modo que no hay un paso de
# aplicación manual que envolver. Lo que sí hace falta es GENERAR la transformación para que
# `reset-db.sh` y `run-api.sh` tengan un esquema que aplicar. El punto de control decide.
#
# Uso: scripts/migrate.sh <NombreDeLaTransformacion>
set -euo pipefail

if [ $# -lt 1 ]; then
  echo "Uso: scripts/migrate.sh <NombreDeLaTransformacion>" >&2
  exit 2
fi

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

# `dotnet ef` EJECUTA EL PROYECTO DE ARRANQUE para cargar el contexto, y eso atraviesa
# `CompositionRoot`, que desde este cambio SE NIEGA A ARRANCAR sin la cadena de conexión del
# almacén. No se aplica ninguna transformación —`Program.cs` saltea la fase 1 con `EF.IsDesignTime`
# y este guion sólo GENERA—, pero la cadena tiene que estar igual. Sale del mismo lugar que la de
# `run-api.sh`, no de una ruta escrita acá.
# shellcheck source=scripts/store-path.sh
. "$repo_root/scripts/store-path.sh"
gf_resolve_store

# LA CONFIGURACIÓN SE DECLARA, NO SE HEREDA (`Pipeline-Producto.md` §3.1). `dotnet ef` CONSTRUYE
# el proyecto de arranque para poder cargar el contexto, y sin decirlo lo construía por omisión:
# dos salidas del mismo árbol, y la transformación generada contra la que nadie más mira.
#
# `Debug` por omisión, por decisión del Product Owner —en desarrollo se trabaja en `Debug`—, y
# DECLARADO en la invocación, que es lo que la regla exige. Los guiones de verificación
# —`verify-stage-c.sh`, `verify-navigation.sh`— se quedan en `Release` a propósito, porque miden
# lo que se despliega; la asimetría es la decisión y no un olvido.
configuration="${GF_CONFIGURATION:-Debug}"

# `dotnet-ef` es herramienta LOCAL del repositorio, para que su versión quede versionada junto
# al código (intake §17.3).
dotnet tool restore

echo "Configuración: $configuration"
dotnet ef migrations add "$1" \
  --project src/GeometriaFactory.Infrastructure/GeometriaFactory.Infrastructure.csproj \
  --startup-project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj \
  --configuration "$configuration" \
  --output-dir Persistence/Migrations

echo "Transformación generada. Una transformación ya fusionada NO se edita."
