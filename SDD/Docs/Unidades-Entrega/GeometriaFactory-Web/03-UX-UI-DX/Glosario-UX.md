# Glosario de experiencia — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Glosario-UX.md
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

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. Alcance de este glosario

### 1.1 `GeometriaFactory-Web`

Declara únicamente el vocabulario **de superficie** que esta categoría acuña, y referencia lo que ya está declarado aguas arriba: en `Vision-Producto.md` §9, que es el glosario raíz de la cadena, y en `Glosario-Funcional.md` de la categoría 02 de este mismo proyecto de código. **Ningún término de §5 se redefine acá.**

La regla de inclusión aplicada es la de `Rules-UX-UI-DX.md` §3.3: entra todo término que aparece en más de un artefacto de esta categoría. Un término que vive en un solo wireframe se define ahí y no entra.

Esta sección tiene un riesgo de vocabulario propio y concentrado: **pantalla, vista, panel, superficie y sección** son palabras que una categoría de diseño usa todo el tiempo, y tres de ellas ya tienen dueño aguas arriba. §2 declara las que se acuñan, §3 las que colisionan dentro de esta sección y §4 las que deliberadamente no se usan. Que §4 exista es la parte que importa: una palabra prohibida sin registro reaparece en el primer documento que alguien escriba después.

### 1.2 `GeometriaFactory-Visor`

Declara únicamente el vocabulario que **esta categoría** acuña: el de la superficie pública vista desde quien la integra, el del recorrido de integración y el del diagnóstico de condiciones. Es obligatorio para los ocho tipos D8, también en variante DX, porque los tipos DX acuñan el vocabulario de su propia superficie pública (`Rules-UX-UI-DX.md` §2.1).

Rige la **regla de no duplicación** de `Rules-UX-UI-DX.md` §3.3: todo término que ya está en `Glosario-Funcional.md` de 02 con la misma semántica se **referencia** y no se redefine, y lo mismo vale para el glosario raíz del producto. Las dos listas de referencia están en §4, y ninguna entrada de §2 las pisa.

Regla de inclusión aplicada: entra todo término que aparece en **más de un artefacto de esta categoría**. Un término que vive en un solo artefacto se define ahí y no entra acá.

## 2. Términos que esta categoría acuña

