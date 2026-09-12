# BT-00012 — Puerta de inspección de los quince puntos contra la guardia, en las dos direcciones

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00012-Puerta-De-Inspeccion-De-Los-Quince-Puntos-Contra-La-Guardia-En-Las-Dos-Direcciones.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T03 · Guardia y traducción
**Etapa del producto:** `c`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Puerta de inspección de los quince puntos contra la guardia, en las dos direcciones.

## 2. Justificación

`05` §8, fila de puntos fuera de la guardia; `05` §9, primer riesgo; `RN-00013`, `INV-09`

## 3. Criterios de aceptación

- Exactamente **4** puntos fuera de la guardia, **ni uno más**, y son los declarados: canje, registro, configuración del administrador y salud
- la inspección **recorre los quince y compara contra la lista en las dos direcciones**
- y **0** puntos que fijen una contraseña sobre una cuenta existente sin credencial. **Se mide en cada etapa que agregue un punto**, no sólo en la que la introdujo

## 4. Dependencias

- BT-00011

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: es el defecto de omisión más caro de esta capa. Un punto nuevo fuera de la guardia rompe `RN-00013` **sin que nada falle** |
| CU upstream | CU-00002 |
| Puntos de acceso que toca | Los quince, en las dos direcciones |
| Fuente de arquitectura | `05` §8, ADR-00003 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00012 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T03 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
