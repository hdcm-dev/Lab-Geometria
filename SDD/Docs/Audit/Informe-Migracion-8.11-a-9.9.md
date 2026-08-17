# Informe de auditoría de migración — 8.11 → 9.9

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-8.11-a-9.9.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Auditor:** Auditor independiente, invocado desde cero
**Responsable de mantenerlo:** el auditor que lo emite; se supera con el informe de la migración siguiente
**Instrumento normativo:** `Master-Prompt.md` **8.4** §10, con los criterios de aceptación de `Migracion-Rules.md` **3.7** §6
**Alcance:** la migración normativa 8.11 → 9.9 del destino `Lab-Geometria`, ejecutada el 2026-08-17
**Veredicto:** **APROBADO** — **0 P0, 0 P1, 3 P2, 3 P3**

---

## 1. Qué se auditó, y contra qué

Las siete filas del `Plan-Migracion-8.11-a-9.9.md` **1.0**, ejecutadas en las fases M2 a M5, sobre el
conjunto normativo **9.9** leído del snapshot `_legacy/9.9/` del repositorio del framework.

**El auditor no heredó ninguna afirmación del orquestador.** Las cifras de este informe se midieron
sobre el árbol: los enlaces se resolvieron uno por uno, el archivado se comparó contra el estado
anterior recuperado del historial, y la revisión de apartamientos se re-verificó leyendo las entradas
del `CHANGELOG` de 8.12 a 9.9 contra el disparador de cada ADR.

**Lo que este informe no audita.** El contenido de las categorías documentales, que es trabajo del
audit de generación con su propio auditor. Acá se audita **la migración**: qué se movió, qué se
preservó y qué se declaró.

---

## 2. Estado de las fases

| Fase | Qué hizo | Estado |
| --- | --- | --- |
| **M0** | Reconocimiento: intake y manifiesto con nombre vigente, procedencia 8.11, conjunto de origen disponible en `_legacy/8.11/`, 459 documentos vivos | **Cerrada** |
| **M1** | Diff normativo **verificado** —no reconstruido—, con el objetivo corregido de 9.1 a **9.9**; revisión de apartamientos; plan emitido y aprobado | **Cerrada** |
| **M2** | Intake **2.2 → 3.0**, siete residuos corregidos, batería sin preguntas | **Cerrada** |
| **M3** | Manifiesto re-derivado **2.2 → 2.3**, sin cambio de contenido | **Cerrada** |
| **M4** | Dos apartamientos con estado y contador; once rutas del layout anterior en cuatro documentos | **Cerrada** |
| **M5** | Procedencia **8.11 → 9.9**, escrita después de verificar la cadena completa | **Cerrada** |
| **M6** | Este informe | **Cerrada** |

**Cada fase se entregó en su propia rama con su pull request**, y el humano fusionó las seis: PR #48
a #52 más la reparación previa. Es el traspaso de `Master-Prompt.md` §12.1, y **se cumplió con una
excepción declarada en `A-06`**.

---

## 3. Compuerta mecánica

Medida sobre el árbol vivo, excluidos los snapshots de `_legacy/`:

| Medición | Resultado |
| --- | --- |
| Enlaces relativos | **4698** |
| Resuelven | **4694** |
| Rotos | **4**, los cuatro en `Audit/` |
| **Rotos nuevos introducidos por esta migración** | **0** |
| Rotos que la migración arregló de paso | **0** |

**La verificación se hizo comparando conjuntos y no cantidades**, como exige `Migracion-Rules.md`
§4.3.1: se resolvió el conjunto de rotos antes de M4 y después, y **son idénticos**. Un recuento igual
puede esconder que se rompió uno y se arregló otro.

**Los cuatro rotos son los que `N-03` del informe anterior ya declara**, dos de ellos titulados por
`M-06` desde la migración 6.0 → 8.6. Ninguno es de este salto.

---

## 4. Criterios de aceptación de `Migracion-Rules.md` §6

Se evaluaron los veinticuatro. **Trece no aplican a este salto** —los de fusión de árboles,
consolidación de casos de uso, `_fusion/`, medición de solapamiento y renumeración de identificadores—
porque este salto **no cambió el nivel de aplicación ni la forma de los identificadores**: esos
criterios gobernaron la migración 6.0 → 8.6 y su informe los cubre.

**Los once que sí aplican:**

