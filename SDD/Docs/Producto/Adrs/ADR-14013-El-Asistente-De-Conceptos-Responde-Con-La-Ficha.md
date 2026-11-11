# ADR-14013 — El asistente de conceptos responde con la ficha; el modelo es opcional y sólo redacta sobre ella

**Producto:** Fábrica de Geometría
**Documento:** ADR-14013-El-Asistente-De-Conceptos-Responde-Con-La-Ficha.md
**Versión:** 1.0
**Estado:** **Propuesta**, pendiente de firma del Product Owner (ACT-ARQ)
**Fecha:** 2026-11-11
**Autor:** implementador del asistente de conceptos (agente), banco de traslado de los conocimientos de la capa de IA
**Nivel:** Producto
**Trazabilidad upstream:** [`ai-layer/06-Especificacion-Asistente-De-Conceptos.md`](../../../../ai-layer/06-Especificacion-Asistente-De-Conceptos.md) · [expediente 0004](../../../Expedientes/0004-Asistente-De-Conceptos/README.md)

---

## 1. ASR que la origina

rama «en parte» del árbol de decisión; sonda del banco: el modelo parafrasea la ficha y no la supera (5/6, 4/6, 4/6).

## 2. Decisión

Contestar con la **ficha de concepto** versionada en `docs-as-code/ayuda/conceptos/` y embebida al compilar. El modelo sólo redacta sobre la ficha cuando se enciende `AI__ConceptsModelWriter`, y únicamente para medir si supera a la ficha en la lectura docente; si no la supera, queda apagado (mismo criterio que la V1 de pistas, actuación 012 del expediente 0002).

## 3. Alternativas

Que el modelo redacte siempre (descartado: la sonda no lo mostró mejor y agrega el riesgo LLM09); RAG con vectores (descartado: nueve fichas no justifican índice).

## 4. Consecuencias

Positivas y de control: el corpus es el trabajo (ARQ-4): la cobertura término ★ → ficha es compuerta (`cobertura_conceptos.py`); el modelo nunca recibe el trabajo, la figura ni sus valores (REQ-IA-15). La capacidad nace apagada (`AI__ConceptsEnabled=false`) y fusionar no la enciende:
el encendido es del Product Owner después de la aceptación (D-6).
