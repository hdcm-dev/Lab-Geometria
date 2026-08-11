# CU-04 — Evaluar la admisibilidad de la cuenta para acceder al laboratorio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-04-Evaluar-La-Admisibilidad-De-La-Cuenta.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §2 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4 (**F-04** precisada, F-03, **F-26**), §4.1 (RN-06, **RN-13**, **RN-16**), §17.1.P.2 (INV-06, **INV-09**), §17.1.P.5, §17.5.P.5, §6 (flujo 1), §7 (**CL-7**)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

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

Responder, sobre una cuenta concreta, si admite o no admite acceso al laboratorio y con qué motivo, para que la capa que emite el acceso no tenga que interpretar por su cuenta el estado de la cuenta. Materializa **INV-06** —un alumno con cuenta `Pendiente` o `Bloqueado` no obtiene acceso— y, desde `PRODUCT-INTAKE` 1.7, también **INV-09**: una cuenta con la marca de cambio de contraseña pendiente no ejerce ninguna capacidad salvo cambiar su propia contraseña.

**El motivo `CREDENCIAL_NO_ESTABLECIDA` queda retirado por `PRODUCT-INTAKE` 1.13.** Cubría la cuenta `Habilitado` **sin** credencial derivada, que era la situación esperada del primer ingreso. Con **RN-16**, habilitar produce la contraseña provisoria y la fija, de modo que esa combinación **no puede existir**: toda cuenta de alumno `Habilitado` tiene credencial. El motivo no se recicla para ninguna otra causa, para que una referencia vieja no resuelva en silencio a una condición distinta de la que nombraba.

