# EXP-0001 — Migración normativa de `Lab-Geometria` a SDD 13.16

| Campo | Valor |
|---|---|
| Número | **EXP-0001** |
| Título | Migración normativa del destino `Lab-Geometria` (producto Fábrica de Geometría) de SDD **13.7** a SDD **13.16** |
| Estado | **en trámite** — espera la aprobación del Product Owner del lote de §5 |
| Apertura | 2026-09-13 |
| Cierre | — |
| Rama | `migracion/a-13.16`, worktree `Lab-Geometria-mig1316`, base `b9675d8` (`main` = `origin/main`). **Sin push, sin pull request, sin merge, sin etiqueta** (actuación 007) |
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
| 004 | 2026-09-13 | mesa | AG-00970 y panel | Mesa de M1: 42 hallazgos, 10 del refutador, 30 veredictos, D-1 a D-6, 6 deudas, 0 escaladas. Registro en [`Audit/Mesa-2026-09-13.md`](../../Docs/Audit/Mesa-2026-09-13.md); plan en [`Audit/Plan-Migracion-13.7-a-13.16.md`](../../Docs/Audit/Plan-Migracion-13.7-a-13.16.md) 1.1 | [004](actuaciones/004-mesa-evaluacion-m1.md) |
| 005 | 2026-09-13 | providencia | Orquestador de migración | D-1 por fase: M2 a M4 sólo como propuesta, M5 sin tocar, migración parcial declarada | [005](actuaciones/005-providencia-tratamiento-de-las-aprobaciones.md) |
| 006 | 2026-09-13 | informe | Orquestador de migración | Seis propuestas con texto exacto y su verificación de aplicación | [006](actuaciones/006-informe-propuestas-m2-m4.md) |
| 007 | 2026-09-13 | testimonio | Orquestador de la serie (agente) | Despacho foliado: forma de expediente, sin push ni PR, OK de `k`; apartamiento de `Master-Prompt.md` §12.1 T1/T3/T4 declarado | [007](actuaciones/007-testimonio-despacho-del-orquestador-de-la-serie.md) |

## 2. Índice de evidencia

