# Casos de prueba referenciales — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Casos-Prueba-Referenciales.md
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

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Catálogo de casos de prueba»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Cómo se lee este catálogo

### 1.1 `GeometriaFactory-Web`

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: la maqueta está aprobada y validada, pero el sistema arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Paso de guion**: una acción de la persona en el navegador, con su resultado observable, ejecutada de forma acumulativa ([`Estrategia-Testing.md`](Estrategia-Testing.md) §1).
- **Inspección estructural**: la que comprueba una propiedad del árbol de fuentes o del tráfico observado, con umbral cero.
- **Verificación forzando la solicitud**: la que comprueba una acotación **sin pasar por la pantalla**. Es obligatoria porque esta pieza **no hace cumplir reglas** (`02` §5).
- **Sonda**: una fila `SD-XX` de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), con su umbral de deriva propio. Un caso de prueba **cita** sondas; no las redefine.
- **Superficie**: una de las once de la categoría 03, con su nombre canónico.

**Un caso de verificación no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten superficie, setup y forma de observación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

### 1.2 `GeometriaFactory-Visor`

Cada `TC-XX` declara los ocho campos de `Rules-Calidad-Y-Pruebas.md` §4.6. **Todas las salidas observadas dicen «Sin ejecutar» y todos los estados dicen `Pendiente`**: el bundle no está construido.

**Vocabulario propio de este catálogo**, declarado acá la primera vez que aparece:

- **Nivel**: la posición en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario, integración, extremo a extremo en página, o inspección del artefacto generado.
- **Condición de medición**: el estado en que hay que poner la escena para que la medición valga. Para cuatro de las **seis** propiedades transversales, `02` §6 la declara, y es **vinculante**.
- **Umbral cero**: la forma de aserción de una propiedad que es una **ausencia**. Un umbral cero sin condición de medición es un caso de prueba mal escrito ([`Estrategia-Testing.md`](Estrategia-Testing.md) §4).
- **Recorrido de ida y vuelta**: pasar de un trabajo a otro y volver, que es lo que `PT-02` cuenta diez veces. Se escribe siempre calificado, porque «recorrido» tiene un segundo referente en esta cadena.

## 2. Catálogo de casos de verificación

### 2.1 `GeometriaFactory-Web`

### 2.1 Acceso, identidad y credencial

#### TC-10001 — Aprovisionamiento-Inicial-Una-Sola-Vez-En-La-Vida-De-La-Instancia

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más verificación forzando la solicitud |
| Cubre | `CU-10004` FA-03; `RN-10001`; `US-10008`; sondas `SD-10001`, `SD-10015` |
| Setup | Instancia sin administrador configurado; servicio de datos levantado |
| Pasos | Given una instancia sin administrador, When se abre la dirección raíz, Then se llega a la superficie de aprovisionamiento inicial, que es **la única puerta del primer arranque**. When se configura el administrador, Then la superficie **deja de armarse para siempre** y la dirección redirige a «ya aprovisionado», **reemplazando la entrada del historial en vez de apilarla**. When se fuerza la solicitud de aprovisionamiento por segunda vez sin pasar por la pantalla, Then el servicio de datos la rechaza |
| Salida esperada | Un aprovisionamiento aplicado, una redirección que no apila historial y un rechazo forzado. **La acotación se verifica forzando y no mirando que el formulario no esté** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10002 — Registro-Sin-Campo-De-Contrasena-Y-Correo-Ya-Usado

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10001`; `RN-10002`; `US-10001`, `US-10002`; sondas `SD-10002`, `SD-10013` |
| Setup | Instancia con administrador configurado |
| Pasos | Given la superficie de registro, When se la inspecciona, Then **no tiene ningún campo de contraseña** y su subtítulo declara la expectativa de que la cuenta nace pendiente de habilitación. When se registra un correo libre, Then aparece el bloque de éxito. When se registra un correo ya usado, Then aparece un **error de operación** distinguible del error de entrada, **sin revelar de quién es la cuenta que lo ocupa** |
| Salida esperada | Un registro aceptado y un rechazo que no revela nada de la cuenta existente; los dos tipos de error distinguidos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10003 — La-Credencial-De-Sesion-No-Aparece-En-El-Navegador

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural |
| Cubre | `CU-10002` CA-02; `RT-02`; NFR de apariciones de la credencial (`05` §8); `US-10003`; `QG-07` |
| Setup | Sesión iniciada con una cuenta habilitada; panel de herramientas de desarrollo abierto |
| Pasos | Given una sesión iniciada, When se inspeccionan el almacenamiento del navegador, las marcas de sesión y el contenido servido, Then la credencial de sesión **no aparece en ninguno de los tres**. Then la única marca del navegador es la de sesión, con sus atributos de sólo servidor, canal seguro y origen estricto |
| Salida esperada | **0** apariciones de la credencial. Es criterio de aceptación de la etapa `c` y su umbral no admite gradación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10004 — Motivo-De-La-Cuenta-Que-No-Admite-Ingreso

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10002`; `RN-10006`; `US-10004`; sondas `SD-10003`, `SD-10015` |
| Setup | Tres cuentas: una `Pendiente`, una `Bloqueado` y una `Habilitado` |
| Pasos | Given la cuenta `Pendiente`, When intenta ingresar, Then se muestra **el motivo de su situación** y no obtiene sesión. Given la `Bloqueado`, Then el motivo es distinto del anterior. Given credenciales inválidas sobre la cuenta `Habilitado`, Then el mensaje **no declara cuál de los dos campos falló**. Then la nota sobre la contraseña olvidada sigue siendo **inerte**: el producto no tiene canal de correo |
| Salida esperada | Tres motivos distinguibles, un rechazo genérico que no discrimina campo, y la nota inerte que no dispara nada |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10005 — Cierre-De-Sesion-Y-Rutas-Acotadas-Por-Papel

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más verificación forzando la solicitud |
| Cubre | `CU-10002`; `RT-09`; `US-10005`; sondas `SD-10024`, `SD-10023` |
| Setup | Una cuenta de alumno habilitada y la de administrador |
| Pasos | Given el ingreso de cada papel, When se completa, Then lleva a **la ruta inicial que corresponde al papel** y la barra lateral ofrece **tres destinos por papel**, sin dibujar los del otro **ni siquiera deshabilitados**. When el alumno escribe una dirección de administrador, Then no llega. When se fuerza la solicitud correspondiente **sin pasar por la pantalla**, Then el servicio de datos la rechaza. When se cierra sesión, Then vuelve al ingreso con su banda de confirmación |
| Salida esperada | Dos recorridos por papel, una acotación verificada **forzando** y un cierre de sesión con banda. La acotación de rutas **no prueba nada por sí sola**: quien hace cumplir es el servicio de datos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10006 — Los-Tres-Cursos-Del-Mismo-Formulario-De-Credencial

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10003`; `RN-10016`, `RN-10013`; `US-10006`, `US-10007`, `US-10028`; sondas `SD-10004`, `SD-10016` |
| Setup | Una cuenta recién habilitada con su provisoria; una cuenta con credencial vigente; una cuenta recién reseteada |
| Pasos | Given el **primer ingreso** tras la habilitación, When se cambia la contraseña presentando **la provisoria como vigente**, Then procede y la marca se levanta. Given una cuenta con credencial vigente, When la cambia presentando la vigente, Then procede. Given una cuenta recién reseteada, When llega al **cambio forzado**, Then recorre **el mismo formulario** y **no tiene salida**. Given una confirmación que no coincide, Then error de entrada. Given la contraseña actual rechazada, Then error de operación **distinguible del anterior** |
| Salida esperada | **Los tres cursos son el mismo formulario y el mismo contrato**, y los dos errores no se colapsan. `SD-10004` sensa **dos** cursos porque el tercero es posterior a la maqueta ([`Estrategia-Testing.md`](Estrategia-Testing.md) §8): este caso verifica los tres contra los criterios de aceptación de `CU-10003` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10007 — Confinamiento-De-La-Cuenta-Marcada-Sin-Sesion-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más verificación forzando la solicitud |
| Cubre | `CU-10002` FA-07, `CU-10003` FA-05; `RN-10013`; `RT-12`; `US-10029` |
| Setup | Una cuenta de alumno con la marca de cambio de contraseña pendiente puesta |
| Pasos | Given la cuenta marcada, When ingresa, Then llega **únicamente** a la superficie de credencial propia, **en el shell de acceso y sin barra lateral**: no obtiene sesión de trabajo. When escribe cualquier otra dirección del panel, Then no llega. When **fuerza la solicitud** de cualquier otra capacidad sin pasar por la pantalla, Then el servicio de datos la rechaza. When cambia su contraseña, Then la marca se levanta y **recién entonces** obtiene sesión de trabajo |
| Salida esperada | Confinamiento observado y **verificado forzando**, más el levantamiento de la marca con el cambio hecho por la propia cuenta. Es el cuarto guardián de `05` §3.1 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Cuentas de la comisión

#### TC-10008 — Las-Cinco-Operaciones-Del-Panel-De-Cuentas

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10004`; `RN-10001`, `RN-10006`, `RN-10016`; `US-10009`; sondas `SD-10009`, `SD-10019`, `SD-10035` |
| Setup | Administrador con sesión; cuentas de alumno en los tres estados |
| Pasos | Given el panel de cuentas, When se lo abre, Then cada fila muestra su **insignia de situación con su texto siempre presente**, rotulada «situación» y no «estado». When se abre la acción de situación, Then **ofrece sólo la transición admitida** y no las tres a la vez. When se habilita una cuenta, Then **se muestra la provisoria al administrador para que la comunique**. When se rehabilita, Then ocurre lo mismo |
| Salida esperada | Las cinco operaciones alcanzables desde la misma lista, con la transición única por estado y la provisoria comunicada en las dos operaciones que la producen |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10009 — Baja-Con-Correo-Escrito-Y-Arrastre-Declarado-Antes-Del-Intento

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10004` FA-02; `RN-10007`; `US-10010`; sondas `SD-10009`, `SD-10019` |
| Setup | Administrador con sesión; una cuenta de alumno con trabajos |
| Pasos | Given la baja de una cuenta, When se abre la confirmación, Then **el aviso de arrastre está en el mismo lugar donde se confirma** y declara qué se pierde **antes del intento**. When el correo escrito no coincide, Then no procede. When coincide, Then procede y aparece la orientación posterior |
| Salida esperada | Una baja aplicada y un rechazo, con el aviso de arrastre presente en el lugar de la confirmación. Falta de ese aviso en ese lugar es **deriva mayor** por `SD-10009` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10010 — Reseteo-Que-Declara-Que-No-Se-Pierde-Nada-Y-Comunica-La-Provisoria

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10004` FA-06; `RN-10012`, `RN-10014`, `RN-10015`; `US-10030` |
| Setup | Administrador con sesión; cuentas de alumno en los **tres** estados, cada una con trabajos |
| Pasos | Given la fila de una cuenta, When se abre el reseteo, Then el formulario **no tiene ningún campo de contraseña** y **declara antes del intento que no se pierde ningún trabajo**. When se confirma, Then **se muestra la provisoria producida por el sistema** para que el administrador la comunique. Given una cuenta `Pendiente` y una `Bloqueado`, When se las resetea, Then **procede igual**: la operación no exige que la cuenta esté habilitada. When se recorre el listado de la cuenta después, Then **todos sus trabajos siguen ahí con sus estados** |
| Salida esperada | Tres reseteos sobre los tres estados de cuenta, la provisoria comunicada, ningún campo de contraseña y el recuento de trabajos idéntico antes y después. **Esta superficie no tiene sonda en la matriz** y el motivo está declarado en [`Estrategia-Testing.md`](Estrategia-Testing.md) §8: es posterior a la aprobación de la maqueta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajo, envío e interpretación

