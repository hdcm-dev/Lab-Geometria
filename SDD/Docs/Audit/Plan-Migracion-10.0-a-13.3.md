# Plan de migración normativa — SDD 10.0 → 13.3

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-10.0-a-13.3.md
**Versión:** 1.1
**Fecha:** 2026-08-23
**Instrumento:** `Master-Prompt-Migracion.md` **2.8**, fase **M1**
**Estado:** **Detención obligatoria.** El plan se presenta completo y espera aprobación. **Ningún documento se modifica durante M1**
**Decisión que lo origina:** salida **B** de `Estado-Del-Destino-2026-08-23.md`, tomada por el Product Owner el 2026-08-23, en la **segunda vuelta** de la salida `A`

---

## 1. Cabecera

| Campo | Valor |
|---|---|
| Destino | `Lab-Geometria`, producto **Fábrica de Geometría** |
| Versión de origen | **SDD 10.0**, declarada en `PRODUCT-MANIFEST` **4.0** §1.1 |
| Versión vigente | **SDD 13.3**, `CHANGELOG.md` del framework, 2026-08-23 |
| Conjunto de origen | **Disponible** en `_legacy/10.0/` del framework. Verificado: `Root-Rules` **7.0**, `Rules-Backlog-Tecnico` **4.4** y `Rules-Devops` **5.0** coinciden con la procedencia declarada |
| Clasificación de saltos | **Por severidad.** No degradada: el destino declara procedencia |
| Documentos vivos en `SDD/Docs/` | **497** (898 con `_legacy/`) |
| Migración número | **Séptima** de este destino |

**El diff normativo no se reconstruyó: se verificó.** `Master-Prompt-Migracion.md` §5 lo declara
explícitamente para las invocaciones que llegan desde la reanudación — el diff artefacto por artefacto
vive en `Estado-Del-Destino-2026-08-23.md` §6, y M1 **lo comprobó contra las versiones vigentes** en
lugar de rehacerlo. Reconstruirlo no lo haría más confiable: arriesgaría dos diffs del mismo salto que
no coinciden.

---

## 2. Tabla de saltos por artefacto

| Artefacto del framework | Origen 10.0 | Vigente 13.3 | Severidad | Clasificación que induce |
|---|---|---|---|---|
| `Root-Rules` | **7.0** | **8.4** | **MAJOR** | Regenerar contenido en lo que gobierna |
| `Rules-Backlog-Tecnico` | **4.4** | **5.1** | **MAJOR** | Regenerar contenido en la 06 |
| `Rules-Devops` | **5.0** | **6.1** | **MAJOR** | Regenerar contenido en la 09 |
| `Rules-Contexto` | 4.4 | 4.5 | minor | Revisar |
| `Rules-Necesidades-Negocio` | 4.3 | 4.4 | minor | Revisar |
| `Rules-Especificacion-Funcional` | 5.4 | 5.5 | minor | Revisar |
| `Rules-UX-UI-DX` | 5.4 | 5.5 | minor | Revisar |
| `Rules-Arquitectura-Tecnica` | 4.4 | 4.5 | minor | Revisar |
| `Rules-Plan-Sprint` | 5.4 | 5.5 | minor | Revisar |
| `Rules-Calidad-Y-Pruebas` | 4.5 | 4.6 | minor | Revisar |
| `Rules-Examples` | 6.4 | 6.5 | minor | Revisar |
| `Rules-Documentacion` | 5.4 | 5.5 | minor | Revisar |
| `Intake-Rules` | 4.1 | 4.2 | minor | Revisar el intake |
| `Vocabulario-Rules` | 3.1 | 3.2 | minor | Revisar |
| `Maqueta-Rules` | 4.3 | 4.4 | minor | Revisar |
| `Deriva-Rules` | 5.3 | 5.4 | minor | Revisar |
| `Migracion-Rules` | 3.15 | 3.19 | minor | **Gobierna esta corrida**, no un documento del destino |
| `Master-Prompt` | 8.8 | 8.12 | minor | Proceso |
| `Master-Prompt-Migracion` | 2.8 | 2.8 | — | Sin cambio |
| `Master-Prompt-Reanudacion` | 1.8 | 1.9 | minor | Proceso |
| `Catalogo-De-Criterios` | 1.6 | 1.13 | minor | Índice: no gobierna ningún artefacto |
| `Rules-Base-Conocimiento` | no listado | 2.0 | — | **No alcanza**: no hay orquestador que la lea y las dos unidades tienen `usa_llm == false` |
| `PRODUCT-INTAKE-template` | 3.4 | 3.4 | — | Sin cambio |
| `PRODUCT-MANIFEST-template` | 6.0 | 6.0 | — | Sin cambio |

