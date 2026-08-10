# CU-04 — Administrar las cuentas de la comisión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-04-Administrar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.5
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §5 (tercer criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1, §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.8**, §4 (F-01, F-03, **F-26**), §4.1 (RN-01, RN-06, RN-07, **RN-12**, **RN-13 precisada**), §6 (flujo 1), §7 (CL-6, **CL-7 reescrito**), §9 (X-3, **X-2 retirada**), §11 (**RN-B6 tachado** el 2026-08-09 por el intake 1.10, porque F-26 dejó sin objeto su mitigación; lo que sostenía vive en §7 CL-6), §17.1.P.2 (**INV-09**), §17.6 P.5; [`GeometriaFactory-Contracts` CU-08](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md)
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
- [13. Interacción multiusuario y concurrencia](#13-interacción-multiusuario-y-concurrencia)

---

## 1. Propósito

Darle al administrador el control mínimo y suficiente sobre la lista de su comisión: ver las cuentas con su situación, habilitar, bloquear y rehabilitar, **resetear la contraseña** y dar de baja con una confirmación escrita que declara que la baja elimina también los trabajos de esa cuenta. Incluye la configuración de la cuenta de administrador en el primer arranque, que sólo es posible mientras no exista ninguna.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Administrador | Primario | Recorre la lista de cuentas y ejecuta las **cinco** operaciones sobre cada una |
| Pieza pública | Sistema | Arma el panel de cuentas, exige la confirmación escrita de la baja e invoca los contratos correspondientes |
| Pieza de datos | Secundario | Aplica el cambio de situación, el reseteo de contraseña o la baja, con su arrastre de trabajos |
| Alumno | Secundario | Padece el efecto: obtiene acceso, lo pierde o deja de existir en el laboratorio |

## 3. Precondiciones

- El administrador tiene sesión iniciada por CU-02 y su papel es el de administrador.
- Para el flujo principal existe la cuenta de administrador. Para FA-03, no existe ninguna.
- El producto admite **exactamente un** administrador y dos papeles fijos, sin permisos configurables.

## 4. Flujo principal

1. El administrador abre la ruta de cuentas de su panel.
2. **La pieza pública invoca desde su servidor el contrato de listado de cuentas** de `GeometriaFactory-Contracts` CU-02, pasos 3 y 4.
3. La pieza pública presenta la lista con correo, nombre, apellido, situación y fecha de registro de cada cuenta.
4. El administrador elige una cuenta y una de las tres operaciones de situación: habilitar, bloquear o rehabilitar. El reseteo de contraseña se ejerce desde la misma fila y está en FA-06.
5. La pieza pública invoca el contrato de cambio de situación de `GeometriaFactory-Contracts` CU-02, pasos 5 y 6.
6. La pieza de datos devuelve la situación resultante y la pieza pública actualiza la lista con esa situación, sin inventarla del lado del navegador.
7. El administrador continúa con la cuenta siguiente.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador habilita una cuenta `Pendiente` | Es el paso 4 con la operación de habilitar. A partir de ese momento el alumno puede ingresar y establecer su contraseña por CU-02 FA-02 y CU-03 | El flujo continúa en el paso 7 |
| FA-02 | El administrador da de baja una cuenta | La pieza pública **exige que el administrador escriba el correo de la cuenta** como confirmación, y declara en el mismo lugar que la baja elimina también todos los trabajos de esa cuenta. Con la confirmación completa invoca el contrato de baja de `GeometriaFactory-Contracts` CU-02 FA-01 | El flujo vuelve al paso 2, con la lista ya sin esa cuenta |
| FA-03 | Es el primer arranque del laboratorio y no existe cuenta de administrador | La pieza pública ofrece la ruta de configuración inicial, con correo y contraseña, e invoca el contrato de `GeometriaFactory-Contracts` CU-02 FA-03. Es el **único** momento en que esa ruta arma algo | El flujo continúa en CU-02, paso 1 |
| FA-04 | Alguien abre la ruta de configuración inicial cuando ya existe administrador | La pieza pública no arma el formulario y deriva a la ruta de ingreso | El flujo termina |
| FA-05 | El administrador bloquea una cuenta cuyo alumno tiene sesión iniciada | La situación cambia en la pieza de datos. La sesión ya establecida no se corta desde acá: la próxima solicitud que esa sesión emita recibe el motivo de la situación de cuenta y CU-02 FA-01 la devuelve a la ruta de ingreso | El flujo continúa en el paso 7 |
| FA-06 | El administrador **resetea la contraseña** de una cuenta de alumno (F-26) | La pieza pública **pide confirmación** —la operación cambia la credencial de otra persona y no debe dispararse por accidente— y le presenta al administrador **la contraseña provisoria para que se la comunique al alumno**, en la misma superficie y una sola vez. Con la confirmación completa invoca el contrato de reseteo de contraseña de [`GeometriaFactory-Contracts` CU-08](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md), §4 pasos 1 y 2. **La cuenta y todos sus trabajos se conservan** (RN-12): la lista vuelve con la misma cuenta y en la misma situación. **La operación se ofrece cualquiera sea esa situación** —habilitada, pendiente o bloqueada—, porque el reseteo no la cambia y no la exige (FA-08). El alumno queda obligado a cambiarla en su próximo ingreso, que es CU-03 en su curso de cambio forzado | El flujo vuelve al paso 2 |
| FA-07 | El administrador resetea la contraseña de una cuenta cuyo alumno tiene sesión iniciada | La credencial cambia en la pieza de datos. **La sesión ya establecida no se corta desde acá**, igual que en FA-05: la próxima solicitud que esa sesión emita recibe `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`, y ahí la pieza pública **descarta la credencial de sesión del estado del circuito** y lleva a la persona al **cambio forzado** de CU-03, que resuelve presentando la provisoria. La diferencia con FA-05 sigue siendo el destino: la cuenta bloqueada vuelve a la ruta de ingreso sin camino propio, la reseteada tiene uno | El flujo continúa en el paso 7 |
| FA-08 | El administrador resetea una cuenta que **no está habilitada**, o una que **todavía no estableció su contraseña** | La primera **procede**: el reseteo no exige que la cuenta esté habilitada, la situación vuelve sin cambio y la fila la conserva. La segunda **no procede**: no hay contraseña que reemplazar, y la pieza pública informa el motivo con `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` **sin tratarlo como un error del administrador**, porque el camino que falta es que la persona haga su primer ingreso | El flujo vuelve al paso 2 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación de la baja no coincide con el de la cuenta | La baja **no procede**. La pieza pública lo informa y deja reintentar con la confirmación correcta |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Se intenta configurar una segunda cuenta de administrador | La pieza pública informa que ya existe y deriva a la ruta de ingreso. Terminación controlada: no hay camino alternativo |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | La cuenta sobre la que se opera ya no existe | La pieza pública informa y recarga la lista. Recuperación por reintento sobre la lista actualizada |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Se pidió resetear la cuenta del propio administrador | La pieza pública informa el motivo con todas las letras y **no ofrece un camino que no existe**: para la cuenta propia, el camino es «Mi contraseña», que es el cambio de CU-03 FA-01. Terminación controlada |
| `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` | Se pidió resetear una cuenta que todavía no estableció su contraseña | La pieza pública informa que no hay contraseña que resetear y que la persona todavía tiene que establecer la suya en su primer ingreso. Terminación controlada: **no es un error del administrador y no se le presenta como tal** |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta un campo de la configuración inicial o de la confirmación | La pieza pública señala el campo. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito. **La lista no se muestra con datos viejos**, porque la pieza pública no guarda estado propio |

**Las dos causas de reseteo rechazado que quedaban sin código ya no lo están, y ninguna se resolvió inventando un identificador acá.** La versión anterior de esta tabla las agrupaba bajo un provisional propio, `CONTRATO_RESETEO_NO_ADMITIDO`, junto con la de la cuenta de administrador, y después declaró que el conjunto definitivo de `GeometriaFactory-Contracts` CU-08 §6 cubría **una sola de las tres**. El Product Owner cerró las otras dos por caminos distintos: **resetear una cuenta que no está habilitada dejó de ser un rechazo** —el reseteo no exige que la cuenta esté habilitada—, de modo que esa causa no necesita código porque no existe; y **resetear una que todavía no estableció contraseña sigue siendo un rechazo**, para el que `GeometriaFactory-Contracts` CU-08 1.2 emitió `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, que esta tabla usa. **Las tres causas del provisional quedan resueltas: dos con código del ensamblado y una eliminada.**

## 7. Postcondiciones

- En caso de éxito de cambio de situación: la cuenta queda en la situación que devolvió la pieza de datos, y la lista la refleja.
- En caso de éxito de baja: la cuenta y **todos sus trabajos** dejaron de existir, y la lista ya no la incluye.
- En caso de éxito de configuración inicial: existe la única cuenta de administrador del laboratorio y la ruta de configuración inicial deja de armar formulario para siempre.
- En caso de fallo: ninguna situación cambió y la lista sigue mostrando lo que la pieza de datos devolvió por última vez.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un laboratorio sin cuenta de administrador | Se abre la ruta de configuración inicial y se configura `docente@ejemplo.test` | La cuenta queda creada, y una segunda apertura de esa ruta ya no arma formulario y deriva al ingreso |
| CA-02 | Una cuenta de alumno en situación `Pendiente` | El administrador la habilita | La lista la muestra habilitada, y el alumno pasa a poder establecer su contraseña por CU-03 |
| CA-03 | El panel de cuentas | Se cuentan las operaciones disponibles sobre una cuenta de alumno | Son exactamente **cinco**: habilitar, bloquear, rehabilitar, resetear la contraseña y dar de baja |
| CA-04 | Una cuenta `alumno@ejemplo.test` con dos trabajos | El administrador pide la baja y escribe `alumno@otro.test` como confirmación | La baja no procede y el mensaje declara que la confirmación no coincide |
| CA-05 | La misma cuenta y la misma pantalla de confirmación | El administrador lee la confirmación antes de escribir | El texto declara explícitamente que la baja elimina también los trabajos de esa cuenta |
| CA-06 | La misma cuenta con dos trabajos | El administrador escribe `alumno@ejemplo.test` y confirma | La cuenta desaparece de la lista y sus dos trabajos ya no figuran en ningún listado del laboratorio |
| CA-07 | Un alumno con sesión iniciada | Se abre por dirección directa la ruta de cuentas | La pieza pública no arma la ruta y devuelve al panel del alumno |
| CA-08 | Una cuenta `alumno@ejemplo.test` habilitada, con **tres trabajos**: uno en `Borrador`, uno en `Rechazado` con comentario y uno en `Finalizado` | El administrador resetea su contraseña y confirma | La superficie muestra **una** contraseña provisoria para comunicar, la cuenta sigue en la lista y sigue habilitada, y **sus tres trabajos siguen existiendo con sus estados y sus comentarios** |
| CA-09 | La misma cuenta ya reseteada | El alumno ingresa con la provisoria y pide por dirección directa el listado de sus trabajos | Termina en el cambio de contraseña de CU-03, **sin haber leído ni escrito nada**. Después de cambiarla, la misma navegación funciona |
| CA-10 | El panel de cuentas | Se compara la confirmación del reseteo con la de la baja | La del reseteo **no** exige transcribir el correo, y su texto **no** declara ninguna pérdida de trabajos: la operación no la produce |
| CA-11 | Tres cuentas de alumno con contraseña ya establecida, una `Pendiente`, una `Habilitada` y una `Bloqueada` | Se recorre la lista | Las **3** ofrecen la acción de resetear, y las 3 la ejecutan con éxito conservando su situación. El administrador **no** tiene que habilitar antes para poder resetear |
| CA-12 | El diálogo de reseteo y el resultado de la operación | Se inspecciona la superficie | Hay **0 campos** donde el administrador escriba una contraseña, y la provisoria aparece **una sola vez**, en el resultado, con su aviso de que no se vuelve a mostrar |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [`RN-02`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), y [**`RN-12`**](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [**`RN-13`**](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), que ya tienen archivo en `GeometriaFactory-Domain` |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-02](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) pasos 3 a 6 y FA-01 y FA-03; [`CU-08`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md) §4 y §6, por el reseteo; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-08, US-09, US-10, US-30 |
| Componentes esperados en 05 | Ruta de configuración inicial, panel de cuentas, diálogo de confirmación escrita de la baja y diálogo de reseteo de contraseña con la comunicación de la provisoria |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para FA-03 y FA-04, y de la etapa `d` para las **cinco** operaciones, la confirmación escrita de la baja y el reseteo que conserva los tres trabajos |

## 10. Notas y supuestos

- El arrastre de trabajos en la baja es una invariante del dominio y **no** algo que la pieza pública ejecute. Lo que sí le corresponde es hacer la operación difícil de ejecutar por accidente: por eso la confirmación escrita y el aviso explícito son criterios de aceptación acá.
- **La baja dejó de ser el remedio de un olvido de contraseña.** Hasta el `PRODUCT-INTAKE` 1.6 lo era, con el arrastre de trabajos como consecuencia declarada y aceptada; **1.7 retiró la exclusión X-2 y reescribió el caso límite CL-7**, y el remedio pasó a ser el reseteo de FA-06, que conserva la cuenta y todos sus trabajos. Lo que sigue excluido es la **recuperación autónoma por correo**, que X-1 impide: el laboratorio sigue sin canal de correo.
- El producto no admite un segundo administrador ni permisos finos, por la exclusión X-3. Ninguna variante de este caso de uso los introduce.
- **El contrato de reseteo ya está declarado, y el punto abierto que esta nota llevaba queda cerrado.** `GeometriaFactory-Contracts` emitió **CU-08**, el contrato de reseteo y de cambio obligatorio: sus contratos de uso pasaron de siete a ocho y su conjunto cerrado de códigos, de catorce a dieciséis y hoy a **diecisiete**. Los dos identificadores que esta categoría había acuñado provisoriamente se reemplazan por los definitivos: `CONTRATO_RESETEO_NO_ADMITIDO` por **`CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`** acá, y `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` por **`CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`** en CU-02 y CU-03.
- **Punto abierto cerrado: las dos causas de reseteo rechazado que no tenían código ya no lo necesitan las dos.** El identificador provisional de dos versiones atrás agrupaba tres causas y el definitivo cubría una. El Product Owner cerró las otras dos por caminos distintos. **La primera desapareció**: resolvió que el reseteo **no exige que la cuenta esté habilitada**, porque es una operación sobre la credencial que no toca la situación de la cuenta, y que el administrador tiene que poder resetear y habilitar **en el orden que quiera** sin acordarse de una secuencia; con eso, `GeometriaFactory-Domain` CU-13 FA-02 y `GeometriaFactory-Application` CU-11 dejan de contradecirse, porque la segunda retiró su rechazo `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`. **La segunda recibió código**: `GeometriaFactory-Contracts` CU-08 1.2 emitió `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, y esta categoría lo **usa** en §6 en lugar de acuñar uno, que es lo que venía sosteniendo. El conjunto cerrado del ensamblado pasó de dieciséis a **diecisiete**.
- **Punto abierto cerrado, y la decisión de esta sección quedó ratificada: la contraseña provisoria la produce el sistema.** Esta categoría lo había decidido así como **decisión derivada** —el intake decía que el administrador «fija una contraseña provisoria que le comunica al alumno», sin decir si la escribe o la recibe—, con tres motivos: evita que el docente reutilice la misma clave en toda la comisión, evita que la escriba en un canal donde quede escrita, y hace innecesario un campo de contraseña ajena en el panel, que es superficie que RT-02 preferiría no tener. `GeometriaFactory-Contracts` CU-08 la había decidido **al revés**, con una solicitud de dos campos, y esta sección dejó la discrepancia declarada en lugar de resolverla por su cuenta. **El Product Owner la resolvió a favor de esta lectura**, con el mismo fundamento que el primero de los tres: si la escribe el docente, termina siendo la misma clave para toda la comisión. CU-08 1.2 corrigió la superficie —la solicitud lleva **un solo campo**, el identificador de cuenta, y el resultado devuelve la provisoria generada—, de modo que **la pieza pública ya puede limitarse a mostrarla**, que es lo que FA-06 y CA-08 describen. **El panel no lleva campo de contraseña**, y CA-12 lo verifica.
- **El reseteo no es una baja y no arrastra nada** (RN-12). Por eso su confirmación **no** es la confirmación escrita del correo que exige RN-07 para la baja: exigir transcribir el correo para una operación reversible y no destructiva desalentaría la operación que el producto quiere que el docente use. La confirmación que corresponde acá es la que evita el disparo accidental sobre la fila equivocada, y su forma la decide `03-UX-UI-DX`.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas **RN-12** y **RN-13**, el invariante **INV-09**, el caso límite **CL-7 reescrito** y la exclusión **X-2 retirada**. **§1, §2 y §8 CA-03**: las operaciones del panel pasan de cuatro a **cinco**, con el reseteo de contraseña. **§5**: **FA-06** nueva —el reseteo, con su confirmación y la comunicación de la provisoria— y **FA-07** nueva —el reseteo sobre una sesión viva, que termina en el cambio forzado y no en el ingreso—. **§6**: `CONTRATO_RESETEO_NO_ADMITIDO`, rotulado **pendiente** porque el contrato todavía no existe en `GeometriaFactory-Contracts`. **§8**: CA-08, CA-09 y CA-10 nuevas, que verifican la conservación de los tres trabajos, el confinamiento del alumno reseteado y que la confirmación del reseteo **no** es la confirmación escrita de la baja. **§10**: la nota que declaraba a la baja como único remedio del olvido **se reescribe**, porque 1.7 la volvió falsa; se suman el punto abierto del contrato y la **decisión derivada** sobre quién produce la provisoria, con su fundamento y con lo que costaría cambiarla. **§13**: la concurrencia suma el caso del reseteo sobre una sesión viva. Sube minor: agrega una operación, dos flujos alternativos y tres criterios de aceptación, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación contra el `PRODUCT-INTAKE` 1.8, contra `GeometriaFactory-Contracts` CU-08 y contra las reglas ya emitidas en `GeometriaFactory-Domain`.** **(a) Punto abierto cerrado**: §10 declaraba que el contrato de reseteo «todavía no está declarado» y que la categoría de contratos tenía siete contratos de uso y catorce códigos; **CU-08 está emitido** y el conjunto cerrado es de dieciséis. El identificador provisional `CONTRATO_RESETEO_NO_ADMITIDO`, rotulado `[PENDIENTE]`, se reemplaza en §6 por el definitivo **`CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`**, y FA-06 enlaza el contrato en lugar de remitir al punto abierto. **(b) Punto abierto nuevo, y se declara en lugar de taparse**: el provisional agrupaba **tres** causas y el definitivo cubre **una**; las otras dos —cuenta no habilitada y cuenta sin contraseña establecida— quedan sin código de contrato, y sobre la primera **`GeometriaFactory-Domain` CU-13 FA-02 y `GeometriaFactory-Application` CU-11 §6 se contradicen**. Esta categoría no acuña códigos ni resuelve la discrepancia. **(c) Punto abierto cerrado**: §9 declaraba a RN-12 y RN-13 «todavía sin archivo en `GeometriaFactory-Domain`», y las dos lo tienen; las citas pasan a ser enlaces. **(d)** FA-07 se corrige a la lectura del intake 1.8: la cuenta reseteada **no obtiene sesión de trabajo**, de modo que la sesión viva se descarta al recibir `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` y la persona llega al cambio forzado presentando la provisoria. **(e)** La decisión derivada sobre quién produce la provisoria suma su punto abierto: CU-08 §4 la declara **elegida por el administrador**, al revés de lo que esta sección había decidido. **(f)** La cabecera cita el intake **1.8** y §9 suma CU-08 a los contratos consumidos. Sube minor: cierra dos puntos abiertos, abre dos declarados, fija un identificador y corrige un flujo alternativo. |
| 1.3 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26, y con ellas cierra los dos puntos abiertos que la versión 1.2 había declarado.** **Decisión A: resetear no exige que la cuenta esté habilitada** —es una operación sobre la credencial, no toca la situación de la cuenta, y el administrador puede resetear y habilitar en el orden que quiera sin acordarse de una secuencia—. **Decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador** —si la escribe el docente, termina siendo la misma clave para toda la comisión—. **(a) Punto abierto cerrado, y esta sección tenía razón**: la decisión derivada de §10 sobre quién produce la provisoria queda **ratificada**; `GeometriaFactory-Contracts` CU-08 1.2 corrigió su solicitud a **un solo campo** y su resultado a devolver la provisoria generada, de modo que la pieza pública ya puede limitarse a mostrarla, que es lo que FA-06 describía desde 1.1. La nota deja de declarar una discrepancia y registra su resolución. **(b) Punto abierto cerrado**: de las dos causas de reseteo rechazado sin código, **una desapareció** con la decisión A —y con ella la contradicción declarada entre `GeometriaFactory-Domain` CU-13 FA-02 y `GeometriaFactory-Application` CU-11, que retiró su rechazo— y **la otra recibió código**, `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, que §6 **usa** en lugar de acuñar uno propio; el conjunto cerrado del ensamblado pasó de dieciséis a **diecisiete**. **§5**: **FA-08** nueva, con las dos situaciones que antes no tenían tratamiento —la cuenta no habilitada, que **procede**, y la cuenta sin contraseña establecida, que no—; FA-06 declara que la operación se ofrece **cualquiera sea la situación** de la cuenta. **§8**: **CA-11** —las tres situaciones ofrecen y ejecutan el reseteo— y **CA-12** —cero campos de contraseña en el panel y la provisoria mostrada una sola vez—. Sube minor: cierra dos puntos abiertos, agrega un flujo alternativo, un código de contrato y dos criterios de aceptación, sin invalidar ninguna decisión previa de esta sección. |
| 1.4 | 2026-08-09 | **Deja constancia por el hallazgo `F26-25`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. El informe registra que **`US-30` se agregó a la tabla de trazabilidad de §9 sin registro en ninguna fila de este control de cambios**. Se declara acá, y **no se reescribe ninguna fila histórica**: `US-30` es la historia del **reseteo de la contraseña de un alumno desde el panel**, corresponde a la quinta operación de la fila de cuentas que la versión 1.1 incorporó con la capacidad **F-26**, y está vigente. **Ningún flujo, precondición, motivo, criterio de aceptación ni componente de este caso de uso cambia.** Sube minor: deja registro de un cambio real que se había aplicado sin él. |
| 1.5 | 2026-08-10 | **Cierra la parte del hallazgo `N-5`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo, contra `PRODUCT-INTAKE` **1.11**. La **trazabilidad de cabecera** citaba «§11 (RN-B6)» como riesgo **vigente**; el intake **1.10** tachó esa fila el 2026-08-09, porque **F-26** conserva la cuenta y todos sus trabajos, de modo que la baja dejó de ser el remedio del olvido y la mitigación que `RN-B6` declaraba —advertir al alumno antes de darlo de baja— **quedó sin objeto**. La cita **se conserva** con la constancia de que la fila está tachada y con el motivo, y remite a §7 CL-6, que declara que la baja arrastra los trabajos, que es donde vive hoy lo que sostenía, en lugar de borrarse, para que no se lea como si el riesgo nunca hubiera existido. **Ningún flujo, motivo, precondición, criterio de aceptación ni componente de este caso de uso cambia.** Sube minor: corrige una referencia a una fila retirada. |

## 13. Interacción multiusuario y concurrencia

Sección opcional admitida por `Rules-Especificacion-Funcional.md` §4.3 para el tipo `web-monolith`.

El laboratorio tiene un solo administrador, de modo que no hay dos personas cambiando la situación de la misma cuenta a la vez. Lo que sí puede coincidir es un cambio de situación con una sesión de alumno ya establecida: FA-05 declara que la pieza pública no corta esa sesión y que el efecto se hace visible en la siguiente solicitud que esa sesión emita. **Lo mismo vale para el reseteo**, con un destino distinto: FA-07 declara que la sesión vigente no se corta y que la siguiente solicitud termina en el cambio forzado, no en el ingreso. La pieza pública no mantiene copia de la lista de cuentas entre operaciones: cada recorrido vuelve a pedirla.
