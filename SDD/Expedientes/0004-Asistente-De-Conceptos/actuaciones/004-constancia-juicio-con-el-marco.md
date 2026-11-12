# EXP-0004 · Actuación 004 — Constancia: juicio del asistente con el marco R1–R14

| Campo | Valor |
|---|---|
| Tipo | constancia |
| Fecha | 2026-11-12 |
| Autor | banco de traslado de Lab-Documentos (agente) |
| Corrige | — (complementa la [003](003-constancia-hallazgos-h1-h4.md); la 003 no se reescribe) |

**Expediente:** [EXP-0004](../README.md) · **Foliada:** 004 — no se reescribe

Se juzgó el asistente con la rúbrica R1–R14 del marco de diseño conversacional (guía académica de Lab-Documentos, anexo I). Cada criterio vale de 0 a 2, con un máximo de 28. Se acepta con 22 o más y sin ceros en R3, R4, R7 ni R12. **No se tocaron ni el código ni los datos de producción.**

## 1. Lo que ve el usuario hoy: 0/28

`geometria.aplicada.stream` corre `1.2.1-alpha.0.21+8ea64fc` (#255, 2026-11-03), y `main` (`f095ef8`) va 81 commits por delante. `POST /v1/conceptos/consultas` da 404 y `/estado` no tiene la fila de IA. Sin turnos que juzgar, cada criterio vale 0.

## 2. main encendido en una instancia propia: 13/28

`127.0.0.1:18431`, con `AI__ConceptsEnabled=true`, `HintsEnabled=true` y qwen3.5:4b. Las nueve fichas pasaron a `vigente` sólo en una copia fuera del repo. Batería de 25 preguntas × 3 corridas: 74 de 75 respuestas iguales y un 500.

| Id | Pts | Evidencia |
|---|---|---|
| R1 Intenciones y compuestas | 0 | Intención correcta en 17 de 25. Compuestas: 0 de 4 completas (Q11, Q12, Q13, Q24). Q22 («cuántos alumnos pendientes») sale como estado de un trabajo |
| R2 Anillo | 0 | No hay tabla de 30 casos o más etiquetada por un tercero |
| R3 Responde lo preguntado | 0 | 16 de 25 (64 %) |
| R4 Aclara sólo bajo la regla | 2 | 0 aclaraciones innecesarias en 75 turnos |
| R5 Anclaje | 2 | Ficha citada (`card.id`). La cifra de Q16 sale de WorkView |
| R6 Chip válido para el rol | 1 | Chips legibles, pero sin E2E por rol, y Q21, Q22, Q24 y Q25 del docente no traen destino |
| R7 Nunca callejón | 1 | 6 de 25 sin chip. En Q25 el docente recibe un consejo escrito para el alumno |
| R8 Abstención y rechazo | 1 | 7 de 9 bien. Q18 se rechaza con un motivo falso («es parte del ejercicio») |
| R9 Dice el supuesto | 1 | Q16 nombra el trabajo. Q14 y Q23 lo infieren y no lo usan |
| R10 Forma | 1 | Las formas son correctas, pero Q22 pide una cifra y recibe un concepto |
| R11 Candidatas legibles | 1 | Nada técnico a la vista. La batería no produjo ninguna aclaración F3 |
| R12 Nada ajeno al rol | 2 | Un trabajo ajeno da NotFound. Matriz en `06-Especificacion…` l.72, REQ-IA-26/32 |
| R13 Mejora percibida | 0 | No está desplegado y no hay pares a ciegas |
| R14 Mejora interna trazable | 1 | Las rondas 1–6 citan las baterías v1–v3. No todo #268 trae un caso con captura |

## 3. Hallazgos

- **H1 (despliegue, bloquea).** #264 y #266–#268 están en main y no están desplegados. Además vienen apagados (`AI__ConceptsEnabled=false`), y las fichas siguen en `borrador`, así que `DocsAsCodeConceptCorpus` no serviría ninguna.
- **H2 (500 intermitente).** El regex `Injection` de `ConceptRouter` tiene un timeout de 250 ms (`ConceptRouter.cs:28` y `:167`). Con el host cargado dio `RegexMatchTimeoutException` en 1 de 75 turnos.
- **H3.** Las compuestas no se responden enteras. En Q12 rechaza también la parte permitida.
- **H4.** La voz y los destinos están escritos para el alumno: el docente lee «el trabajo que enviaste».
- **H5.** Q22 contesta una cifra con un concepto ajeno. Q14 y Q23 no usan el trabajo abierto.
- **H6.** Fuera de alcance (Q18) y la acción del docente (Q25) se rechazan como si pidieran resolver el ejercicio.
- **H7 (fortalezas).** Los conceptos se responden bien al primer turno, con ficha y chip. Los pedidos disfrazados se rechazan siempre. No hay aclaraciones innecesarias. Es determinista.

## 4. Plan

1. **P0** — Atrapar `RegexMatchTimeoutException` en `ConceptRouter.Route` (o usar `RegexOptions.NonBacktracking`), con una prueba que fuerce el timeout.
2. **P0** — Que la mesa o el PO decidan qué fichas pasan a `vigente` (REQ-IA-41, H-10). Encender `AI__ConceptsEnabled` en la API y en la Web y desplegar main. Las tres cosas juntas.
3. **P1** — Separar las compuestas: Q11, Q12, Q13 y Q24 a la batería. Si una parte es de resolver, responder la otra.
4. **P1** — Voz y destinos del docente. «Cuántos alumnos pendientes» por función. Usar el trabajo abierto (Volumen, «Ver pistas»), diciendo el supuesto.
5. **P2** — Abstención F7 para lo que está fuera de alcance, y chip en Q17 y Q19. Tabla de anillo (R2) y juicio a ciegas en el sitio desplegado (R13).

**Comparación.** Con la misma rúbrica, la consola de PushDispatch saca 3/28 y Movilidad Urbana 18/28. Ninguno se acepta. La causa común es una intención por mensaje y la falta de un juicio a ciegas sobre lo que ve el usuario.
