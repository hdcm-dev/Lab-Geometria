# EXP-0001 · Actuación 006 — Informe: las propuestas de M2, M3 y M4

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `informe`
**Fecha:** 2026-09-13
**Autor:** Orquestador de migración normativa SDD
**Foliada:** 006

---

**Dónde están:** [`../propuestas/`](../propuestas/). Seis diffs contra el árbol de esta rama, un guion que los aplica y su verificación.

| Propuesta | Archivos | Versión | Qué aplica | Filas del plan |
|---|---|---|---|---|
| `P-01-intake-5.0.diff` | Intake | 4.6 → 5.0 | Marca y pertenencia del insumo; perfil por ecosistema; §16.1 por sample; barrido de «excepción» y D8 por proyecto | PM-01, PM-09 |
| `P-02-manifiesto-7.0.diff` | Manifiesto | 6.1 → 7.0 | Re-derivación: perfiles, una tabla con solución, un grafo de dos clases, dos validaciones | PM-02, PM-09 |
| `P-03-vista-producto-1.11.diff` | `Vista-Producto.md` | 1.10 → 1.11 | Mapa sin D8; clase y generador; párrafo del visor | PM-03, PM-09, PM-11 |
| `P-04-pipeline-producto-1.9.diff` | `Pipeline-Producto.md` | 1.8 → 1.9 | Único generador, ambientes sin Node y modo; §3 y §9 | PM-04, PM-11 |
| `P-06-ciclo-de-origen-118-filas.diff` | 8 documentos de 05, 06 y 09 | +1 minor cada uno | Columna «Ciclo de origen», 95 derivadas y 23 no derivables | PM-06 |
| `P-08-apartamientos.diff` | `ADR-14001` a `ADR-14004` | +1 minor cada uno | Contadores y campo 7 | PM-08 |

**Cómo se verificaron, sin tocar el árbol** ([`E-016`](../propuestas/E-016-verificacion-de-aplicacion.txt)): sobre un worktree descartable se corrió `aplicar-propuestas.sh` —que primero comprueba con `git apply --check` que las seis aplican limpias y aborta antes de escribir si alguna no—, se tomaron los snapshots `_legacy/2026-09-13/` con el precedente del destino (`<Documento>-v<versión>.md` en la carpeta de cada uno), se aplicaron, pasó `verify-solution-tree.sh`, se contaron los enlaces rotos de los archivos tocados y se buscaron residuos vivos del término viejo.

**La batería de preguntas del intake está vacía.** Ninguna sección nueva de la plantilla 3.6 carece de fuente: la marca, el perfil y §16.1 salen del propio intake, del manifiesto, de `Web ADR-10008` y del árbol. **Contenido sin destino: ninguno**; las cinco filas anteriores de §16.1 se transponen con su texto.

**Lo que las propuestas no hacen, y se declara:** no tocan `visor.bundle.js` (punto C del lote, `DD-5`); no agregan el nombre vigente a `11-Documentacion/README.md` :196, que D-3 declara vivo pero pertenece a un documento fuera de este salto (`DD-6`); no corrigen código (`DD-2`); no escriben la procedencia (M5).
