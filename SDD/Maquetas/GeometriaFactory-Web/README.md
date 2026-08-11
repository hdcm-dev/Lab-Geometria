# Maqueta de validación visual — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Tipo:** `web-monolith`
**Fase:** B2 — Validación visual de maqueta
**Modelo UX-UI aplicado:** catálogo base de `References/Design/` — `Design-Rules-Web-Generico` +
`Design-Rules-Primer-Arranque` + `Design-Rules-Identidad-De-Version`
**Fecha de la iteración:** 2026-08-09
**Estado:** **Aprobada** por el Product Owner el 2026-08-11. La aprobación es un acto humano por diseño (`Master-Prompt.md` §15) y por eso la promoción documental del mismo día la dejó expresamente fuera. Se aprueba **con sus tres huecos declarados**: la sexta función de la fachada, el reseteo de contraseña y la provisoria al habilitar no fueron validados visualmente, y su vía es una iteración 5

---

## 1. Qué es y qué no es

Es una **maqueta navegable** para que el docente —Product Owner del producto— vea el producto antes
de que exista. Se valida mirando, no leyendo.

- **No es el producto.** No hay backend y no hay servicio de datos. El **visor tridimensional sí es
  real** desde la iteración 3: es el visualizador que la cátedra ya usa, portado y corregido (§6).
- **No es documentación viva.** Es la línea de base de un momento. Lo que vive es la especificación
  de la categoría 03, que se retroalimenta después de que la maqueta se aprueba.
- **No hay proceso de build.** Lo que se edita es lo que se sirve: HTML, CSS y JavaScript planos, con
  Bootstrap 5 por CDN. Podés corregirla a mano con cualquier editor de texto.
- **Ninguna llamada de red a servicios reales.** La maqueta es autónoma. La única petición que emite
  es la de la recarga automática, cuando la encendés, y va contra sus propios archivos.

---

## 2. Cómo se abre

Cuatro caminos, todos sirven exactamente los mismos archivos.

### Servidor estático de línea de comandos (el más simple)

```bash
cd /home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria/SDD/Maquetas/GeometriaFactory-Web
python3 -m http.server 8080
```

y abrir <http://localhost:8080> en el navegador.

### Servidor liviano del editor (el recomendado si vas a corregirla a mano)

En Visual Studio Code, la extensión **Live Server** o equivalente: botón derecho sobre `index.html`
→ *Open with Live Server*. Recarga el navegador sola en cada guardado, así que el ciclo de editar,
guardar y ver no tiene paso intermedio.

### Recarga automática de la propia maqueta

Si no usás la extensión del editor, encendé **«Recarga automática»** en la barra de validación del
pie. Consulta los archivos cada 3 segundos y refresca cuando alguno cambia. Está **apagada por
omisión** y su estado queda recordado en el navegador.

### Abrir el archivo directamente

`index.html` con doble clic. Sirve para una mirada rápida. Sobre `file://` la recarga automática no
puede funcionar: el interruptor aparece deshabilitado con la razón a la vista, y nada más falla.

---

## 3. Cómo se usa la barra de validación

La franja del pie, con borde punteado, rotulada
**«Barra de validación de maqueta — no forma parte del producto»**. Es un instrumento de la maqueta:
no se traslada ni a la especificación ni al código.

| Control | Qué hace |
| --- | --- |
| Selector de estado | Cambia el estado de la superficie en curso sin recargar ni tocar código. Están todos los estados que declara la sección 5 del wireframe de esa superficie. |
| Recarga automática | Apagada por omisión. Encendida, refresca la página cuando cambia un archivo de la maqueta. |
| Volver a la portada | `index.html`, con el índice de las once superficies y el contrato de campos. |

El estado elegido queda recordado por superficie durante la sesión del navegador, y se puede fijar
por dirección: `Ingreso.html?estado=sesion-cerrada`.

---

## 4. Las once superficies

Una por wireframe de `SDD/Docs/Proyectos/GeometriaFactory-Web/03-UX-UI-DX/`, con el nombre canónico
declarado en la sección 1 de cada uno.

