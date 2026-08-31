# Estrategia de versionado — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Estrategia-Versionado.md
**Versión:** 3.2
**Estado:** Propuesto
**Fecha:** 2026-08-24
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Política de crecimiento del punto de extensión»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Versionado semántico

### 1.1 `GeometriaFactory-Web`

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.2.P.7 · GeometriaFactory-Web lo declara **sin excepciones**, junto con las convenciones de mensaje de confirmación, y con rama, pull request y etiqueta por etapa.

**Este proyecto de código es hoja del grafo y no expone contrato a nadie** (intake §14, fila de `GeometriaFactory-Web`). La consecuencia sobre el versionado es directa y conviene decirla antes que nada: **no hay integrador cuya compilación pueda romperse por un cambio suyo**. Un cambio mayor acá no rompe a otro proyecto de código: **rompe a la persona que usa el producto**, y eso lo detecta el guion de demostración, no un compilador.

De ahí que la clase de cambio se decida sobre **lo que la persona ve y puede hacer**, y no sobre una superficie de tipos:

| Clase | Qué la produce en esta unidad | Cómo se detecta |
| --- | --- | --- |
| **Mayor** | Se quita una superficie, una ruta o una acción que la persona tenía | El guion de demostración acumulativo (`QG-10004`): un paso de una etapa anterior deja de pasar |
| **Mayor** | Cambia el desenlace de una acción sin que la persona lo pida: lo que antes guardaba ahora rechaza, o al revés | El mismo, y las filas de la matriz de sensado de la superficie afectada |
| **Mayor** | Se rompe una de las tres reglas de arquitectura: aparece una petición del navegador al servicio de datos, el bundle adquiere red o configuración, o un mensaje expone una dirección de servicio | `QG-10005`, `QG-10009` y `QG-10008`, **con umbral 0 cada uno**. Ninguna compilación lo detecta |
| **Menor** | Se agrega una superficie, una ruta o una acción sin quitar ninguna | El guion de la etapa nueva |
| **Menor** | Se agrega un estado a una superficie existente sin cambiar los que había | Las filas nuevas de la matriz de sensado |
| **Parche** | Se corrige lo construido para que coincida con la línea de base visual aprobada | La fila de la matriz de sensado que registraba la deriva |

**La tercera fila es la más importante de la tabla, y es la que distingue a este proyecto de código de todos los demás del producto.** `RA-01`, `RA-02` y `RA-03` son reglas de nivel producto (intake §14) y **este es el único proyecto de código desde el que se pueden violar las tres**, porque es el único que sirve el navegador. Un cambio que las rompe **compila, se publica y se ve bien**: sólo lo detectan los recuentos de `QG-10005`, `QG-10008` y `QG-10009`.

### 1.2 `GeometriaFactory-Visor`

Se adopta el **versionado semántico 2.0.0** en el archivo de manifiesto del paquete, junto con las convenciones de mensaje de confirmación, igual que el resto del producto (intake §17.2.P.7 · GeometriaFactory-Visor).

