# Criterios de validación — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Criterios-Validacion.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6 y §22
**Trazabilidad downstream:** `09-Devops` y `11-Documentacion`
**Consolida a:** los documentos homónimos de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase, y sus cuatro proyectos de código tenían
el suyo.** Cada sección lleva **una subsección por proyecto**, con su texto **transpuesto sin
reescritura**: lo que cambia es el orden y no el contenido.

**Las seis secciones son comunes a las cuatro capas**, y sus criterios **se suman, no se
seleccionan**: un criterio de validación de una capa no queda satisfecho porque otra capa cumpla el
suyo. La unidad de entrega cumple cuando cumplen las cuatro.

---

## 1. Propósito

### 1.1 `GeometriaFactory-Api`

Define qué significa que `GeometriaFactory-Api` está **validado**. Es el **proyecto de código principal del producto** y una de sus dos unidades de entrega, de modo que acá «validado» quiere decir **que el servicio puede atender a la pieza pública sin perder ninguna decisión tomada adentro y sin exponer nada de lo que la topología protege**.

Los momentos en que se aplican estos criterios son el **punto de control de cada etapa**, que el intake §15 declara bloqueante, y el **momento en que el artefacto se construye y arranca**. **No incluyen el despliegue**: el intake §17.1.P.8 · GeometriaFactory-Api lo declara manual y del Product Owner.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

### 1.2 `GeometriaFactory-Domain`

Define qué significa que `GeometriaFactory-Domain` está **validado**. Como este proyecto de código no es una unidad de despliegue —no tiene proceso propio y no se publica en ningún repositorio de paquetes (`05` §5)—, «validado» no quiere decir «liberado»: quiere decir **que la biblioteca puede sostener la etapa que la usa**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante. No hay una fecha de liberación que preparar, porque el intake declara sin plazo calendario.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

### 1.3 `GeometriaFactory-Application`

Define qué significa que `GeometriaFactory-Application` está **validado**. Como este proyecto de código no es una unidad de despliegue —no tiene proceso propio y no se publica en ningún repositorio de paquetes (`05` §5)—, «validado» no quiere decir «liberado»: quiere decir **que la capa de casos de uso puede sostener la etapa que la usa y las dos capas que dependen de ella**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante. No hay una fecha de liberación que preparar, porque el intake declara sin plazo calendario.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

### 1.4 `GeometriaFactory-Infrastructure`

Define qué significa que `GeometriaFactory-Infrastructure` está **validado**. Como este proyecto de código no es una unidad de despliegue —viaja embebido en el proceso de `GeometriaFactory-Api`—, «validado» no quiere decir «liberado»: quiere decir **que el borde del sistema puede sostener la etapa que lo usa sin perder un dato, sin producir un secreto adivinable y sin engañar al alumno sobre por qué su texto no se interpretó**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

### 2.1 `GeometriaFactory-Api`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **doce** casos de uso tienen al menos un caso de verificación pasado, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **12 de 12** |
| CV-02 | **Los quince puntos de acceso están ejercidos**, que es lo que `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api` | Matriz §5 | **15 de 15** |
| CV-03 | Exactamente **4** puntos quedan fuera de la guardia y **11** bajo ella, verificado **en las dos direcciones** | `TC-00007` | **4 + 11 = 15**, sin gradación |
| CV-04 | Las **treinta** historias de usuario tienen su caso de verificación | Matriz §2, columna de historias | **30 de 30** |
| CV-05 | Las **dieciséis** reglas de negocio tienen verificado el tramo que esta capa ejerce; las **tres** sin tramo tienen verificado que **esta capa no deshaga lo que otra decidió** | Matriz §4 | **16 de 16**, con **13** con tramo y **3** sin él |
| CV-06 | Los **nueve** invariantes tienen verificado lo que esta capa aporta a cada uno | Matriz §6 | **9 de 9** |
| CV-07 | **16 de 17** códigos del contrato tienen traducción declarada y **1** está declarado **sin destino con su motivo**; hay **0** inventados y **0** renombrados | `TC-00024` y `TC-00027`, en las dos direcciones | **14 + 1 = 15**, con **0** y **0** |
| CV-08 | Las **tres** familias empobrecidas dan respuestas **indistinguibles en cuerpo y en código** | `TC-00025` | **3 de 3**, sin gradación |
| CV-09 | Los **ocho** escenarios del intake §20 están ejercitados **como cuerpo de petición**, sin sustituirlos por datos sintéticos | `TC-00017`, `TC-00019`, `TC-00022`, `TC-00035` y la batería del validador que corre desde acá | **8 de 8** |
| CV-10 | Un envío cuyo texto **no verifica** responde con **éxito** y no con un código de fallo | `TC-00017`, con `E-5` y `E-8` | **3 de 3** envíos exitosos con estados distintos |

