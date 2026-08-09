# CU-09 — Eliminar un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-09-Eliminar-Un-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (separación entre alumnos y acotación de la eliminación); [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §5 (retiro de trabajos por el administrador); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-07, F-24), §4.1 (RN-03, RN-04, RN-11), §4.2, §7 (CL-5, CL-10), §17.2.P.5; orquesta [`CU-09`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) y [`CU-11`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-11-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) de GeometriaFactory-Domain
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

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

Retirar un trabajo del laboratorio, resolviendo antes cuál de los dos alcances aplica: el del **alumno**, que elimina sólo lo propio y sólo en `Borrador`, o el del **administrador**, que elimina cualquiera de los trabajos que ve, en los tres estados que no son `Borrador`. Los dos alcances son un solo contrato porque responden la misma pregunta —si el retiro procede— y porque el dominio ya los tiene separados en dos resoluciones que esta capa elige según el papel de quien pide.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Solicita la eliminación declarando la identidad y el papel de quien la pide |
| Puerto de repositorio de trabajos | Sistema | Recupera el trabajo y ejecuta el retiro efectivo |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Resuelve si la eliminación procede, con la resolución que corresponde al papel |

El alumno y el administrador son los sujetos de las reglas; el contrato lo ejerce el código consumidor.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien solicita.
- El consumidor aporta el identificador del trabajo.

## 4. Flujo principal

1. El consumidor solicita eliminar un trabajo declarando quién lo pide.
2. El caso de uso recupera el trabajo por el puerto de repositorio.
3. El papel de quien pide es `Alumno`: el caso de uso consulta al dominio la resolución de acceso del alumno, que verifica pertenencia y estado `Borrador` (RN-03, RN-04, INV-02, INV-03).
4. La resolución procede: el caso de uso ejecuta el retiro por el puerto de repositorio, con el trabajo, sus piezas y sus observaciones, en una única unidad de trabajo.
5. El caso de uso devuelve que el trabajo fue retirado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El papel de quien pide es `Administrador` | El caso de uso consulta al dominio la resolución de alcance del administrador, que admite la eliminación en los tres estados que él ve y la niega en `Borrador` (RN-04, RN-11). Si procede, el retiro sigue el mismo camino del paso 4 | Paso 4 |
| FA-02 | El alumno pide eliminar un trabajo propio que no está en `Borrador` | El caso de uso devuelve no procede con el motivo `OPERACION_FUERA_DE_BORRADOR`. Un trabajo `Rechazado` queda como registro del intento y sólo el administrador puede quitarlo | Termina el caso de uso |
| FA-03 | El alumno pide eliminar un trabajo de otro alumno | El caso de uso devuelve no procede con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, indistinguible de la inexistencia | Termina el caso de uso |
| FA-04 | El administrador pide eliminar un trabajo en `Borrador` | El caso de uso devuelve no procede con el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`: los borradores no forman parte de su flujo de trabajo, ni para verlos ni para quitarlos | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | Un alumno pide eliminar un trabajo ajeno, o un identificador que no existe | No retira nada. El consumidor lo traduce a «no encontrado» y nunca a «no autorizado» |
| `OPERACION_FUERA_DE_BORRADOR` | Un alumno pide eliminar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado` | No retira nada. Es un motivo distinto del anterior porque acá la existencia ya está admitida para su dueño |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | El administrador pide eliminar un trabajo en `Borrador` | No retira nada |
| `PAPEL_NO_RECONOCIDO` | El papel declarado no pertenece al conjunto cerrado de dos valores | Termina sin evaluar ninguna de las dos resoluciones |

Ninguno deja el trabajo a medio retirar: o se va entero con sus piezas y sus observaciones, o no se toca.

## 7. Postcondiciones

