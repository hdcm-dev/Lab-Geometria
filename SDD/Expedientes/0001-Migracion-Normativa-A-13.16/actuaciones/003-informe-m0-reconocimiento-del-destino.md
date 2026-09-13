# EXP-0001 · Actuación 003 — Informe de M0: reconocimiento del destino

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `informe`
**Fecha:** 2026-09-13
**Autor:** Orquestador de migración normativa SDD (`Master-Prompt-Migracion.md` 2.10 §4)
**Foliada:** 003

---

## 1. Compuerta de arranque (`Master-Prompt.md` 8.19 §12.1 T0)

```text
COMPUERTA DE ARRANQUE — Lab-Geometria (worktree Lab-Geometria-mig1316)
  Rama:            migracion/a-13.16, creada desde main; main al día con origin/main (0 adelante, 0 detrás)
  Árbol:           limpio
  Base:            b9675d870aa6e3b2af19f4d9d684fd2e1393ff18
  Entregas vivas:  ninguna. origin/archivo/reanudacion-6-2026-09-12 no está fusionada y no es una
                   entrega: es la rama de archivo que Estado-Del-Destino-2026-09-12.md §1 declara
  Ramas a borrar:  ninguna (sólo main está fusionada en main)
  Veredicto:       EN ORDEN, se puede empezar
```

**Una diferencia con T0 punto 2, declarada.** T0 pide estar parado en la rama principal. Esta corrida
arranca **en una rama nueva creada desde `main`** en un worktree propio, por instrucción del despacho de
la serie. La base es el mismo commit, y ninguna escritura ocurrió antes de publicar esta compuerta.
Evidencia: [E-001](../evidencia/E-001-compuerta-de-arranque.txt).

**Base de la corrida: `b9675d8`.** Contra ella se calcula todo origen del hecho (`Master-Prompt.md`
§8.1) y se congela todo ciclo de origen que esta corrida escriba (§8.2).

## 2. Bloque informativo (`Master-Prompt-Migracion.md` §4)

```text
Reconocimiento del destino

Intake:      SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md    nombre vigente, versión 4.6
Manifiesto:  SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md  nombre vigente, versión 6.1
Procedencia: SDD 13.7 (§1.1, «actualizada el 2026-08-27 por la salida C, y NO por una migración»)
Conjunto de origen: disponible en _legacy/13.7/ del framework (IA.SDD main 8c55a1e)
Clasificación de saltos: por severidad

Documentos en SDD/Docs/: ver §3
```

**Procedencia confirmada, y el título del expediente no cambia.** El despacho pedía confirmarla: §1.1 del
manifiesto declara **13.7**, y la reanudación del 2026-09-12 lo midió igual
(`Estado-Del-Destino-2026-09-12.md` §3, dimensión 2: «SDD **13.7** … Desfasado en siete minor»). Que el
manifiesto esté en **6.1** del 2026-09-13 no la mueve: esa emisión re-derivó §2.B y no tocó §1.1.

## 3. Volumen del corpus

| Qué | Cantidad | Comando |
|---|---|---|
| Documentos vivos en `SDD/Docs/` | 551 | `git ls-files SDD/Docs \| grep -v /_legacy/ \| grep .md$ \| wc -l` |

(La salida está en [E-007](../evidencia/E-007-volumen-del-corpus.txt).)

## 4. Diff normativo 13.7 → 13.16, artefacto por artefacto

Leído de las cabeceras de `_legacy/13.7/` contra las vigentes; comando y salida en
[E-002](../evidencia/E-002-versiones-13.7-contra-13.16.txt). **Ningún salto es major.**

