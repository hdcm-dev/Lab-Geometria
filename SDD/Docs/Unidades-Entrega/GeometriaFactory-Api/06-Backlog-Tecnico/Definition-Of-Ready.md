# Definition of Ready — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Definition-Of-Ready.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las cinco secciones son comunes, y los criterios se suman.** Una historia está lista cuando cumple la definición de la capa que la toca **y** las transversales de la unidad.

---

## 1. Criterios DoR para historias de usuario

### 1.1 `GeometriaFactory-Api`

Ocho criterios, todos respondibles con sí o no. Los cuatro últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto.** Con **dos excepciones declaradas**: las historias de `CU-00010` y de `CU-00012`, porque `02` §7.2 declara que esos dos casos de uso **no trazan a ninguna necesidad** y explica por qué.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el punto de acceso que la realiza**, de los **quince** de `Definicion-Superficie-HTTP.md` §3, o declara que no realiza ninguno; y el componente de `05` §3.1 que lo aloja.
5. **Declara si su punto está bajo la guardia.** Si no lo está, declara **cuál de las cuatro ausencias declaradas** es y por qué. Una historia que agregue un punto y no diga nada de la guardia **no está lista**: es el defecto de omisión que rompe `RN-00013` **sin que nada falle**.
6. **Toda condición que la historia transporta es uno de los diecisiete códigos vivos** del conjunto cerrado de `GeometriaFactory-Contracts`, con su destino declarado en la tabla de traducción. Una historia que necesite un código nuevo **no está lista**: los códigos **no se acuñan acá**, y donde el conjunto no tiene código **se usa el genérico y se declara el hueco**.
7. **Declara que no decide qué se dice.** Una historia que decida un estado, una admisibilidad, una pertenencia sobre el dato o qué campos cruzan la frontera **está mal ubicada**: `02` §4 lo enuncia en una línea.
8. **Si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas, lo declara.** Son tres —credenciales inválidas sin declarar qué campo falló, recurso que no se ve sin distinguir inexistente de ajeno de fuera de alcance, y correo ya registrado sin declarar situación ni papel—, y en las tres **es la decisión y no el defecto**.

### 1.2 `GeometriaFactory-Domain`

Seis criterios, todos respondibles con sí o no. Una historia que no los cumpla no entra a la etapa.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 en su tabla de trazabilidad.
2. **Declara su necesidad de negocio y su etapa.** La historia nombra la `NB-XX` que sostiene y la etapa del producto en la que se ejerce, de las **ocho** comprometidas.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Cita por identificador toda regla e invariante que ejerce**, sin volver a enunciarla. El enunciado vive en `Reglas-De-Negocio/` y en `Definicion-Modelo-De-Dominio.md`, y una historia que lo reescriba abre una segunda fuente de verdad.
5. **Toda condición de rechazo que produce existe en el catálogo** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Una historia que necesite una condición nueva no está lista: primero se da de alta en 03.
6. **Sus tareas técnicas están identificadas y ninguna está bloqueada.** Si una `BT-XX` de la que depende cierra un punto abierto que sigue abierto, la historia no entra.

### 1.3 `GeometriaFactory-Application`

