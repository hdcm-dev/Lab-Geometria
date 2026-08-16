# CU-02 — Gobernar el ciclo de vida de la cuenta del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-03), §4.1 (RN-01 y RN-07), §17.1.P.2 (INV-05), §17.1.P.5, §7 (CL-6), §9 (X-3), §11 (RN-B6)
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
- Para la baja, el consumidor ya obtuvo del administrador la confirmación escrita del correo de la cuenta (RN-07). Esa comprobación es del consumidor: el dominio expresa la exigencia, no la interfaz que la recoge.

## 4. Flujo principal

1. La capa de aplicación solicita al alumno una transición de estado de cuenta.
2. El dominio comprueba que el papel de la cuenta sea `Alumno` y lee su estado actual.
3. El dominio comprueba que el par estado actual y transición solicitada figure en la tabla de transiciones admitidas.
4. El dominio aplica la transición y deja el nuevo estado como estado actual.
5. El dominio devuelve el alumno con su nuevo estado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La operación solicitada es la baja de la cuenta | El dominio exige que la baja arrastre los trabajos del alumno **en cualquier estado en que estén**, incluidos los terminales: la cuenta y sus trabajos desaparecen juntos, y no se admite una baja que deje trabajos huérfanos (RN-07). El dominio expresa esa condición como parte de la operación; la eliminación efectiva del dato la ejecuta la infraestructura | Termina el caso de uso: no hay estado posterior porque la cuenta deja de existir |
| FA-02 | Se solicita habilitar una cuenta que ya está `Habilitado` | El dominio trata la operación como sin efecto y devuelve el alumno sin cambio de estado, en lugar de rechazarla: la operación es idempotente respecto del estado | Paso 5 |
| FA-03 | Se solicita bloquear una cuenta `Pendiente` | Transición no declarada por las fuentes. El dominio la rechaza y no la infiere | Paso 3, con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | El par estado actual y transición solicitada no figura en la tabla de transiciones | Rechaza la operación y conserva el estado actual sin modificar |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Se solicita la baja declarando que los trabajos del alumno se conservan | Rechaza la operación: la baja arrastra los trabajos, y esa consecuencia está aceptada por escrito aguas arriba |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se solicita **cualquiera de las cuatro operaciones** —habilitar, bloquear, rehabilitar o dar de baja— sobre la cuenta con papel `Administrador` | Rechaza la operación y conserva la cuenta sin modificar. Las cuatro están declaradas sobre cuentas de alumno (F-03), y sobre la única cuenta de administrador ninguna tiene inversa posible: la instancia quedaría sin nadie capaz de habilitar, desbloquear y revisar (INV-05, RN-01) |

Los tres rechazos son terminaciones controladas: la cuenta queda exactamente como estaba antes de la solicitud.

**Identificador retirado en la versión 1.2.** El código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` queda **retirado** y no figura entre las condiciones vivas de este contrato: cubría una sola de las cuatro operaciones y dejaba las otras tres sin guarda, que es como se llegó al hallazgo H-01 de la ronda r3. Toda cita anterior resuelve a `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, que cubre las cuatro. **El identificador retirado no se recicla para ninguna otra condición**, para que una referencia vieja no resuelva en silencio a un código distinto del que nombraba.

## 7. Postcondiciones

