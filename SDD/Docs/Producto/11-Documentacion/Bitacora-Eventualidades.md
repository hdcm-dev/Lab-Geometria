---
doc_id: DOC-PRODUCTO-BITACORA-EVENTUALIDADES-01
doc_type: bitacora-eventualidades
title: Bitácora de eventualidades — Fábrica de Geometría
status: Vigente
rol_intervencion: [operador, mantenedor]
owner: Technical Writer / Documentation Lead (AG-00110)
version: "1.1"
last_review: 2026-08-30
momento: 2
traces:
  - Rules-Documentacion-§0.6
  - Rules-Documentacion-§4.2
---

# Bitácora de eventualidades

**Producto:** Fábrica de Geometría
**Nivel:** Producto
**Instrumento:** `Rules-Documentacion.md` §0.6 y §4.2

---

## 1. Qué es esto, y qué no es

Durante la construcción y las primeras corridas aparecen situaciones que **ningún documento de diseño podía anticipar**, porque sólo se manifiestan al ejecutar el sistema en un entorno real. Hoy se resuelven una vez, quedan en la memoria de quien las resolvió y se pierden; el siguiente las vuelve a sufrir idénticas.

**Esta bitácora es un buffer de captura, no un destino.** Cada entrada tiene que terminar propagada a un documento permanente, y el campo `destino` lo declara. **Ninguna eventualidad se cierra sin destino asignado**: si no aplica ninguna categoría, se marca `No absorbida` con su motivo.

**El campo que la hace útil es `intentos_descartados`.** Un documento permanente registra lo que funcionó; sólo la bitácora conserva lo que se probó y no, que es lo que evita que el siguiente lo repita.

**No se confunde con el sensado de deriva.** La deriva mide divergencia contra una línea de base aprobada: algo se apartó de lo acordado. Una eventualidad es un hecho del entorno que nadie había previsto: no hay línea de base de la cual apartarse.

## 2. Por qué esta bitácora nace poblada y no vacía

**Se emite el 2026-08-30, con `I-01` abierto desde el 2026-08-29**, y el hallazgo decía que el paso 3 de la fase da la bitácora por existente y no existía. Emitir un archivo vacío habría cerrado el hallazgo **sin capitalizar nada**, que es exactamente el instrumento ceremonial que estas reglas evitan.

Las entradas son reales y están fechadas: **salieron de implementar los dieciséis samples y de corregir los primeros defectos que dejaron**, entre el 2026-08-27 y el 2026-08-30. Todas cumplen la definición de §0.6 —hechos del entorno que ningún documento predijo— y todas costaron tiempo de diagnóstico.

---

## 3. Entradas

### `EVE-00001` · El servicio ignora `ASPNETCORE_URLS` y escucha en 5080

| Campo | Contenido |
| --- | --- |
| `ambito` | `GeometriaFactory-Api` |
| `fecha` | 2026-08-30 |
| `momento` | Construcción |
| `sintoma` | El contenedor arranca, el registro dice `Now listening on: http://0.0.0.0:5080`, y el puerto publicado con `-p 18099:8080` **no responde**. El registro trae además el aviso `Overriding address(es) 'http://0.0.0.0:8080'` |
| `causa` | La configuración de **Kestrel del propio proyecto** gana sobre `ASPNETCORE_URLS`. No es que la variable se ignore: se aplica y después se sobrescribe, y el aviso lo dice |
| `resolucion` | Publicar contra el puerto interno real: `-p <libre>:5080`. La variable se puede omitir |
| `intentos_descartados` | Fijar `ASPNETCORE_URLS=http://0.0.0.0:8080` —lo que produce el aviso—; fijar `ASPNETCORE_HTTP_PORTS=8080`, que produce el **mismo** aviso por otra vía |
| `destino` | `Guia-Contenedor` → prerrequisitos y puertos. **Pendiente de propagación** |

### `EVE-00002` · El contenedor deja archivos de root en el árbol del repositorio

| Campo | Contenido |
| --- | --- |
| `ambito` | `producto` |
| `fecha` | 2026-08-29 |
| `momento` | Construcción |
| `sintoma` | Después de correr `dotnet build` en contenedor, `rm -rf` desde el anfitrión falla con `Permiso denegado` sobre decenas de archivos de `bin/` y `obj/` |
| `causa` | El contenedor corre como `root` por omisión y escribe con ese dueño sobre el volumen montado |
| `resolucion` | `docker run -u "$(id -u):$(id -g)"`, y además `-e DOTNET_CLI_HOME=/tmp/dh`, porque sin `HOME` escribible el SDK se detiene con *«The user's home directory could not be determined»*. Para limpiar lo ya escrito: un contenedor efímero que borre desde adentro |
| `intentos_descartados` | `rm -rf` desde el anfitrión; `chown` desde el anfitrión, que falla por el mismo motivo |
| `destino` | `Guia-Contenedor` → cómo correr herramientas sobre el árbol montado. **Pendiente de propagación** |

