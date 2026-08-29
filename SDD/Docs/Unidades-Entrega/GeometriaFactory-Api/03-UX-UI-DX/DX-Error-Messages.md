# Catálogo de mensajes de error — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** DX-Error-Messages.md
**Versión:** 2.1
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

**Cinco de las siete secciones son comunes.** Es el grupo con **más contenido único del inventario de la fusión** —853 líneas—, y la consolidación es una **unión de catálogo**: los mensajes de las cuatro capas conviven porque sus códigos ya tenían rango propio. Los **principios de redacción** los declara sólo el host, y valen para los cuatro catálogos.

---

## 1. Principios de redacción

### 1.1 `GeometriaFactory-Api`

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo. La tercera tiene acá una particularidad que las capas de adentro no tienen:

> **Casi todo lo que responde esta superficie lo lee un programa, no una persona.** El consumidor es el código de la pieza pública, y lo que necesita no es un texto sino **saber qué hacer**, que es siempre una de cuatro cosas: **corregir y reintentar**, **derivar a otra pantalla**, **mostrar lo que pasó** o **pasar a estado degradado**. La columna «qué hace el consumidor» del catálogo declara cuál de las cuatro corresponde en cada caso.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe lo que no se pudo hacer, no quién se equivocó.
2. **Nada genérico donde el producto sabe ser específico.** Las tres familias del §1.5 dicen menos **a propósito**; en todas las demás, decir poco es un defecto.
3. **Ninguna respuesta revela lo que RA-03 prohíbe**, y **todas quedan registradas del lado del servidor**, junto con todo intento de acceso rechazado.
4. **Ningún código se inventa.** Los códigos son del conjunto cerrado del ensamblado de contratos; donde falta uno, se usa el genérico y **el hueco se declara** (§2.4).
5. **El código de respuesta y el código del contrato son dos cosas distintas y las dos viajan.** El primero le dice al consumidor qué clase de fallo fue; el segundo, cuál exactamente.

### 1.2 Dos resultados que no son fallos

Es la distinción que sostiene todo lo demás, y la que más se equivoca en esta capa: **lo que otro producto trataría como error, acá viaja en una respuesta exitosa.** Ninguno de los dos tiene entrada en este catálogo.

| Lo que ocurre | Por qué **no** es un fallo | Dónde está declarado |
| --- | --- | --- |
| **El texto enviado no verifica** | El envío **procede**: el trabajo se guardó, con su texto íntegro, con su estado `Borrador` y con sus observaciones **localizadas por índice de figura y campo**. El ensamblado de contratos lo declara señal y no error | `CU-00006` §5 FA-01 y CA-02, CA-03 |
| **El listado no tiene elementos** | Es una comisión sin entregas todavía. El consumidor distingue vacío de fallo **por el tipo recibido y no por el conteo** | `CU-00007` §5 FA-03 y CA-10 |

**La consecuencia más cara de confundir el primero.** Si un envío cuyo texto no verifica respondiera con un código de fallo, el producto le diría a la persona que **su petición estaba mal** cuando lo que pasó es que su programa emitió algo que no se puede interpretar —y su trabajo, mientras tanto, se guardó con el defecto ya localizado—. Vería un fallo y **no vería lo único que le sirve**. Es exactamente el problema que el producto viene a resolver, reintroducido en el último tramo.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite **un código de respuesta y un código del contrato**, más un texto neutro y la ubicación del defecto cuando la hay. **No compone mensajes para personas**: el texto que alguien lee lo arma la pieza pública, y está sujeta a la misma prohibición de §1.4.

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada situación: la base sobre la que la pieza pública compone. No es una cadena que el servicio produzca.

### 1.4 Lo que ninguna respuesta puede decir

Es RA-03, regla de nivel producto, y **acá es el único lugar donde se puede violar hacia afuera**: es la última vez que un dato del backend se toca antes de salir del servidor propio.

| Nunca aparece en una respuesta | Por qué | Qué corresponde |
| --- | --- | --- |
| La **dirección de un servicio interno**, en cualquier forma | Es el enunciado literal de RA-03 | El motivo, sin origen |
| La **ruta del archivo del almacén** | Es una dirección de servicio interno a los efectos de RA-03 | «El servicio no puede atender» |
| La **clave de firma**, ni una parte | No entra al repositorio de código, no entra a la imagen y no entra a una respuesta | «No hay acceso válido» |
| La **contraseña recibida** ni el valor derivado de una credencial | Ninguno de los dos cruza la frontera | La respuesta genérica de credenciales inválidas |
| La **contraseña provisoria**, fuera del cuerpo del resultado del reseteo | Se devuelve **una vez** y **no se registra en ninguna traza** | Nada: el valor viaja en el resultado y en ningún otro lado |
| El **texto original del alumno** dentro de un mensaje o de una traza | El texto es el trabajo de una persona y el registro del servidor no es su lugar | La posición y el campo, que es lo que la regla exige |
| **Trazas de la implementación** o nombres de tipos internos | Es lo que un defecto no previsto expone si no se lo maneja | El código genérico, con su código de respuesta |

**Y la contracara, que es igual de obligatoria:** el intake declara registro estructurado del lado del servidor **de cada error y de cada intento de acceso rechazado**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar, y el docente que despliega a mano se queda sin nada que mirar.

### 1.5 Las tres familias deliberadamente empobrecidas

Tres familias de respuestas dicen **menos de lo que el servicio sabe**, y en las tres eso es la decisión y no el defecto. Se declaran juntas porque el impulso de «mejorarlas» es el mismo en las tres.

| Familia | Qué no dice | Por qué |
| --- | --- | --- |
| **Credenciales inválidas** | No declara si falló el correo o la contraseña | Distinguirlos permitiría averiguar por tanteo **qué correos están registrados**. Lo declara el intake §17.1.P.5 · GeometriaFactory-Api |
| **Recurso que no se ve** | No distingue el inexistente, el ajeno y el que está fuera del alcance del solicitante | Es **RN-00003**. Distinguirlos permitiría averiguar por tanteo **qué identificadores existen** |
| **Correo ya registrado** | No declara la situación ni el papel de la cuenta que ocupa el correo | Misma familia: no confirmar nada sobre una cuenta que el solicitante no debería conocer |

**Una prueba las cubre a las tres, y es la misma en las tres**: comparar dos respuestas que deberían ser indistinguibles y verificar que lo son.

## 2. Taxonomía

### 2.1 `GeometriaFactory-Api`

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Entradas | Código de respuesta |
| --- | --- | --- | --- |
| **Entrada inválida** | Lo que llegó no es utilizable: falta un campo, o el que llegó no cumple lo que el contrato le pide | 3 | `400` |
| **Credencial no admitida** | La credencial presentada no habilita | 2 | `401` |
| **Situación de la cuenta** | La cuenta existe y su situación impide la operación. **Estas dos llevan motivo**, y es lo que permite derivar a la pantalla correcta | 2 | `403` |
| **Facultad** | El papel no habilita la operación | 2 | `403` |
| **Recurso no visible** | Lo pedido no existe, no es del solicitante o está fuera de lo que ve | 2 | `404` |
| **Conflicto de estado** | La operación es legítima y el estado no la admite | 6 | `409` |
| **No clasificado** | Lo que el conjunto cerrado no previó, o lo que el mundo no dejó completar | 1 | `500` o `503` |

**Dieciocho entradas**, y el reparto por código de respuesta cierra: 3 + 2 + 2 + 2 + 2 + 6 + 1 = 18.

**La categoría de conflicto de estado es la más poblada, y es la señal más clara de dónde está esta capa.** Seis de las dieciocho entradas describen operaciones **legítimas, pedidas por quien tiene derecho a pedirlas**, que el estado del sistema no admite. Es lo que le pasa a un producto con una máquina de estados que importa: la mayoría de sus negativas no son de seguridad.

### 2.2 Las dos respuestas sin código del contrato

| Respuesta | Cuándo | Por qué no lleva código |
| --- | --- | --- |
| `401` de la guardia | No hay acceso, el acceso venció, o su firma no corresponde | El conjunto cerrado **no declara ninguno** que describa un acceso ausente o inválido, y **esta capa no inventa códigos**. Lo que el consumidor necesita saber es que tiene que volver a canjear credenciales, y eso lo dice el número |
| `400` de petición ilegible | El cuerpo no se puede leer, o un valor no pertenece a un conjunto cerrado | Ocurre **antes** de que la petición llegue a ser el tipo del contrato: no hay contrato con el que hablar todavía |

**Las dos son deliberadas y se declaran para que su ausencia de código no se lea como un olvido.**

### 2.3 El código sin destino, y por qué

Del conjunto cerrado de **diecisiete** códigos vivos, **uno no tiene código de respuesta asignado y no puede tenerlo**: el que describe que la pieza de datos **no responde**. El ensamblado de contratos lo declara «el único que el contrato admite que produzca la propia pieza pública».

Una respuesta de esta superficie con ese código sería una contradicción en sus términos: **si hubo respuesta, el servicio respondió**. Lo declara `CU-00009` §10 y se repite acá para que una revisión posterior no lo levante como cobertura faltante: **son dieciséis códigos con destino sobre diecisiete, y el hueco es intencional**.

### 2.4 Los dos huecos del conjunto cerrado, **cerrados**

Estaban elevados al Product Owner y **el Product Owner los resolvió** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): el ensamblado de contratos incorporó **un código propio para cada uno**, y `GeometriaFactory-Contracts` los emite formalmente en [`../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../../Producto/Contratos-Inter-Unidad/Contratos-Abstractions.md) §5.1. **Esta categoría no inventó ninguno**, que era la condición con la que los declaró abiertos.

| Hueco | Qué faltaba | Con qué se cerró |
| --- | --- | --- |
| **El papel no alcanza, fuera del desenlace** | El conjunto cerrado tenía **un solo** código de facultad y su enunciado estaba acotado al desenlace de la revisión. La capa de aplicación emite un motivo de facultad requerida también en **el gobierno de cuentas, el reseteo y la revisión de la comisión** | `OPERATION_ADMIN_ONLY`, con respuesta `403` y entrada propia en §3.4. `OUTCOME_ADMIN_ONLY` **no cambia de enunciado** y sigue acotado a aprobar y a rechazar |
| **El trabajo no está en `Borrador`, fuera de la eliminación** | El código análogo del conjunto cerrado estaba acotado por su enunciado **a la eliminación y al camino del alumno**. Un envío o una reedición forzados fuera de `Borrador` no tenían dónde ir | `STATE_FORBIDS_UPDATE`, con respuesta `409` y entrada propia en §3.6. `STATE_FORBIDS_DELETE` **no cambia de enunciado** y sigue acotado a la eliminación |

**El efecto medible del cierre es que el código genérico baja de cuatro destinos a dos**: le quedan `500`, para el defecto no previsto, y `503`, para la terminación degradada. Los dos destinos que se van —`403` y `409`— existían **sólo** porque el conjunto cerrado no tenía código propio para esos caminos. Que un código genérico cubra cuatro situaciones distintas era exactamente lo que el producto evita en todos los demás lugares, y por eso se declaró en vez de naturalizarse.

### 2.2 `GeometriaFactory-Domain`

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | El dato que llega está ausente, vacío, no admitido en esta operación, o contradice a lo que el propio dato declara | 20 |
| **Recurso ausente** | Lo que la operación referencia no existe, o todavía no tiene valor | 2 |
| **Conflicto de estado** | La operación es legítima, pero el estado actual de la cuenta, del trabajo o de la instancia no la admite | 15 |
| **Conflicto de facultad** | La operación es legítima y el estado la admitiría, pero el papel declarado no la ejerce, o el camino por el que se pide no es el suyo | 5 |

Sobre **conflicto de facultad**, que es una categoría agregada a la enumeración de referencia: se declara aparte porque las cinco condiciones que agrupa no se resuelven mirando el dato ni el estado, sino el papel, y confundirlas con un conflicto de estado llevaría a buscar el remedio en una transición que no existe. La distinción es la misma que separa a CU-02009, que responde por el alumno, de CU-02011, que responde por el administrador.

### 2.2 Las dos categorías vacías, con su motivo

Se declaran vacías en lugar de omitirse, para que nadie las complete más adelante con condiciones inventadas.

| Categoría | Condiciones | Motivo |
| --- | --- | --- |
| **Error transitorio** | Ninguna | Un error transitorio supone una operación que puede volver a intentarse y a veces sale bien. Este proyecto de código no atiende peticiones, no abre conexiones y no ejecuta entrada ni salida (`PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Domain): ninguna de sus guardas depende del momento en que se la invoque. **El dominio nunca pide reintentar** |
| **Error interno** | Ninguna | Todo rechazo del dominio es una guarda declarada, con su caso de uso y su regla. Una falla no declarada no sería una condición de este catálogo: sería un defecto del proyecto de código, y su lugar es una prueba que falla, no una entrada acá |

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que el consumidor tiene que hacer:

| Forma | Qué significa | Dónde aparece |
| --- | --- | --- |
| **Rechazo** | El dominio se niega a la operación. No construye la entidad, o la deja exactamente como estaba. No hay efecto parcial ni estado intermedio, porque el dominio no guarda nada | CU-02001, CU-02002, CU-02003, CU-02005, CU-02006, CU-02007, CU-02008, CU-02010, CU-02013 |
| **Motivo de resultado** | La operación es una consulta y **siempre devuelve un resultado**; el código es el motivo por el que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | CU-02004, CU-02009, CU-02011 |

La diferencia importa: ante un rechazo, el consumidor corrige la invocación; ante un motivo de resultado, el consumidor **informa** o **encamina** a la operación que corresponde. `PASSWORD_CHANGE_PENDING` es el ejemplo canónico: no es un fallo, es la situación esperada de toda cuenta de alumno recién habilitada o recién reseteada. Hasta la versión 1.4 el ejemplo canónico era `CREDENTIAL_NOT_SET`, que **RN-02016 retiró** del catálogo junto con el primer ingreso anónimo que lo producía.

### 2.3 `GeometriaFactory-Application`

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | El dato que llega está ausente, vacío, no admitido en este camino, o no pertenece a un conjunto cerrado declarado | 14 |
| **Recurso ausente** | Lo que la operación referencia no existe, no existe **para quien lo pide**, o todavía no tiene valor | 3 |
| **Conflicto de estado** | La operación es legítima, pero el estado actual de la cuenta, del trabajo o del conjunto de cuentas no la admite | 12 |
| **Conflicto de facultad** | La operación es legítima y el estado la admitiría, pero el papel declarado por **quien pide** no la ejerce, o el papel de la **cuenta destino** no admite la operación | 3 |
| **Conflicto de alcance** | La operación es legítima y el papel la ejerce, pero el trabajo pedido está fuera de lo que ese papel ve | 1 |
| **Error transitorio** | Un puerto no pudo completar lo que se le pidió, por una causa que no depende de lo que el consumidor pidió | 1 |
| **Error interno** | Un adaptador de puerto devolvió algo que el contrato no admite. No es un defecto del caso de uso ni del consumidor | 2 |

Dos categorías se agregan a la enumeración de referencia y conviene justificarlas, porque son exactamente las que esta capa existe para ejercer:

- **Conflicto de facultad** se declara aparte porque no se resuelve mirando el dato ni el estado, sino el papel de quien pide. Confundirla con un conflicto de estado llevaría a buscar el remedio en una transición que no existe.
- **Conflicto de alcance** se declara aparte de las otras dos porque su remedio también es distinto: no hay dato que corregir ni papel que cambiar, hay un trabajo que simplemente no forma parte del flujo de trabajo del administrador. Fundirla con «conflicto de estado» haría creer que existe una transición que lo trae al alcance, y no existe (RN-04011).

**Una divergencia deliberada de clasificación con el proyecto de código hermano, declarada para que no se lea como descuido.** El motivo `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` está clasificado allá como conflicto de facultad y acá como **entrada inválida**. El fundamento es uno solo, y es que el referente cambia con la capa: en el dominio el papel llega como pretensión de constituir una entidad reservada; acá el papel es **un dato del pedido de alta**, no la facultad de quien pide. Nadie está ejerciendo una facultad que no tiene, y **CU-04001 no verifica facultad ni pertenencia** —el auto-registro lo ejerce una persona que todavía no tiene cuenta (CU-04001 §10)—: lo que se rechaza es un valor del pedido, exactamente como en `UNRECOGNIZED_ROLE`, que esta capa clasifica igual.

Y lo que esta divergencia **no** invoca, escrito acá para que nadie lo reponga: **no hay correspondencia uno a uno entre la categoría de conflicto de facultad y la negativa por facultad de §2.4**. La categoría tiene tres miembros —`ADMINISTRATOR_ROLE_REQUIRED`, `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` y `RESET_LIMITED_TO_STUDENT_ACCOUNTS`— y las negativas de autorización son cuatro, de las cuales sólo la primera de esas tres es una. Son cosas de distinto orden: la categoría es taxonómica y la negativa es una de las tres comprobaciones de autorización. La clasificación de este motivo se sostiene por el referente del papel y por nada más.

### 2.2 Las dos categorías que el proyecto de código hermano declaró vacías

El proyecto de código hermano las declaró vacías con su motivo, porque el dominio no ejecuta entrada ni salida. **Acá no están vacías, y la diferencia es informativa**: es la primera capa del producto que depende de algo que puede no responder.

| Categoría | Condiciones | Por qué existen acá y no en el dominio |
| --- | --- | --- |
| **Error transitorio** | `PARSE_RESULT_UNAVAILABLE` | Esta capa **depende de puertos**, y un puerto puede no poder completar lo que se le pidió. La terminación es degradada y declarada: el trabajo queda en `Borrador` con su texto intacto (CU-04005 §6). Aun así, **esta capa no reintenta**: devuelve el estado degradado y quien decida reintentar es el consumidor |
| **Error interno** | `MALFORMED_PIECE_SET`, `MALFORMED_OBSERVATION` | Son los dos casos en que el motivo no denuncia lo que el consumidor pidió sino **lo que un adaptador devolvió**. El caso de uso no los puede corregir y no los puede mostrar: un conjunto mal formado es un defecto del validador y no un resultado que el alumno deba ver (CU-04005 §6). Los dos son **condiciones agregadas**, y su relación con los ocho rechazos del dominio que agrupan está en §2.5 |

Ninguna otra condición pertenece a estas dos categorías, y una falla no declarada tampoco: su lugar es una prueba que falla, no una entrada acá.

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que el consumidor tiene que hacer:

| Forma | Qué significa | Dónde aparece |
| --- | --- | --- |
| **Negativa sin escritura** | El caso de uso se niega a una operación de escritura. No abre la unidad de trabajo, o la cierra sin efecto; el repositorio queda exactamente como estaba | CU-04001, CU-04002, CU-04003 en sus operaciones sobre la credencial, CU-04004, CU-04005, CU-04008, CU-04009, CU-04010 |
| **Motivo de resultado** | La operación es una consulta y **siempre devuelve un resultado**; el motivo es la razón por la que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | CU-04003 en la consulta de admisibilidad, CU-04006, CU-04007 |
| **Terminación degradada** | La operación no se completó por una causa que no depende del pedido, y el caso de uso lo declara en vez de fingir un resultado. Es la forma de una sola condición: `PARSE_RESULT_UNAVAILABLE` | CU-04005 |

La diferencia importa: ante una negativa sin escritura el consumidor corrige la invocación; ante un motivo de resultado **informa** o **encamina** a la operación que corresponde; ante una terminación degradada informa que el servicio no está disponible y **no** presenta el trabajo como interpretado. `CREDENTIAL_NOT_SET` es el ejemplo canónico del segundo caso: no es un fallo, es la situación esperada del primer ingreso efectivo del alumno.

### 2.4 Las tres negativas de autorización

Esta es la sección que justifica que `tiene_auth` valga true en este proyecto de código, y la que hay que dejar imposible de confundir. Las **cuatro** comprobaciones transversales de `Especificacion-Funcional.md` §4 producen cuatro negativas, y **confundir las dos primeras es el error más caro que un consumidor puede cometer contra esta capa**: confirmar que un recurso ajeno existe habilita averiguar por tanteo qué identificadores existen.

