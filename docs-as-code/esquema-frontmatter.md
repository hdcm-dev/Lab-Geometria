---
doc_id: FIC-GEO-002
doc_type: anexo
title: Esquema de frontmatter de docs-as-code
description: Campos obligatorios, vocabulario y reglas de identificador para toda pieza nueva de la carpeta docs-as-code de Lab-Geometria.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Esquema de frontmatter de docs-as-code

Nota del artefacto A-03 de la [ficha](ficha-diseno-documental.md). El esquema JSON que la compuerta lee
(`.docs-guard/esquemas/cuerpo.schema.json`) llega en el PR 2; esta nota fija lo que exige y por qué.

## 1. Campos

Toda pieza Markdown nueva de `docs-as-code/` abre con este bloque YAML: los diez campos más `description`, y
`delivery` por la decisión D-18.

```yaml
doc_id: FIC-GEO-nnn            # prefijo + segmento GEO + número; único en todo el repo
doc_type: ficha                # del vocabulario de §2
title: Título
description: De 10 a 160 caracteres.
status: borrador               # borrador | vigente | obsoleto
origin: ia-generated           # human | ia-assisted | ia-generated
confidence: media              # alta | media | baja; obligatorio si origin no es human
owner: responsable
last_review: AAAA-MM-DD
audience: [desarrollo, agente] # sólo orienta la escritura
delivery: [publico]            # a quién se entrega; en este repo, siempre publico
traces: [FIC-GEO-001]          # IDs a los que traza; a la ficha, salvo la ficha misma
```

Un valor con «: » va entre comillas. El ID se declara **una sola vez**, en `doc_id`; el H1 no lo repite, porque
`doc_id` y `# ID —` en el mismo archivo dan `id-duplicado` en la compuerta.

## 2. Vocabulario

| Campo | Valores admitidos |
| --- | --- |
| `doc_type` | `ficha`, `anexo`, `proceso`, `indice`, `rrd`, `registro-audiencias` (los que usa esta carpeta, dentro de la lista de la compuerta de origen) |
| `audience` | `humano`, `agente`, `estudiante`, `docente`, `desarrollo`, `qa`, `devops`, `po`, `analista`, `persona-usuaria` |
| `delivery` | `publico`. Es el techo de estos repos públicos: no se crean `producto`, `restringido` ni `interno` ([registro de audiencias](AUD-geometria.md)) |

`audience` y `delivery` no son lo mismo: `audience` dice para quién se escribe; `delivery`, a quién se entrega.
`audiencias:`, `audiences:` y variantes cercanas dan aviso.

## 3. Prefijos

| Prefijo | Para | Ejemplo |
| --- | --- | --- |
| `FIC` | ficha, notas y anexos del sistema documentador | `FIC-GEO-001` |
| `AUD` | registro de audiencias | `AUD-GEO-001` |
| `RD` | regla o requerimiento documental (las filas del RRD usan `RD-nn`) | `RD-GEO-001` |
| `PU` | catálogo del punto de uso (PR 3) | `PU-GEO-001` |

Los cuatro se comprobaron libres en todo el repo (ficha, §1, comando 9). El registro que lee la compuerta es
`.docs-guard/prefijos.txt` (PR 2).

## 4. Qué pasa con lo heredado

Lo que ya existe fuera de esta carpeta (`SDD/Docs/**`, `README.md`) puede declarar sus metadatos en YAML **o** en una
tabla Campo/Valor, como hace `SDD/Docs/README.md:3-15`. Ahí la compuerta sólo **avisa**: no se migra nada y el corpus
generado no se edita. En `docs-as-code/**` el esquema arranca en avisa y pasa a **bloquea** con el par rojo/verde
(decisión D-10).
