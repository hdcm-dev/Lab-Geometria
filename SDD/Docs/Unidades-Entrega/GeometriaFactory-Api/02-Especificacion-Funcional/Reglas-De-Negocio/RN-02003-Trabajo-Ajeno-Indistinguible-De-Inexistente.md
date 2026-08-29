# RN-02003 — Un trabajo ajeno es indistinguible de uno inexistente

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-02003), §17.1.P.2 · GeometriaFactory-Domain (INV-02), §7 (CL-5), §17.1.P.5 · GeometriaFactory-Application, §17.1.P.5 · GeometriaFactory-Api; [`NB-00003`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5
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

Un alumno **sólo ve y opera sus propios trabajos**. Para un alumno que no es su dueño, un trabajo es indistinguible de un trabajo que no existe: el producto no confirma ni niega su existencia, y responde lo mismo en los dos casos.

## 2. Justificación

Es la regla declarada en PRODUCT-INTAKE §4.1 y la respuesta del cliente al caso límite de un alumno que pide por dirección el trabajo de otro: devuelve «no encontrado» y no «no autorizado», porque «no autorizado» confirma que el recurso existe (§7, CL-5). El invariante que la expresa como condición permanente es **INV-02**: un alumno sólo accede a sus propios trabajos, y no existe consulta que devuelva trabajos de otro alumno a un papel de alumno (§17.1.P.2 · GeometriaFactory-Domain). Se verifica del lado del servidor y no ocultando un control en la pantalla.

## 3. Ámbito de aplicación

- Se evalúa en toda consulta de un trabajo por su identificador, en sus cuatro estados.
- Se evalúa antes de toda operación de reedición o de eliminación por parte de un alumno.
- No alcanza al administrador, cuyo alcance sobre los trabajos de la comisión lo fijan RN-02011 y RN-02004.
- El dominio devuelve el motivo `WORK_NOT_FOUND_FOR_REQUESTER`; la traducción a la respuesta que sale del proceso pertenece a `GeometriaFactory-Api`, que debe conservar la indistinguibilidad.

## 4. Consecuencia si se viola

Rechazo, con la forma exacta del rechazo como parte de la regla: la respuesta ante un trabajo ajeno debe ser idéntica a la respuesta ante un trabajo inexistente. Un mensaje, un código o un tiempo de respuesta que permitan distinguirlos violan la regla aunque la operación no se haya ejecutado.

## 5. CU afectados

- [CU-00028](../Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) — Resolver el acceso de un alumno a un trabajo.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: consulta de un trabajo del alumno A por parte del alumno B, con resultado idéntico al de una consulta sobre un trabajo inexistente. La verificación equivalente **forzando la petición al servicio de datos**, y no sólo desde la pantalla, es criterio bloqueante declarado aguas arriba (PRODUCT-INTAKE §17.1.P.6 · GeometriaFactory-Api) y pertenece a las pruebas de integración de `GeometriaFactory-Api`. El criterio de éxito de negocio es de `NB-00003` §5: 0 operaciones que procedan sobre trabajos de otro alumno.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el enunciado completo que `PRODUCT-INTAKE` 1.3 §4.1 transcribe y el enunciado de INV-02 de §17.1.P.2 · GeometriaFactory-Domain. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. El enunciado antepone la formulación de la fuente —«un alumno sólo ve y opera sus propios trabajos»— a la indistinguibilidad, que es su forma de verificación. §3 precisa que la regla alcanza a los cuatro estados y que no alcanza al administrador, cuyo alcance fijan RN-02011 y RN-02004. |
| 1.2 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **1 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
