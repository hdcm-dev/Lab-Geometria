# Estrategia de testing — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Estrategia-Testing.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md); [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6, §17.1.P.8, §20, §21 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md); `09-Devops` y `11-Documentacion`
**Consolida a:** las estrategias de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0 §4, salida **S1**

---

## 0. Cómo leer este documento

**La unidad de entrega tiene una sola estrategia de testing, y sus cuatro proyectos de código tienen
pirámides y umbrales distintos.** Este documento declara las dos cosas: cada sección lleva **una
subsección por proyecto de código**, con el texto que ese proyecto declaraba, y esta sección cero
declara lo que **sólo se ve poniéndolos juntos**.

**Ninguna cifra se promedió.** Es la regla que gobierna la consolidación de esta categoría, y el
motivo es que un promedio de umbrales de cobertura **no es un umbral**: si el dominio pide 90 y el
host 75, un 82,5 global se cumple con el dominio en 70, que es exactamente lo que el 90 existía para
impedir.

### 0.1 Los cuatro pisos, juntos por primera vez

| Proyecto de código | Líneas | Ramas | Contra la guía para su tipo | Lectura |
| --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | **90 %** | **85 %** | `library` pide 80 · **sube 10** | Cero dependencias salientes: todo es entrada y salida de una operación |
| `GeometriaFactory-Application` | **85 %** | **80 %** | `library` pide 80 · **sube 5** | Casos de uso con dobles de los cuatro puertos |
| `GeometriaFactory-Infrastructure` | **85 %** | **80 %** | `library` pide 80 · **sube 5**, y el validador va a **95** | Dos motores y dos adaptadores |
| `GeometriaFactory-Api` | **75 %** | **70 %** | `rest-api` pide 80 · **baja 5** | Es cableado, y el cableado se verifica ejerciéndolo |
| **Unidad de entrega** | **no se promedia** | **no se promedia** | — | Se cumplen **los cuatro** o no se cumple |

