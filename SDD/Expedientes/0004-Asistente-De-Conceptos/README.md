# EXP-0004 — Asistente de conceptos: «Preguntá un concepto»

| Campo | Valor |
|---|---|
| Número | **EXP-0004** |
| Título | Rebanada del asistente de conceptos: fichas de ayuda, frontera didáctica, destinos y huella |
| Estado | **en trámite** (actuación 001): construido en `feat/asistente-rebanada`, **sin PR**, apagado por omisión (`AI__ConceptsEnabled=false`); las nueve fichas en `borrador` hasta la firma del PO |
| Apertura | 2026-11-11 |
| Rama | `feat/asistente-rebanada` desde `main` `53a2810` |
| Partes | **Product Owner** (firma las fichas y decide encender); **banco de traslado de Lab-Documentos** (agente, aplica los conocimientos de la guía de IA a un segundo sistema real) |
| Objeto | Probar que los conocimientos de la capa de IA de Lab-Documentos se trasladan a Geometría sin romper su límite didáctico: el asistente ayuda a entender y a encontrar, nunca hace el ejercicio |
| Especificación | [`ai-layer/06-Especificacion-Asistente-De-Conceptos.md`](../../../ai-layer/06-Especificacion-Asistente-De-Conceptos.md) |
| Base | `main` `53a2810` |

**Forma.** La de [EXP-0002](../0002-Capa-De-IA-Pistas-Y-Borrador/README.md): carátula, actuaciones foliadas que no
se reescriben y evidencia con SHA-256.

## 1. Índice foliado de actuaciones

| Nº | Fecha | Tipo | Autor | Resumen | Enlace |
|---|---|---|---|---|---|
| 001 | 2026-11-11 | constancia | banco de traslado (agente) | Rebanada construida; 874 pruebas en verde y 1 omitida; batería ×3 contra el modelo 24/24 en las tres; ADR-14013 a 14015; apartamientos declarados | [001](actuaciones/001-constancia-construccion-de-la-rebanada.md) |

## 2. Índice de evidencia

| Id | Qué | Archivo |
|---|---|---|
| E-001 | build y pruebas de la solución en contenedor | [E-001](evidencia/E-001-build-y-pruebas.txt) |
| E-002 | batería v1 ×3 contra el modelo, con SHA-256 de cada corrida | [E-002](evidencia/E-002-bateria-x3.txt) · corridas en [`ai-layer/ensayos/corrida-conceptos-2026-11-11/`](../../../ai-layer/ensayos/corrida-conceptos-2026-11-11/) |

## 3. Punto de continuación

1. El PO revisa y firma las nueve fichas de `docs-as-code/ayuda/` (`status: vigente`); hasta entonces el servicio real
   contesta «No está en la ayuda» a todo concepto.
2. Puntuación docente de la redacción con modelo contra la ficha (criterio 17): mientras no se haga, el redactor queda
   apagado (`AI__ConceptsModelWriter=false`) y responde la ficha.
3. Capturas de la app servida (criterio 20) y PR con conformación automática.
