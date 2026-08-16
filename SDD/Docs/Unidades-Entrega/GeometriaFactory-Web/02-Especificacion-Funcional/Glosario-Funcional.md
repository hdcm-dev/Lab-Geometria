# Glosario funcional — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Glosario-Funcional.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Tabla de términos que esta categoría acuña»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Alcance de este glosario

### 1.1 `GeometriaFactory-Web`

Declara únicamente el vocabulario que la especificación funcional de `GeometriaFactory-Web` **acuña**, y referencia lo que ya está declarado en `Vision-Producto.md` §9, que es el glosario raíz de la cadena, o en los glosarios de los proyectos de código de los que esta pieza depende. Ningún término de §4 se redefine acá.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo caso de uso se define ahí y no entra.

### 1.2 `GeometriaFactory-Visor`

Declara el vocabulario que la especificación funcional de `GeometriaFactory-Visor` **acuña**: el de la escena, la malla, el árbol, la selección y la instancia. Todo lo que ya declara el glosario raíz del producto —`Vision-Producto.md` §9— se referencia en §4 y **no se redefine acá**.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo artefacto se define ahí y no entra.

## 2. Términos que esta categoría acuña

### 2.1 `GeometriaFactory-Web`

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Pieza pública | Este proyecto de código en su papel de servicio desplegable: el front del hosting público y el único punto de contacto del navegador. **Se escribe siempre calificado**, por §3.2 | `Especificacion-Funcional.md`, CU-10001 a CU-10010, `README.md` | Referente declarado en el glosario raíz §9.2; acá se registra su uso, no se redefine |
| Pieza de datos | El otro servicio desplegable del producto: el que sostiene el dato y las reglas. La pieza pública lo invoca desde su servidor y nunca desde el navegador | `Especificacion-Funcional.md`, CU-10001 a CU-10010 | Referente declarado en el glosario raíz §9.2 |
| Circuito | La conexión viva entre el navegador y la pieza pública, que sostiene la interacción y **termina ahí**: no llega a la pieza de datos | `Especificacion-Funcional.md`, CU-10002, CU-10005, CU-10007, CU-10010 | — |
| Estado del circuito | La memoria que la pieza pública mantiene, **del lado de su propio servidor**, asociada al circuito de una persona. Es donde vive la credencial de sesión, y es lo único que la pieza pública conserva: no es persistencia | `Especificacion-Funcional.md` §6 (RT-02, RT-06), CU-10002, CU-10010 | — |
| Marca de sesión | Lo único que la pieza pública deja en el navegador para reconocer el circuito de una persona. **No transporta la credencial de sesión ni ningún dato de la cuenta** | `Especificacion-Funcional.md` §6 (RT-02), CU-10001, CU-10002 | — |
| Estado degradado | La situación en la que la pieza pública sigue en pie y no puede obtener datos porque la pieza de datos no responde. Se presenta con aviso explícito, sin dirección de servicio interno y sin excepción sin manejar, y **se distingue de un listado vacío por el tipo recibido y no por el conteo** | `Especificacion-Funcional.md` §6, CU-10001, CU-10003 a CU-10010 | El término lo acuña `GeometriaFactory-Contracts`; acá se usa con la misma semántica, aplicada a lo que la persona ve |
| Cartel de reconexión | El aviso propio del circuito cuando la conexión con el navegador se corta. **Es distinto del estado degradado**: habla del tramo navegador–pieza pública y no de la pieza de datos | `Especificacion-Funcional.md` §6, CU-10002, CU-10010 | — |
| Panel | El conjunto de rutas que la pieza pública arma para una persona según su papel. Hay dos: el panel del alumno y el panel del administrador. **No es desplegable por separado** | `Especificacion-Funcional.md`, CU-10002, CU-10004, CU-10006, CU-10008, CU-10009 | Se corresponde con lo que `Vocabulario-Rules.md` §2 llama módulo |
| Ruta protegida | Cada ruta que la pieza pública no arma sin sesión, o sin el papel que corresponde. Acota lo que se ofrece; **no hace cumplir ninguna regla**, que es tarea de la pieza de datos | `Especificacion-Funcional.md` §6 (RT-09), CU-10002, CU-10004 | — |
| Vista de trabajo | La página que presenta un trabajo con sus **cuatro partes**: datos y texto original a la izquierda; elemento de dibujo arriba y árbol de la estructura abajo, a la derecha. Su disposición viene decidida aguas arriba y probada en el aula | `Especificacion-Funcional.md` §7, CU-10005, CU-10007, CU-10009 | Primer referente de «vista»; ver §3.1 |
| Árbol de la estructura | La representación jerárquica y colapsable del texto original del trabajo, que la pieza pública arma a partir de la estructura que devuelve la fachada del visualizador | `Especificacion-Funcional.md` §7, CU-10005, CU-10007 | «árbol del texto». No se usa ninguna forma que nombre el formato del texto: el vocabulario del producto habla del texto del alumno, no de su sintaxis |
| Componente anfitrión del visualizador | El componente de la pieza pública que aloja el elemento de dibujo, invoca las **seis** funciones de la fachada y **opera el ciclo de vida de la instancia**. Es quien invoca `destruir` al descartarse, y quien **consulta el entorno del navegador y manda los dos valores de verdad** del movimiento automático: el bundle no consulta nada | `Especificacion-Funcional.md` §7, CU-10005, CU-10007 | El término lo acuña `GeometriaFactory-Visor`; acá se precisa quién lo encarna |
| Previsualización | El dibujo de la escena a partir de un texto que la persona todavía no envió. **No anticipa el estado del trabajo**: quien decide si el texto verifica es la pieza de datos | `Especificacion-Funcional.md` §7, CU-10005, CU-10007 | — |
| Contraseña provisoria | La credencial que **el sistema produce** cuando el administrador resetea la contraseña de la cuenta de un alumno, y que el administrador le comunica **fuera del producto**, porque no hay canal de correo. **El panel no tiene dónde escribirla**: se la muestra una vez. Sirve para una sola cosa: entrar a cambiarla | `Especificacion-Funcional.md` §6 (RT-12), CU-10002, CU-10003, CU-10004 | «clave provisoria». **No se dice «contraseña temporal»**: no vence por tiempo, sólo por uso |
| Cambio de contraseña pendiente | La marca que deja el reseteo sobre una cuenta y que la confina a cambiar su propia contraseña. **Ninguna otra ruta se arma mientras esté puesta**, y quien lo hace cumplir es la pieza de datos | `Especificacion-Funcional.md` §6 (RT-12), CU-10002, CU-10003, CU-10004 | «marca», en forma corta. No es una situación de cuenta: convive con la cuenta habilitada sin reemplazarla |
| Cambio forzado | El tercer curso de la superficie de credencial propia: el cambio que hace, obligada, la persona a la que le resetearon la contraseña. Mismo formulario que el cambio voluntario, **sin salida** | `Especificacion-Funcional.md` §3.1, CU-10002, CU-10003 | — |
| Reseteo de contraseña | La quinta operación del panel de cuentas: el administrador reemplaza la credencial de un alumno por una provisoria, **cualquiera sea la situación de esa cuenta**. **No es una baja y no elimina ningún trabajo** | `Especificacion-Funcional.md` §3, CU-10004 | «resetear la clave». **No se dice «recuperación»**: la recuperación autónoma sigue sin existir |
| Confirmación escrita | La exigencia de escribir un valor conocido —el correo de la cuenta— antes de ejecutar una operación destructiva. Existe para que la operación sea difícil de ejecutar por accidente | `Especificacion-Funcional.md`, CU-10004, CU-10009 | — |
| Acción única de guardado | El hecho de que el alumno disponga de **una sola** acción para guardar un trabajo: enviar. No existe «guardar sin enviar», y de ahí que `Borrador` signifique exactamente «el texto no verificó» | `Especificacion-Funcional.md` §3.1, CU-10005, CU-10006 | Referencia la entrada «Enviar» del glosario raíz §9.1 |
| Retiro de un trabajo | La eliminación que el administrador ejerce sobre cualquier trabajo que ve, en cualquiera de sus tres estados visibles. Se distingue de la eliminación del alumno, acotada al estado `Borrador` | `Especificacion-Funcional.md`, CU-10006, CU-10009 | — |
| Papel | El valor, dentro de un conjunto cerrado de dos, con el que una persona opera: alumno o administrador. La pieza pública lo recibe en la respuesta de sesión y lo usa para decidir qué panel arma; **no lo hace cumplir** | `Especificacion-Funcional.md`, CU-10002, CU-10004, CU-10006, CU-10007, CU-10008, CU-10009 | El término lo acuña `GeometriaFactory-Contracts`. No se usa «rol» |
| Situación de cuenta | El valor, dentro de un conjunto cerrado, que declara si una cuenta está pendiente, habilitada o bloqueada | `Especificacion-Funcional.md`, CU-10001, CU-10002, CU-10003, CU-10004 | El término lo acuña `GeometriaFactory-Contracts`. Se prefiere «situación» para no colisionar con el estado del trabajo |
| Desenlace | La decisión del administrador que resuelve un trabajo en estado `Pendiente`, dentro de un conjunto cerrado de dos valores: aprobar y rechazar | `Especificacion-Funcional.md`, CU-10006, CU-10007, CU-10008, CU-10009 | El término lo acuña `GeometriaFactory-Contracts`. **No se usa para el resultado de interpretar un texto**, que es «resultado de la interpretación» |
| Elemento de dibujo | El elemento de la página sobre el que la instancia del visualizador dibuja, provisto por el componente anfitrión | `Especificacion-Funcional.md` §7, CU-10005, CU-10007 | El término lo acuña `GeometriaFactory-Visor`; no se usa el nombre técnico del elemento |

