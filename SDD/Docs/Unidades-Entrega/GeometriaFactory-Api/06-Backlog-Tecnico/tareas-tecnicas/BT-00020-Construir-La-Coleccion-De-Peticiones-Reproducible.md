# BT-00020 — Construir la colección de peticiones reproducible

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00020-Construir-La-Coleccion-De-Peticiones-Reproducible.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T05 · Verificación, muestras y despliegue
**Etapa del producto:** `h`
**Tipo:** docs
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la colección de peticiones reproducible.

## 2. Justificación

`PRODUCT-INTAKE` §16.1 y §18 `S-2`; `05` §8, última fila; [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)

## 3. Criterios de aceptación

- Se reproduce en **5 pasos o menos**, con **0** datos de prueba inventados
- los cuerpos son los escenarios del intake §20
- incluye alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, que es lo que la ubica en la etapa `h`
- **no implementa nada: demuestra**, y vive en el árbol de muestras del repositorio

## 4. Dependencias

- BT-00016
- BT-00017
- BT-00018
- BT-00019

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00030 |
| CU upstream | CU-00012 |
| Puntos de acceso que toca | Los quince, por ejercicio |
| Fuente de arquitectura | ADR-00008, `PRODUCT-INTAKE` §16.1 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00020 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T05 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