### `EVE-00003` · SQLite en modo WAL deja tres archivos y no uno

| Campo | Contenido |
| --- | --- |
| `ambito` | `GeometriaFactory-Infrastructure` |
| `fecha` | 2026-08-29 |
| `momento` | Construcción |
| `sintoma` | Un proceso que crea un almacén temporal y borra el `.db` al terminar deja `geometriafactory-*.db-wal` y `-shm` en el directorio |
| `causa` | El modo WAL acompaña al archivo con un registro de escrituras no plegadas y un índice compartido. Además, `Microsoft.Data.Sqlite` reutiliza conexiones de un pozo, y una conexión viva mantiene el `-wal` abierto |
| `resolucion` | `SqliteConnection.ClearAllPools()` **y después** borrar los tres: el archivo, `-wal` y `-shm`. Es lo que `scripts/reset-db.sh` ya hacía bien y lo que el sample no hacía |
| `intentos_descartados` | Borrar sólo el `.db`; borrar los tres sin soltar el pozo primero |
| `destino` | `Runbook-Operacion` → nueva entrada `OPS-XXXXX` sobre limpieza del almacén. **Pendiente de propagación** |

### `EVE-00004` · Una página abierta con `file://` no puede leer sus archivos vecinos ni cargar módulos

| Campo | Contenido |
| --- | --- |
| `ambito` | `GeometriaFactory-Visor` |
| `fecha` | 2026-08-30 |
| `momento` | Ensayo de entrega |
| `sintoma` | La página del sample se cuelga esperando datos que nunca llegan; el conductor agota su espera de treinta segundos sin que la bandera de listo se ponga |
| `causa` | El navegador prohíbe `fetch` sobre `file://` por política de origen, **y también los módulos ES** por el mismo motivo. Las dos fallan en silencio para quien mira la página |
| `resolucion` | Los datos entran por **etiqueta de guion** —un `.js` que asigna a `window`— y el anfitrión es un **guion clásico** y no un módulo. Es lo único que funciona igual abierto a mano y conducido |
| `intentos_descartados` | `fetch('./datos/...')`; `<script type="module">`; servir la carpeta con un servidor, descartado porque §4 del ejemplo pide abrir el archivo directamente |
| `destino` | `Conceptos-Fundamentales` del visor → cómo se integra la página que lo hospeda. **Pendiente de propagación** |

### `EVE-00005` · La imagen del SDK no trae `jq`, `python3` ni `sqlite3`

| Campo | Contenido |
| --- | --- |
| `ambito` | `producto` |
| `fecha` | 2026-08-29 |
| `momento` | Construcción |
| `sintoma` | Los guiones que arman cuerpos JSON o leen el almacén fallan con `command not found` dentro del entorno contenido |
| `causa` | `mcr.microsoft.com/dotnet/sdk:10.0` trae `curl` y `openssl` y nada más de eso |
| `resolucion` | **No se agregan herramientas al entorno.** Para JSON, `samples/api/01-basico/cuerpos/escapar.awk`; para SQLite, una aplicación de un solo archivo de C#, como `tools/informe-cobertura.cs` ya hacía. Agregar una dependencia movería el anclaje de versión de la etapa `a` por comodidad de un sample |
| `intentos_descartados` | `apt-get install jq` dentro del contenedor, que además no persiste entre corridas |
| `destino` | `Guia-Contribucion` → qué hay y qué no hay en el entorno contenido. **Pendiente de propagación** |

### `EVE-00006` · Una aplicación de un solo archivo necesita `#:project`, y `JsonSerializer` la rompe

| Campo | Contenido |
| --- | --- |
| `ambito` | `producto` |
| `fecha` | 2026-08-30 |
| `momento` | Construcción |
| `sintoma` | Dos fallas seguidas: `CS0246: The type or namespace name 'GeometriaFactory' could not be found`, y después `IL2026`/`IL3050` sobre `JsonSerializer.Serialize`, tratados como error |
| `causa` | La primera, que un archivo suelto de C# no hereda las referencias de la solución. La segunda, que `Directory.Build.props` fija `TreatWarningsAsErrors` y los avisos de recorte de `System.Text.Json` **son avisos** |
| `resolucion` | Encabezar el archivo con `#:project <ruta al csproj>`, y **no usar `JsonSerializer` con reflexión**: escribir la salida a mano o usar generación de origen |
| `intentos_descartados` | Copiar el archivo dentro de un proyecto temporal; bajar `TreatWarningsAsErrors`, descartado porque es la puerta `QG-00001` |
| `destino` | `Guia-Contribucion` → cómo se escribe una herramienta de un solo archivo. **Pendiente de propagación** |

