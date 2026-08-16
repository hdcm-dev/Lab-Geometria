# Casos de prueba referenciales — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** los **doce** casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/); los **quince** puntos de acceso de [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 y de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.4; las **treinta** historias de [`../06-Backlog-Tecnico/historias-usuario/`](../06-Backlog-Tecnico/historias-usuario/); las **dieciocho** entradas y los **diecisiete** códigos vivos de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §6.1 y de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5; los **diecisiete** NFR de `05` §8; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §20 y §21
**Trazabilidad downstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Cómo se lee este catálogo](#1-cómo-se-lee-este-catálogo)
- [2. Catálogo de casos de verificación](#2-catálogo-de-casos-de-verificación)
  - [2.1 Acceso y admisión de la petición](#21-acceso-y-admisión-de-la-petición)
  - [2.2 Cuentas de la comisión](#22-cuentas-de-la-comisión)
  - [2.3 Trabajos y desenlace](#23-trabajos-y-desenlace)
  - [2.4 Traducción a protocolo](#24-traducción-a-protocolo)
  - [2.5 Composición, arranque y salud](#25-composición-arranque-y-salud)
  - [2.6 Medición, superficie y demostración](#26-medición-superficie-y-demostración)
- [3. Recuento y verificación](#3-recuento-y-verificación)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. Cómo se lee este catálogo

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Integración por protocolo**: la prueba que levanta el proceso real y golpea un punto de acceso con su verbo, su cuerpo y su cabecera de autorización, contra el almacén real.
- **Inspección con umbral exacto**: la que recorre un conjunto cerrado y compara **en las dos direcciones**. Su umbral no admite gradación.
- **Forzando la petición**: la verificación que ejerce una acotación **sin pasar por la interfaz**. Es lo que la fuente exige para la eliminación.
- **Punto de acceso**: uno de los **quince** de `05` §3.4, citado por su identificador `A-XX`.
- **Familia empobrecida**: una de las **tres** en que dos respuestas distintas tienen que ser **indistinguibles** en cuerpo y en código.

## 2. Catálogo de casos de verificación

### 2.1 Acceso y admisión de la petición

#### TC-00001 — Canje-De-Credenciales-Por-Un-Acceso-Firmado

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00001`; punto `A-01`; `US-00001` |
| Setup | Almacén preparado con una cuenta habilitada; clave de firma de prueba provista |
| Pasos | Given una cuenta habilitada y sus credenciales, When se las canjea en `A-01`, Then se devuelve un acceso firmado con sus **cuatro** reclamos y una vigencia declarada. When se usa ese acceso en un punto bajo la guardia, Then la petición se admite |
| Salida esperada | Un acceso emitido y admitido. `A-01` **no está bajo la guardia**: es el punto que produce el acceso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00002 — Credenciales-Invalidas-Sin-Declarar-Que-Campo-Fallo

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00001`, `CU-00009`; punto `A-01`; `US-00002`; **segunda familia empobrecida** |
| Setup | Una cuenta habilitada y un correo que no existe |
| Pasos | Given un correo inexistente, When se lo canjea, Then la respuesta rechaza el acceso. Given el correo correcto con la contraseña equivocada, Then la respuesta es **idéntica en cuerpo y en código**. Then **ninguna de las dos declara cuál campo falló** |
| Salida esperada | Dos respuestas indistinguibles. Es una de las **tres** familias empobrecidas y su umbral no admite gradación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00003 — Cuenta-Pendiente-O-Bloqueada-Con-Motivo

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00001`; `RN-00006`; `INV-06`; punto `A-01`; `US-00003` |
| Setup | Cuentas en estado `Pendiente` y `Bloqueado` |
| Pasos | Given una cuenta `Pendiente`, When intenta canjear, Then la respuesta **niega el acceso con su motivo**, distinto del rechazo por credenciales. Given una `Bloqueado`, Then el motivo es **distinguible** del anterior. Then ninguno de los dos motivos revela nada más que la situación |
| Salida esperada | Dos motivos distinguibles entre sí y del rechazo genérico de `TC-00002`. **La situación de la cuenta sí se dice; cuál campo falló, no** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00004 — Rechazo-Sin-Acceso-Vencido-O-Con-Firma-Ajena

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00002`; los **once** puntos bajo la guardia; `US-00004` |
| Setup | Accesos en sus formas: válido, **vencido**, **con firma ajena** y ausente |
| Pasos | Given una petición **sin** cabecera de autorización sobre un punto bajo la guardia, When se la envía, Then se rechaza. Given un acceso **vencido**, Then también. Given un acceso firmado con **otra clave**, Then también. Then los tres rechazos ocurren **antes** de tocar la capa de aplicación |
| Salida esperada | Tres rechazos, ninguno de los cuales llega a ejercer un caso de uso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00005 — El-Papel-Que-Cada-Punto-Declara

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00002`; `RN-00001`; `US-00005` |
| Setup | Un acceso de alumno y uno de administrador, los dos válidos |
| Pasos | Given un acceso de alumno, When se golpea un punto que declara papel de administrador, Then se rechaza **por papel** y no por pertenencia. Given el acceso de administrador sobre el mismo punto, Then se admite. Then **exigir el papel no reemplaza la comprobación sobre el dato**, que es de la capa de aplicación y ocurre después |
| Salida esperada | Un rechazo por papel y una admisión, con la frontera declarada: **acá se exige el papel, adentro se comprueba la pertenencia** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00006 — La-Guardia-Del-Cambio-Pendiente-Sobre-Todos-Los-Puntos-Salvo-Uno

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00002`; `RN-00013`; `INV-09`; `US-00006` |
| Setup | Un acceso válido de una cuenta **con la marca de cambio de contraseña pendiente puesta** |
| Pasos | Given ese acceso, When se golpea **cada uno** de los once puntos bajo la guardia salvo `A-05`, Then los diez se rechazan con el motivo de cambio pendiente. When se golpea `A-05` —cambiar la contraseña propia—, Then **procede**, y es la **única** excepción. When se completa el cambio, Then los diez puntos vuelven a admitirse |
| Salida esperada | Diez rechazos, una excepción y el levantamiento verificado. Es `INV-09` sostenido desde el borde |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00007 — Exactamente-Cuatro-Puntos-Fuera-De-La-Guardia

| Campo | Valor |
| --- | --- |
| Tipo | **Inspección con umbral exacto** |
| Cubre | NFR de puntos fuera de la guardia (`05` §8); `QG-05`; `RN-00013`; los **quince** puntos de `05` §3.4 |
| Setup | La tabla de puntos de acceso del sistema construido |
| Pasos | Given los **quince** puntos, When se recorre cuál exige acceso firmado, Then exactamente **4** no lo exigen —`A-01`, `A-02`, `A-03` y `A-16`— y **11** sí. When se recorre en la dirección inversa, Then **ningún punto del sistema construido queda fuera de la tabla**. Then **ninguno de los cuatro fija una contraseña sobre una cuenta existente** |
| Salida esperada | 4 y 11, en las dos direcciones, y **0** puntos que fijen contraseña sin credencial. Es el primer riesgo de `05` §9: **un punto nuevo fuera de la guardia hace que `RN-00013` deje de valer y nada falla** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Cuentas de la comisión

#### TC-00008 — Registro-De-Cuenta-Sin-Campo-De-Contrasena

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00003`; `RN-00002`; `INV-01`; punto `A-02`; `US-00007` |
| Setup | Almacén preparado |
| Pasos | Given el punto de registro, When se inspecciona su tipo de petición, Then **no tiene campo de contraseña**. When se registra un correo libre, Then la cuenta se constituye. When se registra un correo ya usado, Then se rechaza **sin revelar el estado ni el papel de la cuenta que lo ocupa**. Then el punto **es anónimo por diseño y así debe seguir** |
| Salida esperada | Un registro aceptado, un rechazo que no revela nada, y la ausencia verificada del campo de contraseña |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00009 — Administrador-Solo-Mientras-No-Exista-Ninguna

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00003`; `RN-00001`; `INV-05`, `INV-08`; punto `A-03`; `US-00008` |
| Setup | Almacén sin administrador, y almacén con administrador |
| Pasos | Given un almacén sin administrador, When se configura por `A-03`, Then la cuenta se constituye habilitada. Given el almacén ya con administrador, When se vuelve a golpear `A-03`, Then se rechaza. Then el punto **no está bajo la guardia porque no hay todavía identidad que pueda autenticarse** |
| Salida esperada | Una configuración y un rechazo, con la exención de guardia justificada por su motivo declarado |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00010 — Cambio-De-Contrasena-Propia-En-Sus-Dos-Formas

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, más inspección |
| Cubre | `CU-00003`; `RN-00016`, `RN-00013`; punto `A-05`; `US-00009`, `US-00010`; NFR de puntos que fijan contraseña sin credencial |
| Setup | Una cuenta recién habilitada con su provisoria, y una con credencial vigente |
| Pasos | Given la cuenta recién habilitada, When cambia su contraseña presentando **la provisoria como vigente**, Then procede y la marca se levanta. Given la cuenta con credencial vigente, When la cambia presentando la vigente, Then procede. Given una petición **sin** la vigente, Then se rechaza. Then, por inspección de los **cuatro** puntos que no exigen acceso, **ninguno fija una contraseña sobre una cuenta existente** |
| Salida esperada | Dos cambios y un rechazo, más un recuento en **0**. `A-05` es **la única excepción de la guardia del cambio pendiente**, y el identificador retirado que fijaba contraseña sin credencial **no se recicla** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00011 — Listado-De-Cuentas-Con-Su-Situacion-Y-Su-Marca

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00004`; punto `A-06`; `US-00011` |
| Setup | Cuentas en los tres estados, con y sin la marca; acceso de administrador |
| Pasos | Given el listado, When se lo pide como administrador, Then cada cuenta trae **su situación y su marca**, con los tipos del ensamblado de contratos y **sin campos agregados ni recortados** por esta capa. Given un acceso de alumno, Then se rechaza por papel |
| Salida esperada | Un listado con situación y marca, y un rechazo por papel |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00012 — Cambio-De-Situacion-Con-Verificacion-De-Papel

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00004`; `RN-00001`, `RN-00006`, `RN-00016`; punto `A-07`; `US-00012` |
| Setup | Cuentas en los tres estados; acceso de administrador y de alumno |
| Pasos | Given una cuenta `Pendiente`, When el administrador la habilita por `A-07`, Then la operación procede **y la respuesta devuelve la provisoria**. When la rehabilita, Then también. Given un par estado-operación no admitido, Then se rechaza con su código. Given un acceso de alumno, Then se rechaza por papel |
| Salida esperada | Dos transiciones con provisoria devuelta y dos rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00013 — Baja-Transportando-El-Correo-Escrito-Como-Confirmacion

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00004`; `RN-00007`; punto `A-08`; `US-00013` |
| Setup | Una cuenta con trabajos; acceso de administrador |
| Pasos | Given la baja con el correo escrito que **coincide**, When se la envía, Then procede y **los trabajos se arrastran**. Given un correo escrito que no coincide, Then se rechaza y **nada cambia**. Then esta capa **transporta** el correo escrito y **no lo compara**: la comparación es de la capa de aplicación |
| Salida esperada | Una baja con arrastre y un rechazo, con la frontera declarada |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00014 — Reseteo-Que-Devuelve-La-Provisoria-Una-Sola-Vez

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00005`; `RN-00012`, `RN-00014`; punto `A-09`; `US-00014` |
| Setup | Una cuenta de alumno con trabajos; acceso de administrador |
| Pasos | Given la cuenta, When el administrador la resetea por `A-09`, Then la respuesta trae **la provisoria, una sola vez**. When se vuelve a consultar la cuenta por cualquier punto, Then **la provisoria ya no aparece en ninguna respuesta**. Then la cuenta conserva su estado y **todos sus trabajos** |
| Salida esperada | Una provisoria devuelta una vez y **0** reapariciones, con el recuento de trabajos idéntico antes y después |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00015 — El-Reseteo-No-Exige-Ni-Comprueba-La-Situacion-De-La-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00005`; `RN-00015`; punto `A-09`; `US-00015` |
| Setup | Cuentas en los **tres** estados; acceso de administrador |
| Pasos | Given una cuenta `Pendiente`, una `Habilitado` y una `Bloqueado`, When el administrador resetea cada una, Then **las tres proceden**. Then el tipo de petición **no lleva ningún campo de situación** y ninguna respuesta declara un motivo por ese concepto |
| Salida esperada | Tres reseteos sobre tres estados distintos, y **0** motivos por situación. Es `RN-00015` verificada **por ausencia** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00016 — La-Provisoria-No-Aparece-En-Ninguna-Traza

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, más inspección del registro del servidor |
| Cubre | `CU-00005`; `RA-03`; punto `A-09`; `US-00016` |
| Setup | El registro del servidor de la ejecución de `TC-00014` |
| Pasos | Given un reseteo ejecutado, When se inspecciona el registro del servidor, Then la provisoria **no aparece en ninguna entrada**. When se inspecciona cualquier respuesta posterior, Then **tampoco**. Then el evento sí queda registrado, **sin el valor** |
| Salida esperada | **0** apariciones del valor y **1** evento registrado sin él |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajos y desenlace

#### TC-00017 — Envio-Que-Responde-Con-Exito-Transportando-El-Estado-Decidido

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00006`; `RN-00005`; `INV-04`; punto `A-10`; `US-00017` |
| Setup | Acceso de alumno; los textos literales de **`E-1`** y de **`E-5`** del intake §20 |
| Pasos | Given el texto de `E-1`, When se lo envía por `A-10`, Then la respuesta es **exitosa** y transporta el estado que la interpretación decidió, con **3 piezas y 2 advertencias** en el cuerpo. Given el texto de `E-5`, cuyo contenido **no verifica**, Then la respuesta **también es exitosa**, con el estado `Borrador` y las observaciones en el cuerpo, y **no** un código de fallo. Given el texto de `E-8`, Then lo mismo |
| Salida esperada | Tres respuestas exitosas con estados distintos. Es el quinto riesgo de `05` §9: **un código de fallo le diría a la persona que su petición estaba mal cuando lo que pasa es que su programa emitió algo que no se puede interpretar** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00018 — Reenvio-De-Un-Borrador-Con-El-Texto-Vuelto-A-Pegar

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00006`; `RN-00008`, `RN-00004`; punto `A-11`; `US-00018` |
| Setup | Un trabajo propio en `Borrador`; un trabajo propio fuera de `Borrador` |
| Pasos | Given un trabajo en `Borrador`, When se lo reenvía por `A-11` con un texto nuevo, Then el texto se reemplaza **entero** y el estado se recalcula. Given un trabajo fuera de `Borrador`, Then se rechaza. Given un trabajo ajeno, Then la respuesta es **la de recurso no visible**, no la de no autorizado |
| Salida esperada | Un reenvío aplicado y dos rechazos, uno de ellos indistinguible del recurso inexistente |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00019 — Texto-Original-Sin-Normalizar-En-El-Borde-Y-Sin-Truncar

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, con comparación byte a byte |
| Cubre | `CU-00006`; `RN-00008`; punto `A-10`; `US-00019`; NFR de textos alterados; `QG-09` |
| Setup | Los textos literales de **`E-1`** y de **`E-2`**, y un cuerpo por encima del límite declarado |
| Pasos | Given el texto de `E-1`, When se lo envía y se recupera lo guardado, Then son **idénticos byte a byte**. Given el de `E-2`, con sus **dos comas finales**, Then también: **el borde no normaliza**. Given un cuerpo por encima del límite, Then **se rechaza y no se trunca**, y la respuesta lo declara |
| Salida esperada | **0** caracteres de diferencia y **0** truncamientos silenciosos. Es el tercer riesgo de `05` §9: truncar **rompe `RN-00008` en silencio** y el alumno lo descubre al ver el dibujo |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00020 — Eliminacion-Con-Los-Dos-Alcances-Forzando-La-Peticion

| Campo | Valor |
| --- | --- |
| Tipo | **Forzando la petición**, integración por protocolo |
| Cubre | `CU-00006`; `RN-00004`, `RN-00003`, `RN-00011`; `INV-02`, `INV-03`; punto `A-12`; `US-00020`; NFR de eliminaciones fuera de alcance; `QG-12` |
| Setup | Trabajos propios en los cuatro estados, trabajos de otro alumno, y accesos de alumno y de administrador |
| Pasos | Given un trabajo propio en `Borrador` y el alumno, When lo elimina, Then procede. Given un trabajo propio **fuera de `Borrador`**, When se **fuerza la petición** contra `A-12` sin pasar por la interfaz, Then **se rechaza**. Given un trabajo **que no pertenece al solicitante**, When se fuerza la petición, Then **se rechaza**, con la respuesta de recurso no visible. Given el administrador sobre los tres estados que ve, Then los tres se eliminan; sobre un `Borrador` ajeno, Then se rechaza |
| Salida esperada | Cuatro eliminaciones y tres rechazos, con **0** eliminaciones fuera de alcance aceptadas. **Es el único criterio de verificación del producto que la fuente exige ejercer forzando la petición contra esta superficie**, y el intake §17.5.P.6 lo declara bloqueante |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00021 — Listado-Sin-Parametro-Para-Pedir-Borradores-Ajenos

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, más inspección |
| Cubre | `CU-00007`; `RN-00011`, `RN-00003`; punto `A-13`; `US-00021` |
| Setup | Trabajos de dos alumnos en los cuatro estados; accesos de los dos papeles |
| Pasos | Given el punto de listado, When se inspecciona su superficie, Then **no existe ningún parámetro con el que pedir borradores ajenos**. Given el acceso de alumno, When se lista, Then vienen sólo los propios. Given el de administrador, Then **ningún `Borrador`** viene. Then el alcance **llega decidido** desde la capa de aplicación y esta capa no lo recalcula |
| Salida esperada | Dos listados acotados y la ausencia verificada del parámetro. **La ausencia de superficie es la garantía más fuerte**: lo que no existe no se puede pedir |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00022 — Detalle-Con-Piezas-Componentes-Observaciones-Y-Comentario

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00007`; `RN-00003`, `RN-00011`; punto `A-14`; `US-00022` |
| Setup | Un trabajo materializado desde el texto de **`E-7`**, con desenlace y comentario; accesos de los dos papeles |
| Pasos | Given el trabajo, When el dueño pide su detalle por `A-14`, Then recibe piezas con su índice, componentes, observaciones, comentario y **el texto original**. Given el administrador, Then recibe **lo mismo**. Given un trabajo ajeno pedido por un alumno, Then la respuesta es la de recurso no visible. Then la **proyección de listado no arrastra el texto** y el detalle sí |
| Salida esperada | Dos detalles equivalentes con las **seis** piezas de `E-7`, y un rechazo indistinguible del inexistente |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00023 — Desenlace-Desde-Pendiente-Con-Su-Terminalidad

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00008`; `RN-00010`, `RN-00011`; `INV-07`; punto `A-15`; `US-00023` |
| Setup | Trabajos en estado `Pendiente`, `Finalizado`, `Rechazado` y `Borrador`; accesos de los dos papeles |
| Pasos | Given un trabajo en estado `Pendiente` y el administrador, When lo aprueba sin comentario y rechaza otro con comentario, Then los dos alcanzan su estado terminal. Given un trabajo ya terminal, Then se rechaza por terminalidad. Given un `Borrador`, Then se rechaza por alcance. Given un acceso de alumno, Then se rechaza **por papel, antes de tocar el dato** |
| Salida esperada | Dos desenlaces y tres rechazos, cada uno con su código distinto |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Traducción a protocolo

#### TC-00024 — Dieciseis-Codigos-Con-Destino-Y-Uno-Sin-El

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **inspección con umbral exacto** |
| Cubre | `CU-00009`; `US-00024`; NFR de códigos con traducción; `QG-06` |
| Setup | El conjunto cerrado de **diecisiete** códigos del ensamblado de contratos, y la tabla de traducción de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 |
| Pasos | Given los diecisiete códigos, When se recorre la tabla, Then **16** tienen código de respuesta asignado y **1** está declarado **sin destino con su motivo**: el que describe que la pieza de datos no responde, porque **si hubo respuesta, el servicio respondió**. When se recorre en la dirección inversa, Then **ninguna fila de la tabla cita un código que no esté en el conjunto cerrado** |
| Salida esperada | 16 con destino, 1 sin él con su motivo, y **0** filas huérfanas en las dos direcciones. **El hueco es intencional y está declarado**, para que una revisión posterior no lo levante como cobertura faltante |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00025 — Las-Tres-Familias-Empobrecidas-Son-Indistinguibles

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00009`; `RN-00003`, `RN-00002`; `INV-02`; `US-00024`; NFR de respuestas indistinguibles; `QG-07` |
| Setup | Trabajos propios y ajenos, un identificador inexistente, cuentas con correos ocupados por una cuenta habilitada y por una bloqueada |
| Pasos | Given un trabajo **ajeno** y un identificador **inexistente**, When se los pide, Then las dos respuestas son **idénticas en cuerpo y en código**. Given un **correo inválido** y una **contraseña inválida** en el canje, Then también. Given un correo **ocupado por una cuenta habilitada** y uno **ocupado por una cuenta bloqueada** en el registro, Then también |
| Salida esperada | **3 de 3** comparaciones idénticas. Es el segundo riesgo de `05` §9: la traducción que parece más informativa es la tentadora, y **ninguna capa de adentro puede repararla** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00026 — Ninguna-Respuesta-Expone-Direccion-Ruta-Secreto-Ni-Traza

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, más inspección del registro del servidor |
| Cubre | `CU-00009`; `RA-03`; `US-00025`; NFR de respuestas que exponen; `QG-08` |
| Setup | Las respuestas de fallo de los **quince** puntos, y el registro del servidor de esa ejecución |
| Pasos | Given cada respuesta de fallo, When se la inspecciona, Then **no contiene** dirección de servicio interno, ruta del almacén, secreto ni traza. Given el registro del servidor de la misma ejecución, Then **el error sí queda registrado**, con lo que la respuesta no lleva |
| Salida esperada | **0** exposiciones sobre los quince puntos y **cobertura completa** en el registro del servidor. Es `RA-03` con su contracara |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00027 — Cero-Codigos-Inventados-Y-Cero-Renombrados

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **inspección con umbral exacto** |
| Cubre | `CU-00009`; NFR de códigos con traducción; `QG-06` |
| Setup | El conjunto de códigos que el sistema construido emite, y el conjunto cerrado del ensamblado de contratos |
| Pasos | Given los dos conjuntos, When se los compara, Then **ningún código emitido está fuera del conjunto cerrado** y **ninguno aparece con un nombre distinto del que el ensamblado declara**. Then esta capa **no agrega, no renombra y no traduce a texto** ningún código |
| Salida esperada | Dos recuentos en **0**. El conjunto cerrado es del ensamblado de contratos y **esta capa no lo amplía** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Composición, arranque y salud

#### TC-00028 — Cuatro-Puertos-Conectados-A-Su-Adaptador

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **inspección con umbral exacto**, con fallo en construcción |
| Cubre | `CU-00010`; `US-00026`; NFR de puertos conectados; `QG-10` |
| Setup | La composición de raíz, y una variante con un adaptador ausente |
| Pasos | Given la composición completa, When se resuelve el grafo, Then los **4** puertos tienen exactamente **un** adaptador cada uno: **0** sin adaptador y **0** con más de uno. Given la variante con un adaptador ausente, Then **falla en construcción** y no en la primera petición |
| Salida esperada | 4 de 4 resueltos y un fallo temprano. Es el séptimo riesgo de `05` §9: sin esto, **el servicio arranca y falla al primer uso, en producción y sin nadie mirando** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00029 — Una-Sola-Configuracion-De-Intercambio-Compartida

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **inspección** |
| Cubre | `CU-00010`; NFR de configuraciones de intercambio; `QG-10` |
| Setup | La composición de raíz de este proyecto de código y el cliente tipado de `GeometriaFactory-Web` |
| Pasos | Given los dos extremos, When se inspecciona su configuración de intercambio, Then hay exactamente **1** declarada en el producto, **compartida por los dos**. Then no hay una segunda declaración en ninguno de los dos lados |
| Salida esperada | Un recuento en **1**. Es el cuarto riesgo de `05` §9: si los dos extremos serializan distinto, **el fallo aparece en tiempo de ejecución y no lo detecta la compilación**, que es la única red que este producto tiene |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00030 — Transformaciones-De-Esquema-Al-Arrancar

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00011`; `US-00027` |
| Setup | Un almacén **inexistente** en la ubicación que la configuración declara |
| Pasos | Given un almacén inexistente, When arranca el servicio, Then **dispara** la preparación, el almacén queda transformado y el servicio atiende. When se vuelve a arrancar sobre el almacén ya preparado, Then **no se aplica nada dos veces**. Then la **transformación la ejecuta el adaptador** y esta capa sólo la dispara |
| Salida esperada | Un arranque con preparación y uno idempotente, con la frontera declarada |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00031 — Arranque-Detenido-Y-Cero-Peticiones-Atendidas

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo, con la preparación forzada a fallar |
| Cubre | `CU-00011`; `US-00028`; NFR de peticiones con preparación incompleta; `QG-11` |
| Setup | Una preparación del almacén que **no puede completarse** |
| Pasos | Given la preparación que falla, When arranca el servicio, Then **el arranque se detiene**. When se golpea cualquiera de los quince puntos, Then **ninguno responde como si el servicio estuviera en condiciones**: el recuento de peticiones atendidas con la preparación incompleta es **0** |
| Salida esperada | Un arranque detenido y **0** peticiones atendidas. **Es preferible no atender a atender mal** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00032 — Salud-Que-Responde-Sin-Exigir-Acceso

| Campo | Valor |
| --- | --- |
| Tipo | Integración por protocolo |
| Cubre | `CU-00011`; punto `A-16`; `US-00029` |
| Setup | El servicio en condiciones, sin ningún acceso emitido |
| Pasos | Given el servicio arrancado, When se golpea `A-16` **sin cabecera de autorización**, Then responde. Then su respuesta **no expone la topología**: ni dirección de servicio interno, ni ruta del almacén. Then el punto **no está bajo la guardia, porque tiene que poder responder cuando nadie puede autenticarse** |
| Salida esperada | Una respuesta sin acceso y **0** exposiciones. Es uno de los **cuatro** puntos exentos, y su exención tiene motivo declarado |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00033 — Arranque-En-Frio-En-Menos-De-Treinta-Segundos

| Campo | Valor |
| --- | --- |
| Tipo | **Medición** |
| Cubre | NFR de arranque en frío (`05` §8); `QG-13` |
| Setup | El contenedor detenido y un almacén vacío |
| Pasos | Given el contenedor detenido, When se lo arranca, Then aplica las transformaciones y **responde salud en menos de 30 segundos**, medido desde el arranque hasta la primera respuesta de `A-16` |
| Salida esperada | Una medición registrada. El umbral viene rotulado **[ASUNCIÓN del intake §17.5.P.10, asunción `A-5`]** y su gate es **condicionado**. Su razón de ser está declarada: **para que la comprobación del despliegue sirva de algo** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.6 Medición, superficie y demostración

#### TC-00034 — Latencia-Del-Listado-Y-Caudal-Sostenido

| Campo | Valor |
| --- | --- |
| Tipo | **Medición**, en la batería de integración |
| Cubre | NFR de latencia y de caudal (`05` §8); `QG-14` |
| Setup | Almacén con el volumen de una comisión; el servicio levantado |
| Pasos | Given el punto de listado, When se lo ejerce repetidamente, Then el **percentil 99** queda por debajo de **500 ms**, medido **en el servidor** y sin contar el tramo de red doméstica. When se sostiene la carga, Then el servicio sostiene **20 peticiones por minuto** |
| Salida esperada | Dos mediciones registradas. Los dos umbrales vienen rotulados **[ASUNCIÓN]** y sus gates son **condicionados**. **La medición se hace en el servidor**: el tramo de internet doméstico no está bajo control y no entra |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00035 — La-Coleccion-De-Peticiones-Reproducible

| Campo | Valor |
| --- | --- |
| Tipo | Ejecución de la colección, en la demostración de etapa |
| Cubre | `CU-00012`; `US-00030`; NFR de pasos de la colección; `QG-15` |
| Setup | El servicio levantado sobre un almacén preparado |
| Pasos | Given la colección versionada con el código, When se la ejecuta, Then recorre la superficie en **5 pasos o menos** y **todos pasan**. Then **ninguno de sus cuerpos es un dato de prueba inventado**: los textos salen de los escenarios del intake §20 y las identidades son valores evidentemente ficticios, declarados como tales. Then la colección incluye **la aprobación y el rechazo** |
| Salida esperada | Una ejecución completa en 5 pasos o menos con **0** datos inventados. **No es una prueba automatizada**: es la forma de demostración que el intake declara para este tipo de proyecto de código |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00036 — Sin-Canal-De-Sesion-Interactiva-Y-Sin-Intercambio-De-Origen-Cruzado

| Campo | Valor |
| --- | --- |
| Tipo | **Inspección con umbral exacto** |
| Cubre | `RA-01`; el sexto riesgo de `05` §9; criterio de aceptación de la etapa `a` |
| Setup | La configuración del proceso y la tabla de los quince puntos |
| Pasos | Given el servicio construido, When se lo inspecciona, Then **no expone ni requiere canal de sesión interactiva**: el circuito del front **termina en el front y no llega hasta acá**. Then **no hay configuración de intercambio de origen cruzado**, porque el navegador no alcanza esta superficie. Then **ningún punto de acceso está pensado para el navegador** |
| Salida esperada | Tres ausencias verificadas. Es el sexto riesgo de `05` §9: reabrir cualquiera de las tres **rompe `RA-01`, que es regla de nivel producto**, y el costo de equivocarse es de rediseño |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-00037 — Forma-De-La-Piramide-De-Pruebas

| Campo | Valor |
| --- | --- |
| Tipo | **Inspección** del informe de la batería |
| Cubre | NFR de forma de la pirámide (`05` §8); `QG-04` |
| Setup | El informe de la etapa de pruebas del pipeline |
| Pasos | Given el informe, When se cuentan las pruebas por clase, Then el reparto es **60 %** de integración y **40 %** unitarias. Then **la inversión respecto de lo habitual es deliberada** y no un desvío |
| Salida esperada | Un recuento registrado. El **reparto numérico** viene rotulado **[ASUNCIÓN]** y su gate es condicionado; **la inversión no es asunción** y no queda en suspenso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 3. Recuento y verificación

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de verificación de este catálogo | **37**, `TC-00001` a `TC-00037` | Contar los encabezados de §2 |
| Casos de uso con al menos un caso de verificación | **12 de 12** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Puntos de acceso ejercidos | **15 de 15** | Matriz §5 |
| Historias con caso de verificación | **30 de 30** | Matriz §2, columna de historias |
| NFR con caso de verificación propio | **15 de 17**; los otros dos son mediciones del pipeline | Matriz §3 |
| Reglas de negocio con lo que esta capa hace por ellas verificado | **16 de 16** | Matriz §4 |
| Inspecciones con umbral exacto | **5** — `TC-00007`, `TC-00024`, `TC-00027`, `TC-00028`, `TC-00036` | §2, columna de tipo |
| Casos que verifican **forzando la petición** | **1** — `TC-00020`, y es el único que la fuente exige así | §2.3 |
| Escenarios del intake §20 usados como cuerpo de petición | **8 de 8** | `E-1` en `TC-00017`, `TC-00019`; `E-2` en `TC-00019`; `E-5` y `E-8` en `TC-00017`; `E-7` en `TC-00022`; `E-3`, `E-4` y `E-6` en `TC-00035` y en la batería del validador que corre desde acá |
| Casos de verificación deshabilitados | **0** | Ninguna fila lo declara |

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y siete** casos de verificación, `TC-00001` a `TC-00037`, repartidos en seis grupos, cada uno con sus ocho campos y su upstream explícito, incluidos el punto de acceso `A-XX` que ejerce y el riesgo de `05` §9 que mitiga. Incluye **cinco** inspecciones con umbral exacto —entre ellas la de los **cuatro** puntos fuera de la guardia, que es el control que más veces hay que ejercer— y **el único** caso que la fuente exige verificar **forzando la petición**. Todos los estados dicen `Pendiente` y todas las salidas observadas dicen «Sin ejecutar». Los **ocho** escenarios del intake §20 entran **como cuerpo de petición**, sin sustituirse por datos sintéticos. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **5**. Sube minor. |
