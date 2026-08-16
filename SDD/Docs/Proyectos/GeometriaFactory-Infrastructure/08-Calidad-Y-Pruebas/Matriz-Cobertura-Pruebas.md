# Matriz de cobertura de pruebas — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Matriz-Cobertura-Pruebas.md
**Versión:** 1.2
**Estado:** Aprobado
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

Catorce filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| Tiempo de interpretación del texto semilla | Menos de **200 ms** para el texto de **3** piezas de `E-1`, **sin almacén** **[ASUNCIÓN del intake §17.3.P.10]** | `TC-06015`. Gate `QG-14`, condicionado | Cronometrado dentro de la batería unitaria, sin abrir el almacén | `Pendiente` |
| Cobertura del proyecto de código | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.3.P.6]** | Informe del pipeline, **no un caso de prueba**. Gate `QG-05`, condicionado | Recolector de cobertura, con informe por componente | `Pendiente` |
| Cobertura del validador de figuras | **95 %** de líneas **[ASUNCIÓN del intake §17.3.P.6]**. Es el número más alto del producto | Informe del pipeline acotado a los **dos motores**, **no un caso de prueba**. Gate `QG-06`, condicionado | Recolector de cobertura con alcance acotado | `Pendiente` |
| Tolerancia de comparación de valores | **0.01** absoluta con operador **estricto**. **No es asunción**: sale de que el emisor redondea a 2 decimales | `TC-06009`, que debe dar **exactamente 2** advertencias y no 3 | Caso de prueba del escenario `E-1` | `Pendiente` |
| Casos de la batería del validador que pasan | **10 de 10**, con los **ocho** escenarios como entrada | `TC-06001` a `TC-06010`, contra la tabla de §6 | Etapa `test` del pipeline. Gate `QG-03` | `Pendiente` |
| Peticiones de red originadas por los dos motores | Exactamente **0** | `TC-06014` | Inspección de dependencias de los dos motores | `Pendiente` |
| Aplicación de transformaciones sobre almacén inexistente | **1 de 1** intento exitoso, sin paso manual | `TC-06032` | Etapa de verificación de transformaciones del pipeline. Gate `QG-04` | `Pendiente` |
| Provisorias iguales en dos producciones consecutivas | Exactamente **0**, sobre la misma cuenta y entre cuentas distintas | `TC-06027` | Prueba que produce dos provisorias y compara, y prueba de no derivabilidad | `Pendiente` |
| Componentes de pieza y texto original en una consulta de listado | Exactamente **0** y **0** | `TC-06019` | Inspección de la proyección devuelta | `Pendiente` |
| Escrituras que reemplazan el texto original conservado | Exactamente **0** aceptadas | `TC-06016` | Prueba que materializa un trabajo existente con un texto distinto | `Pendiente` |
| Retiros parciales tras una baja interrumpida | Exactamente **0** | `TC-06021` | Prueba de baja **con el almacén interrumpido a mitad de operación** | `Pendiente` |
| Mensajes y trazas con un secreto, la ruta del almacén o el texto del alumno | Exactamente **0** | `TC-06035` | Prueba de inspección sobre las 17 condiciones **y sobre el registro del servidor**, en las dos direcciones | `Pendiente` |
| Cobertura del catálogo de condiciones | **100 %** de las **17** alcanzadas, y **0** emitidas fuera del catálogo | `TC-06034` | Prueba de inspección que compara los dos conjuntos en las dos direcciones | `Pendiente` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-01`, **no un caso de prueba** | Etapa `build` del pipeline | `Pendiente` |

**Los tres valores rotulados [ASUNCIÓN] se citan con su rótulo y no se convierten en compromiso.** Su confirmación está pendiente del Product Owner en el intake §22 —asunción `A-3` para las dos coberturas y `A-5` para los 200 ms— y `PA-11` de `05` §11 los registra. Hasta entonces sus gates son **condicionados**.

**La tolerancia de 0.01 no lleva rótulo y no es condicionada.** El intake §22 la enumera expresamente entre «lo que NO es asunción». Arrastrarla al tratamiento condicionado sería un error de lectura.

**Tres de los catorce NFR no tienen caso de prueba y es correcto que no lo tengan**: dos son informes de cobertura del pipeline y el tercero es la puerta de construcción.

## 4. Trazabilidad RN ↔ tests

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

## 5. Trazabilidad regla conceptual de modelo ↔ tests

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

**Siete de siete con caso de prueba.** `RC-06001`, `RC-06005` y `RC-06007` son las tres que sostienen directamente un NFR con umbral cero, y por eso sus casos son también los que `QG-11` y `QG-10` miden.

## 6. La batería del validador contra los escenarios

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
| ~~**El intake escribía «nueve pruebas del validador» en dos gates** —§17.3.P.8 y §17.5.P.8— **y la batería tiene diez**~~ **CERRADO** | Un lector del gate podía dar la puerta por cumplida con nueve casos, dejando `E-8` sin cubrir, que es justamente el escenario que cerró la única condición del contrato de fachada sin dato de prueba | **Cerrado por el intake 1.20**, que corrigió los cinco lugares que decían nueve —los dos gates, §17.3.P.6, §17.2.P.11 y el encabezado de §21— sobre el hallazgo que levantó esta categoría. Ya no hay nada derivado al Product Owner por este motivo. Esta categoría aplicó **diez** desde su emisión, siguiendo `05` §8 y §10.5, y no bajó la batería para que coincidiera con la redacción. Ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 |
| **La medición de mutación no está en el pipeline** y su herramienta no está elegida | El umbral de 60 % no se puede exigir todavía en ninguno de los siete componentes con umbral de mutación | Elección y anclaje junto con el resto del tooling de la etapa `a`; hasta que corra, se reporta «sin medir» y no bloquea |
| **Los tres valores rotulados [ASUNCIÓN]** —las dos coberturas y los 200 ms— siguen sin confirmar | Los gates `QG-05`, `QG-06` y `QG-14` son condicionados y no bloquean la fusión | `PA-11` de `05` §11, antes de fijar la puerta de cobertura en `09-Devops` |
| **Cuál de las dos funciones de derivación de clave se ancla** no está decidido (`05` §11 `PA-03`) | `TC-06025` y `TC-06026` verifican **la forma** —parámetros versionados junto al valor derivado, sin valor por defecto silencioso— y no la función concreta. Los casos de prueba no cambian con la elección; los valores esperados de las pruebas de derivación sí | El equipo en la etapa `a`, aplicando el criterio que la ADR correspondiente declara |
| **Hasta dónde llega el conjunto de tipos reconstruibles** no está enumerado por ninguna fuente (`05` §11 `PA-04`) | `TC-06011` verifica los **seis** que los escenarios ejercitan. Un séptimo tipo produciría error de validación, que es correcto **pero puede no ser lo deseado** | El Product Owner, con la enumeración de las clases de la actividad. **No se agrega ningún tipo acá**, porque ninguna fuente lo enumera |
| **Cómo se sostiene que la provisoria «no se repite»** (`05` §11 `PA-06`) | `TC-06027` verifica **impredecibilidad y no repetición observada en dos producciones**; verificarla contra un registro de provisorias anteriores exigiría conservarlas, y el producto no guarda contraseñas en claro. Esta categoría **hereda la lectura y no la reabre** | El Product Owner, para confirmarla o reemplazarla |
| ~~**Ninguna fila `VER-XX` y ninguna matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque este proyecto de código no ejecutó la Fase B2 —`requiere_maqueta` es false— y no tenía categoría 10 emitida | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación, `VER-06001` a `VER-06003`, y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que declara **tres** filas, `SD-06001` a `SD-06003`, todas en `Sin verificar`. La matriz nace **sin ninguna fila de línea de base visual**, porque la Fase B2 sigue sin haberse ejecutado: es el caso de `Deriva-Rules.md` §2.3. La fila se conserva con su desenlace en lugar de retirarse |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`** declarado en §8. Se emitió [`../10-Examples/`](../10-Examples/) con **tres** contratos de verificación y con ellos [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, con **tres** filas en `Sin verificar`. La fila del hueco se **conserva** con su desenlace y su fecha. **Ninguna de las cinco tablas de cobertura cambia**: las sondas no sustituyen a ningún caso de prueba, y §5 de la propia matriz de sensado declara que **`SD-06001` no reemplaza a los diez casos de la batería obligatoria**. Siguen siendo **siete** huecos, ahora dos de ellos cerrados. |
| 1.1 | 2026-08-11 | **`H-01`.** El primer hueco de §8 estaba **abierto con remediación pendiente del Product Owner** sobre algo que el Product Owner ya resolvió en el intake **1.20**. La fila **se conserva** —para no dejar hueco de numeración— y queda **cerrada** con su desenlace, incluidos los cinco lugares que la fuente corrigió. Siguen siendo **siete** huecos y ninguna cobertura cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara las tres tablas obligatorias —**diez** filas de caso de uso con sus **veinticinco** historias, **catorce** de NFR y **dieciséis** de regla de negocio, ninguna agrupada—, más dos propias: **siete** reglas conceptuales de modelo y los **diez** casos de la batería del validador contra su escenario. Refleja el reparto de `02` §6 —catorce reglas con tramo acá y dos sin él, con caso de prueba igual para verificar que esta capa **guarda el dato y no lo comprueba**— y declara las **tres** cuyo tramo principal vive acá. Declara la cobertura por los **ocho** componentes con «Sin medir» en lugar de cero, con el piso propio de **95 %** del validador y con la única exención de mutation score justificada. Cita los tres valores rotulados **[ASUNCIÓN]** con su rótulo y separa la **tolerancia de 0.01**, que el intake §22 excluye expresamente de las asunciones. Declara **siete** huecos, el primero de ellos la divergencia entre los dos gates del intake que escriben «nueve» y la batería de **diez**. |
