# CU-11 — Resolver el alcance del administrador sobre un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-11-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §1 y §5 (retiro de trabajos por el administrador); [`NB-07`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) §5 (alcance de la vista del administrador); `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-12 y F-24), §4.1 (RN-04 y RN-11), §4.2 (tabla de quién puede qué en cada estado), §17.1.P.2 (INV-03 e INV-07), §6 (flujo 2.1)
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

Responder si un trabajo concreto entra en el alcance del administrador y si él puede eliminarlo. Son las dos caras de la misma pregunta: el administrador **no ve los trabajos en `Borrador`**, y **elimina cualquiera de los que sí ve, en cualquier estado**. Es el contrato simétrico de CU-09, que responde lo mismo para el alumno.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Consulta el alcance antes de incluir un trabajo en una vista de revisión o de eliminarlo |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Ejecuta la consulta y la eliminación efectiva sólo sobre lo que el alcance admitió |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Resuelve, sobre un trabajo, si está dentro del alcance del administrador y si admite eliminación |

El administrador es el sujeto de la regla. El actor del contrato es el código consumidor.

## 3. Precondiciones

- El trabajo existe y su estado pertenece al conjunto cerrado de cuatro valores.
- El consumidor declara que quien solicita tiene papel `Administrador`.
- La operación pertenece al conjunto ver, eliminar.

## 4. Flujo principal

1. La capa de aplicación consulta si un trabajo entra en el alcance del administrador para una operación.
2. El dominio comprueba que el papel declarado sea `Administrador`.
3. El dominio comprueba que el estado del trabajo **no** sea `Borrador` (RN-11).
4. El dominio devuelve que la operación procede, para ver y para eliminar por igual: la eliminación por parte del administrador alcanza cualquier estado que él ve (RN-04).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El trabajo consultado está en estado `Borrador` | El dominio devuelve **no procede** con el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`: los borradores no forman parte de su flujo de trabajo, ni para verlos ni para eliminarlos | Termina el caso de uso |
| FA-02 | El administrador elimina un trabajo en estado `Finalizado` o `Rechazado` | Procede. La terminalidad de esos dos estados impide que el trabajo cambie de estado o de contenido (INV-07), no que el administrador lo retire. Es lo que permite limpiar los intentos que ya no hacen falta | Paso 4 |
| FA-03 | El administrador elimina un trabajo en estado `Pendiente`, antes de darle desenlace | Procede: los tres estados que ve admiten eliminación. El trabajo desaparece sin desenlace | Paso 4 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | El trabajo está en estado `Borrador` | Devuelve no procede. A diferencia del motivo equivalente de CU-09, éste no oculta la existencia del trabajo: expresa que está fuera del flujo de trabajo del administrador |
| `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` | El papel declarado no es `Administrador` | Devuelve no procede sin evaluar el estado. La pregunta por el alcance del alumno es CU-09 |
| `OPERACION_DESCONOCIDA` | La operación consultada no pertenece al conjunto declarado | Devuelve no procede |

La consulta no modifica nada en ningún caso.

## 7. Postcondiciones

- **Éxito:** el resultado es procede, o no procede con exactamente un motivo. El trabajo no cambia.
- **Fallo:** no hay caso de fallo propio; la consulta siempre devuelve un resultado.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con 1 trabajo en estado `Borrador` y 1 en estado `Pendiente` | La capa de aplicación consulta el alcance del administrador sobre los 2 | El dominio devuelve que procede sobre 1 de 2: el que está en estado `Pendiente` |
| CA-02 | Un trabajo en estado `Borrador` | La capa de aplicación consulta si el administrador puede eliminarlo | El dominio devuelve no procede con el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` |
| CA-03 | Un trabajo en estado `Finalizado` | La capa de aplicación consulta si el administrador puede eliminarlo | El dominio devuelve que procede: la terminalidad no impide el retiro |
| CA-04 | Un trabajo en estado `Pendiente` | La capa de aplicación consulta si un solicitante con papel `Alumno` puede eliminarlo por esta vía | El dominio devuelve no procede con el motivo `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` |
| CA-05 | Los 3 estados que el administrador ve: `Pendiente`, `Finalizado` y `Rechazado` | La capa de aplicación consulta la eliminación sobre uno de cada estado | El dominio devuelve que procede en los 3 de 3 |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-09 en su criterio de retiro de trabajos por el administrador, y NB-07 en su criterio de alcance de la vista |
| Reglas de negocio aplicables | [RN-11](../Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md), [RN-04](../Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) |
| Invariantes | INV-03, por complemento: el recorte de INV-03 a la eliminación por parte de un alumno es lo que deja lugar a este alcance. INV-07, en cuanto la terminalidad no impide el retiro |
| Historias de usuario a generar en 06 | US de exclusión de los borradores del alcance del administrador, US de eliminación por el administrador en los tres estados que ve |
| Componentes esperados en 05 | Predicado de alcance sobre la entidad de trabajo, que las consultas de listado consumen |
| Tests previstos en 08 | Pruebas unitarias del predicado en los cuatro estados y para los dos papeles. La verificación de que el listado del administrador sobre un alumno con un borrador y un trabajo en estado `Pendiente` devuelve sólo el segundo pertenece a las pruebas de la capa que hace la consulta |

## 10. Notas y supuestos

- **El dominio no ejecuta consultas ni arma listados**: no conoce el conjunto de trabajos. Lo que declara acá es el **predicado** que decide, trabajo por trabajo, si entra en el alcance del administrador; la consulta que lo aplica sobre el conjunto vive en `GeometriaFactory-Application` y en `GeometriaFactory-Infrastructure`. Por eso RN-11 es, en el vocabulario del intake, una regla de alcance de consulta y no una condición permanente sobre los datos: no tiene invariante asociado.
- El desenlace del trabajo no es parte de este contrato: es CU-10.
- La baja de una cuenta de alumno, que arrastra todos sus trabajos cualquiera sea su estado, tampoco: es CU-02.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Nace con el circuito de revisión que `PRODUCT-INTAKE` 1.3 incorporó: recoge **RN-11**, que no existía, y la mitad de **RN-04** que la ampliación del 2026-08-08 agregó —el administrador elimina cualquier trabajo que ve, en cualquier estado—. Es el contrato simétrico de CU-09, que la versión anterior de esta categoría resolvía sólo para el alumno. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |

## 17. Compatibilidad de la superficie pública

Incluir los borradores en el alcance del administrador, o acotar su eliminación a algunos estados, son cambios de alcance que contradicen RN-11 y RN-04: suben versión mayor y exigen decisión del Product Owner.
