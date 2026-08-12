# CU-13 — Resetear la contraseña de una cuenta de alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4 (**F-26**, F-03, **F-04** precisada), §4.1 (**RN-12**, **RN-13**, RN-01, RN-07, RN-14, RN-15, **RN-16**), §17.1.P.2 (**INV-09**, INV-05, INV-08), §17.1.P.5, §7 (**CL-7**), §9 (**X-2 retirada**, X-1 vigente), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-7)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Sostener el reseteo de contraseña que el administrador ejerce sobre una **cuenta de alumno**: fijarle una **contraseña provisoria** y dejar la cuenta marcada como con **cambio de contraseña pendiente**, sin tocar nada más. Es el contrato de uso que hace verificable la promesa central de la capacidad F-26: **la cuenta y todos sus trabajos se conservan**, con sus estados y sus comentarios (RN-12).

**Resetear no es dar de baja.** Es la distinción que este caso de uso existe para hacer imposible de confundir. Hasta `PRODUCT-INTAKE` 1.6 el único camino declarado ante una contraseña olvidada era dar de baja la cuenta y volver a darla de alta, y por RN-07 eso eliminaba **todos** los trabajos del alumno: el primer olvido costaba la cursada entera. La capacidad F-26 cierra ese agujero y retira la exclusión X-2; el caso límite CL-7 queda reescrito sobre este camino.

Lo que este caso de uso **no** hace: no cambia el estado de cuenta, no elimina ningún trabajo, no deriva la contraseña provisoria y no levanta la marca que pone. Levantarla es del reemplazo de credencial de [CU-03](CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md), y sólo lo hace la propia cuenta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita el reseteo de la credencial derivada de un alumno ya constituido |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | **Genera** la contraseña provisoria y la deriva antes de que el valor llegue al dominio, y materializa el resultado |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza el reseteo, reemplaza la credencial derivada y pone la marca |

El administrador es el **sujeto** de la regla, no el actor: quien invoca la superficie pública de esta biblioteca es el código consumidor. El dominio **no maneja secretos**: la contraseña provisoria llega ya derivada (PRODUCT-INTAKE §17.1.P.5), de modo que **el dominio no conoce el valor que el administrador le va a comunicar al alumno**. Tampoco lo produce: **quien lo produce es el sistema y no el administrador**, por decisión del Product Owner, y la exigencia sobre ese valor vive en la capa que lo produce (§10).

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- **La cuenta sobre la que se opera tiene papel `Alumno`.**
- La credencial derivada de la cuenta ya tiene valor.
- El valor de credencial provisoria que se aporta ya está derivado; el dominio no recibe texto en claro.

## 4. Flujo principal

1. La capa de aplicación solicita resetear la credencial derivada de un alumno, aportando el valor ya derivado de la contraseña provisoria.
2. El dominio comprueba que el papel de la cuenta sea `Alumno`.
3. El dominio comprueba que la credencial derivada tenga valor.
4. El dominio comprueba que el valor aportado no esté vacío.
5. El dominio comprueba que la solicitud no declare ningún efecto sobre los trabajos del alumno ni sobre su estado de cuenta.
6. El dominio reemplaza la credencial derivada con el valor aportado.
7. El dominio pone la marca de cambio de contraseña pendiente.
8. El dominio devuelve el alumno, con su estado de cuenta, su papel, su identidad y su conjunto de trabajos **sin ningún cambio**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se resetea la contraseña de una cuenta que ya tiene la marca puesta | El dominio admite la operación: reemplaza la contraseña provisoria por la nueva y la marca sigue puesta. Es el caso del alumno que también olvida la provisoria, y no hay ningún motivo declarado para rechazarlo | Paso 8, con la marca sin cambio |
| FA-02 | Se resetea la contraseña de una cuenta `Bloqueado` o `Pendiente` | El dominio admite la operación: el reseteo **no** es una transición de la máquina de estados de cuenta y no exige que la cuenta esté `Habilitado`. La cuenta sigue sin obtener acceso, pero por INV-06 y no por este caso de uso | Paso 8, con el estado de cuenta sin cambio |
| FA-03 | Se resetea la contraseña de una cuenta `Pendiente`, que nunca fue habilitada y **nunca tuvo credencial** | El dominio admite la operación: **fija** la credencial derivada provisoria en lugar de reemplazarla, y pone la marca. Es el mismo acto, con la misma postcondición. Hasta la versión 1.2 este flujo se rechazaba, porque el camino declarado para una cuenta sin credencial era el primer ingreso anónimo; **RN-16 suprimió ese camino** y con él el motivo del rechazo | Paso 8, con fijación en lugar de reemplazo |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se solicita el reseteo sobre la cuenta con papel `Administrador` | Rechaza la operación y conserva la cuenta sin modificar. **Es el mismo código con el que CU-02 cierra sus cuatro operaciones**, y se reutiliza porque la causa es la misma: es una operación del administrador declarada sobre cuentas de alumno, y sobre la única cuenta de administrador no tiene quién la ejerza. El cambio de su propia contraseña entra por el reemplazo de CU-03, FA-01 |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | La solicitud declara que el reseteo elimina los trabajos del alumno, o que cambia su estado de cuenta | Rechaza la operación. **Resetear no es dar de baja y no dispara RN-07**: la cuenta y todos sus trabajos se conservan (RN-12) |
| `VALOR_DERIVADO_VACIO` | El valor de credencial provisoria aportado está vacío | Rechaza la operación y conserva la credencial derivada anterior. Entrada declarada primero en CU-03 §6, con la misma causa |

