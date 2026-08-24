# Estado del destino — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Estado-Del-Destino-2026-08-23.md
**Versión:** 1.0
**Fecha:** 2026-08-23
**Autor:** Orquestador de reanudación SDD
**Nivel:** Producto
**Instrumento:** `Master-Prompt-Reanudacion.md` **1.9**, fases R0 a R4
**Supera a:** `Estado-Del-Destino-2026-08-18.md` 1.0

---

## 1. Qué es este documento

**Es la cuarta reanudación de este destino.** Reconstruye en qué estado está leyendo el árbol, sin
memoria de ninguna sesión anterior, declara sus divergencias, registra la decisión del Product Owner
y deja el punto de continuación para que el trabajo siguiente **no vuelva a deducir lo que acá está
deducido**.

**Es la única escritura de la reanudación sobre este destino**, salvo lo que la salida elegida
ejecute con su propia confirmación — acá, la reparación de la salida **A**, registrada en §7.

**No es un audit.** Declara estado, no veredicto: no aprueba ni rechaza nada y no tiene niveles de
hallazgo.

---

## 2. Paso 0 de R0 — la compuerta de arranque, y la entrega viva que detuvo el diagnóstico

**La corrida se detuvo antes de medir ninguna dimensión**, que es lo que `Master-Prompt-Reanudacion.md`
§2 paso 0 manda cuando T0 encuentra una entrega esperando fusión.

```text
COMPUERTA DE ARRANQUE — Lab-Geometria   (primera corrida)
  Rama:            docs/a3-decisiones-d4-d5-d8   al día con su remoto, 1 adelante de main
  Árbol:           limpio
  Entregas vivas:  rama docs/a3-decisiones-d4-d5-d8, empujada y sin fusionar
  Ramas a borrar:  ninguna
  Veredicto:       SE DETIENE: hay una unidad entregada esperando fusión (comprobación 4)
```

El Product Owner fusionó el **PR #78** y borró la rama. La verificación de **T5** confirmó el merge
—`b4a4804` alcanzable desde `main` por `b6031f1`—, que `main` **no trajo nada más por encima de lo
entregado**, y dejó el repositorio así:

```text
COMPUERTA DE ARRANQUE — Lab-Geometria   (después de T5)
  Rama:            main, al día con origin/main
  Árbol:           limpio
  Entregas vivas:  ninguna
  Ramas a borrar:  docs/a3-decisiones-d4-d5-d8 — borrada
  Veredicto:       EN ORDEN, se puede empezar
```

**Por qué esto va antes que las dimensiones y no es higiene.** El historial del repositorio es el
contraste observable de las dimensiones 3 y 5, y **no incluye lo que no está fusionado**: medir la
etapa de construcción contra un `main` que todavía no tenía el trabajo entregado habría declarado
«coincide» o «diverge» sobre un estado que estaba por cambiar.

---

## 3. Las seis dimensiones

