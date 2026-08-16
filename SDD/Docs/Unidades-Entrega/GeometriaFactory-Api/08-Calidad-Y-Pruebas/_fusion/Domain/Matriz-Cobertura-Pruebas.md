# Matriz de cobertura de pruebas — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Domain/Especificacion-Funcional.md) 1.9 §3 y §4; [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 y §8
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
  - [2.1 Las dos pruebas de inspección estructural y a qué trazan](#21-las-dos-pruebas-de-inspección-estructural-y-a-qué-trazan)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad invariante ↔ tests](#5-trazabilidad-invariante--tests)
- [6. Cobertura por capa](#6-cobertura-por-capa)
- [7. Huecos identificados](#7-huecos-identificados)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Propósito y alcance

Es el documento bisagra de la categoría: relaciona los **trece** casos de uso, los **seis** NFR, las **dieciséis** reglas de negocio y los **nueve** invariantes con los **veintisiete** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido. La matriz se actualiza al cerrar cada etapa, que es la cadencia que [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §5 declara.

Esta matriz **agrega una cuarta tabla** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de invariante contra prueba. El motivo es que en este proyecto de código los invariantes no son un subconjunto de las reglas —diez reglas tienen invariante y seis no lo tienen— y `05` §8 declara un NFR propio sobre su ejercicio.

## 2. Trazabilidad CU ↔ tests

Trece filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Domain/Especificacion-Funcional.md) §3. Ninguna se agrupa.

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
| `TC-02025` El dominio no lee el reloj ni el conjunto | Cero ocurrencias de lectura de reloj y de consulta de conjuntos en las operaciones públicas, y dos ejecuciones idénticas sin fijar el reloj del entorno | [`ADR-02006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `BT-02009`; `05` §7, filas de configuración y de zona horaria | `Pendiente` |
| `TC-02027` Ninguna condición prevista viaja como excepción | Las **42** condiciones llegan como valor de retorno tipado y **0** invocaciones lanzan; la distinción con el defecto de programación del consumidor se verifica aparte | [`ADR-02002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `BT-02007`; `QG-08` | `Pendiente` |

**Por qué no se les inventa un `CU-XX` ni una `RN-XX`.** Las dos verifican decisiones de arquitectura, no comportamiento pedido por un caso de uso: atarlas a un identificador funcional para que la tabla cerrara sería exactamente la clase de trazabilidad falsa que esta matriz existe para evitar. Lo que sí corresponde es que **estén enumeradas**, y esta subsección es su instrumento.

## 3. Trazabilidad NFR ↔ tests

Seis filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo de la batería de pruebas del dominio | Menos de **10 segundos** de punta a punta **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain]** | Medición del pipeline, **no un caso de prueba**. Gate `QG-07`, condicionado | Duración total reportada por el ejecutor en la etapa `test` | `Pendiente` |
| Cobertura de la biblioteca | **90 %** de líneas y **85 %** de ramas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-03`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Dependencias salientes del proyecto de código | Exactamente **0** y **0** | `TC-02024` | Inspección del archivo de proyecto, y revisión del pull request | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **42** condiciones alcanzadas, y **0** emitidas fuera del catálogo | `TC-02023` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Ejercicio de los invariantes | **100 %** de los **nueve** con prueba de violación rechazada, **sin dobles** | `TC-02026`, sobre la tabla de §5 | Prueba de inspección sobre la matriz, revisada al cerrar cada etapa | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los dos valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y `A-5` para el tiempo— y su conversión en trabajo es `BT-02015`. Hasta entonces sus gates son **condicionados** ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1): se miden y se registran, y no bloquean la fusión.

**Tres de los seis NFR no tienen caso de prueba y es correcto que no lo tengan**: son mediciones del pipeline, no comportamientos de la biblioteca. Inventarles un `TC-XX` habría producido un caso de prueba sin aserción sobre el sistema.

## 4. Trazabilidad RN ↔ tests

Dieciséis filas, una por regla de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Domain/Especificacion-Funcional.md) §4. Ninguna se agrupa. La columna de invariante es la que esa sección le asigna a cada regla.

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

## 5. Trazabilidad invariante ↔ tests

Nueve filas, una por invariante de [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4. Es la tabla que `TC-02026` recorre.

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

## 6. Cobertura por capa

La partición es por los **cinco** componentes de `05` §3.1, no por capas de despliegue: este proyecto de código no tiene ninguna. Los umbrales son los de [`Estrategia-Testing.md`](../../Estrategia-Testing.md) §2.

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

## 7. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los cinco componentes | Elección y anclaje de la herramienta junto con el resto del tooling de la etapa `a`; hasta que corra, el mutation score se reporta como «sin medir» y no bloquea |
| **Los dos valores rotulados [ASUNCIÓN]** —cobertura y tiempo de la batería— siguen sin confirmar | Los gates `QG-03` y `QG-07` son condicionados y no bloquean la fusión | `BT-02015` del backlog técnico, antes de fijar la puerta de cobertura en `09-Devops` |
| **El criterio de comparación de dos correos no está decidido** (`02` §9, `BT-02016`) | `TC-02001` y `TC-02002` verifican que la unicidad llegue **declarada**, no cómo se compara. Mientras la decisión no exista, la normalización no se puede probar acá | `BT-02016`, junto con la capa que ejerce la verificación, antes de cerrar la etapa `d` |
| **El alcance efectivo de `INV-09` fuera de la admisibilidad** (`02` §9, `05` §11 `PA-03`) | `TC-02010` y `TC-02009` verifican la guarda **en la puerta única**. Si alguna capa de más arriba habilitara un camino que no pase por la admisibilidad, la marca tendría que volver a comprobarse ahí y esta matriz no lo detectaría | La categoría 02 de `GeometriaFactory-Api`, al fijar por dónde entra cada petición. No es bloqueante para este proyecto de código |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-02001` a `VER-02003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas `SD-02001` a `SD-02003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`** declarado en §7. Se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-02001` a `VER-02003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0 con **tres** filas en `Sin verificar`. La fila del hueco se **conserva** con su desenlace y su fecha, en lugar de retirarse. **Ninguna de las cinco tablas de cobertura cambia**: las sondas no sustituyen a ningún caso de prueba y su alcance está declarado en §5 de la propia matriz de sensado. |
| 1.1 | 2026-08-11 | **`H-04`.** El cierre de §2 afirmaba que **ningún `TC-XX` deja de referenciar** un `CU-XX`, una `RN-XX`, un `INV-XX` o un NFR, y era falso en su propio documento: `TC-02025` y `TC-02027` trazan a una ADR, a una tarea técnica y a un quality gate, y no tenían fila en ninguna de las cuatro tablas. La frase se reemplaza por el recuento verdadero —**25 de 27**— y se agrega **§2.1**, que enumera las dos con su trazabilidad. **Ninguna cobertura, umbral ni caso cambia.** Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**trece** filas de caso de uso, **seis** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, una cuarta de **nueve** invariantes exigida por el NFR de ejercicio de `05` §8, y la cobertura por los **cinco** componentes con «Sin medir» en lugar de cero. Cita los dos valores rotulados **[ASUNCIÓN]** con su rótulo y declara sus gates como condicionados; separa el mutation score, que es piso de la regla de la categoría y no del intake; y declara **cinco** huecos con su plan de remediación, incluida la ausencia de matriz de sensado de deriva con su fundamento. |
