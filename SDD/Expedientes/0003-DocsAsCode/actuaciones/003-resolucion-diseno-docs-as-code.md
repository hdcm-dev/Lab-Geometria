# Resolución — diseño DocsAsCode de Lab-Geometria

| Campo | Valor |
| --- | --- |
| Tipo | resolucion |
| Fecha | 2026-11-02 |
| Autor | presidencia de la mesa DocsAsCode (agente) |
| Corrige | — |
| Reintegra | ninguna |

**Decisión.** Se aprueba, 3 de 3 con condiciones, el diseño de la propuesta de la mesa corregido por su refutación. El
texto completo es la resolución de la mesa (evidencia 001, pieza 99). Lo sustancial:

1. Ficha A-01 en `docs-as-code/ficha-diseno-documental.md`; registro de audiencias `alumno`, `docente` y `consumidor-api`,
   todas `publico`, con el control único `delivery-publico`.
2. Ayuda en el punto de uso: un catálogo con capas cerradas, que **no duplica** las pistas de la IA (expediente 0002).
3. API: sin entrega restringida. La instantánea OpenAPI la regenera la automatización en push a main (ola 2).
4. Compuerta: `.github/workflows/docs.yml` propio, sin `paths`, sólo lectura, con copia con huella de `docs-guard`; todo
   arranca en avisa.
5. Regla de entrega por `docs-as-code/` enlazado desde el README y por la ia-db. **No se edita `SDD/Docs/**`.**
6. Ola 1: PRs 0, 1, 2, 3, 4a y 4b. Ola 2 declarada, con ok de Guía 16 por archivo en la Web.

**Veredicto por criterio.**

| Criterio | Veredicto | Observable |
| --- | --- | --- |
| La ola 1 no toca paths que cambian la imagen funcional (`src/`, `visor/`, `Contracts/`, `deploy/`) | cumple por diseño | `git diff --name-only` de cada PR |
| Toda fusión avanza la versión de la próxima imagen (`deploy/Dockerfile:33`); el despliegue es manual | declarado | columna del plan |
| No se edita el corpus generado (`AGENTS.md:61`) | cumple | elevación de este folio |
| Nada privado en los repos públicos | cumple | folio 002; A11 |
| La compuerta no frena a la otra sesión | cumple | «bloquea» sólo en `docs-as-code/**` |

**Elevación al orquestador SDD (sin edición).**

1. Agregar a `SDD/Docs/Producto/11-Documentacion/Contrato-Agentes.md` la regla de entrega, para que `AGENTS.md` se regenere:

   > Regla de entrega (expediente 0003). En Lab-Geometria y Lab-Geometria.Documentacion, ambos públicos, todo documento
   > lleva `delivery: publico`; no se crean piezas `producto`, `restringido` ni `interno`. El agente hereda la audiencia de
   > su usuario y por este canal sólo recibe `publico`. La ayuda de uso explica el campo y el formato; la discrepancia la
   > comenta la capa de pistas (expediente 0002) y la ayuda no la duplica. La forma de la API es pública cuando
   > `Documentacion:Publicada` está encendida (/openapi/v1.json, /documentacion); su instantánea la regenera la
   > automatización. Control: `delivery-publico` en docs.yml.

2. La fila «alumno» en el mapa de lectura por rol de `SDD/Docs/README.md` (líneas 127-136 en c5afa0e).
3. La deriva de `Contratos-REST.md` (líneas 46-52) frente a ADR-08008 (`changelog.md`, líneas 479-486).
4. La licencia del repositorio (`SDD/Docs/README.md`, líneas 144-150).

**Decisiones del Product Owner aplicadas con su default (no bloquean).** H-01 sin sitio · H-02 copia con huella +
extensiones (apartamiento parcial) · H-03 jerarquía plana · H-04 API `publico` · H-05 elevar · H-06 ola 2 después de 0002 con
ok por archivo · H-07 cerrada, pública · H-08 sin enlace · H-09 no tocar la Api en la ola 1 · H-10 plantillas de 0002 ·
H-11 licencia elevada · H-12 fusión automática G1–G4 · H-13 ia-db + docs-as-code · H-14 bloquea sólo en docs-as-code ·
H-15 sin puntero en AGENTS.md · H-16 sin documentación de API restringida en repos públicos. Si el Product Owner responde, su respuesta entra como folio nuevo con testimonio.

**Plan.**

| Tramo | PR | Paths |
| --- | --- | --- |
| T-0 | expediente | `SDD/Expedientes/0003-DocsAsCode/**` |
| T-1 | ficha y audiencias | `docs-as-code/**` |
| T-2 | compuerta | `.docs-guard/**`, `.github/workflows/docs.yml`, `scripts/docs-local.sh`, `docs-as-code/controles/**` |
| T-3 | punto de uso | `docs-as-code/punto-de-uso/**`, `docs-as-code/reglas/**`, `docs-as-code/controles/**` |
| T-4A | regla de entrega | `docs-as-code/regla-de-entrega.md`, `README.md` |
| T-4B | ia-db | Lab-Geometria.Documentacion `ia-db/indexes/00_MASTER-INDEX.md` |

Sigue: PRs T-1 a T-4B de la ola 1, con una constancia por tramo; elevación al orquestador SDD · presidencia de la mesa DocsAsCode · Cierra con: constancia de T-4B
