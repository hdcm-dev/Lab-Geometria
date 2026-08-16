# 06 · Backlog técnico — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
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
| EP-08001 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-08001 a BT-08005 |
| EP-08002 Identidad del administrador y sesión | `c` | US-08001, US-08014, US-08016 | BT-08006 a BT-08009 |
| EP-08003 Ciclo de vida de la cuenta de alumno | `d` | US-08002, US-08003, US-08004, US-08005, US-08021, US-08022 | BT-08010, BT-08011 |
| EP-08004 Gestión del trabajo | `e` | US-08006, US-08007, US-08008, US-08009, US-08019 | BT-08012, BT-08013 |
| EP-08005 Interpretación y verificación del dato del alumno | `f` | US-08011, US-08013, US-08015 | BT-08014 |
| EP-08006 Visualización del trabajo | `g` | US-08012 | BT-08014 |
| EP-08007 Desenlace de la entrega | `h` | US-08017, US-08018, US-08020 | BT-08015 |
| EP-08008 Capacidades de prioridad menor | `i…` | US-08010 | — |

## 4. Historias `Must Have` del tramo comprometido

**Veintiuna de las veintidós.** La restante, US-08010, es `Could` y cae en la fase `i…`, **fuera** del tramo comprometido de ocho etapas. El fundamento del reparto está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

## 5. Tareas técnicas prioritarias

**BT-08002**, la puerta de cero referencias hacia `GeometriaFactory-Domain`, porque el intake la declara como la vía por la que el acoplamiento vuelve. **BT-08008**, la inspección de superficie pública, porque es el único mecanismo con el que la regla de exposición deja de ser una declaración. Y **BT-08006** con **BT-08007**, el tipo de error único y su conjunto cerrado de diecisiete códigos, porque los otros siete contratos de uso comparten sus caminos de error.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-3 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida desde la Fase E**: se comprobó abriendo la carpeta y [`Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) existe. Se corrige la frase y se enlaza el artefacto, para que un lector que llegue por 06 no siga creyendo que la DoD no existe. **No era regresión de la Fase G**: el residuo es anterior. Ninguna historia, ítem de backlog ni recuento de esta sección cambia. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los cuatro artefactos con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura y resume las ocho épicas con su etapa, el reparto de prioridad y las tareas técnicas prioritarias con el fundamento de cada una. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
