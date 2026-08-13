#!/usr/bin/env bash
# Deja el almacén EN SU ESTADO DE PRIMER ARRANQUE: vacío, sin ninguna cuenta y sin ningún
# trabajo, con su esquema al día.
# Criterio de éxito declarado: el almacén queda vacío y con su esquema al día
# (intake §17.3.P.8; `Api/03 Guia-Onboarding-Developer.md` §3.2).
#
# La ruta del almacén sale de la configuración del servicio y no está escrita en el código.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
api_dir="$repo_root/src/GeometriaFactory.Api"

store_file="${GEOMETRIAFACTORY_STORE_FILE:-$api_dir/geometriafactory.db}"

rm -f "$store_file" "$store_file-shm" "$store_file-wal"
echo "Almacén borrado: $store_file"

# El esquema se aplica solo en el próximo arranque (`Infrastructure ADR-07`), y el arranque en
# dos fases no atiende ninguna petición hasta que la preparación terminó (`QG-11`).
echo "El esquema se aplica en el próximo arranque de scripts/run-api.sh."
