# 06 · Backlog técnico — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: seis épicas, veintisiete historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cinco épicas técnicas, dieciséis tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Seis criterios de entrada para las historias y cinco para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Veintisiete archivos, uno por historia, por superar el umbral de veinte |

**No hay `tareas-tecnicas/`**, y es decisión declarada: las dieciséis tareas están por debajo del umbral de treinta que la regla de la categoría fija para exigir archivo individual, y viven inline en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §3 con la misma estructura obligatoria.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1 y §2, para entender qué épica corresponde a qué etapa del producto.
2. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2, para ver qué se construye antes de qué.
3. La historia concreta bajo [`historias-usuario/`](historias-usuario/).
4. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-01 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-01 a BT-05 |
| EP-02 Identidad del administrador y sesión | `c` | US-07, US-08, US-24, US-25 | BT-06, BT-07, BT-10, BT-11 |
| EP-03 Ciclo de vida de la cuenta de alumno | `d` | US-01 a US-06, US-26, US-27 | BT-09, BT-10, BT-11, BT-16 |
| EP-04 Gestión del trabajo | `e` | US-09, US-10, US-18, US-19, US-22 | BT-06, BT-12 |
| EP-05 Interpretación y verificación del dato del alumno | `f` | US-11 a US-17 | BT-08, BT-12, BT-13 |
| EP-06 Desenlace de la entrega | `h` | US-20, US-21, US-23 | BT-12, BT-14 |

## 4. Historias `Must Have` del tramo comprometido

**Veintiséis de las veintisiete**, con la única `Should` en US-12. El tramo comprometido son las **ocho** etapas `a` a `h` del intake §15, y este proyecto de código toca **seis** de ellas. El fundamento del reparto está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

## 5. Tareas técnicas prioritarias

Las cinco de la etapa `a` —BT-01 a BT-05— porque nada de este proyecto de código empieza sin ellas, y las dos que cierran un punto abierto de esa misma etapa: **BT-02**, los nombres de tipos y de espacios de nombres, y **BT-03**, la herramienta que calcula la versión. Las dos tienen caja temporal en la etapa `a` y no se arrastran.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los cuatro artefactos con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura y resume las seis épicas con su etapa, el reparto de prioridad y las tareas técnicas prioritarias. |
