# Observación — Lo que `ADR-08006` alcanza aguas arriba

**Producto:** Fábrica de Geometría
**Documento:** Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md
**Versión:** 5.0
**Estado:** Reabierta y vuelta a cerrar — **el alcance era de cuatro afirmaciones y no de tres**, y la cuarta apareció el 2026-08-29 al correr los samples
**Fecha:** 2026-08-30
**Autor:** Orquestador SDD
**Instrumento:** `Master-Prompt.md` §9, manejo de ambigüedad: un dato que el producto no puede resolver por su cuenta **se eleva y no se decide**
**Alcanza a:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §20.E-7 y §20.E-8; `Requerimientos-Tecnicos.md` §8.3

---

## 1. Por qué existe este documento

[`../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md`](../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md)
registra una decisión del Product Owner que cambia un contrato declarado: **el visor recibe las
piezas reconstruidas y no el texto del alumno**.

Esa decisión alcanza **tres afirmaciones que viven en documentos del Product Owner**, y este
orquestador **no los edita**: el intake es documento humano y los requerimientos técnicos son su
fuente. Lo que corresponde es enumerar qué queda desalineado, con qué texto exacto, y para qué
decisión.

**Ninguna de las tres invalida la decisión.** Las tres describen propiedades que la decisión cambia,
y la pregunta no es si la decisión está bien —ya está tomada— sino **qué se hace con lo que esas
tres afirmaban**.

## 1bis. La cuarta afirmación, que este barrido no encontró

**Esta observación se cerró en 4.0 con tres afirmaciones enumeradas y sus tres escrituras aplicadas. Faltaba una cuarta, y era una categoría entera.**

`ADR-08006` cambió la fachada del visor el **2026-08-16**. Los §6 de las tres categorías `10-Examples` del visor describían la fachada anterior —un texto que se carga, una estructura que se devuelve— y **siguieron describiéndola**. Este documento **no menciona la categoría 10 ni una vez**: se verificó con `grep`, cero ocurrencias de `10-Examples`, `ejemplo-0` y `samples/`.

**Lo más filoso es que este mismo documento decía la respuesta.** Su §2.2 escribe que la pieza de `E-8` «**no llega al visor**» — que es exactamente lo que el ejemplo de `02-intermedio` contradecía en su §6, y nadie los leyó juntos.

**Cuánto duró.** El ejemplo del visor se emitió el **2026-08-11**, el ADR es del **2026-08-16**, y el primero que volvió a leer ese §6 fue quien lo implementó, el **2026-08-29**. **Dieciocho días**, con la decisión que lo invalidaba adentro.

**Qué se hizo.** Los cuatro documentos —los tres del visor y `ejemplo-03-avanzado-infraestructura`— pasaron a **2.0** el 2026-08-30, con sus §6 alineados y el motivo escrito en cada uno. Los cuatro samples cierran ahora **sin divergencias**.

**Y qué queda, que no es del producto.** El hueco es de método: el framework tiene una **matriz de propagación** para la retroalimentación de maqueta —`Maqueta-Rules.md` §3.6— y **ninguna para un ADR**, de modo que la cobertura del barrido depende de que quien lo hace se acuerde de la categoría. Está reportado como **`Reporte 21`** en `IA.SDD.Documentacion`, junto con la propuesta de un campo que declare **contra qué lista se verificó** el barrido y no sólo qué se encontró.

## 2. Las tres afirmaciones alcanzadas

### 2.1 `§20.E-7` punto 4 — «todo esto ocurre sin backend»

**Qué dice hoy.** «Todo esto ocurre **sin backend**, con el bundle cargado en una página estática y
el JSON pegado a mano: es la propiedad de `tools_json_figure_viewer` que RT §8.3 exige no perder.»

**Qué pasa a ser cierto.** La página estática sigue existiendo y el bundle sigue dibujando sin
backend, pero **lo que se pega ahí es la estructura de piezas y no el texto del alumno**. Pegar el
texto crudo ya no dibuja.

**Lo que el escenario deja de ejercitar, y es lo que importa medir.** `E-7` era el único que cubría
el mapeo de los seis tipos **y la clave `Bases`** desde el lado del bundle. El mapeo de tipos se
conserva; **la tolerancia de claves deja de ser suya**, porque ya no la tiene.

### 2.2 `§20.E-8` — la condición `DIMENSION_NO_LEGIBLE`

**Qué dice hoy.** El punto 2 declara que la pieza del índice 1 «no se dibuja, y el resultado de
`cargarJson` la reporta con índice 1 y código `DIMENSION_NO_LEGIBLE`».

**Qué pasa a ser cierto.** Esa pieza **no llega al visor**: el validador no la reconstruyó y emitió
su error de validación con posición y campo, y el trabajo quedó en `Borrador`. El punto 5 del mismo
escenario —el desenlace del envío— **no cambia y se cumple igual**.

