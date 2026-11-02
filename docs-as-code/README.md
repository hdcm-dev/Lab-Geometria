---
doc_id: FIC-GEO-005
doc_type: indice
title: Documentación como código de Lab-Geometria
description: Índice de la carpeta docs-as-code, con la ficha de diseño, el registro de audiencias y las piezas de la ola 1 del expediente 0003.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, po, docente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Documentación como código de Lab-Geometria

Esta carpeta es la parte **escrita a mano** del sistema documentador del repositorio. La especificación del producto
sigue en `SDD/Docs/`, que se genera y no se edita desde acá: lo que esta carpeta encuentra desalineado allí **se eleva,
no se corrige** (`AGENTS.md:61`). El caso que la abrió es el expediente
[`SDD/Expedientes/0003-DocsAsCode`](../SDD/Expedientes/0003-DocsAsCode/README.md).

| Pieza | Qué es | Archivo |
| --- | --- | --- |
| Ficha de diseño documental (A-01) | Inventario medido, decisiones D-01 a D-18, catálogo activo, decisiones H | [`ficha-diseno-documental.md`](ficha-diseno-documental.md) |
| Registro de audiencias (A-12) | A quién se entrega cada pieza y con qué nivel; en este repo, todo `publico` | [`AUD-geometria.md`](AUD-geometria.md) |
| Esquema de frontmatter (A-03, nota) | Los campos que lleva toda pieza nueva de esta carpeta | [`esquema-frontmatter.md`](esquema-frontmatter.md) |
| Cómo intervenir (A-06) | Dónde vive cada pieza, quién decide y cómo se valida | [`como-intervenir.md`](como-intervenir.md) |
| Glosario | Un término por concepto: valor declarado, derivado, observación, advertencia, pista | [`glosario.md`](glosario.md) |
| Regla RD-01 | La ayuda acompaña y no resuelve el trabajo | [`reglas/RD-01-ayuda-no-resuelve.md`](reglas/RD-01-ayuda-no-resuelve.md) |

Lo que llega en los PR siguientes de la ola 1 (compuerta `docs.yml`, catálogo del punto de uso, regla de entrega) se
suma a esta tabla en su propio PR.

**Efecto sobre el producto.** Nada de esta carpeta entra en una imagen: `deploy/Dockerfile:26-28` copia
`Directory.Build.props`, la solución y `src/`, y `:33` copia `.git`; `deploy/Dockerfile.web:42-46` suma `visor/` y
`scripts/build-visor.sh`. Fusionar un cambio de esta carpeta no dispara
ningún despliegue (`deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`); el despliegue es manual, a cargo del
Product Owner.
