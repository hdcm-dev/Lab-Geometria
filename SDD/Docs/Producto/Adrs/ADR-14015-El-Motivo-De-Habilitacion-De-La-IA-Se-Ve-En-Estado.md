# ADR-14015 — El motivo de habilitación de la IA se expone en `/estado` sin dirección ni modelo

**Producto:** Fábrica de Geometría
**Documento:** ADR-14015-El-Motivo-De-Habilitacion-De-La-IA-Se-Ve-En-Estado.md
**Versión:** 1.0
**Estado:** **Propuesta**, pendiente de firma del Product Owner (ACT-ARQ)
**Fecha:** 2026-11-11
**Autor:** implementador del asistente de conceptos (agente), banco de traslado de los conocimientos de la capa de IA
**Nivel:** Producto
**Trazabilidad upstream:** [`ai-layer/06-Especificacion-Asistente-De-Conceptos.md`](../../../../ai-layer/06-Especificacion-Asistente-De-Conceptos.md) · [expediente 0004](../../../Expedientes/0004-Asistente-De-Conceptos/README.md)

---

## 1. ASR que la origina

la IA apagada no se ve en el sitio; en PushDispatch una clave con nombre parecido dejó la IA apagada en silencio.

## 2. Decisión

`/salud` informa `AssistantAi` con un conjunto cerrado —`Habilitada`, `FaltaEndpoint`, `FaltaModelo`, `ApagadaPorParametro`, `ClaveDesconocida`— y la **clave** entre paréntesis; `/estado` lo muestra. Una clave parecida (`AI__Modelo`, raíz `IA`, `AI_<clave>`) se informa primero, por su nombre y nunca por su valor.

## 3. Alternativas

Un booleano «IA encendida» (descartado: no dice qué falta); escribir el motivo sólo en el registro (descartado: nadie lo lee en el sitio).

## 4. Consecuencias

Positivas y de control: nunca la dirección del servidor ni el nombre del modelo en `/salud`, `/estado`, registros ni evidencias (K2 R6); probado con `Estado_MotivoDeLaIa_SinDireccion` y `Clave_Variante_Desconocida`. La capacidad nace apagada (`AI__ConceptsEnabled=false`) y fusionar no la enciende:
el encendido es del Product Owner después de la aceptación (D-6).
