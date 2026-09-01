# Medición — cuánto cuesta pintar el listado de la comisión

**Producto:** Fábrica de Geometría
**Documento:** Medicion-Pintado-Del-Listado-2026-08-31.md
**Versión:** 1.1
**Fecha:** 2026-08-31
**Instrumento:** [`tools/medicion-pintado-del-listado.sh`](../../../tools/medicion-pintado-del-listado.sh) y su conductor de navegador
**Estado:** **Emitido.** Mide la pieza pública; **no cierra `PT-05`**

---

## 0. El resultado, primero

> **A mil trabajos la pantalla tarda 1,6 s en estar pintada y el documento pesa 1,2 MB. Y a los
> trescientos ya cruzó el umbral que su propio wireframe declara.**
>
> **La mitigación que el diseño previó para exactamente este caso —«esqueleto por fila por encima de
> 400 ms»— no existe, y bajo el modo de render de esta superficie no puede existir.**

| Trabajos | Alumnos | 1ª pintura | Filtro | Documento | vs. los 400 ms del wireframe |
|---:|---:|---:|---:|---:|---|
| 30 | 10 | **96 ms** | 101 ms | 40 KB | holgado |
| 102 | 34 | **361 ms** | 129 ms | 125 KB | **al borde** |
| 300 | 100 | **483 ms** | 97 ms | 358 KB | **cruzado** |
| 1002 | 334 | **1579 ms** | 305 ms | **1,2 MB** | **cuatro veces** |

**El servicio de datos no era el problema, y esto lo confirma por contraste.** A esos mismos mil
trabajos, [la medición del servicio](Medicion-Volumen-De-Comision-2026-08-31.md) da **12,3 ms** y
**257 KB**. La pantalla tarda **128 veces más** y su documento pesa **casi cinco veces más**: **el costo
está en el marcado y en pintarlo, no en obtener el dato.**

---

## 1. Antes de los números: una suposición que era falsa, y era mía

**El informe del servicio, emitido hoy mismo, advertía en su §4 que «pintar un listado grande es uno de
esos actos discretos que viajan por el circuito».** Es falso para esta superficie, y la corrección va
primero porque cambia cómo se leen los números.

`ClassSubmissionList.razor` **no declara `@rendermode`**. La pieza pública registra el modo interactivo y
**sólo ocho componentes lo adoptan**; éste no está entre ellos. Su filtro es un `<form method="get">`, y
el propio código lo dice sin ambigüedad:

> *«los dos viajan por la dirección porque esta superficie es de render estático»*

**Es render estático del servidor: la sesión interactiva no participa.**

**Y la consecuencia es buena.** El comportamiento de esta pantalla ante el volumen **no depende del
transporte**: que el hosting no ofrezca WebSocket y la sesión repliegue a long polling —medido, aceptado
y cerrado como `PT-01.b` en amarillo estable— **no la afecta en absoluto**. Lo medido acá es
**representativo**, salvo la latencia de red hasta el hosting, y **no un piso** como el informe anterior
anunciaba. El §4 de ese informe se corrige en la misma tanda.

---

## 2. Qué se midió, y con qué criterio

**Dos cosas, y no se promedian:**

1. **Primera pintura** — desde que se pide `/entrega-comision` hasta que **los grupos y las filas que la
   siembra dejó están en el documento**. El criterio de «terminó» es **el conteo, no un evento de
   carga**: una página puede estar cargada y el listado a medio pintar, y lo único que significa «el
   docente ya lo ve» es que estén las `N` secciones de alumno y las `3N` filas.
2. **Filtro por alumno** — elegir un alumno y enviar el formulario. Es **una navegación completa**, no
   una actualización parcial.

**Sobre el mismo montaje que la medición del servicio**: dos contenedores, almacén propio y efímero,
puertos propios, siembra por el circuito real del producto —alta, habilitación, provisoria, cambio de
contraseña— y el escenario `E1` como texto de cada trabajo.

**Una limitación que hay que declarar y que esta medición no comparte con la del servicio: es UNA
corrida por corte, no treinta.** La del servicio reporta percentiles sobre treinta repeticiones; ésta
levanta un navegador por corte y mide una vez. **Los números son indicativos de la magnitud y no
percentiles**, y la dispersión se nota —el salto de 96 a 361 ms entre 30 y 102 trabajos es mayor de lo
que la tendencia posterior sugiere—. Para una cifra fina hay que repetir; el instrumento acepta otros
cortes por `GF_MEDICION_CORTES`.

---

## 3. El hallazgo: la mitigación declarada no existe, y no puede existir así

**`Wireframes-Listado-De-La-Comision.md` §5 declara, para esta superficie:**

> *«**Performance percibida.** Es la superficie con más volumen del producto. **Esqueleto por fila por
> encima de 400 ms.** **No hay paginación** […]: el alcance declarado es una comisión»*

