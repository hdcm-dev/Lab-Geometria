# CU-03 — Exponer el alta de cuenta y la credencial propia

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-14
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.34** (§4.1 RN-13 y su fila de control de cambios 1.34: **con qué se autentica el cambio forzado**), **1.26** §4 (F-01, F-02, **F-04** precisada, F-05), §4.1 (RN-01, RN-02, RN-06, RN-13, **RN-16**), §6 (flujo 1), §9 (X-1), §14 (RA-01, RA-03), §17.5.P.5; `Proyectos/GeometriaFactory-Contracts/.../CU-02-Contrato-De-Administracion-De-Cuentas.md`; `Proyectos/GeometriaFactory-Application/.../CU-01-Registrar-El-Alta-De-Una-Cuenta.md`, `.../CU-10-Configurar-La-Cuenta-De-Administrador.md` y `.../CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Api

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

---

## 1. Propósito

Exponer los **tres** puntos de acceso que tienen en común un rasgo que ninguno de los demás tiene, y que es el motivo por el que están en un solo contrato: **se ejercen sin acceso firmado, o sin que el papel importe**.

| Punto | Intención | Quién lo ejerce |
| --- | --- | --- |
| **A-02** | Registrar una cuenta de alumno, **sin campo de contraseña** | Una persona que todavía no tiene cuenta |
| **A-03** | Configurar la cuenta de administrador, **sólo mientras no exista ninguna** | El docente, en el primer arranque del laboratorio |
| **A-05** | Cambiar la contraseña propia **exigiendo la vigente**, con **dos formas de autenticarse** (intake 1.34): con sesión de trabajo, o con la contraseña actual | Cualquiera de los dos papeles, ya dentro del laboratorio; **y quien entra por primera vez con su provisoria o llega derivado por un reseteo**, que **no tiene sesión de trabajo** y se autentica con la provisoria |

**El punto A-04 quedó retirado por `PRODUCT-INTAKE` 1.13, y su capacidad no.** Exponía el establecimiento de la contraseña en el primer ingreso y era **la única escritura de contraseña de esta superficie que ocurría sin credencial** —A-02 y A-03 también se ejercen sin credencial, y siguen haciéndolo: el **registro** es anónimo por diseño (`PRODUCT-INTAKE` **1.15** §4.1)—: su forma de identificación era el punto abierto más importante de esta categoría. **RN-16** hace que habilitar produzca la contraseña provisoria, con lo cual la persona llega a elegir la suya **ya autenticada**, por **A-05**. El alumno sigue eligiendo su contraseña; lo que desapareció es el punto anónimo. **El identificador `A-04` no se recicla.**

Los tres son el circuito de identidad que el producto sostiene **sin canal de correo**: el intake §9 X-1 declara que el flujo está diseñado para evitar el envío de correo y que **la contraseña no se transporta nunca** desde el sistema hacia la persona. Las únicas excepciones son las dos provisorias que el sistema produce —la de la habilitación y la del reseteo—, que viajan hacia el **administrador** y no hacia la persona, y que son de otros puntos y otro contrato (CU-04 y CU-05).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma las tres solicitudes desde sus formularios y las envía servidor a servidor |
| Alumno y administrador | Sujetos de la regla | Nunca invocan estos puntos directamente (RA-01) |

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones (CU-11).
- Para **A-05**, la petición llega de **una de dos maneras**, que `PRODUCT-INTAKE` **1.34** declara: con un **acceso firmado válido**, que atravesó la guardia de CU-02 con su excepción declarada; o **sin acceso**, con el correo y la contraseña vigente, que es el **cambio forzado**. La segunda existe porque RN-13 **no le emite sesión de trabajo** a la cuenta marcada: exigirle acceso firmado dejaba su pantalla inalcanzable. **En las dos, la vigente es obligatoria y no se emite ningún acceso.**
- Para **A-02** y **A-03**, la petición **no** trae acceso y **no** lo necesita.

## 4. Flujo principal

1. Llega una petición a **A-02** con correo, nombre y apellido, **sin campo de contraseña**.
2. Se ejerce el alta contra la capa de aplicación, que constituye la cuenta en situación `Pendiente` y sin credencial.
3. Se responde `201` con el resultado del registro, que declara la situación inicial de la cuenta.
4. Llega una petición a **A-05** con la contraseña vigente y la nueva.
5. Se ejerce el reemplazo de la credencial contra la capa de aplicación, que exige la verificación de la vigente.
6. Se responde `200`. **La contraseña nueva no vuelve en la respuesta y no queda registrada.**

**El registro no elige contraseña, y eso no es un detalle de formulario.** Es lo que hace posible el flujo sin correo: la cuenta nace sin credencial y **la recibe en el acto de habilitación**, con la provisoria que el sistema produce y que el administrador le comunica en persona; recién entonces la persona elige la suya, cambiándola por **A-05**. Hasta el `PRODUCT-INTAKE` 1.12 la fijaba ella misma, sin credencial, y ése era el agujero que **RN-16** cierra.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Es el primer arranque del laboratorio y no existe cuenta de administrador | Llega una petición a **A-03** con correo y contraseña. La capa de aplicación constituye la cuenta con papel `Administrador`, **`Habilitado` y con credencial**, que es el estado inicial opuesto al del registro. Se responde `201` | Termina |
| FA-02 | Ya existe una cuenta de administrador y llega otra petición a **A-03** | Se responde `409`. **El contrato no ofrece camino alternativo y la respuesta no sugiere ninguno** | Termina |
| FA-03 | Una persona **recién habilitada** cambia por **A-05** la provisoria que la habilitación produjo, presentándola como vigente | Es **el mismo camino que FA-04**, y ésa es la decisión de `PRODUCT-INTAKE` 1.13: el primer ingreso y el cambio posterior a un reseteo dejan de ser dos caminos. Se responde `200`, la marca queda levantada y el camino de entrada vuelve a ser **A-01** | Termina |
| FA-04 | Una cuenta con la marca de cambio pendiente cambia su contraseña por **A-05**, presentando la provisoria como vigente **y su correo, sin acceso firmado** | **Es el cambio forzado**, y desde `PRODUCT-INTAKE` **1.34** se ejerce por la segunda forma de autenticación de este punto: quien autentica es **la provisoria**, porque RN-13 no le da sesión de trabajo a esta cuenta. El reemplazo procede y **levanta la marca** en la misma unidad de trabajo. La contraseña nueva la elige la persona y **el administrador no la conoce**. **No se emite ningún acceso**: la sesión llega recién en el canje siguiente, por A-01 | Termina |
| FA-05 | La cuenta que cambia su contraseña por **A-05** tiene papel `Administrador` | El flujo es idéntico: este punto **no distingue papeles**, y es el único camino por el que el administrador cambia su propia contraseña | Paso 6 |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Punto | Causa |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | A-02, A-03, A-05 | Falta el correo, el nombre o el apellido en el registro; falta una de las dos contraseñas en el cambio. La respuesta **nombra el campo ausente** |
| `CONTRATO_CORREO_YA_REGISTRADO` | `409` | A-02 | El correo ya pertenece a una cuenta. **La respuesta no declara la situación ni el papel de esa cuenta** |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | `409` | A-03 | Ya existe una cuenta con papel `Administrador` |
| `CONTRATO_CREDENCIAL_INVALIDA` | `401` | A-05 | La contraseña vigente presentada no corresponde. Texto neutro, **y la marca de cambio pendiente, si estaba, sigue puesta** |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | Los cuatro | El almacén no está disponible. **La respuesta no incluye su ruta** |

**Ninguna de las cinco devuelve una contraseña, en claro ni derivada, y ninguna la registra.** Los únicos valores de credencial que esta superficie devuelve alguna vez son las dos provisorias que el sistema produce —en CU-04 al habilitar y en CU-05 al resetear—, y ninguna de las dos se registra.

## 7. Postcondiciones

- **A-02 con éxito:** existe una cuenta en situación `Pendiente`, sin credencial, que **todavía no obtiene acceso** (RN-06). La credencial la recibe al ser habilitada, por A-07 (CU-04).
- **A-03 con éxito:** existe **exactamente una** cuenta con papel `Administrador`, habilitada y con credencial, y ninguna petición posterior a ese punto puede crear otra.
- **A-05 con éxito:** la cuenta tiene una credencial derivada nueva; y cuando la vigente presentada era una provisoria —del primer ingreso o de un reseteo—, **la marca quedó levantada**.
- **Fallo:** la pieza pública recibe su código de respuesta y **el almacén queda como estaba**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | La solicitud de registro del ensamblado de contratos | Se inspecciona su superficie | Declara **correo, nombre y apellido**, y **0 campos** de contraseña |
| CA-02 | Un correo ya registrado | Se registra de nuevo por A-02 | Responde `409`, y el cuerpo **no declara la situación ni el papel** de la cuenta que lo ocupa |
| CA-03 | Una instancia con administrador ya configurado | Se invoca A-03 | Responde `409` y **sigue existiendo exactamente 1** cuenta con papel `Administrador` |
| CA-04 | Una instancia sin ninguna cuenta de administrador | Se invoca A-03 | Responde `201`, y la cuenta queda **`Habilitado` y con credencial**, que es el estado inicial opuesto al de A-02 |
| CA-05 | Una cuenta con la marca de cambio pendiente | Se invoca A-05 con la provisoria como vigente | Responde `200`, la marca queda levantada y una petición posterior a cualquier otro punto **ya no recibe** el `403` de la guardia |
| CA-09 | Una cuenta **recién habilitada** y una cuenta **reseteada**, las dos con su provisoria | Se invoca A-05 sobre cada una con su provisoria como vigente | Las **2** responden `200` por el **mismo** punto y el mismo camino: **0 puntos** de esta superficie fijan una contraseña sobre una cuenta existente sin credencial |
| CA-06 | La misma cuenta marcada | Se invoca A-05 con una vigente equivocada | Responde `401` y **la marca sigue puesta** |
| CA-07 | Cualquiera de los cuatro puntos, con la respuesta y el registro del servidor observados | Se ejerce con éxito y con fallo | **0 apariciones** de cualquier contraseña recibida o elegida, del valor derivado de una credencial, de la clave de firma y de la ruta del almacén |
| CA-08 | El punto A-05 | Se invoca con un acceso de papel `Administrador` y con uno de papel `Alumno` | Las **2** peticiones se admiten: este punto **no distingue papeles** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 por el alta y por la configuración inicial; NB-02 por la credencial propia sin canal de correo |
| Reglas de negocio aplicables | [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), en el punto de configuración y su negativa. [RN-02](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), en la traducción del correo ocupado. [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), por la situación inicial que el alta fija. [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), porque **A-05 es la única excepción de la guardia**, y desde el intake **1.34** también el único punto que se puede ejercer **sin sesión de trabajo presentando la contraseña vigente**, que es lo que vuelve alcanzable el cambio forzado |
| Regla de arquitectura del producto | **RA-03** en las cinco condiciones de §6; **RA-01** en los tres puntos, que la pieza pública ejerce servidor a servidor aunque dos de ellos no exijan acceso |
| Puntos de acceso | A-02, A-03, A-05 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-02`, incluida su reutilización por el cambio obligatorio |
| Historias de usuario a generar en 06 | US-07, US-08, US-09, US-10 |
| Componentes esperados en 05 | Tres puntos de acceso. **El punto abierto de identificación de A-04 quedó cerrado** por `PRODUCT-INTAKE` 1.13 §4.1 (RN-16), con el retiro del punto |
| Tests previstos en 08 | Integración por los **nueve** criterios, incluida la prueba de que el primer ingreso y el cambio posterior a un reseteo recorren el mismo punto (CA-09); y una inspección de que ninguna traza contiene contraseñas |

