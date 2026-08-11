# CU-06 — Consultar los trabajos propios del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (visibilidad del avance, separación entre alumnos); [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §5 (devolución visible para el alumno); [`NB-06`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md) §5 (piezas efectivamente dibujadas, sincronización entre el árbol y la escena, parcial); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-08, F-11), §4.1 (RN-03), §4.2, §7 (CL-5), §17.2.P.5, §17.2.P.10 (las consultas de listado nunca cargan los componentes); orquesta [`CU-09` de GeometriaFactory-Domain](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md)
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

Devolverle al alumno el listado de **sus** trabajos con el estado de cada uno, y el detalle de uno de ellos con sus piezas, sus componentes, sus observaciones y, si lo tiene, el desenlace y el comentario del administrador. El alcance de la consulta lo fija la pertenencia: un alumno no obtiene por esta vía ningún trabajo de otro, ni siquiera cambiando el identificador de la petición.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Solicita el listado o el detalle aportando la identidad del alumno solicitante |
| Puerto de repositorio de trabajos | Sistema | Resuelve la consulta acotada al dueño y recupera el detalle |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Resuelve si el solicitante puede ver el trabajo pedido |

El alumno es el sujeto de la regla.

## 3. Precondiciones

- El consumidor aporta la identidad del alumno solicitante, ya autenticado por la capa externa.
- Para el detalle, aporta además el identificador del trabajo.

## 4. Flujo principal

1. El consumidor solicita el listado de trabajos del alumno solicitante.
2. El caso de uso pide al puerto de repositorio los trabajos **cuyo dueño es ese alumno**, y no filtra después sobre un conjunto mayor.
3. El puerto devuelve, por cada trabajo, su identificador, su nombre, su fecha, su estado y el recuento de observaciones, **sin los componentes de las piezas**.
4. El caso de uso devuelve el listado con los cuatro estados distinguibles.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El consumidor solicita el **detalle** de un trabajo | El caso de uso lo recupera por el puerto y consulta al dominio si el solicitante puede verlo. Si procede, devuelve los datos, el texto original, las piezas con sus componentes y las observaciones | Termina el caso de uso |
| FA-02 | El trabajo del detalle tiene desenlace | El detalle incluye el estado terminal y el comentario del administrador si lo hay. El alumno ve el desenlace de su propio trabajo, y que el comentario sea opcional significa que puede no haber ninguno | Termina el caso de uso |
| FA-03 | El alumno no tiene ningún trabajo | El caso de uso devuelve un listado vacío, que no es un error | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | El detalle se pide sobre un trabajo de otro alumno, o sobre un identificador que no existe | Devuelve no procede. El consumidor lo traduce a «no encontrado» y **nunca** a «no autorizado»: confirmar que el recurso existe pero es ajeno ya sería informar de más (RN-03, INV-02) |
| `SOLICITANTE_NO_DECLARADO` | El consumidor no aporta la identidad del solicitante | Termina sin consultar el repositorio: un listado sin dueño declarado sería el listado de todos |

Ninguna de las dos modifica nada: son consultas.

## 7. Postcondiciones

- **Éxito, listado:** el resultado contiene exactamente los trabajos del solicitante, sin componentes de las piezas.
- **Éxito, detalle:** el resultado contiene un trabajo del solicitante con sus piezas, sus componentes, sus observaciones y su desenlace si lo tiene.
- **Fallo:** ningún dato de otro alumno se devuelve ni se insinúa.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno A con 4 trabajos, uno en cada estado: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado`, y un alumno B con 2 trabajos | El consumidor solicita el listado del alumno A | El caso de uso devuelve 4 trabajos, los 4 del alumno A, y los 4 estados quedan distinguibles |
| CA-02 | El mismo repositorio | El consumidor solicita el listado del alumno A | Ninguno de los 4 elementos devueltos trae componentes de piezas |
| CA-03 | Un trabajo del alumno B en estado `Pendiente` | El consumidor solicita su detalle declarando como solicitante al alumno A | El caso de uso devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, el mismo que devolvería para un identificador inexistente |
| CA-04 | Un trabajo del alumno A en estado `Rechazado` con el comentario «Revisá el área del cubo» | El alumno A solicita su detalle | El caso de uso devuelve el estado `Rechazado` y el comentario «Revisá el área del cubo» |
| CA-05 | Un alumno A sin ningún trabajo | El consumidor solicita su listado | El caso de uso devuelve 0 trabajos y ningún motivo de error |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, NB-09 en su criterio de devolución visible para el alumno, y NB-06 de forma parcial: acá se entregan las piezas con su identidad posicional que el dibujo consume |
| Reglas de negocio aplicables | [RN-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) |
| Casos de uso de dominio orquestados | [CU-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) |
| Puertos que consume | Repositorio de trabajos |
| Historias de usuario a generar en 06 | US-17, US-18, US-19 |
| Componentes esperados en 05 | Caso de uso de listado propio y caso de uso de detalle propio, con dos formas de resultado distintas: sin componentes y con componentes |
| Tests previstos en 08 | Unitarias con repositorio simulado: listado acotado al dueño, ausencia de componentes en el listado, detalle ajeno con el motivo de inexistencia, detalle con desenlace y comentario, y listado vacío |

## 10. Notas y supuestos

- **La consulta se acota en el pedido al puerto, no filtrando después.** Pedir todos los trabajos y descartar los ajenos en memoria daría el mismo resultado visible y sería el patrón que la separación entre alumnos viene a impedir.
- **Las consultas de listado nunca cargan los componentes de las piezas**: es una decisión de modelado declarada aguas arriba, con efecto directo en el tiempo de respuesta, y por eso el listado y el detalle tienen resultados distintos.
- El alumno **ve** sus cuatro estados; lo que la acotación al `Borrador` restringe es operar sobre el trabajo, no verlo.
- El dibujo en tres dimensiones y el árbol del texto se arman fuera de esta capa, con los datos que este caso de uso entrega.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |

## 17. Compatibilidad de la superficie pública

Agregar campos al elemento del listado es compatible mientras no incorpore los componentes de las piezas, que es lo que la decisión de modelado excluye. Devolver un trabajo ajeno, o distinguir hacia afuera el ajeno del inexistente, contradicen RN-03.
