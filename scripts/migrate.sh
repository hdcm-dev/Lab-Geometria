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

# LA CONFIGURACIÓN SE DECLARA, NO SE HEREDA (`Pipeline-Producto.md` §3.1). `dotnet ef` CONSTRUYE
# el proyecto de arranque para poder cargar el contexto, y sin decirlo construía `Debug` mientras
# el resto del ciclo de guiones trabaja sobre `Release`: dos salidas del mismo árbol, y la
# transformación generada contra la que nadie más mira.
configuration="${GF_CONFIGURATION:-Release}"

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
