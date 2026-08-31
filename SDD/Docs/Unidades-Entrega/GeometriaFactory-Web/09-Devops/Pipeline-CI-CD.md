# Canalización CI/CD — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Pipeline-CI-CD.md
**Versión:** 3.6
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **3 secciones existen sólo en `GeometriaFactory-Visor`** —«Alcance y qué no es este pipeline», «Triggers», «Qué aporta a la canalización del front»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Alcance: acá sí hay despliegue

### 1.1 `GeometriaFactory-Web`

A diferencia de los tres proyectos de código de nivel topológico 0 y de las bibliotecas del backend, **`GeometriaFactory-Web` tiene unidad de despliegue propia**: `05` §5 la declara como «la publicación de la aplicación en el hosting público, con dominio y transporte seguro», y una de las **dos** unidades desplegables del producto.

Tres rasgos ordenan este documento, y los tres son de la fuente:

1. **Es el anfitrión del bundle del visor**, de modo que su canalización incluye la cadena de herramientas del navegador además de la de la plataforma. `05` §5 declara qué viaja adentro: la aplicación, los tipos de `GeometriaFactory-Contracts` compilados y **el bundle del visor como recurso estático generado**, que se copia al directorio de recursos estáticos y **nunca se edita a mano**.
2. **Su canal de entrega no es un registro de artefactos: es una subida por FTP**, que el intake §17.2.P.7 · GeometriaFactory-Web declara y que **no es transaccional** (`R-03`). De ahí que el flujo no termine en la subida.
3. **No tiene proyecto de pruebas propio.** El intake §17.2.P.6 · GeometriaFactory-Web lo declara: su verificación es el guion de demostración de cada etapa, acumulativo, más las pruebas de integración que ejercitan el servicio que consume. Por eso **acá no hay stage de `test` con batería propia**, y decirlo es más honesto que inventarle uno.

Lo que este pipeline ejecuta y bloquea son **los once quality gates** de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 y **las tres puertas técnicas** de su §3.2. **Esta categoría no los redefine, no los relaja y no agrega ninguno.**

## 2. Stages

### 2.1 `GeometriaFactory-Web`

### 2.1 Los ocho pasos del flujo de publicación

Los pasos son los que el intake §17.2.P.8 · GeometriaFactory-Web declara, **en su orden y sin agregar ninguno**. `05` §5 enumera **el mismo conjunto en siete**, porque agrupa las dos preparaciones de cadena de herramientas en una; el conjunto de actos es idéntico.

| # | Paso | Qué hace | Gate que verifica |
| --- | --- | --- | --- |
| 1 | Obtención del código | Trae el estado del repositorio que se va a publicar | Ninguno propio |
| 2 | Preparación de la cadena de la plataforma | Deja disponible la plataforma objetivo del front | Ninguno propio. Su falla es la señal de `PT-01.a` cuando la versión del hosting no acompaña |
| 3 | Preparación de la cadena del navegador e **instalación reproducible** de dependencias en `visor/` | Instala desde el archivo de bloqueo, no por resolución libre | Ninguno propio |
| 4 | **Empaquetado del bundle y copia** al directorio de recursos estáticos | Genera el bundle en este mismo flujo y lo copia a `src/GeometriaFactory.Web/wwwroot/js/` | **`QG-02`**: el bundle se genera **en el mismo flujo**, nunca se toma de un artefacto viejo |
| 5 | Publicación de la aplicación | Produce la salida publicable del front | **`QG-01`**: la construcción termina **sin advertencias** |
| 6 | Inyección de la dirección del servicio de datos desde secretos | Toma el valor del almacén de secretos del repositorio; **la dirección real no se versiona** | Ninguno propio. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |
| 7 | Subida por FTP | Deja la publicación en el hosting. **No es transaccional** (`R-03`) | Ninguno propio |
| 8 | **Verificación de que la dirección pública responde** | Cierra el flujo comprobando el resultado, no la acción | **`QG-03`**: el flujo **no termina en la subida** |

**El paso 8 es el que define el carácter de esta canalización.** El intake §17.2.P.8 · GeometriaFactory-Web lo funda sin ambigüedad: **«una subida por FTP que deja la aplicación caída y se reporta como exitosa es peor que una falla visible»**. Un flujo que terminara en el paso 7 dejaría sin detección el modo de falla más caro del producto.

