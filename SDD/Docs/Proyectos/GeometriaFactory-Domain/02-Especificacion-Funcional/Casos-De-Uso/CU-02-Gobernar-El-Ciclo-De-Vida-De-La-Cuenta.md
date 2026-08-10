# CU-02 — Gobernar el ciclo de vida de la cuenta del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md
**Versión:** 1.6
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4 (F-03, **F-04** precisada, **F-26**), §4.1 (RN-01, RN-07, **RN-12**, **RN-13**, **RN-14** y **RN-16**), §17.1.P.2 (INV-05, INV-08, **INV-09**), §17.1.P.5, §7 (CL-6, **CL-7** reescrito), §9 (X-3, **X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-6)
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

Sostener las cuatro operaciones que el administrador ejerce sobre una **cuenta de alumno** —habilitar, bloquear, rehabilitar y dar de baja— como transiciones verificables del dominio, admitiendo únicamente las que la máquina de estados declara. Las cuatro forman un solo contrato de uso porque son el mismo acto de admisión visto en cuatro momentos de la vida de la cuenta (`NB-01` §5, criterio de cobertura de las cuatro operaciones).

**Las cuatro alcanzan sólo a las cuentas con papel `Alumno`**, y no es una restricción de este documento: es el enunciado literal de la capacidad, «F-03 · Habilitar, bloquear, rehabilitar y dar de baja física cuentas **de alumno** desde el panel del administrador» (PRODUCT-INTAKE §4). Sobre la cuenta de administrador no procede ninguna de las cuatro.

**Habilitar produce la contraseña provisoria de la cuenta, y esto sí es de este contrato.** Desde `PRODUCT-INTAKE` 1.13, **RN-16** declara que habilitar una cuenta produce una contraseña provisoria con el mismo mecanismo y el mismo tratamiento que la del reseteo, y deja la cuenta con **cambio de contraseña pendiente** (INV-09). La habilitación deja entonces de ser una transición pura de la máquina de estados: **exige la credencial derivada provisoria** y pone la marca. La consecuencia estructural es que **ninguna cuenta de alumno llega a `Habilitado` sin credencial**, y es lo que suprime del producto la única escritura que ocurría sin credencial —el primer ingreso anónimo—.

**El reseteo de contraseña no es una quinta operación de este contrato.** Desde `PRODUCT-INTAKE` 1.7, el administrador también resetea la contraseña de un alumno (F-26), y ese acto vive en [CU-13](CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) y no acá, por tres motivos: no cambia el estado de cuenta, **no dispara RN-07** y su efecto propio es poner una marca que ninguna de estas cuatro operaciones toca. La distinción no es formal: es la que separa la operación que **elimina** todos los trabajos del alumno de la que los **conserva**.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita la transición de estado o la baja sobre un alumno ya constituido |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa fuera del dominio el resultado de la transición |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza la transición según la máquina de estados |

El administrador es el **sujeto** de la regla, no el actor del caso de uso: quien invoca la superficie pública de esta biblioteca es el código consumidor.

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- **La cuenta sobre la que se opera tiene papel `Alumno`** (F-03).
- La operación solicitada pertenece al conjunto habilitar, bloquear, rehabilitar, dar de baja.
- **Para habilitar y para rehabilitar, el consumidor aporta la credencial derivada provisoria** que la capa de infraestructura produjo (RN-14, RN-16). El dominio no la produce y nunca la conoce en claro.
- Para la baja, el consumidor ya obtuvo del administrador la confirmación escrita del correo de la cuenta (RN-07). Esa comprobación es del consumidor: el dominio expresa la exigencia, no la interfaz que la recoge.

## 4. Flujo principal

