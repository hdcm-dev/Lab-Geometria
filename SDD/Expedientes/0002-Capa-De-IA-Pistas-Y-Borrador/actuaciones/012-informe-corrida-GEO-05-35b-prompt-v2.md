# EXP-0002 · Actuación 012 — Informe: corrida GEO-05 del prompt v2 con `qwen3.5:35b-a3b`

| Campo | Valor |
|---|---|
| Tipo | informe |
| Fecha | 2026-11-03 |
| Autor | mesa de evaluación de Geometría (agentes, por delegación del Product Owner) |
| Responde a | actuación 011 §4 (elegir entre otro candidato con el prompt v2 y servir sólo la plantilla) |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 012 — no se reescribe

---

## Índice

- [1. Qué se corrió](#1-qué-se-corrió)
- [2. Resultado](#2-resultado)
- [3. Lectura](#3-lectura)
- [4. Recomendación](#4-recomendación)

## 1. Qué se corrió

GEO-05: las condiciones de GEO-04 (30 casos, variante A, prompt v2 de `01` §5.2, misma rúbrica) con
`qwen3.5:35b-a3b` sin razonamiento, el candidato que con el prompt v1 había puntuado 4,2. Arnés
`ai-layer/ensayos/pistas.py`, que ahora elige el candidato con `PISTAS_MODELO` (por omisión el de la ficha).
Salidas, puntuación, resumen y sumas en `ai-layer/ensayos/corrida-GEO-05-2026-11-03/`; las cifras de esta
actuación las recalcula la CI sin modelo (`recalcular.py`, sección 4).

## 2. Resultado

- Filtro: 30/30 aceptadas, 0 fugas del valor, ninguna falla del modelo.
- Latencia: frío 35,7 s; en caliente p50 16,3 s y p95 20,6 s; 3 pedidos por encima del tope de 20 s (el
  frío y dos en caliente). Precarga al arrancar con `keep_alive: -1` obligatoria y aun así insuficiente.
- Rúbrica de GEO-04: media de la mesa 3,5 de 5, **puntuación de la mesa, no del docente**. Plantilla 4,5;
  `qwen3.5:4b` con v2, 3,2 (26 de 30 defectuosas); `qwen3.5:35b-a3b` con v1, 4,2.
- **Pistas: 3 falsas y 4 ambiguas** en 30 casos. Ejemplos: C06 manda a «elevar al cuadrado» para el volumen
  del cubo; C14 presenta como error la fórmula correcta del área del ortoedro; C15 «elevas ese resultado al
  cuadrado»; C25 compara el cuadrado con «un hexágono».
- Voseo 29 de 30; nivel 1 en el cálculo del campo 30 de 30; la regla 7 se incumple en la mayoría (dicta la
  operación correcta).

## 3. Lectura

El 35b comete muchos menos errores que el 4b, pero la regla de `01` §5.2-5.3 descarta con un solo texto falso
o ambiguo, y hay siete. La media no llega a 4 ni supera a la plantilla, y el tope de 20 s no se sostiene ni en
caliente. **La condición de encendido no queda cumplida**: no falta sólo la firma docente. El modelo de la
ficha no cambia y `AI:HintsEnabled` sigue en `false` en `main`.

## 4. Recomendación

Servir sólo la plantilla (contrato de pistas 1.1, 4,5) y **cerrar la V1 así**. Una reapertura exige un
candidato que dé 0 pistas falsas o ambiguas en los 30 casos con el p95 en caliente por debajo del tope; la
columna «Docente» de las planillas de GEO-04 y GEO-05 queda a disposición del docente.
