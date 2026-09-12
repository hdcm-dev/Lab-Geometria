# BT-00002 — Construir la composición de raíz con los cuatro puertos y sus adaptadores

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00002-Construir-La-Composicion-De-Raiz-Con-Los-Cuatro-Puertos-Y-Sus-Adaptadores.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T01 · Fundaciones, composición de raíz y arranque
**Etapa del producto:** `a`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Construir la composición de raíz con los cuatro puertos y sus adaptadores.

## 2. Justificación

`05` §3.1, componente «Composición de raíz»; [`ADR-00006`](../../05-Arquitectura-Tecnica/Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md); `05` §9, séptimo riesgo

## 3. Criterios de aceptación

- **4 de 4** puertos conectados a su adaptador, y **0** puertos sin adaptador o con más de uno
- la composición es **única** y no se reparte en módulos por área, porque **la frontera tiene que ser contable en un solo lugar**
- si falta una dependencia, **falla en construcción** y no hay petición que responder
- toda la configuración del despliegue entra **por acá y por ningún otro lado**

## 4. Dependencias

- BT-00001

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | US-00026 |
| CU upstream | CU-00010 |
| Puntos de acceso que toca | Ninguno: construye el grafo y desaparece |
| Fuente de arquitectura | ADR-00006 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00002 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T01 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
