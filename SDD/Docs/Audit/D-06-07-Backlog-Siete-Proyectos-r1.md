# Auditoría de la Fase D · categorías 06 Backlog Técnico y 07 Plan de Sprint de los siete proyectos de código · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/fase-d-backlog` |
| Objeto de la ronda | Dictaminar la Fase D emitida en dos olas, commits `914cc43` (tres proyectos de código de nivel topológico 0) y `818997f` (los cuatro restantes) |
| Alcance auditado | Los **208** documentos nuevos de `Proyectos/*/06-Backlog-Tecnico/` y `Proyectos/*/07-Plan-Sprint/` —contados sobre `git diff --name-only 497397c HEAD`, que da 209 rutas, de las cuales una es la modificación de `GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md` y no un documento nuevo—. Más las fuentes contra las que se contrastan |
| Fuentes de contraste | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18**, `00-Contexto/Roadmap-Producto.md`, `01-Necesidades-Negocio/`, y las categorías **02**, **03** y **05** de cada proyecto de código; `IA.SDD/SDD/Devs/Rules/Rules-Backlog-Tecnico.md` **3.1** y `Rules-Plan-Sprint.md` **3.1** (repositorio de origen, **sólo lectura**) |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ninguna cobertura se acepta por estar declarada: las matrices se recorren fila por fila con herramienta, los recuentos se cuentan de nuevo, y ninguna cita entrecomillada se da por buena sin abrir el documento citado. El antecedente pesa: el rechazo de la Fase C fue por dos citas de un texto del intake que ya no existía |
| Fuera de alcance | `_legacy/`; las tres fuentes originales del intake, que viven en otro repositorio bajo `PROMPTs/`; las categorías 04 y 08 a 11, no emitidas |
| Auditor | Auditor independiente, sin participación en la emisión |
| Fecha | 2026-08-11 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Cobertura y trazabilidad](#2-cobertura-y-trazabilidad)
- [3. Subordinación al plan de etapas](#3-subordinacion-al-plan-de-etapas)
- [4. Los tres apartamientos conscientes](#4-los-tres-apartamientos-conscientes)
- [5. Veracidad de las afirmaciones sobre otras fuentes](#5-veracidad-de-las-afirmaciones-sobre-otras-fuentes)
- [6. Recuentos y conjuntos cerrados, contados de nuevo](#6-recuentos-y-conjuntos-cerrados-contados-de-nuevo)
- [7. Forma](#7-forma)
- [8. Hallazgos](#8-hallazgos)
- [9. Lo que no pude verificar](#9-lo-que-no-pude-verificar)
- [10. Dictamen](#10-dictamen)
- [11. ¿Alcanza este backlog para construir el producto?](#11-alcanza-este-backlog-para-construir-el-producto)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Resumen ejecutivo

**La cobertura está completa y la verifiqué con herramienta, no leyendo declaraciones.** Los **67** casos de uso de los siete proyectos de código —Domain 13, Contracts 8, Visor 7, Application 11, Web 10, Infrastructure 10, Api 12— reciben todos al menos una tarea técnica, y lo comprobé reconstruyendo la cobertura inversa desde las propias matrices en lugar de leer el párrafo que la declara. Las **148** tareas técnicas tienen las 148 filas en las siete matrices `BT ↔ US ↔ CU`, sin una sola celda vacía en la columna de historias consumidoras ni en la de fuente de arquitectura. Las **180** historias declaran todas al menos un caso de uso, y ninguna cita un identificador que su categoría 02 no tenga. Las **16** reglas de negocio, los **9** invariantes, las **9** necesidades de negocio y los **8** escenarios del anexo del intake aparecen todos, sin excepción, en al menos un artefacto del backlog.

**Los siete backlogs se subordinaron al plan de etapas y ninguno inventó un orden propio.** Las épicas de los siete llevan los nombres de las fases de `Roadmap-Producto.md` §3, letra por letra, y ninguna etapa fue renombrada, reordenada ni inventada. Cada etapa que un proyecto de código no toca está declarada con motivo, en `Product-Backlog.md` §2 y repetida en `Mini-Plan.md` §2. **El punto delicado está bien resuelto**: `GeometriaFactory-Visor` y `GeometriaFactory-Web` declaran el momento en que se miden `PT-02` y `PT-03` como **momento y no como etapa**, con esas palabras, y el Visor le dedica una sección entera —§2.1, «Por qué EP-02 no es una etapa nueva»— a fundamentarlo.

**Los tres apartamientos conscientes están bien fundados y bien declarados, y los tres los verifiqué contra el intake abriendo el intake.** «Sin plazo; el avance se mide por etapas cerradas» está literal en §8 y en §10 del intake 1.18; `equipo_n = 1` está literal en §2; y el reparto 18 `Must Have` sobre 26 capacidades lo conté yo mismo sobre la tabla de §4 y da exactamente 18 de 26. Ninguno de los tres es una omisión disfrazada: los tres declaran qué no hicieron, por qué, quién lo cerraría y cuándo.

**Muestreé las citas de más peso y no encontré ninguna falsa.** Abrí las seis que más carga argumental sostienen y las seis resisten: «a definir por el docente» está literal en §17.3.P.4; «una propiedad exigida explícitamente» está literal en §16.1; «un caso de uso, una unidad de trabajo» es el título de `ADR-05` de Application; la vigencia «corta» sin acceso de refresco está en §17.5.P.5; las dos candidatas de derivación de clave están en §17.3.P.1; y la cita de la redacción retirada —«ninguna escritura anónima en el sistema»— se usa **correctamente**, presentada como lo que el intake corrigió y no como lo que el intake dice. Cero citas falsas en la muestra.

**Los barridos mecánicos dan cero en cinco familias de regresión.** Cero enlaces relativos rotos sobre los 208 documentos; cero filas de tabla con más o menos celdas que su encabezado; cero identificadores fantasma en los mini-planes; cero identificadores de tres dígitos; cero menciones a stacks concretos o productos comerciales, contra un criterio de aceptación explícito de la guía que la Fase C sí había violado.

**Levanto cuatro hallazgos: ninguno P0, ninguno P1, uno P2 y tres P3.** El P2 es un recuento falso en el registro del repositorio —el mensaje del commit `818997f` afirma 232 historias donde hay 180— que ya se propagó fuera del corpus. Los tres P3 son enumeraciones de cobertura inversa incompletas respecto de su propia matriz, un umbral de la guía aplicado en su banda laxa, y la uniformidad sospechosa —aunque individualmente honesta— de la distribución MoSCoW.

**Dictamen: APROBADO.**

---

## 2. Cobertura y trazabilidad

Es lo propio de esta fase y es donde puse el grueso del esfuerzo. Verifiqué las dos direcciones.

### 2.1 De la fuente al trabajo: nada quedó sin cubrir

**Los 67 casos de uso.** Conté los archivos de `02-Especificacion-Funcional/Casos-De-Uso/` de cada proyecto de código y reconstruí desde cada matriz `BT ↔ US ↔ CU` el conjunto de tareas técnicas que alcanza a cada caso de uso. Los 67 tienen al menos una. El reparto que conté es Domain 13, Contracts 8, Visor 7, Application 11, Web 10, Infrastructure 10, Api 12.

**Ningún caso de uso citado es inexistente.** Barrí las 166 fichas de historia más los 14 bloques inline del Visor extrayendo la fila `CU cubiertos` de cada tabla de trazabilidad y comprobando que el identificador cae dentro del rango real de la categoría 02 de ese proyecto de código. Cero identificadores fuera de rango. Es la comprobación que atrapa la historia que se inventa un caso de uso para tener a qué trazar.

**Las 16 reglas de negocio.** `RN-01` a `RN-16` aparecen todas en las carpetas de 06, entre 18 y 54 ocurrencias cada una. La menos citada es `RN-15` —el reseteo que no exige cuenta habilitada— con 18, y la más citada `RN-16` con 54, que es coherente con que sea la regla que unificó el primer ingreso con el reseteo y que por eso toca a casi todas las capas.

**Los 9 invariantes.** `INV-01` a `INV-09` aparecen todos, entre 9 y 32 ocurrencias. `INV-09` —la marca de cambio de contraseña pendiente— es el más citado, con 32.

**Las 9 necesidades de negocio.** `NB-00001` a `NB-00009` aparecen todas. `NB-00008`, la que dos proyectos de código declaran explícitamente no tocar, es la menos citada con 19 ocurrencias, y aun así tiene trabajo asociado en Web, Api e Infrastructure.

**Los 8 escenarios del anexo.** `E-1` a `E-8` aparecen los ocho. Es el conjunto cuya cita falsa provocó el rechazo de la Fase C —donde se citaba «E-1 a E-7» y «siete escenarios»—, y esta vez el corpus lo trata bien: `GeometriaFactory-Infrastructure` §2 EP-05 declara «la batería de **10** casos sobre los **ocho** escenarios», con el número correcto.

**Los puntos abiertos de la categoría 05.** Los siete backlogs recogen los puntos abiertos de su `05` §11 y los convierten en tarea técnica cuando la admiten, o los declaran no propios cuando la decisión pertenece a otro proyecto de código. Conté 54 puntos abiertos declarados en las siete secciones §6, y el patrón dominante es la remisión explícita al identificador de origen (`05` §11 `PA-XX`).

### 2.2 Del trabajo a la fuente: nadie inventó trabajo

**Las 148 tareas técnicas tienen fuente.** Recorrí las siete matrices de §4 con herramienta comprobando que ninguna fila tenga vacía la columna de historias consumidoras ni la de fuente de arquitectura. Cero celdas vacías. Donde no hay historia consumidora, la celda dice «Infraestructura compartida» con la justificación al lado, que es exactamente la salida que la guía §4.5 punto 7 admite.

**Las 180 historias tienen caso de uso.** Ninguna de las 166 fichas carece de referencia a un `CU-XX`, y los 14 bloques inline del Visor tampoco.

**Ninguna historia inventa una capacidad.** Las capacidades `F-01` a `F-26` que aparecen en el backlog son las del intake §4, y las cuatro que no aparecen citadas de forma individual —`F-16` entre ellas— aparecen dentro de rangos («F-15 a F-17») en las secciones que declaran qué queda fuera del tramo comprometido.

**Los mini-planes no inventan identificadores.** Comprobé que los `US-XX` y `BT-XX` de los siete `Mini-Plan.md` caen dentro del rango real del backlog de su proyecto de código: cero fantasmas. Y comprobé lo inverso, que es más exigente: los siete mini-planes comprometen **todas** las historias y **todas** las tareas técnicas de su backlog, sin dejar ninguna fuera de tramo.

### 2.3 Las matrices: todas las filas, y las filas verdaderas

Las siete matrices tienen tantas filas como tareas técnicas: Domain 16, Contracts 18, Visor 18, Application 21, Web 23, Infrastructure 26, Api 26. Suman 148, que es el total declarado.

Verifiqué la **verdad** de las filas por el camino más severo que la estructura admite: reconstruí la cobertura inversa desde la matriz y la comparé con la cobertura inversa que el documento afirma. En cinco de los siete proyectos de código el párrafo de cobertura inversa es una selección deliberada —nombra las tareas técnicas sustantivas y omite las de alcance general, y su afirmación es «al menos una», que se cumple—; en los otros dos se lee como exhaustivo y no lo es del todo. Eso es el hallazgo **D-06-02**, P3.

---

## 3. Subordinación al plan de etapas

**Las ocho etapas `a` a `h` del intake §15 sobrevivieron intactas.** Las épicas de los siete backlogs llevan los nombres de la columna «Épica candidata» de `Roadmap-Producto.md` §3, sin renombrar: «Esqueleto ambulante y verificación de viabilidad», «Navegación y sistema visual», «Identidad del administrador y sesión», «Ciclo de vida de la cuenta de alumno», «Gestión del trabajo», «Interpretación y verificación del dato del alumno», «Visualización del trabajo», «Desenlace de la entrega». Ninguna épica introduce una etapa nueva y ninguna altera el orden.

**Cada proyecto de código declara con motivo las etapas que no toca.** Domain toca 6 de 8 y declara `b` y `g` sin trabajo; Infrastructure toca 5 y declara `b`, `g` y `h`, con el motivo de la `h` explicitado —su aporte ya está construido en la `e`—; Contracts y Web tocan las ocho; Application y Api tocan seis; el Visor tiene una estructura de tres tramos propia de su condición de bundle. En los siete casos la declaración vive en `Product-Backlog.md` §2 y se repite, con enlace relativo que resuelve, en `Mini-Plan.md` §2.

**El punto sensible —el «momento previo a la etapa `g`»— está bien resuelto en los dos proyectos de código que lo declaran.**

- `GeometriaFactory-Visor` le dedica una sección con título propio: `Product-Backlog.md` §2.1, «Por qué EP-02 no es una etapa nueva». La columna de etapa de EP-02 no dice una letra: dice **«Antes de comprometer la etapa `g`»**, con la remisión a `Roadmap-Producto.md` §2.2. Y `Mini-Plan.md` §1.3 lo nombra sin ambigüedad: «**Un momento declarado del roadmap, no una etapa.**»
- `GeometriaFactory-Web` hace lo mismo en `Mini-Plan.md` §1.3: «Este proyecto de código toca **las ocho** etapas comprometidas, y además participa de un **momento** que el roadmap declara y que no es una etapa».

Ninguno de los dos le puso letra, ninguno lo intercaló en la secuencia, y los dos remiten al mismo párrafo del roadmap. Es exactamente lo que correspondía.

**Y hay una señal que va más allá de lo pedido.** Los dos proyectos de código detectaron, desde los dos lados de la fachada, que `US-21` de Web y las dos historias `Should` del Visor están **dentro de lo que `PT-02` mide antes de comprometer la etapa `g`**, de modo que en la práctica no son diferibles aunque su prioridad declarada lo admita. Los dos **se negaron a subirles la prioridad**, porque eso habría sido reprioritizar una capacidad del Product Owner, y elevaron la tensión como punto abierto —`PA-06` en Visor, `PA-02` en Web— para que la decida quien corresponde. Es la disciplina de frontera que la Fase C había mostrado, sostenida.

---

## 4. Los tres apartamientos conscientes

Los audito como apartamientos, que es lo que son: la pregunta no es si se apartan de la guía —se apartan— sino si el fundamento es verdadero y está declarado.

### 4.1 Las estimaciones «Sin fijar»

**Fundado y declarado. No es una omisión.**

La guía §4.7 y §6 exigen declarar una técnica de estimación y mantenerla. Los siete backlogs no la fijan y **lo declaran en una subsección con título propio**, `Product-Backlog.md` §4.1, «Por qué la unidad de estimación queda abierta».

El fundamento que invocan lo verifiqué abriendo el intake. En la línea 297, §8: «Sin plazo calendario: RT §1 declara «sin plazo; el avance se mide por etapas cerradas»». Y en la línea 323, §10: «**Sin fecha**, justificado: «sin plazo; el avance se mide por etapas cerradas»». La cita es literal y aparece dos veces. El argumento derivado también cierra: sin iteraciones cerradas no hay velocidad de la que derivar puntos, y la unidad de planificación de este producto es la etapa y no el sprint (`Roadmap-Producto.md` §1.2).

La consecuencia está aplicada con consistencia: las 180 historias y las 148 tareas técnicas dicen «Sin fijar» en su campo de estimación, y la decisión de si alguna vez se estima queda como `PA-01` en los siete backlogs, con dueño —el Product Owner, que es también quien ejecuta— y momento —al cerrar la primera etapa con carga funcional—.

**Es la salida correcta.** Poner números habría convertido una decisión declarada del Product Owner en una omisión aparente, y la categoría 07 los habría tomado como capacidad.

### 4.2 La categoría 07 emite sólo `Mini-Plan.md`

**Fundado y declarado, y además es lo que la guía manda, no un apartamiento.**

Esto merece una precisión. `Rules-Plan-Sprint.md` §2.1 declara `Mini-Plan.md` **obligatorio** para proyectos de código de 1 dev, y declara los otros cuatro artefactos —`Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md`, `Velocidad-Equipo.md`— a **omitir** en ese caso. §2.2 lo repite en tabla. Y §6 tiene un criterio de aceptación explícito: «Para proyectos de código de un solo dev, existe `Mini-Plan.md` y no existen los cuatro artefactos completos».

De modo que emitir sólo el mini-plan **cumple la guía**; emitir los cuatro habría sido la infracción.

El insumo que dispara esa rama lo verifiqué en el intake, línea 136, §2: «**Cantidad de personas del equipo de desarrollo: `equipo_n = 1`.**», con el razonamiento de por qué el agente de IA no cuenta como persona, y con la consecuencia nombrada en el propio intake: «que la categoría 07 emita únicamente `Mini-Plan.md`».

Los siete `README.md` de la sección 07 declaran los cuatro artefactos omitidos **con el motivo de cada uno**, en vez de dejarlos ausentes sin explicación. Eso está por encima del mínimo.

### 4.3 La distribución MoSCoW casi toda `Must`

**Fundado y declarado, y con una reserva que registro como P3 y no como defecto.**

El recuento, hecho por mí sobre las cabeceras de las 166 fichas más los bloques inline del Visor: **174 `Must`, 5 `Should`, 2 `Could`** sobre 180 historias, es decir 96,7 % `Must`.

La guía es dura con esto: el anti-patrón «Todo Must Have» está en §4.8 y la distribución sugerida de §4.7 es 50-60 % `Must`. Pero el criterio de aceptación de §6 es más estrecho de lo que la tabla sugiere: «La distribución MoSCoW **no es 100 % Must**; hay reparto razonable entre Must, Should y Could». No es 100 %, y hay `Should` y hay `Could`.

**El fundamento que los backlogs declaran es verdadero y lo conté yo.** El intake §4 declara 26 capacidades, y son 18 `Must Have`: F-01 a F-12, F-21, F-22, F-23, F-24, F-25 y F-26. Las ocho restantes son 2 `Should` (F-13, F-14), 3 `Could` (F-15, F-16, F-17) y 3 `Won't v1` (F-18, F-19, F-20). Dieciocho sobre veintiséis, exacto.

**Y el argumento va más lejos que ese recuento, correctamente.** Las etapas comprometidas `c` a `h` contienen 19 capacidades y **sólo una de ellas no es `Must Have`**: `F-13`. Las `Could` y las `Won't` viven en la fase `i…`, que este backlog no planifica. De modo que un backlog que cubre exactamente el tramo comprometido y refleja fielmente la prioridad de su fuente **tiene que** salir casi todo `Must`. El recorte por prioridad no desaparece: se reemplaza por el recorte por etapa, que es la unidad que este producto sí tiene, y los siete backlogs lo dicen con esas palabras en su §4.2.

**Las siete historias no-`Must` son honestas, una por una.** Las abrí. Ninguna es un `Should` de compromiso puesto para no dar 100 %:

- Domain `US-12`: su origen no es una capacidad sino una decisión técnica pre-tomada del intake §17.1.P.11.
- Contracts `US-10` (`Could`): el resumen por alumno y por estado, que es `F-15`, `Could Have` en la fuente.
- Application `US-16`: su origen es la decisión de `05` §4 sobre la indisponibilidad de un puerto, no una capacidad.
- Infrastructure `US-23`: su caso de uso es el único de los diez que no traza a ninguna necesidad de negocio. **Verifiqué esa afirmación** abriendo `02` §7.2, que declara «Nueve de los diez casos de uso trazan al menos a una necesidad de negocio, y uno no traza a ninguna» y marca `CU-09`.
- Api `US-30`: la colección de peticiones, que demuestra y no implementa.
- Web `US-21` y las dos `Should` del Visor: `F-13`, la única `Should Have` de la fuente que toca a esas piezas.

La reserva que registro como **D-06-03** es de otra clase: que seis de los siete proyectos de código tengan **exactamente una** historia no-`Must` es un patrón demasiado regular para ser casualidad, y el propio mensaje del commit lo admite («Cada proyecto declara una Should con origen verificable»). No es un defecto —cada una está fundada y verificada— pero es una señal de que el ejercicio de priorización se resolvió por proyecto de código como un mínimo a alcanzar y no como una lectura independiente.

---

## 5. Veracidad de las afirmaciones sobre otras fuentes

Es el defecto de fondo de este producto y por eso lo traté como el eje. **No di por buena ninguna cita entrecomillada: abrí la fuente en todos los casos que reporto.**

Recolecté las citas con delimitadores angulares de los 208 documentos y las agrupé. La mayoría son nombres de componente de la categoría 05 entrecomillados como término, no como cita de texto. Muestreé las seis que sostienen carga argumental real:

| Cita | Dónde se usa | Qué fuente se le atribuye | Verificación |
| --- | --- | --- | --- |
| «sin plazo; el avance se mide por etapas cerradas» | Los siete `Product-Backlog.md` §4.1 | `PRODUCT-INTAKE` §8 y §10 vía `Roadmap-Producto.md` §1.1 | **Verdadera.** Literal en el intake 1.18, líneas 297 y 323 |
| «a definir por el docente» | Infrastructure `Backlog-Tecnico.md` BT-26 y `Product-Backlog.md` `PA-07` | `PRODUCT-INTAKE` §17.3.P.4 | **Verdadera.** Literal: «Frecuencia a definir por el docente» |
| «una propiedad exigida explícitamente» | Visor `Product-Backlog.md` US-14 | `PRODUCT-INTAKE` §16.1 | **Verdadera.** Literal, y con el contraste que el backlog reproduce: «no un agregado de conveniencia» |
| «un caso de uso, una unidad de trabajo» | Application `US-05`, Infrastructure `US-09` | `05` §4 de Application | **Verdadera.** Es el punto 4 de §4 y el título de `ADR-05` |
| «ninguna escritura anónima en el sistema» | Contracts `US-02` | Redacción **retirada** del intake 1.13 | **Verdadera y bien encuadrada.** El backlog la presenta como lo que el intake corrigió el 2026-08-09 por ser falsa, no como lo que el intake dice. Es el uso correcto de una cita muerta |
| Vigencia «corta», sin acceso de refresco, sin número | Api `Product-Backlog.md` `PA-05` | `PRODUCT-INTAKE` §17.5.P.5 | **Verdadera.** La tabla dice «Vigencia \| Corta. Renovación por reingreso; **sin token de refresco** en este alcance», y en efecto no fija número |

**Cero citas falsas en la muestra.** Y una observación que va al eslabón, que es lo que la Fase C enseñó: la cita de Contracts `US-02` es el caso más difícil del corpus —citar un texto que la fuente **retiró**— y está resuelto del único modo que no produce una afirmación falsa, que es declarando que es la redacción anterior y por qué se retiró.

**Además verifiqué dos afirmaciones sobre puntos abiertos, para descartar el punto abierto falso**, que es el defecto que el criterio negativo sí admite como hallazgo:

- Api `PA-05` afirma que el intake declara la vigencia «corta» y **no fija número**. Abrí §17.5.P.5: no hay número. Punto abierto verdadero.
- Infrastructure `PA-03` afirma que el intake declara **dos candidatas** de función de derivación de clave sin anclar ninguna. Abrí §17.3.P.1 y §17.3.P.5: declara dos, con la disyunción explícita, y ancla las versiones «en la etapa `a`». Punto abierto verdadero.

No encontré ningún punto abierto que su fuente ya hubiera resuelto.

---

## 6. Recuentos y conjuntos cerrados, contados de nuevo

Conté todos sobre el instrumento. La columna «Contado» es mi recuento, no el del corpus.

| Conjunto | Esperado | Contado | Dónde y cómo |
| --- | --- | --- | --- |
| Reglas de negocio | 16 | **16** | `RN-01` a `RN-16` sobre el intake §4.1, sin huecos |
| Invariantes | 9 | **9** | `INV-01` a `INV-09` sobre el intake |
| Escenarios del anexo | 8 | **8** | `§20.E-1` a `§20.E-8`, contando los encabezados de tercer nivel de la Parte D |
| Necesidades de negocio | 9 | **9** | Nueve archivos en `01-Necesidades-Negocio/Necesidades-De-Negocio/`, `NB-00001` a `NB-00009` |
| Códigos de contrato vivos | 15 sobre 18 emitidos | **15** | Filas de datos de `Contracts/05/Contratos-Abstractions.md` §5.1: quince. El corpus declara además los tres retirados y la regla de no reciclado |
| Puntos de acceso | 15 | **15** | Filas de `Api/02/Definicion-Superficie-HTTP.md` §3: `A-01` a `A-03` y `A-05` a `A-16`, sin `A-04`. Tres más doce, quince. El cuadre que el propio documento publica —cuatro sin acceso firmado más once bajo la guardia— también da quince |
| Funciones de fachada | 6 | **6** | Filas de la tabla del intake §17.7.P.3: `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir`, `establecerMovimiento` |
| Casos de uso · Domain | 13 | **13** | Archivos de `02-Especificacion-Funcional/Casos-De-Uso/` |
| Casos de uso · Contracts | 8 | **8** | Ídem |
| Casos de uso · Visor | 7 | **7** | Ídem |
| Casos de uso · Application | 11 | **11** | Ídem |
| Casos de uso · Web | 11 | **10** | **Discrepa.** Ver abajo |
| Casos de uso · Infrastructure | 10 | **10** | Ídem |
| Casos de uso · Api | 12 | **12** | Ídem |

**La discrepancia de Web no es del backlog.** `GeometriaFactory-Web` tiene **diez** casos de uso, `CU-01` a `CU-10`, y su `02/Especificacion-Funcional.md` §3 los enumera en diez filas. El backlog dice «diez» con esa palabra en tres lugares —`Backlog-Tecnico.md` §4, su control de cambios y el `Mini-Plan.md` §8— y su cobertura inversa tiene diez entradas. El once del encargo de auditoría es un dato erróneo aguas arriba de esta ronda, y lo declaro como tal: **el backlog cuenta bien y quien esperaba once contaba mal**. Lo que sí hay son once **superficies** en Web —`05` §3.4— y el backlog las cubre y las declara aparte; es probable que ahí nazca la confusión, y como los contextos son disjuntos no lo reporto como hallazgo.

**Los recuentos propios de la fase.**

| Magnitud | Declarado | Contado | Veredicto |
| --- | --- | --- | --- |
| Documentos nuevos | 208 | **208** | Cuadra. 67 en `914cc43` y 141 en `818997f` —142 rutas menos una modificación— |
| Tareas técnicas | 148 | **148** | Cuadra. 16 + 18 + 18 + 21 + 23 + 26 + 26 |
| Historias de usuario | 232 | **180** | **No cuadra.** 27 + 22 + 14 + 32 + 30 + 25 + 30. Ver hallazgo `D-06-01` |
| Estimaciones sin fijar en la primera ola | 115 | **115** | Cuadra. 63 historias más 52 tareas técnicas |

---

## 7. Forma

Todo lo de esta sección se verificó con barrido mecánico sobre los 208 documentos, no por muestreo.

**Versiones y estado.** Los 208 declaran `Versión: 1.0` y estado: 42 dicen `Propuesto` —los seis artefactos por proyecto de código: `Product-Backlog.md`, `Backlog-Tecnico.md`, `Definition-Of-Ready.md`, los dos `README.md` y el `Mini-Plan.md`, por siete— y 166 dicen `Propuesta`, que son las fichas de historia. La concordancia de género es correcta y no hay ningún documento sin versión o sin estado.

**Tablas de control de cambios.** Los 208 tienen sección de control de cambios. Cero excepciones.

**Filas con tantas celdas como columnas.** Recorrí todas las tablas de los 208 documentos comparando el número de celdas de cada fila con el de su encabezado, neutralizando los separadores dentro de tramos de código. **Cero filas discordantes.**

**Enlaces relativos.** Resolví los enlaces relativos de los 208 documentos contra el árbol real. **Cero rotos.**

**Tabla de contenido.** Los `Product-Backlog.md`, `Backlog-Tecnico.md`, `Definition-Of-Ready.md` y `Mini-Plan.md` de los siete proyectos de código la llevan inmediatamente después de la cabecera, como exige la guía §4.1 para documentos de más de tres secciones de primer nivel. Las fichas de historia no la llevan, y corresponde: la guía exceptúa los documentos breves.

**Identificadores.** Cero ocurrencias de identificadores de tres o más dígitos —`US-001`, `BT-001`, `EP-001`— en los 208 documentos. La corrección obligatoria que la guía §3.2 hereda del antecedente Motor DSL está aplicada.

**Umbrales de archivo propio.** La guía §2.1 exige archivo individual por historia a partir de **20** historias, lo recomienda entre 10 y 20, y admite inline por debajo de 10; para tareas técnicas el umbral obligatorio es **30**.

| Proyecto de código | Historias | Modo | Tareas técnicas | Modo | Veredicto |
| --- | --- | --- | --- | --- | --- |
| Domain | 27 | Archivo propio | 16 | Inline | Correcto |
| Contracts | 22 | Archivo propio | 18 | Inline | Correcto |
| Visor | 14 | **Inline** | 18 | Inline | Admisible; ver `D-06-04` |
| Application | 32 | Archivo propio | 21 | Inline | Correcto |
| Web | 30 | Archivo propio | 23 | Inline | Correcto |
| Infrastructure | 25 | Archivo propio | 26 | Inline | Correcto |
| Api | 30 | Archivo propio | 26 | Inline | Correcto |

**Criterios de aceptación en Given/When/Then.** Las 166 fichas tienen **al menos dos** escenarios en formato `Given … When … Then`. Cero excepciones. La guía lo exige para las `Must` y `Should`, y acá lo cumplen todas.

**Definition of Ready.** La guía exige entre 5 y 8 criterios para historias y entre 4 y 6 para tareas técnicas. Los siete documentos caen dentro: historias entre 6 y 8, tareas técnicas entre 5 y 6. Los siete declaran excepciones admitidas y aprobador. Y los siete declaran explícitamente que **no son la Definition of Done**, que todavía no existe porque la categoría 08 no está emitida, con lo cual el solapamiento que la guía prohíbe no puede ocurrir.

**Vocabulario y sustitución léxica.** Cero menciones a stacks concretos, productos comerciales o protocolos del dominio fuente en los 208 documentos. Es notable porque es el criterio que la Fase C había violado en tres lugares: la Fase D no heredó el defecto.

---

## 8. Hallazgos

Ninguno P0. Ninguno P1.

### D-06-01 · P2 · El registro del repositorio afirma 232 historias donde hay 180

**Dónde está.** El mensaje del commit `818997f`, segundo párrafo: «Con esto los siete proyectos tienen backlog y plan: doscientas treinta y dos historias y ciento cuarenta y ocho tareas técnicas».

**Qué dice.** Doscientas treinta y dos historias.

**Qué debería decir.** **Ciento ochenta.** El reparto real es Domain 27, Contracts 22, Visor 14, Application 32, Web 30, Infrastructure 25, Api 30. La cifra de tareas técnicas del mismo párrafo, en cambio, es correcta: 148.

**Cómo lo verifiqué.** Conté los archivos de `06-Backlog-Tecnico/historias-usuario/` de los seis proyectos de código que los tienen —166— y las filas de historia del índice inline del Visor —14—. Contrasté ese recuento con las filas de la tabla §3 de cada `Product-Backlog.md`, que coinciden proyecto por proyecto, y con el identificador máximo de cada backlog, que coincide con la cantidad en los siete casos, de modo que no hay huecos ni identificadores repetidos. La primera ola sí cuadra: su mensaje declara «sesenta y tres historias y cincuenta y dos tareas técnicas» y son 27 + 22 + 14 = 63 y 16 + 18 + 18 = 52.

**Por qué P2 y no P3.** Porque ya se propagó: el encargo de esta auditoría llegó pidiendo verificar «unas 232 historias de usuario», es decir que la cifra falsa salió del repositorio y entró en un documento de control. Es exactamente el mecanismo que la Fase C tuvo que corregir dos veces: una afirmación numérica que nace en un lugar, no se recuenta, y se hereda. Y por qué no P1: **ningún documento emitido contiene la cifra**. Barrí los 208 buscando «232» y da cero. El defecto vive en el registro de git, no en el corpus, de modo que no hay documento que corregir ni cobertura que rehacer.

**Qué corresponde hacer.** No reescribir historia de git. Dejar constancia del recuento correcto —180— en el control de cambios de la próxima intervención sobre la categoría, o en el `README.md` de la sección 06 de cada proyecto de código, que hoy declara su propia cantidad y la declara bien.

### D-06-02 · P3 · Tres enumeraciones de cobertura inversa omiten tareas técnicas que su propia matriz les atribuye

**Dónde está.** El párrafo «Cobertura inversa» de `Backlog-Tecnico.md` §4 en tres proyectos de código:

- `GeometriaFactory-Domain`: «CU-08 en BT-08, BT-12 y BT-13». La fila `BT-14` de la misma matriz declara «CU-04, CU-08, CU-11, CU-12, CU-03», de modo que `BT-14` falta en la enumeración de `CU-08`.
- `GeometriaFactory-Visor`: «CU-01 en BT-04, BT-05, BT-06, BT-08, BT-14 y BT-16». La fila `BT-11` declara «CU-01, CU-07», de modo que `BT-11` falta en la enumeración de `CU-01`.
- `GeometriaFactory-Application`: «CU-01 en BT-08, BT-12 y BT-21» y «CU-04 en BT-07, BT-09, BT-10 y BT-15». La fila `BT-20` declara «CU-01, CU-03, CU-04, CU-05, CU-08, CU-10, CU-11», de modo que `BT-20` falta en las dos.

**Qué dice.** Enumeraciones que se leen como exhaustivas —en Domain y Visor lo son para todas las demás filas— y no lo son.

**Qué debería decir.** O bien completar las cuatro entradas, o bien declarar que la enumeración es una selección de las tareas sustantivas, que es lo que `GeometriaFactory-Web` hace de hecho al omitir las tareas de alcance general sin que eso lo vuelva falso.

**Cómo lo verifiqué.** Reconstruí, con herramienta y para los siete proyectos de código, el diccionario inverso `CU → {BT}` a partir de las filas de cada matriz, excluyendo las filas cuya columna de casos de uso declara un rango general del tipo «CU-01 a CU-13», y lo comparé entrada por entrada con la enumeración que el párrafo afirma. En cuatro proyectos de código la enumeración es igual o mayor —incluye las filas generales, lo cual es correcto—; en los tres citados es menor.

**Por qué P3.** Porque la afirmación que el párrafo hace es «los N casos de uso tienen **al menos una** tarea técnica que los realiza», y esa afirmación es verdadera en los siete proyectos de código: la comprobé para los 67 casos de uso. Lo que falla es la exhaustividad de una enumeración auxiliar, no la cobertura. Ninguna decisión depende de ello.

### D-06-03 · P3 · La distribución MoSCoW alcanza el mínimo de la guía por un ítem por proyecto de código

**Dónde está.** Los siete `Product-Backlog.md` §4. Seis de los siete proyectos de código tienen exactamente **una** historia no-`Must` sobre totales de 22 a 32; sólo el Visor tiene tres sobre catorce. El global es 174 `Must`, 5 `Should`, 2 `Could` sobre 180.

**Qué dice.** Los siete §4.2 fundamentan la distribución con el reparto del intake y con que el recorte de este producto es por etapa y no por prioridad.

**Qué debería decir.** Sustantivamente, lo mismo: **el fundamento es verdadero** y lo verifiqué (§4.3 de este informe). Lo que corresponde agregar es el reconocimiento de que la distribución no es el resultado de un ejercicio de priorización sino de la fidelidad a una fuente que ya priorizó, y que por lo tanto la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog** y la reemplaza íntegramente el orden de etapas.

**Cómo lo verifiqué.** Extraje el campo `Prioridad MoSCoW` de las 166 fichas y las filas de prioridad del índice inline del Visor; conté 18 `Must Have` sobre 26 capacidades en el intake §4, fila por fila; y crucé las 19 capacidades de las etapas `c` a `h` contra esa clasificación, encontrando que sólo `F-13` no es `Must Have`. Abrí las siete historias no-`Must` y verifiqué el origen declarado de cada una, incluida la afirmación de Infrastructure sobre el caso de uso sin necesidad de negocio, que su `02` §7.2 confirma.

**Por qué P3 y no P2.** Porque el criterio de aceptación de la guía —«no es 100 % Must»— se cumple, porque cada excepción está individualmente fundada y verificada, y porque el fundamento colectivo es verdadero. Es una reserva de lectura, no un defecto de construcción.

### D-06-04 · P3 · Las catorce historias del Visor viven inline en la banda donde la guía recomienda archivo propio

**Dónde está.** `GeometriaFactory-Visor/06-Backlog-Tecnico/`. No hay carpeta `historias-usuario/`; las catorce historias viven en `Product-Backlog.md` §3.2 y §3.3, con su estructura completa.

**Qué dice.** El backlog aplica el modo inline, admitido por la guía §3.3 para proyectos de código por debajo del umbral obligatorio de 20.

**Qué debería decir.** La tabla maestra de la guía §2.1 clasifica el tramo de 10 a 20 historias como **«Recomendado»** para archivo propio, no como indiferente. Con catorce, el Visor cae en ese tramo y elige la opción que la guía no recomienda, sin declarar por qué. Correspondía o bien el archivo propio, o bien una línea que declare la elección —los otros seis proyectos de código declaran su modo y su umbral explícitamente en el `README.md` de la sección—.

**Cómo lo verifiqué.** Conté los archivos de `historias-usuario/` de los siete proyectos de código y las filas del índice de historias de cada `Product-Backlog.md`; leí §2.1 y §3.3 de `Rules-Backlog-Tecnico.md` 3.1.

**Por qué P3.** Porque es la banda recomendada y no la obligatoria, porque el contenido no se pierde —las catorce historias tienen sus siete secciones, sus criterios en Given/When/Then y su trazabilidad, que es lo que §3.3 exige en los dos modos— y porque el Visor es el proyecto de código con menos historias del producto. Es forma, no sustancia.

---

## 9. Lo que no pude verificar

Lo declaro como no verificado en vez de darlo por bueno.

1. **La veracidad de cada una de las 148 filas de matriz contra el documento de arquitectura que cita.** Verifiqué que las 148 filas existen, que ninguna tiene celdas vacías, que sus identificadores de caso de uso caen en rango, y que la cobertura inversa que declaran es consistente con la propia matriz salvo en los tres casos del hallazgo `D-06-02`. **No abrí las 148 secciones de `05` §3.1, §8 y §11 que las filas citan** para comprobar una por una que el componente, la ADR o el punto abierto invocado dice lo que la fila supone. Muestreé una decena y las diez cerraron, pero no es una verificación exhaustiva.

2. **La veracidad de los criterios de aceptación de las 180 historias contra el enunciado de la regla de negocio que ejercen.** Verifiqué que las 16 reglas aparecen y que ninguna quedó sin trabajo asociado; no verifiqué que cada escenario Given/When/Then reproduzca fielmente la verificación que el intake §4.1 declara para esa regla.

3. **Si algún caso de uso quedó cubierto sólo nominalmente.** La cobertura se verificó por trazabilidad declarada. Que una tarea técnica *cite* un caso de uso no prueba que lo *realice*; para eso haría falta leer el criterio de aceptación de la tarea contra el flujo del caso de uso, y son 148 por 67 combinaciones posibles.

4. **La adecuación de las estimaciones.** No aplica: no hay ninguna. Es el punto 4.1 de este informe, no una limitación de la auditoría.

5. **El contenido de las tres fuentes originales —RF, RT, AN— que el intake cita.** Viven en otro repositorio bajo `PROMPTs/` y están fuera de alcance por la regla de la carpeta. Cuando un documento de la Fase D cita a RF o a RT lo hace **a través del intake**, y lo que verifiqué es que el intake dice lo que le atribuyen.

---

## 10. Dictamen

**APROBADO.**

**Los cuatro fundamentos.**

**Primero: la cobertura, que es lo propio de esta fase, está completa y verificada sobre el instrumento.** Los 67 casos de uso, las 16 reglas, los 9 invariantes, las 9 necesidades y los 8 escenarios tienen todos trabajo asociado. Las 148 tareas técnicas tienen las 148 filas y ninguna celda vacía. Las 180 historias declaran todas su caso de uso y ninguna cita un identificador inexistente. Y lo inverso también cierra: los siete mini-planes comprometen la totalidad del backlog de su proyecto de código, sin dejar un solo ítem fuera de tramo.

**Segundo: el backlog se subordinó al plan de etapas sin excepción.** Ocho etapas, ocho nombres tomados del roadmap, cero etapas nuevas, cero reordenamientos, y cada etapa no tocada declarada con motivo en dos lugares. El caso difícil —el momento previo a la etapa `g`— está resuelto en los dos proyectos de código que lo tienen, con las palabras correctas, con una sección dedicada en uno de ellos, y sin que ninguno le asignara letra. Un backlog que tenía un incentivo claro para inventarse una etapa `f-bis` y no lo hizo.

**Tercero: los tres apartamientos son decisiones, no omisiones, y sus tres fundamentos son verdaderos.** Los verifiqué abriendo el intake: la frase sobre el plazo está literal y dos veces; `equipo_n = 1` está literal y con su consecuencia nombrada por el propio intake; el 18 de 26 lo conté sobre la tabla. En los tres casos el corpus declara qué no hizo, por qué, quién lo cerraría y cuándo. El de la categoría 07, además, ni siquiera es un apartamiento: la guía manda emitir sólo el mini-plan cuando el equipo es de una persona, y emitir los cuatro artefactos de ceremonia habría sido la infracción.

**Cuarto, y es el que más pesa dado el antecedente: no encontré una sola afirmación falsa sobre otra fuente.** Muestreé las seis citas de más carga argumental y las seis resisten. El caso más difícil del corpus —citar una redacción que el intake retiró— está resuelto del único modo que no produce una falsedad. Los dos puntos abiertos que muestreé son verdaderos: sus fuentes efectivamente no resuelven lo que se declara abierto. Y los barridos mecánicos dan cero en las cinco familias de regresión, incluida la de stacks concretos, que la Fase C sí había violado y que esta fase no heredó.

**Lo que impide un rechazo.** No hay ningún P0 ni ningún P1. El único P2 vive en el mensaje de un commit y no en el corpus: ningún documento emitido contiene la cifra falsa, de modo que no hay documento que corregir, ni cobertura que rehacer, ni decisión que reabrir. Los tres P3 son una enumeración auxiliar incompleta, una lectura de la distribución MoSCoW y un umbral aplicado en su banda laxa; los tres se resuelven sin tocar una sola decisión y corresponde tomarlos en la próxima intervención sobre los archivos afectados, no en una tanda propia.

**Lo que eleva esta fase por encima del mínimo.** Dos cosas. La primera es que los siete backlogs se negaron a decidir por otros: cuando `GeometriaFactory-Web` y `GeometriaFactory-Visor` descubrieron —desde los dos lados de la fachada, y de manera independiente— que una historia `Should` es en la práctica no diferible porque una puerta técnica la mide, ninguno de los dos le subió la prioridad. La elevaron como tensión declarada para que la decida el Product Owner. Subirla habría sido lo cómodo y habría mejorado de paso la distribución MoSCoW, que es justamente el número que la guía mira con dureza. Se resistieron a mejorar su propia métrica a costa de usurpar una decisión ajena. La segunda es que los tres apartamientos están declarados **en subsecciones con título propio** —«Por qué la unidad de estimación queda abierta», «Por qué la distribución MoSCoW es la que es», «Por qué esta categoría emite un mini-plan y no planes de iteración»—, repetidas en los siete proyectos de código. Un apartamiento que se esconde se escribe en una nota al pie; éstos piden ser leídos.

---

## 11. ¿Alcanza este backlog para construir el producto?

**Sí, y con un margen que conviene nombrar con precisión, porque no es el margen habitual.**

Este backlog alcanza porque resuelve el problema que realmente tenía por delante, que no era priorizar sino **repartir**. Siete proyectos de código con un grafo de dependencias acíclico, ocho etapas verticales que atraviesan todas las capas, y un solo par de manos para construirlo: el riesgo real no era construir lo que no se pidió, sino que una etapa llegara a su punto de control bloqueante con una capa lista y otra sin empezar, o con dos capas que hubieran resuelto lo mismo dos veces. Contra ese riesgo, la fase entrega el instrumento correcto: cada uno de los 180 ítems de historia y 148 de tarea tiene una etapa asignada, y los siete mini-planes reagrupan el trabajo por etapa en lugar de por proyecto de código, de modo que la pregunta «qué falta para cerrar la `d`» tiene respuesta en siete documentos que se leen en paralelo y usan los mismos nombres de etapa. Eso es lo que hace ejecutable un producto de siete piezas con una sola persona.

**Lo que este backlog no da, y hay que saberlo antes de empezar, es una previsión de esfuerzo.** No hay puntos, no hay tallas, no hay velocidad, y por lo tanto no hay forma de responder «cuánto falta» en ninguna unidad que no sea «cuántas etapas quedan». Es una decisión declarada del Product Owner y está bien tomada —inventar números sin historial habría producido una falsa precisión que la categoría 07 habría consumido como capacidad—, pero tiene una consecuencia operativa concreta: **la única señal temprana de que una etapa está mal cortada será que el punto de control no llegue**, porque no habrá una estimación contra la cual el atraso se haga visible antes. Los siete backlogs anticiparon eso mejor de lo que la guía les exigía, al reemplazar la cadencia de refinamiento por sprint —que no existe— por una sesión de refinamiento **por etapa**, al abrir la rama y antes de escribir la primera línea. Esa sesión es, en este producto, el único lugar donde una etapa mal cortada se puede detectar a tiempo. Conviene que no se saltee.

**Y hay una segunda cosa que el backlog hace bien y que va a valer más adelante que ahora.** Cincuenta y cuatro puntos abiertos declarados, cada uno con dueño y con momento, y ninguno de ellos falso: los que muestreé están efectivamente sin resolver en su fuente. Un backlog que llega a la ejecución con cincuenta y cuatro incógnitas nombradas es mucho más seguro que uno que llega con cinco, porque las otras cuarenta y nueve no desaparecieron por no escribirlas —aparecen el día que alguien tiene que elegir un nombre de tipo, un umbral de fluidez o una vigencia de acceso, y si no estaban declaradas se resuelven por defecto y en silencio—. La mayoría de esas incógnitas están además **ancladas al punto de control de la etapa `a`**, que es donde corresponde: el andamiaje es exactamente el momento en que las decisiones de nombre y de herramienta son baratas.

**El corpus está en condiciones de seguir a las categorías 08 a 11 sin arrastrar deuda.** La categoría 08 recibe 180 conjuntos de criterios Given/When/Then ya escritos y una Definition of Ready que declara explícitamente dónde termina y dónde empezará la Definition of Done, de modo que el solapamiento que la guía prohíbe no puede ocurrir por accidente. Es la clase de trabajo que la categoría siguiente puede tomar sin volver atrás.

---

## 12. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Auditoría de ronda 1 de la Fase D —categorías 06 Backlog Técnico y 07 Plan de Sprint— sobre los **208** documentos nuevos de los commits `914cc43` y `818997f`, contra `Rules-Backlog-Tecnico.md` 3.1 y `Rules-Plan-Sprint.md` 3.1 del repositorio de origen, el intake 1.18, el roadmap y las categorías 02, 03 y 05 de los siete proyectos de código. Verifica la cobertura reconstruyendo con herramienta el diccionario inverso `CU → {BT}` desde las siete matrices en lugar de leer el párrafo que lo declara: los **67** casos de uso, las **16** reglas, los **9** invariantes, las **9** necesidades y los **8** escenarios tienen todos trabajo asociado, y ninguna historia cita un caso de uso fuera del rango de su categoría 02. Comprueba que los siete mini-planes comprometen la totalidad del backlog sin identificadores fantasma. Recuenta los siete conjuntos cerrados y los siete cierran —16 reglas, 9 invariantes, 8 escenarios, 9 necesidades, 15 códigos vivos sobre 18 emitidos, 15 puntos de acceso, 6 funciones de fachada—; corrige el recuento de casos de uso de `Web`, que son **diez** y no once, contra el dato del encargo. Audita los tres apartamientos conscientes y los declara los tres bien fundados, verificando sus fundamentos sobre el intake abierto. Muestrea las **seis** citas entrecomilladas de más peso y **las seis resultan verdaderas**, incluida la cita de una redacción retirada, que está bien encuadrada. Barre cinco familias de regresión sobre los 208 documentos: cero enlaces rotos, cero filas de tabla discordantes, cero identificadores de tres dígitos, cero identificadores fantasma, cero menciones a stacks concretos. Levanta **cuatro hallazgos: ninguno P0, ninguno P1, un P2 y tres P3**. El P2 es un recuento falso en el mensaje del commit `818997f` —232 historias donde hay **180**— que no aparece en ningún documento emitido pero que ya se propagó fuera del corpus. **Dictamen: APROBADO.** Ninguna decisión debe reabrirse. |
