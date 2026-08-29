# ADR-14005 — Las familias que el destino acuñó declaran ámbito de proyecto de código y conservan su ancho

**Producto:** Fábrica de Geometría
**Documento:** ADR-14005-Familias-Acunadas-Por-El-Destino-Con-Ambito-De-Proyecto.md
**Versión:** 2.0
**Estado:** **RETIRADO** el 2026-08-29 por decisión del Product Owner, **sin haber sido aceptado**. El apartamiento que proponía dejó de hacer falta: las dos familias se renumeraron al mapa de bloques del destino y **pasaron a cumplir la norma en vez de apartarse de ella**. Ver §0
**Fecha:** 2026-08-29
**Autor:** Mesa evaluadora del 2026-08-29
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Origen:** hallazgo `H-03` de [`../../Audit/Mesa-2026-08-29.md`](../../Audit/Mesa-2026-08-29.md), levantado por el Product Owner el 2026-08-29
**Trazabilidad upstream:** `Root-Rules.md` **8.6** §9.1, §9.2 y §9.5 · `Migracion-Rules.md` §4.3.1 pasada 1.b
**Trazabilidad downstream:** las dos `Pipeline-CI-CD.md`, las dos `Criterios-Validacion.md`, `Roadmap-Producto.md` §2.2

---

## 0. Por qué este ADR se retira sin haber sido aceptado

**El Product Owner lo evaluó el 2026-08-29 y eligió la salida que hace innecesario el apartamiento.**
Este documento **se conserva** —no se borra— porque su §1 es el diagnóstico que llevó a la decisión, y
porque un apartamiento propuesto y descartado es tan informativo como uno aceptado.

**Los dos defectos que la evaluación le encontró:**

1. **Inventaba un ámbito que el framework no tiene.** `Root-Rules.md` §9.1 declara **exactamente dos**
   —el producto y el conjunto normativo vigente— y «proyecto de código» no es uno. Lo que este ADR
   presentaba como un apartamiento de **ancho** era, en realidad, uno de **ámbito**: mucho más grande,
   y sobre un conjunto que §9.1 declara cerrado.
2. **Citaba `ADR-14002` para conservar el ancho, y citaba la mitad equivocada.** Aquél conservó el
   ancho de diez familias con este motivo: *«renumerarlas no reconecta nada con nada: elige un número
   nuevo»*. Es cierto de aquellas diez —colecciones internas de un documento, **que no colisionan**— y
   **falso acá**: `QG` colisiona 15 de 15 y `CV` 35 de 40. Renumerarlas **sí reconecta algo**.

**Y el dato que lo decidió: el destino ya tenía un mapa de bloques, y estas dos familias eran de las
pocas que lo ignoraban.**

| Bloque | `00` | `02` | `04` | `06` | `08` | `10` | `12` |
|---|---|---|---|---|---|---|---|
| Proyecto de código | Api | Domain | Application | Infrastructure | Contracts | Web | Visor |

`US`, `CU`, `RN`, `RC`, `ADR`, `VER` y `SD` lo usan. Es exactamente el caso de `RN` en `ADR-14002` —el
único que **sí se renumeró**— y no el de las diez: *«había una forma vigente y una cita que no la
usaba»*.

**Qué se hizo en su lugar, el 2026-08-29:** el tramo **`R-4`** renumeró **507 ocurrencias** en 28
documentos al mapa de bloques. Las dos familias pasan a **cinco dígitos** y a **ámbito producto**, que
son los que `Root-Rules.md` §9.2 y §9.1 piden. **No queda apartamiento que declarar.**

