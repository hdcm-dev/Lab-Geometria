# CU-01 — Canjear credenciales por un acceso firmado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-01-Canjear-Credenciales-Por-Un-Acceso-Firmado.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §4 (F-05, **F-04** precisada), §4.1 (RN-06, RN-13, **RN-16**), §14 (RA-01, RA-03), §17.5.P.3, §17.5.P.5 y su nota de seguridad, §11 (RN-B5); `Proyectos/GeometriaFactory-Contracts/.../CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` completo; `Proyectos/GeometriaFactory-Application/.../CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md`; `Proyectos/GeometriaFactory-Infrastructure/.../CU-08-Emitir-El-Acceso-Firmado.md` y `CU-06-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md`
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

Exponer **A-01**, el punto de acceso que recibe un correo y una contraseña y devuelve un acceso firmado. Es el único punto de esta superficie cuya ruta declara una fuente —el intake §17.5.P.3—, y el único que puede fallar de **cuatro** maneras distintas que la persona necesita distinguir.

Lo que este caso de uso hace: recibir el par de credenciales, pedir a las capas de adentro que resuelvan la admisibilidad de la cuenta, y **si la cuenta admite el acceso**, pedir su emisión y devolverlo. Lo que **no** hace: no compara contraseñas —eso es el mecanismo de credenciales de `GeometriaFactory-Infrastructure`—, no decide si la cuenta admite el acceso —eso lo resuelve el dominio y llega resuelto—, no sostiene sesión y **no guarda el acceso en ninguna parte**.

Es además el punto donde el producto acepta por escrito un riesgo: el intake §17.5.P.5 registra la elección de canjear la contraseña en claro contra este punto como **decisión consciente y no como omisión**, porque el intermediario es el propio front del mismo sistema y el alcance es un laboratorio de aula.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| `GeometriaFactory-Web` | Primario | Recibe correo y contraseña del formulario de la persona y los canjea contra este punto, **servidor a servidor** |
| Mecanismo de acceso firmado de `GeometriaFactory-Infrastructure` | Sistema | Emite el acceso con sus cuatro reclamos, firmado con la clave provista |
| Alumno y administrador | Sujetos de la regla | Nunca invocan este punto: **el navegador no alcanza esta superficie** (RA-01) |

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones (CU-11).
- La clave de firma está provista por configuración (CU-10). Sin ella **no se emite ningún acceso**.
- La petición llega por HTTP desde `GeometriaFactory-Web`, con el par de credenciales en el cuerpo, con los tipos del ensamblado de contratos.

## 4. Flujo principal

1. Llega la petición al punto **A-01** con correo y contraseña.
2. Se consulta la admisibilidad de la cuenta a la capa de aplicación.
3. La cuenta es admisible: la capa de aplicación devuelve la identidad y el papel.
4. Se verifica la credencial contra el valor derivado guardado, por el mecanismo de credenciales.
5. Se pide la emisión del acceso firmado con **identificador, correo, papel y expiración**.
6. Se responde `200` con el tipo de respuesta de sesión del contrato: el acceso, el identificador, el correo y el papel.

**El acceso no se guarda de este lado.** Esta superficie es sin estado: quien lo conserva es el circuito de la pieza pública, del lado de su servidor, y **el navegador no lo ve nunca**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La cuenta que se canjea tiene papel `Administrador` | El flujo es idéntico y sólo cambia el valor del reclamo de papel. **Este punto no distingue papeles**: qué habilita cada uno lo deciden los demás puntos y las capas de adentro | Paso 6 |
| FA-02 | La petición llega dos veces con las mismas credenciales | Se emiten **dos accesos**, y es correcto: el punto es sin estado y no lleva registro de emisiones. La vigencia corta es lo que acota el efecto | Paso 6 |
| FA-03 | El acceso emitido vence | **No es un camino de este punto**: la renovación del producto es **por reingreso** y no hay acceso de refresco en este alcance. La persona vuelve a canjear | Paso 1 de una petición nueva |

## 6. Excepciones y errores

**Los códigos son los del ensamblado de contratos.** Esta capa no agrega ninguno; lo que decide es su código de respuesta, según la tabla de [`Definicion-Superficie-HTTP.md`](../Definicion-Superficie-HTTP.md) §6.

