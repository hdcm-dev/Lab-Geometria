# 06 · Backlog técnico — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: ocho épicas, veintidós historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cinco épicas técnicas, dieciocho tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Siete criterios de entrada para las historias y cinco para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Veintidós archivos, uno por historia, por superar el umbral de veinte |

**No hay `tareas-tecnicas/`**: las dieciocho tareas están por debajo del umbral de treinta y viven inline en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §3 con la misma estructura obligatoria.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.2, para entender qué es una historia en un proyecto de código sin comportamiento.
2. [`Product-Backlog.md`](Product-Backlog.md) §2, para ver qué épica corresponde a qué etapa.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para entender por qué las tareas de acá son de forma y de inspección.
4. La historia concreta bajo [`historias-usuario/`](historias-usuario/).
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-01 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-01 a BT-05 |
| EP-02 Identidad del administrador y sesión | `c` | US-01, US-14, US-16 | BT-06 a BT-09 |
| EP-03 Ciclo de vida de la cuenta de alumno | `d` | US-02, US-03, US-04, US-05, US-21, US-22 | BT-10, BT-11 |
| EP-04 Gestión del trabajo | `e` | US-06, US-07, US-08, US-09, US-19 | BT-12, BT-13 |
| EP-05 Interpretación y verificación del dato del alumno | `f` | US-11, US-13, US-15 | BT-14 |
| EP-06 Visualización del trabajo | `g` | US-12 | BT-14 |
| EP-07 Desenlace de la entrega | `h` | US-17, US-18, US-20 | BT-15 |
| EP-08 Capacidades de prioridad menor | `i…` | US-10 | — |

## 4. Historias `Must Have` del tramo comprometido

**Veintiuna de las veintidós.** La restante, US-10, es `Could` y cae en la fase `i…`, **fuera** del tramo comprometido de ocho etapas. El fundamento del reparto está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

## 5. Tareas técnicas prioritarias

**BT-02**, la puerta de cero referencias hacia `GeometriaFactory-Domain`, porque el intake la declara como la vía por la que el acoplamiento vuelve. **BT-08**, la inspección de superficie pública, porque es el único mecanismo con el que la regla de exposición deja de ser una declaración. Y **BT-06** con **BT-07**, el tipo de error único y su conjunto cerrado de quince códigos, porque los otros siete contratos de uso comparten sus caminos de error.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-3 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida desde la Fase E**: se comprobó abriendo la carpeta y [`Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) existe. Se corrige la frase y se enlaza el artefacto, para que un lector que llegue por 06 no siga creyendo que la DoD no existe. **No era regresión de la Fase G**: el residuo es anterior. Ninguna historia, ítem de backlog ni recuento de esta sección cambia. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los cuatro artefactos con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura y resume las ocho épicas con su etapa, el reparto de prioridad y las tareas técnicas prioritarias con el fundamento de cada una. |
