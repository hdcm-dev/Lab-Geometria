# Casos de prueba referenciales — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Casos-Prueba-Referenciales.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1** §17.1.P.6 y §22
**Trazabilidad downstream:** `09-Devops` y `11-Documentacion`
**Consolida a:** los documentos homónimos de `GeometriaFactory-Domain`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase, y sus cuatro proyectos de código tenían
el suyo.** Cada sección lleva **una subsección por proyecto**, con su texto **transpuesto sin
reescritura**: lo que cambia es el orden y no el contenido.

**Es el documento más grande de la categoría**, y su consolidación es una **unión de catálogo**: los
casos de prueba de las cuatro capas conviven sin colisionar, porque la renumeración les dio rango
propio. **Ninguno se descarta y ninguno se funde**: un `TC` del dominio y uno del host verifican
cosas distintas aunque se parezcan en el título.

---

## 1. Cómo se lee este catálogo

### 1.1 `GeometriaFactory-Api`

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Integración por protocolo**: la prueba que levanta el proceso real y golpea un punto de acceso con su verbo, su cuerpo y su cabecera de autorización, contra el almacén real.
- **Inspección con umbral exacto**: la que recorre un conjunto cerrado y compara **en las dos direcciones**. Su umbral no admite gradación.
- **Forzando la petición**: la verificación que ejerce una acotación **sin pasar por la interfaz**. Es lo que la fuente exige para la eliminación.
- **Punto de acceso**: uno de los **quince** de `05` §3.4, citado por su identificador `A-XX`.
- **Familia empobrecida**: una de las **tres** en que dos respuestas distintas tienen que ser **indistinguibles** en cuerpo y en código.

