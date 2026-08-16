# Matriz de sensado de deriva — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Matriz-Sensado-Deriva.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08); alta de las sondas `VER-XX` por Developer Advocate / Sample Engineer Senior (AG-10)
**Variante:** Calidad y pruebas
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2, §4, §5.3, §5.4, §5.5 y §6; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Visor/Especificacion-Funcional.md) 1.2 §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §10.2; [`../03-UX-UI-DX/README.md`](../../../03-UX-UI-DX/_fusion/Visor/README.md) §4; [`../../GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../Matriz-Sensado-Deriva.md) 1.2; `Deriva-Rules.md` §2.3, §3 y §4
**Trazabilidad upstream de las sondas `VER-XX`:** [`../10-Examples/README.md`](../../../10-Examples/_fusion/Visor/README.md) 1.1 §3 y los tres contratos de verificación de [`../10-Examples/ejemplo-01-basico.md`](../../../10-Examples/ejemplo-01-basico.md), [`../10-Examples/ejemplo-02-intermedio.md`](../../../10-Examples/ejemplo-02-intermedio.md) y [`../10-Examples/ejemplo-03-avanzado.md`](../../../10-Examples/ejemplo-03-avanzado.md), los tres 1.0
**Trazabilidad downstream:** [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 y [`Definition-Of-Done.md`](Definition-Of-Done.md) §1.3, que exigen su actualización al cerrar cada momento

---

## Tabla de contenido

- [1. Qué es esta matriz y por qué existe una acá](#1-qué-es-esta-matriz-y-por-qué-existe-una-acá)
- [2. Contra qué se sensa, si este proyecto de código no tuvo maqueta propia](#2-contra-qué-se-sensa-si-este-proyecto-de-código-no-tuvo-maqueta-propia)
- [3. La matriz](#3-la-matriz)
- [4. Correspondencia con la matriz de `GeometriaFactory-Web`](#4-correspondencia-con-la-matriz-de-geometriafactory-web)
- [5. Umbrales de deriva aplicados](#5-umbrales-de-deriva-aplicados)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué es esta matriz y por qué existe una acá

Convierte el contrato aprobado de la fachada en una lista de comprobaciones que se pueden correr en cualquier momento de la codificación, para responder si lo construido sigue siendo lo acordado.

**Por qué existe una matriz para este proyecto de código.** `requiere_maqueta` es **true** en `GeometriaFactory-Visor` (`PRODUCT-MANIFEST` §5), de modo que `Rules-Calidad-Y-Pruebas.md` §2.1 la declara obligatoria. La emitió AG-03M al cerrar la Fase B2 en los proyectos de código con maqueta propia; **acá la abre AG-08 en la Fase E**, por el motivo que §2 desarrolla.

**Ninguna fila afirma nada sobre el estado del sistema construido.** Todas declaran qué tendría que ser cierto. Todas nacen en `Sin verificar` y sin fecha, porque el bundle no está construido.

Los momentos en que esta matriz se mueve, de `Deriva-Rules.md` §4:

| Momento | Quién | Qué pasa |
| --- | --- | --- |
| Cierre de la Fase B2 | AG-03M | **No emitió matriz para este proyecto de código.** La validación de la fachada se integró en la maqueta de `GeometriaFactory-Web`, por decisión del Product Owner |
| Cierre de la Fase E | AG-08 | **Abrió este documento** con doce filas, `SD-12001` a `SD-12012`, todas en `Sin verificar` y con su método de verificación resuelto |
| Cierre de la fase que genera la categoría 10 | AG-10 | **Hecho el 2026-08-11.** Alta de una fila `VER-XX` por cada contrato de verificación de las tres partes del sample **S-1**: `SD-12013`, `SD-12014` y `SD-12015`, todas en `Sin verificar`, con el comando del contrato como método |
| Cierre de cada momento del producto | La única persona del equipo | Verificación de las filas que el momento toca; estado y fecha actualizados; derivas mayores escaladas |

## 2. Contra qué se sensa, si este proyecto de código no tuvo maqueta propia

Hay que decirlo con precisión, porque de otro modo esta matriz parecería sensar contra una línea de base que no existe.

**`GeometriaFactory-Visor` ejecutó su Fase B2 y quedó aprobada, pero no tuvo maqueta propia.** El `PRODUCT-MANIFEST` §5 lo declara: hubo **una sola maqueta**, la de `GeometriaFactory-Web`, y la validación de la fachada se integró en ella **por decisión del Product Owner**, porque la fachada no dibuja superficie propia y lo único observable de ella es la escena embebida en su anfitrión. En consecuencia, los tres artefactos de línea de base —`Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md`— viven en la categoría 03 de `GeometriaFactory-Web` y **no se duplicaron** acá, como declara [`../03-UX-UI-DX/README.md`](../../../03-UX-UI-DX/_fusion/Visor/README.md) §4.

**De ahí se sigue qué sensa cada matriz, y por qué no se pisan:**

| Matriz | Contra qué sensa | Qué mira |
| --- | --- | --- |
| La de [`GeometriaFactory-Web`](../../Matriz-Sensado-Deriva.md) | Los identificadores `SUP-XX`, `CMP-XX`, `EST-XX`, `NAV-XX` y `DM-XX` de su línea de base visual y su contrato de datos | Si lo construido **se parece a lo que el humano aprobó mirando**, incluida la escena embebida y sus controles |
| **Ésta** | Los elementos del **contrato de la fachada** —las seis funciones, las siete garantías, los siete códigos y las seis propiedades transversales— | Si el bundle **sigue haciendo lo que su contrato dice**, aunque nadie lo mire |

**Es exactamente la distinción de `Deriva-Rules.md` §2**: las sondas de maqueta miden el parecido con lo aprobado; las de contrato y comportamiento miden que el sistema siga haciendo lo que la especificación dice. Un proyecto de código sin interfaz visual propia **también tiene deriva que sensar**, y este documento es su instrumento.

**Ninguna fila de esta matriz cita un identificador de línea de base visual.** Las que sensan un elemento que la maqueta sí validó lo declaran en §4, con la fila de la matriz de Web que lo mira desde el otro lado.

## 3. La matriz

| ID | Elemento del contrato | Afirmación a verificar | Método de verificación | Evidencia esperada | Umbral de deriva | Estado | Última verificación |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `SD-12001` | Las **seis** funciones de `Definicion-Contrato-De-Fachada.md` §4 | La superficie expone exactamente **6** funciones, con los nombres que el intake §17.2.P.3 · GeometriaFactory-Visor fija, bajo **1** nombre propio en el objeto global y **0** identificadores globales sueltos | Test automatizado: `TC-12018`, inspección del **bundle generado** | `[EV-XX \| ejecucion \| inspección del bundle generado \| recuento de funciones e identificadores globales \| fecha]` | **Menor**: cambia un nombre interno no expuesto. **Mayor**: falta una función, aparece una séptima, cambia un nombre expuesto o aparece un identificador global suelto | `Sin verificar` | — |
| `SD-12002` | Garantía `G-1` · Cero red | El archivo de guion origina exactamente **0** peticiones, medidas **con los dos movimientos prendidos y sostenidos** y durante los gestos de rotar y acercar; y hay **0** ocurrencias de las tres formas de petición en el código fuente **y en el bundle generado** | Test automatizado: `TC-12016` y `TC-12018` | `[EV-XX \| ejecucion \| conteo de peticiones en la pestaña de red con los dos movimientos prendidos \| recuento \| fecha]` | **Mayor, sin gradación.** El umbral es exactamente 0. Una medición hecha **sin la condición** no cuenta como medición | `Sin verificar` | — |
| `SD-12003` | Garantía `G-2` · Cero persistencia | **0** claves escritas en el almacenamiento del navegador por la fachada, con cualquier estado de los movimientos, y recargar la página **no repone** la preferencia. La exclusión de claves ajenas se hace **por espacio de nombres declarado y no por prefijo** | Test automatizado: `TC-12017` | `[EV-XX \| ejecucion \| recuento de claves del almacenamiento \| umbral 0 \| fecha]` | **Mayor, sin gradación** | `Sin verificar` | — |
| `SD-12004` | Garantía `G-3` · Sin configuración propia | Todo lo que la instancia necesita llega por parámetro; la fachada **no consulta la preferencia de movimiento reducido del sistema** y **no conserva la elección** | Test automatizado: `TC-12003`; más inspección del bundle | `[EV-XX \| ejecucion \| inspección del bundle generado \| ausencia de consulta de preferencia \| fecha]` | **Mayor**: la fachada lee configuración por su cuenta. Es lo que hace que la prueba pueda prender los movimientos aunque el entorno declare la preferencia | `Sin verificar` | — |
| `SD-12005` | Garantía `G-4` · Aislamiento entre instancias | Dos instancias vivas **no comparten** escena, ni selección, ni disposición; un identificador liberado no se reutiliza y produce `INSTANCIA_DESCONOCIDA` | Test automatizado: `TC-12001`, `TC-12004` | `[EV-XX \| ejecucion \| dos instancias en la misma página \| independencia verificada \| fecha]` | **Mayor**: dos instancias se afectan, o un identificador liberado vuelve a resolver | `Sin verificar` | — |
| `SD-12006` | Garantía `G-5` · Sin fallo silencioso | **100 %** de las piezas no dibujadas quedan enumeradas con su índice y su código, y **0** desaparecen sin registro. Una pieza con dimensión en `0.00` **se dibuja** y no figura entre las no dibujadas | Test automatizado: `TC-12007`, con los escenarios `E-5`, `E-8` y `E-6` del intake §20 | `[EV-XX \| ejecucion \| caso de prueba de los escenarios E-5, E-8 y E-6 \| resultado de dibujo \| fecha]` | **Mayor, sin gradación.** Es el defecto original que `NB-00006` viene a cerrar, y perder la figura de `E-6` vaciaría la garantía | `Sin verificar` | — |
| `SD-12007` | Garantía `G-6` · Determinismo, y §5.4 | Dos procesados del mismo texto producen la **misma disposición**, comparable pieza por pieza; **se compara posición, no orientación**, y ningún movimiento la altera | Test automatizado: `TC-12009`, con el escenario `E-1` en las cuatro combinaciones de movimiento | `[EV-XX \| ejecucion \| doble procesado del escenario E-1 \| comparación de posiciones \| fecha]` | **Mayor, sin gradación**: la disposición cambia entre dos procesados del mismo texto | `Sin verificar` | — |
| `SD-12008` | Garantía `G-7` · Terminación controlada | Ninguna condición deja la instancia en estado indeterminado: o la operación surte efecto completo, o la instancia queda como estaba y la condición se informa por su código | Test automatizado: `TC-12002`, `TC-12010`, `TC-12011`, `TC-12012`, `TC-12013` | `[EV-XX \| ejecucion \| casos de condición del catálogo \| estado de la instancia tras cada uno \| fecha]` | **Mayor**: una condición deja la instancia a medio modificar | `Sin verificar` | — |
| `SD-12009` | Los **siete** códigos de `Definicion-Contrato-De-Fachada.md` §6, en sus **ocho** cursos | El bundle informa exactamente los siete códigos del contrato y **ninguno más**; `INSTANCIA_DESCONOCIDA` aparece en **cinco** funciones y sigue siendo **un solo código**; `ELEMENTO_DE_DIBUJO_INVALIDO` se presenta en sus **dos cursos** y sigue siendo un solo código | Test automatizado: `TC-12021`, comparación en las dos direcciones | `[EV-XX \| ejecucion \| comparación del conjunto emitido contra el contrato \| 7 de 7 y 0 fuera \| fecha]` | **Mayor**: aparece un código que no está en la lista cerrada de siete, o un curso nuevo se acuña como código | `Sin verificar` | — |
| `SD-12010` | §5.3 · Tipos dibujables y lectura de dimensiones | Se dibujan los **seis** tipos —tres volumétricos y tres planos—; las claves `Tapas` y `Bases` se aceptan como sinónimos; el ortoedro de `E-7` se dibuja con ancho 6, profundidad 4 y altura 8; y **el cero es una dimensión legible** | Test automatizado: `TC-12005`, `TC-12006`, `TC-12007`, con los escenarios `E-7`, `E-2`, `E-3`, `E-4` y `E-6` | `[EV-XX \| ejecucion \| caso de prueba de los escenarios E-7 y E-2 \| recuento de piezas dibujadas por tipo \| fecha]` | **Mayor**: falta un tipo dibujable, o un ortoedro con la clave `Tapas` deja de dibujarse. Es el defecto que hoy tiene el visualizador previo | `Sin verificar` | — |
| `SD-12011` | §5.5 · Gobierno del movimiento automático | Los **dos** movimientos se prenden y se apagan por separado sobre una instancia viva, **sin reconstruirla** y sin perder la selección; el no nombrado conserva su estado; al apagar el giro las piezas **vuelven a su orientación de partida**; los dos se detienen mientras la persona arrastra y mientras la superficie no está visible, **sin cambiar el estado gobernado**; y el estado **sobrevive a la carga de otro texto** | Test automatizado: `TC-12013`, `TC-12014`, `TC-12003` | `[EV-XX \| ejecucion \| casos de prueba de CU-12007 \| estado efectivo de los dos movimientos \| fecha]` | **Menor**: cambia la velocidad de un movimiento. **Mayor**: el cambio reconstruye la instancia, se pierde la selección, o apagar el giro deja las piezas donde el tiempo las encontró | `Sin verificar` | — |
| `SD-12012` | Las dos puertas técnicas `PT-02` y `PT-03` del intake §15 y §17.2.P.8 · GeometriaFactory-Visor | El motor de dibujo queda **dentro** del bundle y la página funciona sin acceso a redes externas; el bundle carga en una página del anfitrión, la creación arma la escena, la carga de `E-1` dibuja **las tres figuras incluido el ortoedro**, **diez** recorridos de ida y vuelta no degradan, y el árbol y la escena **se sincronizan por índice** | Test automatizado: `TC-12019` y `TC-12020`, con los recorridos medidos **con los dos movimientos prendidos** | `[EV-XX \| ejecucion \| medición de PT-02 y PT-03 \| los seis tramos, uno por uno \| fecha]` | **Mayor, sin gradación y sin excepción.** Una puerta que no pasa **detiene la planificación de la etapa `g`** y no se arrastra como deuda | `Sin verificar` | — |

| `SD-12013` | `VER-12001` de [`../10-Examples/ejemplo-01-basico.md`](../../../10-Examples/ejemplo-01-basico.md) §9 | El recorrido mínimo de la página integradora cierra sin backend: se crea la instancia sin dibujar nada, el texto de `E-1` produce **3** piezas dibujadas y **0** no dibujadas —`Cilindro`, `Cubo` y `Ortoedro`, uno de cada—, dos procesados dan la **misma disposición**, el identificador liberado produce `INSTANCIA_DESCONOCIDA`, y las peticiones del archivo de guion son **0** | El comando del contrato: `bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify` | Campo `evidencia` de `VER-12001`, con su fecha | **Menor**: cambia el texto de una línea de salida sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, cambia el comando sin actualizar el contrato, aparecen precondiciones no declaradas, o `CU-12001`, `CU-12002` o `CU-12005` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-12014` | `VER-12002` de [`../10-Examples/ejemplo-02-intermedio.md`](../../../10-Examples/ejemplo-02-intermedio.md) §9 | La lectura del dato del alumno y la selección por índice se sostienen: `E-7` da **6** piezas con **3** tipos volumétricos y **3** planos y el ortoedro con ancho 6, profundidad 4 y altura 8; las claves `Tapas` y `Bases` son sinónimos; `E-8` enumera la pieza del índice **1** con `DIMENSION_NO_LEGIBLE` y campo `Largo`; `E-6` **se dibuja**; y `ELEMENTO_DE_DIBUJO_INVALIDO` en su curso **C-2** deja la instancia viva | El comando del contrato: `bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify` | Campo `evidencia` de `VER-12002`, con su fecha | **Menor**: cambia el texto de una línea sin cambiar su semántica. **Mayor**: el `criterio_aceptacion` falla, la figura de `E-6` deja de dibujarse, un escenario se sustituye o se reformatea, o `CU-12002`, `CU-12003` o `CU-12004` dejan de estar cubiertos | `Sin verificar` | — |
| `SD-12015` | `VER-12003` de [`../10-Examples/ejemplo-03-avanzado.md`](../../../10-Examples/ejemplo-03-avanzado.md) §9 | El contrato entero se ejerce sin backend: **6 de 6** funciones, **6 de 6** propiedades transversales y **2 de 2** puertas técnicas; el archivo de guion expone **6** funciones bajo **1** nombre global con **0** globales sueltas y **0** ocurrencias de las tres formas de petición en la fuente **y** en el bundle generado; las peticiones medidas **con los dos movimientos prendidos y sostenidos** son **0**; las claves escritas son **0**; y los códigos son **7 de 7** con **0** acuñados aguas abajo | El comando del contrato: `bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify` | Campo `evidencia` de `VER-12003`, con su fecha | **Mayor, sin gradación** en los tramos de umbral cero y en las dos puertas técnicas: son las mismas propiedades que `SD-12002`, `SD-12003` y `SD-12012` sensan desde el contrato. **Mayor** además si el `criterio_aceptacion` falla, si una corrida se hace **sin** las condiciones de medición declaradas en las precondiciones del contrato, o si `CU-12006` o `CU-12007` dejan de estar cubiertos | `Sin verificar` | — |

**Cobertura declarada.** Las **doce** primeras filas cubren: las **seis** funciones (`SD-12001`), las **siete** garantías (`SD-12002` a `SD-12008`, una por garantía), los **siete** códigos en sus **ocho** cursos (`SD-12009`), los **seis** tipos dibujables y la lectura de dimensiones (`SD-12010`), las **ocho** reglas de gobierno del movimiento (`SD-12011`) y las **dos** puertas técnicas (`SD-12012`). Las **seis** propiedades transversales de `02` §6 quedan alcanzadas dentro de `SD-12002`, `SD-12003`, `SD-12006`, `SD-12007`, `SD-12012` y —la de ejercitarse sin backend— dentro de `SD-12012` y de `TC-12015`, que `SD-12001` cita como método.

**Tres filas `VER-XX`, dadas de alta el 2026-08-11** al emitirse `10-Examples`. Son `SD-12013`, `SD-12014` y `SD-12015`, una por cada contrato de verificación de las tres partes del sample **S-1**, con el comando del contrato como método y sin ningún desvío. **Sin contratos huérfanos ni filas sin contrato que las respalde**, que es lo que exige `Deriva-Rules.md` §6: la correspondencia es uno a uno con `VER-12001`, `VER-12002` y `VER-12003` de `10-Examples`.

**Por qué las tres se solapan con filas anteriores sin duplicarlas.** `SD-12001` a `SD-12012` sensan **elementos del contrato** —las seis funciones, las siete garantías, los siete códigos— por el método que la categoría 08 les resolvió, que son casos de prueba de la batería. `SD-12013` a `SD-12015` sensan **el sample**, con su propio comando y su propia aserción, que es lo que `Deriva-Rules.md` §2.4 declara como carácter distintivo de la clase. Cuando las dos miran lo mismo, la deriva mayor es la misma y el umbral no se contradice: se declaró igual en las dos.

**Total: quince filas.** Doce ancladas en el contrato de la fachada y tres en los contratos de verificación de `10-Examples`. **Ninguna cita un identificador de línea de base visual.**

**Esta matriz no está vacía**, que es la condición que `Deriva-Rules.md` §2.3 exige: una matriz sin filas sería un proyecto de código sin instrumento de sensado.

## 4. Correspondencia con la matriz de `GeometriaFactory-Web`

Se declara para que ninguna lectura posterior confunda las dos matrices, y para que ningún elemento se sense dos veces con umbrales distintos.

| Fila de esta matriz | Fila de la matriz de Web que mira el mismo elemento desde el lado visual | Qué mira cada una |
| --- | --- | --- |
| `SD-12001` | `SD-12043` de Web | Acá: que la superficie del bundle sean seis funciones. Allá: que **la escena se opere exclusivamente por esas seis funciones** desde el componente anfitrión |
| `SD-12002` | `SD-12043` de Web | Acá: el recuento sobre el bundle con los movimientos prendidos. Allá: el recuento durante la interacción con la escena |
| `SD-12003` | `SD-12047` de Web | Acá: que la fachada no escriba ninguna clave. Allá: que **la preferencia de cada movimiento sea del componente anfitrión** |
| `SD-12006` | `SD-12039` y `SD-12040` de Web | Acá: la enumeración en el resultado de dibujo. Allá: que la pieza de dimensión `0.00` **se dibuje** y que el recuento de piezas sin registro sea 0 |
| `SD-12007` | `SD-12041` y `SD-12045` de Web | Acá: la comparación de dos procesados por posición. Allá: la comparación de disposiciones en las cuatro combinaciones de movimiento |
| `SD-12009` | `SD-12018` de Web | Acá: el conjunto emitido contra el contrato. Allá: que **los ocho estados que materializan las siete condiciones existan y usen los códigos sin renombrarlos** |
| `SD-12011` | `SD-12044`, `SD-12046` y `SD-12048` de Web | Acá: el gobierno por la fachada. Allá: los **controles** de los dos movimientos, su reposición de orientación y su comportamiento con preferencia de movimiento reducido declarada |
| `SD-12012` | `SD-12042` de Web | Acá: las dos puertas enteras. Allá: los **diez recorridos** de ida y vuelta sin degradación |

**Las dos matrices son complementarias y no redundantes.** La de Web ancla cada fila en un identificador de línea de base **validado visualmente**; ésta ancla cada fila en un elemento del **contrato**. Cuando las dos miran lo mismo, lo miran desde lados distintos: allá se pregunta si se parece a lo aprobado, acá si sigue haciendo lo que dice.

**Un elemento que la maqueta no validó y esta matriz sí sensa**: la **sexta función**, `establecerMovimiento`. La matriz de Web declara en su §4 que se incorporó al contrato **después** de que el Product Owner aprobó la maqueta y que **no fue validada visualmente**, y por eso su `SD-12043` la sensa contra el contrato y no contra la maqueta. Acá no hay tensión que resolver: **todas** las filas de esta matriz se anclan en el contrato.

## 5. Umbrales de deriva aplicados

Derivados de la tabla de `Deriva-Rules.md` §3, sin agregarle dimensiones.

| Dimensión | Deriva menor, se registra y no bloquea | Deriva mayor, bloquea y exige decisión | Filas |
| --- | --- | --- | --- |
| Superficie pública del contrato | Cambia un nombre interno no expuesto | Falta una función, aparece una séptima, cambia un nombre expuesto o aparece un identificador global suelto | `SD-12001` |
| Garantías del contrato | — | Perder cualquiera de las siete. **Es cambio mayor aunque las seis firmas no se toquen** | `SD-12002` a `SD-12008` |
| Conjunto cerrado de condiciones | — | Aparece un código fuera de la lista de siete, o un curso nuevo se acuña como código | `SD-12009` |
| Lectura del dato del alumno | Cambia el orden en que se leen dos claves equivalentes | Falta un tipo dibujable, una variante de clave deja de aceptarse, o el cero deja de ser dimensión legible | `SD-12010` |
| Gobierno del movimiento | Cambia la velocidad o la duración de una detención | El cambio reconstruye la instancia, se pierde la selección, o no se repone la orientación de partida | `SD-12011` |
| Puertas técnicas | — | Cualquier tramo que no pase | `SD-12012`, `SD-12015` |
| Contratos y comportamiento (`VER-XX`) | Cambia el texto de un mensaje de salida sin cambiar su semántica, o cambia el formato de un registro | El `criterio_aceptacion` falla; cambia el comando de ejecución sin actualizar el contrato; aparecen precondiciones no declaradas; o el caso de uso que la sonda ejercita deja de estar cubierto | `SD-12013`, `SD-12014`, `SD-12015` |

**Las filas sin gradación** —`SD-12002`, `SD-12003`, `SD-12006`, `SD-12007`, `SD-12012` y los tramos de umbral cero y de puerta técnica de `SD-12015`— declaran deriva mayor ante cualquier diferencia, porque verifican garantías del contrato o puertas técnicas, que no admiten tolerancia.

**Toda deriva mayor se resuelve por una de dos vías y nunca por omisión** (`Deriva-Rules.md` §3): se corrige el bundle para volver al contrato, o **se cambia el contrato con aprobación humana explícita**, en cuyo caso la categoría 02 lo modifica, el intake lo consolida y esta matriz se rehace. Una garantía o un código **no se cambian desde acá**.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Actualización de trazabilidad al resolver el informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** El `README.md` de [`../10-Examples/`](../10-Examples/), del que esta matriz toma sus contratos, pasó a **1.1** al corregir sus puntos abiertos falsos sobre el `PRODUCT-INTAKE` §16.1 y §18; la trazabilidad upstream lo cita ahora en esa versión. Las carpetas de `/samples` que el **P0-1** reclamaba **ya existen**, esqueletadas con su README local y su comando previsto, de modo que el «método de verificación» de cada fila apunta a una ruta que resuelve. **Ninguna fila, contrato, umbral ni estado cambia**, y las sondas siguen en `Sin verificar` sin fecha. Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor. |
| 1.1 | 2026-08-11 | **Alta de las sondas `VER-XX` por AG-10**, al cerrar la fase que genera `10-Examples`. Es el segundo momento de sensado de `Deriva-Rules.md` §4. Suma **tres** filas, `SD-12013` a `SD-12015`, una por cada contrato de verificación de las tres partes del sample **S-1**, con el comando del contrato como método sin desvío, el campo `evidencia` del sample como evidencia esperada y estado `Sin verificar` sin fecha. La matriz pasa de **doce** a **quince** filas. §5 suma la dimensión «Contratos y comportamiento (`VER-XX`)» de `Deriva-Rules.md` §3 y agrega `SD-12015` a la fila de puertas técnicas. Se declara por qué las tres nuevas se solapan con filas anteriores sin duplicarlas —unas sensan el elemento del contrato por caso de prueba, las otras el sample por su propio comando— y que el umbral no se contradice entre ellas. **Ninguna de las doce filas anteriores cambia.** Sube minor: agrega filas y una dimensión de umbral sin alterar ninguna afirmación ya emitida. |
| 1.0 | 2026-08-11 | Emisión inicial, abierta por AG-08 en la Fase E. Declara por qué existe una matriz para este proyecto de código —`requiere_maqueta` es true— y **contra qué sensa**, dado que la Fase B2 se ejecutó sin maqueta propia y sus tres artefactos de línea de base viven en la categoría 03 de `GeometriaFactory-Web`: todas las filas se anclan en elementos del **contrato de la fachada** y ninguna cita un identificador de línea de base visual. Declara **doce** filas, `SD-12001` a `SD-12012`, con método de verificación resuelto, evidencia esperada, umbral de deriva y estado `Sin verificar`, que cubren las seis funciones, las siete garantías una por una, los siete códigos en sus ocho cursos, los seis tipos dibujables, las ocho reglas de gobierno del movimiento y las dos puertas técnicas. Declara la correspondencia con **ocho** filas de la matriz de `GeometriaFactory-Web` para que ningún elemento se sense dos veces con umbrales distintos, y la ausencia de filas `VER-XX` por no estar emitida la categoría 10. |
