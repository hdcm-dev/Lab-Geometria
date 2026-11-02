# ADR-14008 — La capa de IA es una carpeta de Infrastructure con prueba de arquitectura, y no un proyecto

**Producto:** Fábrica de Geometría
**Documento:** ADR-14008-La-Capa-De-IA-Es-Una-Carpeta-Con-Prueba-De-Arquitectura.md
**Versión:** 1.0
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 007), por delegación del Product Owner (actuación 005)
**Fecha:** 2026-11-02
**Autor:** Mesa de construcción de la capa de IA
**Nivel:** Producto
**Origen:** ADR-IA-03 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md), asentada con número del repositorio
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md)

---

## 1. ASR que la origina

REQ-IA-01 y REQ-IA-02.

## 2. Contexto

Las capas ya son proyectos, pero Persistence y la pieza de IA comparten el ensamblado de Infrastructure, y `DomainReferencesNoThirdPartyLibrary` admite todo prefijo `Microsoft`, incluido el SDK de IA.

## 3. Alternativas

(a) proyecto nuevo `GeometriaFactory.Infrastructure.AI`: lo impone el compilador y suma un nodo a la solución (DC-5); (b) **carpeta `Infrastructure/AI/` con prueba de arquitectura**.

## 4. Decisión

(b), con dos pruebas nuevas: `DomainReferencesNoAiSdk` (no relaja la vigente) y `AiSdkOnlyInInfrastructureAiAndComposition`.

## 5. Consecuencias

Piloto sin cambiar la solución; se pierde la garantía del compilador dentro de Infrastructure y la reemplaza una prueba que se mutó a mano para verla en rojo.

## 6. Deja de valer si

La capa pasa de A0 a herramientas o agentes: entonces (a).

## 7. En el código

`tests/GeometriaFactory.Domain.Tests/DependencyGateTests.cs`, `tests/GeometriaFactory.Integration.Tests/AiLayerArchitectureTests.cs`.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-11-02 | Emisión: asienta ADR-IA-03 con número del repositorio, por la resolución de la actuación 007 del expediente 0002 | Mesa de construcción de la capa de IA |
