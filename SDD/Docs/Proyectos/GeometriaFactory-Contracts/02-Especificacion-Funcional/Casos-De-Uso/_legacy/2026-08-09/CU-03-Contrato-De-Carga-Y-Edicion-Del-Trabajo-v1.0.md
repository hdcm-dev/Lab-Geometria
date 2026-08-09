> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md`](../../CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-03 — Contrato de carga y edición del trabajo

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-03-Contrato-De-Carga-Y-Edicion-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` §1, §5; `NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5; `00-Contexto/Vision-Producto.md` §9.1 (Trabajo, Pieza); `00-Contexto/Alcance-Producto.md` §4.1 (F-06, F-07) y §5 (X-4); `PRODUCT-INTAKE` §17.4 P.2, P.3, P.5 y P.11 (decisión 2), §17.5 P.3, §4 (F-06, F-07), §6 (flujo 2), §7 (CL-3), §20.E-2
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

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
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar los tipos de transferencia con los que un trabajo del alumno se crea, se reedita y se elimina a través de la frontera entre las dos piezas desplegables. La decisión central de este caso de uso es que **el texto original del trabajo viaja como una sola cadena, sin interpretarse en el contrato**: el contrato no declara piezas, ni componentes, ni valores derivados en la solicitud. La interpretación es responsabilidad de la pieza de datos.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma la solicitud de alta o de edición con lo que la persona cargó, y consume el resultado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Recibe la cadena tal cual y produce el resultado sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara el campo de texto original como cadena y el conjunto cerrado de estados del trabajo |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-01.
- El contrato declara el conjunto cerrado de estados del trabajo: `Borrador`, `Pendiente` y `Finalizado`.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de alta de trabajo con cuatro campos: nombre, fecha, descripción y texto original.
2. El código de la pieza pública asigna al campo de texto original **la cadena exacta** que la persona pegó, sin normalizarla, sin reordenarla y sin quitarle caracteres.
3. El código de la pieza pública envía la solicitud a la pieza de datos.
4. El código de la pieza de datos responde con el resultado de alta, que trae el identificador propio del trabajo, su estado y su fecha de registro.
5. El código de la pieza pública arma la solicitud de edición con el identificador y los mismos cuatro campos, para volver sobre un trabajo ya guardado.
6. El código de la pieza de datos responde con el resultado de edición, con el estado resultante del trabajo.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El texto original está incompleto o no se puede interpretar y la persona igual quiere guardar | El contrato admite la solicitud sin cambio alguno: el campo de texto original es una cadena y el contrato no le impone forma. El resultado devuelve estado `Borrador` y las observaciones de CU-05 asociadas | El flujo vuelve al paso 5 cuando la persona reedita |
| FA-02 | La persona elimina un trabajo suyo que está en `Borrador` | El contrato usa la solicitud de eliminación, con un único campo: el identificador del trabajo | El flujo termina; el trabajo deja de aparecer en el listado de CU-04 |
| FA-03 | La persona finaliza el trabajo | El contrato usa la solicitud de finalización, con el identificador del trabajo. El resultado devuelve el estado alcanzado y las observaciones que lo impidieron, si las hubo | El flujo vuelve al paso 5 si la finalización no procede |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud llega sin nombre, sin fecha o sin texto original | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación por corrección y reintento |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a un trabajo del solicitante, o no existe | Respuesta de error de CU-06 con texto neutro que **no distingue** el caso de trabajo ajeno del de trabajo inexistente. Terminación controlada |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | Se pide eliminar un trabajo que no está en `Borrador` | Respuesta de error de CU-06 que declara el estado actual del trabajo. Terminación controlada |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | Se pide finalizar un trabajo cuyo texto original tiene errores de validación | Respuesta de error de CU-06 acompañada de las observaciones de CU-05, con índice de figura y campo. Handoff: el trabajo permanece en `Borrador` |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene el identificador y el estado resultante del trabajo, y el texto original que envió es idéntico carácter por carácter al que la persona pegó.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06; el contrato no transporta ninguna versión reescrita del texto original.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de alta de trabajo | Se inspecciona su superficie pública | El texto original está declarado como **una sola cadena**, y el tipo declara 0 campos de pieza, de componente, de valor derivado y de observación |
| CA-02 | El texto del escenario E-2 del intake —un ortoedro con dos comas finales y la clave `Tapas`— | El código de la pieza pública arma la solicitud de alta con ese texto | El campo de texto original transporta las dos comas finales y la clave `Tapas` sin modificación: 0 caracteres alterados respecto del original |
| CA-03 | Un trabajo en estado `Pendiente` | El código de la pieza pública arma la solicitud de eliminación con su identificador | La respuesta es el tipo de error de CU-06 con código `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` y declara el estado `Pendiente` |
| CA-04 | El trabajo de otra persona, cuyo identificador se conoce | El código de la pieza pública pide su edición | La respuesta es el tipo de error de CU-06 con código `CONTRATO_TRABAJO_NO_ENCONTRADO`, con el mismo texto que produce un identificador inexistente |
| CA-05 | Un trabajo cuyo texto original es el escenario E-5 del intake, con una figura de tipo desconocido en la posición 1 | El código de la pieza pública pide la finalización | La respuesta es el tipo de error de CU-06 con código `CONTRATO_TEXTO_NO_INTERPRETABLE`, acompañado de una observación con índice de figura 1 y campo `Tipo`, y el trabajo queda en `Borrador` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, y NB-04 por el límite entre guardar y finalizar |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican, todas de `GeometriaFactory-Domain`, [`RN-08`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) sobre CA-02, [`RN-04`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) sobre CA-03, [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre CA-04 y [`RN-05`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) sobre CA-05. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-06 tipos de alta y de edición de trabajo; US-07 tipos de eliminación y de finalización |
| Componentes esperados en 05 | Familia de tipos de transferencia de trabajo del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración de alta con el texto de E-2 sin alteración, de eliminación fuera de `Borrador` forzando la petición al servicio, de edición de un trabajo ajeno y de finalización bloqueada con E-5 |

## 10. Notas y supuestos

- El contrato no interpreta el texto: no tolera ni rechaza comas finales, ni resuelve claves. Toda la tolerancia pertenece a `GeometriaFactory-Infrastructure` y a `GeometriaFactory-Domain`.
- El contrato tampoco declara ninguna forma de corregir el texto del alumno: la edición del texto desde el producto está excluida (X-4 de `Alcance-Producto.md` §5). Lo que la solicitud de edición reemplaza es el texto entero que la persona vuelve a pegar.
- Supuesto de alcance: el contrato no declara carga de archivos; el texto llega como cadena dentro de la misma solicitud.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los tipos de alta, edición, eliminación y finalización del trabajo, con el texto original como cadena no interpretada en el contrato. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-03`, `RN-04`, `RN-05` y `RN-08` de `GeometriaFactory-Domain`, cada una contra el criterio de aceptación que sostiene, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Cambiar el campo de texto original de cadena a una estructura interpretada es el cambio incompatible de mayor impacto de todo el ensamblado: contradice la decisión pre-tomada de `PRODUCT-INTAKE` §17.4 P.11 y obliga a rehacer los dos extremos.
- Agregar un estado al conjunto cerrado del trabajo se trata como incompatible: la pieza pública deja de cubrir todos los casos aunque compile.
- Agregar un campo opcional a la solicitud de alta es compatible.
