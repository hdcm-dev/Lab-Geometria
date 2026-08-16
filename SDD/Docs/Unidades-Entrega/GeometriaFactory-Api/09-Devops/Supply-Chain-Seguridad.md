# Seguridad de la cadena de suministro — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Supply-Chain-Seguridad.md
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

**Seis de las diez secciones son comunes; las otras cuatro nombran la preocupación propia de cada
capa** —la superficie expuesta en el host, la autorización en la aplicación, **las dos bibliotecas
sensibles** en la infraestructura, y qué de la política es propio y qué del producto en el dominio—.
Juntas dan la cadena de suministro de la entrega; por separado, cada una parecía completa.

---

## 1. Nota previa sobre el origen de este documento

**Ninguna fuente del producto declara política de cadena de suministro, y las tres capas lo
declararon por separado.** Se conservan las tres notas porque cada una precisa distinto de dónde sale
lo que su documento afirma.

### 1.1 `GeometriaFactory-Domain`

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto —ni el intake, ni las categorías 02 a 08 de este proyecto de código— declara política de cadena de suministro. `Rules-Devops.md` §2.1 la exige para los ocho tipos D8, de modo que **todo lo que este documento decide es una decisión de esta categoría y va declarada como tal**. No se le atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta: la convención del corpus es nombrar las herramientas por su función, y la elección concreta pertenece al punto de control de la etapa `a`.

### 1.2 `GeometriaFactory-Application`

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

### 1.3 `GeometriaFactory-Infrastructure`

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.
**Y una diferencia con las otras cuatro bibliotecas del producto.** Aquéllas declararon que su análisis de composición **no tiene sujeto**, porque no tienen dependencias externas. **Éste sí lo tiene**: el intake §17.1.P.1 · GeometriaFactory-Infrastructure declara **tres** dependencias core externas y **dos son sensibles**. Es la biblioteca del producto donde este documento más contenido real tiene.

## 2. Inventario de componentes

### 2.1 `GeometriaFactory-Api`

**Decisión de esta categoría: el inventario de esta unidad se emite en el stage `imagen`, sobre lo que la imagen efectivamente lleva.** Es el inventario que más importa del producto: la imagen es lo que corre en el servidor donde vive el dato.