| Negativa | Motivo | Qué se preguntó | ¿Oculta la existencia del recurso? | Traducción del consumidor |
| --- | --- | --- | --- | --- |
| **Pertenencia** | `WORK_NOT_FOUND_FOR_REQUESTER` | ¿Este trabajo es del alumno que lo pide? | **Sí, deliberadamente.** El trabajo ajeno y el identificador inexistente comparten motivo por diseño | «No encontrado», y **nunca** «no autorizado» |
| **Facultad** | `ADMINISTRATOR_ROLE_REQUIRED` | ¿Quien pide esta operación reservada tiene el papel `Administrador`? | **No, y no tiene por qué.** No hay recurso ajeno cuya existencia proteger: se preguntó por una facultad, no por un recurso | Explícita: la operación requiere la facultad de administrador |
| **Alcance** | `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | ¿Este trabajo entra en lo que el administrador ve? | **No.** Expresa que el trabajo está fuera de su flujo de trabajo, no que no exista | Explícita: los trabajos en `Borrador` no forman parte de la revisión |
| **Cambio de contraseña pendiente** | `PASSWORD_CHANGE_PENDING` | ¿La cuenta que pide fue reseteada por el administrador y todavía no cambió su clave? | **No, y no debe.** La persona sabe perfectamente que le resetearon la clave: ocultarlo la dejaría sin saber qué hacer | Explícita, y **con el camino**: hay que cambiar la contraseña antes de cualquier otra cosa |

Las **cinco** precisiones que rigen en toda la categoría, transcriptas de `Especificacion-Funcional.md` §4 porque son el insumo directo de este catálogo:

1. **El papel no reemplaza a la pertenencia.** Son dos comprobaciones distintas: un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso.
2. **La negativa por pertenencia y la negativa por facultad no se confunden.** La primera oculta la existencia del recurso; la segunda no tiene nada que ocultar.
3. **La comprobación se hace sobre el dato recuperado y antes de escribir.** No se resuelve ocultando un control en la pantalla, y por eso es verificable con dobles sin base de datos.
4. **El trabajo ajeno y el identificador inexistente comparten motivo por diseño.** Distinguirlos permitiría averiguar por tanteo qué identificadores existen.
5. **La cuarta comprobación corta antes que las otras tres y tiene una sola excepción.** Una cuenta marcada por un reseteo no ejerce ninguna capacidad —ni las que su papel y su pertenencia admitirían— salvo cambiar su propia contraseña, que es el reemplazo de CU-04003 FA-05. La marca la pone únicamente CU-04011 y la levanta únicamente ese cambio (INV-09). **Para el consumidor esto tiene una consecuencia operativa**: ante `PASSWORD_CHANGE_PENDING` no hay dato que corregir ni papel que cambiar, hay una sola ruta a la que llevar a la persona.

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos motivos distintos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador— y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse (`Especificacion-Funcional.md` §4, CU-04008 §10, CU-04009 §10). Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.

**Procedimiento de decisión**, para el consumidor que tiene que traducir un motivo y para quien escribe un caso de uso nuevo:

1. **¿La pregunta fue por un recurso concreto que puede ser de otra persona?** Si es sí, la negativa oculta: mismo motivo para el ajeno y para el inexistente, y traducción a «no encontrado». Termina acá.
2. **¿La pregunta fue por una facultad, sin recurso ajeno de por medio?** Entonces la negativa puede ser explícita: no hay nada que ocultar, y ocultarla sólo haría más difícil el diagnóstico.
3. **¿La cuenta que pide está marcada como con cambio de contraseña pendiente?** Entonces ninguna de las dos preguntas anteriores llega a hacerse: la negativa es explícita y encamina al cambio.
3. **¿La pregunta fue por un recurso que el papel sí puede ver en general, pero éste en particular queda fuera de su alcance?** Entonces la negativa es explícita y **no oculta**: el administrador ve todo lo que no es borrador, y decirle que un borrador está fuera de su alcance no le revela nada que no supiera.

**Traducciones prohibidas.** Ninguna de estas cuatro es admisible en `GeometriaFactory-Api` ni en ninguna superficie aguas abajo, y la métrica que las cuenta tiene objetivo cero ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §6):

| Traducción prohibida | Por qué | Qué corresponde |
| --- | --- | --- |
| `WORK_NOT_FOUND_FOR_REQUESTER` → «no autorizado» | Confirma que el recurso existe y que es de otro. Es exactamente lo que RN-04003 impide | «No encontrado» |
| Devolver una respuesta distinta para el trabajo ajeno y para el identificador inexistente | La distinción por sí sola permite el tanteo, aunque los dos textos sean vagos | Una sola respuesta, indistinguible |
| Distinguir hacia afuera la cuenta inexistente de la cuenta que no admite ingreso | Revela qué correos están registrados (CU-04003 §6 y §10) | No admisible, sin distinguir el motivo hacia afuera |
| `ADMINISTRATOR_ROLE_REQUIRED` → «no encontrado» | El error simétrico, y también es un defecto: oculta lo que no hace falta ocultar y deja al integrador sin diagnóstico | Explícita, tal como el motivo la declara |

**Cómo se sostiene esto sin confiar en la buena memoria.** La indistinguibilidad es verificable: CA-03 de CU-04006 exige que el motivo devuelto para el detalle de un trabajo ajeno sea **el mismo** que para un identificador inexistente, y CA-03 de CU-04009 lo exige para la eliminación. Las dos son pruebas unitarias con repositorio simulado, y son las que impiden que una refactorización reintroduzca la distinción sin que nadie se dé cuenta.

### 2.5 Lo que esta capa produce y lo que el dominio rechaza sin que acá ocurra

Los once casos de uso orquestan trece casos de uso del dominio, y **el dominio declara rechazos que esta capa no puede producir**. Su ausencia del catálogo no es un olvido: la 02 los nombra uno por uno en sus §10 para que no se lea así, y acá se reúnen en una sola tabla porque para quien implementa la capa es información operativa. **Ninguna fila de esta tabla es una condición de este catálogo**, y por eso ninguna entra en los recuentos de §7.

| Rechazo del dominio | Origen | Por qué acá no ocurre | Dónde está declarado |
| --- | --- | --- | --- |
| `EMAIL_UNIQUENESS_NOT_VERIFIED` | Dominio, auto-registro y configuración | **Inalcanzable por construcción.** Los dos caminos de alta consultan el correo antes y declaran siempre la verificación al invocar | CU-04001 §10 |
| `DELETION_WITHOUT_WORK_CASCADE` | Dominio, ciclo de vida de la cuenta | **Inalcanzable por construcción.** El flujo alternativo de la baja siempre declara el arrastre | CU-04002 §10 |
| `EDIT_OUTSIDE_DRAFT` | Dominio, creación y reedición del trabajo | **Equivalente**, no ausente: es la misma negativa que `OPERATION_OUTSIDE_DRAFT`. Esta capa corta antes, con la resolución de acceso del dominio | CU-04004 §10 |
| `SUBMISSION_WITHOUT_PARSE_RESULT` | Dominio, gobierno del estado del trabajo | **Inalcanzable por construcción.** El envío interpreta siempre antes de invocar al dominio | CU-04005 §10 |
| `OUTCOME_NOT_ALLOWED_BY_CONTRACT` | Dominio, gobierno del estado del trabajo | **Inalcanzable por construcción.** El envío no ofrece aprobar ni rechazar: eso es CU-04008 | CU-04005 §10 |
| `UNKNOWN_PIECE_TYPE`, `DECLARED_FAMILY_CONTRADICTS_TYPE`, `INVALID_PIECE_POSITION`, `REBUILD_ON_TERMINAL_WORK` | Dominio, reconstrucción del conjunto de piezas | **Agregados deliberadamente** en `MALFORMED_PIECE_SET`. Los cuatro son defectos del validador o de la orquestación, y ninguno es un resultado que el alumno deba ver | CU-04005 §6 |
| `UNKNOWN_OBSERVATION_KIND`, `ERROR_WITHOUT_LOCATION`, `WARNING_MISSING_BOTH_VALUES`, `OBSERVATION_ON_MISSING_PIECE` | Dominio, registro de las observaciones | **Agregados deliberadamente** en `MALFORMED_OBSERVATION`, por el mismo criterio y de forma simétrica | CU-04005 §6 |
| `OUTCOME_REQUIRES_ADMINISTRATOR_ROLE`, `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` | Dominio, desenlace y alcance del administrador | **No llegan a producirse.** Esta capa corta antes con su propia verificación de facultad, y emite un motivo único por los dos | `Especificacion-Funcional.md` §4, CU-04008 §10, CU-04009 §10 |
| `UNKNOWN_OPERATION` | Dominio, acceso del alumno y alcance del administrador | **Inalcanzable por construcción.** Cada resolución se consulta con una operación fija; lo que sí puede llegar mal es el papel, y eso es `UNRECOGNIZED_ROLE` | CU-04009 §10 |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Dominio, reseteo de la contraseña de una cuenta de alumno | **No llega a producirse.** Esta capa corta antes con su propio acotamiento a cuentas de alumno y emite `RESET_LIMITED_TO_STUDENT_ACCOUNTS`; el cierre es el mismo y su fuente es `RN-04015`, que lo ancla en INV-08 | CU-04011 §6 y §10 |
| `RESET_WITH_WORK_CASCADE` | Dominio, reseteo de la contraseña de una cuenta de alumno | **Inalcanzable por construcción.** La invocación de esta capa **nunca declara** efecto sobre los trabajos ni sobre el estado de la cuenta: el reseteo no es una baja y no dispara RN-04007 (RN-04012) | CU-04011 §7 y §10 |

Dos consecuencias para quien implementa:

1. **Una condición agregada esconde varias causas del dominio, y eso es deliberado.** Al depurar `MALFORMED_PIECE_SET` o `MALFORMED_OBSERVATION`, el motivo fino que hay que mirar es el que devolvió el dominio, y está en las tablas de la 02 de `GeometriaFactory-Domain`. Este catálogo no lo repite porque no es lo que esta capa emite.
2. **Un rechazo inalcanzable que aparece en ejecución es un defecto de esta capa, no del consumidor.** Si el dominio devuelve `SUBMISSION_WITHOUT_PARSE_RESULT`, el caso de uso saltó un paso propio. Es la mejor señal temprana que ofrece esta frontera.

### 2.4 `GeometriaFactory-Infrastructure`

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | Lo que llegó no es utilizable: falta, está vacío, o pide algo que el contrato no admite | 6 |
| **Recurso ausente** | Lo que la operación necesita no fue provisto | 1 |
| **Conflicto de estado** | La operación es legítima, pero el estado del almacén o del conjunto no la admite | 4 |
| **Conflicto de facultad** | — | **0** |
| **Conflicto de alcance** | — | **0** |
| **Error transitorio** | Algo de lo que esta capa depende no pudo completar lo que se le pidió, por una causa que no depende de lo que se pidió | 5 |
| **Error interno** | El dato guardado no permite hacer lo que el contrato promete | 1 |

**El error transitorio es la categoría más poblada de este catálogo, y es la señal más clara de dónde está esta capa.** Las capas de adentro se prueban enteras con dobles y no dependen de nada; acá se depende de un archivo, de una fuente de aleatoriedad y de un secreto que alguien tiene que haber provisto. **Cinco de diecisiete condiciones existen porque el mundo puede no responder.**

### 2.2 Las dos categorías vacías, y por qué acá lo están

| Categoría | Por qué está vacía |
| --- | --- |
| **Conflicto de facultad** | **Esta capa no autoriza.** No comprueba el papel de quien pide y no recibe la identidad del solicitante para comprobarla. La verificación de facultad es de `GeometriaFactory-Application`, y llega resuelta |
| **Conflicto de alcance** | Por la misma razón. El recorte por dueño o por estado **llega en el pedido**: esta capa lo resuelve, no lo decide. Lo único que hace por su cuenta es **negarse a resolver una consulta que llega sin recorte**, y eso está clasificado como entrada inválida, porque lo que falta es un dato del pedido y no una facultad de quien lo hace |

**Es el espejo exacto del proyecto de código hermano.** En `GeometriaFactory-Application` estas dos categorías son las que existen y las que justifican su flag de autenticación; acá están vacías y el flag vale true por otra cosa: porque acá viven **los mecanismos**. Quien busque en este catálogo una negativa de autorización está buscando en la capa equivocada.

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que hay que hacer:

| Forma | Qué significa | Cuántas | Qué tiene que hacer quien la recibe |
| --- | --- | --- | --- |
| **Negativa sin escritura** | El contrato se niega. No abre la unidad de trabajo, o la cierra sin efecto; el almacén queda exactamente como estaba | 11 | Corregir la invocación |
| **Terminación degradada** | La operación no se completó por una causa que no depende del pedido, y el contrato **lo declara en vez de fingir un resultado**. **Esta capa no reintenta** | 4 | Informar el estado degradado. Reintentar, si corresponde, lo decide el consumidor |
| **Arranque detenido** | La preparación del almacén no se pudo completar y **el servicio no atiende ninguna petición**. Es la forma propia de esta capa y no existe en ninguna otra | 2 | Revisar el despliegue: el volumen, la ruta, el linaje de transformaciones. **No es un problema de código** |

**La forma «motivo de resultado» no se usa en este catálogo, y se declara para que su ausencia no se lea como olvido.** Es la forma de las consultas que siempre devuelven un resultado con su razón, y acá las consultas devuelven **el dato o nada encontrado**: la razón por la que no hay nada la pone el consumidor, que es el que sabe quién preguntó.

### 2.4 Las tres condiciones que fallan hacia el lado seguro

Son las tres que un implementador apurado convertiría en un valor por defecto, y las tres tienen en común que **el atajo no falla: funciona mal en silencio.** Es la clase de defecto que este catálogo existe para prevenir.

| Condición | El atajo tentador | Por qué el atajo es peor que la condición |
| --- | --- | --- |
| `RANDOMNESS_SOURCE_UNAVAILABLE` | Componer la contraseña provisoria con un contador, con la fecha o con el correo del alumno | Produce una provisoria **adivinable**, que es exactamente lo que RN-06014 prohíbe, y el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** |
| `SIGNING_KEY_MISSING` | Generar una clave al vuelo, o emitir sin firmar | El sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno. Una clave generada al vuelo además invalida todos los accesos en cada reinicio, con lo cual el síntoma visible es otro |
| `STORE_PATH_UNAVAILABLE` | Caer hacia una ruta alternativa dentro de la imagen | El servicio arranca, acepta trabajos de la comisión entera **y los pierde en el siguiente reemplazo de versión**. Nadie se entera hasta que alguien busca su trabajo y no está |

La regla que resume las tres, y que conviene poder recitar: **cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice. No la cumple a medias.**

`MIGRATION_NOT_APPLICABLE` es de la misma familia y merece su propia línea porque su atajo es el más destructivo de todos: **descartar el almacén y crearlo de nuevo** deja el servicio impecable y sin los trabajos de nadie.

### 2.5 El caso de uso sin condiciones

**`CU-06009` no tiene ninguna entrada en este catálogo, y su ausencia está declarada.** Devolver el momento actual no recibe entrada que pueda ser inválida, no toca el almacén, no consume secretos y no depende de nada que pueda no responder: la única forma de que falle es que falle el proceso entero, y eso no es una condición de ningún contrato.

Se deja escrito por dos motivos. Uno, para que una revisión posterior no lo levante como cobertura faltante: **son nueve subsecciones de catálogo para diez casos de uso, y el hueco es intencional**. Y dos, porque su ausencia dice algo: es el contrato más trivial de la capa, y **que sea trivial es la prueba de que la inversión está bien hecha**. Si algún día tuviera una condición, sería señal de que se le agregó lógica que pertenece a otro lado.

## 3. Catálogo

### 3.1 `GeometriaFactory-Api`

**Dieciocho entradas.** Las **dieciséis** primeras son los códigos del conjunto cerrado con destino en esta superficie; las dos últimas son las respuestas sin código de §2.2. **Ninguna se inventó y ninguna quedó afuera**; el recuento y su verificación están en §6. Eran dieciocho hasta la emisión 1.0: **RN-00016** retiró dos códigos del conjunto cerrado y ninguno los reemplaza.

### 3.1 Entrada inválida

Respuesta `400`. **Ninguna de las tres deja escritura.**

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | Falta un campo que la petición necesita | La solicitud se armó incompleta | **Corregir y reintentar.** La respuesta **nombra el campo**, y eso es lo que permite señalarlo en el formulario en vez de mostrar un cartel genérico |
| `CONFIRMATION_MISMATCH` | El correo escrito como confirmación no coincide con el de la cuenta | Quien da de baja escribió otro correo | **Mostrar lo que pasó y pedir de nuevo.** La respuesta **no devuelve el correo esperado**: si lo devolviera, la confirmación dejaría de confirmar nada |
| — (sin código) | La petición no se puede leer | El cuerpo está mal formado, o un valor no pertenece a un conjunto cerrado | **Corregir y reintentar.** Ver §2.2 |

### 3.2 Credencial no admitida

Respuesta `401`. **Ninguna de las dos declara más de lo que declara**, y es la primera de las tres familias empobrecidas de §1.5.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `INVALID_CREDENTIALS` | La credencial presentada no habilita | El correo no corresponde a ninguna cuenta, **o** la contraseña no es la de esa cuenta, **o** la contraseña vigente presentada en un cambio no corresponde | **Mostrar lo que pasó y pedir de nuevo.** El mensaje **no dice cuál de los dos campos falló**, y el consumidor **no debe inferirlo ni sugerirlo** |
| — (sin código) | No hay un acceso válido en la petición | No se presentó acceso, venció, o su firma no corresponde | **Volver a canjear credenciales.** Las tres causas responden igual, porque el trabajo que le queda al consumidor es el mismo |

### 3.3 Situación de la cuenta

Respuesta `403`, **con motivo**. Es la única familia del catálogo donde el motivo es lo importante: **de él depende a qué pantalla deriva el consumidor**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `ACCOUNT_NOT_ENABLED` | La cuenta todavía no fue habilitada, o está bloqueada | El administrador no la habilitó, o la bloqueó | **Mostrar la situación.** No hay nada que la persona pueda hacer sola: **depende del administrador**, y decírselo evita que siga probando |
| `PASSWORD_CHANGE_REQUIRED` | La cuenta tiene una contraseña provisoria sin cambiar | **El administrador la habilitó (RN-00016) o la reseteó (F-26)**: desde `PRODUCT-INTAKE` 1.13 es también el mensaje del **primer ingreso**, y por eso reemplazó al que describía la cuenta habilitada sin contraseña | **Derivar al cambio de contraseña.** **Un solo código para todas las operaciones bloqueadas**, porque el trabajo que le queda al consumidor es siempre el mismo, y por eso el mensaje **no nombra la operación que se pidió** |

### 3.4 Facultad

Respuesta `403`. **Dos entradas desde que el primer hueco de §2.4 quedó cerrado**, y entre las dos cubren el rechazo por papel completo: dentro del desenlace y fuera de él.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `OUTCOME_ADMIN_ONLY` | Sólo el administrador resuelve un trabajo | Un alumno pidió el desenlace, **aun de un trabajo propio** | **Mostrar lo que pasó.** A diferencia de la familia del recurso no visible, acá **no hay nada que ocultar**: el trabajo puede ser del propio solicitante, y la negativa no revela nada que él no sepa |
| `OPERATION_ADMIN_ONLY` | Esta acción es exclusiva del administrador | Un alumno pidió gobernar las cuentas de la comisión, ver el listado de la comisión o resetear la contraseña de otra cuenta | **Mostrar «no tenés permiso», y no el mensaje de fallo.** Es exactamente la distinción que el código genérico no permitía hacer: el consumidor sabe que la petición estuvo bien formada y que lo que no alcanza es el papel |

### 3.5 Recurso no visible

Respuesta `404`. Es la segunda familia empobrecida de §1.5, y **la que sostiene la regla que esta capa puede romper sola**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `WORK_NOT_FOUND` | El trabajo pedido no está disponible para el solicitante | **Tres causas indistinguibles**: no existe, no es suyo, o está fuera de lo que ve —el borrador que el administrador no ve— | **Mostrar lo que pasó, sin inferir cuál de las tres fue.** Un consumidor que muestre «ese trabajo es de otro alumno» rompe **RN-00003** desde el otro lado de la frontera |
| `STUDENT_NOT_FOUND` | La cuenta referenciada no está disponible | El filtro por alumno de un listado, o el identificador de una operación de administración, referencia una cuenta que no existe | **Reintentar sin el filtro**, o corregir el identificador |

### 3.6 Conflicto de estado

Respuesta `409`. **Seis entradas, la categoría más poblada.** Todas describen operaciones legítimas que el estado del sistema no admite, y en todas **quien pide tiene derecho a pedir**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `EMAIL_ALREADY_REGISTERED` | El correo ya pertenece a una cuenta | Un registro repetido | **Mostrar lo que pasó.** El mensaje **no declara la situación ni el papel** de la cuenta que lo ocupa: tercera familia empobrecida de §1.5 |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | La instancia ya tiene su cuenta de administrador | Se intentó configurar una segunda | **Mostrar lo que pasó, sin ofrecer alternativa**: el contrato no declara ninguna, y sugerir una sería inventarla |
| `STATE_FORBIDS_DELETE` | El estado del trabajo no habilita al solicitante a eliminarlo | El alumno pidió eliminar un trabajo suyo que ya no está en `Borrador` | **Mostrar el estado actual**, que la respuesta declara. **No se produce nunca en el camino del administrador** |
| `STATE_FORBIDS_UPDATE` | El trabajo ya fue entregado y no admite cambios | Se forzó un envío o una reedición sobre un trabajo en `Pendiente`, `Finalizado` o `Rechazado` | **Mostrar el estado actual y no ofrecer forma de volver a `Borrador`**: no existe. Es el segundo hueco de §2.4, cerrado |
| `STATE_FORBIDS_OUTCOME` | El trabajo no está en condiciones de recibir un desenlace | O nunca estuvo en estado `Pendiente`, o **ya lo recibió y está en un estado terminal** | **Mostrar el estado actual y no ofrecer forma de revertirlo**: no existe |
| `RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | La cuenta de administrador no se resetea por este camino | Se pidió el reseteo sobre ella | **Derivar al cambio de la propia contraseña**, que es el camino que sí existe |

### 3.7 No clasificado

| Código del contrato | Respuesta | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- | --- |
| `UNCLASSIFIED_ERROR` | `500` o `503` | Lo que ocurrió no tiene una representación propia en el contrato | **Dos causas, desde que §2.4 quedó cerrado**: una terminación degradada del almacén o de una fuente de la que el servicio depende (`503`); un defecto no previsto (`500`) | **Depende del número, y ése es el problema.** Con `503`, pasar a **estado degradado explícito**; con `500`, mostrar que algo falló; con `403` y `409`, mostrar lo que pasó sin más detalle. **Los dos primeros existen sólo por los huecos de §2.4** |

