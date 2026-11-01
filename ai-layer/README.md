# ai-layer · Especificación de una capa de IA para la Fábrica de Geometría

> Carpeta de especificación. **Nada de esto está construido** y nada toca `src/`, `tests/`, el intake ni el
> despliegue. Expediente: [`SDD/Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/`](../SDD/Expedientes/0002-Capa-De-IA-Pistas-Y-Borrador/README.md).
> Fecha: 2026-11-01. Base: `main` `5f07b12`.

## Índice

- [1. Qué es esta carpeta](#1-qué-es-esta-carpeta)
- [2. Documentos](#2-documentos)
- [3. Cómo se produjo](#3-cómo-se-produjo)
- [4. Cómo se verifica](#4-cómo-se-verifica)
- [5. Límites](#5-límites)

## 1. Qué es esta carpeta

El caso de uso público de un conocimiento de especificación de capas de IA para servicios .NET. Especifica
dos capacidades de nivel A0 (el modelo sólo propone texto, sin herramientas):

- **V1 · Pistas:** tres pistas graduadas por cada discrepancia entre el valor declarado y el derivado, sin
  dar el valor ni el texto corregido.
- **V2 · Borrador de comentario:** un borrador que el docente edita y firma al resolver un trabajo.

Las dos respetan las reglas del producto: el validador determinista decide (G-6, G-7), el JSON del alumno no
se corrige (X-4), no hay calificación (F-21) y la ayuda no resuelve el trabajo (`ADR-10010` de la Web).

## 2. Documentos

| Archivo | Qué contiene |
|---|---|
| [`01-Especificacion-V1-Pistas.md`](01-Especificacion-V1-Pistas.md) | arc42 reducido (§1, 3, 5, 8, 9, 10, 11), C4, ficha, prompt, requisitos y riesgos de V1 |
| [`02-Especificacion-V2-Borrador-De-Comentario.md`](02-Especificacion-V2-Borrador-De-Comentario.md) | lo mismo para V2, sólo lo que cambia respecto de V1 |
| [`03-ADR.md`](03-ADR.md) | ADR-IA-01 a ADR-IA-06 |
| [`04-Propuesta-Norma-De-Nomenclatura.md`](04-Propuesta-Norma-De-Nomenclatura.md) | filas para la Norma de Nomenclatura §6, antes de cualquier identificador |
| [`05-Checklist-Completitud.md`](05-Checklist-Completitud.md) | checklist binario de V1 y V2 |
| [`ensayos/`](ensayos/) | criterio R-LLM, arnés, banco sintético y resultado de la prueba de concepto del prompt de V1 |

## 3. Cómo se produjo

Se aplicó **sólo** el Conocimiento de especificación v0 (borrador de la etapa E4 de la guía 16 de
Lab-Documentos), sin abrir la guía, para medir si el conocimiento alcanza por sí solo. Lo que faltó, sobró
o confundió está registrado como hallazgos K-nn en la bitácora pública de esa guía (etapa E5).

## 4. Cómo se verifica

| Control | Comando | Resultado del 2026-11-01 |
|---|---|---|
| Requisitos completos | `python3 -I E4-verificar-requisitos.py 01-*.md 02-*.md` (script de la bitácora E4) | 16 y 8 requisitos, 0 incompletos, rc 0 |
| Diagramas | `tools/medios/mermaid.sh` del laboratorio | 4 de 4 ok, rc 0 |
| Filtro de referencia | `python3 -I ensayos/pistas.py --autoprueba` | 8/8, rc 0 |
| Conducta del modelo | `ensayos/Resultado.md` | ver ese archivo |

## 5. Límites

- Las pruebas de los requisitos están **especificadas**, no escritas.
- La revisión humana del checklist está pendiente.
- El ingreso al intake, el servidor de PROD y el número de las ADR son del Product Owner.
