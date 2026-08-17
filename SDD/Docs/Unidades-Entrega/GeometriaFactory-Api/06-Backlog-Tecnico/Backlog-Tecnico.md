# Backlog técnico — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Backlog-Tecnico.md
**Versión:** 2.0
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

**Las cuatro secciones son comunes.** La consolidación es una **unión de catálogo**: los `BT` de las cuatro capas conviven sin colisionar porque la renumeración les dio rango propio, y **el orden de ejecución lo fija el grafo de compilación**, no este documento.

---

## 1. Cómo se lee este backlog

### 1.1 `GeometriaFactory-Api`

Las **veintiséis** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de `05` §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11, de un punto de acceso de la superficie de 02 o de una regla de delivery del intake §15. **Ocho** nacieron de un punto abierto: BT-00005, BT-00007, BT-00009, BT-00010, BT-00015, BT-00021, BT-00025 y BT-00026; **tres de esos puntos —`PA-02`, `PA-03` y `PA-06` de `05` §11— quedaron resueltos** por `PRODUCT-INTAKE` **1.29**, y BT-00015 y BT-00021 pasaron de indagación a trabajo con alcance cerrado.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Dos tareas son inspecciones en las dos direcciones y no funcionalidades.** BT-00012 recorre los **quince** puntos de acceso contra la lista de la guardia, y BT-00013 recorre el conjunto cerrado de **diecisiete** códigos contra la tabla de traducción. Existen porque **el defecto característico de esta capa es de omisión**: un punto sin guardia o un código sin destino **no se ven leyendo el punto nuevo**, se ven comparando contra una lista.
2. **Una tarea fija una decisión que obliga a otro proyecto de código.** BT-00008 fija el **formato de intercambio para los dos extremos**, porque el ensamblado de contratos no lo impone y `GeometriaFactory-Web` declaró que **no lo decide de un solo lado** y que lo adopta. La coincidencia **se verifica ejerciendo el servicio real**, no comparando dos archivos de configuración.
3. **La pirámide de pruebas de este proyecto de código está invertida a propósito**: **60 % de integración y 40 % unitarias**, porque lo que esta capa aporta es cableado. BT-00022 es esa batería, y **golpea el servicio real contra el almacén real**.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

### 1.2 `GeometriaFactory-Domain`

Las **dieciséis** tareas técnicas viven **inline** en este documento y no en archivos individuales, porque el proyecto de código está por debajo del umbral de treinta que fija la regla de la categoría. Cada una declara su fuente upstream por identificador, sus criterios de aceptación, sus dependencias, su tipo y las historias que la consumen.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, de una ADR, de un NFR de su §8, de un punto abierto de su §11 o de una regla de delivery del intake §15. Las cuatro que cierran un punto abierto —BT-02002, BT-02003, BT-02015 y BT-02016— son la parte de este backlog que convierte en trabajo lo que las categorías anteriores dejaron declarado sin resolver, en lugar de resolverlo por su cuenta.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1. Lo que ordena las tareas es la **etapa** y las dependencias de §3, no un tamaño relativo.

### 1.3 `GeometriaFactory-Application`

Las **veintiuna** tareas técnicas viven **inline** en este documento y no en archivos individuales, porque el proyecto de código está por debajo del umbral de treinta que fija la regla de la categoría. Cada una declara su fuente upstream por identificador, sus criterios de aceptación, sus dependencias, su tipo y las historias que la consumen.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11 o de una regla de delivery del intake §15. **Cinco** convierten en trabajo un punto abierto que las categorías anteriores dejaron declarado sin resolver, en lugar de resolverlo por su cuenta: BT-04002, BT-04003, BT-04018, BT-04020 y BT-04021.

**Dos particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Dos tareas cierran un punto abierto que no es de esta capa, y por eso lo acompañan en lugar de resolverlo.** BT-04020 —los sellos de tiempo— y BT-04021 —el criterio de comparación de correos— tienen su titularidad declarada en otro lado: el Product Owner con `GeometriaFactory-Domain` en el primer caso, y la categoría 05 de `GeometriaFactory-Infrastructure` en el segundo (`05` §11 `PA-04` y `PA-03`). Este backlog las hace visibles con su plazo y **no las decide**.
2. **La puerta más dura de este proyecto de código es una ausencia**: cero pruebas de esta capa que toquen la base de datos real (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Application). BT-04006 la materializa, y es lo que sostiene que la autorización por pertenencia se pueda verificar sin base, que es exactamente lo que la fuente exige probar.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1. Lo que ordena las tareas es la **etapa** y las dependencias de §3, no un tamaño relativo.

### 1.4 `GeometriaFactory-Infrastructure`

Las **veintiséis** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de `05` §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11, de una regla conceptual de modelo de la categoría 02 o de una puerta del intake §17.1.P.8 · GeometriaFactory-Infrastructure. **Siete** convierten en trabajo un punto abierto: BT-06002, BT-06003, BT-06019, BT-06023, BT-06024, BT-06025 y BT-06026.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **La mitad de las tareas no toca el almacén, y eso es una decisión de arquitectura y no una casualidad.** Los dos motores del validador, el reloj y el mecanismo de credenciales **no abren el archivo de datos y no hacen red** (`05` §2 propiedad 2), y es lo que hace que la batería obligatoria del producto sea barata de correr y que el NFR de los **200 ms** sea atribuible a esta capa.
2. **La épica del validador es la mitigación del único riesgo de negocio del producto.** El intake declara con probabilidad **alta** e impacto **alto** que el validador se escriba sin leer el análisis; su mitigación declarada es una batería de pruebas, y es EP-T05 entera. Las **cuatro trampas del formato se escriben antes de leer texto**, no después de que algo falle.
3. **Una tarea cierra un punto abierto que ninguna otra capa puede cerrar: la función de derivación de clave.** El intake §17.1.P.1 · GeometriaFactory-Infrastructure la asigna a este proyecto de código y declara dos candidatas **sin elegir**; `ADR-06004` fija la forma y el criterio, y BT-06003 fija la elección concreta en la etapa `a`. **No es una decisión que se pueda delegar hacia arriba ni hacia abajo.**

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 `GeometriaFactory-Api`

