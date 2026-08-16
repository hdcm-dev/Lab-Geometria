# Plan de migración normativa — 8.6 → 8.11

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-8.6-a-8.11.md
**Versión:** 1.0
**Estado:** Propuesto — espera aprobación del Product Owner
**Fecha:** 2026-08-16
**Autor:** Orquestador de migración normativa SDD
**Instrumento normativo:** `Master-Prompt-Migracion.md` **2.3** §5, con `Migracion-Rules.md` **3.4**
**Diff de origen:** `Estado-Del-Destino-2026-08-16.md` §4, **verificado y no reconstruido** (`Master-Prompt-Migracion.md` 2.3 §5)

---

## 1. Cabecera

| Campo | Valor |
| --- | --- |
| Destino | `PROG2/Geometria/Lab-Geometria` |
| Versión de origen | SDD **8.6** (`PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 2.1 §1.1) |
| Versión vigente | SDD **8.11** (`IA.SDD/CHANGELOG.md`) |
| Conjunto de origen | **Disponible** en `IA.SDD/_legacy/8.6/`, verificado por listado |
| Clasificación de saltos | **Por severidad** — el destino tiene bloque de procedencia completo, no se degrada a «revisar todo» |
| Documentos vivos en `SDD/Docs/` | **450** (845 con `_legacy/`) |
| Invocado desde | `Master-Prompt-Reanudacion.md` 1.1, salida **B**, decisión del Product Owner del 2026-08-16 |

### 1.1 Reconocimiento del destino (M0)

```text
Reconocimiento del destino

Intake:      SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md   nombre vigente
Manifiesto:  SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md nombre vigente
Procedencia: SDD 8.6
Conjunto de origen: disponible en _legacy/8.6/
Clasificación de saltos: por severidad

Documentos en SDD/Docs/: 450 vivos
```

Ningún nombre legado que resolver: el destino ya atravesó la migración 6.0 → 8.6 y sus dos artefactos
de intake llevan el nombre vigente. **M0 no se detiene.**

---

## 2. Tabla de saltos por artefacto, con su severidad

Verificada contra la versión que hoy declara cada archivo del framework, artefacto por artefacto.

### 2.1 Reglas de categoría — las catorce

| Archivo de reglas | Origen | Vigente | Salto | Severidad |
| --- | --- | --- | --- | --- |
| `Rules-Contexto` | 4.1 | 4.1 | **ninguno** | Nula |
| `Rules-Necesidades-Negocio` | 4.0 | 4.0 | **ninguno** | Nula |
| `Rules-Especificacion-Funcional` | 5.0 | 5.0 | **ninguno** | Nula |
| `Rules-UX-UI-DX` | 5.0 | 5.0 | **ninguno** | Nula |
| `Rules-Arquitectura-Tecnica` | 4.0 | 4.0 | **ninguno** | Nula |
| `Rules-Backlog-Tecnico` | 4.0 | 4.0 | **ninguno** | Nula |
| `Rules-Plan-Sprint` | 5.0 | 5.0 | **ninguno** | Nula |
| `Rules-Calidad-Y-Pruebas` | 4.1 | 4.1 | **ninguno** | Nula |
| `Rules-Devops` | 4.0 | 4.0 | **ninguno** | Nula |
| `Rules-Examples` | 6.0 | 6.0 | **ninguno** | Nula |
| `Rules-Documentacion` | 5.0 | 5.0 | **ninguno** | Nula |
| `Root-Rules` | 5.2 | 5.2 | **ninguno** | Nula |
| `Rules-Prompts-AI` | no aplica | 4.0 | — | No aplica: el destino no emite categoría 04 |
| `Rules-Design-Modelo-Template` | no aplica | 1.1 | — | No aplica |

### 2.2 Reglas transversales — las cuatro aplicadas

| Archivo de reglas | Origen | Vigente | Salto | Severidad |
| --- | --- | --- | --- | --- |
| `Intake-Rules` | 4.0 | 4.0 | **ninguno** | Nula |
| `Vocabulario-Rules` | 3.0 | 3.0 | **ninguno** | Nula |
| `Maqueta-Rules` | 4.0 | 4.0 | **ninguno** | Nula |
| `Deriva-Rules` | 5.0 | 5.0 | **ninguno** | Nula |
| `Migracion-Rules` | 3.2 | **3.4** | 3.2 → 3.3 → 3.4 | **Nula sobre artefactos.** Gobierna esta corrida, no la forma de ningún documento |

### 2.3 Plantillas y orquestadores

| Artefacto | Origen | Vigente | Salto | Severidad |
| --- | --- | --- | --- | --- |
| `PRODUCT-INTAKE-template` | 3.0 | **3.1** | Sí | **Baja, y sin trabajo.** §5 |
| `PRODUCT-MANIFEST-template` | 5.0 | 5.0 | **ninguno** | Nula |
| `Master-Prompt` | 7.4 | **7.7** | Sí | Nula sobre artefactos |
| `Master-Prompt-Migracion` | 2.0 | **2.3** | Sí | Nula sobre artefactos |
| `Master-Prompt-Reanudacion` | inexistente | **1.1** | Alta nueva | Nula sobre artefactos |

**El renglón que decide el alcance de esta migración.** Las catorce reglas de categoría y las cuatro
transversales aplicadas **están en la misma versión que la procedencia declara**. Una regla de
categoría que no cambia no puede obligar a reemitir ningún documento de su categoría: es la
clasificación por artefacto de `Migracion-Rules.md` §4.3, aplicada a un salto cuyas cinco versiones
tocaron **proceso** y no **forma de artefacto**.

---

## 3. Renombres de artefacto aplicables

**Ninguno.**

Verificado sobre el `CHANGELOG.md` del framework: el bloque «Impacto sobre destinos existentes» que
`SDD-Development-Guide.md` §VI.4 exige en toda entrada **major** aparece en las entradas 7.0, 6.0,
5.1 y 4.0. **Las cinco entradas del salto —8.7, 8.8, 8.9, 8.10 y 8.11— son minor y ninguna lo
lleva**, porque ninguna renombra ni retira artefactos del destino.

Es la clase de cambio que ningún diff de versiones puede inferir, y por eso se verifica leyendo el
`CHANGELOG` y no comparando números. El resultado es cero.

---

## 4. Tabla de documentos

**Clasificación única: `no tocar`, para los 450 documentos vivos.**

`Migracion-Rules.md` §4.3 clasifica **por artefacto y por la regla que lo gobierna**. Como ninguna de
las catorce reglas de categoría cambió de versión, no hay documento cuya regla de gobierno haya
cambiado, y por lo tanto ninguno cae en `regenerar contenido` ni en `revisar`.

| Nivel / Unidad | Categoría | Regla que la gobierna | Salto de la regla | Documentos vivos | Clasificación | Fuente de contenido |
| --- | --- | --- | --- | --- | --- | --- |
| Producto | `00-Contexto` | `Rules-Contexto` 4.1 | ninguno | 5 | **no tocar** | — |
| Producto | `01-Necesidades-Negocio` | `Rules-Necesidades-Negocio` 4.0 | ninguno | 11 | **no tocar** | — |
| Producto | `Producto/` (vista, pipeline, norma, ADRs, contratos, 11) | `Master-Prompt` §3, `Rules-Documentacion` 5.0 | ninguno | 22 | **no tocar** | — |
| Producto | `Audit/` | — (informes, no artefactos normados) | — | 45 | **no tocar** | — |
| Producto | raíz de `SDD/Docs/` | `README.md` y `Handoff-Checkout.md` | ninguno | 2 | **no tocar** | — |
| `GeometriaFactory-Api` | `02-Especificacion-Funcional` | `Rules-Especificacion-Funcional` 5.0 | ninguno | 39 | **no tocar** | — |
| `GeometriaFactory-Api` | `03-UX-UI-DX` | `Rules-UX-UI-DX` 5.0 | ninguno | 5 | **no tocar** | — |
| `GeometriaFactory-Api` | `05-Arquitectura-Tecnica` | `Rules-Arquitectura-Tecnica` 4.0 | ninguno | 48 | **no tocar** | — |
| `GeometriaFactory-Api` | `06-Backlog-Tecnico` | `Rules-Backlog-Tecnico` 4.0 | ninguno | 118 | **no tocar** | — |
| `GeometriaFactory-Api` | `07-Plan-Sprint` | `Rules-Plan-Sprint` 5.0 | ninguno | 2 | **no tocar** | — |
| `GeometriaFactory-Api` | `08-Calidad-Y-Pruebas` | `Rules-Calidad-Y-Pruebas` 4.1 | ninguno | 9 | **no tocar** | — |
| `GeometriaFactory-Api` | `09-Devops` | `Rules-Devops` 4.0 | ninguno | 6 | **no tocar** | — |
| `GeometriaFactory-Api` | `10-Examples` | `Rules-Examples` 6.0 | ninguno | 14 | **no tocar** | — |
| `GeometriaFactory-Api` | `11-Documentacion` | `Rules-Documentacion` 5.0 | ninguno | 1 | **no tocar** | — |
| `GeometriaFactory-Web` | `02-Especificacion-Funcional` | `Rules-Especificacion-Funcional` 5.0 | ninguno | 14 | **no tocar** | — |
| `GeometriaFactory-Web` | `03-UX-UI-DX` | `Rules-UX-UI-DX` 5.0 | ninguno | 23 | **no tocar** | — |
| `GeometriaFactory-Web` | `05-Arquitectura-Tecnica` | `Rules-Arquitectura-Tecnica` 4.0 | ninguno | 27 | **no tocar** | — |
| `GeometriaFactory-Web` | `06-Backlog-Tecnico` | `Rules-Backlog-Tecnico` 4.0 | ninguno | 34 | **no tocar** | — |
| `GeometriaFactory-Web` | `07-Plan-Sprint` | `Rules-Plan-Sprint` 5.0 | ninguno | 2 | **no tocar** | — |
| `GeometriaFactory-Web` | `08-Calidad-Y-Pruebas` | `Rules-Calidad-Y-Pruebas` 4.1 | ninguno | 10 | **no tocar** | — |
| `GeometriaFactory-Web` | `09-Devops` | `Rules-Devops` 4.0 | ninguno | 7 | **no tocar** | — |
| `GeometriaFactory-Web` | `10-Examples` | `Rules-Examples` 6.0 | ninguno | 5 | **no tocar** | — |
| `GeometriaFactory-Web` | `11-Documentacion` | `Rules-Documentacion` 5.0 | ninguno | 1 | **no tocar** | — |
| | | | **Total** | **450** | **450 `no tocar`** | |

**Apartamiento declarado sobre el formato de §5 del orquestador.** `Master-Prompt-Migracion.md` §5
pide **una fila por documento**. Acá la tabla es **por categoría con su recuento**, y el apartamiento
se declara en lugar de ejercerse en silencio. El motivo: la fila por documento existe para que cada
documento lleve su clasificación y su fuente de contenido cuando **difieren entre documentos de la
misma categoría**. Con salto de regla cero, la clasificación es la misma para los 450 por
construcción —la deriva de la regla, no del documento—, y 450 filas idénticas no agregan
verificabilidad: la ocultan. El recuento por categoría **sí** es verificable con herramienta, y es la
forma en que este plan se deja auditar en M6.

**Ninguna fila queda sin resolver**, que es la condición que M5 verifica y cuyo incumplimiento es P0.

---

## 5. El único punto de contacto, y qué trabajo genera

La 8.7 llevó `PRODUCT-INTAKE-template` de 3.0 a 3.1: §17 pasa a dos tablas de identidad y `D8` /
`redistribuible` dejan de pedírsele al proyecto de código.

**Este intake ya cumple**, verificado sobre el archivo y no sobre la afirmación:

| Verificación | Evidencia |
| --- | --- |
| §17.1 y §17.2 declaran `tipo_unidad_entrega` y `redistribuible` **en la unidad de entrega** | `rest-api` / `false` para `GeometriaFactory-Api`; `web-monolith` / `false` para `GeometriaFactory-Web` |
| La tabla de proyectos de código **no** los lleva, con su constancia | «`tipo_unidad_entrega` y `redistribuible` **no figuran acá**: son atributos de la unidad de entrega, según §13.1 y §13.2» |
| §13.2 lo declara para todo el intake | «**Los proyectos de código no llevan valor D8**, y esta emisión los deja sin él» |

**Por qué ya cumplía.** El `CHANGELOG` 8.7 lo dice: el defecto de la plantilla **se descubrió
migrando este destino**, en la fase M2 de la migración 6.0 → 8.6, y el agente emitió la contradicción
como hallazgo aguas arriba en lugar de copiarla. La 3.1 recoge lo que este intake ya resolvió.

**Trabajo que genera en M2: ninguna reescritura.** No hay diff de estructura que presentar —cero
secciones movidas, partidas, colapsadas, renombradas, cero campos que cambien de dueño o
desaparezcan, cero secciones nuevas sin fuente, cero contenido sin destino—, y por lo tanto **no hay
batería de preguntas que emitir**. M2 se declara **sin cambios** y no escribe. Consecuencia
correlativa: **M3 no re-deriva el manifiesto**, porque el intake no cambió y el manifiesto es su
artefacto derivado; sólo M5 lo toca, en su bloque de procedencia.

---

## 6. Documentos fuera de alcance

| Qué | Razón (`Migracion-Rules.md` §2.2) |
| --- | --- |
| `SDD/Docs/**/_legacy/**` — 395 documentos | Snapshots de estados superados. No se migran: preservar es su función |
| `SDD/Intake/_legacy/2026-08-16/` | Ídem |
| `SDD/Docs/Audit/` — 45 informes | Registros fechados de lo que se verificó en su fecha. Reescribirlos falsea el registro |
| `SDD/Docs/Handoff-Checkout.md` | Ya declarado **superado** por el hallazgo M-08 de la migración anterior, con su cartel y su tabla de reubicación. Sigue fuera de alcance por la misma razón |
| `SDD/Maquetas/` | No es documentación normada por una regla de categoría |
| El código: `src/`, `tests/`, `visor/`, `scripts/`, `deploy/`, `changelog.md` | Una migración normativa no toca el código ni su registro. **Nota:** el registro de cambios tiene la divergencia `D-01` del informe de estado, que esta migración **no repara** |

---

## 7. Degradación declarada

**Ninguna.** El destino tiene bloque de procedencia completo en `PRODUCT-MANIFEST` 2.1 §1.1, con las
veintidós filas de artefacto. La clasificación de saltos se hace **por severidad** y no se degrada a
«revisar todo» (`Migracion-Rules.md` §4.5).

---

## 8. Qué queda por ejecutar, fase por fase

| Fase | Qué corresponde acá | Escribe |
| --- | --- | --- |
| **M0** | Hecha (§1.1). Destino reconocible, sin nombres legados | No |
| **M1** | Este plan. Diff **verificado**, no reconstruido | Este documento |
| **M2** | **Sin cambios**: el intake ya cumple la plantilla 3.1 (§5). Se declara y no se escribe | No |
| **M3** | **No corresponde**: el intake no cambió, y el manifiesto se re-deriva del intake | No |
| **M4** | **Sin trabajo**: 450 documentos clasificados `no tocar`. Sin cortes ni audits intermedios, porque no hay documento migrado que auditar | No |
| **M5** | **La fase sustantiva de esta migración.** Verifica que la cadena quedó completa y reescribe el bloque de procedencia de `PRODUCT-MANIFEST` §1.1 de **8.6 a 8.11**, con su fila de control de cambios y bump minor | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` |
| **M6** | Auditoría independiente de la migración, con los catorce criterios de `Migracion-Rules.md` §6 | `Informe-Migracion-8.6-a-8.11.md` |

**Esta migración es, en los hechos, la actualización verificada de la procedencia.** Es exactamente
el caso que `Master-Prompt-Reanudacion.md` §4 nombra «actualizar la procedencia sin migrar», con una
diferencia que importa: acá **se ejecuta por la fase M5 del orquestador de migración**, que es la que
existe para impedir que la procedencia afirme algo que nadie comprobó. La lista de qué cambió en el
framework y por qué cada cosa no toca al destino está en §2, §3 y §5 de este plan, y en §4 del
informe de estado.

---

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Plan del salto **8.6 → 8.11**, con el diff normativo **verificado** desde `Estado-Del-Destino-2026-08-16.md` §4 en lugar de reconstruido (`Master-Prompt-Migracion.md` 2.3 §5). Catorce reglas de categoría y cuatro transversales **sin salto**; cambian los tres orquestadores, `Migracion-Rules` y `PRODUCT-INTAKE-template` 3.0 → 3.1. **Cero renombres de artefacto**, verificado por ausencia de bloque «Impacto sobre destinos existentes» en las cinco entradas del salto, todas minor. **450 documentos vivos clasificados `no tocar`**, con el apartamiento del formato de fila por documento declarado en §4. M2 sin cambios, M3 sin corresponder, M4 sin trabajo; **M5 es la fase sustantiva**. | Orquestador de migración normativa SDD |
