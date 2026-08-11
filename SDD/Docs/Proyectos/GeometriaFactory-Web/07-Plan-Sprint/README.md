# 07 · Plan de sprint — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-07)

---

## 1. Documento de esta sección

| Documento | Propósito |
| --- | --- |
| [`Mini-Plan.md`](Mini-Plan.md) | Plan único condensado: las ocho etapas más el momento de medición de `PT-02`, ítems comprometidos, alcance técnico, Definition of Done aplicada, riesgos, criterios de hecho, trazabilidad y bitácora |

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
| Etapas que toca este proyecto de código | **Las ocho**, más el momento de medición de `PT-02` y `PT-03` que precede a la `g` |
| Etapas cerradas | 0 |
| Etapa abierta | Ninguna: el producto está en fase de especificación |
| Historias comprometidas | 30 de 30 |
| Tareas técnicas comprometidas | 23 |
| Superficies que el plan pone en pie | 11 de 11 |
| Puertas técnicas propias | **`PT-01`** en sus cuatro partes, en la etapa `a` **antes que cualquier otra cosa**; y la parte de **`PT-02`** medida sobre una página de esta pieza, antes de comprometer la `g` |

**El momento previo a la etapa `g` no es una etapa nueva**: es el que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.2 declara para las dos puertas del visor. El fundamento está en [`Mini-Plan.md`](Mini-Plan.md) §1.3.

## 4. Dónde vive lo que este plan no decide

| Contenido | Dónde vive |
| --- | --- |
| Qué se construye y en qué prioridad | [`../06-Backlog-Tecnico/`](../06-Backlog-Tecnico/) |
| El diseño de las once superficies, sus estados y su línea de base visual | [`../03-UX-UI-DX/`](../03-UX-UI-DX/), **emitida y validada contra una maqueta aprobada** |
| El orden de las etapas, sus criterios de transición y dónde se miden las puertas | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) |
| Las dieciséis reglas de negocio | `GeometriaFactory-Domain`. **Esta pieza no hace cumplir ninguna** |
| El formato de intercambio | La categoría 05 de `GeometriaFactory-Api`. Esta pieza **lo adopta**, con BT-12 |
| El umbral numérico de tiempo de respuesta | Abierto: `PA-06` del backlog, elevado con BT-21. **La categoría 05 se negó a inventarlo** |
| Si el bundle generado se versiona o se ignora | `09-Devops`, **todavía no emitida**. Este plan lo acompaña con BT-23 |
| La Definition of Done canónica | `08-Calidad-Y-Pruebas`, **todavía no emitida**. Lo que sí está emitido de esa categoría es [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que este plan consume |

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Declara el único artefacto emitido, los **cuatro** que se omiten con el motivo de cada uno, el estado del plan con la constancia de que este proyecto de código toca **las ocho** etapas y de que el momento previo a la `g` no es una etapa nueva, y dónde vive lo que este plan no decide, incluidas las tres decisiones cuya titularidad es de otro lado y las dos categorías todavía no emitidas. |
