# Estado del destino — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Estado-Del-Destino-2026-08-27.md
**Versión:** 1.2
**Fecha:** 2026-08-27
**Autor:** Orquestador de reanudación SDD
**Nivel:** Producto
**Instrumento:** `Master-Prompt-Reanudacion.md` **1.10**, fases R0 a R4
**Supera a:** [`Estado-Del-Destino-2026-08-23.md`](Estado-Del-Destino-2026-08-23.md) 1.1
**Registro de mesa:** [`Mesa-2026-08-27.md`](Mesa-2026-08-27.md) 1.0

---

## 1. Qué es este documento

**Es la quinta reanudación de este destino, y la primera con mesa de evaluación.** Reconstruye en qué
estado está leyendo el árbol, sin memoria de ninguna sesión anterior, declara sus divergencias,
registra la decisión del Product Owner y deja el punto de continuación.

**La novedad de esta corrida es `R1.5`**, la fase preplanificadora que el framework **13.7** publicó
hoy: un panel que refuta el corpus **antes** de que se elija la salida, para que la decisión más cara
del método no se tome sobre la mitad de la información. Su mecánica y su resultado completo viven en
[`Mesa-2026-08-27.md`](Mesa-2026-08-27.md); acá va lo que R2 consumió de ella.

**No es un audit.** Declara estado, no veredicto: no aprueba ni rechaza nada y no tiene niveles de
hallazgo. Los niveles que aparecen citados son los de la mesa.

---

## 2. Paso 0 de R0 — la compuerta de arranque

**Por primera vez en las cinco reanudaciones, T0 no detuvo nada.**

```text
COMPUERTA DE ARRANQUE — Lab-Geometria   (Master-Prompt.md §12.1 T0)
  Rama:            main, al día con origin/main (0 adelante, 0 atrás)
  Árbol:           limpio
  Entregas vivas:  ninguna — 0 ramas remotas sin fusionar en origin/main
  Ramas a borrar:  ninguna
  Veredicto:       EN ORDEN, se puede empezar
```

**Se publica aunque esté todo en orden**, porque es lo único que distingue «no había nada que
arreglar» de «no se miró», y porque el historial del repositorio es el contraste observable de las
dimensiones 3 y 5: sin esta compuerta, las dos se leen contra un observable incompleto.

---

## 3. Las seis dimensiones

| # | Dimensión | Fuente declarativa | Quién la mantiene | Lectura | Contraste observable | Resultado |
|---|---|---|---|---|---|---|
| 1 | ¿Hay documentación generada? | — | — | — | **482 archivos `.md` vivos** en `SDD/Docs/` — 499 archivos contando los `.json` de `Audit/`, 1046 con `_legacy/` | **Sí** |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST` **5.1** §1.1 | La generación y la migración | **SDD 13.3**, reescrita por M5 el 2026-08-25 | `CHANGELOG` del framework: **13.7** (2026-08-27, hoy) | **Diverge — `D-06`**, y ver §6 |
| 3 | ¿La migración terminó? | [`Informe-Migracion-10.0-a-13.3.md`](Informe-Migracion-10.0-a-13.3.md) 1.0 — **APROBADO CON HALLAZGOS**, en segunda ronda | La migración | **Completa**: once de once filas del plan resueltas, 160 estados previos archivados, ningún contenido sin destino | **0 carpetas `_fusion/`**. Los **siete** planes de migración tienen su informe | **Coincide** |
| 4 | ¿Qué quedó abierto? | Informe de migración §7 (`HM-01`, `HM-02`, `HM-03`) y [`A3-Decisiones-Del-Product-Owner.md`](A3-Decisiones-Del-Product-Owner.md) 1.6 | Quien cierra cada hallazgo, nombrado en el hallazgo | `HM-01` del destino; `HM-02` y `HM-03` del framework; **dos** decisiones del Product Owner abiertas, `D6` y `D7` | Ítems diferidos contados sobre el árbol: §4. **`HM-01` abierto y anclado por la mesa**: `H-01` | **Coincide, con `HM-01` reparado — §9** |
| 5 | ¿En qué etapa de construcción va? | [`../../../changelog.md`](../../../changelog.md) | El **equipo de desarrollo**, en la rama de la etapa; verifica el **Product Owner** en el PR | Etapas `a` a `h` cerradas, más **los prerrequisitos de la fase `i`** y **su puerta**, las dos repuestas y declaradas el 2026-08-18 | Último commit sobre código: **`c4d2f23`, 2026-08-18**, «etapa `i`: la puerta antes del despliegue, y el formulario de PT-05 vacío». Los **55 commits** siguientes tocan **sólo `SDD/`** | **COINCIDEN** |
| 5' | ídem, segunda fuente | `Estrategia-Versionado.md` §1 punto 4 y §4.1 | Categoría 09 | «Cada etapa cerrada y fusionada recibe una etiqueta» | `git tag` → **cinco**: `v0.1.0`, `v0.2.0`, `v0.5.0`, `v0.7.0`, `v0.8.0`, para **ocho** etapas | **Divergía — `D-03'`, REPARADA — §9** |
| 6 | ¿Qué falta para la siguiente? | [`../00-Contexto/Roadmap-Producto.md`](../00-Contexto/Roadmap-Producto.md) **1.9** §2.1 y §5.2 | Quien cierra cada etapa | Fase **`i` · Despliegue real**, `F-14` sola, `PT-05` | — | **Punto de continuación en §10** |

