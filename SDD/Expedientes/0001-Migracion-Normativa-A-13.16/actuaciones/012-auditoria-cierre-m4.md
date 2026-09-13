# EXP-0001 · Actuación 012 — Auditoría de cierre de M4, sobre lo escrito

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `auditoria`
**Fecha:** 2026-09-13
**Autor:** Auditor independiente de cierre, invocado desde cero (`Master-Prompt.md` §10) con el encargo de refutar; asentado por el Orquestador de migración normativa SDD
**Foliada:** 012

---

## 1. Lo que se audita: la aplicación de M2 a M4

Con la aprobación de la actuación [011](011-testimonio-aprobacion-plan-e-intake.md) (puntos A y B del lote),
el orquestador corrió `propuestas/aplicar-propuestas.sh --aprobacion 011` en sus tres fases —`intake`,
`manifiesto`, `docs`— con `--fecha 2026-09-13`, en un solo commit, **`c14bf3b`**: intake **5.0**, manifiesto
**7.0**, `Vista-Producto.md` **1.11**, `Pipeline-Producto.md` **1.9**, ciclo de origen en 118 filas de ocho
documentos y campo 5 en los ocho `CU-0800N`, `ADR-14001` a `14004` revisados; **24 documentos**, cada uno con
su snapshot en el `_legacy/2026-09-13/` de su carpeta; `verify-solution-tree.sh` conforme. La rama es
`migracion/a-13.16-aplicacion`, sobre `main` = `1d4afc4` (fusión de PR #205, que publicó la migración parcial
declarada); sin push, sin pull request, sin merge, sin etiqueta.

**Compuerta mecánica previa** ([E-022](../evidencia/E-022-verificacion-de-lo-aplicado.txt)): la aplicación se
reprodujo sobre un worktree descartable desde `255b55c` y los hashes de árbol de `SDD/Intake` y `SDD/Docs`
coinciden con `HEAD`; los siete diffs aplican en reversa; 24 de 24 snapshots idénticos al blob previo con el
nombre del precedente; versión de cabecera coherente con el máximo del registro en 24 de 24; 0 marcadores del
guion (la única ocurrencia de `{{` es una cita de plantilla en un registro del 2026-08-17); 984 enlaces
relativos, 0 rotos; el manifiesto 7.0 declara «Intake (origen) 5.0», plantilla 6.1 y las once validaciones de
§4 en «Cumple». La compuerta declaró como recortes los recuentos anclados, los identificadores, los ítems
diferidos con evento ya ocurrido y el ciclo de origen, que quedaron para el auditor.

**Dos apartamientos de proceso, propios de la corrida** (`M6-29`; origen del hecho calculado, `Master-Prompt.md`
§8.1): las tres fases fueron a un solo commit, contra el «commit por fase» del punto de continuación de la
actuación 010 —el orden D6 lo preserva el guion y E-022 (1) lo reproduce—; y M4 no se cortó por unidad de
entrega con un audit por corte (`Master-Prompt-Migracion.md` §8), sino con este único audit de cierre, que es lo
que la actuación [005](005-providencia-tratamiento-de-las-aprobaciones.md) había dejado planificado.

**Una constancia** (`M6-28`): el lote de la actuación 010 preguntó por «el plan de migración 1.2» y la
aprobación de la 011 asienta el 1.3, que era el vigente en el árbol desde `0f26c2b`; la diferencia son `M6-14`
y `M6-15`, cuyos efectos (puntos D y E) sí estaban en el lote que el Product Owner leyó. Sin efecto material;
010 y 011 no se reescriben.

## 2. El informe

Íntegro, transcripto sin edición, en [E-024](../evidencia/E-024-audit-de-cierre-integro.md). Resumido en
[`SDD/Docs/Audit/Informe-Migracion-13.7-a-13.16.md`](../../../Docs/Audit/Informe-Migracion-13.7-a-13.16.md) 1.1 §9,
que es donde la norma lo ubica.