Siete criterios, todos respondibles con sí o no. Los tres últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto**, de las que [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §2.1 enumera.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1 que la sostiene** y los **puertos** que consume, de los cuatro de `02` §3.
5. **Declara cuál de las cuatro comprobaciones de `02` §4 la alcanza, o declara que ninguna la alcanza y por qué.** Una historia que no diga nada de la cuarta comprobación **no está lista**: es el camino por el que `INV-09` se pierde, y `Domain ADR-04005` §6 ya declaró que el dominio no puede impedirlo.
6. **Toda condición de rechazo que la historia produce existe en el catálogo de las 36** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Una historia que necesite una condición nueva **no está lista**: el catálogo es cerrado y se compara en las dos direcciones.
7. **Se puede verificar con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso.** Si no se puede, o la historia está mal ubicada o algún componente está consultando por su cuenta, que es el primer riesgo de `05` §9.

### 1.4 `GeometriaFactory-Infrastructure`

Ocho criterios, todos respondibles con sí o no. Los cuatro últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto.** Con **una excepción declarada**: las dos historias de `CU-06009` pueden no declarar necesidad, porque `02` §7.2 declara que ese caso de uso **no traza a ninguna** y explica por qué.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de `05` §3.1 que la sostiene**, y si toca el almacén, **las reglas conceptuales de modelo que materializa**, de las siete.
5. **Declara que no toma ninguna decisión de negocio.** Una historia que decida un estado, una autorización, una admisibilidad o el resultado de una comparación de confirmación **está mal ubicada**: `02` §4 lo enuncia en una línea, y esta capa **provee el mecanismo**.
6. **Toda condición que la historia produce existe en el catálogo de las 17** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), y la historia declara si es un **resultado** o un **fallo**. Confundirlos es el segundo riesgo de `05` §9, con probabilidad **alta**.
7. **Si la historia tiene un camino en el que un mecanismo no puede cumplir su promesa, declara que se detiene y lo dice.** No la cumple a medias, no compone el valor por otro medio y no cae hacia un sustituto. Una historia sin ese camino declarado **no está lista** cuando el mecanismo puede fallar.
8. **Declara si toca el almacén.** Si no lo toca —los dos motores, el reloj y el mecanismo de credenciales—, su prueba es **unitaria y sin base**; si lo toca, la prueba de integración pertenece a `GeometriaFactory-Api` y la historia lo declara.

## 2. Criterios DoR para tareas técnicas

### 2.1 `GeometriaFactory-Api`

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, un punto de acceso de la superficie de 02 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero puntos fuera de la guardia de más, cero códigos inventados, cero respuestas que exponen, cero eliminaciones fuera de alcance aceptadas, cero truncamientos— el criterio se expresa **con umbral cero y con la condición en que se mide**.
4. **Si la tarea toca la superficie, declara que se compara contra una lista en las dos direcciones.** El defecto característico de esta capa es de **omisión**, y `05` §9 le asigna probabilidad **alta**: no se detecta leyendo el punto nuevo.
5. **Sus dependencias están declaradas y ninguna es circular**, y ninguna cruza la regla de `05` §3.2: **ninguna superficie depende de otra superficie**, **el traductor está después de las cinco** y **la composición de raíz no atiende peticiones**.
6. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas; y si la decisión **obliga a otro proyecto de código o pertenece a otro**, lo declara. BT-00008 obliga a `GeometriaFactory-Web`; BT-00026 la mide `09-Devops`; BT-00015 y BT-00021 elevan al Product Owner.

### 2.2 `GeometriaFactory-Domain`

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1, una ADR, un NFR de su §8, un punto abierto de su §11 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR o la puerta que la sostiene.
3. **Sus criterios de aceptación son verificables** por inspección, por prueba automatizada o por medición de una puerta declarada. «Queda bien hecho» no es un criterio.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular.**
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas**, no en horas: la unidad de planificación del producto es la etapa (`Roadmap-Producto.md` §1.2), y una caja temporal en días sería un plazo que ninguna fuente da.

### 2.3 `GeometriaFactory-Application`

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero dependencias salientes de más, cero pruebas que tocan la base, cero componentes cargados en el listado— el criterio se expresa con **umbral cero y con la condición en la que se mide**.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias de `05` §3.2: **ningún orquestador depende de otro orquestador**, y la guarda no lee conjuntos ni escribe.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas. Y si el punto abierto que cierra **es de otro proyecto de código**, lo declara: la tarea acompaña y no decide.

### 2.4 `GeometriaFactory-Infrastructure`

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, una regla conceptual de modelo o una puerta del intake §17.1.P.8 · GeometriaFactory-Infrastructure.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero peticiones de red, cero componentes cargados, cero retiros parciales, cero provisorias repetidas, cero mensajes con secreto— el criterio se expresa **con umbral cero y con la condición en la que se mide**.
4. **Si la tarea tiene un atajo destructivo conocido, lo escribe como prohibido.** `05` §9 identifica dos de impacto **muy alto**: **componer la provisoria por un contador, la fecha o el correo** cuando la fuente de material impredecible no responde, y **descartar el almacén y crearlo de nuevo** ante un esquema que no corresponde. Una tarea que los toque y no los declare **no está lista**.
5. **Sus dependencias están declaradas y ninguna es circular**, y ninguna cruza la regla de `05` §3.2: **ningún adaptador depende de otro adaptador** —el único par acoplado son los dos motores, y en una sola dirección—, **los dos motores, el reloj y el mecanismo de credenciales no dependen del contexto de persistencia**, y **la composición de raíz no es de acá**.
6. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas; y si el punto abierto que cierra **no es de este proyecto de código**, lo declara.

