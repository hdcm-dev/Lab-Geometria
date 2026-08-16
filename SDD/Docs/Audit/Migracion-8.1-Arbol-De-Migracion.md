# Árbol de migración normativa — SDD 6.0 a 8.1

**Documento:** Migracion-8.1-Arbol-De-Migracion.md
**Versión:** 1.0
**Fecha:** 2026-08-15
**Regla:** `Migracion-Rules.md` §4.3.1 (renumeración) y §4.3.2 (migración estructural)
**Estado:** **Pasada 1 de 2 — inventario.** Ningún archivo del destino fue modificado por este documento

---

## 1. Diff normativo

El destino declara procedencia SDD 6.0. Los quince archivos normativos que gobiernan su
documentación saltan **major**, sin un solo salto menor.

| Archivo | Origen | Vigente |
|---|---|---|
| `Master-Prompt` | 5.2 | 7.1 |
| `Root-Rules` | 3.1 | 5.1 |
| `Rules-Especificacion-Funcional` | 4.0 | 5.0 |
| `Rules-Examples` | 4.1 | 6.0 |
| `Rules-Plan-Sprint` | 3.1 | 5.0 |
| Las diez restantes | 3.1 / 4.0 / 4.1 | major cada una |

## 2. Clasificación confirmada (§4.3.2 paso 1)

| Proyecto de código | Señal | Clasificación |
|---|---|---|
| `GeometriaFactory-Api` | Principal; «Host REST **desplegado en el servidor propio**» | **Unidad de entrega** |
| `GeometriaFactory-Web` | «**desplegado en el hosting público**; único punto de contacto del navegador» | **Unidad de entrega** |
| `GeometriaFactory-Domain` | «Entidades e invariantes del dominio» | Proyecto de código de `Api` |
| `GeometriaFactory-Application` | «Casos de uso y puertos» | Proyecto de código de `Api` |
| `GeometriaFactory-Infrastructure` | «EF Core con SQLite, seguridad» | Proyecto de código de `Api` |
| `GeometriaFactory-Visor` | «Proyecto Node.js/TypeScript que produce el bundle» | Proyecto de código de `Web` |
| `GeometriaFactory-Contracts` | «**Referenciado por Api y por Web**» | Proyecto de código **compartido** |

Los siete tienen `redistribuible: false`: ninguna librería es unidad de entrega por publicación.

## 3. Matriz de composición resultante

| | Contracts | Domain | Application | Infrastructure | Visor |
|---|---|---|---|---|---|
| **Api** | X | X | X | X | |
| **Web** | X | | | | X |

`Contracts` es compartido: su árbol **no se funde en ninguna** unidad. Su contenido de arquitectura va
al inventario de `Producto/Vista-Producto.md` y el resto se presenta como contenido sin destino.

## 4. Colisiones de identificador

Al fundir árboles que numeraban de forma independiente, con ámbito de unicidad ahora en el producto:

| Unidad de entrega | Familia | Declarados | Colisionan |
|---|---|---|---|
| `Api` | `ADR` | 27 | 19 |
| `Api` | `CU` | 46 | 33 |
| `Api` | `US` | 114 | 82 |
| `Api` | `RN` | 16 | 0 |
| `Api` | `RC` | 7 | 0 |
| `Web` | `ADR` | 13 | 6 |
| `Web` | `CU` | 17 | 7 |
| `Web` | `US` | 30 | 0 |
| **Total** | | **270** | **147** |

## 5. Esquema de renumeración

Offset por proyecto de código de origen, que **conserva el número original como sufijo legible**: de
`ADR-04006` se lee que era el `ADR-06` de Application. Reversible y auditable.

| Unidad de entrega | Origen | Offset | Ejemplo |
|---|---|---|---|
| `Api` | Api | +0 | `ADR-01` → `ADR-00001` |
| `Api` | Domain | +2000 | `ADR-01` → `ADR-02001` |
| `Api` | Application | +4000 | `ADR-06` → `ADR-04006` |
| `Api` | Infrastructure | +6000 | `ADR-03` → `ADR-06003` |
| — | Contracts | +8000 | `CU-06` → `CU-08006` |
| `Web` | Web | +10000 | `ADR-02` → `ADR-10002` |
| `Web` | Visor | +12000 | `ADR-01` → `ADR-12001` |

**Verificado: 270 identificadores mapeados, 0 colisiones en el destino.**

**`NB` no se renumera.** Es de nivel producto, la produce una sola categoría y no colisiona con nada.
Solo se ensancha a cinco dígitos. Es la corrección 8.1 del framework, encontrada al calcular este
árbol: aplicar la regla anterior obligaba a renumerar sus 2.309 citas sin motivo.

## 6. Volumen

| Qué | Cantidad |
|---|---|
| Documentos en `SDD/Docs/` | 589 |
| Archivos a renombrar | 270 |
| Archivos que contienen identificadores | 635 |
| Ocurrencias de identificador a reescribir | ~27.700 |
| Árboles de once categorías | 7 → 2 |

## 7. Decisiones que la migración no puede tomar

### 7.1 Cincuenta y siete citas desnudas ambiguas

Identificadores citados en prosa cuyo número **no existe en el proyecto que los escribe**, de modo que
apuntan a otro proyecto sin decir a cuál. Es el defecto que el reporte `01` documentó —ámbito de
unicidad no declarado— materializado en la migración.

| Proyecto que cita | Familia | Citas | Identificadores |
|---|---|---|---|
| Application | `CU` | 27 | `CU-12`, `CU-13` |
| Visor | `CU` | 11 | `CU-12`, `CU-15`, `CU-16`, `CU-17`, `CU-28` |
| Web | `CU` | 7 | `CU-11`, `CU-13`, `CU-27` |
| Infrastructure | `CU` | 7 | `CU-11` |
| Contracts | `US`, `CU` | 3 | `US-23`, `US-30`, `CU-27` |
| Domain, Infrastructure | `US` | 2 | `US-30` |

**Son resolubles leyendo, no automáticamente.** Varias traen el proyecto en la misma oración —«`US-30`
de Api», «`CU-13`, la operación de reseteo del dominio»—; otras dependen del párrafo. Y al menos una
no apunta a ningún proyecto: «el intake prevé veintisiete casos de uso `CU-01` a `CU-27` **a nivel
producto**», que es una numeración prevista y no emitida.

### 7.2 La familia calificada `P·CU`, con 166 ocurrencias

El destino acuñó `P·CU-XX` para referirse a casos de uso **de nivel producto** previstos por el
intake. No es una familia del framework: es un identificador acuñado aguas abajo, que es el patrón
del reporte `01` §4.2.

Hay que decidir qué es en el modelo nuevo: una familia propia con su prefijo declarado, una remisión
al intake que no necesita identificador, o casos de uso de una unidad de entrega que todavía no se
emitieron.

### 7.3 El contenido sin destino de `Contracts`

Setenta y dos documentos de un proyecto de DTOs, entre ellos `Guia-Onboarding-Developer.md` y un
`Entornos-Deploy.md` que declara no tener entornos. Su arquitectura va al inventario de producto; el
resto se presenta documento por documento y **no se descarta en silencio**.

## 8. Qué falta para la pasada 2

1. Resolver las 57 citas ambiguas de §7.1.
2. Decidir qué es `P·CU` (§7.2).
3. Recorrer el contenido sin destino de `Contracts` (§7.3).

Con eso, la pasada de aplicación es mecánica: renombre, reescritura de referencias y fusión de
árboles, cerrando con las tres comprobaciones bloqueantes de `Migracion-Rules.md` §4.3.1.
