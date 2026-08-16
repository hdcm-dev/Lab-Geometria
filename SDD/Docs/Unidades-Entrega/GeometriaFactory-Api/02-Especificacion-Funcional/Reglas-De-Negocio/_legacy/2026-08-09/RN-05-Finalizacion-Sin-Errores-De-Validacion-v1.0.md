> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-05-Finalizacion-Sin-Errores-De-Validacion.md`](../../RN-02005-Finalizacion-Sin-Errores-De-Validacion.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-05 — Un trabajo no se finaliza con errores de validación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-05-Finalizacion-Sin-Errores-De-Validacion.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-3 y CL-4), §4 (F-10), §17.2.P.11 punto 2, §21 (cobertura de invariantes y reglas), §20.E-1, §20.E-2, §20.E-5, §20.E-6; [`NB-04`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5; [`NB-05`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) §5
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

Un trabajo con al menos una observación de especie error de validación no se finaliza; un trabajo cuyas observaciones son todas de especie advertencia se finaliza sin impedimento.

## 2. Justificación

Es el límite declarado entre guardar y entregar: el borrador acepta texto que todavía no se puede interpretar, y la entrega exige texto interpretado sin errores (PRODUCT-INTAKE §7, CL-3). La contracara es igual de deliberada: una discrepancia entre el valor declarado y el derivado **no bloquea**, porque bloquear dejaría fuera de la entrega justamente el caso que más interesa observar y el alumno lo viviría como un rechazo del producto en lugar de como información sobre su código (§7, CL-4, y `NB-05` §1).

## 3. Ámbito de aplicación

- Se evalúa en la transición de `Pendiente` a `Finalizado`.
- No se evalúa al guardar ni al enviar: las dos operaciones proceden con errores de validación pendientes.
- No se evalúa sobre las advertencias, en ningún momento.

## 4. Consecuencia si se viola

Rechazo de la finalización, con el motivo `FINALIZACION_CON_ERRORES_DE_VALIDACION`. El trabajo conserva su estado y sus observaciones, y el alumno puede seguir guardándolo como borrador mientras corrige su programa.

En sentido inverso, impedir la finalización de un trabajo cuyas observaciones son todas advertencias también viola esta regla: el carácter no bloqueante de la advertencia es parte del enunciado, no una tolerancia.

## 5. CU afectados

- [CU-08](../../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) — Gobernar el estado del trabajo.
- [CU-07](../../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) — Registrar las observaciones del trabajo, que es donde la especie queda fijada.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08, con los escenarios del intake como entrada: E-1 y E-2 finalizan con advertencias; E-5 no finaliza y sí se guarda como borrador; E-4 finaliza con 0 observaciones; E-6 finaliza porque no hay error de interpretación. Los criterios de éxito de negocio son de `NB-04` §5 —0 trabajos finalizados con errores pendientes— y de `NB-05` §5 —0 trabajos impedidos de guardarse o de entregarse por tener advertencias—.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
