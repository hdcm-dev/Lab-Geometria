# Constancia — tramo T-11: componente AyudaDeCampo

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-11** (PR 11, ola 2) del plan del folio 003: el componente `AyudaDeCampo` y
el resolvedor de `contexto:`, con la decisión **H-08** en su default (sin enlace a la ayuda larga).

- **Componente.** `src/GeometriaFactory.Web/Components/Shared/AyudaDeCampo.razor` recibe el contexto de una fila del
  catálogo del punto de uso (`web/<id-del-elemento>`) y el texto de la capa `pista`, y dibuja un `span` con clase
  `gf-requirement` cuyo `id` es el que resuelve del contexto. Un contexto sin el prefijo `web/`, con el id vacío o con
  blancos o barras en el id se rechaza al dibujar.
- **H-08.** El componente no dibuja ningún enlace a `docs-as-code/ayuda/`.
- **Uso mínimo.** `CredentialRequirement.razor` (tramo T-10) dibuja ahora su texto con
  `AyudaDeCampo Contexto="web/credential-requirement"`. El `id` resultante, la clase y el texto visible son los
  mismos que antes; el texto sigue escrito en ese archivo, donde `anclas` lo vigila. El catálogo actualiza la línea
  de la fuente (`CredentialRequirement.razor:13`).
- **Control `anclas`.** Como el `id` ya no está escrito en el `.razor` sino que lo resuelve el componente,
  `docs-as-code/controles/anclas.py` acepta como ancla, además de `id="<id>"`, un `Contexto="web/<id>"`. Sigue en
  avisa.

**Qué no se tocó.** Ni `WorkSubmission.razor`, ni la capa de IA, ni `tests/`, `Contracts/`, `deploy/`, `visor/`,
`ai-layer/`, la Api, el expediente 0002 o `SDD/Docs/**`.

**Verificación.** La que se pudo correr en local; el detalle está en la descripción del PR.

**Efecto sobre el producto.** Cambio funcional en la Web que despliega en el próximo build manual del PO (el HTML
dibujado no cambia). Ninguna fusión dispara un despliegue (`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: tramo T-12 del plan del folio 003 · mantenimiento de docs-as-code (agente)
