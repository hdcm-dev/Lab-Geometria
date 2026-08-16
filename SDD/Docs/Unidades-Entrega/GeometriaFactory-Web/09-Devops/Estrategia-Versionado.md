# Estrategia de versionado — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Estrategia-Versionado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md`](../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md) 1.0; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3 y §1.4; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §15, §17.2.P.3 · GeometriaFactory-Contracts, §17.2.P.7 · GeometriaFactory-Web y §17.2.P.8 · GeometriaFactory-Web
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md)

---

## Tabla de contenido

- [1. Versionado semántico](#1-versionado-semántico)
- [2. Convenciones de mensaje de confirmación](#2-convenciones-de-mensaje-de-confirmación)
- [3. Herramienta de cálculo de la versión](#3-herramienta-de-cálculo-de-la-versión)
- [4. Modelo de ramas](#4-modelo-de-ramas)
- [5. Canales](#5-canales)
- [6. Qué versiona esta unidad, que no es lo que parece](#6-qué-versiona-esta-unidad-que-no-es-lo-que-parece)
- [7. Política de cambios incompatibles](#7-política-de-cambios-incompatibles)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Versionado semántico

Se adopta el **versionado semántico 2.0.0**, con el formato `MAJOR.MINOR.PATCH[-PRERELEASE][+BUILDMETADATA]`. El intake §17.2.P.7 · GeometriaFactory-Web lo declara **sin excepciones**, junto con las convenciones de mensaje de confirmación, y con rama, pull request y etiqueta por etapa.

**Este proyecto de código es hoja del grafo y no expone contrato a nadie** (intake §14, fila de `GeometriaFactory-Web`). La consecuencia sobre el versionado es directa y conviene decirla antes que nada: **no hay integrador cuya compilación pueda romperse por un cambio suyo**. Un cambio mayor acá no rompe a otro proyecto de código: **rompe a la persona que usa el producto**, y eso lo detecta el guion de demostración, no un compilador.

De ahí que la clase de cambio se decida sobre **lo que la persona ve y puede hacer**, y no sobre una superficie de tipos:

| Clase | Qué la produce en esta unidad | Cómo se detecta |
| --- | --- | --- |
| **Mayor** | Se quita una superficie, una ruta o una acción que la persona tenía | El guion de demostración acumulativo (`QG-04`): un paso de una etapa anterior deja de pasar |
| **Mayor** | Cambia el desenlace de una acción sin que la persona lo pida: lo que antes guardaba ahora rechaza, o al revés | El mismo, y las filas de la matriz de sensado de la superficie afectada |
| **Mayor** | Se rompe una de las tres reglas de arquitectura: aparece una petición del navegador al servicio de datos, el bundle adquiere red o configuración, o un mensaje expone una dirección de servicio | `QG-05`, `QG-09` y `QG-08`, **con umbral 0 cada uno**. Ninguna compilación lo detecta |
| **Menor** | Se agrega una superficie, una ruta o una acción sin quitar ninguna | El guion de la etapa nueva |
| **Menor** | Se agrega un estado a una superficie existente sin cambiar los que había | Las filas nuevas de la matriz de sensado |
| **Parche** | Se corrige lo construido para que coincida con la línea de base visual aprobada | La fila de la matriz de sensado que registraba la deriva |

**La tercera fila es la más importante de la tabla, y es la que distingue a este proyecto de código de todos los demás del producto.** `RA-01`, `RA-02` y `RA-03` son reglas de nivel producto (intake §14) y **este es el único proyecto de código desde el que se pueden violar las tres**, porque es el único que sirve el navegador. Un cambio que las rompe **compila, se publica y se ve bien**: sólo lo detectan los recuentos de `QG-05`, `QG-08` y `QG-09`.

## 2. Convenciones de mensaje de confirmación

Se adoptan las **Conventional Commits 1.0.0**, con el mismo efecto sobre la versión que en el resto del producto:

| Prefijo del mensaje | Efecto sobre la versión |
| --- | --- |
| `feat` | Sube **MINOR** |
| `fix` | Sube **PATCH** |
| `feat!`, o `BREAKING CHANGE` en el pie del mensaje | Sube **MAJOR** |
| `refactor`, `perf`, `test`, `chore`, `docs`, `style`, `build`, `ci` | No sube nada |

**Precisión propia de este proyecto de código.** Como ninguna de las seis clases de §1 la detecta un compilador, **el marcador de cambio incompatible no puede depender de que algo falle al construir**: se escribe porque el criterio de §1 dice que corresponde. Un cambio que quita una acción de una superficie y llega etiquetado `feat` es un cambio mayor mal marcado, y lo levanta la revisión del pull request más el guion acumulativo.

## 3. Herramienta de cálculo de la versión

**Se declara por su función, y esta categoría no la elige**, por el mismo motivo que en el resto del producto: ninguna fuente la nombra y la regla de anclaje del intake, en el encabezado de su Parte C, la ata al momento en que se introduce.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | **Ninguna de las seis clases de §1.** No hay superficie de tipos que comparar: lo que cambia es lo que la persona ve, y eso lo decide el criterio y lo verifica el guion |

**Y una versión que sí se ancla y no se calcula**: la de la **biblioteca de componentes de interfaz**, que la fuente deja explícitamente **[A VERIFICAR]** y declara que se registra al crear el andamiaje (intake §17.2.P.1 · GeometriaFactory-Web). Es `PA-01` de `05` §11 y `BT-10002` de la etapa `a`. **Esta categoría no la inventa.**

## 4. Modelo de ramas

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**, sin abrir la rama de una etapa antes de fusionar la anterior; y sin OK explícito no se avanza (intake §10, §15 y §17.2.P.7 · GeometriaFactory-Web).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 que corren en el pull request: `QG-01`, y las inspecciones `QG-05` a `QG-10`.
- **La rama principal es la que publica.** Es el único proyecto de código del producto donde fusionar puede desencadenar un despliegue por sí solo, y por eso el filtro de rutas del flujo importa: ver [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.
- El cierre de la etapa exige además `QG-04` sobre el guion acumulativo y `QG-11` sobre las filas de la matriz de sensado que la etapa tocó.
- No se exige revisor humano independiente: `equipo_n` es 1 y el filtro es el punto de control bloqueante.

**Las etapas que este proyecto de código toca son ocho** —`a` a `h`, **todas las comprometidas**—, según [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2, citado por [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §6. Es el único proyecto de código del producto que las toca todas, y la consecuencia para esta categoría es que **su guion acumulativo crece en cada una**: en la etapa `h` `QG-04` verifica los pasos de las ocho.

## 5. Canales

**No hay canales de publicación de paquete**, y hay **un** canal de despliegue.

`Rules-Devops.md` §4.3 pide declarar canales `preview` y `stable`; esa figura pertenece a artefactos que se publican en un feed y se consumen por versión. Acá el artefacto **no se publica**: `redistribuible` es false (intake §13; `05` §5, fila de publicación como paquete) y lo que existe es **una subida a un destino único**, el hosting público.

| Figura del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Canal `preview` | **No existe** | No hay feed ni integrador que consuma un anticipo. Lo que un anticipo compraría —probar antes de que lo vea un usuario— no existe en este producto: ver el apartamiento de ambientes en [`Entornos-Deploy.md`](Entornos-Deploy.md) §1.1 |
| Canal `stable` | **Se corresponde con el único destino**: el hosting público | Intake §17.2.P.7 · GeometriaFactory-Web |
| Sufijos de anticipo `-alpha`, `-beta`, `-rc` | **No se usan** | No hay canal donde publicar un anticipo. Las etiquetas del producto son **de etapa cerrada**, no de anticipo (intake §15) |

## 6. Qué versiona esta unidad, que no es lo que parece

Hay una asimetría propia de este proyecto de código que conviene declarar, porque afecta a lo que una etiqueta garantiza:

| Qué viaja adentro de la publicación | Cómo se versiona | Consecuencia |
| --- | --- | --- |
| La aplicación del front | Con la versión de esta unidad, calculada desde las etiquetas | Volver a la etiqueta anterior reconstruye exactamente esta parte |
| Los tipos de `GeometriaFactory-Contracts` **compilados adentro** | Con el estado del repositorio en esa etiqueta, **no con una versión de paquete** | No hay versión intermedia que resolver: `ADR-10003` de aquel proyecto de código lo decide como **compilación compartida** |
| El **bundle del visor** como recurso estático generado | **Con el fuente que lo generó, y no con un archivo guardado**: no está versionado, se regenera en cada flujo | Volver a la etiqueta anterior **regenera** el bundle; no lo restaura. Es una propiedad y no un costo: un archivo restaurado podría no corresponder al fuente |

**La tercera fila es la que suele leerse mal.** Que el bundle no esté en el repositorio no debilita a la etiqueta: la etiqueta apunta al **fuente del visor** en ese estado, y el flujo lo reconstruye. Lo que se pierde es la posibilidad de servir un bundle sin construirlo, que es precisamente lo que `QG-02` prohíbe.

**Y una asimetría que esta unidad no puede resolver sola**: `ADR-10006` §6 de `GeometriaFactory-Visor` acepta que un cambio mayor del punto de extensión del bundle **no lo detecta ninguna compilación** desde este lado. La mitigación desde acá es `QG-09` —**0** invocaciones al interior del bundle, con las **6** funciones de la fachada como única vía— más la revisión.

## 7. Política de cambios incompatibles

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno.** Lo que rige en su lugar:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Ante un cambio **mayor** de los de §1, el guion de demostración de la etapa **y los de todas las anteriores** pasan al **100 %** antes del punto de control | `QG-04`, con `TC-10035`. **Bloqueante** | Intake §17.2.P.6 · GeometriaFactory-Web y §15, regla de no-regresión acumulativa |
| Ante un cambio **incompatible del contrato**, **las dos unidades desplegables se despliegan juntas** | El `QG-08` de `GeometriaFactory-Contracts`, que bloquea la publicación de la etapa. Tratamiento operativo en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2 | Intake §17.2.P.3 · GeometriaFactory-Contracts |
| **0** advertencias de construcción | `QG-01`, en el paso 5 del flujo | Intake §17.2.P.8 · GeometriaFactory-Web; [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, sexta métrica |
| **0** apariciones de la dirección del servidor propio en el repositorio | Inspección del árbol de fuentes y del historial | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8, primera métrica |
| **0** etapas cerradas sin etiqueta | Inspección del historial contra el índice de informes de cierre | Intake §17.2.P.7 · GeometriaFactory-Web |
| Toda **deriva mayor** contra la línea de base visual se resuelve corrigiendo lo construido o actualizando la línea de base con aprobación humana, **nunca por omisión** | `QG-11`, al cerrar la etapa | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Todo cambio mayor recibe su fila en el registro de cambios del producto | Revisión del pull request de la etapa, que **es** el punto de control | Intake §15, regla de delivery 3 |

**Las seis métricas de [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §8 se adoptan sin agregar ninguna**, y las cuatro que no figuran arriba como obligación de versionado figuran como gates en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2: la respuesta de la dirección pública, la salida hacia el servicio de datos, el bundle generado en el mismo flujo y las publicaciones que terminan sin comprobar.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Adopta el versionado semántico 2.0.0 y las Conventional Commits 1.0.0 que el intake §17.2.P.7 · GeometriaFactory-Web declara **sin excepciones**, y declara la consecuencia de que esta unidad sea **hoja del grafo y no exponga contrato a nadie**: la clase de cambio se decide sobre **lo que la persona ve y puede hacer**, con **seis** clases y **ninguna** detectable por un compilador, entre ellas la rotura de las tres reglas de arquitectura, que es la única clase que **compila, se publica y se ve bien**. Declara la herramienta de cálculo por su función sin elegirla, y la versión de la biblioteca de componentes como valor **[A VERIFICAR]** que no se inventa. Declara el modelo de ramas con la precisión de que **acá fusionar puede desencadenar un despliegue**, y que este es el único proyecto de código que toca **las ocho** etapas comprometidas. Declara la ausencia de canales de paquete con **un** canal de despliegue, la asimetría de **qué versiona realmente la etiqueta** —incluido el bundle, que se regenera y no se restaura— y la política de cambios incompatibles con **siete** obligaciones, adoptando las seis métricas de `ADR-10007` §8 sin agregar ninguna. |
