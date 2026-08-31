# Reporte — Lo que los dieciséis samples encontraron

**Producto:** Fábrica de Geometría
**Documento:** Reporte-Hallazgos-De-Los-Samples-2026-08-30.md
**Versión:** 4.0
**Estado:** Abierto — **cuatro vivos de doce emitidos**. Cinco cerrados, dos retirados tras verificarlos contra el contrato, y tres nuevos que salieron de esa verificación
**Fecha:** 2026-08-30
**Autor:** Orquestador SDD
**Instrumento:** La implementación de los dieciséis samples de las categorías `10-Examples`, corrida contra el producto real
**Alcanza a:** `visor/src/viewer/instance.ts`; `Contratos-REST.md`; `Definicion-Contrato-De-Fachada.md`; `AccessTokenIssuer`; `TwoPhaseStartup`; las categorías `10-Examples` de las dos unidades de entrega

---

## 1. Por qué existe este documento

Entre el 2026-08-27 y el 2026-08-30 se implementaron los **dieciséis samples** que tienen documento que los gobierna. Ninguno se escribió contra un doble del producto salvo donde el propio contrato lo pedía: los de dominio y aplicación corren el código real, los de infraestructura abren SQLite de verdad, los de `api` levantan el servicio, y los del visor conducen un navegador con capacidad gráfica.

**El resultado no es el interesante; el residuo sí.** **Cinco** cierran exactos contra su §6 y **once** con divergencias declaradas; al ordenar esas once aparecen **nueve hallazgos** que no son del sample sino del producto o de sus documentos. Este reporte los enumera para que se decidan, y **no los decide**.

**Lo que este documento NO es.** No es una auditoría —no hubo auditor independiente— y no es un plan. Es el residuo de una implementación, escrito por quien la hizo.

## 2. El recuento, para leer el resto en contexto

| Grupo | Samples | Contra §6 |
| --- | --- | --- |
| `domain` | 3 | los tres coinciden |
| `application` | 3 | 11/12 · 13/14 · 14/15 |
| `infrastructure` | 3 | 13/13 · 9/14 · 12/18 |
| `api` | 3 | 11/13 · 22/23 · 7/10 |
| `visor` | 3 | 7/9 · 9/15 · 14/17 |
| `web` | 1 | **13/13, sin divergencias** |

**Cinco cierran exactos**: los tres de dominio, `infrastructure/01-basico` y `web/01-datos-seed`.

**Los dos extremos dicen lo mismo desde lados opuestos.** Los de dominio cierran exactos porque el dominio **no depende de nada**: ninguna decisión de arquitectura posterior lo alcanzó. `web/01-datos-seed` cierra exacto porque es el documento **más nuevo**, el que menos decisiones atravesó desde que se escribió. Los que más divergen —los tres del visor— son los que quedaron entre una decisión y la otra.

## 3. Los dos defectos — **los dos cerrados el 2026-08-30**

Son los únicos dos hallazgos donde el producto hace algo que ninguna fuente respalda. Los dos son del visor y **los dos son el mismo patrón**: el efecto ya ocurrió cuando se decide sobre él.

### 3.1 `H-01` — Una selección rechazada borra la vigente

**Dónde:** `visor/src/viewer/instance.ts`, `ViewerInstance.select`.

**Qué pasa.** El método recorre **todas** las mallas apagando el resalte de las que no coinciden con la posición pedida, y recién al terminar el recorrido descubre que ninguna coincidía. Devuelve `false`, la fachada informa `INDEX_OUT_OF_RANGE` — y para entonces el resalte que había ya se apagó.

**Qué dice la fuente.** `ejemplo-02-intermedio.md` §6, línea `[11]`: *«seleccion vigente conservada=si»*.

**Cómo se ve.** El cuadro posterior al rechazo no es el anterior. Lo mide `samples/visor/02-intermedio/tests/lectura-y-seleccion.mjs`.

**Por qué importa más de lo que parece.** El caso llega desde la interfaz: un índice que el árbol muestra pero la escena no dibujó. La persona hace clic en una fila legítima y **pierde el resaltado que tenía**, sin ningún mensaje que lo explique.