### 1.2 `GeometriaFactory-Domain`

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes. Declarar cualquier otra cosa sería una afirmación sobre el estado del sistema sin evidencia.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario o integración interna.
- **Fixture**: un constructor de entidad compartido, de los cuatro que declara [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no una regla de negocio.
- **Resultado de interpretación**: el conjunto de piezas y observaciones que el consumidor le aporta al dominio ya producido. El dominio **no lo produce**: lo adopta.

**Un caso de prueba no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten contrato de uso, setup y forma de verificación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

### 1.3 `GeometriaFactory-Application`

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes. Declarar cualquier otra cosa sería una afirmación sobre el estado del sistema sin evidencia.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1. Acá hay un solo nivel: unitario.
- **Doble de puerto**: la sustitución de uno de los **cuatro** puertos de `02` §3, que es la única frontera que una prueba de este proyecto de código sustituye ([`Estrategia-Testing.md`](Estrategia-Testing.md) §5).
- **Fixture**: un constructor compartido, de los cuatro que declara [`Estrategia-Testing.md`](Estrategia-Testing.md) §5.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no un caso de uso.
- **Resultado de interpretación**: el conjunto de piezas, observaciones y cantidad de figuras del conjunto raíz que el puerto de validación devuelve. Esta capa **no lo produce**: lo recibe y lo entrega al dominio.
- **La marca**: la marca de cambio de contraseña pendiente, que es lo que `INV-09` gobierna.

**Un caso de prueba no es una historia.** Varias historias se ejercitan en el mismo `TC-XX` cuando comparten contrato de uso, setup y forma de verificación; la correspondencia completa está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.

### 1.4 `GeometriaFactory-Infrastructure`

Cada `TC-XX` declara ocho campos, según `Rules-Calidad-Y-Pruebas.md` §4.6: identificador y nombre, tipo, upstream cubierto, setup, pasos en Given-When-Then, salida esperada, salida observada y estado.

**Todas las filas de «Salida observada» dicen «Sin ejecutar» y todos los estados dicen `Pendiente`.** No hay sistema construido: el proyecto de código arranca en la etapa `a` y este catálogo se emite antes.

**Vocabulario de este catálogo**, definido acá la primera vez que aparece y no redefinido después:

- **Nivel**: la posición de una prueba en la pirámide de [`Estrategia-Testing.md`](Estrategia-Testing.md) §1 — unitario o integración interna.
- **Integración interna**: la prueba que necesita un **almacén efímero**, creado y descartado por ella misma. No es la batería de integración del producto, que es de `GeometriaFactory-Api`.
- **Fixture**: uno de los cuatro constructores compartidos de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5, incluidos los **ocho textos literales** de los escenarios del intake §20.
- **Prueba de inspección**: la que comprueba una propiedad estructural del proyecto de código y no un contrato.
- **Los diez casos de la batería**: los que [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §10.5 enumera, con su origen en el intake §21.

**Los diez primeros casos de este catálogo son, uno a uno, los diez de la batería.** No se agruparon ni se reordenaron: la correspondencia con la tabla de `05` §10.5 es de identidad, y así se puede recorrer sin traducir.

## 2. Catálogo de casos de verificación

### 2.1 `GeometriaFactory-Api`

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
| Salida esperada | Cuatro eliminaciones y tres rechazos, con **0** eliminaciones fuera de alcance aceptadas. **Es el único criterio de verificación del producto que la fuente exige ejercer forzando la petición contra esta superficie**, y el intake §17.1.P.6 · GeometriaFactory-Api lo declara bloqueante |
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
| Salida esperada | Una medición registrada. El umbral viene rotulado **[ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Api, asunción `A-5`]** y su gate es **condicionado**. Su razón de ser está declarada: **para que la comprobación del despliegue sirva de algo** |
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

### 3.1 `GeometriaFactory-Api`

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

### 3.2 `GeometriaFactory-Domain`

| Magnitud | Valor |
| --- | --- |
| Casos de prueba declarados | **27**, `TC-02001` a `TC-02027`, serie contigua |
| Casos de uso cubiertos | **13 de 13** |
| Reglas de negocio cubiertas | **16 de 16** |
| Invariantes cubiertos | **9 de 9** |
| Historias de usuario cubiertas | **27 de 27** |
| Condiciones del catálogo alcanzadas | **42 de 42**, agregadas en `TC-02023` y desplegadas en los casos funcionales |
| NFR con caso de prueba asociado | **3 de 6**: dependencias salientes (`TC-02024`), cobertura del catálogo (`TC-02023`) y ejercicio de los invariantes (`TC-02026`). Los otros tres —tiempo de la batería, cobertura de líneas y ramas, y advertencias de construcción— **se miden en el pipeline y no por un caso de prueba**, y su tratamiento está en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §3 |
| Escenarios del intake §20 usados como fixture | **8 de 8**, en `TC-02013`, `TC-02014`, `TC-02015`, `TC-02017` y `TC-02018` |
| Casos de prueba sin upstream declarado | **0**. Cada `TC-XX` declara al menos un `CU-XX`, `RN-XX`, `INV-XX` o NFR |

**Verificación de la cobertura de los ocho escenarios, uno por uno:** `E-1` y `E-2` en `TC-02017`; `E-3` y `E-4` en `TC-02015` y `E-3` además en `TC-02017` por vía de `E-1`; `E-5` en `TC-02014`, `TC-02016` y `TC-02018`; `E-6` en `TC-02017`; `E-7` en `TC-02013`; `E-8` en `TC-02018`. Ninguno queda sin caso de prueba y ninguno se sustituye por datos sintéticos.

### 3.3 `GeometriaFactory-Application`

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de prueba de este catálogo | **31**, `TC-04001` a `TC-04031` | Contar los encabezados de §2 |
| Casos de uso con al menos un caso de prueba | **11 de 11** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Reglas de negocio con al menos un caso de prueba | **16 de 16** | Matriz §4 |
| Invariantes con al menos un caso de prueba | **9 de 9** | Matriz §6 |
| Comprobaciones de autorización con prueba de su negativa | **4 de 4**, más **1** prueba de orden (`TC-04011`) | Matriz §5 |
| Historias con caso de prueba | **32 de 32** | Matriz §2, columna de historias |
| NFR con caso de prueba propio | **6 de 9**; los otros tres son mediciones del pipeline | Matriz §3 |
| Escenarios del intake §20 usados como fixture | **8 de 8** | `TC-04015` (`E-1`, `E-2`, `E-3`), `TC-04016` (`E-5`, `E-8`), `TC-04017` (`E-4`, `E-6`), `TC-04022` (`E-7`) |
| Casos de prueba de inspección estructural | **6**, `TC-04026` a `TC-04031` | §2.5 |
| Casos de prueba deshabilitados | **0** | Ninguna fila lo declara |

**Los ocho escenarios están, uno por uno, y ninguno se sustituye.** `E-1`, `E-2` y `E-3` en `TC-04015`; `E-4` y `E-6` en `TC-04017`; `E-5` y `E-8` en `TC-04016`; `E-7` en `TC-04022`. La forma en que entran a esta capa es la que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara: **el resultado de interpretación que el doble del puerto devuelve**, no el texto.

### 3.4 `GeometriaFactory-Infrastructure`

| Magnitud | Valor | Cómo se verifica |
| --- | --- | --- |
| Casos de prueba de este catálogo | **35**, `TC-06001` a `TC-06035` | Contar los encabezados de §2 |
| Casos de la batería del validador | **10 de 10**, `TC-06001` a `TC-06010`, en el mismo orden que `05` §10.5 | §2.1 y [`Estrategia-Testing.md`](Estrategia-Testing.md) §6.1 |
| Casos de uso con al menos un caso de prueba | **10 de 10** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Reglas de negocio con tramo acá y caso de prueba | **14 de 14**; las **dos** sin tramo se declaran | Matriz §4 |
| Reglas conceptuales de modelo con caso de prueba | **7 de 7** | Matriz §5 |
| Historias con caso de prueba | **25 de 25** | Matriz §2, columna de historias |
| NFR con caso de prueba propio | **11 de 14**; los otros tres son mediciones del pipeline | Matriz §3 |
| Escenarios del intake §20 usados como texto literal | **8 de 8** | `E-1` en `TC-06006`, `TC-06009`, `TC-06012`, `TC-06015`; `E-2` en `TC-06001`, `TC-06002`, `TC-06006`, `TC-06016`; `E-3` en `TC-06003`, `TC-06005`; `E-4` en `TC-06004`; `E-5` en `TC-06008`, `TC-06012`; `E-6` en `TC-06007`; `E-7` en `TC-06011`, `TC-06019`; `E-8` en `TC-06010` |
| Casos de prueba de inspección estructural | **3** — `TC-06014`, `TC-06034`, `TC-06035` | §2.2 y §2.6 |
| Casos de integración interna | **11** — `TC-06016` a `TC-06024`, `TC-06032`, `TC-06033` | §2.3 y §2.5 |
| Casos de prueba deshabilitados | **0** | Ninguna fila lo declara |

**Los ocho escenarios están, uno por uno, y entran como texto literal.** `E-7` es el único que no respalda un caso de la batería, y se usa igual como cobertura adicional declarada por `05` §10.5.

## 4. Catálogo de casos de prueba

### 4.1 `GeometriaFactory-Domain`

### 2.1 Ciclo de vida de la cuenta

#### TC-02001 — Alta-De-Alumno-Con-Cuenta-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02001`; `RN-02002`; `INV-01`, `INV-08`; `US-02001` |
| Setup | Ninguno. La operación constituye la entidad; la unicidad del correo llega declarada como comprobada por el consumidor |
| Pasos | Given los datos obligatorios del alta y la unicidad del correo declarada como comprobada, When se constituye el alumno, Then la cuenta nace `Pendiente`, sin credencial derivada y con papel `Alumno` |
| Salida esperada | Entidad constituida; estado de cuenta `Pendiente`; credencial derivada sin valor |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02002 — Rechazos-Del-Alta-De-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02001`; `RN-02001`, `RN-02002`; `INV-01`, `INV-05`, `INV-08`; `US-02002`, `US-02003` |
| Setup | Ninguno |
| Pasos | Given un alta a la que le falta el correo, el nombre o el apellido, When se la invoca, Then se rechaza con `DATO_OBLIGATORIO_AUSENTE`. Given un alta sin la declaración de unicidad comprobada, Then `UNICIDAD_DE_CORREO_NO_VERIFICADA`. Given un alta que aporta credencial derivada, Then `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`. Given un alta que pide un estado distinto de `Pendiente`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given un alta que pide papel `Administrador`, Then `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` |
| Salida esperada | Cinco rechazos, uno por condición, sin entidad constituida en ninguno de los cinco |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02003 — Habilitar-Bloquear-Y-Rehabilitar-Con-Provisoria

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02002`, `CU-02003`; `RN-02016`, `RN-02014`, `RN-02006`; `INV-09`; `US-02004`, `US-02006` |
| Setup | Fixture de cuenta de alumno en los tres estados |
| Pasos | Given una cuenta `Pendiente` y la credencial provisoria ya derivada, When se la habilita, Then queda `Habilitado`, con la credencial fijada y **con la marca de cambio de contraseña pendiente puesta**. Given la habilitación invocada sin aportar la provisoria derivada, Then se rechaza con `HABILITACION_SIN_CREDENCIAL_PROVISORIA`. Given una cuenta `Habilitado`, When se la bloquea y se la rehabilita, Then las dos transiciones proceden. Given un par estado-operación no declarado, Then `TRANSICION_DE_CUENTA_NO_ADMITIDA` |
| Salida esperada | Tres transiciones aplicadas y dos rechazos con su condición; la marca puesta en cada habilitación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02004 — Baja-Con-Arrastre-Y-Confirmacion-Escrita

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02002`; `RN-02007`, `RN-02004`; `INV-03`; `US-02005` |
| Setup | Fixture de cuenta de alumno `Habilitado` más fixture de trabajo en los cuatro estados, los cuatro pertenecientes a esa cuenta |
| Pasos | Given una cuenta con trabajos en los cuatro estados y la confirmación escrita que coincide, When se da de baja, Then la cuenta y **los cuatro trabajos** se materializan como una sola unidad. Given la baja solicitada declarando que los trabajos se conservan, Then `BAJA_SIN_ARRASTRE_DE_TRABAJOS` |
| Salida esperada | Baja con arrastre a los cuatro estados, incluidos `Finalizado` y `Rechazado`; un rechazo con su condición |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02005 — Ninguna-Operacion-Alcanza-A-La-Cuenta-De-Administrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02002`, `CU-02013`; `RN-02001`, `RN-02015`; `INV-05`, `INV-08`; `US-02004`, `US-02005` |
| Setup | Fixture de cuenta de administrador |
| Pasos | Given la cuenta con papel `Administrador`, When se intenta habilitarla, bloquearla, rehabilitarla, darla de baja y resetear su contraseña, Then las **cinco** invocaciones se rechazan con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` y la cuenta queda `Habilitado` |
| Salida esperada | Cinco rechazos con el mismo código, y la cuenta intacta. Es la prueba de regresión de la familia de defectos que se abrió dos veces (`05` §9, segundo riesgo) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02006 — Configurar-El-Administrador-Y-Rechazar-El-Segundo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02012`; `RN-02001`, `RN-02002`; `INV-05`, `INV-08`, `INV-01`; `US-02024`, `US-02025` |
| Setup | Ninguno para el camino feliz; la ausencia de administrador previo llega declarada por el consumidor |
| Pasos | Given la ausencia de administrador declarada, los datos obligatorios y la credencial ya derivada, When se configura, Then la cuenta nace `Habilitado`, con papel `Administrador` y con credencial. Given una segunda configuración, o una invocación sin la declaración de ausencia, Then `ADMINISTRADOR_YA_CONFIGURADO`. Given la configuración sin credencial derivada o con valor vacío, Then `CONFIGURACION_SIN_CREDENCIAL`. Given un estado pedido distinto de `Habilitado`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given la unicidad no declarada, Then `UNICIDAD_DE_CORREO_NO_VERIFICADA`. Given faltando un dato obligatorio, Then `DATO_OBLIGATORIO_AUSENTE` |
| Salida esperada | Una configuración aplicada y cinco rechazos, uno por condición de `CU-02012` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02007 — Reseteo-Que-Conserva-La-Cuenta-Y-Sus-Trabajos

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02013`; `RN-02012`, `RN-02014`, `RN-02015`; `INV-09`; `US-02026` |
| Setup | Fixture de cuenta de alumno en los tres estados, cada una con trabajos en los cuatro estados |
| Pasos | Given una cuenta `Pendiente`, una `Habilitado` y una `Bloqueado`, cada una con sus trabajos, When se resetea su contraseña con la provisoria ya derivada, Then en las tres la credencial se reemplaza, **la marca de cambio de contraseña pendiente queda puesta**, el estado de cuenta **no cambia** y **ningún trabajo se pierde ni cambia de estado**. Given una solicitud armada como si fuera una baja, Then `RESETEO_CON_ARRASTRE_DE_TRABAJOS`. Given una invocación con el valor derivado vacío, Then `VALOR_DERIVADO_VACIO` |
| Salida esperada | Tres reseteos aplicados sobre los tres estados de cuenta, con el recuento de trabajos y sus estados idénticos antes y después; dos rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Credencial y admisibilidad

#### TC-02008 — Reemplazar-La-Credencial-Exigiendo-La-Vigente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02003`; `RN-02006`; `INV-06`; `US-02007` |
| Setup | Fixture de cuenta de alumno `Habilitado` con credencial fijada |
| Pasos | Given la credencial vigente declarada verificada y un valor nuevo ya derivado, When se reemplaza, Then el valor se reemplaza y no se conserva historial. Given el reemplazo sin la declaración de verificación, Then `CREDENCIAL_VIGENTE_NO_VERIFICADA`. Given un valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta `Pendiente` o `Bloqueado`, Then `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`. Given una fijación por primera vez sobre una credencial ya fijada, Then `CREDENCIAL_YA_FIJADA` |
| Salida esperada | Un reemplazo aplicado y cuatro rechazos, uno por condición de `CU-02003` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02009 — El-Cambio-Efectivo-Levanta-La-Marca

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02003`, `CU-02004`; `RN-02013`, `RN-02012`, `RN-02016`; `INV-09`; `US-02027` |
| Setup | Dos cuentas `Habilitado` con la marca puesta: una que la recibió al ser habilitada y otra al ser reseteada |
| Pasos | Given cualquiera de las dos cuentas con la marca puesta, When se reemplaza la credencial declarando verificada la vigente, Then el reemplazo procede **y la marca se levanta**. Given la misma cuenta antes del cambio, When se evalúa su admisibilidad, Then no es admisible con motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`; después del cambio, Then es admisible |
| Salida esperada | La marca se levanta **únicamente** por el cambio efectuado por la propia cuenta, y el resultado es el mismo para los dos orígenes de la marca |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02010 — Admisibilidad-Como-Puerta-Unica

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02004`; `RN-02006`, `RN-02013`, `RN-02016`; `INV-06`, `INV-09`; `US-02008` |
| Setup | Fixture de cuenta de alumno en los tres estados, con y sin la marca |
| Pasos | Given una cuenta `Pendiente`, When se evalúa la admisibilidad, Then devuelve no admisible con motivo `CUENTA_PENDIENTE`. Given `Bloqueado`, Then `CUENTA_BLOQUEADA`. Given `Habilitado` con la marca puesta, Then `CAMBIO_DE_CONTRASENA_PENDIENTE`. Given `Habilitado` sin la marca, Then admisible |
| Salida esperada | Tres motivos de resultado y un admisible. **La operación siempre devuelve resultado y nunca lanza**: los tres códigos son motivo de resultado y no rechazo (`03` §2.3) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajo, interpretación y envío

#### TC-02011 — Constituir-Un-Trabajo-Con-Dueno-Y-Texto-Original

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02005`; `RN-02008`, `RN-02003`; `INV-02`; `US-02009` |
| Setup | Fixture de cuenta de alumno `Habilitado` |
| Pasos | Given un dueño, un nombre, una fecha declarada por el alumno y el texto original, When se constituye el trabajo, Then nace en `Borrador` con el texto **idéntico carácter por carácter** al recibido. Given la constitución sin dueño, Then `TRABAJO_SIN_DUENO`. Given sin nombre o sin fecha, Then `DATO_OBLIGATORIO_AUSENTE`. Given un texto declarado como corrección del que pegó el alumno, Then `TEXTO_ORIGINAL_ALTERADO` |
| Salida esperada | Un trabajo constituido con el texto íntegro, y tres rechazos. La comparación del texto es byte a byte |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02012 — Reedicion-Acotada-Al-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02005`, `CU-02009`; `RN-02004`, `RN-02010`; `INV-03`, `INV-07`; `US-02010`, `US-02019` |
| Setup | Fixture de trabajo en los cuatro estados, del mismo dueño |
| Pasos | Given un trabajo en `Borrador`, When se lo reedita, Then el texto nuevo lo reemplaza y **la interpretación anterior se descarta**. Given un trabajo en `Pendiente`, `Finalizado` o `Rechazado`, When se lo reedita, Then `REEDICION_FUERA_DE_BORRADOR` |
| Salida esperada | Una reedición aplicada y tres rechazos, uno por cada estado que no es `Borrador` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02013 — Adoptar-El-Conjunto-De-Piezas-Con-Identidad-Posicional

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02006`; `RN-02009`; `US-02011`, `US-02012` |
| Setup | Fixture de trabajo en `Borrador` y el **resultado de interpretación** derivado del escenario `E-7` del intake §20: seis piezas, tres volumétricas —`Cilindro`, `Cubo`, `Ortoedro`— y tres planas —`Rectangulo`, `Cuadrado`, `Circulo`— |
| Pasos | Given ese conjunto, When se lo adopta, Then las seis piezas quedan con la posición que su figura ocupa en el conjunto raíz, **sin recalcularla**, y la familia plana o volumétrica se **deriva del tipo** y no se guarda. Given una posición repetida, negativa o fuera de rango, Then `POSICION_DE_PIEZA_INVALIDA`. Given la familia aportada como dato, Then `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`. Given un trabajo `Finalizado` o `Rechazado`, Then `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` |
| Salida esperada | Seis piezas adoptadas con su posición y su familia derivada; tres rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02014 — La-Posicion-De-Una-Figura-No-Adoptada-Queda-Reservada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02006`, `CU-02007`; `RN-02009`; `US-02011`, `US-02014` |
| Setup | Resultado de interpretación derivado del escenario `E-5` del intake §20: dos figuras, la del índice 0 válida y la del índice 1 con tipo fuera del conjunto conocido |
| Pasos | Given ese conjunto, When se lo adopta, Then la pieza del índice 0 **se adopta** y la del índice 1 no, con `TIPO_DE_PIEZA_DESCONOCIDO`; **la posición 1 queda reservada** y la 0 conserva la suya, sin renumerar. When se registra la observación de error sobre la posición 1, Then se adopta, porque una posición reservada **sí pertenece al rango** |
| Salida esperada | Una pieza adoptada, una posición reservada, y la observación de error aceptada sobre esa posición. Es el caso insignia de `RN-02009` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02015 — Advertencia-Con-El-Par-De-Valores

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02007`; `US-02013` |
| Setup | Resultados de interpretación derivados de los escenarios `E-3` y `E-4` del intake §20 |
| Pasos | Given la advertencia de área de `E-3` con declarado 36.00 y derivado 54.00, When se la registra, Then se adopta con **los dos valores**. Given la misma advertencia emitida con un solo número, Then `ADVERTENCIA_SIN_LOS_DOS_VALORES`. Given el resultado de `E-4`, que trae **cero observaciones**, When se lo adopta, Then el trabajo queda sin ninguna |
| Salida esperada | Una advertencia con su par, un rechazo, y un conjunto vacío adoptado sin error. `E-3` y `E-4` son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra, y el contraste es lo que prueba que la verificación mide la geometría y no la forma del texto |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02016 — Error-De-Validacion-Con-Posicion-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02007`; `RN-02009`; `US-02014` |
| Setup | Resultado de interpretación derivado del escenario `E-5` del intake §20 |
| Pasos | Given una observación de especie error de validación atribuible a una figura, con **índice de figura 1** y **campo `Tipo`**, When se la registra, Then se adopta. Given la misma sin posición ni campo, Then `ERROR_SIN_UBICACION`. Given una observación que designa una posición fuera del rango del conjunto raíz interpretado, Then `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`. Given una especie que no es `Advertencia` ni error de validación, Then `ESPECIE_DE_OBSERVACION_DESCONOCIDA` |
| Salida esperada | Una observación adoptada con índice 1 y campo `Tipo`, y tres rechazos. El índice **1 y no 0** es lo que prueba que la ubicación se calcula |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02017 — Envio-Que-Verifica-Y-Pasa-A-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02008`; `RN-02005`; `INV-04`; `US-02015` |
| Setup | Trabajos en `Borrador` con los resultados de interpretación derivados de los escenarios `E-1`, `E-2`, `E-4` y `E-6` del intake §20 |
| Pasos | Given el trabajo de `E-1`, con **3 piezas y 2 advertencias** y sin errores, When se lo envía, Then pasa a `Pendiente`. Given el de `E-2`, con 1 pieza y 1 advertencia de volumen, Then pasa a `Pendiente` **con la advertencia asociada**. Given el de `E-4`, con cero observaciones, Then pasa a `Pendiente`. Given el de `E-6`, que se interpreta y produce a lo sumo una advertencia, Then pasa a `Pendiente`. Given un trabajo ya en `Pendiente`, When se lo reenvía, Then `ENVIO_FUERA_DE_BORRADOR`. Given un envío antes de incorporar el resultado de la interpretación, Then `ENVIO_SIN_INTERPRETACION`. Given un desenlace pedido por esta vía, Then `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` |
| Salida esperada | Cuatro envíos que pasan a `Pendiente` —las advertencias **no** lo impiden— y tres rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02018 — Envio-Que-No-Verifica-Y-Queda-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-02008`; `RN-02005`, `RN-02008`; `INV-04`; `US-02016` |
| Setup | Trabajos en `Borrador` con los resultados de interpretación derivados de los escenarios `E-5` y `E-8` del intake §20 |
| Pasos | Given el trabajo de `E-5`, cuyo resultado trae una observación de severidad `Error`, When se lo envía, Then **queda en `Borrador`** con su texto conservado y no pasa a `Pendiente`. Given el trabajo de `E-8`, cuya dimensión no legible **el intake resuelve como error y no como advertencia** [DECISIÓN 2026-08-09, §20.E-8 punto 5], Then **queda en `Borrador`** con el mensaje localizado por índice de figura y campo |
| Salida esperada | Dos envíos que no transicionan, con el texto original intacto en los dos. `E-8` es el modo de falla que el intake declara **más probable** de todos los escenarios |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02019 — Ninguna-Transicion-Sale-De-Un-Estado-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02008`, `CU-02010`, `CU-02006`; `RN-02010`; `INV-07`; `US-02017` |
| Setup | Fixture de trabajo en `Finalizado` y en `Rechazado` |
| Pasos | Given un trabajo en cualquiera de los dos estados terminales, When se pide enviar, reeditar, reconstruir el conjunto de piezas o aplicar un desenlace nuevo, Then todas las invocaciones se rechazan y **ni el estado ni el contenido cambian** |
| Salida esperada | Rechazo en los dos estados y en las cuatro operaciones, con `TRANSICION_DESDE_ESTADO_TERMINAL`, `REEDICION_FUERA_DE_BORRADOR` y `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` según la operación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Acceso, alcance y desenlace

#### TC-02020 — Trabajo-Ajeno-Indistinguible-De-Inexistente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02009`; `RN-02003`, `RN-02004`; `INV-02`, `INV-03`; `US-02018`, `US-02019` |
| Setup | Dos cuentas de alumno y un trabajo de cada una, en los cuatro estados |
| Pasos | Given un trabajo de otro alumno cuyo identificador el solicitante conoce, y un identificador inexistente, When se resuelve el acceso, Then los **dos** devuelven `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, con resultado idéntico. Given un trabajo propio fuera de `Borrador`, When se consulta reeditar o eliminar, Then `OPERACION_FUERA_DE_BORRADOR`; When se consulta ver, Then procede en los cuatro estados. Given una operación fuera del conjunto declarado, Then `OPERACION_DESCONOCIDA` |
| Salida esperada | Dos resultados idénticos para el ajeno y el inexistente —comparados campo por campo—, la acotación al borrador de reeditar y eliminar, y ver admitido en los cuatro estados |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02021 — Alcance-Del-Administrador-Sin-Los-Borradores

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02011`; `RN-02011`, `RN-02004`, `RN-02001`; `INV-05`; `US-02022`, `US-02023` |
| Setup | Fixture de trabajo en los cuatro estados |
| Pasos | Given un trabajo en `Borrador`, When el administrador consulta su alcance, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given los otros **tres** estados, Then entran en su alcance y **admiten eliminación**, incluidos los dos terminales. Given un papel que no es `Administrador`, Then `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR`. Given una operación fuera del conjunto declarado, Then `OPERACION_DESCONOCIDA` |
| Salida esperada | Un borrador excluido, tres estados con eliminación admitida, y dos motivos de resultado |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02022 — Desenlace-Exclusivo-Y-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-02010`; `RN-02010`, `RN-02011`, `RN-02001`; `INV-07`, `INV-05`; `US-02020`, `US-02021` |
| Setup | Fixture de trabajo en los cuatro estados y fixture de cuenta de administrador |
| Pasos | Given un trabajo en `Pendiente` y el papel `Administrador`, When se lo aprueba, Then pasa a `Finalizado`; When se lo rechaza, Then pasa a `Rechazado`; el comentario es **opcional** en los dos. Given un trabajo en otro estado, Then `DESENLACE_FUERA_DE_PENDIENTE`. Given un papel que no es `Administrador`, aun sobre un trabajo propio, Then `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`. Given un desenlace que no es aprobar ni rechazar, Then `DESENLACE_DESCONOCIDO`. Given un trabajo terminal, Then `TRANSICION_DESDE_ESTADO_TERMINAL` |
| Salida esperada | Dos desenlaces aplicados, con y sin comentario, y cuatro rechazos |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Pruebas de inspección estructural

#### TC-02023 — Catalogo-De-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | NFR «Cobertura del catálogo de condiciones» de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8; `BT-02008` |
| Setup | El conjunto de códigos que la biblioteca puede emitir, y el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §6.2 |
| Pasos | Given los dos conjuntos, When se los compara **en las dos direcciones**, Then no hay ningún código emitido que falte en el catálogo, ni ninguna de las **42** condiciones del catálogo sin al menos una prueba que la alcance |
| Salida esperada | **42 de 42** alcanzadas y **0** emitidas fuera del catálogo. Los **cinco** identificadores retirados —tres por renombre y dos por imposibilidad de su causa (`03` §6.1)— **no se reciclan** y su aparición es una falla |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02024 — Cero-Dependencias-Salientes

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | NFR «Dependencias salientes» de `05` §8; `BT-02004`; `QG-04` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Setup | El archivo de proyecto de la biblioteca |
| Pasos | Given el archivo de proyecto, When se lo inspecciona, Then declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización |
| Salida esperada | Dos recuentos en 0. Es la propiedad que justifica el estilo entero y el intake la declara como condición de la capa (§17.1.P.1 · GeometriaFactory-Domain) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02025 — El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | [`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `BT-02009`; `05` §7, filas de configuración y de zona horaria |
| Setup | El código de las operaciones públicas |
| Pasos | Given todas las operaciones, When se las inspecciona, Then ninguna obtiene el momento por su cuenta ni consulta conjuntos de entidades. When se corre la batería completa sin fijar el reloj del entorno, Then el resultado es idéntico en dos ejecuciones consecutivas |
| Salida esperada | Cero ocurrencias de lectura de reloj y de consulta de conjunto; dos ejecuciones con resultado idéntico |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02026 — Los-Nueve-Invariantes-Ejercidos-Sin-Dobles

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección sobre la matriz, nivel unitario |
| Cubre | NFR «Ejercicio de los invariantes» de `05` §8; `BT-02014`; `INV-01` a `INV-09` |
| Setup | La matriz de §5 de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) |
| Pasos | Given los **nueve** invariantes, When se recorre la matriz, Then cada uno tiene al menos una prueba que **verifica su violación rechazada**, y ninguna de esas pruebas usa dobles |
| Salida esperada | **9 de 9** con prueba de violación rechazada y **0** dobles. Es la mitigación declarada del riesgo de que un invariante se ejerza en un componente y no en otro |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-02027 — Ninguna-Condicion-Prevista-Viaja-Como-Excepcion

| Campo | Valor |
| --- | --- |
| Tipo | Prueba de inspección, nivel unitario |
| Cubre | [`ADR-02002`](../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `BT-02007`; `QG-08` |
| Setup | Las invocaciones que producen cada una de las **42** condiciones |
| Pasos | Given cada condición del catálogo, When se la provoca, Then el resultado llega como **valor de retorno tipado** con su código, y **ninguna** invocación lanza. When se invoca con un argumento nulo donde el contrato exige valor, Then sí se lanza: es defecto de programación del consumidor y no una regla de negocio |
| Salida esperada | 42 rechazos por valor de retorno y 0 excepciones de negocio; la distinción con el defecto de programación verificada aparte |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 4.2 `GeometriaFactory-Application`

### 2.1 Alta y gobierno de cuentas

#### TC-04001 — Alta-De-Alumno-Con-La-Unicidad-Resuelta-Por-El-Puerto

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04001`; `RN-04002`, `RN-04006`; `INV-01`; `US-04001` |
| Setup | Doble del repositorio de cuentas que responde que el correo **no** está registrado; doble del reloj con un momento fijo elegido por la prueba |
| Pasos | Given los datos obligatorios del alta y un correo que el repositorio declara libre, When se invoca el auto-registro, Then la cuenta se constituye **`Pendiente`, sin credencial y con papel `Alumno`**, y la materialización se pide una sola vez |
| Salida esperada | Cuenta constituida en estado `Pendiente`, sin credencial derivada; el doble de repositorio recibe exactamente una materialización; el momento usado es el que la prueba fijó |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04002 — Rechazos-Del-Auto-Registro

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04001`; `RN-04001`, `RN-04002`; `INV-01`, `INV-05`; `US-04002` |
| Setup | Doble del repositorio de cuentas en dos configuraciones: correo libre y correo ocupado |
| Pasos | Given un correo que el repositorio declara ocupado, When se invoca el alta, Then `CORREO_YA_REGISTRADO`. Given el mismo correo declarado libre pero rechazado por la materialización, Then **el mismo motivo por el segundo camino**. Given un alta a la que le falta un dato obligatorio, Then `DATO_OBLIGATORIO_AUSENTE`. Given un alta que aporta credencial, Then `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`. Given un alta que pide un estado distinto de `Pendiente`, Then `ESTADO_INICIAL_NO_NEGOCIABLE`. Given un alta que pide papel `Administrador`, Then `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` |
| Salida esperada | Seis rechazos —`CORREO_YA_REGISTRADO` por sus **dos** caminos y cuatro motivos más—, **sin cuenta constituida** en ninguno y sin unidad de trabajo abierta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04003 — Configurar-El-Administrador-Y-Rechazar-El-Segundo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04010`; `RN-04001`, `RN-04002`, `RN-04006`; `INV-05`, `INV-08`, `INV-01`; `US-04003`, `US-04028` |
| Setup | Doble del repositorio de cuentas que responde si ya existe una cuenta con papel `Administrador`, en sus dos formas; doble del reloj |
| Pasos | Given que el repositorio declara que no existe administrador, los datos obligatorios y la credencial **ya derivada**, When se configura, Then la cuenta nace **`Habilitado`, con papel `Administrador` y con credencial**. Given que ya existe uno, Then `ADMINISTRADOR_YA_CONFIGURADO`. Given la configuración sin credencial derivada, Then `CONFIGURACION_SIN_CREDENCIAL`. Given un estado pedido distinto de `Habilitado`, Then `ESTADO_INICIAL_NO_NEGOCIABLE` **por la causa opuesta a la de `TC-04002`**. Given un correo ya registrado, Then `CORREO_YA_REGISTRADO`. Given un dato obligatorio ausente, Then `DATO_OBLIGATORIO_AUSENTE` |
| Salida esperada | Una configuración aplicada y cinco rechazos. La fila de `ESTADO_INICIAL_NO_NEGOCIABLE` se verifica **en sus dos causas opuestas**, que es lo que [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §1.4 declara |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04004 — Habilitar-Bloquear-Y-Rehabilitar-Produciendo-La-Provisoria

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04002`, `CU-04003`; `RN-04016`, `RN-04014`, `RN-04006`, `RN-04001`; `INV-09`, `INV-06`; `US-04004`, `US-04008` |
| Setup | Fixture de cuenta de alumno en los tres estados; fixture de solicitante administrador sin marca; dobles del repositorio de cuentas y del reloj |
| Pasos | Given una cuenta `Pendiente` y el administrador como solicitante, When se la habilita, Then queda `Habilitado`, con la credencial provisoria **ya derivada** fijada y **con la marca puesta**. Given la habilitación invocada sin aportar el valor derivado, Then `HABILITACION_SIN_CREDENCIAL_PROVISORIA`. Given el valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta `Habilitado`, When se la bloquea y se la rehabilita, Then las dos transiciones proceden y la rehabilitación **vuelve a poner la marca**. Given un par estado-operación no declarado, Then `TRANSICION_DE_CUENTA_NO_ADMITIDA`. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` |
| Salida esperada | Tres transiciones aplicadas con la marca puesta en las dos que producen provisoria, y cuatro rechazos con su motivo. **`RN-04014` no se ejerce acá**: lo que se verifica es que el valor llega ya producido y ya derivado y que la operación lo rechaza vacío |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04005 — Baja-Con-Confirmacion-Escrita-Y-Arrastre-En-Una-Sola-Unidad-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04002`; `RN-04007`, `RN-04004`, `RN-04001`; `INV-03`, `INV-08`; `US-04005`, `US-04006` |
| Setup | Fixture de cuenta de alumno `Habilitado` con trabajos en los **cuatro** estados; fixture de solicitante administrador; dobles de los repositorios de cuentas y de trabajos |
| Pasos | Given la cuenta con sus cuatro trabajos y el correo escrito que **coincide**, When se da de baja, Then la cuenta y **los cuatro trabajos** se materializan en **una sola** unidad de trabajo. Given un correo escrito que no coincide, Then `CONFIRMACION_DE_BAJA_NO_COINCIDE` y **nada cambia**. Given la baja pedida sobre la cuenta de administrador, Then `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` |
| Salida esperada | Baja con arrastre a los cuatro estados, incluidos `Finalizado` y `Rechazado`, con **una** apertura de unidad de trabajo y no dos; tres rechazos sin efecto parcial. Es el caso testigo del NFR de unidades de trabajo por caso de uso |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04006 — Reseteo-Que-Conserva-Cuenta-Estado-Y-Todos-Sus-Trabajos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04011`; `RN-04012`, `RN-04015`, `RN-04014`, `RN-04013`; `INV-09`; `US-04029`, `US-04031` |
| Setup | Fixture de cuenta de alumno en los **tres** estados, cada una con trabajos en los cuatro; fixture de solicitante administrador; dobles de los repositorios y del reloj |
| Pasos | Given una cuenta `Pendiente`, una `Habilitado` y una `Bloqueado`, cada una con sus trabajos, When el administrador resetea su contraseña con la provisoria **ya producida y ya derivada**, Then en las tres la credencial se reemplaza, **la marca queda puesta**, el estado de cuenta **no cambia** y **ningún trabajo se pierde ni cambia de estado ni pierde su comentario**. Given el valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta inexistente, Then `CUENTA_INEXISTENTE` |
| Salida esperada | Tres reseteos aplicados sobre los tres estados de cuenta, con el recuento de trabajos y sus estados idénticos antes y después; **cero** retiros pedidos al repositorio de trabajos; dos rechazos. La ausencia de comprobación de estado es lo que verifica `RN-04015` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04007 — El-Reseteo-Esta-Acotado-A-Cuentas-De-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04011`; `RN-04001`, `RN-04015`; `INV-08`, `INV-05`; `US-04029` |
| Setup | Fixture de cuenta de administrador; fixture de solicitante administrador y de solicitante alumno |
| Pasos | Given la cuenta con papel `Administrador` como objeto del reseteo, When se lo pide, Then `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y la cuenta queda intacta. Given un solicitante alumno sobre una cuenta ajena, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **antes** de tocar el repositorio |
| Salida esperada | Dos rechazos con su motivo, cero escrituras, y la constancia de que la negativa por facultad se resuelve sin consultar el repositorio de cuentas |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Ingreso, credencial y la marca

#### TC-04008 — Admisibilidad-Con-Su-Motivo-En-Los-Tres-Estados

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`; `RN-04006`, `RN-04013`; `INV-06`, `INV-09`; `US-04007` |
| Setup | Fixture de cuenta de alumno en los tres estados, con y sin la marca; doble del repositorio de cuentas |
| Pasos | Given una cuenta `Pendiente`, When se consulta la admisibilidad, Then **no admisible** con `CUENTA_PENDIENTE`. Given una `Bloqueado`, Then `CUENTA_BLOQUEADA`. Given una `Habilitado` sin marca, Then **admisible**. Given una `Habilitado` **con la marca puesta**, Then **no admisible** con `CAMBIO_DE_CONTRASENA_PENDIENTE`. Given un correo que no corresponde a ninguna cuenta, Then `CUENTA_INEXISTENTE` |
| Salida esperada | Cinco resultados distintos, con los **motivos sin colapsar**: `CUENTA_PENDIENTE` y `CUENTA_BLOQUEADA` no se confunden entre sí ni con la marca. Ninguna invocación lanza |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04009 — Reemplazo-De-Credencial-Exigiendo-La-Vigente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`; `RN-04013`, `RN-04006`; `INV-09`, `INV-06`; `US-04009` |
| Setup | Fixture de cuenta de alumno `Habilitado` con credencial fijada; doble del repositorio de cuentas |
| Pasos | Given la credencial vigente declarada verificada y un valor nuevo **ya derivado**, When la propia cuenta la reemplaza, Then el valor se reemplaza. Given la vigente **no** verificada, Then `CREDENCIAL_VIGENTE_NO_VERIFICADA`. Given un valor derivado vacío, Then `VALOR_DERIVADO_VACIO`. Given una cuenta que no está habilitada, Then `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`. Given una fijación sobre una cuenta que ya tiene credencial, Then `CREDENCIAL_YA_FIJADA` |
| Salida esperada | Un reemplazo aplicado y cuatro rechazos, cada uno con su motivo y sin escritura parcial |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04010 — La-Marca-Se-Levanta-Solo-Con-El-Cambio-Hecho-Por-La-Propia-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04003`, `CU-04002`, `CU-04011`; `RN-04013`, `RN-04016`, `RN-04012`; `INV-09`; `US-04032`, `US-04030` |
| Setup | Fixture de cuenta de alumno `Habilitado` **con la marca puesta**; dobles del repositorio de cuentas y del reloj |
| Pasos | Given la cuenta con la marca, When la **propia cuenta** reemplaza su credencial presentando la vigente verificada, Then el valor se reemplaza **y la marca se levanta**. Given la misma cuenta, When el administrador la resetea, Then la marca **queda puesta** y no se levanta. Given la misma cuenta, When el administrador la habilita o la rehabilita, Then la marca **queda puesta**. Given cualquier otra operación de la capa sobre esa cuenta, Then la marca **no cambia** |
| Salida esperada | **Un solo camino levanta la marca** y los otros tres la ponen o la dejan como estaba. Es la verificación directa de `INV-09` y de la precisión 5 de `02` §4 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04011 — La-Cuarta-Comprobacion-Corta-Antes-Que-Las-Otras-Tres

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de orden** |
| Cubre | Los **once** casos de uso, de forma transversal; las **cuatro** comprobaciones de `02` §4; `RN-04013`, `RN-04003`, `RN-04001`, `RN-04011`; `INV-09`, `INV-02`, `INV-03`; `US-04030` |
| Setup | Fixture de solicitante en sus **cuatro** formas; fixture de trabajo propio y ajeno en los cuatro estados; dobles de los cuatro puertos, **sin base de datos** |
| Pasos | Given un solicitante **con la marca puesta** que además pide un trabajo ajeno, When invoca cualquier caso de uso salvo el reemplazo de `CU-04003` FA-05, Then el motivo emitido es `CAMBIO_DE_CONTRASENA_PENDIENTE` y **no** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given un solicitante con la marca que además carece de facultad, Then el motivo es el de la marca y **no** `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`. Given un solicitante con la marca sobre un borrador ajeno al alcance del administrador, Then el motivo es el de la marca y **no** `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given el reemplazo de `CU-04003` FA-05, Then la marca **no corta** y la operación procede |
| Salida esperada | La cuarta comprobación gana sobre las otras tres en los tres cruces, y cede **sólo** en la única excepción declarada. `05` §8 exige exactamente **1** prueba de este orden, y ésta es esa prueba |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04012 — Pertenencia-Y-Facultad-No-Se-Confunden

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`, `CU-04005`, `CU-04006`, `CU-04009`, `CU-04007`, `CU-04008`, `CU-04002`, `CU-04011`; `RN-04003`, `RN-04001`; `INV-02`; `US-04012`, `US-04025` |
| Setup | Fixture de trabajo propio y ajeno; fixture de solicitante alumno y administrador, ninguno con marca; dobles de los cuatro puertos |
| Pasos | Given un trabajo **ajeno** y un identificador **inexistente**, When el alumno los pide, Then los dos resultados son **idénticos**: `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, sin ninguna diferencia observable. Given una operación reservada pedida por un alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, que sí es explícito. Given un solicitante no declarado, Then `SOLICITANTE_NO_DECLARADO`. Given un papel que no es ninguno de los dos, Then `PAPEL_NO_RECONOCIDO` |
| Salida esperada | Indistinguibilidad verificada en los dos sentidos y las dos negativas separadas. Es la mitigación del tercer riesgo de `05` §9 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Trabajo, envío e interpretación

#### TC-04013 — Constituir-El-Trabajo-Con-Dueno-Y-Sello-Del-Reloj

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`; `RN-04008`, `RN-04003`; `INV-02`; `US-04010`, `US-04011` |
| Setup | Doble del reloj con **dos momentos consecutivos distintos** elegidos por la prueba; doble del repositorio de trabajos |
| Pasos | Given dueño, nombre, fecha declarada por el alumno y texto original, When se constituye el trabajo, Then nace en `Borrador`, con dueño, con identificador propio y con el sello tomado **del puerto de reloj** y no del reloj del entorno. Given un trabajo sin dueño, Then `TRABAJO_SIN_DUENO`. Given un dato obligatorio ausente, Then `DATO_OBLIGATORIO_AUSENTE`. Given el mismo texto de `E-2` cargado y recuperado, Then el texto es **idéntico carácter por carácter** |
| Salida esperada | Trabajo constituido con los tres sellos distinguibles —la fecha del alumno y los del sistema—, texto íntegro y dos rechazos. Dos ejecuciones consecutivas con momentos distintos producen sellos distintos y **el mismo resultado por lo demás** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04014 — Reeditar-Solo-Un-Trabajo-Propio-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04004`; `RN-04003`, `RN-04004`, `RN-04008`; `INV-02`, `INV-03`; `US-04012`, `US-04011` |
| Setup | Fixture de trabajo en los cuatro estados, propio y ajeno; dobles de los puertos |
| Pasos | Given un trabajo propio en `Borrador`, When se lo reedita con un texto nuevo, Then el texto se reemplaza **entero** y la interpretación anterior se descarta. Given un trabajo propio fuera de `Borrador`, Then `OPERACION_FUERA_DE_BORRADOR`. Given un trabajo ajeno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given una reedición que pretende alterar el texto conservado sin reemplazarlo, Then `TEXTO_ORIGINAL_ALTERADO` |
| Salida esperada | Una reedición aplicada en los tres estados donde no procede y tres rechazos; el texto nunca queda mezclado entre la versión vieja y la nueva |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04015 — Envio-Con-Advertencias-Que-Pasa-A-Pendiente

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`, `RN-04008`, `RN-04009`; `INV-04`; `US-04013` |
| Setup | Doble del puerto de validación cargado con los resultados de interpretación de los escenarios **`E-1`, `E-2` y `E-3`** del intake §20; fixture de trabajo propio en `Borrador` |
| Pasos | Given el resultado de `E-1` —**3 piezas y 2 advertencias**, ninguna de severidad `Error`—, When se envía el trabajo, Then el dominio lo lleva a `Pendiente` y las dos advertencias quedan incorporadas. Given el de `E-2` —1 pieza, 1 advertencia de volumen—, Then pasa a `Pendiente` **con la advertencia asociada**. Given el de `E-3` —advertencia de área con el par declarado **36.00** y derivado **54.00**—, Then pasa a `Pendiente` y el par de valores llega entero, sin texto genérico y **sin corregir el valor del alumno** |
| Salida esperada | Tres envíos que pasan a `Pendiente` con sus observaciones incorporadas; el texto original intacto en los tres. **Los datos son los del intake §20 y no se sustituyen por datos sintéticos** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04016 — Envio-Con-Error-Que-Queda-En-Borrador

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`, `RN-04009`, `RN-04008`; `INV-04`; `US-04014` |
| Setup | Doble del puerto de validación cargado con los resultados de **`E-5`** y **`E-8`**; fixture de trabajo propio en `Borrador` |
| Pasos | Given el resultado de `E-5` —una observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**, y la primera pieza interpretada igual—, When se envía, Then el trabajo **queda en `Borrador`**, con su texto conservado y con la observación ubicada. Given el resultado de `E-8` —dimensión no legible—, Then **también es error y no advertencia**, y el trabajo **queda en `Borrador`** [DECISIÓN 2026-08-09 del intake §20.E-8 punto 5]. Given una observación sin índice de figura o sin campo, Then `OBSERVACION_MAL_FORMADA` |
| Salida esperada | Dos envíos que **no** transicionan, con las observaciones localizadas por índice y campo, más un rechazo por observación mal formada. La distinción entre `E-3` —advertencia— y `E-8` —error— es lo que este caso separa |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04017 — Envio-Sin-Observaciones-Y-Con-Dimension-En-Cero

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04005`; `INV-04`; `US-04013` |
| Setup | Doble del puerto de validación cargado con los resultados de **`E-4`** y **`E-6`** |
| Pasos | Given el resultado de `E-4` —**cero observaciones en total**—, When se envía, Then el trabajo pasa a `Pendiente` **sin ninguna observación que incorporar**. Given el de `E-6` —una figura que **se interpreta** y produce a lo sumo una advertencia—, Then el trabajo pasa a `Pendiente` y la figura **no se descarta** |
| Salida esperada | Dos envíos que pasan a `Pendiente`; el de `E-4` con la colección de observaciones vacía y no ausente. Es el **criterio negativo**, que el intake §20.E-4 declara más difícil de acertar que el positivo |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04018 — Interpretacion-Por-El-Puerto-Sin-Tocar-El-Almacen

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04009`; `US-04015` |
| Setup | Dobles de los cuatro puertos, con el de validación instrumentado para contar invocaciones |
| Pasos | Given un envío, When se lo ejecuta, Then el puerto de validación se invoca **exactamente una vez** con el texto original tal cual, y el resultado —piezas, observaciones y **la cantidad de figuras del conjunto raíz**— se entrega al dominio sin recomponerlo. Given la cantidad de figuras que el puerto declara y una observación cuyo índice cae fuera de ese rango, Then `CONJUNTO_DE_PIEZAS_MAL_FORMADO` |
| Salida esperada | Una invocación al puerto, cero al almacén real, y la cantidad de figuras usada como rango de validación de la posición. Es lo que `02` §3 declara que sólo `CU-04005` hace viajar |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04019 — Terminacion-Controlada-Cuando-La-Interpretacion-No-Esta-Disponible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04005`; `RN-04008`; `US-04016` |
| Setup | Doble del puerto de validación configurado como **no disponible**; fixture de trabajo propio en `Borrador` con su texto |
| Pasos | Given el puerto de validación no disponible, When se envía el trabajo, Then se devuelve `INTERPRETACION_NO_DISPONIBLE` **como valor y no como excepción**, el trabajo **queda en `Borrador`**, el texto original queda **intacto** y no se abre ninguna unidad de trabajo de escritura. Given un envío sobre un trabajo que no está en `Borrador`, Then `ENVIO_FUERA_DE_BORRADOR` |
| Salida esperada | Un rechazo controlado sin efecto parcial y un rechazo por estado. Ninguna excepción escapa de la capa |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Consulta, alcance y desenlace

#### TC-04020 — Listado-Propio-Con-Los-Cuatro-Estados-Y-Sin-Componentes

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04006`; `RN-04003`; `INV-02`; `US-04017`, `US-04019` |
| Setup | Fixture de trabajo en los cuatro estados, propios y de otro alumno; doble del repositorio de trabajos que registra el predicado recibido |
| Pasos | Given trabajos propios en los cuatro estados y trabajos de otro alumno, When el alumno pide su listado, Then recibe **sólo los propios**, con los cuatro estados distinguibles, y el predicado de dueño **llegó en la consulta** y no se aplicó después. Given el mismo listado, Then **ningún componente de pieza viene materializado** |
| Salida esperada | Listado acotado por consulta, cuatro estados distinguibles y cero componentes cargados |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04021 — Listado-De-La-Comision-Sin-Borradores-Y-Con-Filtro-Por-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04007`; `RN-04011`, `RN-04001`; `US-04020`, `US-04021` |
| Setup | Fixture de trabajo en los cuatro estados de **dos** alumnos distintos; fixture de solicitante administrador y alumno |
| Pasos | Given trabajos de dos alumnos en los cuatro estados, When el administrador pide el listado de la comisión, Then **ningún trabajo en `Borrador`** aparece y cada fila trae su dueño. Given el filtro por un alumno, Then el recorte **se compone con** el predicado de alcance y no lo reemplaza. Given un solicitante alumno, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **sin consultar el repositorio de trabajos** |
| Salida esperada | Listado sin borradores, filtro compuesto con el alcance, negativa por facultad resuelta antes de consultar |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04022 — Detalle-Equivalente-Para-Los-Dos-Papeles-Con-Desenlace-Y-Comentario

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04006`, `CU-04007`; `RN-04003`, `RN-04011`; `INV-02`; `US-04018`, `US-04019`, `US-04022` |
| Setup | Doble del puerto de validación con el resultado de **`E-7`** —6 piezas, los seis tipos—; fixture de trabajo `Finalizado` con comentario y `Rechazado` con comentario |
| Pasos | Given un trabajo propio con desenlace y comentario, When el alumno abre su detalle, Then recibe **piezas con su índice y sus componentes**, las observaciones, el desenlace y el comentario. Given el mismo trabajo, When el administrador lo abre, Then recibe **los mismos elementos**. Given un borrador ajeno pedido por el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given un trabajo ajeno pedido por un alumno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` |
| Salida esperada | Dos detalles equivalentes con las **seis** piezas de `E-7` y sus componentes, y dos negativas **distintas** según el papel, que es lo que separa el alcance del administrador de la pertenencia del alumno |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04023 — Aprobar-Y-Rechazar-Desde-Pendiente-Con-Comentario-Opcional

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04008`; `RN-04010`, `RN-04011`; `INV-07`; `US-04023`, `US-04024` |
| Setup | Fixture de trabajo en estado `Pendiente`; fixture de solicitante administrador; doble del reloj |
| Pasos | Given un trabajo en `Pendiente` y el administrador, When se lo aprueba **sin comentario**, Then alcanza `Finalizado`. Given otro, When se lo rechaza **con comentario**, Then alcanza `Rechazado` y el comentario queda con su fecha y su autor. Given un desenlace que no es ninguno de los dos, Then `DESENLACE_DESCONOCIDO` |
| Salida esperada | Dos estados terminales alcanzados, el comentario opcional en las dos formas, y un rechazo. El sello de desenlace sale del puerto de reloj |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04024 — Rechazo-De-Toda-Transicion-Sin-Facultad-O-Desde-Estado-Terminal

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04008`; `RN-04010`, `RN-04001`, `RN-04011`; `INV-07`; `US-04025` |
| Setup | Fixture de trabajo en los cuatro estados; fixture de solicitante alumno y administrador |
| Pasos | Given un trabajo en `Pendiente` y un solicitante alumno, When pide el desenlace, Then `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` **antes** de comprobar el estado. Given un trabajo en `Borrador` y el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`. Given un trabajo `Finalizado` o `Rechazado`, Then `TRANSICION_DESDE_ESTADO_TERMINAL` y **el contenido no cambia**. Given un trabajo que no está en `Pendiente` pero sí en el alcance, Then `DESENLACE_FUERA_DE_PENDIENTE`. Given un identificador que no existe, Then `TRABAJO_INEXISTENTE` |
| Salida esperada | Cinco rechazos, con **el orden verificado**: facultad antes que alcance y alcance antes que estado, que es lo que impide que un rechazo por facultad se lea como rechazo por terminalidad |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04025 — Eliminacion-Con-Sus-Dos-Alcances-Opuestos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-04009`; `RN-04004`, `RN-04011`, `RN-04003`; `INV-03`, `INV-02`; `US-04026`, `US-04027` |
| Setup | Fixture de trabajo en los cuatro estados, propio y ajeno; fixture de solicitante alumno y administrador |
| Pasos | Given un trabajo propio en `Borrador` y el alumno, When lo elimina, Then se retira. Given un trabajo propio fuera de `Borrador` y el alumno, Then `OPERACION_FUERA_DE_BORRADOR`. Given un trabajo ajeno y el alumno, Then `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`. Given los **tres** estados que el administrador ve, When los elimina, Then los tres se retiran. Given un `Borrador` y el administrador, Then `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` |
| Salida esperada | Cuatro retiros —uno del alumno y tres del administrador— y tres rechazos. Los dos alcances son **opuestos** y ninguno se filtra en el otro |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Pruebas de inspección estructural

#### TC-04026 — Cero-Pruebas-Que-Tocan-La-Base-De-Datos-Real

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de pruebas que tocan la base real (`05` §8); `QG-04`; `BT-04006` |
| Setup | Ninguno. Se inspecciona el propio proyecto de pruebas |
| Pasos | Given el proyecto de pruebas de esta capa, When se lo inspecciona, Then **0** pruebas abren un almacén real, **0** referencian una biblioteca de acceso a datos y **0** leen una cadena de conexión |
| Salida esperada | Tres recuentos en cero. Es la puerta propia y bloqueante que el intake §17.1.P.8 · GeometriaFactory-Application declara: una prueba que la incumpla **está mal ubicada** y pertenece a la batería de integración de `GeometriaFactory-Api` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04027 — Una-Sola-Dependencia-Saliente

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de dependencias salientes (`05` §8); `QG-05`; `BT-04001`, `BT-04004` |
| Setup | Ninguno. Se lee el archivo de proyecto |
| Pasos | Given el archivo de proyecto, When se lo inspecciona, Then declara exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web |
| Salida esperada | Un recuento en 1 y cuatro en 0. Es la propiedad que justifica el estilo entero y la que hace posible `TC-04026` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04028 — Catalogo-De-36-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de cobertura del catálogo (`05` §8); `QG-06`; las **36** condiciones de `03` §7.1 |
| Setup | El conjunto de códigos que la batería observó emitidos, y el catálogo de `03` §3 |
| Pasos | Given los dos conjuntos, When se los compara, Then **las 36 condiciones del catálogo están alcanzadas por al menos una prueba** y **ninguna condición emitida queda fuera del catálogo** |
| Salida esperada | 36 de 36 alcanzadas y 0 fuera. La comparación es **en las dos direcciones**: una sola dirección deja pasar el código inventado aguas abajo, que es lo que la Definition of Ready §1 criterio 6 prohíbe |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04029 — A-Lo-Sumo-Una-Unidad-De-Trabajo-Por-Caso-De-Uso

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de unidades de trabajo por caso de uso (`05` §8); `QG-08`; `RN-04007`; `US-04006` |
| Setup | Dobles de los repositorios instrumentados para contar aperturas de unidad de trabajo |
| Pasos | Given cada uno de los once casos de uso, When se lo ejerce, Then abre **a lo sumo una** unidad de trabajo. Given la baja de una cuenta con trabajos en los cuatro estados —caso testigo—, Then la cuenta y los cuatro trabajos se materializan en **la misma** |
| Salida esperada | Once recuentos en 0 o 1, y el caso testigo con **una** apertura y cinco materializaciones dentro de ella |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04030 — Cero-Componentes-Cargados-En-Las-Consultas-De-Listado

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de componentes de pieza en las consultas de listado (`05` §8); `QG-09`; `US-04019` |
| Setup | Doble del repositorio de trabajos que devuelve la proyección declarada |
| Pasos | Given el listado del alumno y el de la comisión, When se los resuelve, Then la colección de componentes **no viene materializada** en ninguna de las dos. Given el detalle, Then **sí** viene |
| Salida esperada | Dos listados sin componentes y un detalle con ellos. Es la decisión de modelado con efecto directo en el tiempo de respuesta del listado del administrador (intake §17.1.P.10 · GeometriaFactory-Application) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-04031 — Ninguna-Condicion-Prevista-Viaja-Como-Excepcion

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | `QG-11`; [`ADR-04006`](../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md); el quinto riesgo de `05` §9 |
| Setup | Los mismos dobles con los que se ejercen las 36 condiciones |
| Pasos | Given cada una de las **36** condiciones del catálogo, When se la provoca, Then el caso de uso **devuelve un valor** con su código y **no lanza**. Given la indisponibilidad de un puerto, Then tampoco lanza: devuelve `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | 36 rechazos como valor y **0** excepciones de negocio. Las excepciones quedan reservadas a defectos de programación del consumidor, que es lo que `ADR-04006` decidió |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 4.3 `GeometriaFactory-Infrastructure`

### 2.1 La batería del validador: los diez casos

#### TC-06001 — Ortoedro-Con-Clave-Sinonima

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **1** de la batería (`T1`); `CU-06001`; `RN-06009`; `US-06001`; paso `P-3` del flujo |
| Setup | Fixture con el **texto literal** del escenario `E-2` del intake §20 |
| Pasos | Given el texto de `E-2`, cuyo ortoedro declara sus bases con **la clave que el programa del alumno emite**, When se lo interpreta, Then las bases **se leen** y la pieza se reconstruye con **2 bases y 4 laterales**. Given el mismo texto con la clave equivalente, Then el resultado es **idéntico**: las dos se aceptan como sinónimas |
| Salida esperada | Una pieza reconstruida con sus seis componentes, por cualquiera de las dos claves. **Con un validador ingenuo, acá es donde falla** (§20.E-2, punto 3) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06002 — Texto-Con-Comas-Finales

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **2** de la batería (`T2`); `CU-06001`; `US-06001`; paso `P-2` |
| Setup | Fixture con el texto literal de `E-2`, **con sus dos comas finales** |
| Pasos | Given el texto tal como el programa lo emite, **con comas finales**, When se lo lee, Then **el parseo tiene éxito**. Given un texto con comentarios, Then también, por la misma tolerancia. Given un texto que no parsea **ni con tolerancia**, Then se emite una observación de validación y **no** `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | Dos lecturas exitosas y un rechazo con el código correcto. La distinción entre **texto ilegible** y **motor no disponible** es la que `TC-06013` desarrolla |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06003 — Cubo-Con-Caras-Cuadrado

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **3** de la batería (`T3`); `CU-06001`; `US-06001`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-3` |
| Pasos | Given el cubo de `E-3`, cuyas caras declaran el tipo que emite el primer ejemplo de la cátedra, When se lo interpreta, Then las caras **se interpretan** y el campo que se usa para dibujar es el largo |
| Salida esperada | Seis caras interpretadas, con la lectura del largo. Es la mitad de `T3` que viene del primer ejemplo; la otra mitad es `TC-06004` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06004 — Cubo-Con-Caras-Rectangulo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **4** de la batería (`T3`); `CU-06001`; `US-06001`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-4` |
| Pasos | Given el mismo cubo de lado 3, emitido por el **otro** ejemplo de la cátedra, cuyas caras declaran el otro tipo, When se lo interpreta, Then las caras **se interpretan igual que las de `TC-06003`**: las dos traen el largo, que es lo que se usa |
| Salida esperada | Resultado equivalente al de `TC-06003` en cuanto a la reconstrucción. **El contraste entre los dos ejemplos es lo que hace visible el defecto** (§20.E-4, contexto) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06005 — Area-Del-Cubo-Declarada-Contra-Derivada

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **5** de la batería; `CU-06002`; `RN-06009`; `US-06005`, `US-06007`; paso `P-6` |
| Setup | Fixture con el texto literal de `E-3`, ya interpretado por `TC-06003` |
| Pasos | Given el cubo de `E-3` con su área declarada **36.00**, When se deriva el área desde sus componentes, Then el valor derivado es **54.00** y se emite **una advertencia** que expresa **los dos valores**, no un texto genérico. Then el volumen declarado **27.00** coincide con el derivado y **no produce observación**. Then **el valor del alumno no se corrige** y el trabajo **no se rechaza** |
| Salida esperada | Una advertencia con su par de valores y una comparación sin observación. Es «el caso incómodo por excelencia»: el dato erróneo es un dato **correctamente emitido** por el programa del alumno |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06006 — Volumen-Del-Ortoedro-Declarado-Contra-Derivado

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **6** de la batería; `CU-06002`; `RN-06009`; `US-06005`, `US-06007`; paso `P-6` |
| Setup | Fixtures con los textos literales de `E-2` y de `E-1` |
| Pasos | Given el ortoedro de `E-2` con volumen declarado **343.00**, When se deriva, Then el derivado es **1029.00** y se emite **una advertencia**, **no un error**. Then su **área** derivada coincide con la declarada y **no** produce observación. Given el mismo ortoedro dentro de `E-1`, Then el resultado es el mismo |
| Salida esperada | Una advertencia de volumen y ninguna de área, en los dos escenarios. La advertencia **permite el paso a estado `Pendiente`**: esa decisión es del dominio y no de acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06007 — Dimension-En-Cero-Que-No-Descarta-La-Figura

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **7** de la batería; `CU-06001`, `CU-06002`; `US-06003`; pasos `P-4` y `P-6` |
| Setup | Fixture con el texto literal de `E-6` |
| Pasos | Given la figura de `E-6` con una dimensión en **0.00**, When se la interpreta, Then **se interpreta y no se descarta**: la comprobación es de **existencia del campo, no de veracidad de su valor**. Then se produce **a lo sumo una advertencia** por el valor derivado, y **nunca un error de interpretación** |
| Salida esperada | Una figura reconstruida y a lo sumo una advertencia. **Descartarla sería aplicar un juicio que ninguna regla pidió** y dejaría al alumno sin ver su propio error (§20.E-6, «Qué ejercita») |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06008 — Tipo-Desconocido-Con-Posicion-Y-Campo

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **8** de la batería; `CU-06001`; `RN-06009`; `US-06004`; paso `P-3` |
| Setup | Fixture con el texto literal de `E-5` |
| Pasos | Given el texto de `E-5`, When se lo interpreta, Then se produce una observación de severidad **`Error`**, no de advertencia, con **índice de figura 1** y **campo `Tipo`**, y **nunca un texto genérico**. Then **la primera pieza, que es válida, se interpreta igual**: un error en un elemento **no descarta el resto del análisis**. Given un elemento sin el campo de tipo, un conjunto raíz vacío y un texto que no parsea ni con tolerancia, Then los tres producen **el mismo tratamiento de error** |
| Salida esperada | Un error localizado, la pieza válida reconstruida y tres tratamientos equivalentes. Es el tramo principal de `RN-06009` |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06009 — Texto-Semilla-Completo

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **y medición de la tolerancia** |
| Cubre | Caso **9** de la batería; `CU-06001`, `CU-06002`; `US-06002`, `US-06006`; NFR de tolerancia; pasos `P-1` a `P-7` |
| Setup | Fixture con el texto literal de `E-1` |
| Pasos | Given el texto semilla de `E-1`, When se lo interpreta y se verifican sus valores, Then se reconstruyen **3 piezas** con índices 0, 1 y 2, y se emiten **exactamente 2 advertencias**. Then **el cilindro no produce ninguna observación**: su área declarada 113.10 contra la suma de componentes 113.09 da una diferencia de **exactamente 0.01**, y con el operador **estricto** eso **no** produce advertencia. Then **ninguna observación es de severidad `Error`** |
| Salida esperada | 3 piezas y **2** advertencias. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto**, y el caso de prueba canónico del producto falla. El intake §17.1.P.10 · GeometriaFactory-Infrastructure declara este número con su fundamento y **lo excluye expresamente de las asunciones** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06010 — Dimension-No-Legible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | Caso **10** de la batería; `CU-06001`; `RN-06009`; `US-06004`; paso `P-4` |
| Setup | Fixture con el texto literal de `E-8` |
| Pasos | Given el texto de `E-8`, cuya dimensión viene escrita con el separador decimal de la configuración regional del alumno y por eso **deja de ser un número**, When se lo interpreta, Then se produce un **error de validación** con **índice de figura** y **campo**, y **el código es el de dimensión no legible y no el de texto inválido**: el texto es sintácticamente válido y lo que falla es la lectura de un valor. Then la otra pieza **se interpreta igual** |
| Salida esperada | Un error localizado con el código correcto. **Confundir los dos códigos es el error que este escenario detecta** (§20.E-8, punto 3), y es el modo de falla **más probable** de todos los escenarios porque lo produce la máquina del alumno y no su programación |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.2 Cobertura adicional del validador

#### TC-06011 — Los-Seis-Tipos-Reconstruibles

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `US-06003`; **cobertura adicional declarada** de `05` §10.5 |
| Setup | Fixture con el texto literal de `E-7` |
| Pasos | Given el texto de `E-7`, When se lo interpreta, Then se reconstruyen **seis** piezas, una por cada tipo reconstruible, con las figuras planas **como piezas del conjunto raíz** y no sólo como componentes. Then el ortoedro se lee por su clave alternativa, igual que en `TC-06001` |
| Salida esperada | Seis piezas de seis tipos distintos. **`E-7` no respalda ninguno de los diez casos de la batería y se usa igual**, porque es el único texto que ejercita el mapeo completo: así lo declara `05` §10.5 |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06012 — La-Cantidad-De-Figuras-Del-Conjunto-Raiz-Y-La-Posicion-Reservada

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `RN-06009`; `US-06002`, `US-06003`; `RC-06002` |
| Setup | Fixtures con los textos literales de `E-5` y de `E-1` |
| Pasos | Given el texto de `E-5`, donde una figura **no se reconstruye**, When se lo interpreta, Then la **cantidad de figuras del conjunto raíz** que se devuelve **incluye la no reconstruida**, y **la posición de esa figura queda reservada**: la siguiente pieza **no se renumera**. Given `E-1`, Then la cantidad coincide con las tres piezas reconstruidas. Given un conjunto en el que la cantidad y las posiciones no cierran, Then `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` |
| Salida esperada | La cantidad que incluye lo no reconstruido, la posición reservada y un rechazo. Es lo que hace que el índice de una observación tenga un rango contra el que validarse |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06013 — Texto-Ilegible-No-Es-Motor-No-Disponible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06001`; `US-06001`; el segundo riesgo de `05` §9 |
| Setup | Un texto que no parsea ni con tolerancia; el texto ausente; y el motor forzado a no estar disponible |
| Pasos | Given un texto ilegible, When se lo interpreta, Then se emite **una observación de validación** y **no** `INTERPRETACION_NO_DISPONIBLE`. Given el texto ausente, Then `TEXTO_ORIGINAL_AUSENTE`. Given el motor efectivamente no disponible, Then **sí** `INTERPRETACION_NO_DISPONIBLE` |
| Salida esperada | Tres resultados distintos y **ninguna confusión entre resultado y fallo**. `05` §9 le asigna probabilidad **alta**: es la garantía que más veces se rompe al implementar, porque el alumno vería «el servicio no está disponible» y esperaría a que se recupere de un problema que no tiene |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06014 — Cero-Peticiones-De-Red-De-Los-Dos-Motores

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de peticiones de red (`05` §8); `QG-08`; `CU-06001` CA-11 |
| Setup | El árbol de dependencias de los dos motores |
| Pasos | Given los dos motores, When se inspeccionan sus dependencias y se ejecuta la batería completa con el acceso a red observado, Then el recuento de peticiones originadas por ellos es exactamente **0**. Then **ninguno abre el almacén**: reciben texto y devuelven observaciones |
| Salida esperada | Dos recuentos en cero. Es lo que el intake §17.1.P.3 · GeometriaFactory-Infrastructure declara —«el validador de figuras no hace red»— y lo que hace que la interpretación se pueda medir **sin almacén** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06015 — Tiempo-De-Interpretacion-Del-Texto-Semilla

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **medición** |
| Cubre | NFR de tiempo de interpretación (`05` §8); `QG-14`; `US-06001` |
| Setup | Fixture con el texto literal de `E-1`; medición **sin almacén** |
| Pasos | Given el texto de **3** piezas de `E-1`, When se lo interpreta y se verifican sus valores, Then el tiempo total es menor a **200 ms**, medido **sin abrir el almacén**, que es la condición que el intake §17.1.P.10 · GeometriaFactory-Infrastructure declara |
| Salida esperada | Una medición registrada. El umbral viene rotulado **[ASUNCIÓN del intake §22, asunción `A-5`]** y su gate es **condicionado**: se mide y se registra, y no bloquea hasta la confirmación del Product Owner |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.3 Almacén: trabajos y cuentas

#### TC-06016 — Texto-Original-Literal-Y-Escritura-Que-Lo-Reemplaza-Rechazada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `RN-06008`; `RC-06001`; `US-06008`; NFR de escrituras que reemplazan el texto |
| Setup | Almacén efímero preparado; fixture con el texto literal de `E-2`, **con sus comas finales** |
| Pasos | Given el texto de `E-2`, When se materializa el trabajo y se lo recupera, Then el texto guardado es **idéntico carácter por carácter** al original, con sus comas finales intactas. When se intenta materializar el **mismo trabajo** con un texto distinto, Then se rechaza con `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` y **el texto guardado no cambia** |
| Salida esperada | Comparación byte a byte sin diferencias y un rechazo sin efecto. Es el **tramo principal de `RN-06008`**: ésta es la capa donde el texto se escribe, y por lo tanto donde puede perderse |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06017 — Materializacion-Del-Trabajo-En-Una-Unidad-De-Trabajo

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `US-06009` |
| Setup | Almacén efímero; fixture de trabajo con piezas, componentes y observaciones |
| Pasos | Given un trabajo con sus piezas, sus componentes y sus observaciones, When se lo materializa, Then las cuatro cosas quedan en **una sola** unidad de trabajo. When el almacén se interrumpe a mitad, Then **no queda nada escrito**. Given una escritura concurrente sobre el mismo trabajo, Then `ESCRITURA_CONCURRENTE_RECHAZADA`. Given el almacén no disponible, Then `ALMACEN_NO_DISPONIBLE` |
| Salida esperada | Una materialización completa, una interrupción sin efecto parcial y dos rechazos con su código |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06018 — Consulta-Con-El-Recorte-Ya-Trasladado-Y-Sin-Recorte-No-Resuelta

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `RN-06003`, `RN-06011`; `US-06010` |
| Setup | Almacén efímero con trabajos de dos alumnos en los cuatro estados |
| Pasos | Given una consulta con el recorte por dueño **ya declarado**, When se la resuelve, Then devuelve sólo lo que ese recorte admite. Given una consulta con el predicado de alcance del administrador declarado, Then **ningún borrador viaja**. Given una consulta **sin recorte declarado**, Then `CONSULTA_SIN_ALCANCE_DECLARADO` y **no se resuelve** |
| Salida esperada | Dos consultas acotadas y un rechazo. **Esta capa no comprueba pertenencia**: lo que hace es **no ofrecer el camino** por el que `RN-06003` y `RN-06011` se romperían |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06019 — Listado-Sin-Componentes-Y-Sin-Texto-Original

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06003`; `US-06011`; NFR de componentes en listados; `QG-10` |
| Setup | Almacén efímero con un trabajo con seis piezas y sus componentes, materializado desde `E-7` |
| Pasos | Given la **proyección de listado**, When se la resuelve, Then la colección de componentes **no viene materializada** y el **texto original no aparece** en el resultado. Given el **detalle**, Then las dos cosas **sí** vienen |
| Salida esperada | Dos recuentos en cero para el listado y presencia completa en el detalle. `05` §9 le asigna probabilidad **media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06020 — Retiro-Fisico-Todo-O-Nada

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06004`; `RN-06004`; `RC-06005`; `US-06012` |
| Setup | Almacén efímero con un trabajo con piezas, componentes y observaciones |
| Pasos | Given un trabajo con todo lo que cuelga de él, When se lo retira, Then **se retira físicamente**, sin marca lógica, con sus piezas, sus componentes y sus observaciones. When se consulta después, Then no existe. Given un retiro que sólo alcanzaría a parte de lo que cuelga, Then `RETIRO_PARCIAL_NO_ADMITIDO` |
| Salida esperada | Un retiro completo y un rechazo. **No hay borrado lógico**: es la única operación destructiva del producto y se ejerce entera |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06021 — Arrastre-De-La-Baja-Interrumpido-Sin-Retiro-Parcial

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna, **con el almacén interrumpido a mitad de operación** |
| Cubre | `CU-06004`; `RN-06007`; `RC-06005`; `US-06013`; NFR de retiros parciales; `QG-11` |
| Setup | Almacén efímero con una cuenta y **cuatro** trabajos suyos en los cuatro estados |
| Pasos | Given la cuenta con sus cuatro trabajos, When se ejecuta el arrastre de la baja, Then **la cuenta y los cuatro trabajos** se retiran en la misma unidad. When el almacén **se interrumpe a mitad de la operación**, Then **no se retira nada**: la cuenta sigue y sus cuatro trabajos también |
| Salida esperada | Un arrastre completo y una interrupción con **0** retiros parciales. Es el mecanismo de medición que `05` §8 declara para ese NFR |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06022 — Unicidad-Del-Correo-Y-Del-Administrador-En-El-Almacen

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06001`, `RN-06002`; `INV-01`, `INV-05`; `US-06014` |
| Setup | Almacén efímero con una cuenta de alumno y una de administrador |
| Pasos | Given una cuenta con un correo ya registrado, When se materializa otra con el mismo correo, Then el almacén la rechaza con `CORREO_YA_REGISTRADO`, **aunque la consulta previa del consumidor no lo hubiera visto**. Given una segunda cuenta con papel de administrador, Then `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` |
| Salida esperada | Dos rechazos del almacén. Es la **segunda línea deliberada**: la consulta previa del consumidor **no es una garantía por sí sola**, y la capa de aplicación ya declara este camino como flujo alternativo propio |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06023 — Las-Dos-Preguntas-Sobre-El-Conjunto

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06001`, `RN-06002`; `US-06015` |
| Setup | Almacén efímero, en dos estados: con administrador y sin él |
| Pasos | Given un correo, When se pregunta si está registrado, Then la respuesta es un sí o un no y **no una cuenta**. Given el almacén sin administrador, When se pregunta si existe uno, Then no; con administrador, Then sí. Then **ninguna de las dos respuestas revela el estado ni el papel de la cuenta que ocupa un correo** |
| Salida esperada | Cuatro respuestas correctas y ninguna filtración. Son las dos preguntas que **ninguna entidad sola responde**, y por eso viven en el repositorio |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06024 — La-Marca-Viaja-Y-No-Altera-El-Estado-De-La-Cuenta

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06005`; `RN-06012`, `RN-06013`, `RN-06015`, `RN-06016`; `RC-06007`; `US-06016` |
| Setup | Almacén efímero con cuentas en los **tres** estados, cada una con trabajos |
| Pasos | Given las tres cuentas, When se escribe la marca de cambio de contraseña pendiente en cada una, Then **el estado de la cuenta no cambia** en ninguna, **ningún trabajo se pierde ni cambia de estado**, y **la marca viaja** al recuperarla. Then **la marca no es un estado de cuenta**: no ocupa su lugar ni lo reemplaza (`RC-06007`) |
| Salida esperada | Tres escrituras de marca sobre tres estados distintos, con el estado y los trabajos intactos. **La comprobación de qué habilita la marca no es de acá**: acá se conserva y se transporta |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.4 Mecanismos de seguridad

#### TC-06025 — Derivacion-Sin-Guardar-Ni-Registrar-En-Claro

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06006`; `US-06017` |
| Setup | Una contraseña en claro evidentemente ficticia, declarada como tal |
| Pasos | Given una contraseña en claro, When se la deriva, Then se devuelve el valor derivado **con sus parámetros versionados junto a él**. Then **la contraseña en claro no queda escrita en ninguna parte** —ni en el almacén, ni en el registro del servidor, ni en el mensaje de ninguna condición—. Given la contraseña en claro ausente, Then `CONTRASENA_EN_CLARO_AUSENTE` |
| Salida esperada | Un valor derivado con sus parámetros y **0** apariciones del valor en claro en los tres lugares. Es el **único punto del producto donde la contraseña en claro se convierte en el valor guardado** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06026 — Verificacion-Que-Distingue-El-Derivado-Ilegible

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06006`; `US-06018` |
| Setup | Un valor derivado válido, uno con parámetros que no se pueden leer, y dos contraseñas en claro |
| Pasos | Given la contraseña correcta y su valor derivado, When se verifica, Then el veredicto es afirmativo. Given una contraseña distinta, Then negativo. Given un valor derivado **ilegible** —parámetros ausentes o no interpretables—, Then `CREDENCIAL_DERIVADA_ILEGIBLE`, **que no es lo mismo que una contraseña equivocada** |
| Salida esperada | Dos veredictos y un rechazo distinguible. Confundir el derivado ilegible con la contraseña equivocada haría que un dato corrupto se leyera como intento fallido |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06027 — Provisoria-No-Adivinable-Y-Sin-Repetirse

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06007`; **`RN-06014`, su tramo principal y único**; `RN-06016`; `US-06019`; NFR de provisorias repetidas; `QG-09` |
| Setup | La fuente de material impredecible disponible; dos cuentas distintas |
| Pasos | Given la misma cuenta, When se producen **dos** provisorias consecutivas, Then **son distintas**. Given dos cuentas distintas, Then también. Then **ninguna es derivable del nombre, del correo ni de la fecha**. Then la invocación **no lleva ningún dato del acto que la motiva**, de modo que la de la **habilitación** y la del **reseteo** son el mismo mecanismo y no se pueden distinguir |
| Salida esperada | **0** provisorias iguales y **0** derivables de un dato conocido. Es la delegación explícita que las tres capas de arriba le hacen a ésta: `RN-06014` es la única de las dieciséis reglas cuyo tramo principal **y único** vive acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06028 — Sin-Aleatoriedad-No-Se-Produce-Valor

| Campo | Valor |
| --- | --- |
| Tipo | Unit, con la fuente de material impredecible doblada |
| Cubre | `CU-06007`; `RN-06014`; `US-06020`; el tercer riesgo de `05` §9 |
| Setup | La fuente de material impredecible **que no responde** |
| Pasos | Given la fuente que no responde, When se pide una provisoria, Then se devuelve `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y **no se produce ningún valor**. Then **no se compone una provisoria por otro medio**: ni un contador, ni la fecha, ni el correo, ni el nombre |
| Salida esperada | Un rechazo y **cero** valores producidos por un atajo. `05` §9 lo declara de impacto **muy alto** con un fundamento que conviene repetir: **un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06029 — Acceso-Firmado-Con-Sus-Cuatro-Reclamos

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06008`; `RN-06001`; `US-06021` |
| Setup | Una clave de firma evidentemente ficticia, provista por configuración de prueba |
| Pasos | Given los **cuatro** reclamos y la clave, When se emite el acceso, Then lleva los cuatro y **la firma verifica**. When se lo verifica con una clave distinta, Then el veredicto es negativo. Given reclamos incompletos, Then `RECLAMOS_INCOMPLETOS` y **no se emite**. Then el acceso **transporta el papel sin decidir qué habilita** |
| Salida esperada | Una emisión con sus cuatro reclamos, dos veredictos y un rechazo. La decisión de qué habilita el papel **no es de acá** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06030 — Sin-Clave-De-Firma-No-Hay-Emision

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06008`; `US-06022`; `QG-12`; el cuarto riesgo de `05` §9 |
| Setup | La configuración de prueba **sin** clave de firma |
| Pasos | Given la ausencia de clave, When se pide emitir un acceso, Then `CLAVE_DE_FIRMA_AUSENTE` y **no se emite ninguno**. Then **no se genera una clave al vuelo** y **no se emite sin firmar** |
| Salida esperada | Un rechazo y **cero** accesos emitidos por cualquiera de los dos atajos. `05` §9 lo declara de impacto muy alto: con cualquiera de ellos **el sistema arranca, emite accesos y nadie lo nota hasta que alguien falsifica uno** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06031 — El-Sello-Del-Reloj-Entra-Por-Puerto

| Campo | Valor |
| --- | --- |
| Tipo | Unit |
| Cubre | `CU-06009`; `US-06023` |
| Setup | Ninguno |
| Pasos | Given el adaptador de reloj, When se le pide el momento, Then devuelve el momento actual del sistema. Then **es el contrato más corto de la capa**, y es lo que permite que las capas de arriba fijen el momento en sus pruebas sin tocar el reloj del entorno |
| Salida esperada | Un momento devuelto, y dos invocaciones consecutivas que **no son necesariamente iguales**. La reproducibilidad de los sellos es de quien lo consume, no de acá |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.5 Arranque y preparación del almacén

#### TC-06032 — Transformaciones-Aplicadas-Sobre-Almacen-Inexistente

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06010`; `US-06024`; NFR de aplicación de transformaciones; `QG-04` |
| Setup | Un almacén **inexistente**, en una ubicación recibida por configuración de prueba |
| Pasos | Given un almacén que no existe, When arranca la preparación, Then el almacén **se crea**, las transformaciones **se aplican solas** y **ningún paso manual hace falta**. When se vuelve a arrancar sobre el almacén ya preparado, Then **no se aplica nada dos veces** y el linaje queda registrado |
| Salida esperada | **1 de 1** aplicación exitosa sobre almacén inexistente y una segunda ejecución idempotente. Es **criterio de aceptación de la etapa `c`** y etapa propia del pipeline (intake §17.1.P.8 · GeometriaFactory-Infrastructure) |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06033 — Arranque-Detenido-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso

| Campo | Valor |
| --- | --- |
| Tipo | Integración interna |
| Cubre | `CU-06010`; `US-06025`; el quinto y el sexto riesgo de `05` §9 |
| Setup | Tres almacenes: uno con un esquema que **no corresponde** al linaje esperado; una ubicación **no disponible**; y uno correcto |
| Pasos | Given un almacén cuyo esquema no corresponde, When arranca la preparación, Then `MIGRACION_NO_APLICABLE` y **el arranque se detiene**. Then **el almacén no se descarta y no se recrea**. Given una ubicación no disponible, Then `RUTA_DEL_ALMACEN_NO_DISPONIBLE` y el arranque **se detiene**; Then **no se cae hacia una ruta alternativa dentro de la imagen**. Given el almacén correcto, Then el arranque procede |
| Salida esperada | Dos detenciones y un arranque. La primera evita «el atajo más destructivo del producto» —dejar el servicio impecable y **sin los trabajos de nadie**—; la segunda evita que el servicio acepte trabajos de la comisión entera y **los pierda en el siguiente reemplazo de versión** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

### 2.6 Pruebas de inspección estructural

#### TC-06034 — Catalogo-De-17-Condiciones-En-Las-Dos-Direcciones

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | NFR de cobertura del catálogo (`05` §8); `QG-13`; las **17** condiciones de `03` §3 |
| Setup | El conjunto de códigos que la batería observó emitidos, y el catálogo de `03` §3 |
| Pasos | Given los dos conjuntos, When se los compara, Then **las 17 condiciones están alcanzadas por al menos una prueba** y **ninguna condición emitida queda fuera del catálogo** |
| Salida esperada | 17 de 17 alcanzadas y 0 fuera. La comparación es **en las dos direcciones** |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

#### TC-06035 — Ningun-Mensaje-Ni-Traza-Con-Un-Secreto-La-Ruta-Del-Almacen-O-El-Texto-Del-Alumno

| Campo | Valor |
| --- | --- |
| Tipo | Unit, **prueba de inspección** |
| Cubre | `RA-03`; NFR de mensajes y trazas (`05` §8); `QG-13` |
| Setup | Las 17 condiciones provocadas una por una, y el registro del servidor de esa ejecución |
| Pasos | Given cada una de las 17 condiciones, When se la provoca, Then su mensaje **no contiene** la clave de firma, ninguna contraseña en claro, la ruta del almacén ni el texto del alumno. Given el registro del servidor de la misma ejecución, Then **tampoco**. Then **todo error que se muestre queda registrado del lado del servidor**, que es la contracara de `RA-03` |
| Salida esperada | **0** apariciones en las dos direcciones —mensajes y registro— y **17 de 17** errores registrados del lado del servidor |
| Salida observada | Sin ejecutar |
| Estado | `Pendiente` |

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.1 §4). Pasa de ser el documento del proyecto de código `GeometriaFactory-Api` a ser el de la **unidad de entrega**, absorbiendo los homónimos de `GeometriaFactory-Domain`, `-Application` e `-Infrastructure`. Cada sección lleva **una subsección por proyecto de código**, con su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con los cuatro juntos. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
