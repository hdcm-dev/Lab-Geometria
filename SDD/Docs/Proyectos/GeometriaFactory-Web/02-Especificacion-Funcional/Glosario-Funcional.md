# Glosario funcional — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Glosario-Funcional.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena: §9.1 términos del dominio del cliente, §9.2 términos que esa categoría precisa, §9.3 resolución del choque de vocabulario); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1, §4.2, §12 y §12.1, §14, §17.6 P.3, P.4, P.5, P.10 y P.11; `../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Glosario-Funcional.md`; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §2; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `../03-UX-UI-DX/`, cuyo `Glosario-UX.md` referencia estos términos en lugar de duplicarlos; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Vista](#31-vista)
  - [3.2 Pieza](#32-pieza)
  - [3.3 `Pendiente`](#33-pendiente)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Declara únicamente el vocabulario que la especificación funcional de `GeometriaFactory-Web` **acuña**, y referencia lo que ya está declarado en `Vision-Producto.md` §9, que es el glosario raíz de la cadena, o en los glosarios de los proyectos de código de los que esta pieza depende. Ningún término de §4 se redefine acá.

La regla de inclusión aplicada es la de `Rules-Especificacion-Funcional.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo caso de uso se define ahí y no entra.

## 2. Términos que esta categoría acuña

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

## 4. Términos referenciados y no redefinidos

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

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial. Declara **veinte** términos acuñados por la especificación funcional de la pieza pública, tres términos con más de un referente —«vista», «pieza» y `Pendiente`—, cada uno con su evidencia de colisión y su forma resuelta, y dieciséis términos referenciados del glosario raíz más los referenciados de los glosarios de `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`. |
| 1.0 | 2026-08-09 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Web-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-07**: el conteo de términos acuñados decía «diecinueve» y la tabla de §2 tiene **veinte** filas; se corrige el número. Se corrige **dentro de la fila de emisión** —y no como excepción a la regla de no reescribir una fila ya escrita— porque esa fila no describe un estado anterior sino el contenido vigente de este mismo documento, y era la única ocurrencia del conteo en todo el artefacto. El conteo de términos referenciados, dieciséis, se verificó por recuento y ya era correcto. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**. §2 pasa de **veinte a veinticuatro** términos acuñados, con los cuatro que trae la capacidad **F-26**: «contraseña provisoria», «cambio de contraseña pendiente», «cambio forzado» y «reseteo de contraseña», los dos primeros con un alias prohibido y su motivo. La entrada «componente anfitrión del visualizador» pasa de cinco a **seis** funciones y declara que es quien consulta el entorno del navegador y manda los dos valores de verdad del movimiento automático, por la frontera que 1.7 fijó para **F-25**. Sube minor: agrega términos sin redefinir ninguno. |
| 1.2 | 2026-08-09 | Absorbe las dos decisiones del Product Owner sobre **F-26** que `CU-10004` 1.3 aplica. §2 corrige dos definiciones **sin dar de alta ni de baja ningún término**: **contraseña provisoria** pasa a declarar que **la produce el sistema** y que el panel no tiene dónde escribirla, y **reseteo de contraseña** explicita que procede **cualquiera sea la situación de la cuenta**. Los recuentos de §2 no cambian: siguen siendo veinticuatro términos acuñados. **Autor:** Analista Funcional senior (AG-02) |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: la fila 1.2 de este control de cambios tenía **cuatro celdas en una tabla de tres columnas**. El texto de la fila se conserva íntegro y el autor pasa a leerse dentro de la celda de cambios, en lugar de en una cuarta columna que la tabla no declara. **Ningún término cambia de definición, de forma obligatoria ni de criterio de inclusión**, y el recuento de veinticuatro términos acuñados no cambia. Sube minor: repara la tabla sin alterar lo que sus filas dicen. |
