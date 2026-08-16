# Documentos absorbidos por la consolidación de la fusión (M-10)

**Fecha:** 2026-08-16
**Motivo:** `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0
**Regla:** `Migracion-Rules.md` §4.3.2
**Estado:** **`GeometriaFactory-Api` cerrada: 9 de 9 categorías.** 37 de 67 grupos · **9 de 18 carpetas `_fusion/` retiradas**

---

Los documentos de esta carpeta estaban estacionados en `<categoria>/_fusion/<Origen>/` desde la
migración estructural 6.0 → 8.x, esperando la consolidación que `Migracion-Rules.md` §4.3.2 reserva
al humano. **No se borran**: su contenido está en el documento consolidado que los reemplaza.

**Consolidar acá no es deduplicar.** El inventario midió **5,9 % de solapamiento** entre las
versiones de un mismo grupo: el 94 % del contenido es propio de una capa. Por eso el documento
consolidado es una **unión con atribución** —una subsección por proyecto de código, nombrada— y no
una selección.

## Grupos consolidados

**Tres categorías de `GeometriaFactory-Api` terminadas**, con su carpeta `_fusion/` retirada:

| Categoría | Grupos | Documentos absorbidos | Salidas |
| --- | --- | --- | --- |
| `08-Calidad-Y-Pruebas` | 9 | 24 | **S1** ×5, **S2** ×3, **S3** el índice |
| `05-Arquitectura-Tecnica` | 4 | 11 | **S1** ×1, **S2** ×2, **S3** el índice |
| `02-Especificacion-Funcional` | 3 | 9 + 3 snapshots | **S1** el índice maestro, **S2** el glosario, **S3** el `README` |
| `03-UX-UI-DX` | 5 | 15 | **S1** ×2, **S2** ×2, **S3** el índice |
| `06-Backlog-Tecnico` | 4 | 12 | **S1** ×1, **S2** ×2, **S3** el índice |
| `09-Devops` | 5 | 15 | **S1** ×4, **S3** el índice |
| `07-Plan-Sprint` | 2 | 6 | **S1** ×1, **S3** el índice |
| `11-Documentacion` | 1 | 3 | **S3** el índice. **33 % de solapamiento, el más alto del inventario** |
| `10-Examples` | 4 | 3 + **9 renombrados, no absorbidos** | **S4** los samples, **S3** el índice |

**Preservación medida sobre los veintitrés documentos que transponen: 7418 líneas de contenido
absorbidas, 0 sin correspondencia.** Los nueve `README` no transponen —les corresponde **S3**— y sus
715 líneas están enteras acá.

**`10-Examples` es la excepción, y es una decisión.** Sus **doce samples no se fundieron**: los cuatro
`ejemplo-01-basico` eran cuatro samples distintos —cada uno declara qué demuestra y no coinciden—, y
un sample tiene contrato de verificación y evidencia de corrida. Fundir cuatro con contratos distintos
produce uno que **no verifica ninguno**. Se les dio identidad visible con el sufijo del proyecto de
código que ejercita cada uno, **renombrando los cuatro de cada nivel** para que ninguno quede
privilegiado por conservar el nombre corto. **Es la única salida del inventario que no reduce
documentos.**

**Dos huecos encontrados y cerrados durante la ejecución:** el **preámbulo** entre la cabecera y la §1,
que la transposición no leía —alcanzaba a dos documentos y quedó recuperado como su §1—, y la
**reconexión por sustitución de patrón**, que rompió más de lo que arreglaba y se reemplazó por
resolución de destino (§5.1 y §5.2 del análisis).

**Los snapshots `_legacy/` que viajaban dentro de `_fusion/` acompañan a sus documentos.**

## Qué falta

**30 grupos**, todos en **`GeometriaFactory-Web`**, con **9 carpetas `_fusion/`** y una sola capa estacionada, `Visor`, con su clasificación propuesta y su orden en
`Audit/Migracion-M10-Consolidacion-Fusion.md` §4 y §5. Mientras un grupo no se consolide, sus
documentos siguen en `<categoria>/_fusion/<Origen>/`, y **la presencia de esa carpeta declara que la
fusión no terminó**.
