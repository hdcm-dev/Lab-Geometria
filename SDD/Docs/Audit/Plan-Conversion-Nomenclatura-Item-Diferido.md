# Plan de conversión — la nomenclatura del ítem diferido, y el remedio de `M-01`

**Producto:** Fábrica de Geometría
**Documento:** Plan-Conversion-Nomenclatura-Item-Diferido.md
**Versión:** 1.0
**Fecha:** 2026-08-20
**Origen:** Auditoría propia posterior a M6, contrastando las columnas emitidas contra `Root-Rules.md` **7.0** §12.2
**Alcance:** dos reparaciones sobre un destino ya migrado. **Ninguna reabre la migración**

---

## 1. Los dos defectos, y los dos son de la migración que los produjo

### 1.1 `N-01` · La columna no se llama como el campo que realiza

**`Root-Rules.md` §12.2 declara sus cuatro campos, y el cuarto dice literalmente:**

> *4. **En qué evento se cierra, nombrando un artefacto y su sección** — no un momento.*

**La fase M4 emitió la columna como «Dónde se cierra (artefacto y sección)».** Es la misma cosa y
**no es el mismo nombre**, y la diferencia no es cosmética: *«dónde»* nombra un **lugar** y el campo
de la regla nombra un **evento**. Esa distinción es exactamente la que §12.2 existe para sostener —un
momento no sirve, un evento anclado a un artefacto sí—, de modo que degradarla en el rótulo debilita
justo lo que la regla vino a fijar.

**Es un defecto propio y se declara como tal.** Lo escribió el mismo agente que redactó §12.2 en el
framework, tres días antes.

### 1.2 `M-01` · La exclusión del barrido, sin enumerar · **P2 abierto**

Declarado en `Informe-Migracion-9.12-a-10.0.md` §2. El barrido de la migración afirmó **«superficie
CERO»** para las citas a `Root-Rules.md` §12 y el patrón hoy devuelve **2** — las dos siendo la frase
que **nombra el propio barrido**. `SDD-Development-Guide.md` §VI.3.2 exige residuo cero **fuera de
exclusiones enumeradas con su motivo**, y la migración no enumeró ninguna.

---

## 2. Mapa de conversión

**La forma anterior se declara como patrón literal, no como descripción** (`§VI.3.2`).

| # | Forma anterior (patrón literal) | Forma vigente | Ocurrencias medidas |
|---|---|---|---|
| C1 | `Dónde se cierra (artefacto y sección)` | `En qué evento se cierra (artefacto y sección)` | **18** — una cabecera por **tabla**, y los seis documentos llevan **18 tablas** entre todos, no una cada uno |
| C2 | `Dónde se cierra (artefacto y sección)` *(dentro de las filas de control de cambios)* | `En qué evento se cierra (artefacto y sección)` | **6** — una por documento |


**Total a convertir: 24 ocurrencias en 6 documentos** — 18 cabeceras de tabla y 6 menciones en el control de cambios.

**El recuento de C1 se corrigió después de correr la conversión.** La emisión de este plan decía «6, una por tabla» dando por hecho **una tabla por documento**, y son **18**: los documentos de la API llevan cuatro cada uno —uno por proyecto de código— y los del Web, dos. Es un recuento declarado en prosa sin anclar, que es justo lo que `Root-Rules.md` §10 desaconseja; se corrige acá en lugar de dejarlo.

### 2.1 Lo que NO se convierte, enumerado con su motivo

| Qué | Motivo |
|---|---|
| `Audit/Estado-Del-Destino-2026-08-16.md` línea 207, «Dónde se cierra **la procedencia**» | **Es otro uso y es anterior**: nombra dónde se cierra la procedencia en la fase M5, no el campo de §12.2. Y vive en un **informe emitido con fecha**, que es registro histórico |
| **Este mismo plan**, §1.1, §2 y §5 | **Declara el patrón para poder convertirlo**, de modo que nombrarlo es su función. `§VI.3.2` prevé el caso: la regla 4 se corre sobre el texto propio, y una declaración que no pudiera nombrar lo que corrige sería inútil. **Es la tercera vez que este barrido se encuentra a sí mismo** —antes en la nota de coherencia del framework y en `M-01`—, de modo que la exclusión se enumera de entrada en lugar de descubrirse después |
| Todo `_legacy/` | Snapshots congelados |

