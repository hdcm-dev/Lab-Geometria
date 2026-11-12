# Actuación 005 — P0 del juicio: el 500 por vencimiento del regex

**Fecha:** 2026-11-12 · **Rama:** `fix/juicio-r1` desde `origin/main` `fd5abb4` · **Origen:** actuación 004, plan
priorizado de Geometría, punto 1 (P0).

## 1. El defecto

En la corrida 2 del juicio, «¿Qué es un ortoedro?» (Q04) devolvió `500 UNCLASSIFIED_ERROR`. El registro de la API muestra
una `RegexMatchTimeoutException` lanzada desde `Regex.IsMatch` en el intérprete. Las expresiones de `ConceptRouter`
no tienen retroceso catastrófico, pero llevaban un tope de **250 ms de reloj**. Con el servidor cargado, el reloj
avanza aunque el hilo no corra, y una pregunta de 20 caracteres vencía. La carga del servidor se convertía en un
error de la pregunta.

## 2. Reproducción (rojo)

[E-009](../evidencia/E-009-reproduccion-regex-timeout.txt): `ConceptRouter.Route("¿Qué es un ortoedro?")`, 4000
llamadas en 256 hilos, contenedor limitado a `--cpus=0.1`. Con el código previo vencieron **36 de 4000**.
[E-010](../evidencia/E-010-rojo-lineal.txt): sobre el código previo, la prueba nueva da **15 rojas** (una por cada
expresión con retroceso y tope de reloj).

## 3. El arreglo

- Todas las expresiones del enrutador usan `RegexOptions.NonBacktracking`, un motor de tiempo lineal en la entrada. La
  entrada ya está topeada en 300 caracteres (REQ-IA-21), así que el tiempo queda acotado por construcción y el tope de
  reloj pasa a `Regex.InfiniteMatchTimeout`.
- El motor lineal no admite mirada atrás. `(?<!\bse )\bcalcula` se reemplazó por dos cuentas: hay pedido si hay más
  «calcula» que «se calcula». Se conserva el efecto: «cómo se calcula el área total» sigue siendo concepto.
- Defensa de última línea: `Route` atrapa `RegexMatchTimeoutException` y devuelve la abstención (`sin-ficha`), que no
  resuelve nada. La pantalla siempre recibe una respuesta y nunca un 500.
- `Normalize` usa una expresión estática y lineal en lugar de `Regex.Replace` con tope.

## 4. Verde

- `ConceptRouterLinealTests` (nueva): 15 expresiones lineales y sin reloj, la frase del juez bajo 256 hilos sin
  excepción y siempre con la ficha `ortoedro`, y tres casos de «se calcula» contra «calculá».
- Después del arreglo, el mismo arnés da **0 de 4000** en las tres corridas (E-009).
- `scripts/test.sh` en contenedor ([E-012](../evidencia/E-012-pruebas-act-005.txt)): Domain 95/95, Application 192 + 16 omitidas, Integration 715 + 1 roja de tiempo (`RateLimitingTests`, ráfaga con el host a carga ~50). Repetida sola, da 22/22. No toca el enrutador. La sonda permanente
  (`ConceptRouterSondaTests`) sigue en verde, sin quitar ninguna frase.
- Batería v3 ×3, sin modelo ([`v3-juicio-r1`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-juicio-r1/)):
  24/24 en las tres corridas, 72 de 72 con HTTP 200. Queda pendiente la lectura docente, como en las corridas
  anteriores. La primera corrida 1 chocó con el límite de tasa (4 respuestas 429) por ir pegada a la siembra. Se
  repitió 90 s después.
- Las 25 preguntas del juez ×3 ([E-011](../evidencia/E-011-bateria-del-juez-x3.txt)): **75 de 75 con HTTP 200**, con
  la misma forma y la misma intención que la corrida 1 del juicio. El arreglo no cambia ninguna respuesta.

Instancia propia: `127.0.0.1`, las nueve fichas en `vigente` sólo en una copia fuera del repositorio,
`AI__ConceptsEnabled=true`, `AI__ConceptsModelWriter=false`, sin servidor de modelos y con una clave de firma efímera.

## 5. Preguntas compuestas: no se separan en esta actuación

El enrutador devuelve **una** `ConceptRoute`, y `ConceptAnswer` lleva una forma, una ficha y un destino. Para separar
Q11, Q12, Q13 y Q24 hay que cambiar el contrato de la respuesta (`/v1/conceptos/consultas`), la huella y la consola de
la Web. Eso no es un arreglo del enrutador. Además, en Q12 («qué es el volumen y cuánto vale en mi cubo») hay que
decidir cómo responder la parte de concepto sin debilitar la regla F-G1, que pone la negativa antes de las fichas.
Queda como P1 del plan, para un expediente propio con su caso de batería (Q11, Q12, Q13 y Q24 en una v4).

## 6. Pendiente del PO (sin tocar en esta actuación)

Producción sigue igual. El P0-2 del plan es del PO y va junto:

1. **Fichas a `vigente`** (REQ-IA-41, H-10): en las nueve `docs-as-code/ayuda/conceptos/*.md`, cambiar
   `status: borrador` por `status: vigente` en las que apruebe, por PR.
2. **Encender el asistente**: `AI__ConceptsEnabled=true` en la API **y** en la Web del despliegue. Mientras no haya
   puntuación docente, `AI__ConceptsModelWriter` sigue en `false`.
3. **Publicar `main`** con este arreglo incluido. Desde 2026-11 el despliegue es manual (`workflow_dispatch`).
