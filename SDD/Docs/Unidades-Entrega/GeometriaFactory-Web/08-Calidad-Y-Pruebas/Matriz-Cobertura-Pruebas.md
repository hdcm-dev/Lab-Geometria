# Matriz de cobertura de pruebas — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Matriz-Cobertura-Pruebas.md
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

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **2 secciones existen sólo en `GeometriaFactory-Visor`** —«Trazabilidad garantía ↔ tests», «Trazabilidad código de condición ↔ tests»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Propósito y alcance

### 1.1 `GeometriaFactory-Web`

Es el documento bisagra de la categoría: relaciona los **diez** casos de uso, los **catorce** NFR, las **dieciséis** reglas de negocio y las **trece** restricciones transversales con los **treinta y cinco** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** Todas dicen `Pendiente`, porque el sistema no está construido. La maqueta sí está aprobada y validada, y eso es una cosa distinta: lo aprobado es la **línea de base**, no el sistema.

Esta matriz **agrega una cuarta tabla** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de restricción transversal contra prueba. El motivo es que las **trece** restricciones de `02` §6 no son reglas de negocio —esta pieza no hace cumplir ninguna— y sin embargo son lo que este proyecto de código sí tiene que sostener, con un componente y una ADR asignados a cada una en `05` §10.2.

**Y declara en §7 su relación con [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md)**, que ya existía antes de esta fase y que **no se duplica acá**.

### 1.2 `GeometriaFactory-Visor`

Relaciona los **siete** casos de uso, los **ocho** NFR, las **dieciséis** reglas de negocio del producto, las **siete** garantías del contrato de fachada y los **siete** códigos de condición con los **veintiún** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), y declara la cobertura por componente.

**Ninguna columna de estado afirma que algo esté verde.** El bundle no está construido.

Esta matriz **agrega dos tablas** a las tres que `Rules-Calidad-Y-Pruebas.md` §4.5 exige: la de garantías y la de códigos de condición. Las dos tienen fundamento en la arquitectura: `05` §10.2 declara que **las siete garantías son parte del contrato y no detalles de implementación**, de modo que perder una es cambio mayor aunque las seis firmas no se toquen; y `05` §9 declara como riesgo que un código se acuñe aguas abajo y que 03 y 08 se desincronicen.

## 2. Trazabilidad CU ↔ tests

### 2.1 `GeometriaFactory-Web`

Diez filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Tests | Historias cubiertas | Estado |
| --- | --- | --- | --- | --- |
| CU-10001 Registrar la cuenta de alumno | Given la superficie de registro **sin ningún campo de contraseña**, When se registra un correo libre, Then aparece el bloque de éxito; con un correo ya usado, Then error de operación **sin revelar de quién es la cuenta** | `TC-10002` | US-10001, US-10002 | `Pendiente` |
| CU-10002 Iniciar y cerrar sesión sin exponer la credencial | Given una sesión iniciada, When se inspecciona el navegador, Then la credencial **no aparece**; el ingreso lleva a la ruta inicial del papel, el cierre vuelve al ingreso con su banda, y una cuenta que no admite ingreso recibe **su motivo** | `TC-10003`, `TC-10004`, `TC-10005`, `TC-10007` | US-10003, US-10004, US-10005, US-10029 | `Pendiente` |
| CU-10003 Establecer y cambiar la contraseña propia | Given los **tres** cursos —primer ingreso con la provisoria, cambio con la vigente y cambio forzado—, When se recorren, Then son **el mismo formulario y el mismo contrato**, y sólo el tercero no tiene salida | `TC-10006`, `TC-10007` | US-10006, US-10007, US-10028 | `Pendiente` |
| CU-10004 Administrar las cuentas de la comisión | Given el panel de cuentas, When se ejercen las **cinco** operaciones, Then la acción de situación ofrece **sólo la transición admitida**, la baja exige el correo escrito con su aviso de arrastre, y habilitar y resetear **comunican la provisoria** | `TC-10001`, `TC-10008`, `TC-10009`, `TC-10010` | US-10008, US-10009, US-10010, US-10030 | `Pendiente` |
| CU-10005 Enviar un trabajo y ver el resultado de la interpretación | Given el texto pegado, When se previsualiza, Then **se dibuja sin verificar y sin ninguna petición**; When se envía, Then el texto viaja **carácter por carácter** y el resultado muestra advertencias con su par de valores o errores con índice y campo | `TC-10011`, `TC-10012`, `TC-10013`, `TC-10014` | US-10011, US-10012, US-10013, US-10014 | `Pendiente` |
| CU-10006 Consultar el listado propio y operar sobre el borrador | Given trabajos en los cuatro estados, When se abre el panel propio, Then los controles **no se dibujan** fuera de `Borrador` y el desenlace se ve en la fila; el vacío se distingue del fallo **por el tipo recibido** | `TC-10015`, `TC-10016`, `TC-10026`, `TC-10028` | US-10015, US-10016, US-10017 | `Pendiente` |
| CU-10007 Abrir un trabajo y explorarlo en escena y árbol | Given un trabajo abierto, When se lo explora, Then la vista tiene **sus cuatro partes**, el árbol y la escena se sincronizan **por índice**, los dos movimientos se gobiernan desde el anfitrión y diez recorridos **no degradan** | `TC-10016`, `TC-10017`, `TC-10018`, `TC-10019`, `TC-10020`, `TC-10021`, `TC-10022`, `TC-10023` | US-10018, US-10019, US-10020, US-10021 | `Pendiente` |
| CU-10008 Recorrer la entrega de la comisión | Given trabajos de dos alumnos en los cuatro estados, When el administrador abre el listado, Then está agrupado y filtrable, **ningún `Borrador` aparece**, y el filtrado sin resultados se distingue del vacío | `TC-10024`, `TC-10028` | US-10022, US-10023 | `Pendiente` |
| CU-10009 Resolver un trabajo con comentario opcional | Given un trabajo en estado `Pendiente` abierto como administrador, When se lo aprueba o rechaza, Then procede con comentario opcional y se vuelve al listado actualizado; el bloque de decisión **no tiene ruta propia** y no aparece para el alumno | `TC-10025`, `TC-10026` | US-10024, US-10025 | `Pendiente` |
| CU-10010 Sostener la aplicación en estado degradado y reconexión | Given el servicio detenido y el circuito cortado, When se opera, Then los **dos tramos** se distinguen, el aviso reemplaza el contenido **y no el armazón**, y lo escrito se conserva | `TC-10027`, `TC-10028` | US-10026, US-10027 | `Pendiente` |

