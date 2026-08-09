# RN-10 — El desenlace es exclusivo del administrador y es terminal

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-10), §4 (F-21 y F-23), §4.2 (modelo de estados del trabajo y sus tres consecuencias aceptadas), §17.1.P.2 (INV-07), §5 (historia 7.1), §6 (flujo 2.1); [`NB-09`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1
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

Sólo el administrador aprueba o rechaza un trabajo, y sólo desde estado `Pendiente`. `Finalizado` y `Rechazado` son terminales: ninguna transición sale de ellos.

## 2. Justificación

El desenlace es lo que convierte una entrega depositada en una entrega con respuesta, y es facultad del docente por definición del circuito de revisión que el Product Owner incorporó el 2026-08-08 (PRODUCT-INTAKE §4.1). La exclusividad no es una comodidad de interfaz: un alumno que pudiera aprobarse su propio trabajo vaciaría de sentido la revisión, y por eso la regla se hace cumplir aunque la petición llegue forzada por fuera de la interfaz.

La terminalidad se decidió con su consecuencia a la vista: corregir un rechazo significa **cargar un trabajo nuevo**, y el rechazado queda como registro del intento (§4.2, consecuencia 1). El invariante que la expresa como condición permanente es **INV-07**: un trabajo en `Finalizado` o en `Rechazado` no cambia de estado ni de contenido.

## 3. Ámbito de aplicación

- Se evalúa en toda solicitud de aprobación o de rechazo, sobre el papel de quien la pide y sobre el estado del trabajo.
- Se evalúa en toda transición que se intente sobre un trabajo ya `Finalizado` o `Rechazado`, venga de donde venga.
- Alcanza también al **contenido**: sobre un trabajo terminal no se reedita el texto, no se reconstruyen las piezas y no se reemplazan las observaciones.
- **No** alcanza a la eliminación por parte del administrador, que retira el trabajo entero y es RN-04, ni a la baja de la cuenta de su dueño, que es RN-07. La terminalidad impide que el trabajo cambie, no que desaparezca.
- El comentario del administrador se fija en el mismo acto del desenlace y es opcional en los dos casos.

## 4. Consecuencia si se viola

Rechazo. Una solicitud de desenlace sin papel de administrador se rechaza con el código `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`; una sobre un trabajo que no está en estado `Pendiente`, con `DESENLACE_FUERA_DE_PENDIENTE`; y cualquier transición sobre un trabajo terminal, con `TRANSICION_DESDE_ESTADO_TERMINAL`. En los tres casos el trabajo queda exactamente como estaba.

## 5. CU afectados

- [CU-10](../Casos-De-Uso/CU-10-Resolver-El-Desenlace-Del-Trabajo.md) — Resolver el desenlace del trabajo.
- [CU-08](../Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md) — Gobernar el estado del trabajo en el envío, que rechaza toda transición desde un estado terminal.
- [CU-05](../Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md) y [CU-06](../Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) — En cuanto a que el contenido de un trabajo terminal tampoco cambia.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: aprobación y rechazo admitidos desde estado `Pendiente` con papel `Administrador`; rechazados con papel `Alumno`; rechazados desde `Borrador`, desde `Finalizado` y desde `Rechazado`; y la comprobación de que **0 transiciones** salen de los dos estados de cierre, incluidas las de reedición y de reconstrucción de piezas. El criterio de verificación declarado por el intake es que un alumno que fuerce la transición contra el servicio de datos sea rechazado (§4.1), y esa mitad pertenece a las pruebas de integración de `GeometriaFactory-Api`. Los criterios de éxito de negocio son de `NB-09` §5: 2 de 2 desenlaces disponibles y 0 transiciones que salgan de un estado terminal.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Regla nueva del circuito de revisión que el Product Owner incorporó el 2026-08-08 y que `PRODUCT-INTAKE` 1.3 §4.1 declara; §17.1.P.2 declara INV-07 como el invariante que la expresa. No existía en la fuente funcional original. |
