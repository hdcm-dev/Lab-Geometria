#!/usr/bin/env bash
# Controles OpenAPI de la compuerta de docs (expediente 0003, tramo T-6): oasdiff y Spectral sobre la instantánea.
#
# Lo corre el job `openapi` de `.github/workflows/docs.yml`. Es aparte de `scripts/docs-local.sh` porque necesita
# Docker y baja dos imágenes con versión fija; la compuerta principal sigue sin red. El recuento contra el contrato
# (recuento_openapi.py) no necesita nada de esto y corre en docs-local.sh.
#
# Uso:  docs-as-code/controles/openapi.sh [REV_BASE]
#   REV_BASE  revisión con la que se compara la instantánea (en un PR, la rama base). Sin ella, oasdiff se saltea.
#
# Pasos:
#   autoprueba  par rojo/verde plantado de oasdiff y de Spectral sobre copias temporales          SIEMPRE bloquea
#   oasdiff     `breaking` (Tufin) de la instantánea de REV_BASE contra la del árbol, más `changelog`   avisa
#   spectral    las tres reglas de docs-as-code/controles/spectral.yaml sobre la instantánea         avisa
# En «avisa» un hallazgo es una advertencia del PR y no cambia el código de salida.
# Código de salida: 0 verde; 1 la autoprueba falló (los controles están rotos); 2 error de uso.
# No compila la Api, no usa secretos y no escribe en el árbol.
set -uo pipefail

OASDIFF_IMG="tufin/oasdiff:v1.11.4"
SPECTRAL_IMG="stoplight/spectral:6.15.0"
INST="docs-as-code/api/openapi-v1.json"
REGLAS="docs-as-code/controles/spectral.yaml"

RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
cd "$RAIZ"
EN_CI="${GITHUB_ACTIONS:-false}"
REV_BASE="${1:-}"
command -v docker >/dev/null || { echo "falta docker: estos controles corren en el runner o en un host con Docker" >&2; exit 2; }
command -v python3 >/dev/null || { echo "falta python3" >&2; exit 2; }
[ -f "$INST" ] || { echo "no existe $INST" >&2; exit 2; }

TMP="$(mktemp -d)"
trap 'rm -rf "$TMP"' EXIT
U="$(id -u):$(id -g)"

oasdiff()  { docker run --rm -u "$U" -v "$TMP:/t:ro" "$OASDIFF_IMG" "$@"; }
spectral() { docker run --rm -u "$U" -v "$TMP:/t:ro" -w /t "$SPECTRAL_IMG" lint --quiet -r /t/reglas.yaml "$@"; }
aviso() { if [ "$EN_CI" = true ]; then echo "::warning title=$1::$2"; else echo "aviso $1: $2"; fi; }
grupo_abrir()  { if [ "$EN_CI" = true ]; then echo "::group::$1"; else echo "== $1"; fi; }
grupo_cerrar() { if [ "$EN_CI" = true ]; then echo "::endgroup::"; fi; }

cp "$INST" "$TMP/head.json"
cp "$REGLAS" "$TMP/reglas.yaml"

# ---------- autoprueba (bloquea) ----------
fallas=0
esperar() {  # etiqueta, código esperado (0 verde / 1 rojo), comando...
  local etiqueta="$1" esperado="$2" rc=0; shift 2
  "$@" >/dev/null 2>&1 || rc=$?
  if [ "$rc" = "$esperado" ]; then echo "  ok    $etiqueta"; else echo "  FALLA $etiqueta (rc=$rc, se esperaba $esperado)"; fallas=$((fallas + 1)); fi
}
grupo_abrir "autoprueba openapi (bloquea)"
# Una operación en orden para las tres reglas de Spectral, y la misma instantánea sin una operación para oasdiff.
python3 -I - "$TMP" <<'EOF'
import json, sys, copy
t = sys.argv[1]
d = json.load(open(f'{t}/head.json'))
json.dump(d, open(f'{t}/verde-oasdiff.json', 'w'))
sin = copy.deepcopy(d)
ruta = sorted(sin['paths'])[0]
del sin['paths'][ruta]
json.dump(sin, open(f'{t}/rojo-sin-ruta.json', 'w'))
ok = {'openapi': '3.1.1', 'info': {'title': 'autoprueba', 'version': '1'},
      'paths': {'/x': {'get': {'operationId': 'X', 'summary': 'Prueba',
                               'responses': {'200': {'description': 'ok'}, '404': {'description': 'no'}}}}}}