### 3.2 `H-02` — Apagar un movimiento no deshace lo que hizo

**Dónde:** `visor/src/viewer/instance.ts`, el bucle de dibujo.

**Qué pasa.** Con `pieceSpin` apagado el bucle deja de incrementar `mesh.rotation.y`, y nada más: las piezas se quedan en la orientación en la que estaban. Con `cameraOrbit` apagado, la cámara se queda en el ángulo al que llegó.

**Qué dice la fuente.** `ejemplo-03-avanzado.md` §6, línea `[7]`: *«piezas de vuelta en su orientacion de partida=si»*.

**Lo que hace falta decidir.** Si «apagar» significa **detener** o significa **volver**. Las dos son defendibles; hoy el código hace la primera y el ejemplo afirma la segunda, y **nada declara cuál es la buena**. La decisión alcanza a los dos movimientos por igual.

## 4. Los huecos entre el código y su contrato

### 4.1 `H-03` — Hay un punto HTTP expuesto que el contrato REST no declara

**Dónde:** `POST /interpretaciones`, en `WorkEndpoints.cs`, con nombre `InterpretWork`.

**Qué pasa.** El servicio expone **diecisiete operaciones sobre trece rutas**. `Contratos-REST.md` declara **dieciséis** puntos de acceso —`A-01` a `A-17`, sin el `A-04`, retirado—. La que sobra está implementada, exige acceso firmado y papel `Alumno`, tiene sus comentarios de diseño escritos… y **no figura en la tabla**.

**No es un punto olvidado en el código: es un punto olvidado en el contrato.** Lo mide `samples/api/03-avanzado`, contando sobre el documento OpenAPI que el propio servicio publica.

### 4.2 ~~`H-04` — `Issue` devuelve el mismo `null` para dos fallas de clase distinta~~ — **RETIRADO**

**No es un hallazgo, y el error es de método: se midió un componente aislado y se afirmó del producto.**

`CompositionRoot` **ya detiene el arranque** si la clave de firma falta o es más corta que el mínimo, con un mensaje que nombra la llave y nunca el valor. En la aplicación compuesta, la rama de clave ausente de `Issue` **es inalcanzable**.

El sample `infrastructure/03` la alcanzó porque construye `AccessTokenIssuer` **directamente** — legítimo para un sample de capa. Lo que la versión 1.0 llamó «dos fallas indistinguibles del producto» es **una rama defensiva de un componente**, de la misma familia que `H-09` y que `NON_DRAWABLE_TYPE`.

**Queda como constancia y no se borra**: se emitió, se verificó, no procede.

### 4.3 `H-05` — La capa de infraestructura declara dos códigos tipados y sus ejemplos le piden seis

**Dónde:** `InfrastructureConditionCode`, que declara `UNREADABLE_PASSWORD_HASH` y `RANDOMNESS_SOURCE_UNAVAILABLE`.

**Qué pasa.** `ejemplo-03-avanzado-infraestructura.md` §6 nombra seis rechazos tipados; el sample sólo puede exhibir **uno**. Cuatro de los códigos **no existen** —las fallas viajan como `null`— y el segundo que sí existe no es provocable desde un sample.

**No es el mismo caso que `infrastructure/02`.** Allá los códigos existían con otro nombre y en otra capa —`DELETION_WITHOUT_WORK_CASCADE`, `ADMINISTRATOR_ALREADY_CONFIGURED`, `ORIGINAL_JSON_ALTERED`, los tres en el dominio—. Acá no existen.

## 5. Los que se ven hacia afuera

### 5.1 `H-06` — El mensaje del arranque detenido lleva una traza, y nombra el síntoma

**Dónde:** el arranque, ante un almacén de linaje desconocido.

**Dos tercios de `RA-03` se cumplen.** El mensaje **no lleva la ruta del almacén ni ninguna dirección**, que era lo delicado. Pero lleva la **traza de pila entera**, tal cual sale del proveedor.

