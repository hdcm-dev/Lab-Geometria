# BT-00022 — Construir la batería de integración con la pirámide invertida

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00022-Construir-La-Bateria-De-Integracion-Con-La-Piramide-Invertida.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T05 · Verificación, muestras y despliegue
**Etapa del producto:** `c`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la batería de integración con la pirámide invertida.

## 2. Justificación

`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Api; `05` §8, filas de cobertura y de forma de la pirámide

## 3. Criterios de aceptación

- La batería **golpea el servicio real por su superficie contra el almacén real**
- la forma declarada es **60 %** de integración y **40 %** unitarias, **invertida a propósito porque lo que esta capa aporta es cableado y el cableado se verifica ejerciéndolo**
- cubre además el contrato con el ensamblado de tipos **de extremo a extremo**. Los dos porcentajes vienen **rotulados como asunción** y se usan como vigentes

## 4. Dependencias

- BT-00001
- BT-00003

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: es la verificación de esta capa y también la de las capas de adentro que no pueden tocar la base |
| CU upstream | CU-00001 a CU-00011 |
| Puntos de acceso que toca | Los quince |
| Fuente de arquitectura | `05` §8, ADR-00001 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00022 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T05 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
