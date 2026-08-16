# Especificación funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Especificacion-Funcional.md
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

**Ocho de las dieciséis secciones son comunes a las cuatro capas. Las otras ocho existen en una sola,
y juntas completan lo que ninguna declaraba entera:**

| Sección | Sólo en |
| --- | --- |
| Las cinco responsabilidades de esta capa · Lo que decide y lo que sólo transporta | `GeometriaFactory-Api` |
| Catálogo de reglas de negocio | `GeometriaFactory-Domain` |
| Los puertos que esta capa declara · Autorización por pertenencia y verificación de facultad | `GeometriaFactory-Application` |
| Los cuatro puertos que implementa y los dos mecanismos que provee · Lo que esta capa hace y lo que no decide | `GeometriaFactory-Infrastructure` |

**Los casos de uso ya no están acá**: se consolidaron antes, de 63 a 9, y viven en `Casos-De-Uso/`.
Lo que este documento consolida es **el índice y el marco**, que es lo que la consolidación de casos
de uso había dejado a medias.

---

## 1. Alcance funcional de este proyecto de código

### 1.1 `GeometriaFactory-Api`

`GeometriaFactory-Api` es el **proyecto de código principal** del producto —así lo declaran el intake §13 y el `PRODUCT-MANIFEST` §1— y es donde el producto **se vuelve alcanzable**. Es el único de los siete que ensambla a los demás: depende de `GeometriaFactory-Application`, de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Contracts`, y es el **nivel 3**, el último, del orden topológico. Nadie depende de él por compilación; lo alcanza `GeometriaFactory-Web` por HTTP, en tiempo de ejecución.

Su forma es la del **host delgado** que el intake §17.1.P.2 · GeometriaFactory-Api declara: puntos de acceso que traducen petición a caso de uso y resultado a tipo de transferencia, más la composición de raíz que conecta los puertos con sus adaptadores. El actor primario de los **doce** casos de uso es el código de `GeometriaFactory-Web`, servidor a servidor; el alumno y el administrador aparecen como sujetos de las reglas y nunca como actores, porque **el navegador nunca alcanza esta superficie** (RA-01).

Cuatro rasgos distinguen a esta capa de las tres que ensambla, y los cuatro recorren sus casos de uso:

1. **Acá está la frontera del proceso.** Todo lo que las tres capas de adentro resuelven con referencias de proyecto de código —tipos, motivos, excepciones— acá tiene que convertirse en algo que viaje por un protocolo y sobreviva a un salto de red. Es el único lugar del backend donde un dato puede alterarse por codificación, por serialización o por un intermediario.
2. **Acá se traduce, y traducir es decidir.** Un motivo de la capa de aplicación no es un código de respuesta; un código del contrato no es un número de protocolo. Las dos traducciones son de esta capa y **ninguna otra las puede reparar**: si acá se elige mal, la regla se rompe hacia afuera sin que ninguna capa de adentro se entere. El caso más caro está en RN-00003, y §6 lo declara.
3. **Acá vive la única puerta.** El intake §17.1.P.9 · GeometriaFactory-Api declara que un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio. Todo lo que este proyecto de código no exponga, no existe para nadie de afuera.
4. **Acá se aplica RA-03 en el único lugar donde se puede violar hacia afuera.** Las capas de adentro producen códigos y motivos; ninguno de ellos llega solo a una persona. Lo que llega es lo que esta capa emite, y por eso **ningún mensaje de esta superficie incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma**.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización por pertenencia y la verificación de facultad, en `GeometriaFactory-Application`; la interpretación del texto, el guardado, la derivación de credenciales y la emisión del acceso firmado, en `GeometriaFactory-Infrastructure`; los tipos que cruzan la frontera, en `GeometriaFactory-Contracts`; las páginas, el dibujo y todo lo que una persona ve, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

### 1.2 `GeometriaFactory-Domain`

`GeometriaFactory-Domain` contiene las entidades e invariantes del dominio y es el centro de la regla de dependencias: no depende de nada y lo consumen `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure` por referencia de proyecto de código (PRODUCT-INTAKE §13 y §17.1.P.1 · GeometriaFactory-Domain).

Por eso esta especificación tiene una forma particular y deliberada, que es la de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los trece casos de uso es el código que consume la biblioteca. El alumno y el administrador aparecen como **sujetos de las reglas** que el dominio hace cumplir, nunca como actores.

Lo que no está acá, y dónde está: la interpretación del texto del alumno, el cálculo de los valores derivados, la persistencia, las consultas y los listados, la verificación de la unicidad del correo sobre el conjunto de alumnos, la derivación de la contraseña y la emisión del acceso pertenecen a `GeometriaFactory-Application` y a `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, a `GeometriaFactory-Contracts`; el dibujo, a `GeometriaFactory-Visor`. La tabla completa de fronteras está en [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) §7.

### 1.3 `GeometriaFactory-Application`

`GeometriaFactory-Application` contiene los **casos de uso** del producto y los **puertos** que la infraestructura implementa. Depende únicamente de `GeometriaFactory-Domain` y de nada más; lo consumen `GeometriaFactory-Api`, por sus casos de uso, y `GeometriaFactory-Infrastructure`, por sus puertos. Es el nivel 1 del orden topológico del producto.

Esta especificación tiene la forma de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los **once** casos de uso es el código que consume la biblioteca; el alumno y el administrador aparecen como sujetos de las reglas, nunca como actores.

Dos rasgos distinguen a esta capa de la de dominio, y los dos recorren todos sus casos de uso:

1. **La dependencia se invierte.** Esta capa declara qué necesita —guardar y recuperar, interpretar el texto del alumno, saber qué hora es— y otra capa lo provee. Es lo que permite ejercer un caso de uso entero con dobles, sin base de datos ni frontera de proceso. Un caso de uso de esta categoría que mencionara el motor de persistencia, el mecanismo de acceso o el protocolo de transporte estaría mal ubicado.
2. **Acá se decide quién puede hacer qué.** El dominio declara las condiciones; esta capa las ejerce sobre el pedido concreto, antes de tocar el repositorio. Es autorización, no autenticación: no se comparan contraseñas ni se emiten accesos.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la interpretación efectiva del texto, la derivación de la contraseña, la emisión del acceso y el guardado, en `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, en `GeometriaFactory-Contracts`; las páginas y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

### 1.4 `GeometriaFactory-Infrastructure`

`GeometriaFactory-Infrastructure` es donde el producto **toca el mundo**. Implementa los cuatro puertos que declara `GeometriaFactory-Application`, provee los dos mecanismos de seguridad que las capas de adentro delegaron, y es **el proyecto de código que modela y ejerce la persistencia del producto**: el `PRODUCT-MANIFEST` §5 declara ese flag true acá y también en `GeometriaFactory-Api`, pero aquél **delega en éste** y sólo toma de configuración la ruta del archivo y dispara la aplicación de las transformaciones al arrancar (intake §17.1.P.4 · GeometriaFactory-Api). Depende de `GeometriaFactory-Application` y de `GeometriaFactory-Domain`, y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Es el nivel 2 del orden topológico.

Esta especificación tiene la forma de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los **diez** casos de uso es el código que consume la biblioteca; el alumno y el administrador aparecen como sujetos de las reglas, nunca como actores.

Tres rasgos distinguen a esta capa de las dos de adentro, y los tres recorren sus casos de uso:

1. **Acá vive el mecanismo, no la decisión.** Las capas de adentro declararon qué hace falta —guardar, interpretar, derivar, firmar, saber qué hora es— y acá se dice **con qué**. Un caso de uso de esta categoría que decidiera un estado, una autorización o una transición estaría mal ubicado.
2. **Acá está el riesgo del producto.** El intake declara con probabilidad alta y con impacto alto que **el validador se escribe sin leer el análisis**, porque el texto del alumno no es JSON estrictamente válido. Es el único riesgo de negocio cuya mitigación es una batería de pruebas, y esa batería vive acá. Por eso [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) es el documento de concepto central de este proyecto de código.
3. **Acá están las dos piezas sensibles.** La derivación de credenciales y la emisión del acceso firmado. Y una tercera que el resto de las capas delegó explícitamente: la **producción de la contraseña provisoria** —la de la habilitación y la del reseteo, que son la misma y con un solo mecanismo (RN-06016)—, que `GeometriaFactory-Application` declara como la única de las dieciséis reglas sin tramo en su capa, y que `GeometriaFactory-Contracts` exige por sus propiedades sin declarar mecanismo.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización por pertenencia y la verificación de facultad, en `GeometriaFactory-Application`; los datos que cruzan la frontera del proceso, en `GeometriaFactory-Contracts`; los endpoints, el arranque y la configuración, en `GeometriaFactory-Api`; las páginas y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

## 2. Documentos de esta categoría

### 2.1 `GeometriaFactory-Api`

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) | Documento de concepto central: los **quince** puntos de acceso, los **diez** códigos de respuesta, las dos traducciones y qué está declarado por una fuente y qué es derivación de esta categoría |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Doce casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

### 2.2 `GeometriaFactory-Domain`

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) | Documento de concepto central: las cinco entidades, los nueve invariantes vigentes y las tres máquinas de estado |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Trece casos de uso, uno por archivo |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | Dieciséis reglas de negocio, una por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

### 2.3 `GeometriaFactory-Application`

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Once casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones y su motivo |

### 2.4 `GeometriaFactory-Infrastructure`

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) | Documento de concepto central: las cuatro trampas del formato, las siete garantías, los ocho escenarios y la cobertura de la batería obligatoria |
| [`Modelo-Datos/Modelo-Conceptual.md`](Modelo-Datos/Modelo-Conceptual.md) | Las cinco entidades, sus atributos, las cuatro relaciones y los cuatro conjuntos cerrados |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | Siete reglas conceptuales de modelo, una por archivo |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Diez casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

## 3. Las cinco responsabilidades de esta capa

### 3.1 `GeometriaFactory-Api`

Salen de §17.5 del intake y no se amplían acá. Cada una tiene su caso de uso o su grupo de casos de uso, y ninguna queda sin contrato.

| Responsabilidad | Qué significa | CU |
| --- | --- | --- |
| **Superficie de acceso** | Los puntos de acceso con su verbo y sus códigos de respuesta, sobre los tipos de `GeometriaFactory-Contracts` | CU-00001, CU-00003, CU-00004, CU-00005, CU-00006, CU-00007, CU-00008 |
| **Admisión de la petición** | Verificar el acceso firmado, exigir el papel que cada punto declara y aplicar la guardia del cambio de contraseña pendiente | CU-00002 |
| **Traducción a protocolo** | Convertir el motivo de la capa de aplicación en código del contrato, y el código del contrato en código de respuesta | CU-00009 |
| **Composición de la aplicación** | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | CU-00010 |
| **Arranque y salud** | Aplicar las transformaciones de esquema al arrancar y responder por el estado del servicio | CU-00011 |

Y una sexta cosa que no es una responsabilidad de tiempo de ejecución y por eso se lista aparte: **la colección de peticiones reproducible** (CU-00012), que el intake §16.1 y §18 declaran como la forma de demostración de este tipo de proyecto de código.

**El alcance de la unidad de trabajo llega decidido**: la capa de aplicación declara un caso de uso, una unidad de trabajo, y esta capa no abre ninguna por su cuenta. Una petición ejerce a lo sumo un caso de uso.

## 4. Lo que esta capa decide y lo que sólo transporta

### 4.1 `GeometriaFactory-Api`

Es la frontera que hay que dejar imposible de confundir, porque **acá es donde una decisión ya tomada puede deshacerse sin que nadie lo note**.

**Enunciado en una línea: esta capa decide cómo se dice, y no decide qué se dice.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Qué punto de acceso existe, con qué verbo y con qué código de respuesta | **Sí** (CU-00001 y CU-00003 a CU-00008) | — |
| Verificar la firma y la expiración del acceso, y exigir el papel del punto | **Sí** (CU-00002). El mecanismo de verificación es de `GeometriaFactory-Infrastructure`; **exigirlo en cada punto es de acá** | — |
| Aplicar la guardia del cambio de contraseña pendiente sobre todo punto salvo uno | **Sí** (CU-00002). La comprobación es de la capa de aplicación; **que ningún punto la saltee es de acá** | — |
| Elegir el código de respuesta de cada motivo, y en particular **no distinguir el recurso ajeno del inexistente** | **Sí** (CU-00009). Es la traducción que RN-00003 exige por escrito | — |
| Conectar cada puerto con su adaptador y tomar de configuración la ubicación del almacén y la clave de firma | **Sí** (CU-00010) | — |
| Aplicar las transformaciones de esquema al arrancar | **Sí** (CU-00011), como **disparo**. La transformación la ejecuta el adaptador | `GeometriaFactory-Infrastructure` |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar la pertenencia de un trabajo o la facultad de administrador **sobre el dato** | **No.** Lo que acá se exige es el papel declarado en el acceso; la comprobación sobre el dato recuperado es de la capa de aplicación, y **el papel no la reemplaza** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Llega en el resultado y se transporta en una respuesta exitosa | `GeometriaFactory-Domain` |
| Interpretar el texto del alumno o verificar sus valores | **No.** El texto viaja como cadena y **no se normaliza en el borde** | `GeometriaFactory-Infrastructure` |
| Declarar qué campos cruzan la frontera | **No.** Los tipos son del ensamblado de contratos, y **esta capa no agrega ni recorta campos** | `GeometriaFactory-Contracts` |
| Presentar el estado degradado a una persona | **No.** Acá termina en un código de respuesta | `GeometriaFactory-Web` |

Seis precisiones que rigen en toda la categoría:

1. **Exigir el papel no es autorizar.** El papel viaja en el acceso firmado y esta capa lo exige por punto; la verificación de pertenencia y la de facultad se hacen sobre el dato recuperado y son de la capa de aplicación. Que un punto exija `Administrador` no exime a la capa de adentro de comprobar, y **duplicar la comprobación acá crearía un segundo lugar donde la regla puede decir otra cosa**.
2. **La guardia del cambio de contraseña pendiente tiene una sola excepción declarada**: el cambio de la propia contraseña. La comprobación es de la capa de aplicación; lo que esta capa aporta es que **ningún punto de acceso quede fuera de ella**, que es la parte que se rompe agregando un punto nuevo y olvidándose.
3. **Ningún mensaje de esta superficie incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma.** Es RA-03, que es regla de nivel producto, y su contracara es que **todo error que se responda queda registrado del lado del servidor**, junto con todo intento de acceso rechazado.
4. **Esta superficie no tiene ningún cliente legítimo que no sea `GeometriaFactory-Web`.** Es RA-01. De ahí salen tres ausencias que no son olvidos y se declaran: **no hay CORS**, **no hay WebSockets** y **no hay ningún punto de acceso pensado para que lo invoque un navegador**.
5. **RA-02 no tiene tramo acá, y se declara.** El visor es un visualizador puro, sin red, sin configuración y sin identidad; esta capa **no compone su bundle, no lo sirve y no lo configura**. Su contribución a RA-02 es negativa y estructural: al no existir ningún punto de acceso pensado para el navegador, no hay nada que el bundle pudiera llamar aunque quisiera. No tener tramo no es incumplirla.
6. **Sin estado.** El intake §17.1.P.3 · GeometriaFactory-Api y §17.1.P.11 · GeometriaFactory-Api declaran REST sin estado y sin sesiones persistentes: lo que se parece a una sesión vive en el circuito de la pieza pública. Ningún punto de acceso de esta superficie depende de lo que ocurrió en la petición anterior.

## 5. Catálogo de casos de uso

### 5.1 `GeometriaFactory-Api`

| CU | Nombre | Capacidad que describe | Puntos de acceso | Estado |
| --- | --- | --- | --- | --- |
| CU-00021 | [`CU-00021` · Dar de alta una cuenta de alumno](Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | El auto-registro, sin campo de contraseña, que deja la cuenta `Pendiente` y sin credencial | A-02 | Propuesto |
| CU-00022 | [`CU-00022` · Ingresar al laboratorio y sostener la sesión](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | El canje, la guardia de los once puntos y el cambio de la contraseña propia con sus dos formas de autenticarse | A-01, A-05, y A-05 a A-15 por la guardia | Propuesto |
| CU-00023 | [`CU-00023` · Gobernar las cuentas de la comisión](Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Listado, cambio de situación con la provisoria devuelta una sola vez, y la única operación destructiva de la superficie | A-06, A-07, A-08 | Propuesto |
| CU-00024 | [`CU-00024` · Resetear la contraseña de un alumno](Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | El único punto que devuelve un valor de credencial, conservando la cuenta y todos sus trabajos | A-09 | Propuesto |
| CU-00025 | [`CU-00025` · Configurar la cuenta de administrador en el primer arranque](Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | La ventana de alta que se cierra con la primera configuración, y el punto que dice si sigue abierta | A-03, A-17 | Propuesto |
| CU-00026 | [`CU-00026` · Enviar un trabajo y ver sus observaciones](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | La única acción de guardado, con el texto conservado íntegro y las observaciones localizadas por figura y campo | A-10, A-11 | Propuesto |
| CU-00027 | [`CU-00027` · Eliminar un trabajo](Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | Un solo punto con dos alcances de reglas opuestas, con la regla adentro y no en la superficie | A-12 | Propuesto |
| CU-00028 | [`CU-00028` · Consultar el listado y el detalle de los trabajos](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Los dos únicos puntos que no escriben, con los dos alcances complementarios y la proyección de listado | A-13, A-14 | Propuesto |
| CU-00029 | [`CU-00029` · Dar desenlace a la revisión](Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | La única transición irreversible de la superficie, con el comentario opcional en los dos desenlaces | A-15 | Propuesto |

**Nueve casos de uso, uno por capacidad de la unidad de entrega.** Eran **doce** hasta la
consolidación 8.5, y la diferencia no es de recorte: **cuatro de aquellos doce no eran casos de uso de
esta unidad** —la traducción del motivo a respuesta de protocolo, la composición de la aplicación, el
arranque del servicio y la colección reproducible— y se reubicaron a `05-Arquitectura-Tecnica`, a
`09-Devops` y a `10-Examples`; los ocho restantes **agrupaban por perfil de autenticación y por
recurso**, que es el criterio correcto de un contrato de superficie y **transversal a las
capacidades**, de modo que dos de ellos se reparten en más de un caso de uso. El motivo y el reparto
punto por punto están en `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1.1 y §2.1.2, y los
treinta y dos documentos absorbidos están en `_legacy/2026-08-16-consolidacion-8.5/`.

