# BT-00011 — Construir la guardia de admisión transversal

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00011-Construir-La-Guardia-De-Admision-Transversal.md
**Versión:** 1.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T03 · Guardia y traducción
**Etapa del producto:** `c`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la guardia de admisión transversal.

## 2. Justificación

`05` §3.1, componente «Guardia de admisión»; [`ADR-00003`](../../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `05` §7, filas de autenticación, autorización y guardia

## 3. Criterios de aceptación

- Verifica **firma y expiración** del acceso, exige el **papel** que cada punto declara y aplica la **guardia del cambio de contraseña pendiente**
- es transversal a los **once** puntos que exigen acceso
- **exigir el papel no es autorizar**: la verificación de pertenencia y de facultad se hace sobre el dato recuperado y es de la capa de aplicación, y **duplicarla acá crearía un segundo lugar donde la regla puede decir otra cosa**

## 4. Dependencias

- BT-00002
- BT-00007

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00004, US-00005, US-00006 |
| CU upstream | CU-00002 |
| Puntos de acceso que toca | Los **once** bajo la guardia |
| Fuente de arquitectura | ADR-00003 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00011 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T03 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
