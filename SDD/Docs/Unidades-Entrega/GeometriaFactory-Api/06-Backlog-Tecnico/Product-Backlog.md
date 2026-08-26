# Product backlog — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Product-Backlog.md
**Versión:** 4.3
**Estado:** Propuesto
**Fecha:** 2026-08-26
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las seis secciones son comunes.** Unión de catálogo: las `US` de las cuatro capas, con su rango propio.

---

## 1. Objetivos del producto

### 1.1 `GeometriaFactory-Api`

Este backlog convierte en trabajo planificable los **doce** contratos de uso de `GeometriaFactory-Api`, el **proyecto de código principal** del producto: los **quince** puntos de acceso de su superficie, la guardia que los admite, las **dos** traducciones, la composición de raíz, el arranque y la colección de peticiones reproducible.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **seis** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una.

### 1.1 Qué significa ser el proyecto de código principal para este backlog

`02` §1 lo declara: es el **nivel 3**, el último del orden topológico, y **el único de los siete que ensambla a los demás**. Cuatro consecuencias operativas:

1. **Ninguna historia se puede cerrar antes que las tres capas que ensambla.** Dentro de cada etapa, el trabajo de este proyecto de código va **último**: un caso de uso que no exista en la capa de aplicación es un punto de acceso que acá no se puede exponer.
2. **Su verificación está invertida a propósito.** La pirámide declarada es **60 % de integración y 40 % unitarias**, porque **lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo** (`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Api).
3. **Es la única puerta.** Un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio: **todo lo que este proyecto de código no exponga, no existe para nadie de afuera**.
4. **Es donde dos reglas de negocio se rompen hacia afuera sin que ninguna capa de adentro se entere**: `RN-00003`, eligiendo un código de respuesta que confirma la existencia de un recurso ajeno, y `RN-00013`, dejando un punto de acceso fuera de la guardia. Por eso dos tareas técnicas de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) son inspecciones **en las dos direcciones** y no funcionalidades.

### 1.2 Qué es una historia en la frontera del proceso

- **El rol de las treinta historias es el mismo**: el **código de `GeometriaFactory-Web`, servidor a servidor**. El alumno y el administrador aparecen como sujetos de las reglas y nunca como actores, porque **el navegador nunca alcanza esta superficie** (`RA-01`).
- **Ninguna historia decide qué se dice.** `02` §4 lo enuncia en una línea: **esta capa decide cómo se dice, y no decide qué se dice**. Una historia que decidiera un estado, una admisibilidad o qué campos cruzan la frontera estaría mal ubicada.
- **Ninguna historia acuña un código del contrato.** Los códigos son los **diecisiete vivos** del conjunto cerrado de `GeometriaFactory-Contracts` —sobre **veinte** identificadores emitidos, tres retirados y **ninguno reciclado**—, y esta capa **no agrega, no renombra y no traduce a texto** ninguno.
- **Tres ausencias son declaradas y no olvidos**: **no hay intercambio de origen cruzado**, **no hay canal bidireccional** y **no hay ningún punto de acceso pensado para que lo invoque un navegador**. Las tres salen de `RA-01`.
- **Y hay una historia que no implementa nada: demuestra.** US-00030 es la colección de peticiones reproducible, que `PRODUCT-INTAKE` §16.1 declara como la forma de demostración de este tipo de proyecto de código.

### 1.2 `GeometriaFactory-Domain`

Este backlog convierte en trabajo planificable los **trece** contratos de uso y las **dieciséis** reglas que `GeometriaFactory-Domain` declara, sin agregar alcance y sin reordenar el plan de etapas. Su propósito es que, en cualquier momento, se pueda responder qué parte del dominio ya está construida y de qué etapa del producto depende esa parte.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido del producto —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance que el intake declara, **8 de 8 etapas** (§22, asunción `A-2`). Una historia de este backlog está en el MVP si la etapa que la contiene está entre esas ocho; ninguna otra prueba de pertenencia se aplica.

**Este backlog no reordena las etapas ni las renombra.** Las seis épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una. Formalizarlas es lo que ese documento pide de la categoría 06; inventar una agrupación distinta habría creado una segunda fuente de verdad sobre el orden de construcción.

### 1.1 Qué significa nivel topológico 0 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Domain` en el **nivel 0** del orden topológico de construcción, junto con `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`. Tres consecuencias operativas, y ninguna de ellas es una licencia para adelantar alcance:

1. **Ninguna historia ni ninguna tarea de este backlog espera a otro proyecto de código.** El proyecto de código no referencia a ninguno (`05` §2 propiedad 1), de modo que su trabajo puede empezar apenas la etapa `a` deja el esqueleto en pie.
2. **Su trabajo condiciona el de los niveles 1 a 3.** `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure` compilan contra esta biblioteca, de modo que una guarda que acá no exista es una guarda que allá no se puede invocar. Dentro de cada etapa, lo de este backlog va primero.
3. **El orden topológico no cambia el orden de las etapas.** Las etapas son estrictamente secuenciales y sin paralelismo (`Roadmap-Producto.md` §4). Que este proyecto de código pueda arrancar primero significa que arranca primero **dentro** de la etapa vigente, no que pueda construir la etapa `e` mientras la `c` sigue abierta.

### 1.3 `GeometriaFactory-Application`

Este backlog convierte en trabajo planificable los **once** contratos de uso de `GeometriaFactory-Application`, la capa que contiene los casos de uso del producto y los **cuatro** puertos que la infraestructura implementa. Su propósito es que en cualquier momento se pueda responder qué parte de la orquestación ya está construida y de qué etapa del producto depende esa parte.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido del producto —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance que el intake declara, **8 de 8 etapas** (§22, asunción `A-2`). Una historia de este backlog está en el MVP si la etapa que la contiene está entre esas ocho; ninguna otra prueba de pertenencia se aplica. **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **seis** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una. Formalizarlas es lo que ese documento pide de la categoría 06.

### 1.1 Qué significa nivel topológico 1 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Application` en el **nivel 1**, con una sola dependencia saliente: `GeometriaFactory-Domain`. Tres consecuencias operativas:

1. **Ninguna historia de este backlog se puede cerrar antes que la guarda de dominio que invoca.** Dentro de cada etapa, el trabajo de `GeometriaFactory-Domain` va primero: una guarda que allá no exista es una guarda que acá no se puede invocar.
2. **Su trabajo condiciona el de los niveles 2 y 3.** `GeometriaFactory-Infrastructure` implementa los cuatro puertos que esta capa declara y `GeometriaFactory-Api` los conecta; un puerto que acá no esté declarado no se puede implementar ni conectar.
3. **Ninguna historia espera a la infraestructura para poder verificarse.** El estilo de la capa está elegido precisamente para que un caso de uso entero se pueda ejercer con dobles de los cuatro puertos, **sin base de datos y sin frontera de proceso** (`05` §8, NFR de cero pruebas que tocan la base real). Eso hace que las historias de este backlog sean verificables dentro de su etapa aunque el adaptador correspondiente todavía no exista.

### 1.2 Qué es una historia en una capa de casos de uso

`GeometriaFactory-Application` no tiene pantallas y no atiende peticiones. En consecuencia:

- **El rol de las treinta y dos historias es el mismo**: el **código consumidor de la biblioteca**, que en el producto es `GeometriaFactory-Api` a través de su composición de raíz. El alumno y el administrador aparecen como **sujetos de las reglas** y nunca como actores, tal como lo declara [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §1.
- **Buena parte del valor de esta capa es una negativa bien dada.** Diez de las treinta y dos historias entregan un rechazo con su motivo, no un efecto: es lo que hace que el consumidor pueda distinguir por qué no pudo hacer algo. Sus criterios se expresan sobre el **motivo emitido** y sobre el estado que **no** cambió.
- **Ninguna historia acuña un código de condición.** Las condiciones son **36**, su fuente es [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), y `05` §8 exige la cobertura del catálogo **en las dos direcciones**: cien por ciento alcanzadas por una prueba y cero emitidas fuera del catálogo.
- **Ninguna historia enuncia una regla ni un invariante.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; acá se citan por identificador y se declara qué tramo ejerce esta capa, que es lo que `02` §6 y `05` §10.2 ya repartieron.

### 1.4 `GeometriaFactory-Infrastructure`

Este backlog convierte en trabajo planificable los **diez** contratos de uso de `GeometriaFactory-Infrastructure`, la capa donde el producto **toca el mundo**: los **cuatro** puertos que implementa, los **dos** mecanismos de seguridad que las capas de adentro delegaron y la responsabilidad de dejar el almacén en condiciones antes de la primera petición.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **cinco** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una.

### 1.1 Qué significa nivel topológico 2 para este backlog

`02` §1 ubica a este proyecto de código en el **nivel 2**: depende de `GeometriaFactory-Application` y de `GeometriaFactory-Domain`, y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Tres consecuencias operativas:

1. **Ninguna historia se puede cerrar antes que el puerto que implementa esté declarado.** Dentro de cada etapa, el trabajo de las dos capas de adentro va primero: un puerto que allá no exista es un adaptador que acá no se puede escribir.
2. **Este proyecto de código no registra sus propios adaptadores.** Los declara y `GeometriaFactory-Api` los conecta; un registro automático desde acá haría que la frontera dejara de ser contable (`05` §3.2 punto 4).
3. **La mitad de este backlog no espera a nada.** Los dos motores del validador, el reloj y el mecanismo de credenciales **no tocan el almacén ni hacen red** (`05` §2 propiedad 2), de modo que se pueden construir y probar unitariamente sin base, sin frontera de proceso y sin ningún otro proyecto de código en pie.

### 1.2 Qué es una historia en la capa que toca el mundo

- **El rol de las veinticinco historias es el mismo**: el **código consumidor de la biblioteca**, que en el producto es la composición de raíz de `GeometriaFactory-Api` y, a través de ella, los casos de uso que la necesitan. El alumno y el administrador aparecen como sujetos de las reglas y nunca como actores.
- **Acá está el riesgo del producto, y el backlog tiene que reflejarlo.** `02` §1 declara que el intake asigna probabilidad **alta** e impacto **alto** a que **el validador se escriba sin leer el análisis**, porque el texto del alumno no es texto estrictamente válido. Es el **único** riesgo de negocio cuya mitigación declarada es una batería de pruebas, y esa batería vive acá: es la épica EP-06005 entera.
- **Ninguna historia toma una decisión de negocio.** `02` §4 lo enuncia en una línea: **esta capa provee el mecanismo y no toma ninguna decisión de negocio**. Una historia que decidiera un estado, una autorización o una admisibilidad estaría mal ubicada.
- **Ninguna historia acuña una condición.** Las condiciones son **17**, su fuente es [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), **ninguna es un código de protocolo** y su traducción pertenece a `GeometriaFactory-Api`.
- **Varias historias entregan una terminación y no un efecto**, y es deliberado: cuando un mecanismo no puede cumplir su promesa, **se detiene y lo dice**; no la cumple a medias, no compone un valor por otro medio y no cae hacia un sustituto (`05` §2 propiedad 4).

## 2. Épicas

### 2.1 `GeometriaFactory-Api`

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-00001 | Esqueleto ambulante y verificación de viabilidad | `a` | La composición de raíz conecta los cuatro puertos, el arranque prepara el almacén en dos fases y el punto de salud responde sin exigir acceso. **`PT-04` se mide acá**, y se verifica que **la sesión interactiva del front no llega hasta acá** | US-00026, US-00027, US-00028, US-00029 | BT-00001 a BT-00006 |
| EP-00002 | Identidad del administrador y sesión | `c` | El canje de credenciales, la guardia de admisión sobre los once puntos que exigen acceso, los puntos de alta y de credencial propia, y las **dos** traducciones con su tabla única | US-00001, US-00002, US-00003, US-00004, US-00005, US-00008, US-00010, US-00024, US-00025 | BT-00007 a BT-00016 |
| EP-00003 | Ciclo de vida de la cuenta de alumno | `d` | El gobierno de la comisión, el reseteo que devuelve la provisoria **una sola vez y no la registra**, y la guardia del cambio de contraseña pendiente sobre todos los puntos salvo uno | US-00006, US-00007, US-00009, US-00011, US-00012, US-00013, US-00014, US-00015, US-00016 | BT-00011, BT-00012, BT-00017 |
| EP-00004 | Gestión del trabajo | `e` | Los cinco puntos sobre trabajos: el texto que **no se normaliza en el borde**, la eliminación con sus dos alcances, el listado sin parámetro para pedir borradores y el detalle | US-00019, US-00020, US-00021, US-00022 | BT-00018, BT-00023, BT-00024 |
| EP-00005 | Interpretación y verificación del dato del alumno | `f` | El envío y el reenvío, que **responden con éxito** transportando el estado que la interpretación decidió | US-00017, US-00018 | BT-00018, BT-00022 |
| EP-00006 | Desenlace de la entrega | `h` | El punto de desenlace con su terminalidad, y la colección de peticiones reproducible, que incluye la aprobación y el rechazo | US-00023, US-00030 | BT-00019, BT-00020, BT-00021 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La `b` construye la cáscara del front y no agrega ningún punto de acceso. La `g` integra la visualización y el árbol, y **todo lo que esa etapa necesita de esta superficie ya está expuesto en la `e`**: el punto de detalle devuelve piezas, componentes y texto original desde entonces, y el dibujo ocurre del otro lado de la frontera, en el navegador. Agregar una épica en `g` habría creado trabajo que no existe.

### 2.2 `GeometriaFactory-Domain`

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-02001 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, compila sin dependencias salientes y sus decisiones abiertas de nombre y de herramienta quedan cerradas en el punto de control | Ninguna: la etapa `a` no tiene capacidad funcional asociada (`Roadmap-Producto.md` §2.1) | BT-02001 a BT-02005 |
| EP-02002 | Identidad del administrador y sesión | `c` | La cuenta de administrador se constituye en el primer arranque y la admisibilidad y el cambio de credencial quedan resueltos como contrato de uso | US-02007, US-02008, US-02024, US-02025 | BT-02006, BT-02007, BT-02010, BT-02011 |
| EP-02003 | Ciclo de vida de la cuenta de alumno | `d` | Alta, ciclo de vida, credencial provisoria, reseteo y marca de cambio pendiente | US-02001 a US-02006, US-02026, US-02027 | BT-02009, BT-02010, BT-02011, BT-02016 |
| EP-02004 | Gestión del trabajo | `e` | El trabajo se constituye con dueño e identidad propia, y quedan resueltos el acceso del alumno y el alcance del administrador | US-02009, US-02010, US-02018, US-02019, US-02022 | BT-02006, BT-02012 |
| EP-02005 | Interpretación y verificación del dato del alumno | `f` | El conjunto de piezas y las observaciones se adoptan, y el envío resuelve entre `Borrador` y estado `Pendiente` | US-02011 a US-02017 | BT-02008, BT-02012, BT-02013 |
| EP-02006 | Desenlace de la entrega | `h` | Aprobar y rechazar desde el estado `Pendiente`, con terminalidad, y la eliminación por el administrador | US-02020, US-02021, US-02023 | BT-02012, BT-02014 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La etapa `b` construye la cáscara del front y la `g` la visualización y el árbol; ninguna de las dos toca entidades, invariantes ni transiciones. Lo que este proyecto de código aporta a la visualización —la identidad posicional de la pieza— se construye en la etapa `f`, con US-02011, porque es parte de la adopción del conjunto de piezas y no del dibujo (`02` §5.2, cobertura parcial de NB-00006).

### 2.3 `GeometriaFactory-Application`

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-04001 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, compila con una sola dependencia saliente y sus decisiones abiertas de nombre —incluido el del cuarto puerto— quedan cerradas en el punto de control | Ninguna: la etapa `a` no tiene capacidad funcional asociada (`Roadmap-Producto.md` §2.1) | BT-04001 a BT-04006 |
| EP-04002 | Identidad del administrador y sesión | `c` | El segundo camino de alta, la consulta de admisibilidad con su motivo y el reemplazo de la credencial por la propia cuenta | US-04003, US-04007, US-04009, US-04028 | BT-04007, BT-04008, BT-04010, BT-04012, BT-04014 |
| EP-04003 | Ciclo de vida de la cuenta de alumno | `d` | Auto-registro, las cuatro operaciones de admisión, la credencial provisoria, el reseteo y la marca de cambio pendiente con su comprobación transversal | US-04001, US-04002, US-04004, US-04005, US-04006, US-04008, US-04029, US-04030, US-04031, US-04032 | BT-04010, BT-04011, BT-04012, BT-04013, BT-04014, BT-04021 |
| EP-04004 | Gestión del trabajo | `e` | El trabajo se constituye y se reedita con su texto íntegro, y las dos consultas quedan resueltas con su predicado de alcance ya aplicado | US-04010, US-04011, US-04012, US-04017, US-04019, US-04020, US-04021, US-04022, US-04026 | BT-04009, BT-04015, BT-04016 |
| EP-04005 | Interpretación y verificación del dato del alumno | `f` | El envío interpreta por el puerto y deja que el dominio resuelva el estado, con la terminación controlada cuando la interpretación no está disponible | US-04013, US-04014, US-04015, US-04016 | BT-04015, BT-04019 |
| EP-04006 | Desenlace de la entrega | `h` | Aprobar y rechazar desde el estado `Pendiente`, la eliminación por el administrador y la lectura del desenlace por el alumno | US-04018, US-04023, US-04024, US-04025, US-04027 | BT-04015, BT-04017 |

**Las etapas `b` y `g` no producen épica en este proyecto de código, y es declaración y no olvido.** La etapa `b` construye la cáscara del front y la `g` la visualización y el árbol; ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización. Lo que esta capa aporta a la visualización —la entrega de las piezas con su identidad posicional y sus componentes en el detalle— se construye en la etapa `e` con US-04019, porque es la forma del resultado de la consulta y no el dibujo (`02` §7.2, cobertura parcial de NB-00006).

### 2.4 `GeometriaFactory-Infrastructure`

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-06001 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, el almacén se crea y se transforma al arrancar, y el arranque se detiene antes que operar sobre un almacén en el que no se puede confiar. **`PT-04` se mide acá** | US-06024, US-06025 | BT-06001 a BT-06008 |
| EP-06002 | Identidad del administrador y sesión | `c` | El almacén sostiene la unicidad, responde las dos preguntas sobre el conjunto, deriva y verifica credenciales, y emite el acceso firmado con la clave que recibe y no busca | US-06014, US-06015, US-06017, US-06018, US-06021, US-06022, US-06023 | BT-06005, BT-06009, BT-06012, BT-06013, BT-06015, BT-06021 |
| EP-06003 | Ciclo de vida de la cuenta de alumno | `d` | La provisoria que el sistema produce, la marca que viaja sin ser un estado de cuenta, y el arrastre de la baja como única operación destructiva | US-06013, US-06016, US-06019, US-06020 | BT-06009, BT-06011, BT-06014, BT-06025 |
| EP-06004 | Gestión del trabajo | `e` | El trabajo se materializa con su texto literal, la consulta se resuelve con el recorte ya trasladado y el retiro es físico y todo o nada | US-06008, US-06009, US-06010, US-06011, US-06012 | BT-06005, BT-06010, BT-06011 |
| EP-06005 | Interpretación y verificación del dato del alumno | `f` | El validador de figuras: lectura tolerante con las **cuatro** trampas, derivación por tipo, tolerancia de **0.01** con operador estricto y la batería de **10** casos sobre los **ocho** escenarios | US-06001 a US-06007 | BT-06016 a BT-06020, BT-06024 |

**Las etapas `b`, `g` y `h` no producen épica en este proyecto de código, y es declaración y no olvido.** La `b` construye la cáscara del front y la `g` la visualización; ninguna de las dos toca el almacén, los motores ni los mecanismos. La `h` es el circuito de revisión, y lo que esta capa aporta a él —guardar el estado terminal y el comentario del administrador— **ya está construido en la etapa `e`**: el comentario es **campo y no entidad, y sin historial** (`RC-06007`), y la columna existe desde la transformación inicial del esquema. Agregar una épica en `h` habría creado trabajo que no existe.

## 3. Historias por épica

### 3.1 `GeometriaFactory-Api`

Las **treinta** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó —`US-00001` a `US-00030`, sin huecos—, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-00001 · Esqueleto ambulante y verificación de viabilidad

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00026](historias-usuario/US-00026-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion.md) | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | Must | **No aplica** (§4.1) | Propuesta | CU-00010 | EP-00001 |
| [US-00027](historias-usuario/US-00027-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md) | Aplicar las transformaciones de esquema al arrancar, sobre almacén inexistente | Must | **No aplica** (§4.1) | Propuesta | CU-00011 | EP-00001 |
| [US-00028](historias-usuario/US-00028-Detener-El-Arranque-En-Lugar-De-Atender-Sobre-Un-Almacen-Dudoso.md) | Detener el arranque en lugar de atender peticiones sobre un almacén en el que no se puede confiar | Must | **No aplica** (§4.1) | Propuesta | CU-00011 | EP-00001 |
| [US-00029](historias-usuario/US-00029-Responder-Por-El-Estado-Del-Servicio-Sin-Exigir-Acceso.md) | Responder por el estado del servicio en un punto que no exige acceso | Must | **No aplica** (§4.1) | Propuesta | CU-00011 | EP-00001 |

### 3.2 EP-00002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00001](historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) | Canjear correo y contraseña por un acceso firmado con sus cuatro reclamos | Must | **No aplica** (§4.1) | Propuesta | CU-00001 | EP-00002 |
| [US-00002](historias-usuario/US-00002-Responder-Credenciales-Invalidas-Sin-Declarar-Que-Campo-Fallo.md) | Responder credenciales inválidas **sin declarar cuál de los dos campos falló** | Must | **No aplica** (§4.1) | Propuesta | CU-00001 | EP-00002 |
| [US-00003](historias-usuario/US-00003-Responder-Con-Motivo-A-La-Cuenta-Pendiente-O-Bloqueada.md) | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | Must | **No aplica** (§4.1) | Propuesta | CU-00001 | EP-00002 |
| [US-00004](historias-usuario/US-00004-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md) | Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde | Must | **No aplica** (§4.1) | Propuesta | CU-00002 | EP-00002 |
| [US-00005](historias-usuario/US-00005-Exigir-El-Papel-Declarado-Por-Cada-Punto-De-Acceso.md) | Exigir el papel declarado por cada punto de acceso | Must | **No aplica** (§4.1) | Propuesta | CU-00002 | EP-00002 |
| [US-00008](historias-usuario/US-00008-Configurar-La-Cuenta-De-Administrador-Solo-Mientras-No-Exista-Ninguna.md) | Configurar la cuenta de administrador sólo mientras no exista ninguna | Must | **No aplica** (§4.1) | Propuesta | CU-00003 | EP-00002 |
| [US-00010](historias-usuario/US-00010-Cambiar-La-Contrasena-Propia-Exigiendo-La-Vigente.md) | Cambiar la contraseña propia exigiendo la vigente | Must | **No aplica** (§4.1) | Propuesta | CU-00003 | EP-00002 |
| [US-00024](historias-usuario/US-00024-Traducir-Cada-Codigo-Del-Contrato-Al-Codigo-De-Respuesta.md) | Traducir cada código del contrato al código de respuesta que le corresponde | Must | **No aplica** (§4.1) | Propuesta | CU-00009 | EP-00002 |
| [US-00025](historias-usuario/US-00025-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md) | Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor | Must | **No aplica** (§4.1) | Propuesta | CU-00009 | EP-00002 |

### 3.3 EP-00003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00006](historias-usuario/US-00006-Aplicar-La-Guardia-Del-Cambio-Pendiente-A-Todos-Los-Puntos-Salvo-Uno.md) | Aplicar la guardia del cambio de contraseña pendiente a todos los puntos salvo uno | Must | **No aplica** (§4.1) | Propuesta | CU-00002 | EP-00003 |
| [US-00007](historias-usuario/US-00007-Registrar-Una-Cuenta-De-Alumno-Sin-Campo-De-Contrasena.md) | Registrar una cuenta de alumno sin campo de contraseña | Must | **No aplica** (§4.1) | Propuesta | CU-00003 | EP-00003 |
| [US-00009](historias-usuario/US-00009-Cambiar-La-Contrasena-Propia-Con-La-Provisoria-Como-Vigente.md) | Cambiar la contraseña propia con la provisoria como vigente | Must | **No aplica** (§4.1) | Propuesta | CU-00003 | EP-00003 |
| [US-00011](historias-usuario/US-00011-Listar-Las-Cuentas-De-La-Comision-Con-Su-Situacion-Y-Su-Marca.md) | Listar las cuentas de la comisión con su situación y su marca | Must | **No aplica** (§4.1) | Propuesta | CU-00004 | EP-00003 |
| [US-00012](historias-usuario/US-00012-Cambiar-La-Situacion-De-Una-Cuenta-Con-Verificacion-De-Papel.md) | Cambiar la situación de una cuenta con verificación de papel | Must | **No aplica** (§4.1) | Propuesta | CU-00004 | EP-00003 |
| [US-00013](historias-usuario/US-00013-Dar-De-Baja-Transportando-El-Correo-Escrito-Como-Confirmacion.md) | Dar de baja una cuenta transportando el correo escrito como confirmación | Must | **No aplica** (§4.1) | Propuesta | CU-00004 | EP-00003 |
| [US-00014](historias-usuario/US-00014-Resetear-La-Contrasena-Y-Devolver-La-Provisoria-Una-Sola-Vez.md) | Resetear la contraseña de un alumno y devolver la provisoria **una sola vez** | Must | **No aplica** (§4.1) | Propuesta | CU-00005 | EP-00003 |
| [US-00015](historias-usuario/US-00015-No-Exigir-Ni-Comprobar-La-Situacion-De-La-Cuenta-Al-Resetear.md) | No exigir ni comprobar la situación de la cuenta al resetear | Must | **No aplica** (§4.1) | Propuesta | CU-00005 | EP-00003 |
| [US-00016](historias-usuario/US-00016-No-Registrar-La-Provisoria-En-Ninguna-Traza.md) | No registrar la provisoria en ninguna traza | Must | **No aplica** (§4.1) | Propuesta | CU-00005 | EP-00003 |

### 3.4 EP-00004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00019](historias-usuario/US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md) | Transportar el texto original **sin normalizarlo en el borde** | Must | **No aplica** (§4.1) | Propuesta | CU-00006 | EP-00004 |
| [US-00020](historias-usuario/US-00020-Eliminar-Un-Trabajo-Con-Los-Dos-Alcances-Forzando-La-Peticion.md) | Eliminar un trabajo con los dos alcances, verificado **forzando la petición** | Must | **No aplica** (§4.1) | Propuesta | CU-00006 | EP-00004 |
| [US-00021](historias-usuario/US-00021-Listar-Trabajos-Sin-Parametro-Para-Pedir-Borradores-Ajenos.md) | Listar trabajos con el alcance ya decidido y sin parámetro para pedir borradores ajenos | Must | **No aplica** (§4.1) | Propuesta | CU-00007 | EP-00004 |
| [US-00022](historias-usuario/US-00022-Devolver-El-Detalle-Con-Piezas-Componentes-Observaciones-Y-Comentario.md) | Devolver el detalle con piezas, componentes, observaciones y comentario | Must | **No aplica** (§4.1) | Propuesta | CU-00007 | EP-00004 |

### 3.5 EP-00005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00017](historias-usuario/US-00017-Enviar-Un-Trabajo-Nuevo-Y-Recibir-El-Estado-Que-La-Interpretacion-Decidio.md) | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | Must | **No aplica** (§4.1) | Propuesta | CU-00006 | EP-00005 |
| [US-00018](historias-usuario/US-00018-Reenviar-Un-Trabajo-En-Borrador-Con-El-Texto-Que-La-Persona-Volvio-A-Pegar.md) | Reenviar un trabajo en `Borrador` con el texto que la persona volvió a pegar | Must | **No aplica** (§4.1) | Propuesta | CU-00006 | EP-00005 |

### 3.6 EP-00006 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-00023](historias-usuario/US-00023-Aprobar-O-Rechazar-Un-Trabajo-En-Estado-Pendiente.md) | Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | **No aplica** (§4.1) | Propuesta | CU-00008 | EP-00006 |
| [US-00030](historias-usuario/US-00030-Ejercitar-La-Superficie-Con-Una-Coleccion-Reproducible.md) | Ejercitar la superficie con una colección reproducible en cinco pasos o menos | **Should** | **No aplica** (§4.1) | Propuesta | CU-00012 | EP-00006 |

### 3.2 `GeometriaFactory-Domain`

Las **veintisiete** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**, que es lo que esa sección declara que le corresponde. Ninguna historia se agrega, ninguna se retira y ninguna se renumera. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-02001 · Esqueleto ambulante y verificación de viabilidad

Sin historias. La etapa `a` es un hito interno sin capacidad funcional asociada, y todo su trabajo en este proyecto de código es técnico: vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-02001 a BT-02005. Declararlo acá evita que se lea como un hueco de cobertura.

### 3.2 EP-02002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02007](historias-usuario/US-02007-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md) | Reemplazar la credencial derivada exigiendo la vigente | Must | **No aplica** (§4.1) | Propuesta | CU-02003 | EP-02002 |
| [US-02008](historias-usuario/US-02008-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | Evaluar la admisibilidad de la cuenta y devolver su motivo | Must | **No aplica** (§4.1) | Propuesta | CU-02004 | EP-02002 |
| [US-02024](historias-usuario/US-02024-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | Configurar la cuenta de administrador en el primer arranque, habilitada y con credencial | Must | **No aplica** (§4.1) | Propuesta | CU-02012 | EP-02002 |
| [US-02025](historias-usuario/US-02025-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md) | Rechazar la configuración de un segundo administrador | Must | **No aplica** (§4.1) | Propuesta | CU-02012 | EP-02002 |

### 3.3 EP-02003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02001](historias-usuario/US-02001-Constituir-Un-Alumno-Con-Cuenta-Pendiente-Y-Sin-Credencial.md) | Constituir un alumno con cuenta `Pendiente` y sin credencial | Must | **No aplica** (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02002](historias-usuario/US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md) | Rechazar el alta con datos obligatorios ausentes | Must | **No aplica** (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02003](historias-usuario/US-02003-Exigir-La-Unicidad-Del-Correo-Verificada-En-El-Alta.md) | Exigir la unicidad del correo verificada en el alta | Must | **No aplica** (§4.1) | Propuesta | CU-02001 | EP-02003 |
| [US-02004](historias-usuario/US-02004-Habilitar-Bloquear-Y-Rehabilitar-Una-Cuenta.md) | Habilitar, bloquear y rehabilitar una cuenta | Must | **No aplica** (§4.1) | Propuesta | CU-02002 | EP-02003 |
| [US-02005](historias-usuario/US-02005-Dar-De-Baja-Una-Cuenta-Arrastrando-Sus-Trabajos.md) | Dar de baja una cuenta arrastrando sus trabajos en cualquier estado | Must | **No aplica** (§4.1) | Propuesta | CU-02002 | EP-02003 |
| [US-02006](historias-usuario/US-02006-Fijar-La-Credencial-Provisoria-En-El-Acto-De-Habilitacion.md) | Fijar la credencial derivada provisoria en el acto de habilitación | Must | **No aplica** (§4.1) | Propuesta | CU-02003, CU-02002 | EP-02003 |
| [US-02026](historias-usuario/US-02026-Resetear-La-Contrasena-Conservando-Cuenta-Y-Trabajos.md) | Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos | Must | **No aplica** (§4.1) | Propuesta | CU-02013 | EP-02003 |
| [US-02027](historias-usuario/US-02027-Exigir-El-Cambio-De-La-Provisoria-Antes-De-Toda-Otra-Capacidad.md) | Exigir el cambio de la contraseña provisoria antes de toda otra capacidad, y levantar la marca al cambiarla | Must | **No aplica** (§4.1) | Propuesta | CU-02004, CU-02003 | EP-02003 |

### 3.4 EP-02004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02009](historias-usuario/US-02009-Constituir-Un-Trabajo-Con-Dueno-Identidad-Y-Texto-Original.md) | Constituir un trabajo con dueño, identidad propia y texto original | Must | **No aplica** (§4.1) | Propuesta | CU-02005 | EP-02004 |
| [US-02010](historias-usuario/US-02010-Reeditar-Un-Trabajo-En-Borrador-Descartando-La-Interpretacion-Anterior.md) | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | Must | **No aplica** (§4.1) | Propuesta | CU-02005 | EP-02004 |
| [US-02018](historias-usuario/US-02018-Resolver-La-Pertenencia-De-Un-Trabajo-A-Su-Dueno.md) | Resolver la pertenencia de un trabajo a su dueño | Must | **No aplica** (§4.1) | Propuesta | CU-02009 | EP-02004 |
| [US-02019](historias-usuario/US-02019-Acotar-Al-Borrador-Lo-Que-El-Alumno-Reedita-Y-Elimina.md) | Acotar al estado `Borrador` lo que el alumno reedita y elimina | Must | **No aplica** (§4.1) | Propuesta | CU-02009 | EP-02004 |
| [US-02022](historias-usuario/US-02022-Excluir-Los-Borradores-Del-Alcance-Del-Administrador.md) | Excluir los trabajos en `Borrador` del alcance del administrador | Must | **No aplica** (§4.1) | Propuesta | CU-02011 | EP-02004 |

### 3.5 EP-02005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02011](historias-usuario/US-02011-Reconstruir-El-Conjunto-De-Piezas-Con-Identidad-Posicional.md) | Reconstruir el conjunto de piezas con identidad posicional | Must | **No aplica** (§4.1) | Propuesta | CU-02006 | EP-02005 |
| [US-02012](historias-usuario/US-02012-Derivar-La-Familia-Plana-O-Volumetrica-Desde-El-Tipo.md) | Derivar la familia plana o volumétrica desde el tipo | Should | **No aplica** (§4.1) | Propuesta | CU-02006 | EP-02005 |
| [US-02013](historias-usuario/US-02013-Registrar-Advertencias-Con-El-Valor-Declarado-Y-El-Derivado.md) | Registrar advertencias con el valor declarado y el derivado | Must | **No aplica** (§4.1) | Propuesta | CU-02007 | EP-02005 |
| [US-02014](historias-usuario/US-02014-Registrar-Errores-De-Validacion-Con-Posicion-De-Pieza-Y-Campo.md) | Registrar errores de validación con posición de pieza y campo | Must | **No aplica** (§4.1) | Propuesta | CU-02007 | EP-02005 |
| [US-02015](historias-usuario/US-02015-Enviar-Un-Trabajo-Que-Verifica-Y-Pasa-A-Estado-Pendiente.md) | Enviar un trabajo que verifica y pasa a estado `Pendiente` | Must | **No aplica** (§4.1) | Propuesta | CU-02008 | EP-02005 |
| [US-02016](historias-usuario/US-02016-Enviar-Un-Trabajo-Que-No-Verifica-Y-Queda-En-Borrador.md) | Enviar un trabajo que no verifica y queda en `Borrador` con sus errores | Must | **No aplica** (§4.1) | Propuesta | CU-02008 | EP-02005 |
| [US-02017](historias-usuario/US-02017-Rechazar-Toda-Transicion-Desde-Un-Estado-Terminal.md) | Rechazar toda transición desde un estado terminal | Must | **No aplica** (§4.1) | Propuesta | CU-02008 | EP-02005 |

### 3.6 EP-02006 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02020](historias-usuario/US-02020-Aprobar-Un-Trabajo-En-Estado-Pendiente.md) | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Must | **No aplica** (§4.1) | Propuesta | CU-02010 | EP-02006 |
| [US-02021](historias-usuario/US-02021-Rechazar-Un-Trabajo-En-Estado-Pendiente.md) | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | **No aplica** (§4.1) | Propuesta | CU-02010 | EP-02006 |
| [US-02023](historias-usuario/US-02023-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md) | Eliminar por el administrador en los tres estados que ve | Must | **No aplica** (§4.1) | Propuesta | CU-02011 | EP-02006 |

### 3.3 `GeometriaFactory-Application`

Las **treinta y dos** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**, que es lo que esa sección declara que le corresponde. Ninguna se agrega, ninguna se retira y ninguna se renumera. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-04001 · Esqueleto ambulante y verificación de viabilidad

Sin historias. La etapa `a` es un hito interno sin capacidad funcional asociada, y todo su trabajo en este proyecto de código es técnico: vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-04001 a BT-04006. Declararlo acá evita que se lea como un hueco de cobertura.

### 3.2 EP-04002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04003](historias-usuario/US-04003-Configurar-La-Cuenta-De-Administrador-Con-Su-Ventana-De-Alta.md) | Configurar la cuenta de administrador con su ventana de alta | Must | **No aplica** (§4.1) | Propuesta | CU-04010 | EP-04002 |
| [US-04007](historias-usuario/US-04007-Devolver-El-Motivo-De-Una-Cuenta-Que-No-Admite-Ingreso.md) | Devolver el motivo de una cuenta que no admite ingreso | Must | **No aplica** (§4.1) | Propuesta | CU-04003 | EP-04002 |
| [US-04009](historias-usuario/US-04009-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md) | Reemplazar la credencial derivada exigiendo la verificación de la vigente | Must | **No aplica** (§4.1) | Propuesta | CU-04003 | EP-04002 |
| [US-04028](historias-usuario/US-04028-Rechazar-La-Configuracion-De-Un-Segundo-Administrador.md) | Rechazar la configuración de un segundo administrador | Must | **No aplica** (§4.1) | Propuesta | CU-04010 | EP-04002 |

### 3.3 EP-04003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04001](historias-usuario/US-04001-Constituir-Una-Cuenta-De-Alumno-Pendiente-Y-Sin-Credencial.md) | Constituir una cuenta de alumno en estado `Pendiente` y sin credencial | Must | **No aplica** (§4.1) | Propuesta | CU-04001 | EP-04003 |
| [US-04002](historias-usuario/US-04002-Rechazar-El-Alta-Con-Un-Correo-Ya-Registrado.md) | Rechazar el alta con un correo ya registrado | Must | **No aplica** (§4.1) | Propuesta | CU-04001 | EP-04003 |
| [US-04004](historias-usuario/US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md) | Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad | Must | **No aplica** (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04005](historias-usuario/US-04005-Dar-De-Baja-Exigiendo-El-Correo-Escrito-Como-Confirmacion.md) | Dar de baja una cuenta exigiendo el correo escrito como confirmación | Must | **No aplica** (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04006](historias-usuario/US-04006-Arrastrar-En-La-Baja-Todos-Los-Trabajos-De-La-Cuenta.md) | Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado | Must | **No aplica** (§4.1) | Propuesta | CU-04002 | EP-04003 |
| [US-04008](historias-usuario/US-04008-Fijar-La-Credencial-Derivada-Provisoria-Dentro-De-La-Habilitacion.md) | Fijar la credencial derivada provisoria dentro de la habilitación | Must | **No aplica** (§4.1) | Propuesta | CU-04003, CU-04002 | EP-04003 |
| [US-04029](historias-usuario/US-04029-Resetear-La-Contrasena-De-Un-Alumno-Con-Verificacion-De-Facultad.md) | Resetear la contraseña de un alumno fijando una provisoria, con verificación de facultad | Must | **No aplica** (§4.1) | Propuesta | CU-04011 | EP-04003 |
| [US-04030](historias-usuario/US-04030-Impedir-Que-Una-Cuenta-Marcada-Ejerza-Cualquier-Otra-Capacidad.md) | Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad | Must | **No aplica** (§4.1) | Propuesta | CU-04011, y la comprobación transversal de `02` §4 | EP-04003 |
| [US-04031](historias-usuario/US-04031-Conservar-La-Cuenta-Su-Estado-Y-Todos-Sus-Trabajos-Tras-El-Reseteo.md) | Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo | Must | **No aplica** (§4.1) | Propuesta | CU-04011 | EP-04003 |
| [US-04032](historias-usuario/US-04032-Levantar-La-Marca-Con-El-Cambio-Hecho-Por-La-Propia-Cuenta.md) | Levantar la marca con el cambio efectivo hecho por la propia cuenta, y sólo con él | Must | **No aplica** (§4.1) | Propuesta | CU-04003 | EP-04003 |

### 3.4 EP-04004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04010](historias-usuario/US-04010-Cargar-Un-Trabajo-Con-Dueno-Identificador-Propio-Y-Sello-Del-Reloj.md) | Cargar un trabajo con dueño, identificador propio y sello tomado del reloj | Must | **No aplica** (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04011](historias-usuario/US-04011-Conservar-El-Texto-Original-Integro-Al-Cargar-Y-Al-Reeditar.md) | Conservar el texto original íntegro al cargar y al reeditar | Must | **No aplica** (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04012](historias-usuario/US-04012-Reeditar-Solo-Un-Trabajo-Propio-En-Borrador.md) | Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior | Must | **No aplica** (§4.1) | Propuesta | CU-04004 | EP-04004 |
| [US-04017](historias-usuario/US-04017-Listar-Los-Trabajos-Propios-Con-Los-Cuatro-Estados-Distinguibles.md) | Listar los trabajos propios con los cuatro estados distinguibles | Must | **No aplica** (§4.1) | Propuesta | CU-04006 | EP-04004 |
| [US-04019](historias-usuario/US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md) | Devolver el detalle con piezas y componentes, y el listado sin componentes | Must | **No aplica** (§4.1) | Propuesta | CU-04006 | EP-04004 |
| [US-04020](historias-usuario/US-04020-Listar-Los-Trabajos-De-La-Comision-Excluyendo-Los-Borradores.md) | Listar los trabajos de la comisión excluyendo los borradores | Must | **No aplica** (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04021](historias-usuario/US-04021-Filtrar-El-Listado-De-La-Comision-Por-Alumno.md) | Filtrar el listado de la comisión por alumno, con el recorte vigente | Must | **No aplica** (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04022](historias-usuario/US-04022-Abrir-El-Detalle-De-Un-Trabajo-De-La-Comision.md) | Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno | Must | **No aplica** (§4.1) | Propuesta | CU-04007 | EP-04004 |
| [US-04026](historias-usuario/US-04026-Eliminar-Un-Trabajo-Propio-Solo-En-Borrador.md) | Eliminar un trabajo propio sólo en `Borrador` | Must | **No aplica** (§4.1) | Propuesta | CU-04009 | EP-04004 |

### 3.5 EP-04005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04013](historias-usuario/US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md) | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | Must | **No aplica** (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04014](historias-usuario/US-04014-Enviar-Un-Trabajo-Con-Errores-Y-Que-Quede-En-Borrador.md) | Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación | Must | **No aplica** (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04015](historias-usuario/US-04015-Interpretar-El-Texto-Por-El-Puerto-Sin-Tocar-La-Base-De-Datos.md) | Interpretar el texto por el puerto de validación, sin tocar la base de datos | Must | **No aplica** (§4.1) | Propuesta | CU-04005 | EP-04005 |
| [US-04016](historias-usuario/US-04016-Terminar-De-Forma-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible.md) | Terminar de forma controlada cuando la interpretación no está disponible | **Should** | **No aplica** (§4.1) | Propuesta | CU-04005 | EP-04005 |

### 3.6 EP-04006 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-04018](historias-usuario/US-04018-Ver-El-Desenlace-Y-El-Comentario-Del-Trabajo-Propio.md) | Ver el desenlace y el comentario del trabajo propio | Must | **No aplica** (§4.1) | Propuesta | CU-04006 | EP-04006 |
| [US-04023](historias-usuario/US-04023-Aprobar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md) | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Must | **No aplica** (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04024](historias-usuario/US-04024-Rechazar-Un-Trabajo-En-Estado-Pendiente-Con-Comentario-Opcional.md) | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Must | **No aplica** (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04025](historias-usuario/US-04025-Rechazar-Toda-Transicion-Sin-Facultad-O-Desde-Un-Estado-Terminal.md) | Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal | Must | **No aplica** (§4.1) | Propuesta | CU-04008 | EP-04006 |
| [US-04027](historias-usuario/US-04027-Eliminar-Por-El-Administrador-En-Los-Tres-Estados-Que-Ve.md) | Eliminar por el administrador en los tres estados que ve | Must | **No aplica** (§4.1) | Propuesta | CU-04009 | EP-04006 |

### 3.4 `GeometriaFactory-Infrastructure`

Las **veinticinco** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó —`US-06001` a `US-06025`, sin huecos—, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-06001 · Esqueleto ambulante y verificación de viabilidad

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06024](historias-usuario/US-06024-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md) | Aplicar las transformaciones de esquema al arrancar, sobre base inexistente | Must | **No aplica** (§4.1) | Propuesta | CU-06010 | EP-06001 |
| [US-06025](historias-usuario/US-06025-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md) | Detener el arranque en lugar de operar sobre un almacén en el que no se puede confiar | Must | **No aplica** (§4.1) | Propuesta | CU-06010 | EP-06001 |

**Es la única de las cinco épicas de etapa `a` del producto que tiene historias**, y el motivo es que `PT-04` se mide en esa etapa: la imagen del servicio de datos **aplica sus actualizaciones de esquema sobre base vacía y responde salud** (`Roadmap-Producto.md` §5.2, transición `a` → `b`). Sin estas dos historias, esa puerta no se puede medir.

### 3.2 EP-06002 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06014](historias-usuario/US-06014-Sostener-En-El-Almacen-La-Unicidad-Del-Correo-Y-La-Del-Administrador.md) | Sostener en el almacén la unicidad del correo y la del administrador | Must | **No aplica** (§4.1) | Propuesta | CU-06005 | EP-06002 |
| [US-06015](historias-usuario/US-06015-Responder-Las-Dos-Preguntas-Sobre-El-Conjunto.md) | Responder si un correo está registrado y si ya existe una cuenta con papel `Administrador` | Must | **No aplica** (§4.1) | Propuesta | CU-06005 | EP-06002 |
| [US-06017](historias-usuario/US-06017-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro.md) | Derivar una contraseña sin guardarla ni registrarla en claro | Must | **No aplica** (§4.1) | Propuesta | CU-06006 | EP-06002 |
| [US-06018](historias-usuario/US-06018-Verificar-Una-Credencial-Y-Distinguir-El-Derivado-Ilegible.md) | Verificar una credencial y distinguir el valor derivado ilegible de la contraseña equivocada | Must | **No aplica** (§4.1) | Propuesta | CU-06006 | EP-06002 |
| [US-06021](historias-usuario/US-06021-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md) | Emitir el acceso firmado con sus cuatro reclamos | Must | **No aplica** (§4.1) | Propuesta | CU-06008 | EP-06002 |
| [US-06022](historias-usuario/US-06022-Rechazar-La-Emision-Sin-Clave-De-Firma.md) | Rechazar la emisión sin clave de firma, sin generar una al vuelo | Must | **No aplica** (§4.1) | Propuesta | CU-06008 | EP-06002 |
| [US-06023](historias-usuario/US-06023-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md) | Proveer el sello por un puerto, para que las pruebas lo puedan fijar | **Should** | **No aplica** (§4.1) | Propuesta | CU-06009 | EP-06002 |

### 3.3 EP-06003 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06013](historias-usuario/US-06013-Arrastrar-Todos-Los-Trabajos-De-Una-Cuenta-Dada-De-Baja.md) | Arrastrar todos los trabajos de una cuenta dada de baja, todo o nada | Must | **No aplica** (§4.1) | Propuesta | CU-06004 | EP-06003 |
| [US-06016](historias-usuario/US-06016-Conservar-Y-Transportar-La-Marca-Sin-Alterar-El-Estado.md) | Conservar y transportar la marca de cambio de contraseña pendiente sin alterar el estado | Must | **No aplica** (§4.1) | Propuesta | CU-06005 | EP-06003 |
| [US-06019](historias-usuario/US-06019-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | Producir una contraseña provisoria no adivinable y sin repetirse | Must | **No aplica** (§4.1) | Propuesta | CU-06007 | EP-06003 |
| [US-06020](historias-usuario/US-06020-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md) | Terminar sin producir valor cuando la fuente de aleatoriedad no responde | Must | **No aplica** (§4.1) | Propuesta | CU-06007 | EP-06003 |

### 3.4 EP-06004 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06008](historias-usuario/US-06008-Conservar-El-Texto-Original-Literal-Y-Rechazar-Toda-Escritura-Que-Lo-Reemplace.md) | Conservar el texto original literal y rechazar toda escritura que lo reemplace | Must | **No aplica** (§4.1) | Propuesta | CU-06003 | EP-06004 |
| [US-06009](historias-usuario/US-06009-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones.md) | Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo | Must | **No aplica** (§4.1) | Propuesta | CU-06003 | EP-06004 |
| [US-06010](historias-usuario/US-06010-Resolver-La-Consulta-Con-El-Recorte-Ya-Trasladado-Al-Pedido.md) | Resolver la consulta con el recorte ya trasladado al pedido | Must | **No aplica** (§4.1) | Propuesta | CU-06003 | EP-06004 |
| [US-06011](historias-usuario/US-06011-Excluir-Componentes-Y-Texto-Original-Del-Resultado-De-Un-Listado.md) | Excluir componentes y texto original del resultado de un listado | Must | **No aplica** (§4.1) | Propuesta | CU-06003 | EP-06004 |
| [US-06012](historias-usuario/US-06012-Retirar-Fisicamente-Un-Trabajo-Con-Todo-Lo-Que-Cuelga-De-El.md) | Retirar físicamente un trabajo con todo lo que cuelga de él | Must | **No aplica** (§4.1) | Propuesta | CU-06004 | EP-06004 |

### 3.5 EP-06005 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06001](historias-usuario/US-06001-Leer-El-Texto-Real-Con-Tolerancia-A-Comas-Finales-Y-Claves-Sinonimas.md) | Leer el texto real del alumno con tolerancia a comas finales y a las claves sinónimas | Must | **No aplica** (§4.1) | Propuesta | CU-06001 | EP-06005 |
| [US-06002](historias-usuario/US-06002-Devolver-La-Cantidad-De-Figuras-Del-Conjunto-Raiz.md) | Devolver la cantidad de figuras del conjunto raíz, incluidas las no reconstruidas | Must | **No aplica** (§4.1) | Propuesta | CU-06001 | EP-06005 |
| [US-06003](historias-usuario/US-06003-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md) | Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas | Must | **No aplica** (§4.1) | Propuesta | CU-06001 | EP-06005 |
| [US-06004](historias-usuario/US-06004-Emitir-El-Error-De-Validacion-Con-Posicion-De-Figura-Y-Campo.md) | Emitir el error de validación con posición de figura y campo | Must | **No aplica** (§4.1) | Propuesta | CU-06001 | EP-06005 |
| [US-06005](historias-usuario/US-06005-Derivar-El-Valor-Desde-Las-Dimensiones-Y-Los-Componentes.md) | Derivar el valor desde las dimensiones y los componentes | Must | **No aplica** (§4.1) | Propuesta | CU-06002 | EP-06005 |
| [US-06006](historias-usuario/US-06006-Comparar-Con-Tolerancia-Absoluta-Y-Operador-Estricto.md) | Comparar con tolerancia absoluta y **operador estricto** | Must | **No aplica** (§4.1) | Propuesta | CU-06002 | EP-06005 |
| [US-06007](historias-usuario/US-06007-Emitir-La-Advertencia-Con-El-Valor-Declarado-Y-El-Derivado.md) | Emitir la advertencia con el valor declarado y el derivado, sin corregir ninguno | Must | **No aplica** (§4.1) | Propuesta | CU-06002 | EP-06005 |

## 4. Métricas de avance

### 4.1 `GeometriaFactory-Api`

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 29 | 96,7 % | **No aplica** (§4.1) |
| Should | 1 | 3,3 % | **No aplica** (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **30** | **100 %** | **No aplica** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 30 de 30 |
| Historias cerradas | 0 de 30 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **30 de 30**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Puntos de acceso que las historias ponen en pie | **15 de 15**, con el reparto de [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §4. `A-04` está **retirado y no se recicla** |
| Puntos bajo la guardia | 11 de 15; los otros cuatro son ausencias declaradas y contables |
| Tareas técnicas declaradas | 26 |
| Tareas técnicas cerradas | 0 de 26 |
| Etapas del producto que este proyecto de código toca | 6 de las 8 comprometidas: `a`, `c`, `d`, `e`, `f` y `h` |
| Deuda declarada en el backlog | **8** tareas técnicas que cierran o elevan un punto abierto: BT-00005, BT-00007, BT-00009, BT-00010, BT-00015, BT-00021, BT-00025 y BT-00026. Son ocho sobre los diez puntos abiertos de §6: `PA-01` no se convierte en trabajo, y `PA-03` y `PA-04` comparten una sola tarea, BT-00015 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1).

### 4.1 Por qué el producto no estima, y por qué eso no es un pendiente

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por los mismos tres motivos que los proyectos de código ya emitidos: sin plazo calendario y avance por etapas cerradas; la **etapa** como unidad de planificación; y `equipo_n = 1`.

**Y hay un motivo propio, que en este proyecto de código es el más pesado del producto**: de los **diecisiete** requerimientos no funcionales de `05` §8, **cinco vienen rotulados [ASUNCIÓN]** desde el intake y siguen pendientes de confirmación —latencia, caudal, arranque en frío, cobertura y **la forma misma de la pirámide de pruebas**—. Es la mayor concentración de valores sin confirmar de los siete proyectos de código. Un backlog que usa cinco números vigentes sin respaldo, y que además inventara puntos de historia, tendría seis.

En consecuencia la columna `Estimación` dice **«No aplica»** en las treinta historias de este backlog, y **también en las veintiséis tareas técnicas** de [`Backlog-Tecnico.md`](Backlog-Tecnico.md), a las que el cierre se propagó en la ronda 2 del corte, y **el punto `PA-01` de §6 queda cerrado por lectura**: no era una decisión pendiente sino un hecho — **ocho etapas se cerraron sin una sola estimación**, con `equipo_n = 1` y sin capacidad numérica declarada (`Mini-Plan.md` §1.2). La figura es la de [`../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), el ítem obligatorio **sin objeto**.

### 4.2 Por qué la distribución MoSCoW es la que es

**29 `Must` y 1 `Should`**:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** Todas las capacidades que bajan a esta superficie son `Must Have`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no bajan acá.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue real de la fase `i…`, `F-15` a `F-17` son de esa misma fase y `F-18` a `F-20` están fuera del alcance de la primera versión. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Esta capa no dibuja y F-13 no baja acá con ninguna de las dos prioridades, pero contarla entre las de prioridad menor sería una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-00030**, la colección de peticiones reproducible. Y lo es porque **su origen no es una capacidad de `PRODUCT-INTAKE` §4 sino la estrategia de demostración de §16.1 y §18**: es un artefacto que vive en el árbol de muestras del repositorio y **no implementa nada, demuestra**. `02` §7.2 lo declara con todas las letras y por eso `CU-00012` **no traza a ninguna necesidad de negocio**. El producto funciona sin ella; lo que se pierde es la forma de demostración que el tipo de proyecto de código tiene declarada.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.**

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-00010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-00012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11 · GeometriaFactory-Domain), `US-00016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-00023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-00030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

### 4.2 `GeometriaFactory-Domain`

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 26 | 96,3 % | **No aplica** (§4.1) |
| Should | 1 | 3,7 % | **No aplica** (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **27** | **100 %** | **No aplica** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 27 de 27 |
| Historias cerradas | 0 de 27 |
| Porcentaje cerrado | 0 % |
| Tareas técnicas declaradas | 16 |
| Tareas técnicas cerradas | 0 de 16 |
| Etapas del producto que este proyecto de código toca | 6 de las 8 comprometidas (`a`, `c`, `d`, `e`, `f`, `h`) |
| Deuda declarada en el backlog | 4 tareas técnicas que cierran un punto abierto: BT-02002, BT-02003, BT-02015 y BT-02016 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance del producto se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1); esta tabla mide sólo el estado de este backlog.

### 4.1 Por qué el producto no estima, y por qué eso no es un pendiente

La regla de la categoría exige declarar una técnica de estimación y mantenerla. **Este backlog no la fija, y lo declara en lugar de inventarla.**

El intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas** (`Roadmap-Producto.md` §1.1, que lo cita de `PRODUCT-INTAKE` §10). No hay historial de iteraciones cerradas del que derivar una velocidad, no hay iteraciones —la unidad de planificación es la **etapa**, no el sprint (`Roadmap-Producto.md` §1.2)— y el equipo es de **una sola persona** (`PRODUCT-INTAKE` §2, `equipo_n = 1`). Poner puntos de historia o tallas acá produciría números que ninguna fuente sostiene y que la categoría 07 tomaría como capacidad.

En consecuencia: la columna `Estimación` dice **«No aplica»** en las veintisiete historias de este backlog, y **también en las dieciséis tareas técnicas** de [`Backlog-Tecnico.md`](Backlog-Tecnico.md), a las que el cierre se propagó en la ronda 2 del corte, y **`PA-01` de §6 queda cerrado por lectura**: el producto no estima, y **ocho etapas se cerraron sin una sola estimación**. Lo que sí se declara y se usa para ordenar es la **etapa** de cada ítem, que es la unidad que el producto sí tiene.

### 4.2 Por qué la distribución MoSCoW es la que es

La regla de la categoría marca como anti-patrón que todo sea `Must`. Este backlog queda en **26 `Must` sobre 27**, y el motivo es del alcance del producto y no de una falta de priorización:

1. **La prioridad la declara el Product Owner en el intake, y esta categoría no reprioriza** (`Rules-Plan-Sprint.md` §1.3 declara esa división de titularidad para AG-06). `PRODUCT-INTAKE` §4 declara **diecinueve** de sus **veintiséis** capacidades como `Must Have` —**dieciocho** hasta el 2026-08-10, y `F-13` desde que la versión **1.19** de esa fuente la promovió—.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no tocan este proyecto de código.** Son **siete** desde el 2026-08-10, y no ocho: F-14 es del despliegue, F-15 a F-17 son de etapa `i…` y F-18 a F-20 están fuera del alcance de la primera versión. Ninguna de esas siete baja a entidades ni a invariantes del dominio. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Para este proyecto de código el cambio es sólo de exactitud del enunciado —F-13 es de la visualización y nunca bajó acá—, pero el enunciado hay que corregirlo igual, porque contar a una `Must Have` entre las capacidades de prioridad menor es una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-02012**, y lo es porque su origen no es una capacidad sino una decisión técnica pre-tomada del intake (§17.1.P.11 · GeometriaFactory-Domain punto 4, la familia plana o volumétrica derivada del tipo por tabla de consulta). El dominio funciona sin ella; lo que se pierde es una derivación de conveniencia.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.** Si una etapa aprieta, lo que se difiere no es una historia `Should` sino una etapa entera, y las etapas son secuenciales y con punto de control bloqueante (`Roadmap-Producto.md` §4 y §5.1). El ejercicio de recorte existe, pero su unidad es la etapa.

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-02010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-02012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11 · GeometriaFactory-Domain), `US-02016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-02023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-00030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

### 4.3 `GeometriaFactory-Application`

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 31 | 96,9 % | **No aplica** (§4.1) |
| Should | 1 | 3,1 % | **No aplica** (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **32** | **100 %** | **No aplica** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 32 de 32 |
| Historias cerradas | 0 de 32 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **32 de 32**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 21 |
| Tareas técnicas cerradas | 0 de 21 |
| Etapas del producto que este proyecto de código toca | **6** de las 8 comprometidas: `a`, `c`, `d`, `e`, `f` y `h`. La `a` es una de ellas aunque no produzca historias: su trabajo es íntegramente técnico |
| Deuda declarada en el backlog | 5 tareas técnicas que cierran o elevan un punto abierto: BT-04002, BT-04003, BT-04018, BT-04020 y BT-04021 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance del producto se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1); esta tabla mide sólo el estado de este backlog.

### 4.1 Por qué el producto no estima, y por qué eso no es un pendiente

La regla de la categoría exige declarar una técnica de estimación y mantenerla. **Este backlog no la fija, y lo declara en lugar de inventarla**, con el mismo fundamento que los tres proyectos de código de nivel 0 ya emitidos:

1. El intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas** (`Roadmap-Producto.md` §1.1, que lo cita de `PRODUCT-INTAKE` §10).
2. **No hay iteraciones**: la unidad de planificación es la **etapa**, no el sprint (`Roadmap-Producto.md` §1.2), de modo que no hay historial del que derivar una velocidad.
3. **El equipo es de una sola persona** (`PRODUCT-INTAKE` §2, `equipo_n = 1`).

En consecuencia, la columna `Estimación` dice **«No aplica»** en las treinta y dos historias de este backlog, y **también en las veintiuna tareas técnicas** de [`Backlog-Tecnico.md`](Backlog-Tecnico.md), a las que el cierre se propagó en la ronda 2 del corte, y **`PA-01` de §6 queda cerrado por lectura**: el producto no estima, y **ocho etapas se cerraron sin una sola estimación**. Lo que sí se declara y ordena es la **etapa** de cada ítem.

**Hay un motivo propio de este proyecto de código**, y conviene decirlo: el único NFR de tiempo que lo alcanza —los **500 ms** del caso de uso más pesado— viene **rotulado como asunción** desde el intake y sigue pendiente de confirmación del Product Owner (`05` §8 y §11 `PA-05`). Un backlog que usa como vigente un número que su propia fuente no confirmó, y que además inventara puntos de historia, tendría dos números sin respaldo en lugar de uno.

### 4.2 Por qué la distribución MoSCoW es la que es

**31 `Must` y 1 `Should`**, y el motivo es del alcance del producto y no de una falta de priorización:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** `PRODUCT-INTAKE` §4 declara como `Must Have` todas las capacidades que bajan a esta capa: `F-01` a `F-12`, `F-22`, `F-23`, `F-24` y `F-26`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no tocan este proyecto de código.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue; `F-15` a `F-17` son de la fase `i…`; `F-18` a `F-20` están fuera del alcance de la primera versión. Ninguna de esas siete baja a un caso de uso de esta capa. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. No cambia nada del trabajo de esta capa —F-13 es de la visualización y de la presentación, que esta capa no toca—, pero sí cambia el enunciado, porque contar a una `Must Have` entre las de prioridad menor es una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-04016**, y lo es porque **su origen no es una capacidad sino una decisión de esta arquitectura**: `05` §4 declara que «la indisponibilidad de un puerto es una condición y no una excepción que escapa». Ninguna capacidad de §4 del intake la pide. El producto funciona sin ella —el caso de uso de envío terminaría con una excepción del consumidor— y lo que se pierde es que el texto original quede intacto y el motivo sea legible. Diferible, y no gratis.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.** Si una etapa aprieta, lo que se difiere no es una historia `Should` sino una etapa entera, y las etapas son secuenciales y con punto de control bloqueante (`Roadmap-Producto.md` §4 y §5.1).

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-04010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-04012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11 · GeometriaFactory-Domain), `US-04016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-04023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-04030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

### 4.4 `GeometriaFactory-Infrastructure`

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 24 | 96,0 % | **No aplica** (§4.1) |
| Should | 1 | 4,0 % | **No aplica** (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **25** | **100 %** | **No aplica** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 25 de 25 |
| Historias cerradas | 0 de 25 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **25 de 25**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 26 |
| Tareas técnicas cerradas | 0 de 26 |
| Etapas del producto que este proyecto de código toca | 5 de las 8 comprometidas: `a`, `c`, `d`, `e` y `f` |
| Casos de la batería obligatoria del validador | 0 de **10**, con los **ocho** escenarios `E-1` a `E-8` del intake §20 como entrada |
| Deuda declarada en el backlog | **7** tareas técnicas que cierran o elevan un punto abierto: BT-06002, BT-06003, BT-06019, BT-06023, BT-06024, BT-06025 y BT-06026 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1).

### 4.1 Por qué el producto no estima, y por qué eso no es un pendiente

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por los mismos tres motivos que los proyectos de código ya emitidos: sin plazo calendario y avance por etapas cerradas; unidad de planificación la **etapa** y no el sprint; y `equipo_n = 1`.

**Y hay un motivo propio, más fuerte todavía**: de los **catorce** NFR de `05` §8, **tres vienen rotulados [ASUNCIÓN]** desde el intake y siguen pendientes de confirmación del Product Owner —los 200 ms de la interpretación y las **tres** coberturas, incluida la de **95 %** del validador, que es el número más alto del producto—. Un backlog que usa como vigentes tres números sin confirmar, y que además inventara puntos de historia, tendría cuatro números sin respaldo en lugar de tres.

En consecuencia la columna `Estimación` dice **«No aplica»** en las veinticinco historias de este backlog, y **también en las veintiséis tareas técnicas** de [`Backlog-Tecnico.md`](Backlog-Tecnico.md), a las que el cierre se propagó en la ronda 2 del corte, y **el punto `PA-01` de §6 queda cerrado por lectura**: no era una decisión pendiente sino un hecho — **ocho etapas se cerraron sin una sola estimación**, con `equipo_n = 1` y sin capacidad numérica declarada (`Mini-Plan.md` §1.2). La figura es la de [`../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), el ítem obligatorio **sin objeto**.

### 4.2 Por qué la distribución MoSCoW es la que es

**24 `Must` y 1 `Should`**:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** Todas las capacidades que bajan a esta capa son `Must Have`: `F-01` a `F-12`, `F-22` a `F-24` y `F-26`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no bajan acá.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue, `F-15` a `F-17` son de la fase `i…` y `F-18` a `F-20` están fuera del alcance de la primera versión. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Esta capa no la toca ni antes ni después —es de la visualización y de la presentación—, pero contar a una `Must Have` entre las de prioridad menor sería una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-06023**, proveer el sello por un puerto. Y lo es por una razón que la propia categoría 02 ya dejó escrita: **`CU-06009` es el único de los diez casos de uso que no traza a ninguna necesidad de negocio** (`02` §7.2). Su origen no es una capacidad sino **una decisión de testabilidad** —que los sellos sean verificables en prueba, `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 3—. El producto funciona sin ella; lo que se pierde es que las pruebas de las capas de adentro sean reproducibles sin fijar el reloj del entorno.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.**

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-06010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-06012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11 · GeometriaFactory-Domain), `US-06016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-06023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-00030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

### 5.1 `GeometriaFactory-Api`

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | **Cada vez que la etapa agrega un punto de acceso**, con la lista de los quince y la guardia sobre la mesa. Es la sesión que mitiga el defecto característico de esta capa, que es **de omisión** |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra el **punto de acceso** de `Definicion-Superficie-HTTP.md` §3 que la realiza, contra el componente de `05` §3.1 que la aloja y contra la tabla de traducción de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 |
| Entrada obligatoria a la sesión | Los **quince** puntos de acceso con su columna de guardia, los **diecisiete** códigos vivos del contrato con su destino, y las **tres** familias deliberadamente empobrecidas |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Dos reglas propias de este refinamiento.** La primera: **todo punto de acceso nuevo se compara contra la lista de la guardia antes de escribirse**, porque `05` §9 declara con probabilidad **alta** que un punto quede fuera de ella y **nada falle**; los defectos de omisión no se ven leyendo el punto nuevo. La segunda: **toda respuesta de fallo se compara contra su vecina**, porque las **tres** familias deliberadamente empobrecidas —credenciales inválidas, recurso que no se ve y correo ya registrado— **dicen menos de lo que el servicio sabe, y en las tres es la decisión y no el defecto**.

### 5.2 `GeometriaFactory-Domain`

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa y antes de escribir la primera línea de código. La cadencia por sprint de la regla no aplica: no hay sprints, la unidad es la etapa (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Al cerrar la etapa, sobre las historias de la siguiente, dentro de la preparación del punto de control |
| Responsable | La única persona del equipo, con el papel de AG-06. Con `equipo_n = 1` no hay dos papeles que negociar, y por eso el filtro real de calidad es la Definition of Ready y no el acuerdo entre personas |
| Formato | Revisión de la historia contra su caso de uso de 02 y contra el componente de `05` §3.1 que la sostiene. **Sin estimación relativa**, por §4.1 |
| Entrada obligatoria a la sesión | Los puntos abiertos de `05` §11 que la etapa cierra, y las condiciones de error del catálogo de 03 que la etapa produce |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

### 5.3 `GeometriaFactory-Application`

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa y antes de escribir la primera línea de código. La cadencia por sprint de la regla no aplica: no hay sprints, la unidad es la etapa (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Al cerrar la etapa, sobre las historias de la siguiente, dentro de la preparación del punto de control |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su caso de uso de 02, contra el componente de `05` §3.1 que la sostiene y contra las **cuatro** comprobaciones de `02` §4. **Sin estimación relativa**, por §4.1 |
| Entrada obligatoria a la sesión | Las **cuatro** comprobaciones transversales con su orden fijo, los **cuatro** puertos y las condiciones del catálogo de 03 que la etapa produce |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega una operación que lee o escribe algo, la sesión pregunta **si la cuarta comprobación la alcanza**. `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca de cambio de contraseña pendiente, y `Domain ADR-04005` §6 ya declaró que el dominio **no puede impedirlo**. Es una dependencia de disciplina que cae acá, y el refinamiento es donde se ejerce.

### 5.4 `GeometriaFactory-Infrastructure`

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | **Al abrir la etapa `f`**, sobre las siete historias del validador, con el análisis de las **cuatro trampas** del formato y los **ocho escenarios** sobre la mesa. Es la sesión que mitiga el riesgo de negocio del producto |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra el componente de `05` §3.1 que la sostiene y contra las **siete** reglas conceptuales de modelo cuando toca el almacén |
| Entrada obligatoria a la sesión | Las **17** condiciones del catálogo de 03 con su distinción entre **resultado** y **fallo**, las **cuatro** trampas del formato, y la lista de las **cinco** cosas que nunca entran en un mensaje ni en una traza, más el texto original del alumno |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Dos reglas propias de este refinamiento.** La primera: cada vez que una historia agrega un camino de fallo, la sesión pregunta **si ese camino podría componer el valor por otro medio en lugar de detenerse**. `05` §9 declara como riesgo de impacto **muy alto** que la provisoria se componga por un contador, la fecha o el correo cuando la fuente de material impredecible no responde, porque **el reseteo parece haber funcionado**. La segunda: cada vez que una historia devuelve una condición, la sesión pregunta **si es un resultado o un fallo**; `05` §9 declara con probabilidad **alta** que un texto ilegible termine devolviendo la condición de servicio no disponible, y el alumno esperaría a que se recupere de un problema que no tiene.

## 6. Puntos abiertos de este backlog

### 6.1 `GeometriaFactory-Api`

> **Correspondencia con `Root-Rules.md` §12.2.** La columna **`Punto abierto`** realiza sus campos
> **1 · qué falta** —el enunciado en negrita— y **2 · por qué no se puede hoy** —el desarrollo que
> sigue—; **`Quién lo cierra`** realiza el campo 3 y **`En qué evento se cierra`** el campo 4.
> **`Estado` no es un campo de §12.2**: deriva de su tabla de escalamiento y se declara como tal.


| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `c` § «Decidido en esta etapa, y elevado al punto de control» |**Cerrado** el 2026-08-25 · **por lectura, en el corte de la 06 de la migración 10.0 → 13.3**: el producto **no estima**, y no es una decisión pendiente sino un hecho. `PRODUCT-INTAKE` §2 declara `equipo_n = 1`; `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**; y **ocho etapas se cerraron sin una sola estimación**. Se cierra **retirando el punto**, que `A3-Decisiones-Del-Product-Owner.md` §3 admite como cierre válido, con la figura de `ADR-14004` |
| PA-02 | **Las rutas y los verbos definitivos** de los quince puntos de acceso (`05` §11 `PA-01`). Las **dos** únicas cosas que una fuente declara son el punto de canje, con su ruta, y la **existencia** de un punto de salud, cuya ruta la fuente no da; las quince filas son **propuesta derivada rotulada fila por fila**. Convertido en trabajo como BT-00007 | El equipo en el punto de control de la etapa `a` | `src/GeometriaFactory.Api/Endpoints/` | **Cerrado** el 2026-08-20 · **A2b, por lectura**: **18 puntos de acceso** mapeados, con sus verbos |
| PA-03 | ~~**Qué código del contrato recibe una operación de administrador pedida por quien no lo es**, fuera del desenlace~~ (`05` §11 `PA-02`). **CERRADO**: el Product Owner incorporó `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` al conjunto cerrado y `GeometriaFactory-Contracts` lo emite. Esta categoría **no inventó ningún código**, que era la condición con la que lo declaró abierto. BT-00015 pierde su motivo de elevación y conserva el de propagación | **Cerrado** por el Product Owner, `PRODUCT-INTAKE` **1.29** §17.4 P.3 | **Resuelto** el **2026-08-12** | **Cerrado** |
| PA-04 | ~~**Qué código del contrato recibe un envío o una reedición forzados fuera de `Borrador`**~~ (`05` §11 `PA-03`). **CERRADO** por la misma decisión, con `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` | **Cerrado** por el Product Owner, `PRODUCT-INTAKE` **1.29** §17.4 P.3 | **Resuelto** el **2026-08-12** | **Cerrado** |
| PA-05 | **La vigencia exacta del acceso firmado** (`05` §11 `PA-04`). El intake declara «corta» y sin acceso de refresco, y **no fija número**; la ADR correspondiente fija el **criterio** y toma el número de configuración. Convertido en trabajo como BT-00010 | El equipo en la etapa `a`, y el Product Owner si quisiera fijarlo | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.1, **fase `i` · Despliegue real**, que es cuando la configuración del ambiente se declara de verdad | **Vigente, y reformulado el 2026-08-26 por lectura** (`A2c`). **Deja de ser cierto que «no hay número»**: el valor en efecto es **480 minutos —ocho horas—**, por omisión de `SigningOptions.cs:25` (`public int LifetimeInMinutes { get; set; } = 480;`), y **nada lo sobreescribe**: ni `appsettings`, ni la composición. **Lo que queda abierto es otra pregunta, y es del Product Owner**: si ocho horas cumplen el criterio que `ADR-00003` §5 fija —«que **caduque dentro de la sesión de trabajo de una clase**»—, porque **ninguna fuente declara cuánto dura una clase** y sin ese dato la comparación no se puede hacer leyendo. **Y una obligación derivada**, con el mismo criterio con que el Product Owner cerró `D4` el 2026-08-20: el valor **se declara explícitamente** en la configuración, porque una configuración que se sobreentiende acierta hasta el día que alguien cambia el otro lado |
| PA-06 | **El valor del límite de tamaño del cuerpo de una petición** (`05` §11 `PA-05`). La **forma** ya está decidida y no se reabre —un solo límite para todo el producto, tomado de configuración, que **rechaza y nunca trunca**—; lo que falta es el número. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá**. Convertido en trabajo como BT-00009 | El equipo en la etapa `a`, y el Product Owner si quisiera un valor propio | **Decisión del Product Owner, 2026-08-20** · `Audit/A3-Decisiones-Del-Product-Owner.md` `D4` | **Cerrado** el 2026-08-20 · Se adopta **el límite por omisión del servidor HTTP** y no se fija uno propio. **Queda una obligación derivada**: por la regla que `scripts/verify-explicit-configuration.sh` hace cumplir —una configuración que se sobreentiende acierta hasta que alguien cambia el otro lado—, **el valor por omisión se declara explícitamente** cuando se toque la composición |
| PA-07 | ~~**El alcance de la colección de peticiones**~~ (`05` §11 `PA-06`). **CERRADO a favor de los ocho escenarios `E-1` a `E-8`**, que es la lectura que la categoría 02 ya había adoptado y que esta categoría heredaba: **no cambia ningún artefacto de este backlog**. BT-00021 pierde su motivo de elevación | **Cerrado** por el Product Owner, `PRODUCT-INTAKE` **1.29** §18 | **Resuelto** el **2026-08-12** | **Cerrado** |
| PA-08 | **Los nombres de tipos y de espacios de nombres, y las versiones exactas de los paquetes** (`05` §11 `PA-07`). Convertido en trabajo como BT-00005 | El equipo en la etapa `a` | [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §6, glosario de correspondencia | **Cerrado** el 2026-08-20 · **A2, por lectura**: la norma fija el glosario y `src/` tiene **23 espacios de nombres** vivos con esos nombres |
| PA-09 | **La construcción de la imagen en destino desde el repositorio** (`05` §11 `PA-08`), que el intake rotula **[A VERIFICAR]** y exige **probar una vez antes de depender del mecanismo**. **No es una asunción de esta categoría.** Convertido en trabajo como BT-00026 | `09-Devops`, midiendo | [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.1, fase `i` · Despliegue real | **Vigente.** La fase `i` está planificada y **no ocurrió** |
| PA-10 | **Los cinco valores rotulados [ASUNCIÓN]** de `05` §8 —latencia, caudal, arranque en frío, cobertura y forma de la pirámide—, pendientes en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-09`). Convertido en trabajo como BT-00025 | El Product Owner sobre su propio documento | `09-Devops/Estrategia-Versionado.md` y `09-Devops/Pipeline-CI-CD.md`, §ubicación de las puertas | **Cerrado** el 2026-08-26 · **decisión del Product Owner**, sobre [`../../../Audit/D1-Confirmacion-De-Asunciones.md`](../../../Audit/D1-Confirmacion-De-Asunciones.md) 1.0 y el intake §22: **confirma los valores rotulados `[ASUNCIÓN]`** —los umbrales de latencia y arranque de `A-5`, las coberturas y la forma de la pirámide de `A-3`, los gates que no son de líneas de `A-4` y las métricas de negocio de `A-2`—. **Con una excepción declarada y no confirmada: el caudal de 20 peticiones por minuto**, cuyo fundamento se cayó al cerrarse `D5` —el volumen de la comisión, incognoscible— y **cuyo valor definitivo sale de `PT-05`**, no de esta confirmación. Sigue **provisorio** y con su fila propia en `05` §8. **Confirmar no es medir**: los umbrales se eligieron altos donde la fuente señala criticidad, y ninguna fuente los fija |

**Diez filas: siete abiertas y tres resueltas —`PA-03`, `PA-04` y `PA-07`—, las tres cerradas por `PRODUCT-INTAKE` **1.29** el 2026-08-12.** Las filas resueltas se conservan con su desenlace y su fecha en lugar de retirarse. **`PA-10` de la categoría 05 no figura acá porque está resuelto**: los recuentos congelados del catálogo de condiciones de la categoría 03 quedaron corregidos en su versión 1.3 y coinciden punto por punto con lo que la categoría 05 publica. Este backlog usa los números vigentes y **no reabre** el punto.

### 6.2 `GeometriaFactory-Domain`

| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PA-01 | **La unidad de estimación.** Ninguna fuente da base para puntos de historia ni para tallas, por lo declarado en §4.1. Queda por decidir si se adopta alguna al cerrarse las primeras etapas, cuando ya haya historia real, o si el producto se planifica siempre por etapa | El Product Owner, que es también quien ejecuta | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `c` § «Decidido en esta etapa, y elevado al punto de control» |**Cerrado** el 2026-08-25 · **por lectura, en el corte de la 06 de la migración 10.0 → 13.3**: el producto **no estima**, y no es una decisión pendiente sino un hecho. `PRODUCT-INTAKE` §2 declara `equipo_n = 1`; `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**; y **ocho etapas se cerraron sin una sola estimación**. Se cierra **retirando el punto**, que `A3-Decisiones-Del-Product-Owner.md` §3 admite como cierre válido, con la figura de `ADR-14004` |
| PA-02 | **Los nombres de tipos y de espacios de nombres**, que el intake deja abiertos y ata al punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain; `05` §11 PA-01). Este backlog no los resuelve: los convierte en trabajo, BT-02002 | El Product Owner en el punto de control de la etapa `a` | [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §6, glosario de correspondencia | **Cerrado** el 2026-08-20 · **A2, por lectura**: la norma fija el glosario y `src/` tiene **23 espacios de nombres** vivos con esos nombres |
| PA-03 | **La herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain; `05` §11 PA-04). Convertido en trabajo como BT-02003 | El equipo en la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PA-04 | **Los dos valores rotulados [ASUNCIÓN]** de `05` §8 —tiempo de la batería de pruebas y cobertura mínima—, pendientes de confirmación en `PRODUCT-INTAKE` §22 (asunción `A-3` para la cobertura y `A-5` para el tiempo). Convertido en trabajo como BT-02015 | El Product Owner sobre su propio documento | `09-Devops/Estrategia-Versionado.md` y `09-Devops/Pipeline-CI-CD.md`, §ubicación de las puertas | **Cerrado** el 2026-08-26 · **decisión del Product Owner**, sobre [`../../../Audit/D1-Confirmacion-De-Asunciones.md`](../../../Audit/D1-Confirmacion-De-Asunciones.md) 1.0 y el intake §22: **confirma los valores rotulados `[ASUNCIÓN]`** —los umbrales de latencia y arranque de `A-5`, las coberturas y la forma de la pirámide de `A-3`, los gates que no son de líneas de `A-4` y las métricas de negocio de `A-2`—. **Con una excepción declarada y no confirmada: el caudal de 20 peticiones por minuto**, cuyo fundamento se cayó al cerrarse `D5` —el volumen de la comisión, incognoscible— y **cuyo valor definitivo sale de `PT-05`**, no de esta confirmación. Sigue **provisorio** y con su fila propia en `05` §8. **Confirmar no es medir**: los umbrales se eligieron altos donde la fuente señala criticidad, y ninguna fuente los fija |
| PA-05 | **La ambigüedad del intake sobre RN-02012 e INV-09** (`05` §11 PA-03, `02` §4). Este backlog hereda la lectura de 02 y no la resuelve; ninguna historia depende de cuál lectura rija | El Product Owner sobre `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |
| PA-06 | **El criterio de comparación de dos correos** (`02` §9). Convertido en trabajo como BT-02016 | `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación | `src/GeometriaFactory.Application/…/EmailIdentity.Normalize` | **Cerrado** el 2026-08-20 · **A2b, por lectura**: la normalización existe y la consumen `IAccountRepository` y `ResolveSignInUseCase` |

### 6.3 `GeometriaFactory-Application`

| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1. Queda por decidir si se adopta alguna al cerrarse las primeras etapas, cuando ya haya historia real, o si el producto se planifica siempre por etapa | El Product Owner, que es también quien ejecuta | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `c` § «Decidido en esta etapa, y elevado al punto de control» |**Cerrado** el 2026-08-25 · **por lectura, en el corte de la 06 de la migración 10.0 → 13.3**: el producto **no estima**, y no es una decisión pendiente sino un hecho. `PRODUCT-INTAKE` §2 declara `equipo_n = 1`; `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**; y **ocho etapas se cerraron sin una sola estimación**. Se cierra **retirando el punto**, que `A3-Decisiones-Del-Product-Owner.md` §3 admite como cierre válido, con la figura de `ADR-14004` |
| PA-02 | **El identificador del cuarto puerto**, el de repositorio de cuentas. `05` §11 `PA-01` confirma que el puerto existe, declara que su ausencia en el intake es una omisión de **nombre** y no de alcance, y ata el nombre al **punto de control de la etapa `a`**. Este backlog **no lo fija**: lo convierte en trabajo como parte de BT-04002, con esa caja temporal | El equipo en el punto de control de la etapa `a` | `src/GeometriaFactory.Application/Ports/IAccountRepository.cs` | **Cerrado** el 2026-08-20 · **A2, por lectura**: el cuarto puerto existe y se llama `IAccountRepository` |
| PA-03 | **Los nombres definitivos de tipos y de espacios de nombres** (`05` §11 `PA-02`). Convertido en trabajo como BT-04002 | El Product Owner y el equipo en el punto de control de la etapa `a` | [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §6, glosario de correspondencia | **Cerrado** el 2026-08-20 · **A2, por lectura**: la norma fija el glosario y `src/` tiene **23 espacios de nombres** vivos con esos nombres |
| PA-04 | **La herramienta que calcula la versión** a partir de las convenciones de mensaje de confirmación (`05` §11 `PA-06`). Convertido en trabajo como BT-04003 | El equipo en la etapa `a` | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `a` § «Decidido en esta etapa, y elevado al punto de control» | **VENCIDO.** La etapa `a` cerró el **2026-08-13** y el punto sigue abierto |
| PA-05 | **Los dos valores rotulados [ASUNCIÓN]** de `05` §8 —los 500 ms del caso de uso más pesado y la cobertura mínima—, pendientes de confirmación en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-05`). Convertido en trabajo como BT-04018 | El Product Owner sobre su propio documento | `09-Devops/Estrategia-Versionado.md` y `09-Devops/Pipeline-CI-CD.md`, §ubicación de las puertas | **Cerrado** el 2026-08-26 · **decisión del Product Owner**, sobre [`../../../Audit/D1-Confirmacion-De-Asunciones.md`](../../../Audit/D1-Confirmacion-De-Asunciones.md) 1.0 y el intake §22: **confirma los valores rotulados `[ASUNCIÓN]`** —los umbrales de latencia y arranque de `A-5`, las coberturas y la forma de la pirámide de `A-3`, los gates que no son de líneas de `A-4` y las métricas de negocio de `A-2`—. **Con una excepción declarada y no confirmada: el caudal de 20 peticiones por minuto**, cuyo fundamento se cayó al cerrarse `D5` —el volumen de la comisión, incognoscible— y **cuyo valor definitivo sale de `PT-05`**, no de esta confirmación. Sigue **provisorio** y con su fila propia en `05` §8. **Confirmar no es medir**: los umbrales se eligieron altos donde la fuente señala criticidad, y ninguna fuente los fija |
| PA-06 | **Los sellos de alta, de modificación y de desenlace**: el intake los sostiene como verificables en prueba y el modelo del dominio **no los declara como atributos** (`05` §11 `PA-04`). Este backlog **no lo resuelve**: lo eleva como BT-04020 | El Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos a su modelo | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |
| PA-07 | **El criterio de comparación de dos correos** (`05` §11 `PA-03`). **No es de este proyecto de código decidirlo**: `05` lo derivó a la categoría 05 de `GeometriaFactory-Infrastructure`, que es la que materializa el índice. Convertido en trabajo como BT-04021, que **acompaña** la decisión y no la toma | La categoría 05 de `GeometriaFactory-Infrastructure` | `src/GeometriaFactory.Application/…/EmailIdentity.Normalize` | **Cerrado** el 2026-08-20 · **A2b, por lectura**: la normalización existe y la consumen `IAccountRepository` y `ResolveSignInUseCase` |

### 6.4 `GeometriaFactory-Infrastructure`

| Id | Punto abierto | Quién lo cierra | En qué evento se cierra (artefacto y sección) | Estado |
| --- | --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | [`../../../../../changelog.md`](../../../../../changelog.md), etapa `c` § «Decidido en esta etapa, y elevado al punto de control» |**Cerrado** el 2026-08-25 · **por lectura, en el corte de la 06 de la migración 10.0 → 13.3**: el producto **no estima**, y no es una decisión pendiente sino un hecho. `PRODUCT-INTAKE` §2 declara `equipo_n = 1`; `Mini-Plan.md` §1.2 declara que **no hay capacidad numérica y es deliberado**; y **ocho etapas se cerraron sin una sola estimación**. Se cierra **retirando el punto**, que `A3-Decisiones-Del-Product-Owner.md` §3 admite como cierre válido, con la figura de `ADR-14004` |
| PA-02 | **Los nombres definitivos de tipos y de espacios de nombres, y el criterio de nombrado del adaptador de cuentas** (`05` §11 `PA-01` y `PA-02`). El **identificador del cuarto puerto no se fija acá**: lo declara `GeometriaFactory-Application` y su ADR-06002 lo ató al punto de control de la etapa `a`. Convertido en trabajo como BT-06002 | El equipo en el punto de control de la etapa `a`, sobre la superficie de `GeometriaFactory-Application` | [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §6, glosario de correspondencia | **Cerrado** el 2026-08-20 · **A2, por lectura**: la norma fija el glosario y `src/` tiene **23 espacios de nombres** vivos con esos nombres |
| PA-03 | **Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros** (`05` §11 `PA-03`). El intake declara dos candidatas y **no elige**; `ADR-06004` fija la **forma** —parámetros versionados junto al valor derivado, sin valor por defecto silencioso— y el **criterio de elección**. **La decisión es de este proyecto de código** (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Infrastructure). Convertido en trabajo como BT-06003 | El equipo en la etapa `a`, aplicando el criterio de `ADR-06004` §7 | `src/GeometriaFactory.Infrastructure/Security/PasswordDerivation.cs` | **Cerrado** el 2026-08-20 · **A2b, por lectura**: elige **PBKDF2** y escribe su criterio; el parámetro entra por `PasswordDerivation:Iterations` |
| PA-04 | **Hasta dónde llega el conjunto de tipos reconstruibles** (`05` §11 `PA-04`). Los **seis** que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar, y **ninguna fuente enumera las clases de la actividad**. Convertido en trabajo como BT-06024 | El Product Owner, con la enumeración de las clases de la actividad | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |
| PA-05 | **El límite de tamaño del texto que se acepta** (`05` §11 `PA-05`). **No es de este proyecto de código decidirlo**: `ADR-06006` §2 decide que el motor **no impone límite propio** y el valor y su forma de rechazo los fija la categoría 05 de `GeometriaFactory-Api`, que ya lo tomó. **No se convierte en trabajo acá** | La categoría 05 de `GeometriaFactory-Api` | Ya reasignado | **Cerrado** |
| PA-06 | **Cómo se sostiene que la provisoria «no se repite»** (`05` §11 `PA-06`). `CU-06007` §10 adopta que la sostiene la impredecibilidad y **descarta** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas. Convertido en trabajo como BT-06025 | El Product Owner, para confirmarla o reemplazarla | `05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-…` §10 | **Cerrado** el 2026-08-20 · **A2, verificado abriendo el `CU`**: §10 adopta que la sostiene la impredecibilidad y descarta el registro de provisorias |
| PA-07 | **La frecuencia del respaldo y la fecha de última modificación de la cuenta** (`05` §11 `PA-07` y `PA-09`). La primera la fuente la declara «a definir por el docente»; la segunda **entraría por el dominio y no por acá**. Convertidos en trabajo como BT-06026 | El Product Owner, con `09-Devops` y con `GeometriaFactory-Domain` | **Falta declarar el evento** | **No conforme con §12.2**: sin evento de cierre, nada lo puede vencer. **A declarar por el Product Owner** |
| PA-08 | ~~**La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`**~~ (`05` §11 `PA-10`). **CERRADO**: el Product Owner la **confirma tal como está**, con el fundamento que la categoría 02 había declarado. Este backlog no cambia: su prueba sigue siendo parte de BT-06021 | **Cerrado** por el Product Owner, `PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5 | **Resuelto** el **2026-08-12** | **Cerrado** |
| PA-09 | **Los valores rotulados [ASUNCIÓN]** de `05` §8 —los 200 ms y las **tres** coberturas—, pendientes en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-11`). Convertido en trabajo como BT-06023 | El Product Owner sobre su propio documento | `09-Devops/Estrategia-Versionado.md` y `09-Devops/Pipeline-CI-CD.md`, §ubicación de las puertas | **Cerrado** el 2026-08-26 · **decisión del Product Owner**, sobre [`../../../Audit/D1-Confirmacion-De-Asunciones.md`](../../../Audit/D1-Confirmacion-De-Asunciones.md) 1.0 y el intake §22: **confirma los valores rotulados `[ASUNCIÓN]`** —los umbrales de latencia y arranque de `A-5`, las coberturas y la forma de la pirámide de `A-3`, los gates que no son de líneas de `A-4` y las métricas de negocio de `A-2`—. **Con una excepción declarada y no confirmada: el caudal de 20 peticiones por minuto**, cuyo fundamento se cayó al cerrarse `D5` —el volumen de la comisión, incognoscible— y **cuyo valor definitivo sale de `PT-05`**, no de esta confirmación. Sigue **provisorio** y con su fila propia en `05` §8. **Confirmar no es medir**: los umbrales se eligieron altos donde la fuente señala criticidad, y ninguna fuente los fija |
| PA-10 | **De dónde sale el valor derivado del área de una pieza volumétrica.** `CU-06002` §10 adopta la **suma de los componentes** y lo declara como derivación, porque el intake la muestra dos veces así y una vez como fórmula, y las dos formas coinciden en el caso donde se cruzan. Convertido en trabajo como BT-06019 | `05-Arquitectura-Tecnica` ya fijó la tabla; el Product Owner puede confirmarla | `05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-…` §10 | **Cerrado** el 2026-08-20 · **A2, verificado abriendo el `CU`**: §10 declara que sale de **la suma de sus componentes**, como el intake lo muestra en E-1 |

**Diez filas: nueve abiertas y una resuelta, `PA-08`**, cerrada por `PRODUCT-INTAKE` **1.29** el 2026-08-12 y conservada con su desenlace y su fecha en lugar de retirarse.

**`PA-08` de la categoría 05 no figura acá porque está resuelto**: los dos recuentos de escenarios que aquella categoría había levantado quedaron corregidos en `PRODUCT-INTAKE` **1.18**, y son **ocho**. Este backlog usa ocho y **no reabre** el punto.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
| 3.0 | 2026-08-19 | **Migración normativa 9.12 → 10.0, fase M4.** Las **33** filas de puntos abiertos pasan a la forma de `Root-Rules.md` **§12.2**: la columna «Cuándo» —que nombraba **momentos**— se reemplaza por **«En qué evento se cierra (artefacto y sección)»** y entra la columna **«Estado»**. Un momento no deja rastro que alguien pueda abrir, y un cierre que nadie comprueba no ocurre. **Al nombrar el artefacto, 22 quedaron VENCIDAS**: su evento apunta a un punto de control de etapa ya cerrada o a la categoría 09 ya emitida. **5** quedan **sin evento declarado** —decían «sin fecha comprometida»— y §12.2 exige uno: **se marcan como no conformes y quedan para el Product Owner**, porque inventarles un evento sería exactamente lo que esta migración vino a impedir. **Ningún punto abierto se cierra acá y ninguno se inventa**: la migración los vuelve contables. Sube **major**: cambia la estructura de la tabla. | Orquestador de migración normativa SDD |
| 3.1 | 2026-08-20 | **Conversión de nomenclatura, `N-01`.** La columna que la fase M4 emitió como «Dónde se cierra» pasa a **«En qué evento se cierra (artefacto y sección)»**, que es como `Root-Rules.md` **7.0** §12.2 nombra literalmente su **campo 4**. No es cosmético: *«dónde»* nombra un **lugar** y el campo nombra un **evento**, y esa distinción es la que §12.2 existe para sostener. Entra además la **nota de correspondencia** de los cuatro campos con las cinco columnas, que declara que `Punto abierto` realiza los campos **1 y 2** juntos y que **`Estado` no es un campo de §12.2** sino un derivado de su tabla de escalamiento. **Se declara en lugar de partir la columna**, porque partirla obligaría a reescribir la prosa de las filas que `Informe-Migracion-9.12-a-10.0.md` `A7` verificó **idénticas**. **Ninguna fila cambia de contenido, de estado ni de recuento.** Plan en `Audit/Plan-Conversion-Nomenclatura-Item-Diferido.md`. Sube **minor**: cambia un rótulo y entra una nota; la estructura de la tabla no se toca. | Orquestador SDD |
| 3.2 | 2026-08-20 | **Paso `A2` del plan de cierre**: **7** punto(s) abierto(s) **cerrados por lectura del árbol**, cada uno con **cita al artefacto que ya tenía la decisión**. Ninguno se cerró por criterio propio: por la pregunta previa de `Master-Prompt.md` §8.1, una respuesta que se sostiene con cita literal **es trabajo propio y no detención**. Los que remitían a un caso de uso **se verificaron abriendo el `CU`**, que era la condición que `Clasificacion-Pendientes-A1.md` §4 puso: una fila que dice «el `CU` lo adopta» **no prueba que el `CU` lo diga**. **Ningún enunciado de punto abierto se tocó** y ninguna decisión se inventó. Sube minor. | Orquestador SDD |
| 3.3 | 2026-08-20 | **Segunda pasada del paso `A2`**: **4** punto(s) abierto(s) cerrados **por lectura del árbol**, sobre las familias que `Audit/A3-Decisiones-Del-Product-Owner.md` §1 dejó verificadas. Cada uno cita **el archivo que ya tenía la decisión** — el motor de dibujo anclado en `three 0.169.0`, `PBKDF2` en `PasswordDerivation.cs`, el `@media` de 768 px en `app.css`, `EmailIdentity.Normalize`, los 18 puntos de acceso, las herramientas de cada stage en los guiones, y **la biblioteca de componentes, que no existe porque la etapa `b` decidió no introducirla** y su `.csproj` lo declara como apartamiento. **Ninguno se cerró por criterio propio** y **ningún enunciado de punto abierto se tocó**. Sube minor. | Orquestador SDD |
| 3.4 | 2026-08-20 | **1** punto(s) abierto(s) **cerrados por decisión del Product Owner** del 2026-08-20, sobre `Audit/A3-Decisiones-Del-Product-Owner.md`: el **volumen de la comisión** queda cerrado **por incognoscible** —el dato no se sabe ni se puede saber de antemano, y no se fija número—; el **límite de tamaño del cuerpo** adopta **el valor por omisión del servidor**, con la obligación derivada de declararlo explícitamente cuando se toque la composición; y el **mutation score** se cierra **con un no**, dejando `CV-19` declarado sin medir. **Ningún enunciado de punto abierto se tocó.** Sube minor. | Orquestador SDD |
| 4.0 | 2026-08-25 | **Migración normativa 10.0 → 13.3, fase M4, corte de la categoría 06** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.1). **`PA-01`, la unidad de estimación, queda CERRADO POR LECTURA en las cuatro tablas** —no por decisión—: no era un pendiente sino un hecho. `PRODUCT-INTAKE` §2 declara **`equipo_n = 1`**, y de ese dato el framework deriva que la 07 emita sólo `Mini-Plan.md`; su §1.2 declara que **no se declara capacidad numérica y es deliberado**; y el contraste que lo cierra es que **ocho etapas se cerraron sin una sola estimación**. **Estaba VENCIDO** —diferido al punto de control de la etapa `c`, que cerró el **2026-08-14** sin registrarlo— y las 114 historias de este backlog lo citaban: con la forma nueva de `Rules-Backlog-Tecnico.md` **5.0** §4.4 punto 5.b habría entrado a cada una como hallazgo **P1**. Se cierra **retirando el punto**, que `Audit/A3-Decisiones-Del-Product-Owner.md` §3 admite como cierre válido, con la figura del ítem **sin objeto** de [`../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md). **La columna `Estimación` pasa de «Sin fijar» a «No aplica»** y §4.1 deja de titularse «por qué queda abierta». Estado previo archivado en [`_legacy/2026-08-25/Product-Backlog-v3.4.md`](_legacy/2026-08-25/Product-Backlog-v3.4.md). Sube **major**: el salto de la regla que lo gobierna es major. |
| 4.1 | 2026-08-25 | **Ronda 2 del corte de la 06**, sobre el audit independiente que lo aprobó **con hallazgos**. **La columna `Estimación` pasa de «No se estima» a «No aplica»** (**P2**): la prosa citaba entre comillas una forma que las celdas no tenían, y el destino quedaba con **tres** literales para una decisión única. **Y la frase que afirmaba haber cambiado también las tareas técnicas se vuelve verdadera** (**P1**): el cierre se propagó de verdad a `Backlog-Tecnico.md`, que la ronda 1 describió sin abrir. **Y una constancia sobre los recuentos del mensaje de entrega de la ronda 1**, que el audit refutó (**P3**) y que no se pueden editar allí: los estados previos archivados son **148** y no 152 —144 historias, 2 `Product-Backlog` y 2 `Mini-Plan`—, y los enlaces reescritos **1099** y no 694. |
| 4.2 | 2026-08-26 | **`A2c` · cierre por lectura de `D3`, la vigencia del acceso firmado.** `A3-Decisiones-Del-Product-Owner.md` §4 —«**`D3` puede estar fijada y no la encontré.** Busqué en la composición de la API y no en la configuración. **Si aparece, pasa a la lista de §1 y deja de ser tuya**»— dejó autorizado este cierre, y **apareció**: el valor en efecto es **480 minutos** por omisión de `SigningOptions.cs:25`, y **nada lo sobreescribe**. La fila **deja de afirmar que no hay número**, que era falso, y **deja de estar vencida**: su evento pasa a la **fase `i`**, que es cuando la configuración del ambiente se declara y **no ocurrió**. **Lo que queda abierto es otra pregunta y es del Product Owner**: si ocho horas cumplen el criterio de `ADR-00003` §5 —«que caduque **dentro de la sesión de trabajo de una clase**»—, porque **ninguna fuente declara cuánto dura una clase** y sin ese dato la comparación no se puede hacer leyendo. Se suma la **obligación derivada** con el mismo criterio con que el Product Owner cerró `D4`: el valor **se declara explícitamente**. Sube **minor**: reformula un punto abierto y no cambia ninguna decisión. |
| 4.3 | 2026-08-26 | **`D1` confirmada por el Product Owner el 2026-08-26**, sobre `Audit/D1-Confirmacion-De-Asunciones.md` 1.0 y el intake §22. Se confirman los valores rotulados `[ASUNCIÓN]` —umbrales de latencia y arranque (`A-5`), coberturas y forma de la pirámide (`A-3`), gates que no son de líneas (`A-4`) y métricas de negocio (`A-2`)—, con lo que **cuatro filas vencidas de este documento quedan cerradas**. **Con una excepción declarada: el caudal de 20 peticiones por minuto queda FUERA de la confirmación.** Su fundamento se cayó al cerrarse `D5` —el volumen de la comisión, incognoscible— y su valor sale de **`PT-05`**; sigue **provisorio**. **Y una constancia que la confirmación no compra**: los umbrales se eligieron altos donde la fuente señala criticidad, ninguna fuente los fija, y **confirmarlos no los vuelve medidos**. Sube **minor**: cierra puntos abiertos y no cambia ninguna decisión de contenido. |
