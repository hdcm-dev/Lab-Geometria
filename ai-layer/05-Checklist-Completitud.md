# Capa de IA · Checklist binario de completitud de V1 y V2

> Plantilla: `E4-Checklist-Completitud.md` del Conocimiento de especificación v0 (bitácora de la guía 16,
> etapa E4), que reproduce su §11. Una tabla por especificación. Fecha: 2026-11-01.

> **Estado al 2026-11-02 (expediente 0002):** V1 **construida, flag apagado**; V2 especificada, **diferida**.
> El checklist de abajo se conserva tal como lo dejó el revisor 1.

> **Revisión 2 (2026-11-03, actuación 009):** la hizo la **mesa de Geometría** por delegación del Product
> Owner (relator y especialistas, agentes). Se declara así porque la plantilla pide «humano de la mesa»: la
> segunda lectura es independiente del revisor 1 —otra sesión, otro encargo, contra el código fusionado y no
> contra la especificación sola— pero no es humana. El docente puede contrafirmarla en la columna
> «Contrafirma» sin que eso bloquee nada.

## Índice

- [1. Datos de la revisión](#1-datos-de-la-revisión)
- [2. V1 · Pistas](#2-v1--pistas)
- [3. V2 · Borrador de comentario](#3-v2--borrador-de-comentario)
- [4. Veredicto](#4-veredicto)

## 1. Datos de la revisión

| Campo | V1 | V2 |
|---|---|---|
| Especificación revisada | `01-Especificacion-V1-Pistas.md` | `02-Especificacion-V2-Borrador-De-Comentario.md` |
| Contexto | PROTO | PROTO |
| Revisor 1 | agente que especificó sin la guía (sólo el Conocimiento v0 y el repositorio) | ídem |
| Revisor 2 | mesa de Geometría (agentes), por delegación del PO, 2026-11-03: **firmado** | ídem: **firmado con observaciones** (ver §4) |
| Contrafirma | docente: optativa | docente: optativa |
| Fecha | 2026-11-01 | 2026-11-01 |
| `E4-verificar-requisitos.py` | 16 requisitos, 0 incompletos, rc 0 | 8 requisitos, 0 incompletos, rc 0 |

## 2. V1 · Pistas

| # | Ítem | PROTO | Resultado | Fuente o motivo |
|---|---|---|---|---|
| C-01 | Cuatro preguntas respondidas | DEBE | SÍ | §1.2 |
| C-02 | Resultado del árbol registrado | DEBE | SÍ | §1.3: A0 |
| C-03 | ESC-, CTX- y ACT- fijados | DEBE | SÍ | §1.3; ESC con código propio ESC-GEO-01 (K-01) |
| C-04 | Escalón de conocimiento y nivel con criterio de salto | DEBE | SÍ | §1.4 |
| C-05 | C4 de contexto y contenedores validados | DEBERÍA | SÍ | figuras 2.1 y 3.1; `mermaid.sh` rc 0 |
| C-06 | Ninguna flecha del modelo al dominio ni a los datos | DEBE | SÍ | figura 3.1: el adaptador sólo implementa el puerto |
| C-07 | Ficha por capacidad con prompt propio | DEBE | SÍ | §5.1, §5.2 |
| C-08 | ADR-01, 02 y 11 con ASR | DEBE | SÍ | `03-ADR.md` ADR-IA-01..03 |
| C-09 | ADR-06 y 07 si A1 o más | DEBE | NO APLICA | nivel A0, sin herramientas (§6) |
| C-10 | Todo `REQ-IA-nn` con criterio y prueba, script 0 | DEBE | SÍ | §7; rc 0 |
| C-11 | Tipo de prueba por requisito | DEBERÍA | SÍ | columna «Tipo» de §7 |
| C-12 | Matriz OWASP filtrada con ficha | DEBERÍA | SÍ | §8, ficha de forma propia (K-05) |
| C-13 | Qué datos salen y hacia qué servidor, por ACT-SEC | DEBE | SÍ | §2.2, §4.3 |
| C-14 | Sin valores de infraestructura ni secretos | DEBE | SÍ | §4.3, REQ-IA-15; R-PRIV rc 0 sobre `ai-layer/` |
| C-15 | MCP: quién, dirección, control | DEBE | NO APLICA | sin MCP (§2.3) |
| C-16 | Frontera con clientes citada del libro MAUI | PUEDE | NO APLICA | cliente Blazor servidor, no híbrido (§2.4) |
| C-17 | Métrica, umbral, error caro y quién fija el umbral | DEBE | SÍ | §1.2 |

## 3. V2 · Borrador de comentario

| # | Ítem | PROTO | Resultado | Fuente o motivo |
|---|---|---|---|---|
| C-01 | Cuatro preguntas respondidas | DEBE | SÍ | §1.2 |
| C-02 | Resultado del árbol registrado | DEBE | SÍ | §1.3: A0 |
| C-03 | ESC-, CTX- y ACT- fijados | DEBE | SÍ | §1.3; ESC-GEO-02 propio (K-01) |
| C-04 | Escalón y nivel con criterio de salto | DEBE | SÍ | §1.3 |
| C-05 | C4 validados | DEBERÍA | SÍ | figuras 2.1 y 3.1; `mermaid.sh` rc 0 |
| C-06 | Ninguna flecha del modelo al dominio ni a los datos | DEBE | SÍ | figura 3.1: el repositorio lo lee el caso de uso, no el adaptador |
| C-07 | Ficha con prompt propio | DEBE | SÍ | §3 |
| C-08 | ADR-01, 02 y 11 con ASR | DEBE | SÍ | reutiliza ADR-IA-01..03; ADR-IA-06 propia |
| C-09 | ADR-06 y 07 si A1 o más | DEBE | NO APLICA | A0; ADR-IA-06 registra por qué no hay escritura del modelo |
| C-10 | Requisitos con criterio y prueba, script 0 | DEBE | SÍ | §5; rc 0 |
| C-11 | Tipo de prueba | DEBERÍA | SÍ | columna «Tipo» de §5 |
| C-12 | Matriz OWASP filtrada | DEBERÍA | SÍ | §6 |
| C-13 | Datos que salen, por ACT-SEC | DEBE | SÍ | §2.2; servidor como V1 §4.3 |
| C-14 | Sin infraestructura ni secretos | DEBE | SÍ | R-PRIV rc 0 |
| C-15 | MCP | DEBE | NO APLICA | sin MCP (§2.3) |
| C-16 | Frontera MAUI | PUEDE | NO APLICA | cliente Blazor servidor |
| C-17 | Métrica, umbral, error caro, quién fija | DEBE | SÍ | §1.2; el umbral de REQ-IA-26 lo fija el docente (pendiente de valor) |

## 4. Veredicto

| Conteo | V1 | V2 |
|---|---|---|
| DEBE en SÍ | 11 | 11 |
| DEBE en NO APLICA con motivo | 2 | 2 |
| DEBE en NO | 0 | 0 |
| DEBERÍA en NO | 0 | 0 |
| **Completa (revisor 1)** | **sí** | **sí** |
| Revisor 2 (mesa, actuación 009) | **conforme** | **conforme con observaciones** |

**Observaciones del revisor 2.**

- **V1, C-13:** ahora hay decisión de ACT-SEC sobre el servidor de PROD (`01` §4.3); antes sólo había la de
  PROTO. Se confirma SÍ.
- **V1, C-17:** el umbral de REQ-IA-13 (media ≥ 4/5) está; la medición docente todavía no. No es ítem del
  checklist —el checklist mide completitud de la especificación, no su cumplimiento— y queda en
  `ensayos/corrida-2026-11-03/Revision-Utilidad.md`.
- **V2, C-17:** el umbral de REQ-IA-26 se fija en la actuación 009 (≥ 60 % de borradores enviados con
  edición menor, como k de n con Clopper-Pearson). Pasa de «pendiente de valor» a SÍ.
- **V2, C-08:** ADR-IA-06 recibe número reservado `ADR-14011`; sigue propuesta mientras V2 esté diferida.

**Observaciones de la actuación 010 (2026-11-03, misma mesa).** No cambia ningún resultado; se anotan para
que la lectura siguiente no tenga que reconstruirlas.

- **V1, C-13:** la decisión de ACT-SEC sobre el servidor de PROD se revisó con la evaluación completa
  (`01` §4.3, `ADR-14012`): mismo servidor, modelo `qwen3.5:4b` sin razonamiento. Sigue en SÍ, ahora sin la
  salvedad de «provisional por evidencia incompleta».
- **V1, C-14:** la evaluación completa se resume en `01` §4.7 sin dirección, puerto ni credenciales del
  servidor; el informe original y su arnés no entran al repositorio. R-PRIV se vuelve a correr sobre
  `ai-layer/` y `SDD/` antes de abrir el PR.
- **V1, C-10:** entran REQ-IA-17 y REQ-IA-18 con criterio y prueba; `E4-verificar-requisitos.py` se vuelve a
  correr al aplicar (esperado: 18 requisitos, 0 incompletos).
- **V1, C-12:** la fila LLM09 pasa de riesgo previsto a **observado** (dos textos falsos que pasaron el
  filtro); el tratamiento es el flag apagado en PROD hasta REQ-IA-13.
- **V1, C-11 (mesa 0004):** prompt y plantillas pasan a ser un solo **contrato de pistas, versión 1.1**,
  vigente y no definitivo, con condición de revisión (`01` §5.2), y la prueba bloqueante
  `HintContractMatchesSpecTests` compara el código con los bloques marcados de `01`. Fusionar no despliega
  (canal manual) y no enciende: encender es una decisión aparte.
