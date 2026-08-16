> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-05-Crear-Y-Reeditar-Un-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-05-Crear-Y-Reeditar-Un-Trabajo.md`](../../CU-02005-Crear-Y-Reeditar-Un-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-05 — Crear y reeditar un trabajo con dueño e identidad propia

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-05-Crear-Y-Reeditar-Un-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §1, §4 y §5; [`NB-04`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (quinto criterio); `00-Contexto/Vision-Producto.md` §9.1; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-06 y F-07), §7 (CL-3), §9 (X-4), §12 (definición de trabajo), §17.1.P.11
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

Constituir un trabajo con dueño, identificador propio, nombre, fecha, descripción y el texto original del alumno, en estado `Borrador`, y admitir su reedición mientras siga en ese estado. Es lo que convierte el esfuerzo de la Actividad 1 en una unidad con existencia propia en lugar de un texto en un portapapeles.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita la constitución o la reedición del trabajo |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa el trabajo fuera del dominio, con su texto original íntegro |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica los datos obligatorios, fija el estado inicial y conserva el texto original sin tocarlo |

El alumno es el sujeto de la regla: es quien carga y reedita. El actor del contrato es el código consumidor.

## 3. Precondiciones

- El dueño es un alumno existente. El trabajo no puede constituirse sin dueño.
- Nombre y fecha están presentes. La descripción admite vacío.
- El texto original se aporta tal como el alumno lo pegó, sin ninguna transformación previa.
- Para la reedición, el trabajo está en estado `Borrador`.

## 4. Flujo principal

1. La capa de aplicación solicita constituir un trabajo con dueño, nombre, fecha, descripción y texto original.
2. El dominio verifica que el dueño esté presente.
3. El dominio verifica que nombre y fecha estén presentes.
4. El dominio adopta el texto original **tal como lo recibe**, sin normalizarlo, sin reformatearlo y sin interpretarlo.
5. El dominio fija el estado en `Borrador`.
6. El dominio deja el conjunto de piezas vacío y el conjunto de observaciones vacío.
7. El dominio devuelve el trabajo constituido.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno reedita un trabajo en `Borrador` | El dominio reemplaza nombre, fecha, descripción y texto original, **descarta** el conjunto de piezas y el de observaciones que hubiera de una interpretación anterior, y conserva el estado `Borrador`, el dueño y el identificador | Paso 7 |
| FA-02 | El texto original que se aporta no se puede interpretar como figuras | El dominio lo adopta igual: el borrador acepta texto que todavía no se puede interpretar, porque es el estado en el que el alumno pasa la mayor parte del tiempo (PRODUCT-INTAKE §7, CL-3) | Paso 5 |
| FA-03 | Se solicita reeditar un trabajo en estado `Pendiente` o `Finalizado` | El dominio lo rechaza: la reedición está acotada al borrador | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRABAJO_SIN_DUENO` | No se aporta dueño | Rechaza la constitución. Un trabajo sin dueño no es un trabajo |
| `DATO_OBLIGATORIO_AUSENTE` | Falta el nombre o la fecha | Rechaza la constitución |
| `REEDICION_FUERA_DE_BORRADOR` | Se reedita un trabajo que no está en `Borrador` | Rechaza la reedición y conserva el trabajo sin cambios |
| `TEXTO_ORIGINAL_ALTERADO` | El consumidor aporta un texto original que declara ser una versión corregida del que pegó el alumno | Rechaza la operación: el producto no edita el dato del alumno (RN-08) |

## 7. Postcondiciones

- **Éxito de la constitución:** existe un trabajo en estado `Borrador`, con dueño, identificador propio, nombre, fecha, descripción, texto original íntegro, 0 piezas y 0 observaciones.
- **Éxito de la reedición:** el trabajo conserva identificador, dueño y estado `Borrador`, tiene los datos nuevos y su interpretación anterior quedó descartada.
- **Fallo:** no hay efecto. El trabajo, si existía, queda exactamente como estaba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno habilitado y los datos nombre `Entrega 1`, fecha 2026-08-08, descripción vacía y el texto del escenario E-2 | La capa de aplicación solicita constituir el trabajo | El dominio devuelve un trabajo en estado `Borrador`, con dueño, 0 piezas, 0 observaciones y el texto original idéntico carácter por carácter al aportado, con sus 2 comas finales |
| CA-02 | Un trabajo en estado `Borrador` con 3 piezas y 2 observaciones interpretadas del escenario E-1 | La capa de aplicación reedita el trabajo con un texto original nuevo | El dominio devuelve el trabajo con 0 piezas, 0 observaciones, el texto nuevo y el mismo identificador y dueño |
| CA-03 | Un trabajo en estado `Finalizado` | La capa de aplicación solicita reeditarlo | El dominio rechaza con el código `REEDICION_FUERA_DE_BORRADOR` |
| CA-04 | Los datos de un trabajo sin dueño | La capa de aplicación solicita constituirlo | El dominio rechaza con el código `TRABAJO_SIN_DUENO` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-04 en su criterio de conservación del original |
| Reglas de negocio aplicables | [RN-08](../../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) |
| Invariantes | INV-02 (el trabajo nace con dueño), INV-04 |
| Historias de usuario a generar en 06 | US de carga de trabajo, US de guardado y reedición de borrador |
| Componentes esperados en 05 | Entidad de trabajo con su texto original y su conjunto cerrado de estados |
| Tests previstos en 08 | Pruebas unitarias de constitución, de reedición con descarte de la interpretación anterior y de los cuatro rechazos; comparación carácter por carácter del texto original con el escenario E-2 |

## 10. Notas y supuestos

- El dominio **no interpreta** el texto original: sólo lo conserva. La interpretación es CU-06 y la ejecuta el validador de figuras, que vive detrás de un puerto de la capa de aplicación.
- La fecha del trabajo es un dato que declara el alumno y no una lectura del reloj del sistema.
- El identificador propio del trabajo lo asigna el consumidor o el dominio según lo que decida 05; este caso de uso exige que exista, no cómo se genera.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

El texto original es parte del contrato y su tratamiento es no negociable: cualquier evolución que admita transformarlo rompe RN-08 y deja de ser compatible con lo que el producto promete. Agregar un dato opcional al trabajo es compatible; volverlo obligatorio, no.
