#!/usr/bin/env bash
# ============================================================================
# verify-stage-f.sh — Puerta de los OCHO criterios de transición de la etapa
# `f` (`Roadmap-Producto.md` §5.2, `f` → `g`): la interpretación del texto real
# del alumno, con su acción única de envío.
#
#   F-1  Los casos de prueba obligatorios pasan con los escenarios de datos del
#        intake como entrada. SOBRE EL RECUENTO, ver la nota de abajo.
#   F-2  El texto TAL COMO LO EMITE el programa del alumno se interpreta, con sus
#        particularidades de formato incluidas.
#   F-3  ENVIAR es la única acción de guardado: un envío que verifica pasa el
#        trabajo a `Pendiente`, y uno que no verifica lo deja en `Borrador` con
#        sus errores localizados.
#   F-4  Un cubo del primer ejemplo produce advertencia de área con los dos
#        valores expresados y pasa a `Pendiente` igual; el mismo cubo del segundo
#        ejemplo NO produce ninguna advertencia.
#   F-5  Un tipo desconocido produce error CON ÍNDICE DE FIGURA Y CAMPO, y el
#        trabajo no pasa a `Pendiente`.
#   F-6  La comparación de valores usa TOLERANCIA ABSOLUTA y no igualdad exacta.
#   F-7  El texto original se conserva ÍNTEGRO y nunca se reescribe.
#   F-8  `PT-02` y `PT-03` medidas antes de comprometer `g`.
#
# UNA DISCREPANCIA DE RECUENTO, DECLARADA Y NO RESUELTA ACÁ. El criterio `F-1`
# del roadmap dice «los **nueve** casos de prueba obligatorios». El intake
# **1.20** §17.1.P.8 escribe «las **diez** pruebas del validador pasan», y
# `Criterios-Validacion.md` `CV-26` dice «los **diez** casos de la batería».
# El roadmap quedó con el recuento anterior a la aposición del décimo.
#
# ESTE GUION NO ELIGE UN NÚMERO. Corre la batería entera —que hoy son catorce
# métodos, porque algunos casos se ejercen en más de una forma— y **declara la
# discrepancia** para que la corrija un acto propio, con el mismo criterio con el
# que se cerraron las magnitudes de `Vista-Producto`. Un guion que afirmara
# «nueve pasan» estaría eligiendo cuál de las dos fuentes tiene razón.
#
# `F-8` NO SE REEJECUTA ACÁ: es `PT-02`, que tiene su propia medición con
# navegador, y `PT-03`, que se comprueba por inspección del paquete. La puerta
# los invoca en lugar de reescribirlos.
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"
source scripts/lib-puerta.sh

puerta_abre "f" "f → g" "la interpretación del texto real del alumno"

criterio "F-1  la batería obligatoria, con los escenarios del intake" \
  E1ProducesThreePiecesAndExactlyTwoWarnings \
  E2IsReadDespiteTrailingCommasAndReadsBasesFromTapas \
  E3ReadsSquareFacesAndWarnsOnlyAboutTheArea \
  E4ReadsRectangleFacesAndProducesNoObservationAtAll \
  E5ReportsTheUnknownTypeAtPositionOneAndKeepsTheValidPiece \
  E6InterpretsTheFigureWithALengthOfZeroAndDoesNotDiscardIt \
  E7ReconstructsTheSixDrawableTypesWithThePlainOnesAsRootPieces \
  E8ReportsTheUnreadableDimensionAtPositionOneAndKeepsTheOrthohedron \
  AnUnreadableTextIsAResultAndNotABreakdown \
  AnEmptyRootSetProducesAnObservationWithoutPiecePosition

criterio "F-2  el texto tal como lo emite el programa, con su formato" \
  E2IsReadDespiteTrailingCommasAndReadsBasesFromTapas \
  E2WarnsAboutTheVolumeAndNotAboutTheArea

criterio "F-3  enviar es la única acción de guardado" \
  AWorkWhoseTextVerifiesGoesToSubmittedWithItsWarnings \
  AWorkWhoseTextDoesNotVerifyStaysInDraftWithItsErrorLocated \
  InterpretingReturnsThePiecesToDrawAndWritesNothing \
  InterpretingATextThatDoesNotVerifyReturnsWhatItCouldRebuild \
  InterpretingWithoutTextIsRejectedNamingTheField

criterio "F-4  el cubo del primer ejemplo advierte y el del segundo no" \
  E3ReadsSquareFacesAndWarnsOnlyAboutTheArea \
  E4ReadsRectangleFacesAndProducesNoObservationAtAll

criterio "F-5  el tipo desconocido, con índice de figura y campo" \
  E5ReportsTheUnknownTypeAtPositionOneAndKeepsTheValidPiece \
  E8ReportsTheUnreadableDimensionAtPositionOneAndKeepsTheOrthohedron

criterio "F-6  tolerancia absoluta y no igualdad exacta" \
  TheStrictOperatorAnchorsTheToleranceInATestAndNotOnlyInProse \
  E1DoesNotWarnAboutTheCylinderBecauseTheDifferenceIsExactlyTheTolerance

criterio "F-7  el texto original, íntegro y nunca reescrito" \
  TheOriginalTextIsNeverModified \
  ForcingAnEditOutsideDraftIsRejectedAndKeepsTheStoredText \
  TheListingProjectionCarriesNoOriginalText

printf '\n-- F-8 · PT-02 y PT-03, que tienen su propia medición --\n'

# NO SE PUDO MEDIR NO ES LO MISMO QUE FALLA, y el guion los distingue. `PT-02` corre
# con navegador en contenedor: si el entorno no tiene `docker`, la puerta se detiene
# declarando que le falta el instrumento, en lugar de reportar que el criterio falla
# —que sería inventar un defecto del producto— o de saltearlo en silencio —que sería
# dar por verificado lo que nadie midió—.
if ! command -v docker >/dev/null 2>&1; then
  printf '  \033[33mSIN MEDIR\033[0m F-8  este entorno no tiene `docker`, y `PT-02` lo necesita\n'
  printf '        Corré esta puerta donde `dotnet` y `docker` convivan, que es lo que el\n'
  printf '        `devcontainer` declarado provee. NO es una falla del criterio: es que\n'
  printf '        acá no hay con qué medirlo.\n\n'
  printf 'INCOMPLETA · los siete criterios de batería pasan; `F-8` quedó sin medir\n'
  exit 2
fi

if bash scripts/verify-viewer-lifecycle.sh >/tmp/puerta-f-pt02.log 2>&1; then
  printf '  \033[32mOK\033[0m   F-8  PT-02 pasa con sus once controles \033[2m(verify-viewer-lifecycle.sh)\033[0m\n'
  printf '        PT-03 se comprueba por inspección del paquete; su medición está en\n'
  printf '        SDD/Docs/Producto/Medicion-Puertas-Tecnicas-PT-02-PT-03.md\n'
else
  printf '  \033[31mFALLA\033[0m F-8  PT-02 falla; ver /tmp/puerta-f-pt02.log\n'
  tail -15 /tmp/puerta-f-pt02.log
  exit 1
fi

cat <<'NOTA'

  DISCREPANCIA DECLARADA EN `F-1`. El roadmap §5.2 dice «los NUEVE casos de
  prueba obligatorios»; el intake 1.20 §17.1.P.8 y `Criterios-Validacion.md`
  CV-26 dicen DIEZ. El roadmap quedó con el recuento anterior a la aposición del
  décimo caso. Este guion corre la batería entera y NO elige un número: la
  corrección del roadmap es un acto propio.
NOTA

puerta_cierra "f" "los ocho criterios"
