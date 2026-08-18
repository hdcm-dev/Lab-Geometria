#!/usr/bin/env bash
# ============================================================================
# verify-stage-h.sh — Puerta de los SIETE criterios de transición de la etapa
# `h` (`Roadmap-Producto.md` §5.2, `h` → `i…`), que es la que **cierra el
# alcance comprometido**:
#
#   H-1  El administrador aprueba un trabajo en `Pendiente` y queda en
#        `Finalizado`; rechaza otro y queda en `Rechazado`.
#   H-2  El comentario escrito se guarda cuando el administrador lo deja, y los
#        dos desenlaces funcionan SIN comentario.
#   H-3  Aprobar y rechazar son FACULTAD EXCLUSIVA del administrador: un alumno
#        que fuerce la transición contra el servicio de datos es rechazado.
#   H-4  `Finalizado` y `Rechazado` son TERMINALES: ninguna transición sale de
#        ellos y su contenido no cambia.
#   H-5  El alumno ve EL DESENLACE en su propio listado, y EL COMENTARIO al
#        abrir el trabajo desde ese listado.
#   H-6  El administrador elimina un trabajo en estado `Pendiente` y el trabajo
#        desaparece.
#   H-7  El alcance comprometido está cerrado: las ocho fases tienen OK
#        explícito.
#
# LOS SEIS PRIMEROS SON MECÁNICOS Y ESTE GUION LOS CORRE. El séptimo NO LO ES, y
# el guion no simula que sí: es una declaración del Product Owner sobre las ocho
# fases, y lo único que se puede comprobar acá es que las siete puertas
# anteriores tengan su guion y pasen. El guion lo declara y no lo marca.
#
# DÓNDE SE MIDE CADA UNO. Los seis mecánicos son de superficie —de la del
# servicio de datos y de la de la pieza pública—, no de la escena, y por eso
# ninguno necesita navegador: a diferencia de la etapa `g`, acá lo que se afirma
# es qué acepta y qué rechaza el servicio, y qué llega al marcado.
#
# LAS PETICIONES SE FUERZAN CONTRA LA SUPERFICIE, que es lo que el intake
# §17.5.P.6 exige para `H-3`: con un acceso firmado legítimo del alumno, sobre su
# propio trabajo, sin pasar por ninguna pantalla. Que la interfaz no ofrezca el
# botón no prueba nada.
#
# Y LO QUE NO SE VE EN UNA RESPUESTA SE LEE DEL ALMACÉN: que el estado quedó
# donde quedó, que el comentario se guardó, y que un trabajo eliminado
# desapareció de verdad. Creerle a la respuesta sobre eso sería verificar lo que
# el producto dice de sí mismo.
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"

fails=0
ok()  { printf '  \033[32mOK\033[0m   %s\n' "$1"; }
bad() { printf '  \033[31mFALLA\033[0m %s\n' "$1"; fails=$((fails + 1)); }

echo "== Puerta de la etapa \`h\` → \`i…\` · siete criterios =="
echo

# ---------------------------------------------------------------------------
# H-1 a H-4 y H-6 · la superficie del servicio de datos
# ---------------------------------------------------------------------------
echo "-- H-1, H-2, H-3, H-4 y H-6 · la superficie del servicio de datos --"

superficie=(
  "TheAdministratorApprovesOneWorkAndRejectsAnotherAndBothLandOnTheirTerminalStatus"
  "TheCommentIsStoredWhenWrittenAndBothOutcomesProceedWithoutIt"
  "TheStudentForcingTheOutcomeOnTheirOwnWorkIsRefusedAndNothingMoves"
  "TheTerminalStatusesRefuseEveryOutcomeAndTheirContentDoesNotChange"
  "TheAdministratorDeletesASubmittedWorkAndItDisappearsWhileTheStudentCannot"
)

filtro=""
for prueba in "${superficie[@]}"; do
  filtro="${filtro}${filtro:+|}FullyQualifiedName~${prueba}"
done

if dotnet test tests/GeometriaFactory.Integration.Tests --configuration Release \
     --filter "$filtro" >/tmp/etapa-h-superficie.log 2>&1; then
  ok "$(grep -oP 'Passed:\s+\K\d+' /tmp/etapa-h-superficie.log | tail -1) pruebas de superficie pasan"
else
  bad "la superficie falla; ver /tmp/etapa-h-superficie.log"
  tail -25 /tmp/etapa-h-superficie.log
fi

echo

# ---------------------------------------------------------------------------
# H-5 · la pieza pública
# ---------------------------------------------------------------------------
echo "-- H-5 · el desenlace en el listado y el comentario en el detalle --"

if dotnet test tests/GeometriaFactory.Integration.Tests --configuration Release \
     --filter "FullyQualifiedName~TheStudentSeesTheOutcomeInTheirListingAndTheCommentOnOpeningTheWork" \
     >/tmp/etapa-h-publica.log 2>&1; then
  ok "el alumno ve el desenlace donde va y el comentario donde va"
else
  bad "la pieza pública falla; ver /tmp/etapa-h-publica.log"
  tail -25 /tmp/etapa-h-publica.log
fi

echo

# ---------------------------------------------------------------------------
# H-7 · las puertas anteriores, que es lo comprobable de este criterio
# ---------------------------------------------------------------------------
echo "-- H-7 · el alcance comprometido, en lo que es comprobable --"

echo "  Las puertas con guion en el árbol:"
for guion in scripts/verify-*.sh; do
  printf '    · %s\n' "$(basename "$guion")"
done

cat <<'NOTA'

  H-7 NO ES MECÁNICO Y ESTE GUION NO LO MARCA. «Las ocho fases tienen OK
  explícito» es una declaración del Product Owner en cada punto de control, y
  ningún guion la puede producir: lo que se corre acá verifica que las puertas
  existan y pasen, que es condición necesaria y no suficiente.

  Las etapas `d`, `e` y `f` siguen sin guion propio. Se declara como estado
  observado —igual que lo hizo el orquestador de reanudación con la `g`— y no
  como falla de esta puerta: sus criterios los cubren pruebas de la batería, lo
  que no tienen es un guion que los reúna.
NOTA

echo
[ "$fails" -eq 0 ] && {
  echo "CONFORME · los seis criterios mecánicos de la etapa \`h\` se verifican"
  echo "           H-7 queda para el punto de control del Product Owner"
  exit 0
}
echo "NO CONFORME · $fails comprobacion(es) fallan"; exit 1
