---
doc_id: RD-GEO-002
doc_type: rrd
title: Regla de entrega de la documentación de Lab-Geometria
description: Regla de entrega del expediente 0003 para el canal de agente; todo documento de estos repos públicos se entrega como publico.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, docente, po]
delivery: [publico]
traces: [FIC-GEO-001, RD-GEO-001]
---

# Regla de entrega de la documentación de Lab-Geometria

Canal de agente de la [ficha de diseño documental](ficha-diseno-documental.md): esta pieza y la ia-db de
Lab-Geometria.Documentacion. `AGENTS.md` no se edita acá (`AGENTS.md:3-5`); el mismo texto se elevó al orquestador SDD
para que lo regenere (expediente 0003, folio 003).

## 1. Texto de la regla

El mismo texto vale en este repositorio, en la ia-db y en la elevación:

```text
Regla de entrega (expediente 0003). En Lab-Geometria y Lab-Geometria.Documentacion, ambos públicos, todo
documento lleva `delivery: publico`; no se crean piezas `producto`, `restringido` ni `interno`. El agente
hereda la audiencia de su usuario y por este canal sólo recibe `publico`. La ayuda de uso explica el campo
y el formato; la discrepancia la comenta la capa de pistas (expediente 0002) y la ayuda no la duplica.
La forma de la API es pública cuando `Documentacion:Publicada` está encendida (/openapi/v1.json,
/documentacion); su instantánea la regenera la automatización. Control: `delivery-publico` en docs.yml.
```

## 2. Requerimiento

| ID | Audiencia | Tarea | Pieza | Criterio | Decisión |
| --- | --- | --- | --- | --- | --- |
| RD-02 | agente | Saber qué nivel de entrega lleva un documento de estos repositorios | Esta pieza, el README raíz y la ia-db (T-4B) | Dado un documento de `docs-as-code/**`, cuando se lee su frontmatter, entonces `delivery` es `[publico]`; instrumento: control `delivery-publico` (en avisa, ola 1) | aceptado |

## 3. Anclas

- **Los dos repositorios son públicos:** `README.md:3` («público de consulta»); medición en el folio 002 del expediente
  0003.
- **Las audiencias y su nivel:** [`AUD-geometria.md`](AUD-geometria.md).
- **La ayuda no resuelve el trabajo:** [RD-01](reglas/RD-01-ayuda-no-resuelve.md).
- **Llave de publicación de la API:** `src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:43`
  (`Documentacion:Publicada`) y `:109-110` (siempre en desarrollo).
- **Rutas del documento y del explorador:** `src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:37` y `:40`.
- **Control `delivery-publico`:** `scripts/docs-local.sh:87` (`docs-as-code/**`) y `:101` (`SDD/Docs/**`, en avisa), que
  corre `.github/workflows/docs.yml:44`.

La instantánea de OpenAPI que la automatización regenera es de la ola 2 (job `instantanea-openapi`); hoy no existe en
el repositorio.