**Qué gobierna la versión acá, y por qué es distinto de los otros dos proyectos de código de nivel topológico 0.** [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §2 lo decide: gobierna **la superficie pública del punto de extensión** —las **seis** funciones, las **siete** garantías y los **siete** códigos de condición—, que es el punto de extensión declarado del producto (intake §18) y el único proyecto de código con `tiene_extensibilidad` en true.

Y hay una asimetría que ordena todo lo demás, declarada en `ADR-12006` §1: **el anfitrión no compila contra este artefacto**. Lo carga en el navegador e invoca sus funciones por interoperabilidad, de modo que **un cambio incompatible no rompe ninguna compilación: se manifiesta en tiempo de ejecución**.

Criterio de clase de cambio, transcripto de `ADR-12006` §7 **sin agregarle ni quitarle nada**:

| Clase | Qué la produce | ¿Lo detecta una compilación? |
| --- | --- | --- |
| **Mayor** | Quitar una función, renombrarla o cambiar qué recibe: rompe al anfitrión y al sample S-1 | No |
| **Mayor** | **Perder cualquiera de las siete garantías**, aunque las seis firmas no se toquen | No |
| **Mayor** | Cambiar la semántica de una entrada ya declarada del resultado de dibujo | No |
| **Menor** | Agregar una función. Así entró la sexta, sin romper a ningún anfitrión escrito contra las cinco anteriores | — |
| **Menor** | Agregar una entrada nueva al resultado de dibujo, conservando la semántica de las declaradas | — |
| **Menor** | Agregar un código de condición, que sólo puede nacer en la categoría 02 | — |
| **Sin efecto de contrato** | Cambiar la forma interna del identificador de instancia, mientras siga siendo opaco y cumpla sus tres propiedades semánticas. Que el anfitrión dependa de su forma es un defecto del anfitrión | — |
| **Parche** | Corregir el interior de la capa 3 sin cambiar la superficie ni las garantías | — |

**Ninguna de las tres clases mayores la detecta una compilación**, y es la diferencia operativa más importante frente a `GeometriaFactory-Domain` y `GeometriaFactory-Contracts`, donde al menos una clase mayor se manifiesta al construir. La mitigación que `ADR-12006` §2 declara es **la revisión más el sample S-1**, que ejerce el contrato entero sin ninguna pieza del backend, y esta categoría la hace operativa en [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) §3.

## 2. Convenciones de mensaje de confirmación

### 2.1 `GeometriaFactory-Web`

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** Como ninguna de las seis clases de §1 la detecta un compilador, **el marcador de cambio incompatible no puede depender de que algo falle al construir**: se escribe porque el criterio de §1 dice que corresponde. Un cambio que quita una acción de una superficie y llega etiquetado `feat` es un cambio mayor mal marcado, y lo levanta la revisión del pull request más el guion acumulativo.

### 2.2 `GeometriaFactory-Visor`

**Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Regla propia, y es la consecuencia directa de §1**: como **ninguna** clase mayor la detecta una compilación, el marcador de cambio incompatible se escribe **porque el criterio de `ADR-12006` §7 dice que corresponde**, y nunca porque algo se haya roto al construir. En particular, **perder una garantía es cambio mayor aunque las seis firmas queden intactas y el bundle compile y dibuje**: el archivo de confirmación es el único lugar donde eso se declara antes de que un anfitrión lo descubra en ejecución.

## 3. Herramienta de cálculo de la versión

### 3.b El prefijo de etiqueta — **ítem propio**, y por qué llega tarde a este documento

**Esta subsección realiza el ítem 3.b de `Rules-Devops.md` §4.3**, que la regla **5.0** separó del punto
3 y que este documento **nunca emitió**. La unidad `GeometriaFactory-Api` lo emitió el **2026-08-18**,
en su `Estrategia-Versionado.md` §3.b; **acá quedó sin hacer, y su fila siguió difiriendo el prefijo a
un evento que ya había ocurrido**.

**Lo levantó el audit de la ronda 1 del corte 09 de la migración 10.0 → 13.3**, como hallazgo **P1**: se
estaba emitiendo §5.b cuatro secciones más abajo mientras §3.1 seguía diciendo *«el que se fije al
anclarla, registrado en el punto de control de la etapa `a`»* — **un punto de control que cerró el
2026-08-13 sin registrarlo**. Es el mismo defecto que dejó a este producto ocho etapas sin poder
etiquetarse, y estaba vivo en el documento que se estaba reescribiendo.

| Aspecto | Decisión |
| --- | --- |
| **Prefijo de etiqueta** | **`v`** — el del repositorio entero, no uno propio de esta unidad |
| **Forma completa** | `v<MAJOR>.<MINOR>.<PATCH>`, sin sufijo, sobre el SemVer 2.0.0 que §1 adopta |
| **Ámbito** | El repositorio de código. **Las etiquetas son del repositorio y no de cada ensamblado**, de modo que esta unidad no acuña ni un prefijo propio ni un espacio de nombres propio |

**No se elige acá y por eso se puede escribir sin decidir nada.** El prefijo ya estaba fijado el
2026-08-18 en `Estrategia-Versionado.md` §3.b de `GeometriaFactory-Api`, con su fundamento —la tabla de
canales de `Rules-Devops.md` §4.5 escribe la forma literal «Sólo en tag `v<X.Y.Z>` sin sufijo»— y **las
cinco etiquetas del repositorio ya lo usan**: `v0.1.0`, `v0.2.0`, `v0.5.0`, `v0.7.0` y `v0.8.0`. Lo que
faltaba acá no era la decisión: era **decir que esta unidad se rige por ella**.

**Fijar el prefijo no cierra la elección de la herramienta.** `PA-06` sigue abierto, y empaquetar las
dos cosas en la misma fila es lo que produjo el defecto.

### 3.1 `GeometriaFactory-Web`

**Se declara por su función, y esta categoría no la elige**, por el mismo motivo que en el resto del producto: ninguna fuente la nombra y la regla de anclaje del intake, en el encabezado de su Parte C, la ata al momento en que se introduce.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | **`v`**, el del repositorio entero — ver **§3.b**, que es el ítem propio que `Rules-Devops.md` §4.3 punto 3.b exige |
| Qué **no** calcula la herramienta | **Ninguna de las seis clases de §1.** No hay superficie de tipos que comparar: lo que cambia es lo que la persona ve, y eso lo decide el criterio y lo verifica el guion |

~~**Y una versión que sí se ancla y no se calcula**: la de la **biblioteca de componentes de interfaz**~~ **SIN OBJETO desde el 2026-08-31, y en rigor desde el 2026-08-20.** La fuente la dejaba **[A VERIFICAR]** y declaraba que se registraba al crear el andamiaje (intake §17.2.P.1 · GeometriaFactory-Web), pero **no hay biblioteca de componentes**: `GeometriaFactory.Web.csproj` declara una sola referencia de proyecto y lleva escrito el apartamiento —*«la etapa `b` decide NO INTRODUCIR MudBlazor»*—. `PA-01` de `05` §11 lo cerró **por lectura el 2026-08-20** y `BT-10002` queda sin objeto. **Esta categoría no la inventaba, y ahora tampoco tiene qué anclar**: la única versión que este documento gobierna sigue siendo la del producto.

### 3.2 `GeometriaFactory-Visor`

**Se declara por su función**, como en el resto del producto: el intake §17.1.P.7 · GeometriaFactory-Domain —al que §17.2.P.7 · GeometriaFactory-Visor se alinea— ata la elección al anclaje de la etapa `a`, y `ADR-12006` §6 acepta explícitamente que **la versión no la verifique ninguna herramienta** y que sea una convención sostenida por disciplina.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta, y reflejarla en el manifiesto del paquete |
| Qué **no** puede calcular ninguna herramienta | La pérdida de una garantía y el cambio de semántica de una entrada del resultado de dibujo. Los dos son cambios mayores que no dejan rastro en ninguna firma |
| Qué lo sustituye | La revisión del pull request de la etapa —que **es** el punto de control— y la batería de la categoría 08 sobre las **siete** garantías, con objetivo **7 de 7** verificadas antes de fusionar |

Las tres filas se apoyan en `ADR-12006` §6 y §8.

## 4. Modelo de ramas

### 4.1 `GeometriaFactory-Web`

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**, sin abrir la rama de una etapa antes de fusionar la anterior; y sin OK explícito no se avanza (intake §10, §15 y §17.2.P.7 · GeometriaFactory-Web).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 que corren en el pull request: `QG-10001`, y las inspecciones `QG-10005` a `QG-10010`.
- **La rama principal es la que publica.** Es el único proyecto de código del producto donde fusionar puede desencadenar un despliegue por sí solo, y por eso el filtro de rutas del flujo importa: ver [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.
- El cierre de la etapa exige además `QG-10004` sobre el guion acumulativo y `QG-10011` sobre las filas de la matriz de sensado que la etapa tocó.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son ocho** —`a` a `h`, **todas las comprometidas**—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §6. Es el único proyecto de código del producto que las toca todas, y la consecuencia para esta categoría es que **su guion acumulativo crece en cada una**: en la etapa `h` `QG-10004` verifica los pasos de las ocho.

### 4.2 `GeometriaFactory-Visor`

El del producto, sin variantes: una rama por etapa a partir de la principal, etiqueta al fusionar, un pull request por etapa que **es** el punto de control, etapas en serie y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Domain; `ADR-12006` §7, primera viñeta).

**Los momentos de este proyecto de código no son sólo etapas**, y el modelo de ramas tiene que convivir con eso. [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §1 declara **tres** momentos: la etapa `a`, el **momento de medición de `PT-02` y `PT-03`** —que no es una etapa y no crea una nueva— y la etapa `g`. La consecuencia para esta categoría es que **la medición de las dos puertas no espera a la rama de la etapa `g`**: si esperara, mediría después de comprometerla, que es justo lo que el intake §15 prohíbe al declarar que una puerta que no pasa **detiene la planificación** de lo que depende de ella.

**Reglas de protección de la rama principal:** los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 en verde, incluidas las tres inspecciones **sobre el bundle generado**, y la constancia del OK del punto de control.

## 5. Canales

### 5.b La semántica de sufijos de anticipo — **ítem propio**

**Esta subsección realiza el ítem 5.b de `Rules-Devops.md` §4.3**, que desde la regla **6.0** pide la
semántica de sufijos **separada del conjunto de canales**: qué canales tiene el producto puede
depender de una decisión de distribución abierta, **qué sufijo lleva una preview no depende de nada**.

**Las dos mitades ya estaban contestadas en §5.1 y §5.2**, y esta subsección las reúne en el ítem
propio que la regla exige, sin escribir nada nuevo.

| Aspecto | Decisión |
| --- | --- |
| **Sufijos `-alpha`, `-beta`, `-rc`** | **No se usan**, ni en `GeometriaFactory-Web` ni en `GeometriaFactory-Visor` |
| **Forma que sí se usa** | `v<MAJOR>.<MINOR>.<PATCH>` sin sufijo, la del repositorio entero |
| **Motivo** | No hay canal donde publicar un anticipo ni integrador que lo consuma: el front se publica por FTP en un hosting y **el anfitrión carga el archivo que la construcción produjo** (intake §15 y §16) |
| **Qué lo reabriría** | Que el punto de extensión pase a publicarse como paquete consumible por terceros. **§8 no declara esa condición** —sus cinco obligaciones son sobre cómo crece la superficie, no sobre publicación—, así que se declara acá y no se le atribuye a otra sección |

**No se difiere y por eso no lleva la forma de `Root-Rules.md` §12.2**: está contestado, con su motivo
y su condición de reapertura.

### 5.1 `GeometriaFactory-Web`

**No hay canales de publicación de paquete**, y hay **un** canal de despliegue.

`Rules-Devops.md` §4.3 pide declarar canales `preview` y `stable`; esa figura pertenece a artefactos que se publican en un feed y se consumen por versión. Acá el artefacto **no se publica**: `redistribuible` es false (intake §13; `05` §5, fila de publicación como paquete) y lo que existe es **una subida a un destino único**, el hosting público.

| Figura del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Canal `preview` | **No existe** | No hay feed ni integrador que consuma un anticipo. Lo que un anticipo compraría —probar antes de que lo vea un usuario— no existe en este producto: ver el apartamiento de ambientes en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 |
| Canal `stable` | **Se corresponde con el único destino**: el hosting público | Intake §17.2.P.7 · GeometriaFactory-Web |
| Sufijos de anticipo `-alpha`, `-beta`, `-rc` | **No se usan** | No hay canal donde publicar un anticipo. Las etiquetas del producto son **de etapa cerrada**, no de anticipo (intake §15) |

### 5.2 `GeometriaFactory-Visor`

**No hay canales de publicación.** El intake §17.2.P.7 · GeometriaFactory-Visor declara que **no se publica** en ningún repositorio de paquetes del ecosistema del navegador, y [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §4 descartó la alternativa con su fundamento. El apartamiento frente a `Rules-Devops.md` §2.2 queda registrado en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1.

**Sin sufijos de anticipo.** No hay canal donde publicar un anticipo del punto de extensión ni integrador que lo consuma; el anfitrión carga el archivo que la construcción produjo.

**Y una consecuencia de la resolución de `PA-05`**: como el bundle **no se versiona en el repositorio** ([`Entornos-Deploy.md`](Entornos-Deploy.md) §2), no existe la figura de «la versión del bundle que está en el repositorio». La versión que importa es la del **estado del fuente**, y el artefacto se regenera desde ahí. Es lo que hace verificable la métrica de reproducibilidad de `ADR-12006` §8: dos construcciones desde el mismo estado producen el mismo artefacto.

## 6. Qué versiona esta unidad, que no es lo que parece

### 6.1 `GeometriaFactory-Web`

Hay una asimetría propia de este proyecto de código que conviene declarar, porque afecta a lo que una etiqueta garantiza:

| Qué viaja adentro de la publicación | Cómo se versiona | Consecuencia |
| --- | --- | --- |
| La aplicación del front | Con la versión de esta unidad, calculada desde las etiquetas | Volver a la etiqueta anterior reconstruye exactamente esta parte |
| Los tipos de `GeometriaFactory-Contracts` **compilados adentro** | Con el estado del repositorio en esa etiqueta, **no con una versión de paquete** | No hay versión intermedia que resolver: `ADR-10003` de aquel proyecto de código lo decide como **compilación compartida** |
| El **bundle del visor** como recurso estático generado | **Con el fuente que lo generó, y no con un archivo guardado**: no está versionado, se regenera en cada flujo | Volver a la etiqueta anterior **regenera** el bundle; no lo restaura. Es una propiedad y no un costo: un archivo restaurado podría no corresponder al fuente |

**La tercera fila es la que suele leerse mal.** Que el bundle no esté en el repositorio no debilita a la etiqueta: la etiqueta apunta al **fuente del visor** en ese estado, y el flujo lo reconstruye. Lo que se pierde es la posibilidad de servir un bundle sin construirlo, que es precisamente lo que `QG-10002` prohíbe.

**Y una asimetría que esta unidad no puede resolver sola**: `ADR-10006` §6 de `GeometriaFactory-Visor` acepta que un cambio mayor del punto de extensión del bundle **no lo detecta ninguna compilación** desde este lado. La mitigación desde acá es `QG-12009` —**0** invocaciones al interior del bundle, con las **6** funciones de la fachada como única vía— más la revisión.

## 7. Política de cambios incompatibles

### 7.1 `GeometriaFactory-Web`

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno.** Lo que rige en su lugar:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Ante un cambio **mayor** de los de §1, el guion de demostración de la etapa **y los de todas las anteriores** pasan al **100 %** antes del punto de control | `QG-10004`, con `TC-10035`. **Bloqueante** | Intake §17.2.P.6 · GeometriaFactory-Web y §15, regla de no-regresión acumulativa |
| Ante un cambio **incompatible del contrato**, **las dos unidades desplegables se despliegan juntas** | El `QG-08008` de `GeometriaFactory-Contracts`, que bloquea la publicación de la etapa. Tratamiento operativo en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2 | Intake §17.2.P.3 · GeometriaFactory-Contracts |
| **0** advertencias de construcción | `QG-10001`, en el paso 5 del flujo | Intake §17.2.P.8 · GeometriaFactory-Web; [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, sexta métrica |
| **0** apariciones de la dirección del servidor propio en el repositorio | Inspección del árbol de fuentes y del historial | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, primera métrica |
| **0** etapas cerradas sin etiqueta | Inspección del historial contra el índice de informes de cierre | Intake §17.2.P.7 · GeometriaFactory-Web |
| Toda **deriva mayor** contra la línea de base visual se resuelve corrigiendo lo construido o actualizando la línea de base con aprobación humana, **nunca por omisión** | `QG-10011`, al cerrar la etapa | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request de la etapa, que **es** el punto de control | Intake §15, regla de delivery 3 |

**Las seis métricas de [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8 se adoptan sin agregar ninguna**, y las cuatro que no figuran arriba como obligación de versionado figuran como gates en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2: la respuesta de la dirección pública, la salida hacia el servicio de datos, el bundle generado en el mismo flujo y las publicaciones que terminan sin comprobar.

## 8. Política de crecimiento del punto de extensión

### 8.1 `GeometriaFactory-Visor`

Reemplaza a la política de obsolescencia de `Rules-Devops.md` §4.3, y el reemplazo tiene fundamento: no hay integrador externo a quien dar plazo de migración —el único anfitrión es `GeometriaFactory-Web`, del mismo producto—. Lo que sí hay, y es más exigente que un plazo, es un **procedimiento para que la superficie crezca**.

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Una función nueva en la fachada recorre **los seis pasos** de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 **enteros**, incluida la consolidación en el intake | Criterio de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3, y Definition of Done §1.3 | `08` y `05` |
| Un código de condición **sólo puede nacer en la categoría 02**; ninguno se acuña aguas abajo | `QG-12008`, con `TC-12021`, comparando en las dos direcciones | `08` `Estrategia-Calidad.md` §3 |
| **Perder una garantía es cambio mayor**, y las **siete** se verifican antes de fusionar | Objetivo **7 de 7** | `ADR-12006` §7 y §8 |
| El bundle **nunca se edita a mano**; objetivo: exactamente **0** ediciones manuales | `QG-12009` y `CV-12030` | `ADR-12006` §8; `08` |
| Todo cambio mayor recibe su fila en el registro de cambios del producto; objetivo: **0** cambios mayores sin registro | Revisión del pull request de la etapa | `ADR-12006` §8 |

**El antecedente que muestra que el procedimiento funciona ya ocurrió**: la sexta función de la fachada entró como cambio menor **sin romper a ningún anfitrión escrito contra las cinco anteriores** (`ADR-12006` §6, punto 3, y §7). El intake la consolidó en §17.2.P.3 · GeometriaFactory-Visor en su versión 1.6. Es el recorrido completo de los seis pasos, hecho una vez y registrado.

## 9. Registro del avance y su responsable

**Esta sección responde a los ítems 7 y 8 de `Rules-Devops.md` §4.3**, y se escribe con el caso
observado de este mismo producto a la vista.

### 9.1 Qué documento declara el avance, quién lo actualiza y en qué evento

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

### 9.2 Instrumento preferido: el subproducto del acto

**Entre un registro que hay que acordarse de actualizar y uno que el acto produce solo, manda el
segundo.** Acá los tres instrumentos posibles se comportaron así, medido sobre el árbol:

| Instrumento | ¿Es subproducto del acto? | Estado observado |
| --- | --- | --- |
| **Nombre de la etapa en el mensaje de confirmación de fusión** | **Sí**: fusionar lo escribe | **Intacto.** Es lo que permitió reconstruir que el código estaba en la etapa `e` cuando el registro decía `b` |
| `changelog.md` | No: hay que acordarse | **Se degradó tres etapas.** Repuesto el 2026-08-16 desde los commits, marcado como repuesto |
| **Etiqueta por etapa cerrada** | No: hay que crearla | **Nunca se creó ninguna.** `git tag` devuelve **cero** en todo el repositorio, contra el objetivo del 100 % que §4 declara |

**El instrumento que manda es el historial del repositorio.** Cuando `changelog.md` y el historial no
coinciden, **gana el historial** y la diferencia se repara sobre el registro en prosa, nunca al revés:
es la regla de resolución de `Master-Prompt-Reanudacion.md` §1, y es la que se aplicó al reponer las
tres etapas.

**El registro en prosa se conserva igual, y no es redundante**: el historial dice *qué se fusionó* y
`changelog.md` dice *qué significó*. Lo que cambia es que deja de ser la fuente que decide y pasa a
ser la que explica.

**Sobre las etiquetas, declarado y no disimulado.** El objetivo del 100 % de §4 está incumplido en
su totalidad y esta sección **no lo cierra**: cerrarlo es crear las etiquetas de las etapas ya
cerradas o retirar el objetivo, y las dos son decisiones de la categoría 09 con su propio acto. Se
declara acá porque un ítem 8 que enumera un instrumento sin decir que no existe es exactamente la
clase de afirmación que estos registros degradan.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 3.2 | 2026-08-31 | **Cierre de las dos incógnitas `[A VERIFICAR]` que ya no tenían pregunta**, sobre el inventario [`Inventario-Marcas-A-Verificar-2026-08-31.md`](../../../Audit/Inventario-Marcas-A-Verificar-2026-08-31.md), que clasificó las **71** apariciones vivas del corpus en **cinco** incógnitas. **(a) La versión de plataforma del hosting quedó RESUELTA el 2026-08-13, midiendo**: `PT-01.a` pasa con **200** y el hosting soporta `net10.0`, confirmado desde el panel; no hizo falta bajar la versión objetivo del front. **(b) La versión de la biblioteca de componentes queda SIN OBJETO**: la biblioteca nunca se introdujo y su ausencia es una decisión declarada en el `.csproj` — `PA-01` de `Web/05` §11 **ya lo había cerrado por lectura el 2026-08-20** y el desenlace no bajó. **Ninguna de las dos se decide acá: las dos se leen.**  **Ningún umbral, ningún contrato y ninguna decisión cambian.** |
| 3.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **14 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 3.0 | 2026-08-24 | **Ronda 2 del corte 09 de la migración 10.0 → 13.3**, que repara lo que el **audit independiente** de la ronda 1 levantó. **El veredicto fue RECHAZADO**, con un **P0**: `Migracion-Rules.md` §6 lista «estado previo no archivado» entre los hallazgos que **detienen la cadena**, y la ronda 1 no archivó. La justificación que había invocado —el precedente de editar en el lugar de la migración anterior— **la refuta el propio `ADR-14001` §4**, que acota su apartamiento a «la migración 6.0 → 8.6 y sólo esa» y declara que el archivado de un documento que **sube de versión sin cambiar de lugar sigue siendo por carpeta**. El estado previo queda en `_legacy/2026-08-24/`. **Y entra §3.b, el prefijo de etiqueta, que este documento nunca emitió**: era un **P1** del audit. `GeometriaFactory-Api` lo emitió el 2026-08-18 y acá la fila de §3.1 siguió difiriendo el prefijo *«al punto de control de la etapa `a`»* —**que cerró el 2026-08-13 sin registrarlo**—, mientras la ronda 1 reescribía §5.b cuatro secciones más abajo. **No se decide nada nuevo**: el prefijo `v` ya estaba fijado y las cinco etiquetas del repositorio lo usan; lo que faltaba era declarar que esta unidad se rige por él. **Se corrige además una atribución falsa**: §5.b decía que la condición de reapertura «es la que §8 ya declara», y §8 no la declara —sus cinco obligaciones son sobre cómo crece la superficie, no sobre publicación—; **P2**. **Y sube MAJOR y no minor, corrigiendo el criterio de la fila anterior.** La ronda 1 bumpeó minor con el argumento de que partir una sección no cambia ninguna decisión; el propio destino había bumpeado **major** cinco días antes por la misma operación, con el argumento de que **cambia la estructura de la sección para corresponder con la de la regla**. Los dos razonamientos se sostienen por separado, pero convivir sin declararlo dejaba la serie midiendo con dos varas. **Se adopta el criterio anterior**, que es el que ya estaba escrito. |
| 2.2 | 2026-08-24 | **Migración normativa 10.0 → 13.3, fase M4** (`Audit/Plan-Migracion-10.0-a-13.3.md` 1.0 §4.2). Entra **§5.b, la semántica de sufijos de anticipo como ítem propio**, que `Rules-Devops.md` **6.0** §4.3 separa del conjunto de canales. **No se escribió nada nuevo**: §5.1 y §5.2 ya declaraban que no se usan, con su motivo —no hay canal donde publicar un anticipo y el anfitrión carga el archivo que la construcción produjo—. **No se difiere**: está contestado, con su condición de reapertura, que es la misma que §8 declara para el crecimiento del punto de extensión. Sube **minor**. |
| 2.1 | 2026-08-17 | Entra **§9, el registro del avance y su responsable**, con los ítems **7 y 8** que `Rules-Devops.md` **4.2** §4.3 agregó: qué documento declara la etapa, **quién lo actualiza y en qué evento**, y el **instrumento preferido**, que es el subproducto del acto. Se declara que **manda el historial del repositorio** sobre `changelog.md`, y que el objetivo del 100 % de etiquetas por etapa cerrada está **incumplido en su totalidad** —cero etiquetas en el árbol—, sin cerrarlo acá. Reparación de la divergencia `D-06` de `Audit/Estado-Del-Destino-2026-08-17.md` §2. Sube **minor**. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
