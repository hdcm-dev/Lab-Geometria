# Catálogo de respuestas de fallo de la superficie HTTP

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** DX-Error-Messages.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** §6 de los **doce** casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/` (CU-00001 a CU-00012), con sus §3, §5, §7, §8, §9 y §10; `02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3, §4, §5, §6 y §8; `02-Especificacion-Funcional/Especificacion-Funcional.md` §4 (**la frontera y sus seis precisiones**), §6 y §11; `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §3; `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-00006-Contrato-De-Respuesta-De-Error.md` §6 y §10, que declara el **conjunto cerrado de diecisiete códigos**, y la §6 de sus otros siete contratos de uso; RN-00001 a RN-00016 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; `Proyectos/GeometriaFactory-Infrastructure/03-UX-UI-DX/DX-Error-Messages.md` §1.3 y §2.3; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §14 (RA-03), §17.5.P.5 y §17.5.P.10
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Principios de redacción](#1-principios-de-redacción)
  - [1.1 Qué pasó, por qué pasó, qué hacer](#11-qué-pasó-por-qué-pasó-qué-hacer)
  - [1.2 Dos resultados que no son fallos](#12-dos-resultados-que-no-son-fallos)
  - [1.3 Qué emite esta capa y qué compone el consumidor](#13-qué-emite-esta-capa-y-qué-compone-el-consumidor)
  - [1.4 Lo que ninguna respuesta puede decir](#14-lo-que-ninguna-respuesta-puede-decir)
  - [1.5 Las tres familias deliberadamente empobrecidas](#15-las-tres-familias-deliberadamente-empobrecidas)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Las categorías en uso](#21-las-categorías-en-uso)
  - [2.2 Las dos respuestas sin código del contrato](#22-las-dos-respuestas-sin-código-del-contrato)
  - [2.3 El código sin destino, y por qué](#23-el-código-sin-destino-y-por-qué)
  - [2.4 Los dos huecos del conjunto cerrado, cerrados](#24-los-dos-huecos-del-conjunto-cerrado-cerrados)
- [3. Catálogo](#3-catálogo)
  - [3.1 Entrada inválida](#31-entrada-inválida)
  - [3.2 Credencial no admitida](#32-credencial-no-admitida)
  - [3.3 Situación de la cuenta](#33-situación-de-la-cuenta)
  - [3.4 Facultad](#34-facultad)
  - [3.5 Recurso no visible](#35-recurso-no-visible)
  - [3.6 Conflicto de estado](#36-conflicto-de-estado)
  - [3.7 No clasificado](#37-no-clasificado)
- [4. Tono y voz](#4-tono-y-voz)
- [5. Localización](#5-localización)
- [6. Cobertura y trazabilidad](#6-cobertura-y-trazabilidad)
  - [6.1 Recuento](#61-recuento)
  - [6.2 Verificación mecánica de cobertura](#62-verificación-mecánica-de-cobertura)
  - [6.3 Trazabilidad del artefacto](#63-trazabilidad-del-artefacto)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Principios de redacción

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo. La tercera tiene acá una particularidad que las capas de adentro no tienen:

> **Casi todo lo que responde esta superficie lo lee un programa, no una persona.** El consumidor es el código de la pieza pública, y lo que necesita no es un texto sino **saber qué hacer**, que es siempre una de cuatro cosas: **corregir y reintentar**, **derivar a otra pantalla**, **mostrar lo que pasó** o **pasar a estado degradado**. La columna «qué hace el consumidor» del catálogo declara cuál de las cuatro corresponde en cada caso.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe lo que no se pudo hacer, no quién se equivocó.
2. **Nada genérico donde el producto sabe ser específico.** Las tres familias del §1.5 dicen menos **a propósito**; en todas las demás, decir poco es un defecto.
3. **Ninguna respuesta revela lo que RA-03 prohíbe**, y **todas quedan registradas del lado del servidor**, junto con todo intento de acceso rechazado.
4. **Ningún código se inventa.** Los códigos son del conjunto cerrado del ensamblado de contratos; donde falta uno, se usa el genérico y **el hueco se declara** (§2.4).
5. **El código de respuesta y el código del contrato son dos cosas distintas y las dos viajan.** El primero le dice al consumidor qué clase de fallo fue; el segundo, cuál exactamente.

### 1.2 Dos resultados que no son fallos

Es la distinción que sostiene todo lo demás, y la que más se equivoca en esta capa: **lo que otro producto trataría como error, acá viaja en una respuesta exitosa.** Ninguno de los dos tiene entrada en este catálogo.

| Lo que ocurre | Por qué **no** es un fallo | Dónde está declarado |
| --- | --- | --- |
| **El texto enviado no verifica** | El envío **procede**: el trabajo se guardó, con su texto íntegro, con su estado `Borrador` y con sus observaciones **localizadas por índice de figura y campo**. El ensamblado de contratos lo declara señal y no error | `CU-00006` §5 FA-01 y CA-02, CA-03 |
| **El listado no tiene elementos** | Es una comisión sin entregas todavía. El consumidor distingue vacío de fallo **por el tipo recibido y no por el conteo** | `CU-00007` §5 FA-03 y CA-10 |

**La consecuencia más cara de confundir el primero.** Si un envío cuyo texto no verifica respondiera con un código de fallo, el producto le diría a la persona que **su petición estaba mal** cuando lo que pasó es que su programa emitió algo que no se puede interpretar —y su trabajo, mientras tanto, se guardó con el defecto ya localizado—. Vería un fallo y **no vería lo único que le sirve**. Es exactamente el problema que el producto viene a resolver, reintroducido en el último tramo.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite **un código de respuesta y un código del contrato**, más un texto neutro y la ubicación del defecto cuando la hay. **No compone mensajes para personas**: el texto que alguien lee lo arma la pieza pública, y está sujeta a la misma prohibición de §1.4.

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada situación: la base sobre la que la pieza pública compone. No es una cadena que el servicio produzca.

### 1.4 Lo que ninguna respuesta puede decir

Es RA-03, regla de nivel producto, y **acá es el único lugar donde se puede violar hacia afuera**: es la última vez que un dato del backend se toca antes de salir del servidor propio.

| Nunca aparece en una respuesta | Por qué | Qué corresponde |
| --- | --- | --- |
| La **dirección de un servicio interno**, en cualquier forma | Es el enunciado literal de RA-03 | El motivo, sin origen |
| La **ruta del archivo del almacén** | Es una dirección de servicio interno a los efectos de RA-03 | «El servicio no puede atender» |
| La **clave de firma**, ni una parte | No entra al repositorio de código, no entra a la imagen y no entra a una respuesta | «No hay acceso válido» |
| La **contraseña recibida** ni el valor derivado de una credencial | Ninguno de los dos cruza la frontera | La respuesta genérica de credenciales inválidas |
| La **contraseña provisoria**, fuera del cuerpo del resultado del reseteo | Se devuelve **una vez** y **no se registra en ninguna traza** | Nada: el valor viaja en el resultado y en ningún otro lado |
| El **texto original del alumno** dentro de un mensaje o de una traza | El texto es el trabajo de una persona y el registro del servidor no es su lugar | La posición y el campo, que es lo que la regla exige |
| **Trazas de la implementación** o nombres de tipos internos | Es lo que un defecto no previsto expone si no se lo maneja | El código genérico, con su código de respuesta |

**Y la contracara, que es igual de obligatoria:** el intake declara registro estructurado del lado del servidor **de cada error y de cada intento de acceso rechazado**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar, y el docente que despliega a mano se queda sin nada que mirar.

### 1.5 Las tres familias deliberadamente empobrecidas

Tres familias de respuestas dicen **menos de lo que el servicio sabe**, y en las tres eso es la decisión y no el defecto. Se declaran juntas porque el impulso de «mejorarlas» es el mismo en las tres.

| Familia | Qué no dice | Por qué |
| --- | --- | --- |
| **Credenciales inválidas** | No declara si falló el correo o la contraseña | Distinguirlos permitiría averiguar por tanteo **qué correos están registrados**. Lo declara el intake §17.5.P.5 |
| **Recurso que no se ve** | No distingue el inexistente, el ajeno y el que está fuera del alcance del solicitante | Es **RN-00003**. Distinguirlos permitiría averiguar por tanteo **qué identificadores existen** |
| **Correo ya registrado** | No declara la situación ni el papel de la cuenta que ocupa el correo | Misma familia: no confirmar nada sobre una cuenta que el solicitante no debería conocer |

**Una prueba las cubre a las tres, y es la misma en las tres**: comparar dos respuestas que deberían ser indistinguibles y verificar que lo son.

## 2. Taxonomía

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Entradas | Código de respuesta |
| --- | --- | --- | --- |
| **Entrada inválida** | Lo que llegó no es utilizable: falta un campo, o el que llegó no cumple lo que el contrato le pide | 3 | `400` |
| **Credencial no admitida** | La credencial presentada no habilita | 2 | `401` |
| **Situación de la cuenta** | La cuenta existe y su situación impide la operación. **Estas dos llevan motivo**, y es lo que permite derivar a la pantalla correcta | 2 | `403` |
| **Facultad** | El papel no habilita la operación | 2 | `403` |
| **Recurso no visible** | Lo pedido no existe, no es del solicitante o está fuera de lo que ve | 2 | `404` |
| **Conflicto de estado** | La operación es legítima y el estado no la admite | 6 | `409` |
| **No clasificado** | Lo que el conjunto cerrado no previó, o lo que el mundo no dejó completar | 1 | `500` o `503` |

**Dieciocho entradas**, y el reparto por código de respuesta cierra: 3 + 2 + 2 + 2 + 2 + 6 + 1 = 18.

**La categoría de conflicto de estado es la más poblada, y es la señal más clara de dónde está esta capa.** Seis de las dieciocho entradas describen operaciones **legítimas, pedidas por quien tiene derecho a pedirlas**, que el estado del sistema no admite. Es lo que le pasa a un producto con una máquina de estados que importa: la mayoría de sus negativas no son de seguridad.

### 2.2 Las dos respuestas sin código del contrato

| Respuesta | Cuándo | Por qué no lleva código |
| --- | --- | --- |
| `401` de la guardia | No hay acceso, el acceso venció, o su firma no corresponde | El conjunto cerrado **no declara ninguno** que describa un acceso ausente o inválido, y **esta capa no inventa códigos**. Lo que el consumidor necesita saber es que tiene que volver a canjear credenciales, y eso lo dice el número |
| `400` de petición ilegible | El cuerpo no se puede leer, o un valor no pertenece a un conjunto cerrado | Ocurre **antes** de que la petición llegue a ser el tipo del contrato: no hay contrato con el que hablar todavía |

**Las dos son deliberadas y se declaran para que su ausencia de código no se lea como un olvido.**

### 2.3 El código sin destino, y por qué

Del conjunto cerrado de **diecisiete** códigos vivos, **uno no tiene código de respuesta asignado y no puede tenerlo**: el que describe que la pieza de datos **no responde**. El ensamblado de contratos lo declara «el único que el contrato admite que produzca la propia pieza pública».

Una respuesta de esta superficie con ese código sería una contradicción en sus términos: **si hubo respuesta, el servicio respondió**. Lo declara `CU-00009` §10 y se repite acá para que una revisión posterior no lo levante como cobertura faltante: **son dieciséis códigos con destino sobre diecisiete, y el hueco es intencional**.

### 2.4 Los dos huecos del conjunto cerrado, **cerrados**

Estaban elevados al Product Owner y **el Product Owner los resolvió** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): el ensamblado de contratos incorporó **un código propio para cada uno**, y `GeometriaFactory-Contracts` los emite formalmente en [`../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md) §5.1. **Esta categoría no inventó ninguno**, que era la condición con la que los declaró abiertos.

