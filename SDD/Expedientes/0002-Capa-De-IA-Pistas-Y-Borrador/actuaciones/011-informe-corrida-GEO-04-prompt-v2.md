# EXP-0002 · Actuación 011 — Informe: corrida GEO-04 del prompt v2 con `qwen3.5:4b`

| Campo | Valor |
|---|---|
| Tipo | informe |
| Fecha | 2026-11-03 |
| Autor | mesa de evaluación de Geometría (agentes, por delegación del Product Owner) |
| Responde a | actuación 010 §6 y `01` §5.2 (prompt v2 candidato, decisión D-NIVEL3); punto 2 del punto de continuación |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 011 — no se reescribe

---

## Índice

- [1. Qué se corrió](#1-qué-se-corrió)
- [2. Resultado](#2-resultado)
- [3. Lectura](#3-lectura)
- [4. Lo que queda abierto](#4-lo-que-queda-abierto)

## 1. Qué se corrió

GEO-04: banco determinista de 30 casos, variante A, prompt v2 de `01` §5.2 (reglas 5 a 7), `qwen3.5:4b`
sin razonamiento, 400 tokens, temperatura 0,2, semilla 7, en el servidor de la cátedra sin otra carga.
Arnés `ai-layer/ensayos/pistas.py --prompt v2` (el prompt del código, v1, no cambia). Salidas, puntuación,
resumen y sumas en `ai-layer/ensayos/corrida-GEO-04-2026-11-03/`; las cifras de esta actuación las recalcula
la CI sin modelo (`recalcular.py`, sección 4).

## 2. Resultado

- Filtro: 30/30 aceptadas, 0 fugas del valor, 0 pedidos por encima de 20 s (p95 13,5 s).
- Rúbrica de GEO-04 (punto 5 sólo didáctico, voseo aparte): media de la mesa 3,2 de 5, **puntuación de la
  mesa, no del docente**. Plantilla 4,5; `qwen3.5:4b` con prompt v1, 2,4.
- Voseo en 30 de 30 (regla 5) y nivel 1 en el cálculo del campo en 29 de 30 (regla 6).
- **Pistas: 19 falsas y 7 ambiguas** en 30 casos. Ejemplos: C08 «dividilo por seis, porque un cubo tiene ese
  número de aristas»; C17 «sumá las bases y multiplicá por la altura»; C26 manda a sumar el lado cuatro veces,
  que es el error del alumno; C18 y C19 escriben cifras que no son el derivado y el filtro no ve.

## 3. Lectura

La regla 7 quitó la operación dictada y el 4b la reemplazó por una operación inventada. Por la regla de
`01` §5.2-5.3 (un texto falso o ambiguo descarta, aunque la media suba), **`qwen3.5:4b` con el prompt v2
queda descartado como redactor**; además la media no llega a 4 ni supera a la plantilla. El prompt v2 no
entra al código. **La condición de encendido no queda cumplida**: no falta sólo la firma docente.
`AI:HintsEnabled` sigue en `false` en `main`.

## 4. Lo que queda abierto

| Tema | Dueño |
|---|---|
| Elegir entre otro candidato con el prompt v2 (`qwen3.5:35b-a3b`, 4,2 con v1) y servir sólo la plantilla | mesa de Geometría |
| Columna «Docente» de la planilla de GEO-04 (REQ-IA-13 y 14) | docente |
