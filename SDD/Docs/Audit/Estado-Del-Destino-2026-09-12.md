# Estado del destino — Fábrica de Geometría · 2026-09-12 (séptima reanudación)

**Documento:** `Estado-Del-Destino-2026-09-12.md`
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-09-12
**Autor:** Orquestador de reanudación SDD (`Master-Prompt-Reanudacion.md` **1.13**)
**Revisión leída (base de la corrida):** `5c95dab` de `main`, árbol de trabajo limpio
**Trazabilidad upstream:** el árbol del destino y `IA.SDD` **13.14** en solo lectura
**Trazabilidad downstream:** [`Mesa-2026-09-12.md`](Mesa-2026-09-12.md); rama `reanudacion/7-2026-09-12`

---

## 1. Qué es este documento, y por qué es distinto del que lleva la misma fecha

**Es la séptima reanudación de este destino, y es una prueba de aplicación.** La sexta reanudación
—también fechada 2026-09-12— corrió sobre el **mismo commit base** (`5c95dab`) bajo SDD **13.10**, y
generó en cuatro ciclos de mesa la materia prima de los reportes `25` a `28`, hoy aplicados como
framework **13.11 a 13.14**. Sus dos informes están archivados en la rama `archivo/reanudacion-6-2026-09-12`
del destino y **no en `main`**, a propósito: esta corrida no los hereda como estado vigente, sólo los
lee como antecedente (`git show archivo/reanudacion-6-2026-09-12:SDD/Docs/Audit/Estado-Del-Destino-2026-09-12.md`
y `…/Mesa-2026-09-12.md`).

**Lo que esta corrida agrega, y es el objeto de la prueba**: correr el mismo reconocimiento sobre el
mismo árbol, con el framework corregido, y declarar punto por punto qué cambia. La respuesta corta está
en §7; el detalle, en [`Mesa-2026-09-12.md`](Mesa-2026-09-12.md) §11.

---

## 2. Paso 0 de R0 — la compuerta de arranque (`Master-Prompt.md` §12.1 T0)

```text
COMPUERTA DE ARRANQUE — hdcm-dev/Lab-Geometria
  Rama:            main   al día (0 detrás, 0 adelante)
  Árbol:           limpio (0 cambios, 0 borrados, 0 sin seguir)
  Base:            5c95dab5074717dcd72cd6322312e07f1f1a0691
  Entregas vivas:  ninguna (0 ramas remotas sin fusionar)
  Ramas a borrar:  ninguna
  Veredicto:       EN ORDEN, se puede empezar
```

**Idéntica a la de la sexta reanudación**, salvo la línea `Base`, que esa corrida no publicaba —el
formato de T0 no la llevaba bajo SDD 13.10— y que esta versión del framework exige desde 13.11
(`Master-Prompt.md` §8.1, §12.1). **Es la primera diferencia observable entre las dos corridas**, y no
es cosmética: todo lo que sigue en este informe usa `5c95dab` para calcular el origen de cada hecho.

Sobre esta base se abrió la rama de trabajo `reanudacion/7-2026-09-12` (local, sin push).

---

## 3. Las seis dimensiones (`Master-Prompt-Reanudacion.md` §1)

| # | Dimensión | Fuente declarativa | Lectura | Contraste observable | Resultado |
| --- | --- | --- | --- | --- | --- |
| 1 | ¿Hay documentación generada? | — | — | `SDD/Docs` con **506** documentos vivos fuera de `_legacy` | **Sí** |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST` §1.1 | SDD **13.7**, actualizada sin migrar el 2026-08-27 (quinta reanudación, salida `C`) | `IA.SDD/CHANGELOG.md`: vigente **13.14** | **Desfasado en siete minor** (13.8 a 13.14), ninguno con impacto sobre el destino. Ver §6 |
| 3 | ¿La migración terminó? | `Informe-Migracion-10.0-a-13.3.md` | **APROBADO CON HALLAZGOS**, 2026-08-25 | **0** carpetas `_fusion/` | **Sí**, coincide |
| 4 | ¿Qué quedó abierto? | `Reporte-Hallazgos-De-Los-Samples-2026-08-30.md` §0 | 14 emitidos, 12 cerrados, 2 retirados, cero vivos | Sin enlaces rotos nuevos sobre el corpus reparado (§5 de `Mesa-2026-09-12.md`) | **Coincide** |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` | Última entrada de esta corrida repone PR #186; entrada previa del 2026-09-12 documenta PR #187 | `git log`: PR #186 (2026-09-06) y #187 (2026-09-12) ambos ya reflejados tras la reparación | **Coinciden**, después de la reparación de esta corrida. **Antes de repararla, divergían** — ver DIV-02 |
| 6 | ¿Qué falta para la siguiente? | `Roadmap-Producto.md` **1.10** §2.1 y §5.2 (tras la reparación) | Fase `i`, entregable reescrito con la topología realizada | La topología real coincide con lo declarado tras la reparación; `PT-05` sigue `SIN MEDIR` | **Coincide** en el entregable; **sigue pendiente** en `PT-05` (no es divergencia: es lo que la fase `i` mide) |

