# Definición de la superficie HTTP

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Definicion-Superficie-HTTP.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14** §17.5.P.3 (protocolo, consumidor, ausencia de CORS y de WebSockets, punto de canje y punto de salud), §17.5.P.5 (flujo, reclamos, respuestas, autorización, secretos), §17.5.P.2, §17.5.P.6, §17.5.P.9, §17.5.P.10, §17.5.P.11, §9 (X-9), §14 (**RA-01, RA-02, RA-03**), §4.1 (RN-03, RN-06, RN-09, RN-13, **RN-16**), §4 (**F-04** precisada, F-03); `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/` completo, y en particular la §6 de sus ocho contratos de uso y `CU-06` §6 y §10, de donde sale el **conjunto cerrado de quince códigos**; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §4 y §6; `Proyectos/GeometriaFactory-Infrastructure/03-UX-UI-DX/DX-Error-Messages.md` §1.3 y §2.3, que declara que la traducción de sus condiciones hacia afuera del proceso pertenece a este proyecto de código
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Por qué este documento existe](#1-por-qué-este-documento-existe)
- [2. Qué declara una fuente y qué es derivación de esta categoría](#2-qué-declara-una-fuente-y-qué-es-derivación-de-esta-categoría)
- [3. Los quince puntos de acceso](#3-los-quince-puntos-de-acceso)
- [4. Los diez códigos de respuesta](#4-los-diez-códigos-de-respuesta)
- [5. Las dos traducciones](#5-las-dos-traducciones)
- [6. La tabla de traducción de los quince códigos](#6-la-tabla-de-traducción-de-los-quince-códigos)
- [7. Lo que esta superficie no tiene, y por qué](#7-lo-que-esta-superficie-no-tiene-y-por-qué)
- [8. Lo que ninguna respuesta de esta superficie puede decir](#8-lo-que-ninguna-respuesta-de-esta-superficie-puede-decir)
- [9. Puntos abiertos de esta superficie](#9-puntos-abiertos-de-esta-superficie)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Por qué este documento existe

Los doce casos de uso de esta categoría describen contratos de uso, uno por uno. Ninguno de ellos, ni todos juntos leídos en orden, permiten responder la pregunta que hay que poder responder antes de escribir la primera línea de este proyecto de código: **qué existe hacia afuera y qué no**.

Esa pregunta importa acá más que en las tres capas que este proyecto de código ensambla, por tres motivos que se acumulan:

1. **Lo que no está acá, no existe para nadie.** El intake §17.5.P.9 declara que un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio. Una capacidad implementada en las tres capas de adentro y no expuesta acá es una capacidad que el producto no tiene.
2. **Lo que está acá de más, no lo protege nadie.** Cada punto de acceso agrega una superficie donde la guardia puede faltar y donde una regla puede romperse. La lista completa es la única forma de comprobar que **los once puntos que exigen acceso están los once bajo la misma guardia**.
3. **Acá se elige el número, y el número dice cosas.** Responder «no autorizado» donde la regla exige «no encontrado» confirma la existencia de un recurso ajeno, y ninguna capa de adentro puede repararlo. RN-03 se rompe eligiendo mal un código de respuesta.

Este documento es, entonces, el mapa de la frontera. Lo que declara está gobernado por una regla que atraviesa las tres secciones: **no se inventa nada, y lo que esta categoría deriva va rotulado como derivado**.

## 2. Qué declara una fuente y qué es derivación de esta categoría

Es la sección que hay que leer antes que la tabla de §3, porque de otro modo esa tabla se lee como si toda ella viniera de una decisión ya tomada, y no es así.

**Lo que una fuente declara sobre esta superficie, y es todo lo que declara:**

| Qué | Dónde |
| --- | --- |
| El protocolo es petición-respuesta, **sin estado**, con el acceso firmado en la cabecera de autorización | Intake §17.5.P.3 |
| El formato es JSON, con los tipos de `GeometriaFactory-Contracts` | Intake §17.5.P.3 |
| **El único consumidor es `GeometriaFactory-Web`, servidor a servidor**; el navegador nunca la alcanza | Intake §17.5.P.3, RA-01 |
| Existe un **punto de canje de credenciales**, con correo y contraseña, y su ruta está declarada: `POST /auth/token` | Intake §17.5.P.3 y §17.5.P.5 |
| Existe un **punto de salud**, consumido por la página de salud del front y por la comprobación del despliegue. **La fuente no da su ruta** | Intake §17.5.P.3 |
| Ante credenciales inválidas se responde **`401` genérico, sin revelar cuál campo falló** | Intake §17.5.P.5 |
| Ante cuenta `Pendiente` o `Bloqueada` se responde **`403` con motivo** | Intake §17.5.P.5 |
| La autorización es **por papel en cada punto más verificación de pertenencia**; el papel no alcanza | Intake §17.5.P.5 |
| **No hay versionado de rutas**, porque no hay clientes de terceros | Intake §17.5.P.3 |
| **No hay CORS y no hay WebSockets** | Intake §17.5.P.3 |
| Un trabajo que no está en `Borrador` o que no pertenece al solicitante **se verifica forzando la petición contra esta superficie** | Intake §17.5.P.6 |

**Lo que es derivación de esta categoría**, y va rotulado fila por fila en las tablas que siguen: **las rutas y los verbos de los catorce puntos restantes**, **la partición de la superficie en quince puntos**, y **ocho de los diez códigos de respuesta** con su asignación. Nada de eso lo declara ninguna fuente, y su forma definitiva la fija `05-Arquitectura-Tecnica` y se valida en el punto de control de la primera etapa.

**Un caso intermedio, que conviene no contar como derivación pura:** el `404`. El número no lo declara ninguna fuente, pero **lo que tiene que decir sí está declarado por escrito**: RN-03 exige que el trabajo ajeno responda «no encontrado» y **nunca** «no autorizado», y la capa de aplicación declara que quien traduce su motivo a «no encontrado» es el consumidor, que es este proyecto de código. La elección del número es derivada; **la obligación que satisface no lo es**.

## 3. Los quince puntos de acceso

**Las rutas de las catorce filas que no son el canje son una propuesta derivada de esta categoría**, no una decisión tomada aguas arriba, y se marcan con el rótulo **[derivado]** en la columna de ruta. La columna de intención y la de códigos de respuesta **no son derivadas en la misma medida**: la intención sale de los ocho contratos de uso del ensamblado y de los once casos de uso de la capa de aplicación, y los códigos siguen la tabla de §6.

**Papel exigido** es lo que este punto comprueba sobre el acceso firmado; **no es la autorización completa**, que se hace sobre el dato recuperado y es de la capa de aplicación.

| Id | Intención | Verbo | Ruta | Papel exigido | Códigos de respuesta | CU |
| --- | --- | --- | --- | --- | --- | --- |
| A-01 | Canjear correo y contraseña por un acceso firmado | `POST` | `/auth/token` **[declarada por la fuente]** | Ninguno | `200`, `400`, `401`, `403` | CU-01 |
| A-02 | Registrar una cuenta de alumno, sin campo de contraseña | `POST` | `/cuentas` **[derivado]** | Ninguno | `201`, `400`, `409` | CU-03 |
| A-03 | Configurar la cuenta de administrador, sólo mientras no exista ninguna | `POST` | `/cuentas/administrador` **[derivado]** | Ninguno | `201`, `400`, `409` | CU-03 |
| A-05 | Cambiar la contraseña propia exigiendo la vigente | `POST` | `/cuenta/contrasena` **[derivado]** | `Alumno` o `Administrador` | `200`, `400`, `401` | CU-03 |
| A-06 | Listar las cuentas de la comisión con su situación y su marca | `GET` | `/cuentas` **[derivado]** | `Administrador` | `200`, `401`, `403` | CU-04 |
| A-07 | Cambiar la situación de una cuenta: habilitar, bloquear, rehabilitar. **Habilitar y rehabilitar devuelven la contraseña provisoria** (RN-16) | `POST` | `/cuentas/{id}/situacion` **[derivado]** | `Administrador` | `200`, `400`, `401`, `403`, `404` | CU-04 |
| A-08 | Dar de baja una cuenta, con el correo escrito como confirmación | `DELETE` | `/cuentas/{id}` **[derivado]** | `Administrador` | `204`, `400`, `401`, `403`, `404` | CU-04 |
| A-09 | Resetear la contraseña de un alumno y devolver la provisoria | `POST` | `/cuentas/{id}/reseteo-de-contrasena` **[derivado]** | `Administrador` | `200`, `400`, `401`, `403`, `404`, `409` | CU-05 |
| A-10 | Enviar un trabajo nuevo | `POST` | `/trabajos` **[derivado]** | `Alumno` | `201`, `400`, `401`, `403` | CU-06 |
| A-11 | Reenviar un trabajo que quedó en `Borrador` | `POST` | `/trabajos/{id}` **[derivado]** | `Alumno` | `200`, `400`, `401`, `403`, `404`, `409` | CU-06 |
| A-12 | Eliminar un trabajo, con los dos alcances | `DELETE` | `/trabajos/{id}` **[derivado]** | `Alumno` o `Administrador` | `204`, `401`, `403`, `404`, `409` | CU-06 |
| A-13 | Listar trabajos, con el alcance que el papel determina | `GET` | `/trabajos` **[derivado]** | `Alumno` o `Administrador` | `200`, `401`, `403`, `404` | CU-07 |
| A-14 | Obtener el detalle de un trabajo interpretado | `GET` | `/trabajos/{id}` **[derivado]** | `Alumno` o `Administrador` | `200`, `401`, `403`, `404` | CU-07 |
| A-15 | Aprobar o rechazar un trabajo en estado `Pendiente` | `POST` | `/trabajos/{id}/desenlace` **[derivado]** | `Administrador` | `200`, `400`, `401`, `403`, `404`, `409` | CU-08 |
| A-16 | Responder por el estado del servicio | `GET` | `/salud` **[derivado; la fuente declara el punto y no su ruta]** | Ninguno | `200`, `503` | CU-11 |

**Quince puntos de acceso.** El recuento que importa y que se puede comprobar sobre la tabla: **cuatro no exigen acceso firmado** —A-01, A-02, A-03 y A-16— y **los once restantes exigen acceso firmado y quedan bajo la guardia de `CU-02`**. Cuatro más once son quince. **Ningún punto queda con su forma de identificación abierta**, y ésa es la diferencia con la emisión 1.0.

**El punto A-04 quedó retirado por `PRODUCT-INTAKE` 1.13, y la capacidad que exponía no.** Establecía la contraseña propia en el primer ingreso y era **el único punto de la superficie que escribía sin credencial**: por eso su identidad era el primer punto abierto de §9. **RN-16** hace que habilitar produzca la contraseña provisoria, con lo cual el alumno llega a elegir la suya **ya autenticado**, por **A-05** —el mismo punto que usa el cambio posterior a un reseteo y el cambio voluntario—. La capacidad F-04 se sigue ejerciendo; lo que desapareció es el punto anónimo que la ejercía. **El identificador `A-04` queda retirado y no se recicla**, para que una cita vieja no resuelva en silencio a otro punto.

**Y con él desaparece la clase entera de «escritura sin credencial».** De los cuatro puntos que no exigen acceso firmado, **ninguno fija una contraseña sobre una cuenta existente**: A-01 canja credenciales, A-02 registra una cuenta sin contraseña, A-03 sólo procede mientras no exista administrador y A-16 es de sólo lectura. Es el enunciado de RN-16 visto desde la superficie, y §7 lo declara como ausencia sostenida.

**Por qué la eliminación es un solo punto y no dos.** Porque el ensamblado de contratos declara **la misma solicitud** para el alumno y para el administrador: lo que cambia no es el tipo sino la regla que lo acota, y las reglas viven en el dominio. Dos puntos de acceso habrían declarado la misma superficie dos veces y agregado un lugar donde el papel puede filtrarse en la ruta.

**Por qué el envío son dos puntos y no uno.** Porque el trabajo nuevo no trae identificador y el reenvío sí, y esa diferencia decide qué puede fallar: un reenvío puede referirse a un trabajo que no existe o que ya no está en `Borrador`, y un alta no. Comparten el tipo de solicitud y no comparten su tabla de respuestas.

## 4. Los diez códigos de respuesta

| Código | Qué significa en esta superficie | Origen |
| --- | --- | --- |
| `200` | La operación se resolvió y hay un cuerpo con el tipo de resultado del contrato | **[derivado]** |
| `201` | Se constituyó algo que antes no existía: una cuenta o un trabajo | **[derivado]** |
| `204` | Se retiró algo y no hay cuerpo que devolver | **[derivado]** |
| `400` | La petición no es utilizable: falta un campo que el contrato exige, o el que llegó no es del conjunto cerrado que declara | **[derivado]** |
| `401` | **Ante credenciales inválidas, genérico y sin declarar cuál campo falló.** También ante la ausencia de acceso, el acceso vencido y la firma que no corresponde | **Declarado** por el intake §17.5.P.5, ampliado por derivación a los tres casos de la guardia |
| `403` | **Con motivo**, ante la cuenta que no admite acceso, ante el papel que el punto no admite y ante la cuenta con cambio de contraseña pendiente | **Declarado** por el intake §17.5.P.5 para la cuenta `Pendiente` o `Bloqueada`; los otros dos son derivación |
| `404` | Lo pedido no existe **o no es del solicitante, o está fuera de lo que ve**, sin que la respuesta permita distinguir los tres casos | **[derivado en el número; la obligación es de RN-03]** |
| `409` | La operación es legítima y el estado no la admite: el correo ocupado, el administrador ya configurado, el estado que no admite desenlace, el que no admite eliminar, el reseteo que no aplica | **[derivado]** |
| `500` | Un defecto que el producto no previó. **Nunca lleva detalle de implementación** | **[derivado]** |
| `503` | El servicio no puede atender: el almacén no está disponible, o el arranque todavía no dejó el almacén en condiciones | **[derivado]** |

**Diez códigos.** Dos son de la fuente y ocho son derivación, con el matiz declarado del `404` en §2.

**Dos códigos que esta superficie no usa, y su ausencia es informativa.** No hay `422`: el conjunto de causas que otro producto pondría ahí —un texto del alumno que no verifica— **no es un fallo en éste**, y §5 lo explica. Y no hay `429`: ninguna fuente declara límite de caudal, el caudal previsto es de una comisión durante una clase, y agregarlo sería una decisión que nadie tomó.

## 5. Las dos traducciones

Una petición que falla atraviesa **dos** traducciones antes de convertirse en una respuesta, y confundirlas es el defecto característico de esta capa.

```text
motivo de la capa de aplicación  →  código del contrato  →  código de respuesta
        (o condición del                (conjunto cerrado          (los diez de §4)
         adaptador)                      de quince)
```

| Traducción | Quién la hace | Qué la gobierna |
| --- | --- | --- |
| De motivo interno a **código del contrato** | Esta capa | El conjunto cerrado de quince códigos del ensamblado. **Esta capa no agrega ninguno**: si un motivo no tiene código, el que corresponde es el genérico, y el hueco se declara en §9 en lugar de inventarse uno |
| De código del contrato a **código de respuesta** | Esta capa | La tabla de §6, entera y sin excepciones |

**Dos respuestas de esta superficie no llevan ningún código del contrato, y su ausencia es deliberada.** El `401` de la guardia —acceso ausente, vencido o con firma que no corresponde— y el `400` de una petición que **no llega a ser el tipo del contrato** —un valor fuera de un conjunto cerrado, un cuerpo que no se puede leer— ocurren **antes** de que haya un contrato con el que hablar. El conjunto cerrado no declara códigos para ninguna de las dos, y **esta capa no inventa códigos**: lo que viaja es el código de respuesta, que es lo que la pieza pública necesita.

**Y una cosa que no es una traducción y se confunde con una: el resultado de un envío cuyo texto no verifica.** El ensamblado de contratos lo declara **señal y no error**: el envío procede, el resultado trae el estado `Borrador`, el texto conservado íntegro y las observaciones con su índice de figura y su campo. **En esta superficie eso es una respuesta exitosa.** Si un envío con el texto de `E-5` o de `E-8` respondiera con un código de fallo, el producto le estaría diciendo a la persona que su petición estaba mal cuando lo que pasa es que su programa emitió algo que no se puede interpretar —y el trabajo, mientras tanto, quedó guardado—.

Lo mismo vale para la otra señal declarada del ensamblado: **un listado sin elementos es una respuesta exitosa con una colección vacía**, y la pieza pública distingue vacío de fallo por el tipo recibido y no por el conteo.

**Dos señales, y ninguna de las dos tiene código de respuesta de fallo.**

## 6. La tabla de traducción de los quince códigos

El conjunto cerrado lo declara `GeometriaFactory-Contracts` `CU-06` §10 y es de **quince** códigos, unión de los que declaran sus ocho contratos de uso. Eran diecisiete hasta el `PRODUCT-INTAKE` 1.12; **RN-16** retiró dos, y §9 declara qué punto abierto cerró ese mismo retiro. **Ninguno se agrega, ninguno se renombra y ninguno se traduce a texto acá.**

| Código del contrato | Código de respuesta | Fundamento |
| --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | La petición no es utilizable, y la respuesta **nombra el campo ausente** sin agregar nada más |
| `CONTRATO_CREDENCIAL_INVALIDA` | `401` | **Declarado por la fuente.** Genérico: la respuesta no declara cuál de los dos campos falló |
| `CONTRATO_CUENTA_NO_HABILITADA` | `403` | **Declarado por la fuente**, con motivo, para que la persona sepa en qué situación está su cuenta |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | `403` | Con motivo. **Es un solo código para todas las operaciones bloqueadas y para los dos orígenes de la marca** —la habilitación de RN-16 y el reseteo de F-26—, y acá es un solo código de respuesta para todas. La pieza pública lo convierte en el desvío al cambio de contraseña, que desde el intake 1.13 es también el desvío del **primer ingreso** |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | `404` | **RN-03.** Cubre el inexistente, el ajeno y el que está fuera de lo que el solicitante ve, y las tres respuestas son indistinguibles |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | `404` | El filtro por alumno referencia un identificador que no existe. **Y, por adopción declarada de esta categoría, la cuenta que un punto de administración referencia y no existe**: es la misma situación desde otro punto de acceso, y la ampliación de causa se declara en `CU-04` §10 en lugar de darse por prevista |
| `CONTRATO_CORREO_YA_REGISTRADO` | `409` | El estado del conjunto no admite la operación. La respuesta **no declara la situación ni el papel** de la cuenta que ocupa el correo |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | `409` | Ídem, y **el contrato no ofrece camino alternativo**: la respuesta no sugiere ninguno |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | `400` | Es un campo de la petición que no cumple lo que el contrato le pide, no un estado que impida la operación. **La respuesta no devuelve el correo esperado** |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | `409` | El estado del trabajo no habilita al solicitante. La respuesta **declara el estado actual**, que es lo que el contrato ya transporta |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | `409` | Ídem, **incluido el estado terminal**, y la respuesta no sugiere ninguna forma de revertirlo |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `403` | Es una negativa de facultad y **no tiene nada que ocultar**: no hay recurso ajeno cuya existencia proteger, porque el trabajo puede ser propio |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | `409` | El sujeto del reseteo no lo admite. **No es `403`**: quien pide tiene la facultad, y lo que no procede es la operación sobre esa cuenta |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `403`, `409`, `500` o `503` | **Es el único código con más de un destino, y la distinción es de esta categoría**: `403` cuando el papel del acceso no alcanza **en uno de los tres caminos que no tienen código de facultad propio**; `409` cuando se fuerza un reenvío sobre un trabajo que no está en `Borrador`, **camino que tampoco tiene código propio**; `503` cuando la causa es una terminación degradada del almacén, que no depende de lo que se pidió y puede resolverse sola; `500` cuando es un defecto no previsto. Los cuatro llevan el mismo código de contrato porque el conjunto cerrado no tiene otro, y §9 lo declara |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | **Ninguno** | **No lo produce esta capa.** El ensamblado de contratos lo declara «el único que el contrato admite que produzca la propia pieza pública, porque describe la ausencia de respuesta de la otra pieza». Una respuesta de esta superficie con este código sería una contradicción: si hay respuesta, el servicio respondió |

**Quince códigos: catorce con destino en esta superficie y uno sin él.** Uno de los catorce —el genérico— tiene **cuatro** destinos según la causa, y eso no es una comodidad: es el **síntoma medible de los dos huecos** que §9 declara —dos de sus cuatro destinos existen sólo porque el conjunto cerrado no tiene un código propio para esos caminos—. Donde el conjunto cerrado no tiene un código propio, el genérico es lo único que queda, y un código genérico que cubre tres situaciones distintas es exactamente lo que el producto evita en todos los demás lugares. **Quince filas para quince códigos, ninguna excedente.**

**Las dos señales declaradas del ensamblado —el texto que no verifica y el listado vacío— no están en esta tabla**, y no es un olvido: no son códigos de error y viajan en respuestas exitosas, como declara §5.

## 7. Lo que esta superficie no tiene, y por qué

Cada ausencia es una decisión declarada de una fuente, no un pendiente. Se enumeran porque **una superficie se audita también por lo que no expone**, y porque cuatro de estas **siete** se reintroducen fácil por comodidad. La séptima no se reintroduce por comodidad sino por inercia de diseño, y es la más cara de las siete.

| Ausencia | Por qué | Qué la repone si se rompe |
| --- | --- | --- |
| **CORS** | La superficie no recibe peticiones del navegador: RA-01. Configurar CORS sería declarar que sí las recibe | Agregarlo reabre las tres propiedades de la topología: contenido mixto, CORS y exposición de la dirección del servidor propio |
| **WebSockets** | El circuito de la pieza pública **termina en el front** y no llega hasta acá. El intake lo declara criterio de aceptación de la primera etapa | Un punto de acceso que sostenga una conexión abierta rompe además el «sin estado» de §17.5.P.3 |
| **Pasarela de reenvío en el front** | El intake §9 X-9 la declara **especificada y no implementada**, porque hoy ningún JavaScript del navegador toca esta superficie y la pasarela sólo consumiría el recurso más escaso del plan gratuito. Queda especificada para adoptarla sin rediseño | Su condición de reingreso está declarada: descarga de archivos, carga directa desde el navegador o migración del front a ejecución en el navegador |
| **Versionado de rutas** | No hay clientes de terceros: los dos extremos compilan contra el mismo ensamblado y un cambio incompatible **rompe la compilación** antes que el tiempo de ejecución | La regla operativa que lo reemplaza es el **despliegue conjunto** de las dos piezas desplegables ante un cambio de contrato |
| **Sesión del lado del servidor** | REST sin estado. Lo que se parece a una sesión vive en el circuito de la pieza pública, donde reside el acceso firmado | Un punto de acceso que dependa de la petición anterior obliga a afinidad de servidor, que este despliegue no tiene |
| **Acceso de refresco** | El intake §17.5.P.5 declara vigencia corta y **renovación por reingreso**, sin acceso de refresco en este alcance | Agregarlo es una decisión de alcance, no una comodidad: cambia qué pasa cuando el acceso vence |
| **Cualquier punto que fije una contraseña sobre una cuenta existente sin credencial** | **RN-16** (`PRODUCT-INTAKE` 1.13 §4.1): habilitar produce la contraseña provisoria, de modo que toda operación que fija una contraseña ocurre con la cuenta ya autenticada. La ausencia es lo que impide que alguien que conozca un correo habilitado le fije la contraseña a esa cuenta antes que su dueño | Reponerlo devuelve exactamente ese agujero. La condición de reingreso **no existe**: no es un pendiente de alcance sino una decisión de seguridad del Product Owner |

## 8. Lo que ninguna respuesta de esta superficie puede decir

Es RA-03, que es regla de nivel producto, y **acá es donde se puede violar hacia afuera**: es la última vez que un dato del backend es tocado antes de salir del servidor propio.

| Nunca aparece en una respuesta | Por qué | Qué corresponde |
| --- | --- | --- |
| La **dirección de un servicio interno**, en cualquier forma | Es el enunciado literal de RA-03 | El motivo, sin origen |
| La **ruta del archivo del almacén** | Es una dirección de servicio interno a los efectos de RA-03, y así lo trata la capa que la conoce | «El servicio no puede atender» |
| La **clave de firma**, ni una parte de ella | No entra al repositorio de código, no entra a la imagen y no entra a una respuesta | «No hay acceso válido» |
| La **contraseña en claro** ni el valor derivado de una credencial | Ninguno de los dos cruza la frontera: el ensamblado de contratos lo declara como restricción transversal | La respuesta genérica de credenciales inválidas |
| La **contraseña provisoria**, fuera del cuerpo del resultado del reseteo | Se devuelve **una vez**, a quien la pidió, y **no se registra en ninguna traza** | Nada: el valor viaja en el resultado y no en ningún otro lado |
| **Trazas de la implementación**, nombres de tipos internos o cadenas de llamada | Es lo que un defecto no previsto expone si no se lo maneja | El código genérico, con su código de respuesta |

**Y la contracara, que es igual de obligatoria:** el intake §17.5.P.10 declara registro estructurado del lado del servidor **de cada error y de cada intento de acceso rechazado**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar, y el operador que despliega a mano se queda sin nada que mirar.

## 9. Puntos abiertos de esta superficie

Los **cinco** primeros son propios de este documento y están recogidos en el índice maestro; el sexto se hereda. **Ninguno es bloqueante y ninguno se resuelve acá.** Eran siete en la emisión 1.0, y el que encabezaba la lista **quedó cerrado**.

**Cerrado: la identidad en el establecimiento de la contraseña (A-04).** Era el primer punto abierto de este documento y el más importante de la categoría: A-04 era la única escritura de la superficie que ocurría sin acceso firmado, y ninguna fuente declaraba cómo viajaba la identidad en esa operación. **Lo resolvió el Product Owner en `PRODUCT-INTAKE` 1.13 §4.1, con la regla RN-16**: habilitar una cuenta produce una contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo, y el alumno la cambia por el camino que RN-13 ya fija. La salida no fue ninguna de las dos que esta categoría había anticipado —ni el punto anónimo con prueba de posesión, ni el acceso de alcance acotado— sino **suprimir la operación anónima**: A-04 se retira y su capacidad se ejerce por A-05, bajo la misma guardia que los demás. La fila de control de cambios del intake registra que fue la emisión de este proyecto de código la que levantó el hueco.

1. **El código del contrato para una operación de administrador pedida por quien no lo es**, fuera del desenlace. Verificado recorriendo la §6 de los ocho contratos de uso: el único código de facultad del conjunto cerrado está acotado al desenlace, y el gobierno de cuentas, el reseteo y el listado de la comisión no tienen ninguno.
2. **El código del contrato para un envío o una reedición forzados fuera de `Borrador`.** El conjunto cerrado tiene el código análogo **acotado a la eliminación**.
3. **Las rutas y los verbos definitivos**, que en §3 son propuesta derivada salvo el canje.
4. **La distinción entre `500` y `503` bajo un único código de contrato**, que §6 adopta y declara. Si el Product Owner quisiera que la pieza pública pudiera distinguirlas por el contrato y no por el número, haría falta un código nuevo en el conjunto cerrado, que es decisión del ensamblado de contratos.
5. **El límite de tamaño del cuerpo de una petición.** Ninguna fuente lo declara. Es el mismo hueco que la capa que interpreta el texto declaró abierto para el texto, y acá reaparece con una consecuencia propia: **un límite mal elegido trunca el texto de un alumno y rompe RN-08 sin que nada falle**.
6. **La vigencia exacta del acceso firmado**, declarada «corta» y sin número por el intake, y ya declarada abierta por `GeometriaFactory-Infrastructure`. **No se reabre acá.**

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**, que **cierran el primer punto abierto de este documento**: la identidad en el establecimiento de la contraseña. La salida del Product Owner no fue ninguna de las dos que §9 anticipaba, sino suprimir la operación anónima. **§3**: se **retira el punto de acceso A-04**, los puntos pasan de dieciséis a **quince** y el recuento pasa a **cuatro sin acceso firmado más once bajo la guardia**, sin ninguno con identidad abierta; se declara que el identificador **no se recicla** y que con A-04 desaparece la clase entera de escritura sin credencial, comprobable sobre los cuatro puntos que no exigen acceso; **A-07** pasa a declarar que habilitar y rehabilitar devuelven la contraseña provisoria. **§2**: los recuentos de derivación pasan a catorce rutas y quince puntos. **§6**: el conjunto cerrado del ensamblado pasa de diecisiete a **quince** códigos —salen `CONTRATO_CONTRASENA_NO_ESTABLECIDA` y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA`, los dos por imposibilidad de su causa— y el recuento pasa a **catorce con destino y uno sin él**; la fila del código de cambio requerido declara sus **dos orígenes**. **§7**: entra la **séptima ausencia declarada**, la de todo punto que fije una contraseña sobre una cuenta existente sin credencial, con la constancia de que su condición de reingreso **no existe**. **§9**: el punto abierto 1 pasa a la prosa de cerrados con su resolución, y los puntos abiertos pasan de siete a **seis**, cinco propios y uno heredado. La cabecera cita el intake **1.13**. Sube minor. |
| 1.0 | 2026-08-10 | Emisión inicial. Declara por qué la superficie necesita un mapa completo y no doce contratos sueltos; **qué declara una fuente y qué es derivación de esta categoría**, con las once cosas declaradas y el matiz del `404`, cuyo número es derivado y cuya obligación no lo es; los **dieciséis puntos de acceso** con su verbo, su ruta propuesta rotulada, su papel exigido y sus códigos, y el recuento de cuatro sin acceso, uno con identidad abierta y once bajo la guardia; los **diez códigos de respuesta**, dos de la fuente y ocho derivados, con las dos ausencias informativas; **las dos traducciones** y la señal que no es un fallo; la **tabla de traducción de los diecisiete códigos del contrato**, con dieciséis destinos y uno sin destino declarado, y el único código con dos destinos; las **seis ausencias declaradas** de la superficie con lo que las repone; la prohibición de RA-03 con su contracara de registro; y los **siete puntos abiertos** de la superficie, seis propios y uno heredado. |
| 1.2 | 2026-08-10 | **Cierra el hallazgo `C-05` (P1) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** El **diagrama de §5**, bloque `text`, rotulaba el conjunto cerrado del contrato como «conjunto cerrado de **diecisiete**»: es el tamaño que tenía antes de que **RN-16** unificara los dos mecanismos de credencial inicial y retirara dos códigos. Pasa a **quince**, que es lo que declaran la tabla que está inmediatamente debajo en el mismo §5, el título y el cuerpo de **§6** —«Quince códigos: catorce con destino en esta superficie y uno sin él», recontado fila por fila sobre la tabla de traducción— y `Contracts/CU-06` §10, que es el dueño del conjunto. Era el único lugar del documento donde sobrevivía el número anterior. La cabecera pasa a citar el intake **1.14**. **Ningún punto de acceso, ningún código de respuesta y ninguna fila de la tabla de traducción cambia.** Sube minor. |