### 3.2 `GeometriaFactory-Domain`

Cuarenta y tres condiciones, derivadas una por una de la §6 de los trece casos de uso. Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación están en §6.

Siete condiciones aparecen declaradas en más de un caso de uso. Seis de ellas conservan la misma causa en todos y llevan **una sola entrada**, en el caso de uso donde se declaran primero, con la nota de sus otras apariciones. La séptima, `INITIAL_STATUS_NOT_NEGOTIABLE`, lleva **fila completa en §3.1 y en §3.12** porque sus dos causas son opuestas según el camino de alta: el motivo está en §1.4.

### 3.1 CU-02001 Registrar el alta de un alumno

Es el **auto-registro del alumno**, uno de los dos caminos de alta (§1.4). Forma de terminación: rechazo. En los cinco casos no se produce ninguna instancia y no hay efecto parcial.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | Entrada inválida | Falta un dato obligatorio del alta: correo, nombre o apellido | Uno de los tres llegó vacío o no se proveyó | Completar el dato faltante antes de invocar. El dominio no lo infiere ni lo deja en blanco. Esta condición vuelve a declararse en CU-02005, sobre el nombre y la fecha del trabajo |
| `EMAIL_UNIQUENESS_NOT_VERIFIED` | Entrada inválida | La unicidad del correo no viene declarada como comprobada | El consumidor invocó sin afirmar que comprobó que el correo esté libre | Resolver la unicidad en `GeometriaFactory-Application` con el puerto de repositorio y declararla al invocar. El correo es único en todo el sistema (INV-01, RN-02002) y esa comprobación se afirma sobre el conjunto de alumnos, que el dominio no conoce. Esta condición vuelve a declararse en CU-02012, con la misma causa |
| `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION` | Entrada inválida | El **auto-registro** no admite credencial derivada | Se aportó una credencial derivada junto con los datos del auto-registro | Registrar sin credencial: en este camino se fija recién en el primer ingreso efectivo, por CU-02003. **En la configuración del administrador la credencial sí se aporta**, y eso es CU-02012: el código está acotado a este camino |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta del auto-registro en un estado distinto de `Pendiente` | Constituir sin pedir estado. Toda cuenta de alumno nace `Pendiente` y sólo el administrador la habilita, con acto explícito (CU-02002). **Mismo identificador, causa opuesta en CU-02012**, donde el estado impuesto es `Habilitado`: ver §3.12 y §1.4 |
| `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | Conflicto de facultad | El auto-registro no constituye cuentas con papel `Administrador` | Se pidió constituir un administrador por la vía del alumno | Usar CU-02012, que es el camino que la fuente declara para la configuración del administrador. Constituirlo acá lo dejaría con la cuenta `Pendiente` y sin salida, porque ninguna otra cuenta podría habilitarlo (§1.4) |

### 3.2 CU-02002 Gobernar el ciclo de vida de la cuenta

Las cuatro operaciones de este contrato —habilitar, bloquear, rehabilitar y dar de baja— alcanzan **sólo a las cuentas con papel `Alumno`** (F-03). Forma de terminación: rechazo. En los **cuatro** casos la cuenta queda exactamente como estaba.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ACCOUNT_TRANSITION_NOT_ALLOWED` | Conflicto de estado | La transición pedida no figura en la tabla de transiciones de la cuenta | El par estado actual y operación no está declarado; el caso típico es bloquear una cuenta `Pendiente` sin haber pasado por `Habilitado` | Consultar la tabla de `Definicion-Modelo-De-Dominio.md` §5.1 y encadenar las transiciones declaradas. El dominio no infiere transiciones que ninguna fuente declara |
| `DELETION_WITHOUT_WORK_CASCADE` | Entrada inválida | La baja no admite conservar los trabajos del alumno | Se solicitó la baja declarando que los trabajos se conservan | Solicitar la baja con arrastre y materializar cuenta y trabajos como una sola unidad. El arrastre alcanza a los trabajos en cualquier estado, incluidos `Finalizado` y `Rechazado`, y es una consecuencia aceptada por escrito aguas arriba (RN-02007) |
| `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` | Entrada inválida | Habilitar o rehabilitar exige la credencial derivada provisoria | Se invocó la transición sin aportar el valor derivado de la contraseña provisoria que el sistema produce | Producir la provisoria en la capa que corresponde, derivarla y aportarla en la misma invocación. Desde **RN-02016**, fijar la credencial y poner la marca son efectos del mismo acto que la habilitación, y admitirla sin credencial dejaría la cuenta `Habilitado` sin nada con que autenticarse: es la ventana que RN-02016 cierra. Bloquear y dar de baja **no** exigen credencial |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Conflicto de facultad | Ninguna de las cuatro operaciones procede sobre la cuenta con papel `Administrador` | Se pidió **habilitar, bloquear, rehabilitar o dar de baja** al administrador de la instancia | No hay camino, y no lo hay para ninguna de las cuatro: las cuatro están declaradas sobre cuentas de alumno (F-03) y sobre la única cuenta de administrador ninguna tiene inversa posible (RN-02001, INV-05). Bloquearla o darla de baja deja a la instancia **sin nadie capaz de habilitar, desbloquear y revisar**, y con el circuito de revisión detenido: todo trabajo enviado queda en estado `Pendiente` para siempre. Ver §1.4 |

### 3.3 CU-02003 Fijar y reemplazar la credencial derivada

Forma de terminación: rechazo. En los cuatro casos el alumno queda exactamente como estaba.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` | Conflicto de estado | La credencial derivada sólo se fija con la cuenta `Habilitado` | El estado de cuenta es `Pendiente` o `Bloqueado` | Habilitar o rehabilitar la cuenta primero, por CU-02002. Es la misma condición que INV-06 expresa desde el lado del acceso |
| `CREDENTIAL_ALREADY_SET` | Conflicto de estado | La credencial derivada ya tiene valor | Se pidió fijar por primera vez algo que ya está fijado | Usar el camino de reemplazo, declarando verificada la credencial vigente. El valor anterior se reemplaza y no se conserva historial |
| `CURRENT_CREDENTIAL_NOT_VERIFIED` | Entrada inválida | El reemplazo exige declarar verificada la credencial vigente | Se pidió el reemplazo sin esa declaración | Verificar la credencial vigente en la capa que sí puede compararla, `GeometriaFactory-Infrastructure`, y declararlo al invocar. El dominio no compara credenciales |
| `EMPTY_DERIVED_VALUE` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó con un valor sin contenido | Aportar el valor ya derivado. El dominio no deriva la contraseña y nunca la conoce en claro (`PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Domain) |

### 3.4 CU-02004 Evaluar la admisibilidad de la cuenta

Forma de terminación: **motivo de resultado**. Los **tres** son terminaciones controladas y no excepciones de programa: la evaluación siempre devuelve un resultado, y ese resultado incluye el motivo.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ACCOUNT_PENDING` | Conflicto de estado | La cuenta está registrada y todavía no fue habilitada | El estado de cuenta es `Pendiente` (INV-06, RN-02006) | Informar la situación con todas las letras y no con un rechazo genérico: la persona tiene que saber que espera la habilitación del administrador. No emitir acceso |
| `ACCOUNT_BLOCKED` | Conflicto de estado | La cuenta está bloqueada | El estado de cuenta es `Bloqueado` (INV-06, RN-02006) | Informar el motivo y no emitir acceso. La rehabilitación es un acto explícito del administrador, por CU-02002 |
| `PASSWORD_CHANGE_PENDING` | Conflicto de estado | La cuenta tiene una contraseña provisoria sin cambiar | El administrador la **habilitó** por CU-02002 o la **reseteó** por CU-02013, y la marca sigue puesta. Desde **RN-02016** los dos actos la producen, y es también el motivo con el que llega todo alumno a su primer ingreso | **No es un fallo.** Encaminar al cambio de contraseña, que es el reemplazo de CU-02003 FA-04 y **lo único** que la cuenta puede hacer hasta que la marca se levante (INV-09, RN-02013). No emitir acceso, y no ofrecer ninguna otra ruta: la contraseña nueva la elige el alumno y el administrador no la conoce |

### 3.5 CU-02005 Crear y reeditar un trabajo

Forma de terminación: rechazo.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `WORK_WITHOUT_OWNER` | Entrada inválida | El trabajo no trae dueño | Se invocó la constitución sin el alumno al que pertenece | Aportar el dueño. Un trabajo sin dueño no es un trabajo, y la pertenencia no es transferible (INV-02) |
| `REQUIRED_FIELD_MISSING` | Entrada inválida | Falta el nombre o la fecha del trabajo | Uno de los dos no se proveyó | Completar el dato. La fecha es el dato que el alumno declara, no el del reloj del sistema: el dominio no lee el reloj. Entrada única en §3.1; ésta es su segunda declaración |
| `EDIT_OUTSIDE_DRAFT` | Conflicto de estado | Sólo se reedita un trabajo en `Borrador` | Se pidió reeditar un trabajo en estado `Pendiente`, `Finalizado` o `Rechazado` | No hay reedición fuera del borrador, y en los dos estados terminales el contenido tampoco cambia (INV-07). Si el trabajo fue rechazado, el camino es cargar uno nuevo |
| `ORIGINAL_JSON_ALTERED` | Entrada inválida | El texto original no admite versiones corregidas | El consumidor aportó un texto que declara ser una corrección del que pegó el alumno | Conservar el texto tal como el alumno lo pegó. El producto no edita el dato del alumno (RN-02008), y es justamente lo que hace posible reprocesar el mismo trabajo cuando el validador mejora |

### 3.6 CU-02006 Reconstruir el conjunto de piezas del trabajo

Forma de terminación: rechazo. Salvo donde se indica, el rechazo alcanza a la reconstrucción entera.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `INVALID_PIECE_POSITION` | Entrada inválida | Una posición del conjunto de piezas está repetida, es negativa o cae fuera del rango del conjunto raíz declarado | El conjunto llegó sin identidad posicional estable | Entregar cada pieza con **la posición que su figura ocupa en el conjunto raíz del texto del alumno**, sin recalcularla. La identidad de la pieza es esa posición, porque el dato del alumno no trae identificador propio. **Un hueco no es un defecto**: es la posición reservada de una figura que no se pudo reconstruir (CU-02006 FA-03), y renumerar las adoptadas para dejarlas contiguas desplazaría el índice que el alumno ve y que la observación tiene que informar |
| `UNKNOWN_PIECE_TYPE` | Entrada inválida | El tipo de la pieza no pertenece al conjunto conocido | El texto del alumno declaró un tipo fuera de `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo` | No es un rechazo del conjunto: esa pieza no se adopta y las demás sí, porque un defecto en un elemento no descarta el resto. **La posición de la figura no adoptada queda reservada** y las demás conservan la suya. Registrar la observación de especie error de validación por CU-02007, sobre esa misma posición y con su campo |
| `DECLARED_FAMILY_CONTRADICTS_TYPE` | Entrada inválida | La familia plana o volumétrica aportada contradice a la que el tipo deriva | Se aportó la familia como dato | No aportar la familia: **se deriva del tipo y no se guarda**. `Cilindro`, `Cubo` y `Ortoedro` son volumétricos; `Rectangulo`, `Cuadrado` y `Circulo` son planos |
| `REBUILD_ON_TERMINAL_WORK` | Conflicto de estado | Un trabajo en estado terminal no admite reconstrucción | El trabajo está `Finalizado` o `Rechazado` | No reconstruir: los dos estados terminales no cambian de estado ni de contenido (INV-07). Si hay que reprocesar, el camino es un trabajo nuevo |

### 3.7 CU-02007 Registrar las observaciones del trabajo

Forma de terminación: rechazo. En los cuatro casos se rechaza el conjunto entero de observaciones.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `UNKNOWN_OBSERVATION_KIND` | Entrada inválida | La especie de la observación no es `Advertencia` ni `Error de validación` | Llegó un tercer valor | Usar una de las dos especies. La especie es lo que decide el efecto sobre el envío y el conjunto es cerrado (RN-02005) |
| `ERROR_WITHOUT_LOCATION` | Entrada inválida | Una observación de especie error de validación no indica posición de pieza ni campo, siendo atribuible a una figura | El validador emitió un defecto sin ubicarlo | Emitirlo con su posición de pieza y su campo. Un mensaje genérico es exactamente lo que el producto viene a eliminar (RN-02009). Cuando el defecto **no** es atribuible a ninguna figura, la observación se adopta sin posición y con el campo que el consumidor indique |
| `WARNING_MISSING_BOTH_VALUES` | Entrada inválida | Una advertencia de discrepancia no trae el valor declarado o no trae el derivado | Se emitió con un solo número | Emitir los dos. Sin el par la advertencia no explica nada, y mostrar el par es el mayor valor didáctico del producto |
| `OBSERVATION_ON_MISSING_PIECE` | Recurso ausente | La posición indicada no pertenece al rango de posiciones del conjunto raíz interpretado | La observación designa una figura que el texto del alumno no trae | Ubicar la observación dentro del rango del conjunto raíz, o emitirla sin posición si el defecto no es atribuible a ninguna figura. **Una posición reservada no es una posición inexistente**: la de una figura que no se pudo reconstruir sí pertenece al rango y sí admite observación, que es precisamente el caso insignia de RN-02009 (CU-02006 FA-03, CU-02007 FA-04) |

### 3.8 CU-02008 Gobernar el estado del trabajo en el envío

Forma de terminación: rechazo. En los cuatro casos se conserva el estado actual.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `SUBMISSION_OUTSIDE_DRAFT` | Conflicto de estado | Sólo se envía un trabajo en `Borrador` | Se pidió enviar un trabajo que ya salió de las manos del alumno | No reenviar. Ninguna fuente declara una reentrada al envío desde estado `Pendiente`, y el dominio no la infiere |
| `TRANSITION_FROM_TERMINAL_STATUS` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió cualquier cambio de estado sobre un trabajo terminal | No hay camino de vuelta (INV-07, RN-02010). Corregir un rechazo significa cargar un trabajo nuevo; lo único que un trabajo terminal admite es que el administrador lo elimine. Esta condición vuelve a declararse en CU-02010, sobre un desenlace nuevo |
| `SUBMISSION_WITHOUT_PARSE_RESULT` | Conflicto de estado | El trabajo se envía sin que su texto original haya sido interpretado | Se invocó el envío antes de incorporar el resultado de la interpretación | Invocar CU-02006 y CU-02007 con lo que produjo el validador de figuras, y recién después enviar. El envío decide **sobre** ese resultado: sin él no hay nada que decidir |
| `OUTCOME_NOT_ALLOWED_BY_CONTRACT` | Conflicto de facultad | Aprobar y rechazar no se ejercen por la vía del envío | Se pidió un desenlace en el contrato del alumno | Usar CU-02010. El desenlace es facultad exclusiva del administrador y el alumno no lo ejerce ni sobre su propio trabajo (RN-02010) |

### 3.9 CU-02009 Resolver el acceso del alumno a un trabajo

Forma de terminación: **motivo de resultado**. Ninguno de los tres tiene efecto sobre el trabajo: la consulta no modifica nada.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `WORK_NOT_FOUND_FOR_REQUESTER` | Recurso ausente | El trabajo no existe para quien lo pide | El solicitante no es el dueño del trabajo | Traducirlo a «no encontrado» y **nunca** a «no autorizado»: confirmar la existencia de un trabajo ajeno es exactamente lo que RN-02003 e INV-02 impiden. La indistinguibilidad es deliberada |
| `OPERATION_OUTSIDE_DRAFT` | Conflicto de estado | El dueño no reedita ni elimina fuera de `Borrador` | Se consultó reeditar o eliminar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado` | Informar la acotación al borrador (RN-02004, INV-03). Es un motivo distinto del anterior porque acá la existencia del trabajo ya está admitida para su dueño. **Ver** un trabajo propio sí procede en los cuatro estados, incluidos el desenlace y el comentario |
| `UNKNOWN_OPERATION` | Entrada inválida | La operación consultada no pertenece al conjunto declarado | Se consultó algo distinto de ver, reeditar o eliminar | Consultar una de las operaciones declaradas. El dominio devuelve no procede sin evaluar siquiera la pertenencia. Esta condición vuelve a declararse en CU-02011 |

### 3.10 CU-02010 Resolver el desenlace del trabajo

Forma de terminación: rechazo. En los cuatro casos el trabajo queda intacto.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `OUTCOME_OUTSIDE_SUBMITTED` | Conflicto de estado | Sólo se aprueba o se rechaza un trabajo en estado `Pendiente` | Se pidió el desenlace de un trabajo en otro estado | Un trabajo en `Borrador` no se aprueba ni se rechaza: el administrador ni siquiera lo ve (RN-02011). Un trabajo terminal ya tuvo su desenlace |
| `OUTCOME_REQUIRES_ADMINISTRATOR_ROLE` | Conflicto de facultad | El desenlace exige papel `Administrador` | El papel declarado al invocar no es `Administrador` | Comprobar el papel antes de invocar. La facultad es exclusiva y no se delega, ni siquiera sobre el trabajo propio (RN-02010) |
| `TRANSITION_FROM_TERMINAL_STATUS` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió un desenlace nuevo sobre un trabajo que ya lo tuvo | No hay camino para corregir un desenlace aplicado (INV-07). Lo que el administrador sí puede hacer es eliminar el trabajo, por CU-02011. Entrada única en §3.8 |
| `UNKNOWN_OUTCOME` | Entrada inválida | El desenlace pedido no es aprobar ni rechazar | Llegó un tercer valor | Usar uno de los dos. Aprobar lleva a `Finalizado` y rechazar a `Rechazado`; los dos admiten comentario opcional y los dos son terminales |

### 3.11 CU-02011 Resolver el alcance del administrador sobre un trabajo

Forma de terminación: **motivo de resultado**. La consulta no modifica nada en ningún caso.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | Conflicto de estado | El trabajo está en `Borrador` y no entra en el alcance del administrador | Se consultó un borrador | Excluirlo de la vista de revisión y de la eliminación. A diferencia del motivo equivalente de CU-02009, **éste no oculta la existencia del trabajo**: expresa que está fuera del flujo de trabajo del administrador (RN-02011). Los tres estados que sí ve admiten eliminación, incluidos los dos terminales |
| `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` | Conflicto de facultad | La consulta de alcance exige papel `Administrador` | El papel declarado no es `Administrador` | La pregunta por lo que puede un alumno es CU-02009. El dominio devuelve no procede sin evaluar siquiera el estado |
| `UNKNOWN_OPERATION` | Entrada inválida | La operación consultada no pertenece al conjunto declarado | Se consultó algo distinto de las operaciones declaradas | Consultar una de las declaradas. Entrada única en §3.9 |

### 3.12 CU-02012 Configurar la cuenta de administrador

Es la **configuración del administrador en el primer arranque**, el otro camino de alta (§1.4). Forma de terminación: rechazo. En los cinco casos no se constituye ninguna entidad y la instancia sigue sin administrador, de modo que este mismo contrato vuelve a estar disponible.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | Entrada inválida | Falta un dato obligatorio: correo, nombre o apellido | Uno de los tres llegó vacío o no se proveyó | Completar el dato faltante antes de invocar. Entrada única en §3.1; ésta es su tercera declaración, con la misma causa |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | Conflicto de estado | La instancia ya tiene su cuenta de administrador, o el consumidor no declara que no la tiene | Se pidió configurar una segunda cuenta con papel `Administrador`, o se invocó sin declarar la ausencia de administrador previo | Comprobar sobre el conjunto de cuentas que no existe ninguna con ese papel y declararlo al invocar; el dominio no conoce ese conjunto. Si ya existe una, **no hay camino**: la ventana de alta se cerró y la instancia tiene exactamente un administrador (RN-02001, INV-05) |
| `EMAIL_UNIQUENESS_NOT_VERIFIED` | Entrada inválida | La unicidad del correo no viene declarada como comprobada | El consumidor invocó sin afirmar que comprobó que el correo esté libre | Igual que en el auto-registro: resolverla en la capa de aplicación con el puerto de repositorio y declararla al invocar (INV-01, RN-02002). Entrada única en §3.1 |
| `SETUP_WITHOUT_CREDENTIAL` | Entrada inválida | La configuración del administrador exige la credencial derivada | No se aportó credencial derivada, o el valor aportado está vacío | Aportar el valor **ya derivado**, que el dominio nunca conoce en claro. Una cuenta de administrador sin credencial no podría entrar y **no hay ninguna otra cuenta que pudiera resolverlo**: por eso acá la credencial es obligatoria y en el auto-registro está prohibida |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta de administrador en un estado distinto de `Habilitado` | Constituir sin pedir estado. **Mismo identificador, causa opuesta en CU-02001**, donde el estado impuesto es `Pendiente`: ver §3.1 y §1.4. Una cuenta de administrador `Pendiente` o `Bloqueado` dejaría a la instancia sin salida, porque por INV-06 no obtendría acceso y nadie podría habilitarla |

### 3.13 CU-02013 Resetear la contraseña de una cuenta de alumno