**Las tres cosas que esto afirma, contrastadas:**

| Lo que declara | Lo que hay |
|---|---|
| Que por encima de **400 ms** habrá esqueleto por fila | **La medición cruza los 400 ms entre los 100 y los 300 trabajos** |
| Que existe un **esqueleto** | El sistema visual **sí tiene** las clases —`.gf-skeleton`, con su animación, en `app.css`—, y **ninguna página de la pieza pública las usa**. Ni ésta ni ninguna |
| Que no hay paginación, y que **si el volumen resultara mayor el cambio es acotado** | Sigue siendo cierto, y esta medición **no lo discute** |

**Y hay una razón estructural, que es lo que vuelve el hallazgo interesante en lugar de un olvido.** Bajo
**render estático**, el esqueleto **no se puede mostrar**: el componente obtiene los datos en
`OnInitializedAsync` y el servidor entrega **el documento completo o nada**. No hay ningún instante en el
que el navegador tenga la página sin los datos, que es exactamente el instante que un esqueleto ocupa.

**No es imposible en general: es imposible con este modo de render.** La salida existe y es acotada —
`[StreamRendering]`, que permite mandar un marcador y después transmitir los datos **sin volver la
página interactiva**—, y **el producto no la usa en ninguna parte**.

> **Lo que esto significa, dicho sin dramatizar:** el diseño previó el problema del volumen y escribió su
> mitigación. La superficie se construyó con un modo de render que **la vuelve inaplicable**, y nadie
> contrastó las dos decisiones. **A decenas no se nota. A cientos, la pantalla queda en blanco el tiempo
> que tarde.**

---

## 4. Lo otro que la medición muestra

**El marcado multiplica por casi cinco.** El mismo trabajo pesa **258 bytes** en el JSON del servicio y
**≈ 1,2 KB** en el documento de la pantalla. No es un defecto —una tabla agrupada con insignias,
acciones y accesibilidad **cuesta marcado**— pero explica por qué el documento llega a 1,2 MB con mil
trabajos, y **1,2 MB es una descarga real sobre una conexión de facultad**.

~~**El filtro no escala con el total, y es la buena noticia estructural.** Se mantiene entre 97 y 305 ms
en todo el rango, porque **el recorte lo hace el servicio**: filtrar por alumno devuelve los trabajos de
un alumno, no los mil.~~

> **CORREGIDO el 2026-08-31 en la emisión 1.1. La afirmación es FALSA, y era la más consecuente del
> informe.** La levantó el hallazgo `MI-03` de [`Mesa-2026-08-31-B.md`](Mesa-2026-08-31-B.md), y la mesa
> tuvo razón: **la petición filtrada hace DOS llamadas al servicio, y la primera es SIN CRITERIO.**
>
> `ClassSubmissionList.razor` lo dice en tres líneas que hay que leer juntas:
>
> ```text
> :368   var everything = await DataService.ListWorksAsync(token);      <- SIN criterio
> :376   _unfiltered = everything.Value!;
> :385   var filtered  = await DataService.ListWorksAsync(token, student);
> ```
>
> El comentario que precede a la segunda —*«el criterio va al servicio […] no se filtra acá»*— **describe
> correctamente esa llamada y calla la primera**, que es lo que me hizo leer mal. Y el documento filtrado
> **sigue dibujando un `<option>` por cada alumno de la comisión entera** (`:116` sobre `Students`, que
> `:320-326` deriva de `_unfiltered`).
>
> **Y los propios números lo mostraban.** Filtro: **101 · 129 · 97 · 305 ms**. El corte de mil es **tres
> veces** el de treinta, y es justo donde la primera llamada trae 1002 filas y 257 KB y el selector dibuja
> 334 opciones. **Yo leí la palabra «se mantiene» sobre una serie que no se mantiene.**

**Lo que sí se sostiene, acotado.** El filtro **no escala como la primera pintura** —305 ms contra
1579 ms al mismo volumen—, porque lo que se **dibuja** sí es sólo un alumno. Lo que escala es lo que se
**pide**.

**Y la primera llamada no es un defecto: es una necesidad mal descrita.** El selector de alumnos tiene
que ofrecer **la comisión entera**, no los alumnos del filtro vigente, o filtrar sería un camino de ida.
Ese dato sale de algún lado, y hoy sale de traer todo. Lo que estaba mal era el informe, no el código.

