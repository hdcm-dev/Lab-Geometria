# CU-02 — Cargar el texto del trabajo y dibujar sus piezas

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `NB-06-Visualizacion-Dentro-Del-Producto.md` §1, §4 y §5 (criterios primero, segundo y cuarto); `NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §4, en su parte de piezas efectivamente dibujadas; `00-Contexto/Vision-Producto.md` §3 (diferenciadores D-3 y D-4) y §9; `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.2, P.3 y P.11 (puntos 4 y 5), §14 (RA-02), §20 E-1 y §20 E-7
**Trazabilidad downstream:** 03-UX-UI-DX, 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples

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

Permitir que el componente anfitrión entregue el texto de un trabajo a una instancia viva y obtenga, en una sola invocación, las piezas dibujadas en la escena y la estructura de ese texto lista para presentarse como árbol. Es el contrato de uso que elimina el fallo silencioso: toda pieza que no se dibuja queda enumerada con su índice.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Componente anfitrión | Primario | Entrega el texto del trabajo a una instancia viva e invoca `cargarJson`, y consume el resultado de dibujo |
| Fachada del visor | Sistema | Obtiene el conjunto de piezas del texto, construye las mallas, las ubica por índice y devuelve el resultado de dibujo |
| Texto del trabajo | Secundario | Dato de entrada del que se leen los tipos y las dimensiones. La fachada no lo pide, no lo guarda y no lo reescribe |

## 3. Precondiciones

1. Existe una instancia viva y el componente anfitrión tiene su identificador (`CU-01`).
2. El componente anfitrión tiene el texto del trabajo en la mano. Cómo lo obtuvo no le concierne a la fachada: puede venir de un formulario, de una página de prueba o de un componente que ya lo tenía.
3. No se requiere conectividad de ningún tipo.

## 4. Flujo principal

| Paso | Actor | Acción |
| --- | --- | --- |
| 1 | Componente anfitrión | Invoca `cargarJson(id, texto)` con el identificador de una instancia viva y el texto del trabajo |
| 2 | Fachada del visor | Libera las mallas, las geometrías y los materiales que la carga anterior de esa instancia hubiera creado, y deja la escena vacía |
| 3 | Fachada del visor | Obtiene del texto el conjunto raíz de piezas, en su orden original, y conserva su índice como identidad de cada pieza |
| 4 | Fachada del visor | Para cada pieza, lee su tipo y las dimensiones que necesita para construir su malla, aceptando las variantes de clave declaradas en `Definicion-Contrato-De-Fachada.md` §3.3 |
| 5 | Fachada del visor | Construye la malla de cada pieza dibujable y la ubica en la escena en la posición **derivada de su índice** |
| 6 | Fachada del visor | Arma la estructura del texto para que el componente anfitrión la presente como árbol colapsable |
| 7 | Fachada del visor | Devuelve el resultado de dibujo: piezas dibujadas con índice y tipo, piezas no dibujadas con índice y código de condición, y la estructura del texto |
| 8 | Componente anfitrión | Presenta el árbol y, si lo desea, informa a quien mire las piezas que no se dibujaron |

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 · Carga sucesiva sobre la misma instancia | El componente anfitrión invoca `cargarJson` sobre una instancia que ya tenía un trabajo dibujado | El paso 2 libera por completo lo anterior antes de dibujar lo nuevo. La selección vigente se descarta: después de una carga no hay ninguna pieza resaltada | Paso 3 del flujo principal |
| FA-02 · Piezas de tipo no dibujable dentro de un trabajo con piezas dibujables | Al menos una pieza declara un tipo que no está entre los seis dibujables | Las piezas dibujables se dibujan; las otras quedan enumeradas con `TIPO_NO_DIBUJABLE` y su índice. La carga es exitosa | Paso 5, con el subconjunto dibujable |
| FA-03 · Conjunto raíz vacío | El texto trae un conjunto raíz sin ninguna pieza | La instancia queda viva y con la escena vacía, y el resultado de dibujo devuelve 0 piezas dibujadas y 0 no dibujadas. No es una condición de error | Paso 7 del flujo principal |
| FA-04 · Variante de clave para las bases del volumen | El texto nombra las bases del ortoedro con una clave y no con la otra | La fachada lee la dimensión igual en los dos casos y la pieza se dibuja. Es lectura de dimensión, no validación del trabajo | Paso 5 del flujo principal |
| FA-05 · Pieza con una dimensión en `0.00` | Una pieza de tipo dibujable expone la dimensión que su malla necesita, con valor `0.00` | **La pieza se dibuja.** El cero es una dimensión legible: lo que hace ilegible una dimensión es la **ausencia** de la clave o del componente, nunca su valor. La malla puede resultar visualmente degenerada, y eso es consecuencia legítima del dato del alumno. **No** se emite `DIMENSION_NO_LEGIBLE` y la pieza **no** queda entre las no dibujadas | Paso 5 del flujo principal |

