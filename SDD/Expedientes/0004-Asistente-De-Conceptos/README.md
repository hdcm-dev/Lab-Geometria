# EXP-0004 — Asistente de conceptos: «Preguntá un concepto»

| Campo | Valor |
|---|---|
| Número | **EXP-0004** |
| Título | Rebanada del asistente de conceptos: fichas de ayuda, frontera didáctica, destinos y huella |
| Estado | **en trámite** (actuaciones 001 a 004): construido en `feat/asistente-rebanada`, apagado por omisión (`AI__ConceptsEnabled=false`); las nueve fichas en `borrador` hasta la firma del PO |
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
| 002 | 2026-11-11 | constancia | banco de traslado (agente) | Criterios 8, 13, 27, 29, 30 y 31 cerrados; batería ×3 con modelo y con fichas, 24/24 en las seis, PENDIENTE de lectura docente; pendientes del PO nombrados | [002](actuaciones/002-constancia-cierre-de-criterios-abiertos.md) |
| 003 | 2026-11-11 | constancia | banco de traslado (agente) | Hallazgos H1–H4: enrutador y filtro endurecidos (13 rojas con el código previo), ficha visible en la negativa y chip según estado, `ficha-dicta` en docs-as-code, desborde de Observaciones (previo a #266) corregido; batería v2 ×3 con modelo 24/24, PENDIENTE de lectura docente; CLS 0 y sin tema oscuro; sesión docente y `/estado`; verificación: número en letras sólo como valor de una medida (parser), filtro sin falsos positivos (13/56 → 0/56), chip del docente según el rol, batería v2 ×3 con y sin modelo 24/24 | [003](actuaciones/003-constancia-hallazgos-h1-h4.md) |
| 004 | 2026-11-12 | constancia | banco de traslado (agente) | Juicio con el marco R1–R14: producción 0/28 (no desplegado, 404) y main encendido local 13/28 (R3=0), no se acepta; 500 intermitente por timeout de regex; plan P0 (atrapar el timeout, fichas vigentes, encender y desplegar) a P2 | [004](actuaciones/004-constancia-juicio-con-el-marco.md) |

## 2. Índice de evidencia

| Id | Qué | Archivo |
|---|---|---|
| E-001 | build y pruebas de la solución en contenedor | [E-001](evidencia/E-001-build-y-pruebas.txt) |
| E-003 | mutación de la regla «pide resolver» (rojo y verde) | [E-003](evidencia/E-003-mutacion-pide-resolver.txt) · [parche](evidencia/E-003-mutacion-pide-resolver.patch) |
| E-004 | recall@3 por rol y su autoprueba | [E-004](evidencia/E-004-recall-por-rol.txt) |
| E-005 | veredictos recalculados de las tres carpetas de corridas, con SHA-256 | [E-005](evidencia/E-005-bateria-v2-veredicto.txt) |
| E-006 | pruebas de la solución en contenedor tras la act. 002: 884 en verde, 1 omitida (95 · 90 · 699) | [E-006](evidencia/E-006-pruebas-act-002.txt) |
| E-007 | H1: pruebas nuevas contra el código de `bcb0896` (13 rojas) y la batería completa con el código corregido (95 · 99 · 707, 1 omitida) | [E-007](evidencia/E-007-h1-pruebas-rojo-verde.txt) · capturas en [`evidencia/capturas-act-003/`](evidencia/capturas-act-003/) |
| E-008 | verificación de la 003: pruebas nuevas contra `e6b0371` (16 rojas) y la batería completa con el código corregido (95 · 142 · 709, 1 omitida) | [E-008](evidencia/E-008-verificacion-rojo-verde.txt) |
| E-002 | batería v1 ×3 contra el modelo, con SHA-256 de cada corrida | [E-002](evidencia/E-002-bateria-x3.txt) · corridas en [`ai-layer/ensayos/corrida-conceptos-2026-11-11/`](../../../ai-layer/ensayos/corrida-conceptos-2026-11-11/) |

## 3. Punto de continuación

Lo que queda es del PO (actuación 002 §3): firmar las fichas; leer las respuestas en `lectura-docente.jsonl`
(`no-dicta` / `dicta`); puntuar el modelo contra la ficha (hasta entonces `AI__ConceptsModelWriter=false`); traer
preguntas reales de alumnos; y decidir el encendido. Del agente: capturas, sesión por rol en la Web, crítica y PR
(actuación 002 §4). En Container.Lab-Geometria: sumar las claves del manifiesto a su plantilla y contrastarlas en su CI.