### 2.2 `GeometriaFactory-Domain`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **trece** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2, columna de estado | **13 de 13** |
| CV-02 | Las **dieciséis** reglas de negocio tienen al menos un caso de prueba en verde | Matriz §4 | **16 de 16** |
| CV-03 | Los **nueve** invariantes tienen al menos una prueba que verifica **su violación rechazada**, y ninguna de esas pruebas usa dobles | Matriz §5, recorrida por `TC-02026` | **9 de 9**, con **0** dobles |
| CV-04 | Las **42** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-02023`, comparación en las dos direcciones | **42 de 42** y **0** fuera |
| CV-05 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-02027` | **0** excepciones de negocio |
| CV-06 | Los **ocho** escenarios del intake §20 están ejercitados como fixture, con sus resultados declarados y **sin sustituirlos por datos sintéticos** | `TC-02013` a `TC-02018`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-07 | Las **veintisiete** historias de usuario tienen su caso de prueba | Matriz §2, columna de test, cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3 | **27 de 27** |

### 2.3 `GeometriaFactory-Application`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **once** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then declarado en sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2, columna de estado | **11 de 11** |
| CV-02 | Las **dieciséis** reglas de negocio tienen al menos un caso de prueba en verde | Matriz §4 | **16 de 16** |
| CV-03 | Las **cuatro** comprobaciones de autorización tienen prueba de su negativa **sin base de datos**, y existe **una sola** prueba de que la cuarta corta antes que las otras tres | Matriz §5, y `TC-04011` | **4 de 4**, con **1** prueba de orden |
| CV-04 | Los **nueve** invariantes tienen al menos un caso de prueba que verifica lo que esta capa aporta a cada uno | Matriz §6 | **9 de 9** |
| CV-05 | Las **36** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-04028`, comparación en las dos direcciones | **36 de 36** y **0** fuera |
| CV-06 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-04031` | **0** excepciones de negocio |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados como resultado de interpretación, **sin sustituirlos por datos inventados** | `TC-04015`, `TC-04016`, `TC-04017` y `TC-04022`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | Las **treinta y dos** historias de usuario tienen su caso de prueba | Matriz §2, columna de historias, cruzada con [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3 | **32 de 32** |

### 2.4 `GeometriaFactory-Infrastructure`

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **diez** casos de uso tienen al menos un caso de prueba en verde, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **10 de 10** |
| CV-02 | **La batería del validador pasa entera**, con los **ocho** escenarios del intake §20 como entrada | Matriz §6 y `TC-06001` a `TC-06010` | **10 de 10.** Ver §6 sobre el recuento |
| CV-03 | Las **veinticinco** historias de usuario tienen su caso de prueba | Matriz §2, columna de historias | **25 de 25** |
| CV-04 | Las **dieciséis** reglas de negocio tienen verificado el tramo que esta capa ejerce, y las **dos** sin tramo tienen verificado que **esta capa guarda el dato y no lo comprueba** | Matriz §4 | **16 de 16**, con **14** con tramo y **2** sin él |
| CV-05 | Las **siete** reglas conceptuales de modelo tienen caso de prueba | Matriz §5 | **7 de 7** |
| CV-06 | Las **17** condiciones del catálogo están alcanzadas por al menos una prueba, y no se emite ninguna condición fuera del catálogo | `TC-06034`, comparación en las dos direcciones | **17 de 17** y **0** fuera |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados **como texto literal**, sin sustituirlos por textos escritos a mano | `TC-06001` a `TC-06011` y `TC-06016`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | El escenario `E-1` produce **3 piezas y exactamente 2 advertencias**, y el cilindro **no produce ninguna observación** | `TC-06009` | 3 y **2**. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto** |
| CV-09 | Un texto **ilegible** produce una observación de validación y **no** la condición de motor no disponible | `TC-06013` | Tres resultados distintos y **0** confusiones entre resultado y fallo |

## 3. Criterios no funcionales

### 3.1 `GeometriaFactory-Api`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. Los cinco primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-11 | Latencia del listado, medida **en el servidor** | **Percentil 99 por debajo de 500 ms** **[ASUNCIÓN, `A-5`]** | `TC-00034` | **Condicionado** |
| CV-12 | Caudal sostenido | **20 peticiones por minuto** **[ASUNCIÓN, `A-5`]** | `TC-00034` | **Condicionado** |
| CV-13 | Arranque en frío: aplica transformaciones y responde salud | **Menos de 30 segundos** **[ASUNCIÓN, `A-5`]** | `TC-00033` | **Condicionado** |
| CV-14 | Cobertura del proyecto de código, **por componente y no como número global** | **75 %** de líneas y **70 %** de ramas **[ASUNCIÓN, `A-3`]** | Informe de cobertura por componente | **Condicionado** |
| CV-15 | Forma de la pirámide de pruebas | **60 %** integración y **40 %** unitarias **[ASUNCIÓN en cuanto al reparto]** | `TC-00037` | **Condicionado.** **La inversión no es asunción** y no queda en suspenso |
| CV-16 | Puntos de acceso fuera de la guardia | **4** sobre **15**, ni uno más | `TC-00007` | **Bloqueante, sin gradación** |
| CV-17 | Puntos que fijan una contraseña sobre una cuenta existente sin credencial | **0** | `TC-00010` | **Bloqueante** |
| CV-18 | Códigos del contrato con traducción declarada, en las dos direcciones | 16 con destino, 1 sin él, 0 inventados, 0 renombrados | `TC-00024`, `TC-00027` | **Bloqueante** |
| CV-19 | Respuestas indistinguibles de las tres familias empobrecidas | **3 de 3** | `TC-00025` | **Bloqueante, sin gradación** |
| CV-20 | Respuestas que exponen dirección, ruta, secreto o traza, sobre los quince puntos **y** sobre el registro del servidor | **0** | `TC-00026` | **Bloqueante** |
| CV-21 | Configuraciones de intercambio declaradas en el producto | **1**, compartida por los dos extremos | `TC-00029` | **Bloqueante** |
| CV-22 | Caracteres de diferencia entre el texto enviado y el guardado, y truncamientos silenciosos | **0** y **0** | `TC-00019` | **Bloqueante, sin gradación** |
| CV-23 | Puertos conectados a su adaptador | **4 de 4**, con fallo **en construcción** si falta alguno | `TC-00028` | **Bloqueante** |
| CV-24 | Peticiones atendidas con la preparación del almacén incompleta | **0** | `TC-00031` | **Bloqueante** |
| CV-25 | Eliminaciones fuera de alcance aceptadas **al forzar la petición** | **0** | `TC-00020` | **Bloqueante.** Es el **único** criterio del producto que la fuente exige ejercer forzando la petición contra esta superficie |
| CV-26 | Advertencias de construcción | **0** | Etapa `build`; intake §17.1.P.8 · GeometriaFactory-Api | **Bloqueante** |
| CV-27 | Pasos de la colección de peticiones reproducible, y datos de prueba inventados en ella | **5 o menos**, y **0** | `TC-00035` | **Bloqueante al cierre de la etapa que la incorpora** |

**No hay criterio de disponibilidad, y es correcto que no lo haya.** El intake declara «sin SLO»: el servidor es domiciliario y su caída se responde con **estado degradado en el front**, no con redundancia.

**No se declara ningún tiempo de ejecución de la batería.** Los tres tiempos de este proyecto de código —`CV-00011`, `CV-00012` y `CV-00013`— son **del servicio** y vienen del intake con su rótulo.

### 3.2 `GeometriaFactory-Domain`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. Los dos primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake y **no son compromisos** hasta que el Product Owner los confirme.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-08 | La batería de dominio completa termina en menos de **10 segundos** | 10 s **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain, asunción `A-5` de §22]** | Duración total reportada por el ejecutor en la etapa `test` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-09 | La cobertura alcanza **90 %** de líneas y **85 %** de ramas, **por componente y no como número global** | 90 / 85 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain, asunción `A-3` de §22]**, con los tres componentes que suben declarados en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | Informe de cobertura por componente de la etapa `test` | **Condicionado** |
| CV-10 | El archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | 0 y 0 | `TC-02024` y revisión del pull request | **Bloqueante** |
| CV-11 | El catálogo de condiciones cierra en las dos direcciones | 42 de 42 y 0 fuera | `TC-02023` | **Bloqueante** |
| CV-12 | Los nueve invariantes están ejercidos sin dobles | 9 de 9, 0 dobles | `TC-02026` | **Bloqueante** |
| CV-13 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.1.P.8 · GeometriaFactory-Domain | **Bloqueante** |

**No hay criterio de latencia, de throughput ni de disponibilidad, y es correcto que no lo haya**: este proyecto de código no atiende peticiones ni abre conexiones (`05` §8, cierre de la sección). Inventar un umbral de esos tres sería inventar un sujeto que no existe.

**No se declara ningún otro tiempo de ejecución.** El único que existe es el de `CV-02008`, y viene del intake con su rótulo.

### 3.3 `GeometriaFactory-Application`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. Los dos primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake y **no son compromisos** hasta que el Product Owner los confirme.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-09 | El caso de uso más pesado resuelve el envío del texto de **3** piezas de `E-1` **sin acceso a base** | **500 ms** **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Application, asunción `A-5` de §22]** | Cronometrado dentro de la batería unitaria con doble del puerto de validación, por `BT-04019` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-10 | La cobertura alcanza **85 %** de líneas y **80 %** de ramas, **por componente y no como número global** | 85 / 80 **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Application, asunción `A-3` de §22]**, con los cuatro componentes que suben declarados en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | Informe de cobertura por componente de la etapa `test` | **Condicionado** |
| CV-11 | Ninguna prueba de esta capa toca la base de datos real | **0** | `TC-04026` y revisión del pull request | **Bloqueante.** Es la puerta propia que el intake §17.1.P.8 · GeometriaFactory-Application declara |
| CV-12 | El archivo de proyecto declara **1** referencia al producto y **0** a persistencia, transporte, serialización o marco web | 1 y 0 | `TC-04027` | **Bloqueante** |
| CV-13 | Las consultas de listado no materializan componentes de pieza | **0** en los dos listados | `TC-04030` | **Bloqueante** |
| CV-14 | El catálogo de condiciones cierra en las dos direcciones | 36 de 36 y 0 fuera | `TC-04028` | **Bloqueante** |
| CV-15 | Las cuatro comprobaciones están ejercidas sin base de datos, con la prueba de orden presente | 4 de 4, 1 de orden | `TC-04011` y matriz §5 | **Bloqueante** |
| CV-16 | Ningún caso de uso reparte su efecto entre dos unidades de trabajo | **A lo sumo 1** por caso de uso | `TC-04029`, con la baja como caso testigo | **Bloqueante** |
| CV-17 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.1.P.8 · GeometriaFactory-Application | **Bloqueante** |

**No hay criterio de throughput ni de disponibilidad, y es correcto que no lo haya**: este proyecto de código no atiende peticiones ni abre conexiones (`05` §8, cierre de la sección). Inventar un umbral de esos dos sería inventar un sujeto que no existe.

**No se declara ningún tiempo de ejecución de la batería.** El único tiempo de este proyecto de código es el de `CV-04009`, que es por caso de uso y viene del intake con su rótulo. Ninguna fuente da un tiempo de suite para esta capa.

### 3.4 `GeometriaFactory-Infrastructure`

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. Los tres primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-10 | La interpretación del texto de **3** piezas de `E-1` termina **sin almacén** | **200 ms** **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Infrastructure, asunción `A-5` de §22]** | `TC-06015` | **Condicionado**: se mide y se registra; no bloquea hasta la confirmación |
| CV-11 | La cobertura del proyecto de código, **por componente y no como número global** | **85 %** de líneas y **80 %** de ramas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure, asunción `A-3`]** | Informe de cobertura por componente | **Condicionado** |
| CV-12 | La cobertura del **validador de figuras**, medida sobre los **dos motores** | **95 %** de líneas **[ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Infrastructure]**. Es el número más alto del producto | Informe acotado a los dos motores | **Condicionado** |
| CV-13 | La comparación de valores usa tolerancia **0.01** absoluta con operador **estricto** | 0.01, estricto | `TC-06009` | **Bloqueante, y no condicionado.** El intake §22 lo excluye expresamente de las asunciones |
| CV-14 | Peticiones de red originadas por los dos motores | **0** | `TC-06014` | **Bloqueante** |
| CV-15 | Aplicación de transformaciones sobre un almacén inexistente, sin paso manual | **1 de 1** | `TC-06032` | **Bloqueante.** Es criterio de aceptación de la etapa `c` |
| CV-16 | Provisorias iguales en dos producciones consecutivas, sobre la misma cuenta y entre cuentas | **0**, y **0** derivables del nombre, del correo o de la fecha | `TC-06027` | **Bloqueante** |
| CV-17 | Componentes de pieza y apariciones del texto original en una proyección de listado | **0** y **0** | `TC-06019` | **Bloqueante** |
| CV-18 | Escrituras aceptadas que reemplacen el texto original conservado | **0** | `TC-06016` | **Bloqueante** |
| CV-19 | Retiros parciales tras una baja interrumpida | **0** | `TC-06021`, con el almacén interrumpido a mitad de operación | **Bloqueante** |
| CV-20 | Emisiones de acceso sin clave de firma, y claves generadas al vuelo | **0** y **0** | `TC-06030` | **Bloqueante** |
| CV-21 | Mensajes y trazas con un secreto, la ruta del almacén o el texto del alumno, **en las dos direcciones** —mensaje y registro del servidor— | **0** y **0** | `TC-06035` | **Bloqueante** |
| CV-22 | Cobertura del catálogo de condiciones, en las dos direcciones | 17 de 17 y 0 fuera | `TC-06034` | **Bloqueante** |
| CV-23 | La construcción termina en 0 y **sin advertencias** | 0 advertencias | Etapa `build`; intake §17.1.P.8 · GeometriaFactory-Infrastructure | **Bloqueante** |

