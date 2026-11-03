---
doc_id: FIC-GEO-006
doc_type: proceso
title: Controles de la compuerta de docs
description: Qué verifica la compuerta de docs de Lab-Geometria, en qué ámbito avisa o bloquea y cómo se corre en local.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-03
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
| [`anclas.py`](anclas.py) | Extensión `anclas` (tramo T-3): cada fila del [catálogo del punto de uso](../punto-de-uso/catalogo.md) resuelve a un id y a un texto del `.razor` que cita |
| [`limite_didactico.py`](limite_didactico.py) | Extensión `limite-didactico` (tramo T-3): las prohibiciones verificables de [RD-01](../reglas/RD-01-ayuda-no-resuelve.md), incluida la de no repetir las plantillas de la capa de IA |
| [`pista_sin_enlaces.py`](pista_sin_enlaces.py) | Extensión `pista-sin-enlaces` (tramo T-3): la capa `pista` del catálogo no lleva enlaces |
| [`catalogo_pu.py`](catalogo_pu.py) | Lector del catálogo que comparten las tres extensiones del punto de uso; no es un control |
| [`recuento_openapi.py`](recuento_openapi.py) | Extensión `recuento-openapi` (tramo T-6): la [instantánea OpenAPI](../api/openapi-v1.json) contra la tabla de operaciones de `Contratos-REST.md` §3 y §3.1 (cifra del título, rutas de más o de menos y códigos sin documentar) |
| [`openapi.sh`](openapi.sh) | Controles OpenAPI con Docker (tramo T-6): oasdiff `breaking` de Tufin y Spectral, con su propio par rojo/verde. Lo corre el job `openapi` de `docs.yml` |
| [`spectral.yaml`](spectral.yaml) | Las tres reglas de Spectral, propias y sin `extends`: `operacion-con-id`, `operacion-con-resumen` y `operacion-con-error-documentado` |
| [`enlaces_externos.py`](enlaces_externos.py) | Extensión `enlaces-externos` (tramo T-7, revisión externa R-G2): enlaces `http`/`https` de la documentación, con red. La corre sólo el carril programado `.github/workflows/docs-programado.yml`, no la compuerta |
| [`enlaces-excepciones.txt`](enlaces-excepciones.txt) | Excepciones de `enlaces-externos`: `URL \| motivo \| vence AAAA-MM-DD`. Vencida, no se aplica y se avisa |
| [`autoprueba.sh`](autoprueba.sh) | Par rojo/verde plantado de cada control, sobre un corpus temporal |

Las extensiones reutilizan el lector de frontmatter de la copia sin modificarla. Son el apartamiento parcial de
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
| `anclas` | avisa (fijo) | — | — |
| `limite-didactico` | avisa; a bloquea después de dos corridas verdes de su par | — | — |
| `pista-sin-enlaces` | avisa (fijo) | — | — |
| `recuento-openapi` | avisa (fijo); el contrato que lee es de `SDD/Docs/**` y lo que encuentra allí se eleva | — | — |
| `oasdiff` y `spectral` (job `openapi`) | avisa (fijo); su autoprueba bloquea | — | — |

Los tres controles del punto de uso no siguen a `DOCS_MODO_DOCS_AS_CODE`: quedan en avisa hasta que un PR propio
los cambie en `scripts/docs-local.sh` (resolución del expediente 0003, §5.2). La prohibición 3 de RD-01 (ejemplos no
isomorfos al TP vigente) no tiene control: el repo no declara el TP, y la revisa el Product Owner (H-10).

Todo arranca en **avisa** (decisión D-10): un hallazgo queda como advertencia del PR y el check sigue verde.
`docs-as-code/**` pasa a **bloquea** cambiando `DOCS_MODO_DOCS_AS_CODE` a `bloquea` en `docs.yml`, cuando su línea
de base sea 0 y el par rojo/verde esté demostrado. Fuera de esa carpeta no se bloquea en la ola 1, porque un rojo
frenaría la fusión automática de otras líneas de trabajo. El SDD se genera: lo que la compuerta encuentra allí **se
eleva, no se corrige**.

`esquema` sólo valida frontmatter YAML. Lo heredado que declara sus metadatos en una tabla Campo/Valor se cuenta como
«sin frontmatter» y no se valida.

### 2.0 Enlaces externos (carril programado)

`enlaces-externos` no corre en la compuerta, que es sin red: lo corre `docs-programado.yml` los lunes y a mano. Avisa
siempre. El issue de aviso lleva la etiqueta fija `docs-enlaces` y se busca por ella; con caídos se abre o se comenta,
y sin caídos se comenta y se cierra. Su par rojo/verde está en `autoprueba.sh`, contra un servidor local que devuelve
404, y cubre también la excepción vigente, la vencida y la mal formada.

### 2.1 Controles OpenAPI (tramo T-6)

- **`recuento-openapi`** compara la instantánea con `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md`:
  la cifra en palabras del título de la §3, cada `VERBO /ruta` de la tabla de §3.1 en las dos direcciones (error) y
  los códigos que declara cada punto en la tabla de §3 frente a los que documenta la instantánea (aviso; los resuelve
  el PR de `WithSummary`/`ProducesProblem`, decisión H-09). Corre en `scripts/docs-local.sh`, sin red.
- **oasdiff** (`tufin/oasdiff:v1.11.4`) corre `breaking --fail-on ERR` de la instantánea de la base contra la del
  árbol, y `changelog` para el registro. Una operación nueva sale como `endpoint-added` (informativo), no como
  cambio que rompe.
- **Spectral** (`stoplight/spectral:6.15.0`) corre las tres reglas de [`spectral.yaml`](spectral.yaml).
- Los dos corren en el job `openapi` de `docs.yml` con `docs-as-code/controles/openapi.sh`: necesitan Docker, y la
  compuerta principal sigue sin red. Antes de mirar la instantánea, el guion demuestra su par rojo/verde (una ruta
  retirada para oasdiff y una operación sin `operationId`, sin `summary` y sin respuesta de error para Spectral).

## 3. Cómo se corre

```bash
scripts/docs-local.sh                                   # igual que en el runner
DOCS_MODO_DOCS_AS_CODE=bloquea scripts/docs-local.sh    # cómo correría docs-as-code/** en bloquea
bash docs-as-code/controles/autoprueba.sh               # sólo el par rojo/verde
python3 -I docs-as-code/controles/delivery_publico.py --exigir docs-as-code   # un control suelto
python3 -I docs-as-code/controles/recuento_openapi.py                         # el recuento OpenAPI
bash docs-as-code/controles/openapi.sh origin/main                            # oasdiff y Spectral (con Docker)
python3 -I docs-as-code/controles/enlaces_externos.py \
  --excepciones docs-as-code/controles/enlaces-excepciones.txt                # enlaces externos (con red)
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
