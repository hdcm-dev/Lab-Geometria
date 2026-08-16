# Estrategia de calidad — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Estrategia-Calidad.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6 y §22
**Trazabilidad downstream:** `09-Devops` y `11-Documentacion`
**Consolida a:** los documentos homónimos de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase, y sus cuatro proyectos de código tenían
el suyo.** Cada sección lleva **una subsección por proyecto**, con su texto **transpuesto sin
reescritura**: lo que cambia es el orden y no el contenido.

**Las cuatro capas declaran la misma estructura de calidad con contenidos distintos**, y las cinco
secciones de este documento lo muestran capa por capa. Lo que no se promedia son los umbrales: viven
en [`Estrategia-Testing.md`](Estrategia-Testing.md) §0.1, con los cuatro pisos juntos.

---

## 1. Definición de calidad para este proyecto de código

### 1.1 `GeometriaFactory-Api`

`GeometriaFactory-Api` tiene calidad cuando **ningún punto de acceso queda fuera de la guardia que le corresponde**, cuando **ninguna traducción a protocolo deshace una decisión ya tomada adentro** y cuando **el servicio no atiende una sola petición sobre un almacén que no está en condiciones**.

Las tres partes describen el mismo peligro desde tres ángulos: **acá es donde una decisión correcta de una capa de adentro se puede perder sin que nada falle**. `05` §9 lo declara con precisión en su primer riesgo —un punto nuevo fuera de la guardia hace que `RN-00013` e `INV-09` dejen de valer **y nada falla**— y en el segundo —un trabajo ajeno que responde «no autorizado» confirma la existencia de un recurso ajeno, y **ninguna capa de adentro puede repararlo**—.

**Este es además el proyecto de código donde vive la batería de integración del producto.** El intake §17.1.P.6 · GeometriaFactory-Api declara que `GeometriaFactory.Integration.Tests` golpea **la superficie real por su protocolo contra el almacén real**, y §17.1.P.6 · GeometriaFactory-Infrastructure le asigna a esa batería la persistencia real de `GeometriaFactory-Infrastructure`. La consecuencia para esta categoría es doble: su pirámide está **invertida a propósito**, y **lo que acá se rompe no lo cubre ninguna otra batería del producto**.

### 1.2 `GeometriaFactory-Domain`

`GeometriaFactory-Domain` tiene calidad cuando **ninguna de las dieciséis reglas de negocio y ninguno de los nueve invariantes puede violarse invocando su superficie pública**, y cuando cada rechazo que produce viaja como una de las **42** condiciones catalogadas, con su código estable y sin efecto parcial sobre la entidad.

