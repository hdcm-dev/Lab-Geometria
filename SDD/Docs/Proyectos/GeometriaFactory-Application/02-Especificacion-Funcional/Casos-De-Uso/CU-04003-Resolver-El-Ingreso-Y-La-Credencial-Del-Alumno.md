# CU-04003 — Resolver el ingreso y la credencial del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §5 (explicación al alumno no habilitado, custodia de la credencial vigente, alta de punta a punta); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13**, §4 (**F-04** precisada, F-05, F-03, F-26), §4.1 (RN-04006, **RN-04013 precisada**, RN-04014, **RN-04016**), §6 (flujo 1), §17.1.P.2 (INV-09), §17.2.P.5; orquesta [`CU-02004`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) y [`CU-02003`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) de GeometriaFactory-Domain
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

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
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Resolver, sobre una cuenta concreta, si admite el ingreso al laboratorio y con qué motivo si no lo admite, y sostener las dos operaciones sobre la credencial derivada: **fijarla, que desde `PRODUCT-INTAKE` 1.13 ocurre dentro de la habilitación de CU-04002 y no a pedido del alumno**, y reemplazarla después exigiendo la vigente. Es lo que hace posible la promesa del producto de que **ninguna credencial se transporta**, en un laboratorio sin canal de correo.

Este caso de uso **no emite el acceso ni deriva la contraseña**: recibe el valor ya derivado y devuelve la admisibilidad con su motivo. Los dos mecanismos pertenecen a las capas externas.

Desde el `PRODUCT-INTAKE` **1.7** sostiene además el **cambio forzado**: el reemplazo que hace una cuenta con la contraseña reseteada por CU-04011, que es **lo único que levanta la marca de cambio de contraseña pendiente** (RN-04013, INV-09). Es el mismo reemplazo de siempre; lo que cambia es de dónde viene y qué deja atrás.

**Desde el `PRODUCT-INTAKE` 1.13 ese cambio forzado es también el primer ingreso del alumno.** **RN-04016** hace que habilitar produzca una contraseña provisoria y ponga la misma marca, de modo que la cuenta recién habilitada llega al reemplazo por el mismo camino que la reseteada. La consecuencia sobre este caso de uso es doble: **la fijación deja de tener un solicitante anónimo** —la ejerce CU-04002 dentro de la habilitación— y **el motivo `CREDENCIAL_NO_ESTABLECIDA` queda retirado**, porque su causa dejó de ser posible.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Consulta la admisibilidad antes de resolver un ingreso, o solicita fijar o reemplazar la credencial derivada |
| Puerto de repositorio de cuentas | Sistema | Recupera la cuenta por su correo y materializa la credencial resultante |
| Puerto de reloj del sistema | Sistema | Provee el sello de modificación de la cuenta, que es un metadato de orquestación de esta capa |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Evalúa la admisibilidad y admite o rechaza la operación sobre la credencial |

El alumno es el sujeto de la regla.

## 3. Precondiciones

- El consumidor aporta el correo de la cuenta.
- Para fijar o reemplazar, el consumidor aporta el valor de credencial **ya derivado** y, en el reemplazo, la declaración de que verificó la credencial vigente.

## 4. Flujo principal