**Los nueve declaran el flujo completo, de la persona al almacén.** En el modelo de unidad de entrega
las capas son internas: lo que antes eran tres o cuatro vistas por capa de una misma capacidad —una
por proyecto de código— es hoy un solo documento cuyo actor primario es **quien ejerce la
capacidad**, y no el código de la capa de arriba.

### 5.2 `GeometriaFactory-Domain`

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-02001 | [`CU-00021`](Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | Constituir un alumno con cuenta `Pendiente`, sin credencial derivada y con correo único | Propuesto |
| CU-02002 | [`CU-00023`](Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Habilitar, bloquear, rehabilitar y dar de baja | Propuesto |
| CU-02003 | [`CU-00022`](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Fijar la credencial en el acto de habilitación y reemplazarla después, que es el camino del primer ingreso y el del cambio posterior a un reseteo | Propuesto |
| CU-02004 | [`CU-00022`](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Responder si la cuenta admite acceso y con qué motivo si no lo admite (INV-06) | Propuesto |
| CU-02005 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Constituir el trabajo con dueño, identidad propia y texto original íntegro | Propuesto |
| CU-02006 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Incorporar piezas y componentes con identidad posicional y valores separados | Propuesto |
| CU-02007 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Incorporar advertencias y errores de validación bien formados | Propuesto |
| CU-02008 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Resolver entre `Borrador` y `Pendiente` en la única acción de guardado | Propuesto |
| CU-02009 | [`CU-00028`](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Pertenencia del trabajo y acotación de lo que el alumno opera al borrador | Propuesto |
| CU-02010 | [`CU-00029`](Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | Aprobar o rechazar desde `Pendiente`, con comentario opcional y terminalidad | Propuesto |
| CU-02011 | [`CU-00028`](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Qué trabajos ve el administrador y cuáles puede eliminar | Propuesto |
| CU-02012 | [`CU-00025`](Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | Constituir la única cuenta de administrador, `Habilitado` y con credencial, mientras no exista ninguna | Propuesto |
| CU-02013 | [`CU-00024`](Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | Fijar una contraseña provisoria conservando la cuenta y todos sus trabajos, y poner la marca de cambio de contraseña pendiente (RN-02012, INV-09) | Propuesto |

Trece casos de uso, sobre un mínimo de cinco para el tipo `library`.

**El cambio forzado de contraseña no tiene caso de uso propio, y es una decisión declarada.** La capacidad F-26 tiene dos mitades: el reseteo, que es un acto nuevo del administrador sobre una cuenta ajena, y el cambio obligatorio, que es el acto de la propia cuenta que levanta la marca. La primera es CU-02013. La segunda **es el reemplazo de credencial que CU-02003 ya declaraba**: mismo sujeto, misma precondición de credencial vigente verificada, mismo efecto sobre el atributo. Lo único nuevo es que, cuando la marca está puesta, el reemplazo además la levanta, y eso es un flujo alternativo de CU-02003 y no un contrato distinto. La guarda que impide todo lo demás mientras la marca está puesta vive en **CU-02004**, que es donde ya vive INV-06. Emitir un caso de uso para el cambio forzado habría declarado dos veces la misma superficie, que es exactamente lo que el criterio de fusión de §6 evita.

**Los dos caminos de alta de cuenta son CU-02001 y CU-02012**, y no se fusionan: el auto-registro del alumno nace con la cuenta `Pendiente` y espera habilitación; la configuración del administrador nace `Habilitado`, porque es la cuenta que habilita a las demás y ninguna anterior podría habilitarla a ella.

### 5.3 `GeometriaFactory-Application`

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-04001 | [`CU-00021`](Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | Auto-registro del alumno: correo libre, cuenta constituida en estado `Pendiente` y sin credencial | Propuesto |
| CU-04002 | [`CU-00023`](Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Habilitar, bloquear, rehabilitar y dar de baja, con confirmación escrita y arrastre de los trabajos; **habilitar y rehabilitar producen además la contraseña provisoria** (RN-04016) | Propuesto |
| CU-04003 | [`CU-00022`](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Admisibilidad de la cuenta con su motivo, y fijación —solicitada por CU-04002 al habilitar— y reemplazo de la credencial derivada | Propuesto |
| CU-04004 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Constituir el trabajo con dueño y texto original íntegro, y reeditarlo sólo en `Borrador` | Propuesto |
| CU-04005 | [`CU-00026`](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | La única acción de guardado: interpretar por el puerto, incorporar piezas y observaciones y dejar que el dominio resuelva el estado | Propuesto |
| CU-04006 | [`CU-00028`](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Listado acotado al dueño y sin componentes, y detalle con desenlace y comentario | Propuesto |
| CU-04007 | [`CU-00028`](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Listado de la comisión sin borradores, con dueño para agrupar y filtrar, y detalle equivalente al del alumno | Propuesto |
| CU-04008 | [`CU-00029`](Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | Aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad | Propuesto |
| CU-04009 | [`CU-00027`](Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | Retiro con los dos alcances opuestos: el alumno sólo en `Borrador`, el administrador en todo lo que ve | Propuesto |
| CU-04010 | [`CU-00025`](Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | El segundo camino de alta: cuenta única con papel `Administrador`, `Habilitado` y con credencial, sólo mientras no exista ninguna | Propuesto |
| CU-04011 | [`CU-00024`](Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | Contraseña provisoria **producida por el sistema** y devuelta una vez, con marca de cambio pendiente, conservando la cuenta, **su estado —cualquiera sea—** y **todos sus trabajos** | Propuesto |

Once casos de uso, sobre un mínimo de cinco para el tipo `library`.

### 5.4 `GeometriaFactory-Infrastructure`

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-06001 | [`CU-06001` · Interpretar el texto original y reconstruir las piezas](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) | Lectura tolerante del texto real del alumno, con la cantidad de figuras del conjunto raíz, las piezas con su posición y los errores de validación ubicados | Propuesto |
| CU-06002 | [`CU-06002` · Verificar los valores declarados contra los derivados](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) | La comparación con tolerancia y operador estricto, que señala y no corrige ni rechaza | Propuesto |
| CU-06003 | [`CU-06003` · Guardar y recuperar los trabajos](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) | Materialización y consulta ya acotada, con el texto original conservado íntegro | Propuesto |
| CU-06004 | [`CU-06004` · Ejecutar el borrado físico y el arrastre de la baja](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) | La única operación destructiva del producto: todo o nada, sin borrado lógico | Propuesto |
| CU-06005 | [`CU-06005` · Guardar y recuperar las cuentas de la comisión](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) | Las cuentas con su marca, y las dos preguntas sobre el conjunto que ninguna entidad sola responde | Propuesto |
| CU-06006 | [`CU-06006` · Derivar la contraseña y verificar una credencial](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) | El único punto del producto donde la contraseña en claro se convierte en el valor guardado, y el único que la compara | Propuesto |
| CU-06007 | [`CU-06007` · Producir la contraseña provisoria del reseteo](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) | **La delegación explícita de RN-06014**: un valor no adivinable y que no se repite | Propuesto |
| CU-06008 | [`CU-06008` · Emitir el acceso firmado](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06008-Emitir-El-Acceso-Firmado.md) | Los cuatro reclamos, la firma simétrica y la clave que vive fuera del repositorio de código y de la imagen | Propuesto |
| CU-06009 | [`CU-06009` · Proveer el sello del reloj del sistema](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) | El contrato más corto, y el que explica por qué la capa se puede probar entera con dobles | Propuesto |
| CU-06010 | [`CU-06010` · Preparar el almacén al arrancar](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md) | Crear, transformar el esquema y detener el arranque antes que confiar en un almacén equivocado | Propuesto |

**Diez casos de uso, sobre un mínimo de cinco para el tipo `library`.**

## 6. Reglas de negocio que esta capa hace cumplir

### 6.1 `GeometriaFactory-Api`

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa.

**Trece de las dieciséis tienen tramo acá y tres no lo tienen**, y el recuento cierra en dieciséis. **Dos son las que esta capa puede romper hacia afuera sin que ninguna capa de adentro se entere** —**RN-00003** y **RN-00013**—, y por eso llevan marca propia: la primera se rompe eligiendo un código de respuesta que confirma la existencia de un recurso ajeno; la segunda, dejando un punto de acceso fuera de la guardia.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-02001](Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-00003: el punto de configuración del administrador y su negativa cuando ya existe una. CU-00002: el papel llega en el acceso y cada punto declara cuál exige |
| [RN-02002](Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-00003: el punto de registro traduce el correo ocupado a una respuesta que **no declara la situación ni el papel** de la cuenta que lo ocupa |
| [**RN-02003**](Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | **Tramo de traducción, y es el que esta capa puede romper sola.** CU-00006, CU-00007, CU-00008 y CU-00009: el trabajo ajeno y el inexistente reciben **el mismo código de respuesta y el mismo texto**. La capa de aplicación declara el motivo «que el consumidor traduce a “no encontrado” y nunca a “no autorizado”»; el consumidor es esta capa |
| [RN-02004](Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-00006: los dos alcances sobre el mismo punto. **Es la única regla del producto con un criterio de verificación que exige forzar la petición contra esta superficie**, declarado en el intake §17.1.P.6 · GeometriaFactory-Api |
| [RN-02005](Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | **Sin tramo acá.** El estado llega decidido por el dominio y viaja en una respuesta **exitosa**: un envío cuyo texto no verifica **no es un fallo de protocolo**. Es la confusión más cara de esta capa y CU-00006 la declara |
| [RN-02006](Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | CU-00001: la respuesta **con motivo** que el intake §17.1.P.5 · GeometriaFactory-Api declara, distinta de la respuesta genérica de credenciales inválidas |
| [RN-02007](Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-00004: el punto de baja **transporta el correo escrito como confirmación** y no procede sin él. La comparación y el arrastre son de las capas de adentro |
| [RN-02008](Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | CU-00006: **el borde del proceso es el primer lugar donde el texto puede alterarse** —por codificación, por normalización o por recorte de tamaño— y este contrato declara que no se toca |
| [RN-02009](Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | CU-00007 y CU-00009: la ubicación del defecto **cruza la frontera sin recortarse**. Producirla es de las capas de adentro; no perderla al traducir es de acá |
| [RN-02010](Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | CU-00008: el papel exigido en el punto y la traducción del estado que no admite desenlace, **incluido el terminal** |
| [RN-02011](Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-00007, **de forma negativa**: la superficie **no declara ningún parámetro** con el que el administrador pueda pedir borradores. El alcance llega decidido y acá no se ofrece la puerta por la que la regla se rompería |
| [RN-02012](Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo conserva la cuenta y sus trabajos, y no es una baja | CU-00005, y **CU-00004 por contraste**: son dos puntos de acceso distintos, con verbos distintos, y el del reseteo **no toca ninguna ruta de retiro** |
| [**RN-02013**](Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | **Tramo transversal, y es el otro que esta capa puede romper sola.** CU-00002: la guardia alcanza a **todos** los puntos que exigen acceso salvo el cambio de la propia contraseña. Un punto nuevo que quede fuera de la guardia la rompe sin que nada falle |
| [RN-02014](Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | La provisoria la produce el sistema: no es adivinable y no se repite | **Sin tramo acá.** El valor llega producido y derivado desde `GeometriaFactory-Infrastructure`. Lo que CU-00005 sí declara es **lo que no se hace con él**: no se registra en ninguna traza y se devuelve una sola vez |
| [RN-02015](Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-00005 **de forma estructural**: el punto de acceso **no declara ningún parámetro de situación** y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe |
| [RN-02016](Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria | **Sin tramo propio acá, y con dos efectos estructurales sobre esta superficie.** El primero es un **retiro**: el punto **A-04** deja de existir, porque la escritura anónima que exponía dejó de existir. El segundo es el resultado de **A-07** en CU-00004, que devuelve la provisoria una sola vez. La regla la hace cumplir la capa de aplicación; lo que esta capa aporta es **no exponer ningún punto que la contradiga**, y `Definicion-Superficie-HTTP.md` §7 lo declara como ausencia sostenida |

### 6.2 `GeometriaFactory-Application`

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa, que es una cosa distinta de dónde está enunciada. **Quince de las dieciséis tienen tramo acá** —la excepción es RN-04014, que se explica más abajo—, y en dos el tramo principal está en otra capa: RN-04005, que resuelve el dominio sobre el conjunto de observaciones que esta capa le entrega, y RN-04009, cuyo mensaje ubicado lo produce el validador detrás del puerto. Las dos filas lo declaran.

**Dos de las dieciséis —RN-04012 y RN-04013— entraron con el `PRODUCT-INTAKE` 1.7, otras dos —RN-04014 y RN-04015— con el 1.10 y **RN-04016** con el 1.13; las cinco tienen archivo en `GeometriaFactory-Domain`**, de modo que se enlazan como las once anteriores y el punto abierto de §11 quedó cerrado. **Esta categoría no las redacta**: hacerlo crearía dos enunciados de la misma regla en la misma cadena documental, que es exactamente lo que §9 evita.

**Las dos reglas nuevas merecen una precisión, y la primera es la única sin tramo acá.** **RN-04014** —la provisoria la produce el sistema, no es adivinable y no se repite— sí se **exige por escrito** en `CU-04011` §10, pero **no se ejerce acá**: el valor llega a esta capa ya producido y ya derivado, del mismo lado de la frontera desde el que llega la contraseña que el alumno elige, de modo que quien la ejerce es `GeometriaFactory-Infrastructure` y quien la verifica en prueba es `GeometriaFactory-Contracts` `CU-04008` CA-10. **RN-04015** —resetear no exige cuenta habilitada— se ejerce de forma **negativa**: lo que esta capa hace por ella es **no comprobar** el estado de la cuenta en `CU-04011` §4 y no devolver ningún motivo por ese concepto, que es lo que `CA-06` y `CA-07` verifican.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-02001](Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-04010 (ventana de alta y su negativa), CU-04001 (rechazo del papel `Administrador` por el auto-registro), CU-04002, CU-04003, CU-04007, CU-04008, CU-04011 (verificación de facultad; en CU-04011, además, el acotamiento del reseteo a cuentas de alumno) |
| [RN-02002](Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-04001 y CU-04010: la verificación sobre el conjunto de cuentas es de esta capa, en los dos caminos de alta |
| [RN-02003](Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | CU-04004, CU-04005, CU-04006, CU-04009: la verificación de pertenencia |
| [RN-02004](Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-04009 en sus dos alcances, y CU-04002 en el arrastre de la baja |
| [RN-02005](Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | CU-04005, **con el tramo principal en el dominio**: esta capa entrega el conjunto de observaciones y el dominio resuelve el estado |
| [RN-02006](Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | CU-04003: la consulta de admisibilidad con su motivo. CU-04001 y CU-04010 en cuanto fijan estados iniciales opuestos, que es lo que decide si la cuenta admite acceso desde el alta |
| [RN-02007](Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-04002: la comparación del correo escrito y el retiro de todos los trabajos en la misma unidad de trabajo. **CU-04011 por contraste**: el reseteo no la dispara |
| [RN-02008](Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | CU-04004 y CU-04005: el texto se entrega tal cual y no se reescribe ni cuando la interpretación falla |
| [RN-02009](Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | CU-04005, **con el tramo principal en el validador**, que produce el mensaje ubicado detrás del puerto. Lo que esta capa aporta es la cantidad de figuras del conjunto raíz, que es el rango contra el que la posición se valida, y el rechazo del conjunto mal formado, que no llega al alumno |
| [RN-02010](Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | CU-04008: la verificación de facultad y la propagación de la terminalidad |
| [RN-02011](Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-04007, CU-04008 y CU-04009: el predicado de alcance trasladado a la consulta |
| [**RN-02012**](Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo de contraseña conserva la cuenta y sus trabajos, y no es una baja | CU-04011: la postcondición que deja intactos estado de habilitación, papel, identidad y **todos los trabajos con sus estados y comentarios**, y la ausencia deliberada de todo retiro |
| [**RN-02014**](Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | La contraseña provisoria la produce el sistema: no es adivinable y no se repite entre cuentas ni entre reseteos | **No se ejerce acá**, y `CU-04011` §10 la **exige por escrito** para que no se pierda al bajar de contrato a implementación: el valor llega ya producido y ya derivado. La ejerce `GeometriaFactory-Infrastructure` y la verifica `GeometriaFactory-Contracts` `CU-04008` CA-10 |
| [**RN-02015**](Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-04011, **de forma negativa**: §4 no comprueba el estado de la cuenta, §6 no declara ningún motivo por ese concepto y CA-06 y CA-07 lo verifican. Es también la fuente del cierre sobre la cuenta de administrador que `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` ejerce |
| [**RN-02013**](Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Mientras la provisoria no se cambie, la cuenta no llega a ninguna otra parte del sistema: **se autentica y no obtiene sesión de trabajo** | La cuarta comprobación transversal de §4, en **todos** los casos de uso; CU-04003 FA-06, donde la consulta de admisibilidad devuelve no admisible; CU-04003 FA-05, que es el único lugar donde la marca se levanta; CU-04011, que es el único donde se pone |
| [**RN-02016**](Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que la del reseteo, y la deja con **cambio de contraseña pendiente** | CU-04002, en sus operaciones de **habilitar** y **rehabilitar**: piden el valor al puerto de producción, lo derivan y solicitan fijar la credencial derivada provisoria, de modo que la cuenta queda con la marca puesta y sin ninguna ruta que fije una contraseña sin credencial vigente. **CU-04003 por contraste**: FA-02 es donde la fijación se ejerce y FA-05 el único lugar donde la marca se levanta, y lo hace la propia cuenta |

### 6.3 `GeometriaFactory-Infrastructure`

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa.

**Catorce de las dieciséis tienen tramo acá y dos no lo tienen**, y el recuento cierra en dieciséis. **Tres tienen su tramo principal acá** —RN-06008, RN-06009 y **RN-06014**—, y la consecuencia práctica es directa: **si acá se hacen mal, ninguna capa de más adentro puede repararlas**.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-02001](Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-06005: la restricción de unicidad del almacén, que impide el resultado aunque no explique el camino. CU-06008 transporta el papel en el acceso, sin decidir qué habilita |
| [RN-02002](Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-06005: la segunda línea de la unicidad, con el motivo que la capa de aplicación ya declara recibir por esta vía |
| [RN-02003](Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | CU-06003, **de forma negativa**: la consulta sin recorte declarado **no se resuelve**. Esta capa no comprueba pertenencia; lo que hace es no ofrecer el camino por el que la regla se rompería |
| [RN-02004](Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-06004, **en su mitad de borrado físico**. La acotación por estado y por papel es de la capa de aplicación |
| [RN-02005](Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | CU-06001 y CU-06002 **producen el insumo**: la especie de cada observación. **El estado lo resuelve el dominio** y esta capa no lo decide |
| [RN-02006](Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | **Sin tramo acá.** La admisibilidad se resuelve antes y una cuenta no admitida **no llega** a CU-06008. CU-06005 guarda el estado, que es dato y no comprobación |
| [RN-02007](Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-06004, **en su mitad de arrastre**, con el todo o nada de la unidad de trabajo. La confirmación escrita es de la capa de aplicación |
| [RN-02008](Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | **Tramo principal acá.** CU-06001 no lo devuelve corregido y CU-06003 rechaza toda escritura que lo reemplace (`RC-06001`). Es la capa donde el texto se escribe y se conserva, y por lo tanto donde puede perderse |
| [RN-02009](Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | **Tramo principal acá.** CU-06001 produce el mensaje ubicado y reserva la posición de la figura no reconstruida (`RC-06002`), y CU-06002 emite la advertencia con sus dos valores |
| [RN-02010](Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | **Sin tramo acá.** Esta capa guarda el estado y el comentario; quién puede cambiarlo y desde dónde lo deciden el dominio y la capa de aplicación |
| [RN-02011](Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-06003, **de forma negativa**, igual que RN-06003: el predicado de alcance llega en el pedido y el borrador **no viaja** |
| [RN-02012](Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo conserva la cuenta y sus trabajos, y no es una baja | CU-06005, que escribe la marca **sin tocar el estado ni los trabajos**, y CU-06004 **por contraste**: el reseteo no pasa por el retiro (`RC-06005`, `RC-06007`) |
| [RN-02013](Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | CU-06005: **conserva la marca y la hace viajar**. Sin ese dato, la comprobación transversal de la capa de aplicación no tendría sobre qué decidir. La comprobación **no es de acá** |
| [**RN-02014**](Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | La provisoria la produce el sistema: no es adivinable y no se repite | **Tramo principal, y único, acá: CU-06007.** `GeometriaFactory-Application` §6 declara que es la única de las dieciséis **sin tramo en su capa**, `GeometriaFactory-Contracts` `CU-06008` §10 la exige sin declarar mecanismo, y `RN-06014` §3 nombra a este proyecto de código como el lugar de la generación |
| [RN-02015](Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-06007 **de forma estructural**: la invocación **no recibe** el estado de la cuenta, de modo que no puede comprobarlo. Y CU-06005, que escribe la marca sobre los tres estados sin alterarlos (`RC-06007`) |
| [**RN-02016**](Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria y la deja con cambio de contraseña pendiente | CU-06007, que **produce el valor también para la habilitación**: es el mismo mecanismo y el mismo valor que para el reseteo, y la invocación no lleva ningún dato del acto que la motiva, de modo que no puede distinguirlos (`CU-06007` §3). Y CU-06005, que **escribe la marca** con la credencial derivada provisoria, igual que en el reseteo (`RC-06007`). **Quién habilita y cuándo lo decide la capa de aplicación**, no ésta |

## 7. Matriz NB → CU → RN → US

### 7.1 `GeometriaFactory-Api`

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-00001](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-00003, CU-00004, CU-00005 | RN-00001, RN-00002, RN-00007, RN-00012, RN-00015 | US-00005, US-00006, US-00011, US-00012, US-00013, US-00014, US-00015 |
| [NB-00002](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-00001, CU-00002, CU-00003, CU-00005 | RN-00001, RN-00006, RN-00013, RN-00014 | US-00001, US-00002, US-00003, US-00004, US-00007, US-00008, US-00009, US-00010, US-00016 |
| [NB-00003](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-00006, CU-00007, CU-00011 | RN-00003, RN-00004, RN-00008 | US-00017, US-00018, US-00019, US-00020, US-00021, US-00026, US-00027 |
| [NB-00004](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-00006, CU-00009 | RN-00005, RN-00008, RN-00009 | US-00018, US-00019, US-00024, US-00025 |
| [NB-00005](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-00007 (parcial) | RN-00009 | US-00022 |
| [NB-00006](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-00007 (parcial) | RN-00003 | US-00022 |
| [NB-00007](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-00007 (parcial) | RN-00011 | US-00021, US-00022 |
| [NB-00008](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | CU-00009, CU-00011 | — | US-00025, US-00028, US-00029, US-00030 |
| [NB-00009](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-00006, CU-00007 (parcial), CU-00008 | RN-00004, RN-00010, RN-00011 | US-00020, US-00021, US-00023 |

**Dos de las reglas que la columna cita —RN-00005 y RN-00014— son exactamente las dos que §6 declara *sin tramo en esta capa*.** Figuran igual, y conviene que se lea así: están porque el caso de uso correspondiente **declara explícitamente qué no hace con ellas** —CU-00006, que un envío cuyo texto no verifica no es un fallo de protocolo; CU-00005, que la provisoria no se registra y se devuelve una sola vez—, no porque acá se ejerzan. Quitarlas de la matriz escondería las dos declaraciones que evitan el defecto.

### 7.2 Cobertura bidireccional

**De NB a CU. Las nueve necesidades reciben al menos un caso de uso en este proyecto de código**, y **NB-00008 lo recibe acá por primera vez con tramo propio y no parcial**: `GeometriaFactory-Application` declara explícitamente que no la toca, y `GeometriaFactory-Infrastructure` la declara parcial. Su dolor es de acceso y de despliegue, y **es acá donde el producto se vuelve alcanzable**: el punto de salud y el arranque que se detiene son de esta capa, y la respuesta que la pieza pública convierte en estado degradado explícito sale de acá.

**Nota sobre un recuento de un documento hermano.** `GeometriaFactory-Infrastructure` `Especificacion-Funcional.md` §7.2 declara ser «una de las **dos** secciones del producto» que cubren las nueve necesidades, junto con `GeometriaFactory-Web`. Esa afirmación era exacta cuando se escribió y **con esta emisión pasa a ser tres**. No se corrige desde acá: se declara, y queda listado en §11 para que la próxima intervención sobre aquel documento lo absorba.

**Tres de las nueve quedan cubiertas parcialmente**, y conviene que se lea así:

- **NB-00005.** Lo que esta capa aporta es que la observación con su severidad y su par de valores **cruce la frontera sin recortarse**. Que el alumno la vea, y cómo, es de `GeometriaFactory-Web`.
- **NB-00006.** Lo que aporta es que las piezas, sus componentes y el texto original lleguen al otro lado del proceso. El dibujo es de `GeometriaFactory-Visor` y el árbol y la sincronización son de `GeometriaFactory-Web`.
- **NB-00007.** Lo que aporta es un único punto de listado cuyo alcance llega decidido y que **no ofrece ningún parámetro para pedir borradores ajenos**. La agrupación, el orden y el filtro tal como la persona los ejerce son de `GeometriaFactory-Web`.

**De CU a NB. Diez de los doce casos de uso trazan al menos a una necesidad de negocio, y dos no trazan a ninguna**, lo cual se declara en vez de forzarles una:

| CU | NB que implementa |
| --- | --- |
| CU-00001 | NB-00002 |
| CU-00002 | NB-00002 |
| CU-00003 | NB-00001, NB-00002 |
| CU-00004 | NB-00001 |
| CU-00005 | NB-00001, NB-00002 |
| CU-00006 | NB-00003, NB-00004, NB-00009 |
| CU-00007 | NB-00003, NB-00005 (parcial), NB-00006 (parcial), NB-00007 (parcial), NB-00009 (parcial) |
| CU-00008 | NB-00009 |
| CU-00009 | NB-00004, NB-00008 |
| **CU-00010** | **Ninguna.** Ver abajo |
| CU-00011 | NB-00003, NB-00008 |
| **CU-00012** | **Ninguna.** Ver abajo |

**CU-00010 no traza a ninguna necesidad de negocio, y es correcto que no lo haga.** Conectar un puerto con su adaptador es construcción, no capacidad: ninguna necesidad la pide y nadie la percibe. Inventarle una traza haría creer que hay una necesidad de negocio detrás de una decisión de estructura. Su valor se mide en que **todo lo demás sea probable con dobles**, que es lo que las tres capas de adentro dan por sentado.

**CU-00012 tampoco, y por un motivo distinto: no implementa nada, demuestra.** La colección de peticiones ejercita capacidades que otros casos de uso ya implementan; asignarle las necesidades de esas capacidades las contaría dos veces. Lo que sí tiene es una obligación propia y verificable: **reproducirse en cinco pasos o menos y no inventar ningún texto de prueba**.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-00001 | Canjear correo y contraseña por un acceso firmado con sus cuatro reclamos | CU-00001 |
| US-00002 | Responder credenciales inválidas **sin declarar cuál de los dos campos falló** | CU-00001 |
| US-00003 | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | CU-00001 |
| US-00004 | Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde | CU-00002 |
| US-00005 | Exigir el papel declarado por cada punto de acceso | CU-00002 |
| US-00006 | Aplicar la guardia del cambio de contraseña pendiente a todos los puntos salvo uno | CU-00002 |
| US-00007 | Registrar una cuenta de alumno sin campo de contraseña | CU-00003 |
| US-00008 | Configurar la cuenta de administrador sólo mientras no exista ninguna | CU-00003 |
| US-00009 | Cambiar la contraseña propia con la provisoria como vigente, que es el camino del primer ingreso y el del cambio posterior a un reseteo | CU-00003 |
| US-00010 | Cambiar la contraseña propia exigiendo la vigente | CU-00003 |
| US-00011 | Listar las cuentas de la comisión con su situación y su marca | CU-00004 |
| US-00012 | Cambiar la situación de una cuenta con verificación de papel | CU-00004 |
| US-00013 | Dar de baja una cuenta transportando el correo escrito como confirmación | CU-00004 |
| US-00014 | Resetear la contraseña de un alumno y devolver la provisoria **una sola vez** | CU-00005 |
| US-00015 | No exigir ni comprobar la situación de la cuenta al resetear | CU-00005 |
| US-00016 | No registrar la provisoria en ninguna traza | CU-00005 |
| US-00017 | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | CU-00006 |
| US-00018 | Reenviar un trabajo en `Borrador` con el texto que la persona volvió a pegar | CU-00006 |
| US-00019 | Transportar el texto original **sin normalizarlo en el borde** | CU-00006 |
| US-00020 | Eliminar un trabajo con los dos alcances, verificado **forzando la petición** | CU-00006 |
| US-00021 | Listar trabajos con el alcance ya decidido y sin parámetro para pedir borradores ajenos | CU-00007 |
| US-00022 | Devolver el detalle con piezas, componentes, observaciones y comentario | CU-00007 |
| US-00023 | Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-00008 |
| US-00024 | Traducir cada código del contrato al código de respuesta que le corresponde | CU-00009 |
| US-00025 | Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor | CU-00009 |
| US-00026 | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | CU-00010 |
| US-00027 | Aplicar las transformaciones de esquema al arrancar, sobre almacén inexistente | CU-00011 |
| US-00028 | Detener el arranque en lugar de atender peticiones sobre un almacén en el que no se puede confiar | CU-00011 |
| US-00029 | Responder por el estado del servicio en un punto que no exige acceso | CU-00011 |
| US-00030 | Ejercitar la superficie con una colección reproducible en cinco pasos o menos | CU-00012 |

**Treinta historias previstas, US-00001 a US-00030, sin huecos.**

### 7.4 Las capas internas que cada caso de uso recorre

Hasta la consolidación 8.5 esta sección cruzaba los casos de uso de esta capa con los **once** de
`GeometriaFactory-Application`, porque cada proyecto de código tenía su propia categoría 02. En el
modelo de unidad de entrega **esas vistas ya no existen como casos de uso**: su contenido está dentro
del caso de uso que declara la capacidad, y los documentos de origen están archivados.

Lo que se conserva es el dato que esa tabla hacía visible —**qué recorre cada capacidad hacia
adentro**—, que es lo que permite leer un caso de uso sabiendo dónde vive cada decisión.

| Caso de uso | Recorre hacia adentro |
| --- | --- |
| CU-00021 | Unicidad del correo sobre el conjunto de cuentas, sello del reloj, constitución de la cuenta, materialización |
| CU-00022 | Evaluación de admisibilidad, verificación de credencial, emisión y verificación de la sesión, reemplazo de credencial |
| CU-00023 | Facultad, máquina de transiciones, producción y derivación de la provisoria, arrastre de trabajos en la baja |
| CU-00024 | Facultad, producción y derivación de la provisoria, reemplazo de credencial y puesta de marca como un solo acto |
| CU-00025 | Ausencia de administrador y unicidad del correo sobre el conjunto de cuentas, derivación de la contraseña, constitución |
| CU-00026 | Constitución o reedición del trabajo, motor de interpretación, reconstrucción posicional de piezas, registro de observaciones, resolución de estado |
| CU-00027 | Resolución de alcance por papel, retiro con piezas y observaciones |
| CU-00028 | Resolución de alcance por papel, proyección de listado, recuperación del detalle |
| CU-00029 | Facultad, alcance, transición terminal |

**Ninguno recorre una capa que otro no pueda recorrer.** La separación entre ellos es por capacidad y
no por capa, que es exactamente lo que el modelo anterior no podía expresar.

### 7.2 `GeometriaFactory-Domain`

### 5.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| NB-00001 · Control de admisión y de bajas del laboratorio | CU-02012, CU-02001, CU-02002, CU-02004, CU-02013 | RN-02001, RN-02002, RN-02006, RN-02007, RN-02012, RN-02015, RN-02016 | US-02001, US-02002, US-02004, US-02005, US-02008, US-02024, US-02025, US-02026 |
| NB-00002 · Identidad propia del alumno sin canal de correo | CU-02001, CU-02002, CU-02003, CU-02004, CU-02013 | RN-02001, RN-02002, RN-02006, RN-02012, RN-02013, RN-02014, RN-02016 | US-02001, US-02003, US-02006, US-02007, US-02008, US-02026, US-02027 |
| NB-00003 · Trabajo con dueño, estado y persistencia | CU-02005, CU-02008, CU-02009 | RN-02003, RN-02004, RN-02005, RN-02008 | US-02009, US-02010, US-02015, US-02016, US-02018, US-02019 |
| NB-00004 · Interpretación fiel del dato del alumno | CU-02005, CU-02006, CU-02007, CU-02008 | RN-02005, RN-02008, RN-02009 | US-02010, US-02011, US-02012, US-02014, US-02015, US-02016 |
| NB-00005 · Visibilidad del error de cálculo | CU-02007, CU-02008 | RN-02005 | US-02013, US-02015 |
| NB-00006 · Visualización del trabajo dentro del producto | CU-02006 (parcial: identidad posicional) | RN-02009 | US-02011 |
| NB-00007 · Revisión de la comisión desde un solo lugar | CU-02011 (parcial: alcance de la vista) | RN-02011 | US-02022 |
| NB-00008 · Alcance del laboratorio desde el aula | — | — | — |
| NB-00009 · Desenlace explícito de la entrega | CU-02010, CU-02011 | RN-02004, RN-02010, RN-02011 | US-02020, US-02021, US-02022, US-02023 |

### 5.2 Cobertura bidireccional

**De CU a NB.** Los trece casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-02001 | NB-00002, NB-00001 |
| CU-02002 | NB-00001 |
| CU-02003 | NB-00002 |
| CU-02004 | NB-00001, NB-00002 |
| CU-02005 | NB-00003, NB-00004 |
| CU-02006 | NB-00004, NB-00006 |
| CU-02007 | NB-00005, NB-00004 |
| CU-02008 | NB-00003, NB-00004, NB-00005 |
| CU-02009 | NB-00003 |
| CU-02010 | NB-00009, NB-00003 |
| CU-02011 | NB-00009, NB-00007 |
| CU-02012 | NB-00001 |
| CU-02013 | NB-00001, NB-00002 |

**De NB a CU.** Ocho de las nueve necesidades reciben al menos un caso de uso en este proyecto de código. La restante **no la toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-00008 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: mediciones de viabilidad, despliegue y estado degradado. Este proyecto de código no atiende peticiones ni abre conexiones (PRODUCT-INTAKE §17.1.P.10 · GeometriaFactory-Domain) | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; 09-Devops |

Dos necesidades quedan cubiertas **parcialmente**, y conviene que se lea así:

- **NB-00006.** Lo que este proyecto de código aporta es la identidad posicional de la pieza, que es lo que después permite seleccionarla y resaltarla y lo que sostiene una disposición determinista. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-00007.** Lo que aporta es el **predicado** que decide si un trabajo entra en el alcance del administrador, que es lo que excluye los borradores del listado. La consulta que lo aplica sobre el conjunto, la agrupación y el filtro por alumno viven en `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Web`: el dominio no ejecuta consultas.

### 5.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas. Es el mismo mecanismo con el que `01-Necesidades-Negocio` previó las CU.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-02001 | Constituir un alumno con cuenta `Pendiente` y sin credencial | CU-02001 |
| US-02002 | Rechazar el alta con datos obligatorios ausentes | CU-02001 |
| US-02003 | Exigir la unicidad del correo verificada en el alta | CU-02001 |
| US-02004 | Habilitar, bloquear y rehabilitar una cuenta | CU-02002 |
| US-02005 | Dar de baja una cuenta arrastrando sus trabajos en cualquier estado | CU-02002 |
| US-02006 | Fijar la credencial derivada provisoria en el acto de habilitación | CU-02003, CU-02002 |
| US-02007 | Reemplazar la credencial derivada exigiendo la vigente | CU-02003 |
| US-02008 | Evaluar la admisibilidad de la cuenta y devolver su motivo | CU-02004 |
| US-02009 | Constituir un trabajo con dueño, identidad propia y texto original | CU-02005 |
| US-02010 | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | CU-02005 |
| US-02011 | Reconstruir el conjunto de piezas con identidad posicional | CU-02006 |
| US-02012 | Derivar la familia plana o volumétrica desde el tipo | CU-02006 |
| US-02013 | Registrar advertencias con el valor declarado y el derivado | CU-02007 |
| US-02014 | Registrar errores de validación con posición de pieza y campo | CU-02007 |
| US-02015 | Enviar un trabajo que verifica y pasa a estado `Pendiente` | CU-02008 |
| US-02016 | Enviar un trabajo que no verifica y queda en `Borrador` con sus errores | CU-02008 |
| US-02017 | Rechazar toda transición desde un estado terminal | CU-02008 |
| US-02018 | Resolver la pertenencia de un trabajo a su dueño | CU-02009 |
| US-02019 | Acotar al estado `Borrador` lo que el alumno reedita y elimina | CU-02009 |
| US-02020 | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | CU-02010 |
| US-02021 | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-02010 |
| US-02022 | Excluir los trabajos en `Borrador` del alcance del administrador | CU-02011 |
| US-02023 | Eliminar por el administrador en los tres estados que ve | CU-02011 |
| US-02024 | Configurar la cuenta de administrador en el primer arranque, habilitada y con credencial | CU-02012 |
| US-02025 | Rechazar la configuración de un segundo administrador | CU-02012 |
| US-02026 | Resetear la contraseña de un alumno conservando su cuenta y todos sus trabajos | CU-02013 |
| US-02027 | Exigir el cambio de la contraseña provisoria antes de toda otra capacidad, y levantar la marca al cambiarla | CU-02004, CU-02003 |

### 7.3 `GeometriaFactory-Application`

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-00001](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-04010, CU-04001, CU-04002, CU-04011 | RN-04001, RN-04002, RN-04006, RN-04007, RN-04012 | US-04003, US-04028, US-04001, US-04004, US-04005, US-04006, US-04029, US-04031 |
| [NB-00002](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-04001, CU-04003, CU-04011 | RN-04002, RN-04006, RN-04013 | US-04001, US-04002, US-04007, US-04008, US-04009, US-04030, US-04032 |
| [NB-00003](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-04004, CU-04005, CU-04006, CU-04009 | RN-04003, RN-04004, RN-04005, RN-04008 | US-04010, US-04011, US-04012, US-04015, US-04017, US-04018, US-04026 |
| [NB-00004](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-04004, CU-04005 | RN-04005, RN-04008, RN-04009 | US-04011, US-04013, US-04014, US-04015, US-04016 |
| [NB-00005](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-04005 | RN-04005, RN-04009 | US-04013, US-04015 |
| [NB-00006](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-04006 (parcial: entrega de piezas con identidad posicional) | RN-04003 | US-04019 |
| [NB-00007](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-04007 | RN-04001, RN-04011 | US-04020, US-04021, US-04022 |
| [NB-00008](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | — | — | — |
| [NB-00009](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-04008, CU-04009, CU-04006 (parcial), CU-04007 (parcial) | RN-04004, RN-04010, RN-04011 | US-04018, US-04022, US-04023, US-04024, US-04025, US-04027 |

### 7.2 Cobertura bidireccional

**De CU a NB.** Los once casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-04001 | NB-00002, NB-00001 |
| CU-04010 | NB-00001 |
| CU-04002 | NB-00001 |
| CU-04003 | NB-00002 |
| CU-04004 | NB-00003, NB-00004 |
| CU-04005 | NB-00004, NB-00005, NB-00003 |
| CU-04006 | NB-00003, NB-00009, NB-00006 |
| CU-04007 | NB-00007, NB-00009 |
| CU-04008 | NB-00009 |
| CU-04009 | NB-00003, NB-00009 |
| CU-04011 | NB-00001, NB-00002 |

**De NB a CU.** Ocho de las nueve necesidades reciben al menos un caso de uso en este proyecto de código. La restante **no la toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-00008 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: viabilidad medida, despliegue y estado degradado explícito. Esta capa no atiende peticiones, no abre conexiones y no conoce la frontera de proceso | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; `09-Devops` |

Dos necesidades quedan cubiertas **parcialmente**, y conviene que se lea así:

- **NB-00006.** Lo que esta capa aporta es la entrega de las piezas con su identidad posicional y sus componentes en el detalle, que es el dato con el que después se dibuja y se arma el árbol. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-00007.** Lo que esta capa aporta es el listado con el predicado de alcance ya aplicado y el dato de dueño. La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`, y el panel de resumen es una capacidad de prioridad menor con plazo posterior.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-04001 | Constituir una cuenta de alumno en estado `Pendiente`, sin credencial | CU-04001 |
| US-04002 | Rechazar el alta con un correo ya registrado | CU-04001 |
| US-04003 | Configurar la cuenta del administrador sólo mientras no exista ninguna, habilitada y con credencial | CU-04010 |
| US-04004 | Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad | CU-04002 |
| US-04005 | Dar de baja una cuenta exigiendo el correo escrito como confirmación | CU-04002 |
| US-04006 | Arrastrar en la baja todos los trabajos de la cuenta, en cualquier estado | CU-04002 |
| US-04007 | Devolver el motivo de una cuenta que no admite ingreso | CU-04003 |
| US-04008 | Fijar la credencial derivada provisoria dentro de la habilitación | CU-04003, CU-04002 |
| US-04009 | Reemplazar la credencial derivada exigiendo la verificación de la vigente | CU-04003 |
| US-04010 | Cargar un trabajo con dueño, identificador propio y fecha tomada del reloj | CU-04004 |
| US-04011 | Conservar el texto original íntegro al cargar y al reeditar | CU-04004 |
| US-04012 | Reeditar sólo un trabajo propio en `Borrador`, descartando la interpretación anterior | CU-04004 |
| US-04013 | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | CU-04005 |
| US-04014 | Enviar un trabajo con errores de validación y que quede en `Borrador` con su ubicación | CU-04005 |
| US-04015 | Interpretar el texto por el puerto de validación, sin tocar la base de datos | CU-04005 |
| US-04016 | Terminar de forma controlada cuando la interpretación no está disponible | CU-04005 |
| US-04017 | Listar los trabajos propios con los cuatro estados distinguibles | CU-04006 |
| US-04018 | Ver el desenlace y el comentario del trabajo propio | CU-04006 |
| US-04019 | Devolver el detalle con piezas y componentes, y el listado sin componentes | CU-04006 |
| US-04020 | Listar los trabajos de la comisión excluyendo los borradores | CU-04007 |
| US-04021 | Filtrar el listado de la comisión por alumno, con el recorte vigente | CU-04007 |
| US-04022 | Abrir el detalle de un trabajo de la comisión con los mismos elementos que ve el alumno | CU-04007 |
| US-04023 | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | CU-04008 |
| US-04024 | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-04008 |
| US-04025 | Rechazar toda transición pedida por quien no tiene la facultad o desde un estado terminal | CU-04008 |
| US-04026 | Eliminar un trabajo propio sólo en `Borrador` | CU-04009 |
| US-04027 | Eliminar por el administrador en los tres estados que ve | CU-04009 |
| US-04028 | Rechazar la configuración de un segundo administrador | CU-04010 |
| US-04029 | Resetear la contraseña de un alumno fijando una provisoria, con verificación de facultad | CU-04011 |
| US-04030 | Impedir que una cuenta con cambio de contraseña pendiente ejerza cualquier otra capacidad | CU-04011, y la comprobación transversal de §4 |
| US-04031 | Conservar la cuenta, su estado de habilitación y todos sus trabajos después del reseteo | CU-04011 |
| US-04032 | Levantar la marca con el cambio efectivo hecho por la propia cuenta, y sólo con él | CU-04003 |

### 7.4 Casos de uso de dominio orquestados

Los **doce** casos de uso de `GeometriaFactory-Domain` quedan orquestados por los once de esta capa. Ninguno queda sin orquestar.

| CU de esta capa | CU de dominio que orquesta |
| --- | --- |
| CU-04001 | [CU-00021](Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) |
| CU-04010 | [CU-00025](Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) |
| CU-04002 | [CU-00023](Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) |
| CU-04003 | [CU-00022](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md), [CU-00022](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) |
| CU-04004 | [CU-00026](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md), [CU-00028](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) |
| CU-04005 | [CU-00026](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md), [CU-00026](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md), [CU-00026](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md), CU-04009 |
| CU-04006 | CU-04009 |
| CU-04007 | [CU-00028](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) |
| CU-04008 | [CU-00029](Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md), CU-04011 |
| CU-04009 | CU-04009, CU-04011 |
| CU-04011 | [CU-00024](Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md), la operación de reseteo del dominio |

**CU-04003 del dominio ya no queda orquestado desde dos casos de uso de esta capa.** La versión anterior declaraba que CU-04011 invocaba el reemplazo de CU-04003 «por facultad y sin conocer la credencial vigente»; el dominio tiene una operación propia para eso —**CU-02013**—, que no exige estado `Habilitado` ni declaración de credencial vigente verificada, y CU-04011 pasa a invocarla. La distinción de sujeto y de autorización que aquella nota describía **sigue siendo cierta y ahora la sostiene el dominio**, con dos operaciones en lugar de dos invocaciones de la misma.

### 7.4 `GeometriaFactory-Infrastructure`

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-00001](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-06004, CU-06005, CU-06007 | RN-06001, RN-06002, RN-06007, RN-06012, RN-06014, RN-06015 | US-06012, US-06013, US-06014, US-06015, US-06016, US-06019, US-06020 |
| [NB-00002](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-06005, CU-06006, CU-06007, CU-06008 | RN-06001, RN-06013, RN-06014 | US-06014, US-06017, US-06018, US-06019, US-06020, US-06021, US-06022 |
| [NB-00003](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-06003, CU-06004, CU-06010 | RN-06003, RN-06004, RN-06008 | US-06008, US-06009, US-06010, US-06011, US-06012, US-06024, US-06025 |
| [NB-00004](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-06001 | RN-06005, RN-06008, RN-06009 | US-06001, US-06002, US-06003, US-06004 |
| [NB-00005](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-06002 | RN-06005, RN-06009 | US-06005, US-06006, US-06007 |
| [NB-00006](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-06001 (parcial), CU-06003 (parcial) | RN-06009 | US-06003, US-06011 |
| [NB-00007](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-06003 (parcial) | RN-06011 | US-06010 |
| [NB-00008](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | CU-06010 (parcial) | — | US-06024, US-06025 |
| [NB-00009](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-06003 (parcial), CU-06004 | RN-06004, RN-06011 | US-06009, US-06013 |

### 7.2 Cobertura bidireccional

**De NB a CU. Las nueve necesidades reciben al menos un caso de uso en este proyecto de código.** Es una de las **dos** secciones del producto que lo pueden decir: `GeometriaFactory-Web` también declara la cobertura completa de las nueve en su índice maestro. **En los otros cuatro proyectos de código con documentación emitida hay al menos una necesidad sin caso de uso** —`GeometriaFactory-Domain` y `GeometriaFactory-Application` declaran explícitamente que no tocan NB-00008—. No es un mérito: es una consecuencia de que acá viva el mecanismo de todo lo demás.

**Tres de las nueve quedan cubiertas parcialmente**, y conviene que se lea así:

- **NB-00006.** Lo que esta capa aporta es la **identidad posicional** de la pieza y la entrega de sus componentes en el detalle, que es el dato con el que después se dibuja y se arma el árbol. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-00007.** Lo que aporta es **resolver la consulta con el recorte ya aplicado**. La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`.
- **NB-00008.** Su dolor es de acceso y de despliegue, y esta capa **no atiende peticiones**. Lo único que aporta, y por eso es parcial, es que sus terminaciones degradadas y la detención del arranque de CU-06010 **dejan al producto en un estado que la pieza pública puede declarar**, en lugar de servir datos en los que no se puede confiar. Lo demás es de `GeometriaFactory-Api`, `GeometriaFactory-Web` y `09-Devops`.

**De CU a NB. Nueve de los diez casos de uso trazan al menos a una necesidad de negocio, y uno no traza a ninguna**, lo cual se declara en vez de forzarle una:

| CU | NB que implementa |
| --- | --- |
| CU-06001 | NB-00004, NB-00006 (parcial) |
| CU-06002 | NB-00005 |
| CU-06003 | NB-00003, NB-00006 (parcial), NB-00007 (parcial), NB-00009 (parcial) |
| CU-06004 | NB-00001, NB-00003, NB-00009 |
| CU-06005 | NB-00001, NB-00002 |
| CU-06006 | NB-00002 |
| CU-06007 | NB-00001, NB-00002 |
| CU-06008 | NB-00002 |
| **CU-06009** | **Ninguna.** Ver abajo |
| CU-06010 | NB-00003, NB-00008 (parcial) |

**CU-06009 no traza a ninguna necesidad de negocio, y es correcto que no lo haga.** Es un mecanismo transversal —devolver el momento actual— que ninguna necesidad pide y que existe por una razón de construcción: **que los sellos sean verificables en prueba**. Inventarle una traza sería peor que declarar la ausencia: haría creer que hay una necesidad de negocio detrás de una decisión de testabilidad. Su valor se mide en los casos de uso de la capa de aplicación que lo reemplazan por un doble.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-06001 | Leer el texto real del alumno con tolerancia a comas finales y a las claves sinónimas | CU-06001 |
| US-06002 | Devolver la cantidad de figuras del conjunto raíz, incluidas las no reconstruidas | CU-06001 |
| US-06003 | Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas | CU-06001 |
| US-06004 | Emitir el error de validación con posición de figura y campo | CU-06001 |
| US-06005 | Derivar el valor desde las dimensiones y los componentes | CU-06002 |
| US-06006 | Comparar con tolerancia absoluta y **operador estricto** | CU-06002 |
| US-06007 | Emitir la advertencia con el valor declarado y el derivado, sin corregir ninguno | CU-06002 |
| US-06008 | Conservar el texto original literal y rechazar toda escritura que lo reemplace | CU-06003 |
| US-06009 | Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo | CU-06003 |
| US-06010 | Resolver la consulta con el recorte ya trasladado al pedido | CU-06003 |
| US-06011 | Excluir componentes y texto original del resultado de un listado | CU-06003 |
| US-06012 | Retirar físicamente un trabajo con todo lo que cuelga de él | CU-06004 |
| US-06013 | Arrastrar todos los trabajos de una cuenta dada de baja, todo o nada | CU-06004 |
| US-06014 | Sostener en el almacén la unicidad del correo y la del administrador | CU-06005 |
| US-06015 | Responder si un correo está registrado y si ya existe una cuenta con papel `Administrador` | CU-06005 |
| US-06016 | Conservar y transportar la marca de cambio de contraseña pendiente sin alterar el estado | CU-06005 |
| US-06017 | Derivar una contraseña sin guardarla ni registrarla en claro | CU-06006 |
| US-06018 | Verificar una credencial y distinguir el valor derivado ilegible de la contraseña equivocada | CU-06006 |
| US-06019 | Producir una contraseña provisoria no adivinable y sin repetirse | CU-06007 |
| US-06020 | Terminar sin producir valor cuando la fuente de aleatoriedad no responde | CU-06007 |
| US-06021 | Emitir el acceso firmado con sus cuatro reclamos | CU-06008 |
| US-06022 | Rechazar la emisión sin clave de firma, sin generar una al vuelo | CU-06008 |
| US-06023 | Proveer el sello por un puerto, para que las pruebas lo puedan fijar | CU-06009 |
| US-06024 | Aplicar las transformaciones de esquema al arrancar, sobre base inexistente | CU-06010 |
| US-06025 | Detener el arranque en lugar de operar sobre un almacén en el que no se puede confiar | CU-06010 |

**Veinticinco historias previstas, US-06001 a US-06025, sin huecos.**

## 8. Criterio de recorte aplicado

### 8.1 `GeometriaFactory-Api`

- **Piso y techo.** El piso por tipo lo fija la regla de la categoría y **no se transcribe acá porque el archivo de reglas no está en este repositorio**; no se lo supone ni se lo redondea. El techo lo da la cobertura de las cinco responsabilidades de §3 más la demostración: quedaron **doce**.
- **Particiones.** **La admisión se separó de los puntos de acceso** —CU-00002 frente a los siete restantes— porque es una condición de **todos** ellos y su defecto característico es de omisión: se rompe cuando un punto nuevo queda afuera, y eso no se detecta leyendo el punto sino comparándolo contra la guardia. **La traducción se separó de todo** —CU-00009— porque su unidad de verificación es el conjunto cerrado de códigos del contrato y no un punto de acceso: se prueba recorriendo los quince, no ejerciendo una ruta. **La composición se separó del arranque** —CU-00010 frente a CU-00011— porque terminan distinto: la primera falla en construcción, y la segunda **detiene el servicio**, que es una forma de terminación que ninguna otra parte de esta capa tiene. **El reseteo se separó del gobierno de cuentas** —CU-00005 frente a CU-00004— por el mismo fundamento con el que lo separaron las dos capas de adentro: uno **conserva** la cuenta y sus trabajos y el otro los **elimina**, y ponerlos en el mismo contrato es exactamente la confusión que la capacidad del reseteo vino a cerrar.
- **Fusiones.** El envío y la eliminación quedaron juntos en CU-00006 porque son las dos escrituras que el alumno ejerce sobre su propio trabajo y comparten la comprobación que las acota; el listado y el detalle quedaron juntos en CU-00007 porque son los dos puntos de lectura y se distinguen sólo por la forma del resultado, no por su admisión. **Los cuatro puntos de la credencial propia y del alta quedaron en CU-00003** porque comparten un rasgo que ninguno de los demás tiene y que es lo que hay que poder verificar de una vez: **son los únicos que se ejercen sin acceso firmado o sin que el papel importe**. **Aprobar y rechazar quedaron en CU-00008**, que es la misma fusión que el ensamblado de contratos ya justificó: se distinguen por el valor de un campo de conjunto cerrado.
- **Lo que no se convirtió en caso de uso.** El registro del lado del servidor no recibió contrato propio: es una propiedad transversal que §4 declara una vez y que cada caso de uso ejerce. Tampoco lo recibieron **la ausencia de CORS y la ausencia de WebSockets**, que no son comportamientos sino ausencias declaradas de RA-01, ni **la pasarela de reenvío** del front, que el intake §9 X-9 declara especificada y **no implementada**. Y no lo recibió el despliegue: el intake §17.1.P.8 · GeometriaFactory-Api lo declara manual y a cargo del docente, y su lugar es `09-Devops`.

### 8.2 `GeometriaFactory-Domain`

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron **trece**: once tras la absorción del circuito de revisión, más **CU-02012**, que la corrección del P0 emitió para la capacidad **F-01**, que hasta entonces no tenía caso de uso propio y sobrevivía como flujo alternativo de CU-02001, más **CU-02013**, que `PRODUCT-INTAKE` 1.7 hizo necesario al incorporar la capacidad **F-26**. El alcance del producto había crecido antes: `PRODUCT-INTAKE` 1.3 incorporó el circuito de revisión, `01-Necesidades-Negocio` 1.1 emitió **NB-00009** y pasó de 22 a 27 los casos de uso previstos a nivel producto. La guía de la regla —«library con menos de diez»— es orientativa y la propia regla declara que el techo lo fija la cobertura de las NB; se documenta acá el apartamiento con su causa.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta —habilitar, bloquear, rehabilitar y dar de baja— quedaron en un solo caso de uso, CU-02002, porque `NB-00001` §5 las trata como un único conjunto de cobertura. **El cambio forzado de contraseña se fusionó con el reemplazo de CU-02003** por el mismo criterio: comparten sujeto, precondición y efecto, y lo único propio del cambio forzado —que levanta la marca— es un flujo alternativo y no un contrato distinto (§3). Aprobar y rechazar quedaron en CU-02010 porque son el mismo acto con dos desenlaces, comparten precondición, comentario y terminalidad. El alcance del administrador y su eliminación quedaron en CU-02011 porque las dos responden la misma pregunta: qué trabajos entran en su flujo de trabajo.
- **Particiones.** La reconstrucción de las piezas (CU-02006) se separó del registro de observaciones (CU-02007) porque trazan a necesidades distintas con métricas distintas, que es la misma partición que `01-Necesidades-Negocio` §3.2 justifica entre NB-00004 y NB-00005. **El desenlace se separó del envío** —CU-02010 frente a CU-02008— por los mismos tres criterios con los que 01 partió NB-00009 de NB-00007: sujetos distintos, el alumno que envía y el administrador que decide; reglas distintas, RN-02005 frente a RN-02010; y momentos distintos del ciclo de vida. **El reseteo se separó del ciclo de vida de la cuenta** —CU-02013 frente a CU-02002— por tres motivos que ninguna fusión salvaría: no es una transición de la máquina de estados de cuenta, porque el estado no cambia; **no dispara RN-02007**, que es la regla que gobierna la única operación destructiva de CU-02002; y su efecto propio es poner una marca que ninguna de las cuatro operaciones toca (RN-02012, `Definicion-Modelo-De-Dominio.md` §5.1 y §5.3). Absorberlo en CU-02002 habría puesto en el mismo contrato la operación que **elimina** todos los trabajos del alumno y la que los **conserva**, que es exactamente la confusión que F-26 viene a cerrar. **El alcance del administrador se separó del acceso del alumno** —CU-02011 frente a CU-02009— porque las reglas que los gobiernan son opuestas: al alumno lo acota la pertenencia y el borrador, y al administrador lo acota exactamente lo contrario, todo menos el borrador. **Los dos caminos de alta se separaron** —CU-02012 frente a CU-02001— porque difieren en todo lo que un caso de uso declara: el estado inicial de la cuenta, si la credencial se aporta o se fija después, la ventana en que el alta procede y los códigos de rechazo. Resolverlos en un solo documento fue el origen del P0: el flujo alternativo del administrador atravesaba el paso que fija el estado en `Pendiente`.
- **Lo que no se convirtió en caso de uso.** Todo lo que exige conocer el conjunto de entidades —unicidad efectiva del correo, listados, agrupaciones, filtros— no está acá: el dominio verifica lo que puede verificar sobre una entidad y declara el predicado que las consultas aplican.

### 8.3 `GeometriaFactory-Application`

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron **once**. El décimo es CU-04010 y el undécimo es CU-04011, y las dos causas están declaradas abajo.
- **La partición del reseteo.** **CU-04011 no se fusionó con CU-04002** aunque el administrador ejerza las dos cosas desde el mismo panel, ni con CU-04003 aunque las dos escriban la credencial derivada. Contra CU-04002: el reseteo **no es una transición de la máquina de estados de la cuenta**, escribe credencial, consume el puerto de reloj —que CU-04002 no consume— y deja una marca que las cuatro operaciones de admisión no conocen. Contra CU-04003: el sujeto es otro —el administrador y no la propia persona—, la autorización es otra —facultad y no conocimiento de la credencial vigente— y la postcondición es opuesta, porque CU-04003 FA-05 **levanta** la marca que CU-04011 **pone**. Fusionarlo en cualquiera de los dos habría producido un contrato con postcondiciones contradictorias, que es el defecto que la partición de CU-04001 y CU-04010 ya corrigió una vez.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta quedaron en CU-04002, por el mismo criterio con el que `NB-00001` §5 las trata como un único conjunto de cobertura. El listado y el detalle quedaron juntos —CU-04006 para el alumno, CU-04007 para el administrador— porque comparten la comprobación que los gobierna y se distinguen sólo por la forma del resultado. **La eliminación quedó en un solo caso de uso, CU-04009, con sus dos alcances**, porque los dos responden la misma pregunta y el actor primario del contrato es uno solo: el código consumidor.
- **Particiones.** El envío se separó de la carga —CU-04005 frente a CU-04004— porque son el momento en que el texto entra y el momento en que se interpreta, con reglas distintas y con un puerto distinto de por medio; es la misma partición con la que el dominio separó su CU-04005 de su CU-04008. **Los dos caminos de alta se separaron —CU-04010 frente a CU-04001—**, y la emisión inicial de esta categoría los tenía fusionados. El fundamento de la partición es que no comparten casi nada: el estado inicial es opuesto —`Habilitado` contra `Pendiente`—, la credencial se aporta en uno y se prohíbe en el otro, la ventana de alta existe en uno y no en el otro, y uno se ejerce una sola vez en la vida de la instancia mientras el otro se ejerce una vez por alumno. Lo único que comparten es constituir una cuenta. `GeometriaFactory-Domain` llegó a la misma conclusión y partió su CU-04001 dando de alta su CU-02012; **mantenerlos fusionados acá obligaría a un solo caso de uso a orquestar dos casos de uso de dominio con postcondiciones contradictorias**, que es exactamente lo que produjo el defecto que la ronda r1 del audit levantó. El desenlace se separó de la revisión —CU-04008 frente a CU-04007— por sujetos y reglas distintos, siguiendo la partición que `01-Necesidades-Negocio` §3.2 justificó entre NB-00007 y NB-00009. La consulta del alumno se separó de la del administrador —CU-04006 frente a CU-04007— porque las comprobaciones que las acotan son opuestas: pertenencia contra facultad, y todo lo propio contra todo menos el borrador.
- **Lo que no se convirtió en caso de uso.** La autorización por pertenencia no recibió caso de uso propio aunque se repita en cuatro: es una comprobación transversal declarada en §4, y convertirla en contrato separado duplicaría lo que el dominio ya resuelve en sus CU-04009 y CU-04011. Tampoco lo recibieron la interpretación efectiva del texto, la derivación de la contraseña, **su generación cuando el reseteo la necesita**, la emisión del acceso ni el guardado: son de `GeometriaFactory-Infrastructure` y del consumidor, del mismo lado de la frontera que la derivación. **Los puertos siguen siendo cuatro**: la provisoria llega acá ya producida y ya derivada, exactamente como la contraseña que el alumno elige, de modo que no hace falta una frontera nueva para que el sistema la produzca en lugar del administrador.

### 8.4 `GeometriaFactory-Infrastructure`

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de lo que este proyecto de código implementa. Quedaron **diez**, y la causa es directa: cuatro puertos, dos mecanismos de seguridad y una responsabilidad de arranque no caben en menos sin fusionar contratos que se prueban de formas distintas.
- **Particiones.** **La interpretación se separó de la verificación de valores** —CU-06001 frente a CU-06002— por los mismos criterios con los que el dominio partió su reconstrucción de su registro de observaciones: trazan a necesidades distintas —NB-00004 y NB-00005—, sus observaciones son de **especies distintas** y esas especies tienen **efectos opuestos** sobre el estado del trabajo. Fusionarlos habría puesto en un solo contrato lo que bloquea y lo que no. **El retiro se separó del guardado** —CU-06004 frente a CU-06003 y CU-06005— porque lo que hay que poder verificar del retiro es que **no queda nada**, y eso no es un caso más de la materialización: es la única operación destructiva del producto. **La producción de la provisoria se separó de la derivación** —CU-06007 frente a CU-06006— porque son propiedades distintas con pruebas distintas: una se prueba por no reversibilidad y la otra por **no repetición y no derivabilidad**, y porque CU-06007 es el destinatario de una delegación explícita que conviene poder citar por su identificador. **La preparación del almacén se separó del guardado** —CU-06010 frente a CU-06003— porque su forma de terminación es propia: detiene el arranque.
- **Fusiones.** El guardado y la recuperación quedaron juntos en CU-06003 para trabajos y en CU-06005 para cuentas, porque comparten el almacén, la unidad de trabajo y las condiciones que los gobiernan, y se distinguen sólo por la dirección del dato. **La derivación y la verificación quedaron en CU-06006** porque son la misma función mirada desde los dos lados: no se puede verificar sin saber cómo se derivó. **La emisión y la verificación del acceso quedaron en CU-06008** por el mismo motivo, y porque las dos dependen de la misma clave.
- **Lo que no se convirtió en caso de uso.** El registro del lado del servidor de los errores que se muestran no recibió contrato propio: es una propiedad transversal que §4 declara una vez y que cada caso de uso ejerce. Tampoco lo recibieron el modo de diario ni el respaldo: el primero es un efecto de CU-06010 y el segundo es una operación del docente que ninguna fuente asigna a este proyecto de código.

## 9. Omisiones declaradas

### 9.1 `GeometriaFactory-Api`

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`**, las dieciséis con archivo propio allá, y acá se **referencian** por identificador y con enlace. §6 declara, regla por regla, dónde se ejerce cada una en esta capa. Es el mismo criterio que aplican `GeometriaFactory-Application` §9 y `GeometriaFactory-Infrastructure` §9 |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Omitidos** | El flag `tiene_persistencia` vale **true** en este proyecto de código y en `GeometriaFactory-Infrastructure`, y el `PRODUCT-MANIFEST` §5 declara por qué: acá vale porque **toma de configuración la ruta del archivo y dispara las transformaciones al arrancar**, no porque modele el dato. El intake §17.1.P.4 · GeometriaFactory-Api lo dice en una línea: «delega en `GeometriaFactory.Infrastructure`». El modelo conceptual del producto **ya está emitido**, en `GeometriaFactory-Infrastructure/02-Especificacion-Funcional/Modelo-Datos/`, con sus cinco entidades y sus siete reglas conceptuales; redactarlo de nuevo acá crearía dos descripciones del mismo dato guardado. Lo que sí se documenta acá es lo que esta capa hace con él, en CU-00010 y CU-00011 |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es la **superficie HTTP** | No es una elección de gusto: es lo único de este proyecto de código que existe hacia afuera, es lo que la pieza pública consume y es donde se decide qué se puede romper sin que ninguna capa de adentro se entere. Un lector que abra los doce casos de uso sin haber visto la superficie entera no puede saber si falta un punto de acceso |
| `_legacy/` | **No existe** | Es la emisión inicial de la categoría para este proyecto de código: no hay ninguna versión superada que archivar |

### 9.2 `GeometriaFactory-Domain`

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos/Modelo-Conceptual.md` | **Omitido** | La regla de la categoría lo omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false. El intake declara «no aplica» en §17.1.P.4 · GeometriaFactory-Domain: el dominio no conoce el motor de persistencia. El vocabulario, la semántica y los elementos del concepto viven en `Definicion-Modelo-De-Dominio.md`, que es el documento de concepto central de este proyecto de código |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitido** | Dependen del modelo conceptual, que está omitido, y la regla las omite para `library`. Las restricciones de integridad del dominio están declaradas como los nueve invariantes de `Definicion-Modelo-De-Dominio.md` §4 y como las **dieciséis** reglas de `Reglas-De-Negocio/` |
| `Casos-De-Uso/_legacy/` y `Reglas-De-Negocio/_legacy/` | Existen, con el estado 1.0 archivado | Contienen las copias de la emisión del 2026-08-08 con sufijo de versión, archivadas por el orquestador al publicarse esta revisión. No se editan |

### 9.3 `GeometriaFactory-Application`

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Definicion-<Concepto-Central>.md` | **Omitido** | El concepto central de esta capa son los **puertos**, y los casos de uso ya los describen: cada uno declara cuáles consume y qué le pide a cada uno, y §3 los reúne en una sola tabla. Un documento aparte repetiría eso sin agregar semántica, y la regla lo declara recomendado y no obligatorio para `library` con superficie estrecha |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`** y son atemporales: redactarlas de nuevo acá crearía dos enunciados de la misma regla en la misma cadena documental. Esta categoría las **referencia** por identificador y con enlace, y declara en §6 dónde se ejerce cada una |
| `Modelo-Datos/Modelo-Conceptual.md` y `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitidos** | La regla de la categoría los omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false: el intake declara «no aplica directamente» en §17.1.P.4 · GeometriaFactory-Application. El modelo del dominio vive en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` |

### 9.4 `GeometriaFactory-Infrastructure`

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`**, las dieciséis con archivo propio allá, y son atemporales: redactarlas de nuevo acá crearía dos enunciados de la misma regla en la misma cadena documental. Esta categoría las **referencia** por identificador y con enlace, y §6 declara dónde se ejerce cada una. Es el mismo criterio que `GeometriaFactory-Application` §9 aplica |
| `Modelo-Datos/` | **Emitido, y no omitido** | Es la diferencia con los cinco proyectos de código anteriores y se declara con su fundamento. `GeometriaFactory-Domain` §7 y `GeometriaFactory-Application` §9 omiten estos artefactos con **dos** motivos: que la regla de la categoría los omite para `library`, y que su flag de persistencia es false. **Acá el segundo motivo no se cumple**: es el único `library` del producto con persistencia declarada true en el `PRODUCT-MANIFEST` §5, y el intake declara la persistencia «la responsabilidad central del proyecto de código» (§17.1.P.4 · GeometriaFactory-Infrastructure). Omitirlos dejaría al producto **sin ningún documento que describa el dato guardado**. Se emiten, por lo tanto, como **apartamiento declarado de la guía del tipo**, con la misma forma con la que `GeometriaFactory-Domain` §6 declaró su apartamiento de la guía de «library con menos de diez». Si el orquestador decidiera que la guía del tipo manda sobre el flag, el contenido no se pierde: se muda al documento de concepto central |
| `_legacy/` | `2026-08-10/` | Conserva el estado **1.0** de los veintiséis documentos que la corrección del rechazo de `B-02-03-GeometriaFactory-Infrastructure-r1.md` llevó a 1.1. La emisión inicial no lo tenía, porque no había nada superado que archivar |

## 10. Numeración y nombres de archivo

### 10.1 `GeometriaFactory-Api`

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-00005` de esta categoría no es el `CU-00005` de `GeometriaFactory-Application`, ni el de `GeometriaFactory-Contracts`, ni el de `GeometriaFactory-Infrastructure`. La correspondencia se lee por §3, por la matriz de §7.1 y por la tabla de §7.4, **nunca por número**.
2. **La serie es contigua de CU-00001 a CU-00012**, sin huecos, y su orden es el del recorrido de una petición: primero cómo se obtiene el acceso, después cómo se admite, después qué puntos existen, después cómo se traduce lo que sale, y al final cómo se construye, cómo se arranca y cómo se demuestra.
3. **Los `A-XX` son los puntos de acceso** que [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §3 enumera, y son propios de esta categoría. **No son casos de uso**: un caso de uso puede describir más de un punto de acceso, y §3 de aquel documento declara la correspondencia.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que aquel proyecto de código declaró.
5. **Los códigos `CONTRATO_*` son del ensamblado de contratos** y se citan con su identificador literal, sin renombrarlos y sin traducirlos. Esta categoría **no agrega ninguno**.
6. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las que previó `01-Necesidades-Negocio` ni la de los proyectos de código hermanos.
7. **Los `E-X` son los escenarios del intake §20** y se citan con su identificador de origen, sin renumerar. **Ningún dato de prueba se inventó**: es la regla de delivery del producto que lo prohíbe.

### 10.2 `GeometriaFactory-Domain`

Tres aclaraciones que evitan una lectura equivocada de la trazabilidad:

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** `01-Necesidades-Negocio` §5.3 previó veintisiete casos de uso a nivel producto; esta categoría se emite por proyecto de código, de modo que `CU-02001` de `GeometriaFactory-Domain` no es el mismo artefacto que el `CU-02001` que previó el catálogo de necesidades. La correspondencia entre unos y otros es la matriz de §5.1, que traza por necesidad de negocio y no por número.
2. **Los identificadores `RN-XX` conservan la numeración del intake** y la serie es **contigua de RN-02001 a RN-02016**, sin huecos. Creció en cuatro tramos: `PRODUCT-INTAKE` 1.3 §4.1 transcribió las nueve de la fuente funcional y sumó RN-02010 y RN-02011 del circuito de revisión —con lo que la nota de no contigüidad que esta sección arrastraba por RN-02002 y RN-02006 quedó sin objeto y se retiró—; 1.7 sumó **RN-02012** y **RN-02013** con la capacidad F-26; **1.10** sumó **RN-02014** y **RN-02015**, las dos decisiones del Product Owner sobre esa misma capacidad; y **1.13** sumó **RN-02016**, la decisión sobre la identificación de la cuenta en el primer ingreso. **Cada regla tiene archivo propio en `Reglas-De-Negocio/`**, y son **dieciséis** archivos.
3. **Dos nombres de archivo conservan un slug que ya no describe del todo su enunciado**, y es deliberado: `RN-02004-Eliminacion-Acotada-Al-Borrador.md`, cuyo enunciado se amplió al borrado del administrador, y `RN-02005-Finalizacion-Sin-Errores-De-Validacion.md`, cuyo corte se adelantó del cierre al envío. Los casos de uso de `GeometriaFactory-Contracts` ya citan los dos por esa ruta, y renombrarlos rompería sus enlaces sin agregar información. Cada uno declara la decisión en su control de cambios.

### 10.3 `GeometriaFactory-Application`

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-04009` de esta categoría no es el `CU-04009` de `GeometriaFactory-Domain` ni el que previó el catálogo de necesidades; la correspondencia se lee por la matriz de §7.1 y por la tabla de §7.4, nunca por número.
2. **La serie es contigua de CU-04001 a CU-04011**, sin huecos. **CU-04010 y CU-04011 se numeraron al final y no junto a los casos de uso con los que forman par temático** —CU-04001 y CU-04002 respectivamente—, con el que forma par temático, para no renumerar los ocho casos de uso intermedios que otras categorías ya citan por su identificador. Es la misma decisión con la que `GeometriaFactory-Domain` incorporó su CU-02012.
3. **El nombre de archivo de CU-04001 se conserva** —`CU-04001-Registrar-El-Alta-De-Una-Cuenta.md`— aunque su alcance se acotó al auto-registro, por estabilidad de citación: otras categorías ya lo citan por esa ruta. Es el mismo criterio con el que `GeometriaFactory-Domain` conservó dos nombres de regla.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que ese proyecto de código declaró. **RN-04012 y RN-04013 se citan por enlace como las once anteriores**, porque su archivo aguas arriba ya existe.
5. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las veintisiete que previó `01-Necesidades-Negocio`.

### 10.4 `GeometriaFactory-Infrastructure`

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-06005` de esta categoría no es el `CU-06005` de `GeometriaFactory-Application` ni el de `GeometriaFactory-Domain`. La correspondencia se lee por §3 —qué puerto implementa cada uno— y por la matriz de §7.1, **nunca por número**.
2. **La serie es contigua de CU-06001 a CU-06010**, sin huecos, y su orden es el del recorrido del dato: primero lo que interpreta, después lo que guarda, después lo que protege y al final lo que prepara.
3. **Los identificadores `RC-XX` son propios de esta categoría** y de su carpeta de modelo de datos. **No son reglas de negocio y no compiten con las `RN-XX`**: una regla conceptual de modelo declara cómo el dato sobrevive, no qué decidió el negocio. La serie es contigua de RC-06001 a RC-06007.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que ese proyecto de código declaró.
5. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las que previó `01-Necesidades-Negocio` ni la de los proyectos de código hermanos.
6. **Los `E-X` son los escenarios del intake §20** y se citan con su identificador de origen, sin renumerar. **`T1` a `T4` son las trampas del formato** que el intake declara en §17.1.P.11 · GeometriaFactory-Infrastructure, y tampoco se renumeran.

## 11. Puntos abiertos

### 11.1 `GeometriaFactory-Api`

**Once filas, y ninguna bloqueante. Seis son propias de esta categoría y cinco vienen declaradas de aguas arriba y no se reabren.** De las once, **cuatro están cerradas** —la del establecimiento de la contraseña, las **dos** que eran huecos de la superficie y la del alcance de la colección de peticiones—, y **siete siguen abiertas**. Las tres que cierra `PRODUCT-INTAKE` **1.29** conservan su fila con el desenlace y la fecha, en lugar de retirarse. Eran doce y tres respectivamente en la emisión 1.0.

**Tres cierres del 2026-08-12, por decisión del Product Owner en `PRODUCT-INTAKE` 1.29.** Los **dos huecos de la superficie** que esta categoría encontró y no resolvió —qué código recibe una operación de administrador pedida por quien no lo es fuera del desenlace, y qué código recibe un envío o una reedición forzados fuera de `Borrador`— quedaron cerrados con **dos códigos nuevos del conjunto cerrado**, `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` (§17.4 P.3), que `GeometriaFactory-Contracts` emite y que `Definicion-Superficie-HTTP.md` §6 traduce a `403` y a `409`. **Esta categoría no inventó ninguno de los dos**, que era la condición con la que los declaró abiertos. Y el **alcance de la colección de peticiones** quedó cerrado a favor de **los ocho escenarios `E-1` a `E-8`** (§18), que es exactamente la lectura que esta categoría ya había adoptado: **no cambia ningún artefacto**.

**Cerrado antes, y es el que encabezaba esta tabla: cómo se identifica la cuenta al establecer la contraseña del primer ingreso.** Era el más importante de los propios: la única escritura **de contraseña** de la superficie que ocurría **sin acceso firmado**, con la solicitud de establecimiento declarando «la contraseña elegida» y ninguna fuente declarando cómo viajaba la identidad. **Lo resolvió el Product Owner en `PRODUCT-INTAKE` 1.13 §4.1 con la regla RN-00016**, y no por ninguna de las dos salidas que esta categoría había anticipado —punto anónimo con prueba de posesión, o acceso de alcance acotado— sino **suprimiendo la operación**: habilitar produce una contraseña provisoria, el administrador se la comunica en persona y la cuenta cambia la suya por **A-05**, autenticada. La fila de control de cambios 1.13 del intake registra que fue la emisión de este proyecto de código la que levantó el hueco. Su rastro vive hoy en `CU-00003` §10, en `Definicion-Superficie-HTTP.md` §9 y en la ausencia declarada de §7 de ese mismo documento.

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| ~~**Cómo se identifica la cuenta al establecer la contraseña del primer ingreso**~~ | **CERRADO por `PRODUCT-INTAKE` 1.13 §4.1 (RN-00016)**, ver la prosa de arriba. Enunciado original: es la única escritura de la superficie que ocurre **sin acceso firmado**, porque la persona todavía no puede obtener uno: el ensamblado de contratos declara la solicitud de establecimiento con «la contraseña elegida» y **no declara cómo viaja la identidad de la cuenta**. Un punto de acceso anónimo que acepte correo y contraseña nueva permitiría fijarle la contraseña a cualquier cuenta habilitada que todavía no la tenga. `CU-00003` §10 deja escritas las dos salidas —transportar también la identidad con alguna prueba de posesión, o emitir un acceso de alcance acotado— y **no elige**, porque es una decisión de seguridad y no de forma | **Product Owner**, y `05-Arquitectura-Tecnica` |
| ~~**Qué código del contrato recibe una operación de administrador pedida por un alumno**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §17.4 P.3 (2026-08-12).** El Product Owner incorporó al conjunto cerrado `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR`, que cubre el rechazo por papel **fuera del desenlace** —gobierno de cuentas, listado de la comisión y reseteo—; `GeometriaFactory-Contracts` lo emite en su `Contratos-Abstractions.md` §5.1 y `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` **no cambia de enunciado**. Su fila de traducción está en `Definicion-Superficie-HTTP.md` §6, con destino `403` | **Cerrado**, sin acción pendiente |
| ~~**Qué código del contrato recibe un envío o una reedición forzados fuera de `Borrador`**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §17.4 P.3 (2026-08-12).** El Product Owner incorporó `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`, que cubre el envío y la reedición sobre un trabajo en `Pendiente`, `Finalizado` o `Rechazado`; `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **sigue acotado a la eliminación**. Su fila de traducción está en `Definicion-Superficie-HTTP.md` §6, con destino `409` | **Cerrado**, sin acción pendiente |
| **Las rutas y los verbos definitivos** | **Propio.** Las **dos** únicas cosas que una fuente declara de la superficie son el punto de canje de credenciales y la existencia de un punto de salud, cuya ruta la fuente **no da**. Las **quince** filas de `Definicion-Superficie-HTTP.md` §3 son una **propuesta derivada** de esta categoría, rotulada como tal fila por fila, y su forma definitiva se fija en 05 y se valida en el punto de control de la primera etapa | `05-Arquitectura-Tecnica` |
| **Qué código de respuesta corresponde a una terminación degradada del almacén** | **Propio.** `GeometriaFactory-Infrastructure` declara terminaciones degradadas que **no tienen código propio en el conjunto cerrado del contrato**, y el único que las podría transportar es el genérico. `CU-00009` §6 adopta un código de respuesta para ellas **y lo declara como derivación**, distinguiéndolo del que corresponde a un defecto interno | `05-Arquitectura-Tecnica`, y Product Owner si quisiera un código de contrato propio |
| ~~**El alcance de la colección de peticiones**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §18 (2026-08-12).** El Product Owner resolvió la divergencia **a favor de los ocho escenarios `E-1` a `E-8`**: §18 `S-2` pasa a decir lo mismo que §16.1 ya decía. La lectura que esta categoría había adoptado —los ocho, por `E-8`— **queda confirmada y no cambia ningún artefacto de la categoría 02** | **Cerrado**, sin acción pendiente |
| **Vigencia exacta del acceso firmado** | **Heredado.** El intake declara «corta» y «sin token de refresco», y no fija un número. Es el mismo punto que `GeometriaFactory-Infrastructure` §11 declara abierto, y esta categoría **no lo reabre ni lo resuelve**: lo hereda como condición de su guardia | `05-Arquitectura-Tecnica`, y Product Owner si quisiera fijarlo |
| **Límite de tamaño del cuerpo de una petición** | **Propio.** Ninguna fuente lo declara, y acá se vuelve visible por segunda vez: `GeometriaFactory-Infrastructure` §11 lo declara abierto para el texto que interpreta, y en el borde del proceso el mismo hueco reaparece como límite de cuerpo. **Un límite mal elegido rompe RN-00008 en silencio**, truncando el texto de un alumno | **Product Owner**, y `05-Arquitectura-Tecnica` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la primera etapa. **No es ambigüedad de esta categoría** | `05-Arquitectura-Tecnica` |
| Versiones exactas de los paquetes | El intake §17.1.P.11 · GeometriaFactory-Api lo declara abierto y lo ancla en la primera etapa | `05-Arquitectura-Tecnica`, en la primera etapa |
| Construcción de la imagen en destino desde el repositorio | El intake §17.1.P.11 · GeometriaFactory-Api lo rotula **[A VERIFICAR]** y exige probarlo una vez antes de depender del mecanismo. **No es una asunción de esta categoría** | `09-Devops`, midiendo |
| Valores numéricos de los requerimientos no funcionales | La latencia, el caudal y el arranque en frío están rotulados como asunción aguas arriba. Se usan como vigentes | Product Owner, y `08-Calidad-Y-Pruebas` |

**Y uno que quedó resuelto aguas arriba y se registra para que nadie lo vuelva a abrir**: el desenlace del envío del escenario **E-8**, que el `PRODUCT-INTAKE` **1.12** fija en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21 —es **error de validación**, el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo—. Para esta capa la consecuencia es directa y está en `CU-00006`: **ese envío responde con éxito**, porque el trabajo se guardó y su estado se decidió; lo que no verifica es el texto, no la petición.

**Un residuo de forma de un documento hermano**, que no es un punto abierto de decisión y se anota para que se absorba: la afirmación de `GeometriaFactory-Infrastructure` §7.2 sobre las «dos secciones» que cubren las nueve necesidades, descrita en §7.2 de este documento.

### 11.2 `GeometriaFactory-Domain`

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Nombres de tipos y de espacios de nombres | Declarados abiertos por el intake (§17.1.P.11 · GeometriaFactory-Domain) y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | 05-Arquitectura-Tecnica y la codificación de la etapa `a` |
| Criterio de comparación de dos correos | La unicidad del correo (RN-02002, INV-01) exige decidir si dos correos se comparan tal cual o normalizados. El dominio conserva el dato como lo recibe y no toma la decisión | 05-Arquitectura-Tecnica, junto con la capa que ejerce la verificación |
| **Alcance efectivo de INV-09 fuera de la admisibilidad** | INV-09 enuncia que la cuenta con la marca puesta **no ejerce ninguna capacidad del sistema** salvo cambiar su propia contraseña. El dominio no tiene una puerta única por la que pasen todas las capacidades, de modo que esta categoría concentra la guarda en CU-02004 —la evaluación de admisibilidad—, con el fundamento de que ninguna capacidad se ejerce sin admisión resuelta (`Definicion-Modelo-De-Dominio.md` §4.1). **Es una decisión derivada, no una transcripción**: si la capa que expone habilitara alguna vez un camino que no pase por la admisibilidad, la marca tendría que volver a comprobarse ahí. No es bloqueante y no afecta a ningún criterio de aceptación de esta categoría | 05-Arquitectura-Tecnica y la categoría 02 de `GeometriaFactory-Api`, al fijar por dónde entra cada petición |

**El punto abierto de los sellos de tiempo del trabajo quedó resuelto, y la propuesta de esta categoría era la que el Product Owner adoptó.** `PRODUCT-INTAKE` **§17.1.P.4 · GeometriaFactory-Infrastructure** lleva la «Ampliación del 2026-08-09: sellos de tiempo del trabajo» rotulada **[DECISIÓN del Product Owner]**: `TRABAJO` suma **fecha de creación** y **fecha de última modificación**, distintas de la `Fecha` que el alumno declara —que sigue siendo un dato que él escribe—, y **las produce el consumidor a través del puerto de reloj** para que sean verificables en prueba. Es exactamente lo que esta categoría había propuesto al elevar el punto: los dos atributos aportados por el consumidor, como la fecha de alta del alumno. `Definicion-Modelo-De-Dominio.md` §2.2 los declara desde su versión 1.6. **Matiz que corresponde declarar**: la decisión vive en §17.3, que es la sección técnica de `GeometriaFactory-Infrastructure`, y no en §17.1; lo que baja a la entidad de dominio son los dos atributos y su origen —el consumidor—, y no el mecanismo de reloj, que es del puerto y de la capa que lo consume. **Ningún caso de uso, regla ni invariante de esta categoría cambia** por la incorporación.

**El punto abierto de cómo se identifica la cuenta que establece su contraseña quedó cerrado por el Product Owner, y no era de esta categoría sino de `GeometriaFactory-Api`.** `PRODUCT-INTAKE` **1.13** §4.1 incorpora **RN-02016**: habilitar produce la provisoria, la cuenta queda marcada y el alumno la cambia por el camino de RN-02013. Para esta categoría el efecto es que **la fijación de la credencial deja de ser un acto del alumno anónimo** y pasa a ejercerse dentro de la habilitación de CU-02002, y que dos condiciones se retiran por imposibilidad de su causa: el motivo `CREDENCIAL_NO_ESTABLECIDA` de CU-02004 y el rechazo `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` de CU-02013. **Las 43 condiciones del proyecto de código pasan a 42**: entra `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-02002 y salen las dos anteriores.

**El punto abierto de cómo llega la cuenta con la contraseña reseteada al cambio de contraseña quedó resuelto, y la lectura de esta categoría era la correcta.** `PRODUCT-INTAKE` **1.8** precisa RN-02013: la cuenta con contraseña provisoria **se autentica pero no obtiene sesión de trabajo** —el sistema reconoce la credencial y la deriva al cambio—, con el fundamento de que emitir sesión a una cuenta que por INV-09 no ejerce ninguna capacidad es contradictorio y de que es el paralelo exacto del primer ingreso con contraseña no fijada. Es lo que esta categoría modelaba **sin ingreso** desde su versión 1.4: CU-02004 devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` y el reemplazo de CU-02003 procede igual, porque exige la credencial vigente verificada y no una sesión previa. **Ningún caso de uso, motivo ni criterio de aceptación cambia** por la precisión.

**El punto abierto de la adopción de INV-08 quedó resuelto.** `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain lo incorpora rotulado «adoptado», con la evidencia de las dos puertas como fundamento, y desde esa incorporación se cuenta entre los invariantes vigentes (§4). El registro del recorrido queda en `Definicion-Modelo-De-Dominio.md` §4.2.

Las dos ambigüedades que esta categoría había elevado en su emisión anterior —los enunciados de INV-01 e INV-03, y los de RN-02002 y RN-02006— **están resueltas** en `PRODUCT-INTAKE` 1.3 §4.1 y §17.1.P.2 · GeometriaFactory-Domain, y ninguno de los enunciados fue inventado por esta categoría.

### 11.3 `GeometriaFactory-Application`

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Identificador del puerto de repositorio de cuentas | El intake nombra tres puertos y no éste, que la orquestación de las cuentas y la verificación de unicidad del correo hacen necesario (§3). **No es una regla nueva ni una decisión de alcance**: es un nombre. Acá se lo nombra en lenguaje de dominio | `05-Arquitectura-Tecnica` y el punto de control de la etapa `a` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | `05-Arquitectura-Tecnica` |
| Criterio de comparación de dos correos | La unicidad del correo exige decidir si dos correos se comparan tal cual o normalizados. `GeometriaFactory-Domain` lo dejó abierto y esta categoría **no lo reabre**: lo cita en CU-04001 y lo deja donde está | `05-Arquitectura-Tecnica`, junto con la capa que ejerce la verificación |
| Sellos de alta, de modificación y de desenlace | El intake los sostiene como puertos verificables en prueba, pero **el modelo del dominio no los declara como atributos**: declara la fecha de alta del alumno y la «Fecha» que el alumno declara en su trabajo, y nada más. Esta capa los trata como metadatos de orquestación (§3) y la discrepancia está elevada al Product Owner por `GeometriaFactory-Domain` | Product Owner, y `GeometriaFactory-Domain` si decide incorporarlos a su modelo |
| Valores numéricos de los requerimientos no funcionales | El tiempo de 500 ms del criterio CA-06 de CU-04005 está rotulado como asunción aguas arriba y pendiente de confirmación del Product Owner. Se usa como valor vigente | Product Owner, y `08-Calidad-Y-Pruebas` al verificarlo |

### 11.4 `GeometriaFactory-Infrastructure`

Quince filas, y ninguna bloqueante. **Nueve son propias de esta categoría y seis vienen declaradas de aguas arriba y no se reabren.** De las quince, **una está cerrada** —la condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, que `PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5 confirma el 2026-08-12— y **catorce siguen abiertas**, ocho de ellas propias. La fila cerrada se conserva con su desenlace y su fecha en lugar de retirarse.

**El que era el primero de esta lista ya no está abierto.** Qué devuelve el validador ante el texto del escenario **E-8** lo resolvió el Product Owner y el `PRODUCT-INTAKE` **1.12** lo lleva a su texto vivo: §20.E-8 «Qué verificar» punto 5 y la fila «Dimensión no legible» de §21. El desenlace del envío es **error, no advertencia**: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-06009. El fundamento es que una dimensión ilegible **no es un valor mal calculado sino un valor que no se pudo leer** —la diferencia con las advertencias de E-3 es que allá el sistema entiende lo que el alumno escribió y discrepa del resultado, y acá no lo entiende—, y es además el modo de falla **más probable** de los ocho escenarios, porque lo produce la configuración regional de la máquina y no un error del alumno. El resultado está declarado en `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7 y verificado por `CU-06001` **CA-12**.

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| **Hasta dónde llega el conjunto de tipos reconstruibles** | Propio. Los seis que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar; el análisis del que sale el intake menciona siete clases en `Ejemplo1` y diez en `Ejemplo2` y **ninguna fuente las enumera**, de modo que no se puede afirmar si alguna emite un tipo fuera de los seis | Product Owner, con la enumeración de las clases de la Actividad 1 |
| **Cómo se sostiene que la provisoria «no se repite»** | Propio. `CU-06007` §10 adopta que la sostiene la impredecibilidad y **descarta** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas y el producto no guarda contraseñas en claro. **Es una decisión derivada, no una transcripción** | Product Owner, para confirmarla o reemplazarla |
| **Longitud y alfabeto de la contraseña provisoria** | Propio. Ninguna fuente los declara. `CU-06007` §10 deja escrita la tensión que hay que resolver —transcribible de viva voz y a la vez lejos de lo adivinable— y **no la resuelve** | `05-Arquitectura-Tecnica` |
| **Vigencia exacta del acceso firmado** | Propio. El intake declara «corta» y «sin acceso de refresco», y no fija un número | `05-Arquitectura-Tecnica`, y Product Owner si quisiera fijarlo |
| **De dónde sale el valor derivado del área de una pieza volumétrica** | Propio. El intake la muestra dos veces como **suma de los componentes** —el cilindro de E-1 y el ortoedro de E-2— y una vez como fórmula —`6·l²` en el cubo de E-3—, y las dos formas coinciden en ese cubo. No hay contradicción declarada, pero tampoco hay una regla enunciada. `CU-06002` §10 adopta la suma de componentes y lo declara. Detalle en `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | `05-Arquitectura-Tecnica`, al fijar la tabla de derivación por tipo |
| **Límite de tamaño del texto que se acepta** | Propio. Ninguna fuente lo declara, y el requerimiento no funcional declarado está medido sobre un texto de tres piezas. Un texto arbitrariamente grande no tiene hoy ningún corte declarado. Detalle en `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | Product Owner, y `05-Arquitectura-Tecnica` |
| **Zona horaria y precisión de los sellos** | Propio. Ninguna fuente las declara, y afectan a cómo se guardan las dos fechas del trabajo y la fecha de alta de la cuenta. Detalle en `Modelo-Datos/Modelo-Conceptual.md` §7, en `CU-06009` §10 y en `RC-06006` | `05-Arquitectura-Tecnica` |
| **Fecha de última modificación de la cuenta** | Propio. El modelo del dominio **no la declara** y el consumidor no la registra; este modelo no la incorpora por su cuenta. Si el Product Owner la quisiera, entraría por el dominio y no por acá. Detalle en `Modelo-Datos/Modelo-Conceptual.md` §7 | Product Owner |
| ~~**La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`**~~ | **CERRADA — CONFIRMADA por `PRODUCT-INTAKE` 1.29 §17.3 P.11 punto 5 (2026-08-12).** El Product Owner la **confirma tal como está**, en lugar de reemplazarla, y adopta el fundamento que esta categoría había declarado: «0 advertencias» ante una verificación pedida sin reconstrucción sería indistinguible de un trabajo verificado sin discrepancias, y del lado del visor una escena vacía sin motivo es el **fallo silencioso** que el producto viene a eliminar. **Ninguna fila de `CU-06002` §6, ningún criterio de aceptación y ninguna entrada del catálogo de 03 cambian**: lo que cambia es que la condición deja de ser derivación de esta categoría y pasa a estar enunciada por la fuente | **Cerrado**, sin acción pendiente |
| Cuál función de derivación de clave se ancla, y con qué parámetros | El intake declara «PBKDF2 o Argon2» y no elige. `CU-06006` declara la propiedad y no el mecanismo | `05-Arquitectura-Tecnica`, en la primera etapa |
| Identificador del puerto de repositorio de cuentas | Declarado abierto por `GeometriaFactory-Application` §11. Esta categoría **no lo reabre** y lo nombra en lenguaje de dominio | `05-Arquitectura-Tecnica` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la primera etapa | `05-Arquitectura-Tecnica` |
| Criterio de comparación de dos correos | Declarado abierto por `GeometriaFactory-Domain` y por `GeometriaFactory-Application`. **Acá se vuelve visible**, porque la restricción de unicidad del almacén lo materializa, y esta categoría **no lo resuelve** | `05-Arquitectura-Tecnica`, junto con la capa que ejerce la verificación |
| Frecuencia del respaldo | El intake la declara explícitamente «a definir por el docente». **No es una omisión de esta categoría**: es una decisión de operación que la fuente dejó abierta, y `Modelo-Datos/Modelo-Conceptual.md` §7 la registra sin resolverla | Product Owner, y `09-Devops` |
| Valores numéricos de los requerimientos no funcionales | Los 200 ms de la interpretación y los 30 segundos del arranque en frío están rotulados como asunción aguas arriba. Se usan como vigentes | Product Owner, y `08-Calidad-Y-Pruebas` |

**Y dos que quedaron resueltos aguas arriba y se registran para que nadie los vuelva a abrir**: los **sellos de tiempo del trabajo**, que el intake incorpora al modelo de datos con rótulo de decisión del Product Owner y que `RC-06006` recoge; y **la tolerancia de 0.01 con operador estricto**, que el intake fija con su fundamento y que `CU-06002` transcribe sin margen.

## 12. Catálogo de reglas de negocio

### 12.1 `GeometriaFactory-Domain`

Las **dieciséis** reglas del producto, con el invariante que expresa a cada una como condición permanente sobre los datos. La correspondencia es de PRODUCT-INTAKE §17.1.P.2 · GeometriaFactory-Domain: **los invariantes no son reglas distintas, son las mismas vistas desde el dominio.**

| RN | Enunciado en una línea | Invariante | CU afectados | Estado |
| --- | --- | --- | --- | --- |
| RN-02001 | [`RN-02001` · Administrador único y papeles fijos](Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | INV-05 | CU-02012, CU-02002, CU-02001, CU-02004 | Propuesto |
| RN-02002 | [`RN-02002` · El correo del alumno es único](Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | INV-01 | CU-02001, CU-02012 | Propuesto |
| RN-02003 | [`RN-02003` · Un alumno sólo ve y opera sus propios trabajos](Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | INV-02 | CU-02009 | Propuesto |
| RN-02004 | [`RN-02004` · El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve](Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | INV-03 | CU-02005, CU-02008, CU-02009, CU-02011 | Propuesto |
| RN-02005 | [`RN-02005` · Un trabajo no pasa a estado `Pendiente` con errores de validación](Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | INV-04 | CU-02007, CU-02008, CU-02010 | Propuesto |
| RN-02006 | [`RN-02006` · Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso](Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | INV-06 | CU-02002, CU-02003, CU-02004 | Propuesto |
| RN-02007 | [`RN-02007` · La baja arrastra los trabajos y exige confirmación escrita](Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | — | CU-02002 | Propuesto |
| RN-02008 | [`RN-02008` · El texto original del alumno se conserva íntegro](Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | — | CU-02005, CU-02006, CU-02007 | Propuesto |
| RN-02009 | [`RN-02009` · Toda observación de error indica la posición de la pieza y el campo](Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | — | CU-02006, CU-02007 | Propuesto |
| RN-02010 | [`RN-02010` · El desenlace es exclusivo del administrador y es terminal](Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | INV-07 | CU-02005, CU-02006, CU-02008, CU-02010 | Propuesto |
| RN-02011 | [`RN-02011` · El administrador no ve los trabajos en borrador](Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md) | — | CU-02010, CU-02011 | Propuesto |
| RN-02012 | [`RN-02012` · El reseteo de contraseña conserva la cuenta y sus trabajos](Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | INV-09 | CU-02013, CU-02002 | Propuesto |
| RN-02013 | [`RN-02013` · Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte](Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | INV-09 | CU-02004, CU-02003, CU-02013 | Propuesto |
| RN-02014 | [`RN-02014` · La contraseña provisoria la produce el sistema, no la escribe el administrador](Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | — | CU-02013, CU-02003 | Propuesto |
| RN-02015 | [`RN-02015` · Resetear no exige que la cuenta esté habilitada](Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | — | CU-02013, CU-02002, CU-02004 | Propuesto |
| RN-02016 | [`RN-02016` · Habilitar una cuenta produce su contraseña provisoria](Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | INV-09 | CU-02002, CU-02003, CU-02004, CU-02013 | Propuesto |

Las **seis** filas sin invariante asociado —sobre dieciséis— lo están por un motivo declarado: RN-02007, RN-02008, RN-02009 y **RN-02014** describen comportamientos y no condiciones permanentes sobre el estado; **RN-02015** enuncia la **ausencia** de una precondición, que tampoco es una condición sobre los datos; y RN-02011 es una regla de alcance de consulta (PRODUCT-INTAKE §17.1.P.2 · GeometriaFactory-Domain, cuya prosa enumera esas seis y agrega a RN-02012). **RN-02012, RN-02013 y RN-02016 comparten invariante**, INV-09, y no es un descuido de la tabla: las dos primeras son las dos mitades de la misma condición —qué conserva el reseteo y qué no puede la cuenta hasta cambiar la provisoria—, y **RN-02016 no agrega una mitad nueva sino un segundo origen** de la misma marca. **Diez reglas con invariante y seis sin él.** **La fuente de esa lectura es la columna «regla de negocio que sostiene» de INV-09 en `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain, que dice «RN-02012, RN-02013», y no su prosa**, que en esa misma sección enumera a RN-02012 entre las reglas **sin** invariante asociado y remata «RN-02013 sí lo tiene, y es INV-09». El intake es ambiguo en este punto y esta categoría lo declara en lugar de taparlo: se adopta la lectura de la columna, con el fundamento que `Definicion-Modelo-De-Dominio.md` §4.3 desarrolla —RN-02012 sin INV-09 no tendría cómo impedir que la provisoria sirviera indefinidamente—, y **no se afirma que la prosa del intake lo declare**, porque dice lo contrario. Consolidar una de las dos formas es del Product Owner sobre su propio documento.

Los nueve invariantes no llevan archivo propio: son propiedades permanentes del modelo y viven enunciados en [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) §4.1. **INV-08, que esta categoría había propuesto como candidato, está adoptado** por `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain y se cuenta entre los vigentes; §4.2 conserva el registro de su recorrido. **INV-09 es nuevo del intake 1.7** y es el que sostiene a las **tres** reglas de la contraseña provisoria. **RN-02014 y RN-02015, que el intake 1.10 suma sobre la misma capacidad, no traen invariante y no lo necesitan**: la primera describe cómo se produce un valor que a este proyecto de código le llega ya derivado, y la segunda declara que una precondición no existe. **RN-02016, que el intake 1.13 suma, sí trae invariante y es INV-09**, porque enuncia una condición sobre los datos —ninguna cuenta de alumno `Habilitado` sin credencial, y ninguna habilitación sin marca— y no un comportamiento.

**Desfase sobre la letra de INV-09, declarado y hoy cerrado.** El enunciado que `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain le daba a INV-09 dijo, hasta la versión **1.13**, que la marca «la pone **únicamente** el reseteo del administrador». Esa frase era de la **1.7** y la contradecía la propia 1.13, cuya §4.1 declara en RN-02016 que habilitar deja la cuenta con cambio de contraseña pendiente y cita a INV-09 al hacerlo. Esta categoría adoptó la decisión —**dos orígenes de la marca**— y no la letra que la fuente todavía no había actualizado, y elevó la consolidación al Product Owner sobre su propio documento. **El intake `1.14`, del 2026-08-09, la consolidó**: reescribió el enunciado de INV-09 —«la marca la ponen **únicamente** las dos operaciones que producen una contraseña provisoria: el **reseteo** (RN-02014) y la **habilitación** (RN-02016)»— y lo registró en la fila 1.14 de su control de cambios, corrección **(a)**. **Desde la 1.14 la letra de la fuente coincide con la decisión que esta categoría venía sosteniendo**, y no queda desfase que declarar. La misma traza está en `Definicion-Modelo-De-Dominio.md` §4.1.

## 13. Los puertos que esta capa declara

### 13.1 `GeometriaFactory-Application`

Los puertos son la frontera de este proyecto de código: lo que declara acá lo implementa `GeometriaFactory-Infrastructure`, y la composición de raíz los provee. `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application y §14 los nombran una vez, y esa es la única cita de identificadores de código de esta categoría: `IWorkRepository`, `IFigureValidator` e `ISystemClock`. En el resto de los artefactos los puertos se nombran en lenguaje de dominio, porque los nombres definitivos de tipos se validan en el punto de control de la etapa `a`.

| Puerto | Qué le pide esta capa | Casos de uso que lo consumen |
| --- | --- | --- |
| Repositorio de trabajos | Recuperar un trabajo, resolver una consulta ya acotada por dueño o por alcance, materializar el resultado y ejecutar el retiro | CU-04002, CU-04004, CU-04005, CU-04006, CU-04007, CU-04008, CU-04009 |
| Validación de figuras | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | CU-04005 |
| Reloj del sistema | Los sellos de alta, de modificación y de desenlace, **para que sean verificables en prueba** | CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010, CU-04011 |
| Repositorio de cuentas | Recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado, **incluida la marca de cambio de contraseña pendiente** | CU-04001, CU-04002, CU-04003, CU-04007, CU-04010, CU-04011 |

**El repositorio de cuentas no lleva identificador declarado en el intake**, que nombra los otros tres. No es una invención de esta categoría: `GeometriaFactory-Domain` §1 de su índice asigna explícitamente a esta capa la verificación de la unicidad del correo «sobre el conjunto de alumnos», y ninguna verificación sobre un conjunto es posible sin una frontera que lo alcance. Queda declarado como punto abierto en §11.

**Dos precisiones sobre lo que viaja por los puertos:**

- **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa.** No son la «Fecha» que el alumno declara en su trabajo, que sí modela el dominio como dato del alumno. El modelo del dominio declara la fecha de alta del alumno —que recibe del consumidor, sin leer el reloj— y **no declara** fecha de última modificación de la cuenta ni fecha de creación, de modificación o de desenlace del trabajo. La discrepancia está elevada al Product Owner: hasta que resuelva, estos sellos se leen como dato de esta capa y no como atributos del dominio.
- **La cantidad de figuras del conjunto raíz la produce el validador** al interpretar el texto, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción y su registro de observaciones la hereda como rango de posiciones válidas, de modo que CU-04005 —único orquestador de los dos— es quien la hace viajar.

**El alcance de la unidad de trabajo es un caso de uso, una transacción**: cada caso de uso abre a lo sumo una y no la reparte entre varias operaciones.

## 14. Autorización por pertenencia y verificación de facultad

### 14.1 `GeometriaFactory-Application`

Es lo que hace que el flag `tiene_auth` valga true en este proyecto de código, y es transversal a los once casos de uso. No es autenticación: acá no se comparan contraseñas ni se emiten accesos, y quién es la persona llega ya resuelto desde afuera.

| Comprobación | Qué verifica | Respuesta cuando falla | Dónde se ejerce |
| --- | --- | --- | --- |
| **Pertenencia** | Que el trabajo pedido sea del alumno solicitante | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que el consumidor traduce a «no encontrado» y **nunca** a «no autorizado» | CU-04004, CU-04005, CU-04006, CU-04009 |
| **Facultad** | Que quien pide una operación reservada tenga el papel `Administrador` | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, que sí admite ser explícito: no hay recurso ajeno cuya existencia proteger | CU-04002, CU-04007, CU-04008, CU-04011 |
| **Alcance del administrador** | Que el trabajo no esté en `Borrador`, porque los borradores no forman parte de su flujo de trabajo | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | CU-04007, CU-04008, CU-04009 |
| **Cambio de contraseña pendiente** | Que la cuenta solicitante **no** esté marcada por un reseteo del administrador. Es la comprobación que hace exigible el invariante **INV-09** del intake §17.1.P.2 · GeometriaFactory-Domain | `CAMBIO_DE_CONTRASENA_PENDIENTE`. No lee ni escribe nada: la cuenta **se autentica y no obtiene sesión de trabajo**, y lo único que puede hacer es cambiar su contraseña (RN-04013, intake 1.8 §4.1) | **Todos**, con una sola excepción declarada: el reemplazo de CU-04003 FA-05, que es lo único que la levanta. Ver la precisión 5 |

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos códigos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador—, y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.

Cinco precisiones que rigen en toda la categoría:

1. **El papel no reemplaza a la pertenencia.** Son dos comprobaciones distintas: un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso.
2. **La negativa por pertenencia y la negativa por facultad no se confunden.** La primera oculta la existencia del recurso; la segunda no tiene nada que ocultar.
3. **La comprobación se hace sobre el dato recuperado y antes de escribir.** No se resuelve ocultando un control en la pantalla, y por eso es verificable con dobles sin base de datos.
4. **El trabajo ajeno y el identificador inexistente comparten motivo por diseño.** Distinguirlos permitiría averiguar por tanteo qué identificadores existen.
5. **La cuarta comprobación corta antes que las otras tres, y tiene una sola excepción.** Una cuenta marcada como con cambio de contraseña pendiente no ejerce **ninguna** capacidad del sistema —ni siquiera las que su papel y su pertenencia admitirían—, salvo cambiar su propia contraseña por el reemplazo de CU-04003 FA-05. La marca la ponen **las dos** operaciones que producen una contraseña provisoria —la **habilitación** de CU-04002 (RN-04016) y el **reseteo** de CU-04011 (RN-04014)— y la levanta **únicamente** ese cambio, hecho por la propia cuenta: eso es INV-09, y es lo que hace que la provisoria sea provisoria. Sin él, una clave que el administrador conoce quedaría sirviendo indefinidamente para operar como el alumno. **Es una comprobación de esta capa y no una decisión de ruteo del front**: ocultar rutas acota lo que se ofrece y no hace cumplir nada.

## 15. Los cuatro puertos que implementa y los dos mecanismos que provee

### 15.1 `GeometriaFactory-Infrastructure`

**Los cuatro puertos son los que `GeometriaFactory-Application` §3 declara**, y esta categoría no los redefine: los implementa. Los nombres de los tres primeros los declara el intake —`IWorkRepository`, `IFigureValidator` e `ISystemClock`—; el cuarto, el **repositorio de cuentas**, **no lleva identificador declarado aguas arriba** y es un punto abierto que esta categoría **no reabre y no resuelve**.

| Puerto | Qué implementa acá | CU |
| --- | --- | --- |
| Repositorio de trabajos | Recuperar, resolver la consulta ya acotada, materializar y ejecutar el retiro | CU-06003, CU-06004 |
| Repositorio de cuentas | Recuperar, responder las dos preguntas sobre el conjunto, materializar y ejecutar el retiro | CU-06005, CU-06004 |
| Validación de figuras | Interpretar el texto, reconstruir las piezas y verificar los valores | CU-06001, CU-06002 |
| Reloj del sistema | Devolver el momento actual | CU-06009 |

Y **dos mecanismos que no son puertos de la capa de aplicación** y que por eso conviene distinguir: no los declara nadie como contrato de inversión, sino que los consume la composición de raíz de `GeometriaFactory-Api` y, a través de ella, los casos de uso que los necesitan.

| Mecanismo | Qué provee | CU |
| --- | --- | --- |
| Credenciales | Derivar una contraseña, verificar una credencial y **producir la contraseña provisoria** de la habilitación y del reseteo | CU-06006, CU-06007 |
| Acceso firmado | Emitir y verificar el acceso, con sus cuatro reclamos | CU-06008 |

**Y una responsabilidad que no es ni puerto ni mecanismo**: dejar el almacén en condiciones antes de que el servicio atienda su primera petición (CU-06010). La invoca el arranque de `GeometriaFactory-Api` y no la invoca ningún caso de uso.

**El alcance de la unidad de trabajo es el que la capa de aplicación declara**: un caso de uso, una unidad de trabajo. Del lado de acá se expresa como una por operación.

## 16. Lo que esta capa hace y lo que no decide

### 16.1 `GeometriaFactory-Infrastructure`

Es la frontera que hace que el flag de autenticación valga true en este proyecto de código, y la que hay que dejar imposible de confundir, porque **acá están los dos mecanismos que el producto no puede permitirse mal hechos**.

**Enunciado en una línea: esta capa provee el mecanismo y no toma ninguna decisión de negocio.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Derivar una contraseña y verificar una credencial contra un valor derivado | **Sí** (CU-06006) | — |
| **Producir la contraseña provisoria** de la habilitación y del reseteo, no adivinable y sin repetirse | **Sí** (CU-06007). Es la delegación explícita de las tres capas de arriba | — |
| Emitir y verificar el acceso firmado, con su clave fuera del repositorio de código y de la imagen | **Sí** (CU-06008) | — |
| Guardar y recuperar, conservando el texto original íntegro | **Sí** (CU-06003, CU-06005) | — |
| Interpretar el texto del alumno y emitir observaciones con posición y campo | **Sí** (CU-06001, CU-06002) | — |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto: una cuenta que no admite acceso **no llega a la emisión** | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar la pertenencia de un trabajo o la facultad de administrador | **No.** Cuando esta capa resuelve una consulta acotada, el recorte **ya venía decidido** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Se entrega el conjunto de observaciones y **el dominio resuelve** | `GeometriaFactory-Domain` |
| Comparar el correo escrito como confirmación de una baja | **No.** Llega resuelto | `GeometriaFactory-Application` |
| Traducir un motivo a respuesta de protocolo | **No.** Los códigos de esta capa son valores de enumeraciones cerradas | `GeometriaFactory-Api` |

Cinco precisiones que rigen en toda la categoría:

1. **El traslado del recorte no es una comprobación de autorización.** Que una consulta llegue acotada por dueño o por alcance es una decisión ya tomada afuera; acá se resuelve el pedido tal como viene. Duplicarla en el almacén crearía un segundo lugar donde la regla puede decir otra cosa.
2. **Las restricciones de unicidad del almacén sí son una segunda línea, y eso es deliberado.** La consulta previa del consumidor no es una garantía por sí sola, y `GeometriaFactory-Application` `CU-06001` **FA-02** ya declara ese camino como flujo alternativo propio, con el mismo motivo de correo ocupado.
3. **Ninguna condición de error de esta capa deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.
4. **Ningún mensaje de esta capa incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma.** Es RA-03, que es regla de nivel producto, y su contracara es que **todo error que se muestre queda registrado del lado del servidor**.
5. **De las tres reglas de arquitectura del intake §14, sólo RA-03 tiene tramo acá. RA-01 y RA-02 no lo tienen, y se declara.** **RA-01** —ningún JavaScript del navegador llama a la API— no aplica porque esta capa **no tiene superficie de navegador**, no atiende peticiones y su único consumidor declarado es la composición de raíz de `GeometriaFactory-Api`. **RA-02** —el visor es visualizador puro, sin red, sin configuración y sin identidad— no aplica porque esta capa **no es el visor ni compone su bundle**; su contenido se respeta desde afuera, en la frontera que `Definicion-Contrato-Del-Validador-De-Figuras.md` §8 traza con la fachada y en `CU-06001` **CA-11**, que exige **cero peticiones de red** originadas por el contrato del validador. No tener tramo no es incumplirlas: es no tener superficie donde puedan romperse.

## 17. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con las capas juntas. Los documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
