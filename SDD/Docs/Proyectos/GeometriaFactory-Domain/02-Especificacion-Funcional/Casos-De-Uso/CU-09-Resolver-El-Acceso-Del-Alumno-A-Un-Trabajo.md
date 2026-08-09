# CU-09 — Resolver el acceso de un alumno a un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5 (separación entre alumnos y acotación de la eliminación); `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-07), §4.1 (RN-03 y RN-04), §4.2 (modelo de estados del trabajo), §17.1.P.2 (INV-02 e INV-03), §7 (CL-5), §17.2.P.5, §17.5.P.5, §17.5.P.6
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

Responder si un alumno puede operar sobre un trabajo concreto y de qué manera: verlo, reeditarlo o eliminarlo. Reúne dos condiciones que el producto trata como una sola pregunta —la pertenencia del trabajo a su dueño y la acotación de lo que el alumno opera al estado `Borrador`— porque las dos deciden lo mismo: si la operación procede.

Lo que el **administrador** puede hacer sobre un trabajo no se responde acá: es un alcance distinto, con reglas propias, y vive en CU-11.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Consulta si la operación procede antes de ejecutarla |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Ejecuta la lectura o la eliminación sólo cuando la consulta la admitió |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Resuelve la pertenencia y la acotación por estado |

El alumno es el sujeto de la regla. La verificación ocurre del lado del servidor y no ocultando un control en la pantalla (`NB-03` §4).

## 3. Precondiciones

- El trabajo existe y tiene dueño.
- Se conoce la identidad del alumno que solicita la operación.
- La operación pertenece al conjunto ver, reeditar, eliminar.

## 4. Flujo principal

1. La capa de aplicación consulta si un alumno puede ejecutar una operación sobre un trabajo.
2. El dominio compara la identidad del solicitante con la del dueño del trabajo (INV-02).
3. Si coinciden, el dominio comprueba las condiciones propias de la operación.
4. Para reeditar y para eliminar, el dominio comprueba que el estado del trabajo sea `Borrador` (INV-03, RN-04).
5. El dominio devuelve que la operación procede.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El solicitante no es el dueño del trabajo | El dominio devuelve **no procede** con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que es deliberadamente indistinguible de la inexistencia: no se confirma la existencia de un trabajo ajeno (RN-03, INV-02) | Termina el caso de uso |
| FA-02 | El dueño solicita **ver** un trabajo propio en cualquier estado | Procede: el alumno ve sus cuatro estados en su propio listado, incluidos el desenlace y el comentario del administrador si los hay. Lo que la acotación por estado restringe es operar sobre él, no verlo | Paso 5 |
| FA-03 | El dueño solicita reeditar o eliminar un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado` | El dominio devuelve no procede con el motivo `OPERACION_FUERA_DE_BORRADOR`. Es un motivo distinto del de FA-01 porque acá la existencia del trabajo ya está admitida para su dueño | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | El solicitante no es el dueño | Devuelve no procede. El consumidor debe traducirlo a «no encontrado» y nunca a «no autorizado», para no confirmar la existencia del recurso ajeno |
| `OPERACION_FUERA_DE_BORRADOR` | El dueño intenta reeditar o eliminar fuera del estado `Borrador` | Devuelve no procede y el trabajo queda intacto |
| `OPERACION_DESCONOCIDA` | La operación consultada no pertenece al conjunto declarado | Devuelve no procede sin evaluar la pertenencia |

Ninguno de los tres tiene efecto sobre el trabajo: la consulta no modifica nada.

## 7. Postcondiciones

- **Éxito:** el resultado es procede, o no procede con exactamente un motivo. El trabajo no cambia en ningún caso.
- **Fallo:** no hay caso de fallo propio; la consulta siempre devuelve un resultado.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Borrador` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede eliminarlo | El dominio devuelve que procede |
| CA-02 | Un trabajo en estado `Borrador` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno B puede verlo | El dominio devuelve no procede con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, indistinguible de la inexistencia |
| CA-03 | Un trabajo en estado `Rechazado` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede eliminarlo | El dominio devuelve no procede con el motivo `OPERACION_FUERA_DE_BORRADOR`: el rechazado queda como registro del intento y sólo el administrador lo quita |
| CA-04 | Un trabajo en estado `Pendiente` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede reeditarlo | El dominio devuelve no procede con el motivo `OPERACION_FUERA_DE_BORRADOR` |
| CA-05 | Un trabajo en estado `Finalizado` con comentario, cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede verlo | El dominio devuelve que procede: el alumno ve el desenlace y el comentario de su propio trabajo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03 |
| Reglas de negocio aplicables | [RN-03](../Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) |
| Invariantes | INV-02, INV-03 |
| Historias de usuario a generar en 06 | US de separación de trabajos entre alumnos, US de eliminación acotada al borrador, US de visibilidad del desenlace propio |
| Componentes esperados en 05 | Resolución de pertenencia sobre la entidad de trabajo, con su enumeración cerrada de motivos |
| Tests previstos en 08 | Pruebas unitarias de pertenencia y de acotación en los cuatro estados. La verificación equivalente forzando la petición al servicio de datos, y no sólo desde la pantalla, es criterio bloqueante declarado y pertenece a las pruebas de integración de `GeometriaFactory-Api` |

## 10. Notas y supuestos

- El dominio devuelve un motivo; **la traducción a «no encontrado»** hacia afuera del proceso es responsabilidad de `GeometriaFactory-Api`. Acá se declara la equivalencia para que ninguna capa la invente.
- **INV-03 está acotado a la eliminación por parte de un alumno**, y no a la eliminación en general: el administrador elimina cualquier trabajo que ve, en cualquier estado, y un enunciado sin ese recorte sería falso (PRODUCT-INTAKE §17.1.P.2).
- La autorización por papel no reemplaza a la pertenencia: son dos comprobaciones distintas y la segunda es la que este caso de uso sostiene.
- La eliminación efectiva del dato la ejecuta la infraestructura; el dominio decide si procede.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el circuito de revisión de `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **INV-03 pasa de identificador sin enunciado a enunciado citado**, con su recorte a la eliminación por parte de un alumno. El caso de uso se acota explícitamente al alumno y remite a **CU-11** por el alcance del administrador, que RN-04 amplió. La acotación por estado pasa de cubrir sólo la eliminación a cubrir también la reedición, y el motivo se renombra a `OPERACION_FUERA_DE_BORRADOR`. Se suman `Rechazado` a los estados evaluados y el criterio CA-05, que verifica que el alumno vea el desenlace y el comentario de su propio trabajo. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |

## 17. Compatibilidad de la superficie pública

Distinguir hacia afuera el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` de una inexistencia real sería un cambio incompatible con RN-03, aunque no rompiera ninguna compilación. Agregar una operación al conjunto consultado es compatible mientras las existentes conserven su semántica.
