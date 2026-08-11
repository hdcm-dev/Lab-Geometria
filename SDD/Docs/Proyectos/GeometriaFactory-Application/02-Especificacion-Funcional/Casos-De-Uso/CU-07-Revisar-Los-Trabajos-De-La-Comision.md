# CU-07 — Revisar los trabajos de la comisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-07-Revisar-Los-Trabajos-De-La-Comision.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-07`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) §5 (alcance de la vista, recorte del listado, organización del listado, coincidencia de la vista entre los dos papeles); [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §5 (cobertura de los desenlaces, parcial); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-12), §4.1 (RN-11), §4.2, §6 (flujo 2.1 y flujo 3), §17.2.P.10; orquesta [`CU-11` de GeometriaFactory-Domain](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-11-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md)
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

Entregarle al administrador el listado de los trabajos de toda la comisión —agrupable y filtrable por alumno— y el detalle de cualquiera de ellos, con el mismo contenido que ve su dueño. El alcance es el complemento exacto del de CU-06: acá no rige la pertenencia sino la **facultad**, y lo que se excluye son los trabajos en `Borrador`, que no forman parte del flujo de trabajo del administrador.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Solicita el listado o el detalle declarando la identidad y el papel de quien lo pide |
| Puerto de repositorio de trabajos | Sistema | Resuelve la consulta con el predicado de alcance aplicado y recupera el detalle |
| Puerto de repositorio de cuentas | Sistema | Aporta el alumno dueño de cada trabajo, para agrupar y filtrar |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Resuelve si un trabajo entra en el alcance del administrador |

El administrador es el sujeto de la regla.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien solicita.
- Para el detalle, aporta además el identificador del trabajo.

## 4. Flujo principal

1. El consumidor solicita el listado de la comisión declarando quién lo pide.
2. El caso de uso verifica que el papel sea `Administrador`. Si no lo es, termina en FA-01.
3. El caso de uso pide al puerto de repositorio los trabajos cuyo estado **no** es `Borrador`, aplicando el predicado de alcance que declara el dominio (RN-11).
4. El puerto devuelve, por cada trabajo, su identificador, su nombre, su fecha, su estado, su dueño y el recuento de observaciones, **sin los componentes de las piezas**.
5. El caso de uso devuelve el listado con el dato de dueño que permite agrupar y filtrar por alumno.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien solicita no tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, sin consultar el repositorio. La consulta del alumno sobre sus propios trabajos es CU-06 | Termina el caso de uso |
| FA-02 | El consumidor solicita el listado **filtrado** por un alumno | El caso de uso traslada el filtro al puerto de repositorio, que lo resuelve junto con el predicado de alcance. El recorte de los borradores sigue rigiendo dentro del filtro | Paso 5 |
| FA-03 | El consumidor solicita el **detalle** de un trabajo | El caso de uso consulta al dominio si ese trabajo entra en el alcance. Si procede, devuelve los datos, el texto original, las piezas con sus componentes y las observaciones: los mismos cuatro elementos que ve el alumno | Termina el caso de uso |
| FA-04 | El detalle pedido está en estado `Borrador` | El caso de uso devuelve no procede con el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | El papel declarado no es `Administrador` | No consulta nada. Es una negativa por facultad, distinta de la negativa por pertenencia de CU-06 |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | El trabajo pedido está en `Borrador` | Devuelve no procede. A diferencia del motivo de pertenencia, éste no oculta la existencia del trabajo: expresa que está fuera de su flujo de trabajo |
| `TRABAJO_INEXISTENTE` | El identificador no corresponde a ningún trabajo | Devuelve no procede. Acá no hay recurso ajeno que proteger: el administrador ve todo lo que no es borrador |

Las tres son consultas y no modifican nada.

## 7. Postcondiciones

- **Éxito, listado:** el resultado contiene todos los trabajos de la comisión en los tres estados que el administrador ve, con su dueño y sin componentes de piezas.
- **Éxito, detalle:** el resultado contiene un trabajo que no está en `Borrador`, con sus cuatro elementos.
- **Fallo:** ningún trabajo en `Borrador` se devuelve ni se cuenta.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno A con 1 trabajo en `Borrador` y 1 en estado `Pendiente`, y un alumno B con 1 en `Finalizado` | El administrador solicita el listado de la comisión | El caso de uso devuelve 2 trabajos: el que está en estado `Pendiente` y el `Finalizado`, y ninguno en `Borrador` |
| CA-02 | El mismo repositorio | El administrador solicita el listado filtrado por el alumno A | El caso de uso devuelve 1 trabajo, el que está en estado `Pendiente`, y 0 borradores |
| CA-03 | El mismo repositorio y un solicitante con papel `Alumno` | El consumidor solicita el listado de la comisión | El caso de uso devuelve el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y el repositorio registra 0 consultas |
| CA-04 | Un trabajo del alumno A en `Borrador` | El administrador solicita su detalle por identificador | El caso de uso devuelve el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` |
| CA-05 | Un trabajo del alumno A en estado `Pendiente` con 3 piezas y 2 advertencias | El administrador solicita su detalle | El caso de uso devuelve los 4 elementos —datos, texto original, 3 piezas con sus componentes y 2 advertencias—, los mismos que ve el alumno A |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-07, y NB-09 en cuanto entrega la lista de trabajos en estado `Pendiente` sobre los que se ejerce el desenlace |
| Reglas de negocio aplicables | [RN-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md), [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) |
| Casos de uso de dominio orquestados | [CU-11](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-11-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) |
| Puertos que consume | Repositorio de trabajos, repositorio de cuentas |
| Historias de usuario a generar en 06 | US-20, US-21, US-22 |
| Componentes esperados en 05 | Caso de uso de listado de la comisión con su filtro, y caso de uso de detalle para el administrador |
| Tests previstos en 08 | Unitarias con repositorio simulado: recorte de borradores en el listado y dentro del filtro, ausencia de componentes en el listado, negativa por facultad y detalle de un borrador |

## 10. Notas y supuestos

- **El predicado de alcance lo declara el dominio y la consulta la ejecuta el repositorio**: esta capa es la que los junta. El dominio no conoce el conjunto de trabajos.
- **El recorte de los borradores se traslada al puerto**, no se aplica después sobre un conjunto mayor: un borrador que llega a esta capa y se descarta acá ya viajó, y el criterio del listado es que no aparezca.
- La agrupación y el orden en la pantalla son decisiones de presentación y viven en `03-UX-UI-DX`; acá se entrega el dato de dueño que las hace posibles.
- El recuento por alumno y por estado del panel de resumen es una capacidad de prioridad menor, con plazo posterior, y no forma parte de este contrato.
- El desenlace y la eliminación no están acá: son CU-08 y CU-09.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |

## 17. Compatibilidad de la superficie pública

Agregar criterios de filtro es compatible. Incluir los trabajos en `Borrador` en el alcance del administrador contradice RN-11 y es un cambio de alcance que exige decisión del Product Owner.