**Diez de diez casos de uso con al menos un caso de verificación, y treinta de treinta historias cubiertas.** Ninguno queda huérfano.

### 2.2 `GeometriaFactory-Visor`

Siete filas, una por caso de uso de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3. Ninguna se agrupa.

| CU | Criterio Given-When-Then principal | Test | Tipo | Estado |
| --- | --- | --- | --- | --- |
| CU-12001 Inicializar una instancia del visor | Given un elemento de dibujo con tamaño, When se crea la instancia, Then se devuelve un identificador y la escena queda viva; sin capacidad gráfica o con el elemento sin tamaño, Then se informa la condición y **no se crea instancia** | `TC-12001`, `TC-12002`, `TC-12003` | Integración y extremo a extremo | `Pendiente` |
| CU-12002 Cargar el texto del trabajo y dibujar | Given el texto de un trabajo, When se lo carga, Then se dibujan sus piezas y **toda pieza no dibujada queda enumerada** con su índice y su código; el resultado trae además la estructura del texto | `TC-12005`, `TC-12006`, `TC-12007`, `TC-12008`, `TC-12009`, `TC-12010` | Unit, integración y extremo a extremo | `Pendiente` |
| CU-12003 Seleccionar una pieza por su índice | Given una escena dibujada, When se selecciona un índice, Then esa pieza queda resaltada **en exclusiva**; con un índice que no corresponde a ninguna pieza dibujada, Then se informa la condición y la selección vigente se conserva | `TC-12011` | Extremo a extremo | `Pendiente` |
| CU-12004 Redimensionar la escena | Given un cambio de tamaño del elemento, When se invoca el ajuste, Then se recalcula la relación de aspecto; con el elemento sin tamaño, Then se informa la condición y **la instancia sigue viva** | `TC-12012` | Extremo a extremo | `Pendiente` |
| CU-12005 Destruir la instancia y liberar recursos | Given una instancia viva, When se la destruye, Then libera sus recursos y **corta su bucle**; el identificador queda invalidado | `TC-12004` | Extremo a extremo | `Pendiente` |
| CU-12006 Ejercitar la fachada sin backend | Given la página integradora y un texto pegado a mano, When se recorren las **seis** funciones con **0 servicios del backend disponibles**, Then el recorrido cierra entero y las **seis** propiedades transversales se verifican juntas | `TC-12015`, `TC-12016`, `TC-12017` | Extremo a extremo | `Pendiente` |
| CU-12007 Gobernar el movimiento automático | Given una instancia viva, When se prende o se apaga un movimiento, Then el cambio surte efecto **sin reconstruir la instancia** y sin perder la selección; el no nombrado conserva su estado | `TC-12013`, `TC-12014`, `TC-12003` | Extremo a extremo | `Pendiente` |

**Siete de siete casos de uso con al menos un caso de prueba.** Ninguno queda huérfano.

**Veinte de los veintiún `TC-XX` tienen fila en alguna de las cinco tablas de trazabilidad de esta matriz.** El restante es `TC-12020`, cuya trazabilidad primaria es una **puerta técnica** —y no un `CU-XX`, un NFR, una `RN-XX`, una garantía ni un código de condición—, y por eso no aparece en ninguna de ellas. Está en §2.1, y **no queda sin instrumento de trazabilidad**. Es el caso más sensible de la matriz, porque **es la prueba de `PT-02`** y una puerta que no pasa detiene la planificación de la etapa `g`.

### 2.1 El caso de prueba de la puerta técnica `PT-02`

| Caso de prueba | Qué verifica | A qué traza, según su campo «Cubre» | Estado |
| --- | --- | --- | --- |
| `TC-12020` La puerta `PT-02`: el bundle en una página del anfitrión | Los **cinco** tramos que la puerta exige, medidos juntos: el bundle carga, la creación de instancia arma la escena, el texto de `E-1` dibuja las **tres** figuras con el ortoedro, **diez** recorridos de ida y vuelta con los dos movimientos prendidos **no degradan**, y el árbol y la escena **se sincronizan por índice** | Puerta técnica **`PT-02`** del intake §15 y §17.2.P.8 · GeometriaFactory-Visor; `US-12001`, `US-12004`, `US-12009`, `US-12011`; `QG-12003`; `BT-12014` | `Pendiente` |