### 2.1 EP-T01 · Fundaciones, composición de raíz y arranque

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el servicio exista, que **los cuatro puertos queden conectados con sus cuatro adaptadores en un solo lugar**, que el arranque prepare el almacén antes de atender y **se detenga antes que atender mal**, y que la imagen se construya y responda salud |
| Alcance | Proyecto y batería de integración, composición de raíz, arranque en dos fases, imagen multietapa, anclajes y la puerta de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`, puerta `PT-04`), §16, §17.1.P.7 · GeometriaFactory-Api a P.9; `05` §3.1, §4, §5, §11 `PA-07`; [`ADR-00001`](../05-Arquitectura-Tecnica/Adrs/ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md), [`ADR-00006`](../05-Arquitectura-Tecnica/Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) |
| Etapa | `a` |
| BT contenidas | BT-00001, BT-00002, BT-00003, BT-00004, BT-00005, BT-00006 |

### 2.2 EP-T02 · Superficie y formato de intercambio

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las rutas y los verbos de los **quince** puntos queden fijados en el punto de control, y que el **formato de intercambio quede declarado una sola vez para los dos extremos**, con el límite de cuerpo que **rechaza y nunca trunca** |
| Alcance | Rutas y verbos, formato de intercambio, límite de cuerpo y vigencia del acceso |
| Fuente upstream | `05` §3.4, §7 filas de formato y de configuración, §11 `PA-01`, `PA-04` y `PA-05`; [`ADR-00002`](../05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-00003`](../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 |
| Etapa | `a` |
| BT contenidas | BT-00007, BT-00008, BT-00009, BT-00010 |

### 2.3 EP-T03 · Guardia y traducción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que **ningún punto de acceso quede fuera de la guardia** salvo los cuatro declarados, y que las **dos** traducciones ocurran en una tabla única **sin inventar códigos**, con las **tres** familias deliberadamente empobrecidas verificadas |
| Alcance | Guardia de admisión, traductor, inspecciones en las dos direcciones y los dos huecos del conjunto cerrado |
| Fuente upstream | `05` §3.1 (los dos componentes transversales), §7 filas de autorización, guardia y manejo de errores, §8 filas de puntos fuera de la guardia, de códigos con traducción y de respuestas indistinguibles, §9 riesgos primero y segundo; [`ADR-00003`](../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), [`ADR-00004`](../05-Arquitectura-Tecnica/Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) |
| Etapa | `c` |
| BT contenidas | BT-00011, BT-00012, BT-00013, BT-00014, BT-00015 |

### 2.4 EP-T04 · Las cuatro superficies de acceso

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **quince** puntos vivan repartidos en las **cuatro** superficies que `05` §3.1 declara, sin que ninguna dependa de otra |
| Alcance | Acceso y credencial propia, gobierno de la comisión, trabajos y desenlace |
| Fuente upstream | `05` §3.1 y §3.4; `05` §3.2 punto 1 (ninguna superficie depende de otra); [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) |
| Etapa | `c` a `h`, según la historia que la consuma |
| BT contenidas | BT-00016, BT-00017, BT-00018, BT-00019 |

### 2.5 EP-T05 · Verificación, muestras y despliegue

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la batería de integración ejercite el cableado contra el servicio real, que la colección de peticiones se reproduzca **en cinco pasos o menos y sin datos inventados**, y que los valores rotulados como asunción y el mecanismo de despliegue queden elevados con su plazo |
| Alcance | Colección reproducible, batería de integración, las dos pruebas de criterio propio, y los tres puntos abiertos del Product Owner y de 09 |
| Fuente upstream | `05` §8 filas de la pirámide, de eliminaciones forzadas, de textos alterados y de pasos de la colección; `05` §11 `PA-06`, `PA-08` y `PA-09`; `PRODUCT-INTAKE` §16.1, §17.1.P.6 · GeometriaFactory-Api y §18 |
| Etapa | `e` a `h`, y las elevaciones antes del punto de control de su etapa |
| BT contenidas | BT-00020, BT-00021, BT-00022, BT-00023, BT-00024, BT-00025, BT-00026 |

### 2.2 `GeometriaFactory-Domain`

### 2.1 EP-T01 · Fundaciones del proyecto de código

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, compile con cero dependencias salientes y cierre en su punto de control las dos decisiones que el intake dejó abiertas para la etapa `a` |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, herramienta de versión y las dos puertas de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16 (estructura de repositorio), §17.1.P.7 · GeometriaFactory-Domain y P.11; [`ADR-02001`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-02003`](../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md); `05` §5, §8 y §11 |
| Etapa | `a` |
| BT contenidas | BT-02001, BT-02002, BT-02003, BT-02004, BT-02005 |

### 2.2 EP-T02 · Superficie pública y resultados tipados

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la superficie pública sea la que declara el contrato de abstracciones, con rechazo tipado y con el catálogo de condiciones cerrado en las dos direcciones |
| Alcance | Núcleo de entidades, forma de las guardas, catálogo de condiciones, entrada del momento y de la unicidad por parámetro |
| Fuente upstream | `05` §3.1 (núcleo de entidades), [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), [`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md), [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md), `05` §8 fila del catálogo de condiciones |
| Etapa | `c` a `f`, según la historia que la consuma |
| BT contenidas | BT-02006, BT-02007, BT-02008, BT-02009 |

### 2.3 EP-T03 · Guardas de cuenta y admisibilidad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las reglas de la cuenta se ejerzan en su componente y que la admisibilidad sea una puerta única |
| Alcance | Guardas de cuenta y evaluador de admisibilidad, con `INV-06` e `INV-09` ejercidos en un solo lugar |
| Fuente upstream | `05` §3.1 (guardas de cuenta, evaluador de admisibilidad), [`ADR-02004`](../05-Arquitectura-Tecnica/Adrs/ADR-02004-Frontera-De-Autenticacion-Y-Autorizacion.md), [`ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) |
| Etapa | `c` y `d` |
| BT contenidas | BT-02010, BT-02011 |

### 2.4 EP-T04 · Trabajo, estados y adopción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las transiciones del trabajo y la adopción del conjunto de piezas vivan cada una en su componente, sin que ninguna regla se ejerza dos veces |
| Alcance | Máquina de estados del trabajo y adopción de la interpretación |
| Fuente upstream | `05` §3.1 (máquina de estados del trabajo, adopción de la interpretación), [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `PRODUCT-INTAKE` §4.2 (modelo de estados) |
| Etapa | `e`, `f` y `h` |
| BT contenidas | BT-02012, BT-02013 |

### 2.5 EP-T05 · Verificación y puertas

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los nueve invariantes queden ejercidos, que las puertas medibles del proyecto de código estén definidas y que los dos valores rotulados como asunción se confirmen antes de volverse bloqueantes |
| Alcance | Matriz invariante contra prueba, puertas de cobertura y de tiempo, y el criterio de comparación de correos |
| Fuente upstream | `05` §8 (NFR de ejercicio de los invariantes y de cobertura), `05` §11 PA-02; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §9; `PRODUCT-INTAKE` §22 asunciones `A-3` y `A-5` |
| Etapa | `a` la definición, `d` a `h` la ejecución acumulativa |
| BT contenidas | BT-02014, BT-02015, BT-02016 |

### 2.3 `GeometriaFactory-Application`

### 2.1 EP-T01 · Fundaciones del proyecto de código

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, compile con **una sola** dependencia saliente y cierre en su punto de control las decisiones de nombre que el intake y las categorías 02 y 05 dejaron abiertas para la etapa `a`, incluido el nombre del cuarto puerto |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, herramienta de versión y las tres puertas de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16, §17.1.P.1 · GeometriaFactory-Application, §17.1.P.7 · GeometriaFactory-Application y §17.1.P.8 · GeometriaFactory-Application; [`ADR-04001`](../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-04003`](../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md); `05` §5, §8 y §11 `PA-01`, `PA-02` y `PA-06` |
| Etapa | `a` |
| BT contenidas | BT-04001, BT-04002, BT-04003, BT-04004, BT-04005, BT-04006 |

### 2.2 EP-T02 · Frontera, forma de la superficie y unidad de trabajo

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **cuatro** puertos existan como frontera declarada, que toda negativa prevista viaje como resultado tipado con su código del catálogo cerrado, y que el alcance transaccional lo fije esta capa |
| Alcance | Declaración de puertos, resultado tipado, catálogo de **36** condiciones y unidad de trabajo |
| Fuente upstream | `05` §3.1 (componente «Declaración de puertos») y §3.4; [`ADR-04002`](../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), [`ADR-04005`](../05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md), [`ADR-04006`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); `05` §8, filas de cobertura del catálogo y de unidades de trabajo |
| Etapa | `c`, y la cobertura del catálogo se cierra en la `f`, que es cuando el conjunto de condiciones está entero producido |
| BT contenidas | BT-04007, BT-04008, BT-04009 |

### 2.3 EP-T03 · Guarda de autorización

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las **cuatro** comprobaciones se ejerzan en un único componente, con orden fijo, sobre el dato ya recuperado y antes de escribir; y que la cuarta corte antes que las otras tres |
| Alcance | Componente de guarda, orden fijo y matriz de ejercicio de las cuatro negativas |
| Fuente upstream | `05` §3.1 (componente «Guarda de autorización»), §7 fila de autorización, §8 fila de ejercicio de las cuatro comprobaciones, §9 riesgos primero a tercero; [`ADR-04004`](../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md); [`Domain ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 punto 1 |
| Etapa | `c` la guarda, `d` la matriz completa, porque la cuarta comprobación no tiene sobre qué decidir hasta que exista la marca |
| BT contenidas | BT-04010, BT-04011 |

### 2.4 EP-T04 · Los seis orquestadores

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **once** casos de uso queden repartidos en los **seis** componentes de orquestación que `05` §3.3 declara, sin que ningún orquestador dependa de otro |
| Alcance | Alta de cuentas, gobierno de cuentas, ingreso y credencial, trabajo, consulta y desenlace |
| Fuente upstream | `05` §3.1 y §3.3; §3.2 punto 1 (ningún orquestador depende de otro); [`ADR-04001`](../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md); `05` §6 (forma de la consulta) |
| Etapa | `c` a `h`, según la historia que la consuma |
| BT contenidas | BT-04012, BT-04013, BT-04014, BT-04015, BT-04016, BT-04017 |

### 2.5 EP-T05 · Verificación y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los NFR con objetivo numérico tengan mecanismo de medición, que los valores rotulados como asunción se confirmen antes de volverse bloqueantes, y que los dos puntos abiertos cuya titularidad es de otro lado queden elevados con su plazo |
| Alcance | Puerta de cobertura, medición del caso de uso más pesado, y los dos puntos abiertos ajenos |
| Fuente upstream | `05` §8, filas de tiempo y de cobertura; `05` §11 `PA-03`, `PA-04` y `PA-05`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` |
| Etapa | `d` la mayor parte, `f` la medición del caso de uso más pesado, que es cuando el envío existe |
| BT contenidas | BT-04018, BT-04019, BT-04020, BT-04021 |

### 2.4 `GeometriaFactory-Infrastructure`

### 2.1 EP-T01 · Fundaciones y anclajes

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, que sus nombres queden cerrados en el punto de control y que **la función de derivación de clave quede anclada con sus parámetros versionados** |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, derivación de clave y la puerta de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16, §17.1.P.1 · GeometriaFactory-Infrastructure, §17.1.P.7 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Infrastructure; `05` §11 `PA-01`, `PA-02` y `PA-03`; [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) |
| Etapa | `a` |
| BT contenidas | BT-06001, BT-06002, BT-06003, BT-06004 |

### 2.2 EP-T02 · Almacén, contexto y preparación

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el mapa entre las **cinco** entidades y el esquema físico exista, que las transformaciones se apliquen solas sobre un almacén inexistente y que el arranque **se detenga antes que operar sobre un almacén en el que no se puede confiar** |
| Alcance | Contexto de persistencia y mapeo, preparación del almacén con linaje inmutable, puerta de transformaciones y la zona horaria de los sellos |
| Fuente upstream | `05` §3.1 (componente transversal y mecanismo de arranque), §5 (cuarta etapa del pipeline), §6, §7 fila de zona horaria; [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md), [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) |
| Etapa | `a`, porque `PT-04` se mide ahí |
| BT contenidas | BT-06005, BT-06006, BT-06007, BT-06008 |

### 2.3 EP-T03 · Adaptadores de puerto

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **cuatro** puertos tengan **cuatro** adaptadores separados, que ninguno dependa de otro y que la proyección de listado no arrastre lo que el detalle sí lleva |
| Alcance | Adaptador de cuentas con su índice único, adaptador de trabajos con sus dos formas de lectura, retiro físico con todo o nada, y adaptador de reloj |
| Fuente upstream | `05` §3.1 y §3.4; [`ADR-06001`](../05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), [`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md), [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md); `RC-06001`, `RC-06002`, `RC-06005`, `RC-06006`, `RC-06007` |
| Etapa | `c` el de cuentas y el de reloj, `d` la marca, `e` el de trabajos y el retiro |
| BT contenidas | BT-06009, BT-06010, BT-06011, BT-06012 |

### 2.4 EP-T04 · Mecanismos de seguridad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las **dos** piezas sensibles del producto vivan acá y sólo acá, y que **la producción de la contraseña provisoria** —la delegación explícita de las tres capas de arriba— no se pueda componer por otro medio |
| Alcance | Derivación y verificación de credenciales, producción de la provisoria, emisión y verificación del acceso firmado |
| Fuente upstream | `05` §3.1 (mecanismo de credenciales, mecanismo de acceso firmado), §7 filas de autenticación y de producción de la provisoria, §9 riesgos tercero y cuarto; [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md), [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md); `RN-06014`, `RN-06016` |
| Etapa | `c` la derivación y el acceso, `d` la provisoria |
| BT contenidas | BT-06013, BT-06014, BT-06015 |

### 2.5 EP-T05 · Validador de figuras

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el texto real del alumno se interprete con sus **cuatro** trampas, que los valores se verifiquen con tolerancia **0.01** y operador **estricto**, y que la batería de **10** casos pase con los **ocho** escenarios como entrada. **Es la mitigación del riesgo de negocio del producto** |
| Alcance | Motor de interpretación, motor de verificación, tabla de derivación por tipo, batería obligatoria y la puerta de cero red |
| Fuente upstream | `05` §3.1 (los dos motores), §8 filas de tiempo, de cobertura del validador, de tolerancia, de la batería y de peticiones de red, §9 riesgos primero y segundo, §10.5; [`ADR-06006`](../05-Arquitectura-Tecnica/Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md); [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md); [`Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); `PRODUCT-INTAKE` §20 y §21 |
| Etapa | `f` |
| BT contenidas | BT-06016, BT-06017, BT-06018, BT-06019, BT-06020 |

### 2.6 EP-T06 · Verificación y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el catálogo de **17** condiciones se cierre en las dos direcciones, que ningún mensaje ni traza lleve un secreto, la ruta del almacén o el texto del alumno, y que los cuatro puntos abiertos que quedan elevados tengan plazo |
| Alcance | Catálogo cerrado, prueba de inspección de secretos, valores rotulados como asunción y los tres puntos abiertos del Product Owner |
| Fuente upstream | `05` §7 fila de secretos y datos que no se registran, §8 filas de cobertura del catálogo y de mensajes con secretos; `05` §11 `PA-04`, `PA-06`, `PA-07`, `PA-09` y `PA-11`; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §1.4 |
| Etapa | `d` los tres primeros, y las elevaciones antes del punto de control de la etapa que las contiene |
| BT contenidas | BT-06021, BT-06022, BT-06023, BT-06024, BT-06025, BT-06026 |

## 3. Detalle de las tareas técnicas

### 3.1 `GeometriaFactory-Api`

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-00001 | Crear el proyecto de código y su proyecto de pruebas de integración | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.1.P.1 · GeometriaFactory-Api; `05` §5 | Ninguna | El proyecto de código compila dentro del artefacto de agrupación con sus **tres** dependencias de compilación; **el proyecto de pruebas de integración existe acá y es el que golpea el servicio real**, incluido el de las capas de adentro que no pueden tocar la base | **Infraestructura compartida**: habilita a las 30 |
| BT-00002 | Construir la composición de raíz con los cuatro puertos y sus adaptadores | feature | EP-T01 | `a` | Alta | Sin fijar | `05` §3.1, componente «Composición de raíz»; [`ADR-00006`](../05-Arquitectura-Tecnica/Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md); `05` §9, séptimo riesgo | BT-00001 | **4 de 4** puertos conectados a su adaptador, y **0** puertos sin adaptador o con más de uno; la composición es **única** y no se reparte en módulos por área, porque **la frontera tiene que ser contable en un solo lugar**; si falta una dependencia, **falla en construcción** y no hay petición que responder; toda la configuración del despliegue entra **por acá y por ningún otro lado** | US-00026 |
| BT-00003 | Construir el arranque en dos fases con el punto de salud sin acceso | feature | EP-T01 | `a` | Alta | Sin fijar | `05` §4, quinta viñeta; [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md); `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Api | BT-00002 | Primero se construye el grafo —si falla, falla en construcción—, después se **dispara** la preparación del almacén —si falla, **el arranque se detiene**— y **recién entonces el servicio escucha**; **0** peticiones atendidas con la preparación incompleta; el punto de salud **no exige acceso**, porque tiene que poder responder cuando nadie puede autenticarse | US-00027, US-00028, US-00029 |
| BT-00004 | Construir la imagen multietapa y medir `PT-04` | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.1.P.8 · GeometriaFactory-Api; `05` §5, etapas del pipeline y contenido de la imagen | BT-00003 | La imagen se construye con el archivo de construcción **multietapa**, lleva **sólo el entorno de ejecución** —sin kit de desarrollo ni depurador— y **no tiene linaje con la imagen del contenedor de desarrollo**; arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. **Una puerta que no pasa detiene la planificación de las etapas que dependen de ella** | **Infraestructura compartida**: es `PT-04`, puerta del producto |
| BT-00005 | Anclar nombres de tipos, espacios de nombres y versiones de paquetes | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-07`; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Api, que declara la versión de los paquetes abierta y anclada en la primera etapa | BT-00001 | Los nombres y las versiones quedan decididos y anclados según la regla de anclaje del producto, y registrados. **Caja temporal: la etapa `a`** | **Infraestructura compartida** |
| BT-00006 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, última fila; `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Api | BT-00001 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-00007 | Fijar las rutas y los verbos de los quince puntos de acceso en el punto de control | indagación | EP-T02 | `a` | Alta | Sin fijar | `05` §3.4 y §11 `PA-01`; [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 | BT-00001 | Las rutas y los verbos quedan validados en el punto de control de la etapa `a`. Las **dos** únicas cosas que una fuente declara son el punto de canje, con su ruta, y la **existencia** de un punto de salud, cuya ruta la fuente **no da**; las quince filas son **propuesta derivada rotulada fila por fila** y esta tarea las confirma o las corrige. **`A-04` queda retirado y no se recicla.** **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los quince puntos dependen de ella |
| BT-00008 | Fijar el formato de intercambio para los dos extremos | feature | EP-T02 | `a` | Alta | Sin fijar | [`ADR-00002`](../05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md); `05` §2.2, quinta y sexta decisión heredada; `05` §9, cuarto riesgo | BT-00002 | **Exactamente 1** configuración de intercambio declarada en el producto, en la **composición de raíz** y en ningún otro lado; campos con **nombre literal**, valores de conjunto cerrado **por su nombre y nunca por su posición**, campos nulos **emitidos**, números **sin cultura** y **lectura estricta**; **ningún punto de acceso configura la serialización por su cuenta**. La coincidencia con el otro extremo **se verifica ejerciendo el servicio real** y no comparando dos archivos. **Esta decisión obliga a `GeometriaFactory-Web`, que declaró que la adopta** | US-00019, US-00022, US-00024 |
| BT-00009 | Fijar el límite de tamaño de cuerpo que rechaza y nunca trunca | indagación | EP-T02 | `a` | Alta | Sin fijar | [`ADR-00002`](../05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6; `05` §11 `PA-05`; [`Infrastructure ADR-06006`](../05-Arquitectura-Tecnica/Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3 | BT-00008 | **Un solo** límite para todo el producto, tomado de configuración; el cuerpo que lo excede **se rechaza y nunca se trunca**, y **la forma de rechazo no es configurable**; el número se calibra **sobre el texto más grande que la fuente documenta**. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá** porque el corte pertenece al borde del proceso. **Caja temporal: la etapa `a`** | US-00019 |
| BT-00010 | Fijar la vigencia del acceso firmado | indagación | EP-T02 | `a` | Media | Sin fijar | `05` §11 `PA-04`; [`ADR-00003`](../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Api, que declara «corta» y **sin acceso de refresco** | BT-00002 | El número queda tomado **de configuración** aplicando el criterio ya fijado: que caduque **dentro de la sesión de trabajo de una clase** y que **la renovación sea reingreso**. **Ninguna fuente da el número**, y esta tarea no lo inventa: lo elige aplicando el criterio y lo registra. **Caja temporal: la etapa `a`** | US-00001, US-00004 |
| BT-00011 | Construir la guardia de admisión transversal | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guardia de admisión»; [`ADR-00003`](../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `05` §7, filas de autenticación, autorización y guardia | BT-00002, BT-00007 | Verifica **firma y expiración** del acceso, exige el **papel** que cada punto declara y aplica la **guardia del cambio de contraseña pendiente**; es transversal a los **once** puntos que exigen acceso; **exigir el papel no es autorizar**: la verificación de pertenencia y de facultad se hace sobre el dato recuperado y es de la capa de aplicación, y **duplicarla acá crearía un segundo lugar donde la regla puede decir otra cosa** | US-00004, US-00005, US-00006 |
| BT-00012 | Puerta de inspección de los quince puntos contra la guardia, en las dos direcciones | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §8, fila de puntos fuera de la guardia; `05` §9, primer riesgo; `RN-00013`, `INV-09` | BT-00011 | Exactamente **4** puntos fuera de la guardia, **ni uno más**, y son los declarados: canje, registro, configuración del administrador y salud; la inspección **recorre los quince y compara contra la lista en las dos direcciones**; y **0** puntos que fijen una contraseña sobre una cuenta existente sin credencial. **Se mide en cada etapa que agregue un punto**, no sólo en la que la introdujo | **Infraestructura compartida**: es el defecto de omisión más caro de esta capa. Un punto nuevo fuera de la guardia rompe `RN-00013` **sin que nada falle** |
| BT-00013 | Construir el traductor con la tabla única, sin códigos inventados | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Traductor de motivos y códigos»; [`ADR-00004`](../05-Arquitectura-Tecnica/Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md); `05` §8, fila de códigos con traducción; [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 | BT-00002 | Las **dos** traducciones en ese orden: motivo interno a código del contrato, y código del contrato a código de respuesta; **16 de 17** códigos con traducción declarada y **1** declarado **sin destino con su motivo**; **0** códigos inventados y **0** renombrados; la inspección recorre el conjunto cerrado contra la tabla **en las dos direcciones**; **ningún camino de fallo sale sin pasar por acá** | US-00024, US-00025 |
| BT-00014 | Prueba de las tres familias deliberadamente empobrecidas | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §7, fila de familias empobrecidas; `05` §8, fila de respuestas indistinguibles; `05` §9, segundo riesgo | BT-00013 | **3 de 3** comparaciones dan **idénticas, cuerpo y código**: trabajo ajeno contra inexistente, correo inválido contra contraseña inválida, y correo ocupado por cuenta habilitada contra ocupado por cuenta bloqueada. **En las tres es la decisión y no el defecto**, y la primera es la que rompe `RN-00003` hacia afuera **sin que ninguna capa de adentro se entere** | US-00002, US-00020, US-00021 |
| BT-00015 | Cablear los dos códigos que cerraron los huecos del conjunto cerrado | implementación | EP-T03 | `c` | Media | Sin fijar | `05` §11 `PA-02` y `PA-03`, **los dos resueltos**; `02` §11 | BT-00013 | Los **dos** huecos que esta tarea elevaba están **cerrados** por `PRODUCT-INTAKE` **1.29** §17.4 P.3 (2026-08-12): entraron al conjunto cerrado `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`, que `GeometriaFactory-Contracts` emite. Lo que queda es **trabajo de traducción y no de indagación**: las dos filas nuevas de la tabla de `05` §5, con destinos `403` y `409`, y el genérico bajando de cuatro destinos a dos. **Esta categoría no inventó ningún código**. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner y de `GeometriaFactory-Contracts` |
| BT-00016 | Construir la superficie de acceso y credencial propia | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4 | BT-00007, BT-00011, BT-00013 | Los **cuatro** puntos que se ejercen **sin acceso firmado o sobre la propia cuenta**: canje, registro de cuenta, configuración del administrador y cambio de la propia contraseña. **El registro es anónimo por diseño y así debe seguir**; el cambio de la propia contraseña es **la única excepción de la guardia del cambio pendiente**; **ninguno de los cuatro que no exigen acceso fija una contraseña sobre una cuenta existente** | US-00001, US-00002, US-00003, US-00007, US-00008, US-00009, US-00010 |
| BT-00017 | Construir la superficie de gobierno de la comisión | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §10.2, filas de RN-00007, RN-00012, RN-00015 y RN-00016 | BT-00011, BT-00013, BT-00016 | Los **cuatro** puntos del administrador sobre cuentas ajenas: listado, cambio de situación, baja **transportando el correo escrito** y reseteo; **el cambio de situación devuelve la provisoria** en su resultado y **el reseteo la devuelve una sola vez**; el punto de reseteo **no declara ningún parámetro de situación** y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe; **el reseteo no toca ninguna ruta de retiro** | US-00011, US-00012, US-00013, US-00014, US-00015, US-00016 |
| BT-00018 | Construir la superficie de trabajos | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §6, las dos decisiones de frontera | BT-00008, BT-00011, BT-00013 | Los **cinco** puntos sobre trabajos: envío, reenvío, eliminación con sus **dos** alcances, listado y detalle; **el texto original no se normaliza en el borde**; el listado **no arrastra el texto ni los componentes** y esta capa **no recompone la proyección**; **la superficie no declara ningún parámetro con el que se puedan pedir borradores ajenos** | US-00017, US-00018, US-00019, US-00020, US-00021, US-00022 |
| BT-00019 | Construir la superficie de desenlace | feature | EP-T04 | `h` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §10.3 `INV-07` | BT-00011, BT-00013 | El punto de aprobar o rechazar **desde el estado `Pendiente`**, con comentario opcional; la traducción del estado que no admite desenlace **incluido el terminal**, y **sin sugerir ninguna forma de revertirlo** | US-00023 |
| BT-00020 | Construir la colección de peticiones reproducible | docs | EP-T05 | `h` | Media | Sin fijar | `PRODUCT-INTAKE` §16.1 y §18 `S-2`; `05` §8, última fila; [`ADR-00008`](../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | BT-00016, BT-00017, BT-00018, BT-00019 | Se reproduce en **5 pasos o menos**, con **0** datos de prueba inventados; los cuerpos son los escenarios del intake §20; incluye alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, que es lo que la ubica en la etapa `h`; **no implementa nada: demuestra**, y vive en el árbol de muestras del repositorio | US-00030 |
| BT-00021 | Verificar que la colección lleva los ocho escenarios que el Product Owner fijó | verificación | EP-T05 | `h` | Media | Sin fijar | `05` §11 `PA-06`, **resuelto**; `02` §11 | BT-00020 | El alcance que esta tarea elevaba está **cerrado a favor de los ocho escenarios `E-1` a `E-8`** por `PRODUCT-INTAKE` **1.29** §18 (2026-08-12), que es la lectura que la categoría 02 ya había adoptado. **No cambia ningún artefacto**: lo que queda es recontar los cuerpos de la colección contra los ocho de §20 y dejarlo asentado. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-00022 | Construir la batería de integración con la pirámide invertida | devops | EP-T05 | `c` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Api; `05` §8, filas de cobertura y de forma de la pirámide | BT-00001, BT-00003 | La batería **golpea el servicio real por su superficie contra el almacén real**; la forma declarada es **60 %** de integración y **40 %** unitarias, **invertida a propósito porque lo que esta capa aporta es cableado y el cableado se verifica ejerciéndolo**; cubre además el contrato con el ensamblado de tipos **de extremo a extremo**. Los dos porcentajes vienen **rotulados como asunción** y se usan como vigentes | **Infraestructura compartida**: es la verificación de esta capa y también la de las capas de adentro que no pueden tocar la base |
| BT-00023 | Prueba de eliminación forzada contra la superficie, en sus dos alcances | devops | EP-T05 | `e` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Api, criterio bloqueante tomado de la fuente; `05` §8, fila correspondiente; `Roadmap-Producto.md` §5.2, transición `e` → `f` | BT-00018, BT-00022 | **0** eliminaciones fuera de alcance aceptadas al **forzar la petición**: un trabajo que no está en `Borrador` y uno que no pertenece al solicitante. **Es el único criterio de verificación del producto que la fuente exige ejercer forzando la petición contra esta superficie**, y no sólo por la interfaz | US-00020 |
| BT-00024 | Prueba del texto original byte a byte y del rechazo sin truncamiento | devops | EP-T05 | `e` | Alta | Sin fijar | `05` §8, fila de textos alterados; `05` §9, tercer riesgo; `RN-00008` | BT-00009, BT-00018, BT-00022 | **0** caracteres de diferencia entre lo enviado y lo guardado, comparado **byte a byte** con el texto de `E-1`; y **0** truncamientos silenciosos: un cuerpo por encima del límite **se rechaza y no se trunca**. **Truncar rompe `RN-00008` en silencio**, con el trabajo guardado y el texto mutilado, y el alumno lo descubre al ver el dibujo | US-00019 |
| BT-00025 | Confirmar los cinco valores rotulados como asunción | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, cinco primeras filas; `05` §11 `PA-09`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-00022 | El Product Owner confirma o corrige **latencia, caudal, arranque en frío, cobertura y la forma de la pirámide** sobre su propio documento; hasta entonces se usan como vigentes y la puerta de cobertura **no se declara bloqueante** en 09. **Es la mayor concentración de valores sin confirmar de los siete proyectos de código**, y ninguna salida consiste en inventar un número acá. **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida** |
| BT-00026 | Probar una vez la construcción de la imagen en destino desde el repositorio | indagación | EP-T05 | `h` | Media | Sin fijar | `05` §11 `PA-08`; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Api punto 5, rotulado **[A VERIFICAR]** | BT-00004 | El mecanismo queda **probado una vez antes de depender de él**, tal como el intake exige: el motor de contenedores del destino resuelve la referencia al repositorio y tiene credenciales si es privado. **No es una asunción de esta categoría** y **la decisión de medirlo es de `09-Devops`**; esta tarea la eleva con su plazo. **Caja temporal: antes de la etapa de despliegue real** | **Infraestructura compartida**: es el único canal de entrega declarado |

**Once tareas se justifican como infraestructura compartida** —BT-00001, BT-00004, BT-00005, BT-00006, BT-00007, BT-00012, BT-00015, BT-00021, BT-00022, BT-00025 y BT-00026— y las **quince** restantes declaran al menos una historia consumidora —BT-00002, BT-00003, BT-00008, BT-00009, BT-00010, BT-00011, BT-00013, BT-00014, BT-00016, BT-00017, BT-00018, BT-00019, BT-00020, BT-00023 y BT-00024—. **Once más quince son veintiséis**, y ninguna queda sin una cosa ni la otra.

### 3.2 `GeometriaFactory-Domain`

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-02001 | Crear el proyecto de código y su proyecto de pruebas, sin dependencias salientes | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.1.P.1 · GeometriaFactory-Domain; [`ADR-02001`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación; el archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización; el proyecto de pruebas existe y corre vacío | **Infraestructura compartida**: la sostiene [`ADR-02001`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md). Habilita a las 27 |
| BT-02002 | Fijar los nombres de tipos y de espacios de nombres, y validarlos en el punto de control | indagación | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain (punto abierto de la etapa `a`); `05` §11 PA-01 | BT-02001 | Existe una propuesta de nombres para las cinco entidades y para los espacios de nombres; el Product Owner la acepta o la corrige **en el punto de control de la etapa `a`**; la decisión queda registrada. **Caja temporal: la etapa `a`**, y no se arrastra a la `c` | **Infraestructura compartida**: ninguna historia la consume por separado, todas dependen de que los nombres estén fijados. `05` §9 la declara como riesgo de retrabajo, no de corrección |
| BT-02003 | Elegir y anclar la herramienta que calcula la versión | indagación | EP-T01 | `a` | Media | Sin fijar | `PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain; `05` §11 PA-04 | BT-02001 | La herramienta está elegida y su versión anclada según la regla de anclaje de versiones del producto; el cálculo de la versión a partir de las convenciones de mensaje de confirmación produce un resultado reproducible. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la exige la estrategia de versionado del intake §17.1.P.7 · GeometriaFactory-Domain |
| BT-02004 | Puerta bloqueante de cero dependencias salientes | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de dependencias salientes; `05` §9, primer riesgo | BT-02001 | La inspección del archivo de proyecto es parte de la revisión y **bloquea la fusión** si aparece una dependencia; la puerta se mide en cada etapa, no sólo en la `a` | **Infraestructura compartida**: sostiene la propiedad que justifica el estilo entero ([`ADR-02001`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md)) |
| BT-02005 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de advertencias de construcción; `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Domain | BT-02001 | El guion de construcción termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-02006 | Construir el núcleo de entidades con las cinco entidades del modelo | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.1, componente «Núcleo de entidades»; [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) | BT-02001, BT-02002 | Las cinco entidades quedan constituibles con sus atributos y su semántica; el valor declarado y el derivado de cada pieza se guardan **por separado**; la posición de la pieza es su identidad y el conjunto **admite huecos y no se renumera** (`05` §6) | US-02001, US-02009, US-02011, US-02012, US-02024 |
| BT-02007 | Fijar la forma de la superficie pública: guardas con resultado tipado | feature | EP-T02 | `c` | Alta | Sin fijar | [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) | BT-02001, BT-02002 | Toda condición prevista viaja como **valor de retorno** con su código estable, nunca como excepción de control de flujo; las excepciones quedan reservadas a defectos de programación del consumidor; ninguna operación deja una entidad a medio modificar | US-02002, US-02007 y, por herencia de forma, las 27 |
| BT-02008 | Cerrar el catálogo de las 42 condiciones en las dos direcciones | feature | EP-T02 | `f` | Alta | Sin fijar | `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) | BT-02007 | **100 %** de las 42 condiciones alcanzadas por al menos una prueba, y **0** condiciones producidas por la biblioteca que no figuren en el catálogo; la comparación se hace en las dos direcciones. Los **cinco** identificadores retirados no se reciclan | US-02002, US-02013, US-02014, US-02016 |
| BT-02009 | Hacer que el momento y la unicidad entren por parámetro | feature | EP-T02 | `d` | Alta | Sin fijar | [`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `05` §7, filas de configuración y de zona horaria | BT-02006 | Ninguna operación obtiene el momento por su cuenta ni consulta conjuntos de entidades; la inspección lo verifica; las pruebas son reproducibles sin fijar el reloj del entorno | US-02003, US-02009 |
| BT-02010 | Construir las guardas de cuenta | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guardas de cuenta» | BT-02006, BT-02007 | Papeles, ventana de alta del administrador, ciclo de vida y credencial derivada quedan ejercidos en este componente; **las guardas no invocan al evaluador de admisibilidad** (`05` §3.2) | US-02001, US-02004, US-02005, US-02006, US-02024, US-02025, US-02026 |
| BT-02011 | Construir el evaluador de admisibilidad como puerta única | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Evaluador de admisibilidad»; [`ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) | BT-02006, BT-02010 | `INV-06` e `INV-09` se ejercen **en un solo lugar** y no repetidos en cada operación; el resultado trae el motivo de la no admisión; ninguna otra operación del proyecto de código vuelve a comprobar esas dos condiciones | US-02006, US-02008, US-02026, US-02027 |
| BT-02012 | Construir la máquina de estados del trabajo | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Máquina de estados del trabajo»; `PRODUCT-INTAKE` §4.2 | BT-02006, BT-02007 | Las transiciones del modelo de estados quedan ejercidas, con envío, desenlace, terminalidad y quién elimina en qué estado; una transición no admitida devuelve su condición y no cambia nada | US-02005, US-02010, US-02015, US-02016, US-02017, US-02018, US-02019, US-02020, US-02021, US-02022, US-02023 |
| BT-02013 | Construir la adopción de la interpretación | feature | EP-T04 | `f` | Alta | Sin fijar | `05` §3.1, componente «Adopción de la interpretación» | BT-02006, BT-02012 | El conjunto de piezas, sus componentes y las observaciones se incorporan comprobando que están bien formados; un conjunto mal formado se rechaza **entero** y el trabajo queda como estaba | US-02010, US-02011, US-02013, US-02014, US-02016 |
| BT-02014 | Armar la matriz de ejercicio de los nueve invariantes | docs | EP-T05 | `d` | Alta | Sin fijar | `05` §8, fila de ejercicio de los invariantes; `05` §9, segundo riesgo | BT-02010, BT-02011, BT-02012 | **100 %** de los nueve invariantes con al menos una prueba que verifique su violación rechazada, **sin dobles de prueba**; la matriz se entrega a 08 y se revisa al cerrar cada etapa | US-02008, US-02017, US-02023, US-02025, US-02027 |
| BT-02015 | Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, filas de tiempo de la batería y de cobertura; `05` §11 PA-02; `PRODUCT-INTAKE` §22 asunciones `A-3` y `A-5` | BT-02005, BT-02014 | El Product Owner confirma o corrige los dos valores **sobre su propio documento**; hasta entonces se usan como vigentes y la puerta **no se declara bloqueante** en 09. **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida**: condiciona la puerta del pipeline de todas las historias |
| BT-02016 | Decidir el criterio de comparación de dos correos | indagación | EP-T05 | `d` | Media | Sin fijar | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §9, punto abierto; `RN-02002`, `INV-01` | BT-02006 | Queda decidido si dos correos se comparan tal cual o normalizados, **y dónde** se normaliza; la decisión se toma junto con la capa que ejerce la verificación y no acá sola; el dominio sigue conservando el dato como lo recibe. **Caja temporal: antes de cerrar la etapa `d`** | US-02003 |

**Seis tareas se justifican como infraestructura compartida** —BT-02001, BT-02002, BT-02003, BT-02004, BT-02005 y BT-02015— y las otras diez declaran al menos una historia consumidora. Ninguna tarea queda sin una cosa ni la otra.

### 3.3 `GeometriaFactory-Application`

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-04001 | Crear el proyecto de código y su proyecto de pruebas, con una sola dependencia saliente | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.1.P.1 · GeometriaFactory-Application; [`ADR-04001`](../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación; el archivo de proyecto declara exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web; el proyecto de pruebas existe y corre vacío | **Infraestructura compartida**: la sostiene [`ADR-04001`](../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md). Habilita a las 32 |
| BT-04002 | Fijar los nombres de tipos, de espacios de nombres y **el del cuarto puerto**, y validarlos en el punto de control | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01` y `PA-02`; `05` §3.4 y §9, sexto riesgo; `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application | BT-04001 | Existe una propuesta de nombres para los tipos, los espacios de nombres y **el cuarto puerto, el de repositorio de cuentas**, que ninguna fuente nombra; el Product Owner y el equipo la aceptan o la corrigen **en el punto de control de la etapa `a`**; la decisión queda registrada. **El puerto no se agrega ni se quita: son cuatro**, y lo que se decide es su nombre. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los cuatro componentes que consumen el cuarto puerto dependen de que su nombre esté fijado. `05` §9 le asigna probabilidad **alta** al retrabajo si se fija sin punto de control |
| BT-04003 | Elegir y anclar la herramienta que calcula la versión | indagación | EP-T01 | `a` | Media | Sin fijar | `05` §11 `PA-06`; `PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Application, declarado idéntico a §17.1.P.7 · GeometriaFactory-Domain | BT-04001 | La herramienta está elegida y su versión anclada según la regla de anclaje de versiones del producto; el cálculo a partir de las convenciones de mensaje de confirmación produce un resultado reproducible. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la exige la estrategia de versionado del intake |
| BT-04004 | Puerta bloqueante de dependencias salientes | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de dependencias salientes; `05` §9, primer riesgo | BT-04001 | La inspección del archivo de proyecto es parte de la revisión y **bloquea la fusión** si aparece una dependencia nueva; la puerta se mide **en cada etapa** y no sólo en la `a` | **Infraestructura compartida**: sostiene la propiedad que justifica el estilo entero |
| BT-04005 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de advertencias de construcción; `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Application | BT-04001 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-04006 | Puerta propia de cero pruebas que tocan la base de datos real | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Application (puerta propia y bloqueante); `05` §5 y §8, fila correspondiente; `05` §9, primer riesgo | BT-04001 | Exactamente **0** pruebas de esta capa abren la base de datos real; la pirámide del proyecto de código es **100 %** unitaria; una prueba que la toque **está mal ubicada** y pertenece a la batería de integración de `GeometriaFactory-Api`. La puerta bloquea la fusión | **Infraestructura compartida**: es lo que hace verificable la autorización por pertenencia sin base, que es lo que la fuente exige probar |
| BT-04007 | Declarar los cuatro puertos como frontera de este proyecto de código | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.1, componente «Declaración de puertos», y §3.4; [`ADR-04002`](../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md); [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 | BT-04001, BT-04002 | Los puertos son exactamente **cuatro** —repositorio de trabajos, validación de figuras, reloj del sistema y repositorio de cuentas—; son declaraciones **sin implementación** en este proyecto de código; **este proyecto de código no nombra ni referencia a `GeometriaFactory-Infrastructure`**; la conexión con los adaptadores es de la composición de raíz y no de acá | US-04008, US-04010, US-04015, US-04017, US-04019, US-04020, US-04029, US-04031 |
| BT-04008 | Construir el resultado tipado y cerrar el catálogo de las 36 condiciones en las dos direcciones | feature | EP-T02 | `f` | Alta | Sin fijar | [`ADR-04006`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); `05` §7, fila de manejo de errores; `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) | BT-04001, BT-04002 | Toda condición prevista viaja como **valor de retorno** con su código estable y nunca como excepción de control de flujo; las excepciones quedan reservadas a defectos de programación del consumidor; **100 %** de las **36** condiciones alcanzadas por al menos una prueba y **0** condiciones emitidas que no figuren en el catálogo, comparado **en las dos direcciones** | US-04002, US-04007, US-04014, US-04016, US-04025, US-04030 y, por herencia de forma, las 32 |
| BT-04009 | Fijar el alcance de la unidad de trabajo: un caso de uso, una unidad | feature | EP-T02 | `e` | Alta | Sin fijar | [`ADR-04005`](../05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md); `05` §4, segunda viñeta; `05` §8, fila de unidades de trabajo por caso de uso | BT-04007 | Cada caso de uso abre **a lo sumo 1** unidad de trabajo y **0** reparten su efecto entre dos; el mecanismo lo provee el adaptador y el **alcance** lo fija esta capa; el arrastre de la baja es el caso testigo y se verifica con una prueba | US-04006, US-04010, US-04012, US-04013, US-04023, US-04026, US-04029 |
| BT-04010 | Construir la guarda de autorización con las cuatro comprobaciones en orden fijo | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guarda de autorización»; [`ADR-04004`](../05-Arquitectura-Tecnica/Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md); [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 | BT-04001, BT-04002, BT-04008 | Las **cuatro** comprobaciones —cambio de contraseña pendiente, pertenencia, facultad y alcance del administrador— viven en **un único componente**, en el orden fijo de la ADR y **sobre el dato ya recuperado, antes de escribir**; la guarda **no lee conjuntos y no escribe**; la negativa por pertenencia y la negativa por facultad **no se confunden**, y el trabajo ajeno y el identificador inexistente comparten motivo | US-04004, US-04007, US-04012, US-04020, US-04021, US-04022, US-04025, US-04026, US-04027, US-04029, US-04030 |
| BT-04011 | Armar la matriz de ejercicio de las cuatro comprobaciones, con la prueba de que la cuarta corta primero | docs | EP-T03 | `d` | Alta | Sin fijar | `05` §8, fila de ejercicio de las cuatro comprobaciones; `05` §9, segundo y tercer riesgo | BT-04010 | **4 de 4** comprobaciones con al menos una prueba que verifique su negativa, **sin base de datos**; **1** sola prueba que verifique que la cuarta **corta antes que las otras tres**; una prueba que pide un trabajo ajeno y comprueba que el motivo emitido es el de inexistencia y no el de falta de autorización; la matriz se entrega a 08 y se revisa al cerrar cada etapa | US-04025, US-04026, US-04030 |
| BT-04012 | Construir la orquestación del alta de cuentas | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del alta de cuentas»; `05` §10.3 `INV-01` e `INV-05` | BT-04007, BT-04010 | Los **dos** caminos de alta quedan separados y con estados iniciales opuestos; la unicidad del correo y la existencia previa de una cuenta con papel `Administrador` **se resuelven por el puerto de repositorio de cuentas** y llegan al dominio ya resueltas; el auto-registro **rechaza el papel `Administrador`** | US-04001, US-04002, US-04003, US-04028 |
| BT-04013 | Construir la orquestación del gobierno de cuentas | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del gobierno de cuentas»; `05` §10.2, filas de RN-04007, RN-04012, RN-04015 y RN-04016 | BT-04007, BT-04010, BT-04009 | Las **cuatro** operaciones de admisión y el reseteo viven acá; la baja compara el correo escrito y retira todos los trabajos **en la misma unidad de trabajo**; **habilitar y rehabilitar** piden la provisoria, la derivan afuera y solicitan fijar la credencial derivada, dejando la marca puesta; el reseteo **no comprueba el estado de la cuenta** y **no dispara ningún retiro** | US-04004, US-04005, US-04006, US-04008, US-04029, US-04031 |
| BT-04014 | Construir la orquestación del ingreso y la credencial | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del ingreso y la credencial»; `05` §10.3 `INV-06` e `INV-09` | BT-04007, BT-04010 | La consulta de admisibilidad devuelve el motivo **sin colapsarlo**; la credencial llega **ya derivada** y esta capa **no ve valores en claro**; el reemplazo por la propia cuenta es **el único lugar donde la marca se levanta**, y se levanta sólo con el cambio efectivo | US-04007, US-04008, US-04009, US-04032 |
| BT-04015 | Construir la orquestación del trabajo | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del trabajo»; `05` §10.2, filas de RN-04004, RN-04005, RN-04008 y RN-04009 | BT-04007, BT-04009, BT-04010 | Constituir, reeditar, enviar y retirar quedan acá; el texto original se entrega tal cual y **no se reescribe ni cuando la interpretación falla**; el envío entrega al dominio el conjunto de observaciones **completo y con su especie** y **no decide el estado**; el retiro tiene sus **dos** alcances opuestos | US-04010, US-04011, US-04012, US-04013, US-04014, US-04015, US-04016, US-04026, US-04027 |
| BT-04016 | Construir la orquestación de la consulta, con la proyección sin componentes | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Orquestación de la consulta»; `05` §6, las dos decisiones sobre la forma de la consulta; `05` §8, fila de componentes de pieza en el listado; [`Contracts ADR-08005`](../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md) | BT-04007, BT-04010 | Las dos consultas salen con su **predicado de alcance ya trasladado a la consulta** y no filtrado en memoria; **0** componentes de pieza cargados en el listado del alumno y en el de la comisión; el detalle sí los trae; el detalle del administrador es **equivalente** al del alumno | US-04017, US-04018, US-04019, US-04020, US-04021, US-04022 |
| BT-04017 | Construir la orquestación del desenlace | feature | EP-T04 | `h` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del desenlace»; `05` §10.3 `INV-07` | BT-04007, BT-04010 | Aprobar y rechazar proceden **sólo desde el estado `Pendiente`**, con comentario opcional; la facultad se verifica **antes** de pedir la transición, de modo que el rechazo por facultad no se confunda con el rechazo por terminalidad; la terminalidad se propaga | US-04023, US-04024, US-04025 |
| BT-04018 | Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, filas de tiempo del caso de uso más pesado y de cobertura; `05` §11 `PA-05`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-04005, BT-04006 | El Product Owner confirma o corrige los **500 ms** y la cobertura **sobre su propio documento**; hasta entonces se usan como vigentes y la puerta **no se declara bloqueante** en 09. **Ninguna de las dos salidas es inventar un número acá.** **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida**: condiciona la puerta del pipeline de todas las historias |
| BT-04019 | Medir el tiempo del caso de uso más pesado sobre el escenario `E-1`, sin acceso a base | devops | EP-T05 | `f` | Media | Sin fijar | `05` §8, primera fila; `PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Application y §20 `E-1` | BT-04006, BT-04015, BT-04018 | La medición se hace sobre la batería unitaria **con doble del puerto de validación** y **sin acceso a base**, que es lo que la hace atribuible a esta capa y no al adaptador; el material es el texto de **3** piezas del escenario `E-1` del intake §20 y **no se inventa ningún texto de prueba** | US-04013, US-04014, US-04015 |
| BT-04020 | Elevar los sellos de alta, de modificación y de desenlace al Product Owner | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §11 `PA-04`; `05` §6, cuarta viñeta; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 | BT-04013, BT-04015 | Queda registrado que el intake los sostiene como verificables en prueba y que **el modelo del dominio no los declara como atributos**; hasta que el Product Owner resuelva, esta capa los trata como **metadatos de orquestación** y no como atributos del dominio. **Esta tarea no resuelve la discrepancia: la eleva con su plazo.** **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la titularidad es del Product Owner y de `GeometriaFactory-Domain` |
| BT-04021 | Acompañar la decisión del criterio de comparación de dos correos | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §11 `PA-03`; `RN-04002`, `INV-01`; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §11 | BT-04012 | Queda decidido si dos correos se comparan tal cual o normalizados, **y dónde se normaliza**. **La decisión no es de este proyecto de código**: `05` §11 `PA-03` la derivó a la categoría 05 de `GeometriaFactory-Infrastructure`, que es la que materializa el índice; esta tarea aporta el requisito de la orquestación del alta y **adopta el criterio que aquella fije**. **Caja temporal: antes de cerrar la etapa `d`** | US-04002 |

**Ocho tareas se justifican como infraestructura compartida** —BT-04001, BT-04002, BT-04003, BT-04004, BT-04005, BT-04006, BT-04018 y BT-04020— y las **trece** restantes declaran al menos una historia consumidora. Ninguna queda sin una cosa ni la otra.

### 3.4 `GeometriaFactory-Infrastructure`

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-06001 | Crear el proyecto de código y su proyecto de pruebas | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.1.P.1 · GeometriaFactory-Infrastructure; [`ADR-06001`](../05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación, con sus **dos** dependencias de compilación y ninguna más; el proyecto de pruebas existe y corre vacío; **la integración contra el almacén real pertenece a `GeometriaFactory-Api`** y no a este proyecto de pruebas | **Infraestructura compartida**: habilita a las 25 |
| BT-06002 | Fijar los nombres de tipos y de espacios de nombres, y el criterio de nombrado del adaptador de cuentas | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01` y `PA-02`; [`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6 | BT-06001 | Los nombres quedan decididos y registrados en el punto de control. **El identificador del cuarto puerto no se fija acá**: lo declara `GeometriaFactory-Application` y su ADR-06002 lo ató a ese mismo punto de control; esta tarea aporta el **criterio de nombrado del adaptador**, que es lo que sí le corresponde. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los adaptadores dependen de que el nombre del puerto esté fijado |
| BT-06003 | Anclar la función de derivación de clave y sus parámetros versionados | indagación | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Infrastructure, que declara **dos candidatas y no elige**; `05` §11 `PA-03`; [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) §7 | BT-06001 | La función queda **elegida** y su versión anclada según la regla de anclaje del producto; **los parámetros se versionan junto al valor derivado** y **no hay valor por defecto silencioso**; la elección aplica el criterio que `ADR-06004` §7 fija. **Es una decisión de este proyecto de código y no se delega**: el intake se la asigna. **Caja temporal: la etapa `a`** | US-06017, US-06018 |
| BT-06004 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §5, puertas propias; `05` §8, última fila; `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Infrastructure | BT-06001 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-06005 | Construir el contexto de persistencia y el mapeo de las cinco entidades | feature | EP-T02 | `a` | Alta | Sin fijar | `05` §3.1, componente transversal; `05` §6; [`Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md); [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) | BT-06001, BT-06002 | Las **cinco** entidades del modelo conceptual tienen su correspondencia en el esquema físico, con sus tipos, sus índices y sus restricciones; **el esquema no lleva ninguna columna de pertenencia a instancia** —una instancia, un curso, un administrador—; el modo de diario con registro por delante y el **escritor único** quedan declarados; una escritura concurrente rechazada termina en su condición y **no en espera activa** | US-06009, US-06014, US-06016, US-06024 |
| BT-06006 | Construir la preparación del almacén con linaje inmutable y arranque detenido | feature | EP-T02 | `a` | Alta | Sin fijar | `05` §3.1 (mecanismo de arranque), §4 última viñeta; [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); `05` §9, riesgos quinto y sexto | BT-06005 | Las transformaciones **se aplican solas al arrancar** sobre un almacén inexistente o desactualizado; **una transformación ya fusionada no se edita**; ante un esquema que no corresponde **el arranque se detiene** y **jamás se descarta el almacén para crearlo de nuevo**; ante una ruta no disponible el arranque también se detiene y **no cae hacia ninguna ruta alternativa dentro de la imagen**. **No hay modo de sólo lectura ni arranque parcial** | US-06024, US-06025 |
| BT-06007 | Puerta de transformaciones aplicadas sobre un almacén inexistente | devops | EP-T02 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Infrastructure; `05` §5 y §8, fila correspondiente; `Roadmap-Producto.md` §5.2, transición `a` → `b` (`PT-04`) | BT-06006 | **1 de 1** intento exitoso, **sin paso manual**, sobre un almacén recién creado; es la cuarta etapa del pipeline y es **propia de este proyecto de código**; forma parte de lo que `PT-04` mide en la etapa `a` | **Infraestructura compartida**: es una puerta del producto |
| BT-06008 | Fijar la zona horaria y la precisión de los sellos | feature | EP-T02 | `a` | Media | Sin fijar | `05` §7, fila de zona horaria, que **cierra un punto abierto de la categoría 02**; [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) §2; `RC-06006` | BT-06005 | Los sellos **se producen y se guardan en tiempo universal coordinado**, con la precisión que el puerto de reloj entrega y **sin truncarla**; la conversión a la zona de quien lee **es de la superficie que lo muestra** y no de acá; los **tres** sellos de tiempo del trabajo se distinguen y no se confunden | US-06009, US-06023 |
| BT-06009 | Construir el adaptador de repositorio de cuentas con el índice único | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente correspondiente; [`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md); `05` §9, último riesgo; `RC-06007` | BT-06005, BT-06002 | Recupera una cuenta por su correo, responde las **dos** preguntas sobre el conjunto y materializa el resultado **incluida la marca**; el **índice único sobre la forma normalizada del correo** es la segunda línea de la unicidad, con su condición declarada como camino y no como accidente; **el criterio de comparación de dos correos queda decidido acá**, que es lo que las dos capas de adentro derivaron a esta categoría | US-06014, US-06015, US-06016 |
| BT-06010 | Construir el adaptador de repositorio de trabajos con la proyección separada del detalle | feature | EP-T03 | `e` | Alta | Sin fijar | `05` §3.1, componente correspondiente; `05` §8, fila de componentes cargados; [`Contracts ADR-08005`](../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md); `RC-06001`, `RC-06002` | BT-06005 | Resuelve la consulta **ya acotada** y **no resuelve ninguna sin recorte declarado**; tiene **dos** formas de lectura, proyección y detalle; **0** componentes cargados y **0** apariciones del texto original en la proyección; **0** escrituras aceptadas que reemplacen el texto original conservado | US-06008, US-06009, US-06010, US-06011 |
| BT-06011 | Construir el retiro físico con todo o nada y el arrastre de la baja | feature | EP-T03 | `e` | Alta | Sin fijar | `05` §3.1; [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md); `RC-06005`; `05` §8, fila de retiros parciales | BT-06009, BT-06010 | El retiro es **físico**, sin marca de borrado lógico; **0** retiros parciales tras una baja interrumpida: o se retira la cuenta con todos sus trabajos, o no se retira nada; es **la única operación destructiva del producto** y por eso su criterio es que **no queda nada** | US-06012, US-06013 |
| BT-06012 | Construir el adaptador de reloj del sistema | feature | EP-T03 | `c` | Media | Sin fijar | `05` §3.1, componente correspondiente; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Application punto 3 | BT-06001 | Devuelve el momento actual y **no depende del contexto de persistencia**; es el contrato más corto de la capa y **el que hace reproducibles los sellos en prueba**: con un doble, la batería de las capas de adentro no necesita fijar el reloj del entorno | US-06023 |
| BT-06013 | Construir el mecanismo de derivación y verificación de credenciales | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Mecanismo de credenciales»; [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md); `05` §7, filas de autenticación y de secretos | BT-06003 | La contraseña **nunca se guarda ni se registra en claro**; la verificación distingue el **valor derivado ilegible** de la contraseña equivocada; **no depende del contexto de persistencia** y se prueba unitariamente; los parámetros de derivación **llegan desde la composición de raíz y no se buscan** | US-06017, US-06018 |
| BT-06014 | Construir la producción de la contraseña provisoria, no adivinable y sin repetirse | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1; [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md); `05` §7, fila de producción de la provisoria; `05` §9, tercer riesgo; `RN-06014`, `RN-06016` | BT-06013 | El valor sale **íntegramente de la fuente de material impredecible del sistema**, con la longitud y el alfabeto que `ADR-06005` fija; **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y **ninguna derivable del nombre, del correo ni de la fecha**; **la invocación no lleva ningún dato del acto que la motiva**, de modo que no puede distinguir habilitación de reseteo; **el valor no se registra en ninguna traza**. **Atajo prohibido y escrito: componer el valor por un contador, la fecha o el correo cuando la fuente no responde** | US-06019, US-06020 |
| BT-06015 | Construir el mecanismo de acceso firmado con la clave que recibe y no busca | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1; `05` §7, fila de configuración; `05` §9, cuarto riesgo; `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Infrastructure | BT-06001 | Emite y verifica el acceso con sus **cuatro** reclamos; la clave de firma **se recibe desde afuera y no se busca**: si no llega, la condición correspondiente y **0** accesos emitidos; **jamás se genera una clave al vuelo y jamás se emite sin firmar**; la clave **no entra a ningún mensaje ni a ninguna traza** | US-06021, US-06022 |
| BT-06016 | Construir el motor de interpretación con las cuatro trampas del formato | feature | EP-T05 | `f` | Alta | Sin fijar | `05` §3.1, componente correspondiente; [`ADR-06006`](../05-Arquitectura-Tecnica/Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md); [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md); `Definicion-Contrato-Del-Validador-De-Figuras.md`; `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Infrastructure punto 1 | BT-06001 | Las **cuatro** trampas `T1` a `T4` están escritas **antes de leer texto**: claves sinónimas del ortoedro, comas finales y omisión de comentarios, caras admitidas en sus dos formas, y **los valores calculados erróneos no se rechazan: se señalan**. Devuelve la **cantidad de figuras del conjunto raíz** incluidas las no reconstruidas, y **reserva la posición** de las que no pudo reconstruir. **No abre el almacén y no hace red** | US-06001, US-06002, US-06003, US-06004 |
| BT-06017 | Construir el motor de verificación con tolerancia 0.01 y operador estricto | feature | EP-T05 | `f` | Alta | Sin fijar | `05` §3.1; `05` §7, fila de comparación de valores; `05` §8, fila de tolerancia; `PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Infrastructure | BT-06016 | Se advierte cuando la diferencia absoluta es **mayor** que **0.01**, **nunca mayor o igual**. **No es asunción**: la fuente lo fija con su fundamento, y con «mayor o igual» el escenario `E-1` daría **3** advertencias en lugar de las **2** documentadas. Exige **las piezas ya reconstruidas**: sin ellas devuelve su condición y no «0 advertencias» | US-06005, US-06006, US-06007 |
| BT-06018 | Correr la batería de diez casos con los ocho escenarios como entrada | devops | EP-T05 | `f` | Alta | Sin fijar | `05` §8, fila de casos que pasan, y §10.5; `PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure, §20 y §21 | BT-06016, BT-06017 | **10 de 10** casos pasan, con los **ocho** escenarios `E-1` a `E-8` como entrada; la batería es **unitaria y sin almacén**; **la cobertura del validador alcanza el mínimo declarado, que es el número más alto del producto**; **no se inventa ningún texto de prueba**. `E-7` no respalda ninguno de los diez y **se usa igual como cobertura adicional declarada**, porque es el único texto que ejercita los **seis** tipos reconstruibles | US-06001 a US-06007 |
| BT-06019 | Fijar la tabla de derivación por tipo, incluida el área de una pieza volumétrica | indagación | EP-T05 | `f` | Media | Sin fijar | [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5; `CU-06002` §10; `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | BT-06017 | La tabla queda escrita, tipo por tipo. Para el área de una pieza volumétrica se adopta la **suma de los componentes**, que es lo que la fuente muestra dos veces, y **se declara como derivación** de la categoría 02 y no como transcripción; las dos formas coinciden en el caso donde se cruzan. **Caja temporal: al abrir la etapa `f`** | US-06005, US-06007 |
| BT-06020 | Puerta de cero peticiones de red originadas por los dos motores | devops | EP-T05 | `f` | Alta | Sin fijar | `05` §8, fila correspondiente; `PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Infrastructure, que declara que **el validador no hace red** | BT-06016, BT-06017 | Exactamente **0** peticiones de red originadas por los dos motores; se verifica por **inspección de sus dependencias** y con el criterio de aceptación correspondiente del contrato de uso. Es el **reflejo estructural** de `RA-02` en esta capa, que **no la alcanza** pero que la respeta desde afuera | **Infraestructura compartida**: sostiene que el validador reciba texto y devuelva observaciones, y nada más |
| BT-06021 | Cerrar el catálogo de las 17 condiciones en las dos direcciones | feature | EP-T06 | `d` | Alta | Sin fijar | `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §1.2 y §1.3 | BT-06009, BT-06013, BT-06015 | **100 %** de las **17** condiciones alcanzadas por al menos una prueba y **0** condiciones emitidas que no figuren en el catálogo, comparado **en las dos direcciones**; **ninguna condición es un código de protocolo**: su traducción es de `GeometriaFactory-Api`; la separación entre **resultado** y **fallo** queda ejercida, porque confundirlos haría que un texto ilegible pareciera un servicio caído | US-06004, US-06018, US-06020, US-06022, US-06025 |
| BT-06022 | Prueba de inspección de que ningún mensaje ni traza lleva secreto, ruta ni texto del alumno | devops | EP-T06 | `d` | Alta | Sin fijar | `05` §7, fila de secretos y datos que no se registran; `05` §8, fila correspondiente; `05` §10.4, `RA-03` | BT-06021 | Exactamente **0** mensajes y **0** trazas contienen la clave de firma, la contraseña en claro, el valor derivado de una credencial, la contraseña provisoria producida o la ruta del almacén; y **0** contienen el **texto original del alumno**, que no es secreto y tampoco entra. Se verifica **en las dos direcciones**, sobre las 17 condiciones y sobre el registro del servidor. **`RA-03` es la única de las tres reglas de arquitectura con tramo acá, y es de disciplina y no de ignorancia**: esta capa **conoce** las tres cosas | **Infraestructura compartida**: es la contracara de que todo error mostrado quede registrado del lado del servidor |
| BT-06023 | Confirmar los valores rotulados como asunción y fijar las tres puertas de cobertura | indagación | EP-T06 | `d` | Media | Sin fijar | `05` §8, tres primeras filas; `05` §11 `PA-11`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-06004, BT-06018 | El Product Owner confirma o corrige los **200 ms** y las **tres** coberturas **sobre su propio documento**; hasta entonces se usan como vigentes y las puertas **no se declaran bloqueantes** en 09. **Ninguna de las salidas es inventar un número acá.** **Caja temporal: antes de fijar las puertas en 09** | **Infraestructura compartida**: condiciona las puertas del pipeline de todas las historias |
| BT-06024 | Elevar hasta dónde llega el conjunto de tipos reconstruibles | indagación | EP-T06 | `f` | Media | Sin fijar | `05` §11 `PA-04`; `02` §11 | BT-06016 | Queda declarado si alguna clase de la actividad emite un tipo fuera de los **seis** que los escenarios ejercitan. Hoy **ninguna fuente enumera las clases**, y un tipo fuera del conjunto produce error de validación, que es correcto **pero puede no ser lo deseado**. **Esta tarea eleva y no decide.** **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-06025 | Elevar la forma de sostener que la provisoria «no se repite» | indagación | EP-T06 | `d` | Media | Sin fijar | `05` §11 `PA-06`; `CU-06007` §10 | BT-06014 | Queda registrado que la propiedad la sostiene **la impredecibilidad** y que se **descartó** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas y **el producto no guarda contraseñas en claro**. Es una **decisión derivada y no una transcripción**, y se eleva para que el Product Owner la confirme o la reemplace. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-06026 | Elevar la frecuencia del respaldo y la fecha de última modificación de la cuenta | indagación | EP-T06 | `d` | Baja | Sin fijar | `05` §11 `PA-07` y `PA-09`; `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Infrastructure | BT-06005 | Queda registrado que la **frecuencia del respaldo** la fuente la declara explícitamente «a definir por el docente» —**no es una omisión de esta categoría**— y que la **fecha de última modificación de la cuenta** el modelo del dominio **no la declara**, de modo que si el Product Owner la quisiera **entraría por el dominio y no por acá**. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: las dos decisiones son del Product Owner, con `09-Devops` y con `GeometriaFactory-Domain` |

**Diez tareas se justifican como infraestructura compartida** —BT-06001, BT-06002, BT-06004, BT-06007, BT-06020, BT-06022, BT-06023, BT-06024, BT-06025 y BT-06026— y las **dieciséis** restantes declaran al menos una historia consumidora. **Diez más dieciséis son veintiséis**, y ninguna queda sin una cosa ni la otra.

## 4. Trazabilidad BT ↔ US ↔ CU

### 4.1 `GeometriaFactory-Api`

Las veintiséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5 y los puntos de acceso los de `05` §3.4.

| BT | US que la consumen | CU upstream | Puntos de acceso que toca | Fuente de arquitectura |
| --- | --- | --- | --- | --- |
| BT-00001 | Infraestructura compartida (habilita a las 30) | CU-00001 a CU-00012 | Los quince | `05` §5 |
| BT-00002 | US-00026 | CU-00010 | Ninguno: construye el grafo y desaparece | ADR-00006 |
| BT-00003 | US-00027, US-00028, US-00029 | CU-00011 | A-16 | ADR-00007 |
| BT-00004 | Infraestructura compartida | CU-00011 | A-16 | `05` §5, `PT-04` |
| BT-00005 | Infraestructura compartida | CU-00001 a CU-00012 | Los quince | `05` §11 `PA-07` |
| BT-00006 | Infraestructura compartida | — (puerta de construcción) | Ninguno | `05` §8 |
| BT-00007 | Infraestructura compartida | CU-00001, CU-00003 a CU-00008, CU-00011 | Los quince | `05` §11 `PA-01` |
| BT-00008 | US-00019, US-00022, US-00024 | CU-00006, CU-00007, CU-00009 | Los quince | ADR-00002 |
| BT-00009 | US-00019 | CU-00006 | A-10, A-11 | ADR-00002 §2 punto 6 |
| BT-00010 | US-00001, US-00004 | CU-00001, CU-00002 | A-01, y los once bajo la guardia | ADR-00003 |
| BT-00011 | US-00004, US-00005, US-00006 | CU-00002 | Los **once** bajo la guardia | ADR-00003 |
| BT-00012 | Infraestructura compartida | CU-00002 | Los quince, en las dos direcciones | `05` §8, ADR-00003 |
| BT-00013 | US-00024, US-00025 | CU-00009 | Los quince | ADR-00004 |
| BT-00014 | US-00002, US-00020, US-00021 | CU-00001, CU-00006, CU-00007, CU-00009 | A-01, A-02, A-12, A-13, A-14 | ADR-00004 |
| BT-00015 | Infraestructura compartida | CU-00009 | A-06, A-07, A-08, A-09, A-10, A-11, A-13, A-14 | `05` §11 `PA-02` y `PA-03`, resueltos |
| BT-00016 | US-00001, US-00002, US-00003, US-00007, US-00008, US-00009, US-00010 | CU-00001, CU-00003 | A-01, A-02, A-03, A-05 | `05` §3.1, superficie de acceso |
| BT-00017 | US-00011, US-00012, US-00013, US-00014, US-00015, US-00016 | CU-00004, CU-00005 | A-06, A-07, A-08, A-09 | `05` §3.1, superficie de gobierno |
| BT-00018 | US-00017, US-00018, US-00019, US-00020, US-00021, US-00022 | CU-00006, CU-00007 | A-10, A-11, A-12, A-13, A-14 | `05` §3.1, superficie de trabajos |
| BT-00019 | US-00023 | CU-00008 | A-15 | `05` §3.1, superficie de desenlace |
| BT-00020 | US-00030 | CU-00012 | Los quince, por ejercicio | ADR-00008, `PRODUCT-INTAKE` §16.1 |
| BT-00021 | Infraestructura compartida | CU-00012 | Ninguno | `05` §11 `PA-06`, resuelto |
| BT-00022 | Infraestructura compartida | CU-00001 a CU-00011 | Los quince | `05` §8, ADR-00001 |
| BT-00023 | US-00020 | CU-00006 | A-12 | `05` §8, criterio bloqueante de la fuente |
| BT-00024 | US-00019 | CU-00006 | A-10, A-11 | ADR-00002, `RN-00008` |
| BT-00025 | Infraestructura compartida | — (puertas de cobertura y de tiempo) | Ninguno | `05` §11 `PA-09` |
| BT-00026 | Infraestructura compartida | — (canal de entrega) | Ninguno | `05` §11 `PA-08` |

**Cobertura inversa: los doce casos de uso tienen al menos una tarea técnica que los realiza.** CU-00001 en BT-00010, BT-00014, BT-00016 y BT-00022; CU-00002 en BT-00010, BT-00011, BT-00012 y BT-00022; CU-00003 en BT-00007, BT-00016 y BT-00022; CU-00004 en BT-00007, BT-00017 y BT-00022; CU-00005 en BT-00007, BT-00017 y BT-00022; CU-00006 en BT-00008, BT-00009, BT-00014, BT-00018, BT-00023 y BT-00024; CU-00007 en BT-00008, BT-00014, BT-00018 y BT-00022; CU-00008 en BT-00007, BT-00019 y BT-00022; CU-00009 en BT-00008, BT-00013, BT-00014 y BT-00015; CU-00010 en BT-00002; CU-00011 en BT-00003 y BT-00004; CU-00012 en BT-00020 y BT-00021.

**Cobertura de los quince puntos de acceso.** A-01, A-02, A-03 y A-05 en BT-00016; A-06, A-07, A-08 y A-09 en BT-00017; A-10, A-11, A-12, A-13 y A-14 en BT-00018; A-15 en BT-00019; A-16 en BT-00003. **Los quince tienen tarea técnica**, y **los quince** quedan además recorridos por BT-00012 —contra la guardia— y por BT-00013 —contra la tabla de traducción—. **`A-04` no figura porque está retirado y no se recicla**: establecía la contraseña del primer ingreso sin credencial, y `RN-00016` suprimió esa operación en lugar de resolverla.

**Cobertura de los ocho componentes de `05` §3.1.** Composición de raíz en BT-00002; Guardia de admisión en BT-00011 y BT-00012; Traductor de motivos y códigos en BT-00013, BT-00014 y BT-00015; Superficie de acceso y credencial propia en BT-00016; Superficie de gobierno de la comisión en BT-00017; Superficie de trabajos en BT-00018; Superficie de desenlace en BT-00019; Arranque y salud en BT-00003 y BT-00004. **Los ocho tienen tarea técnica**, y `CU-00012` sigue **sin componente**, como `05` §3.3 declara: BT-00020 produce un artefacto del árbol de muestras y no código de producción.

### 4.2 `GeometriaFactory-Domain`

Las dieciséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-02001 | Infraestructura compartida (habilita a las 27) | CU-02001 a CU-02013 | ADR-02001 |
| BT-02002 | Infraestructura compartida | CU-02001 a CU-02013 | `05` §11 PA-01 |
| BT-02003 | Infraestructura compartida | — (no realiza ningún caso de uso: es la estrategia de versionado) | `05` §11 PA-04 |
| BT-02004 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-02001 |
| BT-02005 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-02003 |
| BT-02006 | US-02001, US-02009, US-02011, US-02012, US-02024 | CU-02001, CU-02005, CU-02006, CU-02007, CU-02012 | `05` §3.1, núcleo de entidades |
| BT-02007 | US-02002, US-02007 | CU-02001, CU-02003 | ADR-02002, Contratos-Abstractions |
| BT-02008 | US-02002, US-02013, US-02014, US-02016 | CU-02001, CU-02007, CU-02008 | `05` §8, catálogo de condiciones |
| BT-02009 | US-02003, US-02009 | CU-02001, CU-02005 | ADR-02006 |
| BT-02010 | US-02001, US-02004, US-02005, US-02006, US-02024, US-02025, US-02026 | CU-02001, CU-02002, CU-02003, CU-02012, CU-02013 | `05` §3.1, guardas de cuenta |
| BT-02011 | US-02006, US-02008, US-02026, US-02027 | CU-02002, CU-02003, CU-02004, CU-02013 | ADR-02005 |
| BT-02012 | US-02005, US-02010, US-02015, US-02016, US-02017, US-02018, US-02019, US-02020, US-02021, US-02022, US-02023 | CU-02002, CU-02005, CU-02008, CU-02009, CU-02010, CU-02011 | `05` §3.1, máquina de estados |
| BT-02013 | US-02010, US-02011, US-02013, US-02014, US-02016 | CU-02005, CU-02006, CU-02007, CU-02008 | `05` §3.1, adopción de la interpretación |
| BT-02014 | US-02008, US-02017, US-02023, US-02025, US-02027 | CU-02004, CU-02008, CU-02011, CU-02012, CU-02003 | `05` §8, ejercicio de los invariantes |
| BT-02015 | Infraestructura compartida | — (puerta de cobertura y de tiempo) | `05` §11 PA-02 |
| BT-02016 | US-02003 | CU-02001 | `02` §9, punto abierto |

**Cobertura inversa: los trece casos de uso tienen al menos una tarea técnica que los realiza.** CU-02001 en BT-02006, BT-02007, BT-02008, BT-02009, BT-02010 y BT-02016; CU-02002 en BT-02010, BT-02011 y BT-02012; CU-02003 en BT-02007, BT-02010, BT-02011 y BT-02014; CU-02004 en BT-02011 y BT-02014; CU-02005 en BT-02006, BT-02009, BT-02012 y BT-02013; CU-02006 en BT-02006 y BT-02013; CU-02007 en BT-02006, BT-02008 y BT-02013; CU-02008 en BT-02008, BT-02012, BT-02013 y BT-02014; CU-02009 en BT-02012; CU-02010 en BT-02012; CU-02011 en BT-02012 y BT-02014; CU-02012 en BT-02006, BT-02010 y BT-02014; CU-02013 en BT-02010 y BT-02011. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

### 4.3 `GeometriaFactory-Application`

Las veintiuna filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-04001 | Infraestructura compartida (habilita a las 32) | CU-04001 a CU-04011 | ADR-04001, `05` §5 |
| BT-04002 | Infraestructura compartida | CU-04001 a CU-04011 | `05` §11 `PA-01` y `PA-02`, ADR-04002 |
| BT-04003 | Infraestructura compartida | — (estrategia de versionado) | `05` §11 `PA-06` |
| BT-04004 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-04001 |
| BT-04005 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-04003 |
| BT-04006 | Infraestructura compartida | — (puerta propia del pipeline) | `05` §5 y §8, ADR-04002 |
| BT-04007 | US-04008, US-04010, US-04015, US-04017, US-04019, US-04020, US-04029, US-04031 | CU-04002, CU-04003, CU-04004, CU-04005, CU-04006, CU-04007, CU-04011 | ADR-04002, `05` §3.4 |
| BT-04008 | US-04002, US-04007, US-04014, US-04016, US-04025, US-04030 | CU-04001, CU-04003, CU-04005, CU-04008, CU-04011 | ADR-04006, `05` §8 |
| BT-04009 | US-04006, US-04010, US-04012, US-04013, US-04023, US-04026, US-04029 | CU-04002, CU-04004, CU-04005, CU-04008, CU-04009, CU-04011 | ADR-04005 |
| BT-04010 | US-04004, US-04007, US-04012, US-04020, US-04021, US-04022, US-04025, US-04026, US-04027, US-04029, US-04030 | CU-04002, CU-04003, CU-04004, CU-04006, CU-04007, CU-04008, CU-04009, CU-04011 | ADR-04004, `05` §3.1 |
| BT-04011 | US-04025, US-04026, US-04030 | CU-04008, CU-04009, CU-04011 | `05` §8, ejercicio de las cuatro comprobaciones |
| BT-04012 | US-04001, US-04002, US-04003, US-04028 | CU-04001, CU-04010 | `05` §3.1, alta de cuentas |
| BT-04013 | US-04004, US-04005, US-04006, US-04008, US-04029, US-04031 | CU-04002, CU-04011 | `05` §3.1, gobierno de cuentas |
| BT-04014 | US-04007, US-04008, US-04009, US-04032 | CU-04003 | `05` §3.1, ingreso y credencial |
| BT-04015 | US-04010, US-04011, US-04012, US-04013, US-04014, US-04015, US-04016, US-04026, US-04027 | CU-04004, CU-04005, CU-04009 | `05` §3.1, trabajo |
| BT-04016 | US-04017, US-04018, US-04019, US-04020, US-04021, US-04022 | CU-04006, CU-04007 | `05` §3.1 y §6, consulta |
| BT-04017 | US-04023, US-04024, US-04025 | CU-04008 | `05` §3.1, desenlace |
| BT-04018 | Infraestructura compartida | — (puerta de cobertura y de tiempo) | `05` §11 `PA-05` |
| BT-04019 | US-04013, US-04014, US-04015 | CU-04005 | `05` §8, primera fila |
| BT-04020 | Infraestructura compartida | CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010, CU-04011 | `05` §11 `PA-04` |
| BT-04021 | US-04002 | CU-04001, CU-04010 | `05` §11 `PA-03` |

**Cobertura inversa: los once casos de uso tienen al menos una tarea técnica que los realiza.** CU-04001 en BT-04008, BT-04012, BT-04020 y BT-04021; CU-04002 en BT-04007, BT-04009, BT-04010 y BT-04013; CU-04003 en BT-04007, BT-04008, BT-04010, BT-04014 y BT-04020; CU-04004 en BT-04007, BT-04009, BT-04010, BT-04015 y BT-04020; CU-04005 en BT-04007, BT-04008, BT-04009, BT-04015, BT-04019 y BT-04020; CU-04006 en BT-04007, BT-04010 y BT-04016; CU-04007 en BT-04007, BT-04010 y BT-04016; CU-04008 en BT-04008, BT-04009, BT-04010, BT-04011, BT-04017 y BT-04020; CU-04009 en BT-04009, BT-04010, BT-04011 y BT-04015; CU-04010 en BT-04012, BT-04020 y BT-04021; CU-04011 en BT-04007, BT-04008, BT-04009, BT-04010, BT-04011, BT-04013 y BT-04020. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

**Cobertura de los ocho componentes de `05` §3.1.** Guarda de autorización en BT-04010 y BT-04011; Declaración de puertos en BT-04007; Orquestación del alta de cuentas en BT-04012; Orquestación del gobierno de cuentas en BT-04013; Orquestación del ingreso y la credencial en BT-04014; Orquestación del trabajo en BT-04015; Orquestación de la consulta en BT-04016; Orquestación del desenlace en BT-04017. **Los ocho tienen tarea técnica y ninguna tarea construye un componente que la arquitectura no declare.**

### 4.4 `GeometriaFactory-Infrastructure`

Las veintiséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-06001 | Infraestructura compartida (habilita a las 25) | CU-06001 a CU-06010 | ADR-06001 |
| BT-06002 | Infraestructura compartida | CU-06001 a CU-06010 | `05` §11 `PA-01` y `PA-02`, ADR-06003 |
| BT-06003 | US-06017, US-06018 | CU-06006 | ADR-06004, `05` §11 `PA-03` |
| BT-06004 | Infraestructura compartida | — (puerta de construcción) | `05` §5 y §8 |
| BT-06005 | US-06009, US-06014, US-06016, US-06024 | CU-06003, CU-06005, CU-06010 | `05` §3.1, contexto de persistencia |
| BT-06006 | US-06024, US-06025 | CU-06010 | ADR-06007 |
| BT-06007 | Infraestructura compartida | CU-06010 | `05` §5, cuarta etapa del pipeline |
| BT-06008 | US-06009, US-06023 | CU-06003, CU-06009 | ADR-06002 §2, `RC-06006` |
| BT-06009 | US-06014, US-06015, US-06016 | CU-06005 | ADR-06003 |
| BT-06010 | US-06008, US-06009, US-06010, US-06011 | CU-06003 | `05` §3.1, adaptador de trabajos |
| BT-06011 | US-06012, US-06013 | CU-06004 | ADR-06002, `RC-06005` |
| BT-06012 | US-06023 | CU-06009 | `05` §3.1, adaptador de reloj |
| BT-06013 | US-06017, US-06018 | CU-06006 | ADR-06004 |
| BT-06014 | US-06019, US-06020 | CU-06007 | ADR-06005 |
| BT-06015 | US-06021, US-06022 | CU-06008 | `05` §3.1, mecanismo de acceso firmado |
| BT-06016 | US-06001, US-06002, US-06003, US-06004 | CU-06001 | ADR-06006, `Flujo-Ejecucion.md` |
| BT-06017 | US-06005, US-06006, US-06007 | CU-06002 | ADR-06006 |
| BT-06018 | US-06001 a US-06007 | CU-06001, CU-06002 | `05` §8 y §10.5 |
| BT-06019 | US-06005, US-06007 | CU-06002 | `Flujo-Ejecucion.md` §5 |
| BT-06020 | Infraestructura compartida | CU-06001, CU-06002 | `05` §8, fila de peticiones de red |
| BT-06021 | US-06004, US-06018, US-06020, US-06022, US-06025 | CU-06001, CU-06006, CU-06007, CU-06008, CU-06010 | `05` §8, cobertura del catálogo |
| BT-06022 | Infraestructura compartida | CU-06001 a CU-06010 | `05` §7 y §10.4, `RA-03` |
| BT-06023 | Infraestructura compartida | — (puertas de cobertura y de tiempo) | `05` §11 `PA-11` |
| BT-06024 | Infraestructura compartida | CU-06001 | `05` §11 `PA-04` |
| BT-06025 | Infraestructura compartida | CU-06007 | `05` §11 `PA-06` |
| BT-06026 | Infraestructura compartida | CU-06003, CU-06005 | `05` §11 `PA-07` y `PA-09` |

**Cobertura inversa: los diez casos de uso tienen al menos una tarea técnica que los realiza.** CU-06001 en BT-06016, BT-06018, BT-06020, BT-06021, BT-06022 y BT-06024; CU-06002 en BT-06017, BT-06018, BT-06019, BT-06020 y BT-06022; CU-06003 en BT-06005, BT-06008, BT-06010, BT-06022 y BT-06026; CU-06004 en BT-06011 y BT-06022; CU-06005 en BT-06005, BT-06009, BT-06022 y BT-06026; CU-06006 en BT-06003, BT-06013, BT-06021 y BT-06022; CU-06007 en BT-06014, BT-06021, BT-06022 y BT-06025; CU-06008 en BT-06015, BT-06021 y BT-06022; CU-06009 en BT-06008 y BT-06012; CU-06010 en BT-06005, BT-06006, BT-06007, BT-06021 y BT-06022.

**Cobertura de los ocho componentes de `05` §3.1.** Contexto de persistencia y mapeo en BT-06005 y BT-06008; Adaptador de repositorio de trabajos en BT-06010 y BT-06011; Adaptador de repositorio de cuentas en BT-06009 y BT-06011; Motor de interpretación de figuras en BT-06016; Motor de verificación de valores en BT-06017 y BT-06019; Adaptador de reloj del sistema en BT-06012; Mecanismo de credenciales en BT-06013 y BT-06014; Mecanismo de acceso firmado y preparación del almacén en BT-06015 y BT-06006. **Los ocho tienen tarea técnica.**

**Cobertura de las siete reglas conceptuales de modelo.** `RC-06001` en BT-06010; `RC-06002` en BT-06010 y BT-06016; `RC-06003` en BT-06005 y BT-06017; `RC-06004` en BT-06005; `RC-06005` en BT-06011; `RC-06006` en BT-06008; `RC-06007` en BT-06009 y BT-06005. **Las siete quedan materializadas y ninguna se enuncia acá**: las enuncia la categoría 02.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