| Código del contrato | Respuesta | Causa |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | Falta el correo o falta la contraseña. La respuesta **nombra el campo ausente**, que es un dato de la petición y no de la cuenta |
| `CONTRATO_CREDENCIAL_INVALIDA` | `401` | El par no corresponde a ninguna cuenta. **Genérico: la respuesta no declara cuál de los dos campos falló** (intake §17.5.P.5) |
| `CONTRATO_CUENTA_NO_HABILITADA` | `403` | La cuenta está `Pendiente` o `Bloqueado`. **Con motivo**, para que la persona sepa en qué situación está su cuenta |
| ~~`CONTRATO_CONTRASENA_NO_ESTABLECIDA`~~ | — | **Retirado del conjunto cerrado** por `PRODUCT-INTAKE` 1.13 §4.1 (**RN-16**): habilitar produce y fija la contraseña provisoria, de modo que ninguna cuenta llega a estar habilitada sin contraseña. **El identificador no se recicla**, y el desvío del primer ingreso lo cubre hoy el código de la fila siguiente. Se conserva la fila tachada para que una cita vieja no quede sin respuesta |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `403` | La cuenta tiene una provisoria sin cambiar, **producida por la habilitación (RN-16) o por el reseteo (F-26)**: desde el intake 1.13 es también el código del **primer ingreso**. Con motivo; la pieza pública lo convierte en el desvío a **A-05**. **No se emite acceso** |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible y la admisibilidad no se pudo resolver. **La respuesta no incluye la ruta del almacén** |

**Ninguna de las cinco vivas emite acceso, y ninguna deja rastro de la contraseña recibida**: ni en la respuesta, ni en el registro del servidor.

**Por qué dos motivos distintos comparten el `403` y uno solo tiene el `401`.** Los dos del `403` describen **la situación de una cuenta que existe** y la persona necesita saber cuál es, porque de eso depende qué tiene que hacer después: esperar la habilitación o cambiar la provisoria. Eran tres hasta que **RN-16** suprimió el establecimiento de la contraseña como camino propio. El `401` describe que **el par no corresponde a ninguna cuenta**, y ahí el producto deliberadamente no dice más: distinguir el correo desconocido de la contraseña equivocada permitiría averiguar por tanteo qué correos están registrados.

## 7. Postcondiciones

