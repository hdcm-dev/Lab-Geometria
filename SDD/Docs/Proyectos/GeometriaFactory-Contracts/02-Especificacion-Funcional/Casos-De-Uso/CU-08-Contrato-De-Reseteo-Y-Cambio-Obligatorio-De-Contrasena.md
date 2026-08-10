# CU-08 — Contrato de reseteo y de cambio obligatorio de contraseña

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5; `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5; `00-Contexto/Vision-Producto.md` §9; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE` **1.8** §4 (**F-26**, F-03), §4.1 (**RN-12**, **RN-13 precisada**, RN-07), §17.1.P.2 (**INV-09**), §7 (**CL-7** reescrito), §9 (**X-2 retirada**, X-1 vigente), §17.4 P.2, P.3, P.5 y P.8, §17.5 P.3 y P.5, §14 (**RA-01**, RA-03)
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

Declarar los tipos de transferencia del circuito que la capacidad **F-26** incorpora: el **reseteo de contraseña**, con el que el administrador le hace fijar a una cuenta de alumno una contraseña provisoria **que el sistema produce y le devuelve para que la comunique**, y el **cambio obligatorio**, con el que esa cuenta la reemplaza antes de poder hacer cualquier otra cosa.

Las dos mitades forman un solo contrato de uso porque forman un solo circuito: la primera **pone** una condición que sólo la segunda **levanta**, y ninguna de las dos se entiende sin la otra. El criterio de recorte de esta categoría es por familias de tipos de transferencia, y ésta es una familia nueva —solicitud de reseteo, resultado de reseteo y el código de error que desvía al cambio— que no existía en el ensamblado.

Lo que este contrato fija, sobre todo, es **qué se conserva**: el resultado del reseteo declara la situación de la cuenta y **no declara ningún campo por el que los trabajos del alumno se pierdan**. Es la contracara exacta de la solicitud de baja de CU-02 FA-01, que sí los arrastra.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Código de la pieza pública compilado contra el contrato | Primario | Arma la solicitud de reseteo cuando actúa para el administrador, y la de cambio de contraseña cuando actúa para el alumno reseteado |
| Código de la pieza de datos compilado contra el contrato | Sistema | **Produce la contraseña provisoria**, los resultados y el código de error que desvía al cambio, sobre los mismos tipos |
| Ensamblado de contratos | Sistema | Declara los campos de cada solicitud y la ausencia de todo campo que permita conservar o descartar trabajos |

No hay actor humano. El administrador y el alumno pertenecen a los casos de uso de `GeometriaFactory-Web`.

## 3. Precondiciones

- Los dos extremos están compilados contra la misma versión del ensamblado de contratos.
- El contrato ya declara los dos papeles fijos del producto y el conjunto cerrado de situaciones de cuenta de CU-02 §3, que **este contrato no amplía**: el reseteo no agrega una cuarta situación.
- El contrato ya declara la solicitud de cambio de contraseña de CU-02 FA-02, con sus dos campos —contraseña vigente y contraseña nueva—. **Este contrato la reutiliza y no la redeclara.**

## 4. Flujo principal

