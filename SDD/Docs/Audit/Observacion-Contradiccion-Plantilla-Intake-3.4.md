# Observación aguas arriba — La plantilla de intake 3.4 se contradice a sí misma

**Producto:** Fábrica de Geometría
**Documento:** Observacion-Contradiccion-Plantilla-Intake-3.4.md
**Versión:** 1.0
**Estado:** **ELEVADA** — no la puede cerrar este destino
**Fecha:** 2026-08-17
**Autor:** Orquestador SDD
**Instrumento:** `Master-Prompt.md` §9, manejo de ambigüedad: un dato que el producto no puede resolver por su cuenta **se eleva y no se decide**
**Alcanza a:** `PRODUCT-INTAKE-template.md` **3.4** del repositorio del framework
**Origen:** hallazgo `A-06` de [`Informe-Migracion-8.11-a-9.9.md`](Informe-Migracion-8.11-a-9.9.md) §6

---

## 1. Qué se encontró

`PRODUCT-INTAKE-template.md` **3.4** declara el nivel de su Parte C de **tres** maneras, y **una de
las tres dice lo contrario que las otras dos**:

| Dónde | Qué dice | Nivel |
| --- | --- | --- |
| §15, tabla de contenido | «Parte C — Técnica **por unidad de entrega** (§17 a §18). Las decisiones de construcción, en un bloque repetible por cada **unidad de entrega** declarada en §13.1» | Entrega |
| §19, checklist | «Técnica **por unidad de entrega** (Parte C)» · «§17 está completo para cada **unidad de entrega** vigente de §13.1… **No hay un bloque §17 por proyecto de código**» | Entrega |
| **Línea 480, el encabezado de la sección** | **«# Parte C — Técnica por proyecto de código»** | **Construcción** |

**El encabezado es el que un agente lee primero al llegar a la Parte C**, y es el que quedó en el eje
anterior a la 8.0.

---

## 2. Por qué se eleva en lugar de resolverse

**El destino no puede corregir la plantilla**: vive en el repositorio del framework, que este producto
lee en **solo lectura**. Y no puede tampoco elegir en silencio cuál de las dos lecturas seguir,
porque elegir es decidir sobre la normativa.

**Lo que sí hizo, y es la salida que el método prevé:** el intake migrado **escribe «por unidad de
entrega»**, el concepto que la plantilla declara en su estructura, en su checklist y en su tabla de
contenido — y emite esta observación en lugar de copiar la contradicción.

---

## 3. El antecedente, que hace esto más que un typo

**Es la segunda vez que este destino levanta una contradicción interna de esta misma plantilla.**

La primera fue en la fase M2 de la migración 6.0 → 8.6: §17 le pedía `tipo_unidad_entrega` (D8) y
`redistribuible` **al proyecto de código**, contra lo que §13.2 de la misma plantilla declaraba. El
agente que completaba el intake **emitió la contradicción como hallazgo aguas arriba en lugar de
copiarla**, y el framework la recogió en la versión **8.7**, con la plantilla pasando a **3.1**.

El `CHANGELOG` 8.7 lo dice con esas palabras. **La plantilla 3.1 recoge lo que este intake ya había
resuelto.**

**Las dos veces el defecto tiene la misma forma**: un cambio de eje que se propagó a la prosa y no a
un lugar puntual —allá una tabla, acá un encabezado—. Es el patrón que el propio framework nombró en
la 8.15: **«el concepto sobrevive en la tabla que se ejecuta, no en la prosa que se lee»**, y su
variante de la 8.17: **un encabezado dentro de un bloque de ejemplo no se lee, se copia**.

---

## 4. Qué se propone, y qué se pide

**Propuesta:** que el encabezado de la línea 480 pase a **«# Parte C — Técnica por unidad de
entrega»**, alineándolo con §15 y §19. Es un cambio de una línea y **no altera ninguna instrucción**:
las doce subsecciones P.1 a P.12 ya están redactadas al nivel de la entrega desde la 3.1.

**Lo que se pide:** nada de este destino. La observación queda **elevada** y se cierra cuando el
framework publique la corrección. Este documento existe para que el hallazgo **no viva sólo dentro de
un informe de migración**, donde la próxima persona que abra la plantilla no lo va a encontrar.

**Verificado el 2026-08-17 contra el conjunto 9.12**, el vigente: la contradicción **sigue en pie**.
La plantilla no cambió de versión desde la 3.4.

---

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Eleva el hallazgo `A-06` del informe de migración 8.11 → 9.9 a observación propia, para que no viva sólo dentro de un informe. `PRODUCT-INTAKE-template` **3.4** declara su Parte C «por unidad de entrega» en §15 y en el checklist de §19, y **«por proyecto de código» en su encabezado de sección**. El destino escribió el concepto vigente y **emitió la contradicción en lugar de copiarla**, que es exactamente lo que ya hizo una vez y el framework recogió en la **8.7**. Verificado contra el conjunto **9.12**: sigue en pie. | Orquestador SDD |
