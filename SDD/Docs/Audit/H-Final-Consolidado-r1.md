# Auditoría final consolidada del producto · Fase H · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-h-consolidacion`, commit `58255c8` |
| Alcance auditado | **El corpus entero**: `SDD/Docs/` —nivel producto, `Audit/` y los siete proyectos de código—, `SDD/Intake/`, `SDD/Maquetas/` y `/samples/`. **854 archivos versionados bajo `SDD/`** |
| Fuentes normativas | `Root-Rules.md` **3.1** —**§6 es la lista de verificación principal de este informe**—, `Rules-Documentacion.md` **4.1**, `Vocabulario-Rules.md` **2.1**, en `IA.SDD/SDD/Devs/Rules/`, de sólo lectura |
| Fuentes del producto | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** y `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** |
| Motivo de la ronda | Auditoría final que precede al traspaso a codificación. No audita una fase: dictamina si el corpus es apto para que alguien empiece a construir a partir de él |
| Criterio de la ronda | **Ningún recuento heredado y ninguna cita a través de un tercero.** Cada magnitud se recontó sobre el instrumento que la produce; cada cita de peso se contrastó abriendo el documento original, no el que lo cita. La forma se verificó con programa sobre el árbol, no leyendo |
| Fuera de alcance | `_legacy/` (documentos superados); las tres fuentes del intake, que viven en otro repositorio bajo `PROMPTs/`; el contenido de los 65 artefactos de la categoría 11, que **no existen** por definición del Momento 1 |
| Auditor | Auditor independiente, sin participación en la generación de ninguna fase |
| Fecha | 2026-08-11 |
| Dictamen | **APTO PARA HANDOFF** — ver §4 |

---

## Tabla de contenido

- [1. La lista de `Root-Rules.md` §6, ítem por ítem](#1-la-lista-de-root-rulesmd-6-ítem-por-ítem)
- [2. Hallazgos por severidad](#2-hallazgos-por-severidad)
- [3. Los tres puntos que la Fase H declaró, dictaminados](#3-los-tres-puntos-que-la-fase-h-declaró-dictaminados)
- [4. Dictamen final](#4-dictamen-final)
- [5. Al Product Owner: qué riesgo real corre quien empiece a construir](#5-al-product-owner-qué-riesgo-real-corre-quien-empiece-a-construir)
- [6. Lo que no reporto, y lo que no verifiqué](#6-lo-que-no-reporto-y-lo-que-no-verifiqué)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. La lista de `Root-Rules.md` §6, ítem por ítem

Los quince criterios de aceptación del entregable, contra `SDD/Docs/README.md` **1.0**.

| # | Criterio de §6 | Estado | Cómo lo comprobé |
| --- | --- | --- | --- |
| 1 | Tabla de proyectos de código con tipo D8, rol, dependencias, proyecto principal, reflejando el `PRODUCT-MANIFEST` sin divergencias | **Cumple** | Comparé fila por fila §2 del README contra §2 del manifiesto **1.3**, abierto directamente. Los siete tipos D8 coinciden (`rest-api` 1, `web-monolith` 1, `library` 5); las siete columnas de dependencias coinciden literalmente —`Api`←{Application, Infrastructure, Contracts}, `Web`←{Contracts, Visor}, `Application`←{Domain}, `Infrastructure`←{Application, Domain}, y `Domain`, `Contracts`, `Visor` sin dependencias—; `redistribuible` es false en los siete en los dos documentos; `GeometriaFactory-Api` está señalado como principal en los dos. **Cero divergencias** |
| 2 | Tabla A enlaza 00, 01, `Producto/` y la carpeta de cada proyecto de código, con path correcto | **Cumple** | Las once filas de §4 resuelven: verifiqué las once rutas con `os.path.exists` desde el directorio del README. Están las dos categorías de nivel producto, `Producto/`, `Audit/` y los siete `Proyectos/<Nombre>/` |
| 3 | Composición reflejada en la cabecera | **Cumple** | Cabecera: «7 proyectos de código (ver tabla de proyectos de código)» y «Proyecto de código principal: `GeometriaFactory-Api`». Conté 7 filas en §2 y 7 carpetas bajo `Proyectos/` |
| 4 | Flujo de lectura diferenciado para al menos 3 roles en la Tabla B, con justificación por rol | **Cumple, con margen** | §5 tiene **cuatro** roles —Product Owner, desarrollador que retoma, auditor o QA, operador— cada uno con orden y con columna «Por qué» no vacía. Mínimo tres, hay cuatro |
| 5 | Glosario rápido con mínimo 10 términos del dominio, una línea cada uno | **Cumple** | Conté **21** filas en §9. Ninguna definición excede una línea. El anti-patrón de §4.5 acota a 10–20 términos: veintiuno queda **una fila por encima** de la banda recomendada, lo cual no es incumplimiento del criterio de §6, que sólo fija piso. Ver P3-4 |
| 6 | Todos los enlaces internos apuntan a rutas existentes; no hay enlaces rotos | **Cumple** | Programa sobre el README: **20** enlaces relativos, **20** resuelven. Extendido al corpus entero, ver el ítem correspondiente de §2 |
| 7 | Cabecera con el bloque obligatorio de §4.1, todos los campos completos | **Cumple** | Los ocho campos de §4.1 están, en el mismo orden y con el mismo rótulo: Producto, Versión del documento, Estado, Fecha, Stack principal, Composición, Proyecto de código principal, Documento. Ninguno vacío. §4.1 aclara que el README raíz **no** declara bloque de trazabilidad, y no lo declara |
| 8 | Entre 200 y 400 líneas | **Cumple, al borde** | `wc -l` = **202**. Dentro de la banda por dos líneas |
| 9 | Sin emojis, negritas decorativas ni términos del dominio prohibido por D7 | **Cumple** | Barrido Unicode de los rangos de emoji sobre los cuatro artefactos de la fase: cero. Barrido de la lista D7 —`Motor DSL`, `ESC-POS`, `Bluetooth`, `NuGet`, `impresora térmica`, `MAUI` fuera del literal D8— sobre los cuatro: **cero ocurrencias**. Las negritas del documento marcan la afirmación de cierre de cada apartado y no rótulos de tabla ni conectores: son de énfasis, no decorativas |
| 10 | Control de cambios al pie con al menos una entrada inicial v1.0 | **Cumple** | §11 tiene una fila, `1.0 · 2026-08-11`, con descripción y autor |
| 11 | Estado del enum cerrado | **Cumple** | `Propuesto`, que pertenece al enum |
| 12 | Términos que la categoría acuña, en el glosario rápido, que enlaza a los de categoría sin reemplazarlos | **Cumple** | §9 abre declarando «No reemplaza a los glosarios de categoría» y enlaza a `Vision-Producto.md` §9 y a la categoría 02 de cada proyecto de código. Los términos que esta categoría precisa y usa en más de un apartado —«proyecto de código», «unidad desplegable», «etapa», «punto de control», «puerta técnica»— están los cinco en el glosario |
| 13 | Ninguna forma desnuda de un término polisémico sin resolver, en artefacto que se lee por secciones | **Cumple** | Los dos polisémicos vivos del corpus están resueltos en el punto de uso. «Proyecto» aparece siempre como **proyecto de código** en los cuatro artefactos (cero formas desnudas). La serie `CU-XX` es homónima entre el nivel producto y los siete proyectos de código, y `Especificacion-Funcional.md` de cada proyecto la resuelve con el prefijo **`P·`** declarado —`P·CU-01` a `P·CU-27` para la previsión de la categoría 01, sin prefijo para la serie local—, que es exactamente el mecanismo que `Vocabulario-Rules.md` §9.2 pide |
| 14 | Ninguna polisemia con contextos disjuntos reportada como defecto ni corregida calificando todas las ocurrencias | **Cumple** | Verifiqué el criterio negativo en las dos direcciones: el corpus no sobrecalifica —«visor», «trabajo», «pieza» y «componente» aparecen desnudos donde el contexto es disjunto y ninguna fase los corrigió—, y **este informe no reporta ninguna**. Ver §6 |

**Los quince ítems cumplen.** Dos quedan al borde de su umbral —202 líneas sobre un piso de 200, y 21 términos sobre una banda recomendada de 10 a 20— y los señalo porque son los que primero se rompen si alguien agrega un párrafo, no porque incumplan.

---

## 2. Hallazgos por severidad

**Cero P0 y cero P1.** Lo que sigue son dos P2 y cuatro P3.

### P2-1 · El defecto de fondo del producto sigue vivo en dos líneas de `GeometriaFactory-Api`, y ya había sido reportado

**Dónde está.** `SDD/Docs/Proyectos/GeometriaFactory-Api/02-Especificacion-Funcional/README.md` **línea 58** y `SDD/Docs/Proyectos/GeometriaFactory-Api/03-UX-UI-DX/README.md` **línea 52**.

**Qué dice.** La primera: «Leer §3 sin §2 hace creer que las quince rutas están decididas, y **quince de ellas no lo están**». La segunda: «el motivo está declarado: **quince de las quince rutas son propuesta derivada**, y leer la tabla sin §2 hace creer que están decididas».

**Qué debería decir.** **Catorce de las quince.** Una ruta —la de `A-01`, el canje de credenciales— la declara la fuente; las otras catorce son derivación de la categoría.

**Cómo lo verifiqué.** Abrí el instrumento, no el documento que lo cita. `Definicion-Superficie-HTTP.md` **línea 62** dice textualmente: «**las rutas y los verbos de los catorce puntos restantes**, **la partición de la superficie en quince puntos**, y **ocho de los diez códigos de respuesta**». Conté además las filas `A-XX` de §3: dieciséis identificadores emitidos, `A-04` retirado por RN-16 y sin reciclar, **quince vivos**. Y el mismo `03-UX-UI-DX/README.md` **línea 34** dice lo correcto —«**catorce de sus quince rutas** todavía no están decididas»—, de modo que el documento **se contradice consigo mismo a dieciocho líneas de distancia**.

**Circunstancia agravante, y por qué no lo dejo en P3.** No es un hallazgo nuevo: es el `N-01` (P2) de [`Coherencia-Corpus-r2.md`](Coherencia-Corpus-r2.md) §7, emitido el 2026-08-10, que identificó las mismas tres ubicaciones con el mismo diagnóstico. **No se corrigió, y llega al traspaso.** Es la única instancia viva del defecto que causó los cinco rechazos —un documento afirmando sobre otra fuente algo que la fuente no sostiene— y sobrevivió a un informe que la nombró con número de línea.

**Impacto real.** Bajo. Quien lea la línea 52 va a tratar como abierta una ruta que la fuente ya fijó, que es el error conservador. La tercera ubicación que `N-01` señalaba —`03-UX-UI-DX/README.md` línea 112— es una fila de control de cambios que narra la emisión 1.0 y **no la cuento**: las filas históricas transcriben lo que se emitió ese día.

### P2-2 · El plan documental de nivel producto declara el estado de sus propios ocho README de cuatro maneras distintas

**Dónde está.** `SDD/Docs/Producto/11-Documentacion/README.md`, líneas 5, 86, 94, 138 y 194, y el frontmatter de los siete README de `Proyectos/<Nombre>/11-Documentacion/`.

**Qué dice.** Las cuatro afirmaciones, sobre el mismo conjunto de ocho documentos:

| Dónde | Qué declara |
| --- | --- |
| Frontmatter de los ocho | `status: Planificado` |
| Cabecera de los ocho | `**Estado:** Propuesto` |
| §3 preámbulo, línea 86 | «Hoy **todos** están en `Planificado` y ninguno tiene fecha de última revisión, porque ninguno se redactó» |
| §3.1, línea 94 | La fila del propio `README.md` declara **`Vigente`** con fecha de revisión `2026-08-11` |
| §3.3, línea 138 | «Siete de esos ocho README … se emiten con este mismo plan y **quedan en estado `Propuesto`**» |

**Qué debería decir.** Una sola cosa. El frontmatter `status` y la cabecera `Estado` responden a dos enums distintos —el de `Rules-Documentacion.md` línea 440 y el de `Root-Rules.md` §6— y esa dualidad es legítima, pero entonces la prosa de §3 y §3.3 tiene que decir **cuál de los dos campo está describiendo**. Hoy la línea 86 y la línea 94 se contradicen dentro del mismo apartado, y la línea 138 contradice a las dos.

**Cómo lo verifiqué.** `grep -m1 '^status:'` sobre los ocho archivos: los ocho devuelven `Planificado`. `sed` sobre la cabecera de los ocho: los ocho devuelven `Propuesto`. Leí las líneas 86, 94, 138 y 194 en el original.

**Impacto real.** Bajo y acotado a la categoría 11, que hoy no tiene contenido. Lo reporto porque es exactamente la familia de defecto que este producto arrastra —una celda de tabla congelada que contradice a la prosa que la rodea— y porque la corrección cuesta tres líneas.

### P3-1 · Cuatro filas de control de cambios con una celda de más

**Dónde está.** `06-Backlog-Tecnico/README.md` de `GeometriaFactory-Api` (línea 63), `-Infrastructure` (62), `-Visor` (59) y `-Web` (65).

**Qué dice.** La tabla de control de cambios de esos cuatro documentos tiene encabezado de **tres** columnas —`| Versión | Fecha | Descripción |`— y la fila `1.1` agrega una cuarta celda, `| Orquestador SDD |`.

**Cómo lo verifiqué.** Programa sobre los 854 archivos: para cada tabla, conté las barras **no escapadas** de cada fila y las comparé con las del encabezado. **Cuatro filas desparejas en todo el corpus vivo**, y son estas cuatro. Las otras 85 que un contador ingenuo marca son celdas con `\|` escapado dentro de informes de `Audit/`, y están bien formadas.

**Impacto real.** De renderizado: la cuarta celda se pierde o desborda según el visor. Ninguna decisión ni recuento cambia.

### P3-2 · El bloque que `Root-Rules.md` §2.2 pide para `rest-api` no está en el README raíz ni declarado como omisión

**Dónde está.** `SDD/Docs/README.md`, en conjunto.

**Qué dice.** `Root-Rules.md` §2.2 declara que para el tipo `rest-api` el README «incluye quick-start con `curl`, autenticación y referencia al contrato». El README raíz no tiene ninguno de los tres, y **no declara la omisión**, a diferencia de lo que hace con los tres archivos satélite en §6.

**Por qué es P3 y no más.** La propia norma se tensiona: **§4.3 clasifica el quick-start como sección opcional** para `rest-api`, y **§4.2 no lo incluye** entre las diez secciones obligatorias, ni **§6** entre los quince criterios de aceptación. Con tres apartados de la regla contra uno, la omisión es defendible. Y hay un argumento de fondo que la sostiene mejor: **no hay código**, de modo que un bloque de comandos ejecutables sería una afirmación sobre algo que no ocurrió, que es el defecto que este producto tiene documentado como el más caro.

**Qué debería decir.** Una línea en §6, junto a los tres satélites, declarando que el bloque `rest-api` de §2.2 se difiere a la `Guia-Inicio-Rapido` de la categoría 11 —que ya lo tiene planificado, con el objetivo de «un solo comando, o la menor cantidad posible, con verificación al final»— porque hoy no hay superficie que invocar.

**Cómo lo verifiqué.** Abrí `Root-Rules.md` §2.2, §4.2, §4.3 y §6 en el repositorio normativo. Grep de `curl` sobre el README: cero.

### P3-3 · La tabla de estado del README raíz es por categoría, no por proyecto de código

**Dónde está.** `SDD/Docs/README.md` §7.

**Qué dice.** `Root-Rules.md` §4.2 ítem 7 pide «tabla de estado **por proyecto de código y por categoría**». La tabla de §7 es por categoría, con la columna «Ámbito» resolviendo el eje de proyecto de código como «Los siete» o «Producto».

**Por qué es P3.** El eje está, colapsado: la información —que las ocho categorías están emitidas en los siete— es recuperable y es verdadera. Desagregarla en una matriz de 7 × 10 en un documento con techo de 400 líneas sería peor lectura. El criterio de §6 no exige la desagregación.

### P3-4 · El glosario rápido tiene 21 términos sobre una banda recomendada de 10 a 20

**Dónde está.** `SDD/Docs/README.md` §9.

**Qué dice.** «Veintiún términos», y son veintiuno contados. El criterio de §6 fija piso de diez y lo cumple; el anti-patrón de §4.5 recomienda «limitar a 10 a 20 términos esenciales». Uno por encima.

**Impacto real.** Ninguno. Lo dejo registrado por completitud del barrido, no porque haya que tocarlo.

### Forma del corpus completo, verificada con programa

| Propiedad | Resultado | Cómo la verifiqué |
| --- | --- | --- |
| Enlaces relativos rotos en el corpus vivo | **0** | Programa sobre los `.md` del árbol: **7.701** enlaces relativos extraídos con expresión regular, resueltos con `os.path.normpath` desde el directorio de cada archivo. 1.100 no resuelven, y **1.097 están dentro de `_legacy/`**: son documentos superados que conservan los enlaces relativos de su ubicación original, lo cual es correcto. De los tres restantes, los tres están en `Audit/E-08-Calidad-Siete-Proyectos-r2.md` y **los tres son falsos positivos**: dos son la transcripción entrecomillada de la cabecera de trazabilidad de otro documento —el informe cita el texto, no enlaza— y el tercero es el marcador `(ruta)` dentro de la descripción de un método de verificación. **Cero enlaces rotos reales en el corpus vivo** |
| Filas de tabla con celdas discordantes | **4** | Ver P3-1 |
| Identificadores fantasma | **0** | Recuperé con expresión regular todas las series cerradas del producto sobre `Docs/` e `Intake/` excluyendo `_legacy/`, y las contrasté contra el conjunto que declara su instrumento: `RN-01`…`RN-16` (nada fuera de rango), `INV-01`…`INV-09`, `NB-00001`…`NB-00009`, `PT-01`…`PT-05`, `RA-01`…`RA-03`, `F-01`…`F-26`, `X-1`…`X-9`, `E-1`…`E-8`, `ADR-XX` contra los archivos de cada proyecto (máximo referenciado = máximo existente en los siete), `CU-XX` contra los archivos de cada proyecto. **Ninguna referencia apunta a un identificador inexistente.** Las referencias `CU-20` a `CU-28` que un barrido ingenuo marca son la serie de nivel producto, escrita con prefijo `P·` declarado |
| Emojis en los artefactos de la fase | **0** | Barrido Unicode sobre los cuatro |

---

### Los recuentos del producto, contados sobre el instrumento

Ninguna cifra de esta tabla se tomó de `Vista-Producto.md` ni del README: cada una se contó sobre los archivos o sobre el documento que la produce.

| Magnitud | Declarado | **Contado por mí** | Instrumento y método |
| --- | --- | --- | --- |
| Casos de uso | 71 | **71** | `ls` de `CU-*.md` bajo `02-Especificacion-Funcional/Casos-De-Uso/` de cada proyecto de código, excluyendo `README`. **Domain 13, Api 12, Application 11, Web 10, Infrastructure 10, Contracts 8, Visor 7.** El reparto coincide fila por fila con el declarado |
| Reglas de negocio | 16 | **16** | `ls` de `Reglas-De-Negocio/` de `GeometriaFactory-Domain`: `RN-01` a `RN-16`, contiguas, sin hueco ni repetición |
| Invariantes del dominio | 9 | **9** | Barrido de `INV-\d\d` sobre `Docs/` e `Intake/`: el conjunto es exactamente `INV-01`…`INV-09`. Nada por encima de 09 en ninguna parte |
| Escenarios de datos | 8 | **8** | Barrido de `E-\d` sobre `PRODUCT-INTAKE` **1.26**: `E-1` a `E-8` |
| Casos de la batería del validador | 10 | **10** | Abrí el intake §21 línea 1478: «los **nueve** casos de RT §11 más el **décimo** que esta sección agregó el 2026-08-09 para la dimensión no legible, **diez** en total». Confirmado en §17.4 línea 784 |
| Códigos del contrato | 15 vivos / 18 emitidos | **15 / 18** | Conté las filas numeradas de `Contracts/Contratos-Abstractions.md` §5.1: **quince**, numeradas 1 a 15. Conté la tabla de §5.2: **tres** retirados. 15 + 3 = 18. Las tres señales de §5.3 están declaradas como no contables, y el barrido de `CONTRATO_[A-Z_]+` sobre el archivo devuelve 19 nombres distintos: los 18 más `CONTRATO_LISTADO_VACIO`, que es una de las tres señales. **Cierra** |
| Puntos de acceso | 15 | **15** | Barrido de `A-\d\d` sobre `Api/Definicion-Superficie-HTTP.md`: dieciséis identificadores, de los cuales `A-04` aparece **sólo** en la fila 1.1 del control de cambios que narra su retiro. Quince vivos en la tabla de §3, y el recuento que el documento propone para comprobarlo —cuatro sin acceso firmado más once bajo la guardia— cierra |
| Funciones de la fachada | 6 | **6** | `Visor/Contratos-Abstractions.md` §3, seis filas. Contrastado contra el original: `PRODUCT-INTAKE` §17.7 P.3 y §13 línea 492 enumeran las mismas seis, `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento` |
| Sondas de verificación `VER-XX` | 19 | **19** | Barrido por proyecto de código: `VER-01`…`VER-03` en seis de los siete y **sólo `VER-01` en `GeometriaFactory-Web`**. 3 × 6 + 1 = 19. Contrastado contra `/samples`: `find samples -mindepth 2 -maxdepth 2 -type d` devuelve **19** carpetas y `find samples -name '*.md'` devuelve **20** archivos —diecinueve más el índice de la raíz—, correspondencia uno a uno |
| Quality gates | 77 | **77** | Conté las filas `QG-XX` de la tabla §3 de `Estrategia-Calidad.md` de cada proyecto de código: **Api 15, Infrastructure 14, Application 11, Web 11, Contracts 9, Visor 9, Domain 8**. Suma 77, y el reparto coincide con el que `Pipeline-Producto.md` §6 declara |
| Decisiones de arquitectura | 45 | **45** | `ls` de `05-Arquitectura-Tecnica/Adrs/ADR-*.md` por proyecto: **Api 8, Web 7, Infrastructure 7, Domain 6, Visor 6, Application 6, Contracts 5**. Suma 45 |

**Los doce recuentos cierran. Ninguno está inflado, ninguno está congelado y el reparto por proyecto de código coincide en todos los casos donde el corpus lo desagrega.** Es el resultado más importante de esta auditoría: el defecto que causó cinco rechazos —recuentos heredados en vez de contados— **no aparece en ninguna de las doce magnitudes del producto**.

### Que la Fase H no haya decidido nada

Verifiqué los cuatro artefactos nuevos buscando lo contrario de lo que declaran: una afirmación que no tuviera detrás un documento anterior que la sostuviera. **No encontré ninguna.**

| Afirmación de peso de la Fase H | Dónde la verifiqué, abriendo el original |
| --- | --- |
| `Vista-Producto.md` §3.1: el manifiesto deriva **8** aristas en §2, dibuja **7** en §3 y valida **7** en §4 | Abrí `PRODUCT-MANIFEST` **1.3**. §2: sumé la columna `Dependencias` fila por fila, 3 + 2 + 0 + 1 + 2 + 0 + 0 = **8**. §3: conté las aristas del bloque `text`, cinco líneas que expresan **7** aristas. §4: la fila dice literalmente «Cumple: **las siete aristas resuelven**». **La cita es exacta en las tres lecturas.** La vista no elige ninguna |
| `Vista-Producto.md` §2: la excepción de nombre y path del `Visor` es apartamiento declarado | `PRODUCT-MANIFEST` §2, párrafo posterior a la tabla, con el fundamento —`GeometriaFactory.Visor` sería nombre de paquete inválido— y el reenvío al intake §13 y §16 |
| `Vista-Producto.md` §1.1: la categoría 04 está omitida por gating, `usa_llm` false en los siete | `PRODUCT-MANIFEST` §5, columna `usa_llm`: false en las siete filas, con fundamento explícito |
| `Pipeline-Producto.md` §4: el filtro de rutas del flujo del front incluye las tres entradas de compilación de `Web`, y la tercera entró por una corrección que `Contracts` elevó | Abrí `PRODUCT-INTAKE` §17.6.P.7: enumera `src/GeometriaFactory.Web/`, `visor/` y `src/GeometriaFactory.Contracts/`, «ampliado el 2026-08-11», y el párrafo «Por qué el contrato entra en el filtro» dice «Lo levantó la Fase F al resolver el punto abierto `PD-01`». Abrí además `Contracts/09-Devops/Entornos-Deploy.md` línea 58: `PD-01` es de esa categoría. **La atribución del pipeline es correcta** |
| `Pipeline-Producto.md` §5: no hay sufijos de anticipo, versionado del contrato por compilación compartida, sin versionado de rutas | Cada fila apunta a un ADR emitido en la Fase C —`Contracts ADR-03`, `Api ADR-08`— y los tres archivos existen. Ninguna decisión nueva |
| `11-Documentacion/README.md` §4: `tiene_extensibilidad` true sólo en `Visor`, y el punto de extensión son las seis funciones | `PRODUCT-MANIFEST` §5: la columna `tiene_extensibilidad` es true en una sola fila. El fundamento del propio manifiesto enumera las seis funciones y lleva una nota `[CORREGIDO 2026-08-11]` que retira una afirmación falsa anterior sobre §18 del intake |
| `11-Documentacion/README.md` §4: el cuerpo integrador se omite en `Web` porque no expone API externa | `PRODUCT-INTAKE` §14: el diagrama de composición pone a `Web` como hoja hacia el navegador y la arista `NAV -.->|"nunca"| API` sostiene que el front no publica contrato a nadie |
| `SDD/Docs/README.md` §2: la partición responde a una restricción de red del intake §14 | `PRODUCT-INTAKE` §14, primer párrafo: «El servidor propio no tiene IP estática y la red de la facultad bloquea el acceso a direcciones dinámicas … el front vive donde no lo bloquean y los datos viven donde persisten». **Cita fiel** |
| `SDD/Docs/README.md` §8: los siete puntos abiertos de nivel producto | Verificados uno por uno más abajo |
| `11-Documentacion/README.md`: recuento de **72** artefactos planificados | Sumé la tabla §3.3: 7 + 9 + 10 + 11 + 9 + 6 + 9 + 11 = **72**, y los siete de proyecto suman **65**, que es lo que el control de cambios declara. Conté las filas de §3.1: **siete**. Conté las entradas de la matriz de ruteo §2: **quince**, que es lo que el control de cambios declara |

**Los cuatro artefactos referencian y no deciden.** El único lugar donde la Fase H estuvo cerca de decidir algo es la omisión del `CHANGELOG.md`, y ahí lo que hizo fue **sobre-declarar** un apartamiento que la regla no exige —ver §3.3—.

### Los puntos abiertos vivos: son verdaderos y tienen titular

Los siete de `SDD/Docs/README.md` §8, verificados contra la fuente que cada uno dice que no los resuelve.

| Punto abierto | ¿Verdadero? | Cómo lo verifiqué | Titular |
| --- | --- | --- | --- |
| Siete u ocho aristas de compilación | **Verdadero** | Las tres secciones del manifiesto se contradicen y ninguna versión posterior las reconcilió: conté 8 en §2, 7 en §3 y la §4 dice «las siete». La 1.3 es la versión vigente | Product Owner. Declarado |
| Falta el informe de Fase B de `GeometriaFactory-Api` | **Verdadero** | `ls SDD/Docs/Audit/B-02-03-*`: hay informes de Application, Contracts, Domain, Infrastructure, Visor y Web. **Ninguno de Api** | Orquestador SDD. Declarado |
| El nombre del cuarto puerto | **Verdadero** | `PRODUCT-MANIFEST` §2 y el intake §13 y §17.2 enumeran **tres** puertos —`IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema`— en los tres lugares donde los nombran. El cuarto, el de repositorio de cuentas, **no tiene nombre en ninguna fuente**. `Application/Arquitectura-Proyecto-Codigo.md` línea 175 confirma que existe y que la categoría no lo inventa | Product Owner y equipo, etapa `a`. Declarado |
| El umbral de fluidez del visor | **Verdadero** | Grep de «fluidez» sobre el intake **1.26**: ninguna ocurrencia con valor numérico. `Visor/Arquitectura-Proyecto-Codigo.md` `PA-03` lo declara y propone verificación cualitativa junto a `PT-02` mientras tanto | Product Owner, o la categoría 08. Declarado |
| El alcance de la colección de peticiones reproducible | **Verdadero, no verificado en detalle** | Confirmé que el punto está declarado en `Api/05-Arquitectura-Tecnica/` §11 con titular. No abrí los dos lugares de la fuente que le atribuyen alcances distintos. Ver §6 |
| Los umbrales rotulados como asunción | **Verdadero** | Abrí el intake §22: la fila `A-5` declara los NFR numéricos —500 ms, 200 ms, p99 500 ms, 20 peticiones por minuto, 30 s de arranque en frío, 10 s de la batería— con la columna de justificación diciendo «RT §12 define puertas técnicas medidas pero **no umbrales de latencia ni de throughput**». Es asunción del intake, no decisión de una fase | Product Owner. Declarado |
| Las marcas para verificar heredadas | **Verdadero** | El intake §22 las lleva rotuladas y el README las remite ahí sin resolverlas | Se resuelven midiendo, etapas `a` e `i`. Declarado |

**Los siete son verdaderos y los siete tienen titular.** No encontré ningún punto abierto falso, que es el hallazgo que la Fase G tuvo que absorber y que esta consolidación no repite. Verifiqué además los seis puntos heredados de `11-Documentacion/README.md` §7: son un subconjunto de estos siete más el de nombres de tipos y espacios de nombres, y ninguno se resuelve en ese documento.

---

## 3. Los tres puntos que la Fase H declaró, dictaminados

### 3.1 El informe de auditoría de Fase B de `GeometriaFactory-Api`

**Los hechos.** `ls SDD/Docs/Audit/B-02-03-*` devuelve informes de seis proyectos de código, en trece archivos entre rondas. **`GeometriaFactory-Api` no tiene ninguno.** Sus categorías 02 y 03 existen y están completas: `02` con `Especificacion-Funcional.md`, `Definicion-Superficie-HTTP.md`, `Glosario-Funcional.md`, `README.md` y doce casos de uso; `03` con `DX-Developer-Experience.md`, `DX-Error-Messages.md`, `Glosario-UX.md`, `Guia-Onboarding-Developer.md` y `README.md`.

**Qué las cubrió, verificado.** [`Coherencia-Corpus-r2.md`](Coherencia-Corpus-r2.md) se corrió **sobre la rama `sdd/api-fase-b`** —la rama donde se produjo exactamente esa Fase B— con alcance declarado «Todo `SDD/Docs/` —nivel producto y los **siete** proyectos de código». Y no fue una pasada nominal: abrió los documentos y encontró defectos en ellos. Su `C-05` cerró una declaración de «diecisiete» códigos en `Api/Definicion-Superficie-HTTP.md` §5, y su `N-01` levantó el defecto de recuento de rutas de los dos README de la `Api` que este informe reporta como P2-1. Además, las categorías 02 y 03 de la `Api` fueron insumo verificado de las fases C, E, F y G, cada una con su informe aprobado: `C-05-…-r2` verificó fila por fila la superficie —«§4 fila 5: quince puntos de acceso, diez códigos de respuesta … **Verifica.** 15 filas `A-XX`, 10 filas de código de respuesta, 15 filas `CONTRATO_*`»—.

**Qué no las cubrió.** Ninguno de esos informes contrastó las dos categorías contra los criterios de aceptación de `Rules-Especificacion-Funcional.md` §6 y `Rules-UX-UI-DX.md` §6, que es lo propio de un informe de Fase B. La cobertura es **de contenido y de coherencia, no de conformidad normativa de categoría**.

**Dictamen: el hueco NO bloquea el traspaso, y está adecuadamente compensado y declarado.** Tres razones. **Primera**, lo que un informe de Fase B protege es que la especificación funcional sea correcta y completa, y esa propiedad está verificada por otra vía y con más pasadas que la de cualquier otro proyecto de código: la superficie de la `Api` es el artefacto más auditado del corpus, porque es el que todas las fases posteriores consumen. **Segunda**, el riesgo residual es de forma normativa, y la forma es lo único que este corpus **no** tiene roto: cero enlaces rotos, cero identificadores fantasma, cabeceras completas. **Tercera**, y es la que decide, **está declarado en dos lugares con titular** —`Vista-Producto.md` §1.1 y `README.md` §8, con el Orquestador SDD como responsable— y con la frase que importa: «cerrarlo es emitir el informe faltante o declarar por escrito que la revisión de coherencia lo sustituye, y ninguna de las dos cosas es una decisión de arquitectura». Un hueco declarado con dueño y con las dos salidas enunciadas no es un hueco: es un ítem de trabajo.

**Recomendación, no bloqueante.** Que el Product Owner elija por escrito una de las dos salidas antes de cerrar la etapa `a`. La segunda —declarar que `Coherencia-Corpus-r2` sustituye al informe— cuesta un párrafo y es defendible con la evidencia de arriba.

### 3.2 La discrepancia del grafo de dependencias, a tres bandas

**Los hechos, contados por mí sobre el original.** `PRODUCT-MANIFEST` **1.3** §2 columna `Dependencias` deriva **ocho** aristas. §3 dibuja **siete**: no dibuja `Application → Api`. §4 valida «las siete aristas resuelven». La octava, `Application → Api`, es una referencia **directa** que la `Api` además recibe **transitivamente** por `Infrastructure`.

**Dictamen: entregarla abierta es aceptable, y cerrarla por conveniencia habría sido el error.** El fundamento no es que el grafo sea acíclico bajo las dos lecturas —que lo es, y el orden topológico de cuatro niveles es idéntico—; es que **las tres lecturas son las tres verdaderas a la vez**. Un consumidor puede declarar una referencia directa a un ensamblado que ya le llega transitivamente: la tabla de §2 no está equivocada, el diagrama de §3 tampoco, y la validación de §4 se hizo sobre el diagrama. No hay un error que corregir; hay una **elección de forma del archivo de proyecto** que nadie tomó todavía. Tomarla habría sido decidir, y esta fase no decide.

Y lo que vuelve inofensiva la espera está verificado: `Pipeline-Producto.md` §4 muestra que **las ocho aristas se resuelven por build conjunto en un único repositorio con un único agrupador**, sin feed de paquetes. Con un feed de por medio, siete u ocho serían dos configuraciones de publicación distintas y esto sería P1. Sin feed, la diferencia se reduce a si el archivo de proyecto de `GeometriaFactory-Api` escribe una línea de referencia o no la escribe, y ninguna de las dos formas rompe nada.

**La consecuencia operativa está bien puesta y hay que respetarla.** `Vista-Producto.md` §3.1 la enuncia así: «ningún documento de este producto debe afirmar un número sin decir cuál de las tres secciones está leyendo». Verifiqué que los tres artefactos de la fase la cumplen: el README dice «siete u ocho», la vista lo desagrega en tres filas, y el pipeline rotula la fila de `Application → Api` como «Compilación, **en disputa**».

### 3.3 La no emisión de `CHANGELOG.md`

**Los hechos.** `SDD/Docs/README.md` §6 declara que no se emiten `CHANGELOG.md`, `CONTRIBUTING.md` ni `LICENSE.md`, con fundamento, y agrega: «La omisión de `CHANGELOG.md` queda registrada como **apartamiento**, porque la regla lo declara obligatorio para el tipo `rest-api` del proyecto de código principal».

**Dictamen: el apartamiento está bien fundado, y de hecho no es un apartamiento.** Abrí `Root-Rules.md` §2. Es cierto que la tabla maestra de **§2.1** marca `CHANGELOG.md` como obligatorio para `library`, `rest-api` y `cli-tool`. Pero **§2.2 cierra el apartado con esta frase**, que el README no cita:

> «Los archivos `CHANGELOG.md`, `CONTRIBUTING.md` y `LICENSE.md` se incluyen en `SDD/Docs/` **solo cuando el proyecto de código requiere comunicación con integradores externos al equipo**.»

**§2.2 condiciona a §2.1, no la contradice**: la tabla dice para qué tipos aplicaría, y el párrafo dice cuándo aplica. Y la condición no se cumple, verificado en la fuente: `PRODUCT-INTAKE` §2 declara `equipo_n` igual a 1; `PRODUCT-MANIFEST` §2 declara `redistribuible` false en los siete; no hay feed de paquetes en ninguna parte del corpus; y la audiencia son dos personas del aula.

De modo que el razonamiento del README —«`Root-Rules.md` §2.1 los pide cuando el proyecto de código necesita comunicarse con integradores externos»— **es el correcto**, sólo que atribuido al apartado equivocado: eso lo dice §2.2, no §2.1. La consecuencia es que la fase **se declaró en apartamiento de una regla que no está apartando**. Es un exceso de prudencia, no un defecto, y prefiero un producto que sobre-declare a uno que omita en silencio. Lo dejo anotado para que la corrección, si se hace, **cite §2.2** y baje el rótulo de «apartamiento» a «omisión con fundamento normativo», que es lo que es.

El fundamento de fondo —que el repositorio ya lleva su bitácora en la raíz del código, declarada en el intake §16, y que un segundo archivo sería una segunda fuente de verdad— es además exactamente el anti-patrón que `Root-Rules.md` §4.5 marca para el roadmap. Es coherente con la norma, no contra ella.

---

## 4. Dictamen final

# APTO PARA HANDOFF

**Fundamento.**

**Cero P0 y cero P1.** No hay ninguna afirmación falsa sobre una fuente en los cuatro artefactos de la Fase H, ningún punto abierto falso, ningún identificador fantasma, ningún enlace roto en el corpus vivo y ninguna decisión tomada por una consolidación que no tenía autoridad para tomarla.

**Los doce recuentos del producto cierran, contados por mí sobre el instrumento.** Setenta y un casos de uso con el reparto exacto por proyecto de código, dieciséis reglas, nueve invariantes, ocho escenarios, diez casos de batería, quince códigos vivos sobre dieciocho emitidos, quince puntos de acceso, seis funciones de fachada, diecinueve sondas con correspondencia uno a uno contra `/samples`, setenta y siete quality gates con el reparto por proyecto, cuarenta y cinco decisiones de arquitectura. **Es el criterio que decide este dictamen**: el defecto que causó los cinco rechazos —recuentos heredados y citas a través de terceros— sobrevive hoy en **dos líneas de dos README de índice**, y en ninguna magnitud del producto, en ningún contrato, en ninguna regla y en ningún gate.

**La consolidación consolidó.** Verifiqué diez afirmaciones de peso de los cuatro artefactos nuevos abriendo el documento original de cada una —el manifiesto §2, §3, §4 y §5; el intake §14, §17.6.P.7, §17.7.P.3, §21 y §22; los ADR citados— y las diez se sostienen. La cita más difícil del corpus, la de las tres lecturas del grafo en `Vista-Producto.md` §3.1, es **literal en las tres**.

**El producto se entrega declarando lo que no sabe.** Siete puntos abiertos de nivel producto, los siete verdaderos y con titular; un hueco de auditoría declarado con dueño y con las dos salidas enunciadas; tres archivos satélite no emitidos con fundamento; diecinueve sondas sin evidencia y dicho con todas las letras; y una discrepancia del manifiesto que la fase se negó a resolver por su cuenta pudiendo hacerlo sin que nadie lo notara. **Esa negativa es el mejor indicador de la calidad del corpus y pesa más que los seis hallazgos menores.**

**Lo que hay que hacer, y no bloquea.** Corregir las dos líneas de P2-1 —es un `sed` de dos palabras y cierra un hallazgo que ya venía reportado—; unificar el estado de los ocho README de la categoría 11 (P2-2); y las cuatro filas de tabla de P3-1. Los tres P3 restantes son de forma y de criterio, y son opinables.

---

## 5. Al Product Owner: qué riesgo real corre quien empiece a construir

Quien tome esta especificación y empiece a escribir código no corre riesgo de construir algo equivocado: corre riesgo de **detenerse**. Es una distinción que importa, porque las dos cosas cuestan muy distinto.

Lo que va a encontrar es un producto donde las cosas que hacen fallar un proyecto están decididas y son verificables una por una: los setenta y un casos de uso con sus criterios de aceptación, las dieciséis reglas del dominio con su enunciado transcrito, los quince puntos de acceso con su verbo y su papel exigido, los quince códigos del contrato con la razón por la que tres se retiraron y no se reciclan, los setenta y siete gates que van a bloquear su pull request, y los ocho escenarios de datos reales que el intake transcribe para que nadie invente un dato de prueba de geometría. No va a tener que adivinar qué hace el sistema, ni preguntar qué pasa cuando un alumno rechaza un trabajo ya rechazado, ni descubrir a mitad de camino que dos capas entendían distinto la misma palabra. Eso está resuelto y lo verifiqué contando, no leyendo.

Donde sí se va a detener es en un puñado de puntos chicos y **todos están señalados con un cartel**: no sabe cómo se va a llamar el cuarto puerto, no sabe si el archivo de proyecto de la `Api` declara una referencia o la recibe transitivamente, no sabe qué número de fluidez tiene que alcanzar el visor, y no sabe si el hosting gratuito soporta la versión de plataforma del front. Ninguno de los cuatro le impide arrancar; los cuatro le exigen **una decisión suya en el punto de control de la etapa `a`**, y ahí está el riesgo real: si ese punto de control se pospone, el equipo va a resolverlos por su cuenta y en silencio, que es exactamente cómo un producto bien especificado empieza a divergir de su especificación. El corpus ya hizo su parte poniéndolos por escrito con su nombre al lado; lo que falta es que usted los mire.

Hay un segundo riesgo, más callado, y es de otra naturaleza. Diecinueve sondas de verificación están diseñadas y ninguna corrió, porque no hay código. Todo lo que este corpus afirma sobre el comportamiento del sistema es una afirmación **sobre el papel**, honestamente rotulada como tal en cada documento. La primera vez que se ejecute la batería de integración algo va a fallar, y no será una sorpresa: será la primera vez que la especificación toque la realidad. Lo que este corpus le compra no es que eso no pase, sino que cuando pase usted sepa exactamente qué documento tiene que corregir. Y la deuda más cara del producto no es técnica: es que quien retome el laboratorio dentro de dos cursadas va a encontrar sesenta y cinco documentos de la categoría 11 **planificados y ninguno escrito**. Es correcto que sea así hoy, porque no hay código que documentar. Deja de serlo si la construcción avanza y el Momento 2 no se corre en cada corte, como el plan prescribe.

---

## 6. Lo que no reporto, y lo que no verifiqué

**Lo que no reporto, por criterio negativo.** Evalué y **descarté** las siguientes, y las dejo escritas para que ninguna ronda posterior las levante como hallazgo:

- **La serie `CU-XX` homónima entre el nivel producto y los siete proyectos de código.** Es una polisemia con contextos disjuntos, y además está **explícitamente resuelta** con el prefijo `P·` declarado en `Especificacion-Funcional.md` de cada proyecto de código. Reportarla sería un defecto de este informe, y corregirla calificando todas las ocurrencias sería peor.
- **`.NET 10` aparece en la lista D7** de términos prohibidos que `Rules-Necesidades-Negocio.md` §6 enumera. Es la lista del dominio fuente del bootstrap, no de este producto: `.NET 10` es el stack que la fuente de **este** producto declara, y `Root-Rules.md` §4.5 exige declarar «`tecnología @ versión`» en la cabecera y en §2. Aplicar D7 literalmente acá sería prohibirle al README decir de qué está hecho el producto.
- **Los 1.097 enlaces relativos que no resuelven dentro de `_legacy/`.** Son documentos superados que conservan los enlaces de su ubicación original. Reescribirlos alteraría el registro histórico.
- **Los 85 «desajustes» de tabla en `Audit/`**, que son celdas con `\|` escapado y están bien formadas.
- **Que el README raíz declare la omisión del `CHANGELOG.md` como apartamiento.** Es sobre-declaración conservadora, tratada en §3.3 como dictamen y no como hallazgo.
- **Los siete puntos abiertos de `README.md` §8 y los seis de `11-Documentacion/README.md` §7.** Un punto abierto verdadero y declarado con titular no es un hallazgo. Verifiqué que los siete son verdaderos.

**Lo que declaro no verificado.** No lo supongo ni lo doy por bueno:

1. **El punto abierto del alcance de la colección de peticiones reproducible.** Confirmé que está declarado con titular en `Api/05-Arquitectura-Tecnica/` §11; **no abrí** los dos lugares de la fuente a los que se atribuyen alcances distintos, de modo que no verifiqué que la contradicción exista tal como se la describe.
2. **La conformidad normativa de las categorías 02 y 03 de `GeometriaFactory-Api`** contra los §6 de `Rules-Especificacion-Funcional.md` y `Rules-UX-UI-DX.md`. Es la materia del informe de Fase B que falta, y emitirlo desde acá sería sustituir una fase por una auditoría final. Mi dictamen de §3.1 es sobre la **suficiencia de la compensación**, no sobre la conformidad.
3. **El contenido interno de los 208 casos de prueba y los 219 criterios de la Fase E**, y el de los 65 artefactos planificados de la categoría 11, que no existen.
4. **La corrección semántica caso por caso de los 71 casos de uso.** Conté los setenta y uno y verifiqué su reparto; no releí sus flujos alternativos. Las fases B, C y E los auditaron con ese alcance y sus informes están aprobados.
5. **`SDD/Maquetas/`**, cuya validación visual cerró en la Fase B2 con informe propio y cuyo rechazo consta como levantado. No la re-audité.

---

## 7. Control de cambios

| Versión | Fecha | Descripción del cambio |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Auditoría final consolidada del producto, ronda 1, sobre `sdd/fase-h-consolidacion` commit `58255c8`. **APTO PARA HANDOFF.** Verifica los **quince** criterios de `Root-Rules.md` §6 uno por uno, los quince cumplen. Recuenta las **doce** magnitudes del producto sobre el instrumento y las doce cierran, con el reparto por proyecto de código coincidente en todos los casos donde el corpus lo desagrega. Verifica **diez** afirmaciones de peso de los cuatro artefactos de la Fase H abriendo el documento original de cada una: las diez se sostienen y **ninguna decide nada**. Verifica los **siete** puntos abiertos de nivel producto: los siete verdaderos y con titular. Forma sobre 854 archivos: **cero** enlaces relativos rotos en el corpus vivo sobre 7.701 extraídos, **cero** identificadores fantasma sobre nueve series cerradas, **cuatro** filas de tabla desparejas. Levanta **dos P2** —el residuo de recuento de rutas de la `Api`, ya reportado como `N-01` por `Coherencia-Corpus-r2.md` y no corregido, y el cuádruple estado de los ocho README de la categoría 11— y **cuatro P3**. **Cero P0 y cero P1.** Dictamina los tres puntos declarados por la fase: el hueco de auditoría de Fase B de la `Api` **no bloquea** y está compensado y declarado; la discrepancia del grafo **es aceptable abierta** porque las tres lecturas son verdaderas a la vez y el build conjunto la vuelve inocua; la no emisión del `CHANGELOG.md` **está bien fundada** y, contra `Root-Rules.md` §2.2, ni siquiera es un apartamiento. Declara **cinco** verificaciones no realizadas y **seis** no-hallazgos descartados por criterio negativo. **Auditor independiente, sin participación en la generación de ninguna fase.** |
