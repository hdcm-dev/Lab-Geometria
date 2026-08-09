# CU-02 — Contrato de administración de cuentas de alumno

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** CU-02-Contrato-De-Administracion-De-Cuentas.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md` §1, §5; `NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md` §1, §5; `00-Contexto/Vision-Producto.md` §9; `00-Contexto/Alcance-Producto.md` §4.1 (F-01, F-02, F-03, F-04, F-05) y §5 (X-1 vigente, X-3); `PRODUCT-INTAKE` 1.7 §4.1 (RN-01, RN-02, RN-06, RN-07, **RN-12**), §17.4 P.2, P.3, P.5 y P.10, §17.5 P.3 y P.5, §14 (RA-03), §4 (**F-26**), §6 (flujo 1), §7 (CL-6, **CL-7** reescrito), §9 (**X-2 retirada**)
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

Declarar los tipos de transferencia del ciclo de vida de una cuenta: el registro que hace el alumno sin elegir contraseña, el establecimiento de contraseña en su primer ingreso efectivo, el cambio de contraseña presentando la vigente, el listado de cuentas que ve el administrador, la orden de cambio de situación de una cuenta —habilitar, bloquear, rehabilitar— y la solicitud de baja, que es un tipo aparte porque exige la confirmación escrita y no es un valor del conjunto cerrado de situaciones de §3. El contrato transporta esa confirmación y no transporta ninguna forma de la contraseña almacenada.

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
5. El código de la pieza pública arma la solicitud de cambio de situación con dos campos: identificador de la cuenta y situación pretendida.
6. El código de la pieza de datos responde con el resultado, que devuelve la situación resultante de la cuenta.
7. El código de la pieza pública, actuando para el alumno ya habilitado, arma la solicitud de establecimiento de contraseña con la contraseña elegida.
8. El código de la pieza de datos responde con el resultado, y a partir de ahí CU-01 vuelve a ser el camino de entrada.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador da de baja una cuenta | El contrato usa la solicitud de baja, que además del identificador exige el campo de **correo escrito como confirmación** y declara que la baja elimina también los trabajos de la cuenta | El flujo vuelve al paso 3, con el listado ya sin la cuenta dada de baja |
| FA-02 | El alumno cambia su contraseña estando dentro del laboratorio | El contrato usa la solicitud de cambio de contraseña, con dos campos: contraseña vigente y contraseña nueva. La vigente es obligatoria por contrato | El flujo vuelve al paso 8 |
| FA-03 | Es el primer arranque del laboratorio y todavía no existe cuenta de administrador | El contrato usa la solicitud de configuración de la cuenta de administrador, con correo y contraseña. El contrato no declara ningún campo que permita configurar una segunda | El flujo continúa en el paso 3 |
| FA-04 | El administrador resetea la contraseña de una cuenta de alumno | **No es este contrato**: la solicitud de reseteo y su resultado son de [CU-08](CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md), que es una familia de tipos propia. Lo que sí es de acá es lo que viene después: **la solicitud de cambio de contraseña de FA-02 se reutiliza tal cual** para el cambio obligatorio, con la provisoria como contraseña vigente | El flujo vuelve al paso 3, con el listado ya declarando el cambio pendiente de esa cuenta |

## 6. Excepciones y errores

| Código | Causa | Respuesta del contrato |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta el correo, el nombre o el apellido en el registro | Respuesta de error de CU-06 que nombra el campo ausente. Recuperación: el código de la pieza pública corrige y reintenta |
| `CONTRATO_CORREO_YA_REGISTRADO` | El correo del registro ya pertenece a una cuenta | Respuesta de error de CU-06 con texto neutro. Terminación controlada |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación de la baja no coincide con el de la cuenta | Respuesta de error de CU-06. La baja no procede; recuperación por reintento con la confirmación correcta |
| `CONTRATO_CREDENCIAL_INVALIDA` | El cambio de contraseña llega sin la contraseña vigente o con una que no corresponde | Respuesta de error de CU-06 con texto neutro. Terminación controlada |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | Se intenta configurar una cuenta de administrador cuando ya existe una | Respuesta de error de CU-06. Terminación controlada: el contrato no ofrece camino alternativo |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde | Respuesta de error de CU-06 con texto neutro y sin dirección del servicio que falló. Handoff al estado degradado |

## 7. Postcondiciones

- En caso de éxito: el código de la pieza pública tiene la situación resultante de la cuenta afectada, o la colección de elementos de listado de cuenta, y ningún campo transporta contraseña almacenada.
- En caso de fallo: el código de la pieza pública tiene un tipo de error de CU-06 y la situación de la cuenta que ya conocía, sin cambio.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El tipo de solicitud de registro del contrato | Se inspecciona su superficie pública | Declara exactamente tres campos —correo, nombre y apellido— y **0 campos de contraseña**, porque el registro no la elige |
| CA-02 | Una cuenta pendiente con correo `alumna@ejemplo.edu` | El administrador solicita el cambio de situación a habilitada | El resultado devuelve la situación resultante `Habilitada` para esa cuenta |
| CA-03 | Una cuenta habilitada con correo `alumna@ejemplo.edu` | Se arma la solicitud de baja con el campo de confirmación en `otra@ejemplo.edu` | La respuesta es el tipo de error de CU-06 con código `CONTRATO_CONFIRMACION_NO_COINCIDE` y la cuenta no se da de baja |
| CA-04 | El tipo de solicitud de cambio de contraseña | Se inspecciona su superficie pública | Declara la contraseña vigente como campo obligatorio: no existe forma válida del tipo que cambie la contraseña sin presentarla |
| CA-05 | Un elemento de listado de cuenta del administrador | Se inspecciona su superficie pública | Trae correo, nombre, apellido, situación y fecha de registro, y **0 campos** con la contraseña almacenada o con cualquier dirección de servicio interno |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | Ninguna propia: este proyecto de código no las redacta. Aplican [`RN-01`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) —administrador único y papeles fijos—, [`RN-02`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) —el correo del alumno es único, que sostiene el código `CONTRATO_CORREO_YA_REGISTRADO`—, [`RN-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) —una cuenta que no está habilitada no obtiene sesión— y [`RN-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) —la baja arrastra los trabajos y exige confirmación escrita—, las cuatro de `GeometriaFactory-Domain`. Ver `Especificacion-Funcional.md` §5 |
| Historias de usuario a generar en 06 | US-03 tipos de registro y de credencial; US-04 tipos de listado y de cambio de situación de cuenta; US-05 solicitud de baja con confirmación escrita |
| Componentes esperados en 05 | Familia de tipos de transferencia de cuentas del ensamblado de contratos |
| Tests previstos en 08 | Pruebas de integración del recorrido de alta de punta a punta —registro, habilitación, establecimiento de contraseña, ingreso—, de la baja con confirmación errónea y del intento de configurar una segunda cuenta de administrador |

## 10. Notas y supuestos

- **Este contrato** no declara ningún tipo que transporte una contraseña provisoria: la solicitud de reseteo es de **CU-08**, que `PRODUCT-INTAKE` 1.7 hizo necesaria al incorporar la capacidad **F-26** y retirar la exclusión **X-2**. Lo que sigue sin existir en ningún tipo del ensamblado es el **enlace de recuperación**, porque no hay canal de correo: la exclusión **X-1** sigue vigente (`Alcance-Producto.md` §5). La redacción anterior de esta nota citaba las dos exclusiones juntas y quedó falsa en su primera mitad.
- La baja de una cuenta arrastra **todos** sus trabajos, cualquiera sea su estado, incluidos los que ya recibieron desenlace. El contrato no declara ningún campo que permita conservarlos: es invariante de dominio y no una opción del solicitante.
- **La baja y el reseteo son operaciones opuestas y no se confunden por su forma.** La solicitud de baja exige la confirmación escrita del correo y elimina la cuenta y todos sus trabajos (RN-07); la de reseteo, que vive en CU-08, no exige confirmación escrita y **conserva la cuenta y todos sus trabajos** (RN-12). Hasta `PRODUCT-INTAKE` 1.6 la baja era el único camino declarado ante una contraseña olvidada, y por eso el primer olvido costaba la cursada entera; **F-26** cierra ese agujero.
- Quién puede pedir el listado de cuentas se verifica en la pieza de datos. El contrato transporta el papel, no lo hace cumplir.
- La forma de los puntos de acceso pertenece a `GeometriaFactory-Api`.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los tipos de registro, credencial, listado de cuentas y cambio de situación, con la confirmación escrita de la baja como campo del contrato. |
| 1.0 | 2026-08-08 | Correcciones absorbidas de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-13**: §1 dejaba leer «dar de baja» como una cuarta transición del conjunto cerrado de situaciones que §3 cierra en tres valores; se reformula para nombrar la baja como solicitud aparte, que es lo que FA-01 ya hacía. **H-07**: la fila de reglas de negocio de §9 pasa a referir por identificador `RN-01` y `RN-07` de `GeometriaFactory-Domain`, con enlaces relativos. **H-09**: la sección opcional se renumera de §12 a §17, el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna para `library`. |
| 1.1 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` 1.3 §4.1, que transcribe completas las once reglas del producto y da de alta `RN-02` y `RN-06`. Cambios: §9 suma las referencias por identificador a `RN-02`, que sostiene el código `CONTRATO_CORREO_YA_REGISTRADO`, y a `RN-06`; §10 declara que la baja arrastra los trabajos en cualquiera de los cuatro estados, incluidos los que ya recibieron desenlace, y que el contrato no ofrece campo para conservarlos. **Ningún tipo, campo ni criterio de aceptación de este contrato cambia**: el circuito de revisión no toca la administración de cuentas. | Analista Funcional + API Designer (AG-02) |

| 1.2 | 2026-08-09 | Actualización por contenido nuevo aguas arriba: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26**, la regla **RN-12**, el retiro de la exclusión **X-2** y la reescritura del caso límite **CL-7**. **Ningún tipo ni campo de este contrato cambia.** Cambios: **FA-04 nuevo**, que declara que la solicitud de reseteo pertenece a **CU-08** y que la solicitud de cambio de contraseña de FA-02 **se reutiliza tal cual** para el cambio obligatorio; §10 corrige una afirmación que quedó falsa —«el contrato no declara ningún tipo que transporte una contraseña provisoria… exclusiones X-1 y X-2»—, acotándola a este contrato y dejando en pie sólo X-1, la del enlace de recuperación por correo; y suma la nota que separa la baja del reseteo como operaciones opuestas. | Analista Funcional + API Designer (AG-02) |
## 17. Compatibilidad de versión pública

Sección opcional de `Rules-Especificacion-Funcional.md` §4.3, que la numera **§17** y la reserva para `library`. Se conserva su número de la regla, aunque deje un hueco tras §11, para que un lector automatizado que busque §17 en cualquier caso de uso del producto encuentre siempre lo mismo.

- Agregar una situación de cuenta al conjunto admitido es **cambio incompatible** de hecho, aunque compile: la pieza pública que no la contempla deja de cubrir todos los casos. Se trata como incompatible y obliga al despliegue conjunto.
- Quitar el campo de confirmación de la baja es incompatible y además contradice el criterio de aceptación CA-03.
- Agregar un campo opcional a un elemento de listado de cuenta es compatible, siempre que no viole el criterio CA-05.
