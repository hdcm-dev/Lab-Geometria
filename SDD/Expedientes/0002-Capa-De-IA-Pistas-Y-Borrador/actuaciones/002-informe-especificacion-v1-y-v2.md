# EXP-0002 · Actuación 002 — Informe: especificación de V1 y V2

**Expediente:** [EXP-0002](../README.md)
**Tipo:** `informe`
**Fecha:** 2026-11-01
**Autor:** agente de la etapa E5 de la guía 16 (especificó sin la guía, sólo con el Conocimiento v0 y el repositorio)
**Foliada:** 002 — no se reescribe

---

## Índice

- [1. Qué se produjo](#1-qué-se-produjo)
- [2. Decisiones de fondo](#2-decisiones-de-fondo)
- [3. Verificaciones](#3-verificaciones)
- [4. Lo que queda abierto](#4-lo-que-queda-abierto)

## 1. Qué se produjo

| Documento | Contenido | Evidencia |
|---|---|---|
| `ai-layer/01-Especificacion-V1-Pistas.md` | V1: 16 requisitos, 2 diagramas, ficha, prompt, matriz | E-002, E-003 |
| `ai-layer/02-Especificacion-V2-Borrador-De-Comentario.md` | V2: 8 requisitos, 2 diagramas | E-002, E-003 |
| `ai-layer/03-ADR.md` | ADR-IA-01 a 06 | — |
| `ai-layer/04-Propuesta-Norma-De-Nomenclatura.md` | 19 filas para §6 y 2 rutas | — |
| `ai-layer/05-Checklist-Completitud.md` | V1 y V2 completas para el revisor 1; revisor humano pendiente | E-002 |

## 2. Decisiones de fondo

1. **Nivel A0 en las dos capacidades.** El modelo sólo redacta; nada escribe. En V2 el docente envía su
   texto por `ResolveWorkUseCase`, que no cambia (ADR-IA-06).
2. **Minimización:** al modelo no viajan el JSON del alumno, el valor derivado ni datos de persona. El
   adaptador conoce el derivado sólo para que el filtro detecte la fuga.
3. **Filtro posterior determinista** y **plantilla de respaldo**: el alumno nunca se queda sin respuesta y
   nunca recibe el valor, un JSON o código.
4. **`IFigureValidator` no cambia** (G-6): la capa de IA va en puertos aparte (ADR-IA-01).
5. **Carpeta `Infrastructure/AI/`** con prueba de arquitectura, y no proyecto nuevo (ADR-IA-03). Hallazgo
   de paso: la prueba vigente `DomainReferencesNoThirdPartyLibrary`
   (`tests/GeometriaFactory.Domain.Tests/DependencyGateTests.cs`) admite cualquier ensamblado con prefijo
   `Microsoft`, de modo que no detectaría el SDK de IA; REQ-IA-01 pide una prueba específica.
6. **A-18 intacto**: endpoint propio `POST /v1/interpretaciones/pistas` (ADR-IA-04).

## 3. Verificaciones

| Control | Resultado | Evidencia |
|---|---|---|
| `E4-verificar-requisitos.py` sobre V1 y V2 | 16 y 8 requisitos, 0 incompletos, rc 0 | E-002 |
| `mermaid.sh` | 4 diagramas ok, rc 0 | E-003 |
| R-PRIV (`compuerta-rpriv.sh`, clases genéricas) sobre `ai-layer/` y este expediente | 0 hallazgos, rc 0 | E-004 |

## 4. Lo que queda abierto

Ver la actuación de resolución: ingreso al intake, servidor de PROD, revisión humana del checklist, texto de
las plantillas y umbral de V2.
