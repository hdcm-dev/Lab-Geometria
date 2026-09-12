# BT-00027 — Reescribir `ADR-00008` adoptando `/v{MAJOR}/` para la superficie pública

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00027-Reescribir-Adr-00008-Adoptando-V-Major-Para-La-Superficie-Publica.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Reescribir `ADR-00008` adoptando `/v{MAJOR}/` para la superficie pública.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 1; `PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Versionado del contrato»; [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) vigente

## 3. Criterios de aceptación

- El ADR declara el cambio de premisa respecto de `ADR-00008` vigente («sin versionado de rutas y despliegue conjunto») y adopta `/v{MAJOR}/` para la superficie pública, con el `MAJOR` compartido con el SemVer del producto (`D-03`)
- el documento anterior pasa a `Superado` o queda reemplazado por `ADR-00009` con el enlace cruzado en los dos sentidos

## 4. Dependencias

Ninguna.

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita a las ocho tareas restantes de esta épica |
| CU upstream | — (sin CU: exposición pública, `PRODUCT-INTAKE` 4.3) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | ADR-00008 (a reescribir) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00027 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
