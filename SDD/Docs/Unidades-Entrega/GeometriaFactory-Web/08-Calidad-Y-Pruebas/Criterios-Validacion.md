# Criterios de validación — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Criterios-Validacion.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **3 secciones existen sólo en `GeometriaFactory-Visor`** —«Criterios de las puertas técnicas», «Criterios de regresión», «Criterios de calidad de código y de artefacto»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Propósito

### 1.1 `GeometriaFactory-Web`

Define qué significa que `GeometriaFactory-Web` está **validado**. A diferencia de los proyectos de código de biblioteca del producto, éste **sí es una unidad de entrega**: se publica en el hosting público y es el único punto de contacto del navegador. Por eso «validado» acá quiere decir **que la etapa puede demostrarse a la persona y publicarse sin dejar la aplicación caída**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante, y **el final del flujo de publicación**, que el intake §17.2.P.8 · GeometriaFactory-Web declara que no termina en la subida.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

### 1.2 `GeometriaFactory-Visor`

Define qué significa que `GeometriaFactory-Visor` está **validado**. Su artefacto es **un archivo de guion generado** que se copia al directorio de recursos estáticos de `GeometriaFactory-Web` y viaja dentro del despliegue de esa unidad, de modo que «validado» no quiere decir «publicado»: quiere decir **que las siete garantías se sostienen sobre el bundle generado y que las dos puertas técnicas pasan**.

Este documento tiene una sección que los otros dos proyectos de código de nivel topológico 0 no tienen: **§4, las puertas técnicas**. Es la consecuencia de que el intake §15 declare `PT-02` y `PT-03` sobre este proyecto de código, y de que el roadmap las ubique antes de comprometer la etapa `g`.

## 2. Criterios funcionales

### 2.1 `GeometriaFactory-Web`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **diez** casos de uso tienen al menos un caso de verificación pasado, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **10 de 10** |
| CV-02 | Las **treinta** historias de usuario tienen su caso de verificación | Matriz §2, columna de historias | **30 de 30** |
| CV-03 | Las **trece** restricciones transversales tienen caso de verificación | Matriz §5 | **13 de 13** |
| CV-04 | Las **dieciséis** reglas de negocio tienen verificado **lo que esta pieza hace por ellas**, y ninguna afirmación depende de que esta pieza las haga cumplir | Matriz §4 | **16 de 16** |
| CV-05 | **Toda acotación se verificó forzando la solicitud sin pasar por la pantalla**, y no mirando que el control no se dibuja | `TC-10001`, `TC-10005`, `TC-10007`, `TC-10015`, `TC-10025`, `TC-10026` | **6 de 6** casos ejecutados sobre las acotaciones vigentes |
| CV-06 | Los **diecisiete** códigos vivos del contrato **más** el camino de ausencia de respuesta tienen mensaje de superficie, y **ninguno** expone dirección, ruta de datos ni traza | `TC-10031` | **16 de 16** mensajes, con **0** exposiciones |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados **en su forma original y completa**, sin sustituirlos por datos sintéticos | `TC-10011` a `TC-10014`, `TC-10017` a `TC-10020`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | El escenario `E-1` produce **exactamente 3 piezas y 2 advertencias**, y el cilindro **no produce ninguna observación** | `TC-10013` | 3 y 2, con **0** observaciones del cilindro. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto** |

### 2.2 `GeometriaFactory-Visor`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **siete** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **7 de 7** |
| CV-02 | Las **seis** funciones de la fachada están ejercitadas | Matriz §2 y `TC-12015` | **6 de 6** |
| CV-03 | Las **siete** garantías del contrato de fachada tienen caso de prueba en verde | Matriz §5 | **7 de 7** |
| CV-04 | Los **siete** códigos de condición están cubiertos en sus **ocho** cursos, y **ninguno se acuña aguas abajo** | Matriz §6 y `TC-12021` | **7 de 7** códigos, **8 de 8** cursos, **0** acuñados |
| CV-05 | Las **catorce** historias tienen su caso de prueba | Matriz §2 cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.1 | **14 de 14** |
| CV-06 | Los **seis** tipos dibujables se dibujan, tres volumétricos y tres planos | `TC-12005`, con el texto del escenario `E-7` | **6 de 6** |
| CV-07 | Las variantes de clave del emisor se leen como sinónimos, y **el cero es una dimensión legible** | `TC-12006` y `TC-12007`, con los escenarios `E-2`, `E-7` y `E-6` | Las dos claves aceptadas; la figura de `E-6` **entre las dibujadas** |
| CV-08 | Los **ocho** escenarios del intake §20 están ejercitados **como texto**, sin sustituirlos por datos sintéticos | Verificación uno por uno de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-09 | **Ninguna regla de negocio se verifica en este proyecto de código** | Matriz §4 | **0 de 16**, y es el resultado correcto |

