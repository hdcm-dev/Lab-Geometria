# CU-08005 — Contrato de detalle del trabajo interpretado

**Producto:** Fábrica de Geometría
**Documento:** CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1, §5; `NB-00005-Visibilidad-Del-Error-De-Calculo.md` §1, §5; `NB-00006-Visualizacion-Dentro-Del-Producto.md` §5; `NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md` §5 (quinto criterio); `NB-00009-Desenlace-Explicito-De-La-Entrega.md` §1 y §5 (sexto criterio); `00-Contexto/Vision-Producto.md` §9.1 (Pieza, Componente, Observación, Advertencia, Error de validación, Valor declarado / valor derivado); `00-Contexto/Alcance-Producto.md` §4.1 (F-09, F-10, F-11) y §8; `PRODUCT-INTAKE` **1.14** §4 (F-21), §4.1 (RN-08010), §4.2, §12 (entrada «comentario»), §17.4 P.3, P.5, P.10 y P.11, §17.5 P.3, §4 (F-09, F-10, F-11), §6 (flujo 4), §20.E-1, §20.E-3, §20.E-4, §20.E-5
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

Declarar el tipo de transferencia que devuelve un trabajo completo: sus datos, su texto original íntegro, las piezas y componentes que la pieza de datos reconstruyó, las observaciones que la interpretación produjo —con su severidad y sus dos valores cuando corresponde— y, cuando el trabajo ya recibió su desenlace, el **comentario** que el administrador escribió. Es la carga útil que sostiene a la vez la previsualización, el árbol de la estructura, la revisión del administrador y la devolución que el alumno lee, y por eso es la única del contrato que transporta el texto original completo y el comentario.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Solicita el detalle de un trabajo y reparte su contenido entre la vista de datos, el texto, la escena y el árbol |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el detalle con las piezas reconstruidas y las observaciones |
| Ensamblado de contratos | Sistema | Declara la forma de la pieza, del componente, de la observación con sus dos especies de severidad, y del comentario, que es un bloque distinto y no una observación |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El código de la pieza pública tiene una credencial de sesión obtenida por CU-08001 y un identificador de trabajo obtenido por CU-08004.
- El contrato declara el conjunto cerrado de severidades de observación, con las dos especies del glosario raíz: advertencia y error de validación.
- El contrato declara el conjunto cerrado de cuatro estados del trabajo, con `Finalizado` y `Rechazado` como terminales.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de detalle con el identificador del trabajo.
2. El código de la pieza de datos produce el tipo de detalle con seis bloques: datos del trabajo —incluido su estado—, texto original íntegro, colección de piezas, colección de observaciones, datos de identificación del alumno dueño y **comentario del administrador**, que viaja sin poblar mientras el trabajo no haya recibido desenlace.
3. Cada pieza de la colección trae su índice en el conjunto raíz, su tipo declarado y su colección de componentes, y cada componente trae su papel en la pieza y sus dimensiones.
4. Cada observación trae severidad, índice de figura, campo señalado, texto y, cuando la severidad es de advertencia, el valor declarado y el valor derivado.
5. El código de la pieza pública entrega la colección de piezas al bundle del visor para que la dibuje, y el texto original al componente que arma el árbol.
6. El código de la pieza pública muestra las observaciones junto al trabajo, sin filtrarlas por severidad.
7. El código de la pieza pública muestra el comentario, si viene poblado, **como bloque aparte de las observaciones**: son cosas distintas y el contrato las mantiene separadas.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien pide el detalle es el administrador | El contrato es el mismo tipo, con los mismos seis bloques: el administrador ve exactamente lo que ve el alumno dueño. No hay una variante enriquecida del detalle | El flujo continúa en el paso 3 |
| FA-02 | El texto original tiene errores de validación y el trabajo está en `Borrador` | El detalle llega igual, con la colección de piezas parcial —las figuras que sí se interpretaron— y las observaciones de severidad de error de validación en la colección | El flujo continúa en el paso 5, con lo que haya podido reconstruirse |
| FA-03 | El trabajo no produjo ninguna observación | La colección de observaciones llega con cero elementos, y no ausente. El código de la pieza pública no tiene que distinguir entre colección vacía y campo faltante | El flujo continúa en el paso 6 |
| FA-04 | El trabajo ya recibió su desenlace por CU-08007 | El detalle llega con el estado terminal en sus datos y con el bloque de comentario poblado, si el administrador escribió uno. Si no escribió ninguno, el bloque llega sin poblar y **el estado sigue expresando el desenlace por sí solo** | El flujo continúa en el paso 7 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El identificador no corresponde a un trabajo visible para el solicitante, o no existe | Respuesta de error de CU-08006 con texto neutro que no distingue los dos casos. Terminación controlada |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-08006 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