### 2.1 `GeometriaFactory-Web`

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y notas |
| --- | --- | --- | --- |
| **Superficie** | La unidad de diseño y de maquetado de esta categoría: una ruta con su conjunto propio de estados, un diálogo con flujo propio, o un bloque alojado dentro de otra superficie con su mapa de estados propio. Es lo que un archivo `wireframes-<superficie>` documenta y lo que la maqueta de la Fase B2 materializa como una unidad navegable | Todos | Es el término canónico. **No se usa «pantalla» como sinónimo**; ver §4 |
| **Nombre canónico de superficie** | El identificador estable de una superficie, en Título-Con-Guiones, declarado en la sección 1 de su wireframe y reusado sin cambios por la maqueta y por la línea de base visual | `Experiencia-De-Uso.md` §3.1, los once wireframes | Cambiarlo rompe la trazabilidad con la maqueta |
| **Superficie alojada** | La superficie que **no tiene ruta**: se dibuja dentro de otra superficie y sólo se llega a ella llegando a su anfitriona. Tiene nombre canónico, mapa de estados propio y lista de interacciones propia —por eso se documenta en un wireframe separado—, pero **no es un destino de navegación y no se construye como página aparte**. Este producto tiene exactamente una: `Resolucion-Del-Trabajo`, alojada en `Vista-De-Trabajo` | `Wireframes-Resolucion-Del-Trabajo.md` §1, `Wireframes-Vista-De-Trabajo.md` §1 y §3, `Experiencia-De-Uso.md` §3.1 | El calificador «alojada» es parte del término. Sin él, un wireframe separado se lee como pantalla propia, que es exactamente el malentendido que la validación visual de la Fase B2 expuso. Una exhibición aislada de una superficie alojada es **instrumento de validación**, no ruta del producto |
| **Movimiento automático de la escena** | El movimiento que la escena tridimensional hace **sin que la persona la arrastre**, gobernado por dos controles independientes: la **órbita de la cámara**, que gira el punto de vista alrededor del conjunto dejando las piezas quietas, y el **giro de las figuras**, que rota cada pieza sobre su eje vertical en su lugar. Son **preferencias de quien mira**, no instrumento de validación, y por eso viven junto al dibujo | `Wireframes-Vista-De-Trabajo.md` §3 y §7 | Realiza la capacidad F-25, `Must Have` desde el `PRODUCT-INTAKE` 1.7. Los dos se detienen mientras la persona arrastra y con la pestaña oculta, y arrancan apagados si el sistema declara preferencia de movimiento reducido: **esa preferencia la consulta el componente anfitrión**, que le manda al visor **dos valores de verdad**, uno por movimiento. **El visor no consulta nada.** **Ninguno altera la disposición de las piezas**, que sale del índice: el determinismo comprometido es de la posición, no de la orientación. «Órbita de la cámara» no redefine la «cámara orbital» del `Glosario-Funcional.md` del proyecto de código del visor: aquélla es el mecanismo, ésta es el movimiento que se prende y se apaga |
| **Contraseña provisoria** | La credencial que **el sistema produce** al resetear la contraseña de un alumno, y que el administrador le comunica **fuera del producto**, porque no hay canal de correo. **El panel no tiene dónde escribirla** (RN-10014). La superficie la muestra **una sola vez** y no la vuelve a mostrar | `Wireframes-Panel-De-Cuentas.md` §3, `Wireframes-Credencial-Propia.md` §3 | Realiza la capacidad F-26. **No se dice «contraseña temporal»**: no vence por tiempo, sólo por uso |
| **Cambio forzado** | El curso de `Credencial-Propia` al que llega, obligada, la persona a la que le resetearon la contraseña. Mismo formulario que el cambio voluntario, sobre el **shell de acceso** y **sin sesión**: la provisoria se reconoce y encamina, y no otorga sesión de trabajo (RN-10013) | `Wireframes-Credencial-Propia.md` §1 y §5, `Experiencia-De-Uso.md` §3.2 | No se dice «cambio obligatorio»: lo forzado es la situación, no la política de contraseñas |
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

### 2.2 `GeometriaFactory-Visor`

