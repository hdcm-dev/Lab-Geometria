# Estrategia de testing — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Estrategia-Testing.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §3 y §4; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §4 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §17.2.P.6, §20 (los **ocho** escenarios `E-1` a `E-8`), §21 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md); `09-Devops` y `11-Documentacion`

---

## Tabla de contenido

- [1. Pirámide de testing deseada](#1-pirámide-de-testing-deseada)
- [2. Cobertura mínima por capa](#2-cobertura-mínima-por-capa)
- [3. Tooling](#3-tooling)
- [4. Especificaciones Given-When-Then](#4-especificaciones-given-when-then)
- [5. Mocks y fixtures](#5-mocks-y-fixtures)
- [6. Datos de prueba](#6-datos-de-prueba)
- [7. Ambiente de testing](#7-ambiente-de-testing)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Pirámide de testing deseada

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código **se aparta del reparto y declara el motivo**, que no es de esta categoría sino del intake: §17.2.P.6 declara «pirámide del proyecto de código: **100 % unitarias**; la integración vive en `GeometriaFactory.Integration.Tests`, que pertenece a la Api».

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Los once casos de uso enteros, con **dobles de los cuatro puertos**, más las cuatro comprobaciones de autorización y las pruebas de inspección estructural | **100 %** | Lo declara el intake §17.2.P.6. La inversión de dependencias existe precisamente para que un caso de uso entero sea unitario: no hay nada en esta capa que exija un ambiente |
| Integración | — | **0 %** | **No aplica acá y se declara así en lugar de omitirse.** La batería de integración del producto existe y golpea la API real contra el almacén real, pero es de `GeometriaFactory-Api` (intake §17.2.P.6 y §17.5.P.6). Una prueba de esta capa que abriera el almacén violaría `QG-04` |
| E2E y snapshot | — | **0 %** | El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz (`05` §4 y §5). Un recorrido de punta a punta del producto pasa por `GeometriaFactory-Api` y `GeometriaFactory-Web`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los veinte puntos que la regla asigna a integración, extremo a extremo y snapshot **no se descartan: se reasignan a otro proyecto de código**, que es donde la fuente los pone. El piso unitario **sube** de 80 a 100, de modo que no se baja ninguna exigencia y no hace falta la ADR que §2.2 exige para bajar cobertura.

**Contra la pirámide invertida**: acá sería imposible construirla, porque una prueba de extremo a extremo de esta capa no existe sin salir de ella. **Contra la pirámide aplanada** —un número global de cobertura sin distinguir capas— la defensa es §2 de este documento, que reporta por componente y nunca como número único.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte**, porque no ejercen un caso de uso sino que revisan el proyecto de código sobre sí mismo:

- **Prueba de inspección.** Comprueba una propiedad estructural: cero pruebas que abren el almacén real, una sola dependencia saliente, el conjunto de códigos emitidos contra el catálogo, ninguna consulta de listado que materialice componentes. Se cuentan dentro del nivel unitario porque corren en el mismo ejecutor y con el mismo costo.
- **Prueba de orden.** Una sola, `TC-11`: verifica que la cuarta comprobación corta antes que las otras tres. `05` §8 la exige como NFR con umbral **1**, y es la única prueba del proyecto de código cuyo objeto es el orden entre comprobaciones y no su resultado.

## 2. Cobertura mínima por capa

La partición no es en capas de despliegue —no las hay— sino en los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1. El piso global lo fija el intake §17.2.P.6 y es **85 % de líneas y 80 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`].

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Guarda de autorización | 100 % | 100 % | 60 % | Sube sobre el piso: es el **único** componente donde se cierran `INV-02`, `INV-03` e `INV-09`, y `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que saltee la cuarta comprobación. Una rama sin cubrir acá es una guarda que nadie ejerce |
| Declaración de puertos | **No aplica** | **No aplica** | **No aplica** | Son declaraciones, no lógica: no tienen líneas ejecutables que cubrir. Se verifican por su **uso** en los once casos de uso y por `TC-27`. Declarar un umbral acá sería declarar una medición sin sujeto |
| Orquestación del alta de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene los dos caminos de alta con estados iniciales opuestos, que ya produjeron un defecto de fusión corregido en la categoría 02 |
| Orquestación del gobierno de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: contiene el arrastre de la baja —caso testigo de la unidad de trabajo— y el reseteo, que pone la marca |
| Orquestación del ingreso y la credencial | 95 % | 90 % | 60 % | Sube sobre el piso: es el único lugar donde la marca **se levanta** (`CU-03` FA-05), y donde la admisibilidad devuelve sus motivos sin colapsarlos |
| Orquestación del trabajo | 85 % | 80 % | 60 % | Piso del intake §17.2.P.6 |
| Orquestación de la consulta | 85 % | 80 % | 60 % | Piso del intake |
| Orquestación del desenlace | 85 % | 80 % | 60 % | Piso del intake |
| **Proyecto de código completo** | **85 %** | **80 %** | **60 %** | Intake §17.2.P.6 [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 85/80 global es del intake y viene rotulado **[ASUNCIÓN]**: es el valor que el Product Owner tiene pendiente de confirmar. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los cuatro valores por encima del piso —100, 90, 90 y 95— los sube esta categoría con el fundamento declarado en la columna, que es lo que §2.2 admite («los porcentajes son piso, no techo»).

**La cobertura no se reporta como número global único.** El informe de la etapa `test` se emite por componente, y un 85 % global con la guarda de autorización en 70 % es un incumplimiento aunque el promedio cierre.

## 3. Tooling

Se nombran por función y no por producto, que es la convención que las categorías 03 y 05 de este proyecto de código ya siguen. La elección concreta y su anclaje de versión son de la etapa `a` (intake, encabezado de la Parte C: regla de anclaje de versiones).

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Dobles de los cuatro puertos | **Dobles escritos a mano o con marco de dobles, indistintamente.** Lo que sí se fija es que son **dobles de puerto** y nunca de un componente interno: la frontera que se sustituye es la que declara [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), ver §5 |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 |
| Medición del tiempo del caso de uso más pesado | Cronometrado dentro de la batería unitaria, con doble del puerto de validación y **sin acceso a base**, según `BT-19` |
| Inspección estructural | El propio marco de pruebas, leyendo el archivo de proyecto, el conjunto de códigos emitidos y la proyección devuelta por las consultas |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a` y el nombre no cambia nada de esta estrategia.

## 4. Especificaciones Given-When-Then

**Los criterios de aceptación de las treinta y dos historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos dos escenarios, uno de camino feliz y uno de borde ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio, que es el defecto que este corpus tiene documentado como el que más veces volvió.

**Dónde sí se usan pruebas basadas en propiedades**, que son la otra forma de especificación de esta estrategia:

| Propiedad | Enunciado |
| --- | --- |
| Terminación controlada | Para todo caso de uso y todo estado inicial admisible, o el efecto se aplica entero o el estado queda como estaba y se devuelve la condición (`05` §4) |
| Conjunto cerrado de condiciones | Para toda invocación que rechaza, el código devuelto pertenece a las **36** condiciones del catálogo |
| Indistinguibilidad | Para todo trabajo ajeno y todo identificador inexistente, el motivo emitido es el mismo (`RN-03`, `INV-02`) |
| Precedencia de la cuarta comprobación | Para toda cuenta con la marca puesta y todo caso de uso salvo el reemplazo de `CU-03` FA-05, el motivo emitido es `CAMBIO_DE_CONTRASENA_PENDIENTE` **cualquiera sea** el resultado de las otras tres comprobaciones |

## 5. Mocks y fixtures

**Política de dobles: sólo de puerto, y de ningún otro lugar.** Los **cuatro** puertos de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 son la única frontera que una prueba sustituye. Un doble de un componente interno —de la guarda, de un orquestador— es un hallazgo de revisión: rompe la propiedad de que el caso de uso se ejerce **entero**, que es lo que el intake §17.2.P.6 pide probar.

Los cuatro dobles, con lo que cada uno tiene que poder simular:

| Doble de puerto | Qué tiene que poder simular |
| --- | --- |
| Repositorio de trabajos | Trabajo existente propio, existente ajeno, inexistente, y trabajos en los cuatro estados; una consulta ya acotada; el retiro con arrastre; y la indisponibilidad |
| Validación de figuras | Los resultados de interpretación de los ocho escenarios del intake §20 —piezas, observaciones y **la cantidad de figuras del conjunto raíz**— y la **indisponibilidad**, que `US-16` exige |
| Reloj del sistema | Un momento fijo, elegido por la prueba, y dos momentos distintos consecutivos |
| Repositorio de cuentas | Cuenta en cada uno de sus tres estados, con y sin la marca; correo ya registrado y no registrado; administrador existente y ausente; y la materialización con la marca |

Fixtures compartidos, todos como **constructores**:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Solicitante en sus cuatro formas | Alumno sin marca, alumno con marca, administrador sin marca, administrador con marca | Es la entrada de las cuatro comprobaciones y aparece en los once casos de uso |
| Cuenta de alumno en cada uno de sus tres estados | `Pendiente`, `Habilitado`, `Bloqueado`, con y sin la marca | Seis combinaciones que aparecen en `CU-01`, `CU-02`, `CU-03` y `CU-11` |
| Trabajo en cada uno de sus cuatro estados | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, propio y ajeno | El alcance, la pertenencia y la terminalidad se prueban contra los ocho pares |
| Resultados de interpretación de los escenarios del intake | Los conjuntos de piezas, observaciones y cantidad de figuras que corresponden a `E-1` a `E-8`, ver §6 | Es el material que hace comparables las pruebas de este proyecto de código con las de `GeometriaFactory-Infrastructure`, que es quien los produce de verdad |

**Regla de duplicación:** un caso de prueba que necesite una variante de un fixture la deriva del constructor compartido y no lo copia. Un segundo constructor equivalente es un hallazgo de revisión.

## 6. Datos de prueba

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado —`medido`, `derivado` o `reconstruido`—. §21 los cruza contra la batería obligatoria de **nueve** casos de prueba de RT §11, más un décimo que esa misma sección agrega.

**Cómo los usa este proyecto de código, que es la parte que hay que decir con precisión.** Esta capa **no interpreta el texto del alumno**: la interpretación es de `GeometriaFactory-Infrastructure` y llega por el puerto de validación de figuras ([`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3). De cada escenario, entonces, lo que entra acá **no es el texto sino el resultado que el doble del puerto devuelve**: piezas, observaciones y la cantidad de figuras del conjunto raíz. El texto original sí viaja íntegro por la capa, y eso es lo que `RN-08` exige verificar.

| Escenario | Qué aporta a las pruebas de este proyecto de código | Fuente del valor |
| --- | --- | --- |
| `E-1` | 3 piezas y 2 advertencias, sin errores. El envío **pasa a `Pendiente`**. Es además el material del NFR de 500 ms | §20.E-1, «Qué verificar» puntos 5 y 6 |
| `E-2` | 1 pieza, 1 advertencia de volumen y ningún error. **Pasa a `Pendiente` con la advertencia asociada** | §20.E-2, puntos 4, 6 y 7 |
| `E-3` | Advertencia de área con el par declarado 36.00 y derivado 54.00, que el mensaje debe expresar entero. **El trabajo no se rechaza** | §20.E-3, puntos 2 y 4 |
| `E-4` | **Cero observaciones en total.** Es el criterio negativo: el envío pasa a `Pendiente` sin ninguna observación que incorporar | §20.E-4, punto 4 |
| `E-5` | Observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**; la primera pieza, válida, se interpreta igual. El trabajo **queda en `Borrador`** con su texto conservado | §20.E-5, puntos 1 a 4 |
| `E-6` | Una figura que **se interpreta** y produce a lo sumo una advertencia; el trabajo pasa a `Pendiente` | §20.E-6, puntos 1 a 3 |
| `E-7` | Conjunto de 6 piezas que cubre los seis tipos dibujables. Ejercita el detalle con piezas y componentes de `US-19` frente al listado sin componentes | §20.E-7, puntos 1 y 3 |
| `E-8` | **El desenlace del envío es error, no advertencia** [DECISIÓN 2026-08-09]: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige `RN-09` | §20.E-8, punto 5 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un fixture de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización. Si el intake cambia un escenario, el cambio baja acá como una corrección con su fila de control de cambios.

**Lo que no se inventa.** Ningún caso de prueba de este proyecto de código introduce un resultado de interpretación que no corresponda a un escenario de §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un identificador de trabajo, un momento— se usa un valor evidentemente ficticio y se declara como tal en el `TC-XX`: son datos de identidad y de orquestación, no datos de geometría, y el intake no los fija.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake, encabezado de la Parte C, y §17.2.P.9) |
| Aislamiento entre pruebas | Total y por construcción: no hay estado compartido entre invocaciones, no hay caché y no hay registro estático (`05` §4). Ninguna prueba depende del orden de ejecución |
| Paralelismo | Admitido. `05` §4 declara que la batería puede correr en paralelo porque ninguna prueba comparte estado ni base |
| Base de datos | **Ninguna, y el umbral es exactamente 0.** `tiene_persistencia` es false y el intake §17.2.P.8 declara la puerta propia: una prueba de esta capa que abra el almacén real **está mal ubicada** |
| Variables de entorno y secretos | **Ninguno.** El proyecto de código no lee configuración (`05` §7) y la contraseña llega ya derivada, la provisoria ya producida y ya derivada |
| Reloj | **No se fija ni se simula el reloj del entorno.** El momento entra por el puerto de reloj, de modo que la prueba lo elige. Es lo que el intake §17.2.P.11 punto 3 declara que el puerto existe para permitir |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** El único tiempo que este proyecto de código tiene declarado es el del caso de uso más pesado —**500 ms** sobre `E-1`, [ASUNCIÓN del intake §17.2.P.10]—, que es una medición por caso de uso y no de la suite. Ninguna fuente da un tiempo de suite para esta capa, y no se inventa uno |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo con su apartamiento del reparto de `Rules-Calidad-Y-Pruebas.md` §2.2 y el motivo —el intake §17.2.P.6 declara 100 % unitarias y ubica la integración en `GeometriaFactory-Api`—, la cobertura mínima por los **ocho** componentes de `05` §3.1 con el origen de cada número separado y con la declaración de que el componente de puertos no tiene umbral porque no tiene líneas que cubrir, el tooling nombrado por función, la decisión de no adoptar archivos de escenario ejecutables, la política de dobles **sólo de puerto** con los cuatro dobles y lo que cada uno simula, los cuatro fixtures compartidos, el uso de los **ocho** escenarios reales del intake §20 —con la precisión de que a esta capa le entra el resultado que el doble devuelve y no el texto— y el ambiente de testing, incluida la constancia de que no se declara ningún tiempo de ejecución de suite que ninguna fuente dé. |
