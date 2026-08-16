# Casos de prueba referenciales — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** los **diez** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) y las **trece** restricciones transversales de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6; las **treinta** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); las **once** superficies y la línea de base de [`../03-UX-UI-DX/`](../03-UX-UI-DX/); los **catorce** NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8; las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de verificación](#2-catálogo-de-casos-de-verificación)
  - [2.1 Acceso, identidad y credencial](#21-acceso-identidad-y-credencial)
  - [2.2 Cuentas de la comisión](#22-cuentas-de-la-comisión)
  - [2.3 Trabajo, envío e interpretación](#23-trabajo-envío-e-interpretación)
  - [2.4 Vista de trabajo y escena](#24-vista-de-trabajo-y-escena)
  - [2.5 Comisión y desenlace](#25-comisión-y-desenlace)
  - [2.6 Degradación y reconexión](#26-degradación-y-reconexión)
  - [2.7 Inspecciones estructurales y puertas](#27-inspecciones-estructurales-y-puertas)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: la maqueta está aprobada y validada, pero el sistema arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Paso de guion**: una acción de la persona en el navegador, con su resultado observable, ejecutada de forma acumulativa ([`Estrategia-Testing.md`](Estrategia-Testing.md) §1).
- **Inspección estructural**: la que comprueba una propiedad del árbol de fuentes o del tráfico observado, con umbral cero.
- **Verificación forzando la solicitud**: la que comprueba una acotación **sin pasar por la pantalla**. Es obligatoria porque esta pieza **no hace cumplir reglas** (`02` §5).
- **Sonda**: una fila `SD-XX` de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), con su umbral de deriva propio. Un caso de prueba **cita** sondas; no las redefine.
- **Superficie**: una de las once de la categoría 03, con su nombre canónico.

**Un caso de verificación no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten superficie, setup y forma de observación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

## 2. Catálogo de casos de verificación

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
| Cubre | `CU-10007`; `US-10021`; `PT-02`, cuya definición en el intake §17.7.P.8 incluye que **el árbol y la escena se sincronicen por índice**; sondas `SD-10031`, `SD-10041` |
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

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-03`.** El campo «Cubre» de `TC-10020` atribuía a `PT-03` la sincronización del árbol y la escena por índice, que el intake §17.7.P.8 declara parte de **`PT-02`**; y el recuento de §3 daba `PT-03` por ejercido en `TC-10020` y `TC-10021`. Corregidos los dos: `PT-03` —el motor dentro del bundle y la página sin acceso a CDN— **no tiene caso propio acá** y se verifica del lado de `GeometriaFactory-Visor`. **Ningún caso de prueba, paso ni salida esperada cambia**, y los **35** casos siguen siendo 35. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y cinco** casos de verificación, `TC-10001` a `TC-10035`, repartidos en siete grupos, cada uno con sus ocho campos y con su upstream explícito, incluidas las sondas `SD-XX` de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que cada uno ejerce **sin redefinir su umbral**. Incluye **seis** casos que verifican **forzando la solicitud sin pasar por la pantalla**, que es obligatorio porque esta pieza no hace cumplir reglas; **cinco** inspecciones estructurales con umbral cero para `RA-01`, `RA-02` y `RA-03`; y los casos de las puertas técnicas `PT-01`, `PT-02` y `PT-03`. Todos los estados dicen `Pendiente` y todas las salidas observadas dicen «Sin ejecutar». Los **ocho** escenarios del intake §20 entran **en su forma original y completa**, sin sustituirse por datos sintéticos. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **4**. Sube minor. |
