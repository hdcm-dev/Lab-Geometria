# BT-00018 — Construir la superficie de trabajos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00018-Construir-La-Superficie-De-Trabajos.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T04 · Las cuatro superficies de acceso
**Etapa del producto:** `e`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la superficie de trabajos.

## 2. Justificación

`05` §3.1, componente correspondiente, y §3.4; `05` §6, las dos decisiones de frontera

## 3. Criterios de aceptación

- Los **cinco** puntos sobre trabajos: envío, reenvío, eliminación con sus **dos** alcances, listado y detalle
- **el texto original no se normaliza en el borde**
- el listado **no arrastra el texto ni los componentes** y esta capa **no recompone la proyección**
- **la superficie no declara ningún parámetro con el que se puedan pedir borradores ajenos**

## 4. Dependencias

- BT-00008
- BT-00011
- BT-00013

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00017, US-00018, US-00019, US-00020, US-00021, US-00022 |
| CU upstream | CU-00006, CU-00007 |
| Puntos de acceso que toca | A-10, A-11, A-12, A-13, A-14 |
| Fuente de arquitectura | `05` §3.1, superficie de trabajos |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00018 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T04 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
