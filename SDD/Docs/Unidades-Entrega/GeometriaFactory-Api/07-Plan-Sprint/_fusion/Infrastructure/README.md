# 07 · Plan de sprint — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Maintainer Lead (AG-07)

---

## 1. Documento de esta sección

| Documento | Propósito |
| --- | --- |
| [`Mini-Plan.md`](Mini-Plan.md) | Plan único condensado: los cinco tramos, ítems comprometidos, alcance técnico, Definition of Done aplicada, riesgos, criterios de hecho, trazabilidad y bitácora |

## 2. Artefactos que esta sección no emite, y por qué

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Plan-Iteracion-Sprint-XX.md` | **Omitido** | `equipo_n = 1` (`PRODUCT-INTAKE` §2). La regla de la categoría sustituye los cuatro artefactos por el mini-plan |
| `Template-Sprint-Review.md` | **Omitido** | Ídem. El evento de cierre de este producto es el **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15) |
| `Template-Sprint-Retrospectiva.md` | **Omitido** | Ídem. Con una sola persona no hay retrospectiva de equipo que facilitar |
| `Velocidad-Equipo.md` | **Omitido** | Ídem, y no hay iteraciones cerradas ni plazo calendario del que derivar una velocidad (`Roadmap-Producto.md` §1.1) |

## 3. Estado del plan

| Aspecto | Valor al 2026-08-10 |
| --- | --- |
| Etapas comprometidas del producto | 8 (`a` a `h`) |
| Etapas que toca este proyecto de código | 5: `a`, `c`, `d`, `e` y `f` |
| Etapas cerradas | 0 |
| Etapa abierta | Ninguna: el producto está en fase de especificación |
| Historias comprometidas | 25 de 25 |
| Tareas técnicas comprometidas | 26 |
| Casos de la batería obligatoria del validador | 0 de **10**, con los ocho escenarios `E-1` a `E-8` como entrada |
| Puertas técnicas que lo alcanzan | **`PT-04`**, en su parte de transformaciones aplicadas sobre base vacía, medida en la etapa `a` |

**Las etapas `b`, `g` y `h` no producen trabajo acá**, y el motivo está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2, incluida la `h`, cuyo aporte ya está construido en la `e`.

## 4. Dónde vive lo que este plan no decide

| Contenido | Dónde vive |
| --- | --- |
| Qué se construye y en qué prioridad | [`../06-Backlog-Tecnico/`](../06-Backlog-Tecnico/) |
| El orden de las etapas, sus criterios de transición y dónde se miden las puertas | [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) |
| Las dieciséis reglas y los nueve invariantes | `GeometriaFactory-Domain`, categorías 02 y 05 |
| El identificador del cuarto puerto | El punto de control de la etapa `a`, **sobre la superficie de `GeometriaFactory-Application`**, que es quien lo declara. Este plan aporta el criterio de nombrado del **adaptador**, con BT-06002 |
| El límite de tamaño del texto que se acepta | La categoría 05 de `GeometriaFactory-Api`. **Ya está reasignado y no se convierte en trabajo acá** |
| La composición de raíz y la conexión de los adaptadores | `GeometriaFactory-Api`. Este proyecto de código **declara sus adaptadores y no los registra** |
| La Definition of Done canónica y el guion de medición | `08-Calidad-Y-Pruebas`, **todavía no emitida** |
| Las tres puertas de cobertura y el pipeline | `09-Devops`, **todavía no emitida**. Hasta que BT-06023 cierre, **no se declaran bloqueantes** |

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Declara el único artefacto emitido, los **cuatro** que se omiten con el motivo de cada uno, el estado del plan con sus **cinco** tramos y la puerta `PT-04` que lo alcanza, y dónde vive lo que este plan no decide, incluidas las tres decisiones cuya titularidad es de otro proyecto de código y las dos categorías todavía no emitidas. |
