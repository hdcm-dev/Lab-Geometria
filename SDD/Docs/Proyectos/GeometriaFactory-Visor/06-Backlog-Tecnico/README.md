# 06 · Backlog técnico — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: tres épicas, catorce historias inline con sus criterios, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cuatro épicas técnicas, dieciocho tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Siete criterios de entrada para las historias y cinco para las tareas técnicas |

**No hay `historias-usuario/` ni `tareas-tecnicas/`**, y es decisión declarada: las **catorce** historias están por debajo del umbral de veinte y las **dieciocho** tareas por debajo del de treinta, de modo que viven inline con la misma estructura obligatoria —criterios de aceptación, trazabilidad y verificación de entrada en las historias; justificación upstream, dependencias y criterios en las tareas—.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.2, para entender qué es una historia en un visualizador puro y por qué algunas entregan una ausencia verificable.
2. [`Product-Backlog.md`](Product-Backlog.md) §2.1, para entender por qué el grueso del trabajo va **antes** de que se abra la etapa `g`.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para el orden entre las tres capas.
4. La historia concreta en [`Product-Backlog.md`](Product-Backlog.md) §3.
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Momento del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-01 Esqueleto ambulante y verificación de viabilidad | Etapa `a` | Ninguna | BT-01, BT-02, BT-03 |
| EP-02 Medición de las puertas técnicas del visor | Antes de comprometer la etapa `g` | US-01, US-04, US-09, US-11 | BT-04 a BT-10, BT-12, BT-13, BT-14, BT-16 |
| EP-03 Visualización del trabajo | Etapa `g` | US-02, US-03, US-05, US-06, US-07, US-08, US-10, US-12, US-13, US-14 | BT-06, BT-07, BT-11, BT-15, BT-17, BT-18 |

**EP-02 no crea una etapa nueva ni renombra ninguna**: se apoya en el momento de medición que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.2 ya declara para `PT-02` y `PT-03`. El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §2.1.

## 4. Historias `Must Have` del tramo comprometido

**Doce de las catorce.** Las dos `Should` —US-08 y US-09— derivan de `F-13`, la única capacidad `Should Have` que toca a este proyecto de código, y **las dos están dentro de lo que la puerta `PT-02` mide**: la tensión está elevada como `PA-06` en [`Product-Backlog.md`](Product-Backlog.md) §6 y **no se resolvió reprioritizando**.

**Las catorce están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-13** y **BT-14**, las dos puertas técnicas, porque una puerta que no pasa detiene la planificación de la etapa `g` y no se arrastra como deuda. **BT-16**, la inspección de la superficie del bundle generado, porque es donde se verifica que el visualizador siga siendo puro **sobre el artefacto que se sirve** y no sobre el código fuente. Y **BT-09**, el anclaje de la versión del motor de dibujo, porque `05` §9 le asigna probabilidad **alta** al retrabajo que su cambio de interfaz puede exigir.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos con su propósito, declara la ausencia de las dos carpetas de archivos individuales con su motivo, fija el orden de lectura, resume las tres épicas con su momento del producto y la constancia de que EP-02 no crea una etapa, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