**Antes de reparar, las dimensiones 5 y 6 divergían exactamente como en la sexta reanudación** —mismo
commit base, mismo corpus—: DIV-01 a DIV-04 de §5 son releídas de esa corrida y confirmadas contra el
árbol, no redescubiertas. Lo que cambia es **qué se hizo con ellas**: ver §7.

---

## 4. Ítems diferidos (`Root-Rules.md` §12.2)

**No se rebarrieron las 118 filas fila por fila en esta corrida**: el recuento de la sexta reanudación
(87 cerrados, 25 vigentes, 6 `NO APLICA`) sigue siendo la lectura vigente, porque **el hecho que las
gobierna no cambió**: la fase `i` sigue abierta, así que el evento «cierre de la fase `i`» que
veintiuna de las vigentes nombran sigue sin ocurrir. Repetir el barrido completo habría sido releer un
hecho que esta corrida no tocó.

| Estado | Cantidad | Qué significa |
| --- | --- | --- |
| Cerrados | **87** | Sin cambios respecto de la sexta reanudación |
| Vigentes | **25** | 21 atadas al evento «cierre de la fase `i`», que sigue sin ocurrir; el resto con evento propio |
| `NO APLICA` | **6** | Con la figura de `ADR-14004` |
| **Total** | **118** | |

**Ninguno de los 118 lleva el campo `ciclo de origen`** (`Root-Rules.md` §12.2 punto 5, `Master-Prompt.md`
§8.2): todos se declararon antes de SDD 8.7, y `Migracion-Rules.md` §4.8/§4.9 declara que eso **no es
hallazgo por sí solo** — se deriva o se marca `no derivable — anterior al mecanismo` recién en una
migración real sobre este destino, que esta corrida no es.

**Los cuatro ítems de deuda que esta corrida agrega** (ADR de topología a nivel Producto, retiro formal
del canal de FTP, `PT-05`, el barrido de las 21 filas) **sí llevan el campo**, declarado en
[`Mesa-2026-09-12.md`](Mesa-2026-09-12.md) §8: `mesa · producto · base 5c95dab` — es la primera vez que
este destino declara un hueco con el mecanismo de 13.13.

---

## 5. Divergencias

Releídas de la sexta reanudación contra el mismo commit base, y declarado qué hizo esta corrida con
cada una.

### DIV-01 · El entregable de la etapa `i` cambió y el corpus no lo registraba

| | |
| --- | --- |
| **Lectura declarativa (antes de reparar)** | `Roadmap-Producto.md` 1.9 §2.1/§5.2 e intake `F-14`: «front publicado por FTP en el hosting y servicio de datos en el servidor propio» |
| **Lectura observable** | Desde el 2026-09-06 las dos piezas corren en contenedores del servidor propio de i7infra, publicadas por túnel con dominio propio (`geometria.aplicada.stream`); el hosting externo se conserva como alternativa, no como canal vigente |
| **Origen del hecho** | **Ajeno a la corrida**: calculado contra la base `5c95dab` — el despliegue del 2026-09-06 y la reemisión del intake 4.0/4.1 del 2026-09-12 están en la base, sin cambios de esta corrida hasta que se reparó |
| **Qué hizo esta corrida** | **Reparada.** `Master-Prompt.md` §13.1 (13.14) dice exactamente qué es esta clase de hecho —el Product Owner editando su propio documento fuera de una corrida— y qué evento dispara la reapertura del roadmap (`Rules-Backlog-Tecnico.md` §3.6, `Rules-Contexto.md` §3.5). Se aplicó sin escalar: intake 4.2, roadmap 1.10 (commits `4e6ed8d`, `b902343`) |

