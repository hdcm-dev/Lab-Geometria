# Auditoría de la Fase G · categoría 10 Examples de los siete proyectos de código · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-g-examples` |
| Objeto de la ronda | Dictaminar la emisión 1.0 de la categoría **10-Examples** de los siete proyectos de código, entregada en dos olas: `f8d75f3` (Domain, Contracts, Visor, más el intake a 1.23) y `d0b29c5` (Application, Infrastructure, Api, Web, más el intake a 1.24); las **diecinueve** sondas `VER-XX` dadas de alta en las siete matrices de sensado; y el cierre de los huecos que las fases anteriores dejaron anotados esperando esta categoría |
| Alcance auditado | Los **26** documentos nuevos de las siete carpetas `10-Examples/` —siete `README.md` y diecinueve markdown explicativos—; los **19** contratos de verificación campo por campo; las siete `Matriz-Sensado-Deriva.md`, con las 61 filas previas de Web y las 12 previas del Visor contrastadas por `git diff`; los recuentos, recontados; la cobertura de casos de uso, **reconstruida con herramienta** desde los bloques `verifica` y no desde el párrafo que la declara; y forma —cabeceras, celdas, enlaces— verificada con programa sobre los 26 archivos más las siete matrices |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.24**, en especial §16.1, §17.1.P.2, §17.7.P.3, §18, §20 y §21, y sus archivados **1.22** y **1.23**; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §5; las categorías **02**, **05**, **06** y **08** de cada proyecto de código; `Rules-Examples.md` **4.1** y `Deriva-Rules.md` de `IA.SDD`, leídas como norma y **no modificadas** |
| Criterio de la ronda | **Ninguna cita se dio por buena sin abrir la fuente.** Cada afirmación sobre §16.1, §18, §20, §21, `Rules-Examples.md` y `Deriva-Rules.md` se contrastó contra el texto de la fuente, no contra lo que otro documento del corpus dice de ella. El estado de los 19 contratos se leyó del YAML, no de la prosa. La cobertura de casos de uso se recompuso con herramienta desde los 19 bloques `verifica` y se comparó contra los `CU-XX` que existen en cada `02` |
| Fuera de alcance | `_legacy/`; las fuentes originales bajo `PROMPTs/`; la categoría 11, no emitida y correctamente declarada así en los siete README |
| Auditor | Auditor independiente, sin participación en la emisión |
| Fecha | 2026-08-11 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Si es la pasada de diseño y no otra cosa](#2-si-es-la-pasada-de-diseño-y-no-otra-cosa)
- [3. Los huecos que esta fase debía cerrar](#3-los-huecos-que-esta-fase-debía-cerrar)
- [4. La integración con la matriz de sensado de Web](#4-la-integración-con-la-matriz-de-sensado-de-web)
- [5. Cobertura y trazabilidad, reconstruidas con herramienta](#5-cobertura-y-trazabilidad-reconstruidas-con-herramienta)
- [6. Afirmaciones sobre otras fuentes](#6-afirmaciones-sobre-otras-fuentes)
- [7. Recuentos](#7-recuentos)
- [8. Forma](#8-forma)
- [9. Hallazgos](#9-hallazgos)
- [10. Lo que no pude verificar](#10-lo-que-no-pude-verificar)
- [11. Dictamen](#11-dictamen)
- [12. ¿Alcanzan estos ejemplos para construir el producto desde la especificación?](#12-alcanzan-estos-ejemplos-para-construir-el-producto-desde-la-especificación)
- [13. Control de cambios](#13-control-de-cambios)

---

## 1. Resumen ejecutivo

**Los diecinueve contratos nacen sin evidencia, y eso está bien hecho.** Extraje los diecinueve bloques `verificacion:` con herramienta: los diecinueve traen `evidencia:` con un único campo, `estado: "No verificado — sin código"`. **No hay una sola `fecha:` ni una sola `salida:` en toda la categoría**, que es la forma exacta que `Rules-Examples.md` §0.2 fija para la pasada de diseño. Ninguna sonda afirma haber corrido. Los diecinueve traen además los cuatro campos que §4.6 exige antes de la evidencia, y los criterios de aceptación son aserciones —exit code y líneas exactas de salida—, no prosa: revisé los diecinueve y ninguno dice «verificar que funcione correctamente». En esa materia, que es la primera que había que mirar, la emisión es limpia.

**Y sin embargo la pasada de diseño quedó incompleta en su otra mitad, y los siete README afirman lo contrario.** §0.2 asigna a esta pasada dos productos: los markdown con su contrato, y **las carpetas de `/samples` esqueletadas, con su README local y su comando previsto**. Las carpetas no existen. `git ls-files` no devuelve un solo archivo bajo `samples/`, y `find` no encuentra el directorio. Los siete README dicen, con la misma redacción, que «esta pasada deja **esqueletada**» su carpeta «con su README local y su comando previsto». Son **siete afirmaciones falsas sobre un entregable no producido**, y arrastran a las diecinueve filas de las matrices, cuyo método de verificación es un comando que apunta a una ruta que hoy no resuelve —lo que `Deriva-Rules.md` §6 prohíbe explícitamente—. Es el hallazgo **P0** de esta ronda.

**Los huecos que las fases anteriores dejaron anotados están cerrados, todos, y con desenlace.** Los busqué con `grep` en todo el corpus y los seguí uno por uno por `git diff`: las filas no se retiraron, se conservaron tachadas o reescritas con su fecha de cierre y el motivo original legible al lado. El único que sigue abierto —el primer hueco de `Contracts` §7, que ningún sample cierra porque las sondas no golpean el servicio real y `QG-05` sigue dependiendo de la batería de integración de Api— está declarado abierto y con fundamento verdadero. Ninguno se declaró cerrado sin estarlo.

**La integración con la matriz de Web es correcta y la verifiqué por diferencia, no por lectura.** `git diff` entre `5b2f63e` y `d0b29c5` sobre `Matriz-Sensado-Deriva.md` de Web no elimina ni modifica **ninguna** fila `SD-01` a `SD-61`: sólo agrega `SD-62`, la dimensión de umbral correspondiente y la prosa que la explica. Lo mismo en el Visor, que pasa de doce a quince sin tocar las doce. `SD-62` no duplica cobertura de línea de base y donde mira lo mismo que `SD-36` declara el mismo umbral, «mayor sin gradación», sin contradicción.

**Los recuentos son todos correctos.** Conté yo: dieciséis reglas, nueve invariantes, ocho escenarios, diez casos de batería, quince códigos vivos sobre dieciocho emitidos con tres retirados, quince puntos de acceso —con `A-04` retirado y sin reciclar—, seis funciones de fachada, y los siete recuentos de casos de uso, que dan 13, 8, 7, 11, 10, 10 y 12. Ninguno está inflado.

**Lo que falla es lo de siempre en este producto: afirmaciones sobre otras fuentes hechas sin abrir la fuente.** Cuatro README declaran «lo que queda abierto: la consolidación de `PRODUCT-INTAKE` §16.1», y §16.1 se consolidó **en el mismo commit que los entrega**. Cinco documentos citan como vivo un «residuo de cinco funciones en §18» que §18 no tiene: §18 dice «las **seis**». Ese residuo lo declara el `PRODUCT-MANIFEST` §5, y los cinco documentos lo tomaron de ahí en vez de abrir §18 —la ola 1 lo reconoció en su mensaje de commit y no lo sacó de sus documentos—. Y §18 quedó sin alinear: sigue declarando tres muestras cuando §16.1 ahora asigna carpeta propia a seis proyectos, que es exactamente la contradicción entre dos celdas de tablas distintas que la 1.24 dice haber descubierto y cerrado. La cerró para uno y la abrió para cuatro.

---

## 2. Si es la pasada de diseño y no otra cosa

`Rules-Examples.md` §0.2 fija tres cosas para esta pasada: los markdown explicativos completos, el contrato con su `criterio_aceptacion` declarado y `evidencia` en `No verificado — sin código`, y las carpetas de `/samples` esqueletadas con su README local y su comando previsto.

**Lo primero y lo segundo están.** Recorrí los diecinueve archivos con programa. Los diecinueve tienen las diez secciones de §4.2 —conté los encabezados `## N.` de cada uno: diez en los diecinueve, sin excepción—, y los diecinueve tienen su §9 con el bloque YAML de §4.6. Los cinco campos:

| Campo de §4.6 | Presente en | Observación |
| --- | --- | --- |
| `verifica` | 19/19 | Todos los `CU-XX` y `US-XX` citados existen en el `02` y el `06` de su proyecto de código; verifiqué los máximos: Api cita hasta `US-30` sobre 30 historias, Application hasta `US-32` sobre 32, Contracts hasta `US-22` sobre 22, Domain hasta `US-27` sobre 27, Infrastructure hasta `US-25` sobre 25, Web hasta `US-23` sobre 30. Ninguno inventa identificador |
| `comando` | 19/19 | Copy-paste desde la raíz. **Ninguno resuelve hoy**, por el P0 de §9 |
| `precondiciones` | 19/19 | — |
| `criterio_aceptacion` | 19/19 | Aserciones: `exit_code` más líneas exactas. Leí los diecinueve; ninguno es prosa. Doce declaran además aserciones **negativas**, con el fundamento escrito de qué defecto pasaría todas las positivas |
| `evidencia` | 19/19 | **Un solo campo, `estado: "No verificado — sin código"`, en los diecinueve.** Cero `fecha:`, cero `salida:` en toda la categoría |

**Lo tercero no está, y se afirma que sí.** Es el P0. Lo detallo en §9.

**La cantidad de samples por tipo es la correcta.** `Rules-Examples.md` §2.2 fija tres para `library` y `rest-api`: los cinco `library` —Domain, Contracts, Application, Infrastructure, Visor— y el `rest-api` —Api— emiten tres cada uno. Para `web-monolith` fija dos, «datos seed + tema custom **(si hay punto de extensión visual)**», y Web emite uno. **La omisión del segundo está bien fundada y lo comprobé contra la fuente**: `tiene_extensibilidad` es `false` para Web en el `PRODUCT-MANIFEST` §5, y el único `true` del producto es el Visor, cuyo punto de extensión es el contrato de la fachada. Un sample de tema custom en Web afirmaría una capacidad que el producto no tiene.

**Los slugs son de los admitidos por §3.1.** `basico`, `intermedio`, `avanzado` y `datos-seed`, los cuatro en la lista cerrada. Ninguno atado al dominio, que es la prohibición expresa de esa sección. El desvío de estructura de `/samples` —un segmento por proyecto de código, `/samples/domain/`, `/samples/api/`, etc.— está declarado en los siete README con el mismo fundamento: §2.3 supone un proyecto de código por repositorio y este producto tiene siete en uno solo. Es carpeta extra, no renombre de las base, que es lo que §2.3 admite. **No es hallazgo.**

---

## 3. Los huecos que esta fase debía cerrar

Los busqué con `grep -rn "VER-XX"` sobre todo `SDD/Docs`, excluyendo `_legacy`, y después seguí cada uno por `git diff 5b2f63e d0b29c5`. Son siete, uno por proyecto de código:

| Proyecto de código | Dónde estaba anotado | Cómo quedó | Verificación |
| --- | --- | --- | --- |
| Domain | `08/README.md` §3 y `Matriz-Cobertura-Pruebas.md` §7 | **Cerrado.** Fila conservada, reescrita como «Emitido el 2026-08-11, en 1.0», con el motivo original legible y la constancia de que la condición «sin Fase B2» sigue en pie | `git diff` sobre los dos archivos |
| Contracts | `08/README.md` §3 y `Matriz-Cobertura-Pruebas.md` §7 | **Cerrado**, con la constancia expresa de que **el primer hueco de §7 sigue abierto**: las tres sondas no golpean el servicio real y `QG-05` sigue dependiendo de la batería de Api | Ídem; abrí `Matriz-Cobertura-Pruebas.md` y la fila del primer hueco sigue ahí, sin tachar |
| Visor | `08/README.md` §6 | **Cerrado.** «Esta sección declaraba que no había ninguna… la matriz pasa de doce a quince filas. Ninguna de las doce anteriores cambia» | Ídem, más el `git diff` de la matriz |
| Application | `08/README.md` §3 y `Matriz-Cobertura-Pruebas.md` §8 | **Cerrado**, mismo patrón | Ídem |
| Infrastructure | `08/README.md` §3 y `Matriz-Cobertura-Pruebas.md` §8 | **Cerrado**, con la constancia de que siguen siendo siete huecos, ahora dos cerrados | Ídem |
| Api | `08/README.md` §3, `Matriz-Cobertura-Pruebas.md` §8 y `07-Plan-Sprint/README.md` §4 | **Cerrado en los tres.** El de `07` declara además que **otras dos filas de esa misma tabla quedaron desactualizadas** —las de `08` y `09`, que dicen «todavía no emitida» estando emitidas— y que corregirlas es de la categoría 07 | Ídem; abrí las dos filas y la observación es cierta |
| Web | `Estrategia-Testing.md` §8 y `Matriz-Sensado-Deriva.md` §1 y §4 | **Cerrado.** La fila que decía «no se hace: `10-Examples` no está emitida» se conserva con su desenlace | Ídem |

**Ninguno se declaró cerrado sin estarlo.** En los siete casos abrí el archivo de destino y verifiqué que el artefacto que el cierre invoca existe con la versión que declara. El caso de `Contracts` es el más interesante y está bien resuelto: cierra el hueco que le tocaba y **se niega a declarar cerrado el que no cierra**, con el fundamento correcto —ningún tipo del ensamblado se ejercita de verdad hasta que exista la API, y las sondas no golpean el servicio real—. Eso es un punto abierto correctamente declarado, y por el criterio negativo de esta ronda no es hallazgo.

**La titularidad del alta está bien resuelta y no es una desviación.** `Deriva-Rules.md` §2.3 asigna a AG-08 la apertura de la matriz en la Fase E; acá la abre AG-10 al cerrar la fase de la categoría 10, porque la Fase E ya había cerrado y en ese momento las sondas no existían. Los cuatro proyectos sin maqueta lo declaran explícitamente y lo anclan en el **segundo momento de sensado** de §4, que es literalmente «al cerrar la fase que genera la categoría 10 · AG-10 · alta de una sonda `VER-XX` por cada contrato de verificación declarado en la pasada de diseño, todas en `Sin verificar`». Abrí §4 y la cita es exacta. **No es hallazgo.**

---

## 4. La integración con la matriz de sensado de Web

Lo verifiqué por diferencia, que es la única forma de comprobar que algo *no* cambió.

**Ninguna de las 61 filas anteriores cambió.** `git diff 5b2f63e d0b29c5 -- .../GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md` no contiene **una sola línea eliminada que empiece por `| \`SD-`** en el rango `SD-01` a `SD-61`. Las líneas eliminadas son cinco: la versión, el autor, la fila de la tabla de momentos que registraba la ausencia, la frase «ninguna fila `VER-XX`…» y las dos frases de recuento que pasan de «sesenta y una» a distinguir línea de base de total. Ni un umbral, ni un método, ni una afirmación de fila se tocó.

**La nueva no duplica cobertura de línea de base.** `SD-62` se ancla en `VER-01` y la matriz lo dice explícitamente: «`SD-62` no cubre ninguno de ellos: se ancla en un contrato de verificación y no en un elemento validado visualmente». La cobertura de los 211 identificadores sigue atribuida a las 61, y el recuento no se movió.

**Los umbrales no se contradicen.** `SD-62` declara «Mayor, sin gradación» en sus dos tramos de regla de negocio: un borrador visible en el listado de la comisión (`RN-11`) y un carácter cambiado en el texto original (`RN-08`). Abrí las filas viejas que miran lo mismo:

- `SD-36`, sobre `DM-12`: «**Mayor**, sin gradación: cualquier normalización, reordenamiento o carácter faltante viola RN-08». **Idéntico.** Y `SD-36` figura en la lista de filas sin gradación de §5.
- `SD-10`, la superficie del listado de la comisión: «**Mayor**: aparece un trabajo en estado `Borrador`, o el listado ofrece aprobar o rechazar sin abrir el trabajo». El umbral efectivo coincide —cualquier aparición es mayor, no hay tramo menor—, de modo que **no hay contradicción**. Con una imprecisión menor que anoto como P3: `SD-10` no figura en la lista de «filas sin gradación» de §5, y la prosa nueva le atribuye esa clasificación.

**Y la sonda aporta algo que las 61 no aportaban**, cosa que verifiqué en `Estrategia-Testing.md` §1 y §3: este proyecto de código no tiene proyecto de pruebas propio y su verificación es un guion que ejecuta una persona. `SD-62` es la primera fila de esa matriz que trae comando y aserción propios. La asimetría que invoca de `Deriva-Rules.md` §4 —«una `SUP-XX` exige que alguien mire y compare, mientras que una `VER-XX` se corre sola y devuelve un veredicto»— la contrasté contra la fuente y es literal.

**El Visor, lo mismo.** Doce a quince filas, `SD-13` a `SD-15`, y el `git diff` no elimina ninguna fila `SD-01` a `SD-12`.

**Correspondencia una a una entre contratos y filas, sin huérfanos.** La conté con herramienta sobre los siete proyectos: 3-3-3-3-3-3-1 contratos, 3-3-3-3-3-3-1 filas que los citan. Diecinueve y diecinueve. `Deriva-Rules.md` §6 exige exactamente eso y se cumple.

---

## 5. Cobertura y trazabilidad, reconstruidas con herramienta

No le creí a ningún párrafo de cobertura. Extraje los diecinueve bloques `verifica:` y armé la unión por proyecto de código, contra los `CU-XX` que existen en cada `02-Especificacion-Funcional/Casos-De-Uso/`:

| Proyecto de código | CU que existen | Unión de los `verifica` | Lo que declara el README | ¿Cierto? |
| --- | --- | --- | --- | --- |
| Domain | 13 | `CU-01` a `CU-13`, sin repetir | «trece de trece, sin repeticiones y sin huecos» | **Sí** |
| Contracts | 8 | `CU-01` a `CU-08` | ocho de ocho | **Sí** |
| Visor | 7 | `CU-01` a `CU-07` | siete de siete | **Sí** |
| Application | 11 | `CU-01` a `CU-11` | once de once | **Sí** |
| Infrastructure | 10 | `CU-01` a `CU-10` | diez de diez | **Sí** |
| Api | 12 | `CU-01` a `CU-12` | doce de doce | **Sí** |
| Web | 10 | `CU-05`, `CU-06`, `CU-08` | «3 de 10, y los 7 restantes tienen su verificación declarada en otro lado» | **Sí**, con la salvedad de cita del P2-1 |

**Las ausencias están justificadas y las justificaciones son verdaderas.** Las dos que importan:

- **Web, siete casos de uso sin sonda.** El fundamento —el guion de demostración de cada etapa los cubre— está efectivamente escrito en el intake §16.1 y en RF §9.3 citado desde §18. Es cierto que el intake ya había escrito esa justificación. Lo que falla es la forma de la cita a la regla que la habilita, no el fondo.
- **Visor, siete de ocho escenarios.** Falta `E-3`, y el fundamento es que `E-3` y `E-4` ejercitan la verificación del área declarada, que el Visor no hace porque la fachada lee una dimensión y no valida un trabajo. Abrí §20.E-3 y §20.E-4: es correcto, los dos son el mismo cubo emitido por los dos ejemplos de la cátedra y lo que ejercitan es `T3` y `T4`, materia del validador. La derivación a `TC-06` resuelve.
- **Domain, seis de ocho escenarios.** Excluye `E-2` y `E-7` con fundamento que verifiqué contra §20: `E-2` aporta las dos trampas de formato —clave `Tapas` y comas finales—, que son de la lectura del texto y no del dominio, y `E-7` ejercita los seis tipos dibujables, que es del que dibuja. Correcto.

**Los enlaces de trazabilidad resuelven.** Corrí un verificador sobre los 26 archivos de la categoría más las siete matrices: **0 enlaces relativos rotos** sobre el total.

---

## 6. Afirmaciones sobre otras fuentes

Es donde está el grueso de los hallazgos. Contrasté cada cita entrecomillada contra el documento al que se atribuye.

**Lo que resistió el contraste.** Las citas a `Rules-Examples.md` §0.1 sobre la arista B y su destinatario, a §2.2 y §2.3 sobre el piso de samples y su condición de punto de extensión visual, a §3.1 sobre los slugs admitidos, y a `Deriva-Rules.md` §2.4, §4 y §6 son fieles. La afirmación de que §18 asigna a `Infrastructure` la muestra `S-3` y de que la redacción anterior de §16.1 lo contradecía **es verdadera**: abrí §18 y la columna de `S-3` dice «GeometriaFactory-Infrastructure (validador)», y la fila de §16.1 anterior a 1.24 decía «sin samples propios». Esa contradicción existía y la 1.24 la cerró correctamente.

**Lo que no resistió.**

1. **El residuo de cinco funciones en §18 no existe.** Abrí §18: dice «el contrato de la fachada del visor (`inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento`, las **seis** que §17.7 P.3 declara desde 1.6)». La enumeración está completa y con el rótulo de seis. Quien afirma lo contrario es el `PRODUCT-MANIFEST` §5, que sigue diciendo «**La enumeración de §18 del intake sigue nombrando cinco**: es un residuo de la fuente anterior a 1.6». **Cinco documentos de esta fase repitieron esa afirmación tomándola del manifiesto en lugar de abrir §18.** Es exactamente el defecto que la ola 1 identificó en su propio mensaje de commit —«una de las contradicciones que el informe reporta estaba desactualizada: el residuo de cinco funciones en §18 se corrigió en la versión 1.11»— y que no sacó de sus documentos.

2. **Cuatro README declaran abierta la consolidación de §16.1 que su propio commit cerró.** Domain y Contracts fueron entregados en `f8d75f3`, el commit que sube el intake a **1.23** y reescribe la fila de §16.1 de Domain y Contracts. Sus README dicen «lo que queda abierto: la consolidación de `PRODUCT-INTAKE` §16.1, que hoy sigue diciendo que este proyecto de código no tiene samples propios». Application fue entregado en `d0b29c5`, el commit que sube a **1.24** y reescribe su fila, y su README dice «cuya fila para este proyecto de código sigue diciendo "sin samples propios"». Las tres afirmaciones son falsas contra el estado del repositorio en el momento en que se publican. Infrastructure repite la frase pero su segundo término —la alineación con §18— **sí sigue abierto**, de modo que su punto abierto es medio verdadero.

3. **§18 quedó desalineado, y es la misma falla que la 1.24 dice haber corregido.** §18 sigue declarando **tres** muestras, `S-1` Visor, `S-2` Api, `S-3` Infrastructure, y afirma que «**No hay sample de flujo de usuario final**, y es deliberado». Mientras tanto §16.1 asigna carpeta propia en `/samples` a **seis** proyectos de código y el corpus documenta **diecinueve** samples. La 1.24 descubrió que §16.1 y §18 se contradecían «dentro del mismo documento… porque vivía en dos celdas de tablas distintas», arregló la celda de Infrastructure y dejó la contradicción abierta para Domain, Contracts, Application y Web. Sólo Infrastructure la declara.

4. **§16.1 sigue diciendo que Web no produce sample propio, y Web produjo uno.** La fila dice «No produce sample propio: el guion de demostración de cada etapa… cumple ese papel». Web emitió `ejemplo-01-datos-seed.md` con carpeta `/samples/web/01-datos-seed/`. El README de Web argumenta, con cuidado, que la frase «sigue siendo cierta de la arista A». El argumento es bueno pero no salva la celda: la columna de §16.1 se titula «**Qué hay en `/samples`**» y describe contenido de carpeta, no aristas; y `Rules-Examples.md` §2.1 hace de §16.1 la sección que **gobierna** esa materialización. Web además cierra con «**nada que elevar sobre §16.1** por parte de este proyecto de código», con lo cual la contradicción queda sin ruta de cierre asignada, a diferencia de los otros cinco que sí elevaron.

5. **Dos citas con elisión no marcada.** La de `Rules-Examples.md` §6 en el README de Web corta el criterio en «o la ausencia está justificada» y omite «**en `Decisiones-Proyecto.md`**», que es justamente el término que fija dónde debe vivir la justificación —y ningún proyecto de código de este producto tiene ese archivo—. La de `Deriva-Rules.md` §2.3 en Domain va rotulada «lo prevé **literalmente**» y elide sin marca «la abre AG-08 en la Fase E», que es el fragmento que asigna titularidad y del que el propio documento se desvía en el párrafo siguiente. Application y Contracts repiten la elisión sin el rótulo «literalmente».

---

## 7. Recuentos

Conté yo, sobre la fuente, no sobre la declaración.

| Conjunto | Declarado | Contado | Cómo |
| --- | --- | --- | --- |
| Reglas de negocio | 16 | **16** | `RN-01` a `RN-16` en las tablas de §4.1 del intake, sin huecos |
| Invariantes | 9 | **9** | `INV-01` a `INV-09` en §17.1.P.2. La prosa «nueve invariantes desde el 2026-08-09» es correcta |
| Escenarios | 8 | **8** | `§20.E-1` a `§20.E-8`, encabezados contados |
| Casos de batería | 10 | **10** | Filas de la tabla de §21: los nueve de RT §11 más el décimo de dimensión no legible |
| Códigos de contrato | 15 vivos / 18 emitidos | **15 / 18** | 18 identificadores únicos en §3.2 de `DX-Error-Messages.md`, tres de ellos con fila de retiro: `DXT-09`, `DXT-13` y `DXT-18`. 18 − 3 = 15 |
| Puntos de acceso | 15 | **15** | Filas `A-XX` de `Definicion-Superficie-HTTP.md` §3: `A-01` a `A-16` **sin `A-04`**, retirado y no reciclado |
| Funciones de fachada | 6 | **6** | §17.7.P.3 y §18 del intake, las dos coincidentes |
| Casos de uso | 13 · 8 · 7 · 11 · 10 · 10 · 12 | **13 · 8 · 7 · 11 · 10 · 10 · 12** | Archivos `CU-*.md` de cada `02/Casos-De-Uso/`: Domain 13, Contracts 8, Visor 7, Application 11, Infrastructure 10, Web 10, Api 12 |
| Contratos `VER-XX` | 19 | **19** | Bloques `verificacion:` en los 19 markdown |
| Filas de la matriz de Web | 62 | **62** | 61 de línea de base más `SD-62` |

**Ninguno está inflado ni desactualizado.** Vale registrar además que el README de Api levanta por su cuenta un residuo de la categoría 02 —`CU-12` §9 escribe «13 de 16» donde su propia §8 y §10 dicen «13 de 15»—, lo declara, escribe 15 y no corrige desde afuera. Abrí `CU-12` y el residuo existe. Es la conducta correcta y no es hallazgo de esta fase.

---

## 8. Forma

Verificada con programa sobre los 26 archivos de la categoría y las siete matrices.

- **Cabeceras.** Los 26 traen `Versión`, `Estado`, `Fecha`, `Autor` y trazabilidad upstream y downstream. Los diecinueve markdown traen su `## 10. Control de cambios`. Los siete README también.
- **Celdas.** Ninguna fila de ninguna tabla tiene un número de celdas distinto del de su encabezado. Cero desvíos sobre el total.
- **Enlaces.** Cero enlaces relativos rotos.
- **Secciones.** Diez encabezados numerados en cada uno de los diecinueve markdown, que es lo que exige §4.2.
- **Versionado.** Los artefactos previos tocados suben minor con el fundamento escrito en su control de cambios: `08/README.md` y `Matriz-Cobertura-Pruebas.md` de los seis a 1.2, `Matriz-Sensado-Deriva.md` de Web a 1.3 y del Visor a 1.1, `Estrategia-Testing.md` de Web a 1.2, `07/README.md` de Api a 1.1.

**El `.json` de Domain: verifiqué su alcance real y es menor de lo reportado.** Aparece en **un solo archivo y un solo lugar** —el árbol de la §5 de `GeometriaFactory-Domain/10-Examples/ejemplo-02-intermedio.md`, líneas 45 y 46— y nombra seis fixtures: `E1.json`, `E3.json`, `E4.json`, `E5.json`, `E6.json`, `E8.json`. Los otros seis proyectos de código usan `.txt` y declaran el fundamento; Domain **no menciona `.txt` en ninguna parte de su categoría 10** ni declara el fundamento. Ahora bien: **el fundamento no se rompe**, porque el escenario que no es JSON estrictamente válido es `E-2`, y `E-2` no está entre los seis de Domain —su README declara y justifica esa exclusión—. De modo que es una inconsistencia de convención, no un dato en riesgo. Los `registro.json` y `baja.json` de `Contracts/ejemplo-01` no cuentan: son cuerpos de petición, no escenarios de §20.

**Un residuo preexistente que esta fase tocó de cerca y no levantó.** Los `06-Backlog-Tecnico/README.md` de Domain, Contracts y Application siguen diciendo «La Definition of Done vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida», y `08` está emitida desde la Fase E. No es regresión de la Fase G, que sí tocó esos `08`. Lo dejo anotado como P3 para la próxima revisión de 06.

---

## 9. Hallazgos

### P0-1 · Las carpetas de `/samples` no existen, y los siete README afirman que esta pasada las dejó esqueletadas

**Dónde.** `SDD/Docs/Proyectos/GeometriaFactory-{Api,Application,Contracts,Domain,Infrastructure,Visor,Web}/10-Examples/README.md`, §1 de cada uno.

**Qué dice.** Los siete, con la misma redacción: «Cada markdown apunta a una carpeta ejecutable de `/samples/<proyecto>/` del repositorio, que esta pasada deja **esqueletada**: con su README local y su comando previsto, y sin corrida hecha». El de Web agrega, en negrita, «**Ninguna carpeta de `/samples` promete una corrida que no se hizo**».

**Qué debería decir.** O bien las carpetas existen con su README local y su comando previsto —que es lo que `Rules-Examples.md` §0.2 asigna como salida de la pasada de diseño, junto con los markdown—, o bien los siete README declaran que la materialización de `/samples` queda pendiente y por qué. Lo que no puede hacerse es afirmar que se dejaron esqueletadas cuando el directorio `samples/` no existe.

**Cómo lo verifiqué.** `git ls-files | grep -i samples` devuelve vacío. `find . -type d -name samples -not -path "./.git/*"` devuelve vacío. El árbol de la raíz del repositorio tiene exactamente dos entradas, `README.md` y `SDD/`. Los `git show --stat` de `f8d75f3` y `d0b29c5` no crean ningún archivo fuera de `SDD/`.

**Por qué es P0.** Por tres razones acumuladas. Primera: es la mitad del entregable que §0.2 define para esta pasada, y falta entera. Segunda: es una afirmación falsa sobre trabajo producido, repetida siete veces, que es el defecto de fondo que este producto viene arrastrando y que esta categoría existe para no cometer. Tercera: arrastra a las diecinueve filas de las siete matrices de sensado, cuyo «método de verificación» es un comando que apunta a rutas inexistentes —`dotnet run --project samples/domain/01-basico`, `bash samples/web/01-datos-seed/run.sh` y sus diecisiete hermanos—, y `Deriva-Rules.md` §6 exige literalmente que «ninguna evidencia citada apunta a una ruta, identificador o comando que no resuelve».

### P1-1 · Cuatro README declaran abierta la consolidación de §16.1 que su propio commit cerró

**Dónde.** `GeometriaFactory-Domain/10-Examples/README.md:92`, `GeometriaFactory-Contracts/10-Examples/README.md:96`, `GeometriaFactory-Application/10-Examples/README.md:105`, y con reserva `GeometriaFactory-Infrastructure/10-Examples/README.md:109`.

**Qué dice.** Domain: «Lo que queda abierto: la consolidación de `PRODUCT-INTAKE` §16.1, **que hoy sigue diciendo que este proyecto de código no tiene samples propios**». Application: «cuya fila para este proyecto de código **sigue diciendo «sin samples propios»**».

**Qué debería decir.** Que la consolidación **se hizo en esta misma emisión**: la fila de Domain y Contracts la reescribió el intake 1.23, entregado en el mismo commit `f8d75f3`; la de Application, el intake 1.24, entregado en el mismo commit `d0b29c5`. Lo que queda abierto no es §16.1 sino §18, que es otra cosa y sólo Infrastructure la nombra.

**Cómo lo verifiqué.** `git show --stat f8d75f3` incluye `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`; el `git diff 5b2f63e d0b29c5` sobre el intake muestra la fila «Domain, Contracts | `library` | **`/samples/domain/` y `/samples/contracts/`** [AMPLIADO 2026-08-11]» reemplazando a «Sin samples propios». Abrí §16.1 del intake vigente y las cuatro bibliotecas tienen carpeta declarada.

**Por qué importa.** El criterio de esta ronda no penaliza un punto abierto bien declarado; penaliza uno falso. Un lector que siga «lo que queda abierto» va a ir a corregir algo que ya está corregido, y —peor— la frase le dice que la fuente vinculante de la estructura de `/samples/domain/` es el README y no §16.1, cuando §16.1 ya la declara.

### P1-2 · Cinco documentos citan como vivo un residuo de §18 que §18 no tiene

**Dónde.** `GeometriaFactory-Visor/10-Examples/README.md:95`, y como precedente invocado en `GeometriaFactory-Domain/10-Examples/README.md:92`, `GeometriaFactory-Contracts/10-Examples/README.md:96` y `GeometriaFactory-Application/10-Examples/README.md:105`. La fuente de la afirmación es `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md`, línea 149.

**Qué dice.** El Visor: «El `PRODUCT-INTAKE` §18, al describir el punto de extensión, enumera las funciones de la fachada; el `PRODUCT-MANIFEST` §5 ya registró que esa enumeración quedó como **residuo de la fuente anterior a la versión 1.6** del intake». Los otros tres: «con el mismo criterio con que el `PRODUCT-MANIFEST` §5 trata el **residuo de §18 sobre el número de funciones de la fachada**».

**Qué debería decir.** Nada de eso, o bien que el residuo **ya no existe**. §18 vigente enumera las seis funciones por nombre y las rotula «las **seis** que §17.7 P.3 declara desde 1.6». El que quedó desactualizado es el `PRODUCT-MANIFEST` §5, que sigue afirmando «La enumeración de §18 del intake sigue nombrando cinco».

**Cómo lo verifiqué.** Abrí §18 del intake 1.24 y leí la enumeración completa. Abrí la línea 149 del manifiesto y leí la afirmación contraria. Y abrí la fila 1.6 del control de cambios del intake, que registra la incorporación de `establecerMovimiento` como sexta función.

**Por qué importa.** Es la forma canónica del defecto que esta auditoría persigue: una cita hecha contra lo que otro documento dice de la fuente, en vez de contra la fuente. El caso es agravante porque la ola 1 **sabía** que esa contradicción estaba desactualizada —lo escribió en el mensaje de commit— y aun así dejó la afirmación viva en cuatro documentos que entregó en ese mismo commit, y en un quinto en el siguiente.

### P1-3 · §18 del intake quedó desalineado con §16.1, que es la misma contradicción que la 1.24 dice haber cerrado

**Dónde.** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` §18, contra §16.1 del mismo documento.

**Qué dice.** §18 declara **tres** muestras —`S-1` Visor, `S-2` Api, `S-3` Infrastructure—, su tabla de reproducibilidad en tres filas y la frase «**No hay sample de flujo de usuario final**, y es deliberado». §16.1 declara ahora carpeta propia en `/samples` para **seis** proyectos de código, y el corpus documenta diecinueve samples.

**Qué debería decir.** §18 tendría que registrar las muestras nuevas, o declarar expresamente que su tabla `S-X` cubre sólo las de demostración al cliente y que las de arista B viven en §16.1 y en las categorías 10.

**Cómo lo verifiqué.** `git diff 5b2f63e d0b29c5 -- SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`: el diff toca la cabecera de versión, dos filas de §16.1 y dos filas del control de cambios. **§18 no aparece en el diff.** Después abrí §18 en el documento vigente y verifiqué que sigue con tres filas.

**Por qué importa.** La entrada 1.24 del control de cambios describe con precisión el mecanismo de esta falla —«la contradicción sobrevivió veintitrés versiones sin que nadie la mirara porque vivía en dos celdas de tablas distintas»— y acto seguido lo reproduce: cierra la celda de Infrastructure y deja las de Domain, Contracts, Application y Web contradiciendo a §18. Una fase que nombra el patrón y no lo cierra es peor que una que no lo vio.

### P1-4 · §16.1 sigue diciendo que Web «no produce sample propio», y Web produjo uno

**Dónde.** `PRODUCT-INTAKE` §16.1, fila de `GeometriaFactory-Web`, contra `GeometriaFactory-Web/10-Examples/` entero.

**Qué dice.** §16.1: «No produce sample propio: el guion de demostración de cada etapa, ejecutado en el navegador del host, cumple ese papel (RF §9.3)». El README de Web §5 sostiene que «esa frase sigue siendo cierta de la arista A y este README no la contradice», y cierra con «**nada que elevar sobre §16.1** por parte de este proyecto de código».

**Qué debería decir.** §16.1 tendría que registrar `/samples/web/01-datos-seed/`, con la distinción de aristas que el propio README de Web argumenta tan bien. La columna de esa tabla se titula «**Qué hay en `/samples`**»: describe contenido de carpeta, no destinatarios, y hoy hay contenido declarado. Y `Rules-Examples.md` §2.1 es explícito en que «la materialización en código vive en `/samples` y **se gobierna desde §16.1 del PRODUCT-INTAKE**»: una carpeta que §16.1 no reconoce no está gobernada.

**Cómo lo verifiqué.** Leí §16.1 vigente, leí las diez secciones del README de Web y el `ejemplo-01-datos-seed.md`, y comprobé en el `git diff` del intake que la fila de Web no se tocó en ninguna de las dos olas.

**Por qué importa.** Los otros cinco proyectos de código que emitieron contra la letra de §16.1 elevaron la revisión y la revisión se hizo. Web es el único que emite contra la letra y declara expresamente que no hay nada que elevar, con lo cual la contradicción queda sin dueño y sin fecha. Es un P1 y no un P0 porque el argumento de fondo —la arista B no la cubre un guion que ejecuta una persona— es correcto y está bien construido; lo que falta es la consecuencia documental.

### P2-1 · Cita truncada de `Rules-Examples.md` §6 que suprime dónde debe vivir la justificación

**Dónde.** `GeometriaFactory-Web/10-Examples/README.md:45`.

**Qué dice.** «`Rules-Examples.md` §6 admite exactamente eso: «todo CU declarado crítico en 02 tiene al menos una sonda `VER-XX` que lo ejercita, **o la ausencia está justificada**». Ésta es la justificación».

**Qué debería decir.** El criterio completo, que es: «Todo CU declarado crítico en 02 tiene al menos una sonda `VER-XX` que lo ejercita, o la ausencia está justificada **en `Decisiones-Proyecto.md`**». El fragmento suprimido es precisamente el que fija el destino de la justificación, y el README la escribe en sí mismo.

**Cómo lo verifiqué.** `grep -n "ausencia está justificada" Rules-Examples.md` devuelve la línea 398 con el texto completo. Y `find . -name "Decisiones-Proyecto.md"` sobre todo el corpus devuelve vacío: ningún proyecto de código de este producto tiene ese artefacto.

**Nota.** El fondo del argumento de Web es correcto y lo verifiqué en §5. Lo que se objeta es la forma de la cita, que corta justo donde la regla se volvía exigente. Y queda por resolver, fuera de esta fase, si el corpus debe emitir `Decisiones-Proyecto.md` o si la regla no aplica a este producto.

### P2-2 · Cita rotulada «literalmente» con elisión no marcada de `Deriva-Rules.md` §2.3

**Dónde.** `GeometriaFactory-Domain/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md:31`, con la misma elisión sin el rótulo en `Application/…/Matriz-Sensado-Deriva.md:31` y en los `08/README.md` de Domain, Contracts y Application.

**Qué dice.** «`Deriva-Rules.md` §2.3 lo prevé **literalmente**: «cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación»».

**Qué debería decir.** La fuente dice: «Cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: **la abre AG-08 en la Fase E**, poblada solo con sondas `VER-XX` tomadas de los contratos de verificación». La elisión debe marcarse, o el rótulo «literalmente» debe caer.

**Cómo lo verifiqué.** Abrí §2.3 de `Deriva-Rules.md` y comparé carácter por carácter.

**Por qué importa poco y algo.** Poco, porque el documento **no oculta** el punto elidido: el párrafo siguiente se titula «Quién la abre» y declara y fundamenta el desvío hacia AG-10 apoyándose en §4. Algo, porque el fragmento suprimido es exactamente el que el documento se dispone a no cumplir, y rotular «literalmente» una cita recortada en ese punto es el hábito que produce los P1 de §6.

### P3-1 · La convención de extensión de los escenarios se rompe en un lugar de Domain, sin consecuencia sobre el dato

**Dónde.** `GeometriaFactory-Domain/10-Examples/ejemplo-02-intermedio.md`, líneas 45 y 46.

**Qué dice.** El árbol de la §5 nombra `E1.json`, `E3.json`, `E4.json`, `E5.json`, `E6.json` y `E8.json`.

**Qué debería decir.** `.txt`, como los otros seis proyectos de código, y con el fundamento declarado, que en Domain no aparece en ninguna parte de la categoría.

**Cómo lo verifiqué.** `grep -rn "\.json" GeometriaFactory-Domain/10-Examples/` devuelve exactamente esas dos líneas; `grep -rn "txt" ` sobre la misma carpeta devuelve vacío. Y comprobé el alcance real del riesgo: `E-2`, que es el único escenario que no es JSON estrictamente válido, **no está entre los seis**, y el README de Domain declara y fundamenta su exclusión. Los `registro.json` y `baja.json` de `Contracts/ejemplo-01` son cuerpos de petición y quedan fuera de la comparación.

**Alcance real.** Un archivo, dos líneas, seis fixtures, ningún dato en riesgo. Es inconsistencia de convención documental, no un defecto que rompa un escenario.

### P3-2 · La prosa de `SD-62` atribuye a `SD-10` una clasificación que la propia matriz no le da

**Dónde.** `GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`, párrafo «Qué aporta `SD-62`…», y el §3 del README de la categoría 10 de Web.

**Qué dice.** «Donde mira lo mismo que una fila anterior —`SD-36` para el texto original y **la fila de superficie del listado de la comisión** para los borradores— el umbral se declaró igual y no se contradice: **mayor sin gradación en los dos casos**».

**Qué debería decir.** Que el umbral de `SD-10` es «Mayor» para ese supuesto y por lo tanto no se contradice con `SD-62`, sin llamarlo «sin gradación»: la lista de filas sin gradación de §5 de esa matriz enumera `SD-27`, `SD-33`, `SD-36` a `SD-40`, `SD-43`, `SD-45`, `SD-57`, `SD-59` y `SD-60`, y **`SD-10` no está en ella**.

**Cómo lo verifiqué.** Abrí `SD-10` —«**Mayor**: aparece un trabajo en estado `Borrador`, o el listado ofrece aprobar o rechazar sin abrir el trabajo»— y la lista de §5.

**Por qué no es más grave.** Porque **no hay contradicción de umbral**, que era lo que había que descartar: `SD-10` no tiene tramo menor para ese supuesto, de modo que las dos sondas coinciden en el veredicto. Lo que sobra es el adjetivo. Y la emisión hizo bien en no agregar `SD-10` a la lista de §5, porque eso habría modificado una de las 61 filas.

### P3-3 · Residuo preexistente en los `06-Backlog-Tecnico/README.md` de tres proyectos de código

**Dónde.** `GeometriaFactory-{Domain,Contracts,Application}/06-Backlog-Tecnico/README.md`.

**Qué dice.** «La Definition of Done vive en `08-Calidad-Y-Pruebas`, que todavía no está emitida».

**Qué debería decir.** `08` está emitida desde la Fase E y esta fase le agregó un octavo artefacto.

**Cómo lo verifiqué.** `grep` sobre el corpus y `ls` de las carpetas `08` de los tres.

**Constancia.** **No es regresión de la Fase G.** Se registra porque esta fase tocó los `08` de esos tres proyectos de código y era el momento natural de levantarlo, y porque un lector que llegue por 06 seguirá creyendo que la DoD no existe.

---

## 10. Lo que no pude verificar

- **Si las carpetas de `/samples` estaban previstas para otra fase.** `Rules-Examples.md` §0.2 las asigna a la pasada de diseño y ningún documento del corpus declara un desvío de eso. Pero no encontré ninguna instrucción de orquestación que las excluyera explícitamente, de modo que no puedo descartar que exista un acuerdo fuera del corpus auditado. **El hallazgo P0-1 no depende de eso**: aunque el desvío estuviera acordado, los siete README seguirían afirmando que las carpetas se dejaron esqueletadas y esa afirmación seguiría siendo falsa.
- **La corrección aritmética de los criterios de aceptación que involucran recuentos de datos seed.** El criterio de `VER-01` de Web declara «Pendiente=4, Borrador=2, Aprobado=1 y Rechazado=1» sobre los ocho escenarios, y el listado propio en 8 y el de la comisión en 6. La distribución es coherente con lo que §20 declara para cada escenario —`E-5` y `E-8` quedan en `Borrador`, el resto pasa a `Pendiente`— pero los estados `Aprobado` y `Rechazado` los produce el propio seed por decisión suya, no §20, de modo que no hay fuente contra la cual contrastarlos. **No verificado.**
- **Si los `US-XX` citados en los bloques `verifica` corresponden en contenido a lo que cada sample ejercita.** Verifiqué que existen y que están dentro del rango emitido de cada proyecto de código, no que cada historia sea la adecuada. Habría exigido abrir 176 historias. **No verificado en contenido.**

---

## 11. Dictamen

**RECHAZADO.**

El fundamento es el P0-1 y no admite lectura benigna. La pasada de diseño tiene dos productos, la categoría entregó uno, y los siete documentos que la índexan **afirman haber entregado los dos**. No es un olvido: es una afirmación positiva y textual, repetida siete veces, sobre trabajo que no se hizo —«esta pasada deja esqueletada la carpeta, con su README local y su comando previsto»—, en la categoría cuya razón de ser, según sus propias reglas, es que el ejemplo deje de ser prosa y se vuelva verificable. Un producto que viene arrastrando afirmaciones falsas sobre otras fuentes no puede cerrar la fase que instala el instrumento de verificación con una afirmación falsa sobre su propio entregable. Y la consecuencia es operativa, no estética: las diecinueve sondas que esta fase da de alta tienen como método de verificación un comando que no resuelve, contra la exigencia expresa de `Deriva-Rules.md` §6.

A eso se suman cuatro P1 que comparten raíz y que la fase estaba en posición de evitar: cuatro puntos abiertos falsos sobre §16.1 que el propio commit cerró, cinco documentos que citan un residuo de §18 que la ola 1 sabía inexistente, §18 desalineado por la misma mecánica que la 1.24 describe y no aplica, y la fila de Web que quedó contradicha y sin dueño.

**Lo que la ronda reconoce, y no es poco.** Los diecinueve contratos nacen sin evidencia, en la forma exacta que la regla fija, y ninguno finge una corrida. Los siete huecos heredados están cerrados con desenlace y el único que no se cierra se declara abierto con fundamento verdadero. Las 61 filas de Web y las 12 del Visor están intactas, verificado por diferencia. Los diez recuentos dan. La cobertura de casos de uso, reconstruida con herramienta, es verdadera en los siete proyectos de código. La forma es impecable: cero enlaces rotos, cero filas mal formadas, diez secciones en los diecinueve. El razonamiento del sample único de Web y el de la exclusión de `E-3` en el Visor son de los mejores argumentos escritos en este corpus.

**Qué habría que hacer para levantar el rechazo.** Materializar las carpetas de `/samples` con su README local y su comando previsto, o corregir los siete README para que declaren lo que efectivamente hay; sacar de Domain, Contracts, Application e Infrastructure el punto abierto sobre §16.1 y reemplazarlo por el que sí sigue abierto, que es §18; sacar de los cinco documentos el residuo de las cinco funciones y —esto es del Product Owner— corregir el `PRODUCT-MANIFEST` §5, que es donde vive la afirmación falsa; alinear §18 con §16.1; y resolver la fila de Web en §16.1. Los dos P2 y los tres P3 pueden ir en la misma ronda o en la siguiente.

---

## 12. ¿Alcanzan estos ejemplos para construir el producto desde la especificación?

Sí, y con margen, una vez levantado el P0. Ésa es la paradoja de esta ronda: la sustancia técnica de la categoría es la mejor de las fases auditadas hasta acá, y lo que la hunde es la contabilidad de lo que dice haber producido.

Diecinueve contratos con criterio de aceptación evaluable, declarados **antes** de que exista una línea de código, cubren los setenta y un casos de uso del producto en los siete proyectos —trece, ocho, siete, once, diez, diez y doce, con la única ausencia justificada de siete casos de uso de Web—, y transportan los ocho escenarios reales del intake sin sustituirlos por datos sintéticos. Doce de los diecinueve declaran además **aserciones negativas**, y ahí está lo que de verdad hace útil a esta categoría: no verifican que el camino feliz funcione, verifican que el modo de falla más probable no pase inadvertido. Que el índice de figura reportado sea 1 y no 0 en `E-5`. Que el cilindro de `E-1` no tenga observación, porque su diferencia es de exactamente 0.01 y el operador es estricto. Que la negativa por pertenencia no salga como negativa por facultad. Que un solo borrador visible en el listado de la comisión sea la falla y no una diferencia de grado. Que una contraseña provisoria no se derive de un dato de la cuenta. Un equipo —o un agente— que codifique contra estos diecinueve criterios va a construir el producto que la especificación describe, y va a enterarse el mismo día en que se desvíe.

Lo que hoy no puede hacer es **correrlos**, y no por falta de código: por falta de las carpetas donde el código va a vivir, que esta pasada debía dejar armadas y declaró haber armado. Es una hora de trabajo y una corrección de siete párrafos. Hasta que se haga, los diecinueve criterios son excelentes especificaciones de prueba y ninguno es un arnés.

---

## 13. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Ronda 1 de la auditoría de la Fase G. **RECHAZADO** sobre un **P0** —las carpetas de `/samples` no existen y los siete README afirman haberlas dejado esqueletadas, con las diecinueve sondas apuntando a comandos que no resuelven—, cuatro **P1** —cuatro puntos abiertos falsos sobre §16.1 cerrado en el mismo commit; cinco documentos citando un residuo de §18 inexistente tomado del `PRODUCT-MANIFEST` en vez de la fuente; §18 desalineado por la mecánica que la 1.24 describe y no aplica; y la fila de Web contradicha y sin dueño—, dos **P2** de forma de cita y tres **P3**. Se reconoce como correcto: los diecinueve contratos sin evidencia en la forma exacta de `Rules-Examples.md` §0.2, los siete huecos heredados cerrados con desenlace y el único no cerrable declarado con fundamento verdadero, las 61 filas de Web y las 12 del Visor intactas verificadas por `git diff`, los diez recuentos, la cobertura de casos de uso reconstruida con herramienta en los siete proyectos de código, y la forma sin un solo enlace roto ni una fila mal formada. Se declaran **tres** puntos no verificados. |
