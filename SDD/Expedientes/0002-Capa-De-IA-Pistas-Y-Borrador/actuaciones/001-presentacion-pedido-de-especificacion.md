# EXP-0002 · Actuación 001 — Presentación: especificar una capa de IA sin construirla

**Expediente:** [EXP-0002](../README.md) · Capa de IA: pistas sobre observaciones y borrador de comentario
**Tipo:** `presentacion`
**Fecha:** 2026-11-01
**Autor de la presentación:** orquestador del prompt 02 de la guía 16 de Lab-Documentos (agente), por el plan que aprobó su mesa
**Asentada por:** agente de la etapa E5 de ese plan
**Foliada:** 001 — no se reescribe; una corrección es una actuación nueva que cita ésta

---

## Índice

- [1. El pedido](#1-el-pedido)
- [2. Origen del hecho](#2-origen-del-hecho)
- [3. Qué pide, descompuesto](#3-qué-pide-descompuesto)
- [4. Lo que esta presentación no autoriza](#4-lo-que-esta-presentación-no-autoriza)

## 1. El pedido

Aplicar el Conocimiento de especificación de capas de IA (versión v0, borrador público de la guía 16 de
Lab-Documentos) a la Fábrica de Geometría para probar si alcanza por sí solo: especificar el piloto **V1**
(pistas sobre las observaciones, nivel A0, sin herramientas) y después **V2** (borrador del comentario del
docente). **Sólo especificación**: nada en `src/`. Carpeta `ai-layer/`; rama `g16/ai-layer-v1`; sin push ni
pull request.

## 2. Origen del hecho

El pedido llega como tarea calculada por un guion de orquestación; **no es palabra del Product Owner** y no
se asienta como tal. Sus decisiones de fondo (V1 primero, V2 después, V3 y V4 fuera; sólo especificación;
el intake no se edita) son de la mesa del prompt 02 de la guía 16 (decisiones C-6.1 a C-6.7, U-6.1 a U-6.3
y D-26 c). La numeración 0002 la confirmó la sesión de documentación técnica que también ensaya en este
repositorio, en su carpeta propia `docs-as-code/`, que este expediente no toca.

## 3. Qué pide, descompuesto

| # | Pedido | Cómo se toma | Dónde queda |
|---|---|---|---|
| P-1 | Especificar V1 con el conocimiento v0 | arc42 reducido, C4, ADR, requisitos con prueba, matriz de riesgos | `ai-layer/01-…`, `03-ADR.md` |
| P-2 | Validador determinista decide; filtro posterior | §4.1 y §4.2 de V1 | `ai-layer/01-…` |
| P-3 | Puerto asincrónico con endpoint propio y respaldo de plantilla | ADR-IA-04 | `ai-layer/03-ADR.md` |
| P-4 | Después V2 con el mismo procedimiento | especificación propia que reutiliza V1 | `ai-layer/02-…` |
| P-5 | Términos nuevos primero en la Norma de Nomenclatura §6 | propuesta de filas; la norma no se edita en esta rama | `ai-layer/04-…` |
| P-6 | Prueba de concepto opcional del prompt | criterio escrito y commiteado antes de correr | `ai-layer/ensayos/` |
| P-7 | Intake sin editar | se eleva | actuación de resolución |

## 4. Lo que esta presentación no autoriza

- **No es aprobación de la capacidad** ni la incorpora al intake: hoy el intake no tiene capacidad F-n de IA.
- **No habilita a fusionar.** La rama queda local; la fusión sigue el ciclo del repositorio y su guarda.
- **No autoriza código** en `src/` ni `tests/`.
