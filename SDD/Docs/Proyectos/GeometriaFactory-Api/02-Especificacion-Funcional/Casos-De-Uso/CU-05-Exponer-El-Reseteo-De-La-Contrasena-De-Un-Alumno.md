# CU-05 — Exponer el reseteo de la contraseña de un alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md), [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §4 (**F-26**, F-03, **F-04** precisada), §4.1 (RN-12, RN-13, RN-14, RN-15, **RN-16**), §7 (CL-7), §9 (X-2 retirada), §11 (RN-B6 cerrado), §17.1.P.2 (INV-08, INV-09), §14 (RA-03); `Proyectos/GeometriaFactory-Contracts/.../CU-08-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md`; `Proyectos/GeometriaFactory-Application/.../CU-11-Resetear-La-Contrasena-De-Un-Alumno.md`; `Proyectos/GeometriaFactory-Infrastructure/.../CU-07-Producir-La-Contrasena-Provisoria-Del-Reseteo.md`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Api

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

---

## 1. Propósito

Exponer **A-09**, el punto de acceso con el que el administrador resetea la contraseña de un alumno. Es el único punto de toda esta superficie que **devuelve un valor de credencial en su respuesta**, y por eso su contrato es el más estricto en lo que no se puede hacer con esa respuesta.

Es también el punto que cierra un agujero de diseño que el intake declara con nombre: hasta que la capacidad del reseteo entró, el único camino ante un olvido de contraseña era dar de baja y volver a dar de alta, y eso **eliminaba todos los trabajos del alumno**. De ahí la propiedad que este punto tiene que hacer observable desde afuera: **resetear conserva la cuenta, su situación, su papel y todos sus trabajos con sus estados y comentarios**.

Lo que este caso de uso **no** hace: **no produce la provisoria** —la produce el mecanismo de credenciales de `GeometriaFactory-Infrastructure`, que es la única capa con tramo en esa regla—, no la elige, no la valida y **no la registra en ninguna parte**.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Arma la solicitud desde el panel del administrador y **muestra la provisoria una vez** para que él se la comunique al alumno |
| Administrador | Sujeto de la regla | Acciona el reseteo. **No escribe la contraseña** y no la conoce después de cerrar la pantalla |
| Mecanismo de credenciales de `GeometriaFactory-Infrastructure` | Sistema | Produce la provisoria, no adivinable y sin repetirse, y la deriva |

## 3. Precondiciones

- La petición trae acceso firmado con papel `Administrador` y atravesó la guardia de CU-02.
- La solicitud lleva **el identificador de la cuenta y nada más**. En particular **no lleva contraseña**: el panel del administrador no tiene campo de contraseña.

## 4. Flujo principal

1. Llega una petición a **A-09** con el identificador de la cuenta.
2. Se ejerce el reseteo contra la capa de aplicación, que verifica la facultad y acota la operación a cuentas con papel `Alumno`.
3. La capa de aplicación obtiene la provisoria ya producida y ya derivada, la fija como credencial y **deja la marca de cambio de contraseña pendiente**, conservando la situación de la cuenta.
4. Se responde `200` con el resultado del reseteo: **la contraseña provisoria**, la situación conservada de la cuenta y la declaración del cambio pendiente.
5. La provisoria **no queda en ninguna traza del servidor**.

**La situación de la cuenta no se consulta y no se cambia.** Este punto no declara ningún parámetro de situación y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe: resetear opera sobre la credencial y no es una transición de la máquina de estados de la cuenta.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta reseteada está en situación `Pendiente`, o `Bloqueado` | El reseteo **procede igual** y la cuenta **queda en la misma situación** en la que estaba. Es el enunciado literal de la regla que lo independiza del estado, y el motivo declarado es que el administrador no tenga que acordarse de una secuencia | Paso 4 |
| FA-02 | Se resetea dos veces seguidas la misma cuenta | Las **dos** peticiones responden `200` y las provisorias son **distintas**. La marca queda puesta las dos veces, y la segunda provisoria reemplaza a la primera | Paso 4 |
| FA-03 | La cuenta reseteada tiene trabajos en `Borrador`, en `Rechazado` y en `Finalizado` | El reseteo **no toca ninguno**: conserva los tres con sus estados y sus comentarios. Se verifica listándolos después | Paso 4 |