**Y hay algo peor que la traza, que §6 no pide.** El mensaje dice `table "Account" already exists`. Ése es el **síntoma**. La causa —un linaje que el servicio no entiende— no aparece por ningún lado. Quien despliega lee lo primero y sale a buscar una tabla duplicada.

**El arranque sí se detiene**, que es lo que `US-00028` exige, y con cero peticiones atendidas durante todo el intento.

### 5.2 ~~`H-07` — Los `401` de autenticación vuelven sin código de contrato~~ — **RETIRADO**

**No es un hallazgo: el contrato lo declara, y el que estaba mal era el ejemplo.**

`Contratos-REST.md` **§5.1** se titula «Las dos respuestas sin código del contrato» y declara exactamente las dos que el sample midió:

| Respuesta | Por qué no lleva código, según el contrato |
| --- | --- |
| `401` de la guardia | *«El conjunto cerrado no declara ninguno que describa una credencial ausente o inválida, y esta capa no inventa códigos»* |
| `400` de petición ilegible | *«Ocurre antes de que la petición llegue a ser el tipo del contrato: no hay contrato con el que hablar todavía»* |

Y cierra: ***«Las dos son deliberadas y se declaran para que su ausencia de código no se lea como un olvido.»***

**El hallazgo real es el inverso del reportado**: el §6 del ejemplo `01-basico-api` esperaba `6 de 6` y **contradecía al contrato de su propia unidad**. Se corrigió el documento —pasó a **1.1**— y el sample cierra en **13 de 13**.

**Lección de método:** un sample que discrepa del producto obliga a leer **las dos** fuentes antes de decidir cuál está mal. La versión 1.0 leyó una.

## 5bis. Los tres que salieron de verificar los dos retirados

Al golpear la superficie de error completa **en los dos entornos** —que es lo que la versión 1.0 no hizo— aparecieron tres cosas que ningún sample medía.

### 5bis.1 `H-10` — La garantía de `RA-03` dependía de una variable de entorno — **CERRADO**

Ante un cuerpo ilegible el servicio respondía **distinto según el entorno**: en `Production` un `400` vacío; en `Development`, `text/plain` con `BadHttpRequestException` **y el nombre de un tipo interno del producto**.

§5.4 prohíbe *«nombres de tipos internos»* **y no admite excepción por entorno**. Un despliegue arrancado con `ASPNETCORE_ENVIRONMENT=Development` filtraba, **y nadie se enteraba**, porque en la máquina de desarrollo siempre se vio así.

**Decidido por el Product Owner:** que los dos entornos se comporten igual. El detalle no se pierde, **se muda al registro**, que es donde el propio §5.4 dice que tiene que estar.

### 5bis.2 `H-11` — No había manejador de excepciones — **CERRADO**

Un defecto no previsto respondía `500` **con el cuerpo vacío**. §4 le pide «nunca lleva detalle de implementación» —se cumplía— y §5.4 pide, para lo que no se puede decir, **«el código genérico, con su código de respuesta»**. Vacío no es el genérico: `UNCLASSIFIED_ERROR` existe y por esa vía no llegaba nunca.

Se cerró con `ContractErrorHandler`, que **no toca** el `401`, el `404`, el `405` ni el `415`: los dos primeros están declarados sin código, los otros son del protocolo y no del producto. Darles un código haría crecer un conjunto cerrado por motivos ajenos al producto.

**Quedó fijado con cuatro pruebas**, una sobre la decisión que más fácil se revierte por descuido: que el `400` de petición ilegible **siga yendo sin cuerpo**.

### 5bis.3 `H-12` — Un apartamiento que vive en un comentario de código — **ABIERTO**

`ContractTranslation.cs` declara en su cabecera un **apartamiento** con su fundamento: `UNCLASSIFIED_ERROR` sale con `409` en dos motivos y no con `500`, porque «un `500` le diría a la persona que el producto falló cuando lo que pasa es que la operación no procede sobre esa cuenta». **El fundamento se sostiene.**

Dos cosas no:

