# DX — Catálogo de condiciones de la fachada y su diagnóstico accionable

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** DX-Error-Messages.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** DX Lead (AG-03)
**Variante:** DX

**Trazabilidad upstream:** `../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §3.2 (garantías G-1, G-5 y G-7), §3.3 (prohibiciones), §4.6 (`establecerMovimiento`, la sexta función), §5.1 (identificador de instancia), §5.2 (resultado de dibujo), §5.3 (tipos dibujables y lectura de dimensiones), §5.5 (gobierno del movimiento automático de la escena) y **§6 (los siete códigos de condición, fuente única de este catálogo)**; `../02-Especificacion-Funcional/Casos-De-Uso/CU-01` §6, `CU-02` §5 y §6, `CU-03` §5 y §6, `CU-04` §6, `CU-05` §6, `CU-06` §6, `CU-07` §5 y §6; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6; `../../../00-Contexto/Vision-Producto.md` §9.1 (fallo silencioso, observación, advertencia, error de validación); `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §4; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14 (RA-01 y RA-02), §17.7 P.3, P.5, P.6, P.10 y P.11 punto 4, §20 E-1 y E-7
**Trazabilidad downstream:** 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples (sample S-1), 11-Documentacion

---

## Tabla de contenido

- [1. Principios de redacción de errores](#1-principios-de-redacción-de-errores)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Categorías presentes](#21-categorías-presentes)
  - [2.2 Categorías declaradas ausentes, con su motivo](#22-categorías-declaradas-ausentes-con-su-motivo)
  - [2.3 Alcance de la condición: invocación completa o pieza suelta](#23-alcance-de-la-condición-invocación-completa-o-pieza-suelta)
- [3. Catálogo](#3-catálogo)
  - [3.1 Condiciones del elemento de dibujo y del entorno](#31-condiciones-del-elemento-de-dibujo-y-del-entorno)
  - [3.2 Condiciones de ciclo de vida del identificador](#32-condiciones-de-ciclo-de-vida-del-identificador)
  - [3.3 Condiciones de carga del texto del trabajo](#33-condiciones-de-carga-del-texto-del-trabajo)
  - [3.4 Condiciones de selección](#34-condiciones-de-selección)
  - [3.5 Cobertura del catálogo contra el contrato](#35-cobertura-del-catálogo-contra-el-contrato)
- [4. Situaciones que no son entradas de este catálogo](#4-situaciones-que-no-son-entradas-de-este-catálogo)
- [5. Tono y voz](#5-tono-y-voz)
- [6. Localización](#6-localización)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Principios de redacción de errores

El catálogo de un visualizador puro es **angosto**, y por eso tiene que ser exacto. Son siete códigos, ni uno más: los que declara `Definicion-Contrato-De-Fachada.md` §6. Este documento no los redefine ni agrega ninguno; los desarrolla en **trece** entradas de diagnóstico, porque un mismo código pedido desde funciones distintas necesita acciones distintas del lado del anfitrión. **La unidad de catalogación de esta categoría es la función**, y por eso la sexta función de la fachada, `establecerMovimiento` (`Definicion-Contrato-De-Fachada.md` §4.6), suma una entrada —`E-VIS-13`— **sin sumar ningún código**: la condición que puede informar, `INSTANCIA_DESCONOCIDA`, ya existía y pasa a presentarse en **cinco** funciones. Cada entrada declara además el **curso** del contrato del que deriva: §6 del contrato distingue dos cursos de `ELEMENTO_DE_DIBUJO_INVALIDO` —**C-1 en creación** y **C-2 en ajuste**— y los demás códigos tienen curso único. **Un curso no es un código: los códigos siguen siendo siete.**

Seis principios rigen cada entrada:

1. **Tres partes obligatorias.** Qué pasó, por qué pasó y qué hacer al respecto. Una entrada sin la tercera parte no está terminada.
2. **La acción sugerida es siempre del lado del anfitrión.** El archivo de guion no puede resolver ninguna condición por su cuenta: no pide datos, no reintenta y no consulta nada. Si una acción sugerida empieza con «esperar» o con «reintentar», está documentando algo que este proyecto de código no hace.
3. **La fachada emite un código; la frase la compone el anfitrión.** Este catálogo no contiene el texto que una persona lee: contiene el código, su causa y el trabajo que le queda al anfitrión. El anfitrión decide qué mostrar, a quién y en qué idioma.
4. **Ninguna condición culpa a nadie.** Que una pieza no se dibuje no dice nada sobre si el trabajo del alumno está bien: eso lo decide el backend. La redacción evita «el texto es inválido» y usa «no se pudo obtener un conjunto de piezas del texto recibido».
5. **Ninguna pieza desaparece sin registro** (garantía G-5). Las condiciones de alcance por pieza existen exactamente para eso: enumerar con su índice lo que no se dibujó. El fallo silencioso es el defecto que el producto viene a eliminar, y una condición sin entrada de catálogo lo reintroduce por la puerta de atrás. Su umbral no se redeclara acá: la ausencia de fallo silencioso es una de las seis propiedades transversales, y su membresía y su umbral viven en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, que es el lugar único.
6. **Ninguna condición deja la instancia a medias** (garantía G-7). Cada entrada declara el efecto sobre la instancia, y ese efecto nunca es «indeterminado».

## 2. Taxonomía

### 2.1 Categorías presentes

| Categoría | Qué agrupa | Códigos |
| --- | --- | --- |
| **Capacidad del entorno ausente** | El navegador no provee algo que la instancia necesita para existir | `CAPACIDAD_GRAFICA_AUSENTE` |
| **Entrada inválida** | Lo que el anfitrión pasó por parámetro no sirve para lo que pidió | `ELEMENTO_DE_DIBUJO_INVALIDO`, `TEXTO_NO_LEGIBLE`, `TIPO_NO_DIBUJABLE`, `DIMENSION_NO_LEGIBLE`, `INDICE_FUERA_DE_RANGO` |
| **Conflicto de ciclo de vida** | El identificador no designa ninguna instancia viva | `INSTANCIA_DESCONOCIDA` |

### 2.2 Categorías declaradas ausentes, con su motivo

Se declaran para que nadie las agregue después por analogía con otro proyecto de código:

| Categoría ausente | Por qué no existe acá |
| --- | --- |
| **Recurso ausente** en el sentido de «no se encontró lo que se fue a buscar» | La fachada no va a buscar nada. Todo lo que necesita llega por parámetro (garantía G-3). `CAPACIDAD_GRAFICA_AUSENTE` no es de esta categoría: no es un recurso que falta, es una capacidad del entorno que no está |
| **Error transitorio** | No hay red, no hay reintento y no hay nada que pueda estar disponible más tarde por sí solo. Toda condición de este catálogo es determinista: con la misma entrada vuelve a producirse igual (garantía G-6) |
| **Error interno** | La garantía G-7 obliga a terminación controlada: toda condición que la fachada puede reportar está en §6 del contrato. Un fallo que no encaje en los siete códigos **no se cataloga acá**: es un defecto de implementación y va al backlog, no al catálogo de condiciones |
| **Error de validación del trabajo** y **advertencia** | Son observaciones del dominio y **las emite el backend**, no este proyecto de código (`Vision-Producto.md` §9.1, PRODUCT-INTAKE §17.7 P.11 punto 4). No confundirlas con las condiciones de contrato de acá: no comparten ni el emisor, ni el efecto, ni el nombre |
| **Error de autorización** | La fachada no sabe quién es la persona ni qué papel cumple. Prohibición explícita de PRODUCT-INTAKE §17.7 P.5 |

### 2.3 Alcance de la condición: invocación completa o pieza suelta

Distinción que cambia lo que el anfitrión tiene que hacer, y por eso está antes del catálogo:

| Alcance | Qué significa | Códigos |
| --- | --- | --- |
| **Invocación completa** | La invocación no surtió efecto, o surtió efecto dejando la instancia vacía. Se informa como condición general | `CAPACIDAD_GRAFICA_AUSENTE`, `ELEMENTO_DE_DIBUJO_INVALIDO`, `INSTANCIA_DESCONOCIDA`, `TEXTO_NO_LEGIBLE`, `INDICE_FUERA_DE_RANGO` |
| **Pieza suelta** | La invocación fue exitosa y dibujó lo que pudo; la condición viene **por pieza**, dentro del resultado de dibujo, junto al índice de la pieza afectada | `TIPO_NO_DIBUJABLE`, `DIMENSION_NO_LEGIBLE` |

Consecuencia práctica: un anfitrión que sólo mire la condición general de `cargarJson` **va a creer que todo salió bien** en un trabajo al que le faltan piezas. La lista de piezas no dibujadas se lee siempre, incluso cuando la carga fue exitosa.

## 3. Catálogo

Trece entradas, `E-VIS-01` a `E-VIS-13`, derivadas de los **siete** códigos de `Definicion-Contrato-De-Fachada.md` §6. El identificador de entrada es de esta categoría; **el código es del contrato y no se traduce, no se agrupa y no se renombra acá**.

### 3.1 Condiciones del elemento de dibujo y del entorno

| Entrada | Código | Curso | Categoría | Función | Qué pasó | Por qué pasó | Qué hacer del lado del anfitrión |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **E-VIS-01** | `CAPACIDAD_GRAFICA_AUSENTE` | Único | Capacidad del entorno ausente | `inicializar` | No se creó la instancia y no hay identificador que conservar | El navegador no provee la capacidad gráfica tridimensional que la instancia necesita. La combinación está declarada **no soportada** en `Compatibilidad-Plataformas.md` §4 | No reintentar: el resultado va a ser el mismo mientras sea el mismo navegador. Mostrar en el lugar del elemento de dibujo una explicación de que ese navegador no puede dibujar en tres dimensiones, y dejar accesible el resto del trabajo —los datos y el texto— que no depende de la escena. No ocultar el árbol: la estructura del texto no requiere capacidad gráfica |
| **E-VIS-02** | `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-1, en creación** | Entrada inválida | `inicializar` | No se creó la instancia y no se devolvió identificador. La página quedó exactamente como estaba | El elemento entregado no sirve como superficie de dibujo, o tiene tamaño nulo. El caso más frecuente es invocar antes de que el elemento esté presente y medido, o con el elemento todavía oculto | Invocar `inicializar` **después** de que el elemento de dibujo exista en la página y tenga tamaño distinto de cero. Si la vista lo crea oculto, diferir la invocación hasta que sea visible. La fachada no crea, no ubica ni redimensiona elementos de la página: eso es del anfitrión |
| **E-VIS-07** | `ELEMENTO_DE_DIBUJO_INVALIDO` | **C-2, en ajuste** | Entrada inválida | `redimensionar` | No se recalculó la relación de aspecto. **La instancia sigue viva**, con su escena y su selección intactas | El elemento de dibujo de una instancia viva dejó de servir como superficie, o pasó a tamaño cero, por ejemplo porque la vista lo ocultó o lo desmontó de la página | No destruir la instancia: no hace falta. Cuando el elemento vuelva a tener tamaño, invocar `redimensionar` otra vez y la escena se ajusta. Si la vista oculta y muestra su contenido, invocar al volver a mostrarlo |

**Por qué `E-VIS-07` vive acá y no bajo las condiciones de carga.** Su código es `ELEMENTO_DE_DIBUJO_INVALIDO` y su tema es el elemento de dibujo, no el texto del trabajo. Agrupa con `E-VIS-02` porque las dos son el mismo código en sus dos cursos declarados por `Definicion-Contrato-De-Fachada.md` §6 —**C-1 en creación** y **C-2 en ajuste**—, y leerlas juntas es lo que muestra que la reacción del anfitrión es la misma y lo que cambia es el momento del ciclo de vida. **Los identificadores de entrada no se renumeran**: son estables y se citan desde otras categorías; el orden de esta tabla es de agrupación, no de numeración.

### 3.2 Condiciones de ciclo de vida del identificador

Las **cinco** entradas comparten el mismo código y la misma causa; lo que cambia es qué le quedó por hacer al anfitrión. Son cinco desde que la fachada tiene **seis** funciones: `establecerMovimiento` también exige identificador y también puede recibir uno que ya no designa nada.

| Entrada | Código | Curso | Categoría | Función | Qué pasó | Por qué pasó | Qué hacer del lado del anfitrión |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **E-VIS-03** | `INSTANCIA_DESCONOCIDA` | Único, en cinco funciones | Conflicto de ciclo de vida | `cargarJson` | No se dibujó nada y ninguna instancia cambió | El identificador no corresponde a ninguna instancia viva: o nunca existió, o ya se liberó con `destruir` | No volver a cargar con ese identificador. Invocar `inicializar` otra vez, conservar el identificador nuevo y recién entonces cargar el texto. Revisar el orden de la vista: cargar un trabajo antes de haber inicializado, o después de haber destruido al salir, es el origen habitual |
| **E-VIS-04** | `INSTANCIA_DESCONOCIDA` | Único, en cinco funciones | Conflicto de ciclo de vida | `seleccionarPieza` | No se resaltó nada y ninguna instancia cambió | Mismo motivo que E-VIS-03. Suele aparecer cuando el árbol sigue vivo en la página después de que la instancia se destruyó | Dejar de emitir selecciones cuando la instancia ya no existe: al destruir, el anfitrión descarta el identificador **y** desactiva la interacción del árbol con la escena. El árbol puede seguir mostrándose; lo que no puede es seguir pidiendo resaltados |
| **E-VIS-05** | `INSTANCIA_DESCONOCIDA` | Único, en cinco funciones | Conflicto de ciclo de vida | `redimensionar` | No se recalculó nada y ninguna instancia cambió | Mismo motivo que E-VIS-03. Es la condición esperable cuando el anfitrión avisa un cambio de tamaño después de haber destruido la instancia | Dar de baja el mecanismo con el que el anfitrión detecta cambios de tamaño **en el mismo momento** en que invoca `destruir`. No es un error a mostrar: es un aviso llegado tarde, y el anfitrión lo descarta en silencio |
| **E-VIS-06** | `INSTANCIA_DESCONOCIDA` | Único, en cinco funciones | Conflicto de ciclo de vida | `destruir` | No se liberó nada. Ninguna instancia viva se alteró | Se destruyó dos veces el mismo identificador, o se destruyó uno que nunca existió | Nada que corregir en la escena: destruir dos veces **no rompe nada**. Sí conviene revisar el ciclo de vida de la vista, porque una doble destrucción suele indicar dos caminos de salida que no se conocen entre sí |
| **E-VIS-13** | `INSTANCIA_DESCONOCIDA` | Único, en cinco funciones | Conflicto de ciclo de vida | `establecerMovimiento` | No se prendió ni se apagó ningún movimiento, **no se devolvió estado efectivo** y ninguna instancia cambió | Mismo motivo que E-VIS-03. Es la condición esperable cuando el control de movimiento del anfitrión sigue vivo en la página después de que la instancia se destruyó, y alguien lo toca | Desactivar el control de movimiento **en el mismo momento** en que se invoca `destruir`, igual que se desactiva la interacción del árbol (E-VIS-04). No mostrarlo como error: es un pedido llegado tarde y el anfitrión lo descarta. Cuando vuelva a haber instancia, el estado del control se sincroniza con el **estado efectivo** que devuelve la primera invocación exitosa, y no con lo que el anfitrión creía recordar |

**Por qué `E-VIS-13` lleva el número más alto y se lee acá.** Nació después, con la sexta función de la fachada, y **los identificadores de entrada no se renumeran**: son estables y se citan desde otras categorías. Es el mismo criterio con el que `Especificacion-Funcional.md` §3.2 deja a `CU-07` con número más alto que el transversal `CU-06` y lo manda leer antes. El orden de esta tabla es de agrupación, no de numeración.

**Y una precisión que esta entrada hace visible: la fachada no recuerda la elección de quien mira.** El anfitrión que se quedó sin instancia **conserva él** la preferencia y vuelve a pedirla cuando haya instancia nueva; la fachada no la guardó (**G-2**) y tampoco la va a deducir consultando la preferencia de movimiento reducido del sistema (**G-3**). Lo único que la fachada devuelve es el **estado efectivo** de una instancia viva.

### 3.3 Condiciones de carga del texto del trabajo

| Entrada | Código | Curso | Categoría | Función | Qué pasó | Por qué pasó | Qué hacer del lado del anfitrión |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **E-VIS-08** | `TEXTO_NO_LEGIBLE` | Único | Entrada inválida | `cargarJson` | No se dibujó ninguna pieza. Lo que había dibujado antes se liberó igual, y la instancia quedó **viva y vacía** | Del texto recibido no se pudo obtener un conjunto de piezas | Dejar la instancia como está y ofrecer cargar otro texto: no hay que destruir ni reinicializar. **No presentar esto como veredicto sobre el trabajo del alumno**: la fachada no valida y no emite ni advertencias ni errores de validación. Quien dictamina si el texto es un trabajo válido, y por qué, es el backend, y su respuesta es la que el anfitrión muestra |
| **E-VIS-09** | `TIPO_NO_DIBUJABLE` | Único, por pieza | Entrada inválida, alcance por pieza | `cargarJson` | La carga fue exitosa. Esa pieza no se dibujó y quedó enumerada en el resultado de dibujo **con su índice**; las demás se dibujaron | La pieza declara un tipo que no está entre los seis dibujables: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado` y `Circulo` | Leer siempre la lista de piezas no dibujadas, aunque la carga haya sido exitosa, y señalar esas piezas en el árbol por su índice: es lo que impide el fallo silencioso. Redactar la explicación como «el visor no dibuja este tipo de pieza», no como «esta pieza está mal»: la fachada no la califica de error del trabajo |
| **E-VIS-10** | `DIMENSION_NO_LEGIBLE` | Único, por pieza | Entrada inválida, alcance por pieza | `cargarJson` | La carga fue exitosa. Esa pieza no se dibujó y quedó enumerada con su índice; las demás se dibujaron | La pieza es de un tipo dibujable pero **no expone** la dimensión necesaria para construir su malla: la clave o el componente del que se lee la medida **está ausente** —por ejemplo, un volumen sin el componente del que se lee su medida—. La causa es siempre la **ausencia**, nunca el valor: **una dimensión presente con valor `0.00` no produce esta condición**, porque el cero es una dimensión legible y esa pieza **se dibuja** (`Definicion-Contrato-De-Fachada.md` §5.3 y §6; `CU-02` FA-05 y CA-08) | Mismo tratamiento que E-VIS-09: señalarla en el árbol por su índice. Si el caso se repite sobre piezas que el backend **sí** interpreta, el problema no es del trabajo sino de la lectura de dimensiones de la fachada, y eso es un defecto a reportar: el contrato exige que la fachada acepte las mismas variantes de clave que el backend, precisamente para que no haya piezas interpretadas que la escena no dibuje. **Una pieza con una dimensión en `0.00` que no aparezca entre las dibujadas es exactamente ese defecto**, y no una entrada de este catálogo: hay que reportarla en lugar de explicarla al anfitrión |

**El cero es una dimensión legible, y por eso `E-VIS-10` habla de ausencia y no de valor.** El escenario `E-6` del intake §20 es una figura plana con una dimensión en `0.00`, y el visualizador previo la perdía sin aviso porque evaluaba la verdad del número en lugar de su presencia. Una pieza que expone la dimensión con valor `0.00` **se dibuja**, aunque la malla resulte visualmente degenerada, y **no queda entre las no dibujadas**: descartarla contradiría ese escenario declarado y vaciaría la garantía G-5, que es el defecto que el producto viene a eliminar. La consecuencia práctica para el anfitrión es que **no hay nada que explicar** en el árbol sobre esa pieza: figura entre las dibujadas como cualquier otra.

### 3.4 Condiciones de selección

| Entrada | Código | Curso | Categoría | Función | Qué pasó | Por qué pasó | Qué hacer del lado del anfitrión |
| --- | --- | --- | --- | --- | --- | --- | --- |
| **E-VIS-11** | `INDICE_FUERA_DE_RANGO` | Único | Entrada inválida | `seleccionarPieza` | No se resaltó nada. La selección que hubiera quedó intacta y la escena no cambió | El índice pedido no corresponde a ninguna pieza dibujada del resultado de dibujo vigente porque no está en el conjunto raíz, o no hay resultado de dibujo vigente porque todavía no se cargó ningún texto | Tomar los índices **del propio resultado de dibujo** de la última carga, y no de una lista anterior. Después de cada `cargarJson`, reconstruir el árbol con los índices nuevos: los del trabajo anterior ya no valen. La fachada no resalta ninguna pieza por aproximación, y eso es deliberado |
| **E-VIS-12** | `INDICE_FUERA_DE_RANGO` | Único | Entrada inválida | `seleccionarPieza` | No se resaltó nada, porque el índice pedido es el de una pieza que el resultado de dibujo enumera como **no dibujada**. La selección vigente se conserva | La pieza existe en el trabajo y tiene índice, pero no tiene malla: quedó fuera por `TIPO_NO_DIBUJABLE` o por `DIMENSION_NO_LEGIBLE` | Explicar en el árbol **por qué** esa pieza no se puede resaltar, usando el código con el que quedó enumerada en el resultado de dibujo, y no dejar el elemento del árbol comportándose como si fuera seleccionable. Es la única entrada del catálogo en la que el anfitrión ya tiene la explicación en la mano antes de invocar |

**Las dos entradas derivan del enunciado literal del contrato.** `Definicion-Contrato-De-Fachada.md` §6 declara que `INDICE_FUERA_DE_RANGO` se produce cuando el índice «no corresponde a ninguna **pieza dibujada** del resultado de dibujo vigente» y que **cubre los dos casos**: el índice que no está en el conjunto raíz (`E-VIS-11`) y el índice de una pieza que el resultado enumera como no dibujada (`E-VIS-12`), que figura en el resultado pero no tiene malla que resaltar. Las dos entradas se derivan de esa fila sin reinterpretarla. Rige igual el mecanismo de contención del catálogo: **un código nuevo sólo puede nacer en 02**, y esta categoría no acuña ninguno.

### 3.5 Cobertura del catálogo contra el contrato

| Código de `Definicion-Contrato-De-Fachada.md` §6 | Entradas de este catálogo | Funciones cubiertas |
| --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | E-VIS-01 | `inicializar` |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | E-VIS-02 (curso **C-1, en creación**), E-VIS-07 (curso **C-2, en ajuste**) | `inicializar`, `redimensionar` |
| `INSTANCIA_DESCONOCIDA` | E-VIS-03, E-VIS-04, E-VIS-05, E-VIS-06, **E-VIS-13** | `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, **`establecerMovimiento`**: las **cinco** funciones que exigen identificador |
| `TEXTO_NO_LEGIBLE` | E-VIS-08 | `cargarJson` |
| `TIPO_NO_DIBUJABLE` | E-VIS-09 | `cargarJson`, por pieza |
| `DIMENSION_NO_LEGIBLE` | E-VIS-10 | `cargarJson`, por pieza |
| `INDICE_FUERA_DE_RANGO` | E-VIS-11, E-VIS-12 | `seleccionarPieza`, en los dos casos que el enunciado del contrato cubre |

**7 de 7 códigos cubiertos, en 13 entradas, y los dos cursos declarados por el contrato con una entrada cada uno.** `inicializar` no aparece con `INSTANCIA_DESCONOCIDA` porque es la única función que se invoca sin identificador de instancia; las otras **cinco** lo exigen y las cinco tienen su entrada.

**El recuento sigue cerrando después de la capacidad F-25 y de su sexta función.** El gobierno del movimiento automático de la escena (`Definicion-Contrato-De-Fachada.md` §5.5) **no emite ninguna condición propia**: ni la órbita de la cámara ni el giro de las figuras acuñan código, y `establecerMovimiento` tampoco (§4.6 y `CU-07` §6). La lista del contrato **sigue cerrada en siete**; lo que creció es este catálogo, de doce a **trece** entradas, porque su unidad es la **función** y no el código. Un movimiento que no arranca porque la instancia no existe es `INSTANCIA_DESCONOCIDA` —`E-VIS-03` a `E-VIS-06` y `E-VIS-13`, según la función— y nada más. Tampoco son condiciones las opciones: **ausentes en `inicializar`** dan el arranque apagado que el contrato declara, y **ausentes en `establecerMovimiento`** dejan cada movimiento no nombrado con el estado que tenía. Un estado de movimiento no admite fallo parcial: o la instancia existe y el estado queda fijado, o la instancia no existe (garantía **G-7**).

## 4. Situaciones que no son entradas de este catálogo

Se declaran acá porque las seis se confunden con condiciones de error, y ninguna lo es.

| Situación | Por qué no es una entrada | Qué hace el anfitrión |
| --- | --- | --- |
| **Conjunto raíz vacío**: el texto trae un conjunto sin ninguna pieza | La carga es exitosa: el resultado de dibujo devuelve 0 piezas dibujadas y 0 no dibujadas (`CU-02` §5 FA-03). No hay condición | Mostrar la escena vacía y el árbol vacío como un estado legítimo, no como un fallo |
| **Limpiar la selección**: se pide que no haya ninguna pieza resaltada | Es un curso normal de `seleccionarPieza` (`CU-03` §5 FA-03), no una condición | Quitar el resaltado en el árbol junto con el de la escena |
| **Petición de red observada** durante el recorrido de integración | No es una condición de la fachada: es una **violación del gate de cero red**, y hace fallar `CU-06`. El umbral es exactamente 0 y no admite excepción | Nada que mostrar: es un defecto bloqueante del archivo de guion. Se verifica por inspección —cero ocurrencias de las tres primitivas de red en el código fuente y en el artefacto generado— y contando peticiones en la pestaña de red durante la interacción (PRODUCT-INTAKE §17.7 P.6 y P.10) |
| **Degradación después de recorrer trabajos** de ida y vuelta | No hay código que la reporte: es un requerimiento no funcional que se mide, no una condición que se informa | Verificar que el anfitrión invoca `destruir` al salir de cada vista. Diez recorridos de ida y vuelta no deben degradar la visualización (`CU-05` CA-04). El umbral vive en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, lugar único de las seis propiedades transversales |
| **El movimiento automático se detuvo solo**: la órbita de la cámara o el giro de las figuras dejan de moverse sin que nadie los apagara | **No es una condición de la fachada** y no lleva código ni entrada de catálogo. Es el comportamiento declarado del gobierno del movimiento (`Definicion-Contrato-De-Fachada.md` §5.5): los dos se detienen mientras la persona **arrastra la cámara** —para no pelearle el control a quien lo tomó— y mientras la **superficie de dibujo no está visible** —para que un movimiento invisible no siga consumiendo recursos—. El tercer caso, el de la **preferencia de movimiento reducido del sistema**, ni siquiera llega a la fachada: es el anfitrión el que la consulta y el que decide no pedir el movimiento, porque consultarla la fachada violaría G-3 | Nada que mostrar como error. El control visible es del anfitrión, y lo que refleja es el **estado efectivo** que devolvió la última invocación de `establecerMovimiento` más la preferencia que el propio anfitrión conserva —la fachada no la guarda (G-2)—; la detención por arrastre o por superficie no visible **no cambia ese estado gobernado**, de modo que el control no se apaga solo. **No acuñar un código para esto**: `Definicion-Contrato-De-Fachada.md` §6 declara la lista cerrada en siete y un código nuevo sólo puede nacer en 02, nunca aguas abajo |
| **Una pieza con una dimensión en `0.00` aparece dibujada** | No es una condición: es el comportamiento correcto. El cero es una dimensión legible y `DIMENSION_NO_LEGIBLE` la produce la **ausencia** de la clave o del componente, nunca el valor (§3.3, `E-VIS-10`) | Presentar esa pieza como cualquier otra pieza dibujada, aunque la malla se vea degenerada. Si el trabajo tiene algo que decir sobre una medida en cero, lo dice el backend con su observación, no el visor |

## 5. Tono y voz

| Regla | Sí | No |
| --- | --- | --- |
| Nombrar el hecho, no juzgar el dato | «El visor no dibuja este tipo de pieza» | «Pieza inválida» |
| No atribuir intención a quien invoca | «Se pidió un índice que no está en el resultado de dibujo vigente» | «Pediste mal el índice» |
| No prometer lo que la fachada no hace | «Cargá otro texto cuando quieras» | «Reintentando automáticamente» |
| Nombrar la pieza por su índice, que es su identidad | «La pieza de índice 2 no se dibujó» | «Una de las figuras no se dibujó» |
| No mezclar vocabularios | «Condición de la fachada» | «Advertencia» o «error de validación», que son del backend |
| Español rioplatense neutro técnico, sin signos de admiración ni disculpas | «No se pudo obtener un conjunto de piezas del texto recibido» | «¡Ups! Perdón, algo salió mal» |

Coherencia con el resto del producto: el tono es el mismo que rige la documentación del laboratorio y el que 11-Documentacion consolida. Lo específico de acá es que **la fachada no habla**: quien redacta la frase final es siempre el anfitrión, y estas reglas son las que el anfitrión aplica al componerla.

## 6. Localización

1. **Los siete códigos no se traducen.** Son identificadores estables del contrato, parte de la superficie pública, y cambiarlos —incluso sólo su forma— es cambio mayor (`Definicion-Contrato-De-Fachada.md` §7). Viajan siempre en su forma declarada.
2. **La fachada no compone texto para personas** y por lo tanto no tiene catálogo de mensajes que localizar. Componer frases dentro del archivo de guion obligaría a que supiera en qué idioma lee alguien, y eso es conocimiento del sistema que `RA-02` le prohíbe.
3. **La localización vive en el anfitrión.** El componente que embebe el archivo de guion traduce cada código a la frase que corresponda. El producto se entrega en un solo idioma —español rioplatense—, y esta separación existe igual: no por previsión de idiomas futuros, sino porque es lo que mantiene al visor sin conocimiento del sistema.
4. **Los identificadores `E-VIS-XX` son documentales.** Sirven para citar una entrada de este catálogo desde otra categoría; no forman parte del retorno de ninguna función y no se muestran a nadie.

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Superficie pública documentada | Las condiciones de las **seis** funciones de la fachada (`Definicion-Contrato-De-Fachada.md` §6), con `establecerMovimiento` incorporada el 2026-08-09 y **sin código nuevo** |
| Rol de intervención | Developer integrador del bundle: cada entrada le dice qué le queda por hacer del lado del anfitrión |
| Necesidad de negocio | `NB-06` §4, en su punto de que los ortoedros no se dibujan y su ausencia no produce ningún mensaje: el catálogo es lo que cierra ese hueco |
| CU origen | `CU-01` §6, `CU-02` §5 y §6, `CU-03` §5 y §6, `CU-04` §6, `CU-05` §6, `CU-07` §5 y §6 —origen de `E-VIS-13`— y `CU-06` §6, en ese orden de lectura |
| Reglas de negocio relevantes | Ninguna. Las condiciones de este catálogo son de contrato, no de dominio (`Especificacion-Funcional.md` §5.2) |
| Wireframes asociados | N/A. Variante DX con cero wireframes |
| US a generar | 06-Backlog-Tecnico: US de retorno de la condición por invocación, US de enumeración por pieza con índice en el resultado de dibujo, y US de consumo del catálogo por el componente anfitrión |
| Tests previstos | 08-Calidad-Y-Pruebas: una prueba por entrada del catálogo, con el efecto sobre la instancia declarado en la columna «qué pasó» como aserción y el curso del contrato como discriminante en `ELEMENTO_DE_DIBUJO_INVALIDO`; gate de cero red por inspección y por conteo de peticiones |
| Propiedades transversales | Las seis se declaran, con su membresía y su umbral, en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, que es el lugar único. Este documento las invoca y **no las re-enumera** |
| Catálogo de diseño aplicado | N/A para la variante DX |
| Validación visual de maqueta | **Ejecutada y aprobada** dentro de la maqueta de `GeometriaFactory-Web`: este proyecto de código no tuvo maqueta propia. De ahí salieron las dos correcciones de esta ronda —el cero como dimensión legible en `E-VIS-10` y la detención propia del movimiento automático como no-entrada de §4— y ninguna de las dos acuñó código |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Catálogo de doce entradas `E-VIS-01` a `E-VIS-12` derivadas de los siete códigos de condición de `Definicion-Contrato-De-Fachada.md` §6, con la acción sugerida siempre del lado del anfitrión; taxonomía de tres categorías presentes y cinco declaradas ausentes con su motivo; distinción de alcance entre invocación completa y pieza suelta; cuatro situaciones que no son entradas del catálogo, incluida la violación del gate de cero red; reglas de tono y política de localización que dejan la composición de frases en el componente anfitrión. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-08**: `E-VIS-07` se mueve de §3.3 «Condiciones de carga del texto del trabajo» —encabezado que no le correspondía, porque su función es `redimensionar` y no `cargarJson`— a §3.1, renombrada «Condiciones del elemento de dibujo y del entorno», con la nota que explica la agrupación y declara que los identificadores de entrada no se renumeran. **H-01, de su lado**: el catálogo suma columna **Curso** alineada con `Definicion-Contrato-De-Fachada.md` §6, y `E-VIS-02` y `E-VIS-07` declaran el curso del que derivan —**C-1 en creación** y **C-2 en ajuste**—; §1, §3.5 y la trazabilidad lo recogen. El total de códigos sigue siendo siete y el de entradas, doce. **Alineación con el enunciado precisado de `INDICE_FUERA_DE_RANGO`**: se retira la nota que declaraba que el contrato no acuñaba código propio para el curso de `CU-03` FA-02, porque §6 ahora lo cubre explícitamente; `E-VIS-11` y `E-VIS-12` pasan a derivar del enunciado literal, sin reinterpretarlo. **H-03**: se califican las dos ocurrencias desnudas de «recorrido» de §4 —«recorrido de integración» y «recorridos de ida y vuelta»—, que es la sección donde los dos sentidos conviven en la misma tabla. **H-02, de su lado**: §1 principio 5, §4 y la trazabilidad remiten a `Especificacion-Funcional.md` §6 como lugar único de la membresía y del umbral de las seis propiedades transversales, sin re-enumerarlas. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código por no tener maqueta propia. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) El cero como dimensión legible**: **`E-VIS-10`** precisa que la causa de `DIMENSION_NO_LEGIBLE` es la **ausencia** de la clave o del componente del que se lee la medida y **nunca el valor**, y declara que una dimensión presente con valor `0.00` **no** produce la condición porque esa pieza se dibuja; su acción sugerida suma que una pieza en `0.00` ausente de las dibujadas es un **defecto a reportar** y no una entrada de este catálogo; **§3.3** suma la nota que ancla la corrección en el escenario `E-6` del intake §20 y en la garantía G-5, con la consecuencia práctica de que el anfitrión no tiene nada que explicar sobre esa pieza. **(b) Capacidad F-25, movimiento automático de la escena**: **§4** pasa de cuatro a **seis** situaciones que no son entradas del catálogo, con «el movimiento se detuvo solo» —por arrastre de la cámara, por superficie no visible o porque el anfitrión no lo pidió al consultar la preferencia de movimiento reducido del sistema— y con «una pieza con una dimensión en `0.00` aparece dibujada»; la primera declara además que el contrato §6 **prohíbe acuñar códigos nuevos aguas abajo**. **§3.5** deja constancia de que el recuento sigue cerrando: **7 de 7 códigos en 12 entradas**, porque ningún movimiento emite condición. La cabecera suma `Definicion-Contrato-De-Fachada.md` §5.5 a la trazabilidad upstream, y la fila de validación visual de maqueta de §7 pasa de prevista a **ejecutada y aprobada**. |
| 1.0 | 2026-08-09 | Alineación con la **sexta función de la fachada**, `establecerMovimiento(id, opciones)`, acuñada por `Definicion-Contrato-De-Fachada.md` §4.6 al cerrar la **Fase B2**, con contrato de uso en el **`CU-07` nuevo** y consolidación en el intake **1.6**. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) `INSTANCIA_DESCONOCIDA` pasa de cuatro a cinco funciones**: nace **`E-VIS-13`**, la entrada del código para `establecerMovimiento`, con su qué pasó —ningún movimiento cambia y **no se devuelve estado efectivo**—, su por qué y su acción del lado del anfitrión —desactivar el control de movimiento en el mismo momento en que se invoca `destruir`, y resincronizarlo después contra el **estado efectivo** de la primera invocación exitosa—. El curso de las cinco entradas del código pasa a decir «Único, en **cinco** funciones» y §3.2 declara por qué son cinco. **(b) Ningún código nuevo y el recuento de códigos no cambia**: siguen siendo **siete** y la lista del contrato sigue cerrada; lo que crece es este catálogo, de **doce a trece** entradas, porque su unidad de catalogación es la **función** y no el código. §1, §3, §3.5 y §7 recogen las dos cifras juntas para que no se lean como una sola. **(c) Numeración**: se declara que `E-VIS-13` lleva el número más alto y se lee dentro de §3.2 porque **los identificadores de entrada no se renumeran**, con el mismo criterio con el que `CU-07` no se renumera antes de `CU-06`. **(d) Frontera bundle/anfitrión**: §3.2 suma la precisión de que la fachada **no recuerda la elección** de quien mira —guardarla violaría G-2— ni la deduce consultando la preferencia de movimiento reducido del sistema —consultarla violaría G-3—, y la situación «el movimiento se detuvo solo» de §4 pasa a decir que la detención por arrastre o por superficie no visible **no cambia el estado gobernado**, de modo que el control del anfitrión no se apaga solo. **(e)** La cabecera suma §4.6 y `CU-07`; §7 pasa a **seis** funciones y declara el orden de lectura de los casos de uso. Las **seis** situaciones que no son entradas del catálogo **no cambian de número**. |
