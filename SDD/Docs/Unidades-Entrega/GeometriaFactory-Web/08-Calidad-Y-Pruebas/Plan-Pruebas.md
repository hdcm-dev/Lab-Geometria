# Plan de pruebas — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Plan-Pruebas.md
**Versión:** 2.1
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Plan por momento del producto»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Alcance del plan

### 1.1 `GeometriaFactory-Web`

**Qué cubre.** Los **treinta y cinco** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) y las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), repartidos entre las **ocho** etapas comprometidas del producto —`a` a `h`—. **Este es el único de los siete proyectos de código del producto que produce épica en las ocho**, y así lo declara [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2.

**Qué no cubre, y dónde se cubre.** Las reglas de negocio y su cumplimiento, en `GeometriaFactory-Domain` y en las capas que las ejercen; la batería de integración contra el servicio de datos real, en `GeometriaFactory-Api`; el interior del bundle y sus siete condiciones, en `GeometriaFactory-Visor`; la interpretación del texto, en `GeometriaFactory-Infrastructure`.

**Y algo que no cubre y conviene decir aparte:** esta pieza **no verifica que una regla se cumpla**, porque no la hace cumplir (`02` §5). Lo que verifica de cada acotación es **que forzar la solicitud sin pasar por la pantalla la reciba rechazada del otro lado**. Seis casos de verificación existen sólo para eso.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

### 1.2 `GeometriaFactory-Visor`

**Qué cubre.** Los **veintiún** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre los **tres** momentos que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara como épicas: la etapa `a`, el **momento de medición de `PT-02` y `PT-03`** —antes de comprometer la etapa `g`— y la etapa `g`.

**Por qué §5 se titula «plan por momento del producto» y no «plan por etapa».** Porque el momento central de este proyecto de código **no es una etapa**: es el punto en que las dos puertas técnicas se miden, que el roadmap §2.2 ubica **antes de comprometer la fase `g`** y que su §5.2 incluye entre los criterios de la transición `f` → `g`. `06` §2.1 declara que EP-12002 no crea una etapa ni altera el orden de las ocho comprometidas, y este plan hereda esa forma sin inventar una etapa nueva.

**Qué no cubre, y dónde se cubre.** Toda decisión sobre el trabajo del alumno —si es válido, qué produce advertencia, quién ve qué— en el backend; la presentación del árbol, los controles de movimiento y la accesibilidad de la superficie visible, en `GeometriaFactory-Web`; el desenlace del envío, en `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure`.

**Las etapas `b` a `f` y la `h` no producen filas de trabajo en este plan**, y es declaración y no olvido: ninguna dibuja nada, y la fachada dibuja el mismo trabajo para el alumno y para el administrador sin saber cuál de los dos lo mira.

**Sin fechas y sin duraciones.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». El único umbral contado de este plan son los **diez recorridos** de ida y vuelta, que se cuentan en recorridos y no en segundos.

## 2. Criterios de entrada

### 2.1 `GeometriaFactory-Web`

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los **ocho** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluidos el 4 —superficie declarada—, el 6 —toda condición es uno de los **diecisiete** códigos vivos o el camino de ausencia de respuesta— y el 7 —ninguna afirmación depende de que esta pieza haga cumplir una regla—.
- [ ] **`PT-01` está medida en sus cuatro partes** y su resultado registrado. El intake §15 la ubica en la etapa `a`, **antes que cualquier otra cosa**: sin ella el modelo de front no está confirmado.
- [ ] **Antes de la etapa `g`: `PT-02` y `PT-03` están medidas.** Una puerta que no pasa **detiene la planificación de la etapa que depende de ella** y no se arrastra como deuda.
- [ ] El servicio de datos está levantado desde el contenedor de desarrollo, a partir de la etapa `c`. El guion no se ejecuta contra un doble.
- [ ] El bundle del visor **generado en el flujo y no tomado de un artefacto viejo**, a partir de la etapa `g`.
- [ ] Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están identificadas, con el método resuelto que declara [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.1.

### 2.2 `GeometriaFactory-Visor`

- [ ] `BT-12001` está cerrada: la cadena de construcción es reproducible y produce un archivo **vacío pero real**.
- [ ] `BT-12002` está cerrada: existe el guion propio del bundle, para no encadenar la construcción del resto del producto en cada iteración.
- [ ] `BT-12009` está cerrada antes del momento de medición: la versión del motor de dibujo está anclada y registrada, y si es posterior a la del visualizador previo, el cambio de interfaz que exija está documentado.
- [ ] Las historias del momento cumplen los **siete** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluidos el quinto —qué garantías ejerce— y el sexto —todo código usado es uno de los siete—.
- [ ] Existe un navegador con **capacidad gráfica tridimensional** en el entorno donde se ejecutan las pruebas de extremo a extremo, y un conductor capaz de contar peticiones de red y de leer el almacenamiento del navegador.
- [ ] El conductor puede **prender los dos movimientos** aunque el entorno declare preferencia de movimiento reducido. Sin esto, las mediciones de ausencia no se pueden hacer en su peor caso.

## 3. Criterios de salida

### 3.1 `GeometriaFactory-Web`

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están ejecutados y pasan.
- [ ] **El guion de la etapa y los de todas las anteriores pasan al 100 %** (`TC-10035`). Es la regla de no-regresión acumulativa del intake §15, que **no es asunción**.
- [ ] Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, **con estado y fecha actualizados**.
- [ ] **Ninguna deriva mayor queda sin resolver.** Se corrige lo construido, o se actualiza la línea de base con aprobación humana explícita. **Nunca por omisión.**
- [ ] Las cinco inspecciones estructurales —`TC-10029` a `TC-10033`— dan **0** en cada uno de sus recuentos, en la condición declarada.
- [ ] Los seis casos que verifican **forzando la solicitud** —`TC-10001`, `TC-10005`, `TC-10007`, `TC-10015`, `TC-10025`, `TC-10026`— se ejecutaron para las acotaciones que la etapa introdujo.
- [ ] Los gates `QG-10001`, `QG-10002`, `QG-10003`, `QG-10005`, `QG-10006`, `QG-10007`, `QG-10008`, `QG-10009`, `QG-10010` y `QG-10011` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] `QG-10004` **se cumple**: el guion de la etapa y los de todas las anteriores pasan al 100 %. Es **bloqueante**, no condicionado (ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1).
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

### 3.2 `GeometriaFactory-Visor`

- [ ] Todos los `TC-XX` en alcance del momento están escritos, ejecutados y en verde.
- [ ] Cada `TC-XX` de una propiedad de **ausencia** se ejecutó **con su condición de medición declarada**, y la condición quedó registrada junto al resultado. Un umbral cero medido sin su condición **no cuenta**.
- [ ] **Ningún `TC-XX` que estaba en verde pasó a rojo** sin justificación escrita.
- [ ] Los gates `QG-12001`, `QG-12004` a `QG-12009` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] En el momento de medición: **`PT-02` y `PT-03` pasan enteras**. Si alguna no pasa, **la etapa `g` no se compromete**; no se arrastra como deuda.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, con sus cinco tablas.
- [ ] [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) tiene el estado de cada fila actualizado con su fecha de verificación.
- [ ] Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] Si el momento propuso una función nueva en la fachada, los seis pasos de [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 se recorrieron enteros, incluida la consolidación en el intake.
- [ ] El punto de control tiene el OK explícito del Product Owner.

## 4. Riesgos de calidad

### 4.1 `GeometriaFactory-Web`

Alineados con los **siete** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §9, más tres propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que aparezca un guion del navegador que llame al servicio de datos | **Muy alto** | Media | `TC-10029` y `TC-10030` en **cada** etapa, con el conteo hecho **con los dos movimientos prendidos**; `QG-10005` y `QG-10006` bloquean la fusión |
| RQ-02 | Que el proceso del hosting recicle y la persona pierda la sesión en mitad de un acto | Alto | Media, y **medida**: es `PT-01.c` | No hay mitigación técnica que inventar. Lo que hay es tratamiento verificado: `TC-10027` ejerce el estado «sesión no restablecible», y el envío como **única** acción de guardado hace que un corte no deje un trabajo a medias |
| RQ-03 | Que un mensaje mostrado lleve una dirección de servicio, una ruta de datos o una traza | Alto | Media, porque entra por el camino de excepción | `TC-10031` recorre los **diecisiete** códigos **y** el camino de ausencia de respuesta, sobre el traductor, que es el único punto por el que un mensaje llega a la persona |
| RQ-04 | Que un componente termine tocando el interior del bundle porque la fachada no expone algo | Alto | Media | `TC-10032` en cada etapa a partir de la `g`; y el procedimiento del `Visor` para cuando falta algo en la fachada, que **no es tocar el interior** |
| RQ-05 | Que la liberación de la instancia no se invoque y recorrer trabajos acumule contextos gráficos | Alto | Media, porque es la clase de omisión que no falla la primera vez | `TC-10021` como puerta `PT-02`, medida **antes de comprometer la etapa `g`** y reejecutada al cerrarla |
| RQ-06 | Que una subida deje la aplicación caída y se reporte como exitosa | Alto | Media, porque la subida **no es transaccional** | `QG-10003`: el flujo **no termina en la subida**, termina comprobando que la dirección pública responde |
| RQ-07 | Que un listado incorpore un campo del detalle y arrastre el texto completo de cada trabajo | Medio | Alta | `TC-10015` y `TC-10024` verifican la forma del listado; la proyección separada es decisión de `GeometriaFactory-Contracts` y esta pieza la consume sin invertirla |
| RQ-08 | **Que una acotación se dé por verificada mirando que el control no se dibuja**, sin forzar la solicitud | **Muy alto**: es la forma exacta en que una regla se cree cumplida y no lo está | Alta, porque es lo cómodo | Criterio de salida de §3: los **seis** casos que fuerzan la solicitud se ejecutan para toda acotación que la etapa introduce. La Definition of Ready §1 criterio 7 lo exige desde la entrada |
| RQ-09 | **Que una deriva mayor se resuelva por omisión**, dejando la fila en `Sin verificar` y siguiendo | Alto: la línea de base deja de ser línea de base | Media | Criterio de salida de §3 y `QG-10011`: ninguna deriva mayor queda sin resolver, y la decisión —corregir o actualizar la línea de base— es del Product Owner con constancia escrita |
| RQ-10 | **Que el guion se ejecute sólo para la etapa en curso** y la regla acumulativa se erosione | Alto: es la única red de seguridad de regresión que este proyecto de código tiene | Alta, porque el guion crece en cada etapa y ejecutarlo entero se vuelve caro | `TC-10035` es acumulativo por definición y su criterio de salida lo exige. **La regla acumulativa no es la parte rotulada [ASUNCIÓN]**: lo rotulado es expresarla como puerta con umbral del 100 % |

### 4.2 `GeometriaFactory-Visor`

Alineados con los **seis** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que aparezca una petición de red en el bundle, **por comodidad o por una dependencia que la haga por dentro** | **Muy alto**: rompe `RA-01` a través de `RA-02` | Baja para la primera causa, **media para la segunda** | `TC-12018` inspecciona **el bundle generado y no sólo la fuente**; `TC-12016` cuenta peticiones con los movimientos prendidos |
| RQ-02 | Que el anfitrión termine dependiendo de nombres internos del motor y el motor deje de ser reemplazable | Alto: se pierde el punto de extensión declarado del producto | Media | `TC-12018` verifica que la superficie son **6** funciones y nada más; [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) declara los ocho compromisos de un reemplazo |
| RQ-03 | Que un bucle de dibujo sobreviva a la destrucción y se acumule al recorrer trabajos | Alto: degradación progresiva, que es lo que `PT-02` mide | Media | `TC-12004`, con los diez recorridos medidos **con los movimientos prendidos**, que es su peor caso |
| RQ-04 | Que la versión del motor exija una interfaz distinta de la del visualizador previo | Medio: retrabajo acotado a la capa 3 | **Alta**: el intake ya lo anticipa | `BT-12009` como criterio de entrada de §2, cerrada **antes** del momento de medición |
| RQ-05 | Que una pieza deje de dibujarse sin quedar enumerada | Alto: es el defecto original que `NB-00006` viene a cerrar | Baja | `TC-12007`, con los escenarios `E-5`, `E-8` y `E-6`, incluida la comprobación negativa de que la pieza de dimensión cero **sí** se dibuja |
| RQ-06 | Que se acuñe un código de condición fuera de la categoría 02 | Medio: el conjunto deja de ser cerrado y 03 y 08 se desincronizan | Media, y el catálogo de 03 ya creció de doce a **trece** entradas **sin** que creciera el conjunto de códigos | `TC-12021`, que compara en las dos direcciones y verifica la distinción entre código y curso |
| RQ-07 | **Que una medición de ausencia se haga sin su condición** y quede en verde midiendo el caso fácil | **Muy alto**: el gate más importante del proyecto de código pasaría sin haber ejercitado nunca el bucle | **Alta**, porque los entornos de prueba automatizados suelen declarar preferencia de movimiento reducido | Criterio de entrada de §2 —el conductor puede prender los movimientos— y criterio de salida de §3 —la condición queda registrada junto al resultado— |
| RQ-08 | **Que se invente un umbral numérico de fluidez** para poder cerrar un criterio | Medio: un número inventado acá se propagaría como si fuera del producto | Media | `05` §8 se niega a inventarlo y esta categoría también; `BT-12018` deja las dos salidas admitidas, y ninguna es inventar un número |

## 5. Plan por etapa

### 5.1 `GeometriaFactory-Web`

Sin fechas y sin duraciones, por lo declarado en §1. `TC-10035` aparece en **todas** las etapas porque es acumulativo.

| Etapa | Épica | Alcance de testing | Casos de verificación en alcance | Filas de la matriz de sensado | Entregable de esta categoría |
| --- | --- | --- | --- | --- | --- |
| `a` | EP-10001 Esqueleto ambulante y verificación de viabilidad | Las **cuatro** mediciones de `PT-01` y la inspección de la única salida | `TC-10034`, `TC-10030`, `TC-10035` | Ninguna: todavía no hay superficie construida | `PT-01` medida y registrada, con la salida declarada si alguna parte no pasa |
| `b` | EP-10002 Navegación y sistema visual | Los **dos** shells, el mapa de rutas y las **once** superficies con marcador de posición | `TC-10005`, `TC-10029`, `TC-10035` | `SD-10001` a `SD-10011`, `SD-10023` a `SD-10027`, `SD-10054` a `SD-10056`, `SD-10059` a `SD-10061` | Las once superficies sensadas contra la maqueta; el primer conteo de peticiones del navegador |
| `c` | EP-10003 Identidad del administrador y sesión | Aprovisionamiento, ingreso con la credencial custodiada, cambio de contraseña y estado degradado | `TC-10001`, `TC-10003`, `TC-10004`, `TC-10006`, `TC-10027`, `TC-10028`, `TC-10031`, `TC-10035` | `SD-10014`, `SD-10015`, `SD-10016`, `SD-10022`, `SD-10057`, `SD-10058` | **0 apariciones de la credencial en el navegador**, criterio de aceptación de esta etapa; los diecisiete códigos traducidos |
| `d` | EP-10004 Ciclo de vida de la cuenta de alumno | Registro, panel de cuentas con sus cinco operaciones, provisoria comunicada y confinamiento | `TC-10002`, `TC-10007`, `TC-10008`, `TC-10009`, `TC-10010`, `TC-10035` | `SD-10009`, `SD-10013`, `SD-10019`, `SD-10028`, `SD-10035` | El cuarto guardián verificado **forzando la solicitud**; las operaciones de `F-26` verificadas contra `CU-10003` y `CU-10004`, **sin sonda propia** |
| `e` | EP-10005 Gestión del trabajo | Carga con el texto intacto, listado propio y listado de la comisión | `TC-10011`, `TC-10015`, `TC-10024`, `TC-10026`, `TC-10035` | `SD-10005`, `SD-10010`, `SD-10021`, `SD-10029`, `SD-10034`, `SD-10036` | Comparación carácter por carácter del texto de `E-2`; indistinguibilidad verificada forzando |
| `f` | EP-10006 Interpretación y verificación del dato del alumno | Previsualización que dibuja y no verifica, y presentación de advertencias y errores | `TC-10012`, `TC-10013`, `TC-10014`, `TC-10035` | `SD-10006`, `SD-10017`, `SD-10030`, `SD-10033`, `SD-10037`, `SD-10038` | Los escenarios `E-1`, `E-3`, `E-5` y `E-8` ejercitados; **exactamente dos** advertencias en `E-1` |
| `g` | EP-10007 Visualización del trabajo | La vista de trabajo con sus cuatro elementos, el árbol, la sincronización por índice y el gobierno del movimiento | `TC-10017`, `TC-10018`, `TC-10019`, `TC-10020`, `TC-10021`, `TC-10022`, `TC-10023`, `TC-10032`, `TC-10033`, `TC-10035` | `SD-10007`, `SD-10018`, `SD-10031`, `SD-10039` a `SD-10053` | `PT-02` y `PT-03` pasadas **antes** de comprometer la etapa; los escenarios `E-6` y `E-7` ejercitados |
| `h` | EP-10008 Desenlace de la entrega | El desenlace en el listado propio, la resolución con comentario opcional y el retiro | `TC-10016`, `TC-10025`, `TC-10035`, y reejecución de `TC-10024` y `TC-10026` | `SD-10008`, `SD-10020`, `SD-10026`, `SD-10027` | Matriz completa: 10 de 10 casos de uso, 13 de 13 restricciones, 61 de 61 sondas verificadas |

**La suma cubre los treinta y cinco casos de verificación y las sesenta y una filas.** `TC-10024` y `TC-10026` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido; `TC-10035` aparece en las ocho porque es acumulativo por definición.

## 6. Recursos

### 6.1 `GeometriaFactory-Web`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la verificación observada y la aprobación |
| Ambientes | **Dos, y no son intercambiables**: el contenedor de desarrollo con el navegador del equipo anfitrión para el guion, y el **hosting público** para `PT-01`, porque lo que esa puerta mide son las capacidades de ese hosting |
| Servicio de datos | Levantado y **real** a partir de la etapa `c`. Su indisponibilidad se provoca deteniéndolo, no simulándola |
| Datos | Los **ocho** escenarios del intake §20 **en su forma original y completa**; y los datos de maqueta de [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md), con la salvedad de los **valores compuestos para la maqueta que no viajan al producto** |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función: panel de herramientas de desarrollo, lector de pantalla, medición de contraste y un cliente de peticiones para forzar la solicitud |
| Línea de base | [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md), [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md) y [`../03-UX-UI-DX/Bitacora-Validacion-Maqueta.md`](../03-UX-UI-DX/Bitacora-Validacion-Maqueta.md), aprobados por el Product Owner |

### 6.2 `GeometriaFactory-Visor`

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` |
| Ambiente de construcción | El contenedor de desarrollo, con el entorno de ejecución de la cadena de herramientas |
| Ambiente de ejecución | Un navegador con **capacidad gráfica tridimensional**, más un conductor capaz de contar peticiones y de leer el almacenamiento. **No hay backend**, y su ausencia es una propiedad exigida y no una carencia |
| Datos | Los textos de los **ocho** escenarios del intake §20, transcriptos sin modificación, y el elemento de dibujo de tamaño cero de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función |
| Página de prueba | El sample **S-1**, que es a la vez ejemplo y material de prueba, y cuyo desarrollo pertenece a `10-Examples` |

## 7. Plan por momento del producto

### 7.1 `GeometriaFactory-Visor`

Sin fechas y sin duraciones, por lo declarado en §1.

| Momento | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| Etapa `a` | EP-12001 Esqueleto ambulante | La cadena de construcción y el artefacto vacío pero real. Ninguna capacidad funcional | Ninguno de los veintiuno: no hay fachada todavía. Se pone en pie el ejecutor y se mide `QG-12001` | Batería que corre; `BT-12001`, `BT-12002` y `BT-12003` cerradas |
| **Antes de comprometer la etapa `g`** | EP-12002 Medición de las puertas técnicas | Todo lo que `PT-02` y `PT-03` exigen que ya funcione: crear instancia, dibujar `E-1` con el ortoedro, sincronizar por índice y liberar recursos | `TC-12001`, `TC-12002`, `TC-12004`, `TC-12005`, `TC-12006`, `TC-12007`, `TC-12008`, `TC-12009`, `TC-12010`, `TC-12011`, `TC-12012`, `TC-12016`, `TC-12017`, `TC-12018`, `TC-12019`, `TC-12020`, `TC-12021` | **Las dos puertas medidas.** Si alguna no pasa, la etapa `g` no se compromete |
| Etapa `g` | EP-12003 Visualización del trabajo | Lo que la etapa integra: el movimiento automático de `F-25`, el árbol y la página integradora sin backend | `TC-12003`, `TC-12013`, `TC-12014`, `TC-12015`, y reejecución de `TC-12016`, `TC-12017` y `TC-12009` con los movimientos gobernados en vivo | Sample **S-1** en pie; las **seis** propiedades transversales verificadas juntas; `BT-12018` cerrada o elevada |

**La suma cubre los veintiún casos de prueba.** `TC-12016`, `TC-12017` y `TC-12009` aparecen dos veces porque la etapa `g` incorpora el gobierno en vivo de los movimientos, y las tres propiedades tienen que seguir sosteniéndose con esa capacidad presente.

**El grueso del trabajo de este proyecto de código cae antes de que la etapa `g` se abra**, y este plan lo refleja en lugar de esconderlo: diecisiete de los veintiún casos de prueba se ejecutan en el momento de medición, porque una puerta que no pasa detiene la planificación de la etapa que depende de ella.

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **7 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