| Archivo | Superficie | CU origen | Shell |
| --- | --- | --- | --- |
| `Aprovisionamiento-Inicial.html` | `Aprovisionamiento-Inicial` | CU-04 FA-03 y FA-04 | Acceso |
| `Registro-De-Cuenta.html` | `Registro-De-Cuenta` | CU-01 | Acceso |
| `Ingreso.html` | `Ingreso` | CU-02 | Acceso |
| `Credencial-Propia.html` | `Credencial-Propia` | CU-03 | Acceso (establecimiento) / Trabajo (cambio) |
| `Panel-De-Trabajos-Del-Alumno.html` | `Panel-De-Trabajos-Del-Alumno` | CU-06 | Trabajo |
| `Envio-De-Trabajo.html` | `Envio-De-Trabajo` | CU-05 | Trabajo |
| `Vista-De-Trabajo.html` | `Vista-De-Trabajo` | CU-07 | Trabajo |
| `Resolucion-Del-Trabajo.html` | `Resolucion-Del-Trabajo` | CU-09 | Trabajo, **alojada dentro de** `Vista-De-Trabajo` |
| `Panel-De-Cuentas.html` | `Panel-De-Cuentas` | CU-04 flujo principal, FA-01, FA-02, FA-05 | Trabajo |
| `Listado-De-La-Comision.html` | `Listado-De-La-Comision` | CU-08 | Trabajo |
| `Estado-Degradado-Y-Reconexion.html` | `Estado-Degradado-Y-Reconexion` | CU-10 | Los dos, por superposición |

### La resolución del trabajo no tiene ruta propia

`Wireframes-Resolucion-Del-Trabajo.md` §1 lo dice: es un **bloque alojado dentro de
`Vista-De-Trabajo`**, debajo de la cabecera del trabajo y **antes** de las cuatro partes, que aparece
sólo para el administrador y sólo mientras el trabajo está en estado `Pendiente`. La maqueta lo
materializa así:

- **`Vista-De-Trabajo.html` dibuja el bloque** cuando se la abre con `papel=administrador` sobre un
  trabajo `Pendiente`. Es el camino del producto: entrega de la comisión → abrir un trabajo.
- **`Resolucion-Del-Trabajo.html` sigue existiendo** porque el wireframe declara que la maqueta tiene
  que demostrar ese bloque **como recorrido**, con su mapa de estados propio. Ahora muestra el
  trabajo completo —datos, comentario, observaciones, texto, escena y árbol— debajo del bloque: el
  docente lee el trabajo y decide en el mismo lugar, sin saltar a otra superficie.
- **Las cuatro partes y el bloque de decisión se arman una sola vez**, en `Maqueta.js`
  (`pintarCuatroPartes` y `pintarBloqueDeDecision`). Las dos superficies llaman a la misma función,
  para que una corrección no haya que hacerla dos veces.

---

## 4.b Cómo se navega

Los caminos son los de `Experiencia-De-Uso.md` §3.3 a §3.7. La maqueta lleva el contexto entre
superficies por la dirección, con el mismo mecanismo que usa para fijar estados:

| Parámetro | Qué lleva |
| --- | --- |
| `?t=<id>` | Qué trabajo se abre |
| `?papel=alumno` / `?papel=administrador` | Con qué papel se lo mira: decide la barra lateral y si aparece el bloque de decisión |
| `?desde=comision` / `?desde=mis-trabajos` / `?desde=envio` | De dónde vino la persona, para que «volver» vuelva ahí |
| `?estado=<id>` | Con qué estado se abre la superficie |

Recorridos que funcionan de punta a punta:

- **Entrega de la comisión** → *Abrir* → vista del trabajo **como administrador**, con el bloque de
  decisión si está en estado `Pendiente` → *Aprobar* o *Rechazar* → diálogo con la terminalidad
  declarada → vuelve al listado de la comisión. *Retirar* sigue el mismo camino, con su propio diálogo.
- **Mis trabajos** → *Abrir* → vista del trabajo **como alumno**, sin bloque de decisión y con el
  comentario del docente si el trabajo lo tiene → *Volver a mis trabajos*.
- **Mis trabajos** → *Editar* sobre un borrador → envío en su **curso de reedición**, con los datos y
  el texto tal como quedaron.
- **Trabajo nuevo** → envío en su curso de creación → *Enviar* → bloque de resultado → *Ver el
  trabajo* o *Volver a mis trabajos*, que es donde el trabajo ya figura.
