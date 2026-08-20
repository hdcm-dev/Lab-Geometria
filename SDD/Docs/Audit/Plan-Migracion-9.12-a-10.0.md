# Plan de migración normativa — SDD 9.12 → 10.0

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-9.12-a-10.0.md
**Versión:** 1.0
**Fecha:** 2026-08-19
**Estado:** **M1 · esperando aprobación.** Ningún documento modificado
**Instrumento:** `Master-Prompt-Migracion.md` **2.8**, fase M1
**Origen → vigente:** SDD **9.12** → **10.0** · **Sexta** migración normativa de este destino

---

## 1. Cabecera

| Campo | Valor |
|---|---|
| Versión de origen | **9.12**, declarada en `PRODUCT-MANIFEST` **3.2** §1.1 |
| Versión vigente | **10.0** |
| Conjunto de origen reconstruible | **Sí**, `_legacy/9.12/` existe en el repositorio del framework |
| Saltos atravesados | 9.13 a 9.19 (**siete minor**) y **10.0** (un major) |
| Diff normativo previo | El de `Estado-Del-Destino-2026-08-18.md` §4 cubre 9.12 → 9.19 y **se verifica acá, no se rehace** |

## 2. Tabla de saltos por artefacto

**Medido contra los archivos vivos del framework.**

| Artefacto | Origen 9.12 | Vigente 10.0 | Severidad | ¿Alcanza al destino? |
|---|---|---|---|---|
| `Root-Rules` | 6.2 | **7.0** | **major** | **Sí** — §12 se parte y entra §12.2 |
| `Rules-Devops` | 4.6 | **5.0** | **major** | **Sí** — §4.3 punto 3 se parte |
| `Master-Prompt` | 8.4 | 8.8 | minor | No — proceso |
| `Master-Prompt-Migracion` | 2.7 | 2.8 | minor | No — proceso |
| `Master-Prompt-Reanudacion` | 1.6 | 1.8 | minor | No — proceso |
| `Migracion-Rules` | 3.9 | 3.15 | minor | No — gobierna cómo se migra |
| `Catalogo-De-Criterios` | 1.1 | 1.6 | minor | No — índice |
| **Las once reglas de categoría** | 4.3–6.4 | **idénticas** | — | **No se movieron ni una** |
| `Intake-Rules` · `Vocabulario-Rules` · `Maqueta-Rules` · `Deriva-Rules` | 4.1 / 3.1 / 4.3 / 5.3 | **idénticas** | — | No |
| `PRODUCT-INTAKE-template` · `PRODUCT-MANIFEST-template` | 3.4 / 6.0 | **3.4 / 6.0** | — | No |

**Dos artefactos alcanzan al destino, y los dos entran con la 10.0.** Los siete saltos minor anteriores
tienen **alcance documental cero**, ya verificado y declarado en la reanudación del 2026-08-18.

## 3. Renombres de artefacto aplicables

**Ninguno.** El bloque «Impacto sobre destinos existentes» de la entrada 10.0 declara su tabla de
renombres **vacía**, y las entradas 9.13 a 9.19 son minor y no llevan bloque.

## 4. Las dos superficies del salto, medidas sobre el árbol

### 4.1 Citas a `Root-Rules.md` §12 → §12.1 · **superficie CERO**

**Medido: 0 ocurrencias** en `SDD/Docs/` y `SDD/Intake/`. Este destino nunca citó esa sección, de modo
que la primera fila de la tabla de secciones movidas **no lo toca**. Se declara verificado y no
supuesto.

**Exclusión enumerada** (`SDD-Development-Guide.md` §VI.3.2). Las **2** ocurrencias vivas del patrón
`Root-Rules.md §12` son **la frase que nombra este mismo barrido** —acá y en la tabla de superficies
de `PRODUCT-MANIFEST` §1.1—, no citas de la sección como fuente normativa. §VI.3.2 prevé el caso al
exigir que la regla 4 se corra **sobre el texto propio**. Cierra el hallazgo `M-01` de
`Informe-Migracion-9.12-a-10.0.md` §2.


### 4.2 Ítems diferidos sin la forma de §12.2 · **superficie 92 filas**

**Es la superficie real de esta migración, y es grande.** El destino declara sus decisiones pendientes
en tablas de puntos abiertos `PA-xx` y `PD-xx`, con columnas *«quién lo cierra»* y *«cuándo»*. La
columna del cuándo **nombra momentos**, que es exactamente lo que §12.2 dejó de admitir.

**118 filas vivas**, en seis documentos. Clasificadas contra su evento de cierre:

