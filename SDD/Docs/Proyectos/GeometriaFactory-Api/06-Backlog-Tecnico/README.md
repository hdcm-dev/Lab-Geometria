# 06 · Backlog técnico — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)

---

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
| EP-01 Esqueleto ambulante y verificación de viabilidad | `a` | US-26, US-27, US-28, US-29 | BT-01 a BT-06 |
| EP-02 Identidad del administrador y sesión | `c` | US-01 a US-05, US-08, US-10, US-24, US-25 | BT-07 a BT-16 |
| EP-03 Ciclo de vida de la cuenta de alumno | `d` | US-06, US-07, US-09, US-11 a US-16 | BT-11, BT-12, BT-17 |
| EP-04 Gestión del trabajo | `e` | US-19, US-20, US-21, US-22 | BT-18, BT-23, BT-24 |
| EP-05 Interpretación y verificación del dato del alumno | `f` | US-17, US-18 | BT-18, BT-22 |
| EP-06 Desenlace de la entrega | `h` | US-23, US-30 | BT-19, BT-20, BT-21 |

**Las etapas `b` y `g` no producen épica acá**, con el motivo declarado en [`Product-Backlog.md`](Product-Backlog.md) §2: la `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

## 4. Historias `Must Have` del tramo comprometido

**Veintinueve de las treinta.** La única `Should` es **US-30** —la colección de peticiones reproducible—, y lo es porque **es la única historia que no implementa nada sino que demuestra**: su origen no es una capacidad del intake §4 sino la estrategia de demostración de §16.1 y §18, y su caso de uso **no traza a ninguna necesidad de negocio**. El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

**Las treinta están dentro del tramo comprometido de ocho etapas.**

## 5. Tareas técnicas prioritarias

**BT-12**, la inspección de los quince puntos contra la guardia **en las dos direcciones**, porque un punto nuevo fuera de la guardia rompe `RN-13` **sin que nada falle** y `05` §9 le asigna probabilidad **alta**: los defectos de omisión no se ven leyendo el punto nuevo. **BT-14**, la prueba de las tres familias deliberadamente empobrecidas, porque la primera de las tres es la que rompe `RN-03` hacia afuera y **ninguna capa de adentro puede repararlo**. **BT-08**, el formato de intercambio, porque un desajuste entre los dos extremos **aparece en tiempo de ejecución y no lo detecta la compilación**, que es la única red que este producto tiene. Y **BT-24**, la prueba del texto byte a byte, porque truncar el cuerpo **rompe `RN-08` en silencio** y el alumno lo descubre al ver el dibujo.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura, resume las seis épicas con su etapa del producto y las dos que no producen épica, y nombra las tareas técnicas prioritarias con el fundamento de cada una, incluidas las dos inspecciones que detectan defectos de omisión. |