## 3. Criterios no funcionales

### 3.1 `GeometriaFactory-Web`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. El de los pasos del guion lleva su rótulo **[ASUNCIÓN]** porque así viene del intake en cuanto a su **forma de puerta**.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-09 | `PT-01.a`: el front publicado arranca y sirve la página inicial | **200** en la dirección pública | `TC-10034` | **Puerta técnica**: si no pasa, se baja la versión objetivo del front |
| CV-10 | `PT-01.b`: transporte del circuito | Semáforo; **amarillo aceptable** documentando la latencia percibida | `TC-10034` | **Puerta técnica**: sólo el rojo obliga a cambiar el modelo de front. **Un repliegue de mayor latencia no es motivo de rediseño** |
| CV-11 | `PT-01.c`: estabilidad del proceso | **20 minutos** continuos sin reciclado, con reconexión funcional | `TC-10034` | **Puerta técnica**: es el peor escenario y **no tiene mitigación en el código** |
| CV-12 | `PT-01.d`: salida hacia el backend | Una llamada de salud devuelve **datos reales** | `TC-10034` | **Puerta técnica**: si no pasa, publicar el servicio de datos en un puerto convencional |
| CV-13 | Pasos del guion de demostración de la etapa **y de todas las anteriores** | **100 %** **[ASUNCIÓN del intake §17.2.P.6 · GeometriaFactory-Web en cuanto a expresarlo como puerta; asunción `A-4` de §22, que declara que cambia la forma del gate y no su carácter bloqueante]** | `TC-10035` | **Bloqueante.** Lo rotulado [ASUNCIÓN] es **la forma de la puerta**, y §22 declara que un cambio del Product Owner no toca su carácter. **La regla acumulativa rige igual**: no es asunción de nadie |
| CV-14 | Peticiones del navegador hacia el servicio de datos | **0**, medidas **con los dos movimientos prendidos** | `TC-10029` | **Bloqueante, sin gradación**. Una medición hecha sin la condición **no cuenta como medición** |
| CV-15 | Salidas hacia el servicio de datos y bibliotecas de guion que consulten | **1** y **0** | `TC-10030` | **Bloqueante** |
| CV-16 | Apariciones de la credencial de sesión en el navegador | **0** | `TC-10003` | **Bloqueante**. Criterio de aceptación de la etapa `c` |
| CV-17 | Mensajes que exponen dirección, ruta de datos o traza | **0** sobre los diecisiete códigos y el camino de ausencia | `TC-10031` | **Bloqueante** |
| CV-18 | Tráfico de circuito durante la interacción con la escena | **0**, y el texto viaja **1** sola vez por trabajo | `TC-10033` | **Bloqueante** |
| CV-19 | Instancias del visor no liberadas tras **10** recorridos, con los dos movimientos prendidos | **0** | `TC-10021`, puerta `PT-02` | **Puerta técnica**: si no pasa, **detiene la planificación de la etapa `g`** y no se arrastra como deuda |
| CV-20 | Invocaciones al interior del bundle | **0**, con **6 de 6** funciones como única vía | `TC-10032` | **Bloqueante** |
| CV-21 | Elementos de la línea de base demostrados | **11 de 11** superficies, **73 de 73** componentes, **74 de 74** estados, **24 de 24** rutas y **29 de 29** campos | Las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Bloqueante al cierre de la etapa** para los elementos que la etapa toca |
| CV-22 | Advertencias de construcción | **0** | Etapa de construcción del flujo de publicación | **Bloqueante** |

