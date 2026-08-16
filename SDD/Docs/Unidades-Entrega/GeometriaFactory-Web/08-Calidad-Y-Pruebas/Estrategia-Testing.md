# Estrategia de testing — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Estrategia-Testing.md
**Versión:** 2.0
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

## 1. Pirámide de testing deseada

### 1.1 `GeometriaFactory-Web`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `web-monolith` la distribución **70 / 20 / 10** entre unitario, integración y extremo a extremo. Este proyecto de código **se aparta del reparto y declara el motivo**, que no es de esta categoría sino del intake: §17.2.P.6 · GeometriaFactory-Web declara que **no tiene proyecto de pruebas propio** en el árbol del repositorio y que su verificación es **el guion de demostración de cada etapa**, ejecutado en el navegador del equipo anfitrión y acumulativo por la regla de no-regresión, más las pruebas de integración que ejercitan el servicio que consume.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | — | **0 %** | **No aplica hoy, y se declara así en lugar de omitirse.** No hay proyecto de pruebas propio. El intake §17.2.P.6 · GeometriaFactory-Web lo deja abierto en una sola dirección: «si en alguna etapa se agregan pruebas automatizadas de componentes, su cobertura mínima se fija en ese momento y se registra». Hasta que eso ocurra, un porcentaje unitario sería una medición sin sujeto |
| Integración | Las pruebas que ejercitan el servicio de datos que esta pieza consume | **0 % acá** | Existen y son necesarias, pero **son de `GeometriaFactory-Api`**: `GeometriaFactory.Integration.Tests` pertenece a ese proyecto de código (intake §17.1.P.6 · GeometriaFactory-Api). Esta pieza las consume como contexto y no las posee |
| Extremo a extremo observado | **El guion de demostración**, acumulativo, ejecutado en el navegador; las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); y las **tres** puertas técnicas | **100 %** | Es lo que la fuente declara como verificación de este proyecto de código, y es lo único que puede observar lo que la persona observa |

**El apartamiento es de forma y no de rigor, y tiene una consecuencia que conviene decir sin adornos.** Un guion observado es más caro de ejecutar y menos reproducible que una batería automatizada: por eso esta estrategia lo compensa con **dos instrumentos que sí son enumerables y verificables uno por uno** —las 61 filas de la matriz de sensado y los **35** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md)—, y con **seis inspecciones estructurales** cuyo umbral es exactamente cero y que no dependen de que alguien mire bien.

**Contra la pirámide invertida**: acá la pirámide **es** de extremo a extremo, y no por descuido sino por decisión de la fuente. Lo que la mantiene sana es que las propiedades críticas —`RA-01`, `RA-02`, `RA-03`, la credencial fuera del navegador— **no se verifican mirando la pantalla** sino contando y forzando: `TC-10029` a `TC-10033`. **Contra la pirámide aplanada**: §2 no reporta ningún número global de cobertura, porque no hay ninguno que reportar.

**Tres clases de verificación que conviene nombrar aparte:**

- **Paso de guion.** Una acción de la persona en el navegador, con su resultado observable. Se ejecuta acumulativamente: el guion de la etapa **y los de todas las anteriores**.
- **Inspección estructural.** Comprueba una propiedad del árbol de fuentes o del tráfico observado, con umbral cero: peticiones del navegador, salidas hacia el servicio de datos, invocaciones al interior del bundle, mensajes que exponen la topología.
- **Verificación forzando la solicitud.** La que comprueba una acotación **sin pasar por la pantalla**. Es obligatoria porque esta pieza **no hace cumplir reglas** (`02` §5): que un control no se dibuje no prueba nada, y la Definition of Ready §1 criterio 7 lo exige explícitamente.

### 1.2 `GeometriaFactory-Visor`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5**. Este proyecto de código la adopta **con una redistribución declarada**, porque su verificación central no es unitaria: es la medición de propiedades sobre el **bundle generado** corriendo en una página.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Lector del texto y disposición derivada del índice: dos componentes de la capa 3 que son transformación pura de entrada a salida | **45 %** | Baja respecto del 80 de la regla porque **la mayor parte de lo que hay que verificar no es una función pura**: es una escena viva, un bucle de dibujo y un contexto gráfico |
| Integración | La fachada sobre el registro de instancias y el servicio de dibujo: ciclo de vida completo de una instancia, sin el navegador real cuando la comprobación no lo exige | **20 %** | Es donde se verifican `G-4`, `G-7` y la mayor parte de los siete códigos |
| Extremo a extremo en página | El recorrido de las **seis** funciones sobre una página real con capacidad gráfica: el sample **S-1** y la página del anfitrión de `PT-02` | **25 %** | **Sube muy por encima del 5 de la regla**, y es el apartamiento principal. Sin una página real no se pueden medir ni los diez recorridos, ni la cuenta de peticiones con el bucle corriendo, ni la liberación del contexto gráfico |
| Inspección del artefacto generado | Recuentos sobre el bundle: funciones expuestas, identificadores globales, ocurrencias de las tres formas de petición, claves escritas | **10 %** | Es el gate que el intake §17.2.P.6 · GeometriaFactory-Visor pone **en lugar de** la cobertura de líneas |

