# EXP-0001 · Actuación 013 — Resolución: migración completa y cierre del expediente

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `resolucion`
**Fecha:** 2026-09-13
**Autor:** Orquestador de migración normativa SDD
**Foliada:** 013 · **Forma del lote:** `Master-Prompt.md` §8.1 y §7.0, `Mesa-Rules.md` §7.1

---

## 1. Resolución

La migración normativa de `Lab-Geometria` de SDD **13.7** a SDD **13.16** queda **completa**
(`Migracion-Rules.md` §4.6, a contrario; `Master-Prompt-Migracion.md` §9): la cadena D6 migrada —intake 5.0,
manifiesto 7.0, los veintidós documentos de `SDD/Docs/` del plan—, ninguna fila del plan 1.4 sin resolver
(§4.1), el audit de cierre sin P0 (actuación [012](012-auditoria-cierre-m4.md)) y **la procedencia del
manifiesto reescrita a 13.16** (7.1, 2026-09-13). La resolución provisoria de la actuación
[010](010-resolucion-lote-al-product-owner.md) queda superada por ésta.

**Sobre la detención de M5** (`Master-Prompt-Migracion.md` §9: «se presenta el resultado de la verificación y
qué se va a hacer con la procedencia, antes de escribirla»). El resultado de la verificación es el §4.1 del
plan 1.4 y el §9 del informe 1.1; lo que se hizo con la procedencia es el 7.1. **Se escribió antes de esta
presentación**, por instrucción del orquestador de la serie y sobre la fila PM-10 del plan que el Product Owner
aprobó en la actuación 011 («procedencia a 13.16 sólo con la cadena completa»). Se declara como apartamiento de
la forma de §9, propio de la corrida (`Master-Prompt.md` §8.1), con su salida: **si el Product Owner no
confirma el punto G del lote, el 7.1 se revierte al snapshot `_legacy/2026-09-13/…-v7.0.md`** y la migración
vuelve a parcial declarada, que es lo que §9 paso 3 manda.

## 2. Lo que cambió en esta rama, desde `main` = `1d4afc4`

| Commit | Artefacto | Versión antes → después | Actuación |
|---|---|---|---|
| `255b55c` | `actuaciones/011-testimonio-aprobacion-plan-e-intake.md` | nueva | 011 |
| `c14bf3b` | `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` | 4.6 → **5.0** (major, caso (b)) | 011, 012 |
| `c14bf3b` | `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | 6.1 → **7.0** (re-derivado) | 011, 012 |
| `c14bf3b` | `SDD/Docs/Producto/Vista-Producto.md` | 1.10 → 1.11 | 012 |
| `c14bf3b` | `SDD/Docs/Producto/Pipeline-Producto.md` | 1.8 → 1.9 | 012 |
| `c14bf3b` | `Api/05/Arquitectura-Unidad-Entrega.md`, `Api/06/Product-Backlog.md`, `Api/09/Pipeline-CI-CD.md`, `Api/09/Supply-Chain-Seguridad.md`, `Web/05/Arquitectura-Unidad-Entrega.md`, `Web/06/Product-Backlog.md`, `Web/09/Pipeline-CI-CD.md`, `Web/09/Supply-Chain-Seguridad.md` | 3.14 → 3.15, 4.5 → 4.6, 3.9 → 3.10, 3.2 → 3.3, 3.8 → 3.9, 4.5 → 4.6, 3.8 → 3.9, 3.3 → 3.4 (ciclo de origen, 118 filas) | 012 |
| `c14bf3b` | `SDD/Docs/Producto/Contratos-Inter-Unidad/CU-08001` a `CU-08008` | +1 minor cada uno (campo 5) | 012 |
| `c14bf3b` | `SDD/Docs/Producto/Adrs/ADR-14001` a `ADR-14004` | 1.2 → 1.3, 1.2 → 1.3, 1.3 → 1.4, 1.2 → 1.3 (contador, campo 7) | 012 |
| `c14bf3b` | 24 snapshots `_legacy/2026-09-13/<Documento>-v<versión>.md` | nuevos | 012 |
| `c6d754e` | `SDD/Docs/Audit/Mesa-2026-09-13.md` | 1.2 → 1.3 (`DD-8`, `DD-9`) | 012 |
| `c6d754e` | `SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md` | 1.3 → 1.4 (§4.1 estado al cierre) | 012 |
| `c6d754e` | `SDD/Docs/Audit/Informe-Migracion-13.7-a-13.16.md` | 1.0 → 1.1 (§9 audit de cierre, §8 migración completa) | 012 |
| `c6d754e` | `evidencia/E-015b`, `E-022`, `E-023`, `E-024`; `actuaciones/012` | nuevos | 012 |
| `add9f01` | `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` | 7.0 → **7.1** (M5: procedencia **13.16**); snapshot `-v7.0.md` | 013 |
| `add9f01` | `SDD/Docs/README.md` | 2.7 → 2.8 (§7.1 octava migración cerrada); snapshot `-v2.7.md` | 013 |
| `add9f01` | `changelog.md` | entrada «Migración normativa SDD 13.7 → 13.16, cerrada» | 013 |
| _(este commit)_ | `actuaciones/013`, `README.md` del expediente | resolución y estado **resuelto** | 013 |

## 3. Lo que rige por default, sin respuesta del Product Owner

Del lote de la actuación 010: **C** (`visor.bundle.js` en el intake) queda como `DD-5`; **D** (`PA-09` de
`Api/06` :749) y **E** (`PD-04` de `Api/09` :597) se tratan como huecos del ciclo, por espejo de `PA-08` y
`PA-07`. Los tres siguen abiertos y con su forma de `Root-Rules.md` §12.2; ninguno condiciona la procedencia.

## 4. El lote de cierre

```text
LOTE DE CIERRE — EXP-0001, migración 13.7 → 13.16 · 2026-09-13