**La condición no se retira del contrato de la fachada**, y el motivo está en su §4.2: sigue
haciendo falta para el caso en que el anfitrión entregue una pieza que la fachada no pueda usar. Lo
que cambia es que **deja de ser el camino normal de este escenario**.

### 2.3 `RT` §8.3 — la propiedad que se pide no perder

**CORRECCIÓN DE LA VERSIÓN 1.0, Y ES SUSTANTIVA.** Las versiones anteriores de esta observación
declararon que `RT` §8.3 era «la única que la decisión contradice **de frente**». **Al leer el texto
completo, es menos que eso y también es otra cosa.** Los dos hallazgos:

**Primero: la fuente ya contemplaba lo decidido.** La tabla de §8.3 declara que el bundle recibe «el
texto **o la estructura** del JSON de figuras, ya obtenida por el front». La estructura **ya estaba
prevista**, de modo que `ADR-08006` no contradice esa fila: **elige entre dos opciones que la fuente
había dejado abiertas.**

**Segundo: hay una fila que sí se invierte, y no estaba enumerada.** La tabla de responsabilidades de
§8.3 asignaba «interpretar el JSON para dibujar» al **bundle**, «con la tolerancia de claves de §6.3
aplicada a la lectura». Esa es la afirmación que la decisión da vuelta, y **la versión 1.0 no la
había visto**: se quedó en la consecuencia 2 y no leyó la tabla de abajo.

**Lo que sí queda alcanzado, entonces, son dos frases y no una propiedad entera:**

| Dónde | Qué decía | Qué pasa a decir |
| --- | --- | --- |
| Consecuencia 2 | «Se abre una página con **un JSON pegado a mano**» | «con **la estructura de piezas** puesta a mano». Probar sin backend **no se pierde**; cambia qué se pega |
| Tabla de responsabilidades | Interpretar para dibujar: **bundle**, con la tolerancia de §6.3 | Interpretar: **backend**. El bundle recibe piezas reconstruidas y **no aplica ninguna tolerancia** |

**`RA-02` no se toca**, y conviene decirlo porque es el encabezado de toda la sección: el bundle
sigue sin conocer el sistema, sin leer configuración y **sin hacer una sola llamada de red**. Está
medido sobre el paquete construido en `Medicion-Puertas-Tecnicas-PT-02-PT-03.md` §2.

## 3. Qué se le pide al Product Owner

| # | Decisión | Estado |
| --- | --- | --- |
| 1 | Qué se hace con `§20.E-7` punto 4 | **TOMADA el 2026-08-16: reescribirlo.** El intake **2.2** lo declara sobre el camino nuevo y agrega qué sigue ejercitando el bundle —el mapeo de los seis tipos— y qué dejó de ser suyo |
| 2 | Qué se hace con `§20.E-8` puntos 2 y 3 | **TOMADA el 2026-08-16: reescribirlos.** El intake **2.2** declara que la pieza no llega al visor y que **la confusión que el escenario detecta cambia de par**: pasa a ser entre no poder dibujar y no poder interpretar. El punto 5 no se tocó |
| 3 | Qué se hace con `RT` §8.3 | **TOMADA Y APLICADA el 2026-08-16**, con autorización explícita del Product Owner para escribir sobre su documento. Se reescriben **las dos frases** de §2.3 y nada más |

### 3.1 La tercera, con lo que su decisión deja pendiente

**`RT` §8.3 pide no perder que cualquiera pegue el texto y vea el dibujo, sin instalar nada.** Con
`ADR-08006` se conservan «sin instalar nada» y «sin backend», y **no se conserva «pegando el
texto»**: la página suelta del visor recibe la estructura de piezas, y quien tenga el texto crudo
necesita al laboratorio para convertirlo.

**Dónde corre el validador ya está decidido y no cambia esto.** El 2026-08-16 el Product Owner
resolvió que corre **en el servicio de datos**, de modo que la pantalla le pide la interpretación
por `A-18` y le pasa el DTO al bundle. Esa decisión define **quién** interpreta; la de `RT` §8.3
define **qué se hace con una propiedad que ya no se cumple entera**, y son preguntas distintas.

**Lo decidido.** Se **precisa la propiedad**: «sin instalar nada y sin backend», sin «pegando el
texto». La página de prueba del visor se entrega con los ocho escenarios **ya convertidos**, de modo
que sigue abriéndose y dibujando sin levantar nada; lo que se pierde es pegar texto nuevo sin el
laboratorio. La alternativa —sostenerla como estaba— habría obligado al bundle a seguir
interpretando, y con eso `ADR-08006` no procedía.

**La escritura se aplicó, y con autorización explícita.** `Requerimientos-Tecnicos.md` **no vive en
el árbol de este producto**: está en `Lab-Geometria.Documentacion/PROMPTs/`, que es material de su
autor, donde este orquestador lee y no escribe. El Product Owner **autorizó expresamente** esta
escritura el 2026-08-16, acotada a las dos frases de §2.3. **La autorización es de este caso y no
cambia la regla**: la carpeta sigue siendo suya.