**La dimensión 5 coincide por segunda vez seguida, y el motivo sigue siendo el mismo**: desde el
2026-08-18 no hubo trabajo de código, de modo que no hubo ocasión de incumplir la regla. **El próximo
cambio en `src/` la vuelve a poner a prueba**, y esta vez con la excepción de las etiquetas ya escrita
donde vive la regla.

---

## 4. Ítems diferidos (`Root-Rules.md` §12.2)

**Contados sobre el árbol vivo, no leídos de un informe** — y el recuento anterior estaba
desactualizado, que es el hallazgo `H-07` de la mesa.

**Son ocho documentos y 118 filas, no seis y 116.** La migración 10.0 → 13.3 emitió `PD-10` en las
**dos** `Supply-Chain-Seguridad.md`, con los cuatro campos de §12.2, y el recuento que
`Estado-Del-Destino-2026-08-23.md` §4 declaraba **no las incluyó**. Los documentos son: los dos
`Product-Backlog.md`, los dos `Pipeline-CI-CD.md`, las dos `Arquitectura-Unidad-Entrega.md` y las dos
`Supply-Chain-Seguridad.md`.

| Estado | Antes de esta corrida | **Después de la salida `A`** | Qué significa |
|---|---|---|---|
| Cerrados | 85 | **86** | `PA-05` de `-Web` `06` cerrado por lectura, parche `P-02` |
| **Vencidos** | **13** | **9** | Su evento de cierre ya ocurrió → **`P1`** por la tabla de escalamiento de §12.2 |
| Vigentes | 7 + 2 (`PD-10`) | **12** | Su evento no ocurrió. **Ocho de los doce esperan la fase `i`** |
| **Sin evento** | **11** | **11** | **No conformes con §12.2**, elevados al Product Owner como `E-04`. **Sin evento, nada las puede vencer nunca** |
| **Total** | 116 + 2 | **118** | |

**Los nueve vencidos que quedan son dos preguntas, no nueve**, y las dos son del Product Owner:
**cinco filas** de `D7` —la herramienta que calcula la versión— y **cuatro** de los dos umbrales de
`-Web`. Van como escaladas `E-02` y `E-03` de la mesa, con su default declarado.

**Los cuatro que dejaron de estar vencidos** son la versión de plataforma del hosting, en tres
documentos de `-Web`, y el formato de intercambio. Ninguno se cerró inventando un valor: **el primero
no era una decisión** y el segundo **estaba cerrado desde el 2026-08-10 y la fila no se había
enterado**. Ver §9.

---

## 5. Divergencias