### 2.1 Roles y recorrido de integración

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Rol de intervención | Quien interviene **sobre** el proyecto de código, no quien usa el producto. En este proyecto de código son dos, y los cumple la misma persona más un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | Reemplaza a «audiencia» en las secciones DX (`Rules-UX-UI-DX.md`, control de cambios 1.7). «Audiencia» queda para las secciones UX, que no existen en esta categoría |
| Developer integrador del bundle | Rol de intervención que embebe el archivo de guion en una superficie anfitriona e invoca las seis funciones desde ella. No modifica el interior del archivo de guion | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` | «Integrador», en su forma corta, dentro de esta categoría. **No hay integrador externo**: el artefacto no se publica |
| Developer mantenedor del bundle | Rol de intervención que modifica el interior del archivo de guion —lectura de dimensiones, construcción de mallas, disposición, liberación— sin alterar el contrato | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Mantenedor», en su forma corta |
| Superficie pública | El conjunto de lo que un consumidor puede invocar: las **seis** funciones de la fachada, sus firmas, las siete garantías y los siete códigos de condición. Nada más. La sexta, `establecerMovimiento`, entró el 2026-08-09 y **no movió las otras dos cifras**: no acuña garantía ni código | Los cuatro artefactos de esta categoría | Se usa con la misma semántica que en `Definicion-Contrato-De-Fachada.md` §7, que la enuncia sin declararla como término |
| Recorrido de integración | Secuencia de invocaciones de las seis funciones que un rol de intervención ejecuta para verificar el contrato de punta a punta. Es la unidad de trabajo del onboarding y del quick-start | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Ver §3.1: **la forma desnuda «recorrido» no se usa** en esta categoría |
| Tramo de onboarding | Cada uno de los tres cortes temporales del onboarding —5, 30 y 60 minutos— con un objetivo verificable propio | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Tramo», en su forma corta, dentro de esta categoría |
| Objetivo verificable | Enunciado de cierre de un tramo que se cumple o no se cumple por observación directa, sin juicio intermedio | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | Camino más corto desde el repositorio hasta ver una pieza dibujada: cinco pasos, todos dentro del entorno de desarrollo contenido | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Ciclo corto de construcción | Camino que genera **sólo** el archivo de guion, sin compilar el resto del producto. Es el que rige para trabajar sobre el visor | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Se opone al **ciclo completo de construcción**, que encadena las dos cosas (PRODUCT-INTAKE §17.7 P.8) |

### 2.2 Documentación y su organización

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis con los que se organiza la documentación de este proyecto de código: **modo tutorial**, **modo how-to**, **modo reference** y **modo explanation**. Cada modo tiene un solo dueño declarado | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `README.md` | Los cuatro nombres se conservan en su forma original por ser los del marco. Ver §3.2 sobre «reference» |
| Lectura por sección | Propiedad de redacción de esta categoría: cada sección se escribe para ser legible sola, porque un agente de IA recibe secciones y no documentos (`Vocabulario-Rules.md` §9.2) | `DX-Developer-Experience.md`, `DX-Error-Messages.md` | — |

### 2.3 Diagnóstico y catálogo de condiciones

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Entrada de catálogo | Desarrollo documental de un código de condición para una función concreta, identificado `E-VIS-XX`. Un mismo código puede tener varias entradas, porque el trabajo que le queda al anfitrión cambia según desde qué función se produjo | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El identificador `E-VIS-XX` es **documental**: no forma parte del retorno de ninguna función y no se muestra a nadie |
| Diagnóstico accionable | Las tres partes obligatorias de una entrada de catálogo: qué pasó, por qué pasó y qué hacer al respecto. Sin la tercera, la entrada no está terminada | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | — |
| Acción del lado del anfitrión | Tercera parte del diagnóstico accionable. Es siempre trabajo del componente anfitrión, porque la fachada no puede resolver ninguna condición por su cuenta: no pide datos, no reintenta y no consulta nada | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Alcance de la condición | Si una condición afecta a la **invocación completa** o a una **pieza suelta** dentro de una carga exitosa. La distinción cambia lo que el anfitrión tiene que leer del retorno | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Gate de cero red | Verificación bloqueante de que el archivo de guion no origina ninguna petición: por inspección del código fuente y del artefacto generado, y contando peticiones en la pestaña de red durante la interacción. El umbral, exactamente 0, lo declara `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, lugar único de las seis propiedades transversales | `DX-Error-Messages.md`, `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | Es la verificación de la propiedad **cero red**, que declara `Glosario-Funcional.md`; el término de acá nombra el control, no la propiedad |
| Fuga de la fachada | Toda invocación a nombres internos del archivo de guion, o manipulación del elemento de dibujo, hecha por un anfitrión por fuera de las seis funciones. Tocar la escena para prender o apagar un movimiento automático, en lugar de invocar `establecerMovimiento`, es una fuga como cualquier otra. Su umbral es 0 y un valor distinto es defecto bloqueante | `DX-Developer-Experience.md`, `README.md` | — |

### 2.4 Medición

| Término | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| TTFS · time-to-first-success | Tiempo desde abrir el repositorio en el entorno de desarrollo contenido hasta ver dibujadas las piezas del escenario de cobertura | `DX-Developer-Experience.md`, `README.md` | Se mide con cronómetro y un solo observador: no hay telemetría posible en un proyecto de código sin red y sin persistencia |
| TTFV · time-to-first-value | Tiempo desde el primer éxito hasta modificar el interior del archivo de guion, regenerarlo y comprobar que el contrato quedó idéntico | `DX-Developer-Experience.md`, `README.md` | Mismo método de medición que TTFS |

## 3. Términos con más de un referente

### 3.1 `GeometriaFactory-Web`

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

### 3.2 `GeometriaFactory-Visor`

Criterio aplicado: `Vocabulario-Rules.md` §9.1. Se desambigua **sólo** cuando los sentidos comparten contexto de lectura, y el contexto de lectura de un subagente es la sección.

### 3.1 «Recorrido»

Es el término polisémico verificado de esta categoría. **Evidencia de colisión, por ocurrencia y en secciones donde los dos sentidos conviven:**

| Sección | Ocurrencia con el sentido de integración | Ocurrencia con el sentido de continuidad de uso |
| --- | --- | --- |
| `DX-Error-Messages.md` §4, tabla de situaciones que no son entradas del catálogo | Fila «Petición de red observada durante el recorrido de integración» | Fila siguiente: «Diez recorridos de ida y vuelta no deben degradar la visualización» |
| `Guia-Onboarding-Developer.md` §6, tabla de trazabilidad | Filas «CU origen» y «Tests previstos»: «recorrido de integración completo sin backend», «recorrido de integración de humo» | Fila «Necesidad de negocio»: «10 de 10 recorridos de ida y vuelta» |

Los dos sentidos conviven **dentro de una misma tabla**, que es el caso en que la entrada de glosario no alcanza: quien lee una fila no tiene a la vista la otra. Por eso la forma de desambiguación elegida es la **calificada obligatoria**, que es el segundo escalón de `Vocabulario-Rules.md` §9.3, y no el primero.

| Referente | Qué designa | Dónde nace | Forma que le corresponde |
| --- | --- | --- | --- |
| De integración | Secuencia de invocaciones de las seis funciones que verifica el contrato de punta a punta | Esta categoría (§2.1) | **Siempre calificado**: «recorrido de integración» |
| De continuidad de uso | Cada ida y vuelta entre trabajos, de los diez con que se verifica que la visualización no degrada | `NB-00006` §5, tercer criterio; `CU-12005` CA-04 | **Siempre calificado**: «recorrido de ida y vuelta» |

**La forma desnuda «recorrido» no se usa como sustantivo en esta categoría.** Es el corolario de `Vocabulario-Rules.md` §9.2: cuando conviven dos formas calificadas, el término desnudo es el defecto.

**Alcance exacto de la invariante, para que sea verificable por barrido.** Gobierna el **sustantivo en uso** —«el recorrido», «los recorridos»—, que es la forma que admite los dos referentes. No gobierna:

- Las **formas verbales y el participio** —«se recorren los tres tramos», «recorrer trabajos de ida y vuelta», «las seis funciones, recorridas en el orden de su ciclo de vida»—, donde el complemento del verbo fija el referente en la misma oración.
- Las **menciones metalingüísticas**, en las que el término se nombra a sí mismo entre comillas: el título de esta entrada, las filas de glosario que la citan y las entradas de control de cambios que la registran.

Extender la invariante a esas formas sería la **sobrecorrección** que `Vocabulario-Rules.md` **§9.1** tipifica como defecto: esa sección cierra diciendo que «la corrección que ese falso positivo induce —calificar todas las ocurrencias del término— **es** un defecto», con el énfasis en «es» que trae la fuente. No corresponde citar acá §9.4, que prohíbe otra cosa: declarar una invariante sin haber verificado que los contextos colisionan. Esa verificación sí se hizo, y su evidencia está más arriba en esta misma sección.

**Estado de cumplimiento verificado.** El audit `B-02-03-GeometriaFactory-Visor-r1.md`, hallazgo **H-03**, encontró la invariante declarada y no cumplida. `Vocabulario-Rules.md` §9.5 exige que el registro de una intervención léxica declare **cuántas ocurrencias se revisaron y cuántas se cambiaron**, porque es el par de cifras que permite distinguir una intervención por ocurrencia de una sustitución global disfrazada. Las dos:

| Cifra | Valor | Cómo se obtiene |
| --- | --- | --- |
| **Revisadas** | **61** ocurrencias de la raíz «recorrid» en los cinco artefactos de la categoría: **48** en el cuerpo y **13** en las entradas de control de cambios, que son el registro y no el corpus intervenido | Barrido de la raíz sobre los cinco archivos, clasificando cada ocurrencia por referente y por forma. El par cuerpo/registro se declara porque el total crece a cada ronda: el registro menciona el término al registrarlo |
| **Cambiadas** | **20** sustantivos desnudos en uso, calificados uno por uno y sin ninguna sustitución global: **nueve** en `DX-Developer-Experience.md`, **nueve** en `Guia-Onboarding-Developer.md` y **dos** en `DX-Error-Messages.md`. Más **un** participio de `Guia-Onboarding-Developer.md` §5 —«ya está recorrido»— sustituido por «ya se recorrió», que no admite lectura de sustantivo | Enumeración previa de las ocurrencias y sustitución sólo de las que cambiaban de forma, con barrido posterior de verificación |

Las ocurrencias revisadas y no cambiadas ya estaban calificadas, o son formas verbales, participios o menciones metalingüísticas que la invariante no gobierna. `README.md` §6 criterio 14 registra el estado corregido.

### 3.2 «Reference», modo de documentación

No es una polisemia declarada: es una precisión de nombre para no crear una. «Reference» designa acá **uno de los cuatro modos de Diátaxis**, y no la referencia bibliográfica ni la trazabilidad de un documento. Por eso los cuatro modos se escriben siempre con su calificador —«modo reference», «modo how-to», «modo tutorial», «modo explanation»— y nunca sueltos.

### 3.3 Verificación negativa

Se revisaron los demás términos acuñados en §2 buscando referentes múltiples dentro del corpus del producto. **Ninguno verificado** además del de §3.1.

En particular, **no se califican** «escena», «malla», «árbol» ni «instancia»: `Glosario-Funcional.md` §3.3 ya verificó que sus contextos son disjuntos y resolvió no calificarlos. Esta categoría **adopta esa resolución y no la reabre**; volver a calificarlos sería el falso positivo que `Vocabulario-Rules.md` §9.1 y §9.4 tipifican como defecto.

Tampoco se reabre la resolución de «pieza»: rige la del glosario raíz, con la forma desnuda reservada al referente del dominio y el segundo referente siempre calificado. En los artefactos de esta categoría el segundo referente **no aparece**.

## 4. Palabras de superficie que esta categoría deliberadamente no usa

### 4.1 `GeometriaFactory-Web`

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

### 5.1 `GeometriaFactory-Web`

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

### 5.2 `GeometriaFactory-Visor`

### 4.1 Del glosario funcional de este proyecto de código

Puntero único: [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md). Los **veinticuatro** términos que esa categoría acuña se usan acá **con su misma semántica** —eran veinte hasta que la capacidad **F-25** y su sexta función sumaron los cuatro del movimiento automático—. Los que aparecen en más de un artefacto de esta categoría:

| Término | Cómo lo usa esta categoría |
| --- | --- |
| Fachada | El objeto que toda esta documentación describe. «Contrato de fachada» cuando se nombra el conjunto de funciones más sus garantías |
| Componente anfitrión | El destinatario de toda acción sugerida del catálogo de condiciones |
| Elemento de dibujo | Lo que el anfitrión entrega a `inicializar` y **no vuelve a tocar** por su cuenta |
| Instancia del visor · Identificador de instancia | Unidad del ciclo de vida que el onboarding recorre entera |
| Resultado de dibujo | Lo que el anfitrión tiene que leer completo, incluidas las piezas no dibujadas |
| Estructura del texto · Árbol · Índice de pieza · Selección | Material con el que el anfitrión sincroniza su árbol con la escena |
| Tipo dibujable · Malla · Escena · Disposición | Vocabulario del dibujo, usado en las entradas del catálogo y en las verificaciones de los tramos |
| Código de condición | Fuente única del catálogo de `DX-Error-Messages.md`. Los siete están declarados en `Definicion-Contrato-De-Fachada.md` §6 y esta categoría **no agrega ninguno**. La sexta función tampoco: `INSTANCIA_DESCONOCIDA` pasa a presentarse en **cinco** funciones y sigue siendo un solo código |
| Movimiento automático · Órbita de la cámara · Giro de las figuras | Los dos movimientos independientes de la capacidad **F-25** y su superordinado, declarados en `Glosario-Funcional.md` §2 y desarrollados en `Definicion-Contrato-De-Fachada.md` §5.5. Esta categoría los usa para decir **qué gobierna el anfitrión invocando la fachada** y qué no toca por su cuenta; no los redefine y no los renombra |
| Estado efectivo del movimiento | Lo que `establecerMovimiento` devuelve: el estado en que quedan **los dos** movimientos después de la operación. Es lo que el anfitrión lee para sincronizar su control visible con lo que la escena está haciendo, en lugar de suponerlo |
| Cero red · Cero persistencia | Dos de las **seis propiedades transversales**, verificadas en el quick-start y en cada tramo del onboarding. Su membresía y su umbral se declaran una sola vez en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, y esta categoría no los re-enumera |
| Página integradora | La superficie del sample S-1 sobre la que se ejecuta todo el onboarding |
| Texto del trabajo | Lo que se pega a mano en el área de texto de la página integradora |
| Capacidad gráfica tridimensional | Prerrequisito del onboarding y causa de la entrada E-VIS-01 del catálogo |

**Frontera de vocabulario del movimiento automático, para que estos cuatro términos no se usen mal.** Los cuatro nombran **lo que la escena hace**, y ninguno nombra un control ni una preferencia. El **control visible**, la **consulta de la preferencia de movimiento reducido** del sistema y la **conservación de la elección** son del componente anfitrión: si el archivo de guion consultara la preferencia violaría G-3 —leer configuración propia— y si guardara la elección violaría G-2 —persistir—. La fachada sólo **recibe el estado deseado y lo aplica**, y devuelve el estado efectivo. Una frase de esta categoría que le atribuya a la fachada un control, una preferencia o una memoria de la elección es un defecto, no un matiz.

### 4.2 Del glosario raíz del producto

Puntero único: [`../00-Contexto/Vision-Producto.md`](../../../00-Contexto/Vision-Producto.md) §9.

| Término | Cómo lo usa esta categoría |
| --- | --- |
| Trabajo | Lo que el alumno entrega en el laboratorio. **No es una «unidad de entrega»**: ese término normativo designa a las piezas desplegables del producto |
| Pieza, referente del dominio | Cada figura del conjunto raíz del trabajo. Forma desnuda. Ver §3.3 |
| Pieza en su segundo referente | Siempre calificado. No aparece en los artefactos de esta categoría |
| Observación, advertencia, error de validación | Se nombran **sólo** para declarar que este proyecto de código no emite ninguna de las tres, y para que no se las confunda con los códigos de condición de la fachada, que son otra cosa y no llevan esos nombres |
| Fallo silencioso | Lo que la enumeración de piezas no dibujadas elimina, y el motivo por el que ninguna condición puede quedar sin entrada de catálogo |
| Componente, figura plana de una pieza | De donde se leen las dimensiones. **No confundir con «componente anfitrión»** |
| Punto de control | Momento en que se registran las métricas DX del recorrido de integración |

Rige además el **choque de vocabulario** de `Vision-Producto.md` §9.3 y `PRODUCT-INTAKE` §12.1: «proyecto de código» designa exclusivamente una unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

### 4.3 De otros documentos del producto

| Término | Dónde está declarado | Cómo lo usa esta categoría |
| --- | --- | --- |
| Bundle | `PRODUCT-INTAKE` §17.7 y §14 | Nombre con el que el intake designa al artefacto de este proyecto de código. En la prosa de esta categoría se usa **archivo de guion**, que es la forma que fijó 02; «bundle» se conserva únicamente dentro de los dos nombres de rol de §2.1, que son los que el encargo de la categoría acuñó |
| Entorno de desarrollo contenido | `Compatibilidad-Plataformas.md` §2.3 | Único lugar donde se ejecuta cualquier paso de esta categoría. El host de desarrollo no tiene ni va a tener las herramientas |
| Artefacto generado | `PRODUCT-INTAKE` §17.7 P.7 | El archivo de guion se genera y **nunca se edita a mano** |
| Punto de extensión | `PRODUCT-INTAKE` §18 | El contrato de la fachada. Es lo que la documentación de esta categoría existe para sostener |
| Sample S-1 | `PRODUCT-INTAKE` §16.1 y §18 | La página integradora sin backend sobre la que corre el onboarding. Su materialización es de 10-Examples |
| Escenario E-1, escenario E-7 | `PRODUCT-INTAKE` §20 | Material de dibujo del quick-start y de los tramos: E-7 cubre los seis tipos dibujables, E-1 trae las tres piezas con el ortoedro incluido |
| `RA-01`, `RA-02`, `RA-03` | `PRODUCT-INTAKE` §14 | Reglas de arquitectura de nivel producto. `RA-02` es la que esta documentación no puede contradecir ni de pasada |

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
