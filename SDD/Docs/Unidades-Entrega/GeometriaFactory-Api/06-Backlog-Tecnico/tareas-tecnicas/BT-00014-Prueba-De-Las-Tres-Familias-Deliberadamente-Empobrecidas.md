# BT-00014 — Prueba de las tres familias deliberadamente empobrecidas

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00014-Prueba-De-Las-Tres-Familias-Deliberadamente-Empobrecidas.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T03 · Guardia y traducción
**Etapa del producto:** `c`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Prueba de las tres familias deliberadamente empobrecidas.

## 2. Justificación

`05` §7, fila de familias empobrecidas; `05` §8, fila de respuestas indistinguibles; `05` §9, segundo riesgo

## 3. Criterios de aceptación

- **3 de 3** comparaciones dan **idénticas, cuerpo y código**: trabajo ajeno contra inexistente, correo inválido contra contraseña inválida, y correo ocupado por cuenta habilitada contra ocupado por cuenta bloqueada. **En las tres es la decisión y no el defecto**, y la primera es la que rompe `RN-00003` hacia afuera **sin que ninguna capa de adentro se entere**

## 4. Dependencias

- BT-00013

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00002, US-00020, US-00021 |
| CU upstream | CU-00001, CU-00006, CU-00007, CU-00009 |
| Puntos de acceso que toca | A-01, A-02, A-12, A-13, A-14 |
| Fuente de arquitectura | ADR-00004 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00014 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T03 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