Los **tres** rechazos dejan al alumno exactamente como estaba: con su credencial anterior, su marca anterior, su estado de cuenta y todos sus trabajos.

## 7. Postcondiciones

**Código retirado en la versión 1.3.** `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` queda **retirado** y no figura entre las condiciones vivas de este contrato. Existía por una decisión derivada que **RN-16** dejó sin fundamento: rechazaba el reseteo sobre una cuenta sin credencial porque la marca sólo la levanta el reemplazo y la cuenta habría quedado marcada sin camino. Con RN-16 ese camino existe y es el mismo que el del reseteo, de modo que la única cuenta sin credencial que queda —la `Pendiente` nunca habilitada— se resetea sin problema por FA-03. Retirarlo cierra además la tensión que el código tenía con **RN-15**, que declara que el reseteo procede sobre `Pendiente`. **El identificador retirado no se recicla.**

- **Éxito:** el alumno tiene la credencial derivada provisoria, la marca de cambio de contraseña pendiente puesta y **ningún otro atributo cambiado**. Su estado de cuenta, su papel, su identidad, su correo y su conjunto de trabajos —con sus estados, sus observaciones y sus comentarios— son los mismos que antes de la operación.
- **Fallo:** no hay efecto parcial. En particular, no existe ningún camino por el que la credencial se reemplace y la marca no se ponga, ni al revés: los dos son un solo acto.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado`, credencial derivada con valor y 3 trabajos —uno en `Borrador`, uno en `Rechazado` con comentario y uno en `Finalizado`— | La capa de aplicación solicita el reseteo con un valor derivado no vacío | El dominio devuelve el alumno con la credencial provisoria, la marca puesta, cuenta `Habilitado` y **los 3 trabajos con sus estados y sus comentarios**, con 0 eliminaciones |
| CA-02 | El mismo alumno, ya reseteado y con la marca puesta | La capa de aplicación consulta su admisibilidad | El dominio devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` (CU-04) |
| CA-03 | El mismo alumno, ya reseteado | La propia cuenta reemplaza su credencial declarando verificada la provisoria (CU-03 FA-04) | La marca queda levantada y la cuenta vuelve a ser admisible, con sus 3 trabajos intactos |
| CA-04 | La cuenta con papel `Administrador` | La capa de aplicación solicita resetear su contraseña | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta sigue sin modificar |
| CA-05 | Un alumno con cuenta `Pendiente`, nunca habilitado y con credencial derivada **sin valor** | La capa de aplicación solicita el reseteo | El dominio **admite** la operación: fija la credencial provisoria, pone la marca y devuelve la cuenta en `Pendiente`, **sin cambio de estado**. **0 rechazos** se producen por la ausencia de credencial previa |
| CA-06 | Un alumno con cuenta `Bloqueado`, credencial con valor y 2 trabajos | La capa de aplicación solicita el reseteo declarando que los trabajos se eliminan | El dominio rechaza con el código `RESETEO_CON_ARRASTRE_DE_TRABAJOS` y los 2 trabajos siguen existiendo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 en su criterio de gobierno de las cuentas desde un solo lugar, NB-02 en su criterio de identidad propia sin canal de correo |
| Reglas de negocio aplicables | [RN-12](../Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md), que es la que este caso de uso materializa; [RN-13](../Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), por la marca que pone; [RN-15](../Reglas-De-Negocio/RN-15-Reseteo-Independiente-Del-Estado-De-Cuenta.md), por los tres estados sobre los que procede; [RN-16](../Reglas-De-Negocio/RN-16-Habilitar-Produce-La-Provisoria.md), que **retira** el rechazo por credencial no fijada y hace de este caso de uso y de la habilitación de CU-02 dos usos del mismo mecanismo; [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), por el cierre sobre la cuenta de administrador; y [RN-07](../Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) **por contraste**: es la regla que este caso de uso **no** dispara |
| Invariantes | INV-09, por la marca; INV-08, por el cierre sobre la cuenta de administrador |
| Historias de usuario a generar en 06 | US-26, resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos |
| Componentes esperados en 05 | Operación de reseteo sobre la entidad de alumno, con el atributo de marca de cambio de contraseña pendiente |
| Tests previstos en 08 | Pruebas unitarias del reseteo con trabajos en los cuatro estados verificando que ninguno se pierde, del segundo reseteo sobre una cuenta ya marcada, del reseteo sobre una cuenta `Pendiente` **sin credencial previa**, que procede (CA-05), y de los **tres** rechazos, sin dobles y sin infraestructura |

