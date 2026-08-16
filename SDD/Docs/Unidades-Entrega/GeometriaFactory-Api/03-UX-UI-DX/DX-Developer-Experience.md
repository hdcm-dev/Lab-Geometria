# Experiencia de desarrollo — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** DX-Developer-Experience.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las ocho secciones son comunes a las cuatro capas.** La experiencia de desarrollo de la unidad es la unión de las cuatro, y ninguna sustituye a otra: quien toca el dominio y quien toca el host **hacen cosas distintas con las mismas herramientas**.

---

## 1. Rol de intervención developer

### 1.1 `GeometriaFactory-Api`

### 1.1 Quién interviene acá

No hay integradores externos y no los va a haber: el intake declara que **no hay clientes de terceros** y que por eso no hay versionado de rutas. Pero este proyecto de código tiene, a diferencia de las tres capas que ensambla, **un consumidor real que no es él mismo**: la pieza pública, que lo alcanza por HTTP y que se compila contra el mismo ensamblado de contratos.

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Implementador de la superficie | La persona que sostiene el producto, o el agente de IA que construye por etapas, agregando o cambiando un punto de acceso | **Qué puntos existen**, qué papel exige cada uno, qué códigos de respuesta declara y **qué guardia tiene que atravesar** |
| **Consumidor de la superficie** | Quien escribe el cliente tipado de la pieza pública. Es la misma persona, con otro sombrero, y **es el único consumidor legítimo** | Qué recibe ante cada fallo, cómo distingue un listado vacío de un servicio caído, y **qué respuestas nunca le van a decir nada más de lo que dicen** |
| Mantenedor de la capa | La misma persona, semanas después, sin el contexto de la etapa en que lo escribió | Por qué un código de respuesta es el que es, dónde va un punto nuevo y **qué se rompe agregándolo mal** |
| **Operador del despliegue** | El docente, que **despliega a mano** el contenedor del servicio | Qué significa un arranque que no atiende, qué revisar del lado del despliegue, y **por qué el mensaje no le dice la ruta** |

**El consumidor de la superficie es lo que hace distinta a esta sección.** En las capas de adentro ese papel se declara no aplicable, porque nadie las invoca por su superficie. Acá hay alguien del otro lado de un salto de red, y **todo lo que reciba es lo único que va a tener**: no puede leer un motivo interno, no puede inspeccionar el almacén y no puede preguntar de nuevo con más detalle.

Nivel de experiencia esperado: quien ya escribe servicios HTTP, pero **no** necesariamente conoce las tres reglas del producto que se rompen desde acá sin que nada falle. Esa parte no se supone conocida: se enseña en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.

### 1.2 Qué es la superficie pública de este proyecto de código

> **Este proyecto de código no tiene otra superficie que su superficie HTTP.** No lo referencia nadie por compilación —es el nivel 3, el último del orden topológico— y no expone ningún tipo propio: los tipos son del ensamblado de contratos. **Lo único que existe de él hacia afuera son sus quince puntos de acceso.**

Cinco consecuencias operativas, que gobiernan todo lo demás:

1. **Lo que no está en la superficie, no existe para nadie.** Una capacidad implementada en las tres capas de adentro y no expuesta acá es una capacidad que el producto no tiene. El mapa completo está en [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, y **es la primera lectura de esta sección**.
2. **Catorce de las quince rutas son propuesta derivada.** Las únicas cosas que una fuente declara son el punto de canje de credenciales, con su ruta, y la existencia de un punto de salud, cuya ruta **la fuente no da**. Leer la tabla sin haber leído §2 de aquel documento es el error de lectura más probable de todo este proyecto de código.
3. **Acá se traduce dos veces, y traducir es decidir.** De motivo interno a código del contrato, y de código del contrato a código de respuesta. La segunda traducción es la que puede romper una regla hacia afuera sin que nada falle.
4. **Acá está la única puerta.** Un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio. Todo lo demás del backend está detrás.
5. **Acá se aplica RA-03 en el único lugar donde se puede violar hacia afuera.** Es la última vez que un dato del backend se toca antes de salir del servidor propio.

**Tres ausencias que no son olvidos y que se reponen por comodidad**: no hay CORS, no hay WebSockets y no hay ningún punto pensado para que lo invoque un navegador. Las tres salen de RA-01, y reponerlas reabre las tres propiedades de la topología del producto: contenido mixto, CORS y exposición de la dirección del servidor propio.

### 1.3 La frontera entre lo que se decide y lo que se transporta

**Enunciado en una línea: esta capa decide cómo se dice, y no decide qué se dice.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Qué punto de acceso existe, con qué verbo y con qué código de respuesta | **Sí** | — |
| Verificar el acceso firmado y exigir el papel que el punto declara | **Sí.** El mecanismo de verificación es de la capa que toca el mundo; **exigirlo en cada punto es de acá** | — |
| Que **ningún punto** quede fuera de la guardia del cambio de contraseña pendiente | **Sí.** La comprobación es de la capa de aplicación | — |
| Elegir el código de respuesta de cada código del contrato | **Sí** | — |
| Conectar cada puerto con su adaptador y tomar la configuración del despliegue | **Sí** | — |
| Decidir si una cuenta admite el acceso, la pertenencia de un trabajo o la facultad sobre el dato | **No.** Llegan resueltas | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Llega decidido y viaja en una respuesta **exitosa** | `GeometriaFactory-Domain` |
| Interpretar el texto del alumno o verificar sus valores | **No.** El texto viaja como cadena y **no se normaliza en el borde** | `GeometriaFactory-Infrastructure` |
| Declarar qué campos cruzan la frontera y qué códigos existen | **No.** **Esta capa no agrega ningún código al conjunto cerrado** | `GeometriaFactory-Contracts` |
| Presentar el estado degradado a una persona | **No** | `GeometriaFactory-Web` |

Tres precisiones que la tabla no alcanza a decir sola:

1. **Exigir el papel no es autorizar**, y duplicar la autorización acá sería peor que no hacerla: crearía un segundo lugar donde la regla puede decir otra cosa. Lo que la guardia aporta es cortar temprano **lo que ningún dato podría autorizar**.
2. **RA-02 no tiene tramo acá, y se declara.** Esta capa no compone el bundle del visor, no lo sirve y no lo configura. Su contribución es negativa y estructural: **al no existir ningún punto pensado para el navegador, no hay nada que el bundle pudiera llamar aunque quisiera**. No tener tramo no es incumplirla.
3. **Sin estado.** Ningún punto depende de lo que ocurrió en la petición anterior. Lo que se parece a una sesión vive en el circuito de la pieza pública, del lado de su servidor, y **el acceso firmado nunca llega al navegador**.

### 1.4 Las dos cosas que sólo se rompen acá

De las **dieciséis** reglas de negocio del producto, **dos se pueden romper desde esta capa hacia afuera sin que ninguna capa de adentro se entere**, porque las de adentro habrían hecho su parte bien.

| Regla | Qué se rompe si acá se hace mal | Dónde se verifica |
| --- | --- | --- |
| **RN-00003** — el trabajo ajeno es indistinguible del inexistente | Responder «no autorizado» donde la regla exige «no encontrado» **confirma la existencia de un recurso ajeno**, y permite averiguar por tanteo qué identificadores existen. Nada falla: la capa de aplicación devolvió el motivo correcto y esta capa lo tradujo mal | `CU-00006` CA-07, `CU-00007` CA-07 y CA-08, `CU-00009` CA-03 |
| **RN-00013** — con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | **Agregar un punto de acceso y olvidarse de la guardia** la incumple sin que nada falle: el punto funciona, responde bien y deja operar a una cuenta que no debería. El defecto no está en lo que el punto hace, está en lo que no atraviesa | `CU-00002` CA-01 y CA-05 |

**Las dos se rompen produciendo algo válido**, y ése es el patrón. Por eso sus criterios de aceptación **comparan respuestas** y **cuentan puntos**, en lugar de esperar que algo falle.

Y una tercera, que no es una regla de negocio sino de arquitectura, y que tiene el mismo patrón: **RA-03**. Un mensaje que incluya la ruta del almacén o la dirección de un servicio interno no rompe nada visible; simplemente le entrega a quien mire la respuesta algo que no debería tener.

### 1.2 `GeometriaFactory-Domain`

### 1.1 Quién interviene acá

No hay integradores externos. `GeometriaFactory-Domain` no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain); sus únicos consumidores son otros dos proyectos de código del mismo producto, que lo referencian por referencia de proyecto de código y no cruzan ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Domain).

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Mantenedor | La persona que sostiene el producto y que vuelve sobre este proyecto de código semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** (`equipo_n` = 1) | Dónde poner una regla nueva, por qué un rechazo existe, y qué se prueba sin nada |
| Integrador de capa | La misma persona, o el agente, escribiendo `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure` contra esta superficie | Qué contrato de uso invoca, qué tiene que haber resuelto **antes** de invocar, y qué código de condición recibe cuando no lo resolvió |
| Operador | **No aplica.** Este proyecto de código no atiende peticiones, no abre conexiones, no registra ni instrumenta (§17.1.P.10 · GeometriaFactory-Domain). No hay nada que operar | — |

Nivel de experiencia esperado: quien ya escribe código de aplicación y conoce el vocabulario del laboratorio, pero **no** necesariamente el estilo de modelo de dominio con invariantes explícitas. La documentación no supone ese estilo conocido: lo explica en §1.2 y lo apoya en `Definicion-Modelo-De-Dominio.md` §4.

Herramientas que ya conoce: el entorno de desarrollo contenido del propio repositorio y los scripts de `scripts/` (`PRODUCT-INTAKE` §16). No se supone ninguna otra.

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, porque es la razón por la que este proyecto de código existe y por la que no tiene dependencias:

