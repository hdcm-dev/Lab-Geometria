# ADR-14004 — Un ítem obligatorio sin objeto se declara «no aplica», y no se difiere

**Producto:** Fábrica de Geometría
**Documento:** ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md
**Versión:** 1.1
**Estado:** **Propuesto** — esperando la aprobación del Product Owner
**Fecha:** 2026-08-24
**Autor:** Orquestador de migración normativa SDD
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Alcanza a:** `Entornos-Deploy.md` §2.b de `GeometriaFactory-Api` y §3.b de `GeometriaFactory-Web`, frente a `Rules-Devops.md` §4.4 punto 2.b
**Trazabilidad upstream:** `Audit/Plan-Migracion-10.0-a-13.3.md` 1.0 §4.2 · `Rules-Devops.md` **6.0** §4.4 · `Root-Rules.md` **8.4** §12.2

---

## 1. Contexto

**La migración 10.0 → 13.3 trajo cuatro ítems partidos, y uno de ellos no tiene objeto en este
producto.** `Rules-Devops.md` §4.4 punto 2.b, desde la regla 6.0, exige que la categoría 09 declare la
**aprobación de `plan` antes de `apply`** como ítem propio, separado de la herramienta de
infraestructura declarativa. El motivo que la regla escribe es correcto y este ADR no lo discute: es
una política de proceso, se enuncia en términos neutros y **vale para las cuatro herramientas que la
regla nombra**, de modo que esperar a elegir herramienta para declararla sería diferir por arrastre.

**El problema es anterior a esa neutralidad.** Este producto **no tiene ninguna herramienta
declarativa de infraestructura**, y no por una decisión postergada:

- `Entornos-Deploy.md` §2.1 de `GeometriaFactory-Api`: *«Herramienta declarativa de infraestructura ·
  **Ninguna.** No hay nube que provisionar: el servidor domiciliario ya existe»*, con fundamento en el
  intake §10 y sus tres piezas de infraestructura de costo cero.
- `Entornos-Deploy.md` §3.1 de `GeometriaFactory-Web`: *«**No hay infraestructura declarativa**, y la
  ausencia es de la fuente y no de esta categoría»*: el hosting es un servicio de terceros que se
  contrata y se configura por fuera del repositorio.

**Sin herramienta no hay `plan` y no hay `apply`.** El ítem no está bloqueado esperando un dato: **no
tiene sujeto**.

---

## 2. Decisión

**Un ítem obligatorio de una `§4.x` cuyo objeto no existe en este producto se declara «no aplica», con
su motivo y su condición de reapertura, y no se difiere con la forma de `Root-Rules.md` §12.2.**

Se aplica hoy a un solo ítem —§4.4 punto 2.b— y **se declara como criterio y no como excepción de una
fila**, porque la próxima partición de la próxima versión del framework va a producir el mismo caso.

**La declaración obligatoria tiene tres partes**, y sin las tres el ítem queda incompleto:

1. **Que no aplica**, dicho con esas palabras y en el lugar donde el ítem se contestaría.
2. **Por qué no tiene objeto**, con cita a la sección del propio documento que lo declara.
3. **Qué lo reabriría** — la condición concreta que le devolvería sujeto.

---

## 3. Motivo

**`Root-Rules.md` §12.2 define el diferimiento por lo que no se puede contestar *hoy*:** *«Un ítem que
una §4.x declara obligatorio **y que no se puede contestar hoy** se difiere…»*. La figura presupone que
el ítem se va a contestar más adelante, y por eso su campo 4 exige **el evento de cierre nombrando
artefacto y sección**.

**Un ítem sin objeto no tiene evento de cierre que nombrar.** Escribirle uno obliga a inventarlo, y
`Migracion-Rules.md` §4.1 lo prohíbe. Escribirle uno vago —«cuando se adopte una herramienta»—
reproduce exactamente el defecto que el reporte `14` del framework documentó sobre este mismo
producto: **un ítem diferido hacia un evento que nadie iba a mirar**, que dejó ocho etapas sin poder
etiquetarse.

**Y hay un costo concreto de tratarlo como diferido.** La tabla de puntos abiertos de este destino
tiene hoy **33 filas vencidas y 11 sin evento**, y su frente de cierre está declarado en
`Audit/Plan-Cierre-De-Pendientes.md`. Agregar dos filas que **nadie puede cerrar nunca** —porque no
hay nada que decidir— degrada el instrumento: una tabla de pendientes donde algunas filas no son
pendientes deja de servir para saber qué falta.

**Lo que este ADR no dice.** No dice que la política de aprobación sea innecesaria, ni que dependa de
la herramienta. Dice que **este producto no tiene el objeto sobre el que la política se aplicaría**, y
que declararlo es más verdadero que prometer contestarlo.

---

## 4. Consecuencias

**A favor.** La tabla de puntos abiertos sigue conteniendo sólo cosas que alguien puede cerrar. Y el
lector de la 09 encuentra el ítem contestado en el lugar donde lo busca, en lugar de tener que deducir
de una ausencia si se omitió o si no aplica.

