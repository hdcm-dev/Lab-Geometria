> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md`](../../RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-03 — Un trabajo ajeno es indistinguible de uno inexistente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-5, con INV-02), §17.2.P.5, §17.5.P.5; [`NB-03`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5
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

Para un alumno que no es su dueño, un trabajo es indistinguible de un trabajo que no existe: el producto no confirma ni niega su existencia, y responde lo mismo en los dos casos.

## 2. Justificación

Es la respuesta declarada del cliente al caso límite de un alumno que pide por dirección el trabajo de otro: devuelve «no encontrado» y no «no autorizado», porque «no autorizado» confirma que el recurso existe (PRODUCT-INTAKE §7, CL-5). La regla materializa INV-02 y se verifica del lado del servidor, no ocultando un control en la pantalla.

## 3. Ámbito de aplicación

- Se evalúa en toda consulta de un trabajo por su identificador.
- Se evalúa antes de toda operación de reedición o de eliminación.
- El dominio devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`; la traducción a la respuesta que sale del proceso pertenece a `GeometriaFactory-Api`, que debe conservar la indistinguibilidad.

## 4. Consecuencia si se viola

Rechazo, con la forma exacta del rechazo como parte de la regla: la respuesta ante un trabajo ajeno debe ser idéntica a la respuesta ante un trabajo inexistente. Un mensaje, un código o un tiempo de respuesta que permitan distinguirlos violan la regla aunque la operación no se haya ejecutado.

## 5. CU afectados

- [CU-09](../../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) — Resolver el acceso de un alumno a un trabajo.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: consulta de un trabajo del alumno A por parte del alumno B, con resultado idéntico al de una consulta sobre un trabajo inexistente. La verificación equivalente **forzando la petición al servicio de datos**, y no sólo desde la pantalla, es criterio bloqueante declarado aguas arriba (PRODUCT-INTAKE §17.5.P.6) y pertenece a las pruebas de integración de `GeometriaFactory-Api`. El criterio de éxito de negocio es de `NB-03` §5: 0 operaciones que procedan sobre trabajos de otro alumno.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
