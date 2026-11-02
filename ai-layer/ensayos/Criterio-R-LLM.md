# Criterio R-LLM del ensayo de pistas (escrito antes de correr)

> Ensayo opcional de la etapa E5 de la guía 16 (prueba de concepto del prompt de pistas del piloto V1).
> Fecha de escritura: 2026-11-01, antes de la primera llamada al modelo. Este archivo se commitea antes de
> correr, para que el criterio quede fijado con su SHA. Cualquier cambio posterior va en un archivo nuevo.

## Índice

- [1. Qué se mide y qué no](#1-qué-se-mide-y-qué-no)
- [2. Rótulo de la corrida](#2-rótulo-de-la-corrida)
- [3. Banco de casos](#3-banco-de-casos)
- [4. Variantes](#4-variantes)
- [5. Métricas y cómo se informan](#5-métricas-y-cómo-se-informan)
- [6. Regla de decisión fijada de antemano](#6-regla-de-decisión-fijada-de-antemano)
- [7. Lo que este ensayo no puede decir](#7-lo-que-este-ensayo-no-puede-decir)

## 1. Qué se mide y qué no

Se mide la **conducta del modelo** ante el prompt propio de la capacidad «pistas de observaciones»
(`01-Especificacion-V1-Pistas.md` §5.2) y la **garantía del filtro posterior** (`ensayos/pistas.py`,
función `filtrar`), que es la implementación de referencia de los requisitos REQ-IA-05 y REQ-IA-06.

No se mide la utilidad pedagógica: la puntúa el docente (REQ-IA-13) y este ensayo no la sustituye.

## 2. Rótulo de la corrida

| Campo | Valor |
|---|---|
| Servidor | estación 1 del laboratorio de la guía (Ollama, sólo CPU); su dirección no se escribe en el repositorio: llega por la variable `OLLAMA_URL` |
| Modelo | `qwen3.5:4b`, digest abreviado `d8b0f5e9760c` (el script lo comprueba antes de la primera llamada) |
| Opciones | `temperature` 0.2, `seed` 7, `num_ctx` 4096, `think` falso, salida con esquema JSON de tres campos |
| Turno | gestor de recursos del laboratorio, un turno para toda la corrida, un pedido a la vez |
| Fecha | la del registro de la corrida |

## 3. Banco de casos

30 casos generados por `ensayos/pistas.py --banco`, deterministas, sobre los seis tipos de figura del
producto y los dos campos que el validador compara (`Area`, `Volumen`). Cada caso reproduce un error de
fórmula típico (por ejemplo, el área del cubo con 4·l² en lugar de 6·l², que es la trampa T4 del contrato
del validador). El banco se guarda en `ensayos/banco.json` con su sha256 en el registro.

## 4. Variantes

| Variante | Qué viaja al modelo |
|---|---|
| **A** (la que especifica V1) | tipo de figura, campo, valor declarado y sentido de la diferencia (mayor o menor); **no** viaja el valor derivado |
| **B** (contraste) | lo mismo que A **más** el valor derivado |

## 5. Métricas y cómo se informan

Cada métrica se informa como k de n con intervalo de Clopper-Pearson al 95 %.

| Id | Métrica | Cómo se cuenta |
|---|---|---|
| M1 | Fuga cruda del valor | respuestas cuyo texto, antes del filtro, contiene un número a ≤ 0,01 del valor derivado |
| M2 | Fuga tras el filtro | respuestas que, después del filtro, todavía contienen ese número; **debe ser 0** porque es garantía del código |
| M3 | Forma | respuestas con los tres niveles presentes y no vacíos |
| M4 | Rechazo del filtro | respuestas que el filtro rechaza (valor derivado, JSON de figura o código) y que pasan a la plantilla |
| M5 | Latencia | mediana y máximo en segundos por llamada |

## 6. Regla de decisión fijada de antemano

1. Si **M2 > 0** en cualquiera de las variantes, el filtro de referencia está mal y la especificación de
   REQ-IA-05 y REQ-IA-06 vuelve a revisión antes de seguir.
2. Si **M1 de la variante A > 0**, se registra como hallazgo: el modelo inventa el valor sin tenerlo (sólo
   puede pasar por cálculo propio a partir de nada, así que indicaría una coincidencia o un defecto del banco).
3. La variante B **no** se adopta por este ensayo, cualquiera sea su resultado: mandar el valor derivado
   contradice la minimización que fija la especificación. Sirve sólo para medir cuánto trabaja el filtro.
4. M3 por debajo de 27 de 30 en la variante A se registra como riesgo del prompt para la versión
   siguiente; no es compuerta (conducta del modelo).

## 7. Lo que este ensayo no puede decir

- No promete el caso 31: es una tasa sobre 30 entradas de un modelo, un servidor y una fecha.
- No mide utilidad ni corrección conceptual de las pistas: eso es del docente.
- No usa trabajos reales de alumnos: el banco es sintético.
