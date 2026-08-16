# 06 · Backlog técnico — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
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
| EP-02001 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-02001 a BT-02005 |
| EP-02002 Identidad del administrador y sesión | `c` | US-02007, US-02008, US-02024, US-02025 | BT-02006, BT-02007, BT-02010, BT-02011 |
| EP-02003 Ciclo de vida de la cuenta de alumno | `d` | US-02001 a US-02006, US-02026, US-02027 | BT-02009, BT-02010, BT-02011, BT-02016 |
| EP-02004 Gestión del trabajo | `e` | US-02009, US-02010, US-02018, US-02019, US-02022 | BT-02006, BT-02012 |
| EP-02005 Interpretación y verificación del dato del alumno | `f` | US-02011 a US-02017 | BT-02008, BT-02012, BT-02013 |
| EP-02006 Desenlace de la entrega | `h` | US-02020, US-02021, US-02023 | BT-02012, BT-02014 |

## 4. Historias `Must Have` del tramo comprometido

**Veintiséis de las veintisiete**, con la única `Should` en US-02012. El tramo comprometido son las **ocho** etapas `a` a `h` del intake §15, y este proyecto de código toca **seis** de ellas. El fundamento del reparto está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

## 5. Tareas técnicas prioritarias

Las cinco de la etapa `a` —BT-02001 a BT-02005— porque nada de este proyecto de código empieza sin ellas, y las dos que cierran un punto abierto de esa misma etapa: **BT-02002**, los nombres de tipos y de espacios de nombres, y **BT-02003**, la herramienta que calcula la versión. Las dos tienen caja temporal en la etapa `a` y no se arrastran.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-3 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida desde la Fase E**: se comprobó abriendo la carpeta y [`Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md) existe. Se corrige la frase y se enlaza el artefacto, para que un lector que llegue por 06 no siga creyendo que la DoD no existe. **No era regresión de la Fase G**: el residuo es anterior. Ninguna historia, ítem de backlog ni recuento de esta sección cambia. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los cuatro artefactos con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura y resume las seis épicas con su etapa, el reparto de prioridad y las tareas técnicas prioritarias. |