- **Barra lateral** → *Mi contraseña* abre el **curso de cambio** conservando el papel; *Cancelar*
  vuelve al panel de la persona.
- **Aprovisionamiento** → *Ingreso* con su banda de confirmación; **Registro** → *Ingreso*; cierre de
  sesión → *Ingreso* con su banda.

Ningún camino se inventó: los once destinos son los que declara `Experiencia-De-Uso.md` §3.1, y los
tres destinos por papel de la barra lateral son los de §3.2.

---

## 5. De dónde salen los datos

**Fuente única: `assets/js/Datos-Maqueta.js`.** Ningún HTML hardcodea datos: los renderiza el
JavaScript desde ahí. Si querés cambiar un dato, cambialo ahí una vez y cambia en todas las
superficies.

| Conjunto | Origen |
| --- | --- |
| Texto semilla del trabajo `Cubo y ortoedro` | `PRODUCT-INTAKE` §20.E-1, transcrito carácter por carácter |
| Texto del borrador con error de validación | `PRODUCT-INTAKE` §20.E-5 |
| Texto del trabajo `Entrega 1`, sin observaciones | `PRODUCT-INTAKE` §20.E-4 |
| Texto del trabajo `Primer intento`, con advertencia de área | `PRODUCT-INTAKE` §20.E-3 |
| Texto del trabajo `Segundo intento` | `PRODUCT-INTAKE` §20.E-6 |
| Texto de partida del formulario de envío | `PRODUCT-INTAKE` §20.E-2 |
| Nombres, fechas y estados de los trabajos | `Wireframes-Panel-De-Trabajos-Del-Alumno.md` §2 y `Wireframes-Listado-De-La-Comision.md` §2 |
| Cuentas, correos y fechas de registro | `Wireframes-Panel-De-Cuentas.md` §2 |
| Comentario del rechazo | `Wireframes-Resolucion-Del-Trabajo.md` §2 |
| Sello de versión y su diagnóstico | `Representacion-Sello-De-Version.md` §2 |
| Toda la microcopy | Los §2 y §3 de los once wireframes |

### Los cuatro valores compuestos para la maqueta

La documentación no los declara y el Product Owner autorizó componerlos para que la maqueta sea
recorrible. Están marcados con `origen: 'compuesto-para-la-maqueta'` en `Datos-Maqueta.js` y **no
deben trasladarse a la especificación sin una decisión explícita**:

1. **La credencial de la cuenta de administrador de prueba** — `docente@ej.test` / `laboratorio`.
   Es un instrumento de la maqueta, exhibido a la vista en `index.html` y en `Ingreso.html`.
2. **La cuarta cuenta de alumno, `Dario Nuñez` / `dario@ej.test`, en situación `Pendiente`.** Los
   wireframes declaran tres cuentas de alumno, y hacen falta cuatro para que las tres situaciones de
   cuenta convivan con dos alumnos que además tienen trabajos.
3. **El nombre del quinto trabajo, `Segundo intento`.** Hace falta un segundo rechazo para mostrar el
   rechazo **sin** comentario, que el Product Owner pidió expresamente; los wireframes nombran cuatro
   trabajos.
4. **El texto de `E-7` sin el `Radio` del círculo**, para demostrar la condición
   `DIMENSION_NO_LEGIBLE` de la fachada del visor. Es el **único** de los siete códigos del contrato
   que no tiene escenario propio en el intake §20 ni fila en su §21: se compone quitando una clave de
   una pieza documentada. Ver el hallazgo H-5 de la devolución.

### Lo que la maqueta no puede contradecir

- El texto semilla produce **exactamente 3 piezas y 2 advertencias**.
- El **cilindro no produce ninguna observación**: declara `113.10` y sus componentes suman `113.09`,
  con diferencia de exactamente `0.01`. El operador de tolerancia es **estricto** —advierte si la
  diferencia es *mayor* que `0.01`—, así que ese caso no advierte. Una maqueta con tres advertencias
  contradiría el caso de prueba canónico del producto.
- Los valores **declarado** y **derivado** se muestran **sin reformatear**, tal como el texto los
  trae y como el sistema los recalcula.
