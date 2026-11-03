#!/usr/bin/env bash
# Autoprueba de la compuerta de docs (expediente 0003, tramo T-2): el par rojo/verde plantado.
#
# Arma en un directorio temporal un corpus mínimo de docs-as-code/ que está en orden (VERDE) y lo verifica con los
# controles en modo estricto: todos tienen que dar 0. Después planta, de a uno, los tres defectos del criterio del PR 2
# y exige que el control que corresponde dé 1 (ROJO):
#   id-unico          un doc_id duplicado
#   enlaces-internos  un enlace a un archivo que no existe
#   delivery-publico  un «delivery: [restringido]»
# y, desde el tramo T-3, los de los controles del punto de uso sobre un catálogo y un .razor mínimos:
#   anclas            un texto de la pantalla que cambió y un id que no existe
#   limite-didactico  un envío JSON completo, una clase C# y una plantilla de la capa de IA copiada en el catálogo
#   pista-sin-enlaces un enlace en la capa pista
# y, desde el tramo T-6, el del recuento OpenAPI sobre un contrato y una instantánea mínimos:
#   recuento-openapi  una operación de más en la instantánea y una cifra del título que no coincide
# y, desde la revisión externa R-G2, el de enlaces externos contra un servidor local que devuelve 404:
#   enlaces-externos  un enlace a una ruta que da 404 y una excepción vencida
# Si un control no da rojo sobre su defecto o no da verde sin él, la compuerta misma está rota y este guion sale con 1.
# No toca el árbol del repositorio. Requiere bash y python3 (sólo en el host o en el runner; la imagen del SDK no trae
# python3, AGENTS.md:81).
set -euo pipefail

RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
DG="$RAIZ/.docs-guard/dg.py"
CTL="$RAIZ/docs-as-code/controles"
TMP="$(mktemp -d)"
trap 'rm -rf "$TMP"' EXIT
export GITHUB_ACTIONS=false  # salida en texto: los hallazgos plantados no son anotaciones del PR

armar() {
  rm -rf "$TMP/c"
  mkdir -p "$TMP/c/docs-as-code"
  cat > "$TMP/c/docs-as-code/a.md" <<'EOF'
---
doc_id: FIC-GEO-901
doc_type: ficha
title: Pieza A de la autoprueba
description: Pieza en orden que usa la autoprueba de la compuerta de docs.
status: borrador
origin: ia-generated
confidence: media
owner: autoprueba
last_review: 2026-11-02
audience: [desarrollo]
delivery: [publico]
traces: [FIC-GEO-902]
---

# Pieza A

## Sección de prueba

Ver [la pieza B](b.md).
EOF
  cat > "$TMP/c/docs-as-code/b.md" <<'EOF'
---
doc_id: FIC-GEO-902
doc_type: anexo
title: Pieza B de la autoprueba
description: Segunda pieza en orden que usa la autoprueba de la compuerta de docs.
status: borrador
origin: ia-generated
confidence: media
owner: autoprueba
last_review: 2026-11-02
audience: [desarrollo]
delivery: [publico]
traces: [FIC-GEO-901]
---

# Pieza B

Vuelve a [la sección de A](a.md#sección-de-prueba).
EOF
  mkdir -p "$TMP/c/docs-as-code/punto-de-uso" "$TMP/c/src"
  cat > "$TMP/c/src/Pantalla.razor" <<'EOF'
<label for="campo-prueba">Campo de prueba</label>
<input id="campo-prueba" aria-describedby="campo-prueba-pista" />
<span id="campo-prueba-pista">Escribí el <code>Nombre</code> tal como lo imprime tu programa.</span>
EOF
  cat > "$TMP/c/src/Plantillas.cs" <<'EOF'
("Revisá cómo calcula el área tu figura de prueba.", "Otra plantilla de respaldo de la autoprueba.")
EOF
  cat > "$TMP/c/docs-as-code/punto-de-uso/catalogo.md" <<'EOF'
---
doc_id: PU-GEO-901
doc_type: anexo
title: Catálogo de la autoprueba
description: Catálogo mínimo en orden que usa la autoprueba de los controles del punto de uso.
status: borrador
origin: ia-generated
confidence: media
owner: autoprueba
last_review: 2026-11-02
audience: [desarrollo]
delivery: [publico]
traces: [FIC-GEO-901]
---

# Catálogo de la autoprueba

<!-- catalogo:inicio -->
| contexto | capa | texto | fuente |
| --- | --- | --- | --- |
| `web/campo-prueba` | etiqueta | Campo de prueba | `src/Pantalla.razor:1` |
| `web/campo-prueba` | pista | Escribí el `Nombre` tal como lo imprime tu programa. | `src/Pantalla.razor:3` |
| `web/campo-prueba` | info | La forma completa está en la pieza A. | `docs-as-code/a.md` |
<!-- catalogo:fin -->
EOF
  # Contrato e instantánea mínimos del recuento OpenAPI (tramo T-6), fuera de docs-as-code/ del corpus.
  cat > "$TMP/c/contrato.md" <<'EOF'
## 3. Operaciones: los dos puntos de acceso

| Punto | Verbo | Qué hace | Papel exigido | Códigos | CU de la capa de aplicación |
| --- | --- | --- | --- | --- | --- |
| A-01 | `POST` | Canjear | Ninguno | `200`, `401` | Ingreso |
| A-16 | `GET` | Salud | Ninguno | `200` | Ninguno |

| Punto | Ruta pública | Punto | Ruta pública |
| --- | --- | --- | --- |
| A-01 | `POST /v1/auth/token` | A-16 | `GET /salud` |
EOF
  cat > "$TMP/c/api.json" <<'EOF'
{"openapi": "3.1.1", "info": {"title": "autoprueba", "version": "1"}, "paths": {
  "/v1/auth/token": {"post": {"responses": {"200": {"description": "ok"}, "401": {"description": "no"}}}},
  "/salud": {"get": {"responses": {"200": {"description": "ok"}}}}}}
EOF
}

control() {  # nombre → comando sobre el corpus temporal
  local c="$TMP/c"
  case "$1" in
    esquema)          python3 -I "$DG" esquema --raiz "$c" "$c/docs-as-code" ;;
    id-unico)         python3 -I "$DG" trazas --raiz "$c" "$c/docs-as-code" ;;
    fuera)            python3 -I "$DG" fuera --raiz "$c" "$c/docs-as-code" ;;
    enlaces-internos) python3 -I "$CTL/enlaces_internos.py" --raiz "$c" "$c/docs-as-code" ;;
    delivery-publico) python3 -I "$CTL/delivery_publico.py" --exigir --raiz "$c" "$c/docs-as-code" ;;
    anclas)            python3 -I "$CTL/anclas.py" --raiz "$c" "$c/docs-as-code" ;;
    limite-didactico)  python3 -I "$CTL/limite_didactico.py" --plantillas "$c/src/Plantillas.cs" --raiz "$c" "$c/docs-as-code" ;;
    pista-sin-enlaces) python3 -I "$CTL/pista_sin_enlaces.py" --raiz "$c" "$c/docs-as-code" ;;
    recuento-openapi)  python3 -I "$CTL/recuento_openapi.py" --contrato "$c/contrato.md" --instantanea "$c/api.json" --raiz "$c" ;;
  esac
}

