#!/usr/bin/env bash
# ============================================================================
# verify-stage-e.sh — Puerta de los CINCO criterios de transición de la etapa
# `e` (`Roadmap-Producto.md` §5.2, `e` → `f`): el trabajo con dueño, estado e
# identificador, y el listado del administrador.
#
#   E-1  Un trabajo se carga con nombre, fecha, descripción y texto, y recibe
#        IDENTIFICADOR PROPIO Y ESTADO.
#   E-2  Un trabajo queda en `Borrador` CON EL TEXTO INVÁLIDO y se reedita.
#   E-3  La eliminación por el alumno sólo procede en `Borrador` y sólo sobre
#        trabajos propios, verificado FORZANDO LA PETICIÓN al servicio de datos,
#        no sólo por la interfaz.
#   E-4  Un alumno que pide el trabajo de otro recibe «no encontrado».
#   E-5  El administrador ve los trabajos agrupados y filtrados por alumno, y su
#        listado NO INCLUYE los que están en estado `Borrador`.
#
# EL CRITERIO E-3 ES EL QUE DEFINE ESTA PUERTA, y por eso corre cuatro pruebas:
# la que elimina lo que corresponde, las dos que fuerzan la petición contra la
# superficie —fuera de `Borrador` y sobre el trabajo de otro— y la que la fuerza
# pasando por alto la pantalla. El intake §17.5.P.6 lo exige así: que la interfaz
# no ofrezca el botón NO PRUEBA NADA.
#
# Ver `lib-puerta.sh` sobre por qué las tres puertas nuevas comparten forma y las
# otras tres no.
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"
source scripts/lib-puerta.sh

puerta_abre "e" "e → f" "el trabajo con dueño, estado e identificador"

criterio "E-1  el trabajo se carga y recibe identificador y estado" \
  AWorkIsLoadedAndGetsItsOwnIdentifierAndStatus \
  TheStudentLoadsAWorkAndSeesItInTheirListingWithItsIdentifierAndItsStatus \
  AWorkWithoutANameIsRejectedNamingTheField \
  TheAdministratorCannotLoadAWork

criterio "E-2  queda en borrador con el texto inválido y se reedita" \
  AnInvalidTextLeavesTheWorkInDraftAndItIsEdited \
  TheStudentReeditsADraftAndTheChangeIsVisible \
  ForcingAnEditOutsideDraftIsRejectedAndKeepsTheStoredText

criterio "E-3  la eliminación, con la petición forzada contra el servicio" \
  TheStudentDeletesTheirOwnDraft \
  ForcingTheDeletionOutsideDraftIsRejectedAndTheWorkSurvives \
  ForcingTheDeletionOfAnotherStudentsWorkAnswersLikeAMissingOne \
  TheDeletionIsStillRefusedByTheServiceWhenTheRequestIsForcedPastTheScreen \
  TheStudentCannotDeleteAWorkOutsideDraftAndTheProtectionHoldsWhenTheRequestIsForced \
  TheAdministratorCannotDeleteADraft \
  TheAdministratorDeletesTheThreeStatusesTheySee

criterio "E-4  el trabajo de otro responde «no encontrado»" \
  AStudentAskingForAnotherStudentsWorkGetsNotFound \
  TheStudentAskingForSomeoneElsesWorkGetsNotFoundAndTheScreenSaysNothingElse \
  TheAdministratorOpeningADraftGetsNotFound \
  TheOwnerSeesTheirWorkWithTheWholeText

criterio "E-5  el listado del administrador, agrupado, filtrado y sin borradores" \
  TheAdministratorListingIsGroupedByStudentAndExcludesDrafts \
  TheStudentFilterNarrowsWithinTheAdministratorScope \
  TheAdministratorListingDoesNotBringTheDrafts \
  AFilterOnAMissingStudentAnswersNotFound \
  TheStudentListingCoversTheirFourStatusesAndIgnoresTheFilter \
  TheListingProjectionCarriesNoOriginalText

puerta_cierra "e" "los cinco criterios"
