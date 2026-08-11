# Glosario UX — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Glosario-UX.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Glosario-Funcional.md` completo —§2 (diecinueve términos acuñados), §3 (los tres términos con más de un referente) y §4 (términos referenciados)—; `../../../00-Contexto/Vision-Producto.md` §9.1, §9.2 y §9.3 (glosario raíz de la cadena); `Vocabulario-Rules.md` §2, §4 y §9; `Rules-UX-UI-DX.md` §3.3; `Design-Rules-Web-Generico.md` §4 y §5; `Design-Rules-Primer-Arranque.md` §4; `Design-Rules-Identidad-De-Version.md` §4
**Trazabilidad downstream:** Fase B2 de validación visual de maqueta, cuyo inventario identificado usa estos nombres; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`; `11-Documentacion`

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Panel](#31-panel)
  - [3.2 Sección](#32-sección)
- [4. Palabras de superficie que esta categoría deliberadamente no usa](#4-palabras-de-superficie-que-esta-categoría-deliberadamente-no-usa)
- [5. Términos referenciados y no redefinidos](#5-términos-referenciados-y-no-redefinidos)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Alcance de este glosario

Declara únicamente el vocabulario **de superficie** que esta categoría acuña, y referencia lo que ya está declarado aguas arriba: en `Vision-Producto.md` §9, que es el glosario raíz de la cadena, y en `Glosario-Funcional.md` de la categoría 02 de este mismo proyecto de código. **Ningún término de §5 se redefine acá.**

La regla de inclusión aplicada es la de `Rules-UX-UI-DX.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo wireframe se define ahí y no entra.

