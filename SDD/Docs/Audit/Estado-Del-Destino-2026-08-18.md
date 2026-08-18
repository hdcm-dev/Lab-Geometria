# Estado del destino — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Estado-Del-Destino-2026-08-18.md
**Versión:** 1.0
**Fecha:** 2026-08-18
**Autor:** Orquestador de reanudación SDD
**Nivel:** Producto
**Instrumento:** `Master-Prompt-Reanudacion.md` **1.7**, fases R0 a R4
**Supera a:** `Estado-Del-Destino-2026-08-17.md` 1.0

---

## 1. Qué es este documento

**Es la tercera reanudación de este destino.** Reconstruye en qué estado está leyendo el árbol, sin
memoria de ninguna sesión anterior, declara sus divergencias, registra la decisión del Product Owner
y deja el punto de continuación para que el trabajo siguiente **no vuelva a deducir lo que acá está
deducido**.

**Es la única escritura de la reanudación sobre este destino**, salvo lo que la salida elegida
ejecute con su propia confirmación — acá, la reparación de la salida **A**, registrada en §7.

**No es un audit.** Declara estado, no veredicto: no aprueba ni rechaza nada y no tiene niveles de
hallazgo.

---

## 2. Las seis dimensiones

| # | Dimensión | Fuente declarativa | Quién la mantiene | Lectura | Contraste observable | Resultado |
|---|---|---|---|---|---|---|
| 1 | ¿Hay documentación generada? | — | — | — | `SDD/Docs/` con **889 archivos** | **Sí** |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST` **3.2** §1.1 | La generación y la migración | **SDD 9.12** | `CHANGELOG` del framework: **9.19** | **Diverge — `D-02`, por diseño** |
| 3 | ¿La migración terminó? | `Informe-Migracion-9.10-a-9.12.md` 1.0 — **APROBADO** | La migración | Completa, 0 P0/P1/P2, 1 P3 cerrado | **0 carpetas `_fusion/`** | **Coincide** |
| 4 | ¿Qué quedó abierto? | `Cierre-De-Hallazgos-Abiertos-2026-08-17.md` 1.0 | Quien cierra cada hallazgo | **Cero hallazgos abiertos** que este destino pueda cerrar | Enlaces: **4700 de 4700**, 0 rotos (medido en `2e04013`) | **Coincide** |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` | El **equipo de desarrollo**, en la rama de la etapa; verifica el **Product Owner** en el PR | Última unidad: «Las puertas que faltaban — `d`, `e` y `f`» | Historial: **dos merges posteriores**, PR #61 y PR #62 | **DIVERGE — `D-01`** |
| 5' | ídem, segunda fuente | `Estrategia-Versionado.md` §2.4 y §11 | Categoría 09 | «Cada etapa cerrada y fusionada recibe una **etiqueta**» | `git tag` → **cero** | **DIVERGE — `D-03`** |
| 6 | ¿Qué falta para la siguiente? | `Roadmap-Producto.md` **1.8** §2.1 y §5.2 | Quien cierra cada etapa | Fase **`i` · Despliegue real**, F-14 sola, `PT-05` | — | **Punto de continuación en §6** |

**La dimensión 5 tiene dos fuentes declarativas y las dos se degradaron**, que es exactamente lo que
`Master-Prompt-Reanudacion.md` §1.1 R3 anticipa: ninguna de las dos es subproducto del acto.

---

## 3. Divergencias

| # | Dim. | Lectura declarativa | Lectura observable | Evidencia | Estado |
|---|---|---|---|---|---|
| **`D-01`** | 5 | La última unidad registrada en `changelog.md` es «Las puertas que faltaban — `d`, `e` y `f`», rama `codigo/puertas-d-e-f` | Después se fusionó `codigo/prerrequisitos-fase-i`, que **tocó código e infraestructura** y no dejó entrada | `4cc596b` modifica `.github/workflows/deploy-front-ftp.yml`, `deploy/compose.yaml` y agrega `ADR-14003`; `git log -- changelog.md` se detiene en `2e04013` | **REPARADA** — §7 |
| **`D-02`** | 2 | Procedencia **9.12** | Framework vigente **9.19** | §4, diff artefacto por artefacto | **Abierta, por diseño** |
| **`D-03`** | 5' | «Cada etapa cerrada y fusionada recibe una etiqueta» | **Cero etiquetas** en todo el árbol | `git tag` sin salida | **Abierta** — declarada el 2026-08-17 y no cerrada |
| **`D-04`** | 6 | `Roadmap-Producto` `F-1`: «los **nueve** casos de prueba obligatorios» | Intake **1.20** §17.1.P.8 y `CV-26`: «los **diez** casos de la batería» | `changelog.md`, §«Una discrepancia declarada y no resuelta» | **Abierta** — decisión del Product Owner |

