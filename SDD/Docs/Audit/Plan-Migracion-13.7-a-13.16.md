# Plan de migración normativa — SDD 13.7 → 13.16

**Producto:** Fábrica de Geometría
**Documento:** Plan-Migracion-13.7-a-13.16.md
**Versión:** 1.1
**Fecha:** 2026-09-13
**Instrumento:** `Master-Prompt-Migracion.md` **2.10**, fase **M1**
**Estado:** **Presentado al Product Owner, sin aprobar.** Lleva los veredictos de [`Mesa-2026-09-13.md`](Mesa-2026-09-13.md). **Mientras no se apruebe, nada se escribe en `SDD/Intake/` ni en `SDD/Docs/`** fuera de esta carpeta (dictamen D-1)
**Decisión que lo origina:** presentación del Product Owner del 2026-09-13, [EXP-0001 actuación 001](../../Expedientes/0001-Migracion-Normativa-A-13.16/actuaciones/001-presentacion-pedido-del-product-owner.md)
**Base de la corrida:** `b9675d8`
**Expediente:** [`SDD/Expedientes/0001-Migracion-Normativa-A-13.16/`](../../Expedientes/0001-Migracion-Normativa-A-13.16/README.md), que folia este plan por enlace

---

## 1. Cabecera

| Campo | Valor |
|---|---|
| Destino | `Lab-Geometria`, producto **Fábrica de Geometría** |
| Versión de origen | **SDD 13.7**, `PRODUCT-MANIFEST` §1.1 |
| Versión vigente | **SDD 13.16**, `IA.SDD` `main` `8c55a1e` |
| Conjunto de origen | **Disponible** en `_legacy/13.7/` del framework |
| Clasificación de saltos | **Por severidad**. No degradada |
| Documentos vivos en `SDD/Docs/` | **551** en la base (E-007) |
| Migración número | **Octava** de este destino |

## 2. Tabla de saltos

La tabla artefacto por artefacto está en EXP-0001 actuación 003 §4 y E-002. **Ningún salto es major**: ningún documento se clasifica «regenerar». Superficies sobre el corpus: `Root-Rules` 8.7, `Intake-Rules` 4.3, `Rules-Arquitectura-Tecnica` 4.6, `Rules-Devops` 6.2, `Rules-Examples` 6.6 y las plantillas 3.6 y 6.1.

## 3. Renombres de artefacto

**Ninguno.** Leídos los ocho bloques «Impacto sobre destinos existentes» de 13.8 a 13.16.

## 4. Filas del plan

**La columna «Fuente» usa sólo los tres valores de `Migracion-Rules.md` §2.1** (MN-LEC-04); lo que se midió va en «Evidencia».