Esta sección tiene un riesgo de vocabulario propio y concentrado: **pantalla, vista, panel, superficie y sección** son palabras que una categoría de diseño usa todo el tiempo, y tres de ellas ya tienen dueño aguas arriba. §2 declara las que se acuñan, §3 las que colisionan dentro de esta sección y §4 las que deliberadamente no se usan. Que §4 exista es la parte que importa: una palabra prohibida sin registro reaparece en el primer documento que alguien escriba después.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y notas |
| --- | --- | --- | --- |
| **Superficie** | La unidad de diseño y de maquetado de esta categoría: una ruta con su conjunto propio de estados, un diálogo con flujo propio, o un bloque alojado dentro de otra superficie con su mapa de estados propio. Es lo que un archivo `wireframes-<superficie>` documenta y lo que la maqueta de la Fase B2 materializa como una unidad navegable | Todos | Es el término canónico. **No se usa «pantalla» como sinónimo**; ver §4 |
| **Nombre canónico de superficie** | El identificador estable de una superficie, en Título-Con-Guiones, declarado en la sección 1 de su wireframe y reusado sin cambios por la maqueta y por la línea de base visual | `Experiencia-De-Uso.md` §3.1, los once wireframes | Cambiarlo rompe la trazabilidad con la maqueta |
| **Superficie alojada** | La superficie que **no tiene ruta**: se dibuja dentro de otra superficie y sólo se llega a ella llegando a su anfitriona. Tiene nombre canónico, mapa de estados propio y lista de interacciones propia —por eso se documenta en un wireframe separado—, pero **no es un destino de navegación y no se construye como página aparte**. Este producto tiene exactamente una: `Resolucion-Del-Trabajo`, alojada en `Vista-De-Trabajo` | `Wireframes-Resolucion-Del-Trabajo.md` §1, `Wireframes-Vista-De-Trabajo.md` §1 y §3, `Experiencia-De-Uso.md` §3.1 | El calificador «alojada» es parte del término. Sin él, un wireframe separado se lee como pantalla propia, que es exactamente el malentendido que la validación visual de la Fase B2 expuso. Una exhibición aislada de una superficie alojada es **instrumento de validación**, no ruta del producto |
| **Movimiento automático de la escena** | El movimiento que la escena tridimensional hace **sin que la persona la arrastre**, gobernado por dos controles independientes: la **órbita de la cámara**, que gira el punto de vista alrededor del conjunto dejando las piezas quietas, y el **giro de las figuras**, que rota cada pieza sobre su eje vertical en su lugar. Son **preferencias de quien mira**, no instrumento de validación, y por eso viven junto al dibujo | `Wireframes-Vista-De-Trabajo.md` §3 y §7 | Realiza la capacidad F-25, `Must Have` desde el `PRODUCT-INTAKE` 1.7. Los dos se detienen mientras la persona arrastra y con la pestaña oculta, y arrancan apagados si el sistema declara preferencia de movimiento reducido: **esa preferencia la consulta el componente anfitrión**, que le manda al visor **dos valores de verdad**, uno por movimiento. **El visor no consulta nada.** **Ninguno altera la disposición de las piezas**, que sale del índice: el determinismo comprometido es de la posición, no de la orientación. «Órbita de la cámara» no redefine la «cámara orbital» del `Glosario-Funcional.md` del proyecto de código del visor: aquélla es el mecanismo, ésta es el movimiento que se prende y se apaga |
| **Contraseña provisoria** | La credencial que **el sistema produce** al resetear la contraseña de un alumno, y que el administrador le comunica **fuera del producto**, porque no hay canal de correo. **El panel no tiene dónde escribirla** (RN-14). La superficie la muestra **una sola vez** y no la vuelve a mostrar | `Wireframes-Panel-De-Cuentas.md` §3, `Wireframes-Credencial-Propia.md` §3 | Realiza la capacidad F-26. **No se dice «contraseña temporal»**: no vence por tiempo, sólo por uso |
| **Cambio forzado** | El curso de `Credencial-Propia` al que llega, obligada, la persona a la que le resetearon la contraseña. Mismo formulario que el cambio voluntario, sobre el **shell de acceso** y **sin sesión**: la provisoria se reconoce y encamina, y no otorga sesión de trabajo (RN-13) | `Wireframes-Credencial-Propia.md` §1 y §5, `Experiencia-De-Uso.md` §3.2 | No se dice «cambio obligatorio»: lo forzado es la situación, no la política de contraseñas |
| **Shell** | El armazón dentro del que se dibuja una superficie. Este producto tiene exactamente dos: el **shell de acceso**, sin navegación, y el **shell de trabajo**, con barra lateral. La frontera entre ellos es tener sesión y sistema operable | `Experiencia-De-Uso.md` §3.2, todos los wireframes | El catálogo lo llama «shell partido» cuando nombra la partición. **No se traduce**: es el término del catálogo |
| **Bloque** | Una agrupación visual con límite propio dentro de una superficie: el bloque de decisión, el bloque de comentario, el bloque de la escena | La mayoría de los wireframes | **Es lo que en otras categorías se llamaría «panel»**, y acá no se llama así; ver §3.1 |
| **Insignia de estado** | La representación compacta del estado de un trabajo o de la situación de una cuenta, con forma de píldora y **con su texto siempre presente**. El color es refuerzo y nunca el único canal | `Representacion-Fila-De-Trabajo.md`, cinco wireframes | Realiza el patrón de insignia del catálogo. No se usa «etiqueta» para esto |
| **Fila de trabajo** | La representación de un trabajo dentro de una colección, con su estado y con las acciones que ese estado admite para quien mira | `Representacion-Fila-De-Trabajo.md`, tres wireframes | En la versión angosta reflúye a tarjeta apilada y conserva el nombre |
| **Banda de resultado** | La franja de ancho completo dentro de una tarjeta o de una superficie que comunica el resultado de un intento. Dos variantes: **error**, anunciada como alerta, y **confirmación**, anunciada como estado | Cinco wireframes | Patrón del catálogo de primer arranque. Su texto se resuelve desde un código de resultado y **no se compone a mano en la vista** |
| **Aviso de indisponibilidad** | La materialización visible del estado degradado: el bloque que ocupa el área de contenido cuando la pieza de datos no responde, con el armazón intacto y con reintento | `Wireframes-Estado-Degradado-Y-Reconexion.md` y las diez superficies que lo referencian | «Estado degradado» es el término funcional, acuñado aguas arriba; **éste nombra su forma en la superficie**. No son sinónimos intercambiables: uno es la situación, el otro es lo que se dibuja |
| **Estado vacío explicado** | La representación de una colección con cero elementos cuando el servicio está disponible: ilustración neutra, texto orientativo y acción siguiente. **Se distingue del aviso de indisponibilidad por el tipo recibido y no por el conteo** | Tres wireframes, `Experiencia-De-Uso.md` §4.1 | El calificador «explicado» es parte del término: un vacío sin explicación es un hueco, no un estado |
| **Estado de superficie** | Cada situación que una superficie puede presentar y que su wireframe enumera en su sección 5. **Un estado no declarado no se maqueta y por lo tanto no se valida** | `Experiencia-De-Uso.md` §4.2, los once wireframes | No se confunde con el estado del trabajo ni con la situación de una cuenta, que son de dominio |
| **Requisito declarado** | El texto de apoyo que enuncia, en positivo y **antes de que la persona escriba**, la regla que un campo tiene que cumplir. Se deriva de la política del sistema y **no se transcribe como literal en la vista** | Tres wireframes | Patrón del catálogo de primer arranque. Se opone al mensaje que aparece recién al fallar |
| **Confirmación escrita** | La forma de superficie de la exigencia de transcribir un valor conocido antes de una operación destructiva: campo, valor a transcribir a la vista, y acción destructiva inhabilitada hasta que coinciden | `Wireframes-Panel-De-Cuentas.md`, `Experiencia-De-Uso.md` §2.1 | El término lo acuña la categoría 02; acá se registra **su forma**, no se redefine |
| **Sello de versión** | La declaración de qué versión de sí misma corre la instancia, al pie de la superficie que lo aloja, en sus **dos ubicaciones obligatorias** | `Representacion-Sello-De-Version.md`, los once wireframes | Patrón del catálogo de identidad de versión |
| **Detalle de diagnóstico** | El despliegue que expone el contrato de identidad de versión completo, con copiado en un solo gesto. **Expone la identidad del artefacto, nunca la topología** | `Representacion-Sello-De-Version.md`, dos wireframes | Es la única vía de reporte del producto: no hay canal de soporte |
| **Orientación posterior** | La grilla de tarjetas de acceso que sugiere los pasos siguientes tras el aprovisionamiento. **Orienta, no bloquea**: no es un asistente ni una lista de tareas con progreso | `Wireframes-Panel-De-Cuentas.md`, `Experiencia-De-Uso.md` §2.3 y §3.3 | Patrón del catálogo de primer arranque. **Se aloja en el destino al completar y no en la superficie de arranque**, que es por lo que no aparece en `Wireframes-Aprovisionamiento-Inicial.md` |
| **Punto de quiebre angosto** | El ancho por debajo del cual las superficies reflúyen a una columna y las filas pasan a tarjetas apiladas. Fijado en 768 px [ASUNCIÓN, tomada del documento base] | Los once wireframes | No se usa el anglicismo |
| **Versión angosta** | La descripción del reflujo de una superficie por debajo del punto de quiebre, en la sección 6 de cada wireframe | Los once wireframes | La regla la llama «versión móvil o responsive»; acá se usa «angosta» porque el criterio es el ancho disponible y no la clase de dispositivo |