**`D-01` es la reincidencia de la `D-01` del 2026-08-16**, en su forma menor: **una unidad** de
diferencia en lugar de tres etapas. Que sea menor es el resultado de contrastar seguido, no de que la
regla se haya cumplido. La regla ya nombra a su responsable desde la reparación del 2026-08-17 — el
defecto que quedaba no era de sujeto sino de cumplimiento.

**`D-03` y `D-04` no las repara ninguna salida de este prompt**: la primera es crear las etiquetas
retroactivas o retirar el objetivo de la categoría 09, y la segunda es un criterio de transición de
una etapa **ya cerrada y demostrada**, que cambiarlo desde una puerta sería reescribir contra qué se
cerró. Las dos son del Product Owner.

---

## 4. Diff normativo 9.12 → 9.19

**Medido contra los archivos vivos del repositorio del framework, no deducido del `CHANGELOG`.**

| Artefacto del framework | Procedencia 9.12 | Vivo en 9.19 | ¿Alcanza al árbol? |
|---|---|---|---|
| `Master-Prompt` | 8.4 | **8.7** | **No.** §8.1 la pregunta previa, §10 el encargo al auditor, §10.0 la compuerta mecánica. Gobierna cómo se genera y se audita, no la forma de ningún documento |
| `Master-Prompt-Migracion` | 2.7 | **2.8** | **No.** Cómo se migra |
| `Master-Prompt-Reanudacion` | 1.6 | **1.7** | **No.** Este mismo prompt |
| `Migracion-Rules` | 3.9 | **3.15** | **No.** §4.3.2, reglas de emisión E5 a E9: gobiernan una migración **en vuelo**, y no hay ninguna (0 carpetas `_fusion/`, informe con veredicto) |
| `Catalogo-De-Criterios` | 1.1 | **1.5** | **No.** Índice: no define criterios y no gobierna ningún artefacto de este destino |
| `SDD-Development-Guide` | no listado | **1.19** | **No.** Guía del framework |
| `Root-Rules` | 6.2 | **6.2** | Sin cambio |
| `Rules-Contexto` | 4.4 | **4.4** | Sin cambio |
| `Rules-Necesidades-Negocio` | 4.3 | **4.3** | Sin cambio |
| `Rules-Especificacion-Funcional` | 5.4 | **5.4** | Sin cambio |
| `Rules-UX-UI-DX` | 5.4 | **5.4** | Sin cambio |
| `Rules-Arquitectura-Tecnica` | 4.4 | **4.4** | Sin cambio |
| `Rules-Backlog-Tecnico` | 4.4 | **4.4** | Sin cambio |
| `Rules-Plan-Sprint` | 5.4 | **5.4** | Sin cambio |
| `Rules-Calidad-Y-Pruebas` | 4.5 | **4.5** | Sin cambio |
| `Rules-Devops` | 4.6 | **4.6** | Sin cambio |
| `Rules-Examples` | 6.4 | **6.4** | Sin cambio |
| `Rules-Documentacion` | 5.4 | **5.4** | Sin cambio |
| `Intake-Rules` | 4.1 | **4.1** | Sin cambio |
| `Vocabulario-Rules` | 3.1 | **3.1** | Sin cambio |
| `Maqueta-Rules` | 4.3 | **4.3** | Sin cambio |
| `Deriva-Rules` | 5.3 | **5.3** | Sin cambio |
| `PRODUCT-INTAKE-template` | 3.4 | **3.4** | Sin cambio |
| `PRODUCT-MANIFEST-template` | 6.0 | **6.0** | Sin cambio |

**Alcance documental del salto: cero.** **Las once reglas de categoría, `Root-Rules`, los dos
templates y tres de las cuatro reglas transversales no se movieron ni una versión.** Los seis
artefactos que sí se movieron son **instrumentos de proceso**: cómo se genera, cómo se migra, cómo se
audita y cómo se reanuda.

**Umbral de continuidad (`Master-Prompt-Reanudacion.md` §4.0.1): ninguno.** Entre 9.12 y 9.19 no hay
un solo salto **major** — las siete son minor, y ninguna entrada del `CHANGELOG` del framework en ese
tramo lleva bloque «Impacto sobre destinos existentes», que §VI.4 exige sólo en las major. Con cero
major con impacto, **la salida C es correcta y barata**, y `B` sería migrar por el número.

**Sería el tercer salto consecutivo de alcance documental cero** — 9.9 → 9.10, 9.10 → 9.12 y éste.

**Una observación sobre el repositorio fuente, que no afecta a este destino.** `Migracion-Rules` vive
en **3.15** y la entrada 9.19 del `CHANGELOG` declara 3.14. Es materia del framework, que este prompt
lee en solo lectura; se registra para que la próxima reanudación no lo redescubra.

