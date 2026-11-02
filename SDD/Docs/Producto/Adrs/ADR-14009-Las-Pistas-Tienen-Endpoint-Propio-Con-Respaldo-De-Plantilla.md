# ADR-14009 — Las pistas tienen endpoint propio, con respaldo de plantilla, y A-18 no cambia

**Producto:** Fábrica de Geometría
**Documento:** ADR-14009-Las-Pistas-Tienen-Endpoint-Propio-Con-Respaldo-De-Plantilla.md
**Versión:** 1.0
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 007), por delegación del Product Owner (actuación 005)
**Fecha:** 2026-11-02
**Autor:** Mesa de construcción de la capa de IA
**Nivel:** Producto
**Origen:** ADR-IA-04 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md), asentada con número del repositorio
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md)

---

## 1. ASR que la origina

REQ-IA-07 y REQ-IA-08.

## 2. Contexto

A-18 (`POST /v1/interpretaciones`) previsualiza sin guardar y es sincrónico; un modelo sólo CPU tarda decenas de segundos.

## 3. Alternativas

(a) sumar pistas a A-18: le cambia contrato y latencia; (b) **endpoint propio `POST /v1/interpretaciones/pistas`**.

## 4. Decisión

(b), con plantilla por par (figura, campo) para *degradado* y *rechazado*; sin cola ni segundo plano. **Con el flag apagado la ruta existe y responde 200 sólo con plantilla** (decisión D-FLAG, 4-1); la Web no la llama mientras `AI:HintsEnabled` sea `false`, que es el valor por omisión.

## 5. Consecuencias

A-18 queda aislado; la Web hace un pedido aparte.

## 6. Deja de valer si

La latencia de PROD hace preferible un segundo plano.

## 7. En el código

`InterpretationsEndpoint_DoesNotUseHintPort`, `HintPort_FallsBackToTemplate`.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-11-02 | Emisión: asienta ADR-IA-04 con número del repositorio, por la resolución de la actuación 007 del expediente 0002 | Mesa de construcción de la capa de IA |
