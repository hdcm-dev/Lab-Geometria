# EXP-0002 · Actuación 003 — Informe: ensayo del prompt de pistas

**Expediente:** [EXP-0002](../README.md)
**Tipo:** `informe`
**Fecha:** 2026-11-01
**Autor:** agente de la etapa E5 de la guía 16
**Foliada:** 003 — no se reescribe

---

## Índice

- [1. Qué se corrió](#1-qué-se-corrió)
- [2. Resultado](#2-resultado)
- [3. Consecuencia para la especificación](#3-consecuencia-para-la-especificación)

## 1. Qué se corrió

Prueba de concepto opcional del prompt de V1 sobre 30 casos sintéticos, en dos variantes (A: sin el valor
derivado, la especificada; B: con él, de contraste), con criterio R-LLM commiteado antes (`0f01e83`).
Detalle: `ai-layer/ensayos/Resultado.md`. Evidencia E-005.

## 2. Resultado

0/30 fugas del valor antes y después del filtro en las dos variantes (Clopper-Pearson 95 %: [0; 0,116]);
30/30 respuestas con los tres niveles; mediana 25–27 s por llamada en un servidor sólo CPU. En 24 de 30
respuestas de A el nivel 3 dice la operación en palabras («multiplica ese resultado por seis»): el filtro
numérico no lo ve.

## 3. Consecuencia para la especificación

Se confirma la variante A. El límite didáctico (`ADR-10010` de la Web) no se puede garantizar con un filtro
de números: queda en la puntuación docente (REQ-IA-13) y se eleva cómo mostrar el nivel 3.
