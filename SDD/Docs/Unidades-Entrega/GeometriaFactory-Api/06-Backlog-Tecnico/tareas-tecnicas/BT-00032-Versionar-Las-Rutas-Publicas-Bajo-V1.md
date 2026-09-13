# BT-00032 — Versionar las rutas públicas bajo `/v1/`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00032-Versionar-Las-Rutas-Publicas-Bajo-V1.md
**Versión:** 1.2
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Versionar las rutas públicas bajo `/v1/`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 7

## 3. Criterios de aceptación

- Las rutas públicas quedan bajo `/v1/`
- la batería de integración existente (BT-00022) corre **en verde** contra las rutas versionadas
- no queda ninguna ruta pública sin versión de MAJOR en su prefijo
- las rutas efectivamente publicadas bajo `/v1/` se comparan **en las dos direcciones** contra los **quince** puntos de acceso de `05` §3.4: ninguno de los quince queda sin `/v1/` y ningún prefijo `/v1/` corresponde a una ruta que `05` §3.4 no declare

## 4. Dependencias

- BT-00027
- BT-00029
- BT-00030

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita BT-00034 y BT-00035 |
| CU upstream | — (sin CU: versionado de ruta) |
| Puntos de acceso que toca | Los quince, bajo `/v1/` |
| Fuente de arquitectura | ADR-00008 (a reescribir) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00032 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1). El criterio 1 se sostiene en `ADR-00008` (§2 y §7). El criterio 4 exige, para toda tarea que toque la superficie, declarar la comparación **en las dos direcciones**: esta tarea toca los quince puntos y no la declaraba. Se agrega un cuarto criterio de aceptación en §3, citando `05` §3.4 (la tabla de los quince puntos, ya existente), que la implica: ninguno de los quince queda sin `/v1/` y ningún prefijo `/v1/` corresponde a una ruta ajena a esa tabla. **Ningún otro criterio cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **§4 retira la dependencia de `BT-00028`**, que pasó a `Descartada` por [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) (la API autentica personas, no aplicaciones; **Aceptado**). Quedan tres dependencias: `BT-00027`, `BT-00029` y `BT-00030`. **Ningún criterio de aceptación ni fuente cambia.** Reevaluación del criterio 5 de `Definition-Of-Ready.md` §2.1: **Sí** (tres dependencias, sin ciclo, verificado con `tsort` en `Audit/DoR-Tramo-k-2026-09-12.md` 1.2). Sigue **Ready**. |