## 6. Excepciones y errores

| Código | Causa | Respuesta de la fachada |
| --- | --- | --- |
| `INSTANCIA_DESCONOCIDA` | El identificador recibido no corresponde a ninguna instancia viva | No se dibuja nada, ninguna instancia cambia y se informa el código |
| `TEXTO_NO_LEGIBLE` | Del texto recibido no se puede obtener un conjunto de piezas | La instancia queda viva y vacía: lo anterior se libera igual y no se dibuja nada nuevo. Se informa el código. La fachada **no** califica el texto de inválido ni emite observación: eso es del backend |
| `TIPO_NO_DIBUJABLE` | Una pieza declara un tipo fuera de los seis dibujables | Esa pieza no se dibuja y queda enumerada con su índice; el resto del trabajo se dibuja |
| `DIMENSION_NO_LEGIBLE` | Una pieza de tipo dibujable **no expone** la dimensión necesaria para construir su malla: la clave o el componente del que se lee la medida está ausente. **Un valor de `0.00` no es causa de esta condición** (FA-05) | Esa pieza no se dibuja y queda enumerada con su índice; el resto del trabajo se dibuja |

Ninguna de las cuatro condiciones deja la instancia en estado indeterminado ni obliga a destruirla: después de cualquiera de ellas, la instancia admite una carga nueva.

## 7. Postcondiciones

