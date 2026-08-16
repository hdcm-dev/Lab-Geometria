# Auditoría de la Fase B2 — Validación visual de maqueta · GeometriaFactory-Web · ronda 2

| Campo | Valor |
| --- | --- |
| Fase | B2 — Validación visual de maqueta (última fase del nivel 1) |
| Proyecto de código | `GeometriaFactory-Web` (`web-monolith`), con la validación del contrato de fachada de `GeometriaFactory-Visor` integrada en la misma maqueta por decisión del Product Owner |
| Rama auditada | `sdd/cierre-de-huecos` |
| Alcance auditado | `SDD/Maquetas/GeometriaFactory-Web/` íntegra; `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md` de `GeometriaFactory-Web/03-UX-UI-DX/`; `Matriz-Sensado-Deriva.md` de `08-Calidad-Y-Pruebas/`; los documentos de `GeometriaFactory-Visor` alcanzados por los hallazgos de r1; `SDD/Docs/00-Contexto/`, `SDD/Docs/01-Necesidades-Negocio/`, `SDD/Docs/Producto/Vista-Producto.md`, `SDD/Docs/Handoff-Checkout.md`; `SDD/Intake/` (intake 1.26, manifiesto 1.2) |
| Motivo de la ronda | Dictaminar si se levanta el rechazo de `B2-Maqueta-GeometriaFactory-Web-r1.md` 1.0, que devolvió la fase con **18 hallazgos** (1 P0, 8 P1, 4 P2, 5 P3) |
| Criterio de la ronda | **El instrumento, no la declaración.** Ningún hallazgo se da por cerrado porque un control de cambios diga haberlo cerrado. La cobertura del P0 se recontó por extracción propia sobre el texto vivo de la matriz; el contraste declarado se recalculó; la frontera del movimiento se leyó en el código y no en la prosa |
| Fuera de alcance | Lo que las fases C a H decidieron sobre otros proyectos de código; sus informes propios ya lo cierran |
| Auditor | Auditor independiente (Arquitecto de Soluciones + QA Senior), invocado desde cero, sin participación en la generación de la fase ni en la corrección de sus hallazgos |
| Fecha | 2026-08-11 |
| Informes anteriores | `B2-Maqueta-GeometriaFactory-Web-r1.md` (leído, **no modificado**) |

---

## 0. Esta ronda se emite tardíamente, y hay que decir por qué

**Este informe llega dos días tarde y su ausencia fue, hasta hoy, un hueco de proceso del framework, no un detalle de archivo.**

La ronda 1 se emitió el **2026-08-09** con veredicto **RECHAZADO**. Las correcciones se hicieron —viajaron en los commits `43245da` y `95c79c4`— y el trabajo siguió adelante: las Fases C, D, E, F, G y H se generaron y se auditaron sobre una línea de base visual y una matriz de sensado que **nacieron de una fase formalmente rechazada**. Nadie emitió la ronda 2 que levantara el rechazo. En el intervalo, el manifiesto (§5, `requiere_maqueta`) y `Linea-Base-Visual.md` §1 siguieron declarando la maqueta aprobada, y `Vista-Producto.md` llegó a declarar el rechazo **levantado por un informe que no lo levanta**.

El hueco lo detectó y lo declaró correctamente `Handoff-Checkout.md`, salvedad **`B-2`**: «La Fase B2 de la maqueta de `GeometriaFactory-Web` tiene un solo informe y su veredicto es RECHAZADO … **no existe un `B2-Maqueta-GeometriaFactory-Web-r2.md`** que levante el rechazo». Esta ronda existe para cerrar esa salvedad.

