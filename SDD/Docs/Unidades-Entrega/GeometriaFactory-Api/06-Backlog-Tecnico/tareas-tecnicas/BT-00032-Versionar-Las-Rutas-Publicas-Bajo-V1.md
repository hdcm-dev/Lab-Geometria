# BT-00032 — Versionar las rutas públicas bajo `/v1/`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00032-Versionar-Las-Rutas-Publicas-Bajo-V1.md
**Versión:** 2.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** feature
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Versionar las rutas públicas bajo `/v1/`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 7

## 3. Criterios de aceptación

- Las rutas públicas quedan bajo `/v1/`
- la batería de integración existente (BT-00022) corre **en verde** contra las rutas versionadas
- no queda ninguna ruta pública sin versión de MAJOR en su prefijo
- las rutas efectivamente publicadas bajo `/v1/` se comparan **en las dos direcciones** contra los **quince** puntos de acceso de `05` §3.4: ninguno de los quince queda sin `/v1/` y ningún prefijo `/v1/` corresponde a una ruta que `05` §3.4 no declare

## 4. Dependencias

- BT-00027
- BT-00029
- BT-00030

## 5. Tipo

`feature`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita BT-00034 y BT-00035 |
| CU upstream | — (sin CU: versionado de ruta) |
| Puntos de acceso que toca | Los quince, bajo `/v1/` |
| Fuente de arquitectura | [`ADR-00010`](../../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) (supera parcialmente a ADR-00008) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00032 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1). El criterio 1 se sostiene en `ADR-00008` (§2 y §7). El criterio 4 exige, para toda tarea que toque la superficie, declarar la comparación **en las dos direcciones**: esta tarea toca los quince puntos y no la declaraba. Se agrega un cuarto criterio de aceptación en §3, citando `05` §3.4 (la tabla de los quince puntos, ya existente), que la implica: ninguno de los quince queda sin `/v1/` y ningún prefijo `/v1/` corresponde a una ruta ajena a esa tabla. **Ningún otro criterio cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **§4 retira la dependencia de `BT-00028`**, que pasó a `Descartada` por [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) (la API autentica personas, no aplicaciones; **Aceptado**). Quedan tres dependencias: `BT-00027`, `BT-00029` y `BT-00030`. **Ningún criterio de aceptación ni fuente cambia.** Reevaluación del criterio 5 de `Definition-Of-Ready.md` §2.1: **Sí** (tres dependencias, sin ciclo, verificado con `tsort` en `Audit/DoR-Tramo-k-2026-09-12.md` 1.2). Sigue **Ready**. |
| 1.3 | 2026-09-12 | **Implementación** en la rama `fase-k/bt-00032-rutas-v1`, base `ce1c68f` (`main`, fusión de `BT-00033`). Commits: `0fa8c5d` (código, `feat(api)!`) y el de documentación que cierra esta fila. **Qué se hizo.** Un único `MapGroup("/v1")` en `Program.cs` con la cifra en `Endpoints/ContractRoutePrefix.cs`; `DataServiceClient` del Web antepone `v1/` (`ApiBaseUrl` sigue siendo el anfitrión: el túnel enruta por nombre de host y `API_BASE_URL` del despliegue **no cambia**); batería de integración, E2E, samples `api/01`, `api/02`, `web/01-datos-seed` y guiones de `scripts/` y `tools/` con `/v1/`; `Contratos-REST.md` **1.8** §3.1 y `Definicion-Superficie-HTTP.md` **1.11**; entrada `BREAKING` en `changelog.md`. **Decisión que `ADR-00010` §7 remitió a esta tarea: `/salud` (`A-16`), `/openapi/v1.json` y `/documentacion` quedan fuera del prefijo**, enumeradas como exentas en `ContractRoutePrefix.ExemptRoutes` y fundadas en `Contratos-REST.md` §3.1 (`ADR-00007` §2 punto 3, `05` §3.4 «Arranque y salud», los dos `healthcheck` que piden `/salud`, `ADR-00010` §8 que compara el prefijo contra lo que `/salud` informa, `ADR-08008`). **`SI NO RESPONDÉS`, queda así: `/salud` no se versiona.** Es lectura del árbol y no decisión de producto escrita; si el Product Owner la refuta, cambian `ContractRoutePrefix.ExemptRoutes`, `ContractRoutePrefixTests`, los dos `healthcheck` y `HealthPath` del front. **Sin prefijo: `404`, sin redirección**, medido. **Los cuatro criterios de §3, con evidencia.** (1) Rutas bajo `/v1/`: `git grep -n -E 'Map(Get\|Post\|Put\|Delete\|Group)\(' -- src/GeometriaFactory.Api` → 17 `Map{Get,Post,Delete}` en `Endpoints/` + 1 `MapGroup` en `Program.cs`; de los 17, 16 se mapean sobre el grupo y 1 (`/salud`) sobre `app`. (2) Batería en verde, `mcr.microsoft.com/dotnet/sdk:10.0`, `dotnet build -c Release -p:SkipVisorBuild=true` **0 advertencias**; `dotnet test GeometriaFactory.sln -c Release -p:SkipVisorBuild=true` → **Domain 94/94, Application 56/56, Integración 382/382 = 532/532** (`main`: 522; los diez nuevos son `ContractRoutePrefixTests`). Samples contra el servicio de la rama (puerto 5081): `api/01-basico` **CONFORME 13/13**, `api/02-intermedio` **CONFORME** con su `D-1` ya declarada, `api/03-avanzado` **CONFORME** con su `D-3` ya declarada y **17 operaciones** en `/openapi/v1.json`, `web/01-datos-seed` **CONFORME 13/13**; **ningún snapshot esperado cambia**. (3) Ninguna ruta pública sin `MAJOR` salvo las tres exentas: lo afirma `EveryPublishedRouteCarriesTheVersionPrefixExceptTheThreeExempt` sobre `EndpointDataSource`, y `git grep -n -E '"/(auth\|works\|accounts)' -- src tests samples` → 2 líneas, las dos legítimas (la constante relativa `TokenRoute` y el caso del `404`); el barrido más ancho por `auth/token\|cuenta/contrasena\|cuentas\|trabajos\|interpretaciones\|aprovisionamiento` sin `/v1/` deja sólo constantes compuestas con `ContractPrefix`, rutas relativas de los E2E sobre base `<api>/v1/`, las páginas del front y las cinco rutas sin prefijo que la prueba del `404` ejerce a propósito. (4) Dos direcciones: `ContractRoutePrefixTests` transcribe los **diecisiete** de `Contratos-REST.md` §3 —que absorben los quince de `05` §3.4 más `A-17` y `A-18`— y contrasta contra lo publicado: **16 bajo `/v1/` y `A-16` exento; 0 de más, 0 de menos**. **Probada fallando**: con `app.MapGet("/sin-prefijo")` y `contrato.MapGet("/con-prefijo-pero-ajena")` agregados a mano fallan 3 de 10 y la salida nombra las dos rutas. `git tag -l` → `v0.1.0 v0.2.0 v0.5.0 v0.7.0 v0.8.0`, sin cambios. Enlaces relativos de los dos `.md` tocados: todos resuelven; anchos de tabla: la fila 1.8 de `Definicion-Superficie-HTTP.md` §10 tiene una celda de más **desde `main`** y no se toca (§4.1 de la norma: las filas de control de cambios no se reescriben). **Por qué `En curso` y no `Done`:** el despliegue en producción —reconstruir la imagen desde `main` con esta fusión y la etiqueta `v1.0.0` del evento de `BT-00033`— es del Product Owner; los E2E de Playwright no se corrieron localmente (corren en CI); y la exención de `/salud` queda con `SI NO RESPONDÉS`. `05` §3.4 sigue diciendo «quince» (sin `A-17` ni `A-18`) desde antes de esta tarea: no se toca acá. |
| 2.0 | 2026-09-12 | **Cierre: pasa a `Done`.** Los tres pendientes que la 1.3 dejó declarados se resolvieron con hechos posteriores a la fusión. **(a) Fusión y etiqueta.** PR #195 fusionado en `main` = `8e5e2f9`. **Etiqueta anotada `v1.0.0` creada sobre `8e5e2f9` y empujada** (`git rev-parse v1.0.0^{commit}` = `8e5e2f9`; GitHub `refs/tags/v1.0.0` → objeto `295e7c0`): primera etiqueta manual bajo [`../../09-Devops/Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) 5.1 (`PD-VER-01`), y la salida de `0.x` que `BT-00033` 2.0 dejó a cargo de esta tarea. **(b) Producción reconstruida desde `main`** (`docker compose up -d --build`, `~/docker/lab-geometria`); ambos contenedores `healthy`. Verificado por el orquestador el 2026-09-13 02:48–02:52 UTC: `curl https://api-geometria.aplicada.stream/salud` → `{"ready":true,"version":"1.0.0+8e5e2f947c05d83b1e33cb40e35bf89bac70ef71",…}`; `GET /v1/trabajos` (sin acceso) → `401`; `GET /trabajos` (sin prefijo) → `404`; `POST /v1/auth/token` (credencial falsa) → `401`; `GET /openapi/v1.json` → `200`; front `/estado` → `200` y muestra `1.0.0+8e5e2f9…`; el log del web registra `GET http://lab-geometria-api:8080/v1/aprovisionamiento` y `GET …/salud`, es decir, `DataServiceClient` antepone `v1/` al contrato y no a la salud, tal como la 1.3 lo describe. **(c) La exención queda ratificada.** La decisión que la 1.3 dejó con `SI NO RESPONDÉS` —`/salud` (`A-16`), `/openapi/v1.json` y `/documentacion` fuera del prefijo— la **ratifica el Product Owner** («ok, encargate de todo»): **`/salud` no se versiona**. Deja de ser lectura del árbol y pasa a decisión de producto escrita, en `Contratos-REST.md` 1.8 §3.1 y en [`ADR-00010`](../../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) 1.1 §7, que deja de remitirla acá. `ContractRoutePrefix.ExemptRoutes`, `ContractRoutePrefixTests`, los dos `healthcheck` y `HealthPath` del front quedan como están. **Veredicto final de los cuatro criterios de §3.** (1) Rutas públicas bajo `/v1/` — **cumple**: medido en la 1.3 sobre el árbol (16 sobre el grupo, `/salud` sobre `app`) y en producción sobre `8e5e2f9` (`/v1/trabajos` → `401`, la guardia responde detrás del prefijo). (2) Batería de `BT-00022` en verde contra las rutas versionadas — **cumple**: 532/532 en la 1.3, sin cambio de código entre esa medición y `8e5e2f9`. (3) Ninguna ruta pública sin `MAJOR` en su prefijo — **cumple**: `EveryPublishedRouteCarriesTheVersionPrefixExceptTheThreeExempt` sobre `EndpointDataSource`, y las tres exentas ya no son una salvedad provisoria sino una decisión ratificada; en producción, `/trabajos` → `404` sin redirección. (4) Dos direcciones contra los quince de `05` §3.4 — **cumple**: `ContractRoutePrefixTests` contrasta los diecisiete de `Contratos-REST.md` §3 (los quince más `A-17` y `A-18`) contra lo publicado, 0 de más y 0 de menos, probada fallando en la 1.3. **Los E2E de Playwright** siguen sin correrse localmente: corren en CI sobre la fusión, y el recorrido de producción de (b) ejerce el front contra la API versionada. `05` §3.4 sigue diciendo «quince»: no se toca acá, como en la 1.3. Alcance respetado: sin `src/`, sin `BT-00029`, sin `BT-00030`, sin `Arquitectura-Unidad-Entrega.md`. Sube **major** y no 1.4 porque el paso a `Done` cambia el estado de vida de la tarea, como `BT-00027`, `BT-00031` y `BT-00033` (2.0). |