## 3. Términos con más de un referente

Se declaran únicamente los términos cuyos sentidos **colisionan en el mismo contexto de lectura**, según el criterio de `Vocabulario-Rules.md` §9.2, donde el contexto de lectura de un subagente es la sección. No se reporta ningún otro caso: los términos cuyos sentidos se distinguen solos quedan fuera, por la prohibición de §9.4 sobre calificar ocurrencias de contextos disjuntos.

**«Vista» no figura acá y es deliberado.** Su polisemia ya está resuelta aguas arriba, en `Glosario-Funcional.md` §3.1, con tres referentes y una forma calificada obligatoria. Esta sección **respeta esa resolución en lugar de crear una propia**: escribe «vista de trabajo» siempre calificada para el primer referente, y usa «superficie», «ruta» o «componente» para los otros dos, que es exactamente la sustitución que la categoría 02 fijó. Reabrirla acá habría producido dos resoluciones distintas del mismo término dentro del mismo proyecto de código.

### 3.1 Panel

Dos referentes, y colisionan de lleno en esta categoría.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| El conjunto de rutas que la pieza pública arma para una persona según su papel: el panel del alumno y el panel del administrador | **«panel»**, en su forma desnuda. Es el término que acuña `Glosario-Funcional.md` §2 y **no se redefine** | `Experiencia-De-Uso.md` §3.2, los wireframes que nombran destinos de navegación |
| Una agrupación visual con límite propio dentro de una superficie | **No se usa «panel» para esto.** Se escribe **«bloque»**, o «tarjeta» cuando la forma es la del patrón de tarjeta del catálogo | Todos los wireframes |

