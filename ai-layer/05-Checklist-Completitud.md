# Capa de IA · Checklist binario de completitud de V1 y V2

> Plantilla: `E4-Checklist-Completitud.md` del Conocimiento de especificación v0 (bitácora de la guía 16,
> etapa E4), que reproduce su §11. Una tabla por especificación. Fecha: 2026-11-01.

> **Estado al 2026-11-02 (expediente 0002):** V1 **construida, flag apagado**; V2 especificada, **diferida**.
> El checklist de abajo se conserva tal como lo dejó el revisor 1; la revisión humana sigue pendiente.

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
| Revisor 2 | humano de la mesa: **pendiente** (se pide en el lote al Product Owner del expediente 0002) | ídem |
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
| Revisor 2 (humano de la mesa) | pendiente | pendiente |