## 3. Correspondencia de los cuatro campos con las cinco columnas

**Se declara en cada documento en lugar de partir la columna, y el motivo es que partirla obligaría a
reescribir la prosa de 116 filas** — es decir, a tocar el enunciado de puntos abiertos que la
migración se comprometió a no tocar, y que `Informe-Migracion-9.12-a-10.0.md` `A7` verificó
**idénticos**.

| Campo de §12.2 | Columna que lo realiza |
|---|---|
| 1 · Qué falta | **`Punto abierto`**, su enunciado en negrita de apertura |
| 2 · Por qué no se puede hoy | **`Punto abierto`**, el desarrollo que sigue al enunciado |
| 3 · Quién lo cierra | **`Quién lo cierra`** |
| 4 · En qué evento se cierra | **`En qué evento se cierra (artefacto y sección)`** |
| — | **`Estado`**, que **no es un campo de §12.2**: realiza su **tabla de escalamiento**, y se declara como derivado y no como quinto campo |

**Una constancia honesta sobre los campos 1 y 2.** Que la columna `Punto abierto` los lleve a los dos
se verificó **por muestreo y no exhaustivamente**: las filas revisadas abren con el qué en negrita y
siguen con el porqué. **No se comprobó fila por fila**, y si alguna sólo lleva el qué, la corrección
es de contenido y no de esta conversión.

## 4. Remedio de `M-01`

Se agrega, en **`Plan-Migracion-9.12-a-10.0.md` §4.1** y en **`PRODUCT-MANIFEST` §1.1**, la exclusión
que faltaba, con su motivo:

> **Exclusión enumerada:** las **2** ocurrencias vivas del patrón `Root-Rules.md §12` son **la frase
> que nombra este mismo barrido** en el plan y en la tabla de superficies. No son citas de la sección
> como fuente normativa. `§VI.3.2` prevé el caso al exigir que la regla 4 se corra **sobre el texto
> propio**.

## 5. Cómo se verifica que la conversión cuadra

- [x] [enumerable] **Cero ocurrencias vivas** de `Dónde se cierra (artefacto y sección)` fuera de las exclusiones de §2.1.
- [x] [enumerable] **18 cabeceras** con la forma vigente, una por tabla de puntos abiertos. **Verificado: 18.**
- [x] [enumerable] El recuento de filas **no cambia**. **Verificado**: 118 vivas (116 migradas + 2 fuera de alcance), **76** vencidas, **14** sin evento.
- [x] [enumerable] La columna `Punto abierto` queda **idéntica**. **Verificado por diff en los seis documentos** contra el commit de M6.
- [x] [enumerable] Cabecera igual a la mayor fila del control de cambios. **Verificado: 6 de 6, todos en 3.1.**
- [x] [enumerable] `M-01`: la exclusión aparece en los **dos** lugares y el informe de M6 lo registra **cerrado el 2026-08-20**. **Verificado.**
- [x] [interpretativo] Ningún enunciado de punto abierto cambia de sentido — se sostiene sobre el diff idéntico de la columna.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-20 | Emisión inicial. Declara **dos reparaciones sobre un destino ya migrado**, ninguna de las cuales reabre la migración: **`N-01`**, la columna emitida por M4 como «Dónde se cierra» cuando `Root-Rules.md` §12.2 campo 4 dice literalmente **«En qué evento se cierra»** —defecto propio, escrito por el mismo agente que redactó la regla tres días antes—, con su mapa de conversión de **24 ocurrencias en 6 documentos** y sus dos exclusiones enumeradas; y **`M-01`**, el hallazgo **P2** de `Informe-Migracion-9.12-a-10.0.md` §2, cuyo remedio es enumerar la exclusión del barrido en el plan y en `§1.1`. Declara además la **correspondencia de los cuatro campos de §12.2 con las cinco columnas**, con el motivo de no partir `Punto abierto` —hacerlo obligaría a reescribir la prosa de 116 filas que `A7` verificó idénticas— y con la constancia de que la cobertura de los campos 1 y 2 se verificó **por muestreo y no fila por fila**. | Orquestador SDD |