**El apartamiento sigue siendo uno solo, y sigue sin ADR.** De los cuatro pisos, el único que **baja**
el de su tipo es el de `GeometriaFactory-Api`: 75 contra el 80 que `Rules-Calidad-Y-Pruebas.md` §2.2
fija para `rest-api`. Los otros tres suben. La consolidación **no lo resuelve ni lo diluye**: el
número lo fija el intake §17.1.P.6 rotulado `[ASUNCIÓN]`, y la ADR que §2.2 exige para bajar cobertura
**sigue faltando**, en `05-Arquitectura-Tecnica`. Queda registrado en
[`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8.

**Que la unidad promedie 83,75 no significa nada, y por eso no se escribe en ninguna tabla.**

### 0.2 Las cuatro pirámides, y por qué no hay una sola

| Proyecto de código | Unit | Integración | E2E | Forma |
| --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | **90 %** | 10 % interna | 0 % | Pirámide **acentuada**: sube el piso unitario de 80 a 90 |
| `GeometriaFactory-Application` | **100 %** | 0 % | 0 % | **Toda unitaria**: la integración vive en la batería de la entrega |
| `GeometriaFactory-Infrastructure` | mayoritaria | interna con almacén efímero | 0 % | Motores sin almacén, adaptadores con él |
| `GeometriaFactory-Api` | 40 % | **60 %** | 0 % | Pirámide **invertida**, y es un apartamiento declarado del intake |
| **Unidad de entrega** | — | — | **0 %** | El recorrido de una persona pasa por `GeometriaFactory-Web`, y su verificación es el guion de demostración **de esa unidad de entrega** |

**Las cuatro conviven sin contradecirse, y ponerlo por escrito es lo que la consolidación agrega.**
La pirámide invertida del host y la ausencia total de integración en la capa de aplicación **son la
misma decisión vista desde dos lados**: la integración del backend se ejerce una sola vez, en la
batería de la entrega, contra la superficie real y el almacén real. Leídas por separado, la de
`Application` parecía una omisión.

### 0.3 Lo que no admite promedio en ningún caso

Además de la cobertura por líneas hay **cobertura contable**, y ésa se cumple entera o no se cumple:
**15 de 15** puntos de acceso ejercidos, **17 de 17** códigos del contrato recorridos, **4 de 4**
puertos conectados, **4** puntos fuera de la guardia, y el **100 % de endpoints cubiertos por contract
test** que `Rules-Calidad-Y-Pruebas.md` §2.2 exige.

---

## 1. Pirámide de testing deseada

### 1.1 `GeometriaFactory-Api`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `rest-api` la distribución **70 / 20 / 10** entre unitario, integración y extremo a extremo, con **100 % de endpoints cubiertos por contract test**. Este proyecto de código **se aparta del reparto, y el apartamiento no es de esta categoría**: el intake §17.1.P.6 · GeometriaFactory-Api declara **«60 % integración, 40 % unitarias, invertida respecto de lo habitual y a propósito, porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo»**.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Integración | **La batería de integración del producto**: golpea la superficie real **por su protocolo** contra el almacén real, con el proceso levantado en el ambiente de prueba | **60 %** [ASUNCIÓN del intake en cuanto al reparto] | Lo declara el intake §17.1.P.6 · GeometriaFactory-Api. Un host delgado no tiene lógica propia que probar en aislamiento: lo que hay que verificar es **que el cable esté conectado**, y eso sólo se ve ejerciéndolo. Es además la batería a la que el intake §17.1.P.6 · GeometriaFactory-Infrastructure le asigna **la persistencia real** de `GeometriaFactory-Infrastructure` |
| Unit | La **traducción de motivos y códigos**, que es la única pieza con lógica propia; y las pruebas de inspección estructural | **40 %** [ASUNCIÓN en cuanto al reparto] | El traductor se puede recorrer entero sobre los **diecisiete** códigos sin levantar el proceso, y hacerlo unitario lo vuelve barato de reejecutar. Las inspecciones —los cuatro puntos fuera de la guardia, los cuatro puertos conectados— también |
| Extremo a extremo con la persona | — | **0 %** | **No aplica acá y se declara así en lugar de omitirse.** El recorrido de una persona pasa por `GeometriaFactory-Web`, y su verificación es el guion de demostración de ese proyecto de código. Acá lo más cerca de un recorrido es la **colección de peticiones reproducible** de `CU-00012`, que el intake declara como forma de demostración de este tipo de proyecto de código y que **no es una prueba automatizada** |

**El apartamiento invierte la pirámide y hay que decir qué se paga por eso.** Una batería mayoritariamente de integración es más lenta y más frágil que una unitaria, y su diagnóstico es más caro. Lo que la mantiene sana acá es que **las propiedades más peligrosas del proyecto de código no dependen de ejercer el cable sino de contarlo**: los cuatro puntos fuera de la guardia, los dieciséis códigos con destino, las tres familias indistinguibles, los cuatro puertos conectados y la única configuración de intercambio se verifican con **inspecciones de umbral exacto**, que son unitarias y baratas.

**Lo que la regla exige y acá se cumple con creces**: `Rules-Calidad-Y-Pruebas.md` §2.2 pide **100 % de endpoints cubiertos por contract test**. Acá los **quince** puntos de acceso se ejercen en la batería de integración, y además la tabla de traducción se recorre entera en las dos direcciones.

**Tres clases de verificación que conviene nombrar aparte:**

- **Prueba de integración por protocolo.** Levanta el proceso y golpea un punto de acceso con su verbo, su cuerpo y su cabecera de autorización, contra el almacén real.
- **Prueba de inspección con umbral exacto.** Recorre un conjunto cerrado —los quince puntos, los diecisiete códigos, los cuatro puertos— y compara **en las dos direcciones**. Su umbral no admite gradación.
- **Verificación forzando la petición.** La que ejerce una acotación **sin pasar por la interfaz**. Es el único criterio de verificación del producto que la fuente exige ejercer así contra esta superficie, y es `TC-00020`.

### 1.2 `GeometriaFactory-Domain`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código la adopta con **una redistribución declarada**, porque no tiene con qué integrar ni qué recorrer de punta a punta.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Las guardas, las transiciones y la constitución de las cinco entidades, sin dobles | **90 %** | El intake §17.1.P.6 · GeometriaFactory-Domain declara «pruebas unitarias puras y sin dobles». Cero dependencias salientes significa que todo lo que hay que probar es entrada y salida de una operación |
| Integración | Composición de dos o más de los cinco componentes de `05` §3.1 dentro del mismo proyecto de código: por ejemplo, adoptar la interpretación y después enviar | **10 %** | Es lo único que califica como integración acá: no hay base de datos, no hay red y no hay marco de aplicación con el que integrar |
| E2E y snapshot | — | **0 %** | **No aplica y se declara así en lugar de omitirse.** El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz (`05` §4 y §5). Un recorrido de punta a punta del producto pasa por `GeometriaFactory-Api`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los cinco puntos que la regla asigna a snapshot y extremo a extremo se reasignan a integración interna; el piso unitario **sube** de 80 a 90. No se baja ninguna exigencia, de modo que no hace falta la ADR que §2.2 exige para bajar cobertura.

**Contra la pirámide invertida**: acá sería imposible construirla, porque no hay nada que recorrer. **Contra la pirámide aplanada** —un número global de cobertura sin distinguir capas— la defensa es §2 de este documento, que reporta por componente y nunca como número único.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte**, porque no ejecutan lógica de negocio sino que revisan el proyecto de código sobre sí mismo:

- **Prueba de inspección.** Comprueba una propiedad estructural del proyecto de código: cero dependencias salientes, el conjunto de códigos emitidos contra el catálogo, ninguna operación que obtenga el momento por su cuenta. Se cuentan dentro del nivel unitario porque corren en el mismo ejecutor y con el mismo costo.
- **Prueba basada en propiedades.** Sobre invariantes que valen para todo valor admisible; ver §4.

### 1.3 `GeometriaFactory-Application`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código **se aparta del reparto y declara el motivo**, que no es de esta categoría sino del intake: §17.1.P.6 · GeometriaFactory-Application declara «pirámide del proyecto de código: **100 % unitarias**; la integración vive en `GeometriaFactory.Integration.Tests`, que pertenece a la Api».

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Los once casos de uso enteros, con **dobles de los cuatro puertos**, más las cuatro comprobaciones de autorización y las pruebas de inspección estructural | **100 %** | Lo declara el intake §17.1.P.6 · GeometriaFactory-Application. La inversión de dependencias existe precisamente para que un caso de uso entero sea unitario: no hay nada en esta capa que exija un ambiente |
| Integración | — | **0 %** | **No aplica acá y se declara así en lugar de omitirse.** La batería de integración del producto existe y golpea la API real contra el almacén real, pero es de `GeometriaFactory-Api` (intake §17.1.P.6 · GeometriaFactory-Application y §17.1.P.6 · GeometriaFactory-Api). Una prueba de esta capa que abriera el almacén violaría `QG-04` |
| E2E y snapshot | — | **0 %** | El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz (`05` §4 y §5). Un recorrido de punta a punta del producto pasa por `GeometriaFactory-Api` y `GeometriaFactory-Web`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los veinte puntos que la regla asigna a integración, extremo a extremo y snapshot **no se descartan: se reasignan a otro proyecto de código**, que es donde la fuente los pone. El piso unitario **sube** de 80 a 100, de modo que no se baja ninguna exigencia y no hace falta la ADR que §2.2 exige para bajar cobertura.

**Contra la pirámide invertida**: acá sería imposible construirla, porque una prueba de extremo a extremo de esta capa no existe sin salir de ella. **Contra la pirámide aplanada** —un número global de cobertura sin distinguir capas— la defensa es §2 de este documento, que reporta por componente y nunca como número único.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte**, porque no ejercen un caso de uso sino que revisan el proyecto de código sobre sí mismo:

- **Prueba de inspección.** Comprueba una propiedad estructural: cero pruebas que abren el almacén real, una sola dependencia saliente, el conjunto de códigos emitidos contra el catálogo, ninguna consulta de listado que materialice componentes. Se cuentan dentro del nivel unitario porque corren en el mismo ejecutor y con el mismo costo.
- **Prueba de orden.** Una sola, `TC-04011`: verifica que la cuarta comprobación corta antes que las otras tres. `05` §8 la exige como NFR con umbral **1**, y es la única prueba del proyecto de código cuyo objeto es el orden entre comprobaciones y no su resultado.

### 1.4 `GeometriaFactory-Infrastructure`

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código la adopta **con una redistribución acotada y declarada**.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Los **dos motores** —interpretación y verificación— sin almacén y sin red; los **dos mecanismos** de seguridad; y las pruebas de inspección estructural | **85 %** | Es donde vive la batería del validador, que es el corazón de este proyecto de código: **10** casos sobre **ocho** escenarios, todos sin almacén. El intake §17.1.P.10 · GeometriaFactory-Infrastructure pide medir la interpretación de `E-1` **sin almacén**, lo que sólo tiene sentido si el motor es probable así |
| Integración interna | Los **dos adaptadores de repositorio**, el contexto de persistencia y **la preparación del almacén al arrancar**, contra un almacén creado y descartado por la propia prueba | **15 %** | No es una elección: estas cuatro cosas **no se pueden verificar sin almacén**. El intake §17.1.P.8 · GeometriaFactory-Infrastructure declara una etapa propia del pipeline —**verificación de transformaciones**— y un criterio de aceptación de la etapa `c` que exige que las transformaciones **se apliquen solas sobre un almacén inexistente**. Sin este nivel, esa puerta no tiene dónde medirse |
| E2E y snapshot | — | **0 %** | **No aplica y se declara así en lugar de omitirse.** El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz. Un recorrido de punta a punta pasa por `GeometriaFactory-Api`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los cinco puntos que la regla asigna a extremo a extremo y snapshot se reasignan a unitario: el piso **sube** de 80 a 85. No se baja ninguna exigencia, de modo que no hace falta la ADR que §2.2 exige para bajar cobertura.

**Dónde termina esta capa y empieza la batería de integración del producto, dicho con precisión.** El intake §17.1.P.6 · GeometriaFactory-Infrastructure declara que **«la persistencia real contra SQLite se prueba desde `GeometriaFactory.Integration.Tests`»**, y ese proyecto de pruebas **pertenece a `GeometriaFactory-Api`** (§17.1.P.6 · GeometriaFactory-Api). La integración interna de acá no lo reemplaza y no lo duplica:

| Verificación | Dónde vive | Por qué |
| --- | --- | --- |
| Que el esquema se cree y se transforme sobre un almacén inexistente, y que el arranque se detenga ante uno dudoso | **Acá**, integración interna | Es puerta del pipeline **de este proyecto de código** y criterio de aceptación de la etapa `c` (intake §17.1.P.8 · GeometriaFactory-Infrastructure) |
| Que un adaptador materialice, recupere y retire respetando el todo o nada | **Acá**, integración interna | Es el contrato del puerto, y su forma de fallar es propia del adaptador |
| Que el producto entero, atendiendo por su superficie, opere sobre el almacén real | **En `GeometriaFactory-Api`** | Es lo que el intake §17.1.P.6 · GeometriaFactory-Infrastructure y §17.1.P.6 · GeometriaFactory-Api declaran, y lo que golpea la superficie por su protocolo |

**Contra la pirámide invertida**: acá sería imposible, porque no hay recorrido de punta a punta que construir. **Contra la pirámide aplanada** —un número global sin distinguir capas— la defensa es §2, que reporta por componente, con el validador con un umbral propio y más alto que el resto.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte:**

- **Prueba de inspección.** Comprueba una propiedad estructural: cero peticiones de red de los dos motores, el conjunto de códigos emitidos contra el catálogo, cero mensajes con un secreto o con la ruta del almacén.
- **Prueba con el almacén interrumpido a mitad de operación.** Es la única forma de verificar que un retiro parcial no ocurre, y `05` §8 la declara como mecanismo de medición de ese NFR.

## 2. Cobertura mínima por capa

### 2.1 `GeometriaFactory-Api`

La partición es por los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1. El piso global lo fija el intake §17.1.P.6 · GeometriaFactory-Api y es **75 % de líneas y 70 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`]. **Es el piso más bajo del producto, y el motivo está declarado en la fuente**: este proyecto de código es cableado, y su valor se verifica ejerciéndolo y no cubriéndolo.

