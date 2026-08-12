# CU-02 — Contrato de administración de cuentas de alumno

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-02-Contrato-De-Administracion-De-Cuentas.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5; `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5; `00-Contexto/Vision-Producto.md` §9; `00-Contexto/Alcance-Producto.md` §4.1 (F-01, F-02, F-03, F-04, F-05) y §5 (X-1 vigente, X-3); `PRODUCT-INTAKE` **1.13** §4.1 (RN-01, RN-02, RN-06, RN-07, **RN-12**, RN-13, RN-14 y **RN-16**), §17.1.P.2 (**INV-09**), §17.4 P.2, P.3, P.5 y P.10, §17.5 P.3 y P.5, §14 (RA-03), §4 (**F-26**, F-03, **F-04** precisada), §6 (flujo 1), §7 (CL-6, **CL-7** reescrito), §9 (**X-2 retirada**)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de este proyecto de código; `08-Calidad-Y-Pruebas`

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
- [17. Compatibilidad de versión pública](#17-compatibilidad-de-versión-pública)

---

## 1. Propósito

Declarar los tipos de transferencia del ciclo de vida de una cuenta: el registro que hace el alumno sin elegir contraseña, el cambio de contraseña presentando la vigente, el listado de cuentas que ve el administrador, la orden de cambio de situación de una cuenta —habilitar, bloquear, rehabilitar— y la solicitud de baja, que es un tipo aparte porque exige la confirmación escrita y no es un valor del conjunto cerrado de situaciones de §3. El contrato transporta esa confirmación y no transporta ninguna forma de la contraseña almacenada.

**La solicitud de establecimiento de contraseña quedó retirada por `PRODUCT-INTAKE` 1.13, y la capacidad que sostenía no.** Hasta la 1.12, este contrato declaraba un tipo con la contraseña elegida por el alumno **sin ninguna credencial que lo identificara**: era el único tipo del ensamblado que expresaba una escritura anónima **de credencial**. **El registro de cuenta sigue siendo anónimo**, y su solicitud también: es la puerta por la que el alumno entra al laboratorio (`CU-01`). **RN-16** hace que habilitar produzca la contraseña provisoria, con lo cual el alumno llega a elegir la suya **ya identificado**, por la solicitud de cambio de contraseña de FA-02 —la misma que usa el cambio posterior a un reseteo—. El alumno sigue eligiendo su contraseña; lo que desapareció es el tipo que la transportaba sin credencial. El **resultado del cambio de situación** pasa, en contrapartida, a transportar la **contraseña provisoria en claro** cuando la situación pretendida es habilitada.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma las solicitudes de registro, de credencial y de cambio de situación, y consume el listado de cuentas |
| Código de la pieza de datos compilado contra el contrato | Sistema | Produce el listado y las respuestas de resultado sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara los valores admitidos de situación de cuenta y los campos de cada solicitud |

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El contrato declara el conjunto cerrado de situaciones de cuenta que el producto reconoce: pendiente, habilitada y bloqueada.
- El contrato declara los dos papeles fijos del producto y ningún esquema de permisos configurables, por la exclusión X-3 de `Alcance-Producto.md` §5.

## 4. Flujo principal

1. El código de la pieza pública arma la solicitud de registro con tres campos: correo, nombre y apellido. **No hay campo de contraseña.**
2. El código de la pieza de datos responde con el resultado del registro, que declara la situación inicial de la cuenta como pendiente.
3. El código de la pieza pública, actuando para el administrador, solicita el listado de cuentas.
4. El código de la pieza de datos produce la colección de elementos de listado de cuenta, cada uno con correo, nombre, apellido, situación y fecha de registro.
5. El código de la pieza pública arma la solicitud de cambio de situación con dos campos: identificador de la cuenta y situación pretendida. **La solicitud no transporta contraseña**: la provisoria no la escribe el administrador (RN-14).
6. El código de la pieza de datos responde con el resultado, que devuelve la situación resultante de la cuenta y, **cuando la situación pretendida es habilitada**, la **contraseña provisoria en claro** y la declaración de que la cuenta quedó con cambio de contraseña pendiente (RN-16, INV-09).
7. El administrador le comunica la provisoria al alumno **por fuera del producto**: no hay canal de correo y el contrato no declara ningún tipo que la transporte hacia él.
8. El código de la pieza pública, actuando ahora para el alumno, canja credenciales por CU-01 con la provisoria, recibe el código de desvío de CU-08 y deriva a la **solicitud de cambio de contraseña de FA-02**, con la provisoria como vigente y la nueva elegida por el alumno.
9. El código de la pieza de datos responde con el resultado del cambio, y a partir de ahí CU-01 vuelve a ser el camino de entrada, ya con respuesta de sesión.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador da de baja una cuenta | El contrato usa la solicitud de baja, que además del identificador exige el campo de **correo escrito como confirmación** y declara que la baja elimina también los trabajos de la cuenta | El flujo vuelve al paso 3, con el listado ya sin la cuenta dada de baja |
| FA-02 | El alumno cambia su contraseña estando dentro del laboratorio | El contrato usa la solicitud de cambio de contraseña, con dos campos: contraseña vigente y contraseña nueva. La vigente es obligatoria por contrato. **Es la misma solicitud del paso 8**, la del primer ingreso, y la misma del cambio posterior a un reseteo: desde `PRODUCT-INTAKE` 1.13 hay **un solo tipo** para las tres situaciones | El flujo vuelve al paso 9 |
| FA-03 | Es el primer arranque del laboratorio y todavía no existe cuenta de administrador | El contrato usa la solicitud de configuración de la cuenta de administrador, con correo y contraseña. El contrato no declara ningún campo que permita configurar una segunda | El flujo continúa en el paso 3 |
| FA-04 | El administrador resetea la contraseña de una cuenta de alumno | **No es este contrato**: la solicitud de reseteo y su resultado son de [CU-08](CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md), que es una familia de tipos propia. Lo que sí es de acá es lo que viene después: **la solicitud de cambio de contraseña de FA-02 se reutiliza tal cual** para el cambio obligatorio, con la provisoria como contraseña vigente | El flujo vuelve al paso 3, con el listado ya declarando el cambio pendiente de esa cuenta |
| FA-05 | El administrador **rehabilita** una cuenta bloqueada | El contrato usa la misma solicitud de cambio de situación, y el resultado trae **una provisoria nueva** y el cambio pendiente: rehabilitar es habilitar a los efectos de RN-16. No hay campo que distinga una habilitación de una rehabilitación, y no hace falta | El flujo continúa en el paso 7 |
| FA-06 | El administrador **bloquea** una cuenta, o cambia la situación a una que no es habilitada | El resultado devuelve la situación resultante y **0 provisorias**: sólo la habilitación produce una. El campo de provisoria del resultado queda sin valor, y esa ausencia es la señal de que no hay nada que comunicar | El flujo vuelve al paso 3 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el correo, el nombre o el apellido en el registro | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación: el código de la pieza pública corrige y reintenta |
| `CONTRATO_CORREO_YA_REGISTRADO` | El correo del registro ya pertenece a una cuenta | Respuesta de error de CU-06 con texto neutro. Terminación controlada |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación de la baja no coincide con el de la cuenta | Respuesta de error de CU-06. La baja no procede; recuperación por reintento con la confirmación correcta |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio de contraseña llega sin la contraseña vigente o con una que no corresponde | Respuesta de error de CU-06 con texto neutro. Terminación controlada |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Se intenta configurar una cuenta de administrador cuando ya existe una | Respuesta de error de CU-06. Terminación controlada: el contrato no ofrece camino alternativo |
| `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | Se pide una operación de **gobierno de las cuentas de la comisión** —listado de cuentas, cambio de situación o baja (F-03)—, y quien la pide no tiene el papel `Administrador` | Respuesta de error de CU-06 con texto neutro y **sin escritura**: la operación no ocurre y ningún estado cambia. Terminación controlada. Entra al conjunto cerrado por `PRODUCT-INTAKE` **1.29** §17.4 P.3 |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

