# Estrategia de testing — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Estrategia-Testing.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §3.4, §4 y §8; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §17.5.P.6, §17.5.P.8, §20 (los **ocho** escenarios `E-1` a `E-8`), §21 y §22
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

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `rest-api` la distribución **70 / 20 / 10** entre unitario, integración y extremo a extremo, con **100 % de endpoints cubiertos por contract test**. Este proyecto de código **se aparta del reparto, y el apartamiento no es de esta categoría**: el intake §17.5.P.6 declara **«60 % integración, 40 % unitarias, invertida respecto de lo habitual y a propósito, porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo»**.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Integración | **La batería de integración del producto**: golpea la superficie real **por su protocolo** contra el almacén real, con el proceso levantado en el ambiente de prueba | **60 %** [ASUNCIÓN del intake en cuanto al reparto] | Lo declara el intake §17.5.P.6. Un host delgado no tiene lógica propia que probar en aislamiento: lo que hay que verificar es **que el cable esté conectado**, y eso sólo se ve ejerciéndolo. Es además la batería a la que el intake §17.3.P.6 le asigna **la persistencia real** de `GeometriaFactory-Infrastructure` |
| Unit | La **traducción de motivos y códigos**, que es la única pieza con lógica propia; y las pruebas de inspección estructural | **40 %** [ASUNCIÓN en cuanto al reparto] | El traductor se puede recorrer entero sobre los **quince** códigos sin levantar el proceso, y hacerlo unitario lo vuelve barato de reejecutar. Las inspecciones —los cuatro puntos fuera de la guardia, los cuatro puertos conectados— también |
| Extremo a extremo con la persona | — | **0 %** | **No aplica acá y se declara así en lugar de omitirse.** El recorrido de una persona pasa por `GeometriaFactory-Web`, y su verificación es el guion de demostración de ese proyecto de código. Acá lo más cerca de un recorrido es la **colección de peticiones reproducible** de `CU-12`, que el intake declara como forma de demostración de este tipo de proyecto de código y que **no es una prueba automatizada** |

**El apartamiento invierte la pirámide y hay que decir qué se paga por eso.** Una batería mayoritariamente de integración es más lenta y más frágil que una unitaria, y su diagnóstico es más caro. Lo que la mantiene sana acá es que **las propiedades más peligrosas del proyecto de código no dependen de ejercer el cable sino de contarlo**: los cuatro puntos fuera de la guardia, los catorce códigos con destino, las tres familias indistinguibles, los cuatro puertos conectados y la única configuración de intercambio se verifican con **inspecciones de umbral exacto**, que son unitarias y baratas.

**Lo que la regla exige y acá se cumple con creces**: `Rules-Calidad-Y-Pruebas.md` §2.2 pide **100 % de endpoints cubiertos por contract test**. Acá los **quince** puntos de acceso se ejercen en la batería de integración, y además la tabla de traducción se recorre entera en las dos direcciones.

**Tres clases de verificación que conviene nombrar aparte:**

- **Prueba de integración por protocolo.** Levanta el proceso y golpea un punto de acceso con su verbo, su cuerpo y su cabecera de autorización, contra el almacén real.
- **Prueba de inspección con umbral exacto.** Recorre un conjunto cerrado —los quince puntos, los quince códigos, los cuatro puertos— y compara **en las dos direcciones**. Su umbral no admite gradación.
- **Verificación forzando la petición.** La que ejerce una acotación **sin pasar por la interfaz**. Es el único criterio de verificación del producto que la fuente exige ejercer así contra esta superficie, y es `TC-20`.

## 2. Cobertura mínima por capa