json.dump(ok, open(f'{t}/verde.json', 'w'))
for nombre, cambio in (('sin-id', lambda o: o.pop('operationId')),
                       ('sin-resumen', lambda o: o.pop('summary')),
                       ('sin-error', lambda o: o['responses'].pop('404'))):
    r = copy.deepcopy(ok); cambio(r['paths']['/x']['get'])
    json.dump(r, open(f'{t}/rojo-{nombre}.json', 'w'))
EOF
esperar 'oasdiff verde: la instantánea contra sí misma'          0 oasdiff breaking /t/head.json /t/verde-oasdiff.json --fail-on ERR
esperar 'oasdiff rojo: una ruta retirada'                         1 oasdiff breaking /t/head.json /t/rojo-sin-ruta.json --fail-on ERR
esperar 'spectral verde: operación en orden'                      0 spectral /t/verde.json
esperar 'spectral rojo: operacion-con-id'                         1 spectral /t/rojo-sin-id.json
esperar 'spectral rojo: operacion-con-resumen'                    1 spectral /t/rojo-sin-resumen.json
esperar 'spectral rojo: operacion-con-error-documentado'          1 spectral /t/rojo-sin-error.json
grupo_cerrar
if [ "$fallas" -gt 0 ]; then echo "== openapi: ROJO ($fallas fallas en la autoprueba): los controles están rotos"; exit 1; fi

# ---------- oasdiff (avisa) ----------
resumen=()
grupo_abrir "oasdiff (avisa)"
if [ -z "$REV_BASE" ]; then
  echo "  sin revisión base: se saltea"; resumen+=("  oasdiff                  salteado (sin base)")
elif ! git show "$REV_BASE:$INST" > "$TMP/base.json" 2>/dev/null; then
  echo "  $REV_BASE no tiene $INST: se saltea"; resumen+=("  oasdiff                  salteado (la base no tiene instantánea)")
else
  echo "  base: $REV_BASE"
  rc=0; salida="$(oasdiff breaking /t/base.json /t/head.json --fail-on ERR 2>&1)" || rc=$?
  printf '%s\n' "$salida"
  oasdiff changelog /t/base.json /t/head.json 2>&1 | sed 's/^/  changelog: /'
  if [ "$rc" = 0 ]; then resumen+=("  oasdiff                  verde")
  else aviso oasdiff "cambios que rompen en $INST frente a $REV_BASE (ver el registro del job)"; resumen+=("  oasdiff                  avisa (con hallazgos)"); fi
fi
grupo_cerrar

# ---------- spectral (avisa) ----------
grupo_abrir "spectral (avisa)"
rc=0; salida="$(spectral /t/head.json 2>&1)" || rc=$?
printf '%s\n' "$salida"
if [ "$rc" = 0 ]; then resumen+=("  spectral                 verde")
else
  n="$(printf '%s\n' "$salida" | grep -c ' error ' || true)"
  aviso spectral "$n hallazgos de las reglas de $REGLAS en $INST (ver el registro del job)"
  resumen+=("  spectral                 avisa (con hallazgos: $n)")
fi
grupo_cerrar

echo "== resumen de los controles OpenAPI"
echo "  autoprueba               verde"
printf '%s\n' "${resumen[@]}"
echo "== openapi: verde"
