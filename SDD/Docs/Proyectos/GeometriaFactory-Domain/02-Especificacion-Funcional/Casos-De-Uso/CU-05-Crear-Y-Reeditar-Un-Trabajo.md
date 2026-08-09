# CU-05 — Crear y reeditar un trabajo con dueño e identidad propia

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-05-Crear-Y-Reeditar-Un-Trabajo.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §1, §4 y §5; [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (conservación del original y acción única de guardado); `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-06, F-07 y F-22), §4.1 (RN-04 y RN-08), §4.2 (modelo de estados del trabajo), §17.1.P.2 (INV-02), §9 (X-4), §12 (definición de trabajo), §17.1.P.11
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

Constituir un trabajo con dueño, identificador propio, nombre, fecha, descripción y el texto original del alumno, en estado `Borrador`, y admitir su reedición mientras siga en ese estado. Es lo que convierte el esfuerzo de la Actividad 1 en una unidad con existencia propia en lugar de un texto en un portapapeles.

La resolución del estado que sigue a la carga **no ocurre acá**: el alumno tiene una sola acción de guardado, enviar, y es el envío el que interpreta el texto y decide si el trabajo queda en `Borrador` o pasa a estado `Pendiente` (CU-08).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita la constitución o la reedición del trabajo |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa el trabajo fuera del dominio, con su texto original íntegro |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica los datos obligatorios, fija el estado inicial y conserva el texto original sin tocarlo |

El alumno es el sujeto de la regla: es quien carga y reedita. El actor del contrato es el código consumidor.

## 3. Precondiciones

- El dueño es un alumno existente. El trabajo no puede constituirse sin dueño (INV-02).
- Nombre y fecha están presentes. La descripción admite vacío.
- El texto original se aporta tal como el alumno lo pegó, sin ninguna transformación previa.
- Para la reedición, el trabajo está en estado `Borrador`, que es el único estado que el alumno edita (PRODUCT-INTAKE §4.2).

## 4. Flujo principal

1. La capa de aplicación solicita constituir un trabajo con dueño, nombre, fecha, descripción y texto original.
2. El dominio verifica que el dueño esté presente.
3. El dominio verifica que nombre y fecha estén presentes.
4. El dominio adopta el texto original **tal como lo recibe**, sin normalizarlo, sin reformatearlo y sin interpretarlo.
5. El dominio fija el estado en `Borrador`.
6. El dominio deja el conjunto de piezas vacío, el conjunto de observaciones vacío y el comentario del administrador sin valor.
7. El dominio devuelve el trabajo constituido.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno reedita un trabajo en `Borrador` | El dominio reemplaza nombre, fecha, descripción y texto original, **descarta** el conjunto de piezas y el de observaciones que hubiera de una interpretación anterior, y conserva el estado `Borrador`, el dueño y el identificador | Paso 7 |
| FA-02 | El texto original que se aporta no se puede interpretar como figuras | El dominio lo adopta igual. `Borrador` significa exactamente eso: que el texto todavía no verifica, o que el trabajo recién se creó (PRODUCT-INTAKE §4.2). El alumno corrige y vuelve a enviar cuantas veces haga falta | Paso 5 |
| FA-03 | Se solicita reeditar un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado` | El dominio lo rechaza: la reedición está acotada al borrador, y en los dos estados terminales el contenido tampoco cambia (INV-07) | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRABAJO_SIN_DUENO` | No se aporta dueño | Rechaza la constitución. Un trabajo sin dueño no es un trabajo |
| `DATO_OBLIGATORIO_AUSENTE` | Falta el nombre o la fecha | Rechaza la constitución |
| `REEDICION_FUERA_DE_BORRADOR` | Se reedita un trabajo que no está en `Borrador` | Rechaza la reedición y conserva el trabajo sin cambios |
| `TEXTO_ORIGINAL_ALTERADO` | El consumidor aporta un texto original que declara ser una versión corregida del que pegó el alumno | Rechaza la operación: el producto no edita el dato del alumno (RN-08) |

## 7. Postcondiciones

- **Éxito de la constitución:** existe un trabajo en estado `Borrador`, con dueño, identificador propio, nombre, fecha, descripción, texto original íntegro, 0 piezas, 0 observaciones y sin comentario del administrador.
- **Éxito de la reedición:** el trabajo conserva identificador, dueño y estado `Borrador`, tiene los datos nuevos y su interpretación anterior quedó descartada.
- **Fallo:** no hay efecto. El trabajo, si existía, queda exactamente como estaba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado` y los datos nombre `Entrega 1`, fecha 2026-08-09, descripción vacía y el texto del escenario E-2 | La capa de aplicación solicita constituir el trabajo | El dominio devuelve un trabajo en estado `Borrador`, con dueño, 0 piezas, 0 observaciones y el texto original idéntico carácter por carácter al aportado, con sus 2 comas finales |
| CA-02 | Un trabajo en estado `Borrador` con 3 piezas y 2 observaciones interpretadas del escenario E-1 | La capa de aplicación reedita el trabajo con un texto original nuevo | El dominio devuelve el trabajo con 0 piezas, 0 observaciones, el texto nuevo y el mismo identificador y dueño |
| CA-03 | Un trabajo en estado `Rechazado` | La capa de aplicación solicita reeditarlo | El dominio rechaza con el código `REEDICION_FUERA_DE_BORRADOR`: corregir un rechazo significa cargar un trabajo nuevo |
| CA-04 | Un trabajo en estado `Pendiente` | La capa de aplicación solicita reeditarlo | El dominio rechaza con el código `REEDICION_FUERA_DE_BORRADOR` |
| CA-05 | Los datos de un trabajo sin dueño | La capa de aplicación solicita constituirlo | El dominio rechaza con el código `TRABAJO_SIN_DUENO` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-04 en su criterio de conservación del original |
| Reglas de negocio aplicables | [RN-08](../Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-04](../Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) en cuanto al estado que el alumno edita y elimina, [RN-10](../Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) en cuanto a que el contenido de un trabajo terminal tampoco se reedita |
| Invariantes | INV-02 (el trabajo nace con dueño), INV-07 (los dos estados terminales no cambian de contenido) |
| Historias de usuario a generar en 06 | US de carga de trabajo, US de reedición en `Borrador` |
| Componentes esperados en 05 | Entidad de trabajo con su texto original y su conjunto cerrado de cuatro estados |
| Tests previstos en 08 | Pruebas unitarias de constitución, de reedición con descarte de la interpretación anterior y de los cuatro rechazos; comparación carácter por carácter del texto original con el escenario E-2 |

## 10. Notas y supuestos

- **No existe una acción de guardar sin enviar** (PRODUCT-INTAKE §4, F-22): la carga constituye el trabajo y el envío resuelve su estado. Este caso de uso cubre la constitución y la reedición; el envío es CU-08.
- El dominio **no interpreta** el texto original: sólo lo conserva. La interpretación se incorpora por CU-06 y la ejecuta el validador de figuras, que vive detrás de un puerto de la capa de aplicación.
- La fecha del trabajo es un dato que declara el alumno y no una lectura del reloj del sistema.
- El identificador propio del trabajo lo asigna el consumidor o el dominio según lo que decida 05; este caso de uso exige que exista, no cómo se genera.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el circuito de revisión de `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **§1 y §10** declaran que el guardado y el envío se unificaron en una sola acción y que la resolución del estado es de CU-08. **FA-02** reformula el borrador como «el texto todavía no verifica», que es lo que `Borrador` significa en el modelo nuevo. **FA-03 y CA-03** incorporan `Rechazado` y la terminalidad. **§7** suma el comentario del administrador sin valor al estado inicial. **§9 corrige la atribución de INV-04**, que la versión anterior citaba como el invariante del texto íntegro: RN-08 no tiene invariante asociado, y las invariantes que restringen a este caso de uso son INV-02 e INV-07. **Correcciones de la ronda r1 del audit**: hallazgo **P3-01**, §9 suma **RN-10**, que ya listaba a este caso de uso por la parte de la terminalidad que impide reeditar el contenido; hallazgo **P3-04**, la sección opcional se numera §17. |

## 17. Compatibilidad de la superficie pública

El texto original es parte del contrato y su tratamiento es no negociable: cualquier evolución que admita transformarlo rompe RN-08 y deja de ser compatible con lo que el producto promete. Agregar un dato opcional al trabajo es compatible; volverlo obligatorio, no.
