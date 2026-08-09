> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-06-Contrato-De-Respuesta-De-Error.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-06-Contrato-De-Respuesta-De-Error.md`](../../CU-06-Contrato-De-Respuesta-De-Error.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-06 — Contrato de respuesta de error

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-06-Contrato-De-Respuesta-De-Error.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5 (tercer criterio); `NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md` §1, §5 (cuarto criterio); `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §5 (tercer criterio); `00-Contexto/Vision-Producto.md` §9.1 (Fallo silencioso, Error de validación) y §7 R-03; `00-Contexto/Alcance-Producto.md` §8; `PRODUCT-INTAKE` §17.4 **P.5**, §14 (RA-03), §17.5 P.3 y P.5, §7 (CL-2, CL-5, CL-8), §20.E-5
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

Declarar el único tipo de transferencia con el que un fallo cruza la frontera entre las dos piezas desplegables. Es el caso de uso transversal del ensamblado: los otros cinco lo referencian en lugar de declarar cada uno su propia forma de error. Su restricción central es la regla de arquitectura RA-03: el texto es neutro, lleva índice de figura y campo señalado cuando corresponde, y **nunca** la dirección del servicio que falló.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Recibe el tipo de error, decide qué mostrarle a la persona y cuándo pasar a estado degradado |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el tipo de error con código, texto neutro y ubicación del defecto cuando la hay |
| Ensamblado de contratos | Sistema | Declara el tipo de error y el conjunto de códigos que los seis casos de uso usan |

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
| FA-01 | El fallo proviene de la interpretación del texto del alumno | La colección de detalles se puebla con un elemento por defecto encontrado, cada uno con índice de figura y campo | El flujo continúa en el paso 5, y el código de la pieza pública muestra la ubicación exacta |
| FA-02 | El fallo es que la pieza de datos no responde | El tipo de error lo produce el propio código de la pieza pública, con el código `CONTRATO_SERVICIO_NO_DISPONIBLE` y la colección de detalles vacía | El flujo continúa en el paso 5, con estado degradado explícito |
| FA-03 | El fallo se refiere a un recurso ajeno al solicitante | El código y el texto son los mismos que para un recurso inexistente: el contrato no ofrece forma de distinguirlos | El flujo continúa en el paso 5 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Una solicitud llega incompleta | Detalle con el nombre del campo ausente y sin índice de figura. Recuperación por corrección y reintento |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | El texto del alumno tiene defectos que impiden reconstruir figuras | Un detalle por defecto, con índice de figura y campo. Handoff al flujo de reedición de CU-03 |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | Recurso inexistente o ajeno | Texto neutro, sin detalles. Terminación controlada |
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
| CA-02 | El texto del escenario E-5 del intake, con una figura de tipo desconocido en la posición 1 | La pieza de datos produce el error de interpretación | El tipo de error trae al menos un detalle con índice de figura 1 y campo `Tipo`, y el texto no es genérico |
| CA-03 | Un canje de credenciales con la contraseña equivocada | La pieza de datos produce el error | El texto neutro no nombra ni el campo de correo ni el de contraseña: la respuesta no revela cuál de los dos falló |
| CA-04 | La pieza de datos detenida | El código de la pieza pública intenta cualquier solicitud | Recibe el tipo de error con código `CONTRATO_SERVICIO_NO_DISPONIBLE`, con 0 detalles y con un texto que no contiene ninguna dirección; el resultado es estado degradado y no una excepción sin manejar |
| CA-05 | Un alumno que pide el trabajo de otro, cuyo identificador conoce | La pieza de datos produce el error | El código y el texto son idénticos a los de un identificador inexistente: 0 campos permiten distinguir los dos casos |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-04, NB-08, y NB-02 por la explicación de la situación de la cuenta |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-09`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) —toda observación de error indica la posición de la pieza y el campo, que es la regla que `PRODUCT-INTAKE` §17.4 P.5 ancla a este tipo— sobre CA-02, y [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre CA-05, las dos de `GeometriaFactory-Domain`. La regla de arquitectura RA-03 es de nivel producto, vive en `PRODUCT-INTAKE` §14 y su tratamiento arquitectónico pertenece a 05 |
| Historias de usuario a generar en 06 | US-14 tipo de error con texto neutro; US-15 detalle de ubicación con índice de figura y campo; US-16 conjunto cerrado de códigos de error |
| Componentes esperados en 05 | Tipo de transferencia de error del ensamblado de contratos, transversal a las demás familias |
| Tests previstos en 08 | Prueba de inspección de superficie pública para CA-01; pruebas de integración de error de interpretación con E-5, de credencial inválida, de recurso ajeno y de servicio detenido |

## 10. Notas y supuestos

- El tipo de error es el mismo para los seis casos de uso del ensamblado. Un tipo de error por familia multiplicaría los lugares donde se puede filtrar una dirección de servicio, que es exactamente lo que RA-03 evita.
- El contrato no fija el código de estado de la respuesta del servicio: eso pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.5).
- Este caso de uso no describe cómo se presenta el estado degradado a la persona: eso pertenece a `GeometriaFactory-Web` y a la categoría 03.
- El código `CONTRATO_SERVICIO_NO_DISPONIBLE` es el único que el contrato admite que produzca la propia pieza pública, porque describe la ausencia de respuesta de la otra pieza.
- El conjunto cerrado de códigos es la unión de los que declaran los seis casos de uso, y **trece** con la incorporación de `CONTRATO_CONTRASENA_NO_ESTABLECIDA` por la corrección H-02 de la ronda 1 de auditoría. Dos señales declaradas quedan deliberadamente fuera del conjunto, en las subsecciones §6.1 de CU-04 y de CU-05, porque no producen respuesta de error.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el tipo único de respuesta de error, con texto neutro, conjunto cerrado de códigos y detalle de ubicación con índice de figura y campo, y la prohibición de transportar direcciones de servicio interno. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-02**, por arrastre de CU-01: se incorpora la fila del código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` en §6 y §10 declara que el conjunto cerrado pasa a trece códigos, con las dos señales declaradas que quedan fuera. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-09` —la regla que el intake §17.4 P.5 ancla a este tipo— y `RN-03`, las dos de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar un código al conjunto cerrado se trata como incompatible: la pieza pública dejaría de cubrir todos los casos aunque compile. `CONTRATO_ERROR_NO_CLASIFICADO` es la salida prevista para no tener que agregar códigos ante cada fallo nuevo.
- Agregar cualquier campo que pueda transportar una dirección de servicio o una traza de implementación se rechaza aunque compile: viola RA-03 y el criterio CA-01.
- Agregar un campo opcional al detalle de ubicación es compatible.
