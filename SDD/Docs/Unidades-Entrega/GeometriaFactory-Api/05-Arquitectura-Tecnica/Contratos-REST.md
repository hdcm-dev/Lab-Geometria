# Contrato de la superficie HTTP — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Contratos-REST.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-31
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
  - [2.1 Por qué no hay descripción formal de servicio](#21-por-qué-no-hay-descripción-formal-de-servicio)
  - [2.2 El formato de intercambio y su configuración](#22-el-formato-de-intercambio-y-su-configuración)
- [3. Operaciones: los diecisiete puntos de acceso](#3-operaciones-los-diecisiete-puntos-de-acceso)
- [4. Los diez códigos de respuesta](#4-los-diez-códigos-de-respuesta)
- [5. Manejo de errores: la tabla de traducción de los diecisiete códigos](#5-manejo-de-errores-la-tabla-de-traducción-de-los-diecisiete-códigos)
  - [5.1 Las dos respuestas sin código del contrato](#51-las-dos-respuestas-sin-código-del-contrato)
  - [5.2 Los dos huecos declarados del conjunto cerrado, cerrados](#52-los-dos-huecos-declarados-del-conjunto-cerrado-cerrados)
  - [5.3 Las dos señales que no son fallos](#53-las-dos-señales-que-no-son-fallos)
  - [5.4 Lo que ninguna respuesta puede decir](#54-lo-que-ninguna-respuesta-puede-decir)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara **qué expone `GeometriaFactory-Api` hacia afuera del proceso**, con qué compromisos y con qué códigos. Es el único contrato del producto que cruza una frontera de proceso.

**Su único consumidor legítimo es `GeometriaFactory-Web`, servidor a servidor.** Es `RA-01`, regla de nivel producto: **el navegador nunca alcanza esta superficie**. De ahí salen tres ausencias que este contrato declara y que no son olvidos: no hay intercambio de origen cruzado, no hay canal bidireccional y **no hay ningún punto de acceso pensado para que lo invoque un navegador**.

Los casos de uso que se materializan a través de este contrato son **once de los doce** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. El doceavo —la colección de peticiones reproducible— **ejercita este contrato en lugar de exponerlo**, y su lugar es el árbol de muestras del repositorio.

**Este documento no redefine la superficie**: la superficie la declara [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md), con sus quince puntos y sus rutas rotuladas fila por fila como propuesta derivada. Lo que este documento agrega es lo que a la categoría 05 le toca: **el formato de intercambio y su configuración, el criterio de traducción y la política de versionado**.

## 2. Formato

### 2.1 Por qué no hay descripción formal de servicio

**Este contrato se declara en prosa estructurada y no en una descripción formal de servicio, y es un apartamiento declarado de la guía del tipo `rest-api`.**

La guía exige una descripción formal para este tipo. **La fuente decide lo contrario, por escrito y con fundamento**: `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Contracts descarta «generar el cliente desde una descripción formal» por costo de cadena de herramientas frente a un contrato que consumen dos proyectos de código del mismo producto, y §17.1.P.12 · GeometriaFactory-Contracts lo cierra: «se renuncia a un contrato descrito en una notación formal y a clientes generados: **con dos consumidores compilados juntos, el costo no se paga**».

Emitir una descripción formal contra esa decisión crearía **una segunda fuente de verdad sobre la misma superficie**, que envejecería sin que nada la compare, en un producto cuyo defecto documentado más repetido es exactamente ése. El contrato formal del producto **es el ensamblado de tipos de transferencia**, que los dos extremos compilan, y este documento es la superficie que lo transporta.

**El apartamiento se declara acá y se registra en el README de la sección**, para que una revisión posterior no lo levante como artefacto faltante.

### 2.2 El formato de intercambio y su configuración

**Es lo que esta categoría fija, y lo fija para los dos extremos.** `GeometriaFactory-Contracts` decidió no imponer formato —sus tipos no referencian ninguna biblioteca de serialización— y reasignó la decisión a las categorías 05 de este proyecto de código y de `GeometriaFactory-Web`; aquélla declaró que **no la toma de un solo lado** y que la toma ésta, por ser la del productor. El detalle está en [`ADR-00002`](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md); acá va el contrato.

| Regla | Decisión | Qué pasa si se rompe |
| --- | --- | --- |
| Notación | **Notación de objetos de texto**, sobre los tipos de `GeometriaFactory-Contracts` | Declarado por el intake §17.1.P.3 · GeometriaFactory-Api |
| Nombres de campo | **Tal como los declara el tipo**, sin transformación de estilo | Es la única convención que no puede desincronizarse, porque no hay nada que configurar distinto en cada lado |
| Conjuntos cerrados | **Por su nombre, nunca por su posición**. Son **cuatro**: papel de la cuenta, estado de cuenta, estado del trabajo y especie de observación | Un valor insertado en el medio cambiaría el significado de **todos** los datos ya emitidos |
| Campos nulos | **Se emiten** | La nulidad significa cosas: una credencial nula es una cuenta `Pendiente`; un comentario nulo es un trabajo sin desenlace escrito |
| Números decimales | **Sin cultura, con punto decimal** | Es el modo de falla que el escenario `E-8` documenta como el más probable del producto; reproducirlo en la frontera propia sería absurdo |
| Lectura de la petición | **Estricta**: un campo desconocido se rechaza con `400` | Aceptarlo en silencio deja que un extremo desactualizado envíe algo que el otro descarta sin decirlo |
| Tamaño del cuerpo | **Un solo límite en todo el producto, tomado de configuración. El cuerpo que lo excede se rechaza con `400`. Nunca se trunca** | Truncar rompe `RN-00008` **en silencio**: el trabajo queda guardado con el texto mutilado |
| Texto original del alumno | **No se normaliza en el borde**: no se recodifica, no se recortan espacios, no se normalizan saltos de línea | El borde del proceso es el primer lugar donde el texto puede alterarse |

**Ocho filas, y no son ocho reglas de formato: son seis, más dos que no lo son.** Las **seis reglas de formato** son las que numera [`ADR-00002`](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 —nombres de campo, conjuntos cerrados, campos nulos, números decimales, lectura de la petición y tamaño del cuerpo—, y **son ellas** las que están elegidas para que **ninguna dependa de que dos configuraciones coincidan**. Las otras dos filas viven en la misma tabla porque rigen la misma frontera, pero no son reglas sobre el formato: la **notación** es el formato mismo, y la **prohibición de normalizar el texto original** la declara `ADR-00002` explícitamente como no siendo de formato. **6 + 1 + 1 = 8**, y el contenido de las ocho coincide punto por punto con el de la ADR.

**Transporte.** Petición y respuesta, **sin estado**, con la credencial firmada en la cabecera de autorización. En desarrollo se escucha **sin certificado**, para evitar la fricción del certificado de confianza dentro del contenedor. **Un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio.**

## 3. Operaciones: los diecisiete puntos de acceso

Los diecisiete son los de [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, y **este contrato los adopta sin cambiarlos**: las rutas siguen siendo propuesta derivada de aquella categoría, rotulada fila por fila, y su forma definitiva se valida en el punto de control de la etapa `a`. Lo que esta tabla agrega es la columna de **caso de uso de la capa de aplicación** que cada punto ejerce, que es lo que ata la superficie a lo que realmente ocurre.

| Punto | Verbo | Qué hace | Papel exigido | Códigos | CU de la capa de aplicación |
| --- | --- | --- | --- | --- | --- |
| A-01 | `POST` | Canjear correo y contraseña por una credencial firmada | Ninguno | `200`, `400`, `401`, `403` | Consulta de admisibilidad del ingreso |
| A-02 | `POST` | Registrar una cuenta de alumno, **sin campo de contraseña**. Es anónimo por diseño y así debe seguir | Ninguno | `201`, `400`, `409` | Alta de cuenta por auto-registro |
| A-03 | `POST` | Configurar la cuenta de administrador, **sólo mientras no exista ninguna** | Ninguno | `201`, `400`, `409` | Configuración de la cuenta de administrador |
| A-05 | `POST` | Cambiar la contraseña propia exigiendo la vigente. **Es el único punto que levanta la marca** | `Alumno` o `Administrador` | `200`, `400`, `401` | Reemplazo de la credencial propia |
| A-06 | `GET` | Listar las cuentas de la comisión con su situación y su marca | `Administrador` | `200`, `401`, `403` | Gobierno de las cuentas, en su lectura |
| A-07 | `POST` | Cambiar la situación de una cuenta. **Habilitar y rehabilitar devuelven la contraseña provisoria** (`RN-00016`) | `Administrador` | `200`, `400`, `401`, `403`, `404` | Gobierno de las cuentas |
| A-08 | `DELETE` | Dar de baja una cuenta, **transportando el correo escrito como confirmación** | `Administrador` | `204`, `400`, `401`, `403`, `404` | Gobierno de las cuentas, en su baja con arrastre |
| A-09 | `POST` | Resetear la contraseña de un alumno y devolver la provisoria **una sola vez** | `Administrador` | `200`, `400`, `401`, `403`, `404`, `409` | Reseteo de la contraseña de un alumno |
| A-10 | `POST` | Enviar un trabajo nuevo, con el texto original **sin normalizar** | `Alumno` | `201`, `400`, `401`, `403` | Envío del trabajo e interpretación de su texto |
| A-11 | `POST` | Reenviar un trabajo que quedó en `Borrador` | `Alumno` | `200`, `400`, `401`, `403`, `404`, `409` | Carga y reedición, y envío |
| A-12 | `DELETE` | Eliminar un trabajo, **con los dos alcances opuestos** | `Alumno` o `Administrador` | `204`, `401`, `403`, `404`, `409` | Eliminación de un trabajo |
| A-13 | `GET` | Listar trabajos, con el alcance que el papel determina y **sin componentes** | `Alumno` o `Administrador` | `200`, `401`, `403`, `404` | Consulta de los trabajos propios; revisión de la comisión |
| A-14 | `GET` | Obtener el detalle de un trabajo interpretado | `Alumno` o `Administrador` | `200`, `401`, `403`, `404` | Consulta de los trabajos propios; revisión de la comisión |
| A-15 | `POST` | Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | `Administrador` | `200`, `400`, `401`, `403`, `404`, `409` | Desenlace del trabajo |
| A-16 | `GET` | Responder por el estado del servicio. **No exige acceso** | Ninguno | `200`, `503` | Ninguno: invoca la preparación del almacén |
| A-17 | `GET` | Responder **si el laboratorio ya tiene administrador**, y nada más. **No exige acceso** y es de **sólo lectura** | Ninguno | `200` | Configuración de la cuenta de administrador, en su consulta |
| A-18 | `POST` | **Interpretar un texto sin guardar nada**, para la previsualización. **No constituye ningún trabajo**: devuelve la estructura reconstruida y no deja rastro | `Alumno` | `200`, `400`, `401`, `403` | Envío del trabajo e interpretación de su texto, en su camino sin persistencia |

**Diecisiete puntos: cinco sin credencial firmada —A-01, A-02, A-03, A-16 y A-17— y doce bajo la guardia. Cinco más doce son diecisiete.**

**A-17 entra porque el guardián 1 de `Web ADR-00003` §2 no se podía construir sin él**, y esa constancia va acá y no sólo en 02 porque es una propiedad de **esta** tabla: **ninguno de los quince puntos anteriores servía para que un anónimo preguntara si el laboratorio ya tiene administrador**. `A-03` configura —es escritura—, `A-16` responde por la salud del servicio y `A-06` exige el papel. El fundamento entero, lo que el punto revela y **por qué el dato no se le agregó a `A-16`** —la salud la consume el chequeo del contenedor de `deploy/compose.yaml`, y mezclarle un hecho del producto acopla dos cosas que cambian por motivos distintos— están en [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, que es donde se decide. **La decisión la tomó el orquestador, con el Product Owner avisado, y queda a ratificación.**

**El identificador `A-04` está retirado y no se recicla.** Establecía la contraseña del primer ingreso **sin credencial**, y `RN-00016` suprimió la operación en lugar de resolverla: habilitar produce la provisoria y el alumno cambia la suya ya autenticado, por `A-05`. **De los cinco puntos que no exigen credencial, ninguno fija una contraseña sobre una cuenta existente** —`A-16` y `A-17` son de sólo lectura—, y ésa es la propiedad que hay que poder comprobar sobre esta tabla.

## 4. Los diez códigos de respuesta

| Código | Qué significa en esta superficie | Origen |
| --- | --- | --- |
| `200` | La operación se resolvió y hay un cuerpo con el tipo de resultado del contrato | **[derivado]** |
| `201` | Se constituyó algo que antes no existía: una cuenta o un trabajo | **[derivado]** |
| `204` | Se retiró algo y no hay cuerpo que devolver | **[derivado]** |
| `400` | La petición no es utilizable: falta un campo que el contrato exige, el que llegó no es del conjunto cerrado que declara, el cuerpo no se puede leer, trae un campo desconocido, o **excede el límite de tamaño** | **[derivado]** |
| `401` | **Ante credenciales inválidas, genérico y sin declarar cuál campo falló.** También ante la ausencia de credencial, la credencial vencida y la firma que no corresponde | **Declarado** por el intake §17.1.P.5 · GeometriaFactory-Api; ampliado por derivación a los tres casos de la guardia |
| `403` | **Con motivo**, ante la cuenta que no admite acceso, ante el papel que el punto no admite y ante la cuenta con cambio de contraseña pendiente | **Declarado** por el intake §17.1.P.5 · GeometriaFactory-Api para la cuenta `Pendiente` o `Bloqueado`; los otros dos son derivación |
| `404` | Lo pedido no existe, **o no es del solicitante, o está fuera de lo que ve**, sin que la respuesta permita distinguir los tres casos | **[derivado en el número; la obligación es de `RN-00003`]** |
| `409` | La operación es legítima y el estado no la admite | **[derivado]** |
| `500` | Un defecto que el producto no previó. **Nunca lleva detalle de implementación** | **[derivado]** |
| `503` | El servicio no puede atender: el almacén no está disponible, o el arranque todavía no lo dejó en condiciones | **[derivado]** |

**Diez códigos: dos de la fuente y ocho de derivación**, con el matiz declarado del `404`, cuyo **número** es derivado y cuya **obligación** no lo es.

**Dos códigos que esta superficie no usa, y su ausencia es informativa.** No hay respuesta de entidad no procesable: el conjunto de causas que otro producto pondría ahí —un texto del alumno que no verifica— **no es un fallo en éste** (§5.3). Y no hay respuesta de exceso de peticiones: ninguna fuente declara límite de caudal, el previsto es de una comisión durante una clase, y agregarlo sería una decisión que nadie tomó ([`ADR-00005`](Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md)).

## 5. Manejo de errores: la tabla de traducción de los diecisiete códigos

Una petición que falla atraviesa **dos** traducciones: motivo interno a **código del contrato**, y código del contrato a **código de respuesta**. Las dos son de esta capa ([`ADR-00004`](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md)), y ésta es la tabla única de la segunda.

El conjunto cerrado lo declara `GeometriaFactory-Contracts` y tiene **diecisiete** códigos vivos, dos de ellos incorporados por `PRODUCT-INTAKE` **1.29** §17.4 P.3. Sobre **veinte** identificadores que ese ensamblado emitió a lo largo de su historia, **tres están retirados y ninguno se recicla**. **Esta capa no agrega, no renombra y no traduce a texto ninguno.**

**Las diecisiete filas están, una por código, sin agrupar.**

| Código del contrato | Código de respuesta | Fundamento |
| --- | --- | --- |
| `REQUIRED_FIELD_MISSING` | `400` | La petición no es utilizable, y la respuesta **nombra el campo ausente** sin agregar nada más |
| `INVALID_CREDENTIALS` | `401` | **Declarado por la fuente.** Genérico: la respuesta **no declara cuál de los dos campos falló** |
| `ACCOUNT_NOT_ENABLED` | `403` | **Declarado por la fuente**, con motivo, para que la persona sepa en qué situación está su cuenta |
| `PASSWORD_CHANGE_REQUIRED` | `403` | Con motivo. **Es un solo código para todas las operaciones bloqueadas y para los dos orígenes de la marca** —la habilitación y el reseteo—, y acá un solo código de respuesta para todas |
| `WORK_NOT_FOUND` | `404` | **`RN-00003`.** Cubre el inexistente, el ajeno y el que está fuera de lo que el solicitante ve, y las tres respuestas son **indistinguibles** |
| `STUDENT_NOT_FOUND` | `404` | El filtro por alumno referencia un identificador que no existe, y —por adopción declarada de la categoría 02— la cuenta que un punto de administración referencia y no existe |
| `EMAIL_ALREADY_REGISTERED` | `409` | El estado del conjunto no admite la operación. La respuesta **no declara la situación ni el papel** de la cuenta que ocupa el correo |
| `ADMINISTRATOR_ALREADY_CONFIGURED` | `409` | Ídem, y **el contrato no ofrece camino alternativo**: la respuesta no sugiere ninguno |
| `CONFIRMATION_MISMATCH` | `400` | Es un campo de la petición que no cumple lo que el contrato le pide, no un estado que impida la operación. **La respuesta no devuelve el correo esperado** |
| `STATE_FORBIDS_DELETE` | `409` | El estado del trabajo no habilita al solicitante. La respuesta **declara el estado actual**, que es lo que el contrato ya transporta |
| `STATE_FORBIDS_OUTCOME` | `409` | Ídem, **incluido el estado terminal**, y la respuesta **no sugiere ninguna forma de revertirlo** |
| `OUTCOME_ADMIN_ONLY` | `403` | Es una negativa de facultad y **no tiene nada que ocultar**: no hay recurso ajeno cuya existencia proteger, porque el trabajo puede ser propio |
| `RESET_NOT_APPLICABLE_TO_ADMINISTRATOR_ACCOUNT` | `409` | El sujeto del reseteo no lo admite. **No es `403`**: quien pide tiene la facultad, y lo que no procede es la operación sobre esa cuenta |
| `OPERATION_ADMIN_ONLY` | `403` | Negativa de facultad **fuera del desenlace** —gobierno de cuentas, listado de la comisión y reseteo—. Como la del desenlace, **no tiene nada que ocultar**: el recurso no es ajeno y lo que no alcanza es el papel |
| `STATE_FORBIDS_UPDATE` | `409` | El estado del trabajo no habilita la escritura pedida —envío o reedición—. La respuesta **declara el estado actual** y no sugiere ninguna forma de volver a `Borrador` |
| `UNCLASSIFIED_ERROR` | `500` o `503` | **Es el único código con más de un destino**: `503` cuando la causa es una terminación degradada del almacén, que no depende de lo que se pidió y puede resolverse sola; `500` cuando es un defecto no previsto. **Bajó de cuatro destinos a dos** cuando los dos códigos nuevos se llevaron el `403` y el `409` que sólo estaban acá por falta de código propio |
| `SERVICE_UNAVAILABLE` | **Ninguno** | **No lo produce esta capa.** El ensamblado lo declara «el único que el contrato admite que produzca la propia pieza pública, porque describe la ausencia de respuesta de la otra pieza». Una respuesta de esta superficie con este código sería una contradicción en sus términos: **si hay respuesta, el servicio respondió** |

**Diecisiete códigos: dieciséis con destino en esta superficie y uno sin él. Diecisiete filas para diecisiete códigos, ninguna excedente.**

**Que el código genérico haya bajado de cuatro destinos a dos es la medida del cierre de §5.2**: los dos destinos que se fueron existían **sólo** porque el conjunto cerrado no tenía un código propio para esos caminos.

### 5.1 Las dos respuestas sin código del contrato

| Respuesta | Cuándo | Por qué no lleva código |
| --- | --- | --- |
| `401` de la guardia | No hay credencial, la credencial venció, o su firma no corresponde | El conjunto cerrado **no declara ninguno** que describa una credencial ausente o inválida, y **esta capa no inventa códigos**. Lo que el consumidor necesita saber es que tiene que volver a canjear credenciales, y eso lo dice el número |
| `400` de petición ilegible | El cuerpo no se puede leer, un valor no pertenece a un conjunto cerrado, llega un campo desconocido, o el cuerpo excede el límite | Ocurre **antes** de que la petición llegue a ser el tipo del contrato: no hay contrato con el que hablar todavía |

**Las dos son deliberadas y se declaran para que su ausencia de código no se lea como un olvido.**

### 5.2 Los dos huecos declarados del conjunto cerrado, **cerrados**

Estaban elevados al Product Owner, con la constancia de que **esta categoría no inventaría un código para taparlos** porque los códigos son del ensamblado de contratos. **El Product Owner los resolvió** (`PRODUCT-INTAKE` **1.29** §17.4 P.3) y `GeometriaFactory-Contracts` emitió los dos códigos.

| Hueco | Qué faltaba | Con qué se cerró |
| --- | --- | --- |
| **El papel no alcanza, fuera del desenlace** | El conjunto cerrado tenía **un solo** código de facultad y su enunciado estaba acotado al desenlace de la revisión. La capa de aplicación emite un motivo de facultad requerida también en **el gobierno de cuentas, el reseteo y la revisión de la comisión** | `OPERATION_ADMIN_ONLY`, con destino `403` en la tabla de §5. Cerrado el **2026-08-12** |
| **El trabajo no está en `Borrador`, fuera de la eliminación** | El código análogo estaba acotado por su enunciado **a la eliminación y al camino del alumno**. Un envío o una reedición forzados fuera de `Borrador` no tenían dónde ir | `STATE_FORBIDS_UPDATE`, con destino `409` en la tabla de §5. Cerrado el **2026-08-12** |

**Ningún código vecino cambió de enunciado**, y **ninguno de los dos recicla un identificador retirado**.

**Y una constancia que esta sección debía y no daba, agregada en la 1.5.** Este párrafo afirmaba que `UNCLASSIFIED_ERROR` **«bajó de cuatro destinos a dos»**. **Tiene cuatro**: `503`, `500`, `409` y `403`.

**Los dos que sobran son un apartamiento declarado, y desde el 2026-08-31 lo está con su forma.** Vive en [`Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md`](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) **2.0** §2.1, con los seis campos que `Root-Rules.md` §11 exige: qué obligación no cumple —§6 de la superficie le da al genérico sólo `500` y `503`—, por qué no aplica, las tres alternativas descartadas, los tres disparadores que lo superarían, su estado `vigente`, y su contador.

**El contador es el dato que conviene mirar: 0 revisados y siete transcurridos.** Siete migraciones normativas pasaron entre el 2026-08-12 y hoy sin revisarlo, porque `Migracion-Rules.md` §4.7 revisa apartamientos **declarados** y éste no lo estaba.

### 5.3 Las dos señales que no son fallos

| Señal | Qué viaja | Por qué no es un fallo |
| --- | --- | --- |
| **El envío cuyo texto no verifica** | Respuesta **exitosa**, con el trabajo guardado, el estado `Borrador`, el texto conservado íntegro y las observaciones con su índice de figura y su campo | **El trabajo se guardó y su estado se decidió.** Lo que no verifica es el texto, no la petición. Un código de fallo le diría a la persona que su petición estaba mal mientras su trabajo, en realidad, quedó guardado |
| **El listado sin elementos** | Respuesta **exitosa** con una colección vacía | Una comisión sin entregas todavía. El consumidor distingue vacío de fallo **por el tipo recibido y no por el conteo** |

**Ninguna de las dos tiene código de respuesta de fallo, y ninguna figura en la tabla de §5.**

### 5.4 Lo que ninguna respuesta puede decir

Es `RA-03`, regla de nivel producto, y **acá es donde se puede violar hacia afuera**: es la última vez que un dato del backend es tocado antes de salir del servidor propio.

| Nunca aparece en una respuesta | Qué corresponde en su lugar |
| --- | --- |
| La **dirección de un servicio interno**, en cualquier forma | El motivo, sin origen |
| La **ruta del archivo del almacén** | «El servicio no puede atender» |
| La **clave de firma**, ni una parte de ella | «No hay credencial válida» |
| La **contraseña en claro** ni el valor derivado de una credencial | La respuesta genérica de credenciales inválidas |
| La **contraseña provisoria**, fuera del cuerpo del resultado del reseteo y de la habilitación | Nada: el valor viaja en el resultado, **una sola vez**, y no en ningún otro lado |
| **Trazas de la implementación**, nombres de tipos internos o cadenas de llamada | El código genérico, con su código de respuesta |

**Y la contracara, que es igual de obligatoria:** registro estructurado del lado del servidor **de cada error y de cada intento de acceso rechazado**. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar, y el operador que despliega a mano se queda sin nada que mirar.

## 6. Versionado del contrato

**No se versionan las rutas, y lo que lo reemplaza es el despliegue conjunto de las dos piezas desplegables ante todo cambio de contrato** ([`ADR-00008`](Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)). No hay clientes de terceros: los dos extremos compilan contra el mismo ensamblado y **un cambio incompatible rompe la compilación antes de romper el tiempo de ejecución**.

| Cambio sobre este contrato | Clase | Qué obliga |
| --- | --- | --- |
| Quitar o renombrar un punto de acceso, o cambiar su verbo | Mayor | Despliegue conjunto |
| Cambiar el papel que un punto exige, o sacarlo de la guardia | Mayor | Despliegue conjunto, **y modificación de [`ADR-00003`](Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md)** |
| Cambiar el código de respuesta de un código del contrato | Mayor | Despliegue conjunto |
| Quitar un código del conjunto cerrado, o reciclar un identificador retirado | Mayor | Es del ensamblado de contratos, **y reponer un identificador retirado se rechaza aunque compile** |
| Cambiar cualquiera de las ocho filas de §2.2 —las seis reglas de formato o las dos que no lo son— | Mayor | **Los dos extremos a la vez**: es la clase de cambio que **no rompe ninguna compilación** |
| Agregar un punto de acceso | Menor | Entra a la tabla de §3 **en la misma intervención**, y la prueba de inspección de la guardia falla si no está |
| Agregar un código al conjunto cerrado | Menor | Entra a la tabla de §5 con su destino |
| Corregir un punto para que cumpla lo que ya declaraba | Parche | Ninguna |

**Compatibilidad hacia atrás: no hay convivencia de dos versiones y no hay deprecación gradual.** La política es corregir los dos lados en la misma etapa. **Cada etapa cerrada recibe una etiqueta**, y la reversión es volver a la etiqueta anterior y reconstruir.

**Tres clases de cambio que la compilación compartida no detecta**, y cada una con su mecanismo: la **configuración de intercambio**, fijada de un solo lado en §2.2; el **esquema del almacén**, verificado al arrancar con su linaje, que detiene el arranque si no cierra; y las **rutas**, ejercidas por la batería de integración contra el servicio real.

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo materializan | **Once** de los doce de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5. `CU-00012` lo **ejercita** en lugar de exponerlo |
| Puntos de acceso | **Diecisiete**: A-01 a A-03 y A-05 a A-18. `A-04` retirado y **no reciclado** |
| Códigos de respuesta | **Diez** distintos, de §4 |
| Códigos del contrato | **Diecisiete** vivos sobre **veinte** identificadores emitidos por `GeometriaFactory-Contracts`; **dieciséis con destino acá y uno sin él** |
| CU de la capa de aplicación | Los **once**, con el reparto de la columna de §3 y la correspondencia de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.4 |
| RN que cubre | RN-00001 a RN-00016, las **dieciséis**, con el reparto de [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.2. **Trece** tienen tramo acá; RN-00005, RN-00014 y RN-00016 no. **Dos** se rompen desde acá: RN-00003 y RN-00013 |
| Invariantes | INV-01 a INV-09, los **nueve**, con el aporte declarado en [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §10.3 |
| Reglas de arquitectura | **Las tres.** `RA-01` la sostiene y es el único que puede romperla; `RA-02` no tiene tramo acá y se declara; `RA-03` se ejerce en §5.4 |
| ADR que lo gobiernan | ADR-00002, ADR-00003, ADR-00004, ADR-00005, ADR-00008 |
| Consumidor | **Uno solo**: `GeometriaFactory-Web`, servidor a servidor, por HTTP en tiempo de ejecución |
| Tests previstos en 08 | **Una prueba por código del conjunto cerrado**, no una por punto de acceso; la inspección de la tabla de §5 en las dos direcciones; las tres comparaciones de respuestas indistinguibles; la prueba de texto original byte a byte y la de rechazo sin truncamiento; la de eliminación forzada en sus dos alcances; y la colección de peticiones reproducible como ejercicio de punta a punta |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara el apartamiento de la descripción formal de servicio con el fundamento de la fuente, **fija el formato de intercambio y su configuración para los dos extremos** con sus ocho reglas —cerrando el punto abierto que `GeometriaFactory-Contracts` reasignó y `GeometriaFactory-Web` devolvió—, adopta los quince puntos de acceso con la columna de caso de uso que cada uno ejerce, los diez códigos de respuesta con las dos ausencias informativas, y publica la **tabla de traducción con sus quince filas** —catorce con destino y una sin él—, las dos respuestas sin código, los dos huecos declarados, las dos señales que no son fallos, la prohibición de `RA-03` y la política de versionado sin versionado de rutas con las tres clases de cambio que la compilación no detecta. |
| 1.1 | 2026-08-10 | **Cierra el hallazgo `C-05-03` (P2) del informe de auditoría [`../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0**, en su mitad de este documento. §2.2 publicaba **ocho** filas bajo la columna `Regla` y §9 hablaba de «las ocho reglas», mientras [`ADR-00002`](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 numeraba **seis**: el mismo objeto con dos recuentos en la misma ola, en el artefacto que cierra un reasignado entre capas. **No había contradicción de contenido**, y se verificó fila por fila. §2.2 agrega el cuadre explícito **6 + 1 + 1 = 8**, nombrando las **seis reglas de formato** y las **dos filas que no lo son** —la notación y la prohibición de normalizar el texto original—, y aclarando que el predicado «ninguna depende de que dos configuraciones coincidan» se predica de las seis. §9 pasa a decir «las ocho **filas** de §2.2». **Ninguna fila de la tabla, ningún punto de acceso, ningún código y ninguna política de versionado cambia.** Sube minor. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **18**. Sube minor. |
| 1.3 | 2026-08-15 | **Adopta el punto de acceso `A-17`, que `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` 1.7 agrega a la superficie.** El punto responde **si el laboratorio ya tiene administrador, y nada más**: `GET /aprovisionamiento`, **sin papel exigido**, de **sólo lectura** y con **un solo código de respuesta**, `200`. Existe porque el **guardián 1 de `Web ADR-00003` §2** —el único de los cuatro que nunca se construyó— **no se podía construir**: ninguno de los quince puntos anteriores servía para que un anónimo preguntara eso. **§3**: fila del punto, columna de caso de uso de la capa de aplicación —la configuración de la cuenta de administrador, en su consulta—, recuento de **cinco sin credencial firmada más once bajo la guardia = dieciséis**, y la constancia de que la clase «fijar una contraseña sobre una cuenta existente sin credencial» **sigue ausente**, porque `A-16` y `A-17` son de sólo lectura. **§7**: los puntos de acceso pasan a **dieciséis**, A-01 a A-03 y A-05 a A-17, con `A-04` retirado y **no reciclado**. **Ningún código de respuesta, ninguna fila de la tabla de traducción de §5, ninguna regla de versionado de §6 y ningún otro recuento de §7 cambia.** Este contrato **adopta y no decide**: la decisión está en 02, la tomó el orquestador con el Product Owner avisado, y **queda a ratificación**. Sube minor. |
| 1.4 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) §8. **19 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.5 | 2026-08-31 | **Adopta el punto de acceso `A-18`**, que `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` declara y este contrato no había recogido: `POST /interpretaciones`, interpretar un texto **sin guardar nada**, para la previsualización. **Lo creó [`ADR-08006`](../../../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md) como su contrapartida declarada**: si el visor recibe piezas reconstruidas en lugar del texto, previsualizar necesita que alguien las reconstruya. **El barrido de alcance de esa decisión llegó a la categoría 02 y no a ésta**, de modo que el servicio exponía diecisiete operaciones contra dieciséis declaradas acá — lo encontró el sample `api/03-avanzado` contando sobre el documento OpenAPI que el propio servicio publica. Es el mismo hueco que dejó los §6 de la categoría 10 describiendo la fachada anterior, reportado al framework como `Reporte 21`. Se actualizan los cuatro recuentos: encabezado, prosa de §3, cierre de §3 —cinco sin credencial y **doce** bajo la guardia— y la fila de §7. **§5.2 suma una constancia que debía**: afirmaba que el genérico «bajó de cuatro destinos a dos» y **tiene cuatro** —`503`, `500`, `409` y `403`—, los dos últimos por apartamientos declarados en un comentario de código y no como ADR; queda elevado. Sube minor: adopta lo que otra categoría ya declaró y no cambia ninguna decisión. |
| 1.6 | 2026-08-31 | **§5.2 cita el apartamiento en lugar de constatar el desvío.** Los dos destinos de más del código genérico están declarados desde hoy con la forma de `Root-Rules.md` §11 en `ADR-00004` **2.0** §2.1 —seis campos, con sus tres alternativas descartadas y sus tres disparadores—, de modo que este párrafo deja de elevar un hueco y pasa a apuntar a su decisión. Sube minor. |