### DIV-02 · El registro de cambios no tenía el PR #186

| | |
| --- | --- |
| **Lectura declarativa** | `changelog.md`, sin entrada para `89f3ab3` (2026-09-06) |
| **Lectura observable** | `git show --stat 89f3ab3`: 118 líneas, `deploy/Dockerfile.web` |
| **Origen del hecho** | **Ajeno a la corrida**: la omisión es del 2026-09-06, previa a esta corrida |
| **Qué hizo esta corrida** | **Reparada.** Entrada repuesta, marcada como repuesta (commit `6b16127`). Es la **cuarta** vez que este documento llega tarde a una fusión propia |

### DIV-03 · El índice del corpus afirmaba un estado que ningún instrumento sostenía

| | |
| --- | --- |
| **Lectura declarativa** | `SDD/Docs/README.md` §7: «etapa `e`», «séptima migración en curso», cabecera con manifiesto 4.0/intake 3.0 |
| **Lectura observable** | §7.2 del mismo documento ya decía `a`-`h` Cerrada; migración cerrada desde 2026-08-25; manifiesto 6.0, intake ya en 4.1 antes de esta corrida |
| **Origen del hecho** | **Ajeno a la corrida** |
| **Qué hizo esta corrida** | **Reparada.** §7 deja de enunciar estado y remite a §7.2 y al informe de migración; cabecera corregida (commit `b1966a0`) |

### DIV-04 · Dos canales de publicación vivos, y el corpus sólo describía el retirado

| | |
| --- | --- |
| **Lectura declarativa** | Roadmap, `ADR-14003`, `Pipeline-Producto.md` §3, `Guia-Publicacion-Front-Ftp.md` describían el hosting como canal vigente |
| **Lectura observable** | `.github/workflows/deploy-front-ftp.yml` seguía disparándose con cada fusión que tocara el front, el visor o los contratos, y publicó al hosting el 2026-09-12 (mismo `main`, dos topologías, un día) |
| **Origen del hecho** | **Ajeno a la corrida**: el disparador y su última ejecución son del 2026-09-12 anterior a esta corrida |
| **Qué hizo esta corrida** | **Reparada en parte.** El intake y el roadmap ya declaran el canal como alternativa (DIV-01); el disparador automático del workflow se retiró (commit `b180b76`, pasa a `workflow_dispatch`). **Deuda declarada**: los tres pasos formales de retiro de `DESPLIEGUE.md` (fuera del corpus, es documento externo del despliegue real) no se ejecutan desde esta corrida — no le corresponde tocarlos |

### DIV-05 · Procedencia 13.7 contra vigente 13.14

**Por diseño, y no es defecto.** Ver §6.

---

## 6. Diff normativo 13.7 → 13.14, artefacto por artefacto

