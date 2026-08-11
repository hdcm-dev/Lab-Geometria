# Casos de prueba referenciales — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** los **ocho** contratos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) con sus criterios de aceptación; las **once** restricciones transversales de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6; las **veintidós** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); los **siete** NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3.1 y §3.2; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §18 y §20
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de prueba](#2-catálogo-de-casos-de-prueba)
  - [2.1 Sesión y cuentas](#21-sesión-y-cuentas)
  - [2.2 Trabajo, listado y detalle](#22-trabajo-listado-y-detalle)
  - [2.3 Error, desenlace y reseteo](#23-error-desenlace-y-reseteo)
  - [2.4 Inspecciones de superficie y de construcción](#24-inspecciones-de-superficie-y-de-construcción)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara los ocho campos de `Rules-Calidad-Y-Pruebas.md` §4.6. **Todas las salidas observadas dicen «Sin ejecutar» y todos los estados dicen `Pendiente`**: el ensamblado no está construido y afirmar otra cosa sería una afirmación sobre el estado del sistema sin evidencia.

**Vocabulario propio de este catálogo**, declarado acá la primera vez que aparece:

- **Inspección de superficie**: comprobación que se hace leyendo la superficie pública del ensamblado, sin ejecutar nada. Es la mitad de la verificación de este proyecto de código ([`Estrategia-Testing.md`](Estrategia-Testing.md) §1).
- **Integración**: prueba que golpea el servicio real por su protocolo y usa los tipos de este ensamblado como cuerpo de petición y de respuesta. **Vive materialmente en `GeometriaFactory-Api`**; acá se declara qué tiene que verificar sobre los tipos.
- **Recuento**: la forma característica de aserción de este proyecto de código — cero campos, cuatro campos, quince códigos ([`Estrategia-Testing.md`](Estrategia-Testing.md) §4).
- **Fixture**: uno de los cuatro cuerpos declarados en [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.

## 2. Catálogo de casos de prueba

### 2.1 Sesión y cuentas

#### TC-01 — Respuesta-De-Sesion-Con-Cuatro-Campos

| Campo | Valor |
| --- | --- |
| Tipo | Integración e inspección de superficie |
| Cubre | `CU-01`; `RT-01`, `RT-10`; `US-01`; NFR «Campos de la respuesta de sesión» |
| Setup | Servicio real levantado; una cuenta `Habilitado` sin la marca de cambio de contraseña pendiente |
| Pasos | Given un canje de credenciales válido, When se lo ejerce, Then la respuesta de sesión llega con **exactamente 4 campos**. When se inspecciona su superficie, Then **0 campos** transportan el hash de la contraseña o la clave de firma, y **0** transportan una condición que impida operar |
| Salida esperada | Tres recuentos: 4, 0 y 0 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02 — Las-Condiciones-Que-Impiden-Operar-Viajan-Como-Error

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-01`, `CU-06`, `CU-08`; `RT-10`; `US-01`, `US-14`, `US-22`; `RN-06`, `RN-13`, `RN-16` de `GeometriaFactory-Domain` |
| Setup | Tres cuentas: una `Pendiente`, una **recién habilitada** con la marca puesta y una **reseteada** con la marca puesta |
| Pasos | Given la cuenta `Pendiente`, When canjea credenciales, Then recibe respuesta de **error** con `CONTRATO_CUENTA_NO_HABILITADA` y **no** una respuesta de sesión. Given las dos cuentas con la marca, When cada una canjea con su provisoria, Then **las 2 respuestas traen el mismo código** `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO`: el contrato **no multiplica el código por origen de la marca** |
| Salida esperada | Ninguna respuesta de sesión emitida en los tres casos; dos respuestas con código idéntico. Es `CA-08` de `CU-06` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-03 — Registro-De-Cuenta-De-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-02`; `US-02`; `RN-02` de `GeometriaFactory-Domain` |
| Setup | Servicio real levantado, sin la cuenta que se registra |
| Pasos | Given una solicitud de registro completa, When se la envía, Then el resultado declara la cuenta creada con su situación. Given un correo ya registrado, Then llega `CONTRATO_CORREO_YA_REGISTRADO`. Given una solicitud sin un campo obligatorio, Then `CONTRATO_CAMPO_REQUERIDO_AUSENTE` con **un detalle con el campo señalado** |
| Salida esperada | Un registro y dos respuestas de error con su código y su detalle |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04 — Listado-De-Cuentas-Del-Panel-Del-Administrador

| Campo | Valor |
| --- | --- |
| Tipo | Integración e inspección de superficie |
| Cubre | `CU-02`; `RT-01`; `US-03` |
| Setup | Varias cuentas en las tres situaciones |
| Pasos | Given el listado de cuentas, When se lo pide, Then cada elemento trae los campos que el contrato declara. When se inspecciona su superficie, Then **0 campos** transportan ninguna forma de la contraseña almacenada |
| Salida esperada | Elementos con sus campos declarados y un recuento en 0 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-05 — Cambio-De-Situacion-De-La-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-02`; `US-04`; `RN-16` de `GeometriaFactory-Domain` |
| Setup | Una cuenta `Pendiente` y una `Habilitado` |
| Pasos | Given la orden de habilitar, When se la envía, Then el resultado **devuelve la provisoria producida**, porque habilitar la produce (`RN-16`). Given la orden de bloquear y la de rehabilitar, Then el resultado declara la situación alcanzada. Given la configuración de un segundo administrador, Then `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` |
| Salida esperada | Tres órdenes con su resultado y un rechazo. **Ningún tipo de establecimiento anónimo de contraseña participa**: `RN-16` lo eliminó |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06 — Baja-Con-Su-Confirmacion-Escrita

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-02`; `US-05`; `RN-07` de `GeometriaFactory-Domain` |
| Setup | Una cuenta con trabajos |
| Pasos | Given la solicitud de baja con el correo escrito como confirmación **coincidente**, When se la envía, Then la baja procede. Given una confirmación que no coincide, Then `CONTRATO_CONFIRMACION_NO_COINCIDE` con **un detalle con el campo de confirmación** |
| Salida esperada | Una baja y un rechazo con su detalle. La confirmación escrita es **una barrera deliberada**: la baja arrastra los trabajos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Trabajo, listado y detalle

#### TC-07 — El-Texto-Original-Viaja-Como-Cadena-Sin-Interpretarse

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-03`; `RT-03`, `RT-08`; `US-06`, `US-19`; `RN-08` de `GeometriaFactory-Domain` |
| Setup | Fixture del sample **S-2** con el cuerpo del escenario **E-2** del intake §20, cuyo texto **no es JSON estrictamente válido** |
| Pasos | Given el envío con el texto de `E-2`, When se lo envía y después se pide el detalle, Then el texto vuelve **idéntico carácter por carácter** al enviado. When se inspecciona la superficie del campo, Then es **una sola cadena** y no una estructura interpretada. When se lee el estado del resultado, Then pertenece al conjunto cerrado de **cuatro** valores |
| Salida esperada | Comparación byte a byte que cierra, un campo de tipo cadena y un estado del conjunto cerrado. `E-2` es el mejor caso porque su texto quebraría cualquier intento de interpretación en el contrato |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-08 — Solicitud-Unica-De-Eliminacion-Para-Los-Dos-Papeles

| Campo | Valor |
| --- | --- |
| Tipo | Integración e inspección de superficie |
| Cubre | `CU-03`; `US-07`; `RN-04`, `RN-03`, `RN-11` de `GeometriaFactory-Domain` |
| Setup | Un trabajo del alumno en `Borrador` y otro en `Pendiente`; el mismo servicio con papel de administrador |
| Pasos | Given la solicitud de eliminación, When se inspecciona la superficie, Then **hay un solo tipo de solicitud** para los dos papeles. Given el alumno eliminando un trabajo fuera de `Borrador`, Then `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR`. Given el administrador pidiendo un trabajo en `Borrador`, Then `CONTRATO_TRABAJO_NO_ENCONTRADO`, el **mismo** código que para un inexistente y un ajeno |
| Salida esperada | Un solo tipo de solicitud, y dos códigos distintos por causa distinta; **0 campos** permiten distinguir las tres causas de `CONTRATO_TRABAJO_NO_ENCONTRADO` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-09 — La-Proyeccion-De-Listado-No-Arrastra-El-Detalle

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie e integración |
| Cubre | `CU-04`; `RT-04`; `US-08`, `US-20`; NFR «Carga útil del listado» |
| Setup | Varios trabajos con texto original extenso y con comentario del administrador |
| Pasos | Given la superficie de la familia de listado, When se la inspecciona, Then hay **0** ocurrencias del texto original, **0** de componentes de pieza y **0** del comentario del administrador. Given el listado devuelto por el servicio real, Then ningún elemento trae esos tres datos, y sí trae el **estado**, que es lo que expresa el desenlace |
| Salida esperada | Tres recuentos en 0, y el estado presente. Es la restricción que da sentido a la separación entre `CU-04` y `CU-05` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-10 — El-Alcance-Del-Listado-Cambia-Segun-El-Papel

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-04`; `US-09`; `RN-11`, `RN-03` de `GeometriaFactory-Domain` |
| Setup | Trabajos de dos alumnos, en los cuatro estados |
| Pasos | Given el papel de alumno, When pide el listado, Then recibe **sólo** los propios, en los cuatro estados. Given el papel de administrador, Then recibe los de la comisión **sin ninguno en `Borrador`**, con los datos para agrupar y filtrar por alumno. Given un filtro por un alumno inexistente, Then `CONTRATO_ALUMNO_NO_ENCONTRADO` con un detalle con el campo de filtro |
| Salida esperada | Dos alcances distintos con el mismo tipo, y un rechazo. El listado vacío **no es un error**: llega como colección de cero elementos y se distingue de la indisponibilidad **por el tipo recibido y no por el conteo** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-11 — Resumen-Por-Alumno-Y-Por-Estado

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-04`; `US-10` |
| Setup | Trabajos de varios alumnos en distintos estados |
| Pasos | Given el resumen por alumno y por estado, When se lo pide, Then llega con los recuentos que el contrato declara |
| Salida esperada | Recuentos coherentes con el listado. **Este caso de prueba está fuera del tramo comprometido**: `US-10` es `Could` y deriva de `F-15`, capacidad `Could Have` de la fase `i…`. Se declara para que la cobertura de las veintidós historias quede completa, y **no se compromete** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente`, fuera del tramo comprometido |

#### TC-12 — El-Detalle-Transporta-Piezas-Componentes-Y-Texto

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-05`; `RT-03`; `US-11`, `US-12` |
| Setup | Trabajos con los resultados de interpretación de los escenarios **E-1**, **E-6** y **E-7** del intake §20 |
| Pasos | Given el detalle del trabajo de `E-1`, When se lo pide, Then trae **3** piezas con sus componentes y el texto original. Given el de `E-7`, Then trae **6** piezas que cubren los seis tipos, tres volumétricos y tres planos. Given el de `E-6`, cuya figura declara una dimensión en `0.00`, Then esa pieza **viaja como cualquier otra**: el contrato no la descarta |
| Salida esperada | Tres detalles con sus recuentos de piezas, y el texto original presente en los tres, que es lo que el árbol despliega |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-13 — La-Observacion-Lleva-Severidad-Y-El-Par-De-Valores

| Campo | Valor |
| --- | --- |
| Tipo | Integración e inspección de superficie |
| Cubre | `CU-05`; `US-13`; `RN-09` de `GeometriaFactory-Domain` |
| Setup | Resultados de interpretación de los escenarios **E-3** y **E-4** del intake §20 |
| Pasos | Given el detalle del trabajo de `E-3`, When se lo pide, Then trae **una** observación con severidad y con **el valor declarado 36.00 y el derivado 54.00 en campos propios**. Given el de `E-4`, Then trae **cero** observaciones. When se inspecciona la superficie de la observación, Then los dos valores son campos separados y no un texto compuesto |
| Salida esperada | Una observación con su par, una colección vacía, y dos campos separados. `E-3` y `E-4` son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-14 — El-Comentario-Es-Bloque-Propio-Y-Nunca-Observacion

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie e integración |
| Cubre | `CU-05`, `CU-07`; `RT-09`; `US-18`, `US-20` |
| Setup | Un trabajo con desenlace y comentario, y otro con desenlace sin comentario |
| Pasos | Given la superficie del detalle, When se la inspecciona, Then el comentario es un **bloque propio** y **no comparte ni un campo** con la colección de observaciones. Given el trabajo con comentario y el que no lo tiene, When se piden los dos detalles, Then el comentario es a lo sumo **uno** y su ausencia es un caso admitido |
| Salida esperada | Cero campos compartidos y dos detalles válidos, uno con comentario y otro sin él. Es el error más fácil de cometer contra este contrato, y por eso tiene restricción transversal propia |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Error, desenlace y reseteo

#### TC-15 — El-Tipo-De-Error-Tiene-Cuatro-Campos-Y-Ninguno-Filtra

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie e integración |
| Cubre | `CU-06`; `RT-01`, `RT-02`; `US-14`; NFR «Campos capaces de transportar una dirección de servicio, una ruta de datos o un secreto» |
| Setup | El tipo de error del contrato; y el servicio de datos **detenido** para el segundo tramo |
| Pasos | Given el tipo de error, When se inspecciona su superficie, Then declara **exactamente cuatro** campos —código, texto, detalles y momento— y **0 campos** que puedan transportar una dirección de servicio, una ruta de archivo de datos o un valor de secreto. Given el servicio detenido, When la unidad pública intenta cualquier solicitud, Then recibe `CONTRATO_SERVICIO_NO_DISPONIBLE` con **0 detalles** y con un texto que **no contiene ninguna dirección**. Given un canje con la contraseña equivocada, Then el texto neutro **no nombra ni el campo de correo ni el de contraseña** |
| Salida esperada | Cuatro campos, tres recuentos en 0, y un texto que no revela cuál de los dos datos de ingreso falló. Son `CA-01`, `CA-03` y `CA-04` de `CU-06` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-16 — El-Conjunto-Cerrado-Tiene-Quince-Codigos-Vivos

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie |
| Cubre | `CU-06`; `US-14`, `US-16`; NFR «Códigos de error del conjunto cerrado» |
| Setup | El conjunto cerrado de códigos del contrato, y la tabla de `03` §3.2, que es la única del proyecto de código donde los **dieciocho** identificadores emitidos están enumerados juntos |
| Pasos | Given el conjunto cerrado, When se lo recorre entero, Then tiene exactamente **15** códigos vivos. When se busca un código cuya causa sea una cuenta habilitada sin contraseña, o un reseteo sobre una cuenta sin contraseña, Then **0 códigos** de los quince responden a esas causas. When se comparan los identificadores vivos contra los **tres retirados**, Then **ninguno se recicla**. Given un fallo que el contrato no previó, Then llega `CONTRATO_ERROR_NO_CLASIFICADO` |
| Salida esperada | 15 vivos, 3 retirados sin reciclar, 18 emitidos en total, 0 códigos para las dos causas que `RN-16` volvió imposibles, y el cierre del conjunto verificado. Es `CA-09` de `CU-06` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-17 — El-Detalle-De-Ubicacion-Lleva-Indice-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Integración |
| Cubre | `CU-06`, `CU-05`; `RT-02`; `US-15`; `RN-09` de `GeometriaFactory-Domain` |
| Setup | Fixture del sample **S-2** con el cuerpo del escenario **E-5** del intake §20, cuyo primer elemento es válido a propósito; y el cuerpo del escenario **E-8**, cuya segunda pieza declara una dimensión no legible |
| Pasos | Given el envío de `E-5`, When se lo envía, Then la observación de error del detalle trae **índice de figura 1** y **campo `Tipo`**, y no un texto genérico. Given el envío de `E-8`, Then el resultado trae estado **`Borrador`** y la observación localizada **por índice de figura y campo**, porque el intake resuelve ese desenlace como **error y no como advertencia** [DECISIÓN 2026-08-09, §20.E-8 punto 5]. Given un envío sin el campo de nombre, Then llega `CONTRATO_CAMPO_REQUERIDO_AUSENTE` con **al menos un detalle con el campo señalado** `Nombre`, y el texto no es genérico |
| Salida esperada | Índice 1 y campo `Tipo` en la observación de `E-5`; estado `Borrador` con observación localizada en `E-8`; un detalle con el campo señalado en el error de forma. **El índice 1 y no 0** es lo que prueba que la ubicación se calcula y no se informa siempre la primera figura. Es `CA-02` de `CU-06` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-18 — El-Desenlace-Es-Cerrado-Y-No-Tiene-Vuelta

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie e integración |
| Cubre | `CU-07`; `RT-08`; `US-17`; `RN-10`, `RN-11` de `GeometriaFactory-Domain` |
| Setup | Un trabajo en `Pendiente`, uno en `Rechazado` y uno en `Borrador` |
| Pasos | Given la superficie del desenlace, When se la inspecciona, Then el conjunto es cerrado con **dos** valores y el comentario es **opcional** en los dos. When se busca un tipo, un campo o un valor que permita salir de `Finalizado` o de `Rechazado`, Then hay **0**. Given el trabajo en `Rechazado`, When el administrador pide su desenlace, Then `CONTRATO_ESTADO_NO_PERMITE_DESENLACE`, que **declara el estado actual** y trae **0 campos** que sugieran revertirlo. Given un papel que no es administrador, Then `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` |
| Salida esperada | Dos valores, comentario opcional, tres recuentos en 0 y dos rechazos. Es `CA-03` y `CA-06` de `CU-06` y `CU-07` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-19 — El-Reseteo-No-Lleva-Contrasena-Y-No-Pierde-Trabajos

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie e integración |
| Cubre | `CU-08`, `CU-02`; `RT-01`, `RT-11`; `US-21`, `US-22`; `RN-12`, `RN-14`, `RN-15`, `RN-16` de `GeometriaFactory-Domain` |
| Setup | Una cuenta de alumno con trabajos, y la cuenta de administrador |
| Pasos | Given la superficie de la solicitud de reseteo, When se la inspecciona, Then lleva **un solo campo**, el identificador de cuenta, y **0** campos de contraseña. Given el resultado del reseteo, Then declara la situación **conservada**, el cambio pendiente y **la provisoria producida**, y **0 campos** por los que los trabajos se pierdan. Given el reseteo sobre la cuenta de administrador, Then `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`. Given el cambio obligatorio, When se inspecciona su solicitud, Then es **el mismo tipo** que el cambio voluntario de `CU-02` y no un tipo nuevo |
| Salida esperada | Un campo en la solicitud, tres recuentos en 0, la provisoria en la respuesta, un rechazo, y un solo tipo de solicitud de cambio reutilizado. **Resetear no es dar de baja**, y esto es lo que lo prueba |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Inspecciones de superficie y de construcción

#### TC-20 — Cero-Referencias-Hacia-El-Dominio

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie |
| Cubre | `RT-05`; NFR «Referencias hacia `GeometriaFactory-Domain`»; `BT-02`; `QG-02`; `DXC-01` |
| Setup | El archivo de proyecto y el árbol de fuentes del ensamblado |
| Pasos | Given el archivo de proyecto, When se lo inspecciona, Then declara exactamente **0** referencias hacia `GeometriaFactory-Domain`. When se corre la comprobación reproducible que [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3 publica para `DXC-01`, Then no arroja coincidencias |
| Salida esperada | Recuento en 0 y comprobación sin coincidencias. **La puerta se mide en cada etapa y no sólo en la `a`**: es la vía por la que el intake declara que el acoplamiento vuelve |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-21 — Cien-Por-Ciento-De-Los-Tipos-Ejercitados

| Campo | Valor |
| --- | --- |
| Tipo | Inspección sobre la matriz |
| Cubre | `RT-07`; NFR «Tipos ejercitados por prueba de integración» **[ASUNCIÓN del intake §17.4.P.6, sobre la forma del gate]**; `BT-16`; `QG-05`, **bloqueante** |
| Setup | La matriz tipo contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6 y la superficie pública del ensamblado |
| Pasos | Given todos los tipos de transferencia de las **ocho** familias, When se recorre la matriz, Then cada uno tiene al menos una prueba de integración que lo ejercita contra el servicio real, y **ningún tipo queda sin fila** |
| Salida esperada | **100 %** de los tipos con al menos una prueba. Es el gate equivalente que reemplaza a la cobertura de líneas, y su umbral viene rotulado **[ASUNCIÓN]** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-22 — Ningun-Tipo-Habilita-Al-Navegador-A-Invocar-El-Servicio

| Campo | Valor |
| --- | --- |
| Tipo | Inspección de superficie |
| Cubre | `RT-11`; `RA-01` del intake §14; `QG-09` |
| Setup | La superficie de las **ocho** familias |
| Pasos | Given todos los tipos, When se los recorre, Then **ninguno** está pensado para que el navegador arme la solicitud: todas las solicitudes las arma el servidor de la unidad pública y viajan servidor a servidor, **incluidas las que llevan credenciales en claro** —canje, cambio y reseteo— |
| Salida esperada | Cero tipos que habiliten la invocación desde el navegador. La verificación estructural completa pertenece a `05` y a `09`; lo que se comprueba acá es que ningún tipo la presuponga |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

| Magnitud | Valor |
| --- | --- |
| Casos de prueba declarados | **22**, `TC-01` a `TC-22`, serie contigua |
| Contratos de uso cubiertos | **8 de 8** |
| Restricciones transversales cubiertas | **11 de 11**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5 |
| Historias de usuario cubiertas | **22 de 22**, una de ellas —`US-10`— fuera del tramo comprometido y declarada así |
| NFR con caso de prueba asociado | **6 de 7**; el séptimo, advertencias de construcción, se mide en la etapa `build` y no por un caso de prueba |
| Códigos del conjunto cerrado verificados | **15 vivos** y **3 retirados sin reciclar**, sobre **18** emitidos, agregados en `TC-16` y desplegados en los casos funcionales |
| Escenarios del intake §20 alcanzados | **8 de 8** |
| Casos de prueba sin upstream declarado | **0** |

**Verificación de la cobertura de los ocho escenarios, uno por uno:** `E-1`, `E-6` y `E-7` en `TC-12`; `E-2` en `TC-07`; `E-3` y `E-4` en `TC-13`; `E-5` y `E-8` en `TC-17`. Ninguno se sustituye por datos sintéticos.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** El campo «Cubre» de `TC-21` califica ahora el rótulo [ASUNCIÓN] de `QG-05` como referido a **la forma del gate** y declara el gate **bloqueante**, según §17.4.P.6 y la fila `A-4` del intake §22. **Ningún caso de prueba, paso ni salida esperada cambia.** Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **veintidós** casos de prueba referenciales, `TC-01` a `TC-22`, cada uno con tipo, upstream por identificador, setup, pasos en Given-When-Then, salida esperada, salida observada y estado. Diecinueve cubren los **ocho** contratos de uso y las **veintidós** historias; los tres últimos son inspecciones de superficie sobre las referencias hacia el dominio, el ejercicio de los tipos por integración y la prohibición de que el navegador invoque el servicio. Los **ocho** escenarios del intake §20 quedan alcanzados, con la precisión de qué parte de cada uno le toca a un ensamblado que transporta y no interpreta. `TC-11` se declara fuera del tramo comprometido con su motivo. Todas las salidas observadas dicen «Sin ejecutar» y todos los estados `Pendiente`. |
