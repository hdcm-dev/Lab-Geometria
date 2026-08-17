# Plan de migración normativa — 8.11 → 9.9

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-8.11-a-9.9.md
**Versión:** 1.0
**Estado:** Propuesto — **esperando la aprobación de la detención de M1**
**Fecha:** 2026-08-17
**Autor:** Orquestador de migración normativa SDD
**Responsable de mantenerlo:** el orquestador de migración que lo emite; lo cierra el informe de M6
**Instrumento normativo:** `Master-Prompt-Migracion.md` **2.7** §5, con `Migracion-Rules.md` **3.7**
**Origen:** SDD **8.11** · **Vigente:** SDD **9.9**
**Conjunto de origen:** **disponible** en `_legacy/8.11/` del repositorio del framework
**Invocado desde:** `Master-Prompt-Reanudacion.md`, con el informe `Estado-Del-Destino-2026-08-17.md`

---

## 1. Qué es este plan y de dónde sale

Es la salida de la fase **M1**. Llega invocado desde la reanudación, de modo que —por
`Master-Prompt-Migracion.md` §5— **no reconstruye el diff normativo: lo verifica**. Reconstruirlo no
lo haría más confiable; lo haría más lento, y arriesgaría dos diffs del mismo salto que no coinciden.

**Y lo verificó contra un objetivo que se movió.** El informe de la reanudación midió el diff contra
el conjunto **9.1**. Entre esa medición y esta fase el framework publicó **ocho versiones** —9.2 a
9.9, todas del 2026-08-17—, de modo que **el objetivo real de esta migración es 9.9**. Lo que la
verificación encontró está en §2 y §3; el resumen es que la conclusión de fondo del informe **se
sostiene**, y que **reapareció una superficie que el informe declaraba cerrada**.

---

## 2. Verificación del diff normativo, artefacto por artefacto

**Método.** Se leyó la versión que declara hoy la cabecera de cada archivo del framework y se comparó
contra la tabla de procedencia de `PRODUCT-MANIFEST` **2.2** §1.1. No se dedujo nada del número de
conjunto.

### 2.1 Tabla completa

| Artefacto del framework | Procedencia (8.11) | Vigente (9.9) | Cambió | Severidad para este destino |
| --- | --- | --- | --- | --- |
| `Master-Prompt` | 7.7 | **8.4** | Sí | **Nula sobre artefactos.** Tres bloques en §3.4, §12.1 el traspaso, §8.1 la forma de la detención |
| `Master-Prompt-Migracion` | 2.3 | **2.7** | Sí | **Nula sobre artefactos.** Es el instrumento que ejecuta este plan |
| `Master-Prompt-Reanudacion` | 1.1 | **1.6** | Sí | **Nula sobre artefactos.** Es el instrumento que produjo el informe de estado |
| `Root-Rules` | 5.2 | **6.1** | Sí | **ALTA. Alcanza los dos apartamientos declarados.** Ver §3 |
| `Rules-Contexto` | 4.1 | **4.3** | Sí | **Nula.** La cabecera de §4.1 ya se reparó |
| `Rules-Necesidades-Negocio` | 4.0 | **4.2** | Sí | **Nula.** Ídem |
| `Rules-Especificacion-Funcional` | 5.0 | **5.3** | Sí | **Nula.** Ídem |
| `Rules-UX-UI-DX` | 5.0 | **5.3** | Sí | **Nula.** Ídem |
| `Rules-Arquitectura-Tecnica` | 4.0 | **4.3** | Sí | **Nula.** La cabecera y el renombre del artefacto ya se repararon |
| `Rules-Backlog-Tecnico` | 4.0 | **4.3** | Sí | **Nula.** Ídem |
| `Rules-Plan-Sprint` | 5.0 | **5.3** | Sí | **Nula.** Ídem |
| `Rules-Calidad-Y-Pruebas` | 4.1 | **4.4** | Sí | **Nula.** Ídem |
| `Rules-Devops` | 4.0 | **4.5** | Sí | **Nula.** Los ítems 7 y 8 de §4.3 ya se escribieron |
| `Rules-Examples` | 6.0 | **6.3** | Sí | **Nula.** Ídem |
| `Rules-Documentacion` | 5.0 | **5.3** | Sí | **Nula.** Ídem |
| `Intake-Rules` | 4.0 | **4.1** | Sí | **Nula.** El mapeo en tres tablas describe lo que el manifiesto ya declara |
| `Vocabulario-Rules` | 3.0 | **3.1** | Sí | **Nula.** Glosario y cita del despacho |
| `Maqueta-Rules` | 4.0 | **4.2** | Sí | **Nula.** La Fase B2 está confirmada y cerrada; el paso 5 de la 4.2 gobierna una maqueta en curso |
| `Deriva-Rules` | 5.0 | **5.2** | Sí | **Nula.** Ruta de salida del prompt de despacho |
| `Migracion-Rules` | 3.4 | **3.7** | Sí | **Gobierna esta corrida.** §4.7 nueva, la revisión de apartamientos; §4.3.1 el procedimiento de mover un documento |
| `PRODUCT-INTAKE-template` | 3.1 | **3.4** | Sí | **MEDIA. Alcanza §16 del intake.** Ver §4 |
| `PRODUCT-MANIFEST-template` | 5.0 | **6.0** | Sí | **Nula.** El campo del principal ya está en su forma vigente, verificado |

