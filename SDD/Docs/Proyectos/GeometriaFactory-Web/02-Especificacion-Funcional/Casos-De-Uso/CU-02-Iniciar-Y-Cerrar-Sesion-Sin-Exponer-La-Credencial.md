# CU-02 — Iniciar y cerrar sesión sin exponer la credencial

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (tercer, cuarto y quinto criterio); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §5 (segundo criterio); `../../../../00-Contexto/Vision-Producto.md` §9.2 (pieza en su segundo referente, forma calificada de `Pendiente`); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-05, **F-26**), §4.1 (RN-01, RN-06, **RN-13**), §6 (flujo 1), §9 (**X-2 retirada**), §14 (RA-01, RA-03), §17.1.P.2 (**INV-09**), §17.6 P.3, **P.5**, P.11 punto 1
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
3. **La pieza pública invoca el contrato de canje de credenciales** de `GeometriaFactory-Contracts` CU-01 desde su servidor, servidor a servidor. Ningún guion del navegador participa.
4. La pieza de datos devuelve la credencial de sesión, el identificador de la persona, su correo y su papel.
5. La pieza pública **guarda la credencial de sesión en el estado del circuito**, en memoria del servidor del hosting público, y no la escribe en ninguna respuesta dirigida al navegador.
6. La pieza pública deja en el navegador únicamente una marca de sesión propia del circuito, que no transporta la credencial ni ningún dato de la cuenta.
7. La pieza pública arma el panel que corresponde al papel recibido y lleva a la persona a su ruta inicial: el listado de trabajos propios si el papel es de alumno, el listado de la comisión si es de administrador.
8. Cuando la persona cierra sesión, la pieza pública descarta la credencial de sesión del estado del circuito, invalida la marca de sesión del navegador y devuelve a la ruta de ingreso.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta está `Pendiente` o bloqueada | El contrato devuelve `CONTRATO_CUENTA_NO_HABILITADA` con su motivo. La pieza pública muestra el motivo tal como corresponde a la situación de la cuenta y **no otorga sesión**: no se guarda credencial de sesión y el navegador no recibe marca de sesión | El flujo vuelve al paso 1 |
| FA-02 | La cuenta está habilitada pero la persona todavía no estableció su contraseña | El contrato devuelve `CONTRATO_CONTRASENA_NO_ESTABLECIDA`. La pieza pública deriva a CU-03, flujo de establecimiento | El flujo se reanuda en el paso 1 una vez establecida la contraseña |
| FA-03 | La persona que ingresa es el administrador | El canje es idéntico y sólo cambia el valor del papel. La pieza pública arma el panel del administrador | El flujo continúa en el paso 7 |
| FA-04 | La persona pide una ruta del panel sin tener sesión | La pieza pública no arma la ruta y devuelve a la de ingreso, sin revelar qué había en la ruta pedida | El flujo vuelve al paso 1 |
| FA-05 | Un alumno con sesión pide una ruta del panel del administrador | La pieza pública no arma la ruta y devuelve al panel del alumno, sin revelar qué había en la ruta pedida | El flujo vuelve al paso 7 |
| FA-07 | La cuenta está habilitada y con contraseña, y el administrador se la **reseteó**: llega con la marca de cambio de contraseña pendiente | El canje **procede** —la persona ingresa, y la credencial de sesión se custodia igual que siempre—, pero la pieza pública la lleva al **cambio forzado** de CU-03 en lugar de al panel de su papel, y ninguna otra ruta queda disponible hasta que lo complete (RN-13, INV-09). **No es un rechazo de ingreso**: distinguirlo importa, porque la persona sí tiene que poder entrar para poder cambiarla | El flujo continúa en CU-03, FA-04 |
| FA-06 | El circuito se corta y se restablece | La pieza pública muestra su cartel de reconexión y, al restablecerse, la sesión sigue vigente porque la credencial nunca vivió en el navegador. El tratamiento del cartel es de CU-10 | El flujo vuelve al punto donde la persona estaba |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El correo o la contraseña no corresponden a ninguna cuenta | La pieza pública muestra un único mensaje que **no declara cuál de los dos campos falló**. Terminación controlada: no hay reintento automático |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta está `Pendiente` o bloqueada | Handoff a FA-01: se muestra el motivo y no se otorga sesión |
| `CONTRATO_CONTRASENA_NO_ESTABLECIDA` | Primer ingreso efectivo de una cuenta ya habilitada | Handoff a FA-02, que deriva a CU-03 |
| `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` [PENDIENTE, CU-04 §10] | La cuenta fue reseteada por el administrador y todavía no cambió la provisoria | Handoff a FA-07: **se otorga sesión** y la persona queda confinada al cambio forzado. Es la diferencia con `CONTRATO_CUENTA_NO_HABILITADA`, donde no hay sesión de ningún tipo |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el correo o la contraseña | La pieza pública señala el campo faltante. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito, sin dirección de servicio interno en el mensaje, y sin excepción sin manejar |

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
| CA-04 | Una cuenta habilitada que nunca estableció contraseña | La persona intenta ingresar | Es derivada al establecimiento de contraseña de CU-03 y no obtiene sesión en ese intento |
| CA-05 | Un alumno con sesión iniciada | Pide por dirección directa una ruta del panel del administrador | La pieza pública no arma esa ruta, lo devuelve a su propio panel y no revela qué contenía |
| CA-06 | Una sesión iniciada | La persona cierra sesión y vuelve a pedir una ruta del panel | Es devuelta a la ruta de ingreso; la credencial de sesión ya no está en el estado del circuito |
| CA-07 | Un recorrido completo de ingreso y navegación por el panel | Se inspecciona el tráfico de red del navegador | Cero peticiones del navegador hacia la pieza de datos, y ningún mensaje visible contiene una dirección de servicio interno |
| CA-08 | Una cuenta habilitada a la que el administrador le reseteó la contraseña | La persona ingresa con la provisoria | **Obtiene sesión** y aterriza en el cambio forzado de CU-03, no en el panel de su papel. Ninguna otra ruta de su papel se arma hasta que lo complete |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md), [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) |
| Reglas de negocio aplicables | [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md). Viven y se hacen cumplir en `GeometriaFactory-Domain` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-01](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md) completo, con FA-01, FA-02 y FA-03; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-03, US-04, US-05 |
| Componentes esperados en 05 | Página de ingreso, custodia de la credencial de sesión en el estado del circuito, y el mecanismo de protección de rutas por papel |
| Tests previstos en 08 | Guion de demostración de la etapa `c`, con la inspección del navegador que verifica CA-02; guion de la etapa `d` para FA-01 y FA-02 |

