# BT-00016 — Construir la superficie de acceso y credencial propia

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00016-Construir-La-Superficie-De-Acceso-Y-Credencial-Propia.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T04 · Las cuatro superficies de acceso
**Etapa del producto:** `c`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la superficie de acceso y credencial propia.

## 2. Justificación

`05` §3.1, componente correspondiente, y §3.4

## 3. Criterios de aceptación

- Los **cuatro** puntos que se ejercen **sin acceso firmado o sobre la propia cuenta**: canje, registro de cuenta, configuración del administrador y cambio de la propia contraseña. **El registro es anónimo por diseño y así debe seguir**
- el cambio de la propia contraseña es **la única excepción de la guardia del cambio pendiente**
- **ninguno de los cuatro que no exigen acceso fija una contraseña sobre una cuenta existente**

## 4. Dependencias

- BT-00007
- BT-00011
- BT-00013

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00001, US-00002, US-00003, US-00007, US-00008, US-00009, US-00010 |
| CU upstream | CU-00001, CU-00003 |
| Puntos de acceso que toca | A-01, A-02, A-03, A-05 |
| Fuente de arquitectura | `05` §3.1, superficie de acceso |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00016 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T04 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
