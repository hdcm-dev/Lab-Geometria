# Reporte — Lo que los dieciséis samples encontraron

**Producto:** Fábrica de Geometría
**Documento:** Reporte-Hallazgos-De-Los-Samples-2026-08-30.md
**Versión:** 1.0
**Estado:** Abierto — **nueve hallazgos, ninguno decidido**
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

## 3. Los dos defectos

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

## 4. Los tres huecos entre el código y su contrato

### 4.1 `H-03` — Hay un punto HTTP expuesto que el contrato REST no declara

**Dónde:** `POST /interpretaciones`, en `WorkEndpoints.cs`, con nombre `InterpretWork`.

**Qué pasa.** El servicio expone **diecisiete operaciones sobre trece rutas**. `Contratos-REST.md` declara **dieciséis** puntos de acceso —`A-01` a `A-17`, sin el `A-04`, retirado—. La que sobra está implementada, exige acceso firmado y papel `Alumno`, tiene sus comentarios de diseño escritos… y **no figura en la tabla**.

**No es un punto olvidado en el código: es un punto olvidado en el contrato.** Lo mide `samples/api/03-avanzado`, contando sobre el documento OpenAPI que el propio servicio publica.

### 4.2 `H-04` — `Issue` devuelve el mismo `null` para dos fallas de clase distinta

**Dónde:** `AccessTokenIssuer.Issue`.

**Qué pasa.** Devuelve `null` cuando falta la clave de firma **y** cuando faltan reclamos, junto con otros tres casos. Quien lo llama no puede distinguirlos.

**Por qué no son la misma clase de falla.** Reclamos incompletos es un **pedido mal armado**: el llamador tiene el defecto y lo puede corregir. Clave de firma ausente es un **despliegue mal configurado**: nadie que pida un acceso lo puede arreglar, y el servicio no debería estar atendiendo. `scripts/store-path.sh` cuenta que este producto **ya eligió detenerse en el arranque ante configuración faltante**, por exactamente ese motivo. Acá el arranque sigue y la falla aparece, mucho después, como un acceso que no se emite.

**Lo que la fuente esperaba.** `ejemplo-03-avanzado-infraestructura.md` §6 pide `SIGNING_KEY_MISSING` e `INCOMPLETE_CLAIMS`, dos códigos que no existen.

### 4.3 `H-05` — La capa de infraestructura declara dos códigos tipados y sus ejemplos le piden seis

**Dónde:** `InfrastructureConditionCode`, que declara `UNREADABLE_PASSWORD_HASH` y `RANDOMNESS_SOURCE_UNAVAILABLE`.

**Qué pasa.** `ejemplo-03-avanzado-infraestructura.md` §6 nombra seis rechazos tipados; el sample sólo puede exhibir **uno**. Cuatro de los códigos **no existen** —las fallas viajan como `null`— y el segundo que sí existe no es provocable desde un sample.

**No es el mismo caso que `infrastructure/02`.** Allá los códigos existían con otro nombre y en otra capa —`DELETION_WITHOUT_WORK_CASCADE`, `ADMINISTRATOR_ALREADY_CONFIGURED`, `ORIGINAL_JSON_ALTERED`, los tres en el dominio—. Acá no existen.

## 5. Los dos que se ven hacia afuera

### 5.1 `H-06` — El mensaje del arranque detenido lleva una traza, y nombra el síntoma

**Dónde:** el arranque, ante un almacén de linaje desconocido.

**Dos tercios de `RA-03` se cumplen.** El mensaje **no lleva la ruta del almacén ni ninguna dirección**, que era lo delicado. Pero lleva la **traza de pila entera**, tal cual sale del proveedor.

**Y hay algo peor que la traza, que §6 no pide.** El mensaje dice `table "Account" already exists`. Ése es el **síntoma**. La causa —un linaje que el servicio no entiende— no aparece por ningún lado. Quien despliega lee lo primero y sale a buscar una tabla duplicada.

**El arranque sí se detiene**, que es lo que `US-00028` exige, y con cero peticiones atendidas durante todo el intento.

### 5.2 `H-07` — Los `401` de autenticación vuelven sin código de contrato

**Qué pasa.** Los tres —sin acceso, acceso vencido, firma ajena— responden con `Content-Length: 0`. Los emite la tubería de autenticación, **antes de que corra una línea de código del producto**, así que la traducción de errores nunca los ve.

**No es una fuga:** no hay nada adentro que pueda filtrarse. Pero rompe la uniformidad que `ejemplo-01-basico-api.md` §6 daba por sentada, **y justo en las respuestas que un cliente ve más seguido**: las de la sesión vencida.

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
| `H-01` | La selección rechazada que borra la vigente. **Es un defecto y no tiene lectura alternativa** | Equipo, con corrección |
| `H-02` | Si «apagar un movimiento» significa detener o volver | Product Owner |
| `H-03` | Qué se hace con `POST /interpretaciones`: entra al contrato o sale del código | Product Owner |
| `H-04` | Si las dos fallas de `Issue` deben distinguirse, y si la clave ausente debe detener el arranque como ya hace el almacén | Product Owner |
| `H-05` | Si la capa de infraestructura debe declarar los códigos que sus ejemplos le piden, o si los ejemplos deben decir lo que la capa hace | Product Owner |
| `H-06` | La traza en el mensaje del arranque detenido, y que nombre el síntoma en vez de la causa | Equipo |
| `H-07` | Si los `401` de la tubería deben llevar código de contrato | Product Owner |
| `H-08` | **Qué se hace con los tres §6 del visor, y si el barrido de alcance debe incluir la categoría 10 por regla** | Product Owner |
| `H-09` | El código `UNKNOWN` acuñado aguas abajo | Equipo |

**Ninguno impide seguir.** Los dieciséis samples corren y los dieciséis declaran por escrito lo que no coincide.

## 9. Una observación sobre el instrumento

**Los samples encontraron más en los documentos que en el código.** De los nueve hallazgos, **dos** son defectos de código; los otros siete son huecos entre lo que el producto hace y lo que sus contratos y ejemplos afirman.

Eso repite un patrón que la mesa del 2026-08-27 ya había nombrado desde otro lado: **el corpus está mejor auditado que los instrumentos que lo auditan**. Acá la forma es otra —los ejemplos son el instrumento, y son ellos los que quedaron atrás— pero la causa es la misma: **lo que se escribe antes que el código no tiene quien lo vuelva a leer cuando el código cambia**, salvo que alguien lo corra.

Correr los dieciséis samples es exactamente eso: volver a leerlos, con el producto contestando.

---

## Control de cambios

| Versión | Fecha | Cambio |
| --- | --- | --- |
| 1.0 | 2026-08-30 | Emisión. Nueve hallazgos de la implementación de los dieciséis samples, más tres divergencias investigadas y cerradas como no-hallazgo. Ninguno decidido. |
