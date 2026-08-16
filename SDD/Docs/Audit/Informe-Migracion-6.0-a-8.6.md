# Informe de migración — SDD 6.0 a 8.6

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-6.0-a-8.6.md
**Versión:** 2.0
**Fase:** M6 — Auditoría de migración, **ronda 2** (`Master-Prompt-Migracion.md` 2.0 §10)
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

Hallazgos de esta ronda: **0 P0**, **3 P1**, **4 P2**. El veredicto es **APROBADO CON OBSERVACIONES**
y la migración se declara **COMPLETA**, con **dos conformidades de forma abiertas** que se enumeran
como M-08 y M-09 y que **no son documentos sin migrar**.

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
| **`_fusion/<Origen>/`** | **146 documentos** en 18 carpetas | Ninguno se sobrescribió |

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

### M-08 · P1 · propio · por guion — Dos documentos de referencia cruzada no se reconectaron en M4

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

**Recomendación.** Reconectarlos desde registro, con la misma disciplina de dos pasadas de M4. Son
**dos archivos**, y el mapeo es el mismo que ya está registrado.

### M-09 · P1 · propio · sólo por lectura — Las familias de identificadores del intake conservan su ancho de origen

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

**Recomendación, y no es obvia.** Renumerar las familias del intake es una **decisión del Product
Owner sobre su propio documento**, con un alcance de más de 5000 ocurrencias y sin ningún beneficio
funcional inmediato. Las dos salidas legítimas son ejecutarla con árbol y registro, o **declararla
como apartamiento** con su ADR y el motivo. Lo que no es legítimo es dejarla sin decidir, que es
exactamente lo que pasó hasta acá.

### M-02 · absorbido por M-08

La ronda 1 declaraba aparte que `Producto/Norma-De-Nomenclatura.md` mezcla las dos formas de
identificador. **Es el mismo hallazgo que M-08**, visto desde uno solo de sus dos archivos: la causa
no es que la norma documente la correspondencia histórica —eso sería deliberado— sino que **el
documento no se reconectó**. Se conserva el identificador con su remisión, en lugar de retirarlo, para
que una cita de la ronda 1 no quede sin respuesta.

### M-03 · P1 · propio · sólo por lectura — El archivado no usó el `_legacy/` de cada carpeta

**Evidencia.** `Migracion-Rules.md` §6 pide que «el estado previo de cada documento migrado quede
archivado en el `_legacy/` de su propia carpeta». El archivado se hizo en **dos carpetas centrales**:
`_legacy/2026-08-15-migracion-8.2/` y `_legacy/2026-08-16-consolidacion-8.5/`.

**Por qué se hizo así, y por qué es un hallazgo igual.** La migración **movió árboles enteros**: la
carpeta de origen de la mayoría de los documentos **dejó de existir**, de modo que un `_legacy/` por
carpeta habría dispersado un mismo acto en veinte lugares y habría perdido el hecho de que fue uno.
La decisión es defendible; **no haberla declarado como apartamiento no lo es**: `Root-Rules.md` §11
exige que un artefacto que se aparta de la norma lleve su ADR, y un apartamiento sin declarar se
evalúa como omisión y no como decisión.

**Recomendación.** Emitir el ADR de apartamiento, o mover los snapshots. La primera es la que
corresponde al hecho.

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

**La migración es COMPLETA.**