**Dónde están sus criterios de validación.** Los tramos se cuentan en [`Criterios-Validacion.md`](Criterios-Validacion.md) §4, que es la sección de puertas técnicas que este proyecto de código tiene y los otros dos de nivel topológico 0 no: **cuatro** criterios, `CV-20` a `CV-23`, que reparten los cinco tramos —`CV-20` toma juntos la carga del bundle y la creación de la escena—. **`TC-12020` no está fuera de la trazabilidad; está en el instrumento que le corresponde**, y esta subsección lo enlaza desde la matriz para que el recorrido inverso `TC → matriz` cierre.

## 3. Trazabilidad NFR ↔ tests

### 3.1 `GeometriaFactory-Web`

Catorce filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8.

| NFR | Objetivo numérico | Cómo se verifica | Herramienta de medición | Estado |
| --- | --- | --- | --- | --- |
| `PT-01.a` arranque en la dirección pública | Respuesta **200** | `TC-10034` | Comprobación al final del flujo de publicación | `Pendiente` |
| `PT-01.b` transporte del circuito | Semáforo; **amarillo aceptable** documentando la latencia percibida; sólo el rojo obliga a cambiar el modelo de front | `TC-10034` | Inspección del transporte negociado | `Pendiente` |
| `PT-01.c` estabilidad del proceso | **20 minutos** de navegación continua sin reciclado, y reconexión funcional al cortar y restablecer | `TC-10034` | Recorrido cronometrado. **No tiene mitigación en el código** (`R-06`) | `Pendiente` |
| `PT-01.d` salida hacia el backend | Una llamada de salud devuelve **datos reales** del servidor propio | `TC-10034` | Recorrido en la etapa `a` | `Pendiente` |
| Pasos del guion de demostración | **100 %** de la etapa **y de todas las anteriores** **[ASUNCIÓN del intake §17.2.P.6 · GeometriaFactory-Web en cuanto a expresarlo como puerta]** | `TC-10035`. Gate `QG-10004`, bloqueante | Ejecución en el navegador del equipo anfitrión | `Pendiente` |
| Peticiones del navegador hacia el servicio de datos | Exactamente **0**, con los dos movimientos prendidos | `TC-10029` | Conteo en la pestaña de red | `Pendiente` |
| Salidas del proyecto de código hacia el servicio de datos | Exactamente **1**, y **0** bibliotecas de guion que consulten | `TC-10030` | Inspección del árbol de fuentes y de las dependencias de guion | `Pendiente` |
| Apariciones de la credencial de sesión en el navegador | Exactamente **0** | `TC-10003` | Inspección del almacenamiento, de las marcas de sesión y del contenido servido | `Pendiente` |
| Mensajes que exponen dirección, ruta o traza | Exactamente **0** sobre los **diecisiete** códigos vivos **y** sobre el camino de ausencia de respuesta | `TC-10031` | Inspección del traductor de condiciones y barrido de la microcopy | `Pendiente` |
| Tráfico de circuito durante la interacción con la escena | Exactamente **0**, y el texto viaja **una sola vez por trabajo** | `TC-10033` | Conteo en la pestaña de red mientras se rota y se acerca | `Pendiente` |
| Instancias del visor no liberadas | Exactamente **0** tras **10** recorridos, con los dos movimientos prendidos | `TC-10021`, puerta `PT-02` | Recuento de recursos vivos al final de los diez | `Pendiente` |
| Invocaciones al interior del bundle | Exactamente **0**: **6 de 6** funciones como única vía | `TC-10032` | Inspección del árbol de fuentes | `Pendiente` |
| Estados de la línea de base demostrados | **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas | **Las 61 filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md)**, no un caso de verificación. Ver §7 | Recorrido de la matriz al cerrar cada etapa | `Sin verificar` |
| Advertencias de construcción | Exactamente **0** | Gate `QG-10001`, **no un caso de verificación** | Etapa de construcción del flujo de publicación | `Pendiente` |

