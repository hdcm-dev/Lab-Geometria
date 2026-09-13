# BT-00034 — Sample de onboarding para un cliente externo contra `/v1/`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00034-Sample-De-Onboarding-Para-Un-Cliente-Externo-Contra-V1.md
**Versión:** 2.0
**Estado:** Done
**Fecha:** 2026-09-13
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Sample de onboarding para un cliente externo contra `/v1/`.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 9; `05` §8.1 (`GeometriaFactory-Api`), última fila **«Pasos de la colección de peticiones reproducible»** (5 o menos, 0 datos inventados, [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)) — el mismo NFR que ya sostiene a BT-00020, del que esta tarea es análoga

## 3. Criterios de aceptación

- Un cliente de referencia corre contra `/v1/` **sin conocer el código fuente**, en **cinco pasos o menos**, análogo a BT-00020: se autentica **como una persona** por `POST /auth/token` con correo y contraseña, que es la única vía de toda aplicación propia ([`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)); lo que lo distingue de BT-00020 es que corre **sin conocer el código fuente** y contra las rutas versionadas

## 4. Dependencias

- BT-00032

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: extiende BT-00020 al consumidor externo |
| CU upstream | — (sin CU: onboarding externo) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §8.1 (`GeometriaFactory-Api`), fila «Pasos de la colección de peticiones reproducible», [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00034 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban únicamente el registro de mesa, no admitido. Esta tarea es declaradamente análoga a BT-00020, que cita `05` §8, última fila («Pasos de la colección de peticiones reproducible», `ADR-00008`); se agrega la misma cita acá, porque sostiene el mismo criterio de forma (cinco pasos o menos, cero datos inventados) que esta tarea reclama sin fuente propia. **Ningún criterio de aceptación cambia**. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Propagación de [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)** (la API autentica personas, no aplicaciones; **Aceptado**). El criterio de §3 decía «autenticado con clave propia de cliente externo y no con el acceso de un alumno»; esa clave no existe. El sample se autentica **como una persona** por `POST /auth/token`, igual que BT-00020, y lo que lo distingue es que corre sin conocer el código fuente y contra `/v1/`. El título conserva «cliente externo» con el sentido que `PRODUCT-INTAKE` **4.4** §17.1.P.3 le da: una aplicación propia distinta del front. §4 no cambia (`BT-00032`); esta ficha no dependía de `BT-00028` de forma directa, y la transitiva se retiró en `BT-00032` 1.2. Sigue **Ready**. |
| 2.0 | 2026-09-13 | **Implementación y cierre: pasa a `Done`.** Rama `fase-k/bt-00034-sample-onboarding`, base `1748503` (`main` = `v1.1.0`). Commits: `32f0b9e` (el sample y su documentación) y el de esta fila. **Dónde quedó.** [`/samples/api/04-cliente-http-basico/`](../../../../../../samples/api/04-cliente-http-basico/), gobernado por [`../../10-Examples/ejemplo-04-cliente-http-basico-api.md`](../../10-Examples/ejemplo-04-cliente-http-basico-api.md) 1.0 con `VER-00004`; fila en [`../../10-Examples/README.md`](../../10-Examples/README.md) 2.1 (§2, §3, desvío 4 de §6), `SD-00004` en [`../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) 2.3 y fila en `samples/README.md` 2.1. **El slug** es `cliente-http-basico`, uno de los cerrados de `Rules-Examples.md` §3.1 y el que §2.3 reserva para el cliente HTTP de una `rest-api`; entra como carpeta extra (`04-`) sin renombrar las tres anteriores. `onboarding-externo` no es un slug admitido y no se acuñó. **Qué hace, en cinco pasos y un solo comando** (`API_EMAIL=... API_PASSWORD=... bash run.sh`, con `API_BASE_URL` por omisión `https://api-geometria.aplicada.stream`): (1) `GET /openapi/v1.json` y lista las operaciones con `awk` —17, 16 bajo `/v1/` y `/salud` exenta—; (2) `POST /v1/auth/token` con las credenciales de **una persona** ([`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)); (3) `GET /v1/trabajos`; (4) `POST /v1/trabajos` con `E-1` del intake §20 transcripto de `api/02-intermedio/cuerpos/E1.txt` byte a byte —`201`, `Pendiente`, 2 advertencias, 0 errores—; (5) `GET /v1/trabajos` en bucle hasta el primer `429`, con tope, y lee `Retry-After` ([`../../05-Arquitectura-Tecnica/Contratos-REST.md`](../../05-Arquitectura-Tecnica/Contratos-REST.md) §4.1). Sólo `curl`, `bash`, `awk` y `sed`; ninguna dirección, correo ni contraseña escrita; el acceso firmado se usa y no se imprime; el README dice qué verificar en cada paso. `verificar.sh` compara contra `esperado/salida.txt` y corre solo sobre una salida guardada. **Contra qué se corrió, y cómo levantarlo** (no contra producción, que tiene cuota): `docker run -d --name gf-bt34 --network host -w /w -u 1000:1000 -v <worktree>:/w -v <almacén>:/datos -v <nuget>:/nuget -e HOME=/tmp -e DOTNET_CLI_HOME=/tmp -e NUGET_PACKAGES=/nuget -e ASPNETCORE_ENVIRONMENT=Production -e Documentacion__Publicada=true -e Kestrel__Endpoints__Http__Url=http://0.0.0.0:5081 -e AccessToken__SigningKey=<clave propia> -e 'ConnectionStrings__Store=Data Source=/datos/bt34.db' mcr.microsoft.com/dotnet/sdk:10.0 bash -lc 'cd src/GeometriaFactory.Api && dotnet run -c Debug'`, con los umbrales del límite de tasa **por omisión**; la caché de NuGet del anfitrión es de `root`, así que se usó una propia. La persona se preparó **por fuera del sample**, con `curl`, como lo haría el administrador: `POST /v1/cuentas/administrador`, `POST /v1/cuentas`, canje del administrador, `POST /v1/cuentas/{id}/situacion` (`Enabled`, devuelve la provisoria) y `POST /v1/cuenta/contrasena`. **Salida de la corrida** (2026-09-13, desde una copia de la carpeta en un directorio sin `src/`): `[1 contrato] GET /openapi/v1.json: 200 \| operaciones: 17 \| bajo /v1/: 16 \| exentas del prefijo: /salud` · `[2 canje] POST /v1/auth/token: 200 \| acceso firmado recibido: si \| papel: Student` · `[3 listado] GET /v1/trabajos: 200 \| es una lista: si` · `[4 envio] POST /v1/trabajos con E-1: 201 \| estado del trabajo: Pendiente \| advertencias: 2 \| errores: 0` · `[5 cuota] GET /v1/trabajos hasta el limite: 429 recibido: si \| Retry-After presente: si \| entre 1 y 60: si \| sin cuerpo: si` · `Pasos ejecutados: 5 \| Respuestas comparadas: 6 \| Diferencias: 0` → **`CONFORME · las 6 líneas coinciden con el snapshot de §6`**, salida 0. Informado y no comparado: 59 peticiones en el paso 5 hasta el `429` (la 61.ª de la persona en la ventana, con `Retry-After: 60`), 63 en total. `verificar.sh` probado **fallando**: con `Pendiente` cambiado por `Borrador` en la línea 4 → `NO CONFORME · 1 línea(s) difieren sin motivo declarado`, salida 1; `run.sh` sin credenciales → salida 2 con el aviso. **El criterio de §3, con evidencia.** *Corre sin conocer el código fuente*: **cumple** — la carpeta se copió a un directorio fuera del repositorio y corrió igual; `grep -rn "src/\|scripts/\|SDD/" samples/api/04-cliente-http-basico/run.sh` → 0. *Cinco pasos o menos*: **cumple** — cinco pasos del recorrido en un solo comando, y cinco pasos de «Cómo correrlo» en el documento (copiar, exportar credenciales, dirección opcional, correr, leer). *Contra `/v1/`*: **cumple** — los tres puntos van con prefijo y la línea 1 cuenta 16 bajo `/v1/`. *Como una persona por `POST /auth/token`*: **cumple** — `peticiones/01-canjear` es `POST /v1/auth/token` con correo y contraseña; no hay clave de cliente en ningún archivo (`grep -rin "api.key\|client_credentials" samples/api/04-cliente-http-basico` → sólo la variación de §7 que lo descarta). *Análogo a `BT-00020`*: **cumple** — misma forma que `api/01-basico` y `api/02-intermedio` (peticiones con marcas, cuerpos `.txt`, `escapar.awk`, snapshot), y cero datos inventados: `E-1` es copia byte a byte. **Verificación**: `git tag -l` → `v0.1.0 v0.2.0 v0.5.0 v0.7.0 v0.8.0 v1.0.0 v1.1.0`, sin cambios; enlaces relativos de los `.md` tocados resueltos; `src/` no se tocó. **Lo no verificado**: la corrida contra `https://api-geometria.aplicada.stream` no se hizo —tiene cuota y el paso 5 la gasta a propósito—; el documento OpenAPI de producción sí se leyó (`200`, 13 KB, 17 operaciones, mismas que el local) y la única diferencia con el local es la URL de `servers`, que el sample no compara. Que el sample corra en una máquina sin `bash` (Windows sin WSL) no se probó ni se promete. **Deuda declarada, fuera de esta tarea**: `10-Examples/README.md` §1 y §3 siguen describiendo a los tres primeros samples como esqueleto y `Sin verificar` desde el 2026-08-11, cuando corren desde el 2026-08-30; esta tarea agregó el cuarto sin reescribir esos párrafos. |
