# BT-00008 — Fijar el formato de intercambio para los dos extremos

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00008-Fijar-El-Formato-De-Intercambio-Para-Los-Dos-Extremos.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T02 · Superficie y formato de intercambio
**Etapa del producto:** `a`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Fijar el formato de intercambio para los dos extremos.

## 2. Justificación

[`ADR-00002`](../../05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md); `05` §2.2, quinta y sexta decisión heredada; `05` §9, cuarto riesgo

## 3. Criterios de aceptación

- **Exactamente 1** configuración de intercambio declarada en el producto, en la **composición de raíz** y en ningún otro lado
- campos con **nombre literal**, valores de conjunto cerrado **por su nombre y nunca por su posición**, campos nulos **emitidos**, números **sin cultura** y **lectura estricta**
- **ningún punto de acceso configura la serialización por su cuenta**. La coincidencia con el otro extremo **se verifica ejerciendo el servicio real** y no comparando dos archivos. **Esta decisión obliga a `GeometriaFactory-Web`, que declaró que la adopta**

## 4. Dependencias

- BT-00002

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00019, US-00022, US-00024 |
| CU upstream | CU-00006, CU-00007, CU-00009 |
| Puntos de acceso que toca | Los quince |
| Fuente de arquitectura | ADR-00002 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00008 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T02 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