#### TC-10011 — Texto-Pegado-Y-Enviado-Sin-Reescribir-Un-Caracter

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más comparación carácter por carácter |
| Cubre | `CU-10005`; `RN-10008`; `RT-08`; `US-10011`; sondas `SD-10036`, `SD-10006` |
| Setup | Alumno con sesión; el texto del escenario **`E-2`** del intake §20, **con sus dos comas finales** |
| Pasos | Given el texto de `E-2` pegado tal cual, When se lo envía, Then lo que llega al servicio de datos es **idéntico carácter por carácter** a lo pegado. When se abre el trabajo, Then el texto se muestra **sin normalizar, sin reordenar y sin perder ningún carácter**, con avance uniforme por carácter y colapsado por omisión |
| Salida esperada | Comparación byte a byte sin diferencias. **Cualquier normalización es deriva mayor sin gradación** por `SD-10036`, y viola `RN-10008` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10012 — Previsualizar-Declarando-Que-Dibujar-No-Es-Verificar

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más conteo en la pestaña de red |
| Cubre | `CU-10005`; `US-10012`; sondas `SD-10006`, `SD-10017` |
| Setup | Alumno con sesión; el texto del escenario `E-1` |
| Pasos | Given el texto pegado, When se previsualiza, Then la escena se arma **sin emitir ninguna petición hacia el servicio de datos**, y la superficie **declara que dibujar no es verificar**. When se envía, Then aparece el bloque de resultado con el estado que la interpretación decidió |
| Salida esperada | Previsualización con **0** peticiones y una declaración explícita de que no verifica. Una petición acá es **deriva mayor** por `SD-10006` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10013 — Advertencias-Con-El-Par-Declarado-Y-Derivado-Sin-Reformatear

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10005`, `CU-10007`; `RN-10005`, `RN-10009`; `US-10013`; sondas `SD-10017`, `SD-10033`, `SD-10037`, `SD-10038` |
| Setup | Alumno con sesión; los textos de **`E-1`** y **`E-3`** del intake §20 |
| Pasos | Given el texto de `E-1`, When se lo envía, Then el resultado declara **3 piezas y 2 advertencias**, el trabajo pasa a estado `Pendiente` y **la advertencia no impide la entrega**. Then **el cilindro no produce ninguna observación**: la diferencia de `0.01` no supera la tolerancia estricta. Given el de `E-3`, When se lo envía, Then la advertencia de área muestra **36.00 declarado y 54.00 derivado**, los dos **exactamente como llegan, sin reformatear**, y no un texto genérico |
| Salida esperada | Dos envíos que pasan a estado `Pendiente` con sus advertencias; **exactamente dos** advertencias en `E-1` —una tercera significaría que el operador de tolerancia dejó de ser estricto— y el par de valores presentado sin reformatear |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10014 — Errores-Con-Indice-De-Figura-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10005`, `CU-10007`; `RN-10009`, `RN-10005`; `US-10014`; sondas `SD-10030`, `SD-10040`, `SD-10018` |
| Setup | Alumno con sesión; los textos de **`E-5`** y **`E-8`** del intake §20 |
| Pasos | Given el texto de `E-5`, When se lo envía, Then el error se presenta con **índice de figura 1** y **campo `Tipo`**, nunca con un texto genérico, y el trabajo **queda en `Borrador`**. Given el de `E-8`, When se lo envía, Then **también queda en `Borrador`** y la pieza no dibujada **queda enumerada con su índice y su código**, separada de la lista de observaciones. Then **el árbol muestra las dos piezas**, incluida la que no se dibujó |
| Salida esperada | Dos envíos que no transicionan, con el error localizado y la pieza no dibujada enumerada. **Un error sin índice o sin campo es deriva mayor** por `SD-10030`; una pieza que desaparece sin registro lo es por `SD-10040` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10015 — Listado-Propio-Con-Los-Cuatro-Estados-Y-Controles-No-Dibujados

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más verificación forzando la solicitud |
| Cubre | `CU-10006`; `RN-10004`; `US-10015`, `US-10016`; sondas `SD-10005`, `SD-10019`, `SD-10035` |
| Setup | Alumno con sesión y trabajos en los **cuatro** estados; un alumno sin trabajos |
| Pasos | Given trabajos en los cuatro estados, When se abre el panel propio, Then cada fila muestra su estado con **texto siempre presente** y `Pendiente` **siempre calificado**. Then los controles de reeditar y eliminar **no se dibujan** fuera de `Borrador`, en lugar de dibujarse inhabilitados. When se **fuerza la solicitud** de eliminar un trabajo que no está en `Borrador`, Then el servicio de datos la rechaza. Given el alumno sin trabajos, Then el estado vacío **está explicado** y no es un hueco |
| Salida esperada | Cuatro estados distinguibles, controles ausentes donde no corresponden, acotación verificada **forzando** y estado vacío explicado |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10016 — Desenlace-Y-Comentario-Del-Trabajo-Propio

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10006`, `CU-10007`; `RN-10010`; `US-10017`; sondas `SD-10021`, `SD-10058` |
| Setup | Alumno con sesión y trabajos `Finalizado` y `Rechazado`, uno con comentario y otro sin él |
| Pasos | Given los dos trabajos, When se abre el listado propio, Then **el desenlace se ve en la fila**. When se abre el trabajo, Then **el comentario aparece al abrirlo y no en el listado**, en la columna izquierda y **separado de las observaciones**. Given el trabajo sin comentario, Then se dibuja el estado «sin comentario» y no un hueco |
| Salida esperada | Desenlace en el listado, comentario al abrir, y el comentario **nunca presentado como observación ni como calificación** —lo que sería deriva mayor por `SD-10058`— |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Vista de trabajo y escena

#### TC-10017 — Los-Cuatro-Elementos-De-La-Vista-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10007`; `US-10018`; sondas `SD-10007`, `SD-10039`, `SD-10055` |
| Setup | Alumno con sesión y un trabajo con el texto de **`E-6`**; y otro con el de **`E-7`** |
| Pasos | Given un trabajo abierto, When se mira la vista, Then tiene **sus cuatro partes** —datos, texto, escena y árbol—, con el comentario y las observaciones en la columna izquierda. Given el texto de `E-6`, When se dibuja, Then **la pieza con dimensión `0.00` se dibuja** y no produce condición de dibujo. Given el de `E-7`, Then se dibujan **seis piezas**, una por cada tipo, con el ortoedro leído por su clave `Bases`. When se cruza el punto de quiebre, Then la escena va primero y se ajusta |
| Salida esperada | Cuatro partes presentes, seis tipos dibujados y la figura de dimensión cero visible. **Perder la figura de `E-6` es deriva mayor sin gradación** por `SD-10039` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10018 — Observaciones-Con-Su-Severidad-Y-Su-Par-De-Valores

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10007`; `RN-10009`; `US-10019`; sondas `SD-10030`, `SD-10031`, `SD-10033` |
| Setup | Trabajos con los resultados de `E-3`, `E-4` y `E-5` |
| Pasos | Given el de `E-3`, When se abre, Then la observación muestra su **severidad** y su **par declarado y derivado** sin reformatear. Given el de `E-4` —**cero observaciones**—, Then se dibuja «sin observaciones» como **línea explícita** y no como hueco. Given el de `E-5`, Then la observación de error trae **índice y campo**, y **las piezas no dibujadas no se mezclan con las observaciones** |
| Salida esperada | Tres presentaciones distintas y la separación entre observación del trabajo y condición del dibujo. Mezclarlas es **deriva mayor** por `SD-10031` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10019 — Arbol-Colapsable-Y-Navegable-Por-Teclado

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, con recorrido por teclado |
| Cubre | `CU-10007`; `US-10020`; sondas `SD-10049`, `SD-10050`, `SD-10051` |
| Setup | Un trabajo con el texto de `E-7`, abierto |
| Pasos | Given el árbol, When se abre la vista, Then **arranca colapsado**. When se recorre sólo con teclado, Then las flechas arriba y abajo se mueven entre piezas y las flechas derecha e izquierda despliegan y pliegan. Then **la escena tiene equivalente accesible**: alternativa textual compuesta **desde el resultado de dibujo**, árbol completo y enumeración de las piezas no dibujadas, **sin leerse del interior de la escena** |
| Salida esperada | Recorrido completo por teclado y equivalente accesible presente. **Que la escena quede como única vía a la información es deriva mayor** por `SD-10050` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10020 — Sincronizacion-Del-Arbol-Y-La-Escena-Por-Indice-De-Pieza

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10007`; `US-10021`; `PT-02`, cuya definición en el intake §17.2.P.8 · GeometriaFactory-Visor incluye que **el árbol y la escena se sincronicen por índice**; sondas `SD-10031`, `SD-10041` |
| Setup | Un trabajo con el texto de `E-1`, abierto |
| Pasos | Given la vista abierta, When se selecciona una pieza en el árbol, Then se resalta **la misma** en la escena, y a la inversa. Then **el índice de la pieza es el mismo** en el árbol, en la escena y en el resultado de dibujo. When se carga el mismo texto dos veces, Then la disposición es **la misma**, comparable pieza por pieza |
| Salida esperada | Selección cruzada en los dos sentidos con índices coincidentes, y disposición determinista. **Índices que dejan de coincidir es deriva mayor** por `SD-10031`; disposición que cambia entre dos cargas lo es por `SD-10041` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10021 — Ciclo-De-Vida-De-La-Instancia-Y-Diez-Recorridos-Sin-Degradar