**El único valor rotulado [ASUNCIÓN] se cita con su rótulo y no se convierte en compromiso.** Es la asunción `A-4` del intake §22, y lo que rotula es **expresar la regla acumulativa como puerta con umbral del 100 %**, no la regla en sí. **`QG-10004` bloquea igual**: la columna «Si el Product Owner la cambia» de `A-4` dice que «cambia la forma del gate, no su carácter bloqueante», y §17.2.P.6 · GeometriaFactory-Web lo llama «gate bloqueante y numérico» ([`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1).

**Dos de los catorce NFR no tienen caso de verificación propio y es correcto que no lo tengan**: uno lo cubre la matriz de sensado, que es un instrumento distinto y ya emitido, y el otro es una puerta del flujo de publicación. Inventarles un `TC-XX` habría duplicado un instrumento que ya existe.

**No hay NFR de cobertura de líneas ni de tiempo de respuesta**, y `05` §8 declara el fundamento de las dos ausencias. Esta matriz no les inventa fila.

### 3.2 `GeometriaFactory-Visor`

Ocho filas, una por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §8. Los seis primeros son las **seis propiedades transversales** de `02` §6, con las condiciones de medición que esa sección declara como lugar único.

| NFR | Objetivo numérico | Condición de medición | Test | Estado |
| --- | --- | --- | --- | --- |
| Cero red | Exactamente **0** peticiones originadas por el archivo de guion | **Con los dos movimientos prendidos y sostenidos**, y durante los gestos de rotar y acercar | `TC-12016`, `TC-12018` | `Pendiente` |
| Cero persistencia | **0** claves escritas en el almacenamiento del navegador, y ningún estado conservado entre páginas | Cualquier estado de los movimientos; se comprueba además que recargar no repone la preferencia | `TC-12017`, `TC-12003` | `Pendiente` |
| Se ejercita sin backend | Recorrido completo de las **seis** funciones con **0 servicios del backend disponibles** | Sin condición adicional | `TC-12015` | `Pendiente` |
| Disposición determinista | Dos procesados del mismo texto producen la **misma disposición**, comparable pieza por pieza | **Se compara posición, no orientación**; vale con cualquier estado de los movimientos | `TC-12009` | `Pendiente` |
| Liberación de recursos | **10 recorridos** de ida y vuelta sin degradación | **Con los dos movimientos prendidos** durante los recorridos | `TC-12004` | `Pendiente` |
| Ausencia de fallo silencioso | **100 %** de las piezas no dibujadas enumeradas con índice y código, y **0** sin registro | Sin condición adicional | `TC-12007` | `Pendiente` |
| Dependencias traídas de una red de distribución externa en tiempo de ejecución | Exactamente **0** | Página abierta sin acceso a redes externas; puerta `PT-03` | `TC-12019` | `Pendiente` |
| Superficie pública del bundle | Exactamente **6** funciones, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos | Inspección del **bundle generado**, no sólo de la fuente | `TC-12018` | `Pendiente` |

**Ocho de ocho NFR con caso de prueba.**

**Las condiciones de medición son vinculantes y no se redefinen acá.** `02` §6 es su lugar único; esta tabla las transcribe. Una medición hecha sin su condición no cuenta como medición: mediría el caso fácil.

**No hay NFR de latencia con umbral numérico**, y esta categoría **no lo inventa**. `05` §8 declara que la fuente no fija un valor de fluidez y lo deja como punto abierto `PA-03`; ver §8.

## 4. Trazabilidad RN ↔ tests

### 4.1 `GeometriaFactory-Web`

Dieciséis filas, una por regla. **Este proyecto de código no hace cumplir ninguna regla de negocio** (`02` §5, `05` §10.3): lo que esta tabla declara es **qué hace esta pieza por cada una** y con qué se verifica, que es una cosa distinta. Las reglas se enuncian en `GeometriaFactory-Domain`.

| RN | Qué hace esta pieza por ella | Tests | Estado |
| --- | --- | --- | --- |
| RN-10001 Administrador único y papeles fijos | Ofrece el aprovisionamiento **una sola vez** y deja de armarlo; no dibuja el destino del otro papel **ni deshabilitado** | `TC-10001`, `TC-10005` | `Pendiente` |
| RN-10002 El correo del alumno es único | Presenta el rechazo del registro como error de operación, **sin revelar de quién es la cuenta** | `TC-10002` | `Pendiente` |
| RN-10003 Trabajo ajeno indistinguible de inexistente | Presenta los dos con **el mismo mensaje**, y **verifica la acotación forzando la solicitud** | `TC-10026` | `Pendiente` |
| RN-10004 Eliminación acotada al borrador | **No dibuja el control** fuera de `Borrador`, en lugar de dibujarlo inhabilitado; y fuerza la solicitud para verificar la acotación | `TC-10015`, `TC-10025` | `Pendiente` |
| RN-10005 No se pasa a estado `Pendiente` con errores de validación | Presenta el estado resultante del envío con sus observaciones, y declara que **la previsualización dibuja y no verifica** | `TC-10012`, `TC-10013`, `TC-10014` | `Pendiente` |
| RN-10006 Cuenta `Pendiente` o `Bloqueado` sin acceso | Muestra el motivo de la situación al intentar ingresar, sin sesión | `TC-10004` | `Pendiente` |
| RN-10007 Baja con arrastre y confirmación escrita | Exige el correo escrito y **declara el arrastre antes del intento, en el mismo lugar donde se confirma** | `TC-10009` | `Pendiente` |
| RN-10008 Texto original conservado íntegro | Envía el texto **carácter por carácter** y lo muestra sin reescribirlo | `TC-10011` | `Pendiente` |
| RN-10009 Observación de error con posición y campo | Presenta cada observación con índice y campo, y **nunca mezcla las piezas no dibujadas con las observaciones** | `TC-10014`, `TC-10018` | `Pendiente` |
| RN-10010 Desenlace exclusivo del administrador y terminalidad | No ofrece salida de los estados terminales, y aloja el bloque de decisión **sólo** para el administrador y sobre un trabajo en estado `Pendiente` | `TC-10016`, `TC-10025` | `Pendiente` |
| RN-10011 El administrador no ve los borradores | **No los pide**: el listado se trae ya acotado, y pedir un borrador por dirección directa devuelve «no encontrado» | `TC-10024`, `TC-10026` | `Pendiente` |
| RN-10012 El reseteo conserva la cuenta y sus trabajos | **Declara en la superficie, antes del intento, que no se pierde ningún trabajo** | `TC-10010` | `Pendiente` |
| RN-10013 Cambio forzado antes de toda otra capacidad | El **cuarto guardián**: la única ruta alcanzable es el cambio de la propia contraseña, **sin sesión de trabajo** | `TC-10006`, `TC-10007` | `Pendiente` |
| RN-10014 La provisoria la produce el sistema | **Ningún campo de contraseña** en el formulario de reseteo, y la provisoria producida se le muestra al administrador | `TC-10010` | `Pendiente` |
| RN-10015 Resetear no exige cuenta habilitada | **Por ausencia**: la superficie no condiciona el reseteo al estado de la cuenta ni declara ningún motivo por ese concepto | `TC-10010` | `Pendiente` |
| RN-10016 Habilitar produce la provisoria | Muestra la provisoria **también al habilitar**, y por eso el primer ingreso recorre **el mismo formulario** que los otros dos cursos | `TC-10006`, `TC-10008` | `Pendiente` |

**Dieciséis de dieciséis reglas con al menos un caso de verificación.** La columna del medio nunca dice «hace cumplir»: dice qué ofrece, qué no dibuja o qué declara. **Cuando lo que hace es acotar, el caso de verificación fuerza la solicitud**, porque acotar no prueba nada por sí solo.

### 4.2 `GeometriaFactory-Visor`

**Este proyecto de código no tiene reglas de dominio**, y no es una omisión: es un visualizador puro y las reglas del trabajo del alumno las decide el backend (`02` §5.2; intake §14 `RA-02`, §17.2.P.5 · GeometriaFactory-Visor y P.11 punto 4). Lo que tiene son **condiciones de contrato**, que están en §6 de esta matriz.

La tabla se emite igual, con las **dieciséis** reglas del producto, para declarar de forma verificable **que ninguna se verifica acá y dónde se verifica cada una**. Dieciséis filas, ninguna agrupada.

| RN | ¿La verifica este proyecto de código? | Dónde se verifica |
| --- | --- | --- |
| RN-12001 Administrador único y papeles fijos | No. La fachada no sabe quién mira ni qué papel cumple | `GeometriaFactory-Domain` |
| RN-12002 El correo del alumno es único | No | `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure` |
| RN-12003 Trabajo ajeno indistinguible de inexistente | No. La fachada dibuja el mismo trabajo sin saber de quién es | `GeometriaFactory-Domain` |
| RN-12004 Eliminación acotada al borrador | No | `GeometriaFactory-Domain` |
| RN-12005 Sin errores de validación no hay estado `Pendiente` | No, **y es la frontera que más se confunde**: el visor informa por qué no dibujó una pieza, y **decidir si el trabajo pasa a `Pendiente` es del validador** (intake §20.E-8 punto 4) | `GeometriaFactory-Domain` y `GeometriaFactory-Infrastructure` |
| RN-12006 Cuenta `Pendiente` o `Bloqueado` sin acceso | No. La fachada no participa de ninguna decisión de autorización | `GeometriaFactory-Domain` |
| RN-12007 Baja con arrastre y confirmación escrita | No | `GeometriaFactory-Domain` |
| RN-12008 Texto original conservado íntegro | No. La fachada **no conserva ni reescribe** el texto: lo recibe, lo lee y no lo persiste | `GeometriaFactory-Domain` |
| RN-12009 Observación de error con posición y campo | No. La fachada **no emite observaciones**, ni advertencias ni errores de validación. Lo que sí emite es la **enumeración de piezas no dibujadas con su índice y su código**, que es otra cosa y se verifica en `TC-12007` | `GeometriaFactory-Domain` y `GeometriaFactory-Contracts` |
| RN-12010 Desenlace exclusivo y terminal | No | `GeometriaFactory-Domain` |
| RN-12011 El administrador no ve los borradores | No | `GeometriaFactory-Domain` |
| RN-12012 El reseteo conserva la cuenta y sus trabajos | No | `GeometriaFactory-Domain` |
| RN-12013 Cambio forzado antes de toda otra capacidad | No | `GeometriaFactory-Domain` |
| RN-12014 La provisoria la produce el sistema | No | `GeometriaFactory-Infrastructure` |
| RN-12015 Resetear no exige cuenta habilitada | No | `GeometriaFactory-Domain` |
| RN-12016 Habilitar produce la provisoria | No | `GeometriaFactory-Domain` |

**Cero de dieciséis, y es el resultado correcto.** Un caso de prueba de este proyecto de código que verificara una regla de negocio sería un defecto de titularidad: le atribuiría a un visualizador puro una decisión que `RA-02` le prohíbe tomar.

## 5. Trazabilidad restricción transversal ↔ tests

### 5.1 `GeometriaFactory-Web`

Trece filas, una por restricción de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6. El componente que la sostiene es el que `05` §10.2 le asigna.

| RT | Qué exige, en una línea | Componente que la sostiene | Tests | Estado |
| --- | --- | --- | --- | --- |
| RT-01 | Ninguna llamada al servicio de datos se origina en el navegador | Cliente tipado | `TC-10029`, `TC-10030` | `Pendiente` |
| RT-02 | La credencial de sesión vive en el estado del circuito y no aparece en el navegador | Sesión y estado del circuito | `TC-10003` | `Pendiente` |
| RT-03 | Ningún mensaje mostrado incluye dirección, archivo de datos ni traza | Traductor de condiciones | `TC-10031` | `Pendiente` |
| RT-04 | El bundle se invoca exclusivamente por sus **seis** funciones | Anfitrión del visor | `TC-10032` | `Pendiente` |
| RT-05 | La liberación de la instancia **no es opcional** | Anfitrión del visor | `TC-10021` | `Pendiente` |
| RT-06 | La pieza pública **no guarda estado propio**: ni copia local, ni caché, ni réplica | Servicios de aplicación de front | `TC-10003`, `TC-10027` | `Pendiente` |
| RT-07 | La indisponibilidad es **estado degradado explícito**, y el vacío se distingue del fallo **por el tipo recibido** | Traductor de condiciones, Superficies | `TC-10027`, `TC-10028` | `Pendiente` |
| RT-08 | El texto original se envía carácter por carácter y no se reescribe | Servicios de aplicación de front | `TC-10011` | `Pendiente` |
| RT-09 | Ninguna ruta del panel es accesible sin sesión, y el alumno no alcanza rutas de administrador. **Acota lo que se ofrece** | Armazón y encaminamiento | `TC-10005`, `TC-10007` | `Pendiente` |
| RT-10 | Sin tráfico de circuito durante la interacción, y el texto viaja **una sola vez por trabajo** | Anfitrión del visor | `TC-10033` | `Pendiente` |
| RT-11 | Sin capacidad gráfica la escena no es soportada, **y el resto sigue disponible** | Anfitrión del visor, Superficies | `TC-10023` | `Pendiente` |
| RT-12 | Una cuenta marcada llega sólo al cambio de su propia contraseña, **y sin sesión de trabajo** | Armazón y encaminamiento | `TC-10007` | `Pendiente` |
| RT-13 | El anfitrión manda **dos valores de verdad** y **lee él la preferencia de movimiento reducido** | Anfitrión del visor | `TC-10022` | `Pendiente` |

**Trece de trece con caso de verificación.** Las que exigen una **ausencia** —`RT-01`, `RT-04`, `RT-06`, `RT-10`— se verifican con umbral cero y en la condición declarada, nunca por no haberse observado lo contrario.

## 6. Cobertura por capa

### 6.1 `GeometriaFactory-Web`

La partición es por los **ocho** componentes de `05` §3.1, agrupados en las **tres** capas de presentación que [`ADR-10004`](../05-Arquitectura-Tecnica/Adrs/ADR-10004-Tres-Capas-De-Presentacion.md) declara. **No hay porcentaje de líneas que reportar**, y el motivo está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2: no hay proyecto de pruebas propio. Lo que se declara es qué ejerce cada componente.

| Capa | Componente | Casos de verificación que lo ejercen | Cobertura de líneas | Umbral |
| --- | --- | --- | --- | --- |
| 1 | Armazón y encaminamiento | `TC-10005`, `TC-10007`, `TC-10035` | **No aplica** | Sin umbral: no hay proyecto de pruebas propio |
| 1 | Superficies | `TC-10035`, y los `TC-XX` de cada superficie | **No aplica** | Ídem. Su cobertura contable son las **11 de 11** superficies sensadas |
| 1 | Representaciones reutilizadas | `TC-10015` (fila de trabajo), `TC-10018` (lista de observaciones), `TC-10035` (sello de versión) | **No aplica** | Ídem |
| 2 | Servicios de aplicación de front | `TC-10028`, `TC-10030` | **No aplica** | Ídem |
| 2 | Sesión y estado del circuito | `TC-10003`, `TC-10027` | **No aplica** | Ídem |
| 2 | Traductor de condiciones a presentación | `TC-10028`, `TC-10031` | **No aplica** | Ídem. Su cobertura contable son los **17 de 17** códigos vivos más el camino de ausencia |
| 3 | Cliente tipado del servicio de datos | `TC-10029`, `TC-10030` | **No aplica** | Ídem |
| 3 | Anfitrión del visor | `TC-10020`, `TC-10021`, `TC-10022`, `TC-10032`, `TC-10033` | **No aplica** | Ídem. Su cobertura contable son las **6 de 6** funciones de la fachada |

**«No aplica» y no «0 %».** Un cero afirmaría que hay una medición cuyo resultado es cero; acá **no hay medición porque no hay instrumento**, y la fuente lo declara así. Si en alguna etapa se agregan pruebas automatizadas de componentes, esta tabla gana su columna de umbral con su fila de control de cambios.

**Los ocho componentes tienen al menos un caso de verificación que los ejerce, y ninguno queda sin ejercer.**

### 6.2 `GeometriaFactory-Visor`

La partición es por los **seis** componentes de `05` §3.1, dos de los cuales no son de este proyecto de código.

| Componente | Capa | Métrica declarada | Medición | Umbral |
| --- | --- | --- | --- | --- |
| Componente anfitrión | 1, **fuera de este proyecto de código** | — | — | No aplica: su cobertura es de la categoría 08 de `GeometriaFactory-Web` |
| Fachada plana | 2 | Funciones ejercitadas | Sin medir | **6 de 6** |
| Registro de instancias | 2 | Cursos del ciclo de vida del identificador | Sin medir | 100 %: válido, ya liberado, inexistente |
| Lector del texto | 3 | Tipos dibujables y variantes de clave | Sin medir | **6 de 6** tipos; `Tapas` y `Bases` como sinónimos; **el cero como dimensión legible** |
| Servicio de dibujo | 3 | Garantías que lo alcanzan | Sin medir | `G-5`, `G-6`, disposición determinista y liberación de recursos |
| Motor de dibujo tridimensional | 3, **empaquetado** | — | — | **No se prueba por dentro**, y es deliberado ([`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md)) |
| **Bundle generado** | — | Recuentos de superficie y de ausencia | Sin medir | **6**, **1**, **0**, **0**, **0**, **0** |

