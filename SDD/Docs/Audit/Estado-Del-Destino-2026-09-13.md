# Estado del destino — Fábrica de Geometría, 2026-09-13

**Documento:** Estado-Del-Destino-2026-09-13.md
**Versión:** 1.1
**Fecha:** 2026-09-13
**Instrumento:** `Master-Prompt-Reanudacion.md` **1.14** (octava reanudación), framework SDD 13.18
**Base de la corrida:** `4fc2763bc8997259422adb3fb0714a38664c21eb`
**Registro de mesa:** [`Mesa-2026-09-13-ciclo-2.md`](Mesa-2026-09-13-ciclo-2.md)

---

## 1. Paso 0 — compuerta de arranque (`Master-Prompt.md` §12.1 T0)

Primera corrida de T0: **SE DETUVO** por árbol sucio (57 borrados sin commitear en `evidencia/` de la raíz). El Product Owner decidió commitear el borrado: rama `chore/retirar-evidencia-de-la-raiz`, PR #207, checks en verde, fusionado en `4fc2763`, rama borrada.

```text
COMPUERTA DE ARRANQUE — hdcm-dev/Lab-Geometria
  Rama:            main   al día
  Árbol:           limpio
  Base:            4fc2763bc8997259422adb3fb0714a38664c21eb
  Entregas vivas:  ninguna (archivo/reanudacion-6-2026-09-12 es archivo deliberado, declarado en el árbol)
  Ramas a borrar:  ninguna
  Veredicto:       EN ORDEN, se puede empezar
```

## 2. Las seis dimensiones

| # | Dimensión | Fuente | Lectura | Contraste | Resultado |
|---|---|---|---|---|---|
| 1 | Documentación generada | — | — | 554 documentos vivos en `SDD/Docs/` | **Sí** |
| 2 | Versión del framework | Manifiesto 7.1 §1.1 | SDD **13.16** | Vigente **13.18** | Desfasado en 2 minor, 0 major. Ver §6 |
| 3 | ¿La migración terminó? | `Informe-Migracion-13.7-a-13.16.md` 1.1 | APROBADO CON OBSERVACIONES, 0 P0 | 0 carpetas `_fusion/` | **Sí**, coinciden. No hay migración en curso |
| 4 | ¿Qué quedó abierto? | Informe 1.1 §9, `Mesa-2026-09-13.md` §8 | `M6-31`, `DD-1..DD-9`, lote F/G con defaults | 6456 enlaces, 0 rotos; 7 referencias en prosa a `evidencia/` retirada | **Diverge** (DIV-03) |
| 5 | Etapa de construcción | `changelog.md` | `k` cerrada (OK del PO, 2026-09-13) | `git log`: `fase-k/*` #190–#202, fix #204 | **Coinciden en construcción**; el changelog no registra #207 (C-04) |
| 6 | Qué falta para la siguiente | `Roadmap-Producto.md` 1.14 §5.2 | Fase `i`: seis criterios, `PT-05` | `Medicion-PT-05.md` `SIN MEDIR`; la puerta `verify-stage-i.sh` mide criterios de la 1.9 | **Diverge** (DIV-01, `DD-R8-1`) |

## 3. Pendientes declarados

- **Expedientes:** `0001-Migracion-Normativa-A-13.16` resuelto, forma histórica (13.18): se nombra, no cuenta.
- **Ítems diferidos (criterio escrito junto al número):** en las 118 filas de las tablas de 05/06/09 de Api y Web, las que tienen `Estado` que empieza en **Vigente** son **20**; **19** nombran la fase `i` como evento, que no ocurrió. **Tres** de esas 19 (Api/09 `PD-03`, Api/06 `PA-03`, `PA-04`) tienen su evento ya cumplido por `D-03` (RN8-REF-02) → **16** reales. Las cifras «23» (R0 de esta corrida) y «21» (`Estado-Del-Destino-2026-09-12.md`) no reproducen. Vencidos concluyentes: los tres de REF-02. No concluyentes: `DD-9`.
- **Deuda:** `DD-1..DD-9` (ningún evento ocurrió), `DD-8` ampliada a 129, `DD-R8-1..6` nuevas.
- **Lote F/G de EXP-0001 act. 013:** sin respuesta; rigen los defaults (F → `DD-8`; G → procedencia 13.16 confirmada).

## 4. Divergencias

| Id | Declarativa | Observable | Origen del hecho |
|---|---|---|---|
| DIV-01 | Roadmap §4 :118 «estrictamente secuenciales», :132 «`k` depende de `i`» | `k` cerrada con OK del PO con `i` abierta y `PT-05` sin medir | Ajeno (2026-09-12/13, en la base) |
| DIV-02 | `SDD/Docs/README.md` 2.8 §7.2: `i` «Planificada, no ejecutada», sin fila `k` | Despliegue real desde 2026-09-06; `k` cerrada | Ajeno |
| DIV-03 | 7 referencias a `evidencia/2026-09-1x-…/`; changelog sin #207 | La carpeta no existe desde `3662bf9`; el contenido vive en `3662bf9^` | **Ajeno** (la base es el merge de #207; R0 lo había declarado «de la corrida» y la mesa lo corrigió) |
| DIV-04 | 8 filas «el último que queda», 3 «no ocurrió» | El punto de control de `k` ocurrió después | Ajeno |
| DIV-05 | Procedencia 13.16 | Vigente 13.18 | Por diseño |

Además, la mesa encontró: la puerta de `i` no puede pasar por construcción (`DD-R8-1`); producción corre **`v1.1.2`** según `/salud` (2026-09-13 23:45 UTC) mientras el roadmap y el Mini-Plan dicen `v1.1.0`; `README.md` raíz y `SDD/README.md` afirman que nada empezó.