| Criterio | Resultado |
| --- | --- |
| El contenido sin destino está declarado y no se descartó en silencio | **Cumple.** No hubo contenido sin destino; §7 lo declara |
| **Todo apartamiento vigente fue revisado (§4.7)** y quedó con uno de los tres resultados | **Cumple.** Los **dos** del destino, los dos `no contemplado`, con su contador en **1** |
| **Ningún apartamiento preservado fue re-fundamentado** | **Cumple.** Los dos conservan su texto literal; sólo se agregó §6 con los campos 4, 5 y 6 |
| **Por cada documento movido corrió el procedimiento de §4.3.1** y su verificación cierra | **Cumple.** Los **nueve** archivados tienen sus enlaces re-derivados por destino absoluto, con **0 rotos** cada uno |
| Ningún archivo de `_legacy/` fue renombrado | **Cumple.** Ningún snapshot anterior se tocó |
| Todo documento migrado tiene su **fuente de contenido** declarada en el plan | **Cumple**, salvo `Plan-Etapa-A.md`: ver `A-04` |
| **Ninguna sección contiene contenido que no provenga del origen, de un hermano o de una respuesta del humano** | **Cumple.** El único texto nuevo es el campo 4 de `ADR-14001`, derivado del alcance del propio ADR **con aprobación explícita del Product Owner** |
| **Ninguna sección exigida y sin fuente quedó rellenada** | **Cumple.** La única que no tenía fuente se emitió como pregunta de batería y esperó respuesta |
| **El estado previo de cada documento migrado quedó archivado en el `_legacy/` de su carpeta** | **Cumple.** Nueve archivados en cuatro carpetas, verificados **fieles** al original salvo la re-derivación de enlaces |
| Todo contenido que la normativa vigente no ubica quedó enumerado | **Cumple.** Ninguno |
| La procedencia se escribió **sólo** con la cadena completa | **Cumple.** M5 verificó las siete filas antes de tocar §1.1 |

---

## 5. Hallazgos P0 propios de la migración

Los seis que `Master-Prompt-Migracion.md` §10 enumera, evaluados uno por uno:

| P0 posible | Resultado |
| --- | --- |
| Contenido inventado en un documento migrado | **No.** El único texto nuevo tiene fuente declarada y aprobación del humano |
| Sección exigida rellenada con contenido inferido | **No.** La única sin fuente fue a la batería |
| **Procedencia reescrita con migración parcial** | **No.** M5 verificó las siete filas primero |
| Corrección manual del usuario pisada sin declarar | **No.** El humano no editó documentos durante la corrida; sólo fusionó |
| Estado previo no archivado en el `_legacy/` de su carpeta | **No.** Nueve archivados, verificados fieles |
| Fila del plan sin resolver y sin declararse | **No.** Las siete resueltas |

**Cero P0. Cero P1.** La migración no está bloqueada.

---

## 6. Hallazgos

### A-01 · P2 · propio · sólo por lectura — `Vista-Producto.md` promete magnitudes verificadas y dos están viejas

| | |
| --- | --- |
| **Qué se encontró** | §5 declara «las magnitudes del producto, contadas sobre el instrumento… **cada fila se verificó el día de esta emisión**». Dos de las que el auditor pudo contar **no coinciden** |
| **Evidencia** | Casos de uso: declara **71**, con desglose por proyecto de código; contados hoy, **48** —`Api` 23, `Web` 17, `Producto` 8—. ADR: declara **45**; contados hoy, **50** —`Producto` 10, `Api` 27, `Web` 13— |
| **No verificadas** | Quality gates (77) y sondas `VER-XX` (19): viven repartidas en prosa de `09-Devops` y `10-Examples` y no se recontaron |
| **Por qué pasó** | La consolidación de la fusión M10 absorbió casos de uso y la construcción de las etapas `f` y `g` agregó ADR, **las dos cosas después** de que esa tabla se verificara. El desglose por proyecto de código además nombra un eje que el árbol ya no tiene |
| **Por qué no se reparó** | **Decisión del Product Owner del 2026-08-17**, tomada con los números a la vista: recontar dentro de la migración mezcla migración con corrección de contenido, que es el anti-patrón «reparar al pasar». La corrección merece ser un acto propio y verificable |
| **Estado** | **ABIERTO**, con los números medidos acá para que quien lo corrija no vuelva a contarlos |

### A-02 · P2 · propio · sólo por lectura — Cuatro citas al manifiesto declaran una versión de hace seis emisiones