**«Sin medir» y no «0 %».** No hay bundle construido.

**No hay columna de líneas, de ramas ni de mutation score**, y las tres ausencias están declaradas con su motivo en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2: el intake pone un gate de inspección **en lugar de** la cobertura de líneas, y mutar código de dibujo produciría mutantes que sólo una comparación de imágenes podría matar, técnica que §1 de ese documento descarta con su fundamento.

## 7. Relación con la matriz de sensado de deriva

### 7.1 `GeometriaFactory-Web`

[`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2 **ya existía en esta carpeta antes de esta fase** y es un artefacto vigente de la categoría. Esta matriz de cobertura **no la duplica**: la cita.

| Instrumento | Qué responde | Unidad | Cuántas |
| --- | --- | --- | --- |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | ¿Lo construido se sigue pareciendo a lo que el Product Owner aprobó mirando? | Sonda `SD-XX`, con su **umbral de deriva** | **61** |
| Esta matriz, sobre [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | ¿El sistema hace lo que las historias dicen? | Caso de verificación `TC-XX`, con su **criterio de aceptación** | **35** |

**Ningún `TC-XX` redefine el umbral de una sonda**, y ninguna sonda declara un criterio de aceptación. Cuando los dos miran el mismo elemento, el caso de verificación **cita** la sonda en su columna de upstream, que es lo que hace [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §2 en cada ficha. La resolución del método de verificación de las 61 filas, por familia y con su etapa, está en [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.1.

**Los 74 estados, las 11 superficies, los 73 componentes y las 24 rutas de la línea de base son cobertura de la matriz de sensado y no de esta matriz.** Por eso la fila correspondiente de §3 remite a ella y no a un `TC-XX`.

## 8. Huecos identificados

### 8.1 `GeometriaFactory-Web`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **Los elementos de interfaz de la capacidad `F-26` no tienen sonda** en la matriz de sensado, porque son posteriores a la aprobación de la maqueta y no tienen identificador en la línea de base | El reseteo desde el panel, su diálogo, la comunicación de la provisoria y el **tercer curso** de la superficie de credencial se verifican contra los criterios de aceptación de `CU-10003` y `CU-10004` —`TC-10006`, `TC-10007`, `TC-10010`— pero **no tienen umbral de deriva** | La **iteración 5** de maqueta y la reemisión de la línea de base que la propia matriz declara pendiente en su §4. **No se les inventa sonda acá**: una sonda anclada en un identificador inexistente diría comparar contra algo que la línea de base no contiene |
| **No hay proyecto de pruebas propio** ni umbral de cobertura de líneas | Toda la verificación funcional es observada, y su reproducibilidad depende de que el guion esté escrito paso por paso | Es lo que la fuente declara (intake §17.2.P.6 · GeometriaFactory-Web). La compensación son las **cinco** inspecciones estructurales con umbral cero y las 61 sondas. Si se agregan pruebas automatizadas de componentes, su umbral se fija en ese momento |
| **El valor rotulado [ASUNCIÓN]** —la forma de la puerta del guion— sigue sin confirmar | **Ninguna sobre el carácter del gate**: `QG-10004` bloquea, porque `A-4` declara que un cambio del Product Owner cambia la forma y no el carácter. Lo que puede cambiar es **cómo** se mide. **La regla acumulativa rige**: no es asunción de nadie | El Product Owner sobre el intake §22, asunción `A-4`, antes de fijar la forma de la puerta en `09-Devops` |
| **No hay umbral de tiempo de respuesta** (`05` §11 `PA-04`) | Ningún caso de verificación puede declarar que una pantalla tardó demasiado. Lo que sí se verifica es que el indicador de espera aparezca cuando corresponde | El Product Owner, o esta categoría al fijar su guion de medición, **después** de `PT-01`. **No se inventa uno acá**, por el mismo criterio con el que `05` §8 no lo inventó |
| **El formato de intercambio y su configuración** no están fijados (`05` §11 `PA-03`) | `TC-10031` verifica la traducción de los diecisiete códigos, pero la forma en que llegan depende de una decisión de los dos extremos | La categoría 05 de `GeometriaFactory-Api`, como productor, con esta pieza como consumidor |
| ~~**Ninguna fila `VER-XX`** en la matriz de sensado~~ · **Cerrado el 2026-08-11** | Se declaraba porque no había sondas de contrato de verificación | **Cerrado**: se emitió [`../10-Examples/`](../10-Examples/) con **un** contrato de verificación, `VER-10001`, y con él [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.3** sumó la fila **`SD-10062`**, en `Sin verificar`. Es la primera fila de esa matriz que trae **su propio comando y su propia aserción**, y no desplaza al guion de demostración de su papel. La fila se conserva con su desenlace en lugar de retirarse |

### 8.2 `GeometriaFactory-Visor`

| Hueco | Consecuencia | Plan de remediación |
| --- | --- | --- |
| **El umbral numérico de fluidez no existe** (`05` §11 `PA-03`, `BT-12018`) | La interacción fluida se verifica de forma **cualitativa declarada** junto con `PT-02`, y no con un número | `BT-12018`, antes de cerrar la etapa `g`: o el Product Owner fija un umbral, o esta categoría fija su guion de medición cualitativo. **Ninguna de las dos salidas es inventar un número**, y `05` §8 se niega explícitamente a hacerlo |
| **La versión del motor de dibujo no está anclada** (`05` §11 `PA-01`, `BT-12009`) | Si la versión que se adopte exige una interfaz distinta de la del visualizador previo, la capa 3 se rehace y varios casos de prueba se reescriben | `BT-12009`, antes de comprometer la etapa `g`, que es cuando se miden `PT-02` y `PT-03` |
| **La versión mínima de navegador no está fijada** (`05` §11 `PA-04`) | El requisito se declara **por capacidad** y no por versión, de modo que `TC-12002` verifica la ausencia de capacidad gráfica y no una versión | El Product Owner sobre su propio documento, sin fecha comprometida. **No es bloqueante** |
| ~~**No hay filas `VER-XX` en la matriz de sensado de deriva**~~ · **Cerrado el 2026-08-11** | Se declaraba porque las sondas de contrato y comportamiento que la categoría 10 aporta todavía no existían | **Cerrado**: [`../10-Examples/`](../10-Examples/) se emitió en su pasada de diseño y desarrolló el sample **S-1** en **tres** partes, con un contrato de verificación cada una. [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.1 les dio de alta las filas `SD-12013`, `SD-12014` y `SD-12015`, todas en `Sin verificar`, y pasó de **doce** a **quince** sondas. La fila se conserva con su desenlace en lugar de retirarse |
| **Las pruebas de extremo a extremo exigen un navegador con capacidad gráfica en el entorno de ejecución** | Un entorno de integración continua sin esa capacidad no puede medir `PT-02`, `TC-12016` ni `TC-12017` | Es una condición del ambiente, declarada en [`Estrategia-Testing.md`](Estrategia-Testing.md) §7. Su provisión concreta pertenece a `09-Devops` |

## 9. Trazabilidad garantía ↔ tests

### 9.1 `GeometriaFactory-Visor`

Siete filas, `G-1` a `G-7`, las de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2. Ninguna se agrupa.

| Garantía | Enunciado, en una línea | Componente que la sostiene (`05` §10.2) | Test | Estado |
| --- | --- | --- | --- | --- |
| G-1 · Cero red | Ninguna función ni ningún movimiento origina una petición | Todos, por ausencia; se verifica sobre el bundle entero | `TC-12016`, `TC-12018`, `TC-12001`, `TC-12015` | `Pendiente` |
| G-2 · Cero persistencia | Ninguna función escribe en el almacenamiento del navegador | Todos, por ausencia | `TC-12017`, `TC-12003` | `Pendiente` |
| G-3 · Sin configuración propia | Todo lo que la instancia necesita llega por parámetro | Fachada plana | `TC-12003`, `TC-12001` | `Pendiente` |
| G-4 · Aislamiento entre instancias | Dos instancias vivas no comparten escena, ni selección, ni disposición | Registro de instancias, servicio de dibujo | `TC-12001`, `TC-12004`, `TC-12011` | `Pendiente` |
| G-5 · Sin fallo silencioso | Toda pieza no dibujada queda enumerada con su índice | Lector del texto, servicio de dibujo | `TC-12007` | `Pendiente` |
| G-6 · Determinismo | La misma entrada produce la misma **posición** de cada pieza, no la misma orientación | Servicio de dibujo | `TC-12009` | `Pendiente` |
| G-7 · Terminación controlada | O la operación surte efecto completo, o la instancia queda como estaba | Fachada plana | `TC-12002`, `TC-12010`, `TC-12011`, `TC-12012`, `TC-12013`, `TC-12004` | `Pendiente` |

**Siete de siete garantías con caso de prueba.** Perder cualquiera es **cambio mayor** aunque las seis firmas no se toquen, y por eso esta tabla existe: sin ella, la verificación de un cambio se limitaría a comprobar que las firmas siguen ahí.

## 10. Trazabilidad código de condición ↔ tests

### 10.1 `GeometriaFactory-Visor`

Ocho filas: **siete códigos**, uno de ellos con **dos cursos**, que es la forma en que §6 del contrato de fachada los declara. **Un curso no es un código.**

| Código | Curso | Test | Entrada de `03` | Estado |
| --- | --- | --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | Único | `TC-12002` | `E-VIS-01` | `Pendiente` |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-1, en creación** | `TC-12002` | `E-VIS-02` | `Pendiente` |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-2, en ajuste** | `TC-12012` | `E-VIS-07` | `Pendiente` |
| `INSTANCIA_DESCONOCIDA` | Único, **en cinco funciones** | `TC-12004`, `TC-12011`, `TC-12013` | `E-VIS-03` a `E-VIS-06` y `E-VIS-13` | `Pendiente` |
| `TEXTO_NO_LEGIBLE` | Único | `TC-12010` | `E-VIS-08` | `Pendiente` |
| `TIPO_NO_DIBUJABLE` | Único, por pieza | `TC-12007` | `E-VIS-09` | `Pendiente` |
| `DIMENSION_NO_LEGIBLE` | Único, por pieza | `TC-12007` | `E-VIS-10` | `Pendiente` |
| `INDICE_FUERA_DE_RANGO` | Único, con **dos casos** | `TC-12011` | `E-VIS-11`, `E-VIS-12` | `Pendiente` |

**Siete de siete códigos cubiertos, en ocho filas de curso.** El catálogo de `03` los desarrolla en **trece** entradas porque su unidad de catalogación es la **función** y no el código; esta matriz sigue la unidad del contrato, que es la condición. `TC-12021` verifica que las dos cifras no se confundan y que **ningún código se acuñe aguas abajo**.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **5 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
