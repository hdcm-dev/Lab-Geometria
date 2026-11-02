# ADR-14007 — El acceso al modelo es `IChatClient`, con el adaptador OllamaSharp elegido en la composición

**Producto:** Fábrica de Geometría
**Documento:** ADR-14007-El-Acceso-Al-Modelo-Es-IChatClient-Con-OllamaSharp-En-La-Composicion.md
**Versión:** 1.0
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 007), por delegación del Product Owner (actuación 005)
**Fecha:** 2026-11-02
**Autor:** Mesa de construcción de la capa de IA
**Nivel:** Producto
**Origen:** ADR-IA-02 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md), asentada con número del repositorio
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md)

---

## 1. ASR que la origina

REQ-IA-02 y REQ-IA-15.

## 2. Contexto

El servidor de modelos de PROTO es uno del laboratorio y el de PROD no está decidido (punto E del lote).

## 3. Alternativas

(a) cliente HTTP propio; (b) **`IChatClient` (`Microsoft.Extensions.AI.Abstractions` 10.11.0) con el adaptador elegido en la composición**. Entre los adaptadores: OllamaSharp 5.5.0 o el cliente compatible con OpenAI.

## 4. Decisión

(b) con **OllamaSharp 5.5.0**, versión fija, sin clave (decisión D-ADAPT de la mesa, 5-0). La dirección llega por `AI:Endpoint`, **sin valor por omisión**; sólo se registra el cliente si es un URL absoluto `http` o `https`. Sin dirección, el puerto devuelve *degradado* y el arranque no falla.

## 5. Consecuencias

Cambiar de proveedor toca un solo lugar; cambiar de adaptador **no** prueba igual conducta: el banco se vuelve a correr.

## 6. Deja de valer si

El producto adopta otro contrato común de acceso a modelos, o PROD exige un proveedor con credencial.

## 7. En el código

`NoModelEndpoint_StartsDegraded`, `AiSdkOnlyInInfrastructureAiAndComposition`.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-11-02 | Emisión: asienta ADR-IA-02 con número del repositorio, por la resolución de la actuación 007 del expediente 0002 | Mesa de construcción de la capa de IA |