## 6. Excepciones y errores

| Código del contrato | Respuesta | Causa |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | La solicitud llega sin identificador de cuenta |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | La cuenta referenciada no existe. **Adopción declarada**, con el mismo fundamento de `CU-04` §10 |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `409` | Se pidió el reseteo sobre la cuenta con papel `Administrador`. **No es `403`**: quien pide tiene la facultad, y lo que no procede es la operación sobre esa cuenta. El camino que sí existe es el cambio de la propia contraseña, por **A-05** |
| ~~`CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`~~ | — | **Retirado del conjunto cerrado** por `PRODUCT-INTAKE` 1.13 §4.1 (**RN-16**). Existía porque el camino declarado para una cuenta sin contraseña era el primer ingreso anónimo; suprimido ese camino, **el reseteo procede** y fija la provisoria en lugar de reemplazarla. El retiro cierra además la tensión con **RN-15**, que declara que el reseteo procede sobre `Pendiente`. **El identificador no se recicla** ~ino que ya existe es que la persona la establezca en su primer ingreso, por **A-04** |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `403` o `503` | `403` cuando el papel del acceso no alcanza y el contrato no tiene código de facultad para este camino (§10); `503` cuando el almacén no está disponible **o la fuente de material impredecible no respondió** |

**Ninguna fila por cuenta no habilitada, y su ausencia es informativa.** El ensamblado de contratos lo declara explícitamente: de las dos causas de reseteo rechazado que se habían llegado a plantear, **la de la cuenta no habilitada dejó de existir** cuando el Product Owner decidió que resetear no exige habilitación. Esta superficie no la repone.

**Cuando la provisoria no se pudo producir, el reseteo no se completa.** El mecanismo de credenciales termina de forma degradada en lugar de componer el valor por otro medio, y esta superficie lo transporta como fallo. **Un reseteo que no se completa es recuperable —se vuelve a pedir— y una provisoria adivinable no se nota hasta que alguien la usa.**

## 7. Postcondiciones

