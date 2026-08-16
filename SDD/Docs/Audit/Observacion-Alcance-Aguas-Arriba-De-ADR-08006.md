# Observación — Lo que `ADR-08006` alcanza aguas arriba

**Producto:** Fábrica de Geometría
**Documento:** Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md
**Versión:** 1.0
**Estado:** Emitido — **espera decisión del Product Owner**
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD
**Instrumento:** `Master-Prompt.md` §9, manejo de ambigüedad: un dato que el producto no puede resolver por su cuenta **se eleva y no se decide**
**Alcanza a:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §20.E-7 y §20.E-8; `Requerimientos-Tecnicos.md` §8.3

---

## 1. Por qué existe este documento

[`../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md`](../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md)
registra una decisión del Product Owner que cambia un contrato declarado: **el visor recibe las
piezas reconstruidas y no el texto del alumno**.

Esa decisión alcanza **tres afirmaciones que viven en documentos del Product Owner**, y este
orquestador **no los edita**: el intake es documento humano y los requerimientos técnicos son su
fuente. Lo que corresponde es enumerar qué queda desalineado, con qué texto exacto, y para qué
decisión.

**Ninguna de las tres invalida la decisión.** Las tres describen propiedades que la decisión cambia,
y la pregunta no es si la decisión está bien —ya está tomada— sino **qué se hace con lo que esas
tres afirmaban**.

## 2. Las tres afirmaciones alcanzadas

### 2.1 `§20.E-7` punto 4 — «todo esto ocurre sin backend»

**Qué dice hoy.** «Todo esto ocurre **sin backend**, con el bundle cargado en una página estática y
el JSON pegado a mano: es la propiedad de `tools_json_figure_viewer` que RT §8.3 exige no perder.»

**Qué pasa a ser cierto.** La página estática sigue existiendo y el bundle sigue dibujando sin
backend, pero **lo que se pega ahí es la estructura de piezas y no el texto del alumno**. Pegar el
texto crudo ya no dibuja.

**Lo que el escenario deja de ejercitar, y es lo que importa medir.** `E-7` era el único que cubría
el mapeo de los seis tipos **y la clave `Bases`** desde el lado del bundle. El mapeo de tipos se
conserva; **la tolerancia de claves deja de ser suya**, porque ya no la tiene.

### 2.2 `§20.E-8` — la condición `DIMENSION_NO_LEGIBLE`

**Qué dice hoy.** El punto 2 declara que la pieza del índice 1 «no se dibuja, y el resultado de
`cargarJson` la reporta con índice 1 y código `DIMENSION_NO_LEGIBLE`».

**Qué pasa a ser cierto.** Esa pieza **no llega al visor**: el validador no la reconstruyó y emitió
su error de validación con posición y campo, y el trabajo quedó en `Borrador`. El punto 5 del mismo
escenario —el desenlace del envío— **no cambia y se cumple igual**.

**La condición no se retira del contrato de la fachada**, y el motivo está en su §4.2: sigue
haciendo falta para el caso en que el anfitrión entregue una pieza que la fachada no pueda usar. Lo
que cambia es que **deja de ser el camino normal de este escenario**.

### 2.3 `RT` §8.3 — la propiedad que se pide no perder

**Qué dice hoy.** Que no se pierda la propiedad de `tools_json_figure_viewer`: cualquiera pega el
texto y ve el dibujo, sin instalar nada.

**Qué pasa a ser cierto.** Se conserva «sin instalar nada» y se conserva «sin backend». **No se
conserva «pegando el texto»**: para dibujar hay que pegar la estructura de piezas, y quien tenga el
texto crudo necesita al laboratorio para convertirlo.

**Es el costo declarado de la decisión**, y está anotado como tal en `ADR-08006` §3.2.

## 3. Qué se le pide al Product Owner

| # | Decisión | Opciones |
| --- | --- | --- |
| 1 | Qué se hace con `§20.E-7` punto 4 | **(a)** Reescribirlo declarando que la página estática recibe la estructura de piezas · **(b)** Conservarlo y declarar que describe el comportamiento anterior a `ADR-08006` |
| 2 | Qué se hace con `§20.E-8` puntos 2 y 3 | **(a)** Reescribirlos sobre el camino nuevo, con la pieza retenida por el validador · **(b)** Conservarlos como el caso de contrato que la fachada sigue declarando |
| 3 | Qué se hace con `RT` §8.3 | **(a)** Precisar la propiedad: «sin instalar nada y sin backend», sin «pegando el texto» · **(b)** Sostenerla como está y **reabrir la decisión**, porque es la única de las tres que la decisión contradice de frente |

**La tercera es la que conviene mirar primero.** Las dos primeras son ajustes de redacción sobre
escenarios; la tercera es una propiedad que el análisis declaró **como cosa a no perder**, y la
decisión la pierde en parte. Puede ser un costo aceptable —el Product Owner ya lo aceptó al decidir—
pero merece quedar aceptado **sobre este texto** y no sólo sobre el resumen.

## 4. Qué NO está esperando esta observación

**La construcción no está bloqueada.** `ADR-08006` habilita escribir la firma nueva, el punto de
acceso `A-18` y el bloque de previsualización, y los tres documentos que declaran esas cosas ya
están al día. Lo que espera acá es **qué dicen de sí mismos** el intake y los requerimientos
técnicos, que es trabajo del Product Owner y no del que construye.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Eleva las **tres afirmaciones aguas arriba** que `ADR-08006` alcanza —`§20.E-7` punto 4, `§20.E-8` puntos 2 y 3, y `RT` §8.3—, con el texto exacto de cada una, qué pasa a ser cierto, y la decisión que se le pide al Product Owner sobre cada una. Declara que **ninguna invalida la decisión** y que **la construcción no está bloqueada**, y señala que la tercera es la única que la decisión contradice de frente. | Orquestador SDD |