**El apartamiento es doble y está fundado.** El nivel unitario baja de 80 a 45 y el de extremo a extremo sube de 5 a 25. El motivo no es comodidad sino que **las propiedades que este proyecto de código compromete no son verificables sin una escena viva**: `02` §6 declara para cuatro de las seis propiedades transversales una condición de medición que exige un bucle de dibujo corriendo. Una batería mayoritariamente unitaria mediría el caso fácil, que es exactamente lo que esa sección viene a impedir.

**Snapshot: no aplica, y se declara.** La salida de este proyecto de código es una escena tridimensional; una comparación de imágenes sería frágil, dependiente del hardware gráfico y **no distinguiría un cambio legítimo de orientación de una deriva de posición**, cuando el determinismo comprometido por `G-6` es de **posición y no de orientación**. Lo que reemplaza al snapshot es la comparación de dos procesados pieza por pieza.

## 2. Cobertura mínima por capa

### 2.1 `GeometriaFactory-Web`

**No hay umbral de cobertura de líneas, y no es una omisión.** El intake §17.2.P.6 · GeometriaFactory-Web declara un **«gate bloqueante y numérico en lugar de cobertura de líneas»**, y el intake §22 lo rotula como asunción `A-4` en cuanto a su forma. Lo que sí se cubre, y se cuenta, es esto:

| Dimensión | Unidad de cobertura | Umbral | Fuente |
| --- | --- | --- | --- |
| Pasos del guion de demostración | Paso ejecutado y pasado | **100 %** de la etapa y de todas las anteriores [ASUNCIÓN del intake §17.2.P.6 · GeometriaFactory-Web en cuanto a la forma de la puerta] | Intake §17.2.P.6 · GeometriaFactory-Web; `05` §8 |
| Superficies de la línea de base | Superficie sensada | **11 de 11** | [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) §4, filas `SD-10001` a `SD-10011` |
| Componentes de la línea de base | Componente sensado | **73 de 73** | Ídem, tabla de cobertura de §4 |
| Estados de la línea de base | Estado sensado | **74 de 74** | Ídem |
| Rutas de la línea de base | Ruta sensada | **24 de 24** | Ídem |
| Campos del contrato de datos de maqueta | Campo sensado | **29 de 29** | Ídem |
| Casos de uso | Caso de uso con verificación | **10 de 10** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Restricciones transversales | Restricción con verificación | **13 de 13** | Matriz §5 |
| Códigos vivos del contrato | Código traducido a presentación **sin exponer nada** | **15 de 15** | Matriz §3, `TC-10031` |
| Superficies con recorrido por teclado y contraste medido | Superficie | **11 de 11** | `SD-10051` y `SD-10052` de la matriz de sensado |

**Los tres componentes de capa 1, los tres de capa 2 y los dos de capa 3 de `05` §3.1 no llevan umbral numérico propio**, y decirlo es más honesto que repartir un porcentaje inventado entre ocho módulos que ninguna herramienta va a medir. Lo que sí tiene cada componente es **al menos un caso de verificación que lo ejerce**, y eso sí se declara en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6.

**Si en alguna etapa se agregan pruebas automatizadas de componentes**, su cobertura mínima se fija en ese momento y se registra acá con su fila de control de cambios. Es la única puerta que el intake §17.2.P.6 · GeometriaFactory-Web deja abierta, y esta estrategia la deja abierta igual, sin anticipar un número.

### 2.2 `GeometriaFactory-Visor`

La partición es por los **seis** componentes de `05` §3.1, de los cuales **dos no son de este proyecto de código**.