## 10. Notas y supuestos

- **El punto abierto de la identidad en A-04 quedó cerrado por el Product Owner, y conviene dejar escrito cómo.** Esta categoría lo había elevado como el más importante de las suyas: el ensamblado declaraba la solicitud de establecimiento con «la contraseña elegida» y **no declaraba cómo viajaba la identidad**, de modo que era la única escritura **de contraseña** de la superficie que ocurría antes de que la persona pudiera obtener un acceso firmado. `PRODUCT-INTAKE` **1.13** §4.1 lo resuelve con **RN-16**, y **no por ninguna de las dos salidas que esta nota anticipaba**: en lugar de darle identidad al punto anónimo, lo suprime. Habilitar produce una contraseña provisoria, el administrador se la comunica en persona y la cuenta cambia la suya por A-05, autenticada. El fundamento registrado en el intake es el que esta nota describía: un punto anónimo con correo y contraseña elegida dejaba que cualquiera que conociera un correo habilitado le fijara la contraseña a esa cuenta antes que su dueño. **Las dos salidas que se habían anticipado quedan sin objeto**, y se conservan acá como trazabilidad de lo que se evaluó:. Las dos salidas visibles, y **esta categoría no elige entre ellas**:
  1. **Punto anónimo que transporta también la identidad**, con alguna prueba de posesión que ninguna fuente declara. Sin esa prueba, cualquiera podría fijarle la contraseña a cualquier cuenta habilitada que todavía no la tenga, y el circuito sin correo se convertiría en un circuito sin credencial.
  2. **Acceso de alcance acotado emitido para ese único paso**, que traslada el problema al momento de emitirlo pero deja el punto bajo la misma guardia que los demás.
  Fue, como esta categoría anticipaba, una decisión de seguridad y no de forma, y la tomó el **Product Owner**. `Definicion-Superficie-HTTP.md` §9 registra el cierre.