| Hueco | Qué faltaba | Con qué se cerró |
| --- | --- | --- |
| **El papel no alcanza, fuera del desenlace** | El conjunto cerrado tenía **un solo** código de facultad y su enunciado estaba acotado al desenlace de la revisión. La capa de aplicación emite un motivo de facultad requerida también en **el gobierno de cuentas, el reseteo y la revisión de la comisión** | `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR`, con respuesta `403` y entrada propia en §3.4. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` **no cambia de enunciado** y sigue acotado a aprobar y a rechazar |
| **El trabajo no está en `Borrador`, fuera de la eliminación** | El código análogo del conjunto cerrado estaba acotado por su enunciado **a la eliminación y al camino del alumno**. Un envío o una reedición forzados fuera de `Borrador` no tenían dónde ir | `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`, con respuesta `409` y entrada propia en §3.6. `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambia de enunciado** y sigue acotado a la eliminación |

**El efecto medible del cierre es que el código genérico baja de cuatro destinos a dos**: le quedan `500`, para el defecto no previsto, y `503`, para la terminación degradada. Los dos destinos que se van —`403` y `409`— existían **sólo** porque el conjunto cerrado no tenía código propio para esos caminos. Que un código genérico cubra cuatro situaciones distintas era exactamente lo que el producto evita en todos los demás lugares, y por eso se declaró en vez de naturalizarse.

