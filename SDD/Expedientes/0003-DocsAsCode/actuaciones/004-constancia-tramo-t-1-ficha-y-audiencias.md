# Constancia — tramo T-1: ficha y audiencias

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-1** del plan del folio 003: la carpeta `docs-as-code/` con su índice, la ficha
de diseño documental A-01 (`FIC-GEO-001`), el registro de audiencias A-12 (`AUD-GEO-001`), la nota del esquema de
frontmatter (`FIC-GEO-002`), «Cómo intervenir» A-06 (`FIC-GEO-003`), el glosario (`FIC-GEO-004`) y la regla RD-01
(`RD-GEO-001`), que cita el límite didáctico de ADR-10010. Toda pieza lleva `delivery: [publico]`.

**Base.** `origin/main` aaef547. La mesa había leído c5afa0e; entre las dos entró la capa de IA (expediente 0002). Las
citas se releyeron en aaef547 y las que se movieron se corrigieron en la ficha (§8), entre ellas los números de línea de
`WorkEndpoints.cs` y la evidencia del descarte de la autenticación por cliente, que pasa a ADR-00009.

**Verificación.** Los comandos «pendiente» del inventario (1b, 2 a 11) se corrieron y su salida está en la ficha, §1.
Sobre `docs-as-code/` dieron verde, corridos sólo localmente, `markdownlint-cli2` 0.17.2 con la configuración de
`docs-guard` y los controles `esquema`, `trazas`, `fuera` y `ai` de una copia de `docs-guard` sin versionar; el barrido
de rutas de host, IP y nombres de máquinas no dio coincidencias. La compuerta versionada llega con el tramo siguiente del plan (compuerta de docs).

**Efecto sobre el producto.** Ninguno funcional: `docs-as-code/` no entra en ninguna imagen y ningún workflow despliega
al fusionar.

Sigue: tramo de la compuerta de docs del plan del folio 003 · mantenimiento de docs-as-code (agente)
