# 06 · Backlog técnico — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
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
| EP-04001 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-04001 a BT-04006 |
| EP-04002 Identidad del administrador y sesión | `c` | US-04003, US-04007, US-04009, US-04028 | BT-04007, BT-04008, BT-04010, BT-04012, BT-04014 |
| EP-04003 Ciclo de vida de la cuenta de alumno | `d` | US-04001, US-04002, US-04004, US-04005, US-04006, US-04008, US-04029, US-04030, US-04031, US-04032 | BT-04010, BT-04011, BT-04012, BT-04013, BT-04014, BT-04021 |
| EP-04004 Gestión del trabajo | `e` | US-04010, US-04011, US-04012, US-04017, US-04019, US-04020, US-04021, US-04022, US-04026 | BT-04009, BT-04015, BT-04016 |
| EP-04005 Interpretación y verificación del dato del alumno | `f` | US-04013, US-04014, US-04015, US-04016 | BT-04015, BT-04019 |
| EP-04006 Desenlace de la entrega | `h` | US-04018, US-04023, US-04024, US-04025, US-04027 | BT-04015, BT-04017 |

**Las etapas `b` y `g` no producen épica en este proyecto de código**, y el motivo está en [`Product-Backlog.md`](Product-Backlog.md) §2: ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización.

## 4. Historias `Must Have` del tramo comprometido

**Treinta y una de las treinta y dos.** La única `Should` es **US-04016** —terminar de forma controlada cuando la interpretación no está disponible—, y lo es porque su origen no es una capacidad del intake sino una decisión de esta arquitectura (`05` §4). El fundamento completo está en [`Product-Backlog.md`](Product-Backlog.md) §4.2.

**Las treinta y dos están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-04002**, porque cierra el nombre del **cuarto puerto** —el que ninguna fuente declara— en el punto de control de la etapa `a`, y `05` §9 le asigna probabilidad **alta** al retrabajo si se fija sin ese punto de control. **BT-04006**, la puerta de cero pruebas que tocan la base de datos real, porque es lo que hace verificable la autorización por pertenencia sin base, que es exactamente lo que la fuente exige probar. **BT-04010** y **BT-04011**, la guarda con las cuatro comprobaciones en orden fijo y su matriz de ejercicio, porque un camino que ejerza una capacidad sin resolver antes la marca es el riesgo de impacto **muy alto** de `05` §9.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-3 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida desde la Fase E**: se comprobó abriendo la carpeta y [`Definition-Of-Done.md`](../../../08-Calidad-Y-Pruebas/Definition-Of-Done.md) existe. Se corrige la frase y se enlaza el artefacto, para que un lector que llegue por 06 no siga creyendo que la DoD no existe. **No era regresión de la Fase G**: el residuo es anterior. Ninguna historia, ítem de backlog ni recuento de esta sección cambia. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias con su propósito, declara la ausencia de `tareas-tecnicas/` con su motivo y la presencia de `historias-usuario/` con el suyo, fija el orden de lectura, resume las seis épicas con su etapa del producto y las dos etapas que no producen épica, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
