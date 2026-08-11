# CU-08 — Recorrer la entrega de la comisión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-08-Recorrer-La-Entrega-De-La-Comision.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §1, §5 (los siete criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md` §5 (primer criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-12, F-15), §4.1 (RN-03, RN-11), §4.2, §6 (flujo 2.1, flujo 3), §17.6 P.4
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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

---

## 1. Propósito

Darle al administrador un único lugar donde ver los trabajos de toda la comisión, agrupados y filtrados por alumno, **sin los que están en estado `Borrador`**, para que pueda recorrer la entrega de una sola vez sin pedirle nada a nadie.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Recorre el listado de la comisión, lo agrupa y lo filtra por alumno, y abre los trabajos que quiere revisar |
| Pieza pública | Sistema | Pide el listado con el alcance que corresponde al papel, lo agrupa y lo filtra, y encadena con la apertura del trabajo |
| Pieza de datos | Secundario | Devuelve la colección acotada a lo que el administrador ve, sin los trabajos en estado `Borrador` |

## 3. Precondiciones

- El administrador tiene sesión iniciada por CU-02 y su papel es el de administrador.
- Existen cuentas de alumno habilitadas con trabajos enviados.
- El alcance de lo que el administrador ve lo decide la pieza de datos, no la pantalla.

## 4. Flujo principal

1. El administrador abre su ruta inicial, que es el listado de la comisión.
2. **La pieza pública invoca desde su servidor el contrato de listado** de `GeometriaFactory-Contracts` CU-04, flujo principal, con el criterio de filtro por alumno sin poblar.
3. La pieza de datos devuelve la colección con los trabajos en estado `Pendiente`, `Finalizado` y `Rechazado` de toda la comisión, **nunca los que están en estado `Borrador`**, cada uno con identificador, nombre, fecha, estado, cantidad de piezas, cantidad de advertencias y datos de identificación del alumno dueño.
4. La pieza pública presenta la colección **agrupada por alumno**.
5. El administrador filtra por el alumno que quiere revisar. La pieza pública puebla el campo opcional de filtro y vuelve al paso 2.
6. El administrador abre un trabajo del listado, con lo que el flujo continúa en CU-07, FA-01.
7. Al volver, la pieza pública vuelve a pedir el listado, que ya refleja los estados actualizados.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador abre un trabajo en estado `Pendiente` para resolverlo | El listado le entrega el identificador. El desenlace se ejerce en CU-09, no desde el listado: **el elemento de listado no transporta ningún campo de decisión** | El flujo vuelve al paso 1, con el trabajo ya en un estado terminal |
| FA-02 | El administrador quiere ver quién todavía no entregó | El listado agrupado muestra sólo alumnos con trabajos visibles. La pieza pública lo declara explícitamente para que la ausencia de un alumno no se lea como un trabajo perdido | El flujo continúa en el paso 5 |
| FA-03 | No hay trabajos que satisfagan el filtro | El contrato devuelve la colección con cero elementos —señal `CONTRATO_LISTADO_VACIO`—. La pieza pública muestra un listado vacío explicado, **distinguible del estado degradado por el tipo recibido y no por el conteo** | El flujo vuelve al paso 5 |
| FA-04 | El administrador consulta el recuento por alumno y por estado | La pieza pública invoca el contrato de resumen de `GeometriaFactory-Contracts` CU-04, FA-04, con una fila por alumno y el recuento por cada uno de los tres estados. Es capacidad de prioridad menor, prevista para la etapa `i` | El flujo vuelve al paso 1 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | El filtro referencia un alumno que ya no existe, por ejemplo porque fue dado de baja en CU-04 | La pieza pública informa y recarga el listado sin filtro. Recuperación por reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno. **No se muestra ningún listado**, porque la pieza pública no guarda copia de los datos |
| `CONTRATO_ERROR_NO_CLASIFICADO` | Fallo que el contrato no previó | Handoff a CU-10, con el mismo tratamiento |

### 6.1 Señal declarada que no es error

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_LISTADO_VACIO` | No hay trabajos que satisfagan el filtro | Curso de FA-03: listado vacío explicado, nunca presentado como fallo |

## 7. Postcondiciones

- En caso de éxito: el administrador tiene a la vista los trabajos visibles de la comisión, agrupados por alumno, y puede filtrar por uno.
- En caso de fallo: no se muestra ningún listado con datos viejos y la persona ve el estado degradado.
- En ningún caso: aparece en el listado del administrador un trabajo en estado `Borrador`, ni el listado ofrece resolver un trabajo sin abrirlo.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con un trabajo en estado `Borrador` y otro en estado `Pendiente` | El administrador abre su listado | Ve sólo el que está en estado `Pendiente`: **cero** trabajos en estado `Borrador` en todo el listado |
| CA-02 | Tres alumnos con trabajos enviados | El administrador abre su listado | Los trabajos aparecen agrupados por alumno, y el filtro por alumno está disponible: **2 de 2** criterios de organización |
| CA-03 | Un listado con trabajos de tres alumnos | El administrador filtra por `alumno@ejemplo.test` | El listado muestra únicamente los trabajos de esa cuenta |
| CA-04 | El listado de la comisión | El administrador recorre la entrega completa | No necesita solicitar ningún envío, pedido ni archivo fuera del producto: **cero** solicitudes externas |
| CA-05 | Un trabajo en estado `Pendiente` en el listado | El administrador mira lo que el listado ofrece sobre ese trabajo | Puede abrirlo; el listado **no** ofrece aprobar ni rechazar sin abrirlo |
| CA-06 | Una comisión sin ningún trabajo enviado | El administrador abre su listado | Ve un listado vacío explicado, distinto del estado degradado |
| CA-07 | El servicio de datos detenido | El administrador abre su listado | La página sigue en pie con el estado degradado, y el mensaje no contiene ninguna dirección de servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-07`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md), [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) |
| Reglas de negocio aplicables | [`RN-11`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md), [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-04](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-04-Contrato-De-Listado-De-Trabajos.md) flujo principal, FA-02, FA-03 y FA-04, y su señal §6.1; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función. El listado no dibuja |
| Historias de usuario a generar en 06 | US-22, US-23 |
| Componentes esperados en 05 | Panel del administrador con el listado de la comisión, su agrupación y su filtro, y el panel de resumen de FA-04 |
| Tests previstos en 08 | Guion de demostración de la etapa `e` para el recorte del listado y la organización; guion de la etapa `i` para FA-04 |

## 10. Notas y supuestos

- El recorte que excluye los trabajos en estado `Borrador` lo decide la pieza de datos según el papel del solicitante. La pieza pública no filtra por su cuenta: si lo hiciera, la regla dependería de la pantalla, y el navegador no es confiable.
- La agrupación y el filtro son organización de la presentación y **sí** son responsabilidad de esta pieza; el alcance de lo que se organiza, no.
- El recuento de FA-04 es capacidad de prioridad menor. Se declara acá para que su lugar quede fijado, y su planificación pertenece a 07.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