**No hay criterio de cobertura de líneas ni de tiempo de respuesta, y las dos ausencias tienen fundamento declarado.** La primera, porque no hay proyecto de pruebas propio (intake §17.2.P.6 · GeometriaFactory-Web). La segunda, porque las tolerancias de **400 ms** son de **diseño de la espera** y no compromisos de tiempo de respuesta (`05` §8 y `PA-04` de su §11). **Inventar cualquiera de las dos sería inventar una medición sin sujeto o un compromiso sobre un hosting cuya latencia la propia fuente declara incógnita.**

### 3.2 `GeometriaFactory-Visor`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. **Los seis primeros son las seis propiedades transversales de `02` §6 y llevan su condición de medición, que es vinculante.**

| Id | Criterio | Umbral | Condición de medición | Test |
| --- | --- | --- | --- | --- |
| CV-10 | Cero red | Exactamente **0** peticiones originadas por el archivo de guion | **Con los dos movimientos prendidos y sostenidos**, y durante los gestos de rotar y acercar | `TC-12016`, `TC-12018` |
| CV-11 | Cero persistencia | **0** claves escritas y ningún estado conservado entre páginas | Cualquier estado de los movimientos; recargar no repone la preferencia; la exclusión de claves ajenas se hace **por espacio de nombres declarado y no por prefijo** | `TC-12017` |
| CV-12 | Se ejercita sin backend | Recorrido de las **seis** funciones con **0** servicios del backend disponibles | Sin condición adicional | `TC-12015` |
| CV-13 | Disposición determinista | Dos procesados del mismo texto producen la **misma disposición** | **Se compara posición, no orientación**; vale con cualquier estado de los movimientos | `TC-12009` |
| CV-14 | Liberación de recursos | **10 recorridos** de ida y vuelta sin degradación | **Con los dos movimientos prendidos** | `TC-12004` |
| CV-15 | Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas con índice y código, y **0** sin registro | Sin condición adicional | `TC-12007` |
| CV-16 | Dependencias traídas de una red externa en tiempo de ejecución | Exactamente **0** | Página abierta sin acceso a redes externas | `TC-12019` |
| CV-17 | Superficie pública del bundle | **6** funciones, **1** nombre propio en el objeto global, **0** globales sueltas | Inspección del **bundle generado** | `TC-12018` |

**Una medición hecha sin su condición no cuenta como medición.** Es el criterio más importante de esta sección, y su fundamento está en `02` §6: sin condiciones declaradas, la prueba mediría el caso fácil y quedaría en verde sin haber ejercitado nunca un bucle de dibujo corriendo.

**No hay criterio de fluidez con umbral numérico**, y **esta categoría no lo inventa**. `05` §8 declara que la fuente no fija un valor y lo deja abierto como `PA-03`. Hasta que exista, la fluidez se verifica **de forma cualitativa declarada** junto con `PT-02`, y esa verificación cualitativa **no se reporta como si fuera un número**.

## 4. Criterios de regresión y de deriva

### 4.1 `GeometriaFactory-Web`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-23 | El guion de demostración se ejecuta **entero y acumulativo** al cerrar cada etapa: la de la etapa y las de todas las anteriores, **sin correcciones** | 100 % de los pasos escritos hasta ese momento |
| CV-24 | **Ningún paso que pasaba en la etapa anterior deja de pasar** sin justificación escrita en el informe de cierre | 0 regresiones sin justificar |
| CV-25 | Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca quedan con **estado y fecha actualizados**, y ninguna vuelve a `Sin verificar` sin que algo se haya regenerado | 61 filas con estado coherente con lo construido |
| CV-26 | **Ninguna deriva mayor queda sin resolver.** Se corrige lo construido, o se actualiza la línea de base con aprobación humana explícita | 0 derivas mayores abiertas al cerrar la etapa |
| CV-27 | Toda deriva **menor** queda registrada aunque no bloquee | 100 % de las menores registradas |
| CV-28 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso de verificación por defecto cerrado, como mínimo |
| CV-29 | Las cinco inspecciones estructurales —`TC-10029` a `TC-10033`— se ejecutan en **todas** las etapas a partir de aquella en que su sujeto existe | Presentes en cada ejecución |

**La regla de no regresión es acumulativa por diseño y es la única red de seguridad que este proyecto de código tiene**, porque no tiene batería automatizada propia. El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, **sin correcciones**.

## 5. Criterios de calidad de código

