# 06 · Backlog técnico — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Scrum Master (AG-06)

---

## 1. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Product-Backlog.md`](Product-Backlog.md) | Índice maestro priorizado: ocho épicas, treinta historias, métricas y refinamiento |
| [`Backlog-Tecnico.md`](Backlog-Tecnico.md) | Cinco épicas técnicas, veintitrés tareas técnicas inline y la matriz BT ↔ US ↔ CU con la columna de superficies |
| [`Definition-Of-Ready.md`](Definition-Of-Ready.md) | Ocho criterios de entrada para las historias y seis para las tareas técnicas |
| [`historias-usuario/`](historias-usuario/) | Las **treinta** historias, una por archivo |

**No hay `tareas-tecnicas/`**, y es decisión declarada: las **veintitrés** tareas están por debajo del umbral de treinta. **Sí hay `historias-usuario/`**, porque las treinta superan el umbral de veinte.

## 2. Orden de lectura

1. [`Product-Backlog.md`](Product-Backlog.md) §1.1 y §1.2, para entender qué significa ser la pieza pública y por qué **ninguna historia hace cumplir una regla de negocio**.
2. [`Product-Backlog.md`](Product-Backlog.md) §2, para el reparto de las ocho épicas sobre las ocho etapas del producto.
3. [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §1 y §2, para el orden entre las fundaciones, el armazón, la salida única y el anfitrión del visor.
4. La historia concreta en [`historias-usuario/`](historias-usuario/), y su superficie en [`../03-UX-UI-DX/`](../03-UX-UI-DX/).
5. [`Definition-Of-Ready.md`](Definition-Of-Ready.md), antes de comprometerla.

## 3. Épicas vigentes

| Épica | Etapa del producto | Historias | Tareas técnicas |
| --- | --- | --- | --- |
| EP-01 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-01 a BT-06 |
| EP-02 Navegación y sistema visual | `b` | Ninguna | BT-07, BT-08, BT-09, BT-10 |
| EP-03 Identidad del administrador y sesión | `c` | US-03, US-04, US-05, US-06, US-08, US-26, US-27 | BT-11 a BT-15 |
| EP-04 Ciclo de vida de la cuenta de alumno | `d` | US-01, US-02, US-07, US-09, US-10, US-28, US-29, US-30 | BT-07, BT-13, BT-14 |
| EP-05 Gestión del trabajo | `e` | US-11, US-15, US-16, US-22, US-23 | BT-13 |
| EP-06 Interpretación y verificación del dato del alumno | `f` | US-12, US-13, US-14 | BT-16 |
| EP-07 Visualización del trabajo | `g` | US-18, US-19, US-20, US-21 | BT-16, BT-17, BT-18, BT-23 |
| EP-08 Desenlace de la entrega | `h` | US-17, US-24, US-25 | BT-19, BT-20 |

**Las ocho etapas comprometidas producen épica acá**, y es el único de los siete proyectos de código del que se puede decir. Las dos primeras no tienen historias porque son hitos internos sin capacidad funcional asociada.

## 4. Historias `Must Have` del tramo comprometido

**Las treinta.** Desde el 2026-08-10 este backlog no tiene ninguna historia no-`Must`: la que era `Should` es **US-21** —sincronización del árbol y la escena por índice de pieza—, que deriva de `F-13`, y el Product Owner **promovió esa capacidad a `Must Have`** en `PRODUCT-INTAKE` **1.19**, cerrando la tensión que este backlog había elevado como `PA-02` y que **no había resuelto reprioritizando**. Es la misma tensión que `GeometriaFactory-Visor` elevó desde el otro lado de la fachada, y una sola decisión cerró las dos. El 100 % `Must` resultante queda declarado como apartamiento consciente en [`Product-Backlog.md`](Product-Backlog.md) §4.2, con su motivo.

**Las treinta están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-04**, las cuatro mediciones de `PT-01`, porque se hacen en la etapa `a` **antes que cualquier otra cosa** y de su resultado depende el modelo entero de esta pieza: la salida preferente si `PT-01.b` o `PT-01.c` dan rojo está registrada y es un cambio de modelo, no un ajuste. **BT-15**, la puerta de cero peticiones del navegador, porque `RA-01` es la regla que sostiene la topología entera y `05` §9 le asigna impacto **muy alto**. **BT-14**, la custodia de la credencial en el estado del circuito, porque `05` §2.1 la llama **la decisión más consecuente del producto en términos de lo que la persona puede observar**. Y **BT-12**, la adopción del formato de intercambio, porque un desajuste entre los dos extremos **no lo detecta la compilación**, que es la única red que este producto tiene.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de `N-1` del informe `G-10-Examples-Siete-Proyectos-r2.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida y auditada desde la Fase E**: el residuo quedó vivo cuando la corrección de la ronda 1 arregló sólo los tres proyectos que aquel informe nombraba, de los **siete** que lo tenían. Ninguna decisión, recuento ni artefacto cambia. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura, resume las ocho épicas con su etapa del producto y la constancia de que este es el único proyecto de código que toca las ocho, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4. Las historias `Must Have` pasan de veintinueve a **treinta**, con el desenlace de `PA-02` —cerrado, con su fila conservada en `Product-Backlog.md` §6— y con la remisión al apartamiento del 100 % `Must` declarado en `Product-Backlog.md` §4.2. Ninguna épica, tarea técnica ni Definition of Ready cambia. Sube minor. |
