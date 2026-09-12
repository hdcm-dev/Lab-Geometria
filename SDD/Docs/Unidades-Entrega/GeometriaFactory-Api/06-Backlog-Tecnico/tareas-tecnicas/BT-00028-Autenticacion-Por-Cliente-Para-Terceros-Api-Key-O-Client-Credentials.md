# BT-00028 — Autenticación por cliente para terceros (API key o `client_credentials`)

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md
**Versión:** 1.0
**Estado:** Borrador
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Autenticación por cliente para terceros (API key o `client_credentials`).

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 3; `PRODUCT-INTAKE` **4.3** §17.1.P.5 (autenticación por cliente); el ítem diferido **D-01** condiciona quién es el cliente y no el mecanismo

## 3. Criterios de aceptación

- Un cliente con clave válida (API key o `client_credentials`, RFC 6749 §4.4) recibe `200`
- con clave inválida o revocada recibe `401`
- existe mecanismo de revocación por cliente
- el ROPC de los alumnos (BT-00010, BT-00011) **no cambia de comportamiento**

## 4. Dependencias

- BT-00027

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita BT-00029 y BT-00030 |
| CU upstream | — (sin CU: cliente externo) |
| Puntos de acceso que toca | Ninguno bajo `/v1/` todavía |
| Fuente de arquitectura | `PRODUCT-INTAKE` §17.1.P.5 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00028 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
