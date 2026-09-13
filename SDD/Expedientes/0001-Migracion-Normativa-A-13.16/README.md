# EXP-0001 — Migración normativa de `Lab-Geometria` a SDD 13.16

| Campo | Valor |
|---|---|
| Número | **EXP-0001** |
| Título | Migración normativa del destino `Lab-Geometria` (producto Fábrica de Geometría) de SDD **13.7** a SDD **13.16** |
| Estado | **abierto** |
| Apertura | 2026-09-13 |
| Cierre | — |
| Rama | `migracion/a-13.16`, worktree `Lab-Geometria-mig1316`, base `b9675d8` (`main` = `origin/main`). **Sin push, sin pull request, sin merge, sin etiqueta** |
| Partes | **Product Owner** (presenta y decide); **Orquestador de migración normativa SDD** (`Master-Prompt-Migracion.md` 2.10, instruye); **comisiones de mesa** que se convoquen (`Mesa-Rules.md` 1.3); **auditor independiente de M6** |
| Objeto | Llevar `SDD/Intake/` y `SDD/Docs/` de la procedencia declarada a la versión vigente del framework, preservando contenido (`Migracion-Rules.md` 3.20), y asentar en la misma rama el OK de la fase `k` |
| Origen | [Actuación 001](actuaciones/001-presentacion-pedido-del-product-owner.md), presentación del Product Owner del 2026-09-13 |

**Adelanto declarado a una norma que todavía no existe.** La forma de expediente —carátula, actuaciones
foliadas que no se reescriben, evidencia con procedencia y SHA-256— la pidió el Product Owner el
2026-09-13 y **el framework aún no la norma**. Se aplica como adelanto; cuando la norma llegue, este
expediente se alinea con una actuación nueva y no reescribiendo las anteriores.

**Relación con los artefactos que la norma vigente ubica.** El plan de migración, el registro de mesa y el
informe de M6 viven **donde `Migracion-Rules.md` §2.1 y `Mesa-Rules.md` §2.1 los mandan**:
`SDD/Docs/Audit/`. **El expediente los folia por enlace y no los duplica.**

---

## 1. Índice foliado de actuaciones

| Nº | Fecha | Tipo | Autor | Resumen | Enlace |
|---|---|---|---|---|---|
| 001 | 2026-09-13 | presentacion | Product Owner | Migrar `Lab-Geometria`; ante un problema, mesa y no detención; saber dónde se está parado | [001](actuaciones/001-presentacion-pedido-del-product-owner.md) |
| 002 | 2026-09-13 | testimonio | Product Owner | OK del punto de control de la fase `k`, asentado en roadmap, mini-plan y changelog | [002](actuaciones/002-testimonio-ok-fase-k.md) |
| 003 | 2026-09-13 | informe | Orquestador de migración | M0: compuerta en orden (base `b9675d8`), procedencia **13.7** confirmada, diff 13.7 → 13.16 sin major, superficies medidas | [003](actuaciones/003-informe-m0-reconocimiento-del-destino.md) |

## 2. Índice de evidencia

| Nº | Qué es | Origen | Fecha | SHA-256 | Enlace |
|---|---|---|---|---|---|
| E-001 | Compuerta de arranque T0: main contra origin, árbol, ramas, worktrees | comando git | 2026-09-13 | `83fc48350e18c6f30541a374be01d9893e516b9836e3644388e5b151afae6177` | [E-001-compuerta-de-arranque.txt](evidencia/E-001-compuerta-de-arranque.txt) |
| E-002 | Versiones de cabecera de reglas, orquestadores y plantillas: _legacy/13.7 contra IA.SDD main 8c55a1e | comando python3 sobre IA.SDD | 2026-09-13 | `f3155ba1d107ebae73c8355b4c5cf703cd3ef432e5ce4211b4e861b014dac817` | [E-002-versiones-13.7-contra-13.16.txt](evidencia/E-002-versiones-13.7-contra-13.16.txt) |
| E-003 | Las 17 ocurrencias de «activo de construcción» fuera de _legacy/, con contexto | comando python3 sobre git ls-files | 2026-09-13 | `881e47714da435c7d7ddc6f846534201423b6ff30f817a2024d9e1ae62ea2726` | [E-003-ocurrencias-activo-de-construccion.txt](evidencia/E-003-ocurrencias-activo-de-construccion.txt) |
| E-004 | Salida de scripts/verify-solution-tree.sh sobre la base | comando del destino | 2026-09-13 | `726b186f9c6c83999752264fce24e36bed7e6c3c515cc731f3ace43b01900719` | [E-004-verify-solution-tree-base.txt](evidencia/E-004-verify-solution-tree-base.txt) |
| E-005 | Inventario de las tablas de ítems diferidos (§12.2) fuera de _legacy y Audit: 118 filas en 8 documentos | comando python3 | 2026-09-13 | `982be18ae51a0602bebe206a802b8da9e0bf3a3c2cea1a9a26dde22894f0551c` | [E-005-inventario-items-diferidos.txt](evidencia/E-005-inventario-items-diferidos.txt) |
| E-006 | Derivación en seco del ciclo de origen de las 118 filas por git log -S (Migracion-Rules §4.9) | agente (orquestador), script E-006b | 2026-09-13 | `f967229da4765dfb944d2f23dc99cf8d572b4206e2e21765764018bde7b67183` | [E-006-ciclo-de-origen-derivacion-en-seco.json.txt](evidencia/E-006-ciclo-de-origen-derivacion-en-seco.json.txt) |
| E-006b | Script de la derivación en seco | agente (orquestador) | 2026-09-13 | `0e7ebf87a44b3f54c12746f0999b8a615d74e177ec4979f1a472b0c13d5c06e6` | [E-006b-derivar_ciclo.py](evidencia/E-006b-derivar_ciclo.py) |
| E-007 | Volumen de SDD/Docs por carpeta | comando git | 2026-09-13 | `8a02a7b840be9aaf91f7d65ba0a7fc26842b67b8d1c3aa601b74ad9c96a88d3b` | [E-007-volumen-del-corpus.txt](evidencia/E-007-volumen-del-corpus.txt) |
| E-008 | Estado, campos 4 a 6 y commit de introducción de los cinco ADR de apartamiento | comando git | 2026-09-13 | `8f4b6f7aa556990e92aecc56f7cffc658f3ead2979d930e67e66ef8c40675b43` | [E-008-apartamientos.txt](evidencia/E-008-apartamientos.txt) |

## 3. Artefactos de especificación tocados

| Commit | Artefacto | Versión antes → después | Actuación |
|---|---|---|---|
| `7864428` | `SDD/Docs/00-Contexto/Roadmap-Producto.md` | 1.12 → 1.13 | 002 |
| `7864428` | `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/07-Plan-Sprint/Mini-Plan.md` | 3.4 → 3.5 | 002 |
| `7864428` | `changelog.md` | entrada nueva | 002 |

## 4. Resolución

Pendiente.

## 5. Punto de continuación

**Dónde estoy:** M0 cerrada (actuación 003), sin detención.
**Qué sigue:** M1 — plan de migración `SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md` y mesa de evaluación `SDD/Docs/Audit/Mesa-2026-09-13.md`.
**Qué bloquea:** nada.
