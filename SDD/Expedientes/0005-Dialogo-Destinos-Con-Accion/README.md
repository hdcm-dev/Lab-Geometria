# EXP-0005 — Diálogo: los destinos abren la acción

| Campo | Valor |
|---|---|
| Número | **EXP-0005** |
| Título | «Ver pistas» y «Ver un ejemplo» abren la acción (deuda R-06, D-2 y D-3 del EXP-0004) |
| Estado | **en trámite** (actuación 002): construido en `feat/0005-dialogo-destinos-con-accion`; la IA sigue apagada por omisión |
| Apertura | 2026-11-12 |
| Rama | `feat/0005-dialogo-destinos-con-accion` desde `main` `a672f85` |
| Partes | **Product Owner** (decide encender); **banco de traslado de Lab-Documentos** (agente: rebanada C1 elegida al contrastar una aplicación a ciegas de los conocimientos de diálogo con Geometría) |
| Objeto | Que el chip del asistente lleve al lugar exacto (K2 §5.4) sin dar números ni usar el modelo |
| Especificación | [`ai-layer/06-Especificacion-Asistente-De-Conceptos.md`](../../../ai-layer/06-Especificacion-Asistente-De-Conceptos.md), tabla del catálogo |
| Base | `main` `a672f85` |

**Forma.** La de [EXP-0004](../0004-Asistente-De-Conceptos/README.md): carátula, actuaciones foliadas que no se
reescriben y evidencia con SHA-256.

## 1. Índice foliado de actuaciones

| Nº | Fecha | Tipo | Autor | Resumen | Enlace |
|---|---|---|---|---|---|
| 001 | 2026-11-12 | constancia | banco de traslado (agente) | `?pistas=1` y `?ejemplo=1`; catálogo con `OpensWith` validado contra la página; 3 rojas con el código previo y 3 con la mutación; 95 · 170 (16 omitidas) · 715 (1 omitida), 0 rojas; batería v3 ×3 sin modelo 24/24 (la v2 de control da 15/24 con la v3); capturas 1280 y 390 sin desborde | [001](actuaciones/001-constancia-destinos-con-accion.md) |
| 002 | 2026-11-12 | constancia | banco de traslado (agente) | crítica de diseño 7,5: chip de pistas sólo con `AI:HintsEnabled`, rótulo «de la primera advertencia», capturas oscuras duplicadas retiradas; 95 · 170 (16) · 716 (1); batería v3 ×3 24/24 con pistas encendidas | [002](actuaciones/002-constancia-critica-de-diseno.md) |

## 2. Índice de evidencia

| Id | Qué | Archivo |
|---|---|---|
| E-001 | pruebas nuevas contra el código previo, mutación y batería completa | [E-001](evidencia/E-001-pruebas-rojo-verde.txt) |
| E-002 | batería v3 ×3 sin modelo con SHA-256, y rojo de la v3 sobre registros previos | [E-002](evidencia/E-002-bateria-v3-x3.txt) · corridas en [`ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-sin-modelo/`](../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-sin-modelo/) |
| E-003 | capturas del destino abierto a 1280 y 390 (sólo claras desde la act. 002) | [`evidencia/capturas/`](evidencia/capturas/) (con `SHA256SUMS`) |
| E-004 | crítica de diseño: rojo, suites, batería v3 con pistas encendidas y control apagado | [E-004-critica-de-diseno](evidencia/E-004-critica-de-diseno.txt) |

## 3. Punto de continuación

Lectura docente de la batería (sigue pendiente, como en EXP-0004) y la decisión del PO de encender. Propuestas no
tomadas en esta rebanada, en orden: cifras del docente, aclaración del falso amigo «pendiente», voto útil/no útil,
lector de la huella, preguntas sugeridas, consola en otras pantallas, anillo 3 con vuelta, función sintáctica del dígito.