| Artefacto | Estado | Procedencia que declara |
| --- | --- | --- |
| `SDD/Docs/` | **Migrado a 8.6** | Categorías por unidad de entrega, identificadores de ámbito producto y ancho cinco |
| `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` | **Migrado**, **2.0** | Plantilla **3.0**, con los dos ejes en §13 y el bloque técnico por unidad de entrega en §17 |
| `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | **Re-derivado y confirmado**, **2.1** | Plantilla **5.0**, y su §1.1 declara el conjunto **8.6** |

**La procedencia se reescribió, y la condición que lo habilita se verificó antes de escribirla.** Los
tres artefactos de la cadena están migrados, ninguna fila del plan quedó abierta y la batería de M2
está resuelta. Es lo que `Master-Prompt-Migracion.md` §9 paso 2 exige, y el manifiesto **2.1** declara
en su control de cambios qué se verificó.

**Lo que queda abierto no son documentos sin migrar, y por eso no vuelve a la migración parcial.** Son
**dos conformidades de forma** —M-08, dos documentos sin reconectar; y M-09, las familias del intake
con su ancho de origen— más el apartamiento sin declarar de M-03. Ninguna es de las seis condiciones
P0 de esta fase, y las tres están enumeradas con su alcance medido.

**Qué implica para la próxima invocación del orquestador de generación.** Su reconciliación normativa
va a leer la procedencia, encontrarla **coincidente con la vigente**, informar «al día» en una línea y
continuar sin preguntar, que es el comportamiento que `Master-Prompt.md` §2.1 tiene para ese caso.

## 8. Veredicto

**APROBADO CON OBSERVACIONES.** Cero P0. La migración **6.0 → 8.6 queda cerrada**.

Lo que la migración hizo está verificado: 26.057 identificadores renumerados sin una sola colisión,
844 enlaces reconectados en M4 y **2208 citas de sección** en M2, **100 % de anclaje de referencias**,
una consolidación de casos de uso que **se corrigió dos veces al leer las fuentes**, y un intake cuyo
§17 se transpuso **sin reescritura**, con 252 líneas de contenido y **cero pérdidas** fuera de los
cinco valores D8 que el modelo de dos ejes retira por decisión.

**El resultado que más importa no es de forma.** Al pasar los flags de siete filas por proyecto de
código a dos por unidad de entrega, `tiene_persistencia` de la entrega `GeometriaFactory-Api` **pasa
a true**. Evaluado por proyecto de código quedaba en false —la persistencia vive en
`GeometriaFactory-Infrastructure`, que no se despliega— y **se habría omitido el modelo de datos de
la entrega**. Es el defecto que el modelo de dos ejes existía para cerrar, y acá se lo ve cerrado
sobre un producto real.

**Condiciones abiertas, ninguna bloqueante:**

1. **M-08**: reconectar `Handoff-Checkout.md` y `Producto/Norma-De-Nomenclatura.md` desde registro.
2. **M-09**: decidir sobre las familias de identificadores del intake — renumerarlas con árbol y
   registro, o **declararlas como apartamiento** con su ADR. La decisión es del Product Owner.
3. **M-03**: emitir el ADR de apartamiento del archivado central, o mover los snapshots.

**Ninguna de las tres impide reinvocar el orquestador de generación**, que es lo que sigue.

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Auditoría de la migración 6.0 → 8.6 sobre el destino `Lab-Geometria`. Estado de las siete fases, resultado de la compuerta mecánica sobre 435 documentos vivos, los veintiséis criterios de aceptación de `Migracion-Rules.md` §6, siete hallazgos —0 P0, 4 P1, 3 P2—, el inventario de contenido sin destino y la declaración de **migración parcial** con la procedencia intacta. |
| 2.0 | 2026-08-16 | **Ronda 2**, después de que M2, M3 y M5 se ejecutaran. La migración pasa de **PARCIAL** a **COMPLETA** y la procedencia queda en **8.6**. §2 declara las siete fases hechas y suma §2.2 con la resolución de la batería de M2. §3 rehace los recuentos sobre 437 documentos vivos. §4 cierra los tres criterios que dependían de M2 y M3. En hallazgos: **M-01 se reemplaza por M-08 y M-09**, que separan dos causas que la ronda 1 había atribuido a una sola —dos documentos de referencia cruzada sin reconectar, y las familias del intake con su ancho de origen—; **M-02 queda absorbido por M-08** y se conserva con su remisión; **M-04 baja de P1 a P2**, cerrado en lo sustantivo. Total: **0 P0, 3 P1, 4 P2**. |