| Salto | Artefacto | 13.7 | 13.14 | ¿Alcanza al destino? |
| --- | --- | --- | --- | --- |
| 13.8 | `Mesa-Rules`, `Master-Prompt-Reanudacion`, `Catalogo-De-Criterios` | 1.0/1.10/1.14 | 1.1/1.11/1.15 | No. Rigen hacia adelante, sin invalidar registros previos |
| 13.9 | Alta de conocimiento anexo (PR manual) | — | 1.0/1.1 | No. El intake no cita ningún alias |
| 13.10 | Alta de conocimiento anexo (plantillas HTML) | — | 1.0/1.2 | No. Ídem, y la fase que lo usaría ya cerró |
| **13.11** | `Master-Prompt` §8.1/§12.1/§7.0/§9, `Mesa-Rules` §7/§7.1/§8, `Master-Prompt-Reanudacion` §6, `Master-Prompt-Migracion` M4, `Catalogo-De-Criterios` | 8.14/1.1/1.11/2.9/1.15 | 8.15/1.2/1.12/2.10/1.16 | **Sí, de proceso.** Origen del hecho: cambia **cómo** este mismo informe se escribe (línea `Base` de T0, cálculo de origen en cada divergencia), no ninguna decisión ya tomada del destino |
| **13.12** | `Vocabulario-Rules`, `Master-Prompt` §10.0/§10/§15, `Mesa-Rules` §6.1/§8, `SDD-Development-Guide`, `SDD-User-Guide`, `Catalogo-De-Criterios` | 3.2/8.15/1.2/1.29/1.20/1.16 | 3.3/8.16/1.3/1.30/1.21/1.17 | No. El destino no tiene afirmaciones de colisión léxica sin comando adjuntas (no se relevó exhaustivo; ninguna se encontró en los documentos tocados por esta corrida) |
| **13.13** | `Root-Rules` §11/§12.1/§12.2, `Master-Prompt` §8.2/§10.0/§15, `Migracion-Rules` §4.8/§4.9/§6 | 8.6/8.16/3.19 | 8.7/8.17/3.20 | **Sí, de proceso.** Todo hueco nuevo que esta corrida declare lleva `ciclo de origen` (§4); los 118 existentes no son hallazgo por carecer de él |
| **13.14** | `Master-Prompt` §13.1/§15, `Rules-Backlog-Tecnico` §3.6, `Rules-Contexto` §3.5, `Master-Prompt-Reanudacion` §4 | 8.17/5.1/4.5/1.12 | 8.18/5.2/4.6/1.13 | **Sí, y es el salto decisivo de esta corrida.** Resuelve mecánicamente DIV-01: dónde se asienta una decisión de producto posterior al handoff y qué evento reabre backlog/roadmap. Sin esta versión, DIV-01 habría necesitado escalarse otra vez como en la sexta reanudación |

**Ningún salto es major, y ninguno declara «Impacto sobre destinos existentes» vacío salvo 13.11 y
13.13** (que sí lo declaran, y ambos «de proceso», sin invalidar artefactos ya emitidos). **El umbral
de continuidad de `Master-Prompt-Reanudacion.md` §4.0.1 no se cruza**: cero major con impacto entre
13.7 y 13.14. La salida `C` (seguir en la versión declarada) sigue siendo barata y correcta en cuanto a
procedencia — lo que cambia no es si migrar, es **qué preguntas ya no hace falta que este informe le
haga al humano**, que es exactamente §7.

---

## 7. Qué cambió respecto de la sexta reanudación, punto por punto

Esta es la respuesta directa a la pregunta que motiva esta corrida.

1. **La línea `Base` de T0 ahora se publica** (13.11). La sexta reanudación corrió T0 sin ella; esta
   corrida la usa para calcular el origen de cada divergencia y cada escalada (§5, §9 de la mesa).

2. **DIV-01 ya no necesitó un reporte al framework para resolverse.** En la sexta reanudación, esta
   misma divergencia obligó a un ciclo 2 de mesa con tres agentes ad hoc (agilidad, gestión de
   proyecto, normativa del ciclo) para concluir que el framework no tenía dónde registrar una decisión
   de producto posterior al handoff, y ese hallazgo salió como reporte `25`. En esta corrida,
   `Master-Prompt.md` §13.1 y `Rules-Backlog-Tecnico.md` §3.6 **ya contestan la pregunta**: la mesa la
   aplicó directamente (§4 de `Mesa-2026-09-12.md`, hallazgo `RS-REQ-01`/`RS-P-01`/`RS-P-02`) sin
   convocar panel nuevo ni producir reporte. **La consulta que ya no salió**: «¿dónde se registra que
   el Product Owner cambió el alcance después del handoff?». **Por qué**: tiene respuesta literal en
   `Master-Prompt.md` §13.1, y la pregunta previa de `Master-Prompt.md` §8.1 la resuelve como trabajo
   propio, no como detención.

3. **Un hueco nuevo salió con ciclo de origen declarado desde que se escribió** (13.13,
   `Master-Prompt.md` §8.2): los cuatro ítems de deuda de `Mesa-2026-09-12.md` §8 (ADR de topología,
   retiro formal del FTP, `PT-05`, barrido de las 21 filas) llevan `mesa · producto · base 5c95dab`.
   En la sexta reanudación, los cinco ítems de deuda que su mesa declaró (§8 de su registro,
   archivado) **no llevaban ese campo** porque el mecanismo no existía todavía.