- **A-03 no exige acceso, y es correcto.** En el primer arranque no hay ninguna cuenta con la que obtenerlo. Lo que lo acota no es un papel sino la existencia: **sólo procede mientras no exista ninguna cuenta de administrador**, y la ventana la gobierna la capa de aplicación.
- **El producto no envía correo, y este contrato es donde eso se nota.** El intake §9 X-1 declara la exclusión, que **sigue vigente y con el mismo alcance**: ninguna contraseña se transporta **por un canal del sistema hacia la persona**. Lo que cambió con RN-16 es que la credencial inicial ya no la elige la persona sino que la produce el sistema y la comunica el administrador **por fuera del producto**, en persona. Incorporar el envío de correo cambiaría el flujo de alta entero.
- **El cambio de la propia contraseña exige la vigente, por contrato, y desde el intake 1.13 no hay ninguna excepción.** No hay ningún camino en esta superficie por el que alguien fije una contraseña sobre una cuenta existente sin conocer la anterior —la excepción era A-04, y se retiró—. Los dos caminos que se le parecen los ejerce el **administrador** sobre una cuenta ajena: la habilitación (CU-04) y el reseteo (CU-05), que producen una provisoria que él **no elige** y que obligan a cambiarla.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**, que **cierran el punto abierto más importante de esta categoría**: la identidad en A-04. La resolución del Product Owner **suprime la operación anónima** en lugar de darle identidad. **§1**: los puntos de este contrato pasan de cuatro a **tres** con el retiro de **A-04**, con el fundamento y la constancia de que el identificador **no se recicla**, y A-05 pasa a declarar que sirve también al primer ingreso. **§3**: sale la precondición de identificación abierta. **§4**: la nota del registro sin contraseña se rehace sobre la provisoria de la habilitación. **§5**: **FA-03** se rehace sobre el cambio por A-05, que es el mismo camino que FA-04. **§6**: sale `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y las condiciones pasan de seis a **cinco**; `CONTRATO_CAMPO_REQUERIDO_AUSENTE` deja de nombrar A-04. **§7**: se rehacen las postcondiciones de A-02 y de A-05. **§8**: entra **CA-09**, que verifica **0 puntos** que fijen contraseña sin credencial. **§9**: los puntos de acceso, las reglas de arquitectura, los componentes y las pruebas se ajustan. **§10**: la nota del punto abierto pasa a registrar su **cierre**, con las dos salidas anticipadas conservadas como trazabilidad de lo evaluado; se precisa el alcance de X-1, que sigue vigente; y se declara que ya no hay ninguna excepción a la exigencia de credencial vigente. Sube minor. |
| 1.2 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-16)**, que declara falsa la afirmación de 1.13 según la cual la regla no deja ninguna escritura anónima en el sistema: el **registro de cuenta** de RF-03 es anónimo por diseño y debe seguir siéndolo. En esta superficie eso es visible: **A-02 no trae acceso y no lo necesita**, y §3 ya lo declaraba así. **§1** acota el fundamento del retiro de A-04 —era la única escritura de **contraseña** sin credencial, no la única escritura sin credencial— y **§10** acota la nota del punto abierto cerrado en el mismo sentido. **Ningún punto de acceso, código de error, criterio de aceptación ni recuento cambia**, y los puntos siguen siendo tres. Sube minor. |
| 1.4 | 2026-08-14 | **Absorbe la resolución de `PRODUCT-INTAKE` 1.34: con qué se autentica el cambio forzado**, y con eso deja de contradecir a `CU-01` §6. Este contrato declaraba en §3 que toda petición a **A-05** trae acceso firmado, y `CU-01` §6 declara que la cuenta con la marca **no obtiene acceso**: juntas dejaban al alumno reseteado **sin ninguna manera de llegar al cambio**. **§1**: la fila de A-05 declara las **dos formas de autenticarse** y quién ejerce cada una. **§3**: la precondición de A-05 pasa a declarar las dos maneras, con el fundamento y con la constancia de que la vigente es obligatoria en las dos y de que ninguna emite acceso. **§5 FA-04**: el cambio forzado declara que se ejerce sin acceso firmado, con el correo y la provisoria, y que la sesión llega recién en el canje siguiente. **§9**: la fila de RN-13 lo registra. **Cabecera**: pasa a citar el intake **1.34**. **Ningún punto de acceso, código de respuesta ni criterio de aceptación cambia**, y CA-09 sigue valiendo: **0 puntos** fijan una contraseña sobre una cuenta existente **sin credencial**. Sube minor. |
| 1.3 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, en la extensión que la búsqueda de propagación que el propio informe exige dejó al descubierto: la cabecera citaba `PRODUCT-INTAKE` **1.13** y pasa a citar **1.26**, vigentes hoy. El informe listaba **nueve** cabeceras envejecidas y sólo una de esta carpeta, `CU-12`; el `grep` sobre las categorías 02 y 03 devuelve **diecinueve** archivos con la cita vieja, **los doce casos de uso entre ellos**, y los diecinueve se corrigen en esta tanda. Se abrieron las secciones del intake que este caso de uso cita y **su contenido no cambió** entre 1.13 y 1.26 en nada que este documento afirme, de modo que **no había ninguna afirmación falsa**: lo que se repara es la trazabilidad. **Ningún paso, código, regla, criterio de aceptación ni recuento cambia.** Sube minor. |
