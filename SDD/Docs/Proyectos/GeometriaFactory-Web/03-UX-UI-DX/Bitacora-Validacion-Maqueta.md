# Bitácora de validación de la maqueta — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Bitacora-Validacion-Maqueta.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Maquetador de validación visual (AG-03M)
**Variante:** UX/UI
**Trazabilidad upstream:** `SDD/Maquetas/GeometriaFactory-Web/` íntegra, en sus cuatro iteraciones y en la ronda de corrección posterior; `Maqueta-Rules.md` §3.5 y §3.6; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-25 y F-26), §17.7 P.3, §17.7 P.10 y su control de cambios 1.5, 1.6 y 1.7; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`; `SDD/Docs/Audit/B2-Maqueta-GeometriaFactory-Web-r1.md`
**Trazabilidad downstream:** [`Linea-Base-Visual.md`](Linea-Base-Visual.md) y [`Contrato-Datos-Maqueta.md`](Contrato-Datos-Maqueta.md), que se emiten desde la maqueta que esta bitácora deja aprobada; `Matriz-Sensado-Deriva.md` de `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Qué registra esta bitácora](#1-qué-registra-esta-bitácora)
- [2. Las cuatro iteraciones](#2-las-cuatro-iteraciones)
- [2.b Lo que pasó después de la aprobación](#2b-lo-que-pasó-después-de-la-aprobación)
- [3. Aprobación](#3-aprobación)
- [4. Hallazgos emitidos y adónde se propagó cada uno](#4-hallazgos-emitidos-y-adónde-se-propagó-cada-uno)
- [5. Lo que quedó sin resolver, y por qué](#5-lo-que-quedó-sin-resolver-y-por-qué)
- [5.b La ronda de corrección de la auditoría](#5b-la-ronda-de-corrección-de-la-auditoría)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué registra esta bitácora

Una entrada por iteración de validación, con su vía —por prompt o manual—, la observación del Product Owner, el cambio aplicado sobre la maqueta y el documento que quedó a retroalimentar. Es el registro que hace auditable el paso 5 de la Fase B2 y el que fundamenta la propagación del paso 6.

**Las dos vías del `Maqueta-Rules.md` §3.5 se ofrecieron explícitamente** en cada entrega de la maqueta: corrección por prompt, y corrección manual con relectura, interpretación enumerada y confirmación previa a propagar. Las cuatro iteraciones se resolvieron por la vía A, por prompt. No hubo correcciones manuales, de modo que no hubo interpretación que confirmar ni corrección manual que preservar.

## 2. Las cuatro iteraciones

### Iteración 1 — La navegación obligaba a resolver sin ver el trabajo

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09 |
| Vía | A, corrección por prompt |
| Observación del Product Owner | La resolución del trabajo aparecía como una pantalla propia, a la que se llegaba navegando, y el docente tenía que decidir el desenlace **sin el trabajo a la vista**. «No puedo aprobar algo que no estoy mirando.» |
| Diagnóstico | La maqueta contradecía al wireframe. `Wireframes-Resolucion-Del-Trabajo.md` §1 ya declaraba que el bloque se aloja dentro de `Vista-De-Trabajo`; la maqueta lo había construido como página suelta. El wireframe tenía razón, pero su enunciado llegaba tarde y en subordinada, después del propósito y del nombre canónico, y su propio rótulo de sección dice «Pantalla» |
| Cambio aplicado | `Vista-De-Trabajo.html` pasa a dibujar el bloque de decisión cuando se la abre con papel de administrador sobre un trabajo en estado `Pendiente`, que es el camino del producto. `Resolucion-Del-Trabajo.html` se conserva porque el wireframe exige demostrar el bloque como recorrido con su mapa de estados propio, y pasa a mostrar el trabajo entero debajo del bloque. Las cuatro partes y el bloque de decisión se arman **una sola vez**, en `pintarCuatroPartes` y `pintarBloqueDeDecision` de `Maqueta.js`, para que una corrección no haya que hacerla dos veces. Se incorpora el paso del contexto por dirección: `?t=`, `?papel=`, `?desde=` y `?estado=` |
| Documento a retroalimentar | `Wireframes-Resolucion-Del-Trabajo.md` §1 y `Glosario-UX.md` §2 |

### Iteración 2 — El contrato de fachada del visor no se podía juzgar

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09 |
| Vía | A, corrección por prompt |
| Observación del Product Owner | `GeometriaFactory-Visor` también tenía `requiere_maqueta` en verdadero. El Product Owner decidió **no darle maqueta propia** y pidió que su validación se integre en ésta, dentro de las superficies que lo usan: el visor no tiene pantallas propias, su superficie pública son cinco funciones planas, y validarlo aislado mostraría una página de prueba que ya existe como sample S-1 |
| Cambio aplicado | Nace el **panel del contrato de fachada**, en la columna de escena y árbol de `Vista-De-Trabajo` y de `Resolucion-Del-Trabajo`, con borde punteado y el rótulo «instrumento de validación, no forma parte del producto». Hace visibles las cinco funciones con su efecto, el ciclo de vida con su identificador de instancia, los cuatro recuentos del resultado de dibujo —incluido **«sin registro»**, que tiene que ser siempre 0—, las siete condiciones con sus dos cursos, las seis propiedades transversales con su umbral, y las dos comprobaciones que se corren a mano: volver a cargar el mismo texto y comparar la disposición, y hacer diez recorridos de ida y vuelta midiendo geometrías, materiales y contextos gráficos vivos. Se suman a la barra de validación los ocho estados que materializan las siete condiciones |
| Documento a retroalimentar | Ninguno de contenido: el panel materializa lo que `Definicion-Contrato-De-Fachada.md` §4, §6 y `Especificacion-Funcional.md` §6 del visor ya declaraban. La decisión de no maquetar el visor por separado se registra en el `README.md` §4 de su categoría 03 |

### Iteración 3 — Absorción del visor real

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09 |
| Vía | A, corrección por prompt |
| Observación del Product Owner | La representación plana no alcanzaba para validar lo que el producto viene a resolver. El Product Owner pidió montar **el visualizador que la cátedra ya usa**, para ver de verdad si los defectos que el producto promete eliminar quedan eliminados |
| Cambio aplicado | Nace `assets/js/Visor-Tridimensional.js`, port del visualizador previo, con Three.js por red de distribución, sin compilación y sin gestor de paquetes. Se absorbe la construcción de los objetos tridimensionales, la escena con sus luces, su grilla y su cámara orbital reimplementada a mano, y el árbol colapsable. **No se absorben tres defectos**: la disposición al azar, que pasa a derivarse del índice de cada pieza; la intolerancia de claves, por la que hoy ningún ortoedro generado por la aplicación se dibuja, y que pasa a tolerar `Bases` y `Tapas`, `Cuadrado` y `Rectangulo`, comas finales y comentarios; y el fallo silencioso, que pasa a enumerar toda pieza no dibujada con su índice y su código. **Además el cero pasa a ser una dimensión legible**: la figura con una dimensión en `0.00` ya no se descarta, que el visualizador previo perdía por evaluar la verdad del número. La representación plana queda como respaldo rotulado, para el caso sin capacidad gráfica |
| Documento a retroalimentar | `Definicion-Contrato-De-Fachada.md` §5.3 y §6, y `CU-02` §5, §6 y §8, del proyecto de código del visor: el cero como dimensión legible no estaba declarado en ninguna parte |

### Iteración 4 — Los dos movimientos automáticos

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09 |
| Vía | A, corrección por prompt |
| Observación del Product Owner | Con el visor real montado, el Product Owner pidió que la escena se mueva sola para leerla mejor, y que el movimiento se pueda apagar. Pidió **dos movimientos separados**: que gire la cámara alrededor del conjunto, como en el visualizador previo, y que giren las piezas sobre su eje, que es lo que le permite ver la cara de atrás sin arrastrar |
| Cambio aplicado | Dos casillas de verificación independientes al pie del área de dibujo, tildables por separado y las dos a la vez, tildadas por omisión. Los dos movimientos se detienen mientras la persona arrastra y con la pestaña oculta; **arrancan destildados si el sistema declara preferencia de movimiento reducido**, y el control lo dice. Al apagar el giro de las figuras, las piezas vuelven a su orientación de partida. La elección se persiste con claves prefijadas `mq-`, para que la medición de «cero persistencia» del panel siga contando 0. **Se verificó en las cuatro combinaciones que ninguno altera la disposición**: cada pieza sigue cayendo en la celda que le da su índice |
| Documento a retroalimentar | `Wireframes-Vista-De-Trabajo.md` §3, §4 y §7; `Experiencia-De-Uso.md` §7; `Glosario-UX.md` §2; y, aguas arriba, `Definicion-Contrato-De-Fachada.md` §3.2, §3.3, §4.1 y su §5.5 nueva, más la categoría 03 del proyecto de código del visor. El `PRODUCT-INTAKE` lo absorbió en su versión 1.5 como capacidad **F-25** |

## 2.b Lo que pasó después de la aprobación

Las cuatro iteraciones de §2 son las que el Product Owner miró. **Después de aprobar hubo dos rondas más de decisiones suyas**, que no se validaron mirando la maqueta y que sin embargo cambiaron documentos aguas arriba. Se registran acá porque una bitácora que se detiene en la aprobación deja el cierre de la fase sin trazabilidad.

### Ronda posterior 1 — la sexta función de la fachada y la condición de medición del NFR

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09, posterior a la aprobación de §3 |
| Vía | A, corrección por prompt. **No hubo validación visual**: la decisión se tomó sobre el texto del contrato, no mirando la maqueta |
| Observación del Product Owner | (a) Gobernar el movimiento con la vía declarada hasta entonces —destruir la instancia, reinicializarla con opciones nuevas y recargar el texto— **perdía la selección de pieza** y producía un parpadeo cada vez que alguien tocaba una casilla. Pidió una función que prenda y apague los dos movimientos **sobre la instancia viva**. (b) El NFR de cero peticiones de red se podía medir con los movimientos apagados, y entonces la medición pasaba sin haber ejercitado nunca el bucle de dibujo |
| Cambio aplicado | (a) La superficie pública de la fachada pasa de cinco a **seis funciones**, con `establecerMovimiento(id, opciones)`. No abre ningún código de condición nuevo: con un identificador que no corresponde a una instancia viva informa `INSTANCIA_DESCONOCIDA`, que ya existe, y los siete códigos siguen cerrados. (b) El umbral del NFR no cambia —exactamente 0— pero pasa a medirse **con los dos movimientos prendidos**, que es el peor caso. **Sobre la maqueta no se aplicó ningún cambio**: sigue gobernando el movimiento con dos métodos de instancia, `orbitar(v)` y `girarFiguras(v)` |
| Documento a retroalimentar | `PRODUCT-INTAKE` §17.7 P.3, que lo absorbió en su versión **1.6**; `Definicion-Contrato-De-Fachada.md` §4.6, §6 y §3.3; `Especificacion-Funcional.md` §6 y los `CU` alcanzados del proyecto de código del visor; `Guia-Onboarding-Developer.md` y `DX-Developer-Experience.md`; y, acá, `Linea-Base-Visual.md` §6.1 y la sonda `SD-43` |

### Ronda posterior 2 — las cinco decisiones consolidadas en el intake 1.7

| Campo | Contenido |
| --- | --- |
| Fecha | 2026-08-09, al revisar los entregables de la Fase B2 |
| Vía | A, corrección por prompt. **Tampoco hubo validación visual** |
| Observación del Product Owner | Cinco decisiones, de las cuales tres tocan lo que esta fase produjo: que el olvido de contraseña no puede costar la cursada entera; que el movimiento automático es comportamiento propio del bundle y la órbita ya existe hoy, así que diferirla sería portar quitando algo que funciona; y que la frontera del movimiento tiene que quedar dicha sin ambigüedad |
| Cambio aplicado | En el intake 1.7: **(a)** entra **F-26**, reseteo de contraseña por el administrador con contraseña provisoria, `Must Have`; retira la exclusión `X-2` y reescribe el caso límite `CL-7`. **(b)** **F-25 sube de `Should Have` a `Must Have`**. **(c)** **Frontera del movimiento**: el componente anfitrión manda **dos valores de verdad** por la fachada y el bundle **no consulta nada** —quien lee `prefers-reduced-motion` es el anfitrión—. **(d)** Se incorpora el escenario **E-8** para `DIMENSION_NO_LEGIBLE`, que cierra el punto abierto `H-5`. **(e)** El objetivo de avance pasa de «7 de 7 etapas» a «8 de 8». Sobre la maqueta, (c) **sí** se aplicó: ver §5.b |
| Documento a retroalimentar | `PRODUCT-INTAKE` 1.7 lo consolidó, y **la propagación al nivel producto ya está hecha**: el orquestador la completó el 2026-08-09. `Roadmap-Producto.md` y `NB-06` recogieron la promoción de F-25 a `Must Have` en su versión **1.3**, y `Alcance-Producto.md` y `Necesidades-Negocio.md` —que habían quedado afuera de esa primera pasada y seguían declarando F-25 `Should Have` y no comprometida— la recogieron en sus versiones **1.3**, junto con la incorporación de F-26 al nivel producto. Esta bitácora **no propaga nada de eso**: son documentos de nivel producto, suben versión con archivado y su propagación es del orquestador |

**Ninguna de las dos rondas se validó mirando la maqueta.** Está declarado acá y en [`Linea-Base-Visual.md`](Linea-Base-Visual.md) §6.1, con el detalle de qué quedó fuera de la validación del 2026-08-09.

## 3. Aprobación

**El Product Owner aprobó la maqueta explícitamente el 2026-08-09**, al cierre de la iteración 4, sobre las once superficies con el visor real montado, el panel del contrato de fachada operativo y los dos movimientos automáticos gobernables. La aprobación no se infiere de ningún silencio.

Lo que quedó aprobado está inventariado en [`Linea-Base-Visual.md`](Linea-Base-Visual.md) y en [`Contrato-Datos-Maqueta.md`](Contrato-Datos-Maqueta.md), y convertido en comprobaciones en `../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`.

## 4. Hallazgos emitidos y adónde se propagó cada uno

Las cuatro iteraciones produjeron seis hallazgos; la ronda de corrección posterior sumó un séptimo, `H-7`, que es del catálogo del framework y no del producto. La columna de destino es la lectura de la matriz de propagación de `Maqueta-Rules.md` §3.6.

| ID | Hallazgo | Clase según la matriz de §3.6 | Adónde se propagó |
| --- | --- | --- | --- |
| `H-1` | Contraste insuficiente de un color del catálogo de diseño: el sello de versión, con su jerarquía baja, no llegaba a 4.5:1 con el token terciario de texto | Patrón o token visual transversal · destino `template` | **No es de este producto.** La maqueta lo esquivó usando el token secundario, y el defecto quedó registrado como **observación al framework** sobre `IA.SDD/SDD/Devs/References/Design/`, que es repositorio fuente y de sola lectura para esta fase. No se corrigió ahí |
| `H-2` | Coma contra punto: los esquemas dibujaban los valores `declarado` y `derivado` reformateados a coma decimal, contra la excepción que los propios documentos enuncian | Criterio de presentación de un campo · destino 03 | `Wireframes-Vista-De-Trabajo.md` §2, `Wireframes-Envio-De-Trabajo.md` §2 y `Representacion-Lista-De-Observaciones.md` §1, §2 y §5, con la nota que declara el punto como deliberado |
| `H-3` | Dueño contradictorio de un trabajo: `Primer intento` figuraba como de una alumna en un wireframe y de otro alumno en otro, con la misma fecha, el mismo estado y los mismos recuentos | Dato de ejemplo inconsistente entre superficies · destino 03 | `Wireframes-Listado-De-La-Comision.md` §2 y `Wireframes-Panel-De-Trabajos-Del-Alumno.md` §2, con la declaración de cuál de los dos documentos es dueño del conjunto de datos de ejemplo |
| `H-4` | Recuentos que no correspondían a ningún dato declarado: cantidades de piezas y de advertencias, recuento de trabajos por grupo, fila del panel de resumen y un guion de prueba que describía un caso que su propio documento no contenía | Ídem · destino 03 | `Wireframes-Listado-De-La-Comision.md` §2 y §8, `Wireframes-Panel-De-Trabajos-Del-Alumno.md` §2 y `Representacion-Lista-De-Observaciones.md` §2 |
| `H-5` | `DIMENSION_NO_LEGIBLE` es la única de las siete condiciones del contrato de fachada **sin escenario propio** en el `PRODUCT-INTAKE` §20 ni fila en su §21. La maqueta la demostró con un texto compuesto | Alcance de datos de prueba · destino `PRODUCT-INTAKE` | **Quedó como punto abierto del intake 1.5.** Esta fase no lo resuelve: corresponde al Product Owner decidir si se incorpora un escenario `E-8` o si la condición queda declarada sin dato de prueba |
| `H-6` | El **giro de las figuras no existe** en el visualizador previo: el original mueve la cámara y deja las piezas quietas. Es comportamiento no declarado en la especificación, incorporado por decisión del Product Owner durante la validación visual | Alcance funcional que la maqueta mostró faltante · destino 00, 01, 02, 03, 06, 07 | El `PRODUCT-INTAKE` lo absorbió como **F-25** en su versión 1.5. En 03: `Wireframes-Vista-De-Trabajo.md`, `Experiencia-De-Uso.md` y `Glosario-UX.md` de este proyecto de código, y la categoría 03 del proyecto de código del visor. En 02: `Definicion-Contrato-De-Fachada.md` §5.5, que declara el gobierno de los dos movimientos. **`00-Contexto` y `01-Necesidades-Negocio` se informaron primero y se tocaron después, con confirmación del humano**, que es el curso completo de la regla de corte de `Maqueta-Rules.md` §3.6: `Alcance-Producto.md`, `Roadmap-Producto.md`, `Necesidades-Negocio.md` y `NB-06` pasaron a **1.2** el 2026-08-09, con su 1.1 archivada en `_legacy/2026-08-09/` y con entrada de control de cambios que cita la Fase B2. Ver §5 |
| `H-7` | **El catálogo de diseño no tiene token de borde para el estado de atención.** La hoja de estilos de la maqueta lo había resuelto inventando un color propio, `#E8D3AE`, para el borde de la banda de atención y del cartel de reconexión | Patrón o token visual transversal · destino `template` | **No es de este producto.** La maqueta lo esquivó pasando a usar `--color-text-warning`, que sí existe en el catálogo, y el faltante quedó registrado como **observación al framework** sobre `IA.SDD/SDD/Devs/References/Design/`, que es repositorio fuente y de sola lectura para esta fase. Mismo tratamiento que `H-1`. Emitido en la ronda de corrección de la auditoría, no en las cuatro iteraciones |

Además de los hallazgos, la iteración 3 produjo una corrección que no se emitió como hallazgo numerado y que sí se propagó: **el cero como dimensión legible**, ausente de toda la documentación del visor y necesaria para que el escenario `E-6` del intake §20 no se contradiga.

## 5. Lo que quedó sin resolver, y por qué

| Qué | Por qué no se resolvió acá |
| --- | --- |
| **`H-6` alcanzaba a `00-Contexto` y a `01-Necesidades-Negocio`** | **Ya no está sin resolver.** La regla de corte de `Maqueta-Rules.md` §3.6 obligó a detenerse, informar el alcance real y pedir confirmación, y eso se hizo: informado el alcance, **el humano confirmó y las dos categorías se tocaron el 2026-08-09**. `Alcance-Producto.md` y `Roadmap-Producto.md` pasaron a **1.2** con su 1.1 archivada en `00-Contexto/_legacy/2026-08-09/`; `Necesidades-Negocio.md` y `NB-06` pasaron a **1.2**, con `Necesidades-Negocio-v1.1.md` archivada en `01-Necesidades-Negocio/_legacy/2026-08-09/`. Las cuatro entradas de control de cambios citan la Fase B2 |
| **`H-5`, la condición sin escenario de datos propio** | **Resuelto por el Product Owner.** El `PRODUCT-INTAKE` **1.7** incorpora el escenario **`E-8`** para `DIMENSION_NO_LEGIBLE`. El punto quedó abierto en 1.5 y cerrado en 1.7 |
| **`H-1`, el contraste del token del catálogo** | El catálogo vive en el repositorio fuente `IA.SDD`, que es de sola lectura para esta fase. Se registra como observación al framework |
| **`H-7`, el token de borde de atención que el catálogo no tiene** | Mismo motivo que `H-1`: el catálogo es de sola lectura para esta fase. La maqueta usa mientras tanto un token existente |
| **El gobierno del movimiento con la instancia viva** | **Ya no es punto abierto.** El intake §17.7 P.3 fijaba la superficie pública en cinco funciones; el intake **1.6** la extendió a **seis** con `establecerMovimiento(id, opciones)`, y `Definicion-Contrato-De-Fachada.md` §4.6 la especifica. Lo que **sí** queda declarado, y no es lo mismo, es que esa sexta función **no fue validada visualmente**: ver §2.b y `Linea-Base-Visual.md` §6.1 |
| **La propagación del intake 1.7 y 1.9 a `00-Contexto` y a `01-Necesidades-Negocio`** | **Ya no está sin resolver, y esta fila registra qué pasó porque la versión 1.1 de esta bitácora lo declaró mal.** Decía que `Roadmap-Producto.md` §3 punto 4 y `NB-06` §5 y §9 «siguen escritos contra el intake 1.5», cuando esos dos documentos **se habían corregido en el mismo commit** que subió esta bitácora a 1.1; y no nombraba lo que sí seguía pendiente, que era `Alcance-Producto.md` y `Necesidades-Negocio.md`. Lo detectó la auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` como hallazgo **`F26-12`**. El estado real, verificado el 2026-08-09: los cuatro documentos están propagados —`Roadmap-Producto.md` 1.4, `Alcance-Producto.md` 1.3, `Necesidades-Negocio.md` 1.3 y `NB-06` 1.4—, F-25 figura como `Must Have` comprometida y **F-26 entró al nivel producto** con su etapa, su fila de alcance y su necesidad de negocio, que es `NB-02`. La propagación fue del orquestador, no de esta fase |

## 5.b La ronda de corrección de la auditoría

El 2026-08-09, después de la aprobación y de las dos rondas de §2.b, la auditoría independiente `SDD/Docs/Audit/B2-Maqueta-GeometriaFactory-Web-r1.md` **rechazó** la fase por un P0 y enumeró dieciocho hallazgos. La corrección **no es una iteración de validación**: el Product Owner no volvió a mirar la maqueta y esta bitácora no le atribuye ninguna observación nueva. Lo que se tocó sobre la maqueta:

| Qué se corrigió | Por qué |
| --- | --- |
| La **frontera bundle/anfitrión del movimiento automático** | El visor traía `prefiereMovimientoReducido()` **dentro del bundle** y, con opciones ausentes, la instancia nacía con los movimientos **prendidos**. Las dos cosas contradicen `Definicion-Contrato-De-Fachada.md` §3.3 y §4.1. Se quitó la consulta del bundle, la instancia pasa a arrancar **apagada** ante opciones ausentes o parciales, y quien lee `prefers-reduced-motion` es la página, que le manda dos valores de verdad. **El contrato no se aflojó: se aplicó**, y coincide con la decisión (c) del intake 1.7 |
| Un **dato del dominio compuesto dentro de un HTML** | `Envio-De-Trabajo.html` componía la pieza no dibujada de respaldo en la propia superficie. Pasó a `Datos-Maqueta.js`, que es la fuente única, con su origen declarado en `Wireframes-Envio-De-Trabajo.md` §2. Se neutralizaron además los dos textos de ejemplo de campo que replicaban datos de ejemplo |
| **Literales visuales ad hoc** | Nueve literales de color fuera del bloque `:root`, tipografía fuera de la escala y ciento treinta y cinco atributos `style=` en línea. Los cuatro colores que no existían como token se reemplazaron por tokens del catálogo —el faltante quedó como `H-7`—, la tipografía volvió a la escala y los `style=` se consolidaron en clases que sólo materializan tokens. Hoy **ningún HTML ni guion de la maqueta lleva `style=`** |
| **Accesibilidad de la escena y del árbol** | El lienzo pasa a ser alcanzable por teclado —flechas para orbitar, `+` y `-` para acercar, Inicio para volver a la vista de partida— y la rueda del mouse deja de capturarse salvo con el lienzo enfocado. El árbol pasa a tener **un solo portador de rol y de estado por nodo**, con tabindex móvil. El panel del contrato de fachada devuelve el foco después de refrescarse. La ayuda de las dos casillas de movimiento pasa a `aria-describedby` |
| **Dos citas equivocadas** | La referencia cruzada del código a un hallazgo (`H-2` por `H-6`) y el origen de un dato de ejemplo, que apuntaba al documento que no es su dueño |

El sello de la maqueta lo declara: `iteración del 2026-08-09 · r1-correccion-de-auditoria`.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: una **línea en blanco partía la tabla** de §4 y dejaba fuera de ella la fila `H-7`, la del token de diseño que el catálogo no tenía. Se retira, **sin tocar el texto de ninguna fila**. Deja además constancia de las **dos correcciones aplicadas sobre los archivos de la maqueta** en la misma pasada, que esta bitácora es el lugar de registrar: **`F26-22`**, en `assets/js/Maqueta.js`, donde `resaltarPorIndice()` escribía `aria-selected` también sobre `b.parentNode` —el `<ul role="group">`, que no es seleccionable— contra el propio comentario del archivo «UN SOLO PORTADOR DE ROL Y DE ESTADO»; el arrastre se retira y el camino gemelo `seleccionar()`, que ya estaba bien, no se toca. Con eso `AB2-08(b)` queda cerrado en el instrumento y no sólo en la declaración. **`F26-29`**, en `assets/css/Estilos-Maqueta.css`, donde quedaban **tres `rgba()` fuera del bloque `:root`** contra la afirmación absoluta del propio archivo «no hay literales visuales ad hoc»: se promueven a tokens —`--color-border-on-brand`, `--color-text-on-brand-soft` y `--color-backdrop`—, y **ninguno es un color nuevo**: son `--color-text-on-brand` y `--color-text-primary` con opacidad. **Ningún hallazgo, ninguna iteración, ninguna decisión del Product Owner, ningún recuento de esta bitácora y ningún identificador de la línea de base cambia**, y el rendido de las doce superficies es el mismo. Sube minor. |
| 1.2 | 2026-08-09 | Corrección del hallazgo **`F26-12`** de la auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, que constató que la versión 1.1 —emitida justamente para corregir tres afirmaciones falsas sobre el estado de otros archivos— **escribió dos afirmaciones falsas nuevas**, en §2.b y en §5: las dos declaraban que `Roadmap-Producto.md` §3 punto 4 y `NB-06` §5 y §9 «siguen escritos contra el intake 1.5», cuando los dos ya se habían corregido **en el mismo commit** que subió esta bitácora a 1.1, y ninguna de las dos nombraba lo que sí seguía sin propagar, que era `Alcance-Producto.md` y `Necesidades-Negocio.md`. Las dos filas se reescriben contra el estado verificado del árbol el 2026-08-09, con la versión de cada uno de los cuatro documentos y con la constancia de que la propagación de F-26 al nivel producto —alcance, roadmap y necesidad de negocio— también quedó hecha. **La fila 1.1 no se toca**: es registro de lo que esa versión declaró, y su acreditación de haber verificado las afirmaciones «una por una contra los archivos» queda como está, porque el hallazgo consiste precisamente en que esa verificación no se hizo. Ningún hallazgo, ninguna iteración y ninguna decisión del Product Owner de esta bitácora cambia. |
| 1.1 | 2026-08-09 | Corrección del hallazgo **`AB2-03`** de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, que constató tres afirmaciones falsas y la ausencia de registro del cierre. **Las tres afirmaciones se verificaron una por una contra los archivos y se reescribieron**: (a) `00-Contexto` y `01-Necesidades-Negocio` **no** «quedaron sin tocar» —los cuatro documentos alcanzados están en 1.2, fechados 2026-08-09, con archivado en `_legacy/2026-08-09/`—; (b) el intake **no** fija la superficie pública en cinco funciones —la extendió a seis en 1.6— y el punto abierto del gobierno del movimiento quedó cerrado; (c) la trazabilidad de cabecera apuntaba al intake 1.5 y hoy corresponde **1.7**. Se agrega **§2.b**, con las dos rondas de decisiones posteriores a la aprobación —la sexta función y la condición de medición del NFR, consolidadas en el intake 1.6; y las cinco decisiones consolidadas en el intake 1.7—, cada una con su vía, su observación y sus documentos retroalimentados, y con la constancia de que **ninguna de las dos se validó mirando la maqueta**. Se agrega **§5.b**, la ronda de corrección de la auditoría, que no es iteración de validación. `H-5` pasa de punto abierto a resuelto por el escenario `E-8` del intake 1.7. Se suma el hallazgo **`H-7`**, faltante de token de borde de atención en el catálogo del framework, con destino `template` y mismo tratamiento que `H-1`. Queda declarado como pendiente del orquestador lo que el intake 1.7 obliga a propagar a nivel producto. |
| 1.0 | 2026-08-09 | Emisión inicial, al cierre de la Fase B2. Registra las cuatro iteraciones de validación con su vía, la observación del Product Owner, el cambio aplicado y el documento retroalimentado; la aprobación explícita del 2026-08-09; los seis hallazgos con la lectura de la matriz de propagación de `Maqueta-Rules.md` §3.6 y el destino de cada uno; y los cuatro asuntos que quedaron sin resolver, con el alcance concreto del que alcanza a las categorías de nivel producto. |
