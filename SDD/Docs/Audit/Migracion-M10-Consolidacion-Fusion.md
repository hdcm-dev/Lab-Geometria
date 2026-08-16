# M-10 — Consolidación de la fusión: el inventario y lo que mide

**Producto:** Fábrica de Geometría
**Documento:** Migracion-M10-Consolidacion-Fusion.md
**Versión:** 1.2
**Fecha:** 2026-08-16
**Regla:** `Migracion-Rules.md` §4.3.2
**Cierra:** el hallazgo **M-10** de [`Informe-Migracion-6.0-a-8.6.md`](Informe-Migracion-6.0-a-8.6.md) 5.0
**Estado:** **Análisis y propuesta. Ningún documento fue consolidado por este documento**

---

## Tabla de contenido

- [1. Lo que el inventario cambió del planteo](#1-lo-que-el-inventario-cambió-del-planteo)
- [2. El inventario](#2-el-inventario)
- [3. Las cuatro salidas, y cómo se decide cuál aplica](#3-las-cuatro-salidas-y-cómo-se-decide-cuál-aplica)
- [4. Clasificación propuesta por categoría](#4-clasificación-propuesta-por-categoría)
- [5. Orden de ejecución propuesto](#5-orden-de-ejecución-propuesto)
- [6. Lo que este análisis no decide](#6-lo-que-este-análisis-no-decide)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Lo que el inventario cambió del planteo

**Entré a esto esperando duplicados y encontré lo contrario.** La hipótesis con la que se archiva en
`_fusion/` es que al fundir árboles varios documentos con el mismo nombre describen lo mismo desde su
capa, y que consolidar es **elegir uno y absorber**. Es lo que pasó con los casos de uso de la
categoría 02, donde 63 documentos se consolidaron en 19.

Medido sobre los 67 grupos, no es lo que pasa con el resto:

| Medición | Valor |
| --- | --- |
| Líneas de contenido **únicas** en los 67 grupos | **15.763** |
| Líneas **comunes a todas las versiones** de su grupo | **933** |
| Proporción | **5,9 %** |

**El 94 % del contenido es propio de una sola capa.** El solapamiento por grupo va del **1 %** —la
especificación funcional del visor contra la del portal— al **34 %** —los `README.md` de la categoría
11, que son los más formulaicos—, con **mediana del 7 %**.

**La consecuencia sobre el método es directa: consolidar acá es una unión con atribución, no una
deduplicación.** Casi nada se descarta, porque casi nada está repetido. Lo que hay que resolver no es
«cuál de las cuatro versiones sobrevive» sino **cómo se ordena en un solo documento lo que cuatro
capas dicen de la misma categoría, sin perder de quién es cada parte**.

Es el mismo problema que el §17 del intake, y ahí funcionó la **transposición**: una sección por
subsección, y dentro de cada una una entrada por proyecto de código, nombrada. No es casualidad —es
la misma forma de dato— y por eso este análisis propone reutilizar el criterio en lugar de inventar
otro.

## 2. El inventario

**140 documentos en 18 carpetas `_fusion/`, agrupados en 67 grupos de consolidación.** Cada grupo es
un nombre de documento dentro de una categoría de una unidad de entrega.

| Categoría | Grupos | Líneas únicas | Comunes | % |
| --- | --- | --- | --- | --- |
| `08-Calidad-Y-Pruebas` | 18 | 4026 | 245 | 6 % |
| `03-UX-UI-DX` | 7 | 2527 | 91 | 4 % |
| `09-Devops` | 10 | 1902 | 132 | 7 % |
| `02-Especificacion-Funcional` | 6 | 1682 | 47 | 3 % |
| `06-Backlog-Tecnico` | 8 | 1647 | 122 | 7 % |
| `10-Examples` | 5 | 1218 | 57 | 5 % |
| `05-Arquitectura-Tecnica` | 7 | 1696 | 89 | 5 % |
| `07-Plan-Sprint` | 4 | 854 | 80 | 9 % |
| `11-Documentacion` | 2 | 211 | 70 | 33 % |

**Todos los grupos tienen documento vigente.** No hay ningún caso de un documento que sólo exista en
`_fusion/`: la unidad de entrega tiene el suyo —el del proyecto de código que le da nombre— y las
otras capas están estacionadas. Eso simplifica la consolidación: **hay un documento anfitrión** y lo
que falta es incorporarle lo que las demás capas traen.

**Seis documentos de `_fusion/` no son grupos de consolidación**: son `_legacy/` propios que viajaron
con su carpeta. No se tocan.

## 3. Las cuatro salidas, y cómo se decide cuál aplica

| Salida | Cuándo | Qué se hace |
| --- | --- | --- |
| **S1 · Transposición con atribución** | El documento tiene **secciones fijas** que cada capa completa con su contenido —estrategias, matrices, pipelines, criterios— | El anfitrión conserva su estructura; cada sección incorpora una entrada por proyecto de código, **nombrada**. Ninguna cifra se promedia |
| **S2 · Unión de catálogo** | El documento es una **colección de entradas identificadas** —backlogs, casos de prueba, mensajes de error, glosarios— | Se unen las entradas. **Los identificadores ya no colisionan** porque la renumeración les dio rango propio por capa, de modo que la unión es directa |
| **S3 · Reescritura del índice** | El documento es un **`README.md` o índice** de la categoría | El anfitrión se reescribe sobre el inventario real de la categoría fundida. Los de las capas **no aportan contenido**: aportan la lista de lo que hay que indexar, y se archivan |
| **S4 · Coexistencia con identidad propia** | Los documentos son **artefactos distintos que comparten nombre por convención**, no versiones de uno solo | Se conservan los N, renombrados para que su identidad sea visible. **Nada se funde** |

**Cómo se decide, y por qué el % de solapamiento no alcanza.** El porcentaje dice cuánto se repite,
no qué es el documento. Un `Mini-Plan.md` con 6 % de solapamiento y un `ejemplo-01-basico.md` con
3 % se parecen en el número y no en lo que hay que hacer con ellos: el primero es un plan que las
cuatro capas escriben sobre las mismas secciones (**S1**), el segundo son cuatro ejemplos distintos
que se llaman igual porque la convención de la categoría los numera (**S4**).

## 4. Clasificación propuesta por categoría

| Categoría | Documentos | Salida propuesta | Fundamento |
| --- | --- | --- | --- |
| `08-Calidad-Y-Pruebas` | `Estrategia-Testing`, `Estrategia-Calidad`, `Criterios-Validacion`, `Plan-Pruebas`, `Definition-Of-Done` | **S1** | Cada capa declara **sus umbrales**, distintos entre sí —90/85 en dominio, 85/80 en aplicación, 95 en el validador—. `PRODUCT-INTAKE` §22 A-3 y A-4 los declara asunciones vivas: **promediarlos pierde el dato y ninguna prueba lo detecta** |
| | `Matriz-Cobertura-Pruebas`, `Casos-Prueba-Referenciales`, `Matriz-Sensado-Deriva` | **S2** | Son catálogos de entradas identificadas, con rango propio por capa |
| | `README` | **S3** | |
| `03-UX-UI-DX` | `DX-Error-Messages`, `Glosario-UX` | **S2** | Catálogos: mensajes por código y términos. **El de mensajes es el grupo con más contenido único del inventario**, 853 líneas |
| | `DX-Developer-Experience`, `Guia-Onboarding-Developer` | **S1** | Secciones fijas que cada capa completa |
| | `README` | **S3** | |
| `09-Devops` | `Pipeline-CI-CD`, `Entornos-Deploy`, `Estrategia-Versionado`, `Supply-Chain-Seguridad` | **S1** | Stages y gates por capa, con los de la entrega como marco |
| | `README` | **S3** | |
| `02-Especificacion-Funcional` | `Especificacion-Funcional`, `Glosario-Funcional` | **S1** el índice maestro, **S2** el glosario | Los casos de uso **ya están consolidados**; falta el índice y el vocabulario |
| | `README` | **S3** | |
| `06-Backlog-Tecnico` | `Product-Backlog`, `Backlog-Tecnico` | **S2** | Catálogos de `US` y `BT` con rango propio por capa |
| | `Definition-Of-Ready` | **S1** | |
| | `README` | **S3** | |
| `05-Arquitectura-Tecnica` | `Arquitectura-Proyecto-Codigo` | **S1** | Es el grupo más grande del inventario: **732 líneas únicas** entre cuatro capas |
| | `Decisiones-Arquitectura`, `Contratos-Abstractions` | **S2** | Catálogos de ADR y de contratos |
| | `README` | **S3** | |
| `07-Plan-Sprint` | `Mini-Plan` | **S1** | |
| | `README` | **S3** | |
| `10-Examples` | `ejemplo-01`, `ejemplo-02`, `ejemplo-03` | **S4** | **Son samples distintos que comparten nombre por la convención de la categoría.** El `ejemplo-01-basico` del dominio y el de la infraestructura no son dos versiones de uno: son dos ejemplos. Fundirlos produciría un sample que no ejercita nada |
| | `README` | **S3** | |
| `11-Documentacion` | `README` | **S3** | Es el grupo con más solapamiento del inventario, 33 %: son los más formulaicos |

**S4 se propone en un solo lugar, y conviene que sea el que más se discuta.** Es la única salida que
**no reduce documentos**, y por eso es la que más fácil se descarta por incomodidad. El criterio para
sostenerla es el de `Rules-Examples.md`: un sample tiene un contrato de verificación y una evidencia
de corrida. Cuatro samples con contratos distintos no se funden en uno con un contrato: se funden en
uno que **no verifica ninguno de los cuatro**.

## 5. Orden de ejecución propuesto

Por valor y por riesgo, no por tamaño:

1. **`08-Calidad-Y-Pruebas`** — 18 grupos, 4026 líneas. Es donde está el dato que más caro sale
   perder: los umbrales por capa. Y es la más grande, de modo que valida el criterio sobre el peor
   caso en lugar del más cómodo.
2. **`05-Arquitectura-Tecnica`** y **`02-Especificacion-Funcional`** — cierran las categorías que la
   consolidación de casos de uso dejó a medias.
3. **`03-UX-UI-DX`**, **`06-Backlog-Tecnico`**, **`09-Devops`** — volumen alto, criterio ya validado.
4. **`07-Plan-Sprint`**, **`10-Examples`**, **`11-Documentacion`** — las tres chicas, y `10-Examples`
   con la decisión de S4 ya tomada.

**Se propone empezar por un grupo completo y presentarlo antes de seguir**, como se hizo con
`CU-00021` en la consolidación de casos de uso: el criterio de redacción se valida mejor sobre un
documento concreto que sobre una tabla de salidas. El candidato es
**`08-Calidad-Y-Pruebas/Estrategia-Testing.md` de `GeometriaFactory-Api`**: cuatro versiones, 312
líneas únicas, 6 % de solapamiento, y es el que lleva los umbrales que no se pueden promediar.


## 5.1 La unidad de consolidación es la categoría, no el documento — aprendido en el primer grupo

**El primer grupo consolidado rompió 61 enlaces**, y no por un defecto de la consolidación sino por
su granularidad.

Al sacar `Estrategia-Testing.md` de `_fusion/Domain/`, `_fusion/Application/` y
`_fusion/Infrastructure/`, **los demás documentos que seguían estacionados en esas mismas carpetas se
quedaron citando a un hermano que ya no estaba**. `Plan-Pruebas.md` de `_fusion/Domain/` citaba
`Estrategia-Testing.md` como vecino de carpeta, y esa cita dejó de resolver.

**Es estructural y va a repetirse en cada grupo.** Los documentos de una capa se citan entre sí como
vecinos, de modo que **consolidar de a un documento deja a sus hermanos apuntando al vacío**, y hay
que reconectarlos en cada pasada. Sobre 67 grupos, eso es reconectar la misma carpeta hasta nueve
veces.

**La corrección al plan: la unidad de trabajo es la categoría completa.** Se consolidan los N
documentos de una categoría **en una sola pasada**, y recién al terminarla se retira su carpeta
`_fusion/` entera. Así cada carpeta se reconecta una vez y no N veces, y —más importante— **la
categoría queda coherente consigo misma en todo momento**: no hay un estado intermedio donde la mitad
de sus documentos cite a documentos vigentes y la otra mitad a estacionados.

Los 61 enlaces del primer grupo quedaron reconectados, con su registro en
`Migracion-M10-Registro-Reconexion.json`. **El grupo `Estrategia-Testing` queda como está** —ya
consolidado— y `08-Calidad-Y-Pruebas` se termina como categoría, que es como se hará el resto.


## 5.2 La reconexión se hace por resolución de destino, no por sustitución de patrón — aprendido en la primera categoría

**Al terminar `08-Calidad-Y-Pruebas` intenté reconectar los enlaces con una sustitución de patrón
sobre todo el árbol, y rompí 181 enlaces donde había 96.**

El patrón parecía seguro: reescribir `../../../08-Calidad-Y-Pruebas/_fusion/<capa>/<doc>` a
`../08-Calidad-Y-Pruebas/<doc>`. **Lo que no consideré es que la profundidad correcta depende de dónde
está el documento que cita**, y el mismo texto de enlace es correcto desde una categoría e incorrecto
desde dentro de un `_fusion/`. La sustitución alcanzó documentos de `GeometriaFactory-Web` que no
tenían nada que ver con la consolidación y les rompió enlaces que estaban bien.

Los archivos dañados se restauraron desde el commit anterior, y la reconexión se rehízo con otro
método: **para cada enlace que no resuelve, buscar el destino real por nombre de archivo, acotado a la
unidad de entrega del documento que cita o del destino roto, y calcular la ruta con `relpath`**. No
reescribe ningún enlace que funcione, no depende de la profundidad y no puede alcanzar a un documento
ajeno. Resolvió **92 de 94**; los dos restantes son los del informe de auditoría anterior a la
migración, que citan por nombre ambiguo y son registro histórico.

**Es la misma lección que la migración ya había aprendido dos veces, y la volví a cometer.** En M4 fue
la etiqueta y el destino tomando desplazamientos distintos; en M2, el pase de citas reescribiendo mis
propios encabezados. Las tres veces el defecto es el mismo: **una sustitución de patrón no sabe dónde
está parada**. La regla que queda escrita para el resto de las categorías es que la reconexión se hace
**resolviendo destinos, y sólo sobre los enlaces que ya no resuelven**.

## 6. Lo que este análisis no decide

- **No consolida nada.** `Migracion-Rules.md` §4.3.2 reserva la decisión al humano, grupo por grupo,
  y lo que este documento aporta es la medición para que la decisión no sea a ciegas.
- **No decide qué pasa con los `_legacy/` que viajaron dentro de `_fusion/`.** Son seis y son
  snapshots de un estado anterior de un documento que hoy está estacionado. Cuando su grupo se
  consolide habrá que decidir si acompañan al archivo de la consolidación o se quedan donde están.
- **No mide la calidad de lo que hay adentro.** Mide cuánto se repite. Que dos capas digan cosas
  distintas no significa que las dos sean correctas: puede haber contradicciones entre capas que sólo
  aparezcan al ponerlas en el mismo documento, que es exactamente lo que pasó al consolidar los casos
  de uso y produjo el hallazgo `M-07`. **Es previsible que la consolidación levante hallazgos**, y
  eso no es un costo del método: es su rendimiento.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Inventario de los 67 grupos de consolidación con su medición de solapamiento —**5,9 % global**, del 1 % al 34 %—, que **corrige la hipótesis de partida**: no son duplicados, el 94 % del contenido es propio de una capa y consolidar es una unión con atribución. Cuatro salidas con su criterio, clasificación propuesta por categoría, orden de ejecución y lo que el análisis no decide. **Ningún documento fue consolidado.** |
| 1.1 | 2026-08-16 | **§5.1 nueva, aprendida al consolidar el primer grupo.** La consolidación de `Estrategia-Testing.md` rompió **61 enlaces** de los documentos que seguían estacionados en las mismas carpetas `_fusion/`, que lo citaban como vecino. Es estructural y se repetiría en cada grupo, hasta nueve veces por carpeta. **La unidad de trabajo pasa a ser la categoría completa**, no el documento: se consolidan sus N documentos en una pasada y recién entonces se retira su `_fusion/`, con lo que cada carpeta se reconecta una vez y la categoría nunca queda en un estado intermedio incoherente. |
| 1.2 | 2026-08-16 | **§5.2 nueva, aprendida al cerrar la primera categoría.** Una sustitución de patrón para reconectar los enlaces **rompió 181 donde había 96**, alcanzando documentos de la otra unidad de entrega: la profundidad correcta de un enlace depende de dónde está el documento que cita, y un patrón no lo sabe. Los dañados se restauraron desde el commit y la reconexión se rehízo **resolviendo destinos por nombre, acotada a la unidad de entrega y sólo sobre enlaces que ya no resuelven**: 92 de 94. Es la tercera vez que la migración tropieza con lo mismo —M4 con la etiqueta y el destino, M2 con los encabezados propios—, y queda como regla para las ocho categorías restantes. |
