# EXP-0001 · Actuación 004 — Mesa de evaluación de M1

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `mesa`
**Fecha:** 2026-09-13
**Autor:** AG-00970, presidente de mesa (el orquestador de migración, sin voto)
**Foliada:** 004

---

**El registro vive donde la norma lo manda y acá se folia por enlace:**
[`SDD/Docs/Audit/Mesa-2026-09-13.md`](../../../Docs/Audit/Mesa-2026-09-13.md) 1.0.

| Parte | Quién | Qué produjo |
|---|---|---|
| Panel ciego | Requisitos, Verificación, Lector sin contexto, Arquitectura, Operación y entrega, Trazabilidad documental, Formal | 42 hallazgos (ninguno P0), cada uno con nivel y ancla |
| Refutación | Refutador | 8 refutaciones y 2 hallazgos nuevos |
| Jurado | Cinco jueces (Evidencia, Impacto, Costo y beneficio, Coherencia histórica, Riesgo) | 30 veredictos, dictámenes D-1 a D-6, 6 ítems de deuda, 0 escaladas |

**Lo que cambió en la corrida por esta mesa**, y conviene que un lector lo vea primero:

1. **La derivación del ciclo de origen de M0 era falsa en la mitad de las filas.** La evidencia E-006 (113 derivables) tomaba el commit más antiguo de todo el repositorio. Se rehízo tres veces a la vista de la mesa —v2, v3 y v4 (53/65, sobre-conservadora)— hasta la **v5 (95/23)**, que busca el alta de la fila en el archivo de su propio proyecto. Las cuatro cuentas quedan publicadas; **ninguna fila se escribió con las anteriores**.
2. **Esta migración termina parcial, y es por norma y no por falta de trabajo.** Dictamen D-1: sin la aprobación explícita del Product Owner no se escribe el intake, y sin intake migrado no se re-deriva el manifiesto ni se migran los documentos. Todo lo que la migración haría está hecho **como propuesta con texto exacto** (actuación 006).
3. **No se le pregunta nada que el árbol conteste.** La mesa descartó preguntar el valor de `API_BASE_URL` (ancla `C`, decisión ya asignada) y resolvió D-2 a D-6 leyendo el conjunto.

**Informes íntegros del panel, del refutador y del jurado:** transcriptos en su forma resumida en el registro §4 y §6; los textos completos se entregaron al orquestador en la corrida y se conservan en el cierre de la corrida que el despacho recibe. **No se versionan en el árbol por su volumen**, y se declara así.