## 3. Catálogo

**Dieciocho entradas.** Las **dieciséis** primeras son los códigos del conjunto cerrado con destino en esta superficie; las dos últimas son las respuestas sin código de §2.2. **Ninguna se inventó y ninguna quedó afuera**; el recuento y su verificación están en §6. Eran dieciocho hasta la emisión 1.0: **RN-00016** retiró dos códigos del conjunto cerrado y ninguno los reemplaza.

### 3.1 Entrada inválida

Respuesta `400`. **Ninguna de las tres deja escritura.**

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | Falta un campo que la petición necesita | La solicitud se armó incompleta | **Corregir y reintentar.** La respuesta **nombra el campo**, y eso es lo que permite señalarlo en el formulario en vez de mostrar un cartel genérico |
| `CONTRATO_CONFIRMACION_NO_COINCIDE` | El correo escrito como confirmación no coincide con el de la cuenta | Quien da de baja escribió otro correo | **Mostrar lo que pasó y pedir de nuevo.** La respuesta **no devuelve el correo esperado**: si lo devolviera, la confirmación dejaría de confirmar nada |
| — (sin código) | La petición no se puede leer | El cuerpo está mal formado, o un valor no pertenece a un conjunto cerrado | **Corregir y reintentar.** Ver §2.2 |

