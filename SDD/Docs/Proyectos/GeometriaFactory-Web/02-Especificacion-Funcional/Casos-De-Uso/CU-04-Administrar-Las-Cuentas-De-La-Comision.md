# CU-04 — Administrar las cuentas de la comisión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-04-Administrar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5 (los cinco criterios); `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §5 (tercer criterio); `../../../../00-Contexto/Alcance-Producto.md` §4.1, §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-01, F-03, **F-26**), §4.1 (RN-01, RN-06, RN-07, **RN-12**, **RN-13**), §6 (flujo 1), §7 (CL-6, **CL-7 reescrito**), §9 (X-3, **X-2 retirada**), §11 (RN-B6), §17.1.P.2 (**INV-09**), §17.6 P.5
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
| FA-06 | El administrador **resetea la contraseña** de una cuenta de alumno (F-26) | La pieza pública **pide confirmación** —la operación cambia la credencial de otra persona y no debe dispararse por accidente— y le presenta al administrador **la contraseña provisoria para que se la comunique al alumno**, en la misma superficie y una sola vez. Con la confirmación completa invoca el contrato de reseteo de contraseña de `GeometriaFactory-Contracts` (§10, punto abierto). **La cuenta y todos sus trabajos se conservan** (RN-12): la lista vuelve con la misma cuenta y en la misma situación. El alumno queda obligado a cambiarla en su próximo ingreso, que es CU-03 en su curso de cambio forzado | El flujo vuelve al paso 2 |
| FA-07 | El administrador resetea la contraseña de una cuenta cuyo alumno tiene sesión iniciada | La credencial cambia en la pieza de datos. **La sesión ya establecida no se corta desde acá**, igual que en FA-05: la próxima solicitud que esa sesión emita recibe el motivo de cambio de contraseña pendiente y la pieza pública la lleva al **cambio forzado** de CU-03, sin devolverla al ingreso | El flujo continúa en el paso 7 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación de la baja no coincide con el de la cuenta | La baja **no procede**. La pieza pública lo informa y deja reintentar con la confirmación correcta |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Se intenta configurar una segunda cuenta de administrador | La pieza pública informa que ya existe y deriva a la ruta de ingreso. Terminación controlada: no hay camino alternativo |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | La cuenta sobre la que se opera ya no existe | La pieza pública informa y recarga la lista. Recuperación por reintento sobre la lista actualizada |
| `CONTRATO_RESETEO_NO_ADMITIDO` [PENDIENTE, §10] | Se pidió resetear una cuenta que no lo admite: la del propio administrador, una que no está habilitada o una que todavía no estableció contraseña | La pieza pública informa el motivo con todas las letras y **no ofrece un camino que no existe**: para la cuenta bloqueada, el remedio es rehabilitarla primero; para la que nunca estableció contraseña, no hay nada que resetear y el alumno ya tiene abierto el establecimiento; para la propia, el camino es «Mi contraseña». Terminación controlada |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta un campo de la configuración inicial o de la confirmación | La pieza pública señala el campo. Recuperación por corrección y reintento |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Handoff a CU-10: estado degradado explícito. **La lista no se muestra con datos viejos**, porque la pieza pública no guarda estado propio |

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

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| Reglas de negocio aplicables | [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [`RN-02`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), y **`RN-12`** y **`RN-13`** del `PRODUCT-INTAKE` 1.7 §4.1, **todavía sin archivo en `GeometriaFactory-Domain`** |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-02](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Contrato-De-Administracion-De-Cuentas.md) pasos 3 a 6 y FA-01 y FA-03; [`CU-06`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) |
| Fachada del visualizador | Ninguna función |
| Historias de usuario a generar en 06 | US-08, US-09, US-10 |
| Componentes esperados en 05 | Ruta de configuración inicial, panel de cuentas, diálogo de confirmación escrita de la baja y diálogo de reseteo de contraseña con la comunicación de la provisoria |
| Tests previstos en 08 | Guion de demostración de la etapa `c` para FA-03 y FA-04, y de la etapa `d` para las **cinco** operaciones, la confirmación escrita de la baja y el reseteo que conserva los tres trabajos |

## 10. Notas y supuestos

