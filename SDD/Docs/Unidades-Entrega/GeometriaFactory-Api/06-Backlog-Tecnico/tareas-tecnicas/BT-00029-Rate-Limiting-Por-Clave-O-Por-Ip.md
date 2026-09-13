# BT-00029 — Rate limiting por persona autenticada o por IP

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md
**Versión:** 1.2
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Rate limiting por persona autenticada o por IP.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 4; `05` §8.1 (`GeometriaFactory-Api`), fila **«Caudal sostenido»** ([`ADR-00005`](../../05-Arquitectura-Tecnica/Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md)), que deriva su cifra «de la limitación de escritor único del almacén»

## 3. Criterios de aceptación

- `Microsoft.AspNetCore.RateLimiting` aplicado **por persona autenticada** (el identificador que viaja en la credencial firmada) para los puntos bajo la guardia, y **por dirección de origen** para los que no exigen acceso; nunca por una clave de aplicación, que no existe ([`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md))
- una batería que excede la cuota recibe `429`
- el único escritor SQLite no se satura bajo la carga sintética que la batería ejercita

## 4. Dependencias

Ninguna.

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: protege el recurso que BT-00006 y BT-00022 ya miden |
| CU upstream | — (sin CU: cuota de uso) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §8.1 (`GeometriaFactory-Api`), fila «Caudal sostenido», [`ADR-00005`](../../05-Arquitectura-Tecnica/Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00029 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban `05` §9 con el identificador `RS-OPE-01`, que **no existe en `05`**: es un identificador del registro de mesa `Mesa-2026-09-12.md` §119, y §9 (riesgos) de las cuatro capas de `05` no lleva identificadores de fila. Existe, en cambio, una fuente admitida por el criterio 1 (un NFR de §8) que ya sostiene el mismo hecho técnico: `05` §8.1 fila «Caudal sostenido», que deriva su cifra «de la limitación de escritor único del almacén» y cita [`ADR-00005`](../../05-Arquitectura-Tecnica/Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md). Se corrige la cita en §2 y en §7 a esa fila; **ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Propagación de [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)** (la API autentica personas, no aplicaciones; **Aceptado**). El sujeto del límite de tasa deja de ser «una clave» —no hay claves de aplicación— y pasa a ser **la persona autenticada** (identificador de la credencial) o, para los cuatro puntos que no exigen acceso, **la dirección de origen**. Cambian el título, §1 y el primer criterio de §3; los otros dos criterios no cambian. §4 retira la dependencia de `BT-00028`, que pasó a `Descartada`: queda **sin dependencias**. El archivo conserva su nombre para no romper los enlaces del catálogo, del Mini-Plan y de la auditoría. Reevaluación del criterio 5 de `Definition-Of-Ready.md` §2.1: **Sí** (sin dependencias, sin ciclo); los demás criterios no se ven afectados. Sigue **Ready**. |