| Qué entra a la imagen | De dónde viene | Quién lo ancla |
| --- | --- | --- |
| El entorno de ejecución de la plataforma, **sin kit de desarrollo ni depurador** | La imagen base de ejecución, **sin linaje con la del contenedor de desarrollo** | Esta categoría, en el archivo de construcción; la versión se ancla en la etapa `a` |
| Las dependencias core de este proyecto de código, incluida la de **acceso firmado** | Intake §17.1.P.1 · GeometriaFactory-Api | El equipo, en la etapa `a` |
| **Las dependencias externas de `GeometriaFactory-Infrastructure`**, que son **tres** y de las cuales **dos son sensibles** | Intake §17.1.P.1 · GeometriaFactory-Infrastructure | `GeometriaFactory-Infrastructure`; ver [`../../GeometriaFactory-Infrastructure/09-Devops/Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| `GeometriaFactory-Application`, `GeometriaFactory-Domain` y `GeometriaFactory-Contracts`, **sin dependencias externas propias** | Intake §17.1.P.1 · GeometriaFactory-Application, §17.1.P.1 · GeometriaFactory-Domain y §17.1.P.1 · GeometriaFactory-Contracts | — |
| El **bundle del visor** | **No entra.** Viaja en la otra unidad desplegable | Intake §13 |

**La tercera fila es la que obliga a que el inventario se tome sobre la imagen y no sobre el archivo de proyecto de este proyecto de código.** La mayor parte de las dependencias externas que llegan al servidor propio **no las declara este proyecto de código**: las trae `GeometriaFactory-Infrastructure`, y dos de ellas son las piezas más sensibles del producto. Un inventario tomado sobre la superficie propia describiría lo que menos riesgo tiene.

**La quinta fila es una separación que conviene tener escrita.** El motor de dibujo tridimensional **nunca llega al servidor donde vive el dato**: queda dentro del bundle, que viaja en la publicación del front. Es una consecuencia de la topología del intake §14 y **reduce a la mitad la superficie de terceros de esta unidad**.

| Aspecto del inventario | Decisión |
| --- | --- |
| Cuándo se emite | En el stage `imagen`, sobre la imagen construida para medir `PT-04` |
| Dónde se adjunta | Al **informe de cierre** de la etapa |
| Formato y generador | **No se nombran.** Ninguna fuente los declara y su elección es de la etapa `a`, por la regla de anclaje de versiones. Ver `PD-02` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |
| Qué **no** cubre | Lo que el destino agregue al reconstruir. Ver §3 |

### 2.2 `GeometriaFactory-Domain`

**Este proyecto de código no emite inventario propio, y el motivo es que no tiene componentes que inventariar.**

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Ninguna**. Es biblioteca de clases **sin dependencias core**: no referencia persistencia, ni marco web, ni bibliotecas de serialización | Intake §17.1.P.1 · GeometriaFactory-Domain |
| Referencias salientes admitidas | **0** a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | `05` §8, tercera fila; `QG-04` |
| Artefacto publicado | **Ninguno**: `redistribuible` es false y no se publica en ningún feed | Intake §13 y §17.1.P.7 · GeometriaFactory-Domain |

**Decisión: el inventario de componentes se emite en la unidad desplegable que embebe a esta biblioteca**, no acá. El inventario que le sirve a alguien es el de lo que sale del repositorio —la imagen del backend y la publicación del front—, y ahí es donde este proyecto de código aparece como un componente más, con su versión calculada según [`Estrategia-Versionado.md`](Estrategia-Versionado.md).

**Lo que sí aporta este proyecto de código al inventario del producto es una propiedad verificable y bloqueante: su fila del inventario no tiene hijos.** `QG-04` lo hace cumplir en cada pull request. Un inventario con una dependencia nueva colgando de esta biblioteca es, antes que un hallazgo de cadena de suministro, un incumplimiento del gate que justifica el estilo entero del proyecto de código.

### 2.3 `GeometriaFactory-Application`

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Ninguna.** La única dependencia core es `GeometriaFactory.Domain`, que es del mismo producto y a su vez no tiene ninguna | Intake §17.1.P.1 · GeometriaFactory-Application y §17.1.P.1 · GeometriaFactory-Domain |
| Referencias a otros proyectos de código del producto | Exactamente **1** | `QG-05`, con `TC-04027` |
| Referencias a bibliotecas de persistencia, transporte, serialización o marco web | **0** | `QG-05`, bloqueante |
| Artefacto publicado | **Ninguno**: `redistribuible` es false | Intake §13; `05` §5 |

**Decisión: el inventario se emite en la unidad desplegable que embebe este ensamblado**, no acá. Lo que este proyecto de código aporta a ese inventario es **una fila con un solo hijo, que a su vez no tiene ninguno**, y un gate que lo sostiene: `QG-05`, con umbral **1 y 0**.

**La ausencia de marco web merece un párrafo, porque es la que más se rompe sola.** La tentación característica de una capa de casos de uso es tomar prestado un tipo del marco web —un resultado, un tipo de acción, una excepción de protocolo— para no escribir el propio. `QG-05` lo prohíbe con umbral **0**, y el efecto sobre la cadena de suministro es que **este ensamblado no arrastra ninguna dependencia transitiva al proceso que lo carga**: todo lo que entra a la imagen del backend por esta vía es código del propio producto.

### 2.4 `GeometriaFactory-Infrastructure`

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias core externas | **Tres**, nombradas por su función: el proveedor de acceso a datos con su motor embebido, la biblioteca de **derivación de clave** y la de **emisión de acceso firmado** | Intake §17.1.P.1 · GeometriaFactory-Infrastructure |
| Herramienta de transformaciones | Instalada como **herramienta local del repositorio**, para que su versión quede versionada junto al código | Intake §17.1.P.1 · GeometriaFactory-Infrastructure |
| Dependencias del producto | `GeometriaFactory-Application` y `GeometriaFactory-Domain` | Intake §13 |
| Artefacto publicado | **Ninguno**: `redistribuible` es false | Intake §13; `05` §5 |

**Decisión: el inventario se emite en la unidad desplegable que embebe este ensamblado**, no acá; pero **este proyecto de código aporta la mayor parte de las dependencias externas de esa unidad**, y por eso su anclaje es una decisión de cadena de suministro y no de conveniencia.

**Las versiones exactas no figuran en este documento y no es una omisión.** El intake §17.1.P.1 · GeometriaFactory-Infrastructure declara que se anclan en la etapa `a` y se registran en ese momento, y la regla de anclaje del encabezado de la Parte C del intake prohíbe que una versión cambie **como efecto colateral de una actualización**. Escribir un número acá lo congelaría antes de que se decida, que es el defecto que este corpus viene corrigiendo en otras tablas.

## 3. Firma del artefacto

### 3.1 `GeometriaFactory-Api`

**No se firma, y la brecha se declara en lugar de darse por cubierta.**

| Requisito | Estado | Motivo |
| --- | --- | --- |
| Firma de la imagen | **No cumplido, y además no tendría objeto en este canal.** El intake §17.1.P.7 · GeometriaFactory-Api declara que **la imagen no se publica en ningún registro**: se construye en destino. **No hay artefacto en tránsito que firmar**, porque lo que viaja es el código fuente desde el repositorio | Intake §17.1.P.7 · GeometriaFactory-Api |
| Registro público de transparencia | **No cumplido** | Lo mismo, y además exigiría infraestructura que el intake §10 no financia |
| Integridad de lo que sí viaja | **Parcialmente cumplido, y es lo que corresponde mirar acá.** Lo que llega al destino es **una etiqueta del repositorio**, y su integridad es la del propio repositorio | `05` §5; [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 |
| Integridad del origen | **Cumplido**: etiqueta por etapa cerrada, y reversión apoyada en ella | Intake §17.1.P.7 · GeometriaFactory-Api |

**El desplazamiento que este canal produce, dicho sin suavizar.** En un modelo con registro, la firma protegería la imagen entre quien la construye y quien la corre. Acá **quien la construye es quien la corre**, de modo que la pregunta de confianza se desplaza al eslabón anterior: **que lo que el destino trae del repositorio sea lo que la etapa cerró**. Lo que hoy sostiene eso es la etiqueta y el control de acceso del propio repositorio, y **no hay una comprobación criptográfica declarada**. Es la brecha, y queda escrita.

### 3.2 `GeometriaFactory-Domain`

**No se firma, y no es una omisión.** Se firma para que un integrador pueda verificar autoría e integridad de algo que recibió por un canal. Acá no hay canal ni integrador: el artefacto no sale del repositorio, y sus **dos** consumidores lo obtienen por referencia de proyecto dentro de la misma construcción (intake §13, columna de dependencias).

**Dónde sí corresponde firmar, y por qué no se decide acá:** la imagen del backend y la publicación del front son lo que cruza hacia un destino. La política de firma de esas dos pertenece a la categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`, y este documento **no la escribe por ellas**.

**Lo que sí rige acá es la integridad del origen**: toda etapa cerrada lleva etiqueta ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §6, objetivo **100 %**), y la reversión se apoya en esa etiqueta ([`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7). Es lo que permite reconstruir exactamente el estado de cualquier demostración ya aprobada.

### 3.3 `GeometriaFactory-Application`

**No se firma acá.** No hay canal por el que un integrador reciba este ensamblado: sus consumidores lo obtienen por referencia de proyecto y lo embeben en su propio artefacto. La firma tiene sujeto en **lo que sale del repositorio** —la imagen del backend y la publicación del front— y esa decisión pertenece a las categorías 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

**Lo que sí rige acá es la integridad del origen**: etiqueta por etapa cerrada y reversión apoyada en ella ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 y §6, [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7).

### 3.4 `GeometriaFactory-Infrastructure`

**No se firma acá.** No hay canal por el que un integrador reciba este ensamblado: su único consumidor es la composición de raíz de `GeometriaFactory-Api`, y lo embebe en su propio artefacto. La firma tiene sujeto en **lo que sale del repositorio** —la imagen del backend y la publicación del front—, y esa decisión pertenece a las categorías 09 de esas dos unidades.

**Y una distinción que en este proyecto de código hay que hacer explícita, porque las dos cosas se llaman igual.** Acá vive la **emisión de accesos firmados**, que es una **capacidad del producto** —firmar un acceso con una clave simétrica provista desde afuera— y **no es la firma de un artefacto de la cadena de suministro**. Son dos preocupaciones distintas:

| Preocupación | Qué firma | Quién verifica | Dónde vive en este corpus |
| --- | --- | --- | --- |
| Firma **de artefacto** | Un artefacto publicado, para que un integrador compruebe autoría e integridad | Un integrador externo, que acá **no existe** | No aplica en este proyecto de código |
| Firma **de acceso** | El acceso que el producto emite a una persona ya autenticada | El propio servicio, al recibirlo | Intake §17.1.P.5 · GeometriaFactory-Infrastructure; gate `QG-12` |

**Lo que sí rige acá como integridad del origen**: etiqueta por etapa cerrada, reversión apoyada en ella, y **linaje de transformaciones inmutable** ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4).

## 4. Nivel de integridad de la construcción

### 4.1 `GeometriaFactory-Api`

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido en la canalización**: `scripts/build.sh`, `scripts/test.sh` y el archivo de construcción multietapa son los mismos en la máquina de quien construye y en el pipeline | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

**Y una brecha propia de este canal, que ninguna otra unidad del producto tiene.** La imagen que la canalización verifica **no es la imagen que corre**: la del servidor propio se construye ahí, en otro momento y sobre otra máquina. Dos consecuencias que se declaran en lugar de disimularse:

| Consecuencia | Qué implica |
| --- | --- |
| **El inventario del §1 describe la imagen verificada, no exactamente la desplegada** | Si entre una y otra cambió algo que la construcción resuelve —una versión no anclada, un repositorio de paquetes que devuelve otra cosa—, las dos imágenes pueden diferir. **La regla de anclaje de versiones del intake es lo único que hoy lo acota**, y por eso acá no es una preferencia de estilo sino el mecanismo principal |
| **La reproducibilidad no está verificada entre las dos máquinas** | Ninguna fuente exige compararlas y esta categoría **no declara que sean idénticas**. Lo que declara es que la única garantía disponible es el anclaje explícito de toda versión |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**. **La elevación es de nivel producto.**

### 4.2 `GeometriaFactory-Domain`

**Nivel objetivo: el primero del marco de niveles de integridad de la construcción, y se declara con su brecha abierta en lugar de darlo por alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| La construcción es **automatizada y reproducible por guion**, no artesanal | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, y todo corre dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| Se emite **procedencia** del artefacto: qué se construyó, desde qué estado del repositorio y con qué entradas | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha, no se declara el nivel alcanzado |

**Por qué no se fija un nivel más alto.** Los niveles superiores exigen infraestructura de construcción con garantías propias —aislamiento del ejecutor, procedencia inalterable— que no tiene sujeto en un producto que el intake §10 declara **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero. Declarar un nivel que nadie va a poder acreditar sería peor que declarar el que se puede sostener con su brecha a la vista.

**La elevación queda como punto abierto** y es de nivel producto: sólo tiene sentido resolverla junto con la procedencia de la imagen del backend, que es el artefacto que efectivamente se despliega.

### 4.3 `GeometriaFactory-Application`

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero. **La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia de los dos artefactos que se despliegan.

### 4.4 `GeometriaFactory-Infrastructure`

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** `scripts/build.sh` y `scripts/test.sh` son los mismos guiones en la máquina de quien construye y en el pipeline, dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**. **La elevación es de nivel producto.**

**Una precisión propia.** Si alguna vez se emitiera procedencia del artefacto del servidor propio, **la parte que más valor tendría es la de este proyecto de código**: es el que introduce las dependencias externas, y una procedencia sin ellas describiría lo que menos riesgo tiene.

## 5. Análisis de dependencias

### 5.1 `GeometriaFactory-Api`

| Comprobación | Umbral o criterio | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Anclaje explícito de **toda** versión que entra a la imagen | Toda versión **fijada explícitamente**; un cambio mayor **se documenta, nunca es efecto colateral** | Revisión de los archivos de proyecto y del de construcción, en la etapa `a` y en cada cambio. Acá es **el mecanismo principal**, por §3 | Bloqueante como regla del intake, encabezado de la Parte C |
| Contenido de la imagen final | **Sólo el entorno de ejecución**, sin kit de desarrollo ni depurador, y **sin linaje con la imagen del contenedor de desarrollo** | Inspección del archivo de construcción | **Bloqueante**: Definition of Done §1.4 |
| Puertos publicados hacia el enrutador | **Uno**, y es el único punto de entrada al servidor propio | Inspección del archivo de composición | `05` §5 |
| Configuración de intercambio declarada en el producto | **1** sola | `QG-10`, con `TC-00029`, en el stage `build` | **Bloqueante** |
| Actualización automática de dependencias | **No se declara ninguna.** Contradiría la regla de anclaje, y acá además haría divergir la imagen verificada de la desplegada | — | — |

**La segunda fila es un control de superficie de ataque escrito como control de empaquetado.** Una imagen que llevara el kit de desarrollo al servidor domiciliario multiplicaría lo que un acceso indebido puede hacer ahí, y el intake §17.1.P.9 · GeometriaFactory-Api lo prohíbe con esas palabras. La Definition of Done §1.4 lo verifica **por inspección del archivo de construcción**, que es donde se ve el linaje.

**La quinta fila tiene acá un motivo extra respecto del resto del producto.** En las bibliotecas, una actualización automática rompería la regla de anclaje; acá, además, **haría que la imagen desplegada dejara de corresponder a la verificada**, que es la brecha declarada en §3.

### 5.2 `GeometriaFactory-Domain`

**Sin dependencias no hay análisis de composición que hacer, y lo que reemplaza al análisis es la verificación de que ese cero se sostiene.**

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Referencias salientes del archivo de proyecto | **0** a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | `QG-04`, con `TC-02024` y la revisión del pull request | **Bloqueante** |
| Actualización automática de dependencias | **No aplica**: no hay dependencias que actualizar | — | — |

**El día en que este proyecto de código adquiera una dependencia externa, el gate bloqueante se dispara antes que cualquier análisis de composición.** Es un orden afortunado: la primera pregunta no será «¿esa dependencia tiene vulnerabilidades?» sino «¿por qué el dominio adquirió una dependencia?», que es la que [`../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) obliga a contestar.

**La regla de anclaje de versiones del producto rige igual** aunque hoy no haya nada que anclar: el intake, en el encabezado de su Parte C, declara que toda versión de paquete se fija explícitamente y que un cambio de versión mayor es una decisión que se documenta, **nunca el efecto colateral de una actualización**. Esa regla es la que hace que una actualización automática silenciosa no sea admisible en este producto.

### 5.3 `GeometriaFactory-Application`

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Referencias a otros proyectos de código del producto | Exactamente **1** | `QG-05`, con `TC-04027` y la revisión del pull request | **Bloqueante** |
| Referencias a bibliotecas de persistencia, transporte, serialización o marco web | **0** | El mismo gate | **Bloqueante** |
| Pruebas de esta capa que abren el almacén real | **0** | `QG-04`, con `TC-04026` | **Bloqueante** |
| Actualización automática de dependencias | **No aplica**: no hay dependencias externas que actualizar | — | — |

**Sin dependencias externas, el análisis de composición no tiene sujeto y lo que corresponde verificar es que ese cero se sostenga.** Las tres primeras filas son esa verificación, y las tres ya bloquean desde la Fase E: esta categoría no agrega ninguna comprobación nueva, las ubica en el stage donde corren. `QG-05` corre en `build`, que es el más barato, y es **la propiedad que sostiene a `QG-04`**: sin biblioteca de persistencia declarada, una prueba de esta capa no tiene con qué abrir un almacén.

**La regla de anclaje de versiones del producto rige igual**: el intake, en el encabezado de su Parte C, declara que toda versión de paquete se fija explícitamente y que un cambio de versión mayor se documenta, **nunca como efecto colateral de una actualización**. Acá alcanza al ejecutor de pruebas y al recolector de cobertura, que son herramientas del proyecto de pruebas y no dependencias del ensamblado.

### 5.4 `GeometriaFactory-Infrastructure`

**Acá el análisis de composición tiene sujeto real**, a diferencia de las otras cuatro bibliotecas del producto.

| Comprobación | Umbral o criterio | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Peticiones de red originadas por los **dos motores** | Exactamente **0** | `QG-08`, con `TC-06014`, inspección de dependencias de los dos motores | **Bloqueante** |
| Anclaje explícito de las **tres** dependencias core y de la herramienta de transformaciones | Toda versión **fijada explícitamente**, nunca cambiada como efecto colateral | Revisión del archivo de proyecto y del archivo de herramientas, en la etapa `a` y en cada cambio | Bloqueante como regla del intake, encabezado de la Parte C |
| Elección y anclaje de la **función de derivación de clave** | El intake declara dos opciones y **no elige**. La forma y el criterio los fija `ADR-06004`; la elección concreta es de la etapa `a` | Punto de control de la etapa `a`. Registrado como `PD-03` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 | Bloqueante como tarea de la etapa `a` |
| Actualización automática de dependencias | **No se declara ninguna.** Contradiría la regla de anclaje | — | — |

**La primera fila es un gate de composición escrito como recuento, y conviene ver por qué está donde está.** El intake §17.1.P.3 · GeometriaFactory-Infrastructure declara que **el validador de figuras no hace red**: recibe texto y devuelve observaciones. `QG-08` no verifica esa intención en el código propio, sino **en las dependencias de los dos motores**: una biblioteca que hiciera una petición por dentro rompería la propiedad sin que ninguna línea del proyecto de código la mencione. Es exactamente el modo de falla que un análisis de composición existe para encontrar, y acá está escrito como **0**.

**La regla de anclaje de versiones no es una preferencia de esta categoría**: el intake, en el encabezado de su Parte C, la declara para los seis proyectos de código de la plataforma, y agrega que **un cambio de versión mayor es una decisión que se documenta**. En este proyecto de código alcanza a tres dependencias externas, a la herramienta de transformaciones y al motor de almacenamiento embebido (intake §17.1.P.9 · GeometriaFactory-Infrastructure).

## 6. Análisis estático y dinámico

### 6.1 `GeometriaFactory-Api`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**» | Intake §17.1.P.8 · GeometriaFactory-Api; `QG-01` |
| Estático de superficie | **Existe, bloquea y es la verificación característica de este proyecto de código**: `QG-05` sobre los **quince** puntos en las dos direcciones, `QG-06` sobre los **diecisiete** códigos del contrato, `QG-08` sobre las respuestas y el registro del servidor, y `QG-10` sobre la composición de raíz | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| **Dinámico** | **Existe, y es el más completo del producto**: la batería de integración **golpea la superficie real por su protocolo contra el almacén real**, y `QG-12` exige verificar **forzando la petición** y no por la interfaz | Intake §17.1.P.6 · GeometriaFactory-Api; `Estrategia-Calidad.md` §1 |
| Dinámico sobre el artefacto empaquetado | **Existe**: el stage `imagen` arranca la imagen, aplica las transformaciones sobre un almacén vacío y comprueba salud | `PT-04`; `QG-13` |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con el sujeto más sensible**: el intake §17.1.P.5 · GeometriaFactory-Api declara que la clave de firma va **como secreto del repositorio, nunca en el archivo del flujo de trabajo** | [`Entornos-Deploy.md`](Entornos-Deploy.md) §6 |

**La tercera fila es la que hace de este proyecto de código el que más superficie verifica del producto, y `QG-12` es su caso extremo.** `Estrategia-Calidad.md` §3 lo declara: es **el único criterio de verificación del producto que la fuente exige ejercer forzando la petición**, y no mirando una pantalla. Desde la cadena de suministro, la lectura es que **la comprobación de que un control existe no puede hacerse sobre el cliente que respeta el control**.

### 6.2 `GeometriaFactory-Domain`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**, integrado en el stage `build`: `CV-20` declara **0** advertencias nuevas del análisis estático, bloqueante por `CV-13`, que es el gate de construcción sin advertencias | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §5 y §3 |
| Estático de superficie | **Existe y bloquea**: las pruebas de inspección `TC-02023`, `TC-02024`, `TC-02026` y `TC-02027` revisan el proyecto de código sobre sí mismo —catálogo de condiciones, dependencias salientes, invariantes ejercidos y condiciones que no viajan como excepción— | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §1, «prueba de inspección» |
| Dinámico | **No aplica, y se declara en lugar de omitirse** | Un análisis dinámico ejercita una aplicación en ejecución. Este proyecto de código **no atiende peticiones ni abre conexiones** (`05` §8, cierre), de modo que no hay superficie que ejercitar. El análisis dinámico del producto tiene sujeto en `GeometriaFactory-Api`, que es quien expone la superficie HTTP |
| Detección de secretos en las confirmaciones | **Recomendada a nivel producto y no propia**: este proyecto de código no maneja secretos (intake §17.1.P.5 · GeometriaFactory-Domain), pero comparte repositorio con los que sí | Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**No se agrega ninguna herramienta nueva al pipeline.** Todo lo que esta sección declara ya está ejecutándose como gate de la categoría 08; lo que hace este documento es nombrarlo desde la perspectiva de la seguridad de la construcción, que es la que faltaba.

### 6.3 `GeometriaFactory-Application`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**», y no «sin errores» | Intake §17.1.P.8 · GeometriaFactory-Application, por remisión a §17.1.P.8 · GeometriaFactory-Domain; `QG-01` |
| Estático de estructura | **Existe, bloquea, y es la verificación característica de este proyecto de código**: `QG-05` sobre el archivo de proyecto, `QG-06` sobre el catálogo de las **36** condiciones en las dos direcciones, `QG-08` sobre los **once** orquestadores y `QG-09` sobre la proyección de listado | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Dinámico | **No aplica acá, y tiene sujeto en otro proyecto de código**: este ensamblado no expone ninguna superficie de red. La que un análisis dinámico ejercitaría es la HTTP, que expone `GeometriaFactory-Api` | Intake §17.1.P.3 · GeometriaFactory-Application: «no aplica hacia afuera del proceso» |
| Detección de secretos en las confirmaciones | **Recomendada a nivel producto**: este proyecto de código no maneja secretos, pero comparte repositorio con los que sí | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

### 6.4 `GeometriaFactory-Infrastructure`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**», que es la formulación de `QG-01`. El intake §17.1.P.8 · GeometriaFactory-Infrastructure la declara como «build en 0 sin advertencias» | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3, `QG-01`; intake §17.1.P.8 · GeometriaFactory-Infrastructure |
| Estático de estructura | **Existe y bloquea**: `QG-10` sobre las proyecciones de listado, `QG-11` sobre el texto original conservado, `QG-12` sobre la emisión de accesos y `QG-13` sobre el catálogo de las **17** condiciones en las dos direcciones | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| Dinámico sobre superficie de red | **No aplica acá**: este ensamblado **no expone endpoints** y sus dos motores no hacen red. La superficie que un análisis dinámico ejercitaría es la HTTP, que expone `GeometriaFactory-Api` | Intake §17.1.P.3 · GeometriaFactory-Infrastructure |
| **Dinámico sobre almacenamiento** | **Existe, y es propio de este proyecto de código**: el stage `verificar-transformaciones` ejercita el arranque **sobre un almacén inexistente** y comprueba que el esquema queda completo sin paso manual | `QG-04`; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con el sujeto más sensible del producto**: este proyecto de código es el que trabaja con la clave de firma, aunque no la custodie | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

## 7. Política ante vulnerabilidades publicadas

### 7.1 `GeometriaFactory-Api`

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre una de las **dos bibliotecas sensibles** que embebe —derivación de clave o emisión de acceso firmado— | Se ancla la versión corregida en `GeometriaFactory-Infrastructure` y **se despliega esta unidad**: es la única forma de que la corrección llegue al servidor. Los accesos vigentes caducan solos, porque la vigencia es **corta** y **no hay acceso de refresco** | El equipo ancla; el Product Owner despliega |
| Vulnerabilidad sobre la **imagen base de ejecución** | Se ancla la versión corregida en el archivo de construcción y se vuelve a desplegar. **La reconstrucción en destino la trae**, y ése es el único caso donde ese canal juega a favor | El equipo, con constancia |
| Vulnerabilidad sobre el **entorno del servidor propio**, fuera de la imagen | **Fuera del alcance de esta cadena.** Es la máquina del Product Owner | El Product Owner |
| Exposición de la **clave de firma** | Se rota el valor en el ambiente y se reinicia el servicio. **El valor no está en el repositorio ni en la imagen**, de modo que la rotación no exige reconstruir | Intake §17.1.P.5 · GeometriaFactory-Api |
| Exposición de la **dirección del servidor propio** | Se revisa por dónde se filtró: `QG-08` mide **0** respuestas que la expongan, sobre los **quince** puntos **y** sobre el registro del servidor. Es `RA-03`, y `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 lo ubica **en el último tramo antes de salir del servidor propio** | El equipo, con constancia |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

**Y dos riesgos aceptados por escrito que esta categoría transcribe y no reabre.** El intake §17.1.P.5 · GeometriaFactory-Api declara que **el tramo entre el front y este servicio viaja en claro si ese salto es HTTP plano**, con el túnel saliente como salida **documentada y no adoptada**; y registra la **nota de seguridad sobre el flujo de credenciales**, aceptado porque el intermediario es el propio front del mismo sistema, el tramo hacia el navegador es seguro y el alcance es un laboratorio de aula. Las dos son decisiones del Product Owner registradas aguas arriba.

### 7.2 `GeometriaFactory-Domain`

**Sin dependencias externas, la superficie de vulnerabilidad propia de este proyecto de código es la de su plataforma de ejecución.** Esa plataforma la comparten los seis proyectos de código del producto que no son el visor, y su versión objetivo la fija el intake para todo el producto.

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad publicada sobre la plataforma de ejecución | Se trata como **decisión de plataforma del producto**, no como parche de este proyecto de código: la corrección es una actualización de la versión objetivo, que por la regla de anclaje del intake **se documenta y no se aplica como efecto colateral** | El Product Owner, con la constancia en el punto de control de la etapa en curso |
| Vulnerabilidad publicada sobre una dependencia de este proyecto de código | **No tiene sujeto hoy**: no hay dependencias. Si alguna vez la hay, la vulnerabilidad es el segundo problema; el primero es `QG-04` | — |
| Vulnerabilidad que afecta a la unidad desplegable que embebe esta biblioteca | Es de la categoría 09 de esa unidad. Este proyecto de código sólo tiene que poder **reconstruirse desde su etiqueta**, y puede | Categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días, y es deliberado.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas», y un plazo de remediación en horas sería exactamente el tipo de compromiso calendario que ninguna fuente da. Lo que sí rige es el mecanismo: **el punto de control de la etapa es bloqueante**, de modo que una vulnerabilidad conocida y no tratada llega a la mesa del Product Owner en el cierre de la etapa en curso y no puede quedar en silencio.

**Comunicación a integradores: no aplica.** No hay integradores externos —`redistribuible` es false— y el intake §10 declara que **ninguna normativa de compliance aplica**: es un laboratorio de aula con cuentas creadas para la materia.

### 7.3 `GeometriaFactory-Application`

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la plataforma de ejecución | Decisión de plataforma del producto, no parche de este proyecto de código. A diferencia de `GeometriaFactory-Contracts`, **este ensamblado se carga en un solo proceso**, de modo que una bajada de versión del front no lo alcanza | El Product Owner, con constancia en el punto de control |
| Vulnerabilidad sobre una dependencia de este proyecto de código | **No tiene sujeto**: no hay dependencias externas. Si alguna vez la hubiera, el primer problema es `QG-05` | — |
| Vulnerabilidad sobre la herramienta de pruebas o de cobertura | Se ancla la versión corregida por la regla de anclaje del intake, y se registra en el punto de control de la etapa. **No alcanza al ensamblado que se despliega**: son herramientas del proyecto de pruebas | El equipo, con constancia |
| Vulnerabilidad sobre la unidad desplegable que lo embebe | Es de la categoría 09 de `GeometriaFactory-Api`. Este ensamblado sólo tiene que poder reconstruirse desde su etiqueta, y puede | Categoría 09 de `GeometriaFactory-Api` |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso, que impide que una vulnerabilidad conocida quede sin tratar en silencio.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

### 7.4 `GeometriaFactory-Infrastructure`

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la **biblioteca de derivación de clave** | Se ancla la versión corregida. **Si la corrección cambia los parámetros de derivación**, las contraseñas ya guardadas siguen verificándose porque `ADR-06004` exige que los parámetros viajen **junto al valor derivado, sin valor por defecto silencioso** | El equipo, con constancia en el punto de control |
| Vulnerabilidad sobre la **biblioteca de emisión de acceso firmado** | Se ancla la versión corregida y **se despliega la unidad del servidor propio**. Los accesos vigentes caducan solos: el intake §17.1.P.5 · GeometriaFactory-Api declara vigencia **corta** y **sin acceso de refresco** | El mismo, y el Product Owner que ejecuta el despliegue |
| Vulnerabilidad sobre el **proveedor de acceso a datos o su motor embebido** | Se ancla la versión corregida y se ejercita el stage `verificar-transformaciones` **antes** de construir la imagen: un cambio de motor puede alterar cómo se aplica el linaje | El equipo |
| Vulnerabilidad sobre la plataforma de ejecución | Decisión de plataforma del producto. **Este ensamblado no llega al front**, de modo que una bajada de versión del front no lo alcanza | El Product Owner |
| Vulnerabilidad sobre la unidad desplegable que lo embebe | Es de la categoría 09 de `GeometriaFactory-Api` | Esa categoría |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

**Y un riesgo aceptado por escrito que esta categoría no reabre**: el intake declara que **las credenciales viajan en claro en el tramo entre el front y el servicio de datos** si ese salto es HTTP plano, con el túnel saliente como salida **documentada y no adoptada**. Alcanza a lo que este proyecto de código recibe, pero la decisión es del Product Owner y está registrada aguas arriba.

## 8. La superficie expuesta como preocupación de cadena de suministro

### 8.1 `GeometriaFactory-Api`

Esta sección existe porque acá, además de dependencias, **hay algo que ninguna otra unidad del producto tiene: un puerto abierto hacia afuera en la máquina donde vive el dato**.

| Propiedad | Por qué es de cadena de suministro y no sólo de diseño |
| --- | --- |
| **Un solo punto de entrada al servidor propio** | `05` §5 lo declara: todo lo que este proyecto de código no exponga **no existe para nadie de afuera**. La superficie de ataque de la máquina **es exactamente la lista de quince puntos**, y por eso un punto nuevo es una decisión de exposición y no una funcionalidad |
| **Exactamente 4 puntos fuera de la guardia, ni uno más** | `QG-05`, medido **en las dos direcciones**. `05` §9 declara el riesgo: un punto nuevo fuera de la guardia hace que una regla del producto deje de valer **y nada falla** |
| **El punto de salud no exige acceso y no diagnostica** | [`ADR-00007`](../05-Arquitectura-Tecnica/Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2, regla 4: responde por el estado del servicio y **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró**. Es el punto que **cualquiera puede llamar**, y por eso es el que menos puede contar |
| **Ningún mensaje expone dirección, ruta, secreto ni traza** | `QG-08`, con umbral **0**, sobre los quince puntos **y sobre el registro del servidor** |
| **Tres familias empobrecidas indistinguibles** | `QG-07`, **3 de 3** en cuerpo y en código. Es lo que impide que la superficie revele la existencia de un recurso ajeno, y `Estrategia-Calidad.md` §3 declara que **ninguna capa de adentro puede repararlo** |

**Las cinco comparten la propiedad que las hace un problema de esta categoría y no sólo de la 05**: **su incumplimiento no produce ningún fallo**. Un punto agregado fuera de la guardia responde bien; una respuesta más informativa se ve mejor; un mensaje con la dirección adentro ayuda a diagnosticar. **Las cinco se miden con recuentos y ninguna con un juicio**, y por eso corren en cada pull request que toca la superficie, que es la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 materializa como trigger propio.

**Y la advertencia que cierra el documento**: esas cinco reglas protegen **la única máquina del producto donde vive el trabajo de la comisión**. El intake §11 registra desde el negocio que su caída es un riesgo aceptado con estado degradado; **su exposición indebida no está aceptada por nadie**, y no tiene ninguna mitigación posterior.

## 9. Qué de esta política es propio y qué es del producto

### 9.1 `GeometriaFactory-Domain`

La tabla existe para que la próxima categoría 09 —la de un proyecto de código que sí se despliega— sepa qué le queda por decidir y no lo dé por escrito acá:

| Preocupación | Dónde se decide |
| --- | --- |
| Cero dependencias salientes y su verificación | **Acá**, y ya bloquea: `QG-04` |
| Cero advertencias de construcción y análisis estático | **Acá**, y ya bloquea: `QG-01` con `CV-20` |
| Inventario de componentes de lo que se despliega | Categoría 09 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` |
| Firma del artefacto desplegado | Las mismas dos |
| Procedencia de la construcción y elevación del nivel de integridad | Nivel producto, junto con la imagen del backend |
| Análisis dinámico de la superficie HTTP | Categoría 09 de `GeometriaFactory-Api` |
| Rotación de secretos del despliegue | Categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api` |

## 10. La autorización como preocupación de cadena de suministro

### 10.1 `GeometriaFactory-Application`

Esta sección existe porque en este proyecto de código la cadena de suministro clásica —dependencias, inventario, firma— **no es donde está el riesgo**, y decirlo sin ofrecer dónde sí está dejaría el documento vacío.

El riesgo real de esta capa es **que una comprobación de autorización deje de ejercerse en un camino nuevo**, y no llega por una dependencia: llega por un caso de uso que alguien agrega. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §2 lo pone como eje de la prioridad del proyecto de código citando su caso más agudo: `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad **sin resolver antes la marca de cambio de contraseña pendiente**. Sus tres propiedades, desde el punto de vista de la seguridad de la construcción:

| Propiedad | Por qué importa acá |
| --- | --- |
| **Entra de a un caso de uso por vez, y compila** | Ninguna herramienta de análisis de composición lo detectaría: no es una dependencia, es un orquestador nuevo que no llamó a una comprobación |
| **Se verifica con un recuento, no con un juicio** | `QG-07` mide **4 de 4** comprobaciones con prueba de su negativa **sin base de datos**, y **1** sola prueba de que la cuarta corta antes que las otras tres |
| **Su verificación no necesita ambiente** | Es la consecuencia útil de que la capa se pruebe entera con dobles: la comprobación más sensible del proyecto de código se puede ejercer en el stage `test`, sin levantar nada |

**La conclusión operativa para el pipeline** es que la comprobación de seguridad más valiosa de este proyecto de código corre **en cada pull request que agrega o cambia un caso de uso**, y se cierra **al cerrar la etapa** con `QG-07` sobre la matriz entera. Es la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 materializa como trigger propio.

## 11. Las dos bibliotecas sensibles, y qué las hace distintas del resto

### 11.1 `GeometriaFactory-Infrastructure`

Esta sección existe porque en este proyecto de código **la cadena de suministro sí es donde está buena parte del riesgo**, y conviene separar qué protege cada mecanismo.

| Dependencia, por su función | Qué pasaría si estuviera comprometida | Qué la protege hoy |
| --- | --- | --- |
| **Derivación de clave** | Las contraseñas guardadas dejarían de estar protegidas, **sin ninguna señal visible**: el producto seguiría funcionando igual | El anclaje explícito de versión, los parámetros versionados junto al valor derivado ([`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)) y la revisión de todo cambio de versión mayor como decisión documentada |
| **Emisión de acceso firmado** | Se podrían emitir accesos válidos sin la clave, o filtrarse la clave. **Es la capacidad más sensible del producto** | El mismo anclaje, más `QG-12` —**0** emisiones sin clave de firma y **0** claves generadas al vuelo— y la clave viviendo **fuera del repositorio y fuera de la imagen** |

**Las dos comparten una propiedad que las distingue de cualquier otra dependencia del producto**: su compromiso **no produce ningún síntoma**. Un motor de dibujo comprometido se nota; una derivación de clave debilitada no. De ahí que el único mecanismo disponible sea **saber exactamente qué versión se está usando**, que es lo que la regla de anclaje del intake compra, y **no dejar que cambie sola**.

**La contribución de este proyecto de código a la seguridad del producto tiene además una parte que no es una dependencia**, y es la contraseña provisoria: `QG-09` mide **0** provisorias repetidas y **0** derivables del nombre, del correo ni de la fecha, y [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) fija la longitud y el alfabeto que lo sostienen. **No se verifica contra un registro de provisorias anteriores**, porque conservarlas exigiría guardar contraseñas en claro; la sostiene la impredecibilidad, y así lo declara `PA-06` de `05` §11, que esta categoría **hereda y no reabre**.

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