> **La superficie pública de un modelo de dominio son sus guardas.** Lo que un consumidor invoca acá no es una API de servicio: es la construcción y la transición de entidades que **se niegan a entrar en un estado prohibido**.

Tres consecuencias operativas, que gobiernan todo lo demás:

1. **El resultado de una invocación no es un dato, es una entidad que ya verificó sus invariantes.** Si el dominio devolvió el alumno constituido, es porque el correo, el nombre y el apellido estaban presentes, la unicidad venía declarada como verificada, no se aportó credencial y el estado inicial es cuenta `Pendiente`, que es el del **auto-registro** (CU-02001 §4). El otro camino de alta, la configuración del administrador, tiene el suyo y es `Habilitado` (CU-02012). No hay que volver a comprobar nada de eso aguas abajo.
2. **El dominio no resuelve nada por su cuenta.** No consulta, no reintenta, no lee el reloj, no interpreta el texto del alumno, no deriva contraseñas y no emite acceso. Cuando una condición se afirma sobre un conjunto de entidades —la unicidad del correo, INV-01— el dominio **la exige declarada** y quien la ejerce es la capa de aplicación con su puerto de repositorio (`Definicion-Modelo-De-Dominio.md` §4.1 y §7).
3. **Un rechazo es una terminación controlada, no una avería.** El dominio no construye la entidad, o la deja exactamente como estaba, y devuelve la causa; no queda estado intermedio porque no guarda nada. El catálogo completo de esas causas es [`DX-Error-Messages.md`](DX-Error-Messages.md).

Nueve invariantes vigentes y **dieciséis** reglas de negocio son las dos caras de esto, y la relación entre ambos es lo que le dice al mantenedor dónde poner una regla nueva. Está desarrollada en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7, con su procedimiento de decisión en §7.3, sobre la correspondencia que declara `Definicion-Modelo-De-Dominio.md` §4.3.

### 1.3 La frontera de autenticación

Es sutil y conviene dejarla imposible de confundir, porque un error acá se paga en dos capas a la vez.

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| El estado de cuenta `Pendiente`, `Habilitado`, `Bloqueado` y sus transiciones admitidas | Sí (CU-02002) | — |
| La **condición** de que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso (INV-06, RN-02006) | Sí (CU-02004) | — |
| La respuesta de admisibilidad con sus **tres** motivos: cuenta `Pendiente`, cuenta bloqueada y **cambio de contraseña pendiente**. El cuarto, credencial no establecida, quedó retirado por **RN-02016** | Sí (CU-02004 §6) | — |
| La exigencia de que la credencial derivada se fije **en el acto de habilitación**, con la provisoria que el sistema produce, y de que el reemplazo declare verificada la vigente | Sí (CU-02002, CU-02003) | — |
| La exigencia de que la cuenta del administrador nazca `Habilitado` y **con su credencial ya aportada**, porque es la que habilita a las demás y ninguna anterior podría habilitarla a ella | Sí (CU-02012) | — |
| La exigencia de que **ninguna de las cuatro operaciones** de ciclo de vida —habilitar, bloquear, rehabilitar y dar de baja— proceda sobre la cuenta del administrador: las cuatro están declaradas sobre cuentas de alumno (F-03) | Sí (CU-02002) | — |
| Comparar una contraseña, derivarla, emitir o validar un acceso, sostener una sesión | **No** | `GeometriaFactory-Infrastructure` (§17.1.P.5 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Infrastructure) y `GeometriaFactory-Api` |
| Autorizar por papel el acceso a un endpoint | **No.** La evaluación de admisibilidad se resuelve por estado y por credencial, nunca por papel (CU-02004 FA-02) | La capa que expone los endpoints |

Enunciado en una línea, que es como conviene recordarlo: **el dominio no implementa autenticación, pero sí modela las reglas que la condicionan.** La contraseña llega ya derivada y el dominio no la conoce nunca en claro (§17.1.P.5 · GeometriaFactory-Domain).

Y una advertencia que la frontera hace fácil de subestimar: estas reglas **no protegen sólo el acceso**. Si la cuenta del administrador queda bloqueada o dada de baja, nadie aprueba ni rechaza, todo trabajo enviado se queda en estado `Pendiente` para siempre y **el circuito de revisión entero se detiene** (RN-02010). Por eso las guardas de CU-02002 y de CU-02012 sobre esa cuenta son de dominio y no de la capa que emite el acceso.

Quien busque acá el mecanismo no lo va a encontrar, y quien lo implemente afuera ignorando la regla va a construir un camino de acceso que INV-06 no cubre. Las dos son la misma equivocación leída desde dos lados.

### 1.3 `GeometriaFactory-Application`

### 1.1 Quién interviene acá

No hay integradores externos. `GeometriaFactory-Application` no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Application); sus dos consumidores son proyectos de código del mismo producto y no cruzan ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Application). Y son **dos consumidores de naturaleza distinta**, que es el rasgo que ordena toda esta sección:

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Mantenedor de la capa | La persona que sostiene el producto y vuelve sobre este proyecto de código semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** (`equipo_n` = 1) | Dónde va un caso de uso nuevo, qué puerto le corresponde declarar, y por qué una negativa existe |
| Integrador por casos de uso | La misma persona, o el agente, escribiendo `GeometriaFactory-Api` contra los casos de uso de esta capa | Qué contrato de uso invoca, qué tiene que haber resuelto **antes** de invocar, qué motivo recibe cuando no lo resolvió y **cómo se traduce ese motivo hacia afuera del proceso** |
| Implementador de puertos | La misma persona, o el agente, escribiendo `GeometriaFactory-Infrastructure` contra los puertos de esta capa | Qué le pide cada puerto, qué garantías tiene que sostener y qué **no** puede devolver sin romper un caso de uso |
| Operador | **No aplica.** Este proyecto de código no atiende peticiones, no abre conexiones, no registra ni instrumenta. Sus únicos NFR son el tiempo del caso de uso más pesado y la exclusión de los componentes en las consultas de listado (§17.1.P.10 · GeometriaFactory-Application) | — |

Nivel de experiencia esperado: quien ya escribe código de aplicación, pero **no** necesariamente conoce el estilo de casos de uso con inversión de dependencias. La documentación no lo supone conocido: lo explica en §1.2 y lo enseña paso a paso en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.

Herramientas que ya conoce: el entorno de desarrollo contenido del propio repositorio y los scripts de `scripts/` (`PRODUCT-INTAKE` §16). No se supone ninguna otra.

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, porque es la razón por la que esta capa existe y por la que se puede probar entera sin base de datos:

> **La superficie pública de esta capa tiene dos caras que miran para lados opuestos.** Una son los **casos de uso**, que un consumidor invoca. La otra son los **puertos**, que esta capa **declara** y otra capa implementa. La dependencia se invierte: acá se dice qué hace falta, y afuera se dice con qué.

Quien no entienda eso va a intentar consultar datos desde acá, y esa es la equivocación más frecuente contra esta capa. Cuatro consecuencias operativas, que gobiernan todo lo demás:

1. **Un puerto no es un cliente.** Esta capa no abre conexiones, no arma consultas y no elige motor. Declara «recuperar un trabajo», «resolver una consulta ya acotada por dueño o por alcance», «interpretar este texto y devolverme cuántas figuras trae el conjunto raíz, las piezas y las observaciones», «dame el sello» (`Especificacion-Funcional.md` §3). El cómo vive en `GeometriaFactory-Infrastructure`, detrás del contrato. Se renunció a consultar la base con proyecciones ad-hoc desde el caso de uso, y lo que se compró con esa renuncia es poder probar el caso de uso entero con dobles (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Application).
2. **El recorte se traslada al puerto, no se aplica después.** El alumno pide sus trabajos y el pedido ya sale acotado al dueño; el administrador pide los de la comisión y el pedido ya sale con el predicado de alcance aplicado (CU-04006 §10, CU-04007 §10). Pedir todo y descartar en memoria da el mismo resultado visible y es exactamente el patrón que la separación entre alumnos viene a impedir.
3. **Esta capa orquesta y decide quién puede, pero no declara reglas.** Las **dieciséis** reglas del producto viven en `GeometriaFactory-Domain`, **las dieciséis con archivo propio allá**, y acá se **ejercen** sobre el pedido concreto (`Especificacion-Funcional.md` §6). Un caso de uso que enunciara una regla nueva estaría mal ubicado; el procedimiento de decisión está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.3.
4. **Una negativa es una terminación controlada, no una avería.** El caso de uso no escribe nada, o deja todo exactamente como estaba, y devuelve un motivo de una enumeración cerrada. **El motivo no es un código de protocolo**: su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2). El catálogo completo de esas condiciones es [`DX-Error-Messages.md`](DX-Error-Messages.md).

El alcance transaccional se declara una sola vez y vale para los once contratos: **un caso de uso, una unidad de trabajo** (`Especificacion-Funcional.md` §3). Ninguna operación reparte sus escrituras entre varias, y por eso ninguna condición de error deja efecto parcial.

**Dos cosas que viajan por los puertos y que conviene reconocer antes de escribir nada**, porque las dos son fuente de equivocaciones caras:

- **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa**, no atributos del dominio. El modelo del dominio declara la fecha de alta del alumno —que recibe del consumidor, sin leer el reloj— y la «Fecha» que el alumno declara en su trabajo, y nada más. La discrepancia está elevada al Product Owner y declarada como punto abierto (`Especificacion-Funcional.md` §3 y §11). Que el reloj sea un puerto es lo que hace verificable en prueba cada uno de esos sellos.
- **La cantidad de figuras del conjunto raíz la produce el validador y la hace viajar CU-04005.** Entra por el puerto de validación junto con las piezas y las observaciones, y llega hasta el dominio, que la exige como precondición. **No es derivable de las piezas adoptadas**, porque ésas admiten huecos: la posición de una figura que no se pudo reconstruir queda reservada. Sin ese dato el dominio no tiene rango contra el cual validar la posición de una observación, y el mecanismo entero de RN-04009 deja de ser comprobable. CU-04005 es el único orquestador de la reconstrucción y del registro de observaciones, de modo que es el único que puede aportarlo.

