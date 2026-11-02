---
doc_id: PU-GEO-001
doc_type: anexo
title: Catálogo de la ayuda en el punto de uso
description: Ayuda de los formularios de la Web de Lab-Geometria por campo, en capas etiqueta, pista, info, ejemplo y validacion.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, docente, po]
delivery: [publico]
traces: [FIC-GEO-001, RD-GEO-001, RD-GEO-003]
---

# Catálogo de la ayuda en el punto de uso

Pieza CL-06 de la [ficha de diseño documental](../ficha-diseno-documental.md) (tramo T-3 del expediente 0003). Es la
**fuente documental** de la ayuda que la Web muestra junto a cada campo con regla. El texto visible **sigue en los
`.razor`**: este catálogo no lo reemplaza ni cambia funcionalidad; lo registra, lo traza y deja que la compuerta
compruebe que lo registrado y lo dibujado no se separen.

## 1. Cómo se lee

- **`contexto`**: `web/<id-del-elemento>`. El id es el atributo `id` del control en el `.razor`. La ayuda se ancla por
  **id**, no por texto: si el texto cambia, el id sigue resolviendo y el control `anclas` avisa la diferencia.
- **`capa`**, de un conjunto cerrado:

| Capa | Qué dice | Dónde está |
| --- | --- | --- |
| `etiqueta` | El nombre del campo, en el `<label for>` | En pantalla |
| `pista` | Cómo se completa el campo, bajo la etiqueta y unida con `aria-describedby` | En pantalla |
| `info` | Contexto que el campo no muestra: por qué la regla, qué pasa después | Sólo en este catálogo |
| `ejemplo` | Un ejemplo de forma, que no resuelve el trabajo ([RD-01](../reglas/RD-01-ayuda-no-resuelve.md)) | En pantalla o sólo acá, según la fila |
| `validacion` | El mensaje cuando el valor no cumple, junto al campo o en el aviso de la pantalla | En pantalla |

- **`fuente`**: `ruta.razor:línea` en `origin/main` (base: el commit en el que se fusionó el tramo T-2). Las capas
  `etiqueta`, `pista` y `validacion` copian el texto de esa línea; el control `anclas` comprueba que siga contenido en
  el archivo (sin etiquetas HTML ni comillas invertidas, con los blancos normalizados).
- **Frontera con las pistas de IA (expediente 0002).** La capa `pista` de este catálogo explica **el campo y el
  formato**. La discrepancia entre un valor declarado y el derivado la comentan las pistas que genera la capa de IA a
  pedido del alumno (`src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:146`, «Pistas para tu
  advertencia»). El catálogo **no las duplica ni las anticipa**: el control `limite-didactico` avisa si un texto
  repite una plantilla de respaldo de esa capa (`src/GeometriaFactory.Infrastructure/AI/TemplateHints.cs`).

## 2. Catálogo

