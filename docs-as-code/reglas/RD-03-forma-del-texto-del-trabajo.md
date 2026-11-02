---
doc_id: RD-GEO-003
doc_type: rrd
title: Forma del texto del trabajo que lee el laboratorio
description: Ficha de la regla que explica el campo Texto del trabajo de la pantalla de envío, con anclas al código que la aplica.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, docente, po]
delivery: [publico]
traces: [FIC-GEO-001, RD-GEO-001]
---

# Forma del texto del trabajo que lee el laboratorio

Ficha de regla del contexto `web/submission-text` del [catálogo del punto de uso](../punto-de-uso/catalogo.md). Explica
**el campo y el formato**; no dice cómo se calcula ninguna figura ni por qué un valor está mal
([RD-01](RD-01-ayuda-no-resuelve.md)). `RD-GEO-002` queda reservado para la regla de entrega (tramo T-4A).

## 1. Requerimiento

| ID | Audiencia | Tarea | Pieza | Criterio | Decisión |
| --- | --- | --- | --- | --- | --- |
| RD-03 | alumno | Pegar el texto que imprime su programa sabiendo qué claves lee el laboratorio | Pista bajo «Texto del trabajo» y esta ficha | Dado el texto de la pista en pantalla, cuando se compara con las claves que el validador lee, entonces nombra todas las que el alumno puede necesitar; instrumento: control `anclas` (texto en pantalla) y relectura de §2 contra el validador. La diferencia de §3 se eleva | aceptado |

## 2. La regla, con sus anclas

Cada punto, con el lugar del código que lo aplica en `origin/main`:

- **El texto entra sin transformar**: sin normalizar, sin reordenar y sin `Trim`.
  `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:384-387` (comentario de la pantalla, RN-08).
- **La raíz es una lista de figuras o una figura suelta.**
  `src/GeometriaFactory.Infrastructure/Figures/LocalFigureValidator.cs:108-113`.
- **Nombres de tipo de pieza que se reconocen**: `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo`.
  `LocalFigureValidator.cs:577-586`.
- **Claves escalares**: `Tipo`, `Largo`, `Ancho`, `Radio`, `Altura`, `Area`, `Volumen`. `LocalFigureValidator.cs:52-58`.
- **Conjuntos de componentes por tipo**: cubo `Caras`; ortoedro `Bases` o `Tapas` (sinónimas) y `Laterales`; cilindro
  `Tapas` y `Lado`. `LocalFigureValidator.cs:487-495`.
- **Una clave con otro nombre se guarda, pero no se dibuja ni se verifica.** Lo dice la pantalla:
  `WorkSubmission.razor:419-426`.

Los términos del contrato (`Tipo`, `Largo`, `Radio`…) no se traducen (decisión D-16 de la ficha).

## 3. Diferencia entre la pantalla y el validador (se eleva)

La pista de la pantalla (`WorkSubmission.razor:424-425`) nombra cuatro conjuntos de componentes: `Caras`, `Bases`,
`Laterales` y `Tapas`. El validador lee además **`Lado`** como el lateral del cilindro
(`LocalFigureValidator.cs:490`). Un alumno que sólo lee la pista no sabe que el cilindro lo lleva.

No se corrige acá: el texto visible vive en el `.razor` (ola 2, con ok por archivo, H-06). Queda registrado en el
catálogo (§3) y en la constancia del tramo T-3 del expediente 0003.

## 4. Lo que esta ficha no dice

- Cómo se calcula el área o el volumen de una figura: es parte del trabajo del alumno (RD-01, prohibición 2).
- Un JSON completo de envío: el único ejemplo es el de la pantalla, y su revisión contra el trabajo práctico vigente
  queda para el Product Owner (catálogo, §3).
- Por qué un valor declarado no coincide con el derivado: eso lo comentan las pistas de la capa de IA (expediente 0002).