**Un puerto no lleva identificador declarado aguas arriba y conviene saberlo antes de buscarlo.** El intake nombra tres —`IWorkRepository`, `IFigureValidator` e `ISystemClock` (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application)— y no nombra el **puerto de repositorio de cuentas**, que la orquestación de las cuentas y la verificación de unicidad del correo necesitan. No es una regla nueva ni una decisión de alcance: es un nombre, está declarado como punto abierto en `Especificacion-Funcional.md` §11 y esta sección **no lo reabre**. Acá se lo nombra en lenguaje de dominio, y su identificador se difiere a `05-Arquitectura-Tecnica` y al punto de control de la etapa `a`.

### 1.3 La frontera entre autorizar y autenticar

Es la frontera que hace que `tiene_auth` valga true en este proyecto de código, y conviene dejarla imposible de confundir porque un error acá se paga en dos capas a la vez. El proyecto de código hermano declaró la suya —lo que el dominio modela y lo que no implementa— y ésta es la de esta capa, con la misma forma.

**Enunciado en una línea, que es como conviene recordarlo: esta capa no autentica, autoriza.** Quién es la persona llega ya resuelto desde afuera; lo que se decide acá es qué puede hacer esa persona sobre este recurso concreto.

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Verificar que el trabajo pedido sea del alumno solicitante, sobre el dato recuperado y antes de escribir | **Sí** (CU-04004, CU-04005, CU-04006, CU-04009) | — |
| Verificar que quien pide una operación reservada tenga el papel `Administrador` | **Sí** (CU-04002, CU-04007, CU-04008) | — |
| Acotar lo que el administrador ve y opera, excluyendo los trabajos en `Borrador` | **Sí** (CU-04007, CU-04008, CU-04009) | — |
| Consultar si una cuenta admite el ingreso, y devolver el motivo cuando no lo admite | **Sí** (CU-04003 §4 y §6) | — |
| Exigir que el reemplazo de la credencial derivada declare verificada la vigente | **Sí** (CU-04003 FA-04) | — |
| Exigir que la configuración del administrador aporte credencial derivada, y que el auto-registro no la aporte | **Sí** (CU-04010 §6, CU-04001 §6). Son los dos caminos de alta, con reglas opuestas | — |
| Comparar una contraseña, derivarla, comparar la credencial vigente | **No.** El valor llega **ya derivado** y el valor en claro nunca atraviesa esta capa (CU-04003 §10, CU-04010 §3) | `GeometriaFactory-Infrastructure` (`PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Application, §17.1.P.5 · GeometriaFactory-Infrastructure) |
| Emitir o validar un acceso, sostener una sesión | **No.** Acá se resuelve si la cuenta lo admite y por qué; quién lo emite y con qué mecanismo es de las capas externas (CU-04003 §10) | `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api` |
| Autenticar la petición y establecer quién la firma | **No.** La identidad del solicitante llega **declarada** por el consumidor, ya autenticada (CU-04004 §3, CU-04006 §3) | `GeometriaFactory-Api` |
| Traducir un motivo a respuesta de protocolo | **No.** El motivo es un valor de una enumeración cerrada, no un código de protocolo | `GeometriaFactory-Api` |

Dos precisiones que la tabla no alcanza a decir sola:

1. **Que el consumidor haya autenticado a la persona no alcanza.** El papel no dice de quién es el trabajo, y por eso la pertenencia se verifica igual y sobre el dato recuperado, no sobre lo que declara la petición (CU-04004 §10).
2. **La verificación no se resuelve ocultando un control en la pantalla.** Un alumno que fuerce la petición contra el servicio de datos tiene que ser rechazado igual, y eso es exactamente lo que esta capa hace verificable con dobles (CU-04008 §10, `Especificacion-Funcional.md` §4 punto 3).

Quien busque acá el mecanismo no lo va a encontrar; quien lo implemente afuera creyendo que la autorización viaja con él va a construir un camino que la pertenencia no cubre. Las dos son la misma equivocación leída desde dos lados.

### 1.4 Las cuatro negativas, y la que nunca se traduce a «no autorizado»

Las **cuatro** comprobaciones transversales de `Especificacion-Funcional.md` §4 producen cuatro negativas distintas, y **confundir las dos primeras es el error más caro que un consumidor puede cometer contra esta capa**: revelar que un recurso ajeno existe habilita el tanteo de identificadores.

| Negativa | Qué se preguntó | Motivo | Qué oculta | Traducción obligatoria del consumidor |
| --- | --- | --- | --- | --- |
| **Por pertenencia** | ¿Este trabajo es del alumno que lo pide? | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | **La existencia del recurso.** El trabajo ajeno y el identificador inexistente comparten motivo por diseño | «No encontrado», y **nunca** «no autorizado» |
| **Por facultad** | ¿Quien pide esta operación reservada tiene el papel `Administrador`? | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | **Nada.** No hay recurso ajeno cuya existencia proteger: se preguntó por una facultad, no por un recurso | Puede ser explícita: «requiere la facultad de administrador» |
| **Por alcance** | ¿Este trabajo entra en lo que el administrador ve? | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | **Nada.** Tampoco oculta la existencia: expresa que el trabajo está fuera de su flujo de trabajo | Puede ser explícita: «los borradores no forman parte de la revisión» |
| **Por cambio de contraseña pendiente** | ¿La cuenta solicitante fue reseteada y todavía no cambió la provisoria? | `CAMBIO_DE_CONTRASENA_PENDIENTE` | **Nada**, y además **corta antes que las otras tres**: no lee ni escribe nada (INV-09). Su única excepción declarada es el reemplazo de `CU-04003` FA-05, que es lo que la levanta | Debe ser explícita y **debe derivar al cambio de contraseña**: la cuenta se autentica y **no obtiene sesión de trabajo** (RN-04013) |

La regla mnemotécnica, que es la que hay que poder recitar sin abrir el documento: **el papel no reemplaza a la pertenencia, y la pertenencia no se confiesa.** Un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso (`Especificacion-Funcional.md` §4 punto 1).

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos motivos distintos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador— y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una (`Especificacion-Funcional.md` §4).

El tratamiento completo —con el procedimiento de decisión, la tabla de traducciones prohibidas y las pruebas que lo sostienen— está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4. Y lo que el dominio rechaza sin que acá llegue a ocurrir —por construcción, por equivalencia o por agregación deliberada— está reunido en su §2.5, que es la sección que evita que la ausencia de un motivo del dominio se lea como olvido.

### 1.4 `GeometriaFactory-Infrastructure`

### 1.1 Quién interviene acá

No hay integradores externos. `GeometriaFactory-Infrastructure` no se publica en ningún feed, se compila dentro de la solución de código del producto y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Pero hay un tipo de interviniente que las capas de adentro no tienen, y es el que ordena buena parte de esta sección:

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Implementador de adaptadores | La persona que sostiene el producto, o el agente de IA que construye por etapas, escribiendo la implementación de un puerto que `GeometriaFactory-Application` declaró | Qué le pide el puerto, **qué garantías tiene que sostener** y **qué no puede devolver** sin romper un caso de uso de la capa de adentro |
| Mantenedor de la capa | La misma persona, semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** | Dónde va un adaptador nuevo, por qué una condición existe, y **cuál de los atajos tentadores está prohibido y por qué** |
| **Operador del despliegue** | El docente, que despliega **a mano** el contenedor de la pieza de datos | Qué significa cada terminación degradada y cada arranque detenido, y **qué revisar del lado del despliegue**: el volumen, la ruta, la clave de firma, el linaje de transformaciones |
| Integrador por casos de uso | **No aplica acá.** Nadie invoca esta capa por su superficie: la composición de raíz la conecta y los casos de uso la usan **a través de los puertos**, sin conocerla | — |

**El operador es lo que hace distinta a esta sección.** En el proyecto de código hermano ese papel se declara «no aplica»: aquella capa no atiende peticiones, no abre conexiones y no registra. Acá **seis de las diecisiete condiciones de error se diagnostican mirando el despliegue y no el código**, y por eso el operador tiene fila propia y no una nota al pie. Las seis, nombradas para que el recuento se pueda comprobar: `ALMACEN_NO_DISPONIBLE`, `RUTA_DEL_ALMACEN_NO_DISPONIBLE`, `MIGRACION_NO_APLICABLE`, `CLAVE_DE_FIRMA_AUSENTE`, `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y `CREDENCIAL_DERIVADA_ILEGIBLE`.

Nivel de experiencia esperado: quien ya escribe código de acceso a datos, pero **no** necesariamente conoce el dato real que este producto tiene que leer. Esa parte no se supone conocida: se enseña en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7, y su fuente es [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md).

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, y lo que decide si un cambio acá es correcto:

> **Esta capa no tiene superficie propia: tiene la forma de los contratos que otra capa declaró.** Los cuatro puertos son de `GeometriaFactory-Application`; acá se los implementa. Lo único propio son **dos mecanismos** —credenciales y acceso firmado— y **una responsabilidad de arranque**. La dependencia se invirtió arriba, y acá se paga la factura.

Cinco consecuencias operativas, que gobiernan todo lo demás:

1. **Acá vive el mecanismo y no la decisión.** Un adaptador que decidiera un estado, una autorización o una transición estaría mal ubicado. La tabla completa está en §1.3.
2. **Acá está el riesgo declarado del producto.** El intake registra, con probabilidad alta y con impacto alto, que **el validador se escribe sin leer el análisis** y rechaza el dato real de los alumnos: *«la aplicación no sirve para el dato que existe»*. Es el único riesgo de negocio del producto cuya mitigación es una batería de pruebas, y esa batería es de esta capa. **Antes de escribir una línea de lectura de texto hay que leer el documento de concepto central**, y no es una recomendación de estilo.
3. **Acá está la única persistencia del producto.** Es el único `library` de los siete con persistencia declarada —el flag vale true acá y en `GeometriaFactory-Api`, que delega en éste—, y por eso el único con un modelo de datos documentado. **El modelo del dominio manda**: acá se materializa, no se decide.
4. **Acá viven los tres secretos.** La contraseña en claro —que existe **sólo** en este proyecto de código—, el valor derivado de la credencial y la clave de firma. Ninguno entra a un mensaje, a una traza ni al repositorio de código.
5. **Acá se depende del mundo.** Un archivo que puede no estar montado, una fuente de aleatoriedad que puede no responder, un esquema que puede no corresponder. **Cinco de las diecisiete condiciones existen por eso**, y todas terminan de forma degradada o deteniendo el arranque: **ninguna finge un resultado**.

**Dos garantías que esta capa tiene que sostener y que su contrato no puede expresar solo**, porque se rompen produciendo algo válido:

- **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las que no se pudieron reconstruir, y **no es derivable de las piezas adoptadas**. Si el adaptador la calculara contando piezas, el número sería siempre creíble y siempre estaría mal cuando hubiera un hueco, y el mecanismo entero de la observación ubicada dejaría de ser comprobable.
- **La posición de una figura no reconstruida queda reservada**, y el almacén no compacta. Compactar tampoco falla: produce mensajes que apuntan a la figura equivocada.

### 1.3 La frontera entre el mecanismo y la decisión

Es la frontera que hace que el flag de autenticación valga true en este proyecto de código, y **conviene notar que vale true por un motivo distinto del de las dos capas de adentro**: allá vale porque se modela la regla o se ejerce la autorización; acá vale porque **está el mecanismo**.

**Enunciado en una línea: esta capa provee el mecanismo y no toma ninguna decisión de negocio.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Derivar una contraseña y verificar una credencial | **Sí** (CU-06006). Es el último punto del recorrido de la contraseña en claro: acá se convierte en el valor guardado y acá se compara | — |
| **Producir la contraseña provisoria** del reseteo | **Sí** (CU-06007). Es una **delegación explícita** de las tres capas de arriba | — |
| Emitir y verificar el acceso firmado | **Sí** (CU-06008) | — |
| Leer el texto real del alumno y emitir observaciones ubicadas | **Sí** (CU-06001, CU-06002) | — |
| Guardar, recuperar y retirar | **Sí** (CU-06003, CU-06004, CU-06005) | — |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto: una cuenta no admitida **no llega** a la emisión | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar pertenencia o facultad | **No.** El recorte de una consulta **llega en el pedido** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Se entrega el conjunto de observaciones y **el dominio resuelve** | `GeometriaFactory-Domain` |
| Comparar el correo escrito como confirmación de una baja | **No.** Llega resuelto | `GeometriaFactory-Application` |
| Traducir un código a respuesta de protocolo | **No** | `GeometriaFactory-Api` |
| Decidir dónde vive el archivo del almacén y cuándo arranca el servicio | **No.** La ruta llega de configuración | `GeometriaFactory-Api` y `09-Devops` |

Tres precisiones que la tabla no alcanza a decir sola:

1. **El traslado del recorte no es una comprobación de autorización.** Que una consulta llegue acotada por dueño o por alcance es una decisión ya tomada afuera. Lo único que esta capa hace por su cuenta es **negarse a resolver una consulta que llega sin recorte**, y lo hace por integridad del pedido: no sabe quién preguntó.
2. **Las restricciones de unicidad del almacén sí son una segunda línea, y eso es deliberado.** El código de correo ocupado se llama igual acá y en la capa de aplicación, y no es casualidad: `GeometriaFactory-Application` `CU-06001` **FA-02** ya declara ese camino como flujo alternativo propio, con el mismo motivo. **La verificación previa no es una garantía por sí sola.**
3. **La marca de cambio de contraseña pendiente se conserva acá y se comprueba afuera.** Esta capa la escribe, la conserva sobre cualquiera de los tres estados de cuenta y la hace viajar; **la comprobación transversal que confina a la cuenta es de la capa de aplicación**. Sin el dato, esa comprobación no tendría sobre qué decidir; con el dato, esta capa no decide nada.

### 1.4 Las tres cosas que sólo se rompen acá

De las **dieciséis** reglas de negocio del producto, **tres tienen su tramo principal en esta capa**, y es la única de la que eso se puede decir. La consecuencia práctica es directa: **si acá se hacen mal, ninguna capa de más adentro puede repararlas**.

| Regla | Qué se rompe si acá se hace mal | Dónde se verifica |
| --- | --- | --- |
| **RN-06008** — el texto original se conserva íntegro | Normalizar el texto al guardarlo no falla: el alumno vuelve a abrir su trabajo y ve un texto que no escribió, las comas finales desaparecen y el escenario que documenta la tolerancia deja de ser reproducible desde el almacén | CU-06001 CA-09, CU-06003 CA-01 y CA-02 |
| **RN-06009** — toda observación de error indica posición y campo | Compactar las posiciones no falla: produce mensajes que apuntan a la figura equivocada, y el alumno busca su defecto donde no está | CU-06001 CA-04, CU-06003 CA-08 |
| **RN-06014** — la provisoria la produce el sistema, no es adivinable y no se repite | Componerla con un contador o con la fecha no falla: el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** | CU-06007 CA-01 a CA-05 |

**Las tres se rompen produciendo algo válido**, y ése es el patrón. Por eso las tres tienen criterios de aceptación que comparan, cuentan o inspeccionan, en lugar de esperar que algo falle.

**RN-06014 merece una nota aparte porque es una delegación con nombre.** `GeometriaFactory-Application` §6 declara que es **la única de las dieciséis sin tramo en su capa**, porque el valor le llega ya producido y ya derivado; `GeometriaFactory-Contracts` `CU-06008` §10 exige las dos propiedades del valor devuelto y declara explícitamente que **el contrato no declara mecanismo**; y la propia regla, en `GeometriaFactory-Domain`, nombra a este proyecto de código como el lugar de la generación. **Tres documentos apuntan acá. Acá no hay a quién apuntar.**

## 2. Onboarding por tramos

### 2.1 `GeometriaFactory-Api`

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y el servicio arranca sobre un almacén vacío | `./scripts/build.sh` termina en 0 y sin advertencias, `./scripts/test.sh` pasa entero y el punto de salud responde |
| 30 minutos | **Sabe qué existe hacia afuera.** Dado un pedido cualquiera del producto, nombra el punto de acceso que lo atiende, el papel que exige y **si atraviesa la guardia o no** | Reproduce, sin abrirla, la partición de la tabla de puntos de acceso: **cuatro sin acceso firmado y once bajo la guardia. Cuatro más once son quince**, y ninguno queda con su forma de identificación abierta |
| 1 hora | **Corre la colección entera y entiende por qué los ocho escenarios responden con éxito.** Explica por qué un envío que no verifica **no es un fallo de protocolo**, y qué pasaría si lo fuera | La colección ejecutada, con sus **8** envíos, **8** respuestas de éxito y **2** trabajos en `Borrador` |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

**El tramo de 1 hora es el que más rinde de esta capa**, y su objetivo no es casual: la confusión que evita —creer que un texto que no verifica es un fallo de la petición— es la que convertiría el mayor valor didáctico del producto en un mensaje de error.

### 2.2 `GeometriaFactory-Domain`

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y la batería de dominio queda en verde | `./scripts/build.sh` termina en 0 y sin advertencias, y `./scripts/test.sh` pasa entero. La batería de dominio completa en menos de 10 segundos (§17.1.P.10 · GeometriaFactory-Domain) |
| 30 minutos | Sabe leer una guarda: elige un rechazo del catálogo, ubica el caso de uso que lo declara y la regla o el invariante que lo sostiene, y encuentra la prueba que lo ejercita | Escribe, sin abrir el intake, la tríada código → CU → RN o INV de tres rechazos cualesquiera, y la contrasta con `DX-Error-Messages.md` §6 |
| 1 hora | Sabe dónde poner una regla nueva: distingue una condición permanente sobre el estado, que es un invariante y va como guarda de la entidad, de un comportamiento o de un alcance de consulta, que no lo es y va en otra capa | Clasifica RN-02007, RN-02008, RN-02009 y RN-02011 como reglas sin invariante asociado y justifica por qué, coincidiendo con `Definicion-Modelo-De-Dominio.md` §4.3 |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

### 2.3 `GeometriaFactory-Application`

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y la batería de la capa de aplicación queda en verde **sin base de datos** | `./scripts/build.sh` termina en 0 y sin advertencias, `./scripts/test.sh` pasa entero, y `dotnet test tests/GeometriaFactory.Application.Tests` queda en verde. Ninguna prueba de esta capa toca la base de datos real: es la puerta de calidad propia y bloqueante de §17.1.P.8 · GeometriaFactory-Application |
| 30 minutos | Sabe distinguir las tres negativas **de autorización** (`DX-Error-Messages.md` §2.4): dado un motivo del catálogo, dice si oculta la existencia del recurso o no, y cómo se traduce hacia afuera | Clasifica `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` sin abrir el intake, y coincide con [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4 |
| 1 hora | Entiende la inversión: nombra los cuatro puertos que esta capa declara, dice qué le pide a cada uno y ejercita un caso de uso entero con dobles, sin base de datos ni frontera de proceso | Recorre el criterio de aceptación CA-01 de CU-04005 con un validador doble y un repositorio simulado, explica por qué el reloj es un puerto y por qué la cantidad de figuras del conjunto raíz no se puede derivar de las piezas adoptadas. La tabla de puertos que tiene que reproducir es la de `Especificacion-Funcional.md` §3 |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

### 2.4 `GeometriaFactory-Infrastructure`

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido | `./scripts/build.sh` termina en 0 y sin advertencias y `./scripts/test.sh` pasa entero |
| 30 minutos | **Sabe qué tiene de raro el dato real del alumno.** Dado un texto, nombra las cuatro trampas del formato y dice qué hace un lector ingenuo con cada una | Recorre [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) §2 y responde, sobre el texto del escenario **E-2**, por qué un lector estricto lo rechaza entero y por qué el ortoedro no se dibuja hoy |
| 1 hora | **Corre la batería obligatoria y entiende qué prueba cada caso.** Nombra los nueve casos de prueba del producto, dice qué escenario ejercita a cada uno y por qué el criterio negativo de **E-4** es más difícil de acertar que el positivo de **E-3** | La tabla de cobertura de §7 del documento de concepto central, reproducida sin abrirlo, y la batería del validador en verde |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

**El tramo de 30 minutos es el que más rinde de todo el producto**, y su objetivo no es casual: es exactamente el conocimiento cuya ausencia el intake declara como el defecto que más veces se repite.

## 3. Quick-start

### 3.1 `GeometriaFactory-Api`

Objetivo del quick-start: **el primer resultado exitoso**, que acá es **la colección de peticiones corriendo entera contra el servicio real**. Es el resultado que mejor explica la capa: no hay pantalla, no hay circuito y no hay visor.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas. Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio de código en el entorno de desarrollo contenido, que el
#    propio repositorio define en `.devcontainer/`. Todo lo demás corre adentro.

# 1. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh

# 2. Guion de ejecución del servicio.
#    Criterio de éxito: arranca, aplica las transformaciones y el punto de salud responde.
./scripts/run-api.sh

# 3. Ejecutar la colección de peticiones contra el servicio.
#    Criterio de éxito: los 8 envíos responden con éxito, 6 trabajos en estado
#    `Pendiente` y 2 en `Borrador`.
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, guion de reinicio del almacén, guion de ejecución del servicio, colección de peticiones— y conservan su forma literal porque el lector los tiene que poder ejecutar. **Las rutas y los nombres de guion salen del intake §16 y §18: no se eligen acá.**

**Tres pasos, sobre el máximo de cinco** que el intake exige a las muestras del producto.

Lo que el quick-start deliberadamente **no** incluye: publicar la imagen, alcanzar la red desde afuera, configurar un dominio. Ninguna hace falta para el primer resultado, y si un paso futuro las pidiera, **el paso está mal ubicado**.

### 3.2 Verificación del quick-start

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los guiones y las rutas salen del intake y **no se inventan acá**.

### 3.2 `GeometriaFactory-Domain`

Objetivo del quick-start: **el primer resultado exitoso**, que acá es la batería de dominio en verde. Es el resultado más barato de obtener del producto entero, y es deliberado: este proyecto de código **se prueba sin nada** —sin base de datos, sin red y sin dobles (§17.1.P.6 · GeometriaFactory-Domain)—, que es exactamente lo que justifica su cobertura mínima más alta del producto.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas (`PRODUCT-INTAKE` Parte C, decisiones comunes; `Alcance-Producto.md` §4.4). Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio en el entorno de desarrollo contenido, que el propio
#    repositorio define en `.devcontainer/`. Todos los pasos siguientes corren adentro.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh

# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y completa en menos de 10 segundos.
dotnet test tests/GeometriaFactory.Domain.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de script salen de `PRODUCT-INTAKE` §16 y el proyecto de prueba, de §17.1.P.6 · GeometriaFactory-Domain: no se eligen acá.

Después del paso 3 ya hubo primer resultado exitoso. El primer resultado **con sentido de dominio** llega al observar una guarda negándose, y está en `Guia-Onboarding-Developer.md` §3.

Lo que el quick-start deliberadamente **no** incluye, porque este proyecto de código no lo tiene: levantar una base de datos, aplicar una transformación de esquema, arrancar un servicio, configurar una credencial de acceso o pedir un dato de red. Si algún paso futuro los pide, el paso está mal ubicado.

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la etapa `a`, que es la que crea el andamiaje de la solución de código y ancla las versiones (§17.1.P.7 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain). El compromiso de verificación es el siguiente, y es lo que impide que este documento quede describiendo un quick-start que dejó de correr:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los scripts y las rutas salen de `PRODUCT-INTAKE` §16 y no se inventan acá.

### 3.3 `GeometriaFactory-Application`

Objetivo del quick-start: **el primer resultado exitoso**, que acá es la batería de la capa de aplicación en verde **sin base de datos, sin red y sin servicio levantado**. Es el resultado que mejor explica la capa: si hiciera falta preparar algo externo para correrla, la inversión de dependencias no estaría hecha.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas (`PRODUCT-INTAKE` Parte C, decisiones comunes; `Alcance-Producto.md` §4.4). Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio en el entorno de desarrollo contenido, que el propio
#    repositorio define en `.devcontainer/`. Todos los pasos siguientes corren adentro.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh

# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y sin haber preparado
#    ninguna base de datos, ningún servicio y ninguna credencial de acceso.
dotnet test tests/GeometriaFactory.Application.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de script salen de `PRODUCT-INTAKE` §16 y el proyecto de prueba, de §17.1.P.6 · GeometriaFactory-Application: no se eligen acá.

Después del paso 3 ya hubo primer resultado exitoso. El primer resultado **con sentido de aplicación** llega al ver un caso de uso entero resuelto con dobles, y está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §3.

Lo que el quick-start deliberadamente **no** incluye, porque esta capa no lo tiene: levantar una base de datos, aplicar una transformación de esquema, arrancar un servicio, configurar una credencial de acceso o pedir un dato de red. Si algún paso futuro los pide, **el paso está mal ubicado y probablemente la prueba también**: la integración vive en `GeometriaFactory.Integration.Tests`, que pertenece a la Api (§17.1.P.6 · GeometriaFactory-Application).

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la etapa `a`, que es la que crea el andamiaje de la solución de código y ancla las versiones. El compromiso de verificación es el siguiente, y es lo que impide que este documento quede describiendo un quick-start que dejó de correr:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los scripts y las rutas salen de `PRODUCT-INTAKE` §16 y §17.1.P.6 · GeometriaFactory-Application, y no se inventan acá.

### 3.4 `GeometriaFactory-Infrastructure`

Objetivo del quick-start: **el primer resultado exitoso**, que acá es **la batería del validador en verde sobre los textos reales de los escenarios**. Es el resultado que mejor explica la capa: el validador se prueba **sin almacén**, porque recibe texto y devuelve observaciones.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas. Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio de código en el entorno de desarrollo contenido, que el
#    propio repositorio define en `.devcontainer/`. Todo lo demás corre adentro.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero, incluidas las diez pruebas del validador.
./scripts/test.sh

# 3. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, guion de reinicio del almacén— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de guion salen del intake §16: **no se eligen acá**.

**El paso 3 es propio de este proyecto de código** y no existe en el quick-start de las capas de adentro. Es el camino de vuelta declarado del producto y lo que permite repetir cualquier prueba de persistencia desde un estado conocido.

Lo que el quick-start deliberadamente **no** incluye: arrancar la pieza de datos, configurar una clave de firma o alcanzar la red. Ninguna de las tres hace falta para el primer resultado, y si un paso futuro las pidiera, **el paso está mal ubicado**.

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la primera etapa, que es la que crea el andamiaje de la solución de código y ancla las versiones. El compromiso de verificación es el siguiente:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los guiones y las rutas salen del intake §16 y §17.1.P.6 · GeometriaFactory-Infrastructure, y no se inventan acá.

## 4. Diátaxis

### 4.1 `GeometriaFactory-Api`

Los cuatro modos existen, y **tres de ellos ya viven en artefactos de la cadena**: este documento no los duplica, los ubica y los enlaza.

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca toqué esta superficie; llevame de la mano una hora» |
| How-to | Tarea | Los doce casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) | «Tengo que agregar un punto / traducir un motivo / arrancar el servicio: qué tengo que sostener» |
| Reference | Información | [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, §4 y §6; [`DX-Error-Messages.md`](DX-Error-Messages.md) para el catálogo; los dos glosarios | «Qué punto atiende esto» / «qué código de respuesta le corresponde a este código del contrato» |
| Explanation | Comprensión | §1.2, §1.3 y §1.4 de este documento; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §1, §2, §5 y §7; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué un envío que no verifica responde con éxito» / «por qué quince rutas están sin decidir» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

Regla de mantenimiento: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza.

### 4.2 `GeometriaFactory-Domain`

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza. Duplicarlos sería fabricar una segunda fuente de verdad sobre reglas que 02 ya declaró.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca trabajé contra este dominio; llevame de la mano una hora» |
| How-to | Tarea | Los trece casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`, cada uno con sus precondiciones, su flujo principal y sus flujos alternativos. En la etapa que corresponda, los ejemplos de uso que produzca `11-Documentacion` | «Tengo que constituir un alumno / enviar un trabajo / aplicar un desenlace: qué tengo que haber resuelto antes» |
| Reference | Información | `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` para entidades, atributos, cardinalidades y transiciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y `02-Especificacion-Funcional/Glosario-Funcional.md` para el vocabulario | «Qué atributos tiene una observación» / «qué significa `ENVIO_SIN_INTERPRETACION`» |
| Explanation | Comprensión | §1.2 y §1.3 de este documento; `Definicion-Modelo-De-Dominio.md` §4, §6 y §7; `Guia-Onboarding-Developer.md` §7 | «Por qué el dominio no verifica la unicidad del correo si es un invariante suyo» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes (`Guia-Onboarding-Developer.md` §5).
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla y el invariante que lo restringen, que son su explanation.
- El glosario de esta sección referencia el glosario funcional de 02 y el glosario raíz de 00 en lugar de redefinir términos.

Regla de mantenimiento, que evita el anti-patrón de documentación mezclada: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza. La regla vale también para el agente de IA que construye por etapas.

### 4.3 `GeometriaFactory-Application`

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza. Duplicarlos sería fabricar una segunda fuente de verdad sobre contratos que 02 ya declaró.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca trabajé contra esta capa; llevame de la mano una hora» |
| How-to | Tarea | Los once casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), cada uno con sus precondiciones, su flujo principal y sus flujos alternativos. En la etapa que corresponda, los ejemplos de uso que produzca `11-Documentacion` | «Tengo que dar de alta una cuenta / enviar un trabajo / aplicar un desenlace: qué tengo que haber resuelto antes» |
| Reference | Información | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 para los puertos y §4 para las cuatro comprobaciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) para el vocabulario | «Qué le pide el caso de uso al puerto de validación de figuras» / «qué significa `OBSERVACION_MAL_FORMADA`» |
| Explanation | Comprensión | §1.2, §1.3 y §1.4 de este documento; `Especificacion-Funcional.md` §1, §4 y §8; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué el reloj es un puerto» / «por qué la negativa por pertenencia no se distingue de la inexistencia» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes ([`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §5).
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla de negocio y el caso de uso de dominio que orquesta, que son su explanation.
- El glosario de esta sección referencia el glosario funcional de 02 y el glosario raíz de 00 en lugar de redefinir términos.

Regla de mantenimiento, que evita el anti-patrón de documentación mezclada: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza. La regla vale también para el agente de IA que construye por etapas.

### 4.4 `GeometriaFactory-Infrastructure`

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca escribí un adaptador de este producto; llevame de la mano una hora» |
| How-to | Tarea | Los diez casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) | «Tengo que implementar la lectura del texto / el retiro / la emisión del acceso: qué garantías tengo que sostener» |
| Reference | Información | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4; [`../02-Especificacion-Funcional/Modelo-Datos/`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) para el dato guardado; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones; los dos glosarios | «Qué guarda la pieza» / «qué significa `MIGRACION_NO_APLICABLE`» |
| Explanation | Comprensión | **[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md), que es el explanation más importante del producto**; §1.2, §1.3 y §1.4 de este documento; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué el texto del alumno no es JSON válido y por qué eso no se corrige» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes.
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla de negocio, la regla conceptual de modelo y el puerto que implementa, que son su explanation.
- Cada regla conceptual de modelo declara en su §6 la prueba que la verifica.
- Los glosarios de esta sección y de la anterior referencian el glosario raíz en lugar de redefinir términos.