Es una definición estrecha a propósito. Este proyecto de código no atiende peticiones, no abre conexiones y no persiste nada ([`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) §17.1.P.3 · GeometriaFactory-Domain, P.4 y P.10), de modo que no hay disponibilidad, latencia ni throughput que medir acá. Lo único que puede fallar es que una guarda no esté, que esté en un solo componente y no en el otro, o que un rechazo llegue como excepción en lugar de como valor.

La consecuencia operativa es que **la calidad de este proyecto de código se mide entera con pruebas unitarias puras y con inspecciones**, y que un defecto suyo no se descubre en un ambiente: se descubre en una prueba que falla o en una revisión que rechaza.

### 1.3 `GeometriaFactory-Application`

`GeometriaFactory-Application` tiene calidad cuando **las cuatro comprobaciones de autorización se ejercen en todos los caminos que las alcanzan y en el orden fijo declarado**, cuando **cada uno de los once casos de uso se puede ejercer entero con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso**, y cuando toda negativa prevista viaja como una de las **36** condiciones catalogadas, con su código estable y sin efecto parcial sobre la unidad de trabajo.

Las tres partes de esa definición no son intercambiables. La primera es la que sostiene `INV-02`, `INV-03` e `INV-09`; la segunda es la propiedad estructural que justifica el estilo entero del proyecto de código ([`../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md`](../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md)) y es lo que hace que la primera sea verificable sin ambiente; la tercera es la que impide que un rechazo se convierta en un fallo silencioso aguas arriba.

La consecuencia operativa es que **la calidad de este proyecto de código se mide entera con pruebas unitarias con dobles y con inspecciones**. No hay ambiente donde descubrir un defecto suyo: se descubre en una prueba que falla o en una revisión que rechaza. La batería de integración del producto existe, pero **no es de esta capa**: vive en `GeometriaFactory-Api` (intake §17.1.P.6 · GeometriaFactory-Application).

### 1.4 `GeometriaFactory-Infrastructure`

`GeometriaFactory-Infrastructure` tiene calidad cuando **el validador interpreta el texto real del alumno tal como su programa lo emite, con sus cuatro trampas de formato, y señala sin corregir y sin rechazar**; cuando **ninguna operación del almacén deja efecto parcial ni pierde el texto original**; y cuando **los dos mecanismos que el producto no puede permitirse mal hechos —la derivación de credenciales y la emisión del acceso firmado— fallan hacia el rechazo y nunca hacia un valor adivinable ni hacia una firma improvisada**.

Las tres partes tienen un rasgo común que conviene decir de una vez: **acá los defectos no se notan.** Una provisoria adivinable no se nota hasta que alguien la usa; un acceso emitido sin clave no se nota hasta que alguien lo falsifica; un almacén recreado en lugar de transformado deja el servicio impecable y **sin los trabajos de nadie**; y un validador escrito sin leer el análisis funciona con datos inventados y falla con el dato que existe. `05` §9 declara **cinco** riesgos de impacto muy alto o alto con ese perfil.

La consecuencia operativa es que esta categoría **no confía en la ausencia de síntomas**: cada una de esas propiedades tiene un caso de prueba con umbral numérico, y la mayoría de esos umbrales es exactamente **cero**.

## 2. Atributos de calidad priorizados

### 2.1 `GeometriaFactory-Api`

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica** | Exactamente **4** puntos de acceso fuera de la guardia, **ni uno más**, verificado sobre los **quince** en las dos direcciones; **3 de 3** familias empobrecidas con respuestas indistinguibles en cuerpo y en código; **0** respuestas que expongan dirección, ruta, secreto o traza; **0** eliminaciones fuera de alcance aceptadas **al forzar la petición** (`05` §8) |
| Adecuación funcional | **Crítica** | **12 de 12** casos de uso con caso de verificación; **15 de 15** puntos de acceso ejercidos; **16 de 17** códigos del contrato con traducción declarada y **1** declarado **sin destino con su motivo**, con **0** inventados y **0** renombrados |
| Fiabilidad | **Crítica** | **0** peticiones atendidas con la preparación del almacén incompleta; **4 de 4** puertos conectados a su adaptador, con fallo en construcción si falta alguno; **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos |
| Eficiencia de desempeño | **Alta** | Percentil 99 del listado por debajo de **500 ms**, medido **en el servidor** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api]; caudal sostenido de **20 peticiones por minuto** [ASUNCIÓN]; arranque en frío en menos de **30 segundos** [ASUNCIÓN] |
| Mantenibilidad | **Alta** | **75 %** de líneas y **70 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Api]; pirámide de **60 %** integración y **40 %** unitarias [ASUNCIÓN], **invertida a propósito**; **1** sola configuración de intercambio declarada en el producto; **0** advertencias de construcción |
| Compatibilidad | **Media** | Los tipos que cruzan la frontera son los del ensamblado de contratos y **esta capa no agrega ni recorta campos**; sin versionado de rutas, porque no hay clientes de terceros |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false. Su equivalente es la experiencia del desarrollador que consume la superficie, y la **colección de peticiones reproducible** de `CU-00012` es su instrumento |
| Portabilidad | **Baja** | Plataforma única sobre el sistema operativo del contenedor, con la imagen final llevando **sólo el entorno de ejecución** y sin linaje con la imagen de desarrollo (intake §17.1.P.9 · GeometriaFactory-Api) |

**Este es el único proyecto de código del producto con `tiene_observabilidad_critica` == true** (`PRODUCT-MANIFEST` §5), y el motivo está declarado: es el único que declara un percentil con métrica numérica. **No hay atributo de disponibilidad**, y es correcto: el intake declara «sin SLO», el servidor es domiciliario y la caída se responde con **estado degradado en el front**, no con redundancia.

### 2.2 `GeometriaFactory-Domain`

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los dos valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | 100 % de los **trece** casos de uso con al menos un caso de prueba por criterio de aceptación; 100 % de los **nueve** invariantes con prueba de violación rechazada, sin dobles ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8) |
| Fiabilidad | **Crítica** | 100 % de las **42** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) alcanzadas por al menos una prueba, y **0** condiciones producidas por la biblioteca que no figuren en el catálogo (`05` §8) |
| Mantenibilidad | **Alta** | **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización (`05` §8); **0** advertencias de construcción (intake §17.1.P.8 · GeometriaFactory-Domain) |
| Eficiencia de desempeño | **Media**, y sólo de construcción | Batería de dominio completa en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain]. No hay métrica de runtime porque no hay runtime propio |
| Seguridad | **Baja como implementación, alta como regla** | El proyecto de código no deriva ni compara credenciales: la contraseña llega ya derivada (intake §17.1.P.5 · GeometriaFactory-Domain). Lo que sí se verifica es `INV-06` e `INV-09`, que condicionan el acceso |
| Compatibilidad | **Media** | La superficie pública es contrato para `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`; su estabilidad la gobierna [`ADR-02003`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false. Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../03-UX-UI-DX/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.1.P.9 · GeometriaFactory-Domain). No hay matriz de plataformas que probar |

**Los dos atributos críticos son los que justifican la existencia de este proyecto de código.** El intake declara que sus invariantes son «la última defensa de las reglas» (§17.1.P.6 · GeometriaFactory-Domain), y esa frase es la que fija la prioridad: si una guarda falla acá, ninguna capa de más arriba la repone.

### 2.3 `GeometriaFactory-Application`

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los dos valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | 100 % de los **once** casos de uso con al menos un caso de prueba por criterio de aceptación de sus historias; **4 de 4** comprobaciones de autorización con prueba de su negativa ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8) |
| Seguridad | **Crítica, como autorización y no como mecanismo** | La cuarta comprobación corta antes que las otras tres, con **1** prueba dedicada a ese orden (`05` §8). Esta capa no compara contraseñas ni emite accesos: la contraseña llega ya derivada y la provisoria ya producida (intake §17.1.P.5 · GeometriaFactory-Application) |
| Fiabilidad | **Crítica** | 100 % de las **36** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §7.1 alcanzadas por al menos una prueba, y **0** condiciones producidas por la capa que no figuren en el catálogo (`05` §8); **a lo sumo 1** unidad de trabajo por caso de uso, sin efecto repartido |
| Mantenibilidad | **Alta** | Exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web (`05` §8); **0** advertencias de construcción (intake §17.1.P.8 · GeometriaFactory-Application) |
| Eficiencia de desempeño | **Media** | El caso de uso más pesado —el envío que interpreta el texto semilla de **3** piezas del escenario `E-1`— resuelve en menos de **500 ms**, medido **sin acceso a base** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application]. **0** componentes de pieza cargados en las consultas de listado |
| Compatibilidad | **Media** | La superficie pública es contrato para `GeometriaFactory-Api`, y los **cuatro** puertos son contrato para `GeometriaFactory-Infrastructure`; su estabilidad la gobierna [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false (`PRODUCT-MANIFEST` §5). Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../03-UX-UI-DX/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.1.P.9 · GeometriaFactory-Application). No hay matriz de plataformas que probar |

**Los tres atributos críticos se sostienen entre sí.** El intake declara que la verificación de pertenencia existe porque «el rol no alcanza» (§17.1.P.5 · GeometriaFactory-Application), y `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca de cambio de contraseña pendiente. Esta estrategia trata esos dos enunciados como el eje de su prioridad.

### 2.4 `GeometriaFactory-Infrastructure`

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | **10 de 10** casos de la batería del validador con los **ocho** escenarios `E-1` a `E-8` como entrada (`05` §8 y §10.5); **10 de 10** casos de uso con caso de prueba; tolerancia **0.01** con operador **estricto**, que **no es asunción** |
| Seguridad | **Crítica** | **0** provisorias iguales en dos producciones consecutivas y entre cuentas; **0** emisiones de acceso sin clave de firma; **0** contraseñas guardadas o registradas en claro; **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno (`05` §8) |
| Fiabilidad | **Crítica** | **0** retiros parciales tras una baja interrumpida; **0** escrituras aceptadas que reemplacen el texto original conservado; **1 de 1** aplicación de transformaciones sobre almacén inexistente, sin paso manual |
| Eficiencia de desempeño | **Alta** | Interpretación del texto de **3** piezas de `E-1` en menos de **200 ms**, medida **sin almacén** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure]; **0** componentes de pieza y **0** apariciones del texto original en una proyección de listado |
| Mantenibilidad | **Alta** | **95 %** de líneas en el validador de figuras [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure], **el número más alto del producto** y puesto donde la fuente señala el criterio que más veces se rompe; **0** advertencias de construcción |
| Compatibilidad | **Media** | Implementa los **cuatro** puertos que `GeometriaFactory-Application` declara, y no los redefine; provee **dos** mecanismos y **una** responsabilidad de arranque que ningún puerto declara |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false (`PRODUCT-MANIFEST` §5). Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../03-UX-UI-DX/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.1.P.9 · GeometriaFactory-Infrastructure), con el motor de almacenamiento embebido y anclado en la etapa `a` |