- **Éxito:** el trabajo no existe, y tampoco sus piezas ni sus observaciones. El retiro es definitivo y no deja el trabajo en ningún estado nuevo.
- **Fallo:** el trabajo queda íntegro, con su estado y su contenido.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en `Borrador` del alumno A, con 3 piezas y 2 observaciones | El alumno A lo elimina | El caso de uso lo retira entero y el repositorio queda con 0 trabajos, 0 piezas y 0 observaciones de ese trabajo |
| CA-02 | Un trabajo en `Rechazado` del alumno A | El alumno A intenta eliminarlo | El caso de uso devuelve el motivo `OPERACION_FUERA_DE_BORRADOR` y el trabajo sigue existiendo |
| CA-03 | Un trabajo en `Borrador` del alumno B | El alumno A intenta eliminarlo | El caso de uso devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, el mismo que para un identificador inexistente, y el trabajo sigue existiendo |
| CA-04 | 3 trabajos del alumno A, uno en estado `Pendiente`, uno en `Finalizado` y uno en `Rechazado` | El administrador los elimina de a uno | El caso de uso retira los 3 de 3 |
| CA-05 | Un trabajo en `Borrador` del alumno A | El administrador intenta eliminarlo | El caso de uso devuelve el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` y el trabajo sigue existiendo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03 en su criterio de separación entre alumnos y acotación de la eliminación; NB-09 en su criterio de retiro de trabajos por el administrador |
| Reglas de negocio aplicables | [RN-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md), [RN-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) |
| Casos de uso de dominio orquestados | [CU-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md), [CU-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-11-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) |
| Puertos que consume | Repositorio de trabajos |
| Historias de usuario a generar en 06 | US-26, US-27 |
| Componentes esperados en 05 | Caso de uso de eliminación con selección de resolución por papel; contrato de retiro en el puerto de repositorio de trabajos |
| Tests previstos en 08 | Unitarias con repositorio simulado: eliminación propia en `Borrador`, propia fuera de `Borrador`, ajena, eliminación por el administrador en los tres estados que ve, y sobre un borrador. La verificación forzando la petición al servicio de datos pertenece a las pruebas de integración de `GeometriaFactory-Api` |

## 10. Notas y supuestos

- **Los dos alcances son opuestos y por eso conviven en un solo contrato**: al alumno lo acota la pertenencia y el borrador; al administrador lo acota exactamente lo contrario, todo menos el borrador. Elegir la resolución por el papel es la única decisión propia de esta capa.
- **El actor primario sigue siendo uno solo**, el código consumidor. Los dos sujetos de las reglas no son actores del contrato, de modo que el caso de uso no tiene dos actores primarios.
- La eliminación de **todos** los trabajos de una cuenta al darla de baja no es este caso de uso: es CU-02, y allí el disparador es la baja de la cuenta y no el trabajo.
- El retiro es definitivo: no hay estado de eliminado ni recuperación posterior.
- **`OPERACION_DESCONOCIDA` del dominio es inalcanzable por construcción.** Este contrato consulta una sola operación —eliminar— en cada una de las dos resoluciones, de modo que el conjunto cerrado que el dominio verifica nunca se viola desde acá; lo que sí puede llegar mal es el papel, y eso es `PAPEL_NO_RECONOCIDO`. Se nombra para que su ausencia en §6 no se lea como olvido.
- **`ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` del dominio tampoco se produce**: la resolución que este caso de uso consulta la elige por el papel declarado, y la equivalencia con el motivo de facultad de esta capa está en `Especificacion-Funcional.md` §4.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-14**: §10 declara que `OPERACION_DESCONOCIDA` es inalcanzable por construcción y cuál es su relación con `PAPEL_NO_RECONOCIDO`. **H-13**: §10 declara que `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` del dominio no llega a producirse y remite a la equivalencia del índice maestro. |

## 17. Compatibilidad de la superficie pública

Ampliar la eliminación del alumno más allá de `Borrador`, o acotar la del administrador a algunos de los estados que ve, contradicen RN-04 y son cambios de alcance. Agregar un motivo a la enumeración es compatible si el consumidor tiene un camino por defecto.
