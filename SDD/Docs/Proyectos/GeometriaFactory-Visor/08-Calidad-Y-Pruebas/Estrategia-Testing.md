# Estrategia de testing — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Estrategia-Testing.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §6; [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2, §5.3, §5.5 y §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §4 y §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §16.1, §17.7.P.6, §18 (sample S-1), §20 y §21
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md)

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

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5**. Este proyecto de código la adopta **con una redistribución declarada**, porque su verificación central no es unitaria: es la medición de propiedades sobre el **bundle generado** corriendo en una página.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Lector del texto y disposición derivada del índice: dos componentes de la capa 3 que son transformación pura de entrada a salida | **45 %** | Baja respecto del 80 de la regla porque **la mayor parte de lo que hay que verificar no es una función pura**: es una escena viva, un bucle de dibujo y un contexto gráfico |
| Integración | La fachada sobre el registro de instancias y el servicio de dibujo: ciclo de vida completo de una instancia, sin el navegador real cuando la comprobación no lo exige | **20 %** | Es donde se verifican `G-4`, `G-7` y la mayor parte de los siete códigos |
| Extremo a extremo en página | El recorrido de las **seis** funciones sobre una página real con capacidad gráfica: el sample **S-1** y la página del anfitrión de `PT-02` | **25 %** | **Sube muy por encima del 5 de la regla**, y es el apartamiento principal. Sin una página real no se pueden medir ni los diez recorridos, ni la cuenta de peticiones con el bucle corriendo, ni la liberación del contexto gráfico |
| Inspección del artefacto generado | Recuentos sobre el bundle: funciones expuestas, identificadores globales, ocurrencias de las tres formas de petición, claves escritas | **10 %** | Es el gate que el intake §17.7.P.6 pone **en lugar de** la cobertura de líneas |

**El apartamiento es doble y está fundado.** El nivel unitario baja de 80 a 45 y el de extremo a extremo sube de 5 a 25. El motivo no es comodidad sino que **las propiedades que este proyecto de código compromete no son verificables sin una escena viva**: `02` §6 declara para cuatro de las seis propiedades transversales una condición de medición que exige un bucle de dibujo corriendo. Una batería mayoritariamente unitaria mediría el caso fácil, que es exactamente lo que esa sección viene a impedir.

**Snapshot: no aplica, y se declara.** La salida de este proyecto de código es una escena tridimensional; una comparación de imágenes sería frágil, dependiente del hardware gráfico y **no distinguiría un cambio legítimo de orientación de una deriva de posición**, cuando el determinismo comprometido por `G-6` es de **posición y no de orientación**. Lo que reemplaza al snapshot es la comparación de dos procesados pieza por pieza.

## 2. Cobertura mínima por capa

La partición es por los **seis** componentes de `05` §3.1, de los cuales **dos no son de este proyecto de código**.

