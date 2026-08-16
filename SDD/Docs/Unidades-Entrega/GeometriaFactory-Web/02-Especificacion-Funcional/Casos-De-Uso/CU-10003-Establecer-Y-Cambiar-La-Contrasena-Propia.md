# CU-10003 — Establecer y cambiar la contraseña propia

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5 (segundo y cuarto criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13**, §4 (**F-04** precisada, F-05, F-03, **F-26**), §4.1 (**RN-10013 precisada**, **RN-10016**), §6 (flujo 1), §7 (**CL-7 reescrito**), §9 (X-1, **X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-7), §17.1.P.2 (**INV-09**), §17.6 P.5; [`GeometriaFactory-Contracts` CU-08008](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) §4 y §6
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

Permitir que la persona elija su contraseña en su primer ingreso, ya habilitada, y que después la cambie cuando quiera presentando la vigente. Es la única forma que tiene de administrar su credencial dentro del laboratorio.

**Desde `PRODUCT-INTAKE` 1.13 los tres cursos son uno solo, y el formulario también.** **RN-10016** declara que habilitar una cuenta produce una contraseña provisoria, con lo cual el primer ingreso deja de ser un formulario de dos campos sin credencial y pasa a ser el **mismo** formulario de tres campos del cambio: contraseña vigente —que es la provisoria que el administrador le comunicó—, nueva y su repetición. **La persona sigue eligiendo su contraseña**; lo que cambió es que llega a elegirla identificada. La ruta de establecimiento **sin credencial vigente deja de existir**.

Desde el `PRODUCT-INTAKE` **1.7** sostiene además un tercer curso, el **cambio forzado**: el de la persona a la que el administrador le reseteó la contraseña (F-26). Es el mismo formulario del cambio, con una diferencia que lo gobierna todo: **hasta que no lo complete, no llega a ninguna otra parte del sistema** (RN-10013, INV-09).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Persona con cuenta habilitada | Primario | Elige su contraseña por primera vez, la reemplaza presentando la vigente, o la reemplaza obligada tras un reseteo, presentando la provisoria |
| Pieza pública | Sistema | Presenta el formulario, arma la solicitud contra la pieza de datos y no conserva ninguna contraseña |
| Pieza de datos | Secundario | Registra la credencial derivada y devuelve el resultado |

## 3. Precondiciones

- Para el **primer ingreso**: la cuenta acaba de ser habilitada y tiene la marca de cambio de contraseña pendiente puesta, situación que CU-10002 detecta en su FA-02. **La persona no tiene sesión de trabajo**: lo que la identifica es la **provisoria** que el administrador le comunicó, y que presenta como contraseña vigente.
- Para el cambio: la persona tiene sesión iniciada por CU-10002 y conoce su contraseña vigente.
- Para el **cambio forzado**: la cuenta está marcada como con cambio de contraseña pendiente, situación que CU-10002 detecta en su FA-07. **La persona no tiene sesión de trabajo**: lo que la identifica es la **provisoria** que el administrador le comunicó y que presenta como contraseña vigente, igual que en el establecimiento el primer ingreso efectivo se resuelve sin sesión.
- La pieza pública no guarda estado propio: nada de lo que se escribe en este formulario sobrevive a la operación.

## 4. Flujo principal

1. La persona llega a la ruta de cambio de contraseña, derivada por CU-10002 FA-02 después de canjear con su provisoria.
2. La pieza pública presenta el formulario de **tres** campos —contraseña vigente, contraseña nueva y su repetición—, declarando **por qué** está ahí: la contraseña con la que entró es provisoria y tiene que reemplazarla.
3. La persona completa los tres campos y confirma.
4. La pieza pública verifica que las dos escrituras de la nueva coincidan antes de salir hacia la pieza de datos.
5. **La pieza pública invoca desde su servidor el contrato de cambio de contraseña** de `GeometriaFactory-Contracts` CU-10002 FA-02, con la provisoria como vigente. **Es el mismo contrato que consume el cambio posterior a un reseteo**, y no hay ningún tipo del ensamblado que acepte una contraseña nueva sin la vigente.
6. La pieza de datos responde con el resultado, la contraseña queda reemplazada y **la marca queda levantada**.
7. La pieza pública informa el resultado y devuelve a la ruta de ingreso de CU-10002, que a partir de ahora es el camino de entrada.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La persona, ya dentro del laboratorio, quiere cambiar su contraseña | La pieza pública presenta el formulario con tres campos —contraseña vigente, contraseña nueva y su repetición— e invoca el contrato de cambio de contraseña de `GeometriaFactory-Contracts` CU-10002 FA-02. **La contraseña vigente es obligatoria por contrato** | El flujo vuelve al panel de la persona, con la sesión vigente |
| FA-02 | Las dos escrituras de la contraseña nueva no coinciden | La pieza pública lo señala sin salir hacia la pieza de datos | El flujo vuelve al paso 3 |
| FA-03 | La persona abandona el primer ingreso sin completarlo | No queda nada guardado: la cuenta sigue habilitada, con su provisoria y con la marca puesta, y el próximo ingreso con la provisoria vuelve a derivar acá por CU-10002 FA-02. **La provisoria no vence por abandonar el formulario** | El flujo vuelve al paso 1 de CU-10002 |
| FA-04 | La persona llega al cambio forzado **después de un reseteo**, derivada por CU-10002 FA-07 y **sin sesión de trabajo** | Es **exactamente el flujo principal**, con la única diferencia de qué le dice el mensaje: le resetearon la contraseña en lugar de acabar de ser habilitada. Desde `PRODUCT-INTAKE` 1.13 los dos orígenes de la marca comparten formulario, contrato y curso; la pieza pública presenta el mismo formulario de tres campos, declarando **por qué** está ahí: le resetearon la contraseña y tiene que elegir una. **No hay «cancelar»**: no hay ningún estado previo al que volver, porque no hay sesión y ninguna otra ruta está disponible. Con el cambio aplicado, la marca se levanta y la pieza pública devuelve a la ruta de ingreso de CU-10002, **que a partir de ahí sí entrega sesión**, exactamente como en el flujo principal del establecimiento | El flujo termina en el paso 1 de CU-10002, ya con la contraseña nueva |
| FA-05 | La persona con cambio de contraseña pendiente pide **cualquier otra ruta** | La pieza pública la devuelve al cambio forzado, **sin revelar qué contenía la ruta pedida** y sin presentarlo como error: es la situación esperada. No hay sesión que acotar —el canje nunca la emitió—, y la verificación que lo hace cumplir no es ésta sino la de la pieza de datos en cada solicitud (§10) | El flujo vuelve a FA-04 |
| FA-06 | La persona con cambio de contraseña pendiente abandona el cambio forzado sin completarlo | No queda nada guardado y no hay sesión que cerrar. **La marca sigue puesta**: el próximo ingreso con la provisoria vuelve a derivar al cambio forzado. La provisoria no vence por abandonar el formulario ni por el paso del tiempo | El flujo vuelve al paso 1 de CU-10002 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio llegó sin la contraseña vigente, o con una que no corresponde | La pieza pública lo informa sobre el campo de contraseña vigente y **no aplica el cambio**. Terminación controlada |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta alguno de los campos del formulario | La pieza pública señala el campo que el contrato nombra. Recuperación por corrección y reintento |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta fue bloqueada entre la derivación y el envío del formulario | La pieza pública muestra el motivo y devuelve a la ruta de ingreso, sin establecer contraseña |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | La persona con la marca puesta pidió cualquier otra cosa que no fuera su propio cambio | La pieza pública la lleva al cambio forzado con el motivo declarado. **No es un error de la persona** y no se presenta como tal |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10010: estado degradado explícito, sin dirección de servicio interno, y con la posibilidad de reintentar |

## 7. Postcondiciones

- En caso de éxito del primer ingreso: la cuenta tiene la contraseña que la persona eligió, **la marca quedó levantada** y la persona puede ingresar por CU-10002.
- En caso de éxito de cambio: la contraseña anterior deja de servir y la sesión vigente se conserva.
- En caso de éxito de **cambio forzado**: **la marca queda levantada**, la provisoria deja de servir y el ingreso por CU-10002 vuelve a entregar sesión y a abrir todas las rutas del papel de la persona. **El administrador no conoce la contraseña nueva**: la eligió la persona y nunca pasó por su panel.
- En caso de fallo del cambio forzado: la marca sigue puesta y la persona sigue confinada al cambio. **Sus trabajos siguen todos ahí**: el reseteo no eliminó ninguno (RN-10012).
- En caso de fallo: la credencial no cambia y la persona conserva la que tenía.
- En ningún caso: la pieza pública conserva ninguna contraseña, ni la escribe en el navegador, ni la incluye en ningún mensaje.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta habilitada sin contraseña, derivada por CU-10002 FA-02 | La persona escribe `clave-nueva-01` dos veces y confirma | La contraseña queda establecida y el siguiente ingreso con `clave-nueva-01` entrega el panel |
| CA-02 | Una persona con sesión iniciada | Pide cambiar su contraseña sin escribir la vigente | El cambio no se aplica y el mensaje señala el campo de contraseña vigente |
| CA-03 | Una persona con sesión iniciada y contraseña vigente `clave-nueva-01` | Cambia a `clave-nueva-02` presentando la vigente | El cambio se aplica, el ingreso con `clave-nueva-01` deja de funcionar y el ingreso con `clave-nueva-02` funciona |
| CA-04 | El formulario de establecimiento | La persona escribe dos valores distintos en contraseña y repetición | La pieza pública lo señala y **no** emite ninguna solicitud hacia la pieza de datos |
| CA-05 | Un recorrido completo de establecimiento y de cambio | Se inspecciona el navegador con las herramientas de desarrollo | Ninguna contraseña ni credencial de sesión queda observable en el navegador |
| CA-06 | Una cuenta a la que el administrador le reseteó la contraseña, con tres trabajos | La persona ingresa con la provisoria | Llega al cambio forzado **sin obtener sesión** —el navegador no recibe marca de sesión y el estado del circuito no guarda credencial—, la superficie **declara por qué está ahí** y **no ofrece «cancelar»** |
| CA-07 | La misma persona en el cambio forzado | Pide por dirección directa el listado de sus trabajos, o cualquier otra ruta de su papel | Vuelve al cambio forzado, sin haber leído ni escrito nada, y sin que el mensaje revele qué contenía la ruta pedida |
| CA-08 | La misma persona | Completa el cambio presentando la provisoria como vigente | La marca queda levantada; el ingreso siguiente con la contraseña nueva **sí entrega sesión** y el panel, sus tres trabajos siguen ahí con sus estados, la provisoria deja de funcionar y el administrador **no conoce** la contraseña nueva |
| CA-09 | La misma persona, antes de cambiarla | Abandona el cambio forzado y vuelve a ingresar con la provisoria | Vuelve a terminar en el cambio forzado: la marca sólo la levanta el cambio efectivo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-02006`](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), y [**`RN-02013`**](../../../GeometriaFactory-Api/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), que ya tiene archivo en `GeometriaFactory-Domain`. La derivación de la credencial y su verificación viven en `GeometriaFactory-Infrastructure`; la admisibilidad de la cuenta y el invariante INV-09, en `GeometriaFactory-Domain` y en `GeometriaFactory-Application` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08002](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08002-Contrato-De-Administracion-De-Cuentas.md) pasos 7 y 8, y FA-02 —que [`CU-08008`](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) §3 **reutiliza y no redeclara** para el cambio forzado—; [`CU-08006`](../../../../Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-10006, US-10007, US-10028, US-10029 |
| Componentes esperados en 05 | **Una sola** página de cambio de contraseña, que sirve al primer ingreso, al cambio posterior a un reseteo y al cambio voluntario dentro del panel; y el **guard de cambio pendiente** que confina a la cuenta marcada. La página de establecimiento sin credencial vigente **no se construye**: dejó de existir con RN-10016 |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para el cambio con contraseña vigente, y de la etapa `d` para el **primer ingreso con la provisoria como vigente**, con la comprobación de que recorre el mismo formulario y el mismo contrato que el cambio posterior a un reseteo, y de que **0 rutas** de esta pieza aceptan una contraseña nueva sin la vigente |

## 10. Notas y supuestos

- Este caso de uso existe porque **no hay canal de correo**: **ninguna contraseña se transporta por un canal del sistema hacia la persona**, y la definitiva la elige ella. Lo que cambió con `PRODUCT-INTAKE` 1.13 es que la credencial inicial ya no la elige la persona sino que la produce el sistema y **se la comunica el administrador por fuera del producto**. Es la traducción del flujo 1 del intake con la precisión de F-04.
- **Un olvido de contraseña ya no se resuelve por baja y alta nueva.** Hasta el `PRODUCT-INTAKE` 1.6 ésa era la consecuencia aceptada y **arrastraba todos los trabajos de la cuenta**; 1.7 retiró la exclusión X-2, reescribió CL-7 e incorporó el **reseteo** de CU-10004 FA-06, que conserva la cuenta y sus trabajos. Lo que sigue sin existir es la **recuperación autónoma**: no hay canal de correo (X-1), y el remedio pasa siempre por el administrador.
- **No queda ninguna ruta de esta pieza que fije una contraseña sin credencial vigente.** Era la de establecimiento del primer ingreso, y **RN-10016** la suprimió. Es lo que hace que la afirmación de arriba —el alumno sigue eligiendo su contraseña— no vuelva a abrir el agujero que la decisión cierra: elige, pero identificado.
- **Acotar rutas no hace cumplir el confinamiento.** FA-05 describe lo que esta pieza ofrece; quien impide efectivamente que una cuenta marcada lea o escriba es la pieza de datos, que verifica la marca en cada solicitud (invariante INV-09, ejercido en `GeometriaFactory-Application`). Es el mismo criterio con el que RT-09 trata la protección de rutas por papel, y por eso CA-07 fuerza la solicitud **sin pasar por la pantalla**.
- **Al cambio forzado se llega sin sesión de trabajo**, y es lo que el `PRODUCT-INTAKE` **1.8** precisa sobre RN-10013: la cuenta con provisoria se autentica, el sistema la reconoce y la deriva acá, sin emitir sesión. Hasta el intake 1.12 este curso era el **paralelo** del establecimiento del flujo principal; con **RN-10016** dejó de ser un paralelo y pasó a ser **el mismo curso**, porque el flujo principal es hoy un cambio con la provisoria como vigente. Los dos se resuelven sobre el shell de acceso y los dos terminan devolviendo a la ruta de ingreso—, y por eso «no hay cancelar» no es una decisión de superficie sino la descripción de lo que hay: no existe un estado previo al que volver.
- **La provisoria no vence por tiempo ni por abandonar el formulario.** Lo único que la termina es el cambio efectivo. El producto no declara vencimiento y esta categoría **no lo inventa**: si el Product Owner lo quisiera, sería una regla nueva aguas arriba.
- Las exigencias de forma de la contraseña —longitud, composición— no las fija esta categoría. Si el producto las adopta, se declaran en 05-Arquitectura-Tecnica y se hacen cumplir del lado de la pieza de datos.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con su regla **RN-10013**, el invariante **INV-09**, el caso límite **CL-7 reescrito** y la exclusión **X-2 retirada**. **§1**: la superficie suma un tercer curso, el **cambio forzado** de quien fue reseteado. **§3**: precondición del cambio forzado, con la provisoria como credencial vigente. **§5**: **FA-04**, **FA-05** y **FA-06** nuevas —llegada al cambio forzado sin salida, cualquier otra ruta devuelta sin revelar su contenido, y el cierre de sesión que no levanta la marca—. **§6**: `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado **pendiente** porque el contrato todavía no existe en `GeometriaFactory-Contracts` (CU-10004 §10). **§7**: dos postcondiciones nuevas, incluida la que declara que **el administrador no conoce la contraseña nueva**. **§8**: CA-06 a CA-09 nuevas, una de ellas forzando la solicitud sin pasar por la pantalla. **§10**: la nota que declaraba a la baja como único remedio del olvido **se reescribe**, porque 1.7 la volvió falsa; se suman la precisión de que acotar rutas no hace cumplir el confinamiento, y la de que la provisoria no vence por tiempo, que esta categoría **no inventa**. Sube minor: agrega un curso, tres flujos alternativos y cuatro criterios de aceptación, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación contra el `PRODUCT-INTAKE` 1.8, contra `GeometriaFactory-Contracts` CU-10008 y contra las reglas ya emitidas en `GeometriaFactory-Domain`.** **(a)** La versión 1.1 modelaba el cambio forzado **con sesión iniciada**, sobre el enunciado de RN-10013 anterior a la precisión. El intake 1.8 §4.1 declara que la cuenta con provisoria **se autentica y no obtiene sesión de trabajo**; se corrigen §3, FA-04, FA-05, FA-06, la postcondición del cambio forzado y CA-06, CA-08 y CA-09, y §10 suma la nota que declara el paralelo con el establecimiento. El curso sigue siendo el tercero de este caso de uso y sigue viviendo sobre el shell de acceso. **(b)** El código provisional `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE`, rotulado `[PENDIENTE]`, se reemplaza por **`CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`**, emitido por `GeometriaFactory-Contracts` CU-10008 §6; el rótulo se retira. **(c)** **Punto abierto cerrado**: §9 declaraba a RN-10013 «todavía sin archivo en `GeometriaFactory-Domain`», y el archivo existe —`Reglas-De-Negocio/RN-10013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md`—; la cita pasa a ser un enlace. **(d)** La cabecera cita el intake **1.8** y §9 declara la reutilización de la solicitud de cambio que CU-10008 §3 no redeclara. Sube minor: corrige una lectura, fija un identificador y cierra un punto abierto. |
| 1.3 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-7, que declara que no hay recuperación **autónoma** y que el alumno depende del administrador, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún curso, flujo alternativo, código de rechazo, postcondición ni criterio de aceptación de este caso de uso cambia.** Sube minor: corrige una referencia a una fila retirada. |
| 1.4 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-10016) y la precisión de F-04**: habilitar una cuenta produce su contraseña provisoria, con lo cual **los tres cursos de este caso de uso pasan a ser uno solo** y el formulario también. **§1** declara el cambio y deja escrito que la capacidad no desaparece: la persona sigue eligiendo su contraseña, ya identificada. **§3** rehace la precondición del primer ingreso, que ahora exige la provisoria como credencial vigente. **§4** reescribe los siete pasos sobre el formulario de **tres** campos y el contrato de cambio de `CU-10002` FA-02, en lugar del contrato de establecimiento **que se retiró del ensamblado**. **§5**: FA-03 se rehace y **FA-04** pasa a declararse como el mismo curso que el flujo principal, con la sola diferencia del mensaje. **§7** suma el levantamiento de la marca a la postcondición del primer ingreso. **§9**: los componentes esperados pasan a **una sola** página, con la constancia de que la de establecimiento sin credencial vigente **no se construye**; las pruebas previstas suman la comprobación de **0 rutas** que acepten una contraseña nueva sin la vigente. **§10** precisa el alcance de la ausencia de canal de correo, registra que el paralelo con el cambio forzado dejó de ser paralelo, y suma la nota que declara la ruta suprimida. **El título del caso de uso no cambia**, para no romper las citas de las demás secciones. Sube minor. |