1. La capa de aplicación solicita al alumno una transición de estado de cuenta.
2. El dominio comprueba que el papel de la cuenta sea `Alumno` y lee su estado actual.
3. El dominio comprueba que el par estado actual y transición solicitada figure en la tabla de transiciones admitidas.
4. Si la transición solicitada es **habilitar** o **rehabilitar**, el dominio comprueba que se haya aportado la credencial derivada provisoria, la fija por el camino de fijación de CU-03 y pone la **marca de cambio de contraseña pendiente** (RN-16, INV-09).
5. El dominio aplica la transición y deja el nuevo estado como estado actual.
6. El dominio devuelve el alumno con su nuevo estado y, cuando la transición fue habilitar o rehabilitar, con la marca puesta.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La operación solicitada es la baja de la cuenta | El dominio exige que la baja arrastre los trabajos del alumno **en cualquier estado en que estén**, incluidos los terminales: la cuenta y sus trabajos desaparecen juntos, y no se admite una baja que deje trabajos huérfanos (RN-07). El dominio expresa esa condición como parte de la operación; la eliminación efectiva del dato la ejecuta la infraestructura | Termina el caso de uso: no hay estado posterior porque la cuenta deja de existir |
| FA-02 | Se solicita habilitar una cuenta que ya está `Habilitado` | El dominio trata la operación como sin efecto y devuelve el alumno sin cambio de estado, en lugar de rechazarla: la operación es idempotente respecto del estado. **Tampoco fija credencial ni pone marca**: no hay transición, y producir una provisoria nueva sin que la haya pedido nadie dejaría al alumno fuera de su propia cuenta. Para eso está el reseteo de CU-13, que es explícito | Paso 6 |
| FA-03 | Se solicita bloquear una cuenta `Pendiente` | Transición no declarada por las fuentes. El dominio la rechaza y no la infiere | Paso 3, con el rechazo de §6 |
| FA-04 | Se solicita habilitar o rehabilitar una cuenta que **ya tiene la marca de cambio de contraseña pendiente** puesta, por un reseteo anterior | El dominio fija la credencial provisoria nueva y **deja la marca puesta**: no hay estado intermedio que distinguir y la marca no se acumula. Es el mismo tratamiento que FA-01 de CU-13 le da al segundo reseteo | Paso 5 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | El par estado actual y transición solicitada no figura en la tabla de transiciones | Rechaza la operación y conserva el estado actual sin modificar |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Se solicita la baja declarando que los trabajos del alumno se conservan | Rechaza la operación: la baja arrastra los trabajos, y esa consecuencia está aceptada por escrito aguas arriba |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | Se solicita habilitar o rehabilitar una cuenta de alumno **sin aportar la credencial derivada provisoria** | Rechaza la operación y conserva el estado actual sin modificar. Admitirla dejaría la cuenta en `Habilitado` sin nada con que autenticarse, y el único camino para darle una credencial sería reponer el punto anónimo que **RN-16** suprime |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se solicita **cualquiera de las cuatro operaciones** —habilitar, bloquear, rehabilitar o dar de baja— sobre la cuenta con papel `Administrador` | Rechaza la operación y conserva la cuenta sin modificar. Las cuatro están declaradas sobre cuentas de alumno (F-03), y sobre la única cuenta de administrador ninguna tiene inversa posible: la instancia quedaría sin nadie capaz de habilitar, desbloquear y revisar (INV-05, RN-01) |

Los **cuatro** rechazos son terminaciones controladas: la cuenta queda exactamente como estaba antes de la solicitud.

**Identificador retirado en la versión 1.2.** El código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` queda **retirado** y no figura entre las condiciones vivas de este contrato: cubría una sola de las cuatro operaciones y dejaba las otras tres sin guarda, que es como se llegó al hallazgo H-01 de la ronda r3. Toda cita anterior resuelve a `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, que cubre las cuatro. **El identificador retirado no se recicla para ninguna otra condición**, para que una referencia vieja no resuelva en silencio a un código distinto del que nombraba.

## 7. Postcondiciones

