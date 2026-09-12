# BT-00001 — Crear el proyecto de código y su proyecto de pruebas de integración

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00001-Crear-El-Proyecto-De-Codigo-Y-Su-Proyecto-De-Pruebas-De-Integracion.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T01 · Fundaciones, composición de raíz y arranque
**Etapa del producto:** `a`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Crear el proyecto de código y su proyecto de pruebas de integración.

## 2. Justificación

`PRODUCT-INTAKE` §16 y §17.1.P.1 · GeometriaFactory-Api; `05` §5

## 3. Criterios de aceptación

- El proyecto de código compila dentro del artefacto de agrupación con sus **tres** dependencias de compilación
- **el proyecto de pruebas de integración existe acá y es el que golpea el servicio real**, incluido el de las capas de adentro que no pueden tocar la base

## 4. Dependencias

Ninguna.

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita a las 30 |
| CU upstream | CU-00001 a CU-00012 |
| Puntos de acceso que toca | Los quince |
| Fuente de arquitectura | `05` §5 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00001 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T01 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
