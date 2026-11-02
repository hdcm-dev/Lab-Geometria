# Capa de IA de la Fábrica de Geometría · Decisiones (ADR)

| Campo | Valor |
|---|---|
| Documento | `ai-layer/03-ADR.md` |
| Versión | 0.1 |
| Estado | Propuestas. Entran al registro de ADR del repositorio, con su número, cuando el Product Owner apruebe la capacidad (expediente 0002) |
| Fecha | 2026-11-01 |
| Formato | el del Conocimiento de especificación v0 §7: estado, ASR que lo origina, contexto, alternativas, criterio, decisión, consecuencias, confianza, deja de valer si, en el código |

**Correspondencia con el registro del repositorio (expediente 0002, actuación 007, 2026-11-02).** ADR-IA-01
→ `ADR-14006`, IA-02 → `ADR-14007`, IA-03 → `ADR-14008`, IA-04 → `ADR-14009`, IA-05 → `ADR-14010`, en
`SDD/Docs/Producto/Adrs/`. ADR-IA-06 (V2) sigue propuesta y sin número: V2 está diferida. Esta nota se
agrega; el texto de abajo no se reescribe.

## Índice

- [ADR-IA-01 · Dónde vive la llamada al modelo](#adr-ia-01--dónde-vive-la-llamada-al-modelo)
- [ADR-IA-02 · Contrato de acceso y adaptador del proveedor](#adr-ia-02--contrato-de-acceso-y-adaptador-del-proveedor)
- [ADR-IA-03 · Capas y comprobación de la regla de dependencia](#adr-ia-03--capas-y-comprobación-de-la-regla-de-dependencia)
- [ADR-IA-04 · Endpoint propio con respaldo de plantilla](#adr-ia-04--endpoint-propio-con-respaldo-de-plantilla)
- [ADR-IA-05 · Los nombres de la capa entran primero a la norma](#adr-ia-05--los-nombres-de-la-capa-entran-primero-a-la-norma)
- [ADR-IA-06 · V2: el borrador lo propone el modelo y lo firma el docente](#adr-ia-06--v2-el-borrador-lo-propone-el-modelo-y-lo-firma-el-docente)

## ADR-IA-01 · Dónde vive la llamada al modelo

| Campo | Contenido |
|---|---|
| Estado | propuesta (equivale a la ADR-01 mínima del v0 §7) |
| ASR que lo origina | REQ-IA-03 (`IFigureValidator` sincrónico y sin red, garantía G-6) y REQ-IA-04 (al modelo no viaja el JSON) |
| Contexto | V1 y V2 necesitan texto de un modelo; el validador, que es el corazón del producto, no puede depender de la red |
| Alternativas | (a) un método asincrónico nuevo en `IFigureValidator`: rompe G-6; (b) llamar al modelo desde la Web: rompe RA-02, la Api es dueña de las reglas; (c) **puerto propio por capacidad en Application, adaptador en `Infrastructure/AI`** |
| Criterio | no tocar G-6 ni RA-01..03; probar el caso de uso sin modelo [Con §2, P-1] |
| Decisión | (c): `IObservationHintWriter` (V1) e `ICommentDraftWriter` (V2) en `Application/Ports/`, adaptadores en `Infrastructure/AI/` |
| Consecuencias | se gana aislamiento y pruebas sin modelo; se pierde un poco de simplicidad (dos puertos más) |
| Confianza | alta: se apoya en contratos ya vigentes del repositorio y en P-1 |
| Deja de valer si | una capacidad necesita que el modelo decida algo que hoy decide el validador |
| En el código | REQ-IA-01..04 (pruebas especificadas, no escritas) |

## ADR-IA-02 · Contrato de acceso y adaptador del proveedor

| Campo | Contenido |
|---|---|
| Estado | propuesta (ADR-02 mínima) |
| ASR que lo origina | REQ-IA-02 y REQ-IA-15 |
| Contexto | el servidor de modelos de PROTO es uno del laboratorio y el de PROD no está decidido |
| Alternativas | (a) cliente HTTP propio contra un servidor; (b) **`IChatClient` con el adaptador elegido en la composición** [Con §2, P-2] |
| Criterio | cambiar de servidor sin tocar Application; probar con un doble |
| Decisión | (b); la dirección llega por configuración del ambiente, sin valor por omisión |
| Consecuencias | se gana el cambio de proveedor en un solo lugar; cambiar de adaptador no prueba igual conducta: la métrica se re-corre [Con §2, P-2] |
| Confianza | media: el adaptador concreto no se ensayó en este repositorio |
| Deja de valer si | el producto adopta otro contrato común de acceso a modelos |
| En el código | `Api/Composition/` (registro); REQ-IA-15 |

## ADR-IA-03 · Capas y comprobación de la regla de dependencia

| Campo | Contenido |
|---|---|
| Estado | propuesta (ADR-11 mínima) |
| ASR que lo origina | REQ-IA-01 y REQ-IA-02 |
| Contexto | las capas ya son **proyectos** (`GeometriaFactory.Domain`, `.Application`, `.Infrastructure`, `.Api`): el compilador impide que Application vea Infrastructure. Pero Persistence y la pieza de IA compartirían el ensamblado de Infrastructure, y la prueba vigente `DomainReferencesNoThirdPartyLibrary` acepta cualquier ensamblado con prefijo `Microsoft`, incluido el SDK de IA |
| Alternativas | (a) proyecto nuevo `GeometriaFactory.Infrastructure.AI`: el compilador lo impone, suma un nodo a la solución (puerta DC-5); (b) **carpeta `Infrastructure/AI/` con prueba de arquitectura** |
| Criterio | menor cambio estructural en un piloto; la regla se verifica en cada build |
| Decisión | (b), con REQ-IA-01 y REQ-IA-02 como pruebas nuevas |
| Consecuencias | se gana un piloto sin cambiar la solución; se pierde la garantía del compilador dentro de Infrastructure |
| Confianza | alta |
| Deja de valer si | la capa de IA pasa de una capacidad A0 a herramientas o agentes: entonces (a) |
| En el código | `tests/GeometriaFactory.Domain.Tests` y una prueba de arquitectura nueva |

## ADR-IA-04 · Endpoint propio con respaldo de plantilla

| Campo | Contenido |
|---|---|
| Estado | propuesta |
| ASR que lo origina | REQ-IA-07 y REQ-IA-08 |
| Contexto | A-18 `POST /v1/interpretaciones` previsualiza sin guardar y es sincrónico; un modelo sólo CPU tarda decenas de segundos |
| Alternativas | (a) sumar pistas a la respuesta de A-18: le cambia contrato y latencia; (b) **endpoint propio `POST /v1/interpretaciones/pistas`**, pedido aparte que la Web hace después de mostrar A-18 |
| Criterio | A-18 intacto; la falta del modelo nunca deja al alumno sin respuesta |
| Decisión | (b), con plantilla por par (figura, campo) para *degradado* y *rechazado*; sincrónico respecto del pedido, sin cola ni segundo plano (el v0 no lo decide, §7) |
| Consecuencias | se gana aislamiento de A-18; se pierde que la Web reciba todo en un pedido |
| Confianza | media: la latencia de PROTO se midió sólo en el ensayo de `ensayos/` |
| Deja de valer si | la latencia de PROD hace preferible un segundo plano |
| En el código | `Api/Endpoints/WorkEndpoints.cs` (ruta nueva); REQ-IA-07, REQ-IA-08 |

## ADR-IA-05 · Los nombres de la capa entran primero a la norma

| Campo | Contenido |
|---|---|
| Estado | propuesta |
| ASR que lo origina | ninguno de calidad: lo origina la regla del repositorio (Norma de Nomenclatura §6.1) |
| Contexto | la norma prohíbe escribir un identificador sin fila previa en el glosario |
| Alternativas | (a) escribir los nombres al construir; (b) **proponer las filas ahora** (`04-Propuesta-Norma-De-Nomenclatura.md`) |
| Criterio | la regla §6.1 del repositorio |
| Decisión | (b); las filas se agregan a la norma en el mismo cambio que apruebe la capacidad |
| Consecuencias | ninguna negativa: es la regla vigente |
| Confianza | alta |
| Deja de valer si | cambia la norma |
| En el código | ninguno hasta la aprobación |

## ADR-IA-06 · V2: el borrador lo propone el modelo y lo firma el docente

| Campo | Contenido |
|---|---|
| Estado | propuesta |
| ASR que lo origina | REQ-IA-21 y REQ-IA-22 de `02-Especificacion-V2-Borrador-De-Comentario.md` |
| Contexto | `ResolveWorkUseCase.ExecuteAsync` recibe un `comment` opcional; el comentario no es calificación (F-21, X-5 retirada) |
| Alternativas | (a) el modelo escribe el comentario al resolver: sería una escritura del modelo, nivel A1 con aprobación; (b) **el modelo devuelve un borrador que el docente edita y envía por el flujo vigente** |
| Criterio | quedarse en A0 [Con §2, P-7]; ninguna escritura del modelo [Con §2, P-6] |
| Decisión | (b): endpoint de sólo lectura para el borrador; la escritura sigue siendo la del docente con `ResolveWorkUseCase` |
| Consecuencias | ADR-06 y ADR-07 no aplican; el docente puede enviar el borrador sin leerlo (riesgo de confianza excesiva, `02-…` §6) |
| Confianza | alta |
| Deja de valer si | se pide que el modelo resuelva trabajos |
| En el código | ninguno hasta la aprobación |