fallas=0
esperar() {  # control, código esperado, etiqueta
  local rc=0
  control "$1" > "$TMP/salida.txt" 2>&1 || rc=$?
  if [ "$rc" = "$2" ]; then
    echo "  ok    $3: $1 dio $rc"
  else
    echo "  FALLA $3: $1 dio $rc, se esperaba $2"
    sed 's/^/        /' "$TMP/salida.txt"
    fallas=$((fallas + 1))
  fi
}

echo "== autoprueba: verde (corpus en orden)"
armar
for c in esquema id-unico fuera enlaces-internos delivery-publico anclas limite-didactico pista-sin-enlaces recuento-openapi; do esperar "$c" 0 verde; done

echo "== autoprueba: rojo (un defecto plantado por vez)"
armar
sed -i 's/^doc_id: FIC-GEO-902$/doc_id: FIC-GEO-901/' "$TMP/c/docs-as-code/b.md"
esperar id-unico 1 'rojo doc_id duplicado'

armar
sed -i 's/(b\.md)/(no-existe.md)/' "$TMP/c/docs-as-code/a.md"
esperar enlaces-internos 1 'rojo enlace roto'

armar
sed -i 's/(a\.md#sección-de-prueba)/(a.md#no-existe)/' "$TMP/c/docs-as-code/b.md"
esperar enlaces-internos 1 'rojo ancla rota'

armar
sed -i 's/^delivery: \[publico\]$/delivery: [restringido]/' "$TMP/c/docs-as-code/a.md"
esperar delivery-publico 1 'rojo delivery restringido'

armar
sed -i '/^delivery: /d' "$TMP/c/docs-as-code/a.md"
esperar delivery-publico 1 'rojo delivery ausente'

armar
sed -i 's/^doc_type: ficha$/doc_type: inventado/' "$TMP/c/docs-as-code/a.md"
esperar esquema 1 'rojo doc_type fuera del vocabulario'

armar
sed -i 's/Campo de prueba</Campo cambiado</' "$TMP/c/src/Pantalla.razor"
esperar anclas 1 'rojo texto de la pantalla cambiado'

armar
sed -i 's|`web/campo-prueba` \| etiqueta|`web/campo-inexistente` \| etiqueta|' "$TMP/c/docs-as-code/punto-de-uso/catalogo.md"
esperar anclas 1 'rojo id inexistente'

