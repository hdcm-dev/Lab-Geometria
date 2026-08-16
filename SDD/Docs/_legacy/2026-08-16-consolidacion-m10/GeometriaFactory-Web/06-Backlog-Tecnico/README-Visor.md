# 06 · Backlog técnico — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: tres épicas, catorce historias inline con sus criterios, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cuatro épicas técnicas, dieciocho tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Siete criterios de entrada para las historias y cinco para las tareas técnicas |

**No hay `historias-usuario/` ni `tareas-tecnicas/`**, y es **elección declarada, no un umbral aplicado**. Las **dieciocho** tareas técnicas están por debajo del umbral de treinta y también por debajo de la banda que la regla recomienda para archivo propio, de modo que ahí no hay nada que elegir. **Las catorce historias sí caen en la banda de diez a veinte, que `Rules-Backlog-Tecnico.md` §2.1 clasifica como recomendada para archivo propio**, y este proyecto de código elige el modo inline: el fundamento está en [`Product-Backlog.md`](Product-Backlog.md) §3, «Por qué inline y no un archivo por historia», con sus tres motivos y con la condición para revisar la elección. En los dos modos la regla exige lo mismo y acá se cumple: criterios de aceptación, trazabilidad y verificación de entrada en las historias; justificación upstream, dependencias y criterios en las tareas.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.2, para entender qué es una historia en un visualizador puro y por qué algunas entregan una ausencia verificable.
2. [`Product-Backlog.md`](Product-Backlog.md) §2.1, para entender por qué el grueso del trabajo va **antes** de que se abra la etapa `g`.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para el orden entre las tres capas.
4. La historia concreta en [`Product-Backlog.md`](Product-Backlog.md) §3.
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Momento del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-12001 Esqueleto ambulante y verificación de viabilidad | Etapa `a` | Ninguna | BT-12001, BT-12002, BT-12003 |
| EP-12002 Medición de las puertas técnicas del visor | Antes de comprometer la etapa `g` | US-12001, US-12004, US-12009, US-12011 | BT-12004 a BT-12010, BT-12012, BT-12013, BT-12014, BT-12016 |
| EP-12003 Visualización del trabajo | Etapa `g` | US-12002, US-12003, US-12005, US-12006, US-12007, US-12008, US-12010, US-12012, US-12013, US-12014 | BT-12006, BT-12007, BT-12011, BT-12015, BT-12017, BT-12018 |

**EP-12002 no crea una etapa nueva ni renombra ninguna**: se apoya en el momento de medición que [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §2.2 ya declara para `PT-02` y `PT-03`. El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §2.1.

## 4. Historias `Must Have` del tramo comprometido

**Las catorce.** Desde el 2026-08-10 este backlog no tiene ninguna historia no-`Must`: las dos que eran `Should` —US-12008 y US-12009— derivan de `F-13`, y el Product Owner **promovió esa capacidad a `Must Have`** en `PRODUCT-INTAKE` **1.19**, cerrando la tensión que este backlog había elevado como `PA-06` y que **no había resuelto reprioritizando**. `GeometriaFactory-Web` había elevado la misma tensión desde el otro lado de la fachada. El 100 % `Must` resultante queda declarado como apartamiento consciente en [`Product-Backlog.md`](Product-Backlog.md) §4.2, con su motivo.

**Las catorce están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-12013** y **BT-12014**, las dos puertas técnicas, porque una puerta que no pasa detiene la planificación de la etapa `g` y no se arrastra como deuda. **BT-12016**, la inspección de la superficie del bundle generado, porque es donde se verifica que el visualizador siga siendo puro **sobre el artefacto que se sirve** y no sobre el código fuente. Y **BT-12009**, el anclaje de la versión del motor de dibujo, porque `05` §9 le asigna probabilidad **alta** al retrabajo que su cambio de interfaz puede exigir.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de `N-1` del informe `G-10-Examples-Siete-Proyectos-r2.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida y auditada desde la Fase E**: el residuo quedó vivo cuando la corrección de la ronda 1 arregló sólo los tres proyectos que aquel informe nombraba, de los **siete** que lo tenían. Ninguna decisión, recuento ni artefacto cambia. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos con su propósito, declara la ausencia de las dos carpetas de archivos individuales con su motivo, fija el orden de lectura, resume las tres épicas con su momento del producto y la constancia de que EP-12002 no crea una etapa, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`** (`PRODUCT-INTAKE` **1.19** §4) y **cierra el hallazgo `D-06-04`** del informe [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **§1**: la nota de ausencia de las dos carpetas deja de presentarse como umbral aplicado y pasa a distinguir los dos casos —las dieciocho tareas están por debajo de toda banda, las catorce historias caen en la banda que la regla **recomienda** para archivo propio— y remite al fundamento de la elección, que se escribe en `Product-Backlog.md` §3. **§4**: las historias `Must Have` pasan de doce a **catorce**, con el desenlace de `PA-06` y con la remisión al apartamiento del 100 % `Must` declarado en `Product-Backlog.md` §4.2. Ninguna épica, tarea técnica ni Definition of Ready cambia. Sube minor. |