**Cambió todo.** Es el renglón inverso al del salto anterior, donde ninguna regla de categoría se
había movido.

### 2.2 Qué de todo eso llega a un artefacto del destino

**Las once reglas de categoría aplicadas cambiaron de versión y ninguna genera trabajo**, y el motivo
es verificable: lo que traían para el destino —la cabecera de nivel de su §4.1 y el renombre del
artefacto de `05`— **se reparó el 2026-08-17**, antes de esta migración, por la salida A de la
reanudación. Los commits `560d348`, `84e53f6` y `9c11b62` son la evidencia, y §8 del informe de
estado su verificación.

**Quedan tres superficies vivas**, y las tres nacieron después de que el informe midiera:

| # | Superficie | De qué versión viene | Alcance medido |
| --- | --- | --- | --- |
| **S-1** | Los apartamientos declarados necesitan **estado** y **contador de saltos** | `Root-Rules` 6.1, por SDD **9.7** | **2 ADR** |
| **S-2** | El árbol de §16 del intake declara el **layout anterior a la 8.0** | `PRODUCT-INTAKE-template` 3.4, por SDD **9.0** | **1 documento**, 1 ocurrencia |
| **S-3** | Documentos de nivel producto que ubican artefactos bajo `Proyectos/` | La misma, por coherencia con `Root-Rules` §2.1 | **3 documentos**, 10 ocurrencias |

### 2.3 Renombres de artefacto aplicables

**Ninguno pendiente.** Se leyeron los bloques «Impacto sobre destinos existentes» de las entradas
major atravesadas por el salto: entre 8.12 y 9.9 hay **una sola** —la **9.0**— y sus tres renombres
están resueltos:

| Renombre de la 9.0 | Estado en el destino |
| --- | --- |
| `Proyecto de código principal` → `Unidad de entrega principal` | **Ya conforme**, en `PRODUCT-MANIFEST` §1 y en `SDD/Docs/README.md` 1.6 |
| `proyecto-de-codigo-principal` → `unidad-de-entrega-principal` | Sin superficie: es campo del bloque informativo del orquestador |
| `orden-topologico` → `orden-topologico-de-compilacion` + `orden-de-integracion` | Sin superficie: el corpus cita «orden topológico» como prosa, no como nombre de campo |

Y el renombre de artefacto de la **8.17** —`Arquitectura-Proyecto-Codigo.md` →
`Arquitectura-Unidad-Entrega.md`— **ya se aplicó**, con sus 284 referencias reconectadas por destino.

**Se comprobó por lectura del `CHANGELOG` y no por ausencia de noticia**, que es lo que
`Migracion-Rules.md` §111 exige: un renombre es el único cambio que ningún diff de versiones infiere.

---

## 3. Revisión de apartamientos · `Migracion-Rules.md` §4.7

**Es la fase que la 9.7 agregó a M1**, y la primera vez que este destino la atraviesa. Se revisa cada
apartamiento de `Root-Rules.md` §11 contra la normativa vigente, y **el insumo no es una
interpretación**: es el campo 4 del propio ADR, los disparadores que superarían la decisión.

**El destino tiene exactamente dos**, tipados como `Apartamiento declarado (Root-Rules.md §11)`:

