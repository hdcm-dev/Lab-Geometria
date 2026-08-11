# Matriz de cobertura de pruebas — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §4, §5, §6 y §7.3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §10.2 y §10.3; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) 1.1 §3
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad comprobación de autorización ↔ tests](#5-trazabilidad-comprobación-de-autorización--tests)
- [6. Trazabilidad invariante ↔ tests](#6-trazabilidad-invariante--tests)
- [7. Cobertura por capa](#7-cobertura-por-capa)
- [8. Huecos identificados](#8-huecos-identificados)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance

Es el documento bisagra de la categoría: relaciona los **once** casos de uso, los **nueve** NFR, las **dieciséis** reglas de negocio, las **cuatro** comprobaciones de autorización y los **nueve** invariantes con los **treinta y un** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido. La matriz se actualiza al cerrar cada etapa, que es la cadencia que [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §5 declara.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de comprobación de autorización contra prueba y la de invariante contra prueba. El motivo de la primera es que `05` §8 declara un NFR propio sobre el ejercicio de las cuatro comprobaciones, con umbral numérico; el de la segunda, que los invariantes no son un subconjunto de las reglas y esta capa aporta a cada uno algo distinto de lo que aporta el dominio (`05` §10.3).

## 2. Trazabilidad CU ↔ tests

Once filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Registrar el alta de una cuenta | Given un correo que el repositorio declara libre, When se invoca el auto-registro, Then la cuenta nace `Pendiente`, sin credencial y con papel `Alumno`; con correo ocupado, con credencial aportada, con estado pedido o con papel `Administrador`, Then se rechaza | `TC-01`, `TC-02` | US-01, US-02 | `Pendiente` |
| CU-02 Gobernar las cuentas de la comisión | Given el administrador y una cuenta de alumno, When la habilita, bloquea o rehabilita, Then la transición procede y las dos que producen provisoria dejan **la marca puesta**; When la da de baja con el correo escrito que coincide, Then arrastra sus trabajos en **una sola** unidad de trabajo | `TC-04`, `TC-05`, `TC-10` | US-04, US-05, US-06, US-08 | `Pendiente` |
| CU-03 Resolver el ingreso y la credencial del alumno | Given una cuenta en cada estado, con y sin la marca, When se consulta la admisibilidad, Then devuelve admisible o no admisible **con su motivo sin colapsar**; When la propia cuenta reemplaza su credencial presentando la vigente verificada, Then el valor se reemplaza **y la marca se levanta** | `TC-08`, `TC-09`, `TC-10`, `TC-04` | US-07, US-09, US-32, US-08 | `Pendiente` |
| CU-04 Cargar y reeditar un trabajo propio | Given dueño, texto original y el momento del puerto de reloj, When se constituye, Then nace en `Borrador` con el texto íntegro; When se reedita fuera de `Borrador` o sobre trabajo ajeno, Then se rechaza | `TC-13`, `TC-14` | US-10, US-11, US-12 | `Pendiente` |
| CU-05 Enviar un trabajo e interpretar su texto | Given un resultado de interpretación sin errores, When se envía, Then el dominio lo lleva a `Pendiente` aunque tenga advertencias; con al menos un error, Then queda en `Borrador`; con el puerto no disponible, Then termina de forma controlada y el texto queda intacto | `TC-15`, `TC-16`, `TC-17`, `TC-18`, `TC-19` | US-13, US-14, US-15, US-16 | `Pendiente` |
| CU-06 Consultar los trabajos propios del alumno | Given trabajos propios y ajenos, When el alumno pide su listado, Then recibe sólo los propios, con los cuatro estados y **sin componentes**; el detalle trae piezas, componentes, desenlace y comentario | `TC-20`, `TC-22` | US-17, US-18, US-19 | `Pendiente` |
| CU-07 Revisar los trabajos de la comisión | Given trabajos de dos alumnos en los cuatro estados, When el administrador pide el listado, Then **ningún `Borrador`** aparece y el filtro por alumno se compone con el alcance; el detalle es equivalente al del alumno | `TC-21`, `TC-22` | US-20, US-21, US-22 | `Pendiente` |
| CU-08 Dar desenlace a un trabajo | Given un trabajo en `Pendiente` y el administrador, When lo aprueba o lo rechaza, Then alcanza su estado terminal con comentario opcional; sin facultad, fuera del alcance o desde un estado terminal, Then se rechaza con el motivo que corresponde | `TC-23`, `TC-24` | US-23, US-24, US-25 | `Pendiente` |
| CU-09 Eliminar un trabajo | Given un trabajo propio en `Borrador` y el alumno, When lo elimina, Then se retira; Given los tres estados que el administrador ve, Then los tres se retiran; los dos alcances son opuestos y ninguno se filtra en el otro | `TC-25` | US-26, US-27 | `Pendiente` |
| CU-10 Configurar la cuenta de administrador | Given que el repositorio declara que no existe administrador y la credencial ya derivada, When se configura, Then nace `Habilitado`; una segunda configuración se rechaza | `TC-03` | US-03, US-28 | `Pendiente` |
| CU-11 Resetear la contraseña de un alumno | Given una cuenta de alumno en cualquiera de sus tres estados y el administrador, When se la resetea con la provisoria ya producida y derivada, Then conserva estado, papel, identidad y **todos** sus trabajos, y queda con la marca puesta; sobre la cuenta de administrador, Then se rechaza | `TC-06`, `TC-07`, `TC-10`, `TC-11` | US-29, US-30, US-31 | `Pendiente` |

**Once de once casos de uso con al menos un caso de prueba, y treinta y dos de treinta y dos historias cubiertas.** Ninguno queda huérfano y ningún `TC-XX` deja de referenciar un `CU-XX`, una `RN-XX`, un `INV-XX`, una comprobación o un NFR.

## 3. Trazabilidad NFR ↔ tests

Nueve filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo del caso de uso más pesado | Menos de **500 ms** para el envío que interpreta el texto de **3** piezas de `E-1`, medido **sin acceso a base** **[ASUNCIÓN del intake §17.2.P.10]** | Medición del pipeline por `BT-19`, **no un caso de prueba de comportamiento**. Gate `QG-10`, condicionado | Cronometrado dentro de la batería unitaria, con doble del puerto de validación | `Pendiente` |
| Cobertura de la biblioteca | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.2.P.6]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-03`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Pruebas de esta capa que tocan la base de datos real | Exactamente **0** | `TC-26` | Prueba de inspección del proyecto de pruebas, y revisión del pull request | `Pendiente` |
| Dependencias salientes del proyecto de código | Exactamente **1** al producto y **0** a persistencia, transporte, serialización o marco web | `TC-27` | Inspección del archivo de proyecto | `Pendiente` |
| Componentes de pieza en las consultas de listado | Exactamente **0** cargados, en los dos listados | `TC-30` | Inspección de la proyección devuelta por la consulta | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **36** alcanzadas, y **0** emitidas fuera del catálogo | `TC-28` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Ejercicio de las cuatro comprobaciones | **4 de 4** con prueba de su negativa **sin base de datos**, y **1** sola prueba de que la cuarta corta antes que las otras tres | `TC-11`, sobre la tabla de §5 | Prueba de orden y matriz de §5, revisada al cerrar cada etapa | `Pendiente` |
| Unidades de trabajo por caso de uso | **A lo sumo 1**, y **0** casos de uso que repartan su efecto | `TC-29` | Dobles instrumentados que cuentan aperturas, con la baja como caso testigo | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los dos valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para la cobertura y `A-5` para los 500 ms— y su conversión en trabajo es `BT-18`. Hasta entonces sus gates son **condicionados** ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1): se miden y se registran, y no bloquean la fusión.

**Tres de los nueve NFR no tienen caso de prueba y es correcto que no lo tengan**: son mediciones del pipeline, no comportamientos de la biblioteca. Inventarles un `TC-XX` habría producido un caso de prueba sin aserción sobre el sistema.

## 4. Trazabilidad RN ↔ tests

Dieciséis filas, una por regla. La columna de tramo es la que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 y [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §10.2 le asignan a cada una **en esta capa**; esta matriz la refleja y no la redefine. Las reglas se enuncian en `GeometriaFactory-Domain` y acá se referencian.

| RN | Tramo en esta capa | Tests que lo verifican | Estado |
| --- | --- | --- | --- |
| RN-01 Administrador único y papeles fijos | Ventana de alta y su negativa; rechazo del papel `Administrador` en el auto-registro; verificación de facultad; acotamiento del reseteo a cuentas de alumno | `TC-02`, `TC-03`, `TC-04`, `TC-05`, `TC-07`, `TC-12`, `TC-21`, `TC-24` | `Pendiente` |
| RN-02 El correo del alumno es único | La verificación sobre el conjunto de cuentas, en los dos caminos de alta | `TC-01`, `TC-02`, `TC-03` | `Pendiente` |
| RN-03 Trabajo ajeno indistinguible de inexistente | La verificación de pertenencia, con un solo motivo para los dos casos | `TC-12`, `TC-13`, `TC-14`, `TC-20`, `TC-25` | `Pendiente` |
| RN-04 Eliminación acotada al borrador para el alumno | Los dos alcances opuestos de la eliminación, y el arrastre de la baja | `TC-05`, `TC-14`, `TC-25` | `Pendiente` |
| RN-05 No se pasa a estado `Pendiente` con errores de validación | Entregar el conjunto de observaciones con su especie, **con el tramo principal en el dominio** | `TC-15`, `TC-16`, `TC-17` | `Pendiente` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | La consulta de admisibilidad con su motivo, y los estados iniciales opuestos de los dos caminos de alta | `TC-03`, `TC-04`, `TC-08`, `TC-09` | `Pendiente` |
| RN-07 Baja con arrastre y confirmación escrita | La comparación del correo escrito y el retiro de todos los trabajos **en la misma unidad de trabajo** | `TC-05`, `TC-29` | `Pendiente` |
| RN-08 Texto original conservado íntegro | El texto se entrega tal cual y no se reescribe **ni cuando la interpretación falla** | `TC-13`, `TC-14`, `TC-15`, `TC-16`, `TC-19` | `Pendiente` |
| RN-09 Observación de error con posición y campo | La cantidad de figuras del conjunto raíz como rango de validación, y el rechazo del conjunto mal formado, **con el tramo principal en el validador** | `TC-15`, `TC-16`, `TC-18` | `Pendiente` |
| RN-10 Desenlace exclusivo del administrador y terminalidad | La verificación de facultad y la propagación de la terminalidad | `TC-23`, `TC-24` | `Pendiente` |
| RN-11 El administrador no ve los borradores | El predicado de alcance **trasladado a la consulta** | `TC-21`, `TC-22`, `TC-24`, `TC-25` | `Pendiente` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | La postcondición que deja intactos estado, papel, identidad y todos los trabajos, y la **ausencia deliberada** de todo retiro | `TC-06`, `TC-10` | `Pendiente` |
| RN-13 Cambio forzado antes de toda otra capacidad | La **cuarta** comprobación transversal en los once casos de uso, y el único lugar donde la marca se levanta | `TC-06`, `TC-08`, `TC-09`, `TC-10`, `TC-11` | `Pendiente` |
| RN-14 La provisoria la produce el sistema | **Ninguno: es la única de las dieciséis sin tramo en esta capa** (`02` §6, `05` §10.2). Lo que se verifica acá es su consecuencia sobre la superficie —que el valor llega **ya producido y ya derivado** y que la operación lo rechaza vacío—, no su producción | `TC-04`, `TC-06` | `Pendiente` |
| RN-15 Resetear no exige cuenta habilitada | De forma **negativa**: no se comprueba el estado de la cuenta y no se devuelve ningún motivo por ese concepto | `TC-06`, `TC-07` | `Pendiente` |
| RN-16 Habilitar produce la provisoria | Habilitar y rehabilitar piden el valor al puerto, lo derivan afuera y fijan la credencial provisoria, dejando la marca puesta | `TC-04`, `TC-10` | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de prueba, y quince con tramo en esta capa.** La excepción es **RN-14**, y su fila lo declara: `05` §10.2 le asigna «ninguno de este proyecto de código». Tiene caso de prueba igual porque lo que se verifica acá **no es la producción de la provisoria** —que ocurre en `GeometriaFactory-Infrastructure`— sino su consecuencia sobre la superficie de esta capa. Es el mismo tratamiento que `GeometriaFactory-Domain` le dio en su matriz.

**RN-12, RN-13 y RN-16 comparten INV-09**, con la lectura que la categoría 02 de `GeometriaFactory-Domain` adoptó y que `05` §10.2 hereda declarando que **no afirma que la prosa del intake la respalde**. Esta matriz hereda esa lectura y tampoco lo afirma.

## 5. Trazabilidad comprobación de autorización ↔ tests

Cuatro filas, una por comprobación de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4. Es la tabla que el NFR de ejercicio de las cuatro comprobaciones recorre.

| Comprobación | Motivo que emite al fallar | Test de su negativa | Sin base de datos | Estado |
| --- | --- | --- | --- | --- |
| Cambio de contraseña pendiente (la **cuarta**, que corta primero) | `CAMBIO_DE_CONTRASENA_PENDIENTE` | `TC-11`, `TC-08`, `TC-10` | Sí | `Pendiente` |
| Pertenencia | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | `TC-12`, `TC-14`, `TC-20`, `TC-25` | Sí | `Pendiente` |
| Facultad | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | `TC-04`, `TC-05`, `TC-07`, `TC-12`, `TC-21`, `TC-24` | Sí | `Pendiente` |
| Alcance del administrador | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | `TC-22`, `TC-24`, `TC-25` | Sí | `Pendiente` |

**Cuatro de cuatro con prueba de su negativa, y una sola prueba de orden.** `TC-11` es esa prueba y verifica los **tres** cruces —marca contra pertenencia, marca contra facultad y marca contra alcance— más la única excepción declarada. `05` §8 fija el umbral en exactamente **1** prueba de orden, y esta matriz no agrega una segunda.

## 6. Trazabilidad invariante ↔ tests

Nueve filas, una por invariante. La columna de aporte es la de `05` §10.3, que declara **qué hace esta capa por cada uno**; los invariantes se enuncian en `GeometriaFactory-Domain`.

| Invariante | Qué aporta esta capa (`05` §10.3) | Test que lo verifica acá | Estado |
| --- | --- | --- | --- |
| INV-01 El correo del alumno es único | La verificación sobre el conjunto, por el puerto de repositorio de cuentas | `TC-01`, `TC-02`, `TC-03` | `Pendiente` |
| INV-02 Un alumno sólo accede a sus propios trabajos | La verificación de pertenencia sobre el dato recuperado, antes de escribir | `TC-11`, `TC-12`, `TC-13`, `TC-20`, `TC-22`, `TC-25` | `Pendiente` |
| INV-03 El alumno elimina sólo en `Borrador` y sobre trabajo propio | La misma pertenencia, más el traslado del alcance a la consulta | `TC-05`, `TC-11`, `TC-14`, `TC-25` | `Pendiente` |
| INV-04 Un trabajo `Finalizado` no tiene errores de interpretación | Entregar al dominio el conjunto completo de observaciones con su especie. **No decide el estado** | `TC-15`, `TC-16`, `TC-17` | `Pendiente` |
| INV-05 Existe exactamente un administrador | Resolver por el puerto si ya existe una cuenta con papel `Administrador` | `TC-02`, `TC-03`, `TC-07` | `Pendiente` |
| INV-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | Invocar la admisibilidad y propagar sus motivos **sin colapsarlos** | `TC-04`, `TC-08`, `TC-09` | `Pendiente` |
| INV-07 Un estado terminal no cambia de estado ni de contenido | Verificar la facultad **antes** de pedir la transición | `TC-23`, `TC-24` | `Pendiente` |
| INV-08 La cuenta de administrador está siempre `Habilitado` | **Nada propio, y es correcto**: no hay operación de esta capa que pueda violarlo. Lo protege por el costado el acotamiento del reseteo | `TC-03`, `TC-05`, `TC-07` | `Pendiente` |
| INV-09 Con la marca puesta la cuenta no ejerce ninguna otra capacidad | **El aporte más consecuente de esta capa**: la cuarta comprobación, en orden fijo y en un único componente | `TC-04`, `TC-06`, `TC-08`, `TC-10`, `TC-11` | `Pendiente` |

**Nueve de nueve con caso de prueba.** La fila de `INV-08` conserva su declaración de que esta capa no aporta nada propio y tiene pruebas igual: lo que verifican es que **ninguna operación de la capa lo viola**, que es una afirmación distinta y sí comprobable acá.

## 7. Cobertura por capa

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

**El umbral global de 85 / 80 viene rotulado [ASUNCIÓN] desde el intake §17.2.P.6.** El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`, y esta categoría lo adopta como tal sin atribuírselo al intake.

## 8. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los siete componentes con umbral | Elección y anclaje de la herramienta junto con el resto del tooling de la etapa `a`; hasta que corra, el mutation score se reporta como «sin medir» y no bloquea |
| **Los dos valores rotulados [ASUNCIÓN]** —cobertura y 500 ms— siguen sin confirmar | Los gates `QG-03` y `QG-10` son condicionados y no bloquean la fusión | `BT-18` del backlog técnico, antes de fijar la puerta de cobertura en `09-Devops` |
| **El nombre del cuarto puerto no está fijado** (`05` §11 `PA-01`, `BT-02`) | Los dobles de `Estrategia-Testing.md` §5 se escriben contra un nombre en lenguaje de dominio, y renombrarlos después es retrabajo en los cuatro componentes que lo consumen | `BT-02`, en el punto de control de la etapa `a`, **antes** de escribir los casos de prueba que lo usan |
| **El criterio de comparación de dos correos no está decidido** (`05` §11 `PA-03`) | `TC-01` y `TC-02` verifican que la unicidad llegue **resuelta por el puerto**, no cómo se comparan dos correos. La normalización no se puede probar acá | La categoría 05 de `GeometriaFactory-Infrastructure`, junto con el índice que la sostenga. **No es bloqueante para este proyecto de código** |
| **Los sellos de alta, de modificación y de desenlace no son atributos del modelo del dominio** (`05` §11 `PA-04`, `BT-20`) | `TC-13` y `TC-23` verifican que el sello sale **del puerto de reloj** y no del entorno; si el Product Owner decide incorporarlos al modelo, la verificación se muda de capa | El Product Owner, y `GeometriaFactory-Domain`. Sin fecha comprometida |
| **Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva** | Este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tiene categoría 10 emitida | `Rules-Calidad-Y-Pruebas.md` §2.1 omite la matriz para ese caso. Ver [`README.md`](README.md) §3 |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**once** filas de caso de uso con sus **treinta y dos** historias, **nueve** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, más dos tablas propias exigidas por el NFR de ejercicio de las comprobaciones y por `05` §10.3: **cuatro** filas de comprobación de autorización y **nueve** de invariante. Declara la cobertura por los **ocho** componentes con «Sin medir» en lugar de cero y con el componente de puertos **sin umbral** por no tener líneas ejecutables. Cita los dos valores rotulados **[ASUNCIÓN]** con su rótulo y declara sus gates como condicionados; separa el mutation score, que es piso de la regla de la categoría y no del intake; y declara **seis** huecos con su plan de remediación, incluida la ausencia de matriz de sensado de deriva con su fundamento. |