## 3. Términos con más de un referente

### 3.1 `GeometriaFactory-Web`

Se declaran los tres términos cuyos sentidos **colisionan en el mismo contexto de lectura**, según el criterio de `Vocabulario-Rules.md` §9.2: el contexto de lectura de un subagente es la sección, no el documento. No se reporta ningún otro caso: los términos cuyos sentidos se distinguen solos quedan fuera, por la prohibición de §9.4 y por el anti-patrón de `Rules-Especificacion-Funcional.md` §4.5 sobre calificar ocurrencias de contextos disjuntos.

### 3.1 Vista

Tres referentes, y es el término polisémico propio de este proyecto de código: los otros dos ya venían declarados aguas arriba.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| La página que presenta un trabajo, con sus cuatro partes | **«vista de trabajo»**, siempre calificada | CU-10005, CU-10007, CU-10009; `Especificacion-Funcional.md` §7 |
| La porción de página que un componente arma —lo que en la prosa de interfaz se llamaría una pantalla o una parte de ella— | **No se usa «vista» para esto.** Se escribe «página», «ruta» o «componente», según qué se esté nombrando | Todos los casos de uso |
| La perspectiva de datos que un papel obtiene: «lo que el administrador ve» | **No se usa «vista» para esto.** Se escribe «lo que el papel ve», «alcance» o «visibilidad» | CU-10006, CU-10007, CU-10008, CU-10009 |

