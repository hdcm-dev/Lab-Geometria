# Estrategia de versionado — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Estrategia-Versionado.md
**Versión:** 5.0
**Estado:** Propuesto
**Fecha:** 2026-09-12
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Tres de las diez secciones son comunes.** Lo que la consolidación junta por primera vez son **los
linajes que el producto versiona además del suyo**: `GeometriaFactory-Infrastructure` declara dos
—las transformaciones de esquema y los parámetros de derivación de clave— que ninguna otra capa
menciona, y que **no siguen la versión del producto**.

---

## 1. Versionado semántico, y qué reemplaza al versionado de rutas

### 1.1 `GeometriaFactory-Api`

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Api lo declara **sin excepciones**, junto con las convenciones de mensaje de confirmación, una rama y un pull request por etapa, y **una etiqueta por cada etapa cerrada y fusionada, para poder volver a cualquier demostración**. Declara además que **el registro de cambios se actualiza en la rama de la etapa, no después de la fusión**.

**Y declaraba una ausencia con su sustituto, que es lo que ordena este documento.** Hasta su versión 4.2, el intake §17.1.P.3 · GeometriaFactory-Api decía que **no hay versionado de rutas porque no hay clientes de terceros**, y [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 declaraba qué lo reemplaza, en cinco reglas. **Esa premisa quedó superada el 2026-09-12** (`PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Versionado del contrato»): hay aplicaciones propias además del front que no compilan contra el ensamblado (`ADR-00009`), y [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) adopta `/v{MAJOR}/` para la superficie pública, **derogando la regla 1 de abajo y conservando las otras cuatro**. Las cinco se transcriben igual, con su estado marcado; **el evento de etiqueta (regla 4) lo reescribe `BT-00033` y la política de deprecación la agrega `BT-00035`, en este mismo documento y no en esta entrada**:

1. ~~**Una sola versión de la superficie vive a la vez**: sin prefijo de versión en las rutas, sin convivencia de dos formas de un punto y sin deprecación gradual.~~ **Derogada el 2026-09-12 por [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md)**: la superficie pública lleva `/v{MAJOR}/`, dos `MAJOR` pueden convivir durante el plazo de deprecación (`BT-00035`), y dentro de un mismo `MAJOR` sigue viviendo una sola forma de cada punto.
2. **Todo cambio del ensamblado de contratos obliga al despliegue conjunto** de esta unidad y de la pública.
3. **Tres clases de cambio no las detecta la compilación, y cada una tiene su mecanismo**: la **configuración de intercambio**, declarada una sola vez para los dos extremos; el **esquema del almacén**, verificado al arrancar con su linaje, que detiene el arranque si no cierra; y **las rutas**, que sólo el consumidor conoce y que la batería de integración ejerce contra el servicio real.
4. **Cada etapa cerrada y fusionada recibe una etiqueta**, y la reversión es volver a la etiqueta anterior y reconstruir. **Con una excepción declarada, y son las etapas `c`, `d` y `f`**: se cerraron entre el 2026-08-14 y el 2026-08-17 **sin etiqueta y sin que existiera el prefijo** que §3.b fijó recién el 2026-08-18, y sus cinco etiquetas repuestas ese día **no las pudieron cubrir sin inventar el punto de anclaje**. Los tres huecos de numeración están declarados con su motivo en [`../../../../../changelog.md`](../../../../../changelog.md) § «Los tres huecos de numeración son deliberados». **La regla rige desde la fase `i` en adelante y no se retira**: lo que se declara es que tres etapas ya cerradas quedan fuera de su alcance, porque una regla que el árbol incumple en silencio no es una regla. **Y desde el 2026-09-12 el evento cambia, no la obligación**: la etiqueta ya no espera al cierre de etapa sino a **cada fusión a `main` que cambie código de producción**, por decisión del Product Owner en la mesa del ciclo 2; la regla nueva, su motivo y lo que deja pendiente están en **§3.c**.
5. **La colección de peticiones reproducible es parte del contrato hacia afuera**, y cuando la superficie cambia, la colección cambia con ella.

**La tercera regla es la que esta categoría tiene que hacer operativa**, porque las tres clases que la compilación no detecta son las que un pipeline puede dejar pasar:

| Clase que la compilación no detecta | Dónde se la atrapa en la canalización | Umbral |
| --- | --- | --- |
| **Configuración de intercambio** divergente entre los dos extremos | `QG-00010`, en el stage `build` | **1** sola configuración declarada en el producto |
| **Esquema del almacén** que no cierra | El stage `verificar-transformaciones` de `GeometriaFactory-Infrastructure`, y después el arranque en dos fases, que **detiene el arranque** si la preparación no se completó | 0 pasos manuales; el servicio **no escucha** si no cerró |
| **Rutas** que cambian sin que el consumidor se entere | La batería de integración, que ejerce el servicio real por su protocolo | La batería entera en verde (`QG-00002`) |

**La segunda fila tiene una propiedad que las otras dos no tienen**: su falla **no se puede ignorar en ejecución**. [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2 declara que **no hay modo de sólo lectura ni arranque parcial**, con el fundamento de que un servicio que atiende sobre un almacén en el que no se puede confiar es peor que uno que no arranca: «el segundo se nota en el despliegue, el primero se nota cuando alguien busca su trabajo y no está».

## 2. Convenciones de mensaje de confirmación

### 2.1 `GeometriaFactory-Api`

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisiones propias de este proyecto de código, y las dos salen de `ADR-00008` §7.** Primera: **todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa**, de modo que el mensaje que lo introduce no puede quedar aislado en una rama que se fusione sola. Segunda: **la colección de peticiones se actualiza en la misma intervención en que cambia la superficie**; una confirmación que agrega un punto de acceso y no toca la colección deja la demostración de la etapa fallando, que es la señal correcta.

**Y una tercera que esta categoría agrega, derivada de `QG-00005`**: agregar un punto de acceso **es siempre un cambio que hay que declarar**, aunque sea aditivo, porque cambia el recuento de la guardia de admisión. No sube mayor por sí solo; lo que exige es que el pull request diga **de qué lado de la guardia queda**, y `TC-00007` lo verifica en las dos direcciones.

### 2.2 `GeometriaFactory-Domain`

Se adoptan las **Conventional Commits 1.0.0**, declaradas por el intake §17.1.P.7 · GeometriaFactory-Domain sin excepciones. El efecto sobre el número de versión es el de la tabla, y es lo que hace que la versión se calcule y no se escriba a mano:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**El prefijo no reemplaza al criterio de §1.** Un cambio marcado `feat` que en realidad quita un valor de un conjunto cerrado es un cambio mayor mal etiquetado, y lo levanta la revisión del pull request de la etapa. La convención de mensajes ordena el cálculo; **quien decide la clase es el criterio de `ADR-02003` §7**.

### 2.3 `GeometriaFactory-Application`

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** La tabla de §1 tiene **una** fila donde un cambio que se escribiría naturalmente como `feat` es **mayor**: agregar una operación a un puerto. Quien la escriba tiene que marcarla con `feat!` o con el pie de cambio incompatible **aunque el verbo del cambio sea «agregar»**. No hay herramienta que lo deduzca; lo deduce el criterio de `ADR-04003` §7 y lo verifica la revisión del pull request.

### 2.4 `GeometriaFactory-Infrastructure`

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** Toda confirmación que **agregue una transformación de esquema** queda atada a la etapa en la que entra, por la obligación de §1. En la práctica eso significa que el mensaje nombra la etapa, y que **una transformación no viaja sola a una rama de otra etapa**: sería un linaje distinto del que se aplicó en cualquier almacén ya existente.

## 3. Herramienta de cálculo de la versión

### 3.b El prefijo de etiqueta — **ítem propio**, fijado el 2026-08-18

**Esta subsección realiza el ítem 3.b de `Rules-Devops.md` §4.3**, que desde la regla **5.0** pide el
prefijo **separado de la herramienta**. Hasta la 9.19 los dos venían en el punto 3, y este documento
los contestaba juntos; **el prefijo se difería por arrastre cuando lo bloqueado era la herramienta**.
La numeración `3.b` es literal y no decorativa: se lee contra el ítem de la regla que la exige.

**Este apartado cierra un ítem obligatorio que las cuatro tablas de abajo venían difiriendo.**
`Rules-Devops.md` §4.3 punto 3 exige que este documento declare, junto con la herramienta,
**«Configuración base y prefijo de tag»**. Hasta la emisión 2.1 las cuatro filas «Prefijo de
etiqueta» contestaban *«el que se fije al anclarla, registrado en el punto de control de la etapa
`a`»*, y ese punto de control **cerró el 2026-08-13 sin registrarlo**.

| Aspecto | Decisión |
| --- | --- |
| **Prefijo de etiqueta** | **`v`** — una sola vez para el repositorio entero, no por proyecto de código |
| **Forma completa** | `v<MAJOR>.<MINOR>.<PATCH>`, sin sufijo, sobre el SemVer 2.0.0 que §1 adopta |
| **Ámbito** | El repositorio de código. Las etiquetas son del repositorio y no de cada ensamblado: hay un solo espacio de nombres de etiquetas para los siete proyectos de código |

**No se eligió por criterio propio, y esa es la diferencia que hace que se pueda escribir.** El
prefijo sale del propio `Rules-Devops.md`, que en la tabla de canales de su §4.5 escribe la forma
literal **«Sólo en tag `v<X.Y.Z>` sin sufijo»**. El framework no sólo pidió el prefijo: lo usa con
`v` en su propio texto, y las cuatro herramientas que su §4.3 punto 3 nombra —MinVer, GitVersion,
semantic-release, Nerdbank.GitVersioning— lo aceptan o lo traen por omisión.

**Fijar el prefijo NO cierra la elección de la herramienta**, y las dos cosas venían empaquetadas
en la misma fila sin necesidad: elegir `v` no exige haber elegido MinVer, y esperar a la
herramienta dejó ocho etapas sin poder etiquetarse. **`PA-06` de `05` §11 sigue abierto** —«la
herramienta que calcula la versión no está elegida»— y este apartado no lo toca. **Actualización del 2026-09-12**: la herramienta quedó elegida —MinVer, `D-03` de la mesa del ciclo 2— y `PA-06` de Application y `PA-04` de Domain cerraron; lo declaran §3.1 a §3.4 y el punto 3 se lee ahora completo.

**Y una corrección propia sobre la emisión anterior.** La fila de §3.2 decía que el prefijo quedaba
«abierto como `PD-01` de `Pipeline-CI-CD.md` §10». **Esa referencia no lo alcanza**: el `PD-01` de
§10.2 es *«la herramienta concreta de cada stage —ejecutor de pruebas, recolector de cobertura y
reglas de análisis estático»*, donde la herramienta de versionado no figura. El prefijo estaba
diferido **hacia un punto abierto que no lo cubría**, es decir sin dueño real, que es la forma en
que este mismo producto ya perdió el registro de tres etapas. Queda declarado en lugar de
corregirse en silencio, y elevado al framework como el reporte `14` de `IA.SDD.Documentacion`.

### 3.1 `GeometriaFactory-Api`

**Elegida el 2026-09-12: MinVer.** Hasta esa fecha esta subsección la declaraba sólo por su función —ninguna fuente la nombraba y `PA-07` de `05` §11 dejaba las versiones exactas ancladas en la etapa `a`, que cerró sin registrarla—. La mesa del ciclo 2 la propuso por **menor huella** ([`../../../Audit/Mesa-2026-09-12-ciclo-2.md`](../../../Audit/Mesa-2026-09-12-ciclo-2.md) §3.3) y el Product Owner la confirmó como default `D-03` (§8). La adoptó `BT-00033`. **La configuración es la del repositorio entero y vive una sola vez**, en `Directory.Build.props`, igual que la puerta `QG-01`:

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| **Herramienta y versión anclada** | **`MinVer` 8.0.0**, `PackageReference` exacto con `PrivateAssets="All"` en `Directory.Build.props`, por la regla de anclaje del intake (encabezado de la Parte C) que el propio archivo declara |
| **Configuración base** | **`MinVerTagPrefix=v`** y nada más. Sin `MinVerMinimumMajorMinor`, sin identificadores de anticipo propios, sin metadatos de construcción: cada propiedad que no está es una decisión que no hizo falta |
| **Qué calcula sobre un commit etiquetado** | La etiqueta tal cual: `v0.9.0` → `0.9.0` |
| **Qué calcula entre etiquetas** | `MAJOR.MINOR.(PATCH+1)-alpha.0.<altura>`, donde la altura es la cantidad de commits desde la etiqueta por el camino de fusiones: sobre `main` el 2026-09-12 (`ffc3d71`), `0.8.1-alpha.0.134`. **El `-alpha.0` no es un canal de anticipo** —§5.b los descarta— sino la marca de MinVer de que la construcción no corresponde a ninguna etiqueta y no se entrega: es la «versión de trabajo» que §5.2 ya declaraba |
| **Dónde vive la versión** | **En ningún archivo.** Se calcula al construir y se sella en `AssemblyInformationalVersion` como `<versión>+<sha>`; el `+<sha>` lo sigue poniendo el SDK por el `-p:SourceRevisionId` de los dos `Dockerfile`. `/salud` (`A-16`) la informa porque ya leía ese atributo: **no hubo cambio en `src/`** |
| **Qué necesita para calcular bien** | **El historial y las etiquetas.** Sobre un clon superficial sin etiquetas calcula `0.0.0-alpha.0` **sin advertir** —medido el 2026-09-12 sobre el checkout que BuildKit deja desde la URL del repositorio, que es `--depth=1 --no-tags` aunque conserve el `.git`—. Por eso `ci.yml` pide `fetch-depth: 0` y los dos `Dockerfile` completan el historial (`git fetch --unshallow --tags`) cuando `.git/shallow` existe, sin romper la construcción si no pueden |
| Prefijo de etiqueta | **`v`** — ver **§3.b**, que es el ítem propio que `Rules-Devops.md` §4.3 punto 3.b exige |
| Qué **no** calcula la herramienta | **Las tres clases de §1 que la compilación no detecta.** Ninguna herramienta de comparación de superficie vería una configuración de intercambio divergente, un esquema que no cierra ni una ruta que sólo el consumidor conoce |

### 3.2 `GeometriaFactory-Domain`

**Elegida el 2026-09-12: MinVer, la misma de §3.1 y con la misma configuración**, porque hay un solo `Directory.Build.props` y un solo espacio de etiquetas. Hasta esa fecha esta subsección la declaraba sólo por su función, y era deliberado: el intake §17.1.P.7 · GeometriaFactory-Domain decía que la calcula «la herramienta que se ancle en la etapa `a`», `ADR-02003` §6 aceptaba depender de una herramienta no elegida, y nombrarla acá habría sido inventar una decisión atada a un punto de control futuro. **Ese punto de control cerró sin registrarla, y la decisión la tomó el Product Owner el 2026-09-12** (`D-03`, mesa del ciclo 2 §8).

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión a partir de las etiquetas del repositorio y de los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | **`v`** — ver **§3.b**, que es el ítem propio que `Rules-Devops.md` §4.3 punto 3.b exige |
| Dónde se ancla | Etapa `a`, por la regla de anclaje de versiones del intake, encabezado de la Parte C: toda versión se fija explícitamente y su cambio mayor se documenta, nunca como efecto colateral de una actualización |
| Qué se registra | La elección de la herramienta y su versión: **`MinVer` 8.0.0**, anclada en `Directory.Build.props`. Era el `PA-04` de `05` §11.2, **cerrado el 2026-09-12**; el prefijo dejó de estar atado a ella en §3.0 |

**Lo que no se hace es versionar a mano.** El anti-patrón está declarado en `Rules-Devops.md` §4.8 y el intake ya lo previene al exigir el cálculo por herramienta.

### 3.3 `GeometriaFactory-Application`

**Elegida el 2026-09-12: MinVer, la misma de §3.1 y con la misma configuración.** Hasta esa fecha esta subsección la declaraba sólo por su función: `05` §11.3 registraba el punto abierto `PA-06` —«la herramienta que calcula la versión a partir de las convenciones de mensaje de confirmación no está elegida»— atado al punto de control de la etapa `a`, y `ADR-04003` §7 decía lo mismo. **`PA-06` cerró el 2026-09-12** con la decisión `D-03` del Product Owner (mesa del ciclo 2 §8) y su adopción en `Directory.Build.props`.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Dónde vive la versión | Calculada al construir, como `ADR-04003` §7 pedía; **no en el archivo de proyecto sino en `Directory.Build.props`**, que es el único archivo de construcción compartido por los veinte proyectos de la solución |
| Prefijo de etiqueta | **`v`** — ver **§3.b**, que es el ítem propio que `Rules-Devops.md` §4.3 punto 3.b exige |
| Qué **no** calcula la herramienta | La clase de cambio de la fila aditiva-mayor de §1. Ninguna herramienta de comparación de superficie la marcaría como mayor sin conocer que la cara de abajo la implementa otro proyecto de código |

### 3.4 `GeometriaFactory-Infrastructure`

**Elegida el 2026-09-12: MinVer, la misma de §3.1 y con la misma configuración.** Hasta esa fecha esta subsección la declaraba sólo por su función: el intake §17.1.P.7 · GeometriaFactory-Infrastructure remite a §17.1.P.7 · GeometriaFactory-Domain, que la anclaba en la etapa `a`, y ninguna fuente la nombraba. La decisión es la `D-03` del 2026-09-12 y vale para los cinco proyectos de código de esta unidad por igual.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | **`v`** — ver **§3.b**, que es el ítem propio que `Rules-Devops.md` §4.3 punto 3.b exige |
| Qué **no** calcula la herramienta | **Las dos clases mayores que compilan** de §1, y **el linaje de transformaciones**, que no es una versión semántica sino una secuencia ordenada |

**Y dos versiones que se anclan y no se calculan**, las dos con efecto sobre la ejecución y no sobre el número de versión de este ensamblado:

| Qué se ancla | Dónde vive el anclaje | Fundamento |
| --- | --- | --- |
| La **herramienta de transformaciones de esquema**, instalada como **herramienta local del repositorio** para que su versión quede versionada junto al código | El archivo de herramientas del repositorio, anclado en la etapa `a` | Intake §17.1.P.1 · GeometriaFactory-Infrastructure |
| El **motor de almacenamiento en su versión embebida** por el proveedor de acceso a datos | El archivo de proyecto, anclado en la etapa `a` | Intake §17.1.P.9 · GeometriaFactory-Infrastructure |

### 3.c El evento de etiqueta — **ítem propio**, fijado el 2026-09-12

**Qué cambia y por qué.** Hasta el 2026-09-12 el evento era **el cierre de etapa** (§1 punto 4, §4 y §11.2, desde el intake §17.1.P.7), «rige desde la fase `i`» — y la fase `i` llegó al despliegue real, con producción respondiendo por `/salud`, **sin que el árbol recibiera ninguna etiqueta** (la fase sigue abierta por `PT-05`, y ese es justamente el problema: un evento que espera al cierre no etiqueta lo que ya se despliega): `git tag -l` devolvía las cinco repuestas el 2026-08-18 y **129 fusiones a `main` desde `v0.8.0`** sin etiqueta, medido en [`../../../Audit/Mesa-2026-09-12-ciclo-2.md`](../../../Audit/Mesa-2026-09-12-ciclo-2.md) §2.7. Un evento que no ocurre no versiona nada. El Product Owner aprobó en esa mesa (§8, `E-04`) el evento nuevo:

| Aspecto | Decisión |
| --- | --- |
| **Evento** | **Toda fusión a `main` que cambie código de producción recibe una etiqueta** `v<MAJOR>.<MINOR>.<PATCH>` |
| **Qué es «código de producción»** | Lo que cambia el artefacto que se despliega. **Mínimo indiscutido**: `src/**` y `visor/**`. **Pendiente del Product Owner** (`PD-VER-02`, abajo): si entran también `Directory.Build.props`, los `*.csproj`, `GeometriaFactory.sln` y `deploy/Dockerfile*`, que cambian el artefacto sin cambiar una línea de `src/` — la adopción de MinVer misma es el ejemplo |
| **Número** | Lo calcula **Conventional Commits** (§2): `feat` sube MINOR, `fix` sube PATCH, `feat!` o `BREAKING CHANGE` suben MAJOR; los demás no suben. **MAJOR es compartido con el contrato REST público** (`/v{MAJOR}/`, mesa ciclo 2 §3.3): subirlo acá es subirlo allá |
| **Qué NO cambia** | El prefijo `v` (§3.b), la ausencia de sufijos en las etiquetas (§5.b), que las etiquetas sean anotadas y digan cuándo se crearon (`changelog.md` § «Las cinco etiquetas»), y la reversión como «volver a la etiqueta anterior y reconstruir» (§1 punto 4) |
| **Una fusión que no cambia código de producción** | **No recibe etiqueta**, y MinVer la versiona igual como `<última>.(PATCH+1)-alpha.0.<altura>`: se ve que no es una entrega |

**Y una condición de la propia serie histórica que la regla nueva expone.** «Calculado por Conventional Commits» supone mensajes con prefijo, y **ninguna confirmación de `main` lleva `feat` ni `fix`** —`git log --format=%s main | grep -Ec '^(feat|fix)(\(|!|:)'` → `0` el 2026-09-12; los únicos prefijos en uso son `docs` y `codigo`—. Hacia adelante la regla exige que **la fusión que cambia código de producción lleve el prefijo que dice qué sube**, y la revisión del pull request lo verifica (§2). Hacia atrás no se puede calcular nada: por eso las dos etiquetas retroactivas que `BT-00033` pide **se proponen y no se calculan** (`PD-VER-03`).

#### Decisiones pendientes del Product Owner (`Root-Rules.md` §12.2)

| Campo | `PD-VER-01` · cómo se materializa la etiqueta |
| --- | --- |
| Qué falta | Elegir **quién crea la etiqueta** en cada fusión a `main` que cambia código de producción |
| Opción (a) · **un job en `ci.yml`** | En `push` a `main`: calcula con MinVer (`dotnet minver` o la salida de la construcción), decide si sube por el prefijo del asunto de la fusión, y **crea y empuja la etiqueta** si el conjunto de archivos cambiados incluye código de producción y el commit no la tiene ya. **Costo**: ~40 líneas de flujo, `permissions: contents: write`, un `git tag -a` + `git push --tags` desde CI; una hora de trabajo y un ciclo de prueba sobre una rama. **Compra**: el instrumento pasa a ser **subproducto del acto** (§11.2), que es lo que la serie de este producto demostró que funciona. **Riesgo**: la acción es **externa e irreversible** —una etiqueta empujada con el número equivocado queda en el remoto—, y un prefijo mal puesto se convierte en versión sin que nadie la mire |
| Opción (b) · **etiqueta manual del Product Owner** | Después de fusionar, `git tag -a v<X.Y.Z> -m "…" <sha> && git push origin v<X.Y.Z>`, con el número que MinVer sugiere (`dotnet build` imprime `MinVer: Calculated version …` con `-p:MinVerVerbosity=normal`). **Costo**: cero código; un paso manual por fusión. **Riesgo**: el que §11.2 ya midió —«hay que acordarse»—: el instrumento que no es subproducto del acto se degradó tres etapas seguidas |
| Por qué no se puede hoy | Crear etiquetas desde la canalización es una acción externa e irreversible sobre el remoto, y no la aprueba nadie más que el Product Owner |
| Quién lo cierra | El Product Owner |
| En qué evento se cierra | Su respuesta sobre el informe de `BT-00033`; con (a), el flujo entra por su propio pull request y esta fila cambia a «cerrado» |
| **SI NO RESPONDÉS** | **Rige (b)**: la etiqueta la crea el Product Owner a mano, con el número que MinVer sugiere, hasta que apruebe el job. Nada se crea desde CI sin esa aprobación |

| Campo | `PD-VER-02` · qué cuenta como código de producción |
| --- | --- |
| Qué falta | Fijar el conjunto de rutas cuyo cambio dispara la etiqueta |
| Por qué no se puede hoy | La mesa dijo «código de producción» sin enumerar; `src/**` y `visor/**` son indiscutidos, y el resto —`Directory.Build.props`, `*.csproj`, `GeometriaFactory.sln`, `deploy/Dockerfile*`— cambia el artefacto sin cambiar el código |
| Quién lo cierra | El Product Owner |
| En qué evento se cierra | La misma respuesta de `PD-VER-01` |
| **SI NO RESPONDÉS** | **Rige el conjunto amplio**: `src/**`, `visor/**`, `Directory.Build.props`, `**/*.csproj`, `GeometriaFactory.sln` y `deploy/Dockerfile*`. Motivo: todo eso entra a la imagen que se despliega, y una regla que deja afuera lo que cambia el artefacto vuelve a producir el hueco que esta sección cierra |

| Campo | `PD-VER-03` · las dos etiquetas retroactivas |
| --- | --- |
| Qué falta | Etiquetar `89f3ab3` (fusión #186, «Dockerizar el front», 2026-09-06) y `5c95dab` (fusión #187, «El bundle lo genera el `.csproj`», 2026-09-12), o declarar por qué no |
| Por qué no se puede hoy | No se puede **calcular**: entre `v0.8.0` y `5c95dab` hay 129 fusiones y **cero** mensajes `feat`/`fix`/`BREAKING`; por la tabla de §2 nada sube, y por la regla propia de MinVer el siguiente número sería `0.8.1`, que **afirmaría que en 129 fusiones sólo hubo correcciones**, y `changelog.md` registra en ese tramo el recorrido del alumno, las pruebas de extremo a extremo, los dieciséis samples, el front en contenedor y el despliegue real. Un número que se calcula sobre mensajes que no llevan lo que hay que calcular no es un cálculo. Y **etiquetar es irreversible en el remoto**: lo hace el Product Owner |
| Propuesta | **`v0.9.0` sobre `89f3ab3`** —primera fusión desde la que el producto entero se construye en contenedor y se despliega en destino, MINOR porque hay función nueva desde `v0.8.0` (SemVer 2.0.0 §7) y por la convención de la serie —«`0.x` y una MINOR por» incremento entregado, y el despliegue real es el incremento de la fase `i` aunque la fase siga abierta por `PT-05`— y **`v0.9.1` sobre `5c95dab`** —lo que cambia entre las dos es cómo se genera el bundle y la estructura de la solución, sin función nueva, PATCH por la tabla de §2—. Las dos **anotadas y declaradas retroactivas** en su mensaje, como las cinco del 2026-08-18. Sobre `main` MinVer pasaría a calcular `0.9.2-alpha.0.<altura>` |
| Alternativas | **`v0.9.0` sólo sobre `5c95dab`**, que es lo que producción corre hoy (`/salud` informa `+5c95dab`) y deja a `89f3ab3` sin etiqueta con este motivo: cambió un solo archivo, `deploy/Dockerfile.web`, que no es `src/**` ni `visor/**`. O **ninguna**: la regla nueva rige desde su fecha y las 129 fusiones anteriores quedan declaradas acá como fuera de alcance, igual que `c`, `d` y `f` en §1 punto 4 |
| Quién lo cierra | El Product Owner, con `git tag -a … <sha>` y `git push origin <etiqueta>` |
| En qué evento se cierra | Las etiquetas aparecen en `git tag -l`; la ficha `BT-00033` pasa a `Done` |
| **SI NO RESPONDÉS** | **No se crea ninguna** —ninguna etiqueta la crea nadie más que el Product Owner— y `BT-00033` queda `En curso` por ese criterio, con este apartado como la declaración explícita que su tercer criterio admite |

## 4. Modelo de ramas

### 4.1 `GeometriaFactory-Api`

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Api).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, **incluida la batería de integración completa**, que vive acá y que ninguna otra canalización del producto puede correr.
- **Todo pull request que agregue o cambie un punto de acceso reejecuta `TC-00007` en las dos direcciones sobre los quince.** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 lo llama **el control que más veces hay que ejercer**.
- **Ninguna etapa se cierra sin etiqueta**, porque la reversión del servidor propio depende de ella: no hay imagen publicada a la que volver. **Con la excepción de `c`, `d` y `f`, declarada en §1 punto 4** — el absoluto describía un árbol que tiene **cinco etiquetas para ocho etapas**, y afirmarlo sin la excepción es lo que la divergencia `D-03'` de [`../../../Audit/Estado-Del-Destino-2026-08-23.md`](../../../Audit/Estado-Del-Destino-2026-08-23.md) §5 levantó. **Desde el 2026-09-12 la unidad del evento es la fusión a `main` que cambia código de producción y no la etapa** (§3.c): el cierre de etapa sigue fusionándose y por eso sigue etiquetándose, pero ya no es el único momento.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §5.

### 4.2 `GeometriaFactory-Domain`

El modelo lo declara el producto y este proyecto de código lo hereda entero. No se elige acá ninguna variante:

- **Una rama por etapa**, a partir de la rama principal, con **etiqueta al fusionar** (intake §17.1.P.7 · GeometriaFactory-Domain).
- **Un pull request por etapa, y el pull request es el punto de control** (intake §15).
- **Etapas en serie**: no se abre la rama de una etapa antes de que la anterior esté fusionada (intake §10 y §15).
- **Sin OK explícito del Product Owner no se avanza** (intake §10, restricción «etapas en serie»).

**Consecuencia sobre las reglas de protección de la rama principal**, que es lo que esta categoría sí aporta: la fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 en verde y la constancia del OK del punto de control. **No se exige un revisor humano independiente**, y no por relajación: `equipo_n` es 1 y [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 ya declara que lo que reemplaza al revisor independiente es el punto de control bloqueante de cada etapa.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §1. Las etapas `b` y `g` no producen rama de trabajo acá, y su ausencia está declarada allá.

### 4.3 `GeometriaFactory-Application`

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**, sin abrir la rama de una etapa antes de fusionar la anterior; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes y los de rechazo en revisión de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.
- **Todo pull request que agregue o cambie un caso de uso, un puerto o una condición del catálogo ejecuta las inspecciones correspondientes** —`TC-04028` en las dos direcciones y `TC-04029` sobre el caso de uso tocado—, por la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 declara.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante, exactamente como lo declara [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4.

**Las etapas que este proyecto de código toca son seis** —`a`, `c`, `d`, `e`, `f` y `h`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §5.

### 4.4 `GeometriaFactory-Infrastructure`

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1.
- **Todo pull request que agregue o cambie una transformación de esquema ejecuta el stage `verificar-transformaciones` sobre un almacén inexistente y sobre el linaje completo**, y no sólo sobre la transformación nueva. Es la cadencia propia de este proyecto de código.
- **Ninguna fusión edita una transformación ya fusionada.** Se rechaza en revisión, y su fundamento es de la fuente y no de esta categoría.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son cinco** —`a`, `c`, `d`, `e` y `f`—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §5.

## 5. Canales

### 5.b La semántica de sufijos de anticipo — **ítem propio**

**Esta subsección realiza el ítem 5.b de `Rules-Devops.md` §4.3**, que desde la regla **6.0** pide la
semántica de sufijos **separada del conjunto de canales**, por el mismo motivo por el que §3.b separó
el prefijo de etiqueta de la herramienta: *«qué canales tiene el producto puede depender de una
decisión de distribución todavía abierta; **qué sufijo lleva una preview no depende de nada**»*.

**Acá las dos mitades ya estaban contestadas, y por eso esta subsección no escribe nada nuevo.** Lo
que hace es reunir en un ítem propio lo que **las tres subsecciones de §5 y la §10.1** venían
declarando por separado, para que se lea contra el ítem de la regla que lo exige.

| Aspecto | Decisión |
| --- | --- |
| **Sufijos `-alpha`, `-beta`, `-rc`** | **No se usan**, en ningún proyecto de código de esta unidad de entrega |
| **Forma que sí se usa** | `v<MAJOR>.<MINOR>.<PATCH>` **sin sufijo**, la que §3.b fija |
| **Motivo** | Las etiquetas de este producto son **de etapa cerrada y no de anticipo** (intake §15 y §17.1.P.7 · GeometriaFactory-Api), y no hay canal donde publicar un anticipo ni integrador que lo consuma |
| **Qué lo reabriría** | Que aparezca un canal de publicación. Mientras `redistribuible` sea false y la imagen se construya en destino, no hay a quién anticiparle nada |

**No se difiere y por eso no lleva la forma de `Root-Rules.md` §12.2.** Un ítem se difiere cuando no
se puede contestar hoy; éste se contesta: **no se usan**, con su motivo y su condición de reapertura.
Declararlo como pendiente habría sido diferir algo ya decidido, que es el defecto simétrico del que
la 11.0 vino a corregir.

### 5.1 `GeometriaFactory-Api`

**No hay canales de publicación**, y hay **un** destino de despliegue.

`Rules-Devops.md` §4.3 pide declarar canales `preview` y `stable`; esa figura pertenece a artefactos que se publican y se consumen por versión. Acá el artefacto **no se publica**: el intake §17.1.P.7 · GeometriaFactory-Api declara la imagen construida **en destino desde el repositorio, sin publicar en un registro**, y `redistribuible` es false.

| Figura del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Canal `preview` | **No existe** | No hay registro ni integrador que consuma un anticipo. Lo que un anticipo compraría —probar antes de que llegue a producción— lo compra la puerta `PT-04`, que ejercita el arranque completo **antes** de que exista la oportunidad de desplegar |
| Canal `stable` | **Se corresponde con el único destino**: el servidor propio | Intake §17.1.P.7 · GeometriaFactory-Api |
| Despliegue **canario** | **No existe.** Sin proxy inverso no hay despliegue con solapamiento, y el almacén tiene **escritor único** | Intake §17.1.P.8 · GeometriaFactory-Api y §17.1.P.12 · GeometriaFactory-Api; [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 |
| Sufijos de anticipo `-alpha`, `-beta`, `-rc` | **No se usan** | Las etiquetas del producto son **de etapa cerrada**, no de anticipo (intake §15 y §17.1.P.7 · GeometriaFactory-Api) |

### 5.2 `GeometriaFactory-Domain`

**No hay canales, y el motivo no es una omisión de esta categoría.** El intake §17.1.P.7 · GeometriaFactory-Domain declara que esta biblioteca **no se publica en ningún feed** y que se compila dentro de `GeometriaFactory.sln`; el intake §13 lo generaliza al producto entero. Sin feed no hay canal `preview` ni canal `stable` a los que promover: serían dos nombres sin destino.

`Rules-Devops.md` §2.2 fija para el tipo `library` un modelo de canales `preview` / `stable` sobre feed único y admite quitar ambientes «con un ADR que lo justifique». **Ese ADR existe y es anterior a esta categoría**: [`ADR-02003`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md), que evaluó la publicación en un repositorio de paquetes interno como alternativa y la descartó porque el intake la descarta explícitamente y porque agregaría infraestructura a un producto que las fuentes declaran básico. El apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

**Sufijos de versión de anticipo.** El formato admite `-alpha`, `-beta` y `-rc`, pero **este proyecto de código no los usa**, porque no hay canal donde publicar un anticipo ni integrador que lo consuma. La versión que la herramienta calcula entre etiquetas es de trabajo y no se entrega a nadie.

### 5.3 `GeometriaFactory-Application`

**No hay canales de publicación.** El intake §17.1.P.7 · GeometriaFactory-Application, por remisión a §17.1.P.7 · GeometriaFactory-Domain, declara que no se publica en ningún feed, y §13 lo generaliza al producto entero: **ningún proyecto de código se publica como paquete redistribuible**. `05` §5 lo repite en su última fila.

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único y admite apartarse con un ADR que lo justifique: **el ADR existe y es [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md)**, cuyo §2 declara que no se publica en ningún repositorio de paquetes y que por eso **no hay deprecación gradual, ni versiones conviviendo, ni consumidor externo al que avisar**. El apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.

**Tampoco se usan sufijos de anticipo** —`-alpha`, `-beta`, `-rc`—: no hay canal donde publicar un anticipo ni integrador que lo consuma. Los dos consumidores compilan contra el estado del repositorio.

## 6. Política de cambios incompatibles

### 6.1 `GeometriaFactory-Api`

Esta sección reemplazaba a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo lo fundaba [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2: **no hay a quién darle plazo**, porque el único consumidor es del mismo producto. **Desde el 2026-09-12 sí hay a quién darle plazo** —las aplicaciones propias que no compilan contra el ensamblado, [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.3—, y **la política de deprecación que esta sección tiene que sumar la escribe `BT-00035`** (un cuatrimestre, cabecera `Deprecation`; `D-02`), no esta entrada. Mientras tanto rigen las convenciones impuestas de `ADR-00008` §7 y sus métricas de §8 **que `ADR-00010` conserva** —la primera fila de abajo está derogada—:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| ~~**Ninguna ruta lleva prefijo ni sufijo de versión**~~ **Derogada por [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md)**: todo punto del contrato lleva `/v{MAJOR}/` (`BT-00032`); ningún punto convive con una forma anterior de sí mismo **dentro de un mismo `MAJOR`** | Comparación en las dos direcciones entre las rutas publicadas y `Contratos-REST.md` §3 | `ADR-00010` §8, primeras tres métricas |
| **Todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa** | `QG-08008` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa**; revisión de cada etapa que toque el ensamblado | Intake §17.1.P.3 · GeometriaFactory-Contracts; `ADR-00008` §8, sexta métrica |
| La **colección de peticiones** se actualiza en la misma intervención en que cambia la superficie, **se reproduce en cinco pasos o menos y no inventa datos de prueba** | `QG-00015`, con `TC-00035`, al cierre de la etapa que la incorpora | `ADR-00008` §7 y §8, cuarta y quinta métrica |
| **0** etapas cerradas sin etiqueta | Inspección del historial | `ADR-00008` §8, tercera métrica |
| Un punto de acceso nuevo **declara de qué lado de la guardia queda** | `QG-00005`, con `TC-00007` en las dos direcciones. **Exactamente 4 fuera, ni uno más** | `05` §9, primer riesgo |
| Todo cambio mayor recibe su fila en el registro de cambios del producto, **escrita en la rama de la etapa** | Revisión del pull request, que **es** el punto de control | Intake §17.1.P.7 · GeometriaFactory-Api |

**Las seis métricas de `ADR-00008` §8 se adoptan sin agregar ninguna**, y las seis figuran arriba o en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §5.

**Y una ausencia que `ADR-00008` §2 sostiene y esta categoría no reabre**: **la pasarela de reenvío del front no se implementa**. El intake la declara **especificada y no implementada**, y su condición de reingreso está escrita: descarga de archivos, carga directa desde el navegador o migración del front a ejecución en el navegador. **Ninguna de las tres está en el tramo comprometido**, y por eso esta canalización no la contempla.

### 6.2 `GeometriaFactory-Application`

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno**. Lo que rige en su lugar sale de `ADR-04003` y de la Definition of Done:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Ante un cambio mayor, **las dos caras se corrigen en la misma etapa** | Imposible por construcción: el artefacto de agrupación no compila. Se verifica en cada pull request | `ADR-04003` §2 y §8, segunda métrica |
| **0** advertencias de construcción | `QG-04001`, en el stage `build` | `ADR-04003` §8, primera métrica |
| **0** paquetes publicados en un repositorio de paquetes | Inspección del pipeline | `ADR-04003` §8, tercera métrica |
| **0** etapas cerradas sin etiqueta | Inspección de etiquetas contra el índice de informes de cierre | `ADR-04003` §8, cuarta métrica |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request de la etapa, que **es** el punto de control | Intake §15, regla de delivery 3; `changelog.md` del árbol del intake §16 |
| Una condición retirada del catálogo **no recicla su identificador** | Revisión, con la fila «quitar una condición del catálogo, o reciclar su identificador» de §1 | `ADR-04003` §7 |

**Las cuatro métricas de `ADR-04003` §8 se adoptan sin agregar ninguna.** La segunda es la más fuerte del documento y conviene no perderle el sentido: su modo de verificación es «imposible por construcción», y eso es exactamente lo que compra la compilación compartida. Donde la compilación no llega —el reciclado de un identificador de condición— el filtro es la revisión, y por eso figura como fila propia.

## 7. Versionado semántico

### 7.1 `GeometriaFactory-Domain`

**Se adopta el versionado semántico en su versión 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Domain lo declara «sin excepciones», junto con las convenciones de mensaje de confirmación.

**Qué gobierna la versión acá, que es la pregunta que hay que contestar en un proyecto de código que no se publica.** [`ADR-02003`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) §2 la contesta: gobierna la **compatibilidad de compilación de los dos consumidores del dominio**, `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`. Esta categoría no reabre esa decisión y no agrega criterios: transcribe el criterio de §7 de esa ADR porque es el que el pipeline tiene que hacer cumplir.

| Clase | Qué la produce, según `ADR-02003` §7 |
| --- | --- |
| **Mayor** | Quitar o renombrar un tipo, una operación o un atributo de la superficie pública; cambiar qué recibe una operación; **quitar un valor de un conjunto cerrado** —los cuatro estados del trabajo, los tres estados de cuenta, los dos papeles, las dos especies de observación—; y **perder cualquiera de los nueve invariantes**, aunque ninguna firma cambie |
| **Menor** | Agregar un tipo, una operación o un atributo opcional; **agregar un valor a un conjunto cerrado**, que obliga al consumidor a contemplarlo pero no rompe su compilación; agregar una condición de error al catálogo |
| **Parche** | Corregir el comportamiento de una guarda para que cumpla el invariante que ya declaraba, sin cambiar la superficie |

**La fila que conviene no perder de vista es la última de «mayor»**: perder un invariante es cambio mayor aunque ninguna firma se toque. No lo detecta ninguna herramienta de resolución de dependencias; lo detecta `QG-02006`, que exige los **nueve** invariantes ejercidos con prueba de violación rechazada y sin dobles.

**Desde cuándo hay superficie que versionar.** `ADR-02003` §2 declara que la superficie pública empieza a ser estable en el **punto de control de la etapa `a`**, cuando se fijan los nombres de tipos y de espacios de nombres que el intake §17.1.P.11 · GeometriaFactory-Domain deja abiertos. Todo lo anterior es prehistoria de versionado y no genera cambio mayor.

### 7.2 `GeometriaFactory-Application`

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Application declara la estrategia de este proyecto de código **idéntica a la de §17.1.P.7 · GeometriaFactory-Domain**: versionado semántico, convenciones de mensaje de confirmación, **sin publicación en feed**, y una rama y una etiqueta por etapa.

**Qué gobierna la compatibilidad acá, y no lo decide esta categoría.** [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) §2 lo decide: **el contrato se protege por compilación compartida y no por descripción formal ni por convivencia de versiones**, un cambio incompatible rompe la compilación del artefacto de agrupación, y la política es corregir las dos caras **en la misma etapa**.

**La superficie de este proyecto de código tiene dos caras, y de ahí sale su asimetría propia.** `ADR-04003` §2 la declara y esta categoría la transcribe sin tocarla: **agregar una operación a un puerto es cambio mayor**, porque obliga a todo implementador a proveerla, mientras que agregar un caso de uso es cambio menor. La tabla de clases se toma de `ADR-04003` §7 sin agregarle ni quitarle nada:

| Cambio sobre la superficie | Cara | Clase |
| --- | --- | --- |
| Quitar o renombrar un caso de uso, o cambiar su postcondición | Hacia arriba | **Mayor** |
| Cambiar qué exige resuelto un caso de uso antes de invocarlo | Hacia arriba | **Mayor** |
| Quitar, renombrar o cambiar la firma de una operación de un puerto | Hacia abajo | **Mayor** |
| **Agregar** una operación a un puerto existente | Hacia abajo | **Mayor**, por la asimetría de `ADR-04003` §2 |
| Agregar un puerto nuevo | Hacia abajo | **Mayor** |
| Quitar una condición del catálogo de `03`, o reciclar su identificador | Las dos | **Mayor** |
| Agregar un caso de uso | Hacia arriba | Menor |
| Agregar una condición al catálogo de `03` | Las dos | Menor |
| Corregir un orquestador para que ejerza la comprobación que ya declaraba | Ninguna | Parche |

**La fila que hay que leer dos veces es la cuarta.** Es contraintuitiva —agregar suele ser menor— y es la única de las nueve donde un cambio aditivo sube mayor. El motivo es que la cara de abajo es un contrato **que otro implementa**: `GeometriaFactory-Infrastructure` tiene que proveer la operación nueva, y hasta que la provea el artefacto de agrupación no compila.

### 7.3 `GeometriaFactory-Infrastructure`

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.1.P.7 · GeometriaFactory-Infrastructure declara la estrategia **idéntica a la de §17.1.P.7 · GeometriaFactory-Domain** —versionado semántico, convenciones de mensaje, sin publicación en feed, una rama y una etiqueta por etapa— **y le agrega una obligación propia**, que es el eje de este documento: **cada transformación de esquema se versiona con el código de su etapa, y no se editan transformaciones ya fusionadas**.

**Qué gobierna la compatibilidad de la superficie de código.** Este proyecto de código **implementa** los cuatro puertos que `GeometriaFactory-Application` declara, y su único consumidor es la composición de raíz de `GeometriaFactory-Api` (intake §14). La compatibilidad se protege por **compilación compartida**: un cambio incompatible rompe la construcción del artefacto de agrupación antes que la ejecución.

| Clase de cambio sobre la superficie de código | Ejemplo | ¿Lo detecta la compilación? |
| --- | --- | --- |
| **Mayor** | Un adaptador deja de implementar una operación del puerto que declara | Sí |
| **Mayor** | Cambia el comportamiento observable de un adaptador sin cambiar su firma: una consulta de listado empieza a cargar componentes de pieza | **No.** Lo detecta `QG-06010`, con umbral **0** |
| **Mayor** | Cambia lo que se conserva del texto original del alumno | **No.** Lo detecta `QG-06011`, con umbral **0** |
| **Menor** | Se agrega un adaptador para un puerto nuevo que la capa de aplicación declaró | Sí, si falta |
| **Parche** | Se corrige un adaptador para que cumpla lo que ya declaraba | — |

**Las dos filas del medio son las que importan acá.** Son cambios mayores **que compilan**, y las dos tocan lo que la fuente protege con más fuerza: la regla de no cargar componentes en los listados (intake §17.1.P.12 · GeometriaFactory-Infrastructure) y la conservación íntegra del texto original (`RN-06008`, intake §17.1.P.11 · GeometriaFactory-Infrastructure punto 2). Ninguna herramienta de comparación de superficie las vería; las ven `QG-06010` y `QG-06011`, y por eso son gates.

## 8. Política de obsolescencia y de cambios incompatibles

### 8.1 `GeometriaFactory-Domain`

**No hay política de plazos de obsolescencia, y declararlo es la respuesta correcta.** Una política de obsolescencia existe para dar tiempo de migración a integradores que no controlás. Acá los **dos** consumidores son proyectos de código del mismo producto, se compilan en el mismo artefacto de agrupación y en la misma ejecución del pipeline: un cambio incompatible **rompe su compilación en el acto**, que es el aviso más temprano y más barato que puede existir. Prometer «dos versiones menores antes de remover» sería una promesa hecha a nadie.

Lo que sí hay, y es obligatorio:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Todo cambio mayor recibe su **fila en el registro de cambios del producto**, `changelog.md` | Revisión del pull request de la etapa. Objetivo: **0** cambios mayores sin fila | `ADR-02003` §7 y §8 |
| Un cambio mayor exige que los **nueve** invariantes se verifiquen por prueba antes de fusionar | `QG-02006`, con `TC-02026` | `ADR-02003` §8, cuarta métrica |
| Un elemento que se va a quitar se marca como obsoleto en la superficie antes de removerse, dentro de la misma etapa o de la siguiente | Revisión del pull request | Decisión de esta categoría: es lo único que la ausencia de plazos deja sin cubrir, y no cuesta nada en un producto de dos consumidores compilados juntos |
| Toda etapa cerrada lleva su etiqueta | Inspección de etiquetas contra la lista de etapas cerradas. Objetivo: **100 %** | `ADR-02003` §8, segunda métrica |

**La reversión se apoya en la etiqueta y no en el retiro de una versión publicada**: ver [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7.

## 9. Los dos linajes que este proyecto de código versiona además del suyo

### 9.1 `GeometriaFactory-Infrastructure`

Es lo que distingue a este documento de los de las otras cuatro bibliotecas del producto: **acá hay dos secuencias que sobreviven al despliegue y que no son la versión del ensamblado**.

| Linaje | Qué es | Regla que lo gobierna | Qué pasa si se rompe |
| --- | --- | --- | --- |
| **Transformaciones de esquema** | La secuencia ordenada que lleva un almacén desde inexistente hasta el esquema en uso | **Se versiona con el código de su etapa y no se edita una ya fusionada** (intake §17.1.P.7 · GeometriaFactory-Infrastructure); el linaje es **inmutable** ([`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) | Un almacén existente tiene aplicado un linaje que ya no coincide con el del código. **Volver a una etiqueta anterior no lo deshace**: el esquema del almacén no se recompila |
| **Parámetros de la derivación de clave** | Los parámetros con los que se derivó cada contraseña guardada | **Se versionan junto al valor derivado, sin valor por defecto silencioso** ([`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)) | Un cambio de parámetros dejaría sin verificar las contraseñas ya guardadas si no se conservara con qué se derivó cada una |

**Los dos son la razón por la que la reversión de este proyecto de código no es simétrica.** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 lo declara: volver a la etiqueta anterior revierte el código, **no el almacén**. Una transformación equivocada se corrige **con otra transformación**, nunca editando la anterior; y el guion de restablecimiento, que sí deja el almacén como en el primer arranque, **no es un camino de producción** (`05` §5).

**Y una consecuencia que el producto ya declaró y esta categoría no reabre**: el intake §17.1.P.4 · GeometriaFactory-Infrastructure declara el respaldo como **copia del archivo con el diario activo**, consistente, con **frecuencia a definir por el docente**. Es el único mecanismo declarado para volver atrás sobre datos, y su cadencia **no la fija esta categoría**.

## 10. Canales y política de cambios incompatibles

### 10.1 `GeometriaFactory-Infrastructure`

**No hay canales de publicación.** El intake §17.1.P.7 · GeometriaFactory-Infrastructure, por remisión a §17.1.P.7 · GeometriaFactory-Domain, declara que no se publica en ningún feed, y §13 lo generaliza al producto. `05` §5 lo repite en su última fila. `Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único; el apartamiento queda desarrollado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1. **Tampoco se usan sufijos de anticipo**: no hay canal donde publicar uno ni integrador que lo consuma.

Esta sección reemplaza además a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, con el mismo fundamento que en el resto del producto —**no hay integrador externo a quien dar plazo**— y con las obligaciones que sí rigen:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| **Ninguna transformación ya fusionada se edita** | Revisión del pull request de la etapa | Intake §17.1.P.7 · GeometriaFactory-Infrastructure; [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |
| Las transformaciones **se aplican solas sobre un almacén inexistente**, sin paso manual | `QG-06004`, en el stage `verificar-transformaciones` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure, criterio de aceptación de la etapa `c` |
| **0** advertencias de construcción | `QG-06001`, en `build` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure |
| **0** componentes de pieza y **0** apariciones del texto original en una proyección de listado | `QG-06010`, con `TC-06019` | Es una de las dos clases mayores que compilan (§1) |
| **0** escrituras que reemplacen el texto original conservado | `QG-06011`, con `TC-06016` y `TC-06021` | La otra clase mayor que compila (§1) |
| **0** etapas cerradas sin etiqueta | Inspección del historial contra el índice de informes de cierre | Intake §15 y §17.1.P.7 · GeometriaFactory-Infrastructure |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request, que **es** el punto de control | Intake §15, regla de delivery 3 |
| Los parámetros de derivación **viajan junto al valor derivado**, sin valor por defecto silencioso | Revisión, contra [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | El mismo ADR |

**La primera fila es la única obligación de versionado de todo el producto que alcanza a un dato que sobrevive al código.** Las demás protegen la construcción o la ejecución; ésa protege **almacenes que ya existen y que ninguna canalización toca**.

## 11. Registro del avance y su responsable

**Esta sección responde a los ítems 7 y 8 de `Rules-Devops.md` §4.3**, y se escribe con el caso
observado de este mismo producto a la vista.

### 11.1 Qué documento declara el avance, quién lo actualiza y en qué evento

| Campo | Valor |
| --- | --- |
| **Documento que declara el avance** | `changelog.md`, en la raíz del repositorio de código. Es el único documento que declara en qué etapa va el producto |
| **Quién lo actualiza** | **El equipo de desarrollo** —una persona más el agente de IA, `PRODUCT-INTAKE` §2— en la rama de la etapa. La responsabilidad no se delega al agente: quien abre el pull request responde por la fila |
| **Quién verifica que se actualizó** | **El Product Owner**, en la revisión del pull request de la etapa, que `PRODUCT-INTAKE` §15 declara punto de control bloqueante |
| **En qué evento** | **Antes de fusionar la rama de la etapa**, no después. Es la regla que la primera línea del propio `changelog.md` declara desde su emisión |
| **Si ningún rol correspondiera** | La organización dueña del repositorio, la cátedra de Programación 2. No es el caso acá, y se declara para que el orden de resolución de `Master-Prompt-Reanudacion.md` §1.1 R2 quede cerrado |

**Por qué esta fila existe.** La obligación estaba escrita sin sujeto —«se actualiza en la rama de la
etapa»—, y una obligación sin sujeto no la incumple nadie en particular: **se incumplió tres veces
seguidas**, en las etapas `c`, `d` y `e`, sin que nada chirriara. Lo encontró el orquestador de
reanudación contrastando el documento contra el historial, y quedó registrado como la divergencia
`D-01` de [`../../../Audit/Estado-Del-Destino-2026-08-16.md`](../../../Audit/Estado-Del-Destino-2026-08-16.md) §2.

### 11.2 Instrumento preferido: el subproducto del acto

**Entre un registro que hay que acordarse de actualizar y uno que el acto produce solo, manda el
segundo.** Acá los tres instrumentos posibles se comportaron así, medido sobre el árbol:

| Instrumento | ¿Es subproducto del acto? | Estado observado |
| --- | --- | --- |
| **Nombre de la etapa en el mensaje de confirmación de fusión** | **Sí**: fusionar lo escribe | **Intacto.** Es lo que permitió reconstruir que el código estaba en la etapa `e` cuando el registro decía `b` |
| `changelog.md` | No: hay que acordarse | **Se degradó tres etapas.** Repuesto el 2026-08-16 desde los commits, marcado como repuesto |
| **Etiqueta por etapa cerrada** | No: hay que crearla | **Ninguna se creó a tiempo, y cinco se repusieron el 2026-08-18.** Hasta esa fecha `git tag` devolvía **cero** en todo el repositorio. Hoy están `v0.1.0`, `v0.2.0`, `v0.5.0`, `v0.7.0` y `v0.8.0`, **retroactivas y declaradas como tales**; `c`, `d` y `f` quedan sin etiqueta porque su ancla no sale del historial sin inventarla (`changelog.md`). El objetivo del 100 % **sigue incumplido**, ahora en tres etapas y no en ocho. **Y el 2026-09-12 se midió lo que el evento «cierre de etapa» dejó**: 129 fusiones a `main` desde `v0.8.0` sin etiqueta. El evento pasa a ser cada fusión que cambia código de producción (§3.c); si es subproducto del acto o no depende de `PD-VER-01` |

**El instrumento que manda es el historial del repositorio.** Cuando `changelog.md` y el historial no
coinciden, **gana el historial** y la diferencia se repara sobre el registro en prosa, nunca al revés:
es la regla de resolución de `Master-Prompt-Reanudacion.md` §1, y es la que se aplicó al reponer las
tres etapas.

**El registro en prosa se conserva igual, y no es redundante**: el historial dice *qué se fusionó* y
`changelog.md` dice *qué significó*. Lo que cambia es que deja de ser la fuente que decide y pasa a
ser la que explica.

**Sobre las etiquetas, declarado y no disimulado.** El objetivo del 100 % de §1 y §10 está incumplido en
su totalidad y esta sección **no lo cierra**: cerrarlo es crear las etiquetas de las etapas ya
cerradas o retirar el objetivo, y las dos son decisiones de la categoría 09 con su propio acto. Se
declara acá porque un ítem 8 que enumera un instrumento sin decir que no existe es exactamente la
clase de afirmación que estos registros degradan.

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 5.0 | 2026-09-12 | **`BT-00033` · la herramienta y el evento de etiqueta**, sobre la decisión `D-03` y la respuesta `E-04` del Product Owner en [`../../../Audit/Mesa-2026-09-12-ciclo-2.md`](../../../Audit/Mesa-2026-09-12-ciclo-2.md) §8. **§3.1 a §3.4 dejan de declarar la herramienta «por su función»**: es **MinVer 8.0.0**, anclada exacta en `Directory.Build.props` con `MinVerTagPrefix=v` y nada más; se documenta qué calcula sobre un commit etiquetado y entre etiquetas, que la versión no vive en ningún archivo, y **la condición medida** de que sobre un clon superficial sin etiquetas calcula `0.0.0-alpha.0` sin advertir —que es lo que BuildKit deja desde la URL del repositorio— y cómo lo cubren `ci.yml` y los dos `Dockerfile`. `PA-06` de Application y `PA-04` de Domain (`05` §11) quedan cerrados. **Entra §3.c, el evento de etiqueta como ítem propio**: toda fusión a `main` que cambie código de producción, número por Conventional Commits, MAJOR compartido con el contrato. Con **tres decisiones pendientes** en la forma de `Root-Rules.md` §12.2 y con su `SI NO RESPONDÉS`: `PD-VER-01` quién crea la etiqueta —job de CI costeado contra etiqueta manual; **default: manual**, porque crear etiquetas desde CI es una acción externa e irreversible—, `PD-VER-02` qué cuenta como código de producción, y `PD-VER-03` las dos etiquetas retroactivas de `89f3ab3` y `5c95dab`, **propuestas y no creadas** (`v0.9.0` y `v0.9.1`), con la constancia de que no se pueden calcular porque ninguna confirmación de `main` lleva `feat` ni `fix`. §1 punto 4, §4.1 y §11.2 remiten a §3.c en una oración cada uno, sin reescribir lo transcripto. **La deprecación (§6, §8, §10) no se toca: es `BT-00035`.** Sube **major**: cambia una regla de versionado —el evento— y no sólo su estructura. |
| 4.3 | 2026-09-12 | **§1.1 y §6.1 dejan de afirmar «no hay versionado de rutas porque no hay clientes de terceros» y «no hay a quién darle plazo»** (tarea `BT-00027`). La premisa quedó superada por `PRODUCT-INTAKE` **4.3** §17.1.P.3 y [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) (`/v{MAJOR}/` para la superficie pública; `ADR-00009` dice quiénes son los clientes). La regla 1 de §1.1 y la primera fila de §6.1 quedan **tachadas y marcadas como derogadas**, no borradas; las otras cuatro reglas se conservan. **Este documento no se reescribe entero acá, a propósito**: el evento de etiqueta y la herramienta son `BT-00033`, y la política de deprecación (plazo de un cuatrimestre, cabecera `Deprecation`) es `BT-00035`; las dos escriben sobre este mismo documento. Sube minor. |
| 4.2 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **16 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 4.1 | 2026-08-27 | **Default `E-01` de la mesa de evaluación del 2026-08-27** ([`../../../Audit/Mesa-2026-08-27.md`](../../../Audit/Mesa-2026-08-27.md)), que cierra la divergencia **`D-03'`** abierta desde el 2026-08-18. **§1 punto 4 y §4.1 afirmaban en absoluto que ninguna etapa se cierra sin etiqueta**, y el árbol tiene **cinco etiquetas para ocho etapas**. Los tres huecos —`c`, `d` y `f`— estaban justificados **en el registro de cambios del producto y no acá**, que es donde vive la regla que incumplen. Se escribe la excepción con su motivo —el prefijo de etiqueta no existía hasta el 2026-08-18 y las repuestas no podían anclarlas sin inventar el punto— y **la regla no se retira**: rige desde la fase `i` en adelante. Alternativa descartada: retirar el absoluto, que habría eliminado la obligación en vez de acotarla. |
| 4.0 | 2026-08-24 | **Ronda 2 del corte 09 de la migración 10.0 → 13.3**, que repara lo que el **audit independiente** de la ronda 1 levantó. **El veredicto fue RECHAZADO**, con un **P0**: `Migracion-Rules.md` §6 lista «estado previo no archivado» entre los hallazgos que **detienen la cadena**, y la ronda 1 no archivó. La justificación que había invocado —el precedente de editar en el lugar de la migración anterior— **la refuta el propio `ADR-14001` §4**, que acota su apartamiento a «la migración 6.0 → 8.6 y sólo esa» y declara que el archivado de un documento que **sube de versión sin cambiar de lugar sigue siendo por carpeta**. El estado previo queda en `_legacy/2026-08-24/`. **Corrección propia de este documento**: §5.b decía «las **cuatro** subsecciones de abajo» y §5 tiene **tres** —5.1, 5.2 y 5.3—; la cuarta declaración de sufijos vive en §10.1. `Root-Rules.md` §10 R1, levantado como **P3**. **Y sube MAJOR y no minor, corrigiendo el criterio de la fila anterior.** La ronda 1 bumpeó minor con el argumento de que partir una sección no cambia ninguna decisión; el propio destino había bumpeado **major** cinco días antes por la misma operación, con el argumento de que **cambia la estructura de la sección para corresponder con la de la regla**. Los dos razonamientos se sostienen por separado, pero convivir sin declararlo dejaba la serie midiendo con dos varas. **Se adopta el criterio anterior**, que es el que ya estaba escrito. |
| 3.1 | 2026-08-24 | **Migración normativa 10.0 → 13.3, fase M4** (`Audit/Plan-Migracion-10.0-a-13.3.md` 1.0 §4.2). `Rules-Devops.md` sube a **6.0** y **parte cuatro ítems más** con la misma mecánica que la 5.0 aplicó al prefijo de etiqueta: la mitad bloqueada arrastraba a la que no lo estaba. Acá entra el que alcanza a este documento: **§5.b, la semántica de sufijos de anticipo como ítem propio**, separada del conjunto de canales del punto 5. **No se escribió nada nuevo**: las cuatro subsecciones de §5 ya declaraban que `-alpha`, `-beta` y `-rc` **no se usan**, con su motivo, y §5.b las reúne para que se lea contra el ítem de la regla que lo exige. **No se difiere y por eso no lleva la forma de `Root-Rules.md` §12.2**: está contestado, con su condición de reapertura. Sube **minor**: parte una sección y no cambia ninguna decisión. |
| 3.0 | 2026-08-19 | **Migración normativa 9.12 → 10.0, fase M4.** `Rules-Devops.md` sube a **5.0** y su §4.3 **parte el punto 3 en dos**: la herramienta con su configuración base, y el **prefijo de tag** como **ítem propio 3.b**. La subsección que la emisión 2.2 había creado como **§3.0** pasa a numerarse **§3.b**, leída literalmente contra el ítem de la regla que la exige, y declara esa correspondencia en su primer párrafo. **Ninguna decisión se reabre**: el prefijo sigue siendo **`v`**, fijado el 2026-08-18 con cita de `Rules-Devops.md` §4.5, y la elección de la herramienta sigue abierta como `PA-06`. Las cuatro filas «Prefijo de etiqueta» de §3.1 a §3.4 apuntan al ítem nuevo. **Es reordenamiento y no contenido**: el destino ya cumplía el fondo de la regla nueva un día antes de que existiera, porque el defecto que la originó se midió acá. Sube **major**: cambia la estructura de §3 para corresponder con la de §4.3. | Orquestador de migración normativa SDD |
| 2.2 | 2026-08-18 | **Se fija el prefijo de etiqueta, que era un ítem obligatorio contestado con un diferimiento.** `Rules-Devops.md` §4.3 punto 3 exige declarar «configuración base y **prefijo de tag**», y las cuatro filas de §3 respondían *«el que se fije al anclarla, registrado en el punto de control de la etapa `a`»* — punto de control que **cerró el 2026-08-13 sin registrarlo**, y ocho etapas se construyeron sin poder etiquetarse. Entra **§3.b**, que fija **`v`** con la forma `v<MAJOR>.<MINOR>.<PATCH>` y ámbito de repositorio, **citando el propio `Rules-Devops.md` §4.5** —«Sólo en tag `v<X.Y.Z>` sin sufijo»— en lugar de elegirlo por criterio propio. Declara que **fijar el prefijo no cierra la elección de la herramienta**: `PA-06` sigue abierto, y empaquetar las dos cosas en la misma fila es lo que produjo el bloqueo. **Corrección propia:** la fila de §3.2 remitía el prefijo al `PD-01` de `Pipeline-CI-CD.md` §10, que es *«la herramienta concreta de cada stage»* y **no lo cubre**: el prefijo estaba diferido hacia un punto abierto sin dueño real. Se declara y se eleva al framework como el reporte `14`. **§11.2** deja de decir que nunca se creó ninguna etiqueta: cinco se repusieron el 2026-08-18 y el objetivo del 100 % sigue incumplido en tres. Sube **minor**: entra una sección y se corrigen cinco filas; ninguna regla de versionado cambia. | Orquestador SDD |
| 2.1 | 2026-08-17 | Entra **§11, el registro del avance y su responsable**, con los ítems **7 y 8** que `Rules-Devops.md` **4.2** §4.3 agregó: qué documento declara la etapa, **quién lo actualiza y en qué evento**, y el **instrumento preferido**, que es el subproducto del acto. Se declara que **manda el historial del repositorio** sobre `changelog.md`, y que el objetivo del 100 % de etiquetas por etapa cerrada está **incumplido en su totalidad** —cero etiquetas en el árbol—, sin cerrarlo acá. Reparación de la divergencia `D-06` de `Audit/Estado-Del-Destino-2026-08-17.md` §2. Sube **minor**. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