1. El código de la pieza pública, actuando para el administrador, arma la **solicitud de reseteo** con un solo campo: el identificador de la cuenta. **La solicitud no transporta contraseña**: la provisoria no la escribe el administrador.
2. El código de la pieza de datos **genera** la contraseña provisoria y responde con el **resultado del reseteo**, que declara la situación de la cuenta —la misma que tenía—, que la cuenta quedó con **cambio de contraseña pendiente** y **la contraseña provisoria en claro**, para que el administrador pueda comunicarla.
3. El administrador le comunica la contraseña provisoria al alumno **por fuera del producto**: no hay canal de correo y el contrato no declara ningún tipo que la transporte hacia el alumno.
4. El código de la pieza pública, actuando ahora para el alumno, canja credenciales por CU-01 con la contraseña provisoria.
5. El código de la pieza de datos **no produce respuesta de sesión**: produce el tipo de error de CU-06 con el código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`.
6. El código de la pieza pública deriva a la **solicitud de cambio de contraseña** de CU-02 FA-02, con la provisoria como contraseña vigente y la nueva elegida por el alumno.
7. El código de la pieza de datos responde con el resultado del cambio, y a partir de ahí CU-01 vuelve a ser el camino de entrada, ya con respuesta de sesión.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador resetea una cuenta que ya tiene el cambio de contraseña pendiente | El contrato usa la misma solicitud de reseteo. El resultado declara la misma situación de cuenta, el mismo cambio pendiente y **una provisoria nueva**: no hay campo que distinga un primer reseteo de un segundo, y no hace falta | El flujo continúa en el paso 3 |
| FA-02 | El alumno reseteado intenta cualquier otra operación del producto en lugar del cambio | La pieza de datos produce el tipo de error de CU-06 con el mismo código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`, cualquiera sea la operación pedida. **El código es uno solo** y no se multiplica por operación: lo que el consumidor tiene que hacer es siempre lo mismo, derivar al cambio | El flujo vuelve al paso 6 |
| FA-03 | El administrador cambia su propia contraseña | No es este contrato: usa la solicitud de cambio de contraseña de CU-02 FA-02, con su vigente. **El reseteo no procede sobre la cuenta de administrador** y el contrato lo declara con código propio en §6 | Termina el flujo |
| FA-04 | El administrador resetea una cuenta de alumno que **no está habilitada** —`Pendiente` o `Bloqueada`— | El contrato usa la misma solicitud y produce el mismo resultado: **el reseteo no exige que la cuenta esté habilitada**, porque no cambia la situación y el resultado la declara **sin cambio**. Habilitar y resetear son dos operaciones independientes, y el administrador las ejerce en el orden que quiera. La cuenta sigue sin obtener sesión mientras no esté habilitada, pero por el código de situación de CU-01 y no por éste | El flujo continúa en el paso 3 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | La cuenta tiene una contraseña provisoria sin cambiar y se pide el canje de credenciales o cualquier otra operación | Respuesta de error de CU-06 con texto neutro y su motivo. **No** se produce respuesta de sesión. Handoff al contrato de cambio de contraseña de CU-02 FA-02 |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se pide el reseteo sobre la cuenta con papel `Administrador` | Respuesta de error de CU-06 con texto neutro. Terminación controlada: el contrato no ofrece camino alternativo, y el cambio de la propia contraseña es CU-02 FA-02. **Su fuente es `RN-15`**, que lo enuncia y lo ancla en **INV-08** |
| `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` | Se pide el reseteo sobre una cuenta de alumno que **todavía no estableció su contraseña** | Respuesta de error de CU-06 con texto neutro. Terminación controlada: no hay contraseña que reemplazar, y el camino que ya existe es que la persona la establezca en su primer ingreso, por CU-02 FA-02 y `CONTRATO_CONTRASENA_NO_ESTABLECIDA` de CU-01. **Es un código propio y no la reutilización de aquél**: el consumidor es otro —el administrador y no la persona— y lo que le queda por hacer es otro, esperar en lugar de establecer |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | La solicitud de reseteo llega sin identificador de cuenta | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación: el código de la pieza pública corrige y reintenta |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio llega sin la contraseña vigente o con una que no corresponde a la provisoria | Respuesta de error de CU-06 con texto neutro. Terminación controlada, y **el cambio pendiente sigue puesto** |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

**Tres códigos nuevos y tres reutilizados.** El conjunto cerrado de CU-06 pasa de catorce a **diecisiete**; los otros tres ya existían con la misma causa. **Ninguno de los tres nuevos es la cuenta no habilitada**: el reseteo no la rechaza, y por eso no hay código que la nombre. No se declara ninguna **señal declarada que no es error**: las tres del ensamblado siguen siendo las de CU-03 §6.1, CU-04 §6.1 y CU-05 §6.1, y este contrato no agrega ninguna, porque el cambio pendiente **sí** impide la operación pedida y por lo tanto es un error transportado y no una señal.

## 7. Postcondiciones

