# 06 · Backlog técnico — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: cinco épicas, veinticinco historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Seis épicas técnicas, veintiséis tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Ocho criterios de entrada para las historias y seis para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Las **veinticinco** historias, una por archivo |

**No hay `tareas-tecnicas/`**, y es decisión declarada: las **veintiséis** tareas están por debajo del umbral de treinta. **Sí hay `historias-usuario/`**, porque las veinticinco superan el umbral de veinte.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.2, para entender qué es una historia en la capa que toca el mundo y por qué varias entregan una terminación y no un efecto.
2. [`Product-Backlog.md`](Product-Backlog.md) §2, para el reparto de las cinco épicas y para las tres etapas que no producen épica, incluida la `h` y su motivo.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para las tres particularidades del proyecto de código y el orden entre almacén, adaptadores, mecanismos y validador.
4. La historia concreta en [`historias-usuario/`](historias-usuario/).
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-06001 Esqueleto ambulante y verificación de viabilidad | `a` | US-06024, US-06025 | BT-06001 a BT-06008 |
| EP-06002 Identidad del administrador y sesión | `c` | US-06014, US-06015, US-06017, US-06018, US-06021, US-06022, US-06023 | BT-06005, BT-06009, BT-06012, BT-06013, BT-06015, BT-06021 |
| EP-06003 Ciclo de vida de la cuenta de alumno | `d` | US-06013, US-06016, US-06019, US-06020 | BT-06009, BT-06011, BT-06014, BT-06025 |
| EP-06004 Gestión del trabajo | `e` | US-06008, US-06009, US-06010, US-06011, US-06012 | BT-06005, BT-06010, BT-06011 |
| EP-06005 Interpretación y verificación del dato del alumno | `f` | US-06001 a US-06007 | BT-06016 a BT-06020, BT-06024 |

**EP-06001 es la única épica de etapa `a` del producto que tiene historias**, y el motivo está en [`Product-Backlog.md`](Product-Backlog.md) §3.1: `PT-04` se mide en esa etapa y exige que las actualizaciones de esquema se apliquen sobre base vacía. **Las etapas `b`, `g` y `h` no producen épica acá**, con el motivo declarado en §2 de ese mismo documento.

## 4. Historias `Must Have` del tramo comprometido

**Veinticuatro de las veinticinco.** La única `Should` es **US-06023** —proveer el sello por un puerto—, y lo es porque su caso de uso, `CU-06009`, es **el único de los diez que no traza a ninguna necesidad de negocio**: su origen es una decisión de testabilidad y no una capacidad. El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

**Las veinticinco están dentro del tramo comprometido de ocho etapas.**

## 5. Tareas técnicas prioritarias

**BT-06003**, el anclaje de la función de derivación de clave, porque el intake §17.3.P.1 declara **dos candidatas y no elige**, la decisión es de este proyecto de código y **no se puede delegar hacia arriba ni hacia abajo**. **BT-06016, BT-06017 y BT-06018**, el validador y su batería de diez casos, porque son la **mitigación declarada del único riesgo de negocio del producto**, al que la fuente asigna probabilidad alta e impacto alto. **BT-06014**, la producción de la provisoria, porque su atajo destructivo —componerla por otro medio cuando la fuente de aleatoriedad no responde— **deja el reseteo aparentemente funcionando** y no se nota hasta que alguien la usa. Y **BT-06006**, la preparación del almacén, porque el atajo de descartarlo y crearlo de nuevo **deja el servicio impecable y sin los trabajos de nadie**.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de `N-1` del informe `G-10-Examples-Siete-Proyectos-r2.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida y auditada desde la Fase E**: el residuo quedó vivo cuando la corrección de la ronda 1 arregló sólo los tres proyectos que aquel informe nombraba, de los **siete** que lo tenían. Ninguna decisión, recuento ni artefacto cambia. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura, resume las cinco épicas con su etapa del producto —incluida la única épica de etapa `a` del producto con historias— y nombra las tareas técnicas prioritarias con el fundamento de cada una, con los dos atajos destructivos que las hacen prioritarias. |
