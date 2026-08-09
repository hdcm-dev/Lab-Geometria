> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-08-Gobernar-El-Estado-Del-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-08-Gobernar-El-Estado-Del-Trabajo.md`](../../CU-08-Gobernar-El-Estado-Del-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-08 — Gobernar el estado del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-08-Gobernar-El-Estado-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §5 (visibilidad del avance y cierre del circuito de entrega); [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (límite entre guardar y entregar); [`NB-05`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) §5 (carácter no bloqueante); `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-08), §7 (CL-3 y CL-4), §20.E-1, §20.E-2, §20.E-5
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

Admitir o rechazar las transiciones de estado de un trabajo —de `Borrador` a `Pendiente` y de `Pendiente` a `Finalizado`— haciendo cumplir la regla que separa guardar de entregar: un trabajo con al menos una observación de especie error de validación no se finaliza, y las advertencias no lo impiden.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita la transición de estado del trabajo |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa el nuevo estado fuera del dominio |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza la transición según la máquina de estados y las observaciones del trabajo |

## 3. Precondiciones

- El trabajo existe y su estado pertenece al conjunto `Borrador`, `Pendiente`, `Finalizado`.
- Para finalizar, las observaciones del trabajo ya fueron registradas por CU-07.

## 4. Flujo principal

1. La capa de aplicación solicita una transición de estado sobre el trabajo.
2. El dominio lee el estado actual.
3. El dominio comprueba que el par estado actual y transición solicitada figure en la tabla de transiciones admitidas.
4. Si la transición solicitada es la finalización, el dominio comprueba que el trabajo no tenga ninguna observación de especie error de validación.
5. El dominio aplica la transición.
6. El dominio devuelve el trabajo con su nuevo estado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El trabajo se finaliza teniendo advertencias | El dominio **admite** la finalización: las advertencias no bloquean, y es deliberado. El trabajo queda `Finalizado` con sus advertencias asociadas, que es el caso del escenario E-1 y del E-2 | Paso 5 |
| FA-02 | El trabajo se envía con un texto que todavía no se pudo interpretar | El envío es la transición de `Borrador` a `Pendiente` y ocurre igual; lo que no procede es la finalización, que se rechaza en el paso 4 | Paso 5, con destino `Pendiente` |
| FA-03 | El alumno vuelve a un trabajo `Pendiente` para corregirlo | Ninguna fuente declara una transición de vuelta desde `Pendiente` hacia `Borrador`. El dominio no la infiere y la rechaza | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRANSICION_DE_TRABAJO_NO_ADMITIDA` | El par estado actual y transición no figura en la tabla | Rechaza la operación y conserva el estado actual |
| `FINALIZACION_CON_ERRORES_DE_VALIDACION` | Se solicita finalizar un trabajo con al menos una observación de especie error de validación | Rechaza la finalización. El trabajo conserva su estado y sus observaciones, y sigue pudiendo guardarse como borrador |
| `FINALIZACION_SIN_INTERPRETACION` | Se solicita finalizar un trabajo cuyo texto original nunca fue interpretado | Rechaza la finalización: un trabajo finalizado exige texto interpretado sin errores |

## 7. Postcondiciones

- **Éxito:** el trabajo tiene el nuevo estado; su dueño, su texto original, sus piezas y sus observaciones no cambiaron.
- **Fallo:** el estado se conserva. Ningún rechazo altera el texto original ni descarta las observaciones ya registradas.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo en estado `Pendiente` con las 2 advertencias del escenario E-1 y 0 errores de validación | La capa de aplicación solicita finalizarlo | El dominio devuelve el trabajo en estado `Finalizado`, con sus 2 advertencias conservadas |
| CA-02 | Un trabajo en estado `Pendiente` con 1 observación de especie error de validación, la del escenario E-5 | La capa de aplicación solicita finalizarlo | El dominio rechaza con el código `FINALIZACION_CON_ERRORES_DE_VALIDACION` y el estado sigue siendo `Pendiente` |
| CA-03 | Un trabajo en estado `Borrador` con el texto del escenario E-5, que no se puede interpretar sin errores | La capa de aplicación solicita enviarlo | El dominio devuelve el trabajo en estado `Pendiente`: guardar y enviar no exigen interpretación sin errores; finalizar sí |
| CA-04 | Un trabajo en estado `Finalizado` | La capa de aplicación solicita volverlo a `Borrador` | El dominio rechaza con el código `TRANSICION_DE_TRABAJO_NO_ADMITIDA` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, NB-04, NB-05 |
| Reglas de negocio aplicables | [RN-05](../Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md), [RN-04](../Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) en cuanto al estado que admite eliminación |
| Invariantes | Ninguno de los declarados restringe específicamente esta transición; la restringen RN-05 y la máquina de estados de [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §5.2 |
| Historias de usuario a generar en 06 | US de envío del trabajo, US de finalización con advertencias, US de rechazo de finalización con errores |
| Componentes esperados en 05 | Máquina de transiciones de estado del trabajo dentro de la entidad de trabajo |
| Tests previstos en 08 | Pruebas unitarias de las transiciones admitidas y de las inadmisibles, con E-1 y E-2 como casos que finalizan con advertencias y E-5 como caso que no finaliza |

## 10. Notas y supuestos

- La transición de `Borrador` a `Pendiente` es lo que las fuentes llaman «enviar»; la de `Pendiente` a `Finalizado` es lo que expresa la entrega y la que alimenta la métrica de cierre del circuito didáctico.
- El estado no se deriva de las observaciones: es un atributo propio del trabajo, y las observaciones sólo condicionan una de las transiciones.
- La eliminación no es una transición de estado y por eso no vive acá: está en CU-09.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

Agregar un estado al conjunto cerrado o admitir una transición de vuelta desde `Finalizado` es un cambio de alcance de este caso de uso y de la máquina de estados del modelo: sube versión mayor y exige revisar RN-04 y RN-05.
