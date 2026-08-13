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

# `dotnet-ef` es herramienta LOCAL del repositorio, para que su versión quede versionada junto
# al código (intake §17.3).
dotnet tool restore

dotnet ef migrations add "$1" \
  --project src/GeometriaFactory.Infrastructure/GeometriaFactory.Infrastructure.csproj \
  --startup-project src/GeometriaFactory.Api/GeometriaFactory.Api.csproj \
  --output-dir Persistence/Migrations

echo "Transformación generada. Una transformación ya fusionada NO se edita."