1. El consumidor consulta la admisibilidad de la cuenta de un correo.
2. El caso de uso recupera la cuenta por el puerto de repositorio de cuentas.
3. El caso de uso invoca la evaluación de admisibilidad en el dominio.
4. La cuenta está en estado `Habilitado`, tiene credencial derivada y no tiene la marca puesta: el dominio devuelve admisible.
5. El caso de uso devuelve admisible junto con la identidad y el papel de la cuenta, que es lo que el consumidor necesita para resolver el ingreso. **La cuenta con la marca puesta no llega hasta acá**: el dominio la devuelve no admisible en el paso 3, con el motivo de §6 (FA-06, §10).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta fue **recién habilitada** y todavía no cambió la provisoria que la habilitación produjo | El dominio devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`, **el mismo** que devuelve para una cuenta con la contraseña reseteada. El caso de uso lo devuelve tal cual, y el consumidor lo traduce en el pedido de cambiar la contraseña: es el primer ingreso del flujo de alta, que desde `PRODUCT-INTAKE` 1.13 recorre el camino de RN-04013 | Termina el caso de uso |
| FA-02 | **CU-04002 solicita fijar** la credencial derivada provisoria dentro de la habilitación de una cuenta | El caso de uso toma el sello de modificación del puerto de reloj, invoca la fijación en el dominio y materializa la cuenta, con la marca puesta. Devuelve que procede. **El solicitante es CU-04002 y no el alumno**: no hay ningún camino por el que una petición sin credencial llegue a fijar una contraseña (RN-04016) | Paso 5 |
| FA-03 | El consumidor solicita **reemplazar** la credencial derivada declarando que verificó la vigente | El caso de uso toma el sello de modificación del puerto de reloj, invoca el reemplazo en el dominio y materializa la cuenta | Paso 5 |
| FA-04 | El consumidor solicita el reemplazo sin declarar la verificación de la credencial vigente | El caso de uso propaga el rechazo del dominio con el motivo `CREDENCIAL_VIGENTE_NO_VERIFICADA` y no toca la cuenta | Termina el caso de uso |
| FA-05 | El consumidor solicita el reemplazo sobre una cuenta **marcada como con cambio de contraseña pendiente**, declarando verificada la vigente —que es la provisoria que produjo **la habilitación de CU-04002 o el reseteo de CU-04011**— | Es el **cambio forzado**, y desde `PRODUCT-INTAKE` 1.13 es también **el primer ingreso**. El caso de uso ejerce el reemplazo de FA-03 y además **levanta la marca** en la misma unidad de trabajo. La contraseña nueva la elige el alumno y el administrador no la conoce (RN-04013). **Los dos orígenes de la marca recorren este mismo flujo alternativo**, sin ningún dato que los distinga | Paso 5 |
| FA-06 | Una cuenta marcada pide cualquier otra cosa que no sea su propio cambio de contraseña, incluida la consulta de admisibilidad con la que se resuelve un ingreso | El caso de uso propaga el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` del dominio, sin leer ni escribir nada (INV-09). En la consulta es **no admisible**, de modo que el consumidor **no emite acceso**; en las demás operaciones es no procede. **La única excepción declarada de la guardia transversal del índice maestro §4** es el reemplazo de FA-05, que sí procede porque es lo que la levanta | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CUENTA_PENDIENTE` | El estado de cuenta es `Pendiente` | Devuelve no admisible con este motivo, para que la persona sepa que todavía no fue habilitada y no reciba un rechazo genérico (RN-04006) |
| `CUENTA_BLOQUEADA` | El estado de cuenta es `Bloqueado` | Devuelve no admisible con este motivo |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | Se intenta fijar o reemplazar sobre una cuenta `Pendiente` o `Bloqueado` | Propaga el rechazo del dominio y conserva la credencial como estaba |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Reemplazo sin la declaración de verificación | Propaga el rechazo del dominio |
| `CREDENCIAL_YA_FIJADA` | Se pide fijar por primera vez una credencial que ya tiene valor | Propaga el rechazo del dominio: el camino correcto es el reemplazo de FA-03 |
| `VALOR_DERIVADO_VACIO` | El valor de credencial aportado está vacío | Propaga el rechazo del dominio y conserva la credencial como estaba |
| `CUENTA_INEXISTENTE` | El puerto de repositorio no encuentra el correo | Devuelve no admisible sin distinguir el motivo hacia afuera, para no revelar qué correos están registrados |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | La cuenta está marcada **por la habilitación de CU-04002 o por un reseteo de CU-04011** y pide algo que no es su propio cambio de contraseña | No lee ni escribe nada. En la consulta de admisibilidad es **no admisible**, y **no es un rechazo**: es la situación esperada **del primer ingreso y del reseteo por igual**, y el consumidor la traduce en el pedido de cambiar la provisoria, que es lo único que la cuenta puede hacer (RN-04013, RN-04016, INV-09) |

**Motivo retirado en la versión 1.3.** `CREDENCIAL_NO_ESTABLECIDA` queda **retirado** y no figura entre los motivos vivos de este caso de uso: su causa —cuenta `Habilitado` sin credencial derivada— dejó de ser posible con **RN-04016**. **El identificador no se recicla.** Quien busque el encaminamiento del primer ingreso encuentra `CAMBIO_DE_CONTRASENA_PENDIENTE`.

## 7. Postcondiciones

- **Éxito, consulta:** el resultado es admisible con la identidad y el papel, o no admisible con exactamente un motivo —y `CAMBIO_DE_CONTRASENA_PENDIENTE` es uno de ellos—. Ninguna cuenta cambia.
- **Éxito, credencial:** la cuenta tiene la credencial derivada nueva y su sello de modificación es el del reloj.
- **Éxito, cambio forzado:** además de lo anterior, **la marca queda levantada** y la consulta siguiente devuelve admisible, de modo que la cuenta vuelve a obtener acceso y opera con normalidad. Es la única postcondición del producto que levanta la marca.
- **Fallo:** la cuenta queda exactamente como estaba, marca incluida.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta `Pendiente` de `ana.perez@ejemplo.edu` | El consumidor consulta la admisibilidad de ese correo | El caso de uso devuelve no admisible con el motivo `CUENTA_PENDIENTE` |
| CA-02 | Una cuenta **recién habilitada** de `ana.perez@ejemplo.edu`, con su provisoria y la marca puesta | El consumidor consulta la admisibilidad | El caso de uso devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` —**el mismo** que CA-06—, y **0 cuentas** `Habilitado` sin credencial derivada son alcanzables |
| CA-03 | Una cuenta `Pendiente` y un reloj fijado en 2026-03-20 | **CU-04002 habilita la cuenta** y solicita fijar la credencial derivada provisoria con un valor no vacío | El caso de uso devuelve que procede y la cuenta queda `Habilitado`, con credencial derivada, **marca puesta** y sello de modificación 2026-03-20 |
| CA-04 | Una cuenta en estado `Habilitado` con credencial derivada | El consumidor solicita reemplazarla sin declarar que verificó la vigente | El caso de uso devuelve el motivo `CREDENCIAL_VIGENTE_NO_VERIFICADA` y la credencial derivada no cambia |
| CA-05 | Un repositorio sin ninguna cuenta con el correo `nadie@ejemplo.edu` | El consumidor consulta la admisibilidad de ese correo | El caso de uso devuelve no admisible sin declarar si el correo existe |
| CA-06 | Una cuenta `Habilitado` reseteada por CU-04011, con la marca puesta | El consumidor consulta la admisibilidad | El caso de uso devuelve **no admisible** con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`, con 1 motivo y 0 cambios sobre la cuenta |
| CA-07 | La misma cuenta | El consumidor solicita el reemplazo declarando verificada la credencial vigente | El caso de uso devuelve que procede, la credencial queda reemplazada, **la marca queda levantada** y la consulta siguiente devuelve **admisible** con 0 motivos |
| CA-08 | Una cuenta recién habilitada y una cuenta con la contraseña reseteada, las dos con la marca puesta | El consumidor solicita el reemplazo sobre cada una, declarando verificada la vigente | Las 2 recorren **el mismo** FA-05 y terminan admisibles: **0 caminos** de esta capa fijan una contraseña sin credencial vigente verificada o sin un solicitante autenticado |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00002 |
| Reglas de negocio aplicables | [RN-02006](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-02001](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), y [**RN-02013**](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), que ya tiene archivo propio en `GeometriaFactory-Domain` |
| Casos de uso de dominio orquestados | [CU-02004](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md), [CU-02003](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-04007, US-04008, US-04009, US-04032 |
| Componentes esperados en 05 | Caso de uso de admisibilidad y caso de uso de credencial, con su resultado tipado y su enumeración cerrada de motivos |
| Tests previstos en 08 | Unitarias con dobles sobre los tres estados de cuenta, la cuenta habilitada sin credencial, la fijación, el reemplazo con y sin verificación, el correo inexistente, la admisibilidad que devuelve la marca puesta y el cambio forzado que la levanta |

## 10. Notas y supuestos

- **La derivación de la contraseña no es de esta capa** y el valor en claro nunca la atraviesa: llega derivado desde afuera, tal como el dominio lo exige.
- **La emisión del acceso tampoco es de esta capa.** Acá se resuelve si la cuenta lo admite y por qué; quién lo emite y con qué mecanismo es materia de las capas externas.
- La comparación de la credencial vigente la ejerce quien sabe derivarla; este caso de uso exige que esa verificación se declare, que es la forma en que el dominio la hace exigible sin conocerla.
- El motivo de una cuenta inexistente no se distingue hacia afuera, por el mismo criterio con el que un trabajo ajeno es indistinguible de uno inexistente.
- **El sello de modificación es un metadato de orquestación** que esta capa aporta al materializar la cuenta. El modelo del dominio declara la fecha de alta del alumno, que recibe del consumidor, y **no declara una fecha de última modificación**: mientras el Product Owner no resuelva incorporarla al modelo, este sello se lee como dato de esta capa y no como atributo del dominio. Tampoco se confunde con la «Fecha» que el alumno declara en su trabajo.
- **Esta cuenta puede no pasar nunca por la fijación por primera vez.** La del administrador nace con credencial fijada por CU-04010, de modo que para ella el único camino es el reemplazo.
- **No queda ningún camino de esta capa que fije una contraseña sin identidad.** La **fijación** de FA-02 la solicita CU-04002 dentro de la habilitación, que es una operación del administrador autenticado; el **reemplazo** de FA-03 y FA-05 exige la declaración de credencial vigente verificada. Es el enunciado de **RN-04016** visto desde la orquestación, y lo que verifica CA-08.
- **Una cuenta con la contraseña reseteada es no admisible, y el motivo es el camino.** El `PRODUCT-INTAKE` **1.8** precisa RN-04013: la cuenta con contraseña provisoria **se autentica pero no obtiene sesión de trabajo**, y el sistema la deriva al cambio. Esta capa lo materializa devolviendo no admisible con `CAMBIO_DE_CONTRASENA_PENDIENTE`, que no es un rechazo sino un encaminamiento —exactamente como lo era `CREDENCIAL_NO_ESTABLECIDA` antes de su retiro— y que deja al alumno un camino para levantar la marca: el reemplazo de FA-05, que **no** exige admisibilidad previa. Es lo que `GeometriaFactory-Domain` CU-04004 FA-03 declara, y lo contrario —devolverla admisible con una marca— emitiría acceso a una cuenta que por INV-09 no ejerce ninguna capacidad.
- **La marca la ponen la habilitación de CU-04002 y el reseteo de CU-04011, y la levanta únicamente el cambio efectivo de FA-05**, hecho por la propia cuenta (INV-09, **RN-04016**). El bloqueo de CU-04002 no la toca y conserva la marca tal cual estaba. Hasta `PRODUCT-INTAKE` 1.12 la ponía sólo CU-04011, y esta nota decía que ningún flujo de CU-04002 la tocaba: dejó de ser cierto para dos de sus cuatro operaciones.
- **La declaración de verificación de la credencial vigente sigue siendo obligatoria en el cambio forzado.** Lo que el alumno presenta como vigente es la provisoria que el administrador le comunicó; el mecanismo de comparación no cambia y sigue viviendo fuera de esta capa.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **4** ocurrencias a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-06**: la fecha de modificación de la cuenta pasa a llamarse **sello de modificación** y se declara metadato de orquestación de esta capa en §2 y en §10, porque el modelo del dominio no declara ese atributo y la discrepancia está elevada al Product Owner. **H-14**: §6 suma `CREDENCIAL_YA_FIJADA` y `VALOR_DERIVADO_VACIO`, propagados del dominio y hasta ahora sin camino de vuelta declarado. **H-01, propagado**: §10 declara que la cuenta del administrador nace con credencial fijada por CU-04010 y por lo tanto sólo recorre el camino de reemplazo. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas **RN-04012** y **RN-04013** y el invariante **INV-09** de §17.1.P.2. **§1**: declara que este caso de uso sostiene además el **cambio forzado**, que es lo único que levanta la marca de cambio de contraseña pendiente. **§4 paso 5**: el resultado de la consulta de admisibilidad pasa a llevar también **la marca**, junto con la identidad y el papel. **§5**: **FA-05** nueva, el cambio forzado que reemplaza y levanta la marca en la misma unidad de trabajo, y **FA-06** nueva, la cuenta marcada que pide cualquier otra cosa. **§6**: suma `CAMBIO_DE_CONTRASENA_PENDIENTE`, con la precisión de que **no es una negativa de admisibilidad**. **§7**: la postcondición de la consulta incluye la marca y se agrega la del cambio forzado. **§8**: CA-06 y CA-07 nuevas. **§9**: RN-04013 referenciada contra el intake, US-04032 y los dos tests nuevos. **§10**: cuatro notas nuevas sobre por qué la cuenta marcada es admisible, quién pone y quién levanta la marca, y la vigencia de la declaración de verificación en el cambio forzado. Sube minor: agrega dos flujos alternativos, un motivo y una salida al contrato, sin invalidar ninguna decisión previa. |
| 1.2 | 2026-08-09 | **Reconciliación con el `PRODUCT-INTAKE` 1.8 y con `GeometriaFactory-Domain` CU-04004.** La versión 1.1 modelaba la cuenta reseteada como **admisible con la marca** —«la cuenta sí ingresa»— sobre el enunciado de RN-04013 anterior a la precisión, y con eso contradecía al dominio, que en CU-04004 FA-03 la devuelve **no admisible** con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`. El intake 1.8 §4.1 resuelve la ambigüedad del lado del dominio: la cuenta **se autentica y no obtiene sesión de trabajo**. Se corrigen **§4 paso 5** —la consulta deja de devolver la marca, porque la cuenta marcada no llega a admisible—, **FA-06**, **§6**, las dos postcondiciones de consulta y de cambio forzado, **CA-06** y **CA-07**, y la nota de §10, que pasa a fundar por qué el motivo es un encaminamiento y no un rechazo. **Punto abierto cerrado**: §9 citaba a RN-04013 «todavía sin archivo propio en `GeometriaFactory-Domain`», y el archivo existe; la cita pasa a ser un enlace. La cabecera cita el intake **1.8**. Sube minor: corrige la forma del resultado de un flujo existente, sin agregar ni quitar flujos ni motivos. |
| 1.3 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-04016) y la precisión de F-04.** Habilitar produce la contraseña provisoria y pone la marca, con dos efectos acá. **La fijación cambia de solicitante**: §1, FA-02 y CA-03 pasan a declarar que la ejerce **CU-04002** dentro de la habilitación y no una petición del alumno, con lo que **desaparece el último camino de esta capa que escribía una contraseña sin identidad**. **Y un motivo se retira**: §6 saca `CREDENCIAL_NO_ESTABLECIDA`, cuya causa dejó de ser posible, con la fila de retiro y la constancia de que el identificador **no se recicla**; los motivos pasan de nueve a **ocho**. **§5**: FA-01 se rehace sobre la cuenta recién habilitada, que recibe el **mismo** motivo que la reseteada; FA-05 declara los **dos orígenes** de la marca y que recorren el mismo flujo; FA-06 pierde la mención de la fijación, que ya no la pide una cuenta marcada. **§8**: CA-02 y CA-03 se rehacen y entra **CA-08**, que verifica **0 caminos** que fijen contraseña sin vigente verificada ni solicitante autenticado. **§10**: la nota de quién pone la marca se corrige —la ponen dos operaciones de CU-04002 además de CU-04011— y entra la nota que declara que no queda ningún camino anónimo. Sube minor. |

## 17. Compatibilidad de la superficie pública

Agregar un motivo a la enumeración de no admisibilidad es compatible si el consumidor tiene un camino por defecto. Devolver admisible con la credencial derivada ausente, o admitir el reemplazo sin la declaración de verificación, son cambios incompatibles con RN-04006 y con la custodia de la credencial vigente.

**Devolver admisible una cuenta con la marca de cambio de contraseña pendiente es incompatible**, aunque el resultado transporte la marca junto al papel: emitiría acceso a una cuenta que por INV-09 no ejerce ninguna capacidad, y contradice `GeometriaFactory-Domain` CU-04004 FA-03 y la precisión de RN-04013 del `PRODUCT-INTAKE` 1.8. **Tampoco** es compatible levantar la marca por cualquier vía que no sea el reemplazo de FA-05 hecho por la propia cuenta.