Es la **operación conservadora** del administrador sobre una cuenta ajena: fija una contraseña provisoria y pone la marca, sin tocar el estado de cuenta ni ninguno de los trabajos (§1.5). Forma de terminación: rechazo. En los **tres** casos la cuenta queda exactamente como estaba, con su credencial anterior y su marca anterior.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Conflicto de facultad | El reseteo no procede sobre la cuenta con papel `Administrador` | Se pidió resetear la contraseña del administrador de la instancia | No hay camino, y es el mismo que ya cerraba las cuatro operaciones de CU-02002: **el código se reutiliza porque la causa es la misma**, una operación del administrador declarada sobre cuentas de alumno y sin nadie que la ejerza sobre la suya (RN-02001, INV-05, INV-08). Su cambio de contraseña entra por el reemplazo de CU-02003 FA-01. Entrada única en §3.2; ésta es su segunda declaración |
| `RESET_WITH_WORK_CASCADE` | Entrada inválida | El reseteo no admite eliminar los trabajos del alumno ni cambiar su estado de cuenta | Se armó la solicitud tratando el reseteo como si fuera una baja | **Resetear no es dar de baja** (RN-02012): la cuenta conserva su habilitación, su papel, su identidad y **todos** sus trabajos con sus estados y comentarios. La operación que sí los elimina es la baja de CU-02002, y es irreversible. Ver §1.5 |
| `EMPTY_DERIVED_VALUE` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó con un valor sin contenido | Aportar el valor de la contraseña provisoria **ya derivado**. El dominio no deriva la contraseña y **nunca conoce la provisoria en claro**, que es el valor que el administrador le comunica al alumno por fuera del producto. Entrada única en §3.3; ésta es su segunda declaración |

### 3.3 `GeometriaFactory-Application`

Treinta y seis condiciones, derivadas una por una de la §6 de los once casos de uso. **El número no cambió con la emisión 1.6 y la composición sí**: entró `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` en CU-04002 y salió `CREDENTIAL_NOT_SET`, que declaraban CU-04003 y CU-04011. Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación mecánica están en §7.

**Diez** condiciones se declaran en más de un caso de uso. **Nueve conservan la misma causa en todos** y llevan una sola entrada, en el caso de uso donde aparecen primero, con la nota de sus apariciones restantes. La undécima, `INITIAL_STATUS_NOT_NEGOTIABLE`, lleva **fila completa en §3.1 y en §3.10** porque sus dos causas son opuestas según el camino de alta: el motivo está en §1.4. Es la única fila excedente del catálogo: 37 filas de tabla para 36 condiciones.

### 3.1 CU-04001 Registrar el alta de una cuenta

Es el **auto-registro del alumno**, uno de los dos caminos de alta (§1.4). Forma de terminación: negativa sin escritura. En los cinco casos no se constituye ninguna cuenta y la unidad de trabajo no se abre.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `EMAIL_ALREADY_REGISTERED` | Conflicto de estado | El correo aportado ya pertenece a una cuenta | La consulta de unicidad lo encontró ocupado, o el puerto de repositorio rechazó la materialización por una colisión que esa consulta no vio (CU-04001 FA-02) | Informar que el correo está ocupado y **no informar el estado ni el papel de la cuenta que lo ocupa**. La verificación previa no es una garantía por sí sola: la unicidad efectiva la sostiene también la capa que guarda, y por eso este motivo llega por dos caminos (RN-04002). Esta condición vuelve a declararse en CU-04010, con la misma causa |
| `REQUIRED_FIELD_MISSING` | Entrada inválida | Falta un dato obligatorio del alta: correo, nombre o apellido | El dominio rechazó la constitución porque uno de los tres llegó vacío | Completar el dato antes de invocar. Esta capa **propaga el motivo del dominio sin traducirlo**: no lo infiere ni lo deja en blanco. Esta condición vuelve a declararse en CU-04004, sobre el nombre y la fecha del trabajo, y en CU-04010, con la misma causa que acá |
| `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION` | Entrada inválida | El **auto-registro** no admite credencial derivada | El consumidor aportó una credencial derivada junto con los datos del auto-registro | Registrar sin credencial: en este camino se fija recién en el primer ingreso efectivo, por CU-04003. **En la configuración del administrador la credencial sí se aporta**, y eso es CU-04010: el motivo está acotado a este camino (§1.4) |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta del auto-registro en un estado distinto de `Pendiente` | Invocar sin pedir estado: lo fija el dominio. Toda cuenta de alumno nace `Pendiente` y sólo el administrador la habilita, con acto explícito (CU-04002). **Mismo motivo, causa opuesta en CU-04010**, donde el estado impuesto es `Habilitado`: ver §3.10 y §1.4 |
| `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | Entrada inválida | El auto-registro no constituye cuentas con papel `Administrador` | Se pidió constituir un administrador por la vía del alumno | Usar CU-04010, que es el camino declarado para la configuración del administrador. Constituirlo acá lo dejaría `Pendiente` y sin salida, porque ninguna otra cuenta podría habilitarlo (§1.4). **Sobre su categoría**, que diverge de la del proyecto de código hermano, ver §2.1 |

### 3.2 CU-04002 Gobernar las cuentas de la comisión

Forma de terminación: negativa sin escritura. Ninguna deja efecto parcial: la baja escribe todo o no escribe nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ADMINISTRATOR_ROLE_REQUIRED` | Conflicto de facultad | La operación requiere el papel `Administrador` | El papel declarado por quien pide no es `Administrador` | Comprobar el papel antes de invocar. **Es una negativa por facultad y no por pertenencia**: la existencia de la cuenta destino no se oculta, porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que no tiene (§2.4). El caso de uso no recupera ni modifica nada. Esta condición vuelve a declararse en CU-04007 y en CU-04008 |
| `DELETION_CONFIRMATION_MISMATCH` | Entrada inválida | El correo escrito como confirmación no es el de la cuenta destino | Se solicitó la baja con un correo de confirmación distinto | Volver a pedirle al administrador que escriba el correo exacto de la cuenta. La confirmación escrita es exigencia de RN-04007 y protege la única operación destructiva del producto: no se retira ningún trabajo ni la cuenta, y la unidad de trabajo no se abre |
| `ACCOUNT_TRANSITION_NOT_ALLOWED` | Conflicto de estado | La transición pedida no está admitida desde el estado actual de la cuenta | El dominio rechazó el par estado actual y transición | Encadenar las transiciones declaradas por la máquina de estados de la cuenta, que vive en `GeometriaFactory-Domain`. Esta capa **propaga el motivo y conserva el estado actual**: no infiere transiciones intermedias |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | Conflicto de facultad | La cuenta con papel `Administrador` no se da de baja | Se pidió dar de baja al administrador de la instancia | No hay camino: la instancia quedaría sin administrador (RN-04001) y su alta ya no puede repetirse, porque la ventana se cerró con la primera configuración (§1.4). Esta capa propaga el rechazo del dominio |
| `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` | Entrada inválida | Habilitar o rehabilitar exige la credencial derivada provisoria | El puerto de producción de la provisoria o el de derivación no entregaron el valor, y la transición se invocó sin él | Producir la provisoria, derivarla y aportarla en la misma invocación. Desde **RN-04016** fijar la credencial y poner la marca son efectos del mismo acto que habilitar, y admitir la transición sin credencial dejaría la cuenta `Habilitado` sin nada con que autenticarse |
| `ACCOUNT_NOT_FOUND` | Recurso ausente | No hay ninguna cuenta con el identificador o el correo pedido | El puerto de repositorio de cuentas no la encontró | Verificar el dato con el que se invocó. **Acá no oculta nada**, porque la operación ya exigió la facultad de administrador y el administrador gobierna todas las cuentas de la comisión. Esta condición vuelve a declararse en CU-04003, donde su tratamiento hacia afuera **sí** es distinto: ver §3.3 |

### 3.3 CU-04003 Resolver el ingreso y la credencial del alumno

Dos formas conviven: la consulta de admisibilidad es **motivo de resultado** —siempre devuelve un resultado, y el motivo explica por qué no es admisible— y las operaciones sobre la credencial son negativas sin escritura, que dejan la cuenta exactamente como estaba.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ACCOUNT_PENDING` | Conflicto de estado | La cuenta está registrada y todavía no fue habilitada | El estado de cuenta es `Pendiente` (RN-04006) | **No es un fallo y no se responde con un rechazo genérico**: informar con todas las letras que la cuenta espera la habilitación del administrador, que es lo que el producto promete al alumno sin canal de correo. No emitir acceso |
| `ACCOUNT_BLOCKED` | Conflicto de estado | La cuenta está bloqueada | El estado de cuenta es `Bloqueado` (RN-04006) | Informar el motivo y no emitir acceso. La rehabilitación es un acto explícito del administrador, por CU-04002. Una cuenta bloqueada conserva sus trabajos: la baja es la única operación destructiva |
| `ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` | Conflicto de estado | La credencial derivada sólo se fija o se reemplaza con la cuenta habilitada | Se intentó fijar o reemplazar sobre una cuenta `Pendiente` o `Bloqueado` | Habilitar o rehabilitar la cuenta primero, por CU-04002. Esta capa propaga el rechazo del dominio y conserva la credencial como estaba |
| `CURRENT_CREDENTIAL_NOT_VERIFIED` | Entrada inválida | El reemplazo exige declarar verificada la credencial vigente | Se pidió el reemplazo sin esa declaración | Verificar la credencial vigente en la capa que sí puede compararla —`GeometriaFactory-Infrastructure`— y **declararlo al invocar**. Esta capa no compara credenciales: exige que la verificación se declare, que es la forma en que la regla se hace exigible sin conocer el mecanismo |
| `CREDENTIAL_ALREADY_SET` | Conflicto de estado | La credencial derivada ya tiene valor | Se pidió fijar por primera vez algo que ya está fijado | Usar el camino de reemplazo, declarando verificada la credencial vigente. El valor anterior se reemplaza y no se conserva historial. Es el motivo que recibe siempre la cuenta del administrador si se intenta fijarle credencial, porque nace con una |
| `EMPTY_DERIVED_VALUE` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó la fijación o el reemplazo con un valor sin contenido | Aportar el valor **ya derivado**. Esta capa no deriva la contraseña y nunca la conoce en claro; conserva la credencial como estaba. Esta condición vuelve a declararse en CU-04011, con la misma causa: allá el valor vacío es el de la contraseña provisoria |
| `PASSWORD_CHANGE_PENDING` | Conflicto de estado | La cuenta tiene que cambiar su contraseña antes de hacer cualquier otra cosa | La cuenta fue reseteada por el administrador en CU-04011 y todavía no cambió la provisoria (RN-04013, INV-09) | **No es un fallo y no se responde con un rechazo genérico**: encaminar al cambio de contraseña, que es la única ruta disponible para esa cuenta. **Es la cuarta comprobación transversal de `Especificacion-Funcional.md` §4** y por lo tanto la puede devolver cualquier caso de uso; su entrada vive acá porque acá está su única excepción, el reemplazo de FA-05, que es lo que la levanta. No hay dato que corregir ni papel que cambiar |

**La cuenta inexistente en la consulta de admisibilidad.** `ACCOUNT_NOT_FOUND` tiene su entrada única en §3.2, pero su tratamiento acá es distinto y es una de las reglas de ocultamiento del producto: cuando el puerto de repositorio no encuentra el correo, el caso de uso devuelve **no admisible sin distinguir el motivo hacia afuera**, para no revelar qué correos están registrados (CU-04003 §6, CA-05). Es el mismo criterio con el que un trabajo ajeno es indistinguible de uno inexistente, aplicado a la cuenta.

### 3.4 CU-04004 Cargar y reeditar un trabajo propio

Forma de terminación: negativa sin escritura. Ninguna deja escritura parcial: la unidad de trabajo se abre recién al materializar.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `WORK_NOT_FOUND_FOR_REQUESTER` | Recurso ausente | El trabajo no existe para quien lo pide | El solicitante no es el dueño del trabajo, **o el identificador no existe** | Traducirlo a «no encontrado» y **nunca** a «no autorizado». Los dos casos comparten motivo por diseño: es lo que impide averiguar por tanteo qué identificadores existen (RN-04003). Ver §2.4, incluida la tabla de traducciones prohibidas. Esta condición vuelve a declararse en CU-04005, CU-04006 y CU-04009 |
| `OPERATION_OUTSIDE_DRAFT` | Conflicto de estado | El dueño no reedita ni elimina un trabajo fuera de `Borrador` | Se pidió reeditar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado` | Informar la acotación al borrador (RN-04004). **Es un motivo distinto del anterior porque acá la existencia del trabajo ya está admitida para su dueño**: quien pregunta es el dueño y no hay nada que ocultarle. **Ver** un trabajo propio sí procede en los cuatro estados. Esta condición vuelve a declararse en CU-04009, sobre la eliminación, y es la misma negativa que el dominio llama `EDIT_OUTSIDE_DRAFT` (§2.5) |
| `ORIGINAL_JSON_ALTERED` | Entrada inválida | El texto original no admite versiones corregidas | El consumidor aportó como texto original una versión corregida del que pegó el alumno | Conservar el texto tal como el alumno lo pegó (RN-04008). El producto no edita el dato del alumno, y es justamente lo que hace posible reprocesar el mismo trabajo cuando el validador mejora. La reedición cambia los datos del trabajo y el texto que el alumno **vuelve a pegar**, nunca el texto ya guardado |
| `WORK_WITHOUT_OWNER` | Entrada inválida | El trabajo no trae la identidad del alumno solicitante | El consumidor invocó la carga sin declarar quién la pide | Aportar la identidad del solicitante, que el consumidor ya autenticó. **Un trabajo sin dueño no es un trabajo**, y la pertenencia es lo único que después va a acotar quién lo ve |

**El dato obligatorio ausente en la carga.** `REQUIRED_FIELD_MISSING` tiene su entrada única en §3.1 y vuelve a declararse acá con otro alcance: lo que falta es **el nombre o la fecha del trabajo**, y la fecha en cuestión es la que **declara el alumno**, no un sello del reloj. Esta capa propaga el rechazo del dominio y no materializa nada.

### 3.5 CU-04005 Enviar un trabajo e interpretar su texto

Conviven las tres formas de terminación, y es el único caso de uso donde eso pasa. **Ninguna condición modifica el texto original**, ni siquiera cuando la interpretación falla.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `SUBMISSION_OUTSIDE_DRAFT` | Conflicto de estado | Sólo se envía un trabajo en `Borrador` | Se pidió enviar un trabajo en **estado `Pendiente`** | No reenviar. Esta capa propaga el rechazo del dominio y conserva el estado actual. Enviar es la **única acción de guardado** del alumno, y un trabajo que ya salió de sus manos no vuelve al envío. **El motivo está acotado al estado `Pendiente`**: los dos estados de cierre devuelven el de la fila siguiente |
| `TRANSITION_FROM_TERMINAL_STATUS` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió enviar un trabajo que ya tuvo desenlace | No hay camino de vuelta: los dos estados de cierre no cambian de estado ni de contenido (RN-04010). **Corregir un rechazo significa cargar un trabajo nuevo.** El dominio devuelve **este** motivo y no el anterior para los dos estados de cierre, y no los distingue entre sí; el criterio CA-07 lo ancla. Esta condición vuelve a declararse en CU-04008, sobre un desenlace nuevo |
| `PARSE_RESULT_UNAVAILABLE` | Error transitorio | El puerto de validación de figuras no pudo completar la interpretación | El adaptador que implementa el puerto no respondió o no pudo resolver | Informar que la interpretación no está disponible y **no presentar el trabajo como interpretado**. El caso de uso termina de forma controlada: el trabajo queda en `Borrador` con su texto intacto y se devuelve el estado degradado. **No se inventan observaciones y no se pasa a estado `Pendiente`.** Esta capa no reintenta: si corresponde reintentar, lo decide el consumidor |
| `MALFORMED_PIECE_SET` | Error interno | El conjunto de piezas que devolvió el validador no es adoptable | El dominio lo rechazó por posición inválida —repetida, negativa o fuera del rango declarado—, tipo de pieza desconocido, familia que contradice al tipo, o reconstrucción sobre un trabajo terminal | **Corregir el adaptador del puerto de validación, no la invocación.** Es una **condición agregada** que reúne cuatro rechazos del dominio (§2.5); el motivo fino está en la 02 del dominio. El caso de uso no materializa nada. Atención a la causa más frecuente: **la posición se valida contra la cantidad de figuras del conjunto raíz**, que el validador declara, y no contra la cantidad de piezas adoptadas |
| `MALFORMED_OBSERVATION` | Error interno | El conjunto de observaciones que devolvió el validador no es adoptable | El dominio lo rechazó por especie desconocida, error sin ubicación, advertencia sin los dos valores u observación sobre una posición inexistente | **Corregir el adaptador del puerto de validación, no la invocación.** Es la condición agregada **simétrica** a la anterior, y reúne otros cuatro rechazos del dominio (§2.5). Un conjunto mal formado es un defecto del validador y no un resultado que el alumno deba ver. **Una posición reservada no es una posición inexistente**: la de una figura que no se pudo reconstruir sí pertenece al rango declarado y sí admite observación |

**La negativa por pertenencia en el envío.** `WORK_NOT_FOUND_FOR_REQUESTER` tiene su entrada única en §3.4 y vuelve a declararse acá con una precisión propia que conviene no perder: cuando el solicitante no es el dueño, el caso de uso devuelve el motivo **sin invocar al validador**. El criterio de aceptación CA-05 lo verifica contando 0 invocaciones del validador doble, y es la prueba de que la comprobación ocurre antes y no después.

**El dato que hace comprobable todo lo demás.** El puerto de validación devuelve, además de las piezas y las observaciones, **la cantidad de figuras del conjunto raíz**, incluidas las que no se pudieron reconstruir, y este caso de uso la hace viajar hasta el dominio (CU-04005 §4 pasos 3 y 4). **No es derivable de las piezas adoptadas**, porque ésas admiten huecos, y sin ella el dominio no tiene rango contra el cual validar la posición de una observación. Las dos condiciones agregadas de este caso de uso dependen de ese rango: sin él, `MALFORMED_OBSERVATION` no tendría contra qué evaluar «posición inexistente» y la posición reservada de una figura no reconstruida dejaría de ser comprobable.

### 3.6 CU-04006 Consultar los trabajos propios del alumno

Forma de terminación: motivo de resultado. Las dos son consultas y no modifican nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `REQUESTER_NOT_DECLARED` | Entrada inválida | La consulta no trae la identidad del alumno solicitante | El consumidor pidió el listado o el detalle sin declarar quién lo pide | Aportar la identidad del solicitante, ya autenticada por la capa externa. El caso de uso **termina sin consultar el repositorio de trabajos**: un listado sin dueño declarado sería el listado de todos, y ése es exactamente el resultado que la separación entre alumnos viene a impedir |

**La negativa por pertenencia en la consulta.** `WORK_NOT_FOUND_FOR_REQUESTER`, con entrada única en §3.4, es acá donde su traducción queda declarada con más precisión: el consumidor la traduce a «no encontrado» y **nunca** a «no autorizado», porque confirmar que el recurso existe pero es ajeno ya sería informar de más (CU-04006 §6). CA-03 exige que el motivo sea el mismo que devolvería para un identificador inexistente.

**Un listado vacío no es una condición de error.** El alumno sin ningún trabajo recibe 0 trabajos y ningún motivo (CU-04006 FA-03, CA-05). Tratarlo como error es un defecto del consumidor.

### 3.7 CU-04007 Revisar los trabajos de la comisión

Forma de terminación: motivo de resultado. Las tres son consultas y no modifican nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | Conflicto de alcance | El trabajo está en `Borrador` y no entra en el alcance del administrador | Se pidió el detalle de un borrador | Excluirlo de la vista de revisión. **A diferencia de la negativa por pertenencia, ésta no oculta la existencia del trabajo**: expresa que está fuera de su flujo de trabajo (RN-04011). El recorte se traslada al puerto y no se aplica después sobre un conjunto mayor, de modo que en el listado el borrador ni siquiera aparece ni se cuenta. Esta condición vuelve a declararse en CU-04008 y en CU-04009 |
| `WORK_NOT_FOUND` | Recurso ausente | El identificador no corresponde a ningún trabajo | El identificador pedido no existe | Verificar el identificador. **Acá no hay recurso ajeno que proteger**: el administrador ve todo lo que no es borrador, y por eso este motivo es distinto del de pertenencia y no lo reemplaza. Comparar con `WORK_NOT_FOUND_FOR_REQUESTER`, que sí oculta, es la mejor forma de entender §2.4 |

**La negativa por facultad en la revisión.** `ADMINISTRATOR_ROLE_REQUIRED`, con entrada única en §3.2, se declara acá con una precisión propia: el caso de uso **no consulta el repositorio de trabajos** cuando el papel no es `Administrador`, y CA-03 lo verifica contando 0 consultas. La consulta del alumno sobre sus propios trabajos es CU-04006, y encaminar hacia allí es lo que corresponde.

### 3.8 CU-04008 Dar desenlace a un trabajo

Forma de terminación: negativa sin escritura. En los cinco casos el trabajo queda exactamente como estaba, con su estado y su comentario anteriores.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `OUTCOME_OUTSIDE_SUBMITTED` | Conflicto de estado | Sólo se aprueba o se rechaza un trabajo en estado `Pendiente` | El trabajo está en otro estado | Esperar a que el trabajo sea enviado y su texto verifique. Esta capa propaga el rechazo del dominio y conserva el estado actual. Un trabajo en estado `Pendiente` es, por RN-04005, uno cuyo texto no trajo errores de validación: es la precondición de todo desenlace |
| `UNKNOWN_OUTCOME` | Entrada inválida | El desenlace pedido no es aprobar ni rechazar | Llegó un tercer valor | Usar uno de los dos. Aprobar lleva a `Finalizado` y rechazar a `Rechazado`; los dos admiten comentario opcional y los dos son terminales. El caso de uso termina sin tocar el trabajo |