**No hay criterio de disponibilidad ni de caudal, y es correcto que no lo haya.** El intake §17.1.P.10 · GeometriaFactory-Infrastructure declara «sin SLO» para este proyecto de código, y quien tiene sujeto para el caudal es `GeometriaFactory-Api`, que es el que recibe peticiones.

**No se declara ningún tiempo de ejecución de la batería.** El único tiempo de este proyecto de código es el de `CV-06010`, que es de **interpretación** y viene del intake con su rótulo.

## 4. Criterios de regresión

### 4.1 `GeometriaFactory-Api`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-28 | La batería completa —unitaria y de integración— se ejecuta entera al cerrar cada etapa | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-29 | **Ningún caso de verificación que pasaba en la etapa anterior deja de pasar** sin justificación escrita | 0 regresiones sin justificar |
| CV-30 | **`TC-00007` se ejecuta en todas las etapas que agregan un punto de acceso**, y `TC-00025` y `TC-00026` en todas las que agregan una respuesta de fallo | Presentes en cada una. Son los tres cuyo resultado **cambia al crecer la superficie** |
| CV-31 | **La batería del validador que corre desde acá pasa entera** en toda etapa posterior a la `f` | **10 de 10** en cada ejecución. Ver §6 sobre el recuento |
| CV-32 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso por defecto cerrado, como mínimo |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, **sin correcciones**.