**Tres major, y es la primera vez que este destino atraviesa más de uno en un salto.** Los tres
bloques «Impacto sobre destinos existentes» que importan son los de la **11.0** y la **12.0**; el de la
**13.0** dice *«Ninguno. Ningún destino tiene trabajo»*.

---

## 3. Renombres de artefacto aplicables

**Leídos de los bloques de impacto del `CHANGELOG.md`, que es lo único que los declara.**

| De la versión | Renombres | Naturaleza | Qué toca en este destino |
|---|---|---|---|
| **11.0** | **Ninguno.** Su tabla está declarada vacía | — | Nada |
| **12.0** | **Catorce**, la familia `AG` completa: `AG-00` → `AG-00000`, `AG-01` → `AG-00010`, `AG-02` → `AG-00020`, `AG-03` → `AG-00030`, `AG-03M` → **`AG-00031`**, `AG-04` → `AG-00040`, `AG-05` → `AG-00050`, `AG-06` → `AG-00060`, `AG-07` → `AG-00070`, `AG-08` → `AG-00080`, `AG-09` → `AG-00090`, `AG-10` → `AG-00100`, `AG-11` → `AG-00110`, `AG-ROOT` → **`AG-00990`** | **Identificador**, no archivo ni carpeta | La cita del rol en el mapa de documentación. Ver §4.3 |
| **13.0** | Ninguno | — | Nada |

**Ningún archivo ni carpeta de este destino cambia de nombre.** Los catorce son renombres de
identificador, y el mapeo se lee al derecho para migrar y al revés para reconocer la forma vieja.

**Y lo que este destino NO renumera, declarado antes de que alguien lo cuente.** La forma `AG-NN`
aparece **550 veces en 375 archivos vivos**. `Migracion-Rules.md` **§4.3.1** declara que la
renumeración de una familia del conjunto normativo **la hace el framework y no el destino**, y acota
el trabajo del destino a **reemplazar la cita en su mapa de documentación**. Las 550 ocurrencias son
el tamaño del patrón, no el del trabajo.

---

## 4. Las tres superficies del salto, medidas sobre el árbol

### 4.1 Estimación como ítem propio · **superficie 144 archivos**

**De la 11.0**, `Rules-Backlog-Tecnico.md` §4.4: el punto 5 se parte en **5 · prioridad**, que es del
Product Owner, y **5.b · estimación**, que es del equipo y sale del refinamiento.

| Medida | Valor |
|---|---|
| Archivos `US-XXXXX-<Nombre>.md` vivos | **144** — 114 en `GeometriaFactory-Api`, 30 en `GeometriaFactory-Web` |
| Con la sección empaquetada `## 5. Prioridad y estimación` | **144**, todos |
| Texto vivo de la estimación | **«Estimación: sin fijar»**, remitiendo a `Product-Backlog.md` §4.1 |

**Lo que esta superficie esconde, y hay que decirlo antes de aprobar el plan.** Partir la sección es
mecánico. Lo que no lo es: el bloque de impacto de la 11.0 declara **hallazgo P1 si la estimación
queda diferida sin la forma de `Root-Rules.md` §12.2**, y hoy está diferida **en prosa**, por
remisión. Cerrarla exige una de dos cosas, y las dos son del Product Owner:

- **Fijar la unidad de estimación** —que es la decisión `D2` de `A3-Decisiones-Del-Product-Owner.md`,
  abierta— y estimar, o
- **Diferirla con los cuatro campos de §12.2**, nombrando el artefacto y la sección donde se cierra.

**Es la misma decisión, y por eso conviene tomarla acá y no dos veces.** `D2` estaba en la cola del
frente `A` del plan de cierre de pendientes; la 11.0 la vuelve obligatoria en 144 documentos.

### 4.2 Los cuatro ítems `.b` de la 09 · **superficie 6 documentos**

**De la 11.0**, `Rules-Devops.md`. Cada ítem que empaquetaba dos decisiones se parte, y la mitad que
no estaba bloqueada deja de arrastrar a la que sí.

