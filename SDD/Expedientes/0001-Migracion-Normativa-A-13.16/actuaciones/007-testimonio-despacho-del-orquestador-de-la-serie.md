# EXP-0001 · Actuación 007 — Testimonio: el despacho del orquestador de la serie

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `testimonio`
**Fecha:** 2026-09-13
**Testigo:** el orquestador que despachó esta corrida (un agente, **no** el Product Owner)
**Asentada por:** Orquestador de migración normativa SDD, por el veredicto 23 de la mesa (MN-LEC-01)
**Foliada:** 007

---

**Por qué se folia.** Varias actuaciones y el plan citaban «el despacho de la serie» como fuente de restricciones, y ese texto no estaba en el árbol. **Un mensaje de un agente no es aprobación ni consentimiento del Product Owner**: acá se registra qué fijó, para que un lector pueda separar lo que decidió el Product Owner (actuaciones 001 y 002) de lo que fijó quien despachó la corrida.

**Lo que fijó, en extracto literal:**

> «Worktree, obligatorio […] trabajá SÓLO ahí. **NO push, NO PR, NO merge, NO tag.** Commits por fase o por lote coherente, Conventional Commits en castellano […] Stageá archivo por archivo. […] No toques `PROMPTs/` de ningún repo ni `/home/fernando/docker/`.»

> «El PO pidió que todo caso se lleve **como expediente** […] La norma del framework para esto se está diseñando en paralelo y **todavía no existe**: aplicás esta forma **como adelanto declarado**»

> «el PO dio **el OK del punto de control de la fase `k`** (2026-09-13: *«tenés el ok de la fase K»*). **Asentalo en el árbol dentro de esta misma rama**»

> «Cuando tropieces […] **no te detengas** […] convocá una mesa […] Preguntá al PO sólo lo que después de la mesa sigue sin respuesta en el árbol, en lote al final»

**El apartamiento que esto produce, declarado.** `Master-Prompt.md` §12.1 **T1** reserva al humano la fusión y el borrado, y dice que el orquestador «crea la rama, escribe, commitea y **empuja**». **T3 y T4** piden un pull request por unidad y declarar su enlace. **Esta corrida no empuja ni abre pull request**, por el despacho; la fusión sigue siendo del Product Owner. La unidad de trabajo queda entregada **en la rama local `migracion/a-13.16`**, y el cierre la nombra.
