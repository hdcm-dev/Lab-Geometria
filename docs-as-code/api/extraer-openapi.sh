#!/usr/bin/env bash
# Extrae la instantánea OpenAPI de la Api (expediente 0003, tramo T-5; decisiones K-07 y K-12).
#
# AISLADA (K-12): compila la Api, la levanta como proceso local con un almacén EFÍMERO en un directorio
# temporal, una clave de firma DESCARTABLE generada en el momento y un puerto PROPIO en 127.0.0.1, baja
# `/openapi/v1.json` y apaga el proceso. No usa Docker ni habla con ningún contenedor del despliegue.
#
# Uso:  bash docs-as-code/api/extraer-openapi.sh [salida]   (por omisión docs-as-code/api/openapi-v1.json)
# Variables: GF_OPENAPI_PUERTO (por omisión 5199).
set -euo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
salida="${1:-$raiz/docs-as-code/api/openapi-v1.json}"
puerto="${GF_OPENAPI_PUERTO:-5199}"

tmp="$(mktemp -d)"
pid=""
limpiar() {
  if [[ -n "$pid" ]]; then kill "$pid" 2>/dev/null || true; wait "$pid" 2>/dev/null || true; fi
  rm -rf "$tmp"
}
trap limpiar EXIT

dotnet build "$raiz/src/GeometriaFactory.Api/GeometriaFactory.Api.csproj" -c Release -o "$tmp/bin" -nologo -v quiet

# Producción con el documento publicado por su llave (`ApiDocumentation.cs`), no Development: se describe
# la superficie que se despliega. La clave sólo vive en este proceso y se descarta al salir.
clave="$(head -c 48 /dev/urandom | base64 | tr -d '\n')"
(
  cd "$tmp/bin"
  ASPNETCORE_ENVIRONMENT=Production \
  ASPNETCORE_URLS="http://127.0.0.1:$puerto" \
  Documentacion__Publicada=true \
  AccessToken__SigningKey="$clave" \
  ConnectionStrings__Store="Data Source=$tmp/almacen.db" \
    exec dotnet GeometriaFactory.Api.dll
) >"$tmp/api.log" 2>&1 &
pid=$!

for _ in $(seq 1 60); do
  if curl -fsS "http://127.0.0.1:$puerto/openapi/v1.json" -o "$tmp/openapi.json" 2>/dev/null; then break; fi
  if ! kill -0 "$pid" 2>/dev/null; then cat "$tmp/api.log" >&2; echo "la Api no arrancó" >&2; exit 1; fi
  sleep 1
done
[[ -s "$tmp/openapi.json" ]] || { cat "$tmp/api.log" >&2; echo "sin documento tras 60 s" >&2; exit 1; }

# Guarda de K-12: ningún contenedor del despliegue aparece en lo que hizo la Api.
if grep -q 'lab-geometria-' "$tmp/api.log"; then echo "el registro nombra un contenedor lab-geometria-*" >&2; exit 1; fi

# Formato estable (claves en el orden que da la Api, sangría de 2) para que el diff muestre sólo cambios reales.
python3 -I -c 'import json,sys; d=json.load(open(sys.argv[1])); open(sys.argv[2],"w").write(json.dumps(d,ensure_ascii=False,indent=2)+"\n")' \
  "$tmp/openapi.json" "$salida"
echo "instantánea escrita en ${salida#"$raiz/"}"