**Este piso baja respecto del de la guía, y hay que decirlo.** `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `rest-api` **«80 % aplicación, 70 % infraestructura, 100 % de endpoints cubiertos por contract test»**, y agrega que «los porcentajes son piso, no techo. El equipo puede subir cobertura cuando el dominio lo exige, pero **no bajarla sin un ADR que lo justifique**». **75 < 80**: el piso global de líneas de este proyecto de código **baja** el de la guía en cinco puntos. El de ramas, **70 = 70**, no baja. Es el único de los siete proyectos de código del producto cuyo piso baja; `GeometriaFactory-Domain`, `-Application` e `-Infrastructure` hacen esta misma comparación y concluyen que el suyo **sube**.

**Con qué autoridad baja, y qué hace falta para sostenerlo.** El número no lo elige esta categoría: lo fija el intake §17.1.P.6 · GeometriaFactory-Api y viene rotulado **[ASUNCIÓN]**, de modo que el apartamiento es de la fuente del producto y no una relajación de la Fase E. Pero **la autoridad de la fuente no reemplaza a la ADR que §2.2 exige**: mientras el 75 % siga vigente, este proyecto de código queda con **un apartamiento de la guía declarado y sin ADR**, y **cerrarlo es de la categoría 05**, que es donde viven las ADR. Queda registrado como hueco en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8. **Esta categoría no sube el número por su cuenta** —hacerlo sería contradecir la fuente— **ni lo da por justificado por venir de ella**.

**Qué compensa la caída, que no es un argumento para no declararla.** El piso global es un promedio y los componentes donde un defecto no se nota suben muy por encima: la guardia de admisión y el traductor a **95/90**, la superficie de acceso y la de trabajos a **80/75**, el arranque a **85/80**. Y los cuatro conjuntos contables del cierre de esta sección —15 puntos, 17 códigos, 4 puertos, 4 puntos fuera de la guardia— **no admiten promedio**: se cumplen enteros o no se cumplen. El **100 % de endpoints cubiertos por contract test** que la misma fila de §2.2 exige **se cumple**, y §1 lo declara.

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Composición de raíz | 75 % | 70 % | — | Piso del intake. **Sin mutation score**: es declaración de cableado, y su verificación real es `TC-00028`, que **falla en construcción** si un puerto queda sin adaptador |
| Guardia de admisión | **95 %** | **90 %** | 60 % | Sube muy por encima del piso: es donde se pierde `RN-00013` sin que nada falle, y `05` §9 le asigna probabilidad **alta** por ser un defecto de **omisión** |
| Traductor de motivos y códigos | **95 %** | **90 %** | 60 % | Sube: es la única pieza con lógica propia, y es donde una decisión de adentro se deshace. **Ninguna capa de adentro puede reparar un error suyo** |
| Superficie de acceso y credencial propia | 80 % | 75 % | 60 % | Sube sobre el piso: son los **cuatro** puntos que se ejercen sin acceso firmado, que es exactamente el conjunto que `QG-05` acota |
| Superficie de gobierno de la comisión | 75 % | 70 % | 60 % | Piso del intake |
| Superficie de trabajos | 80 % | 75 % | 60 % | Sube sobre el piso: contiene el punto de eliminación, cuyo forzado es criterio bloqueante de la fuente, y el envío, donde el texto no se normaliza |
| Superficie de desenlace | 75 % | 70 % | 60 % | Piso del intake |
| Arranque y salud | 85 % | 80 % | 60 % | Sube sobre el piso: **0** peticiones atendidas con la preparación incompleta es un umbral que no admite ramas sin cubrir |
| **Proyecto de código completo** | **75 %** | **70 %** | **60 %** | Intake §17.1.P.6 · GeometriaFactory-Api [ASUNCIÓN]. **El 75 baja el 80 que §2.2 fija para `rest-api`**, ver el párrafo de arriba. El mutation score lo toma prestado de la fila `library` de §2.2, que es la única que lo pide |

**De dónde sale cada número, sin mezclarlos.** El 75/70 global es del intake y viene rotulado **[ASUNCIÓN]**. El **mutation score de 60 %** no lo declara ninguna fuente del producto **ni la fila que le corresponde a este proyecto de código en la guía**: §2.2 lo fija para el tipo **`library`** —«mutation score >= 60 % en dominio»— y la fila **`rest-api`**, que es la de este proyecto de código, **no pide mutation score**. Esta categoría lo adopta igual, tomándolo prestado de la fila `library`, porque el traductor de motivos y códigos es la clase de lógica que un puntaje de mutación sí sabe interrogar. **Es más exigencia que la que la guía le pide, no menos**, y se declara así para que nadie lo lea como una obligación de §2.2 sobre `rest-api`. Los valores por encima del piso los sube esta categoría con el fundamento de la columna.

**Además de la cobertura de líneas hay una cobertura contable que no admite promedio**: **15 de 15** puntos de acceso ejercidos, **17 de 17** códigos del contrato recorridos, **4 de 4** puertos conectados y **4** puntos fuera de la guardia. Ésas se cumplen o no se cumplen.

### 2.2 `GeometriaFactory-Domain`

La partición no es en capas de despliegue —no las hay— sino en los **cinco componentes** de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1. El piso global lo fija el intake §17.1.P.6 · GeometriaFactory-Domain y es **90 % de líneas y 85 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`].

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Núcleo de entidades | 90 % | 85 % | 60 % | Piso del intake §17.1.P.6 · GeometriaFactory-Domain |
| Guardas de cuenta | 95 % | 90 % | 60 % | Sube sobre el piso: es el componente donde el P0 del producto y su reincidencia se abrieron (`05` §9, segundo riesgo) |
| Evaluador de admisibilidad | 100 % | 100 % | 60 % | Es la **puerta única** de `INV-06` y de `INV-09` ([`ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md)). Una rama sin cubrir acá es una guarda que nadie ejerce |
| Máquina de estados del trabajo | 95 % | 90 % | 60 % | Sostiene cinco de los nueve invariantes (`05` §10.3) |
| Adopción de la interpretación | 90 % | 85 % | 60 % | Piso del intake |
| **Proyecto de código completo** | **90 %** | **85 %** | **60 %** | Intake §17.1.P.6 · GeometriaFactory-Domain [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 90/85 global es del intake y viene rotulado **[ASUNCIÓN]**: es el valor que el Product Owner tiene pendiente de confirmar. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los tres valores por encima del piso —95, 95 y 100— los sube esta categoría con el fundamento declarado en la columna, que es lo que §2.2 admite («los porcentajes son piso, no techo»).

**La cobertura no se reporta como número global único.** El informe de la etapa `test` se emite por componente, y un 90 % global con el evaluador de admisibilidad en 70 % es un incumplimiento aunque el promedio cierre.

### 2.3 `GeometriaFactory-Application`

La partición no es en capas de despliegue —no las hay— sino en los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1. El piso global lo fija el intake §17.1.P.6 · GeometriaFactory-Application y es **85 % de líneas y 80 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`].

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Guarda de autorización | 100 % | 100 % | 60 % | Sube sobre el piso: es el **único** componente donde se cierran `INV-02`, `INV-03` e `INV-09`, y `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que saltee la cuarta comprobación. Una rama sin cubrir acá es una guarda que nadie ejerce |
| Declaración de puertos | **No aplica** | **No aplica** | **No aplica** | Son declaraciones, no lógica: no tienen líneas ejecutables que cubrir. Se verifican por su **uso** en los once casos de uso y por `TC-04027`. Declarar un umbral acá sería declarar una medición sin sujeto |
| Orquestación del alta de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene los dos caminos de alta con estados iniciales opuestos, que ya produjeron un defecto de fusión corregido en la categoría 02 |
| Orquestación del gobierno de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: contiene el arrastre de la baja —caso testigo de la unidad de trabajo— y el reseteo, que pone la marca |
| Orquestación del ingreso y la credencial | 95 % | 90 % | 60 % | Sube sobre el piso: es el único lugar donde la marca **se levanta** (`CU-04003` FA-05), y donde la admisibilidad devuelve sus motivos sin colapsarlos |
| Orquestación del trabajo | 85 % | 80 % | 60 % | Piso del intake §17.1.P.6 · GeometriaFactory-Application |
| Orquestación de la consulta | 85 % | 80 % | 60 % | Piso del intake |
| Orquestación del desenlace | 85 % | 80 % | 60 % | Piso del intake |
| **Proyecto de código completo** | **85 %** | **80 %** | **60 %** | Intake §17.1.P.6 · GeometriaFactory-Application [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 85/80 global es del intake y viene rotulado **[ASUNCIÓN]**: es el valor que el Product Owner tiene pendiente de confirmar. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los cuatro valores por encima del piso —100, 90, 90 y 95— los sube esta categoría con el fundamento declarado en la columna, que es lo que §2.2 admite («los porcentajes son piso, no techo»).

**La cobertura no se reporta como número global único.** El informe de la etapa `test` se emite por componente, y un 85 % global con la guarda de autorización en 70 % es un incumplimiento aunque el promedio cierre.

### 2.4 `GeometriaFactory-Infrastructure`

La partición es por los **ocho** componentes de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1. El piso global lo fija el intake §17.1.P.6 · GeometriaFactory-Infrastructure y es **85 % de líneas y 80 % de ramas**; el validador tiene un piso propio de **95 % de líneas**. Los tres valores vienen rotulados **[ASUNCIÓN del intake §22, asunción `A-3`]**.

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Contexto de persistencia y mapeo | 85 % | 80 % | 60 % | Piso del intake §17.1.P.6 · GeometriaFactory-Infrastructure |
| Adaptador de repositorio de trabajos | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene el texto original conservado —tramo principal de `RN-06008`— y la proyección de listado, donde `05` §9 declara probabilidad **media-alta** de arrastrar componentes por defecto |
| Adaptador de repositorio de cuentas | 90 % | 85 % | 60 % | Sube sobre el piso: sostiene la unicidad como segunda línea y **la marca que viaja sin ser un estado de cuenta** |
| Motor de interpretación de figuras | **95 %** | 90 % | 60 % | **Piso propio del intake §17.1.P.6 · GeometriaFactory-Infrastructure**: es el número más alto del producto y está donde la fuente señala el criterio que más veces se rompe. El 90 de ramas lo sube esta categoría |
| Motor de verificación de valores | **95 %** | 90 % | 60 % | Ídem: los dos motores son «el validador de figuras» al que el intake le asigna el 95 |
| Adaptador de reloj del sistema | 100 % | 100 % | — | Es el contrato más corto de la capa y no tiene ramas que valga la pena dejar sin cubrir. **Sin mutation score**: un umbral de mutación sobre una operación de una línea no aporta información |
| Mecanismo de credenciales | 95 % | 90 % | 60 % | Sube sobre el piso: contiene la producción de la provisoria, cuyo modo de falla `05` §9 declara de impacto **muy alto** y que **no se nota hasta que alguien la usa** |
| Mecanismo de acceso firmado y preparación del almacén | 95 % | 90 % | 60 % | Sube sobre el piso: contiene los otros dos modos de falla de impacto muy alto —emitir sin clave y recrear el almacén en lugar de transformarlo— |
| **Proyecto de código completo** | **85 %** | **80 %** | **60 %** | Intake §17.1.P.6 · GeometriaFactory-Infrastructure [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 85/80 global y el **95 de líneas del validador** son del intake y vienen rotulados **[ASUNCIÓN]**. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library`, y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los valores de ramas por encima del piso y los tres componentes que suben a 90 o 95 los sube esta categoría con el fundamento de la columna.

**La cobertura no se reporta como número global único.** Un 85 % global con el motor de interpretación en 80 % es un incumplimiento aunque el promedio cierre, porque el 95 del validador es un piso propio y no un promedio ponderado.

## 3. Tooling

### 3.1 `GeometriaFactory-Api`

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a`.

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Integración por protocolo | Un **anfitrión de aplicación en memoria** que levanta el proceso real y permite golpearlo por su protocolo, contra el almacén real. Es lo que el intake §17.1.P.6 · GeometriaFactory-Api declara para `GeometriaFactory.Integration.Tests` |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Medición del percentil y del caudal | Un cliente de carga acotada, ejecutado dentro de la batería de integración, **midiendo en el servidor y no en el cliente** |
| Verificación forzando la petición | Un cliente de peticiones que arma la solicitud **sin pasar por la interfaz**, con la credencial de una sesión válida |
| Inspección estructural | El propio marco de pruebas, recorriendo la tabla de puntos de acceso, la tabla de traducción y el grafo de la composición de raíz |
| Colección de peticiones reproducible | Un archivo de colección versionado con el código, ejecutable a mano o por línea de órdenes. **No es una prueba automatizada**: es la forma de demostración que el intake declara |

**No se nombra ningún producto comercial.** La única decisión de herramienta que la fuente sí acota es la clase de anfitrión en memoria, y se la nombra por su función.

### 3.2 `GeometriaFactory-Domain`

Se nombran por función y no por producto, que es la convención que las categorías 03 y 05 de este proyecto de código ya siguen. La elección concreta y su anclaje de versión son de la etapa `a` (intake, encabezado de la Parte C: regla de anclaje de versiones).

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit e integración interna | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco. Sin marcos de dobles: el intake §17.1.P.6 · GeometriaFactory-Domain declara «sin dobles» |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6 |
| Pruebas basadas en propiedades | Marco de generación de casos de la plataforma, sólo donde §4 lo declara |
| Inspección estructural | El propio marco de pruebas, leyendo el archivo de proyecto y el conjunto de códigos emitidos |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a` y el nombre no cambia nada de esta estrategia.

### 3.3 `GeometriaFactory-Application`

Se nombran por función y no por producto, que es la convención que las categorías 03 y 05 de este proyecto de código ya siguen. La elección concreta y su anclaje de versión son de la etapa `a` (intake, encabezado de la Parte C: regla de anclaje de versiones).

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Dobles de los cuatro puertos | **Dobles escritos a mano o con marco de dobles, indistintamente.** Lo que sí se fija es que son **dobles de puerto** y nunca de un componente interno: la frontera que se sustituye es la que declara [`ADR-04002`](../05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), ver §5 |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 |
| Medición del tiempo del caso de uso más pesado | Cronometrado dentro de la batería unitaria, con doble del puerto de validación y **sin acceso a base**, según `BT-04019` |
| Inspección estructural | El propio marco de pruebas, leyendo el archivo de proyecto, el conjunto de códigos emitidos y la proyección devuelta por las consultas |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a` y el nombre no cambia nada de esta estrategia.

### 3.4 `GeometriaFactory-Infrastructure`

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a`, con dos puntos abiertos propios: **cuál de las dos funciones de derivación de clave se ancla** (`05` §11 `PA-03`) y la versión del motor de almacenamiento embebido.

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Integración interna | El mismo marco, con un **almacén efímero creado y descartado por cada prueba**, y con la ubicación del almacén recibida por configuración de prueba |
| Aserciones | Biblioteca de aserciones del mismo marco |
| Dobles | Sólo donde hace falta aislar el mundo: la **fuente de material impredecible**, para poder simular que no responde (`TC-06028`), y el **almacén interrumpido a mitad de operación** (`TC-06021`). El reloj **no se dobla acá**: acá se implementa |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente **y con un informe acotado a los dos motores**, que es lo que `QG-06` mide |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Medición del tiempo de interpretación | Cronometrado dentro de la batería unitaria, **sin almacén**, que es la condición que el intake §17.1.P.10 · GeometriaFactory-Infrastructure declara |
| Inspección estructural | El propio marco de pruebas, leyendo las dependencias de los dos motores, el conjunto de códigos emitidos y el registro del servidor |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a`.

## 4. Especificaciones Given-When-Then

### 4.1 `GeometriaFactory-Api`

**Los criterios de aceptación de las treinta historias ya están escritos en Given/When/Then**, y la Definition of Ready lo exige.

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe citando la historia de origen.

**Y hay un motivo propio de este proyecto de código**: la **colección de peticiones reproducible** de `CU-00012` ya es una enunciación ejecutable de un recorrido por la superficie. Un juego de archivos de escenario paralelo produciría **tres** enunciaciones del mismo recorrido —la historia, la colección y el escenario— y ninguna sería la fuente.

**Dónde sí se usan pruebas basadas en propiedades:**

| Propiedad | Enunciado |
| --- | --- |
| Cobertura de la guardia | Para todo punto de acceso de los quince, o está dentro de la guardia, o pertenece al conjunto declarado de **cuatro** exenciones. No hay tercera posibilidad |
| Indistinguibilidad | Para cada una de las tres familias empobrecidas, las dos respuestas comparadas son idénticas en cuerpo y en código de respuesta |
| Conjunto cerrado de códigos | Para toda respuesta de fallo, el código del contrato que lleva pertenece al conjunto cerrado de **diecisiete**, y su código de respuesta es el que la tabla de traducción declara |
| Integridad del texto | Para todo texto enviado y aceptado, lo guardado es idéntico carácter por carácter; y para todo texto por encima del límite, **se rechaza y no se trunca** |

### 4.2 `GeometriaFactory-Domain`

**Los criterios de aceptación de las veintisiete historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos un camino feliz y un caso de borde ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio, que es el defecto que este corpus tiene documentado como el que más veces volvió.

**Dónde sí se usan pruebas basadas en propiedades**, que son la otra forma de especificación de esta estrategia:

| Propiedad | Enunciado |
| --- | --- |
| Terminación controlada | Para toda operación y todo estado inicial admisible, o el efecto se aplica entero o la entidad queda como estaba (`05` §4, última viñeta) |
| Conjunto cerrado de condiciones | Para toda invocación que rechaza, el código devuelto pertenece a las **42** condiciones del catálogo |
| Indistinguibilidad | Para todo trabajo ajeno y todo trabajo inexistente, el resultado de `CU-02009` es el mismo (`RN-02003`, `INV-02`) |
| Terminalidad | Para todo trabajo en `Finalizado` o en `Rechazado` y toda transición, el resultado es rechazo (`INV-07`) |

### 4.3 `GeometriaFactory-Application`

**Los criterios de aceptación de las treinta y dos historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos dos escenarios, uno de camino feliz y uno de borde ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio, que es el defecto que este corpus tiene documentado como el que más veces volvió.

**Dónde sí se usan pruebas basadas en propiedades**, que son la otra forma de especificación de esta estrategia:

| Propiedad | Enunciado |
| --- | --- |
| Terminación controlada | Para todo caso de uso y todo estado inicial admisible, o el efecto se aplica entero o el estado queda como estaba y se devuelve la condición (`05` §4) |
| Conjunto cerrado de condiciones | Para toda invocación que rechaza, el código devuelto pertenece a las **36** condiciones del catálogo |
| Indistinguibilidad | Para todo trabajo ajeno y todo identificador inexistente, el motivo emitido es el mismo (`RN-04003`, `INV-02`) |
| Precedencia de la cuarta comprobación | Para toda cuenta con la marca puesta y todo caso de uso salvo el reemplazo de `CU-04003` FA-05, el motivo emitido es `CAMBIO_DE_CONTRASENA_PENDIENTE` **cualquiera sea** el resultado de las otras tres comprobaciones |

### 4.4 `GeometriaFactory-Infrastructure`

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

### 5.1 `GeometriaFactory-Api`

**Política de dobles: ninguno en la batería de integración.** Es la definición misma de esa batería: golpea la superficie real contra el almacén real. Doblar algo ahí la convertiría en otra cosa y dejaría al producto **sin ninguna verificación de que el cable está conectado**.

**Dónde sí se sustituye, y es la única sustitución admitida:**

| Sustitución | Cuándo | Por qué |
| --- | --- | --- |
| Resultados tipados de la capa de aplicación | Las pruebas unitarias del **traductor** | Recorrer los diecisiete códigos por la superficie exigiría provocar quince estados del sistema; el traductor se verifica entero sobre el conjunto cerrado sin levantar el proceso |
| Preparación del almacén **que falla** | `TC-00031` | Es la única forma de verificar que **no se atiende ninguna petición** con la preparación incompleta |
| Ausencia de un adaptador | `TC-00028` | Verifica que la composición de raíz **falla en construcción** y no en la primera petición |

Fixtures compartidos:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Almacén efímero preparado, y uno inexistente | El ambiente de la batería de integración en sus dos estados | Todas las pruebas por protocolo lo necesitan, y ninguna debe heredar el estado de otra |
| Accesos firmados en sus formas | Válido de alumno, válido de administrador, **vencido**, **con firma ajena**, y de una cuenta **con la marca puesta** | Son las cinco entradas de la guardia de admisión |
| Cuentas y trabajos en sus estados | Cuentas en los tres estados con y sin la marca; trabajos en los cuatro estados, propios y ajenos | Los puntos de gobierno, de trabajos y de desenlace se ejercen contra los mismos |
| Los textos de los escenarios del intake | Los **ocho** textos literales de §20 | Ver §6 |

### 5.2 `GeometriaFactory-Domain`

**Política de dobles: ninguno.** El intake §17.1.P.6 · GeometriaFactory-Domain declara «pruebas unitarias puras y sin dobles», y este proyecto de código lo permite porque no tiene dependencias que aislar. Lo que en otros proyectos de código exigiría un doble —el reloj y la unicidad del correo— acá **entra por parámetro** ([`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)): la prueba pasa el momento y la afirmación de unicidad como valores, y por eso es reproducible sin fijar el reloj del entorno.

