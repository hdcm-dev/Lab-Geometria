# Informe de migración normativa — SDD 13.7 → 13.16

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-13.7-a-13.16.md
**Versión:** 1.0
**Fecha:** 2026-09-13
**Instrumento:** `Master-Prompt-Migracion.md` **2.10**, fase **M6**; auditor independiente invocado desde cero, dos rondas
**Plan auditado:** [`Plan-Migracion-13.7-a-13.16.md`](Plan-Migracion-13.7-a-13.16.md) 1.2 · **Mesa:** [`Mesa-2026-09-13.md`](Mesa-2026-09-13.md) 1.1
**Base de la corrida:** `b9675d8` · **Rama:** `migracion/a-13.16` (sin push, sin pull request)
**Expediente:** [`SDD/Expedientes/0001-Migracion-Normativa-A-13.16/`](../../Expedientes/0001-Migracion-Normativa-A-13.16/README.md), actuación 009

---

## 0. Veredicto

**Ronda 1: APROBADO CON OBSERVACIONES** — 0 P0, 4 P1, 5 P2, 4 P3. Los P1 frenaban la aplicación de las propuestas y el cierre.
**Ronda 2: APROBADO CON OBSERVACIONES** — 0 P0, 1 P1 nuevo (`M6-14`), 3 P2, 2 P3, sobre las correcciones de §3. **Ronda 3, acotada a `M6-14` a `M6-17`: cerrados** (§4).

## 1. Qué se migró

**Nada del corpus se escribió.** Es una **migración parcial declarada** (`Migracion-Rules.md` §4.6), por el dictamen D-1 de la mesa: sin la aprobación explícita del Product Owner no se escribe el intake (`Master-Prompt.md` §13 caso (b)), y por la cadena D6 tampoco el manifiesto ni `SDD/Docs/`. Todo lo que la migración haría está hecho como **propuesta con texto exacto**, aplicable con un guion que exige la actuación de aprobación y aborta sin ella:

| Propuesta | Artefacto | Versión | Verificación |
|---|---|---|---|
| `P-01` | Intake | 4.6 → 5.0 | E-016: aplica limpia, marcador completado por el guion, 0 residuos vivos |
| `P-02` | Manifiesto | 6.1 → 7.0 | E-016; validaciones nuevas de §4 en «Cumple» |
| `P-03`, `P-04` | `Vista-Producto.md` 1.11, `Pipeline-Producto.md` 1.9 | minor | E-016: 0 enlaces rotos |
| `P-06`, `P-06b` | 8 documentos de 05/06/09 y 8 `CU-0800N` | minor | E-006g, E-015, E-020 |
| `P-08` | `ADR-14001` a `14004` | minor | Contadores 4/4/3/1, campo 7 |

**Lo que sí se escribió en la rama**: el expediente, el plan, el registro de mesa y este informe en `SDD/Docs/Audit/`, y —ajeno a la migración— el OK de la fase `k` en `Roadmap-Producto.md` 1.14, `Mini-Plan.md` 3.5 y `changelog.md`.

## 2. Los siete P0 de `Master-Prompt-Migracion.md` §10

| P0 | Resultado |
|---|---|
| Contenido inventado | **No**, en lo escrito ni en las propuestas. La fila 5.0 del intake propuesto **cita una actuación de aprobación que todavía no existe**; el guion la exige y completa el número, y aborta sin ella (`M6-04`, cerrado) |
| Sección exigida rellenada con inferencia | No. La batería del intake está vacía porque toda sección nueva tiene fuente (plan PM-01) |
| Procedencia reescrita con migración parcial | **No**: §1.1 sigue en 13.7 |
| Corrección manual pisada | No |
| Estado previo sin archivar | No: los snapshots de `7864428` y `9`, y el guion los toma antes de aplicar (E-016: 24 de 24) |
| Fila del plan sin resolver y sin declarar | No: las doce filas tienen estado (§5) |
| Parche aplicado aguas abajo de su defecto | No: la marca del insumo nace en el intake y se re-deriva |

## 3. Hallazgos de la ronda 1

| Id | Nivel | Hallazgo | Corrección |
|---|---|---|---|
| `M6-01` | P1 | Ocho ítems diferidos verticales (`CU-08001` a `08008`) fuera del inventario y del plan | `P-06b`; plan 1.2 PM-06 con universo 126; Mesa 1.1 |
| `M6-02` | P1 | La clasificación §4.8 no corría su paso 2 | E-015 v2 y v3: paso 2 sobre las 20 filas abiertas; 15 del ciclo, 2 elevadas al lote (puntos D y E) |
| `M6-03` | P1 | El lote al Product Owner no existía en el árbol | Actuación 010 |
| `M6-04` | P1 | El guion podía escribir una aprobación inexistente y aplicaba M3 sin su confirmación | Guion por fases con guarda de actuación de aprobación, probado abortando (E-016) |
| `M6-05` | P2 | Registros de cambios desordenados en documentos tocados, previos a la corrida | `DD-7` |
| `M6-06` | P2 | El registro de mesa se contradecía con el expediente sobre los informes íntegros; motivos de descarte no versionados | Mesa 1.1 §2 y §4; E-017 (jurado íntegro con fundamento por voto), E-018, E-019, E-021 |
| `M6-07` | P2 | 32 filas derivadas elegidas por linaje sin declararlo | E-020; plan 1.2 |
| `M6-08` | P2 | La ocurrencia 9 remitida a un informe inexistente | `DD-6` |
| `M6-09` | P2 | §16.1 propuesto sin fuente para dos columnas | Plan 1.2 PM-01 y fila 5.0 de `P-01` |
| `M6-10` | P3 | README §3 sin el hash del commit | README |
| `M6-11` | P3 | `Roadmap-Producto.md` 1.13: fecha, ítem (2) omitido, afirmación por encima del testimonio | 1.14 |
| `M6-12` | P3 | Cierre de mesa «26 filas» contra 27 | Mesa 1.1 |
| `M6-13` | P3 | E-006h declaraba «unidad de entrega» y P-06 escribe el proyecto | E-006h y E-006g: unidad de trabajo = proyecto en curso (anterior a la 8.0) |

