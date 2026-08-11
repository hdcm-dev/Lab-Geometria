# 06 · Backlog técnico — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: seis épicas, treinta y dos historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cinco épicas técnicas, veintiuna tareas técnicas inline y la matriz BT ↔ US ↔ CU |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Siete criterios de entrada para las historias y cinco para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Las **treinta y dos** historias, una por archivo |

**No hay `tareas-tecnicas/`**, y es decisión declarada: las **veintiuna** tareas están por debajo del umbral de treinta, de modo que viven inline con su justificación upstream, sus dependencias y sus criterios. **Sí hay `historias-usuario/`**, porque las treinta y dos superan el umbral de veinte.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.2, para entender qué es una historia en una capa de casos de uso y por qué diez de ellas entregan una negativa con su motivo.
2. [`Product-Backlog.md`](Product-Backlog.md) §2, para el reparto de las seis épicas sobre las etapas del producto y las dos que no producen épica.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para el orden entre las fundaciones, la frontera, la guarda y los seis orquestadores.
4. La historia concreta en [`historias-usuario/`](historias-usuario/).
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-01 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-01 a BT-06 |
| EP-02 Identidad del administrador y sesión | `c` | US-03, US-07, US-09, US-28 | BT-07, BT-08, BT-10, BT-12, BT-14 |
| EP-03 Ciclo de vida de la cuenta de alumno | `d` | US-01, US-02, US-04, US-05, US-06, US-08, US-29, US-30, US-31, US-32 | BT-10, BT-11, BT-12, BT-13, BT-14, BT-21 |
| EP-04 Gestión del trabajo | `e` | US-10, US-11, US-12, US-17, US-19, US-20, US-21, US-22, US-26 | BT-09, BT-15, BT-16 |
| EP-05 Interpretación y verificación del dato del alumno | `f` | US-13, US-14, US-15, US-16 | BT-15, BT-19 |
| EP-06 Desenlace de la entrega | `h` | US-18, US-23, US-24, US-25, US-27 | BT-15, BT-17 |

**Las etapas `b` y `g` no producen épica en este proyecto de código**, y el motivo está en [`Product-Backlog.md`](Product-Backlog.md) §2: ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización.

## 4. Historias `Must Have` del tramo comprometido

**Treinta y una de las treinta y dos.** La única `Should` es **US-16** —terminar de forma controlada cuando la interpretación no está disponible—, y lo es porque su origen no es una capacidad del intake sino una decisión de esta arquitectura (`05` §4). El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

**Las treinta y dos están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-02**, porque cierra el nombre del **cuarto puerto** —el que ninguna fuente declara— en el punto de control de la etapa `a`, y `05` §9 le asigna probabilidad **alta** al retrabajo si se fija sin ese punto de control. **BT-06**, la puerta de cero pruebas que tocan la base de datos real, porque es lo que hace verificable la autorización por pertenencia sin base, que es exactamente lo que la fuente exige probar. **BT-10** y **BT-11**, la guarda con las cuatro comprobaciones en orden fijo y su matriz de ejercicio, porque un camino que ejerza una capacidad sin resolver antes la marca es el riesgo de impacto **muy alto** de `05` §9.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-3 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida desde la Fase E**: se comprobó abriendo la carpeta y [`Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) existe. Se corrige la frase y se enlaza el artefacto, para que un lector que llegue por 06 no siga creyendo que la DoD no existe. **No era regresión de la Fase G**: el residuo es anterior. Ninguna historia, ítem de backlog ni recuento de esta sección cambia. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo y la presencia de `historias-usuario/` con el suyo, fija el orden de lectura, resume las seis épicas con su etapa del producto y las dos etapas que no producen épica, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
