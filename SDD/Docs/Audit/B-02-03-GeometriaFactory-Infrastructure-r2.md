# Informe de auditoría — Fase B · GeometriaFactory-Infrastructure · categorías 02 y 03 · ronda 2

**Producto:** Fábrica de Geometría
**Fase auditada:** B (02-Especificacion-Funcional y 03-UX-UI-DX)
**Proyecto de código:** GeometriaFactory-Infrastructure (`tipo_proyecto_codigo` = `library`, nivel 2 del orden topológico)
**Alcance de la ronda:** los **27 documentos** vivos de `SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/` tras el commit de corrección `ac8889c`, contrastados contra `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** —corregido en `1ef5997`—, `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2**, y los proyectos de código vecinos. Objeto de la ronda: dictaminar si se levanta el rechazo de `B-02-03-GeometriaFactory-Infrastructure-r1.md`
**Auditor:** Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la generación ni en la corrección
**Fecha:** 2026-08-10
**Ronda:** 2

**Categoría 04:** omitida por gating (`usa_llm` == false). Su ausencia no es hallazgo y no se evalúa.

**Criterio de la ronda.** Se auditó **el instrumento y no la conclusión**: ninguna afirmación de una fila de control de cambios se dio por buena. Cada cierre se comprobó abriendo el texto vivo del documento corregido y, cuando afirma algo sobre otra fuente, abriendo también esa fuente. El diff `d168363..ac8889c` se usó como mapa de dónde mirar, nunca como prueba de que algo quedó bien.

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los seis hallazgos de la ronda 1](#2-los-seis-hallazgos-de-la-ronda-1)
- [3. Recuentos propios sobre lo que la corrección tocó](#3-recuentos-propios-sobre-lo-que-la-corrección-tocó)
- [4. Regresiones](#4-regresiones)
- [5. Hallazgos nuevos](#5-hallazgos-nuevos)
- [6. Dictamen sobre el bump de versión sin archivado](#6-dictamen-sobre-el-bump-de-versión-sin-archivado)
- [7. Lo que se verificó y quedó bien](#7-lo-que-se-verificó-y-quedó-bien)
- [8. Veredicto](#8-veredicto)

---

## 1. Resumen ejecutivo

**Los seis hallazgos de la ronda 1 están cerrados, y cerrados en el texto y no en la promesa.** El P0 —la decisión del intake 1.12 sobre el desenlace de `E-8`— llegó a los nueve pasajes que la ronda 1 tabuló y a un décimo que la ronda 1 no había listado; los cinco que afirmaban en positivo que la fuente no prescribe el desenlace hoy afirman lo contrario, con el mismo contenido que el intake vivo lleva en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21. **No queda un solo pasaje declarándolo abierto fuera de las filas históricas de control de cambios**, comprobado por barrido de las 43 apariciones de `E-8` en el corpus.

Los cuatro recuentos que el encargo pidió recontar **cierran los cuatro**: quince puntos abiertos, nueve propios y seis heredados, contados fila por fila sobre la §11 vigente; diez casos en la batería del proyecto de código, con el décimo mapeado a `CU-01` **CA-12**, que existe, está completo y transcribe el payload del intake sin inventar nada; **25 de 25** cabeceras vivas que citaban intake pasaron a 1.12 y **ninguna cita histórica mutó**; 26 de 27 documentos en 1.1 con fila de control de cambios, y el 27º —`Glosario-UX.md`— legítimamente en 1.0, porque no cita versión de intake y ninguno de los seis hallazgos lo tocaba.

La corrección hizo además dos cosas que merecen quedar escritas. **Corrigió un error del informe de la ronda 1**: donde r1 dijo que las «otras nueve filas» de §21 ubican en `f` sus casos, son **ocho**, porque la fila «Dimensión en `0`» no nombra etapa y remite a la batería de RT §11. Abrí §21: es exacto. Y **corrigió un recuento propio que la auditoría no había señalado**: el encabezado de la tabla de criterios de `CU-01` decía «los seis primeros son escenarios del intake» cuando son siete.

Se emiten **tres hallazgos nuevos, los tres P3**, todos de la misma familia y todos de arrastre de la propia corrección: dos recuentos que la corrección desalineó sin actualizar, y la declaración de `_legacy/` que el bump de versión dejó falsa. Ninguno bloquea.

**Veredicto: APROBADO**, con los tres P3 a absorber en la próxima intervención sobre estos documentos.

---

## 2. Los seis hallazgos de la ronda 1

| Hallazgo | Sev. r1 | Estado verificado | Cómo lo comprobé |
| --- | --- | --- | --- |
| **H-01** — la decisión del intake 1.12 sobre el desenlace de `E-8` no llegó, y cinco pasajes afirman lo contrario de la fuente | P0 | **CERRADO** | Barrido de `E-8` sobre los 27 documentos: **43 apariciones**, filtradas por «punto abierto», «ninguna fuente», «no prescribe», «nada declarado», «no elige», «no hay resultado». **Cero pasajes lo declaran abierto**; los únicos aciertos del filtro son texto que declara el resultado y filas de control de cambios. Verifiqué los cinco pasajes positivos uno por uno: `Especificacion-Funcional.md` §11 hoy encabeza «El que era el primero de esta lista ya no está abierto» y declara error / `Borrador` / mensaje por índice y campo; `Definicion-Contrato…` §6 fila `E-8` lleva resultado esperado —1 observación de especie error de validación con **posición 1** y campo **`Largo`**, ortoedro de la posición 0 reconstruido, posición 1 reservada, trabajo en `Borrador` por RN-05—; §7 precisión 1 y §9 rehechos; `CU-01` §10 declara que el desenlace **sí** es de este contrato. Contra la fuente: abrí `SDD/Intake/…-1.12` §20.E-8 —los puntos hoy van de 1 a 6, sin el doble «5» que r1 anotó, y el punto 5 es el desenlace— y §21 fila «Dimensión no legible». **El contenido transcripto coincide con la fuente.** El décimo pasaje —`Definicion-Contrato…` §8, la frontera con el visor— no estaba en la tabla de r1 y también se corrigió, separando la **condición** `DIMENSION_NO_LEGIBLE`, que es de la fachada, del **desenlace del envío**, que es de esta capa |
| **H-02** — los 27 documentos citan el `PRODUCT-INTAKE` 1.11, archivado | P1 | **CERRADO** | Extracción de las líneas «Trazabilidad upstream» de los 27 archivos: **25 citan 1.12**, y las dos que no citan versión de intake son las mismas dos que r1 identificó (`02/README.md`, que traza a documentos hermanos, y `03/Glosario-UX.md`). **Ninguna cita 1.11.** Las únicas dos apariciones vivas de «1.11» son la **fila 1.0** del control de cambios de `Especificacion-Funcional.md` —histórica, y debe conservarse— y las filas 1.1 que narran el cambio de 1.11 a 1.12. `git diff d168363..ac8889c` no borra **ninguna** línea de versión 1.0 del control de cambios: **cero citas históricas mutadas** |
| **H-03** — la caracterización de la décima fila de §21 es falsa contra la fuente vigente | P1 | **CERRADO** | Leí la precisión 1 de `Definicion-Contrato…` §7 en su texto vigente: hoy dice «la décima **sí tiene tramo en este proyecto de código**», transcribe la fila de §21 con sus etapas `f` y `g`, y concluye que el caso se verifica acá **y también** en la visualización. Contrastado contra §21 del intake, tabla completa: la fila existe y dice eso. La afirmación de apoyo —«**ocho de las otras nueve filas** ubican en `f` sus casos; la novena, "dimensión en `0`", no nombra etapa»— la conté sobre la tabla: ocho filas con «Etapa `f`», y la de `Dimensión en 0` dice «Batería de RT §11». **Exacta**, y más exacta que la de r1 |
| **H-04** — el conjunto de «diez puntos abiertos» no cierra, y `CU-02` afirma falsamente estar en la lista | P2 | **CERRADO** | Conté las filas de la §11 vigente: **quince**, nueve con la marca «Propio» y seis sin ella (derivación de clave, identificador del puerto de cuentas, nombres de tipos y espacios de nombres, criterio de comparación de correos, frecuencia del respaldo, valores numéricos de los RNF). Los seis que r1 listó como faltantes están los seis; el de `E-8` **salió**. Verifiqué la remisión de `CU-02` §6 abriendo los dos extremos: hoy dice «queda registrada como punto abierto propio de la categoría en `Especificacion-Funcional.md` §11», y §11 **la contiene**. La cifra se replica coherente en `02/README.md` («Quince … nueve propios … seis») y en `03/README.md` («Los **quince** que §11 declara: **nueve propios**») |
| **H-05** — `CU-04` atribuye a `CU-07` la conservación de la cuenta y sus trabajos | P3 | **CERRADO** | Leí la primera viñeta de `CU-04` §10 en su texto vigente: la atribución pasó a **RN-12**, con el tramo asignado a `CU-05` y a este `CU-04` por contraste, exactamente como la fila RN-12 de `Especificacion-Funcional.md` §6 lo declara |
| **H-06** — se cita §14 «(RA-01 a RA-03)» y sólo se ejerce RA-03, sin declarar no aplicabilidad | P3 | **CERRADO** | `Especificacion-Funcional.md` §4 pasó de «Cuatro precisiones» a «**Cinco**», y la quinta declara **RA-01 y RA-02 no aplicables** con su motivo —sin superficie de navegador, no atiende peticiones, no compone el bundle— y deja RA-03 como la única con tramo acá. Contrasté los enunciados contra §14 del intake (líneas 505-506): coinciden. **Ver N-01**: el bump de cuatro a cinco dejó una cita desactualizada en un documento hermano |

**Ninguno de los seis reaparece bajo otra forma, y ninguno se cerró declarándolo cerrado.**

---

## 3. Recuentos propios sobre lo que la corrección tocó

Contados sobre los archivos, sin usar ninguna cifra declarada.

### 3.1 El desenlace de `E-8`

`grep` de `E-8` sobre los 27 documentos: **43 apariciones**. Clasificadas a mano: las que lo declaran **error** con el trabajo en `Borrador` son las de `Especificacion-Funcional.md` §11, `Definicion-Contrato…` §6, §7, §8 y §9, `CU-01` CA-12 y §10, `02/README.md`, `03/README.md`, `Guia-Onboarding-Developer.md` y `DX-Error-Messages.md` §5; el resto son citas de escenario, fixtures y filas de control de cambios. **Cero pasajes lo dan por abierto.** Las filas históricas `| 1.0 |` conservan su redacción original, que es lo correcto.

Verifiqué además que el contenido declarado sea el del intake y no una paráfrasis suelta: **posición 1** y campo **`Largo`** coinciden con §20.E-8 punto 2 («la reporta con índice 1 … nombrando el campo `Largo`»); el desenlace coincide con el punto 5; la distinción condición/desenlace coincide con el punto 4.

### 3.2 Puntos abiertos: quince

Filas de la tabla de `Especificacion-Funcional.md` §11, en orden: tipos reconstruibles · «no se repite» · longitud y alfabeto de la provisoria · vigencia del acceso firmado · valor derivado del área volumétrica · límite de tamaño del texto · zona horaria y precisión de los sellos · fecha de última modificación de la cuenta · `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` — **nueve marcados «Propio»** — y derivación de clave · puerto de cuentas · nombres de tipos · comparación de correos · frecuencia del respaldo · valores de los RNF — **seis heredados**. **Quince. Cierra**, y la aritmética contra r1 también: de los cinco propios anteriores salió `E-8` y entraron cinco de los seis que faltaban; el sexto, la frecuencia del respaldo, quedó clasificado como heredado con el argumento correcto —el intake la declara «a definir por el docente»—.

Ninguno de los quince es hallazgo por estar abierto: los quince declaran motivo y destinatario.

### 3.3 La batería: diez casos

La tabla de `Definicion-Contrato…` §7 tiene **diez filas numeradas 1 a 10**, cada una con escenario, caso de uso y criterio. La décima es «Dimensión no legible · E-8 · CU-01 · **CA-12**». Abrí `CU-01` §8: **CA-12 existe**, con Given/When/Then completos, el payload de `"Largo": "3,50"` y `"Ancho": "3,50"` como cadenas, cantidad 2, 1 pieza reconstruida, 1 observación de especie error de validación con posición 1 y campo `Largo`, posición reservada, código **no** `JSON_INVALIDO`, y el trabajo en `Borrador` por RN-05, citando `PRODUCT-INTAKE` 1.12 §20.E-8 punto 5 y §21. **Está completo y trazado.**

Los recuentos satélite de la corrección también cierran: `CU-01` §9 pasó a «E-1 … y E-8» y sus fixtures a «E-1 a E-8»; la precisión 3 de §7 pasó de cinco a **seis** casos de estructura, y lo verifiqué sobre la tabla —CU-02 aparece en las filas 5, 6, 7 y 9, cuatro; CU-01 en las filas 1, 2, 3, 4, 7, 8, 9 y 10, de las cuales seis son sólo suyas y dos, la 7 y la 9, tocan a los dos: 4 + 6 = 10—. El «los ocho casos de la batería que corresponden a la interpretación» de `CU-01` §9, que **antes no cerraba** —eran siete filas—, hoy cierra en ocho.

### 3.4 Citas al intake: 1.12 en las cabeceras vivas, 1.11 en las históricas

Ya tabulado en H-02. **25 cabeceras vivas en 1.12, cero en 1.11, cero filas históricas alteradas.** El manifiesto sigue citado en **1.2**, que es el vigente.

### 3.5 Versiones: 26 de 27 en 1.1

Cabeceras: **26 documentos en 1.1** y uno en 1.0, `03-UX-UI-DX/Glosario-UX.md`. Los 26 tienen **fila `| 1.1 |`** en su control de cambios, y los 27 conservan su fila `| 1.0 |`. La exclusión de `Glosario-UX.md` es correcta y la verifiqué en vez de suponerla: no cita versión de intake, y no contiene ninguna aparición de `E-8`, de «punto abierto» ni del recuento de la batería; sus dos menciones de «batería» son genéricas y su trazabilidad a `Especificacion-Funcional.md` §11 sigue resolviendo.

---

## 4. Regresiones

| Comprobación | Resultado |
| --- | --- |
| **Filas de tabla con más celdas que columnas** | **0**, sobre **158 tablas** de los 27 documentos, con escapes `\|` contabilizados. La ronda 1 verificó cero y **sigue en cero** |
| **Identificadores fantasma** | Barrido de `RN-XX`, `RC-XX`, `NB-XX`, `INV-XX`, `RA-XX`, `E-X`, `CU-XX`, `US-XX` y `CA-XX`. Todo lo citado existe: RN-01 a RN-15, RC-01 a RC-07, NB-00001 a NB-00009, RA-01 a RA-03, E-1 a E-8, CU-01 a CU-10 locales más `Application` CU-11, US-01 a US-25, y **CA-01 a CA-12 en `CU-01`, con CA-12 recién creada y referenciada desde seis documentos, los seis resolviendo**. `INV-01`, `INV-05`, `INV-08` e `INV-09` aparecen en el corpus —r1 dijo «sólo INV-09», que era inexacto: las tres primeras están en la trazabilidad de `CU-05` y ya estaban antes del commit—; **los cuatro existen** en §17.1.P.2 del intake, que declara nueve. **Cero fantasmas** |
| **Recuentos que no cierran** | Todos los recontados en §3 cierran. **Dos quedaron desalineados por la corrección** → N-01 y N-02 |
| **Afirmaciones falsas sobre otras fuentes** | Se reverificaron abriendo la fuente las cuatro afirmaciones nuevas de más peso: el contenido de §20.E-8 punto 4 y punto 5; la fila de §21 con sus etapas; el conteo «ocho de las otras nueve filas»; los enunciados de RA-01 y RA-02 en §14. **Las cuatro exactas.** Se encontró **una** afirmación desactualizada sobre un documento hermano → N-01 |

---

## 5. Hallazgos nuevos

### N-01 · **P3** · `DX-Error-Messages.md` sigue diciendo que la §4 del índice maestro tiene «cuatro precisiones», y desde esta corrección tiene cinco

**Dónde está.** `03-UX-UI-DX/DX-Error-Messages.md`, línea de trazabilidad upstream: «`Especificacion-Funcional.md` §3, §4 (**la frontera entre mecanismo y decisión** y sus **cuatro precisiones**)».

**Qué debería decir.** Cinco. El cierre de **H-06** agregó a §4 la quinta precisión, la de no aplicabilidad de RA-01 y RA-02, y el encabezado de esa sección pasó explícitamente de «Cuatro precisiones» a «Cinco precisiones». `DX-Error-Messages.md` **fue tocado y subido a 1.1 en el mismo commit** —para cerrar H-01, H-04 y H-02— y la cita quedó sin actualizar.

**Por qué se reporta y por qué es P3.** Es exactamente el defecto de fondo de este producto en miniatura: una afirmación numérica sobre otro documento que la fuente ya no sostiene. No induce a ningún error de contenido —quien siga la remisión encuentra §4 y la lee entera— y por eso no pasa de P3.

**Cómo lo verifiqué.** Barrido de «precisiones» sobre los 27 documentos: las apariciones que declaran una cifra son dos de `DX-Developer-Experience.md` («Tres precisiones que la tabla no alcanza a decir sola», «Cinco precisiones que el catálogo hace cumplir»), `Definicion-Contrato…` §7 («Tres precisiones sobre la matriz»), `Especificacion-Funcional.md` §4 («**Cinco**») y esta cita. Contraste directo con el encabezado vigente de §4.

---

### N-02 · **P3** · La métrica de cobertura de la batería y la previsión de tests de `DX-Developer-Experience.md` siguen en nueve, después de que el documento de concepto central decidiera diez

**Dónde está.** `03-UX-UI-DX/DX-Developer-Experience.md`, tabla de métricas: «**Cobertura de la batería obligatoria** \| Casos de prueba del producto con criterio de aceptación en esta categoría \| **9 de 9** \| **La tabla de §7 del documento de concepto central**»; y la fila de trazabilidad «Tests previstos en 08 \| **Las nueve pruebas** de la batería obligatoria con los textos de los escenarios como fixtures».

**Qué debería decir.** El cierre de H-03 decidió y declaró que **la batería de este proyecto de código pasa a tener diez casos**, y la tabla de §7 a la que la métrica remite como método de verificación **tiene hoy diez filas, todas con criterio**. La métrica que apunta a esa tabla debería medir diez, y la previsión para `08-Calidad-Y-Pruebas` debería anunciar diez pruebas, con CA-12 entre ellas.

**Por qué es P3 y no P2.** Es **literalmente defendible**: la batería obligatoria *del producto* sigue siendo de nueve casos —RT §11, y el propio intake lo dice al encabezar §21—, y las dos celdas hablan de «casos de prueba **del producto**» y de «la batería obligatoria». La distinción entre los nueve del producto y los diez de este proyecto de código está bien hecha en `Definicion-Contrato…` §7. Pero `DX-Developer-Experience.md` fue subido a 1.1 en este mismo commit y su previsión de tests es lo que `08` va a leer para dimensionar el trabajo; dejarla en nueve le esconde CA-12. No hay contradicción formal, hay desalineación.

**Cómo lo verifiqué.** Barrido de «nueve casos», «diez casos» y «batería» sobre los 27 documentos; lectura de las dos celdas en su texto vigente; recuento de las filas de la tabla de §7 —diez— y del encabezado de §21 del intake —«la batería obligatoria de nueve casos de prueba de RT §11»—.

---

### N-03 · **P3** · `Especificacion-Funcional.md` §9 sigue declarando que no hay ninguna versión superada que archivar, y el bump a 1.1 creó una

**Dónde está.** `02-Especificacion-Funcional/Especificacion-Funcional.md` §9, tabla de omisiones declaradas: «`_legacy/` \| **No existe** \| Es la emisión inicial de la categoría para este proyecto de código y **no hay ninguna versión superada que archivar**».

**Qué dice hoy la realidad.** 26 de los 27 documentos están en **1.1**, con lo cual sus 1.0 **son versiones superadas**. No existe carpeta `_legacy/` en ningún punto del árbol de este proyecto de código: `find` devuelve **cero** directorios `_legacy` y **cero** archivos con sufijo de versión. Los cinco proyectos de código hermanos **sí** archivan: `GeometriaFactory-Contracts/02-Especificacion-Funcional/_legacy/2026-08-09/` contiene `Especificacion-Funcional-v1.0.md` y `-v1.1.md`, y `GeometriaFactory-Domain` declara en su propia fila 1.1 «**Sube minor y archiva el estado anterior**».

**Por qué es P3.** No hay pérdida de información: el 1.0 vive en `d168363` y la traza del cambio está en las filas 1.1. El defecto es que una **declaración de estado propio** quedó falsa por efecto del bump. Se anuda con §6 de este informe y su corrección natural es la misma decisión: o el bump se revierte —y la declaración vuelve a ser cierta—, o el bump se sostiene y entonces la §9 debe decir que `_legacy/` está pendiente de creación o que el archivado se resuelve por historial de git, declarándolo.

---

## 6. Dictamen sobre el bump de versión sin archivado

**Lo que no pude verificar.** `Master-Prompt.md` **no está en este repositorio** —`find` sobre todo el árbol no devuelve ningún archivo con ese nombre, ni ningún `Rules-*.md` que transcriba la política—. Por lo tanto **declaro no verificado el texto de §5**, incluida su condición sobre los estados `Borrador` y `Propuesto` y sobre el disparador del bump —haber sido citado como insumo por otro artefacto—. No la supongo y no la reconstruyo. Lo que sigue dictamina **sobre el enunciado tal como el encargo lo formula**, y aporta la evidencia interna del repositorio, que sí pude leer.

**Los hechos comprobados.** Los 27 documentos nacieron en **1.0** con estado **`Propuesto`** —los 27 lo siguen declarando: `grep` sobre las cabeceras devuelve 27 de 27 `Propuesto`, y el bump a 1.1 **no cambió ningún estado**—. La corrección subió **26** a **1.1** con fila de control de cambios que nombra el informe de auditoría de su propia fase de emisión, y **no archivó** ningún 1.0.

**Dictamen.** Tomando el enunciado del encargo como fiel, **sí es un apartamiento de la política, y es de forma, no de contenido: gravedad P3.** Los tres atenuantes son reales y verificados: (a) **no hubo pérdida de información** —los 1.0 son recuperables en `d168363`, lo comprobé—; (b) **la traza quedó explícita** —las 26 filas 1.1 nombran el informe, el hallazgo y qué cambió, con un nivel de detalle superior al que un archivado daría por sí solo—; y (c) **el estado `Propuesto` se conservó**, de modo que el bump no simuló una promoción que no ocurrió.

Hay además un **antecedente interno que lo atenúa y que conviene registrar**, sin que valga como autorización: este repositorio ya venía bumpeando por corrección de auditoría de la propia fase de emisión. `GeometriaFactory-Domain` subió a **1.2** «Corrección del P0 reportado por `B-02-03-GeometriaFactory-Application-r1.md`» y a **1.3** «Correcciones de la ronda r3 del audit». Si esa práctica es un apartamiento, es un apartamiento **del producto entero** y no de esta tanda, y auditarlo excede el alcance de esta ronda; si no lo es, esta tanda hizo lo mismo que sus hermanas.

**Lo que sí es privativo de esta tanda, y es lo que sostiene N-03.** Los hermanos que bumpearon **archivaron**; éste bumpeó y **no archivó**. Tomó una mitad de la operación y no la otra, y dejó en §9 una declaración —«no hay ninguna versión superada que archivar»— que su propio bump falsifica. **La incoherencia interna es más reprochable que la elección de bumpear**, porque es verificable sin la política y no depende de cómo se lea §5.

**Recomendación, no condición.** Que el orquestador resuelva en un sentido u otro —revertir los 26 bumps a 1.0 absorbiendo la corrección en la emisión, o sostenerlos y archivar los 1.0 en `_legacy/2026-08-10/`— y que, en cualquiera de los dos casos, la §9 quede diciendo la verdad sobre el estado del proyecto de código. **No se hace de esto una condición para promover**, porque no afecta a ninguna decisión de contenido y porque la política que lo gobierna no es legible desde este repositorio.

---

## 7. Lo que se verificó y quedó bien

| Comprobación | Resultado |
| --- | --- |
| Los seis hallazgos de r1 | **Cerrados los seis**, verificados en el texto y contra las fuentes |
| Pasajes que declaran `E-8` abierto fuera del control de cambios | **0**, sobre 43 apariciones |
| Puntos abiertos en §11 | **15 = 9 + 6**, recontados fila por fila; replicados coherentes en los dos README |
| Batería del proyecto de código | **10 casos**, todos con criterio; CA-12 existe, completa y trazada al intake |
| Citas al intake en cabeceras vivas | **25 en 1.12, 0 en 1.11**; **0 citas históricas mutadas** |
| Versiones | **26 en 1.1 con fila de control de cambios**, 27 conservan su fila 1.0; el 27º documento correctamente excluido |
| Filas de tabla malformadas | **0** sobre **158 tablas** |
| Identificadores fantasma | **0**, con CA-12 resolviendo desde sus seis referencias |
| Afirmaciones nuevas sobre otras fuentes | Cuatro muestreadas y abiertas en la fuente: **las cuatro exactas** |
| Recuento de `CU-01` §9 «ocho casos de interpretación» | **Pasó a cerrar**: antes eran siete filas y hoy son ocho |
| Dos correcciones no pedidas | Legítimas y verificadas: «ocho de las otras nueve filas» de §21 y «los siete primeros» de la tabla de criterios de `CU-01` |
| Lo que r1 declaró bien y podía degradarse | Muestreado y **no degradado**: los diez casos de uso, las 17 condiciones y sus recuentos en `DX-Error-Messages.md` §7, las quince reglas con trece tramos, los siete `RC-XX`, las 25 historias, los 17 términos acuñados, y la cobertura de RN-14, RN-08 y RN-07. El commit no toca esas secciones y sus cifras son las mismas |

**Sobre el criterio negativo del encargo.** No se reporta ninguna polisemia. Las cuatro de `Glosario-Funcional.md` §3 —«validador», «repositorio», «derivado» y `Pendiente`— siguen tratadas, y las tres que §3.5 deja deliberadamente sin corregir tienen contextos disjuntos. Tampoco se reporta ningún punto abierto por estar abierto: los quince están correctamente declarados, y quince es el número legítimo de este proyecto de código.

---

## 8. Veredicto

# APROBADO

**Fundamento.** El rechazo se levanta. El P0 estaba en el punto exacto en que esta capa existe para decidir —qué hace el validador con el texto que la configuración regional produce— y **está resuelto en el texto, no en la afirmación de que se resolvió**: los diez pasajes afectados declaran hoy lo mismo que declara el intake vigente, con posición, campo, estado del trabajo y fundamento, y con un criterio de aceptación ejecutable que lo verifica. Los dos P1, el P2 y los dos P3 están cerrados con la misma solidez, comprobados abriendo las fuentes y no leyendo las filas de control de cambios. Los cuatro recuentos que la corrección movió cierran los cuatro en recuento independiente, y las dos trampas clásicas de una corrección —mutar la historia y romper las tablas— no se activaron: cero citas históricas alteradas y cero filas malformadas sobre 158 tablas.

Quedan **tres P3**, los tres de arrastre de la propia corrección y ninguno bloqueante: dos cifras que la corrección desalineó en documentos que ella misma tocó —las «cuatro precisiones» y el «9 de 9»— y la declaración de `_legacy/` que el bump de versión dejó falsa. Se absorben en la próxima intervención sobre estos documentos, junto con la decisión del orquestador sobre el bump, y **no son condición para promover**.

**Condición única para la promoción formal:** ninguna. La Fase B de `GeometriaFactory-Infrastructure` está en condiciones de ser citada como insumo por `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas`.

---

## Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del informe. Auditoría de ronda 2 de la Fase B de `GeometriaFactory-Infrastructure`, categorías 02 y 03, sobre los 27 documentos tras el commit de corrección `ac8889c`, contra el `PRODUCT-INTAKE` 1.12 corregido en `1ef5997` y el `PRODUCT-MANIFEST` 1.2. **Los seis hallazgos de la ronda 1 se verifican cerrados** —el P0 de `E-8` en los diez pasajes afectados, con CA-12 creada y trazada; los dos P1, el P2 y los dos P3 comprobados en el texto y contra las fuentes—. Recuentos propios: cero pasajes declaran `E-8` abierto sobre 43 apariciones; quince puntos abiertos, nueve propios y seis heredados; diez casos en la batería del proyecto de código; 25 cabeceras vivas en 1.12 y cero citas históricas mutadas; 26 de 27 documentos en 1.1 con fila de control de cambios. Cero filas de tabla malformadas sobre 158 tablas y cero identificadores fantasma. **Tres hallazgos nuevos, los tres P3**: la cita a «cuatro precisiones» de §4 que hoy son cinco, la métrica «9 de 9» de la batería frente a la tabla de diez, y la declaración de `_legacy/` que el bump falsifica. **Dictamen sobre el bump de 26 documentos a 1.1 sin archivar el 1.0**: apartamiento de forma de gravedad **P3**, con la política de `Master-Prompt.md` §5 declarada **no verificada** por no estar el archivo en este repositorio; lo privativo de esta tanda no es bumpear —los proyectos de código hermanos también bumpearon por auditoría de su propia fase— sino bumpear sin archivar y dejar §9 afirmando que no hay versión superada. **Veredicto: APROBADO**, sin condiciones para promover. |