## 5. Resultado de la mesa

16 hallazgos consolidados, 16 procedentes (5-0), 9 parches con texto exacto (PR-01..PR-09), 6 deudas nuevas, 1 escalada (`E-1`). Bloque de cierre y detalle en [`Mesa-2026-09-13-ciclo-2.md`](Mesa-2026-09-13-ciclo-2.md) §9.

## 6. Diff normativo 13.16 → 13.18

| Versión | Qué cambió | ¿Alcanza al destino? |
|---|---|---|
| 13.17 | Alta de `Knowledge-Mesa-De-Expertos-A-Pedido.md`, `Index-Knowledge.md` 1.3 | No: catálogo sin alias citado por el intake |
| 13.18 | `Expediente-Rules.md` 1.0; `Master-Prompt.md` 8.20, `Master-Prompt-Migracion.md` 2.11, `Master-Prompt-Reanudacion.md` 1.14, `Mesa-Rules.md` 1.4, `Migracion-Rules.md` 3.21, `Root-Rules.md` 8.8, `Catalogo-De-Criterios.md` 1.19 | Sólo hacia adelante: EXP-0001 queda forma histórica; el próximo expediente numera `0002`; las respuestas del PO se asientan con su literal. **Ningún artefacto existente se reescribe** |

Umbral de continuidad (§4.0.1): **cero major** con impacto → la salida C es barata.

## 7. Recomendación

```text
RECOMENDACIÓN — A · Reparar primero (aplicar PR-01..PR-09), y después D · continuar la fase i

  Continuidad del origen: sostenible — 0 major entre 13.16 y 13.18
  Alcance real del salto: 0 artefactos reescritos; 13.18 sólo obliga hacia adelante
  Volumen alcanzado:      14 documentos por los parches (554 vivos)
  Estado del repositorio: EN ORDEN tras PR #207, base 4fc2763
  Divergencias abiertas:  4 reales (DIV-01..04) + 3 de la mesa; por eso A
  Costo de no hacerlo hoy: la fase i se trabajaría con un README que dice que no empezó, una puerta que
                          no puede pasar y un roadmap que se contradice; el repo es público de consulta
  Alternativa razonable:  D directo. Ganaría si lo único que querés hoy es medir PT-05 en la facultad:
                          la medición no depende de los parches

  DE LA MESA
  Hallazgos procedentes:  16 — P1: 2 · P2: 10 · P3: 4
  Parches listos:         9, en 14 documentos
  Deuda declarada:        6 nuevas + ampliación de DD-8
  Escaladas al humano:    1 (E-1), con default
```

B (migrar a 13.18) no se recomienda: sin major y sin artefacto alcanzado, sería trabajo sin resultado; C (actualizar la procedencia a 13.18 sin migrar) procede verificado por §6, y es mecánico. E no aplica.

## 8. Decisión

**Salida A, después D** — elegida por el Product Owner el 2026-09-13, y **`E-1` opción (2)**.

| Campo | Valor |
|---|---|
| Literal | «A, después D (Recommended)» · «(2) Reescribir §4 (Recommended)» |
| Canal | Claude Code (extensión de VS Code), selección en la consulta de R2 |
| Fecha y hora | 2026-09-13, zona -03:00, después de las 23:55 UTC del juez de Evidencia |
| Huella | Las dos respuestas, byte a byte, como las devolvió la herramienta de consulta |

**Qué continúa (R4):** se aplican PR-01 a PR-09 en la rama `reanudacion/8-2026-09-13-reparacion`, con §4 del roadmap **reescrito** (no apartamiento en prosa); se verifica cada parche con su comando; PR con checks en verde; T5; y se vuelve a R0 sobre el árbol reparado. Después, salida D: fase `i`.

**Reparadas las divergencias, lo que sigue decidiendo es migrar o seguir en la versión declarada.** La recomendación recalculada es **C**: 13.16 → 13.18 no tiene major ni artefacto alcanzado (§6); queda para la próxima corrida que escriba la procedencia (`DD-R8-5`).

## 9. Punto de continuación

| Campo | Valor |
|---|---|
| Etapa | `i` · Despliegue real — abierta; despliegue realizado el 2026-09-06 |
| Qué falta | Los seis criterios de `Roadmap-Producto.md` §5.2 fila `i` → `j…`; `PT-05` desde la red de la facultad con un alumno; el circuito completo sobre producción |
| Puerta | `scripts/verify-stage-i.sh` — **no certifica** hasta cerrar `DD-R8-1` |
| Qué la bloquea | `PT-05` exige presencia física; `DD-R8-1` exige una `BT` de código |
| Documentos | `Roadmap-Producto.md` §2.1, §2.2, §5.2 · `Medicion-PT-05.md` · intake §15 |

## 10. Lo que no se pudo verificar

- El informe de cierre de la fase `k` que el intake §15 ubica en `Lab-Geometria.Documentacion/Avances/` (fuera de este repositorio).
- `verify-stage-i.sh` contra el despliegue real: se corrió con una `API_URL` inalcanzable a propósito.
- El contenido del `.env` del Product Owner (fuera de alcance); innecesario tras leer `/salud`.

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.1 | 2026-09-13 | §8 asienta la decisión con su literal (A → D; `E-1` opción 2) y la recomendación recalculada. | Orquestador de reanudación SDD |
| 1.0 | 2026-09-13 | Emisión: T0 detenido y normalizado por PR #207; seis dimensiones; mesa R1.5; recomendación A → D; decisión pendiente. | Orquestador de reanudación SDD |
