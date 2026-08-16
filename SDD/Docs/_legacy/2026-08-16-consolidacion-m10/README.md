# Documentos absorbidos por la consolidación de la fusión (M-10)

**Fecha:** 2026-08-16
**Motivo:** `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.0
**Regla:** `Migracion-Rules.md` §4.3.2
**Estado:** **En curso.** 1 de 67 grupos consolidados

---

Los documentos de esta carpeta estaban estacionados en `<categoria>/_fusion/<Origen>/` desde la
migración estructural 6.0 → 8.x, esperando la consolidación que `Migracion-Rules.md` §4.3.2 reserva
al humano. **No se borran**: su contenido está en el documento consolidado que los reemplaza.

**Consolidar acá no es deduplicar.** El inventario midió **5,9 % de solapamiento** entre las
versiones de un mismo grupo: el 94 % del contenido es propio de una capa. Por eso el documento
consolidado es una **unión con atribución** —una subsección por proyecto de código, nombrada— y no
una selección.

## Grupos consolidados

| Grupo | Salida | Documentos absorbidos | Reemplazo |
| --- | --- | --- | --- |
| `GeometriaFactory-Api` / `08-Calidad-Y-Pruebas` / `Estrategia-Testing.md` | **S1** transposición con atribución | `Estrategia-Testing-Domain.md`, `-Application.md`, `-Infrastructure.md` | [`Estrategia-Testing.md`](../../Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Testing.md) 2.0 |

## Qué falta

**66 grupos**, con su clasificación propuesta y su orden en
`Audit/Migracion-M10-Consolidacion-Fusion.md` §4 y §5. Mientras un grupo no se consolide, sus
documentos siguen en `<categoria>/_fusion/<Origen>/`, y **la presencia de esa carpeta declara que la
fusión no terminó**.
