# Constancia — tramo T-9: defecto WCAG del correo en el registro de cuenta

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-9** (PR 9, ola 2) del plan del folio 003, solo y declarado como cambio
funcional en la Web. Defecto (WCAG 2.2, 3.3.1 y 1.3.1) registrado en `docs-as-code/punto-de-uso/catalogo.md:81`:
en `src/GeometriaFactory.Web/Components/Pages/AccountRegistration.razor` (base `d544e5a`), el campo de correo
apuntaba siempre a `aria-describedby="registration-email-error"` (`:125`), pero el `span` con ese id sólo se dibujaba
con `EmailAlreadyRegistered` (`:127-130`). Con `EmailDomainNotAdmitted` (`:85-93`, `:226`) la referencia quedaba
colgada y el campo, marcado inválido (`:126`), no tenía mensaje propio.

- El `span` `registration-email-error` se dibuja ahora en los dos estados de error del correo; con
  `EmailDomainNotAdmitted` dice «Ese correo no es de un dominio admitido.». No nombra dominios ni dice si el
  correo tenía cuenta (R-07).
- `aria-describedby` se emite sólo cuando el `span` existe; en los demás estados el atributo no se dibuja, así
  que no queda ninguna referencia a un id ausente.

**Qué no se tocó.** Sólo cambia ese archivo de `src/`. Ni `WorkSubmission.razor`, ni la capa de IA, ni
`tests/`, `Contracts/`, `deploy/`, el expediente 0002 o `SDD/Docs/**`. La fila de `catalogo.md:81` describe el
defecto ya corregido y la ancla de `:129` sigue válida; su actualización queda para el mantenimiento del catálogo.

**Verificación.** La que se pudo correr en local; el detalle está en la descripción del PR.

**Efecto sobre el producto.** Cambio funcional en la Web que despliega en el próximo build manual del PO. Ninguna
fusión dispara un despliegue (`deploy-front-ftp.yml` es sólo `workflow_dispatch`).

Sigue: tramos T-10 a T-12 del plan del folio 003 · mantenimiento de docs-as-code (agente)