**Por qué INV-09 se ejerce acá y no en cada caso de uso.** El invariante alcanza a *todas* las capacidades del sistema y el dominio no tiene una puerta única por la que pasen todas. La decisión de esta categoría es concentrar la guarda en esta evaluación, con el mismo fundamento por el que INV-06 vive acá: ninguna capacidad se ejerce sin admisión resuelta. Está declarada como **decisión derivada** en [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §4.1 y como punto abierto en `Especificacion-Funcional.md` §9.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Consulta la admisibilidad antes de resolver un ingreso |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Emite el acceso sólo si la evaluación fue admisible. El mecanismo de emisión no es del dominio |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Evalúa el estado de la cuenta y devuelve el motivo de la negativa |

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- La comprobación de la credencial presentada ya ocurrió, o va a ocurrir, fuera del dominio: esta evaluación es sobre la cuenta, no sobre la credencial.

## 4. Flujo principal

1. La capa de aplicación consulta la admisibilidad de la cuenta de un alumno.
2. El dominio lee el estado de cuenta.
3. El dominio comprueba que el estado sea `Habilitado`.
4. El dominio comprueba que la marca de cambio de contraseña pendiente esté levantada.
5. El dominio devuelve admisible, sin ningún motivo asociado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-02 | La cuenta consultada tiene papel `Administrador` | La evaluación es la misma: se resuelve por estado, por credencial y por marca, no por papel. La autorización por papel es de la capa que expone los endpoints, no del dominio. La cuenta de administrador nunca tiene la marca puesta, porque el reseteo no procede sobre ella (CU-13 §6) | Paso 3 |
| FA-03 | El estado es `Habilitado`, la credencial tiene valor y **la marca de cambio de contraseña pendiente está puesta** | El dominio devuelve **no admisible** con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`. No es un rechazo: es la situación esperada después de un reseteo, en la que corresponde invocar el reemplazo de CU-03 FA-04, que es lo único que la cuenta puede hacer (RN-13, INV-09) | Termina el caso de uso con resultado no admisible |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `CUENTA_PENDIENTE` | El estado de cuenta es `Pendiente` | Devuelve no admisible con este motivo, para que el consumidor pueda informarle a la persona su situación con todas las letras y no con un rechazo genérico |
| `CUENTA_BLOQUEADA` | El estado de cuenta es `Bloqueado` | Devuelve no admisible con este motivo |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | El estado es `Habilitado`, la credencial tiene valor y la marca está puesta | Devuelve no admisible con este motivo, que el consumidor traduce en el pedido de cambiar la contraseña provisoria. **Es lo único que la cuenta puede hacer hasta que la marca se levante** (INV-09) |

Los **tres** son terminaciones controladas y no excepciones de programa: la evaluación siempre devuelve un resultado, y ese resultado incluye el motivo. Ninguno es un código de protocolo: la traducción a respuesta pertenece a `GeometriaFactory-Api`.

**Motivo retirado en la versión 1.3.** `CREDENCIAL_NO_ESTABLECIDA` queda **retirado** y no figura entre los motivos vivos de este contrato: su causa —cuenta `Habilitado` sin credencial derivada— dejó de ser posible con **RN-16** (`PRODUCT-INTAKE` 1.13 §4.1). **El identificador retirado no se recicla.** Toda cita anterior que describiera el encaminamiento del primer ingreso resuelve hoy a `CAMBIO_DE_CONTRASENA_PENDIENTE`, que es el motivo con el que el alumno recién habilitado llega al cambio.

## 7. Postcondiciones

- **Éxito:** el resultado es admisible o no admisible con exactamente un motivo. En ningún caso el dominio cambia el estado de la cuenta: la evaluación no tiene efecto.
- **Fallo:** no hay caso de fallo propio. Una cuenta inexistente no llega hasta acá, porque el dominio evalúa sobre una entidad ya constituida.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado` y credencial derivada con valor | La capa de aplicación consulta la admisibilidad | El dominio devuelve admisible, con 0 motivos, y la cuenta sigue en `Habilitado` |
| CA-02 | Un alumno con cuenta `Pendiente` | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CUENTA_PENDIENTE` |
| CA-03 | Un alumno con cuenta `Bloqueado` y credencial derivada con valor | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CUENTA_BLOQUEADA` |
| CA-04 | Un alumno **recién habilitado**: cuenta `Habilitado`, credencial provisoria con valor y marca puesta | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`. **0 cuentas de alumno `Habilitado` sin credencial derivada** son alcanzables, y por eso el motivo `CREDENCIAL_NO_ESTABLECIDA` ya no tiene criterio que lo ejercite |
| CA-05 | Un alumno reseteado: cuenta `Habilitado`, credencial provisoria con valor y marca de cambio de contraseña pendiente puesta | La capa de aplicación consulta la admisibilidad | El dominio devuelve no admisible con el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`, con 1 motivo y 0 cambios sobre la cuenta |
| CA-06 | El mismo alumno, después de reemplazar su credencial por CU-03 FA-04 | La capa de aplicación consulta la admisibilidad | El dominio devuelve admisible, con 0 motivos: la marca quedó levantada y la cuenta opera con normalidad |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 en su criterio de admisión explícita, NB-02 en su criterio de explicación al alumno no habilitado |
| Reglas de negocio aplicables | [RN-06](../Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md); [RN-13](../Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), que es la que sostiene el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE`; [RN-16](../Reglas-De-Negocio/RN-16-Habilitar-Produce-La-Provisoria.md), que es la que **retira** el motivo `CREDENCIAL_NO_ESTABLECIDA` al hacer imposible su causa; y [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) en cuanto al conjunto cerrado de papeles |
| Invariantes | INV-06 e **INV-09** |
| Historias de usuario a generar en 06 | US de ingreso de alumno habilitado, US de aviso de cuenta pendiente, US de aviso de cuenta bloqueada, **US-27** de encaminamiento al cambio obligatorio de contraseña, que cubre **también** el primer ingreso del alumno recién habilitado |
| Componentes esperados en 05 | Consulta de admisibilidad sobre la entidad de alumno, con su enumeración cerrada de motivos |
| Tests previstos en 08 | **Cuatro** pruebas unitarias, una por cada resultado posible —admisible y los tres motivos—, sin dobles; la de `CAMBIO_DE_CONTRASENA_PENDIENTE` se ejercita sobre una cuenta **recién habilitada** y sobre una **reseteada**, con su par después del cambio, porque desde RN-16 las dos llegan por el mismo camino |

## 10. Notas y supuestos

- INV-06 es una regla de dominio aunque el acceso se materialice en la infraestructura. El dominio modela **la condición**; el mecanismo por el que el acceso se emite y su vigencia pertenecen a `GeometriaFactory-Infrastructure` y a `GeometriaFactory-Api`.
- La distinción entre un rechazo genérico por credencial inválida y un aviso explícito por cuenta `Pendiente` o `Bloqueado` es una decisión declarada aguas arriba (PRODUCT-INTAKE §17.5.P.5) y este caso de uso le da al consumidor el dato para sostenerla.
- **El paso 4 del flujo anterior desapareció y conviene decir por qué no es un aflojamiento.** La comprobación de que la credencial derivada tenga valor se retiró del flujo porque **RN-16** la trasladó al momento de la habilitación, donde CU-02 la exige y la rechaza si falta. La condición no dejó de comprobarse: se comprueba antes, y una vez, en lugar de en cada evaluación de admisibilidad.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. §9 incorpora **RN-06**, la regla que INV-06 sostiene y cuyo enunciado el intake anterior no transcribía. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |
| 1.2 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**: la capacidad **F-26**, la regla **RN-13** y el invariante **INV-09**. §1 declara que este caso de uso materializa ahora dos invariantes y **por qué la guarda de INV-09 se concentra acá**, como decisión derivada de esta categoría y no como transcripción. El flujo principal suma el paso 5, la comprobación de la marca; **FA-03** describe la situación esperada después de un reseteo; §6 suma el motivo **`CAMBIO_DE_CONTRASENA_PENDIENTE`** y el conjunto pasa de tres a **cuatro**; §8 suma **CA-05** y **CA-06**, el par que verifica el motivo y su levantamiento; y §9 refiere RN-13 e INV-09. **§17 declara la discrepancia** entre la regla de compatibilidad de este contrato, que trata el motivo nuevo como incompatible, y la subida minor del documento, que sigue la política de versionado de esta propagación. |
| 1.3 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04.** Habilitar produce y fija la contraseña provisoria, de modo que la combinación «cuenta `Habilitado` **sin** credencial derivada» deja de ser alcanzable. **§1** declara el retiro del motivo `CREDENCIAL_NO_ESTABLECIDA` con su fundamento y con la constancia de que el identificador **no se recicla**. **§4** pierde el paso que comprobaba la credencial y renumera; **§5** pierde **FA-01**, que describía la situación esperada del primer ingreso. **§6** retira la fila del motivo y el conjunto pasa de cuatro a **tres**, con la fila de retiro declarada. **§8** rehace **CA-04** sobre el alumno recién habilitado, que llega al mismo motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` que el reseteado. **§9** suma RN-16 como la regla que retira el motivo y ajusta las pruebas previstas de cinco a **cuatro**. **§10** suma la nota que declara que la comprobación retirada del flujo **no dejó de hacerse**: se hace una vez, en la habilitación de CU-02. Sube minor. |

## 17. Compatibilidad de la superficie pública

El conjunto de motivos es cerrado y forma parte del contrato: agregar un motivo obliga a revisar a los consumidores que los traducen a mensajes, y por eso sube la versión mayor de este caso de uso.

**La emisión 1.3 retira un motivo del conjunto cerrado, y eso es también un cambio incompatible** para los consumidores que lo traducen: `GeometriaFactory-Application` y la capa que traduce motivos dejan de tener que contemplar `CREDENCIAL_NO_ESTABLECIDA`, y una implementación que lo siga produciendo estaría declarando una cuenta que RN-16 no admite. **Reponer el motivo se rechaza aunque compile.** La versión del documento sube minor por la misma política que la emisión 1.2 declara abajo.

**La emisión 1.2 agrega un motivo y aun así sube minor**, y la discrepancia se declara en lugar de disimularse. La regla de compatibilidad de arriba habla del **contrato frente a sus consumidores**, y sigue valiendo: `CAMBIO_DE_CONTRASENA_PENDIENTE` obliga a `GeometriaFactory-Application` y a la capa que traduce motivos a revisar su enumeración, y el cambio se trata como incompatible a todos los efectos técnicos. La **versión del documento** sigue la política de versionado del framework para esta propagación, que es minor. Si el punto de control decide que las dos cosas tienen que coincidir, lo que corresponde es subir este caso de uso a 2.0, no retirar el motivo.