| # | Dim. | Lectura declarativa | Lectura observable | Evidencia | Estado |
|---|---|---|---|---|---|
| **`D-06`** | 2 | Procedencia **SDD 13.3** | Framework vigente **SDD 13.7** | §6, diff artefacto por artefacto | **CERRADA en la segunda vuelta** — §8.1: la salida `C` actualizó la procedencia a **13.7**, con la verificación de §6 como fundamento |
| **`D-03'`** | 5' | `Estrategia-Versionado.md` §1 punto 4 y §4.1: «**ninguna etapa se cierra sin etiqueta**», en absoluto | **Cinco** etiquetas para **ocho** etapas | `git tag`; los tres huecos —`c`, `d` y `f`— declarados **en el `changelog.md` y no donde vive la regla** | **REPARADA** — §9, default `E-01` de la mesa |
| **`D-05`** | 2 | `PRODUCT-MANIFEST` línea 3: «plantilla de referencia **5.0**»; `PRODUCT-INTAKE` línea 3: «**3.0**» | La **§1.1 del propio manifiesto** declara la plantilla en **6.0** y el intake en **3.5**, y el árbol del framework las tiene así | Hallazgo `H-04` de la mesa, ancla **E2** | **REPARADA** — §9, parche `P-04` |

**Ninguna divergencia queda abierta salvo la de procedencia**, que es la única de las tres que **no es
un defecto**: diverge por diseño cada vez que el framework publica una versión, y hoy la publicó hace
horas.

---

## 6. Diff normativo 13.3 → 13.7, artefacto por artefacto

**Medido contra los archivos vivos del repositorio del framework, no deducido del `CHANGELOG`.**

| Artefacto del framework | Procedencia 13.3 | Vivo en 13.7 | Severidad | ¿Alcanza al árbol? |
|---|---|---|---|---|
| `Master-Prompt` | 8.12 | **8.14** | minor | **No.** Gobierna cómo se genera y se audita |
| `Master-Prompt-Migracion` | 2.8 | **2.9** | minor | **No.** M1 convoca la mesa; no cambia forma de documento |
| `Master-Prompt-Reanudacion` | 1.9 | **1.10** | minor | **No.** Este mismo prompt: entra `R1.5` |
| `Root-Rules` | 8.4 | **8.6** | minor | **No.** 8.5 fija cómo se acuña un identificador de rol **del framework**; 8.6 agrega `AG-00970`, el presidente de mesa. Ninguna de las dos toca la forma de un artefacto del destino |
| `Catalogo-De-Criterios` | 1.13 | **1.14** | minor | **No.** Índice: no define criterios |
| `Maqueta-Rules` | 4.4 | **4.5** | minor | **No.** Rotula sus ítems como sustituibles; la Fase B2 de `-Web` está confirmada y cerrada |
| `Rules-Base-Conocimiento` | 2.0 | **2.2** | minor | **No.** Las **dos** unidades declaran `usa_llm == false` |
| **`Mesa-Rules`** | — | **1.0** | — | **Sólo al método, no al corpus.** Gobierna esta misma fase `R1.5`, y **su registro es un artefacto nuevo del destino**: [`Mesa-2026-08-27.md`](Mesa-2026-08-27.md) |
| Las otras **16 reglas** y las **2 plantillas** | — | **sin cambio** | — | — |

**Alcance documental del salto: cero artefactos del destino.** Es la primera vez desde la 9.9 → 9.10
que un salto no toca nada, y la primera desde que existe el umbral de continuidad en que los **cuatro**
saltos declaran su bloque «Impacto sobre destinos existentes» **vacío**:

- **13.4** y **13.5**: «**Ninguno**».
- **13.6**: «Ninguno forzado por la publicación, y no es una migración» — la obligación del banco rige
  para las compuertas escritas **desde esa versión en adelante**.
- **13.7**: «Ninguno forzado por la publicación» — *«un destino cuya reanudación anterior corrió sin
  mesa **no queda no conforme**: la fase no existía»*.

---

## 7. Recomendación, y su fundamento

```text
RECOMENDACIÓN — A · Reparar primero, y por qué
  Continuidad del origen: SOSTENIBLE — cero major con impacto entre 13.3 y 13.7
  Alcance real del salto: 0 artefactos del destino, de 8 artefactos del framework que se movieron
  Volumen alcanzado:      482 documentos vivos
  Estado del repositorio: limpio, main al día, sin entregas vivas (T0 EN ORDEN)
  Divergencias abiertas:  3 — D-06 por diseño, D-03' residual, D-05 nueva
  Costo de no hacerlo hoy:los 13 vencidos son 13 hallazgos P1 que la próxima corrida vuelve a contar
  Alternativa razonable:  D · continuar la construcción (fase `i`)

  DE LA MESA (§3.1)
  Hallazgos procedentes:  7 — P1 × 3, P2 × 3, P3 × 1
  Parches listos:         6, en capas 05, 06, 09, Intake y Audit — 11 celdas
  Deuda declarada:        1, con evento en la próxima migración normativa que alcance artefactos
  Escaladas al humano:    4, agrupadas y con default
```

