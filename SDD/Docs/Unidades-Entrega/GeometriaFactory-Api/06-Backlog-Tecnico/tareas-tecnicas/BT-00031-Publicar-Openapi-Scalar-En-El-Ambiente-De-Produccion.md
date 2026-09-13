# BT-00031 — Publicar OpenAPI/Scalar en el ambiente de producción

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00031-Publicar-Openapi-Scalar-En-El-Ambiente-De-Produccion.md
**Versión:** 2.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** devops
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Publicar OpenAPI/Scalar en el ambiente de producción.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 6; [`ADR-08008`](../../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md), **Estado: Aceptado**, §2 punto 2: «El explorador navegable existe —Scalar, en `/documentacion`— y no se publica solo. En desarrollo está siempre; fuera de desarrollo hace falta decir `Documentacion__Publicada=true`.» Es una ADR, fuente admitida por el criterio 1 de la DoR

## 3. Criterios de aceptación

- `Documentacion__Publicada=true` en el ambiente de producción
- `/openapi/v1.json` responde `200`
- Scalar sirve la documentación pública sin exigir credencial de alumno
- el recuento de puntos de acceso fuera de la guardia sigue en exactamente **4** (`05` §8.1, §3.4: `A-01`, `A-02`, `A-03`, `A-16`) después de publicar, porque `/openapi/v1.json` y `/documentacion` no son puntos de la superficie de `02` sino del explorador que `ADR-08008` §2 declara

## 4. Dependencias

Ninguna.

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**, sostenida por `ADR-08008` (criterio 2 de la DoR): ya construido, sólo cambia la configuración de publicación |
| CU upstream | — (sin CU: documentación pública) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | [`ADR-08008`](../../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md) §2 punto 2 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00031 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1): **queda en Borrador**. El criterio 1 no se cumple: §2 y §7 citan sólo `Mesa-2026-09-12-ciclo-2.md` §4 ítem 6, un registro de mesa que no está en la lista admitida. Se buscó una fuente admitida —componente de `05` §3.1, ADR, NFR de §8, riesgo de §9, punto abierto de §11 de las cuatro capas, punto de acceso de la superficie de `02`, regla de delivery del intake §15— para «publicar OpenAPI/Scalar en producción»: **no existe ninguna**. Ni `05` ni el intake §15 mencionan Scalar, OpenAPI ni la variable `Documentacion__Publicada`. Queda **abierto** hasta que la categoría 05 (o el Product Owner) registre esta decisión en una fuente admitida. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Corrección de un error, detectada por el orquestador contra el árbol.** La v1.1 declaró que «ninguna fuente del árbol» sostiene publicar OpenAPI/Scalar, pero la búsqueda de v1.1 se limitó a `05` (`Arquitectura-Unidad-Entrega.md`) **de esta unidad de entrega**. Existe [`ADR-08008`](../../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md), **nivel Producto, Estado: Aceptado**, cuya §2 punto 2 declara: «El explorador navegable existe —Scalar, en `/documentacion`— y no se publica solo. En desarrollo está siempre; fuera de desarrollo hace falta decir `Documentacion__Publicada=true`.» — una ADR, categoría admitida por el criterio 1. Se agrega esa cita en §2 y en §7, y un cuarto criterio de aceptación con umbral cero: `/openapi/v1.json` y `/documentacion` no son puntos de la superficie de `02` (no figuran en `A-01`…`A-18` de `05` §3.4 ni en `Definicion-Superficie-HTTP.md`), y el código ya los mapea (`src/GeometriaFactory.Api/Composition/ApiDocumentation.cs`: `MapOpenApi`, `MapScalarApiReference`); publicar en producción cambia configuración, no agrega puntos, y el recuento de `05` §8.1 (exactamente 4 fuera de la guardia: `A-01`, `A-02`, `A-03`, `A-16`, de `05` §3.4) sigue en 4. Reevaluación de los seis criterios de `Definition-Of-Ready.md` §2.1: **1: Sí** (`ADR-08008` §2 punto 2, una ADR); **2: Sí** (§7: infraestructura compartida sostenida por `ADR-08008`, criterio 2); **3: Sí** (los tres criterios originales son verificables por inspección/petición HTTP, y el cuarto fija umbral cero con la condición de medición: `05` §8.1/§3.4); **4: N/A** (§7: «Puntos de acceso que toca: Ninguno»; no agrega punto a la superficie de `02`); **5: Sí** (§4: «Ninguna», sin dependencias); **6: N/A** (tipo `devops`, no indagación). Los seis dan: pasa a **Ready**. Evidencia y corrección en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md), sección BT-00031 reevaluada. |
| 2.0 | 2026-09-12 | **Cierre: pasa a `Done`.** Sin rama de código: la tarea cambia configuración de un despliegue que ya existía, no el árbol. El Product Owner reconstruyó producción desde `main` = `ce1c68f` (fusión #193, `BT-00033`) con `docker compose up -d --build` en `~/docker/lab-geometria` y **`Documentacion__Publicada=true` en `.env`**; el orquestador verificó el 2026-09-13 02:23 UTC, sin credencial, contra `https://api-geometria.aplicada.stream`: `curl -sS …/salud` → `{"ready":true,"version":"0.8.1-alpha.0.135+ce1c68f3a37275c57861c7f2a86035a8f4699001","serverTimeUtc":"2026-09-13T02:23:5…"}`; `curl -sS -o /dev/null -w "%{http_code}" …/openapi/v1.json` → `200`; `curl -sS -o /dev/null -w "%{http_code}" …/documentacion` → `302`; `docker compose ps` → `lab-geometria-api healthy · lab-geometria-web healthy`. Antes de la reconstrucción `/openapi/v1.json` daba `404` y `/salud` decía `1.0.0+5c95dab…`. **Criterio 1 — cumple**: `Documentacion__Publicada=true` está en el `.env` del despliegue, y la prueba de que la variable surtió efecto es el cambio de `404` a `200` sobre el mismo punto sin ningún cambio en `src/` (el mapeo ya existía: `ApiDocumentation.cs`, `MapOpenApi`, `MapScalarApiReference`). **Criterio 2 — cumple**: `/openapi/v1.json` responde `200` en producción. **Criterio 3 — cumple**: las dos peticiones se hicieron sin `Authorization` y ninguna devolvió `401`; `/documentacion` responde `302`, que es la **redirección propia del explorador Scalar** hacia su ruta canónica, no un rechazo de la guardia ni un punto de la superficie de `02`. **Criterio 4 — cumple, con umbral cero**: `05` §8.1 y §3.4 no se tocaron y el recuento de puntos fuera de la guardia sigue en exactamente **4** (`A-01`, `A-02`, `A-03`, `A-16`); `/openapi/v1.json` y `/documentacion` no entran en ese recuento porque no son puntos de la superficie sino el documento y el explorador que `ADR-08008` §2 punto 2 declara, y publicarlos fue un cambio de configuración, no de código. Alcance respetado: sin `src/`, sin rutas (`BT-00032`), sin evento de etiqueta (`BT-00033`). Sube **minor** por indicación del orquestador para este cierre (el precedente de `BT-00027` 2.0 subió major por el mismo motivo; la diferencia se declara y no se disimula). Sube **major** y no 1.3 porque el paso a `Done` cambia el estado de vida de la tarea, como BT-00027 (2.0). |