**Los tres atributos críticos son los que el resto del producto no puede reparar.** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 lo dice con precisión: **tres reglas tienen su tramo principal acá** —`RN-06008`, `RN-06009` y `RN-06014`— y «si acá se hacen mal, ninguna capa de más adentro puede repararlas». Esa frase es la que fija esta prioridad.

## 3. Quality gates

### 3.1 `GeometriaFactory-Api`

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cinco primeros salen del intake §17.1.P.8 · GeometriaFactory-Api; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.1.P.8 · GeometriaFactory-Api) |
| QG-02 | El guion de pruebas pasa **entero**, **incluida la batería del validador** | Etapa `test` del pipeline | Bloquea la fusión. Ver §3.2 sobre el recuento de esa batería |
| QG-03 | La cobertura alcanza **75 %** de líneas y **70 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Api] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-04 | La pirámide del proyecto de código es **60 %** de integración y **40 %** unitarias [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Api] | Recuento de pruebas por clase en el informe de la etapa `test` (`TC-00037`) | **Condicionado**, ver §3.1 |
| QG-05 | Exactamente **4** puntos de acceso quedan fuera de la guardia de admisión, **ni uno más**, sobre los **quince** | `TC-00007`, inspección en las dos direcciones | **Bloquea la fusión.** Es el primer riesgo de `05` §9: un punto nuevo fuera de la guardia hace que `RN-00013` deje de valer **y nada falla** |
| QG-06 | **16 de 17** códigos del contrato tienen traducción declarada, **1** está declarado **sin destino con su motivo**, y hay **0** inventados y **0** renombrados | `TC-00024` y `TC-00027`, comparación en las dos direcciones contra [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 | Bloquea la fusión |
| QG-07 | **3 de 3** familias empobrecidas dan respuestas **indistinguibles en cuerpo y en código** | `TC-00025` | Bloquea la fusión. Es el segundo riesgo de `05` §9, y **ninguna capa de adentro puede repararlo** |
| QG-08 | **0** respuestas que expongan dirección de servicio, ruta de datos, secreto o traza, sobre los **quince** puntos **y** sobre el registro del servidor | `TC-00026` | Bloquea la fusión. Es `RA-03` |
| QG-09 | **0** caracteres de diferencia entre el texto enviado y el guardado, y **0** truncamientos silenciosos | `TC-00019` | Bloquea la fusión. **Rechazar, nunca truncar** |
| QG-10 | **4 de 4** puertos conectados a su adaptador, con **0** sin adaptador o con más de uno; y **1** sola configuración de intercambio declarada en el producto | `TC-00028` y `TC-00029` | Bloquea la fusión, **con fallo en construcción** cuando falta un puerto |
| QG-11 | **0** peticiones atendidas con la preparación del almacén incompleta | `TC-00031` | Bloquea la fusión |
| QG-12 | **0** eliminaciones fuera de alcance aceptadas **al forzar la petición** contra esta superficie | `TC-00020` | Bloquea la fusión. Es **el único criterio de verificación del producto que la fuente exige ejercer forzando la petición**, y el intake §17.1.P.6 · GeometriaFactory-Api lo declara bloqueante |
| QG-13 | El arranque en frío aplica las transformaciones y responde salud en menos de **30 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api] | `TC-00033` | **Condicionado**, ver §3.1 |
| QG-14 | Percentil 99 del listado por debajo de **500 ms** medido en el servidor, y caudal sostenido de **20 peticiones por minuto** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api] | `TC-00034`, en la batería de integración | **Condicionado**, ver §3.1 |
| QG-15 | La colección de peticiones reproducible tiene **5 pasos o menos** y **0 datos de prueba inventados** | `TC-00035` | Bloquea el cierre de la etapa que la incorpora |