## 3. Excepciones admitidas

### 3.1 `GeometriaFactory-Api`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historias de `CU-00010` y de `CU-00012` | El criterio 2 de §1 se cumple **declarando que no trazan a ninguna necesidad y por qué**: conectar un puerto con su adaptador es construcción, y la colección de peticiones **no implementa nada, demuestra**. Inventarles una traza haría creer que hay una necesidad detrás de una decisión de estructura | El Product Owner |
| Historia de `CU-00012` | El criterio 4 de §1 se cumple **declarando que no realiza ningún punto de acceso**: `05` §3.3 declara que es el único de los doce casos de uso **sin componente**, porque es un artefacto del árbol de muestras y no código de producción | El Product Owner |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende de uno de los **cinco** valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-10` del backlog se cierre con BT-00025. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que agrega un punto de acceso | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un punto fuera de la guardia rompe `RN-00013` sin que nada falle, y un código inventado rompe el conjunto cerrado del ensamblado de contratos, que **dos extremos compilan juntos** | — |
| Historia que traduce la negativa de pertenencia | Ninguno: **no se admite excepción al criterio 8**. Responder «no autorizado» donde corresponde «no encontrado» **confirma la existencia de un recurso ajeno**, permite averiguar por tanteo qué identificadores existen y **ninguna capa de adentro puede repararlo** | — |

### 3.2 `GeometriaFactory-Domain`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el resultado esperado en lugar de con un criterio verificable de antemano: el objeto de la tarea es producir la decisión que hoy no existe | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya condición de rechazo todavía no está en el catálogo de 03 | El criterio 5 de §1 se difiere **una sola vez**, con el alta en 03 comprometida antes de cerrar la etapa | El Product Owner, con constancia escrita en el informe de cierre de la etapa |
| Historia que la etapa vigente sólo ejerce parcialmente | Ninguno: **no se admite**. Una historia que no cabe entera en su etapa está mal cortada y se redivide, por el mismo criterio con el que el intake §15 obliga a redividir una etapa mal cortada | — |

**Ninguna excepción alcanza al criterio 1 ni al criterio 4 de §1.** Una historia sin caso de uso o que reescriba una regla no entra bajo ninguna circunstancia: son los dos defectos que este corpus tiene documentados como los que más veces volvieron.

### 3.3 `GeometriaFactory-Application`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Tarea que **acompaña** un punto abierto cuya titularidad es de otro proyecto de código —BT-04020 y BT-04021— | El criterio 2 de §2 se cumple declarando de quién es la decisión y cuál es el plazo, en lugar de una historia consumidora | El Product Owner |
| Historia cuya verificación depende de uno de los dos valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-05` del backlog se cierre con BT-04018. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que agrega una operación que lee o escribe | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un camino que ejerza una capacidad sin resolver antes la marca es el riesgo de impacto **muy alto** de `05` §9, y una condición acuñada aguas abajo rompe la cobertura del catálogo en las dos direcciones | — |

### 3.4 `GeometriaFactory-Infrastructure`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historias de `CU-06009`, el sello del reloj | El criterio 2 de §1 se cumple **declarando que no traza a ninguna necesidad y por qué**, según `02` §7.2. Inventarle una traza sería peor que declarar la ausencia | El Product Owner |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende de uno de los valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-09` del backlog se cierre con BT-06023. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia del validador de figuras | Ninguno: **no se admite excepción al criterio 3 ni al 6**, y su material de prueba son **los ocho escenarios del intake §20 y ninguno inventado**. Es la mitigación del único riesgo de negocio del producto | — |
| Tarea que toca la producción de la provisoria o la preparación del almacén | Ninguno: **no se admite excepción al criterio 4 de §2**. Los dos atajos están escritos porque los dos **dejan el sistema aparentemente funcionando**: una provisoria adivinable no se nota hasta que alguien la usa, y un almacén recreado deja el servicio impecable y sin los trabajos de nadie | — |

## 4. Aprobador

