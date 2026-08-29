# Matriz de cobertura de pruebas — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 2.2
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

**Seis de las diez secciones son comunes a las cuatro capas. Las otras cuatro existen en una sola, y
eso es lo que la consolidación hace visible:**

| Sección | Sólo en | Por qué |
| --- | --- | --- |
| Trazabilidad punto de acceso ↔ tests | `GeometriaFactory-Api` | Es la única capa con superficie HTTP |
| Trazabilidad comprobación de autorización ↔ tests | `GeometriaFactory-Application` | Es donde vive la verificación de pertenencia, que no es la guardia por papel |
| Trazabilidad regla conceptual de modelo ↔ tests | `GeometriaFactory-Infrastructure` | Es la única con modelo de datos materializado |
| La batería del validador contra los escenarios | `GeometriaFactory-Infrastructure` | Los ocho escenarios `E-1` a `E-8` los ejerce el validador de figuras |

**Ninguna de las cuatro es prescindible y ninguna se puede fundir con otra**: trazan cosas distintas
contra pruebas distintas. Leída sólo la matriz del host, la trazabilidad de invariantes y la de
reglas conceptuales del producto **no aparecían por ningún lado**.

---

## 1. Propósito y alcance

### 1.1 `GeometriaFactory-Api`

Es el documento bisagra de la categoría: relaciona los **doce** casos de uso, los **diecisiete** NFR, las **dieciséis** reglas de negocio, los **quince** puntos de acceso y los **nueve** invariantes con los **treinta y siete** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el sistema no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de punto de acceso contra prueba y la de invariante contra prueba. La primera, porque `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api` **100 % de endpoints cubiertos**, y esa cobertura no cabe dentro de la tabla de casos de uso: un caso de uso agrupa varios puntos. La segunda, porque `05` §10.3 declara qué aporta esta capa a cada invariante, y en dos de ellos —`INV-02` e `INV-09`— **la propiedad observable se decide acá**.

### 1.2 `GeometriaFactory-Domain`

