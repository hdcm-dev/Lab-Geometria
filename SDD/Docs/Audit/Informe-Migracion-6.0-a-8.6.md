# Informe de migración — SDD 6.0 a 8.6

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-6.0-a-8.6.md
**Versión:** 6.0
**Fase:** M6 — Auditoría de migración, **ronda 6** (`Master-Prompt-Migracion.md` 2.0 §10)
**Alcance:** el árbol `SDD/` del destino `Lab-Geometria`, en la rama de migración
**Auditor:** Arquitecto de Soluciones + QA Senior, con la mecánica de `Master-Prompt.md` §10
**Fecha:** 2026-08-16
**Criterios:** D1 a D9; `Root-Rules.md` §9 a §12; los **veintiséis** criterios de aceptación de `Migracion-Rules.md` §6; los hallazgos P0 propios de `Master-Prompt-Migracion.md` §10

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Estado final de cada fase del plan](#2-estado-final-de-cada-fase-del-plan)
- [3. Compuerta mecánica](#3-compuerta-mecánica)
- [4. Criterios de aceptación de `Migracion-Rules.md` §6](#4-criterios-de-aceptación-de-migracion-rulesmd-6)
- [5. Hallazgos](#5-hallazgos)
- [6. Contenido sin destino](#6-contenido-sin-destino)
- [7. Declaración de migración completa o parcial](#7-declaración-de-migración-completa-o-parcial)
- [8. Veredicto](#8-veredicto)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Resumen ejecutivo

**La migración está cerrada.** Las siete fases corrieron: la ronda 1 de esta auditoría encontró **M2 y
M3 sin ejecutar** y por eso declaró la migración **parcial** y dejó la procedencia intacta. Las dos se
ejecutaron después —M2 con su doble detención resuelta y M3 con confirmación explícita del Product
Owner el 2026-08-16—, y con la cadena completa **M5 reescribió la procedencia a 8.6**.

Hallazgos de esta ronda: **0 P0**, **0 P1**, **8 P2**. El veredicto es **APROBADO** y la migración se
declara **COMPLETA Y CERRADA EN SUS DOS EJES**:

| Eje | Estado |
| --- | --- |
| **Las siete fases de la migración** | **Completas.** M0 a M6 corrieron, la procedencia es verificable y los tres artefactos de la cadena están migrados |
| **La consolidación de la fusión** | **Cerrada.** 67 de 67 grupos, **18 de 18 carpetas `_fusion/` retiradas**, 9726 líneas absorbidas con 0 sin correspondencia |

**Las rondas 2, 3 y 4 declararon la migración «COMPLETA» sin separar los dos ejes, y eso fue un
error de esta auditoría.** Peor: la ronda 2 contó los 146 documentos de `_fusion/` en la compuerta
mecánica **como evidencia a favor** —«ninguno se sobrescribió»—, cuando `Migracion-Rules.md` §4.3.2
dice de esa carpeta que **«su presencia declara que la fusión no terminó»**. Es cierto que ninguno se
sobrescribió; no es cierto que eso significara que estaba terminado. El hallazgo es **M-10**.

**Los tres P1 de la ronda 2 quedaron cerrados el 2026-08-16**, cada uno por la salida que le
correspondía y no por la misma: **M-03** con un ADR de apartamiento, **M-08** con una supersesión
declarada y un registro de citas ambiguas, y **M-09** partido en dos —lo determinable se ejecutó, lo
que no lo era se declaró—. Ninguno se cerró afirmando que el problema no existía.

**Qué cambió respecto de la ronda 1.** M-04 —el orden de las fases— queda **cerrado**: M2 y M3 se
ejecutaron, aunque después de M4. M-01 queda **reemplazado por M-08 y M-09**, que lo dicen con
precisión: la ronda 1 atribuyó los residuos de forma vieja a que el intake no estaba migrado, y con el
intake ya migrado se ve que son **dos cosas distintas**, una de ellas ajena al intake. M-03 y M-07
siguen como estaban. Los recuentos de la compuerta se rehicieron sobre el árbol final.

## 2. Estado final de cada fase del plan

| Fase | Qué exige | Estado | Evidencia |
| --- | --- | --- | --- |
| **M0** Reconocimiento | Inventario del destino y su procedencia declarada | **Hecha** | `Migracion-8.1-Arbol-De-Migracion.md` §2, con la clasificación confirmada por el humano |
| **M1** Diff normativo | Qué cambió de 6.0 a la vigente | **Hecha** | `Migracion-8.1-Arbol-De-Migracion.md` §1 |
| **M2** Intake | Re-expresar el intake bajo la plantilla vigente, con doble detención y bump **major** | **Hecha** | Propuesta y diff en `Migracion-M2-Propuesta-Intake.md`; **las dos aprobaciones dadas el 2026-08-16**; intake **1.34 → 2.0** sobre plantilla 3.0, archivado previo en `SDD/Intake/_legacy/2026-08-16/` |
| **M3** Manifiesto | Re-derivar el manifiesto del intake migrado | **Hecha** | Manifiesto **1.4 → 2.0** sobre plantilla 5.0, emitido en estado `Propuesto` y **confirmado por el Product Owner** |
| **M4** `SDD/Docs/` | Renumeración, reestructuración, reconexión y consolidación | **Hecha** | §3 de este informe |
| **M5** Cierre de procedencia | Reescribir la procedencia **sólo** si la cadena está completa | **Hecha** | Verificación con la cadena completa; procedencia reescrita a **8.6** en el manifiesto **2.1**, con las dos filas nuevas que el salto agrega |
| **M6** Auditoría | Este informe | **Hecha, ronda 2** | Este documento |

### 2.1 Las filas abiertas del árbol de migración

`Migracion-8.1-Arbol-De-Migracion.md` §8 dejó tres decisiones que la migración no podía tomar. Su
estado final:

| Fila | Estado | Cómo se resolvió |
| --- | --- | --- |
| §7.1 — **57 citas desnudas ambiguas** | **Resuelta** | Reconectadas desde los dos registros confirmados, `Migracion-8.1-Registro-Reconexion.json` (703 entradas) y `-Contracts.json` (141) |
| §7.2 — **la familia calificada `P·CU`, 166 ocurrencias** | **Resuelta** | Retirada, con trazabilidad real de necesidades en su lugar |
| §7.3 — **el contenido sin destino de `Contracts`** | **Resuelta** | Distribuido: los contratos a `Producto/Contratos-Inter-Unidad/`, el resto archivado con README que declara el motivo de cada categoría |

**Ninguna fila del plan quedó sin resolver y sin declararse**, que es uno de los seis P0 propios de
esta fase.

### 2.2 La batería de M2

| Pregunta | Resolución | Dónde queda |
| --- | --- | --- |
| **B-1** Estado de las unidades de entrega | Las dos **`vigente`** | Intake §13.1 y manifiesto §2.A |
| **B-2** NFR de los proyectos que no se despliegan | **Se conservan nombrando su proyecto**, con apartamiento declarado contra la afirmación de la plantilla | Intake §17.1.P.10 y §17.2.P.10 |
| **B-3** Renumeración de los pre-ADR | **Renumerar y reconectar desde registro** | `Migracion-M2-Registro-Citas-17.json`, **2208 citas** |


## 3. Compuerta mecánica

Corrida sobre los **437 documentos vivos** del árbol —excluidos `_legacy/` y `_fusion/`, que por
definición conservan la forma anterior—.

| Verificación | Resultado | Lectura |
| --- | --- | --- |
| **Enlaces que resuelven** | **2 rotos** | Los dos en `Audit/E-08-Calidad-Siete-Proyectos-r2.md`, por **nombre ambiguo**. Ver M-06 |
| **Anclaje de referencias (R5)** | **1145 de 1145** | **100 %** |
| **Colisión de identificadores** | **0** | Ámbito producto, ancho cinco, sin choques |
| **Citas `§17.<n>.P.<m>` sin anclar a su proyecto** | **0** | 2208 reconectadas; las 24 restantes del intake son **sus propios encabezados de sección**, y las 2 del manifiesto llevan el proyecto entre comillas |
| **Residuos de forma vieja de identificadores** | **Los hay**, y son **dos causas distintas** | M-08 y M-09 |
| **`_legacy/` de la migración** | 3 carpetas: la de la fusión, la de la consolidación y la del intake y el manifiesto | Ninguno se borró |
| **`_fusion/<Origen>/`** | **146 documentos** en 18 carpetas | Ninguno se sobrescribió, **y su presencia declara que la fusión no terminó** (`Migracion-Rules.md` §4.3.2). Ver **M-10** |

**Alcance que la compuerta declara no haber mirado:** el contenido de los documentos.


## 4. Criterios de aceptación de `Migracion-Rules.md` §6

### 4.1 Enumerables

| Criterio | Estado |
| --- | --- |
| Todo proyecto de código del manifiesto de origen aparece en la clasificación | **Cumple.** Los siete: dos como unidad de entrega, cinco como componentes |
| El contenido sin destino está declarado y no se descartó en silencio | **Cumple.** §6 de este informe |
| El árbol declara las citas desnudas ambiguas y su resolución confirmada | **Cumple.** §7.1 del árbol, 57 filas |
| Existe la propuesta de consolidación de casos de uso y no se aplicó por su cuenta | **Cumple.** `Migracion-8.1-Deduplicacion-Propuesta.md` propuso 25 pares sin veredicto; el veredicto lo dio `Migracion-8.5-Consolidacion-Decidida.md` con confirmación explícita del humano |
| Los documentos que chocaron al fundir están en `<categoria>/_fusion/<Origen>/` y ninguno se sobrescribió | **Cumple.** 146 documentos en 18 carpetas |
| Los enlaces se reconectaron desde un registro confirmado que distingue lo roto de lo reparado | **Cumple.** 844 entradas entre los dos registros |
| El árbol declara las familias acuñadas por el destino con su resolución confirmada | **Cumple.** `P·CU`, §7.2 del árbol |
| Existe el árbol de migración con una fila por identificador alcanzado, confirmado antes de aplicar | **Cumple.** Dos pasadas: inventario y aplicación, con confirmación entre ellas |
| Después de aplicar, ninguna referencia colgada, ningún identificador colisiona, sin residuos de forma vieja fuera de `_legacy/` | **Cumple parcialmente.** Las dos primeras sí; la tercera **no**, por dos causas distintas: **M-08** y **M-09** |
| Ningún archivo de `_legacy/` fue renombrado por la renumeración | **Cumple.** Las dos carpetas conservan los nombres de origen |

### 4.2 Interpretativos

| Criterio | Estado |
| --- | --- |
| Si el salto alcanza el nivel de aplicación, la clasificación de proyectos de código en unidades de entrega **la confirmó el humano**, y el informe declara qué señal sustentaba cada propuesta | **Cumple.** `Migracion-8.1-Arbol-De-Migracion.md` §2, con la señal por proyecto de código y la confirmación explícita antes de aplicar |
| Todo documento migrado tiene su fuente de contenido declarada en el plan, con uno de los tres valores admitidos de §2.1 | **Cumple.** El árbol clasifica cada documento; los nueve consolidados declaran además sus fuentes en la cabecera |
| Todo contenido del origen que la normativa vigente no ubica quedó enumerado en el informe, con su texto localizable | **Cumple.** §6 de este informe, con ruta y volumen por bloque |
| Ningún caso de uso se fusionó automáticamente por coincidencia de título | **Cumple.** La consolidación se decidió **por capacidad**, y dos veces el corte se corrigió al leer las fuentes: `Migracion-8.5-Consolidacion-Decidida.md` §2.1.1 y §2.1.2 |
| Ninguna sección contiene contenido que no provenga del origen, de un hermano o del humano | **Cumple.** Los nueve casos de uso consolidados citan sus fuentes en la cabecera, y §11 de cada uno declara qué agregó la unión y por qué |
| Ninguna sección exigida sin fuente quedó rellenada | **Cumple.** Los motivos internos sin traducción a respuesta se declararon **inalcanzables por construcción** en lugar de inventarles un código |
| El estado previo de cada documento migrado quedó archivado antes de sobrescribir | **Cumple con apartamiento no declarado.** Hallazgo M-03 |
| Ninguna corrección manual del usuario fue pisada | **Cumple.** No hubo correcciones manuales sobre el árbol durante la migración |
| Cada documento del plan lleva su clasificación de §4.3 | **Cumple** |
| El intake migrado se verificó contra la plantilla vigente y su bump es major | **Cumple.** Verificado sección por sección contra la plantilla 3.0, no sólo contra los campos bloqueantes; bump **1.34 → 2.0** |
| El intake se migró antes que el manifiesto, y el manifiesto antes que los documentos generados | **Cumple a medias.** El intake sí se migró antes que el manifiesto; los dos, después de los documentos. Ver **M-04**, que baja de P1 a P2 |
| Si el destino no declaraba procedencia, la degradación está declarada | **No aplica.** El destino declaraba procedencia 6.0 |
| El bloque de procedencia se reescribió **sólo** si toda la cadena quedó migrada | **Cumple.** Se reescribió **después** de verificar la cadena completa, en M5, y el manifiesto 2.1 declara la verificación que lo habilitó |
| Ninguna fila del plan quedó sin resolver y sin declararse | **Cumple.** §2.1 |
| Ningún renombre de artefacto se resolvió por inferencia | **Cumple.** Leído del bloque de impacto del `CHANGELOG.md` del framework |
| Ninguna sustitución de término se hizo por reemplazo global de cadena | **Cumple, y se verificó.** La renumeración procesó `[etiqueta](destino)` **como unidad**, después de que una corrida en seco mostrara la etiqueta y el destino tomando desplazamientos distintos |

## 5. Hallazgos

**Marca de origen:** propio de la migración, o aguas arriba. **Marca de detectabilidad:** por guion, o
sólo por lectura.

### M-10 · P2 · propio · por guion — La consolidación de la fusión está en una de nueve categorías · **CERRADO**

**Evidencia.** `find` sobre el árbol vivo devuelve **146 documentos en 18 carpetas `_fusion/`**, tres
por cada categoría de `GeometriaFactory-Api` —`Application`, `Domain`, `Infrastructure`— y una por
cada categoría de `GeometriaFactory-Web` —`Visor`—:

| Categoría | `Api` | `Web` | Qué espera consolidación |
| --- | --- | --- | --- |
| `08-Calidad-Y-Pruebas` | 27 | 9 | Estrategias, criterios, matrices de cobertura y casos referenciales, uno por capa |
| `03-UX-UI-DX` | 15 | 4 | Guías de onboarding, DX y catálogos de mensajes de error |
| `09-Devops` | 15 | 5 | Pipelines, entornos y estrategias de versionado |
| `02-Especificacion-Funcional` | 12 | 4 | Los `README.md` e índices maestros por capa. **Los casos de uso ya están consolidados** |
| `06-Backlog-Tecnico` | 12 | 4 | Backlogs de producto y técnicos |
| `10-Examples` | 12 | 1 | Ejemplos por capa |
| `05-Arquitectura-Tecnica` | 11 | 3 | Documentos de arquitectura y decisiones por capa |
| `07-Plan-Sprint` | 6 | 2 | Mini-planes por capa |
| `11-Documentacion` | 3 | 1 | Índices de documentación |

**Qué significa, y qué no.** **No** son documentos sin migrar: están renumerados, movidos y con sus
enlaces reconectados, y por eso el eje de las fases está completo y la procedencia es verificable.
Lo que falta es el acto que `Migracion-Rules.md` §4.3.2 declara **humano por diseño**: decidir, para
cada grupo de documentos que chocó al fundir, si son el mismo documento visto desde su capa —y
entonces se escribe uno con el contenido de todos— o si son documentos distintos que conservan su
identidad.

**Es exactamente el trabajo que ya se hizo para la categoría 02**, donde los 63 casos de uso pasaron a
19 con su decisión escrita y sus documentos absorbidos archivados. Faltan las otras ocho.

**Por qué esta auditoría no lo vio en tres rondas.** Porque miró `_fusion/` con el criterio de «nada
se sobrescribió», que es **un** criterio de la regla, y no leyó la frase de la misma sección que
declara qué significa que esa carpeta exista. Un recuento puede confirmar una propiedad y ocultar
otra, y **contarlo a favor fue peor que no contarlo**.

**CERRADO el 2026-08-16.** Los **67 grupos** se consolidaron y las **18 carpetas `_fusion/` se
retiraron**: no queda ninguna en el árbol. **9726 líneas de contenido absorbidas, 0 sin
correspondencia** en los documentos que transponen; las 1008 de los nueve `README` no transponen por
diseño de la salida S3 y están enteras en el archivo. El resultado, las cuatro salidas usadas y lo
aprendido están en `Migracion-M10-Consolidacion-Fusion.md` **2.0** §5.3.

**Dos correcciones de método durante la ejecución, las dos anotadas**: la unidad de trabajo pasó del
documento a **la categoría completa** —consolidar de a uno dejaba a los hermanos estacionados
apuntando al vacío—, y la reconexión pasó de la sustitución de patrón a **la resolución de destino**,
después de que un patrón rompiera 181 enlaces donde había 96.

### M-08 · P2 · propio · por guion — Dos documentos de referencia cruzada no se reconectaron en M4 · **CERRADO**

**Archivos:** `Docs/Handoff-Checkout.md` (247 identificadores de forma vieja) y
`Docs/Producto/Norma-De-Nomenclatura.md` (455).

**Evidencia.** `Handoff-Checkout.md`: «`02-Especificacion-Funcional/Reglas-De-Negocio/` | **16**
(`RN-01` a `RN-16`)». `Norma-De-Nomenclatura.md`: «`Linea-Base-Visual.md` §2, `EST-34`». Ninguno de
esos identificadores existe hoy con esa forma en el árbol.

**Causa.** Los dos son **documentos de referencia cruzada de nivel producto**: no pertenecen a
ninguna categoría ni a ninguna unidad de entrega, y la pasada de renumeración de M4 recorrió las
categorías. Cayeron en el hueco entre dos recorridos.

**Por qué la ronda 1 no lo separó.** Atribuyó **todos** los residuos a que el intake no estaba
migrado. Con el intake ya migrado se ve que son dos cosas: éstos **no citan al intake**, citan al
propio árbol, y son residuos de verdad.

**CERRADO el 2026-08-16, con salidas distintas para cada archivo, porque no eran el mismo problema.**

**`Handoff-Checkout.md` no se reconecta, y se declara superado.** Es un inventario fechado el
2026-08-12, y **sus recuentos también están viejos**: dice «doce casos de uso» y «siete proyectos de
código». Reconectarle los identificadores habría producido un documento con identificadores nuevos y
recuentos viejos, **afirmando cosas que nunca fueron ciertas**. Lleva ahora un bloque de supersesión
que declara su fecha, qué dejó de ser cierto y **dónde está hoy cada cosa que inventariaba**. Se
conserva sin tocar porque es el registro de lo que se entregó ese día.

**`Norma-De-Nomenclatura.md` tiene sus 464 citas inventariadas, y 411 son ambiguas.** El documento
**no tiene secciones por proyecto de código**, de modo que un `CU-02` desnudo puede resolver a
cualquiera de las siete numeraciones de origen. Sólo **51 resuelven** por el documento co-citado en la
misma fila. `Migracion-Rules.md` §4.3.1 dice qué hacer con esto: **declararlas y confirmarlas, no
inferirlas**. El inventario completo, cita por cita y con su línea, está en
`Migracion-M8-Registro-Citas-Norma.json`. **De las 411, las 106 de familia `RN` se cerraron por
M-09**, porque `RN` es familia única en el producto y su mapeo no depende del contexto.

**Las 305 restantes se resolvieron el 2026-08-16, y no por confirmación sino porque no eran
ambiguas.** El calificador estaba en el texto, en cuatro formas que ningún resolutor único
alcanzaba: delante del identificador, detrás, en otra columna de la fila, y en el documento fuente
citado en la fila. Cuatro pasadas sucesivas —cada una midiendo el resto de la anterior— las llevaron
a 16, y esas dieciséis se leyeron una por una. La norma quedó en **0 citas de forma vieja** en las
familias `CU`, `ADR`, `US` y `BT`. El mismo procedimiento se corrió sobre el resto del árbol:
**111 citas más** en `Plan-Etapa-A.md`, `Compatibilidad-Plataformas.md` y las necesidades de negocio.

Baja a **P2** y **cierra**.

### M-09 · P2 · propio · sólo por lectura — Las familias de identificadores del intake conservan su ancho de origen · **CERRADO**

**Evidencia.** El intake numera sus reglas `RN-01` a `RN-16` y sus invariantes `INV-01` a `INV-09`.
`Root-Rules.md` §9.2 fija **cinco dígitos uniformes** y enumera las familias alcanzadas: `NB`, `CU`,
**`RN`**, `RC`, `ADR`, `US`, `BT`, `EP`… con **dos únicas exclusiones**, `AG-XX` y el ordinal de
iteración. Ni `RN` ni `INV` están excluidas.

**Alcance medido en el árbol vivo**, sin `Audit/`: `F` 1634, `E` 1143, `A` 912, `INV` 559, `RA` 447,
`RN` 228, `X` 205, `R` 174, `CL` 158, `CP` 14, `RF` 10.

**Causa, y es una omisión de alcance de M0.** El salto 6.0 → 8.6 **alcanza la forma de los
identificadores**, y `Migracion-Rules.md` §4.3.1 exige un árbol de migración con una fila por
identificador alcanzado. El árbol se construyó **sobre `SDD/Docs/`** y no incluyó las familias que el
intake acuña. La consecuencia visible es que hoy conviven `RN-15` y `RN-02015` para la misma regla, y
**nada en el texto dice que son la misma**.

**Por qué no es P0.** No es ninguno de los seis P0 propios de la fase: no hay contenido inventado, ni
sección rellenada, ni procedencia escrita con documentos sin migrar, ni fila del plan sin declarar.
**Todos los documentos están migrados**; lo que falta es conformidad de forma en un conjunto de
familias, y queda declarado acá.

**CERRADO el 2026-08-16, partido en dos, porque no todas las familias eran el mismo caso.**

**`RN` se renumeró**, a `RN-02001` a `RN-02016`, con **377 citas reconectadas** desde
`Migracion-M9-Registro-Citas-RN.json`. **No era una elección de numeración**: el árbol migrado ya
numeraba esas mismas reglas así, y convivían `RN-15` y `RN-02015` **para la misma regla** sin que nada
lo dijera. No había dos formas legítimas: había una vigente y una cita que no la usaba. El intake sube
a **2.1** y su §4.1 lo declara.

**Las diez familias restantes conservan su ancho**, y el apartamiento está en
[`ADR-14002`](../Producto/Adrs/ADR-14002-Familias-Propias-Del-Intake-Con-Ancho-De-Origen.md). El
motivo es que **no tienen numeración de destino**: renumerarlas no reconecta nada, elige un número
nuevo sobre 5000 ocurrencias del documento del Product Owner, y `F-26` no resuelve mejor como
`F-00026`. El ADR declara qué lo reabre: que alguna de esas familias pase a tener artefacto propio
generado.

Baja a **P2**.

### M-02 · absorbido por M-08

La ronda 1 declaraba aparte que `Producto/Norma-De-Nomenclatura.md` mezcla las dos formas de
identificador. **Es el mismo hallazgo que M-08**, visto desde uno solo de sus dos archivos: la causa
no es que la norma documente la correspondencia histórica —eso sería deliberado— sino que **el
documento no se reconectó**. Se conserva el identificador con su remisión, en lugar de retirarlo, para
que una cita de la ronda 1 no quede sin respuesta.

### M-03 · P2 · propio · sólo por lectura — El archivado no usó el `_legacy/` de cada carpeta · **CERRADO**

**Evidencia.** `Migracion-Rules.md` §6 pide que «el estado previo de cada documento migrado quede
archivado en el `_legacy/` de su propia carpeta». El archivado se hizo en **dos carpetas centrales**:
`_legacy/2026-08-15-migracion-8.2/` y `_legacy/2026-08-16-consolidacion-8.5/`.

**Por qué se hizo así, y por qué es un hallazgo igual.** La migración **movió árboles enteros**: la
carpeta de origen de la mayoría de los documentos **dejó de existir**, de modo que un `_legacy/` por
carpeta habría dispersado un mismo acto en veinte lugares y habría perdido el hecho de que fue uno.
La decisión es defendible; **no haberla declarado como apartamiento no lo es**: `Root-Rules.md` §11
exige que un artefacto que se aparta de la norma lleve su ADR, y un apartamiento sin declarar se
evalúa como omisión y no como decisión.

**CERRADO el 2026-08-16** con [`ADR-14001`](../Producto/Adrs/ADR-14001-Archivado-Central-De-La-Migracion.md),
que declara el apartamiento con su motivo, acota su alcance **a esta migración y sólo a ésta** —el
archivado ordinario de `Master-Prompt.md` §13 sigue siendo por carpeta— y enumera las tres
alternativas descartadas. Baja a **P2**.

### M-04 · P2 · propio · sólo por lectura — El orden de las fases no se respetó

**Evidencia.** `Migracion-Rules.md` §6 exige que «el intake se migre antes que el manifiesto, y el
manifiesto antes que los documentos generados». Se ejecutó **sólo M4**, sin M2 ni M3.

**Consecuencia observable.** El árbol de `Docs/` habla de **unidades de entrega** y el manifiesto
sigue declarando **siete proyectos de código**. Los dos documentos que gobiernan la generación
describen un producto con una estructura que el árbol generado ya no tiene.

**Por qué no es P0.** El orden existe para que los documentos generados no se deriven de un intake
desactualizado. Acá **no se derivó nada del intake**: se migró la forma de documentos ya aprobados, y
su contenido viene de sí mismos. El daño que el criterio previene no ocurrió; lo que queda es la
cadena incompleta, que §7 declara.

**Estado en esta ronda: cerrado en lo sustantivo.** M2 y M3 se ejecutaron el 2026-08-16, en ese orden, y el manifiesto vuelve a describir la misma estructura que el árbol. Lo que queda registrado es que corrieron **después** de M4 y no antes, que es por lo que baja a P2 en lugar de cerrarse del todo: el hecho ocurrió y el informe no lo borra.

### M-05 · P2 · propio · por guion — Nueve identificadores de la unidad `Api` sin usar entre `CU-00013` y `CU-00020`

**Evidencia.** Los nueve casos de uso consolidados empiezan en `CU-00021`, dejando el tramo
`CU-00013` a `CU-00020` vacío.

**Lectura.** Es **deliberado y correcto**: los identificadores absorbidos no se reciclan, y empezar
en `CU-00013` habría dejado a `CU-00013` a un dígito de distancia de `CU-00012`, que sigue vivo en
`10-Examples`. El hueco está declarado en el README de `_legacy/2026-08-16-consolidacion-8.5/`. Se
reporta como P2 porque un hueco de numeración sin explicación al alcance de la vista es lo que la
próxima migración va a leer como pérdida.

**Recomendación.** Ninguna acción. Queda registrado para que no se levante en la ronda siguiente.

### M-06 · P2 · propio · por guion — Dos enlaces rotos por nombre ambiguo en un informe de `Audit/`

**Archivo:** `Audit/E-08-Calidad-Siete-Proyectos-r2.md`.

**Evidencia.** Cita `Estrategia-Testing.md` y `Casos-Prueba-Referenciales.md` sin ruta suficiente; el
primero existe en cuatro ubicaciones del árbol y el segundo en tres.

**Lectura.** Es un informe de auditoría **anterior** a la migración, del layout de siete proyectos de
código. Reescribirle los enlaces cambiaría un registro histórico. Se declara y no se toca.

### M-07 · P2 · aguas arriba · sólo por lectura — Los casos de uso no habían absorbido el cierre del intake 1.29

**Evidencia.** `PRODUCT-INTAKE` **1.29** §17.4 P.3 incorporó al conjunto cerrado del contrato
`CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`, el
2026-08-12, cerrando los dos huecos que la unidad `Api` había elevado al Product Owner. La
propagación llegó a `Definicion-Superficie-HTTP.md` §6 y §9, al índice maestro, a
`05-Arquitectura-Tecnica`, a `03-UX-UI-DX` y al backlog — **y no a los ocho casos de uso**, que
cuatro días después seguían declarando los dos puntos abiertos y respondiendo con el código genérico.

**Cómo apareció.** No lo detectó una búsqueda: apareció al **leer juntas** las tres vistas de una
misma capacidad, que es lo que la consolidación obliga a hacer. Los cinco casos de uso consolidados
afectados lo absorben, y `CU-00023` §10 registra el alcance de la propagación incompleta.

**Marca de origen.** Aguas arriba: el defecto es de la ronda de propagación del 2026-08-12, no de la
migración. Se reporta acá porque la migración es donde se encontró.

## 6. Contenido sin destino

**Ningún documento se borró y ningún contenido se descartó.** El inventario, con su ubicación
localizable:

| Contenido | Volumen | Dónde está |
| --- | --- | --- |
| Casos de uso absorbidos por la consolidación, unidad `Api` | **32** documentos | `_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/`, con README que declara qué caso de uso reemplaza a cada uno |
| Categorías del proyecto de código `GeometriaFactory-Contracts` sin destino en el modelo de dos ejes | **86** documentos | `_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/`, con README que declara el motivo por categoría |
| Documentos que chocaron de nombre al fundir árboles | **146** documentos | `<categoria>/_fusion/<Origen>/` en 18 carpetas, sin sobrescritura |
| Operaciones internas que no son casos de uso de una unidad de entrega | **16** documentos | Reubicados a `05-Arquitectura-Tecnica/Operaciones-Internas/`, `09-Devops`, `10-Examples` y `Producto/Contratos-Inter-Unidad/` |
| Contrato del componente visor | **7** documentos | `GeometriaFactory-Web/05-Arquitectura-Tecnica/Contrato-Componente-Visor/` |
| La familia calificada `P·CU` | **166** ocurrencias | Retirada, con trazabilidad real de necesidades en su lugar |

**Lo que sí se perdió, y hay que decirlo: nada de contenido, y sí de forma.** Los 32 documentos
absorbidos conservan su texto, pero **su estructura por capa dejó de ser navegable desde el árbol
vivo**: quien quiera leer «qué decía el dominio del alta de un alumno» tiene que ir a `_legacy/`. Es
la consecuencia buscada de la consolidación y no un defecto, pero es una pérdida de acceso y no se
declara como si no lo fuera.

## 7. Declaración de migración completa o parcial

**La migración es COMPLETA en sus dos ejes.**

### 7.1 Las fases

| Artefacto | Estado |
| --- | --- |
| `SDD/Docs/` | **Migrado a 8.6** |
| `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` | **Migrado**, 2.1, plantilla 3.0 |
| `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | **Re-derivado y confirmado**, 2.1, plantilla 5.0, procedencia **8.6** |

### 7.2 La consolidación de la fusión

| | Valor |
| --- | --- |
| Grupos consolidados | **67 de 67** |
| Carpetas `_fusion/` retiradas | **18 de 18** · **0 en el árbol** |
| Líneas de contenido absorbidas | **9726** |
| Sin correspondencia en los que transponen | **0** |
| Líneas de los `README`, salida S3 | 1008, enteras en el archivo |

`Migracion-Rules.md` §4.3.2 declara que la presencia de una carpeta `_fusion/` significa que la
fusión no terminó. **No queda ninguna.**

### 7.3 Para la próxima invocación del orquestador de generación

Su reconciliación normativa va a leer la procedencia, encontrarla **coincidente con la vigente** e
informar «al día». **No hay nada pendiente que la afecte.**

## 8. Veredicto

**APROBADO.** Cero P0, cero P1. La migración **6.0 → 8.6 queda cerrada en sus dos ejes**.

**Lo que se hizo, medido:** 26.057 identificadores renumerados sin una sola colisión · 844 enlaces
reconectados en M4, 2208 citas de sección en M2, 377 de regla en M9, 442 de artefacto en M8 y 927 en
la consolidación · **100 % de anclaje de referencias** · un intake cuyo §17 se transpuso sin
reescritura con cero pérdidas · **67 grupos de fusión consolidados con 9726 líneas absorbidas y cero
sin correspondencia**.

**El resultado que más importa no es de forma.** Al pasar los flags de siete filas por proyecto de
código a dos por unidad de entrega, `tiene_persistencia` de la entrega `GeometriaFactory-Api` **pasa
a true**. Evaluado por proyecto de código quedaba en false —la persistencia vive en
`GeometriaFactory-Infrastructure`, que no se despliega— y **se habría omitido el modelo de datos de
la entrega**.

**Lo que esta auditoría aprendió sobre sí misma, en seis rondas.** Declaró la migración «COMPLETA»
tres veces antes de que lo fuera, y una de esas veces **contó a favor la evidencia que decía lo
contrario** —los 146 documentos en `_fusion/`—. El patrón se repitió en el verificador de
preservación, que sobre-reportó cuatro de cinco veces. **La lección es la misma en los dos casos: un
recuento correcto puede sostener una conclusión falsa**, y lo que lo evita no es medir más sino
comprobar cada marca contra el texto.

**Ninguna condición queda abierta.** Los ocho P2 son informativos y están todos declarados: el hueco
de numeración `CU-00013`–`CU-00020`, los dos enlaces de un informe anterior a la migración, el orden
en que corrieron las fases, los tres apartamientos con ADR, el hallazgo aguas arriba de la
propagación del intake 1.29, y la consolidación de la fusión ya cerrada.

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Auditoría de la migración 6.0 → 8.6 sobre el destino `Lab-Geometria`. Estado de las siete fases, resultado de la compuerta mecánica sobre 435 documentos vivos, los veintiséis criterios de aceptación de `Migracion-Rules.md` §6, siete hallazgos —0 P0, 4 P1, 3 P2—, el inventario de contenido sin destino y la declaración de **migración parcial** con la procedencia intacta. |
| 2.0 | 2026-08-16 | **Ronda 2**, después de que M2, M3 y M5 se ejecutaran. La migración pasa de **PARCIAL** a **COMPLETA** y la procedencia queda en **8.6**. §2 declara las siete fases hechas y suma §2.2 con la resolución de la batería de M2. §3 rehace los recuentos sobre 437 documentos vivos. §4 cierra los tres criterios que dependían de M2 y M3. En hallazgos: **M-01 se reemplaza por M-08 y M-09**, que separan dos causas que la ronda 1 había atribuido a una sola —dos documentos de referencia cruzada sin reconectar, y las familias del intake con su ancho de origen—; **M-02 queda absorbido por M-08** y se conserva con su remisión; **M-04 baja de P1 a P2**, cerrado en lo sustantivo. Total: **0 P0, 3 P1, 4 P2**. |
| 3.0 | 2026-08-16 | **Ronda 3**, después del cierre de los tres P1. Veredicto **APROBADO** —cero P0 y cero P1— y migración **COMPLETA Y CERRADA**. **M-03** cierra con `ADR-14001`, que declara el apartamiento del archivado central y lo acota a esta migración. **M-08** cierra con dos salidas distintas: `Handoff-Checkout.md` se declara **superado** en lugar de reconectarse, porque sus recuentos también están viejos y migrarlo a medias lo habría vuelto falso; y las 464 citas de `Norma-De-Nomenclatura.md` quedan **inventariadas**, con 51 resueltas por documento co-citado y **305 pendientes de confirmación humana** después del cierre de M-09. **M-09** cierra partido en dos: `RN` **se renumera** —377 citas, porque el árbol ya numeraba esas reglas y convivían dos números para la misma— y las diez familias restantes se **declaran** en `ADR-14002`. Total: **0 P0, 0 P1, 7 P2**. |
| 4.0 | 2026-08-16 | **Ronda 4.** Cierra el último tema abierto: las 305 citas que la ronda 3 dejaba pendientes de confirmación. **No eran ambiguas**: su calificador estaba en el texto en cuatro formas distintas, y cuatro resolutores sucesivos más una lectura de las dieciséis finales las llevaron a **0**. **442 citas reconectadas** en total —331 en la norma y 111 en el resto del árbol—, con sus tres registros. Las **29 citas a la previsión de casos de uso retirada** se conservan por `Root-Rules.md` §9.3 y se declaran en el control de cambios de los siete documentos que las llevan: no admiten reescritura correcta, porque la correspondencia entre la previsión y lo emitido es lo que el corpus declara que nunca resolvió. **0 P0, 0 P1, 7 P2, ningún tema abierto.** |
| 5.0 | 2026-08-16 | **Ronda 5.** Corrige el veredicto de las rondas 2 a 4, que declararon la migración «COMPLETA» **sin separar dos ejes distintos**: las fases y la consolidación de la fusión. Las fases están completas; la consolidación está en **una de nueve categorías**, con **146 documentos en 18 carpetas `_fusion/`**, y `Migracion-Rules.md` §4.3.2 declara que esa presencia significa que la fusión no terminó. Entra **M-10** como único P1, con el inventario por categoría. **§3 corrige la lectura de la compuerta**, que había contado esos 146 documentos a favor. **§7 se parte en 7.1, 7.2 y 7.3.** El veredicto pasa de **APROBADO** a **APROBADO CON OBSERVACIONES**. La procedencia **se sostiene**: ningún documento quedó sin migrar, que es el caso que `Migracion-Rules.md` §4.6 define como migración parcial. **0 P0, 1 P1, 7 P2.** |
| 6.0 | 2026-08-16 | **Ronda 6.** **M-10 cerrado**: los 67 grupos de la fusión consolidados y las **18 carpetas `_fusion/` retiradas**, con **9726 líneas absorbidas y 0 sin correspondencia**. La migración pasa a **COMPLETA Y CERRADA EN SUS DOS EJES** y el veredicto a **APROBADO**, **0 P0 y 0 P1**. §7 se rehace sobre los dos ejes cerrados. §8 registra lo que la auditoría aprendió sobre sí misma en seis rondas: declaró «COMPLETA» tres veces antes de que lo fuera, y una de ellas contó a favor la evidencia que decía lo contrario. |