4. **Una decisión de producto se recibió por el mecanismo de §13.1, no por un parche de mesa sobre un
   artefacto del framework destino.** La sexta reanudación diseñó `P-15` —tocar
   `A3-Decisiones-Del-Product-Owner.md`, un instrumento del framework, para que «el destino no dependa
   de un repositorio ajeno para su propia auditoría»—. Esta corrida corrigió ese rumbo (`RS-REF-01` de
   la mesa): `A3-…md` es el instrumento del paso A3 de `Plan-Cierre-De-Pendientes.md`, con alcance
   acotado a ocho decisiones específicas de 2026-08-20, y forzar dos decisiones nuevas ahí habría sido
   reparar en la capa equivocada. `Master-Prompt.md` §13.1 nombra el lugar correcto: el intake mismo,
   que «es la declaración vigente del alcance». **Qué la disparó**: la comprobación de capa de origen
   de `Mesa-Rules.md` §3 (P3), aplicada contra el propio antecedente en lugar de copiarlo.

5. **DIV-04 se cerró más cerca de la causa.** La sexta reanudación proponía (`P-16`) declarar el canal
   de FTP como alternativa **en la documentación de la 09**. Esta corrida, con el origen del hecho
   calculado (ajeno a la corrida, pero con causa identificable: el disparador del workflow), tocó el
   disparador mismo y no sólo el texto — es la diferencia entre declarar una decisión y hacerla
   efectiva en el artefacto que la contradecía en los hechos.

6. **Lo que NO cambió**: `E-02` (API pública), `E-04` (evento de etiquetado) y `E-05` (estado durable
   del front) siguen exactamente donde estaban. El testimonio del Product Owner transcripto en la
   invocación de esta corrida contesta lo que en la sexta eran `E-01` y `E-03`, y no menciona estas
   tres. El mecanismo nuevo del framework no inventa una respuesta donde no la hay: **13.11-13.14
   resuelven cómo se procesa una consulta que sí tiene dueño, no eliminan las que genuinamente son del
   Product Owner**. Siguen en el lote de §9.

---

## 8. Recomendación, y su fundamento

```text
RECOMENDACIÓN — A · Reparar primero (ya ejecutada en esta corrida), y después D · continuar la construcción

  Continuidad del origen: sostenible — cero major con impacto entre 13.7 y 13.14
  Alcance real del salto: 0 artefactos del destino reescritos por el salto normativo en sí;
                          2 mecanismos de proceso (§13.1, §8.2) aplicados sobre 4 divergencias
                          preexistentes
  Volumen alcanzado:      506 documentos vivos
  Estado del repositorio: EN ORDEN (T0 limpio, sin entregas vivas, base 5c95dab)
  Divergencias abiertas:  0 reales tras la reparación de esta corrida (DIV-01 a DIV-04
                          reparadas; DIV-05 es por diseño); 12 hallazgos procedentes de la mesa,
                          6 con parche ya aplicado
  Costo de no hacerlo hoy: la doble publicación del 2026-09-12 se habría repetido en la
                          próxima fusión que tocara el front; el roadmap y el intake habrían
                          seguido describiendo un entregable retirado; el README raíz seguía
                          siendo falso contra su propia tabla
  Alternativa razonable:  D directo, sin reparar. Perdía por el mismo motivo que en la sexta
                          reanudación: la puerta de la fase i (verify-stage-i.sh) mide el canal
                          equivocado si no se repara primero qué es el entregable vigente

  DE LA MESA (§3.1, `Mesa-2026-09-12.md`)
  Hallazgos procedentes:  12 — con parche aplicado: 6; deuda declarada: 4; escalados: 3 (idénticos
                          a 3 de las 5 escaladas del ciclo 1 de la sexta reanudación)
  Parches listos:         6, en 6 capas, y los 6 YA APLICADOS en esta corrida
  Deuda declarada:        4, con ciclo de origen (13.13) — primera vez que este destino lo usa
  Escaladas al humano:    3, agrupadas, con origen del hecho (13.11) — ver §9
```

