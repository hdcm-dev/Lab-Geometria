#!/usr/bin/env bash
# Compuerta de docs de Lab-Geometria (expediente 0003, tramo T-2; artefacto A-07).
#
# Es el MISMO guion que corre `.github/workflows/docs.yml`: en local da los mismos pasos en el mismo orden.
# SÓLO EN EL HOST O EN EL RUNNER: necesita bash y python3, y la imagen del SDK no trae python3 (AGENTS.md:81).
# No compila nada y no escribe en el árbol. Sólo el paso `portal` usa red y docker (se omite con DOCS_PORTAL=no).
#
# Uso:  scripts/docs-local.sh
#       DOCS_MODO_DOCS_AS_CODE=bloquea scripts/docs-local.sh   # cómo correría docs-as-code/** en bloquea
#
# Pasos:
#   huella      la copia .docs-guard/ coincide con las huellas de .docs-guard/source.lock          SIEMPRE bloquea
#   autoprueba  par rojo/verde plantado de cada control (docs-as-code/controles/autoprueba.sh)    SIEMPRE bloquea
#   ámbitos     esquema, id-unico, fuera, enlaces-internos y delivery-publico sobre:
#                 docs-as-code/**   modo DOCS_MODO_DOCS_AS_CODE (por defecto «avisa»; decisión D-10)
#                 README.md         avisa (nunca bloquea en la ola 1, H-14)
#                 SDD/Docs/**       avisa, sin _legacy/ (generado; lo desalineado se eleva, AGENTS.md:61)
#   punto de uso  anclas, limite-didactico y pista-sin-enlaces sobre docs-as-code/** (tramo T-3), en avisa
#   recuento    la instantánea OpenAPI contra la tabla de operaciones de Contratos-REST.md (tramo T-6), en avisa.
#   portal      build del portal en contenedor fijo (scripts/docs-portal.sh) y portal-negativo sobre el sitio (PR-P4),
#               en avisa. Necesita docker y red (PyPI con hashes): con DOCS_PORTAL=no, o sin docker, queda «omitido».
#               En CI lo corre el job `portal` de docs.yml, aparte, y el job `docs` lo omite para seguir sin red.
#   versiones-fijas  workflows y Dockerfiles sin versiones flotantes ni imágenes sin digest (folio 018), en avisa
#               oasdiff y Spectral necesitan Docker y corren aparte (docs-as-code/controles/openapi.sh, job `openapi`)
# En «avisa» un control con hallazgos queda en el resumen y no cambia el código de salida.
# Código de salida: 0 verde; 1 algún paso que bloquea dio rojo; 2 error de uso de algún control.
set -uo pipefail

RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$RAIZ"
DG=".docs-guard/dg.py"
CTL="docs-as-code/controles"
MODO_DAC="${DOCS_MODO_DOCS_AS_CODE:-avisa}"
EN_CI="${GITHUB_ACTIONS:-false}"
case "$MODO_DAC" in avisa|bloquea) ;; *) echo "DOCS_MODO_DOCS_AS_CODE: «$MODO_DAC» no es avisa ni bloquea" >&2; exit 2 ;; esac
command -v python3 >/dev/null || { echo "falta python3: este guion corre sólo en el host o en el runner" >&2; exit 2; }

RESUMEN=()
ROJO=0
ERROR_USO=0

grupo_abrir()  { if [ "$EN_CI" = true ]; then echo "::group::$1"; else echo "== $1"; fi; }
grupo_cerrar() { if [ "$EN_CI" = true ]; then echo "::endgroup::"; fi; }

# paso NOMBRE MODO comando...  — corre el comando, lo anota en el resumen y aplica el modo.
paso() {
  local nombre="$1" modo="$2"; shift 2
  local rc=0 salida
  grupo_abrir "$nombre ($modo)"
  if [ "$modo" = avisa ] && [ "$EN_CI" = true ]; then
    # En avisa, un hallazgo es una advertencia del PR, no un error.
    salida="$("$@" 2>&1)" || rc=$?
    printf '%s\n' "$salida" | sed 's/^::error /::warning /'
  else
    "$@" || rc=$?
  fi
  grupo_cerrar
  local estado
  case "$rc" in
    0) estado=verde ;;
    1) if [ "$modo" = bloquea ]; then estado=ROJO; ROJO=1; else estado="avisa (con hallazgos)"; fi ;;
    *) estado="ERROR DE USO (rc=$rc)"; ERROR_USO=1 ;;
  esac
  RESUMEN+=("$(printf '  %-34s %s' "$nombre" "$estado")")
}