**El paso 4 es el que hace de este flujo el único lugar donde el bundle existe para un usuario.** [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §2 cerró el punto abierto `PA-05` de aquel proyecto de código decidiendo que **el bundle se ignora en el repositorio y lo genera la canalización**, y uno de sus cuatro fundamentos es precisamente este paso: la canalización ya lo genera, y `QG-12002` prohíbe tomarlo de un artefacto viejo. Esta categoría **adopta esa decisión sin reabrirla** y resuelve su consecuencia operativa pendiente en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2.

### 2.2 Tabla de gates

| Gate | Dónde corre | Qué verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `QG-01` | Paso 5 del flujo | La construcción termina **sin advertencias** | 0 advertencias | **Bloquea la fusión** |
| `QG-02` | Paso 4, e inspección de la definición del flujo | El bundle se genera **en el mismo flujo**, y el paso de generación **precede** al de publicación sin artefacto cacheado de por medio | 0 bundles tomados de un artefacto anterior | **Bloquea la publicación** |
| `QG-03` | Paso 8 | La dirección pública **responde** | La dirección pública responde | **Bloquea el flujo** |
| `QG-04` | Ejecución del guion en el navegador del equipo anfitrión (`TC-10035`) | **100 %** de los pasos del guion de la etapa **y de todas las anteriores** | 100 % | **Bloquea el punto de control.** No es condicionado: ver §2.3 |
| `QG-05` | `TC-10029`, conteo en la pestaña de red | **0** peticiones del navegador hacia el servicio de datos, **con los dos movimientos automáticos prendidos** | 0 | **Bloqueante, sin gradación.** Es `RA-01` |
| `QG-06` | `TC-10030`, inspección del árbol de fuentes y de las dependencias de guion | **1** sola salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta | 1 y 0 | **Bloqueante** |
| `QG-07` | `TC-10003`, inspección del almacenamiento, de las marcas de sesión y del contenido servido | **0** apariciones de la credencial de sesión en el navegador | 0 | **Bloqueante.** Criterio de aceptación de la etapa `c` |
| `QG-08` | `TC-10031`, sobre el traductor de condiciones | **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **diecisiete** códigos vivos **y** sobre el camino de ausencia de respuesta | 0 | **Bloqueante.** Es `RA-03` |
| `QG-09` | `TC-10032`, inspección del árbol de fuentes | **0** invocaciones al interior del bundle: las **6** funciones de la fachada son la única vía, y **0** accesos al elemento de dibujo fuera del anfitrión | 0, 6 y 0 | **Bloqueante.** Es `RA-02` sostenida desde este lado |
| `QG-10` | `TC-10033`, conteo en la pestaña de red mientras se rota y se acerca | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja **una sola vez por trabajo** | 0 y 1 | **Bloqueante** |
| `QG-11` | Recorrido de la matriz al cerrar la etapa | Las filas de [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) que la etapa toca, verificadas con estado y fecha, y **ninguna deriva mayor sin resolver** | 0 derivas mayores abiertas | **Bloquea el cierre de la etapa** |

**Tres gates corren dentro del flujo de publicación y ocho no.** `QG-01`, `QG-02` y `QG-03` son del flujo; los otros ocho son inspecciones y recorridos que corren en el pull request de la etapa o al cerrarla. Mezclarlos en una sola columna daría la impresión falsa de que una publicación verde equivale a una etapa cerrada, y no lo es: **la publicación verifica que la aplicación quedó en pie, no que hace lo que debe**.

**Una medición de ausencia hecha sin su condición no cuenta como medición.** `QG-12005` se mide **con los dos movimientos automáticos prendidos**: un conteo hecho con los movimientos apagados daría cero sin haber ejercitado nunca el bucle de dibujo, que es donde una petición se colaría. Es el mismo criterio que [`../../GeometriaFactory-Visor/09-Devops/Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 hace cumplir del otro lado de la fachada.

### 2.3 Ningún gate de este proyecto de código es condicionado

**Los once bloquean**, cada uno lo que su columna declara: la fusión, la publicación, el flujo, el punto de control o el cierre de la etapa. El único con valor rotulado **[ASUNCIÓN]** es `QG-04`, y **no por eso queda condicionado**.

No lo decide esta categoría: [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1 lo declara y da el fundamento, apoyado en dos textos que dicen lo mismo. El intake §17.2.P.6 · GeometriaFactory-Web escribe la puerta como **«gate bloqueante y numérico en lugar de cobertura de líneas»**, con el rótulo alcanzando a **expresarlo como gate**; y el intake §22, fila `A-4`, columna «Si el Product Owner la cambia», dice **«cambia la forma del gate, no su carácter bloqueante»**.

**La regla que esta ola aplica en todo el producto es la que la Fase E fijó**: una asunción **sobre el umbral mismo** condiciona; una asunción **sobre la forma del gate** no. Acá lo que está en duda es cómo se expresa la puerta, no si detiene, y **la regla acumulativa de no-regresión no es asunción de nadie**: la declara el intake §15 como regla de delivery. Esta categoría materializa `QG-04` como **bloqueante desde la primera etapa que lo alcanza**.

**Contra la lectura opuesta**, que sería condicionarlo por prudencia: este es el único proyecto de código del producto **sin batería automatizada propia**. Condicionar su única puerta acumulativa habría dejado al front sin ningún gate que detenga un punto de control, que es exactamente lo que la fuente puso a salvo.

### 2.4 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en el paso 5** | El criterio es «construcción sin advertencias» (`QG-01`), y ninguna fuente declara un linter separado. La verificación de tipos del lenguaje del bundle ocurre en el paso 4 y su falla es falla de ese paso |
| Test | **No existe como stage con batería propia, y está declarado** | Intake §17.2.P.6 · GeometriaFactory-Web: este proyecto de código **no tiene proyecto de pruebas propio** en el árbol del repositorio. Su verificación es el guion acumulativo (`QG-00004`) más las inspecciones de `QG-00005` a `QG-00010`, y las pruebas de integración que ejercitan el servicio que consume, que **pertenecen a `GeometriaFactory-Api`** |
| SCA | **Existe, y acá sí tiene sujeto** | Es una de las **dos** unidades desplegables, y arrastra dos cadenas de dependencias: la de la plataforma —con la biblioteca de componentes de interfaz— y la del navegador, con el motor de dibujo dentro del bundle. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría** | Acá sí hay artefacto que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma, y la brecha se declara** | El canal de entrega es una subida por FTP a un hosting gratuito, sin mecanismo de verificación de firma por parte de quien recibe. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **Existe**, y es la subida más su verificación | Intake §17.2.P.7 · GeometriaFactory-Web y §17.2.P.8 · GeometriaFactory-Web. Tiene documento propio: [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md) |

### 2.2 `GeometriaFactory-Visor`

Los stages son los que declaran el intake §17.2.P.8 · GeometriaFactory-Visor y `05` §5: **instalación reproducible de dependencias → empaquetado → copia al directorio de recursos estáticos del anfitrión**. Los guiones son los que el intake §16 y §17.2.P.8 · GeometriaFactory-Visor declaran: `scripts/build-visor.sh` hace **sólo** el bundle, para el ciclo corto de trabajo sobre el visor, y `scripts/build.sh` lo encadena con la compilación del resto del producto.

**Todo corre dentro del contenedor de desarrollo**, incluido el gestor de paquetes del ecosistema del navegador (intake §17.2.P.1 · GeometriaFactory-Visor y §10).

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `instalar` | Instalación **reproducible** de dependencias desde el archivo de bloqueo, dentro del contenedor de desarrollo | Ninguno propio: su falla detiene la construcción | — | Bloqueante por construcción |
| `empaquetar` | `scripts/build-visor.sh`: el empaquetador produce el bundle | `QG-01`: el bundle **se genera sin errores** | 0 errores | **Bloqueante** |
| `inspeccionar` | Recuentos sobre el **bundle generado** y sobre el fuente (`TC-12016`, `TC-12018`) | `QG-04`: **0** peticiones originadas por el archivo de guion y **0** ocurrencias de las tres formas de petición en el fuente **y en el bundle** | 0 y 0, **medido con los dos movimientos prendidos y sostenidos** | **Bloqueante, sin gradación** |
| `inspeccionar` | Lectura del almacenamiento del navegador (`TC-12017`) | `QG-05`: **0** claves escritas y ningún estado conservado entre páginas | 0 | **Bloqueante, sin gradación** |
| `inspeccionar` | Recuento de la superficie expuesta por el bundle (`TC-12018`) | `QG-06`: exactamente **6** funciones, **1** nombre propio en el objeto global y **0** identificadores globales sueltos | 6, 1 y 0 | **Bloqueante** |
| `probar` | Batería del proyecto de código: unitario, integración y extremo a extremo en página | `QG-07`: **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** sin registro (`TC-12006`) | 100 % y 0 | **Bloqueante, sin gradación** |
| `probar` | `TC-12021`, contra §6 del contrato de fachada | `QG-08`: los códigos de condición son exactamente **siete** y **ninguno se acuña aguas abajo** | 7 y 0 | **Se rechaza en revisión** |
| **Medición de puertas** | `TC-12019` | `QG-02` (**`PT-03`**): el motor de dibujo queda **dentro** del bundle y la página funciona **sin acceso a redes de distribución externas**; **0** dependencias traídas de una red externa en tiempo de ejecución | 0 | **Bloqueante, y detiene la planificación de la etapa `g`** |
| **Medición de puertas** | `TC-12020` | `QG-03` (**`PT-02`**): el bundle carga en una página del anfitrión, la creación de instancia arma la escena, la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**, **diez** recorridos de ida y vuelta no degradan, y el árbol y la escena **se sincronizan por índice** | 3 de 3 figuras; 10 recorridos sin degradación, **con los dos movimientos prendidos** | **Bloqueante, y detiene la planificación de la etapa `g`** |
| `copiar` | Copia del bundle a `src/GeometriaFactory.Web/wwwroot/js/` | Ninguno propio. Su verificación es la del anfitrión: ver [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) §3 | — | Bloqueante por construcción |
| Revisión del pull request | Comparación del bundle contra el fuente que lo generó | `QG-09`: el bundle **nunca se edita a mano**; es artefacto generado y reproducible | 0 ediciones manuales | **Se rechaza en revisión** |

**Ningún gate de este proyecto de código es condicionado**, y no es una decisión de esta categoría: [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §4 lo declara y da el motivo —sus umbrales no salen de valores rotulados **[ASUNCIÓN]**, salen del contrato de la fachada y de las dos puertas técnicas—. **La única marca [ASUNCIÓN] que alcanza a este proyecto de código está en el intake §17.2.P.6 · GeometriaFactory-Visor y es sobre la forma del gate —expresarlo como automatizable— y no sobre la regla**, que es `RA-02` y ya es criterio de aceptación de la etapa `g`. Por la regla que la Fase E fijó, una asunción sobre la forma **no condiciona**: `QG-12004` bloquea.

**Una medición de ausencia hecha sin su condición no cuenta como medición.** Es el criterio más importante de [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3 y el pipeline lo hace cumplir: `QG-04` se mide **con los dos movimientos automáticos prendidos y sostenidos**, y el ejecutor de las pruebas de extremo a extremo tiene que poder prenderlos **aunque el entorno declare preferencia de movimiento reducido** ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2). Un pipeline que midiera cero peticiones con los movimientos apagados quedaría en verde **sin haber ejercitado nunca el bucle de dibujo**, que es donde una petición se colaría.

### 2.2 El momento de medición de las dos puertas técnicas

`PT-02` y `PT-03` **no son criterios de esta categoría ni de la 08**: las declara el intake §15 y §17.2.P.8 · GeometriaFactory-Visor, y se miden **antes de comprometer la etapa `g`**. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1 declara que esta cadena **no puede convertirlas en gates condicionados, ni cambiar lo que miden, ni agregarles criterios**, y esta categoría lo adopta sin tocar nada.

Lo que sí le corresponde a 09 es declarar **cómo se ejecutan dentro de la canalización**:

| Aspecto | Decisión |
| --- | --- |
| Cuándo corre la medición | En un momento propio del producto, que **no es una etapa**: el que [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §5 llama momento de medición, ubicado antes de comprometer la etapa `g` |
| Sobre qué corre | Sobre el **bundle generado** por el stage `empaquetar`, cargado en una página real con capacidad gráfica tridimensional. Nunca sobre el fuente |
| Qué pasa si una no pasa | **La etapa `g` no se compromete.** No hay diferimiento, no hay deuda y no hay carácter condicionado ([`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §7) |
| Qué **no** hace el pipeline | No mide fluidez con un número: ninguna fuente lo da y `PA-03` de `05` §11 lo deja abierto. La verificación es **cualitativa declarada** junto con `PT-02`, y **no se reporta como si fuera un número** |

**Diecisiete de los veintiún casos de prueba de este proyecto de código se ejecutan en ese momento**, antes de que la etapa `g` se abra ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §5). La canalización lo refleja en lugar de esconderlo: el grueso del costo de ejecución de este proyecto de código cae antes de la etapa que lo necesita.

