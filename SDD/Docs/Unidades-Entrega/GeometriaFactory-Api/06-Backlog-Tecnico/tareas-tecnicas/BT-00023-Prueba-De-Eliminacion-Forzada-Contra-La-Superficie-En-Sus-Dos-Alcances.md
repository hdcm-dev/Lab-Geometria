# BT-00023 — Prueba de eliminación forzada contra la superficie, en sus dos alcances

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00023-Prueba-De-Eliminacion-Forzada-Contra-La-Superficie-En-Sus-Dos-Alcances.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T05 · Verificación, muestras y despliegue
**Etapa del producto:** `e`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Prueba de eliminación forzada contra la superficie, en sus dos alcances.

## 2. Justificación

`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Api, criterio bloqueante tomado de la fuente; `05` §8, fila correspondiente; `Roadmap-Producto.md` §5.2, transición `e` → `f`

## 3. Criterios de aceptación

- **0** eliminaciones fuera de alcance aceptadas al **forzar la petición**: un trabajo que no está en `Borrador` y uno que no pertenece al solicitante. **Es el único criterio de verificación del producto que la fuente exige ejercer forzando la petición contra esta superficie**, y no sólo por la interfaz

## 4. Dependencias

- BT-00018
- BT-00022

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00020 |
| CU upstream | CU-00006 |
| Puntos de acceso que toca | A-12 |
| Fuente de arquitectura | `05` §8, criterio bloqueante de la fuente |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00023 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T05 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