| Campo | Valor |
| --- | --- |
| Tipo | Puerta técnica `PT-02`, ejecución medida |
| Cubre | `CU-10007`; `RT-05`; NFR de instancias no liberadas (`05` §8); sonda `SD-10042` |
| Setup | Dos trabajos abiertos alternadamente, **con los dos movimientos automáticos prendidos**, que es el peor caso declarado |
| Pasos | Given dos trabajos, When se recorre de ida y vuelta **diez** veces, Then al final quedan **0** instancias del visor vivas y **0** recursos sin liberar: geometrías, materiales y contexto gráfico. Then el descarte del componente que aloja la escena **invoca la liberación**, que `RT-05` declara **no opcional** |
| Salida esperada | Recuento en cero tras los diez recorridos. Es la puerta `PT-02`, y **una puerta que no pasa detiene la planificación de la etapa `g`**: no se arrastra como deuda |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10022 — Gobierno-De-Los-Dos-Movimientos-Desde-El-Anfitrion

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más inspección estructural |
| Cubre | `CU-10007`; `RT-13`; `RA-02`; sondas `SD-10044`, `SD-10045`, `SD-10046`, `SD-10047`, `SD-10048` |
| Setup | Un trabajo abierto; la preferencia de movimiento reducido, primero ausente y después declarada en el sistema |
| Pasos | Given la vista, When se miran los controles, Then hay **dos** movimientos independientes, tildables por separado y **los dos a la vez**. When se los cambia, Then **la disposición no se altera** en ninguna de las cuatro combinaciones. When se apaga el giro, Then las piezas **vuelven a su orientación de partida**. When se arrastra la cámara, Then los dos se detienen **sin cambiar el estado gobernado**. Given la preferencia de movimiento reducido declarada, Then los dos **arrancan destildados** y el control declara por qué. Then la inspección del código muestra que **es esta pieza la que lee la preferencia** y le manda al bundle **dos valores de verdad** |
| Salida esperada | Dos controles independientes, orientación repuesta, disposición intacta y la preferencia leída **de este lado**. Es `RA-02` sostenida desde acá: si esta pieza dejara de leerla, el bundle tendría que consultar |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10023 — Sin-Capacidad-Grafica-El-Resto-Del-Producto-Sigue-Disponible

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10007`; `RT-11`; sondas `SD-10050`, `SD-10011` |
| Setup | Navegador con la capacidad gráfica tridimensional deshabilitada |
| Pasos | Given el navegador sin capacidad gráfica, When se abre un trabajo, Then la escena **se declara no soportada** con un aviso propio y **el resto de la vista sigue disponible**: datos, texto, árbol y observaciones. When se recorre el resto del producto, Then **nada más se degrada** |
| Salida esperada | Escena no soportada, resto operable, y el equivalente accesible cumpliendo su función. La combinación sin capacidad gráfica **no es soportada para la escena y sí para todo lo demás** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Comisión y desenlace

#### TC-10024 — Entrega-De-La-Comision-Agrupada-Y-Filtrada-Sin-Borradores

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10008`; `RN-10011`; `US-10022`, `US-10023`; sondas `SD-10010`, `SD-10019` |
| Setup | Administrador con sesión; trabajos de dos alumnos en los cuatro estados |
| Pasos | Given el listado de la comisión, When se lo abre, Then está **agrupado por alumno**, **ningún trabajo en `Borrador`** aparece, y hay barra de filtros, nota de ausencia y panel de resumen. When se filtra por un alumno inexistente, Then el estado de **filtrado sin resultados** se distingue del vacío. Then **el listado no ofrece aprobar ni rechazar sin abrir el trabajo** |
| Salida esperada | Listado sin borradores, agrupado y filtrable, con el filtrado sin resultados distinguido del vacío. **Un borrador visible acá es deriva mayor** por `SD-10010` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10025 — Resolver-Con-Comentario-Opcional-Y-Retirar-Lo-Que-Ve

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, más verificación forzando la solicitud |
| Cubre | `CU-10009`; `RN-10010`, `RN-10004`; `US-10024`, `US-10025`; sondas `SD-10008`, `SD-10020`, `SD-10026`, `SD-10027` |
| Setup | Administrador con sesión; trabajos en estado `Pendiente`, `Finalizado` y `Rechazado`, y un `Borrador` ajeno |
| Pasos | Given un trabajo en estado `Pendiente` abierto como administrador, When se mira, Then el bloque de decisión está **dentro de la vista de trabajo** y **no tiene ruta propia**. When se aprueba sin comentario y se rechaza con comentario, Then los dos proceden y **se vuelve al listado con el trabajo ya actualizado**. Given un trabajo `Finalizado`, Then el bloque **no aparece**. Given el alumno, Then **nunca aparece**. When se **fuerza la solicitud** del desenlace desde un estado terminal o con el papel de alumno, Then el servicio de datos la rechaza. When se retira un trabajo de los tres estados que el administrador ve, Then los tres se retiran y el retirado **deja de figurar** |
| Salida esperada | Dos desenlaces aplicados, el bloque alojado y sin ruta propia, y los rechazos verificados **forzando**. **Una ruta a la superficie alojada es deriva mayor sin gradación** por `SD-10027`, y es el hallazgo que la validación visual expuso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10026 — Trabajo-Ajeno-E-Inexistente-Indistinguibles-Forzando-La-Solicitud