### 5.1 `GeometriaFactory-Web`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-30 | Cobertura de líneas | **No aplica**: no hay proyecto de pruebas propio (intake §17.2.P.6 · GeometriaFactory-Web) | **No exigible.** Si en alguna etapa se agregan pruebas automatizadas de componentes, su umbral se fija en ese momento y se registra en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 |
| CV-31 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-10022` |
| CV-32 | Todo valor visual sale de un token del catálogo de diseño; no hay literales visuales ad hoc | 0 literales fuera del catálogo | **Bloqueante**, por la sonda `SD-10054` |
| CV-33 | Ningún instrumento de la maqueta ni valor compuesto para la maqueta llega al sistema construido | 0 instrumentos y 0 valores | **Bloqueante, sin gradación**, por las sondas `SD-10059` y `SD-10060` |
| CV-34 | Recorrido completo por teclado, foco visible y contraste de **4.5:1** en las once superficies | 11 de 11 | **Bloqueante**, por las sondas `SD-10051` y `SD-10052` |
| CV-35 | Ninguna superficie invoca al cliente tipado: entre una superficie y la salida hay siempre un servicio de aplicación de front | 0 invocaciones directas | **Bloqueante**, por `TC-10030`. Es lo que hizo posible la Fase B2 y lo que mantiene maquetable cada superficie |

## 6. Excepciones documentadas

### 6.1 `GeometriaFactory-Web`

**Un criterio no cumplido no se acepta en silencio.** Las cuatro únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| **Ningún criterio de este proyecto de código es condicionado.** `CV-10013` lleva un valor rotulado [ASUNCIÓN], pero lo rotulado es **la forma de la puerta** y el intake §22 `A-4` declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante» | **No hay salida admitida**: `CV-10013` no alcanzado **bloquea el cierre** como cualquier otro criterio bloqueante. Ejecutar sólo el guion de la etapa en curso no es una excepción admitida | El Product Owner, con constancia escrita, como en cualquier criterio bloqueante |
| Criterio **no exigible** —`CV-10030`— | Se declara «no aplica» con el fundamento citado. **No se reporta un número inventado** | — |
| **Puerta técnica** que no pasa —`CV-10009` a `CV-10012`, `CV-10019`— | **No hay excepción.** El intake §15 declara que una puerta que no pasa **detiene la planificación de las etapas que dependen de ella** y no se arrastra como deuda. La salida es la que cada puerta declara: bajar la versión objetivo del front, cambiar el modelo de front, publicar el servicio en un puerto convencional, o detener la etapa `g` | El Product Owner decide la salida, no la excepción |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** ejecutar el guion sólo de la etapa en curso; dar por verificada una acotación mirando que el control no se dibuja; dejar una deriva mayor en `Sin verificar` y seguir; contar peticiones del navegador **sin los dos movimientos prendidos**; sustituir un escenario del intake por un texto que dé el resultado esperado; publicar sin comprobar que la dirección pública responde.

### 6.2 `GeometriaFactory-Visor`

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| **Umbral de fluidez inexistente** | La verificación es **cualitativa declarada** junto con `PT-02`, y se registra como tal. **No habilita a inventar un número**, y así lo declara la excepción correspondiente de la DoR §3 | El Product Owner, o esta categoría al fijar su guion de medición (`BT-12018`) |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica con la remediación y **el momento no cierra** hasta que se cumpla | El Product Owner, con constancia escrita |
| **`PT-02` o `PT-03` que no pasan** | **Ninguna excepción.** La etapa `g` no se compromete. No se arrastra como deuda, no se difiere y no se convierte en condicionada | — |
| Medición de ausencia hecha **sin su condición** | **No se admite.** No cuenta como medición: mediría el caso fácil | — |
| Historia que introduce comportamiento en la capa 3 que rompe una garantía | **No se admite**, y es la misma prohibición que la DoR §3 declara del lado de la entrada: perder una garantía es cambio mayor aunque las seis firmas no se toquen | — |

**Lo que no es una excepción admitida:** acuñar un código de condición fuera de la categoría 02, editar el bundle a mano, medir cero red con los movimientos apagados, o reportar la fluidez con un número que ninguna fuente da.

## 7. Criterios de las puertas técnicas

### 7.1 `GeometriaFactory-Visor`

Las dos puertas las declara el intake §15 y §17.2.P.8 · GeometriaFactory-Visor. **Esta sección no las redefine, no las relaja y no les agrega criterios**: declara con qué caso de prueba se mide cada tramo.

| Id | Puerta y tramo | Umbral | Test |
| --- | --- | --- | --- |
| CV-18 | **`PT-03`** · El motor de dibujo tridimensional queda **dentro** del bundle | El motor dentro; **0** dependencias de red externa en tiempo de ejecución | `TC-12019` |
| CV-19 | **`PT-03`** · La página funciona **sin acceso a redes de distribución externas** | La fachada se ejerce entera sin ese acceso | `TC-12019` |
| CV-20 | **`PT-02`** · El bundle carga en una página del anfitrión y la creación de instancia **arma la escena** | Carga y escena viva | `TC-12020` |
| CV-21 | **`PT-02`** · La carga del texto dibuja las **tres** figuras de `E-1`, **ortoedro incluido** | 3 de 3 | `TC-12020`, `TC-12005` |
| CV-22 | **`PT-02`** · **Diez** recorridos de ida y vuelta **no degradan** | 10 sin degradación, **con los dos movimientos prendidos** | `TC-12020`, `TC-12004` |
| CV-23 | **`PT-02`** · El árbol y la escena **se sincronizan por índice** | Sincronización verificada en los dos sentidos | `TC-12020`, `TC-12011` |

**Las dos puertas son vinculantes y no admiten carácter condicionado.** Una que no pasa **detiene la planificación de la etapa `g`** y no se arrastra como deuda. Es el mismo fundamento con el que el Product Owner promovió `F-13` a `Must Have` en el intake **1.19**: una capacidad citada por una puerta técnica deja de ser diferible, y `CV-12023` es exactamente esa capacidad.

## 8. Criterios de regresión

### 8.1 `GeometriaFactory-Visor`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-24 | La batería completa se ejecuta entera al cerrar cada momento del producto, y no sólo los casos que el momento tocó | 100 % de los `TC-XX` escritos hasta ese punto |
| CV-25 | **Ningún caso de prueba que estaba en verde pasa a rojo** sin justificación escrita | 0 regresiones sin justificar |
| CV-26 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 por defecto cerrado, como mínimo |
| CV-27 | Las **seis** propiedades transversales se reverifican **después** de incorporar el gobierno en vivo de los movimientos, y no sólo antes | 6 de 6 reverificadas en la etapa `g` |
| CV-28 | `TC-12005` —las tres figuras de `E-1` con el ortoedro dibujado— se ejecuta en **todos** los momentos a partir de la medición de puertas | Presente en cada ejecución. Es la regresión del defecto original: hoy, en el visualizador previo, **ningún ortoedro generado por la aplicación se dibuja** |

## 9. Criterios de calidad de código y de artefacto

### 9.1 `GeometriaFactory-Visor`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-29 | La regla de dependencias entre capas se respeta: la capa 1 no conoce el interior, la capa 2 **no contiene lógica de dibujo** y la capa 3 no conoce al anfitrión | 0 violaciones | **Bloqueante** |
| CV-30 | El bundle **nunca se editó a mano**: es un artefacto generado y reproducible | 100 % generado | **Bloqueante** |
| CV-31 | El motor de dibujo **nunca se expone al anfitrión** | 0 exposiciones | **Bloqueante** |
| CV-32 | **Cobertura de líneas: no aplica como criterio.** El intake §17.2.P.6 · GeometriaFactory-Visor fija el gate de inspección de cero red **en lugar de** la cobertura de líneas | — | **No aplicable, declarado** |
| CV-33 | **Mutation score: no aplica.** No hay forma de matar los mutantes del código de dibujo sin recurrir a la comparación de imágenes, que [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 descarta con su fundamento | — | **No aplicable, declarado** |
| CV-34 | **Snapshot de la escena: no aplica.** Una comparación de imágenes sería frágil y **no distinguiría un cambio legítimo de orientación de una deriva de posición**, cuando el determinismo comprometido es de posición | — | **No aplicable, declarado** |

**Las tres «no aplicable» se declaran en lugar de omitirse.** Un lector que no encuentre cobertura de líneas ni mutation score en un proyecto de código de tipo `library` tiene que poder leer por qué.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **5 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