- El **comentario** del administrador no es una observación ni una calificación.
- `Pendiente` nombra **dos estados distintos**: el de la cuenta y el del trabajo. En la superficie de
  cuentas la columna se llama **«situación»** justamente para no colisionar.

---

## 6. El área de dibujo y el árbol

**El área de dibujo monta el visor real.** `assets/js/Visor-Tridimensional.js` es el port del
visualizador que la cátedra ya usa (`tools_json_figure_viewer/js/visor.js`), con Three.js r128 por
red de distribución, igual que el framework de grilla: sin compilación y sin gestor de paquetes. Las
cinco funciones de la fachada operan sobre esa escena de verdad.

### Qué se absorbió del visor original

- La construcción de los objetos tridimensionales y sus funciones de creación: cilindro, cubo,
  ortoedro y las tres formas planas.
- La escena con sus luces —ambiental, direccional con sombra y puntual—, su grilla de referencia y su
  **cámara orbital reimplementada a mano**, porque r128 no trae control orbital.
- El **árbol colapsable**, que la documentación llama «el mejor recurso didáctico del visor»: el
  recorrido de la estructura entera, clave por clave y valor por valor, hasta la hoja.

### Qué NO se absorbió, y es lo importante

| Defecto del visor original | Qué hace el port |
| --- | --- |
| **Disposición al azar.** `Math.random()` en las líneas 209, 210, 232, 233, 246, 247, 276 y 277, más el barajado de objetos y de puntos de grilla | La celda de cada pieza se **deriva de su índice** en el conjunto raíz. Dos procesados del mismo texto dan la misma disposición, comparable pieza por pieza. La separación conserva la idea buena del original: el radio de envoltura máximo más un margen |
| **Intolerancia de claves.** Exige `Bases` en el ortoedro cuando el programa del alumno emite `Tapas`, y por eso hoy **ningún ortoedro generado por la aplicación se dibuja** | Se toleran `Bases` y `Tapas`, `Cuadrado` y `Rectangulo` en las caras, comas finales y comentarios de línea y de bloque. Además el **cero es una dimensión legible**: la figura con una dimensión en `0.00` no se descarta (intake §20.E-6), que el original perdía por evaluar la verdad del número |
| **Fallo silencioso.** La figura que no se puede dibujar simplemente no aparece | Toda pieza no dibujada queda enumerada con su índice y su código —`TIPO_NO_DIBUJABLE` o `DIMENSION_NO_LEGIBLE`—, en la lista al lado de la escena y marcada en el árbol, con el recuento **«sin registro»** que verifica que ninguna quede afuera |

**Lo que la documentación declara que no se porta** (intake §17.7 P.2) y efectivamente no está: las
cinco variantes comentadas del procesamiento del arreglo, las dos variantes de posición aleatoria, la
función de cilindro sin uso (`updateCylinder`, que lee dos deslizadores inexistentes), los dos
manejadores cableados sobre funciones que nunca se definieron (`toggleWireframe` y `centerObjects`) y
las bibliotecas cargadas sin usar (jQuery, Popper y el JavaScript del framework de grilla).

**El visor no hace red, no lee configuración y no sabe quién es el usuario.** No hay `fetch`, ni
`XHR`, ni carga de texturas, fuentes o imágenes; no toca el almacenamiento del navegador ni los
parámetros de la dirección; no recibe ni consulta identidad ninguna. Su umbral de peticiones es
exactamente cero, y el panel lo mide.

### Los dos movimientos automáticos, y su control

Al pie del área de dibujo hay **dos casillas de verificación independientes**. Se tildan por separado
y se pueden tildar las dos a la vez. Son preferencias de quien mira la escena, no instrumento de
validación, y por eso viven junto al dibujo y no en el panel del contrato.

| Casilla | Qué hace | Por omisión |
| --- | --- | --- |
| **Órbita de la cámara** | La cámara gira sola alrededor del conjunto; las piezas quedan quietas. Es el giro del visor original: incremento lento del ángulo horizontal cuando no hay arrastre y hay piezas en la escena | **Tildada** |
| **Giro de las figuras** | Cada pieza rota sobre su eje vertical, en su lugar | **Tildada** |

- Los dos **se detienen mientras la persona arrastra** la cámara, como en el original, y también con
  la pestaña oculta.