**Las otras tres negativas de este caso de uso** tienen entrada única en otras secciones y se declaran acá con su precisión propia:

- `ADMINISTRATOR_ROLE_REQUIRED` (§3.2): **la facultad no se delega, ni siquiera sobre el trabajo propio.** Un alumno que intente aprobar su propio trabajo recibe esta negativa, y el caso de uso no recupera ni modifica el trabajo (CA-03). Se verifica acá y no en la pantalla: un alumno que fuerce la petición contra el servicio de datos tiene que ser rechazado igual.
- `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` (§3.7): un borrador no se aprueba ni se rechaza, **y el administrador ni siquiera lo ve** (CA-05). Este caso de uso comprueba el alcance **antes** que el desenlace, y por eso devuelve este motivo y no el de estado.
- `TRANSITION_FROM_TERMINAL_STATUS` (§3.5): el trabajo ya tuvo desenlace. No se corrige una aprobación ni se revisa un rechazo; lo único que un trabajo terminal admite es que el administrador lo elimine, por CU-04009.

### 3.9 CU-04009 Eliminar un trabajo

Forma de terminación: negativa sin escritura. Ninguna deja el trabajo a medio retirar: o se va entero con sus piezas y sus observaciones, o no se toca.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `UNRECOGNIZED_ROLE` | Entrada inválida | El papel declarado no pertenece al conjunto cerrado de dos valores | El consumidor declaró un papel distinto de `Alumno` o `Administrador` | Declarar uno de los dos. El caso de uso **termina sin evaluar ninguna de las dos resoluciones**, porque elegir la resolución por el papel es la única decisión propia de esta capa acá, y sin papel válido no hay resolución que elegir (RN-04001, papeles fijos) |

**Las tres negativas restantes de este caso de uso**, con entrada única en otras secciones, y que juntas explican por qué los dos alcances conviven en un solo contrato:

- `WORK_NOT_FOUND_FOR_REQUESTER` (§3.4): el alumno pide eliminar un trabajo ajeno, o un identificador que no existe. **No se retira nada** y el consumidor lo traduce a «no encontrado», nunca a «no autorizado». CA-03 exige que el motivo sea el mismo que para un identificador inexistente.
- `OPERATION_OUTSIDE_DRAFT` (§3.4): el alumno pide eliminar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado`. Es un motivo distinto del anterior porque acá la existencia ya está admitida para su dueño. **Un trabajo `Rechazado` queda como registro del intento**, y sólo el administrador puede quitarlo.
- `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` (§3.7): el administrador pide eliminar un borrador. Los borradores no forman parte de su flujo de trabajo, **ni para verlos ni para quitarlos**.

Los dos alcances son opuestos y por eso conviven: al alumno lo acotan la pertenencia y el borrador; al administrador lo acota exactamente lo contrario, todo menos el borrador.

### 3.10 CU-04010 Configurar la cuenta de administrador

Es la **configuración del administrador en el primer arranque**, el otro camino de alta (§1.4). Forma de terminación: negativa sin escritura. En los cinco casos no se constituye ninguna cuenta y la instancia sigue sin administrador, de modo que este mismo contrato vuelve a estar disponible.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | Conflicto de estado | Ya existe una cuenta con papel `Administrador` | Se pidió configurar un administrador habiendo uno | No hay camino: la instancia tiene exactamente uno y **la ventana de alta se cierra con la primera configuración y no vuelve a abrirse** (RN-04001). El caso de uso no consulta siquiera el correo. Es también el motivo que el dominio devuelve si la ausencia de administrador no se le declara |
| `SETUP_WITHOUT_CREDENTIAL` | Entrada inválida | La configuración del administrador exige credencial derivada | No se aportó credencial derivada, o el valor llegó vacío | Aportar la credencial **ya derivada**: la contraseña en claro no atraviesa esta capa. **Es lo opuesto al auto-registro**, donde la credencial está prohibida (§1.4): una cuenta de administrador sin credencial no podría entrar, y no hay ninguna otra cuenta que pudiera resolverlo |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta de administrador en un estado distinto de `Habilitado` | Invocar sin pedir estado: lo fija el dominio. **Mismo motivo, causa opuesta en CU-04001**, donde el estado impuesto es `Pendiente`: ver §3.1 y §1.4. Una cuenta de administrador `Pendiente` o `Bloqueado` dejaría a la instancia sin salida, porque no obtendría acceso y nadie podría habilitarla |

**Las dos negativas que este caso de uso comparte con el auto-registro**, con entrada única en §3.1 y la misma causa acá:

- `EMAIL_ALREADY_REGISTERED`: el correo del administrador ya pertenece a otra cuenta. No se constituye nada y **no se informa el papel ni el estado de la cuenta que lo ocupa**.
- `REQUIRED_FIELD_MISSING`: falta el correo, el nombre o el apellido. Esta capa propaga el motivo del dominio.

**Un criterio de este caso de uso que conviene conocer aunque no produzca ninguna condición.** CA-02 encadena la configuración con la consulta de admisibilidad de CU-04003 y exige que devuelva admisible **con 0 motivos**: el administrador entra inmediatamente después de configurarse. Es la prueba de que el primer arranque es recorrible de punta a punta, y el defecto que la partición en dos caminos de alta vino a cerrar.

### 3.11 CU-04011 Resetear la contraseña de un alumno

Es el **reseteo de contraseña por el administrador** (F-26 del `PRODUCT-INTAKE` 1.7). Forma de terminación: negativa sin escritura. Ninguna deja efecto parcial: el reseteo escribe credencial, marca y sello, o no escribe nada. **En ningún caso se retira un trabajo**: resetear no es dar de baja y no dispara RN-04007.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `RESET_LIMITED_TO_STUDENT_ACCOUNTS` | Conflicto de facultad | El reseteo se ejerce sobre cuentas con papel `Alumno` | Se pidió resetear la contraseña de la cuenta con papel `Administrador` | No hay camino por acá: el administrador administra su propia credencial por el reemplazo de CU-04003, declarando verificada la vigente. El acotamiento es una **decisión derivada** de esta capa, declarada con su fundamento en CU-04011 §10: un reseteo sobre sí mismo dejaría al único administrador confinado por INV-09, con la instancia sin gobierno y sin ninguna otra cuenta que pudiera resolverlo |

**Las tres negativas que este caso de uso comparte con otros**, con entrada única donde aparecen primero y la misma causa acá:

- `ADMINISTRATOR_ROLE_REQUIRED` (§3.2): quien pide el reseteo no tiene el papel. No se recupera la cuenta destino ni se toca ninguna credencial.
- `ACCOUNT_NOT_FOUND` (§3.2): el puerto no encuentra la cuenta destino. **Acá tampoco oculta nada**, por el mismo motivo que en CU-04002: la operación ya exigió la facultad de administrador.
- `EMPTY_DERIVED_VALUE` (§3.3): la contraseña provisoria llegó vacía. **Desde la emisión 1.6 también lo declara CU-04002**, en la habilitación, por la misma causa. Esta capa nunca la conoce en claro. **Desde que la provisoria la produce el sistema y no la escribe el administrador, esta condición ya no puede nacer de lo que escriba una persona**, sino de un defecto de quien la produce; se conserva catalogada igual, porque suponerla imposible es como se termina escribiendo una credencial vacía.

**Y dos negativas que este caso de uso dejó de declarar, escritas acá para que nadie las reponga.**

**`CREDENTIAL_NOT_SET` salió del catálogo entero con la emisión 1.6.** Figuraba acá sobre la cuenta destino que nunca había fijado credencial, y en §3.3 como motivo de resultado del primer ingreso. **RN-04016** (`PRODUCT-INTAKE` 1.13 §4.1) hace que habilitar produzca y fije la contraseña provisoria: ninguna cuenta llega a estar habilitada sin credencial, y el reseteo sobre una cuenta `Pendiente` sin credencial simplemente la fija. **No es un rechazo que se relaje: es una causa que dejó de existir**, y el identificador **no se recicla**. Quien busque el encaminamiento del primer ingreso encuentra `PASSWORD_CHANGE_PENDING` en §3.3.

**`ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` dejó de declararse acá con la emisión 1.2.** Figuraba en CU-04011 sobre la cuenta destino `Pendiente` o `Bloqueado`. **El Product Owner resolvió que el reseteo no exige que la cuenta esté habilitada** —es una operación sobre la credencial, no toca el estado de la cuenta, y el administrador resetea y habilita en el orden que quiera—, de modo que la condición **no se relajó ni se renombró: dejó de existir para este caso de uso**. Sigue vigente en CU-04003, donde la cuenta que fija o reemplaza **su propia** credencial sí tiene que estar habilitada, y su entrada de §3.3 no cambia.

**Un criterio de este caso de uso que conviene conocer aunque no produzca ninguna condición.** Este caso de uso **no invoca el reemplazo de credencial del dominio, sino su operación de reseteo**, que no exige que se declare verificada la credencial vigente y no exige estado `Habilitado`. La versión anterior de esta nota describía lo contrario —el reemplazo sostenido por la verificación de facultad en lugar de por una comparación de contraseñas—, y la corrección está en CU-04011 §10, para que nadie la lea como un atajo ni la reponga: el administrador no conoce la contraseña del alumno y no la conocerá, y lo que autoriza la operación de este lado sigue siendo la facultad.

### 3.4 `GeometriaFactory-Infrastructure`

**Diecisiete condiciones, derivadas una por una de la §6 de los diez casos de uso.** Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación mecánica están en §7.

**Una sola condición se declara en más de un caso de uso** —`STORE_UNAVAILABLE`, en CU-06003, CU-06004 y CU-06005, siempre con la misma causa—, lleva **una sola entrada** en la subsección donde aparece primero, y sus dos apariciones restantes se anotan ahí. **No hay ninguna fila excedente: 17 filas de tabla para 17 condiciones.**

### 3.1 CU-06001 Interpretar el texto original y reconstruir las piezas

Es el contrato de mayor riesgo del producto. **Ninguna de sus dos condiciones nace de que el alumno haya escrito mal el texto**: eso produce observaciones, que son resultados (§1.2).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ORIGINAL_JSON_MISSING` | Entrada inválida | Se pidió interpretar sin texto | La invocación llegó con texto nulo o vacío | Aportar el texto original. **No se confunde con el conjunto raíz vacío**, que sí es un texto, sí se interpreta y sí produce una observación (CU-06001 FA-03): acá el defecto es de la invocación y allá es del dato del alumno |
| `PARSE_RESULT_UNAVAILABLE` | Error transitorio | La interpretación no se pudo completar por una causa que no depende del texto | El adaptador no pudo resolver | Informar el estado degradado y **no presentar el trabajo como interpretado**. **No se inventan observaciones, no se devuelve un conjunto vacío como si fuera un resultado y no se informan figuras que no se contaron.** Es el único código de este catálogo con destinatario declarado aguas arriba: `GeometriaFactory-Application` `CU-06005` §6 lo espera por este puerto. **Esta capa no reintenta** |

**La confusión que este par previene.** Si un texto ilegible devolviera la segunda condición en lugar de una observación, el producto le diría al alumno que el servicio no está disponible cuando lo que pasa es que su programa emitió algo que no se puede leer. El criterio `CU-06001` CA-10 existe exactamente para eso y exige el resultado, no el código.

### 3.2 CU-06002 Verificar los valores declarados contra los derivados

Forma de terminación: negativa sin escritura. **Este contrato no emite errores de validación**: los emite CU-06001.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `PIECE_SET_NOT_REBUILT` | Conflicto de estado | Se pidió verificar los valores sin haber reconstruido las piezas | La orquestación del adaptador salteó la interpretación | Reconstruir primero, por CU-06001. **No se devuelve «0 advertencias»**: sería indistinguible de un trabajo verificado sin discrepancias, y convertiría un defecto de orquestación en un resultado creíble. **Es una decisión derivada de la categoría 02**, declarada como punto abierto en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §11 y en `CU-06002` §6 y §10: ninguna fuente enuncia esta condición |

### 3.3 CU-06003 Guardar y recuperar los trabajos

Forma de terminación: negativa sin escritura en las dos primeras, degradada en las dos últimas. Ninguna deja escritura parcial.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `QUERY_WITHOUT_DECLARED_SCOPE` | Entrada inválida | La consulta de listado llegó sin dueño y sin predicado de alcance | El consumidor no trasladó el recorte al pedido | Trasladar el recorte **antes** de pedir. Un listado sin recorte sería el listado de todos los trabajos de la comisión, que es lo que RN-06003 y RN-06011 vienen a impedir. **Esta capa no lo comprueba por autorización sino por integridad del pedido**: no sabe quién preguntó |
| `WRITE_REWRITES_ORIGINAL_JSON` | Entrada inválida | El texto original no admite ser reemplazado | Una materialización aportó, para un trabajo existente, un texto distinto del conservado | Conservar el texto tal como el alumno lo pegó (RN-06008, `RC-06001`). **Es la condición que hace exigible la regla en el único lugar donde el texto puede perderse.** La reedición cambia los datos del trabajo y el texto que el alumno **vuelve a pegar**, nunca el ya guardado |
| `CONCURRENT_WRITE_REJECTED` | Error transitorio | Otra operación tenía el almacén tomado para escribir | El motor **no admite escrituras concurrentes** y el backend opera como escritor único | Informar y **no reintentar acá**: si corresponde reintentar, lo decide el consumidor. La concurrencia real es baja porque el alcance es de aula, y el escritor único es una restricción **aceptada por escrito** a cambio de un despliegue sin servicio de base de datos aparte |
| `STORE_UNAVAILABLE` | Error transitorio | El almacén no está alcanzable | La ubicación configurada no responde, o el volumen persistente no está montado | Revisar el despliegue, no el código. **No hay réplica ni caché**: los datos no están disponibles hasta que el servidor vuelva, y la pieza pública lo declara como estado degradado. **El mensaje no incluye la ruta** (§1.4). Esta condición vuelve a declararse en CU-06004 y en CU-06005, con la misma causa |

### 3.4 CU-06004 Ejecutar el borrado físico y el arrastre de la baja

Forma de terminación: negativa sin escritura. **Ninguna deja retiro parcial**, y es la propiedad entera de este contrato.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `PARTIAL_DELETION_NOT_ALLOWED` | Entrada inválida | La baja de una cuenta retira todos sus trabajos o no ocurre | Se pidió la baja sin declarar el arrastre, o declarándolo sobre un subconjunto | Declarar el arrastre completo. Un arrastre parcial dejaría **trabajos sin dueño**, que es la forma más silenciosa de romper el modelo: nada falla y el listado del administrador sigue mostrándolos. El criterio con el que RN-06007 se verifica es que **no quede ningún trabajo del alumno dado de baja** |

**El almacén no disponible en el retiro.** `STORE_UNAVAILABLE` tiene su entrada en §3.3 y vuelve a declararse acá con la misma causa. Su precisión propia: **no retira nada**, de modo que una baja interrumpida deja la cuenta y sus trabajos enteros (CU-06004 CA-05).

### 3.5 CU-06005 Guardar y recuperar las cuentas de la comisión

Forma de terminación: negativa sin escritura en las dos primeras.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `EMAIL_ALREADY_REGISTERED` | Conflicto de estado | El correo aportado ya pertenece a una cuenta | La materialización colisionó con una cuenta existente | **No informar el estado ni el papel de la cuenta que ocupa el correo.** Es la **segunda línea** de la unicidad: la consulta previa del consumidor no es una garantía por sí sola, y `GeometriaFactory-Application` `CU-06001` **FA-02** ya declara ese camino: «el puerto de repositorio rechaza la materialización por una colisión de correo que la consulta no vio», con el mismo motivo. **El código se llama igual allá y acá, y no es casualidad**: es la misma regla verificada dos veces |
| `ADMINISTRATOR_UNIQUENESS_VIOLATED` | Conflicto de estado | La instancia admite una sola cuenta con papel `Administrador` | La materialización habría dejado dos | Usar el camino de configuración del administrador, que la capa de aplicación gobierna con su ventana de alta. **Acá se impide el resultado, no se explica el camino**: esta capa no conoce la ventana |

**El almacén no disponible en las cuentas.** `STORE_UNAVAILABLE`, con entrada en §3.3, vuelve a declararse acá con la misma causa y sin precisión propia.

### 3.6 CU-06006 Derivar la contraseña y verificar una credencial

Forma de terminación: negativa sin escritura. **Ninguna incluye en su respuesta la contraseña ni el valor derivado** (§1.4).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `PLAINTEXT_PASSWORD_MISSING` | Entrada inválida | Se pidió derivar o verificar sin contraseña | La invocación llegó con valor nulo o vacío | Aportar la contraseña. **No se deriva la cadena vacía**: produciría un valor derivado válido para una credencial que nadie eligió, y la capa de aplicación ya rechaza el valor derivado vacío del otro lado de la frontera |
| `UNREADABLE_PASSWORD_HASH` | Error interno | El valor derivado guardado no permite verificar | No lleva los parámetros con los que se produjo, o su forma no corresponde a la función anclada | **Corregir el dato guardado o el camino de migración de parámetros, no la invocación.** Y **no responder «no coincide»**: lo haría indistinguible de una contraseña equivocada, y la cuenta quedaría inaccesible sin que nadie supiera por qué. Es un defecto del almacén o de una migración, no de quien intenta entrar |

### 3.7 CU-06007 Producir la contraseña provisoria del reseteo

Forma de terminación: degradada. **Es el contrato con una sola condición, y es la más importante de este catálogo.**

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `RANDOMNESS_SOURCE_UNAVAILABLE` | Error transitorio | No se pudo producir una contraseña provisoria | La fuente de material impredecible del sistema no respondió | **Informar que el reseteo no se completó, y no completarlo.** Bajo ninguna circunstancia se compone el valor por otro medio: con un contador, con la fecha o con un dato de la cuenta, la provisoria queda **adivinable**, que es exactamente lo que RN-06014 prohíbe, y el reseteo parece haber funcionado. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa.** El camino declarado es volver a intentar el reseteo, que produce un valor nuevo |

**Por qué esta condición existe y no es paranoia.** El fundamento de la regla que sostiene es de uso: si la provisoria la escribiera el docente, terminaría siendo la misma clave para toda la comisión. Una provisoria producida por un contador reproduce ese defecto **sin que nadie lo haya decidido**.

### 3.8 CU-06008 Emitir el acceso firmado

Forma de terminación: negativa sin escritura. **Ninguna incluye la clave de firma ni la dirección de un servicio interno** (§1.4).

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `SIGNING_KEY_MISSING` | Recurso ausente | No hay clave de firma provista | El arranque no recibió el valor por variable de entorno ni por archivo montado | Proveerla en el despliegue. **No se genera una clave de reemplazo al vuelo y no se emite sin firmar**: un acceso sin firma verificable es peor que ningún acceso, porque el sistema seguiría funcionando y nadie lo notaría hasta que alguien lo falsifique. **El mensaje no dice de dónde se esperaba leerla** |
| `INCOMPLETE_CLAIMS` | Entrada inválida | El acceso exige identificador, correo, papel y expiración | Se pidió emitir sin alguno de los cuatro | Aportar los cuatro. **Ninguno se completa con un valor por defecto**: un acceso sin papel dejaría a las capas de adentro decidiendo sobre un dato que nadie declaró, y uno sin expiración no vencería nunca |

### 3.9 CU-06010 Preparar el almacén al arrancar

Forma de terminación: **arranque detenido**, en las dos. Es la única subsección del catálogo donde aparece esa forma.

| Código | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `MIGRATION_NOT_APPLICABLE` | Conflicto de estado | El esquema encontrado no corresponde al linaje de transformaciones conocido | El almacén está por delante del código, o su esquema divergió | Revisar el despliegue: restaurar el respaldo, o revisar la transformación. **No se aplica un esquema por aproximación y no se descarta el almacén**: el segundo atajo deja el servicio impecable y sin los trabajos de nadie. Causa frecuente: **una transformación ya fusionada que se editó** |
| `STORE_PATH_UNAVAILABLE` | Error transitorio | La ubicación configurada del almacén no admite escritura | El volumen persistente no está montado | Revisar el montaje del volumen. **No se cae hacia una ruta alternativa dentro de la imagen**: el servicio arrancaría, aceptaría trabajos de la comisión entera y los perdería en el siguiente reemplazo de versión. **El mensaje no incluye la ruta** (§1.4) |

## 4. Tono y voz

### 4.1 `GeometriaFactory-Api`

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir lo que no se pudo hacer, no juzgar a quien pidió | «El trabajo pedido no está disponible para el solicitante» | «No tenés permiso para ver eso» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «El estado del trabajo no habilita al solicitante a eliminarlo» | «Operación no permitida» |
| No decir de más donde el producto decide decir de menos | «La credencial presentada no habilita» | «La contraseña es incorrecta» |
| No prometer lo que esta capa no hace | «El servicio no puede atender» | «Reintentando automáticamente» |
| No exponer secretos, rutas ni el trabajo de una persona | «El servicio no puede atender» | «No se pudo abrir el archivo en `/data/...`» |
| No confundir el dato del alumno con un fallo | El envío responde con éxito y trae sus observaciones | «El JSON enviado es inválido» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |

