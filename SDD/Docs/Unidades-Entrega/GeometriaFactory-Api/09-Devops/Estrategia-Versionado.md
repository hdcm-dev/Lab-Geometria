# Estrategia de versionado — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Estrategia-Versionado.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
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

**Y declara una ausencia con su sustituto, que es lo que ordena este documento.** El intake §17.1.P.3 · GeometriaFactory-Api dice que **no hay versionado de rutas porque no hay clientes de terceros**, y [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2 declara qué lo reemplaza, en cinco reglas que esta categoría transcribe y no reescribe:

1. **Una sola versión de la superficie vive a la vez**: sin prefijo de versión en las rutas, sin convivencia de dos formas de un punto y sin deprecación gradual.
2. **Todo cambio del ensamblado de contratos obliga al despliegue conjunto** de esta unidad y de la pública.
3. **Tres clases de cambio no las detecta la compilación, y cada una tiene su mecanismo**: la **configuración de intercambio**, declarada una sola vez para los dos extremos; el **esquema del almacén**, verificado al arrancar con su linaje, que detiene el arranque si no cierra; y **las rutas**, que sólo el consumidor conoce y que la batería de integración ejerce contra el servicio real.
4. **Cada etapa cerrada y fusionada recibe una etiqueta**, y la reversión es volver a la etiqueta anterior y reconstruir.
5. **La colección de peticiones reproducible es parte del contrato hacia afuera**, y cuando la superficie cambia, la colección cambia con ella.

**La tercera regla es la que esta categoría tiene que hacer operativa**, porque las tres clases que la compilación no detecta son las que un pipeline puede dejar pasar:

| Clase que la compilación no detecta | Dónde se la atrapa en la canalización | Umbral |
| --- | --- | --- |
| **Configuración de intercambio** divergente entre los dos extremos | `QG-10`, en el stage `build` | **1** sola configuración declarada en el producto |
| **Esquema del almacén** que no cierra | El stage `verificar-transformaciones` de `GeometriaFactory-Infrastructure`, y después el arranque en dos fases, que **detiene el arranque** si la preparación no se completó | 0 pasos manuales; el servicio **no escucha** si no cerró |
| **Rutas** que cambian sin que el consumidor se entere | La batería de integración, que ejerce el servicio real por su protocolo | La batería entera en verde (`QG-02`) |

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

**Y una tercera que esta categoría agrega, derivada de `QG-05`**: agregar un punto de acceso **es siempre un cambio que hay que declarar**, aunque sea aditivo, porque cambia el recuento de la guardia de admisión. No sube mayor por sí solo; lo que exige es que el pull request diga **de qué lado de la guardia queda**, y `TC-00007` lo verifica en las dos direcciones.

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

### 3.1 `GeometriaFactory-Api`

**Se declara por su función, y esta categoría no la elige**: ninguna fuente la nombra, y `PA-07` de `05` §11 deja los nombres definitivos y **las versiones exactas de los paquetes** anclados en la etapa `a`.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | **Las tres clases de §1 que la compilación no detecta.** Ninguna herramienta de comparación de superficie vería una configuración de intercambio divergente, un esquema que no cierra ni una ruta que sólo el consumidor conoce |

### 3.2 `GeometriaFactory-Domain`

**Se declara por su función y no por su producto, y es deliberado.** El intake §17.1.P.7 · GeometriaFactory-Domain dice que la versión la calcula «la herramienta que se ancle en la etapa `a`» y que se registra en ese momento; `ADR-02003` §6 acepta explícitamente depender de una herramienta todavía no elegida y **no la nombra**. Esta categoría hace lo mismo: nombrar una acá sería inventar una decisión que el intake ata a un punto de control futuro.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión a partir de las etiquetas del repositorio y de los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el mismo punto de control |
| Dónde se ancla | Etapa `a`, por la regla de anclaje de versiones del intake, encabezado de la Parte C: toda versión se fija explícitamente y su cambio mayor se documenta, nunca como efecto colateral de una actualización |
| Qué se registra | La elección y su versión, en el punto de control de la etapa `a`. Queda abierto como `PD-01` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

**Lo que no se hace es versionar a mano.** El anti-patrón está declarado en `Rules-Devops.md` §4.8 y el intake ya lo previene al exigir el cálculo por herramienta.

### 3.3 `GeometriaFactory-Application`

**Se declara por su función, y esta categoría no la elige.** `05` §11 registra el punto abierto `PA-06` —«la herramienta que calcula la versión a partir de las convenciones de mensaje de confirmación no está elegida»— y lo ata al punto de control de la etapa `a`; `ADR-04003` §7 dice lo mismo. Elegirla acá cerraría un punto abierto que la fuente dejó atado a una medición que todavía no se hizo.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Dónde vive la versión | En el archivo de proyecto, calculada; `ADR-04003` §7 lo declara |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | La clase de cambio de la fila aditiva-mayor de §1. Ninguna herramienta de comparación de superficie la marcaría como mayor sin conocer que la cara de abajo la implementa otro proyecto de código |

### 3.4 `GeometriaFactory-Infrastructure`

**Se declara por su función, y esta categoría no la elige**: el intake §17.1.P.7 · GeometriaFactory-Infrastructure remite a §17.1.P.7 · GeometriaFactory-Domain, que la ancla en la etapa `a`, y ninguna fuente la nombra.

| Aspecto | Decisión |
| --- | --- |
| Función | Calcular la versión desde las etiquetas del repositorio y los mensajes de confirmación desde la última etiqueta |
| Prefijo de etiqueta | El que se fije al anclarla, registrado en el punto de control de la etapa `a` |
| Qué **no** calcula la herramienta | **Las dos clases mayores que compilan** de §1, y **el linaje de transformaciones**, que no es una versión semántica sino una secuencia ordenada |

**Y dos versiones que se anclan y no se calculan**, las dos con efecto sobre la ejecución y no sobre el número de versión de este ensamblado:

| Qué se ancla | Dónde vive el anclaje | Fundamento |
| --- | --- | --- |
| La **herramienta de transformaciones de esquema**, instalada como **herramienta local del repositorio** para que su versión quede versionada junto al código | El archivo de herramientas del repositorio, anclado en la etapa `a` | Intake §17.1.P.1 · GeometriaFactory-Infrastructure |
| El **motor de almacenamiento en su versión embebida** por el proveedor de acceso a datos | El archivo de proyecto, anclado en la etapa `a` | Intake §17.1.P.9 · GeometriaFactory-Infrastructure |

## 4. Modelo de ramas

### 4.1 `GeometriaFactory-Api`

El del producto, heredado entero y sin variantes: **una rama por etapa** a partir de la principal, con etiqueta al fusionar; **un pull request por etapa, que es el punto de control**; **etapas en serie**; y sin OK explícito no se avanza (intake §10, §15 y §17.1.P.7 · GeometriaFactory-Api).

**Reglas de protección de la rama principal**, que es lo que esta categoría aporta:

- La fusión exige los gates bloqueantes de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, **incluida la batería de integración completa**, que vive acá y que ninguna otra canalización del producto puede correr.
- **Todo pull request que agregue o cambie un punto de acceso reejecuta `TC-00007` en las dos direcciones sobre los quince.** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 lo llama **el control que más veces hay que ejercer**.
- **Ninguna etapa se cierra sin etiqueta**, porque la reversión del servidor propio depende de ella: no hay imagen publicada a la que volver.
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

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo lo funda la propia [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) §2: **no hay a quién darle plazo**, porque el único consumidor es del mismo producto. Lo que rige en su lugar son las convenciones impuestas de `ADR-00008` §7 y sus métricas de §8:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| **Ninguna ruta lleva prefijo ni sufijo de versión**, y ningún punto de acceso convive con una forma anterior de sí mismo | Inspección de los **quince** puntos | `ADR-00008` §7 y §8, primeras dos métricas |
| **Todo cambio del ensamblado de contratos entra con el despliegue de las dos piezas en la misma etapa** | `QG-08` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa**; revisión de cada etapa que toque el ensamblado | Intake §17.1.P.3 · GeometriaFactory-Contracts; `ADR-00008` §8, sexta métrica |
| La **colección de peticiones** se actualiza en la misma intervención en que cambia la superficie, **se reproduce en cinco pasos o menos y no inventa datos de prueba** | `QG-15`, con `TC-00035`, al cierre de la etapa que la incorpora | `ADR-00008` §7 y §8, cuarta y quinta métrica |
| **0** etapas cerradas sin etiqueta | Inspección del historial | `ADR-00008` §8, tercera métrica |
| Un punto de acceso nuevo **declara de qué lado de la guardia queda** | `QG-05`, con `TC-00007` en las dos direcciones. **Exactamente 4 fuera, ni uno más** | `05` §9, primer riesgo |
| Todo cambio mayor recibe su fila en el registro de cambios del producto, **escrita en la rama de la etapa** | Revisión del pull request, que **es** el punto de control | Intake §17.1.P.7 · GeometriaFactory-Api |

**Las seis métricas de `ADR-00008` §8 se adoptan sin agregar ninguna**, y las seis figuran arriba o en [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §5.

**Y una ausencia que `ADR-00008` §2 sostiene y esta categoría no reabre**: **la pasarela de reenvío del front no se implementa**. El intake la declara **especificada y no implementada**, y su condición de reingreso está escrita: descarga de archivos, carga directa desde el navegador o migración del front a ejecución en el navegador. **Ninguna de las tres está en el tramo comprometido**, y por eso esta canalización no la contempla.

### 6.2 `GeometriaFactory-Application`

Esta sección reemplaza a la política de obsolescencia que `Rules-Devops.md` §4.3 pide, y el reemplazo está fundado: **una política de obsolescencia da plazo de migración a integradores que no se controlan, y acá no hay ninguno**. Lo que rige en su lugar sale de `ADR-04003` y de la Definition of Done:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Ante un cambio mayor, **las dos caras se corrigen en la misma etapa** | Imposible por construcción: el artefacto de agrupación no compila. Se verifica en cada pull request | `ADR-04003` §2 y §8, segunda métrica |
| **0** advertencias de construcción | `QG-01`, en el stage `build` | `ADR-04003` §8, primera métrica |
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

**La fila que conviene no perder de vista es la última de «mayor»**: perder un invariante es cambio mayor aunque ninguna firma se toque. No lo detecta ninguna herramienta de resolución de dependencias; lo detecta `QG-06`, que exige los **nueve** invariantes ejercidos con prueba de violación rechazada y sin dobles.

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
| **Mayor** | Cambia el comportamiento observable de un adaptador sin cambiar su firma: una consulta de listado empieza a cargar componentes de pieza | **No.** Lo detecta `QG-10`, con umbral **0** |
| **Mayor** | Cambia lo que se conserva del texto original del alumno | **No.** Lo detecta `QG-11`, con umbral **0** |
| **Menor** | Se agrega un adaptador para un puerto nuevo que la capa de aplicación declaró | Sí, si falta |
| **Parche** | Se corrige un adaptador para que cumpla lo que ya declaraba | — |

**Las dos filas del medio son las que importan acá.** Son cambios mayores **que compilan**, y las dos tocan lo que la fuente protege con más fuerza: la regla de no cargar componentes en los listados (intake §17.1.P.12 · GeometriaFactory-Infrastructure) y la conservación íntegra del texto original (`RN-06008`, intake §17.1.P.11 · GeometriaFactory-Infrastructure punto 2). Ninguna herramienta de comparación de superficie las vería; las ven `QG-10` y `QG-11`, y por eso son gates.

## 8. Política de obsolescencia y de cambios incompatibles

### 8.1 `GeometriaFactory-Domain`

**No hay política de plazos de obsolescencia, y declararlo es la respuesta correcta.** Una política de obsolescencia existe para dar tiempo de migración a integradores que no controlás. Acá los **dos** consumidores son proyectos de código del mismo producto, se compilan en el mismo artefacto de agrupación y en la misma ejecución del pipeline: un cambio incompatible **rompe su compilación en el acto**, que es el aviso más temprano y más barato que puede existir. Prometer «dos versiones menores antes de remover» sería una promesa hecha a nadie.

Lo que sí hay, y es obligatorio:

| Obligación | Cómo se verifica | Fundamento |
| --- | --- | --- |
| Todo cambio mayor recibe su **fila en el registro de cambios del producto**, `changelog.md` | Revisión del pull request de la etapa. Objetivo: **0** cambios mayores sin fila | `ADR-02003` §7 y §8 |
| Un cambio mayor exige que los **nueve** invariantes se verifiquen por prueba antes de fusionar | `QG-06`, con `TC-02026` | `ADR-02003` §8, cuarta métrica |
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
| Las transformaciones **se aplican solas sobre un almacén inexistente**, sin paso manual | `QG-04`, en el stage `verificar-transformaciones` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure, criterio de aceptación de la etapa `c` |
| **0** advertencias de construcción | `QG-01`, en `build` | Intake §17.1.P.8 · GeometriaFactory-Infrastructure |
| **0** componentes de pieza y **0** apariciones del texto original en una proyección de listado | `QG-10`, con `TC-06019` | Es una de las dos clases mayores que compilan (§1) |
| **0** escrituras que reemplacen el texto original conservado | `QG-11`, con `TC-06016` y `TC-06021` | La otra clase mayor que compila (§1) |
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
| **Etiqueta por etapa cerrada** | No: hay que crearla | **Nunca se creó ninguna.** `git tag` devuelve **cero** en todo el repositorio, contra el objetivo del 100 % que §1 y §10 declara |

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
| 2.1 | 2026-08-17 | Entra **§11, el registro del avance y su responsable**, con los ítems **7 y 8** que `Rules-Devops.md` **4.2** §4.3 agregó: qué documento declara la etapa, **quién lo actualiza y en qué evento**, y el **instrumento preferido**, que es el subproducto del acto. Se declara que **manda el historial del repositorio** sobre `changelog.md`, y que el objetivo del 100 % de etiquetas por etapa cerrada está **incumplido en su totalidad** —cero etiquetas en el árbol—, sin cerrarlo acá. Reparación de la divergencia `D-06` de `Audit/Estado-Del-Destino-2026-08-17.md` §2. Sube **minor**. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
