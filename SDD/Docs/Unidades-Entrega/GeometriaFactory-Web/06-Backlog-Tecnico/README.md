# 06 · Backlog técnico — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-16
**Autor:** Scrum Master (AG-06)

---


## 0. Esta categoría es de la unidad de entrega

**Los documentos de esta categoría se consolidaron el 2026-08-16**, absorbiendo los de `GeometriaFactory-Visor`. Cada uno lleva una subsección por proyecto de código, con su texto transpuesto sin reescritura.

**Los cuatro documentos tienen las mismas secciones en los dos proyectos de código.** Las `US` y los `BT` conviven sin colisionar: la renumeración le dio al visor el rango `12xxx`.

**La carpeta `_fusion/` se retira**: la fusión terminó acá. Lo absorbido está en
[`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/06-Backlog-Tecnico/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/06-Backlog-Tecnico/).

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
| EP-10001 Esqueleto ambulante y verificación de viabilidad | `a` | Ninguna | BT-10001 a BT-10006 |
| EP-10002 Navegación y sistema visual | `b` | Ninguna | BT-10007, BT-10008, BT-10009, BT-10010 |
| EP-10003 Identidad del administrador y sesión | `c` | US-10003, US-10004, US-10005, US-10006, US-10008, US-10026, US-10027 | BT-10011 a BT-10015 |
| EP-10004 Ciclo de vida de la cuenta de alumno | `d` | US-10001, US-10002, US-10007, US-10009, US-10010, US-10028, US-10029, US-10030 | BT-10007, BT-10013, BT-10014 |
| EP-10005 Gestión del trabajo | `e` | US-10011, US-10015, US-10016, US-10022, US-10023 | BT-10013 |
| EP-10006 Interpretación y verificación del dato del alumno | `f` | US-10012, US-10013, US-10014 | BT-10016 |
| EP-10007 Visualización del trabajo | `g` | US-10018, US-10019, US-10020, US-10021 | BT-10016, BT-10017, BT-10018, BT-10023 |
| EP-10008 Desenlace de la entrega | `h` | US-10017, US-10024, US-10025 | BT-10019, BT-10020 |

**Las ocho etapas comprometidas producen épica acá**, y es el único de los siete proyectos de código del que se puede decir. Las dos primeras no tienen historias porque son hitos internos sin capacidad funcional asociada.

## 4. Historias `Must Have` del tramo comprometido

**Las treinta.** Desde el 2026-08-10 este backlog no tiene ninguna historia no-`Must`: la que era `Should` es **US-10021** —sincronización del árbol y la escena por índice de pieza—, que deriva de `F-13`, y el Product Owner **promovió esa capacidad a `Must Have`** en `PRODUCT-INTAKE` **1.19**, cerrando la tensión que este backlog había elevado como `PA-02` y que **no había resuelto reprioritizando**. Es la misma tensión que `GeometriaFactory-Visor` elevó desde el otro lado de la fachada, y una sola decisión cerró las dos. El 100 % `Must` resultante queda declarado como apartamiento consciente en [`Product-Backlog.md`](Product-Backlog.md) §4.2, con su motivo.

**Las treinta están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna historia de la fase `i…`.

## 5. Tareas técnicas prioritarias

**BT-10004**, las cuatro mediciones de `PT-01`, porque se hacen en la etapa `a` **antes que cualquier otra cosa** y de su resultado depende el modelo entero de esta pieza: la salida preferente si `PT-01.b` o `PT-01.c` dan rojo está registrada y es un cambio de modelo, no un ajuste. **BT-10015**, la puerta de cero peticiones del navegador, porque `RA-01` es la regla que sostiene la topología entera y `05` §9 le asigna impacto **muy alto**. **BT-10014**, la custodia de la credencial en el estado del circuito, porque `05` §2.1 la llama **la decisión más consecuente del producto en términos de lo que la persona puede observar**. Y **BT-10012**, la adopción del formato de intercambio, porque un desajuste entre los dos extremos **no lo detecta la compilación**, que es la única red que este producto tiene.

## 6. Definition of Ready vigente

La de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) 1.0. La Definition of Done **no vive acá**: vive en [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md), **emitida desde la Fase E**.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de `N-1` del informe `G-10-Examples-Siete-Proyectos-r2.md` 1.0.** Esta sección declaraba que la Definition of Done «vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y **`08` está emitida y auditada desde la Fase E**: el residuo quedó vivo cuando la corrección de la ronda 1 arregló sólo los tres proyectos que aquel informe nombraba, de los **siete** que lo tenían. Ninguna decisión, recuento ni artefacto cambia. **Autor:** Orquestador SDD |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección. Enumera los tres artefactos y la carpeta de historias, declara la ausencia de `tareas-tecnicas/` con su motivo, fija el orden de lectura, resume las ocho épicas con su etapa del producto y la constancia de que este es el único proyecto de código que toca las ocho, y nombra las tareas técnicas prioritarias con el fundamento de cada una. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4. Las historias `Must Have` pasan de veintinueve a **treinta**, con el desenlace de `PA-02` —cerrado, con su fila conservada en `Product-Backlog.md` §6— y con la remisión al apartamiento del 100 % `Must` declarado en `Product-Backlog.md` §4.2. Ninguna épica, tarea técnica ni Definition of Ready cambia. Sube minor. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a indexar la categoría de la **unidad de entrega**. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