- Si el sistema declara **preferencia de movimiento reducido**, los dos arrancan **destildados** y el
  propio control lo dice: «arrancan apagados: tu sistema pide movimiento reducido». **Quien consulta
  `prefers-reduced-motion` es la página, no el visor**: ver la frontera más abajo.
- El estado se **persiste**, con las claves `mq-orbita-de-camara` y `mq-giro-de-figuras`. El prefijo
  `mq-` no es decorativo: el panel mide «cero persistencia» contando las claves que **no** empiezan
  con `mq-`, así que una clave sin prefijo haría fallar esa medición.
- La interacción con el mouse no cambia: **arrastrar** gira la cámara, **la rueda** acerca y aleja.
  La rueda **sólo se captura cuando el lienzo tiene el foco**: si no, la página se desplaza normal.
- Con el lienzo enfocado, el teclado hace lo mismo: **flechas** para orbitar, **`+`** y **`-`** para
  acercar y alejar, **Inicio** para volver a la vista de partida.
- **Ninguno de los dos afecta el determinismo**, que es de la **posición** y no de la orientación:
  cada pieza sigue cayendo en la celda que le da su índice. Verificado en las cuatro combinaciones.

> **Hallazgo H-6, para el paso 6 de retroalimentación.** El **giro de las figuras no existe en el
> visor original**: el original mueve la cámara y deja las piezas quietas. Es **comportamiento no
> declarado en la especificación, incorporado por decisión del Product Owner durante la validación
> visual**, y queda **pendiente de propagar a la documentación de la categoría 03 del proyecto de
> código del visor y a su contrato de fachada**. La órbita de la cámara, en cambio, sí está en el
> original y sólo se le agregó el control para prenderla y apagarla.

> **La frontera entre el bundle y el anfitrión, tal como la fija el contrato.**
> `Definicion-Contrato-De-Fachada.md` §3.3 y §4.1 son explícitos y la maqueta los cumple: el visor
> **no dibuja el control, no conserva la elección y no consulta configuración propia** —hacerlo
> violaría la garantía G-3—, y con las opciones **ausentes o parciales arranca con los dos
> movimientos apagados**. Quien lee `prefers-reduced-motion` es **esta página**, que es el componente
> anfitrión: lo traduce a **dos valores de verdad**, uno por movimiento, y se los pasa al bundle por
> `crear(elemento, opciones)` y después por `orbitar()` y `girarFiguras()`. El bundle no expone
> ninguna función que consulte el entorno.
>
> Hasta la ronda de corrección de la auditoría, la maqueta hacía lo contrario en las dos cosas: el
> bundle traía `prefiereMovimientoReducido()` y las opciones ausentes lo hacían nacer **prendido**.
> Quedó corregido, y la corrección **no afloja el contrato**: lo aplica.

### Representación de respaldo

Si el navegador no ofrece capacidad gráfica tridimensional, el área de dibujo cae a la
**representación plana** que la maqueta tenía antes —un SVG con una silueta por pieza sobre una línea
de suelo— y se rotula como respaldo, con el motivo a la vista. No es el camino normal.

### La sincronización entre el árbol y la selección

- Elegí un nodo de pieza del árbol: la escena **resalta la malla real** de esa pieza y sólo esa.
- Pinchá una pieza en la escena: el nodo correspondiente queda marcado, **por el mismo índice**.
- La flecha del nodo despliega la rama; el resto del botón es la selección. Las ramas arrancan
  colapsadas: el original abría todo, y con seis piezas de cuatro niveles el árbol dejaba de servir.

- El recorrido por teclado va por el árbol —flechas arriba y abajo entre piezas, derecha e izquierda
  para desplegar y plegar— y el cambio de selección se anuncia como región activa.
- El estado **«Índice sin representación»** demuestra el caso del nodo cuya pieza no se dibujó: la
  selección vigente se conserva, se declara que no tiene representación en la escena y el nodo sigue
  siendo navegable.

---

## 6.b El contrato de fachada del visor, validado acá dentro

`GeometriaFactory-Visor` también tenía `requiere_maqueta` en true. Por decisión del Product Owner
**no tiene maqueta propia**: su validación se integra en ésta, dentro de las superficies que lo usan.
El fundamento es del propio contrato: el visor **no tiene pantallas propias**, su superficie pública
son cinco funciones planas que un componente anfitrión invoca, y validarlo aislado mostraría una
página de prueba que ya existe como sample S-1.