| Campo | Valor |
| --- | --- |
| Tipo | Verificación forzando la solicitud |
| Cubre | `CU-10006`, `CU-10007`, `CU-10009`; `RN-10003`, `RN-10011`; sonda `SD-10021` |
| Setup | Dos alumnos con trabajos; el administrador; un identificador que no existe |
| Pasos | Given un alumno con sesión, When **fuerza la solicitud** de un trabajo de otro alumno y la de un identificador inexistente, Then las dos respuestas son **indistinguibles**: mismo código, mismo mensaje, mismo tiempo observable. Given el administrador, When fuerza la solicitud de un **borrador** ajeno, Then recibe «no encontrado» y no un motivo distinto. When se abre cualquiera de los tres por la pantalla, Then el mensaje mostrado **también es el mismo** |
| Salida esperada | Indistinguibilidad verificada **sin pasar por la pantalla**, que es la única forma de verificarla: acá no se hace cumplir nada, se acota lo que se ofrece. **Distinguir el trabajo ajeno del inexistente es deriva mayor** por `SD-10021` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.6 Degradación y reconexión

#### TC-10027 — Estado-Degradado-Y-Reconexion-Como-Dos-Tramos-Distintos

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, con el servicio detenido y la red cortada |
| Cubre | `CU-10010`; `RT-07`; `US-10026`, `US-10027`; sondas `SD-10011`, `SD-10014` |
| Setup | Sesión iniciada con contenido en pantalla y texto escrito sin enviar |
| Pasos | Given el servicio de datos **detenido**, When se pide un listado, Then el aviso reemplaza **el contenido y no el armazón**: la navegación sigue disponible y la acción de reintentar está nombrada. Then **lo escrito se conserva**. Given la **red del navegador cortada**, When se corta el circuito, Then aparece el cartel de reconexión **estilizado con los tokens del producto**. When se restablece, Then se recupera. Given un corte que no se puede restablecer, Then el estado «sesión no restablecible» se muestra como estado propio |
| Salida esperada | Los **dos tramos** distinguidos —servicio caído y circuito caído—, el armazón conservado y lo escrito intacto. **Que el aviso reemplace el armazón o que se pierda lo escrito es deriva mayor** por `SD-10011` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10028 — Listado-Vacio-Distinguido-Del-Fallo-Por-El-Tipo-Recibido

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion |
| Cubre | `CU-10010`, `CU-10006`, `CU-10008`; `RT-07`; `US-10026`; sondas `SD-10012`, `SD-10014`, `SD-10019` |
| Setup | Un alumno sin trabajos; el servicio de datos detenido |
| Pasos | Given el alumno sin trabajos y el servicio disponible, When abre su panel, Then ve el **estado vacío explicado**. Given el servicio detenido, When abre el mismo panel, Then ve el **estado de indisponibilidad**, distinto del anterior. Then la distinción se resuelve **por el tipo recibido y no por el conteo** |
| Salida esperada | Dos estados distinguibles con la misma cantidad de filas en pantalla, cero. **Que falte la distinción entre indisponible y vacío es deriva mayor** por `SD-10014` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.7 Inspecciones estructurales y puertas

