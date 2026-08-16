# Estrategia de testing — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Estrategia-Testing.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §7; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §4, §8 y §10.5; [`../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5; [`../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §17.3.P.6, §17.3.P.8, §20 (los **ocho** escenarios `E-1` a `E-8`), §21 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md); `09-Devops` y `11-Documentacion`

---

## Tabla de contenido

- [1. Pirámide de testing deseada](#1-pirámide-de-testing-deseada)
- [2. Cobertura mínima por capa](#2-cobertura-mínima-por-capa)
- [3. Tooling](#3-tooling)
- [4. Especificaciones Given-When-Then](#4-especificaciones-given-when-then)
- [5. Mocks y fixtures](#5-mocks-y-fixtures)
- [6. Datos de prueba](#6-datos-de-prueba)
  - [6.1 Los ocho escenarios contra los diez casos de la batería](#61-los-ocho-escenarios-contra-los-diez-casos-de-la-batería)
- [7. Ambiente de testing](#7-ambiente-de-testing)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Pirámide de testing deseada

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código la adopta **con una redistribución acotada y declarada**.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Los **dos motores** —interpretación y verificación— sin almacén y sin red; los **dos mecanismos** de seguridad; y las pruebas de inspección estructural | **85 %** | Es donde vive la batería del validador, que es el corazón de este proyecto de código: **10** casos sobre **ocho** escenarios, todos sin almacén. El intake §17.3.P.10 pide medir la interpretación de `E-1` **sin almacén**, lo que sólo tiene sentido si el motor es probable así |
| Integración interna | Los **dos adaptadores de repositorio**, el contexto de persistencia y **la preparación del almacén al arrancar**, contra un almacén creado y descartado por la propia prueba | **15 %** | No es una elección: estas cuatro cosas **no se pueden verificar sin almacén**. El intake §17.3.P.8 declara una etapa propia del pipeline —**verificación de transformaciones**— y un criterio de aceptación de la etapa `c` que exige que las transformaciones **se apliquen solas sobre un almacén inexistente**. Sin este nivel, esa puerta no tiene dónde medirse |
| E2E y snapshot | — | **0 %** | **No aplica y se declara así en lugar de omitirse.** El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz. Un recorrido de punta a punta pasa por `GeometriaFactory-Api`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los cinco puntos que la regla asigna a extremo a extremo y snapshot se reasignan a unitario: el piso **sube** de 80 a 85. No se baja ninguna exigencia, de modo que no hace falta la ADR que §2.2 exige para bajar cobertura.

**Dónde termina esta capa y empieza la batería de integración del producto, dicho con precisión.** El intake §17.3.P.6 declara que **«la persistencia real contra SQLite se prueba desde `GeometriaFactory.Integration.Tests`»**, y ese proyecto de pruebas **pertenece a `GeometriaFactory-Api`** (§17.5.P.6). La integración interna de acá no lo reemplaza y no lo duplica:

| Verificación | Dónde vive | Por qué |
| --- | --- | --- |
| Que el esquema se cree y se transforme sobre un almacén inexistente, y que el arranque se detenga ante uno dudoso | **Acá**, integración interna | Es puerta del pipeline **de este proyecto de código** y criterio de aceptación de la etapa `c` (intake §17.3.P.8) |
| Que un adaptador materialice, recupere y retire respetando el todo o nada | **Acá**, integración interna | Es el contrato del puerto, y su forma de fallar es propia del adaptador |
| Que el producto entero, atendiendo por su superficie, opere sobre el almacén real | **En `GeometriaFactory-Api`** | Es lo que el intake §17.3.P.6 y §17.5.P.6 declaran, y lo que golpea la superficie por su protocolo |

**Contra la pirámide invertida**: acá sería imposible, porque no hay recorrido de punta a punta que construir. **Contra la pirámide aplanada** —un número global sin distinguir capas— la defensa es §2, que reporta por componente, con el validador con un umbral propio y más alto que el resto.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte:**

- **Prueba de inspección.** Comprueba una propiedad estructural: cero peticiones de red de los dos motores, el conjunto de códigos emitidos contra el catálogo, cero mensajes con un secreto o con la ruta del almacén.
- **Prueba con el almacén interrumpido a mitad de operación.** Es la única forma de verificar que un retiro parcial no ocurre, y `05` §8 la declara como mecanismo de medición de ese NFR.

## 2. Cobertura mínima por capa

La partición es por los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1. El piso global lo fija el intake §17.3.P.6 y es **85 % de líneas y 80 % de ramas**; el validador tiene un piso propio de **95 % de líneas**. Los tres valores vienen rotulados **[ASUNCIÓN del intake §22, asunción `A-3`]**.

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Contexto de persistencia y mapeo | 85 % | 80 % | 60 % | Piso del intake §17.3.P.6 |
| Adaptador de repositorio de trabajos | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene el texto original conservado —tramo principal de `RN-06008`— y la proyección de listado, donde `05` §9 declara probabilidad **media-alta** de arrastrar componentes por defecto |
| Adaptador de repositorio de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene la unicidad como segunda línea y **la marca que viaja sin ser un estado de cuenta** |
| Motor de interpretación de figuras | **95 %** | 90 % | 60 % | **Piso propio del intake §17.3.P.6**: es el número más alto del producto y está donde la fuente señala el criterio que más veces se rompe. El 90 de ramas lo sube esta categoría |
| Motor de verificación de valores | **95 %** | 90 % | 60 % | Ídem: los dos motores son «el validador de figuras» al que el intake le asigna el 95 |
| Adaptador de reloj del sistema | 100 % | 100 % | — | Es el contrato más corto de la capa y no tiene ramas que valga la pena dejar sin cubrir. **Sin mutation score**: un umbral de mutación sobre una operación de una línea no aporta información |
| Mecanismo de credenciales | 95 % | 90 % | 60 % | Sube sobre el piso: contiene la producción de la provisoria, cuyo modo de falla `05` §9 declara de impacto **muy alto** y que **no se nota hasta que alguien la usa** |
| Mecanismo de acceso firmado y preparación del almacén | 95 % | 90 % | 60 % | Sube sobre el piso: contiene los otros dos modos de falla de impacto muy alto —emitir sin clave y recrear el almacén en lugar de transformarlo— |
| **Proyecto de código completo** | **85 %** | **80 %** | **60 %** | Intake §17.3.P.6 [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 85/80 global y el **95 de líneas del validador** son del intake y vienen rotulados **[ASUNCIÓN]**. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`, y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los valores de ramas por encima del piso y los tres componentes que suben a 90 o 95 los sube esta categoría con el fundamento de la columna.

**La cobertura no se reporta como número global único.** Un 85 % global con el motor de interpretación en 80 % es un incumplimiento aunque el promedio cierre, porque el 95 del validador es un piso propio y no un promedio ponderado.

## 3. Tooling

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a`, con dos puntos abiertos propios: **cuál de las dos funciones de derivación de clave se ancla** (`05` §11 `PA-03`) y la versión del motor de almacenamiento embebido.

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Integración interna | El mismo marco, con un **almacén efímero creado y descartado por cada prueba**, y con la ubicación del almacén recibida por configuración de prueba |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Dobles | Sólo donde hace falta aislar el mundo: la **fuente de material impredecible**, para poder simular que no responde (`TC-06028`), y el **almacén interrumpido a mitad de operación** (`TC-06021`). El reloj **no se dobla acá**: acá se implementa |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente **y con un informe acotado a los dos motores**, que es lo que `QG-06` mide |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Medición del tiempo de interpretación | Cronometrado dentro de la batería unitaria, **sin almacén**, que es la condición que el intake §17.3.P.10 declara |
| Inspección estructural | El propio marco de pruebas, leyendo las dependencias de los dos motores, el conjunto de códigos emitidos y el registro del servidor |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a`.

## 4. Especificaciones Given-When-Then

**Los criterios de aceptación de las veinticinco historias ya están escritos en Given/When/Then**, y la Definition of Ready lo exige.

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe citando la historia de origen.

**Y hay un motivo propio de este proyecto de código para no hacerlo**: los **diez** casos de la batería del validador ya están enumerados en dos lugares del corpus —[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §7 y `05` §10.5— y su fuente última es el intake §21. Un tercer juego de archivos de escenario abriría una **cuarta** enunciación del mismo caso.

**Dónde sí se usan pruebas basadas en propiedades:**

| Propiedad | Enunciado |
| --- | --- |
| Impredecibilidad de la provisoria | Para toda cuenta y todo par de producciones consecutivas, las dos provisorias son distintas, y ninguna es derivable del nombre, del correo ni de la fecha |
| Conservación del texto | Para todo texto original y toda materialización posterior del mismo trabajo, el texto guardado es idéntico carácter por carácter al primero |
| Todo o nada | Para toda operación de escritura y todo punto de interrupción del almacén, o el efecto está entero o no está nada |
| Conjunto cerrado de condiciones | Para toda invocación que rechaza, el código devuelto pertenece a las **17** condiciones del catálogo |

## 5. Mocks y fixtures

**Política de dobles: los mínimos, y sólo del mundo.** Esta capa **es** el borde del sistema, de modo que doblar sus propias piezas la vaciaría de contenido. Lo que sí se sustituye:

| Doble | Cuándo | Por qué |
| --- | --- | --- |
| Fuente de material impredecible que **no responde** | `TC-06028` | Es la única forma de verificar que ante su ausencia **no se compone una provisoria por otro medio** —un contador, la fecha, el correo—, que `05` §9 declara de impacto muy alto |
| Almacén **interrumpido a mitad de operación** | `TC-06021` | Es el mecanismo de medición que `05` §8 declara para el NFR de cero retiros parciales |
| Ubicación del almacén **no disponible** | `TC-06033` | Verifica que el arranque **se detiene** en lugar de caer hacia una ruta alternativa dentro de la imagen, que `05` §9 declara de impacto alto y probabilidad media |
| Esquema **que no corresponde** al linaje esperado | `TC-06033` | Verifica que el almacén **no se descarta ni se recrea**, que es «el atajo más destructivo del producto» según `05` §9 |

**Lo que no se dobla, y por qué:** el reloj —acá se implementa, no se consume—; el almacén en su operación normal —para eso está el almacén efímero de la integración interna—; y los dos motores entre sí —el de verificación consume las piezas que el de interpretación reconstruye, y probarlos por separado con piezas inventadas perdería exactamente el acoplamiento que la batería verifica—.

Fixtures compartidos:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Los **ocho** textos de los escenarios del intake §20 | El texto original de `E-1` a `E-8`, **literal y carácter por carácter**, con sus comas finales y sus claves tal como están | Es el material de los diez casos de la batería. Ver §6 |
| Almacén efímero preparado | Un almacén recién creado con las transformaciones aplicadas, y otro **inexistente** | Los dos adaptadores y la preparación del almacén los necesitan en los dos estados |
| Cuenta en cada uno de sus tres estados, con y sin la marca | Seis combinaciones | `CU-06005` escribe la marca sobre los tres estados sin alterarlos |
| Trabajo con piezas, componentes y observaciones | Un trabajo materializable completo, y su proyección de listado | La distinción entre las **dos formas de lectura** se prueba contra el mismo trabajo |

## 6. Datos de prueba

**Los datos de prueba de este proyecto de código son reales y no se sustituyen por datos sintéticos, y acá esa regla es más estricta que en ninguna otra capa.** Los **ocho** escenarios `E-1` a `E-8` del intake §20 **son datos salidos de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra**, cada uno con su procedencia y su estado declarado —`medido`, `derivado` o `reconstruido`—. Esta es **la capa que los interpreta**: acá entran como **texto**, entero y literal, no como resultado ya producido.

**Por qué esta capa no puede permitirse un dato sintético.** El riesgo de negocio que el intake declara y que `05` §9 pone primero es **«que el validador se escriba sin leer el análisis y no sirva para el dato que existe»**, con probabilidad **alta si no se controla** y con la consecuencia de dejar el producto inútil para el dato real. Un texto de prueba escrito a mano por comodidad **pasaría** las cuatro trampas sin ejercitarlas, porque quien lo escribe ya sabe cuáles son. Los ocho escenarios existen precisamente porque nadie los escribió pensando en el validador.

| Escenario | Estado declarado | Qué aporta a esta capa | Fuente del valor |
| --- | --- | --- | --- |
| `E-1` | **medido** | El texto semilla: **3 piezas y 2 advertencias**; el cilindro **sin ninguna observación**, porque su diferencia de `0.01` **no supera** la tolerancia estricta. Es además el material del NFR de 200 ms | §20.E-1, «Qué verificar» puntos 2 a 5 |
| `E-2` | **derivado** | **Las dos comas finales** (`T2`) y **la clave `Tapas` en el ortoedro** (`T1`); 1 pieza con 2 bases y 4 laterales; área sin observación y **volumen con advertencia**: derivado 1029.00 contra declarado 343.00 | §20.E-2, puntos 1 a 6 |
| `E-3` | **medido** | Caras `Cuadrado` (`T3`) y **el área declarada 36.00 contra la derivada 54.00**; volumen sin observación; y el trabajo **no se rechaza ni se corrige el valor** | §20.E-3, puntos 1 a 5 |
| `E-4` | **derivado** | Caras `Rectangulo` (`T3`) y **cero observaciones en total**. Es el **criterio negativo**: un validador que advirtiera siempre pasaría `E-3` y fallaría éste | §20.E-4, puntos 1 a 4 |
| `E-5` | **reconstruido** | Tipo desconocido: observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**; y **la primera pieza, válida, se interpreta igual** | §20.E-5, puntos 1 a 3 |
| `E-6` | **reconstruido** | Dimensión en `0.00`: la figura **se interpreta**, no se descarta, y produce **a lo sumo una advertencia**, nunca un error de interpretación. Es el criterio de **existencia contra veracidad** | §20.E-6, puntos 1 a 3 |
| `E-7` | **derivado** | Los **seis** tipos reconstruibles, con la clave `Bases` en el ortoedro, y las figuras planas como piezas del conjunto raíz. **No respalda ninguno de los diez casos** y se usa como cobertura **adicional declarada** (`05` §10.5) | §20.E-7, puntos 1 a 3 |
| `E-8` | **reconstruido** | Dimensión **no legible**: el error que la configuración regional del alumno produce de verdad. **El código es de dimensión no legible y no de texto inválido**: el texto es sintácticamente válido y lo que falla es la lectura de un valor. **Confundir los dos códigos es el error que este escenario detecta** | §20.E-8, puntos 2, 3 y 5 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un fixture de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización. Si el intake cambia un escenario, el cambio baja acá como una corrección con su fila de control de cambios.

**Lo que no se inventa.** Ningún caso de prueba de este proyecto de código introduce un texto de figuras que no esté en §20. Donde hace falta un dato que ningún escenario da —un correo, un identificador de trabajo, un momento, una contraseña en claro— se usa un valor evidentemente ficticio y se declara como tal en el `TC-XX`: son datos de identidad y de mecanismo, no datos de geometría.

### 6.1 Los ocho escenarios contra los diez casos de la batería

Es la tabla de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §10.5, con la columna que a esta categoría le toca: **qué caso de prueba la materializa**. Ninguna fila se agrupa y ninguna se agrega.

| # | Caso de la batería | Escenario | CU | Paso del flujo | Caso de prueba |
| --- | --- | --- | --- | --- | --- |
| 1 | Ortoedro con clave sinónima (`T1`) | `E-2` | CU-06001 | P-3 | `TC-06001` |
| 2 | Texto con comas finales (`T2`) | `E-2` | CU-06001 | P-2 | `TC-06002` |
| 3 | Cubo con caras `Cuadrado` (`T3`) | `E-3` | CU-06001 | P-4 | `TC-06003` |
| 4 | Cubo con caras `Rectangulo` (`T3`) | `E-4` | CU-06001 | P-4 | `TC-06004` |
| 5 | Área del cubo declarada contra derivada | `E-3` | CU-06002 | P-6 | `TC-06005` |
| 6 | Volumen del ortoedro declarado contra derivado | `E-2`, `E-1` | CU-06002 | P-6 | `TC-06006` |
| 7 | Dimensión en `0` que no descarta la figura | `E-6` | CU-06001 y CU-06002 | P-4 y P-6 | `TC-06007` |
| 8 | Tipo desconocido con posición y campo | `E-5` | CU-06001 | P-3 | `TC-06008` |
| 9 | Texto semilla completo | `E-1` | CU-06001 y CU-06002 | P-1 a P-7 | `TC-06009` |
| 10 | Dimensión no legible | `E-8` | CU-06001 | P-4 | `TC-06010` |

**Diez casos, uno por fila, y siete de los ocho escenarios representados.** El octavo, `E-7`, **no respalda ninguno de los diez y se usa igual**, como cobertura adicional declarada: `TC-06011` lo ejercita porque es el único texto que cubre los **seis** tipos reconstruibles. La afirmación no es de esta categoría: la hace `05` §10.5 y acá se hereda.

**El décimo caso existe por una decisión del Product Owner.** El intake §21 lo agrega con el rótulo **[DECISIÓN 2026-08-09]** y declara que `E-8` cerró la única condición del contrato de fachada que no tenía dato de prueba. Sobre el recuento de **nueve** que dos gates del intake escribieron **hasta 1.19** y que el intake **1.20** corrigió a **diez**, ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake §17.3.P.9) |
| Almacén | **Efímero, creado y descartado por cada prueba de integración interna.** Nunca el almacén de desarrollo ni el de producción. Su ubicación **se recibe por configuración de prueba y no se busca** |
| Aislamiento entre pruebas | Total. Las unitarias no comparten estado; las de integración interna crean su propio almacén y lo descartan. Ninguna prueba depende del orden de ejecución |
| Paralelismo | **Admitido en el nivel unitario. En integración interna, sólo si cada prueba tiene su propio archivo de almacén**: el motor de almacenamiento del producto es de **escritor único**, y dos pruebas sobre el mismo archivo se bloquearían entre sí |
| Secretos | **Ninguno real.** La clave de firma de las pruebas es un valor evidentemente ficticio, declarado como tal, **provisto por configuración de prueba**. `TC-06030` verifica que **sin clave no hay emisión**, y para eso hace falta poder no proveerla |
| Reloj | **Acá se implementa el reloj, no se consume**: `TC-06031` verifica que el sello sale del puerto. Ninguna prueba fija el reloj del entorno |
| Datos de geometría | Los **ocho** textos del intake §20, literales. **Ningún texto de figuras se escribe a mano** |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** El único tiempo declarado es el de la **interpretación** del texto de `E-1`: menos de **200 ms**, medido sin almacén [ASUNCIÓN del intake §17.3.P.10]. Ninguna fuente da un tiempo de suite para esta capa, y no se inventa uno |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** §6 afirmaba en presente que «dos gates del intake **todavía escriben**» nueve casos de batería. El intake **1.20** los corrigió a **diez**, en el mismo commit que emitió este documento. Reescrito contra el texto vivo, con el nueve ubicado **hasta 1.19**. **Es una décima ocurrencia del patrón que el informe contó en nueve pasajes.** Ningún dato de prueba, fixture, umbral ni caso cambia: la tabla de §6.1 tenía y sigue teniendo **diez** filas. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo con su redistribución acotada —85 unitario, 15 integración interna, 0 de extremo a extremo—, con la frontera precisa entre la integración interna de esta capa y la batería de integración del producto, que el intake ubica en `GeometriaFactory-Api`. Declara la cobertura por los **ocho** componentes, con el piso propio de **95 %** que el intake le asigna al validador y con el origen de cada número separado del mutation score, que es de la regla de la categoría. Declara el tooling por función, la política de dobles **mínimos y sólo del mundo** con los cuatro admitidos y lo que explícitamente no se dobla, los **ocho** escenarios reales del intake §20 **como texto entero y literal** —con el motivo por el que esta capa no puede permitirse un dato sintético— y su §6.1 cruza los ocho escenarios contra los **diez** casos de la batería, uno por fila, con el caso de prueba que materializa cada uno. Declara el ambiente, incluida la restricción de paralelismo que impone el escritor único y la constancia de que no se declara ningún tiempo de suite que ninguna fuente dé. |