La tardanza tiene una consecuencia que conviene declarar antes de los resultados: **esta ronda no audita el árbol de entonces sino el de hoy**. Entre r1 y esta emisión la maqueta y sus documentos de deriva absorbieron siete fases más, dos capacidades nuevas y catorce versiones de intake. Por eso §3 no se limita a los dieciocho hallazgos: absorbe lo que pasó después, que es donde están los hallazgos nuevos.

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los dieciocho hallazgos de r1, uno por uno](#2-los-dieciocho-hallazgos-de-r1-uno-por-uno)
- [3. Lo que cambió después de r1, y qué efecto tiene](#3-lo-que-cambió-después-de-r1-y-qué-efecto-tiene)
- [4. La maqueta como código](#4-la-maqueta-como-código)
- [5. Coherencia con lo que el resto del corpus afirma sobre la Fase B2](#5-coherencia-con-lo-que-el-resto-del-corpus-afirma-sobre-la-fase-b2)
- [6. Hallazgos nuevos](#6-hallazgos-nuevos)
- [7. Dictamen](#7-dictamen)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Resumen ejecutivo

**El P0 está cerrado, y cerrado bien.** `Matriz-Sensado-Deriva.md` afirmaba cubrir los doscientos once identificadores de la línea de base con una enumeración que no resolvía para `CMP-04`, `CMP-05`, `CMP-07` y `CMP-08`. **Conté la cobertura yo mismo**, extrayendo la columna de elemento de las sesenta y dos filas vivas y expandiendo todos los rangos: **11 de 11 `SUP-XX`, 73 de 73 `CMP-XX`, 74 de 74 `EST-XX`, 24 de 24 `NAV-XX` y 29 de 29 `DM-XX`. Ningún identificador queda fuera.** Los cuatro que faltaban los cubre `SD-61`, una fila **nueva y con sustancia** —afirmación, método, evidencia esperada y umbral propios—, y no una anotación en una fila existente para que la cuenta cerrara. La propia matriz declara esa distinción y la funda. Verifiqué además que los recuentos de familia no fueran una afirmación interna: `Linea-Base-Visual.md` enumera efectivamente 11 `SUP-XX`, 73 `CMP-XX`, 74 `EST-XX` y 24 `NAV-XX`, y `Contrato-Datos-Maqueta.md` 29 `DM-XX`. 11+73+74+24+29 = 211.

De los **dieciocho hallazgos, diecisiete están cerrados y verificados sobre el instrumento**; el restante, `AB2-18`, es un **punto abierto correctamente declarado** —el código de la maqueta anota por qué la exclusión por prefijo es inocua allí y `SD-47` exige que en el sistema construido sea por espacio de nombres—, y por el criterio negativo de esta auditoría **no es un hallazgo**.

La maqueta como código pasa los cinco criterios que r1 exigía, comprobados por conteo y no por lectura de prosa: **cero atributos `style=`** en los doce HTML y en los tres guiones (eran 135), **cero literales de color fuera de `:root`** —el único `#` que sobrevive fuera del bloque está dentro de un comentario—, **cero tipografías fuera de la escala**, **un solo portador de rol y de estado por nodo del árbol** con `tabindex` móvil, **lienzo operable por teclado** (flechas, `+`/`-`, `Inicio`) con la rueda capturada **sólo con el lienzo enfocado**, y el dato de dominio saliendo únicamente de `Datos-Maqueta.js`. La frontera del movimiento quedó como el Product Owner decidió y como el contrato manda: `Visor-Tridimensional.js` **no lee `prefers-reduced-motion`**, arranca con los dos movimientos apagados ante opciones ausentes, y quien consulta la preferencia y manda dos valores de verdad es `Maqueta.js`, el anfitrión.

El desfase de la sexta función **está declarado y es verdadero**: `Linea-Base-Visual.md` §6.1 lo declara con su fecha, `CMP-71` lo repite, `SD-43` la sensa contra el contrato y no contra la maqueta, y —esto es lo que lo hace verificable y no declarativo— **la propia maqueta lo dice en pantalla**: el panel de fachada se titula «cinco de las seis funciones» y lleva la nota de que `establecerMovimiento` no fue validada visualmente. El hueco de F-26 también está declarado, en §6.1 y en §4 de la matriz, con la iteración 5 como camino y con la negativa explícita a asignarle identificadores de línea de base a algo que nadie miró. **No está disimulado.**

Se registran **cuatro hallazgos nuevos: 1 P1, 2 P2 y 1 P3**. Ninguno es P0 y ninguno toca el instrumento de sensado en su capacidad de sensar. El P1 no está en la fase sino en su registro: `Vista-Producto.md` declara el rechazo de B2 levantado por `F26-Propagacion-r2.md`, que levanta el suyo propio y no éste.

**Veredicto: APROBADO. Se levanta el rechazo de `B2-Maqueta-GeometriaFactory-Web-r1.md`.**

---

## 2. Los dieciocho hallazgos de r1, uno por uno

Estado: **Cerrado** / **Declarado abierto** (punto abierto correctamente declarado, que por el criterio negativo no es hallazgo) / **Abierto**.

| # | Sev. r1 | Qué exigía | Estado | Cómo lo comprobé |
| --- | --- | --- | --- | --- |
| `AB2-01` | **P0** | La matriz declara cubrir 211 identificadores y cuatro `CMP` no están en ninguna fila | **Cerrado** | **Recuento propio**, no lectura de la nota: extraje la columna «Elemento de línea de base» de las **62** filas `SD-XX` vivas, expandí todos los rangos `X-nn a X-mm` y sumé por familia. Resultado: `SUP` 11/11, `CMP` **73/73**, `EST` 74/74, `NAV` 24/24, `DM` 29/29, sin faltantes. `CMP-04`, `CMP-05`, `CMP-07` y `CMP-08` los cubre **`SD-61`** (matriz `:128`), fila nueva con afirmación propia —los cuatro componentes transversales del recorrido de acceso, superficie por superficie—, método (inspección más recorrido con lector de pantalla), evidencia esperada y umbral gradado. Contrasté los totales contra las fuentes: `Linea-Base-Visual.md` enumera 11 `SUP-XX`, 73 `CMP-XX`, 74 `EST-XX`, 24 `NAV-XX`; `Contrato-Datos-Maqueta.md`, 29 `DM-XX`. La nota de cobertura de §4 pasó de párrafo a **tabla verificable** y declara aparte que `SD-62` no cubre ninguno de los 211 |
| `AB2-02` | P1 | Línea de base y matriz declaran cinco funciones contra un contrato de seis; y declarar que la sexta no se validó | **Cerrado** | `CMP-71` (`Linea-Base-Visual.md:141`) dice **seis** y las enumera, atribuye al anfitrión consultar la preferencia y conservar la elección, y remite a §6. §6 (`:268`) dice «**cinco de las seis**» y explica por qué el panel exhibe cinco. **`Linea-Base-Visual.md` §6.1 existe** (`:273`) y declara el desfase con su fecha. `SD-43` de la matriz sensa contra el contrato. Ver §3.2 para la verificación de que la declaración es **verdadera** y no sólo presente |
| `AB2-03` | P1 | La bitácora miente en tres afirmaciones y no registra la ronda posterior a la aprobación | **Cerrado** | La bitácora está en 1.4 con **§2.b** («Lo que pasó después de la aprobación», con las dos rondas posteriores y la constancia de que ninguna se validó mirando la maqueta) y **§5.b** («La ronda de corrección de la auditoría», que declara explícitamente que **no es iteración de validación** y que el Product Owner no volvió a mirar). Las tres afirmaciones falsas de §5 están reescritas con su estado real y verificadas: `H-6` declara los cuatro documentos en 1.2 con archivado; el punto del gobierno del movimiento declara la sexta función y remite a §6.1; la trazabilidad de cabecera ya no apunta al intake 1.5 sino al **1.14** |
| `AB2-04` | P1 | F-25 `Should Have` comprometida de hecho por CA de casos de uso `Must` | **Cerrado** | Se eligió la primera de las dos vías de r1 y se ejecutó: el Product Owner **promovió F-25 a `Must Have`** en el intake 1.7. `Roadmap-Producto.md` §5.2 pasó de **seis a siete criterios** en la transición `g` → `h`, con el gobierno de los dos movimientos como séptimo; `NB-00006` §3 y §5 dejaron de afirmar que diferirla «no la compromete» y declaran que la transición no cierra sin ella. La cadena de contradicción que r1 describió ya no existe. (El cierre por instrumento lo ejecutó y verificó además `F26-Propagacion-r2.md` §`F26-04`; lo reverifiqué sobre el texto vivo de `NB-00006` y del roadmap) |
| `AB2-05` | P1 | La maqueta contradice el contrato en la frontera bundle/anfitrión del movimiento | **Cerrado** | **Leído en el código.** `Visor-Tridimensional.js:333-338`: el comentario declara que el visor «NO lee `prefers-reduced-motion`» por G-3, y **la función `prefiereMovimientoReducido()` ya no existe** en el archivo —`grep` sobre la maqueta entera devuelve la preferencia sólo en `Maqueta.js:610` y en el `@media` del CSS—. `:434-435` pasó de `(opciones.orbitaCamara === undefined) ? !prefiereMovimientoReducido() : …` a **`var orbitaCamara = !!opciones.orbitaCamara;`**, ídem `giroDeFiguras`: con opciones ausentes o parciales arranca **apagado**, que es §4.1 del contrato. `Maqueta.js:601-612` declara y ejerce el otro lado: el anfitrión consulta la preferencia, persiste la elección con claves `mq-` y manda **dos valores de verdad**. Ver §3.3 |
| `AB2-06` | P1 | Un dato del dominio compuesto dentro de un HTML | **Cerrado** | `Envio-De-Trabajo.html:243-246`: el respaldo dejó de componerse en la superficie y pasa a `(b.piezasNoDibujadas \|\| [])`, con el valor viviendo en `Datos-Maqueta.js:551` (`{ indice: 1, motivo: 'tipo no dibujable', codigo: 'TIPO_NO_DIBUJABLE' }`). El `placeholder="por ejemplo, ana"` de `Panel-De-Cuentas.html` ya no existe: hoy dice `"nombre, apellido o correo"`. Barrido de los doce HTML por nombres, correos y literales de figura: lo único que queda son **identificadores de selección** (`T-1`, `C-1`) que eligen qué registro de `Datos-Maqueta.js` mostrar, que es lo contrario de hardcodear el dato. Las tres afirmaciones que r1 declaró falsas vuelven a ser verdaderas |
| `AB2-07` | P1 | Literales visuales ad hoc: nueve colores, tipografía fuera de escala, 135 `style=` | **Cerrado** | **Por conteo.** `grep -o 'style="' *.html` → **0** (eran 135); ídem en los tres `.js` → 0; `.style.` asignado desde JS → 0. Extraje el bloque `:root` y busqué colores en el resto del CSS: **una sola ocurrencia, `#04342C`, y está dentro de un comentario** (`:29`). Los cuatro colores sin token —`#E8D3AE`, `#F4F9F7`, `#FFD9A6`, `#CFE6DE`— no existen en ningún archivo de la maqueta. `font-size` con literal numérico fuera de `var()` → 0: la portada volvió a la escala. El faltante real de catálogo quedó como `H-7`, con destino `template`, que es la vía que r1 indicó |
| `AB2-08` | P1 | Escena inalcanzable por teclado, ARIA doble en el árbol, foco perdido en el panel, ayuda sólo en `title` | **Cerrado**, los cuatro tramos | (a) `Visor-Tridimensional.js:373` `lienzo.setAttribute('tabindex','0')` y `:677` `addEventListener('keydown', alTeclear)`, con `alTeclear` resolviendo flechas para orbitar, `+`/`-` para acercar e `Inicio` para volver a la vista de partida. (b) La rueda: `alRodar` abre con **`if (document.activeElement !== lienzo) { return; }`** antes del `preventDefault()`, de modo que ya no se captura sin foco. (c) El árbol: `Maqueta.js:1140`, `:1153` y `:1189` emiten `<li role="treeitem">` **con el estado en el mismo nodo** y el interior degradado a `<span>` de presentación; `:1214` implementa el `tabindex` móvil y `:1227` fija el punto de entrada. Los dos caminos que escriben `aria-selected` —`seleccionar()` (`:1259`) y `resaltarPorIndice()` (`:1974`)— escriben sobre el `<li>` portador. (d) `refrescarPanelDeFachada` abre con `marcaDeFoco(host)` y cierra devolviéndolo (`:1737-1756`). (e) La ayuda de las dos casillas va por **`aria-describedby`** en elemento propio (`:634-640`), no en `title` |
| `AB2-09` | P1 | Un control de cambios declara una nota que no está en §2 | **Cerrado** | `Wireframes-Envio-De-Trabajo.md:93` lleva hoy la nota, con la misma redacción que sus dos gemelos. Y la traza de la discrepancia **no se borró**: la entrada original quedó anotada («*hasta entonces esta entrada la declaraba y §2 no la tenía —hallazgo `AB2-09`—*») y se sumó una entrada propia del cierre. Es el tratamiento correcto |
| `AB2-10` | P2 | Siete documentos retroalimentados con fecha de cabecera anterior | **Cerrado** | Leí el campo `**Fecha:**` de los siete: ninguno declara ya 2026-08-08. Cinco están en 2026-08-09 y `DX-Developer-Experience.md` avanzó a 2026-08-11 por fases posteriores |
| `AB2-11` | P2 | El índice de 03 afirma que el modelo UX-UI no está elegido | **Cerrado** | `03-UX-UI-DX/README.md:117` pasó a declarar el **resultado del paso 1**: catálogo de `Devs/Modelos-UX-UI/` vacío, única opción ofrecible la de por defecto, catálogo base de `References/Design/` aplicado, sin capitalizar modelo nuevo |
| `AB2-12` | P2 | Siete filas sin dimensión de umbral en §5 | **Cerrado** | **Recuento propio** sobre §5: extraje todos los `SD-XX` de la tabla de dimensiones y de la lista de filas sin gradación, expandiendo rangos. Cubren **62 de 62**, sin faltantes. Las siete que r1 nombró están mapeadas (`SD-41`, `SD-42`, `SD-47` en «Contrato de fachada»; `SD-46` en «Componentes»; `SD-54` en «Tokens visuales»; `SD-55` en «Superficies · versión angosta»; `SD-58` en «Vocabulario»). §5 declara además que **el umbral vinculante es el de la fila** |
| `AB2-13` | P2 | `<h3>` bajo `<h1>` sin `<h2>`; dos pares de color sin medir | **Cerrado**, los dos tramos | (a) `Panel-De-Cuentas.html:32-34` intercala el `<h2>` «Por dónde seguir» antes de los `<h3>` de las tarjetas, con el comentario que lo funda. (b) Los dos colores no medidos **desaparecieron** con `AB2-07`, y la barra quedó sobre tokens con contraste declarado. **Recalculé los tres ratios** contra `#04342C` con la fórmula de luminancia relativa de WCAG: `--color-background-warning` (`#FAEEDA`) **11.94**, `--color-brand-primary-tint` (`#E1F5EE`) **12.07**, `--color-text-on-brand` (`#FFFFFF`) **13.70**. Coinciden con lo declarado en el CSS `:26-31` hasta el segundo decimal |
| `AB2-14` | P3 | Referencia cruzada de hallazgo equivocada en el código | **Cerrado** | `Maqueta.js:569` dice hoy «hallazgo H-6 en README.md §6». `H-2` ya no se cita ahí |
| `AB2-15` | P3 | `T-4` declara su origen en el documento que no es su dueño | **Cerrado** | El objeto `T-4` de `Datos-Maqueta.js` declara `origen: 'Wireframes-Panel-De-Trabajos-Del-Alumno.md §2 + intake §20.E-3'`. La cadena vieja no aparece en el archivo |
| `AB2-16` | P3 | Control de cambios fuera de orden cronológico | **Cerrado** | Extraje las filas de control de cambios de `GeometriaFactory-Visor/03-UX-UI-DX/Glosario-UX.md`: la secuencia de fechas es monótona (2026-08-08, 2026-08-08, 2026-08-08, 2026-08-09, 2026-08-09). La corrección está declarada junto con la de `AB2-10` |
| `AB2-17` | P3 | Tres filas del contrato de datos citan una regla del framework en la columna de modelo conceptual | **Cerrado** | `DM-27`, `DM-28` y `DM-29` declaran hoy «**No es un atributo del modelo conceptual**» en esa columna —la fórmula de `DM-07` y `DM-16`— y la referencia a `Design-Rules-Identidad-De-Version.md` §2 se movió a la columna de nota. Ningún campo, tipo ni superficie cambió |
| `AB2-18` | P3 | El recuento de «cero persistencia» excluye por prefijo y no por espacio de nombres | **Declarado abierto** — no es hallazgo | El código **no cambió**: `Maqueta.js:1101` sigue haciendo `k.indexOf('mq-') !== 0`. Lo que cambió es que el punto está **declarado donde corresponde y con su destino**: el comentario que lo precede dice que la exclusión «es por PREFIJO y no por espacio de nombres», que en la maqueta es inocuo por no haber terceros, y que **donde `SD-47` mida lo mismo sobre el sistema construido la exclusión tiene que ser por espacio de nombres declarado**; y `SD-47` (matriz `:114`) lo incorporó literalmente a su afirmación. Un punto abierto correctamente declarado, con su vía, no es hallazgo |

**Recuento: 17 cerrados, 1 punto abierto correctamente declarado, 0 abiertos.**

---

## 3. Lo que cambió después de r1, y qué efecto tiene

### 3.1 La matriz pasó de 61 a 62 filas

Confirmado por conteo: **62 filas `SD-XX`**. La sexagésima segunda es `SD-62`, dada de alta el 2026-08-11 al cerrar la Fase G, y es una sonda **`VER-XX`**: se ancla en el único contrato de verificación de `../10-Examples/` (`VER-01` de `ejemplo-01-datos-seed.md` §9), toma **el comando del contrato como método sin desvío** y el campo `evidencia` del sample como evidencia esperada. Cierra el hueco que §1 y §4 declaraban desde 1.0 («ninguna fila `VER-XX`, porque este proyecto de código no tiene categoría 10 todavía»), que era una ausencia declarada y no un defecto.

Efecto sobre esta ronda: **ninguno adverso, y hay que decir por qué no lo tiene**. `SD-62` **no cubre ningún identificador de la línea de base** y la matriz lo declara explícitamente antes de la tabla de cobertura. Eso es lo que impide que el alta de una fila diluya el recuento de los 211: la cobertura sigue verificándose sobre las **61** filas de línea de base, y así la recontó esta ronda. §5 sumó su dimensión de umbral y los dos tramos sin gradación de `SD-62`, de modo que el mapeo sigue alcanzando a las 62.

### 3.2 La frontera del movimiento, y la sexta función nunca validada

La decisión del Product Owner —**el anfitrión consulta la preferencia de movimiento reducido y manda dos valores de verdad; el bundle no consulta nada**— está cumplida en el código y verificada en §2, `AB2-05`, y en §4.

Sobre la sexta función, `Linea-Base-Visual.md` **§6.1 existe** y su declaración es **verdadera**, comprobado contra el árbol y no contra sí misma:

- §6.1 afirma que «`establecerMovimiento` **no aparece en ningún archivo de la maqueta** y **nadie la miró en pantalla**». No hay **ninguna función, método ni botón** con ese nombre en `Visor-Tridimensional.js`, `Maqueta.js`, `Datos-Maqueta.js` ni en los doce HTML: el gobierno del movimiento se ejerce con `orbitar(v)` y `girarFiguras(v)`, métodos de instancia, y el panel de fachada expone **cinco** botones. Verificado por `grep` sobre la maqueta entera.
- La declaración **no vive sólo en el documento**: la maqueta la exhibe. El panel se titula «`GeometriaFactory-Visor` · **cinco de las seis funciones**» y lleva el texto «*establecerMovimiento se decidió después de la aprobación de la maqueta y no fue validada visualmente*» a la vista de quien la abra (`Maqueta.js:1587-1595`), con el comentario del bloque remitiendo a `Linea-Base-Visual.md` §6.1 y a `SD-43`. Un desfase declarado en el documento **y en el instrumento** es lo contrario de un desfase disimulado.
- §6.1 declara además **qué sí se validó** —las dos casillas de `CMP-72`, el arranque destildado con preferencia de movimiento reducido, la detención al arrastrar y con la pestaña oculta, la vuelta a la orientación de partida, y que ninguna combinación altera la disposición— y fija la vía de cierre: **iteración 5** y reemisión de la línea de base, «y no una edición silenciosa de esta tabla».

La única objeción que le encuentro a §6.1 es de redacción y va como `NB2-04`.

### 3.3 F-26 y RN-16: capacidades nuevas sin sonda

**F-26 (reseteo de contraseña por el administrador), entrada con el intake 1.7: el hueco está declarado, en los dos artefactos, y no está disimulado.**

- `Linea-Base-Visual.md` §6.1 le dedica una fila propia: enumera lo que arrastra —quinta operación de `SUP-09`, diálogo de confirmación, comunicación de la provisoria, tercer curso de `SUP-04` sin sesión otorgada—, declara que **nada de eso se validó** y, lo que importa, **se niega a asignarle `CMP-XX` ni `EST-XX`**: «un identificador de línea de base afirma que alguien lo miró y lo aprobó, y nadie lo miró». Los cuatro recuentos de §1 **no se inflan**: siguen siendo 11/73/74/24, y lo verifiqué contando los identificadores del documento.
- `Matriz-Sensado-Deriva.md` §4 lleva el párrafo «**Lo que esta matriz todavía no sensa, y por qué no se le inventa una sonda**», con el mismo razonamiento: una sonda anclada en un identificador inexistente «diría comparar contra una línea de base que no lo contiene». Declara que **`SD-04` conserva su alcance de dos cursos** y no se lo amplía por decreto, y fija la **iteración 5** como el momento en que nacen las sondas de F-26.

Ésa es exactamente la forma correcta de un hueco: nombrado, acotado, con su motivo y con su vía. **No es hallazgo.**

**RN-16 (habilitar una cuenta produce una contraseña provisoria), entrada con el intake 1.13: el hueco está incompletamente declarado.** Va como `NB2-03` en §6. El resumen es que RN-16 no agrega una operación nueva —agrega comportamiento nuevo y visible a **habilitar**, que es una de las **cuatro operaciones que la maqueta sí validó**—, y ni §6.1 ni el párrafo de §4 de la matriz lo alcanzan: los dos hablan de F-26 y de la ronda del intake 1.7.

---

## 4. La maqueta como código

Los seis criterios que r1 exigía, verificados por conteo sobre el árbol vivo:

| Criterio | Resultado | Cómo |
| --- | --- | --- |
| Cero atributos `style=` en línea | **Cumple** — 0 (eran 135) | `grep -o 'style="'` sobre los doce HTML y los tres `.js`; y `.style.` asignado desde JS también en 0 |
| Cero literales de color fuera de `:root` | **Cumple** — 0 en regla | Extracción del bloque `:root` y búsqueda de `#hex`, `rgb()`, `hsl()` en el resto: única ocurrencia, dentro de un comentario. Los 28 colores viven en `:root` |
| Un solo portador de rol y estado por nodo del árbol | **Cumple** | `role`, `aria-selected` y `aria-expanded` sólo en el `<li role="treeitem">`; interior degradado a `<span>`; `tabindex` móvil en `:1214`; los dos caminos de escritura de estado verificados |
| Canvas operable por teclado | **Cumple** | `tabindex="0"` en `:373`; `keydown` con flechas, `+`/`-` e `Inicio` |
| Rueda capturada sólo con foco | **Cumple** | `alRodar` retorna antes del `preventDefault()` si `document.activeElement !== lienzo` |
| Dato de dominio sólo desde `Datos-Maqueta.js` | **Cumple** | Composición de dominio en HTML: ninguna. Lo que queda en los HTML son identificadores de selección sobre el catálogo |

Añado, por ser lo que r1 dejó como condición de fondo: la **frontera del movimiento** quedó del lado correcto. El bundle no consulta el entorno más allá de la capacidad gráfica del elemento, arranca apagado ante opciones ausentes o parciales, y el anfitrión es quien lee `prefers-reduced-motion`, quien persiste la elección con claves `mq-` y quien manda los dos valores de verdad. Es lo que `Definicion-Contrato-De-Fachada.md` §3.3 y §4.1 fijan, lo que el Product Owner decidió y lo que `SD-47` sensa.

---

## 5. Coherencia con lo que el resto del corpus afirma sobre la Fase B2

Con el dictamen de §7 —**APROBADO**—, la afirmación del corpus de que la maqueta está aprobada **queda saneada**, y a partir de hoy es verdadera en los dos sentidos: aprobada por el Product Owner el 2026-08-09, y con su fase auditada y sin rechazo vigente. En particular:

- **`PRODUCT-MANIFEST` §5**, `requiere_maqueta`: «`Visor` ejecutó su Fase B2 y quedó aprobada» — queda saneada.
- **`Linea-Base-Visual.md` §1** y **`Bitacora-Validacion-Maqueta.md` §3**, la aprobación del Product Owner tras cuatro iteraciones — nunca estuvieron en discusión; r1 las verificó y esta ronda no las reabre.
- **`Handoff-Checkout.md`**, salvedad **`B-2`** y bloque 8 — **esta ronda la cierra**. Su redacción («no existe un `B2-…-r2.md` que levante el rechazo») era **correcta al momento de escribirse** y hoy queda superada; corresponde actualizarla, no corregirla, porque no afirmaba nada falso.
- **`Vista-Producto.md`**, fila B2 — **afirma algo falso** y esta ronda no lo sanea automáticamente. Va como `NB2-01`.

---

## 6. Hallazgos nuevos

### P1 — alto

**`NB2-01` · `Vista-Producto.md` declara el rechazo de B2 levantado por un informe que no lo levanta**
*Archivo:* `SDD/Docs/Producto/Vista-Producto.md`
*Sección:* §3, tabla de fases, fila `B2`
*Evidencia:* la fila remite a «[`B2-Maqueta-GeometriaFactory-Web-r1.md`], **cuyo rechazo levanta** [`F26-Propagacion-r2.md`]» y dictamina «Rechazado en ronda 1; **rechazo levantado**». `F26-Propagacion-r2.md` §7 dice textualmente: «**APROBADO. Se levanta el rechazo de `F26-Propagacion-r1.md`**», y su cabecera declara como motivo de la ronda «dictaminar si se levanta el rechazo de `F26-Propagacion-r1.md` 1.0» sobre un alcance que son **tres commits de propagación de F-26**, no la Fase B2. Ese informe sí verificó el cierre de varios `AB2-XX` de paso, y lo hizo bien —`AB2-04` lo cerró por su instrumento—, pero **un informe no puede levantar el rechazo de una fase que no audita**, y no dice haberlo hecho.
*Por qué es P1 y no P0:* `Deriva-Rules.md` §1 tipifica como P0 la afirmación respaldada con evidencia que no resuelve, y ésta lo es. No lo elevo a P0 por dos razones que conviene declarar: la afirmación está en un documento de **consolidación** y no en el instrumento de la fase, y **su contenido pasa a ser verdadero con la emisión de este informe** —lo que queda mal es la cita, no la conclusión—. Agrava, en cambio, que el corpus se contradiga a sí mismo: `Handoff-Checkout.md` `B-2` declara lo opuesto y con razón.
*Recomendación:* que la fila cite **este informe** como el que cierra la fase, y que el dictamen diga «Rechazado en ronda 1; **aprobado en ronda 2**». Corregir en el mismo movimiento la salvedad `B-2` y el bloque 8 de `Handoff-Checkout.md`, que quedan cerrados.

### P2 — medio

**`NB2-02` · La declaración de hueco de la matriz remite a una versión de wireframe que envejeció, y es la remisión que gobierna la construcción de F-26**
*Archivo:* `SDD/Docs/Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`
*Sección:* §4, párrafo «Lo que esta matriz todavía no sensa»
*Evidencia:* cierra con «lo que gobierna la construcción de esa capacidad son `CU-04` §5 FA-06 y FA-07, `CU-03` §5 FA-04 a FA-06 y **sus wireframes 1.1**». Hoy `Wireframes-Panel-De-Cuentas.md` está en **1.6** y `Wireframes-Credencial-Propia.md` en **1.4**. Es **el mismo defecto** que `F26-Propagacion-r1.md` levantó como `F26-13` sobre `Linea-Base-Visual.md` §6.1 y que allá se corrigió pasando a nombrar «**la versión vigente**» en lugar de un número que envejece —con el fundamento explícito de que «quien siguiera la remisión literal habría implementado el campo de contraseña que la 1.2 eliminó»—. La corrección se aplicó al documento hermano y **no a éste**.
*Consecuencia operativa:* es la única remisión constructiva que la matriz da para F-26 mientras no haya iteración 5, y apunta a dos documentos que ya cambiaron cinco y tres versiones respectivamente, incluido el retiro del campo de contraseña del panel.
*Recomendación:* reemplazar «sus wireframes 1.1» por «sus wireframes **en su versión vigente**», idéntico a como quedó `Linea-Base-Visual.md` §6.1.

**`NB2-03` · El hueco de RN-16 no está declarado: cambia el comportamiento de una operación que sí fue validada, y ni la línea de base ni la matriz lo registran**
*Archivo:* `SDD/Docs/Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Linea-Base-Visual.md` y `.../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`
*Sección:* §6.1 de la primera; §4, párrafo de lo que no se sensa, y `SD-09` de la segunda
*Evidencia:* `PRODUCT-INTAKE` 1.13 §4.1 declara **RN-16**: «**Habilitar una cuenta produce una contraseña provisoria** … el sistema la produce, **la pantalla se la muestra al administrador** para que se la comunique, y la cuenta queda con cambio de contraseña pendiente». `Wireframes-Panel-De-Cuentas.md` 1.5 lo absorbió y reescribió la fila de comportamiento de **habilitar**: «la situación cambia y **la superficie muestra la contraseña provisoria para comunicarla, una sola vez**». Eso no le toca a F-26: **habilitar es una de las cuatro operaciones que la maqueta validó el 2026-08-09**, y la maqueta no muestra ninguna provisoria en ninguna parte (`grep -r provisoria` sobre `SDD/Maquetas/GeometriaFactory-Web/` devuelve **cero**). Sin embargo: (a) §6.1 tiene fila para `establecerMovimiento` y fila para F-26, y **ninguna para RN-16**; `RN-16` aparece en el documento **una sola vez, en el control de cambios 1.4**, que registra que se reescribió la descripción de `SUP-04` y afirma que «las superficies, sus archivos de maqueta y su recuento no cambian» —cierto— pero **no declara que el comportamiento nuevo no fue validado**; (b) el párrafo de §4 de la matriz nombra sólo **F-26** y la ronda del intake **1.7**, y no alcanza a una regla que entró en 1.13; (c) `SD-09` sigue afirmando el panel de cuentas «con … la acción de situación que ofrece sólo la transición admitida», sin la comunicación de la provisoria, de modo que **la sonda sensaría conforme un panel que no cumple RN-16**.
*Por qué es P2 y no P1:* el hueco **no está disimulado** —la reescritura está registrada en el control de cambios y la regla vive con todo detalle en su wireframe, en `CU-04` y en `US-09`—, y el mecanismo que RN-16 unifica es el mismo que §6.1 ya declara sin validar en la fila de F-26 («la comunicación de la contraseña provisoria»). Lo que falta es el registro explícito de que **una operación aprobada cambió de comportamiento después de aprobada**, que es justamente lo que §6.1 existe para decir.
*Recomendación:* sumar a §6.1 una tercera fila —RN-16, intake 1.13, qué se validó el 2026-08-09 (habilitar sin provisoria), qué quedó fuera (la exhibición de la provisoria y el pendiente de cambio)— y extender el párrafo de §4 de la matriz a esa segunda ronda, con la misma iteración 5 como vía. Alternativamente, acotar la afirmación de `SD-09` para que no cubra el comportamiento nuevo. No corresponde asignarle `CMP-XX` ni `EST-XX`, por el mismo motivo que la línea de base ya declara para F-26.

### P3 — bajo

**`NB2-04` · Una afirmación de §6.1 es exacta en su intención e inexacta en su letra.** `Linea-Base-Visual.md` §6.1 dice que `establecerMovimiento` «**no aparece en ningún archivo de la maqueta**». Lo que no aparece es **la función**: el nombre sí aparece, tres veces y a propósito, para declarar su ausencia —`README.md:319` y `:330`, el comentario de bloque de `Maqueta.js:1569` y el texto que el panel muestra en pantalla en `:1593`—. Es exactamente el tipo de enunciado absoluto que el corpus verifica por `grep`, y por `grep` no resuelve. Corregir a «no está implementada en ningún archivo de la maqueta: la maqueta la nombra sólo para declarar que no la ejerce».

### Polisemias y puntos abiertos evaluados y descartados

- El **par «recorrido»** de `Glosario-UX.md` §3.1, con sus dos referentes calificados obligatoriamente. Contextos disjuntos y resolución declarada: **no es hallazgo**.
- El **par «árbol»** —el del JSON crudo y el de las piezas— fusionado en un solo control por decisión de la maqueta y declarado en el comentario de `Maqueta.js`: **no es hallazgo**.
- **`AB2-18`**, exclusión por prefijo: punto abierto declarado con su motivo, su alcance de inocuidad y su destino en `SD-47`. **No es hallazgo**, por el criterio negativo de esta ronda.
- La **ausencia de sondas para F-26**: hueco declarado en dos artefactos con su vía. **No es hallazgo**.
- La **matriz sin fila `VER-XX` entre 1.0 y 1.2**: ausencia declarada en su momento y cerrada en 1.3. **No es hallazgo**.

---

## 7. Dictamen

**APROBADO. Se levanta el rechazo de `B2-Maqueta-GeometriaFactory-Web-r1.md` 1.0.**

El fundamento es directo. El rechazo de r1 tuvo **un solo motivo declarado**, el P0 `AB2-01`, y ese motivo **ya no existe**: no porque un control de cambios lo diga, sino porque **conté la cobertura de la matriz por extracción propia** y da 211 de 211, con los cuatro `CMP` que faltaban cubiertos por una sonda que los sensa de verdad, con método, evidencia esperada y umbral propios, y con la propia matriz declarando por qué **no** se resolvió anotándolos en una fila existente. La afirmación que r1 tipificó como «evidencia que no resuelve» hoy resuelve, y resuelve para quien la verifique como la verifiqué yo.

De los otros diecisiete hallazgos, **dieciséis están cerrados sobre el instrumento** y el restante es un punto abierto correctamente declarado. Los tres incumplimientos técnicos que r1 consideraba de fondo —dato de dominio en un HTML, literales visuales ad hoc, accesibilidad de la escena— están cerrados por conteo y no por prosa: 0 `style=`, 0 colores fuera de `:root`, lienzo con foco y teclado, rueda sólo con foco, árbol con un solo portador de rol y estado. Y la contradicción con el contrato en la frontera del movimiento, que era el hallazgo con más consecuencia sobre lo que se va a construir, se resolvió **aplicando el contrato y no aflojándolo**: el bundle no consulta nada y el anfitrión manda dos valores de verdad, que es lo que después el Product Owner confirmó en el intake 1.7 y lo que `SD-47` sensa hoy.

Conviene decir también qué **no** hizo esta ronda condicionar el dictamen. Los cuatro hallazgos nuevos no son P0 y ninguno afecta la capacidad de la línea de base ni de la matriz de servir como punto de comparación: `NB2-01` es una cita mal puesta en un documento de consolidación cuyo contenido esta misma emisión vuelve verdadero; `NB2-02` es una remisión que envejeció y se corrige con tres palabras; `NB2-03` es un hueco real pero **incompletamente declarado, no disimulado**, sobre una capacidad cuya especificación vive completa aguas abajo; `NB2-04` es una letra que no acompaña a su intención. Los cuatro son de corrección puntual.

**Condiciones de corrección, no de bloqueo** —la fase queda promovida y estas correcciones se absorben sin reabrirla:

1. `NB2-01`: que `Vista-Producto.md` cite este informe como el que cierra B2, y que se actualice la salvedad `B-2` de `Handoff-Checkout.md`, que **queda cerrada por esta emisión**.
2. `NB2-03`: fila de RN-16 en `Linea-Base-Visual.md` §6.1 y extensión del párrafo de §4 de la matriz a la segunda ronda de capacidades nuevas.
3. `NB2-02` y `NB2-04`: las dos correcciones de redacción.

Y una constancia que excede a esta fase, porque es del framework y no del producto: **el hueco de proceso que motivó este informe fue no emitir la ronda 2, no lo que la ronda 2 encontró**. Durante dos días, siete fases se generaron y se auditaron sobre una línea de base cuya fase estaba formalmente rechazada, mientras el manifiesto la declaraba aprobada. Que el resultado haya sido favorable no valida el procedimiento: **el corpus no supo, en todo ese intervalo, que el instrumento contra el que iba a medir la deriva de todo el front estaba respaldado por un veredicto negativo**. Lo detectó el check-out y no la cadena de fases. Corresponde que el orquestador trate la emisión de la ronda de cierre como bloqueante de la fase siguiente, y no como un trámite posterior.

---

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial, **tardía y declarada como tal en §0**. Ronda 2 sobre el estado vivo de la rama `sdd/cierre-de-huecos`, dos días después de la ronda 1 y siete fases más tarde. Verifica los **dieciocho** hallazgos de `B2-Maqueta-GeometriaFactory-Web-r1.md` uno por uno contra el árbol y no contra lo declarado —17 cerrados, 1 punto abierto correctamente declarado—; **recuenta por extracción propia** la cobertura de los 211 identificadores sobre las 62 filas de la matriz y el mapeo de umbrales de §5; **recalcula** los tres ratios de contraste de la barra de validación; verifica en el código la frontera bundle/anfitrión del movimiento, los seis criterios técnicos de la maqueta y la veracidad de la declaración de desfase de `establecerMovimiento`; absorbe lo que cambió después de r1 —la fila `VER-XX`, la decisión del Product Owner sobre el movimiento, F-26 y RN-16— y contrasta la fase contra lo que el manifiesto, `Vista-Producto.md` y `Handoff-Checkout.md` afirman de ella. Cuatro hallazgos nuevos: 1 P1, 2 P2, 1 P3; ninguno P0. **Dictamen: APROBADO.** Cierra la salvedad `B-2` de `Handoff-Checkout.md`. | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