## 10. Notas y supuestos

- **CA-02 es el criterio que materializa la decisión más consecuente del producto.** El intake lo declara verificable con las herramientas de desarrollo (§17.6 P.5), y es la razón por la que la llamada a la pieza de datos la hace el servidor de la pieza pública y no el navegador: sin eso vuelven el contenido mixto, la negociación de origen cruzado y la exposición de la dirección del servidor propio.
- El tramo entre la pieza pública y la pieza de datos puede viajar sin cifrar, y el riesgo está **aceptado por escrito** aguas arriba (`PRODUCT-INTAKE` §11, RN-B5). Este caso de uso no lo reabre ni propone mitigarlo: esa decisión es de 05-Arquitectura-Tecnica.
- La protección de rutas de FA-04 y FA-05 es necesaria pero no suficiente: la pertenencia y el papel los verifica la pieza de datos en cada solicitud. Ocultar una ruta no es hacer cumplir una regla.
- **No hay recuperación autónoma de contraseña olvidada**, porque no hay canal de correo (X-1). Lo que sí hay desde el `PRODUCT-INTAKE` 1.7, que **retiró la exclusión X-2**, es el **reseteo por el administrador** de CU-04 FA-06: fija una provisoria, se la comunica al alumno y **la cuenta conserva todos sus trabajos** (RN-12). La consecuencia que este caso de uso declaraba —que el remedio arrastraba los trabajos— **dejó de ser cierta** y no debe seguir citándose.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con su regla **RN-13** y el invariante **INV-09**. **§5**: **FA-07** nueva, el ingreso de una cuenta reseteada, que **sí obtiene sesión** y queda confinada al cambio forzado de CU-03; se declara explícitamente por qué no es un rechazo de ingreso. **§6**: `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado **pendiente**, con la distinción frente a `CONTRATO_CUENTA_NO_HABILITADA`: en aquél no hay sesión de ningún tipo. **§8**: CA-08 nueva. **§10**: la nota sobre la ausencia de recuperación **se reescribe**: X-2 quedó retirada, lo que sigue excluido es la recuperación autónoma por correo, y la afirmación de que el remedio arrastra los trabajos dejó de ser cierta. Sube minor: agrega un flujo alternativo, un código y un criterio de aceptación, sin invalidar ninguna decisión previa. |