| Clase | Filas | Qué significa |
|---|---|---|
| **Cerradas** | **21** | Ya resueltas, con su desenlace y su fecha. **Conformes, no se tocan** |
| **Vencidas** | **78** | Su evento nombra una etapa **ya cerrada** —`a` cerró el 2026-08-13, `h` el 2026-08-18— o la categoría 09 **ya emitida**. Por §12.2 son **hallazgo P1** |
| **Sin evento** | **14** | «Sin fecha comprometida». **No declaran evento de cierre**, de modo que ninguna comprobación las puede vencer |
| **A revisar una por una** | **5** | Su evento nombra un artefacto —`PA-01` de `05` §11, `EP-12002`, la próxima emisión de la 05— y **puede que ya cumplan**. Se resuelven leyendo, no contando |

**Reparto por documento:**

| Documento | Filas |
|---|---|
| `Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md` | 33 |
| `Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md` | 31 |
| `Unidades-Entrega/GeometriaFactory-Web/06-Backlog-Tecnico/Product-Backlog.md` | 16 |
| `Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md` | 16 |
| `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md` | 12 |
| `Unidades-Entrega/GeometriaFactory-Web/09-Devops/Pipeline-CI-CD.md` | 8 |
| `Audit/B-02-03-GeometriaFactory-Domain-r1.md` | **2 — fuera de alcance**, §5 |

### 4.3 El prefijo de tag como ítem propio · **superficie 1 documento**

`Rules-Devops.md` §4.3 pasa a pedir el prefijo en su **punto 3.b**, separado de la herramienta.
`Estrategia-Versionado.md` **2.2** ya lo declara —en su §3.0, escrito el 2026-08-18— pero **dentro de
su propia sección y no como el ítem que la estructura ahora nombra**. Es reordenamiento, no decisión:
**el valor `v` ya está fijado y no se reabre**.

## 5. Documentos fuera de alcance

| Documento | Razón (`Migracion-Rules.md` §2.2) |
|---|---|
| `Audit/B-02-03-GeometriaFactory-Domain-r1.md` | **Informe de auditoría emitido**, con veredicto y fecha. Es registro histórico: sus dos filas describen el estado de un momento, y reescribirlas haría decir a un informe cerrado algo que no dijo |
| Todo `_legacy/` | Snapshots congelados, intocables |
| `changelog.md` y los informes de `Audit/` | Registro histórico del producto |

## 6. Lo que este plan hace notar antes de que se apruebe

**Las 78 filas vencidas no son un defecto que la migración crea: son uno que destapa.** Estaban
vencidas desde antes —algunas desde el 2026-08-13— y **nada las miraba**, que es exactamente la falla
que el reporte `14` documentó y que la 10.0 vino a cerrar. Este destino es el que la produjo, así que
es coherente que sea el primero en pagarla.

**Y hay una decisión de alcance escondida en el número.** Reescribir 92 filas con la forma de §12.2
obliga, en cada una, a **contestar en qué artefacto y sección se cierra** — y en varias la respuesta
honesta puede ser que ya no aplica, o que nadie la va a cerrar. **Eso no lo puede decidir la
migración**: es del Product Owner, fila por fila, y por eso M4 va a detenerse más de una vez.

## 7. Clasificación y orden

| Fase | Qué hace | Estado |
|---|---|---|
| **M2 · intake** | **Sin filas.** El intake no declara puntos abiertos con esta forma ni cita §12 | Sin trabajo |
| **M3 · manifiesto** | **Sin filas.** Se re-deriva sólo si M2 cambió §13, y M2 no cambia nada | Sin trabajo |
| **M4 · `SDD/Docs/`** | **92 filas en 6 documentos**, más `Estrategia-Versionado.md` por §4.3 | **Es toda la migración** |
| **M5 · procedencia** | Reescribe §1.1 a **10.0**, sólo con M4 completa | Pendiente |
| **M6 · auditoría** | Auditor independiente | Pendiente |

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-19 | Emisión inicial del plan, **fase M1**, sexta migración de este destino y **la primera desde la 8.11 → 9.9 que alcanza artefactos**. Dos artefactos del framework la alcanzan, los dos de la 10.0: `Root-Rules` **7.0** y `Rules-Devops` **5.0**; los siete saltos minor previos tienen alcance documental cero, ya verificado. **Renombres: ninguno**, con la tabla del bloque de impacto declarada vacía. Tres superficies medidas sobre el árbol y no supuestas: las citas a §12 dan **cero**, el prefijo de tag es **un documento** y ya tiene su valor fijado, y los ítems diferidos son **118 filas vivas** de las cuales **78 están vencidas**, 14 sin evento y 5 a revisar leyendo. Declara que las vencidas **no son un defecto que la migración crea sino uno que destapa**, y que reescribirlas esconde una **decisión de alcance del Product Owner** fila por fila. | Orquestador de migración normativa SDD |