### 4.1 `GeometriaFactory-Api`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15), que es además donde se validan las **rutas y los verbos** de los quince puntos.
2. La puerta de **imagen** del pipeline, que exige que se construya con el archivo multietapa, **arranque, aplique las transformaciones sobre un almacén vacío y responda salud**. Es `PT-04`.
3. **Las dos inspecciones en las dos direcciones**: los quince puntos contra la guardia, y los diecisiete códigos contra la tabla de traducción. No dependen de que alguien las revise: se corren, y son las únicas que detectan un defecto de **omisión**.

### 4.2 `GeometriaFactory-Domain`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar a la etapa, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona**, y eso hay que declararlo en lugar de simularlo: no hay una segunda persona que actúe de filtro. Lo que reemplaza a ese filtro es el **punto de control bloqueante** de cada etapa, que el intake §15 declara como regla de delivery: el orquestador se detiene, presenta el guion y espera OK explícito. La DoR dice cuándo se puede empezar; el punto de control es donde alguien distinto del código verifica que se empezó bien.

### 4.3 `GeometriaFactory-Application`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son dos cosas:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15), que es donde se cierran los nombres de la etapa `a`, incluido el del cuarto puerto.
2. Las **puertas medidas** del pipeline, que no dependen de que alguien las revise: cero dependencias salientes de más, cero advertencias de construcción y **cero pruebas de esta capa que toquen la base de datos real**. La última es la más dura y es propia de este proyecto de código (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Application).

### 4.4 `GeometriaFactory-Infrastructure`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **puertas propias del pipeline**: construcción sin advertencias y **transformaciones aplicadas solas sobre un almacén inexistente**, que es la cuarta etapa y es propia de acá.
3. **La batería obligatoria de diez casos del validador, con los ocho escenarios del intake como entrada.** No depende de que alguien la revise: se corre. Es la mitigación declarada del **único riesgo de negocio del producto**, y la fuente le asigna probabilidad **alta si no se controla**.

## 5. Qué no es esta DoR

### 5.1 `GeometriaFactory-Api`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona la batería de integración corriendo con su proporción, ni las tres comparaciones indistinguibles, ni la prueba de eliminación forzada, que son condiciones de cierre.

**No define la superficie.** Los **quince** puntos de acceso, sus verbos y sus códigos de respuesta están en `Definicion-Superficie-HTTP.md` y en [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md); esta DoR exige que la historia **declare** su punto, no que lo rediseñe.

**Y no redacta reglas de negocio ni códigos del contrato.** Las **dieciséis** reglas viven en `GeometriaFactory-Domain` y los **diecisiete** códigos vivos en `GeometriaFactory-Contracts`. Esta capa **no agrega, no renombra y no traduce a texto** ningún código.

### 5.2 `GeometriaFactory-Domain`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que hace las veces de criterio de cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5, que son de nivel producto y no de proyecto de código. Esta DoR habla de **cuándo empezar** y no toca ninguna condición de cierre: no menciona cobertura, ni pruebas que pasen, ni documentación al día, que son de la DoD.

### 5.3 `GeometriaFactory-Application`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona la cobertura alcanzada, ni los 500 ms medidos, ni la matriz de las cuatro comprobaciones completa, que son condiciones de cierre.

**Tampoco redacta reglas ni invariantes.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; lo que esta DoR exige es que la historia los **cite por identificador** y declare el tramo que esta capa ejerce, que es lo que `02` §6 y `05` §10.2 ya repartieron.

### 5.4 `GeometriaFactory-Infrastructure`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona los diez casos pasando, ni las tres coberturas alcanzadas, ni los 200 ms medidos.

**No redacta reglas de negocio ni invariantes.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; lo que esta DoR exige es que la historia los **cite por identificador**. Las **siete** reglas conceptuales de modelo sí son propias de la categoría 02 de este proyecto de código, y **no compiten con las reglas de negocio**: una regla conceptual de modelo declara cómo el dato sobrevive, no qué decidió el negocio.

**Y no decide el límite de tamaño del texto.** `ADR-06006` §2 decidió que el motor **no impone límite propio** y reasignó el valor y su forma de rechazo a la categoría 05 de `GeometriaFactory-Api`, que ya la tomó: **rechaza y nunca trunca**.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
