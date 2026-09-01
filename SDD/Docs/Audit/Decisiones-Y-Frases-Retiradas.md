# Decisiones tomadas y frases que dejaron de ser ciertas

**Producto:** Fábrica de Geometría
**Documento:** Decisiones-Y-Frases-Retiradas.md
**Versión:** 1.0
**Fecha:** 2026-08-31
**Instrumento:** lo lee [`scripts/verify-propagacion.sh`](../../../scripts/verify-propagacion.sh), que **falla** si una frase vigilada revive
**Estado:** **Vigente.** Es un registro operativo, no un informe: se edita cada vez que se toma una decisión

---

## 0. Para qué existe, medido

**Este producto tiene verificaciones para todo menos para una pregunta: «¿quedó algo diciendo lo
contrario?».** Es el diagnóstico de la mesa del 2026-08-31 —*verifica sus condiciones y no verifica sus
efectos*— aplicado al corpus documental, y está medido **cinco veces en dos días**:

| Decisión | Llegó a | Quedó diciendo lo contrario |
|---|---|---|
| `D1`, confirmada el 2026-08-26 | 2 documentos | **35 filas** «Condicionado» en tres documentos de `08` |
| `D3`, reformulada el 2026-08-26 | 3 de 7 documentos | **7 apariciones** de «no está declarado por ninguna fuente» |
| `D4`, decidida el 2026-08-20 | 5 documentos | **15 apariciones** de «se ancla en la etapa `a`», cerrada el 2026-08-13 |
| `D8`, decidida el 2026-08-20 | 1 documento | **17 apariciones** de «exigible todavía» |
| `PT-01.a`, medida el 2026-08-13 | 3 documentos | **10 apariciones** en 9 documentos |