- **Éxito de una habilitación o de una rehabilitación:** el alumno tiene estado `Habilitado`, **credencial derivada con valor** y la **marca de cambio de contraseña pendiente** puesta. Ningún otro atributo cambió, y en particular ninguno de sus trabajos.
- **Éxito de un bloqueo:** el alumno tiene el nuevo estado y ningún otro atributo cambió; la credencial y la marca quedan como estaban.
- **Éxito de una baja:** la operación queda declarada como baja con arrastre de los trabajos del alumno, para que el consumidor la materialice como una sola unidad.
- **Fallo:** el estado de cuenta se conserva sin cambios y no hay efecto parcial.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita habilitarlo aportando la credencial derivada provisoria | El dominio devuelve el alumno con cuenta `Habilitado`, **credencial derivada con valor** y la **marca de cambio de contraseña pendiente** puesta |
| CA-08 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita habilitarlo **sin aportar** la credencial derivada provisoria | El dominio rechaza con el código `HABILITACION_SIN_CREDENCIAL_PROVISORIA`, la cuenta sigue en `Pendiente` y **0 cuentas** quedan `Habilitado` sin credencial |
| CA-09 | Un alumno con cuenta `Bloqueado`, con credencial derivada y sin marca | La capa de aplicación solicita rehabilitarlo aportando una credencial derivada provisoria nueva | El dominio devuelve la cuenta `Habilitado` con la marca puesta, y la credencial derivada **no es la anterior**: la rehabilitación es una habilitación a los efectos de RN-16 |
| CA-02 | Un alumno con cuenta `Habilitado` | La capa de aplicación solicita bloquearlo y luego rehabilitarlo, aportando en la rehabilitación la credencial derivada provisoria | El dominio devuelve el alumno con cuenta `Bloqueado` y después `Habilitado`, y admite las 4 operaciones declaradas |
| CA-03 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita bloquearlo | El dominio rechaza con el código `TRANSICION_DE_CUENTA_NO_ADMITIDA` y la cuenta sigue en `Pendiente` |
| CA-04 | Un alumno con cuenta `Bloqueado` y 3 trabajos, uno de ellos en estado `Finalizado` | La capa de aplicación solicita darlo de baja conservando los trabajos | El dominio rechaza con el código `BAJA_SIN_ARRASTRE_DE_TRABAJOS` |
| CA-05 | La cuenta con papel `Administrador` | La capa de aplicación solicita darla de baja | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` |
| CA-06 | La cuenta con papel `Administrador`, en estado `Habilitado` | La capa de aplicación solicita **bloquearla** | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta sigue en `Habilitado`: bloquearla dejaría a la instancia sin ninguna cuenta capaz de desbloquearla |
| CA-07 | La cuenta con papel `Administrador` | La capa de aplicación solicita habilitarla y, por separado, rehabilitarla | El dominio rechaza las 2 con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`: las 4 operaciones quedan cerradas sobre esa cuenta |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 |
| Reglas de negocio aplicables | [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-07](../Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [RN-06](../Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) —este caso de uso es donde el estado de cuenta cambia, y de ese estado depende que la cuenta obtenga o no acceso—, [RN-16](../Reglas-De-Negocio/RN-16-Habilitar-Produce-La-Provisoria.md) —habilitar produce la provisoria y pone la marca, que es lo que CA-01, CA-08 y CA-09 verifican—, [RN-14](../Reglas-De-Negocio/RN-14-Provisoria-Producida-Por-El-Sistema.md) por las propiedades del valor que el dominio recibe ya derivado, y [RN-13](../Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) por el efecto de la marca que este contrato pone |
| Invariantes | INV-05, INV-06, **INV-09** |
| Historias de usuario a generar en 06 | US de habilitación **con provisoria producida y marca puesta**, US de bloqueo y rehabilitación, US de baja con arrastre |
| Componentes esperados en 05 | Máquina de transiciones de estado de cuenta dentro de la entidad de alumno |
| Tests previstos en 08 | Pruebas unitarias de la tabla de transiciones, incluidas las inadmisibles; del rechazo de la baja sin arrastre; y de **RN-16**: habilitación con credencial provisoria que deja la marca (CA-01), habilitación sin credencial que se rechaza (CA-08), rehabilitación que también deja la marca (CA-09) y la prueba negativa de que ningún camino de este contrato lleva una cuenta de alumno a `Habilitado` sin credencial derivada |

## 10. Notas y supuestos

- La confirmación escrita del correo antes de la baja es una exigencia de negocio que el dominio **declara** y que la interfaz del producto **recoge**; el detalle de esa interacción pertenece a la categoría 03 del proyecto de código de la pieza pública, no a esta.
- La baja es física y no un estado: por eso no aparece en la máquina de estados como destino, sino como salida del ciclo de vida.
- La pérdida de los trabajos de la cuenta dada de baja es un riesgo residual declarado y aceptado aguas arriba (`Vision-Producto.md` §8, RG-06). Alcanza también a los trabajos en estado `Finalizado` y `Rechazado`: la terminalidad de esos dos estados impide que cambien de estado o de contenido (INV-07), no que la baja de la cuenta los arrastre.
- La eliminación de **un** trabajo, sea por su dueño o por el administrador, no es esta operación: vive en CU-09 y en CU-11.
- **La baja dejó de ser la salida ante una contraseña olvidada.** Era el único camino declarado hasta `PRODUCT-INTAKE` 1.6, y por RN-07 costaba todos los trabajos del alumno. El intake 1.7 incorpora **F-26**, retira la exclusión **X-2** y reescribe **CL-7** sobre el reseteo de CU-13, que conserva la cuenta y sus trabajos (RN-12). La justificación de RN-07 quedó actualizada en consecuencia: la baja sigue siendo destructiva e irreversible, pero ya no es frecuente por este motivo.
- **Por qué la habilitación deja de ser una transición pura, y por qué eso no la convierte en dos operaciones.** Fijar la credencial y poner la marca son efectos de la **misma** decisión del administrador, y separarlos habría producido exactamente la ventana que **RN-16** cierra: una cuenta `Habilitado` sin credencial, alcanzable por cualquiera que conociera el correo. El dominio los admite o los rechaza juntos, y por eso el rechazo de §6 es uno solo.
- **La provisoria no la produce el dominio.** El dominio recibe la credencial **ya derivada** y nunca la contraseña en claro, exactamente como en CU-03 y en CU-13. Quién produce el valor y con qué propiedades —no adivinable, no repetido entre cuentas ni entre actos— es **RN-14** y es de `GeometriaFactory-Infrastructure`; que la superficie se la muestre una vez al administrador para que la comunique es de `GeometriaFactory-Web`.
- **Por qué las cuatro operaciones se cierran sobre la cuenta de administrador.** Bloquearla produce el mismo efecto que darla de baja: por INV-06 no obtendría acceso, y como es única (RN-01) nadie podría desbloquearla. El daño va más allá de que una persona no entre: sin administrador nadie aprueba ni rechaza —CU-10 y CU-11 exigen ese papel—, de modo que **todos los trabajos quedarían en estado `Pendiente` para siempre y el circuito de revisión completo se detendría**. Habilitarla y rehabilitarla no procede por otro motivo: ya está `Habilitado` y nunca sale de ahí.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Precisa que la baja arrastra los trabajos **en cualquier estado**, incluidos los dos terminales que el modelo de estados nuevo introduce, y distingue ese arrastre de la terminalidad de INV-07. Cita el enunciado de RN-01 y RN-07 de §4.1 y el de INV-05 de §17.1.P.2. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Correcciones de la ronda r1 del audit**: hallazgo **P3-01**, §9 suma **RN-06** e INV-06, que ya listaban a este caso de uso porque es acá donde el estado de cuenta cambia; hallazgo **P3-04**, la sección opcional se numera §17, como fija `Rules-Especificacion-Funcional.md` §4.3. |
| 1.2 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-01**. §6 rechazaba únicamente la **baja** de la cuenta de administrador y dejaba las otras tres operaciones sin guarda: nada impedía **bloquearla**, y una cuenta bloqueada no obtiene acceso por INV-06, de modo que se alcanzaba por otra puerta la misma condición sin salida del P0. La corrección no es una decisión de diseño sino una transcripción que faltaba: la capacidad **F-03** del intake ya declara las cuatro operaciones sobre «cuentas **de alumno**», y esa cita queda escrita en §1 como fundamento para que nadie la revierta creyéndola inventada. §1, §3 y el paso 2 del flujo acotan el papel de la cuenta; el código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` se **retira** y lo reemplaza `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, que cubre las cuatro, con su fila de retiro declarada y sin reciclar el identificador; se suman los criterios CA-06 y CA-07, que cierran el bloqueo y las dos operaciones restantes; y §10 declara el efecto completo, que no se agota en el acceso: sin administrador el circuito de revisión entero se detiene. |
| 1.3 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**, que incorpora la capacidad **F-26** y la regla **RN-12**. **Ninguna de las cuatro operaciones de este contrato cambia** y no se agrega ninguna condición de rechazo. §1 declara que el **reseteo de contraseña no es una quinta operación** de este contrato y vive en **CU-13**, con los tres motivos que lo separan: no cambia el estado de cuenta, no dispara RN-07 y pone una marca que estas cuatro no tocan. §10 registra que **la baja dejó de ser la salida ante una contraseña olvidada**, con el retiro de X-2 y la reescritura de CL-7. |
| 1.4 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: una **línea en blanco partía la tabla** de este control de cambios y dejaba fuera de ella las filas que la seguían. Se retira, **sin tocar el texto de ninguna fila**. **Ninguna sección de este contrato de uso se toca**, y ningún flujo, código de rechazo, postcondición ni criterio de aceptación cambia. Sube minor: repara el renderizado de una tabla. |
| 1.5 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-6, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún flujo, precondición, código de rechazo, postcondición ni criterio de aceptación de este contrato de uso cambia**: la baja sigue arrastrando los trabajos de la cuenta. Sube minor: corrige una referencia a una fila retirada. |
| 1.6 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04.** Habilitar una cuenta **produce su contraseña provisoria** y la deja con **cambio de contraseña pendiente**, con el mismo mecanismo y el mismo tratamiento que el reseteo, de modo que no queda ninguna escritura anónima en el sistema. **§1** declara que la habilitación deja de ser una transición pura de la máquina de estados y su consecuencia estructural: ninguna cuenta de alumno llega a `Habilitado` sin credencial. **§3** suma la precondición de la credencial derivada provisoria aportada. **§4** suma el paso 4, que fija la credencial y pone la marca, y renumera los dos siguientes. **§5** precisa FA-02 —habilitar lo ya habilitado **no** produce provisoria nueva, porque dejaría al alumno fuera de su propia cuenta— y suma **FA-04**, habilitar sobre una cuenta ya marcada. **§6** suma el rechazo `HABILITACION_SIN_CREDENCIAL_PROVISORIA` y el recuento de rechazos pasa de tres a **cuatro**. **§7** parte la postcondición de éxito en habilitación y bloqueo. **§8** rehace CA-01 y CA-02 y suma **CA-08** y **CA-09**. **§9** suma RN-16, RN-14 y RN-13 a las reglas aplicables e **INV-09** a los invariantes. **§10** suma las dos notas que declaran por qué fijar la credencial y poner la marca son la misma operación y por qué el valor no lo produce el dominio. Sube minor: amplía una operación existente sin retirar ninguna transición ni ningún estado del conjunto cerrado. |
## 17. Compatibilidad de la superficie pública

Agregar un estado de cuenta al conjunto cerrado, o una transición nueva, es un cambio de alcance de este caso de uso y sube la versión mayor del documento. Quitar una transición admitida es un cambio incompatible para `GeometriaFactory-Application`, que la invoca por referencia de proyecto de código.

**La habilitación cambió de superficie con la emisión 1.6** y exige ahora la credencial derivada provisoria: es un cambio incompatible para `GeometriaFactory-Application`, que la invoca. **Volver a admitir la habilitación sin credencial se rechaza aunque compile**: repone la cuenta `Habilitado` sin credencial, que es la situación que **RN-16** suprime.
