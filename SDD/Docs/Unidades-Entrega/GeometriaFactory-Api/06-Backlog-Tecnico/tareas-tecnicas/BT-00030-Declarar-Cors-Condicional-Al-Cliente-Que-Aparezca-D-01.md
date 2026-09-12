# BT-00030 — Declarar CORS, condicional al cliente que aparezca (`D-01`)

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00030-Declarar-Cors-Condicional-Al-Cliente-Que-Aparezca-D-01.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** indagación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Declarar CORS, condicional al cliente que aparezca (`D-01`).

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 5; ítem diferido **D-01** (`PRODUCT-INTAKE` §9/§17.1.P.3, `Mesa-2026-09-12.md` §8)

## 3. Criterios de aceptación

- Mientras `D-01` siga abierto, el documento de arquitectura declara **explícitamente por qué CORS no aplica hoy** (no hay cliente de navegador de otro origen conocido)
- si `D-01` cierra con un cliente JavaScript de otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: hasta que `D-01` cierre**

## 4. Dependencias

- BT-00028

## 5. Tipo

`indagación`. si `D-01` cierra con un cliente JavaScript de otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: hasta que `D-01` cierre**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: depende del ítem diferido `D-01` y no lo duplica |
| CU upstream | — (sin CU: condicionado a `D-01`) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | Ítem diferido `D-01` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00030 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