| Componente | Capa | Métrica | Umbral |
| --- | --- | --- | --- |
| Componente anfitrión | 1, **fuera de este proyecto de código** | — | No aplica: vive en `GeometriaFactory-Web` y su cobertura es de la categoría 08 de ese proyecto de código |
| Fachada plana | 2 | Funciones ejercitadas; garantías sostenidas | **6 de 6** funciones con al menos un caso de prueba; `G-3` y `G-7` verificadas |
| Registro de instancias | 2 | Cursos del ciclo de vida del identificador | 100 % de los cursos: identificador válido, ya liberado, e inexistente. `G-4` verificada |
| Lector del texto | 3 | Tipos dibujables y variantes de clave del emisor | **6 de 6** tipos; las variantes `Tapas` y `Bases` aceptadas como sinónimos; **el cero como dimensión legible** |
| Servicio de dibujo | 3 | Propiedades transversales que lo alcanzan | `G-5`, `G-6`, disposición determinista y liberación de recursos |
| Motor de dibujo tridimensional | 3, **empaquetado** | — | **No se prueba por dentro**, y es deliberado: probarlo ataría este proyecto de código a un motor concreto y lo volvería irreemplazable, que es lo contrario del punto de extensión ([`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md)) |
| **Bundle generado** | — | Recuentos de superficie y de ausencias | **6** funciones, **1** nombre propio en el objeto global, **0** globales sueltas, **0** peticiones, **0** claves, **0** dependencias de red externa |

**No hay umbral de cobertura de líneas, y su ausencia está declarada aguas arriba.** El intake §17.2.P.6 · GeometriaFactory-Visor fija un «gate bloqueante y verificable por inspección, **en lugar de cobertura de líneas**», y lo enuncia como **cero ocurrencias de las tres formas de petición** —que el intake nombra una por una— **en el código fuente del proyecto de código y en el bundle generado**. La sustitución de los tres nombres por su descripción es de este documento, por la convención del corpus de no nombrar tecnologías, y por eso queda **fuera de las comillas** [ASUNCIÓN en cuanto a expresarlo como gate automatizable; la regla es de `RA-02` y ya es criterio de aceptación de la etapa `g`]. **El rótulo [ASUNCIÓN] alcanza a la forma del gate, no a la regla**, y esta categoría lo cita con esa precisión.

**No hay mutation score**, y su ausencia se declara en lugar de omitirse: `Rules-Calidad-Y-Pruebas.md` §2.2 lo pide para `library`, pero acá la mayor parte del valor está en propiedades medidas sobre una escena viva, y mutar el código de dibujo produciría mutantes que sólo una comparación de imágenes podría matar —justamente la técnica que §1 descarta con su motivo—. **Es la única exigencia de §2.2 que este proyecto de código no cumple.**

## 3. Tooling

### 3.1 `GeometriaFactory-Web`

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a` (intake, regla de anclaje de versiones), y la biblioteca de componentes de interfaz está explícitamente **[A VERIFICAR]** en la fuente (`05` §11 `PA-01`).

| Propósito | Herramienta, por su función |
| --- | --- |
| Ejecución del guion de demostración | El navegador del equipo anfitrión, con su panel de herramientas de desarrollo abierto. **No es un marco: es una persona ejecutando pasos**, y declararlo así evita que se lea como automatización |
| Conteo de peticiones y de tráfico de circuito | Pestaña de red del panel de herramientas de desarrollo |
| Inspección del almacenamiento y de las marcas de sesión | Panel de aplicación del navegador |
| Inspección estructural del árbol de fuentes | Búsqueda sobre el repositorio y revisión del archivo de proyecto y del manifiesto de dependencias de guion |
| Recorrido por teclado y lectura asistida | Navegación por teclado sin ratón, y un lector de pantalla del sistema |
| Medición de contraste | Herramienta de medición de contraste sobre los pares de color del catálogo de diseño |
| Verificación forzando la solicitud | Un cliente de peticiones que arma la solicitud **sin pasar por la pantalla**, contra el servicio de datos, con la credencial de una sesión válida |
| Comprobación de la dirección pública | El paso final del flujo de publicación, que verifica que la dirección responde |

**No se nombra ningún producto comercial.** La biblioteca de componentes es la única pieza que la fuente nombra, y su versión es un punto abierto declarado.

### 3.2 `GeometriaFactory-Visor`

Nombrado por función, según la convención de las categorías 02, 03 y 05 de este proyecto de código.

| Propósito | Herramienta, por su función |
| --- | --- |
| Unit e integración | Marco de pruebas del entorno de ejecución de la cadena de herramientas, sólo en tiempo de construcción |
| Extremo a extremo en página | Un conductor de navegador con capacidad gráfica tridimensional, capaz de contar peticiones de red y de leer el almacenamiento del navegador |
| Inspección del bundle generado | Comprobación reproducible de texto sobre el archivo de guion producido, más lectura de los identificadores que expone en el objeto global |
| Página integradora sin backend | El sample **S-1** del intake §18 y §16.1, que es a la vez ejemplo y material de prueba |
| Construcción | El guion propio del bundle, para el ciclo corto, y el guion general encadenado (intake §17.2.P.8 · GeometriaFactory-Visor) |

**El motor de dibujo tridimensional y el empaquetador se nombran por su función y no por su producto**, que es la convención que `05` §2.2 declara con su fundamento: el motor es reemplazable por diseño y nombrarlo en cada documento haría más caro reemplazarlo.

## 4. Especificaciones Given-When-Then

### 4.1 `GeometriaFactory-Web`

**Los criterios de aceptación de las treinta historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos dos escenarios ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio.

**Y hay una razón adicional propia de este proyecto de código**: los pasos del guion de demostración ya son, en la práctica, especificaciones ejecutables observadas. Duplicarlos en un formato de escenario produciría **tres** enunciados del mismo criterio —la historia, el guion y el archivo de escenario— y ninguno sería la fuente.

### 4.2 `GeometriaFactory-Visor`

Los criterios de aceptación de los **siete** casos de uso ya están escritos, y las **catorce** historias los llevan inline en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3, con la exigencia de la DoR §1 criterio 3.

**Decisión de esta categoría: no se adopta un juego de archivos de escenario ejecutables.** Los criterios viven en las historias y en los casos de uso; cada `TC-XX` los transcribe citando su origen.

**La forma característica de los criterios de este proyecto de código es el umbral cero con su condición de medición**, y conviene decirlo porque cambia cómo se escribe cada aserción. La DoR §2 criterio 3 ya lo exige del lado de la entrada: «cuando la propiedad que sostienen es una **ausencia**, el criterio se expresa con umbral cero y con la condición en que se mide. Un criterio de ausencia sin condición de medición no está listo: mediría el caso fácil». **Esta categoría lo hereda del lado del cierre**: un `TC-XX` de ausencia sin condición de medición es un caso de prueba mal escrito.

## 5. Mocks y fixtures

### 5.1 `GeometriaFactory-Web`

**Política de dobles: ninguno del lado del servicio de datos, a partir de la etapa `c`.** El guion de demostración se ejecuta contra el servicio de datos real levantado en el contenedor de desarrollo, porque lo que verifica es el recorrido completo de la persona. Un doble del servicio de datos convertiría el guion en una demostración de la maqueta.

**Lo que sí se sustituye, y es la única sustitución admitida:**

| Sustitución | Cuándo | Por qué |
| --- | --- | --- |
| **Servicio de datos caído** | `TC-10027`, `TC-10028`, y las filas `SD-10014` de la matriz de sensado | El estado degradado y la reconexión **no se pueden observar** con el servicio disponible. Se provoca deteniendo el servicio, no simulándolo |
| **Circuito cortado y restablecido** | `TC-10027`, y `PT-01.c` | Ídem: se provoca cortando la red del navegador |
| **Preferencia de movimiento reducido declarada por el sistema** | `TC-10022`, y `SD-10048` | Se declara en el sistema operativo o en el navegador. **No se simula desde el código**, porque lo que se verifica es que esta pieza la **lea** y la traduzca a dos valores de verdad |
| **Sin capacidad gráfica tridimensional** | `TC-10023`, y `RT-11` | Se deshabilita la capacidad en el navegador, para verificar que el resto del producto sigue disponible |

**Los datos de las etapas `b` y anteriores** salen de [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md), y ese documento declara en su §5 **valores compuestos para la maqueta que no se propagan al producto** —la credencial de prueba y la cuarta cuenta de ejemplo—. La fila `SD-10060` de la matriz de sensado los sensa: **un dato compuesto para la maqueta que figure como dato del producto es deriva mayor**.

### 5.2 `GeometriaFactory-Visor`

**Sin dobles del motor de dibujo.** Sustituirlo por un doble haría que las pruebas verificaran el doble y no la escena, y perdería exactamente lo que hay que medir: el contexto gráfico, su liberación y el bucle.

**Un solo doble admitido, y con condición**: el conductor de navegador puede simular la **preferencia de movimiento reducido** del sistema. La fachada no la consulta —hacerlo violaría `G-3`—, de modo que lo que se simula es el entorno del anfitrión y no una dependencia del bundle. Es lo que permite verificar que la prueba **puede prender los dos movimientos aunque el entorno declare la preferencia**, que es la condición sin la cual la medición de cero red quedaría en verde sin ejercitar el bucle.

Fixtures declarados:

| Fixture | Qué contiene | De dónde sale |
| --- | --- | --- |
| Texto del escenario `E-1` | Tres piezas: `Cilindro`, `Cubo` y `Ortoedro`, con la clave `Bases` en el ortoedro | Intake §20.E-1, transcripto íntegro |
| Texto del escenario `E-7` | Seis piezas que cubren los seis tipos dibujables, tres volumétricos y tres planos | Intake §20.E-7 |
| Texto del escenario `E-2` | Un ortoedro con la clave `Tapas` y con dos comas finales | Intake §20.E-2 |
| Texto del escenario `E-8` | Un ortoedro dibujable y un cubo con dimensión no legible | Intake §20.E-8 |
| Texto del escenario `E-6` | Una figura plana con una dimensión en `0.00` | Intake §20.E-6 |
| Texto del escenario `E-5` | Dos figuras, la segunda con tipo fuera del conjunto conocido | Intake §20.E-5 |
| Elemento de dibujo de tamaño cero | Una superficie de dibujo sin tamaño, para los dos cursos de `ELEMENTO_DE_DIBUJO_INVALIDO` | Compuesto por esta categoría; **no es un dato de geometría** y no sustituye ningún escenario |

## 6. Datos de prueba

### 6.1 `GeometriaFactory-Web`

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado. §21 los cruza contra la batería obligatoria de **nueve** casos de prueba de RT §11, más un décimo que esa misma sección agrega.

**Cómo los usa este proyecto de código.** Acá los escenarios entran **como texto que la persona pega en el formulario de envío**, que es exactamente la forma en que el alumno los produce. Es el único proyecto de código del producto donde el escenario se usa en su forma original y completa, carácter por carácter: los demás reciben su interpretación ya producida.

| Escenario | Qué verifica en esta pieza | Fuente del valor |
| --- | --- | --- |
| `E-1` | **3 piezas y 2 advertencias**; las tres figuras se dibujan en la escena; procesar dos veces produce la **misma disposición**. Es el texto semilla y el caso canónico | §20.E-1, «Qué verificar» puntos 5 y 7; `SD-10037`, `SD-10038`, `SD-10041` |
| `E-2` | **El texto se envía carácter por carácter con sus dos comas finales**, y se muestra sin reescribirlo. Es el material de `RN-10008` y de la fila `SD-10036` | §20.E-2, punto 1; `SD-10036` |
| `E-3` | La advertencia de área con el par **36.00 declarado y 54.00 derivado**, mostrada **exactamente como llega, sin reformatear** | §20.E-3, punto 2; `SD-10033` |
| `E-4` | **Cero observaciones**: la lista de observaciones se dibuja como línea explícita y no como hueco | §20.E-4, punto 4; `SD-10021` |
| `E-5` | El error con **índice de figura 1** y **campo `Tipo`**, nunca un texto genérico; y **ninguna pieza desaparece sin quedar enumerada** | §20.E-5, puntos 1 a 4; `SD-10030`, `SD-10040` |
| `E-6` | La pieza con dimensión **`0.00` se dibuja** y no produce condición de dibujo | §20.E-6, puntos 1 y 4; `SD-10039` |
| `E-7` | Los **seis** tipos dibujables, con la clave `Bases` en el ortoedro | §20.E-7, puntos 1 a 3; `SD-10040` |
| `E-8` | La pieza no se dibuja y **queda enumerada con su índice y su código**; **el árbol muestra las dos piezas**, incluida la que no se dibujó; y el desenlace del envío **es error**: el trabajo queda en `Borrador` | §20.E-8, puntos 2, 5 y 6 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un dato de prueba de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización.

**Lo que no se inventa.** Ningún caso de verificación introduce un texto de figuras que no esté en §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un comentario del administrador— se usa un valor evidentemente ficticio y se declara como tal, con la salvedad de `SD-10060`: **los valores compuestos para la maqueta no viajan al producto**.

### 6.2 `GeometriaFactory-Visor`

**Los datos de geometría de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, su procedencia y su estado declarado; §21 los cruza contra la batería obligatoria de **diez** casos de prueba —los **nueve** de la fuente técnica más el **décimo** que esa misma sección agregó el 2026-08-09 para la dimensión no legible— y declara, en su tabla de cobertura de invariantes, que **el contrato de fachada tiene sus siete condiciones con escenario** en `E-1` a `E-8`.

**Este proyecto de código sí recibe el texto**, a diferencia de los otros dos de nivel 0: `cargarJson` lo procesa. Por eso los ocho escenarios entran acá **como texto y no como resultado**.

| Escenario | Qué ejercita de este proyecto de código | Fuente |
| --- | --- | --- |
| `E-1` | Se dibujan **las tres figuras**, **ortoedro incluido**, y procesar el mismo trabajo dos veces produce la **misma disposición**. Es material declarado de `02` §6 y lo que `PT-02` mide | §20.E-1, punto 7 |
| `E-2` | La clave `Tapas` como sinónimo de las bases: **en el visor, el ortoedro se dibuja**. Hoy, en el visualizador previo, ningún ortoedro generado por la aplicación se dibuja | §20.E-2, punto 8 |
| `E-3` y `E-4` | El cubo de lado 3 con caras `Cuadrado` y con caras `Rectangulo`. Para la fachada los dos se dibujan igual: el campo que se usa es `Largo`. **La fachada no emite ninguna observación** sobre el área declarada: eso es del backend | §20.E-3 punto 1 y §20.E-4 punto 1 |
| `E-5` | Una figura con tipo fuera de los seis dibujables: **no se dibuja y queda enumerada** con su índice y `TIPO_NO_DIBUJABLE`; la primera, válida, se dibuja igual | §20.E-5, punto 3, leído desde el lado del dibujo |
| `E-6` | Una dimensión en `0.00`: **la figura se dibuja**, porque el cero es una dimensión legible y lo que produce `DIMENSION_NO_LEGIBLE` es la **ausencia** de la clave. Que una figura de dimensión cero no se vea **no es una falla del validador ni de la fachada** | §20.E-6, puntos 1 y 4; contrato de fachada §5.3 |
| `E-7` | Los **seis** tipos dibujables como piezas del conjunto raíz; el ortoedro con ancho 6, profundidad 4 y altura 8; y **todo esto sin backend**, con **0 peticiones** originadas por el bundle | §20.E-7, puntos 1 a 5 |
| `E-8` | El ortoedro del índice 0 **se dibuja** y la pieza del índice 1 **no**, reportada con **índice 1**, código `DIMENSION_NO_LEGIBLE` y el campo `Largo`. **El código es `DIMENSION_NO_LEGIBLE` y no `JSON_INVALIDO`**: confundirlos es el error que este escenario detecta | §20.E-8, puntos 1 a 3 |

**Los ocho escenarios están alcanzados y ninguno se sustituye.** `E-8` es además el que cierra el hueco que la versión 1.5 del intake dejó abierto: hasta entonces `DIMENSION_NO_LEGIBLE` era la única de las siete condiciones del contrato **sin escenario propio en §20 ni fila en §21**.

**Una precisión de frontera que esta estrategia hereda y no relaja.** `E-8` punto 4 declara que **el visor informa por qué no dibujó una pieza y que decidir si el trabajo pasa a `Pendiente` es del validador, no del bundle**. Ningún caso de prueba de este proyecto de código verifica el desenlace del envío: eso pertenece a `GeometriaFactory-Domain` y a `GeometriaFactory-Infrastructure`.

## 7. Ambiente de testing

### 7.1 `GeometriaFactory-Web`

| Aspecto | Decisión |
| --- | --- |
| Dónde corre el guion | En el **navegador del equipo anfitrión**, contra la aplicación levantada desde el contenedor de desarrollo (intake §17.2.P.6 · GeometriaFactory-Web) |
| Dónde corren las puertas | `PT-01` contra el **hosting público**, porque lo que mide son las capacidades de ese hosting y no las del contenedor. `PT-02` y `PT-03` en el navegador del equipo anfitrión |
| Servicio de datos | **Real y levantado**, no simulado, a partir de la etapa `c`. Su indisponibilidad se provoca deteniéndolo |
| Estado propio | **Ninguno que preparar.** `RT-06` declara que esta pieza no guarda estado propio: ni copia local, ni caché, ni réplica. No hay almacén que sembrar de este lado |
| Secretos | La dirección del servicio de datos viene de configuración y **la dirección real del servidor propio no se versiona**. Ningún secreto entra al repositorio |
| Aislamiento entre verificaciones | Cada paso del guion arranca desde una sesión conocida. Los pasos **sí tienen orden**, porque el guion es un recorrido; lo que no puede haber es dependencia de una ejecución anterior **de otra etapa** |
| Duración | **No se declara ningún tiempo de ejecución del guion ni de la suite.** Ninguna fuente lo da. Los únicos tiempos declarados son los **20 minutos** de `PT-01.c` —que es una duración de la prueba, no un plazo— y las tolerancias percibidas de 400 ms, que `05` §8 declara **de diseño de la espera y no compromisos de tiempo de respuesta** |

### 7.2 `GeometriaFactory-Visor`

| Aspecto | Decisión |
| --- | --- |
| Dónde se construye | Dentro del contenedor de desarrollo; el gestor de paquetes corre ahí (intake §17.2.P.1 · GeometriaFactory-Visor) |
| Dónde se ejecuta lo de extremo a extremo | Un navegador con **capacidad gráfica tridimensional**. Sin ella el visor no es soportado y la fachada informa `CAPACIDAD_GRAFICA_AUSENTE`, que es en sí mismo un caso de prueba |
| Runtime en ejecución | **Ninguno propio**: en tiempo de ejecución no hay entorno de la cadena de herramientas, hay un archivo servido como recurso estático (`05` §5) |
| Backend | **Ninguno, y es una propiedad exigida.** El recorrido completo se hace con **0 servicios del backend disponibles** (`02` §6) |
| Preferencia de movimiento reducido | Se simula en el conductor, según §5. Las mediciones de ausencia se hacen **con los dos movimientos prendidos** |
| Aislamiento | Cada prueba crea sus instancias y las destruye. Dos instancias vivas no comparten nada (`G-4`), de modo que el paralelismo es admisible |
| Duración | **No se declara ninguna.** Ninguna fuente da un tiempo de ejecución para la batería de este proyecto de código, y esta categoría no lo inventa. Lo único con umbral temporal declarado es la ausencia de degradación tras **diez** recorridos, que se cuenta en recorridos y no en segundos |

## 8. Relación con la matriz de sensado de deriva ya emitida

### 8.1 `GeometriaFactory-Web`

**[`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) ya existe en esta carpeta desde la Fase B2, en versión 1.2, y esta Fase E no la reemplaza, no la duplica y no la reescribe.** La emitió AG-03M al cerrar la Fase B2 con la maqueta aprobada por el Product Owner, y su propia §2 declara que era **el único artefacto de la categoría emitido por esa fase** y que «cuando AG-08 genere la categoría, incorpora esta matriz en lugar de crear una nueva». Esto es exactamente eso.

**Qué hace esta Fase E con ella, y qué no hace:**

| Acción | Estado |
| --- | --- |
| Incorporarla como artefacto vigente de la categoría, listado en [`README.md`](README.md) §1 | **Hecho** |
| Resolver el **método de verificación** de sus filas, que es lo que su §1 le asigna al cierre de la Fase E | **Hecho en §8.1**, por familia de filas y con el `TC-XX` que la ejerce |
| Exigir su actualización al cerrar cada etapa, con estado y fecha | **Hecho** en [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 y en [`Definition-Of-Done.md`](Definition-Of-Done.md) §1.3 |
| Convertirla en gate | **Hecho**: `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Modificar sus filas, sus umbrales o su recuento de línea de base, que sigue siendo **61** | **No se hace.** Un umbral de deriva se cambia con aprobación humana explícita, no desde esta categoría |
| Abrir filas nuevas para la capacidad **F-26** | **No se hace**, y el motivo lo declara la propia matriz en su §4: los elementos de interfaz que arrastra esa capacidad **no tienen identificador en la línea de base**, porque son posteriores a la aprobación de la maqueta. Sus sondas nacen con la **iteración 5** de maqueta y la reemisión de la línea de base. **Que no tengan sonda no significa que no se verifiquen**: `TC-10006`, `TC-10007` y `TC-10010` de esta categoría las cubren contra los criterios de aceptación de `CU-10003` y `CU-10004`, que es lo que la propia matriz declara que gobierna esa construcción mientras tanto |
| Abrir filas `VER-XX` | **No se hizo en la Fase E, y quedó hecho el 2026-08-11 por AG-10**: en ese momento `10-Examples` no estaba emitida y la matriz lo declaraba. Al emitirse, la matriz sumó la fila **`SD-10062`** desde el único contrato de verificación de [`../10-Examples/`](../10-Examples/), en `Sin verificar`. **El alta no es de esta categoría**: `Deriva-Rules.md` §4 se la asigna a AG-10 en el momento que cierra la categoría 10, y las **61** filas que esta categoría sí resolvió en §8.1 no cambian |

**La frontera entre los dos instrumentos, dicha una sola vez.** La matriz de sensado responde «¿lo construido se sigue pareciendo a lo que el humano aprobó mirando, y sigue respetando el contrato?». El catálogo de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) responde «¿el sistema hace lo que las historias dicen?». Cuando los dos miran lo mismo, la matriz aporta el **umbral de deriva** y el caso de prueba aporta el **criterio de aceptación**; ninguno de los dos reemplaza al otro y **ninguno redefine el umbral del otro**.

### 8.1 Resolución del método de verificación por familia de filas

Las **61** filas de la matriz agrupadas por su método resuelto. La agrupación es la que la propia matriz declara en su §3, y esta tabla **no cambia ninguna fila**: declara con qué se ejerce cada familia y en qué etapa entra.

| Familia | Filas | Cuántas | Método resuelto | Casos de verificación que la ejercen | Etapa en que entra |
| --- | --- | --- | --- | --- | --- |
| Superficies | `SD-10001` a `SD-10011` | 11 | **Inspección visual** contra la maqueta aprobada, superficie por superficie | `TC-10035`, y los `TC-XX` de la superficie correspondiente | `b`, y se reverifica cuando la superficie recibe capacidad |
| Familias de estados | `SD-10012` a `SD-10022` | 11 | **Conmutación de estado** en el sistema construido, contra la tabla de la línea de base | `TC-10004`, `TC-10013`, `TC-10014`, `TC-10015`, `TC-10024`, `TC-10027`, `TC-10028` | `c` a `h`, según la superficie |
| Familias de rutas | `SD-10023` a `SD-10027` | 5 | **Recorrido completo**, más revisión de la tabla de rutas del sistema construido para `SD-10027` | `TC-10005`, `TC-10007`, `TC-10026`, `TC-10035` | `b` para el mapa, `c` a `h` para los recorridos |
| Modelo de datos y formatos | `SD-10028` a `SD-10036` | 9 | **Inspección visual con escenario del intake**, más comparación carácter por carácter para `SD-10036` | `TC-10011`, `TC-10013`, `TC-10014`, `TC-10018` | `e` y `f` |
| Comportamiento verificable por ejecución | `SD-10037` a `SD-10042` | 6 | **Ejecución automatizable** con los escenarios `E-1`, `E-5`, `E-6` y `E-7`, y los **10** recorridos de ida y vuelta | `TC-10013`, `TC-10014`, `TC-10017`, `TC-10020`, `TC-10021` | `f` y `g` |
| Contrato de fachada y movimiento | `SD-10043` a `SD-10048` | 6 | **Inspección del árbol de fuentes** más **conteo en la pestaña de red**, e inspección visual para los controles | `TC-10022`, `TC-10029`, `TC-10032`, `TC-10033` | `g` |
| Accesibilidad | `SD-10049` a `SD-10053` | 5 | **Recorrido por teclado**, **lectura asistida** y **medición de contraste**, las tres observadas | `TC-10019`, `TC-10023`, y revisión de accesibilidad de las once superficies | `b` para el armazón, `g` para el árbol y la escena |
| Tokens, ancho angosto y sello | `SD-10054` a `SD-10056` | 3 | **Revisión de las hojas de estilo** e **inspección visual** en ancho angosto | `TC-10035` | `b` |
| Barridos de microcopy | `SD-10057`, `SD-10058` | 2 | **Barrido de los mensajes visibles**, con umbral 0 | `TC-10031` | `c` |
| Barridos de residuos de maqueta y componentes transversales | `SD-10059` a `SD-10061` | 3 | **Barrido del sistema construido** buscando cada instrumento y cada valor compuesto, e inspección superficie por superficie para `SD-10061` | `TC-10035`, y la revisión de la etapa `b` | `b`, y se reverifica al cerrar `h` |

**Once más once más cinco más nueve más seis más seis más cinco más tres más dos más tres son sesenta y una.** El recuento cierra contra el de la matriz, y ninguna fila queda sin método resuelto ni sin etapa asignada.

**Cuántas quedan como inspección observada y cuántas se automatizan.** Las **seis** filas de `SD-10037` a `SD-10042` y las de conteo de `SD-10043`, `SD-10045` y `SD-10047` admiten ejecución automatizable; **las demás quedan como inspección**, y decirlo es más honesto que declarar una automatización que este proyecto de código no tiene proyecto de pruebas donde alojar.

### 8.2 Correspondencia con la matriz de `GeometriaFactory-Visor`

`GeometriaFactory-Visor` emitió su propia matriz de sensado en su Fase E y declaró en su §4 una tabla de correspondencia contra ésta, «para que ningún elemento se sense dos veces con umbrales distintos». **Esta categoría la verificó fila por fila desde este lado y la confirma**: las **ocho** correspondencias que esa tabla declara son verdaderas contra el texto de las filas citadas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2.

| Fila de la matriz del `Visor` | Fila de ésta que la correspondencia cita | Verificación desde este lado |
| --- | --- | --- |
| `SD-10001` seis funciones de la fachada | `SD-10043` | **Verdadera.** `SD-10043` afirma que la escena se opera **exclusivamente por las seis funciones** desde el componente anfitrión |
| `SD-10002` cero red | `SD-10043` | **Verdadera.** `SD-10043` incluye el recuento de peticiones en la pestaña de red **durante la interacción con la escena** |
| `SD-10003` cero persistencia | `SD-10047` | **Verdadera.** `SD-10047` afirma que **la preferencia de cada movimiento es del componente anfitrión** y que la fachada no escribe ninguna clave |
| `SD-10006` sin fallo silencioso | `SD-10039` y `SD-10040` | **Verdadera.** `SD-10039` sensa que la pieza con dimensión `0.00` **se dibuja**; `SD-10040` sensa que el recuento de piezas sin registro es **0** |
| `SD-10007` determinismo | `SD-10041` y `SD-10045` | **Verdadera.** `SD-10041` compara dos cargas del mismo texto; `SD-10045` compara las disposiciones en las **cuatro** combinaciones de movimiento |
| `SD-10009` siete códigos en ocho cursos | `SD-10018` | **Verdadera.** `SD-10018` sensa los **ocho** estados que materializan las **siete** condiciones del contrato y que **usan los códigos sin renombrarlos** |
| `SD-10011` gobierno del movimiento | `SD-10044`, `SD-10046` y `SD-10048` | **Verdadera.** `SD-10044` sensa los dos controles independientes; `SD-10046`, la reposición de la orientación de partida; `SD-10048`, el arranque destildado con preferencia de movimiento reducido declarada |
| `SD-10012` puertas `PT-02` y `PT-03` | `SD-10042` | **Verdadera en lo que afirma, y parcial en su alcance.** `SD-10042` sensa los **diez recorridos de ida y vuelta sin degradación**, que es lo que la correspondencia le atribuye. **Lo que esa fila no cubre es la otra puerta de la correspondencia, `PT-03`** —que el motor de dibujo quede **dentro** del bundle y la página funcione **sin acceso a CDN externos**, que es como el intake §17.2.P.8 · GeometriaFactory-Visor define `PT-03`—, que es propiedad del bundle y **se sensa sólo del lado del `Visor`**. La correspondencia no afirma lo contrario: dice «acá las dos puertas enteras, allá los diez recorridos» |

**No hay doble sensado con umbrales distintos.** Las filas de esta matriz se anclan en identificadores de línea de base **validados visualmente**; las de la matriz del `Visor` se anclan en elementos del **contrato de la fachada**. Cuando las dos miran lo mismo, lo miran desde lados distintos, y los umbrales que declaran son compatibles: **deriva mayor sin gradación en las dos** para cero red, cero persistencia, fallo silencioso, determinismo y las puertas técnicas.

**Un punto que conviene dejar escrito porque las dos matrices lo tratan.** La **sexta función**, `establecerMovimiento`, se incorporó al contrato **después** de que el Product Owner aprobó la maqueta y **no fue validada visualmente**; por eso `SD-10043` de esta matriz la sensa **contra el contrato y no contra la maqueta**, y así lo dice su columna de umbral y la nota al pie de su §4. La matriz del `Visor` lo declara del mismo modo. **Las dos coinciden y ninguna afirma que la maqueta la haya validado.**

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