- **Éxito:** la escena contiene una malla por cada pieza dibujable del texto, ubicada por su índice; el resultado de dibujo enumera dibujadas y no dibujadas; el componente anfitrión tiene la estructura del texto para su árbol; el texto recibido no se conservó ni se modificó; hubo 0 peticiones de red.
- **Fallo:** la instancia sigue viva; la escena queda vacía o con la carga anterior liberada, según el código; no queda ninguna malla huérfana ni contexto gráfico duplicado.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia viva y el texto del escenario E-1 del intake, con tres piezas: `Cilindro`, `Cubo` y `Ortoedro` | El componente anfitrión invoca `cargarJson(id, texto)` | El resultado de dibujo enumera **3 piezas dibujadas** con los índices 0, 1 y 2, **incluido el ortoedro**, y 0 piezas no dibujadas |
| CA-02 | Una instancia viva y el texto del escenario E-7 del intake, con seis piezas que cubren los seis tipos dibujables | El componente anfitrión invoca `cargarJson(id, texto)` | El resultado de dibujo enumera **6 piezas dibujadas** con los índices 0 a 5: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado` y `Circulo` |
| CA-03 | Una instancia viva y el texto del escenario E-7, cuyo ortoedro declara bases de 6.00 × 4.00 y laterales de altura 8.00 | El componente anfitrión invoca `cargarJson(id, texto)` | La pieza de índice 2 se dibuja con ancho 6, profundidad 4 y altura 8, coherente con el volumen declarado de 192.00 |
| CA-04 | Una instancia viva y el texto del escenario E-1 | El componente anfitrión invoca `cargarJson(id, texto)` dos veces seguidas con el mismo texto | Las dos cargas producen la **misma disposición** de las tres piezas, comparable pieza por pieza, y el mismo resultado de dibujo |
| CA-05 | Una instancia viva y un texto cuyo conjunto raíz tiene 3 piezas, y la de índice 1 declara un tipo que no está entre los seis dibujables | El componente anfitrión invoca `cargarJson(id, texto)` | Se dibujan 2 piezas, con índices 0 y 2, y el resultado de dibujo enumera la pieza de índice 1 como no dibujada con el código `TIPO_NO_DIBUJABLE`: ninguna pieza desaparece sin registro |
| CA-06 | Una instancia viva y un texto del que no se puede obtener ningún conjunto de piezas | El componente anfitrión invoca `cargarJson(id, texto)` | La fachada informa `TEXTO_NO_LEGIBLE`, la escena queda vacía, la instancia sigue viva y no se emite ninguna advertencia ni ningún error de validación |
| CA-07 | Una instancia viva, la pestaña de red vacía y el texto del escenario E-7 | El componente anfitrión invoca `cargarJson(id, texto)` | La pestaña de red registra exactamente **0 peticiones** originadas por la fachada, y el almacenamiento del navegador queda sin ninguna clave nueva |
| CA-08 | Una instancia viva y el texto del escenario E-6 del intake, un `Rectangulo` con `Largo` en `0.00` | El componente anfitrión invoca `cargarJson(id, texto)` | El resultado de dibujo enumera **1 pieza dibujada** con índice 0 y **0 piezas no dibujadas**: la pieza no se descarta, no se emite `DIMENSION_NO_LEGIBLE` y la figura no desaparece de la escena |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-06; NB-04 en su parte de piezas efectivamente dibujadas |
| Reglas de negocio aplicables | Ninguna. Este proyecto de código no declara RN (ver `README.md` de la sección) |
| Historias de usuario a generar | US de dibujo del trabajo, de disposición derivada del índice y de enumeración de piezas no dibujadas, en 06-Backlog-Tecnico |
| Componentes esperados | Fachada plana y servicio de dibujo, con la lectura de tipos y dimensiones; 05-Arquitectura-Tecnica fija la composición |
| Tests previstos | 08-Calidad-Y-Pruebas: E-1 con tres piezas y ortoedro dibujado, E-7 con los seis tipos, doble carga con disposición idéntica, tipo no dibujable enumerado, texto no legible y conteo de peticiones en 0 |
| Concepto central | `Definicion-Contrato-De-Fachada.md` §4.2, §5.2, §5.3, §5.4 y §6 |

## 10. Notas y supuestos

- **La fachada no valida el trabajo.** No decide si es válido, no emite advertencias ni errores de validación y no compara valor declarado contra valor derivado: todo eso lo hace el backend del producto. Que la fachada acepte las mismas variantes de clave que el backend no es duplicar la validación; es saber de dónde leer una dimensión para construir una malla (PRODUCT-INTAKE §17.7 P.11 punto 4).
- El escenario **E-1** tiene su texto editado a mano —nombra las bases del ortoedro con una clave distinta de la que emite el programa del alumno y no trae comas finales—, de modo que ejercita el camino feliz de la lectura de claves y **no** las tolerancias del formato. Las trampas del formato las ejercita E-2, que es material del backend y no de este caso de uso.
- El escenario **E-7** es el único que ejercita las figuras planas como piezas del conjunto raíz, y es el juego de datos del sample S-1.
- `RectanguloDesarrollado` no se dibuja como pieza del conjunto raíz: aparece sólo como componente del cilindro y sirve para leer su dimensión.
- La posición de cada pieza se deriva de su índice. Reemplaza el ordenamiento aleatorio del visualizador previo y es lo que hace comparables dos previsualizaciones del mismo trabajo.
- **El estado de los movimientos automáticos sobrevive a la carga.** Cargar otro texto reemplaza el contenido dibujado, no el gobierno de la escena: lo que estaba prendido sigue prendido y las piezas nuevas nacen girando o quietas según ese estado. Cambiarlo es de `establecerMovimiento` (`CU-07`), y no de este caso de uso.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Contrato de uso de `cargarJson`, con cuatro flujos alternativos, cuatro condiciones de error y siete criterios de aceptación anclados en los escenarios E-1 y E-7 del intake. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **El cero como dimensión legible**: nace **FA-05**, una pieza con una dimensión en `0.00` se dibuja; **§6** precisa que la causa de `DIMENSION_NO_LEGIBLE` es la ausencia de la clave o del componente y nunca el valor; y nace **CA-08**, anclado en el escenario `E-6` del intake §20. Lo motivó la validación visual: el visualizador previo evaluaba la verdad del número y perdía la figura sin aviso, lo que contradice ese escenario declarado y vacía la garantía G-5 del contrato. Concuerda con `Definicion-Contrato-De-Fachada.md` §5.3 y §6. |
| 1.0 | 2026-08-09 | Segunda absorción de la **Fase B2**, por la decisión del Product Owner de agregar una **sexta función** a la fachada. **Sin subir versión** por `Master-Prompt.md` §5 (documento en estado `Propuesto`). §10 suma la nota que declara que **el estado de los movimientos automáticos sobrevive a `cargarJson`**: la carga reemplaza el contenido dibujado y no el gobierno de la escena, que es de `establecerMovimiento` (`CU-07`). El contrato de `cargarJson` no cambia por lo demás. |
