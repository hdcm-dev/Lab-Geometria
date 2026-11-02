# ADR-14010 — Los nombres de la capa de IA entran primero a la Norma de Nomenclatura

**Producto:** Fábrica de Geometría
**Documento:** ADR-14010-Los-Nombres-De-La-Capa-De-IA-Entran-Primero-A-La-Norma.md
**Versión:** 1.0
**Estado:** **Aceptada por la mesa** (expediente 0002, actuación 007), por delegación del Product Owner (actuación 005)
**Fecha:** 2026-11-02
**Autor:** Mesa de construcción de la capa de IA
**Nivel:** Producto
**Origen:** ADR-IA-05 de [`ai-layer/03-ADR.md`](../../../../ai-layer/03-ADR.md), asentada con número del repositorio
**Trazabilidad upstream:** `ai-layer/01-Especificacion-V1-Pistas.md` · [expediente 0002](../../../Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md)

---

## 1. ASR que la origina

ninguno de calidad: la regla §6.1 de la Norma de Nomenclatura.

## 2. Contexto

La norma prohíbe escribir un identificador sin fila previa en el glosario.

## 3. Alternativas

(a) escribir los nombres al construir; (b) **asentar las filas antes del primer identificador**.

## 4. Decisión

(b): la tabla §6.27 de la norma (versión 1.32) entra en el commit del tramo T1, anterior a todo cambio en `src/`.

## 5. Consecuencias

Ninguna negativa: es la regla vigente.

## 6. Deja de valer si

Cambia la norma.

## 7. En el código

la historia de la rama: el commit «EXP-0002 T1» precede a todo commit que toque `src/`.

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-11-02 | Emisión: asienta ADR-IA-05 con número del repositorio, por la resolución de la actuación 007 del expediente 0002 | Mesa de construcción de la capa de IA |
