# Observación — La sincronización escena ⇄ árbol tiene una dirección sin canal

**Producto:** Fábrica de Geometría
**Documento:** Observacion-Sincronizacion-Escena-Arbol.md
**Versión:** 1.0
**Estado:** Emitido — **espera decisión del Product Owner**
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD
**Instrumento:** `Master-Prompt.md` §9 — un dato que el producto no puede resolver por su cuenta **se eleva y no se decide**
**Alcanza a:** `Definicion-Contrato-De-Fachada.md` §4 (las seis funciones); `Wireframes-Vista-De-Trabajo.md` §4; `F-13`

---

## 1. Qué pide el wireframe

`Wireframes-Vista-De-Trabajo.md` §4 declara **dos interacciones simétricas**, y `F-13` —`Must Have`
desde el intake 1.19— las nombra como una sola capacidad:

| Interacción | Qué tiene que pasar |
| --- | --- |
| **Seleccionar un nodo del árbol** | «Se pide resaltar esa pieza por su índice. **La escena resalta esa pieza y sólo esa**» |
| **Seleccionar una pieza en la escena** | «**El nodo correspondiente del árbol queda marcado y visible**, por el mismo índice, sin traducir identidad» |

## 2. Qué se construyó, y qué no

**La primera está hecha.** El nodo del árbol lleva su posición a la vista, y activarlo invoca
`selectPiece` con ese índice. La identidad es la misma de los dos lados y no se traduce nada.

**La segunda no, y no es un olvido.** El contrato de la fachada declara **seis funciones**, y las
seis van del anfitrión hacia el visor: `initialize`, `cargarPiezas`, `selectPiece`, `resize`,
`destroy` y `setMotion`. **Ninguna es un aviso del visor hacia su anfitrión.**

De modo que cuando la persona elige una pieza **en la escena**, no hay por dónde enterarse: el
anfitrión no tiene forma de saberlo, y sin saberlo no puede marcar el nodo.

**Es un hueco del contrato y no de la implementación.** Escribirlo igual exigiría una vía que el
contrato no declara, y las tres formas de improvisarla son peores que el hueco: leer el interior del
visor desde afuera rompe la regla de aislamiento de §5 del wireframe —«la escena se opera
exclusivamente por las seis funciones»—; poner un evento en el elemento del dibujo inventa una
superficie que ninguna fuente declara; y sondear el estado del visor cada tanto es una vía nueva
disfrazada de otra cosa.

## 3. Las dos formas de cerrarlo

| Salida | Qué implica |
| --- | --- |
| **Un aviso en las opciones de `initialize`** | El anfitrión entrega una función que el visor llama al seleccionarse una pieza. **No agrega una séptima función**: `ViewerOptions` ya existe y es el lugar donde el anfitrión configura la instancia. Es el cambio más chico que cierra el hueco |
| **Una séptima función de fachada** | Explícita y simétrica con las demás, y **toca la zona de frontera `F-01a`**: las seis funciones las fijó el Product Owner el 2026-08-12, y su recuento está citado en el intake, en la norma y en tres documentos más |

**La primera parece la razonable**, y aun así **no se toma acá**: las dos cambian el contrato de la
fachada, y ese contrato lo fijó el Product Owner. Lo que corresponde es elevarlo con las dos
opciones y su costo.

## 4. Qué pasa mientras tanto

**La superficie no promete lo que no hace.** El texto que acompaña al árbol dice «elegí una figura
del árbol y se resalta en la escena», que es exactamente lo que ocurre. **No dice que funcione al
revés**, y por eso no hay control muerto ni promesa incumplida: hay una capacidad a medias,
declarada.

**`F-13` no queda cumplida.** Es `Must Have` y condiciona el cierre de la etapa `g` por la
transición `g` → `h`, cuyo quinto criterio es «el árbol y la escena se sincronizan por índice de
pieza». **La mitad que falta es de este contrato**, y el punto de control de la etapa no debería
cerrarse sin resolverla.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, al construir el árbol de la vista de trabajo. Declara que **la dirección árbol → escena está hecha** y que **la escena → árbol no tiene canal**: las seis funciones de la fachada van todas del anfitrión hacia el visor y ninguna avisa de vuelta. Enumera las tres formas de improvisarla y por qué las tres son peores que el hueco, y eleva **dos salidas** con su costo: un aviso dentro de las opciones de `initialize` —el cambio más chico, sin séptima función— o una séptima función, que toca la zona de frontera `F-01a`. Declara que **`F-13` no queda cumplida** y que el punto de control de la etapa `g` no debería cerrarse sin resolverlo. | Orquestador SDD |