Fixtures que sí existen, todos como **constructores de entidad** compartidos:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Cuenta de alumno en cada uno de sus tres estados | `Pendiente`, `Habilitado`, `Bloqueado`, con y sin la marca de cambio de contraseña pendiente | Seis combinaciones que aparecen en `CU-02002`, `CU-02003`, `CU-02004` y `CU-02013` |
| Cuenta de administrador | Única, `Habilitado`, con credencial derivada | `INV-05` e `INV-08` la exigen en esa forma y sólo en esa |
| Trabajo en cada uno de sus cuatro estados | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado` | Las transiciones y la terminalidad se prueban contra los cuatro |
| Resultados de interpretación de los escenarios del intake | Los conjuntos de piezas y observaciones que corresponden a `E-1` a `E-8`, ver §6 | Es el material que hace comparables las pruebas de este proyecto de código con las de `GeometriaFactory-Infrastructure` |

**Regla de duplicación:** un caso de prueba que necesite una variante de un fixture la deriva del constructor compartido y no lo copia. Un segundo constructor equivalente es un hallazgo de revisión.

### 5.3 `GeometriaFactory-Application`

**Política de dobles: sólo de puerto, y de ningún otro lugar.** Los **cuatro** puertos de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 son la única frontera que una prueba sustituye. Un doble de un componente interno —de la guarda, de un orquestador— es un hallazgo de revisión: rompe la propiedad de que el caso de uso se ejerce **entero**, que es lo que el intake §17.1.P.6 · GeometriaFactory-Application pide probar.

Los cuatro dobles, con lo que cada uno tiene que poder simular:

| Doble de puerto | Qué tiene que poder simular |
| --- | --- |
| Repositorio de trabajos | Trabajo existente propio, existente ajeno, inexistente, y trabajos en los cuatro estados; una consulta ya acotada; el retiro con arrastre; y la indisponibilidad |
| Validación de figuras | Los resultados de interpretación de los ocho escenarios del intake §20 —piezas, observaciones y **la cantidad de figuras del conjunto raíz**— y la **indisponibilidad**, que `US-04016` exige |
| Reloj del sistema | Un momento fijo, elegido por la prueba, y dos momentos distintos consecutivos |
| Repositorio de cuentas | Cuenta en cada uno de sus tres estados, con y sin la marca; correo ya registrado y no registrado; administrador existente y ausente; y la materialización con la marca |

Fixtures compartidos, todos como **constructores**:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Solicitante en sus cuatro formas | Alumno sin marca, alumno con marca, administrador sin marca, administrador con marca | Es la entrada de las cuatro comprobaciones y aparece en los once casos de uso |
| Cuenta de alumno en cada uno de sus tres estados | `Pendiente`, `Habilitado`, `Bloqueado`, con y sin la marca | Seis combinaciones que aparecen en `CU-04001`, `CU-04002`, `CU-04003` y `CU-04011` |
| Trabajo en cada uno de sus cuatro estados | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, propio y ajeno | El alcance, la pertenencia y la terminalidad se prueban contra los ocho pares |
| Resultados de interpretación de los escenarios del intake | Los conjuntos de piezas, observaciones y cantidad de figuras que corresponden a `E-1` a `E-8`, ver §6 | Es el material que hace comparables las pruebas de este proyecto de código con las de `GeometriaFactory-Infrastructure`, que es quien los produce de verdad |

**Regla de duplicación:** un caso de prueba que necesite una variante de un fixture la deriva del constructor compartido y no lo copia. Un segundo constructor equivalente es un hallazgo de revisión.

### 5.4 `GeometriaFactory-Infrastructure`

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

### 6.1 `GeometriaFactory-Api`

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** Los **ocho** escenarios `E-1` a `E-8` del intake §20 son datos salidos de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, con su procedencia y su estado declarado. §21 los cruza contra la batería obligatoria de nueve casos de la fuente técnica **más un décimo** que esa misma sección agrega.

**Cómo los usa este proyecto de código.** Acá los escenarios entran **como cuerpo de una petición**, que es la forma en que llegan de verdad desde el front. Lo que esta capa verifica sobre ellos **no es la interpretación** —que es de `GeometriaFactory-Infrastructure`— sino tres cosas propias del borde:

| Escenario | Qué verifica en esta capa | Fuente del valor |
| --- | --- | --- |
| `E-1` | Que el envío **responde con éxito** transportando el estado que la interpretación decidió, con sus **3 piezas y 2 advertencias** en el cuerpo; y que lo guardado es **idéntico carácter por carácter** a lo enviado | §20.E-1, «Qué verificar» puntos 5 y 6 |
| `E-2` | Que el texto **no se normaliza en el borde**: sus dos comas finales llegan intactas al almacén | §20.E-2, punto 1 |
| `E-5` | Que un envío cuyo texto **no verifica** responde con **éxito** y no con un código de fallo, transportando el estado `Borrador` y las observaciones en el cuerpo. Es el quinto riesgo de `05` §9 | §20.E-5, punto 4 |
| `E-8` | Lo mismo que `E-5`, con el otro modo de falla: **error y no advertencia**, con el trabajo en `Borrador` | §20.E-8, punto 5 |
| `E-3`, `E-4`, `E-6`, `E-7` | **La colección de peticiones reproducible y la batería del validador que corre desde acá.** No tienen verificación propia de borde: lo que ejercitan es la interpretación, que es de otra capa | §21; intake §17.1.P.8 · GeometriaFactory-Api |

**Lo que esta capa nunca hace con un escenario**: interpretarlo, normalizarlo, truncarlo o rechazarlo por su contenido geométrico. Un envío con errores de interpretación **es una respuesta exitosa**.

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**. Un dato de prueba de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización.

**Lo que no se inventa.** La **colección de peticiones reproducible** tiene como NFR **0 datos de prueba inventados** (`05` §8): sus cuerpos salen de los escenarios del intake y sus identidades son valores evidentemente ficticios, declarados como tales.

### 6.2 `GeometriaFactory-Domain`

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado —`medido`, `derivado` o `reconstruido`—. §21 los cruza contra la batería obligatoria de **diez** casos de prueba —los **nueve** de la fuente técnica más el **décimo** que esa misma sección agregó el 2026-08-09 para la dimensión no legible—.

**Cómo los usa este proyecto de código, que es la parte que hay que decir con precisión.** El dominio **no interpreta el texto del alumno**: la interpretación es de `GeometriaFactory-Infrastructure` y la reconstrucción de piezas le llega ya producida ([`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §1). De cada escenario, entonces, lo que entra acá **no es el texto sino su resultado**: el conjunto de piezas y de observaciones que el escenario declara en su bloque «Qué verificar».