El **panel del contrato de fachada** vive en la columna de escena y árbol de `Vista-De-Trabajo` y de
`Resolucion-Del-Trabajo`, debajo del árbol, con borde punteado y el rótulo
«instrumento de validación, no forma parte del producto». En el producto real el componente anfitrión
invoca las cinco funciones sin exhibirlas: este panel existe para que el contrato se pueda juzgar.

> **El contrato tiene seis funciones y este panel demuestra cinco.** La sexta,
> `establecerMovimiento(id, opciones)`, se decidió **después** de que el Product Owner aprobó la
> validación visual, y **no fue validada visualmente**. En la maqueta el gobierno del movimiento se
> resuelve con dos métodos de instancia —`orbitar(v)` y `girarFiguras(v)`—, que hacen lo mismo pero
> no son la sexta función plana del contrato. Está declarado así, con su fecha, en
> `Linea-Base-Visual.md` §6 y en la sonda `SD-43`, para que nadie lea el panel como si fuera el
> inventario completo de la superficie pública.

**Qué hace visible:**

| Qué | Cómo se ve |
| --- | --- |
| **Cinco de las seis funciones** | Un botón por función: `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`. Cada invocación cambia lo que se ve y queda anotada en la bitácora con su resultado. **`establecerMovimiento` no tiene botón**: se decidió después de la aprobación y no se validó visualmente |
| El **ciclo de vida** | Una insignia con `Inexistente` → `Viva` → `Liberada`, y el identificador de instancia. Antes de inicializar no hay identificador; después de inicializar hay instancia viva **sin ninguna pieza dibujada**; al destruir, la escena queda vacía y el identificador deja de ser válido |
| El **resultado de dibujo** | Cuatro recuentos: conjunto raíz, dibujadas, no dibujadas enumeradas y **sin registro**. El último tiene que ser siempre 0 |
| Las **siete condiciones** | Una tabla desplegable con código, curso, cuándo y efecto, y un estado en la barra de validación por cada una |
| Las **seis propiedades transversales** | Una lista con su umbral y lo que la maqueta mide de verdad en ese momento |
| Qué garantiza cada función | Un desplegable con la semántica de las cinco, referida a su caso de uso |

**Los textos que el panel puede cargar** salen todos del intake §20: `E-1` (tres piezas), `E-7` (los
seis tipos dibujables), `E-2` (el ortoedro con clave «Tapas» y comas finales, que es con lo que se
comprueba la tolerancia de claves), `E-5` (con una pieza de tipo no dibujable), `E-7` sin la
dimensión del círculo, y un texto del que no se obtienen piezas. **El texto es la entrada real**: el
visor lo interpreta con su lector tolerante, y de ahí salen la escena, el árbol y la lista de piezas
no dibujadas. Los árboles declarados de `Datos-Maqueta.js` quedan como respaldo y como contraste.

**Las siete condiciones y con qué estado se alterna cada una**, desde la barra de validación de
`Vista-De-Trabajo`:

| Código | Curso | Estado de la barra |
| --- | --- | --- |
| `CAPACIDAD_GRAFICA_AUSENTE` | Único | Escena no disponible |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | C-1, en creación | Elemento de dibujo sin tamaño, en creación |
| `ELEMENTO_DE_DIBUJO_INVALIDO` | C-2, en ajuste | Elemento de dibujo sin tamaño, en ajuste |
| `INSTANCIA_DESCONOCIDA` | Único, en cuatro funciones | Instancia desconocida |
| `TEXTO_NO_LEGIBLE` | Único | Texto no legible |
| `TIPO_NO_DIBUJABLE` | Único, por pieza | Piezas no dibujadas |
| `DIMENSION_NO_LEGIBLE` | Único, por pieza | Pieza sin dimensión legible |
| `INDICE_FUERA_DE_RANGO` | Único | Índice sin representación en la escena |

Son ocho filas y **siete códigos**: `ELEMENTO_DE_DIBUJO_INVALIDO` tiene dos cursos y no dos códigos.
Los dos últimos cursos —el de ajuste y el de instancia desconocida— **no le avisan nada a la
persona**, que es lo que el contrato manda: se observan sólo en la bitácora del panel.