- **`Root-Rules.md` §11 pide que un apartamiento sea un ADR con seis campos**, no un comentario en un archivo de código. Un comentario no tiene estado, ni disparadores que lo superen, ni cuenta de saltos de versión sobrevividos — y es exactamente lo que el reporte `19` al framework describe desde otro lado.
- **`Contratos-REST.md` §5.2 afirma que el genérico «bajó de cuatro destinos a dos».** Tiene **tres**: `503`, `409` y `500`. El documento dice algo que el código contradice, y no lo sabe.

**Se propone** emitir el ADR y corregir el recuento — **no** retirar el `409`.

## 6. El hallazgo de fondo: una decisión que se propagó hacia arriba y no hacia abajo

### 6.1 `H-08` — El barrido de alcance de `ADR-08006` no llegó a `10-Examples`

**Éste explica la mitad de las divergencias del visor, y es el más importante del reporte.**

[`Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md`](Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md) 4.0 —**cerrada**, con sus tres decisiones tomadas y sus tres escrituras aplicadas— enumeró qué quedaba desalineado cuando el visor dejó de recibir el texto del alumno. Alcanzó `PRODUCT-INTAKE` §20.E-7, §20.E-8 y `Requerimientos-Tecnicos.md` §8.3.

**No menciona `10-Examples` ni una vez.** Se verificó con `grep`: cero ocurrencias de `10-Examples`, `ejemplo-0` y `samples/` en todo el documento.

**El resultado es que los tres §6 del visor siguen describiendo la fachada anterior**: dicen que se carga un texto, que el visor devuelve la estructura de ese texto para el árbol, y que enumera como no dibujada una figura que hoy **el laboratorio rechaza antes** y nunca le llega. La observación §2.2 llega a decir, con todas las letras, que esa pieza «no llega al visor» — y el ejemplo que la afirma quedó sin tocar.

**Lo que hay que decidir no es si `ADR-08006` está bien.** Está tomada y es correcta. Lo que hay que decidir es **qué se hace con los tres §6 que quedaron atrás**, y si el barrido de alcance de una decisión debe incluir la categoría 10 por regla y no por criterio de quien lo hace.

**Y hay un efecto colateral medido:** `NON_DRAWABLE_TYPE` quedó **sin camino**. El laboratorio sólo reconstruye los seis tipos que el visor dibuja; el séptimo del dominio, `RectanguloDesarrollado`, existe únicamente como componente, y puesto como figura raíz el laboratorio lo **rechaza** —se probó—. La guarda del visor es defensa en profundidad y hoy no cubre ningún caso alcanzable.

### 6.2 `H-09` — Un código acuñado aguas abajo del contrato

**Dónde:** `visor/src/viewer/instance.ts`, `reason ?? 'UNKNOWN'`.

`ejemplo-03-avanzado.md` §6 línea `[15]` exige **cero** códigos acuñados fuera de los siete del contrato de fachada. Hay uno: `UNKNOWN`, como respaldo. **Hoy no es alcanzable** —`meshFor` siempre pone motivo cuando no hay malla— y sigue siendo un código que el contrato no declara.

*(El mismo renglón revela que el bundle declara seis de los siete códigos del contrato. El que falta es `UNREADABLE_TEXT`, y **no es un hallazgo**: era el código del texto del alumno, que la fachada ya no recibe. Es `H-08` otra vez.)*

## 7. Lo que NO es un hallazgo, y conviene decirlo

Tres divergencias se investigaron y **se cierran acá**, porque el producto tiene razón y el ejemplo no:

| Divergencia | Por qué el producto tiene razón |
| --- | --- |
| `QUERY_WITHOUT_DECLARED_SCOPE` no existe (`infrastructure/02`) | `IWorkRepository` declara por escrito que ninguna operación de listado se puede pedir sin recorte. **Una operación que no existe es más fuerte que un rechazo en ejecución**: no compila |
| El `503` de `A-16` no tiene camino (`api/03`) | Es la consecuencia de que el producto eligiera **detenerse en el arranque** en vez de atender degradado. La rama existe y no se puede alcanzar porque el proceso no llega a escuchar |
| La global suelta `__THREE__` (`visor/03`) | No la pone el producto: la registra el motor gráfico al cargarse, para avisar si hay dos copias suyas en la página |

