# ADR-14005 — Las familias que el destino acuñó declaran ámbito de proyecto de código y conservan su ancho

**Producto:** Fábrica de Geometría
**Documento:** ADR-14005-Familias-Acunadas-Por-El-Destino-Con-Ambito-De-Proyecto.md
**Versión:** 1.0
**Estado:** **Propuesto** — requiere la aceptación del Product Owner
**Fecha:** 2026-08-29
**Autor:** Mesa evaluadora del 2026-08-29
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Origen:** hallazgo `H-03` de [`../../Audit/Mesa-2026-08-29.md`](../../Audit/Mesa-2026-08-29.md), levantado por el Product Owner el 2026-08-29
**Trazabilidad upstream:** `Root-Rules.md` **8.6** §9.1, §9.2 y §9.5 · `Migracion-Rules.md` §4.3.1 pasada 1.b
**Trazabilidad downstream:** las dos `Pipeline-CI-CD.md`, las dos `Criterios-Validacion.md`, `Roadmap-Producto.md` §2.2

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

> `QG-03` en `GeometriaFactory-Api` pide **75 % de líneas y 70 % de ramas**.
> `QG-03` en `GeometriaFactory-Domain` pide **90 y 85**.
> `QG-03` en `GeometriaFactory-Application` pide **85 y 80**.

**El mismo identificador nombra tres puertas con tres umbrales distintos.** `QG-05` tiene **cinco**
enunciados distintos y `QG-14` **dos**. Un identificador que no es único en su ámbito **no es una
dirección**, y `Root-Rules.md` §9.1 lo pide por ese motivo: para que una cita resuelva.

---

## 2. Decisión

**Las tres familias declaran su ámbito y conservan su ancho de dos dígitos, y las dos cosas se deciden
por separado porque tienen fundamentos distintos.**

1. **El ámbito de `QG`, `CV` y `PT` es el proyecto de código**, y se declara. No es una elección
   nueva: **es el ámbito que las tres ya ejercen**, y lo único que faltaba era escribirlo. Toda cita
   desde fuera del proyecto de código **lo nombra**: `QG-03 de -Domain`, nunca `QG-03` a secas.
2. **Las tres conservan el ancho de dos dígitos**, con este apartamiento declarado.

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

> **Ítem diferido (`Root-Rules.md` §12.2) · las citas de `QG` sin ámbito.**
> **1 · Qué falta:** barrer las citas de `QG-NN`, `CV-NN` y `PT-NN` que no nombran su proyecto de
> código y calificarlas.
> **2 · Por qué no se puede hoy:** son **2141 ocurrencias** y la calificación no es mecánica — una cita
> dentro del documento de su propio proyecto de código **no necesita** el calificador.
> **3 · Quién lo cierra:** las categorías 08 y 09 de las dos unidades de entrega.
> **4 · En qué evento se cierra:** la **Fase J**, en su revisión de huecos y contradicciones entre
> documentos.

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
| 1.0 | 2026-08-29 | Emisión inicial, **a partir de una pregunta del Product Owner** —«¿no debería ser `QG-00003`?»— que al buscarse la respuesta destapó algo mayor: **`QG`, `CV` y `PT` son familias que el destino acuñó sin declarar prefijo, forma ni ámbito**, contra `Root-Rules.md` §9.5. Declara el **ámbito de proyecto de código** —que las tres ya ejercen— y **conserva el ancho de dos dígitos**, con el precedente de `ADR-14002` para el ancho y el de `D10` para la forma. Deja escrito que **`QG-03` nombra hoy tres puertas con tres umbrales distintos**, que es el defecto de fondo, y que **repararlo no es de este ADR**: queda como ítem diferido con evento en la Fase J. | Mesa evaluadora del 2026-08-29 |
