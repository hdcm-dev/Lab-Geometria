# CU-06 — Consultar el listado propio y operar sobre el borrador

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §1, §5 (segundo, tercero y cuarto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md` §5 (sexto criterio); `../../../../00-Contexto/Vision-Producto.md` §9.1 (estado del trabajo); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-07, F-08), §4.1 (RN-03, RN-04, RN-10), §4.2 (tabla de quién puede qué), §7 (CL-5, CL-10), §17.6 P.4
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

Darle al alumno un lugar donde ver todos sus trabajos con su estado —los cuatro del conjunto cerrado, incluidos los que quedaron en borrador—, volver sobre los que todavía puede editar, eliminarlos si quiere y enterarse de que un trabajo ya recibió su desenlace.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno | Primario | Recorre sus trabajos, abre uno, vuelve sobre un borrador o lo elimina |
| Pieza pública | Sistema | Pide el listado acotado al solicitante, ofrece sólo las acciones que el estado admite e invoca los contratos correspondientes |
| Pieza de datos | Secundario | Devuelve los trabajos del alumno y hace cumplir la pertenencia y la acotación de la eliminación |

## 3. Precondiciones

- El alumno tiene sesión iniciada por CU-02 y su papel es el de alumno.
- El conjunto de estados es cerrado y tiene cuatro valores: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado`. Los dos últimos son terminales.

## 4. Flujo principal

1. El alumno abre su ruta inicial, que es el listado de trabajos propios.
2. **La pieza pública invoca desde su servidor el contrato de listado** de `GeometriaFactory-Contracts` CU-04, FA-01, que devuelve únicamente los trabajos del solicitante, **incluidos los que están en estado `Borrador`**, que son suyos.
3. La pieza pública presenta cada trabajo con su nombre, su fecha, su estado, su cantidad de piezas y su cantidad de advertencias.
4. La pieza pública ofrece sobre cada trabajo sólo las acciones que su estado admite: abrir, siempre; volver sobre él y eliminarlo, únicamente en estado `Borrador`.
5. El alumno abre un trabajo, con lo que el flujo continúa en CU-07.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno vuelve sobre un trabajo en estado `Borrador` | La pieza pública lo abre para editarlo y el envío se ejerce con CU-05, FA-05 | El flujo vuelve al paso 1 con el estado que la interpretación haya decidido |
| FA-02 | El alumno elimina un trabajo suyo en estado `Borrador` | La pieza pública pide confirmación e invoca el contrato de eliminación de `GeometriaFactory-Contracts` CU-03, FA-02 | El flujo vuelve al paso 1, ya sin ese trabajo |
| FA-03 | El alumno tiene un trabajo con desenlace | El listado muestra el estado `Finalizado` o `Rechazado`, que es donde el alumno se entera del desenlace. **El comentario no viaja en el listado**: lo ve al abrir el trabajo, en CU-07 | El flujo continúa en el paso 5 |
| FA-04 | El alumno todavía no cargó ningún trabajo | El contrato devuelve la colección con cero elementos —señal `CONTRATO_LISTADO_VACIO`—. La pieza pública muestra un listado vacío explicado, **distinguible del estado degradado por el tipo recibido y no por el conteo** | El flujo termina hasta que el alumno cargue uno |
| FA-05 | El alumno quiere corregir un trabajo `Rechazado` | El estado es terminal: la pieza pública no ofrece editar ni eliminar. La única salida es cargar un trabajo nuevo por CU-05 | El flujo continúa en CU-05, paso 1 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El alumno pide por dirección directa un trabajo que no es suyo, o que no existe | La pieza pública muestra un mensaje neutro que **no distingue** el trabajo ajeno del inexistente, y devuelve al listado. No se confirma la existencia del recurso ajeno |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | El alumno fuerza la eliminación de un trabajo suyo que no está en estado `Borrador` | La eliminación no procede. La pieza pública declara el estado actual del trabajo y recarga el listado. Terminación controlada |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno. **No se muestra ningún listado**, porque la pieza pública no guarda copia de los datos |
| `CONTRATO_ERROR_NO_CLASIFICADO` | Fallo que el contrato no previó | Handoff a CU-10, con el mismo tratamiento |

### 6.1 Señal declarada que no es error

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_LISTADO_VACIO` | No hay trabajos que mostrar | Curso de FA-04: listado vacío explicado, nunca presentado como fallo |

## 7. Postcondiciones

- En caso de éxito: el alumno tiene a la vista todos sus trabajos con su estado, y sobre cada uno sólo las acciones que ese estado admite.
- En caso de eliminación: el trabajo dejó de existir y el listado ya no lo incluye.
- En caso de fallo: el listado no se muestra con datos viejos y la persona ve el estado degradado.
- En ningún caso: la pieza pública ofrece al alumno editar, eliminar o resolver un trabajo que no está en estado `Borrador`, ni muestra un trabajo que no le pertenece.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuatro trabajos, uno en cada estado del conjunto cerrado | Abre su listado | Los cuatro figuran y sus cuatro estados se distinguen entre sí |
| CA-02 | Un trabajo propio en estado `Borrador` | El alumno mira las acciones ofrecidas | Aparecen abrir, volver sobre él y eliminar |
| CA-03 | Un trabajo propio en estado `Pendiente` | El alumno mira las acciones ofrecidas | Aparece sólo abrir: ni editar ni eliminar |
| CA-04 | Un trabajo propio en estado `Rechazado` | El alumno fuerza la solicitud de eliminación contra la pieza de datos, sin usar la pantalla | La eliminación no procede y la respuesta declara el estado actual del trabajo |
| CA-05 | Un trabajo de otro alumno, cuyo identificador se conoce | El alumno lo pide por dirección directa | Recibe «no encontrado», nunca «no autorizado», y el listado no cambia |
| CA-06 | Un alumno sin ningún trabajo cargado | Abre su listado | Ve un listado vacío explicado, distinto del estado degradado |
| CA-07 | Un trabajo propio que el administrador aprobó | El alumno abre su listado | El estado figura como `Finalizado`, sin que el alumno tenga que abrir el trabajo para enterarse |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) |
| Reglas de negocio aplicables | [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [`RN-04`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) —cubre hoy los dos caminos de eliminación—, [`RN-10`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-04](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-04-Contrato-De-Listado-De-Trabajos.md) FA-01 y su señal §6.1; [`CU-03`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md) FA-02; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función. El listado no dibuja: la proyección que recibe no trae ni el texto original ni los componentes de las piezas |
| Historias de usuario a generar en 06 | US-15, US-16, US-17 |
| Componentes esperados en 05 | Panel del alumno con el listado propio y el diálogo de confirmación de eliminación |
| Tests previstos en 08 | Guion de demostración de la etapa `e` para el listado y la eliminación acotada; guion de la etapa `h` para CA-07 |

## 10. Notas y supuestos

- Ocultar el botón **no es** hacer cumplir la regla. El paso 4 acota lo que se ofrece, y CA-04 verifica que la acotación también se sostenga cuando la solicitud se fuerza sin pasar por la pantalla. La última defensa es la pieza de datos.
- `Rechazado` es terminal y el alumno no lo edita ni lo elimina: queda como registro del intento y sólo el administrador puede quitarlo, en CU-09 FA-03. Es una consecuencia aceptada por el Product Owner y no una limitación de este caso de uso.
- El listado es una proyección deliberadamente pobre y por eso es barato de recorrer. Todo lo demás del trabajo llega en CU-07.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
