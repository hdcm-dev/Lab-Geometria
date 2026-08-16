> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md`](../../CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-09 — Resolver el acceso de un alumno a un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5 (separación entre alumnos y acotación de la eliminación); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-5), §4 (F-07), §17.2.P.5, §17.5.P.5, §17.5.P.6
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
- [12. Compatibilidad de la superficie pública](#12-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Responder si una persona puede operar sobre un trabajo concreto y de qué manera: leerlo, reeditarlo o eliminarlo. Reúne dos condiciones que el producto trata como una sola pregunta —la pertenencia del trabajo a su dueño y la acotación de la eliminación al estado `Borrador`— porque las dos deciden lo mismo: si la operación procede.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Consulta si la operación procede antes de ejecutarla |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Ejecuta la lectura o la eliminación sólo cuando la consulta la admitió |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Resuelve la pertenencia y la acotación de la eliminación |

El alumno y el administrador son sujetos de la regla. La verificación ocurre del lado del servidor y no ocultando un control en la pantalla (`NB-03` §4).

## 3. Precondiciones

- El trabajo existe y tiene dueño.
- Se conoce la identidad del alumno que solicita la operación y su papel.
- La operación pertenece al conjunto leer, reeditar, eliminar.

## 4. Flujo principal

1. La capa de aplicación consulta si un alumno puede ejecutar una operación sobre un trabajo.
2. El dominio compara la identidad del solicitante con la del dueño del trabajo.
3. Si coinciden, el dominio comprueba las condiciones propias de la operación.
4. Para la eliminación, el dominio comprueba que el estado del trabajo sea `Borrador`.
5. El dominio devuelve que la operación procede.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El solicitante no es el dueño del trabajo | El dominio devuelve **no procede** con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que es deliberadamente indistinguible de la inexistencia: no se confirma la existencia de un trabajo ajeno (RN-03, INV-02) | Termina el caso de uso |
| FA-02 | El solicitante tiene papel `Administrador` | La revisión de los trabajos de la comisión la resuelven las consultas de la capa de aplicación, no la pertenencia del dominio. Este caso de uso responde sobre la pertenencia y no sobre el papel, y por eso no concede acceso por papel | Termina el caso de uso, y la decisión queda del lado del consumidor |
| FA-03 | El dueño solicita eliminar un trabajo en `Pendiente` o `Finalizado` | El dominio devuelve no procede con el motivo `ELIMINACION_FUERA_DE_BORRADOR`. Es un motivo distinto del anterior porque acá la existencia del trabajo ya está admitida para su dueño | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | El solicitante no es el dueño | Devuelve no procede. El consumidor debe traducirlo a «no encontrado» y nunca a «no autorizado», para no confirmar la existencia del recurso ajeno |
| `ELIMINACION_FUERA_DE_BORRADOR` | El dueño intenta eliminar fuera del estado `Borrador` | Devuelve no procede y el trabajo queda intacto |
| `OPERACION_DESCONOCIDA` | La operación consultada no pertenece al conjunto declarado | Devuelve no procede sin evaluar la pertenencia |

Ninguno de los tres tiene efecto sobre el trabajo: la consulta no modifica nada.

## 7. Postcondiciones

- **Éxito:** el resultado es procede, o no procede con exactamente un motivo. El trabajo no cambia en ningún caso.
- **Fallo:** no hay caso de fallo propio; la consulta siempre devuelve un resultado.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Borrador` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede eliminarlo | El dominio devuelve que procede |
| CA-02 | Un trabajo en estado `Borrador` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno B puede leerlo | El dominio devuelve no procede con el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, indistinguible de la inexistencia |
| CA-03 | Un trabajo en estado `Finalizado` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede eliminarlo | El dominio devuelve no procede con el motivo `ELIMINACION_FUERA_DE_BORRADOR` |
| CA-04 | Un trabajo en estado `Pendiente` cuyo dueño es el alumno A | La capa de aplicación consulta si el alumno A puede reeditarlo | El dominio devuelve no procede: la reedición está acotada al borrador por CU-05 |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03 |
| Reglas de negocio aplicables | [RN-03](../../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) |
| Invariantes | INV-02, y el aspecto de la pertenencia que INV-03 nombra sin enunciar (ver [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §4.2) |
| Historias de usuario a generar en 06 | US de separación de trabajos entre alumnos, US de eliminación acotada al borrador |
| Componentes esperados en 05 | Resolución de pertenencia sobre la entidad de trabajo, con su enumeración cerrada de motivos |
| Tests previstos en 08 | Pruebas unitarias de pertenencia y de acotación. La verificación equivalente forzando la petición al servicio de datos, y no sólo desde la pantalla, es criterio bloqueante declarado y pertenece a las pruebas de integración de `GeometriaFactory-Api` |

## 10. Notas y supuestos

- El dominio devuelve un motivo; **la traducción a «no encontrado»** hacia afuera del proceso es responsabilidad de `GeometriaFactory-Api`. Acá se declara la equivalencia para que ninguna capa la invente.
- La autorización por papel no reemplaza a la pertenencia: son dos comprobaciones distintas y la segunda es la que este caso de uso sostiene.
- La eliminación efectiva del dato la ejecuta la infraestructura; el dominio decide si procede.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

Distinguir hacia afuera el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` de una inexistencia real sería un cambio incompatible con RN-03, aunque no rompiera ninguna compilación. Agregar una operación al conjunto consultado es compatible mientras las existentes conserven su semántica.
