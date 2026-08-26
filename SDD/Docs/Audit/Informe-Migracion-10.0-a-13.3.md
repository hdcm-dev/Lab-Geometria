# Informe de migración normativa — SDD 10.0 → 13.3

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-10.0-a-13.3.md
**Versión:** 1.0
**Fecha:** 2026-08-25
**Instrumento:** `Master-Prompt-Migracion.md` **2.8**, fase **M6**
**Auditor:** subagente auditor, **no independiente** — ver §0
**Veredicto:** **APROBADO CON HALLAZGOS**, en segunda ronda. La primera **RECHAZÓ** con tres P0
**Declaración:** **MIGRACIÓN COMPLETA**

---

## 0. El auditor no fue independiente, y esto va antes que el veredicto

**`Master-Prompt.md` §10 declara qué compra la independencia y qué no, y de eso depende cómo se lee
este informe:**

> *«**Sí compra: ausencia de compromiso.** El agente que tomó una decisión tiene interés en que la
> respuesta sea que estuvo bien. Eso no se corrige con más contexto ni con mejor prompt: es
> estructural de quien decidió. **No compra: independencia de criterio.** Dos agentes del mismo modelo,
> leyendo las mismas reglas, tienden a coincidir ante una pregunta abierta. Una confirmación
> correlacionada se lee como verificación sin serlo.»*

**Qué tuvo este auditor y qué no.** No ejecutó ninguno de los cortes, de modo que **la ausencia de
compromiso sobre el trabajo migrado sí la tuvo**. Lo que no tuvo es independencia respecto de **sus
propios veredictos anteriores**: auditó los tres cortes y sus rondas, y varias de las reparaciones que
después juzgó existen porque él las pidió. En la segunda ronda de M6 el compromiso fue mayor todavía,
porque el commit auditado repara los P0 que él mismo levantó.

**Lo que sí queda sostenido con evidencia**, y no depende del criterio: los recuentos, las versiones,
la fidelidad de los snapshots, la resolución de los enlaces y la existencia de cada campo. Todo eso se
midió **de cero sobre el árbol** y está citado.

**Lo que queda sin verificar, y se declara en lugar de darse por bueno:** las tres preguntas de
criterio de esta migración —si **«no aplica»** es figura legítima frente a un ítem obligatorio sin
objeto, si el **cierre por lectura** de la estimación fue correcto, y si la **partición quirúrgica** en
lugar de la re-expresión completa era admisible—. El auditor las contestó igual las siete veces que se
las preguntó, y eso es exactamente la **confirmación correlacionada** que §10 advierte que no vale.

**Recomendación formal del auditor, que este informe adopta:** encargar una **segunda ronda con
auditor invocado desde cero**. Es la misma deuda que dejó la migración 9.12 → 10.0 —cuyo informe
declara en su §0 que su auditor tampoco fue independiente— y `Plan-Cierre-De-Pendientes.md` §5 declara
que **caduca**: cuanto más se construya encima, menos dice. **Esta migración construyó 167 documentos
encima.**

---

## 1. Qué se migró

| Campo | Valor |
|---|---|
| Origen | **SDD 10.0**, declarada en `PRODUCT-MANIFEST` 4.0 §1.1 |
| Vigente | **SDD 13.3** |
| Migración número | **Séptima** de este destino, y **la primera que atraviesa tres saltos major** |
| Plan | [`Plan-Migracion-10.0-a-13.3.md`](Plan-Migracion-10.0-a-13.3.md) **1.8** |
| Documentos vivos tocados | **167** |
| Estados previos archivados | **160** |
| Cortes de M4 | **Tres**, cada uno con su audit: la 09, el README raíz y la 06 |

**Las tres reglas que subieron major y alcanzaron artefactos:** `Root-Rules` **7.0 → 8.4**,
`Rules-Backlog-Tecnico` **4.4 → 5.1** y `Rules-Devops` **5.0 → 6.1**.

---

## 2. Los seis P0 de `Master-Prompt-Migracion.md` §10

| # | Criterio | Veredicto final |
|---|---|---|
| 1 | Contenido inventado | **CUMPLE** |
| 2 | Sección exigida rellenada con contenido inferido | **CUMPLE** |
| 3 | Procedencia reescrita con migración parcial | **CUMPLE** tras reparación — ver §4 |
| 4 | Corrección manual pisada sin declarar la interpretación | **CUMPLE**, con la reserva de §5 |
| 5 | Estado previo de un documento migrado sin archivar | **CUMPLE** tras reparación — ver §4 |
| 6 | Fila del plan sin resolver y sin declararse | **CUMPLE** tras reparación — ver §4 |

**Los tres que no cumplían son los que produjeron el RECHAZO de la primera ronda**, y los tres se
repararon el mismo día.

---

## 3. Estado final de cada fila del plan