| Escenario | Qué aporta a las pruebas de este proyecto de código | Fuente del valor |
| --- | --- | --- |
| `E-1` | Conjunto de 3 piezas y 2 advertencias, sin errores. El trabajo **pasa a `Pendiente` al enviarlo** | §20.E-1, punto 6 de «Qué verificar» |
| `E-2` | 1 pieza con 2 bases y 4 laterales, 1 advertencia de volumen y ningún error. **Pasa a `Pendiente` con la advertencia asociada** | §20.E-2, puntos 4, 6 y 7 |
| `E-3` | Advertencia de área con el par declarado 36.00 y derivado 54.00. Es el caso insignia de `ADVERTENCIA_SIN_LOS_DOS_VALORES` | §20.E-3, punto 2 |
| `E-4` | **Cero observaciones en total.** Es el criterio negativo: el envío pasa a `Pendiente` sin ninguna observación que adoptar | §20.E-4, punto 4 |
| `E-5` | Observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**; la primera pieza, válida, se interpreta igual. El trabajo **queda en `Borrador`** | §20.E-5, puntos 1 a 4 |
| `E-6` | Una figura que **se interpreta** y produce a lo sumo una advertencia; el trabajo pasa a `Pendiente` | §20.E-6, puntos 1 a 3 |
| `E-7` | Conjunto de 6 piezas que cubre los seis tipos, tres volumétricos y tres planos. Ejercita la derivación de familia de `US-02012` | §20.E-7, puntos 1 y 3 |
| `E-8` | **El desenlace del envío es error, no advertencia** [DECISIÓN 2026-08-09]: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige `RN-02009` | §20.E-8, punto 5 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un fixture de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización. Si el intake cambia un escenario, el cambio baja acá como una corrección con su fila de control de cambios.

