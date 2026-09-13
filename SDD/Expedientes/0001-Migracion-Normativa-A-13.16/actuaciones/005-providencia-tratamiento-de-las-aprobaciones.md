# EXP-0001 · Actuación 005 — Providencia: cómo sigue el trámite sin la aprobación del Product Owner

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `providencia`
**Fecha:** 2026-09-13
**Autor:** Orquestador de migración normativa SDD, en cumplimiento del dictamen D-1 de la actuación 004
**Foliada:** 005

---

**Rige, por fase, y lo que queda para después:**

| Fase | Qué exige la norma | Qué hace esta corrida | Qué queda esperando |
|---|---|---|---|
| M0 | Reconocer; detener sólo si el destino no es reconocible | Hecha (actuación 003) | — |
| M1 | Presentar el plan y esperar aprobación (`Master-Prompt-Migracion.md` §5) | Plan 1.1 y registro de mesa escritos en `SDD/Docs/Audit/` | **Aprobación del plan** (lote, punto A) |
| M2 | Escribir el intake sólo con aprobación explícita, nada rellenado, bump major (`Master-Prompt.md` §13 caso (b); `Master-Prompt-Migracion.md` §6) | **No escribe.** Propuesta `P-01` con el diff y la batería, que no tiene preguntas | **Aprobación del diff del intake** (punto B) |
| M3 | Re-derivar del intake migrado y confirmar | **No escribe.** Propuesta `P-02` | Punto B |
| M4 | Migrar `SDD/Docs/` en orden D6 | **No escribe.** Propuestas `P-03`, `P-04`, `P-06`, `P-08`, verificadas sobre una copia descartable (E-016) | Punto B |
| M5 | Procedencia sólo con la cadena completa | **No se toca.** Sigue en 13.7 | Aplicación de las propuestas |
| M6 | Auditoría independiente | **Se corre igual**, sobre la corrida y sus propuestas | — |

**Por qué commitear en esta rama cuenta como escritura** (MN-REF-02): `Master-Prompt-Migracion.md` 2.4 ya introdujo la entrega por rama con su pull request y **mantuvo** la regla de que M2 no escribe sin aprobación. Que la rama no se fusione sin el Product Owner no la vuelve un borrador: es una unidad de trabajo.

**Por qué no es detenerse** (actuación 001, P-3): la migración parcial es un estado final legítimo (`Migracion-Rules.md` §4.6), y todo lo que no depende de la aprobación está hecho. Una sola aplicación, con `propuestas/aplicar-propuestas.sh`, completa M2 a M4 cuando llegue el OK; M5 y un audit de cierre vienen después.

**Qué escribe esta rama, entonces:** el expediente, el plan y el registro de mesa en `SDD/Docs/Audit/`, el informe de M6, y —ajeno a la migración— el OK de la fase `k` (actuación 002).
