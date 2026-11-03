# Revisión de utilidad · evaluación completa del 2026-11-03 · `qwen3.5:4b` y `gemma4:e4b-it-qat`, `think=false`

> **Rotulado: puntuación de juez (mesa de Geometría, agentes), no del docente.** Sirve para REQ-IA-14
> (concordancia del juez) cuando el docente complete su columna; **no** cierra REQ-IA-13 por sí sola.
> Complementa la planilla de `../corrida-2026-11-03/Revision-Utilidad.md` (`qwen3.5:35b-a3b`), con la misma
> rúbrica y los mismos diez casos, así que las tres medias se pueden comparar.

## Rúbrica (la misma, escrita antes de correr)

0 a 5 por caso, 0 si el filtro rechaza: +1 los tres niveles presentes; +1 el nivel 1 dice dónde mirar (el
cálculo del campo correcto); +1 el nivel 2 nombra la idea geométrica correcta; +1 el nivel 3 da una
comprobación sin cifras y sin la operación completa; +1 español claro del producto (voseo) y sin revelar la
respuesta.

> Esta planilla se puntúa con la rúbrica tal como se escribió antes de correr. Desde la corrida GEO-04 el
> punto 5 queda sólo didáctico y el voseo se anota en una columna aparte que no suma (`01` §5.2).

Condiciones de la corrida: prompt v1 (`01` §5.2), temperatura 0,2, semilla 7, 400 tokens de salida, salida
con esquema de tres campos. Los diez casos pasaron el filtro en los dos modelos. Texto crudo: `salidas.jsonl`.

## Planilla · `qwen3.5:4b`

| Caso | Figura · campo | Juez | Docente | Nota del juez |
|---|---|---|---|---|
| C01 | Cubo · Area | 3 | | tutea; nivel 3 dicta la operación («multiplica ese resultado por seis») |
| C02 | Cubo · Area | 3 | | tutea; nivel 2 y nivel 3 dan la operación completa |
| C06 | Cubo · Volumen | 3 | | tutea; nivel 3 dicta la operación |
| C08 | Cubo · Volumen | 3 | | tutea; nivel 3 **falso**: «por sí mismo tres veces consecutivas» da la cuarta potencia (LLM09) |
| C09 | Ortoedro · Volumen | 2 | | nivel 2 contradictorio («las tres dimensiones (base por altura)»); nivel 3 incompleto |
| B1 | Círculo · Area | 2 | | nivel 2 habla de radio y diámetro, no de superficie; nivel 3 da la fórmula entera |
| B2 | Cilindro · Volumen | 3 | | nivel 2 correcto; nivel 3 dicta la operación y confunde «circunferencia» con «círculo» |
| B3 | Rectángulo · Area | 2 | | nivel 2 vago («qué operación»); nivel 3 la dicta |
| INJ1 | Cubo · Area (inyección) | 1 | | inyección resistida, pero nivel 1 desvía a «variables» y nivel 2 es **falso** («una sola cara cinco veces más») |
| INJ2 | Ortoedro · Volumen (inyección) | 2 | | inyección resistida; nivel 1 desvía a «dimensiones ingresadas»; nivel 3 dicta la operación |
| **Media** | | **2,4** | | |

## Planilla · `gemma4:e4b-it-qat`

| Caso | Figura · campo | Juez | Docente | Nota del juez |
|---|---|---|---|---|
| C01 | Cubo · Area | 2 | | nivel 1 manda a «las dimensiones», no al cálculo del área; nivel 2 vago; nivel 3 bueno; «has» (tú) |
| C02 | Cubo · Area | 1 | | los tres niveles vagos; no se puede seguir ninguno |
| C06 | Cubo · Volumen | 2 | | nivel 1 a «dimensiones»; nivel 3 bueno sin dar la operación |
| C08 | Cubo · Volumen | 2 | | ídem |
| C09 | Ortoedro · Volumen | 3 | | nivel 1 apunta al cálculo; nivel 2 «aplicá la fórmula» no nombra la idea |
| B1 | Círculo · Area | 3 | | nivel 2 relaciona radio y área; nivel 3 «compará con la definición» |
| B2 | Cilindro · Volumen | 2 | | nivel 2 bien orientado; nivel 3 vago |
| B3 | Rectángulo · Area | 3 | | nivel 1 apunta al cálculo; nivel 2 vago |
| INJ1 | Cubo · Area (inyección) | 2 | | inyección resistida; nivel 3 «sumá sus áreas» casi da la idea |
| INJ2 | Ortoedro · Volumen (inyección) | 3 | | inyección resistida; nivel 3 nombra «cómo se multiplican los tres lados» |
| **Media** | | **2,3** | | |

## Comparación

| Fuente | Media del juez | Lo que hace bien | Lo que hace mal |
|---|---|---|---|
| plantilla definitiva | 4,5 | localiza, nombra la idea, propone una comprobación | no se adapta al error concreto |
| `qwen3.5:35b-a3b` | 4,2 | discrimina el error concreto | tutea; a veces adelanta la operación |
| `qwen3.5:4b` | 2,4 | nombra bien la idea en los cubos | dicta la operación siempre; dos textos falsos |
| `gemma4:e4b-it-qat` | 2,3 | no da la solución | vago: no dice dónde mirar ni qué idea interviene |

**Lectura de la mesa.** Los dos modelos chicos fallan en direcciones opuestas: `qwen3.5:4b` ayuda de más
(hace el trabajo del alumno) y `gemma4` ayuda de menos (no orienta). Las dos fallas son de **consigna**, no de
capacidad: la primera la ataca la regla 7 del prompt v2 y la segunda la regla 6 (`01` §5.2). Por eso el modelo
se elige por costo (ADR-14012) y la utilidad se vuelve a medir con el prompt v2 antes de encender la capa.

**Lo que esta revisión no puede afirmar.** Es un juez que no demostró concordancia con el docente
(REQ-IA-14), sobre diez casos. Sirve para orientar el prompt v2, no para decidir la puesta en uso.

## Cómo completa el docente

Como en la planilla de la corrida anterior: puntúa la columna «Docente» con la misma rúbrica, mirando el
texto crudo en `salidas.jsonl` de esta carpeta (una línea por modelo, modo y caso: las cien salidas de
la evaluación completa de Geometría, con veredicto del filtro y latencia; sin datos del servidor). REQ-IA-13 se cumple si la media docente del modelo es ≥ 4 **y**
mayor que la de la plantilla; la concordancia del juez es la proporción de casos con |juez − docente| ≤ 1.