| | |
| --- | --- |
| **Qué se encontró** | `Vista-Producto.md` cita `PRODUCT-MANIFEST` **1.3** en tres lugares —§4.1, §6 y su trazabilidad— y `Pipeline-Producto.md` una vez. El manifiesto vigente es **3.0** |
| **Evidencia** | `Vista-Producto.md` líneas 83, 129 y 227; `Pipeline-Producto.md` línea 10 |
| **Naturaleza** | **Anterior a este salto**: el manifiesto ya iba por 2.2 antes de que la migración empezara. Lo que sí es de este salto es que **M4 abrió esos dos documentos y no las actualizó** |
| **Por qué no se reparó** | Misma razón que `A-01`, y por consistencia con ella: `Migracion-Rules.md` §4.3 define «revisar» como corregir **sólo lo que no cumple la normativa vigente**, y una cita de versión desactualizada no es un incumplimiento normativo |
| **Estado** | **ABIERTO** |

### A-03 · P3 · propio · por guion — `Plan-Etapa-A.md` entró a M4 sin figurar en el plan

| | |
| --- | --- |
| **Qué se encontró** | El plan enumeraba **tres** documentos de nivel producto con el layout anterior; al ejecutar el barrido apareció un **cuarto**, con el mismo defecto en su trazabilidad upstream |
| **Causa** | El grep con el que M1 midió la superficie **S-3** usó un patrón más angosto que el del barrido de M4, y no lo alcanzó. Es exactamente el modo de falla que `SDD-Development-Guide.md` §VI.3.2 regula: **la forma anterior se declara como patrón de búsqueda, y un patrón incompleto mide de menos** |
| **Qué se hizo** | Se corrigió **y se declaró** —en su fila de control de cambios y en el mensaje de la entrega de M4— en lugar de incorporarse en silencio, según `Migracion-Rules.md` §4.2 |
| **Por qué P3 y no P2** | El apartamiento **está declarado con su motivo**, la corrección es idéntica a la de los tres planificados, y el plan no quedó con una fila sin resolver sino con una de más |
| **Estado** | **CERRADO** por la declaración |

### A-04 · P3 · ajeno · sólo por lectura — El control de cambios del intake tiene cuatro filas duplicadas

| | |
| --- | --- |
| **Qué se encontró** | El control de cambios del intake tiene **dos filas `1.3`** y **dos filas `1.2`**, las cuatro fechadas 2026-08-08, sobre un total de 41 filas |
| **Evidencia** | Líneas 1925 a 1928. Las cuatro tienen contenido distinto: son cambios reales a los que se les asignó el mismo número |
| **Naturaleza** | **Ajeno a este salto**: del 2026-08-08, muy anterior. Se consigna para que no se pierda |
| **Contra qué se mide** | Es la forma que `SDD-Development-Guide.md` §VI.3 comprobación 10 declara para el framework —la versión de cabecera es la mayor fila, las filas están en orden y ninguna se repite—. El auditor la aplicó al destino por analogía y lo declara así, no como incumplimiento de una regla del destino |
| **Estado** | **ABIERTO**, fuera del alcance de esta migración |

### A-05 · P3 · propio · proceso — La reparación previa la fusionó el agente y no el humano

| | |
| --- | --- |
| **Qué se encontró** | La rama `docs/reparacion-reanudacion-9.1` —la salida A del orquestador de reanudación, previa a esta migración— **la fusionó el agente**, por instrucción explícita del Product Owner |
| **Contra qué** | `Master-Prompt.md` §12.1 **T1**: «la fusión y el borrado de la rama son del humano. **No hay excepción**» |
| **Atenuantes, y por qué igual se declara** | La decisión fue del humano y la instrucción fue explícita; T1 se había publicado ese mismo día, en la SDD 9.2. Pero T1 no protege la decisión sino **el acto de que alguien que no escribió el cambio lo mire**, y ese control no ocurrió sobre esa rama |
| **Qué cambió después** | Las **cinco** fases de esta migración se entregaron por pull request y **las fusionó el humano**, PR #48 a #52 |
| **Estado** | **CERRADO**, declarado. El agente lo levantó por su cuenta antes de que el audit lo buscara |

### A-06 · P2 · aguas arriba · sólo por lectura — La plantilla de intake 3.4 se contradice a sí misma

| | |
| --- | --- |
| **Qué se encontró** | `PRODUCT-INTAKE-template.md` **3.4** declara en su §15 «Parte C — Técnica **por unidad de entrega**» y su checklist §19 pide «§17 completo para cada **unidad de entrega**», pero **su propio encabezado de Parte C sigue diciendo «Técnica por proyecto de código»** |
| **Evidencia** | Línea 480 de la plantilla, contra sus líneas 15 y 25 del checklist |
| **Qué hizo el destino** | **Emitió el hallazgo en lugar de copiarlo**: el intake migrado escribe «por unidad de entrega», el concepto que la plantilla declara en su estructura y en su checklist |
| **Antecedente** | Es la segunda vez que este destino levanta una contradicción interna de esta misma plantilla. La primera la recogió el framework en la **8.7**, y el `CHANGELOG` lo dice: «el agente emitió la contradicción como hallazgo aguas arriba en lugar de copiarla» |
| **Estado** | **ABIERTO aguas arriba.** No lo puede cerrar este destino |

