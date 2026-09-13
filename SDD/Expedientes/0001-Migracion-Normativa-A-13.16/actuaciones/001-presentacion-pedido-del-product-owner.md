# EXP-0001 · Actuación 001 — Presentación: el pedido del Product Owner

**Expediente:** [EXP-0001](../README.md) · Migración normativa de `Lab-Geometria` a SDD 13.16
**Tipo:** `presentacion`
**Fecha:** 2026-09-13
**Autor de la presentación:** Product Owner
**Asentada por:** Orquestador de migración normativa SDD (`Master-Prompt-Migracion.md` 2.10)
**Foliada:** 001 — no se reescribe; una corrección es una actuación nueva que cita ésta

---

## 1. El pedido, literal

> «me centraría primero en migrar Lab-Geometria — cuando termines con este luego podés seguir migrando
> con RPI.VideoControl — cuando te encuentres con un problema, en vez de pararte, armá una mesa adecuada
> para llegar a una conclusión, evaluá realmente si necesitás preguntarme algo o es una mala
> interpretación analizando todo el conjunto del problema — acordate en saber dónde estás parada»

Fecha del testimonio: **2026-09-13**. Llega a esta corrida por el despacho del orquestador de la serie,
que fija además la forma de expediente (decisión del Product Owner del mismo día) y el alcance: esta rama
(`migracion/a-13.16`), sin push, sin pull request, sin merge y sin etiqueta.

## 2. Qué pide, descompuesto

| # | Pedido | Cómo lo toma esta corrida | Dónde queda |
|---|---|---|---|
| P-1 | Migrar primero `Lab-Geometria` | Es la decisión de migrar que `Master-Prompt-Migracion.md` §0 exige que llegue tomada: **no se decide acá** | Todo el expediente |
| P-2 | Después, `RPI.VideoControl` | **Fuera del objeto de este expediente.** Se registra como continuación, no se inicia desde esta rama | Punto de continuación del [README](../README.md) |
| P-3 | Ante un problema, mesa y no detención | Rige `Master-Prompt.md` §8.1 (origen del hecho y pregunta previa) y `Mesa-Rules.md` §0.0/§7. Cada mesa se folia como actuación `mesa` | Actuaciones de tipo `mesa` |
| P-4 | Preguntar sólo lo que la mesa no pudo contestar leyendo el conjunto | Lote único al final, forma §8.1 completa con `ORIGEN DEL HECHO` y `SI NO RESPONDÉS` | Actuación `resolucion` y README §Punto de continuación |
| P-5 | «Saber dónde estás parada» | El punto de continuación de la carátula se actualiza en cada fase | README §5 |

## 3. Lo que esta presentación no autoriza, y se declara

- **No es la aprobación del plan de migración, del diff del intake ni de la procedencia.** Una
  presentación decide *migrar*; las aprobaciones de M1, M2, M3 y M5 son actos distintos. Cómo se tratan
  esas detenciones con el Product Owner ausente lo resuelve la mesa de M1 (actuación de tipo `mesa`).
- **No habilita a fusionar.** `Master-Prompt.md` §12.1 T1 y el despacho de la serie: la fusión es del
  Product Owner.
