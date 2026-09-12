# BT-00013 — Construir el traductor con la tabla única, sin códigos inventados

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00013-Construir-El-Traductor-Con-La-Tabla-Unica-Sin-Codigos-Inventados.md
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

Construir el traductor con la tabla única, sin códigos inventados.

## 2. Justificación

`05` §3.1, componente «Traductor de motivos y códigos»; [`ADR-00004`](../../05-Arquitectura-Tecnica/Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md); `05` §8, fila de códigos con traducción; [`Contratos-REST.md`](../../05-Arquitectura-Tecnica/Contratos-REST.md) §5

## 3. Criterios de aceptación

- Las **dos** traducciones en ese orden: motivo interno a código del contrato, y código del contrato a código de respuesta
- **16 de 17** códigos con traducción declarada y **1** declarado **sin destino con su motivo**
- **0** códigos inventados y **0** renombrados
- la inspección recorre el conjunto cerrado contra la tabla **en las dos direcciones**
- **ningún camino de fallo sale sin pasar por acá**

## 4. Dependencias

- BT-00002

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00024, US-00025 |
| CU upstream | CU-00009 |
| Puntos de acceso que toca | Los quince |
| Fuente de arquitectura | ADR-00004 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00013 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T03 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
