# BT-00033 — Adoptar MinVer y etiquetar los commits de producción

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00033-Adoptar-Minver-Y-Etiquetar-Los-Commits-De-Produccion.md
**Versión:** 1.3
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** devops
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Adoptar MinVer y etiquetar los commits de producción.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 8; `D-03` confirmado (MinVer); `05` §11.3 (`GeometriaFactory-Application`) `PA-06` y §11.2 (`GeometriaFactory-Domain`) `PA-04` — los dos puntos abiertos sobre «la herramienta que calcula la versión», hoy vigentes y reasignados a la fase `i`, que `D-03` resuelve adoptando MinVer

## 3. Criterios de aceptación

- `/salud` informa la versión SemVer real calculada por MinVer
- el evento de etiqueta es la fusión a `main` que cambia código de producción, calculado por Conventional Commits
- los commits `89f3ab3` y `5c95dab` quedan etiquetados o se declara explícitamente por qué no

**Veredicto al 2026-09-12** (detalle en §8, filas 1.2 y 1.3):

| Criterio | Estado | Quién lo cierra |
| --- | --- | --- |
| `/salud` informa la versión SemVer real | **Cumplido en producción**: el Product Owner reconstruyó desde `main` = `ce1c68f` y `https://api-geometria.aplicada.stream/salud` responde `"version":"0.8.1-alpha.0.135+ce1c68f3a37275c57861c7f2a86035a8f4699001"` (verificado el 2026-09-13 02:23 UTC). La altura 135 prueba que el `git fetch --unshallow --tags` del `Dockerfile` corrió sobre el clon superficial de BuildKit | Cerrado por el Product Owner, `docker compose up -d --build` |
| El evento de etiqueta | **Cumplido**: declarado en `Estrategia-Versionado.md` §3.c y **decidido** por el Product Owner el 2026-09-12 («ok, encargate de todo y sigue», que acepta los defaults): `PD-VER-01` **etiqueta manual** (opción b), la crea quien fusiona código de producción a `main` con el número que MinVer imprime; el job de CI no se implementa. `PD-VER-02` **conjunto amplio**: `Directory.Build.props`, `*.csproj`, `.sln` y `deploy/Dockerfile*` cuentan como código de producción | Cerrado por el Product Owner, `Estrategia-Versionado.md` 5.1 §3.c |
| `89f3ab3` y `5c95dab` etiquetados, o declarado por qué no | **Cerrado por la segunda vía del criterio**: `PD-VER-03` decidido **sin etiquetas retroactivas**. Por qué no: no se pueden calcular —cero `feat`/`fix` en 129 fusiones— y etiquetar es irreversible en el remoto; la propuesta `v0.9.0`/`v0.9.1` queda registrada como **descartada**. La regla rige desde su fecha y las fusiones anteriores quedan fuera de alcance, como `c`, `d` y `f` en §1 punto 4. La primera fusión con rutas `/v1/` (`BT-00032`) etiqueta **`v1.0.0`** | Cerrado por el Product Owner, `Estrategia-Versionado.md` 5.1 §3.c |

## 4. Dependencias

Ninguna.

## 5. Tipo