Regla de mantenimiento: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza.

## 5. Mensajes de error y diagnóstico

### 5.1 `GeometriaFactory-Api`

Principio de redacción, aplicado sin excepción a las **18** entradas del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. Acá la tercera parte tiene dos destinatarios distintos, y esa es la particularidad de esta capa:

> **Casi todo lo que responde esta superficie lo lee un programa, no una persona.** El consumidor es el código de la pieza pública, y lo que necesita no es un texto: es **saber qué hacer**, que es siempre una de cuatro cosas —corregir y reintentar, derivar a otra pantalla, mostrar lo que pasó, o pasar a estado degradado—. Las únicas dos entradas cuyo destinatario es una persona directamente son las del arranque detenido, y esa persona es **el operador que despliega a mano**.

Cinco precisiones que el catálogo hace cumplir:

1. **Dos resultados que parecen fallos son el funcionamiento normal del producto**, y ninguno tiene entrada en el catálogo: **el texto que no verifica** y **el listado vacío**. Los dos viajan en respuestas exitosas, y [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 los reúne.
2. **La confusión más cara de esta capa es una sola**, y conviene poder recitarla: si un envío cuyo texto no verifica respondiera con un código de fallo, el producto le diría a la persona que su petición estaba mal **cuando su trabajo se guardó y sus errores están localizados por figura y por campo**. Vería un fallo y no vería lo único que le sirve.
3. **Tres familias de respuestas están deliberadamente empobrecidas**, y no es un defecto: la respuesta genérica de credenciales inválidas, la del recurso que no se ve y la del correo ya registrado. Las tres dicen **menos** de lo que saben, y las tres tienen la misma razón: no confirmar la existencia de algo que el solicitante no debería saber que existe.
4. **Ningún mensaje incluye la ruta del almacén, la clave de firma, una contraseña, la provisoria, el texto del alumno ni la dirección de un servicio interno.** Es RA-03, y su contracara obligatoria es que **todo error respondido queda registrado del lado del servidor**, junto con **todo intento de acceso rechazado**.
5. **Esta capa no reintenta.** Devuelve el código y quien decida reintentar es la pieza pública, que es la que sabe qué estaba haciendo la persona.

### 5.2 `GeometriaFactory-Domain`

Principio de redacción, aplicado sin excepción a las **42** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene una forma particular:

> El diagnóstico accionable de una condición de error del dominio dice siempre **qué hacer del lado del consumidor**, porque el dominio no resuelve nada por su cuenta: no consulta, no reintenta y no corrige el dato.

Cuatro precisiones que el catálogo hace cumplir:

1. **El dominio emite un código, no un texto.** No produce mensajes para personas, no los traduce y no los formatea: no conoce ningún formato de serialización (§17.1.P.1 · GeometriaFactory-Domain) ni cruza ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Domain). El enunciado en lenguaje plano del catálogo es la base que la capa que expone usa para componer su mensaje, y la traducción a respuesta de protocolo pertenece a `GeometriaFactory-Api` (CU-02001 §6, CU-02004 §6).
2. **Ningún código es genérico.** Un rechazo dice qué guarda se negó, no «operación inválida». Es la misma exigencia que RN-02009 le impone al producto frente al alumno, aplicada acá frente al consumidor.
3. **Un código no filtra lo que la regla oculta.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia y el consumidor lo traduce a «no encontrado», nunca a «no autorizado» (RN-02003, INV-02, CU-02009 §6).
4. **Una condición de error no es una observación.** La distinción es la que sostiene todo el modelo y está desarrollada en [`Glosario-UX.md`](Glosario-UX.md) §3.1.

