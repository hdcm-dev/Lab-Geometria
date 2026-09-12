# BT-00029 — Rate limiting por clave o por IP

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Rate limiting por clave o por IP.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 4; `05` §9 (único escritor SQLite, `RS-OPE-01`)

## 3. Criterios de aceptación

- `Microsoft.AspNetCore.RateLimiting` aplicado por clave o por IP
- una batería que excede la cuota recibe `429`
- el único escritor SQLite no se satura bajo la carga sintética que la batería ejercita

## 4. Dependencias

- BT-00028

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: protege el recurso que BT-00006 y BT-00022 ya miden |
| CU upstream | — (sin CU: cuota de uso) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §9, `RS-OPE-01` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00029 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
