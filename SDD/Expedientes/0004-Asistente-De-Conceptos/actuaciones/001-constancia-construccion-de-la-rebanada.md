# EXP-0004 · Actuación 001 — Constancia: construcción de la rebanada «Preguntá un concepto»

| Campo | Valor |
|---|---|
| Tipo | constancia |
| Fecha | 2026-11-11 |
| Autor | banco de traslado de Lab-Documentos (agente) |
| Corrige | — |

**Expediente:** [EXP-0004](../README.md) · **Foliada:** 001 — no se reescribe

## 1. Qué quedó

- **Aplicación.** `ConceptRouter` (función pura: inyección → tema ajeno → **pide resolver** → operar → fichas),
  `AskConceptUseCase` (filtro por rol antes de elegir, abstención sin llamar al modelo, cifras del mismo caso de uso que
  el listado), `DestinationCatalog` (id del catálogo, revalidación por instancia; el docente nunca recibe `/editar`).
- **Infraestructura.** `DocsAsCodeConceptCorpus` (sólo sirve `status: vigente`), `AiEnablement` (motivo exacto de
  habilitación, nombra la clave), `ModelConceptAnswerWriter` + `ConceptAnswerFilter` (redacción opcional y filtrada),
  tabla `HuellaDeConsulta` por migración `ConceptTrace` (siete columnas, sin texto ni persona).
- **Servicio y Web.** `POST /v1/conceptos` (404 apagado), motivo en `/salud` y `/estado`, `ConceptPanel` en la carga y
  en la vista del trabajo, estilos en `wwwroot/css/app.css`.
- **Corpus.** Nueve fichas en `docs-as-code/ayuda/` con control `docs-as-code/controles/cobertura_conceptos.py`; todas en
  `borrador` hasta la firma del PO.
- **Norma.** `ADR-14013` (la ficha es la respuesta), `ADR-14014` (enrutado y frontera son código puro), `ADR-14015`
  (el motivo de habilitación se ve en estado); norma de nomenclatura, contratos REST y OpenAPI actualizados.

## 2. Cifras medidas

| Medida | Valor | Evidencia |
|---|---|---|
| Pruebas | 874 en verde, 1 omitida (95 dominio · 90 aplicación · 689 integración); base `53a2810`: 788 | E-001 |
| Batería ×3 contra el modelo | 24/24, 24/24, 24/24 (umbral ≥ 22/24) | E-002 |
| Latencia con modelo, p95 caliente | 4,69 a 4,81 s (umbral ≤ 8 s) | E-002 |
| Respuestas del modelo con dígitos | 0 de 15 | E-002 |
| Frío | no medido en esta corrida: el modelo ya estaba en memoria; vale el de la sonda (109 s) | E-002 |

## 3. Apartamientos declarados

1. **Corpus de ensayo.** Con las fichas en `borrador`, el servicio real se abstiene de todo concepto. La batería corrió
   sobre una publicación aparte con las nueve fichas puestas en `vigente` **fuera del repositorio**; el repositorio no
   cambia de estado ninguna ficha sin la firma del PO.
2. **Canario.** Como la ficha es la respuesta (ADR-14013), el texto de una ficha sembrada se muestra como dato; la
   prueba `Corpus_Canario_NoSeObedece` verifica que no aparezca la salida del modelo que obedece la instrucción, no que
   el texto de la ficha omita la palabra.
3. **Sin PR.** La rama queda empujada sin PR, por encargo del banco.