<!-- catalogo:inicio -->
| contexto | capa | texto | fuente |
| --- | --- | --- | --- |
| `web/provisioning-password` | etiqueta | Contraseña | `src/GeometriaFactory.Web/Components/Pages/InitialProvisioning.razor:95` |
| `web/provisioning-password` | pista | Vas a usarla para entrar; no hay forma de recuperarla. | `src/GeometriaFactory.Web/Components/Pages/InitialProvisioning.razor:100` |
| `web/credential-new` | etiqueta | Contraseña nueva | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialSetup.razor:26` |
| `web/credential-new` | pista | Vas a usarla para entrar; no hay forma de recuperarla. | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialSetup.razor:29` |
| `web/forced-new` | etiqueta | Contraseña nueva | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialForcedChange.razor:159` |
| `web/forced-new` | pista | Vas a usarla para entrar; no hay forma de recuperarla. | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialForcedChange.razor:162` |
| `web/credential-change-new` | etiqueta | Contraseña nueva | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialChange.razor:100` |
| `web/credential-change-new` | pista | Vas a usarla para entrar; no hay forma de recuperarla. | `src/GeometriaFactory.Web/Components/Pages/OwnCredentialChange.razor:105` |
| `web/registration-email` | etiqueta | Correo | `src/GeometriaFactory.Web/Components/Pages/AccountRegistration.razor:123` |
| `web/registration-email` | validacion | Ese correo ya está registrado. | `src/GeometriaFactory.Web/Components/Pages/AccountRegistration.razor:129` |
| `web/registration-email` | validacion | Ese correo no es de un dominio que el laboratorio admita. Registrate con tu correo institucional. | `src/GeometriaFactory.Web/Components/Pages/AccountRegistration.razor:90` |
| `web/submission-date` | etiqueta | Fecha del trabajo, la que declarás vos | `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:365` |
| `web/submission-date` | validacion | este control no lo puede mostrar con esa forma. Elegilo de nuevo en el calendario. | `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:372` |
| `web/submission-text` | etiqueta | Texto del trabajo | `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:383` |
| `web/submission-text` | pista | Es el texto que imprime tu programa. El laboratorio lee las claves `Tipo`, `Largo`, `Ancho`, `Radio`, `Altura`, `Area`, `Volumen` y los conjuntos de componentes (`Caras`, `Bases`, `Laterales`, `Tapas`). Una clave con otro nombre se guarda, pero no se dibuja ni se verifica. | `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:419` |
| `web/submission-text` | info | La regla completa del texto, con lo que la pista no dice, está en la ficha RD-GEO-003. | `docs-as-code/reglas/RD-03-forma-del-texto-del-trabajo.md` |
| `web/submission-text` | ejemplo | Ver un ejemplo | `src/GeometriaFactory.Web/Components/Pages/WorkSubmission.razor:429` |
| `web/accounts-search` | etiqueta | Buscar por correo o nombre | `src/GeometriaFactory.Web/Components/Pages/AccountsPanel.razor:267` |
| `web/works-search` | etiqueta | Buscar por nombre | `src/GeometriaFactory.Web/Components/Pages/StudentWorkPanel.razor:177` |
<!-- catalogo:fin -->

## 3. Observaciones por fila

Hechos que el relevamiento encontró y que **no se corrigen acá**: el código es de la ola 2, con ok de la sesión de la
capa de IA por archivo (decisión H-06).

| `contexto` | Observación | Destino |
| --- | --- | --- |
| `web/provisioning-password` | La resolución del expediente lo llamó `web/initial-provisioning-password`; el id real del control es `provisioning-password` (`InitialProvisioning.razor:96`) y el catálogo usa el real | — |
| `web/*-new`, `web/provisioning-password` | La misma pista está escrita **cuatro veces**, una por pantalla (`InitialProvisioning.razor:100`, `OwnCredentialSetup.razor:29`, `OwnCredentialForcedChange.razor:162`, `OwnCredentialChange.razor:105`). El catálogo la registra una vez por contexto para que `anclas` vigile las cuatro | ola 2, PR 10: una sola fuente en código |
| `web/registration-email` | El control apunta siempre a `aria-describedby="registration-email-error"` (`AccountRegistration.razor:125`), pero ese `span` sólo se dibuja con `EmailAlreadyRegistered` (`:127-130`). Con `EmailDomainNotAdmitted` el mensaje va sólo en el aviso de la pantalla (`:85-93`) y la referencia queda colgada: defecto WCAG 3.3.1 / 1.3.1 | ola 2, PR 9, solo y declarado como cambio funcional |
| `web/submission-date` | La capa `validacion` copia la parte fija del mensaje; el día declarado lo inserta la pantalla (`WorkSubmission.razor:372`) | — |
| `web/submission-text` | La pista nombra cuatro conjuntos de componentes; el validador lee además `Lado` en el cilindro (`src/GeometriaFactory.Infrastructure/Figures/LocalFigureValidator.cs:490`). Ver [RD-03](../reglas/RD-03-forma-del-texto-del-trabajo.md), §3 | se eleva con la ficha de la regla |
| `web/submission-text` | El ejemplo de la pantalla es un JSON completo y correcto de un cubo de arista 3 (`WorkSubmission.razor:428-445`). Si el TP vigente pide un cubo, choca con las prohibiciones 1 y 3 de [RD-01](../reglas/RD-01-ayuda-no-resuelve.md). El repo no declara cuál es el TP vigente, así que **no se puede decidir desde acá**: el catálogo no copia el JSON y lo marca para que el Product Owner lo revise (H-10) | revisión del PO |
| `web/accounts-search`, `web/works-search` | Las dos cajas tienen `<label for>` propio (`AccountsPanel.razor:267`, `StudentWorkPanel.razor:177`); el `placeholder` (`:270`, `:180`) repite un ejemplo y no lleva información esencial. La auditoría del nombre accesible calculado sigue en la ola 2 | ola 2, PR 10 |

## 4. Cómo se agrega una fila

1. Elegir el `id` del control en el `.razor` y escribir `web/<id>`.
2. Copiar el texto de la capa tal como está en pantalla (sin etiquetas HTML) y su `ruta:línea`.
3. Contrastar el texto con [RD-01](../reglas/RD-01-ayuda-no-resuelve.md).
4. Correr `scripts/docs-local.sh`: `anclas`, `limite-didactico` y `pista-sin-enlaces` tienen que quedar sin hallazgos
   o con el hallazgo explicado en §3.
