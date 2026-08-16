# Wireframes — Vista de trabajo

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Vista-De-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md` íntegro —§4 pasos 4 a 13, FA-01 a FA-06, §6 y CA-01 a CA-10—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-04, RT-05, RT-07, RT-10, RT-11) y §7; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §3.1, §3.2, §4, §5.2 y §6; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §1, §4, §5 (los siete criterios); `NB-05` §5 (tercer criterio); `NB-04` §5 (sexto criterio); `NB-07` §5 (quinto criterio); `NB-09` §5 (sexto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-11, F-13), §6 (flujo 3), §14 (RA-02, RA-03), §17.6 P.3, P.10, **P.11 punto 4 y punto 5**; `Design-Rules-Web-Generico.md` §3, §4, §5, §7, §8; `Design-Rules-Blazor-Mudblazor.md` §2, §4, §5
**Trazabilidad downstream:** Fase B2 de validación visual de maqueta; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Pantalla y propósito](#1-pantalla-y-propósito)
- [2. Layout](#2-layout)
- [3. Componentes principales](#3-componentes-principales)
- [4. Interacciones](#4-interacciones)
- [5. Estados](#5-estados)
- [6. Versión angosta](#6-versión-angosta)
- [7. Notas de implementación](#7-notas-de-implementación)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Pantalla y propósito

**Nombre canónico de superficie: `Vista-De-Trabajo`.**

Presenta un trabajo cargado con sus **cuatro partes**: datos y texto original a la izquierda; elemento de dibujo arriba y árbol de la estructura abajo, a la derecha. La persona explora la escena, recorre el árbol, sincroniza los dos por índice de pieza, lee las observaciones y, si lo hay, el comentario del administrador.

**Es idéntica para el alumno dueño y para el administrador**, y esa identidad es un criterio de éxito de negocio: cuatro de cuatro elementos, las mismas observaciones. La única diferencia es el bloque de decisión, que aparece para el administrador sobre un trabajo en estado `Pendiente` y se documenta en [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md).

**Disposición decidida aguas arriba.** Viene del visualizador que la cátedra ya usa y está probada en el aula. Este wireframe la **documenta y la precisa** —qué pasa en pantalla angosta, qué pasa mientras carga, qué pasa si el texto no verifica— y **no la rediseña**.

## 2. Layout

Shell de trabajo, con la barra lateral del papel de quien mira. Dos columnas en el área de contenido; la derecha se parte en dos filas.

```text
+----------+----------------------------------------------------------------+
| Laborat. |  < Volver al listado                                           |
|          |  Cubo y ortoedro          [Pendiente]                          |
| ·Mis     |  12/08/2026 · Ana Diaz · 3 piezas · 2 advertencias              |
|  trabajos+--------------------------+-------------------------------------+
| ·Trabajo |  DATOS DEL TRABAJO       |  ESCENA                             |
|  nuevo   |  Nombre  Cubo y ortoedro |  +-------------------------------+  |
| ·Mi      |  Fecha   12/08/2026      |  |                               |  |
|  contra- |  Estado  [Pendiente]     |  |   elemento de dibujo          |  |
|  seña    |  Alumno  Ana Diaz        |  |   (instancia del visualiz.)   |  |
|          |                          |  |                               |  |
| -------- |  Descripción             |  [x] Órbita de la cámara            |
| Ana Diaz |  Entrega de la Act. 1... |  [x] Giro de las figuras            |
| [Cerrar  |                          |  Todas las piezas se dibujaron.     |
|  sesión] |  COMENTARIO DEL DOCENTE  +-------------------------------------+
| v1.4.2   |  +---------------------+ |  ÁRBOL DE LA ESTRUCTURA             |
|          |  | Revisá el área del  | |  v conjunto (3)                     |
|          |  | cubo.               | |    > [0] Cilindro                   |
|          |  +---------------------+ |    > [1] Cubo                       |
|          |                          |    v [2] Ortoedro         <-- selec.|
|          |  OBSERVACIONES (2)       |        Bases (2)                    |
|          |  [adv] fig. 1 · Area     |        Laterales (4)                |
|          |        decl. 36.00       |                                     |
|          |        deriv. 54.00      |                                     |
|          |  [adv] fig. 2 · Volumen  |                                     |
|          |        decl. 343.00      |                                     |
|          |        deriv. 1029.00    |                                     |
|          |                          |                                     |
|          |  TEXTO ORIGINAL      [v] |                                     |
|          |  +---------------------+ |                                     |
|          |  | [ { "Tipo": "Cil... | |                                     |
|          |  +---------------------+ |                                     |
+----------+--------------------------+-------------------------------------+
```

**Los datos del esquema son los del escenario `E-1` del intake §20, y no se mezclan con los de otro escenario.** El trabajo dibujado produce **tres piezas —Cilindro, Cubo y Ortoedro— y dos advertencias**, ninguna pieza queda sin dibujar, y por eso el bloque de piezas no dibujadas aparece vacío con su declaración positiva. La lista de piezas no dibujadas se demuestra en el estado `piezas-no-dibujadas` de §5, con el escenario `E-5`, que es el que tiene una pieza de tipo no dibujable. Un esquema que tomara el árbol de un escenario y los recuentos de otro enseñaría a construir una superficie que no puede existir.

**Los valores `declarado` y `derivado` se escriben con punto, también en este esquema.** Es la excepción declarada de §7 a la convención de coma decimal: se muestran exactamente como el texto del alumno los trae y como el sistema los recalcula, **sin reformatear**. El esquema los dibuja así a propósito, para que quien lo copie no reintroduzca el formateo que la regla prohíbe.

**Por qué las observaciones y el comentario están en la columna izquierda y esto no agrega una quinta parte.** Las cuatro partes son datos, texto, escena y árbol. Las observaciones y el comentario **pertenecen a los datos del trabajo**: son lo que el producto y el docente dicen sobre él, no una parte nueva de la disposición. Ubicarlos a la derecha habría empujado la escena o el árbol fuera de vista, que es exactamente lo que la disposición probada evita.

**Orden de la columna izquierda, y por qué ése.** Datos → comentario → observaciones → texto original. El comentario va **antes** que las observaciones porque es lo primero que el alumno busca cuando su trabajo ya tiene desenlace, y el texto original va último y colapsado porque es largo y es material de consulta, no de lectura.

**Proporción de las columnas.** La izquierda toma alrededor de un tercio y la derecha dos tercios [ASUNCIÓN sujeta a la validación visual de la Fase B2]. El bloque de la escena mantiene una relación de aspecto próxima a 4:3 dentro de su columna, dejando al árbol el resto de la altura.

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Barra de regreso | Base §4.9, píldora auxiliar | Volver al listado de origen | «Volver al listado» | Lleva al listado del papel de quien mira: el propio si es el alumno, el de la comisión si es el administrador |
| Cabecera del trabajo | Base §2.2 | Identificar el trabajo en una línea | Nombre, insignia de estado, fecha, alumno dueño, recuento de piezas y de advertencias | Inerte. El nombre es el encabezado de primer nivel de la superficie |
| Insignia de estado | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) §2 | Declarar el estado | Uno de los cuatro valores | Texto siempre presente. El color es refuerzo |
| Bloque de datos | Base §4.4, filas clave/valor | Presentar los datos declarados | Nombre, fecha, estado, alumno, descripción | Solo lectura en todos los casos. **Esta superficie nunca edita** |
| Bloque de comentario | — | Presentar el comentario del administrador | Texto libre, a lo sumo uno | **Bloque propio, separado de las observaciones.** Sin severidad, sin índice, sin campo señalado, sin tono de alerta. **No se dibuja si no viene poblado** |
| Lista de observaciones | [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md) | Presentar lo que el producto emitió al interpretar | Severidad, índice de figura, campo señalado, y el par declarado/derivado en las advertencias | **Sin filtrar por severidad.** Con cero elementos, muestra «Sin observaciones» y no un hueco |
| Bloque de texto original | Base §4.6, divulgación progresiva | Conservar el texto a la vista | El texto **íntegro, carácter por carácter** | Colapsable, colapsado por omisión. Solo lectura y sin reescritura de ningún carácter |
| Bloque de la escena | — | Alojar el elemento de dibujo | La escena de la instancia del visualizador | Es el **componente anfitrión**: provee el elemento, invoca las cinco funciones y opera el ciclo de vida |
| Lista de piezas no dibujadas | Base §5 | Enumerar lo que no se dibujó | Índice de pieza y motivo, por pieza | **Junto a la escena, nunca en la lista de observaciones.** Con cero elementos, declara en positivo que todas las piezas se dibujaron |
| Controles de movimiento automático | Base §4.6, grupo de casillas | Gobernar los **dos movimientos** de la escena (F-25) | Dos casillas independientes, con etiqueta visible: **«Órbita de la cámara»** y **«Giro de las figuras»** | **Al pie del área de dibujo, no en un panel aparte**: son preferencias de quien mira. Se tildan por separado y pueden estar tildadas las dos a la vez. Tildadas por omisión, salvo preferencia de movimiento reducido declarada por el sistema, en cuyo caso **arrancan destildadas y el control declara por qué**. La elección se conserva entre trabajos. Los dos se detienen mientras la persona arrastra y con la pestaña oculta. **Ninguno de los dos altera la disposición de las piezas** |
| Árbol de la estructura | — | Recorrer el texto como jerarquía | La estructura que devuelve la fachada | Colapsable por nodo. El nodo de una pieza lleva su índice a la vista |
| Bloque de decisión | [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md) | Dar desenlace | Dos decisiones y comentario opcional | Se aloja debajo de la cabecera. **Sólo para el administrador y sólo en estado `Pendiente`** |

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Abrir el trabajo | Activación desde un listado | Se pide el detalle, se arma la superficie, se inicializa la instancia y se carga el texto **una sola vez por trabajo** | Sesión iniciada y trabajo visible para quien lo pide |
| Seleccionar un nodo de pieza en el árbol | Activación con puntero o con teclado | Se pide resaltar esa pieza por su índice. **La escena resalta esa pieza y sólo esa** | La instancia está viva |
| Seleccionar una pieza en la escena | Interacción con la escena | El nodo correspondiente del árbol queda marcado y visible, por el **mismo índice**, sin traducir identidades | Ídem |
| Girar, acercar y encuadrar la escena | Interacción con la escena | La cámara se mueve. **Cero tráfico de circuito hacia el servidor durante toda la interacción** | Ídem |
| Prender o apagar la órbita de la cámara | Tildar o destildar su casilla | La cámara empieza o deja de girar sola alrededor del conjunto. **Las piezas quedan quietas** y la disposición no cambia. El cambio se anuncia como región activa | La instancia está viva |
| Prender o apagar el giro de las figuras | Tildar o destildar su casilla | Cada pieza empieza o deja de rotar sobre su eje vertical, en su lugar. **Al apagarlo, las piezas vuelven a su orientación de partida**, para que dos personas que lo apagan vean exactamente lo mismo. La disposición no cambia | Ídem |
| Colapsar o expandir un nodo | Activación | El nodo cambia de estado. No toca la escena | — |
| Expandir el texto original | Activación del control de divulgación | Se muestra el texto íntegro | — |
| Cambiar el tamaño disponible para la escena | Cambio de tamaño de la ventana o del punto de quiebre | El componente anfitrión **invoca el ajuste**. No ocurre solo: la fachada no observa tamaños ni decide cuándo ajustar | La instancia está viva |
| Pasar de un trabajo al siguiente | Activación desde el listado, o navegación directa | Se libera la instancia vigente antes de crear la nueva, o se recarga el texto sobre la instancia viva, que reemplaza por completo lo dibujado | — |
| Abandonar la superficie | Navegación a otra ruta | **Se libera la instancia. No es opcional**: sin eso, recorrer trabajos acumula contextos gráficos en el navegador | — |

**Regla de aislamiento, y es una restricción de diseño y no sólo de implementación.** La escena se opera **exclusivamente** por las cinco funciones de la fachada. Ninguna interacción de esta superficie superpone marcas sobre la escena, lee su contenido, la captura ni toca su interior. La lista de piezas no dibujadas está **al lado** y no encima, y la alternativa textual del elemento de dibujo se compone con lo que la fachada devolvió, no con lo que la escena muestra.

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | **No aplica al trabajo**: una superficie de detalle siempre tiene un trabajo o no tiene superficie. Sí aplican los vacíos internos, declarados abajo | Se declara para que la ausencia sea deliberada |
| **Cargando** | El detalle está en camino | Esqueleto en las cuatro partes y barra fina en la parte superior del contenido. La escena **no se inicializa hasta que su elemento tiene tamaño** |
| **Con datos** | El detalle llegó y la escena dibujó | Las cuatro partes completas |
| **Sin observaciones** | La colección llegó con cero elementos, no ausente | Línea explícita «Sin observaciones». **Nunca un bloque en blanco** |
| **Sin comentario** | El bloque de comentario no viene poblado | **El bloque no se dibuja.** El estado del trabajo expresa el desenlace por sí solo |
| **Con comentario** | Viene poblado | Bloque propio, separado de las observaciones, sin severidad ni índice ni campo |
| **Piezas no dibujadas** | El resultado enumera piezas sin dibujo | Lista con índice y motivo junto a la escena. Las demás piezas se dibujan. **Ninguna desaparece sin registro** |
| **Trabajo en borrador con errores** | El trabajo está en estado `Borrador` y su texto tiene errores de validación | El detalle llega igual: se dibuja lo que se pudo reconstruir, se enumeran las piezas faltantes y las observaciones de error de validación se muestran pobladas |
| **Texto no legible** | La fachada no obtuvo piezas del texto | La instancia queda viva y vacía. Se informa junto a la escena, y **el árbol y las observaciones se muestran igual** |
| **Escena no disponible** | El navegador no provee la capacidad gráfica tridimensional | El bloque de la escena se reemplaza por un bloque explicativo. **Se mantienen las otras tres partes**: datos, texto y árbol |
| **Elemento de dibujo sin tamaño, en creación** | El elemento no sirve como superficie al crear la instancia | No hay instancia. Se informa que la escena no está disponible y se conservan las otras tres partes. Al recuperar tamaño, se vuelve a crear |
| **Elemento de dibujo sin tamaño, en ajuste** | El elemento pasó a tamaño cero al pedir el ajuste | **La instancia sigue viva**, con su escena y su selección intactas. Una invocación posterior ajusta cuando el elemento vuelva a tener tamaño. Sin aviso a la persona |
| **Índice sin representación** | Se pide resaltar un índice que no corresponde a ninguna pieza dibujada | La selección vigente se conserva. Se indica que esa pieza no tiene representación en la escena, y **el nodo del árbol sigue siendo navegable** |
| **Error de operación** | El identificador no corresponde a un trabajo visible, o no existe | Mensaje neutro que **no distingue** el trabajo ajeno del inexistente, y regreso al listado de quien lo pidió |
| **Indisponible** | El servicio de datos no responde | Aviso de indisponibilidad. **No se arma ni la escena ni el árbol.** Ver [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) |
| **Reconectando** | Se corta el circuito | Cartel de reconexión superpuesto. **La escena permanece a la vista y sigue girándose**, porque no depende del circuito |

El último estado merece su nota: es el único lugar del producto donde la persona puede seguir haciendo algo útil mientras el circuito está caído, y es una consecuencia directa de que la escena no hace red.

## 6. Versión angosta

Es la parte de esta superficie que el diseño sí decide, porque la disposición probada en el aula es de pantalla ancha y aguas arriba no dice qué pasa cuando no la hay.

Punto de quiebre principal en 768 px [ASUNCIÓN].

- **Las dos columnas se apilan en una sola**, y el orden pasa a ser: cabecera → bloque de decisión, si corresponde → **escena** → piezas no dibujadas → **árbol** → datos y descripción → comentario → observaciones → texto original.
- **La escena sube al primer lugar y es deliberado.** En pantalla ancha las cuatro partes conviven; en angosta hay que elegir qué se ve sin desplazarse, y es el dibujo: es a lo que la persona vino y es lo que no tiene sustituto. Los datos y el texto son consulta y toleran estar más abajo.
- **El comentario y las observaciones no bajan del texto original.** El texto original se mantiene último y colapsado.
- La escena toma el ancho disponible con su relación de aspecto, con una altura mínima que la deje utilizable. Por debajo de esa altura mínima el bloque **no se colapsa a cero**: eso produciría el estado de elemento sin tamaño y perdería la instancia.
- **Al cruzar el punto de quiebre, el componente anfitrión invoca el ajuste de la escena.** Es la interacción de la sección 4 que en pantalla angosta se dispara sola por reflujo, y omitirla deja la escena deformada o fuera de encuadre.
- La barra lateral colapsa a navegación superior o a cajón, según el patrón del documento base. El sello de versión viaja con ella.
- Contenido legible sin desplazamiento horizontal a 320 px. **El texto original y el árbol se desplazan dentro de su propio contenedor**, nunca haciendo desplazar la página entera.

## 7. Notas de implementación

**Accesibilidad.** Es la superficie con más carga de accesibilidad del producto y tiene tres puntos propios:

1. **La escena no es la única vía a la información.** El árbol presenta el mismo contenido, se recorre con flechas, se activa con la barra o el ingreso, y su selección va en los dos sentidos por el mismo índice. Quien no puede operar la escena tiene el árbol completo.
2. **El elemento de dibujo lleva una alternativa textual** que declara qué es y qué contiene, compuesta con el recuento de piezas dibujadas y no dibujadas del resultado. Se compone con lo que la fachada devolvió: **no se lee del interior de la escena**.
3. **Ninguna pieza desaparece sin quedar enumerada en texto.** La lista de piezas no dibujadas es, además de la eliminación del fallo silencioso, lo que hace que la información del dibujo exista fuera del dibujo.

Además: el árbol se marca como estructura de árbol, con estado de expansión declarado por nodo y con el nodo seleccionado anunciado. El cambio de selección se anuncia como región activa: quien no ve la escena tiene que enterarse de que el resaltado cambió. Las observaciones llevan su severidad **escrita**, no sólo su color. El bloque de comentario se distingue del de observaciones por encabezado propio y no sólo por posición. La escena **sí se mueve sola** cuando alguno de los dos movimientos automáticos está tildado, y por eso el movimiento ambiental es un problema que esta superficie tiene que resolver y no evitar: los dos movimientos son **gobernables por la persona** con dos casillas que son controles de formulario reales —etiqueta visible y asociada, operables por teclado, agrupadas con nombre y con su cambio anunciado como región activa—, y **arrancan destildados cuando el sistema declara preferencia de movimiento reducido**, con el control declarando por qué. Con las dos casillas destildadas la escena sólo se mueve cuando la persona la arrastra. Las transiciones de expansión del árbol respetan la misma preferencia.

**Performance percibida.** El texto del trabajo viaja del servidor al navegador **una sola vez por trabajo**, y ni el árbol ni la escena se vuelven a componer desde el servidor. Durante la interacción con la escena **no hay tráfico de circuito**: es el único lugar del producto con respuesta inmediata, y conviene no gastarlo en animaciones de entrada. La carga muestra esqueleto en las cuatro partes por encima de 400 ms.

**Internacionalización.** Los valores declarado y derivado de cada advertencia se muestran **exactamente como el texto del alumno los trae y como el sistema los recalcula**, sin reformatear: reescribirlos rompería la comparación que es el mayor valor didáctico del producto. Las fechas del trabajo y de registro se rotulan distinto para que no se lean como la misma.

**Restricciones de arquitectura.** Todo el detalle llega por el servidor de la pieza pública; **el navegador no emite ninguna petición hacia el servicio de datos**, ni siquiera mientras se rota la escena. La escena se opera sólo por las cinco funciones. La liberación de la instancia al descartar el componente **no es opcional**. Ningún mensaje incluye la dirección de un servicio interno.

**Los dos movimientos automáticos también se gobiernan por la fachada**, y su gobierno no es una excepción a la regla de aislamiento: la superficie no toca la escena para prender o apagar el movimiento. La **preferencia** de cada movimiento es de esta superficie, que es la que la conserva entre trabajos; la fachada la recibe y la ejerce, y **no guarda nada**. Ninguno de los dos movimientos origina una sola petición de red, y ninguno altera la disposición: el determinismo comprometido es de la **posición** de cada pieza, derivada de su índice, y no de su orientación en un instante. Ver `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §5.5.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno dueño y docente como administrador, con **la misma superficie** |
| CU origen | [`CU-07`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md) íntegro. `CU-09` aporta el bloque de decisión, documentado aparte |
| Reglas de negocio relevantes | `RN-03` (trabajo ajeno indistinguible de inexistente), `RN-08` (texto conservado íntegro), `RN-09` (observación con posición y campo), `RN-11` (el administrador no ve los borradores) |
| Restricciones transversales | `RT-03`, `RT-04`, `RT-05`, `RT-07`, `RT-10`, `RT-11` |
| Contrato de fachada | **Las cinco funciones**, con sus siete códigos de condición |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §2.4, §3.6, §4.3, §5.2, §7 |
| Representaciones que invoca | [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md), [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) §2 para la insignia, [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) en la barra lateral |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md`, `Design-Rules-Blazor-Mudblazor.md` |
| US a generar en 06 | `US-18`, `US-19`, `US-20`, `US-21` |
| Tests previstos en 08 | Guion de demostración de la etapa `g` completo: tres de tres piezas con el texto semilla, ortoedro incluido; sincronización por índice; pieza de tipo desconocido enumerada; diez idas y vueltas sin degradación; dos procesados con la misma disposición; cuatro de cuatro elementos para el administrador; recuento de peticiones del navegador con umbral 0; recorrido por teclado del árbol |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Documenta y precisa la disposición de cuatro partes decidida aguas arriba y probada en el aula, sin rediseñarla: declara el orden y el fundamento de la columna izquierda, por qué observaciones y comentario no agregan una quinta parte, dieciséis estados —incluidos los cinco del contrato de fachada y los dos cursos del elemento sin tamaño—, la versión angosta con la escena en primer lugar y el ajuste obligatorio al cruzar el punto de quiebre, y la resolución de accesibilidad de la escena tridimensional por árbol navegable, alternativa textual compuesta desde el resultado y enumeración textual de las piezas no dibujadas. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: las `NB-06`, `NB-05`, `NB-04`, `NB-07` y `NB-09` de la cabecera pasan a citarse con sección y criterio numerado. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-02** (coma contra punto): §2 escribe los cuatro valores `declarado` y `derivado` del esquema **con punto**, y suma la nota que declara que el esquema los dibuja así a propósito, porque la excepción de §7 a la convención de coma decimal ya lo exigía y el esquema la incumplía. **H-04** (recuentos sin dato declarado): §2 deja de mezclar dos escenarios: el árbol pasa a `Cilindro`, `Cubo` y `Ortoedro` con las `Bases (2)` del ortoedro —que es el escenario `E-1`, el que produce las tres piezas y las dos advertencias dibujadas—, la pieza no dibujada del escenario `E-5` sale del esquema y su demostración queda en el estado `piezas-no-dibujadas` de §5, y la lista de piezas no dibujadas con cero elementos pasa a declarar en positivo que todas se dibujaron. **F-25**: §3 suma el componente «Controles de movimiento automático» con las dos casillas independientes; §4 suma las dos interacciones de prender y apagar; §7 sustituye la afirmación de que la escena no gira sola —que la capacidad nueva vuelve falsa— por la resolución de accesibilidad del movimiento ambiental, y declara que el gobierno pasa por la fachada, que la preferencia es de esta superficie, que ninguno de los dos movimientos origina peticiones y que ninguno altera la disposición. |
