# Constancia — tramo T-10: pista de la contraseña en una sola fuente y buscadores

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-10** (PR 10, ola 2) del plan del folio 003, con el alcance de la fila
`web/own-credential-*` y la fila de los buscadores del inventario del punto de uso.

- **Pista de la contraseña ×4.** En la base `caf4e6c` el requisito «Vas a usarla para entrar; no hay forma de
  recuperarla.» estaba escrito cuatro veces: `InitialProvisioning.razor:100`, `OwnCredentialSetup.razor:29`,
  `OwnCredentialForcedChange.razor:162` y `OwnCredentialChange.razor:105` (todos en
  `src/GeometriaFactory.Web/Components/Pages/`). Ahora vive una sola vez en el componente nuevo
  `CredentialRequirement.razor`, que las cuatro pantallas dibujan. El id del `span` pasa a ser
  `credential-requirement` en las cuatro y cada campo de contraseña nueva apunta a él con `aria-describedby`;
  cada pantalla dibuja el componente una sola vez, así que el id no se repite en ningún documento.
- **Buscadores.** Se auditó el nombre accesible de `accounts-search` y `works-search`: cada caja tiene `<label for>`
  propio (`AccountsPanel.razor:267`, `StudentWorkPanel.razor:177`), el ícono de lupa es `aria-hidden="true"`
  (`Components/Shared/Icon.razor:11`) y el `placeholder` sólo repite un ejemplo. No falta etiqueta: no hay cambio
  de código.
- **Catálogo.** `docs-as-code/punto-de-uso/catalogo.md` registra la pista una vez, con contexto
  `web/credential-requirement` y fuente en el componente; las observaciones de las dos filas quedan cerradas.

**Qué no se tocó.** Ni `WorkSubmission.razor`, ni la capa de IA, ni `tests/`, `Contracts/`, `deploy/`, `visor/`,
`ai-layer/`, el expediente 0002 o `SDD/Docs/**`.

**Verificación.** La que se pudo correr en local; el detalle está en la descripción del PR.

**Efecto sobre el producto.** Cambio funcional en la Web que despliega en el próximo build manual del PO (el texto
visible no cambia; cambian el id del requisito y su origen). Ninguna fusión dispara un despliegue
(`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: tramos T-11 y T-12 del plan del folio 003 · mantenimiento de docs-as-code (agente)
