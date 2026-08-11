# Backlog técnico — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Backlog-Tecnico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **ocho** componentes), §3.4 (los **cuatro** puertos), §5 (etapas del pipeline y puerta propia), §7 (cross-cutting), §8 (los **nueve** NFR), §9 (los **seis** riesgos) y §11 (los **seis** puntos abiertos); las **seis** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) (las **36** condiciones); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15 (etapas, reglas de delivery y puertas), §16 (estructura de repositorio), §17.2.P.1 a P.12 y §22 (asunciones `A-3` y `A-5`)
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones del proyecto de código](#21-ep-t01--fundaciones-del-proyecto-de-código)
  - [2.2 EP-T02 · Frontera, forma de la superficie y unidad de trabajo](#22-ep-t02--frontera-forma-de-la-superficie-y-unidad-de-trabajo)
  - [2.3 EP-T03 · Guarda de autorización](#23-ep-t03--guarda-de-autorización)
  - [2.4 EP-T04 · Los seis orquestadores](#24-ep-t04--los-seis-orquestadores)
  - [2.5 EP-T05 · Verificación y puntos abiertos](#25-ep-t05--verificación-y-puntos-abiertos)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **veintiuna** tareas técnicas viven **inline** en este documento y no en archivos individuales, porque el proyecto de código está por debajo del umbral de treinta que fija la regla de la categoría. Cada una declara su fuente upstream por identificador, sus criterios de aceptación, sus dependencias, su tipo y las historias que la consumen.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11 o de una regla de delivery del intake §15. **Cinco** convierten en trabajo un punto abierto que las categorías anteriores dejaron declarado sin resolver, en lugar de resolverlo por su cuenta: BT-02, BT-03, BT-18, BT-20 y BT-21.

**Dos particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Dos tareas cierran un punto abierto que no es de esta capa, y por eso lo acompañan en lugar de resolverlo.** BT-20 —los sellos de tiempo— y BT-21 —el criterio de comparación de correos— tienen su titularidad declarada en otro lado: el Product Owner con `GeometriaFactory-Domain` en el primer caso, y la categoría 05 de `GeometriaFactory-Infrastructure` en el segundo (`05` §11 `PA-04` y `PA-03`). Este backlog las hace visibles con su plazo y **no las decide**.
2. **La puerta más dura de este proyecto de código es una ausencia**: cero pruebas de esta capa que toquen la base de datos real (`PRODUCT-INTAKE` §17.2.P.8). BT-06 la materializa, y es lo que sostiene que la autorización por pertenencia se pueda verificar sin base, que es exactamente lo que la fuente exige probar.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1. Lo que ordena las tareas es la **etapa** y las dependencias de §3, no un tamaño relativo.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones del proyecto de código

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, compile con **una sola** dependencia saliente y cierre en su punto de control las decisiones de nombre que el intake y las categorías 02 y 05 dejaron abiertas para la etapa `a`, incluido el nombre del cuarto puerto |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, herramienta de versión y las tres puertas de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16, §17.2.P.1, §17.2.P.7 y §17.2.P.8; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md); `05` §5, §8 y §11 `PA-01`, `PA-02` y `PA-06` |
| Etapa | `a` |
| BT contenidas | BT-01, BT-02, BT-03, BT-04, BT-05, BT-06 |

### 2.2 EP-T02 · Frontera, forma de la superficie y unidad de trabajo

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **cuatro** puertos existan como frontera declarada, que toda negativa prevista viaje como resultado tipado con su código del catálogo cerrado, y que el alcance transaccional lo fije esta capa |
| Alcance | Declaración de puertos, resultado tipado, catálogo de **36** condiciones y unidad de trabajo |
| Fuente upstream | `05` §3.1 (componente «Declaración de puertos») y §3.4; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md), [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); `05` §8, filas de cobertura del catálogo y de unidades de trabajo |
| Etapa | `c`, y la cobertura del catálogo se cierra en la `f`, que es cuando el conjunto de condiciones está entero producido |
| BT contenidas | BT-07, BT-08, BT-09 |

### 2.3 EP-T03 · Guarda de autorización

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las **cuatro** comprobaciones se ejerzan en un único componente, con orden fijo, sobre el dato ya recuperado y antes de escribir; y que la cuarta corte antes que las otras tres |
| Alcance | Componente de guarda, orden fijo y matriz de ejercicio de las cuatro negativas |
| Fuente upstream | `05` §3.1 (componente «Guarda de autorización»), §7 fila de autorización, §8 fila de ejercicio de las cuatro comprobaciones, §9 riesgos primero a tercero; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md); [`Domain ADR-05`](../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) §6 punto 1 |
| Etapa | `c` la guarda, `d` la matriz completa, porque la cuarta comprobación no tiene sobre qué decidir hasta que exista la marca |
| BT contenidas | BT-10, BT-11 |

### 2.4 EP-T04 · Los seis orquestadores

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **once** casos de uso queden repartidos en los **seis** componentes de orquestación que `05` §3.3 declara, sin que ningún orquestador dependa de otro |
| Alcance | Alta de cuentas, gobierno de cuentas, ingreso y credencial, trabajo, consulta y desenlace |
| Fuente upstream | `05` §3.1 y §3.3; §3.2 punto 1 (ningún orquestador depende de otro); [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md); `05` §6 (forma de la consulta) |
| Etapa | `c` a `h`, según la historia que la consuma |
| BT contenidas | BT-12, BT-13, BT-14, BT-15, BT-16, BT-17 |

### 2.5 EP-T05 · Verificación y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los NFR con objetivo numérico tengan mecanismo de medición, que los valores rotulados como asunción se confirmen antes de volverse bloqueantes, y que los dos puntos abiertos cuya titularidad es de otro lado queden elevados con su plazo |
| Alcance | Puerta de cobertura, medición del caso de uso más pesado, y los dos puntos abiertos ajenos |
| Fuente upstream | `05` §8, filas de tiempo y de cobertura; `05` §11 `PA-03`, `PA-04` y `PA-05`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` |
| Etapa | `d` la mayor parte, `f` la medición del caso de uso más pesado, que es cuando el envío existe |
| BT contenidas | BT-18, BT-19, BT-20, BT-21 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-01 | Crear el proyecto de código y su proyecto de pruebas, con una sola dependencia saliente | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.2.P.1; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación; el archivo de proyecto declara exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web; el proyecto de pruebas existe y corre vacío | **Infraestructura compartida**: la sostiene [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md). Habilita a las 32 |
| BT-02 | Fijar los nombres de tipos, de espacios de nombres y **el del cuarto puerto**, y validarlos en el punto de control | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01` y `PA-02`; `05` §3.4 y §9, sexto riesgo; `PRODUCT-INTAKE` §17.2.P.1 | BT-01 | Existe una propuesta de nombres para los tipos, los espacios de nombres y **el cuarto puerto, el de repositorio de cuentas**, que ninguna fuente nombra; el Product Owner y el equipo la aceptan o la corrigen **en el punto de control de la etapa `a`**; la decisión queda registrada. **El puerto no se agrega ni se quita: son cuatro**, y lo que se decide es su nombre. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los cuatro componentes que consumen el cuarto puerto dependen de que su nombre esté fijado. `05` §9 le asigna probabilidad **alta** al retrabajo si se fija sin punto de control |
| BT-03 | Elegir y anclar la herramienta que calcula la versión | indagación | EP-T01 | `a` | Media | Sin fijar | `05` §11 `PA-06`; `PRODUCT-INTAKE` §17.2.P.7, declarado idéntico a §17.1.P.7 | BT-01 | La herramienta está elegida y su versión anclada según la regla de anclaje de versiones del producto; el cálculo a partir de las convenciones de mensaje de confirmación produce un resultado reproducible. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la exige la estrategia de versionado del intake |
| BT-04 | Puerta bloqueante de dependencias salientes | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de dependencias salientes; `05` §9, primer riesgo | BT-01 | La inspección del archivo de proyecto es parte de la revisión y **bloquea la fusión** si aparece una dependencia nueva; la puerta se mide **en cada etapa** y no sólo en la `a` | **Infraestructura compartida**: sostiene la propiedad que justifica el estilo entero |
| BT-05 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de advertencias de construcción; `PRODUCT-INTAKE` §17.2.P.8 | BT-01 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-06 | Puerta propia de cero pruebas que tocan la base de datos real | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.2.P.8 (puerta propia y bloqueante); `05` §5 y §8, fila correspondiente; `05` §9, primer riesgo | BT-01 | Exactamente **0** pruebas de esta capa abren la base de datos real; la pirámide del proyecto de código es **100 %** unitaria; una prueba que la toque **está mal ubicada** y pertenece a la batería de integración de `GeometriaFactory-Api`. La puerta bloquea la fusión | **Infraestructura compartida**: es lo que hace verificable la autorización por pertenencia sin base, que es lo que la fuente exige probar |
| BT-07 | Declarar los cuatro puertos como frontera de este proyecto de código | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.1, componente «Declaración de puertos», y §3.4; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md); [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 | BT-01, BT-02 | Los puertos son exactamente **cuatro** —repositorio de trabajos, validación de figuras, reloj del sistema y repositorio de cuentas—; son declaraciones **sin implementación** en este proyecto de código; **este proyecto de código no nombra ni referencia a `GeometriaFactory-Infrastructure`**; la conexión con los adaptadores es de la composición de raíz y no de acá | US-08, US-10, US-15, US-17, US-19, US-20, US-29, US-31 |
| BT-08 | Construir el resultado tipado y cerrar el catálogo de las 36 condiciones en las dos direcciones | feature | EP-T02 | `f` | Alta | Sin fijar | [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); `05` §7, fila de manejo de errores; `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) | BT-01, BT-02 | Toda condición prevista viaja como **valor de retorno** con su código estable y nunca como excepción de control de flujo; las excepciones quedan reservadas a defectos de programación del consumidor; **100 %** de las **36** condiciones alcanzadas por al menos una prueba y **0** condiciones emitidas que no figuren en el catálogo, comparado **en las dos direcciones** | US-02, US-07, US-14, US-16, US-25, US-30 y, por herencia de forma, las 32 |
| BT-09 | Fijar el alcance de la unidad de trabajo: un caso de uso, una unidad | feature | EP-T02 | `e` | Alta | Sin fijar | [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md); `05` §4, segunda viñeta; `05` §8, fila de unidades de trabajo por caso de uso | BT-07 | Cada caso de uso abre **a lo sumo 1** unidad de trabajo y **0** reparten su efecto entre dos; el mecanismo lo provee el adaptador y el **alcance** lo fija esta capa; el arrastre de la baja es el caso testigo y se verifica con una prueba | US-06, US-10, US-12, US-13, US-23, US-26, US-29 |
| BT-10 | Construir la guarda de autorización con las cuatro comprobaciones en orden fijo | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guarda de autorización»; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md); [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 | BT-01, BT-02, BT-08 | Las **cuatro** comprobaciones —cambio de contraseña pendiente, pertenencia, facultad y alcance del administrador— viven en **un único componente**, en el orden fijo de la ADR y **sobre el dato ya recuperado, antes de escribir**; la guarda **no lee conjuntos y no escribe**; la negativa por pertenencia y la negativa por facultad **no se confunden**, y el trabajo ajeno y el identificador inexistente comparten motivo | US-04, US-07, US-12, US-20, US-21, US-22, US-25, US-26, US-27, US-29, US-30 |
| BT-11 | Armar la matriz de ejercicio de las cuatro comprobaciones, con la prueba de que la cuarta corta primero | docs | EP-T03 | `d` | Alta | Sin fijar | `05` §8, fila de ejercicio de las cuatro comprobaciones; `05` §9, segundo y tercer riesgo | BT-10 | **4 de 4** comprobaciones con al menos una prueba que verifique su negativa, **sin base de datos**; **1** sola prueba que verifique que la cuarta **corta antes que las otras tres**; una prueba que pide un trabajo ajeno y comprueba que el motivo emitido es el de inexistencia y no el de falta de autorización; la matriz se entrega a 08 y se revisa al cerrar cada etapa | US-25, US-26, US-30 |
| BT-12 | Construir la orquestación del alta de cuentas | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del alta de cuentas»; `05` §10.3 `INV-01` e `INV-05` | BT-07, BT-10 | Los **dos** caminos de alta quedan separados y con estados iniciales opuestos; la unicidad del correo y la existencia previa de una cuenta con papel `Administrador` **se resuelven por el puerto de repositorio de cuentas** y llegan al dominio ya resueltas; el auto-registro **rechaza el papel `Administrador`** | US-01, US-02, US-03, US-28 |
| BT-13 | Construir la orquestación del gobierno de cuentas | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del gobierno de cuentas»; `05` §10.2, filas de RN-07, RN-12, RN-15 y RN-16 | BT-07, BT-10, BT-09 | Las **cuatro** operaciones de admisión y el reseteo viven acá; la baja compara el correo escrito y retira todos los trabajos **en la misma unidad de trabajo**; **habilitar y rehabilitar** piden la provisoria, la derivan afuera y solicitan fijar la credencial derivada, dejando la marca puesta; el reseteo **no comprueba el estado de la cuenta** y **no dispara ningún retiro** | US-04, US-05, US-06, US-08, US-29, US-31 |
| BT-14 | Construir la orquestación del ingreso y la credencial | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del ingreso y la credencial»; `05` §10.3 `INV-06` e `INV-09` | BT-07, BT-10 | La consulta de admisibilidad devuelve el motivo **sin colapsarlo**; la credencial llega **ya derivada** y esta capa **no ve valores en claro**; el reemplazo por la propia cuenta es **el único lugar donde la marca se levanta**, y se levanta sólo con el cambio efectivo | US-07, US-08, US-09, US-32 |
| BT-15 | Construir la orquestación del trabajo | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del trabajo»; `05` §10.2, filas de RN-04, RN-05, RN-08 y RN-09 | BT-07, BT-09, BT-10 | Constituir, reeditar, enviar y retirar quedan acá; el texto original se entrega tal cual y **no se reescribe ni cuando la interpretación falla**; el envío entrega al dominio el conjunto de observaciones **completo y con su especie** y **no decide el estado**; el retiro tiene sus **dos** alcances opuestos | US-10, US-11, US-12, US-13, US-14, US-15, US-16, US-26, US-27 |
| BT-16 | Construir la orquestación de la consulta, con la proyección sin componentes | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Orquestación de la consulta»; `05` §6, las dos decisiones sobre la forma de la consulta; `05` §8, fila de componentes de pieza en el listado; [`Contracts ADR-05`](../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) | BT-07, BT-10 | Las dos consultas salen con su **predicado de alcance ya trasladado a la consulta** y no filtrado en memoria; **0** componentes de pieza cargados en el listado del alumno y en el de la comisión; el detalle sí los trae; el detalle del administrador es **equivalente** al del alumno | US-17, US-18, US-19, US-20, US-21, US-22 |
| BT-17 | Construir la orquestación del desenlace | feature | EP-T04 | `h` | Alta | Sin fijar | `05` §3.1, componente «Orquestación del desenlace»; `05` §10.3 `INV-07` | BT-07, BT-10 | Aprobar y rechazar proceden **sólo desde el estado `Pendiente`**, con comentario opcional; la facultad se verifica **antes** de pedir la transición, de modo que el rechazo por facultad no se confunda con el rechazo por terminalidad; la terminalidad se propaga | US-23, US-24, US-25 |
| BT-18 | Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, filas de tiempo del caso de uso más pesado y de cobertura; `05` §11 `PA-05`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-05, BT-06 | El Product Owner confirma o corrige los **500 ms** y la cobertura **sobre su propio documento**; hasta entonces se usan como vigentes y la puerta **no se declara bloqueante** en 09. **Ninguna de las dos salidas es inventar un número acá.** **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida**: condiciona la puerta del pipeline de todas las historias |
| BT-19 | Medir el tiempo del caso de uso más pesado sobre el escenario `E-1`, sin acceso a base | devops | EP-T05 | `f` | Media | Sin fijar | `05` §8, primera fila; `PRODUCT-INTAKE` §17.2.P.10 y §20 `E-1` | BT-06, BT-15, BT-18 | La medición se hace sobre la batería unitaria **con doble del puerto de validación** y **sin acceso a base**, que es lo que la hace atribuible a esta capa y no al adaptador; el material es el texto de **3** piezas del escenario `E-1` del intake §20 y **no se inventa ningún texto de prueba** | US-13, US-14, US-15 |
| BT-20 | Elevar los sellos de alta, de modificación y de desenlace al Product Owner | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §11 `PA-04`; `05` §6, cuarta viñeta; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 | BT-13, BT-15 | Queda registrado que el intake los sostiene como verificables en prueba y que **el modelo del dominio no los declara como atributos**; hasta que el Product Owner resuelva, esta capa los trata como **metadatos de orquestación** y no como atributos del dominio. **Esta tarea no resuelve la discrepancia: la eleva con su plazo.** **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la titularidad es del Product Owner y de `GeometriaFactory-Domain` |
| BT-21 | Acompañar la decisión del criterio de comparación de dos correos | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §11 `PA-03`; `RN-02`, `INV-01`; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §11 | BT-12 | Queda decidido si dos correos se comparan tal cual o normalizados, **y dónde se normaliza**. **La decisión no es de este proyecto de código**: `05` §11 `PA-03` la derivó a la categoría 05 de `GeometriaFactory-Infrastructure`, que es la que materializa el índice; esta tarea aporta el requisito de la orquestación del alta y **adopta el criterio que aquella fije**. **Caja temporal: antes de cerrar la etapa `d`** | US-02 |

**Ocho tareas se justifican como infraestructura compartida** —BT-01, BT-02, BT-03, BT-04, BT-05, BT-06, BT-18 y BT-20— y las **trece** restantes declaran al menos una historia consumidora. Ninguna queda sin una cosa ni la otra.

## 4. Trazabilidad BT ↔ US ↔ CU

Las veintiuna filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-01 | Infraestructura compartida (habilita a las 32) | CU-01 a CU-11 | ADR-01, `05` §5 |
| BT-02 | Infraestructura compartida | CU-01 a CU-11 | `05` §11 `PA-01` y `PA-02`, ADR-02 |
| BT-03 | Infraestructura compartida | — (estrategia de versionado) | `05` §11 `PA-06` |
| BT-04 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-01 |
| BT-05 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-03 |
| BT-06 | Infraestructura compartida | — (puerta propia del pipeline) | `05` §5 y §8, ADR-02 |
| BT-07 | US-08, US-10, US-15, US-17, US-19, US-20, US-29, US-31 | CU-02, CU-03, CU-04, CU-05, CU-06, CU-07, CU-11 | ADR-02, `05` §3.4 |
| BT-08 | US-02, US-07, US-14, US-16, US-25, US-30 | CU-01, CU-03, CU-05, CU-08, CU-11 | ADR-06, `05` §8 |
| BT-09 | US-06, US-10, US-12, US-13, US-23, US-26, US-29 | CU-02, CU-04, CU-05, CU-08, CU-09, CU-11 | ADR-05 |
| BT-10 | US-04, US-07, US-12, US-20, US-21, US-22, US-25, US-26, US-27, US-29, US-30 | CU-02, CU-03, CU-04, CU-06, CU-07, CU-08, CU-09, CU-11 | ADR-04, `05` §3.1 |
| BT-11 | US-25, US-26, US-30 | CU-08, CU-09, CU-11 | `05` §8, ejercicio de las cuatro comprobaciones |
| BT-12 | US-01, US-02, US-03, US-28 | CU-01, CU-10 | `05` §3.1, alta de cuentas |
| BT-13 | US-04, US-05, US-06, US-08, US-29, US-31 | CU-02, CU-11 | `05` §3.1, gobierno de cuentas |
| BT-14 | US-07, US-08, US-09, US-32 | CU-03 | `05` §3.1, ingreso y credencial |
| BT-15 | US-10, US-11, US-12, US-13, US-14, US-15, US-16, US-26, US-27 | CU-04, CU-05, CU-09 | `05` §3.1, trabajo |
| BT-16 | US-17, US-18, US-19, US-20, US-21, US-22 | CU-06, CU-07 | `05` §3.1 y §6, consulta |
| BT-17 | US-23, US-24, US-25 | CU-08 | `05` §3.1, desenlace |
| BT-18 | Infraestructura compartida | — (puerta de cobertura y de tiempo) | `05` §11 `PA-05` |
| BT-19 | US-13, US-14, US-15 | CU-05 | `05` §8, primera fila |
| BT-20 | Infraestructura compartida | CU-01, CU-03, CU-04, CU-05, CU-08, CU-10, CU-11 | `05` §11 `PA-04` |
| BT-21 | US-02 | CU-01, CU-10 | `05` §11 `PA-03` |

**Cobertura inversa: los once casos de uso tienen al menos una tarea técnica que los realiza.** CU-01 en BT-08, BT-12, BT-20 y BT-21; CU-02 en BT-07, BT-09, BT-10 y BT-13; CU-03 en BT-07, BT-08, BT-10, BT-14 y BT-20; CU-04 en BT-07, BT-09, BT-10, BT-15 y BT-20; CU-05 en BT-07, BT-08, BT-09, BT-15, BT-19 y BT-20; CU-06 en BT-07, BT-10 y BT-16; CU-07 en BT-07, BT-10 y BT-16; CU-08 en BT-08, BT-09, BT-10, BT-11, BT-17 y BT-20; CU-09 en BT-09, BT-10, BT-11 y BT-15; CU-10 en BT-12, BT-20 y BT-21; CU-11 en BT-07, BT-08, BT-09, BT-10, BT-11, BT-13 y BT-20. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

**Cobertura de los ocho componentes de `05` §3.1.** Guarda de autorización en BT-10 y BT-11; Declaración de puertos en BT-07; Orquestación del alta de cuentas en BT-12; Orquestación del gobierno de cuentas en BT-13; Orquestación del ingreso y la credencial en BT-14; Orquestación del trabajo en BT-15; Orquestación de la consulta en BT-16; Orquestación del desenlace en BT-17. **Los ocho tienen tarea técnica y ninguna tarea construye un componente que la arquitectura no declare.**

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Application`. Declara **cinco** épicas técnicas con su objetivo, su alcance, su fuente upstream y la etapa en la que corren, y **veintiuna** tareas técnicas inline —por debajo del umbral de treinta— cada una con tipo, fuente upstream por identificador, dependencias, criterios de aceptación verificables y las historias que la consumen. Convierte en trabajo los cinco puntos abiertos que las categorías 02 y 05 dejaron declarados, con la precisión de que **dos de ellos no son de este proyecto de código y por eso se acompañan en lugar de resolverse**: los sellos de tiempo, cuya titularidad es del Product Owner con `GeometriaFactory-Domain`, y el criterio de comparación de correos, que `05` §11 derivó a la categoría 05 de `GeometriaFactory-Infrastructure`. Declara como tarea propia la puerta más dura del proyecto de código, que es una ausencia: cero pruebas que toquen la base de datos real. Emite la matriz BT ↔ US ↔ CU con sus veintiuna filas, la cobertura inversa sobre los once casos de uso y la cobertura de los ocho componentes de la arquitectura. |
| 1.1 | 2026-08-11 | **Cierra el hallazgo `D-06-02`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **§4**: la enumeración de cobertura inversa omitía **BT-20** en **dos** entradas, la de **CU-01** y la de **CU-04**, pese a que la fila de BT-20 de esa misma matriz declara «CU-01, CU-03, CU-04, CU-05, CU-08, CU-10, CU-11» y a que las otras cinco entradas de esa fila sí la incluían. La omisión no afectaba la cobertura —los once casos de uso tenían y tienen al menos una tarea técnica— pero sí la exhaustividad de la enumeración. Se agrega BT-20 a las dos entradas y se declara explícitamente que la enumeración **es exhaustiva** y que incluye las filas de alcance general. **Se recontó la matriz entera**, reconstruyendo el diccionario inverso `CU → {BT}` desde las veintiuna filas: éstas eran las únicas dos discrepancias. Ninguna tarea técnica, dependencia ni criterio cambia. Sube minor. |
