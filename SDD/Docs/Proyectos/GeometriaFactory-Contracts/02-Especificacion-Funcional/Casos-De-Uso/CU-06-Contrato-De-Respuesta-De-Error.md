# CU-06 — Contrato de respuesta de error

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-06-Contrato-De-Respuesta-De-Error.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5 (tercer criterio); `NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md` §1, §5 (cuarto criterio); `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §5 (tercer criterio); `00-Contexto/Vision-Producto.md` §9.1 (Fallo silencioso, Error de validación) y §7 R-03; `00-Contexto/Alcance-Producto.md` §8; `PRODUCT-INTAKE` 1.3 §4.1 (RN-05, RN-09, RN-10), §4.2, §7 (CL-3), §17.4 **P.5**, §14 (RA-03), §17.5 P.3 y P.5, §7 (CL-2, CL-5, CL-8), §20.E-5
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

Declarar el único tipo de transferencia con el que un fallo cruza la frontera entre las dos piezas desplegables. Es el caso de uso transversal del ensamblado: los otros seis lo referencian en lugar de declarar cada uno su propia forma de error. Su restricción central es la regla de arquitectura RA-03: el texto es neutro, lleva índice de figura y campo señalado cuando corresponde, y **nunca** la dirección del servicio que falló.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Recibe el tipo de error, decide qué mostrarle a la persona y cuándo pasar a estado degradado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el tipo de error con código, texto neutro y ubicación del defecto cuando la hay |
| Ensamblado de contratos | Sistema | Declara el tipo de error y el conjunto de códigos que los siete casos de uso usan |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El contrato declara un único tipo de error, compartido por todos los casos de uso del ensamblado.
- El conjunto de códigos de error es cerrado y está declarado en el propio contrato.

## 4. Flujo principal

1. El código de la pieza de datos detecta que una solicitud no puede satisfacerse.
2. El código de la pieza de datos instancia el tipo de error con cuatro campos: código, texto neutro, colección de detalles de ubicación y momento.
3. Cada detalle de ubicación trae el nombre del campo señalado y, cuando el fallo proviene de la interpretación del texto del alumno, el índice de figura.
4. El código de la pieza de datos verifica que el texto neutro no contenga direcciones de servicio, nombres de archivo de datos ni valores de secreto.
5. El código de la pieza pública recibe el tipo de error y decide la presentación: mensaje sobre el campo, aviso de situación de cuenta o estado degradado.
6. El código de la pieza pública nunca reenvía el tipo de error al navegador tal cual: lo traduce a su propia presentación, por RA-03.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El fallo es de forma de la solicitud y el contrato puede ubicarlo | La colección de detalles se puebla con un elemento por campo defectuoso, con el nombre del campo señalado | El flujo continúa en el paso 5, y el código de la pieza pública muestra el mensaje sobre el campo |
| FA-02 | El fallo es que la pieza de datos no responde | El tipo de error lo produce el propio código de la pieza pública, con el código `CONTRATO_SERVICIO_NO_DISPONIBLE` y la colección de detalles vacía | El flujo continúa en el paso 5, con estado degradado explícito |
| FA-03 | El fallo se refiere a un recurso ajeno al solicitante | El código y el texto son los mismos que para un recurso inexistente: el contrato no ofrece forma de distinguirlos | El flujo continúa en el paso 5 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Una solicitud llega incompleta | Detalle con el nombre del campo ausente y sin índice de figura. Recuperación por corrección y reintento |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | Recurso inexistente, ajeno, o fuera de lo que el solicitante ve —como un trabajo en estado `Borrador` pedido por el administrador— | Texto neutro, sin detalles. Terminación controlada |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | Se pide aprobar o rechazar un trabajo que no está en estado `Pendiente`, incluido el que ya está en un estado terminal | Texto neutro que declara el estado actual, sin detalles. Terminación controlada; el contrato no ofrece salida de un estado terminal |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | Quien pide el desenlace no es el administrador | Texto neutro, sin detalles. Terminación controlada |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | Una cuenta habilitada intenta ingresar antes de establecer su contraseña | Texto neutro con motivo, sin detalles. Handoff al contrato de establecimiento de contraseña de CU-02. Ver el fundamento en CU-01 §10 |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde o responde fuera de tiempo | Texto neutro, sin detalles y **sin** dirección del servicio. Handoff al estado degradado de la pieza pública |
| `CONTRATO_ERROR_NO_CLASIFICADO` | Un fallo que el contrato no previó | Texto neutro y código genérico. Es la garantía de que nunca llega a la persona un fallo sin representación en el contrato, que es la definición de fallo silencioso que el producto viene a eliminar |

## 7. Postcondiciones

- En caso de fallo representado: el código de la pieza pública tiene un código de un conjunto cerrado, un texto neutro y, cuando corresponde, la ubicación del defecto.
- En ningún caso: el tipo de error transporta direcciones de servicio interno, rutas de archivos de datos, valores de secreto ni trazas de la implementación.
- El contrato no tiene camino por el que un fallo llegue sin representación: `CONTRATO_ERROR_NO_CLASIFICADO` cierra el conjunto.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de error del contrato | Se inspecciona su superficie pública | Declara exactamente cuatro campos —código, texto, detalles y momento— y **0 campos** que puedan transportar una dirección de servicio, una ruta de archivo de datos o un valor de secreto |
| CA-02 | Una solicitud de envío de trabajo que llega sin el campo de nombre | La pieza de datos produce el error | El tipo de error trae código `CONTRATO_CAMPO_REQUERIDO_AUSENTE` y al menos un detalle con el campo señalado `Nombre`, y el texto no es genérico |
| CA-03 | Un canje de credenciales con la contraseña equivocada | La pieza de datos produce el error | El texto neutro no nombra ni el campo de correo ni el de contraseña: la respuesta no revela cuál de los dos falló |
| CA-04 | La pieza de datos detenida | El código de la pieza pública intenta cualquier solicitud | Recibe el tipo de error con código `CONTRATO_SERVICIO_NO_DISPONIBLE`, con 0 detalles y con un texto que no contiene ninguna dirección; el resultado es estado degradado y no una excepción sin manejar |
| CA-05 | Un alumno que pide el trabajo de otro, cuyo identificador conoce | La pieza de datos produce el error | El código y el texto son idénticos a los de un identificador inexistente: 0 campos permiten distinguir los dos casos |
| CA-06 | Un trabajo en estado `Rechazado` | El administrador pide su desenlace | El tipo de error trae código `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` y declara el estado actual `Rechazado`, con **0 campos** que sugieran una forma de revertirlo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-04, NB-08, NB-09 por los dos códigos del desenlace, y NB-02 por la explicación de la situación de la cuenta |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-09`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) —la regla que `PRODUCT-INTAKE` §17.4 P.5 ancla a este tipo, y que desde el modelo vigente se verifica sobre la observación del detalle de CU-05; ver §10—, [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre CA-05, [`RN-10`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) sobre CA-06 y los dos códigos nuevos, y [`RN-11`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) sobre la causa ampliada de `CONTRATO_TRABAJO_NO_ENCONTRADO`, las cuatro de `GeometriaFactory-Domain`. La regla de arquitectura RA-03 es de nivel producto, vive en `PRODUCT-INTAKE` §14 y su tratamiento arquitectónico pertenece a 05 |
| Historias de usuario a generar en 06 | US-14 tipo de error con texto neutro; US-15 detalle de ubicación con campo señalado e índice de figura; US-16 conjunto cerrado de catorce códigos de error |
| Componentes esperados en 05 | Tipo de transferencia de error del ensamblado de contratos, transversal a las demás familias |
| Tests previstos en 08 | Prueba de inspección de superficie pública para CA-01; pruebas de integración de solicitud incompleta (CA-02), de credencial inválida (CA-03), de servicio detenido (CA-04), de recurso ajeno (CA-05) y de desenlace sobre estado terminal (CA-06) |

## 10. Notas y supuestos

- El tipo de error es el mismo para los siete casos de uso del ensamblado. Un tipo de error por familia multiplicaría los lugares donde se puede filtrar una dirección de servicio, que es exactamente lo que RA-03 evita.
- El contrato no fija el código de estado de la respuesta del servicio: eso pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.5).
- Este caso de uso no describe cómo se presenta el estado degradado a la persona: eso pertenece a `GeometriaFactory-Web` y a la categoría 03.
- El código `CONTRATO_SERVICIO_NO_DISPONIBLE` es el único que el contrato admite que produzca la propia pieza pública, porque describe la ausencia de respuesta de la otra pieza.
- **El conjunto cerrado tiene catorce códigos.** Es la unión de los que declaran los siete casos de uso. Respecto de la versión anterior entran dos —`CONTRATO_ESTADO_NO_PERMITE_DESENLACE` y `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR`, los dos derivados de RN-10 y del modelo de estados de `PRODUCT-INTAKE` §4.2— y sale uno, `CONTRATO_TEXTO_NO_INTERPRETABLE`. **Tres señales declaradas** quedan deliberadamente fuera del conjunto, en las subsecciones §6.1 de CU-03, CU-04 y CU-05, porque no producen respuesta de error.
- **Por qué salió `CONTRATO_TEXTO_NO_INTERPRETABLE`.** Con el envío como acción única de guardado, un texto que no verifica ya no hace fallar ninguna operación: el envío procede y el trabajo queda en estado `Borrador` con sus observaciones (`PRODUCT-INTAKE` §4.2 y §7 CL-3). No quedó ninguna operación que pueda fallar por ese motivo, así que el código dejó de ser un código de error y pasó a ser una señal declarada en CU-03 §6.1 y en CU-05 §6.1.
- **Dónde quedó el índice de figura.** El tipo de error conserva en su detalle de ubicación la capacidad de transportarlo, porque `PRODUCT-INTAKE` §17.4 P.5 se la exige «cuando corresponde». Desde el modelo vigente **ningún código del conjunto lo usa**: los defectos de interpretación del texto del alumno viajan como observaciones del detalle de CU-05, que sí llevan índice de figura y campo señalado, y es ahí donde `RN-09` se verifica. La capacidad se conserva y no se ejerce; declararlo evita que 05 la elimine por parecer muerta o que 08 la busque en el lugar equivocado.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el tipo único de respuesta de error, con texto neutro, conjunto cerrado de códigos y detalle de ubicación con índice de figura y campo, y la prohibición de transportar direcciones de servicio interno. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-02**, por arrastre de CU-01: se incorpora la fila del código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` en §6 y §10 declara que el conjunto cerrado pasa a trece códigos, con las dos señales declaradas que quedan fuera. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-09` —la regla que el intake §17.4 P.5 ancla a este tipo— y `RN-03`, las dos de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4 (F-22, F-23), §4.1 (RN-05, RN-10, RN-11), §4.2 y §7 (CL-3), y `NB-09` de 01. Cambios: el conjunto cerrado pasa a **catorce códigos**; entran `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` y `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR`, los dos derivados de CU-07 y de RN-10; **sale `CONTRATO_TEXTO_NO_INTERPRETABLE`**, porque con el envío como acción única ninguna operación falla por ese motivo, y pasa a señal declarada en CU-03 y CU-05; la causa de `CONTRATO_TRABAJO_NO_ENCONTRADO` se amplía al trabajo en estado `Borrador` que el administrador no ve; FA-01 y CA-02 se reformulan sobre un fallo de forma de la solicitud y se agrega CA-06; §10 declara dónde quedó el índice de figura y por qué la capacidad se conserva sin ejercerse; §9 refiere `RN-10` y `RN-11` por identificador.  **Corrección de la ronda 3 de auditoría, hallazgo H-01**, absorbida en esta misma intervención sin subir versión: §1, §2 y §10 seguían describiendo un catálogo de seis casos de uso —«los otros cinco», «los seis casos de uso»— y contradecían a la propia §10, que ya declaraba el conjunto cerrado como unión de siete. Las tres menciones pasan a seis y a siete; ninguna decisión de contrato cambia. | Analista Funcional + API Designer (AG-02) |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar o quitar un código del conjunto cerrado se trata como incompatible: la pieza pública dejaría de cubrir todos los casos aunque compile. Esta versión ejerce las dos cosas —entran dos códigos y sale uno— por un cambio de alcance de producto, y por eso obliga al despliegue conjunto. `CONTRATO_ERROR_NO_CLASIFICADO` sigue siendo la salida prevista para no tener que agregar códigos ante cada fallo nuevo.
- Agregar cualquier campo que pueda transportar una dirección de servicio o una traza de implementación se rechaza aunque compile: viola RA-03 y el criterio CA-01.
- Agregar un campo opcional al detalle de ubicación es compatible.
