# ADR-14006 — La llamada al modelo vive detrás de un puerto propio, en `Infrastructure/AI`

**Producto:** Fábrica de Geometría
**Documento:** ADR-14006-La-Llamada-Al-Modelo-Vive-Detras-De-Un-Puerto-Propio.md
**Versión:** 1.0
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 007), por delegación del Product Owner (actuación 005)
**Fecha:** 2026-11-02
**Autor:** Mesa de construcción de la capa de IA
**Nivel:** Producto
**Origen:** ADR-IA-01 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md), asentada con número del repositorio
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md)

---

## 1. ASR que la origina

REQ-IA-03 y REQ-IA-04.

## 2. Contexto

V1 necesita texto de un modelo y el validador, corazón del producto, no puede depender de la red (G-6).

## 3. Alternativas

(a) un método asincrónico nuevo en `IFigureValidator`: rompe G-6; (b) llamar al modelo desde la Web: rompe RA-02; (c) **puerto propio por capacidad en Application, adaptador en `Infrastructure/AI`**.

## 4. Decisión

(c): `IObservationHintWriter` en `Application/Ports/`, `ModelObservationHintWriter` en `Infrastructure/AI/`. El puerto de V2 (`ICommentDraftWriter`) queda reservado mientras V2 esté diferida.

## 5. Consecuencias

Se gana aislamiento y pruebas sin modelo; se suma un puerto (cinco en `CompositionRoot.DeclaredPorts`).

## 6. Deja de valer si

Una capacidad necesita que el modelo decida algo que hoy decide el validador.

## 7. En el código

`FigureValidatorPortStaysSynchronous`, `HintRequest_CarriesOnlyTypeFieldDeclaredDirection`.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-11-02 | Emisión: asienta ADR-IA-01 con número del repositorio, por la resolución de la actuación 007 del expediente 0002 | Mesa de construcción de la capa de IA |