| # | Dimensión | Fuente declarativa | Quién la mantiene | Lectura | Contraste observable | Resultado |
|---|---|---|---|---|---|---|
| 1 | ¿Hay documentación generada? | — | — | — | **494 archivos vivos** en `SDD/Docs/` (898 con `_legacy/`) | **Sí** |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST` **4.0** §1.1 | La generación y la migración | **SDD 10.0** | `CHANGELOG` del framework: **13.3** (2026-08-23) | **Diverge — `D-02`** |
| 3 | ¿La migración terminó? | `Informe-Migracion-9.12-a-10.0.md` 1.0 — **APROBADO CON UN HALLAZGO** | La migración | Completa, 0 P0, 0 P1, 1 P2 **cerrado el 2026-08-20**, 0 P3 | **0 carpetas `_fusion/`**. Los seis planes de migración tienen su informe | **Coincide** |
| 4 | ¿Qué quedó abierto? | `Informe-Migracion-9.12-a-10.0.md` §3, `Plan-Cierre-De-Pendientes.md` 1.0 y `A3-Decisiones-Del-Product-Owner.md` 1.0 | Quien cierra cada hallazgo, nombrado en el hallazgo | Ningún hallazgo de nivel P abierto. **Cinco decisiones del Product Owner** y la **segunda ronda de M6** sin encargar | Ítems diferidos contados sobre el árbol: ver §4 | **Coincide con lo declarado** |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` | El **equipo de desarrollo**, en la rama de la etapa; verifica el **Product Owner** en el PR | Última unidad: «Las etiquetas que nunca se crearon», rama `codigo/etiquetas-retroactivas` (PR #66). Etapas `a` a `h` cerradas | **Cero cambios** en `src/`, `tests/`, `scripts/`, `visor/`, `deploy/`, `.github/`, `Directory.Build.props` y la solución después de `39b8922`. Los doce PR siguientes tocan **sólo `SDD/`** | **COINCIDEN** |
| 5' | ídem, segunda fuente | `Estrategia-Versionado.md` §1.1 punto 4 y §4.1 | Categoría 09 | «Cada etapa cerrada y fusionada recibe una **etiqueta**» (§1.1) y «ninguna etapa se cierra sin etiqueta» (§4.1) | `git tag` → **cinco**: `v0.1.0`, `v0.2.0`, `v0.5.0`, `v0.7.0`, `v0.8.0`, para **ocho** etapas | **DIVERGE — `D-03'`**, residual |
| 6 | ¿Qué falta para la siguiente? | `Roadmap-Producto.md` **1.9** §2.1 y §5.2 | Quien cierra cada etapa | Fase **`i` · Despliegue real**, F-14 sola, `PT-05` | — | **Punto de continuación en §8** |

**La dimensión 5 dejó de divergir, y conviene decir por qué.** Es la primera reanudación de este
destino que la encuentra al día: en la del 2026-08-16 el registro estaba **tres etapas** atrás y en la
del 2026-08-18 **una unidad**. No es que la regla se haya empezado a cumplir sola — es que **desde el
PR #66 no hubo trabajo de código**, de modo que no hubo ocasión de incumplirla. El próximo cambio en
`src/` vuelve a ponerla a prueba.

---

## 4. Ítems diferidos (`Root-Rules.md` §12.2)

**Contados sobre el árbol vivo, no leídos de un informe.** Seis documentos llevan la tabla de cuatro
campos que §12.2 obliga: los dos `Product-Backlog.md`, los dos `Pipeline-CI-CD.md` y las dos
`Arquitectura-Unidad-Entrega.md` de las dos unidades de entrega.

| Estado | Filas | Qué significa |
|---|---|---|
| Cerrados | **67** | Cerrados por la migración 10.0 y por las pasadas `A2` y `A2b` del plan de cierre |
| **Vencidos** | **33** | **Su evento de cierre ya ocurrió** → **P1** por la tabla de escalamiento de §12.2 |
| Vigentes | **5** | Su evento no ocurrió. `PD-01` de la 09 espera la fase `i` |
| **Sin evento** | **11** | **No conformes con §12.2**, elevados al Product Owner |
| **Total** | **116** | Coincide con las 116 que la migración 9.12 → 10.0 volvió contables |

**El recuento coincide con el que declara el commit `b4a4804`** —«vencidos: de 37 a 33; sin evento: de
12 a 11»—, de modo que la fuente declarativa y el árbol dicen lo mismo. **Es la comprobación más
barata del método y por eso se hace acá**: la reanudación ya leyó el árbol entero sin memoria.

**Lo que estos 33 vencidos no son.** No son 33 decisiones pendientes del Product Owner:
`A3-Decisiones-Del-Product-Owner.md` 1.0 los agrupó en **ocho decisiones**, de las cuales **tres se
cerraron el 2026-08-20** —`D4`, `D5` y `D8`— y **cinco siguen abiertas**: `D1` los `[ASUNCIÓN]` del
intake §22 (~14 filas de un saque), `D2` la unidad de estimación, `D3` la vigencia del acceso firmado,
`D6` la versión de plataforma del hosting —que **la fase `i` contesta midiendo**— y `D7` la
herramienta `PA-06`.

---

## 5. Divergencias

| # | Dim. | Lectura declarativa | Lectura observable | Evidencia | Estado |
|---|---|---|---|---|---|
| **`D-02`** | 2 | Procedencia **SDD 10.0** | Framework vigente **SDD 13.3** | §6, diff artefacto por artefacto | **Abierta.** Por diseño, pero **ya no es barata**: ver el umbral de §7.2 |
| **`D-03'`** | 5' | `Estrategia-Versionado.md` de `-Api` §4.1: «**ninguna etapa se cierra sin etiqueta**», y §1.1 punto 4: «cada etapa cerrada y fusionada recibe una etiqueta, y la reversión es volver a la etiqueta anterior y reconstruir» | **Cinco** etiquetas para **ocho** etapas | `git tag` → `v0.1.0`, `v0.2.0`, `v0.5.0`, `v0.7.0`, `v0.8.0`. Los tres huecos —`c`, `d` y `f`— están declarados **con su motivo** en `changelog.md`, «Los tres huecos de numeración son deliberados» | **Abierta.** Residual de la `D-03` cerrada el 2026-08-18 |
| **`D-04`** | 6 | `Roadmap-Producto` **1.8** §5.2, criterio `F-1`: «los **nueve** casos de prueba obligatorios» | `PRODUCT-INTAKE` **1.20** §17.1.P.8 y §21, y `CV-26`: «los **diez** casos» | `FigureValidatorBatteryTests.cs`, cabecera: «la batería obligatoria del producto: **diez casos**» | **REPARADA** — §9 |

**`D-03'` es residual y no reincidencia.** La `D-03` original decía «cero etiquetas en todo el árbol»
y se cerró creando las cinco que se podían anclar sin inventar el punto. Lo que quedó abierto es de
otra naturaleza: **el documento de la 09 sigue afirmando la regla en absoluto**, sin la excepción que
el registro de cambios sí declara. Repararla es escribir la excepción donde vive la regla, o retirar
el absoluto — **y no se hizo en esta pasada**: el Product Owner acotó el alcance de `A` a `D-04`.

**`D-02` no la repara `A`.** La resuelven `B` —migrando— o `C` —declarando el desfase—, y por eso
vuelve a la mesa en la segunda vuelta de §10.

---

## 6. Diff normativo 10.0 → 13.3, artefacto por artefacto

**Medido contra los archivos vivos del repositorio del framework, no deducido del `CHANGELOG`.** La
columna de severidad se lee de la numeración, como `Migracion-Rules.md` §4.3 exige.

| Artefacto del framework | Procedencia 10.0 | Vivo en 13.3 | Severidad | ¿Alcanza al árbol? |
|---|---|---|---|---|
| `Master-Prompt` | 8.8 | **8.12** | minor | **No.** Gobierna cómo se genera y se audita, no la forma de ningún documento |
| `Master-Prompt-Migracion` | 2.8 | 2.8 | — | Sin cambio |
| `Master-Prompt-Reanudacion` | 1.8 | **1.9** | minor | **No.** Este mismo prompt |
| `Root-Rules` | **7.0** | **8.4** | **MAJOR** | **SÍ.** §9.1 declara **dos ámbitos** de unicidad y §9.2 hace cumplir el ancho de cinco dígitos a la familia `AG`; §10 R5 pasa de «único en el producto» a «único en su ámbito». **§13 nueva**, precedencia entre reglas, que es criterio de resolución y no forma de documento |
| `Rules-Contexto` | 4.4 | **4.5** | minor | **Sólo por la cita del rol.** Ver el párrafo de abajo |
| `Rules-Necesidades-Negocio` | 4.3 | **4.4** | minor | Ídem |
| `Rules-Especificacion-Funcional` | 5.4 | **5.5** | minor | Ídem |
| `Rules-UX-UI-DX` | 5.4 | **5.5** | minor | Ídem |
| `Rules-Arquitectura-Tecnica` | 4.4 | **4.5** | minor | Ídem |
| `Rules-Backlog-Tecnico` | **4.4** | **5.1** | **MAJOR** | **SÍ.** §4.4 punto 5 se parte en **5 (prioridad, del Product Owner)** y **5.b (estimación, del equipo)** |
| `Rules-Plan-Sprint` | 5.4 | **5.5** | minor | Sólo por la cita del rol |
| `Rules-Calidad-Y-Pruebas` | 4.5 | **4.6** | minor | Ídem |
| `Rules-Devops` | **5.0** | **6.1** | **MAJOR** | **SÍ.** Cuatro ítems se parten: §4.3 punto 5.b (semántica de sufijos), §4.4 punto 2.b (aprobación de `plan` antes de `apply`), §4.6 punto 1.b (generador de SBOM) y §4.6 punto 5.b (DAST, separado de SAST) |
| `Rules-Examples` | 6.4 | **6.5** | minor | Sólo por la cita del rol |
| `Rules-Documentacion` | 5.4 | **5.5** | minor | Ídem |
| `Intake-Rules` | 4.1 | **4.2** | minor | Ídem |
| `Vocabulario-Rules` | 3.1 | **3.2** | minor | Ídem |
| `Maqueta-Rules` | 4.3 | **4.4** | minor | Ídem |
| `Deriva-Rules` | 5.3 | **5.4** | minor | Ídem |
| `Migracion-Rules` | 3.15 | **3.19** | minor | **No.** §4.3.1 gobierna **la migración**, y dice que la renumeración de una familia del conjunto normativo **la hace el framework y no el destino** |
| `Catalogo-De-Criterios` | 1.6 | **1.13** | minor | **No.** Índice: no define criterios |
| `Rules-Base-Conocimiento` | no listado | **2.0** | — | **No.** No hay orquestador que lea la regla, y las dos unidades tienen `usa_llm == false` |
| `PRODUCT-INTAKE-template` | 3.4 | 3.4 | — | Sin cambio |
| `PRODUCT-MANIFEST-template` | 6.0 | 6.0 | — | Sin cambio |

### 6.1 Las tres superficies medidas sobre el árbol, y no supuestas

| Superficie | De qué versión | Medida | Qué obliga |
|---|---|---|---|
| **Estimación como ítem propio**, `Rules-Backlog-Tecnico` §4.4 punto 5.b | **11.0** | **144 archivos `US-*.md`** —114 de `GeometriaFactory-Api` y 30 de `GeometriaFactory-Web`—, todos con la sección **«## 5. Prioridad y estimación»** empaquetada. Su texto vivo dice **«Estimación: sin fijar»** remitiendo a `Product-Backlog.md` §4.1 | Partir la sección en **5** y **5.b**, y **diferir la estimación con la forma de §12.2** o fijarla. Sin la forma, **hallazgo P1** por el bloque de impacto de la 11.0 |
| **Los cuatro ítems `.b` de `Rules-Devops`** | **11.0** | Existen los seis documentos alcanzados: `Estrategia-Versionado.md`, `Entornos-Deploy.md` y `Supply-Chain-Seguridad.md` de **las dos** unidades de entrega. `Estrategia-Versionado.md` de `-Api` **ya tiene su §3.b**, el prefijo de etiqueta, de la migración anterior | Cada uno de los cuatro pasa a ítem propio. **P1** si quedan diferidos sin la forma de §12.2 |
| **La cita del rol con la forma nueva**, `Root-Rules` §4.4 Tabla A | **12.0** | **`SDD/Docs/README.md`, cinco filas** del mapa de documentación: `AG-00`, `AG-01`, `AG-05`, `AG-09`, `AG-11`, `AG-02 a AG-11` y `AG-ROOT` en el pie de la 1.0 | Reemplazar por el mapeo de catorce entradas —`AG-00` → `AG-00000` … `AG-ROOT` → `AG-00990`—. **Una sustitución mecánica en un solo archivo**, y el archivo es **uno por producto** |

**Y una superficie que se mide para acotarla, no para migrarla.** La forma vieja `AG-NN` aparece
**550 veces en 375 archivos vivos** —cabeceras de autor, notas de rol, matrices de deriva—. **El
destino no las renumera**: `Migracion-Rules.md` §4.3.1 declara que una migración de destino **no
renumera la familia** y acota su trabajo a **reemplazar la cita en su mapa de documentación**. Se
declara acá para que el plan de migración no confunda el tamaño del patrón con el tamaño del trabajo,
y para que nadie lea el número como si fueran 550 correcciones obligatorias.

**Alcance documental del salto: tres reglas major y una obligación de una celda.** Es la primera vez
desde la 9.12 → 10.0 que el salto alcanza artefactos, y la primera desde que este destino existe que
alcanza **el backlog entero**.

---

## 7. Recomendación, y su fundamento

```text
RECOMENDACIÓN — B · Migrar a la vigente, y por qué
  Continuidad del origen: COMPROMETIDA — dos major con impacto no vacío entre 10.0 y 13.3
  Alcance real del salto: 3 reglas major de 24 artefactos; 144 US, 4 ítems .b y 1 celda
  Volumen alcanzado:      494 documentos vivos
  Estado del repositorio: limpio, main al día, sin entregas vivas (T0 EN ORDEN)
  Divergencias abiertas:  3 — D-02 por diseño, D-03' residual, D-04 reparada en §9
  Costo de no hacerlo hoy:cada fila que el frente A escribe de vuelta nace con la forma vieja
  Alternativa razonable:  D · continuar la construcción (fase `i`)
```

### 7.1 Los seis factores, desarrollados

- **Continuidad del origen.** Entre la procedencia y la vigente hay **tres major** —11.0, 12.0 y
  13.0— y **dos de ellas traen bloque «Impacto sobre destinos existentes» no vacío**. La 13.0 lo
  declara vacío con estas palabras: *«Ninguno. Ningún destino tiene trabajo»*.
- **Alcance real del salto.** Las tres superficies de §6.1, medidas sobre el árbol.
- **Volumen alcanzado.** 494 documentos vivos, de los cuales el salto toca 144 historias de usuario,
  seis documentos de la 09 y el README raíz.
- **Estado del repositorio.** `EN ORDEN` después de T5.
- **Divergencias abiertas.** Tres, ninguna bloqueante para `B`.
- **Costo de no hacerlo hoy.** **El frente `A` del plan de cierre toca exactamente los documentos que
  el salto alcanza**: los dos `Product-Backlog.md` y los dos `Pipeline-CI-CD.md` son cuatro de los
  seis que llevan la tabla de ítems diferidos. Cerrar las 33 filas vencidas antes de migrar significa
  escribirlas dos veces.
- **Alternativa razonable: `D`.** Ganaría si el despliegue fuera urgente: `PT-05` valida `RN-B1`
  —«sin acceso el laboratorio no existe», impacto **Alto**— y es la única medición del producto que
  puede obligar a mover la topología. **La fase `i` no toca ningún documento alcanzado por el salto**,
  de modo que las dos no compiten.

### 7.2 El umbral de continuidad, y por qué `C` no se ofrece como equivalente

`Master-Prompt-Reanudacion.md` §4.0.1 fija el umbral de forma mecánica: **cuántos major con bloque de
impacto no vacío atraviesa el salto**. Acá son **dos** —la 11.0 y la 12.0—, y la tabla de esa sección
dice qué significa con estas palabras: *«ninguna regla vigente puede auditar ni extender ese corpus:
el destino quedó fuera del alcance del método que dice usar»*.

**Por eso `C` no aparece en §8 como equivalente de `B`.** Sigue siendo elegible, y su consecuencia
está escrita: cada documento nuevo —y el frente `A` va a escribir decenas— **nace con la forma que dos
major ya cambiaron**, y agranda la migración futura en lugar de acercarla.

---

## 8. Decisión

| Campo | Valor |
|---|---|
| **Salida elegida** | **A · Reparar primero** |
| **Quién** | El Product Owner, el 2026-08-23 |
| **Sobre qué** | Las cinco salidas presentadas en R2, con la recomendación **B** y su alternativa **D** |
| **Alcance acordado** | **`D-04` únicamente.** `D-02` no la repara `A`; `D-03'` queda declarada y abierta por decisión del Product Owner |
| **Qué sigue** | **Recalcular y volver a preguntar.** Reparada `D-04`, se vuelve a R0 y la pregunta pendiente sigue siendo **migrar o seguir en la versión declarada** |

**La recomendación fue `B` y su fundamento está en §6 y §7.** No se eligió: el Product Owner eligió
`A`, que es la única salida que las otras cuatro dan por hecha.

---

## 9. Reparación de la salida A

**Rama:** `arreglo/roadmap-f1-diez-casos`

| Divergencia | Qué se hizo |
|---|---|
| **`D-04`** | `Roadmap-Producto.md` **1.8 → 1.9**: el criterio `F-1` de la transición `f` → `g` pasa de «los **nueve** casos de prueba obligatorios» a «los **diez**». El estado anterior queda archivado en `00-Contexto/_legacy/2026-08-23/Roadmap-Producto-v1.8.md` |

**Por qué la repara el Product Owner y no un guion.** `f` es una etapa **ya cerrada y demostrada**, y
corregir su criterio de transición desde una puerta sería reescribir contra qué se cerró. La decisión
se tomó el 2026-08-23, sobre la salida `A` de este informe.

**El recuento no se eligió: se leyó de la fuente.** `PRODUCT-INTAKE` **1.20** §17.1.P.8 escribe «las
**diez** pruebas del validador pasan», su §21 tiene **diez** filas —la décima incorporada con `E-8`
bajo el rótulo `[DECISIÓN 2026-08-09]`—, y `Criterios-Validacion.md` de `GeometriaFactory-Api` lo
declara en `CV-26` y en sus dos notas, con la constancia de que **«cerrar la etapa con nueve casos no
es una excepción admitida»**. **El contraste observable coincide**:
`tests/GeometriaFactory.Integration.Tests/FigureValidatorBatteryTests.cs` declara en su cabecera «la
batería obligatoria del producto: **diez casos**».

**Qué no cambia esta reparación.** **Nada de lo construido.** La etapa `f` se cerró corriendo la
batería entera, que ya eran diez; lo que estaba mal era el recuento **escrito en el roadmap**,
heredado de la redacción anterior a la 1.20 del intake. No se tocó ningún guion de puerta, ninguna
prueba y ningún documento de la 08.

**Lo que la reparación no toca**: `D-02` y `D-03'`, por lo declarado en §5 y §8.

---

## 10. Punto de continuación

### 10.1 La segunda vuelta, que es lo primero que sigue

**Fusionada esta reparación, se vuelve a R0 sobre el árbol reparado** y la recomendación **se
recalcula**. `Master-Prompt-Reanudacion.md` §4.0.2 exige decirlo con estas palabras, para que quien
eligió `A` sepa que está en la segunda vuelta y no lea la pregunta como nueva:

> Reparada `D-04`, lo que sigue decidiendo es **migrar a la vigente o seguir en la versión
> declarada**. La recomendación recalculada será **`B`**, por el umbral de §7.2 — dos major con
> impacto, que la reparación de `D-04` no mueve.

### 10.2 Si la salida de la segunda vuelta es `B`

`Master-Prompt-Migracion.md`, **con el diff normativo de §6 ya hecho**: su fase M1 lo verifica en
lugar de construirlo. Las tres superficies de §6.1 son las filas del plan.

### 10.3 Si la salida es `D` — la construcción, que no tiene prompt

**El alcance comprometido está cerrado.** Las ocho etapas `a` a `h` están construidas y demostradas y
las ocho tienen puerta verificable por guion.

| Campo | Valor |
|---|---|
| **Etapa siguiente** | **Fase `i` · Despliegue real** |
| **Capacidad** | **F-14, sola** |
| **Puerta técnica** | **`PT-05`** — medir el acceso desde la red de la facultad |
| **Qué valida** | **`RN-B1`**, impacto **Alto**: «sin acceso el laboratorio no existe» |
| **Entregable** | Front publicado por FTP en el hosting, servicio de datos en el servidor propio |
| **Puerta de entrada** | `Roadmap-Producto.md` §5.2, transición `h` → `i`, **ya satisfecha** |
| **Puerta de salida** | `scripts/verify-stage-i.sh`, **siete criterios**; `Medicion-PT-05.md` en **`SIN MEDIR`**, y el guion comprueba que ese estado **ya no diga `SIN MEDIR`** |
| **Documentos que la gobiernan** | `Roadmap-Producto.md` **1.9** §2.1 y §5.2 · `Pipeline-CI-CD.md` §2.1 (QG-01, QG-02) · `Entornos-Deploy.md` §3 · `ADR-14003` 1.1 · `Estrategia-Versionado.md` |
| **Qué la bloquea** | **Sólo el Product Owner**: secretos del hosting, acceso al host y una persona en la red de la facultad |

**El otro frente disponible es `A` del plan de cierre**: las 33 filas vencidas y las 11 sin evento,
con las cinco decisiones abiertas de `A3` —`D1`, `D2`, `D3`, `D6` y `D7`—. **`D2`, la unidad de
estimación, es literalmente lo que la 11.0 convierte en ítem propio en 144 historias**: decidirla
dentro de la migración cierra las dos cosas de una vez.

### 10.4 Lo que queda abierto y es del Product Owner

- **`D-03'`, la excepción de las etiquetas.** Escribir en `Estrategia-Versionado.md` la excepción de
  `c`, `d` y `f` con su motivo —que el `changelog.md` ya declara— o retirar el absoluto de §4.1.
- **`D-02`, la procedencia en 10.0.** La resuelve la segunda vuelta.
- **La segunda ronda de `M6`**, con auditor **independiente**. `Plan-Cierre-De-Pendientes.md` §5
  declara que **caduca**: cuanto más se construya encima, menos dice, y el frente `A` la vuelve
  obsoleta si corre antes.
- **El reporte `12` del framework**, que no es de este destino y no bloquea nada de él.

---

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-23 | Emisión inicial. **Cuarta reanudación** del destino, con `Master-Prompt-Reanudacion.md` **1.9**. **§2 registra que T0 detuvo la corrida** por una entrega viva —el PR #78— y publica las dos compuertas, antes y después de T5. **La dimensión 5 coincide por primera vez**, y §3 declara por qué: desde el PR #66 no hubo trabajo de código. **§4 cuenta los ítems diferidos sobre el árbol** —116 filas, **33 vencidas**, **11 sin evento**, 5 vigentes, 67 cerradas— y el recuento coincide con el declarado. **§6 mide el diff normativo 10.0 → 13.3 artefacto por artefacto**: tres reglas **major** —`Root-Rules` 7.0 → 8.4, `Rules-Backlog-Tecnico` 4.4 → 5.1 y `Rules-Devops` 5.0 → 6.1— y **tres superficies medidas**: **144 archivos `US-*.md`** que empaquetan prioridad y estimación, los **cuatro ítems `.b`** de la 09, y **cinco filas** del mapa de documentación con la forma vieja de `AG`. Declara además que las **550 ocurrencias en 375 archivos** de `AG-NN` **no las renumera el destino**, por `Migracion-Rules.md` §4.3.1. **§7.2 aplica el umbral de continuidad**: **dos major con impacto**, y por eso **`C` no se ofrece como equivalente**. Recomendación **`B`** con alternativa **`D`**; el Product Owner eligió **`A`**, acotada a **`D-04`**, y **volver a preguntar** en la segunda vuelta. §9 registra la reparación de `D-04` y §10 el punto de continuación. | Orquestador de reanudación SDD |