**Ninguna de las cinco es un error de juicio.** Las cinco decisiones son correctas, fechadas y
registradas. Lo que falta es el camino de vuelta, y el framework lo tiene reportado como el
[reporte 24](https://github.com/hdcm-dev/IA.SDD.Documentacion): **`Root-Rules.md` §12.1 y §12.2 obligan a
declarar por qué evento se volverá, y nada obliga al evento a volver.**

---

## 1. Cómo se usa, y la única regla que hay que respetar

**Cada fila tiene un `estado`, y de él depende que la compuerta falle:**

| `estado` | Qué significa | Qué hace la compuerta |
|---|---|---|
| **`pendiente`** | La frase **todavía vive** en el corpus. La barrida no se hizo | **Informa y no falla.** Es la lista de trabajo |
| **`vigilada`** | La barrida se hizo y la frase **ya no debería aparecer** | **FALLA si reaparece** |

**Una fila pasa de `pendiente` a `vigilada` en la misma unidad de trabajo que hace la barrida, nunca
antes.** Poner `vigilada` sobre una frase que todavía vive deja la compuerta en rojo permanente, y
**una compuerta en rojo permanente es una compuerta apagada** — lo aprendimos con `C-3`, que estuvo
catorce días informando un defecto real que nadie leía.

**Un patrón que atrapa también a la corrección es un patrón malo, y lo aprendimos en la primera
corrida.** `RE-06` seguía sonando después de arreglar su superviviente, porque la frase corregida
—«**llevaba** la marca…»— repetía el literal que el patrón busca. La salida no es aflojar el patrón: es
**escribir la corrección sin repetir el token vigilado**. Un patrón que no distingue la afirmación de la
mención obliga a elegir entre dejarlo sonar o apagarlo, y las dos son la misma derrota. Es la familia del
falso positivo de `[etiqueta](destino)`, que crece cada vez que alguien lo documenta.

**Y una fila nueva se agrega EN LA MISMA UNIDAD que toma la decisión**, no después. Si la decisión no
retira ninguna frase, se escribe igual con el patrón vacío y una línea que lo diga: **la ausencia
declarada es distinta de la ausencia por olvido**, que es exactamente lo que este documento existe para
distinguir.

---

## 2. El registro

**Formato de cada fila, y lo lee un guion**: `id | decisión | patrón | dónde puede vivir | estado | dueño`.
El patrón es una expresión regular extendida, **sin alternancia**: la barra vertical es el separador de
celdas de la tabla y un patrón que la use rompe la lectura —pasó en la primera corrida—. Dos frases
distintas van en **dos filas**. La columna «dónde puede vivir» lista prefijos de ruta
donde la frase es **legítima** —típicamente `Audit/`, porque un informe que cita la frase retirada para
explicar que se retiró **no es una reaparición**—.

| id | Decisión o hecho | Patrón de la frase retirada | Dónde puede vivir | Estado | Dueño |
|---|---|---|---|---|---|
| `RE-01` | **`D4`** (2026-08-20): se adopta el valor por omisión del servidor para el tamaño del cuerpo | `se ancla en la etapa .a.` | `SDD/Docs/Audit/` | `pendiente` | `U-11` |
| `RE-02` | **`D8`** (2026-08-20): el *mutation score* **no** entra al pipeline | `exigible todav` | `SDD/Docs/Audit/` | `pendiente` | `U-12` |
| `RE-03` | **`D3`** (2026-08-26): la vigencia del acceso son 480 minutos por omisión | `no está declarado por ninguna fuente` | `SDD/Docs/Audit/` | `pendiente` | `U-12` |
| `RE-04` | **`D1`** (2026-08-26): los umbrales quedan confirmados y sus puertas, bloqueantes | `\*\*Condicionado` | `SDD/Docs/Audit/` | `pendiente` | `U-13` |
| `RE-05` | **La cobertura se mide desde el 2026-08-27** (`scripts/coverage.sh`) | `No hay código construido` | `SDD/Docs/Audit/` | `pendiente` | `U-13` |
| `RE-06` | **`PT-01.a`** (2026-08-13): el hosting soporta `net10.0` | `versión de plataforma que soporta el hosting.{0,40}\[A VERIFICAR\]` | `SDD/Docs/Audit/` | **`vigilada`** | — |
| `RE-07` | **`PA-01`** (2026-08-20): no hay biblioteca de componentes | `[Aa]nclar la versión de la biblioteca de componentes` | `SDD/Docs/Audit/` | **`vigilada`** | — |
| `RE-08` | **`MI-01`** (2026-08-31): el respaldo existe | `no hay ningún guion de respaldo` | `SDD/Docs/Audit/` | **`vigilada`** | — |
| `RE-09` | **`MI-09`** (2026-08-31): `/salud` evalúa el almacén en cada consulta | `punto de salud publica un sello del arranque` | `SDD/Docs/Audit/` | **`vigilada`** | — |

**Las cuatro `vigiladas` son las decisiones cuya barrida ya se hizo**, tres de ellas hoy. Las cinco
`pendientes` son la lista de trabajo de las unidades documentales del plan, y **cada una las pasa a
`vigilada` al terminar**.

---

## 3. Ítems diferidos, con disparador medible

**La otra mitad del mismo problema.** Uno vigila lo que se **retiró**; éste vigila lo que se
**pospuso**. `Root-Rules.md` §12.2 declara la forma de un ítem diferido —cuatro campos— y **no declara
dónde viven**: hoy cada uno queda escrito en el documento que a alguien se le ocurra, y **§12.3, que
sería el registro durable, no existe** (reportado al framework).

**La regla, y es dura: un ítem sin disparador MEDIBLE no se puede diferir. Se escala.** «Cuando
moleste» no es un disparador; «cuando la comisión pase de 300 trabajos» sí, y «en el punto de control de
la fase `i`» también.

| id | Qué se difirió | Por qué no ahora | Disparador de reapertura | Dueño |
|---|---|---|---|---|
| `DF-01` | El **fragmento de retorno** al filtrar por debajo de 768 px no tiene destino visible | Deuda menor de `U-02`; la ruta alternativa existe | Que una medición de uso muestre que el docente pierde el lugar al volver | Equipo |
| `DF-02` | **Disco lleno** no lo detecta la comprobación de salud | Detectarlo exige escribir de verdad sobre el almacén de producción | Que el volumen del destino pase del 80 % de ocupación | Equipo, en la fase `i` |
| `DF-03` | **`restart: unless-stopped` no reacciona** a un `healthcheck` en rojo | Docker reinicia por salida del proceso, no por salud; hoy no hay ciclo que corregir | Que entre al despliegue un supervisor que **sí** reaccione a la salud | Equipo |
| `DF-04` | El **peso del documento** se mide en unidades del DOM y no en bytes transferidos | El instrumento reporta `content().length`; la compresión no está activa | `U-09`, que separa las dos columnas | Equipo |
| `DF-05` | **`gf-notice`** existía como clase inventada; se retiró usando `gf-banner` | El sistema visual no tiene un aviso propio para este caso | Que la maqueta incorpore un aviso distinto del `banner` | Equipo |
| `DF-07` | **`[StreamRendering]` para las tres superficies de listado** — es la única salida que permite un esqueleto sin volver la página interactiva | El producto no lo usa en ninguna parte; adoptarlo es una decisión de arquitectura de la pieza pública y no un arreglo de wireframe | Que una medición sobre el destino real muestre la primera pintura por encima de **400 ms** con el volumen que la comisión tenga | Equipo, con `ADR-10001` |
| `DF-06` | **`NotFoundPage` es interactiva sin motivo declarado** — residuo de la etapa `b`, la única de las seis que no justifica su modo en la cabecera | Volverla estática es un cambio de código que `ADR-10001` 1.1 **no ordena**; la ADR fija el criterio, no reescribe lo existente | Que se toque esa superficie por cualquier otro motivo, o que la salida preferente de `ADR-10001` §7 se active | Equipo |

---

## 4. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.0 | 2026-08-31 | Emisión inicial. Es `U-04` del plan de la mesa, y contiene sus dos «cosas nuevas» `N-2` y `N-4`, **que son la misma vista de los dos lados**: una vigila lo que se retiró y la otra lo que se pospuso. Nace con **nueve** frases retiradas —cuatro `vigiladas` y cinco `pendientes`, que son la lista de trabajo de las unidades documentales— y **cinco** ítems diferidos con disparador medible. El diseño clave es la columna `estado`: **la compuerta falla sólo sobre lo `vigilado`**, para que no nazca en rojo — la lección de `C-3`, que estuvo catorce días informando un defecto real sin que nadie lo leyera, **porque una compuerta en rojo permanente es una compuerta apagada**. | Orquestador SDD |
