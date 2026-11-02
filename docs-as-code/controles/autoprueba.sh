#!/usr/bin/env bash
# Autoprueba de la compuerta de docs (expediente 0003, tramo T-2): el par rojo/verde plantado.
#
# Arma en un directorio temporal un corpus mínimo de docs-as-code/ que está en orden (VERDE) y lo verifica con los
# controles en modo estricto: todos tienen que dar 0. Después planta, de a uno, los tres defectos del criterio del PR 2
# y exige que el control que corresponde dé 1 (ROJO):
#   id-unico          un doc_id duplicado
#   enlaces-internos  un enlace a un archivo que no existe
#   delivery-publico  un «delivery: [restringido]»
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
}

control() {  # nombre → comando sobre el corpus temporal
  local c="$TMP/c"
  case "$1" in
    esquema)          python3 -I "$DG" esquema --raiz "$c" "$c/docs-as-code" ;;
    id-unico)         python3 -I "$DG" trazas --raiz "$c" "$c/docs-as-code" ;;
    fuera)            python3 -I "$DG" fuera --raiz "$c" "$c/docs-as-code" ;;
    enlaces-internos) python3 -I "$CTL/enlaces_internos.py" --raiz "$c" "$c/docs-as-code" ;;
    delivery-publico) python3 -I "$CTL/delivery_publico.py" --exigir --raiz "$c" "$c/docs-as-code" ;;
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
for c in esquema id-unico fuera enlaces-internos delivery-publico; do esperar "$c" 0 verde; done

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

if [ "$fallas" -gt 0 ]; then
  echo "== autoprueba: ROJO ($fallas fallas): la compuerta está rota"
  exit 1
fi
echo "== autoprueba: verde"