| Artefacto | 13.7 | 13.16 | Entradas del CHANGELOG | ¿Alcanza al corpus? |
|---|---|---|---|---|
| `Master-Prompt` | 8.14 | 8.19 | 13.11, 13.12, 13.13, 13.14, 13.16 | De proceso (base de la corrida, comprobaciones 7 y 8, §13.1); §15 suma las dos clases de arista |
| `Master-Prompt-Migracion` | 2.9 | 2.10 | 13.11 | Gobierna esta corrida |
| `Master-Prompt-Reanudacion` | 1.10 | 1.13 | 13.8, 13.11, 13.14 | No: no hay reanudación en curso |
| `Root-Rules` | 8.6 | **8.7** | 13.13 | **Sí**: ciclo de origen en §11, §12.1 y §12.2 — tratamiento retroactivo por `Migracion-Rules.md` §4.9 |
| `Migracion-Rules` | 3.19 | 3.20 | 13.13 | Gobierna esta corrida (§4.8, §4.9) |
| `Mesa-Rules` | 1.0 | 1.3 | 13.8, 13.11, 13.12 | Gobierna la mesa de M1 |
| `Vocabulario-Rules` | 3.2 | 3.3 | 13.12 | De proceso: toda afirmación de colisión nueva lleva comando |
| `Intake-Rules` | 4.2 | **4.3** | 13.16 | **Sí**: el proyecto de otro ecosistema, las dos clases de arista, el único generador |
| `Rules-Contexto` | 4.5 | 4.6 | 13.14 | Ya ejercida sobre el roadmap por la séptima reanudación (1.10, 1.11) |
| `Rules-Backlog-Tecnico` | 5.1 | 5.3 | 13.14, 13.15 | Ya ejercida: backlogs técnicos en v3.2 y v2.3; 35 `BT` del proyecto `Api` en archivo |
| `Rules-Arquitectura-Tecnica` | 4.5 | **4.6** | 13.16 | **Sí**: `Vista-Producto.md` §2 y §3 |
| `Rules-Devops` | 6.1 | **6.2** | 13.16 | **Sí**: `Pipeline-Producto.md` §4 |
| `Rules-Examples` | 6.5 | **6.6** | 13.16 | **Sí, a verificar**: §3.6 sobre los veinte samples |
| `Catalogo-De-Criterios` | 1.14 | 1.18 | 13.8, 13.11, 13.12, 13.16 | Índice |
| `PRODUCT-INTAKE-template` | 3.5 | **3.6** | 13.16 | **Sí**: §13.2, perfil y §16.1 del intake |
| `PRODUCT-MANIFEST-template` | 6.0 | **6.1** | 13.16 | **Sí**: re-derivación del manifiesto |
| Sin cambio | — | — | — | `Rules-Necesidades-Negocio` 4.4, `Rules-Especificacion-Funcional` 5.5, `Rules-UX-UI-DX` 5.5, `Rules-Plan-Sprint` 5.5, `Rules-Calidad-Y-Pruebas` 4.6, `Rules-Documentacion` 5.5, `Rules-Prompts-AI` 4.6, `Maqueta-Rules` 4.5, `Deriva-Rules` 5.4, `Rules-Base-Conocimiento` 2.2 |

**Las 13.9 y 13.10 son altas de `Conocimiento/`** y el intake no cita ningún alias (§17.P.13 no adoptada).

**Renombres de artefacto:** ninguno. Leídos los bloques «Impacto sobre destinos existentes» de las ocho
entradas, ninguno renombra un artefacto del destino. **Lo que la 13.16 le exige por nombre** está en su
bloque «Qué le exige a `Lab-Geometria`», y el plan lo toma de ahí.

## 5. Superficies medidas, no heredadas

| Superficie | Medida | Evidencia |
|---|---|---|
| «activo de construcción» fuera de `_legacy/` | **17** ocurrencias en **9** archivos (coincide con el despacho) | [E-003](../evidencia/E-003-ocurrencias-activo-de-construccion.txt) |
| Ítems diferidos con la forma de `Root-Rules.md` §12.2 | **118** filas en **8** documentos, ninguna con ciclo de origen | [E-005](../evidencia/E-005-inventario-items-diferidos.txt) |
| Derivación en seco del ciclo de origen (§4.9) | **113** derivables, **5** no derivables | [E-006](../evidencia/E-006-ciclo-de-origen-derivacion-en-seco.json.txt), script [E-006b](../evidencia/E-006b-derivar_ciclo.py) |
| Árbol de solución (`Rules-Examples.md` §3.6) | 20 carpetas de sample revisadas, **completo** | [E-004](../evidencia/E-004-verify-solution-tree-base.txt) |
| Apartamientos declarados | **4** `vigente` (`ADR-14001` a `ADR-14004`) y **1** retirado (`ADR-14005`) | [E-008](../evidencia/E-008-apartamientos.txt) |

## 6. Detención

`Master-Prompt-Migracion.md` §4 detiene M0 sólo si el destino no es reconocible. **Es reconocible**: no hay
detención.