## 4. Ronda 2

**Veredicto: APROBADO CON OBSERVACIONES** — 0 P0, 1 P1 nuevo, 3 P2, 2 P3. De los trece de la ronda 1, once cerrados y dos parciales (`M6-01`, `M6-02`), verificados sobre un worktree descartable con las siete propuestas aplicadas: los ocho `CU` con campo 5, 24 snapshots, versión de cabecera coherente en 24 de 24, guion abortando sin actuación. **La actuación 008 es conforme** a `Master-Prompt.md` §13 y `Migracion-Rules.md` §4.1: rechazar como aprobación una delegación anterior sobre otro objeto es la única salida que no fabrica una decisión. 28 de 28 SHA-256 coinciden.

| Id | Nivel | Hallazgo | Corrección (ronda 3) |
|---|---|---|---|
| `M6-14` | P1 | E-015 clasificaba por conteo de palabras y una fila (`PD-04` de Api :597) descansaba en un falso positivo: las dos «ocurrencias» de `datos` eran «meta**datos**» | E-015 v3 ancla el tema a inicio de palabra y declara que es una aproximación al paso 2 literal: 15 del ciclo, 0 de norma posterior, 3 sin ciclo, **2 elevadas**; punto E del lote (actuación 010); plan 1.3, mesa 1.2 |
| `M6-15` | P2 | Un noveno ítem vertical fuera del universo: `ADR-14005` :185, y el comando de PM-06 acotado a la carpeta que confirmaba su hipótesis | Plan 1.3: comando ampliado a `SDD/Docs`, con las 12 coincidencias clasificadas; el ítem de `ADR-14005` es registro de un ADR **retirado** el 2026-08-29, superado por el tramo `R-4` |
| `M6-16` | P2 | La guarda de aprobación era textual y aceptaba «no aprobado»; la fecha `2026-09-13` estaba congelada en el guion y en los diffs | Guion v3: exige una forma afirmativa y rechaza negaciones; `--fecha` (por omisión, hoy) reescribe sólo las filas de control de cambios, las marcas `[CORREGIDO …]` y las carpetas `_legacy/<fecha>/` de las líneas agregadas. Probado con `2026-09-20`: 24 filas nuevas con la fecha dada, 0 con la vieja, 24 snapshots en `_legacy/2026-09-20/` (E-016) |
| `M6-17` | P3 | El abort «sin actuación» era del `ls` bajo `set -e`, sin mensaje | Guion v3: aborta con su mensaje (E-016, caso 1) |
| `M6-18` | P3 | Un informe único con dos rondas, contra `Master-Prompt.md` §10.1, por el path único de `Master-Prompt-Migracion.md` §10 | Declarado acá y en `Mesa-2026-09-13.md` §8 como observación al framework |

**Ronda 3.** Acotada a los cinco hallazgos de arriba, corrida por el orquestador con los comandos de E-016 (v3) y E-015 (v3) y **no por un auditor independiente**: se declara así. Los cuatro con corrección quedaron cerrados por evidencia reproducible; `M6-18` es del framework.

## 5. Estado final de cada fila del plan (1.2)

| Fila | Estado |
|---|---|
| PM-01 | Propuesta `P-01`, no escrita, verificada; espera el punto B del lote |
| PM-02 | Propuesta `P-02`, no escrita; su confirmación propia es la fase `manifiesto` del guion |
| PM-03, PM-04 | Propuestas, no escritas, verificadas |
| PM-05 | No tocar: `Rules-Examples.md` §3.6 se cumple, probado fallando (E-014) |
| PM-06 | Propuestas `P-06` y `P-06b`, no escritas; 126 huecos con tratamiento §4.9; 2 elevados al lote |
| PM-07 | No tocar: `PD-VER-01` a `03` decididas (E-013) |
| PM-08 | Propuesta `P-08`: no contemplado × 4, contadores 4/4/3/1 |
| PM-09 | Vivas en las propuestas; ocurrencia 9 en `DD-6`; registro sin tocar |
| PM-10 | Procedencia sin tocar, correcto |
| PM-11 | Evento no ocurrido (esta M4 no escribe); declarado |
| PM-12 | No tocar |

## 6. Contenido sin destino

**Ninguno.** Verificado sobre `P-02`: la nota histórica de §2 sobre la cita «2.0» del intake se retira del cuerpo y su fila 7.0 lo declara, remitiendo a la fila 5.1 donde vive. Las cinco filas de §16.1 del intake se transponen con su texto.

## 7. Candidatos a regla del framework (`Migracion-Rules.md` §4.7)

`ADR-14001` (4 saltos), `ADR-14002` (4) y `ADR-14003` (3), con el criterio de conteo de `Plan-Migracion-10.0-a-13.3.md` §5.1.1. Y las cuatro observaciones al framework de `Mesa-2026-09-13.md` §8.

## 8. Declaración

**Migración parcial**, legítima por `Migracion-Rules.md` §4.6: la procedencia declara 13.7, que sigue siendo cierto; el estado por fase está en EXP-0001 actuación 005, y lo que falta para completarla es un solo acto del Product Owner (actuación 010, puntos A y B) seguido de tres invocaciones del guion, un audit de cierre y M5.

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-09-13 | Emisión con las dos rondas del auditor independiente y la tercera, acotada, del orquestador. | Auditor independiente de M6, transcripto por el Orquestador de migración normativa SDD |
