#!/usr/bin/env bash
# ============================================================================
# verify-stage-d.sh — Puerta de los DIEZ criterios de transición de la etapa
# `d` (`Roadmap-Producto.md` §5.2, `d` → `e`): el ciclo de vida de la cuenta de
# alumno, sin correo de por medio, y el reseteo de credencial.
#
#   D-1   Un alumno se registra con correo, nombre y apellido, SIN elegir
#         contraseña.
#   D-2   Una cuenta en `Pendiente` recibe un aviso explícito de que todavía no
#         fue habilitada.
#   D-3   El administrador habilita, bloquea, rehabilita y da de baja, y la baja
#         EXIGE CONFIRMACIÓN escribiendo el correo de la cuenta.
#   D-4   Al habilitar, el producto muestra una PROVISORIA QUE EL ADMINISTRADOR
#         NO ESCRIBIÓ, y el alumno queda obligado a cambiarla.
#   D-5   NINGÚN punto de acceso acepta un correo y una contraseña nueva sin
#         credencial (RN-02016).
#   D-6   El administrador RESETEA desde el mismo panel, que no tiene campo de
#         contraseña.
#   D-7   Dos reseteos consecutivos producen provisorias DISTINTAS, y ninguna es
#         derivable del nombre, del correo ni de la fecha.
#   D-8   El reseteo procede sobre `Bloqueado` y sobre `Pendiente` SIN cambiarles
#         la situación, y NO procede sobre la cuenta de administrador.
#   D-9   La cuenta reseteada se autentica y NO obtiene sesión de trabajo: toda
#         ruta termina en el cambio, y recién al cambiarla opera con normalidad.
#   D-10  Después del reseteo la cuenta conserva su identidad, su situación y
#         TODOS sus trabajos, verificado sobre tres estados distintos.
#
# POR QUÉ ESTE GUION EXISTE. Las etapas `b`, `c`, `g` y `h` tienen su puerta;
# `d`, `e` y `f` no tenían ninguna, y el orquestador de reanudación lo dejó
# declarado como estado observado. Los criterios YA estaban cubiertos por la
# batería: lo que faltaba era el guion que los reúne y los corre juntos, para
# que cerrar la etapa no dependa de que alguien recuerde cuáles mirar.
#
# NO SE ESCRIBIÓ NINGUNA PRUEBA NUEVA PARA ESTA PUERTA, y es deliberado: si un
# criterio necesitara una prueba que no existe, la etapa no habría estado
# cerrada. Este guion **verifica que lo esté**, no la cierra.
#
# CADA CRITERIO SE CORRE CONTRA LAS PRUEBAS QUE LO CUBREN, nombradas una por
# una. Un filtro por clase habría pasado igual y no diría qué criterio cubre
# qué: la lista es el mapa entre el roadmap y la batería, y es lo que hace
# auditable esta puerta.
# ============================================================================
set -uo pipefail

raiz="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$raiz"
source scripts/lib-puerta.sh

puerta_abre "d" "d → e" "el ciclo de vida de la cuenta de alumno"

criterio "D-1  registro sin elegir contraseña" \
  AStudentRegistersWithEmailNameAndSurnameAndWithoutChoosingAPassword \
  TheStudentRegistersWithoutChoosingAPasswordAndTheAccountIsLeftPending \
  RegisteringAnAlreadyRegisteredEmailAnswersConflictWithoutRevealingTheAccount

criterio "D-2  la cuenta pendiente recibe aviso explícito" \
  ThePendingAccountIsToldItHasNotBeenEnabledYet \
  ThePendingStudentIsToldExplicitlyThatTheAccountIsNotEnabledYet

criterio "D-3  habilitar, bloquear, rehabilitar y dar de baja con correo escrito" \
  TheAdministratorEnablesBlocksReEnablesAndDeletes \
  TheDeletionDoesNotProceedWhenTheWrittenEmailDoesNotMatch \
  TheListingCarriesTheStatusAndTheMarkAndNoFormOfTheCredential

criterio "D-4  la provisoria que el administrador no escribió, y el cambio obligado" \
  EnablingProducesAProvisionalTheAdministratorDidNotWriteAndForcesItsChange \
  EnablingShowsTheProvisionalToTheAdministratorAndTheStudentIsForcedToChangeIt \
  EveryProvisionalIsTwelveCharactersLong \
  TheAlphabetHasNoAmbiguousCharactersAndNoPunctuation

criterio "D-5  ningún punto acepta correo y contraseña nueva sin credencial" \
  NoAccessPointAcceptsAnEmailAndANewPasswordWithoutACredential \
  TheCredentialFormNeverAcceptsANewPasswordWithoutTheCurrentOne \
  TheCredentialFormOnlyServesTheForcedChangeAndStillRequiresTheCurrentPassword

criterio "D-6  el reseteo desde el mismo panel, sin campo de contraseña" \
  TheAdministratorResetsFromTheSamePanelAndTheProvisionalsDiffer \
  TheProductionOperationDeclaresNoParameters

criterio "D-7  dos reseteos, provisorias distintas y no derivables" \
  TwoConsecutiveResetsProduceDistinctProvisionalsDerivableFromNothing \
  NoProvisionalDerivesFromAccountDataNorFromTheClock \
  AThousandProvisionalsHaveNoRepetition

criterio "D-8  procede sobre bloqueado y pendiente, no sobre el administrador" \
  ResettingProceedsOverBlockedAndPendingWithoutChangingTheStatus \
  TheSurfaceRefusesEveryOperationOverTheAdministratorAccount \
  ThePanelOffersNoOperationOnTheAdministratorAccount

criterio "D-9  se autentica y no obtiene sesión de trabajo hasta cambiarla" \
  TheStudentWhosePasswordWasResetReachesTheChangeAndOnlyThenGetsAWorkingSession \
  WhileTheMarkIsSetNoPathHandsOutAWorkingSession \
  TheResetStudentWalksTheWholeWayThroughTheRedirectAndOnlyThenGetsASession \
  TheMarkIsReadFromTheStoreAndOnlyTheEffectiveChangeLiftsIt \
  WithTheMarkSetNoGuardedPointAnswersEvenWithAnAccessObtainedBeforeTheReset

criterio "D-10 conserva identidad, situación y todos sus trabajos" \
  TheResetKeepsTheIdentityAndTheStatusAndDeclaresNoWayToLoseAWork \
  AccountResetKeepsAllTheWorksInTheirThreeStatuses

puerta_cierra "d" "los diez criterios"
