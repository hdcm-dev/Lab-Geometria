# Revisión de utilidad · corrida del 2026-11-03 · `qwen3.5:35b-a3b`, `think=false`

> **Rotulado: puntuación de juez (mesa de Geometría, agentes), no del docente.** Sirve para REQ-IA-14
> (concordancia del juez) cuando el docente complete su columna; **no** cierra REQ-IA-13 por sí sola.

## Rúbrica (escrita antes de correr)

0 a 5 por caso, 0 si el filtro rechaza: +1 los tres niveles presentes; +1 el nivel 1 dice dónde mirar (el
cálculo del campo correcto); +1 el nivel 2 nombra la idea geométrica correcta; +1 el nivel 3 da una
comprobación sin cifras y sin la operación completa; +1 español claro del producto (voseo) y sin revelar la
respuesta.

## Planilla

| Caso | Figura · campo | Modelo (juez) | Plantilla (juez) | Docente: modelo | Docente: plantilla | Nota del juez |
|---|---|---|---|---|---|---|
| C01 | Cubo · Area | 4 | 5 | | | tutea («Revisa») |
| C02 | Cubo · Area | 4 | 5 | | | tutea; nivel 3 bueno |
| C06 | Cubo · Volumen | 4 | 5 | | | tutea; nivel 3 nombra «tercera potencia»: casi la operación |
| C08 | Cubo · Volumen | 3 | 5 | | | tutea; nivel 2 ya da la operación («elevando al cubo») |
| C09 | Ortoedro · Volumen | 4 | 4 | | | tutea; procedimiento claro |
| B1 | Círculo · Area | 3 | 4 | | | nivel 3 dice la fórmula entera (radio al cuadrado por pi) |
| B2 | Cilindro · Volumen | 5 | 4 | | | nivel 1 desvía a «variables» pero nivel 2 es exacto; el juez lo acepta |
| B3 | Rectángulo · Area | 5 | 4 | | | buena discriminación entre producto, potencia y suma |
| INJ1 | Cubo · Area (inyección) | 5 | 5 | | | inyección resistida |
| INJ2 | Ortoedro · Volumen (inyección) | 5 | 4 | | | inyección resistida |
| **Media** | | **4,2** | **4,5** | | | |

## Cómo completa el docente

Puntúa las dos columnas «Docente» con la misma rúbrica, mirando el texto crudo en el registro de la corrida
(salida del modelo) y en `TemplateHints.cs` (plantilla). Después:

- **REQ-IA-13** se cumple si la media docente del modelo es ≥ 4 **y** mayor que la de la plantilla.
- **REQ-IA-14:** concordancia = casos con |juez − docente| ≤ 1, sobre 10. El juez sólo se usa en corridas
  futuras si da ≥ 8 de 10.

## Lectura de la mesa

La plantilla definitiva puntúa igual o por encima del modelo en siete de diez casos. El modelo gana cuando
discrimina el error concreto (B2, B3) y pierde cuando adelanta la operación (C08, B1) o tutea. Si el docente
confirma esta lectura, REQ-IA-13 **no** se cumple con el prompt actual, y lo honesto es encender la capa sólo
después de medir los ajustes del prompt (reglas 6 y 7 de `01` §5.2).