### 7.1 Los seis factores, desarrollados

- **Continuidad del origen.** Entre la procedencia y la vigente hay **cero major**. Los cuatro saltos
  son minor y los cuatro declaran su bloque de impacto vacío.
- **Alcance real del salto.** Ocho artefactos del framework se movieron y **ninguno alcanza un
  artefacto de este destino**, verificado uno por uno en §6.
- **Volumen alcanzado.** 482 documentos vivos, de los cuales el salto toca **cero**.
- **Estado del repositorio.** `EN ORDEN`, sin detención, por primera vez en cinco reanudaciones.
- **Divergencias abiertas.** Tres, y **dos las repara `A` con parches ya escritos**.
- **Costo de no hacerlo hoy.** Los cuatro parches de ítems diferidos son once celdas; no aplicarlos
  deja trece filas que la próxima reanudación vuelve a contar como trece `P1`, y **dos de ellas
  afirman cosas falsas** —un punto «abierto» que está cerrado y un ítem «cerrado» que está abierto—.
- **Alternativa razonable: `D`.** Ganaría si el despliegue fuera urgente: `PT-05` valida `RN-B1`
  —«sin acceso el laboratorio no existe», impacto **Alto**— y es la única medición que puede obligar a
  mover la topología. **La fase `i` no toca ninguno de los cinco documentos que los parches tocan**, de
  modo que las dos no compiten y `A` no la demora más que una revisión de pull request.

### 7.2 El umbral de continuidad, y por qué esta vez sí se puede actualizar la procedencia

`Master-Prompt-Reanudacion.md` §4.0.1 fija el umbral de forma mecánica: **cuántos major con bloque de
impacto no vacío atraviesa el salto**.

| | 2026-08-23 | **Hoy** |
|---|---|---|
| Major con impacto entre procedencia y vigente | **Dos** (11.0 y 12.0) | **Cero** |
| Qué dice la tabla de §4.0.1 | «Ninguna regla vigente puede auditar ni extender ese corpus» → **B**, y decirlo con esas palabras | «El desfase es de proceso. **C es correcta y barata**» |
| Se ofrecía `C` como equivalente | **No** | **Sí** |

**Y la condición que `C` exige está cumplida.** Actualizar la procedencia sin migrar sólo procede
cuando **se verificó artefacto por artefacto que el salto no alcanza al destino**, con la lista de qué
cambió y por qué cada cosa no lo toca. **Esa lista es §6 de este informe.** No se afirma que el delta
«parece chico»: se enumeran los ocho artefactos que se movieron, con su severidad y con el motivo por
el que cada uno queda afuera.

---

## 8. Decisión

| Campo | Valor |
|---|---|
| **Salida elegida** | **A · Reparar primero** |
| **Quién** | El Product Owner, el **2026-08-27** |
| **Sobre qué** | Las cinco salidas presentadas en R2, con la recomendación **A** y su alternativa **D**, y con el plan de la mesa ya escrito |
| **Alcance acordado** | **Los seis parches de la mesa** y **el default de `E-01`**. `E-02`, `E-03` y `E-04` quedan abiertas con su default declarado, que es **no actuar** |
| **Qué sigue** | **Recalcular y volver a preguntar.** Reparadas las divergencias, lo que sigue decidiendo es **migrar o seguir en la versión declarada** |

**La recomendación fue `A` y se eligió `A`.** Es la única salida que las otras cuatro dan por hecha:
elegir `B`, `C`, `D` o `E` con una divergencia abierta significa trabajar sobre un estado que el árbol
declara mal.

### 8.1 La segunda vuelta, y su decisión