- El arrastre de trabajos en la baja es una invariante del dominio y **no** algo que la pieza pública ejecute. Lo que sí le corresponde es hacer la operación difícil de ejecutar por accidente: por eso la confirmación escrita y el aviso explícito son criterios de aceptación acá.
- **La baja dejó de ser el remedio de un olvido de contraseña.** Hasta el `PRODUCT-INTAKE` 1.6 lo era, con el arrastre de trabajos como consecuencia declarada y aceptada; **1.7 retiró la exclusión X-2 y reescribió el caso límite CL-7**, y el remedio pasó a ser el reseteo de FA-06, que conserva la cuenta y todos sus trabajos. Lo que sigue excluido es la **recuperación autónoma por correo**, que X-1 impide: el laboratorio sigue sin canal de correo.
- El producto no admite un segundo administrador ni permisos finos, por la exclusión X-3. Ninguna variante de este caso de uso los introduce.
- **[PUNTO ABIERTO] El contrato de reseteo de contraseña todavía no está declarado en `GeometriaFactory-Contracts`.** Esa categoría declara hoy siete contratos de uso y catorce códigos de error, ninguno de los cuales cubre F-26, que entró con el `PRODUCT-INTAKE` 1.7. Los dos códigos que este caso de uso nombra —`CONTRATO_RESETEO_NO_ADMITIDO` acá y `CONTRATO_CAMBIO_DE_CONTRASENA_PENDIENTE` en CU-02 y CU-03— están rotulados como **pendientes** y su nombre definitivo lo fija `GeometriaFactory-Contracts`. **Esta categoría no los inventa como si existieran**: los nombra para poder declarar el comportamiento de la superficie y deja escrito que la confirmación es de otra sección.
- **[DECISIÓN DERIVADA] Quién elige la contraseña provisoria no lo declara el intake, y acá se decide que la produzca la pieza de datos y no el administrador.** El intake dice que el administrador «fija una contraseña provisoria que le comunica al alumno», sin decir si la escribe o la recibe. Se decide que la produzca el servicio y que la pieza pública sólo la **muestre una vez** para que el administrador la comunique, por tres motivos: evita que el docente reutilice la misma clave en toda la comisión, evita que la escriba en un canal donde quede escrita, y hace innecesario un campo de contraseña ajena en el panel, que es superficie que RT-02 preferiría no tener. **Si el Product Owner prefiere que la escriba el administrador**, el cambio es de un campo en la superficie y no altera ninguna regla.
- **El reseteo no es una baja y no arrastra nada** (RN-12). Por eso su confirmación **no** es la confirmación escrita del correo que exige RN-07 para la baja: exigir transcribir el correo para una operación reversible y no destructiva desalentaría la operación que el producto quiere que el docente use. La confirmación que corresponde acá es la que evita el disparo accidental sobre la fila equivocada, y su forma la decide `03-UX-UI-DX`.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas **RN-12** y **RN-13**, el invariante **INV-09**, el caso límite **CL-7 reescrito** y la exclusión **X-2 retirada**. **§1, §2 y §8 CA-03**: las operaciones del panel pasan de cuatro a **cinco**, con el reseteo de contraseña. **§5**: **FA-06** nueva —el reseteo, con su confirmación y la comunicación de la provisoria— y **FA-07** nueva —el reseteo sobre una sesión viva, que termina en el cambio forzado y no en el ingreso—. **§6**: `CONTRATO_RESETEO_NO_ADMITIDO`, rotulado **pendiente** porque el contrato todavía no existe en `GeometriaFactory-Contracts`. **§8**: CA-08, CA-09 y CA-10 nuevas, que verifican la conservación de los tres trabajos, el confinamiento del alumno reseteado y que la confirmación del reseteo **no** es la confirmación escrita de la baja. **§10**: la nota que declaraba a la baja como único remedio del olvido **se reescribe**, porque 1.7 la volvió falsa; se suman el punto abierto del contrato y la **decisión derivada** sobre quién produce la provisoria, con su fundamento y con lo que costaría cambiarla. **§13**: la concurrencia suma el caso del reseteo sobre una sesión viva. Sube minor: agrega una operación, dos flujos alternativos y tres criterios de aceptación, sin invalidar ninguna decisión previa. |

## 13. Interacción multiusuario y concurrencia

Sección opcional admitida por `Rules-Especificacion-Funcional.md` §4.3 para el tipo `web-monolith`.

El laboratorio tiene un solo administrador, de modo que no hay dos personas cambiando la situación de la misma cuenta a la vez. Lo que sí puede coincidir es un cambio de situación con una sesión de alumno ya establecida: FA-05 declara que la pieza pública no corta esa sesión y que el efecto se hace visible en la siguiente solicitud que esa sesión emita. **Lo mismo vale para el reseteo**, con un destino distinto: FA-07 declara que la sesión vigente no se corta y que la siguiente solicitud termina en el cambio forzado, no en el ingreso. La pieza pública no mantiene copia de la lista de cuentas entre operaciones: cada recorrido vuelve a pedirla.