**Lo que no se inventa.** Ningún caso de prueba de este proyecto de código introduce un payload de figuras que no esté en §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un momento— se usa un valor evidentemente ficticio y se declara como tal en el `TC-XX`: son datos de identidad, no datos de geometría, y el intake no los fija.

### 6.3 `GeometriaFactory-Application`

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado —`medido`, `derivado` o `reconstruido`—. §21 los cruza contra la batería obligatoria de **nueve** casos de prueba de RT §11, más un décimo que esa misma sección agrega.

**Cómo los usa este proyecto de código, que es la parte que hay que decir con precisión.** Esta capa **no interpreta el texto del alumno**: la interpretación es de `GeometriaFactory-Infrastructure` y llega por el puerto de validación de figuras ([`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3). De cada escenario, entonces, lo que entra acá **no es el texto sino el resultado que el doble del puerto devuelve**: piezas, observaciones y la cantidad de figuras del conjunto raíz. El texto original sí viaja íntegro por la capa, y eso es lo que `RN-04008` exige verificar.

| Escenario | Qué aporta a las pruebas de este proyecto de código | Fuente del valor |
| --- | --- | --- |
| `E-1` | 3 piezas y 2 advertencias, sin errores. El envío **pasa a `Pendiente`**. Es además el material del NFR de 500 ms | §20.E-1, «Qué verificar» puntos 5 y 6 |
| `E-2` | 1 pieza, 1 advertencia de volumen y ningún error. **Pasa a `Pendiente` con la advertencia asociada** | §20.E-2, puntos 4, 6 y 7 |
| `E-3` | Advertencia de área con el par declarado 36.00 y derivado 54.00, que el mensaje debe expresar entero. **El trabajo no se rechaza** | §20.E-3, puntos 2 y 4 |
| `E-4` | **Cero observaciones en total.** Es el criterio negativo: el envío pasa a `Pendiente` sin ninguna observación que incorporar | §20.E-4, punto 4 |
| `E-5` | Observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**; la primera pieza, válida, se interpreta igual. El trabajo **queda en `Borrador`** con su texto conservado | §20.E-5, puntos 1 a 4 |
| `E-6` | Una figura que **se interpreta** y produce a lo sumo una advertencia; el trabajo pasa a `Pendiente` | §20.E-6, puntos 1 a 3 |
| `E-7` | Conjunto de 6 piezas que cubre los seis tipos dibujables. Ejercita el detalle con piezas y componentes de `US-04019` frente al listado sin componentes | §20.E-7, puntos 1 y 3 |
| `E-8` | **El desenlace del envío es error, no advertencia** [DECISIÓN 2026-08-09]: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige `RN-04009` | §20.E-8, punto 5 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un fixture de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización. Si el intake cambia un escenario, el cambio baja acá como una corrección con su fila de control de cambios.

**Lo que no se inventa.** Ningún caso de prueba de este proyecto de código introduce un resultado de interpretación que no corresponda a un escenario de §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un identificador de trabajo, un momento— se usa un valor evidentemente ficticio y se declara como tal en el `TC-XX`: son datos de identidad y de orquestación, no datos de geometría, y el intake no los fija.

### 6.4 `GeometriaFactory-Infrastructure`

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

Es la tabla de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §10.5, con la columna que a esta categoría le toca: **qué caso de prueba la materializa**. Ninguna fila se agrupa y ninguna se agrega.

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

### 7.1 `GeometriaFactory-Api`

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, con el proceso levantado por el anfitrión en memoria de la batería de integración |
| Almacén | **Real y efímero**: el mismo motor que en producción, en un archivo creado y descartado por la batería. **Nunca el almacén de desarrollo ni el de producción** |
| Aislamiento entre pruebas | Cada prueba de integración parte de un almacén preparado y conocido. **El paralelismo entre pruebas que comparten archivo de almacén no es admisible**: el motor es de **escritor único** |
| Secretos | La clave de firma de las pruebas es un valor **evidentemente ficticio**, provisto por configuración de prueba. **Ningún secreto real entra al repositorio, ni en el pipeline** |
| Transporte | **Sin canal de sesión interactiva.** El intake §17.1.P.3 · GeometriaFactory-Api declara que esta superficie **no expone ni requiere** ese canal, y que es criterio de aceptación de la etapa `a`. `TC-00036` lo verifica |
| Intercambio de origen cruzado | **No configurado**, porque el navegador no alcanza esta superficie. `TC-00036` verifica su ausencia |
| Medición del percentil y del caudal | **En el servidor**, sin contar el tramo de red doméstica, que el intake declara fuera de control |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** Los tres tiempos declarados —percentil, caudal y arranque en frío— son del **servicio**, no de la suite, y vienen del intake con su rótulo [ASUNCIÓN]. Ninguna fuente da un tiempo de suite, y no se inventa uno |

### 7.2 `GeometriaFactory-Domain`

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake, encabezado de la Parte C, y §17.1.P.9 · GeometriaFactory-Domain) |
| Aislamiento entre pruebas | Total y por construcción: no hay estado compartido entre invocaciones, no hay caché y no hay registro estático (`05` §4). Ninguna prueba depende del orden de ejecución |
| Paralelismo | Admitido. `05` §4 declara que la batería puede correr en paralelo porque ninguna prueba comparte estado |
| Base de datos | **Ninguna.** `tiene_persistencia` es false |
| Variables de entorno y secretos | **Ninguno.** El proyecto de código no lee configuración (`05` §7) y la contraseña llega ya derivada |
| Reloj | **No se fija ni se simula.** El momento entra por parámetro, de modo que la prueba lo elige ([`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)) |
| Duración | La batería completa en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain]. **Ningún otro tiempo de ejecución se declara acá**: ninguna fuente da otro |

