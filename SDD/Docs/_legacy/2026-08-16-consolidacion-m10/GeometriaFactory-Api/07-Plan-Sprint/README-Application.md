# 07 · Plan de sprint — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Maintainer Lead (AG-07)

---

## 1. Documento de esta sección

| Documento | Propósito |
| --- | --- |
| [`Mini-Plan.md`](Mini-Plan.md) | Plan único condensado: los seis tramos, ítems comprometidos, alcance técnico, Definition of Done aplicada, riesgos, criterios de hecho, trazabilidad y bitácora |

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
| Etapas que toca este proyecto de código | 6: `a`, `c`, `d`, `e`, `f` y `h` |
| Etapas cerradas | 0 |
| Etapa abierta | Ninguna: el producto está en fase de especificación |
| Historias comprometidas | 32 de 32 |
| Tareas técnicas comprometidas | 21 |
| Puertas técnicas propias | **Ninguna de las cinco del producto se mide sobre este proyecto de código.** Lo que sí lo alcanza es la consecuencia: una puerta que no pasa detiene la planificación de las etapas que dependen de ella |

**Las etapas `b` y `g` no producen trabajo acá**, y el motivo está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/Product-Backlog.md) §2.

## 4. Dónde vive lo que este plan no decide

| Contenido | Dónde vive |
| --- | --- |
| Qué se construye y en qué prioridad | [`../06-Backlog-Tecnico/`](../06-Backlog-Tecnico/) |
| El orden de las etapas, sus criterios de transición y dónde se miden las puertas | [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) |
| Las dieciséis reglas y los nueve invariantes | `GeometriaFactory-Domain`, categorías 02 y 05 |
| El nombre del cuarto puerto | El **punto de control de la etapa `a`**, según [`../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md). Este plan lo compromete como BT-04002 y **no lo fija** |
| El criterio de comparación de dos correos | La categoría 05 de `GeometriaFactory-Infrastructure`. Este plan lo acompaña con BT-04021 |
| La Definition of Done canónica y el guion de medición | `08-Calidad-Y-Pruebas`, **todavía no emitida** |
| La puerta de cobertura y el pipeline | `09-Devops`, **todavía no emitida** |

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Declara el único artefacto emitido, los **cuatro** que se omiten con el motivo de cada uno, el estado del plan con sus **seis** tramos y la constancia de que ninguna de las cinco puertas técnicas del producto se mide sobre este proyecto de código, y dónde vive lo que este plan no decide, incluidas las dos decisiones cuya titularidad es de otro lado y las dos categorías todavía no emitidas. |