- **Éxito de una transición:** el alumno tiene el nuevo estado y ningún otro atributo cambió.
- **Éxito de una baja:** la operación queda declarada como baja con arrastre de los trabajos del alumno, para que el consumidor la materialice como una sola unidad.
- **Fallo:** el estado de cuenta se conserva sin cambios y no hay efecto parcial.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita habilitarlo | El dominio devuelve el alumno con cuenta `Habilitado` |
| CA-02 | Un alumno con cuenta `Habilitado` | La capa de aplicación solicita bloquearlo y luego rehabilitarlo | El dominio devuelve el alumno con cuenta `Bloqueado` y después `Habilitado`, y admite las 4 operaciones declaradas |
| CA-03 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita bloquearlo | El dominio rechaza con el código `TRANSICION_DE_CUENTA_NO_ADMITIDA` y la cuenta sigue en `Pendiente` |
| CA-04 | Un alumno con cuenta `Bloqueado` y 3 trabajos, uno de ellos en estado `Finalizado` | La capa de aplicación solicita darlo de baja conservando los trabajos | El dominio rechaza con el código `BAJA_SIN_ARRASTRE_DE_TRABAJOS` |
| CA-05 | La cuenta con papel `Administrador` | La capa de aplicación solicita darla de baja | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` |
| CA-06 | La cuenta con papel `Administrador`, en estado `Habilitado` | La capa de aplicación solicita **bloquearla** | El dominio rechaza con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta sigue en `Habilitado`: bloquearla dejaría a la instancia sin ninguna cuenta capaz de desbloquearla |
| CA-07 | La cuenta con papel `Administrador` | La capa de aplicación solicita habilitarla y, por separado, rehabilitarla | El dominio rechaza las 2 con el código `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`: las 4 operaciones quedan cerradas sobre esa cuenta |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 |
| Reglas de negocio aplicables | [RN-01](../../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-07](../../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [RN-06](../../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) —este caso de uso es donde el estado de cuenta cambia, y de ese estado depende que la cuenta obtenga o no acceso— |
| Invariantes | INV-05, INV-06 |
| Historias de usuario a generar en 06 | US de habilitación, US de bloqueo y rehabilitación, US de baja con arrastre |
| Componentes esperados en 05 | Máquina de transiciones de estado de cuenta dentro de la entidad de alumno |
| Tests previstos en 08 | Pruebas unitarias de la tabla de transiciones, incluidas las inadmisibles, y del rechazo de la baja sin arrastre |

## 10. Notas y supuestos

- La confirmación escrita del correo antes de la baja es una exigencia de negocio que el dominio **declara** y que la interfaz del producto **recoge**; el detalle de esa interacción pertenece a la categoría 03 del proyecto de código de la pieza pública, no a esta.
- La baja es física y no un estado: por eso no aparece en la máquina de estados como destino, sino como salida del ciclo de vida.
- La pérdida de los trabajos de la cuenta dada de baja es un riesgo residual declarado y aceptado aguas arriba (`Vision-Producto.md` §8, RG-06). Alcanza también a los trabajos en estado `Finalizado` y `Rechazado`: la terminalidad de esos dos estados impide que cambien de estado o de contenido (INV-07), no que la baja de la cuenta los arrastre.
- La eliminación de **un** trabajo, sea por su dueño o por el administrador, no es esta operación: vive en CU-09 y en CU-11.
- **Por qué las cuatro operaciones se cierran sobre la cuenta de administrador.** Bloquearla produce el mismo efecto que darla de baja: por INV-06 no obtendría acceso, y como es única (RN-01) nadie podría desbloquearla. El daño va más allá de que una persona no entre: sin administrador nadie aprueba ni rechaza —CU-10 y CU-11 exigen ese papel—, de modo que **todos los trabajos quedarían en estado `Pendiente` para siempre y el circuito de revisión completo se detendría**. Habilitarla y rehabilitarla no procede por otro motivo: ya está `Habilitado` y nunca sale de ahí.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Precisa que la baja arrastra los trabajos **en cualquier estado**, incluidos los dos terminales que el modelo de estados nuevo introduce, y distingue ese arrastre de la terminalidad de INV-07. Cita el enunciado de RN-01 y RN-07 de §4.1 y el de INV-05 de §17.1.P.2. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Correcciones de la ronda r1 del audit**: hallazgo **P3-01**, §9 suma **RN-06** e INV-06, que ya listaban a este caso de uso porque es acá donde el estado de cuenta cambia; hallazgo **P3-04**, la sección opcional se numera §17, como fija `Rules-Especificacion-Funcional.md` §4.3. |
| 1.2 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-01**. §6 rechazaba únicamente la **baja** de la cuenta de administrador y dejaba las otras tres operaciones sin guarda: nada impedía **bloquearla**, y una cuenta bloqueada no obtiene acceso por INV-06, de modo que se alcanzaba por otra puerta la misma condición sin salida del P0. La corrección no es una decisión de diseño sino una transcripción que faltaba: la capacidad **F-03** del intake ya declara las cuatro operaciones sobre «cuentas **de alumno**», y esa cita queda escrita en §1 como fundamento para que nadie la revierta creyéndola inventada. §1, §3 y el paso 2 del flujo acotan el papel de la cuenta; el código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` se **retira** y lo reemplaza `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, que cubre las cuatro, con su fila de retiro declarada y sin reciclar el identificador; se suman los criterios CA-06 y CA-07, que cierran el bloqueo y las dos operaciones restantes; y §10 declara el efecto completo, que no se agota en el acceso: sin administrador el circuito de revisión entero se detiene. |

## 17. Compatibilidad de la superficie pública

Agregar un estado de cuenta al conjunto cerrado, o una transición nueva, es un cambio de alcance de este caso de uso y sube la versión mayor del documento. Quitar una transición admitida es un cambio incompatible para `GeometriaFactory-Application`, que la invoca por referencia de proyecto de código.