| Fila | Artefacto | Regla y salto | Qué cambia | Clasificación | Fuente | Evidencia | Fase | Propuesta |
|---|---|---|---|---|---|---|---|---|
| **PM-01** | Intake 4.6 → **5.0** | Plantilla 3.5 → 3.6; `Intake-Rules` 4.2 → 4.3 | §13.2 marca y pertenencia por la arista; grafo y orden con dos clases; §13.3 perfil por ecosistema y puntos 2 y 3 como decisión del destino; §16.1 una fila por sample por el D8 de la unidad, filas anteriores transpuestas; §16, §17.2, §19, §23 y trazabilidad sin «excepción» ni D8 por proyecto; cabecera 3.6. **No** toca `visor.bundle.js` (lote) | Revisar, con escritura estructural, caso (b), major | Documento de origen; documento hermano (manifiesto §2.A–§2.C, `Web ADR-10008`) | `GeometriaFactory.sln` y `samples/*/*.csproj` (E-014), E-016 | M2 | `P-01` |
| **PM-02** | Manifiesto 6.1 → **7.0** | Plantilla 6.0 → 6.1 | Re-derivación: §1.2 perfiles; §2.B una tabla con «Solución de código» y la marca; §2.1; §3 un grafo, dos clases, Visor en nivel 0; §4 dos validaciones | Re-derivar | Documento hermano (intake 5.0) | `.csproj` (informe Formal) | M3 | `P-02` |
| **PM-03** | `Vista-Producto.md` 1.10 → 1.11 | `Rules-Arquitectura-Tecnica` 4.5 → 4.6 §4.8 | §2 sin D8 ni `redistribuible`; §3 clase y generador; §1.1 nombre vigente sin borrar el anterior; párrafo del visor sin «apartamiento declarado»; cabecera sin versiones | Revisar | Documento de origen; documento hermano (manifiesto, intake) | E-016 | M4 | `P-03` |
| **PM-04** | `Pipeline-Producto.md` 1.8 → 1.9 | `Rules-Devops` 6.1 → 6.2 §4.9 puntos 3 y 4 | §4 `Visor → Web` con único generador, tres ambientes sin Node, modo `SkipVisorBuild` y sus dos conductas; clases de las otras aristas; §3 sin D8 por proyecto; §9 fila de aristas cerrada; cabecera sin versiones | Revisar | Documento de origen; documento hermano (`Web ADR-10008`) | E-012 | M4 | `P-04` |
| **PM-05** | `samples/` y 10-Examples | `Rules-Examples` 6.5 → 6.6 §3.6 | **Nada que cambiar**: las tres propiedades se cumplen | No tocar | — | E-014, probada fallando | M4 | — |
| **PM-06** | 8 documentos, **118** filas de ítems diferidos | `Root-Rules` 8.7 §12.2 punto 5; `Migracion-Rules` §4.8/§4.9 | Sexta columna «Ciclo de origen»: **95 derivadas**, **23 no derivables** (derivación v5) | Revisar | Documento de origen | E-006g/h (y las cuentas anteriores E-006, E-006e), E-015 | M4 | `P-06` |
| **PM-07** | `Estrategia-Versionado.md` de Api, `PD-VER-01` a `03` | Ídem | **Nada**: las tres filas están «Decidido · 2026-09-12» y son registro, no hueco (MN-REF-N2) | No tocar | — | E-013 | — | — |
| **PM-08** | `ADR-14001` a `ADR-14004` | `Root-Rules` 8.7 §11 punto 7; `Migracion-Rules` §4.7 | Revisión (§5): contadores 4, 4, 3 y 1; campo 7 derivado del alta del archivo | Revisar | Documento de origen | E-008, E-006e | M4 | `P-08` |
| **PM-09** | 17 ocurrencias de «activo de construcción» | 13.16, «Qué le exige a `Lab-Geometria`» | Vivas re-expresadas en P-01 a P-04; registro sin tocar; clasificación en §6 | Revisar, por ocurrencia | Documento de origen | E-003, E-016 | M2–M4 | `P-01`–`P-04` |
| **PM-10** | `PRODUCT-MANIFEST` §1.1 | `Master-Prompt-Migracion` §9 | Procedencia a 13.16 **sólo con la cadena completa** | — | — | E-002 | M5 | — |
| **PM-11** | Deuda de `Mesa-2026-08-27.md` §8 (cabeceras con versión citada) | `Root-Rules` §10 R1 | Evento: la M4 de esta migración. **Esta M4 no escribe** (D-1): el evento no ocurre; P-03 y P-04 aplican la corrección a sus cabeceras y E-011 mide el resto | — | — | E-011 | M4 | `P-03`, `P-04` |
| **PM-12** | Resto del corpus | Reglas sin cambio o de proceso | — | No tocar | — | — | — | — |

## 5. Revisión de apartamientos (`Migracion-Rules.md` §4.7)

**Criterio de conteo**, que no se reabre: `Plan-Migracion-10.0-a-13.3.md` §5.1.1 — cuentan los saltos **que alcanzaron artefactos**. El 13.3 → 13.7 no alcanzó ninguno y **no cuenta**; el 13.7 → 13.16 **sí**. **El contador sólo corre si el ADR sigue `vigente`** (MN-FOR-05).

| ADR | Disparador (campo 4) | Absorbido | Contradicho | No contemplado | **Resultado** | Contador |
|---|---|---|---|---|---|---|
| `ADR-14001` | El framework declara el archivado de una migración estructural, o una migración posterior archiva de forma central | No: ninguna entrada 13.8–13.16; **y esta migración archiva por carpeta** (`_legacy/2026-09-13/` de cada documento, en `propuestas/aplicar-propuestas.sh`) (MN-FOR-06) | No | Sí | **No contemplado** | 3 → **4** |
| `ADR-14002` | Una familia propia del intake pasa a tener artefacto propio | No | No | Sí | **No contemplado** | 3 → **4** |
| `ADR-14003` | IP estática o nombre DDNS en `API_BASE_URL` | **No demostrado**: el canal vigente no depende de la dirección (`F-14`, `X-10`), pero el valor del secreto no vive en ningún repositorio y el ADR que lo supere es del Product Owner (`Mesa-2026-09-12.md` §8) | No | Sí, frente al framework | **No contemplado** (dictamen D-2) | 2 → **3** |
| `ADR-14004` | El framework incorpora el ítem sin objeto, o el producto adopta infraestructura declarativa | No | No | Sí | **No contemplado** | 0 → **1** |

