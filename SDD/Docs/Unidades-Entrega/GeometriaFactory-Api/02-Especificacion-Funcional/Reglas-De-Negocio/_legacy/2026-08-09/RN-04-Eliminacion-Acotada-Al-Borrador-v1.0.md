> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `RN-04-Eliminacion-Acotada-Al-Borrador.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`RN-04-Eliminacion-Acotada-Al-Borrador.md`](../../RN-04-Eliminacion-Acotada-Al-Borrador.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# RN-04 — La eliminación de un trabajo está acotada al borrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-04-Eliminacion-Acotada-Al-Borrador.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-07, con RN-04), §17.5.P.6; [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) §4 y §5; `00-Contexto/Alcance-Producto.md` §4.1
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

Un trabajo sólo se elimina mientras está en estado `Borrador`. En `Pendiente` y en `Finalizado` la eliminación no procede, cualquiera sea quien la solicite.

## 2. Justificación

La capacidad declarada del producto acota la eliminación al borrador (PRODUCT-INTAKE §4, F-07). El fundamento de negocio es que un trabajo ya presentado no puede desaparecer: el estado es lo que expresa la entrega y una entrega borrada dejaría a la métrica de cierre del circuito sin sustento (`NB-03` §4).

## 3. Ámbito de aplicación

- Se evalúa cada vez que se consulta si una eliminación procede.
- Se evalúa también cuando la solicitud llega forzando la petición al servicio de datos y no desde la pantalla: la regla es del dominio y no de la interfaz.
- No se evalúa en la baja de una cuenta: esa operación arrastra los trabajos del alumno cualquiera sea su estado, y su regla es RN-07.

## 4. Consecuencia si se viola

Rechazo, con el motivo `ELIMINACION_FUERA_DE_BORRADOR`. El trabajo queda intacto, con su estado, su texto original, sus piezas y sus observaciones.

## 5. CU afectados

- [CU-09](../Casos-De-Uso/CU-09-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) — Resolver el acceso de un alumno a un trabajo.
- [CU-08](../Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md) — Gobernar el estado del trabajo, en cuanto al único estado desde el que el trabajo puede dejar de existir.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: eliminación admitida en `Borrador`, rechazada en `Pendiente` y rechazada en `Finalizado`. El criterio bloqueante de verificarlo **forzando la petición** pertenece a las pruebas de integración de `GeometriaFactory-Api` (PRODUCT-INTAKE §17.5.P.6). El criterio de éxito de negocio es de `NB-03` §5: 0 eliminaciones que procedan fuera del estado `Borrador`.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
