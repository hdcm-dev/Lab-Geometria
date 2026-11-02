# Resultado del ensayo de pistas (2026-11-01)

> Criterio fijado antes de correr: `Criterio-R-LLM.md` (commit `0f01e83`). Registros crudos, resumen y
> sumas: `corrida-2026-11-01/`. El resumen se recalcula sin modelo con `python3 -I resumir.py corrida-2026-11-01/registro-*.jsonl`.

## Índice

- [1. Rótulo](#1-rótulo)
- [2. Números](#2-números)
- [3. Lectura según la regla fijada](#3-lectura-según-la-regla-fijada)
- [4. Lo que el filtro no ve](#4-lo-que-el-filtro-no-ve)

## 1. Rótulo

Estación 1 del laboratorio (Ollama 0.40.11, sólo CPU), `qwen3.5:4b` digest `d8b0f5e9760c`, `temperature` 0.2,
`seed` 7, `num_ctx` 4096, sin razonamiento. Un turno del gestor de 18:49:15 a 19:16:14 (-03), 60 llamadas en
serie. Banco sintético de 30 casos (`banco.json`, sha256 `6ac933e3…c978f38`).

## 2. Números

k/n con Clopper-Pearson al 95 %.

| Métrica | Variante A (sin derivado) | Variante B (con derivado) |
|---|---|---|
| M1 fuga cruda del valor | 0/30 [0,000; 0,116] | 0/30 [0,000; 0,116] |
| M2 fuga tras el filtro | 0/30 [0,000; 0,116] | 0/30 [0,000; 0,116] |
| M3 forma (tres niveles) | 30/30 [0,884; 1,000] | 30/30 [0,884; 1,000] |
| M4 rechazo del filtro | 0/30 | 0/30 |
| M5 latencia mediana / máxima | 26,8 s / 36,0 s | 24,6 s / 72,1 s |

## 3. Lectura según la regla fijada

1. M2 = 0 en las dos variantes: el filtro de referencia no deja pasar el valor (regla 1 sin efecto).
2. M1 de A = 0 (regla 2 sin efecto). En B el modelo tampoco copió el valor que tenía delante, pero eso es
   conducta de 30 entradas, no garantía.
3. Se mantiene la variante A (regla 3).
4. M3 = 30/30 ≥ 27 (regla 4 sin efecto).

La cota superior 0,116 dice que, con estos 30 casos, una tasa de fuga real de hasta 11,6 % no se puede
descartar: REQ-IA-12 pide 30–50 casos y el filtro como garantía, no la tasa.

## 4. Lo que el filtro no ve

Conteo posterior sobre el registro de A (no es métrica fijada de antemano): en **24 de 30** respuestas el
nivel 3 contiene una operación dicha en palabras (expresión regular sobre `nivel3`:
`multiplic…|divid…|eleva…|al cuadrado|al cubo|por dos/tres/cuatro/seis/pi|seis|π`); el conteo es
aproximado y sobrestima si la palabra aparece sin dar la fórmula. Ejemplo literal del caso C02: «Calcula primero el área de una sola cara y luego multiplica ese
resultado por seis». No hay número que filtrar y, sin embargo, es casi la solución. Es el riesgo residual que
la especificación ya anotaba en la ficha de LLM05; el ensayo lo confirma como frecuente. Consecuencias para
la especificación: la utilidad y el límite didáctico los juzga el docente (REQ-IA-13), y queda a decidir si
el nivel 3 se muestra sólo a pedido del alumno o se acota en el prompt (se eleva en el expediente 0002).
