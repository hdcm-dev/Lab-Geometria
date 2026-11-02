---
doc_id: RD-GEO-001
doc_type: rrd
title: La ayuda acompaña y no resuelve el trabajo
description: Regla RD-01 de la ayuda de uso de Lab-Geometria, derivada de ADR-10010, con sus prohibiciones verificables.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, docente, po]
delivery: [publico]
traces: [FIC-GEO-001]
---

# La ayuda acompaña y no resuelve el trabajo

## 1. Fuente

La regla no es nueva: **cita** el límite didáctico de
`SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-10010-La-Ayuda-De-Usuario-Acompana-Sin-Resolver-El-Trabajo.md:26`:

> **Límite didáctico:** **la ayuda no entrega el JSON ni las clases resueltas**. Puede describir el formato de
> intercambio y sus reglas, que ya son públicas en el contrato, y mostrar ejemplos que no coincidan con un trabajo
> práctico vigente.

Esta pieza la descompone en prohibiciones que se pueden comprobar. Si las dos dicen cosas distintas, manda el ADR.

## 2. Requerimiento

| ID | Audiencia | Tarea | Pieza | Criterio | Decisión |
| --- | --- | --- | --- | --- | --- |
| RD-01 | alumno | Entender qué pide cada campo sin recibir el trabajo hecho | CL-06, `docs-as-code/punto-de-uso/` y `docs-as-code/ayuda/` | Dado un texto de ayuda de uso, cuando se compara con las prohibiciones de §3, entonces no viola ninguna; instrumento: control `limite-didactico` (PR 3, en avisa) | aceptado |

## 3. Prohibiciones verificables

1. **No entrega el JSON resuelto.** Ningún texto de ayuda trae un JSON completo de envío que verifique para la figura
   del trabajo práctico vigente.
2. **No entrega las clases resueltas.** Ningún texto trae código C# de las clases de figura del trabajo.
3. **Los ejemplos no son isomorfos al TP vigente.** Un ejemplo usa figuras o dimensiones que no coinciden con las del
   trabajo práctico en curso.
4. **No explica por qué un valor está mal.** La capa `pista` del catálogo explica cómo leer «declarado» y «derivado»;
   la discrepancia concreta la comenta la capa de pistas de IA (expediente 0002), y la ayuda sólo puede enlazar a esa
   superficie.
5. **Sí puede** describir el formato de intercambio y sus reglas, que ya son públicas en el contrato.

## 4. Fundamento didáctico

El paso del ejemplo resuelto a la resolución propia funciona cuando el ejemplo no es el problema mismo: Renkl, A., &
Atkinson, R. K. (2003). *Structuring the transition from example study to problem solving in cognitive skill
acquisition: A cognitive load perspective*. Educational Psychologist, 38(1), 15-22.

## 5. Quién la revisa

La letra de la ayuda del alumno la revisa el Product Owner contra esta regla (decisión H-10 de la
[ficha](../ficha-diseno-documental.md)).
