# Constancia — tramo T-8: prueba negativa pareada de `Documentacion:Publicada`

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-8** (PR 8, ola 2) del plan del folio 003: la prueba negativa pareada de la
llave `Documentacion:Publicada` (`src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:43`, decidida en
`:109-110`). Archivo nuevo `tests/GeometriaFactory.Integration.Tests/ApiDocumentationPublishedSettingPairTests.cs`.

- Para `Production` y `Staging`, con la llave ausente o en `false`: `GET /openapi/v1.json` y `GET /documentacion/`
  dan 404. En la misma prueba, su par: mismo ambiente con la llave en `true`, las dos rutas dan 200.
- Complementa a `ApiDocumentationSurfaceTests.cs` (que ya mide `Production` sin llave y con ella): suma el `false`
  explícito y un segundo ambiente, de modo que la regla quede atada a «no es desarrollo» y no al nombre
  `Production`.

**Qué no se tocó.** `src/`, las pruebas de la capa de IA (`tests/GeometriaFactory.Integration.Tests/AI/**`), el
expediente 0002, `SDD/Docs/**` ni ningún workflow. Se avisa a Guía 16: el PR toca `tests/` con un archivo nuevo.

**Verificación.** `dotnet test tests/GeometriaFactory.Integration.Tests -p:SkipVisorBuild=true` en contenedor
`mcr.microsoft.com/dotnet/sdk:10.0.403`: 580 superadas, 1 omitida, 0 falladas.

**Efecto sobre el producto.** Sin cambio funcional: `tests/` no entra en la imagen y ningún workflow despliega al
fusionar (`deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`).

Sigue: tramo T-9 del plan del folio 003 · mantenimiento de docs-as-code (agente)