> **La consecuencia que este error tenía, y por la que la corrección importa más que el número.** Esta
> frase se convirtió en argumento de diseño en la emisión 1.0 —«le da la razón al diseño que eligió
> agrupación y filtro en lugar de paginación»— y quedó en el control de cambios de un documento de
> `Audit` emitido. **La única evidencia medida que sostenía no paginar se apoyaba en una propiedad que el
> código no tiene.** No se sigue de esto que haya que paginar: se sigue que **esa decisión vuelve a estar
> sin evidencia medida a favor**, que es distinto y es lo que corresponde escribir.
>
> **Segunda consecuencia, sobre el caudal.** Cada acción de filtrar del docente cuesta **dos** peticiones
> a `A-13`, una de ellas de la colección completa. El caudal de 20 pet/min de `A-5` está provisorio y
> `PT-05` lo va a medir en la fase `i`: **si se mide contando acciones del docente en vez de peticiones
> HTTP, el número va a salir a la mitad del que corresponde.**

---

## 5. Qué NO contesta

- **No cierra la suposición de «decenas y no cientos».** Esa es del diseño de la superficie y **sólo el uso real la valida**: `PT-05`, en la fase `i`.
- **No mide desde la facultad.** Le falta la latencia de red y el ancho de banda reales, que son justamente lo que más pesa sobre un documento de 1,2 MB.
- **No propone paginar.** El wireframe declara que la agrupación con filtro es la forma que el negocio pidió, y la medición del filtro **le da la razón**.
- **No es un percentil.** Una corrida por corte; ver §2.

---

## 6. Cómo se reproduce

```bash
bash tools/medicion-pintado-del-listado.sh
```

Variables: `GF_MEDICION_CORTES` (`30 100 300 1000`), `GF_MEDICION_PUERTO_API` (5099),
`GF_MEDICION_PUERTO_WEB` (5098), `GF_MEDICION_CACHE`.

**La caché de paquetes se comparte entre compilar y ejecutar, y no es una optimización**: el manifiesto
de activos estáticos de la pieza pública guarda **rutas absolutas** a la caché, y con dos cachés
distintas el arranque muere con un `DirectoryNotFoundException` sobre `_framework/` que no dice nada de
lo que pasa. Está anotado en el guion.

---

## 7. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.1 | 2026-08-31 | **`U-08` del plan de la mesa: se corrige la afirmación más consecuente de la emisión 1.0, que era falsa.** «El filtro no escala con el total, porque el recorte lo hace el servicio» **no es cierto**: la petición filtrada hace **dos** llamadas y **la primera es sin criterio** —`:368`, `:376`, `:385`—, y el selector sigue dibujando una opción por alumno de la comisión entera. **Los propios números del informe lo mostraban** —101 · 129 · 97 · **305** ms, con el corte de mil al triple del de treinta— y se leyó «se mantiene» sobre una serie que no se mantiene. Lo levantó `MI-03` de [`Mesa-2026-08-31-B.md`](Mesa-2026-08-31-B.md). **Lo que importa no es el número sino lo que la frase sostenía**: se había convertido en argumento de diseño —«le da la razón al diseño que eligió agrupación y filtro en lugar de paginación»— y era **la única evidencia medida a favor de no paginar**. No se sigue que haya que paginar: se sigue que esa decisión **vuelve a estar sin evidencia medida**. Se registra además que **cada acción de filtrar cuesta dos peticiones a `A-13`**, con lo cual `PT-05` medirá mal el caudal si cuenta acciones del docente en vez de peticiones HTTP. Y se aclara que **la primera llamada no es un defecto sino una necesidad mal descrita**: el selector tiene que ofrecer la comisión entera o filtrar sería un camino de ida. | Orquestador SDD |
| 1.0 | 2026-08-31 | Emisión inicial. Mide la **primera pintura** y el **filtro** de `/entrega-comision` en cuatro cortes, de 30 a 1002 trabajos: **96 ms y 40 KB** en el extremo bajo, **1579 ms y 1,2 MB** en el alto. **Corrige una afirmación del informe del servicio emitido hoy**, que advertía que pintar el listado viaja por el circuito: **no es así**, `ClassSubmissionList.razor` es de **render estático** —no declara `@rendermode`, y su filtro es un `<form method="get">`—, de modo que el repliegue a long polling del hosting **no la afecta** y lo medido es representativo, no un piso. **Hallazgo:** el wireframe declara «esqueleto por fila por encima de 400 ms», **la medición cruza los 400 ms entre los 100 y los 300 trabajos**, y el esqueleto **no existe en ninguna página** pese a que el sistema visual trae sus clases; bajo render estático **no puede existir**, porque el servidor entrega el documento completo o nada, y la salida —`[StreamRendering]`— no se usa en el producto. Registra además que **el marcado multiplica por casi cinco** el peso por trabajo respecto del JSON, y que **el filtro no escala con el total** porque el recorte lo hace el servicio, lo que le da la razón al diseño que eligió agrupación y filtro en lugar de paginación. Declara sus límites: **una corrida por corte y no percentiles**, sin latencia de red, y **no cierra `PT-05`**. | Orquestador SDD |
