# EXP-0002 — Capa de IA: pistas sobre las observaciones y borrador del comentario

| Campo | Valor |
|---|---|
| Número | **EXP-0002** |
| Título | Especificación de una capa de IA de nivel A0 para la Fábrica de Geometría: V1 pistas, V2 borrador de comentario |
| Estado | **especificado**: V1 y V2 completas para el revisor 1; lote al Product Owner abierto (actuación 004). Nada construido |
| Apertura | 2026-11-01 |
| Rama | `g16/ai-layer-v1` desde `origin/main` `5f07b12`, local. **Sin push, sin pull request, sin merge** |
| Partes | **Product Owner** (decide el ingreso al intake); **orquestador del prompt 02 de la guía 16** de Lab-Documentos (agente, presenta); **agente de la etapa E5** (especifica e informa) |
| Objeto | Probar el Conocimiento de especificación v0 de la guía 16 sobre un caso público y dejar la especificación en `ai-layer/` |
| Origen | [Actuación 001](actuaciones/001-presentacion-pedido-de-especificacion.md) |

**Forma.** La del expediente [EXP-0001](../0001-Migracion-Normativa-A-13.16/README.md): carátula, actuaciones
foliadas que no se reescriben y evidencia con SHA-256. Los artefactos viven en [`ai-layer/`](../../../ai-layer/README.md),
que el expediente folia por enlace sin duplicarlos.

---

## Índice

- [1. Índice foliado de actuaciones](#1-índice-foliado-de-actuaciones)
- [2. Índice de evidencia](#2-índice-de-evidencia)
- [3. Artefactos tocados](#3-artefactos-tocados)
- [4. Punto de continuación](#4-punto-de-continuación)

## 1. Índice foliado de actuaciones

| Nº | Fecha | Tipo | Autor | Resumen | Enlace |
|---|---|---|---|---|---|
| 001 | 2026-11-01 | presentacion | orquestador de la guía 16 (agente) | Especificar V1 y luego V2 con el Conocimiento v0, sin código; origen del hecho declarado (no es palabra del PO) | [001](actuaciones/001-presentacion-pedido-de-especificacion.md) |
| 002 | 2026-11-01 | informe | agente E5 | V1 (16 requisitos) y V2 (8), seis ADR, 19 filas de nomenclatura, checklist completo para el revisor 1 | [002](actuaciones/002-informe-especificacion-v1-y-v2.md) |
| 003 | 2026-11-01 | informe | agente E5 | Ensayo del prompt: 0/30 fugas del valor en A y B; 24/30 dicen la operación en palabras | [003](actuaciones/003-informe-ensayo-del-prompt-de-pistas.md) |
| 004 | 2026-11-01 | resolucion | agente E5 | Estado y lote de siete puntos al Product Owner con su «si no respondés» | [004](actuaciones/004-resolucion-lote-al-product-owner.md) |

## 2. Índice de evidencia

| Nº | Qué es | Origen | SHA-256 | Enlace |
|---|---|---|---|---|
| E-001 | Compuerta de arranque: base, rama, expedientes en main | comando git | `8d01ef6d7d5bace2c9cc1c2f8c093c74d4721e2c7707977f3868e91881578245` | [E-001](evidencia/E-001-compuerta-de-arranque.txt) |
| E-002 | Requisitos sin criterio o prueba: 0 en V1 y V2 | script de la bitácora E4 | `311322afa8b78233e126a4a34db7d53a92ba793a97711b6402fe2127eea9e064` | [E-002](evidencia/E-002-verificar-requisitos.txt) |
| E-003 | Cuatro diagramas mermaid validados | `mermaid.sh` | `f927bbeebfb261e41df9c463b27ed886f4b3821196b18d5f616fb776b45c4fb5` | [E-003](evidencia/E-003-mermaid.txt) |
| E-004 | R-PRIV y filtro de fuga: 0 | `compuerta-rpriv.sh`, grep | `c7939664a8bde64079e32f504491ab1ad9e5d3de1a52116c6fd93c7b14af0af4` | [E-004](evidencia/E-004-rpriv.txt) |
| E-005 | Autoprueba del filtro 8/8 y resumen del ensayo recalculado sin modelo | `pistas.py`, `resumir.py` | `1d6d6afd40fc98b593839b51d0b5b1f52e04899498fddfea3092384d0f8c70c8` | [E-005](evidencia/E-005-ensayo-de-pistas.txt) |
| E-006 | Árbol de solución, build 0 advertencias y 591/591 pruebas sin E2E | scripts del repo, `dotnet` en contenedor | `744769affaa11513c8f772c9463d62f29eb741dbbdc56b056ae8f1bb8f117cab` | [E-006](evidencia/E-006-build-y-pruebas.txt) |

## 3. Artefactos tocados

Sólo archivos nuevos en `ai-layer/` y en este expediente. No se tocaron `src/`, `tests/`, el intake, la
Norma de Nomenclatura, `docs-as-code/` ni la ia-db.

## 4. Punto de continuación

**Dónde estoy:** especificación completa para el revisor 1; rama local con commits propios.
**Qué sigue:** respuesta del Product Owner al lote de la actuación 004, en el lote único de cierre de la
guía 16; revisión humana del checklist; las respuestas entran como actuaciones nuevas.