**Evidencia de que los contextos colisionan.** En la sección 3 de varios wireframes conviven el destino de navegación —«el panel del administrador»— y los contenedores visuales de la superficie. Un lector que reciba esa sección suelta y lea «el panel de decisión» no puede decidir si le hablan de un conjunto de rutas o de un recuadro, y las dos lecturas producen decisiones distintas aguas abajo: una es de navegación y la otra de disposición.

**Excepción declarada, y es una sola.** «Panel de resumen» y «panel de cuentas» conservan la palabra porque son nombres propios ya fijados aguas arriba —el segundo es además un nombre canónico de superficie— y renombrarlos habría roto la correspondencia con la categoría 02. Se leen como nombres y no como el término.

### 3.2 Sección

Dos referentes que conviven en cada wireframe, porque los documentos de esta categoría hablan de sí mismos.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| Cada división numerada de un documento de esta cadena | **«sección»**, con su número: «la sección 5 del wireframe», «§4.2 de la regla» | Todos los artefactos |
| Una franja de contenido dentro de una superficie | **No se usa «sección» para esto.** Se escribe **«bloque»**, o «franja» cuando el elemento ocupa el ancho completo | Todos los wireframes |

**Evidencia de que los contextos colisionan.** La sección 5 de cada wireframe enumera estados de bloques de la superficie: la misma oración puede nombrar la división del documento y la franja de la pantalla. La resolución es reservar «sección» para el documento, que es el uso que la regla constructiva impone y que no se puede evitar.

## 4. Palabras de superficie que esta categoría deliberadamente no usa

No son entradas de glosario: son prohibiciones registradas. Existen porque una palabra descartada sin registro reaparece en el primer documento que alguien escriba después de éste.

**Excepción general y única: los rótulos de sección que impone la regla constructiva quedan fuera del alcance de estas prohibiciones.** `Rules-UX-UI-DX.md` §4.2.1 rotula «Pantalla y propósito» la sección 1 de cada wireframe y «Layout» la sección 2, y esos dos rótulos se conservan **por ser los de la regla**: cambiarlos rompería la correspondencia entre la estructura obligatoria y la emitida. La prohibición rige en el **cuerpo** de los documentos, donde se escribe «superficie» y «disposición». La excepción se declara acá, una sola vez y para las dos palabras, en lugar de repetirse en cada fila.

| Palabra | Por qué no se usa | Qué se escribe en su lugar |
| --- | --- | --- |
| **Pantalla** | «Superficie» es el término canónico de esta categoría y es el que la maqueta y la línea de base visual van a nombrar. Además, `Glosario-Funcional.md` §3.1 ya declaró que la porción de página que un componente arma **no se nombra «vista»** y remitió a «página» o «componente»; agregar «pantalla» como tercer sinónimo habría multiplicado las formas en lugar de fijarlas | **«superficie»** en el cuerpo. El rótulo «Pantalla y propósito» de la sección 1 de cada wireframe queda cubierto por la excepción general de arriba |
| **Vista**, sin calificar | Resuelto aguas arriba con forma calificada obligatoria | **«vista de trabajo»** para el primer referente; «superficie», «ruta» o «componente» para los otros dos |
| **Rol** | `Glosario-Funcional.md` §2 declara **«papel»** y anota expresamente que no se usa «rol» | **«papel»** |
| **Estado**, para una cuenta | Colisionaría con el estado del trabajo. La categoría 02 ya eligió el otro término | **«situación de cuenta»** |
| **Modal**, **toast**, **tooltip**, **wizard**, **breakpoint**, **layout** | Anglicismos con equivalente en el catálogo o en el español técnico. El producto se redacta en español rioplatense | **«diálogo»**, «confirmación efímera», «ayuda contextual», «asistente», «punto de quiebre», «disposición» |
| **`Pendiente`**, sin calificar | Nombra dos estados distintos y la regla es vinculante para toda la documentación generada | **«cuenta `Pendiente`»** o **«trabajo en estado `Pendiente`»**. No se califican las enumeraciones del conjunto cerrado ni los identificadores literales |
| **Pieza**, sin calificar, para un servicio desplegable | Resuelto aguas arriba | **«pieza pública»**, **«pieza de datos»**, «piezas desplegables». La forma desnuda queda para la figura del trabajo |
| **Proyecto**, a secas | Choque de vocabulario resuelto aguas arriba | **«proyecto de código»**, o el nombre propio de lo que se nombre |
| **Calificación**, **nota**, **puntaje** | Sigue excluida del producto. El comentario del administrador **no es una calificación** | **«comentario»**, y **«desenlace»** para la decisión |
| **Error**, para una advertencia | Una advertencia no impide nada y no es un error. Confundirlos le diría al alumno que corrija algo que está bien | **«advertencia»**, o **«observación»** cuando el enunciado abarca las dos especies |