**Candidatos a regla del framework** (dos o más saltos): `ADR-14001`, `ADR-14002` y `ADR-14003`, que el informe de migración declara. `ADR-14005` está retirado y no se revisa.

## 6. Las 17 ocurrencias, clasificadas

| # | Archivo:línea (base) | Qué es | Resultado |
|---|---|---|---|
| 1 | Intake :426 | Prosa viva de §13.2 | Re-expresada (P-01) |
| 2 | Intake :1940 | Fila 4.0 del control de cambios | Registro |
| 3–4 | Manifiesto :219, :253 | §3 y §4 vivos | Re-derivadas (P-02) |
| 5 | Manifiesto :306 | Fila 6.0 del control de cambios | Registro |
| 6 | `Vista-Producto.md` :65 | Magnitud viva, cerrada el 2026-08-31 | Nombre vigente agregado sin borrar el anterior (P-03, D-3) |
| 7 | `Vista-Producto.md` :113 | §3 vivo | Re-expresada (P-03) |
| 8 | `Vista-Producto.md` :262 | Control de cambios | Registro |
| 9 | `11-Documentacion/README.md` :196 | Fila cerrada en tabla viva | **Viva (D-3)**: corresponde agregar el nombre vigente; **no está en ninguna propuesta** porque ese documento es de una regla sin cambio (`DD-6`) — se declara en el informe |
| 10 | `11-Documentacion/README.md` :213 | Control de cambios | Registro |
| 11 | `Handoff-Checkout.md` :606 | `X-1`, cerrada el 2026-08-31, resumen de check-out | Registro (D-3) |
| 12 | `Handoff-Checkout.md` :775 | Control de cambios | Registro |
| 13–14 | `Web ADR-10008` :21, :34 | ADR aprobado | Registro |
| 15 | `changelog.md` :1132 | Entrada fechada | Registro |
| 16 | `evidencia/2026-09-12-estructura-solucion/README.md` :16 | Evidencia fechada | Registro |
| 17 | `visor/geometriafactory-visor.csproj` :15 | Comentario en código | Fuera de alcance, `DD-2` |

**Colisión del nombre vigente, medida** (MN-TRZ-05): `git grep -o -i 'insumo de construcci' b9675d8 | wc -l` → **0**; «insumo» suelto aparece 25 veces en el intake, todas en su control de cambios con otro sentido («citada como insumo»), y 4 en `Handoff-Checkout.md`: registro que no se reescribe.

## 7. Cuestiones D-1 a D-6

Resueltas por la mesa: dictámenes en [`Mesa-2026-09-13.md`](Mesa-2026-09-13.md) §6.1. **Ninguna llega al Product Owner como pregunta**: lo que le llega es la aprobación del lote de EXP-0001.

## 8. Documentos fuera de alcance

`SDD/Maquetas/`, `/samples/` como código, `AGENTS.md` y el código fuente (`Migracion-Rules.md` §2.2); `PROMPTs/` y `/home/fernando/docker/` (despacho de la serie, EXP-0001 actuación 007); `Handoff-Checkout.md` y `Norma-De-Nomenclatura.md` como registros fechados; `Plan-Etapa-A.md`, sin regla que lo gobierne.

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.1 | 2026-09-13 | Incorpora los treinta veredictos de `Mesa-2026-09-13.md`. **La derivación en seco del ciclo de origen se retira** (113/5): tomaba commits de todo el repositorio; la vigente es la v5 (95/23), con las cuentas anteriores publicadas. Suma la clasificación §4.8 (0 elevados de 118), la evidencia de §3.6 probada fallando, PM-11 (la deuda de cabeceras cuyo evento es esta M4) y la columna de evidencia separada de la fuente. PM-05 y PM-07 pasan a «no tocar» con su evidencia. §5 abre los tres resultados por ADR y declara el modo de archivado. **Estado: presentado, sin aprobar**, con las propuestas en el expediente. | Orquestador de migración normativa SDD |
| 1.0 | 2026-09-13 | Borrador de M1 presentado a la mesa. | Orquestador de migración normativa SDD |