#### TC-10029 — Cero-Peticiones-Del-Navegador-Hacia-El-Servicio-De-Datos

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural |
| Cubre | `RA-01`; `RT-01`; NFR de peticiones del navegador (`05` §8); `QG-05`; sonda `SD-10043` |
| Setup | Un recorrido completo del producto, **con los dos movimientos automáticos prendidos y sostenidos** |
| Pasos | Given el recorrido completo con el panel de red abierto, When se cuentan las peticiones originadas en el navegador **hacia el servicio de datos**, Then el recuento es exactamente **0**, incluida la interacción con la escena y con los dos movimientos |
| Salida esperada | **0**, sin gradación. Es la regla que sostiene la topología entera, y una medición hecha **sin los movimientos prendidos** no cuenta como medición |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10030 — Una-Sola-Salida-Y-Cero-Bibliotecas-De-Guion-Que-Consulten

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural |
| Cubre | `RA-01`; NFR de salidas hacia el servicio de datos (`05` §8); `QG-06`; `05` §3.2 punto 3 |
| Setup | El árbol de fuentes y el manifiesto de dependencias de guion |
| Pasos | Given el árbol de fuentes, When se lo inspecciona, Then hay exactamente **1** salida hacia el servicio de datos —el cliente tipado— y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta. Then **ninguna superficie invoca al cliente tipado directamente**: entre una superficie y la salida hay siempre un servicio de aplicación de front |
| Salida esperada | Un recuento en 1 y dos en 0. La segunda propiedad es la que hizo posible la Fase B2 y la que mantiene maquetable cada superficie |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10031 — Diecisiete-Codigos-Vivos-Traducidos-Sin-Exponer-Nada

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural, más barrido de la microcopy |
| Cubre | `RA-03`; `RT-03`; NFR de mensajes que exponen (`05` §8); `QG-08`; sondas `SD-10057`, `SD-10058` |
| Setup | El traductor de condiciones, y el conjunto de los **diecisiete** códigos vivos del contrato de `GeometriaFactory-Contracts` |
| Pasos | Given los diecisiete códigos **y** el camino de **ausencia de respuesta**, When se recorre la traducción de cada uno, Then los **dieciséis** mensajes resultantes **no incluyen dirección de servicio, nombre de archivo de datos, traza ni código de error**. Then cada uno dice **qué pasó, por qué y qué hacer**. Given un barrido de toda la microcopy visible, Then `Pendiente` aparece **siempre calificado** donde conviven los dos referentes |
| Salida esperada | **17 de 17** códigos traducidos más el camino de ausencia, con **0** exposiciones. El traductor es el único lugar por el que un mensaje llega a la persona, lo que hace la propiedad verificable en un solo punto |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10032 — Cero-Invocaciones-Al-Interior-Del-Bundle

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural |
| Cubre | `RA-02`; `RT-04`; NFR de invocaciones al interior (`05` §8); `QG-09`; sonda `SD-10043` |
| Setup | El árbol de fuentes del proyecto de código |
| Pasos | Given el árbol de fuentes, When se lo inspecciona, Then las **6** funciones de la fachada son la **única** vía hacia el bundle, con **0** invocaciones a su interior y **0** accesos al elemento de dibujo fuera del anfitrión del visor. Then **invocar `establecerMovimiento` no es una violación**: es lo que el contrato le manda hacer al anfitrión |
| Salida esperada | Seis funciones como única vía y dos recuentos en cero. Es el punto de extensión declarado del producto, y perderlo significa que el motor de dibujo deja de ser reemplazable |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10033 — Cero-Trafico-De-Circuito-Durante-La-Interaccion-Con-La-Escena

| Campo | Valor |
| --- | --- |
| Tipo | Inspección estructural |
| Cubre | `RT-10`; NFR de tráfico de circuito (`05` §8); `QG-10` |
| Setup | Un trabajo abierto, con el panel de red mostrando el tráfico del circuito |
| Pasos | Given la vista de trabajo, When se rota, se acerca y se selecciona en la escena, Then el tráfico de circuito hacia el servidor es exactamente **0**. Then el texto del trabajo viaja del servidor al navegador **una sola vez por trabajo**, en la invocación de carga: ni el árbol ni la escena se vuelven a componer desde el servidor |
| Salida esperada | **0** tráfico durante la interacción y **1** sola transferencia del texto por trabajo |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10034 — Las-Cuatro-Mediciones-De-La-Puerta-PT-01

| Campo | Valor |
| --- | --- |
| Tipo | Puerta técnica `PT-01`, medición |
| Cubre | Los **cuatro** primeros NFR de `05` §8; `PA-02` de `05` §11 |
| Setup | El front publicado en el hosting público, y el servicio de datos levantado en el servidor propio |
| Pasos | Given el front publicado, When se abre la dirección pública, Then responde **200** (`PT-01.a`). When se inspecciona el transporte negociado, Then el semáforo da verde, o **amarillo aceptable** documentando la latencia percibida, o rojo (`PT-01.b`). When se navega **20 minutos** continuos, Then el proceso no recicla el circuito, y al cortar y restablecer la red la reconexión funciona (`PT-01.c`). When se pide la salud, Then devuelve **datos reales** del servidor propio (`PT-01.d`) |
| Salida esperada | Cuatro mediciones registradas. **Sólo el rojo en el transporte o la falla de estabilidad obligan a cambiar el modelo de front**; un repliegue de mayor latencia **no es motivo de rediseño**. Los umbrales **no son asunciones**: el intake §22 lo declara |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10035 — Guion-De-Demostracion-Acumulativo-Al-Cien-Por-Ciento

| Campo | Valor |
| --- | --- |
| Tipo | Paso de guion, acumulativo |
| Cubre | NFR de pasos del guion de demostración (`05` §8); `QG-04`; las **once** superficies; sondas `SD-10001` a `SD-10011`, `SD-10054` a `SD-10056`, `SD-10059` a `SD-10061` |
| Setup | El sistema construido hasta la etapa en curso, levantado desde el contenedor de desarrollo |
| Pasos | Given el guion de la etapa **y los de todas las anteriores**, When se los ejecuta en el navegador del equipo anfitrión, Then **el 100 % de los pasos pasa** antes del punto de control. Then las once superficies se recorren en ancho normal y **en ancho angosto**. Then **ningún instrumento de la maqueta** —barra de validación, panel del contrato de fachada, credencial de prueba exhibida, portada— aparece en el sistema construido. Then **ningún valor compuesto para la maqueta** figura como dato del producto |
| Salida esperada | 100 % de pasos, once superficies recorridas en las dos anchuras, y **0** instrumentos de maqueta y **0** valores de maqueta en el producto. **Un instrumento de validación en producción es deriva mayor sin gradación** por `SD-10059` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

### 3.1 `GeometriaFactory-Web`

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de verificación de este catálogo | **35**, `TC-10001` a `TC-10035` | Contar los encabezados de §2 |
| Casos de uso con al menos un caso de verificación | **10 de 10** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Restricciones transversales con verificación | **13 de 13** | Matriz §5 |
| Historias con caso de verificación | **30 de 30** | Matriz §2, columna de historias |
| NFR con caso de verificación propio | **12 de 14**; uno lo cubre la matriz de sensado y otro es una puerta del flujo de publicación | Matriz §3 |
| Reglas de negocio con verificación de lo que esta pieza hace por ellas | **16 de 16** | Matriz §4 |
| Casos que verifican **forzando la solicitud** | **6** — `TC-10001`, `TC-10005`, `TC-10007`, `TC-10015`, `TC-10025`, `TC-10026` | §2, columna de tipo |
| Inspecciones estructurales | **5** — `TC-10029` a `TC-10033` | §2.7 |
| Puertas técnicas con caso propio | **2** — `TC-10034` para `PT-01`, `TC-10021` para `PT-02`; `TC-10020` ejerce además la sincronización por índice que `PT-02` mide. **`PT-03` no tiene caso propio acá**: es propiedad del bundle y se verifica del lado de `GeometriaFactory-Visor` | §2.7 y §2.4 |
| Escenarios del intake §20 usados como dato | **8 de 8** | `TC-10012`, `TC-10013`, `TC-10020` (`E-1`); `TC-10011` (`E-2`); `TC-10013`, `TC-10018` (`E-3`); `TC-10018` (`E-4`); `TC-10014`, `TC-10018` (`E-5`); `TC-10017` (`E-6`); `TC-10017`, `TC-10019` (`E-7`); `TC-10014` (`E-8`) |
| Casos de verificación deshabilitados | **0** | Ninguna fila lo declara |

**Los ocho escenarios están, uno por uno, y ninguno se sustituye.** Este es el único proyecto de código del producto donde entran **en su forma original y completa**, como texto que la persona pega, porque es donde el alumno los pega de verdad.

### 3.2 `GeometriaFactory-Visor`

| Magnitud | Valor |
| --- | --- |
| Casos de prueba declarados | **21**, `TC-12001` a `TC-12021`, serie contigua |
| Casos de uso cubiertos | **7 de 7** |
| Funciones de la fachada ejercitadas | **6 de 6** |
| Garantías cubiertas | **7 de 7**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5 |
| Propiedades transversales cubiertas | **6 de 6**, con sus condiciones de medición |
| Códigos de condición cubiertos | **7 de 7**, en sus **8** cursos, agregados en `TC-12021` |
| Historias de usuario cubiertas | **14 de 14** |
| NFR con caso de prueba asociado | **8 de 8** |
| Puertas técnicas con caso de prueba | **2 de 2**: `PT-02` en `TC-12020` y `PT-03` en `TC-12019` |
| Escenarios del intake §20 usados como texto | **8 de 8** |
| Casos de prueba sin upstream declarado | **0** |