El catálogo completo, con su taxonomía y su cobertura por caso de uso, es [`DX-Error-Messages.md`](DX-Error-Messages.md).

### 5.3 `GeometriaFactory-Application`

Principio de redacción, aplicado sin excepción a las **36** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene dos destinatarios en vez de uno:

> El diagnóstico accionable de una condición de esta capa dice **qué hacer del lado del consumidor** cuando la negativa nace de lo que el consumidor pidió, y **qué corregir del lado del adaptador del puerto** cuando nace de lo que un puerto devolvió.

Cinco precisiones que el catálogo hace cumplir:

1. **Esta capa emite un motivo, no un texto.** Es un valor de una enumeración cerrada, no un código de protocolo: la traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2). El enunciado en lenguaje plano del catálogo es la base con la que la capa que expone compone lo que una persona lee.
2. **Ningún motivo es genérico.** Una negativa dice qué comprobación se negó, no «operación inválida». Es la misma exigencia que RN-04009 le impone al producto frente al alumno, aplicada acá frente al consumidor.
3. **Un motivo no filtra lo que la regla oculta.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia y el consumidor lo traduce a «no encontrado», nunca a «no autorizado» (RN-04003, CU-04006 §6). Lo mismo vale para la cuenta inexistente en la consulta de admisibilidad, que no distingue el motivo hacia afuera para no revelar qué correos están registrados (CU-04003 §6 y §10).
4. **Una condición de error no es una observación.** Un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de este catálogo**: es el resultado declarado del envío (CU-04005 FA-01). La distinción está desarrollada en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 y en [`Glosario-UX.md`](Glosario-UX.md) §3.1.
5. **El comentario del administrador tampoco es una observación**, y no aparece en ningún lugar de este catálogo: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala (CU-04008 §10).
6. **Un mismo motivo puede tener causas opuestas cuando los caminos son opuestos.** `ESTADO_INICIAL_NO_NEGOCIABLE` rechaza en el auto-registro un estado distinto de `Pendiente` y en la configuración del administrador uno distinto de `Habilitado`. No es una inconsistencia: el enunciado es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino. Es la única condición del catálogo con fila completa en dos subsecciones ([`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4).

El catálogo completo, con su taxonomía, su tratamiento de las tres negativas **de autorización** y su verificación de cobertura, es [`DX-Error-Messages.md`](DX-Error-Messages.md).

### 5.4 `GeometriaFactory-Infrastructure`

Principio de redacción, aplicado sin excepción a las **17** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene un destinatario que las capas de adentro no tienen:

> En esta capa, **la mitad de las condiciones no las provoca nadie que haya invocado mal**: las provoca el mundo. El diagnóstico dice entonces **qué revisar del lado del despliegue**, no qué corregir del lado del código.

Cinco precisiones que el catálogo hace cumplir:

1. **La mayoría de lo que parece un fallo acá es el funcionamiento normal del producto.** Un error de validación, un texto ilegible, cero advertencias, nada encontrado, un conjunto vacío, una credencial que no coincide y un acceso vencido **son resultados**. [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 los reúne, y ninguno tiene entrada en el catálogo.
2. **La confusión más cara del producto es una sola**, y conviene poder recitarla: si un texto ilegible devolviera `INTERPRETACION_NO_DISPONIBLE` en lugar de una observación, el alumno vería «el servicio no está disponible» cuando lo que pasa es que su programa emitió algo que no se puede leer, y **se quedaría esperando a que el sistema se recupere de un problema que no tiene**.
3. **Ningún mensaje incluye la clave de firma, una contraseña, una provisoria, la ruta del almacén ni el texto del alumno.** Es RA-03, regla de nivel producto, y su contracara obligatoria es que **todo error que se muestre queda registrado del lado del servidor**: sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar. La tabla está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4.
4. **Hay tres atajos prohibidos, y los tres son tentadores porque no fallan.** Componer la provisoria por otro medio cuando la fuente de aleatoriedad no responde; generar una clave de firma al vuelo; y caer hacia una ruta alternativa cuando el volumen no está montado. Los tres dejan el sistema funcionando y equivocado. Están en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4, con un cuarto de la misma familia: descartar el almacén ante un esquema divergente.
5. **Esta capa no reintenta.** Devuelve el estado degradado y quien decida reintentar es el consumidor.

## 6. Métricas DX

### 6.1 `GeometriaFactory-Api`

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: no hay developers externos a quienes encuestar y el equipo es de una persona más un agente de IA.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio de código hasta la colección corriendo entera | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio |
| TTFV | Tiempo hasta el primer valor: haber corrido la colección y saber por qué los ocho escenarios responden con éxito | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 3 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de respuestas | Códigos del conjunto cerrado del contrato con destino declarado, más las respuestas sin código | **16 de 16**, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §6 |
| **Puntos de acceso bajo la guardia** | Puntos que exigen acceso firmado y atraviesan la guardia de admisión | **11 de 11, sin tolerancia.** Un punto nuevo entra a la cuenta el mismo día que se agrega | Recuento de la tabla de puntos contra la lista de puntos guardados, en cada punto de control |
| **Códigos inventados** | Códigos del contrato que esta capa produce y que no pertenecen al conjunto cerrado del ensamblado | **0, sin tolerancia** | Recuento de los códigos que la superficie emite contra el conjunto cerrado |
| **Secretos, rutas y textos filtrados** | Respuestas o trazas que contengan la clave de firma, una contraseña, la provisoria, la ruta del almacén, el texto del alumno o la dirección de un servicio interno | **0, sin tolerancia** | Inspección de las respuestas de error y del registro del servidor en cada punto de control |
| **Respuestas que distinguen lo ajeno de lo inexistente** | Pares de respuestas —recurso ajeno contra recurso inexistente— que difieran en algo | **0, sin tolerancia.** Es RN-00003 medida directamente | Comparación byte a byte de los pares de CA-03 de `CU-00009` |
| **Textos de prueba inventados** | Cuerpos de la colección que no salgan de los escenarios declarados | **0, sin tolerancia.** Es una regla de delivery del producto | Comparación de los cuerpos contra el intake §20, en cada punto de control |

Las tres primeras son las métricas DX canónicas. **Las seis últimas son propias de este proyecto de código**, y cinco de ellas tienen tolerancia cero porque miden exactamente las cosas que se rompen produciendo algo válido.

### 6.2 `GeometriaFactory-Domain`

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: este proyecto de código no registra ni instrumenta (§17.1.P.10 · GeometriaFactory-Domain), el producto no tiene canal de correo (`Alcance-Producto.md` §5, exclusión X-1) y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio hasta la batería de dominio en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio, en el punto de control de la etapa |
| TTFV | Tiempo hasta el primer valor: haber visto una guarda negándose y saber ubicar su regla o su invariante | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora, resuelto sin abrir el intake |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones de error declaradas en los trece casos de uso que tienen entrada en el catálogo | 42 de 42, sin inventados | Recuento contra `DX-Error-Messages.md` §6, verificable por lectura de la §6 de cada caso de uso |
| Tiempo de diagnóstico de un rechazo | Tiempo desde ver un código de condición hasta ubicar el caso de uso, la regla y la acción esperada | <= 2 minutos | Cronometrado sobre tres códigos elegidos al azar del catálogo |

Las tres primeras son las métricas DX canónicas. Las dos últimas son propias de este proyecto de código y existen porque acá el catálogo de errores **es** la superficie pública: una condición sin entrada en el catálogo es superficie no documentada.

### 6.3 `GeometriaFactory-Application`

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: esta capa no registra ni instrumenta (§17.1.P.10 · GeometriaFactory-Application), el producto no tiene canal de correo y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio hasta la batería de la capa de aplicación en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio, en el punto de control de la etapa |
| TTFV | Tiempo hasta el primer valor: haber ejercitado un caso de uso entero con dobles y saber nombrar los cuatro puertos | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora, resuelto sin abrir el intake |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones declaradas en la §6 de los once casos de uso que tienen entrada en el catálogo | 36 de 36, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §7, verificable por lectura de la §6 de cada caso de uso |
| Tiempo de diagnóstico de una negativa | Tiempo desde ver un motivo hasta ubicar el caso de uso, la comprobación que se negó y la acción esperada | <= 2 minutos | Cronometrado sobre tres motivos elegidos al azar del catálogo |
| **Traducciones prohibidas** | Cantidad de lugares del consumidor donde una negativa por pertenencia se traduce a «no autorizado», o donde un motivo revela la existencia de un recurso ajeno | **0, sin tolerancia** | Revisión de la traducción de motivos en `GeometriaFactory-Api` en cada punto de control, contra la tabla de [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4 |
| Pruebas de esta capa que tocan la base de datos | Cantidad de pruebas de `tests/GeometriaFactory.Application.Tests` que necesitan preparar algo externo | 0. Es la puerta de calidad propia y bloqueante de §17.1.P.8 · GeometriaFactory-Application | Verificación en el punto de control: una prueba que necesita preparar algo está mal ubicada y pertenece a integración |

Las tres primeras son las métricas DX canónicas. Las cuatro últimas son propias de este proyecto de código: dos porque acá el catálogo de motivos **es** la mitad de la superficie pública, y dos porque la inversión de dependencias sólo se sostiene si se mide.

### 6.4 `GeometriaFactory-Infrastructure`

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: el producto no tiene canal de correo y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio de código hasta la batería del validador en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio |
| TTFV | Tiempo hasta el primer valor: haber corrido la batería obligatoria y saber qué prueba cada caso | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones declaradas en la §6 de los diez casos de uso que tienen entrada en el catálogo | **17 de 17**, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §7 |
| **Cobertura de la batería obligatoria** | Casos de prueba del producto con criterio de aceptación en esta categoría | **10 de 10**, con los escenarios del intake como fixtures. Son los nueve de la batería obligatoria del producto más el décimo que §21 agrega para la dimensión no legible | La tabla de §7 del documento de concepto central, contra los criterios de CU-06001 y CU-06002 |
| **Textos de prueba inventados** | Cantidad de fixtures de validación que no salen de los escenarios declarados | **0, sin tolerancia.** Es una regla de delivery del producto: no se inventan textos de prueba | Revisión de los fixtures en cada punto de control |
| **Secretos y rutas filtrados** | Lugares donde un mensaje, una traza o el repositorio de código contienen la clave de firma, una contraseña, una provisoria o la ruta del almacén | **0, sin tolerancia** | Inspección del registro del servidor y del repositorio en cada punto de control, contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4 |
| **Atajos prohibidos** | Cantidad de lugares donde una de las tres condiciones de §2.4 del catálogo se resuelve con su atajo en lugar de detenerse | **0, sin tolerancia** | Revisión de las tres rutas de código en cada punto de control |

Las tres primeras son las métricas DX canónicas. Las cinco últimas son propias de este proyecto de código: dos porque acá está el riesgo declarado del producto, y tres porque acá están los secretos y los atajos que no fallan.

## 7. Feedback loop

### 7.1 `GeometriaFactory-Api`

No hay canal de issues externo ni encuesta a developers de adopción. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control. Un cambio incompatible en el ensamblado de contratos **rompe la compilación de los dos extremos**, que es la señal más temprana posible | Una compilación rota es retroalimentación inmediata, no un accidente de construcción |
| **La colección de peticiones** | Es la demostración ejecutable de la superficie. Cuando una de sus respuestas esperadas deja de darse, la señal no es «una prueba rota»: es **que la superficie cambió sin que nadie lo declarara** | Se corrige antes de fusionar, y si el cambio era deliberado, se declara en la superficie y en la colección a la vez |
| **El consumidor de la superficie** | Quien escribe el cliente tipado de la pieza pública es el primero que descubre que una respuesta no le alcanza para saber qué hacer. **Una respuesta que lo obliga a adivinar es un defecto de esta sección** | Se corrige el catálogo, no el cliente |
| **El despliegue a mano** | El docente es el primero que ve un arranque que no atiende. **Un mensaje que no le alcanza para saber qué revisar es un defecto de esta sección**, no del despliegue | Se corrige el diagnóstico accionable de esa entrada |
| Informe de cierre por etapa | Documento autocontenido por etapa | Lo que costó entender baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

### 7.2 `GeometriaFactory-Domain`

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y los consumidores son proyectos de código del mismo producto. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner (`Vision-Producto.md` §9.1). Es donde se corre la verificación del quick-start de §3.2 y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control (§17.1.P.8 · GeometriaFactory-Domain). La compilación de los consumidores es la señal más temprana posible de un cambio incompatible de la superficie pública (§17.1.P.3 · GeometriaFactory-Application) | Un cambio que rompe la compilación de `GeometriaFactory-Application` es retroalimentación DX inmediata, no un accidente de construcción |
| Informe de cierre por etapa | Documento autocontenido por etapa, que se lee sin abrir el análisis ni el código (`Alcance-Producto.md` §4.3) | Lo que costó entender en la etapa se anota ahí y baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

### 7.3 `GeometriaFactory-Application`

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y los consumidores son proyectos de código del mismo producto. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start de §3.2 y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control (§17.1.P.8 · GeometriaFactory-Application). Un cambio incompatible en un caso de uso o en un puerto rompe la compilación de `GeometriaFactory-Api` o de `GeometriaFactory-Infrastructure`, que es la señal más temprana posible (§17.1.P.3 · GeometriaFactory-Application) | Una compilación rota aguas abajo es retroalimentación DX inmediata, no un accidente de construcción. La §17 de cada caso de uso declara qué cambio es compatible y cuál sube versión mayor |
| La puerta de calidad de la capa | «Ninguna prueba de esta capa toca la base de datos real» (§17.1.P.8 · GeometriaFactory-Application). Cuando una prueba empieza a necesitar preparar algo, es señal de que un caso de uso dejó de pasar por un puerto | Se corrige la ubicación antes de fusionar, y si la señal se repite se revisa el diseño del puerto, no la prueba |
| Informe de cierre por etapa | Documento autocontenido por etapa, que se lee sin abrir el análisis ni el código | Lo que costó entender en la etapa se anota ahí y baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

### 7.4 `GeometriaFactory-Infrastructure`

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y el único consumidor es la composición de raíz de la pieza de datos. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control. Un cambio incompatible en un adaptador rompe la compilación de la composición de raíz | Una compilación rota es retroalimentación inmediata, no un accidente de construcción |
| **La batería obligatoria del validador** | Es la mitigación declarada del riesgo de negocio más alto del producto. Cuando un caso de la batería empieza a fallar, la señal no es «una prueba rota»: es **que el producto dejó de servir para el dato que existe** | Se corrige antes de fusionar, sin excepción. La cobertura mínima del validador es la más alta del producto |
| **La verificación de transformaciones de esquema** | Que se apliquen solas sobre un almacén inexistente es puerta de calidad bloqueante. Cuando deja de pasar, suele ser porque **se editó una transformación ya fusionada** | Se corrige la transformación nueva, no la vieja, y se declara en el control de cambios |
| **El despliegue a mano** | El docente despliega el contenedor a mano, y es quien primero ve un arranque detenido. **Un mensaje que no le alcanza para saber qué revisar es un defecto de esta sección**, no del despliegue | Se corrige el diagnóstico accionable de esa condición en el catálogo |
| Informe de cierre por etapa | Documento autocontenido por etapa | Lo que costó entender baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

## 8. Trazabilidad

### 8.1 `GeometriaFactory-Api`

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de la superficie, **consumidor de la superficie**, mantenedor de la capa y **operador del despliegue**, los cuatro internos al producto (§1.1) |
| Superficie pública que se documenta | Los **quince** puntos de acceso de [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, y las dos traducciones |
| CU origen | CU-00001 a CU-00012 de este proyecto de código |
| Reglas de negocio relevantes | RN-00001 a RN-00016, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6: **trece con tramo acá, tres sin él, y dos que esta capa puede romper hacia afuera sola** —RN-00003 y RN-00013— |
| Necesidades de negocio | NB-00001 a NB-00009, **las nueve**, tres de ellas parcialmente, y **NB-00008 con su primer tramo propio del producto** |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US de la guardia sobre los once puntos, **con el recuento como criterio de aceptación**; US de las dos traducciones; US del arranque detenido; US de la colección reproducible en tres pasos; US del quick-start verificable en el punto de control |
| Tests previstos en 08 | Integración contra el servicio real, con la pirámide invertida que el intake declara a propósito; **una prueba por punto y por condición de la guardia**; **una prueba por código del conjunto cerrado**; y las inspecciones de secretos, rutas y textos inventados |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema aplicada | **Parcialmente pertinente y no aplicable como extensión.** La configuración —ubicación del almacén y clave de firma— **entra por acá**, y lo que esta sección declara sobre ella es qué pasa cuando falta: el servicio no atiende. Su forma es de `05-Arquitectura-Tecnica` y su provisión, de `09-Devops` |
| Primer arranque aplicado | **Pertinente y acotado.** El primer arranque de la instancia existe —el punto de configuración de la cuenta de administrador, que sólo procede mientras no exista ninguna— pero **no es una superficie de aprovisionamiento**: es un punto de acceso más. La superficie de aprovisionamiento que una persona recorre vive en la categoría 03 de la pieza pública |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; lo que declara es el papel que cada punto exige |
| Identidad de versión aplicada | **Pertinente.** Este proyecto de código **sí** produce un artefacto desplegable identificable —la imagen del servicio— y el producto etiqueta cada etapa cerrada para poder volver a cualquier demostración. Qué informa el punto de salud sobre la versión **no está declarado por ninguna fuente** y es de `05-Arquitectura-Tecnica` |
| Modelo UX-UI aplicado en la Fase B2, validación visual y línea de base | N/A. `requiere_maqueta` == false |

### 8.2 `GeometriaFactory-Domain`

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor del dominio e integrador de capa, los dos internos al producto (`00-Contexto/Vision-Producto.md` §2.2, concentración de roles en una persona) |
| Superficie pública que se documenta | Los trece contratos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`: los **dos caminos de alta**, ciclo de vida de la cuenta, credencial derivada, **reseteo de contraseña**, admisibilidad, ciclo de vida del trabajo, reconstrucción de piezas, observaciones, envío, desenlace y los dos contratos de alcance |
| CU origen | CU-02001 a CU-02012 |
| Reglas de negocio relevantes | RN-02001 a RN-02016; invariantes INV-01 a INV-09 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de documentación de la superficie pública, US del quick-start verificable en el punto de control, US del catálogo de condiciones de error como artefacto mantenido junto al código |
| Tests previstos en 08 | Pruebas unitarias puras y sin dobles sobre cada guarda del catálogo; la batería completa en menos de 10 segundos (§17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain) |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. El dominio no tiene superficies de configuración |
| Primer arranque aplicado | N/A. El dominio no se despliega por instancia |
| Acceso de operador único aplicado | N/A. El dominio no dibuja ninguna superficie de acceso; ver §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed (§17.1.P.7 · GeometriaFactory-Domain) |
| Modelo UX-UI aplicado en la Fase B2 | N/A. `requiere_maqueta` == false |
| Validación visual de maqueta | N/A. `requiere_maqueta` == false |
| Línea de base emitida | N/A. `requiere_maqueta` == false |

### 8.3 `GeometriaFactory-Application`

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor de la capa, integrador por casos de uso e implementador de puertos, los tres internos al producto (`00-Contexto/Vision-Producto.md` §2.2, concentración de roles en una persona) |
| Superficie pública que se documenta | Los once contratos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) y los cuatro puertos de `Especificacion-Funcional.md` §3: repositorio de trabajos, repositorio de cuentas, validación de figuras y reloj del sistema |
| CU origen | CU-04001 a CU-04010 de este proyecto de código |
| Reglas de negocio relevantes | RN-04001 a RN-04016, **las dieciséis con archivo en `GeometriaFactory-Domain`**, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00006 (parcial), NB-00007 (parcial), NB-00009. NB-00008 no la toca este proyecto de código, y su motivo está declarado en `Especificacion-Funcional.md` §7.2 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de documentación de los once contratos y de los cuatro puertos; US del quick-start verificable en el punto de control; US del catálogo de condiciones mantenido junto al código; US de la traducción de motivos en el consumidor, con la traducción prohibida como criterio de aceptación |
| Tests previstos en 08 | Unitarias con dobles sobre cada condición del catálogo, **ninguna tocando la base de datos real** (§17.1.P.6 · GeometriaFactory-Application, §17.1.P.8 · GeometriaFactory-Application); el tiempo de resolución de CU-04005 medido sin acceso a base (§17.1.P.10 · GeometriaFactory-Application) |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. Esta capa no tiene superficies de configuración |
| Primer arranque aplicado | N/A. Esta capa no se despliega por instancia. El alta inicial del administrador es un flujo alternativo de CU-04001, no una superficie de aprovisionamiento |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; la frontera está en §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed (§17.1.P.7 · GeometriaFactory-Application) |
| Modelo UX-UI aplicado en la Fase B2 | N/A. `requiere_maqueta` == false |
| Validación visual de maqueta | N/A. `requiere_maqueta` == false |
| Línea de base emitida | N/A. `requiere_maqueta` == false |

### 8.4 `GeometriaFactory-Infrastructure`

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de adaptadores, mantenedor de la capa y **operador del despliegue**, los tres internos al producto. El integrador por casos de uso **no aplica** acá (§1.1) |
| Superficie pública que se documenta | Los diez contratos de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/): los cuatro puertos que implementa, los dos mecanismos que provee y la responsabilidad de arranque |
| CU origen | CU-06001 a CU-06010 de este proyecto de código |
| Reglas de negocio relevantes | RN-06001 a RN-06016, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6: **catorce con tramo acá, dos sin él, y tres con su tramo principal acá** —RN-06008, RN-06009 y RN-06014— |
| Reglas conceptuales de modelo | RC-06001 a RC-06007, en [`../02-Especificacion-Funcional/Modelo-Datos/`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) |
| Necesidades de negocio | NB-00001 a NB-00009, **las nueve**, tres de ellas parcialmente. Es una de las dos secciones del producto que las tocan todas —la otra es `GeometriaFactory-Web`—, y el motivo está en `Especificacion-Funcional.md` §7.2 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de la lectura tolerante con sus cuatro trampas; US de las tres reglas con tramo principal acá, **con el atajo prohibido como criterio de aceptación**; US de las transformaciones de esquema aplicadas al arrancar; US del quick-start verificable en el punto de control |
| Tests previstos en 08 | Las nueve pruebas de la batería obligatoria con los textos de los escenarios como fixtures, **sin almacén**; las de persistencia contra el almacén real; y las inspecciones de secretos, rutas y textos de prueba inventados |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. La configuración —ruta del almacén, clave de firma— **la toma `GeometriaFactory-Api`** y esta capa la recibe ya resuelta |
| Primer arranque aplicado | N/A como extensión. **La preparación del almacén de CU-06010 es un contrato de uso**, no una superficie de aprovisionamiento |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; la frontera está en §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed |
| Modelo UX-UI aplicado en la Fase B2, validación visual y línea de base | N/A. `requiere_maqueta` == false |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