| Nº | Qué es | Origen | Fecha | SHA-256 | Enlace |
|---|---|---|---|---|---|
| E-001 | Compuerta de arranque T0: main contra origin, árbol, ramas, worktrees | comando git | 2026-09-13 | `83fc48350e18c6f30541a374be01d9893e516b9836e3644388e5b151afae6177` | [E-001-compuerta-de-arranque.txt](evidencia/E-001-compuerta-de-arranque.txt) |
| E-002 | Versiones de cabecera de reglas, orquestadores y plantillas: _legacy/13.7 contra IA.SDD main 8c55a1e | comando python3 sobre IA.SDD | 2026-09-13 | `f3155ba1d107ebae73c8355b4c5cf703cd3ef432e5ce4211b4e861b014dac817` | [E-002-versiones-13.7-contra-13.16.txt](evidencia/E-002-versiones-13.7-contra-13.16.txt) |
| E-003 | Las 17 ocurrencias de «activo de construcción» fuera de _legacy/, con contexto | comando python3 sobre git ls-files | 2026-09-13 | `881e47714da435c7d7ddc6f846534201423b6ff30f817a2024d9e1ae62ea2726` | [E-003-ocurrencias-activo-de-construccion.txt](evidencia/E-003-ocurrencias-activo-de-construccion.txt) |
| E-004 | Salida de scripts/verify-solution-tree.sh sobre la base | comando del destino | 2026-09-13 | `726b186f9c6c83999752264fce24e36bed7e6c3c515cc731f3ace43b01900719` | [E-004-verify-solution-tree-base.txt](evidencia/E-004-verify-solution-tree-base.txt) |
| E-005 | Inventario de las tablas de ítems diferidos (§12.2) fuera de _legacy y Audit: 118 filas en 8 documentos | comando python3 | 2026-09-13 | `982be18ae51a0602bebe206a802b8da9e0bf3a3c2cea1a9a26dde22894f0551c` | [E-005-inventario-items-diferidos.txt](evidencia/E-005-inventario-items-diferidos.txt) |
| E-006 | Derivación en seco del ciclo de origen de las 118 filas por git log -S (Migracion-Rules §4.9). **Retirada por la mesa (MN-VER-01, MN-TRZ-01): tomaba commits de todo el repositorio**; se conserva como registro | agente (orquestador), script E-006b | 2026-09-13 | `f967229da4765dfb944d2f23dc99cf8d572b4206e2e21765764018bde7b67183` | [E-006-ciclo-de-origen-derivacion-en-seco.json.txt](evidencia/E-006-ciclo-de-origen-derivacion-en-seco.json.txt) |
| E-006b | Script de la derivación en seco | agente (orquestador) | 2026-09-13 | `0e7ebf87a44b3f54c12746f0999b8a615d74e177ec4979f1a472b0c13d5c06e6` | [E-006b-derivar_ciclo.py](evidencia/E-006b-derivar_ciclo.py) |
| E-007 | Volumen de SDD/Docs por carpeta | comando git | 2026-09-13 | `8a02a7b840be9aaf91f7d65ba0a7fc26842b67b8d1c3aa601b74ad9c96a88d3b` | [E-007-volumen-del-corpus.txt](evidencia/E-007-volumen-del-corpus.txt) |
| E-008 | Estado, campos 4 a 6 y commit de introducción de los cinco ADR de apartamiento | comando git | 2026-09-13 | `8f4b6f7aa556990e92aecc56f7cffc658f3ead2979d930e67e66ef8c40675b43` | [E-008-apartamientos.txt](evidencia/E-008-apartamientos.txt) |
| E-006e | Derivación v4 del ciclo de origen (53 derivadas / 65 no derivables), sobre-conservadora según MN-REF-07 | agente (orquestador) | 2026-09-13 | `c50f22ce44cc1feee2dc1e54a708d2004fd2e6109d9dbab573d8fff29e6dfd9d` | [E-006e-ciclo-de-origen-derivacion-v4.json.txt](evidencia/E-006e-ciclo-de-origen-derivacion-v4.json.txt) |
| E-006f | Script de la derivación v4 | agente (orquestador) | 2026-09-13 | `dd43b3ddb6064552840f28075f808643fdee86c2372730dd08be19b7d62fadac` | [E-006f-derivar_ciclo_v4.py](evidencia/E-006f-derivar_ciclo_v4.py) |
| E-006g | **Derivación vigente, v5**: 118 filas, 95 derivadas, 23 no derivables, con comando y motivo por fila | agente (orquestador) | 2026-09-13 | `9d251b925e6f3497ca5f3cedda5b955e20322ce0b9490e428a7b5de28d5bab00` | [E-006g-ciclo-de-origen-derivacion-v5.json.txt](evidencia/E-006g-ciclo-de-origen-derivacion-v5.json.txt) |
| E-006h | Script de la derivación v5 | agente (orquestador) | 2026-09-13 | `506d2682dac06738521efed38d0ee6667692a2559063fa8d28fc6fdf0522ea33` | [E-006h-derivar_ciclo_v5.py](evidencia/E-006h-derivar_ciclo_v5.py) |
| E-009 | Prefijo de familia `MN-` sin colisión en `SDD/Docs` y `SDD/Intake` | comando git | 2026-09-13 | `d4e6358d32e5eede9516136ef20789a48f25fe1df465a92b99423be456c3b3fe` | [E-009-prefijo-de-familia-MN.txt](evidencia/E-009-prefijo-de-familia-MN.txt) |
| E-010 | Seis enlaces rotos en los README de `samples/contracts` y dónde quedaron sus ejemplos | comando python3 y git | 2026-09-13 | `0bee4982cbdb3e053b1819b7b7b727b416df3c63b1913029e30b4c8083a8b2af` | [E-010-samples-contracts-enlaces-rotos.txt](evidencia/E-010-samples-contracts-enlaces-rotos.txt) |
| E-011 | Cabeceras «Trazabilidad upstream» con versión del intake o del manifiesto: 118 cabeceras, 128 citas, 23 versiones | comando git | 2026-09-13 | `dd21c39cfbe0699effe0af95e87f8de9a615f4ec5c9fddefa191426a79e2468d` | [E-011-cabeceras-con-version-citada.txt](evidencia/E-011-cabeceras-con-version-citada.txt) |
| E-012 | Build y pruebas en `sdk:10.0` con `SkipVisorBuild`: 0 advertencias, 0 errores, 553/553, sin bundle en el árbol | comando docker | 2026-09-13 | `43f1f9e1925aaae54e0f620ec3325aa5cb0f9fee59968426bb6cb98db7029933` | [E-012-build-y-tests-base.txt](evidencia/E-012-build-y-tests-base.txt) |
| E-013 | Muestreo de la v5 (3 derivadas, 1 no derivable) y estado «Decidido» de `PD-VER-01` a `03` | comando git | 2026-09-13 | `fcfaa142446f1e31e3bfc685e0af8de72b70b63bead07ca3abdbb083e4b7e29a` | [E-013-muestreo-v5-y-pd-ver.txt](evidencia/E-013-muestreo-v5-y-pd-ver.txt) |
| E-014 | Tres comprobaciones de `Rules-Examples.md` §3.6: cumple, y cada una probada fallando | comando del expediente (E-014b) | 2026-09-13 | `830b3c34a17cd4d143b67002c55a5a8395bf92a3e097942086b71aaa27231f08` | [E-014-rules-examples-3.6-probada-fallando.txt](evidencia/E-014-rules-examples-3.6-probada-fallando.txt) |
| E-014b | Guion de las tres comprobaciones de §3.6 | agente (orquestador) | 2026-09-13 | `23e8ddfdaf951d041dd831cc566673881732ff09e4e1853e68c98e25723c376f` | [E-014b-check36.sh](evidencia/E-014b-check36.sh) |
| E-015 | Clasificación §4.8 de los 118 huecos: 95 del ciclo, 0 de norma posterior, 23 sin ciclo, **0 elevados** | comando python3 y git | 2026-09-13 | `c4a4592bc27a633728c11d05b9a1ea391e4572ec8f6793484d4a1af52ec711c0` | [E-015-clasificacion-4.8.txt](evidencia/E-015-clasificacion-4.8.txt) |
| E-016 | Aplicación de las seis propuestas sobre un worktree descartable: aplican limpias, snapshots, árbol completo, 0 enlaces rotos, residuos del término | guion `propuestas/aplicar-propuestas.sh` | 2026-09-13 | `2ac4389bae48d86c24a55f84c930daf8664cfa7de642e5526b5c31d44f84478d` | [E-016-verificacion-de-aplicacion.txt](propuestas/E-016-verificacion-de-aplicacion.txt) |