**Forma desnuda admitida.** Dentro de una sección donde ya se nombró «vista de trabajo» en su forma completa, «la vista» sin calificar se admite: el referente ya quedó fijado para quien lee esa sección, que es la unidad de despacho. En títulos, en cabeceras de trazabilidad y en la primera mención de cada sección se escribe la forma calificada. Es el tratamiento estándar de una familia calificada según `Vocabulario-Rules.md` §9.2.

**Locución que no es este término.** «A la vista» —«tiene a la vista», «conserva a la vista», «el motivo a la vista»— es una locución del español corriente que significa «disponible para mirar» y **no nombra ninguno de los tres referentes**. No se califica ni se sustituye: hacerlo sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

Evidencia de que los contextos colisionan: en CU-10007 conviven, en la misma sección, la página que presenta el trabajo y la afirmación de que el administrador ve lo mismo que el alumno. Un subagente que reciba esa sección suelta y lea «la vista es la misma» no puede decidir si le hablan de la página o del alcance de datos, y las dos lecturas producen decisiones distintas aguas abajo: una es de disposición y la otra es de autorización. La resolución adoptada es la **forma calificada obligatoria** para el primer referente y la sustitución por otro término en los otros dos, que es la más barata que resuelve el caso: una entrada de glosario sola no alcanzaba, porque la forma desnuda seguiría apareciendo en secciones que se despachan por separado.