**La tercera es la que conviene mirar primero.** Las dos primeras son ajustes de redacción sobre
escenarios; la tercera es una propiedad que el análisis declaró **como cosa a no perder**, y la
decisión la pierde en parte. Puede ser un costo aceptable —el Product Owner ya lo aceptó al decidir—
pero merece quedar aceptado **sobre este texto** y no sólo sobre el resumen.

## 4. Estado final

**Las tres decisiones tomadas y las tres escrituras aplicadas.** El intake está en **2.2**, `RT`
§8.3 lleva sus dos frases reescritas, y la construcción está hecha: la firma nueva, `A-18`, la capa
3 del visor y el bloque de previsualización.

**Esta observación queda cerrada.** Lo que deja como enseñanza, y por eso no se borra: **la versión
1.0 declaró un conflicto más grande del que había, por leer una consecuencia y no la tabla de al
lado del mismo apartado**. El barrido por concepto que `SDD-Development-Guide.md` §VI.3.1 pide —
enumerar el término en todo el árbol, **incluido el interior de los archivos ya tocados**— es
exactamente lo que faltó, y es la regla que el framework escribió después de tropezar tres veces con
lo mismo.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Eleva las **tres afirmaciones aguas arriba** que `ADR-08006` alcanza —`§20.E-7` punto 4, `§20.E-8` puntos 2 y 3, y `RT` §8.3—, con el texto exacto de cada una, qué pasa a ser cierto, y la decisión que se le pide al Product Owner sobre cada una. Declara que **ninguna invalida la decisión** y que **la construcción no está bloqueada**, y señala que la tercera es la única que la decisión contradice de frente. | Orquestador SDD |
| 2.0 | 2026-08-16 | **Dos de las tres decisiones tomadas.** El Product Owner resolvió **reescribir** los dos puntos de los escenarios, y el intake pasa a **2.2** con esa absorción. Queda **abierta la tercera**, `RT` §8.3, que es la única que la decisión contradice de frente, y §3.1 la desarrolla con sus dos salidas y lo que cuesta cada una. Se declara además que la decisión de **dónde corre el validador** —en el servicio de datos— **no resuelve ésta**: una dice quién interpreta y la otra qué se hace con una propiedad que ya no se cumple entera. | Orquestador SDD |
| 3.0 | 2026-08-16 | **La tercera decisión, tomada: se precisa `RT` §8.3.** La propiedad pasa a ser «sin instalar nada y sin backend», sin «pegando el texto», y la página de prueba del visor se entrega con los ocho escenarios ya convertidos. **Queda una escritura pendiente que no es de este orquestador**: `Requerimientos-Tecnicos.md` vive en `Lab-Geometria.Documentacion/PROMPTs/`, material de su autor, donde este orquestador lee y no escribe. La decisión está registrada acá; falta que la fuente la refleje. | Product Owner (decisión) · Orquestador SDD |
| 4.0 | 2026-08-16 | **Cerrada: las tres escrituras aplicadas.** `RT` §8.3 se reescribe en sus dos frases, con autorización explícita del Product Owner sobre su propia carpeta —autorización **de este caso**, que no cambia la regla—. **Y §2.3 se corrige a sí misma, que es lo sustantivo de esta versión**: la 1.0 declaró que `RT` §8.3 contradecía la decisión «de frente», y al leer el texto completo son dos hallazgos distintos y menores. La fuente **ya contemplaba** que el bundle recibiera «el texto **o la estructura**», de modo que la decisión elige entre dos opciones abiertas en lugar de contradecir; y **hay una fila que sí se invierte y que la 1.0 no había visto**: la tabla de responsabilidades asignaba interpretar al bundle. Se declara la causa del error —leer una consecuencia y no la tabla de al lado, que es lo que el barrido por concepto existe para evitar— en lugar de corregir la cifra en silencio. | Orquestador SDD |
| 5.0 | 2026-08-30 | **El alcance era de cuatro afirmaciones y no de tres.** Entra §1bis: los §6 de las tres categorías `10-Examples` del visor —y el de `03-avanzado-infraestructura`— describían la fachada anterior a `ADR-08006`, y **este documento no menciona la categoría 10 ni una vez** (verificado con `grep`: cero ocurrencias). Su propio §2.2 escribía que la pieza de `E-8` «no llega al visor», que es lo que el ejemplo contradecía, y nadie los leyó juntos. Duró **dieciocho días**, hasta que alguien corrió el sample. Los cuatro documentos pasaron a 2.0 y sus cuatro samples cierran **sin divergencias**. El hueco de método —que no hay matriz de propagación para un ADR, como sí la hay para la maqueta— está reportado al framework como **`Reporte 21`**. Sube **major**: el alcance de la observación cambia, y una observación cerrada que enumeraba tres pasa a enumerar cuatro. |