**Verificación de la cobertura de los ocho escenarios, uno por uno:** `E-1` en `TC-12005`, `TC-12009`, `TC-12015` y `TC-12020`; `E-2` en `TC-12006`; `E-3` y `E-4` en `TC-12006`; `E-5` en `TC-12007`; `E-6` en `TC-12007`; `E-7` en `TC-12005`, `TC-12006` y `TC-12015`; `E-8` en `TC-12007`, `TC-12008` y `TC-12011`. Ninguno se sustituye por datos sintéticos.

**Verificación de los siete códigos, uno por uno:** `CAPACIDAD_GRAFICA_AUSENTE` en `TC-12002`; `ELEMENTO_DE_DIBUJO_INVALIDO` curso **C-1** en `TC-12002` y curso **C-2** en `TC-12012`; `INSTANCIA_DESCONOCIDA` en `TC-12004`, `TC-12011` y `TC-12013`; `TEXTO_NO_LEGIBLE` en `TC-12010`; `TIPO_NO_DIBUJABLE` en `TC-12007`; `DIMENSION_NO_LEGIBLE` en `TC-12007`; `INDICE_FUERA_DE_RANGO` en `TC-12011`, en los dos casos que su enunciado cubre.

## 4. Catálogo de casos de prueba

### 4.1 `GeometriaFactory-Visor`

### 2.1 Ciclo de vida de la instancia

#### TC-12001 — Crear-Instancia-Y-Aislarla-De-Las-Demas

| Campo | Valor |
| --- | --- |
| Tipo | Integración y extremo a extremo en página |
| Cubre | `CU-12001`; garantías `G-1`, `G-3`, `G-4`, `G-7`; `US-12001`; `BT-12004`, `BT-12005`, `BT-12008` |
| Setup | Una página con dos elementos de dibujo con tamaño, en un navegador con capacidad gráfica tridimensional |
| Pasos | Given un elemento de dibujo, When se crea la instancia, Then se devuelve un **identificador** y la escena queda viva. Given dos instancias en la misma página, When se opera sobre una, Then la otra no cambia: **no comparten escena, ni selección, ni disposición** (`G-4`). Given una instancia creada, When se cuentan las peticiones que origina el archivo de guion, Then son exactamente **cero** (`G-1`) |
| Salida esperada | Un identificador por instancia, aislamiento verificado y un recuento en 0 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12002 — Sin-Capacidad-Grafica-Y-Sin-Elemento-Valido-No-Hay-Instancia

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12001`; garantía `G-7`; `US-12003`; códigos `CAPACIDAD_GRAFICA_AUSENTE` y `ELEMENTO_DE_DIBUJO_INVALIDO` **curso C-1**; entradas `E-VIS-01` y `E-VIS-02` de `03` |
| Setup | Un navegador sin capacidad gráfica tridimensional; y una página con un elemento de dibujo de **tamaño nulo** |
| Pasos | Given el navegador sin capacidad gráfica, When se invoca la creación, Then se informa `CAPACIDAD_GRAFICA_AUSENTE` y **no se devuelve identificador**. Given el elemento de tamaño nulo, When se invoca la creación, Then se informa `ELEMENTO_DE_DIBUJO_INVALIDO` en su curso **C-1** y **no se crea instancia** |
| Salida esperada | Dos condiciones informadas por su código y **cero** instancias creadas. Es lo contrario del fallo silencioso: la fachada dice por qué no pudo |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12003 — El-Estado-Inicial-De-Los-Dos-Movimientos-Llega-Por-Parametro

| Campo | Valor |
| --- | --- |
| Tipo | Integración e inspección del artefacto generado |
| Cubre | `CU-12001`, `CU-12007`; garantías `G-2`, `G-3`; `US-12002`; `BT-12011` |
| Setup | Un conductor de navegador que declara **preferencia de movimiento reducido** del sistema |
| Pasos | Given dos valores de verdad pasados al crear la instancia, When la escena arranca, Then cada movimiento arranca en el estado indicado. Given opciones **ausentes o parciales**, Then los dos arrancan **apagados**. Given la instancia creada, When se inspecciona el bundle, Then **no consulta la preferencia de movimiento reducido del sistema** (`G-3`) y **no escribe ninguna clave** para recordar la elección (`G-2`) |
| Salida esperada | Cuatro combinaciones de arranque correctas, arranque apagado ante opciones ausentes, y dos recuentos en 0. **Que la fachada no consulte la preferencia es lo que permite que la prueba prenda los movimientos aunque el entorno la declare**, y sin eso las mediciones de ausencia medirían el caso fácil |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12004 — Destruir-Libera-Los-Recursos-Y-Corta-El-Bucle

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12005`; garantías `G-4`, `G-7`; `US-12011`; NFR «Liberación de recursos»; `BT-12005`, `BT-12012`, `BT-12014` |
| Setup | Una página con dos trabajos entre los que se puede ir y volver, y el texto del escenario `E-1` |
| Pasos | Given una instancia viva, When se la destruye, Then libera sus recursos gráficos y **corta su bucle de dibujo**. Given **diez recorridos de ida y vuelta** entre trabajos **con los dos movimientos prendidos**, When se cuentan los recursos gráficos vivos al final, Then no hay degradación. Given una instancia ya destruida, When se la vuelve a usar por cualquiera de las cinco funciones que exigen identificador, Then se informa `INSTANCIA_DESCONOCIDA` |
| Salida esperada | Bucle cortado, sin recursos acumulados tras diez recorridos, y el identificador invalidado. **Los movimientos prendidos son la condición de medición declarada**: un bucle que sobreviviera a la destrucción es exactamente la degradación que hay que descartar, y con los movimientos apagados no se ejercitaría |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Carga del texto y dibujo