Dos excepciones declaradas a la regla de calificación, que no son defectos: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente.

### 4.2 `GeometriaFactory-Domain`

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir la guarda, no juzgar a quien invocó | «La unicidad del correo no viene declarada como comprobada» | «Olvidaste verificar la unicidad» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «Sólo se envía un trabajo en `Borrador`» | «El registro está en un estado no editable» |
| Decir la acción en imperativo, del lado del consumidor | «Invocar CU-02006 y CU-02007 y recién después enviar» | «El sistema debería haber interpretado antes» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |
| Nombrar la marca con la palabra «marca» | «la marca de cambio de contraseña pendiente está puesta» | «la cuenta está pendiente» para nombrarla |
| No llamar baja al reseteo, ni al revés | «resetear la contraseña», «dar de baja la cuenta» | «resetear la cuenta», «borrar la contraseña» |
| No prometer lo que el dominio no hace | «No emitir acceso» | «Reintentar en unos segundos» |

Una excepción declarada a la regla de calificación: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen. `ACCOUNT_PENDING` se escribe así, y su enunciado en prosa sí califica. Es la excepción que `Glosario-Funcional.md` §3.3 ya declara, y calificarla sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica como defecto.

### 4.3 `GeometriaFactory-Application`

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir la comprobación, no juzgar a quien invocó | «La operación requiere el papel `Administrador`» | «Olvidaste comprobar el papel» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «Sólo se envía un trabajo en `Borrador`» | «El registro está en un estado no editable» |
| Decir la acción en imperativo, y del lado que corresponde | «Corregir el adaptador del puerto de validación» | «El sistema debería haber validado antes» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |
| No prometer lo que esta capa no hace | «Informar que la interpretación no está disponible» | «Reintentar en unos segundos» |
| No confesar la pertenencia | «No encontrado» | «No tenés permiso sobre ese trabajo» |
| Nombrar el camino de alta cuando la regla es opuesta en el otro | «El **auto-registro** no admite credencial derivada» | «El alta no admite credencial derivada» |

Dos excepciones declaradas a la regla de calificación de `Pendiente`, que no son defectos: **los nombres de los motivos son identificadores literales del contrato** y no se califican ni se traducen —`ACCOUNT_PENDING` se escribe así, y su enunciado en prosa sí califica—, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente. Es la excepción que `Glosario-Funcional.md` §3.3 ya declara, y calificarla sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

### 4.4 `GeometriaFactory-Infrastructure`

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir lo que no se pudo hacer, no juzgar a quien invocó | «El almacén no está alcanzable» | «Te olvidaste de montar el volumen» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «El correo aportado ya pertenece a una cuenta» | «Violación de restricción única» |
| Decir la acción en imperativo, **y del lado que corresponde** | «Revisar el montaje del volumen» | «Reintentar la operación» |
| No prometer lo que esta capa no hace | «Informar el estado degradado» | «Reintentando automáticamente» |
| No exponer secretos ni rutas | «No hay clave de firma provista» | «No se encontró la clave en `/run/secrets/...`» |
| No confundir el dato del alumno con un fallo | «Se pidió interpretar sin texto» | «El JSON del alumno es inválido» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |

Dos excepciones declaradas a la regla de calificación de `Pendiente`, que no son defectos: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente.

## 5. Localización

### 5.1 `GeometriaFactory-Api`

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los códigos del contrato son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son del ensamblado de contratos y renombrar uno rompe la compilación de los dos extremos, que es la señal más temprana posible.
2. **El texto que una persona lee no se compone acá.** Lo arma la pieza pública, y está sujeta a la prohibición de §1.4, que no es una recomendación de estilo sino RA-03.
3. **Un solo idioma en el producto v1**: español rioplatense. **Con una excepción de hecho que conviene declarar**: el texto del alumno puede traer el separador decimal de la cultura de su máquina —una coma en lugar de un punto—, y **eso no es un problema de localización de esta capa**. Qué hace el producto con él está declarado desde el `PRODUCT-INTAKE` **1.12** §20.E-8 punto 5: es **error de validación**, con índice de figura y campo, y el trabajo **queda en `Borrador`** —de modo que, en esta superficie, **ese envío responde con éxito**—.

### 5.2 `GeometriaFactory-Domain`

**El dominio no localiza nada.** Política, en tres reglas:

1. **Los códigos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno es un cambio incompatible para los consumidores y rompe su compilación, que es la señal más temprana posible (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application).
2. **El texto que una persona lee no se compone acá.** La traducción de un código a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra.
3. **Un solo idioma en el producto v1**: español rioplatense. No hay compromiso de traducción y no hay catálogo de recursos que mantener. Si alguna vez lo hubiera, viviría en la capa que compone el mensaje y no acá.

### 5.3 `GeometriaFactory-Application`

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los motivos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno es un cambio incompatible para los consumidores y rompe su compilación, que es la señal más temprana posible (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application). La §17 de cada caso de uso declara qué cambio sobre la enumeración es compatible: **agregar un motivo lo es si el consumidor tiene un camino por defecto**; quitar o resignificar uno, no.
2. **El texto que una persona lee no se compone acá.** La traducción de un motivo a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra. Esa traducción está sujeta a la tabla de traducciones prohibidas de §2.4, que no es una recomendación de estilo sino una regla del producto.
3. **Un solo idioma en el producto v1**: español rioplatense. No hay compromiso de traducción y no hay catálogo de recursos que mantener. Si alguna vez lo hubiera, viviría en la capa que compone el mensaje y no acá.

### 5.4 `GeometriaFactory-Infrastructure`

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los códigos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno rompe la compilación de quien lo consume, que es la señal más temprana posible. La §17 de cada caso de uso declara qué cambio es compatible.
2. **El texto que una persona lee no se compone acá.** La traducción a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra, y está sujeta a la prohibición de §1.4, que no es una recomendación de estilo sino RA-03, regla de nivel producto.
3. **Un solo idioma en el producto v1**: español rioplatense. **Con una excepción de hecho que conviene declarar**: el texto del alumno puede traer separadores decimales de su cultura —una coma en lugar de un punto—, y eso **no es un problema de localización de esta capa** sino un rasgo del dato de entrada. **Qué hace el validador con él está declarado** desde el `PRODUCT-INTAKE` **1.12**, §20.E-8 punto 5: es **error de validación**, con el índice de figura y el campo, y el trabajo **queda en `Borrador`**. Es el escenario `E-8`, y la categoría 02 lo lleva en `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y en `CU-06001` CA-12.

## 6. Cobertura y trazabilidad

### 6.1 `GeometriaFactory-Api`

### 6.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | **12** (CU-00001 a CU-00012) |
| Casos de uso **con** condiciones declaradas en su §6 | **9**. `CU-00010`, `CU-00011` y `CU-00012` no declaran ninguna respuesta de fallo de la superficie, y §6.2 explica por qué |
| Códigos del conjunto cerrado del ensamblado de contratos | **17** |
| Códigos **con** destino en esta superficie | **16** |
| Códigos **sin** destino, declarados | **1** — el que describe la ausencia de respuesta de esta pieza (§2.3) |
| Respuestas **sin** código del contrato, declaradas | **2** (§2.2) |
| **Entradas del catálogo** | **18** = 16 + 2 |
| Códigos inventados por esta categoría | **0** |
| Códigos del conjunto cerrado sin entrada en el catálogo | **0**, salvo el declarado sin destino |
| Resultados que **no** son fallos, reunidos en §1.2 | **2**, ninguno de ellos entrada de este catálogo |
| Huecos del conjunto cerrado declarados y elevados | **2**, los dos **cerrados** por `PRODUCT-INTAKE` **1.29** §17.4 P.3 (§2.4) |

Cuadre: **16 + 1 = 17** códigos del conjunto cerrado, y **16 + 2 = 18** entradas del catálogo. El conjunto cerrado vivo es el que publica la tabla de traducción de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5, con sus **diecisiete** filas —dieciséis con destino y una sin él—, y es contra ella que este recuento cuadra.

### 6.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla.

| Categoría del catálogo | Entradas | Códigos de respuesta |
| --- | --- | --- |
| Entrada inválida | 3 | `400` |
| Credencial no admitida | 2 | `401` |
| Situación de la cuenta | 2 | `403` |
| Facultad | 2 | `403` |
| Recurso no visible | 2 | `404` |
| Conflicto de estado | 6 | `409` |
| No clasificado | 1 | `500`, `503` |
| **Total** | **18** | **Diez códigos de respuesta distintos en toda la superficie**, contando los tres de éxito —`200`, `201`, `204`— que no son de este catálogo |

Las tres comprobaciones que cierran la verificación:

- **De contrato a catálogo.** Los **17** códigos del conjunto cerrado se recorrieron uno por uno: **16** tienen entrada y **1** está declarado sin destino. **0** quedaron sin tratar.
- **De catálogo a contrato.** Las **18** entradas se recorrieron en sentido inverso: **16** citan un código que pertenece al conjunto cerrado y **2** declaran explícitamente no llevar ninguno. **0 códigos inventados**.
- **Los tres casos de uso sin entrada, y su ausencia declarada.** `CU-00010` falla **antes de que exista ninguna petición que responder** —sus dos condiciones detienen la construcción y no producen respuesta—; `CU-00011` falla **deteniendo el arranque**, que tampoco produce respuesta, y su única respuesta de fallo es la del punto de salud, que es un `503` **cubierto por la entrada de no clasificado con su código**, `UNCLASSIFIED_ERROR`, y **no** una tercera respuesta sin código: las respuestas sin código son las **dos** de §2.2; `CU-00012` **no produce condiciones, las provoca**. Los tres se declaran para que su ausencia no se lea como cobertura faltante.

### 6.3 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** Este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin. El quick-start del proyecto de código es único, vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3 y se recorre guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md). Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | **Consumidor de la superficie**, que es quien lee este catálogo entero; implementador de la superficie; y **operador del despliegue** para las terminaciones degradadas ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las respuestas de fallo de los **quince** puntos de acceso, y las dos traducciones de `Definicion-Superficie-HTTP.md` §5 |
| CU origen | CU-00001 a CU-00012, §6 de cada uno. **CU-00010, CU-00011 y CU-00012 no declaran ninguna** |
| Reglas de negocio relevantes | RN-00001 a RN-00016. **Dos se rompen desde este catálogo**: RN-00003, si dos respuestas que deben ser indistinguibles dejan de serlo, y RN-00013, si un punto queda fuera de la guardia |
| Necesidades de negocio | NB-00001 a NB-00009, las nueve. La correspondencia está en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §7.1 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US de la traducción completa de los **dieciséis** códigos; US de las tres familias empobrecidas, **con la comparación de respuestas indistinguibles como criterio de aceptación**; US de la prohibición de §1.4, con inspección del registro del servidor |
| Tests previstos en 08 | **Una prueba por código del conjunto cerrado**, no una por punto de acceso; las comparaciones de respuestas indistinguibles de las tres familias; y una inspección de que ninguna respuesta ni traza contiene secretos, rutas o el texto del alumno |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | Ver [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §8, donde dos de las cuatro se declaran pertinentes y acotadas en lugar de no aplicables |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

### 6.2 `GeometriaFactory-Domain`

### 6.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso con sección de excepciones | 13 (CU-02001 a CU-02013) |
| Filas de condición declaradas en la §6 de los trece casos de uso | 50 |
| Condiciones declaradas en más de un caso de uso | 7: `REQUIRED_FIELD_MISSING` en 3 (CU-02001, CU-02005, CU-02012), y `EMAIL_UNIQUENESS_NOT_VERIFIED`, `INITIAL_STATUS_NOT_NEGOTIABLE`, `TRANSITION_FROM_TERMINAL_STATUS`, `UNKNOWN_OPERATION`, `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` (CU-02002, CU-02013) y `EMPTY_DERIVED_VALUE` (CU-02003, CU-02013) en 2 cada una |
| Filas excedentes por repetición | 8 |
| **Condiciones distintas catalogadas** | **42** (50 − 8) |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |

Verificación: cada entrada de §3 se lee contra la §6 del caso de uso que la titula, y no hay entrada de §3 que no esté ahí ni fila de esas §6 que falte acá.

**Cinco identificadores retirados**, que aparecen en la cadena y que **no son condiciones de este catálogo**. La constancia va en prosa y no en tabla, deliberadamente: una fila encabezada por un identificador la lee como condición viva cualquier recuento automático sobre las tablas de este documento, y el total daría **47** en lugar de **42**. **Tres se retiraron por renombre y dos por imposibilidad de su causa**, que es un motivo distinto y conviene no mezclarlo.

`REBUILD_ON_APPROVED_WORK` fue reemplazado por `REBUILD_ON_TERMINAL_WORK` en CU-02006 1.1, que lo amplió para alcanzar también a `Rechazado`. `NON_CONTIGUOUS_PIECE_POSITION` fue reemplazado por `INVALID_PIECE_POSITION` en CU-02006 1.1, corrección de la ronda r1: un hueco dejó de ser un defecto, porque la posición de una figura que no se pudo reconstruir queda reservada, y lo que se rechaza pasó a ser la posición repetida, negativa o fuera de rango. `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` fue reemplazado por `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` en CU-02002 1.2, corrección de la ronda r3, hallazgo H-01: cubría una sola de las cuatro operaciones y dejaba las otras tres sin guarda, de modo que nada impedía bloquear al administrador.

**Los otros dos no los reemplazó ningún identificador: dejó de ser posible su causa.** `CREDENTIAL_NOT_SET`, de CU-02004, describía la cuenta `Habilitado` sin credencial derivada, y `RESET_ON_UNSET_CREDENTIAL`, de CU-02013, el reseteo sobre una cuenta que nunca había fijado ninguna. **RN-02016** (`PRODUCT-INTAKE` 1.13 §4.1) hizo que habilitar produzca y fije la contraseña provisoria, de modo que ninguna cuenta de alumno llega a `Habilitado` sin credencial y el reseteo sobre una cuenta sin credencial simplemente la fija. **Ninguno de los dos se recicla**, y quien busque hoy el encaminamiento del primer ingreso encuentra `PASSWORD_CHANGE_PENDING` en §3.4.

Toda cita anterior de cualquiera de los tres resuelve al identificador que lo reemplaza. **Ninguno de los tres se recicla para otra condición**, para que una referencia vieja no resuelva en silencio a un código distinto del que nombraba. Los tres renombres **no alteran el recuento**: en cada caso la condición sigue siendo una sola, con nombre nuevo.

### 6.2 Tabla de cobertura

| Código | CU que lo declara | Regla de negocio | Invariante | Forma |
| --- | --- | --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | CU-02001, CU-02005, CU-02012 | — | — | Rechazo |
| `EMAIL_UNIQUENESS_NOT_VERIFIED` | CU-02001, CU-02012 | RN-02002 | INV-01 | Rechazo |
| `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION` | CU-02001 | — | — | Rechazo |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | CU-02001, CU-02012 | — | INV-08 | Rechazo |
| `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | CU-02001 | RN-02001 | INV-05, INV-08 | Rechazo |
| `ACCOUNT_TRANSITION_NOT_ALLOWED` | CU-02002 | — | — | Rechazo |
| `DELETION_WITHOUT_WORK_CASCADE` | CU-02002 | RN-02007 | — | Rechazo |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | CU-02002, CU-02013 | RN-02001 | INV-05, INV-08 | Rechazo |
| `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` | CU-02002 | RN-02016, RN-02014 | INV-09 | Rechazo |
| `ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` | CU-02003 | RN-02006 | INV-06 | Rechazo |
| `CREDENTIAL_ALREADY_SET` | CU-02003 | — | — | Rechazo |
| `CURRENT_CREDENTIAL_NOT_VERIFIED` | CU-02003 | — | — | Rechazo |
| `EMPTY_DERIVED_VALUE` | CU-02003, CU-02013 | — | — | Rechazo |
| `ACCOUNT_PENDING` | CU-02004 | RN-02006 | INV-06 | Motivo de resultado |
| `ACCOUNT_BLOCKED` | CU-02004 | RN-02006 | INV-06 | Motivo de resultado |
| `PASSWORD_CHANGE_PENDING` | CU-02004 | RN-02013, RN-02016 | INV-09 | Motivo de resultado |
| `WORK_WITHOUT_OWNER` | CU-02005 | RN-02003 | INV-02 | Rechazo |
| `EDIT_OUTSIDE_DRAFT` | CU-02005 | RN-02004 | INV-03, INV-07 | Rechazo |
| `ORIGINAL_JSON_ALTERED` | CU-02005 | RN-02008 | — | Rechazo |
| `INVALID_PIECE_POSITION` | CU-02006 | — | — | Rechazo |
| `UNKNOWN_PIECE_TYPE` | CU-02006 | RN-02009 | — | Rechazo parcial |
| `DECLARED_FAMILY_CONTRADICTS_TYPE` | CU-02006 | — | — | Rechazo |
| `REBUILD_ON_TERMINAL_WORK` | CU-02006 | RN-02010 | INV-07 | Rechazo |
| `UNKNOWN_OBSERVATION_KIND` | CU-02007 | RN-02005 | INV-04 | Rechazo |
| `ERROR_WITHOUT_LOCATION` | CU-02007 | RN-02009 | — | Rechazo |
| `WARNING_MISSING_BOTH_VALUES` | CU-02007 | — | — | Rechazo |
| `OBSERVATION_ON_MISSING_PIECE` | CU-02007 | RN-02009 | — | Rechazo |
| `SUBMISSION_OUTSIDE_DRAFT` | CU-02008 | RN-02005 | INV-04 | Rechazo |
| `TRANSITION_FROM_TERMINAL_STATUS` | CU-02008, CU-02010 | RN-02010 | INV-07 | Rechazo |
| `SUBMISSION_WITHOUT_PARSE_RESULT` | CU-02008 | RN-02005 | INV-04 | Rechazo |
| `OUTCOME_NOT_ALLOWED_BY_CONTRACT` | CU-02008 | RN-02010 | INV-07 | Rechazo |
| `WORK_NOT_FOUND_FOR_REQUESTER` | CU-02009 | RN-02003 | INV-02 | Motivo de resultado |
| `OPERATION_OUTSIDE_DRAFT` | CU-02009 | RN-02004 | INV-03 | Motivo de resultado |
| `UNKNOWN_OPERATION` | CU-02009, CU-02011 | — | — | Motivo de resultado |
| `OUTCOME_OUTSIDE_SUBMITTED` | CU-02010 | RN-02010, RN-02011 | INV-07 | Rechazo |
| `OUTCOME_REQUIRES_ADMINISTRATOR_ROLE` | CU-02010 | RN-02010, RN-02001 | INV-07, INV-05 | Rechazo |
| `UNKNOWN_OUTCOME` | CU-02010 | RN-02010 | — | Rechazo |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | CU-02011 | RN-02011, RN-02004 | — | Motivo de resultado |
| `SCOPE_REQUIRES_ADMINISTRATOR_ROLE` | CU-02011 | RN-02001, RN-02011 | INV-05 | Motivo de resultado |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | CU-02012 | RN-02001 | INV-05 | Rechazo |
| `SETUP_WITHOUT_CREDENTIAL` | CU-02012 | — | — | Rechazo |
| `RESET_WITH_WORK_CASCADE` | CU-02013 | RN-02012 | INV-09 | Rechazo |

Las **dieciséis** reglas quedan alcanzadas y los nueve invariantes vigentes también. Las columnas con guion no son un vacío a completar: hay condiciones que sostienen una precondición del contrato de uso sin que ninguna regla de negocio las enuncie por separado, como `CREDENTIAL_ALREADY_SET` o `UNKNOWN_OUTCOME`. Inventarles una regla sería el defecto contrario al que este catálogo evita.

Dos guiones tienen origen declarado fuera de las dieciséis reglas y conviene dejarlo escrito, porque la atribución equivocada sería fácil de reponer:

| Condición | Origen de la exigencia | Por qué **no** es RN-02009 |
| --- | --- | --- |
| `INVALID_PIECE_POSITION` | `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain punto 2: la identidad de la pieza es su posición en el conjunto raíz | RN-02009 gobierna la **ubicación de la observación**, no la identidad de la pieza. La distinción está declarada en CU-02006 §9 |
| `WARNING_MISSING_BOTH_VALUES` | `NB-00005` §5, tercer criterio de éxito: el 100 % de las advertencias se muestran con los dos valores expresados, el declarado y el derivado | El §3 de RN-02009 **excluye explícitamente de su ámbito** a las advertencias de discrepancia de valor, que llevan su propia exigencia. Ninguna de las dieciséis reglas la enuncia |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | El estado inicial de cada camino de alta, declarado por CU-02001 §4 paso 6 y por CU-02012 §4, sobre `Definicion-Modelo-De-Dominio.md` §5.1 | Ninguna de las dieciséis reglas enuncia con qué estado nace una cuenta, y por eso la columna de regla queda en guion. **La columna de invariante sí se llenó**: INV-08, ya adoptado, es exactamente esa condición. La cita de INV-05 como fundamento se retiró en 02: ese invariante habla de la **unicidad** del administrador y de su ventana de alta, no del estado con el que nace |

**Sobre la columna de invariante e INV-08.** La versión anterior de este catálogo anotaba INV-08 entre paréntesis y no lo contaba, porque `Definicion-Modelo-De-Dominio.md` §4.2 lo **proponía** como candidato no vigente. `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain lo **adoptó**, con el enunciado ampliado a todo el ciclo de vida: la cuenta con papel `Administrador` está **siempre** `Habilitado` y toda cuenta con papel `Alumno` nace `Pendiente`. En consecuencia, las tres filas que lo anotaban entre paréntesis pasan a declararlo como invariante vigente, y los invariantes alcanzados por el catálogo son **los nueve**, INV-01 a INV-09. El recorrido de la adopción queda registrado en `Definicion-Modelo-De-Dominio.md` §4.2. **INV-09 es el invariante nuevo del intake 1.7** y lo sostienen tres condiciones: el motivo `PASSWORD_CHANGE_PENDING` de CU-02004, que es donde el dominio ejerce la guarda; el rechazo `RESET_WITH_WORK_CASCADE` de CU-02013; y, desde el intake 1.13, `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` de CU-02002, que es la condición con la que **RN-02016** impide que una cuenta llegue a `Habilitado` sin credencial. El rechazo `RESET_ON_UNSET_CREDENTIAL`, que lo sostenía hasta la versión 1.4, quedó retirado por imposibilidad de su causa (§6.1).

