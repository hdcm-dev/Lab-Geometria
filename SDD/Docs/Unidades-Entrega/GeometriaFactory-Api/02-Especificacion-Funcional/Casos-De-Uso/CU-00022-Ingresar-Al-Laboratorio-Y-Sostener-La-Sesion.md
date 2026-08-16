# CU-00022 — Ingresar al laboratorio y sostener la sesión

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-04), §4.1 (RN-02013, RN-02016), §17.1.P.5 · GeometriaFactory-Domain, §17.1.P.3 · GeometriaFactory-Api y §17.1.P.5 · GeometriaFactory-Api, y la resolución **1.34** sobre con qué se autentica el cambio forzado
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** `CU-00003` §A-05, [`CU-00001`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00001-Canjear-Credenciales-Por-Un-Acceso-Firmado.md), [`CU-00002`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-00002-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md), [`CU-04003`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md), [`CU-02003`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) y [`CU-02004`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Que una persona con cuenta habilitada entre al laboratorio presentando su correo y su contraseña,
obtenga una sesión de trabajo, y que esa sesión sostenga cada una de sus peticiones posteriores. Y
que pueda **cambiar su propia contraseña**, siempre exigiendo la vigente.

Abarca tres tramos que son uno solo desde el punto de vista de quien entra:

| Tramo | Punto de acceso | Qué resuelve |
| --- | --- | --- |
| **El canje** | **A-01** | Recibe correo y contraseña, resuelve si la cuenta admite el ingreso y devuelve la sesión de trabajo |
| **La guardia** | **A-05** a **A-15**, once | Verifica la sesión en cada petición posterior, exige el papel que el punto declara y corta a la cuenta que tiene un cambio de contraseña pendiente |
| **El cambio propio** | **A-05** | Reemplaza la contraseña exigiendo la vigente, con **dos formas de autenticarse** |

**Lo que este caso de uso no hace, y hay que dejarlo imposible de confundir: no autoriza.** Verificar
que la sesión trae papel `Administrador` no es lo mismo que verificar que quien pide puede operar
sobre *ese* trabajo. La comprobación sobre el dato recuperado —pertenencia, facultad y alcance— vive
en cada caso de uso que toca datos. El intake §17.1.P.5 · GeometriaFactory-Api lo dice en una línea: **el rol no alcanza**.

**Tampoco fija la primera credencial.** Desde **RN-02016** la contraseña inicial la produce el
sistema **dentro del acto de habilitación**, que es `CU-00023`. Acá la persona sólo la **reemplaza**,
y siempre presentando la vigente: **ningún camino de este caso de uso fija una contraseña sobre una
cuenta sin conocer la anterior**.

**El producto acepta por escrito un riesgo en el canje.** El intake §17.1.P.5 · GeometriaFactory-Api registra la elección de
canjear la contraseña en claro contra **A-01** como **decisión consciente y no como omisión**, porque
el intermediario es el propio portal del mismo sistema y el alcance es un laboratorio de aula.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Alumno o administrador | Primario | Entra al laboratorio con su correo y su contraseña, y cambia la suya cuando quiere o cuando se lo exigen |
| `GeometriaFactory-Web` | Intermediario | Recibe las credenciales del formulario y las canjea **servidor a servidor**; presenta la sesión en cada petición posterior. **El navegador no alcanza esta superficie** (RA-01) |
| Mecanismo de acceso firmado | Sistema | Emite la sesión con sus cuatro reclamos, y la verifica en cada petición |
| Mecanismo de credenciales | Sistema | Deriva la contraseña y la compara contra el valor derivado guardado. **La contraseña en claro no llega nunca al modelo de dominio** (intake §17.1.P.5 · GeometriaFactory-Domain) |
| Almacén de cuentas | Sistema | Recupera la cuenta por su correo y materializa la credencial resultante |
| Reloj del sistema | Sistema | Provee el sello de modificación de la cuenta |

**Este punto no distingue papeles.** Qué habilita cada uno lo deciden los demás puntos: acá el papel
sólo viaja como reclamo de la sesión.

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones.
- **La clave de firma está provista por configuración.** Sin ella no se emite ninguna sesión y
  ninguna sesión se puede verificar, de modo que **ninguna petición se admite**.
- Para el canje, la petición llega a **A-01** con el par de credenciales en el cuerpo.
- Para cada petición posterior, la sesión viaja en la cabecera de autorización, y **cada punto de
  acceso declara qué papel exige**.
- Para el cambio propio, la petición llega a **A-05** de **una de dos maneras**, que el intake
  **1.34** declara: con **sesión de trabajo válida**, que atravesó la guardia con su excepción; o
  **sin sesión**, con el correo y la contraseña vigente, que es el **cambio forzado**. **En las dos,
  la vigente es obligatoria y no se emite ninguna sesión.**

## 4. Flujo principal

1. La persona escribe su correo y su contraseña en el formulario de ingreso y lo envía.
2. Llega la petición al punto **A-01** con el par de credenciales.
3. Se recupera la cuenta por su correo y se evalúa su **admisibilidad**: que la situación sea
   `Habilitado` y que la marca de cambio de contraseña pendiente esté **levantada**. La evaluación
   **no tiene efecto**: no cambia nada de la cuenta.
4. La cuenta es admisible: se devuelve su identidad y su papel.
5. Se verifica la contraseña presentada contra el valor derivado guardado, por el mecanismo de
   credenciales.
6. Se emite la sesión de trabajo con **identificador, correo, papel y expiración**, y se responde
   `200` con el acceso, el identificador, el correo y el papel.
7. En cada petición posterior a uno de los **once** puntos que exigen sesión, se toma el acceso de la
   cabecera, se verifica su firma y su expiración, se compara su reclamo de papel contra el que el
   punto exige, y **se comprueba que la cuenta no tenga la marca de cambio pendiente**.
8. La petición se admite y sigue hacia su caso de uso, llevando **la identidad y el papel, y nada
   más**: la guardia no agrega ningún dato.

**La sesión no se guarda de este lado.** Esta superficie es sin estado: quien la conserva es el
circuito del portal, del lado de su servidor, y **el navegador no la ve nunca**.

**El paso 7 corta antes que cualquier otra cosa que el punto vaya a hacer.** Una cuenta marcada no lee
ni escribe nada: es **INV-09**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta que canjea tiene papel `Administrador` | El flujo es idéntico y sólo cambia el valor del reclamo de papel | Paso 6 |
| FA-02 | El mismo par de credenciales se canjea dos veces | Se emiten **dos sesiones**, y es correcto: el punto es sin estado y no lleva registro de emisiones. **La vigencia corta es lo que acota el efecto** | Paso 6 |
| FA-03 | La sesión emitida vence | **No hay renovación**: el producto no tiene acceso de refresco en este alcance. La persona vuelve a entrar por A-01 | Paso 1 de una petición nueva |
| FA-04 | El punto de destino no exige un papel en particular y admite los dos | Los pasos 7 y 8 se ejercen igual; la comparación de papel se satisface con cualquiera de los dos valores. **Es el caso de los puntos de lectura de trabajos y del cambio propio**, y no es una excepción a la guardia | Paso 8 |
| FA-05 | La persona, ya dentro del laboratorio, cambia su contraseña por **A-05** | Presenta la vigente y la nueva con su sesión de trabajo. Se verifica la vigente, se toma el sello de modificación del reloj y se reemplaza la credencial derivada en una unidad de trabajo. Se responde `200`. **La contraseña nueva no vuelve en la respuesta y no queda registrada.** Ningún otro atributo de la cuenta cambia y **no se conserva historial**: ninguna fuente lo declara | Termina |
| FA-06 | La cuenta que cambia por **A-05** tiene papel `Administrador` | El flujo es idéntico: este punto **no distingue papeles**, y es **el único camino** por el que el administrador cambia su propia contraseña | Termina |
| FA-07 | **El cambio forzado**: una cuenta con la marca puesta cambia su contraseña por **A-05**, presentando **su correo y la provisoria como vigente, sin sesión de trabajo** | Es el reemplazo de FA-05 con **un efecto adicional: levanta la marca**, en la misma unidad de trabajo. Se ejerce por la segunda forma de autenticación del intake **1.34**, porque **RN-02013 no le emite sesión a esta cuenta**: exigirle sesión dejaba su pantalla inalcanzable. La contraseña nueva la elige la persona y **el administrador no la conoce**. **No se emite ninguna sesión**: llega recién en el canje siguiente, por A-01 | Termina |
| FA-08 | **El primer ingreso**: una cuenta **recién habilitada** cambia la provisoria que la habilitación produjo | Es **el mismo camino que FA-07**, y ésa es la decisión del intake 1.13: el primer ingreso y el cambio posterior a un reseteo dejan de ser dos caminos. **Ningún dato los distingue** | Termina |
| FA-09 | La guardia se aplica sobre **A-05** con una cuenta marcada | **Es la única excepción declarada de la guardia**, y es una: cambiar la propia contraseña es lo único que INV-09 le deja hacer a esa cuenta, y es lo que levanta la marca | Paso 8 |
| FA-10 | La petición llega a uno de los puntos que **no** exigen sesión | **La guardia no se aplica**, y se declara para que su ausencia no se lea como un hueco: el canje, el registro de una cuenta, la configuración del administrador, la consulta de si ya hay administrador y la salud se ejercen sin sesión **por construcción** | Termina fuera de este caso de uso |

## 6. Excepciones y errores

**Los códigos son los del ensamblado de contratos. Esta capa no agrega ninguno**: lo que decide es su
código de respuesta, según [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §6.

### 6.1 El canje, por A-01

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| — | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | Falta el correo o falta la contraseña. La respuesta **nombra el campo ausente**, que es un dato de la petición y no de la cuenta |
| `CUENTA_INEXISTENTE` | `CONTRATO_CREDENCIAL_INVALIDA` | `401` | El par no corresponde a ninguna cuenta. **Genérico: no declara cuál de los dos campos falló** |
| `CUENTA_PENDIENTE`, `CUENTA_BLOQUEADA` | `CONTRATO_CUENTA_NO_HABILITADA` | `403` | La cuenta está `Pendiente` o `Bloqueado`. **Con motivo**, para que la persona sepa en qué situación está su cuenta |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `403` | La cuenta tiene una provisoria sin cambiar, producida por **la habilitación (RN-02016) o por el reseteo**. El portal lo convierte en el desvío a **A-05**. **No se emite sesión** |
| — | `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible. **La respuesta no incluye su ruta** |
| ~~`CREDENCIAL_NO_ESTABLECIDA`~~ | ~~`CONTRATO_CONTRASENA_NO_ESTABLECIDA`~~ | — | **Retirados** por RN-02016: habilitar produce y fija la provisoria, de modo que ninguna cuenta llega a estar habilitada sin credencial. **Los identificadores no se reciclan**, y quien busque el encaminamiento del primer ingreso encuentra la fila anterior. Se conserva tachado para que una cita vieja no quede sin respuesta |

### 6.2 La guardia, sobre los once puntos

| Código del contrato | Respuesta | Causa |
| --- | --- | --- |
| — | `401` | No hay sesión, está vencida, o su firma no corresponde. **Las tres responden igual**: el cuerpo no declara cuál de las tres ocurrió |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `403` | El papel de la sesión no es el que el punto exige, **en el punto del desenlace** |
| `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | `403` | Ídem **fuera del desenlace**: gobierno de cuentas, listado de la comisión y reseteo. Entró al conjunto cerrado por el `PRODUCT-INTAKE` 1.29, y **hasta entonces esos tres caminos respondían sin código**: ver §10 |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `403` | La cuenta tiene una provisoria sin cambiar. **Un solo código para todas las operaciones bloqueadas**, porque lo que le queda por hacer es siempre lo mismo |

**El `401` de la guardia no lleva código del contrato, y es deliberado.** El conjunto cerrado no tiene
ninguno que describa una sesión ausente o inválida, y **esta capa no inventa códigos**: lo que el
contrato no declara, no viaja como código.

### 6.3 El cambio propio, por A-05

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| — | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | Falta una de las dos contraseñas |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | `CONTRATO_CREDENCIAL_INVALIDA` | `401` | La vigente presentada no corresponde. Texto neutro, **y la marca, si estaba, sigue puesta** |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | `CONTRATO_CUENTA_NO_HABILITADA` | `403` | La cuenta está `Pendiente` o `Bloqueado`. La credencial **se conserva como estaba** |
| `VALOR_DERIVADO_VACIO` | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | La contraseña nueva está vacía |
| `CREDENCIAL_YA_FIJADA` | — | — | Se pide fijar por primera vez una credencial que ya tiene valor. **Inalcanzable desde A-05 por construcción**: este punto **sólo reemplaza**, y la fijación ocurre dentro de la habilitación, en `CU-00023` |
| — | `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible |

**Ninguna condición de las tres tablas emite una sesión que no correspondía, devuelve una contraseña
—en claro ni derivada— ni la registra.** Y **ninguna condición de la guardia llega al caso de uso del
punto**: si la guardia rechaza, no se lee ni se escribe nada, y ése es el criterio con el que se
verifica —se comprueba el estado del almacén después del rechazo, no la respuesta—.

**Por qué dos motivos comparten el `403` del canje y uno solo tiene el `401`.** Los dos del `403`
describen **la situación de una cuenta que existe**, y la persona necesita saber cuál es porque de eso
depende qué hacer después: esperar la habilitación o cambiar la provisoria. El `401` describe que **el
par no corresponde a ninguna cuenta**, y ahí el producto deliberadamente no dice más: distinguir el
correo desconocido de la contraseña equivocada permitiría averiguar por tanteo qué correos están
registrados.

## 7. Postcondiciones

- **Canje con éxito:** el portal tiene una sesión firmada con sus cuatro reclamos, de vigencia corta.
  **Nada cambió del lado del servicio**: el canje no escribe.
- **Petición admitida:** el caso de uso del punto recibe la identidad y el papel, y nada más.
- **Petición rechazada por la guardia:** el portal recibe `401` o `403` y **el almacén queda
  exactamente como estaba**.
- **Cambio propio con éxito:** la cuenta tiene una credencial derivada nueva y su sello de
  modificación es el del reloj. Ningún otro atributo cambió, **con una sola excepción declarada**: si
  la marca estaba puesta, el reemplazo **la levanta**, y los dos efectos son un solo acto — no hay
  camino por el que uno ocurra sin el otro. La cuenta vuelve a ser admisible y **obtiene sesión recién
  en el canje siguiente**.
- **Fallo, en los tres tramos:** ninguna sesión emitida, la cuenta exactamente como estaba —marca
  incluida—, la contraseña recibida sin registrar en ninguna parte, y el intento registrado del lado
  del servidor **sin ella**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta de alumno habilitada, con contraseña propia y sin marca | Entra con la contraseña correcta | Responde `200` y el cuerpo trae **el acceso, el identificador, el correo y el papel `Alumno`**, y **0 campos** más |
| CA-02 | La misma cuenta | Entra con la contraseña equivocada, y después con un correo que no existe | Las **2** respuestas son `401` y sus cuerpos son **idénticos**: 0 diferencias que permitan distinguir cuál de los dos casos ocurrió |
| CA-03 | Una cuenta recién registrada, en situación `Pendiente` | Entra | Responde `403` **con el motivo de la situación**, y **0 sesiones emitidas** |
| CA-04 | Una cuenta **recién habilitada** con su provisoria, y una cuenta **reseteada** con la suya | Cada una entra con su provisoria correcta | Las **2** responden `403` con **el mismo** motivo de cambio requerido —no uno propio de cada origen— y **0 sesiones** se emiten. La credencial **se reconoce y no se admite** |
| CA-05 | Una petición de canje sin el campo de contraseña | Se envía | Responde `400` nombrando el campo ausente |
| CA-06 | Una sesión emitida | Se inspecciona | Lleva **exactamente los cuatro reclamos** —identificador, correo, papel y expiración— y su vigencia es corta |
| CA-07 | La superficie completa de [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §3 | Se recorren sus **16** puntos | **11** exigen sesión y aplican la guardia; **5** no la exigen por construcción. 11 + 5 = 16 |
| CA-08 | Los **11** puntos que exigen sesión | Se invoca cada uno **sin** cabecera de autorización, con una sesión vencida y con una firmada con otra clave | Las **33** respuestas son `401`, sus cuerpos son indistinguibles entre sí, y en las 33 el almacén queda **sin ningún cambio** |
| CA-09 | Una sesión válida con papel `Alumno` | Se invoca cada uno de los puntos que exigen `Administrador` | Todos responden `403` y **0 de ellos leen o escriben** el recurso pedido |
| CA-10 | Una cuenta con la marca puesta, con sesión válida | Se invocan **todos** los puntos que exigen sesión **salvo A-05** | **Todas** responden `403` con el **mismo** código del contrato, con 0 detalles y sin nombrar la operación pedida |
| CA-11 | La misma cuenta marcada | Se invoca **A-05** con la provisoria correcta | La guardia **admite**, el cambio procede, la marca queda levantada y una petición posterior a cualquier otro punto **ya no recibe** el `403`. **Es la única excepción, y es una** |
| CA-12 | La misma cuenta marcada | Se invoca **A-05** con una vigente equivocada | Responde `401`, la credencial no cambia y **la marca sigue puesta**: **0 caminos** levantan la marca sin un cambio efectivo |
| CA-13 | Una cuenta marcada, **sin sesión de trabajo**, presentando su correo y su provisoria | Se invoca **A-05** | Responde `200`, la marca queda levantada y **0 sesiones** se emiten: la sesión llega recién en el canje siguiente |
| CA-14 | Una cuenta habilitada de papel `Administrador` y otra de papel `Alumno` | Cada una invoca **A-05** con su vigente | Las **2** peticiones se admiten: este punto **no distingue papeles**, y es el único camino por el que el administrador cambia su propia contraseña |
| CA-15 | Cualquier respuesta de §6, con su cuerpo y el registro del servidor observados | Se produce la condición | **0 apariciones** de la contraseña recibida o elegida, del valor derivado, de la clave de firma, de la sesión presentada, de la ruta del almacén y de la dirección de cualquier servicio interno |
| CA-16 | El almacén no disponible | Se canjea y se cambia la contraseña | Las **2** responden `503`, y **ningún cuerpo dice dónde está el almacén** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00002](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md), en su circuito de identidad sin canal de correo |
| Reglas de negocio aplicables | [RN-02006](../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), con su tramo de traducción acá: la respuesta **con motivo**. [RN-02013](../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), **con el tramo que esta unidad puede romper sola**: un punto de acceso fuera de la guardia la incumple **sin que nada falle**; y con las dos formas de autenticación que el intake 1.34 declara. [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), por el papel que viaja en la sesión y el que cada punto exige. [RN-02010](../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), en el único punto donde el papel insuficiente tiene código del contrato |
| Invariantes del producto | **INV-06**, una cuenta `Pendiente` o `Bloqueado` no obtiene acceso. **INV-09**, una cuenta marcada no ejerce ninguna capacidad salvo cambiar su propia contraseña: la guardia es su expresión en la frontera del proceso |
| Reglas de arquitectura del producto | **RA-01**, el único invocante legítimo es el portal, servidor a servidor. **RA-03**, ninguna respuesta expone secretos ni direcciones de servicios internos, y todo intento queda registrado |
| Puntos de acceso | **A-01** el canje; **A-05** el cambio propio; **A-05 a A-15**, once, gobernados por la guardia. **A-01 es la única ruta que declara una fuente**, el intake §17.1.P.3 · GeometriaFactory-Api |
| Contratos de uso que transporta | `GeometriaFactory-Contracts` `CU-00001` y `CU-00002`, incluida la reutilización de este último por el cambio obligatorio |
| Puertos que consume | Almacén de cuentas, reloj del sistema, mecanismo de acceso firmado, mecanismo de credenciales |
| Historias de usuario a generar en 06 | US-00001 a US-00006, US-00009 |
| Componentes esperados en 05 | Punto de canje; guardia de admisión previa a todo punto que exija sesión; punto de cambio propio; conexiones con la evaluación de admisibilidad, el mecanismo de credenciales y el emisor de la sesión |
| Tests previstos en 08 | Integración por los dieciséis criterios; **una prueba por punto y por condición** en la guardia —no una por condición—, que es la única forma de detectar el punto que quedó afuera; una prueba **estructural** que compare la lista de puntos contra la lista de puntos guardados; la prueba de que el primer ingreso y el cambio posterior a un reseteo recorren el mismo camino (CA-04, CA-13); e inspección de que ninguna traza contiene credenciales |

## 10. Notas y supuestos

- **El defecto característico de la guardia no es hacer mal lo que hace, sino no alcanzar a alguno.**
  Se rompe agregando un punto de acceso nuevo y olvidándose de guardarlo, y **cuando eso pasa nada
  falla**. Por eso CA-07 y CA-08 cuentan puntos en lugar de ejercer uno, y por eso §9 pide una prueba
  estructural.
- **El punto abierto del papel insuficiente fuera del desenlace está cerrado.** El conjunto cerrado
  declaraba `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` para el desenlace y nada para los demás,
  y la guardia respondía `403` sin código. El `PRODUCT-INTAKE` **1.29** incorporó
  `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` el **2026-08-12**, con destino `403`, para los
  tres caminos restantes. `CU-00002` 1.3 seguía declarándolo abierto: el alcance de esa propagación
  incompleta está en `CU-00023` §10.
- **La marca se levanta en un solo lugar de todo el producto**, y es el reemplazo de FA-07. Ninguna
  otra operación la levanta, y ninguna la levanta sin un cambio efectivo de credencial.
- **La evaluación de admisibilidad concentra INV-06 e INV-09 en un solo lugar** porque el invariante
  alcanza a *todas* las capacidades y no hay una puerta única del modelo por la que pasen todas.
  Está declarada como **decisión derivada** en
  [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §4.1.
- **El criterio de comparación de dos correos** —tal cual o normalizados— es un punto abierto
  declarado aguas arriba, y lo resuelve `05-Arquitectura-Tecnica`.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe el punto **A-05** de `CU-00003` 1.5, `CU-00001` 1.5, `CU-00002` 1.3, `CU-04003` 1.3, `CU-02003` 1.3 y `CU-02004` 1.3, que eran **seis vistas de la misma capacidad**: el canje, la guardia y el cambio propio son tres tramos de una sola cosa —entrar y seguir estando dentro— y la admisibilidad y la credencial son cómo se resuelven adentro. La unión no es la suma: el actor primario pasa a ser la persona que entra; §6 se parte en **tres tablas por tramo** con el motivo interno y su traducción a respuesta en la misma fila, y declara `CREDENCIAL_YA_FIJADA` como **inalcanzable por construcción** desde A-05 en lugar de omitirlo; los criterios de aceptación se rehacen sobre la capacidad y quedan **dieciséis**, con **CA-08** unificando las tres condiciones del `401` de la guardia en una sola cuenta de 33 respuestas indistinguibles. La **fijación** de la primera credencial **no queda acá**: ocurre dentro de la habilitación y es de `CU-00023`, y §1 lo declara para que su ausencia no se lea como omisión. Los cinco documentos absorbidos enteros quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Agregar un punto de acceso **sin agregarlo a la guardia** incumple RN-02013 sin que nada falle, y es
el cambio incompatible más probable de esta superficie: CA-07 y CA-08 existen para detectarlo.
Distinguir en la respuesta del `401` del canje el correo desconocido de la contraseña equivocada
contradice CA-02 y convierte el punto en un oráculo de qué correos están registrados. Emitir sesión a
una cuenta marcada contradice RN-02013 y CA-04. Admitir un cambio de contraseña sin exigir la vigente
reabre el agujero que RN-02016 cerró. Guardar la sesión del lado del servicio cambia la naturaleza sin
estado de la superficie y no es compatible. Agregar un acceso de refresco es una capacidad nueva y
está fuera de este alcance: la renovación es **por reingreso**.
