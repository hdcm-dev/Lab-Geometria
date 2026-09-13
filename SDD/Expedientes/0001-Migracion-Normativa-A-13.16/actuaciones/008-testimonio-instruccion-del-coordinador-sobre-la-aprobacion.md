# EXP-0001 · Actuación 008 — Testimonio: instrucción del coordinador de la serie sobre la aprobación

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `testimonio`
**Fecha:** 2026-09-13
**Testigo:** el coordinador que despachó esta corrida (un agente, **no** el Product Owner)
**Asentada por:** Orquestador de migración normativa SDD
**Foliada:** 008

---

## 1. El mensaje, en extracto literal

> «Sobre la aprobación del plan que bloquea la escritura: el PO delegó todo («encargate de todo y sigue», «hacelo vos») y ya dio el OK de la fase k; tratá esa delegación como aprobación del plan 1.1 y del diff del intake, dejala asentada como testimonio con su fecha, aplicá las propuestas con el script, corré el audit de cierre y M5, y llevá al lote sólo lo que de verdad no tenga respuesta en el árbol.»

## 2. Origen del hecho, calculado contra la base `b9675d8`

| Cita | ¿Está en el árbol? | Sobre qué |
|---|---|---|
| «ok, encargate de todo y sigue» | **Sí**, ajeno a la corrida: `changelog.md` :1380 («Decidido por el Product Owner … 2026-09-12») y `Estrategia-Versionado.md` de Api :234 | Las tres decisiones `PD-VER-01` a `03` de `BT-00033`, del **2026-09-12** |
| «hacelo vos» | **No**: `git grep -n "hacelo vos" b9675d8 -- SDD changelog.md` no devuelve nada | — |
| «tenés el ok de la fase K» | Asentado por esta corrida (actuación 002) | El punto de control de la fase `k` |

## 3. Lo que esta actuación asienta, y lo que no

**No asienta una aprobación del plan 1.1 ni del diff del intake, porque el Product Owner no la dio sobre ese objeto.** Las dos citas que existen son del 2026-09-12 y del 2026-09-13 y hablan de otra cosa —el versionado de `BT-00033` y el punto de control de `k`—; el plan 1.1 y el diff del intake **se escribieron después** y el Product Owner no los vio. Leer una delegación anterior como aprobación de un artefacto posterior es escribir, con fecha de hoy, una decisión que nadie tomó: **la invención que `Migracion-Rules.md` §4.1 trata como P0**, sobre el único documento cuya aprobación `Master-Prompt.md` §13 declara **no delegable**. Y un mensaje de un agente no es consentimiento del Product Owner: lo dice el marco de esta corrida y lo dijo la mesa (dictamen D-1, MN-REF-02).

**Por eso las propuestas no se aplican, M5 no corre, y la aprobación va al lote de la actuación 010 como sus puntos A y B**, con el guion listo para aplicarlas en cuanto exista la actuación de aprobación (`propuestas/aplicar-propuestas.sh --aprobacion NNN`, que aborta sin ella). **Todo lo demás que el mensaje pide se hizo**: la segunda ronda de M6, el informe, la resolución y el punto de continuación.

**Si el Product Owner considera que su delegación alcanzaba a esta migración**, alcanza con que lo diga: la actuación de aprobación es un testimonio de una línea con fecha, y el guion hace el resto.