### 7.3 `GeometriaFactory-Application`

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake, encabezado de la Parte C, y §17.1.P.9 · GeometriaFactory-Application) |
| Aislamiento entre pruebas | Total y por construcción: no hay estado compartido entre invocaciones, no hay caché y no hay registro estático (`05` §4). Ninguna prueba depende del orden de ejecución |
| Paralelismo | Admitido. `05` §4 declara que la batería puede correr en paralelo porque ninguna prueba comparte estado ni base |
| Base de datos | **Ninguna, y el umbral es exactamente 0.** `tiene_persistencia` es false y el intake §17.1.P.8 · GeometriaFactory-Application declara la puerta propia: una prueba de esta capa que abra el almacén real **está mal ubicada** |
| Variables de entorno y secretos | **Ninguno.** El proyecto de código no lee configuración (`05` §7) y la contraseña llega ya derivada, la provisoria ya producida y ya derivada |
| Reloj | **No se fija ni se simula el reloj del entorno.** El momento entra por el puerto de reloj, de modo que la prueba lo elige. Es lo que el intake §17.1.P.11 · GeometriaFactory-Application punto 3 declara que el puerto existe para permitir |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** El único tiempo que este proyecto de código tiene declarado es el del caso de uso más pesado —**500 ms** sobre `E-1`, [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application]—, que es una medición por caso de uso y no de la suite. Ninguna fuente da un tiempo de suite para esta capa, y no se inventa uno |