| ADR | Qué aparta | Disparador declarado | Resultado | Contador |
| --- | --- | --- | --- | --- |
| **`ADR-14001`** · El archivado de la migración es central y no por carpeta | El criterio de archivado por carpeta de `Migracion-Rules.md` §6 | **No lo declara como tal.** Lo más cercano es su «Alcance del apartamiento»: cubre **la migración 6.0 → 8.6 y sólo esa** | **NO CONTEMPLADO**, con salvedad — ver §3.1 | **1** |
| **`ADR-14002`** · Las familias propias del intake conservan su ancho de origen | El ancho de cinco dígitos de `Root-Rules.md` §9.2 | **Sí**, en su §4 bajo el título «Qué lo reabre»: que alguna de esas familias **pase a tener artefacto propio generado** | **NO CONTEMPLADO** | **1** |

**Ninguno resultó absorbido ni contradicho.** Se verificó entrada por entrada de 8.12 a 9.9: ninguna
toca el criterio de archivado de una migración estructural, y ninguna le da artefacto propio generado
a las familias `F`, `E`, `A`, `X`, `R`, `CL`, `CP`, `RF` ni `RA`. **La vigente sigue sin decir nada de
los dos casos**, que es la definición de «no contemplado».

**Los dos se preservan con su texto literal**, incluido su fundamento. Reescribirlo contra la
normativa nueva produciría un ADR que dice haber decidido algo que en su fecha nadie decidió, y §4.1
lo prohíbe por la misma razón por la que no se rellena una sección sin fuente.

**Contador en 1, y qué significa.** Es el primer salto que cualquiera de los dos atraviesa con la
revisión existiendo. El umbral del framework es **dos o más**: recién ahí un apartamiento se declara
candidato a regla del framework. **Ninguno lo alcanza todavía**, y el número lo va a reportar solo la
próxima vez.

### 3.1 La salvedad de `ADR-14001`, que va a la batería

**`ADR-14001` no declara el campo 4 de `Root-Rules.md` §11**, los disparadores concretos que
superarían la decisión. Tiene contexto, decisión, motivo, consecuencias y alternativas descartadas —
los campos 1, 2, 3 y parcialmente el 4 por vía de su alcance— pero **no una condición de superación
escrita como tal**.

**No se inventa.** Escribirle un disparador plausible sería exactamente la invención que
`Migracion-Rules.md` §4.1 tipifica como P0. Va a la batería de M1 como pregunta al Product Owner, y
mientras no se resuelva el ADR se clasifica **no contemplado** con su contador en 1, que es lo
conservador: preserva la decisión y no la da por superada.

---

## 4. Tabla de documentos

**Clasificación por documento y no por categoría.** El salto anterior clasificó por categoría y eso
quedó registrado como el hallazgo `N-02`; acá la clasificación se deriva **documento a documento** de
una pregunta mecánica —¿cambió de versión la regla que lo gobierna, y ese cambio alcanza su forma?—
cuyo resultado es uniforme para el grueso del corpus y se declara con su fundamento.

### 4.1 Documentos que entran al plan