**`B` (migrar a la vigente) no se recomienda**: los siete saltos entre 13.7 y 13.14 son minor, y de
los cuatro con «Impacto sobre destinos existentes» no vacío, dos son de proceso puro (13.11, 13.13:
cambian cómo se escribe un informe o un hueco, no qué dice ya escrito) y dos son altas de conocimiento
anexo sin alias citado por este intake (13.9, 13.10). Migrar por el número sería trabajo sin resultado.

**`C` no alcanza sola**: antes de la reparación de esta corrida dejaba cuatro divergencias abiertas.
Después de repararlas, **`C` ya es la salida efectiva**: la procedencia sigue en 13.7, declarada y
verdadera (ningún salto la alcanza), y el desfase queda dicho en este mismo informe.

**`E` no aplica**: no hay migración en curso.

### 8.1 Las salidas, y qué implica cada una

| Salida | Qué continúa en esta sesión | En qué estado deja | ¿Vuelve a preguntar? | Qué **no** resuelve |
| --- | --- | --- | --- | --- |
| **A · Reparar primero** | **Ya ejecutada**: 6 parches de la mesa, commits `4e6ed8d`..`b180b76` | El corpus diciendo lo que el producto es hoy, y el estado vuelto a leer en §3 | No hace falta repetir R0: esta misma corrida ya lo hizo sobre el árbol reparado | Las tres escaladas de §9, que no son del método |
| **B · Migrar a la vigente** | No se invoca | — | — | — |
| **C · Seguir en la versión declarada** | `Master-Prompt.md`, con la decisión ya tomada (procedencia 13.7 se mantiene) | Procedencia 13.7 declarada y verdadera, desfase dicho en §6 | No, hasta que cambie la procedencia o la vigente | El desfase, que sigue existiendo; sólo queda dicho |
| **D · Continuar la construcción** | Nada que invocar: se sigue, con el punto de continuación de §10 | Corpus al día para lo que hace falta; código en la fase `i`, midiendo `PT-05` | No | Las tres escaladas de §9 |
| **E · Retomar migración a medias** | No aplica | — | — | No hay ninguna en vuelo |

**La salida elegida por esta corrida es A, ya ejecutada, seguida de C** (la procedencia declarada
13.7 se mantiene: el diff de §6 la sostiene) **y D como trabajo siguiente** (§10). No hay decisión de
alcance pendiente sobre estas dos: son mecánicas, dado el diff normativo sin major con impacto.

---

## 9. Escaladas pendientes — lote completo (`Master-Prompt.md` §8.1, formato §7.0)

**No hay humano disponible en esta corrida.** Las tres escaladas de abajo son continuación literal de
`E-02`, `E-04` y `E-05` del ciclo 1 de la sexta reanudación (archivado); el testimonio transcripto en
la invocación de esta corrida no las contesta. Están desarrolladas con el formato completo en
[`Mesa-2026-09-12.md`](Mesa-2026-09-12.md) §9; se resumen acá para el lote:

| # | Qué decidir | Origen del hecho | Disparador (`Mesa-Rules.md` §7) | Propuesta | Si no respondés |
| --- | --- | --- | --- | --- | --- |
| **E-02** | La API quedó públicamente alcanzable sin límite de tasa declarado; el intake dice lo contrario | Ajeno a la corrida (despliegue del 2026-09-06) | 5, consecuencia externa | B si los alumnos consumen la API directamente; A si no | Queda como divergencia abierta; no se repara la exposición |
| **E-04** | Qué evento dispara una etiqueta de versión para trabajo posterior a la etapa `h` (dos fusiones sin etiquetar) | Ajeno a la corrida | 1, ambigüedad de intención | Definir el evento; etiquetar retroactivo o no | Nada se etiqueta; deuda con evento «cierre de la fase `i`» |
| **E-05** | El estado durable del front no tiene requisito de continuidad de sesión ni respaldo declarado | Ajeno a la corrida | 4, irreversibilidad acotada | Declarar el requisito y decidir respaldo vs. riesgo aceptado | Requisito queda escrito como deuda; no se toca el procedimiento |

**Ninguna de las tres es de esta corrida**: las tres se calcularon contra la base `5c95dab` y todo el
hecho que las sostiene es anterior a ella. Por eso ninguna lleva «por qué la autocorrección no
alcanzaba» (`Master-Prompt.md` §8.1): esa exigencia es sólo para el origen «de la corrida».

---