| Ítem que se parte | Documento alcanzado | Fuente de contenido (§2.1) | Estado medido en el árbol |
|---|---|---|---|
| §4.3 punto 5.b · **semántica de sufijos** `-alpha`, `-beta`, `-rc` | `Estrategia-Versionado.md` de las **dos** unidades | **Documento de origen**: las subsecciones de §5 ya declaraban que no se usan | A verificar en M4 |
| §4.4 punto 2.b · **aprobación de `plan` antes de `apply`** | `Entornos-Deploy.md` de las **dos** unidades | **Documento de origen**: la sección de provisión ya declaraba que no hay herramienta declarativa | A verificar en M4 |
| §4.6 punto 1.b · **generador de SBOM**, separado del formato | `Supply-Chain-Seguridad.md` de las **dos** unidades | **Respuesta del humano** para el formato —ninguna fuente del producto lo declara— y **documento de origen** para el resto | A verificar en M4 |
| §4.6 punto 5.b · **DAST**, separado de SAST | `Supply-Chain-Seguridad.md` de las **dos** unidades | **Documento de origen** y **documento hermano**: `Pipeline-CI-CD.md` §10 de la propia unidad, del que salen los stages y el estado de los puntos abiertos | A verificar en M4 |

**La columna de fuente de contenido es la que vuelve verificable a §4.1 fila por fila**, y la emisión
1.0 de este plan **no la traía**: lo levantó el audit independiente del corte 09 como hallazgo **P2**,
y su ausencia dejó a la ronda 1 sin el instrumento contra el que se comprueba que nada se inventó.

**El precedente ya está en el árbol y conviene seguirlo.** `Estrategia-Versionado.md` de
`GeometriaFactory-Api` **ya tiene su §3.b** —el prefijo de etiqueta como ítem propio, fijado el
2026-08-18 por la migración anterior—, y su forma es la que estos cuatro deben adoptar.

### 4.3 La cita del rol con la forma nueva · **superficie 1 documento, 5 filas**

**De la 12.0**, exigida por `Root-Rules.md` **§4.4** —la Tabla A del README raíz y su columna
`Responsable`— con la **forma** que fija §9.2.

| Documento | Filas alcanzadas |
|---|---|
| `SDD/Docs/README.md` | **5** en el mapa de documentación: `AG-00`, `AG-01`, `AG-05, AG-09, AG-11`, y dos filas con el rango `AG-02 a AG-11`. Más la cita `AG-ROOT` en la fila 1.0 de su control de cambios |

**El archivo es uno por producto, no por unidad de entrega**: `Root-Rules.md` §1.2 declara que el
README raíz se genera una vez a nivel producto.

**Una constancia sobre el rango.** Dos filas escriben `AG-02 a AG-11`, que no es un identificador sino
**una forma abreviada de enumerar once**. M4 la resuelve enumerando o reescribiendo el rango con la
forma nueva, y **la decisión se declara en la fila del documento**: convertir un rango en una lista
cambia lo que el documento dice, aunque el conjunto sea el mismo.

---

## 5. Revisión de apartamientos (`Migracion-Rules.md` §4.7)

**El insumo es el campo 4 del propio ADR, no una interpretación.** La pregunta es si la normativa
vigente cumple el disparador que cada apartamiento declaró.

| ADR | Disparador declarado (campo 4) | ¿La 13.3 lo cumple? | Resultado | Contador |
|---|---|---|---|---|
| **`ADR-14001`** · Archivado central de la migración | Que el framework declare **cómo se archiva una migración estructural**, o que una migración posterior de este destino **vuelva a archivar de forma central** | **No.** Ninguna entrada de la 10.1 a la 13.3 declara el archivado de una migración estructural | **No contemplado** | **1 → 3** |
| **`ADR-14002`** · Familias propias del intake con ancho de origen | Que alguna de esas familias **pase a tener artefacto propio generado** | **No.** La 12.0 toca `Root-Rules` §9, pero **no le da artefacto propio** a `F`, `E`, `A`, `X`, `R`, `CL`, `CP`, `RF` ni `RA` | **No contemplado** | **1 → 3** |
| **`ADR-14003`** · Dirección del backend por IP dinámica | **IP pública estática** o **nombre DDNS** en `API_BASE_URL` | **No.** Es infraestructura del destino; ninguna versión del framework la toca | **No contemplado** | **0 → 2** |

### 5.1 El contador debe **dos** saltos, no uno, y hay que decir por qué

**La migración 9.12 → 10.0 no corrió esta revisión.** Ni su plan ni su informe mencionan la palabra
«apartamiento»: `ADR-14003` todavía declara en su campo 6 *«ninguna migración lo revisó todavía»*,
y fue emitido en el conjunto **9.12**, de modo que el salto a la 10.0 le pasó por encima sin contarse.

**Por eso los tres incrementos propuestos son de +2 y no de +1**: el salto **9.12 → 10.0**, no contado
en su momento, y el salto **10.0 → 13.3**, que es éste. **No es una corrección retroactiva del informe
de aquella migración** —un informe emitido no se reescribe— sino el reconocimiento de que el contador
mide saltos sobrevividos y el destino sobrevivió los dos.