**Y las 278 que no se pudieron renumerar quedaron a la vista, que era el punto.** Su bloque no se podía
deducir ni de su línea ni de su sección, y **no se inventó**: conservan la forma `QG-NN`, que después
del renumerado **ya no resuelve**. Pasaron de ser 278 ambigüedades invisibles a **278 referencias rotas
que la compuerta mecánica de `Master-Prompt.md` §10.0 levanta**. Su inventario está en
[`../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../Audit/Inventario-Renumerado-R-4-2026-08-29.md).

**Lo que este retiro le debe al reporte `19`.** Aceptar este ADR habría creado exactamente la deuda que
ese reporte denuncia: una regla declarada hacia adelante y un corpus que no la cumple, sin mecanismo
que lo alcance. **El reporte se escribió el mismo día, y su primer efecto fue que su propio caso
dejara de existir.**

---

## 1. Contexto

**El Product Owner preguntó por qué `QG-03` no es `QG-00003`, y la respuesta no estaba escrita en
ninguna parte.** Al buscarla apareció algo más grande que el ancho.

`Root-Rules.md` §9.2 fija **cinco dígitos** para «toda familia que catalogue elementos de una colección
de un producto», y enumera veinticinco prefijos **«y equivalentes»**. `QG` no está en esa lista, pero
tampoco está en ninguna otra:

| Familia | Qué cataloga | Dos dígitos | Cinco dígitos | ¿Quién la declara? |
|---|---|---|---|---|
| `QG` | Puertas de calidad del pipeline | **933** | 0 | **Nadie** |
| `CV` | Criterios de validación | **374** | 0 | **Nadie** |
| `PT` | Puertas técnicas | **834** | 0 | **Nadie** |
| `SD` | Sondas de sensado de deriva | 175 | 525 | `Deriva-Rules.md`, en cinco dígitos |
| `TC` | Casos de prueba | 33 | 1778 | `Root-Rules.md` §9.2, en cinco dígitos |

**Las tres primeras no las acuña ninguna regla del framework y el intake no las menciona ni una vez.**
Las acuñó este destino, y `Root-Rules.md` §9.5 exige que **toda familia declare prefijo, forma y
ámbito en la regla que la acuña**. Las tres declaran **cero de los tres**.

**Y el ancho es el síntoma menor.** El grave es el ámbito:

> `QG-00003` en `GeometriaFactory-Api` pide **75 % de líneas y 70 % de ramas**.
> `QG-02003` en `GeometriaFactory-Domain` pide **90 y 85**.
> `QG-04003` en `GeometriaFactory-Application` pide **85 y 80**.

**El mismo identificador nombra tres puertas con tres umbrales distintos.** `QG-05` tiene **cinco**
enunciados distintos y `QG-14` **dos**. Un identificador que no es único en su ámbito **no es una
dirección**, y `Root-Rules.md` §9.1 lo pide por ese motivo: para que una cita resuelva.

---

### 1.1 Corrección del ciclo 2 de la mesa: **`PT` no va, y su inclusión era un error de esta emisión**

**La emisión 1.0 puso a `QG`, `CV` y `PT` en la misma bolsa porque las tres compartían un síntoma
—dos dígitos, sin declaración— y no verificó si compartían el defecto.** El ciclo 2 lo verificó y
**no lo comparten**.

**Se midió cuántos identificadores de cada familia tienen más de un enunciado en el corpus:**

| Familia | Identificadores con enunciado | Con **más de uno** | Lectura |
|---|---|---|---|
| **`QG`** | 15 | **15** | **Colisiona entera.** `QG-01` es «construcción sin advertencias» en un proyecto de código y «el bundle se genera sin errores» en otro |
| **`CV`** | 40 | **35** | **Colisiona casi entera.** `CV-02` es «la batería del validador pasa» acá y «los quince puntos de acceso están ejercidos» allá |
| **`PT`** | 4 | **0** | **NO colisiona.** `PT-04` es «que la imagen del servicio de datos se construya y arranque» **en los diecisiete lugares donde aparece** |

**`PT` es del producto y ya está declarada como tal**, en
[`../../00-Contexto/Roadmap-Producto.md`](../../00-Contexto/Roadmap-Producto.md) §2.2, que enumera sus
cinco puertas **una sola vez para todo el producto** y dice dónde se mide cada una. No le falta ámbito:
lo tiene, es el producto, y su tabla canónica lo declara. **Sus 169 citas sin calificador no son
ambiguas**, y calificarlas habría sido agregar ruido a un identificador que ya resuelve.

**Qué se corrige, entonces:** este ADR alcanza a **`QG` y `CV`** y **no a `PT`**. Su recuento pasa de
2141 ocurrencias a **1005**, y el de citas realmente ambiguas de 458 a **289**.

**Y por qué queda escrito en lugar de reescribir la emisión 1.0.** El error es del método con que se
armó el ADR —agrupar por síntoma sin verificar el defecto— y borrarlo dejaría el ADR correcto y la
lección perdida. Es el mismo criterio con el que la mesa registra sus parches rechazados.

---

## 2. Decisión

**`QG` y `CV` declaran su ámbito y conservan su ancho de dos dígitos, y las dos cosas se deciden por
separado porque tienen fundamentos distintos.** `PT` queda fuera por §1.1.

1. **El ámbito de `QG` y `CV` es el proyecto de código**, y se declara. No es una elección
   nueva: **es el ámbito que las tres ya ejercen**, y lo único que faltaba era escribirlo. Toda cita
   desde fuera del proyecto de código **lo nombra**: `QG-03 de -Domain`, nunca `QG-02003` a secas.
2. **Las dos conservan el ancho de dos dígitos**, con este apartamiento declarado.

---

## 3. Motivo

**Por qué el ámbito sí se declara y no se discute.** Es la diferencia entre un apartamiento y un
defecto. Un identificador ambiguo dentro de su ámbito declarado es un defecto; uno unívoco en un ámbito
más chico que el del framework es un apartamiento, y se declara. Hoy no hay ámbito escrito, de modo que
**no se puede saber cuál de los dos es** — y ésa es la parte que no puede quedar así.

**Por qué el ancho no se renumera, con el precedente exacto.** `ADR-14002` resolvió esta misma pregunta
para once familias del intake y la partió en dos: `RN` **se renumeró** porque el árbol ya la numeraba
con cinco dígitos y convivían dos números para la misma regla —*«no había dos formas legítimas: había
una forma vigente y una cita que no la usaba»*—; las otras diez **conservaron su ancho** porque
renumerarlas **no reconecta nada: elige un número nuevo**.

**`QG`, `CV` y `PT` son el segundo caso y no el primero.** Ninguna existe en el árbol con cinco
dígitos: los recuentos de §1 son **933 / 0**, **374 / 0** y **834 / 0**. Renumerar 2141 ocurrencias no
haría resolver mejor ninguna referencia —`QG-03 de -Domain` resuelve hoy y `QG-00003 de -Domain`
resolvería igual— y **el ámbito, que es el defecto real, quedaría igual de sin declarar**.

**Y hay un precedente todavía más cercano: `D10`.** El 2026-08-24 el Product Owner decidió para la
familia `PD` —los puntos abiertos que la categoría 09 acuña— exactamente esta forma: *«`PD-NN`, sin
familia nueva, con ámbito **el documento**»*. Este ADR aplica el mismo criterio a las tres que
quedaron sin decidir, con el ámbito que corresponde a cada una.

---

## 4. Consecuencias

**A favor.** Las 2141 ocurrencias no se tocan, y **la ambigüedad real queda cerrada**: una cita de
`QG-03` sin proyecto de código pasa a ser un defecto detectable en vez de una lectura posible.

**En contra, y es real.** El corpus queda con **dos anchos** conviviendo: cinco dígitos en las familias
del framework y dos en éstas y en las once de `ADR-14002`. Se mitiga con lo mismo que lo mitiga allá:
que el apartamiento esté declarado y que el ancho sea consistente **dentro de** cada familia.

**Lo que este ADR NO hace, y hay que decirlo.** **No repara las citas ambiguas que ya existen.** Declara
la regla hacia adelante; el barrido de las citas de `QG-NN` sin proyecto de código es trabajo propio, y
queda como ítem diferido:

> **Ítem diferido (`Root-Rules.md` §12.2) · las 289 citas ambiguas de `QG` y `CV`.**
> **1 · Qué falta:** calificar con su proyecto de código las **289** citas —204 de `QG` y 85 de `CV`—
> que no lo nombran ni en su línea ni en su sección envolvente. Las otras **716 de las 1005 ya
> resuelven** por su contexto, y `PT` queda fuera por §1.1.
> **2 · Por qué no se puede hoy:** **no es mecánico y no es deducible del texto.** Las 289 son
> exactamente las que quedaron después de descartar las que su línea o su sección desambigua: para
> cada una hay que decidir a qué proyecto de código se refería quien la escribió, y eso es
> interpretación. Es el mismo motivo con el que la mesa se negó a reconstruir el mapeo de las 40
> historias pronosticadas: deducirlo del texto sería inventarlo.
> **3 · Quién lo cierra:** las categorías 08 y 09 de las dos unidades de entrega, que son las que
> acuñaron las dos familias.
> **4 · En qué evento se cierra:** la **Fase J**, en su revisión de huecos y contradicciones entre
> documentos.

**Y hay que decir por qué este ítem existe en vez de repararse, porque no es una elección de este
ADR.** `Root-Rules.md` §11 pide seis campos a todo apartamiento y **los seis son sobre la decisión**:
qué obligación no se cumple, por qué, qué se descartó, qué la superaría, su estado y cuántos saltos
sobrevivió. **Ninguno es sobre el corpus que la decisión deja atrás.** Un apartamiento declara una
regla hacia adelante y el método **no tiene etapa que barra lo que la precede** — la revisión de
`Migracion-Rules.md` §4.7 mira el ADR contra la normativa, nunca contra el corpus que gobierna.
Elevado al framework como el **reporte `19`**.

---

## 5. Alternativas descartadas

| Alternativa | Por qué se descartó |
|---|---|
| **Renumerar a cinco dígitos** | 2141 ocurrencias reescritas **sin que ninguna referencia resuelva mejor**, y el defecto real —el ámbito sin declarar— quedaría intacto. Es el fundamento textual de `ADR-14002` §3 |
| **Ámbito producto, renumerando para evitar la colisión** | Obligaría a **quince** puertas distintas donde hoy hay quince números reusados en cuatro proyectos de código, y a reescribir los umbrales de `D1` uno por uno. Cambia el contenido para arreglar la forma |
| **Dejarlo como está** | Es lo que había, y es lo que hizo que el Product Owner tuviera que preguntar. Un identificador sin ámbito declarado **no se puede auditar**: no hay contra qué comparar una colisión |

---

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 2.0 | 2026-08-29 | **RETIRADO sin haber sido aceptado**, por decisión del Product Owner, y §0 dice por qué. La evaluación le encontró dos defectos: **inventaba un ámbito que `Root-Rules.md` §9.1 no tiene** —presentado como apartamiento de ancho cuando era de ámbito— y **citaba la mitad equivocada de `ADR-14002`**, cuyo motivo para conservar el ancho *«renumerarlas no reconecta nada»* vale para diez familias que **no colisionan** y no para dos que colisionan **15 de 15** y **35 de 40**. El dato que lo decidió: **el destino ya tenía un mapa de bloques** y estas dos eran de las pocas que lo ignoraban. En su lugar corrió el tramo **`R-4`**: **507 ocurrencias renumeradas** y las **278** sin bloque deducible **dejadas a la vista como referencias rotas y detectables**. Sube MAJOR: el documento pasa de proponer un apartamiento a registrar por qué no hizo falta. | Product Owner y mesa evaluadora |
| 1.1 | 2026-08-29 | **Corrección del ciclo 2 de la mesa, en §1.1: `PT` sale del alcance.** La emisión 1.0 agrupó a las tres familias **por su síntoma —dos dígitos, sin declaración— sin verificar que compartieran el defecto**, y no lo comparten: medidos los enunciados por identificador, `QG` colisiona **15 de 15**, `CV` **35 de 40** y **`PT` cero de cuatro**. `PT` es del producto, su tabla canónica está en `Roadmap-Producto.md` §2.2 y **sus 169 citas no son ambiguas**. El alcance pasa de 2141 ocurrencias a **1005** y las citas ambiguas de 458 a **289**. El ítem diferido de §4 se rehace con el recuento verdadero y **declara por qué existe en vez de repararse**: `Root-Rules.md` §11 pide seis campos y los seis son sobre la decisión, ninguno sobre el corpus que deja atrás. Elevado al framework como reporte `19`. | Mesa evaluadora, ciclo 2 |
| 1.0 | 2026-08-29 | Emisión inicial, **a partir de una pregunta del Product Owner** —«¿no debería ser `QG-00003`?»— que al buscarse la respuesta destapó algo mayor: **`QG`, `CV` y `PT` son familias que el destino acuñó sin declarar prefijo, forma ni ámbito**, contra `Root-Rules.md` §9.5. Declara el **ámbito de proyecto de código** —que las tres ya ejercen— y **conserva el ancho de dos dígitos**, con el precedente de `ADR-14002` para el ancho y el de `D10` para la forma. Deja escrito que **`QG-03` nombra hoy tres puertas con tres umbrales distintos**, que es el defecto de fondo, y que **repararlo no es de este ADR**: queda como ítem diferido con evento en la Fase J. | Mesa evaluadora del 2026-08-29 |