### 4.2 `GeometriaFactory-Domain`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-14 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos de prueba que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-15 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita en el informe de cierre de la etapa | 0 regresiones sin justificar |
| CV-16 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente, con su fila en la matriz | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-17 | `TC-02005` —las cinco operaciones rechazadas sobre la cuenta de administrador— se ejecuta en **todas** las etapas a partir de la `d` | Presente en cada ejecución. Es la prueba de regresión de la familia de defectos que en este producto **se abrió dos veces** |

**La regla de no regresión es acumulativa por diseño.** El intake declara que cada etapa reejecuta lo anterior, y eso es lo que hace caro que la batería crezca en tiempo: es el motivo por el que `CV-02008` existe.

### 4.3 `GeometriaFactory-Application`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-18 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos de prueba que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-19 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita en el informe de cierre de la etapa | 0 regresiones sin justificar |
| CV-20 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente, con su fila en la matriz | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-21 | `TC-04011` —la prueba de orden de la cuarta comprobación— se ejecuta en **todas** las etapas a partir de la `d` | Presente en cada ejecución. Es la prueba de regresión del riesgo de impacto **muy alto** de `05` §9 |
| CV-22 | `TC-04026` y `TC-04027` se ejecutan en **todas** las etapas, incluida la `a` | Presentes en cada ejecución. Una dependencia nueva o una prueba que abra el almacén se detectan en la etapa que las introduce y no al final |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, declara que al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, sin correcciones.

