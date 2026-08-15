#!/usr/bin/env bash
# Construcción del producto: encadena la generación del bundle con la compilación de la solución.
# Criterio de éxito declarado: termina en 0 y SIN ADVERTENCIAS (`QG-01` en Domain, Contracts,
# Application, Infrastructure y Api). La puerta la impone `Directory.Build.props` con
# TreatWarningsAsErrors, de modo que una advertencia detiene la construcción y no se arrastra
# (`Api/03 Guia-Onboarding-Developer.md` §2).
#
# LA REGLA DE CONFIGURACIÓN DEL REPOSITORIO, que este guion encabeza (`Pipeline-Producto.md`
# §3.1): **la configuración se declara siempre y en los dos lados —el que construye y el que
# ejecuta— aunque el valor por omisión coincida.** Este guion produce `Release`, y por eso todo
# lo que después levanta, prueba o publica esa salida dice `Release` también. La coherencia por
# omisión no cuenta: el día que alguien agregue la configuración de un solo lado, el otro lado
# sigue resolviendo `Debug` y se verifica una salida que nadie construyó.
#
# Quien quiera comprobar que la regla se cumple en todo el árbol:
#   bash scripts/verify-explicit-configuration.sh
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

"$repo_root/scripts/build-visor.sh"

cd "$repo_root"
dotnet restore GeometriaFactory.sln
dotnet build GeometriaFactory.sln --configuration Release --no-restore

echo "Construcción terminada."
