# EXP-0002 — Capa de IA: pistas sobre las observaciones y borrador del comentario

| Campo | Valor |
|---|---|
| Número | **EXP-0002** |
| Título | Especificación de una capa de IA de nivel A0 para la Fábrica de Geometría: V1 pistas, V2 borrador de comentario |
| Estado | **en trámite** (actuación 010): V1 **fusionada** en `main` (PR #237, `056801d`), apagada por omisión (fusionar no despliega ni enciende: el despliegue es manual); ingresada al intake como **F-27**; contrato de pistas 1.1, vigente y versionado, con prueba código = ficha (Círculo · Area corregida en la 010); modelo de producción `qwen3.5:4b` sin razonamiento, 20 s, precarga y permanencia (`ADR-14012`), decididos con la evaluación completa. Falta el código de la 010, la corrida de 30 casos con el prompt v2, la puntuación docente de REQ-IA-13 y la corrida E2E con el flag encendido. V2 especificada y diferida |
| Apertura | 2026-11-01 |
| Rama | especificación: `g16/ai-layer-v1`; construcción: `ai-layer/implementacion-v1`, **fusionada** por el PR #237 (`main` `056801d`); cierre: `ai-layer/actuacion-009` |
| Partes | **Product Owner** (decide el ingreso al intake); **orquestador del prompt 02 de la guía 16** de Lab-Documentos (agente, presenta); **agente de la etapa E5** (especifica e informa) |
| Objeto | Probar el Conocimiento de especificación v0 de la guía 16 sobre un caso público y dejar la especificación en `ai-layer/` |
| Origen | [Actuación 001](actuaciones/001-presentacion-pedido-de-especificacion.md) |
| Base | especificación: `main` `5f07b12`; construcción: `main` `c5afa0e` (fusión del PR #236) |

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
| 005 | 2026-11-02 | providencia | coordinador (agente, por delegación del PO) | Encargo de construir V1 con el flag apagado; testimonio del pedido del PO, redactado por S2 | [005](actuaciones/005-providencia-encargo-de-construccion.md) |
| 006 | 2026-11-02 | informe | mesa de construcción (cinco lentes) | Acta: once decisiones con sus votos, vetos y riesgos | [006](actuaciones/006-informe-mesa-de-construccion.md) |
| 007 | 2026-11-02 | resolucion | coordinador | Resuelve A, C, E, F y G; plan T1 a T8; V2 diferida; fila Reintegra | [007](actuaciones/007-resolucion-plan-de-construccion-v1.md) |
| 008 | 2026-11-02 | constancia | implementador de la capa de IA (agente) | T1 a T8 con sus commits; 741 pruebas en verde y 0 advertencias; cobertura conforme; corrida rotulada con modelo local; apartamientos declarados | [008](actuaciones/008-constancia-construccion-de-v1.md) |
| 009 | 2026-11-03 | resolucion | mesa de Geometría (agentes, por delegación del PO) | Fusión del PR #237; resuelve A (F-27), B (revisor 2), D (no), E (modelo y servidor), F (plantillas) y el umbral de REQ-IA-26; tiempos del despliegue; revisión de utilidad de la mesa; lo que queda abierto | [009](actuaciones/009-resolucion-fusion-y-cierre-del-lote.md) |
| 010 | 2026-11-03 | resolucion | mesa de Geometría (agentes, por delegación del PO) | Evaluación completa (diez configuraciones); corrige el punto E y los tiempos de la 009: `qwen3.5:4b` sin razonamiento, 20 s, precarga y permanencia (`ADR-14012`); flag de producción apagado hasta la puntuación docente; plantilla Círculo · Area; prompt v2 candidato; F-27 al bloque Could | [010](actuaciones/010-resolucion-evaluacion-completa-y-modelo-de-produccion.md) |
| 011 | 2026-11-03 | informe | mesa de evaluación de Geometría (agentes) | GEO-04: prompt v2 con `qwen3.5:4b`, 30 casos; filtro 30/30, media de la mesa 3,2 (no docente); 19 pistas falsas y 7 ambiguas: el 4b con v2 queda descartado y la condición de encendido no se cumple | [011](actuaciones/011-informe-corrida-GEO-04-prompt-v2.md) |
| 012 | 2026-11-03 | informe | mesa de evaluación de Geometría (agentes) | GEO-05: prompt v2 con `qwen3.5:35b-a3b`, 30 casos; filtro 30/30, media de la mesa 3,5 (no docente); 3 pistas falsas y 4 ambiguas, p95 caliente 20,6 s: descartado; recomendación: servir sólo la plantilla y cerrar la V1 así | [012](actuaciones/012-informe-corrida-GEO-05-35b-prompt-v2.md) |

## 2. Índice de evidencia

| Nº | Qué es | Origen | SHA-256 | Enlace |
|---|---|---|---|---|
| E-001 | Compuerta de arranque: base, rama, expedientes en main | comando git | `8d01ef6d7d5bace2c9cc1c2f8c093c74d4721e2c7707977f3868e91881578245` | [E-001](evidencia/E-001-compuerta-de-arranque.txt) |
| E-002 | Requisitos sin criterio o prueba: 0 en V1 y V2 | script de la bitácora E4 | `311322afa8b78233e126a4a34db7d53a92ba793a97711b6402fe2127eea9e064` | [E-002](evidencia/E-002-verificar-requisitos.txt) |
| E-003 | Cuatro diagramas mermaid validados | `mermaid.sh` | `f927bbeebfb261e41df9c463b27ed886f4b3821196b18d5f616fb776b45c4fb5` | [E-003](evidencia/E-003-mermaid.txt) |
| E-004 | R-PRIV y filtro de fuga: 0 | `compuerta-rpriv.sh`, grep | `c7939664a8bde64079e32f504491ab1ad9e5d3de1a52116c6fd93c7b14af0af4` | [E-004](evidencia/E-004-rpriv.txt) |
| E-005 | Autoprueba del filtro 8/8 y resumen del ensayo recalculado sin modelo | `pistas.py`, `resumir.py` | `1d6d6afd40fc98b593839b51d0b5b1f52e04899498fddfea3092384d0f8c70c8` | [E-005](evidencia/E-005-ensayo-de-pistas.txt) |
| E-006 | Árbol de solución, build 0 advertencias y 591/591 pruebas sin E2E | scripts del repo, `dotnet` en contenedor | `744769affaa11513c8f772c9463d62f29eb741dbbdc56b056ae8f1bb8f117cab` | [E-006](evidencia/E-006-build-y-pruebas.txt) |
| E-007 | Árbol, build 0 advertencias y 741 pruebas en verde (1 salteada, rotulada), mutación de REQ-IA-02, puertas d/e/h y autoprueba 8/8 | scripts del repo, `dotnet` en contenedor | `104b159b7c50e4ca9d26d3b3f83290e2201768ccd65d1285f9590fb4275ffbf2` | [E-007](evidencia/E-007-build-y-pruebas.txt) |
| E-008 | Cobertura QG-03, QG-06 y QG-04 conformes | `scripts/coverage.sh` en contenedor | `242501331a29cd474547ec2eb05064a95d2eb665588e231f728c3d728ea5b265` | [E-008](evidencia/E-008-cobertura.txt) |
| E-009 | R-PRIV y A11 sobre el diff de la rama y los commits: 0 coincidencias fuera del ejemplo `localhost` | grep | `b0a567a79acf1e29925898aac01f1ac7722e54817da95cee016a8bf5263c757e` | [E-009](evidencia/E-009-rpriv.txt) |
| E-010 | Corrida rotulada con modelo real: 30/30 Ok, 0/30 fugas, IC 95 % [0; 0,116]; latencias de 26 a 89 s | prueba `ModeloReal` contra Ollama local | `e77debc68b42b408b065a82fc1a1bb0b4b5b4fa1f0b3a799edd0b81f68106e43` | [E-010](evidencia/E-010-corrida-rotulada-modelo-real.txt) |

## 3. Artefactos tocados

**Hasta la actuación 004:** sólo archivos nuevos en `ai-layer/` y en este expediente.

**Desde la actuación 005 (construcción de V1):** `src/` (Application/Ports y Works, `Infrastructure/AI/`,
composición, ruta de pistas, contratos y `WorkSubmission.razor`), `tests/`, la Norma de Nomenclatura (1.32 y
1.33), `ADR-14006` a `ADR-14010`, `Contratos-REST.md`, backlog técnico, matriz de cobertura, entornos y
despliegue, los wireframes de envío y la arquitectura de la Web, `changelog.md` y `ai-layer/`. **No** se
tocaron el intake, `deploy/`, los `Dockerfile`, `.github/`, `_legacy/` ni Audit.

## 4. Punto de continuación

**Dónde estoy:** V1 fusionada en `main` por el PR #237 (`056801d`), apagada por omisión. La actuación 010
corrige la 009 con la evaluación completa: modelo `qwen3.5:4b` sin razonamiento (alternativa
`gemma4:e4b-it-qat`) en el servidor de la cátedra, 20 s en todos los entornos, precarga al arrancar y
permanencia del modelo en cada pedido (`ADR-14012`). La revisión de utilidad muestra que, con el prompt v1,
ningún modelo supera a la plantilla.
**Qué sigue, en orden:** (1) aplicar el código propuesto de la 010 y las filas de la norma, con la CI en
verde; (2) corrida rotulada de 30 casos con `qwen3.5:4b` y 20 s, con el prompt v2 (GEO-04, actuación 011: **hecha**; 19 pistas falsas y 7 ambiguas, el 4b con v2 queda descartado y la condición de encendido no se cumple; GEO-05, actuación 012: el 35b con v2 tampoco la cumple, 3 falsas y 4 ambiguas, y la mesa recomienda servir sólo la plantilla y cerrar la V1 así);
(3) el docente puntúa la corrida v2 y se mide la concordancia del juez (REQ-IA-13 y 14); (4) entorno de
prueba con el flag encendido para las puertas c, f, g e i y las E2E (E-012); (5) recién entonces,
`AI__HintsEnabled=true` en producción. V2 sigue diferida hasta
que se cumplan sus condiciones 1 y 2 (actuación 009 §6).
