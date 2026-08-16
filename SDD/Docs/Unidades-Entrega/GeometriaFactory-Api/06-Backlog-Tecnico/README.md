# 06 · Backlog técnico — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-16
**Autor:** Scrum Master + API Product Owner (AG-06)

---


## 0. Esta categoría es de la unidad de entrega

**Los tres documentos de esta categoría se consolidaron el 2026-08-16.** Las `US` y los `BT` de las
cuatro capas conviven **sin una sola colisión**, porque la renumeración de la migración les había dado
rango propio: es una unión de catálogo directa.

**El orden de ejecución no lo fija este backlog**: lo fija el grafo de compilación del manifiesto
—primero el dominio, después la aplicación y la infraestructura, y al final el host—.

**La carpeta `_fusion/` de esta categoría se retira**: la fusión terminó acá. Los documentos absorbidos
están en [`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/06-Backlog-Tecnico/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/06-Backlog-Tecnico/).

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: seis épicas, treinta historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cinco épicas técnicas, veintiséis tareas técnicas inline y la matriz BT ↔ US ↔ CU con la columna de puntos de acceso |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Ocho criterios de entrada para las historias y seis para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Las **treinta** historias, una por archivo |

**No hay `tareas-tecnicas/`**, y es decisión declarada: las **veintiséis** tareas están por debajo del umbral de treinta. **Sí hay `historias-usuario/`**, porque las treinta superan el umbral de veinte.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.1 y §1.2, para entender qué significa ser el proyecto de código principal y por qué **dos reglas de negocio se rompen desde acá sin que ninguna capa de adentro se entere**.
2. [`Product-Backlog.md`](Product-Backlog.md) §2, para el reparto de las seis épicas y para las dos etapas que no producen épica.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para las tres particularidades del proyecto de código y el orden entre composición de raíz, superficie, guardia y traducción.
4. La historia concreta en [`historias-usuario/`](historias-usuario/), y su punto de acceso en [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3.
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-00001 Esqueleto ambulante y verificación de viabilidad | `a` | US-00026, US-00027, US-00028, US-00029 | BT-00001 a BT-00006 |
| EP-00002 Identidad del administrador y sesión | `c` | US-00001 a US-00005, US-00008, US-00010, US-00024, US-00025 | BT-00007 a BT-00016 |
| EP-00003 Ciclo de vida de la cuenta de alumno | `d` | US-00006, US-00007, US-00009, US-00011 a US-00016 | BT-00011, BT-00012, BT-00017 |
| EP-00004 Gestión del trabajo | `e` | US-00019, US-00020, US-00021, US-00022 | BT-00018, BT-00023, BT-00024 |
| EP-00005 Interpretación y verificación del dato del alumno | `f` | US-00017, US-00018 | BT-00018, BT-00022 |
| EP-00006 Desenlace de la entrega | `h` | US-00023, US-00030 | BT-00019, BT-00020, BT-00021 |

**Las etapas `b` y `g` no producen épica acá**, con el motivo declarado en [`Product-Backlog.md`](Product-Backlog.md) §2: la `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

## 4. Historias `Must Have` del tramo comprometido

**Veintinueve de las treinta.** La única `Should` es **US-00030** —la colección de peticiones reproducible—, y lo es porque **es la única historia que no implementa nada sino que demuestra**: su origen no es una capacidad del intake §4 sino la estrategia de demostración de §16.1 y §18, y su caso de uso **no traza a ninguna necesidad de negocio**. El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

**Las treinta están dentro del tramo comprometido de ocho etapas.**

## 5. Tareas técnicas prioritarias

**BT-00012**, la inspección de los quince puntos contra la guardia **en las dos direcciones**, porque un punto nuevo fuera de la guardia rompe `RN-00013` **sin que nada falle** y `05` §9 le asigna probabilidad **alta**: los defectos de omisión no se ven leyendo el punto nuevo. **BT-00014**, la prueba de las tres familias deliberadamente empobrecidas, porque la primera de las tres es la que rompe `RN-00003` hacia afuera y **ninguna capa de adentro puede repararlo**. **BT-00008**, el formato de intercambio, porque un desajuste entre los dos extremos **aparece en tiempo de ejecución y no lo detecta la compilación**, que es la única red que este producto tiene. Y **BT-00024**, la prueba del texto byte a byte, porque truncar el cuerpo **rompe `RN-00008` en silencio** y el alumno lo descubre al ver el dibujo.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de `N-1` del informe `G-10-Examples-Siete-Proyectos-r2.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida y auditada desde la Fase E**: el residuo quedó vivo cuando la corrección de la ronda 1 arregló sólo los tres proyectos que aquel informe nombraba, de los **siete** que lo tenían. Ninguna decisión, recuento ni artefacto cambia. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura, resume las seis épicas con su etapa del producto y las dos que no producen épica, y nombra las tareas técnicas prioritarias con el fundamento de cada una, incluidas las dos inspecciones que detectan defectos de omisión. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa de indexar la categoría de un proyecto de código a indexar la de la **unidad de entrega**, con sus documentos consolidados en 2.0. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
