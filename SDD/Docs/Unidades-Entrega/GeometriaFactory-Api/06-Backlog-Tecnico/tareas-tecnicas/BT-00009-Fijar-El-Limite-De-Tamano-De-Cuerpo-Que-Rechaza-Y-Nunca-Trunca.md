# BT-00009 — Fijar el límite de tamaño de cuerpo que rechaza y nunca trunca

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00009-Fijar-El-Limite-De-Tamano-De-Cuerpo-Que-Rechaza-Y-Nunca-Trunca.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T02 · Superficie y formato de intercambio
**Etapa del producto:** `a`
**Tipo:** indagación
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Fijar el límite de tamaño de cuerpo que rechaza y nunca trunca.

## 2. Justificación

[`ADR-00002`](../../05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6; `05` §11 `PA-05`; [`Infrastructure ADR-06006`](../../05-Arquitectura-Tecnica/Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3

## 3. Criterios de aceptación

- **Un solo** límite para todo el producto, tomado de configuración
- el cuerpo que lo excede **se rechaza y nunca se trunca**, y **la forma de rechazo no es configurable**
- el número se calibra **sobre el texto más grande que la fuente documenta**. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá** porque el corte pertenece al borde del proceso. **Caja temporal: la etapa `a`**

## 4. Dependencias

- BT-00008

## 5. Tipo

`indagación`. el número se calibra **sobre el texto más grande que la fuente documenta**. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá** porque el corte pertenece al borde del proceso. **Caja temporal: la etapa `a`**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00019 |
| CU upstream | CU-00006 |
| Puntos de acceso que toca | A-10, A-11 |
| Fuente de arquitectura | ADR-00002 §2 punto 6 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00009 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T02 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