**En contra, y es real.** **«No aplica» es una tercera salida que `Rules-Devops.md` §4.4 punto 2.b no
prevé**: su texto ofrece dos —fijarlo hoy, o diferirlo con la forma de §12.2—. Un destino que abusara
de esta figura podría declarar «no aplica» sobre ítems que en realidad no quiso contestar, y **ninguna
comprobación mecánica lo distingue**: las dos declaraciones se ven iguales para un guion. Lo único que
las separa es la cita del campo 2, que este ADR vuelve obligatoria.

**Qué mitiga ese riesgo.** Que la declaración exija **citar la sección del propio documento donde el
objeto se declara inexistente**. Un «no aplica» sin esa cita es un ítem sin contestar disfrazado, y el
audit lo levanta como tal.

---

## 5. Alternativas consideradas

| Alternativa | Por qué no |
| --- | --- |
| **Diferirlo con la forma de §12.2** | Obliga a nombrar un evento de cierre que no existe. Las dos formas de escribirlo son peores: inventar uno concreto —que §4.1 prohíbe— o poner uno vago, que es el defecto del reporte `14` |
| **Declarar el ítem cumplido con «no hay herramienta»** | Contesta otra pregunta. El ítem pide la política de aprobación, no el inventario de herramientas, y darlo por cumplido dejaría el incumplimiento sin rastro |
| **Omitirlo en silencio** | Es lo que el documento hacía antes de este corte, y por eso la partición de la 11.0 existe: un ítem ausente no se distingue de un ítem que nadie miró |
| **Elegir una herramienta declarativa para que el ítem tenga objeto** | Agregar infraestructura a un producto que las fuentes declaran básico, para poder contestar una pregunta. Es la cola moviendo al perro |

---

## 6. Estado del apartamiento

| Campo | Valor |
| --- | --- |
| **1 · Qué obligación se aparta** | `Rules-Devops.md` §4.4 punto 2.b, que ofrece dos salidas —fijar hoy o diferir con §12.2— y no contempla un ítem sin objeto |
| **2 · Alcance** | Los dos `Entornos-Deploy.md` del producto, y **como criterio** todo ítem obligatorio futuro cuyo objeto no exista en este producto |
| **3 · Fundamento** | §3 de este documento |
| **4 · Disparadores que superarían la decisión** | Cualquiera de los dos: que **el framework incorpore la figura del ítem sin objeto** —con lo cual esto deja de ser un apartamiento y pasa a ser la regla—, o que **este producto adopte una herramienta declarativa de infraestructura**, con lo cual el ítem recupera sujeto y se contesta |
| **5 · Estado** | **`vigente` desde que el ADR se acepte.** Hoy el documento está **`Propuesto`**, y el audit del corte 09 levantó como **P3** que el campo declarara `vigente` mientras la cabecera decía `Propuesto`: un apartamiento no rige antes de ser aceptado, y los dos `Entornos-Deploy.md` lo declaran así |
| **6 · Saltos de versión que sobrevivió** | **0** — se emite en el conjunto **13.3** |

**Qué pasa si el contador llega a 2.** `Migracion-Rules.md` §4.7 declara que un apartamiento que
sobrevive dos o más saltos sin ser contemplado ya demostró que **no es de un producto**, y se declara
candidato a regla del framework. Acá esa lectura es especialmente fuerte: **la figura del ítem sin
objeto no tiene nada de particular de este producto**, y si dos saltos pasan sin que el framework la
incorpore, lo que el número va a estar diciendo es que al método le falta una salida.

---

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-24 | **Ronda 3 del corte 09.** El **campo 5** declaraba el apartamiento **`vigente`** mientras la cabecera del ADR decía **`Propuesto`**: un apartamiento **no rige antes de ser aceptado**, y el audit lo levantó como **P3**. Queda declarado que rige **desde que el ADR se acepte**, y los dos `Entornos-Deploy.md` suman la fila que dice que hoy se apoyan en un instrumento todavía no aprobado. **La decisión no cambia**: cambia lo que el documento afirma sobre su propia vigencia. |
| 1.0 | 2026-08-24 | Emisión inicial, **en la ronda 2 del corte 09** de la migración 10.0 → 13.3. Nace de un hallazgo **P2** del audit independiente de la ronda 1: el corte había introducido la figura «**no aplica, y no está diferida**» en los dos `Entornos-Deploy.md` **sin declarar el apartamiento** que `Root-Rules.md` §11 exige, y `Rules-Devops.md` §4.4 punto 2.b sólo ofrece dos salidas. Declara la figura **como criterio y no como excepción de una fila**, con sus tres partes obligatorias —que no aplica, por qué no tiene objeto con cita al propio documento, y qué lo reabriría—. Declara también el riesgo que ninguna comprobación mecánica cubre: **«no aplica» y «no lo contesté» se ven iguales para un guion**, y lo único que los separa es la cita del campo 2. |