## 5. Términos referenciados y no redefinidos

Ya están declarados aguas arriba y **no se redefinen acá**. Se listan porque aparecen en más de un artefacto de esta categoría y quien entre por un wireframe suelto necesita saber dónde está su definición canónica.

| Término | Dónde está declarado | Nota de uso en esta categoría |
| --- | --- | --- |
| Vista de trabajo | `Glosario-Funcional.md` §2 y §3.1 | La superficie de cuatro partes. **Siempre calificada.** Su nombre canónico de superficie es `Vista-De-Trabajo` |
| Panel | `Glosario-Funcional.md` §2 | Conjunto de rutas por papel. Ver §3.1 |
| Estado degradado | `Glosario-Funcional.md` §2 | La situación. Su forma en la superficie es el **aviso de indisponibilidad**, que sí acuña esta categoría |
| Cartel de reconexión | `Glosario-Funcional.md` §2 | Propio del circuito. **Distinto del estado degradado**, y el diseño no los mezcla |
| Circuito · Estado del circuito · Marca de sesión | `Glosario-Funcional.md` §2 | Ninguno se dibuja: son la razón por la que la credencial de sesión no llega al navegador |
| Ruta protegida | `Glosario-Funcional.md` §2 | Acota lo que se ofrece. **Ocultar una acción no hace cumplir ninguna regla** |
| Elemento de dibujo · Componente anfitrión del visualizador · Árbol de la estructura · Previsualización | `Glosario-Funcional.md` §2 | Los cuatro se dibujan en `Vista-De-Trabajo` y los dos últimos también en `Envio-De-Trabajo` |
| Acción única de guardado · Retiro de un trabajo · Desenlace · Situación de cuenta · Papel | `Glosario-Funcional.md` §2 | Vocabulario de las acciones que las superficies ofrecen |
| Confirmación escrita | `Glosario-Funcional.md` §2 | Acá se registra su forma de superficie, no su semántica |
| Trabajo · Pieza · Observación · Advertencia · Error de validación · Comentario · Estado del trabajo · Enviar · Aprobar / Rechazar · Valor declarado / valor derivado · Fallo silencioso · Laboratorio · Actividad 1 · Componente | `Vision-Producto.md` §9.1 | Glosario raíz. **«Componente» de §9.1 es la figura plana de una pieza**; el componente de interfaz se nombra siempre con su función |
| `Pendiente`, forma calificada obligatoria · Pieza en su segundo referente | `Vision-Producto.md` §9.2 | Reglas vinculantes para toda la documentación generada |

