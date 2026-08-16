# Documentos absorbidos por la consolidación de la fusión (M-10)

**Fecha:** 2026-08-16
**Motivo:** `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0
**Regla:** `Migracion-Rules.md` §4.3.2
**Estado:** **En curso.** 16 de 67 grupos consolidados · **3 de 18 carpetas `_fusion/` retiradas**

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

**Preservación medida sobre los doce documentos que transponen: 3070 líneas de contenido absorbidas,
0 sin correspondencia.** Los tres `README` no transponen —les corresponde **S3**, reescribir el
índice— y sus 313 líneas son las listas de artefactos de categorías que dejaron de existir; están
enteras acá.

**Los snapshots `_legacy/` que viajaban dentro de `_fusion/` acompañan a sus documentos**, que es la
decisión que el análisis §6 dejaba abierta.

## Qué falta

**51 grupos** en **6 categorías de `GeometriaFactory-Api`** y **las 9 de `GeometriaFactory-Web`**, con su clasificación propuesta y su orden en
`Audit/Migracion-M10-Consolidacion-Fusion.md` §4 y §5. Mientras un grupo no se consolide, sus
documentos siguen en `<categoria>/_fusion/<Origen>/`, y **la presencia de esa carpeta declara que la
fusión no terminó**.