### 4.4 `GeometriaFactory-Infrastructure`

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-24 | La batería completa se ejecuta entera al cerrar cada etapa, y no sólo los casos que la etapa tocó | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-25 | **Ningún caso de prueba que estaba en verde en la etapa anterior pasa a rojo** sin justificación escrita | 0 regresiones sin justificar |
| CV-26 | **Los diez casos de la batería del validador se reejecutan en toda etapa posterior a la `f`**, y no sólo en ella | 10 de 10 en cada ejecución. Es el riesgo de negocio que la fuente pone primero |
| CV-27 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso de prueba por defecto cerrado, como mínimo |
| CV-28 | Los casos de los tres modos de falla que **no se notan** —`TC-06028`, `TC-06030`, `TC-06033`— se ejecutan en **todas** las etapas a partir de aquella en que su sujeto existe | Presentes en cada ejecución. Son los que `05` §9 declara de impacto muy alto |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, sin correcciones.

## 5. Criterios de calidad de código

### 5.1 `GeometriaFactory-Api`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-33 | Cobertura por componente cumplida, con los **ocho** reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-00014` |
| CV-34 | Mutation score | **60 %**, piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija **para el tipo `library`**; la fila `rest-api`, que es la de este proyecto de código, **no pide mutation score**. **Ninguna fuente del producto lo declara.** Se adopta igual, con más exigencia que la que la guía pide | **No exigible todavía**: la herramienta no está elegida ni corre. **La composición de raíz queda exenta** con su fundamento |
| CV-35 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-00026` |
| CV-36 | Ningún caso de verificación está deshabilitado sin motivo escrito en su fila | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-37 | Ninguna prueba de la batería de integración usa dobles: golpea **la superficie real contra el almacén real** | 0 dobles en integración | **Bloqueante**. Doblar algo ahí la convierte en otra cosa |
| CV-38 | Ninguna prueba usa el almacén de desarrollo ni el de producción: cada una **crea y descarta el suyo** | 0 usos del almacén compartido | **Bloqueante** |
| CV-39 | Ninguna prueba deja un secreto real en el repositorio: la clave de firma de prueba es **evidentemente ficticia** y llega por configuración | 0 secretos reales | **Bloqueante** |
| CV-40 | Los casos de verificación citan los puntos por su identificador `A-XX` **y no por su ruta**, mientras la forma de las rutas siga validándose en el punto de control de la etapa `a` | 0 citas por ruta | **Bloqueante hasta el cierre de la etapa `a`** |

### 5.2 `GeometriaFactory-Domain`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-18 | Cobertura por componente cumplida, con los cinco componentes reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-02009` |
| CV-19 | Mutation score en dominio | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline (hueco declarado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7). Hasta entonces se reporta «sin medir» |
| CV-20 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-02013` |
| CV-21 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-22 | Ningún caso de prueba depende del orden de ejecución ni de un reloj del entorno | 0 dependencias de orden; dos ejecuciones consecutivas con resultado idéntico (`TC-02025`) | **Bloqueante** |

### 5.3 `GeometriaFactory-Application`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-23 | Cobertura por componente cumplida, con los siete componentes con umbral reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-04010` |
| CV-24 | Mutation score | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline (hueco declarado en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8). Hasta entonces se reporta «sin medir» |
| CV-25 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-04017` |
| CV-26 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-27 | Ningún caso de prueba depende del orden de ejecución ni del reloj del entorno | 0 dependencias de orden; el momento entra siempre por el doble del puerto de reloj (`TC-04013`) | **Bloqueante** |
| CV-28 | Ninguna prueba sustituye un componente interno con un doble: los dobles son **sólo de puerto** | 0 dobles de componente interno | **Bloqueante**, por [`Estrategia-Testing.md`](Estrategia-Testing.md) §5 |

### 5.4 `GeometriaFactory-Infrastructure`

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-29 | Cobertura por componente cumplida, con los **ocho** componentes reportados por separado y **el informe de los dos motores reportado aparte** | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-06011` y `CV-06012` |
| CV-30 | Mutation score | **60 %**, piso de `Rules-Calidad-Y-Pruebas.md` §2.2 para el tipo `library`. **Ninguna fuente del producto lo declara** | **No exigible todavía**: la herramienta no está elegida ni corre en el pipeline. Hasta entonces se reporta «sin medir». **El adaptador de reloj queda exento con su fundamento** |
| CV-31 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-06023` |
| CV-32 | Ningún caso de prueba está deshabilitado sin motivo escrito en su fila del catálogo | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-33 | Ninguna prueba de integración interna usa el almacén de desarrollo ni el de producción: cada una **crea y descarta el suyo** | 0 usos del almacén compartido | **Bloqueante** |
| CV-34 | Ningún texto de figuras usado como dato de prueba está escrito a mano: **todos salen del intake §20** | 0 textos escritos a mano | **Bloqueante**. Es la mitigación del riesgo de negocio que la fuente pone primero |
| CV-35 | Ninguna prueba deja un secreto real en el repositorio: la clave de firma de prueba es **evidentemente ficticia** y llega por configuración | 0 secretos reales | **Bloqueante** |

## 6. Excepciones documentadas

### 6.1 `GeometriaFactory-Api`

**Un criterio no cumplido no se acepta en silencio.** Las cuatro únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-00011` a `CV-00015`, `CV-00033`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-00034`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado** | — |
| **Puerta técnica que no pasa** —`PT-04`— | **No hay excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella y **no se arrastra como deuda** | El Product Owner decide la salida, no la excepción |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita |

**Sobre `CV-00031` y el recuento de la batería del validador.** El intake **1.20** escribe «incluidas las **diez** pruebas del validador» en §17.1.P.8 · GeometriaFactory-Api —y «las **diez** pruebas del validador pasan» en §17.1.P.8 · GeometriaFactory-Infrastructure—, y su §21 tiene **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**. **Hasta 1.19 los dos gates escribían nueve**; la Fase C de `GeometriaFactory-Infrastructure` ya había resuelto la lectura en **diez**, este documento la heredó, y la fuente lo confirmó en 1.20. **Cerrar la etapa con nueve casos no es una excepción admitida.**

**Lo que tampoco es una excepción admitida:** agregar un punto de acceso sin declarar si queda dentro de la guardia; enriquecer una respuesta de una familia empobrecida «para que sea más útil»; truncar un cuerpo en lugar de rechazarlo; dar por verificada la eliminación fuera de alcance **sin forzar la petición**; declarar cumplido un NFR de umbral cero por no haber observado lo contrario; o dejar un secreto real en una prueba.

### 6.2 `GeometriaFactory-Domain`

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-02008`, `CV-02009`, `CV-02018`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre de la etapa, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó (`BT-02015`) | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-02019`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado ni se declara cumplido** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito en el punto de control | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** bajar un umbral para que cierre, deshabilitar un caso de prueba para que la batería pase, sustituir un escenario del intake por un dato que dé el resultado esperado, o declarar cumplido un criterio cuya medición no se hizo.

### 6.3 `GeometriaFactory-Application`

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-04009`, `CV-04010`, `CV-04023`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre de la etapa, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó (`BT-04018`) | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-04024`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado ni se declara cumplido** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito en el punto de control | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** bajar un umbral para que cierre, deshabilitar un caso de prueba para que la batería pase, mover una prueba a la batería de integración de `GeometriaFactory-Api` **para esquivar `CV-00011`** en lugar de porque ahí es donde pertenece, sustituir un escenario del intake por un resultado que dé el desenlace esperado, o declarar cumplido un criterio cuya medición no se hizo.

### 6.4 `GeometriaFactory-Infrastructure`

**Un criterio no cumplido no se acepta en silencio.** Las tres únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-06010`, `CV-06011`, `CV-06012`, `CV-06029`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] que el Product Owner todavía no confirmó | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-06030`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado** | — |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita |

**Sobre `CV-00002` y el recuento de la batería.** El intake **1.20** escribe «las **diez** pruebas del validador pasan» en §17.1.P.8 · GeometriaFactory-Infrastructure e «incluidas las **diez** pruebas del validador» en §17.1.P.8 · GeometriaFactory-Api, y su §21 tiene **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**. **Hasta 1.19 los dos gates escribían nueve**, y esta categoría aplicó diez igual, apoyada en `05` §8 y §10.5, que ya habían resuelto la lectura; la fuente lo corrigió en 1.20 y la divergencia está cerrada. **Cerrar la etapa con nueve casos y declarar cumplido `CV-00002` no es una excepción admitida**: dejaría sin cubrir el escenario que cerró la única condición del contrato de fachada que no tenía dato de prueba.

**Lo que tampoco es una excepción admitida:** bajar un umbral para que cierre; deshabilitar un caso de prueba para que la batería pase; **escribir a mano un texto de figuras porque el del intake es largo**; declarar cumplido un NFR de umbral cero por no haber observado lo contrario, sin haberlo medido en su condición; o dejar un secreto real en una prueba.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **24 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