Del catálogo de diseño se referencian, con su nombre del catálogo y sin redefinirse: **tarjeta de acceso**, **tarjeta de aprovisionamiento**, **shell partido**, **grilla de listado**, **formulario de edición**, **botones primario, secundario y destructivo**, **búsqueda y filtros**, **esqueleto**, **ilustración vectorial**, **redirección con estado de resolución**, **distintivo de artefacto preliminar** y **marcador de origen indeterminado**.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Declara diecisiete términos de superficie que esta categoría acuña, dos términos con más de un referente dentro de esta sección —«panel» y «sección»— con su evidencia de colisión y su forma resuelta, diez palabras de superficie deliberadamente no usadas con su alternativa, y los términos referenciados de `Glosario-Funcional.md` de la categoría 02, del glosario raíz y del catálogo de diseño. Declara explícitamente que «vista» **no** se reabre acá: su polisemia está resuelta aguas arriba con forma calificada obligatoria y esta sección respeta esa resolución. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-05**: la fila «Orientación posterior» de §2 corrige su referencia cruzada —el término no aparece en `Wireframes-Aprovisionamiento-Inicial.md`, porque el patrón se aloja en el destino al completar y no en la superficie de arranque— y pasa a citar `Experiencia-De-Uso.md` §2.3 y §3.3, con el motivo declarado. **H-09**: §4 suma una **excepción general y única** que declara fuera del alcance de las prohibiciones a los rótulos de sección que impone la regla constructiva —«Pantalla y propósito» y «Layout» de `Rules-UX-UI-DX.md` §4.2.1—, y la fila «Pantalla» remite a ella en lugar de declarar la excepción sólo para sí misma. La prohibición sigue rigiendo en el cuerpo de los documentos. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **Refuerzo del enunciado de alojamiento**: §2 acuña **«superficie alojada»** —la superficie sin ruta, que se dibuja dentro de otra y no es destino de navegación—, porque un wireframe separado sin ese calificador se lee como pantalla propia, que es el malentendido que la validación visual expuso; y la definición de **«superficie»** suma el tercer caso, el bloque alojado con mapa de estados propio, que hasta ahora quedaba fuera de su enunciado. **F-25**: §2 acuña **«movimiento automático de la escena»** con sus dos controles, la órbita de la cámara y el giro de las figuras, con la verificación de colisión declarada: no redefine la «cámara orbital» del glosario funcional del proyecto de código del visor, que es el mecanismo, mientras que este término nombra el movimiento que se prende y se apaga. El catálogo pasa de diecisiete a diecinueve términos acuñados. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**. §2 acuña dos términos de la capacidad **F-26**: **«contraseña provisoria»**, con el alias prohibido «contraseña temporal» y su motivo, y **«cambio forzado»**, con la precisión de que es el único uso del shell de acceso **con sesión iniciada**. El catálogo pasa de diecinueve a **veintiún** términos acuñados. La entrada de **«movimiento automático de la escena»** precisa la frontera que 1.7 fijó para **F-25**: la preferencia de movimiento reducido **la consulta el componente anfitrión**, que manda dos valores de verdad, y **el visor no consulta nada**. Sube minor: agrega términos y precisa uno, sin redefinir ninguno. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8.** La entrada **«cambio forzado»** declaraba que el curso se dibujaba sobre el shell de acceso **«con sesión iniciada»**, y lo daba como el único uso del shell de acceso con sesión. El intake 1.8 §4.1 precisa RN-13: la cuenta con provisoria **se autentica y no obtiene sesión de trabajo**. La entrada se corrige y pierde esa excepción: el shell de acceso vuelve a coincidir sin residuos con la frontera que declara la entrada **«shell»** —tener sesión y sistema operable—, que por eso no cambia. |
| 1.3 | 2026-08-09 | **Cierra el hallazgo `F26-19`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. La entrada **«contraseña provisoria»** de §2 decía que es «la credencial que **el administrador le fija** a un alumno al resetearla», que es la formulación anterior a la decisión del Product Owner sobre quién la produce: contradecía a `../02-Especificacion-Funcional/Glosario-Funcional.md`, que ya declaraba que **la produce el sistema** y que el panel no tiene dónde escribirla, y contradice hoy a **RN-14**, que el intake 1.10 incorpora. La entrada pasa a decir que **el sistema la produce** y conserva lo demás: el administrador se la comunica fuera del producto, la superficie la muestra una sola vez y no se dice «contraseña temporal». **Ningún otro término cambia, y el recuento de términos acuñados no cambia.** Sube minor. |
