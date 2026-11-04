# ADR-14012 — Modelo chico sin razonamiento, con precarga y permanencia, en el servidor de modelos de la cátedra

**Producto:** Fábrica de Geometría
**Documento:** ADR-14012-Modelo-Chico-Sin-Razonamiento-Con-Precarga-En-El-Servidor-De-La-Catedra.md
**Versión:** 1.1
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 010), por delegación del Product Owner
**Fecha:** 2026-11-03
**Autor:** Mesa de Geometría (relator y especialistas en evaluación de modelos, seguridad de LLM, didáctica y despliegue; agentes)
**Nivel:** Producto
**Origen:** ADR-IA-07 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md)
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` 0.2 §4.3, §4.5 y §4.7 · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md), actuación 010
**Supera:** el punto E de la actuación 009 (`qwen3.5:35b-a3b`, 40 s), que era provisional por evidencia incompleta

---

## 1. ASR que la origina

REQ-IA-10 (tiempo y cupo), REQ-IA-17 (precarga) y REQ-IA-18 (pedido sin razonamiento y con permanencia).

## 2. Contexto

La actuación 009 eligió el modelo con un solo candidato medido y subió el tope a 40 s porque ese modelo
tardaba 33,6 s en frío. La evaluación completa del 2026-11-03 corrió el mismo banco de diez casos sobre diez
configuraciones (siete modelos, con y sin razonamiento donde se puede elegir). Los siete modelos sin
razonamiento pasaron el filtro 10 de 10, inyecciones incluidas; con razonamiento, la familia qwen3.5 devuelve
el contenido vacío. Los datos completos están en `ai-layer/01` §4.7.

## 3. Alternativas

**Modelo.** Cualquiera de los siete que pasan 10/10 sin razonamiento. Los extremos: `qwen3-coder:30b` (el
más rápido en caliente, 10,5 s de p95, pero 18 GB y 28,7 s de frío) y `qwen3.5:4b` (3 GB, 13,3 s de frío,
12,6 s de p95).

**Frío.** (a) Subir el tope hasta cubrirlo (lo que hizo la 009); (b) precargar el modelo y mantenerlo
cargado; (c) elegir un modelo cuyo frío ya entre en el tope.

**Servidor.** API comercial, modelo dentro del contenedor de la Api, servidor con GPU, servidor de la
cátedra. El análisis está en `ai-layer/01` §4.3.

## 4. Criterio

Con la garantía empatada, gana el menor costo de operación: memoria, frío y p95. Y se prefiere la solución
donde el tope por omisión del código (20 s) vale para todos los entornos, porque cada valor que un entorno
pisa es un lugar más donde equivocarse.

## 5. Decisión

1. **Modelo:** `qwen3.5:4b`, **sin razonamiento** (`think=false`). Alternativa: `gemma4:e4b-it-qat`, sin
   razonamiento.
2. **Servidor:** el servidor de modelos de la cátedra, sólo CPU, compartido. Su dirección vive en el secreto
   del despliegue y no se escribe en el repositorio.
3. **Tiempo:** 20 s por llamada en todos los entornos (p95 12,6 s + 50 %, redondeado). Es el valor por
   omisión del código: ningún entorno lo pisa.
4. **Frío:** (b) **y** (c). El modelo elegido ya entra en frío (13,3 s < 20 s); además, el servicio precarga
   el modelo al arrancar (`ModelWarmup`) y cada pedido le pide al servidor que lo mantenga cargado
   (`keep_alive`: `-1` en producción, `30m` por omisión). La precarga es defensa en profundidad hoy y es
   necesaria si alguna vez se vuelve a un modelo grande.
   **Enmienda 1.1 (actuación 013, mesa UX del 2026-11-03):** el tope pasa a **8 s por pedido de pistas**,
   sumadas todas sus llamadas al modelo, y deja de ser «por llamada». Motivo: la Web espera al servicio de
   datos 10 s, y con 20 s por llamada la Web cortaba primero y la alumna recibía un error en lugar de la
   plantilla (REQ-IA-07 roto desde la pantalla, hallazgo F3, 6 de 6 intentos). La regla que rige ahora es
   **el servicio de datos corta antes que la Web**. Consecuencia declarada: con el modelo en CPU compartida
   (p95 12,6 s) la pista sale casi siempre de plantilla; la pista de modelo vuelve cuando GEO-05 fije modelo y
   hardware. Es configuración (`AI__TimeoutSeconds`) y se revierte sin código, siempre por debajo de los 10 s
   de la Web. El punto 4 (frío) queda como defensa en profundidad: con 8 s el frío ya no entra, y la precarga
   pasa a ser necesaria.
5. **Puesta en uso:** el flag de la Web sigue apagado en producción hasta que el docente puntúe la utilidad
   (REQ-IA-13). Esta ADR decide con qué se enciende, no cuándo.

## 6. Consecuencias

- Se gana: 3 GB en lugar de 23; el frío deja de romper el tope; una sola cifra de tiempo para todo el
  producto; ninguna dependencia de cómo esté configurado el servidor compartido.
- Se pierde: el modelo grande redactaba mejor (puntuación de juez 4,2 contra 2,4 del elegido). Mientras el
  prompt v2 no se mida, la plantilla (4,5) es mejor que cualquiera de los dos, y por eso el flag sigue
  apagado.
- `think=false` deja de ser un comentario en la composición y pasa a ser un requisito con prueba (REQ-IA-18).

## 7. Deja de valer si

Con el prompt v2, el modelo queda por debajo de 4 de 5 en la puntuación docente y otro candidato del banco la
supera; el servidor pasa a GPU (se re-mide el banco entero); o un candidato nuevo iguala la garantía con menos
memoria o menos frío.

## 8. En el código

`AiOptions.KeepAlive`, `Api/Composition/OllamaRequestDefaults`, `Infrastructure/AI/ModelWarmup`,
`Api/Composition/ModelWarmupHostedService`. Pruebas: `ModelWarmupTests` (REQ-IA-17 y REQ-IA-18). Los nombres
entran antes a la Norma de Nomenclatura (`ADR-14010`).

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.1 | 2026-11-03 | Enmienda del punto 3 por la actuación 013 (mesa UX, G1): 8 s por pedido, el servicio de datos corta antes que la Web | Implementador de la capa de IA |
| 1.0 | 2026-11-03 | Emisión: asienta ADR-IA-07 con número del repositorio, por la resolución de la actuación 010 del expediente 0002 | Mesa de Geometría |
