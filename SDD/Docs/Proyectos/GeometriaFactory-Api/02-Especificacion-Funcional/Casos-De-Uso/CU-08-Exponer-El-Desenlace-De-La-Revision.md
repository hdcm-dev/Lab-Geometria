# CU-08 — Exponer el desenlace de la revisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-08-Exponer-El-Desenlace-De-La-Revision.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §4 (F-21, F-23), §4.1 (RN-10, RN-11), §4.2 y sus tres consecuencias aceptadas, §6 (flujo 2.1), §7 (CL-10, CL-11), §12 (entradas «Aprobar / Rechazar» y «Comentario»), §14 (RA-03); `Proyectos/GeometriaFactory-Contracts/.../CU-07-Contrato-De-Desenlace-De-La-Revision.md`; `Proyectos/GeometriaFactory-Application/.../CU-08-Dar-Desenlace-A-Un-Trabajo.md`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Api

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

---

## 1. Propósito

Exponer **A-15**, el punto de acceso con el que el administrador aprueba o rechaza un trabajo que está en estado `Pendiente`. Es el punto que le da desenlace a la entrega, y el único de la superficie que produce **una transición irreversible**: los dos estados a los que lleva son terminales y **ningún punto de esta superficie sale de ellos**.

Aprobar y rechazar son **un solo punto de acceso**, no dos, por el mismo criterio con el que el ensamblado de contratos los fusionó: comparten el tipo de solicitud, el resultado, la precondición, los errores y la regla que los gobierna, y se distinguen sólo por el valor de un campo de conjunto cerrado. Dos puntos habrían declarado la misma superficie dos veces.

El comentario del administrador viaja en la misma solicitud y **es opcional en los dos desenlaces**. El intake lo declara y acepta su consecuencia: un alumno puede recibir un rechazo sin explicación escrita.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma la solicitud desde el panel de revisión, con el desenlace pretendido y el comentario cuando lo hay |
| Administrador | Sujeto de la regla | Es el **único** que puede ejercer esta operación, y es facultad exclusiva |
| Alumno | Sujeto de la regla | Recibe el desenlace, y **no lo puede revertir por ningún punto de esta superficie** |

## 3. Precondiciones

- La petición trae acceso firmado con papel `Administrador` y atravesó la guardia de CU-02.
- El trabajo referenciado está en estado `Pendiente`. **Que lo esté lo comprueba el dominio**, y llega resuelto.

## 4. Flujo principal

1. Llega una petición a **A-15** con el identificador del trabajo, el desenlace pretendido y, opcionalmente, el comentario.
2. Se ejerce el desenlace contra la capa de aplicación, que verifica la facultad y el alcance sobre el dato recuperado y propaga la transición al dominio.
3. Se responde `200` con el resultado: **el estado terminal alcanzado** y el comentario tal como quedó registrado.

**El desenlace pretendido pertenece a un conjunto cerrado de dos valores.** Un valor fuera de ese conjunto no es un desenlace desconocido que haya que interpretar: es una petición mal formada.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El desenlace pretendido es el rechazo | El flujo es idéntico y sólo cambia el valor del campo. El estado terminal alcanzado es el otro, y **es igual de terminal** | Paso 3 |
| FA-02 | El desenlace llega **sin comentario** | Procede igual: el comentario es opcional en los dos desenlaces. **El alumno ve el estado y sabe que no fue aceptado, aunque no tenga el motivo por escrito**, y el intake acepta esa consecuencia explícitamente | Paso 3 |
| FA-03 | El alumno quiere corregir un trabajo rechazado | **No hay camino en esta superficie.** El estado es terminal: lo que el alumno hace es **cargar un trabajo nuevo** por A-10, y el rechazado queda como registro del intento hasta que el administrador lo elimine por A-12 | Termina fuera de este contrato |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Causa |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | Falta el identificador o el desenlace pretendido. **Nunca por el comentario**, que es opcional |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | `404` | El identificador no existe o está fuera de lo que el administrador ve, **incluido el trabajo en `Borrador`**. Las respuestas son indistinguibles |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | `409` | El trabajo no está en `Pendiente`: o nunca lo estuvo, o **ya recibió su desenlace y está en un estado terminal**. La respuesta **declara el estado actual y no sugiere ninguna forma de revertirlo** |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `403` | Quien pide no es el administrador, **aun sobre un trabajo propio en `Pendiente`**. Es el **único** código de facultad del conjunto cerrado, y este es el único punto donde se produce |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible |
| — | `400` | El desenlace pretendido **no pertenece al conjunto cerrado de dos valores**. No lleva código del contrato porque **la petición nunca llega a ser el tipo del contrato**: es el mismo tratamiento que el `401` de la guardia de CU-02 |

## 7. Postcondiciones