| Fila | Qué exigía | Estado |
|---|---|---|
| **§4.1** · Estimación como ítem propio | Partir §5 en 5 y 5.b en **144** `US-*.md` | **RESUELTA** — 144/144, 0 con la forma empaquetada |
| **§4.2** · Los cuatro ítems `.b` de la 09 | Un ítem propio por cada `.b` | **RESUELTA** — 10 subsecciones emitidas en 6 documentos |
| **§4.3** · La cita del rol | Forma de cinco dígitos en la Tabla A | **RESUELTA** — 5 filas, 0 residuo en la tabla |
| **§5** · Revisión de apartamientos | Escribir el campo 6 de los tres ADR | **RESUELTA** — **fue el P0 de la primera ronda** |
| **§5.2** · Hallazgos elevados | Registrar `HM-01`, `HM-02` y `HM-03` | **RESUELTA** |
| **§6** · Fuera de alcance | Respetarlo | **RESUELTA con reserva** — las ediciones en `Audit/` agregan hechos, no reescriben veredictos |
| **§7.1** · Partición quirúrgica | Declararla como apartamiento de §4.3 | **RESUELTA** |
| **§8 · M2** · intake | Sin filas | **RESUELTA** — `§17.P.13` es opcional, impacto «Ninguno» |
| **§8 · M3** · manifiesto | Sin filas | **RESUELTA** — plantilla en 6.0 |
| **§8 · M4** | 151 documentos + la revisión de apartamientos | **COMPLETA**, con su demora declarada |
| **§8 · M5** · procedencia | Reescribirla sólo con la cadena completa | **EJECUTADA**, con su condición cumplida después y declarada |

**Once de once. Ninguna sin resolver, ninguna parcial.**

---

## 4. Los tres P0 de la primera ronda, y por qué importan más que el veredicto

### 4.1 La revisión de apartamientos nunca se ejecutó

**El plan mandaba, en su §8: «M4 … más la revisión de apartamientos de §5, que escribe el campo 6 de
los tres ADR».** M4 no los tocó. El último commit sobre `ADR-14001` y `ADR-14002` era del **2026-08-17**
y sobre `ADR-14003` del **2026-08-18**, los tres **anteriores al plan**. El razonamiento estaba hecho y
escrito en §5.1 **en tiempo futuro**; el acto de escribirlo donde el método lo va a leer la próxima
vez, no.

**Y de ahí salieron los otros dos criterios**: fila del plan sin resolver y sin declarar, y procedencia
reescrita sobre una cadena incompleta — porque M5 declaró «ninguna fila del plan quedó sin resolver»
habiendo cerrado **cuatro de cinco**, y `PRODUCT-MANIFEST` §1.1 se apoyó en esa frase.

**Es el defecto que esta misma migración eleva al framework.** `HM-02` describe una obligación atada a
un evento futuro que llega, pasa y no deja rastro de que nadie la miró. **Le pasó a la migración que lo
eleva**, y el auditor lo dijo con esas palabras: *«`HM-02` describe su propio cierre»*.

**Reparado:** los tres ADR en **1.2** —`ADR-14003` en 1.3— con sus contadores en **3, 3 y 2**, sin
re-fundamentar ninguno: el diff de cada uno tiene cuatro hunks y **§1 a §5 quedaron intactos**, que es
lo que `Migracion-Rules.md` §6 exige.

### 4.2 Dos índices sin archivar

Los `README.md` de las dos carpetas `09-Devops` subieron de **2.0 a 2.2** en dos rondas **sin copiarse**,
y uno de los saltos ocurrió **en el mismo commit que archivaba a sus tres hermanos de carpeta**.
`Master-Prompt.md` §5.1 no admite excepciones de nombre y un `README.md` de categoría no está en su
tabla de exenciones. **Reparado**, con el cuerpo recuperado del historial y la demora declarada en el
propio bloque de archivado.

### 4.3 La procedencia declaraba lo que no era cierto

**Reparada**: §1.1 dejó de afirmar que ninguna fila quedó sin resolver y **declara el orden real** —qué
faltaba, cuándo se completó y con qué—, porque una procedencia que no dice cuándo se volvió cierta no
se puede auditar después.

---

## 5. Contenido sin destino

**Ninguno.** `Migracion-Rules.md` §4.2 punto 2 exige enumerarlo con su texto localizable. Se buscó
donde podía haberlo y se midió sobre el total:

| Dónde | Medida |
|---|---|
| Las 144 historias | La prosa de prioridad quedó **verbatim en 144 de 144**, cero diferencias reales |
| El párrafo de estimación | Tenía **tres** redacciones —116 / 27 / 1— y se unificaron en una. **La sustancia de la más larga está preservada** y atribuida a su fuente; cada historia conserva la suya en su `_legacy/` |
| Los seis `PA-01` | Sus enunciados se conservan; sólo cambió la columna de estado |
| La 09 | Las particiones **reunieron texto ya existente** sin descartar ninguno |

**Una reserva declarada, no un hallazgo.** La homogeneización de las tres redacciones se hizo sin
ejecutar el patrón de `Migracion-Rules.md` §4.2 punto 3 —enumerar las diferencias y esperar
confirmación—, porque no había forma de distinguir variación de generación de corrección manual del
usuario. **No concluyente**, y se declara.