armar
sed -i 's/^| `web\/campo-prueba` | info | .*$/| `web\/campo-prueba` | ejemplo | [{"Tipo": "Cubo", "Area": 6}] | `docs-as-code\/a.md` |/' "$TMP/c/docs-as-code/punto-de-uso/catalogo.md"
esperar limite-didactico 1 'rojo envío JSON completo'

armar
printf '\n```csharp\npublic class Cubo { }\n```\n' >> "$TMP/c/docs-as-code/punto-de-uso/catalogo.md"
esperar limite-didactico 1 'rojo clase C#'

armar
sed -i 's/^| `web\/campo-prueba` | info | .*$/| `web\/campo-prueba` | info | Revisá cómo calcula el área tu figura de prueba. | `docs-as-code\/a.md` |/' "$TMP/c/docs-as-code/punto-de-uso/catalogo.md"
esperar limite-didactico 1 'rojo plantilla de IA duplicada'

armar
sed -i 's/tal como lo imprime tu programa\. |/tal como lo imprime tu programa, ver [la guía](..\/a.md). |/' "$TMP/c/docs-as-code/punto-de-uso/catalogo.md"
esperar pista-sin-enlaces 1 'rojo enlace en la pista'

armar
python3 -I - "$TMP/c/api.json" <<'EOF2'
import json, sys
d = json.load(open(sys.argv[1]))
d['paths']['/v1/extra'] = {'get': {'responses': {'200': {'description': 'ok'}}}}
json.dump(d, open(sys.argv[1], 'w'))
EOF2
esperar recuento-openapi 1 'rojo operación de más en la instantánea'

armar
sed -i 's/los dos puntos de acceso/los tres puntos de acceso/' "$TMP/c/contrato.md"
esperar recuento-openapi 1 'rojo cifra del título que no coincide'

echo "== autoprueba: enlaces externos (servidor local que devuelve 404)"
mkdir -p "$TMP/x"
cat > "$TMP/x/servidor.py" <<'EOF2'
import http.server, sys
class H(http.server.BaseHTTPRequestHandler):
    def responder(self):
        self.send_response(200 if self.path == '/ok' else 404)
        self.end_headers()
    do_HEAD = do_GET = responder
    def log_message(self, *a):
        pass
s = http.server.HTTPServer(('127.0.0.1', 0), H)
open(sys.argv[1], 'w').write(str(s.server_address[1]))
s.serve_forever()
EOF2
python3 -I "$TMP/x/servidor.py" "$TMP/x/puerto" &
SERVIDOR=$!
trap 'kill "$SERVIDOR" 2>/dev/null || true; rm -rf "$TMP"' EXIT
for _ in $(seq 50); do [ -s "$TMP/x/puerto" ] && break; sleep 0.1; done
PUERTO="$(cat "$TMP/x/puerto")"
externos() {  # archivo de excepciones → código de salida del control en estricto
  local rc=0
  python3 -I "$CTL/enlaces_externos.py" --raiz "$TMP/x" --estricto --permitir-local --hoy 2026-11-03 \
    --excepciones "$1" "$TMP/x/doc.md" > "$TMP/salida.txt" 2>&1 || rc=$?
  echo "$rc"
}
esperar_externos() {  # código esperado, etiqueta, archivo de excepciones
  local rc; rc="$(externos "$3")"
  if [ "$rc" = "$1" ]; then
    echo "  ok    $2: enlaces-externos dio $rc"
  else
    echo "  FALLA $2: enlaces-externos dio $rc, se esperaba $1"
    sed 's/^/        /' "$TMP/salida.txt"
    fallas=$((fallas + 1))
  fi
}
: > "$TMP/x/sin-excepciones.txt"
echo "Ver [el sitio](http://127.0.0.1:$PUERTO/ok)." > "$TMP/x/doc.md"
esperar_externos 0 'verde enlace que responde' "$TMP/x/sin-excepciones.txt"
echo "Ver [el sitio](http://127.0.0.1:$PUERTO/falta)." > "$TMP/x/doc.md"
esperar_externos 1 'rojo enlace que da 404' "$TMP/x/sin-excepciones.txt"
echo "http://127.0.0.1:$PUERTO/falta | autoprueba | vence 2026-12-31" > "$TMP/x/vigente.txt"
esperar_externos 0 'verde 404 con excepción vigente' "$TMP/x/vigente.txt"
echo "http://127.0.0.1:$PUERTO/falta | autoprueba | vence 2026-11-02" > "$TMP/x/vencida.txt"
esperar_externos 1 'rojo 404 con excepción vencida' "$TMP/x/vencida.txt"
echo "http://127.0.0.1:$PUERTO/falta | autoprueba" > "$TMP/x/mal.txt"
esperar_externos 2 'error de uso excepción sin vencimiento' "$TMP/x/mal.txt"
kill "$SERVIDOR" 2>/dev/null || true

if [ "$fallas" -gt 0 ]; then
  echo "== autoprueba: ROJO ($fallas fallas): la compuerta está rota"
  exit 1
fi
echo "== autoprueba: verde"
