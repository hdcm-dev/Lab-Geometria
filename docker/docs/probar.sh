#!/usr/bin/env bash
# Prueba con curl de la imagen del portal (expediente 0003, folio 018 §8, PR-P7).
#
#   docker/docs/probar.sh [IMAGEN]      # por defecto construye lab-geometria-docs:prueba desde el árbol
#
# Levanta la imagen como se va a correr (--read-only, tmpfs en /tmp, sin capacidades, sin escalar privilegios) en un
# puerto local efímero, espera el healthcheck y comprueba content-types, 404 de rutas internas, redirección de
# directorio y cabeceras. No publica la imagen ni la empuja a ningún registro. Necesita bash, docker y curl.
# Código de salida: 0 todo en orden; 1 alguna comprobación falló; 2 error de uso.
set -uo pipefail

RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
command -v docker >/dev/null || { echo "probar: falta docker" >&2; exit 2; }
command -v curl >/dev/null || { echo "probar: falta curl" >&2; exit 2; }

IMAGEN="${1:-}"
REVISION="$(git -C "$RAIZ" rev-parse HEAD 2>/dev/null || echo desconocida)"
if [ -z "$IMAGEN" ]; then
  IMAGEN=lab-geometria-docs:prueba
  docker build -q -f "$RAIZ/docker/docs/Dockerfile" \
    --build-arg REVISION="$REVISION" \
    --build-arg SOURCE_DATE_EPOCH="$(git -C "$RAIZ" log -1 --format=%ct 2>/dev/null || echo 0)" \
    -t "$IMAGEN" "$RAIZ" >/dev/null || { echo "probar: ROJO, la imagen no se construyó" >&2; exit 1; }
fi

NOMBRE="lab-geometria-docs-prueba-$$"
docker run -d --rm --name "$NOMBRE" --read-only --tmpfs /tmp --cap-drop ALL --security-opt no-new-privileges \
  -p 127.0.0.1::8080 "$IMAGEN" >/dev/null || { echo "probar: no arrancó el contenedor" >&2; exit 1; }
trap 'docker rm -f "$NOMBRE" >/dev/null 2>&1' EXIT
PUERTO="$(docker port "$NOMBRE" 8080/tcp | head -1 | sed 's/.*://')"
BASE="http://127.0.0.1:$PUERTO"

estado=""
for _ in $(seq 1 30); do
  estado="$(docker inspect -f '{{.State.Health.Status}}' "$NOMBRE" 2>/dev/null)"
  [ "$estado" = healthy ] && break
  sleep 2
done

fallas=0
ok()    { echo "  ok    $1"; }
falla() { echo "  FALLA $1"; fallas=$((fallas + 1)); }

[ "$estado" = healthy ] && ok "healthcheck sobre /llms.txt: healthy" || falla "healthcheck: «$estado»"

# codigo RUTA ESPERADO
codigo() {
  local c; c="$(curl -s -o /dev/null -w '%{http_code}' "$BASE$1")"
  [ "$c" = "$2" ] && ok "$1 → $c" || falla "$1 → $c, se esperaba $2"
}
# tipo RUTA PREFIJO-DE-CONTENT-TYPE
tipo() {
  local t; t="$(curl -s -o /dev/null -w '%{http_code} %{content_type}' "$BASE$1")"
  case "$t" in "200 $2"*) ok "$1 → $t" ;; *) falla "$1 → «$t», se esperaba 200 $2" ;; esac
}

echo "== content-types"
tipo /                                   'text/html; charset=utf-8'
tipo /index.md                           'text/markdown; charset=utf-8'
tipo /docs-as-code/glosario/             'text/html; charset=utf-8'
tipo /docs-as-code/glosario.md           'text/markdown; charset=utf-8'
tipo /llms.txt                           'text/plain; charset=utf-8'
tipo /llms-full.txt                      'text/plain; charset=utf-8'
tipo /.well-known/api-catalog            'application/linkset+json'
tipo /api/                               'text/html; charset=utf-8'
tipo /api/openapi-v1.json                'application/json'
tipo /sitemap.xml                        'text/xml'

echo "== JSON servido idéntico a la instantánea"
if curl -s "$BASE/api/openapi-v1.json" | cmp -s - "$RAIZ/docs-as-code/api/openapi-v1.json"; then
  ok "/api/openapi-v1.json idéntico a docs-as-code/api/openapi-v1.json"
else
  falla "/api/openapi-v1.json distinto de la instantánea"
fi

echo "== 404 (rutas internas, archivos ocultos, directorios sin índice, anuncios descartados)"
codigo /SDD/Expedientes/                 404
codigo /SDD/Docs/README.md               404
codigo /.git/HEAD                        404
codigo /docs-as-code/controles/autoprueba.sh 404
codigo /assets/                          404
codigo /search/                          404
codigo /no-existe/                       404
codigo /50x.html                         404
codigo /.well-known/mcp/server-card.json 404
codigo /.well-known/agent-card.json      404
body="$(curl -s "$BASE/no-existe/")"
case "$body" in *nginx*) falla "la página 404 delata el servidor" ;; *) ok "la página 404 es la del portal" ;; esac

echo "== directorio sin barra"
r="$(curl -s -o /dev/null -w '%{http_code} %{redirect_url}' "$BASE/docs-as-code/glosario")"
case "$r" in "301 $BASE/docs-as-code/glosario/"|"301 /docs-as-code/glosario/") ok "/docs-as-code/glosario → $r" ;;
  *) falla "/docs-as-code/glosario → «$r», se esperaba 301 a la ruta con barra" ;; esac

echo "== cabeceras"
cab="$(curl -s -D - -o /dev/null "$BASE/" | tr -d '\r')"
cab404="$(curl -s -D - -o /dev/null "$BASE/no-existe/" | tr -d '\r')"
for h in 'X-Content-Type-Options: nosniff' 'Content-Security-Policy: default-src '"'self'" \
         'Referrer-Policy: no-referrer' 'X-Frame-Options: DENY' "X-Portal-Revision: $REVISION"; do
  printf '%s\n' "$cab" | grep -qiF "$h" && ok "200 lleva «${h%%:*}»" || falla "200 sin «$h»"
  printf '%s\n' "$cab404" | grep -qiF "${h%%:*}:" && ok "404 lleva «${h%%:*}»" || falla "404 sin «${h%%:*}»"
done
printf '%s\n' "$cab" | grep -qiE '^Server: nginx/[0-9]' && falla "Server delata la versión" || ok "Server sin versión"

echo "== usuario y sólo lectura"
u="$(docker exec "$NOMBRE" id -u)"; [ "$u" = 101 ] && ok "usuario 101" || falla "usuario $u"
if docker exec "$NOMBRE" sh -c 'touch /usr/share/nginx/html/x' 2>/dev/null; then falla "el sitio es escribible"; else ok "sitio de sólo lectura"; fi

if [ "$fallas" -gt 0 ]; then echo "== probar: ROJO ($fallas fallas)"; exit 1; fi
echo "== probar: verde"