### 6.1 Señales declaradas que no son error

Se separa de la tabla anterior porque no produce respuesta de error. **Este código no es error en ningún contrato de uso del ensamblado**: es señal declarada acá y en CU-08003 §6.1, y las dos señales se remiten mutuamente. Con el envío como acción única de guardado no quedó ninguna operación que pueda fallar por este motivo, de modo que el código salió del conjunto cerrado de CU-08006, que lo declara en su §10.

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_TEXTO_NO_INTERPRETABLE` | El texto original no produjo ninguna pieza | El detalle llega con la colección de piezas en cero elementos y con las observaciones de error de validación pobladas. Recuperación: la persona corrige y reedita por CU-08003 |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene el trabajo completo, con el texto original idéntico al que se cargó y con las observaciones que la interpretación produjo.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-08006 y no arma ni la escena ni el árbol.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El trabajo del escenario E-1 del intake, con un cilindro, un cubo y un ortoedro | El código de la pieza pública pide el detalle | La colección de piezas trae 3 elementos con índices 0, 1 y 2, y la colección de observaciones trae 2 elementos, los dos de severidad de advertencia |
| CA-02 | El mismo detalle de E-1 | Se lee la observación del cubo | Trae índice de figura 1, campo `Area`, valor declarado 36.00 y valor derivado 54.00: los dos valores viajan en campos propios, no embebidos en el texto |
| CA-03 | El trabajo del escenario E-4 del intake, un cubo cuyo área declarada 54.00 coincide con la derivada | El código de la pieza pública pide el detalle | La colección de observaciones trae 0 elementos y el campo de colección está presente, no ausente |
| CA-04 | El trabajo del escenario E-5 del intake, con una figura de tipo desconocido en la posición 1 | El código de la pieza pública pide el detalle | La colección de observaciones trae al menos un elemento con severidad de error de validación, índice de figura 1 y campo `Tipo`, y la primera pieza, que es válida, sigue presente en la colección de piezas |
| CA-05 | El detalle de cualquier trabajo | Se inspecciona la superficie pública del tipo | Trae el texto original completo y **0 campos** con contraseña almacenada, clave de firma o dirección de servicio interno, incluidos los de la identificación del alumno dueño |
| CA-06 | El mismo trabajo pedido por el alumno dueño y por el administrador | Se comparan las dos cargas útiles | Son del mismo tipo y traen los mismos seis bloques: **6 de 6** coincidentes —datos, texto original, piezas, observaciones, datos de identificación del alumno dueño y comentario—, sin ningún bloque, campo ni variante que el administrador reciba y el alumno no |
| CA-07 | Un trabajo en estado `Rechazado` cuyo administrador escribió el comentario `Revisá la fórmula del área del cubo` | El alumno dueño pide el detalle | Los datos traen estado `Rechazado` y el bloque de comentario trae ese texto exacto; el comentario **no** aparece como elemento de la colección de observaciones |
| CA-08 | El tipo de detalle del trabajo | Se inspecciona la superficie pública del bloque de comentario y la del elemento de observación | El comentario declara **0 campos** de severidad, de índice de figura, de campo señalado y de par de valores, y el elemento de observación declara **0 campos** de autoría humana: los dos bloques no comparten ni un campo, y el comentario es a lo sumo uno mientras las observaciones son una colección |
| CA-09 | Un trabajo en estado `Finalizado` cuyo administrador no escribió comentario | El alumno dueño pide el detalle | Los datos traen estado `Finalizado`, el bloque de comentario llega **sin poblar** y el detalle es válido igual: el comentario es opcional en los dos desenlaces |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00004, NB-00005, NB-00006, NB-00007, NB-00009 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-02009`](../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) sobre CA-04, [`RN-02003`](../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) sobre la excepción `CONTRATO_TRABAJO_NO_ENCONTRADO` y [`RN-02010`](../../Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) sobre CA-07 y CA-09, las tres de `GeometriaFactory-Domain`. La tolerancia de claves, la reconstrucción de piezas y el recálculo de valores son también invariantes de ese proyecto de código, sin identificador nombrable desde acá al momento de esta emisión. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | **Pronóstico de la pasada de diseño, superado y no acuñado.** Esta celda anunciaba las 4 historia(s) `US-08011`, `US-08012`, `US-08013`, `US-08020` «a generar en 06» cuando `GeometriaFactory-Contracts` era un proyecto de código con rango propio. **La consolidación de las unidades de entrega lo retiró y esas historias nunca se acuñaron con ese identificador**: las que cubren este contrato viven hoy en los dos [`Product-Backlog.md`](../../Unidades-Entrega/) con la numeración de su unidad. **La correspondencia una a una NO se reconstruye acá**: ningún registro de reconexión la conserva, y deducirla del texto sería inventarla. Queda como ítem diferido — ver la nota de abajo |
| Componentes esperados en 05 | Familia de tipos de transferencia de detalle del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración sobre los escenarios E-1, E-3, E-4 y E-5 del intake; comparación de la carga útil recibida por los dos papeles para CA-06; detalle de un trabajo rechazado con comentario (CA-07) y de uno aprobado sin comentario (CA-09); inspección de superficie pública de los dos bloques para CA-08 |


