> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md`](../../RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-09 — Toda observación de error indica la posición de la pieza y el campo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-09), §5 (historia 4), §7 (CL-3), §21 (RN-09), §20.E-5, §17.4.P.5; [`NB-04`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1 (fallo silencioso)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Enunciado de la regla](#1-enunciado-de-la-regla)
- [2. Justificación](#2-justificación)
- [3. Ámbito de aplicación](#3-ámbito-de-aplicación)
- [4. Consecuencia si se viola](#4-consecuencia-si-se-viola)
- [5. CU afectados](#5-cu-afectados)
- [6. Pruebas que la verifican](#6-pruebas-que-la-verifican)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Enunciado de la regla

Toda observación de especie error de validación atribuible a una figura indica la posición de esa figura en el conjunto raíz y el campo en el que está el defecto. Ninguna observación de error se expresa como un texto genérico.

## 2. Justificación

El alumno no tiene que adivinar dónde falla su salida: la localización del defecto es información de valor didáctico sobre su propio programa (PRODUCT-INTAKE §5, historia 4, y `NB-04` §1). Es además lo que elimina el fallo silencioso, que es el problema que el producto viene a resolver: hoy la figura simplemente no aparece y nadie le dice por qué. La posición reportada tiene que calcularse y no darse por sentada: el escenario E-5 pone a propósito una primera figura válida para que el índice informado sea 1 y no 0.

## 3. Ámbito de aplicación

- Se evalúa al registrar el conjunto de observaciones de un trabajo.
- No se aplica a las observaciones de especie advertencia de discrepancia de valor, que llevan su propia exigencia: el valor declarado y el derivado, los dos.
- Admite la observación no atribuible a ninguna figura —un conjunto raíz vacío, un texto que no parsea ni con tolerancia—, que se registra sin posición de pieza y con el campo que corresponda.
- El mensaje que sale del proceso lleva la posición y el campo y nunca la dirección de un servicio interno; esa parte la sostiene `GeometriaFactory-Contracts` (§17.4.P.5).

## 4. Consecuencia si se viola

Rechazo del conjunto de observaciones, con el código `ERROR_SIN_UBICACION`. El trabajo conserva las observaciones que tuviera y el consumidor debe volver a entregar el conjunto bien formado.

## 5. CU afectados

- [CU-07](../../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) — Registrar las observaciones del trabajo.
- [CU-06](../../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) — Reconstruir el conjunto de piezas, en cuanto a la pieza de tipo desconocido que no se adopta.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08 con el escenario E-5 como caso principal: la observación resultante es de especie error de validación, con posición de pieza 1 y campo `Tipo`, y la pieza de posición 0, que es válida, se interpreta igual. Se agregan los rechazos de una observación de error sin ubicación y de una observación cuya posición de pieza no existe. El criterio de éxito de negocio es de `NB-04` §5: 0 errores de interpretación reportados sin indicar posición de figura y campo.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
