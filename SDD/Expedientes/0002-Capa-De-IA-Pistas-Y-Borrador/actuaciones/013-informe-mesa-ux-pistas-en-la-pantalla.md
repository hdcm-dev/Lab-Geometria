# EXP-0002 · Actuación 013 — Informe: mesa UX de la capa de IA y su aplicación en la pantalla (G1 a G6)

| Campo | Valor |
|---|---|
| Tipo | informe |
| Fecha | 2026-11-03 |
| Autor | implementador de la capa de IA (agente), por delegación del Product Owner |
| Responde a | pedido del Product Owner del 2026-11-03: «cómo integramos esa capa de IA de cara a la interfaz web» |
| Fuente | mesa de expertos «capa de IA de cara a la interfaz web», 2026-11-03 (dictamen y plan de la presidencia, ronda 2) |

**Expediente:** [EXP-0002](../README.md) · **Foliada:** 013 — no se reescribe

---

## Índice

- [1. La fuente: la mesa UX](#1-la-fuente-la-mesa-ux)
- [2. Lo que la mesa encontró en Geometría](#2-lo-que-la-mesa-encontró-en-geometría)
- [3. Lo que se aplicó: G1 a G6](#3-lo-que-se-aplicó-g1-a-g6)
- [4. Evidencia](#4-evidencia)
- [5. Lo que no cambia](#5-lo-que-no-cambia)
- [6. Lo que queda diferido](#6-lo-que-queda-diferido)

## 1. La fuente: la mesa UX

La mesa se convocó sobre la **interfaz en ejecución** de los dos productos con capa de IA (Geometría y
PushDispatch) y no sobre el modelo ni el prompt, que siguen en GEO-05 y D-NIVEL3. Participaron dos usuarios
estándar con las demos en vivo (una alumna en Geometría), cuatro comisiones —confianza, latencia, interacción
humano-IA y accesibilidad con didáctica—, un consultor de documentación, un refutador, un moderador de
objetivos y la presidencia. **Los informes de la mesa viven fuera del repositorio**; esta actuación es su
resumen para Geometría y su único registro aquí.

**Respuesta de la mesa:** la IA se integra como **ayuda subordinada al dato verificado y en el lugar donde la
persona puede actuar**, no como un bloque aparte. El problema dominante es de **flujo** (cuándo y dónde aparece
la pista) y sólo después de **bloque** (marca, espera, estados).

**Huecos declarados por la propia mesa:** ninguna pista del modelo se observó en vivo (0 de 6 intentos en
Geometría), y los tiempos medidos estaban contaminados por la mesa misma (cupo de 1 y modelo compartido): no
son métricas de producto ni se escriben en la interfaz.

## 2. Lo que la mesa encontró en Geometría

Ningún P0: ninguna cifra no verificada llegó a la pantalla y ninguna caída rompió el formulario.

| # | Hallazgo | Nivel |
|---|---|---|
| F1 | Las pistas sólo existían en la pantalla posterior al envío; desde el trabajo guardado no se pedían, y reintentar obligaba a reenviar y duplicaba trabajos | P1 |
| F3 | La Web cortaba a 10 s y el servicio de datos garantizaba la plantilla recién a los 20 s: **REQ-IA-07 roto desde la vista de la alumna** (6 de 6 intentos en error o plantilla) | P1 |
| F4 | Al ver las pistas desaparecía la advertencia verificada (los dos valores y su explicación) | P1 |
| F5 | La caída no ofrecía reintento y borraba también la ubicación | P1 |
| F6 | Espera muda: botón sin estado, región viva que nacía con la página, foco en un encabezado que no correspondía | P1 |
| F7 | Rótulo «Pista automática / de plantilla»: no decía IA ni calibraba, y «plantilla» es jerga | P1 |
| F9 | Los botones «Ver pistas» tenían el mismo nombre accesible y faltaba una salida después del nivel 3 | P2 |

F2 (la pista llega cuando el trabajo ya está Pendiente), F8 (cada nivel vuelve a pedir las tres pistas) y F10
(exactitud y voseo de la pista de modelo) quedan fuera de esta actuación (§6).

## 3. Lo que se aplicó: G1 a G6

Es el bloque «lo que se puede hacer ya» del plan de la mesa para Geometría. Maqueta y superficie se tocaron
juntas, y los nombres nuevos entraron antes a la Norma de Nomenclatura (§6.27.5, versión 1.35).

| # | Cambio | Dónde |
|---|---|---|
| G1 | **El servicio de datos corta antes que la Web**: `AI:TimeoutSeconds` pasa de 20 s por llamada a **8 s por pedido**, sumadas sus llamadas; la Web sigue esperando 10 s, ahora con nombre (`DataServiceClient.RequestTimeout`). La alumna recibe siempre, al menos, la pista general. Enmienda el punto 3 de `ADR-14012` (1.1) | `AiOptions`, `ModelObservationHintWriter`, `appsettings.json` de la Api, `Program.cs` de la Web |
| G2 | **Pistas desde el detalle del trabajo guardado**: «Ver pistas» por advertencia, sin reenviar; el texto se toma del trabajo; «Probar de nuevo» con el mismo pedido. Sólo para quien escribió el trabajo y con el flag | `WorkView.razor` |
| G3 | **La advertencia completa queda a la vista arriba de las pistas**, también en la caída: en el detalle, las pistas van dentro de la advertencia; en la pantalla de envío, la advertencia encabeza la vista de pistas | `WorkView.razor`, `WorkSubmission.razor` |
| G4 | Rótulos **«Pista redactada por IA: puede no ser exacta»** y **«Pista general del laboratorio»**, una vez por tarjeta; cada nivel lleva la marca en su texto accesible («Pista n de 3. …») | `HintsPanel.razor` (componente nuevo, compartido) |
| G5 | **Espera anunciada**: «Buscando una pista…» en el botón y en una región viva presente desde antes del pedido; **foco en la pista nueva**; título y encabezado propios de la vista de pistas | `surface-interaction.js` (`markPending` usa la acción que disparó el envío y escribe la región viva; ningún atributo nuevo), `HintsPanel.razor`, `WorkSubmission.razor` |
| G6 | **Una línea fija de qué hacer**: «Usá la pista para revisar tu programa. Para corregir el trabajo, cargá una versión nueva.» con enlace, también después del nivel 3; cada «Ver pistas» se describe con la ubicación de su advertencia | `HintsPanel.razor`, `aria-describedby` |

Documentos: `ai-layer/01-Especificacion-V1-Pistas.md` 0.3 (§3.2, §3.4 nueva, §4.5, REQ-IA-07 y 10),
`ai-layer/03-ADR.md`, `ADR-14012` 1.1, `Entornos-Deploy.md` §11.x, Norma de Nomenclatura 1.35 y las maquetas
`Envio-De-Trabajo.html` y `Vista-De-Trabajo.html` (estados «Pistas …»).

## 4. Evidencia

Las cifras de cada corrida van en el registro del PR. Pruebas nuevas o ampliadas:

- `HintsTimeoutTests`: el tope por omisión (8 s) queda por debajo de la espera de la Web (10 s); con un modelo
  que no contesta y dos advertencias, el pedido entero termina dentro de un tope y todas las pistas salen de
  plantilla; agotado el tope, la llamada siguiente no toca el modelo.
- `HintsWebSurfaceTests`: rótulos de G4 (visible y accesible), advertencia arriba de las pistas (G3), foco y
  región viva (G5), línea de acción (G6); `ASlowModel_StillGivesTheLabHint` (G1, a través de la Web);
  `FromTheSavedWork_HintsComeWithoutResubmitting` (G2: sin reenvío y sin duplicados);
  `ByDefault_TheSavedWorkHasNoHintsButton` (flag apagado, el detalle queda igual).
- E2E `PistasDeLaCapaDeIaTests`: en el banco local, con un servidor de modelos que acepta la conexión y no
  contesta nunca, la alumna pide pistas desde el detalle, oye «Buscando una pista…», recibe la pista general
  antes de los 10 s, con el foco en ella, la advertencia a la vista y el mismo único trabajo en el almacén.

## 5. Lo que no cambia

- **El contrato HTTP no cambia**: la ruta de pistas recibe el mismo cuerpo y el detalle manda el texto que
  ya trae el trabajo guardado. `openapi-v1.json` no se regenera.
- `AI:HintsEnabled` sigue en `false` en `main` y en producción (actuaciones 010 a 012).
- Las pistas no se guardan (REQ-IA-09) y el veredicto no cambia (REQ-IA-16).

## 6. Lo que queda diferido

| Ítem | Depende de |
|---|---|
| Exactitud y voseo de la pista de modelo (F10); re-medir la pista de modelo en vivo | GEO-05 y D-NIVEL3 |
| Caché de niveles (F8), precarga de niveles y feedback persistente | REQ-IA-09 |
| Estado de entrega con advertencia (Pendiente bloqueado o borrador), que decide si la pista llega a tiempo de corregir (F2) | Product Owner: flujo del producto, no capa de IA |
| Re-medición de los objetivos O1, O2, O3, O5, O6, O10 y O12 de la mesa con un solo consumidor del modelo | Una segunda ronda de la mesa, después de G1 |