La partición es por los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1. El piso global lo fija el intake §17.5.P.6 y es **75 % de líneas y 70 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`]. **Es el piso más bajo del producto, y el motivo está declarado en la fuente**: este proyecto de código es cableado, y su valor se verifica ejerciéndolo y no cubriéndolo.

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Composición de raíz | 75 % | 70 % | — | Piso del intake. **Sin mutation score**: es declaración de cableado, y su verificación real es `TC-28`, que **falla en construcción** si un puerto queda sin adaptador |
| Guardia de admisión | **95 %** | **90 %** | 60 % | Sube muy por encima del piso: es donde se pierde `RN-13` sin que nada falle, y `05` §9 le asigna probabilidad **alta** por ser un defecto de **omisión** |
| Traductor de motivos y códigos | **95 %** | **90 %** | 60 % | Sube: es la única pieza con lógica propia, y es donde una decisión de adentro se deshace. **Ninguna capa de adentro puede reparar un error suyo** |
| Superficie de acceso y credencial propia | 80 % | 75 % | 60 % | Sube sobre el piso: son los **cuatro** puntos que se ejercen sin acceso firmado, que es exactamente el conjunto que `QG-05` acota |
| Superficie de gobierno de la comisión | 75 % | 70 % | 60 % | Piso del intake |
| Superficie de trabajos | 80 % | 75 % | 60 % | Sube sobre el piso: contiene el punto de eliminación, cuyo forzado es criterio bloqueante de la fuente, y el envío, donde el texto no se normaliza |
| Superficie de desenlace | 75 % | 70 % | 60 % | Piso del intake |
| Arranque y salud | 85 % | 80 % | 60 % | Sube sobre el piso: **0** peticiones atendidas con la preparación incompleta es un umbral que no admite ramas sin cubrir |
| **Proyecto de código completo** | **75 %** | **70 %** | **60 %** | Intake §17.5.P.6 [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 75/70 global es del intake y viene rotulado **[ASUNCIÓN]**. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija, y esta categoría lo adopta como tal. Los valores por encima del piso los sube esta categoría con el fundamento de la columna.

**Además de la cobertura de líneas hay una cobertura contable que no admite promedio**: **15 de 15** puntos de acceso ejercidos, **15 de 15** códigos del contrato recorridos, **4 de 4** puertos conectados y **4** puntos fuera de la guardia. Ésas se cumplen o no se cumplen.

## 3. Tooling

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a`.

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Integración por protocolo | Un **anfitrión de aplicación en memoria** que levanta el proceso real y permite golpearlo por su protocolo, contra el almacén real. Es lo que el intake §17.5.P.6 declara para `GeometriaFactory.Integration.Tests` |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Medición del percentil y del caudal | Un cliente de carga acotada, ejecutado dentro de la batería de integración, **midiendo en el servidor y no en el cliente** |
| Verificación forzando la petición | Un cliente de peticiones que arma la solicitud **sin pasar por la interfaz**, con la credencial de una sesión válida |
| Inspección estructural | El propio marco de pruebas, recorriendo la tabla de puntos de acceso, la tabla de traducción y el grafo de la composición de raíz |
| Colección de peticiones reproducible | Un archivo de colección versionado con el código, ejecutable a mano o por línea de órdenes. **No es una prueba automatizada**: es la forma de demostración que el intake declara |

**No se nombra ningún producto comercial.** La única decisión de herramienta que la fuente sí acota es la clase de anfitrión en memoria, y se la nombra por su función.

## 4. Especificaciones Given-When-Then

**Los criterios de aceptación de las treinta historias ya están escritos en Given/When/Then**, y la Definition of Ready lo exige.

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe citando la historia de origen.

**Y hay un motivo propio de este proyecto de código**: la **colección de peticiones reproducible** de `CU-12` ya es una enunciación ejecutable de un recorrido por la superficie. Un juego de archivos de escenario paralelo produciría **tres** enunciaciones del mismo recorrido —la historia, la colección y el escenario— y ninguna sería la fuente.

**Dónde sí se usan pruebas basadas en propiedades:**

| Propiedad | Enunciado |
| --- | --- |
| Cobertura de la guardia | Para todo punto de acceso de los quince, o está dentro de la guardia, o pertenece al conjunto declarado de **cuatro** exenciones. No hay tercera posibilidad |
| Indistinguibilidad | Para cada una de las tres familias empobrecidas, las dos respuestas comparadas son idénticas en cuerpo y en código de respuesta |
| Conjunto cerrado de códigos | Para toda respuesta de fallo, el código del contrato que lleva pertenece al conjunto cerrado de **quince**, y su código de respuesta es el que la tabla de traducción declara |
| Integridad del texto | Para todo texto enviado y aceptado, lo guardado es idéntico carácter por carácter; y para todo texto por encima del límite, **se rechaza y no se trunca** |

## 5. Mocks y fixtures

**Política de dobles: ninguno en la batería de integración.** Es la definición misma de esa batería: golpea la superficie real contra el almacén real. Doblar algo ahí la convertiría en otra cosa y dejaría al producto **sin ninguna verificación de que el cable está conectado**.

**Dónde sí se sustituye, y es la única sustitución admitida:**

| Sustitución | Cuándo | Por qué |
| --- | --- | --- |
| Resultados tipados de la capa de aplicación | Las pruebas unitarias del **traductor** | Recorrer los quince códigos por la superficie exigiría provocar quince estados del sistema; el traductor se verifica entero sobre el conjunto cerrado sin levantar el proceso |
| Preparación del almacén **que falla** | `TC-31` | Es la única forma de verificar que **no se atiende ninguna petición** con la preparación incompleta |
| Ausencia de un adaptador | `TC-28` | Verifica que la composición de raíz **falla en construcción** y no en la primera petición |

Fixtures compartidos:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Almacén efímero preparado, y uno inexistente | El ambiente de la batería de integración en sus dos estados | Todas las pruebas por protocolo lo necesitan, y ninguna debe heredar el estado de otra |
| Accesos firmados en sus formas | Válido de alumno, válido de administrador, **vencido**, **con firma ajena**, y de una cuenta **con la marca puesta** | Son las cinco entradas de la guardia de admisión |
| Cuentas y trabajos en sus estados | Cuentas en los tres estados con y sin la marca; trabajos en los cuatro estados, propios y ajenos | Los puntos de gobierno, de trabajos y de desenlace se ejercen contra los mismos |
| Los textos de los escenarios del intake | Los **ocho** textos literales de §20 | Ver §6 |

