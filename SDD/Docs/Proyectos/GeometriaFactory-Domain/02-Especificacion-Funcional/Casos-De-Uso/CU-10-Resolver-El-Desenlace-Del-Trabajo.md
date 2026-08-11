# CU-10 — Resolver el desenlace del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-10-Resolver-El-Desenlace-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §1, §4 y §5; [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (visibilidad del avance sobre los 4 estados); `00-Contexto/Vision-Producto.md` §9.1 (estado del trabajo, aprobar / rechazar, comentario) y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-21 y F-23), §4.1 (RN-10), §4.2 (modelo de estados del trabajo y sus tres consecuencias aceptadas), §17.1.P.2 (INV-04 e INV-07), §5 (historia 7.1), §6 (flujo 2.1), §12 (glosario: aprobar / rechazar, comentario)
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

Aplicar sobre un trabajo en estado `Pendiente` el desenlace que decide el administrador —aprobar, que lo pasa a `Finalizado`, o rechazar, que lo pasa a `Rechazado`—, con un comentario escrito opcional, y hacer cumplir que los dos estados de cierre sean terminales. Es el contrato que convierte una entrega depositada en una entrega con respuesta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita el desenlace, habiendo comprobado antes que quien lo pide tiene el papel `Administrador` |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa el estado resultante y el comentario fuera del dominio |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite el desenlace sólo desde `Pendiente`, fija el estado terminal y adopta el comentario |

El administrador es el sujeto de la regla y el alumno el destinatario del desenlace; ninguno de los dos es actor del contrato, que lo ejerce el código consumidor.

## 3. Precondiciones

- El trabajo existe y está en estado `Pendiente`.
- El consumidor declara que quien solicita el desenlace tiene papel `Administrador`: es facultad exclusiva suya (RN-10).
- El desenlace pertenece al conjunto cerrado de dos valores: aprobar o rechazar.
- El comentario, si viene, es texto libre. Puede no venir.

## 4. Flujo principal

1. La capa de aplicación solicita el desenlace de un trabajo, con el papel del solicitante declarado y el comentario si lo hay.
2. El dominio comprueba que el estado actual sea `Pendiente`.
3. El dominio comprueba que el papel declarado sea `Administrador`.
4. El dominio comprueba que el desenlace pertenezca al conjunto cerrado.
5. El dominio fija el estado en `Finalizado` si el desenlace es aprobar, o en `Rechazado` si es rechazar.
6. El dominio adopta el comentario si vino, y lo deja sin valor si no vino.
7. El dominio marca el trabajo como terminal: a partir de acá no cambia de estado ni de contenido (INV-07).
8. El dominio devuelve el trabajo con su desenlace aplicado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El desenlace llega sin comentario | El dominio lo admite: el comentario es **opcional en los dos desenlaces**, y su ausencia es una consecuencia aceptada por escrito aguas arriba —un alumno puede recibir un rechazo sin explicación escrita, y el estado le informa que no fue aceptado (PRODUCT-INTAKE §4.2, consecuencia 3)— | Paso 6, con el comentario sin valor |
| FA-02 | El administrador quiere corregir el desenlace ya aplicado | No hay camino: los dos estados de cierre son terminales y de ellos no sale ninguna transición. El dominio rechaza. Lo que el administrador sí puede hacer es eliminar el trabajo, que es **CU-11** | Termina con el rechazo de §6 |
| FA-03 | El alumno cuyo trabajo fue rechazado quiere volver a intentarlo | El dominio no admite reabrir el trabajo rechazado: corregir un rechazo significa **cargar un trabajo nuevo** (CU-05), y el rechazado queda como registro del intento | Termina el caso de uso sin efecto sobre el trabajo rechazado |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `DESENLACE_FUERA_DE_PENDIENTE` | Se solicita el desenlace de un trabajo que no está en estado `Pendiente` | Rechaza la operación y conserva el estado actual. Un trabajo en `Borrador` no se aprueba ni se rechaza: el administrador ni siquiera lo ve (RN-11) |
| `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` | El papel declarado no es `Administrador` | Rechaza la operación. La facultad es exclusiva y no se delega, ni siquiera sobre el trabajo propio |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Se solicita un desenlace nuevo sobre un trabajo ya `Finalizado` o `Rechazado` | Rechaza la operación: el trabajo no cambia de estado ni de contenido (INV-07) |
| `DESENLACE_DESCONOCIDO` | El desenlace no es aprobar ni rechazar | Rechaza la operación sin tocar el trabajo |

## 7. Postcondiciones

