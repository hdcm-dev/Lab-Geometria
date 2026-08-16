# Casos de prueba referenciales — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** los **siete** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2, §4, §5 y §6; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Visor/Especificacion-Funcional.md) 1.2 §6; las **catorce** historias de [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Visor/Product-Backlog.md) §3; los **ocho** NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) §8; [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/DX-Error-Messages.md) §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.7.P.8, §18, §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Guia-Testing-Extensibilidad.md`](../../Guia-Testing-Extensibilidad.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de prueba](#2-catálogo-de-casos-de-prueba)
  - [2.1 Ciclo de vida de la instancia](#21-ciclo-de-vida-de-la-instancia)
  - [2.2 Carga del texto y dibujo](#22-carga-del-texto-y-dibujo)
  - [2.3 Selección, ajuste y movimiento](#23-selección-ajuste-y-movimiento)
  - [2.4 Propiedades transversales y puertas técnicas](#24-propiedades-transversales-y-puertas-técnicas)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara los ocho campos de `Rules-Calidad-Y-Pruebas.md` §4.6. **Todas las salidas observadas dicen «Sin ejecutar» y todos los estados dicen `Pendiente`**: el bundle no está construido.

**Vocabulario propio de este catálogo**, declarado acá la primera vez que aparece:

- **Nivel**: la posición en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario, integración, extremo a extremo en página, o inspección del artefacto generado.
- **Condición de medición**: el estado en que hay que poner la escena para que la medición valga. Para cuatro de las **seis** propiedades transversales, `02` §6 la declara, y es **vinculante**.
- **Umbral cero**: la forma de aserción de una propiedad que es una **ausencia**. Un umbral cero sin condición de medición es un caso de prueba mal escrito ([`Estrategia-Testing.md`](Estrategia-Testing.md) §4).
- **Recorrido de ida y vuelta**: pasar de un trabajo a otro y volver, que es lo que `PT-02` cuenta diez veces. Se escribe siempre calificado, porque «recorrido» tiene un segundo referente en esta cadena.

## 2. Catálogo de casos de prueba

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
| Salida esperada | Recorrido completo de las seis funciones con cero servicios disponibles y cero peticiones. **Es la propiedad que el intake §17.7.P.6 y `RT §8.3` exigen no perder**, y es el sample que demuestra el punto de extensión |
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
| Cubre | Puerta técnica **`PT-03`** del intake §15 y §17.7.P.8; NFR «Dependencias traídas de una red de distribución externa»; `QG-02`; `BT-12013` |
| Setup | El bundle generado y una página abierta **sin acceso a redes de distribución externas** |
| Pasos | Given el bundle, When se lo inspecciona, Then el motor de dibujo tridimensional **está dentro**. Given la página sin acceso a redes externas, When se la abre y se ejerce la fachada, Then **funciona**: hay exactamente **0** dependencias traídas de una red externa en tiempo de ejecución |
| Salida esperada | El motor dentro y el recuento en 0. **Una puerta que no pasa detiene la planificación de la etapa `g`** y no se arrastra como deuda |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-12020 — Puerta-PT-02-El-Bundle-En-Una-Pagina-Del-Anfitrion

| Campo | Valor |
| --- | --- |
| Tipo | Extremo a extremo en página |
| Cubre | Puerta técnica **`PT-02`** del intake §15 y §17.7.P.8; `US-12001`, `US-12004`, `US-12009`, `US-12011`; `QG-03`; `BT-12014` |
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

## 3. Recuento y verificación

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

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veintiún** casos de prueba referenciales, `TC-12001` a `TC-12021`, cada uno con tipo, upstream por identificador, setup, pasos en Given-When-Then, salida esperada, salida observada y estado. Cubren los **siete** casos de uso, las **seis** funciones, las **siete** garantías, las **seis** propiedades transversales con sus condiciones de medición vinculantes, los **siete** códigos en sus **ocho** cursos, las **catorce** historias, los **ocho** NFR y las **dos** puertas técnicas `PT-02` y `PT-03`, cada una con su caso de prueba propio. Los **ocho** escenarios del intake §20 se usan **como texto**, porque este proyecto de código sí recibe el texto, y se verifica su cobertura uno por uno. Todas las salidas observadas dicen «Sin ejecutar» y todos los estados `Pendiente`. |