### 3.2 Pieza

Dos referentes, declarados en el glosario raíz y reproducidos acá porque los dos aparecen en esta categoría, muchas veces en la misma sección.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo, cuya identidad es su índice en ese conjunto | **Forma desnuda**: «pieza», «las piezas del trabajo», «índice de pieza» | CU-10005, CU-10007 |
| Cada uno de los dos servicios desplegables del producto | **Siempre calificado**: «pieza pública», «pieza de datos», o «piezas desplegables» en su forma colectiva | `Especificacion-Funcional.md`, los diez casos de uso |

Evidencia de que los contextos colisionan: CU-10007 nombra en la misma sección las piezas que se dibujan y la pieza de datos que devuelve el detalle. La forma desnuda queda reservada al referente del dominio, y los sinónimos informales «mitad» y «parte» **no se usan** para el segundo referente.

### 3.3 `Pendiente`

Dos referentes, y la regla que los separa es **vinculante para toda la documentación generada**, declarada aguas arriba en `PRODUCT-INTAKE` §4.2 y en el glosario raíz §9.2.

| Referente | Forma que corresponde | Dónde se lee |
| --- | --- | --- |
| La situación de una cuenta registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | CU-10001, CU-10002, CU-10003, CU-10004 |
| El estado de un trabajo enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»** | CU-10005, CU-10006, CU-10007, CU-10008, CU-10009 |

**La forma desnuda no se usa.** Dos excepciones que no son formas desnudas y por lo tanto no se califican:

1. **Las enumeraciones del conjunto cerrado de estados** —«`Borrador`, `Pendiente`, `Finalizado` y `Rechazado`»—, donde el conjunto que se enumera ya fija el referente y calificar cada miembro sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.
2. **Los identificadores literales** que aparecen entre acentos graves como valor de un campo o de una tabla de estados.

Evidencia de que los contextos colisionan: CU-10002 habla en la misma sección de una cuenta que espera habilitación, y CU-10006 y CU-10008 hablan de trabajos que esperan revisión; en el flujo de alta y en el de revisión los dos sentidos pueden aparecer en el mismo párrafo, y la forma desnuda no los separa.

### 3.2 `GeometriaFactory-Visor`

### 3.1 «Pieza»

Es el término polisémico verificado de esta categoría, y su colisión ya está declarada aguas arriba en `Vision-Producto.md` §9.1 y §9.2. Los dos referentes:

| Referente | Qué designa | Forma que le corresponde |
| --- | --- | --- |
| Del dominio | Cada figura del conjunto raíz del trabajo, identificada por su índice | **Forma desnuda**: «pieza». Es el único referente que usan los siete casos de uso de esta categoría, porque `seleccionarPieza` opera sobre él |
| De la composición del producto | Cada uno de los artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» |

Evidencia de la colisión y su resolución: el glosario raíz ya verificó que los dos sentidos comparten contexto de lectura dentro del producto y por eso obligó a calificar el segundo (`Vision-Producto.md` §9.2, corrección H-01). Esta categoría **no reabre la verificación ni agrega calificaciones**: adopta la resolución vigente. En los artefactos de 02 de este proyecto de código el segundo referente no aparece, salvo en esta entrada, que existe para que un subagente que lea una sección suelta sepa a qué apunta la forma desnuda.

### 3.2 «Resultado de la interpretación», precisión que no es polisemia declarada

`PRODUCT-INTAKE` §17.7 P.3 describe lo que `cargarJson` devuelve como «el resultado de la interpretación». Dentro de este proyecto de código ese valor se nombra **resultado de dibujo**, porque el resultado de la interpretación del producto es otra cosa: lo produce el backend, lleva observaciones y decide si un trabajo puede finalizarse. No es una polisemia que esta categoría declare abierta, es una precisión de nombre para evitar que se cree una: la fachada no interpreta trabajos, dibuja piezas.