**Las dos comprobaciones que se corren a mano**, con sus botones en el panel:

- **«Volver a cargar el mismo texto y comparar»** ejecuta `cargarJson` dos veces con el mismo texto y
  compara las dos disposiciones pieza por pieza. La ubicación de cada pieza se deriva de su **índice**
  en el conjunto raíz, no del orden de llegada: por eso el veredicto es «idéntica pieza por pieza».
- **«Hacer 10 recorridos de ida y vuelta»** ejecuta diez veces el ciclo completo —inicializar,
  cargar, seleccionar, redimensionar, destruir— y reporta cuántas instancias quedaron vivas al final,
  cuántas se liberaron, si la disposición fue la misma en los diez y **cuántas geometrías, materiales
  y contextos gráficos quedan vivos**, que es lo que prueba que `destruir` libera de verdad. El visor
  lleva esa cuenta y la expone; el panel la lee de ahí y **nunca abriendo la escena**.

**Vocabulario, que no se mezcla:** las **observaciones** son lo que el producto emite al interpretar
el texto y viven en la columna izquierda; las **condiciones del visor** son del contrato de la
fachada y viven junto a la escena y en el panel; el **comentario** del docente es una tercera cosa y
tiene su propio bloque.

---

## 7. Cómo se corrige a mano

Los archivos son planos y no hay build: editás, guardás, mirás.

| Qué querés cambiar | Dónde |
| --- | --- |
| Un dato de ejemplo, un texto de interfaz, un estado de un trabajo o de una cuenta | `assets/js/Datos-Maqueta.js` |
| Un color, un tamaño, un espaciado, un radio | `assets/css/Estilos-Maqueta.css`, en el bloque `:root`. **Todo valor visual es un token del catálogo de diseño: si hace falta uno nuevo, se promueve al catálogo antes de usarlo.** |
| La estructura o el texto de una superficie | Su archivo `.html` |
| Las cuatro partes de la vista de trabajo, o el bloque de decisión | `assets/js/Maqueta.js`, en `pintarCuatroPartes` y `pintarBloqueDeDecision`. Están ahí, y no en el HTML, porque **dos** superficies los presentan y duplicarlos obligaría a corregir dos veces |
| Un camino de navegación | El bloque «Navegación entre superficies» de `assets/js/Maqueta.js`, y los `href` de cada HTML |
| El panel del contrato de fachada del visor | `assets/js/Maqueta.js`, en `pintarPanelDeFachada`. Los códigos, las funciones y las propiedades salen de `assets/js/Datos-Maqueta.js` |
| El visor tridimensional: formas, luces, cámara, disposición por índice, tolerancia de claves | `assets/js/Visor-Tridimensional.js`. Es autónomo: no conoce la maqueta, no hace red, no lee configuración y no sabe quién es el usuario |
| Los dos movimientos automáticos: rótulos, valores por omisión y claves persistidas | La tabla `MOVIMIENTOS` de `assets/js/Maqueta.js`. La velocidad de cada uno vive en el bucle de dibujo de `assets/js/Visor-Tridimensional.js` |
| Un ícono | El diccionario `ICONOS` de `assets/js/Maqueta.js`. SVG en línea con `currentColor`, grilla de 24, sin raster y sin packs por CDN |
| Agregar un estado a una superficie | Agregá el bloque con `data-mq-estado="mi-estado"` en su HTML. Aparece solo en la barra de validación. Si querés un rótulo lindo, agregá su entrada en `ROTULOS_DE_ESTADO` de `Maqueta.js` |

Cuando termines, avisale al orquestador: **«revisá la maqueta y tomá las correcciones»**. Relee los
archivos, enumera las diferencias, te presenta su interpretación y recién después la propaga a la
documentación. Las correcciones manuales no se pisan en las iteraciones siguientes.

---

## 8. Accesibilidad

WCAG 2.2 nivel AA es el piso, también acá: una maqueta que se valida sin accesibilidad enseña al
validador humano a aprobar una superficie inaccesible.

- Landmarks (`nav`, `main`), un `h1` por superficie que nombra su tarea —incluido el shell de acceso,
  que no puede quedar sin estructura por no tener navegación— y enlace de salto al contenido.