Y una contradicción de documento, sin consecuencia sobre el código: `ejemplo-02-intermedio-api.md` §6 dice *«Pasos de la coleccion: 3»* mientras sus propias líneas van de `[1]` a `[8]` y su §5 declara ocho archivos.

## 8. Qué se pide decidir

| # | Hallazgo | Quién decide |
| --- | --- | --- |
| ~~`H-01`~~ | **CERRADO.** La comprobación va antes del efecto; el sample `visor/02` lo verifica | Hecho |
| ~~`H-02`~~ | **CERRADO.** El Product Owner decidió que apagar es **detener**; el §6 del ejemplo pasó a 2.0 | Hecho |
| `H-03` | Qué se hace con `POST /interpretaciones`: entra al contrato o sale del código | Product Owner |
| ~~`H-04`~~ | **RETIRADO.** El arranque ya se detiene sin clave; la rama es defensiva e inalcanzable | — |
| ~~`H-05`~~ | **CERRADO.** Los ejemplos dicen lo que la capa hace; no se agregan cuatro códigos sin consumidor | Hecho |
| ~~`H-06`~~ | **CERRADO.** Detenerse pasó a ser una decisión, con salida `78` | Hecho |
| ~~`H-07`~~ | **RETIRADO.** El contrato declara esas dos respuestas sin código. Se corrigió el ejemplo |  — |
| ~~`H-08`~~ | **CERRADO en el producto.** Los cuatro §6 pasaron a 2.0 y sus samples cierran sin divergencias; la observación de `ADR-08006` pasó a 5.0 con la cuarta afirmación. **Lo que sigue abierto es del framework**: `Reporte 21` | Hecho / framework |
| ~~`H-09`~~ | **CERRADO.** Retirado, y la unión discriminada lo volvió imposible | Hecho |
| ~~`H-10`~~ | **CERRADO.** Los dos entornos se comportan igual; el detalle va al registro | Hecho |
| ~~`H-11`~~ | **CERRADO.** El defecto no previsto sale con el código genérico | Hecho |
| `H-12` | El apartamiento del `409` como ADR, y el recuento de §5.2 del contrato | Product Owner |

**Ninguno impide seguir.** Los dieciséis samples corren y los dieciséis declaran por escrito lo que no coincide.

**Y dos de los doce no eran hallazgos.** Se dejan tachados y con su motivo en lugar de borrarlos: un reporte que hace desaparecer lo que se equivocó no deja aprender de qué se equivocó.

## 8bis. `H-13` — Siete de los nueve samples de .NET no verificaban con su comando documentado — **CERRADO**

**Se encontró el 2026-08-30, al terminar de alinear los §6, y es el hallazgo más incómodo de la serie: estaba en el instrumento con el que se hicieron todos los demás.**

`domain` ×3, `application` ×3 e `infrastructure/01` corrían su comparación **sólo con `-- --verificar`**. El comando que el §4 de cada documento declara —y que el contrato de verificación de su §9 cita— **no pasa esa bandera**. Corridos como está documentado, imprimían sus renglones y **devolvían cero sin comparar nada**.

**Un instrumento que se lee como verde sin haber verificado**, que es exactamente el defecto que estos samples existen para encontrar en otros lados.

**Lo primero que se hizo fue comprobar si alguna afirmación previa era falsa.** Se corrieron los siete con la bandera puesta: `domain` ×3 e `infrastructure/01` en CONFORME, y `application` ×3 con **una** divergencia cada uno — exactamente lo que se había reportado. **Ninguna afirmación previa era falsa**, pero ninguna estaba respaldada por el comando documentado.

**Cerrado**: la comparación corre siempre, y las tres divergencias de `application` resultaron ser tres errores de §6 de tres naturalezas distintas —una comprobación que esa capa no hace ni debe hacer, un código de la segunda barrera sobre un recorrido que se detiene en la primera, y un código del dominio donde correspondía el de la aplicación—. Los tres documentos pasaron a 2.0.

**Los nueve samples de .NET cierran hoy en CONFORME con el comando de su documento.**

