# M2 — Propuesta de migración del intake, con su diff de estructura y su batería

**Producto:** Fábrica de Geometría
**Documento:** Migracion-M2-Propuesta-Intake.md
**Versión:** 1.0
**Fase:** M2 — Migración del intake (`Master-Prompt-Migracion.md` 2.0 §6)
**Fecha:** 2026-08-16
**Origen:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.34**, sobre plantilla **2.1**
**Destino:** plantilla `PRODUCT-INTAKE-template.md` **3.0**
**Estado:** **Propuesta. Ningún archivo de `SDD/Intake/` fue modificado por este documento**

---

> **El intake es documento humano.** M2 propone y no escribe hasta tener **dos aprobaciones
> explícitas**: la del diff de estructura de §2, y la resolución de la batería de §4. Aprobar el diff
> **no** autoriza a escribir con la batería abierta.

---

## Tabla de contenido

- [1. Qué cambia, en una línea](#1-qué-cambia-en-una-línea)
- [2. Diff de estructura](#2-diff-de-estructura)
- [3. La decisión de fondo: los siete bloques §17](#3-la-decisión-de-fondo-los-siete-bloques-17)
- [4. Batería de preguntas](#4-batería-de-preguntas)
- [5. Contenido sin destino](#5-contenido-sin-destino)
- [6. Dos defectos de la plantilla vigente, elevados](#6-dos-defectos-de-la-plantilla-vigente-elevados)
- [7. Qué falta para escribir](#7-qué-falta-para-escribir)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué cambia, en una línea

**El intake pasa de describir un eje a describir dos.** Hoy declara **siete proyectos de código** y
les cuelga a cada uno un bloque técnico de doce subsecciones. La plantilla vigente separa lo que se
**entrega** de lo que se **compila**: el producto tiene **dos unidades de entrega** y **siete
proyectos de código**, y el bloque técnico pasa a colgar de las entregas, que son dos.

**Las secciones §1 a §12 no cambian.** Ni de número, ni de nombre, ni de contenido. Todo el cambio
está concentrado en **§13, §14 y §17**.

## 2. Diff de estructura

### Secciones movidas

Ninguna. La numeración §1 a §12 y §14 a §21 se conserva.

### Secciones partidas

| Origen | Vigente | Qué queda en cada una |
| --- | --- | --- |
| **§13** Proyectos de código del producto | **§13.1** Unidades de entrega | Las dos que se despliegan: `GeometriaFactory-Api` y `GeometriaFactory-Web`, con su valor D8, su integración en runtime y su estado |
| | **§13.2** Proyectos de código | Los siete que se compilan, con su solución de código, su stack, sus dependencias de **compilación** y qué unidad compone cada uno |
| | **§13.3** Matriz de composición | El cruce de los dos ejes, con `GeometriaFactory-Contracts` marcado como **compartido** |

### Secciones renombradas

| Origen | Vigente |
| --- | --- |
| §13 «Proyectos de código del producto» | §13 «**Composición del producto: los dos ejes**» |
| §17 «Bloque técnico por proyecto de código» | §17 «**Bloque técnico por unidad de entrega**» |

### Secciones colapsadas

**Este movimiento no tiene renglón en el formato de diff de la regla, y es el más grande de esta
migración.** Se declara aparte en lugar de forzarlo en una fila de «partidas»:

| Origen | Vigente |
| --- | --- |
| §17.1 a §17.7 — **siete** bloques, uno por proyecto de código | §17.1 y §17.2 — **dos** bloques, uno por unidad de entrega |

Los cinco bloques que dejan de tener bloque propio son `Domain`, `Application`, `Infrastructure`,
`Contracts` y `Visor`. **Qué se hace con su contenido es la decisión de §3**, y no la toma esta
propuesta.

### Campos que cambian de dueño o desaparecen

| Campo | En el origen | En la plantilla vigente |
| --- | --- | --- |
| `tipo_proyecto_codigo` (D8) | Atributo de **cada uno de los siete** proyectos de código | **Deja de existir.** El D8 pasa a ser `tipo_unidad_entrega`, atributo de las **dos** unidades de entrega. §13.2 vigente lo dice con todas las letras: «los proyectos de código no llevan valor D8» |
| `redistribuible` | Atributo del proyecto de código | Atributo de la **unidad de entrega** |
| Solución de código | **No existe** | Columna nueva de §13.2 |
| Integra con (runtime) | Se lee en la prosa de §13 y en el diagrama de §14 | Columna nueva de §13.1 |
| Estado (vigente / diferida) | **No existe** | Columna nueva de §13.1 |
| Contrato de integración vs. de compilación | §14 los enumera juntos, en una sola tabla «qué expone cada proyecto de código» | §14 vigente exige **dos tablas separadas** y declara que mezclarlas es «el error frecuente» |

### Secciones nuevas sin fuente

**Ninguna sección entera.** Los campos nuevos de arriba tienen fuente, y se declara cuál:

| Campo nuevo | Fuente propuesta |
| --- | --- |
| §13.1, las dos unidades de entrega y su D8 | **Respuesta del humano**: la clasificación confirmada en `Migracion-8.1-Arbol-De-Migracion.md` §2, con la señal que la sustenta por proyecto de código |
| §13.1, «Integra con (runtime)» | §13 del origen: «la arista `Web → Api` es de **runtime**, no de compilación» |
| §13.1, `redistribuible` de las dos unidades | §13 del origen: «ningún proyecto de código se publica como paquete redistribuible» → `false` en las dos |
| §13.2, «Solución de código» | §16 del origen: `GeometriaFactory.sln` para los seis proyectos .NET; el séptimo, `GeometriaFactory-Visor`, **no pertenece a ninguna solución**: es un proyecto Node.js en `visor/`, con la excepción ya declarada en §13 y §16 |
| §13.2, «Compone» | `Migracion-8.1-Arbol-De-Migracion.md` §3, la matriz confirmada |
| §13.3, la matriz | Derivada de la columna anterior. **Es dato derivado**: se publica para revisión y no se declara a mano |
| §14, las dos clases de contrato | La tabla de §14 del origen ya distingue las dos en su prosa —«la arista `Web → Api` es de runtime»— y lo que falta es **separarlas en dos tablas**, no averiguar nada |

**El campo «Estado (vigente / diferida)» de §13.1 es el único sin fuente**, y va a la batería como
**B-1**.

## 3. La decisión de fondo: los siete bloques §17

Los bloques `Api` y `Web` pasan a ser los bloques de las dos unidades de entrega **sin transformación
de contenido**: ya describen lo que se despliega.

Los otros cinco no tienen dónde ir tal cual. La plantilla vigente explica por qué, y conviene leerlo
antes de decidir:

> «Un proyecto de código que no se despliega no tiene NFR de latencia ni plataformas objetivo
> propias: los hereda de la entrega que lo contiene. Lo que sí es propio de cada proyecto de código
> —su lenguaje, sus dependencias de compilación y su rol en la arquitectura— ya está declarado en
> §13.2, y se detalla en la categoría 05 de la entrega.»

**Lo que hay en esos cinco bloques no es homogéneo**, y por eso la decisión no es una sola:

| Subsección | Qué contiene en los cinco | Lectura |
| --- | --- | --- |
| **P.1** Stack | Lenguaje y dependencias core de cada uno | La plantilla vigente lo previó: «cuando una unidad de entrega se compone de varios proyectos de código con stacks distintos, P.1 **enumera los stacks con el proyecto de código al que corresponde cada uno**». **Tiene destino y es literal** |
| **P.2** Estilo arquitectónico | El estilo interno de cada capa, con sus dos alternativas descartadas | Es lo más denso de los cinco. La plantilla lo llama «detalle de capas internas» y lo manda a **§17 P.2 de la unidad y a la categoría 05** |
| **P.3** a **P.5** Comunicación, persistencia, seguridad | Puertos, EF Core con SQLite, derivación de clave y JWT | Son decisiones **de lo que se entrega**, y las dos unidades ya las heredan de hecho: la persistencia del producto es una sola y vive en la entrega `Api` |
| **P.6** Estrategia de testing | Coberturas y gates **por proyecto de código**, distintos entre sí | **No se pueden fusionar sin perder el dato**: son siete conjuntos de umbrales distintos, y §22 A-3 y A-4 los declaran como asunciones vivas |
| **P.7** a **P.9** Versionado, pipeline, plataformas | Casi idénticos entre los cinco | Se absorben sin pérdida |
| **P.10** NFR | Números por proyecto de código: 500 ms en Application, 200 ms en el validador, p99 de 500 ms en Api | La plantilla dice que un proyecto que no se despliega **no tiene NFR propios**, pero **estos existen y están medidos**. Ver **B-2** |
| **P.11** pre-ADR | Decisiones pre-tomadas, citadas **por identificador** desde toda la documentación generada | **`§17.1.P.11`, `§17.2.P.2` y las demás se citan por su número desde los documentos de `SDD/Docs/`.** Renumerarlas rompe esas citas. Ver **B-3** |
| **P.12** Restricciones y trade-offs | Propios de cada capa | Se absorben con su proyecto de código nombrado |

**La propuesta**, para que la batería tenga contra qué decidirse:

1. Los bloques **§17.1 `GeometriaFactory-Api`** y **§17.2 `GeometriaFactory-Web`** se escriben con el
   contenido de los bloques homónimos del origen, **sin reescritura**.
2. Cada subsección **P.N** de la unidad absorbe las P.N de sus proyectos de código componentes,
   **nombrando el proyecto de código en cada entrada** en lugar de fundir los textos. Es el mismo
   criterio que la consolidación de casos de uso aplicó: la unión declara de quién viene cada parte.
3. `GeometriaFactory-Contracts` es **compartido**: sus entradas aparecen en **las dos** unidades,
   marcadas como tales, que es lo que la matriz de §13.3 hace visible.
4. **Ninguna cifra se promedia, se redondea ni se unifica.** Si `Domain` pide 90/85 y `Api` 75/70, las
   dos filas quedan.

## 4. Batería de preguntas

Formato de `Intake-Rules.md` §6. **Las tres son bloqueantes para escribir.**

### B-1 · §13.1, columna «Estado» — ¿alguna unidad de entrega es diferida?

**Por qué se pregunta.** La columna es nueva y ninguna fuente del intake declara una entrega
planificada para otra etapa. **No se infiere**: una entrega marcada `diferida` no recibe documentación
en la corrida, y suponerlo mal en cualquiera de las dos direcciones cambia el alcance de todo lo que
sigue.

**Lo que se sabe.** §15 y §16 describen las dos unidades como en construcción, y `SDD/Docs/` tiene las
once categorías emitidas para las dos.

**Opciones.**
- **(a)** Las dos `vigente`. *Es lo que la evidencia sugiere.*
- **(b)** Alguna `diferida` — indicar cuál y por qué.

### B-2 · §17 P.10 — ¿qué pasa con los NFR de los proyectos de código que no se despliegan?

**Por qué se pregunta.** La plantilla vigente afirma que un proyecto de código que no se despliega
«no tiene NFR de latencia propios: los hereda de la entrega que lo contiene». **En este producto
existen y están medidos**: 500 ms de validación en `Application` y 200 ms en el validador de
`Infrastructure`, y §22 A-5 los declara como asunción viva. Además, `08-Calidad-Y-Pruebas` los
ejercita: `CU-00026` CA-15 mide «menos de 500 ms, sin acceso a base de datos».

**El conflicto es real:** la plantilla dice que no deberían existir y el producto los tiene medidos y
probados.

**Opciones.**
- **(a)** Conservarlos en el P.10 de la unidad, **nombrando el proyecto de código de cada número**.
  *Preserva el dato y la prueba que lo verifica; deja el P.10 de la unidad con filas de dos
  naturalezas.*
- **(b)** Moverlos a `05-Arquitectura-Tecnica` de la unidad y dejar en P.10 sólo los NFR de la
  entrega. *Alinea con la plantilla; los saca del intake, que es donde el gate los lee.*
- **(c)** Conservarlos y **elevar un apartamiento declarado** contra la afirmación de la plantilla,
  con su ADR. *Es la salida que `Root-Rules.md` §11 prevé cuando el producto contradice la norma con
  evidencia.*

### B-3 · §17 P.11 — ¿se renumeran los pre-ADR o se conservan sus números de origen?

**Por qué se pregunta.** Las decisiones pre-tomadas se citan **por su número de sección** desde toda
la documentación generada: `§17.1.P.11 punto 4`, `§17.2.P.11 punto 3`, `§17.5.P.5`. Al colapsar siete
bloques en dos, `§17.1` deja de ser `Domain` y pasa a ser `Api`. **Cada una de esas citas cambia de
referente sin que nada falle**, que es exactamente el defecto que R5 previene y que acá no se puede
prevenir con un identificador, porque **estas citas son a una sección y no a un artefacto
identificado**.

**Volumen medido:** las citas con la forma `§17.<n>.P.<m>` aparecen en la documentación generada y en
el propio intake, y **todas cambian de referente** con el colapso.

**Opciones.**
- **(a)** Renumerar y **reconectar las citas desde un registro confirmado**, como se hizo con los
  enlaces en M4. *Es la única que deja el intake conforme a la plantilla y las citas correctas; es
  trabajo de dos pasadas.*
- **(b)** Conservar la numeración de origen dentro de cada bloque de unidad —`§17.1.P.11` sigue siendo
  el de `Domain`, ahora anidado bajo la unidad `Api`—. *Ninguna cita se rompe; la numeración deja de
  ser la de la plantilla.*
- **(c)** Renumerar y **no** tocar las citas. *No se propone: deja el corpus afirmando cosas falsas y
  es P0 de la auditoría.*

## 5. Contenido sin destino

Texto del origen que la plantilla vigente **no ubica en ninguna sección**. Enumerado con su ubicación
para que la decisión sea sobre algo localizable, no sobre una descripción.

| Contenido | Dónde está en el origen | Volumen | Situación |
| --- | --- | --- | --- |
| **§22 Supuestos declarados y puntos a confirmar** | Líneas 1596-1615 | 5 asunciones vivas (A-1 a A-5) más las marcas `[A VERIFICAR]` heredadas de las fuentes | **Sin destino.** La plantilla 3.0 no tiene §22. El propio documento se declara «sección propia de este intake, fuera de la plantilla», de modo que **ya era un apartamiento** bajo la 2.1 |
| **`tipo_proyecto_codigo` de los cinco proyectos que no son unidad de entrega** | §13, columna 2 | 5 valores `library` | **Sin destino por decisión de la plantilla.** No es una pérdida accidental: el modelo de dos ejes declara que un proyecto de código no lleva D8 |
| **§13.1 Idioma de los identificadores de código** | Líneas 459-474 | 3 decisiones del Product Owner (`F-01`, `F-02`, `F-03`), con alcance medido en documentos y ocurrencias | **Sin destino, y es el que más importa.** La plantilla 3.0 lleva el perfil de convención de nombres dentro de §13.3, pero **no tiene lugar para decisiones de idioma de identificadores**. Su fuente única es `Producto/Norma-De-Nomenclatura.md`, que sigue viva |
| **La nota sobre los cuatro planos de identidad** | Líneas 25-44 | 1 sección de cabecera | **Sin destino formal**, y se propone conservarla: `Vocabulario-Rules.md` §4 la exige y la plantilla la da por sabida |
| **Fuentes de este intake y regla de veracidad** | Líneas 99-116 | 1 sección | **Sin destino formal.** Es lo que hace verificable todo el resto del documento; se propone conservarla |

**Ninguno se descarta.** Lo que esta propuesta pide decidir es **dónde va cada uno**, y las cinco
filas admiten la misma salida: conservarse como sección propia con **apartamiento declarado**
(`Root-Rules.md` §11), que es lo que el propio §22 ya venía haciendo sin nombrarlo así.

## 6. Dos defectos de la plantilla vigente, elevados

Aparecieron al construir esta propuesta. **No bloquean M2**, y conviene que estén escritos antes de
que alguien los copie.

### D-1 · La tabla de identidad de §17 sigue pidiendo D8 al proyecto de código

`PRODUCT-INTAKE-template.md` 3.0 §17 declara la tabla «Identidad del proyecto de código (repetir por
proyecto de código)» con las filas `tipo_unidad_entrega` (D8) y `redistribuible`. **Contradice a
§13.2 del mismo documento**, que declara que «los proyectos de código no llevan valor D8», y a §13.1,
que hace de `redistribuible` un atributo de la unidad de entrega.

Es un residuo de la intervención 8.0: la tabla se conservó del bloque anterior, que sí era por
proyecto de código. **Quien complete el intake con la plantilla en la mano va a declarar D8 siete
veces.**

### D-2 · Las instrucciones de §17.P.N siguen diciendo «del proyecto de código»

El encabezado de §17 dice «por unidad de entrega» y las instrucciones de P.1, P.2 y P.12 dicen «del
proyecto de código». Es menor y de la misma causa.

**Los dos son de `IA.SDD`, no de este destino.** Se elevan como hallazgos aguas arriba.

## 7. Qué falta para escribir

**Ninguna de las dos aprobaciones está dada.** M2 no escribe hasta tener las dos:

1. **Aprobación del diff de estructura** de §2 y de la propuesta de §3.
2. **Resolución de la batería**: B-1, B-2 y B-3.

Con las dos dadas, la escritura sigue el flujo de `Master-Prompt.md` §13:

- Archivado previo del estado anterior en `SDD/Intake/_legacy/2026-08-16/`.
- Fila en el control de cambios del intake.
- **Bump major**: 1.34 → **2.0**, porque una migración estructural reescribe secciones ya aprobadas.
- Y recién después, **M3**: re-derivar el manifiesto del intake migrado, con su propia confirmación.

**Si alguna de las tres condiciones del caso (b) de `Master-Prompt.md` §13 regla 2 no se cumple** —
falta la aprobación, quedó una sección rellenada con contenido inferido, o el bump no es major— **M2
no escribe**, y esta propuesta queda como el estado declarado de la fase.

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Diff de estructura del intake 1.34 contra la plantilla 3.0: §1 a §12 sin cambio, §13 partido en tres, §13 y §17 renombrados, siete bloques técnicos colapsados en dos, seis campos que cambian de dueño o desaparecen y ninguna sección nueva sin fuente. Propuesta de absorción de los cinco bloques que dejan de tener bloque propio. Batería de tres preguntas bloqueantes: el estado de las unidades de entrega, los NFR de los proyectos que no se despliegan, y la renumeración de los pre-ADR con sus citas por número de sección. Cinco bloques de contenido sin destino, enumerados con su ubicación. Dos defectos de la plantilla vigente, elevados aguas arriba. **Ningún archivo de `SDD/Intake/` fue modificado.** |
