# BT-00033 — Adoptar MinVer y etiquetar los commits de producción

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00033-Adoptar-Minver-Y-Etiquetar-Los-Commits-De-Produccion.md
**Versión:** 1.1
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** devops
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Adoptar MinVer y etiquetar los commits de producción.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 8; `D-03` confirmado (MinVer); `05` §11.3 (`GeometriaFactory-Application`) `PA-06` y §11.2 (`GeometriaFactory-Domain`) `PA-04` — los dos puntos abiertos sobre «la herramienta que calcula la versión», hoy vigentes y reasignados a la fase `i`, que `D-03` resuelve adoptando MinVer

## 3. Criterios de aceptación

- `/salud` informa la versión SemVer real calculada por MinVer
- el evento de etiqueta es la fusión a `main` que cambia código de producción, calculado por Conventional Commits
- los commits `89f3ab3` y `5c95dab` quedan etiquetados o se declara explícitamente por qué no

## 4. Dependencias

Ninguna.

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: cierra `05` §11.3 (`GeometriaFactory-Application`) `PA-06` y §11.2 (`GeometriaFactory-Domain`) `PA-04` |
| CU upstream | — (sin CU: herramienta de versión) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §11 `PA-06` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00033 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de ambigüedad**. §2 y §7 citaban «`05` §11 `PA-06`» sin decir de qué capa: **esta ficha vive en `GeometriaFactory-Api`, cuyo propio `PA-06` es «RESUELTO. El alcance de la colección de peticiones» (§18, ocho escenarios) y no tiene relación con MinVer**. El `PA-06` que corresponde es el de `GeometriaFactory-Application` (§11.3): «la herramienta que calcula la versión... no está elegida», hoy **vigente** y reasignado a la fase `i`; `GeometriaFactory-Domain` §11.2 trae el mismo punto abierto como `PA-04`. Se corrige la cita en §2 y en §7 a los dos, calificados por capa. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
