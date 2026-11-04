# Revisión de la mesa · GEO-04 · prompt v2 · `qwen3.5:4b`, `think=false` · 2026-11-03

> **Puntuación de la mesa de evaluación (agentes), NO del docente.** No cierra REQ-IA-13: la columna
> «Docente» queda vacía para la firma docente. Sirve para REQ-IA-14 cuando el docente la complete.

## Índice

- [1. Condiciones](#1-condiciones)
- [2. Rúbrica](#2-rúbrica)
- [3. Planilla](#3-planilla)
- [4. Comparación](#4-comparación)
- [5. Lectura según la regla de 01 §5.3](#5-lectura-según-la-regla-de-01-53)

## 1. Condiciones

Banco determinista de 30 casos (`../banco.json`), variante A (sin el valor derivado), prompt v2 candidato de
`01` §5.2 (`pistas.py --prompt v2`), `qwen3.5:4b` digest `d8b0f5e9760c`, `think=false`, temperatura 0,2,
semilla 7, `num_ctx` 4096, 400 tokens de salida, esquema de tres campos. 30 llamadas en serie de 21:13 a
21:19 (-03), con el servidor sin otra carga. Texto crudo: `registro-A-v2.jsonl`; cifras: `resumen.txt`.

Filtro: **30/30 aceptadas, 0 fugas del valor**, ninguna falla del modelo; latencia frío 14,1 s, p50 11,4 s,
p95 13,5 s, máximo 14,1 s, **0 pedidos por encima del tope de 20 s**.

## 2. Rúbrica

La de `01` §5.2 para GEO-04, escrita antes de correr: 0 a 5 por caso, 0 si el filtro rechaza: +1 los tres
niveles presentes; +1 el nivel 1 dice dónde mirar (el cálculo del campo); +1 el nivel 2 nombra la idea
geométrica correcta; +1 el nivel 3 da una comprobación sin cifras y sin la operación completa; +1 no revela
la respuesta ni la operación completa en ningún nivel (sólo didáctico). El voseo va en su columna y **no
suma**. La columna «Marca» señala la pista **falsa** (dice algo geométricamente incorrecto o que no comprueba
nada) o **ambigua** (admite una lectura que lleva al error).

## 3. Planilla

| Caso | Figura · campo | Juez | Voseo | Marca | Docente | Nota de la mesa |
|---|---|---|---|---|---|---|
| C01 | Cubo · Area | 4 | sí | ambigua | | nivel 3 «repetí cuántas veces hay que multiplicarlo [el lado]» empuja a potencias del lado, no a sumar caras |
| C02 | Cubo · Area | 3 | sí | falsa | | nivel 3 «tomá el valor que obtuviste y multiplicalo por seis»: el declarado ya suma cuatro caras; dicta además la operación |
| C03 | Cubo · Area | 3 | sí | falsa | | nivel 3 «el valor por lado, multiplicalo seis veces»: se lee como l·6 o l⁶, ninguno es el área |
| C04 | Cubo · Area | 3 | sí | — | | nivel 3 correcto pero dicta la operación completa («una sola cara por seis») |
| C05 | Cubo · Area | 5 | sí | — | | nivel 3 comprobación válida: dividir lo declarado por seis y comparar con una cara |
| C06 | Cubo · Volumen | 3 | sí | falsa | | volumen: «repetí esa operación seis veces, luego sumá» confunde volumen con área |
| C07 | Cubo · Volumen | 2 | sí | ambigua | | nivel 3 «por sí misma dos veces más antes del producto final»: no se sabe cuántos factores quedan |
| C08 | Cubo · Volumen | 4 | sí | falsa | | nivel 3 «dividilo por seis, porque un cubo tiene ese número de aristas»: el cubo tiene doce aristas y la división no comprueba el volumen |
| C09 | Ortoedro · Volumen | 3 | sí | falsa | | nivel 2 «sumando… antes de multiplicar por tres»; nivel 3 «sumalo seis veces»: ninguno es el volumen |
| C10 | Ortoedro · Volumen | 4 | sí | falsa | | nivel 3 compara el volumen con «sumar todas las caras» (eso es el área) |
| C11 | Ortoedro · Volumen | 3 | sí | — | | nivel 3 dicta la fórmula entera («largo, ancho y alto, hacé su producto») |
| C12 | Ortoedro · Volumen | 3 | sí | falsa | | nivel 3 «dividilo por seis y comparalo con cada medida»: no comprueba nada del volumen |
| C13 | Ortoedro · Area | 4 | sí | falsa | | nivel 3 «perímetro de la base por la altura y dos veces la suma de sus alturas opuestas»: no son las dos bases; además «ortópedo» |
| C14 | Ortoedro · Area | 4 | sí | ambigua | | nivel 3 «cada parámetro por su cantidad en la figura»: multiplica medidas, no áreas de caras |
| C15 | Ortoedro · Area | 3 | sí | — | | nivel 3 correcto (pares de caras) pero dicta casi toda la operación |
| C16 | Ortoedro · Area | 3 | sí | falsa | | nivel 2 «todas [las caras] tienen el mismo tamaño»; nivel 3 cara cuadrada por seis: falso para un ortoedro |
| C17 | Cilindro · Area | 4 | sí | falsa | | nivel 3 «sumá las bases y multiplicá por la altura»: no es el área total |
| C18 | Cilindro · Area | 2 | sí | ambigua | | nivel 3 escribe una cifra («entre 6.28») sin decir qué comprueba; nivel 2 habla de «restar» áreas |
| C19 | Cilindro · Area | 2 | sí | falsa | | nivel 3 «dividí tu resultado entre 75.40»: 75,40 es el propio valor declarado; la comprobación da 1 y no enseña nada |
| C20 | Cilindro · Volumen | 4 | sí | falsa | | nivel 3 sin sentido («altura por seis veces más una vez que lo hayas sumado al cuadrado del otro dato») |
| C21 | Cilindro · Volumen | 2 | sí | falsa | | nivel 2 «sumando las medidas»; nivel 3 «multiplicala por seis»: falso para el volumen del cilindro |
| C22 | Cilindro · Volumen | 2 | sí | falsa | | nivel 1 manda a «la medida de la altura»; nivel 3 sin sentido («seis veces una tercera parte con cuatro veces otra mitad») |
| C23 | Rectangulo · Area | 3 | sí | ambigua | | nivel 3 «sumá una medida con la otra para tener el perímetro» (eso es medio perímetro); la idea de comparar sirve, el dato no |
| C24 | Rectangulo · Area | 3 | sí | ambigua | | nivel 2 «multiplicarlas por dos lados»; nivel 3 «cuántas veces se suma» empuja a la suma, que es el error |
| C25 | Cuadrado · Area | 3 | sí | ambigua | | nivel 3 «cuántas veces esa cantidad se suma para formar la superficie»: describe el perímetro |
| C26 | Cuadrado · Area | 3 | sí | falsa | | nivel 3 «repetí ese número cuatro veces sumando»: es exactamente el error del alumno (perímetro) |
| C27 | Circulo · Area | 3 | sí | falsa | | nivel 3 «por sí misma y dividila entre cuatro»: r²/4 no es el área |
| C28 | Circulo · Area | 3 | sí | falsa | | nivel 3 «multiplicando la longitud por sí misma y sumándole seis veces ese resultado»: falso |
| C29 | Circulo · Area | 4 | sí | falsa | | nivel 2 correcto (radio y π); nivel 3 «multiplicalo por dos veces seis»: falso |
| C30 | Circulo · Area | 3 | sí | falsa | | nivel 3 «sumalo al diámetro»: no comprueba el área |
| **Media** | | **3,2** | **30/30** | **19 falsas · 7 ambiguas** | | |

## 4. Comparación

| Fuente | Media del juez | Pistas falsas o ambiguas |
|---|---|---|
| plantilla, contrato 1.1 (`01` §5.3) | 4,5 | 0 |
| `qwen3.5:4b`, prompt v1 (10 casos, `../corrida-2026-11-03-completa/`) | 2,4 | 2 de 10 |
| `qwen3.5:4b`, prompt v2 (30 casos, esta corrida) | 3,2 | 26 de 30 |

Las rúbricas no son idénticas (la v1 sumaba el voseo en el punto 5; esta no): la subida de la media sale casi
entera de ese cambio y de la regla 6 (el nivel 1 ahora apunta al cálculo del campo en 29 de 30), no del nivel 3.

## 5. Lectura según la regla de 01 §5.3

1. **La regla 5 funcionó:** voseo en 30 de 30 (con v1, tuteo en 10 de 10).
2. **La regla 6 funcionó:** el nivel 1 nombra el cálculo del campo en 29 de 30 (la excepción, C22, manda a la altura).
3. **La regla 7 no funcionó y empeoró el defecto.** Al prohibirle dictar la operación, el modelo inventa una:
   19 niveles 3 (o 2) **falsos** y 7 **ambiguos**, entre ellos dos con cifras que el filtro no ve porque no
   son el derivado (C18 «6.28», C19 «75.40»). Con v1 el 4b dictaba la operación correcta; con v2 dicta una falsa.
4. **Regla de 01 §5.2-5.3: un solo texto falso o ambiguo descarta, aunque la media suba.** Hay 26.
   `qwen3.5:4b` con el prompt v2 **queda descartado como redactor**, y la media (3,2) tampoco llega a 4 ni
   supera a la plantilla (4,5).
5. **Condición de encendido: NO cumplida.** No falta sólo la firma docente: aunque el docente puntuara alto,
   la regla del texto falso ya descarta. El prompt v2 no entra al código; `AI:HintsEnabled` sigue en `false`.
   Queda para la mesa elegir entre otro candidato con v2 (`qwen3.5:35b-a3b`, 4,2 con v1) o mostrar sólo la
   plantilla (contrato 1.1, 4,5), que hoy es la fuente mejor puntuada.
