# Informe de auditoría — Fase B · GeometriaFactory-Domain · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Proyecto de código auditado | `GeometriaFactory-Domain` |
| `tipo_proyecto_codigo` (D8) | `library` |
| Fase | B — categorías **02-Especificacion-Funcional** y **03-UX-UI-DX** |
| Alcance de la ronda | **Acotado**: verificación del cierre de los trece hallazgos de r1, con foco en que la resolución de P1-01 —cambio de modelo que tocó doce documentos y renombró un código de condición— no haya roto nada. No se reauditan la estructura completa ni los criterios de §6 de las dos reglas, salvo donde las correcciones pudieran haberlos alterado |
| Categoría 04 | Omitida por gating (`usa_llm` == false). Su ausencia **no** es hallazgo |
| Insumos normativos | `Rules-Especificacion-Funcional.md` §3.3, §4.2, §4.3, §5.3; `Rules-UX-UI-DX.md` §4.2.4, §6; `Vocabulario-Rules.md` §9 y §10; `Master-Prompt.md` §5, §10 y §15 |
| Insumos de contexto | `B-02-03-GeometriaFactory-Domain-r1.md` (leído, **no modificado**); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.3; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.1 |
| Auditor | Auditor independiente de fase — Arquitecto de Soluciones + QA Senior. Sin participación en la generación ni en la corrección de la Fase B, ni en la ronda 1 |
| Fecha | 2026-08-09 |
| Ronda | r2 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Tabla de cierre de hallazgos de r1](#2-tabla-de-cierre-de-hallazgos-de-r1)
- [3. Verificación de la resolución de P1-01](#3-verificación-de-la-resolución-de-p1-01)
- [4. Verificación del fundamento de P2-02](#4-verificación-del-fundamento-de-p2-02)
- [5. Verificaciones de forma](#5-verificaciones-de-forma)
- [6. Hallazgos nuevos](#6-hallazgos-nuevos)
- [7. Veredicto y condiciones para promover](#7-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Los trece hallazgos de r1 están **cerrados**: uno de ellos, P3-03, con un defecto nuevo menor de referencia cruzada. La resolución de P1-01 —la posición de una figura no reconstruida **queda reservada** y el conjunto de piezas adoptadas **admite huecos**— es coherente en los tres artefactos que se contradecían, y su fundamento se sostiene contra el anexo `§20.E-5` del intake, que esta auditoría abrió y contrastó en lugar de creerle a la declaración. El recuento del catálogo, rehecho desde cero, da **exactamente 37 condiciones distintas** con coincidencia byte a byte entre las §6 de los once casos de uso, las entradas de `DX-Error-Messages.md` §3 y las filas de su §6.2; el renombre de `POSICION_DE_PIEZA_NO_CONTIGUA` a `POSICION_DE_PIEZA_INVALIDA` está completo y el identificador viejo sólo sobrevive donde corresponde.

Ningún documento subió de versión y ninguna corrección abrió fila nueva de control de cambios. Se abren **cuatro hallazgos nuevos, los cuatro P3**, ninguno bloqueante.

**Veredicto: APROBADO CON OBSERVACIONES.** El proyecto de código puede avanzar a la Fase C.

---

## 2. Tabla de cierre de hallazgos de r1

Enum de `estado`: `cerrado` · `cerrado parcialmente` · `abierto` · `cerrado con defecto nuevo`.

| # | Hallazgo de r1 | Estado | Evidencia textual verificada en esta ronda |
| --- | --- | --- | --- |
| **P1-01** | Contradicción sobre E-5 entre `CU-06` FA-03, `CU-07` §6/CA-03 y `DX-Error-Messages.md` §3.7 | **cerrado** | Ver §3 de este informe. Los tres lugares dicen lo mismo y ninguno conserva la formulación vieja: `CU-06` FA-03 «El dominio no adopta esa pieza, y **su posición queda reservada**: no se reasigna a ninguna otra pieza y sigue perteneciendo al rango de posiciones del conjunto raíz»; `CU-07` §6 `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` «La posición indicada **no pertenece al rango de posiciones del conjunto raíz interpretado**»; `DX-Error-Messages.md` §3.7 idéntico, más «**Una posición reservada no es una posición inexistente**» |
| **P2-01** | `CU-10` FA-02 remitía la eliminación a CU-09 | **cerrado** | `CU-10` §5 FA-02: «Lo que el administrador sí puede hacer es eliminar el trabajo, que es **CU-11**». La formulación vieja no sobrevive en ningún artefacto vivo |
| **P2-02** | `RN-05` §4 declaraba `TRANSICION_DE_TRABAJO_NO_ADMITIDA`, que ninguna §6 declara | **cerrado** | `RN-05` §4: «**No hay ninguna otra forma de llegar a estado `Pendiente`**, y por eso esta regla no acuña un código de rechazo propio: el envío es la única transición hacia ese estado y la decide el dominio, de modo que no existe una operación de "forzar el paso" que se pueda rechazar», con remisión a `ENVIO_FUERA_DE_BORRADOR` y `TRANSICION_DESDE_ESTADO_TERMINAL` de `CU-08` §6. El identificador ya no vive como código: su única ocurrencia viva es la fila de control de cambios 1.1 de `RN-05`. Fundamento verificado en §4 de este informe |
| **P3-01** | Trazabilidad CU → RN incompleta en `CU-02`, `CU-05` y `CU-06` | **cerrado** | `CU-02` §9: «RN-01, RN-07, **RN-06** —este caso de uso es donde el estado de cuenta cambia…—»; `CU-05` §9: «RN-08, RN-04 …, **RN-10** en cuanto a que el contenido de un trabajo terminal tampoco se reedita»; `CU-06` §9: «RN-08, RN-09 …, **RN-10** en cuanto a que el contenido de un trabajo terminal no cambia» |
| **P3-02** | Dos atribuciones de regla del catálogo contradecían el ámbito de `RN-09` | **cerrado** | `DX-Error-Messages.md` §6.2 pone «—» en las dos filas (`POSICION_DE_PIEZA_INVALIDA` \| CU-06 \| — \| — \| Rechazo; `ADVERTENCIA_SIN_LOS_DOS_VALORES` \| CU-07 \| — \| — \| Rechazo) y agrega la tabla de origen real: `PRODUCT-INTAKE` §17.1.P.11 punto 2 para la primera y «`NB-00005` §5, tercer criterio de éxito» para la segunda. **El ámbito invocado es el que `RN-09` §3 efectivamente declara**, verificado en el archivo: «No se aplica a las observaciones de especie advertencia de discrepancia de valor, que llevan su propia exigencia». `CU-06` §9 suma la fila «Origen de la identidad posicional \| **No es RN-09.** … RN-09 gobierna la **ubicación de la observación**, no la identidad de la pieza» |
| **P3-03** | `Guia-Onboarding-Developer.md` desplazaba tres secciones obligatorias | **cerrado con defecto nuevo** | Las **seis secciones obligatorias de `Rules-UX-UI-DX.md` §4.2.4 recuperaron su numeración**, verificado sobre los encabezados: `## 1. Audiencia y prerrequisitos`, `## 2. Instalación o acceso`, `## 3. Primer ejemplo ejecutable`, `## 4. Diagnóstico de problemas frecuentes en la primera hora`, `## 5. Próximos pasos`, `## 6. Control de cambios`; el contenido insertado quedó al final como `## 7. Dónde va una regla nueva`. Las referencias cruzadas **externas** están actualizadas (`DX-Developer-Experience.md` §1.2 «§7, con su procedimiento de decisión en §7.3», §4 «`Guia-Onboarding-Developer.md` §7», §4 in fine «§5»). Las **internas** están actualizadas en §1 («Es lo que la §7 de esta guía enseña») y en §5 («la §7 de esta guía»), pero **una quedó colgando**: ver N-01 |
| **P3-04** | Los once CU numeraban §12 lo que la regla numera §17 | **cerrado** | Barrido de encabezados de primer nivel sobre los once archivos: los once terminan en `## 17. Compatibilidad de la superficie pública`, con la tabla de contenido actualizada. Ningún `## 12.` sobrevive en el árbol vivo; sí en los snapshots, donde corresponde |
| **P3-05** | La columna «Dónde se ejerce» recortaba los CU de tres reglas | **cerrado** | Resuelto por una vía distinta de la recomendada y correcta: la columna **se retiró**. `Guia-Onboarding-Developer.md` §7.1 tiene ahora tres columnas —Regla, Enunciado abreviado, Invariante— y el control de cambios lo declara: «La tabla de §7.1 pierde la columna "Dónde se ejerce", redundante con la §9 de cada caso de uso». Sin columna no hay recorte posible |
| **P3-06** | Nombres de comandos y rutas frente al criterio de stacks | **cerrado** | `DX-Developer-Experience.md` §3.1 in fine: «Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y **conservan su forma literal porque el lector los tiene que poder ejecutar**. Las rutas y los nombres de script salen de `PRODUCT-INTAKE` §16 y el proyecto de prueba, de §17.1.P.6: no se eligen acá». Contrastado contra el intake: §16 declara literalmente `scripts/`→`build.sh, … test.sh`, `.devcontainer/devcontainer.json`, `GeometriaFactory.sln` y `tests/GeometriaFactory.Domain.Tests`, y §17.1.P.6 confirma el proyecto de prueba. **La solución es razonable** y se evalúa como tal en §6, nota de cierre |
| **P3-07** | Los snapshots no llevaban estado `Superado` ni nota a la versión vigente | **cerrado** | Los **veinte** snapshots de este proyecto de código llevan el bloque de archivado en su primera línea, con los tres datos que la política pide. Cuerpos intactos. Detalle en §5.3 |
| **P3-08** | `CU-08` §10 remitía la eliminación sólo a CU-09 | **cerrado** | `CU-08` §10, última viñeta: «La eliminación no es una transición de estado y no vive acá: la del alumno, acotada al `Borrador`, está en **CU-09**, y la del administrador, que alcanza los tres estados que ve, en **CU-11**» |
| **P3-09** | El catálogo no declaraba el quick-start como no aplicable | **cerrado** | `DX-Error-Messages.md` §6.3 abre con «**Quick-start: no aplicable en este documento, y el motivo es explícito**», remite el quick-start único a `DX-Developer-Experience.md` §3 y cierra con «**No se da por cumplido: se declara no aplicable**». Se resolvió en el catálogo y no en el `README.md` de 03, que era la otra vía sugerida; el criterio queda igualmente declarado y no dado por cumplido |
| **P3-10** | «Rol» con dos referentes y una sola declaración | **cerrado** | `Glosario-Funcional.md` suma `### 3.4 Rol` con los dos referentes en tabla: «Atributo del alumno … → **"papel", siempre**» y «Función que un actor cumple dentro de un caso de uso → **"rol"**, y **sólo** como encabezado de la columna de la tabla de actores, que es donde la regla lo impone». La entrada «Papel» de §2 remite: «"rol" queda reservado al encabezado normativo de la tabla de actores: ver §3.4» |

**Recuento de cierre:** 13 de 13 cerrados · 1 de ellos con defecto nuevo · 0 abiertos · 0 cerrados parcialmente.

Se deja constancia de que **no se reportan** las polisemias que r1 evaluó y descartó —observación, comentario, `Pendiente` en sus tres excepciones, estado, pieza desnuda, mensaje—, por el criterio negativo de `Vocabulario-Rules.md` §9.1, ni la decisión de no renombrar `RN-04` y `RN-05`, que r1 evaluó y validó y que `Especificacion-Funcional.md` §8 punto 3 sigue declarando.

---

## 3. Verificación de la resolución de P1-01

Las cuatro comprobaciones se hicieron por separado, en el orden que sigue.

### 3.1 Comprobación 1 — Los tres lugares dicen lo mismo, y ninguno conserva la formulación vieja

| Artefacto | Texto vigente | Formulación de 1.0 | ¿Sobrevive? |
| --- | --- | --- | --- |
| `CU-06` §5 FA-03 | «El dominio no adopta esa pieza, y **su posición queda reservada**: no se reasigna a ninguna otra pieza y sigue perteneciendo al rango de posiciones del conjunto raíz, de modo que una observación puede ubicarse en ella (RN-09). El conjunto de piezas adoptadas queda con un hueco en esa posición y las demás **conservan la suya**» | «deja constancia de que la posición correspondiente quedó sin reconstruir» | **No** |
| `CU-07` §6 | «`OBSERVACION_SOBRE_PIEZA_INEXISTENTE` \| La posición indicada **no pertenece al rango de posiciones del conjunto raíz interpretado**: designa una figura que el texto del alumno no trae \| Rechaza el conjunto. **Una posición reservada no es una posición inexistente**» | «La posición de pieza indicada no existe en el conjunto de piezas del trabajo» | **No.** La formulación vieja se verificó viva **sólo** en `Casos-De-Uso/_legacy/2026-08-09/CU-07-…-v1.0.md` línea 90, donde corresponde |
| `DX-Error-Messages.md` §3.7 | Mensaje: «La posición indicada no pertenece al rango de posiciones del conjunto raíz interpretado». Causa: «La observación designa una figura que el texto del alumno no trae». Acción: «**Una posición reservada no es una posición inexistente**: la de una figura que no se pudo reconstruir sí pertenece al rango y sí admite observación … (CU-06 FA-03, CU-07 FA-04)» | «La observación referencia una posición que la reconstrucción no adoptó» | **No** |

**Refuerzos coherentes que la corrección agregó, verificados uno por uno:** `CU-07` §3 segundo guion («La posición que trae cada observación es la de la figura **en el conjunto raíz del texto**, no la de una pieza adoptada»); `CU-07` §4 paso 4 («que esa posición pertenezca al rango del conjunto raíz interpretado, **esté o no adoptada** la pieza correspondiente»); `CU-07` FA-04, nuevo, que declara el caso de E-5 como admisible y «no un defecto de coherencia»; `CU-07` §10, nota nueva; `CU-06` §7 postcondición de éxito («El conjunto puede tener huecos … y esas posiciones quedan reservadas»); `Definicion-Modelo-De-Dominio.md` §2.2 («Conjunto de piezas … **Admite huecos**»), §2.3 («**No se recalcula** … aunque otras figuras del mismo conjunto no se hayan podido reconstruir») y §2.5 («Es la posición **en el texto** … debe pertenecer al rango del conjunto raíz»).

**Barrido de residuos.** No queda en el árbol vivo ninguna prosa que exija contigüidad de las posiciones de pieza. Las cinco ocurrencias vivas de «contigu» son otra cosa: tres sobre la serie `RN-01` a `RN-11`, una sobre la posición del **componente** dentro de su pieza —`Definicion-Modelo-De-Dominio.md` §2.4, «Obligatoria y contigua desde 0»—, que es correcta y no colisiona porque los componentes no admiten huecos, y una dentro del mensaje nuevo de `POSICION_DE_PIEZA_INVALIDA`.

### 3.2 Comprobación 2 — El fundamento contra el escenario canónico del intake

Se abrió `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §20.E-5 y se contrastó contra la decisión, en lugar de aceptar la declaración de los documentos.

| Lo que el anexo declara | Verificación |
| --- | --- |
| El array raíz tiene **dos** elementos: `{"Tipo": "Cubo", …}` y `{"Tipo": "Piramide", …}` | ✔ transcripto en el bloque de código de §20.E-5 |
| «El primer elemento del array es válido **a propósito**: obliga a que el índice reportado sea **1 y no 0**, que es la forma de comprobar que el índice se calcula y no se informa siempre el primero» | ✔ literal en «Qué ejercita» |
| «Qué verificar», punto 2: «El mensaje indica **índice de figura 1** y **campo `Tipo`**» | ✔ |
| «Qué verificar», punto 3: «La primera pieza, que es válida, **se interpreta igual**: un error en un elemento no descarta el resto» | ✔ |
| `§17.1.P.11` punto 2: «**La identidad de la pieza es su índice en el array raíz**, porque el JSON no trae identificador» | ✔ |

**La decisión satisface el escenario, y la alternativa no.** Con la base 0 que `CU-06` §4 paso 3 fija («empezando en 0»), la figura de tipo desconocido es la de índice 1. Si el conjunto de piezas adoptadas se cerrara sin huecos, quedaría una sola pieza en la posición 0 y la observación de E-5 no tendría dónde ubicarse: o se la renumeraría a 0 —y el índice reportado sería 0, exactamente lo que el anexo dice que el escenario existe para descartar—, o se la rechazaría con `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`, que era la contradicción de r1. Reservar la posición es la única de las tres lecturas que produce «índice 1, campo `Tipo`» **y** conserva la primera pieza. El fundamento declarado en `CU-06` §10 in fine —«El escenario E-5 lo verifica: su primera figura es válida a propósito, para que el índice reportado sea 1 y no 0»— **es una transcripción fiel del anexo, no una racionalización posterior**.

**Los criterios de aceptación materializan el escenario y son consistentes entre sí:** `CU-06` CA-04 («un conjunto raíz de 2 figuras cuya figura de posición 1, de tipo `Piramide`, no se pudo reconstruir → El dominio adopta 1 pieza, la de posición 0, y **la posición 1 queda reservada**»), `CU-07` CA-03 («adopta 1 observación de especie error de validación, **con posición de pieza 1** y campo `Tipo`») y `Definicion-Modelo-De-Dominio.md` §2.5, ejemplo de instancia («posición de pieza 1 y campo `Tipo`; el trabajo queda en `Borrador`»). `CU-06` CA-05 —«un conjunto raíz de 2 figuras entregado con una pieza en la posición 5 → rechaza con `POSICION_DE_PIEZA_INVALIDA`»— es coherente con la base 0: el rango válido es 0..1.

### 3.3 Comprobación 3 — El atributo nuevo está completo

| Lugar | Estado |
| --- | --- |
| Modelo de dominio | **Presente.** `Definicion-Modelo-De-Dominio.md` §2.2, entidad Trabajo: «Cantidad de figuras del conjunto raíz \| Cuántas figuras trae el texto interpretado, **incluidas las que no se pudieron reconstruir** \| Es el rango de posiciones válidas del trabajo. Sin ella, una observación sobre una figura no reconstruida no tendría contra qué validarse (RN-09)» |
| `CU-06`, que lo recibe | **Declarado como entrada en los dos lugares que corresponden.** §3, segunda precondición: «El resultado de la interpretación llega con las piezas en el orden del conjunto raíz del texto del alumno, y declara **cuántas figuras trae ese conjunto raíz**, incluidas las que no se pudieron reconstruir». §4 paso 1: «La capa de aplicación entrega al trabajo el conjunto de piezas interpretadas **y la cantidad de figuras del conjunto raíz**» |
| `CU-06`, justificación | **Presente.** §10 in fine: «Por eso … la cantidad de figuras del conjunto raíz llega como dato: sin ella no habría rango contra el que validar una posición» |
| `CU-07`, que lo consume | **Consumido sin declararlo.** Ver **N-03** |
| Control de cambios | **Declarado.** `Definicion-Modelo-De-Dominio.md` 1.1: «§2.2 suma el atributo "cantidad de figuras del conjunto raíz", que es el rango contra el que se valida una posición»; `CU-06` 1.1: «§3 y el paso 1 suman la cantidad de figuras del conjunto raíz como dato de entrada» |

**Veredicto de la comprobación 3:** el atributo está incorporado donde se origina y donde se guarda, con su semántica y su motivo; queda un hueco menor de declaración en el caso de uso que lo lee. No es una incorporación a medias del modelo: es una precondición no enunciada, y se reporta como P3.

### 3.4 Comprobación 4 — El renombre y el recuento del catálogo, recontado desde cero

**Extracción mecánica**, hecha sobre la §6 de los once casos de uso **antes** de leer el recuento que declara el catálogo:

| CU | Filas en su §6 | CU | Filas en su §6 |
| --- | --- | --- | --- |
| CU-01 | 4 | CU-07 | 4 |
| CU-02 | 3 | CU-08 | 4 |
| CU-03 | 4 | CU-09 | 3 |
| CU-04 | 3 | CU-10 | 4 |
| CU-05 | 4 | CU-11 | 3 |
| CU-06 | 4 | **Total** | **40** |

| Magnitud | Valor obtenido por esta auditoría | Valor que declara `DX-Error-Messages.md` §6.1 | Coincide |
| --- | --- | --- | --- |
| Filas de condición en las once §6 | 40 | 40 | ✔ |
| Condiciones repetidas en dos CU | 3: `DATO_OBLIGATORIO_AUSENTE`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `OPERACION_DESCONOCIDA` | 3, exactamente ésas | ✔ |
| **Condiciones distintas** | **37** | **37** | ✔ |
| Identificadores distintos en `DX-Error-Messages.md` §3 | **37** | — | ✔ |
| Filas de la tabla de cobertura §6.2 | **37** | — | ✔ |
| Diferencia de conjuntos §6 de los CU ↔ §3 del catálogo | **conjunto vacío en los dos sentidos** | 0 inventadas, 0 faltantes | ✔ |
| Diferencia de conjuntos §6 de los CU ↔ §6.2 | **conjunto vacío en los dos sentidos** | — | ✔ |

**La taxonomía sigue cerrando después del renombre.** El recuento por categoría de §2.1 —17 entrada inválida, 3 recurso ausente, 13 conflicto de estado, 4 conflicto de facultad— se rehizo sobre §3 descontando las tres entradas de segunda declaración: da 17 / 3 / 13 / 4, suma 37. `POSICION_DE_PIEZA_INVALIDA` conserva la categoría «entrada inválida» de su antecesora y `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` conserva «recurso ausente» pese a la reescritura de su causa, que es lo correcto: sigue designando algo que no existe, sólo que ahora contra el rango del conjunto raíz.

**El renombre está completo y sin referencias colgando.** Barrido de todas las cadenas en mayúsculas con guion bajo sobre el árbol vivo de 02 y 03:

| Identificador retirado | Ocurrencias vivas | Ubicación | ¿Corresponde? |
| --- | --- | --- | --- |
| `POSICION_DE_PIEZA_NO_CONTIGUA` | 3 | Fila de control de cambios 1.1 de `CU-06`; fila de la tabla de identificadores retirados de `DX-Error-Messages.md` §6.1; fila de control de cambios 1.0 de `DX-Error-Messages.md` | **Sí**, los tres son de los dos lugares admitidos |
| `RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` | 2 | Fila de control de cambios 1.1 de `CU-06`; tabla de identificadores retirados de §6.1 | **Sí** |
| `TRANSICION_DE_TRABAJO_NO_ADMITIDA` | 1 | Fila de control de cambios 1.1 de `RN-05` | **Sí**. No figura en la tabla de identificadores retirados de §6.1 y no debe figurar: esa tabla es de **renombres** de condiciones del catálogo, y éste nunca fue una condición del catálogo |

No existe ninguna ocurrencia de los tres como código vivo: ni en una §6, ni en el catálogo §3, ni en la tabla de cobertura §6.2, ni en prosa de ninguna regla de negocio. `DX-Error-Messages.md` §6.1 in fine lo declara y esta auditoría lo confirma: «El renombre **no altera el recuento**: sigue habiendo 37 condiciones distintas, una de ellas con nombre nuevo».

---

## 4. Verificación del fundamento de P2-02

El fundamento declarado es que `TRANSICION_DE_TRABAJO_NO_ADMITIDA` describía forzar una transición que en este dominio no existe. Se verificó contra la máquina de estados, no contra la declaración.

`Definicion-Modelo-De-Dominio.md` §5.2 declara cuatro estados y **siete transiciones**, y la tabla las enumera con su sujeto. La única entrada a estado `Pendiente` es «`Borrador` → `Pendiente` \| El alumno \| Envío cuyo texto verifica: ninguna observación de especie error de validación. Las advertencias no lo impiden (RN-05)». La propiedad 1 de la misma sección lo refuerza: «Guardar y enviar se unificaron en **una sola acción, enviar** (F-22)». Entre las transiciones inadmisibles figura «el retorno de `Pendiente` a `Borrador`, que ninguna fuente declara».

**Consecuencia verificada:** el dominio no expone ninguna operación de fijar estado. El estado de destino del envío lo decide el propio dominio a partir de las observaciones —`CU-07` §4 paso 7: «El dominio deja disponible, para el consumidor, si el trabajo tiene al menos una observación de especie error de validación, que es la condición que gobierna el envío»—, de modo que **no hay invocación posible que falle por «forzar el paso a `Pendiente`»**. Las dos invocaciones que sí se rechazan alrededor del envío están declaradas en `CU-08` §6 y son `ENVIO_FUERA_DE_BORRADOR` y `TRANSICION_DESDE_ESTADO_TERMINAL`, que es exactamente lo que `RN-05` §4 remite. El análogo para la otra máquina de estados, `TRANSICION_DE_CUENTA_NO_ADMITIDA`, sí existe y sí está declarado en `CU-02` §6, lo que confirma que el retiro es una asimetría deliberada del modelo y no un olvido.

**El fundamento se sostiene.** El retiro sin reemplazo es la resolución correcta, y es además la que respeta el procedimiento que `Guia-Onboarding-Developer.md` §7.3 paso 4 fija: una condición entra al catálogo desde la §6 de un caso de uso y no desde una regla.

---

## 5. Verificaciones de forma

### 5.1 Versionado — ningún documento subió de versión

Barrido del campo `Versión` de los treinta y un documentos vivos:

| Grupo | Versión esperada | Versión hallada | Resultado |
| --- | --- | --- | --- |
| Los veinte de 02 con emisión previa (`CU-01` a `CU-09`, `Especificacion-Funcional.md`, `Definicion-Modelo-De-Dominio.md`, `Glosario-Funcional.md`, `README.md`, `RN-01`, `RN-03`, `RN-04`, `RN-05`, `RN-07`, `RN-08`, `RN-09`) | 1.1 | **1.1** en los veinte | ✔ |
| Los seis nuevos de 02 (`CU-10`, `CU-11`, `RN-02`, `RN-06`, `RN-10`, `RN-11`) | 1.0 | **1.0** en los seis | ✔ |
| Los cinco de 03 | 1.0 | **1.0** en los cinco | ✔ |

Ninguno lleva sufijo de versión en el nombre del archivo vivo.

### 5.2 Control de cambios — sin filas nuevas

Recuento de filas de versión por documento: los veinte de 02 en 1.1 tienen **exactamente dos** filas (1.0 y 1.1); los seis nuevos y los cinco de 03 tienen **exactamente una** (1.0). **Ninguna fila nueva.** Las correcciones se absorbieron ampliando la fila existente, con la marca explícita de la intervención: catorce documentos contienen la cadena «**Corrección de la ronda r1 del audit**» dentro de su fila vigente —`CU-01` a `CU-11`, `Definicion-Modelo-De-Dominio.md`, `Glosario-Funcional.md`, `RN-05`, `DX-Developer-Experience.md`, `DX-Error-Messages.md` (dos veces) y `Guia-Onboarding-Developer.md`—, y en los tres artefactos de 03 la fórmula usada es «Corrección de la ronda r1 del audit, **sobre esta misma emisión**», que es la forma correcta de absorber una corrección sin abrir versión sobre un documento que nunca salió de 1.0.

### 5.3 Los veinte snapshots y su bloque de archivado

Verificado archivo por archivo sobre `02-Especificacion-Funcional/_legacy/2026-08-09/` (4), `Casos-De-Uso/_legacy/2026-08-09/` (9) y `Reglas-De-Negocio/_legacy/2026-08-09/` (7) — **veinte en total**, que es el número exacto de documentos de 02 con emisión previa:

| Comprobación | Resultado |
| --- | --- |
| Bloque de archivado en la primera línea | **20 / 20**: «> **Artefacto archivado — estado `Superado`**» |
| Estado `Superado` declarado | **20 / 20**: «- **Estado:** `Superado`» |
| Versión que preserva | **20 / 20**: «- **Versión que preserva:** 1.0» |
| Enlace a la versión vigente | **20 / 20**, con ruta relativa correcta a `../../<nombre>.md` |
| Cláusula de intangibilidad | **20 / 20**: «El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro» |
| **Cuerpo no modificado** | **20 / 20**. Los veinte conservan `**Versión:** 1.0`, `**Fecha:** 2026-08-08` y `**Estado:** Propuesto` en el cuerpo —el bloque de archivado se antepone y no reescribe la cabecera, que es lo que la política exige—; ninguno contiene la cadena «ronda r1 del audit»; y las formulaciones superadas siguen ahí donde deben estar: `POSICION_DE_PIEZA_NO_CONTIGUA` en `CU-06-…-v1.0.md`, `TRANSICION_DE_TRABAJO_NO_ADMITIDA` en `RN-05-…-v1.0.md`, «La posición de pieza indicada no existe en el conjunto de piezas del trabajo» en `CU-07-…-v1.0.md` línea 90, y la numeración `## 12.` en los nueve casos de uso archivados |

La categoría 03 no tiene `_legacy/` y es correcto: nunca se había emitido, y su `README.md` §2 lo declara.

---

## 6. Hallazgos nuevos

**P0: ninguno · P1: ninguno · P2: ninguno · P3: cuatro.**

### P3 — bajos

#### N-01 · Referencia cruzada colgando en `Guia-Onboarding-Developer.md` §2, dejada por la renumeración de P3-03

- **Archivo y sección:** `SDD/Docs/Proyectos/GeometriaFactory-Domain/03-UX-UI-DX/Guia-Onboarding-Developer.md` §2, párrafo de cierre.
- **Evidencia textual:** «Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido y se pasa a §3. Si alguno falla, **la §5 tiene los tres arranques que fallan de verdad**».
- **Por qué es un defecto:** la sección con los arranques que fallan es la de diagnóstico, que la corrección devolvió a **§4** —sus tres primeras filas son «Un comando del quick-start no existe en el host», «`./scripts/build.sh` termina en 0 pero con advertencias» y «La batería de dominio tarda notablemente más de 10 segundos»—. La §5 vigente es «Próximos pasos», una tabla de Diátaxis que no contiene ningún arranque fallido. La referencia era correcta con la numeración anterior y quedó sin actualizar. El control de cambios del propio documento afirma que «las referencias cruzadas de los dos documentos que la citaban se corrigieron»: se corrigieron las externas y las otras dos internas, y esta se pasó por alto, de modo que la afirmación del control de cambios es levemente más amplia que el hecho.
- **Impacto:** cae en el punto exacto del recorrido donde un lector cuyo quick-start falló va a saltar, y lo manda a la sección equivocada.
- **Recomendación:** reemplazar «la §5» por «la §4» en `Guia-Onboarding-Developer.md` §2. Es una palabra y no exige subir versión ni abrir fila nueva.

#### N-02 · `CU-06` no cita `§20.E-5` en su trazabilidad upstream, siendo el escenario sobre el que descansa toda la corrección

- **Archivo y sección:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md`, cabecera, campo **Trazabilidad upstream**.
- **Evidencia textual:** «`PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (RN-08 y RN-09), §4.2 …, §17.1.P.2 (INV-07), §17.1.P.11 (puntos 1, 2 y 4), **§20.E-1, §20.E-2, §20.E-6, §20.E-7**». `§20.E-5` no figura.
- **Por qué es un defecto:** el cuerpo de la versión 1.1 apoya en E-5 cuatro afirmaciones cargadas: FA-03 lo cita explícitamente («PRODUCT-INTAKE §20.E-5»), CA-04 es el escenario E-5 entero convertido en criterio de aceptación, §9 lo lista entre los tests previstos («E-1, E-2, **E-5**, E-6 y E-7») y §10 in fine lo invoca como la verificación que justifica la decisión de modelo. La omisión venía de 1.0 —verificado en el snapshot, cuya cabecera tampoco lo lista—, pero en 1.0 E-5 aparecía sólo de pasada en FA-03; la corrección lo volvió el fundamento del caso de uso sin actualizar la cabecera. `Rules-Especificacion-Funcional.md` §3.3 pide trazabilidad upstream con secciones concretas, y `CU-07`, que apoya en el mismo escenario, sí lo cita.
- **Impacto:** bajo. No hay contradicción: la remisión existe en el cuerpo. Lo que falla es la cabecera como índice de dependencias, que es lo que 05 y 08 leen para saber qué fuentes vuelven a revisar si el intake cambia.
- **Recomendación:** agregar `§20.E-5` a la lista de escenarios de la cabecera de `CU-06`.

#### N-03 · `CU-07` valida contra el atributo nuevo sin declararlo entre sus precondiciones

- **Archivo y secciones:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md` §3 y §4 paso 4.
- **Evidencia textual:** §4 paso 4: «El dominio verifica que toda observación de especie error de validación indique la posición de la figura y el campo (RN-09), y **que esa posición pertenezca al rango del conjunto raíz interpretado**, esté o no adoptada la pieza correspondiente». §6, `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`, rechaza precisamente cuando la posición cae fuera de ese rango. Las cuatro precondiciones de §3, en cambio, nombran el trabajo, el conjunto de piezas reconstruido, la posición de cada observación, la especie y los dos valores de la advertencia; **ninguna nombra la cantidad de figuras del conjunto raíz**, que es el dato que define ese rango.
- **Por qué es un defecto:** `CU-06` sí lo declara como entrada, en §3 y en el paso 1, y el atributo vive en el Trabajo por `Definicion-Modelo-De-Dominio.md` §2.2, de modo que la información no se pierde y no hay contradicción. Lo que falta es que el contrato de uso enuncie de qué depende su guarda. Agrava el punto la primera precondición de §3, que admite un camino sin reconstrucción previa —«salvo en las observaciones que expresan que la reconstrucción no fue posible»—: en ese camino el trabajo puede no tener aún cantidad de figuras del conjunto raíz, y el contrato no dice qué ocurre entonces con la guarda de rango. La lectura razonable es que en ese camino toda observación cae en FA-02 y se adopta sin posición, pero es una inferencia del lector y no un enunciado del documento.
- **Impacto:** bajo. 08 puede escribir la prueba de CA-03 sin ambigüedad; lo que queda impreciso es el borde de un trabajo sin reconstrucción.
- **Recomendación:** sumar una precondición a `CU-07` §3 —que el trabajo conoce la cantidad de figuras de su conjunto raíz, que es el rango contra el que se valida la posición— y una aclaración de una línea sobre el camino sin reconstrucción, remitiendo a FA-02.

#### N-04 · Dos documentos de 03 declaran «catorce diagnósticos» donde la tabla tiene trece

- **Archivos y secciones:** `.../03-UX-UI-DX/Guia-Onboarding-Developer.md` §6, fila de control de cambios 1.0, y `.../03-UX-UI-DX/README.md` §2, fila de `Guia-Onboarding-Developer.md`.
- **Evidencia textual:** control de cambios de la guía: «… **catorce diagnósticos** de la primera hora y el enlace explícito a los cuatro modos de Diátaxis». `README.md` §2: «… dónde va una regla nueva y **catorce diagnósticos** frecuentes».
- **Verificación:** la tabla de `Guia-Onboarding-Developer.md` §4 tiene **trece** filas de síntoma, contadas una por una desde «Un comando del quick-start no existe en el host» hasta «Se busca el nombre exacto de un tipo o de un espacio de nombres».
- **Por qué es un defecto menor:** es una afirmación cuantitativa sobre el propio artefacto, del mismo tipo que las que este proyecto de código verifica con rigor en el catálogo de errores. Viene de la emisión 1.0 y no la introdujo la corrección de r1, pero sobrevive en el árbol vivo y se propaga a un segundo documento. No es una afirmación sobre el estado del sistema y no cae bajo D9: es un recuento de contenido propio, comprobable a la vista.
- **Recomendación:** corregir a «trece» en los dos lugares, o agregar la fila que falte si la intención era catorce.

### Nota de cierre sobre la solución de P3-06 — **no es hallazgo**

Se evaluó expresamente si conservar `dotnet test tests/GeometriaFactory.Domain.Tests`, `./scripts/build.sh`, `./scripts/test.sh`, `.devcontainer/` y `GeometriaFactory.sln` incumple el criterio de `Rules-UX-UI-DX.md` §6 sobre menciones a stacks concretos. **La solución adoptada es razonable y no se reporta.** Tres motivos, los tres verificados:

1. **Las rutas salen del propio intake y no se eligen en 03.** `PRODUCT-INTAKE` §16 fija literalmente `GeometriaFactory.sln`, `.devcontainer/devcontainer.json`, `scripts/` con `build.sh` y `test.sh`, y `tests/GeometriaFactory.Domain.Tests`; §17.1.P.6 confirma ese proyecto de prueba. Borrarlas no eliminaría la dependencia: la movería a otro documento.
2. **El criterio apunta al dominio fuente del framework**, que es lo que D7 protege, y la categoría 02 —donde vive el vocabulario del dominio— sigue enteramente libre de stack, verificado en el barrido de r1 y no alterado por las correcciones.
3. **La tensión está declarada y resuelta con criterio.** Los dos documentos nombran cada paso por su papel antes de mostrarlo, y declaran de dónde sale la forma literal. Borrarla dejaría un quick-start no ejecutable, incumpliendo el criterio vecino de la misma §6 que pide «snippet ejecutable y reproducible» — y el criterio de verificación por punto de control de `DX-Developer-Experience.md` §3.2 dejaría de tener objeto.

---

## 7. Veredicto y condiciones para promover

### 7.1 Recuento

| Nivel | r1 | Cerrados en r2 | Nuevos en r2 | Abiertos al cierre de r2 |
| --- | --- | --- | --- | --- |
| **P0** bloqueante | 0 | — | **0** | **0** |
| **P1** alto | 1 | 1 | **0** | **0** |
| **P2** medio | 2 | 2 | **0** | **0** |
| **P3** bajo | 10 | 10 | **4** | **4** |
| **Total** | 13 | 13 | 4 | 4 |

### 7.2 Veredicto

## **APROBADO CON OBSERVACIONES**

No hay ningún P0 y no queda ningún P1 ni P2. Las cuatro verificaciones que el encargo señala como críticas dieron resultado limpio y se dejan explícitas:

1. **Los tres artefactos que se contradecían dicen ahora lo mismo**, y ninguno conserva la formulación vieja en el árbol vivo: las tres formulaciones superadas viven sólo en `_legacy/2026-08-09/` y en filas de control de cambios.
2. **El fundamento de la decisión de modelo se sostiene contra el anexo `§20.E-5` del intake**, verificado abriéndolo: el escenario pone su primera figura válida a propósito para que el índice reportado sea 1 y no 0, y reservar la posición es la única de las tres lecturas posibles que produce ese resultado sin descartar la primera pieza.
3. **El atributo «cantidad de figuras del conjunto raíz» está en el modelo de dominio y declarado como entrada en `CU-06`**, con su motivo; el único hueco es que `CU-07` no lo enuncia entre sus precondiciones, y se reporta como P3.
4. **El renombre está completo y el catálogo recontado desde cero cierra en 37**, con coincidencia exacta y diferencia de conjuntos vacía en los dos sentidos entre las §6 de los once casos de uso, `DX-Error-Messages.md` §3 y su tabla de cobertura §6.2. El identificador viejo sobrevive sólo en filas de control de cambios y en la tabla de identificadores retirados.

Se agrega que **el fundamento del retiro de `TRANSICION_DE_TRABAJO_NO_ADMITIDA` también se sostiene**, verificado contra la máquina de estados del trabajo: el envío es la única entrada a estado `Pendiente`, el destino lo decide el dominio y no existe operación de forzar transición que pueda rechazarse.

### 7.3 Condiciones para promover

**Bloqueantes de la promoción: ninguna. El proyecto de código `GeometriaFactory-Domain` puede avanzar a la Fase C.**

Los cuatro P3 son de redacción y de completitud de cabecera; ninguno cambia una decisión de especificación ni afecta lo que 05, 06 u 08 van a derivar de estos artefactos. Se recomienda absorberlos en una sola pasada, **sin subir versión y sin abrir filas nuevas de control de cambios**, por ser la misma intervención:

1. **N-01** — reemplazar «la §5» por «la §4» en `Guia-Onboarding-Developer.md` §2. Es la única de las cuatro que puede desorientar a un lector en su primer recorrido.
2. **N-02** — agregar `§20.E-5` a la trazabilidad upstream de `CU-06`.
3. **N-03** — sumar la precondición del rango a `CU-07` §3, con la aclaración del camino sin reconstrucción.
4. **N-04** — corregir «catorce» por «trece» en los dos lugares.

Si se corrigen, la fila de control de cambios vigente de cada documento absorbe la mención, como ya se hizo con las correcciones de r1. Si no se corrigen, ninguna condición de promoción queda incumplida.

---

**Fin del informe B-02-03-GeometriaFactory-Domain-r2.**
