# EXP-0002 · Actuación 010 — Resolución: evaluación completa, modelo de producción, tiempo, precarga y plantillas

| Campo | Valor |
|---|---|
| Tipo | resolucion |
| Fecha | 2026-11-03 |
| Autor | mesa de Geometría: relator y especialistas en evaluación de modelos, seguridad de LLM, didáctica y despliegue (agentes, por delegación del Product Owner) |
| Corrige | actuación 009, punto E (modelo y servidor) y §4 (tiempos del despliegue). El resto de la 009 sigue vigente |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 010 — no se reescribe

---

## Índice

- [1. Por qué hay una actuación nueva](#1-por-qué-hay-una-actuación-nueva)
- [2. Marco](#2-marco)
- [3. Lo que midió la evaluación completa](#3-lo-que-midió-la-evaluación-completa)
- [4. Resoluciones](#4-resoluciones)
- [5. Lo que no se hizo en esta pasada](#5-lo-que-no-se-hizo-en-esta-pasada)
- [6. Lo que queda abierto, con dueño](#6-lo-que-queda-abierto-con-dueño)

## 1. Por qué hay una actuación nueva

La 009 eligió `qwen3.5:35b-a3b` con un solo candidato medido y lo registró como **provisional por evidencia
incompleta**, con su condición de revisión escrita: «otro candidato del banco iguala 10/10 con p95 menor». La
evaluación completa cumplió esa condición. En lugar de reescribir la 009 —que queda como testimonio de cómo
se decidió con lo que había— esta actuación la corrige en los dos puntos que cambian. Es el ciclo *Measure →
Manage* del NIST AI RMF hecho visible: una decisión con dueño, fecha y condición de revisión, y su revisión
cuando la condición se da.

## 2. Marco

| Estándar | Qué se aplicó en esta actuación |
|---|---|
| NIST AI RMF 1.0 | *Measure*: mismo banco, misma rúbrica previa y mismo oráculo para diez configuraciones; frío y caliente por separado. *Manage*: el riesgo de pistas que enseñan mal se trata con plantilla y flag apagado, no se acepta. *Govern*: `ADR-14012` con dueño y condición de revisión |
| OWASP Top 10 LLM 2025 | LLM01: los dos casos de inyección resistidos por los diez candidatos que respondieron. LLM05: el filtro sigue siendo la garantía. LLM09: **observado** por primera vez (dos textos falsos que pasaron el filtro). LLM10: razonamiento apagado como control de consumo |
| ISO/IEC 42001:2023 | controles del Anexo A en forma mínima: evaluación de impacto (A.5), registro de verificación y validación (A.6.2.4: `01` §4.7 y la planilla), decisión documentada de puesta en uso (A.6.2.5: §4, punto 5) |
| Didáctica | andamiaje y retroalimentación formativa (Wood, Bruner y Ross, 1976; Hattie y Timperley, 2007; Shute, 2008) para juzgar las salidas y corregir una plantilla |

## 3. Lo que midió la evaluación completa

Diez casos de Geometría (ocho del banco y dos de inyección), diez configuraciones. Tabla completa en
`ai-layer/01-Especificacion-V1-Pistas.md` §4.7; texto crudo en
`ai-layer/ensayos/corrida-2026-11-03-completa/salidas.jsonl`.

| Hallazgo | Evidencia | Consecuencia |
|---|---|---|
| H-1 · La garantía no discrimina | los siete modelos sin razonamiento pasan el filtro 10/10, inyecciones incluidas | se decide por costo |
| H-2 · El razonamiento rompe la capacidad | `qwen3.5:4b` con razonamiento: 0/10, todas vacías; `qwen3.5:35b-a3b`: 2/10; ~200 s por caso | `think=false` pasa a requisito con prueba (REQ-IA-18) |
| H-3 · El modelo grande no compra garantía | 35 B: 10/10 como el de 4 B, con 23 GB y 33,6 s de frío | se cambia de modelo |
| H-4 · Pasar el filtro no es enseñar | juez: plantilla 4,5; 35 B 4,2; 4 B 2,4; gemma4 2,3 | el flag de producción sigue apagado |
| H-5 · LLM09 observado | `qwen3.5:4b`: «por sí mismo tres veces consecutivas» (cuarta potencia) y «una sola cara cinco veces más» | prompt v2, regla 7; la plantilla es el piso |
| H-6 · Una plantilla dictaba la operación | Círculo · Area, nivel 2: «el radio por el radio» | se corrige (§4, punto 6) |

**Límites declarados.** La revisión de utilidad es de un juez, no del docente; diez casos dan un intervalo
de confianza [0,69; 1] para 10/10, que sirve para descartar, no para afirmar 0 fugas.

## 4. Resoluciones

| # | Resolución | Por qué |
|---|---|---|
| 1 | **Modelo de producción: `qwen3.5:4b` con `think=false`.** Alternativa: `gemma4:e4b-it-qat` con `think=false` | el más chico que pasa la garantía (3 GB), el frío más barato (13,3 s), p95 12,6 s. La alternativa es apenas más rápida en caliente pero ocupa el doble y tarda más en frío (`01` §4.7) |
| 2 | **Servidor de producción: el servidor de modelos de la cátedra, sólo CPU**, compartido; dirección en el secreto del despliegue | costo cero, existe, el modelo entra holgado. Descartados: API comercial (sin presupuesto, credencial y tercero), modelo en el contenedor de la Api (imagen y CPU, contradice `ADR-14007`), GPU (no hay; es la condición de revisión) — `01` §4.3 |
| 3 | **Tiempo por llamada: 20 s en todos los entornos**, el valor por omisión del código. Se retiran los 40 s de la 009 | regla del banco: p95 + 50 %, redondeado (12,6 → 18,9 → 20). El frío del modelo elegido entra; los 40 s sólo existían para el frío del modelo grande |
| 4 | **Precarga al arrancar y permanencia en cada pedido**: `ModelWarmup` en segundo plano (tope 120 s), `AI__KeepAlive=-1` en producción y `30m` por omisión | el frío nunca lo paga un alumno; la permanencia viaja en el pedido para no depender de la configuración de un servidor compartido. Reemplaza el «pedido de pistas sintético» que la 009 dejaba al script de despliegue (REQ-IA-17, REQ-IA-18, `ADR-14012`) |
| 5 | **Puesta en uso: `AI__HintsEnabled` sigue en `false` en producción** hasta que el docente puntúe el prompt v2 con `qwen3.5:4b` y la media sea ≥ 4 y mayor que la de la plantilla | H-4 y H-5. Encender es una decisión de producto con evidencia (ISO/IEC 42001 A.6.2.5), no de infraestructura |
| 6 | **Plantillas: el texto de la 009 queda definitivo salvo Círculo · Area**, cuyo nivel 2 y nivel 3 se reescriben (`01` §5.3, `TemplateHints.cs`) | H-6: el nivel 2 decía la operación con palabras. El nuevo nombra la idea y propone un experimento (duplicar el radio) que el alumno corre con su programa |
| 7 | **Prompt v2 candidato** (reglas 5 a 7: voseo; nivel 1 apunta al cálculo del campo; nivel 3 sin la operación) | cada regla responde a un defecto medido; no entra al código sin la corrida rotulada (D-NIVEL3) |
| 8 | **V1 en el intake: F-27 pasa al bloque Could Have** de la tabla de §4, y el intake sube a 5.2 con su fila de control de cambios | la 009 la insertó entre las Must Have; la tabla se ordena por prioridad. El texto de la fila no nombra modelo ni servidor: eso es de la especificación y de `ADR-14012` |
| 9 | **Lo que no viaja al repositorio:** el informe de la evaluación y su arnés no se publican; sólo el resumen de Geometría (`01` §4.7) y sus salidas | el informe mezcla datos de otro sistema y del servidor (dirección, puerto); el repositorio es público (R-PRIV) |

## 5. Lo que no se hizo en esta pasada

No se compiló ni se corrió ninguna prueba: el entorno de contenedores estaba detenido. El código propuesto
(`AiOptions`, `OllamaRequestDefaults`, `ModelWarmup`, `ModelWarmupHostedService`, `ModelWarmupTests`, la
plantilla corregida) se entrega como borrador para que el implementador lo aplique, lo compile y corra la
batería. Punto a verificar al compilar: el tipo de `ChatRequest.KeepAlive` en OllamaSharp 5.5.0 (`01` §4.5).
Los cuatro identificadores nuevos entran antes a la Norma de Nomenclatura (`ADR-14010`).

## 6. Lo que queda abierto, con dueño

| Qué | Dueño | Cierra con |
|---|---|---|
| Aplicar el código propuesto y correr la batería | implementador | CI en verde con REQ-IA-17 y REQ-IA-18 |
| Filas de la Norma de Nomenclatura para `KeepAlive`, `OllamaRequestDefaults`, `ModelWarmup`, `WarmupOutcome`, `ModelWarmupHostedService` | implementador | Norma 1.34, en el mismo cambio |
| Corrida rotulada de 30 casos con `qwen3.5:4b`, prompt v1 **y** v2, con 20 s | implementador | E-011: 30/30 Ok sin «fuera de tiempo» con v1; proporción que pasa el filtro con v2 |
| Puntuación docente de la corrida v2 (REQ-IA-13) | docente | media ≥ 4 y mayor que la plantilla |
| Concordancia del juez (REQ-IA-14) | mesa, con la planilla del docente | ≥ 8 de 10 casos con diferencia ≤ 1 |
| Puertas c, f, g, i y E2E con el flag encendido en el entorno de prueba | implementador | E-012 |
| `AI__HintsEnabled=true` en producción | Product Owner, con lo anterior en verde | cambio de entorno, sin cambio de código |
