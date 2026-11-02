---
doc_id: FIC-GEO-003
doc_type: proceso
title: Cómo intervenir en la documentación de Lab-Geometria
description: Escenario, dónde vive cada pieza, validación local, compuerta, quién decide y excepciones del sistema documentador de Lab-Geometria.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, po, docente]
delivery: [publico]
traces: [FIC-GEO-001]
---

# Cómo intervenir en la documentación de Lab-Geometria

Instancia del artefacto A-06 de la [ficha](ficha-diseno-documental.md). Contesta seis preguntas.

## 1. Escenario y clases activas

Panel web con dos roles (alumno y docente) y un servicio con API cuya forma se publica como OpenAPI. La especificación
es un SDD generado. Las clases activas están en la ficha, §3.

## 2. Dónde vive cada pieza

| Pieza | Dónde | ¿Se edita a mano? |
| --- | --- | --- |
| Especificación del producto | `SDD/Docs/**` | **No.** Se regenera; lo desalineado se eleva al orquestador SDD (`AGENTS.md:61`) |
| Contrato derivado | `AGENTS.md` | **No.** Derivado de `SDD/Docs/Producto/11-Documentacion/Contrato-Agentes.md` (`AGENTS.md:3-5`) |
| Casos | `SDD/Expedientes/NNNN-…/` | Sí, por folios nuevos; un folio publicado no se reescribe |
| Ficha, audiencias, esquema, glosario, reglas | `docs-as-code/` | Sí, por PR |
| Catálogo del punto de uso | `docs-as-code/punto-de-uso/` (PR 3) | Sí, por PR; el texto visible sigue en los `.razor` |
| Capa de IA (pistas) | `ai-layer/**`, expediente 0002 | No desde este sistema: es de otra sesión |
| Índice para agentes | ia-db de Lab-Geometria.Documentacion | Sí, por PR en ese repo |

## 3. Validación local

Hasta el PR 2 no hay guion propio ni configuración de lint en el repo. El PR 2 trae la copia con huella de
`docs-guard` (`.docs-guard/`), con su configuración de `markdownlint-cli2` y los controles de esquema, trazas y enlaces.

Desde el PR 2, `scripts/docs-local.sh` corre la misma compuerta que el workflow. Usa `python3`, que **la imagen del SDK
no trae** (`AGENTS.md:81`): se corre en el host o en el runner, no dentro del contenedor del SDK.

## 4. Workflow y runner de la compuerta

`.github/workflows/docs.yml` (PR 2): `pull_request` y `push` a main, **sin filtro de rutas**, `permissions: contents:
read`, sin secretos, acciones con versión fija, en `ubuntu-latest`. No compila la Api y no toca `ci.yml`. Todo arranca en
**avisa**; sólo `docs-as-code/**` pasa a **bloquea**.

## 5. Quién decide

| Qué | Quién |
| --- | --- |
| Decisiones H (ficha, §4) y letra de la ayuda | Product Owner / docente |
| PRs de `docs-as-code/` | El agente, con fusión automática bajo G1–G4 (`AGENTS.md:65`, `:71`) |
| `ai-layer/**`, expediente 0002, código de pistas | La sesión de la capa de IA |
| `SDD/Docs/**` y `AGENTS.md` | El orquestador SDD, por elevación |

`README.md`, `AGENTS.md` y `.github/workflows/ci.yml` son compartidos: quien los toca avisa antes a la otra sesión, y el
que fusiona segundo rebasea.

## 6. Excepciones

- Ninguna pieza lleva un `delivery:` distinto de `publico` (repos públicos).
- La ayuda de uso no resuelve el trabajo: [RD-01](reglas/RD-01-ayuda-no-resuelve.md).
- Fusionar a main no despliega: `deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`, y el despliegue del servicio es
  manual, a cargo del Product Owner. Toda fusión sí avanza la versión de la próxima imagen, porque la imagen copia `.git`
  (`deploy/Dockerfile:33`).
