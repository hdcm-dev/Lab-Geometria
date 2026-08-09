> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md`](../../CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-05 — Contrato de detalle del trabajo interpretado

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5; `NB-05-Visibilidad-Del-Error-De-Calculo.md` §1, §5; `NB-06-Visualizacion-Dentro-Del-Producto.md` §5; `NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §5 (cuarto criterio); `00-Contexto/Vision-Producto.md` §9.1 (Pieza, Componente, Observación, Advertencia, Error de validación, Valor declarado / valor derivado); `00-Contexto/Alcance-Producto.md` §4.1 (F-09, F-10, F-11) y §8; `PRODUCT-INTAKE` §17.4 P.3, P.5, P.10 y P.11, §17.5 P.3, §4 (F-09, F-10, F-11), §6 (flujo 4), §20.E-1, §20.E-3, §20.E-4, §20.E-5
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

Declarar el tipo de transferencia que devuelve un trabajo completo: sus datos, su texto original íntegro, las piezas y componentes que la pieza de datos reconstruyó, y las observaciones que la interpretación produjo, con su severidad y sus dos valores cuando corresponde. Es la carga útil que sostiene a la vez la previsualización, el árbol de la estructura y la revisión del administrador, y por eso es la única del contrato que sí transporta el texto original completo.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Solicita el detalle de un trabajo y reparte su contenido entre la vista de datos, el texto, la escena y el árbol |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el detalle con las piezas reconstruidas y las observaciones |
| Ensamblado de contratos | Sistema | Declara la forma de la pieza, del componente y de la observación con sus dos especies de severidad |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-01 y un identificador de trabajo obtenido por CU-04.
- El contrato declara el conjunto cerrado de severidades de observación, con las dos especies del glosario raíz: advertencia y error de validación.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de detalle con el identificador del trabajo.
2. El código de la pieza de datos produce el tipo de detalle con cinco bloques: datos del trabajo, texto original íntegro, colección de piezas, colección de observaciones y datos de identificación del alumno dueño.
3. Cada pieza de la colección trae su índice en el conjunto raíz, su tipo declarado y su colección de componentes, y cada componente trae su papel en la pieza y sus dimensiones.
4. Cada observación trae severidad, índice de figura, campo señalado, texto y, cuando la severidad es de advertencia, el valor declarado y el valor derivado.
5. El código de la pieza pública entrega la colección de piezas al bundle del visor para que la dibuje, y el texto original al componente que arma el árbol.
6. El código de la pieza pública muestra las observaciones junto al trabajo, sin filtrarlas por severidad.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien pide el detalle es el administrador | El contrato es el mismo tipo, con los mismos cinco bloques: el administrador ve exactamente lo que ve el alumno. No hay una variante enriquecida del detalle | El flujo continúa en el paso 3 |
| FA-02 | El texto original tiene errores de validación y el trabajo está en `Borrador` | El detalle llega igual, con la colección de piezas parcial —las figuras que sí se interpretaron— y las observaciones de severidad de error de validación en la colección | El flujo continúa en el paso 5, con lo que haya podido reconstruirse |
| FA-03 | El trabajo no produjo ninguna observación | La colección de observaciones llega con cero elementos, y no ausente. El código de la pieza pública no tiene que distinguir entre colección vacía y campo faltante | El flujo continúa en el paso 6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a un trabajo visible para el solicitante, o no existe | Respuesta de error de CU-06 con texto neutro que no distingue los dos casos. Terminación controlada |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

### 6.1 Señales declaradas que no son error