**Fusionada la reparación en el `PR #94`, se volvió a R0 sobre el árbol reparado** y la recomendación
se recalculó. **Esta vez sí se movió**, y conviene decir por qué: el 2026-08-23 la reparación de `D-04`
no tocó ninguno de los dos factores que fundaban la recomendación, y acá **las dos divergencias que
hacían de `A` la única salida honesta quedaron cerradas** —`D-03'` y `D-05`—, de modo que lo único
abierto pasó a ser el desfase de procedencia, que es exactamente lo que la pregunta decide.

**T5, la verificación del merge:** `f3815fb` alcanzable desde `main` por `9fc28e5`, `main` **no trajo
nada por encima de lo entregado**, la rama borrada y el árbol limpio. El recuento sobre el árbol
fusionado confirma lo entregado: **118 filas, 9 vencidas, 86 cerradas, 12 vigentes, 11 sin evento**, y
**5399 enlaces con 0 rotos**.

| Campo | Valor |
|---|---|
| **Salida elegida** | **`C` · Seguir en la versión declarada, actualizando la procedencia a 13.7** — y **`D` a continuación**, en la misma sesión |
| **Quién** | El Product Owner, el **2026-08-27**, en la segunda vuelta |
| **Sobre qué** | Las salidas recalculadas de R2, con la recomendación **`C`** y su alternativa **`D`** |
| **Qué continúa** | `Master-Prompt.md`, **con la decisión ya tomada**: su reconciliación normativa la recibe, informa el desfase **como decidido** y **no vuelve a detenerse** |
| **Qué no resuelve** | Las tres escaladas abiertas de la mesa —`E-02`, `E-03` y `E-04`— y el avance de construcción, que es la salida `D` |

**El acto de `C` fue una tabla y no un trabajo**, que es la diferencia con las siete migraciones
anteriores: `PRODUCT-MANIFEST` **5.2 → 5.3**, con §1.1 reescrita a **13.7** y la lista de §6 como
fundamento. **Ningún documento del corpus cambia.**

**`B` no se eligió y el informe deja escrito por qué**, para que la decisión sea auditable después: un
salto de cuatro versiones que sólo movió reglas de proceso **no toca ningún artefacto**, y migrar por el
número es trabajo sin resultado.

---

## 9. Reparación de la salida A

**Rama:** `arreglo/mesa-2026-08-27`. **Unidad de trabajo:** una reparación, con los parches que la mesa
diseñó y **sin diseñar ninguno nuevo**.

| Parche | Divergencia u hallazgo | Qué se hizo | Documento |
|---|---|---|---|
| **`P-01`** | `H-01` = `HM-01` del informe de migración §7 | Tres filas pasan de **«Cerrado»** a **«Cerrado en parte»**: se cerraron «por lectura» el 2026-08-20 con una lista de herramientas que **no incluye ningún generador de inventario**, y su enunciado lo nombra. El generador sigue abierto como `PD-10` | `Pipeline-CI-CD.md` de `-Api` **3.5 → 3.6** y de `-Web` **3.3 → 3.4** |
| **`P-02`** | `H-02` | `PA-05` pasa de **VENCIDO, «el punto sigue abierto»** a **cerrado por lectura**: su propia fuente —`05` §11 `PA-03`— lo declara cerrado desde el **2026-08-10**, resuelto por `Api ADR-00002` | `Product-Backlog.md` de `-Web` **4.1 → 4.2** |
| **`P-03`** | `H-03` | La evidencia de cierre citaba **`Api ADR-10002`**, que **existe y es otro**; el correcto es `ADR-00002`, al que el enlace de la columna anterior ya apuntaba bien | `Arquitectura-Unidad-Entrega.md` de `-Web` **3.4 → 3.5** |
| **`P-04`** | `D-05` / `H-04` | Línea 3 del manifiesto: **5.0 → 6.0**. Línea 3 del intake: **3.0 → 3.5**, con la constancia de que se emitió sobre la 3.4 y de que el delta es **opcional**. **La procedencia de §1.1 no se toca** | `PRODUCT-MANIFEST` **5.1 → 5.2** y `PRODUCT-INTAKE` **3.0 → 3.1** |
| **`P-05`** | `H-06` | La cabecera citaba `Roadmap-Producto.md` **1.8**; el roadmap está en **1.9**. **El formulario sigue en `SIN MEDIR`** | [`Medicion-PT-05.md`](Medicion-PT-05.md) **1.0 → 1.1** |
| **`P-06`** | `H-05` | El evento de cierre de las tres filas de la versión de plataforma del hosting pasa del punto de control de la etapa `a` a la **fase `i`**: la fuente las rotula `[A VERIFICAR]` y **esas marcas se resuelven midiendo, no decidiendo** | Los tres documentos de `-Web` de arriba |
| **`E-01`** | `D-03'` | Se escribe **la excepción de `c`, `d` y `f` con su motivo donde vive la regla**, en lugar de retirar el absoluto. La regla **no se retira**: rige desde la fase `i` en adelante | `Estrategia-Versionado.md` de `-Api` **4.0 → 4.1** |