| # | Path | Regla que lo gobierna | Qué cambió que lo toca | Clasificación | Fuente de contenido |
| --- | --- | --- | --- | --- | --- |
| 1 | `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` | `PRODUCT-INTAKE-template` 3.4 | **S-2**: el árbol de §16 declara «categorías 00-11 (por proyecto de código bajo `Proyectos/`)», el layout anterior a la 8.0 | **Revisar** (M2) | Documento de origen |
| 2 | `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | `PRODUCT-MANIFEST-template` 6.0 | Re-derivación desde el intake migrado, y la procedencia en M5 | **Revisar** (M3 y M5) | Derivado del intake |
| 3 | `SDD/Docs/Producto/Adrs/ADR-14001-Archivado-Central-De-La-Migracion.md` | `Root-Rules` 6.1 §11 | **S-1**: faltan los campos **5** (estado) y **6** (saltos sobrevividos), y el **4** nunca estuvo | **Revisar** | Documento de origen + **pendiente humano** (§3.1) |
| 4 | `SDD/Docs/Producto/Adrs/ADR-14002-Familias-Propias-Del-Intake-Con-Ancho-De-Origen.md` | `Root-Rules` 6.1 §11 | **S-1**: faltan los campos **5** y **6**; el 4 está bajo el título «Qué lo reabre» | **Revisar** | Documento de origen |
| 5 | `SDD/Docs/Producto/Vista-Producto.md` | `Root-Rules` §2.1 | **S-3**: **6 ocurrencias** de `Proyectos/<Nombre>/`, en §35, §69 y §77 | **Revisar** | Documento de origen |
| 6 | `SDD/Docs/Producto/Pipeline-Producto.md` | `Root-Rules` §2.1 | **S-3**: **3 ocurrencias**, la de §198 en la tabla de ausencia de guía | **Revisar** | Documento de origen |
| 7 | `SDD/Docs/Producto/11-Documentacion/README.md` | `Rules-Documentacion` 5.3 | **S-3**: **1 ocurrencia**, en el detalle por proyecto de código | **Revisar** | Documento de origen |

**Siete documentos entran al plan. Ninguno se regenera**: los siete se revisan, que es corregir sólo
lo que no cumple la normativa vigente, preservando el resto.

### 4.2 Documentos que no entran, con su fundamento

| Conjunto | Cantidad | Clasificación | Por qué |
| --- | --- | --- | --- |
| El resto de `SDD/Docs/` vivo | **452** | **No tocar** | La regla que gobierna cada uno cambió de versión, y **lo que ese cambio traía para el destino ya está aplicado**: la cabecera de nivel de §4.1 en los 313, y el renombre del artefacto de `05`. Verificado con residuo cero el 2026-08-17 |
| `SDD/Docs/Handoff-Checkout.md` | 1 | **Fuera de alcance** | Está **declarado superado** por la migración 6.0 → 8.6, con su cartel y su tabla de dónde está hoy lo que inventariaba. Sus **11 ocurrencias** de `Proyectos/` son parte de lo que el documento declara viejo. Migrarlo a medias lo volvería falso, que es el fundamento del hallazgo `M-08` |
| `SDD/Docs/Audit/*` | 68 | **Fuera de alcance** | Registros fechados. Sus **20 documentos** con `Docs/Proyectos/` describen dónde estaba el árbol cuando se auditó |
| `SDD/Docs/**/_legacy/**` | — | **Fuera de alcance** | Snapshots, por `Migracion-Rules.md` §2.2 |

**Total del destino: 459 documentos vivos.** 7 al plan, 452 no tocar, más el intake y el manifiesto.

### 4.3 Degradación declarada

**No aplica.** El destino declara su bloque de procedencia y el conjunto de origen es reconstruible
desde `_legacy/8.11/`, de modo que la clasificación de saltos es **por severidad** y no degradada a
«revisar todo».

---

## 5. Lo que este plan NO hace

- **No toca la procedencia.** Es trabajo de **M5**, y sólo si la cadena queda completa.
- **No resuelve la salvedad de `ADR-14001`.** La lleva a la batería.
- **No repara lo que ya se reparó.** Las tres divergencias de la salida A están cerradas y verificadas;
  este plan las cita como evidencia, no las rehace.
- **No persigue al framework.** El objetivo se congela en **9.9**, el conjunto vigente al momento de
  emitir este plan. Si el framework publica durante la corrida, **este plan no se reabre**: la
  procedencia que M5 escriba va a decir 9.9, que es contra lo que efectivamente se migró, y eso sigue
  siendo cierto aunque exista una 9.10.

---

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Fase **M1** de la migración **8.11 → 9.9**, invocada desde la reanudación: el diff normativo **se verificó** en lugar de reconstruirse, y la verificación encontró que **el objetivo se había movido de 9.1 a 9.9** durante la sesión. **Cambiaron los veintidós artefactos del framework salvo ninguno**, y aun así el trabajo sobre el destino es de **siete documentos**, porque lo que las once reglas de categoría traían **ya se reparó** por la salida A de la reanudación. Tres superficies vivas: **S-1**, los dos apartamientos sin estado ni contador, por `Root-Rules` 6.1; **S-2**, el árbol de §16 del intake con el layout anterior a la 8.0; y **S-3**, tres documentos de nivel producto que ubican artefactos bajo `Proyectos/`. Primera **revisión de apartamientos** de este destino (`Migracion-Rules.md` §4.7): los dos resultan **no contemplados**, se preservan con su texto literal y su contador queda en **1**, por debajo del umbral de dos que los volvería candidatos a regla del framework. **Cero renombres pendientes**, comprobados por lectura del `CHANGELOG` sobre el único major con impacto del salto, la 9.0. Una salvedad a la batería: **`ADR-14001` no declara el campo 4** de `Root-Rules.md` §11 y no se le inventa uno. | Orquestador de migración normativa SDD |
