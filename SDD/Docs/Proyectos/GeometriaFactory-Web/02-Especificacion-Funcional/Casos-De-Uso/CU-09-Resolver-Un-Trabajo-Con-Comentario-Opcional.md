# CU-09 — Resolver un trabajo con comentario opcional

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md` §1, §5 (los siete criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §1; `../../../../00-Contexto/Vision-Producto.md` §9.1 (aprobar / rechazar, comentario, estado del trabajo); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-21, F-23, F-24), §4.1 (RN-04, RN-10), §4.2, §5 (historia 7.1), §6 (flujo 2.1), §7 (CL-10, CL-11), §9 (retiro de X-5)
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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
- [13. Interacción multiusuario y concurrencia](#13-interacción-multiusuario-y-concurrencia)

---

## 1. Propósito

Permitir que el administrador le dé desenlace a un trabajo en estado `Pendiente` —aprobarlo, con lo que pasa a `Finalizado`, o rechazarlo, con lo que pasa a `Rechazado`—, acompañándolo con un comentario escrito opcional, y que pueda además retirar cualquier trabajo que ve. Es la facultad que convierte una entrega depositada en una entrega resuelta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Decide el desenlace del trabajo, escribe el comentario si quiere y retira trabajos de los que ve |
| Pieza pública | Sistema | Ofrece las dos decisiones sólo donde corresponden, arma la solicitud con el comentario si lo hay y presenta el resultado |
| Pieza de datos | Secundario | Aplica la transición, registra el comentario y devuelve el estado alcanzado |
| Alumno dueño | Secundario | Ve el estado nuevo en su listado y el comentario al abrir el trabajo |

## 3. Precondiciones

- El administrador tiene sesión iniciada por CU-02 y su papel es el de administrador.
- El trabajo está abierto desde CU-08 y CU-07, FA-01, y está en estado `Pendiente`.
- Los dos estados de desenlace son terminales: ninguna transición sale de ellos.

## 4. Flujo principal

1. El administrador abre un trabajo en estado `Pendiente` y lo revisa en escena y árbol, según CU-07 FA-01.
2. La pieza pública ofrece las dos decisiones —aprobar y rechazar— y un campo de comentario **opcional**, sin longitud mínima.
3. El administrador elige una de las dos y, si quiere, escribe el comentario.
4. **La pieza pública invoca desde su servidor el contrato de desenlace de la revisión** de `GeometriaFactory-Contracts` CU-07, con el identificador del trabajo, la decisión y el comentario, que viaja sin poblar si no se escribió nada.
5. La pieza de datos aplica la transición y devuelve el estado alcanzado y el momento en que se registró.
6. La pieza pública muestra el estado alcanzado y devuelve al listado de CU-08, donde el trabajo ya figura con su estado nuevo.
7. La pieza pública deja de ofrecer las dos decisiones sobre ese trabajo, porque su estado ya no las admite.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador rechaza en lugar de aprobar | Es la misma solicitud con el otro valor de la decisión. No hay una pantalla distinta para cada una | El flujo continúa en el paso 5, con estado alcanzado `Rechazado` |
| FA-02 | El administrador resuelve sin escribir comentario | El campo viaja sin poblar y el desenlace procede igual. **El estado expresa el desenlace por sí solo** | El flujo continúa en el paso 5 |
| FA-03 | El administrador retira un trabajo de los que ve, en cualquiera de sus tres estados visibles | La pieza pública pide confirmación e invoca el contrato de eliminación de `GeometriaFactory-Contracts` CU-03, FA-04, que es **la misma solicitud** que usa el alumno. Lo que difiere es la regla que la acota, y esa regla vive en el dominio | El flujo vuelve al listado de CU-08, ya sin ese trabajo |
| FA-04 | El administrador abre un trabajo que ya tiene desenlace | La pieza pública no ofrece las dos decisiones y muestra el estado terminal y el comentario, si lo hay. La única acción disponible sobre él es retirarlo, por FA-03 | El flujo termina |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | El trabajo no está en estado `Pendiente`: nunca lo estuvo, o ya recibió su desenlace | La pieza pública declara el estado actual del trabajo y recarga el listado. Terminación controlada: **no hay camino para revertir un estado terminal** |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | Quien pide el desenlace no es el administrador, aun sobre un trabajo propio en estado `Pendiente` | La solicitud no procede. La pieza pública devuelve al panel del solicitante con un mensaje neutro. Terminación controlada |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a ningún trabajo que el solicitante vea, o no existe. Incluye el trabajo en estado `Borrador` | Mensaje neutro que no distingue los casos y regreso al listado |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el identificador o la decisión. **Nunca por el comentario**, que es opcional | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | No se produce en el camino del administrador: a él no lo acota el estado sino la visibilidad | Se declara para que la ausencia sea deliberada y no un olvido |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno. **El trabajo conserva el estado que tenía** y el administrador puede reintentar |

## 7. Postcondiciones

- En caso de éxito: el trabajo está en un estado terminal, el alumno lo ve en su listado y el comentario, si se escribió, le queda accesible al abrirlo.
- En caso de retiro: el trabajo dejó de existir y desaparece también del listado del alumno dueño.
- En caso de fallo: el trabajo conserva el estado que tenía y no queda ningún desenlace a medio aplicar.
- En ningún caso: la pieza pública ofrece salir de un estado terminal, reemplazar un comentario ya registrado, ni exigir un comentario para resolver.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Pendiente` abierto por el administrador | Se cuentan las decisiones ofrecidas | Son exactamente **2 de 2**: aprobar y rechazar |
| CA-02 | Un trabajo en estado `Pendiente` | El administrador lo aprueba sin escribir comentario | El trabajo pasa a `Finalizado` y el desenlace procede: **0 de 2** desenlaces exigen comentario |
| CA-03 | Un trabajo en estado `Pendiente` | El administrador lo rechaza con el comentario `Revisá el área del cubo` | El trabajo pasa a `Rechazado` y el alumno dueño lee ese comentario al abrirlo, en bloque aparte de las observaciones |
| CA-04 | Un trabajo ya en estado `Finalizado` | Se fuerza contra la pieza de datos una solicitud de desenlace sobre él | La transición no procede y la respuesta declara el estado actual: **cero** transiciones salen de un estado terminal |
| CA-05 | Un alumno con sesión iniciada y un trabajo propio en estado `Pendiente` | Fuerza contra la pieza de datos la solicitud de desenlace, sin usar la pantalla | La transición no procede: **cero** desenlaces ejecutados por un alumno |
| CA-06 | Un trabajo recién aprobado | El alumno dueño abre su listado | El estado figura como `Finalizado` sin que tenga que abrir el trabajo |
| CA-07 | Tres trabajos del mismo alumno, uno en cada estado visible para el administrador | El administrador intenta retirarlos | Puede retirar los **3 de 3**, y ninguno vuelve a aparecer en el listado del alumno |
| CA-08 | El servicio de datos detenido y un trabajo en estado `Pendiente` abierto | El administrador aprueba | La página sigue en pie con el estado degradado, el trabajo conserva su estado y el mensaje no contiene ninguna dirección de servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md), [`NB-07`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) |
| Reglas de negocio aplicables | [`RN-10`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [`RN-04`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) —cubre hoy también la eliminación del administrador sobre cualquier trabajo que ve—, [`RN-11`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-07](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-07-Contrato-De-Desenlace-De-La-Revision.md) completo, con FA-01, FA-02 y FA-03; [`CU-03`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) FA-04; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función propia. La revisión visual del trabajo la aporta CU-07 |
| Historias de usuario a generar en 06 | US-24, US-25 |
| Componentes esperados en 05 | Bloque de decisión dentro de la vista de trabajo del administrador y diálogo de confirmación del retiro |
| Tests previstos en 08 | Guion de demostración de la etapa `h` completo: los dos desenlaces, el comentario opcional, la terminalidad, el forzado por un alumno y el retiro en los tres estados |

## 10. Notas y supuestos

- **El comentario no es una observación y no es una calificación.** Lo escribe una persona, hay a lo sumo uno por trabajo, no lleva nota ni escala, y la vista lo presenta en un bloque propio, separado de lo que emite la interpretación del texto. Lo que sigue excluido del producto es la calificación.
- La terminalidad es lo que hace que el desenlace signifique algo. Su consecuencia declarada y aceptada: corregir un rechazo significa cargar un trabajo nuevo, y los rechazados se acumulan hasta que el administrador los retira por FA-03.
- Que la pantalla deje de ofrecer las decisiones sobre un trabajo resuelto **no es** lo que hace cumplir la regla. CA-04 y CA-05 la verifican forzando la solicitud sin pasar por la pantalla, y quien la hace cumplir es la pieza de datos.
- El comentario viaja al alumno únicamente en el detalle del trabajo. En el listado sólo llega el estado, y eso es deliberado: el listado no arrastra el texto libre de cada trabajo.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |

## 13. Interacción multiusuario y concurrencia

Sección opcional admitida por `Rules-Especificacion-Funcional.md` §4.3 para el tipo `web-monolith`.

Hay un solo administrador, de modo que no existen dos desenlaces simultáneos sobre el mismo trabajo. Lo que sí puede coincidir es un desenlace con el alumno dueño mirando su listado: el listado no se actualiza solo, y el alumno ve el estado nuevo la próxima vez que lo pida. Y puede coincidir un desenlace con un envío del alumno sobre otro trabajo suyo: son trabajos distintos y no compiten. El caso que sí colisiona —el administrador resuelve un trabajo que ya fue resuelto en otra pestaña— lo cierra `CONTRATO_ESTADO_NO_PERMITE_DESENLACE`, que declara el estado actual y no aplica nada.
