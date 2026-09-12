# BT-00024 — Prueba del texto original byte a byte y del rechazo sin truncamiento

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00024-Prueba-Del-Texto-Original-Byte-A-Byte-Y-Del-Rechazo-Sin-Truncamiento.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T05 · Verificación, muestras y despliegue
**Etapa del producto:** `e`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Prueba del texto original byte a byte y del rechazo sin truncamiento.

## 2. Justificación

`05` §8, fila de textos alterados; `05` §9, tercer riesgo; `RN-00008`

## 3. Criterios de aceptación

- **0** caracteres de diferencia entre lo enviado y lo guardado, comparado **byte a byte** con el texto de `E-1`
- y **0** truncamientos silenciosos: un cuerpo por encima del límite **se rechaza y no se trunca**. **Truncar rompe `RN-00008` en silencio**, con el trabajo guardado y el texto mutilado, y el alumno lo descubre al ver el dibujo

## 4. Dependencias

- BT-00009
- BT-00018
- BT-00022

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00019 |
| CU upstream | CU-00006 |
| Puntos de acceso que toca | A-10, A-11 |
| Fuente de arquitectura | ADR-00002, `RN-00008` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00024 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T05 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