**Y una deuda que sigue viva y no es contenido perdido sino contradicho:** las **130 celdas** de los dos
`Backlog-Tecnico.md` que decían `Sin fijar` remitiendo a un `Product-Backlog.md` §4.1 que hoy dice lo
contrario. Se propagó el cierre en la ronda 2 del corte 06; queda dicho porque el defecto original fue
**afirmar haberlo hecho antes de hacerlo**.

---

## 6. Los tres candidatos a regla del framework

`Migracion-Rules.md` §4.7 declara que un apartamiento que sobrevive **dos o más saltos** sin ser
contemplado ya demostró que **no es de un producto**. Los tres de este destino cruzan el umbral:

| ADR | Qué declara | Contador | Por qué sigue sin contemplarse |
|---|---|---|---|
| `ADR-14001` | El archivado de una migración estructural es **central** y no por carpeta | **3** | Ninguna entrada de la 10.1 a la 13.3 declara **cómo se archiva una migración estructural** |
| `ADR-14002` | Las familias propias del intake conservan el **ancho de origen** | **3** | La 12.0 toca `Root-Rules` §9 pero **no le da artefacto propio** a ninguna de esas familias |
| `ADR-14003` | La dirección del backend viaja como **IP pública dinámica** | **2** | Es **infraestructura del destino**: ninguna versión del framework la alcanza |

**Con la salvedad que el audit levantó y el plan §5.1.1 declara:** el contador de este destino mide
**saltos que alcanzaron artefactos**, no revisiones. Bajo la letra de `Root-Rules.md` §11 —que mide
revisiones— los valores serían 2, 2 y 1, y `ADR-14003` **no cruzaría**. La diferencia se eleva junto
con `HM-02` y `HM-03`.

---

## 7. Lo que esta migración destapa y no repara

| # | Hallazgo | Quién puede cerrarlo |
|---|---|---|
| **`HM-01`** | `PD-02` y `PD-03` de los dos `Pipeline-CI-CD.md` se cerraron «por lectura» **con el generador del inventario adentro y sin resolver** | La categoría 09 del destino |
| **`HM-02`** | **El método contrasta el diferimiento contra el calendario y no contra lo que el producto hizo.** Un ítem puede estar impecable y estar difiriendo una pregunta que los hechos ya cerraron | **El framework**, reporte `16` |
| **`HM-03`** | **La política de archivado que este destino aplica no está escrita en ninguna parte**, y sin ella un archivado legítimo y uno omitido se ven iguales | **El framework**, junto con `HM-02` |

---

## 8. Declaración

> ## MIGRACIÓN COMPLETA

Las **tres superficies** están cubiertas y medidas de cero. Las **once filas** del plan están
resueltas. Los **160 estados previos** están archivados y **no queda ningún documento migrado de
`SDD/Docs/` ni de `SDD/Intake/` sin el suyo**. Los **tres apartamientos** están revisados con su campo
6 escrito en el árbol. Las **24 versiones** de la procedencia coinciden con el framework, verificadas
una por una contra las cabeceras de sus archivos.

**`PRODUCT-MANIFEST` §1.1 declara SDD 13.3 y se sostiene**, y declara además **cuándo se volvió
cierta**.

**Lo que queda abierto, por diseño y declarado:** `PD-10`, el generador del inventario, vigente con
evento en la fase `i`; `ADR-14004`, que sigue **`Propuesto`** y espera la aprobación del Product
Owner; y la **segunda ronda de auditoría con auditor invocado desde cero**, que §0 recomienda y que
este informe no reemplaza.

---

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-25 | Emisión inicial. Informe de la **séptima** migración normativa de este destino, **10.0 → 13.3**, y la primera que atraviesa **tres saltos major**. **§0 declara antes que nada que el auditor no fue independiente**, con el criterio de `Master-Prompt.md` §10: tuvo la ausencia de compromiso sobre el trabajo migrado y **no** la tuvo sobre sus propios veredictos, y las **tres preguntas de criterio** —«no aplica», el cierre por lectura y la partición quirúrgica— quedan **no verificadas por correlación**, con la recomendación formal de una segunda ronda desde cero. Veredicto **APROBADO CON HALLAZGOS en segunda ronda**: la primera **RECHAZÓ con tres P0**, y §4 los desarrolla porque **importan más que el veredicto** — el que los produjo es el mismo defecto que esta migración eleva al framework como `HM-02`, de modo que **el hallazgo describe su propio cierre**. Declara **MIGRACIÓN COMPLETA** con las once filas del plan resueltas, **160 estados previos archivados** y **ningún contenido sin destino**. Enumera los **tres candidatos a regla del framework** con su contador y la salvedad de qué mide, y los **tres hallazgos** que el destino no repara. | Auditor (no independiente — ver §0) y Orquestador de migración normativa SDD |
