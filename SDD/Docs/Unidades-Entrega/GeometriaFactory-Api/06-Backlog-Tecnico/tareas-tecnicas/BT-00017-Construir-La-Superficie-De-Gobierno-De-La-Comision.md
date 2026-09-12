# BT-00017 — Construir la superficie de gobierno de la comisión

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00017-Construir-La-Superficie-De-Gobierno-De-La-Comision.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T04 · Las cuatro superficies de acceso
**Etapa del producto:** `d`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la superficie de gobierno de la comisión.

## 2. Justificación

`05` §3.1, componente correspondiente, y §3.4; `05` §10.2, filas de RN-00007, RN-00012, RN-00015 y RN-00016

## 3. Criterios de aceptación

- Los **cuatro** puntos del administrador sobre cuentas ajenas: listado, cambio de situación, baja **transportando el correo escrito** y reseteo
- **el cambio de situación devuelve la provisoria** en su resultado y **el reseteo la devuelve una sola vez**
- el punto de reseteo **no declara ningún parámetro de situación** y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe
- **el reseteo no toca ninguna ruta de retiro**

## 4. Dependencias

- BT-00011
- BT-00013
- BT-00016

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00011, US-00012, US-00013, US-00014, US-00015, US-00016 |
| CU upstream | CU-00004, CU-00005 |
| Puntos de acceso que toca | A-06, A-07, A-08, A-09 |
| Fuente de arquitectura | `05` §3.1, superficie de gobierno |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00017 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T04 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
