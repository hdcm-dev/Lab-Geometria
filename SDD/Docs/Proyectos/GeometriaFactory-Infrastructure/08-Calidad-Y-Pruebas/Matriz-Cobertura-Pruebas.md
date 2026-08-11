# Matriz de cobertura de pruebas — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5, §6 y §7.3; [`../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/`](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §10.2, §10.3 y §10.5
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito y alcance](#1-propósito-y-alcance)
- [2. Trazabilidad CU ↔ tests](#2-trazabilidad-cu--tests)
- [3. Trazabilidad NFR ↔ tests](#3-trazabilidad-nfr--tests)
- [4. Trazabilidad RN ↔ tests](#4-trazabilidad-rn--tests)
- [5. Trazabilidad regla conceptual de modelo ↔ tests](#5-trazabilidad-regla-conceptual-de-modelo--tests)
- [6. La batería del validador contra los escenarios](#6-la-batería-del-validador-contra-los-escenarios)
- [7. Cobertura por capa](#7-cobertura-por-capa)
- [8. Huecos identificados](#8-huecos-identificados)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Propósito y alcance

Es el documento bisagra de la categoría: relaciona los **diez** casos de uso, los **catorce** NFR, las **dieciséis** reglas de negocio, las **siete** reglas conceptuales de modelo y los **diez** casos de la batería del validador con los **treinta y cinco** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el proyecto de código no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de regla conceptual de modelo contra prueba y la de la batería del validador contra escenario. La primera, porque las **siete** reglas conceptuales de `02` no son reglas de negocio y son propias de esta capa: declaran **cómo el dato sobrevive**, no qué decidió el negocio. La segunda, porque la batería es una puerta bloqueante del pipeline con recuento propio y su trazabilidad no cabe dentro de la tabla de casos de uso.

## 2. Trazabilidad CU ↔ tests

Diez filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-01 Interpretar el texto original y reconstruir las piezas | Given el texto real del alumno con sus **cuatro** trampas de formato, When se lo interpreta, Then las piezas se reconstruyen con su posición, la cantidad de figuras del conjunto raíz **incluye las no reconstruidas** y los errores llegan con índice de figura y campo | `TC-01`, `TC-02`, `TC-03`, `TC-04`, `TC-07`, `TC-08`, `TC-09`, `TC-10`, `TC-11`, `TC-12`, `TC-13` | US-01, US-02, US-03, US-04 | `Pendiente` |
| CU-02 Verificar los valores declarados contra los derivados | Given piezas ya reconstruidas, When se derivan área y volumen, Then se comparan con tolerancia **0.01** y operador **estricto**, y la discrepancia **se señala con los dos valores, sin corregir ni rechazar** | `TC-05`, `TC-06`, `TC-07`, `TC-09` | US-05, US-06, US-07 | `Pendiente` |
| CU-03 Guardar y recuperar los trabajos | Given un trabajo con sus piezas, componentes y observaciones, When se lo materializa, Then queda en **una** unidad de trabajo con el texto **literal**; una escritura que lo reemplace se rechaza; y una consulta **sin recorte declarado no se resuelve** | `TC-16`, `TC-17`, `TC-18`, `TC-19` | US-08, US-09, US-10, US-11 | `Pendiente` |
| CU-04 Ejecutar el borrado físico y el arrastre de la baja | Given un trabajo o una cuenta con sus trabajos, When se ejecuta el retiro, Then es **físico y todo o nada**; con el almacén interrumpido, **no se retira nada** | `TC-20`, `TC-21` | US-12, US-13 | `Pendiente` |
| CU-05 Guardar y recuperar las cuentas de la comisión | Given el almacén, When se materializa una cuenta con un correo ocupado o un segundo administrador, Then se rechaza; las **dos** preguntas sobre el conjunto se responden sin revelar la cuenta; y **la marca viaja sin alterar el estado** | `TC-22`, `TC-23`, `TC-24` | US-14, US-15, US-16 | `Pendiente` |
| CU-06 Derivar la contraseña y verificar una credencial | Given una contraseña en claro, When se la deriva, Then el valor derivado lleva **sus parámetros versionados** y la contraseña **no queda escrita en ninguna parte**; el derivado ilegible **se distingue** de la contraseña equivocada | `TC-25`, `TC-26` | US-17, US-18 | `Pendiente` |
| CU-07 Producir la contraseña provisoria del reseteo | Given dos producciones consecutivas, When se comparan, Then **son distintas** y ninguna es derivable de un dato conocido; sin fuente de aleatoriedad, **no se produce ningún valor por otro medio** | `TC-27`, `TC-28` | US-19, US-20 | `Pendiente` |
| CU-08 Emitir el acceso firmado | Given los **cuatro** reclamos y la clave, When se emite, Then la firma verifica; **sin clave no hay emisión** y no se genera una al vuelo | `TC-29`, `TC-30` | US-21, US-22 | `Pendiente` |
| CU-09 Proveer el sello del reloj del sistema | Given el adaptador, When se le pide el momento, Then lo devuelve. Es el contrato más corto de la capa | `TC-31` | US-23 | `Pendiente` |
| CU-10 Preparar el almacén al arrancar | Given un almacén inexistente, When arranca la preparación, Then se crea y se transforma **sola**; ante un esquema que no corresponde o una ubicación no disponible, **el arranque se detiene** y el almacén **no se recrea** | `TC-32`, `TC-33` | US-24, US-25 | `Pendiente` |

**Diez de diez casos de uso con al menos un caso de prueba, y veinticinco de veinticinco historias cubiertas.** Ninguno queda huérfano.

## 3. Trazabilidad NFR ↔ tests

Catorce filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo de interpretación del texto semilla | Menos de **200 ms** para el texto de **3** piezas de `E-1`, **sin almacén** **[ASUNCIÓN del intake §17.3.P.10]** | `TC-15`. Gate `QG-14`, condicionado | Cronometrado dentro de la batería unitaria, sin abrir el almacén | `Pendiente` |
| Cobertura del proyecto de código | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.3.P.6]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-05`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Cobertura del validador de figuras | **95 %** de líneas **[ASUNCIÓN del intake §17.3.P.6]**. Es el número más alto del producto | Informe del pipeline acotado a los **dos motores**, **no un caso de prueba**. Gate `QG-06`, condicionado | Recolector de cobertura con alcance acotado | `Pendiente` |
| Tolerancia de comparación de valores | **0.01** absoluta con operador **estricto**. **No es asunción**: sale de que el emisor redondea a 2 decimales | `TC-09`, que debe dar **exactamente 2** advertencias y no 3 | Caso de prueba del escenario `E-1` | `Pendiente` |
| Casos de la batería del validador que pasan | **10 de 10**, con los **ocho** escenarios como entrada | `TC-01` a `TC-10`, contra la tabla de §6 | Etapa `test` del pipeline. Gate `QG-03` | `Pendiente` |
| Peticiones de red originadas por los dos motores | Exactamente **0** | `TC-14` | Inspección de dependencias de los dos motores | `Pendiente` |
| Aplicación de transformaciones sobre almacén inexistente | **1 de 1** intento exitoso, sin paso manual | `TC-32` | Etapa de verificación de transformaciones del pipeline. Gate `QG-04` | `Pendiente` |
| Provisorias iguales en dos producciones consecutivas | Exactamente **0**, sobre la misma cuenta y entre cuentas distintas | `TC-27` | Prueba que produce dos provisorias y compara, y prueba de no derivabilidad | `Pendiente` |
| Componentes de pieza y texto original en una consulta de listado | Exactamente **0** y **0** | `TC-19` | Inspección de la proyección devuelta | `Pendiente` |
| Escrituras que reemplazan el texto original conservado | Exactamente **0** aceptadas | `TC-16` | Prueba que materializa un trabajo existente con un texto distinto | `Pendiente` |
| Retiros parciales tras una baja interrumpida | Exactamente **0** | `TC-21` | Prueba de baja **con el almacén interrumpido a mitad de operación** | `Pendiente` |
| Mensajes y trazas con un secreto, la ruta del almacén o el texto del alumno | Exactamente **0** | `TC-35` | Prueba de inspección sobre las 17 condiciones **y sobre el registro del servidor**, en las dos direcciones | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **17** alcanzadas, y **0** emitidas fuera del catálogo | `TC-34` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los tres valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para las dos coberturas y `A-5` para los 200 ms— y `PA-11` de `05` §11 los registra. Hasta entonces sus gates son **condicionados**.

**La tolerancia de 0.01 no lleva rótulo y no es condicionada.** El intake §22 la enumera expresamente entre «lo que NO es asunción». Arrastrarla al tratamiento condicionado sería un error de lectura.

**Tres de los catorce NFR no tienen caso de prueba y es correcto que no lo tengan**: dos son informes de cobertura del pipeline y el tercero es la puerta de construcción.

## 4. Trazabilidad RN ↔ tests

Dieciséis filas, una por regla. El tramo de cada una es el que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 y `05` §10.2 le asignan **en esta capa**; esta matriz lo refleja y no lo redefine. Las reglas se enuncian en `GeometriaFactory-Domain`.

| RN | Tramo en esta capa | Tests | Estado |
| --- | --- | --- | --- |
| RN-01 Administrador único y papeles fijos | La restricción de unicidad del almacén, y el transporte del papel en el acceso **sin decidir qué habilita** | `TC-22`, `TC-23`, `TC-29` | `Pendiente` |
| RN-02 El correo del alumno es único | La **segunda línea** de la unicidad, con el motivo que la capa de aplicación ya declara recibir por esta vía | `TC-22`, `TC-23` | `Pendiente` |
| RN-03 Trabajo ajeno indistinguible de inexistente | **De forma negativa**: la consulta sin recorte declarado **no se resuelve**. Esta capa no comprueba pertenencia | `TC-18` | `Pendiente` |
| RN-04 Eliminación acotada al borrador | **La mitad de borrado físico**. La acotación por estado y por papel es de la capa de aplicación | `TC-20` | `Pendiente` |
| RN-05 No se pasa a estado `Pendiente` con errores de validación | **Produce el insumo**: la especie de cada observación. **El estado lo resuelve el dominio** | `TC-08`, `TC-09`, `TC-10` | `Pendiente` |
| RN-06 Cuenta `Pendiente` o `Bloqueado` sin acceso | **Sin tramo acá.** La admisibilidad se resuelve antes y una cuenta no admitida **no llega** a la emisión. Lo que sí se verifica es que la capa **guarda el estado como dato y no lo comprueba** | `TC-24` | `Pendiente` |
| RN-07 Baja con arrastre y confirmación escrita | **La mitad de arrastre**, con el todo o nada. La confirmación escrita es de la capa de aplicación | `TC-21` | `Pendiente` |
| RN-08 Texto original conservado íntegro | **Tramo principal acá**: el motor no lo devuelve corregido y el adaptador **rechaza toda escritura que lo reemplace** | `TC-16`, `TC-05` | `Pendiente` |
| RN-09 Observación de error con posición y campo | **Tramo principal acá**: el mensaje ubicado, la posición reservada de la figura no reconstruida y la advertencia con sus dos valores | `TC-05`, `TC-08`, `TC-10`, `TC-12` | `Pendiente` |
| RN-10 Desenlace exclusivo del administrador y terminalidad | **Sin tramo acá.** Esta capa guarda el estado y el comentario; quién puede cambiarlo lo deciden el dominio y la capa de aplicación. Lo que sí se verifica es que el comentario se materializa **como campo y sin historial** | `TC-17` | `Pendiente` |
| RN-11 El administrador no ve los borradores | **De forma negativa**, igual que `RN-03`: el predicado llega en el pedido y el borrador **no viaja** | `TC-18` | `Pendiente` |
| RN-12 El reseteo conserva la cuenta y sus trabajos | Escribe la marca **sin tocar el estado ni los trabajos**, y **por contraste**: el reseteo **no pasa por el retiro** | `TC-24`, `TC-20` | `Pendiente` |
| RN-13 Cambio forzado antes de toda otra capacidad | **Conserva la marca y la hace viajar.** Sin ese dato, la comprobación transversal de la capa de aplicación no tendría sobre qué decidir. **La comprobación no es de acá** | `TC-24` | `Pendiente` |
| **RN-14 La provisoria la produce el sistema** | **Tramo principal, y único, acá.** `GeometriaFactory-Application` declara que es la única de las dieciséis **sin tramo en su capa**, y `RN-14` nombra a este proyecto de código como el lugar de la generación | `TC-27`, `TC-28` | `Pendiente` |
| RN-15 Resetear no exige cuenta habilitada | **De forma estructural**: la invocación **no recibe** el estado de la cuenta, de modo que **no puede comprobarlo**; y la marca se escribe sobre los tres estados sin alterarlos | `TC-24`, `TC-27` | `Pendiente` |
| RN-16 Habilitar produce la provisoria | **Produce el valor también para la habilitación**: mismo mecanismo y mismo valor que para el reseteo, y la invocación **no lleva ningún dato del acto que la motiva**, de modo que no puede distinguirlos | `TC-24`, `TC-27` | `Pendiente` |

**Catorce de las dieciséis con tramo acá y dos sin él, y el recuento cierra en dieciséis**, que es exactamente el reparto que `02` §6 declara. **Las dos sin tramo —`RN-06` y `RN-10`— tienen caso de prueba igual**, y lo que verifican es una afirmación distinta: que esta capa **guarda el dato y no lo comprueba**. Es la misma clase de verificación que `GeometriaFactory-Application` le dio a `RN-14` desde el otro lado.

**Tres reglas tienen su tramo principal acá —`RN-08`, `RN-09` y `RN-14`—**, y `02` §6 declara la consecuencia: si acá se hacen mal, **ninguna capa de más adentro puede repararlas**. Sus casos de prueba son los que `Plan-Pruebas.md` §4 trata como riesgo de impacto más alto.

## 5. Trazabilidad regla conceptual de modelo ↔ tests

Siete filas, una por regla conceptual de [`../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/`](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/). **No compiten con las reglas de negocio**: una regla conceptual de modelo declara **cómo el dato sobrevive**, no qué decidió el negocio.

| RC | Qué exige, en una línea | Tests | Estado |
| --- | --- | --- | --- |
| [RC-01](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-01-Texto-Original-Escrito-Una-Sola-Vez.md) | El texto original se escribe **una sola vez** y ninguna escritura posterior lo reemplaza | `TC-16` | `Pendiente` |
| [RC-02](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-02-Identidad-Posicional-De-La-Pieza.md) | La identidad de la pieza es su **posición**, y la de una figura no reconstruida **queda reservada** | `TC-12` | `Pendiente` |
| [RC-03](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-03-Valor-Declarado-Y-Derivado-Por-Separado.md) | El valor **declarado** y el **derivado** se guardan por separado, para no tener que recalcular en cada consulta | `TC-05`, `TC-17` | `Pendiente` |
| [RC-04](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-04-La-Familia-No-Se-Persiste.md) | La familia plana o volumétrica **no se persiste**: se deriva del tipo | `TC-17`, `TC-19` | `Pendiente` |
| [RC-05](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-05-Retiro-Fisico-Con-Arrastre.md) | El retiro es **físico y con arrastre**, todo o nada, sin marca lógica | `TC-20`, `TC-21` | `Pendiente` |
| [RC-06](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-06-Tres-Sellos-De-Tiempo-Distintos.md) | Los **tres** sellos de tiempo del trabajo son distintos y no se confunden: la fecha que el alumno declara y los dos que registra el sistema | `TC-17`, `TC-31` | `Pendiente` |
| [RC-07](../02-Especificacion-Funcional/Modelo-Datos/reglas-conceptuales-de-modelo/RC-07-La-Marca-No-Es-Un-Estado-De-Cuenta.md) | **La marca no es un estado de cuenta**: no ocupa su lugar ni lo reemplaza | `TC-24` | `Pendiente` |

**Siete de siete con caso de prueba.** `RC-01`, `RC-05` y `RC-07` son las tres que sostienen directamente un NFR con umbral cero, y por eso sus casos son también los que `QG-11` y `QG-10` miden.

## 6. La batería del validador contra los escenarios

Es la tabla de `05` §10.5 con la columna del caso de prueba que la materializa. **Diez filas, ninguna agrupada.** El detalle de cada escenario está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §6.

| # | Caso de la batería | Escenario | Caso de prueba | Estado |
| --- | --- | --- | --- | --- |
| 1 | Ortoedro con clave sinónima (`T1`) | `E-2` | `TC-01` | `Pendiente` |
| 2 | Texto con comas finales (`T2`) | `E-2` | `TC-02` | `Pendiente` |
| 3 | Cubo con caras `Cuadrado` (`T3`) | `E-3` | `TC-03` | `Pendiente` |
| 4 | Cubo con caras `Rectangulo` (`T3`) | `E-4` | `TC-04` | `Pendiente` |
| 5 | Área del cubo declarada contra derivada | `E-3` | `TC-05` | `Pendiente` |
| 6 | Volumen del ortoedro declarado contra derivado | `E-2`, `E-1` | `TC-06` | `Pendiente` |
| 7 | Dimensión en `0` que no descarta la figura | `E-6` | `TC-07` | `Pendiente` |
| 8 | Tipo desconocido con posición y campo | `E-5` | `TC-08` | `Pendiente` |
| 9 | Texto semilla completo | `E-1` | `TC-09` | `Pendiente` |
| 10 | Dimensión no legible | `E-8` | `TC-10` | `Pendiente` |

**Diez de diez, y siete de los ocho escenarios representados.** El octavo, `E-7`, no respalda ninguno de los diez y se ejercita igual en `TC-11` y `TC-19` como **cobertura adicional declarada** por `05` §10.5.

## 7. Cobertura por capa

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

**El umbral global de 85 / 80 y el de 95 de los dos motores vienen rotulados [ASUNCIÓN] desde el intake §17.3.P.6.** El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`.

**El adaptador de reloj no lleva mutation score, y se declara por qué**: un umbral de mutación sobre una operación de una línea no aporta información. Es la única exención, y no se extiende a ningún otro componente.

## 8. Huecos identificados

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **El intake escribe «nueve pruebas del validador» en dos gates** —§17.3.P.8 y §17.5.P.8— **y la batería tiene diez** | Un lector del gate podría dar la puerta por cumplida con nueve casos, dejando `E-8` sin cubrir, que es justamente el escenario que cerró la única condición del contrato de fachada sin dato de prueba | El Product Owner sobre su propio documento. **Mientras tanto esta categoría aplica diez**, siguiendo `05` §8 y §10.5, y no baja la batería para que coincida con la redacción. Ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los siete componentes con umbral de mutación | Elección y anclaje junto con el resto del tooling de la etapa `a`; hasta que corra, se reporta «sin medir» y no bloquea |
| **Los tres valores rotulados [ASUNCIÓN]** —las dos coberturas y los 200 ms— siguen sin confirmar | Los gates `QG-05`, `QG-06` y `QG-14` son condicionados y no bloquean la fusión | `PA-11` de `05` §11, antes de fijar la puerta de cobertura en `09-Devops` |
| **Cuál de las dos funciones de derivación de clave se ancla** no está decidido (`05` §11 `PA-03`) | `TC-25` y `TC-26` verifican **la forma** —parámetros versionados junto al valor derivado, sin valor por defecto silencioso— y no la función concreta. Los casos de prueba no cambian con la elección; los valores esperados de las pruebas de derivación sí | El equipo en la etapa `a`, aplicando el criterio que la ADR correspondiente declara |
| **Hasta dónde llega el conjunto de tipos reconstruibles** no está enumerado por ninguna fuente (`05` §11 `PA-04`) | `TC-11` verifica los **seis** que los escenarios ejercitan. Un séptimo tipo produciría error de validación, que es correcto **pero puede no ser lo deseado** | El Product Owner, con la enumeración de las clases de la actividad. **No se agrega ningún tipo acá**, porque ninguna fuente lo enumera |
| **Cómo se sostiene que la provisoria «no se repite»** (`05` §11 `PA-06`) | `TC-27` verifica **impredecibilidad y no repetición observada en dos producciones**; verificarla contra un registro de provisorias anteriores exigiría conservarlas, y el producto no guarda contraseñas en claro. Esta categoría **hereda la lectura y no la reabre** | El Product Owner, para confirmarla o reemplazarla |
| **Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva** | Este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tiene categoría 10 emitida | `Rules-Calidad-Y-Pruebas.md` §2.1 omite la matriz para ese caso. Ver [`README.md`](README.md) §3 |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**diez** filas de caso de uso con sus **veinticinco** historias, **catorce** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, más dos propias: **siete** reglas conceptuales de modelo y los **diez** casos de la batería del validador contra su escenario. Refleja el reparto de `02` §6 —catorce reglas con tramo acá y dos sin él, con caso de prueba igual para verificar que esta capa **guarda el dato y no lo comprueba**— y declara las **tres** cuyo tramo principal vive acá. Declara la cobertura por los **ocho** componentes con «Sin medir» en lugar de cero, con el piso propio de **95 %** del validador y con la única exención de mutation score justificada. Cita los tres valores rotulados **[ASUNCIÓN]** con su rótulo y separa la **tolerancia de 0.01**, que el intake §22 excluye expresamente de las asunciones. Declara **siete** huecos, el primero de ellos la divergencia entre los dos gates del intake que escriben «nueve» y la batería de **diez**. |