### `EVE-00007` · El puerto del servicio colisiona con el despliegue local del Product Owner

| Campo | Contenido |
| --- | --- |
| `ambito` | `producto` |
| `fecha` | 2026-08-30 |
| `momento` | Despliegue |
| `sintoma` | `docker run` falla con `Bind for 0.0.0.0:18080 failed: port is already allocated`; y el puerto 5080 lo tiene tomado un contenedor `gf-api` que **es el despliegue de trabajo** |
| `causa` | La máquina de desarrollo corre en paralelo el despliegue local del producto —`gf-api` en 5080, `gf-web` en 5090— y contenedores de otros proyectos en el rango 18080-18081 |
| `resolucion` | Los samples y las verificaciones corren en un **contenedor propio**, con puerto libre —se usó 18099— y **almacén propio** vía `ConnectionStrings__Store`. Varios samples borran cuentas y trabajos: apuntar al almacén de trabajo se lleva puesto lo que haya |
| `intentos_descartados` | Reutilizar el `gf-api` que ya corría, descartado por lo mismo que la resolución explica |
| `destino` | `Guia-Contenedor` → convivencia con el despliegue de trabajo. **Pendiente de propagación**. Se relaciona con la decisión que `scripts/store-path.sh` ya documenta desde el incidente del 2026-08-15 |

### `EVE-00008` · Una batería en verde reportada como puerta en rojo

| Campo | Contenido |
| --- | --- |
| `ambito` | `producto` |
| `fecha` | 2026-08-30 |
| `momento` | Construcción |
| `sintoma` | `scripts/coverage.sh` termina en **2** con *«NO SE PUEDE MEDIR · la batería no pasó»*, y tres líneas más arriba la misma salida dice `Passed! - Failed: 0, Passed: 344`. Las 494 pruebas pasan y la puerta dice rojo |
| `causa` | **No es la batería: es el recolector de cobertura.** Su proceso auxiliar muere con `System.IO.IOException: Broken pipe` al escribir sobre `--results-directory`, y `dotnet test` devuelve distinto de cero aunque las pruebas hayan pasado. Lo dispara el **estado acumulado** de corridas anteriores: carpetas `TestResults/` dentro de cada proyecto de prueba, algunas escritas por un contenedor que corrió como root |
| `resolucion` | Borrar `TestResults/` de la raíz **y de cada proyecto de prueba** antes de medir, y corregir el dueño si alguna corrida las escribió como root. `coverage.sh` ya borra la de la raíz; las de los proyectos no |
| `intentos_descartados` | Buscar el defecto en el cambio de código recién hecho, que era la sospecha natural; se descartó midiendo el **mismo guion sobre `main` en un árbol de trabajo limpio**, donde pasaba. Y volver a correr sin limpiar, que reproduce el fallo idénticamente |
| `destino` | `Guia-Contribucion` → cómo se corre la puerta de cobertura. **Pendiente de propagación.** Y una mejora candidata a `coverage.sh`: que borre también las `TestResults/` de los proyectos, que es donde el estado se acumula |

---

## 4. Triaje

**Ninguna está cerrada**, y el estado es correcto: `Rules-Documentacion.md` §0.6 exige que el triaje se ejecute **en cada corte del Momento 2**, y todas tienen su destino **asignado** —que es lo que la regla dura pide— y **no propagado** todavía.

| Destino | Entradas |
| --- | --- |
| `Guia-Contenedor` | `EVE-00001`, `EVE-00002`, `EVE-00007` |
| `Guia-Contribucion` | `EVE-00005`, `EVE-00006`, `EVE-00008` |
| `Runbook-Operacion` | `EVE-00003` |
| `Conceptos-Fundamentales` del visor | `EVE-00004` |
| `No absorbida` | ninguna |

**Tres de los cuatro documentos de destino todavía no existen.** Están `Planificado` en el plan documental de esta categoría, y por eso la propagación queda pendiente y no omitida: el destino está declarado y el documento que lo recibe es trabajo de esta misma categoría.

---

## Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-30 | Entra **`EVE-00008`**: una batería en verde reportada como puerta en rojo, por estado acumulado de `TestResults/` que hace morir al recolector de cobertura. Se descartó que fuera el cambio de código midiendo el mismo guion sobre `main` en un árbol de trabajo limpio. Deja además una mejora candidata a `coverage.sh`. |
| 1.0 | 2026-08-30 | Emisión inicial, que cierra el hallazgo `I-01` del incremento `I-1` de la Fase I. **Nace poblada y no vacía**, todas de la implementación de los dieciséis samples entre el 2026-08-27 y el 2026-08-30: emitir un archivo vacío habría cerrado el hallazgo sin capitalizar nada. Las siete tienen destino asignado y ninguna propagada; tres de los cuatro documentos de destino están `Planificado` en el plan de la categoría. |