- **Éxito de la aprobación:** el trabajo está en estado `Finalizado`, con su comentario si lo hubo, y es terminal.
- **Éxito del rechazo:** el trabajo está en estado `Rechazado`, con su comentario si lo hubo, y es terminal.
- En los dos casos el dueño, el texto original, las piezas y las observaciones no cambiaron.
- **Fallo:** el trabajo queda exactamente como estaba, con su estado y sin comentario nuevo.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Pendiente` con las 2 advertencias del escenario E-1 | La capa de aplicación solicita aprobarlo con papel `Administrador` y el comentario «revisá la fórmula del área del cubo» | El dominio devuelve el trabajo en estado `Finalizado`, con ese comentario y con sus 2 advertencias conservadas |
| CA-02 | Un trabajo en estado `Pendiente` | La capa de aplicación solicita rechazarlo con papel `Administrador` y sin comentario | El dominio devuelve el trabajo en estado `Rechazado`, con 0 comentarios: el comentario es opcional en los 2 desenlaces |
| CA-03 | Un trabajo en estado `Pendiente` | La capa de aplicación solicita aprobarlo declarando papel `Alumno` | El dominio rechaza con el código `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` y el trabajo sigue en estado `Pendiente` |
| CA-04 | Un trabajo en estado `Finalizado` con comentario | La capa de aplicación solicita rechazarlo con papel `Administrador` | El dominio rechaza con el código `TRANSICION_DESDE_ESTADO_TERMINAL` y el trabajo conserva su estado y su comentario |
| CA-05 | Un trabajo en estado `Borrador` con 1 error de validación | La capa de aplicación solicita aprobarlo con papel `Administrador` | El dominio rechaza con el código `DESENLACE_FUERA_DE_PENDIENTE` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-09, y NB-03 en cuanto a los cuatro estados que el alumno ve en su listado |
| Reglas de negocio aplicables | [RN-10](../Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [RN-11](../Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) en cuanto a qué trabajos pueden llegar a desenlace, [RN-05](../Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) en cuanto a que sólo llegan a `Pendiente` los trabajos sin errores de validación |
| Invariantes | INV-07, e INV-04 por consecuencia: `Finalizado` sólo se alcanza desde `Pendiente`, y a `Pendiente` no se llega con errores de interpretación |
| Historias de usuario a generar en 06 | US de aprobación con comentario opcional, US de rechazo con comentario opcional, US de terminalidad del desenlace |
| Componentes esperados en 05 | Transiciones de desenlace en la entidad de trabajo, con el comentario como atributo opcional |
| Tests previstos en 08 | Pruebas unitarias de aprobación y de rechazo con y sin comentario, del rechazo por papel, del rechazo desde estado terminal y del rechazo desde `Borrador`; y la comprobación de que 0 transiciones salen de los dos estados de cierre |

## 10. Notas y supuestos

- **El comentario no es una observación** y **no es una calificación**: es texto libre, sin nota ni escala, escrito por una persona, y hay a lo sumo uno porque los dos desenlaces son terminales (`Vision-Producto.md` §9.1). Tampoco tiene relación con los comentarios que el validador tolera dentro del texto del alumno, que son sintaxis del dato de entrada.
- La comprobación del papel llega **declarada** por el consumidor: el dominio no resuelve autenticación ni autorización de transporte, sólo exige que la facultad esté acreditada. El mecanismo vive en `GeometriaFactory-Api`.
- El desenlace es **el único camino** por el que un trabajo sale de `Pendiente` sin desaparecer. La otra salida es la eliminación por el administrador, que es **CU-11**: CU-09 responde por el alumno, y el alumno no elimina fuera de `Borrador`.
- La terminalidad deja un residuo aceptado: un alumno que rebota varias veces acumula trabajos rechazados, y sólo el administrador puede quitarlos.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Nace con el circuito de revisión que `PRODUCT-INTAKE` 1.3 incorporó y con la necesidad NB-09 que `01-Necesidades-Negocio` 1.1 emitió. Recoge la transición de `Pendiente` a `Finalizado` que la versión 1.0 de CU-08 gobernaba, la de `Pendiente` a `Rechazado` que no existía, el comentario opcional del administrador y la terminalidad de los dos estados de cierre. **Correcciones de la ronda r1 del audit, dentro de esta misma versión**: hallazgo **P2-01**, FA-02 y §10 remitían la eliminación por el administrador a CU-09 y corresponde **CU-11**, que es el contrato del administrador; hallazgo **P3-04**, la sección opcional se numera §17, como fija `Rules-Especificacion-Funcional.md` §4.3. |

## 17. Compatibilidad de la superficie pública

El conjunto de desenlaces es cerrado y de dos valores, y los dos son terminales. Agregar un tercer desenlace, admitir la corrección de uno ya aplicado o volver obligatorio el comentario son cambios de alcance del modelo: suben versión mayor y exigen revisar RN-10 e INV-07, además de la decisión del Product Owner que los declaró.