**Quince gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que declara los **diecisiete** NFR de este proyecto de código.

### 3.1 Qué significa que un gate esté condicionado

`QG-03`, `QG-04`, `QG-13` y `QG-14` son los cuatro gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para el percentil, el caudal y el arranque en frío, y `A-3` para la forma de la pirámide en cuanto viene de §17.1.P.6 · GeometriaFactory-Api—. `05` §11 los registra y esta estrategia adopta el tratamiento sin cambiarlo: **los valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática.

**Una precisión sobre `QG-04`.** Lo rotulado es **el reparto numérico**, no la decisión de invertir la pirámide: el intake §17.1.P.6 · GeometriaFactory-Api declara la inversión **a propósito**, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». Esa decisión no es asunción y no queda en suspenso.

### 3.2 La batería del validador que corre desde acá

El intake **1.20** declara en §17.1.P.8 · GeometriaFactory-Api que el guion de pruebas de este proyecto de código pasa **«incluidas las diez pruebas del validador»**. Esa batería es de `GeometriaFactory-Infrastructure` y **tiene diez casos**: el intake §21 los cruza en una tabla de **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**, y la Fase C de ese proyecto de código ya había resuelto la lectura en diez.

**Esta categoría aplicó diez y no bajó la batería a nueve para que coincidiera con la redacción de la puerta.** **Hasta 1.19** esa redacción decía nueve en **dos** gates —§17.1.P.8 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Api—, por ser anterior a la incorporación del décimo caso; **el intake la corrigió en 1.20**, junto con §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.11 · GeometriaFactory-Application y el encabezado de §21, sobre el hallazgo que levantaron esta categoría y la de `GeometriaFactory-Infrastructure`. El desenlace queda registrado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8, y la categoría 08 de `GeometriaFactory-Infrastructure` lo registra del mismo modo desde su lado.