Se separa de la tabla anterior porque en este caso de uso no produce respuesta de error. El mismo código **sí** es error en CU-03, cuando se pide finalizar; acá el trabajo existe y hay que poder verlo.

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | El texto original no produjo ninguna pieza | El detalle llega con la colección de piezas en cero elementos y con las observaciones de error de validación pobladas. Recuperación: la persona corrige y reedita por CU-03 |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene el trabajo completo, con el texto original idéntico al que se cargó y con las observaciones que la interpretación produjo.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y no arma ni la escena ni el árbol.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El trabajo del escenario E-1 del intake, con un cilindro, un cubo y un ortoedro | El código de la pieza pública pide el detalle | La colección de piezas trae 3 elementos con índices 0, 1 y 2, y la colección de observaciones trae 2 elementos, los dos de severidad de advertencia |
| CA-02 | El mismo detalle de E-1 | Se lee la observación del cubo | Trae índice de figura 1, campo `Area`, valor declarado 36.00 y valor derivado 54.00: los dos valores viajan en campos propios, no embebidos en el texto |
| CA-03 | El trabajo del escenario E-4 del intake, un cubo cuyo área declarada 54.00 coincide con la derivada | El código de la pieza pública pide el detalle | La colección de observaciones trae 0 elementos y el campo de colección está presente, no ausente |
| CA-04 | El trabajo del escenario E-5 del intake, con una figura de tipo desconocido en la posición 1 | El código de la pieza pública pide el detalle | La colección de observaciones trae al menos un elemento con severidad de error de validación, índice de figura 1 y campo `Tipo`, y la primera pieza, que es válida, sigue presente en la colección de piezas |
| CA-05 | El detalle de cualquier trabajo | Se inspecciona la superficie pública del tipo | Trae el texto original completo y **0 campos** con contraseña almacenada, clave de firma o dirección de servicio interno, incluidos los de la identificación del alumno dueño |
| CA-06 | El mismo trabajo pedido por el alumno dueño y por el administrador | Se comparan las dos cargas útiles | Son del mismo tipo y traen los mismos cinco bloques: **5 de 5** coincidentes —datos, texto original, piezas, observaciones y datos de identificación del alumno dueño—, sin ningún bloque, campo ni variante que el administrador reciba y el alumno no |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-04, NB-05, NB-06, NB-07 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-09`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) sobre CA-04 y [`RN-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre la excepción `CONTRATO_TRABAJO_NO_ENCONTRADO`, las dos de `GeometriaFactory-Domain`. La tolerancia de claves, la reconstrucción de piezas y el recálculo de valores son también invariantes de ese proyecto de código, sin identificador nombrable desde acá al momento de esta emisión. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-11 tipo de detalle de trabajo; US-12 tipos de pieza y de componente; US-13 tipo de observación con severidad y par de valores |
| Componentes esperados en 05 | Familia de tipos de transferencia de detalle del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración sobre los escenarios E-1, E-3, E-4 y E-5 del intake; comparación de la carga útil recibida por los dos papeles para CA-06 |

## 10. Notas y supuestos

- El contrato no decide qué figura se dibuja ni cómo: la fachada del bundle del visor pertenece a `GeometriaFactory-Visor` y su integración a `GeometriaFactory-Web`.
- El contrato no decide con qué tolerancia se compara un valor declarado con uno derivado: esa decisión pertenece al dominio y a su implementación.
- La forma del árbol de la estructura es una decisión de presentación de `GeometriaFactory-Web` sobre el texto original que este contrato transporta.
- El detalle es el único tipo del ensamblado que transporta el texto original completo; el listado de CU-04 lo excluye a propósito.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el detalle del trabajo interpretado, con piezas, componentes y observaciones con severidad, índice de figura, campo señalado y el par de valor declarado y valor derivado. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-12**: CA-06 declaraba cinco bloques y verificaba «4 de 4»; pasa a «5 de 5» con el quinto bloque nombrado, que es el conteo que §4 paso 2 sostiene. **H-14**: `CONTRATO_TEXTO_NO_INTERPRETABLE`, que en este caso de uso el propio texto declara que no es respuesta de error, sale de la tabla de §6 y pasa a la subsección nueva §6.1, que además declara por qué el mismo código sí es error en CU-03. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-03` y `RN-09` de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar una severidad al conjunto cerrado de la observación se trata como incompatible: cambia qué observaciones impiden finalizar, y la pieza pública dejaría de cubrir todos los casos.
- Fusionar el valor declarado y el valor derivado en un solo texto es incompatible y además contradice el criterio CA-02.
- Agregar un campo opcional a la pieza o al componente es compatible, siempre que el bundle del visor pueda ignorarlo.
- Declarar una variante enriquecida del detalle sólo para el administrador es incompatible con el criterio CA-06 y no se admite.
