# BT-00004 — Construir la imagen multietapa y medir `PT-04`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00004-Construir-La-Imagen-Multietapa-Y-Medir-Pt-04.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T01 · Fundaciones, composición de raíz y arranque
**Etapa del producto:** `a`
**Tipo:** devops
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la imagen multietapa y medir `PT-04`.

## 2. Justificación

`PRODUCT-INTAKE` §15 y §17.1.P.8 · GeometriaFactory-Api; `05` §5, etapas del pipeline y contenido de la imagen

## 3. Criterios de aceptación

- La imagen se construye con el archivo de construcción **multietapa**, lleva **sólo el entorno de ejecución** —sin kit de desarrollo ni depurador— y **no tiene linaje con la imagen del contenedor de desarrollo**
- arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. **Una puerta que no pasa detiene la planificación de las etapas que dependen de ella**

## 4. Dependencias

- BT-00003

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: es `PT-04`, puerta del producto |
| CU upstream | CU-00011 |
| Puntos de acceso que toca | A-16 |
| Fuente de arquitectura | `05` §5, `PT-04` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00004 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T01 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
