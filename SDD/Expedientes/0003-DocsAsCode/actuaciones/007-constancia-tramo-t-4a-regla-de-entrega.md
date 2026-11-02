# Constancia — tramo T-4A: regla de entrega

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-4A** del plan del folio 003: la regla de entrega
`docs-as-code/regla-de-entrega.md` (`RD-GEO-002`) con el texto de §6.1 de la resolución, sin cambios, y sus anclas
`ruta:línea`; y **una línea** en el README raíz, en la lista de punteros, que enlaza a `docs-as-code/README.md`.

**Qué no se tocó.** `AGENTS.md` (`AGENTS.md:3-5`) ni `SDD/Docs/**`: el texto para `Contrato-Agentes.md` ya está elevado
en el folio 003. La entrada de la ia-db es el tramo T-4B, en el repositorio hermano.

**README compartido.** Se avisó a Guía 16 antes del PR; si fusiona después, rebasea.

**Verificación.** `scripts/docs-local.sh` en verde; `git diff --stat origin/main -- README.md` = 1 línea añadida.

**Efecto sobre el producto.** Sin cambio funcional: ningún workflow despliega al fusionar (`deploy-front-ftp.yml:38-39`
es sólo `workflow_dispatch`).

Sigue: tramo de la entrada en la ia-db (T-4B) del plan del folio 003 · mantenimiento de docs-as-code (agente)
