# Constancia — tramo T-3: ayuda en el punto de uso

| Campo | Valor |
| --- | --- |
| Tipo | constancia |
| Fecha | 2026-11-02 |
| Autor | mantenimiento de docs-as-code (agente) |
| Corrige | — |

**Qué se hizo.** Se ejecutó el tramo **T-3** del plan del folio 003: el catálogo del punto de uso
`docs-as-code/punto-de-uso/catalogo.md` (`PU-GEO-001`), con una fila por campo y capa (`etiqueta`, `pista`, `info`,
`ejemplo`, `validacion`), contexto `web/<id-del-elemento>` y `ruta.razor:línea`; la ficha de la regla del texto del
trabajo `docs-as-code/reglas/RD-03-forma-del-texto-del-trabajo.md` (`RD-GEO-003`; `RD-GEO-002` queda para T-4A); y
las extensiones `anclas`, `limite-didactico` y `pista-sin-enlaces` en `docs-as-code/controles/`, con su par rojo/verde
en la autoprueba y su paso en `scripts/docs-local.sh`, todas en avisa.

**Frontera con el expediente 0002.** El catálogo explica el campo y el formato. No repite las plantillas de respaldo de
la capa de IA: `limite-didactico` las lee, sin tocarlas, y avisa si un texto las copia. No se tocó `ai-layer/**` ni el
código de las pistas.

**Hallazgos, no corregidos (código de la ola 2 o revisión del PO).**

1. `web/registration-email`: `aria-describedby` apunta a un `span` que sólo existe con `EmailAlreadyRegistered`
   (`AccountRegistration.razor:125`, `:127-130`); defecto WCAG 3.3.1 / 1.3.1 ya previsto para el PR 9.
2. La pista de contraseña está escrita cuatro veces (PR 10).
3. La pista de `web/submission-text` no nombra `Lado`, que el validador lee en el cilindro
   (`WorkSubmission.razor:424-425` frente a `LocalFigureValidator.cs:490`). Se eleva con la ficha RD-03.
4. El ejemplo de la pantalla de envío es un JSON completo de un cubo (`WorkSubmission.razor:428-445`). Si choca con el
   TP vigente no se puede decidir desde el repo: lo revisa el Product Owner (H-10).
5. El contexto que la resolución llamó `web/initial-provisioning-password` es, por el id real, `web/provisioning-password`.

**Apartamiento menor.** El plan del folio 003 no lista `scripts/docs-local.sh` en T-3; se agregó ahí el paso de los tres
controles, porque es el guion que corre `docs.yml` y sin él los controles no corren en la compuerta.

**Verificación.** `scripts/docs-local.sh` en verde; los tres controles nuevos con 0 hallazgos sobre `docs-as-code/**`
(19 filas, 5 pistas); la autoprueba da rojo con cada defecto plantado (texto de pantalla cambiado, id inexistente,
envío JSON completo, clase C#, plantilla de IA copiada, enlace en la pista) y verde sin ellos.

**Efecto sobre el producto.** Ninguno funcional: ninguno de estos paths entra en una imagen y ningún workflow despliega
al fusionar (`deploy-front-ftp.yml:38-39` es sólo `workflow_dispatch`).

Sigue: tramo de la regla de entrega (T-4A) del plan del folio 003 · mantenimiento de docs-as-code (agente)