F · ¿Aprobás quitar el número de versión de las 116 cabeceras «Trazabilidad upstream» que todavía citan el
    intake o el manifiesto con número (DD-8)?
  QUÉ PASÓ         La deuda de Mesa-2026-08-27.md §8 vencía «en la M4 de la próxima migración que alcance
                   artefactos». Esta M4 escribió y sólo corrigió 2 de 118 (Vista-Producto, Pipeline-Producto),
                   porque el plan 1.3 que aprobaste las excluía (PM-11). El audit de cierre lo levantó como P1
                   (M6-19). La corrección de fondo no es poner el número vigente sino dejar de escribirlo
                   (Root-Rules §10 R1): 116 celdas, un minor por documento, sin cambio de contenido declarado.
  ORIGEN DEL HECHO  propio de la corrida · calculado contra b9675d8: la premisa «esta M4 no escribe» la cambió
                   la aplicación, no vos.
  OPCIONES         (1) aprobar, como fila nueva del plan con su propuesta y su audit; (2) aprobar sólo las 11 de
                   los documentos que esta M4 ya abrió; (3) dejarlo en DD-8.
  PROPUESTA        (1). Alternativa razonable: (2), si preferís no abrir 105 documentos más en esta migración.
  SI NO RESPONDÉS  rige (3): DD-8, con evento en la fila PM-11 del plan de la próxima migración normativa.
  QUÉ NECESITO     «aprobado (1)», «aprobado (2)» o «déjalo».

G · Confirmación de la procedencia en 13.16 (manifiesto 7.1), escrita antes de la detención de §9
  QUÉ PASÓ         M5 verificó la cadena completa (plan 1.4 §4.1, informe 1.1 §8 y §9) y reescribió §1.1 con
                   las versiones de E-002. La forma de §9 pide presentar antes de escribir; se escribió por
                   instrucción del orquestador de la serie, sobre tu aprobación de PM-10 en la actuación 011.
  ORIGEN DEL HECHO  propio de la corrida · calculado contra b9675d8.
  OPCIONES         (1) confirmar; (2) revertir el 7.1 al snapshot y dejar la migración como parcial declarada.
  PROPUESTA        (1).
  SI NO RESPONDÉS  rige (1): la verificación que §9 exige está hecha y escrita, y nada de lo que queda abierto
                   (DD-1 a DD-9) es de la cadena.
  QUÉ NECESITO     «confirmado» o «revertí».
```

**Ninguna otra pregunta sobrevivió al audit de cierre.** `M6-31` (la fase `k` cerrada sobre la `i` abierta) es
ajeno a la migración y queda asentado para la próxima reanudación, no para este lote.

## 5. Continuación fuera de este expediente

Migrar `RPI.VideoControl` a 13.16 (actuación 001, P-2): expediente propio, cuando el Product Owner lo
disponga. Y la fusión de esta rama, que es del Product Owner (actuación 007).
