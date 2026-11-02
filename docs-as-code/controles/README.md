---
doc_id: FIC-GEO-006
doc_type: proceso
title: Controles de la compuerta de docs
description: Qué verifica la compuerta de docs de Lab-Geometria, en qué ámbito avisa o bloquea y cómo se corre en local.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Controles de la compuerta de docs

Artefacto A-13 de la [ficha de diseño documental](../ficha-diseno-documental.md). La compuerta es el workflow
`.github/workflows/docs.yml`, que corre `scripts/docs-local.sh`: el mismo guion en el runner y en local. Es «sin
probar» en el sentido de la ficha hasta que el check `docs` dé verde en un PR y en `main`.

## 1. Piezas

| Pieza | Qué es |
| --- | --- |
| `.docs-guard/` | Copia con huella de `docs-guard` (decisión D-09, artefacto A-09): `dg.py` y los tres esquemas, literales, con su SHA-256 y el commit de origen en `.docs-guard/source.lock`. No se edita acá |
| `.docs-guard/prefijos.txt` | Registro cerrado de prefijos de este repo (A-02): `FIC`, `AUD`, `RD`, `PU` y el heredado `DOC` del SDD |
| `.docs-guard/excepciones.txt` | Excepciones con huella (A-08). Vacío |
| [`delivery_publico.py`](delivery_publico.py) | Extensión `delivery-publico`: todo `delivery:` vale `publico` (D-18) |
| [`enlaces_internos.py`](enlaces_internos.py) | Extensión `enlaces-internos`: destinos relativos y anclas, sin red |
| [`autoprueba.sh`](autoprueba.sh) | Par rojo/verde plantado de cada control, sobre un corpus temporal |

Las dos extensiones reutilizan el lector de frontmatter de la copia sin modificarla. Son el apartamiento parcial de
D-09 que la resolución del expediente 0003 declara y avisa al Product Owner (K-13). `enlaces-internos` reemplaza a
lychee, que es lo que usa `docs-guard` para el mismo control: la compuerta de este repo corre sólo con el `python3` del
runner y no baja herramientas.

## 2. Qué se mira y con qué modo

| Paso | `docs-as-code/**` | `README.md` | `SDD/Docs/**` (sin `_legacy/`) |
| --- | --- | --- | --- |
| `huella`: la copia coincide con `source.lock` | bloquea (todo el repo) | | |
| `autoprueba`: el par rojo/verde de cada control | bloquea (todo el repo) | | |
| `esquema` (`dg.py esquema`) | avisa | avisa | avisa |
| `id-unico` (`dg.py trazas`: `id-duplicado` y trazas que no resuelven) | avisa | — | avisa |
| `fuera` (`dg.py fuera`) | avisa | — | — |
| `enlaces-internos` | avisa | avisa | avisa |
| `delivery-publico` (en `docs-as-code/**` con `--exigir`) | avisa | — | avisa |

Todo arranca en **avisa** (decisión D-10): un hallazgo queda como advertencia del PR y el check sigue verde.
`docs-as-code/**` pasa a **bloquea** cambiando `DOCS_MODO_DOCS_AS_CODE` a `bloquea` en `docs.yml`, cuando su línea
de base sea 0 y el par rojo/verde esté demostrado. Fuera de esa carpeta no se bloquea en la ola 1, porque un rojo
frenaría la fusión automática de otras líneas de trabajo. El SDD se genera: lo que la compuerta encuentra allí **se
eleva, no se corrige**.

`esquema` sólo valida frontmatter YAML. Lo heredado que declara sus metadatos en una tabla Campo/Valor se cuenta como
«sin frontmatter» y no se valida.

## 3. Cómo se corre

```bash
scripts/docs-local.sh                                   # igual que en el runner
DOCS_MODO_DOCS_AS_CODE=bloquea scripts/docs-local.sh    # cómo correría docs-as-code/** en bloquea
bash docs-as-code/controles/autoprueba.sh               # sólo el par rojo/verde
python3 -I docs-as-code/controles/delivery_publico.py --exigir docs-as-code   # un control suelto
```

Corre sólo en el host o en el runner: necesita `python3`, que la imagen del SDK no trae (`AGENTS.md:81`). Código de
salida: 0 verde, 1 rojo en un paso que bloquea, 2 error de uso.

## 4. Cómo se actualiza la copia

1. Traer `dg.py` y `esquemas/*.json` de `tools/docs-guard/` del commit nuevo del repositorio de origen.
2. Cambiar `sha`, `fecha` y las líneas `sha256` de `.docs-guard/source.lock`.
3. Correr `scripts/docs-local.sh`: el paso `huella` y la autoprueba tienen que dar verde.

## 5. Cómo se agrega un control

1. Escribir la extensión en esta carpeta, con la biblioteca estándar de Python.
2. Sumar su par rojo/verde a `autoprueba.sh`.
3. Medir su línea de base y agregar el paso a `scripts/docs-local.sh` en avisa.