- En caso de éxito del reseteo: el código de la pieza pública tiene la situación de la cuenta **sin cambio**, la declaración de cambio de contraseña pendiente y **la contraseña provisoria en claro**, que es lo único que el administrador tiene que comunicar. Ningún campo del resultado transporta la contraseña **almacenada** —su forma derivada—, ni ninguna referencia a los trabajos de la cuenta.
- En caso de éxito del cambio: el código de la pieza pública tiene el resultado del cambio y la cuenta vuelve a canjear credenciales con normalidad por CU-01.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y **el cambio pendiente queda como estaba**. El contrato no deja estado parcial, porque los tipos de transferencia no tienen comportamiento.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de reseteo del contrato | Se inspecciona su superficie pública | Declara exactamente **un** campo —identificador de cuenta—, **0 campos** de contraseña, porque la provisoria no la escribe el administrador, y **0 campos** que permitan conservar, descartar o referenciar los trabajos de la cuenta: el reseteo no puede expresarse como una baja |
| CA-02 | El tipo de resultado del reseteo | Se inspecciona su superficie pública | Declara la situación de la cuenta, el cambio de contraseña pendiente y **la contraseña provisoria en claro**, y **0 campos** con la contraseña almacenada, con su forma derivada o con una dirección de servicio interno (`RT-01`) |
| CA-03 | Una cuenta de alumno habilitada con 3 trabajos, uno de ellos en estado `Finalizado` | El administrador la resetea | El resultado devuelve la situación `Habilitada`, el cambio pendiente, una provisoria, y el listado de trabajos de CU-04 sigue trayendo los **3** con sus mismos estados |
| CA-04 | Una cuenta con cambio de contraseña pendiente | El código de la pieza pública canja sus credenciales con la provisoria | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`; **no** se produce respuesta de sesión, y el tipo de respuesta de sesión sigue declarando cuatro campos, sin ninguno agregado para este caso |
| CA-05 | La misma cuenta con cambio pendiente | El código de la pieza pública pide el listado de trabajos, el detalle de un trabajo y el envío de uno nuevo | Las 3 respuestas son el tipo de error de CU-06 con el **mismo** código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`: 1 código para todas las operaciones, y 0 trabajos leídos o escritos |
| CA-06 | La misma cuenta | El alumno cambia la contraseña por la solicitud de CU-02 FA-02, con la provisoria como vigente | El resultado es exitoso y el canje siguiente produce respuesta de sesión; el cambio pendiente ya no se declara |
| CA-07 | La cuenta con papel `Administrador` | Se arma la solicitud de reseteo con su identificador | La respuesta es el tipo de error de CU-06 con código `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta no se resetea |
| CA-08 | Una cuenta de alumno en situación `Bloqueada`, con contraseña establecida y 2 trabajos | El administrador la resetea | El resultado es exitoso: devuelve la situación `Bloqueada` —**sin cambio**—, el cambio pendiente, una provisoria, y los **2** trabajos siguen estando. **0 códigos de error** se producen por la situación de la cuenta |
| CA-09 | Una cuenta de alumno habilitada que **todavía no estableció** su contraseña | El administrador la resetea | La respuesta es el tipo de error de CU-06 con código `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, y **0 provisorias** se devuelven |
| CA-10 | Dos cuentas de alumno distintas | El administrador resetea las dos, y después vuelve a resetear la primera | Las **3** provisorias devueltas son distintas entre sí: la generación **no repite valor entre cuentas ni entre reseteos**, y ninguna de las tres se deriva del identificador de la cuenta ni de su correo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-12`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) —el reseteo conserva la cuenta y sus trabajos, que es lo que CA-01 y CA-03 verifican—, [`RN-13`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) —que sostiene el código `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` y CA-05—, [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) y [`RN-15`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-15-Reseteo-Independiente-Del-Estado-De-Cuenta.md) —**la fuente única del cierre sobre la cuenta de administrador**, que el `PRODUCT-INTAKE` 1.10 escribe dentro del enunciado de RN-15 y ancla en **INV-08**; RN-15 sostiene además FA-04 y CA-08—, [`RN-14`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-14-Provisoria-Producida-Por-El-Sistema.md) —las dos propiedades del valor generado que CA-10 verifica— y [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) **por contraste**: es la regla que este contrato existe para no disparar. Las seis de `GeometriaFactory-Domain`. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-21 tipos de reseteo de contraseña con conservación de trabajos; US-22 desvío al cambio obligatorio con código propio |
| Componentes esperados en 05 | Familia de tipos de transferencia de reseteo y cambio obligatorio del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración del circuito de punta a punta —reseteo, canje que devuelve el código de desvío, cambio, canje exitoso—; prueba de que los trabajos sobreviven al reseteo en los cuatro estados; prueba del reseteo sobre cuenta **no habilitada**, que procede y devuelve la situación sin cambio (CA-08); prueba del reseteo sobre cuenta **sin contraseña establecida**, que devuelve su código propio (CA-09); prueba de que tres provisorias sucesivas son distintas entre sí (CA-10); prueba de que las operaciones de lectura y de escritura devuelven el mismo código con el cambio pendiente; e inspección de superficie pública para CA-01 y CA-02 |

## 10. Notas y supuestos

- **RA-01 no se afloja acá, y conviene decirlo porque es un circuito de credenciales.** Ningún JavaScript del navegador invoca la API: la solicitud de reseteo la arma el **servidor** de la pieza pública y viaja servidor a servidor, exactamente igual que el canje de CU-01 y que la baja de CU-02. El navegador nunca alcanza la API (`PRODUCT-INTAKE` §14 RA-01). Un formulario de reseteo que llamara por su cuenta a la pieza de datos reabriría de una vez el contenido mixto, el CORS y la exposición de la dirección del servidor propio, que es lo que RA-01 sostiene.
- **La contraseña provisoria la produce la pieza de datos y viaja en claro dentro del resultado del reseteo**, en el sentido inverso al de la contraseña presentada en el canje de CU-01 y al de la elegida en el establecimiento de CU-02. Lo que `RT-01` prohíbe es transportar la **contraseña almacenada** —su forma derivada— y ninguna respuesta de este contrato la lleva: lo que devuelve es el valor en claro, una vez, para que el administrador lo comunique. La derivación y la generación son de `GeometriaFactory-Infrastructure`.
- **El cierre sobre la cuenta de administrador tiene desde el `PRODUCT-INTAKE` 1.10 una fuente única, y este contrato la adopta.** Hasta la 1.9 ninguna sección del intake lo declaraba y cada proyecto de código lo anclaba donde podía —éste en `RN-01` e `INV-05`, `GeometriaFactory-Domain` en `INV-08`—, que es el hallazgo `F26-24` de `SDD/Docs/Audit/F26-Propagacion-r1.md`. La fuente es ahora **`RN-15`**, cuyo enunciado cierra con «sigue sin admitirse sobre la cuenta de administrador (INV-08)»: el cierre es **de papel y no de situación de cuenta**, que es exactamente lo que hace compatible a FA-03 con FA-04. `RN-01` e `INV-05` siguen siendo verdaderos y siguen citados, pero como **fundamento** —administrador único— y no como fuente del cierre.
- **Quién produce la provisoria lo decidió el Product Owner y el contrato lo declara: la produce el sistema, no la escribe el administrador.** El fundamento es de uso y está registrado acá para que no se reabra: si la escribe el docente, termina siendo la misma clave para toda la comisión. De ahí las dos propiedades que este contrato **exige del valor devuelto**, y que CA-10 verifica: **no es adivinable** —no se deriva del identificador de la cuenta, de su correo ni de ningún otro dato de la solicitud— y **no se repite entre cuentas ni entre reseteos**. El contrato **no declara mecanismo**: cómo se produce un valor con esas dos propiedades es de `05-Arquitectura-Tecnica` y de `GeometriaFactory-Infrastructure`.
- **El contrato no transporta la provisoria hacia el alumno.** El administrador se la comunica por fuera del producto: no hay canal de correo, la exclusión **X-1** sigue vigente y la que se retiró es **X-2**. Ningún tipo de este ensamblado declara un enlace de recuperación.
- **Un solo código para todas las operaciones bloqueadas**, y es una decisión de contrato. Multiplicarlo por operación —uno para el listado, otro para el envío— daría al consumidor información que no usa: el trabajo que le queda es siempre el mismo, derivar al cambio. Es el mismo criterio con el que `CONTRATO_TRABAJO_NO_ENCONTRADO` cubre tres causas distintas en CU-06.
- **Decisión derivada: el desvío viaja como respuesta de error y no como campo de la respuesta de sesión.** Ninguna fuente declara la forma. Se adopta la del precedente exacto del ensamblado: `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, que CU-01 §10 fundamenta en que `PRODUCT-INTAKE` §17.5 P.5 enumera **cuatro** reclamos de la credencial de sesión y ningún quinto dato. Una respuesta de sesión con una marca sería un quinto dato, y además emitiría una credencial de sesión a una cuenta que por **INV-09** no ejerce ninguna capacidad. **La tensión que esta nota declaraba quedó resuelta a favor de este modelo**: la versión 1.7 del intake decía que la cuenta reseteada «ingresa», y la **1.8** precisa que **se autentica pero no obtiene sesión de trabajo**, con el mismo fundamento —la contradicción con INV-09— y con el paralelo del primer ingreso con contraseña no fijada. El punto abierto que `GeometriaFactory-Domain` `Especificacion-Funcional.md` §9 sostenía quedó cerrado en su versión 1.5.
- La forma de los puntos de acceso —rutas y verbos— pertenece a `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.5 P.3).

## 11. Control de cambios

| Versión | Fecha | Descripción | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la capacidad **F-26** que `PRODUCT-INTAKE` 1.7 incorpora como `Must Have`, con las reglas **RN-12** y **RN-13**, el invariante **INV-09**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. Declara la familia de tipos del reseteo y del cambio obligatorio, con la solicitud de cambio de contraseña de CU-02 FA-02 **reutilizada y no redeclarada**; dos códigos de error nuevos que llevan el conjunto cerrado de CU-06 de catorce a **dieciséis**, y tres reutilizados; siete criterios de aceptación, con CA-01 y CA-03 verificando que **los trabajos se conservan** y CA-05 que un solo código cubre todas las operaciones bloqueadas. Deja declaradas la vigencia de **RA-01** sobre este circuito y una **decisión derivada** con su tensión: el desvío viaja como respuesta de error y no como campo de la respuesta de sesión, por el precedente de `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, aunque RN-13 hable de «ingresa». | Analista Funcional + API Designer (AG-02) |
| 1.1 | 2026-08-09 | **Absorbe la precisión de `PRODUCT-INTAKE` 1.8 §4.1 sobre RN-13**, que la emisión 1.0 de este contrato ayudó a disparar. §10 declaraba una **tensión** entre el «ingresa» de RN-13 y el modelo sin sesión que este contrato adoptó; el intake la resolvió **a favor de este modelo**: la cuenta con provisoria se autentica y **no obtiene sesión de trabajo**. La nota pasa de declarar una tensión a registrar su resolución, y la cabecera cita el intake **1.8**. **Ningún tipo, campo, código de error ni criterio de aceptación cambia**: §4 paso 5 y CA-04 ya decían que no se produce respuesta de sesión. Sube minor: cierra una tensión declarada sin tocar la superficie del contrato, y por lo tanto **§17 no se reabre**: esta versión no agrega ningún cambio incompatible sobre la 1.0. | Analista Funcional + API Designer (AG-02) |
| 1.2 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26.** **Decisión A: resetear no exige que la cuenta esté habilitada** —el reseteo es una operación sobre la credencial y no toca la situación de la cuenta, de modo que el administrador puede resetear y habilitar en el orden que quiera, sin acordarse de una secuencia—. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador** —si la escribe el docente, termina siendo la misma clave para toda la comisión—. Este contrato declaraba lo contrario de B y no se pronunciaba sobre A. **§4 pasos 1 y 2**: la solicitud de reseteo pasa de dos campos a **uno** —el identificador de cuenta— y deja de transportar contraseña; el resultado suma **la provisoria en claro**, que la pieza de datos genera. **§5**: **FA-04** nueva, el reseteo sobre una cuenta `Pendiente` o `Bloqueada`, que **procede**; FA-01 registra que el segundo reseteo devuelve una provisoria nueva. **§6**: entra `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, la segunda de las dos causas que `GeometriaFactory-Web` CU-04 §10 declaraba sin código —la primera, la cuenta no habilitada, **desaparece** con la decisión A y por eso no recibe código—; `CONTRATO_CAMPO_REQUERIDO_AUSENTE` deja de nombrar la contraseña provisoria entre los campos posibles. Los códigos nuevos de este contrato pasan de dos a **tres** y el conjunto cerrado de CU-06, de dieciséis a **diecisiete**. **§7**: el resultado transporta la provisoria en claro y sigue sin transportar su forma almacenada. **§8**: CA-01 y CA-02 se rehacen sobre la superficie nueva, y entran **CA-08, CA-09 y CA-10** —reseteo sobre cuenta bloqueada que procede, reseteo sobre cuenta sin contraseña, y tres provisorias distintas entre sí—. **§10**: la nota de la provisoria en claro cambia de dirección —viaja en la respuesta y no en la solicitud— y se agrega la nota que declara las **dos propiedades exigidas** del valor generado, no adivinable y no repetido, **sin declarar mecanismo**, que es de 05 y de infraestructura. **§17**: dos cláusulas nuevas de rechazo aunque compile. Sube minor: absorbe dos decisiones del Product Owner sobre un documento en estado `Propuesto`, sin invalidar el circuito ni ninguna de las dos mitades que el contrato declara. | Analista Funcional + API Designer (AG-02) |
| 1.3 | 2026-08-09 | **Cierra los hallazgos `F26-24` y la fila de este archivo del `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-24`**: la prohibición de resetear la cuenta de administrador no tenía fuente única y cada proyecto de código la anclaba donde podía —este contrato en `RN-01` e `INV-05`, `GeometriaFactory-Domain` en `INV-08`—. El intake 1.10 le da fuente: **`RN-15`** la enuncia dentro de su propio texto y la ancla en **INV-08**. **§6** lo declara en la fila del código, **§9** cita `RN-15` como la fuente del cierre y suma `RN-14` por las dos propiedades del valor que CA-10 verifica —las reglas aplicables pasan de cuatro a **seis**—, y **§10** suma la nota que registra el cambio de anclaje y deja `RN-01` e `INV-05` como fundamento de administrador único y no como fuente. **`F26-20`**: **§17** decía que con esta emisión «entran **dos** códigos al conjunto cerrado de CU-06», recuento de la versión 1.0; con la 1.2 entró el tercero, `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, y su propia §6 ya lo lista. **Ningún tipo, campo, código de error ni criterio de aceptación cambia**: el cierre sobre la cuenta de administrador ya era el mismo y se le declara la fuente. Sube minor. | Analista Funcional + API Designer (AG-02) |

## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- **Esta emisión es un cambio incompatible** y obliga al despliegue conjunto de las dos piezas desplegables (`RT-06`): entran **tres** códigos al conjunto cerrado de CU-06 —los dos de la emisión 1.0 más `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, que entró con la 1.2, según §6— y una familia de tipos nueva.
- Agregar a la solicitud de reseteo cualquier campo que alcance a los trabajos de la cuenta se **rechaza aunque compile**: contradice RN-12 y el criterio CA-01.
- **Agregar a la solicitud de reseteo un campo de contraseña también se rechaza aunque compile**: contradice CA-01 y la decisión del Product Owner de §10. Reponerlo devolvería la provisoria a la mano del docente, que es lo que la decisión evita.
- **Agregar una condición de rechazo por situación de cuenta al conjunto de §6 se rechaza aunque compile**: contradice FA-04 y CA-08. El reseteo no exige que la cuenta esté habilitada.
- Agregar el cambio de contraseña pendiente como campo de la respuesta de sesión de CU-01 se rechaza aunque compile: contradice CA-04 y el fundamento de §10.
- Agregar un campo opcional al resultado del reseteo es compatible, siempre que no viole CA-02.