### 2.3 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `empaquetar`** | La verificación de tipos del lenguaje fuente ocurre en el empaquetado y su falla es falla de `QG-01`. Ninguna fuente declara un linter separado; su elección concreta es de la etapa `a` |
| SCA | **Existe, y acá sí tiene sujeto** | Es el único de los tres proyectos de código de nivel topológico 0 con dependencias externas. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría**, con su alcance acotado | El motor de dibujo queda **dentro** del bundle por `PT-03`, de modo que un inventario tomado sobre las dependencias del anfitrión **no lo vería**. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | El bundle no viaja por ningún canal hacia un integrador: se copia al anfitrión y se despliega dentro de su publicación. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe como publicación externa.** Lo que existe es la **entrega al anfitrión**, y tiene documento propio | Intake §17.2.P.7 · GeometriaFactory-Visor: no se publica. Ver [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) |

## 3. Triggers, y la resolución de `PD-01`

### 3.1 `GeometriaFactory-Web`

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | Los pasos 1 a 5 del flujo, sin publicar | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Lo anterior, más las inspecciones de `QG-10005` a `QG-10010` | **La fusión** |
| Cierre de la etapa | Lo anterior, más `QG-10004` sobre el guion acumulativo y `QG-10011` sobre las filas de la matriz de sensado que la etapa tocó | El punto de control y el cierre de la etapa |
| **Fusión a la rama principal con cambios bajo `src/GeometriaFactory.Web/`, `visor/` o `src/GeometriaFactory.Contracts/`** | El flujo de publicación entero, los ocho pasos | La publicación |
| **Disparo manual** | El mismo flujo | La publicación |
| Etiqueta de cierre de etapa | El flujo sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

Las dos filas del disparo del flujo son literalmente lo que el intake §17.2.P.7 · GeometriaFactory-Web declara: el flujo se dispara «manualmente y por fusión a la rama principal, restringido a cambios bajo `src/GeometriaFactory.Web/`, `visor/` y **`src/GeometriaFactory.Contracts/`**». La tercera ruta la agregó el Product Owner en el intake **1.22**, resolviendo el hallazgo que `GeometriaFactory-Contracts` elevó a esta categoría y que esta categoría le propuso.

### 3.1 El hallazgo que Contracts elevó, leído entero