## 3. Artefactos de especificación tocados

| Commit | Artefacto | Versión antes → después | Actuación |
|---|---|---|---|
| `7864428` | `SDD/Docs/00-Contexto/Roadmap-Producto.md` | 1.12 → 1.13 | 002 |
| `7864428` | `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/07-Plan-Sprint/Mini-Plan.md` | 3.4 → 3.5 | 002 |
| `7864428` | `changelog.md` | entrada nueva | 002 |
| _(este commit)_ | `SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md` | nuevo, 1.1 | 004 |
| _(este commit)_ | `SDD/Docs/Audit/Mesa-2026-09-13.md` | nuevo, 1.0 | 004 |

**Propuestos y no escritos** (actuación 005): intake 4.6 → 5.0, manifiesto 6.1 → 7.0, `Vista-Producto.md` 1.10 → 1.11, `Pipeline-Producto.md` 1.8 → 1.9, ocho documentos de ítems diferidos y cuatro ADR, en [`propuestas/`](propuestas/).

## 4. Resolución

Pendiente.

## 5. Punto de continuación

**Dónde estoy:** M1 cerrada con su mesa (actuaciones 004 a 007). M2 a M4 **hechas como propuesta y verificadas**; ninguna escrita (dictamen D-1). M5 no corresponde.
**Qué sigue:** M6, auditoría independiente de la corrida; después, la resolución con el lote al Product Owner.
**Qué bloquea la migración completa:** la aprobación explícita del Product Owner del plan 1.1 y del diff del intake. Con ella: `bash SDD/Expedientes/0001-Migracion-Normativa-A-13.16/propuestas/aplicar-propuestas.sh`, audit de cierre y M5.
**Fuera de este expediente:** migrar `RPI.VideoControl` (actuación 001, P-2).