## 9. Una observación sobre el instrumento

**Y el último hallazgo fue sobre los samples mismos.** `H-13` estaba en el instrumento: siete de los nueve no verificaban con su comando documentado. Lo encontró terminar de arreglar todo lo demás, que es la única forma en que un instrumento se audita a sí mismo.

**Los samples encontraron más en los documentos que en el código.** De los nueve hallazgos, **dos** son defectos de código; los otros siete son huecos entre lo que el producto hace y lo que sus contratos y ejemplos afirman.

Eso repite un patrón que la mesa del 2026-08-27 ya había nombrado desde otro lado: **el corpus está mejor auditado que los instrumentos que lo auditan**. Acá la forma es otra —los ejemplos son el instrumento, y son ellos los que quedaron atrás— pero la causa es la misma: **lo que se escribe antes que el código no tiene quien lo vuelva a leer cuando el código cambia**, salvo que alguien lo corra.

Correr los dieciséis samples es exactamente eso: volver a leerlos, con el producto contestando.

---

## Control de cambios

| Versión | Fecha | Cambio |
| --- | --- | --- |
| 2.0 | 2026-08-30 | **Dos hallazgos se retiran, tres entran, y cinco pasan a cerrados.** `H-07` no era un hallazgo: `Contratos-REST.md` §5.1 declara las dos respuestas sin código, deliberadamente y por escrito; el que contradecía al contrato era el §6 del ejemplo, corregido a 1.1 y con su sample en 13/13. `H-04` tampoco: `CompositionRoot` ya detiene el arranque sin clave de firma, de modo que la rama de `Issue` es defensiva e inalcanzable en la aplicación compuesta, y el sample la alcanzó construyendo el emisor directamente. **Los dos errores son del mismo método** —leer una sola fuente, y medir un componente aislado para afirmar del producto— y por eso se dejan tachados en lugar de borrados. De verificarlos salieron **`H-10`**, **`H-11`** y **`H-12`**: los dos primeros ya cerrados, el tercero abierto. `H-01`, `H-06` y `H-09` pasan a cerrados. Sube **major**: el conjunto de hallazgos cambia. |
| 1.0 | 2026-08-30 | Emisión. Nueve hallazgos de la implementación de los dieciséis samples, más tres divergencias investigadas y cerradas como no-hallazgo. Ninguno decidido. |
| 3.0 | 2026-08-30 | **`H-02`, `H-05` y `H-08` cerrados.** Los cuatro §6 que describían algo que el producto no hace —los tres del visor y el de `03-avanzado-infraestructura`— pasaron a **2.0**, y sus cuatro samples cierran **sin divergencias**. En los cuatro se corrigió **el documento y no el producto**, con un motivo escrito por línea. `H-02` llevó una decisión del Product Owner: **«apagar» un movimiento significa detener y no volver**, porque la cámara la puede haber movido la persona y `F-25` gobierna los dos movimientos de forma simétrica. La observación de `ADR-08006` pasó a **5.0** declarando que su alcance era de **cuatro** afirmaciones y no de tres. **Del reporte quedan vivos `H-03` y `H-12`.** Sube **major**. |
| 4.0 | 2026-08-30 | **Entra `H-13`, ya cerrado, y es el más incómodo de la serie: estaba en el instrumento.** Siete de los nueve samples de .NET corrían su comparación **sólo detrás de `--verificar`**, y el comando que su documento declara no la pasa: devolvían **cero sin comparar nada**. Se verificó primero si alguna afirmación previa era falsa —**ninguna lo era**— y después se hizo que la comparación corra siempre. Las tres divergencias de `application` que eso destapó resultaron **tres errores de §6 de tres naturalezas distintas**, y sus documentos pasaron a 2.0. Se alineó además el §6 de `infrastructure/02`, que nombraba cuatro códigos inexistentes —tres viven en el dominio con otro nombre y uno **no existe en ninguna capa**, con su ausencia declarada por escrito en el puerto—. **Los dieciséis samples cierran hoy sin divergencias, con el comando de su documento.** Sube **major**. |