Es el documento bisagra de la categoría: relaciona los **trece** casos de uso, los **seis** NFR, las **dieciséis** reglas de negocio y los **nueve** invariantes con los **veintisiete** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido. La matriz se actualiza al cerrar cada etapa, que es la cadencia que [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §5 declara.

Esta matriz **agrega una cuarta tabla** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de invariante contra prueba. El motivo es que en este proyecto de código los invariantes no son un subconjunto de las reglas —diez reglas tienen invariante y seis no lo tienen— y `05` §8 declara un NFR propio sobre su ejercicio.

### 1.3 `GeometriaFactory-Application`

Es el documento bisagra de la categoría: relaciona los **once** casos de uso, los **nueve** NFR, las **dieciséis** reglas de negocio, las **cuatro** comprobaciones de autorización y los **nueve** invariantes con los **treinta y un** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido. La matriz se actualiza al cerrar cada etapa, que es la cadencia que [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §5 declara.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de comprobación de autorización contra prueba y la de invariante contra prueba. El motivo de la primera es que `05` §8 declara un NFR propio sobre el ejercicio de las cuatro comprobaciones, con umbral numérico; el de la segunda, que los invariantes no son un subconjunto de las reglas y esta capa aporta a cada uno algo distinto de lo que aporta el dominio (`05` §10.3).

### 1.4 `GeometriaFactory-Infrastructure`

Es el documento bisagra de la categoría: relaciona los **diez** casos de uso, los **catorce** NFR, las **dieciséis** reglas de negocio, las **siete** reglas conceptuales de modelo y los **diez** casos de la batería del validador con los **treinta y cinco** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de regla conceptual de modelo contra prueba y la de la batería del validador contra escenario. La primera, porque las **siete** reglas conceptuales de `02` no son reglas de negocio y son propias de esta capa: declaran **cómo el dato sobrevive**, no qué decidió el negocio. La segunda, porque la batería es una puerta bloqueante del pipeline con recuento propio y su trazabilidad no cabe dentro de la tabla de casos de uso.

## 2. Trazabilidad CU ↔ tests

### 2.1 `GeometriaFactory-Api`

Doce filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias | Estado |
| --- | --- | --- | --- | --- |
| CU-00001 Canjear credenciales por un acceso firmado | Given credenciales válidas, When se las canja, Then se emite el acceso; con credenciales inválidas, Then la respuesta **no declara cuál campo falló**; con la cuenta no admitida, Then **el motivo sí se dice** | `TC-00001`, `TC-00002`, `TC-00003` | US-00001, US-00002, US-00003 | `Pendiente` |
| CU-00002 Admitir la petición: acceso, papel y marca | Given un acceso ausente, vencido o con firma ajena, Then la petición se rechaza; el papel se exige por punto; y **la guardia del cambio pendiente alcanza a todos los puntos salvo uno** | `TC-00004`, `TC-00005`, `TC-00006`, `TC-00007` | US-00004, US-00005, US-00006 | `Pendiente` |
| CU-00003 Exponer el alta de cuenta y la credencial propia | Given el registro **sin campo de contraseña** y la configuración del administrador con su ventana, When se los ejerce, Then proceden una sola vez cada uno; el cambio de contraseña propia recorre sus dos formas | `TC-00008`, `TC-00009`, `TC-00010` | US-00007, US-00008, US-00009, US-00010 | `Pendiente` |
| CU-00004 Exponer el gobierno de las cuentas de la comisión | Given el administrador, When lista, cambia la situación o da de baja, Then cada punto exige su papel y **la baja transporta el correo escrito sin compararlo acá** | `TC-00011`, `TC-00012`, `TC-00013` | US-00011, US-00012, US-00013 | `Pendiente` |
| CU-00005 Exponer el reseteo de la contraseña de un alumno | Given una cuenta en cualquiera de sus tres estados, When se la resetea, Then procede, **la provisoria se devuelve una sola vez** y **no aparece en ninguna traza** | `TC-00014`, `TC-00015`, `TC-00016` | US-00014, US-00015, US-00016 | `Pendiente` |
| CU-00006 Exponer el envío y la eliminación de un trabajo | Given un texto cuyo contenido no verifica, When se lo envía, Then **la respuesta es exitosa** con el estado decidido; el texto **no se normaliza y no se trunca**; la eliminación se verifica **forzando la petición** | `TC-00017`, `TC-00018`, `TC-00019`, `TC-00020` | US-00017, US-00018, US-00019, US-00020 | `Pendiente` |
| CU-00007 Exponer el listado y el detalle de los trabajos | Given el listado, When se inspecciona su superficie, Then **no hay parámetro con el que pedir borradores ajenos**; el detalle trae piezas, componentes, observaciones y comentario | `TC-00021`, `TC-00022` | US-00021, US-00022 | `Pendiente` |
| CU-00008 Exponer el desenlace de la revisión | Given un trabajo en estado `Pendiente` y el administrador, When lo aprueba o rechaza, Then alcanza su estado terminal; desde un terminal, desde un `Borrador` o con papel de alumno, Then se rechaza con códigos distintos | `TC-00023` | US-00023 | `Pendiente` |
| CU-00009 Traducir el motivo del contrato a respuesta de protocolo | Given los **diecisiete** códigos, When se recorre la tabla, Then **14** tienen destino y **1** está declarado sin él; las **tres** familias empobrecidas dan respuestas indistinguibles; **0** respuestas exponen la topología | `TC-00024`, `TC-00025`, `TC-00026`, `TC-00027` | US-00024, US-00025 | `Pendiente` |
| CU-00010 Componer la aplicación y conectar los puertos con sus adaptadores | Given la composición, When se resuelve, Then **4 de 4** puertos tienen un adaptador y falta alguno **falla en construcción**; hay **1** sola configuración de intercambio en el producto | `TC-00028`, `TC-00029` | US-00026 | `Pendiente` |
| CU-00011 Arrancar el servicio y dejar el almacén en condiciones | Given un almacén inexistente, When arranca, Then dispara la preparación; si no puede completarse, **el arranque se detiene y no se atiende ninguna petición**; salud responde **sin exigir acceso** | `TC-00030`, `TC-00031`, `TC-00032`, `TC-00033` | US-00027, US-00028, US-00029 | `Pendiente` |
| CU-00012 Ejercitar la superficie con la colección de peticiones reproducible | Given la colección versionada, When se la ejecuta, Then recorre la superficie en **5 pasos o menos** con **0 datos de prueba inventados** | `TC-00035` | US-00030 | `Pendiente` |

**Doce de doce casos de uso con al menos un caso de verificación, y treinta de treinta historias cubiertas.**

**Treinta y seis de los treinta y siete `TC-XX` tienen fila en alguna de las cinco tablas de trazabilidad de esta matriz.** El restante es una inspección con umbral exacto cuya trazabilidad es hacia una **regla de arquitectura de nivel producto**, un **riesgo** y un **criterio de aceptación de etapa**, y por eso no aparece en ninguna de ellas. Está en §2.1, y **no queda sin instrumento de trazabilidad**.

### 2.1 La inspección con umbral exacto que traza a una regla de arquitectura

| Caso de verificación | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-00036` Sin canal de sesión interactiva y sin intercambio de origen cruzado | **Tres ausencias**: no expone ni requiere canal de sesión interactiva, no tiene configuración de intercambio de origen cruzado, y ningún punto de acceso está pensado para el navegador | `RA-01`; el sexto riesgo de `05` §9; criterio de aceptación de la etapa `a` | `Pendiente` |

**Por qué no se le inventa un `CU-XX` ni un punto de acceso.** Lo que mide son **ausencias** en la superficie entera, no el comportamiento de un punto; reabrir cualquiera de las tres rompe `RA-01`, que es regla de nivel producto y no propiedad de un caso de uso. Lo que corresponde es que **esté enumerada**, y esta subsección es su instrumento.

### 2.2 `GeometriaFactory-Domain`

Trece filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Test | Tipo | Estado |
| --- | --- | --- | --- | --- |
| CU-02001 Registrar el alta de un alumno | Given los datos obligatorios y la unicidad declarada comprobada, When se constituye el alumno, Then la cuenta nace `Pendiente` y sin credencial; con un dato ausente, con la unicidad no declarada, con credencial aportada, con estado pedido o con papel `Administrador`, Then se rechaza | `TC-02001`, `TC-02002` | Unit | `Pendiente` |
| CU-02002 Gobernar el ciclo de vida de la cuenta | Given una cuenta de alumno y la provisoria derivada, When se la habilita, bloquea o rehabilita, Then la transición procede y la habilitación fija la credencial y pone la marca; When se la da de baja con confirmación coincidente, Then arrastra sus trabajos | `TC-02003`, `TC-02004`, `TC-02005` | Unit e integración interna | `Pendiente` |
| CU-02003 Fijar y reemplazar la credencial derivada | Given la credencial vigente declarada verificada y un valor nuevo ya derivado, When se reemplaza, Then el valor se reemplaza; y si la marca estaba puesta, Then además se levanta | `TC-02008`, `TC-02009`, `TC-02003` | Unit e integración interna | `Pendiente` |
| CU-02004 Evaluar la admisibilidad de la cuenta | Given una cuenta en cada uno de sus tres estados, con y sin la marca, When se evalúa la admisibilidad, Then devuelve admisible o no admisible **con su motivo**, y nunca lanza | `TC-02010`, `TC-02009` | Unit | `Pendiente` |
| CU-02005 Crear y reeditar un trabajo | Given dueño, nombre, fecha declarada y texto original, When se constituye, Then nace en `Borrador` con el texto íntegro; When se reedita fuera de `Borrador`, Then se rechaza | `TC-02011`, `TC-02012` | Unit | `Pendiente` |
| CU-02006 Reconstruir el conjunto de piezas | Given un conjunto de piezas con su posición en el conjunto raíz, When se lo adopta, Then las posiciones se conservan sin renumerar y la familia se deriva del tipo; una figura no adoptada **reserva su posición** | `TC-02013`, `TC-02014` | Integración interna | `Pendiente` |
| CU-02007 Registrar las observaciones del trabajo | Given una advertencia con su par de valores y un error con índice de figura y campo, When se los registra, Then se adoptan; sin el par o sin la ubicación, Then se rechazan | `TC-02015`, `TC-02016`, `TC-02014` | Unit e integración interna | `Pendiente` |
| CU-02008 Gobernar el estado del trabajo en el envío | Given un trabajo en `Borrador` cuyo resultado no trae errores, When se lo envía, Then pasa a `Pendiente` aunque tenga advertencias; con al menos un error, Then queda en `Borrador` | `TC-02017`, `TC-02018`, `TC-02019` | Integración interna | `Pendiente` |
| CU-02009 Resolver el acceso del alumno a un trabajo | Given un trabajo ajeno y uno inexistente, When se resuelve el acceso, Then los dos resultados son idénticos; ver procede en los cuatro estados y reeditar y eliminar sólo en `Borrador` | `TC-02020`, `TC-02012` | Unit | `Pendiente` |
| CU-02010 Resolver el desenlace del trabajo | Given un trabajo en `Pendiente` y el papel `Administrador`, When se lo aprueba o se lo rechaza, Then alcanza su estado terminal con comentario opcional; en otro estado o con otro papel, Then se rechaza | `TC-02022`, `TC-02019` | Unit | `Pendiente` |
| CU-02011 Resolver el alcance del administrador | Given un trabajo en `Borrador`, When el administrador consulta su alcance, Then queda fuera; en los otros tres estados, Then entra y admite eliminación | `TC-02021` | Unit | `Pendiente` |
| CU-02012 Configurar la cuenta de administrador | Given la ausencia de administrador declarada y la credencial derivada, When se configura, Then nace `Habilitado`; una segunda configuración se rechaza | `TC-02006` | Unit | `Pendiente` |
| CU-02013 Resetear la contraseña de una cuenta de alumno | Given una cuenta de alumno en cualquiera de sus tres estados, When se la resetea, Then conserva estado, papel, identidad y **todos** sus trabajos, y queda con la marca puesta | `TC-02007`, `TC-02005` | Integración interna | `Pendiente` |

**Trece de trece casos de uso con al menos un caso de prueba.** Ninguno queda huérfano.

**Veinticinco de los veintisiete `TC-XX` referencian un `CU-XX`, una `RN-XX`, un `INV-XX` o un NFR, y tienen fila en alguna de las cuatro tablas de esta matriz.** Los **dos** restantes son pruebas de inspección estructural cuya trazabilidad es hacia una **ADR**, una **tarea técnica** o un **quality gate**, y por eso no aparecen en ninguna de esas cuatro tablas. Están en §2.1, y **ninguno queda sin instrumento de trazabilidad**.

### 2.1 Las dos pruebas de inspección estructural y a qué trazan

| Caso de prueba | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-02025` El dominio no lee el reloj ni el conjunto | Cero ocurrencias de lectura de reloj y de consulta de conjuntos en las operaciones públicas, y dos ejecuciones idénticas sin fijar el reloj del entorno | [`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `BT-02009`; `05` §7, filas de configuración y de zona horaria | `Pendiente` |
| `TC-02027` Ninguna condición prevista viaja como excepción | Las **42** condiciones llegan como valor de retorno tipado y **0** invocaciones lanzan; la distinción con el defecto de programación del consumidor se verifica aparte | [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `BT-02007`; `QG-08` | `Pendiente` |

**Por qué no se les inventa un `CU-XX` ni una `RN-XX`.** Las dos verifican decisiones de arquitectura, no comportamiento pedido por un caso de uso: atarlas a un identificador funcional para que la tabla cerrara sería exactamente la clase de trazabilidad falsa que esta matriz existe para evitar. Lo que sí corresponde es que **estén enumeradas**, y esta subsección es su instrumento.

### 2.3 `GeometriaFactory-Application`

Once filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-04001 Registrar el alta de una cuenta | Given un correo que el repositorio declara libre, When se invoca el auto-registro, Then la cuenta nace `Pendiente`, sin credencial y con papel `Alumno`; con correo ocupado, con credencial aportada, con estado pedido o con papel `Administrador`, Then se rechaza | `TC-04001`, `TC-04002` | US-04001, US-04002 | `Pendiente` |
| CU-04002 Gobernar las cuentas de la comisión | Given el administrador y una cuenta de alumno, When la habilita, bloquea o rehabilita, Then la transición procede y las dos que producen provisoria dejan **la marca puesta**; When la da de baja con el correo escrito que coincide, Then arrastra sus trabajos en **una sola** unidad de trabajo | `TC-04004`, `TC-04005`, `TC-04010` | US-04004, US-04005, US-04006, US-04008 | `Pendiente` |
| CU-04003 Resolver el ingreso y la credencial del alumno | Given una cuenta en cada estado, con y sin la marca, When se consulta la admisibilidad, Then devuelve admisible o no admisible **con su motivo sin colapsar**; When la propia cuenta reemplaza su credencial presentando la vigente verificada, Then el valor se reemplaza **y la marca se levanta** | `TC-04008`, `TC-04009`, `TC-04010`, `TC-04004` | US-04007, US-04009, US-04032, US-04008 | `Pendiente` |
| CU-04004 Cargar y reeditar un trabajo propio | Given dueño, texto original y el momento del puerto de reloj, When se constituye, Then nace en `Borrador` con el texto íntegro; When se reedita fuera de `Borrador` o sobre trabajo ajeno, Then se rechaza | `TC-04013`, `TC-04014` | US-04010, US-04011, US-04012 | `Pendiente` |
| CU-04005 Enviar un trabajo e interpretar su texto | Given un resultado de interpretación sin errores, When se envía, Then el dominio lo lleva a `Pendiente` aunque tenga advertencias; con al menos un error, Then queda en `Borrador`; con el puerto no disponible, Then termina de forma controlada y el texto queda intacto | `TC-04015`, `TC-04016`, `TC-04017`, `TC-04018`, `TC-04019` | US-04013, US-04014, US-04015, US-04016 | `Pendiente` |
| CU-04006 Consultar los trabajos propios del alumno | Given trabajos propios y ajenos, When el alumno pide su listado, Then recibe sólo los propios, con los cuatro estados y **sin componentes**; el detalle trae piezas, componentes, desenlace y comentario | `TC-04020`, `TC-04022` | US-04017, US-04018, US-04019 | `Pendiente` |
| CU-04007 Revisar los trabajos de la comisión | Given trabajos de dos alumnos en los cuatro estados, When el administrador pide el listado, Then **ningún `Borrador`** aparece y el filtro por alumno se compone con el alcance; el detalle es equivalente al del alumno | `TC-04021`, `TC-04022` | US-04020, US-04021, US-04022 | `Pendiente` |
| CU-04008 Dar desenlace a un trabajo | Given un trabajo en `Pendiente` y el administrador, When lo aprueba o lo rechaza, Then alcanza su estado terminal con comentario opcional; sin facultad, fuera del alcance o desde un estado terminal, Then se rechaza con el motivo que corresponde | `TC-04023`, `TC-04024` | US-04023, US-04024, US-04025 | `Pendiente` |
| CU-04009 Eliminar un trabajo | Given un trabajo propio en `Borrador` y el alumno, When lo elimina, Then se retira; Given los tres estados que el administrador ve, Then los tres se retiran; los dos alcances son opuestos y ninguno se filtra en el otro | `TC-04025` | US-04026, US-04027 | `Pendiente` |
| CU-04010 Configurar la cuenta de administrador | Given que el repositorio declara que no existe administrador y la credencial ya derivada, When se configura, Then nace `Habilitado`; una segunda configuración se rechaza | `TC-04003` | US-04003, US-04028 | `Pendiente` |
| CU-04011 Resetear la contraseña de un alumno | Given una cuenta de alumno en cualquiera de sus tres estados y el administrador, When se la resetea con la provisoria ya producida y derivada, Then conserva estado, papel, identidad y **todos** sus trabajos, y queda con la marca puesta; sobre la cuenta de administrador, Then se rechaza | `TC-04006`, `TC-04007`, `TC-04010`, `TC-04011` | US-04029, US-04030, US-04031 | `Pendiente` |

**Once de once casos de uso con al menos un caso de prueba, y treinta y dos de treinta y dos historias cubiertas.** Ninguno queda huérfano.

**Treinta de los treinta y un `TC-XX` referencian un `CU-XX`, una `RN-XX`, un `INV-XX`, una comprobación o un NFR, y tienen fila en alguna de las cinco tablas de esta matriz.** El restante es una prueba de inspección estructural cuya trazabilidad es hacia un **quality gate**, una **ADR** y un **riesgo**, y por eso no aparece en ninguna de esas cinco tablas. Está en §2.1, y **no queda sin instrumento de trazabilidad**.

### 2.1 La prueba de inspección estructural y a qué traza

| Caso de prueba | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-04031` Ninguna condición prevista viaja como excepción | Las **36** condiciones del catálogo se devuelven como valor con su código y **0** casos de uso lanzan; la indisponibilidad de un puerto tampoco lanza y devuelve `PARSE_RESULT_UNAVAILABLE` | `QG-11`; [`ADR-04006`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); el quinto riesgo de `05` §9 | `Pendiente` |

**Por qué no se le inventa un `CU-XX`.** Verifica una decisión de arquitectura que atraviesa los once casos de uso sin ser propiedad de ninguno; atarla a uno para que la tabla cerrara sería trazabilidad falsa. Lo que corresponde es que **esté enumerada**, y esta subsección es su instrumento.

### 2.4 `GeometriaFactory-Infrastructure`

Diez filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-06001 Interpretar el texto original y reconstruir las piezas | Given el texto real del alumno con sus **cuatro** trampas de formato, When se lo interpreta, Then las piezas se reconstruyen con su posición, la cantidad de figuras del conjunto raíz **incluye las no reconstruidas** y los errores llegan con índice de figura y campo | `TC-06001`, `TC-06002`, `TC-06003`, `TC-06004`, `TC-06007`, `TC-06008`, `TC-06009`, `TC-06010`, `TC-06011`, `TC-06012`, `TC-06013` | US-06001, US-06002, US-06003, US-06004 | `Pendiente` |
| CU-06002 Verificar los valores declarados contra los derivados | Given piezas ya reconstruidas, When se derivan área y volumen, Then se comparan con tolerancia **0.01** y operador **estricto**, y la discrepancia **se señala con los dos valores, sin corregir ni rechazar** | `TC-06005`, `TC-06006`, `TC-06007`, `TC-06009` | US-06005, US-06006, US-06007 | `Pendiente` |
| CU-06003 Guardar y recuperar los trabajos | Given un trabajo con sus piezas, componentes y observaciones, When se lo materializa, Then queda en **una** unidad de trabajo con el texto **literal**; una escritura que lo reemplace se rechaza; y una consulta **sin recorte declarado no se resuelve** | `TC-06016`, `TC-06017`, `TC-06018`, `TC-06019` | US-06008, US-06009, US-06010, US-06011 | `Pendiente` |
| CU-06004 Ejecutar el borrado físico y el arrastre de la baja | Given un trabajo o una cuenta con sus trabajos, When se ejecuta el retiro, Then es **físico y todo o nada**; con el almacén interrumpido, **no se retira nada** | `TC-06020`, `TC-06021` | US-06012, US-06013 | `Pendiente` |
| CU-06005 Guardar y recuperar las cuentas de la comisión | Given el almacén, When se materializa una cuenta con un correo ocupado o un segundo administrador, Then se rechaza; las **dos** preguntas sobre el conjunto se responden sin revelar la cuenta; y **la marca viaja sin alterar el estado** | `TC-06022`, `TC-06023`, `TC-06024` | US-06014, US-06015, US-06016 | `Pendiente` |
| CU-06006 Derivar la contraseña y verificar una credencial | Given una contraseña en claro, When se la deriva, Then el valor derivado lleva **sus parámetros versionados** y la contraseña **no queda escrita en ninguna parte**; el derivado ilegible **se distingue** de la contraseña equivocada | `TC-06025`, `TC-06026` | US-06017, US-06018 | `Pendiente` |
| CU-06007 Producir la contraseña provisoria del reseteo | Given dos producciones consecutivas, When se comparan, Then **son distintas** y ninguna es derivable de un dato conocido; sin fuente de aleatoriedad, **no se produce ningún valor por otro medio** | `TC-06027`, `TC-06028` | US-06019, US-06020 | `Pendiente` |
| CU-06008 Emitir el acceso firmado | Given los **cuatro** reclamos y la clave, When se emite, Then la firma verifica; **sin clave no hay emisión** y no se genera una al vuelo | `TC-06029`, `TC-06030` | US-06021, US-06022 | `Pendiente` |
| CU-06009 Proveer el sello del reloj del sistema | Given el adaptador, When se le pide el momento, Then lo devuelve. Es el contrato más corto de la capa | `TC-06031` | US-06023 | `Pendiente` |
| CU-06010 Preparar el almacén al arrancar | Given un almacén inexistente, When arranca la preparación, Then se crea y se transforma **sola**; ante un esquema que no corresponde o una ubicación no disponible, **el arranque se detiene** y el almacén **no se recrea** | `TC-06032`, `TC-06033` | US-06024, US-06025 | `Pendiente` |

**Diez de diez casos de uso con al menos un caso de prueba, y veinticinco de veinticinco historias cubiertas.** Ninguno queda huérfano.

## 3. Trazabilidad NFR ↔ tests

### 3.1 `GeometriaFactory-Api`

Diecisiete filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Estado |
| --- | --- | --- | --- |
| Latencia del listado | **Percentil 99 por debajo de 500 ms**, medido **en el servidor** **[ASUNCIÓN]** | `TC-00034`. Gate `QG-00014`, condicionado | `Pendiente` |
| Caudal sostenido | **20 peticiones por minuto** **[ASUNCIÓN]** | `TC-00034`. Gate `QG-00014`, condicionado | `Pendiente` |
| Arranque en frío | Menos de **30 segundos** **[ASUNCIÓN]** | `TC-00033`. Gate `QG-00013`, condicionado | `Pendiente` |
| Cobertura del proyecto de código | **75 %** de líneas y **70 %** de ramas **[ASUNCIÓN]** | Informe del pipeline, **no un caso de verificación**. Gate `QG-00003`, condicionado | `Pendiente` |
| Forma de la pirámide de pruebas | **60 %** integración y **40 %** unitarias **[ASUNCIÓN en cuanto al reparto]** | `TC-00037`. Gate `QG-00004`, condicionado. **La inversión no es asunción** | `Pendiente` |
| Puntos de acceso fuera de la guardia | Exactamente **4** sobre **15**, **ni uno más** | `TC-00007`, inspección en las dos direcciones | `Pendiente` |
| Puntos que fijan una contraseña sobre una cuenta existente sin credencial | Exactamente **0** | `TC-00010`, inspección de los cuatro puntos que no exigen acceso | `Pendiente` |
| Códigos del contrato con traducción declarada | **16 de 17**, con **1** sin destino y su motivo; **0** inventados y **0** renombrados | `TC-00024` y `TC-00027`, en las dos direcciones | `Pendiente` |
| Respuestas indistinguibles de las tres familias empobrecidas | **3 de 3** comparaciones idénticas, cuerpo y código | `TC-00025` | `Pendiente` |
| Respuestas que exponen dirección, ruta, secreto o traza | Exactamente **0**, sobre los quince puntos y sobre el registro del servidor | `TC-00026` | `Pendiente` |
| Configuraciones de intercambio declaradas en el producto | Exactamente **1**, compartida por los dos extremos | `TC-00029` | `Pendiente` |
| Textos originales alterados en el borde | **0** caracteres de diferencia y **0** truncamientos silenciosos | `TC-00019` | `Pendiente` |
| Puertos conectados a su adaptador | **4 de 4**, con **0** sin adaptador o con más de uno | `TC-00028`, con fallo en construcción | `Pendiente` |
| Peticiones atendidas con la preparación del almacén incompleta | Exactamente **0** | `TC-00031` | `Pendiente` |
| Eliminaciones fuera de alcance aceptadas al forzar la petición | Exactamente **0** | `TC-00020`. Gate `QG-00012` | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-00001`, **no un caso de verificación** | `Pendiente` |
| Pasos de la colección de peticiones reproducible | **5 o menos**, con **0** datos de prueba inventados | `TC-00035`. Gate `QG-00015` | `Pendiente` |

**Los valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y la forma de la pirámide, `A-5` para el percentil, el caudal y el arranque en frío—.

**Dos de los diecisiete NFR no tienen caso de verificación propio y es correcto**: uno es el informe de cobertura del pipeline y el otro la puerta de construcción.

**No hay NFR de disponibilidad y esta matriz no le inventa fila.** El intake declara «sin SLO»: la caída del servidor domiciliario se responde con **estado degradado en el front**.

### 3.2 `GeometriaFactory-Domain`

Seis filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo de la batería de pruebas del dominio | Menos de **10 segundos** de punta a punta **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain]** | Medición del pipeline, **no un caso de prueba**. Gate `QG-02007`, condicionado | Duración total reportada por el ejecutor en la etapa `test` | `Pendiente` |
| Cobertura de la biblioteca | **90 %** de líneas y **85 %** de ramas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-02003`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Dependencias salientes del proyecto de código | Exactamente **0** y **0** | `TC-02024` | Inspección del archivo de proyecto, y revisión del pull request | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **42** condiciones alcanzadas, y **0** emitidas fuera del catálogo | `TC-02023` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Ejercicio de los invariantes | **100 %** de los **nueve** con prueba de violación rechazada, **sin dobles** | `TC-02026`, sobre la tabla de §5 | Prueba de inspección sobre la matriz, revisada al cerrar cada etapa | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-02001`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los dos valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y `A-5` para el tiempo— y su conversión en trabajo es `BT-02015`. Hasta entonces sus gates son **condicionados** ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1): se miden y se registran, y no bloquean la fusión.

**Tres de los seis NFR no tienen caso de prueba y es correcto que no lo tengan**: son mediciones del pipeline, no comportamientos de la biblioteca. Inventarles un `TC-XX` habría producido un caso de prueba sin aserción sobre el sistema.

### 3.3 `GeometriaFactory-Application`

Nueve filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo del caso de uso más pesado | Menos de **500 ms** para el envío que interpreta el texto de **3** piezas de `E-1`, medido **sin acceso a base** **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application]** | Medición del pipeline por `BT-04019`, **no un caso de prueba de comportamiento**. Gate `QG-04010`, condicionado | Cronometrado dentro de la batería unitaria, con doble del puerto de validación | `Pendiente` |
| Cobertura de la biblioteca | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Application]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-04003`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Pruebas de esta capa que tocan la base de datos real | Exactamente **0** | `TC-04026` | Prueba de inspección del proyecto de pruebas, y revisión del pull request | `Pendiente` |
| Dependencias salientes del proyecto de código | Exactamente **1** al producto y **0** a persistencia, transporte, serialización o marco web | `TC-04027` | Inspección del archivo de proyecto | `Pendiente` |
| Componentes de pieza en las consultas de listado | Exactamente **0** cargados, en los dos listados | `TC-04030` | Inspección de la proyección devuelta por la consulta | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **36** alcanzadas, y **0** emitidas fuera del catálogo | `TC-04028` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Ejercicio de las cuatro comprobaciones | **4 de 4** con prueba de su negativa **sin base de datos**, y **1** sola prueba de que la cuarta corta antes que las otras tres | `TC-04011`, sobre la tabla de §5 | Prueba de orden y matriz de §5, revisada al cerrar cada etapa | `Pendiente` |
| Unidades de trabajo por caso de uso | **A lo sumo 1**, y **0** casos de uso que repartan su efecto | `TC-04029` | Dobles instrumentados que cuentan aperturas, con la baja como caso testigo | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-04001`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los dos valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y `A-5` para los 500 ms— y su conversión en trabajo es `BT-04018`. Hasta entonces sus gates son **condicionados** ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1): se miden y se registran, y no bloquean la fusión.

**Tres de los nueve NFR no tienen caso de prueba y es correcto que no lo tengan**: son mediciones del pipeline, no comportamientos de la biblioteca. Inventarles un `TC-XX` habría producido un caso de prueba sin aserción sobre el sistema.

### 3.4 `GeometriaFactory-Infrastructure`

Catorce filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo de interpretación del texto semilla | Menos de **200 ms** para el texto de **3** piezas de `E-1`, **sin almacén** **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure]** | `TC-06015`. Gate `QG-06014`, condicionado | Cronometrado dentro de la batería unitaria, sin abrir el almacén | `Pendiente` |
| Cobertura del proyecto de código | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-06005`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Cobertura del validador de figuras | **95 %** de líneas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure]**. Es el número más alto del producto | Informe del pipeline acotado a los **dos motores**, **no un caso de prueba**. Gate `QG-06006`, condicionado | Recolector de cobertura con alcance acotado | `Pendiente` |
| Tolerancia de comparación de valores | **0.01** absoluta con operador **estricto**. **No es asunción**: sale de que el emisor redondea a 2 decimales | `TC-06009`, que debe dar **exactamente 2** advertencias y no 3 | Caso de prueba del escenario `E-1` | `Pendiente` |
| Casos de la batería del validador que pasan | **10 de 10**, con los **ocho** escenarios como entrada | `TC-06001` a `TC-06010`, contra la tabla de §6 | Etapa `test` del pipeline. Gate `QG-06003` | `Pendiente` |
| Peticiones de red originadas por los dos motores | Exactamente **0** | `TC-06014` | Inspección de dependencias de los dos motores | `Pendiente` |
| Aplicación de transformaciones sobre almacén inexistente | **1 de 1** intento exitoso, sin paso manual | `TC-06032` | Etapa de verificación de transformaciones del pipeline. Gate `QG-06004` | `Pendiente` |
| Provisorias iguales en dos producciones consecutivas | Exactamente **0**, sobre la misma cuenta y entre cuentas distintas | `TC-06027` | Prueba que produce dos provisorias y compara, y prueba de no derivabilidad | `Pendiente` |
| Componentes de pieza y texto original en una consulta de listado | Exactamente **0** y **0** | `TC-06019` | Inspección de la proyección devuelta | `Pendiente` |
| Escrituras que reemplazan el texto original conservado | Exactamente **0** aceptadas | `TC-06016` | Prueba que materializa un trabajo existente con un texto distinto | `Pendiente` |
| Retiros parciales tras una baja interrumpida | Exactamente **0** | `TC-06021` | Prueba de baja **con el almacén interrumpido a mitad de operación** | `Pendiente` |
| Mensajes y trazas con un secreto, la ruta del almacén o el texto del alumno | Exactamente **0** | `TC-06035` | Prueba de inspección sobre las 17 condiciones **y sobre el registro del servidor**, en las dos direcciones | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **17** alcanzadas, y **0** emitidas fuera del catálogo | `TC-06034` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-06001`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los tres valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para las dos coberturas y `A-5` para los 200 ms— y `PA-11` de `05` §11 los registra. Hasta entonces sus gates son **condicionados**.

**La tolerancia de 0.01 no lleva rótulo y no es condicionada.** El intake §22 la enumera expresamente entre «lo que NO es asunción». Arrastrarla al tratamiento condicionado sería un error de lectura.

**Tres de los catorce NFR no tienen caso de prueba y es correcto que no lo tengan**: dos son informes de cobertura del pipeline y el tercero es la puerta de construcción.

## 4. Trazabilidad RN ↔ tests

### 4.1 `GeometriaFactory-Api`

Dieciséis filas, una por regla. El tramo de cada una es el que `05` §10.2 le asigna **en esta capa**; esta matriz lo refleja y no lo redefine.

| RN | Tramo en esta capa | Tests | Estado |
| --- | --- | --- | --- |
| RN-00001 Administrador único y papeles fijos | El punto de configuración con su negativa cuando ya existe una, y el papel exigido por punto | `TC-00005`, `TC-00009`, `TC-00012` | `Pendiente` |
| RN-00002 El correo del alumno es único | La traducción del correo ocupado a una respuesta que **no declara la situación ni el papel** de la cuenta que lo ocupa | `TC-00008`, `TC-00025` | `Pendiente` |
| **RN-00003 Trabajo ajeno indistinguible de inexistente** | **Tramo de traducción, y es el que esta capa puede romper sola.** Los tres casos reciben **el mismo código y el mismo cuerpo** | `TC-00020`, `TC-00022`, `TC-00025` | `Pendiente` |
| RN-00004 Eliminación acotada al borrador | Los dos alcances sobre el mismo punto. **Es la única regla con un criterio de verificación que exige forzar la petición contra esta superficie** | `TC-00020` | `Pendiente` |
| RN-00005 No se pasa a estado `Pendiente` con errores de validación | **Sin tramo acá.** El estado llega decidido y viaja en una respuesta **exitosa**. Lo que se verifica es que esta capa **no lo convierta en un fallo** | `TC-00017` | `Pendiente` |
| RN-00006 Cuenta `Pendiente` o `Bloqueado` sin acceso | La respuesta **con motivo** del canje, distinta de la genérica de credenciales inválidas | `TC-00003`, `TC-00012` | `Pendiente` |
| RN-00007 Baja con arrastre y confirmación escrita | El punto **transporta** el correo escrito y no procede sin él. **La comparación y el arrastre son de adentro** | `TC-00013` | `Pendiente` |
| RN-00008 Texto original conservado íntegro | **El borde es el primer lugar donde el texto puede alterarse**: no se normaliza, no se recodifica y el cuerpo que excede el límite **se rechaza, nunca se trunca** | `TC-00018`, `TC-00019` | `Pendiente` |
| RN-00009 Observación de error con posición y campo | La ubicación **cruza la frontera sin recortarse**. Producirla es de adentro; **no perderla al traducir es de acá** | `TC-00017`, `TC-00022` | `Pendiente` |
| RN-00010 Desenlace exclusivo del administrador y terminalidad | El papel exigido en el punto y la traducción del estado que no admite desenlace, **incluido el terminal** | `TC-00023` | `Pendiente` |
| RN-00011 El administrador no ve los borradores | **De forma negativa**: la superficie **no declara ningún parámetro** con el que pedir borradores | `TC-00020`, `TC-00021` | `Pendiente` |
| RN-00012 El reseteo conserva la cuenta y sus trabajos | El reseteo y la baja son **dos puntos distintos con verbos distintos**, y el del reseteo **no toca ninguna ruta de retiro** | `TC-00014` | `Pendiente` |
| **RN-00013 Cambio forzado antes de toda otra capacidad** | **Tramo transversal, y es el otro que esta capa puede romper sola.** Un punto nuevo fuera de la guardia la rompe **sin que nada falle** | `TC-00006`, `TC-00007`, `TC-00010` | `Pendiente` |
| RN-00014 La provisoria la produce el sistema | **Sin tramo acá.** Lo que esta capa declara es **lo que no hace con el valor**: no lo registra en ninguna traza y lo devuelve **una sola vez** | `TC-00014`, `TC-00016` | `Pendiente` |
| RN-00015 Resetear no exige cuenta habilitada | **De forma estructural**: el punto **no declara ningún parámetro de situación** y su tabla de respuestas no tiene ninguna fila por ese concepto | `TC-00015` | `Pendiente` |
| RN-00016 Habilitar produce la provisoria | **Sin tramo propio acá**, con dos efectos estructurales: un identificador de punto **retirado y no reciclado**, y el punto de situación devolviendo la provisoria. Lo que esta capa aporta es **no exponer ningún punto que la contradiga** | `TC-00010`, `TC-00012` | `Pendiente` |

**Trece de las dieciséis con tramo acá y tres sin él**, que es exactamente el reparto que `05` §10.2 declara. **Las tres sin tramo tienen caso de verificación igual**, y lo que verifican es una afirmación distinta: que esta capa **no deshaga** lo que otra decidió.

**Dos reglas están señaladas como las que esta capa puede romper sola** —`RN-00003` y `RN-00013`—, y son las que concentran los dos primeros riesgos de `05` §9. Sus casos son los que [`Plan-Pruebas.md`](Plan-Pruebas.md) §4 trata con la prioridad más alta.

### 4.2 `GeometriaFactory-Domain`

Dieciséis filas, una por regla de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4. Ninguna se agrupa. La columna de invariante es la que esa sección le asigna a cada regla.

| RN | Invariante | Tests que la verifican | Tipo | Estado |
| --- | --- | --- | --- | --- |
| RN-02001 Administrador único y papeles fijos | INV-05 | `TC-02002`, `TC-02005`, `TC-02006`, `TC-02021`, `TC-02022` | Unit | `Pendiente` |
| RN-02002 El correo del alumno es único | INV-01 | `TC-02001`, `TC-02002`, `TC-02006` | Unit | `Pendiente` |
| RN-02003 Trabajo ajeno indistinguible de inexistente | INV-02 | `TC-02011`, `TC-02020` | Unit | `Pendiente` |
| RN-02004 Eliminación acotada al borrador para el alumno | INV-03 | `TC-02004`, `TC-02012`, `TC-02020`, `TC-02021` | Unit e integración interna | `Pendiente` |
| RN-02005 No se pasa a estado `Pendiente` con errores de validación | INV-04 | `TC-02017`, `TC-02018` | Integración interna | `Pendiente` |
| RN-02006 Cuenta `Pendiente` o `Bloqueado` sin acceso | INV-06 | `TC-02003`, `TC-02008`, `TC-02010` | Unit | `Pendiente` |
| RN-02007 Baja con arrastre y confirmación escrita | Ninguno | `TC-02004` | Integración interna | `Pendiente` |
| RN-02008 Texto original conservado íntegro | Ninguno | `TC-02011`, `TC-02018` | Unit e integración interna | `Pendiente` |
| RN-02009 Observación de error con posición y campo | Ninguno | `TC-02013`, `TC-02014`, `TC-02016` | Unit e integración interna | `Pendiente` |
| RN-02010 Desenlace exclusivo del administrador y terminalidad | INV-07 | `TC-02019`, `TC-02022` | Unit | `Pendiente` |
| RN-02011 El administrador no ve los borradores | Ninguno | `TC-02021`, `TC-02022` | Unit | `Pendiente` |
| RN-02012 El reseteo conserva la cuenta y sus trabajos | INV-09 | `TC-02007`, `TC-02009` | Integración interna | `Pendiente` |
| RN-02013 Cambio forzado antes de toda otra capacidad | INV-09 | `TC-02009`, `TC-02010` | Unit e integración interna | `Pendiente` |
| RN-02014 La provisoria la produce el sistema | Ninguno | `TC-02003`, `TC-02007` | Unit e integración interna | `Pendiente` |
| RN-02015 Resetear no exige cuenta habilitada | Ninguno | `TC-02005`, `TC-02007` | Unit e integración interna | `Pendiente` |
| RN-02016 Habilitar produce la provisoria | INV-09 | `TC-02003`, `TC-02009`, `TC-02010` | Unit e integración interna | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de prueba.** El reparto de la columna de invariante es **diez con invariante y seis sin él**, que es exactamente el que declaran `02` §4 y `05` §10.2; esta matriz lo refleja y no lo redefine. **RN-02012, RN-02013 y RN-02016 comparten INV-09**, con la lectura que la categoría 02 adoptó de la columna del propio invariante en el intake §17.1.P.2 · GeometriaFactory-Domain, declarando que la prosa de esa sección es ambigua; esta categoría hereda esa lectura y **no afirma que la prosa del intake la respalde**.

**RN-02014 tiene caso de prueba aunque `05` §10.2 declare que ningún componente de este proyecto de código la gobierna.** Lo que se verifica acá no es la producción de la provisoria —que ocurre afuera— sino su consecuencia sobre la superficie: que la habilitación y el reseteo **exigen el valor ya derivado** y lo rechazan vacío.

### 4.3 `GeometriaFactory-Application`

Dieciséis filas, una por regla. La columna de tramo es la que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 y [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §10.2 le asignan a cada una **en esta capa**; esta matriz la refleja y no la redefine. Las reglas se enuncian en `GeometriaFactory-Domain` y acá se referencian.

| RN | Tramo en esta capa | Tests que lo verifican | Estado |
| --- | --- | --- | --- |
| RN-04001 Administrador único y papeles fijos | Ventana de alta y su negativa; rechazo del papel `Administrador` en el auto-registro; verificación de facultad; acotamiento del reseteo a cuentas de alumno | `TC-04002`, `TC-04003`, `TC-04004`, `TC-04005`, `TC-04007`, `TC-04012`, `TC-04021`, `TC-04024` | `Pendiente` |
| RN-04002 El correo del alumno es único | La verificación sobre el conjunto de cuentas, en los dos caminos de alta | `TC-04001`, `TC-04002`, `TC-04003` | `Pendiente` |
| RN-04003 Trabajo ajeno indistinguible de inexistente | La verificación de pertenencia, con un solo motivo para los dos casos | `TC-04012`, `TC-04013`, `TC-04014`, `TC-04020`, `TC-04025` | `Pendiente` |
| RN-04004 Eliminación acotada al borrador para el alumno | Los dos alcances opuestos de la eliminación, y el arrastre de la baja | `TC-04005`, `TC-04014`, `TC-04025` | `Pendiente` |
| RN-04005 No se pasa a estado `Pendiente` con errores de validación | Entregar el conjunto de observaciones con su especie, **con el tramo principal en el dominio** | `TC-04015`, `TC-04016`, `TC-04017` | `Pendiente` |
| RN-04006 Cuenta `Pendiente` o `Bloqueado` sin acceso | La consulta de admisibilidad con su motivo, y los estados iniciales opuestos de los dos caminos de alta | `TC-04003`, `TC-04004`, `TC-04008`, `TC-04009` | `Pendiente` |
| RN-04007 Baja con arrastre y confirmación escrita | La comparación del correo escrito y el retiro de todos los trabajos **en la misma unidad de trabajo** | `TC-04005`, `TC-04029` | `Pendiente` |
| RN-04008 Texto original conservado íntegro | El texto se entrega tal cual y no se reescribe **ni cuando la interpretación falla** | `TC-04013`, `TC-04014`, `TC-04015`, `TC-04016`, `TC-04019` | `Pendiente` |
| RN-04009 Observación de error con posición y campo | La cantidad de figuras del conjunto raíz como rango de validación, y el rechazo del conjunto mal formado, **con el tramo principal en el validador** | `TC-04015`, `TC-04016`, `TC-04018` | `Pendiente` |
| RN-04010 Desenlace exclusivo del administrador y terminalidad | La verificación de facultad y la propagación de la terminalidad | `TC-04023`, `TC-04024` | `Pendiente` |
| RN-04011 El administrador no ve los borradores | El predicado de alcance **trasladado a la consulta** | `TC-04021`, `TC-04022`, `TC-04024`, `TC-04025` | `Pendiente` |
| RN-04012 El reseteo conserva la cuenta y sus trabajos | La postcondición que deja intactos estado, papel, identidad y todos los trabajos, y la **ausencia deliberada** de todo retiro | `TC-04006`, `TC-04010` | `Pendiente` |
| RN-04013 Cambio forzado antes de toda otra capacidad | La **cuarta** comprobación transversal en los once casos de uso, y el único lugar donde la marca se levanta | `TC-04006`, `TC-04008`, `TC-04009`, `TC-04010`, `TC-04011` | `Pendiente` |
| RN-04014 La provisoria la produce el sistema | **Ninguno: es la única de las dieciséis sin tramo en esta capa** (`02` §6, `05` §10.2). Lo que se verifica acá es su consecuencia sobre la superficie —que el valor llega **ya producido y ya derivado** y que la operación lo rechaza vacío—, no su producción | `TC-04004`, `TC-04006` | `Pendiente` |
| RN-04015 Resetear no exige cuenta habilitada | De forma **negativa**: no se comprueba el estado de la cuenta y no se devuelve ningún motivo por ese concepto | `TC-04006`, `TC-04007` | `Pendiente` |
| RN-04016 Habilitar produce la provisoria | Habilitar y rehabilitar piden el valor al puerto, lo derivan afuera y fijan la credencial provisoria, dejando la marca puesta | `TC-04004`, `TC-04010` | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de prueba, y quince con tramo en esta capa.** La excepción es **RN-04014**, y su fila lo declara: `05` §10.2 le asigna «ninguno de este proyecto de código». Tiene caso de prueba igual porque lo que se verifica acá **no es la producción de la provisoria** —que ocurre en `GeometriaFactory-Infrastructure`— sino su consecuencia sobre la superficie de esta capa. Es el mismo tratamiento que `GeometriaFactory-Domain` le dio en su matriz.

**RN-04012, RN-04013 y RN-04016 comparten INV-09**, con la lectura que la categoría 02 de `GeometriaFactory-Domain` adoptó y que `05` §10.2 hereda declarando que **no afirma que la prosa del intake la respalde**. Esta matriz hereda esa lectura y tampoco lo afirma.

### 4.4 `GeometriaFactory-Infrastructure`

Dieciséis filas, una por regla. El tramo de cada una es el que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 y `05` §10.2 le asignan **en esta capa**; esta matriz lo refleja y no lo redefine. Las reglas se enuncian en `GeometriaFactory-Domain`.

| RN | Tramo en esta capa | Tests | Estado |
| --- | --- | --- | --- |
| RN-06001 Administrador único y papeles fijos | La restricción de unicidad del almacén, y el transporte del papel en el acceso **sin decidir qué habilita** | `TC-06022`, `TC-06023`, `TC-06029` | `Pendiente` |
| RN-06002 El correo del alumno es único | La **segunda línea** de la unicidad, con el motivo que la capa de aplicación ya declara recibir por esta vía | `TC-06022`, `TC-06023` | `Pendiente` |
| RN-06003 Trabajo ajeno indistinguible de inexistente | **De forma negativa**: la consulta sin recorte declarado **no se resuelve**. Esta capa no comprueba pertenencia | `TC-06018` | `Pendiente` |
| RN-06004 Eliminación acotada al borrador | **La mitad de borrado físico**. La acotación por estado y por papel es de la capa de aplicación | `TC-06020` | `Pendiente` |
| RN-06005 No se pasa a estado `Pendiente` con errores de validación | **Produce el insumo**: la especie de cada observación. **El estado lo resuelve el dominio** | `TC-06008`, `TC-06009`, `TC-06010` | `Pendiente` |
| RN-06006 Cuenta `Pendiente` o `Bloqueado` sin acceso | **Sin tramo acá.** La admisibilidad se resuelve antes y una cuenta no admitida **no llega** a la emisión. Lo que sí se verifica es que la capa **guarda el estado como dato y no lo comprueba** | `TC-06024` | `Pendiente` |
| RN-06007 Baja con arrastre y confirmación escrita | **La mitad de arrastre**, con el todo o nada. La confirmación escrita es de la capa de aplicación | `TC-06021` | `Pendiente` |
| RN-06008 Texto original conservado íntegro | **Tramo principal acá**: el motor no lo devuelve corregido y el adaptador **rechaza toda escritura que lo reemplace** | `TC-06016`, `TC-06005` | `Pendiente` |
| RN-06009 Observación de error con posición y campo | **Tramo principal acá**: el mensaje ubicado, la posición reservada de la figura no reconstruida y la advertencia con sus dos valores | `TC-06005`, `TC-06008`, `TC-06010`, `TC-06012` | `Pendiente` |
| RN-06010 Desenlace exclusivo del administrador y terminalidad | **Sin tramo acá.** Esta capa guarda el estado y el comentario; quién puede cambiarlo lo deciden el dominio y la capa de aplicación. Lo que sí se verifica es que el comentario se materializa **como campo y sin historial** | `TC-06017` | `Pendiente` |
| RN-06011 El administrador no ve los borradores | **De forma negativa**, igual que `RN-06003`: el predicado llega en el pedido y el borrador **no viaja** | `TC-06018` | `Pendiente` |
| RN-06012 El reseteo conserva la cuenta y sus trabajos | Escribe la marca **sin tocar el estado ni los trabajos**, y **por contraste**: el reseteo **no pasa por el retiro** | `TC-06024`, `TC-06020` | `Pendiente` |
| RN-06013 Cambio forzado antes de toda otra capacidad | **Conserva la marca y la hace viajar.** Sin ese dato, la comprobación transversal de la capa de aplicación no tendría sobre qué decidir. **La comprobación no es de acá** | `TC-06024` | `Pendiente` |
| **RN-06014 La provisoria la produce el sistema** | **Tramo principal, y único, acá.** `GeometriaFactory-Application` declara que es la única de las dieciséis **sin tramo en su capa**, y `RN-06014` nombra a este proyecto de código como el lugar de la generación | `TC-06027`, `TC-06028` | `Pendiente` |
| RN-06015 Resetear no exige cuenta habilitada | **De forma estructural**: la invocación **no recibe** el estado de la cuenta, de modo que **no puede comprobarlo**; y la marca se escribe sobre los tres estados sin alterarlos | `TC-06024`, `TC-06027` | `Pendiente` |
| RN-06016 Habilitar produce la provisoria | **Produce el valor también para la habilitación**: mismo mecanismo y mismo valor que para el reseteo, y la invocación **no lleva ningún dato del acto que la motiva**, de modo que no puede distinguirlos | `TC-06024`, `TC-06027` | `Pendiente` |

**Catorce de las dieciséis con tramo acá y dos sin él, y el recuento cierra en dieciséis**, que es exactamente el reparto que `02` §6 declara. **Las dos sin tramo —`RN-06006` y `RN-06010`— tienen caso de prueba igual**, y lo que verifican es una afirmación distinta: que esta capa **guarda el dato y no lo comprueba**. Es la misma clase de verificación que `GeometriaFactory-Application` le dio a `RN-06014` desde el otro lado.

**Tres reglas tienen su tramo principal acá —`RN-06008`, `RN-06009` y `RN-06014`—**, y `02` §6 declara la consecuencia: si acá se hacen mal, **ninguna capa de más adentro puede repararlas**. Sus casos de prueba son los que `Plan-Pruebas.md` §4 trata como riesgo de impacto más alto.

## 5. Trazabilidad punto de acceso ↔ tests

### 5.1 `GeometriaFactory-Api`

Quince filas, una por punto de `05` §3.4. **Es la tabla que hace verificable el 100 % de puntos cubiertos que `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api`.**

| Punto | Intención, en una línea | ¿Bajo la guardia? | Tests | Estado |
| --- | --- | --- | --- | --- |
| A-01 | Canjear correo y contraseña por un acceso firmado | **No** | `TC-00001`, `TC-00002`, `TC-00003`, `TC-00007` | `Pendiente` |
| A-02 | Registrar una cuenta de alumno, sin campo de contraseña | **No** | `TC-00008`, `TC-00007`, `TC-00025` | `Pendiente` |
| A-03 | Configurar la cuenta de administrador, sólo mientras no exista ninguna | **No** | `TC-00009`, `TC-00007` | `Pendiente` |
| A-05 | Cambiar la contraseña propia exigiendo la vigente | **Sí**, y es la **única excepción** de la guardia del cambio pendiente | `TC-00006`, `TC-00010` | `Pendiente` |
| A-06 | Listar las cuentas de la comisión con su situación y su marca | **Sí** | `TC-00011`, `TC-00006` | `Pendiente` |
| A-07 | Cambiar la situación de una cuenta | **Sí** | `TC-00012`, `TC-00006` | `Pendiente` |
| A-08 | Dar de baja una cuenta con el correo escrito | **Sí** | `TC-00013`, `TC-00006` | `Pendiente` |
| A-09 | Resetear la contraseña de un alumno | **Sí** | `TC-00014`, `TC-00015`, `TC-00016`, `TC-00006` | `Pendiente` |
| A-10 | Enviar un trabajo nuevo | **Sí** | `TC-00017`, `TC-00019`, `TC-00006` | `Pendiente` |
| A-11 | Reenviar un trabajo en `Borrador` | **Sí** | `TC-00018`, `TC-00006` | `Pendiente` |
| A-12 | Eliminar un trabajo, con los dos alcances | **Sí** | `TC-00020`, `TC-00006` | `Pendiente` |
| A-13 | Listar trabajos con el alcance que el papel determina | **Sí** | `TC-00021`, `TC-00006` | `Pendiente` |
| A-14 | Obtener el detalle de un trabajo interpretado | **Sí** | `TC-00022`, `TC-00006` | `Pendiente` |
| A-15 | Aprobar o rechazar un trabajo en estado `Pendiente` | **Sí** | `TC-00023`, `TC-00006` | `Pendiente` |
| A-16 | Responder por el estado del servicio | **No** | `TC-00032`, `TC-00007` | `Pendiente` |

**Quince de quince puntos con caso de verificación: cuatro fuera de la guardia y once bajo ella. Cuatro más once son quince.** El identificador retirado **no se recicla y no tiene fila**, porque no existe: la operación que exponía dejó de existir con `RN-00016`.

**`TC-00006` aparece en los once puntos bajo la guardia**, y no es redundancia: es la prueba que recorre los diez rechazos y la única excepción. **`TC-00007` aparece en los cuatro exentos y recorre los quince** en las dos direcciones.

## 6. Trazabilidad invariante ↔ tests

### 6.1 `GeometriaFactory-Api`

Nueve filas. La columna de aporte es la de `05` §10.3: declara **qué hace esta capa por cada uno**, que es una cosa distinta de enunciarlo.

| Invariante | Qué aporta esta capa | Tests | Estado |
| --- | --- | --- | --- |
| INV-01 Correo único | Traducir la colisión a una respuesta que **no revela nada** de la cuenta que ocupa el correo | `TC-00008`, `TC-00025` | `Pendiente` |
| INV-02 Acceso sólo a los trabajos propios | **El aporte más delicado**: la comprobación es de adentro, pero **la propiedad observable se decide acá** | `TC-00020`, `TC-00022`, `TC-00025` | `Pendiente` |
| INV-03 Eliminación por el alumno sólo en `Borrador` y sobre trabajo propio | Lo mismo, más el criterio que la fuente exige ejercer **forzando la petición** | `TC-00020` | `Pendiente` |
| INV-04 Trabajo `Finalizado` sin errores de interpretación | **Nada propio, y es correcto**: lo que esta capa hace es **no convertir el estado en un fallo** | `TC-00017` | `Pendiente` |
| INV-05 Exactamente un administrador | Exponer el punto de configuración **con su ventana** y traducir la negativa a conflicto de estado | `TC-00009` | `Pendiente` |
| INV-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Responder **con motivo** en el canje, distinto de la respuesta genérica | `TC-00003` | `Pendiente` |
| INV-07 Estado terminal sin salida ni cambio de contenido | Traducir el estado que no admite desenlace **incluido el terminal**, y **no sugerir ninguna forma de revertirlo** | `TC-00023` | `Pendiente` |
| INV-08 La cuenta de administrador está siempre `Habilitado` | **Nada propio, y es correcto**: no hay punto de acceso que pueda cambiar su situación ni darla de baja | `TC-00007`, `TC-00011` | `Pendiente` |
| INV-09 Cuenta con la marca puesta sin ninguna otra capacidad | **El aporte más consecuente**: garantizar que **ningún punto quede fuera de la guardia**, que es la parte que se rompe agregando un punto y olvidándose | `TC-00006`, `TC-00007` | `Pendiente` |

**Nueve de nueve con caso de verificación.** Las dos filas que declaran «nada propio» tienen prueba igual, y lo que verifican es la **ausencia de una puerta**: que ningún punto de acceso permita lo que el invariante prohíbe.

### 6.2 `GeometriaFactory-Domain`

Nueve filas, una por invariante de [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4. Es la tabla que `TC-02026` recorre.

| Invariante | Componente que lo sostiene (`05` §10.3) | Test de violación rechazada | Usa dobles | Estado |
| --- | --- | --- | --- | --- |
| INV-01 El correo del alumno es único | Núcleo de entidades | `TC-02002` (alta sin la unicidad declarada), `TC-02006` | No | `Pendiente` |
| INV-02 Un alumno sólo accede a sus propios trabajos | Máquina de estados del trabajo | `TC-02020` (trabajo ajeno), `TC-02011` | No | `Pendiente` |
| INV-03 El alumno elimina sólo en `Borrador` y sobre trabajo propio | Máquina de estados del trabajo | `TC-02020`, `TC-02012` | No | `Pendiente` |
| INV-04 Un trabajo `Finalizado` no tiene errores de interpretación | Máquina de estados del trabajo | `TC-02018` (envío con error no transiciona) | No | `Pendiente` |
| INV-05 Existe exactamente un administrador | Guardas de cuenta | `TC-02006` (segunda configuración rechazada) | No | `Pendiente` |
| INV-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Evaluador de admisibilidad | `TC-02010`, `TC-02008` | No | `Pendiente` |
| INV-07 Un estado terminal no cambia de estado ni de contenido | Máquina de estados del trabajo | `TC-02019` (cuatro operaciones sobre los dos estados terminales) | No | `Pendiente` |
| INV-08 La cuenta de administrador está siempre `Habilitado` | Guardas de cuenta | `TC-02005` (las cinco operaciones rechazadas), `TC-02002`, `TC-02006` | No | `Pendiente` |
| INV-09 Con la marca puesta la cuenta no ejerce ninguna otra capacidad | Evaluador de admisibilidad | `TC-02010`, `TC-02009` (la marca se levanta sólo con el cambio efectivo) | No | `Pendiente` |

**Nueve de nueve con prueba de violación rechazada y cero dobles.** Es la mitigación declarada del segundo riesgo de `05` §9 —que un invariante se ejerza en un componente y no en otro— y del NFR de §3.

### 6.3 `GeometriaFactory-Application`

Nueve filas, una por invariante. La columna de aporte es la de `05` §10.3, que declara **qué hace esta capa por cada uno**; los invariantes se enuncian en `GeometriaFactory-Domain`.

| Invariante | Qué aporta esta capa (`05` §10.3) | Test que lo verifica acá | Estado |
| --- | --- | --- | --- |
| INV-01 El correo del alumno es único | La verificación sobre el conjunto, por el puerto de repositorio de cuentas | `TC-04001`, `TC-04002`, `TC-04003` | `Pendiente` |
| INV-02 Un alumno sólo accede a sus propios trabajos | La verificación de pertenencia sobre el dato recuperado, antes de escribir | `TC-04011`, `TC-04012`, `TC-04013`, `TC-04020`, `TC-04022`, `TC-04025` | `Pendiente` |
| INV-03 El alumno elimina sólo en `Borrador` y sobre trabajo propio | La misma pertenencia, más el traslado del alcance a la consulta | `TC-04005`, `TC-04011`, `TC-04014`, `TC-04025` | `Pendiente` |
| INV-04 Un trabajo `Finalizado` no tiene errores de interpretación | Entregar al dominio el conjunto completo de observaciones con su especie. **No decide el estado** | `TC-04015`, `TC-04016`, `TC-04017` | `Pendiente` |
| INV-05 Existe exactamente un administrador | Resolver por el puerto si ya existe una cuenta con papel `Administrador` | `TC-04002`, `TC-04003`, `TC-04007` | `Pendiente` |
| INV-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Invocar la admisibilidad y propagar sus motivos **sin colapsarlos** | `TC-04004`, `TC-04008`, `TC-04009` | `Pendiente` |
| INV-07 Un estado terminal no cambia de estado ni de contenido | Verificar la facultad **antes** de pedir la transición | `TC-04023`, `TC-04024` | `Pendiente` |
| INV-08 La cuenta de administrador está siempre `Habilitado` | **Nada propio, y es correcto**: no hay operación de esta capa que pueda violarlo. Lo protege por el costado el acotamiento del reseteo | `TC-04003`, `TC-04005`, `TC-04007` | `Pendiente` |
| INV-09 Con la marca puesta la cuenta no ejerce ninguna otra capacidad | **El aporte más consecuente de esta capa**: la cuarta comprobación, en orden fijo y en un único componente | `TC-04004`, `TC-04006`, `TC-04008`, `TC-04010`, `TC-04011` | `Pendiente` |

**Nueve de nueve con caso de prueba.** La fila de `INV-08` conserva su declaración de que esta capa no aporta nada propio y tiene pruebas igual: lo que verifican es que **ninguna operación de la capa lo viola**, que es una afirmación distinta y sí comprobable acá.

## 7. Cobertura por capa

### 7.1 `GeometriaFactory-Api`

La partición es por los **ocho** componentes de `05` §3.1. Los umbrales son los de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2.

| Componente | Líneas medidas | Ramas medidas | Mutation score medido | Umbral mínimo (líneas / ramas / mutación) |
| --- | --- | --- | --- | --- |
| Composición de raíz | Sin medir | Sin medir | **No aplica** | 75 / 70 / — |
| **Guardia de admisión** | Sin medir | Sin medir | Sin medir | **95 / 90** / 60 |
| **Traductor de motivos y códigos** | Sin medir | Sin medir | Sin medir | **95 / 90** / 60 |
| Superficie de acceso y credencial propia | Sin medir | Sin medir | Sin medir | 80 / 75 / 60 |
| Superficie de gobierno de la comisión | Sin medir | Sin medir | Sin medir | 75 / 70 / 60 |
| Superficie de trabajos | Sin medir | Sin medir | Sin medir | 80 / 75 / 60 |
| Superficie de desenlace | Sin medir | Sin medir | Sin medir | 75 / 70 / 60 |
| Arranque y salud | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| **Proyecto de código completo** | Sin medir | Sin medir | Sin medir | **75 / 70 / 60** |

**«Sin medir» y no «0 %».** No hay código construido: un cero sería una afirmación falsa sobre el estado del sistema.

**Además de la cobertura de líneas hay una cobertura contable que no admite promedio**, y es la que esta matriz reporta en sus §5 y §3: **15 de 15** puntos ejercidos, **17 de 17** códigos recorridos, **4** puntos fuera de la guardia, **3 de 3** familias indistinguibles y **4 de 4** puertos conectados.

**El umbral global de 75 / 70 viene rotulado [ASUNCIÓN] y es el piso más bajo del producto**, con el motivo declarado en la fuente: este proyecto de código es cableado. El **mutation score de 60 %** es el piso de `Rules-Calidad-Y-Pruebas.md` §2.2 y **no se le atribuye al intake**. La composición de raíz queda exenta con su fundamento: es declaración de cableado y su verificación real es el fallo en construcción de `TC-00028`.

### 7.2 `GeometriaFactory-Domain`

La partición es por los **cinco** componentes de `05` §3.1, no por capas de despliegue: este proyecto de código no tiene ninguna. Los umbrales son los de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2.

| Componente | Líneas medidas | Ramas medidas | Mutation score medido | Umbral mínimo (líneas / ramas / mutación) |
| --- | --- | --- | --- | --- |
| Núcleo de entidades | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| Guardas de cuenta | Sin medir | Sin medir | Sin medir | 95 / 90 / 60 |
| Evaluador de admisibilidad | Sin medir | Sin medir | Sin medir | 100 / 100 / 60 |
| Máquina de estados del trabajo | Sin medir | Sin medir | Sin medir | 95 / 90 / 60 |
| Adopción de la interpretación | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| **Proyecto de código completo** | Sin medir | Sin medir | Sin medir | **90 / 85 / 60** |

**«Sin medir» y no «0 %».** No hay código construido: un cero sería una afirmación falsa sobre el estado del sistema y no una ausencia de medición.

**El umbral global de 90 / 85 viene rotulado [ASUNCIÓN] desde el intake §17.1.P.6 · GeometriaFactory-Domain.** El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`, y esta categoría lo adopta como tal sin atribuírselo al intake.

### 7.3 `GeometriaFactory-Application`

La partición es por los **ocho** componentes de `05` §3.1, no por capas de despliegue: este proyecto de código no tiene ninguna. Los umbrales son los de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2.

| Componente | Líneas medidas | Ramas medidas | Mutation score medido | Umbral mínimo (líneas / ramas / mutación) |
| --- | --- | --- | --- | --- |
| Guarda de autorización | Sin medir | Sin medir | Sin medir | 100 / 100 / 60 |
| Declaración de puertos | **No aplica** | **No aplica** | **No aplica** | **Sin umbral**: no tiene líneas ejecutables propias |
| Orquestación del alta de cuentas | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| Orquestación del gobierno de cuentas | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| Orquestación del ingreso y la credencial | Sin medir | Sin medir | Sin medir | 95 / 90 / 60 |
| Orquestación del trabajo | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| Orquestación de la consulta | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| Orquestación del desenlace | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| **Proyecto de código completo** | Sin medir | Sin medir | Sin medir | **85 / 80 / 60** |

**«Sin medir» y no «0 %».** No hay código construido: un cero sería una afirmación falsa sobre el estado del sistema y no una ausencia de medición.

**El umbral global de 85 / 80 viene rotulado [ASUNCIÓN] desde el intake §17.1.P.6 · GeometriaFactory-Application.** El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`, y esta categoría lo adopta como tal sin atribuírselo al intake.

### 7.4 `GeometriaFactory-Infrastructure`

La partición es por los **ocho** componentes de `05` §3.1. Los umbrales son los de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2.

| Componente | Líneas medidas | Ramas medidas | Mutation score medido | Umbral mínimo (líneas / ramas / mutación) |
| --- | --- | --- | --- | --- |
| Contexto de persistencia y mapeo | Sin medir | Sin medir | Sin medir | 85 / 80 / 60 |
| Adaptador de repositorio de trabajos | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| Adaptador de repositorio de cuentas | Sin medir | Sin medir | Sin medir | 90 / 85 / 60 |
| **Motor de interpretación de figuras** | Sin medir | Sin medir | Sin medir | **95** / 90 / 60 |
| **Motor de verificación de valores** | Sin medir | Sin medir | Sin medir | **95** / 90 / 60 |
| Adaptador de reloj del sistema | Sin medir | Sin medir | **No aplica** | 100 / 100 / — |
| Mecanismo de credenciales | Sin medir | Sin medir | Sin medir | 95 / 90 / 60 |
| Mecanismo de acceso firmado y preparación del almacén | Sin medir | Sin medir | Sin medir | 95 / 90 / 60 |
| **Proyecto de código completo** | Sin medir | Sin medir | Sin medir | **85 / 80 / 60** |

**«Sin medir» y no «0 %».** No hay código construido: un cero sería una afirmación falsa sobre el estado del sistema y no una ausencia de medición.

**El umbral global de 85 / 80 y el de 95 de los dos motores vienen rotulados [ASUNCIÓN] desde el intake §17.1.P.6 · GeometriaFactory-Infrastructure.** El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`.

**El adaptador de reloj no lleva mutation score, y se declara por qué**: un umbral de mutación sobre una operación de una línea no aporta información. Es la única exención, y no se extiende a ningún otro componente.

## 8. Huecos identificados

### 8.1 `GeometriaFactory-Api`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| ~~**El intake escribía «nueve pruebas del validador» en el gate de este proyecto de código** —§17.1.P.8 · GeometriaFactory-Api— **y esa batería tiene diez**~~ **CERRADO** | Un lector del gate podía dar la puerta por cumplida con nueve, dejando `E-8` sin cubrir | **Cerrado por el intake 1.20**, que corrigió §17.1.P.8 · GeometriaFactory-Api —y los otros cuatro lugares que decían nueve— sobre el hallazgo que levantó esta categoría. No queda nada derivado al Product Owner por este motivo. Esta categoría aplicó **diez** desde su emisión, siguiendo la Fase C de `GeometriaFactory-Infrastructure`. Ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 |
| **El piso de cobertura de líneas baja el de la guía y no hay ADR** | `Rules-Calidad-Y-Pruebas.md` §2.2 fija **80 %** de aplicación para el tipo `rest-api` y este proyecto de código fija **75 %**, por el valor que el intake §17.1.P.6 · GeometriaFactory-Api declara con rótulo [ASUNCIÓN]. §2.2 exige un **ADR** para bajar cobertura, y no hay ninguna | **La categoría 05**, que es donde viven las ADR, con la constancia de que el número viene del intake y no de esta categoría. Mientras tanto la caída queda **declarada** en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 y compensada componente por componente, y **no se sube el número por cuenta propia** |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en los siete componentes con umbral | Elección y anclaje junto con el resto del tooling de la etapa `a`; hasta que corra, se reporta «sin medir» y no bloquea |
| **Los valores rotulados [ASUNCIÓN]** —cobertura, forma de la pirámide, percentil, caudal y arranque en frío— siguen sin confirmar | Los gates `QG-00003`, `QG-00004`, `QG-00013` y `QG-00014` son condicionados y no bloquean la fusión | El Product Owner sobre el intake §22, antes de fijar las puertas en `09-Devops` |
| **El formato de intercambio y su configuración** no están fijados, y **la decisión es de esta categoría 05 como productor** | `TC-00029` verifica que haya **1** sola configuración; **cuál sea** no está decidido, y los dos extremos tienen que coincidir o el contrato deja de ser el mismo | La categoría 05 de este proyecto de código, con `GeometriaFactory-Web` como consumidor |
| **La forma definitiva de las rutas se valida en el punto de control de la etapa `a`** | Los casos de verificación citan los puntos por su identificador `A-XX` y **no por su ruta**, precisamente para no atarse a un valor que todavía se valida | El punto de control de la etapa `a`. **Los identificadores no cambian** |
| **El mecanismo de construcción de la imagen en destino está rotulado [A VERIFICAR] por la fuente** | `PT-04` verifica que la imagen se construya y arranque **desde el contenedor de desarrollo**; que se construya **en destino desde el repositorio** es otra cosa y la fuente pide probarlo **una vez antes de depender de él** | El Product Owner y `09-Devops`, antes del despliegue real. **No es criterio de esta categoría**: el despliegue es manual y del Product Owner |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-00001` a `VER-00003` —el segundo es la **colección de peticiones reproducible** de `CU-00012`—, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas, `SD-00001` a `SD-00003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

### 8.2 `GeometriaFactory-Domain`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los cinco componentes | Elección y anclaje de la herramienta junto con el resto del tooling de la etapa `a`; hasta que corra, el mutation score se reporta como «sin medir» y no bloquea |
| **Los dos valores rotulados [ASUNCIÓN]** —cobertura y tiempo de la batería— siguen sin confirmar | Los gates `QG-02003` y `QG-02007` son condicionados y no bloquean la fusión | `BT-02015` del backlog técnico, antes de fijar la puerta de cobertura en `09-Devops` |
| **El criterio de comparación de dos correos no está decidido** (`02` §9, `BT-02016`) | `TC-02001` y `TC-02002` verifican que la unicidad llegue **declarada**, no cómo se compara. Mientras la decisión no exista, la normalización no se puede probar acá | `BT-02016`, junto con la capa que ejerce la verificación, antes de cerrar la etapa `d` |
| **El alcance efectivo de `INV-09` fuera de la admisibilidad** (`02` §9, `05` §11 `PA-03`) | `TC-02010` y `TC-02009` verifican la guarda **en la puerta única**. Si alguna capa de más arriba habilitara un camino que no pase por la admisibilidad, la marca tendría que volver a comprobarse ahí y esta matriz no lo detectaría | La categoría 02 de `GeometriaFactory-Api`, al fijar por dónde entra cada petición. No es bloqueante para este proyecto de código |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-02001` a `VER-02003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas `SD-02001` a `SD-02003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

### 8.3 `GeometriaFactory-Application`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los siete componentes con umbral | Elección y anclaje de la herramienta junto con el resto del tooling de la etapa `a`; hasta que corra, el mutation score se reporta como «sin medir» y no bloquea |
| **Los dos valores rotulados [ASUNCIÓN]** —cobertura y 500 ms— siguen sin confirmar | Los gates `QG-04003` y `QG-04010` son condicionados y no bloquean la fusión | `BT-04018` del backlog técnico, antes de fijar la puerta de cobertura en `09-Devops` |
| **El nombre del cuarto puerto no está fijado** (`05` §11 `PA-01`, `BT-04002`) | Los dobles de `Estrategia-Testing.md` §5 se escriben contra un nombre en lenguaje de dominio, y renombrarlos después es retrabajo en los cuatro componentes que lo consumen | `BT-04002`, en el punto de control de la etapa `a`, **antes** de escribir los casos de prueba que lo usan |
| **El criterio de comparación de dos correos no está decidido** (`05` §11 `PA-03`) | `TC-04001` y `TC-04002` verifican que la unicidad llegue **resuelta por el puerto**, no cómo se comparan dos correos. La normalización no se puede probar acá | La categoría 05 de `GeometriaFactory-Infrastructure`, junto con el índice que la sostenga. **No es bloqueante para este proyecto de código** |
| **Los sellos de alta, de modificación y de desenlace no son atributos del modelo del dominio** (`05` §11 `PA-04`, `BT-04020`) | `TC-04013` y `TC-04023` verifican que el sello sale **del puerto de reloj** y no del entorno; si el Product Owner decide incorporarlos al modelo, la verificación se muda de capa | El Product Owner, y `GeometriaFactory-Domain`. Sin fecha comprometida |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-04001` a `VER-04003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas, `SD-04001` a `SD-04003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

### 8.4 `GeometriaFactory-Infrastructure`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| ~~**El intake escribía «nueve pruebas del validador» en dos gates** —§17.1.P.8 · GeometriaFactory-Infrastructure y §17.1.P.8 · GeometriaFactory-Api— **y la batería tiene diez**~~ **CERRADO** | Un lector del gate podía dar la puerta por cumplida con nueve casos, dejando `E-8` sin cubrir, que es justamente el escenario que cerró la única condición del contrato de fachada sin dato de prueba | **Cerrado por el intake 1.20**, que corrigió los cinco lugares que decían nueve —los dos gates, §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.11 · GeometriaFactory-Application y el encabezado de §21— sobre el hallazgo que levantó esta categoría. Ya no hay nada derivado al Product Owner por este motivo. Esta categoría aplicó **diez** desde su emisión, siguiendo `05` §8 y §10.5, y no bajó la batería para que coincidiera con la redacción. Ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los siete componentes con umbral de mutación | Elección y anclaje junto con el resto del tooling de la etapa `a`; hasta que corra, se reporta «sin medir» y no bloquea |
| **Los tres valores rotulados [ASUNCIÓN]** —las dos coberturas y los 200 ms— siguen sin confirmar | Los gates `QG-06005`, `QG-06006` y `QG-06014` son condicionados y no bloquean la fusión | `PA-11` de `05` §11, antes de fijar la puerta de cobertura en `09-Devops` |
| **Cuál de las dos funciones de derivación de clave se ancla** no está decidido (`05` §11 `PA-03`) | `TC-06025` y `TC-06026` verifican **la forma** —parámetros versionados junto al valor derivado, sin valor por defecto silencioso— y no la función concreta. Los casos de prueba no cambian con la elección; los valores esperados de las pruebas de derivación sí | El equipo en la etapa `a`, aplicando el criterio que la ADR correspondiente declara |
| **Hasta dónde llega el conjunto de tipos reconstruibles** no está enumerado por ninguna fuente (`05` §11 `PA-04`) | `TC-06011` verifica los **seis** que los escenarios ejercitan. Un séptimo tipo produciría error de validación, que es correcto **pero puede no ser lo deseado** | El Product Owner, con la enumeración de las clases de la actividad. **No se agrega ningún tipo acá**, porque ninguna fuente lo enumera |
| **Cómo se sostiene que la provisoria «no se repite»** (`05` §11 `PA-06`) | `TC-06027` verifica **impredecibilidad y no repetición observada en dos producciones**; verificarla contra un registro de provisorias anteriores exigiría conservarlas, y el producto no guarda contraseñas en claro. Esta categoría **hereda la lectura y no la reabre** | El Product Owner, para confirmarla o reemplazarla |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-06001` a `VER-06003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas, `SD-06001` a `SD-06003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

## 9. Trazabilidad comprobación de autorización ↔ tests

### 9.1 `GeometriaFactory-Application`

Cuatro filas, una por comprobación de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4. Es la tabla que el NFR de ejercicio de las cuatro comprobaciones recorre.

| Comprobación | Motivo que emite al fallar | Test de su negativa | Sin base de datos | Estado |
| --- | --- | --- | --- | --- |
| Cambio de contraseña pendiente (la **cuarta**, que corta primero) | `PASSWORD_CHANGE_PENDING` | `TC-04011`, `TC-04008`, `TC-04010` | Sí | `Pendiente` |
| Pertenencia | `WORK_NOT_FOUND_FOR_REQUESTER` | `TC-04012`, `TC-04014`, `TC-04020`, `TC-04025` | Sí | `Pendiente` |
| Facultad | `ADMINISTRATOR_ROLE_REQUIRED` | `TC-04004`, `TC-04005`, `TC-04007`, `TC-04012`, `TC-04021`, `TC-04024` | Sí | `Pendiente` |
| Alcance del administrador | `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | `TC-04022`, `TC-04024`, `TC-04025` | Sí | `Pendiente` |

**Cuatro de cuatro con prueba de su negativa, y una sola prueba de orden.** `TC-04011` es esa prueba y verifica los **tres** cruces —marca contra pertenencia, marca contra facultad y marca contra alcance— más la única excepción declarada. `05` §8 fija el umbral en exactamente **1** prueba de orden, y esta matriz no agrega una segunda.

## 10. Trazabilidad regla conceptual de modelo ↔ tests

### 10.1 `GeometriaFactory-Infrastructure`

Siete filas, una por regla conceptual de [`../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/`](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/). **No compiten con las reglas de negocio**: una regla conceptual de modelo declara **cómo el dato sobrevive**, no qué decidió el negocio.

| RC | Qué exige, en una línea | Tests | Estado |
| --- | --- | --- | --- |
| [RC-06001](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md) | El texto original se escribe **una sola vez** y ninguna escritura posterior lo reemplaza | `TC-06016` | `Pendiente` |
| [RC-06002](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06002-Identidad-Posicional-De-La-Pieza.md) | La identidad de la pieza es su **posición**, y la de una figura no reconstruida **queda reservada** | `TC-06012` | `Pendiente` |
| [RC-06003](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06003-Valor-Declarado-Y-Derivado-Por-Separado.md) | El valor **declarado** y el **derivado** se guardan por separado, para no tener que recalcular en cada consulta | `TC-06005`, `TC-06017` | `Pendiente` |
| [RC-06004](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06004-La-Familia-No-Se-Persiste.md) | La familia plana o volumétrica **no se persiste**: se deriva del tipo | `TC-06017`, `TC-06019` | `Pendiente` |
| [RC-06005](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06005-Retiro-Fisico-Con-Arrastre.md) | El retiro es **físico y con arrastre**, todo o nada, sin marca lógica | `TC-06020`, `TC-06021` | `Pendiente` |
| [RC-06006](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06006-Tres-Sellos-De-Tiempo-Distintos.md) | Los **tres** sellos de tiempo del trabajo son distintos y no se confunden: la fecha que el alumno declara y los dos que registra el sistema | `TC-06017`, `TC-06031` | `Pendiente` |
| [RC-06007](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06007-La-Marca-No-Es-Un-Estado-De-Cuenta.md) | **La marca no es un estado de cuenta**: no ocupa su lugar ni lo reemplaza | `TC-06024` | `Pendiente` |

**Siete de siete con caso de prueba.** `RC-06001`, `RC-06005` y `RC-06007` son las tres que sostienen directamente un NFR con umbral cero, y por eso sus casos son también los que `QG-06011` y `QG-06010` miden.

## 11. La batería del validador contra los escenarios

### 11.1 `GeometriaFactory-Infrastructure`

Es la tabla de `05` §10.5 con la columna del caso de prueba que la materializa. **Diez filas, ninguna agrupada.** El detalle de cada escenario está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §6.

| # | Caso de la batería | Escenario | Caso de prueba | Estado |
| --- | --- | --- | --- | --- |
| 1 | Ortoedro con clave sinónima (`T1`) | `E-2` | `TC-06001` | `Pendiente` |
| 2 | Texto con comas finales (`T2`) | `E-2` | `TC-06002` | `Pendiente` |
| 3 | Cubo con caras `Cuadrado` (`T3`) | `E-3` | `TC-06003` | `Pendiente` |
| 4 | Cubo con caras `Rectangulo` (`T3`) | `E-4` | `TC-06004` | `Pendiente` |
| 5 | Área del cubo declarada contra derivada | `E-3` | `TC-06005` | `Pendiente` |
| 6 | Volumen del ortoedro declarado contra derivado | `E-2`, `E-1` | `TC-06006` | `Pendiente` |
| 7 | Dimensión en `0` que no descarta la figura | `E-6` | `TC-06007` | `Pendiente` |
| 8 | Tipo desconocido con posición y campo | `E-5` | `TC-06008` | `Pendiente` |
| 9 | Texto semilla completo | `E-1` | `TC-06009` | `Pendiente` |
| 10 | Dimensión no legible | `E-8` | `TC-06010` | `Pendiente` |

**Diez de diez, y siete de los ocho escenarios representados.** El octavo, `E-7`, no respalda ninguno de los diez y se ejercita igual en `TC-06011` y `TC-06019` como **cobertura adicional declarada** por `05` §10.5.

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.2 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **25 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.1 | 2026-08-29 | **Tramo `R-3b` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §8. **5 línea(s)** de este documento pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