## 10. Notas y supuestos

- **El dominio no genera la contraseña provisoria y no la conoce.** **La produce el sistema** —no la escribe el administrador, por decisión del Product Owner: si la escribiera, terminaría siendo la misma clave para toda la comisión—, el administrador la comunica por fuera del producto y al dominio llega ya derivada (PRODUCT-INTAKE §17.1.P.5). Que no sea adivinable y que no se repita entre cuentas son **exigencias sobre el valor generado**, y se declaran donde el valor nace: `GeometriaFactory-Application` CU-11 y su puerto, y `GeometriaFactory-Contracts` CU-08 sobre lo que el resultado devuelve. **Acá no se declaran, porque acá el valor ya llegó derivado y el dominio no puede verificarlas.** No hay canal de correo: la exclusión **X-1** sigue vigente y lo que se retiró es **X-2**, la recuperación de contraseña olvidada, que ahora tiene este camino.
- **La provisoria es provisoria porque existe INV-09.** Sin la marca, una clave que el administrador conoce quedaría sirviendo indefinidamente para operar como el alumno. Es el fundamento que el intake declara al enunciar el invariante.
- **La decisión derivada «el reseteo exige credencial ya fijada» quedó retirada, y conviene registrar por qué.** Esta categoría la había adoptado por consistencia del modelo: la marca la levanta únicamente el reemplazo (RN-13), y sobre una credencial sin valor el único camino era la fijación anónima de CU-03, con lo cual una cuenta marcada y sin credencial habría quedado sin salida. **`PRODUCT-INTAKE` 1.13 §4.1 RN-16 disolvió la premisa**: la fijación dejó de ser un acto anónimo del alumno y pasó a ser un efecto de la habilitación, y el cambio de la provisoria es el mismo reemplazo en los dos casos. Sin premisa, el rechazo era sólo un rechazo de más —y además chocaba con **RN-15**, que declara que el reseteo procede sobre `Pendiente`—. Lo que ahora ocurre sobre una cuenta sin credencial es lo que aquella nota anticipaba como salida limpia: el reseteo **fija** en lugar de reemplazar, y sí pone la marca, porque el camino para levantarla ya existe.
- **Este caso de uso y la habilitación de CU-02 son dos usos del mismo mecanismo.** Desde **RN-16**, habilitar produce una provisoria y pone la marca, exactamente como el reseteo. Lo que los distingue no es el efecto sobre la credencial sino el resto del acto: la habilitación **es** una transición de la máquina de estados de cuenta y el reseteo no lo es (RN-15). Siguen siendo dos casos de uso por ese motivo, y no porque el tratamiento de la provisoria difiera: no difiere.
- **El reseteo no exige estado `Habilitado`, y ya no es una decisión derivada: el Product Owner la ratificó.** Ninguna fuente lo condicionaba al estado y condicionarlo habría sido inventar una precondición; el Product Owner resolvió expresamente que el reseteo es una operación sobre la credencial que **no toca el estado de la cuenta**, de modo que se puede resetear la contraseña de una cuenta `Pendiente` o `Bloqueado` y el administrador puede resetear la contraseña y habilitar **en el orden que quiera**, sin acordarse de una secuencia. Resetear la contraseña de una cuenta `Bloqueado` es inocuo: la cuenta sigue sin obtener acceso por INV-06. Lo que **no** cambia es el cierre sobre la cuenta de administrador de §6, que es de INV-08 y no del estado.
- La confirmación con la que el administrador ejerce esta operación en pantalla, si la hubiera, es de la categoría 03 del proyecto de código de la pieza pública. **Este caso de uso no exige confirmación escrita**, a diferencia de la baja: RN-12 no la pide, y pedirla sería trasladar a una operación conservadora la guarda de una destructiva.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **5** ocurrencias a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial, por la capacidad **F-26** que `PRODUCT-INTAKE` 1.7 incorpora como `Must Have`, con las reglas **RN-12** y **RN-13**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. Declara el reseteo como acto que conserva la cuenta, su estado, su papel, su identidad y todos sus trabajos con sus estados y comentarios; el reemplazo de la credencial derivada y la puesta de la marca como un solo acto sin efecto parcial; los tres flujos alternativos —segundo reseteo, cuenta no habilitada y cuenta sin credencial—; y cuatro condiciones de rechazo, dos de ellas reutilizadas de CU-02 y CU-03 con la misma causa. Deja declaradas dos **decisiones derivadas** que ninguna fuente enuncia: que el reseteo exige credencial ya fijada, y que no exige estado `Habilitado`. |
| 1.1 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26.** **Decisión A: resetear no exige que la cuenta esté habilitada.** Este caso de uso **ya lo declaraba así** —FA-02 y §7—, de modo que **ningún flujo, código de rechazo ni criterio de aceptación cambia**: lo único que cambia es el estatuto de la afirmación. La nota de §10 deja de rotularse **decisión derivada** y pasa a registrar la **ratificación del Product Owner**, con su fundamento —el administrador no tiene que acordarse de una secuencia— y con la precisión de que el cierre sobre la cuenta de administrador de §6 **no** se afloja, porque es de INV-08 y no del estado. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**, porque una provisoria escrita por el docente termina siendo la misma clave para toda la comisión. Acá corrige una afirmación que quedó **falsa**: §10 y §2 decían que la provisoria «la elige el administrador». §2 declara que la genera la infraestructura antes de derivarla, y §10 declara que **no ser adivinable y no repetirse entre cuentas son exigencias de la capa que produce el valor** —`GeometriaFactory-Application` CU-11 y `GeometriaFactory-Contracts` CU-08— y **no de ésta**, porque acá el valor llega ya derivado y el dominio no puede verificarlas. Sube minor: corrige una afirmación de contexto y ratifica otra, sin tocar la superficie del caso de uso. |
| 1.2 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-7, que declara la dependencia del administrador, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún flujo, código de rechazo, postcondición ni criterio de aceptación de este caso de uso cambia**: es precisamente esta capacidad la que dejó sin objeto al riesgo. Sube minor: corrige una referencia a una fila retirada. |
| 1.3 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16).** Habilitar produce una contraseña provisoria con el mismo mecanismo y el mismo tratamiento que este caso de uso, de modo que la fijación deja de ser un acto anónimo del alumno y **la premisa de la que colgaba el rechazo por credencial no fijada desaparece**. **§5**: **FA-03** se invierte —el reseteo sobre una cuenta sin credencial **procede** y fija en lugar de reemplazar—. **§6**: se **retira** `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA`, los rechazos pasan de cuatro a **tres**, y la fila de retiro declara el fundamento y la tensión con **RN-15** que el retiro cierra de paso; el identificador **no se recicla**. **§8**: **CA-05** se rehace sobre una cuenta `Pendiente` nunca habilitada, que ahora se resetea sin rechazo. **§9**: suma RN-15 y RN-16 a las reglas aplicables y ajusta las pruebas previstas. **§10**: la nota de la decisión derivada pasa a registrar su **retiro** con el motivo, y entra la nota que declara que este caso de uso y la habilitación de CU-02 son dos usos del mismo mecanismo y qué los sigue distinguiendo. **§17** suma la cláusula que impide reponer el rechazo. Sube minor. |

## 17. Compatibilidad de la superficie pública

- Quitar la puesta de la marca del efecto de esta operación es un cambio **incompatible** y además rompe INV-09: la contraseña provisoria dejaría de ser provisoria.
- Admitir que esta operación cambie el estado de cuenta, o que alcance a los trabajos del alumno, es un cambio de alcance que contradice RN-12 y sube la versión mayor de este caso de uso.
- Agregar una condición de rechazo al conjunto de §6 obliga a revisar a los consumidores que las traducen y sube la versión mayor, con el mismo criterio que CU-04 aplica a su conjunto de motivos.
- **Reponer un rechazo por credencial no fijada se rechaza aunque compile**: contradice FA-03, CA-05 y **RN-15**, que declara que el reseteo procede sobre `Pendiente`.