- Etiqueta visible y asociada en cada control. El texto de ejemplo nunca sustituye a la etiqueta.
- Foco visible de 2 px en todo elemento interactivo, y recorrido completo por teclado, incluidos el
  árbol (flechas arriba y abajo entre piezas, derecha e izquierda para desplegar), los diálogos (foco confinado, escape para cerrar, foco devuelto al control que
  los abrió) y el detalle de diagnóstico del sello.
- Regiones activas: el cambio de estado de la maqueta, el resultado del envío, el aviso de
  indisponibilidad (`alert`), el cartel de reconexión (`status`), la selección de pieza y la
  confirmación de copiado del diagnóstico.
- El color nunca es el único canal: las cuatro insignias de estado del trabajo, las tres de situación
  de cuenta y las dos severidades de observación llevan su texto escrito.
- Objetivos de toque de al menos 24×24 px en todas las acciones por fila.
- `prefers-reduced-motion` respetado, incluidos los **dos movimientos automáticos** del visor: con
  esa preferencia activa las dos casillas arrancan destildadas, el control declara por qué, y la
  escena sólo se mueve cuando la persona la arrastra. **La preferencia la lee la página**, no el
  visor, que es lo que el contrato de fachada exige.
- Las dos casillas de movimiento son controles de formulario reales, con etiqueta visible y asociada
  por `for`, operables por teclado, agrupadas con nombre, con su ayuda asociada por
  `aria-describedby` —y no sólo en `title`—, y su cambio se anuncia como región activa.
- **La escena se opera por teclado.** El lienzo recibe foco (`tabindex="0"`) y responde a las flechas
  —orbitar—, a `+` y `-` —acercar y alejar— y a Inicio —vista de partida—. La rueda del mouse sólo se
  captura con el lienzo enfocado, para no atrapar el desplazamiento de la página. La **elección** de
  pieza tiene además su ruta accesible completa en el árbol, que es lo que declara `SD-50`.
- **El árbol tiene un solo portador de rol por nodo.** El `<li role="treeitem">` lleva el rol,
  `aria-selected`, `aria-expanded` y el foco, con **tabindex móvil** —un solo nodo tabable por
  árbol—; el interior es presentación. Flechas arriba y abajo recorren los nodos visibles, derecha e
  izquierda despliegan y pliegan, Inicio y Fin van a los extremos, y Entrar o espacio seleccionan.
- **El panel del contrato de fachada devuelve el foco.** Las dos comprobaciones a mano reescriben la
  lista que las contiene; después del refresco el foco vuelve al mismo control y no cae al `<body>`.
- El sello de versión usa `color.text.secondary` y no `color.text.tertiary`, para cumplir el
  contraste de 4.5:1 pese a su jerarquía baja. Ver el hallazgo H-1 de la devolución.
- **La barra de validación también está medida**, porque es el instrumento con el que se valida la
  accesibilidad del resto: sobre `--color-brand-primary-dark` sus tres pares dan 11.94:1, 12.07:1 y
  13.70:1. Los dos colores propios que tenía —inventados fuera del catálogo y sin medir— se
  reemplazaron por tokens existentes.
- **Ningún HTML ni guion de la maqueta lleva un atributo `style=` en línea.** Lo que era estilo en
  línea vive en clases con nombre que sólo materializan tokens del catálogo.

---

## 9. Las tres reglas de arquitectura, en la maqueta

- **RA-01.** Ningún guion del navegador llama al backend. Acá no hay backend, y además la maqueta no
  simula llamadas: los filtros y las búsquedas actúan sobre lo ya renderizado.
- **RA-02.** El visor se invoca sólo por su fachada. El área de dibujo monta el visor real, y la
  superficie no lo toca por otro lado: la escena, la selección, el ajuste y la liberación pasan por
  las cinco funciones. La alternativa textual de la escena y la medición de recursos se componen
  **con lo que la fachada devuelve**, nunca leyendo el interior de la escena. La lista de piezas no
  dibujadas está **al lado** de la escena, nunca encima ni dentro de la lista de observaciones.
- **RA-03.** Ningún mensaje visible incluye la dirección de un servicio interno, un nombre de archivo
  de datos, una traza ni un código de error.