- **Éxito:** la pieza pública tiene la provisoria **una sola vez**; la cuenta conserva su situación, su papel, su identidad y **todos sus trabajos**, y queda con la marca puesta. A partir de ahí, **cualquier otra petición de esa cuenta recibe el `403` de la guardia** hasta que cambie la contraseña por A-05.
- **Fallo:** el almacén queda como estaba, **la credencial anterior sigue vigente** y no se produjo ninguna provisoria.
- **En los dos casos:** la provisoria **no aparece en ninguna traza**, ni siquiera parcialmente.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con **3** trabajos, uno en `Borrador`, uno en `Rechazado` y uno en `Finalizado`, con comentario | Se invoca A-09 | Responde `200`, y el alumno conserva los **3** trabajos con sus **3** estados y su comentario: **0 pérdidas** |
| CA-02 | Una cuenta en situación `Bloqueado` | Se invoca A-09 | Responde `200` y la cuenta **sigue en situación `Bloqueado`**: el reseteo no la cambió |
| CA-03 | La misma cuenta | Se invoca A-09 **dos** veces seguidas | Las 2 respuestas son `200` y las **2** provisorias son **distintas** |
| CA-04 | La cuenta con papel `Administrador` | Se invoca A-09 sobre ella | Responde `409` con su código propio, y la credencial de esa cuenta **no cambió** |
| CA-05 | Una cuenta de alumno en situación `Pendiente`, que nunca fue habilitada y no tiene contraseña | Se invoca A-09 | Responde **`200`** con su provisoria y la situación `Pendiente` sin cambio: **0 respuestas `409`** se producen por la ausencia de contraseña previa |
| CA-06 | Una cuenta reseteada | Se invoca **cualquier otro punto** que exija acceso, con un acceso obtenido antes del reseteo | Responde `403` con el código de cambio requerido: la marca corta **aunque el acceso siga siendo válido** |
| CA-07 | La respuesta de un reseteo con éxito y el registro del servidor | Se inspeccionan | La provisoria aparece **exactamente 1 vez**, en el cuerpo de la respuesta, y **0 veces** en el registro del servidor |
| CA-08 | La fuente de material impredecible que no responde | Se invoca A-09 | Responde con fallo, **0 provisorias producidas** y la credencial anterior **sigue vigente** |
| CA-09 | El punto A-09 | Se inspecciona su superficie | **0 parámetros** de situación de cuenta y **0 filas** de respuesta por cuenta no habilitada |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 por la operación del administrador; NB-02 porque es lo que hace sostenible la identidad propia sin canal de correo |
| Reglas de negocio aplicables | [RN-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md), que este punto hace observable desde afuera. [RN-15](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-15-Reseteo-Independiente-Del-Estado-De-Cuenta.md), **de forma estructural**: la superficie no declara parámetro de situación ni respuesta por cuenta no habilitada. [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), porque este punto es el único que pone la marca. [RN-14](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-14-Provisoria-Producida-Por-El-Sistema.md), **sin tramo acá**: lo único que esta capa declara sobre ella es **lo que no hace con el valor** |
| Invariantes del producto | **INV-08**, que sostiene por qué la cuenta de administrador no admite reseteo por este camino; **INV-09**, que la marca hace exigible |
| Regla de arquitectura del producto | **RA-03**, con una exigencia propia: la provisoria **no entra al registro del servidor**, que es donde todo lo demás sí entra |
| Punto de acceso | A-09 |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-08` |
| Historias de usuario a generar en 06 | US-14, US-15, US-16 |
| Componentes esperados en 05 | Punto de acceso de reseteo, con la exclusión explícita de la provisoria del registro estructurado |
| Tests previstos en 08 | Integración por los nueve criterios; y una **inspección del registro del servidor** que verifique que la provisoria no aparece |

## 10. Notas y supuestos

- **La provisoria se devuelve una vez y no se puede volver a pedir.** Si el administrador cierra la pantalla sin comunicarla, el camino declarado es **volver a resetear**, que produce un valor nuevo. Esta superficie no ofrece ningún punto para recuperar una provisoria ya emitida, y agregarlo obligaría a conservarla, que es lo que el producto no hace.
- **El papel insuficiente en este punto no tiene código propio en el contrato**, igual que en el gobierno de cuentas. Está elevado al Product Owner en el índice maestro §11.
- **`403` y `409` no se confunden acá, y la distinción vale la pena escribirla.** El `403` dice «vos no podés pedir esto»; el `409` dice «esto no se puede pedir sobre esa cuenta». Las dos negativas del reseteo son del segundo tipo: quien pide **tiene** la facultad.
- **El registro del servidor de este punto es la excepción declarada de la observabilidad del producto.** El intake exige registro estructurado de cada error y de cada intento rechazado; acá el registro existe igual, y lo que se excluye es **el valor producido**, no el hecho de que hubo un reseteo.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04.** Habilitar produce una contraseña provisoria con **el mismo mecanismo y el mismo tratamiento** que este punto de acceso, de modo que el producto tiene un solo mecanismo de credencial inicial. **§6**: `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` queda **retirado** del conjunto cerrado —su premisa, la existencia de un camino anónimo alternativo, desapareció— y se conserva como fila tachada, con la constancia de que el retiro cierra de paso la tensión con **RN-15**. **§8**: **CA-05** se invierte: el reseteo sobre una cuenta `Pendiente` sin contraseña **procede** y responde `200`. La cabecera cita el intake **1.13**. **El punto A-09, su resultado y la exclusión de la provisoria del registro del servidor no cambian.** Sube minor. |