> **Ítem diferido (`Root-Rules.md` §12.2) · la correspondencia de las historias pronosticadas.**
> **1 · Qué falta:** el mapeo de `US-08011`, `US-08012`, `US-08013`, `US-08020` a las historias vigentes que cubren este contrato.
> **2 · Por qué no se puede hoy:** **ningún registro de reconexión de la consolidación lo conserva**, y reconstruirlo comparando prosa es interpretación y no evidencia. El pronóstico se escribió antes de que existieran las historias reales.
> **3 · Quién lo cierra:** la categoría 06 de las dos unidades de entrega, que es la que las acuñó.
> **4 · En qué evento se cierra:** la **próxima emisión de la 06**, o la **Fase J**, lo que ocurra primero.

## 10. Notas y supuestos

- El contrato no decide qué figura se dibuja ni cómo: la fachada del bundle del visor pertenece a `GeometriaFactory-Visor` y su integración a `GeometriaFactory-Web`.
- El contrato no decide con qué tolerancia se compara un valor declarado con uno derivado: esa decisión pertenece al dominio y a su implementación.
- La forma del árbol de la estructura es una decisión de presentación de `GeometriaFactory-Web` sobre el texto original que este contrato transporta.
- El detalle es el único tipo del ensamblado que transporta el texto original completo y el comentario del administrador; el listado de CU-08004 excluye los dos a propósito.
- **Cómo el contrato impide confundir el comentario con una observación.** Los dos viajan en este mismo tipo, así que la separación no puede quedar librada a la prosa. El contrato la sostiene por construcción, en cuatro planos verificables: **cardinalidad**, el comentario es a lo sumo uno por trabajo —porque los estados de desenlace son terminales— y las observaciones son una colección; **origen**, el comentario lo escribe una persona y las observaciones las emite el producto al interpretar el texto; **forma**, el comentario no declara severidad, ni índice de figura, ni campo señalado, ni par de valor declarado y derivado, y ninguna observación declara autoría humana; **ubicación**, el comentario es un bloque propio del detalle y nunca un elemento de la colección de observaciones. CA-08 verifica los cuatro por inspección.
- El comentario **no es una calificación**: el contrato no declara ningún campo de nota, escala ni puntaje, y calificar sigue fuera del producto aunque la exclusión X-5 se haya retirado para el comentario escrito.
- El contrato transporta el comentario y no lo interpreta: no le impone longitud ni lo asocia a ninguna pieza ni a ningún campo del texto del alumno.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.4 | 2026-08-29 | **Parche `P-02` de la mesa evaluadora del 2026-08-29** ([`../../Audit/Mesa-2026-08-29.md`](../../Audit/Mesa-2026-08-29.md), hallazgo `H-02`, evidencia **E2**, severidad **S2**). La fila «Historias de usuario a generar en 06» de §9 anunciaba historias del rango `08` **que nunca se acuñaron**: la consolidación de las unidades de entrega retiró ese rango y las historias que cubren este contrato se generaron con la numeración de su unidad. La celda pasa a declarar el hecho en lugar de seguir prometiendo artefactos inexistentes, y **la correspondencia una a una NO se reconstruye**: ningún registro de reconexión la conserva y deducirla del texto sería inventarla. Queda como **ítem diferido** con sus cuatro campos, con evento de cierre en la próxima emisión de la 06 o en la Fase J. **Ninguna otra sección cambia.** |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el detalle del trabajo interpretado, con piezas, componentes y observaciones con severidad, índice de figura, campo señalado y el par de valor declarado y valor derivado. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-12**: CA-06 declaraba cinco bloques y verificaba «4 de 4»; pasa a «5 de 5» con el quinto bloque nombrado, que es el conteo que §4 paso 2 sostiene. **H-14**: `CONTRATO_TEXTO_NO_INTERPRETABLE`, que en este caso de uso el propio texto declara que no es respuesta de error, sale de la tabla de §6 y pasa a la subsección nueva §6.1, que además declara por qué el mismo código sí es error en CU-08003. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-08003` y `RN-08009` de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4 (F-21), §4.1 (RN-08010), §4.2, §7 (CL-11), §12, y `NB-00009` de 01. Cambios: el detalle pasa de cinco a **seis bloques** con la incorporación del **comentario del administrador**, opcional y a lo sumo uno por trabajo; §3 declara el conjunto cerrado de cuatro estados con sus dos terminales; se agrega FA-04 para el trabajo ya resuelto; CA-06 pasa a «6 de 6» y se agregan **CA-07, CA-08 y CA-09**, que verifican el comentario poblado, la separación por inspección entre comentario y observación, y el desenlace sin comentario; §9 suma NB-00009 y refiere `RN-08010` por identificador; §10 declara los cuatro planos por los que el contrato impide confundir el comentario con una observación; §17 suma dos cambios que se rechazan aunque compilen. **Precisión de la misma intervención**, detectada por AG-03 al construir su catálogo: §6.1 conservaba la frase «el mismo código sí es error en CU-08003, cuando se pide finalizar», que quedó desactualizada por esta propia actualización —el código no es error en ningún contrato de uso y la acción de finalizar no existe—; la subsección pasa a declarar que es señal declarada en los dos casos de uso, que se remiten mutuamente, y §17 deja de nombrar la finalización al enunciar el efecto de la severidad. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo. **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: el control de cambios tenía **filas con más celdas que columnas** —la celda de autor sobrante, sobre una tabla de tres columnas—, y el texto de esas filas se conserva íntegro: el autor pasa a leerse dentro de la celda de cambios, en lugar de en una cuarta columna que la tabla no declara. **Ninguna otra sección de este contrato de uso se toca**, y ningún tipo, campo, código ni criterio de aceptación cambia. Sube minor: repara la tabla de este control de cambios sin alterar lo que sus filas dicen. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.3**, versión archivada, y pasa a declarar la **1.14**, vigente. Entre la **1.3** y la **1.14** el intake atravesó once emisiones, entre ellas las que incorporaron **F-25**, **F-26** y las reglas **RN-08012** a **RN-08016**: una cabecera que declaraba 1.3 declaraba derivarse de un intake que no conocía ni el reseteo ni la habilitación con contraseña provisoria. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Mover el comentario adentro de la colección de observaciones es incompatible y además destruye la separación que CA-08 verifica: se rechaza aunque compile.
- Agregar al comentario un campo de nota, de escala o de puntaje se rechaza aunque compile: lo convertiría en calificación.
- Agregar una severidad al conjunto cerrado de la observación se trata como incompatible: cambia qué observaciones impiden que un trabajo pase a estado `Pendiente`, y la pieza pública dejaría de cubrir todos los casos.
- Fusionar el valor declarado y el valor derivado en un solo texto es incompatible y además contradice el criterio CA-02.
- Agregar un campo opcional a la pieza o al componente es compatible, siempre que el bundle del visor pueda ignorarlo.
- Declarar una variante enriquecida del detalle sólo para el administrador es incompatible con el criterio CA-06 y no se admite.