**Verificación corrida sobre el árbol reparado:** 5385 enlaces relativos, **0 rotos**; las tres tablas
de ítems diferidos tocadas conservan sus **cinco columnas** en todas sus filas; **vencidos de 13 a 9**;
y las cuatro filas de `-Api` cerradas por `A2b` **cuyo enunciado no nombra el generador siguen
intactas**, verificadas una por una.

**Qué NO toca esta reparación.** Ningún código, ninguna prueba, ningún guion de puerta. Ninguna
historia de usuario. **La procedencia de `PRODUCT-MANIFEST` §1.1**, que sigue en SDD **13.3** hasta que
la segunda vuelta la decida. Y **nada de `E-02`, `E-03` ni `E-04`**, cuyo default declarado es no
actuar.

---

## 10. Punto de continuación

### 10.1 La segunda vuelta, que es lo primero que sigue

**Fusionada esta reparación, se vuelve a R0 sobre el árbol reparado** y la recomendación **se
recalcula**. `Master-Prompt-Reanudacion.md` §4.0.2 exige decirlo con estas palabras, para que quien
eligió `A` sepa que está en la segunda vuelta y no lea la pregunta como nueva:

> Reparadas las divergencias, lo que sigue decidiendo es **migrar a la vigente o seguir en la versión
> declarada**. La recomendación recalculada es **`C` · seguir en la versión declarada, actualizando la
> procedencia a 13.7**, por el umbral de §7.2 — **cero major con impacto**, y la verificación artefacto
> por artefacto que `C` exige ya está escrita en §6.

**Ocurrió, y su desenlace está en §8.1: el Product Owner eligió `C`, y `D` a continuación.** Este
renglón queda escrito en lugar de reemplazarse por el resultado, porque lo que hace auditable la
decisión es que se vea que la pregunta se hizo dos veces y que la segunda **no se disimuló como si
fuera la primera**.

**`B` no se recomienda y conviene decir por qué**, para que no se elija por el número: un salto de
cuatro versiones que sólo movió reglas de proceso —cómo se reanuda, cómo se convoca una mesa, cómo se
acuña un identificador del catálogo del framework— **no toca ningún artefacto**, y migrar por el número
es trabajo sin resultado.

### 10.2 La salida `C` — **EJECUTADA el 2026-08-27**

`Master-Prompt.md`, **con la decisión ya tomada**: su reconciliación normativa la recibió, informa el
desfase **como decidido** y no volvió a detenerse. El acto concreto fue **una tabla**:
`PRODUCT-MANIFEST` **5.2 → 5.3**, con §1.1 reescrita a **13.7**, la lista de §6 como fundamento y
`Mesa-Rules` **1.0** incorporada como artefacto que **alcanza al destino aunque no a su corpus**.
**Ningún documento del corpus cambió**, que es la condición que hacía a `C` correcta y barata.

### 10.3 La salida `D` — la construcción, que no tiene prompt · **ARRANCADA el 2026-08-27**

**El alcance comprometido está cerrado.** Las ocho etapas `a` a `h` están construidas y demostradas, y
la fase `i` tiene ya sus prerrequisitos y su puerta escritos desde el 2026-08-18.

