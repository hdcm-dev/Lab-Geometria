# BT-00015 — Cablear los dos códigos que cerraron los huecos del conjunto cerrado

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00015-Cablear-Los-Dos-Codigos-Que-Cerraron-Los-Huecos-Del-Conjunto-Cerrado.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T03 · Guardia y traducción
**Etapa del producto:** `c`
**Tipo:** implementación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Cablear los dos códigos que cerraron los huecos del conjunto cerrado.

## 2. Justificación

`05` §11 `PA-02` y `PA-03`, **los dos resueltos**; `02` §11

## 3. Criterios de aceptación

- Los **dos** huecos que esta tarea elevaba están **cerrados** por `PRODUCT-INTAKE` **1.29** §17.4 P.3 (2026-08-12): entraron al conjunto cerrado `OPERATION_ADMIN_ONLY` y `STATE_FORBIDS_UPDATE`, que `GeometriaFactory-Contracts` emite. Lo que queda es **trabajo de traducción y no de indagación**: las dos filas nuevas de la tabla de `05` §5, con destinos `403` y `409`, y el genérico bajando de cuatro destinos a dos. **Esta categoría no inventó ningún código**. **Caja temporal: ninguna comprometida**

## 4. Dependencias

- BT-00013

## 5. Tipo

`implementación`. Los **dos** huecos que esta tarea elevaba están **cerrados** por `PRODUCT-INTAKE` **1.29** §17.4 P.3 (2026-08-12): entraron al conjunto cerrado `OPERATION_ADMIN_ONLY` y `STATE_FORBIDS_UPDATE`, que `GeometriaFactory-Contracts` emite. Lo que queda es **trabajo de traducción y no de indagación**: las dos filas nuevas de la tabla de `05` §5, con destinos `403` y `409`, y el genérico bajando de cuatro destinos a dos. **Esta categoría no inventó ningún código**. **Caja temporal: ninguna comprometida**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: la decisión es del Product Owner y de `GeometriaFactory-Contracts` |
| CU upstream | CU-00009 |
| Puntos de acceso que toca | A-06, A-07, A-08, A-09, A-10, A-11, A-13, A-14 |
| Fuente de arquitectura | `05` §11 `PA-02` y `PA-03`, resueltos |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00015 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T03 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
