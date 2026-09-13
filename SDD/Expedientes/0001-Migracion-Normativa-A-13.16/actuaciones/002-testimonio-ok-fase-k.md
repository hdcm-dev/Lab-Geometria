# EXP-0001 · Actuación 002 — Testimonio: OK del punto de control de la fase `k`

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `testimonio`
**Fecha del testimonio:** 2026-09-13
**Testigo:** Product Owner
**Asentada por:** Orquestador de migración normativa SDD
**Foliada:** 002

---

## 1. El testimonio, literal

> «tenés el ok de la fase K»

## 2. Qué acto es, y por qué va en este expediente

**Es el OK explícito del Product Owner en el punto de control de la fase `k`** que
`SDD/Docs/00-Contexto/Roadmap-Producto.md` §5.1 exige para toda transición («El Product Owner dio OK
explícito en el punto de control»). Hasta hoy la fila `k` de §3 decía *«entregable realizado, **punto de
control pendiente del OK explícito del Product Owner** (§5.1)»* (versión **1.12**, 2026-09-13), y
`Mini-Plan.md` **3.4** de `GeometriaFactory-Api` remitía el cierre del tramo a ese mismo OK.

**No es un acto de la migración normativa**: ocurre en la vida del producto, fuera de toda fase del
orquestador (`Master-Prompt.md` §13.1). Se asienta en esta rama por instrucción del despacho de la serie,
y se folia acá porque el expediente registra todo lo que la corrida escribe en el árbol.

## 3. Dónde se asentó

| Artefacto | Antes | Después | Snapshot |
|---|---|---|---|
| `SDD/Docs/00-Contexto/Roadmap-Producto.md` | 1.12 — §3 fila `k` «punto de control pendiente»; §2.1 fila `k` «**Pendiente.** Ninguno de los diez ítems está construido» | **1.13** — las dos filas declaran la fase **cerrada con OK del Product Owner del 2026-09-13** | `_legacy/2026-09-13/Roadmap-Producto-v1.12.md` |
| `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/07-Plan-Sprint/Mini-Plan.md` | 3.4 | **3.5** — nota de cierre del tramo en §3.5 | `_legacy/2026-09-13/Mini-Plan-v3.4.md` |
| `changelog.md` | — | Entrada «Punto de control de la fase `k`: OK del Product Owner — 2026-09-13» | — |

**Una corrección de paso, declarada.** La fila `k` de §2.1 del roadmap seguía diciendo «Pendiente.
Ninguno de los diez ítems está construido» después de que la 1.12 declarara el entregable realizado en
§3: la 1.12 actualizó una fila y no la otra. **Origen del hecho: ajeno a esta corrida** — la línea está en
la base `b9675d8` sin cambios de ninguna rama de esta corrida (`git show b9675d8:SDD/Docs/00-Contexto/Roadmap-Producto.md | sed -n 66p`).
**Tiene respuesta en el árbol** —§3 de la 1.12 y `Mini-Plan.md` 3.4 §3.5, con las ocho `Done` y la
`Descartada`—, de modo que por la pregunta previa de `Master-Prompt.md` §8.1 no es una detención: se
corrige y se declara. En `Mini-Plan.md`, las filas **3.3** y **3.4** del control de cambios estaban en
orden inverso; se reordenan sin cambiar su texto.
