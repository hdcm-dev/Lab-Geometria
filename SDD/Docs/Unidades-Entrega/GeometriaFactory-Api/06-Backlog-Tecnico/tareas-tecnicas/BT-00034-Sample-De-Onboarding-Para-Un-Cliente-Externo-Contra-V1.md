# BT-00034 — Sample de onboarding para un cliente externo contra `/v1/`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00034-Sample-De-Onboarding-Para-Un-Cliente-Externo-Contra-V1.md
**Versión:** 1.2
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Sample de onboarding para un cliente externo contra `/v1/`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 9; `05` §8.1 (`GeometriaFactory-Api`), última fila **«Pasos de la colección de peticiones reproducible»** (5 o menos, 0 datos inventados, [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)) — el mismo NFR que ya sostiene a BT-00020, del que esta tarea es análoga

## 3. Criterios de aceptación

- Un cliente de referencia corre contra `/v1/` **sin conocer el código fuente**, en **cinco pasos o menos**, análogo a BT-00020: se autentica **como una persona** por `POST /auth/token` con correo y contraseña, que es la única vía de toda aplicación propia ([`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)); lo que lo distingue de BT-00020 es que corre **sin conocer el código fuente** y contra las rutas versionadas

## 4. Dependencias

- BT-00032

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: extiende BT-00020 al consumidor externo |
| CU upstream | — (sin CU: onboarding externo) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §8.1 (`GeometriaFactory-Api`), fila «Pasos de la colección de peticiones reproducible», [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00034 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban únicamente el registro de mesa, no admitido. Esta tarea es declaradamente análoga a BT-00020, que cita `05` §8, última fila («Pasos de la colección de peticiones reproducible», `ADR-00008`); se agrega la misma cita acá, porque sostiene el mismo criterio de forma (cinco pasos o menos, cero datos inventados) que esta tarea reclama sin fuente propia. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Propagación de [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)** (la API autentica personas, no aplicaciones; **Aceptado**). El criterio de §3 decía «autenticado con clave propia de cliente externo y no con el acceso de un alumno»; esa clave no existe. El sample se autentica **como una persona** por `POST /auth/token`, igual que BT-00020, y lo que lo distingue es que corre sin conocer el código fuente y contra `/v1/`. El título conserva «cliente externo» con el sentido que `PRODUCT-INTAKE` **4.4** §17.1.P.3 le da: una aplicación propia distinta del front. §4 no cambia (`BT-00032`); esta ficha no dependía de `BT-00028` de forma directa, y la transitiva se retiró en `BT-00032` 1.2. Sigue **Ready**. |