### 3.3 «Órbita», precisión que tampoco es polisemia declarada

La escena de toda instancia tiene una **cámara orbital**: es la que responde al arrastre de quien mira, existe desde `inicializar` y no depende de ningún movimiento automático. La **órbita de la cámara** de §2 es otra cosa: el movimiento automático que hace girar sola esa misma cámara, prendido o apagado por la fachada. No es una polisemia abierta —los dos usos apuntan al mismo objeto, la cámara, y se distinguen por quién la mueve—, pero se declara acá porque confundirlos llevaría a leer que apagar el movimiento automático deja la escena sin cámara orbital, que es falso.

### 3.4 Verificación negativa

Se revisaron los demás términos acuñados en §2 buscando referentes múltiples dentro de esta categoría. **Ninguno verificado** además de los tratados arriba. En particular, no se califican «escena», «malla», «árbol» ni «instancia», cuyos contextos de uso son disjuntos de cualquier otro sentido presente en el corpus del producto: calificarlos sería el falso positivo que `Vocabulario-Rules.md` §9.1 declara defecto.

## 4. Términos referenciados y no redefinidos

### 4.1 `GeometriaFactory-Web`

Los siguientes ya están declarados en `Vision-Producto.md` §9 y **no se redefinen acá**. Se listan porque aparecen en más de un artefacto de esta categoría y un lector que entre por una sección suelta necesita saber dónde está su definición canónica.

| Término | Dónde está declarado | Nota de uso en esta categoría |
| --- | --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 | Es la unidad que el alumno carga. **No es una «unidad de entrega»**: es un registro de datos y no se despliega |
| Laboratorio | §9.1 | Nombre corriente del producto en uso. No se confunde con el calificador «de aula» |
| Observación | §9.1 | Superordinado de «advertencia» y «error de validación». **El comentario del administrador no es una observación** |
| Advertencia | §9.1 | Discrepancia entre valor declarado y derivado. No impide que el trabajo pase a estado `Pendiente` |
| Error de validación | §9.1 | Impide que el trabajo pase a estado `Pendiente` |
| Comentario | §9.1 | Texto libre y opcional del administrador. No es calificación y no es observación; hay a lo sumo uno por trabajo |
| Estado del trabajo | §9.1 | Conjunto cerrado de cuatro valores, con `Finalizado` y `Rechazado` terminales |
| Enviar | §9.1 | La única acción de guardado del alumno |
| Aprobar / Rechazar | §9.1 | Las dos decisiones del administrador, y su facultad exclusiva |
| Valor declarado / valor derivado | §9.1 | El par completo es lo que hace visible el error de fórmula |
| Componente | §9.1 | Figura plana que forma parte de una pieza. **No se confunde con el componente de interfaz**, que en esta categoría se nombra siempre con su función: «componente anfitrión del visualizador» |
| Fallo silencioso | §9.1 | Error que no produce mensaje. Es lo que el producto viene a eliminar, y por eso ninguna pieza desaparece de la escena sin quedar enumerada |
| Actividad 1 | §9.1 | El emisor del texto que consume el producto. No forma parte del producto |
| Etapa | §9.2 | Cada tramo de construcción, con su punto de control |
| Puerta técnica | §9.2 | Verificación de viabilidad que condiciona la planificación |
| Capacidad | §9.2 | Cada ítem del alcance funcional del intake, con identificador `F-XX`. No es sinónimo de caso de uso |

Del glosario de `GeometriaFactory-Contracts` se referencian, con la misma semántica y sin redefinirse: «respuesta de error neutra», «índice de figura», «campo señalado», «proyección de listado», «detalle del trabajo», «credencial de sesión», «texto original del trabajo», «estado terminal» y «señal declarada que no es error». Del contrato de fachada de `GeometriaFactory-Visor` se referencian «instancia del visor», «identificador de instancia» y «resultado de dibujo».

### 4.2 `GeometriaFactory-Visor`

Los siguientes términos ya están declarados en el glosario raíz del producto y **se usan con esa misma semántica**. Puntero único: `../00-Contexto/Vision-Producto.md` §9.

