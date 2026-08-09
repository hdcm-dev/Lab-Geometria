# CU-03 — Resolver el ingreso y la credencial del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §5 (explicación al alumno no habilitado, custodia de la credencial vigente, alta de punta a punta); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-04, F-05, F-26), §4.1 (RN-06, RN-13), §6 (flujo 1), §17.1.P.2 (INV-09), §17.2.P.5; orquesta [`CU-04`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md) y [`CU-03`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) de GeometriaFactory-Domain
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

Resolver, sobre una cuenta concreta, si admite el ingreso al laboratorio y con qué motivo si no lo admite, y sostener las dos operaciones sobre la credencial derivada: fijarla en el primer ingreso efectivo y reemplazarla después exigiendo la vigente. Es lo que hace posible la promesa del producto de que **ninguna credencial se transporta**, en un laboratorio sin canal de correo.

Este caso de uso **no emite el acceso ni deriva la contraseña**: recibe el valor ya derivado y devuelve la admisibilidad con su motivo. Los dos mecanismos pertenecen a las capas externas.

Desde el `PRODUCT-INTAKE` **1.7** sostiene además el **cambio forzado**: el reemplazo que hace una cuenta reseteada por CU-11, que es **lo único que levanta la marca de cambio de contraseña pendiente** (RN-13, INV-09). Es el mismo reemplazo de siempre; lo que cambia es de dónde viene y qué deja atrás.

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
4. La cuenta está en estado `Habilitado` y tiene credencial derivada: el dominio devuelve admisible.
5. El caso de uso devuelve admisible junto con la identidad, el papel de la cuenta y **la marca de cambio de contraseña pendiente**, que es lo que el consumidor necesita para resolver el ingreso y para saber si esa cuenta queda confinada al cambio (§10).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta está en estado `Habilitado` y todavía no tiene credencial derivada | El dominio devuelve no admisible con el motivo `CREDENCIAL_NO_ESTABLECIDA`. El caso de uso lo devuelve tal cual, y el consumidor lo traduce en el pedido de establecer la contraseña: es el primer ingreso efectivo del flujo de alta | Termina el caso de uso |
| FA-02 | El consumidor solicita **fijar** la credencial derivada de una cuenta habilitada que no la tiene | El caso de uso toma el sello de modificación del puerto de reloj, invoca la fijación en el dominio y materializa la cuenta. Devuelve que procede | Paso 5 |
| FA-03 | El consumidor solicita **reemplazar** la credencial derivada declarando que verificó la vigente | El caso de uso toma el sello de modificación del puerto de reloj, invoca el reemplazo en el dominio y materializa la cuenta | Paso 5 |
| FA-04 | El consumidor solicita el reemplazo sin declarar la verificación de la credencial vigente | El caso de uso propaga el rechazo del dominio con el motivo `CREDENCIAL_VIGENTE_NO_VERIFICADA` y no toca la cuenta | Termina el caso de uso |
| FA-05 | El consumidor solicita el reemplazo sobre una cuenta **marcada como con cambio de contraseña pendiente**, declarando verificada la vigente —que es la provisoria que le fijó el administrador en CU-11— | Es el **cambio forzado**. El caso de uso ejerce el reemplazo de FA-03 y además **levanta la marca** en la misma unidad de trabajo. La contraseña nueva la elige el alumno y el administrador no la conoce (RN-13) | Paso 5 |
| FA-06 | Una cuenta marcada pide cualquier otra cosa que no sea su propio cambio de contraseña —incluida la fijación de FA-02— | El caso de uso devuelve no procede con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`, sin leer ni escribir nada (INV-09). **Es la única excepción declarada de la guardia transversal del índice maestro §4**: el reemplazo de FA-05 sí procede, porque es lo que la levanta | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CUENTA_PENDIENTE` | El estado de cuenta es `Pendiente` | Devuelve no admisible con este motivo, para que la persona sepa que todavía no fue habilitada y no reciba un rechazo genérico (RN-06) |
| `CUENTA_BLOQUEADA` | El estado de cuenta es `Bloqueado` | Devuelve no admisible con este motivo |
| `CREDENCIAL_NO_ESTABLECIDA` | La cuenta está habilitada y no tiene credencial derivada | Devuelve no admisible con este motivo, que abre el camino de FA-02 |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | Se intenta fijar o reemplazar sobre una cuenta `Pendiente` o `Bloqueado` | Propaga el rechazo del dominio y conserva la credencial como estaba |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Reemplazo sin la declaración de verificación | Propaga el rechazo del dominio |
| `CREDENCIAL_YA_FIJADA` | Se pide fijar por primera vez una credencial que ya tiene valor | Propaga el rechazo del dominio: el camino correcto es el reemplazo de FA-03 |
| `VALOR_DERIVADO_VACIO` | El valor de credencial aportado está vacío | Propaga el rechazo del dominio y conserva la credencial como estaba |
| `CUENTA_INEXISTENTE` | El puerto de repositorio no encuentra el correo | Devuelve no admisible sin distinguir el motivo hacia afuera, para no revelar qué correos están registrados |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | La cuenta está marcada por un reseteo de CU-11 y pide algo que no es su propio cambio de contraseña | No lee ni escribe nada. **No es una negativa de admisibilidad**: la cuenta sí ingresa, y lo único que puede hacer es cambiar la contraseña (RN-13, INV-09) |