**Un código que salió del conjunto cerrado por esta emisión, y conviene declararlo acá.** `CONTRATO_CONTRASENA_NO_ESTABLECIDA` describía a la cuenta habilitada que todavía no había establecido su contraseña, y la solicitud que lo remediaba era la de establecimiento que este contrato retiró. Con **RN-16** ninguna cuenta llega a estar habilitada sin contraseña, de modo que la causa desapareció: el código sale del conjunto cerrado de CU-06 y **no se recicla**. Quien busque hoy el desvío del primer ingreso encuentra `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` en CU-08.

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene la situación resultante de la cuenta afectada —y, si la situación pretendida fue habilitada, **la contraseña provisoria en claro** y el cambio pendiente declarado—, o la colección de elementos de listado de cuenta. **Ningún campo transporta la contraseña almacenada**, o sea su forma derivada: lo que viaja es el valor en claro, una vez, para que el administrador lo comunique, exactamente como en el resultado del reseteo de CU-08.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y la situación de la cuenta que ya conocía, sin cambio.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de registro del contrato | Se inspecciona su superficie pública | Declara exactamente tres campos —correo, nombre y apellido— y **0 campos de contraseña**, porque el registro no la elige |
| CA-02 | Una cuenta pendiente con correo `alumna@ejemplo.edu` | El administrador solicita el cambio de situación a habilitada | El resultado devuelve la situación resultante `Habilitada`, **1 contraseña provisoria en claro** y el cambio de contraseña pendiente declarado; y **0 campos** con la contraseña almacenada |
| CA-06 | El tipo de solicitud de cambio de situación | Se inspecciona su superficie pública | Declara exactamente dos campos —identificador y situación pretendida— y **0 campos de contraseña**, porque la provisoria no la escribe el administrador (RN-14) |
| CA-07 | Dos cuentas pendientes distintas | El administrador las habilita a las dos, y después bloquea y rehabilita la primera | Las **3** provisorias devueltas son distintas entre sí, y la del bloqueo son **0**: sólo la habilitación y la rehabilitación producen una |
| CA-08 | Una cuenta recién habilitada, con su provisoria | Se inspecciona la superficie pública del ensamblado buscando un tipo que acepte una contraseña nueva sin la vigente | Existe **1 solo** tipo de solicitud de cambio de contraseña, que sirve al primer ingreso, al cambio posterior a un reseteo y al cambio voluntario, y **0 tipos** del ensamblado aceptan una contraseña nueva sin credencial vigente |
| CA-03 | Una cuenta habilitada con correo `alumna@ejemplo.edu` | Se arma la solicitud de baja con el campo de confirmación en `otra@ejemplo.edu` | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CONFIRMACION_NO_COINCIDE` y la cuenta no se da de baja |
| CA-04 | El tipo de solicitud de cambio de contraseña | Se inspecciona su superficie pública | Declara la contraseña vigente como campo obligatorio: no existe forma válida del tipo que cambie la contraseña sin presentarla |
| CA-05 | Un elemento de listado de cuenta del administrador | Se inspecciona su superficie pública | Trae correo, nombre, apellido, situación y fecha de registro, y **0 campos** con la contraseña almacenada o con cualquier dirección de servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) —administrador único y papeles fijos—, [`RN-02`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) —el correo del alumno es único, que sostiene el código `CONTRATO_CORREO_YA_REGISTRADO`—, [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) —una cuenta que no está habilitada no obtiene sesión— y [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) —la baja arrastra los trabajos y exige confirmación escrita—, las cuatro de `GeometriaFactory-Domain`. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-03 tipos de registro y de credencial; US-04 tipos de listado y de cambio de situación de cuenta, **con la provisoria en el resultado de la habilitación**; US-05 solicitud de baja con confirmación escrita |
| Componentes esperados en 05 | Familia de tipos de transferencia de cuentas del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración del recorrido de alta de punta a punta —registro, habilitación **que devuelve la provisoria**, canje que devuelve el desvío, cambio de contraseña, ingreso—, de la habilitación que no repite provisoria (CA-07), de la inspección de superficie que verifica **0 tipos con contraseña nueva sin vigente** (CA-08), de la baja con confirmación errónea y del intento de configurar una segunda cuenta de administrador |

## 10. Notas y supuestos

- **Este contrato** no declara ningún tipo que transporte una contraseña provisoria: la solicitud de reseteo es de **CU-08**, que `PRODUCT-INTAKE` 1.7 hizo necesaria al incorporar la capacidad **F-26** y retirar la exclusión **X-2**. Lo que sigue sin existir en ningún tipo del ensamblado es el **enlace de recuperación**, porque no hay canal de correo: la exclusión **X-1** sigue vigente (`Alcance-Producto.md` §5). La redacción anterior de esta nota citaba las dos exclusiones juntas y quedó falsa en su primera mitad.
- La baja de una cuenta arrastra **todos** sus trabajos, cualquiera sea su estado, incluidos los que ya recibieron desenlace. El contrato no declara ningún campo que permita conservarlos: es invariante de dominio y no una opción del solicitante.
- **La baja y el reseteo son operaciones opuestas y no se confunden por su forma.** La solicitud de baja exige la confirmación escrita del correo y elimina la cuenta y todos sus trabajos (RN-07); la de reseteo, que vive en CU-08, no exige confirmación escrita y **conserva la cuenta y todos sus trabajos** (RN-12). Hasta `PRODUCT-INTAKE` 1.6 la baja era el único camino declarado ante una contraseña olvidada, y por eso el primer olvido costaba la cursada entera; **F-26** cierra ese agujero.
- **No queda ningún tipo del ensamblado que exprese una escritura anónima de credencial, y es lo que esta emisión cierra.** El tipo retirado —la solicitud de establecimiento de contraseña— era el único de esa clase; **la solicitud de registro sigue siendo anónima y debe seguirlo**, porque el registro de cuenta es anónimo por diseño (`PRODUCT-INTAKE` **1.15** §4.1). El retirado transportaba una contraseña nueva sin credencial vigente y sin acceso firmado. Toda operación que fija una contraseña se expresa hoy con la solicitud de cambio de FA-02, que exige la vigente. Es el enunciado de **RN-16** visto desde los tipos, y **agregar un tipo que acepte una contraseña nueva sin credencial se rechaza aunque compile** (§17).
- **La provisoria de la habilitación y la del reseteo son el mismo mecanismo con dos disparadores.** Las dos las produce el sistema, las dos viajan en claro una sola vez dentro del resultado, las dos dejan la marca puesta y las dos se cambian por la solicitud de FA-02. Que vivan en contratos de uso distintos es una consecuencia del recorte por familias de tipos —el reseteo tiene solicitud propia y la habilitación no—, y no una diferencia de tratamiento.
- Quién puede pedir el listado de cuentas se verifica en la pieza de datos. El contrato transporta el papel, no lo hace cumplir.
- La forma de los puntos de acceso pertenece a `GeometriaFactory-Api`.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los tipos de registro, credencial, listado de cuentas y cambio de situación, con la confirmación escrita de la baja como campo del contrato. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-13**: §1 dejaba leer «dar de baja» como una cuarta transición del conjunto cerrado de situaciones que §3 cierra en tres valores; se reformula para nombrar la baja como solicitud aparte, que es lo que FA-01 ya hacía. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-01` y `RN-07` de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4.1, que transcribe completas las once reglas del producto y da de alta `RN-02` y `RN-06`. Cambios: §9 suma las referencias por identificador a `RN-02`, que sostiene el código `CONTRATO_CORREO_YA_REGISTRADO`, y a `RN-06`; §10 declara que la baja arrastra los trabajos en cualquiera de los cuatro estados, incluidos los que ya recibieron desenlace, y que el contrato no ofrece campo para conservarlos. **Ningún tipo, campo ni criterio de aceptación de este contrato cambia**: el circuito de revisión no toca la administración de cuentas. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26**, la regla **RN-12**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. **Ningún tipo ni campo de este contrato cambia.** Cambios: **FA-04 nuevo**, que declara que la solicitud de reseteo pertenece a **CU-08** y que la solicitud de cambio de contraseña de FA-02 **se reutiliza tal cual** para el cambio obligatorio; §10 corrige una afirmación que quedó falsa —«el contrato no declara ningún tipo que transporte una contraseña provisoria… exclusiones X-1 y X-2»—, acotándola a este contrato y dejando en pie sólo X-1, la del enlace de recuperación por correo; y suma la nota que separa la baja del reseteo como operaciones opuestas. **Autor:** Analista Funcional + API Designer (AG-02) |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo. **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: el control de cambios tenía **filas con más celdas que columnas** —la celda de autor sobrante, sobre una tabla de tres columnas—, y el texto de esas filas se conserva íntegro: el autor pasa a leerse dentro de la celda de cambios, en lugar de en una cuarta columna que la tabla no declara. Cierra además la otra parte de `F26-27` que alcanza a este archivo: una **línea en blanco partía la tabla** del control de cambios y dejaba fuera de ella las filas siguientes; se retira, sin tocar el texto de ninguna fila. **Ninguna otra sección de este contrato de uso se toca**, y ningún tipo, campo, código ni criterio de aceptación cambia. Sube minor: repara la tabla de este control de cambios sin alterar lo que sus filas dicen. |
| 1.4 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**, que suprimen del producto la única escritura anónima de credencial que tenía. **§1**: se retira de la enumeración la **solicitud de establecimiento de contraseña** y se declara por qué —era el único tipo del ensamblado que transportaba una contraseña nueva sin credencial que identificara a quien la elegía— y qué la reemplaza: la solicitud de cambio de FA-02, con la provisoria como vigente. **La capacidad no desaparece**: el alumno sigue eligiendo su contraseña, ya identificado. **§4**: los pasos 5 a 8 se rehacen y el flujo pasa a **nueve** pasos; el resultado del cambio de situación suma **la provisoria en claro** y la declaración de cambio pendiente cuando la situación pretendida es habilitada. **§5**: FA-02 declara que es un solo tipo para las tres situaciones, y entran **FA-05** —rehabilitar produce provisoria nueva— y **FA-06** —bloquear no produce ninguna—. **§6**: se declara la salida de `CONTRATO_CONTRASENA_NO_ESTABLECIDA` del conjunto cerrado de CU-06, con su fundamento y con la constancia de que no se recicla. **§7**: la postcondición de éxito distingue el valor en claro, que sí viaja una vez, de la forma almacenada, que no viaja nunca. **§8**: CA-02 se rehace y entran **CA-06, CA-07 y CA-08**. **§9**: se actualizan la US-04 y las pruebas previstas. **§10**: entran las dos notas que declaran que no queda ningún tipo con escritura anónima de credencial y que la provisoria de la habilitación y la del reseteo son el mismo mecanismo con dos disparadores. **§17**: tres cláusulas nuevas. Sube minor: absorbe una decisión del Product Owner sobre un documento en estado `Propuesto`. |
| 1.5 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-16)**, que declara falsa la afirmación de 1.13 según la cual la regla deja al producto sin ninguna escritura anónima: el **registro de cuenta** de RF-03 es anónimo por diseño y debe seguir siéndolo. Lo que se eliminó es la escritura anónima **de credencial**. **§1** acota que el tipo retirado era el único que expresaba una escritura anónima de credencial, y deja escrito que la solicitud de registro sigue siendo anónima. **§10** acota la nota correspondiente en el mismo sentido. **§17** acota la cláusula de reposición. **La fila 1.4** de este control de cambios, que transcribía la afirmación ancha, se corrige del mismo modo. **Ningún tipo, campo, código ni criterio de aceptación cambia**, y CA-08 sigue siendo el que lo comprueba. Sube minor. |
| 1.6 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- **Esta emisión es un cambio incompatible**: **entra** al conjunto cerrado de CU-06 el código `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` para el rechazo por papel en el gobierno de las cuentas (`PRODUCT-INTAKE` **1.29** §17.4 P.3), y obliga al despliegue conjunto de las dos piezas desplegables (`RT-06`). **No recicla ningún identificador retirado.**
- Agregar una situación de cuenta al conjunto admitido es **cambio incompatible** de hecho, aunque compile: la pieza pública que no la contempla deja de cubrir todos los casos. Se trata como incompatible y obliga al despliegue conjunto.
- Quitar el campo de confirmación de la baja es incompatible y además contradice el criterio de aceptación CA-03.
- Agregar un campo opcional a un elemento de listado de cuenta es compatible, siempre que no viole el criterio CA-05.
- **La emisión 1.4 es un cambio incompatible** y obliga al despliegue conjunto de las dos piezas desplegables (`RT-06`): **sale** el tipo de solicitud de establecimiento de contraseña, **sale** del conjunto cerrado de CU-06 el código `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y el resultado del cambio de situación **suma** dos campos.
- **Reponer un tipo que acepte una contraseña nueva sin credencial vigente ni acceso firmado se rechaza aunque compile**: contradice CA-08 y **RN-16**, y devuelve al producto la única escritura anónima de credencial que tenía.
- **Agregar un campo de contraseña a la solicitud de cambio de situación se rechaza aunque compile**: contradice CA-06 y **RN-14**, con el mismo fundamento con el que CU-08 lo rechaza para la solicitud de reseteo.