### 3.2 Credencial no admitida

Respuesta `401`. **Ninguna de las dos declara más de lo que declara**, y es la primera de las tres familias empobrecidas de §1.5.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_CREDENCIAL_INVALIDA` | La credencial presentada no habilita | El correo no corresponde a ninguna cuenta, **o** la contraseña no es la de esa cuenta, **o** la contraseña vigente presentada en un cambio no corresponde | **Mostrar lo que pasó y pedir de nuevo.** El mensaje **no dice cuál de los dos campos falló**, y el consumidor **no debe inferirlo ni sugerirlo** |
| — (sin código) | No hay un acceso válido en la petición | No se presentó acceso, venció, o su firma no corresponde | **Volver a canjear credenciales.** Las tres causas responden igual, porque el trabajo que le queda al consumidor es el mismo |

### 3.3 Situación de la cuenta

Respuesta `403`, **con motivo**. Es la única familia del catálogo donde el motivo es lo importante: **de él depende a qué pantalla deriva el consumidor**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_CUENTA_NO_HABILITADA` | La cuenta todavía no fue habilitada, o está bloqueada | El administrador no la habilitó, o la bloqueó | **Mostrar la situación.** No hay nada que la persona pueda hacer sola: **depende del administrador**, y decírselo evita que siga probando |
| `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` | La cuenta tiene una contraseña provisoria sin cambiar | **El administrador la habilitó (RN-00016) o la reseteó (F-26)**: desde `PRODUCT-INTAKE` 1.13 es también el mensaje del **primer ingreso**, y por eso reemplazó al que describía la cuenta habilitada sin contraseña | **Derivar al cambio de contraseña.** **Un solo código para todas las operaciones bloqueadas**, porque el trabajo que le queda al consumidor es siempre el mismo, y por eso el mensaje **no nombra la operación que se pidió** |

### 3.4 Facultad

Respuesta `403`. **Dos entradas desde que el primer hueco de §2.4 quedó cerrado**, y entre las dos cubren el rechazo por papel completo: dentro del desenlace y fuera de él.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | Sólo el administrador resuelve un trabajo | Un alumno pidió el desenlace, **aun de un trabajo propio** | **Mostrar lo que pasó.** A diferencia de la familia del recurso no visible, acá **no hay nada que ocultar**: el trabajo puede ser del propio solicitante, y la negativa no revela nada que él no sepa |
| `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` | Esta acción es exclusiva del administrador | Un alumno pidió gobernar las cuentas de la comisión, ver el listado de la comisión o resetear la contraseña de otra cuenta | **Mostrar «no tenés permiso», y no el mensaje de fallo.** Es exactamente la distinción que el código genérico no permitía hacer: el consumidor sabe que la petición estuvo bien formada y que lo que no alcanza es el papel |

