# ADR-14014 — El enrutado y la frontera son código puro; el pedido de resolver es negativa útil antes de buscar

**Producto:** Fábrica de Geometría
**Documento:** ADR-14014-El-Enrutado-Y-La-Frontera-Son-Codigo-Puro.md
**Versión:** 1.0
**Estado:** **Propuesta**, pendiente de firma del Product Owner (ACT-ARQ)
**Fecha:** 2026-11-11
**Autor:** implementador del asistente de conceptos (agente), banco de traslado de los conocimientos de la capa de IA
**Nivel:** Producto
**Trazabilidad upstream:** [`ai-layer/06-Especificacion-Asistente-De-Conceptos.md`](../../../../ai-layer/06-Especificacion-Asistente-De-Conceptos.md) · [expediente 0004](../../../Expedientes/0004-Asistente-De-Conceptos/README.md)

---

## 1. ASR que la origina

límite didáctico (`ADR-10010`, RD-01): `Describir()` lo arma el alumno a mano a propósito.

## 2. Decisión

`ConceptRouter` es una función pura que decide anillo e intención en este orden: inyección, tema ajeno, lo de al lado, **pedido de resolver**, operar y fichas. Todo pedido del valor, la fórmula, el JSON, el código o la corrección es **negativa útil con texto fijo**, sin modelo, aunque nombre un concepto con ficha. Sólo el Product Owner puede pasar una intención a otro anillo, por escrito.

## 3. Alternativas

Dejar que el modelo decida (descartado: con un modelo chico la elección varía entre corridas y no se prueba por tabla); buscar la ficha primero (descartado: «¿cuánto da el área de mi cubo?» contestaría el concepto y abriría la puerta a la operación).

## 4. Consecuencias

Positivas y de control: la frontera se prueba en cada build con la tabla de 24 casos y diez formulaciones de «resolveme» con un doble de `IChatClient` que no recibe ningún pedido. La capacidad nace apagada (`AI__ConceptsEnabled=false`) y fusionar no la enciende:
el encendido es del Product Owner después de la aceptación (D-6).