| Campo | Valor |
|---|---|
| **Etapa siguiente** | **Fase `i` · Despliegue real** |
| **Capacidad** | **`F-14`, sola** |
| **Puerta técnica** | **`PT-05`** — medir el acceso desde la red de la facultad. Es **lo único que esa fase hace** |
| **Qué valida** | **`RN-B1`**, impacto **Alto**: «sin acceso el laboratorio no existe» |
| **Entregable** | Front publicado por FTP en el hosting, servicio de datos en el servidor propio |
| **Puerta de entrada** | `Roadmap-Producto.md` **1.9** §5.2, transición `h` → `i`, **ya satisfecha** |
| **Puerta de salida** | `scripts/verify-stage-i.sh`, **siete criterios**; [`Medicion-PT-05.md`](Medicion-PT-05.md) **1.1** en `SIN MEDIR`, y el guion comprueba que ese estado **ya no lo diga** |
| **Documentos que la gobiernan** | `Roadmap-Producto.md` **1.9** §2.1 y §5.2 · `Pipeline-CI-CD.md` §2.1 (`QG-01`, `QG-02`) · `Entornos-Deploy.md` §3 · `ADR-14003` 1.1 · `Estrategia-Versionado.md` **4.1** |
| **Qué la bloquea** | **Sólo el Product Owner**: secretos del hosting, acceso al host y una persona en la red de la facultad |
| **Qué destraba de paso** | **Ocho de los doce ítems vigentes**, cuyo evento de cierre es esta fase — incluidos los dos `PD-10` del generador del inventario y las tres filas que `P-06` reasignó |

#### Qué se hizo el 2026-08-27 al arrancar `D`, y por qué es poco

**Se corrió la puerta y se corrigió su cabecera. Nada más, y no por falta de ganas.**

- `scripts/verify-stage-i.sh` citaba `Roadmap-Producto.md` **1.8** en su cabecera y el roadmap está en
  **1.9**. Es **el mismo hallazgo `H-06`** que la mesa reparó en `Medicion-PT-05.md`, **en el único
  lugar donde no lo había buscado**: un guion. Corregido. Los otros dos usos de «1.8» que quedan en el
  árbol están en `changelog.md` y **son correctos**: registran lo que era cierto el día que se
  escribieron.
- **La puerta se corrió y se comportó como debe.** Sin `PUBLIC_URL` ni `API_URL` devuelve
  `NO SE PUEDE MEDIR` y **sale con código 2** —distinto del 1 de «no conforme» y del 0 de «conforme»—,
  que es la misma convención de `verify-stage-g.sh`. **No pasa en verde**, que es lo que se estaba
  verificando: *«pasar en verde sería afirmar que se midió lo que no se miró»*.

**Y acá se detiene, con la frontera declarada.** Los siete criterios de la transición `i` → `j…`
**miden un despliegue que existe**, y este despliegue no existe todavía. `I-4` necesita **una persona
en la red de la facultad** e `I-5` **dos personas recorriendo el circuito sobre el despliegue real**;
los otros cinco necesitan **las dos direcciones**, que llegan por entorno y salen de secretos que no
están en el repositorio —y que **no deben estarlo**—.

**Lo que la fase `i` necesita, y es todo del Product Owner:**

| # | Qué hace falta | Sin eso |
|---|---|---|
| 1 | Los **secretos del hosting** para el flujo de FTP declarado | `I-1` no se puede correr |
| 2 | El **acceso al servidor propio** donde corre el servicio de datos | `I-2`, `I-3` y `I-6` no se pueden correr |
| 3 | **Una persona en la red de la facultad**, y un alumno de verdad | `I-4` — `PT-05` — no se puede medir, y es **lo único que esta fase hace** |
| 4 | **Dos personas** para el circuito de punta a punta | `I-5` no se puede declarar |

**El resto ya está**, y está desde el 2026-08-18: los prerrequisitos —`deploy/compose.yaml`,
`ADR-14003` y la configuración explícita—, la puerta con sus siete criterios, y
[`Medicion-PT-05.md`](Medicion-PT-05.md) emitido **vacío a propósito**, en `SIN MEDIR`, esperando el
número **sea cual sea**.

### 10.4 Lo que queda abierto y es del Product Owner

- **`E-02` · `D7`, la herramienta que calcula la versión.** Cinco filas vencidas. Tres opciones con su
  costo en [`Mesa-2026-08-27.md`](Mesa-2026-08-27.md) §9.
- **`E-03` · Los dos umbrales de `-Web`.** Cuatro filas vencidas. `D1` no los fijó y `05` §8 se niega a
  inventarlos.