## 10. Punto de continuación (salida D, sin roadmap propio para esto)

| Campo | Valor |
| --- | --- |
| **Etapa** | `i` · Despliegue real — **abierta desde el 2026-08-27**, entregable ya al día con el corpus tras esta corrida |
| **Qué ya ocurrió dentro de ella** | Despliegue real del 2026-09-06 (contenedores, túnel, dominio propio); reinstalación del 2026-09-12 (`5c95dab`); esta reparación documental del mismo día |
| **Qué falta, medido** | `PT-05` sin medir (requiere una persona en la red de la facultad); el circuito completo sobre el despliegue real, sin registrar; las ocho puertas anteriores repetidas sobre el árbol desplegado, sin registrar; el ADR de nivel Producto (deuda declarada, §8 de la mesa) |
| **Puerta de salida** | `scripts/verify-stage-i.sh` — no se corrió en esta corrida (fuera de alcance: exige el despliegue real, que es de sólo lectura para esta sesión) |
| **Qué la bloquea** | Nada del método; `PT-05` requiere presencia física en la facultad |
| **Documentos que la gobiernan** | `Roadmap-Producto.md` **1.10** §2.1/§5.2 · `Medicion-PT-05.md` **1.2** · `PRODUCT-INTAKE` **4.2** §14 |

---

## 11. Commits y rama

**Rama:** `reanudacion/7-2026-09-12` (local; sin push, sin PR, sin merge).

| Commit | Qué hizo |
| --- | --- |
| `4e6ed8d` | Intake: asienta la decisión del 2026-09-06 (F-14, X-10, control de cambios 4.2) |
| `b902343` | Roadmap: reabre fila `i` con el entregable realizado (versión 1.10) |
| `6b16127` | Changelog: repone la entrada del PR #186 |
| `b1966a0` | README raíz: corrige §7 contra su propia tabla y la cabecera |
| `e9bd238` | Medicion-PT-05.md: corrige cita de Roadmap (dato derivado) |
| `b180b76` | Workflow de FTP: retira el disparo automático, documenta la decisión |
| *(éste)* | Este informe y `Mesa-2026-09-12.md` |

---

## 12. Lo que no se pudo verificar

- **El ADR de nivel Producto que declare la topología vigente no se redactó**: exige detalle de
  infraestructura de `~/home/fernando/docker/lab-geometria` que esta corrida no está autorizada a
  inspeccionar (producción, sólo lectura). Queda como deuda declarada con ciclo de origen.
- **`PT-05` no se midió** y esta corrida no fabricó un número: requiere una persona físicamente en la
  red de la facultad.
- **`scripts/verify-stage-i.sh` no se corrió** contra el despliegue real, por el mismo motivo.
- **Las 21 filas de ítems diferidos atadas al evento de la fase `i` no se rebarrieron una por una**:
  el evento que las gobierna (cierre de la fase) no ocurrió, así que el barrido no habría cambiado su
  estado; se declara pendiente para cuando la fase cierre.
- **13.9 y 13.10 (altas de conocimiento anexo) no se verificaron exhaustivamente contra todo alias
  posible del intake**: se verificó que el intake no cita ninguno de los dos por nombre, no se agotó
  cada sección en busca de un alias implícito.
- **La afirmación de no-colisión léxica de 13.12 no se corrió con su comando exacto** sobre los seis
  documentos tocados por esta corrida; se declara por inspección, no por `grep` reproducido, y por eso
  no se afirma como verificación en el sentido de `Vocabulario-Rules.md` §9.4.

---

## 13. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial. Séptima reanudación, prueba de aplicación de SDD 13.11-13.14 sobre el mismo commit base (`5c95dab`) que la sexta. Compuerta de arranque en orden con línea `Base` publicada (13.11); seis dimensiones resueltas; cuatro divergencias reparadas en la misma corrida (DIV-01 a DIV-04) y una por diseño (DIV-05); mesa de un solo ciclo (contra los cuatro de la sexta) con 6 parches aplicados, 4 ítems de deuda con ciclo de origen (13.13, primera vez en este destino) y 3 escaladas con origen del hecho (13.11), continuación literal de `E-02`, `E-04` y `E-05` de la sexta reanudación. Recomienda `A` (ejecutada) seguida de `C` y `D`. |
