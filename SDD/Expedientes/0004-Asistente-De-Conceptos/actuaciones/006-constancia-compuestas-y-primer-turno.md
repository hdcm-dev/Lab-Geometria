# Actuación 006 — P1 del juicio: compuestas parte por parte y respuesta al primer turno

**Fecha:** 2026-11-12 · **Rama:** `fix/juicio-r1` (sigue a la [005](005-constancia-p0-sin-500-por-regex.md)) · **Origen:**
actuación 004, plan de Geometría, puntos 3 y 4 (R1 compuestas 0/8, R3 = 0 con 16/25).

## 1. Qué medía el juez

- **R1.** Las ocho compuestas de la batería (Q11, Q12, Q13, Q24, N01, N02, N03, N09) salían con una sola intención.
  En Q12 y N03 la negativa tapaba también la parte de concepto (H3).
- **R3.** Además de las compuestas, fallaban al primer turno: Q14 y Q23 (no usaban el trabajo abierto, H5), Q22
  (pedía una cifra y recibía la ficha del estado de un trabajo), Q18 (programar en C# se rechazaba como «parte del
  ejercicio», H6) y Q25 (el docente recibía la negativa escrita para el alumno, H4/H6).

## 2. El arreglo

- **Partición** (`ConceptRouter.SplitParts` y `RouteParts`). Se parte en cada «?» que cierra una pregunta y en cada
  «y»/«e» seguido de una palabra que abre otra («y dónde», «y qué», «y calculame»). Una enumeración («largo, ancho y
  altura», «área total y área de una cara») no se parte. «Si la arista mide 4, ¿el volumen…?» es una sola pregunta.
- **La frontera no se debilita.** Lo que entero es inyección, tema ajeno, lo de al lado o sobre el tope no se parte.
  Si la pregunta entera pide resolver y ninguna parte lo pide sola, vale la negativa entera. La prueba
  `Sonda_LaParticionNoCambiaElLadoDeLaFrontera` recorre todas las frases de las pruebas del enrutador: ninguna cambia
  de lado al partirse.
- **Respuesta compuesta.** `ConceptAnswer.Parts` y `ConceptAnswerResponse.Parts` (opcional): forma `Compound`, una
  respuesta por parte con su forma, su ficha y su chip. La parte de resolver recibe la negativa y la otra se responde.
  La huella se escribe por parte (sin texto). La consola de la Web dibuja cada parte con su ficha y su chip.
- **Intenciones nuevas**, todas sin modelo: `operar:sacar-advertencia` (qué hacer, sin el valor, con «Ver pistas»),
  `operar:cuantos-pendientes` (cifra de cuentas de alumnos en Pendiente, del repositorio de cuentas, sólo docente) y
  `operar:habilitar-alumno` (chip nuevo `cuentas` → `/cuentas`, sólo docente).
- **Trabajo abierto.** «¿Por qué mi/este trabajo tiene una advertencia?» con trabajo en pantalla agrega a la ficha
  el supuesto dicho y los campos con advertencia, sin valores; chip «Ver pistas» para el alumno.
- **Voz del docente.** Su negativa dice que corregir o aprobar lo decide él y lleva a la entrega de la comisión.
- **Programar en C#** («escribime un programa en C#») va a lo de al lado, con texto que ya no dice «parte del ejercicio».

## 3. Rojo y verde

- **Rojo:** con `3207b52` las 8 compuestas salen con una intención ([E-011](../evidencia/E-011-bateria-del-juez-x3.txt)
  y la columna «antes» de [E-013](../evidencia/E-013-bateria-del-juez-compuestas-x3.txt)).
- **Verde:** `ConceptRouterCompuestasTests` (Application) y, en `ConceptAssistantTests`, `Compuesta_UnaRespuestaPorParte`
  (8 casos de punta a punta) más tres de primer turno (Q14/Q23, Q22, Q18/Q25). Pruebas en
  [E-014](../evidencia/E-014-pruebas-act-006.txt).
- **Batería del juez ×3** (35 preguntas, instancia propia como en la 005): 105 de 105 con HTTP 200, 35 de 35 iguales en
  las tres corridas. Compuestas: **8 de 8 con todas sus partes respondidas**. Cambian además Q14, Q15, Q18, Q22, Q25.
  Ningún pedido disfrazado deja de rechazarse (Q06–Q10, N05, N06).
- **Batería v3 ×3** ([`v3-juicio-r1-compuestas`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-juicio-r1-compuestas/)):
  24/24 en las tres corridas. Queda pendiente la lectura docente, como en las corridas anteriores.

El puntaje R1–R14 lo vuelve a poner el juez: esta constancia no se autocalifica.

## 4. Lo que no se hizo

- No se tocaron las fichas (siguen en `borrador`) ni el encendido: es el P0-2 del PO (actuación 005 §6).
- Q17 y Q19 (abstención) siguen sin chip, y N10 («¿qué es un cubo?») sin ficha: P2 del plan.
- No hay capturas de la Web nueva: la medición es sobre la API. Las capturas a 1280 y 390 van con el despliegue (R13).
