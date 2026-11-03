# Constancia — revisión externa, R-G1: instantánea OpenAPI en main

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La revisión externa de docs-as-code (sobre `main` en a1ff52d) dejó, entre otros, tres hallazgos sobre la
instantánea OpenAPI del tramo T-5 (folio 008):

- **A1.** El job `instantanea-openapi` de `.github/workflows/docs.yml` empujó la rama `automatico/instantanea-openapi`
  tras el #250, pero no pudo abrir el PR («GitHub Actions is not permitted to create or approve pull requests»). El
  `|| echo "::warning::..."` del último paso (`docs.yml:121-124` en a1ff52d) dejó el run en verde, y la instantánea
  de `main` quedó anterior a T-12.
- **M2.** La instantánea declaraba `"servers": [{"url": "http://127.0.0.1:5199/"}]` (`openapi-v1.json:7-9` en
  a1ff52d), la dirección local de la extracción.
- **B6.** El checkout del job persistía las credenciales (`docs.yml:96` en a1ff52d) mientras compila y ejecuta la Api.

**Qué se hizo** (alcance R-G1 = A1 + M2, con B6):

- Se regeneró `docs-as-code/api/openapi-v1.json` con `extraer-openapi.sh` sobre `origin/main` (a1ff52d) y entra por
  este PR con su entrada en `CHANGELOG-API.md` («Sin publicar»). Coincide con la rama automática salvo `servers`.
- `extraer-openapi.sh` quita `servers` del documento antes de escribirlo.
- `docs.yml`, job `instantanea-openapi`: checkout con `persist-credentials: false`; el token entra sólo en el
  `git push` y en `gh`. Si no puede abrir el PR, abre o comenta el issue de etiqueta fija `instantanea-openapi` (se
  busca por etiqueta, no por título) y sale con error.
- La rama huérfana `automatico/instantanea-openapi` se borra al fusionar este PR.

**Qué no se hizo.** No se cambió el ajuste del repositorio que permite a Actions crear PRs: es decisión del PO.
Mientras siga apagado, cada cambio de la superficie hará fallar el job y dejará el aviso en el issue; la rama quedará
lista para abrir el PR a mano.

**M1 (recuento), medido con `scripts/docs-local.sh`.** Sobre a1ff52d, `recuento-openapi` daba el
`ERROR recuento-cifra` y 17 avisos `recuento-codigos`. Con la instantánea al día quedan el `ERROR recuento-cifra` de
`Contratos-REST.md:75` («diecisiete» frente a 18 operaciones) y un aviso: A-09
(`POST /v1/cuentas/{id}/reseteo-de-contrasena`, `Contratos-REST.md:88`) declara 400 y la instantánea no lo documenta.
`SDD/Docs` no se corrige desde aquí y la Api no entra en este alcance: la cifra y la diferencia de A-09 se elevan al
orquestador SDD.

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, `e2e.yml`,
`deploy-front-ftp.yml`, el expediente 0002 ni `SDD/Docs/**`.

**Efecto sobre el producto.** Ninguno: cambian documentación, un guion de extracción y un workflow de docs. Ninguna
fusión dispara un despliegue (`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: elevar al orquestador SDD la cifra «diecisiete» y el 400 de A-09 · mantenimiento de docs-as-code (agente)