### 6.3 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** El criterio de `Rules-UX-UI-DX.md` §6 pide un quick-start verificable en cada documento `dx-`; acá no corresponde porque este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control en §3.2, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables, que es exactamente lo que se desincroniza primero. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor del dominio e integrador de capa (`DX-Developer-Experience.md` §1.1) |
| Superficie pública que se documenta | Las 42 condiciones de error de los trece contratos de uso |
| CU origen | CU-02001 a CU-02013, §6 de cada uno |
| Reglas de negocio relevantes | RN-02001 a RN-02016; invariantes INV-01 a INV-09 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo de condiciones mantenido junto al código, US de traducción de código a respuesta en la capa que expone, US de la indistinguibilidad de `WORK_NOT_FOUND_FOR_REQUESTER`, US de los dos caminos de alta con su estado inicial propio |
| Tests previstos en 08 | Una prueba unitaria pura y sin dobles por condición, más una prueba de cobertura que verifique que ninguna condición del catálogo quedó sin ejercitar |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. Ninguna de las cuatro extensiones aplica a este proyecto de código |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

### 6.3 `GeometriaFactory-Application`

### 7.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | 11 (CU-04001 a CU-04011) |
| Filas de condición declaradas en la §6 de los once casos de uso | 54 |
| Condiciones declaradas en más de un caso de uso | 10 (`EMAIL_ALREADY_REGISTERED`, `REQUIRED_FIELD_MISSING`, `INITIAL_STATUS_NOT_NEGOTIABLE`, `ADMINISTRATOR_ROLE_REQUIRED`, `ACCOUNT_NOT_FOUND`, `WORK_NOT_FOUND_FOR_REQUESTER`, `OPERATION_OUTSIDE_DRAFT`, `TRANSITION_FROM_TERMINAL_STATUS`, `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` y `EMPTY_DERIVED_VALUE`, que desde la emisión 1.6 comparten **CU-04002, CU-04003 y CU-04011**). **`ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` dejó de ser compartida** en la 1.2 y vuelve a ser exclusiva de CU-04003; **`CREDENTIAL_NOT_SET` salió del catálogo** en la 1.6 |
| Reapariciones, sobre esas diez | 18 |
| **Condiciones distintas catalogadas** | **36** |
| Filas de tabla en §3 | 37. La única excedente es `INITIAL_STATUS_NOT_NEGOTIABLE`, con fila completa en §3.1 y §3.10 por causas opuestas (§1.4) |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |
| Rechazos del dominio sin condición propia acá, declarados en §2.5 | **18**, ninguno de ellos condición de este catálogo. Los dos últimos son los de `CU-02013`, que entraron con la orquestación del reseteo |

Cuadre: 36 + 18 = 54.

### 7.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones, caso de uso por caso de uso, y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla:

| CU | Filas en su §6 | Entradas nuevas en §3 | Condiciones ya catalogadas que reaparecen | Suma |
| --- | --- | --- | --- | --- |
| CU-04001 | 5 | 5 | 0 | 5 |
| CU-04002 | 7 | 6 | 1 (`EMPTY_DERIVED_VALUE`) | 7 |
| CU-04003 | 8 | 7 | 1 (`ACCOUNT_NOT_FOUND`) | 8 |
| CU-04004 | 5 | 4 | 1 (`REQUIRED_FIELD_MISSING`) | 5 |
| CU-04005 | 6 | 5 | 1 (`WORK_NOT_FOUND_FOR_REQUESTER`) | 6 |
| CU-04006 | 2 | 1 | 1 (`WORK_NOT_FOUND_FOR_REQUESTER`) | 2 |
| CU-04007 | 3 | 2 | 1 (`ADMINISTRATOR_ROLE_REQUIRED`) | 3 |
| CU-04008 | 5 | 2 | 3 (`ADMINISTRATOR_ROLE_REQUIRED`, `WORK_OUTSIDE_ADMINISTRATOR_SCOPE`, `TRANSITION_FROM_TERMINAL_STATUS`) | 5 |
| CU-04009 | 4 | 1 | 3 (`WORK_NOT_FOUND_FOR_REQUESTER`, `OPERATION_OUTSIDE_DRAFT`, `WORK_OUTSIDE_ADMINISTRATOR_SCOPE`) | 4 |
| CU-04010 | 5 | 2 | 3 (`EMAIL_ALREADY_REGISTERED`, `REQUIRED_FIELD_MISSING`, `INITIAL_STATUS_NOT_NEGOTIABLE`) | 5 |
| CU-04011 | 4 | 1 | 3 (`ADMINISTRATOR_ROLE_REQUIRED`, `ACCOUNT_NOT_FOUND`, `EMPTY_DERIVED_VALUE`) | 4 |
| **Total** | **54** | **36** | **18** | **54** |

`INITIAL_STATUS_NOT_NEGOTIABLE` se cuenta como entrada nueva en CU-04001 y como reaparición en CU-04010, igual que las otras ocho repetidas: **la segunda fila de tabla de §3.10 no altera el recuento de condiciones distintas**, sólo el de filas de tabla.

Las dos comprobaciones que cierran la verificación:

- **De caso de uso a catálogo.** Ninguna de las 54 filas quedó sin entrada: 36 dieron entrada nueva y 18 son reapariciones de una condición ya catalogada, cada una anotada con su caso de uso adicional.
- **De catálogo a caso de uso.** Ninguna de las 36 entradas de §3 existe sin una fila que la respalde en la §6 del caso de uso que la titula. **No hay ninguna condición inventada por esta categoría**, y en particular no se agregó ninguna a partir de los flujos alternativos: se recorrieron las **veintiuna citas de motivo** que aparecen en las §5 de los once casos de uso y todas corresponden a un motivo ya declarado en la §6 del mismo caso de uso. Tampoco se agregó ninguna a partir de §2.5: los dieciséis rechazos del dominio que esa sección enumera **no son condiciones de este catálogo** y no entran en ningún recuento.

Las apariciones adicionales no se catalogan dos veces, pero **sí llevan su precisión propia** cuando el caso de uso agrega una: la negativa por pertenencia que no invoca al validador (§3.5), la negativa por facultad que no consulta el repositorio de trabajos (§3.7), la facultad que no se delega ni sobre el trabajo propio y el alcance comprobado antes que el estado (§3.8), el tratamiento distinto de la cuenta inexistente en la consulta de admisibilidad (§3.3), el otro alcance del dato obligatorio ausente (§3.4), las dos negativas compartidas entre los dos caminos de alta (§3.10) y las cuatro que el reseteo comparte con CU-04002 y CU-04003, entre ellas `CREDENTIAL_NOT_SET`, que **cambia de forma de terminación** —motivo de resultado en CU-04003, negativa sin escritura en CU-04011— (§3.11).

### 7.3 Tabla de cobertura