[`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §6 y §10 lo declararon así: el filtro de rutas de este flujo cubría `src/GeometriaFactory.Web/` y `visor/`, **dejaba fuera a `src/GeometriaFactory.Contracts/`**, y por eso un cambio de contrato **no disparaba la publicación del front por fusión**, aunque el `QG-08008` de aquel proyecto de código exija que las dos unidades desplegables salgan juntas ante un cambio incompatible. La regla no quedaba incumplida —el flujo también se dispara a mano—, pero **el despliegue conjunto quedaba apoyado en que alguien se acordara de dispararlo**. **El razonamiento que sigue es el que esta categoría hizo para resolverlo, y se conserva porque es el fundamento de la decisión que el Product Owner terminó tomando en el intake 1.22.**

Esta categoría es la dueña del flujo y le correspondió resolverlo. Antes de decidir, hay **tres hechos** que hay que poner juntos, y los tres son verificables:

| Hecho | Dónde se verifica |
| --- | --- |
| El filtro cubría **dos** de las **tres** entradas de compilación de este proyecto de código. El intake §13 declara sus dependencias: `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`; el filtro nombraba `visor/` y el directorio propio, y **omitía el del contrato**. **El intake 1.22 agregó la tercera ruta** | Intake §13 y §17.2.P.7 · GeometriaFactory-Web |
| **El despliegue del backend no es automático en ningún caso.** El intake §17.1.P.8 · GeometriaFactory-Api declara el despliegue de la Api **manual, por el docente**, y que el agente entrega el archivo de construcción y el de composición **y no ejecuta el despliegue** | Intake §17.1.P.8 · GeometriaFactory-Api y §10 |
| El desfase que `QG-00008` teme **sigue siendo posible con el filtro ampliado, y en la dirección contraria**: una fusión que toca sólo `src/GeometriaFactory.Web/` publica el front automáticamente, mientras la Api espera una acción humana. Agregar la ruta del contrato no cambia esto | Intake §17.2.P.7 · GeometriaFactory-Web contra §17.1.P.8 · GeometriaFactory-Api |

**El tercer hecho es el que cambia el problema.** El filtro de rutas no es la pieza que sostiene el despliegue conjunto, y no podría serlo: **con un extremo automático y el otro manual, el despliegue conjunto es siempre un acto humano coordinado**, y agregar una ruta al filtro no lo vuelve automático. Lo que el filtro sí determina es algo más simple y más grave: **si un cambio de contrato llega o no a estar construido y publicado en el front**.

### 3.2 Qué decide esta categoría

**Tres decisiones, y las tres van declaradas como derivadas de esta categoría y no como texto de la fuente.**

| # | Decisión | Fundamento | Estado |
| --- | --- | --- | --- |
| 1 | **El filtro de rutas del flujo incluye `src/GeometriaFactory.Contracts/`**, además de `src/GeometriaFactory.Web/` y `visor/` | Es una entrada de compilación de esta unidad (intake §13). Un cambio de contrato que no reconstruye el front deja publicada una aplicación compilada contra una versión anterior del contrato, que es `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 | **Confirmada por el Product Owner** en el intake **1.22**: §17.2.P.7 · GeometriaFactory-Web enumera hoy las **tres** rutas. Ya no es decisión derivada de esta categoría, es texto de la fuente. `PD-01` de §10 queda cerrado |
| 2 | **`QG-08008` de `GeometriaFactory-Contracts` sigue siendo el gate que sostiene el despliegue conjunto**, y esta categoría no lo reemplaza por el filtro | El filtro dispara una construcción; **no coordina dos despliegues**, y uno de los dos es manual por decisión del Product Owner | Adopción sin cambios |
| 3 | Ante un cambio **incompatible** del contrato, la publicación del front **no se declara cerrada** hasta que la unidad del servidor propio esté desplegada desde el mismo estado del repositorio, con constancia en el informe de cierre de la etapa. **El backend sale primero** | `QG-08008` bloquea la **publicación de la etapa**, no la fusión ([`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §6, paso 2); el orden lo fija el intake §17.2.P.7 · GeometriaFactory-Web desde 1.22 | Materialización en [`Entornos-Deploy.md`](Entornos-Deploy.md) §6 |

**Cómo quedó la primera decisión.** Esta categoría la propuso y la elevó, porque el intake §17.2.P.7 · GeometriaFactory-Web enumeraba dos rutas y no tres y agregar la tercera **cambiaba lo que la fuente declara**. El Product Owner la confirmó en el intake **1.22**, con el mismo fundamento que esta categoría había escrito: el contrato es entrada de compilación del front, y con dos rutas existía el caso silencioso de tocar el contrato, no tocar el front, fusionar y publicar un front compilado contra el contrato anterior. **La tercera ruta rige, y el disparo manual deja de ser lo que sostiene el caso**; la constancia del paso 3 sigue rigiendo, porque el filtro nunca sostuvo el despliegue conjunto.

**Y el orden de salida, que la misma versión del intake decidió.** Cuando front y backend salen juntos, **primero el backend**: una API nueva normalmente acepta lo que mandaba el front anterior, mientras que un front nuevo contra una API vieja le pide algo que todavía no existe. El tratamiento completo está en [`../../GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md) §6, que es la categoría dueña del despliegue del backend.

**Lo que esta categoría no hace.** No convierte el despliegue del backend en automático —lo prohíbe el intake §10 y §17.1.P.8 · GeometriaFactory-Api—, no relaja `QG-00008` y no declara resuelto el desfase de momentos: **el intake 1.22 declara expresamente que el orden no vuelve automático el despliegue conjunto**, de modo que el desfase sigue siendo **irreducible mientras un extremo se despliegue a mano** y el mecanismo sigue siendo la constancia escrita y no un disparador. Lo que el orden logra es que el intervalo **se minimice y no se elimine**.

## 4. Matriz de plataformas

### 4.1 `GeometriaFactory-Web`

Este proyecto de código tiene **tres plataformas y no una**, y confundirlas sería el error característico acá:

| Momento | Plataforma | Fundamento |
| --- | --- | --- |
| **Construcción** | Las **dos** cadenas de herramientas, la de la plataforma y la del navegador, dentro del contenedor de desarrollo. El equipo anfitrión no las tiene instaladas | Intake §10 y encabezado de la Parte C; `05` §5, fila de etapas |
| **Ejecución del servidor** | El hosting gratuito, con servidor de información, transporte seguro y dominio público. ~~**La versión de plataforma que soporta está [A VERIFICAR]** en la fuente~~ **MEDIDA el 2026-08-13: soporta `net10.0`**; `PT-01.a` pasa con **200** | Intake §17.2.P.9 · GeometriaFactory-Web, **con la marca retirada el 2026-08-31** |
| **Ejecución del navegador** | Cualquiera con **capacidad gráfica tridimensional** y con conexión persistente o su repliegue. La fuente **no fija versiones mínimas** | Intake §17.2.P.9 · GeometriaFactory-Web; `05` §5 |

**El requisito de navegador se declara por capacidad y no por versión**, porque la fuente no fija ninguna, y **toda combinación sin capacidad gráfica se considera no soportada** para el visor —el resto del producto sigue disponible (`05` §5)—. Esta categoría **no cierra ese hueco inventando una versión**.

**Y una consecuencia de la fila del medio que conviene no perder.** Si `PT-01.a` no pasa, la salida declarada por el intake §17.2.P.9 · GeometriaFactory-Web es **bajar la versión objetivo del front, no la del backend**, porque son dos artefactos independientes. `GeometriaFactory-Contracts` registró en su `PD-02` que una bajada así lo alcanza, porque su ensamblado se carga en los dos procesos: **esta canalización es el lugar donde esa bajada se ejecutaría**, y la restricción que hereda es que el ensamblado de contratos tiene que seguir siendo cargable por los dos.

### 4.2 `GeometriaFactory-Visor`

Este proyecto de código tiene **dos plataformas y no una**, y confundirlas sería el error característico acá:

| Momento | Plataforma | Fundamento |
| --- | --- | --- |
| **Construcción** | El entorno de ejecución de la cadena de herramientas, en versión de soporte prolongado **anclada**, provista por el contenedor de desarrollo. El gestor de paquetes corre dentro del contenedor | Intake §17.2.P.1 · GeometriaFactory-Visor y §17.2.P.9 · GeometriaFactory-Visor |
| **Ejecución** | El navegador, **con capacidad gráfica tridimensional**. Sin ella el visor **no es soportado**, y la fachada informa la condición correspondiente. **En tiempo de ejecución no hay entorno de la cadena de herramientas**: hay un archivo servido como recurso estático | Intake §17.2.P.9 · GeometriaFactory-Visor; `05` §5 |

**El requisito de navegador se declara por capacidad y no por versión**, porque la fuente no fija versiones mínimas (intake §17.2.P.9 · GeometriaFactory-Visor). Es el punto abierto `PA-04` de `05` §11 y **esta categoría no lo cierra inventando una versión**.

**Consecuencia sobre la matriz del pipeline:** la construcción corre en una sola plataforma —la del contenedor— y la medición de extremo a extremo exige **un navegador con capacidad gráfica tridimensional y un conductor capaz de contar peticiones de red y de leer el almacenamiento del navegador** ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2). **Sin ese conductor, `QG-12004`, `QG-12005` y las dos puertas técnicas no se pueden medir**, y un ejecutor que no lo provea deja al proyecto de código sin sus gates más importantes. Queda registrado como `PD-02` en §10.

## 5. Caché y artefactos

### 5.1 `GeometriaFactory-Web`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de la cadena de la plataforma | Caché del restaurador, con llave derivada de los archivos de proyecto | Decisión de esta categoría |
| Caché de la cadena del navegador | Caché del gestor de paquetes, con llave derivada del archivo de bloqueo de `visor/` | Decisión de esta categoría |
| Invalidación | Al cambiar el archivo de proyecto o el de bloqueo. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El bundle no se cachea entre ejecuciones.** Se regenera en cada flujo | `QG-10002`: nunca se toma de un artefacto viejo. Una caché del bundle sería exactamente el artefacto viejo que el gate prohíbe |
| Artefacto del paso 4 | El bundle, copiado a `src/GeometriaFactory.Web/wwwroot/js/` | Intake §13 y §16 |
| Artefacto del paso 5 | La salida publicable del front, con los tipos del contrato compilados adentro | `05` §5, fila de qué viaja adentro |
| Artefacto del paso 8 | El **registro de la respuesta** de la dirección pública | `QG-10003` |
| Artefactos de inspección | Los **recuentos** de `QG-10005` a `QG-10010`, y el estado y la fecha de las filas de la matriz de sensado que la etapa tocó | `Estrategia-Calidad.md` §3; `Plan-Pruebas.md` §3 |
| Inventario de componentes | Emitido en el flujo de publicación, sobre las **dos** cadenas. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 | Decisión de esta categoría |
| Retención | Mientras dure el punto de control; los registros se adjuntan al informe de cierre | Intake §15, regla de delivery 3 |

**La cuarta fila es la única prohibición de caché del producto, y no es una precaución de esta categoría**: es la consecuencia directa de `QG-10002`. Un flujo que reutilizara el bundle de una ejecución anterior estaría publicando un artefacto que no se generó en ese flujo, que es literalmente lo que el gate mide.

### 5.2 `GeometriaFactory-Visor`

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Instalación de dependencias | **Reproducible desde el archivo de bloqueo**, no resolución libre de versiones | Intake, encabezado de la Parte C: toda versión se fija explícitamente y un cambio mayor se documenta, **nunca como efecto colateral de una actualización** |
| Caché | Caché del gestor de paquetes, con llave derivada del archivo de bloqueo. **Sin expiración por tiempo** | Decisión de esta categoría; un plazo en días no lo da ninguna fuente |
| Artefacto principal | **El bundle**, `visor.bundle.js`, producido por el stage `empaquetar` y copiado a `src/GeometriaFactory.Web/wwwroot/js/` | Intake §13 y §16 |
| Artefacto de inspección | Los **recuentos** de `QG-12004`, `QG-12005` y `QG-12006`, tomados sobre el bundle generado | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3 |
| Artefacto del momento de medición | El registro de `PT-02` y `PT-03`, **con la condición en que se midió cada ausencia** | Definition of Done §1.3 |
| Inventario de componentes del bundle | Emitido en el stage `empaquetar`. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 | Decisión de esta categoría |
| Retención | Mientras dure el punto de control; los registros se adjuntan al informe de cierre | Intake §15 |
| Reproducibilidad exigida | **Dos construcciones desde el mismo estado producen el mismo artefacto** | [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §8, segunda métrica |

**El bundle no se versiona en el repositorio.** Es la resolución del punto abierto `PA-05`, y su fundamento completo está en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2: se genera en cada canalización que lo necesita y se ignora en el control de versiones.

## 6. Promoción

### 6.1 `GeometriaFactory-Web`

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Los gates bloqueantes de §2.2 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Rama principal → publicación en el hosting | El flujo de §2.1, entero | `QG-10001`, `QG-10002` y `QG-10003`, y la Definition of Done §1.4 en sus **seis** puntos | El mismo, con el registro del flujo |
| **Publicación → etapa cerrada, ante un cambio incompatible del contrato** | La constancia del despliegue conjunto | La unidad del servidor propio desplegada **desde el mismo estado del repositorio y antes que la publicación del front**, por el orden que el intake §17.2.P.7 · GeometriaFactory-Web fija desde 1.22 | El mismo, con constancia escrita. Ver §3.2, decisión 3 |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, y `QG-10004` y `QG-10011` en verde | El mismo |

**La publicación se hace fuera del horario de uso**, y no es una recomendación de esta categoría: el intake §17.2.P.8 · GeometriaFactory-Web lo declara como el tratamiento de una subida que **no es transaccional**, y la Definition of Done §1.4 lo exige con la hora registrada del flujo.

### 6.2 `GeometriaFactory-Visor`

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Los gates bloqueantes de §2.1 y los **diez** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con OK explícito (intake §15) |
| **Momento de medición → compromiso de la etapa `g`** | Las dos puertas técnicas pasadas enteras, en sus **seis** tramos `CV-12018` a `CV-12023` | `TC-12019` y `TC-12020`, con sus condiciones de medición registradas | El mismo, con el registro de la medición |
| Bundle construido → bundle en el anfitrión | El stage `copiar` | Los gates de `inspeccionar` en verde sobre ese mismo bundle | Automático dentro de la construcción |

**La segunda fila no tiene equivalente en ningún otro proyecto de código de nivel topológico 0.** Es una promoción cuyo prerrequisito no es un gate de esta cadena sino una **puerta técnica del producto**, y su incumplimiento no bloquea una fusión: bloquea que una etapa **se planifique**.

## 7. Reversión

### 7.1 `GeometriaFactory-Web`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| La publicación dejó la aplicación caída | **Volver a publicar desde la etiqueta anterior.** El flujo entero corre de nuevo, incluido el paso 4, de modo que el bundle también se regenera | Intake §17.2.P.8 · GeometriaFactory-Web; `05` §5, fila de reversión; Definition of Done §1.4 |
| El bundle publicado rompe la visualización | El mismo procedimiento: la reversión efectiva del bundle **es la del front**, y su regeneración es parte de esa publicación | [`../../GeometriaFactory-Visor/09-Devops/Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) §4, segunda fila |
| Un cambio incompatible del contrato llegó a las dos unidades y hay que volver atrás | **Se revierten las dos juntas**, por la misma regla que las obliga a desplegarse juntas | [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §7, segunda fila; `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 |
| La dirección del servicio de datos cambió y el front dejó de alcanzarlo | **No se revierte nada**: se actualiza el secreto y se vuelve a publicar. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §5, consecuencia positiva 2 |

**No hay delist ni retiro de versión publicada**, porque no hay repositorio de paquetes del que retirarla. **Y no hay despliegue con solapamiento**: el canal es una subida sobre el mismo destino, de modo que la reversión es otra publicación y no un cambio de tráfico.

### 7.2 `GeometriaFactory-Visor`

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| El bundle desplegado rompe la visualización | Volver a la **etiqueta de la etapa anterior** y **regenerar** el bundle desde ese estado. No se restaura un archivo guardado: se reconstruye | Intake §17.1.P.8 · GeometriaFactory-Domain, modelo del producto; [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §5, punto 2 |
| El bundle desplegado en el hosting está roto | La reversión efectiva es la del front: **volver a publicar desde la etiqueta anterior**, que es el procedimiento que el intake §17.2.P.8 · GeometriaFactory-Web declara para esa unidad. La regeneración del bundle es parte de esa publicación | Intake §17.2.P.8 · GeometriaFactory-Web |
| Un cambio mayor del punto de extensión rompió al anfitrión | **Ninguna compilación lo detectó**, y es la asimetría que `ADR-12006` §6 acepta. Se revierte por etiqueta y la mitigación previa es la revisión más el sample **S-1**, que ejerce el contrato entero | `ADR-12006` §2 y §5, punto 3 |

**Que el bundle se regenere y no se restaure es consecuencia directa de resolver `PA-05` como se resolvió**, y es una propiedad y no un costo: un archivo restaurado desde el control de versiones podría no corresponder al fuente, que es exactamente lo que `QG-12009` y `CV-12030` prohíben.

## 8. Notificaciones

### 8.1 `GeometriaFactory-Web`

| Canal | Qué comunica |
| --- | --- |
| La salida del flujo de publicación | El resultado de los ocho pasos, con el **registro de la respuesta** de la dirección pública y la hora de la publicación |
| La salida de las inspecciones sobre el pull request de la etapa | Los **recuentos** de `QG-10005` a `QG-10010`, cada uno con la condición en que se midió |
| El informe de cierre de la etapa | Lo anterior, más el resultado del guion acumulativo (`QG-10004`), el estado de las filas de la matriz de sensado que la etapa tocó, **la decisión tomada ante toda deriva mayor** y, cuando hubo cambio incompatible del contrato, la constancia del despliegue conjunto |
| El registro de cambios del producto | Toda fila de cambio mayor de esta unidad |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Lo que sí se comunica a la persona que usa el producto, y no es una notificación de esta categoría**, es el **estado degradado** cuando el servicio de datos no responde: es una superficie declarada del front ([`ADR-10005`](../05-Arquitectura-Tecnica/Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md)), y **nunca incluye la dirección del servicio interno** (`QG-10008`, `RA-03`).

### 8.2 `GeometriaFactory-Visor`

| Canal | Qué comunica |
| --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado del empaquetado y **los recuentos** de las tres inspecciones sobre el bundle generado |
| El registro del momento de medición | `PT-02` y `PT-03` tramo por tramo, **con la condición de medición junto a cada resultado** |
| El informe de cierre de la etapa | Lo anterior, más la verificación cualitativa de fluidez **rotulada como cualitativa** |
| El registro de cambios del producto | Toda fila de cambio mayor del punto de extensión (`ADR-12006` §7) |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

## 9. Las tres puertas técnicas dentro de la canalización

### 9.1 `GeometriaFactory-Web`

`PT-01`, `PT-02` y `PT-03` **no son criterios de esta categoría ni de la 08**: las declara el intake §15, y `Estrategia-Calidad.md` §3.2 las transcribe con la constancia de que **sus umbrales no son asunciones**. Lo que le corresponde a 09 es declarar cómo se ejecutan dentro de la canalización:

| Puerta | Cuándo corre | Sobre qué | Qué pasa si no pasa |
| --- | --- | --- | --- |
| `PT-01`, en sus **cuatro** partes | Etapa `a`, **antes que cualquier otra cosa** | Sobre el front **ya publicado** en el hosting, no sobre una ejecución local: `PT-01.a` mide la dirección pública y `PT-01.c` mide **20 minutos** de navegación continua contra el proceso del hosting | **Detiene la planificación** de lo que dependa de ella. Sólo el rojo en el transporte o la falla de estabilidad obligan a cambiar el modelo de front; el repliegue de mayor latencia **no es motivo de rediseño** |
| `PT-02` | Antes de comprometer la etapa `g` | Sobre el bundle generado, cargado en una página del anfitrión | La etapa `g` **no se compromete** |
| `PT-03` | Antes de comprometer la etapa `g` | Sobre el bundle. **No tiene caso de verificación propio acá**: es propiedad del bundle y se verifica del lado de `GeometriaFactory-Visor` | La etapa `g` **no se compromete** |

**La primera fila tiene una consecuencia sobre el orden de la canalización que ninguna otra puerta del producto tiene**: `PT-01` **exige que el flujo de publicación exista y haya corrido** antes de que se pueda medir. Es la única puerta técnica del producto que se mide sobre un artefacto ya desplegado, y por eso `BT-10001` —crear el proyecto del front **con su flujo de publicación**— y `BT-10006` —la puerta de publicación que termina comprobando— son de la etapa `a` y preceden a `BT-10004`, que es la medición.

**`PT-01.c` es el peor escenario declarado y no tiene mitigación en el código** (`R-06`). Esta categoría lo registra y **no le inventa una**: un reintento, un calentamiento periódico o un segundo proceso serían infraestructura que ninguna fuente declara y que el intake §10 no financia.

## 10. Puntos abiertos

### 10.1 `GeometriaFactory-Web`

> **Correspondencia con `Root-Rules.md` §12.2.** La columna **`Punto abierto`** realiza sus campos
> **1 · qué falta** —el enunciado en negrita— y **2 · por qué no se puede hoy** —el desarrollo que
> sigue—; **`Quién lo cierra`** realiza el campo 3 y **`En qué evento se cierra`** el campo 4.
> **`Estado` no es un campo de §12.2**: deriva de su tabla de escalamiento y se declara como tal.


| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | ~~**La inclusión de `src/GeometriaFactory.Contracts/` en el filtro de rutas del flujo de publicación** (§3.2, decisión 1). Es la resolución que esta categoría propone al hallazgo que `GeometriaFactory-Contracts` elevó, y **cambia lo que el intake §17.2.P.7 · GeometriaFactory-Web enumera**, de modo que se eleva en lugar de darse por tomada~~ · **CERRADO el 2026-08-11**: el Product Owner la confirmó en el intake **1.22**, y §17.2.P.7 · GeometriaFactory-Web enumera hoy las **tres** rutas | El Product Owner, sobre el intake §17.2.P.7 · GeometriaFactory-Web | `PRODUCT-INTAKE` **1.22** §17.2.P.7 | **Cerrado** el 2026-08-11 |
| PD-02 | La **exclusión en el control de versiones** del directorio de salida del empaquetado y del bundle copiado bajo los recursos estáticos. Es la acción pendiente que [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §2.2 declaró con su fecha de lectura, y su tramo de este lado lo resuelve [`Entornos-Deploy.md`](Entornos-Deploy.md) §2 | El equipo, con `BT-10001` | `.gitignore`, bloque `AP-04` | **Cerrado** el 2026-08-20 · **A2, por lectura**: `visor/dist/` está ignorado, con su motivo escrito al lado |
| PD-03 | La **herramienta concreta** de cada paso del flujo —cadenas de herramientas, empaquetador, cliente de subida y generador del inventario— y su anclaje de versión, ~~incluida la **versión de la biblioteca de componentes de interfaz**, que la fuente deja **[A VERIFICAR]** (`PA-01` de `05` §11)~~ **—esa cláusula queda SIN OBJETO el 2026-08-31: no hay biblioteca de componentes, y `PA-01` lo cerró por lectura el 2026-08-20—** | El equipo, en el punto de control de la etapa `a`, con `BT-10002` | `scripts/build.sh`, `scripts/test.sh` y `.github/workflows/deploy-front-ftp.yml` | **Cerrado en parte** el 2026-08-20 · **A2b, por lectura**: las herramientas están ancladas en los guiones — `dotnet build`, `dotnet test`, `npm ci`, `webpack` y `playwright`. **Y el generador del inventario, que este enunciado nombra, NO está entre ellas**: sigue abierto como [`PD-10`](Supply-Chain-Seguridad.md) de [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2.b, con evento en la fase `i`. Acotado el **2026-08-27** por el parche `P-01` de la mesa (hallazgo `H-01`), que cierra el `HM-01` del informe de migración 10.0 → 13.3 §7 |
| PD-04 | Que el ejecutor del flujo provea **navegador con capacidad gráfica tridimensional y conductor capaz de contar peticiones y leer el almacenamiento**. Sin eso, `QG-10005`, `QG-10007` y `QG-10010` no se pueden medir en la canalización y quedarían como medición manual registrada | El equipo, antes de la etapa `c`, que es donde entra `QG-10007` | `scripts/verify-stage-g.sh`, anclaje de la imagen | **Cerrado** el 2026-08-20 · **A2, por lectura**: el ejecutor usa `mcr.microsoft.com/playwright:v1.48.0-jammy`, anclado |
| PD-05 | La **versión de plataforma que soporta el hosting**, **[A VERIFICAR]** en la fuente (`PA-02` de `05` §11). Es `PT-01.a`, **se mide y no se decide**, y su desenlace alcanza al ensamblado de contratos | La medición de `PT-01.a`, con `BT-10004` | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.1, **fase `i` · Despliegue real**, que es donde `PT-05` se mide contra el hosting real | ~~**Vigente, y su evento reasignado el 2026-08-27** por el parche `P-06` de la mesa (hallazgo `H-05`)~~ **CERRADO el 2026-08-31 por lectura: la medición existe desde el 2026-08-13.** `PT-01.a` **PASA** —la ruta pública responde **200**— y **el hosting soporta `net10.0`**, confirmado además desde el panel de la cuenta; **no hizo falta bajar la versión objetivo del front**, que era la salida declarada para el caso contrario. Registrada en `Reporte-Despliegue-Somee.md` §2, `Compatibilidad-Plataformas.md` y `Guia-Publicacion-Front-Ftp.md`. **El parche `P-06` de la mesa del 2026-08-27 le reasignó el evento a la fase `i` sin contrastarla contra esa medición**: acertó en que la etapa `a` no podía cerrarla *decidiendo*, y no vio que ya la había cerrado *midiendo*. Inventario: [`../../../Audit/Inventario-Marcas-A-Verificar-2026-08-31.md`](../../../Audit/Inventario-Marcas-A-Verificar-2026-08-31.md) §2.1. **Y el parche `P-06` merece su constancia, porque el error está en una sola palabra suya**: escribió que la etapa `a` cerró el 2026-08-13 «**sin registrarla**», y la etapa `a` **sí la registró** —ese mismo día, en tres documentos—. El resto del razonamiento de `P-06` es correcto: esta fila no se cerraba decidiendo. Sólo que ya estaba cerrada midiendo |

**`PA-07` de `05` §11 queda cerrado por esta categoría.** Preguntaba si el bundle generado se versiona en el repositorio o se ignora, y lo derivaba a 09; la decisión ya está tomada en [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §2 —**se ignora y lo genera la canalización**— y esta categoría la adopta desde el lado del anfitrión, con su consecuencia operativa en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2. **`PA-04` —el umbral numérico de tiempo de respuesta— sigue abierto y esta categoría no lo cierra**: inventar un número acá lo propagaría como si fuera del producto.

### 10.2 `GeometriaFactory-Visor`

| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —empaquetador, ejecutor de pruebas, conductor de navegador y generador del inventario de componentes— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | `scripts/build.sh`, `scripts/test.sh` y `.github/workflows/deploy-front-ftp.yml` | **Cerrado en parte** el 2026-08-20 · **A2b, por lectura**: las herramientas están ancladas en los guiones — `dotnet build`, `dotnet test`, `npm ci`, `webpack` y `playwright`. **Y el generador del inventario, que este enunciado nombra, NO está entre ellas**: sigue abierto como [`PD-10`](Supply-Chain-Seguridad.md) de [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2.b, con evento en la fase `i`. Acotado el **2026-08-27** por el parche `P-01` de la mesa (hallazgo `H-01`), que cierra el `HM-01` del informe de migración 10.0 → 13.3 §7 |
| PD-02 | Que el ejecutor de la canalización provea **navegador con capacidad gráfica tridimensional y conductor capaz de contar peticiones y leer el almacenamiento**. Sin eso, `QG-12004`, `QG-12005` y las dos puertas técnicas no se pueden medir en la canalización y quedarían como medición manual registrada | El equipo, antes del momento de medición | `scripts/verify-stage-g.sh`, anclaje de la imagen | **Cerrado** el 2026-08-20 · **A2, por lectura**: el ejecutor usa `mcr.microsoft.com/playwright:v1.48.0-jammy`, anclado |
| PD-03 | La **versión del motor de dibujo tridimensional** que se ancla, y el cambio de interfaz que exija si es posterior a la del visualizador previo | El equipo, por `BT-12009` | `visor/package.json`, dependencia `three` | **Cerrado** el 2026-08-20 · **A2b, por lectura**: anclado en **`"three": "0.169.0"`** |

**`PA-05` de `05` §11 queda cerrado por esta categoría**, con su desenlace y su fundamento en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2. **`PA-03` —el umbral numérico de fluidez— sigue abierto y esta categoría no lo cierra**: inventar un número acá lo propagaría como si fuera del producto.

## 11. Alcance y qué no es este pipeline

### 11.1 `GeometriaFactory-Visor`

`GeometriaFactory-Visor` **no es un servicio desplegable y tampoco es una biblioteca compilada como las otras dos de nivel topológico 0**: es un proyecto de la cadena de herramientas del navegador que produce **un archivo de guion generado**, el bundle. `05` §5 lo declara sin unidad de despliegue propia: su artefacto se copia al directorio de recursos estáticos de `GeometriaFactory-Web` y **viaja dentro del despliegue de esa unidad**. `redistribuible` es false y el intake §17.2.P.7 · GeometriaFactory-Visor declara que **no se publica** en ningún repositorio de paquetes del ecosistema del navegador.

De ahí que su DevOps sea **construcción reproducible, medición sobre el artefacto generado y entrega al anfitrión**. Hay tres rasgos que lo distinguen de los otros dos proyectos de código de nivel 0, y los tres ordenan este documento:

1. **Tiene dependencias externas reales.** El motor de dibujo tridimensional entra como dependencia declarada y **termina dentro del bundle**, no por red de distribución externa (intake §17.2.P.1 · GeometriaFactory-Visor, puerta `PT-03`). Es el único de los tres con superficie de cadena de suministro que analizar.
2. **Sus gates se miden sobre el artefacto generado y no sobre el fuente.** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara la revisión sobre el bundle generado ante todo cambio, con el fundamento de que una dependencia que hace una petición por dentro **no aparece en el código fuente y sí en el bundle**.
3. **Dos puertas técnicas del producto se miden acá**, `PT-02` y `PT-03`, y **detienen la planificación de la etapa `g`** si no pasan (intake §15).

## 12. Triggers

### 12.1 `GeometriaFactory-Visor`

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa que toca `visor/` | `instalar` → `empaquetar` → `inspeccionar` → `probar` | Nada por sí solo |
| **Todo cambio del bundle** | `inspeccionar` entero —`QG-12004`, `QG-12005` y `QG-12006`— **sobre el bundle generado y no sólo sobre la fuente** | La fusión. Es la cadencia propia que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara |
| Apertura o actualización del pull request de la etapa | Todos los stages, más la revisión de `QG-12008` y `QG-12009` | La fusión |
| **Antes de comprometer la etapa `g`** | La medición de `PT-02` y `PT-03` completa, con `TC-12019` y `TC-12020` | **El compromiso de la etapa `g`** |
| Fusión a la rama principal | Todos los stages sobre el estado fusionado, **incluida la copia al anfitrión** | El cierre de la etapa |
| Fusión a la rama principal con cambios bajo `visor/` | Además, el flujo de trabajo de publicación del front, que el intake §17.2.P.7 · GeometriaFactory-Web declara restringido a cambios bajo `src/GeometriaFactory.Web/`, `visor/` y `src/GeometriaFactory.Contracts/` | La publicación del front |
| Etiqueta de cierre de etapa | Todos los stages sobre el estado etiquetado | La declaración de etapa cerrada |

**La anteúltima fila es propia de este proyecto de código**: el filtro de rutas de ese flujo de trabajo incluye `visor/`. **Desde el intake 1.22 ya no es el único caso** entre los tres proyectos de código de nivel topológico 0 —el filtro incluye también `src/GeometriaFactory.Contracts/`, de modo que un cambio del ensamblado de contratos dispara la misma publicación—; lo que sigue siendo propio de acá es que **el disparo existe porque el bundle se genera dentro de ese flujo**, y no porque el cambio haya que volver a desplegarlo junto con otra cosa.

**No hay trigger por calendario**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

## 13. Qué aporta a la canalización del front

### 13.1 `GeometriaFactory-Visor`

Este proyecto de código no se despliega, pero **su artefacto viaja dentro del despliegue del front**, y eso lo hace parte de esa canalización:

| Aporte | Efecto | Fundamento |
| --- | --- | --- |
| El flujo de trabajo del front **genera el bundle en su propio interior** —instalación de dependencias, empaquetado y copia a los recursos estáticos— antes de publicar | La canalización del front **no toma el bundle del repositorio**: lo construye | Intake §17.2.P.8 · GeometriaFactory-Web, pasos del flujo de trabajo |
| Ese flujo declara como **gate bloqueante** que el bundle se genere en el mismo flujo y **nunca se tome de un artefacto viejo** | Es la razón operativa por la que versionar el bundle no aportaría nada: el artefacto versionado no se usaría | Intake §17.2.P.8 · GeometriaFactory-Web, quality gates |
| Un cambio bajo `visor/` **dispara** ese flujo | El filtro de rutas lo incluye desde el principio; desde el intake **1.22** también incluye `src/GeometriaFactory.Contracts/`, así que **dos** de los tres proyectos de código de nivel 0 disparan esa publicación, y `GeometriaFactory-Domain` ninguno | Intake §17.2.P.7 · GeometriaFactory-Web |
| `PT-03` garantiza que el motor de dibujo esté **dentro** del bundle | El front funciona sin acceso a redes de distribución externas, que es lo que la puerta mide | Intake §17.2.P.8 · GeometriaFactory-Visor |
| `RA-02`, sostenida por `QG-12004` | El bundle no hace red, y por eso `RA-01` **no se puede violar desde el navegador** | Intake §14 |

## 14. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
| 3.0 | 2026-08-19 | **Migración normativa 9.12 → 10.0, fase M4.** Las **ocho** filas de puntos abiertos de §10 pasan a la forma de `Root-Rules.md` **§12.2**: la columna «Cuándo» —que nombraba **momentos**— se reemplaza por **«En qué evento se cierra (artefacto y sección)»**, y entra la columna **«Estado»**. El cambio no es de redacción: un momento no deja rastro que alguien pueda abrir, y §12.2 exige un evento comprobable. **Y al nombrarlo, siete de las ocho resultaron VENCIDAS**: seis apuntaban a los puntos de control de las etapas `a`, `c` y `g` —cerradas el 2026-08-13, el 08-14 y el 08-17— y la séptima al momento de medición de `PT-02`/`PT-03`, medidas en la `g`. **Verificado y no supuesto**: la sección «Decidido en esta etapa» de la etapa `a` en `changelog.md` registra **cuatro** decisiones y **ninguna es un anclaje de herramienta**. La única cerrada, `PD-01` de §10.1, ya lo estaba con el intake **1.22**. **Ningún punto abierto se cierra acá y ninguno se inventa**: la migración los vuelve contables, y cerrarlos es del equipo y del Product Owner. Sube **major**: la estructura de la tabla de §10 cambia. | Orquestador de migración normativa SDD |
| 3.1 | 2026-08-20 | **Conversión de nomenclatura, `N-01`.** La columna que la fase M4 emitió como «Dónde se cierra» pasa a **«En qué evento se cierra (artefacto y sección)»**, que es como `Root-Rules.md` **7.0** §12.2 nombra literalmente su **campo 4**. No es cosmético: *«dónde»* nombra un **lugar** y el campo nombra un **evento**, y esa distinción es la que §12.2 existe para sostener. Entra además la **nota de correspondencia** de los cuatro campos con las cinco columnas, que declara que `Punto abierto` realiza los campos **1 y 2** juntos y que **`Estado` no es un campo de §12.2** sino un derivado de su tabla de escalamiento. **Se declara en lugar de partir la columna**, porque partirla obligaría a reescribir la prosa de las filas que `Informe-Migracion-9.12-a-10.0.md` `A7` verificó **idénticas**. **Ninguna fila cambia de contenido, de estado ni de recuento.** Plan en `Audit/Plan-Conversion-Nomenclatura-Item-Diferido.md`. Sube **minor**: cambia un rótulo y entra una nota; la estructura de la tabla no se toca. | Orquestador SDD |
| 3.2 | 2026-08-20 | **Paso `A2` del plan de cierre**: **3** punto(s) abierto(s) **cerrados por lectura del árbol**, cada uno con **cita al artefacto que ya tenía la decisión**. Ninguno se cerró por criterio propio: por la pregunta previa de `Master-Prompt.md` §8.1, una respuesta que se sostiene con cita literal **es trabajo propio y no detención**. Los que remitían a un caso de uso **se verificaron abriendo el `CU`**, que era la condición que `Clasificacion-Pendientes-A1.md` §4 puso: una fila que dice «el `CU` lo adopta» **no prueba que el `CU` lo diga**. **Ningún enunciado de punto abierto se tocó** y ninguna decisión se inventó. Sube minor. | Orquestador SDD |
| 3.3 | 2026-08-20 | **Segunda pasada del paso `A2`**: **3** punto(s) abierto(s) cerrados **por lectura del árbol**, sobre las familias que `Audit/A3-Decisiones-Del-Product-Owner.md` §1 dejó verificadas. Cada uno cita **el archivo que ya tenía la decisión** — el motor de dibujo anclado en `three 0.169.0`, `PBKDF2` en `PasswordDerivation.cs`, el `@media` de 768 px en `app.css`, `EmailIdentity.Normalize`, los 18 puntos de acceso, las herramientas de cada stage en los guiones, y **la biblioteca de componentes, que no existe porque la etapa `b` decidió no introducirla** y su `.csproj` lo declara como apartamiento. **Ninguno se cerró por criterio propio** y **ningún enunciado de punto abierto se tocó**. Sube minor. | Orquestador SDD |
| 3.4 | 2026-08-27 | **Parches `P-01` y `P-06` de la mesa de evaluación del 2026-08-27** ([`../../../Audit/Mesa-2026-08-27.md`](../../../Audit/Mesa-2026-08-27.md)). **`PD-01` y `PD-03` pasan de «Cerrado» a «Cerrado en parte»** (hallazgo `H-01`, ancla **E2**, nivel **P1**, que es el `HM-01` del informe de migración 10.0 → 13.3 §7): las dos filas se cerraron «por lectura» el 2026-08-20 con una lista de herramientas que **no incluye ningún generador de inventario**, y su enunciado lo nombra. El generador sigue abierto como `PD-10` de `Supply-Chain-Seguridad.md` §2.b. **`PD-05`, la versión de plataforma del hosting, deja de estar vencida**: su evento pasa a la **fase `i`**. **Vencidos de este documento: de 1 a 0.** |
| 3.5 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **29 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 3.6 | 2026-08-31 | **Cierre de las dos incógnitas `[A VERIFICAR]` que ya no tenían pregunta**, sobre el inventario [`Inventario-Marcas-A-Verificar-2026-08-31.md`](../../../Audit/Inventario-Marcas-A-Verificar-2026-08-31.md), que clasificó las **71** apariciones vivas del corpus en **cinco** incógnitas. **(a) La versión de plataforma del hosting quedó RESUELTA el 2026-08-13, midiendo**: `PT-01.a` pasa con **200** y el hosting soporta `net10.0`, confirmado desde el panel; no hizo falta bajar la versión objetivo del front. **(b) La versión de la biblioteca de componentes queda SIN OBJETO**: la biblioteca nunca se introdujo y su ausencia es una decisión declarada en el `.csproj` — `PA-01` de `Web/05` §11 **ya lo había cerrado por lectura el 2026-08-20** y el desenlace no bajó. **Ninguna de las dos se decide acá: las dos se leen.** **`PD-05` deja de estar vigente** y la cláusula de la biblioteca de componentes sale de `PD-03`. El resto de `PD-03` **no se toca**: el generador del inventario sigue abierto como `PD-10`, con evento en la fase `i`. **Ningún umbral, ningún contrato y ninguna decisión cambian.** |
