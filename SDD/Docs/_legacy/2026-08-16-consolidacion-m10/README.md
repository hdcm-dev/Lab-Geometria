# Documentos absorbidos por la consolidación de la fusión (M-10)

**Fecha:** 2026-08-16
**Motivo:** `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0
**Regla:** `Migracion-Rules.md` §4.3.2
**Estado:** **En curso.** 27 de 67 grupos consolidados · **6 de 18 carpetas `_fusion/` retiradas**

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
| `08-Calidad-Y-Pruebas` | 9 | 24 | **S1** en cinco, **S2** en tres, **S3** el índice |
| `05-Arquitectura-Tecnica` | 4 | 11 | **S1** en uno, **S2** en dos, **S3** el índice |
| `02-Especificacion-Funcional` | 3 | 9 + 3 snapshots `_legacy/` | **S1** el índice maestro, **S2** el glosario, **S3** el `README` |
| `03-UX-UI-DX` | 5 | 15 | **S1** en dos, **S2** en dos, **S3** el índice |
| `06-Backlog-Tecnico` | 4 | 12 | **S1** en uno, **S2** en dos, **S3** el índice |
| `09-Devops` | 5 | 15 | **S1** en cuatro, **S3** el índice |

**Preservación medida sobre los veintiún documentos que transponen: 6765 líneas de contenido
absorbidas, 0 sin correspondencia.** Los seis `README` no transponen —les corresponde **S3**— y sus
545 líneas están enteras acá.

**Un hueco encontrado y cerrado en la tercera tanda: el preámbulo.** La transposición leía sólo el
contenido dentro de las secciones numeradas, y **el texto entre la cabecera y la §1 se perdía**.
Alcanzaba a un solo documento —`Supply-Chain-Seguridad`, donde las tres capas declaraban por separado
de dónde sale su política— y quedó recuperado como su §1. El verificador de preservación se corrigió
para contarlo.

**Los snapshots `_legacy/` que viajaban dentro de `_fusion/` acompañan a sus documentos**, que es la
decisión que el análisis §6 dejaba abierta.

## Qué falta

**40 grupos** en **3 categorías de `GeometriaFactory-Api`** —`07-Plan-Sprint`, `10-Examples` y `11-Documentacion`— y **las 9 de `GeometriaFactory-Web`**, con su clasificación propuesta y su orden en
`Audit/Migracion-M10-Consolidacion-Fusion.md` §4 y §5. Mientras un grupo no se consolide, sus
documentos siguen en `<categoria>/_fusion/<Origen>/`, y **la presencia de esa carpeta declara que la
fusión no terminó**.