#### TC-12005 — Dibujar-Las-Tres-Figuras-De-E1-Y-Los-Seis-Tipos-De-E7

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12002`; `US-12004`; `PT-02` en su tramo de dibujo; `BT-12007`, `BT-12008`, `BT-12009`, `BT-12014` |
| Setup | Los textos de los escenarios `E-1` y `E-7` del intake §20, transcriptos sin modificación |
| Pasos | Given el texto de `E-1`, When se lo carga, Then se dibujan sus **tres** piezas, **ortoedro incluido**. Given el texto de `E-7`, Then se dibujan los **seis** tipos dibujables: tres volumétricos —`Cilindro`, `Cubo`, `Ortoedro`— y tres planos —`Rectangulo`, `Cuadrado`, `Circulo`—. Given el ortoedro de `E-7`, Then se dibuja con **ancho 6, profundidad 4 y altura 8**, coherente con el volumen declarado de 192.00 |
| Salida esperada | Tres piezas en `E-1` y seis en `E-7`, con las dimensiones del ortoedro verificadas. **El ortoedro de `E-1` es el caso insignia**: hoy, en el visualizador previo, ningún ortoedro generado por la aplicación se dibuja |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12006 — Leer-Las-Dimensiones-Con-Las-Variantes-De-Clave-Del-Emisor

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-12002`; `US-12005`; `BT-12007` |
| Setup | Los textos de los escenarios `E-2` —clave `Tapas`—, `E-7` —clave `Bases`—, `E-3` —caras `Cuadrado`— y `E-4` —caras `Rectangulo`— |
| Pasos | Given el ortoedro de `E-2` con la clave **`Tapas`**, When se lo lee, Then **el ortoedro se dibuja**. Given el de `E-7` con la clave **`Bases`**, Then se dibuja igual: las dos claves se aceptan como sinónimos. Given los cubos de `E-3` y de `E-4`, cuyas caras llevan `Cuadrado` y `Rectangulo` respectivamente, Then los dos se dibujan igual, porque **el campo que se usa es `Largo`** |
| Salida esperada | Cuatro lecturas correctas. **La fachada no emite ninguna observación** sobre el área declarada de `E-3`: aceptar variantes de clave es **leer una dimensión y no validar un trabajo**, y son dos responsabilidades distintas sobre el mismo texto |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12007 — Ninguna-Pieza-Desaparece-Sin-Quedar-Enumerada

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12002`; garantía `G-5`; `US-12006`; NFR «Ausencia de fallo silencioso»; códigos `TIPO_NO_DIBUJABLE` y `DIMENSION_NO_LEGIBLE`; entradas `E-VIS-09` y `E-VIS-10` de `03` |
| Setup | Los textos de los escenarios `E-5`, `E-8` y `E-6` del intake §20 |
| Pasos | Given el texto de `E-5`, cuya figura del índice 1 declara un tipo fuera de los seis dibujables, When se lo carga, Then esa pieza **no se dibuja y queda enumerada** con su índice y `TIPO_NO_DIBUJABLE`, y **la del índice 0 se dibuja igual**. Given el texto de `E-8`, Then el ortoedro del índice 0 **se dibuja**, la pieza del índice 1 **no**, y el resultado de dibujo la reporta con **índice 1**, código **`DIMENSION_NO_LEGIBLE`** y el campo `Largo`. Given el texto de `E-6`, cuya figura declara `"Largo": 0.00`, Then **la figura se dibuja**: el cero es una dimensión legible y **no produce `DIMENSION_NO_LEGIBLE`** |
| Salida esperada | **100 %** de las piezas no dibujadas enumeradas con índice y código, y **0** piezas sin registro. Y una comprobación negativa: la de `E-6` **no** aparece entre las no dibujadas. **El código de `E-8` es `DIMENSION_NO_LEGIBLE` y no una condición de texto inválido**: el texto es sintácticamente válido y lo que falla es la lectura de un valor. Confundir los dos es el error que ese escenario detecta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12008 — Devolver-La-Estructura-Del-Texto-Para-El-Arbol

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-12002`; `US-12007`; `BT-12004`, `BT-12007` |
| Setup | El texto del escenario `E-8` del intake §20 |
| Pasos | Given ese texto, When se lo carga, Then el resultado de dibujo trae la **estructura del texto recibido**, con **las dos piezas**, incluida la que no se dibujó. When se inspecciona la fachada, Then **no dibuja el árbol**: devuelve la estructura y la presentación es del componente anfitrión |
| Salida esperada | Estructura con las dos piezas y ninguna presentación de árbol dentro del bundle. Es lo que hace que **se lea lo que el alumno escribió y no lo que la escena logró representar** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12009 — La-Disposicion-Se-Deriva-Del-Indice-Y-Es-Determinista

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12002`; garantía `G-6`; `US-12008`; NFR «Disposición determinista»; `BT-12010` |
| Setup | El texto del escenario `E-1`, cargado dos veces |
| Pasos | Given dos procesados del mismo texto, When se comparan pieza por pieza, Then producen la **misma disposición**. Given la comparación, When se define qué se compara, Then **se compara posición y no orientación**. Given los dos movimientos en cualquiera de sus cuatro combinaciones, When se repite la comparación, Then la propiedad se sostiene: **ningún movimiento altera la disposición** |
| Salida esperada | Disposiciones idénticas en las cuatro combinaciones, comparando posición. El determinismo comprometido es de la **posición derivada del índice**, no de la orientación en un instante |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12010 — Un-Texto-Que-No-Da-Piezas-Deja-La-Instancia-Viva-Y-Vacia

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-12002`; garantía `G-7`; código `TEXTO_NO_LEGIBLE`; entrada `E-VIS-08` de `03` |
| Setup | Una instancia con una escena ya dibujada a partir de `E-1` |
| Pasos | Given un texto del que no se puede obtener un conjunto de piezas, When se lo carga, Then se informa `TEXTO_NO_LEGIBLE`, **la instancia queda viva y vacía**: se libera lo dibujado antes y no se dibuja nada nuevo |
| Salida esperada | La condición informada, la instancia viva y la escena vacía. **La instancia no queda en estado indeterminado** (`G-7`) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Selección, ajuste y movimiento

#### TC-12011 — Resaltar-En-Exclusiva-La-Pieza-Del-Indice-Indicado

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12003`; garantías `G-4`, `G-7`; `US-12009`; `PT-02` en su tramo de sincronización; código `INDICE_FUERA_DE_RANGO`; entradas `E-VIS-11` y `E-VIS-12` de `03` |
| Setup | Una escena dibujada con el texto del escenario `E-8`, que tiene una pieza dibujada y una no dibujada |
| Pasos | Given la escena, When se selecciona la pieza del índice 0, Then queda resaltada **en exclusiva**: ninguna otra lo está. Given un índice que no está en el conjunto raíz, Then `INDICE_FUERA_DE_RANGO` y **la selección vigente se conserva**. Given el índice **1**, que el resultado enumera como **no dibujada**, Then también `INDICE_FUERA_DE_RANGO`: figura en el resultado pero **no tiene malla que resaltar**. Given un identificador que no corresponde a una instancia viva, Then `INSTANCIA_DESCONOCIDA` |
| Salida esperada | Un resaltado exclusivo y tres condiciones, con la selección conservada en las tres. **Los dos casos que `INDICE_FUERA_DE_RANGO` cubre derivan del enunciado literal del contrato y no de una reinterpretación; son dos casos de un mismo curso, no dos cursos** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12012 — Ajustar-La-Escena-Y-Sobrevivir-A-Un-Elemento-Sin-Tamano

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12004`; garantía `G-7`; `US-12010`; código `ELEMENTO_DE_DIBUJO_INVALIDO` **curso C-2**; entrada `E-VIS-07` de `03` |
| Setup | Una instancia viva con su escena, sobre un elemento de dibujo cuyo tamaño se puede cambiar |
| Pasos | Given un cambio de tamaño del elemento, When se invoca el ajuste, Then la relación de aspecto se recalcula. Given el elemento **ocultado o desmontado**, When se invoca el ajuste, Then se informa `ELEMENTO_DE_DIBUJO_INVALIDO` en su curso **C-2**, **la instancia sigue viva** con su escena y su selección intactas, y no se recalcula nada. Given el elemento devuelto a un tamaño válido, When se invoca de nuevo, Then el ajuste procede |
| Salida esperada | Un ajuste, una condición sin pérdida de instancia y un ajuste posterior exitoso. **Es el mismo código que `TC-12002` con otro curso**, y la diferencia está en el efecto sobre la instancia: allá no se crea, acá sigue viva |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12013 — Gobernar-Los-Dos-Movimientos-En-Vivo-Sin-Reconstruir

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12007`; garantía `G-7`; `US-12012`; `BT-12011` |
| Setup | Una instancia viva con el texto de `E-1` cargado y una pieza seleccionada |
| Pasos | Given la instancia, When se prende o se apaga cada movimiento por separado, Then el cambio surte efecto **sin reconstruir la instancia**: la disposición, la selección vigente, el encuadre, el resultado de dibujo y el identificador **quedan como estaban**, y el movimiento **no nombrado conserva su estado**. When se invoca dos veces con el mismo valor, Then el resultado es el mismo. When se apaga el giro de las figuras, Then las piezas **vuelven a su orientación de partida**. When se carga otro texto, Then **el estado de los movimientos sobrevive**. When se invoca con un identificador inválido, Then `INSTANCIA_DESCONOCIDA` y **ningún movimiento cambia** |
| Salida esperada | Cambio en vivo con selección conservada, idempotencia, reposición de orientación, supervivencia a la carga de otro texto, y la condición informada sin efecto. **La sexta función no emite ninguna condición propia**: la lista sigue cerrada en siete |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12014 — Los-Movimientos-Se-Detienen-Sin-Cambiar-El-Estado-Gobernado

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12007`; `US-12013`; `BT-12011` |
| Setup | Una instancia viva con los dos movimientos prendidos |
| Pasos | Given los dos movimientos prendidos, When la persona arrastra la cámara, Then los dos **se detienen** mientras dura el arrastre y **el movimiento no le pelea el control a quien lo tomó**. When la superficie de dibujo deja de estar visible, Then los dos se detienen. En los dos casos, When se consulta el estado gobernado, Then **no cambió**: el control del anfitrión no se apaga solo |
| Salida esperada | Dos detenciones y el estado gobernado intacto en las dos. La detención **no es una condición**: no emite código |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Propiedades transversales y puertas técnicas

