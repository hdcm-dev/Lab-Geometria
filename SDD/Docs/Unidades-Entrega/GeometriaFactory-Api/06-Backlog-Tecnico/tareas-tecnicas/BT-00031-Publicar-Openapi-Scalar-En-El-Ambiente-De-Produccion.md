# BT-00031 — Publicar OpenAPI/Scalar en el ambiente de producción

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00031-Publicar-Openapi-Scalar-En-El-Ambiente-De-Produccion.md
**Versión:** 1.1
**Estado:** Borrador
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** devops
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Publicar OpenAPI/Scalar en el ambiente de producción.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 6

## 3. Criterios de aceptación

- `Documentacion__Publicada=true` en el ambiente de producción
- `/openapi/v1.json` responde `200`
- Scalar sirve la documentación pública sin exigir credencial de alumno

## 4. Dependencias

Ninguna.

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: ya construido, sólo cambia la configuración de publicación |
| CU upstream | — (sin CU: documentación pública) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `Mesa-2026-09-12-ciclo-2.md` §4 ítem 6 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00031 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1): **queda en Borrador**. El criterio 1 no se cumple: §2 y §7 citan sólo `Mesa-2026-09-12-ciclo-2.md` §4 ítem 6, un registro de mesa que no está en la lista admitida. Se buscó una fuente admitida —componente de `05` §3.1, ADR, NFR de §8, riesgo de §9, punto abierto de §11 de las cuatro capas, punto de acceso de la superficie de `02`, regla de delivery del intake §15— para «publicar OpenAPI/Scalar en producción»: **no existe ninguna**. Ni `05` ni el intake §15 mencionan Scalar, OpenAPI ni la variable `Documentacion__Publicada`. Queda **abierto** hasta que la categoría 05 (o el Product Owner) registre esta decisión en una fuente admitida. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