`devops`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: cierra `05` §11.3 (`GeometriaFactory-Application`) `PA-06` y §11.2 (`GeometriaFactory-Domain`) `PA-04` |
| CU upstream | — (sin CU: herramienta de versión) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §11 `PA-06` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00033 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Ejecución** sobre la rama `fase-k/bt-00033-minver`, base `main` = `cfb11f7` al iniciar y **rebasada sobre `ffc3d71`** (fusión #192 de `BT-00027`) antes de cerrar, con la verificación (1), (2) y (3) repetida sobre el HEAD rebasado: 0/0, 522/522, `MinVer: Calculated version 0.8.1-alpha.0.135` (`main` = 134). **Hecho**: `Directory.Build.props` adopta **MinVer 8.0.0** (`PackageReference` exacto, `PrivateAssets="All"`, `MinVerTagPrefix=v`, sin más configuración); `/salud` **no necesitó cambio en `src/`** porque `HealthEndpoint.ReadVersion()` ya lee `AssemblyInformationalVersion`; `Estrategia-Versionado.md` **5.0** (§3.1–§3.4 herramienta, **§3.c** evento con `PD-VER-01/02/03`); `05` **3.11** cierra `PA-04` (Domain) y `PA-06` (Application); `changelog.md`. **Hallazgo que la ficha no anticipaba, y su reparación**: el despliegue del Product Owner construye desde `https://github.com/…#main` con `BUILDKIT_CONTEXT_KEEP_GIT_DIR`, y BuildKit hace ese checkout con `--depth=1 --no-tags` (`source/git/source.go`, `fetch … --depth=1 --no-tags` y `fetch -u --depth=1`); **reproducido**: sobre ese clon MinVer calcula `0.0.0-alpha.0+<sha>` con **0 advertencias**. Los dos `Dockerfile` completan el historial (`git fetch --unshallow --tags origin`) cuando `.git/shallow` existe, tolerando la falla, y `ci.yml` pide `fetch-depth: 0`. **Verificación** (todo en `mcr.microsoft.com/dotnet/sdk:10.0`, SDK 10.0.400, con `-p:SkipVisorBuild=true` porque la imagen no tiene Node): **(1)** `dotnet build GeometriaFactory.sln -c Release --no-restore` → `Build succeeded. 0 Warning(s) 0 Error(s)`, **igual que sobre `main`** (0/0 medido antes del cambio); **(2)** `dotnet test GeometriaFactory.sln -c Release` → Domain 94/94, Application 56/56, Integration 372/372, **522/522**, exit 0; **(3)** `dotnet build … -p:MinVerVerbosity=normal` → `MinVer: Using { Commit: 8ec80a4, Tag: 'v0.8.0', Version: 0.8.0, Height: 133 }. MinVer: Calculated version 0.8.1-alpha.0.133.`; `AssemblyInformationalVersion` del `GeometriaFactory.Api.dll` publicado con `-p:SourceRevisionId=$(git rev-parse HEAD)`: `0.8.1-alpha.0.133+cfb11f754aaacc9aa76d78c95a40db477e429d02`; **camino de producción**: `docker build -f deploy/Dockerfile` sobre un clon `--depth=1 --no-tags` con `origin` = URL de GitHub → registro `El clon es superficial: se completa el historial…` y `curl /salud` sobre la imagen → `{"ready":true,"version":"0.8.1-alpha.0.133+cfb11f754aaacc9aa76d78c95a40db477e429d02",…}`; **(4)** `git tag -l` → `v0.1.0 v0.2.0 v0.5.0 v0.7.0 v0.8.0`, **sin cambios** respecto de `main`; **(5)** enlaces relativos de los cuatro `.md` tocados resueltos contra el árbol: 48 + 241 + 5 + 6, **0 rotos**; anchos de tabla: ninguna fila nueva con recuento de celdas distinto de su encabezado (las filas 2.2 y 3.0 de `Estrategia-Versionado.md` §12 y 3.0 a 3.4 de `05` §12 ya venían con una celda de más y no se tocan). **Cálculo para las etiquetas retroactivas**: `git log --format=%s main \| grep -Ec '^(feat\|fix)(\(\|!\|:)'` → `0`; `git log --oneline --merges v0.8.0..89f3ab3 \| wc -l` → 128, `89f3ab3..5c95dab` → 1, `5c95dab..main` → 4; de las 129 fusiones hasta `5c95dab`, **25** tocan `src/` o `visor/`; `89f3ab3` toca sólo `deploy/Dockerfile.web`, `5c95dab` toca 4 archivos de `src/`+`visor/`. **Pasa a En curso**: cierra cuando el Product Owner responda `PD-VER-01` y `PD-VER-03` y reconstruya producción. |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de ambigüedad**. §2 y §7 citaban «`05` §11 `PA-06`» sin decir de qué capa: **esta ficha vive en `GeometriaFactory-Api`, cuyo propio `PA-06` es «RESUELTO. El alcance de la colección de peticiones» (§18, ocho escenarios) y no tiene relación con MinVer**. El `PA-06` que corresponde es el de `GeometriaFactory-Application` (§11.3): «la herramienta que calcula la versión... no está elegida», hoy **vigente** y reasignado a la fase `i`; `GeometriaFactory-Domain` §11.2 trae el mismo punto abierto como `PA-04`. Se corrige la cita en §2 y en §7 a los dos, calificados por capa. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.3 | 2026-09-12 | **Cierre: pasa a `Done`.** Los tres criterios cierran con dos hechos posteriores a la 1.2. **(a) Producción reconstruida.** El Product Owner corrió `docker compose up -d --build` en `~/docker/lab-geometria` desde `main` = `ce1c68f` (fusión #193 de esta tarea), con `Documentacion__Publicada=true` en `.env` (`BT-00031`). Verificado por el orquestador el 2026-09-13 02:23 UTC: `curl -sS https://api-geometria.aplicada.stream/salud` → `{"ready":true,"version":"0.8.1-alpha.0.135+ce1c68f3a37275c57861c7f2a86035a8f4699001","serverTimeUtc":"2026-09-13T02:23:5…"}`; `docker compose ps` → `lab-geometria-api healthy · lab-geometria-web healthy`. Antes decía `1.0.0+5c95dab…`. **Criterio 1 — cumple**: la versión es la que MinVer calcula sobre `v0.8.0` + 135 fusiones, y la altura 135 —no `0.0.0-alpha.0`— prueba que el `git fetch --unshallow --tags origin` de `deploy/Dockerfile` corrió sobre el clon `--depth=1 --no-tags` de BuildKit, que era el hallazgo de la 1.2. **(b) Decisiones del Product Owner** del 2026-09-12 («ok, encargate de todo y sigue»: acepta las propuestas con sus `SI NO RESPONDÉS`), registradas en [`../../09-Devops/Estrategia-Versionado.md`](../../09-Devops/Estrategia-Versionado.md) **5.1** §3.c. **Criterio 2 — cumple**: el evento está declarado en §3.c y cómo se materializa quedó decidido: `PD-VER-01` **manual** (opción b) —la etiqueta la crea quien fusiona código de producción a `main`, con el número que MinVer imprime; el job de CI **no se implementa**—; `PD-VER-02` **conjunto amplio** —`Directory.Build.props`, `*.csproj`, `GeometriaFactory.sln` y `deploy/Dockerfile*` cuentan como código de producción—. **Criterio 3 — cumple por su segunda vía («o se declara explícitamente por qué no»)**: `PD-VER-03` decidido **sin etiquetas retroactivas** para `89f3ab3` ni `5c95dab`; la propuesta `v0.9.0`/`v0.9.1` queda registrada como descartada. El porqué está escrito: el número no se puede calcular (cero `feat`/`fix` en las 129 fusiones desde `v0.8.0`, medido en la 1.2), etiquetar es irreversible en el remoto, y una regla que rige desde su fecha puede declarar fuera de alcance lo anterior sin inventar un punto de anclaje, igual que §1 punto 4 hizo con `c`, `d` y `f`. **Consecuencia sobre la serie**: la salida de `0.x` la hace la primera fusión con rutas `/v1/` (`BT-00032`), que etiqueta **`v1.0.0`**, coherente con `ADR-00010` (MAJOR de la ruta = MAJOR del producto). `git tag -l` sigue sin cambios; ninguna etiqueta se creó en esta corrida. Alcance respetado: sin `src/`, sin rutas (`BT-00032`), sin deprecación (`BT-00035`). Sube **minor** por indicación del orquestador para este cierre (el precedente de `BT-00027` 2.0 subió major por el mismo motivo; la diferencia se declara). |