- **Éxito:** la pieza pública tiene un acceso firmado con sus cuatro reclamos, de vigencia corta. **Nada cambió del lado del servicio**: este punto no escribe.
- **Fallo:** la pieza pública tiene un código de respuesta y un código del contrato, y **ningún acceso**. La contraseña recibida no quedó registrada en ninguna parte.
- **En los dos casos:** el intento queda registrado del lado del servidor, sin la contraseña.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta de alumno habilitada, con contraseña establecida y sin marca | Se canjea con la contraseña correcta | Responde `200` y el cuerpo trae **el acceso, el identificador, el correo y el papel `Alumno`**, y **0 campos** más |
| CA-02 | La misma cuenta | Se canjea con la contraseña equivocada, y después con un correo que no existe | Las **2** respuestas son `401` y sus cuerpos son **idénticos**: 0 diferencias que permitan distinguir cuál de los dos casos ocurrió |
| CA-03 | Una cuenta recién registrada, en situación `Pendiente` | Se canjea | Responde `403` **con el motivo de la situación de la cuenta**, y **0 accesos emitidos** |
| CA-04 | Una cuenta **recién habilitada**, que canja con la provisoria que la habilitación produjo | Se canjea | Responde `403` con el motivo de cambio requerido —**el mismo** que CA-05, y no uno propio— y **0 accesos** se emiten |
| CA-05 | Una cuenta reseteada, con la provisoria sin cambiar, presentando **la provisoria correcta** | Se canjea | Responde `403` con el motivo de cambio requerido y **0 accesos emitidos**. La credencial se reconoce y **no se admite** |
| CA-06 | Una petición sin el campo de contraseña | Se canjea | Responde `400` nombrando el campo ausente |
| CA-07 | Cualquiera de las respuestas de §6, con su cuerpo y con el registro del servidor observados | Se produce la condición | **0 apariciones** de la contraseña recibida, de la clave de firma, de la ruta del almacén y de la dirección de cualquier servicio interno |
| CA-08 | El almacén no disponible | Se canjea | Responde `503`, y el cuerpo **no dice dónde está el almacén** |
| CA-09 | Un acceso emitido por este punto | Se inspecciona | Lleva **exactamente los cuatro reclamos** —identificador, correo, papel y expiración— y su vigencia es corta |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 |
| Reglas de negocio aplicables | [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), con su tramo de traducción acá: la respuesta **con motivo**. [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md): la cuenta marcada **se autentica y no obtiene sesión de trabajo**, que en esta superficie significa que la petición se reconoce y **no devuelve acceso**. [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), por el papel que viaja en el acceso |
| Reglas de arquitectura del producto | **RA-01**: el único invocante legítimo es la pieza pública, servidor a servidor. **RA-03**: ninguna respuesta expone direcciones de servicios internos, y el intento queda registrado del lado del servidor |
| Punto de acceso | **A-01**, `POST /auth/token`, **la única ruta que declara una fuente** |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-01` |
| Historias de usuario a generar en 06 | US-01, US-02, US-03 |
| Componentes esperados en 05 | Punto de acceso de canje; conexión con la consulta de admisibilidad, con el mecanismo de credenciales y con el emisor del acceso |
| Tests previstos en 08 | Integración contra el servicio real por los nueve criterios; y una inspección de que ninguna respuesta ni traza contiene la contraseña recibida |

## 10. Notas y supuestos

- **El riesgo del tramo en claro está aceptado por escrito**, no omitido: el intake §11 RN-B5 y §17.5.P.5 lo declaran, con el túnel saliente como salida documentada y **no adoptada**. Este contrato no lo mitiga y no pretende hacerlo; lo que sí hace es no agravarlo, no registrando nunca la contraseña recibida.
- **La vigencia exacta del acceso no está declarada por ninguna fuente.** El intake dice «corta» y «sin token de refresco». Es punto abierto heredado y **esta categoría no lo reabre**.
- **Este punto no aplica la guardia de `CU-02`**, y es correcto: la guardia verifica un acceso, y acá todavía no hay ninguno. Lo que sí hace es **rechazar por sí mismo** a la cuenta marcada, que es el único caso en que las dos cosas se parecen.
- **Dos canjes seguidos producen dos accesos válidos.** No hay revocación en este alcance: lo que acota el daño de un acceso filtrado es su vigencia corta, y por eso el punto abierto de la vigencia no es cosmético.
- **La respuesta de sesión no lleva ningún indicador de contraseña pendiente ni de situación de cuenta.** El ensamblado de contratos lo declara como restricción transversal: las tres condiciones que impiden operar viajan como respuesta de error con código propio, y esta superficie las convierte en `403` con motivo.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04.** Habilitar produce la contraseña provisoria, con lo cual **el primer ingreso deja de tener camino y código propios** y recorre el del cambio obligatorio. **§6**: `CONTRATO_CONTRASENA_NO_ESTABLECIDA` queda **retirado** del conjunto cerrado —su causa dejó de ser posible— y se conserva como fila tachada para que una cita vieja no quede sin respuesta; la fila del código de cambio requerido declara sus **dos orígenes**. **§8**: **CA-04** se rehace sobre la cuenta recién habilitada, que recibe el **mismo** código que la reseteada. La cabecera cita el intake **1.13**. **El punto de acceso A-01, sus reclamos y su respuesta no cambian.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra los hallazgos `B-API-07` (P2), `B-API-08` (P2) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§6**, fila tachada de `CONTRATO_CONTRASENA_NO_ESTABLECIDA`: se quita el fragmento colgado «~a **A-04**», resto de un reemplazo de 1.1 con marca de tachado mal cerrada, que dejaba **viva una referencia en presente a un punto de acceso retirado**; el enunciado de la celda termina donde termina. **§6**, prosa posterior a la tabla: «Ninguna de las seis» pasa a «**Ninguna de las cinco vivas**» —cinco filas vivas más una tachada, contadas— y «tres motivos distintos comparten el `403`» pasa a **dos**, con la enumeración sin «establecer su contraseña», que es el camino que **RN-16** suprimió y que este mismo control de cambios declaró suprimido en 1.1. **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26**, vigente hoy. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**: `A-04` sobrevivía en **dos** celdas mutiladas, ésta y la de `CU-05` §6, y las dos se corrigen en la misma tanda; fuera de ellas, las únicas menciones vivas de `A-04` son las que **declaran su retiro** (`Definicion-Superficie-HTTP.md` §3 y §9, `CU-02` §5 y las filas de control de cambios), que son correctas y no se tocan. **Ningún código, ninguna respuesta y ningún criterio de aceptación cambia.** Sube minor. |
