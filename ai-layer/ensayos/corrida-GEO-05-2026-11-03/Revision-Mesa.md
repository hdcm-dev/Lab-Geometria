# Revisión de la mesa · GEO-05 · prompt v2 · `qwen3.5:35b-a3b`, `think=false` · 2026-11-03

> **Puntuación de la mesa de evaluación (agentes), NO del docente.** No cierra REQ-IA-13: la columna
> «Docente» queda vacía para la firma docente.

## Índice

- [1. Condiciones](#1-condiciones)
- [2. Latencia y precarga](#2-latencia-y-precarga)
- [3. Planilla](#3-planilla)
- [4. Comparación](#4-comparación)
- [5. Lectura según la regla de 01 §5.3](#5-lectura-según-la-regla-de-01-53)

## 1. Condiciones

Las de GEO-04 sin cambio salvo el modelo: banco determinista de 30 casos (`../banco.json`), variante A,
prompt v2 candidato de `01` §5.2 (`PISTAS_MODELO=qwen3.5:35b-a3b pistas.py --prompt v2`), digest
`72aac7c28be8`, `think=false`, temperatura 0,2, semilla 7, `num_ctx` 4096, 400 tokens, esquema de tres campos.
30 llamadas en serie de 21:34 a 21:42 (-03), un solo modelo en uso. Rúbrica: la de GEO-04 (`01` §5.2), voseo
aparte y sin sumar; marca **falsa** (geométricamente incorrecta o no comprueba nada) o **ambigua** (admite una
lectura que lleva al error). Texto crudo: `registro-A-v2.jsonl`; cifras: `resumen.txt`.

Filtro: **30/30 aceptadas, 0 fugas del valor**, ninguna falla del modelo.

## 2. Latencia y precarga

| Medida | Valor | Tope (`AI__TimeoutSeconds`) |
|---|---|---|
| frío (caso 1, modelo descargado) | 35,7 s | 20 s: **lo supera** |
| caliente p50 (casos 2 a 30) | 16,3 s | |
| caliente p95 | 20,6 s | **lo supera** |
| máximo en caliente | 21,8 s (C29) | |
| pedidos por encima de 20 s | 3 (el frío, C29 y C30) | |

**Precarga.** Con 23 GB el frío no entra en el tope, como se esperaba: sólo es usable si el servicio de datos
precarga el modelo al arrancar (un pedido vacío con `keep_alive: -1`, ADR-IA-07) y `AI__KeepAlive=-1` lo
retiene. Aun precargado, **2 de 29 pedidos en caliente pasan de 20 s** y el p95 queda en 20,6 s: en el
servidor sólo CPU el tope no se sostiene ni en caliente. Esos pedidos recibirían la plantilla (degradado,
REQ-IA-06), con 20 s de espera perdidos. Con tres observaciones por pedido la espera en caliente sería ≈ 49 s.

## 3. Planilla

| Caso | Figura · campo | Juez | Voseo | Marca | Docente | Nota de la mesa |
|---|---|---|---|---|---|---|
| C01 | Cubo · Area | 3 | sí | — | | nivel 3 dicta la operación («multiplicando por seis lo que corresponde a una sola cara»): correcta, pero viola la regla 7 |
| C02 | Cubo · Area | 3 | sí | — | | igual que C01: correcta, dicta la operación |
| C03 | Cubo · Area | 3 | sí | — | | nivel 3 dicta la fórmula entera (lado por sí mismo, seis veces) |
| C04 | Cubo · Area | 3 | sí | — | | nivel 3 «el valor de una cara por la cantidad de caras»: correcto, dicta la operación |
| C05 | Cubo · Area | 3 | sí | — | | igual que C01 |
| C06 | Cubo · Volumen | 3 | sí | falsa | | nivel 2 «elevar al cuadrado» para el volumen: falso; nivel 3 «por sí misma dos veces» no lo corrige |
| C07 | Cubo · Volumen | 3 | sí | ambigua | | nivel 3 «elevala a esa potencia tres veces consecutivas»: se lee como potencia de potencia |
| C08 | Cubo · Volumen | 3 | sí | — | | nivel 3 con frase rota («elevala a tres veces») salvada por el paréntesis correcto; dicta la operación |
| C09 | Ortoedro · Volumen | 4 | sí | — | | nivel 1 adelanta la operación («multiplicando las tres dimensiones»); nivel 3 comprobación válida (cada medida una vez) |
| C10 | Ortoedro · Volumen | 3 | sí | — | | niveles 2 y 3 dictan la operación completa |
| C11 | Ortoedro · Volumen | 4 | sí | — | | nivel 2 dicta la operación; nivel 3 comprobación válida |
| C12 | Ortoedro · Volumen | 4 | sí | — | | nivel 2 dicta la operación; nivel 3 comprobación válida («¿te faltó alguna medida?») |
| C13 | Ortoedro · Area | 4 | sí | — | | nivel 3 correcto (pares de dimensiones por cara, sumar) pero cerca de dictar la operación |
| C14 | Ortoedro · Area | 3 | sí | falsa | | nivel 3 presenta como error «la suma de tres pares por dos», que es la fórmula correcta: falso |
| C15 | Ortoedro · Area | 2 | no | falsa | | nivel 2 «seis caras distintas» (son tres pares iguales); nivel 3 «elevás ese resultado al cuadrado»: falso; además tutea |
| C16 | Ortoedro · Area | 4 | sí | — | | correcto; nivel 3 cerca de dictar la operación |
| C17 | Cilindro · Area | 4 | sí | — | | nivel 2 nombra las tres partes (dos bases y lateral): idea correcta, casi la fórmula |
| C18 | Cilindro · Area | 5 | sí | — | | correcta y sin operación: comprobación por partes |
| C19 | Cilindro · Area | 5 | sí | — | | correcta y sin operación |
| C20 | Cilindro · Volumen | 3 | sí | — | | nivel 1 cita el valor declarado (no el derivado); nivel 3 dicta la operación y cierra con «hacer contacto con la altura» |
| C21 | Cilindro · Volumen | 3 | sí | — | | nivel 1 adelanta la fórmula (área de la base por altura); nivel 3 dicta |
| C22 | Cilindro · Volumen | 4 | sí | — | | correcta; nivel 3 una comprobación puntual |
| C23 | Rectangulo · Area | 4 | sí | — | | correcta; nivel 3 contrasta sumar con multiplicar sin cifras |
| C24 | Rectangulo · Area | 4 | sí | — | | nivel 2 dicta la operación (base por altura); nivel 3 válido |
| C25 | Cuadrado · Area | 4 | sí | ambigua | | nivel 2 «multiplicarlo por seis como si fuera un hexágono»: comparación inventada que no es el error del alumno |
| C26 | Cuadrado · Area | 3 | sí | — | | niveles 2 y 3 dictan la operación (lado al cuadrado) |
| C27 | Circulo · Area | 4 | sí | ambigua | | nivel 2 «no solo multiplicar por seis»: el seis no tiene relación con el círculo ni con el error |
| C28 | Circulo · Area | 3 | sí | — | | nivel 3 dicta la fórmula entera (radio al cuadrado por π) |
| C29 | Circulo · Area | 3 | sí | ambigua | | igual que C27 en el nivel 2; nivel 3 dicta la fórmula |
| C30 | Circulo · Area | 3 | sí | — | | nivel 3 dicta la fórmula entera y escribe 3,14 (no es el derivado; el filtro no lo ve) |
| **Media** | | **3,5** | **29/30** | **3 falsas · 4 ambiguas** | | |

## 4. Comparación

| Fuente | Media del juez | Pistas falsas o ambiguas | p95 caliente |
|---|---|---|---|
| plantilla, contrato 1.1 (`01` §5.3) | 4,5 | 0 | inmediata |
| `qwen3.5:35b-a3b`, prompt v1 (10 casos, `../corrida-2026-11-03-completa/`) | 4,2 | — (tutea; nombra la operación) | 14,9 s |
| `qwen3.5:4b`, prompt v2 (GEO-04) | 3,2 | 26 de 30 | 13,5 s |
| `qwen3.5:35b-a3b`, prompt v2 (esta corrida) | 3,5 | 7 de 30 | 20,6 s |

La rúbrica de v1 sumaba el voseo; la de v2 no y además castiga dictar la operación: la caída de 4,2 a 3,5
sale de la regla 7, que el 35b incumple en más de la mitad de los casos. Contra el 4b, el 35b comete muchos menos
errores geométricos (7 contra 26), pero no ninguno.

## 5. Lectura según la regla de 01 §5.3

1. **Regla 5 (voseo):** 29 de 30 (C15 «elevás» escrito «elevas»).
2. **Regla 6 (nivel 1 en el cálculo del campo):** 30 de 30.
3. **Regla 7 (sin operación completa): incumplida** en la mayoría; el 35b dicta la operación correcta donde
   el 4b inventaba una falsa.
4. **Un solo texto falso o ambiguo descarta: hay 7** (C06, C14 y C15 falsas; C07, C25, C27 y C29 ambiguas).
   `qwen3.5:35b-a3b` con el prompt v2 **queda descartado como redactor**; la media (3,5) tampoco llega a 4 ni
   supera a la plantilla (4,5), y el tope de 20 s no se sostiene ni con precarga.
5. **Condición de encendido: NO cumplida.** No falta sólo la firma docente. Recomendación de la mesa: servir
   sólo la plantilla (contrato 1.1) y cerrar la V1 así; `AI:HintsEnabled` sigue en `false` y el modelo de la
   ficha no cambia.
