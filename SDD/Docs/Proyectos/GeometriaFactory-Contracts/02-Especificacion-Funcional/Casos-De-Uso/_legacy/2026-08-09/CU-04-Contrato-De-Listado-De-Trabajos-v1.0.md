> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-04-Contrato-De-Listado-De-Trabajos.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-04-Contrato-De-Listado-De-Trabajos.md`](../../CU-04-Contrato-De-Listado-De-Trabajos.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-04 — Contrato de listado de trabajos

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-04-Contrato-De-Listado-De-Trabajos.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §5 (tercer criterio); `NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §1, §5; `00-Contexto/Vision-Producto.md` §9.1; `00-Contexto/Alcance-Producto.md` §4.1 (F-08, F-12) y §4.2 (F-15); `PRODUCT-INTAKE` §17.4 P.3, P.5 y **P.10** (NFR estructural), §17.5 P.3, §4 (F-08, F-12, F-15), §6 (flujo 3)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
  - [6.1 Señales declaradas que no son error](#61-señales-declaradas-que-no-son-error)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar el tipo de transferencia con el que viaja un listado de trabajos, tanto el listado propio del alumno como el listado de toda la comisión que recorre el administrador. Este caso de uso existe sobre todo por su restricción: el elemento de listado es **una proyección deliberadamente pobre**, que no arrastra el texto original ni los componentes de las piezas de cada trabajo.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Solicita el listado, lo consume y lo agrupa o filtra para armar la pantalla correspondiente |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce la colección de elementos de listado, acotada a lo que el papel del solicitante permite |
| Ensamblado de contratos | Sistema | Declara la superficie del elemento de listado, que es donde vive el requisito estructural |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-01 y conoce el papel de la persona.
- El contrato ya declara los tipos de trabajo de CU-03 y el conjunto cerrado de estados.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de listado, que declara el criterio de filtro por alumno como campo opcional.
2. El código de la pieza pública envía la solicitud a la pieza de datos.
3. El código de la pieza de datos produce la colección de elementos de listado de trabajo, cada uno con identificador, nombre, fecha, estado, cantidad de piezas, cantidad de advertencias y datos de identificación del alumno dueño.
4. El código de la pieza pública recorre la colección para armar la lista de la persona, agrupando por alumno cuando el papel es de administrador.
5. El código de la pieza pública abre un elemento del listado invocando el contrato de detalle de CU-05 con el identificador del trabajo.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El papel de la persona es alumno | El contrato es el mismo. El campo de filtro por alumno se ignora y la pieza de datos devuelve únicamente los trabajos del solicitante | El flujo continúa en el paso 4 |
| FA-02 | El administrador filtra por un alumno concreto | El código de la pieza pública puebla el campo opcional de filtro con el identificador del alumno | El flujo continúa en el paso 3 |
| FA-03 | El administrador consulta el recuento por alumno y por estado | El contrato usa el tipo de resumen, con una fila por alumno y el recuento por cada uno de los tres estados. Es capacidad de prioridad menor (F-15) y su tipo se declara aparte del elemento de listado | El flujo vuelve al paso 1 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | El filtro por alumno referencia un identificador inexistente | Respuesta de error de CU-06 con texto neutro. Recuperación: reintento sin filtro |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado, que se distingue del listado vacío |

### 6.1 Señales declaradas que no son error

Se separan de la tabla anterior porque no producen respuesta de error y no forman parte del conjunto cerrado de códigos de error de CU-06.

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_LISTADO_VACIO` | No hay trabajos que satisfagan el filtro | El contrato devuelve la colección con cero elementos. La pieza pública distingue vacío de fallo por el tipo recibido, no por el conteo |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene una colección de elementos de listado en la que ningún elemento contiene el texto original ni los componentes de las piezas.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06, distinguible del listado vacío.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de elemento de listado de trabajo | Se inspecciona su superficie pública | Declara **0 campos** de texto original y **0 campos** de componente de pieza. Es el requisito estructural de `PRODUCT-INTAKE` §17.4 P.10, verificable por inspección |
| CA-02 | Un alumno con 3 trabajos: uno en `Borrador`, uno en `Pendiente` y uno en `Finalizado` | El código de la pieza pública solicita el listado propio | La colección trae 3 elementos y los 3 estados quedan distinguibles a partir del campo de estado, sin abrir ningún detalle |
| CA-03 | Una comisión con 2 alumnos, con 2 y 1 trabajos respectivamente | El administrador solicita el listado con el filtro por alumno vacío | La colección trae 3 elementos, cada uno con los datos de identificación de su alumno dueño, suficientes para agrupar por alumno sin una segunda solicitud |
| CA-04 | El trabajo del escenario semilla del intake, con 3 piezas y 2 advertencias | El código de la pieza pública solicita el listado | El elemento correspondiente trae cantidad de piezas 3 y cantidad de advertencias 2, y no trae ni una sola pieza ni una sola advertencia desarrollada |
| CA-05 | Una comisión sin ningún trabajo cargado | El administrador solicita el listado | La respuesta es la colección con 0 elementos y **no** un tipo de error: el listado vacío y el servicio no disponible son tipos distintos |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, NB-07 |
| Reglas de negocio aplicables | Ninguna en este proyecto de código. El alcance de lo que cada papel puede listar es invariante de `GeometriaFactory-Domain`; ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-08 tipo de elemento de listado de trabajo con la restricción estructural; US-09 tipos de solicitud de listado y de filtro por alumno; US-10 tipo de resumen por alumno y por estado |
| Componentes esperados en 05 | Familia de tipos de transferencia de listado del ensamblado de contratos |
| Tests previstos en 08 | Prueba de inspección de superficie pública para CA-01; pruebas de integración del listado propio, del listado de comisión con y sin filtro, y del listado vacío |

## 10. Notas y supuestos

- La agrupación y el filtro son operaciones de la pieza pública sobre la colección recibida, o de la pieza de datos sobre la consulta; el contrato sólo garantiza que los datos necesarios para agrupar por alumno están en cada elemento.
- El tipo de resumen de FA-03 corresponde a una capacidad de prioridad menor (F-15, etapa `h`). Se declara acá para que su alta posterior no obligue a rediseñar el elemento de listado, y su ausencia en el alcance comprometido no invalida ninguno de los criterios de aceptación de este caso de uso.
- El requisito estructural de CA-01 es del propio intake y se rotula ahí como asunción derivada; está completo y se usa como valor vigente.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el elemento de listado de trabajo como proyección sin texto original ni componentes, y el tipo de resumen por alumno y por estado. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-14**: `CONTRATO_LISTADO_VACIO`, que el propio texto declara que no es error, sale de la tabla de §6 y pasa a la subsección nueva §6.1 de señales declaradas que no son error, que adopta en 02 la resolución que el catálogo de 03 ya usaba. La decisión de diseño no cambia. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar el texto original o los componentes al elemento de listado compila sin error y **aun así se rechaza**: viola el requisito estructural de CA-01, que es el motivo por el que este tipo existe separado del detalle de CU-05.
- Agregar un campo de recuento al elemento de listado es compatible.
- Quitar los datos de identificación del alumno dueño es incompatible: rompe la agrupación del listado de la comisión.
