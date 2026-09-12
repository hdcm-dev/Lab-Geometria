# BT-00026 — Probar una vez la construcción de la imagen en destino desde el repositorio

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00026-Probar-Una-Vez-La-Construccion-De-La-Imagen-En-Destino-Desde-El-Repositorio.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T05 · Verificación, muestras y despliegue
**Etapa del producto:** `h`
**Tipo:** indagación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Probar una vez la construcción de la imagen en destino desde el repositorio.

## 2. Justificación

`05` §11 `PA-08`; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Api punto 5, rotulado **[A VERIFICAR]**

## 3. Criterios de aceptación

- El mecanismo queda **probado una vez antes de depender de él**, tal como el intake exige: el motor de contenedores del destino resuelve la referencia al repositorio y tiene credenciales si es privado. **No es una asunción de esta categoría** y **la decisión de medirlo es de `09-Devops`**
- esta tarea la eleva con su plazo. **Caja temporal: antes de la etapa de despliegue real**

## 4. Dependencias

- BT-00004

## 5. Tipo

`indagación`. esta tarea la eleva con su plazo. **Caja temporal: antes de la etapa de despliegue real**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: es el único canal de entrega declarado |
| CU upstream | — (canal de entrega) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §11 `PA-08` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00026 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T05 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
