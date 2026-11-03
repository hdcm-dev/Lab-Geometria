# Constancia — tramo T-7: carril programado de enlaces externos

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-7** (PR 7, ola 2) del plan del folio 003: el control «Enlaces externos» de
la compuerta, en un carril programado (decisión D-17), con el workflow nuevo `.github/workflows/docs-programado.yml`.

- Disparo: `schedule` semanal (lunes, 09:17 UTC) y `workflow_dispatch`. No corre en PRs ni en push: un sitio ajeno
  caído no frena ninguna fusión ni la conformación automática.
- Ámbito: los `.md` de `docs-as-code/**`, `README.md` y `SDD/Docs/**`. Se ignoran los nombres sin dominio (internos
  de contenedor), `localhost`, `127.0.0.1`, `example.com/.org` y los ejemplos con marcadores. 401, 403 y 429 no
  cuentan como caídos.
- Sólo avisa: si hay enlaces caídos, abre un issue con la tabla (o comenta el que ya esté abierto con el mismo
  título) y deja advertencias en la corrida; el job queda verde.
- Endurecimiento: `contents: read` por defecto y, sólo en el job, `issues: write`; sin secretos; sólo
  `actions/checkout@v4.2.2`; el control es `python3` del runner, sin dependencias. No compila ni despliega.

**Línea de base (corrida local del mismo guion sobre `main` en 8708663).** 19 enlaces externos y 6 caídos, todos en
`SDD/Docs/**`: la raíz de la Api pública y su `/auth/token` responden 404, y las cuatro direcciones del canal
`somee` (que el PO conservó como antecedente, `deploy-front-ftp.yml:29-31`) no resuelven. `SDD/Docs/**` es
generado: **se eleva, no se corrige** (`AGENTS.md:61`). `docs-as-code/**` y `README.md`, sin caídos.

**Qué no se tocó.** `docs.yml`, `ci.yml`, `src/`, la capa de IA del expediente 0002 ni `SDD/Docs/**`.

**Verificación.** Sintaxis YAML validada; el guion del paso se extrajo del workflow y se corrió en local con el
resultado de arriba. La corrida por `workflow_dispatch` queda para después de la fusión (el disparo manual sólo
existe para workflows de la rama por defecto).

**Efecto sobre el producto.** Sin cambio funcional: nada de esto entra en una imagen y ningún workflow despliega al
fusionar (`deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`).

Sigue: tramo T-8 (prueba negativa de `Documentacion:Publicada`) del plan del folio 003 · mantenimiento de docs-as-code (agente)
