# BT-00028 — Autenticación por cliente para terceros (API key o `client_credentials`)

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md
**Versión:** 1.2
**Estado:** Descartada
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Autenticación por cliente para terceros (API key o `client_credentials`).

**Descartada el 2026-09-12 por [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md): no hay nada que construir.** La API autentica personas, no aplicaciones; no existe una tercera clase de identidad a la que darle una clave. El contenido que sigue se conserva como registro de lo que la tarea pedía.

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
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1): **queda en Borrador**. El criterio 1 no se cumple en la letra: §2 y §7 citan `Mesa-2026-09-12-ciclo-2.md` §4 (un registro de mesa) y `PRODUCT-INTAKE` §17.1.P.5 (una sección del intake que no es su §15), ninguna de las dos admitida por el criterio; el ítem diferido `D-01` tampoco lo es, porque no figura como punto abierto de `05` §11. Se buscó una fuente admitida que sostuviera «autenticación por cliente» en `05` §3.1 (componentes), §8 (NFR), §9 (riesgos) y §11 (puntos abiertos) de las cuatro capas, y en la superficie de `02`: **no existe ninguna**. La decisión de diseño ya está tomada en el intake (§17.1.P.5, API key o `client_credentials`), pero **no tiene ADR propia** en `05`; escribirla es trabajo de la categoría 05 (una ADR nueva), no de esta ficha, y esta ficha no la inventa. Queda **abierto** hasta que `05` emita esa ADR. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Pasa a `Descartada`** (`Rules-Backlog-Tecnico.md` §4.1) con fundamento en [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md), **Aceptado** por decisión del Product Owner del 2026-09-12: «no hay tercero — es re simple: un administrador, y luego usuarios generales, que pueden ser cualquiera, entre estos alumnos». La API autentica personas y no aplicaciones: dos identidades (administrador y usuario), `POST /auth/token` con las credenciales de la persona como única vía para toda aplicación propia, **sin claves de API ni `client_credentials`**. La ADR que la detención de DoR pedía (`Audit/DoR-Tramo-k-2026-09-12.md` §4) existe, y lo que dice es que **esta tarea no se construye**. `D-01` queda cerrado sin ítem diferido nuevo (`PRODUCT-INTAKE` **4.4** §17.1.P.3/§17.1.P.5). Las tareas que la declaraban dependencia (`BT-00029`, `BT-00030`, `BT-00032`) la retiran en su propia ficha. §1 recibe la marca; §2 a §7 se conservan como registro. |