- **Éxito:** el trabajo quedó en uno de los dos estados terminales, con su comentario si lo hubo, y **ninguna petición posterior a este punto lo puede mover**.
- **Fallo:** el trabajo queda en el estado en que estaba, y en particular **un desenlace rechazado por estado terminal no altera el comentario existente**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en `Pendiente` y un acceso de administrador | Se invoca A-15 aprobando, con comentario | Responde `200`, el estado resultante es el terminal de aprobación y el comentario queda registrado |
| CA-02 | Otro trabajo en `Pendiente` | Se invoca A-15 rechazando, **sin comentario** | Responde `200`, el estado resultante es el terminal de rechazo y el trabajo queda con **0** comentarios. **Es válido** |
| CA-03 | Un trabajo ya rechazado | Se invoca A-15 sobre él, con cualquiera de los dos desenlaces | Responde `409` declarando el estado actual, y el cuerpo trae **0 campos** que sugieran una forma de revertirlo |
| CA-04 | Un trabajo en `Borrador` de un alumno | El administrador invoca A-15 sobre él | Responde `404`, con el mismo cuerpo que ante un identificador inexistente |
| CA-05 | Un trabajo propio en `Pendiente` y un acceso de papel `Alumno` | El alumno invoca A-15 | Responde `403` con el código de facultad, y el trabajo **sigue en `Pendiente`** |
| CA-06 | Una petición con un desenlace pretendido fuera del conjunto cerrado | Se invoca A-15 | Responde `400` y **0 transiciones** ocurren |
| CA-07 | Cualquier respuesta de §6, con el cuerpo y el registro observados | Se produce | **0 apariciones** de la ruta del almacén y de la dirección de cualquier servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-09 |
| Reglas de negocio aplicables | [RN-10](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), con sus dos mitades traducidas acá: el papel exigido y el estado que no admite desenlace. [RN-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md), porque el borrador le resulta indistinguible del inexistente. [RN-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), por el mismo motivo |
| Invariante del producto | **INV-07**: un trabajo en un estado terminal no cambia de estado ni de contenido. Esta superficie no ofrece ningún punto que lo intente |
| Regla de arquitectura del producto | **RA-03** en las condiciones de §6 |
| Punto de acceso | A-15 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-07` |
| Historias de usuario a generar en 06 | US-23 |
| Componentes esperados en 05 | Un punto de acceso de desenlace, con el conjunto cerrado de dos valores |
| Tests previstos en 08 | Integración por los siete criterios, **incluida la de forzar el desenlace con un acceso de alumno sobre un trabajo propio**, que es el caso que el papel por sí solo no distingue |

## 10. Notas y supuestos

- **La terminalidad se sostiene por ausencia y no por rechazo.** Lo que impide revertir un desenlace no es principalmente el `409`, sino que **no existe ningún punto de acceso que lo intente**: no hay un punto de reapertura, ni un verbo que devuelva un trabajo a `Pendiente`. El `409` cubre el intento sobre el punto que sí existe.
- **La facultad se comprueba dos veces y no es duplicación.** Acá se exige el papel declarado en el acceso, y la capa de aplicación verifica la facultad sobre el dato. La segunda es la que importa; la primera corta temprano lo que ningún dato podría autorizar. **CA-05 verifica el caso donde sólo la segunda alcanzaría**, que es un alumno pidiendo el desenlace de un trabajo propio.
- **Este es el único punto de la superficie con código de facultad propio en el contrato**, y es lo que hace visible el hueco de los demás: en el gobierno de cuentas, en el reseteo y en el listado de la comisión, el mismo rechazo tiene que viajar con el código genérico. Está elevado al Product Owner en el índice maestro §11.
- **El comentario no es una calificación**: es texto libre, sin nota y sin escala, y esta superficie no le impone ninguna estructura. Tampoco es una observación: viaja en su bloque propio y el detalle de CU-07 los mantiene separados.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Realineación de la cita viva al `PRODUCT-INTAKE` 1.13.** Este proyecto de código se emitió contra la **1.12** y la fuente está hoy en **1.13**, que incorpora la regla **RN-16** —habilitar una cuenta produce su contraseña provisoria— y precisa la capacidad **F-04**. La cabecera de trazabilidad pasa a citar **1.13**; la cita de la emisión inicial se conserva en la fila 1.0, que es trazabilidad y no una referencia desactualizada. **Ninguna sección de este contrato de uso se toca**: la decisión de 1.13 alcanza al circuito de credenciales y este caso de uso no lo expone. Sube minor: corrige una cita de trazabilidad. |
| 1.2 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, en la extensión que la búsqueda de propagación que el propio informe exige dejó al descubierto: la cabecera citaba `PRODUCT-INTAKE` **1.13** y pasa a citar **1.26**, vigentes hoy. El informe listaba **nueve** cabeceras envejecidas y sólo una de esta carpeta, `CU-12`; el `grep` sobre las categorías 02 y 03 devuelve **diecinueve** archivos con la cita vieja, **los doce casos de uso entre ellos**, y los diecinueve se corrigen en esta tanda. Se abrieron las secciones del intake que este caso de uso cita y **su contenido no cambió** entre 1.13 y 1.26 en nada que este documento afirme, de modo que **no había ninguna afirmación falsa**: lo que se repara es la trazabilidad. **Ningún paso, código, regla, criterio de aceptación ni recuento cambia.** Sube minor. |