## 6. Datos de prueba

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** Los **ocho** escenarios `E-1` a `E-8` del intake §20 son datos salidos de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, con su procedencia y su estado declarado. §21 los cruza contra la batería obligatoria de nueve casos de la fuente técnica **más un décimo** que esa misma sección agrega.

**Cómo los usa este proyecto de código.** Acá los escenarios entran **como cuerpo de una petición**, que es la forma en que llegan de verdad desde el front. Lo que esta capa verifica sobre ellos **no es la interpretación** —que es de `GeometriaFactory-Infrastructure`— sino tres cosas propias del borde:

| Escenario | Qué verifica en esta capa | Fuente del valor |
| --- | --- | --- |
| `E-1` | Que el envío **responde con éxito** transportando el estado que la interpretación decidió, con sus **3 piezas y 2 advertencias** en el cuerpo; y que lo guardado es **idéntico carácter por carácter** a lo enviado | §20.E-1, «Qué verificar» puntos 5 y 6 |
| `E-2` | Que el texto **no se normaliza en el borde**: sus dos comas finales llegan intactas al almacén | §20.E-2, punto 1 |
| `E-5` | Que un envío cuyo texto **no verifica** responde con **éxito** y no con un código de fallo, transportando el estado `Borrador` y las observaciones en el cuerpo. Es el quinto riesgo de `05` §9 | §20.E-5, punto 4 |
| `E-8` | Lo mismo que `E-5`, con el otro modo de falla: **error y no advertencia**, con el trabajo en `Borrador` | §20.E-8, punto 5 |
| `E-3`, `E-4`, `E-6`, `E-7` | **La colección de peticiones reproducible y la batería del validador que corre desde acá.** No tienen verificación propia de borde: lo que ejercitan es la interpretación, que es de otra capa | §21; intake §17.5.P.8 |

**Lo que esta capa nunca hace con un escenario**: interpretarlo, normalizarlo, truncarlo o rechazarlo por su contenido geométrico. Un envío con errores de interpretación **es una respuesta exitosa**.

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**. Un dato de prueba de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización.

**Lo que no se inventa.** La **colección de peticiones reproducible** tiene como NFR **0 datos de prueba inventados** (`05` §8): sus cuerpos salen de los escenarios del intake y sus identidades son valores evidentemente ficticios, declarados como tales.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, con el proceso levantado por el anfitrión en memoria de la batería de integración |
| Almacén | **Real y efímero**: el mismo motor que en producción, en un archivo creado y descartado por la batería. **Nunca el almacén de desarrollo ni el de producción** |
| Aislamiento entre pruebas | Cada prueba de integración parte de un almacén preparado y conocido. **El paralelismo entre pruebas que comparten archivo de almacén no es admisible**: el motor es de **escritor único** |
| Secretos | La clave de firma de las pruebas es un valor **evidentemente ficticio**, provisto por configuración de prueba. **Ningún secreto real entra al repositorio, ni en el pipeline** |
| Transporte | **Sin canal de sesión interactiva.** El intake §17.5.P.3 declara que esta superficie **no expone ni requiere** ese canal, y que es criterio de aceptación de la etapa `a`. `TC-36` lo verifica |
| Intercambio de origen cruzado | **No configurado**, porque el navegador no alcanza esta superficie. `TC-36` verifica su ausencia |
| Medición del percentil y del caudal | **En el servidor**, sin contar el tramo de red doméstica, que el intake declara fuera de control |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** Los tres tiempos declarados —percentil, caudal y arranque en frío— son del **servicio**, no de la suite, y vienen del intake con su rótulo [ASUNCIÓN]. Ninguna fuente da un tiempo de suite, y no se inventa uno |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo **invertida** —60 integración, 40 unitario, 0 de extremo a extremo con la persona—, con el motivo que el intake §17.5.P.6 declara y con lo que esa inversión cuesta dicho sin adornos, más las inspecciones de umbral exacto que la compensan. Declara la cobertura por los **ocho** componentes, con el piso más bajo del producto y con la guardia de admisión y el traductor muy por encima de él, y la cobertura contable que no admite promedio. Declara el tooling por función, la política de **cero dobles en la batería de integración** con las tres sustituciones admitidas fuera de ella, y el uso de los **ocho** escenarios del intake §20 **como cuerpo de petición**, con la precisión de que esta capa verifica el borde y no la interpretación. Declara el ambiente, incluida la ausencia de canal de sesión interactiva y de intercambio de origen cruzado, y la constancia de que los tres tiempos declarados son **del servicio y no de la suite**. |
