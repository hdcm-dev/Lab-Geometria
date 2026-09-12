# BT-00010 — Fijar la vigencia del acceso firmado

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00010-Fijar-La-Vigencia-Del-Acceso-Firmado.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T02 · Superficie y formato de intercambio
**Etapa del producto:** `a`
**Tipo:** indagación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Fijar la vigencia del acceso firmado.

## 2. Justificación

`05` §11 `PA-04`; [`ADR-00003`](../../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api, que declara «corta» y **sin acceso de refresco**

## 3. Criterios de aceptación

- El número queda tomado **de configuración** aplicando el criterio ya fijado: que caduque **dentro de la sesión de trabajo de una clase** y que **la renovación sea reingreso**. **Ninguna fuente da el número**, y esta tarea no lo inventa: lo elige aplicando el criterio y lo registra. **Caja temporal: la etapa `a`**

## 4. Dependencias

- BT-00002

## 5. Tipo

`indagación`. El número queda tomado **de configuración** aplicando el criterio ya fijado: que caduque **dentro de la sesión de trabajo de una clase** y que **la renovación sea reingreso**. **Ninguna fuente da el número**, y esta tarea no lo inventa: lo elige aplicando el criterio y lo registra. **Caja temporal: la etapa `a`**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00001, US-00004 |
| CU upstream | CU-00001, CU-00002 |
| Puntos de acceso que toca | A-01, y los once bajo la guardia |
| Fuente de arquitectura | ADR-00003 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00010 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T02 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
