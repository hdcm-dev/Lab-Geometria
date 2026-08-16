# 02 · Especificación funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md`; y las categorías 02 de `GeometriaFactory-Contracts`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los nueve casos de uso](#2-los-nueve-casos-de-uso)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y el que se emite](#4-artefactos-omitidos-y-el-que-se-emite)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: las cinco responsabilidades, la frontera entre lo que se decide y lo que se transporta, el catálogo, la tabla de las dieciséis reglas, la matriz NB → CU → RN → US, el criterio de recorte, las omisiones y los once puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) | Documento de concepto central: los quince puntos de acceso, los diez códigos de respuesta, las dos traducciones, la tabla de los diecisiete códigos del contrato, las **siete** ausencias declaradas y lo que ninguna respuesta puede decir | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y los tres términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | **Nueve** casos de uso, uno por capacidad de la unidad de entrega y uno por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

Los **treinta y dos** documentos que la consolidación 8.5 absorbió están en
[`../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/`](../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/),
con un README que declara qué caso de uso reemplaza a cada uno.

## 2. Los nueve casos de uso

| CU | Nombre | En una línea |
| --- | --- | --- |
| CU-00021 | [`CU-00021` · Dar de alta una cuenta de alumno](Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) | El auto-registro sin campo de contraseña, que deja la cuenta a la espera de la habilitación |
| CU-00022 | [`CU-00022` · Ingresar al laboratorio y sostener la sesión](Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | El canje, la guardia de once puntos y el cambio de la contraseña propia. **La guardia es lo que esta unidad puede romper sin que nada falle** |
| CU-00023 | [`CU-00023` · Gobernar las cuentas de la comisión](Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md) | Listado, situación con la provisoria devuelta una sola vez, y la única operación destructiva |
| CU-00024 | [`CU-00024` · Resetear la contraseña de un alumno](Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md) | El único punto que devuelve una credencial, y que conserva la cuenta y todos sus trabajos |
| CU-00025 | [`CU-00025` · Configurar la cuenta de administrador en el primer arranque](Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md) | La ventana de alta que se cierra para siempre, y el punto que dice si sigue abierta |
| CU-00026 | [`CU-00026` · Enviar un trabajo y ver sus observaciones](Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | La única acción de guardado. **Un texto que no verifica es una respuesta exitosa** |
| CU-00027 | [`CU-00027` · Eliminar un trabajo](Casos-De-Uso/CU-00027-Eliminar-Un-Trabajo.md) | Un punto con dos alcances de reglas opuestas, resueltos adentro y no en la superficie |
| CU-00028 | [`CU-00028` · Consultar el listado y el detalle de los trabajos](Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Los dos únicos puntos que no escriben, con la proyección de listado que no arrastra el texto |
| CU-00029 | [`CU-00029` · Dar desenlace a la revisión](Casos-De-Uso/CU-00029-Dar-Desenlace-A-La-Revision.md) | La única transición irreversible, con el comentario opcional en los dos desenlaces |

**Los cuatro que no están, y por qué.** `CU-00009` —traducir el motivo del contrato a respuesta de
protocolo—, `CU-00010` —componer la aplicación— y `CU-00011` —arrancar el servicio— describen
operaciones internas que **ninguna persona ejecuta**, y viven en
[`../05-Arquitectura-Tecnica/Operaciones-Internas/`](../05-Arquitectura-Tecnica/Operaciones-Internas/)
y en `09-Devops`. `CU-00012` **es un sample**, no un caso de uso, y vive en
[`../10-Examples/`](../10-Examples/). Los identificadores no se reciclan.


## 3. Orden de lectura sugerido

1. [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §1, §3 y §4: qué es esta unidad, qué responsabilidades tiene y **qué decide y qué sólo transporta**.
2. [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) **entero, y antes que cualquier caso de uso**, con especial atención a su **§2**: qué declara una fuente y qué es derivación de esta categoría. Leer §3 sin §2 hace creer que las rutas están decididas, y **catorce de las quince no lo están**.
3. **CU-00022**, porque lleva la guardia, y la guardia gobierna once de los dieciséis puntos. Un punto de acceso leído sin ella parece más abierto de lo que es.
4. El recorrido de la persona, en orden: **CU-00025** y **CU-00021** —cómo nace el laboratorio y cómo nace una cuenta—, después **CU-00026**, **CU-00027** y **CU-00028** —lo que el alumno hace y ve—, después **CU-00023**, **CU-00024** y **CU-00029** —lo que el administrador hace—.
5. [`Glosario-Funcional.md`](Glosario-Funcional.md), en particular §3.1 y §3.2, que resuelven las dos polisemias que más caro salen acá: «acceso» y «código».
6. Y si hace falta saber **de dónde viene** un caso de uso, `Audit/Migracion-8.5-Consolidacion-Decidida.md` §2, que declara qué documentos absorbe cada uno y por qué.


## 4. Artefactos omitidos y el que se emite

| Artefacto | Situación |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido.** Las **dieciséis** reglas del producto viven en `GeometriaFactory-Domain`, las dieciséis con archivo propio allá, y acá se **referencian**. §6 del índice maestro declara, regla por regla, dónde se ejerce cada una en esta capa —**trece con tramo, tres sin él y dos que esta capa puede romper hacia afuera sola**— |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Omitidos**, y el motivo merece leerse porque el flag de persistencia de este proyecto de código vale **true**. Vale true porque acá se toma de configuración la ubicación del almacén y se disparan las transformaciones al arrancar, **no porque acá se modele el dato**: el intake lo resume en «delega en `GeometriaFactory.Infrastructure`». El modelo conceptual del producto ya está emitido allá, con sus cinco entidades y sus siete reglas conceptuales, y duplicarlo crearía dos descripciones del mismo dato guardado |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es la **superficie HTTP**: es lo único que este proyecto de código existe hacia afuera, y es donde se decide lo que se puede romper sin que ninguna capa de adentro se entere |
| Sección opcional §17 de los casos de uso | **No se emite**, y se declara en lugar de omitirse en silencio. Los proyectos de código hermanos la llevan porque `Rules-Especificacion-Funcional.md` §4.3 la asigna al tipo `library`; **este proyecto de código es `rest-api`** y esta categoría no se apropia de una asignación que no le corresponde. El contenido equivalente —qué cambio de la superficie es incompatible— **sí está**, y vive en dos lugares: la política de cambios del ensamblado de contratos, que gobierna los tipos, y [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §7, que declara las **siete** ausencias de la superficie y qué las repone |

## 5. Notas de uso de esta sección

- **Los identificadores son de ámbito producto y de ancho fijo de cinco dígitos.** Ya no son locales a un proyecto de código: la unidad de entrega es una y sus capas son internas. Los identificadores absorbidos por la consolidación **no se reciclan**, y por eso los nueve empiezan en `CU-00021`.
- **Los `A-XX` no son casos de uso.** Son los dieciséis puntos de acceso, y un caso de uso puede describir más de uno; **el reparto es de punto de acceso a capacidad**. La correspondencia está en `Definicion-Superficie-HTTP.md` §3.
- **Catorce de las quince rutas son propuesta derivada de esta categoría**, rotuladas fila por fila. La única que declara una fuente es la del canje de credenciales. Leerlas como decididas es el error de lectura más probable de esta sección.
- **Esta categoría no agrega ningún código al conjunto cerrado del contrato.** Donde falta uno, el hueco se **declara** y se eleva, y mientras tanto se usa el genérico. Los dos huecos están en `CU-00009` §10.
- **Esta categoría no toma decisiones de arquitectura**: las rutas definitivas, los nombres de tipos, la herramienta de configuración, el formato del archivo de la colección y los ADR pertenecen a `05-Arquitectura-Tecnica`; la estrategia de pruebas, a `08-Calidad-Y-Pruebas`; el despliegue, que el intake declara **manual y a cargo del docente**, a `09-Devops`. Lo que acá se declara como «tests previstos» es una previsión, no un plan.
- **Ningún dato de prueba se inventó.** Los escenarios se citan por el identificador del intake —`E-1` a `E-8`— sin renumerar, y es una regla de delivery del producto, no una preferencia de esta categoría.
- **Once puntos abiertos**, ninguno bloqueante: **seis propios** y **cinco** heredados de aguas arriba que no se reabren. **Dos de los seis son huecos de la superficie que esta categoría encontró y elevó al Product Owner**, y son los dos caminos para los que el conjunto cerrado de códigos no declara ninguno: la operación de administrador pedida por quien no lo es, y el envío o la reedición forzados fuera de `Borrador`. **El tercero está cerrado** —cómo se identifica la cuenta al establecer la contraseña del primer ingreso— por `PRODUCT-INTAKE` 1.13 §4.1 (**RN-00016**). Están en §11 del índice maestro.
- **Un residuo de forma de un documento hermano**, anotado para que se absorba y no para corregirlo desde acá: `GeometriaFactory-Infrastructure` §7.2 declara ser una de las **dos** secciones del producto que cubren las nueve necesidades, y con esta emisión son **tres**. Está en §7.2 y en §11 del índice maestro.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de los doce casos de uso y de los tres documentos transversales de la sección; orden de lectura de ocho pasos, **que insiste en leer §2 del documento de concepto central antes que su tabla de rutas**, porque quince de las dieciséis son propuesta derivada; las omisiones con su motivo, incluida la del modelo de datos pese al flag de persistencia en true y la de la sección opcional que la regla asigna a otro tipo; y las notas de uso, con los doce puntos abiertos y los tres huecos elevados al Product Owner. |
| 1.1 | 2026-08-10 | Actualización por `PRODUCT-INTAKE` **1.13** §4.1 (**RN-00016**) y la precisión de **F-04**, que **cierran el punto abierto más importante de esta categoría**: la identidad en el establecimiento de la contraseña del primer ingreso. §1 y §4 actualizan los recuentos: los puntos de acceso pasan de dieciséis a **quince** con el retiro de `A-04`, las rutas derivadas de quince a **catorce**, y los puntos abiertos de doce a **once** —siete propios y cuatro heredados—, con los huecos de superficie elevados al Product Owner de tres a **dos**. Ningún artefacto se agrega ni se omite y el orden de lectura no cambia. (Analista Funcional + API Designer (AG-02)). |
| 1.2 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en las declaraciones vivas de este archivo que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La fila de `Reglas-De-Negocio/` decía **quince** reglas y desglosaba «trece con tramo, dos sin él». Las reglas son **dieciséis**, `RN-00001` a `RN-00016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, y el desglose correcto es **trece con tramo y tres sin él**: es exactamente lo que declara §6 del índice maestro de esta categoría —«Trece de las dieciséis tienen tramo acá y tres no lo tienen»—, cuya tabla ya tiene sus dieciséis filas, `RN-00016` incluida. Las dos que esta capa puede romper hacia afuera sola no cambian. **Ningún documento de la sección, ningún caso de uso y ninguna omisión declarada cambia.** Sube minor. |
| 1.3 | 2026-08-11 | **Cierra el hallazgo `B-API-09` (P2)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0 y **absorbe la propagación de `B-API-12` (P2)**. **§4**, fila de la sección opcional §17: las ausencias de la superficie pasan de **seis** a **siete**; son siete desde que `RN-00016` agregó la del punto que fija una contraseña sin credencial, y **§1 de este mismo archivo ya decía siete**, de modo que el archivo se contradecía a catorce líneas de distancia. Contadas una por una sobre la tabla de [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §7: CORS, WebSockets, pasarela de reenvío, versionado de rutas, sesión del lado del servidor, acceso de refresco y el punto que fija contraseña sin credencial. **§5**: el reparto de los once puntos abiertos pasa de siete propios y cuatro heredados a **seis y cinco**, alineado con [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §11; y se corrige la enumeración de los dos huecos elevados al Product Owner, que nombraba **el hueco cerrado por `RN-00016`** en lugar de uno de los dos vivos —defecto que el informe no registra y que se levanta acá—. **Búsqueda de propagación hecha con `grep`**: «seis ausencias» no sobrevive en ningún otro lugar vivo del corpus; el reparto de puntos abiertos se corrige en los **tres** lugares vivos que lo citan, este archivo, `Especificacion-Funcional.md` §11 y `../03-UX-UI-DX/README.md` §6. **Ningún artefacto se agrega ni se omite y el orden de lectura no cambia.** Sube minor. |
| 1.4 | 2026-08-12 | **Cierra el residuo vivo de `P2-1`** de `SDD/Docs/Audit/H-Final-Consolidado-r1.md` §4, reportado también como `N-01` en `Coherencia-Corpus-r2.md`. §5 decía «**quince** de ellas no lo están» sobre las quince rutas, cuando son **catorce**: la del canje de credenciales la declara una fuente. El mismo archivo ya decía lo correcto en su §7, de modo que se contradecía consigo mismo. Es la última de las dos líneas del hallazgo; la otra se corrigió el 2026-08-11 en el `README.md` de `03-UX-UI-DX`. Ninguna decisión, contrato ni caso de prueba cambia. **Autor:** Orquestador SDD |
| 1.4 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
| 2.0 | 2026-08-16 | **Consolidación 8.5** (`Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2). Los casos de uso pasan de **doce a nueve**, uno por capacidad de la unidad de entrega: **cuatro de los doce no eran casos de uso de esta unidad** y se reubicaron —traducción del motivo, composición, arranque y la colección reproducible—, y los ocho restantes **agrupaban por perfil de autenticación y por recurso**, criterio transversal a las capacidades, de modo que `CU-00003` y `CU-00006` se reparten en tres y dos capacidades. Los identificadores nuevos empiezan en `CU-00021` porque **los absorbidos no se reciclan**. §1 declara la carpeta `_legacy/` de la consolidación, §2 lista los nueve, §3 rehace el orden de lectura sobre el recorrido de la persona, y §5 actualiza el ámbito de los identificadores. La cabecera pasa de «proyecto de código» a **unidad de entrega**. Sube major. |