| Componente | Capa | Métrica | Umbral |
| --- | --- | --- | --- |
| Componente anfitrión | 1, **fuera de este proyecto de código** | — | No aplica: vive en `GeometriaFactory-Web` y su cobertura es de la categoría 08 de ese proyecto de código |
| Fachada plana | 2 | Funciones ejercitadas; garantías sostenidas | **6 de 6** funciones con al menos un caso de prueba; `G-3` y `G-7` verificadas |
| Registro de instancias | 2 | Cursos del ciclo de vida del identificador | 100 % de los cursos: identificador válido, ya liberado, e inexistente. `G-4` verificada |
| Lector del texto | 3 | Tipos dibujables y variantes de clave del emisor | **6 de 6** tipos; las variantes `Tapas` y `Bases` aceptadas como sinónimos; **el cero como dimensión legible** |
| Servicio de dibujo | 3 | Propiedades transversales que lo alcanzan | `G-5`, `G-6`, disposición determinista y liberación de recursos |
| Motor de dibujo tridimensional | 3, **empaquetado** | — | **No se prueba por dentro**, y es deliberado: probarlo ataría este proyecto de código a un motor concreto y lo volvería irreemplazable, que es lo contrario del punto de extensión ([`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Motor-De-Dibujo-Empaquetado-Y-Aislado.md)) |
| **Bundle generado** | — | Recuentos de superficie y de ausencias | **6** funciones, **1** nombre propio en el objeto global, **0** globales sueltas, **0** peticiones, **0** claves, **0** dependencias de red externa |

**No hay umbral de cobertura de líneas, y su ausencia está declarada aguas arriba.** El intake §17.7.P.6 fija «gate bloqueante y verificable por inspección, **en lugar de cobertura de líneas**: cero ocurrencias de las tres formas de petición en el código fuente y en el bundle generado» [ASUNCIÓN en cuanto a expresarlo como gate automatizable; la regla es de `RA-02` y ya es criterio de aceptación de la etapa `g`]. **El rótulo [ASUNCIÓN] alcanza a la forma del gate, no a la regla**, y esta categoría lo cita con esa precisión.

**No hay mutation score**, y su ausencia se declara en lugar de omitirse: `Rules-Calidad-Y-Pruebas.md` §2.2 lo pide para `library`, pero acá la mayor parte del valor está en propiedades medidas sobre una escena viva, y mutar el código de dibujo produciría mutantes que sólo una comparación de imágenes podría matar —justamente la técnica que §1 descarta con su motivo—. **Es la única exigencia de §2.2 que este proyecto de código no cumple.**

## 3. Tooling

Nombrado por función, según la convención de las categorías 02, 03 y 05 de este proyecto de código.

| Propósito | Herramienta, por su función |
| --- | --- |
| Unit e integración | Marco de pruebas del entorno de ejecución de la cadena de herramientas, sólo en tiempo de construcción |
| Extremo a extremo en página | Un conductor de navegador con capacidad gráfica tridimensional, capaz de contar peticiones de red y de leer el almacenamiento del navegador |
| Inspección del bundle generado | Comprobación reproducible de texto sobre el archivo de guion producido, más lectura de los identificadores que expone en el objeto global |
| Página integradora sin backend | El sample **S-1** del intake §18 y §16.1, que es a la vez ejemplo y material de prueba |
| Construcción | El guion propio del bundle, para el ciclo corto, y el guion general encadenado (intake §17.7.P.8) |

**El motor de dibujo tridimensional y el empaquetador se nombran por su función y no por su producto**, que es la convención que `05` §2.2 declara con su fundamento: el motor es reemplazable por diseño y nombrarlo en cada documento haría más caro reemplazarlo.

## 4. Especificaciones Given-When-Then

Los criterios de aceptación de los **siete** casos de uso ya están escritos, y las **catorce** historias los llevan inline en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3, con la exigencia de la DoR §1 criterio 3.

**Decisión de esta categoría: no se adopta un juego de archivos de escenario ejecutables.** Los criterios viven en las historias y en los casos de uso; cada `TC-XX` los transcribe citando su origen.

**La forma característica de los criterios de este proyecto de código es el umbral cero con su condición de medición**, y conviene decirlo porque cambia cómo se escribe cada aserción. La DoR §2 criterio 3 ya lo exige del lado de la entrada: «cuando la propiedad que sostienen es una **ausencia**, el criterio se expresa con umbral cero y con la condición en que se mide. Un criterio de ausencia sin condición de medición no está listo: mediría el caso fácil». **Esta categoría lo hereda del lado del cierre**: un `TC-XX` de ausencia sin condición de medición es un caso de prueba mal escrito.

## 5. Mocks y fixtures

**Sin dobles del motor de dibujo.** Sustituirlo por un doble haría que las pruebas verificaran el doble y no la escena, y perdería exactamente lo que hay que medir: el contexto gráfico, su liberación y el bucle.

**Un solo doble admitido, y con condición**: el conductor de navegador puede simular la **preferencia de movimiento reducido** del sistema. La fachada no la consulta —hacerlo violaría `G-3`—, de modo que lo que se simula es el entorno del anfitrión y no una dependencia del bundle. Es lo que permite verificar que la prueba **puede prender los dos movimientos aunque el entorno declare la preferencia**, que es la condición sin la cual la medición de cero red quedaría en verde sin ejercitar el bucle.

Fixtures declarados:

| Fixture | Qué contiene | De dónde sale |
| --- | --- | --- |
| Texto del escenario `E-1` | Tres piezas: `Cilindro`, `Cubo` y `Ortoedro`, con la clave `Bases` en el ortoedro | Intake §20.E-1, transcripto íntegro |
| Texto del escenario `E-7` | Seis piezas que cubren los seis tipos dibujables, tres volumétricos y tres planos | Intake §20.E-7 |
| Texto del escenario `E-2` | Un ortoedro con la clave `Tapas` y con dos comas finales | Intake §20.E-2 |
| Texto del escenario `E-8` | Un ortoedro dibujable y un cubo con dimensión no legible | Intake §20.E-8 |
| Texto del escenario `E-6` | Una figura plana con una dimensión en `0.00` | Intake §20.E-6 |
| Texto del escenario `E-5` | Dos figuras, la segunda con tipo fuera del conjunto conocido | Intake §20.E-5 |
| Elemento de dibujo de tamaño cero | Una superficie de dibujo sin tamaño, para los dos cursos de `ELEMENTO_DE_DIBUJO_INVALIDO` | Compuesto por esta categoría; **no es un dato de geometría** y no sustituye ningún escenario |

## 6. Datos de prueba

**Los datos de geometría de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, su procedencia y su estado declarado; §21 los cruza contra la batería obligatoria de **nueve** casos de prueba y declara, en su tabla de cobertura de invariantes, que **el contrato de fachada tiene sus siete condiciones con escenario** en `E-1` a `E-8`.

**Este proyecto de código sí recibe el texto**, a diferencia de los otros dos de nivel 0: `cargarJson` lo procesa. Por eso los ocho escenarios entran acá **como texto y no como resultado**.

| Escenario | Qué ejercita de este proyecto de código | Fuente |
| --- | --- | --- |
| `E-1` | Se dibujan **las tres figuras**, **ortoedro incluido**, y procesar el mismo trabajo dos veces produce la **misma disposición**. Es material declarado de `02` §6 y lo que `PT-02` mide | §20.E-1, punto 7 |
| `E-2` | La clave `Tapas` como sinónimo de las bases: **en el visor, el ortoedro se dibuja**. Hoy, en el visualizador previo, ningún ortoedro generado por la aplicación se dibuja | §20.E-2, punto 8 |
| `E-3` y `E-4` | El cubo de lado 3 con caras `Cuadrado` y con caras `Rectangulo`. Para la fachada los dos se dibujan igual: el campo que se usa es `Largo`. **La fachada no emite ninguna observación** sobre el área declarada: eso es del backend | §20.E-3 punto 1 y §20.E-4 punto 1 |
| `E-5` | Una figura con tipo fuera de los seis dibujables: **no se dibuja y queda enumerada** con su índice y `TIPO_NO_DIBUJABLE`; la primera, válida, se dibuja igual | §20.E-5, punto 3, leído desde el lado del dibujo |
| `E-6` | Una dimensión en `0.00`: **la figura se dibuja**, porque el cero es una dimensión legible y lo que produce `DIMENSION_NO_LEGIBLE` es la **ausencia** de la clave. Que una figura de dimensión cero no se vea **no es una falla del validador ni de la fachada** | §20.E-6, puntos 1 y 4; contrato de fachada §5.3 |
| `E-7` | Los **seis** tipos dibujables como piezas del conjunto raíz; el ortoedro con ancho 6, profundidad 4 y altura 8; y **todo esto sin backend**, con **0 peticiones** originadas por el bundle | §20.E-7, puntos 1 a 5 |
| `E-8` | El ortoedro del índice 0 **se dibuja** y la pieza del índice 1 **no**, reportada con **índice 1**, código `DIMENSION_NO_LEGIBLE` y el campo `Largo`. **El código es `DIMENSION_NO_LEGIBLE` y no `JSON_INVALIDO`**: confundirlos es el error que este escenario detecta | §20.E-8, puntos 1 a 3 |

**Los ocho escenarios están alcanzados y ninguno se sustituye.** `E-8` es además el que cierra el hueco que la versión 1.5 del intake dejó abierto: hasta entonces `DIMENSION_NO_LEGIBLE` era la única de las siete condiciones del contrato **sin escenario propio en §20 ni fila en §21**.

**Una precisión de frontera que esta estrategia hereda y no relaja.** `E-8` punto 4 declara que **el visor informa por qué no dibujó una pieza y que decidir si el trabajo pasa a `Pendiente` es del validador, no del bundle**. Ningún caso de prueba de este proyecto de código verifica el desenlace del envío: eso pertenece a `GeometriaFactory-Domain` y a `GeometriaFactory-Infrastructure`.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde se construye | Dentro del contenedor de desarrollo; el gestor de paquetes corre ahí (intake §17.7.P.1) |
| Dónde se ejecuta lo de extremo a extremo | Un navegador con **capacidad gráfica tridimensional**. Sin ella el visor no es soportado y la fachada informa `CAPACIDAD_GRAFICA_AUSENTE`, que es en sí mismo un caso de prueba |
| Runtime en ejecución | **Ninguno propio**: en tiempo de ejecución no hay entorno de la cadena de herramientas, hay un archivo servido como recurso estático (`05` §5) |
| Backend | **Ninguno, y es una propiedad exigida.** El recorrido completo se hace con **0 servicios del backend disponibles** (`02` §6) |
| Preferencia de movimiento reducido | Se simula en el conductor, según §5. Las mediciones de ausencia se hacen **con los dos movimientos prendidos** |
| Aislamiento | Cada prueba crea sus instancias y las destruye. Dos instancias vivas no comparten nada (`G-4`), de modo que el paralelismo es admisible |
| Duración | **No se declara ninguna.** Ninguna fuente da un tiempo de ejecución para la batería de este proyecto de código, y esta categoría no lo inventa. Lo único con umbral temporal declarado es la ausencia de degradación tras **diez** recorridos, que se cuenta en recorridos y no en segundos |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el apartamiento doble de la pirámide de `Rules-Calidad-Y-Pruebas.md` §2.2 —el nivel unitario baja de 80 a 45 y el de extremo a extremo sube de 5 a 25— con el fundamento de que cuatro de las seis propiedades transversales exigen una escena viva; la cobertura por los **seis** componentes de `05` §3.1, con los dos que no son de este proyecto de código declarados; las ausencias declaradas de cobertura de líneas, de snapshot y de mutation score, cada una con su motivo; el tooling nombrado por función; el único doble admitido y su condición; los **siete** fixtures; el uso de los **ocho** escenarios reales del intake §20 **como texto y no como resultado**, con la precisión de frontera de `E-8`; y el ambiente, con la constancia de que no se declara ningún tiempo de ejecución que ninguna fuente dé. |