---

## 5. Decisión

| Campo | Valor |
|---|---|
| **Salida elegida** | **A · Reparar primero** |
| **Quién** | El Product Owner, el 2026-08-18 |
| **Sobre qué** | Las cuatro salidas presentadas en R2, con la recomendación **A** y su alternativa **D** |
| **Alcance acordado** | **`D-01` únicamente.** `D-02` es por diseño; `D-03` y `D-04` no las repara ninguna salida de este prompt |

**La recomendación fue A y su fundamento está en §3 y §4**: el salto no alcanza a ningún artefacto
—de modo que `B` sería trabajo sin resultado— y `A` es la única salida que las otras tres dan por
hecha. Elegir `D` con `D-01` abierta habría construido la fase `i` sobre un registro que ya estaba
una unidad atrás, y al terminarla habrían sido dos.

**La alternativa razonable declarada fue `D`**, que habría ganado si se resolvía cerrar `D-01` dentro
de la entrada de la fase `i` en lugar de en una unidad propia.

---

## 6. Punto de continuación

### 6.1 Lo que sigue, y no tiene prompt

**El alcance comprometido está cerrado.** Las ocho etapas `a` a `h` están construidas y demostradas,
con el **OK explícito del Product Owner de las ocho fases** del 2026-08-18, y las ocho tienen puerta
verificable por guion.

**Sigue la fase `i` · Despliegue real**, planificada en `Roadmap-Producto.md` **1.8** §2.1 el
2026-08-18:

| Campo | Valor |
|---|---|
| **Capacidad** | **F-14, sola** |
| **Puerta técnica** | **`PT-05`** — medir el acceso desde la red de la facultad |
| **Qué valida** | **`RN-B1`** — «los alumnos no pueden alcanzar la aplicación desde la red de la facultad», impacto **Alto**: «sin acceso el laboratorio no existe» |
| **Entregable** | Front publicado por FTP en el hosting, servicio de datos en el servidor propio |
| **Puerta de entrada** | `Roadmap-Producto.md` §5.2, transición `h` → `i`, ya satisfecha |
| **Puerta de salida** | `Roadmap-Producto.md` §5.2, transición `i` → `j…`, **siete criterios**, incluido que el resultado de `PT-05` se documente **sea cual sea**: si el acceso no funciona, el número se registra igual y la topología se revisa |
| **Documentos que la gobiernan** | `Roadmap-Producto.md` 1.8 §2.1 y §5.2 · `Pipeline-CI-CD.md` §2.1 (QG-01, QG-02) · `Entornos-Deploy.md` §3 · `ADR-14003` 1.1 · `Estrategia-Versionado.md` |

**Sus prerrequisitos ya están fusionados**: PR #61 dejó las puertas bloqueantes corriendo antes de
publicar, la composición de verificación diciendo qué es, y `ADR-14003` emitido; PR #62 lo aprobó.

**`j…` · Agregados** —F-15, F-16 y F-17, más los candidatos de X-6 y X-7— se planifica de a una
cuando `i` cierre, y el roadmap declara explícitamente que **ninguna de las tres está especificada
todavía**.

### 6.2 Lo que queda abierto y es del Product Owner

- **`D-03`, las etiquetas.** Cero en todo el árbol contra una estrategia de versionado que las
  declara instrumento de reversión. **La fase `i` es despliegue real**, que es donde una etiqueta
  empieza a hacer falta para poder volver atrás. Cerrarlo es crear las etiquetas retroactivas o
  retirar el objetivo de la categoría 09.
- **`D-04`, nueve contra diez.** Criterio `F-1` del roadmap contra el intake 1.20 §17.1.P.8 y
  `CV-26`. El guion corre la batería entera y no elige un número.
- **`D-02`, la procedencia en 9.12.** Con el diff de §4 escrito, actualizarla sin migrar es la salida
  `C` del próximo prompt de reanudación o de generación, y **es barata**: la verificación artefacto
  por artefacto ya está hecha acá.

---

## 7. Reparación de la salida A

**Rama:** `arreglo/registro-prerrequisitos-fase-i`

