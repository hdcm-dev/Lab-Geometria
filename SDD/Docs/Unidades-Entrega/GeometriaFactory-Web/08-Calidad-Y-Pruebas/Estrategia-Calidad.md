# Estrategia de calidad — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Estrategia-Calidad.md
**Versión:** 2.3
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. Definición de calidad para este proyecto de código

### 1.1 `GeometriaFactory-Web`

`GeometriaFactory-Web` tiene calidad cuando **las tres reglas de arquitectura del producto se sostienen desde acá y son verificables en un punto observable cada una**, cuando **lo construido no se aparta de la línea de base visual que el Product Owner aprobó**, y cuando **ninguna interrupción del servicio de datos ni del circuito deja una pantalla rota**.

Las tres partes no son intercambiables. La primera es la razón de ser de la topología entera: `RA-01` sólo se puede violar desde acá, porque éste es el único proyecto de código que sirve el navegador ([`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §10.4). La segunda tiene instrumento propio y ya emitido: [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), con sus **61** filas. La tercera es la única necesidad de negocio que este proyecto de código sostiene y ningún otro puede sostener del lado de la persona.

**Lo que esta definición deliberadamente no dice es «que las reglas de negocio se cumplan».** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5 declara que esta pieza **no hace cumplir ninguna**: ocultar un control, no armar una ruta o no ofrecer una acción **acotan lo que se ofrece y no hacen cumplir nada**. La consecuencia para esta categoría es directa y está en §3: toda acotación se verifica **forzando la solicitud sin pasar por la pantalla**, y no mirando la pantalla.

### 1.2 `GeometriaFactory-Visor`

`GeometriaFactory-Visor` tiene calidad cuando **las siete garantías de su contrato de fachada se sostienen sobre las seis funciones**, cuando **ninguna pieza deja de dibujarse sin quedar enumerada con su índice y su código**, y cuando el archivo de guion **no origina ni una sola petición de red** mientras los dos movimientos automáticos corren.

Las tres partes de esa definición tienen peso distinto y conviene decir por qué. La segunda es el problema original que el producto viene a cerrar: hoy, en la visualización previa, una figura simplemente no aparece y nadie se entera. La tercera es negativa por diseño —no hacer red— y es lo que hace **imposible** violar `RA-01` desde el navegador: la contribución de este proyecto de código a la seguridad del producto es una ausencia, y las ausencias se verifican con umbral cero y con la condición en que se miden.

**Este proyecto de código es el único del producto con `tiene_extensibilidad` true**, y su fachada es el punto de extensión declarado del producto (intake §18). Eso agrega una exigencia propia: la calidad incluye que **un reemplazo de la capa 3 se pueda evaluar sin backend**, con los ocho compromisos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §4. Su verificación vive en [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md).

## 2. Atributos de calidad priorizados

### 2.1 `GeometriaFactory-Web`

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. El valor rotulado **[ASUNCIÓN]** viene así desde el intake y **su forma no es un compromiso**: se usa como vigente hasta que el Product Owner lo confirme (§22 del intake, asunción `A-4`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica** | **0** apariciones de la credencial de sesión en el navegador, verificable con las herramientas de desarrollo (`05` §8; criterio de aceptación de la etapa `c`); **0** peticiones del navegador hacia el servicio de datos; **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **diecisiete** códigos vivos del contrato **y** sobre el camino de ausencia de respuesta |
| Adecuación funcional | **Crítica** | **100 %** de los pasos del guion de demostración de la etapa **y de todas las anteriores** [ASUNCIÓN del intake §17.2.P.6 · GeometriaFactory-Web en cuanto a expresarlo como puerta; la regla acumulativa es de la fuente]; **10 de 10** casos de uso con verificación |
| Fiabilidad | **Crítica** | **0** instancias del visor no liberadas tras **10** recorridos de ida y vuelta (`PT-02`); el estado degradado y la reconexión como **dos tramos** distintos; el listado vacío distinguido del fallo **por el tipo recibido y no por el conteo** (`RT-07`) |
| Usabilidad | **Alta** | **11 de 11** superficies, **73 de 73** componentes, **74 de 74** estados y **24 de 24** rutas de la línea de base visual aprobada, sensados por las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); contraste de **4.5:1** y recorrido completo por teclado en las once superficies |
| Compatibilidad | **Alta** | `PT-01` en sus **cuatro** partes, que el intake §17.2.P.10 · GeometriaFactory-Web declara como los NFR de este proyecto de código; capacidad gráfica tridimensional requerida **por capacidad y no por número de versión**, con el resto del producto disponible sin ella (`RT-11`) |
| Eficiencia de desempeño | **Media** | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viajando **una sola vez por trabajo**. **No hay umbral de tiempo de respuesta**, y esta categoría no lo inventa: ver §3 y `PA-04` de `05` §11 |
| Mantenibilidad | **Alta** | **1** sola salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta; **0** invocaciones al interior del bundle, con las **6** funciones de la fachada como única vía; **0** advertencias de construcción |
| Portabilidad | **Media** | Servidor: el hosting público, con su versión de plataforma **medida por `PT-01.a` el 2026-08-13: soporta `net10.0`** —la fuente la rotulaba `[A VERIFICAR]` y la marca quedó resuelta midiendo—. Navegador: cualquiera con capacidad gráfica tridimensional y circuito, persistente o replegado |

**El atributo que este proyecto de código no puede delegar es la seguridad de la topología.** No porque maneje secretos —la clave de firma es de `GeometriaFactory-Infrastructure`— sino porque es el único punto de contacto del navegador: si acá aparece una petición del navegador hacia el servicio de datos, la partición del producto deja de existir.

### 2.2 `GeometriaFactory-Visor`

Clasificación ISO/IEC 25010. Las **seis propiedades transversales** de `02` §6 son la fuente de los umbrales, y esta tabla **las toma como están y no las redefine**.

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica**, y negativa por diseño | **0 peticiones** originadas por el archivo de guion, medidas **con los dos movimientos prendidos**, que es su peor caso (`02` §6; intake §17.2.P.10 · GeometriaFactory-Visor). Es el NFR más importante del proyecto de código según el propio intake |
| Fiabilidad | **Crítica** | **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** piezas que desaparezcan sin registro (garantía `G-5`) |
| Adecuación funcional | **Crítica** | Los **seis** tipos dibujables, los **siete** códigos de condición y las **siete** garantías, sostenidos por las **seis** funciones |
| Eficiencia de desempeño | **Alta** | **10** recorridos de ida y vuelta entre trabajos sin degradación, medidos **con los dos movimientos prendidos** (`PT-02`) |
| Mantenibilidad | **Alta** | Superficie de exactamente **6** funciones, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos (`05` §8) |
| Compatibilidad | **Alta** | **0** dependencias traídas de una red de distribución externa en tiempo de ejecución (`PT-03`). Navegadores con capacidad gráfica tridimensional; sin ella el visor **no es soportado** y la fachada informa `GRAPHICS_CAPABILITY_MISSING` |
| Usabilidad | **Media, y ajena en su mayor parte** | La superficie visible la dibuja el componente anfitrión, que vive en `GeometriaFactory-Web`. Lo que este proyecto de código aporta es el equivalente accesible: la estructura del texto y la enumeración de piezas no dibujadas |
| Portabilidad | **Media** | Requisito declarado **por capacidad** y no por versión de navegador, porque la fuente no la fija (`05` §5 y §11 `PA-04`) |

**Sobre el atributo de eficiencia y su umbral ausente.** El intake §17.2.P.10 · GeometriaFactory-Visor declara «interacción fluida al rotar y acercar **con el mouse**, sin tráfico de circuito durante el gesto» y **no fija un valor numérico**. `05` §8 se niega explícitamente a inventarlo y lo deja como punto abierto `PA-03`. **Esta categoría tampoco lo inventa**: la fluidez se verifica de forma cualitativa declarada junto con `PT-02`, y el umbral numérico queda abierto. Ver §3.

## 3. Quality gates

### 3.1 `GeometriaFactory-Web`

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los tres primeros los declara el intake §17.2.P.8 · GeometriaFactory-Web; el cuarto, §17.2.P.6 · GeometriaFactory-Web; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8, con una fila por NFR. Las tres puertas técnicas van aparte, en §3.2.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | La construcción termina **sin advertencias** | Etapa de construcción del flujo de publicación | **Bloquea la fusión** (intake §17.2.P.8 · GeometriaFactory-Web) |
| QG-02 | El bundle del visor se genera **en el mismo flujo de publicación**, nunca se toma de un artefacto viejo | Inspección del flujo: el paso de generación precede al de publicación y no hay artefacto cacheado | Bloquea la publicación (intake §17.2.P.8 · GeometriaFactory-Web) |
| QG-03 | El flujo **no termina en la subida**: termina comprobando que la dirección pública responde | Comprobación al final del flujo de publicación | **Bloquea el flujo.** El intake §17.2.P.8 · GeometriaFactory-Web declara que «una subida **por FTP** que deja la aplicación caída y se reporta como exitosa es peor que una falla visible» |
| QG-04 | **100 %** de los pasos del guion de demostración de la etapa **y de todas las anteriores** se ejecutan y pasan antes del punto de control **[ASUNCIÓN del intake §17.2.P.6 · GeometriaFactory-Web en cuanto a expresarlo como gate; sobre la forma, no sobre el carácter]** | Ejecución del guion en el navegador del equipo anfitrión (`TC-10035`) | **Bloquea el punto de control, y no es condicionado.** Lo sujeto a confirmación es **la forma**, ver §3.1 |
| QG-05 | **0** peticiones del navegador hacia el servicio de datos, contadas durante un recorrido completo **con los dos movimientos automáticos prendidos** | `TC-10029`, conteo en la pestaña de red | Bloquea la fusión. Es `RA-01`, la regla que sostiene la topología |
| QG-06 | **1** sola salida hacia el servicio de datos —el cliente tipado— y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta | `TC-10030`, inspección del árbol de fuentes y de las dependencias de guion | Bloquea la fusión |
| QG-07 | **0** apariciones de la credencial de sesión en el navegador | `TC-10003`, inspección del almacenamiento, de las marcas de sesión y del contenido servido | Bloquea la fusión. Es criterio de aceptación de la etapa `c` |
| QG-08 | **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **diecisiete** códigos vivos **y** sobre el camino de ausencia de respuesta | `TC-10031`, inspección del traductor de condiciones, que es el único lugar por el que un mensaje llega a la persona | Bloquea la fusión. Es `RA-03` |
| QG-09 | **0** invocaciones al interior del bundle: las **6** funciones de la fachada son la única vía y hay **0** accesos al elemento de dibujo fuera del anfitrión | `TC-10032`, inspección del árbol de fuentes | Bloquea la fusión. Es `RA-02` sostenida desde este lado |
| QG-10 | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja **una sola vez por trabajo** | `TC-10033`, conteo en la pestaña de red mientras se rota y se acerca | Bloquea la fusión |
| QG-11 | Las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, con estado y fecha, y **ninguna deriva mayor queda sin resolver** | Recorrido de la matriz al cerrar la etapa | Bloquea el cierre de la etapa. Una deriva mayor se resuelve corrigiendo lo construido o actualizando la línea de base con aprobación humana, **nunca por omisión** |

**Once gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8. **No se declara ningún gate de cobertura de líneas**, y el motivo lo da la fuente: este proyecto de código **no tiene proyecto de pruebas propio** en el árbol del repositorio (intake §17.2.P.6 · GeometriaFactory-Web). Inventarle un umbral de cobertura sería inventar una medición sin sujeto.

**Tampoco se declara ningún gate de tiempo de respuesta.** `05` §8 lo declara expresamente: las tolerancias de **400 ms** de [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md) §7 son de **diseño de la espera** —dicen a partir de cuándo se muestra un indicador— y no compromisos de tiempo de respuesta. Esta categoría hereda esa distinción y no la convierte en umbral. Queda como `PA-04` de `05` §11.

### 3.1 Ningún gate de este proyecto de código queda condicionado

**Los once gates de §3 bloquean** —cada uno lo que su columna de consecuencia declara: la fusión, la publicación, el flujo, el punto de control o el cierre de la etapa— **y ninguno es condicionado**. El único que lleva un valor rotulado **[ASUNCIÓN]** es `QG-04`, y **no por eso queda condicionado**.

El intake §17.2.P.6 · GeometriaFactory-Web lo escribe así: **«Gate bloqueante y numérico en lugar de cobertura de líneas: el 100 % de los pasos del guion de demostración de la etapa y de todas las anteriores se ejecuta y pasa antes del punto de control»**, con el rótulo **[ASUNCIÓN en cuanto a expresarlo como gate; la regla acumulativa es de RF §9.4]**. Y el intake §22, fila `A-4`, columna «Si el Product Owner la cambia», dice: **«Cambia la forma del gate, no su carácter bloqueante»**.

Las dos fuentes dicen lo mismo y dicen exactamente qué está en duda: **cómo se expresa la puerta**, no si detiene. **La regla acumulativa es de la fuente y no está en duda**, y el carácter bloqueante tampoco. Condicionar `QG-04` habría suspendido justamente lo que la fuente puso a salvo, en el único proyecto de código del producto **sin batería automatizada propia**: sería la diferencia entre que el guion acumulativo detenga un punto de control o no lo detenga.

**Qué se hace con la asunción, entonces.** El valor y la forma se usan como vigentes y **la puerta se materializa en `09-Devops` como bloqueante desde la primera etapa que la alcanza**. Si el Product Owner cambia la forma —otro umbral, otro instrumento de medición—, cambia la condición que se mide y el gate **sigue bloqueando**. En particular, nada de esto habilita a ejecutar el guion de la etapa sin los de las anteriores: eso es la regla de no-regresión del intake §15 y de RF §9.4, que no es asunción de nadie.

### 3.2 Las tres puertas técnicas que alcanzan a este proyecto de código

Se declaran aparte de los gates porque su consecuencia es distinta: el intake §15 declara que **una puerta que no pasa detiene la planificación de las etapas que dependen de ella y no se arrastra como deuda**.

| Puerta | Qué mide | Dónde se mide | Qué condiciona |
| --- | --- | --- | --- |
| `PT-01`, en sus **cuatro** partes | Arranque en la dirección pública, transporte del circuito, estabilidad del proceso durante **20 minutos** y salida hacia el servicio de datos | Etapa `a`, **antes que cualquier otra cosa** | El modelo de front entero. **Sólo el rojo en el transporte o la falla de estabilidad obligan a cambiarlo**; un repliegue de mayor latencia **no es motivo de rediseño** |
| `PT-02` | Que el visor funcione embebido: el bundle **carga en una página del anfitrión**, la escena se crea, las tres figuras de `E-1` se dibujan —ortoedro incluido—, **navegar y volver 10 veces no degrada** —**0** instancias no liberadas, medidas **con los dos movimientos prendidos**— y **el árbol y la escena se sincronizan por índice** (intake §17.2.P.8 · GeometriaFactory-Visor) | Antes de comprometer la etapa `g` | La etapa `g` entera |
| `PT-03` | Que el **motor de dibujo quede dentro del bundle** y la página **funcione sin acceso a CDN externos** (intake §17.2.P.8 · GeometriaFactory-Visor) | Antes de comprometer la etapa `g` | La etapa `g` entera. **No tiene caso de verificación propio acá**: es propiedad del bundle y se verifica del lado de `GeometriaFactory-Visor` |

**Los umbrales de las tres puertas no son asunciones**, y el intake §22 lo declara expresamente: los 20 minutos de `PT-01.c`, el semáforo de `PT-01.b` y los umbrales de las cinco puertas técnicas «están declarados en las fuentes y se transcriben sin cambio». Esta categoría los transcribe y no los mueve.

### 3.2 `GeometriaFactory-Visor`

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El bundle **se genera sin errores** | Etapa de empaquetado del pipeline (intake §17.2.P.8 · GeometriaFactory-Visor) | Bloquea la fusión |
| QG-02 | **`PT-03`**: el motor de dibujo tridimensional queda **dentro** del bundle y la página funciona **sin acceso a redes de distribución externas**; **0** dependencias traídas de una red externa en tiempo de ejecución | `TC-12019`, sobre el bundle generado | **Bloqueante, y detiene la planificación de la etapa `g`**. Ver §3.1 |
| QG-03 | **`PT-02`**: el bundle carga en una página del anfitrión, la creación de instancia arma la escena, la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**, **diez** recorridos de ida y vuelta no degradan, y el árbol y la escena **se sincronizan por índice** | `TC-12020`, con los recorridos medidos **con los dos movimientos prendidos** | **Bloqueante, y detiene la planificación de la etapa `g`**. Ver §3.1 |
| QG-04 | **Cero red**: exactamente **0** peticiones originadas por el archivo de guion, y **0** ocurrencias de las tres formas de petición en el código fuente **y en el bundle generado** | `TC-12016` y `TC-12018`, con la medición **con los dos movimientos prendidos y sostenidos** | Bloqueante, sin gradación. Es `RA-02`, y a través de ella `RA-01` |
| QG-05 | **Cero persistencia**: **0** claves escritas en el almacenamiento del navegador y ningún estado conservado entre páginas | `TC-12017` | Bloqueante, sin gradación |
| QG-06 | Superficie del bundle: exactamente **6** funciones expuestas, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos | `TC-12018` | Bloqueante |
| QG-07 | **Ausencia de fallo silencioso**: **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** sin registro | `TC-12006` | Bloqueante, sin gradación. Es la garantía `G-5` |
| QG-08 | Los códigos de condición son exactamente **siete** y **ninguno se acuña aguas abajo**; un curso nuevo se agrega como fila de curso y no como código | `TC-12021`, contra §6 del contrato de fachada | Se rechaza en revisión |
| QG-09 | El bundle **nunca se edita a mano**: es un artefacto generado y reproducible | Revisión del pull request de la etapa (intake §17.2.P.7 · GeometriaFactory-Visor) | Se rechaza en revisión |

**No hay gate de cobertura de líneas**, y su ausencia está declarada aguas arriba: el intake §17.2.P.6 · GeometriaFactory-Visor fija como gate «verificable por inspección, **en lugar de cobertura de líneas**» la ausencia de las tres formas de petición de red —que el intake nombra una por una, y que este documento describe en vez de nombrar, fuera de las comillas—. `QG-12004` es ese gate.

**No hay gate de fluidez numérica**, por lo declarado en §2.

### 3.1 Las dos puertas técnicas son vinculantes

`PT-02` y `PT-03` **no son criterios de esta categoría**: las declara el intake §15 y §17.2.P.8 · GeometriaFactory-Visor, y el roadmap §2.2 las ubica **antes de comprometer la etapa `g`**. Su carácter vinculante tiene una consecuencia que este documento hereda y no relaja:

**Una puerta que no pasa detiene la planificación de la etapa `g` y no se arrastra como deuda.** Es el mismo fundamento por el que el Product Owner promovió la capacidad `F-13` a `Must Have` en el intake **1.19**: una capacidad citada por una puerta técnica deja de ser diferible. Esta categoría **no puede convertir `PT-02` ni `PT-03` en gates condicionados**, ni cambiar lo que miden, ni agregarles criterios.

Lo que sí hace esta categoría es **declarar con qué caso de prueba se mide cada una** —`TC-12019` y `TC-12020`— y **con qué condiciones**: los diez recorridos, con los dos movimientos prendidos, porque un bucle de dibujo que sobreviviera a la destrucción es exactamente la degradación que la puerta tiene que descartar y con los movimientos apagados no se ejercitaría.

## 4. Roles de calidad dentro del equipo

### 4.1 `GeometriaFactory-Web`

`equipo_n` es **1** (intake §2): la misma persona diseña las verificaciones, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de verificación, mantener la matriz de cobertura, **resolver el método de verificación de las 61 filas de la matriz de sensado** y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar el valor rotulado [ASUNCIÓN] y **decidir ante toda deriva mayor**: se corrige lo construido o se actualiza la línea de base |
| Revisión mecánica | El flujo de publicación | Los gates `QG-10001`, `QG-10002`, `QG-10003` y las mediciones automatizables de §3 |
| Verificación observada | La persona, en el navegador del equipo anfitrión | El guion de demostración y las filas de la matriz de sensado cuyo método es inspección visual. **No todo acá se automatiza, y decirlo es más honesto que declarar una automatización que no existe** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

### 4.2 `GeometriaFactory-Visor`

`equipo_n` es **1** (intake §2).

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Los casos de prueba, la matriz de cobertura, la matriz de sensado de deriva, la guía de testing de extensibilidad y la DoD |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | El OK del punto de control, y el umbral de fluidez si alguna vez lo fija (`PA-03`) |
| Medición mecánica | El pipeline y el navegador | `QG-12001` lo da el pipeline; `QG-12002` a `QG-12007` se miden sobre el bundle generado y sobre una página, no sobre el código fuente solamente |

**En este proyecto de código el filtro más duro no es la revisión: son las dos puertas medidas sobre el artefacto generado.** Lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §4 y esta estrategia lo adopta: no dependen de que alguien las revise, se miden.

## 5. Cadencia de revisión

### 5.1 `GeometriaFactory-Web`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) y qué filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) entran en alcance | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera y **las filas de la matriz de sensado que la etapa tocó**, con estado y fecha | Matriz actualizada, filas sensadas y la constancia de los gates medidos |
| Antes de comprometer la etapa `g` | `PT-02` y `PT-03` | La medición de las dos puertas, o la detención de la planificación de `g` |
| Ante toda deriva mayor | Si se corrige lo construido o se actualiza la línea de base | La decisión del Product Owner, con constancia escrita. **Nunca se resuelve por omisión** |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

**Una precisión sobre la matriz de sensado.** Su §1 declara como cuarto momento el «cierre de cada sprint de codificación». Este documento lo lee como **cierre de cada etapa**, que es la unidad que el producto tiene, y no cambia el texto de la matriz: la palabra «sprint» de ese documento es de la mecánica genérica de `Deriva-Rules.md`, no una unidad de planificación de este producto.

### 5.2 `GeometriaFactory-Visor`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de la etapa `a` | Que la cadena de construcción sea reproducible y produzca un archivo **vacío pero real** | `BT-12001` cerrada |
| **Antes de comprometer la etapa `g`** | `PT-02` y `PT-03` enteras | Las dos puertas medidas, o la etapa `g` sin comprometer |
| Al cerrar la etapa `g` | Las **seis** propiedades transversales con sus condiciones de medición, las **siete** garantías y los **siete** códigos | Matriz de cobertura actualizada y matriz de sensado de deriva con su estado |
| **Ante todo cambio del bundle** | `QG-12004`, `QG-12005` y `QG-12006`, sobre el **bundle generado** y no sólo sobre la fuente | La constancia de la medición en el pull request |
| Ante toda propuesta de función nueva en la fachada | Los seis pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 | La especificación en 02, o el rechazo con su motivo |

**La revisión sobre el bundle generado y no sólo sobre la fuente es propia de este proyecto de código.** Una dependencia que hace una petición por dentro no aparece en el código fuente y sí en el bundle; `05` §9 declara esa causa como de probabilidad **media**, más alta que la de escribir la petición a mano.

**No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.3 | 2026-08-31 | **Cierre de las dos incógnitas `[A VERIFICAR]` que ya no tenían pregunta**, sobre el inventario [`Inventario-Marcas-A-Verificar-2026-08-31.md`](../../../Audit/Inventario-Marcas-A-Verificar-2026-08-31.md), que clasificó las **71** apariciones vivas del corpus en **cinco** incógnitas. **(a) La versión de plataforma del hosting quedó RESUELTA el 2026-08-13, midiendo**: `PT-01.a` pasa con **200** y el hosting soporta `net10.0`, confirmado desde el panel; no hizo falta bajar la versión objetivo del front. **(b) La versión de la biblioteca de componentes queda SIN OBJETO**: la biblioteca nunca se introdujo y su ausencia es una decisión declarada en el `.csproj` — `PA-01` de `Web/05` §11 **ya lo había cerrado por lectura el 2026-08-20** y el desenlace no bajó. **Ninguna de las dos se decide acá: las dos se leen.**  **Ningún umbral, ningún contrato y ninguna decisión cambian.** |
| 2.2 | 2026-08-29 | **Tramo `R-3d` del renombre `F-03`, que lo cierra.** **1 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni **la prosa que narra el renombre** —una línea que trae la forma vieja y su par vigente está reportando, no usando—. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **4 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