huella() {
  local sha_ok=0 n=0 h f
  while read -r _ h f; do
    n=$((n + 1))
    if [ "$(sha256sum ".docs-guard/$f" 2>/dev/null | cut -d' ' -f1)" = "$h" ]; then
      echo "  ok    $f"
    else
      echo "  FALLA $f: no coincide con source.lock (la copia no se edita acá)"
      sha_ok=1
    fi
  done < <(grep '^sha256: ' .docs-guard/source.lock)
  [ "$n" -gt 0 ] || { echo "  source.lock sin huellas"; return 2; }
  echo "  origen $(grep '^sha: ' .docs-guard/source.lock | cut -d' ' -f2) · $n archivos"
  return $sha_ok
}

# Los .md de SDD/Docs sin el registro histórico _legacy/ (AGENTS.md:60: registro histórico, no se reescribe).
mapfile -t SDD_DOCS < <(find SDD/Docs -name '*.md' -not -path '*/_legacy/*' | sort)

paso huella     bloquea huella
paso autoprueba bloquea bash "$CTL/autoprueba.sh"

paso "docs-as-code · esquema"          "$MODO_DAC" python3 -I "$DG" esquema docs-as-code
paso "docs-as-code · id-unico"         "$MODO_DAC" python3 -I "$DG" trazas docs-as-code
paso "docs-as-code · fuera"            "$MODO_DAC" python3 -I "$DG" fuera docs-as-code
paso "docs-as-code · enlaces-internos" "$MODO_DAC" python3 -I "$CTL/enlaces_internos.py" docs-as-code
paso "docs-as-code · delivery-publico" "$MODO_DAC" python3 -I "$CTL/delivery_publico.py" --exigir docs-as-code

# Punto de uso (tramo T-3): arrancan en avisa y no siguen a DOCS_MODO_DOCS_AS_CODE. limite-didactico pasa a bloquea
# después de dos corridas verdes de su par; anclas y pista-sin-enlaces siguen en avisa (resolución del expediente 0003, §5.2).
paso "docs-as-code · anclas"            avisa python3 -I "$CTL/anclas.py" docs-as-code
paso "docs-as-code · limite-didactico"  avisa python3 -I "$CTL/limite_didactico.py" docs-as-code
paso "docs-as-code · pista-sin-enlaces" avisa python3 -I "$CTL/pista_sin_enlaces.py" docs-as-code

# Recuento OpenAPI (tramo T-6): en avisa fijo; lo que encuentra en el contrato del SDD se eleva, no se corrige.
paso "docs-as-code · recuento-openapi"  avisa python3 -I "$CTL/recuento_openapi.py"

# Portal (folio 018 §8, PR-P4): mkdocs build --strict en la imagen fija y portal-negativo sobre el sitio, en avisa.
# El sitio queda fuera del árbol (directorio temporal). DOCS_PORTAL=no lo omite (job `docs` de CI, sin red).
if [ "${DOCS_PORTAL:-si}" != no ] && command -v docker >/dev/null; then
  paso "portal · build y negativo"     avisa bash scripts/docs-portal.sh
else
  RESUMEN+=("$(printf '  %-34s %s' "portal · build y negativo" "omitido (DOCS_PORTAL=no o sin docker)")")
fi

# Versiones fijas (folio 018 §7, PR-P2): workflows y Dockerfiles. Arranca en avisa y pasa a bloquea tras una corrida
# limpia; hoy quedan hallazgos fuera de deploy-front-ftp.yml (ci.yml, e2e.yml, deploy/Dockerfile*).
paso "workflows · versiones-fijas"      avisa python3 -I "$CTL/versiones_fijas.py"

# Manifiesto del portal (folio 018 §1 y §7, PR-P3): lista permitida en docs-as-code/portal/publicar.yml. Arranca en
# avisa y pasa a bloquea tras una corrida limpia (PR-P7).
paso "portal · manifiesto"             avisa python3 -I "$CTL/portal_manifiesto.py"

paso "README.md · esquema"             avisa python3 -I "$DG" esquema README.md
paso "README.md · enlaces-internos"    avisa python3 -I "$CTL/enlaces_internos.py" README.md

paso "SDD/Docs · esquema"              avisa python3 -I "$DG" esquema "${SDD_DOCS[@]}"
paso "SDD/Docs · id-unico"             avisa python3 -I "$DG" trazas "${SDD_DOCS[@]}"
paso "SDD/Docs · enlaces-internos"     avisa python3 -I "$CTL/enlaces_internos.py" "${SDD_DOCS[@]}"
paso "SDD/Docs · delivery-publico"     avisa python3 -I "$CTL/delivery_publico.py" "${SDD_DOCS[@]}"

echo "== resumen de la compuerta de docs (docs-as-code/** en $MODO_DAC)"
printf '%s\n' "${RESUMEN[@]}"
if [ "$ERROR_USO" = 1 ]; then echo "== docs: ERROR DE USO"; exit 2; fi
if [ "$ROJO" = 1 ]; then echo "== docs: ROJO"; exit 1; fi
echo "== docs: verde"