**Veredicto: APROBADO CON OBSERVACIONES** — 0 P0, 2 P1, 3 P2, 8 P3 (`M6-19` a `M6-31`). Los siete P0 de
`Master-Prompt-Migracion.md` §10: ninguno, con cita. Detectables por guion: 6 de 13.

| Id | Nivel | Qué encontró | Qué se hizo en la corrida |
|---|---|---|---|
| `M6-19` | P1 | El evento de cierre de la deuda de cabeceras de `Mesa-2026-08-27.md` §8 **ocurrió** con `c14bf3b`; plan, mesa e informe decían que no. 116 cabeceras siguen citando versión, 11 en documentos que esta M4 abrió | Cierre parcial declarado (veredicto 21 de la mesa): 2 de 118. `DD-8` en `Mesa-2026-09-13.md` 1.3 §8 con evento reasignado; plan 1.4 PM-11. Las 116 van al Product Owner como punto F del lote de cierre (actuación 013); no se escriben sin su aprobación |
| `M6-20` | P1 | El manifiesto 7.0 decía en su línea 3 que la plantilla 6.1 «declara la procedencia de §1.1» (la tabla decía 6.0) y conservaba en §1.1 la constancia «cita 3.0 / intake 3.1» contra «Intake (origen) 5.0» | Resuelto por M5: §1.1 reescrito entero en el 7.1 |
| `M6-21` | P2 | Plan, README del expediente e informe seguían diciendo «sin aprobar» y «no escritos»; la aplicación sin actuación foliada; E-022 sin indexar | Esta actuación (§1), plan 1.4, informe 1.1, README del expediente |
| `M6-22` | P2 | Los ocho ítems de los `CU`, con ciclo `aa3abd3` y abiertos, sin clasificación §4.8 | [E-015b](../evidencia/E-015b-clasificacion-4.8-v4.txt): hueco del ciclo los ocho; 28 abiertas: 23/0/3/2 |
| `M6-23` | P2 | Nueve ítems abiertos que M4 tocó nombran un momento y no un artefacto con sección; no se puede decidir si el evento ocurrió (no concluyente) | `DD-9`, para los titulares de la 05 y de la 06 |
| `M6-24` | P3 | Doce documentos subieron de versión sin mover la fecha de cabecera | Anotado en `DD-7` |
| `M6-25` | P3 | El nivel de los tres samples de `Contracts` sale del `README.md` del sample (no hay `ejemplo-*.md`, `DD-3`) | Declarado en el plan 1.4 PM-01 |
| `M6-26` | P3 | Residuos del modelo por proyecto de código en `Pipeline-Producto.md` :70 y `Vista-Producto.md` :11 | Anotado en `DD-6` |
| `M6-27` | P3 | La conducta «sin modo y sin Node falla con `MSB3073`» sin evidencia versionada | [E-023](../evidencia/E-023-build-sin-modo-y-sin-node.txt): corrida real, `MSB3073`, código 127 |
| `M6-28` | P3 | Lote «1.2», aprobación «1.3» | §1 de esta actuación; plan 1.4 |
| `M6-29` | P3 | Un commit para tres fases; M4 sin cortes | §1 de esta actuación |
| `M6-30` | P3 | E-022 (5) llamaba «última fila» al máximo | E-022 corregido antes de indexarse |
| `M6-31` | P3 | Fase `k` cerrada sobre la `i`, que el destino declara abierta (ajeno a la migración, no concluyente) | Se asienta para la próxima reanudación |

**Dictamen del auditor sobre M5:** puede escribir la procedencia sin P0 con `M6-19` corregido antes y la
detención de `Master-Prompt-Migracion.md` §9 presentada; las deudas `DD-1` a `DD-9` no son de la cadena.

**Ronda acotada del orquestador**, no de un auditor independiente, sobre las correcciones: E-015b, E-022 y
E-023 por evidencia reproducible; plan 1.4, mesa 1.3, informe 1.1 y manifiesto 7.1 por lectura. Se declara
así, con la observación `M6-18` al framework ya asentada.