#### TC-12015 — Ejercitar-Las-Seis-Funciones-Sin-Backend

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | `CU-12006`; las **seis** propiedades transversales de `02` §6; `US-12014`; NFR «Se ejercita sin backend»; sample **S-1**; `BT-12015` |
| Setup | La página integradora del sample S-1, con el bundle cargado, un área donde se pega el texto y una superficie de dibujo, y **0 servicios del backend disponibles** |
| Pasos | Given la página y el texto de `E-1` pegado a mano, When se recorren las **seis** funciones —crear, cargar, seleccionar, ajustar, gobernar el movimiento y destruir—, Then el recorrido cierra entero **sin ninguna pieza del backend**. When se repite con el texto de `E-7`, Then se dibujan los seis tipos. When se cuentan las peticiones de la pestaña de red durante todo el recorrido, Then son **cero** |
| Salida esperada | Recorrido completo de las seis funciones con cero servicios disponibles y cero peticiones. **Es la propiedad que el intake §17.2.P.6 · GeometriaFactory-Visor y `RT §8.3` exigen no perder**, y es el sample que demuestra el punto de extensión |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12016 — Cero-Red-Con-Los-Dos-Movimientos-Prendidos

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | Garantía `G-1`; propiedad transversal «Cero red»; NFR homónimo; `RA-01` y `RA-02` del intake §14; `QG-04` |
| Setup | Una instancia viva, en un conductor que declara **preferencia de movimiento reducido** del sistema, con los dos movimientos **prendidos explícitamente** |
| Pasos | Given los dos movimientos prendidos y **sostenidos el tiempo suficiente para que el bucle de dibujo corra**, When se cuentan las peticiones originadas por el archivo de guion en la pestaña de red, Then son exactamente **0**. Given los gestos de rotar y acercar con el mouse, When se repite el conteo, Then sigue siendo **0** |
| Salida esperada | Umbral **exactamente 0**, medido en su **peor caso**. **La condición de medición es vinculante**: con los movimientos apagados la prueba quedaría en verde sin haber ejercitado nunca el bucle, que es el caso donde una petición se colaría |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12017 — Cero-Persistencia

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | Garantía `G-2`; propiedad transversal «Cero persistencia»; NFR homónimo; `QG-05` |
| Setup | Una instancia viva, con los movimientos prendidos y apagados alternadamente |
| Pasos | Given cualquier estado de los movimientos, When se cuentan las claves escritas en el almacenamiento del navegador por la fachada, Then son **0**. Given la página recargada, When se consulta el estado de los movimientos, Then **la preferencia no se repone**: la fachada no la guardó. Given el recuento, When se excluyen las claves ajenas, Then la exclusión se hace **por espacio de nombres declarado y no por prefijo** |
| Salida esperada | Recuento en 0 y preferencia no repuesta. **La preferencia es del componente anfitrión**, que es quien la conserva |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12018 — La-Superficie-Del-Bundle-Son-Seis-Funciones-Y-Nada-Mas

| Campo | Valor |
| --- | --- |
| Tipo | Inspección del artefacto generado |
| Cubre | NFR «Superficie pública del bundle»; garantía `G-1` en su tramo de inspección; `QG-04`, `QG-06`; `BT-12016` |
| Setup | El bundle generado y el árbol de fuentes |
| Pasos | Given el bundle, When se inspecciona lo que expone, Then hay exactamente **6** funciones, bajo **1** nombre propio en el objeto global del navegador y **0** identificadores globales sueltos. Given el código fuente **y el bundle generado**, When se buscan las tres formas de petición de red, Then hay **0** ocurrencias en los dos |
| Salida esperada | Cuatro recuentos: 6, 1, 0 y 0. **La inspección se hace sobre el bundle generado y no sólo sobre la fuente**: una dependencia que hiciera una petición por dentro no aparecería en la fuente |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12019 — Puerta-PT-03-El-Motor-Queda-Dentro-Del-Bundle

| Campo | Valor |
| --- | --- |
| Tipo | Inspección del artefacto generado y extremo a extremo en página |
| Cubre | Puerta técnica **`PT-03`** del intake §15 y §17.2.P.8 · GeometriaFactory-Visor; NFR «Dependencias traídas de una red de distribución externa»; `QG-12002`; `BT-12013` |
| Setup | El bundle generado y una página abierta **sin acceso a redes de distribución externas** |
| Pasos | Given el bundle, When se lo inspecciona, Then el motor de dibujo tridimensional **está dentro**. Given la página sin acceso a redes externas, When se la abre y se ejerce la fachada, Then **funciona**: hay exactamente **0** dependencias traídas de una red externa en tiempo de ejecución |
| Salida esperada | El motor dentro y el recuento en 0. **Una puerta que no pasa detiene la planificación de la etapa `g`** y no se arrastra como deuda |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12020 — Puerta-PT-02-El-Bundle-En-Una-Pagina-Del-Anfitrion

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | Puerta técnica **`PT-02`** del intake §15 y §17.2.P.8 · GeometriaFactory-Visor; `US-12001`, `US-12004`, `US-12009`, `US-12011`; `QG-12003`; `BT-12014` |
| Setup | Una página del componente anfitrión con el bundle cargado, y el texto del escenario `E-1` |
| Pasos | Given la página del anfitrión, When se carga el bundle, Then carga. When se crea la instancia, Then **arma la escena**. When se carga el texto de `E-1`, Then **dibuja las tres figuras, incluido el ortoedro**. When se hacen **diez recorridos de ida y vuelta** entre trabajos **con los dos movimientos prendidos**, Then **no degrada**. When se selecciona una pieza desde el árbol y desde la escena, Then **los dos se sincronizan por índice** |
| Salida esperada | Los **cinco** tramos que la puerta exige, medidos juntos. La sincronización por índice es la que dejó de ser diferible cuando el Product Owner promovió `F-13` a `Must Have` en el intake **1.19**, con el fundamento de que una capacidad citada por una puerta no puede ser de prioridad menor |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12021 — Los-Codigos-Son-Siete-Y-Ninguno-Nace-Aguas-Abajo

| Campo | Valor |
| --- | --- |
| Tipo | Inspección del artefacto generado |
| Cubre | Los **siete** códigos de `Definicion-Contrato-De-Fachada.md` §6; `QG-08`; `BT-12006`; `05` §9, sexto riesgo |
| Setup | El conjunto de códigos que el bundle puede informar, y §6 del contrato de fachada como fuente única |
| Pasos | Given los dos conjuntos, When se los compara **en las dos direcciones**, Then el bundle informa exactamente los **siete** códigos del contrato y **ninguno más**. Given `INSTANCIA_DESCONOCIDA`, When se lo provoca desde cada función que exige identificador, Then aparece en **cinco** funciones y **sigue siendo un solo código**. Given `ELEMENTO_DE_DIBUJO_INVALIDO`, Then se presenta en sus **dos cursos** y **sigue siendo un solo código** |
| Salida esperada | 7 de 7 cubiertos, 0 acuñados aguas abajo, y la distinción entre código y curso verificada. **El catálogo de `03` puede crecer sin que crezca el conjunto de códigos** —hoy tiene **trece** entradas sobre siete códigos—, y esa distinción es la que este caso de prueba protege |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **2 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
