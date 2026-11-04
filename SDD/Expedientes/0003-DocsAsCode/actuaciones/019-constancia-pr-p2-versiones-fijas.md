# Constancia — PR-P2 del portal: control `versiones-fijas` y `deploy-front-ftp.yml` fijado

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-03 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Origen.** La resolución del portal (folio 018, §7 y plan de PRs, fila PR-P2) pide un control `versiones_fijas.py`
que falle ante `uses: x@vN` sin parche, versiones `x.x` flotantes e imágenes sin digest, con el par rojo/verde sobre
`deploy-front-ftp.yml`. En `main` (3dfb075) ese workflow tenía cinco referencias flotantes:
`.github/workflows/deploy-front-ftp.yml:52` (`actions/checkout@v4`), `:55` (`actions/setup-dotnet@v4`), `:57`
(`dotnet-version: '10.0.x'`), `:60` (`actions/setup-node@v4`) y `:62` (`node-version: '22'`).

**Qué se hizo.**

- `docs-as-code/controles/versiones_fijas.py`: sin argumentos revisa `.github/workflows/*.yml` y `deploy/Dockerfile*`.
  Reglas `accion-sin-parche` (se exige `vN.M.P` o SHA de 40), `version-flotante` (en claves `*-version`, se exige
  `N.M.P`) e `imagen-sin-digest` (`FROM` e `image:` sin `@sha256:`; las etapas propias y `scratch` no cuentan).
  Sale 0, 1 o 2 como el resto de los controles.
- `deploy-front-ftp.yml` fijado con las mismas versiones que ya usa `docs.yml` (`checkout@v4.2.2`,
  `setup-dotnet@v4.3.1`, SDK `10.0.403`) y `setup-node@v4.4.0` con Node `22.23.4`.
- `autoprueba.sh`: rojo con los pasos del workflow tal como estaban (exige los cinco hallazgos), verde con las
  versiones fijadas, rojo y verde de un Dockerfile sin y con digest, y error de uso con un archivo inexistente.
- `scripts/docs-local.sh`: paso `workflows · versiones-fijas` en **avisa**. Pasa a bloquea tras una corrida limpia.

**Línea de base.** Con este PR `deploy-front-ftp.yml` queda en 0 hallazgos. Quedan hallazgos en `ci.yml`, `e2e.yml`
y `deploy/Dockerfile*`, que el folio 018 deja fuera de este PR (`deploy/Dockerfile*` no se toca en esta ola).

**Qué no se tocó.** `src/`, `Contracts/`, `tests/`, `visor/`, `deploy/`, `ai-layer/`, el expediente 0002 ni
`SDD/Docs/**`.

**Efecto sobre el producto.** Ninguno al fusionar: `deploy-front-ftp.yml` sólo corre con `workflow_dispatch`, y los
demás archivos tocados son de la compuerta de docs. La próxima publicación manual al hosting alternativo usará las
versiones fijadas.

Sigue: PR-P3, manifiesto `publicar.yml` vacío y `portal_manifiesto.py` en avisa; fijar `ci.yml`, `e2e.yml` y
`deploy/Dockerfile*` en un PR propio antes de pasar `versiones-fijas` a bloquea · mantenimiento de docs-as-code (agente)