| Término | Dónde está declarado | Cómo lo usa esta categoría |
| --- | --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 | Lo que el alumno entrega en el laboratorio. La fachada nunca lo guarda ni lo conoce como registro: sólo recibe su texto |
| Pieza (referente del dominio) | `Vision-Producto.md` §9.1 | Cada figura del conjunto raíz del trabajo. Ver §3.1 |
| Componente (figura plana de una pieza) | `Vision-Producto.md` §9.1 | Tapa, cara, base, lateral o lado, de donde la fachada lee las dimensiones del volumen. **No confundir con «componente anfitrión»**, que es término de §2 y designa a quien invoca la fachada |
| Observación, advertencia, error de validación | `Vision-Producto.md` §9.1 | Se nombran únicamente para declarar que **este proyecto de código no emite ninguna de las tres**: son del backend |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 | Se nombran para declarar que la fachada no los compara ni los recalcula |
| Tapa | `Vision-Producto.md` §9.1 | Nombre de clave del que la fachada lee dimensiones, aceptando la variante del dominio del emisor |
| Rectángulo desarrollado | `Vision-Producto.md` §9.1 | Componente `Lado` del cilindro. La fachada lo usa para leer una dimensión y no lo dibuja como pieza del conjunto raíz |
| Coma final | `Vision-Producto.md` §9.1 | Particularidad del texto del alumno. Ver la nota de CU-12002 §10 sobre qué escenario la ejercita |
| Fallo silencioso | `Vision-Producto.md` §9.1 | Es lo que la garantía de enumeración de piezas no dibujadas elimina |
| Laboratorio | `Vision-Producto.md` §9.1 | Nombre corriente del producto en uso |
| Actividad 1, `Describir()` | `Vision-Producto.md` §9.1 | Emisor del dato. No forma parte del producto |
| Pieza en su segundo referente | `Vision-Producto.md` §9.2 | Forma siempre calificada. Ver §3.1 |
| Capacidad (`F-XX`) | `Vision-Producto.md` §9.2 | Ítem del alcance funcional del intake. No es sinónimo de caso de uso |

**Choque de vocabulario vigente** (`Vision-Producto.md` §9.3, `PRODUCT-INTAKE` §12.1), respetado en los siete casos de uso y en el documento de concepto: «proyecto de código» designa exclusivamente una unidad de compilación, la palabra «proyecto» a secas no se usa, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

## 5. Tabla de términos que esta categoría acuña

### 5.1 `GeometriaFactory-Visor`