## 7. Postcondiciones

- **Éxito, consulta:** el resultado es admisible con la identidad, el papel y **la marca de cambio de contraseña pendiente**, o no admisible con exactamente un motivo. Ninguna cuenta cambia.
- **Éxito, credencial:** la cuenta tiene la credencial derivada nueva y su sello de modificación es el del reloj.
- **Éxito, cambio forzado:** además de lo anterior, **la marca queda levantada** y la cuenta opera con normalidad. Es la única postcondición del producto que la levanta.
- **Fallo:** la cuenta queda exactamente como estaba, marca incluida.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta `Pendiente` de `ana.perez@ejemplo.edu` | El consumidor consulta la admisibilidad de ese correo | El caso de uso devuelve no admisible con el motivo `CUENTA_PENDIENTE` |
| CA-02 | Una cuenta en estado `Habilitado` de `ana.perez@ejemplo.edu`, sin credencial derivada | El consumidor consulta la admisibilidad | El caso de uso devuelve no admisible con el motivo `CREDENCIAL_NO_ESTABLECIDA` |
| CA-03 | La misma cuenta y un reloj fijado en 2026-03-20 | El consumidor solicita fijar la credencial derivada con un valor no vacío | El caso de uso devuelve que procede y la cuenta queda con credencial derivada y sello de modificación 2026-03-20 |
| CA-04 | Una cuenta en estado `Habilitado` con credencial derivada | El consumidor solicita reemplazarla sin declarar que verificó la vigente | El caso de uso devuelve el motivo `CREDENCIAL_VIGENTE_NO_VERIFICADA` y la credencial derivada no cambia |
| CA-05 | Un repositorio sin ninguna cuenta con el correo `nadie@ejemplo.edu` | El consumidor consulta la admisibilidad de ese correo | El caso de uso devuelve no admisible sin declarar si el correo existe |
| CA-06 | Una cuenta `Habilitado` reseteada por CU-11, con la marca puesta | El consumidor consulta la admisibilidad | El caso de uso devuelve **admisible**, con la identidad, el papel y **la marca de cambio de contraseña pendiente** |
| CA-07 | La misma cuenta | El consumidor solicita el reemplazo declarando verificada la credencial vigente | El caso de uso devuelve que procede, la credencial queda reemplazada y **la marca queda levantada** |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 |
| Reglas de negocio aplicables | [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), y **RN-13** del `PRODUCT-INTAKE` 1.7 §4.1, todavía sin archivo propio en `GeometriaFactory-Domain` |
| Casos de uso de dominio orquestados | [CU-04](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md), [CU-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-07, US-08, US-09, US-32 |
| Componentes esperados en 05 | Caso de uso de admisibilidad y caso de uso de credencial, con su resultado tipado y su enumeración cerrada de motivos |
| Tests previstos en 08 | Unitarias con dobles sobre los tres estados de cuenta, la cuenta habilitada sin credencial, la fijación, el reemplazo con y sin verificación, el correo inexistente, la admisibilidad que devuelve la marca puesta y el cambio forzado que la levanta |

## 10. Notas y supuestos

- **La derivación de la contraseña no es de esta capa** y el valor en claro nunca la atraviesa: llega derivado desde afuera, tal como el dominio lo exige.
- **La emisión del acceso tampoco es de esta capa.** Acá se resuelve si la cuenta lo admite y por qué; quién lo emite y con qué mecanismo es materia de las capas externas.
- La comparación de la credencial vigente la ejerce quien sabe derivarla; este caso de uso exige que esa verificación se declare, que es la forma en que el dominio la hace exigible sin conocerla.
- El motivo de una cuenta inexistente no se distingue hacia afuera, por el mismo criterio con el que un trabajo ajeno es indistinguible de uno inexistente.
- **El sello de modificación es un metadato de orquestación** que esta capa aporta al materializar la cuenta. El modelo del dominio declara la fecha de alta del alumno, que recibe del consumidor, y **no declara una fecha de última modificación**: mientras el Product Owner no resuelva incorporarla al modelo, este sello se lee como dato de esta capa y no como atributo del dominio. Tampoco se confunde con la «Fecha» que el alumno declara en su trabajo.
- **Esta cuenta puede no pasar nunca por la fijación por primera vez.** La del administrador nace con credencial fijada por CU-10, de modo que para ella el único camino es el reemplazo.
- **La marca de cambio de contraseña pendiente viaja con la admisibilidad y no la reemplaza.** Una cuenta reseteada es **admisible**: ingresa. Lo que la marca declara es que el consumidor debe llevarla al cambio y no a ninguna otra ruta. Devolverla como no admisible sería contradecir RN-13, que dice que ingresa, y dejaría al alumno sin camino para levantarla.
- **La marca la pone únicamente CU-11 y la levanta únicamente el cambio efectivo de FA-05**, hecho por la propia cuenta (INV-09). Ningún otro flujo de esta capa la toca: ni la habilitación, ni el bloqueo, ni la rehabilitación de CU-02, que conservan la marca tal cual estaba.
- **La declaración de verificación de la credencial vigente sigue siendo obligatoria en el cambio forzado.** Lo que el alumno presenta como vigente es la provisoria que el administrador le comunicó; el mecanismo de comparación no cambia y sigue viviendo fuera de esta capa.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-06**: la fecha de modificación de la cuenta pasa a llamarse **sello de modificación** y se declara metadato de orquestación de esta capa en §2 y en §10, porque el modelo del dominio no declara ese atributo y la discrepancia está elevada al Product Owner. **H-14**: §6 suma `CREDENCIAL_YA_FIJADA` y `VALOR_DERIVADO_VACIO`, propagados del dominio y hasta ahora sin camino de vuelta declarado. **H-01, propagado**: §10 declara que la cuenta del administrador nace con credencial fijada por CU-10 y por lo tanto sólo recorre el camino de reemplazo. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas **RN-12** y **RN-13** y el invariante **INV-09** de §17.1.P.2. **§1**: declara que este caso de uso sostiene además el **cambio forzado**, que es lo único que levanta la marca de cambio de contraseña pendiente. **§4 paso 5**: el resultado de la consulta de admisibilidad pasa a llevar también **la marca**, junto con la identidad y el papel. **§5**: **FA-05** nueva, el cambio forzado que reemplaza y levanta la marca en la misma unidad de trabajo, y **FA-06** nueva, la cuenta marcada que pide cualquier otra cosa. **§6**: suma `CAMBIO_DE_CONTRASENA_PENDIENTE`, con la precisión de que **no es una negativa de admisibilidad**. **§7**: la postcondición de la consulta incluye la marca y se agrega la del cambio forzado. **§8**: CA-06 y CA-07 nuevas. **§9**: RN-13 referenciada contra el intake, US-32 y los dos tests nuevos. **§10**: cuatro notas nuevas sobre por qué la cuenta marcada es admisible, quién pone y quién levanta la marca, y la vigencia de la declaración de verificación en el cambio forzado. Sube minor: agrega dos flujos alternativos, un motivo y una salida al contrato, sin invalidar ninguna decisión previa. |

## 17. Compatibilidad de la superficie pública

Agregar un motivo a la enumeración de no admisibilidad es compatible si el consumidor tiene un camino por defecto. Devolver admisible con la credencial derivada ausente, o admitir el reemplazo sin la declaración de verificación, son cambios incompatibles con RN-06 y con la custodia de la credencial vigente.

Agregar la **marca de cambio de contraseña pendiente** al resultado de la consulta es compatible mientras el consumidor tenga un camino por defecto para el valor ausente. **No** es compatible levantar la marca por cualquier vía que no sea el reemplazo de FA-05 hecho por la propia cuenta: contradice INV-09.
