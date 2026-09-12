# BT-00007 — Fijar las rutas y los verbos de los quince puntos de acceso en el punto de control

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00007-Fijar-Las-Rutas-Y-Los-Verbos-De-Los-Quince-Puntos-De-Acceso-En-El-Punto-De-Control.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T02 · Superficie y formato de intercambio
**Etapa del producto:** `a`
**Tipo:** indagación
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Fijar las rutas y los verbos de los quince puntos de acceso en el punto de control.

## 2. Justificación

`05` §3.4 y §11 `PA-01`; [`Definicion-Superficie-HTTP.md`](../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3

## 3. Criterios de aceptación

- Las rutas y los verbos quedan validados en el punto de control de la etapa `a`. Las **dos** únicas cosas que una fuente declara son el punto de canje, con su ruta, y la **existencia** de un punto de salud, cuya ruta la fuente **no da**
- las quince filas son **propuesta derivada rotulada fila por fila** y esta tarea las confirma o las corrige. **`A-04` queda retirado y no se recicla.** **Caja temporal: la etapa `a`**

## 4. Dependencias

- BT-00001

## 5. Tipo

`indagación`. las quince filas son **propuesta derivada rotulada fila por fila** y esta tarea las confirma o las corrige. **`A-04` queda retirado y no se recicla.** **Caja temporal: la etapa `a`**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: los quince puntos dependen de ella |
| CU upstream | CU-00001, CU-00003 a CU-00008, CU-00011 |
| Puntos de acceso que toca | Los quince |
| Fuente de arquitectura | `05` §11 `PA-01` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00007 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T02 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