- **`E-04` · Los once ítems sin evento de cierre.** No conformes con §12.2: sin evento, nada los puede
  vencer nunca.
- ~~**`D-06`, la procedencia en 13.3.**~~ **Cerrada** el 2026-08-27 por la salida `C` — §8.1 y §10.2.
- **`HM-02` y `HM-03`**, que no son de este destino: van al **reporte `16`** del framework, junto con lo
  que esta corrida agrega — la mesa **encontró y descartó** los seis falsos enlaces rotos y **tumbó un
  hallazgo propio** por refutación, que es la primera medición del mecanismo que la 13.7 publicó.

---

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.2 | 2026-08-27 | **Registra el arranque de la salida `D`**, elegida junto con `C` en la segunda vuelta. **§10.3 pasa de condicional a ejecutada** y declara lo poco que `D` pudo avanzar sin el Product Owner, con su frontera escrita: se corrió `scripts/verify-stage-i.sh`, que **devolvió `NO SE PUEDE MEDIR` con código 2** —la convención de `verify-stage-g.sh`, distinta del 1 de «no conforme»— y **no pasó en verde**, que es lo que se estaba verificando; y se corrigió su cabecera, que citaba `Roadmap-Producto.md` **1.8** con el roadmap en **1.9**. **Es el hallazgo `H-06` de la mesa en el único lugar donde no lo había buscado: un guion.** Se agrega la tabla de **las cuatro cosas que la fase `i` necesita**, las cuatro del Product Owner: secretos del hosting, acceso al servidor propio, una persona en la red de la facultad y dos personas para el circuito. | Orquestador de reanudación SDD |
| 1.1 | 2026-08-27 | **Registra la decisión de la segunda vuelta**, que es lo que la salida `A` obliga a volver a preguntar. Fusionada la reparación en el `PR #94`, R0 corrió de nuevo sobre el árbol reparado y **la recomendación se recalculó y esta vez SÍ se movió**: `D-03'` y `D-05` quedaron cerradas y lo único abierto pasó a ser el desfase de procedencia, de modo que `A` dejó de ser la salida obligada y la recomendación pasó a **`C`**. El Product Owner eligió **`C` y `D`**, en ese orden y en la misma sesión. **§8.1 es nueva** y lleva la decisión con su autor, su fecha y la verificación de **T5**; **§5 marca `D-06` como cerrada** y **§10.2 registra `C` como ejecutada** — `PRODUCT-MANIFEST` **5.2 → 5.3**, procedencia a **13.7**, ningún documento del corpus tocado. **§10.1 conserva el renglón de la pregunta pendiente** en lugar de reemplazarlo por su resultado, para que se vea que la pregunta se hizo dos veces. | Orquestador de reanudación SDD |
| 1.0 | 2026-08-27 | Emisión inicial. **Quinta reanudación** del destino, con `Master-Prompt-Reanudacion.md` **1.10**, y **la primera con mesa de evaluación** —`R1.5`, publicada hoy con el framework 13.7—, cuyo registro es [`Mesa-2026-08-27.md`](Mesa-2026-08-27.md) 1.0. **T0 no detuvo la corrida por primera vez en cinco reanudaciones.** **§4 corrige el recuento de ítems diferidos**: son **ocho documentos y 118 filas**, no seis y 116, porque la migración 10.0 → 13.3 emitió `PD-10` en las dos `Supply-Chain-Seguridad.md` y el recuento anterior no las incluyó. **§6 mide el diff 13.3 → 13.7 artefacto por artefacto**: ocho artefactos movidos, **cero major**, y los **cuatro** saltos con bloque de impacto vacío. **§7.2 aplica el umbral de continuidad y lo declara dado vuelta** respecto del 2026-08-23: de **dos major con impacto** a **cero**, de modo que `C` vuelve a ofrecerse y **la verificación artefacto por artefacto que exige ya está escrita**. Recomendación **`A`** con alternativa **`D`**; el Product Owner eligió **`A`**. **§9 registra la reparación**: **seis parches y un default**, once celdas en siete documentos, **vencidos de 13 a 9**, `D-03'` y `D-05` cerradas. §10 deja la segunda vuelta con su recomendación recalculada —**`C`**— y el punto de continuación de la fase `i`. | Orquestador de reanudación SDD |