| Motivo | CU que lo declara | Regla de negocio | Categoría | Forma de terminación |
| --- | --- | --- | --- | --- |
| `EMAIL_ALREADY_REGISTERED` | CU-04001, CU-04010 | RN-04002 | Conflicto de estado | Negativa sin escritura |
| `REQUIRED_FIELD_MISSING` | CU-04001, CU-04004, CU-04010 | — | Entrada inválida | Negativa sin escritura |
| `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION` | CU-04001 | — | Entrada inválida | Negativa sin escritura |
| `INITIAL_STATUS_NOT_NEGOTIABLE` | CU-04001, CU-04010 | — (causas opuestas, §1.4) | Entrada inválida | Negativa sin escritura |
| `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH` | CU-04001 | RN-04001 | Entrada inválida (§2.1) | Negativa sin escritura |
| `ADMINISTRATOR_ROLE_REQUIRED` | CU-04002, CU-04007, CU-04008, CU-04011 | RN-04001, RN-04010 | Conflicto de facultad | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `DELETION_CONFIRMATION_MISMATCH` | CU-04002 | RN-04007 | Entrada inválida | Negativa sin escritura |
| `ACCOUNT_TRANSITION_NOT_ALLOWED` | CU-04002 | — | Conflicto de estado | Negativa sin escritura |
| `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | CU-04002 | RN-04001 | Conflicto de facultad | Negativa sin escritura |
| `ACCOUNT_NOT_FOUND` | CU-04002, CU-04003, CU-04011 | — | Recurso ausente | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `ACCOUNT_PENDING` | CU-04003 | RN-04006 | Conflicto de estado | Motivo de resultado |
| `ACCOUNT_BLOCKED` | CU-04003 | RN-04006 | Conflicto de estado | Motivo de resultado |
| `ENABLE_WITHOUT_TEMPORARY_CREDENTIAL` | CU-04002 | RN-04016, RN-04014 | Entrada inválida | Negativa sin escritura |
| `ACCOUNT_NOT_ENABLED_FOR_CREDENTIAL` | CU-04003 | RN-04006 | Conflicto de estado | Negativa sin escritura |
| `PASSWORD_CHANGE_PENDING` | CU-04003, y **cualquiera** por la comprobación transversal de §4 | RN-04013, INV-09 | Conflicto de estado | Negativa sin escritura |
| `RESET_LIMITED_TO_STUDENT_ACCOUNTS` | CU-04011 | RN-04015, RN-04001 | Conflicto de facultad | Negativa sin escritura |
| `CURRENT_CREDENTIAL_NOT_VERIFIED` | CU-04003 | — | Entrada inválida | Negativa sin escritura |
| `CREDENTIAL_ALREADY_SET` | CU-04003 | — | Conflicto de estado | Negativa sin escritura |
| `EMPTY_DERIVED_VALUE` | CU-04003, CU-04011 | RN-04014 | Entrada inválida | Negativa sin escritura |
| `WORK_NOT_FOUND_FOR_REQUESTER` | CU-04004, CU-04005, CU-04006, CU-04009 | RN-04003 | Recurso ausente | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `OPERATION_OUTSIDE_DRAFT` | CU-04004, CU-04009 | RN-04004 | Conflicto de estado | Negativa sin escritura |
| `ORIGINAL_JSON_ALTERED` | CU-04004 | RN-04008 | Entrada inválida | Negativa sin escritura |
| `WORK_WITHOUT_OWNER` | CU-04004 | RN-04003 | Entrada inválida | Negativa sin escritura |
| `SUBMISSION_OUTSIDE_DRAFT` | CU-04005 | RN-04005 | Conflicto de estado | Negativa sin escritura |
| `TRANSITION_FROM_TERMINAL_STATUS` | CU-04005, CU-04008 | RN-04010 | Conflicto de estado | Negativa sin escritura |
| `PARSE_RESULT_UNAVAILABLE` | CU-04005 | RN-04008 | Error transitorio | Terminación degradada |
| `MALFORMED_PIECE_SET` | CU-04005 | RN-04009 | Error interno | Negativa sin escritura |
| `MALFORMED_OBSERVATION` | CU-04005 | RN-04009, RN-04005 | Error interno | Negativa sin escritura |
| `REQUESTER_NOT_DECLARED` | CU-04006 | RN-04003 | Entrada inválida | Motivo de resultado |
| `WORK_OUTSIDE_ADMINISTRATOR_SCOPE` | CU-04007, CU-04008, CU-04009 | RN-04011, RN-04004 | Conflicto de alcance | Motivo de resultado y negativa sin escritura, según el caso de uso |
| `WORK_NOT_FOUND` | CU-04007 | — | Recurso ausente | Motivo de resultado |
| `OUTCOME_OUTSIDE_SUBMITTED` | CU-04008 | RN-04010, RN-04005 | Conflicto de estado | Negativa sin escritura |
| `UNKNOWN_OUTCOME` | CU-04008 | RN-04010 | Entrada inválida | Negativa sin escritura |
| `UNRECOGNIZED_ROLE` | CU-04009 | RN-04001 | Entrada inválida | Negativa sin escritura |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | CU-04010 | RN-04001 | Conflicto de estado | Negativa sin escritura |
| `SETUP_WITHOUT_CREDENTIAL` | CU-04010 | RN-04006 | Entrada inválida | Negativa sin escritura |

Tres notas sobre las columnas, para que nadie las complete con atribuciones inventadas:

| Caso | Situación |
| --- | --- |
| `INITIAL_STATUS_NOT_NEGOTIABLE` sin regla de negocio | **Ninguna de las dieciséis reglas enuncia con qué estado nace una cuenta.** La atribución a RN-04001 se retiró aguas arriba: ese enunciado habla de la unicidad del administrador y de la ventana en la que su alta es posible, no del estado inicial. El origen está en el modelo de estados de cuenta del dominio y en los dos caminos de alta |
| RN-04008 sin condición que la haga cumplir por rechazo | Tiene una, `ORIGINAL_JSON_ALTERED`, desde la corrección de esta ronda. Su otra mitad sigue siendo un **comportamiento** y no una comprobación: el texto no se reescribe, ni siquiera cuando la interpretación falla, y `PARSE_RESULT_UNAVAILABLE` la cita como garantía y no como violación |
| Columnas con guion | No son un vacío a completar: hay condiciones que sostienen una precondición del contrato sin que ninguna regla de negocio las enuncie por separado, como `CURRENT_CREDENTIAL_NOT_VERIFIED`, `CREDENTIAL_ALREADY_SET` o `WORK_NOT_FOUND`. Inventarles una regla sería el defecto contrario al que este catálogo evita |

### 7.4 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** El criterio de `Rules-UX-UI-DX.md` §6 pide un quick-start verificable en cada documento `dx-`; acá no corresponde porque este artefacto es del modo **reference** y se consulta por motivo, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control en §3.2, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables, que es lo que se desincroniza primero. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Integrador por casos de uso, implementador de puertos y mantenedor de la capa ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las 36 condiciones de error de los once contratos de uso, y las cuatro comprobaciones transversales de `Especificacion-Funcional.md` §4 |
| CU origen | CU-04001 a CU-04011, §6 de cada uno |
| Reglas de negocio relevantes | RN-04001 a RN-04016 de `GeometriaFactory-Domain`, con la correspondencia de §7.3 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo mantenido junto al código; US de traducción de motivo a respuesta en `GeometriaFactory-Api`, con la tabla de traducciones prohibidas de §2.4 como criterio de aceptación; US de la indistinguibilidad de `WORK_NOT_FOUND_FOR_REQUESTER`; US del recorrido del primer arranque, que encadena CU-04010 con la admisibilidad de CU-04003 |
| Tests previstos en 08 | Una prueba unitaria con dobles por condición, **ninguna tocando la base de datos real**; dos pruebas de indistinguibilidad derivadas de CA-03 de CU-04006 y CA-03 de CU-04009; y la prueba de recorrido del primer arranque derivada de CA-02 de CU-04010 |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. Ninguna de las cuatro extensiones aplica a este proyecto de código. **La configuración del administrador de CU-04010 no es la extensión de primer arranque**: acá es un contrato de uso, y la superficie de aprovisionamiento, si la hubiera, viviría en la categoría 03 de la pieza pública |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

### 6.4 `GeometriaFactory-Infrastructure`

### 7.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | **10** (CU-06001 a CU-06010) |
| Casos de uso **con** condiciones declaradas | **9**. `CU-06009` no tiene ninguna, y su ausencia está declarada en §2.5 |
| Filas de condición declaradas en la §6 de los diez casos de uso | **19** |
| Condiciones declaradas en más de un caso de uso | **1** (`STORE_UNAVAILABLE`, en CU-06003, CU-06004 y CU-06005, siempre con la misma causa) |
| Reapariciones, sobre esa una | **2** |
| **Condiciones distintas catalogadas** | **17** |
| Filas de tabla en §3 | **17. Ninguna excedente**: no hay ningún código con causas opuestas según el camino |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |
| Resultados declarados que **no** son condiciones, reunidos en §1.2 | **7**, ninguno de ellos condición de este catálogo |

Cuadre: **17 + 2 = 19**.

### 7.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones, caso de uso por caso de uso, y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla:

| CU | Filas en su §6 | Entradas nuevas en §3 | Condiciones ya catalogadas que reaparecen | Suma |
| --- | --- | --- | --- | --- |
| CU-06001 | 2 | 2 | 0 | 2 |
| CU-06002 | 1 | 1 | 0 | 1 |
| CU-06003 | 4 | 4 | 0 | 4 |
| CU-06004 | 2 | 1 | 1 (`STORE_UNAVAILABLE`) | 2 |
| CU-06005 | 3 | 2 | 1 (`STORE_UNAVAILABLE`) | 3 |
| CU-06006 | 2 | 2 | 0 | 2 |
| CU-06007 | 1 | 1 | 0 | 1 |
| CU-06008 | 2 | 2 | 0 | 2 |
| **CU-06009** | **0** | **0** | **0** | **0** |
| CU-06010 | 2 | 2 | 0 | 2 |
| **Total** | **19** | **17** | **2** | **19** |

Las dos comprobaciones que cierran la verificación:

- **De caso de uso a catálogo.** Ninguna de las 19 filas quedó sin entrada: 17 dieron entrada nueva y 2 son reapariciones de una condición ya catalogada, anotadas con su caso de uso adicional.
- **De catálogo a caso de uso.** Ninguna de las 17 entradas de §3 existe sin una fila que la respalde en la §6 del caso de uso que la titula. **No hay ninguna condición inventada**, y en particular **no se agregó ninguna a partir de los flujos alternativos**: se recorrieron los flujos alternativos de los diez casos de uso y **ninguno cita un código**, porque todos terminan en un resultado. Los siete resultados que eso produce están reunidos en §1.2 y **no entran en ningún recuento**.

### 7.3 Tabla de cobertura

| Código | CU que lo declara | Regla de negocio | Categoría | Forma de terminación |
| --- | --- | --- | --- | --- |
| `ORIGINAL_JSON_MISSING` | CU-06001 | — | Entrada inválida | Negativa sin escritura |
| `PARSE_RESULT_UNAVAILABLE` | CU-06001 | RN-06008 (como garantía: el texto queda intacto) | Error transitorio | Terminación degradada |
| `PIECE_SET_NOT_REBUILT` | CU-06002 | — | Conflicto de estado | Negativa sin escritura |
| `QUERY_WITHOUT_DECLARED_SCOPE` | CU-06003 | RN-06003, RN-06011 | Entrada inválida | Negativa sin escritura |
| `WRITE_REWRITES_ORIGINAL_JSON` | CU-06003 | RN-06008 | Entrada inválida | Negativa sin escritura |
| `CONCURRENT_WRITE_REJECTED` | CU-06003 | — | Error transitorio | Terminación degradada |
| `STORE_UNAVAILABLE` | CU-06003, CU-06004, CU-06005 | — | Error transitorio | Terminación degradada |
| `PARTIAL_DELETION_NOT_ALLOWED` | CU-06004 | RN-06007, RN-06004 | Entrada inválida | Negativa sin escritura |
| `EMAIL_ALREADY_REGISTERED` | CU-06005 | RN-06002 | Conflicto de estado | Negativa sin escritura |
| `ADMINISTRATOR_UNIQUENESS_VIOLATED` | CU-06005 | RN-06001 | Conflicto de estado | Negativa sin escritura |
| `PLAINTEXT_PASSWORD_MISSING` | CU-06006 | — | Entrada inválida | Negativa sin escritura |
| `UNREADABLE_PASSWORD_HASH` | CU-06006 | — | Error interno | Negativa sin escritura |
| `RANDOMNESS_SOURCE_UNAVAILABLE` | CU-06007 | **RN-06014** | Error transitorio | Terminación degradada |
| `SIGNING_KEY_MISSING` | CU-06008 | — | Recurso ausente | Negativa sin escritura |
| `INCOMPLETE_CLAIMS` | CU-06008 | — | Entrada inválida | Negativa sin escritura |
| `MIGRATION_NOT_APPLICABLE` | CU-06010 | — | Conflicto de estado | Arranque detenido |
| `STORE_PATH_UNAVAILABLE` | CU-06010 | — | Error transitorio | Arranque detenido |

Tres notas sobre las columnas, para que nadie las complete con atribuciones inventadas:

| Caso | Situación |
| --- | --- |
| Columnas de regla con guion | **Diez de las diecisiete no tienen regla de negocio detrás, y es correcto.** Esta capa provee mecanismos, y un mecanismo tiene precondiciones que ninguna regla de negocio enuncia: que llegue el texto, que llegue la contraseña, que esté la clave. Inventarles una regla sería el defecto contrario al que este catálogo evita |
| `RN-06009` sin condición que la haga cumplir por rechazo | No la tiene, y no es un hueco: su tramo principal está en esta capa pero se ejerce **produciendo** el mensaje ubicado, no rechazando nada. Su verificación vive en `CU-06001` CA-04 |
| `RN-06014` con una sola condición | `RANDOMNESS_SOURCE_UNAVAILABLE` es lo único que esta capa **rechaza** por esa regla. Lo que la regla exige de verdad —las dos propiedades del valor— no se rechaza: **se produce**, y se verifica en `CU-06007` CA-01 a CA-04 |

### 7.4 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** Este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md). Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de adaptadores, mantenedor de la capa y **operador del despliegue**, que acá sí existe ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las 17 condiciones de error de los diez contratos, y la frontera entre mecanismo y decisión de `Especificacion-Funcional.md` §4 |
| CU origen | CU-06001 a CU-06010, §6 de cada uno. **`CU-06009` no declara ninguna** |
| Reglas de negocio relevantes | RN-06001 a RN-06016 de `GeometriaFactory-Domain`, con la correspondencia de §7.3. **Tres tienen su tramo principal en esta capa**: RN-06008, RN-06009 y RN-06014 |
| Necesidades de negocio | NB-00001 a NB-00009, las nueve. La correspondencia está en `Especificacion-Funcional.md` §7.1 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo mantenido junto al código; US de las tres condiciones que fallan hacia el lado seguro, **con el atajo prohibido como criterio de aceptación**; US de la prohibición de §1.4, con inspección del registro del servidor |
| Tests previstos en 08 | Una prueba por condición. Las de CU-06001, CU-06002, CU-06006, CU-06007 y CU-06008, **unitarias y sin almacén**; las de CU-06003, CU-06004, CU-06005 y CU-06010, de integración contra el almacén real. Y una inspección de que ningún mensaje contiene la clave de firma, una contraseña, una provisoria, la ruta del almacén ni el texto del alumno |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. **La preparación del almacén de CU-06010 no es la extensión de primer arranque**: acá es un contrato de uso, y la superficie de aprovisionamiento, si la hubiera, viviría en la categoría 03 de la pieza pública |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

## 7. Principios de redacción de errores

### 7.1 `GeometriaFactory-Domain`

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá una forma particular, y es la que le da sentido al catálogo entero:

> El diagnóstico accionable dice siempre **qué hacer del lado del consumidor**, porque el dominio no resuelve nada por su cuenta: no consulta, no reintenta, no completa el dato que falta y no corrige el dato del alumno.

Cuatro reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la guarda que se negó, no la torpeza de quien invocó.
2. **Nada genérico.** No hay «operación inválida» ni «error interno». Un rechazo dice qué guarda se negó. Es la misma exigencia que RN-02009 le impone al producto frente al alumno, aplicada frente al consumidor.
3. **Nada que la regla oculte se filtra.** `WORK_NOT_FOUND_FOR_REQUESTER` es deliberadamente indistinguible de la inexistencia (RN-02003, INV-02).
4. **Ningún código es un código de protocolo.** La traducción a respuesta pertenece a `GeometriaFactory-Api` (CU-02001 §6, CU-02004 §6).

### 1.2 Una condición de error no es una observación

Es la distinción que sostiene todo lo demás, y confundirla lleva a modelar mal dos cosas a la vez. Las tres nociones son distintas y ninguna es especie de otra:

| Noción | Qué es | Cuántas hay | Quién la produce | Se guarda |
| --- | --- | --- | --- | --- |
| **Condición de error del dominio** | Una guarda que impide una operación ilegítima del consumidor. Es lo que este catálogo enumera | Una por invocación rechazada, y no sobrevive a la invocación | El dominio, al negarse | No |
| **Observación** | Entidad del dominio con dos especies, advertencia y error de validación, que el producto emite **al interpretar el texto del alumno** y al verificar sus valores | Varias por trabajo, tantas como defectos | El validador de figuras, fuera del dominio; el dominio la adopta por CU-02007 | Sí, como entidad |
| **Comentario** | Texto libre y opcional que el administrador deja al aprobar o al rechazar | A lo sumo uno por trabajo | Una persona | Sí, como atributo del trabajo |

Consecuencia práctica: un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de error de este catálogo**. Es el resultado declarado del envío (CU-02008 FA-01), y traducirlo hacia afuera como fallo sería un defecto del consumidor.

En el sentido inverso: `ERROR_WITHOUT_LOCATION` y `WARNING_MISSING_BOTH_VALUES` **sí** son condiciones de este catálogo, aunque hablen de observaciones. Lo que rechazan no es la observación en sí: es un conjunto de observaciones mal formado que el consumidor intenta adoptar.

### 1.3 Qué emite el dominio y qué compone el consumidor

El dominio emite un **código**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no conoce ningún formato de serialización (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain) y no cruza ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Domain).

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que la capa que expone compone lo que una persona lee. No es una cadena que la biblioteca produzca ni un recurso que exista en el código.

### 1.4 Un mismo código con dos causas opuestas: los dos caminos de alta

Hay **dos caminos de alta** de una cuenta, cada uno con su caso de uso, su estado inicial y su tratamiento de la credencial, y **cada uno rechaza el del otro**:

| Camino | Caso de uso | Estado inicial | Credencial |
| --- | --- | --- | --- |
| Auto-registro del alumno | CU-02001 | `Pendiente` | No se aporta: se fija en el primer ingreso efectivo, por CU-02003 |
| Configuración del administrador en el primer arranque | CU-02012 | **`Habilitado`** | Se aporta en el mismo acto, ya derivada |

**La cuenta del administrador nace habilitada porque es la que habilita a las demás.** Ninguna cuenta anterior podría habilitarla a ella: si naciera `Pendiente`, por INV-06 no obtendría acceso y no habría nadie capaz de sacarla de ahí, de modo que la instancia quedaría inutilizable en el primer arranque. Esa generalización —un estado inicial uniforme para toda cuenta— es exactamente el defecto que la corrección del P0 resolvió, y es el error que un lector de este catálogo tiene que salir sin poder cometer.

**Consecuencia sobre el catálogo, y es la primera vez que ocurre en este proyecto de código.** El identificador `INITIAL_STATUS_NOT_NEGOTIABLE` aparece en los dos caminos con **causas opuestas**:

| Caso de uso | Qué rechaza | Estado que impone |
| --- | --- | --- |
| CU-02001, auto-registro | Constituir la cuenta en un estado **distinto de `Pendiente`** | `Pendiente` |
| CU-02012, configuración del administrador | Constituir la cuenta en un estado **distinto de `Habilitado`** | `Habilitado` |

No es una inconsistencia y no hay que unificarlo: el enunciado del código es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino. Por eso es el único código del catálogo que lleva **fila completa en dos subsecciones de §3** en lugar de una entrada única con nota, y las dos filas se leen juntas. Los otros cuatro códigos declarados en más de un caso de uso conservan la misma causa en todos y siguen con entrada única.

Dos códigos más existen sólo para que ninguno de los dos caminos se cuele por el otro: `ADMINISTRATOR_ROLE_OUTSIDE_THIS_PATH`, que impide constituir un administrador por el auto-registro, y `CREDENTIAL_NOT_ALLOWED_ON_REGISTRATION`, que quedó **acotado al auto-registro** porque en la configuración del administrador la credencial sí se aporta.

**El alta es sólo una de las dos puertas, y la otra es el ciclo de vida posterior.** La misma condición sin salida se alcanza sin tocar el alta: basta con **bloquear** la cuenta del administrador ya configurada. Una cuenta bloqueada no obtiene acceso por INV-06, y el único que puede desbloquearla es él mismo. Por eso las **cuatro** operaciones de CU-02002 —habilitar, bloquear, rehabilitar y dar de baja— alcanzan **sólo a las cuentas con papel `Alumno`**, que es el enunciado literal de la capacidad F-03, y sobre la cuenta de administrador ninguna procede: es lo que rechaza `OPERATION_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` (§3.2).

Y el efecto no se agota en el acceso, que es lo que dimensiona el problema: **sin administrador nadie aprueba ni rechaza**, así que todo trabajo enviado queda en estado `Pendiente` para siempre y **el circuito de revisión entero se detiene** (RN-02010, CU-02010). La instancia sigue aceptando entregas que ya nadie puede resolver.

### 1.5 Resetear no es dar de baja

Es la distinción más cara de este catálogo, porque las dos operaciones las ejerce la misma persona desde el mismo panel y **una de las dos es irreversible**.

| | Baja de la cuenta (CU-02002) | Reseteo de contraseña (CU-02013) |
| --- | --- | --- |
| Qué pasa con la cuenta | Deja de existir | Se conserva, con su estado, su papel y su identidad |
| Qué pasa con los trabajos | **Se eliminan todos**, en los cuatro estados y con sus comentarios (RN-02007) | **Se conservan todos**, con sus estados y sus comentarios (RN-02012) |
| Reversible | No | Sí: la cuenta cambia la provisoria y sigue operando |
| Exige confirmación escrita | Sí, el correo de la cuenta | No, y no es un olvido: la guarda protege de un accidente destructivo |
| Efecto sobre la marca | No aplica | La **pone**, y sólo el reemplazo de la propia cuenta la levanta (RN-02013, INV-09) |

Hasta `PRODUCT-INTAKE` 1.6 la baja era el único camino declarado ante una contraseña olvidada, y por eso el primer olvido costaba todos los trabajos del alumno. La capacidad **F-26** cierra ese agujero, retira la exclusión **X-2** y reescribe el caso límite **CL-7**. Un consumidor que resuelva un olvido de contraseña invocando CU-02002 en lugar de CU-02013 **no recibe ninguna condición de error de este catálogo**: la baja procede, y es correcta como operación. Es el mismo tipo de defecto silencioso que §1.2 describe para el trabajo que vuelve en `Borrador`, y por eso está declarado acá y no en una fila de tabla.

### 7.2 `GeometriaFactory-Application`

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá dos destinatarios, y distinguirlos es lo que la hace accionable:

> El diagnóstico dice **qué hacer del lado del consumidor** cuando la negativa nace de lo que el consumidor pidió, y **qué corregir del lado del adaptador del puerto** cuando nace de lo que un puerto devolvió. Confundirlos manda a corregir la capa equivocada.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la comprobación que se negó, no la torpeza de quien invocó.
2. **Nada genérico.** No hay «operación inválida» ni «error interno». Una negativa dice qué comprobación se negó. Es la misma exigencia que RN-04009 le impone al producto frente al alumno, aplicada frente al consumidor.
3. **Nada que la regla oculte se filtra.** `WORK_NOT_FOUND_FOR_REQUESTER` es deliberadamente indistinguible de la inexistencia (RN-04003), y la cuenta inexistente en la consulta de admisibilidad no se distingue hacia afuera para no revelar qué correos están registrados (CU-04003 §6 y §10). El tratamiento completo está en §2.4.
4. **Ningún motivo es un código de protocolo.** El motivo es un valor de una enumeración cerrada; la traducción a respuesta pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2).
5. **Ninguna condición deja efecto parcial.** El alcance transaccional declarado es un caso de uso, una unidad de trabajo (`Especificacion-Funcional.md` §3), y por eso cada entrada puede afirmar sin excepción que el repositorio de cuentas o el de trabajos quedan como estaban.

### 1.2 Una condición de error no es una observación, y el comentario tampoco

Es la distinción que sostiene todo lo demás, y confundirla lleva a modelar mal tres cosas a la vez. Las tres nociones son distintas y ninguna es especie de otra:

| Noción | Qué es | Cuántas hay | Quién la produce | Se guarda |
| --- | --- | --- | --- | --- |
| **Condición de error del caso de uso** | Una comprobación que impide una operación ilegítima o imposible. Es lo que este catálogo enumera, y se identifica por un **motivo** | Una por invocación negada, y no sobrevive a la invocación | El caso de uso, al negarse, o el dominio, cuyo rechazo el caso de uso propaga | No |
| **Observación** | Entidad del dominio con dos especies, advertencia y error de validación, que el producto emite **al interpretar el texto del alumno** y al verificar sus valores | Varias por trabajo, tantas como defectos | El validador de figuras, detrás del puerto de validación; el caso de uso la incorpora al trabajo (CU-04005 §4 paso 5) | Sí, como entidad |
| **Comentario** | Texto libre y opcional que el administrador deja al aprobar o al rechazar. **No es una observación y no es una calificación** | A lo sumo uno por trabajo | Una persona | Sí, como atributo del trabajo |

Consecuencia práctica, y es la que más veces se equivoca: un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de este catálogo**. Es el resultado declarado del envío (CU-04005 FA-01), el estado lo resolvió el dominio y el caso de uso lo devolvió con sus observaciones localizadas. Traducirlo hacia afuera como fallo sería un defecto del consumidor.

En el sentido inverso: `MALFORMED_OBSERVATION` y `MALFORMED_PIECE_SET` **sí** son condiciones de este catálogo, aunque hablen de observaciones y de piezas. Lo que se niega no es la observación ni la pieza en sí: es un conjunto mal formado que llegó del validador, y que el alumno no debe ver.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite un **motivo**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no cruza ninguna frontera de proceso y sus contratos son referencias de proyecto de código dentro de la misma solución de código (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application).

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que la capa que expone compone lo que una persona lee. No es una cadena que la biblioteca produzca ni un recurso que exista en el código.

### 1.4 Un mismo motivo con dos causas opuestas: los dos caminos de alta

El producto tiene **dos caminos de alta de cuenta**, y no son variantes de uno solo: son dos contratos con reglas opuestas. Entenderlo es condición para leer bien §3.1 y §3.10, y para no buscar en uno lo que está en el otro.

| Rasgo | Auto-registro del alumno (CU-04001) | Configuración del administrador (CU-04010) |
| --- | --- | --- |
| Estado inicial que impone el dominio | `Pendiente` | `Habilitado` |
| Credencial derivada en el alta | **Prohibida.** Se fija en el primer ingreso efectivo, por CU-04003 | **Obligatoria.** La cuenta nace con credencial fijada |
| Ventana de alta | Abierta siempre: una vez por alumno | Abierta **sólo mientras no exista ningún administrador**. Se cierra con la primera configuración y no vuelve a abrirse |
| Papel que constituye | `Alumno` | `Administrador` |
| Veces que se ejerce | Una por alumno | **Una sola en la vida de la instancia** |

El fundamento de que la cuenta del administrador nazca `Habilitado` lo declara el dominio y esta capa no lo redacta de nuevo: si naciera `Pendiente`, la única transición que la sacaría de ahí es que un administrador la habilite, y no hay ninguno; la instancia quedaría inutilizable en el primer arranque (CU-04010 §10).

**Consecuencia sobre el catálogo.** El motivo `INITIAL_STATUS_NOT_NEGOTIABLE` aparece en los dos caminos con **causas opuestas**: en CU-04001 rechaza constituir la cuenta del auto-registro en un estado distinto de `Pendiente`; en CU-04010, en un estado distinto de `Habilitado`. No es una inconsistencia y no hay que unificarlo: el enunciado del motivo es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino.

Por eso es **el único motivo del catálogo que lleva fila completa en dos subsecciones de §3** en lugar de una entrada única con nota, y las dos filas se leen juntas, con remisión mutua. **Es la misma forma que adoptó el proyecto de código hermano** en su propia categoría 03 para el mismo motivo, y se conserva idéntica: la consistencia entre proyectos de código hermanos vale más que la economía de una fila. Las otras ocho condiciones declaradas en más de un caso de uso conservan la misma causa en todos y siguen con entrada única.

### 7.3 `GeometriaFactory-Infrastructure`

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá un destinatario que las capas de adentro no tienen, y es lo que hace específico a este catálogo:

> En esta capa, **la mitad de las condiciones no las provoca nadie que haya invocado mal**: las provoca el mundo. Un archivo que no está montado, una fuente de aleatoriedad que no responde, un esquema que no corresponde. El diagnóstico dice entonces **qué revisar del lado del despliegue**, no qué corregir del lado del código.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la comprobación que se negó o la cosa que no respondió.
2. **Nada genérico.** No hay «error de base de datos» ni «error interno». Una condición dice **qué** no se pudo hacer y **con qué** no se pudo.
3. **Ninguna condición revela lo que RA-03 prohíbe.** Ningún mensaje incluye la ruta del almacén, la clave de firma ni la dirección de un servicio interno, **y todos quedan registrados del lado del servidor**. Es la única forma de diagnosticar sin exponer.
4. **Ningún código es un código de protocolo.** Su traducción pertenece a `GeometriaFactory-Api`.
5. **Ninguna condición deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.

### 1.2 Siete resultados que no son condiciones de error

Es la distinción que sostiene todo lo demás, y la que más se equivoca en esta capa: **la mayoría de lo que parece un fallo acá es el funcionamiento normal del producto.** Ninguno de los siete tiene entrada en este catálogo, y confundirlos con fallos produce un producto que le grita al alumno por hacer bien su trabajo.

| Lo que ocurre | Por qué **no** es una condición de error | Dónde está declarado |
| --- | --- | --- |
| Una figura del texto no se pudo reconstruir | Es una **observación de especie error de validación**: una entidad del dominio, un resultado, y **lo que el alumno tiene que ver** | CU-06001 FA-01, FA-02, FA-05 |
| El texto no se pudo leer **ni con la tolerancia** | Es un resultado igual: se devuelven 0 figuras y **una observación**, no la condición degradada. El trabajo queda en `Borrador` y el alumno corrige | CU-06001 FA-04 y **CA-10** |
| La verificación no encontró ninguna discrepancia | Cero advertencias es un resultado, no un fallo. Es el criterio negativo, más difícil de acertar que el positivo | CU-06002 FA-01 |
| La recuperación no encontró nada | Es «nada encontrado». Quién lo traduce, **y sin revelar la existencia de un recurso ajeno**, es el consumidor | CU-06003 FA-01, CU-06004 FA-01, CU-06005 FA-01 |
| Una consulta con alcance devuelve el conjunto vacío | Una comisión sin entregas todavía | CU-06003 FA-02 |
| La credencial no coincide | Una contraseña equivocada es el caso normal. **No se distingue hacia afuera cuál campo falló** | CU-06006 FA-01 |
| Un acceso está vencido, o su firma no corresponde | Es exactamente lo que la verificación existe para detectar. La renovación del producto es **por reingreso** | CU-06008 FA-01, FA-02 |

**La consecuencia más cara de confundirlos** está en el segundo: si un texto ilegible devolviera `PARSE_RESULT_UNAVAILABLE` en lugar de una observación, el alumno vería «el servicio no está disponible» cuando lo que pasa es que su programa emitió algo que no se puede leer. Se quedaría esperando a que el sistema se recupere de un problema que no tiene.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite un **código**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no expone endpoints y sus contratos son referencias de proyecto de código dentro de la misma solución de código.

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que las capas de afuera componen lo que una persona lee. No es una cadena que la biblioteca produzca.

**Una sola de estas condiciones tiene destinatario declarado aguas arriba**, y conviene saberlo: `PARSE_RESULT_UNAVAILABLE` es la que `GeometriaFactory-Application` `CU-06005` §6 declara recibir por el puerto de validación. Las demás llegan a la composición de raíz o a la capa de aplicación sin nombre propio allá, y su traducción se decide en `05-Arquitectura-Tecnica`.

### 1.4 Lo que ninguna condición de esta capa puede decir

Es la restricción que este catálogo comparte con ninguna otra sección del producto, porque **acá viven los tres secretos y la única ruta de archivo**.

| Nunca aparece en un mensaje | Por qué | Qué corresponde |
| --- | --- | --- |
| La **clave de firma**, ni una parte de ella | No entra al repositorio de código, no entra a la imagen y no entra a un mensaje | «No hay clave de firma provista» |
| La **contraseña en claro** ni el **valor derivado** de una credencial | Es el último punto del recorrido de la primera —de acá para adentro sólo circula la segunda—, y el único lugar donde las dos conviven | «Falta la contraseña» |
| La **contraseña provisoria** producida | Se devuelve una vez, al consumidor, y **no se registra en ninguna traza** | Nada: el valor viaja en el resultado, nunca en un mensaje |
| La **ruta del archivo del almacén** | Es una dirección de servicio interno a los efectos de RA-03 | «La ubicación configurada del almacén no admite escritura» |
| El **texto original del alumno**, entero o en parte, dentro de un mensaje | El texto es el trabajo de una persona y el registro del servidor no es su lugar | La posición y el campo, que es lo que la regla exige |

**Y la contracara, que es igual de obligatoria:** todo error que se muestre queda **registrado del lado del servidor**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar.

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.1 | 2026-08-29 | **Tramo `R-3a` del renombre `F-03`** —«los 101 códigos de condición van a inglés», decisión del Product Owner del 2026-08-12, reconfirmada el 2026-08-29—, que **reanuda los tramos que la [`Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **1.5** suspendió el 2026-08-13**. **383 ocurrencias** pasan de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios ni lo que está entre «…». **Es el documento más cargado del corpus para este renombre**, y por eso `R-3a` arranca por él. **Ninguna palabra de prosa cambia**: el control de diff verificó que las 362 líneas modificadas del tramo difieren **exactamente** en un par del glosario y en nada más. | AG-00030 |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0**. Los absorbidos quedan archivados. Sube **major**. |
