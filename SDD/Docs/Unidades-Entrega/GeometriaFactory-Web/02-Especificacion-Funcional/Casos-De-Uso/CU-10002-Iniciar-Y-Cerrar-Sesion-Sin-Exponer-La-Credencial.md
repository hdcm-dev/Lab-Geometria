# CU-10002 — Iniciar y cerrar sesión sin exponer la credencial

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (tercer, cuarto y quinto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md` §5 (segundo criterio); `../../../../00-Contexto/Vision-Producto.md` §9.2 (pieza en su segundo referente, forma calificada de `Pendiente`); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.8**, §4 (F-05, **F-26**), §4.1 (RN-10001, RN-10006, **RN-10013 precisada**), §6 (flujo 1), §9 (**X-2 retirada**), §14 (RA-01, RA-03), §17.1.P.2 (**INV-09**), §17.6 P.3, **P.5**, P.11 punto 1; [`GeometriaFactory-Contracts` CU-08008](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) §4, §6
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`

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

Permitir que una persona habilitada entre al laboratorio presentando su correo y su contraseña, y que salga cuando quiera, de modo que **la credencial de sesión quede en el estado del circuito, del lado del servidor de la pieza pública, y no llegue nunca al navegador**. Es también el caso de uso que decide qué rutas quedan accesibles según el papel de la persona.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Persona con cuenta del laboratorio | Primario | Presenta sus credenciales, obtiene acceso a su panel y cierra sesión |
| Pieza pública | Sistema | Recibe las credenciales del formulario, las canjea contra la pieza de datos, custodia la credencial de sesión en el estado del circuito y protege las rutas |
| Pieza de datos | Secundario | Verifica las credenciales y emite la credencial de sesión y el papel |

El actor primario es uno solo. El papel que la persona ejerce —alumno o administrador— es una salida del canje, no un actor distinto: lo único que cambia es qué panel arma la pieza pública, y eso se declara en FA-03.

## 3. Precondiciones

- La persona tiene una cuenta con situación habilitada y contraseña ya establecida. Los desvíos por situación de cuenta o por contraseña sin establecer están en §5 y §6.
- La pieza pública tiene configurada la dirección de la pieza de datos, tomada de configuración y **nunca embebida en el código**.
- El circuito entre el navegador y la pieza pública está establecido.

## 4. Flujo principal

1. La persona abre la ruta de ingreso y completa correo y contraseña.
2. La pieza pública recibe los dos valores en su propio servidor.
3. **La pieza pública invoca el contrato de canje de credenciales** de `GeometriaFactory-Contracts` CU-10001 desde su servidor, servidor a servidor. Ningún guion del navegador participa.
4. La pieza de datos devuelve la credencial de sesión, el identificador de la persona, su correo y su papel.
5. La pieza pública **guarda la credencial de sesión en el estado del circuito**, en memoria del servidor del hosting público, y no la escribe en ninguna respuesta dirigida al navegador.
6. La pieza pública deja en el navegador únicamente una marca de sesión propia del circuito, que no transporta la credencial ni ningún dato de la cuenta.
7. La pieza pública arma el panel que corresponde al papel recibido y lleva a la persona a su ruta inicial: el listado de trabajos propios si el papel es de alumno, el listado de la comisión si es de administrador.
8. Cuando la persona cierra sesión, la pieza pública descarta la credencial de sesión del estado del circuito, invalida la marca de sesión del navegador y devuelve a la ruta de ingreso.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta está `Pendiente` o bloqueada | El contrato devuelve `CONTRATO_CUENTA_NO_HABILITADA` con su motivo. La pieza pública muestra el motivo tal como corresponde a la situación de la cuenta y **no otorga sesión**: no se guarda credencial de sesión y el navegador no recibe marca de sesión | El flujo vuelve al paso 1 |
| FA-02 | La cuenta **acaba de ser habilitada** y la persona ingresa con la contraseña provisoria que el administrador le comunicó | El contrato devuelve `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` —**el mismo** código que FA-07, y no uno propio—. La pieza pública **no guarda credencial de sesión** y deriva a CU-10003, a su flujo principal. Desde `PRODUCT-INTAKE` 1.13 (**RN-10016**) el primer ingreso y el cambio posterior a un reseteo son el mismo camino | El flujo se reanuda en el paso 1 una vez cambiada la contraseña |
| FA-03 | La persona que ingresa es el administrador | El canje es idéntico y sólo cambia el valor del papel. La pieza pública arma el panel del administrador | El flujo continúa en el paso 7 |
| FA-04 | La persona pide una ruta del panel sin tener sesión | La pieza pública no arma la ruta y devuelve a la de ingreso, sin revelar qué había en la ruta pedida | El flujo vuelve al paso 1 |
| FA-05 | Un alumno con sesión pide una ruta del panel del administrador | La pieza pública no arma la ruta y devuelve al panel del alumno, sin revelar qué había en la ruta pedida | El flujo vuelve al paso 7 |
| FA-07 | La cuenta está habilitada y con contraseña, y el administrador se la **reseteó**: llega con la marca de cambio de contraseña pendiente | El contrato **reconoce la credencial provisoria y no emite sesión**: devuelve `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`. La pieza pública **no guarda credencial de sesión ni deja marca de sesión en el navegador**, y deriva al **cambio forzado** de CU-10003, que la persona resuelve presentando la provisoria como contraseña vigente (RN-10013, INV-09). **No es un rechazo**: es un encaminamiento, exactamente como el primer ingreso con contraseña no fijada de FA-02, del que sólo se distingue en que acá hay una contraseña y es provisoria | El flujo continúa en CU-10003, FA-04 |
| FA-06 | El circuito se corta y se restablece | La pieza pública muestra su cartel de reconexión y, al restablecerse, la sesión sigue vigente porque la credencial nunca vivió en el navegador. El tratamiento del cartel es de CU-10010 | El flujo vuelve al punto donde la persona estaba |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El correo o la contraseña no corresponden a ninguna cuenta | La pieza pública muestra un único mensaje que **no declara cuál de los dos campos falló**. Terminación controlada: no hay reintento automático |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta está `Pendiente` o bloqueada | Handoff a FA-01: se muestra el motivo y no se otorga sesión |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | La cuenta tiene una provisoria sin cambiar, **producida por la habilitación (FA-02) o por el reseteo (FA-07)** | Handoff a FA-02 o a FA-07 según el origen: **no se otorga sesión** y la persona queda derivada a CU-10003. **Es un solo código para los dos orígenes**, porque lo que la pieza pública tiene que hacer es lo mismo. Hasta el `PRODUCT-INTAKE` 1.12 el primer ingreso tenía código propio, `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, que **RN-10016** retiró del conjunto cerrado al hacer imposible su causa. La diferencia con `CONTRATO_CUENTA_NO_HABILITADA` es el destino: allá la persona vuelve a la ruta de ingreso sin camino propio, acá tiene uno y es el cambio |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el correo o la contraseña | La pieza pública señala el campo faltante. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10010: estado degradado explícito, sin dirección de servicio interno en el mensaje, y sin excepción sin manejar |

## 7. Postcondiciones

- En caso de éxito: existe una credencial de sesión viva **en el estado del circuito** y la persona está en el panel de su papel. El navegador conserva sólo la marca de sesión.
- En caso de cierre de sesión: no queda credencial de sesión en el estado del circuito ni marca de sesión válida en el navegador, y toda ruta del panel vuelve a exigir ingreso.
- En caso de fallo: no hay credencial de sesión de ningún tipo y la persona queda en la ruta de ingreso con el motivo a la vista.
- En ningún caso: la credencial de sesión aparece en el navegador, ni en el documento, ni en el almacenamiento del navegador, ni en ninguna respuesta que el navegador reciba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta habilitada con contraseña establecida | La persona ingresa con correo y contraseña correctos | Queda en el panel de su papel y la credencial de sesión vive únicamente en el estado del circuito |
| CA-02 | Una sesión iniciada correctamente | Se inspecciona el navegador con las herramientas de desarrollo —almacenamiento local, almacenamiento de sesión, marcas de sesión y cuerpo de las respuestas recibidas— | **Cero apariciones de la credencial de sesión**. La única marca presente es la del circuito y no transporta credencial |
| CA-03 | Una cuenta recién registrada, en situación `Pendiente` | La persona intenta ingresar | Recibe el motivo declarado de su situación de cuenta, no obtiene sesión y el navegador no recibe marca de sesión |
| CA-04 | Una cuenta **recién habilitada**, y una cuenta **reseteada**, las dos con su provisoria | Cada persona intenta ingresar con su provisoria | Las **2** reciben el mismo código, son derivadas al mismo cambio de contraseña de CU-10003 y **0 obtienen sesión** en ese intento |
| CA-05 | Un alumno con sesión iniciada | Pide por dirección directa una ruta del panel del administrador | La pieza pública no arma esa ruta, lo devuelve a su propio panel y no revela qué contenía |
| CA-06 | Una sesión iniciada | La persona cierra sesión y vuelve a pedir una ruta del panel | Es devuelta a la ruta de ingreso; la credencial de sesión ya no está en el estado del circuito |
| CA-07 | Un recorrido completo de ingreso y navegación por el panel | Se inspecciona el tráfico de red del navegador | Cero peticiones del navegador hacia la pieza de datos, y ningún mensaje visible contiene una dirección de servicio interno |
| CA-08 | Una cuenta habilitada a la que el administrador le reseteó la contraseña | La persona ingresa con la provisoria | Aterriza en el cambio forzado de CU-10003, no en el panel de su papel, y **no obtiene sesión**: no hay credencial de sesión en el estado del circuito ni marca de sesión en el navegador. Ninguna otra ruta de su papel se arma hasta que lo complete |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md), [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) |
| Reglas de negocio aplicables | [`RN-02006`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [`RN-02001`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md). Viven y se hacen cumplir en `GeometriaFactory-Domain` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08001](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) completo, con FA-01, FA-02 y FA-03; [`CU-08008`](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) §4 y §6, por el canje con la provisoria; [`CU-08006`](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-10003, US-10004, US-10005 |
| Componentes esperados en 05 | Página de ingreso, custodia de la credencial de sesión en el estado del circuito, y el mecanismo de protección de rutas por papel |
| Tests previstos en 08 | Guion de demostración de la etapa `c`, con la inspección del navegador que verifica CA-02; guion de la etapa `d` para FA-01 y FA-02 |

## 10. Notas y supuestos

- **CA-02 es el criterio que materializa la decisión más consecuente del producto.** El intake lo declara verificable con las herramientas de desarrollo (§17.6 P.5), y es la razón por la que la llamada a la pieza de datos la hace el servidor de la pieza pública y no el navegador: sin eso vuelven el contenido mixto, la negociación de origen cruzado y la exposición de la dirección del servidor propio.
- El tramo entre la pieza pública y la pieza de datos puede viajar sin cifrar, y el riesgo está **aceptado por escrito** aguas arriba (`PRODUCT-INTAKE` §11, RN-B5). Este caso de uso no lo reabre ni propone mitigarlo: esa decisión es de 05-Arquitectura-Tecnica.
- La protección de rutas de FA-04 y FA-05 es necesaria pero no suficiente: la pertenencia y el papel los verifica la pieza de datos en cada solicitud. Ocultar una ruta no es hacer cumplir una regla.
- **La cuenta con la contraseña reseteada se autentica y no obtiene sesión de trabajo**, y es la lectura que fija el `PRODUCT-INTAKE` **1.8** al precisar RN-10013: el sistema reconoce la provisoria y la deriva al cambio, sin emitir sesión. No es una sutileza de redacción: la diferencia **es observable** en lo que este caso de uso guarda en el estado del circuito, y emitir una sesión a una cuenta que por INV-09 no ejerce ninguna capacidad sería contradictorio. El mismo criterio con el que el contrato de canje trata la contraseña no establecida (`GeometriaFactory-Contracts` CU-10001 §10 y CU-10008 §10).
- **No hay recuperación autónoma de contraseña olvidada**, porque no hay canal de correo (X-1). Lo que sí hay desde el `PRODUCT-INTAKE` 1.7, que **retiró la exclusión X-2**, es el **reseteo por el administrador** de CU-10004 FA-06: fija una provisoria, se la comunica al alumno y **la cuenta conserva todos sus trabajos** (RN-10012). La consecuencia que este caso de uso declaraba —que el remedio arrastraba los trabajos— **dejó de ser cierta** y no debe seguir citándose.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con su regla **RN-10013** y el invariante **INV-09**. **§5**: **FA-07** nueva, el ingreso de una cuenta reseteada, que **sí obtiene sesión** y queda confinada al cambio forzado de CU-10003; se declara explícitamente por qué no es un rechazo de ingreso. **§6**: `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado **pendiente**, con la distinción frente a `CONTRATO_CUENTA_NO_HABILITADA`: en aquél no hay sesión de ningún tipo. **§8**: CA-08 nueva. **§10**: la nota sobre la ausencia de recuperación **se reescribe**: X-2 quedó retirada, lo que sigue excluido es la recuperación autónoma por correo, y la afirmación de que el remedio arrastra los trabajos dejó de ser cierta. Sube minor: agrega un flujo alternativo, un código y un criterio de aceptación, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación contra el `PRODUCT-INTAKE` 1.8 y contra `GeometriaFactory-Contracts` CU-10008.** **(a)** La versión 1.1 se escribió sobre el enunciado de RN-10013 anterior a la precisión: **FA-07, §6 y CA-08 declaraban que la cuenta reseteada obtenía sesión y quedaba confinada**. El intake 1.8 §4.1 precisa que **se autentica pero no obtiene sesión de trabajo** —el sistema reconoce la provisoria y la deriva al cambio, que es el paralelo del primer ingreso con contraseña no fijada—, y así lo modelan ya `GeometriaFactory-Domain` (CU-10004 FA-03, no admisible) y `GeometriaFactory-Contracts` (CU-10008 §4 paso 5, sin respuesta de sesión). Los tres lugares se corrigen y §10 suma la nota con el fundamento. El cambio forzado sigue siendo un curso de CU-10003 sobre el shell de acceso: lo que cambia es que se llega **sin sesión**. **(b)** El código provisional `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado `[PENDIENTE]`, se reemplaza por el definitivo **`CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`**, que `GeometriaFactory-Contracts` CU-10008 §6 ya emitió; el rótulo se retira. **(c)** §9 suma CU-10008 a los contratos consumidos y la cabecera cita el intake **1.8**. Sube minor: corrige una lectura y fija dos identificadores, sin agregar ni quitar flujos. |
| 1.3 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-10016) y la precisión de F-04**: habilitar una cuenta produce su contraseña provisoria, con lo cual **el primer ingreso deja de tener código y curso propios** y recorre los del cambio obligatorio. **§5**: **FA-02** se rehace sobre la cuenta recién habilitada que ingresa con su provisoria, con el mismo código y el mismo destino que FA-07. **§6**: sale `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, retirado del conjunto cerrado del ensamblado por imposibilidad de su causa, y las condiciones pasan de seis a **cinco**; la fila del código de cambio requerido declara sus **dos orígenes** y registra cuál lo reemplazó. **§8**: **CA-04** se rehace y verifica que los dos orígenes reciben el mismo tratamiento. **La exigencia de que la credencial de sesión no aparezca nunca en el navegador no cambia**, y sigue siendo lo que este caso de uso existe para sostener. Sube minor. |