**Y es la consecuencia que más importa de esta fase.** Con el incremento, **los tres apartamientos
cruzan el umbral de dos o más saltos**, y `Migracion-Rules.md` §4.7 dice qué significa: *«un
apartamiento que sobrevive dos o más saltos sin ser contemplado ya demostró que no es de un
producto»*. **Los tres se declaran candidatos a regla del framework** en el informe de M6, con su
fundamento y su cuenta. La migración no los resuelve —el framework no se toca desde un destino— pero
**el número los reporta**, que es exactamente para lo que existe.

**Ninguno se re-fundamenta.** Los tres siguen `vigente` y se preservan **con su texto literal**,
incluido su fundamento original: reescribirlo contra la normativa nueva produciría un ADR que dice
haber decidido algo que en su fecha nadie decidió.

---

## 6. Documentos fuera de alcance

| Artefacto | Razón (`Migracion-Rules.md` §2.2) |
|---|---|
| Todo `_legacy/` | Snapshots congelados, intocables |
| `changelog.md` del producto y los informes de `SDD/Docs/Audit/` | **Registro histórico.** Un informe emitido con veredicto y fecha describe el estado de un momento; reescribirlo le haría decir algo que no dijo |
| `SDD/Maquetas/` | Material ejecutable que el humano edita a mano, exento del archivado |
| `src/`, `tests/`, `visor/`, `scripts/`, `deploy/`, `samples/` | Código, no documentación de especificación |
| `AGENTS.md` | Se regenera desde `Contrato-Agentes.md` en la Fase I |
| **Categoría 11 · Documentación** | **Caso especial de §2.2**: este destino está **después del handoff** y tiene código, de modo que la 11 vive en el tramo de documentación viva. Su migración se enruta por la re-ejecución de la Fase I, **no por regeneración plana** |

---

## 7. Lo que este plan hace notar antes de que se apruebe

**El trabajo es ancho pero poco profundo, salvo en un punto.** De los 497 documentos vivos, el salto
alcanza **151**: 144 historias de usuario, seis documentos de la 09 y el README raíz. En 150 de ellos
el cambio es de **forma** —partir una sección en dos, reemplazar una cita—. **La excepción es la
estimación**, y no es de forma: es una decisión que hoy no está tomada.

**La decisión de alcance está en §4.1 y es del Product Owner.** Si `D2` se decide, 144 documentos
cierran con un valor. Si no, 144 documentos difieren con los cuatro campos de §12.2 — que es
legítimo y contable, y **muy distinto de la remisión en prosa que tienen hoy**, que la 11.0 califica
**P1**.

**Este plan no toca los 33 ítems vencidos ni los 11 sin evento.** Son del frente `A` del plan de
cierre de pendientes, no del salto. Pero **cuatro de los seis documentos que los llevan son
documentos que M4 va a abrir igual** —los dos `Product-Backlog.md` y los dos `Pipeline-CI-CD.md`—, así
que conviene decidir si se aprovecha la pasada o se hacen dos. **La recomendación es aprovecharla**:
abrir dos veces el mismo documento para dos motivos distintos duplica el archivado y el control de
cambios sin ganar nada.

**Lo que este plan no sabe.** Si los cuatro ítems `.b` de §4.2 ya están declarados por separado en
alguno de los seis documentos de la 09. Se declara como **a verificar en M4** en lugar de estimarse:
un destino que ya declaraba las dos mitades no tiene trabajo de contenido, y sólo se sabe abriendo
cada uno.

---

## 7.1 Cómo se ejecuta «Regenerar contenido» en este salto, y por qué no es re-expresión completa

**La clasificación por severidad de `Migracion-Rules.md` §4.3 dice «Regenerar contenido» para todo
documento gobernado por una regla que subió major**, y su definición es *«el documento se re-expresa
completo bajo la normativa vigente»*. **Este plan no ejecuta eso, y la constancia faltaba.**

**Lo que se ejecuta es una partición quirúrgica**: se parten los ítems que la regla partió y **no se
reescribe el resto del documento**. El fundamento es el bloque «Impacto sobre destinos existentes» de
la **11.0**, que lo dice con estas palabras:

> *«Un destino que ya declaraba las dos mitades de cada ítem **no tiene trabajo de contenido**: parte la
> sección en dos y no escribe nada nuevo.»*

**Y el motivo por el que conviene, más allá de que la entrada lo autorice.** Re-expresar completo un
documento cuyo contenido no cambió **es la forma más barata de inventar**: obliga a reescribir prosa
que nadie pidió que cambiara, y `Migracion-Rules.md` §4.1 califica de **P0** el contenido sin fuente.
La partición, en cambio, **se verifica línea por línea contra el origen**.