| Divergencia | Qué se hizo |
|---|---|
| **`D-01`** | Se repuso en `changelog.md` la entrada de la unidad `codigo/prerrequisitos-fase-i` (PR #61), **marcada como repuesta después de la fusión** en lugar de presentarse como escrita a tiempo |

**Cómo se escribió, y qué no se hizo.** Cada afirmación de la entrada sale del mensaje de la
confirmación `4cc596b` o de un archivo del árbol — el flujo `deploy-front-ftp.yml` con sus dos pasos
de puerta, la cabecera de `deploy/compose.yaml`, y `ADR-14003` con su versión y su estado. **Nada se
infirió de la memoria de la sesión que construyó esa unidad**, que no existe en ésta. **No se
reescribió ningún commit.**

**Por qué no hay entrada para el PR #62.** `docs/adr-14003-aceptado` cambia el estado de un ADR de
`Propuesto` a `Aceptado` y no toca código: el registro de cambios declara el avance de la
**construcción**, y las ramas `docs/` no tienen entrada propia en ninguna de las nueve secciones
anteriores. La aprobación queda registrada **dentro** de la entrada de la unidad que emitió el ADR,
que es donde el lector la busca.

**Lo que la reparación no toca**: `D-02`, `D-03` y `D-04`, por lo declarado en §3 y §5.

---

## 8. Segunda pasada de R0, sobre el árbol reparado

**La salida A vuelve a R0**, y esta pasada es barata porque el informe ya está.

| Dimensión | Antes | Después |
|---|---|---|
| 5 · `changelog.md` contra el historial | **DIVERGE**: última unidad registrada `codigo/puertas-d-e-f`, dos merges posteriores | **COINCIDE**: la última unidad registrada es `codigo/prerrequisitos-fase-i`, y el único merge posterior es `docs/adr-14003-aceptado`, que por §7 no lleva entrada propia |
| 5' · etiquetas | **DIVERGE** | **DIVERGE** — sin cambio, fuera del alcance acordado |
| 2 · procedencia | **DIVERGE** por diseño | Sin cambio |
| 6 · roadmap | `F-1` con recuento anterior | Sin cambio |

**Reparadas las divergencias del alcance acordado, lo que sigue decidiendo es continuar la
construcción o declarar el desfase de versión.** La **recomendación recalculada es `D`**: la fase `i`
tiene su planificación, su puerta de entrada satisfecha y sus prerrequisitos fusionados, y el desfase
a 9.19 **no alcanza a ningún artefacto** — de modo que `C` no habilita nada que `D` necesite, y puede
tomarse cuando convenga con el diff de §4 ya escrito.

**La reanudación termina acá por alcance.** Entrar en la fase `i` es una decisión nueva del Product
Owner sobre un estado que este informe deja reconstruido.

---

## 9. Criterios de aceptación

- [x] Las **seis dimensiones** están resueltas, cada una con su fuente citada (§2).
- [x] Las **tres dimensiones con contraste observable** se contrastaron, y el resultado está
      declarado aunque coincidan (§2, dimensiones 3, 4 y 5).
- [x] Toda divergencia está declarada con **las dos lecturas y la evidencia de cada una** (§3).
- [x] El informe existe y declara la salida elegida, **con su bloque de punto de continuación
      completo** (§5 y §6).
- [x] El **diff normativo** está escrito artefacto por artefacto, y queda disponible para que la
      próxima salida `B` o `C` lo consuma sin rehacerlo (§4).
- [x] La salida la eligió el humano, sobre las **cinco** presentadas — `E` declarada no aplicable con
      su evidencia.
- [x] **No se escribió nada del destino fuera de este informe**, salvo la reparación de la salida
      `A`, registrada en §7 con su propia confirmación.
- [x] La reparación ejecutada y **R0 corrido de nuevo** sobre el árbol reparado (§8).

---

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-18 | Emisión inicial. **Tercera reanudación** de este destino, con `Master-Prompt-Reanudacion.md` **1.7**. Supera a `Estado-Del-Destino-2026-08-17.md`. Compuerta de arranque **EN ORDEN** — árbol limpio, `main` al día, sin entregas vivas ni ramas por borrar. Seis dimensiones resueltas; **cero hallazgos abiertos** y **cero carpetas `_fusion/`**. **Cuatro divergencias**: `D-01`, el registro de cambios una unidad atrás del historial —**reincidencia** de la `D-01` del 2026-08-16, en su forma menor—; `D-02`, la procedencia en 9.12 contra el framework en **9.19**; `D-03`, las etiquetas por etapa, **cero en todo el árbol**, abierta desde el 2026-08-17; y `D-04`, el recuento de casos del criterio `F-1`. **Diff normativo 9.12 → 9.19 artefacto por artefacto**: seis artefactos movidos, **todos instrumentos de proceso**, y **las once reglas de categoría, `Root-Rules`, los dos templates y tres de las cuatro transversales sin cambio** — alcance documental **cero**, el tercero consecutivo. Umbral de continuidad: **cero major**, de modo que `C` es correcta y barata y `B` sería migrar por el número. Salida elegida por el Product Owner: **A, reparar primero**, **ejecutada en la misma sesión** (§7), con `D-01` repuesta desde los commits y el árbol y marcada como repuesta. **Segunda pasada de R0** en §8: la dimensión 5 coincide; la recomendación recalculada es **`D`, entrar en la fase `i`**. | Orquestador de reanudación SDD |
