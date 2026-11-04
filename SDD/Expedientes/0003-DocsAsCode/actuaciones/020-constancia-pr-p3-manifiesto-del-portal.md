# Constancia — PR-P3 del portal: manifiesto `publicar.yml` y control `portal-manifiesto`

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §1, §7 y plan de PRs, fila PR-P3) reemplaza el filtro por
`delivery`, que no filtra nada: en este repo vale siempre `[publico]` por D-18 (`docs-as-code/esquema-frontmatter.md`)
y lo exige `docs-as-code/controles/delivery_publico.py`. En su lugar, un manifiesto explícito como lista permitida, y
un control que bloquea las piezas con `audience` `docente` que no traen un ok explícito.

**Qué se hizo.**

- `docs-as-code/portal/publicar.yml`: manifiesto **vacío** (`piezas: []`), con el formato documentado en su
  cabecera. Cada entrada es un archivo exacto, sin globs, con `llms: true|false` y, para piezas docentes,
  `portal_docente_ok: true`. El alta inicial llega con PR-P4.
- `docs-as-code/controles/portal_manifiesto.py`: lee el manifiesto (subconjunto de YAML, sin dependencias) y da
  hallazgo `formato`, `ruta-no-permitida` (globs, rutas absolutas o con `..`, y lo excluido siempre: `SDD/`,
  `_legacy`, `ai-layer/`, `.docs-guard/`, `docs-as-code/controles/`), `ruta-inexistente` y `docente-sin-ok`.
  Reutiliza el lector de frontmatter de `.docs-guard/dg.py` sin modificarlo. Sale 0, 1 o 2.
- `autoprueba.sh`: verde con el manifiesto del repo y con uno vacío; rojo con `SDD/Docs/Audit/x.md`; rojo con una
  pieza docente sin ok y verde con el ok; rojo con un glob; error de uso con un manifiesto inexistente.
- `scripts/docs-local.sh`: paso `portal · manifiesto` en **avisa**. Pasa a bloquea tras una corrida limpia (PR-P7).

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, el expediente 0002 ni
`SDD/Docs/**`. Nada de la capa de IA.

**Efecto sobre el producto.** Ninguno al fusionar: no hay portal todavía y los archivos tocados son de la compuerta
de docs. `deploy-front-ftp.yml` sólo corre con `workflow_dispatch`.

Sigue: PR-P4, `mkdocs.yml`, build en contenedor fijo, paso `portal` en la compuerta, alta inicial en el manifiesto y
`portal_negativo.py` · mantenimiento de docs-as-code (agente)
