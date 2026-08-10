# Línea de base visual — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Linea-Base-Visual.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Maquetador de validación visual (AG-03M)
**Variante:** UX/UI
**Trazabilidad upstream:** la maqueta aprobada `SDD/Maquetas/GeometriaFactory-Web/` íntegra —`index.html`, las once superficies, `assets/js/Datos-Maqueta.js`, `assets/js/Maqueta.js`, `assets/js/Visor-Tridimensional.js` y `assets/css/Estilos-Maqueta.css`—; los once `Wireframes-*.md` y `Experiencia-De-Uso.md` §3.1, §3.2 y §4.2 de esta misma categoría, ya retroalimentados; `Glosario-UX.md` §2; las tres `Representacion-*.md`; `../02-Especificacion-Funcional/Casos-De-Uso/` `CU-01` a `CU-10`; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §4, §5.5 y §6; `Deriva-Rules.md` §2.1
**Trazabilidad downstream:** `Matriz-Sensado-Deriva.md` de `08-Calidad-Y-Pruebas`, que convierte este inventario en sondas; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Qué es esta línea de base y cómo se usa](#1-qué-es-esta-línea-de-base-y-cómo-se-usa)
- [2. Superficies (`SUP-XX`)](#2-superficies-sup-xx)
- [3. Componentes (`CMP-XX`)](#3-componentes-cmp-xx)
- [4. Estados (`EST-XX`)](#4-estados-est-xx)
- [5. Rutas de navegación (`NAV-XX`)](#5-rutas-de-navegación-nav-xx)
- [6. Lo que la maqueta exhibe y no forma parte de la línea de base](#6-lo-que-la-maqueta-exhibe-y-no-forma-parte-de-la-línea-de-base)
  - [6.1 Lo que el contrato declara y esta línea de base no validó visualmente](#61-lo-que-el-contrato-declara-y-esta-línea-de-base-no-validó-visualmente)
- [7. Evidencia](#7-evidencia)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué es esta línea de base y cómo se usa

Es el **inventario identificado** de lo que el Product Owner miró y aprobó al aprobar la maqueta de validación visual de la Fase B2, el 2026-08-09, después de cuatro iteraciones registradas en [`Bitacora-Validacion-Maqueta.md`](Bitacora-Validacion-Maqueta.md). No es una descripción de la maqueta: es la lista contra la cual se puede preguntar, en cualquier momento de la codificación, si lo construido sigue siendo lo aprobado.

Tres reglas de uso, de `Deriva-Rules.md` §5:

1. **Se cita, no se reinterpreta.** Un agente que necesita algo que no está acá lo pide; no lo deduce.
2. **No reemplaza a la especificación.** Dice qué elementos hay y cómo se ven. El porqué y el comportamiento fino siguen viviendo en `02-Especificacion-Funcional` y en el resto de esta categoría.
3. **Lo que no está acá no está prohibido: está sin validar visualmente.** Tratarlo como prohibido paraliza la construcción; tratarlo como aprobado es deriva.

Los identificadores son de dos dígitos y **estables**: un elemento que se retira no libera su número. Su fila queda con estado `Retirado` y la fecha, para que una referencia vieja no apunte a otra cosa. Todas las filas de este documento nacen vigentes; ninguna está retirada.

**Los nombres son los del glosario y no se inventan.** Los nombres canónicos de superficie son los de la sección 1 de cada wireframe y los de `Experiencia-De-Uso.md` §3.1; los de componente y estado son los de las secciones 3 y 5 de cada wireframe; los términos que los gobiernan —superficie, superficie alojada, bloque, insignia de estado, estado de superficie, movimiento automático de la escena— están declarados en `Glosario-UX.md` §2.

**Cantidades:** 11 `SUP-XX`, 73 `CMP-XX`, 74 `EST-XX` y 24 `NAV-XX`.

## 2. Superficies (`SUP-XX`)

Once superficies, una por `Wireframes-<superficie>.md` de esta categoría y una por archivo de la maqueta. **`SUP-08` es la única superficie alojada**: no tiene ruta y se dibuja dentro de `SUP-07`.

| ID | Nombre canónico | Archivo de la maqueta | Wireframe que la especifica | CU que la origina | Propósito |
| --- | --- | --- | --- | --- | --- |
| `SUP-01` | `Aprovisionamiento-Inicial` | `Aprovisionamiento-Inicial.html` | `Wireframes-Aprovisionamiento-Inicial.md` | `CU-04` FA-03 y FA-04 | Constituir la única cuenta de administrador en el primer arranque del laboratorio |
| `SUP-02` | `Registro-De-Cuenta` | `Registro-De-Cuenta.html` | `Wireframes-Registro-De-Cuenta.md` | `CU-01` | Que el alumno se dé de alta y quede a la espera de habilitación |
| `SUP-03` | `Ingreso` | `Ingreso.html` | `Wireframes-Ingreso.md` | `CU-02` | Iniciar sesión sin exponer la credencial, y recibir las bandas de confirmación de los tres orígenes |
| `SUP-04` | `Credencial-Propia` | `Credencial-Propia.html` | `Wireframes-Credencial-Propia.md` | `CU-03` | Establecer la contraseña propia la primera vez, y cambiarla después |
| `SUP-05` | `Panel-De-Trabajos-Del-Alumno` | `Panel-De-Trabajos-Del-Alumno.html` | `Wireframes-Panel-De-Trabajos-Del-Alumno.md` | `CU-06` | Ver todo lo cargado con su estado, y operar sobre el borrador |
| `SUP-06` | `Envio-De-Trabajo` | `Envio-De-Trabajo.html` | `Wireframes-Envio-De-Trabajo.md` | `CU-05` | Cargar el texto, previsualizarlo y enviarlo, con el resultado de la interpretación a la vista |
| `SUP-07` | `Vista-De-Trabajo` | `Vista-De-Trabajo.html` | `Wireframes-Vista-De-Trabajo.md` | `CU-07` | Explorar un trabajo en sus cuatro partes: datos, texto, escena y árbol |
| `SUP-08` | `Resolucion-Del-Trabajo` | Alojada en `Vista-De-Trabajo.html`; `Resolucion-Del-Trabajo.html` la exhibe aislada como instrumento de validación | `Wireframes-Resolucion-Del-Trabajo.md` | `CU-09` | Dar desenlace a un trabajo en estado `Pendiente`, con comentario opcional, y retirar cualquier trabajo visible |
| `SUP-09` | `Panel-De-Cuentas` | `Panel-De-Cuentas.html` | `Wireframes-Panel-De-Cuentas.md` | `CU-04` flujo principal, FA-01, FA-02 y FA-05 | Administrar las cuentas de la comisión: situación, baja y orientación posterior |
| `SUP-10` | `Listado-De-La-Comision` | `Listado-De-La-Comision.html` | `Wireframes-Listado-De-La-Comision.md` | `CU-08` | Recorrer la entrega de la comisión agrupada por alumno, sin los borradores |
| `SUP-11` | `Estado-Degradado-Y-Reconexion` | `Estado-Degradado-Y-Reconexion.html` | `Wireframes-Estado-Degradado-Y-Reconexion.md` | `CU-10` | Sostener la aplicación cuando el servicio de datos no responde y cuando el circuito se corta |

**`SUP-08` no es un destino de navegación.** Su archivo propio en la maqueta existe porque el wireframe exige demostrar el bloque como recorrido con su mapa de estados completo, y dibuja el trabajo entero debajo del bloque, armado con el mismo código que `SUP-07`. Un sistema construido que le dé ruta propia es **deriva mayor**.

## 3. Componentes (`CMP-XX`)

Setenta y tres componentes, tomados de la sección 3 de los once wireframes y verificados uno por uno contra la maqueta. Un componente que aparece en más de una superficie tiene **una sola fila**, con todas sus superficies.

| ID | Componente | Superficies | Datos que muestra | Comportamiento aprobado | Patrón del catálogo |
| --- | --- | --- | --- | --- | --- |
| `CMP-01` | Tarjeta de aprovisionamiento | `Aprovisionamiento-Inicial` | — | Ancho acotado, anclada arriba. Estados: normal, con error, enviando | `Design-Rules-Primer-Arranque` §4.2 |
| `CMP-02` | Identidad del laboratorio | `Aprovisionamiento-Inicial` | Nombre del producto, marca vectorial | Inerte | Base §6.3 |
| `CMP-03` | Encabezado y subtítulo de alcance | `Aprovisionamiento-Inicial` | Texto fijo | Inerte. El encabezado es el de primer nivel de la superficie | Base §2.2 |
| `CMP-04` | Banda de resultado | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Ingreso`, `Registro-De-Cuenta` | Texto resuelto desde el código de resultado del contrato | Condicional. Variante de error con rol de alerta; variante de confirmación con rol de estado | Primer arranque §4.4 |
| `CMP-05` | Campo de correo | `Aprovisionamiento-Inicial`, `Ingreso`, `Registro-De-Cuenta` | Lo escrito | Etiqueta visible arriba. Sin texto de ejemplo que sustituya la etiqueta | Base §4.6 |
| `CMP-06` | Campo de contraseña y su repetición | `Aprovisionamiento-Inicial` | Enmascarados | Dos campos. La coincidencia se verifica **antes** de salir hacia el servicio de datos | Base §4.6 |
| `CMP-07` | Requisito declarado | `Aprovisionamiento-Inicial`, `Credencial-Propia` | Texto derivado de la política del sistema | Asociado al campo que describe. **No aparece recién al fallar** | Primer arranque §4.5 |
| `CMP-08` | Acción primaria | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Ingreso`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta` | El verbo exacto de la acción de cada superficie | Ancho completo. Se inhabilita con indicador durante el envío | Base §4.9 |
| `CMP-09` | Sello de versión | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Ingreso`, `Registro-De-Cuenta` | Versión legible, distintivo y marcador según corresponda | Al pie. Es **una de las dos ubicaciones obligatorias**: la superficie de acceso | [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) |
| `CMP-10` | Superficie de resolución | `Aprovisionamiento-Inicial` | — | Barra indeterminada. La navegación resultante **reemplaza** la entrada del historial en vez de apilarla | Primer arranque §4.3 |
| `CMP-11` | Tarjeta de credencial | `Credencial-Propia` | — | Ancho acotado en los dos cursos | Primer arranque §4.2 / Base §4.4 |
| `CMP-12` | Subtítulo de motivo | `Credencial-Propia` | Texto distinto por curso | Inerte. En el establecimiento declara que el laboratorio nunca envió una contraseña | Base §2.2 |
| `CMP-13` | Campo de contraseña actual | `Credencial-Propia` | Enmascarado | **Sólo en el curso de cambio. Es obligatorio por contrato** | Base §4.6 |
| `CMP-14` | Campos de contraseña nueva y repetición | `Credencial-Propia` | Enmascarados, con conmutador de visibilidad | La coincidencia se verifica **antes** de salir hacia el servicio de datos | Base §4.6 |
| `CMP-15` | Acción secundaria | `Credencial-Propia` | «Cancelar» | **Sólo en el curso de cambio** | Base §4.9 |
| `CMP-16` | Encabezado y subtítulo | `Envio-De-Trabajo` | Texto fijo por curso | El título es el encabezado de primer nivel | Base §2.2 |
| `CMP-17` | Campos de nombre, fecha y descripción | `Envio-De-Trabajo` | Lo escrito | Etiqueta visible arriba. La fecha la declara el alumno | Base §4.4, §4.6 |
| `CMP-18` | Área de texto del trabajo | `Envio-De-Trabajo` | El texto **tal cual** | Avance uniforme por carácter, para que el texto del alumno se lea sin desalineación, y ancho suficiente para una línea del texto sin cortar. **No se normaliza, no se reordena y no se le quita ningún carácter** | Base §4.6 |
| `CMP-19` | Bloque de previsualización | `Envio-De-Trabajo` | La escena de la instancia | Es el **componente anfitrión**: provee el elemento, invoca la creación, la carga y la liberación | — |
| `CMP-20` | Acción de previsualizar | `Envio-De-Trabajo` | «Previsualizar» | **Sin ninguna llamada al servicio de datos** | Base §4.9, secundaria |
| `CMP-21` | Nota de alcance de la previsualización | `Envio-De-Trabajo` | Texto fijo | Inerte. Es la nota que evita la malinterpretación principal de la superficie | Base §5 |
| `CMP-22` | Lista de piezas no dibujadas | `Envio-De-Trabajo`, `Vista-De-Trabajo` | Índice y motivo, por pieza | **Junto a la escena, nunca como observación del trabajo** | Base §5 |
| `CMP-23` | Nota de acción única | `Envio-De-Trabajo` | Texto fijo | Inerte, contigua a la acción primaria | Base §5 |
| `CMP-24` | Bloque de resultado | `Envio-De-Trabajo` | Estado, y la lista completa | Reemplaza el contenido. Ver [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md) | Base §5 |
| `CMP-25` | Aviso de indisponibilidad | `Estado-Degradado-Y-Reconexion`, `Listado-De-La-Comision`, `Panel-De-Trabajos-Del-Alumno` | Qué no se pudo hacer y qué hacer al respecto | Reemplaza **el contenido**, no el armazón. La navegación sigue disponible | Base §5, estado de error |
| `CMP-26` | Acción de reintentar | `Estado-Degradado-Y-Reconexion` | Verbo que nombra la acción concreta: «Reintentar el envío», no «Reintentar» a secas cuando hay una acción identificable | Al tener éxito, el aviso desaparece **sin volver a ingresar** | Base §4.9 |
| `CMP-27` | Cartel de reconexión | `Estado-Degradado-Y-Reconexion` | Que la conexión se cortó y que se está reintentando | Banda superpuesta en el borde superior. **Se estiliza con los tokens del producto**, no se deja el aspecto por omisión del sistema de componentes | Blazor §2 |
| `CMP-28` | Bloque de contenido conservado | `Estado-Degradado-Y-Reconexion` | El formulario completo, con su texto intacto | No se guarda en ningún lado: sigue en la superficie, nada más | Base §5 |
| `CMP-29` | Estado vacío | `Estado-Degradado-Y-Reconexion`, `Panel-De-Trabajos-Del-Alumno` | Ilustración neutra, texto orientativo y acción siguiente | **Se distingue del aviso por el tipo recibido y no por el conteo** | Base §5 |
| `CMP-30` | Tarjeta de acceso | `Ingreso`, `Registro-De-Cuenta` | — | Ancho acotado, anclada arriba | Primer arranque §4.2, reusada |
| `CMP-31` | Campo de contraseña | `Ingreso` | Enmascarado, con conmutador de visibilidad | Ídem, con propósito de contraseña vigente | Base §4.6 |
| `CMP-32` | Enlace a `Registro-De-Cuenta` | `Ingreso` | «¿No tenés cuenta? Registrarte» | — | Base §4.9 |
| `CMP-33` | Nota sobre la contraseña olvidada | `Ingreso` | Texto fijo | **Inerte: no es un enlace y no dispara nada** | Base §5 |
| `CMP-34` | Encabezado de la superficie | `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno` | Título y subtítulo que dice que los borradores no aparecen | El título es el encabezado de primer nivel | Base §4.3 |
| `CMP-35` | Barra de filtros | `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno` | Selector de alumno, selector de estado | El filtro por alumno **vuelve a pedir la colección** con el criterio poblado; el de estado acota lo ya recibido | Base §4.10 |
| `CMP-36` | Cabecera de grupo | `Listado-De-La-Comision` | Iniciales, nombre, correo, recuento de trabajos | Colapsable. El grupo colapsado conserva su recuento a la vista | Base §3.2 |
| `CMP-37` | Fila de trabajo | `Listado-De-La-Comision`, `Panel-De-Trabajos-Del-Alumno` | Nombre, fecha, insignia de estado, piezas, advertencias | Acción única: abrir. **No hay acciones de decisión** | [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) |
| `CMP-38` | Insignia de estado | `Listado-De-La-Comision`, `Panel-De-Trabajos-Del-Alumno`, `Vista-De-Trabajo` | `Pendiente`, `Finalizado` o `Rechazado`. **Nunca `Borrador`** | Siempre con texto | Representación §2 |
| `CMP-39` | Nota de ausencia | `Listado-De-La-Comision` | Texto fijo al pie de la lista | Inerte. Resuelve una pregunta que el docente se hace siempre | Base §5 |
| `CMP-40` | Acción de resumen | `Listado-De-La-Comision` | «Resumen» | Capacidad de prioridad menor, prevista para una etapa posterior | Base §4.9, secundaria |
| `CMP-41` | Panel de resumen | `Listado-De-La-Comision` | Una fila por alumno y una columna por estado | Se abre y se cierra sin dejar la superficie | Base §4.3 |
| `CMP-42` | Fila de cuenta | `Panel-De-Cuentas` | Iniciales, nombre, apellido, correo, insignia de situación, fecha de registro | Acciones por fila alineadas a la derecha | Base §4.3 |
| `CMP-43` | Iniciales de la cuenta | `Panel-De-Cuentas` | Dos letras sobre círculo con tinte | Vectorial. **No hay foto y no hace falta** | Base §6.3 |
| `CMP-44` | Insignia de situación | `Panel-De-Cuentas` | Uno de los tres valores, **siempre con texto** | El color es refuerzo. Se llama «situación» y no «estado», para no colisionar con el estado del trabajo | Base §4.8 |
| `CMP-45` | Acción de situación | `Panel-De-Cuentas` | Verbo exacto según la situación vigente | **Se ofrece la transición que la situación admite**, no las tres a la vez | Base §4.3 |
| `CMP-46` | Acción de baja | `Panel-De-Cuentas` | Ícono con rótulo accesible propio | Color y borde de peligro. Abre el diálogo de confirmación escrita | Base §4.3, destructiva |
| `CMP-47` | Diálogo de confirmación escrita | `Panel-De-Cuentas` | Nombre de la cuenta, el correo a transcribir | La acción destructiva permanece inhabilitada hasta que lo escrito coincide | Primer arranque §4.4, Base §4.4 |
| `CMP-48` | Aviso de arrastre | `Panel-De-Cuentas` | Texto fijo, en estado de atención | **En el mismo lugar donde se pide la confirmación**, no en otra superficie | Base §5 |
| `CMP-49` | Orientación posterior | `Panel-De-Cuentas` | Tres tarjetas de acceso | **Orienta, no bloquea.** No es un asistente ni una lista de tareas con progreso | Primer arranque §4.6 |
| `CMP-50` | Banda de confirmación | `Panel-De-Cuentas` | Qué quedó creado | Rol de estado. Aparece una sola vez | Primer arranque §4.4 |
| `CMP-51` | Diálogo de confirmación de eliminación | `Panel-De-Trabajos-Del-Alumno` | Nombre del trabajo | Sólo se abre desde una fila en estado `Borrador` | Base §4.4, Blazor §4 |
| `CMP-52` | Subtítulo de expectativa | `Registro-De-Cuenta` | Texto fijo | Inerte. No se colapsa en ningún ancho | Base §2.2 |
| `CMP-53` | Campos de nombre y apellido | `Registro-De-Cuenta` | Lo escrito | Ídem | Base §4.6 |
| `CMP-54` | Enlace a `Ingreso` | `Registro-De-Cuenta` | «¿Ya tenés cuenta? Ingresar» | Es la **única** salida de la superficie | Base §4.9 |
| `CMP-55` | Bloque de éxito | `Registro-De-Cuenta` | Qué quedó creado y qué falta | Reemplaza el formulario. Su acción lleva a `Ingreso` | Base §5 |
| `CMP-56` | Bloque de decisión | `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` | — | Sólo para el administrador y sólo en estado `Pendiente` | Base §4.4 |
| `CMP-57` | Campo de comentario | `Resolucion-Del-Trabajo` | Lo escrito | **Opcional, sin longitud mínima.** La etiqueta lleva la palabra «opcional» a la vista | Base §4.6 |
| `CMP-58` | Nota de destino del comentario | `Resolucion-Del-Trabajo` | Texto fijo | Inerte. Dice que se lee al abrir el trabajo, no en el listado | Base §5 |
| `CMP-59` | Acción de aprobar | `Resolucion-Del-Trabajo` | Verbo exacto: «Aprobar» | Abre el diálogo de confirmación | Base §4.9, primaria |
| `CMP-60` | Acción de rechazar | `Resolucion-Del-Trabajo` | Verbo exacto: «Rechazar» | Abre el mismo diálogo con el otro valor. **No es un control destructivo**: rechazar es una decisión legítima, no una destrucción | Base §4.9, secundaria |
| `CMP-61` | Acción de retirar | `Resolucion-Del-Trabajo` | «Retirar» | Color y borde de peligro, **visualmente separada de las dos decisiones**. Abre su propio diálogo | Base §4.9, destructiva |
| `CMP-62` | Diálogo de confirmación del desenlace | `Resolucion-Del-Trabajo` | Nombre del trabajo, decisión, y el comentario tal como quedó escrito | La confirmación no es escrita: la operación es reversible en el sentido de que el trabajo sigue existiendo | Base §4.4 |
| `CMP-63` | Diálogo de confirmación del retiro | `Resolucion-Del-Trabajo` | Nombre del trabajo, y el aviso de que desaparece también del listado del alumno | Aviso en estado de atención | Base §4.4 |
| `CMP-64` | Bloque de trabajo resuelto | `Resolucion-Del-Trabajo` | Fecha del desenlace y estado alcanzado | **No ofrece las dos decisiones.** La única acción disponible es retirar | Base §5 |
| `CMP-65` | Barra de regreso | `Vista-De-Trabajo` | «Volver al listado» | Lleva al listado del papel de quien mira: el propio si es el alumno, el de la comisión si es el administrador | Base §4.9, píldora auxiliar |
| `CMP-66` | Cabecera del trabajo | `Vista-De-Trabajo` | Nombre, insignia de estado, fecha, alumno dueño, recuento de piezas y de advertencias | Inerte. El nombre es el encabezado de primer nivel de la superficie | Base §2.2 |
| `CMP-67` | Bloque de datos | `Vista-De-Trabajo` | Nombre, fecha, estado, alumno, descripción | Solo lectura en todos los casos. **Esta superficie nunca edita** | Base §4.4, filas clave/valor |
| `CMP-68` | Bloque de comentario | `Vista-De-Trabajo` | Texto libre, a lo sumo uno | **Bloque propio, separado de las observaciones.** Sin severidad, sin índice, sin campo señalado, sin tono de alerta. **No se dibuja si no viene poblado** | — |
| `CMP-69` | Lista de observaciones | `Vista-De-Trabajo` | Severidad, índice de figura, campo señalado, y el par declarado/derivado en las advertencias | **Sin filtrar por severidad.** Con cero elementos, muestra «Sin observaciones» y no un hueco | [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md) |
| `CMP-70` | Bloque de texto original | `Vista-De-Trabajo` | El texto **íntegro, carácter por carácter** | Colapsable, colapsado por omisión. Solo lectura y sin reescritura de ningún carácter | Base §4.6, divulgación progresiva |
| `CMP-71` | Bloque de la escena | `Vista-De-Trabajo` | La escena de la instancia del visualizador | Es el **componente anfitrión**: provee el elemento, invoca las **seis** funciones de la fachada —`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `establecerMovimiento` y `destruir`— y opera el ciclo de vida. Es además quien **consulta la preferencia de movimiento reducido del sistema** y quien **conserva la elección**: la fachada no hace ninguna de las dos cosas. **La sexta función no fue validada visualmente**: ver §6 | — |
| `CMP-72` | Controles de movimiento automático | `Vista-De-Trabajo` | Dos casillas independientes, con etiqueta visible: **«Órbita de la cámara»** y **«Giro de las figuras»** | **Al pie del área de dibujo, no en un panel aparte**: son preferencias de quien mira. Se tildan por separado y pueden estar tildadas las dos a la vez. Tildadas por omisión, salvo preferencia de movimiento reducido declarada por el sistema, en cuyo caso **arrancan destildadas y el control declara por qué**. **Esa decisión la toma el componente anfitrión**, que lee la preferencia y le manda a la fachada dos valores de verdad, uno por movimiento; con opciones ausentes o parciales la instancia arranca con los dos apagados. La elección se conserva entre trabajos. Los dos se detienen mientras la persona arrastra y con la pestaña oculta. **Ninguno de los dos altera la disposición de las piezas** | Base §4.6, grupo de casillas |
| `CMP-73` | Árbol de la estructura | `Vista-De-Trabajo` | La estructura que devuelve la fachada | Colapsable por nodo. El nodo de una pieza lleva su índice a la vista | — |

## 4. Estados (`EST-XX`)

Setenta y cuatro estados canónicos, conmutables uno por uno desde la barra de validación de la maqueta. **Un estado tiene una sola fila** aunque lo presenten varias superficies: es el mismo estado con la misma condición disparadora. La columna de superficies dice dónde se lo aprobó.

Los ocho últimos estados de esta tabla, más `EST-25` a `EST-28`, son los que materializan las **siete condiciones del contrato de fachada** del proyecto de código del visor, con sus dos cursos de `ELEMENTO_DE_DIBUJO_INVALIDO`. Sus códigos son los de `Definicion-Contrato-De-Fachada.md` §6 y no se renombran.

| ID | Identificador del estado | Rótulo aprobado y condición que lo produce | Superficies que lo presentan |
| --- | --- | --- | --- |
| `EST-01` | `resolviendo-destino` | Resolviendo destino | `Aprovisionamiento-Inicial` |
| `EST-02` | `ya-aprovisionado` | Ya aprovisionado (redirección neutra) | `Aprovisionamiento-Inicial` |
| `EST-03` | `con-datos` | Con datos | `Aprovisionamiento-Inicial`, `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion`, `Ingreso`, `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Vista-De-Trabajo` |
| `EST-04` | `cargando` | Cargando | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion`, `Ingreso`, `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` |
| `EST-05` | `enviando` | Enviando | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Ingreso`, `Registro-De-Cuenta` |
| `EST-06` | `error-entrada` | Error de entrada · requisito no cumplido | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Ingreso`, `Registro-De-Cuenta` |
| `EST-07` | `confirmacion-no-coincidente` | Confirmación no coincidente | `Aprovisionamiento-Inicial`, `Credencial-Propia` |
| `EST-08` | `error-operacion` | Error de operación | `Aprovisionamiento-Inicial`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` |
| `EST-09` | `exito` | Éxito | `Aprovisionamiento-Inicial`, `Ingreso`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta` |
| `EST-10` | `indisponible` | Indisponible (el servicio de datos no responde) | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Estado-Degradado-Y-Reconexion`, `Ingreso`, `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` |
| `EST-11` | `reconectando` | Reconectando (se cortó el circuito) | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion`, `Ingreso`, `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` |
| `EST-12` | `vacio` | Vacío (colección con cero elementos) | `Aprovisionamiento-Inicial`, `Credencial-Propia`, `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion`, `Ingreso`, `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno`, `Registro-De-Cuenta`, `Resolucion-Del-Trabajo`, `Vista-De-Trabajo` |
| `EST-13` | `version-preliminar` | Sello: versión preliminar | `Aprovisionamiento-Inicial`, `Ingreso` |
| `EST-14` | `origen-indeterminado` | Sello: origen indeterminado | `Aprovisionamiento-Inicial`, `Ingreso` |
| `EST-15` | `curso-establecimiento` | Curso de establecimiento | `Credencial-Propia` |
| `EST-16` | `exito-establecimiento` | Éxito de establecimiento | `Credencial-Propia` |
| `EST-17` | `cuenta-bloqueada` | Cuenta bloqueada entre la derivación y el envío | `Credencial-Propia` |
| `EST-18` | `curso-cambio` | Curso de cambio | `Credencial-Propia` |
| `EST-19` | `actual-rechazada` | Contraseña actual rechazada | `Credencial-Propia` |
| `EST-20` | `exito-cambio` | Éxito de cambio (la sesión se conserva) | `Credencial-Propia` |
| `EST-21` | `verifico` | Verificó (pasa a Pendiente) | `Envio-De-Trabajo` |
| `EST-22` | `verifico-con-advertencias` | Verificó con advertencias | `Envio-De-Trabajo` |
| `EST-23` | `no-verifico` | No verificó (queda en Borrador) | `Envio-De-Trabajo` |
| `EST-24` | `previsualizado` | Previsualizado | `Envio-De-Trabajo` |
| `EST-25` | `escena-no-disponible` | Escena no disponible · CAPACIDAD_GRAFICA_AUSENTE | `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion`, `Vista-De-Trabajo` |
| `EST-26` | `texto-no-legible` | Texto no legible · TEXTO_NO_LEGIBLE | `Envio-De-Trabajo`, `Vista-De-Trabajo` |
| `EST-27` | `piezas-no-dibujadas` | Piezas no dibujadas · TIPO_NO_DIBUJABLE | `Envio-De-Trabajo`, `Vista-De-Trabajo` |
| `EST-28` | `elemento-sin-tamano` | Elemento de dibujo sin tamaño, en creación · ELEMENTO_DE_DIBUJO_INVALIDO (C-1) | `Envio-De-Trabajo`, `Vista-De-Trabajo` |
| `EST-29` | `trabajo-ajeno` | Trabajo ajeno o inexistente | `Envio-De-Trabajo`, `Panel-De-Trabajos-Del-Alumno`, `Resolucion-Del-Trabajo` |
| `EST-30` | `indisponible-conservado` | Indisponible, con lo escrito conservado | `Envio-De-Trabajo`, `Estado-Degradado-Y-Reconexion` |
| `EST-31` | `reconectado` | Reconectado tras el corte | `Estado-Degradado-Y-Reconexion` |
| `EST-32` | `transporte-replegado` | Transporte replegado (ningún aviso) | `Estado-Degradado-Y-Reconexion` |
| `EST-33` | `fallo-no-clasificado` | Fallo no clasificado | `Estado-Degradado-Y-Reconexion` |
| `EST-34` | `sesion-no-restablecible` | Sesión vencida o no restablecible | `Estado-Degradado-Y-Reconexion`, `Ingreso` |
| `EST-35` | `recuperado` | Recuperado tras el reintento | `Estado-Degradado-Y-Reconexion` |
| `EST-36` | `confirmacion-aprovisionamiento` | Confirmación de aprovisionamiento | `Ingreso` |
| `EST-37` | `confirmacion-registro` | Confirmación de registro | `Ingreso` |
| `EST-38` | `confirmacion-contrasena` | Confirmación de contraseña establecida | `Ingreso` |
| `EST-39` | `sesion-cerrada` | Sesión cerrada | `Ingreso` |
| `EST-40` | `credencial-rechazada` | Credencial rechazada | `Ingreso` |
| `EST-41` | `cuenta-no-habilitada` | Cuenta no habilitada | `Ingreso` |
| `EST-42` | `contrasena-sin-establecer` | Contraseña sin establecer | `Ingreso` |
| `EST-43` | `alumno-filtro-inexistente` | Alumno del filtro inexistente | `Listado-De-La-Comision` |
| `EST-44` | `cero-borradores` | Cero borradores (permanente) | `Listado-De-La-Comision` |
| `EST-45` | `filtrado-sin-resultados` | Filtrado sin resultados | `Listado-De-La-Comision`, `Panel-De-Cuentas`, `Panel-De-Trabajos-Del-Alumno` |
| `EST-46` | `grupo-colapsado` | Grupo colapsado | `Listado-De-La-Comision` |
| `EST-47` | `resumen-abierto` | Resumen abierto | `Listado-De-La-Comision` |
| `EST-48` | `orientacion-posterior` | Orientación posterior al aprovisionamiento | `Panel-De-Cuentas` |
| `EST-49` | `aplicando-situacion` | Aplicando un cambio de situación | `Panel-De-Cuentas` |
| `EST-50` | `ejecutando-baja` | Ejecutando la baja | `Panel-De-Cuentas` |
| `EST-51` | `cuenta-inexistente` | Error de operación · cuenta inexistente | `Panel-De-Cuentas` |
| `EST-52` | `administrador-ya-configurado` | Error de operación · administrador ya configurado | `Panel-De-Cuentas` |
| `EST-53` | `baja-no-coincide` | Confirmación de baja no coincidente | `Panel-De-Cuentas` |
| `EST-54` | `confirmando-baja` | Confirmando la baja (confirmación escrita) | `Panel-De-Cuentas` |
| `EST-55` | `confirmando-eliminacion` | Confirmando eliminación | `Panel-De-Trabajos-Del-Alumno` |
| `EST-56` | `eliminando` | Eliminando | `Panel-De-Trabajos-Del-Alumno` |
| `EST-57` | `correo-ya-registrado` | Error de operación · correo ya registrado | `Registro-De-Cuenta` |
| `EST-58` | `desenlace-no-administrador` | Error · desenlace no ejercido por el administrador | `Resolucion-Del-Trabajo` |
| `EST-59` | `exito-desenlace` | Éxito del desenlace | `Resolucion-Del-Trabajo` |
| `EST-60` | `exito-retiro` | Éxito del retiro | `Resolucion-Del-Trabajo` |
| `EST-61` | `resoluble` | Con datos · resoluble (administrador, trabajo Pendiente) | `Resolucion-Del-Trabajo` |
| `EST-62` | `no-resoluble-papel` | Con datos · no resoluble por papel (mira el alumno) | `Resolucion-Del-Trabajo` |
| `EST-63` | `no-resoluble-estado` | Con datos · no resoluble por estado (ya resuelto) | `Resolucion-Del-Trabajo` |
| `EST-64` | `confirmando-desenlace` | Confirmando desenlace | `Resolucion-Del-Trabajo` |
| `EST-65` | `aplicando` | Aplicando el desenlace | `Resolucion-Del-Trabajo` |
| `EST-66` | `confirmando-retiro` | Confirmando retiro | `Resolucion-Del-Trabajo` |
| `EST-67` | `sin-observaciones` | Sin observaciones | `Vista-De-Trabajo` |
| `EST-68` | `sin-comentario` | Sin comentario | `Vista-De-Trabajo` |
| `EST-69` | `con-comentario` | Con comentario | `Vista-De-Trabajo` |
| `EST-70` | `borrador-con-errores` | Trabajo en borrador con errores | `Vista-De-Trabajo` |
| `EST-71` | `elemento-sin-tamano-ajuste` | Elemento de dibujo sin tamaño, en ajuste · ELEMENTO_DE_DIBUJO_INVALIDO (C-2) | `Vista-De-Trabajo` |
| `EST-72` | `instancia-desconocida` | Instancia desconocida · INSTANCIA_DESCONOCIDA | `Vista-De-Trabajo` |
| `EST-73` | `dimension-no-legible` | Pieza sin dimensión legible · DIMENSION_NO_LEGIBLE | `Vista-De-Trabajo` |
| `EST-74` | `indice-sin-representacion` | Índice sin representación en la escena · INDICE_FUERA_DE_RANGO | `Vista-De-Trabajo` |

## 5. Rutas de navegación (`NAV-XX`)

Veinticuatro rutas, todas recorridas en la maqueta. Los destinos son los de `Experiencia-De-Uso.md` §3.1 y los tres destinos por papel de la barra lateral son los de §3.2. **Ninguna ruta tiene por destino a `SUP-08`.**

| ID | Origen | Disparador | Destino | Qué se preserva al volver |
| --- | --- | --- | --- | --- |
| `NAV-01` | Entrada al producto sin administrador constituido | Resolución del destino inicial | `SUP-01` | — |
| `NAV-02` | `SUP-01` | Éxito del aprovisionamiento | `SUP-03`, con su banda de confirmación | — |
| `NAV-03` | `SUP-01` con administrador ya constituido | Resolución del destino inicial | `SUP-03`, con redirección neutra que **reemplaza** la entrada del historial | — |
| `NAV-04` | `SUP-03` | «¿No tenés cuenta? Registrarte» | `SUP-02` | — |
| `NAV-05` | `SUP-02` | Éxito del registro, o «¿Ya tenés cuenta? Ingresar» | `SUP-03`, con su banda de confirmación en el primer caso | — |
| `NAV-06` | `SUP-03` con contraseña sin establecer | Ingreso aceptado | `SUP-04` en su curso de establecimiento | — |
| `NAV-07` | `SUP-04`, curso de establecimiento | Éxito | `SUP-03`, con su banda de confirmación | — |
| `NAV-08` | `SUP-03` con credencial aceptada, papel alumno | Ingreso | `SUP-05` | — |
| `NAV-09` | `SUP-03` con credencial aceptada, papel administrador | Ingreso | `SUP-10` | — |
| `NAV-10` | Barra lateral del alumno | «Mis trabajos» | `SUP-05` | — |
| `NAV-11` | Barra lateral del alumno | «Trabajo nuevo» | `SUP-06` en su curso de creación | — |
| `NAV-12` | Barra lateral, los dos papeles | «Mi contraseña» | `SUP-04` en su curso de cambio, **conservando el papel** | El papel, para que la barra lateral siga siendo la correcta |
| `NAV-13` | `SUP-04`, curso de cambio | «Cancelar», o éxito del cambio | El panel del papel de la persona: `SUP-05` o `SUP-10` | La sesión, que no se cierra al cambiar la contraseña |
| `NAV-14` | Barra lateral del administrador | «Entrega de la comisión» | `SUP-10` | — |
| `NAV-15` | Barra lateral del administrador | «Cuentas» | `SUP-09` | — |
| `NAV-16` | `SUP-05` | «Abrir» sobre una fila | `SUP-07` **como alumno**, sin bloque de decisión | El origen, para que «volver» vuelva a `SUP-05` |
| `NAV-17` | `SUP-05` | «Editar» sobre una fila en estado `Borrador` | `SUP-06` en su curso de reedición, con los datos y el texto tal como quedaron | El origen |
| `NAV-18` | `SUP-06` | «Enviar» y luego «Ver el trabajo» | `SUP-07` | El origen |
| `NAV-19` | `SUP-06` | «Enviar» y luego «Volver a mis trabajos» | `SUP-05`, donde el trabajo ya figura con su estado nuevo | — |
| `NAV-20` | `SUP-10` | «Abrir» sobre una fila | `SUP-07` **como administrador**, con `SUP-08` alojado si el trabajo está en estado `Pendiente` | El origen, para que «volver» vuelva a `SUP-10` |
| `NAV-21` | `SUP-08` | Confirmación del desenlace, aprobar o rechazar | `SUP-10`, donde el trabajo ya figura con su estado terminal | — |
| `NAV-22` | `SUP-08` | Confirmación del retiro | `SUP-10`, donde el trabajo ya no figura | — |
| `NAV-23` | `SUP-07` | «Volver al listado» | `SUP-05` o `SUP-10`, según el papel de quien mira y el origen declarado | — |
| `NAV-24` | Barra lateral, los dos papeles | «Cerrar sesión» | `SUP-03`, con su banda de sesión cerrada | — |

**Ningún callejón sin salida.** Las once superficies tienen al menos una ruta de salida, incluidas `SUP-01` y `SUP-02`, que no tienen barra lateral. `SUP-11` no es un destino: se superpone al armazón de la superficie en curso y devuelve el control a la misma superficie al recuperarse.

## 6. Lo que la maqueta exhibe y no forma parte de la línea de base

Se declara para que nadie lo construya creyendo que fue aprobado como producto:

| Qué | Por qué no entra |
| --- | --- |
| La **barra de validación de maqueta** con su selector de estado, su interruptor de recarga automática y su vuelta a la portada | Instrumento de la maqueta, rotulado como tal (`Maqueta-Rules.md` §4.3). No se traslada ni a la especificación ni al código |
| El **panel del contrato de fachada** del visor, con los botones de **cinco de las seis** funciones, la insignia de ciclo de vida, los cuatro recuentos, la tabla de las siete condiciones, las seis propiedades transversales y las dos comprobaciones a mano | Instrumento de validación, rotulado como tal. En el producto el componente anfitrión invoca las seis funciones **sin exhibirlas** |
| `index.html`, la portada de la maqueta con el índice de las once superficies y el contrato de campos | Punto de entrada de la maqueta, no superficie del producto |
| La **credencial de la cuenta de administrador de prueba** exhibida a la vista | Instrumento de la maqueta, compuesto para que el Product Owner pudiera recorrer el ingreso |
| La **representación plana de respaldo** de la escena, cuando el navegador no ofrece capacidad gráfica tridimensional | Sí forma parte del producto, pero como realización del estado `EST-25` y no como componente propio: `Wireframes-Vista-De-Trabajo.md` §5 la especifica |

### 6.1 Lo que el contrato declara y esta línea de base **no** validó visualmente

Se declara acá, con su fecha, porque una línea de base que calla un desfase deja de servir como punto de comparación.

| Qué | Cuándo se decidió | Qué se validó el 2026-08-09 | Qué quedó fuera de esa validación |
| --- | --- | --- | --- |
| **`establecerMovimiento(id, opciones)`**, la sexta función de la superficie pública de la fachada de `GeometriaFactory-Visor` | **Después** de la aprobación de la maqueta, en la ronda de decisiones que el `PRODUCT-INTAKE` absorbió en su versión 1.6 y que `Definicion-Contrato-De-Fachada.md` §4.6 especifica | El Product Owner miró y aprobó **los dos movimientos automáticos y su gobierno**: las dos casillas independientes de `CMP-72`, el arranque destildado con preferencia de movimiento reducido, la detención al arrastrar y con la pestaña oculta, la vuelta a la orientación de partida al apagar el giro, y que ninguna combinación altera la disposición | **La función plana en sí.** La maqueta gobierna el movimiento con dos métodos de instancia —`orbitar(v)` y `girarFiguras(v)`—, y el panel del contrato de fachada exhibe cinco botones. `establecerMovimiento` **no aparece en ningún archivo de la maqueta** y **nadie la miró en pantalla** |
| **La capacidad `F-26`** —reseteo de contraseña por el administrador— con todo lo que arrastra: la quinta operación de la fila de `SUP-09`, su diálogo de confirmación, la comunicación de la contraseña provisoria, y el **tercer curso de `SUP-04`**, el cambio forzado sobre el shell de acceso **sin sesión otorgada** | **Después** de la aprobación de la maqueta, en la ronda de decisiones que el `PRODUCT-INTAKE` absorbió en su versión **1.7**, que además retiró la exclusión X-2 y reescribió el caso límite CL-7 | Nada de esto. La maqueta se aprobó con **cuatro** operaciones en la fila de cuentas y con **dos** cursos en la credencial propia | **Todo el alcance de F-26.** Los componentes y estados que `Wireframes-Panel-De-Cuentas.md` y `Wireframes-Credencial-Propia.md` especifican **en su versión vigente** **no tienen `CMP-XX` ni `EST-XX` en esta línea de base**, y no se les asigna uno acá: un identificador de línea de base afirma que alguien lo miró y lo aprobó, y nadie lo miró. También cambió el **texto** de la nota de contraseña olvidada de `SUP-03`, que la maqueta exhibía con la redacción vieja |

**Qué significa para quien construya, en el caso de F-26.** Lo que hay que construir es lo que especifican esos dos wireframes **en su versión vigente** y `CU-04` y `CU-03` en la suya; lo que esta línea de base **no** puede afirmar es que se haya visto en pantalla. Los recuentos de §1 —11 superficies, 73 componentes, 74 estados y 24 rutas— **siguen siendo los de la maqueta aprobada el 2026-08-09** y no se inflan con elementos sin validar. Corresponde una **iteración 5** sobre la maqueta y una reemisión de esta línea de base, junto con el desfase de `establecerMovimiento`.

**Qué significa para quien construya.** La superficie pública que el sistema construido tiene que respetar es la de `Definicion-Contrato-De-Fachada.md` §4, que declara **seis** funciones; `CMP-71` y la sonda `SD-43` están escritos contra esas seis. Lo que esta línea de base **no** puede afirmar es que la sexta se haya visto funcionando: su semántica está declarada en el contrato —opciones parciales conservan el movimiento no nombrado, no abre código de condición nuevo— y no en una validación visual. Si el Product Owner quiere cerrar el desfase, corresponde una **iteración 5** sobre la maqueta y una reemisión de esta línea de base, y no una edición silenciosa de esta tabla.

## 7. Evidencia

| ID | Tipo | Ruta o comando | Ubicación | Fecha |
| --- | --- | --- | --- | --- |
| `EV-01` | `humano` | Aprobación explícita de la maqueta por el Product Owner | [`Bitacora-Validacion-Maqueta.md`](Bitacora-Validacion-Maqueta.md) §3, cierre de la iteración 4 | 2026-08-09 |
| `EV-02` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/` | Los once archivos `<Superficie>.html`, uno por fila de §2 | 2026-08-09 |
| `EV-03` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/assets/js/Maqueta.js` | `ROTULOS_DE_ESTADO`, catálogo de los setenta y cuatro estados de §4 | 2026-08-09 |
| `EV-04` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/assets/js/Maqueta.js` | `DESTINOS`, los tres destinos por papel de `NAV-10` a `NAV-15` | 2026-08-09 |
| `EV-05` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/README.md` | §4, tabla de las once superficies con su CU y su shell; §4.b, recorridos de punta a punta | 2026-08-09 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-13` y la parte del `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanzan a este archivo, contra `PRODUCT-INTAKE` **1.10**. **`F26-13`**: la fila de **F-26** de §6.1 describía el tercer curso de `SUP-04` como «el cambio forzado sobre el shell de acceso **con sesión iniciada**», que es la formulación que `Experiencia-De-Uso.md`, `Glosario-UX.md` y `Wireframes-Credencial-Propia.md` corrigieron en sus versiones 1.2 y que este archivo no había absorbido: pasa a **«sin sesión otorgada»**, que es lo que RN-13 declara. La misma celda remitía a `Wireframes-Panel-De-Cuentas.md` **1.1** y `Wireframes-Credencial-Propia.md` **1.1** como lo que hay que construir, y los dos ya estaban en 1.2 —quien siguiera la remisión literal habría implementado el campo de contraseña que la 1.2 eliminó—: las dos remisiones pasan a nombrar **la versión vigente** en lugar de una versión concreta que envejece. **`F26-27`**: una **línea en blanco partía la tabla** de §6.1 y dejaba la fila de F-26 fuera de ella, que es el contenido más consecuente de la sección; se retira, sin tocar el texto de ninguna fila. **Ningún identificador de línea de base se asigna ni se retira**, y los recuentos de §1 —11 superficies, 73 componentes, 74 estados y 24 rutas— **siguen siendo los de la maqueta aprobada** y no se inflan: lo que §6.1 declara sin validar sigue declarado sin validar, con su iteración 5 pedida. Sube minor. |
| 1.2 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, sin tocar ningún identificador de la línea de base. **§6.1** suma la fila de **F-26**: la quinta operación del panel de cuentas, su diálogo, la comunicación de la provisoria y el tercer curso de la credencial propia **no fueron validados visualmente** —la maqueta se aprobó con cuatro operaciones y dos cursos— y por eso **no reciben `CMP-XX` ni `EST-XX`**: un identificador de línea de base afirma que alguien lo miró, y nadie lo miró. Los cuatro recuentos de §1 quedan intactos por el mismo motivo. **Nada que corregir por F-25**: la corrección del hallazgo `AB2-02` ya había dejado a `CMP-71` y a `CMP-72` diciendo lo que 1.7 confirma —seis funciones, y el anfitrión como quien consulta la preferencia de movimiento reducido y manda dos valores de verdad—. Sube minor: declara un desfase nuevo, sin agregar ni retirar ningún elemento validado. |
| 1.1 | 2026-08-09 | Corrección del hallazgo **`AB2-02`** de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`: `CMP-71` y §6 declaraban **cinco** funciones de la fachada contra un contrato que declara **seis** desde que el intake 1.6 incorporó `establecerMovimiento`. `CMP-71` pasa a seis y suma la atribución al anfitrión de consultar la preferencia de movimiento reducido y de conservar la elección; §6 pasa a decir «cinco de las seis»; y se agrega **§6.1**, que declara explícitamente el desfase: qué se validó el 2026-08-09, cuándo se decidió la sexta función y qué quedó fuera de esa validación. Se precisa además `CMP-72` con la frontera entre el componente anfitrión y la fachada —dos valores de verdad, arranque apagado ante opciones ausentes o parciales—, que es la que `Definicion-Contrato-De-Fachada.md` §3.3 y §4.1 fijan y que la maqueta contradecía (`AB2-05`). Ningún identificador se retira ni se renumera. |
| 1.0 | 2026-08-09 | Emisión inicial, al cierre de la Fase B2 con la maqueta aprobada por el Product Owner. Inventario identificado de once superficies, setenta y tres componentes, setenta y cuatro estados y veinticuatro rutas de navegación, con la declaración de la única superficie alojada, la enumeración de lo que la maqueta exhibe como instrumento y no forma parte de la línea de base, y las cinco evidencias de §7. |