### 3.3 Las puertas técnicas y la frontera del despliegue

Se declaran aparte de los gates porque su consecuencia es distinta: el intake §15 declara que **una puerta que no pasa detiene la planificación de las etapas que dependen de ella y no se arrastra como deuda**.

| Puerta | Qué mide | Dónde se mide | Qué condiciona |
| --- | --- | --- | --- |
| `PT-04` | Que la imagen se construya con su archivo de construcción **multietapa** y arranque desde el contenedor de desarrollo, aplique las transformaciones sobre un almacén vacío y **responda salud** | Etapa `a` | Que el artefacto del servidor propio se pueda construir y arrancar |
| `PT-05` | La premisa completa de la topología, en el **despliegue real** | Etapa `i`, fuera del tramo comprometido | El despliegue real. La fuente **recomienda no relegarla** |

**Y una frontera que esta categoría no cruza.** El intake §17.1.P.8 · GeometriaFactory-Api declara el despliegue **manual, por el docente**, y que **el agente entrega el archivo de construcción y el de composición y no ejecuta el despliegue**. En consecuencia, **ningún criterio de esta categoría se cumple ejecutando un despliegue**: lo que se verifica es que el artefacto se construya, arranque y responda, y el resto es una acción del Product Owner.

### 3.2 `GeometriaFactory-Domain`

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cinco primeros los declara el intake §17.1.P.8 · GeometriaFactory-Domain; los tres siguientes los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.1.P.8 · GeometriaFactory-Domain) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | La cobertura alcanza el mínimo declarado: **90 %** de líneas y **85 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain] | Informe de cobertura de la etapa `test` | **Condicionado**, ver §3.1 |
| QG-04 | El archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | Inspección del archivo de proyecto, en revisión y como prueba de inspección (`TC-02024`) | Bloquea la fusión. Es la propiedad que justifica el estilo entero ([`ADR-02001`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md)) |
| QG-05 | **100 %** de las **42** condiciones del catálogo alcanzadas por prueba, y **0** condiciones emitidas fuera del catálogo | Prueba de inspección en las dos direcciones (`TC-02023`) | Bloquea la fusión |
| QG-06 | **100 %** de los **nueve** invariantes con al menos una prueba que verifique su violación rechazada, **sin dobles de prueba** | Matriz invariante contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5, revisada al cerrar cada etapa (`TC-02026`) | Bloquea el cierre de la etapa |
| QG-07 | La batería completa termina en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain] | Duración total reportada por el ejecutor en la etapa `test` | **Condicionado**, ver §3.1 |
| QG-08 | Ninguna condición prevista viaja como excepción de control de flujo | Revisión de la superficie pública contra [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | Se rechaza en revisión aunque compile |

### 3.1 Qué significa que un gate esté condicionado

`QG-03` y `QG-07` son los dos gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para el tiempo de la batería—. [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/_fusion/Domain/Backlog-Tecnico.md) `BT-02015` declara el tratamiento y esta estrategia lo adopta sin cambiarlo: **los dos valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática. Un incumplimiento se trata como hallazgo del punto de control de la etapa y no como rechazo de la fusión.

### 3.3 `GeometriaFactory-Application`

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cuatro primeros los declara el intake §17.1.P.8 · GeometriaFactory-Application —que remite a §17.1.P.8 · GeometriaFactory-Domain y agrega uno propio—; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.1.P.8 · GeometriaFactory-Application, que remite a §17.1.P.8 · GeometriaFactory-Domain) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | La cobertura alcanza el mínimo declarado: **85 %** de líneas y **80 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Application] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-04 | **Ninguna prueba de esta capa toca la base de datos real.** El umbral es exactamente **0** | Prueba de inspección `TC-04026` y revisión del pull request | **Bloquea la fusión.** Es la puerta propia que el intake §17.1.P.8 · GeometriaFactory-Application declara: «si una lo hace, está mal ubicada y pertenece a integración» |
| QG-05 | El archivo de proyecto declara exactamente **1** referencia a otro proyecto de código del producto y **0** a bibliotecas de persistencia, transporte, serialización o marco web | Inspección del archivo de proyecto, en revisión y como prueba de inspección (`TC-04027`) | Bloquea la fusión. Es la propiedad que sostiene `QG-04` |
| QG-06 | **100 %** de las **36** condiciones del catálogo alcanzadas por prueba, y **0** condiciones emitidas fuera del catálogo | Prueba de inspección en las dos direcciones (`TC-04028`) | Bloquea la fusión |
| QG-07 | **4 de 4** comprobaciones de autorización con al menos una prueba de su negativa **sin base de datos**, y **1** sola prueba que verifique que la cuarta corta antes que las otras tres | Matriz comprobación contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5, revisada al cerrar cada etapa (`TC-04011`) | Bloquea el cierre de la etapa |
| QG-08 | **A lo sumo 1** unidad de trabajo por caso de uso, y **0** casos de uso que repartan su efecto entre dos | Inspección de los once orquestadores y `TC-04029`, con la baja de cuenta como caso testigo | Bloquea la fusión |
| QG-09 | **0** componentes de pieza cargados en el listado del alumno y en el de la comisión | `TC-04030`, sobre la proyección que devuelve la consulta | Bloquea la fusión |
| QG-10 | El caso de uso más pesado resuelve en menos de **500 ms** para el texto semilla de **3** piezas de `E-1`, medido sin acceso a base [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application] | Medición sobre la batería unitaria con doble del puerto de validación, en la etapa `test` | **Condicionado**, ver §3.1 |
| QG-11 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-04031` y revisión de la superficie pública contra [`ADR-04006`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Se rechaza en revisión aunque compile |

**Once gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que es la sección que declara los **nueve** NFR de este proyecto de código con su objetivo numérico. No se agregó ninguna puerta técnica: las cinco del producto —`PT-01` a `PT-05`— se miden en `GeometriaFactory-Web` y en `GeometriaFactory-Api`, y el intake §15 no le asigna ninguna a esta capa.

### 3.1 Qué significa que un gate esté condicionado

`QG-03` y `QG-10` son los dos gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para los 500 ms—. [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) `PA-05` y [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md) `BT-04018` declaran el tratamiento y esta estrategia lo adopta sin cambiarlo: **los dos valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática. Un incumplimiento se trata como hallazgo del punto de control de la etapa y no como rechazo de la fusión.

### 3.4 `GeometriaFactory-Infrastructure`

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cuatro primeros los declara el intake §17.1.P.8 · GeometriaFactory-Infrastructure; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.1.P.8 · GeometriaFactory-Infrastructure) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | **La batería del validador pasa entera: 10 de 10**, con los **ocho** escenarios como entrada | `TC-06001` a `TC-06010`, contra la tabla de `05` §10.5 | Bloquea la fusión. Ver §3.2 sobre el recuento |
| QG-04 | **Las transformaciones de esquema se aplican solas sobre un almacén inexistente**, sin paso manual | Etapa de verificación de transformaciones del pipeline, y `TC-06032` | Bloquea la fusión. Es criterio de aceptación de la etapa `c` (intake §17.1.P.8 · GeometriaFactory-Infrastructure) |
| QG-05 | La cobertura del proyecto de código alcanza **85 %** de líneas y **80 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-06 | La cobertura del **validador de figuras** alcanza **95 %** de líneas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure] | Informe de cobertura acotado a los **dos motores** | **Condicionado**, ver §3.1 |
| QG-07 | La comparación de valores usa tolerancia **0.01** absoluta con operador **estricto**: el escenario `E-1` da **exactamente 2** advertencias y no 3 | `TC-06009` | **Bloquea la fusión, y no es condicionado.** El intake §22 declara expresamente que la tolerancia **no es asunción**: sale de que el emisor redondea a 2 decimales |
| QG-08 | Los **dos motores** originan exactamente **0** peticiones de red | `TC-06014`, inspección de dependencias de los dos motores | Bloquea la fusión |
| QG-09 | **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y ninguna derivable del nombre, del correo ni de la fecha | `TC-06027` | Bloquea la fusión |
| QG-10 | **0** componentes de pieza cargados y **0** apariciones del texto original en una proyección de listado | `TC-06019` | Bloquea la fusión |
| QG-11 | **0** escrituras aceptadas que reemplacen el texto original conservado, y **0** retiros parciales tras una baja interrumpida | `TC-06016` y `TC-06021` | Bloquea la fusión |
| QG-12 | **0** emisiones de acceso sin clave de firma, y **0** claves generadas al vuelo | `TC-06030` | Bloquea la fusión |
| QG-13 | **100 %** de las **17** condiciones del catálogo alcanzadas por prueba, **0** emitidas fuera del catálogo, y **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno | `TC-06034` y `TC-06035`, comparación en las dos direcciones | Bloquea la fusión |
| QG-14 | La interpretación del texto de **3** piezas de `E-1` termina en menos de **200 ms**, medida **sin almacén** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure] | `TC-06015` | **Condicionado**, ver §3.1 |

**Catorce gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que declara los **catorce** NFR de este proyecto de código.

**Una puerta técnica del producto se mide en la etapa `a` de este proyecto de código**: [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2 asigna `PT-04` a su épica `EP-06001`. Su umbral y su consecuencia son los del intake §15 —una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**— y esta categoría no los mueve.

### 3.1 Qué significa que un gate esté condicionado

`QG-05`, `QG-06` y `QG-14` son los tres gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para las dos coberturas, `A-5` para los 200 ms—. `PA-11` de `05` §11 declara el tratamiento y esta estrategia lo adopta sin cambiarlo: **los tres valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática.

**Lo que no es condicionado, y conviene no confundir.** `QG-07` mide un número —**0.01**— que **no está rotulado [ASUNCIÓN]**: el intake §22 lo enumera entre «lo que NO es asunción», con su fundamento. Un gate condicionado por arrastre de ese número sería un error de lectura, y esta estrategia lo declara para que no ocurra.

### 3.2 La batería del validador tiene diez casos, y el intake lo dice desde 1.20

**Esta categoría aplica diez, y declara por qué.**

- El intake **§21** cruza la batería obligatoria contra los escenarios y su tabla tiene **diez** filas: las nueve de la fuente técnica original más **«Dimensión no legible → `E-8`»**, que la propia fila rotula **[DECISIÓN 2026-08-09]**.
- **Hasta la versión 1.19** el intake escribía «las **nueve** pruebas del validador» en §17.1.P.8 · GeometriaFactory-Infrastructure y en §17.1.P.8 · GeometriaFactory-Api, y «la batería obligatoria de nueve casos» en §17.1.P.6 · GeometriaFactory-Infrastructure: eran redacciones anteriores a la incorporación del décimo caso, que quedaron sin propagar. **En 1.20 los tres lugares dicen diez**, junto con §17.1.P.11 · GeometriaFactory-Application y el encabezado de §21.
- El intake **1.20 §17.1.P.6 · GeometriaFactory-Infrastructure** dice hoy «la **batería obligatoria de diez casos**», **§17.1.P.8 · GeometriaFactory-Infrastructure** «las **diez** pruebas del validador pasan» y **§17.1.P.8 · GeometriaFactory-Api** «incluidas las **diez** pruebas del validador» en el pipeline de `GeometriaFactory-Api`.
- [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8 y §10.5 ya habían resuelto la lectura antes de esa corrección: **la batería tiene 10 casos**, «los nueve obligatorios de la fuente más el décimo que §21 agregó con `E-8`».

**Esta categoría mantiene esa lectura, que la fuente ya confirmó.** La divergencia entre los gates y la tabla de §21 **está cerrada**: la levantó esta misma fase y el intake la corrigió en 1.20, de modo que no queda nada derivado al Product Owner por este motivo. **Lo que esta categoría no hizo, y sigue sin hacer, es bajar la batería a nueve para que coincidiera con la redacción de la puerta**: el décimo caso cubre `E-8`, que §21 declara como el escenario que cerró la única condición del contrato de fachada que no tenía dato de prueba. El desenlace queda registrado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8.

## 4. Roles de calidad dentro del equipo

### 4.1 `GeometriaFactory-Api`

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de verificación, mantener la matriz de cobertura y la Definition of Done, y **mantener la batería de integración del producto**, que vive en este proyecto de código |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar los valores rotulados [ASUNCIÓN] y **ejecutar el despliegue**, que no es del agente |
| Revisión mecánica | El pipeline | Los quince gates de §3, en sus etapas: `build`, `test`, cobertura e **imagen** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

### 4.2 `GeometriaFactory-Domain`

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, y confirmar los dos valores rotulados [ASUNCIÓN] |
| Revisión mecánica | El pipeline | Los ocho gates de §3. Es lo único que no depende de que alguien se acuerde |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15), exactamente con el mismo fundamento con el que lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) §4. Esta categoría no inventa un segundo revisor que no existe.

### 4.3 `GeometriaFactory-Application`

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, y confirmar los dos valores rotulados [ASUNCIÓN] |
| Revisión mecánica | El pipeline | Los once gates de §3. Es lo único que no depende de que alguien se acuerde |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2), exactamente con el mismo fundamento con el que lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) §4. Esta categoría no inventa un segundo revisor que no existe.

### 4.4 `GeometriaFactory-Infrastructure`

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar los tres valores rotulados [ASUNCIÓN] y **decidir sobre los puntos abiertos que `05` §11 le derivó** |
| Revisión mecánica | El pipeline | Los catorce gates de §3, en sus cuatro etapas: `restore`, `build`, `test` y **verificación de transformaciones** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

### 5.1 `GeometriaFactory-Api`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de verificación entran en alcance, y **qué puntos de acceso nuevos entran a la guardia** | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| **Ante todo punto de acceso nuevo** | Que quede dentro de la guardia, o que su exención esté entre las **cuatro** declaradas | `TC-00007` reejecutado, con el recuento de los quince en las dos direcciones. **Es el control que más veces hay que ejercer** |
| Al cerrar cada etapa | La matriz de cobertura entera; el estado de cada `TC-XX`; y **la batería de integración completa** | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `c` | Los valores rotulados [ASUNCIÓN] | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de verificación |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

### 5.2 `GeometriaFactory-Domain`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance, según las historias de la etapa | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera, incluida la de los nueve invariantes; el estado de cada `TC-XX` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `d` | Los dos valores rotulados [ASUNCIÓN], por `BT-02015` | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa ([`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §1.2, citado por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §4.1). **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y una cadencia en semanas sería un plazo que ninguna fuente da.

### 5.3 `GeometriaFactory-Application`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance, según las historias de la etapa | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera, incluida la de las cuatro comprobaciones; el estado de cada `TC-XX` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `d` | Los dos valores rotulados [ASUNCIÓN], por `BT-04018` | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa ([`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md), citado por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2). **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y una cadencia en semanas sería un plazo que ninguna fuente da.

### 5.4 `GeometriaFactory-Infrastructure`

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera; el estado de cada `TC-XX`; y **la batería del validador completa** a partir de la etapa `f` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `f` | Los **diez** casos de la batería contra los **ocho** escenarios, uno por uno | La tabla de `05` §10.5 verificada fila por fila |
| Al cerrar la etapa `c` | Los tres valores rotulados [ASUNCIÓN] | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