**Esto es un apartamiento de la mecánica de §4.3 y se declara como tal**, no como una lectura
alternativa. Lo levantó el audit del corte 09 —**A9, no concluyente**— con el argumento exacto: la
entrada de la 11.0 cubre el *contenido*, no la *clasificación*, y ningún archivo del árbol declaraba
que se hubiera optado por partir en lugar de re-expresar.

---

## 8. Clasificación y orden

| Fase | Qué hace acá | Estado |
|---|---|---|
| **M2 · intake** | **Sin filas.** `PRODUCT-INTAKE-template` no se movió —3.4 en el origen y en la vigente—, así que no hay migración estructural del intake | Sin trabajo |
| **M3 · manifiesto** | **Sin filas.** `PRODUCT-MANIFEST-template` sigue en 6.0 y M2 no cambia nada. El manifiesto se toca en M5, sólo por su §1.1 | Sin trabajo |
| **M4 · `SDD/Docs/`** | **151 documentos**: 144 `US-*.md` (§4.1), 6 de la 09 (§4.2) y el README raíz (§4.3). Más la revisión de apartamientos de §5, que escribe el campo 6 de los tres ADR | **Es toda la migración.** Corte 09 **cerrado en ronda 2** |
| **M5 · procedencia** | Reescribe `PRODUCT-MANIFEST` §1.1 a **13.3**, **sólo si M4 cerró completa**. Si no, se declara migración parcial por §4.6 | Pendiente |
| **M6 · auditoría** | Auditor **independiente**, con el encargo de `Master-Prompt.md` §10: refutar y no verificar, con cita literal o el veredicto no vale. Declara los **tres candidatos a regla del framework** de §5.1 | Pendiente |

**Y una constancia sobre M6 que viene de la migración anterior.** Su informe declaró en §0 que **su
auditor no fue independiente** y dejó tres cosas sin verificar. `Plan-Cierre-De-Pendientes.md` §5
declara que esa segunda ronda **caduca** cuanto más se construya encima. Esta migración **agranda lo
construido encima**: si la segunda ronda se va a encargar, conviene decidirlo antes de que M4 reescriba
151 documentos.

---

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.1 | 2026-08-24 | **Repara dos hallazgos del audit independiente del corte 09.** Entra la **columna de fuente de contenido** en la tabla de §4.2 —`Migracion-Rules.md` §2.1 la declara «la forma en que §4.1 se vuelve verificable fila por fila», y la 1.0 no la traía: **P2**—. Y entra **§7.1**, que declara que este plan ejecuta «Regenerar contenido» como **partición quirúrgica y no como re-expresión completa**, con su fundamento en el bloque de impacto de la 11.0 y con el motivo que lo hace preferible: re-expresar un documento cuyo contenido no cambió **es la forma más barata de inventar**, y §4.1 califica eso de P0. Se declara como **apartamiento de la mecánica de §4.3** y no como lectura alternativa, que es lo que el audit dejó **no concluyente** por falta de constancia. | Orquestador de migración normativa SDD |
| 1.0 | 2026-08-23 | Emisión inicial del plan, **fase M1**, **séptima** migración de este destino y la primera que atraviesa **tres saltos major** —`Root-Rules` 7.0 → 8.4, `Rules-Backlog-Tecnico` 4.4 → 5.1 y `Rules-Devops` 5.0 → 6.1—. **El diff normativo no se reconstruyó: se verificó** contra las versiones vivas, por `Master-Prompt-Migracion.md` §5, desde `Estado-Del-Destino-2026-08-23.md` §6. **Renombres: catorce**, la familia `AG` completa, todos de **identificador** y ninguno de archivo; se declara que el destino **no renumera** las 550 ocurrencias del corpus por `Migracion-Rules.md` §4.3.1 y que su trabajo es **la cita en el mapa de documentación**. **Tres superficies medidas sobre el árbol**: **144** historias de usuario que empaquetan prioridad y estimación —con la estimación diferida **en prosa**, que la 11.0 califica **P1**—, los **cuatro ítems `.b`** de la 09 en seis documentos, y **cinco filas** del README raíz. **§5 corre la revisión de apartamientos**, que la migración anterior **no corrió**: los tres ADR resultan **no contemplados** y sus contadores suben **+2** —el salto 9.12 → 10.0 no contado y éste—, con lo cual **los tres cruzan el umbral de dos saltos y se declaran candidatos a regla del framework**. Declara la categoría **11 fuera de alcance** por el caso especial de §2.2, destino posterior al handoff. | Orquestador de migración normativa SDD |