| Término | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Fachada | Superficie pública del archivo de guion: las **seis** funciones planas que el componente anfitrión puede invocar, y nada más | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | «Contrato de fachada» cuando se nombra el conjunto de funciones más sus garantías |
| Componente anfitrión | El componente que embebe el archivo de guion e invoca sus funciones. Es el actor primario de los siete casos de uso. No es una persona, y la fachada no sabe qué componente es | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | «Componente anfitrión mínimo» cuando se trata de la página integradora sin backend de CU-12006 |
| Elemento de dibujo | Elemento de la página, provisto por el componente anfitrión, sobre el que una instancia monta su escena | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12004, CU-12005, CU-12006 | — |
| Instancia del visor | Escena viva asociada a un elemento de dibujo. Nace con `inicializar` y termina con `destruir` | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | «Instancia», en su forma corta, dentro de esta categoría |
| Identificador de instancia | Valor opaco que `inicializar` devuelve y que las otras cinco funciones exigen. Identifica una instancia viva y deja de ser válido cuando se la libera | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | — |
| Escena | Espacio tridimensional de una instancia, con su iluminación y su cámara orbital, donde se ubican las mallas | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | — |
| Malla | Representación tridimensional que la fachada construye para una pieza dibujable y ubica en la escena | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12003, CU-12005 | — |
| Tipo dibujable | Cada uno de los seis tipos de pieza que la fachada sabe convertir en malla: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado` y `Circulo` | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12006 | «Pieza dibujable» para la pieza cuyo tipo lo es |
| Resultado de dibujo | Lo que `cargarJson` devuelve: piezas dibujadas con su índice y su tipo, piezas no dibujadas con su índice y su código de condición, y la estructura del texto. **No lleva observaciones** | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12003, CU-12006 | El intake lo nombra «el resultado de la interpretación» (§17.7 P.3); ver la nota de §3 |
| Estructura del texto | Representación jerárquica del texto recibido que la fachada devuelve para que el componente anfitrión la presente como árbol colapsable | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12006 | — |
| Árbol | Presentación colapsable de la estructura del texto. La arma el componente anfitrión con lo que la fachada le devuelve | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12003, CU-12006 | «Árbol colapsable», forma completa del glosario raíz de la necesidad NB-00006 |
| Selección | Estado de a lo sumo una pieza resaltada por instancia. Se fija por índice y se descarta al cargar un trabajo nuevo o al destruir la instancia | `Definicion-Contrato-De-Fachada.md`, CU-12003, CU-12004, CU-12005 | «Resaltado» para el efecto visible de la selección |
| Índice de pieza | Posición de una pieza en el conjunto raíz del trabajo. Es su identidad, porque el dato del alumno no trae identificador propio, y es la clave con la que el árbol y la escena se sincronizan | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12003, CU-12006 | — |
| Disposición | Ubicación relativa de las piezas en la escena. Se **deriva del índice** de cada pieza, de modo que dos cargas del mismo texto producen la misma disposición | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12004, CU-12006 | «Disposición determinista» cuando se enuncia la propiedad |
| Texto del trabajo | Dato de entrada de `cargarJson`. La fachada lo lee para construir mallas, y ni lo pide, ni lo guarda, ni lo reescribe | `Definicion-Contrato-De-Fachada.md`, CU-12002, CU-12006 | — |
| Código de condición | Código con el que la fachada informa por qué una invocación no surtió efecto, o por qué una pieza no se dibujó. Es una condición de contrato, **no** una observación de dominio | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | — |
| Cero red | Propiedad de la fachada: ninguna función origina una petición de red. El umbral es exactamente 0, medido contando peticiones | `Definicion-Contrato-De-Fachada.md`, CU-12001 a CU-12007 | — |
| Cero persistencia | Propiedad de la fachada: no guarda estado entre páginas ni escribe en el almacenamiento del navegador | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12002, CU-12005, CU-12006 | — |
| Página integradora | Página sin ninguna pieza del backend que carga el archivo de guion, recibe un texto pegado a mano y ejerce las seis funciones. Es el componente anfitrión de CU-12006 y el sample S-1 del producto | `Definicion-Contrato-De-Fachada.md`, CU-12006 | «Página de prueba del visor» en el intake §18 |
| Capacidad gráfica tridimensional | Capacidad que el navegador debe proveer para que exista una instancia. Se declara por capacidad y no por número de versión | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12006 | — |
| Movimiento automático | Movimiento que la escena ejerce sola, sin que la persona la toque. Son **dos e independientes** —órbita de la cámara y giro de las figuras—, los gobierna la fachada y ninguno altera la disposición | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12002, CU-12004, CU-12005, CU-12006, CU-12007 | Capacidad **F-25** del alcance del producto. «Los dos movimientos», en su forma corta dentro de esta categoría |
| Órbita de la cámara | Movimiento automático en el que **la cámara gira sola** alrededor del conjunto y las piezas quedan quietas. Existe en el visualizador previo y se porta | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12006, CU-12007 | No confundir con la **cámara orbital** de la escena, que es la que responde al arrastre de la persona y existe con la órbita apagada |
| Giro de las figuras | Movimiento automático en el que **cada pieza rota sobre su eje vertical, en su lugar**, sin salir de la celda que le asignó su índice. Al apagarlo, cada pieza vuelve a su orientación de partida. Es capacidad nueva: no existe en el visualizador previo | `Definicion-Contrato-De-Fachada.md`, CU-12001, CU-12006, CU-12007 | — |
| Estado efectivo del movimiento | Estado en que quedan los dos movimientos después de gobernarlos, que `establecerMovimiento` devuelve para que el componente anfitrión sincronice sus controles con lo que la escena hace | `Definicion-Contrato-De-Fachada.md`, CU-12007 | — |

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