### 3.5 Recurso no visible

Respuesta `404`. Es la segunda familia empobrecida de §1.5, y **la que sostiene la regla que esta capa puede romper sola**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_TRABAJO_NO_ENCONTRADO` | El trabajo pedido no está disponible para el solicitante | **Tres causas indistinguibles**: no existe, no es suyo, o está fuera de lo que ve —el borrador que el administrador no ve— | **Mostrar lo que pasó, sin inferir cuál de las tres fue.** Un consumidor que muestre «ese trabajo es de otro alumno» rompe **RN-00003** desde el otro lado de la frontera |
| `CONTRATO_ALUMNO_NO_ENCONTRADO` | La cuenta referenciada no está disponible | El filtro por alumno de un listado, o el identificador de una operación de administración, referencia una cuenta que no existe | **Reintentar sin el filtro**, o corregir el identificador |

### 3.6 Conflicto de estado

Respuesta `409`. **Seis entradas, la categoría más poblada.** Todas describen operaciones legítimas que el estado del sistema no admite, y en todas **quien pide tiene derecho a pedir**.

| Código del contrato | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- |
| `CONTRATO_CORREO_YA_REGISTRADO` | El correo ya pertenece a una cuenta | Un registro repetido | **Mostrar lo que pasó.** El mensaje **no declara la situación ni el papel** de la cuenta que lo ocupa: tercera familia empobrecida de §1.5 |
| `CONTRATO_ADMINISTRADOR_YA_CONFIGURADO` | La instancia ya tiene su cuenta de administrador | Se intentó configurar una segunda | **Mostrar lo que pasó, sin ofrecer alternativa**: el contrato no declara ninguna, y sugerir una sería inventarla |
| `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` | El estado del trabajo no habilita al solicitante a eliminarlo | El alumno pidió eliminar un trabajo suyo que ya no está en `Borrador` | **Mostrar el estado actual**, que la respuesta declara. **No se produce nunca en el camino del administrador** |
| `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` | El trabajo ya fue entregado y no admite cambios | Se forzó un envío o una reedición sobre un trabajo en `Pendiente`, `Finalizado` o `Rechazado` | **Mostrar el estado actual y no ofrecer forma de volver a `Borrador`**: no existe. Es el segundo hueco de §2.4, cerrado |
| `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | El trabajo no está en condiciones de recibir un desenlace | O nunca estuvo en estado `Pendiente`, o **ya lo recibió y está en un estado terminal** | **Mostrar el estado actual y no ofrecer forma de revertirlo**: no existe |
| `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | La cuenta de administrador no se resetea por este camino | Se pidió el reseteo sobre ella | **Derivar al cambio de la propia contraseña**, que es el camino que sí existe |

### 3.7 No clasificado

| Código del contrato | Respuesta | Mensaje | Causa probable | Qué hace el consumidor |
| --- | --- | --- | --- | --- |
| `CONTRATO_ERROR_NO_CLASIFICADO` | `500` o `503` | Lo que ocurrió no tiene una representación propia en el contrato | **Dos causas, desde que §2.4 quedó cerrado**: una terminación degradada del almacén o de una fuente de la que el servicio depende (`503`); un defecto no previsto (`500`) | **Depende del número, y ése es el problema.** Con `503`, pasar a **estado degradado explícito**; con `500`, mostrar que algo falló; con `403` y `409`, mostrar lo que pasó sin más detalle. **Los dos primeros existen sólo por los huecos de §2.4** |

## 4. Tono y voz

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir lo que no se pudo hacer, no juzgar a quien pidió | «El trabajo pedido no está disponible para el solicitante» | «No tenés permiso para ver eso» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «El estado del trabajo no habilita al solicitante a eliminarlo» | «Operación no permitida» |
| No decir de más donde el producto decide decir de menos | «La credencial presentada no habilita» | «La contraseña es incorrecta» |
| No prometer lo que esta capa no hace | «El servicio no puede atender» | «Reintentando automáticamente» |
| No exponer secretos, rutas ni el trabajo de una persona | «El servicio no puede atender» | «No se pudo abrir el archivo en `/data/...`» |
| No confundir el dato del alumno con un fallo | El envío responde con éxito y trae sus observaciones | «El JSON enviado es inválido» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |

Dos excepciones declaradas a la regla de calificación, que no son defectos: **los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente.

## 5. Localización

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los códigos del contrato son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son del ensamblado de contratos y renombrar uno rompe la compilación de los dos extremos, que es la señal más temprana posible.
2. **El texto que una persona lee no se compone acá.** Lo arma la pieza pública, y está sujeta a la prohibición de §1.4, que no es una recomendación de estilo sino RA-03.
3. **Un solo idioma en el producto v1**: español rioplatense. **Con una excepción de hecho que conviene declarar**: el texto del alumno puede traer el separador decimal de la cultura de su máquina —una coma en lugar de un punto—, y **eso no es un problema de localización de esta capa**. Qué hace el producto con él está declarado desde el `PRODUCT-INTAKE` **1.12** §20.E-8 punto 5: es **error de validación**, con índice de figura y campo, y el trabajo **queda en `Borrador`** —de modo que, en esta superficie, **ese envío responde con éxito**—.

## 6. Cobertura y trazabilidad

### 6.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | **12** (CU-00001 a CU-00012) |
| Casos de uso **con** condiciones declaradas en su §6 | **9**. `CU-00010`, `CU-00011` y `CU-00012` no declaran ninguna respuesta de fallo de la superficie, y §6.2 explica por qué |
| Códigos del conjunto cerrado del ensamblado de contratos | **17** |
| Códigos **con** destino en esta superficie | **16** |
| Códigos **sin** destino, declarados | **1** — el que describe la ausencia de respuesta de esta pieza (§2.3) |
| Respuestas **sin** código del contrato, declaradas | **2** (§2.2) |
| **Entradas del catálogo** | **18** = 16 + 2 |
| Códigos inventados por esta categoría | **0** |
| Códigos del conjunto cerrado sin entrada en el catálogo | **0**, salvo el declarado sin destino |
| Resultados que **no** son fallos, reunidos en §1.2 | **2**, ninguno de ellos entrada de este catálogo |
| Huecos del conjunto cerrado declarados y elevados | **2**, los dos **cerrados** por `PRODUCT-INTAKE` **1.29** §17.4 P.3 (§2.4) |

Cuadre: **16 + 1 = 17** códigos del conjunto cerrado, y **16 + 2 = 18** entradas del catálogo. El conjunto cerrado vivo es el que publica la tabla de traducción de [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5, con sus **diecisiete** filas —dieciséis con destino y una sin él—, y es contra ella que este recuento cuadra.

### 6.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla.

| Categoría del catálogo | Entradas | Códigos de respuesta |
| --- | --- | --- |
| Entrada inválida | 3 | `400` |
| Credencial no admitida | 2 | `401` |
| Situación de la cuenta | 2 | `403` |
| Facultad | 2 | `403` |
| Recurso no visible | 2 | `404` |
| Conflicto de estado | 6 | `409` |
| No clasificado | 1 | `500`, `503` |
| **Total** | **18** | **Diez códigos de respuesta distintos en toda la superficie**, contando los tres de éxito —`200`, `201`, `204`— que no son de este catálogo |

Las tres comprobaciones que cierran la verificación:

- **De contrato a catálogo.** Los **17** códigos del conjunto cerrado se recorrieron uno por uno: **16** tienen entrada y **1** está declarado sin destino. **0** quedaron sin tratar.
- **De catálogo a contrato.** Las **18** entradas se recorrieron en sentido inverso: **16** citan un código que pertenece al conjunto cerrado y **2** declaran explícitamente no llevar ninguno. **0 códigos inventados**.
- **Los tres casos de uso sin entrada, y su ausencia declarada.** `CU-00010` falla **antes de que exista ninguna petición que responder** —sus dos condiciones detienen la construcción y no producen respuesta—; `CU-00011` falla **deteniendo el arranque**, que tampoco produce respuesta, y su única respuesta de fallo es la del punto de salud, que es un `503` **cubierto por la entrada de no clasificado con su código**, `CONTRATO_ERROR_NO_CLASIFICADO`, y **no** una tercera respuesta sin código: las respuestas sin código son las **dos** de §2.2; `CU-00012` **no produce condiciones, las provoca**. Los tres se declaran para que su ausencia no se lea como cobertura faltante.

### 6.3 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** Este artefacto es del modo **reference** y se consulta por código, no se recorre de principio a fin. El quick-start del proyecto de código es único, vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3 y se recorre guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md). Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | **Consumidor de la superficie**, que es quien lee este catálogo entero; implementador de la superficie; y **operador del despliegue** para las terminaciones degradadas ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las respuestas de fallo de los **quince** puntos de acceso, y las dos traducciones de `Definicion-Superficie-HTTP.md` §5 |
| CU origen | CU-00001 a CU-00012, §6 de cada uno. **CU-00010, CU-00011 y CU-00012 no declaran ninguna** |
| Reglas de negocio relevantes | RN-00001 a RN-00016. **Dos se rompen desde este catálogo**: RN-00003, si dos respuestas que deben ser indistinguibles dejan de serlo, y RN-00013, si un punto queda fuera de la guardia |
| Necesidades de negocio | NB-00001 a NB-00009, las nueve. La correspondencia está en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §7.1 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US de la traducción completa de los **dieciséis** códigos; US de las tres familias empobrecidas, **con la comparación de respuestas indistinguibles como criterio de aceptación**; US de la prohibición de §1.4, con inspección del registro del servidor |
| Tests previstos en 08 | **Una prueba por código del conjunto cerrado**, no una por punto de acceso; las comparaciones de respuestas indistinguibles de las tres familias; y una inspección de que ninguna respuesta ni traza contiene secretos, rutas o el texto del alumno |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | Ver [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §8, donde dos de las cuatro se declaran pertinentes y acotadas en lugar de no aplicables |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Cataloga las **18** entradas derivadas de la §6 de los doce casos de uso y del conjunto cerrado de **diecisiete** códigos del ensamblado de contratos: **16 códigos con destino**, **1 declarado sin destino** y **2 respuestas sin código**. Declara los **dos resultados que no son fallos** con la confusión más cara de esta capa; la prohibición de §1.4 con su contracara de registro; **las tres familias deliberadamente empobrecidas** y la prueba única que las cubre; la taxonomía de siete categorías con su reparto por código de respuesta; **los dos huecos del conjunto cerrado**, elevados al Product Owner, que son el motivo por el que el código genérico tiene cuatro destinos; y la ausencia declarada de `CU-00010`, `CU-00011` y `CU-00012` del catálogo, con el motivo de cada una. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-00016) y la precisión de F-04**, que unifican en uno los dos mecanismos de credencial inicial del producto. El conjunto cerrado del ensamblado pasa de diecisiete a **quince** códigos y este catálogo de **dieciocho a dieciséis entradas**: salen `CONTRATO_CONTRASENA_NO_ESTABLECIDA` de §3.3 y `CONTRATO_RESETEO_NO_APLICABLE_A_CUENTA_SIN_CONTRASENA` de §3.6, las dos **por imposibilidad de su causa** y no por simplificación, y **ninguna la reemplaza**. La entrada de `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` pasa a declarar los **dos orígenes** de la marca y a ser también el mensaje del primer ingreso. §2.1 y §6 actualizan los recuentos del conjunto cerrado, de los puntos de acceso —de dieciséis a **quince**, con el retiro de `A-04`— y de la US prevista. La cabecera cita el intake **1.13**. **Las tres familias empobrecidas y las dos respuestas sin código no cambian.** Sube minor. |
| 1.2 | 2026-08-10 | **Cierra el hallazgo `C-04` (P1) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** La última oración de **§2.3** declaraba «son **dieciséis** códigos con destino sobre **diecisiete**» dos párrafos después de que la misma sección declarara, correctamente, un conjunto cerrado de **quince**. Pasa a «**catorce** códigos con destino sobre **quince**», que es lo que declara la fuente hermana de este mismo proyecto de código —`02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §6: «Quince códigos: catorce con destino en esta superficie y uno sin él»— y lo que da el recuento sobre la tabla de traducción, contada fila por fila. El recuento es el que la propia oración pide que una revisión posterior no levante como cobertura faltante, de modo que blindaba el número equivocado. La cabecera pasa a citar el intake **1.14**. **Ninguna entrada del catálogo, ningún código de respuesta y ningún hueco declarado cambia.** Sube minor. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-05-04` (P1) del informe de auditoría [`../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0**, y con él el `C-05-05` (P2) derivado. La emisión 1.1 retiró dos entradas por **RN-00016** y actualizó §2.3, §2.4 y el encabezado de §3, pero **no recontó las tablas de §2.1 ni de §6**, que siguieron declarando los números anteriores. Los cuatro bloques congelados se llevan al recuento verdadero, **contado fila por fila sobre las siete tablas de §3.1 a §3.7**: **§2.1** pasa a `Situación de la cuenta` **2** —con «estas **dos** llevan motivo»— y `Conflicto de estado` **5**, con el total en **dieciséis** y el reparto **3 + 2 + 2 + 1 + 2 + 5 + 1 = 16**, y el párrafo siguiente a «**cinco** de las **dieciséis**»; el encabezado de **§3.6** pasa de «seis entradas» a «**cinco** entradas», sin dejar de ser la categoría más poblada; **§6.1** pasa a **15** códigos del conjunto cerrado, **14** con destino, **1** sin destino, **2** respuestas sin código y **16 = 14 + 2** entradas, con el cuadre **14 + 1 = 15** y **14 + 2 = 16**, y **cita explícitamente la tabla de traducción de `../05-Arquitectura-Tecnica/Contratos-REST.md` §5 como su cuadre**, que es lo que pide `C-05-05`; **§6.2** repone la tabla de siete categorías con 2 y 5, totaliza **16**, y sus dos comprobaciones pasan a **15 / 14 / 1** y **16 / 14 / 2**. **«Diez códigos de respuesta distintos» de §6.2 se verificó aparte y es correcto: no se toca** —`400`, `401`, `403`, `404`, `409`, `500`, `503`, más `200`, `201` y `204`—. **Ninguna entrada, ningún código del contrato, ningún código de respuesta, ninguna familia empobrecida y ningún hueco declarado cambia**: lo que cambia son cuatro recuentos que describían un catálogo anterior al retiro de `RN-00016`. Sube minor. |
| 1.4 | 2026-08-11 | **Cierra los hallazgos `B-API-17` (P3) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§6.2**, tercera comprobación: la respuesta del punto de salud dejaba de estar clasificada —se la llamaba «un `503` **sin código del contrato**» y a la vez cubierta por la entrada de no clasificado, **que sí lleva código**—, de modo que o contradecía el conjunto cerrado de **dos** respuestas sin código de §2.2 o le sumaba una tercera. Pasa a declararla **con** `CONTRATO_ERROR_NO_CLASIFICADO`, que es lo que fija `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §6 para el `503` de una terminación degradada del almacén, y lo que §3.7 de este catálogo ya enumeraba entre las cuatro causas del código genérico. **Ningún recuento cambia**: el catálogo cerraba igual por cualquiera de las dos lecturas, y sigue en **16 = 14 + 2**, recontado sobre §3.1 a §3.7 (3+2+2+1+2+5+1). **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26**, vigente hoy. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**, según la condición de método del informe: los recuentos que este catálogo gobierna se citaban mal en **seis lugares vivos de cuatro documentos** de `03-UX-UI-DX` —`README.md` §2, `Glosario-UX.md` §2 y §3.1, `DX-Developer-Experience.md` §5 y §6, `Guia-Onboarding-Developer.md` §3.5—, y **los seis se corrigen en esta misma tanda**; `05-Arquitectura-Tecnica/Contratos-REST.md` §5 ya tenía el número correcto. **Ninguna entrada del catálogo, ningún código de respuesta y ningún hueco declarado cambia.** Sube minor. **Enmienda de esta misma fila, 2026-08-11**, absorbida en la versión en curso sin subir —la política de versionado del framework absorbe dentro de la versión vigente las correcciones derivadas del audit de la propia fase de emisión mientras el documento está en `Propuesto`—: el alcance de propagación declaraba «cinco documentos» donde son **cuatro**, contados sobre la enumeración misma —`README.md`, `Glosario-UX.md`, `DX-Developer-Experience.md` y `Guia-Onboarding-Developer.md`—; el número venía heredado sin recontar de la ronda 1. **Los seis lugares siguen siendo seis y ningún recuento del producto se mueve.** Cierra el hallazgo `N-01` (P2) de [`B-02-03-GeometriaFactory-Api-r2.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r2.md) 1.0. |
| 1.5 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **33**. Sube minor. |