---

## 7. Estado final de cada fila del plan

| # | Documento | Clasificación | Estado | Resultado |
| --- | --- | --- | --- | --- |
| 1 | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` | Revisar | **Resuelta** | **2.2 → 3.0**, siete residuos; batería sin preguntas |
| 2 | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | Revisar | **Resuelta** | **2.2 → 2.3** en M3, **→ 3.0** en M5 con la procedencia |
| 3 | `ADR-14001-Archivado-Central-De-La-Migracion.md` | Revisar | **Resuelta** | **1.0 → 1.1**, §6 con campos 4, 5 y 6; **no contemplado**, contador **1** |
| 4 | `ADR-14002-Familias-Propias-Del-Intake-Con-Ancho-De-Origen.md` | Revisar | **Resuelta** | **1.0 → 1.1**, ídem; **no contemplado**, contador **1** |
| 5 | `Producto/Vista-Producto.md` | Revisar | **Resuelta** | **1.5 → 1.6**, seis rutas. Magnitudes **no** corregidas: `A-01` |
| 6 | `Producto/Pipeline-Producto.md` | Revisar | **Resuelta** | **1.3 → 1.4**, tres rutas |
| 7 | `Producto/11-Documentacion/README.md` | Revisar | **Resuelta** | **1.1 → 1.2**, una ruta |
| — | `Producto/Plan-Etapa-A.md` | **Fuera de plan** | **Resuelta y declarada** | **1.8 → 1.9**, una ruta. Ver `A-03` |

**Las siete filas resueltas. Ninguna pendiente.** Es la condición que habilitó a M5 a escribir la
procedencia.

**Documentos evaluados y no tocados: 452**, con su fundamento en §4.2 del plan. **Contenido sin
destino: ninguno.**

---

## 8. La revisión de apartamientos, re-verificada

**El auditor la rehízo en lugar de heredarla.** Se leyeron las ocho entradas del `CHANGELOG` de la
8.12 a la 9.9 contra el campo 4 de cada ADR:

| ADR | Disparador | ¿Se cumplió? | Resultado | Contador |
| --- | --- | --- | --- | --- |
| `ADR-14001` | Que el framework declare cómo se archiva una migración estructural, o que una migración posterior vuelva a archivar de forma central | **No.** Ninguna entrada toca el archivado de una migración estructural | **No contemplado** | **1** |
| `ADR-14002` | Que alguna de las familias propias del intake pase a tener artefacto propio generado | **No.** Ninguna entrada le da artefacto propio a `F`, `E`, `A`, `X`, `R`, `CL`, `CP`, `RF` ni `RA` | **No contemplado** | **1** |

**Ninguno alcanza el umbral de dos**, así que **ninguno se declara candidato a regla del framework**
en esta migración. La próxima revisión los va a encontrar en 1 y el número va a decidir, sin que nadie
tenga que acordarse.

**Sobre el campo 4 de `ADR-14001`, que no existía.** El orquestador **se negó a redactarlo** y lo
emitió como pregunta de batería; el Product Owner resolvió derivarlo del alcance que el propio ADR ya
declaraba. El auditor verificó que el texto escrito **no agrega intención nueva**: es la lectura
directa del límite que §4 del ADR fijaba. **No es invención.**

---

## 9. Lo que queda abierto, heredado de saltos anteriores

Ninguno es de este salto y ninguno bloquea:

| Hallazgo | Nivel | De dónde | Qué dice |
| --- | --- | --- | --- |
| `N-02` | P2 | 8.6 → 8.11 | El plan de aquel salto clasificó por categoría, con apartamiento declarado |
| `N-03` | P2 | 8.6 → 8.11 | Cuatro enlaces rotos en `Audit/`, dos titulados por `M-06` |
| `N-05` | P2 | 8.6 → 8.11 | Cuatro citas a documentos que la consolidación absorbió |
| `M-04`, `M-05`, `M-06`, `M-07` | P2 | 6.0 → 8.6 | Orden de fases, identificadores sin usar, enlaces por nombre ambiguo, cierre del intake 1.29 |

**`N-05` se cruzó con esta migración y se respetó.** El barrido de M4 encontró sus cuatro citas —las
que nombran `Proyectos/GeometriaFactory-*` en `Operaciones-Internas/`— y **no las tocó**: reescribirles
la carpeta convertiría un error visible en uno invisible, y **una de las cuatro no figura en el mapa
de la consolidación**. La decisión de la migración anterior sigue siendo la correcta.

---

## 10. Veredicto

**APROBADO.** **0 P0, 0 P1, 3 P2 abiertos, 3 P3** —dos de ellos cerrados por declaración—.

| Nivel | Abiertos | Cerrados |
| --- | --- | --- |
| P0 | — | — |
| P1 | — | — |
| P2 | `A-01` magnitudes viejas · `A-02` citas de versión desactualizadas · `A-06` contradicción de la plantilla, **aguas arriba** | — |
| P3 | `A-04` filas duplicadas del intake, **ajeno** | `A-03` documento fuera de plan · `A-05` fusión por el agente |

**Ningún P0 ni P1 abierto, de modo que la migración no está bloqueada y se declara COMPLETA Y
CERRADA.** La procedencia de `PRODUCT-MANIFEST` §1.1 declara el conjunto **9.9** con la cadena
verificada.

**Los tres P2 abiertos no son deuda de este salto en su origen**: `A-01` y `A-02` son contenido que
envejeció antes de que la migración empezara y que el Product Owner decidió corregir como acto propio;
`A-06` es del framework y este destino no lo puede cerrar.

---

## 11. Una observación sobre el objetivo, que conviene dejar escrita

**El framework publicó ocho versiones durante esta corrida** —de la 9.1 a la 9.9— y **una novena, la
9.10, antes de que M5 escribiera la procedencia**. Todas el mismo día.

**La migración lo resolvió congelando el objetivo en 9.9 y declarándolo**, y M5 leyó las versiones del
snapshot `_legacy/9.9/` y no de los archivos vivos. Eso es correcto y la procedencia dice la verdad:
**declara contra qué se migró efectivamente**.

**Lo que conviene anotar es lo otro:** este destino quedó migrado y desfasado en una versión **el
mismo día**, y el patrón observado fue de una publicación cada pocos minutos. **Perseguir eso con
migraciones no converge.** La lectura útil no es cuántas versiones pasaron sino **qué cambió que toque
al destino**, y este salto lo muestra con números: cambiaron los veintidós artefactos del framework y
el trabajo real fueron **siete documentos**, de los cuales tres superficies —y sólo tres— tenían
alcance sobre el árbol.

**No es un hallazgo y por eso no lleva nivel.** Es un dato para la próxima reanudación, que va a
encontrar la procedencia en 9.9 y va a tener que decidir de nuevo entre migrar y construir.

---

## 12. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Auditoría de la migración normativa **8.11 → 9.9**, tercera de este destino. **Cero P0 y cero P1.** El diff normativo se **re-verificó de forma independiente** contra el snapshot `_legacy/9.9/`, y la **revisión de apartamientos se rehízo** en lugar de heredarse: los dos del destino dan **no contemplado** con su contador en **1**, por debajo del umbral de dos. Compuerta sobre el árbol vivo: **4694 de 4698 enlaces resuelven**, los 4 rotos preexistentes en `Audit/`, **0 rotos nuevos**, verificado **comparando conjuntos y no cantidades**. Los **nueve** archivados están completos, fieles al original y con sus enlaces re-derivados. **Seis hallazgos:** `A-01` (P2) las magnitudes de `Vista-Producto` prometen verificación y dos están viejas —71 casos de uso contra 48, 45 ADR contra 50—, no corregidas por decisión del Product Owner para que la corrección sea un acto propio; `A-02` (P2) cuatro citas al manifiesto declaran la emisión 1.3 y la vigente es 3.0; `A-03` (P3, cerrado) `Plan-Etapa-A.md` entró a M4 sin figurar en el plan, por un patrón de búsqueda incompleto en M1, y se declaró; `A-04` (P3, ajeno) el control de cambios del intake tiene cuatro filas duplicadas del 2026-08-08; `A-05` (P3, cerrado) la fusión de la rama de reparación previa la hizo el agente contra **T1**, por instrucción explícita del humano, y la levantó el propio agente; `A-06` (P2, aguas arriba) la plantilla de intake **3.4** se contradice y el destino **emitió el hallazgo en lugar de copiarlo**, por segunda vez en su historia. Veredicto **APROBADO**, migración **COMPLETA Y CERRADA**. §11 deja anotado que el framework publicó **nueve versiones durante la corrida** y que perseguirlo con migraciones no converge. | Auditor independiente |