### 7.4 `GeometriaFactory-Infrastructure`

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake §17.1.P.9 · GeometriaFactory-Infrastructure) |
| Almacén | **Efímero, creado y descartado por cada prueba de integración interna.** Nunca el almacén de desarrollo ni el de producción. Su ubicación **se recibe por configuración de prueba y no se busca** |
| Aislamiento entre pruebas | Total. Las unitarias no comparten estado; las de integración interna crean su propio almacén y lo descartan. Ninguna prueba depende del orden de ejecución |
| Paralelismo | **Admitido en el nivel unitario. En integración interna, sólo si cada prueba tiene su propio archivo de almacén**: el motor de almacenamiento del producto es de **escritor único**, y dos pruebas sobre el mismo archivo se bloquearían entre sí |
| Secretos | **Ninguno real.** La clave de firma de las pruebas es un valor evidentemente ficticio, declarado como tal, **provisto por configuración de prueba**. `TC-06030` verifica que **sin clave no hay emisión**, y para eso hace falta poder no proveerla |
| Reloj | **Acá se implementa el reloj, no se consume**: `TC-06031` verifica que el sello sale del puerto. Ninguna prueba fija el reloj del entorno |
| Datos de geometría | Los **ocho** textos del intake §20, literales. **Ningún texto de figuras se escribe a mano** |
| Duración | **No se declara ningún tiempo de ejecución de la batería.** El único tiempo declarado es el de la **interpretación** del texto de `E-1`: menos de **200 ms**, medido sin almacén [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure]. Ninguna fuente da un tiempo de suite para esta capa, y no se inventa uno |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión, salida S1** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0 §4). Pasa de ser la estrategia del proyecto de código `GeometriaFactory-Api` a ser la de la **unidad de entrega**, absorbiendo las de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Las siete secciones llevan **una subsección por proyecto de código**, con su texto **transpuesto sin reescritura**: lo que cambia es el orden y no el contenido. Entra **§0**, que declara lo que sólo se ve con los cuatro juntos: los **cuatro pisos de cobertura** —90/85, 85/80, 85/80 y 75/70—, que **no se promedian**, con la constancia de que el único que baja el de su tipo es el del host y de que **su ADR sigue faltando**; y las **cuatro pirámides**, donde se ve que la invertida del host y la ausencia de integración en la capa de aplicación **son la misma decisión vista desde dos lados**, cosa que leídas por separado no se veía. La cabecera pasa de «Proyecto de código» a **unidad de entrega** y enumera los proyectos que la componen. Los tres documentos absorbidos quedan archivados. Sube **major**. |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-06`.** §2 fijaba el piso global en 75/70 **sin compararlo con la guía**, siendo el único de los siete proyectos de código cuyo piso **baja**: `Rules-Calidad-Y-Pruebas.md` §2.2 fija 80 % de aplicación para el tipo `rest-api` y exige un **ADR** para bajar cobertura. §2 declara ahora el apartamiento, con qué autoridad se hace —el intake §17.1.P.6 · GeometriaFactory-Api, rotulado [ASUNCIÓN]—, que **la autoridad de la fuente no reemplaza a la ADR**, y qué lo compensa. **`H-08`.** El mutation score de 60 % se atribuía a §2.2 sin decir que esa tabla lo pide para el tipo `library` y no para `rest-api`. **Ningún umbral se toca**: el 75/70 no se sube ni se da por justificado por venir de la fuente. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo **invertida** —60 integración, 40 unitario, 0 de extremo a extremo con la persona—, con el motivo que el intake §17.1.P.6 · GeometriaFactory-Api declara y con lo que esa inversión cuesta dicho sin adornos, más las inspecciones de umbral exacto que la compensan. Declara la cobertura por los **ocho** componentes, con el piso más bajo del producto y con la guardia de admisión y el traductor muy por encima de él, y la cobertura contable que no admite promedio. Declara el tooling por función, la política de **cero dobles en la batería de integración** con las tres sustituciones admitidas fuera de ella, y el uso de los **ocho** escenarios del intake §20 **como cuerpo de petición**, con la precisión de que esta capa verifica el borde y no la interpretación. Declara el ambiente, incluida la ausencia de canal de sesión interactiva y de intercambio de origen cruzado, y la constancia de que los tres tiempos declarados son **del servicio y no de la suite**. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **7**. Sube minor. |
