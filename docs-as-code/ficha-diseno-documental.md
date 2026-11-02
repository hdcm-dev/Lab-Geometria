---
doc_id: FIC-GEO-001
doc_type: ficha
title: Ficha de diseño documental de Lab-Geometria
description: Ficha A-01 del sistema documentador de Lab-Geometria, con inventario medido, decisiones D-01 a D-18, catálogo y decisiones H.
status: borrador
origin: ia-generated
confidence: media
owner: mantenimiento de docs-as-code (agente, expediente 0003)
last_review: 2026-11-02
audience: [desarrollo, agente, po, docente]
delivery: [publico]
traces: []
---

# Ficha de diseño documental — Lab-Geometria

| Campo | Valor |
| --- | --- |
| Proyecto y repositorios leídos, con SHA | Lab-Geometria **aaef547** (`origin/main` al abrir este PR); Lab-Geometria.Documentacion **0202722** |
| Base del diseño | La resolución de la mesa leyó Lab-Geometria en **c5afa0e**. Entre las dos bases entraron la capa de IA (expediente 0002, PR #237) y la apertura del expediente 0003 (PR #238). Toda cita de esta ficha se releyó en **aaef547**; las que se movieron se corrigen en §8 |
| Carpetas excluidas o sin lectura (`{excluidas}`) | `_legacy` (registro histórico), `ai-layer` y `0002-Capa-De-IA-Pistas-Y-Borrador` (de otra sesión, por acuerdo) |
| Fecha | 2026-11-02 (salida de `date -u +%F`, comando 10) |
| Quién aplica | Mantenimiento de docs-as-code (agente), por la resolución del expediente 0003 |
| Conocimiento aplicado | DocsAsCode-Designing-Specification **1.4** («borrador ensayado, no probado»). Huella SHA-256 del conocimiento: `cc489f3e84f53b869af4eb597c60f35fb801b383c3923d7856785e8cda51aede` |
| Caso | [Expediente 0003](../SDD/Expedientes/0003-DocsAsCode/README.md), folio 003 (resolución) |

## 1. Inventario (paso 1)

Los comandos se corrieron desde la raíz del repositorio en `aaef547`, con
`EXC=(_legacy ai-layer 0002-Capa-De-IA-Pistas-Y-Borrador)` y los arreglos `X` y `P` armados como manda el bloque de
§3.1 del conocimiento. El comando 1 (`dg.py trazas`) no se corre en este PR: la compuerta llega en el PR 2 y el
comando 1b la reemplaza, aproximado.

| Patrón de §3.1 | Comando | Cuenta | Lectura |
| --- | --- | --- | --- |
| ID declarado más de una vez | 1b | 55 | Aproximado: el 1b cuenta también encabezados de informes que **nombran** un ID ya declarado, p. ej. `### A-01 ·` en dos informes de `SDD/Docs/Audit/`, `### ADR-14003,` en `changelog.md:745` contra el ADR mismo, `### BT-00027 —` en `SDD/Docs/Audit/DoR-Tramo-k-2026-09-12.md:61` contra la tarea. Ninguno está en `docs-as-code/`. La compuerta lo confirma en el PR 2 |
| Versión declarada dos veces | 2 | 3 | `SDD/Docs/Producto/11-Documentacion/README.md` y los `11-Documentacion/README.md` de las dos unidades de entrega llevan `version:` y `**Versión:**`. Corpus generado: se eleva, no se edita |
| Estado «planificado» o equivalente | 3 | 1283 coincidencias en `SDD/Docs/Unidades-Entrega` | Ningún documento de esa carpeta lleva `status:` en frontmatter (`git grep -c '^status:'` → 0); el estado vive en la tabla de cabecera: 186 filas `\| Estado \| \`Pendiente\` \|`, casi todas del backlog técnico. La contradicción de estado de `SDD/Docs/README.md:156` ya la corrigió la versión 2.6 (`:274`), sin control que la vigile |
| Traza de la interfaz a una unidad que no le corresponde | 4 | 0 (rc=1) | Ningún archivo de `src/GeometriaFactory.Web` lleva `traces:` |
| Puntero a un documento obsoleto o ausente | 5 | 1 changelog; 16 líneas con obsoleto/retirado/eliminado/renombrado | Además, la deriva de `Contratos-REST.md:46-52` («Por qué no hay descripción formal de servicio») frente a `changelog.md:479-486` (documento OpenAPI generado, ADR-08008). Se eleva (folio 003, punto 3) |
| Advertencia del índice | 6 | 1 | En `ia-db/indexes/12_Observaciones-Del-Indexado.md` de Lab-Geometria.Documentacion (0202722), línea 84: archivos de `Docs/` contados como pendientes |
| Espacios de IDs en uso | 7 | 25 prefijos más frecuentes, de `CU` (8482) a `RA` (704) | Origen: el `doc_id` del SDD y su registro (`SDD/Docs/README.md:3-15`), ADR-nnnnn, BT-, expedientes. **Choques** con los Id del conocimiento: `A` (1826), `E` (3040), `H` (1016), `F` (2255) ya están en uso con otro sentido. Por eso las decisiones, artefactos y filas H de esta ficha se citan siempre junto con «ficha» o «expediente 0003», y los IDs nuevos llevan el segmento `GEO` |
| Plataformas | 8 | `net10.0` ×1 | `Directory.Build.props:11`. CI: `ubuntu-latest` con .NET `10.0.x` y Node `22` (`ci.yml:47`, `:60-62`, `:67-69`) |
| Prefijos nuevos libres | 9 | `FIC` 0 · `AUD` 0 · `RD` 0 · `PU` 0 | Los cuatro libres en todo el repo (salvo `{excluidas}`); con el segmento `GEO` también 0 |
| Huella del árbol | 11 | `023fec7d293ff36ae0bcef0d5ee4529c7838474e41b02dcffeaac37fd7a5518c` | Informativa: el repo trae `.git`, el ancla es el SHA |
| ¿Filtra por alcance? | lectura | Sólo lo propio, por identidad | `WorkEndpoints.cs:416-422`: el alumno lista sólo lo suyo («su alcance es su propia identidad»); no hay inquilinos ni ámbito por aplicación (`Role.cs:12-18`, dos valores) |
| Organización del runner | 8 / lectura | no condiciona | Con la copia con huella de la compuerta (D-09), el workflow corre en `ubuntu-latest` como `ci.yml:47` |
| **Visibilidad** | 13 / API | **Lab-Geometria: pública. Lab-Geometria.Documentacion: pública** | Medida en el expediente 0003, evidencia 002. Evidencia secundaria: `README.md:3` («…de la cátedra, público de consulta.») |

## 2. Decisiones

| Id | Valor | Evidencia (ruta:línea en aaef547) | Clase |
| --- | --- | --- | --- |
| D-01 | Panel con roles (Web Blazor, `Student` y `Administrator`) ∪ servicio con API (OpenAPI generado + explorador). Hay marco de especificación (SDD). `docs-as-code/` en este repo; la ia-db en el repo hermano | `src/GeometriaFactory.Domain/Values/Role.cs:12-18`; `src/GeometriaFactory.Api/Composition/ApiDocumentation.cs:37-40`; `SDD/Docs/README.md:140` | P |
| D-02 | Clases del catálogo de §3 | — | P |
| D-03 | **Sin sitio publicado.** GitHub muestra el repo público y el explorador de la API es su portal | `ApiDocumentation.cs:37-40` | H-01 (default aplicado) |
| D-04 | Registro de IDs: el `doc_id` del SDD. Lo nuevo en `docs-as-code/` lleva prefijo propio | `SDD/Docs/README.md:3-15` | P |
| D-05 | `docs-as-code/` en la raíz, fuera de toda imagen; la ia-db en el repo hermano | `deploy/Dockerfile:26-33`; `deploy/Dockerfile.web:42-46`, `:62-64`, `:75` | P |
| D-06 | Ancla: commit. Para la copia de `docs-guard`, su SHA en `source.lock` (PR 2) | — | P |
| D-07 | Prefijos `FIC`, `AUD`, `RD`, `PU`, con segmento `GEO` (`FIC-GEO-nnn`, `AUD-GEO-nnn`, `RD-GEO-nnn`, `PU-GEO-nnn`) | comando 9 en 0 para los cuatro | P |
| D-08 | Frontmatter: YAML **o** tabla Campo/Valor en lo heredado, que sólo avisa; en `docs-as-code/` se exige YAML | [`esquema-frontmatter.md`](esquema-frontmatter.md) | P |
| D-09 | `.github/workflows/docs.yml` propio, sin `paths`, endurecido. Motor: copia con huella de `docs-guard` en `.docs-guard/` más extensiones en `docs-as-code/controles/` | precedente de puerta sin filtro de rutas: `ci.yml:1` («EN TODO CAMBIO») | H-02 (default aplicado; apartamiento parcial avisado) |
| D-10 | Todo arranca en **avisa**. Pasa a **bloquea** sólo en `docs-as-code/**`, con par rojo/verde y 0 hallazgos. `SDD/Docs/**` sólo avisa. `_legacy/**` queda fuera | `AGENTS.md:65`, `:71`: un rojo frena la fusión automática de todas las sesiones | P |
| D-11 | PO/docente: filas H y la letra de la ayuda. Agente: PRs de `docs-as-code/` con fusión automática bajo G1–G4. Sesión de la capa de IA: `ai-layer/**`, expediente 0002 y el código de pistas. Orquestador SDD: `SDD/Docs/**` | `AGENTS.md:65`, `:71` | H-12 (default aplicado) |
| D-12 | Canales de agente: ia-db + `docs-as-code/` enlazado desde el README. `AGENTS.md` no se edita | `AGENTS.md:3-5`, `:61` | H-13 (default aplicado) |
| D-13 | Ayuda por rol en el punto de uso. **Estado actual:** el microtexto está literal en los `.razor`, en un idioma | `InitialProvisioning.razor:97-100`; `WorkView.razor:327-342` | P |
| D-14 | Nada privado entra en ninguno de los dos repos: ambos son públicos (medido) | expediente 0003, evidencia 002 | P |
| D-15 | Sin cuerpo formativo (CL-10 apagada). La ayuda didáctica va como CL-06 con el límite de [RD-01](reglas/RD-01-ayuda-no-resuelve.md) | `SDD/Docs/00-Contexto/Vision-Producto.md:53`, `:88` | P |
| D-16 | es-AR. No se traducen claves, IDs, términos del contrato (`Tipo`, `Largo`, `Radio`…) ni rutas `/v1` | `WorkSubmission.razor:421-422` | P |
| D-17 | Carril programado: ola 2 | — | P |
| D-18 | Perfil mínimo, sin extensiones. Un solo control: `delivery-publico` | [`AUD-geometria.md`](AUD-geometria.md) | P |

## 3. Catálogo activo

| Clase | Activa | Dónde vive | Ola |
| --- | --- | --- | --- |
| CL-01 Especificación | sí | `SDD/Docs/**` (generado, sólo lectura) + esta ficha | 1 |
| CL-02 RRD | mínima, apoyada en el SDD | SDD + `docs-as-code/reglas/` | 1 |
| CL-03 AGENTS.md | sí, **derivado, no se edita** | raíz | elevación |
| CL-04 Comentarios XML / `With*` | sólo aviso | `src/GeometriaFactory.Api` | 2 (H-09) |
| CL-05 OpenAPI | instantánea regenerada por la automatización | `docs-as-code/api/` | 2 |
| CL-06 Ayuda y punto de uso | sí | `docs-as-code/punto-de-uso/`, `docs-as-code/reglas/`, `.razor` | 1 (docs) / 2 (código) |
| CL-07 Técnica | es el SDD | `SDD/Docs/Unidades-Entrega/**` | — |
| CL-08 Índice de agentes | ia-db | Lab-Geometria.Documentacion | 1 (PR 4b) |
| CL-09 Expedientes | sí | `SDD/Expedientes/0003-DocsAsCode/` | 1 (PR 0, fusionado) |
| CL-10 Formativo | no | — | — |
| CL-11 Copia con huella | sí | `.docs-guard/` | 1 (PR 2) |
| CL-12 Cómo intervenir | sí | [`como-intervenir.md`](como-intervenir.md) | 1 (este PR) |
| CL-13 README/changelog | puntero en README | raíz (compartido con aviso) | 1 (PR 4a) |
| CL-14 Sitio | no | — | — |

## 4. Decisiones de clase H (aplicadas con su default, avisadas al PO)

Ninguna traba la ola 1. Si el Product Owner responde otra cosa, su respuesta entra al expediente 0003 como folio nuevo.

| Id | Decisión | Estado | Default aplicado | Quién decide |
| --- | --- | --- | --- | --- |
| H-01 | ¿Sitio publicado? | abierta, default | **No** | PO |
| H-02 | Motor de la compuerta | abierta, default | Copia con huella de `docs-guard` + extensiones (apartamiento parcial) | PO |
| H-03 | `docente incluye: alumno` | abierta, default | **Plana** (ver [`AUD-geometria.md`](AUD-geometria.md)) | PO |
| H-04 | API documentable en producción | abierta, default | `publico`, se mantiene | PO (configuración de despliegue) |
| H-05 | `Contrato-Agentes.md` | cerrada por la mesa | Elevar al orquestador SDD; no editar `SDD/Docs/**` | orquestador SDD |
| H-06 | Ola 2 en la Web | abierta, default | Después de 0002, con ok de la sesión de IA por archivo | PO |
| H-07 | Visibilidad de Documentacion | cerrada: pública (medida) | — | — |
| H-08 | Enlace a la ayuda larga | abierta, default | Sin enlace | PO |
| H-09 | Tocar el código de la Api | abierta, default | No en la ola 1 | PO |
| H-10 | Letra de plantillas y ayuda del alumno | abierta, default | Plantillas: de 0002 y del docente. Ayuda: la revisa el PO contra RD-01 | PO/docente |
| H-11 | Licencia del repo público | elevada | La mesa no propone ninguna (`SDD/Docs/README.md:150`) | PO |
| H-12 | Fusión de `docs-as-code/**` | abierta, default | Automática bajo G1–G4 (`AGENTS.md:71`) | PO |
| H-13 | Canales de agente | abierta, default | ia-db + `docs-as-code/` enlazado desde el README | PO |
| H-14 | «Bloquea» fuera de `docs-as-code/**` | abierta, default | Nunca en esta ola | PO |
| H-15 | Puntero a la regla en AGENTS.md | abierta, default | No: AGENTS.md es derivado (`AGENTS.md:3-5`) | PO |
| H-16 | Documentación de API restringida a desarrolladores específicos | abierta, default | No en estos repos: `publico` es el techo | PO |

**Fila obligatoria de API documentable:** la API (`/openapi/v1.json`, `/documentacion`) es H-04: exponerla o
ensancharla es una decisión de despliegue del PO (`ApiDocumentation.cs:101-103`).

## 5. Audiencias y entrega (D-18)

En [`AUD-geometria.md`](AUD-geometria.md): `alumno`, `docente` y `consumidor-api`, todas `publico`; descartados con su
motivo; control único `delivery-publico`.

## 6. Artefactos A-02 a A-13

| Id | Ruta | PR | Estado |
| --- | --- | --- | --- |
| A-02 | `.docs-guard/prefijos.txt` | 2 | sin probar hasta el par rojo/verde |
| A-03 | nota: [`esquema-frontmatter.md`](esquema-frontmatter.md) (este PR); esquema: `.docs-guard/esquemas/cuerpo.schema.json` (PR 2) | 1–2 | sin probar |
| A-04 | no se produce: registro del SDD | — | — |
| A-05 | no se produce: el texto de la regla se elevó (expediente 0003, folio 003) | 0 | — |
| A-06 | [`como-intervenir.md`](como-intervenir.md) | 1 | producido |
| A-07 | `.github/workflows/docs.yml` + `scripts/docs-local.sh` (sólo en el host o el runner) | 2 | sin probar |
| A-08 | línea de base en la descripción del PR 2 + `.docs-guard/excepciones.txt` vacío | 2 | — |
| A-09 | `.docs-guard/source.lock` | 2 | — |
| A-10 | `docs-programado.yml` | ola 2 | — |
| A-11 | línea en el README y entrada en la ia-db (no en AGENTS.md) | 4 | — |
| A-12 | [`AUD-geometria.md`](AUD-geometria.md) | 1 | producido |
| A-13 | `docs-as-code/controles/README.md` | 2 | sin probar |

## 7. Lo que el conocimiento no resolvió / apartamientos

| Sección | Qué quedó |
| --- | --- |
| D-09 / §5.7 | Controles propios (`delivery-publico`, `limite-didactico`, `anclas`, `pista-sin-enlaces`) como extensiones sobre la copia de `docs-guard`: **apartamiento parcial, avisado al PO** |
| §3.7 (P0) | El cómputo de esta ficha corrió en la máquina del agente, sobre un worktree limpio de `origin/main`; el comando 1 queda para la compuerta del PR 2 |
| D-13 | Convivencia de la ayuda estática con las pistas del modelo: la ayuda explica el campo y el formato; la discrepancia la comenta la capa de pistas |
| D-18 | Repo público: `publico` es el techo. No se crea `producto`, `restringido` ni `interno` |
| Canal de agente | El conocimiento supone que el canal derivado (AGENTS.md) se puede escribir. Acá es generado y de sólo lectura: se entrega por la ia-db y por `docs-as-code/`, y se eleva |

## 8. Verificación

| Comprobación | Estado |
| --- | --- |
| Citas ruta:línea | Releídas una por una en **aaef547** con `sed -n` sobre el worktree. Las que se movieron desde c5afa0e se corrigieron acá: el rechazo del docente en los endpoints de sólo-alumno pasó a `WorkEndpoints.cs:112`, `:173`, `:218`, `:255`; los chequeos de Administrator, a `:407` y `:460`; el rol desconocido cae en Student en `:544-551`; el filtro por identidad, a `:416-422`; la regla de forma del texto en `WorkSubmission.razor`, a `:388-389` y `:419-422`; la ayuda de `WorkView.razor`, a `:327-342` |
| Cita corregida | La resolución apoyaba «la autenticación por cliente se descartó» en `Roadmap-Producto.md:66`, que sólo **lista** el ítem. La evidencia del descarte es `ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md:1`, `:19` y `SDD/Docs/Audit/Mesa-2026-09-12-ciclo-2.md:199-200` («no se construye —pasa a `Descartada`—») |
| Visibilidad | Medida (expediente 0003, evidencia 002) |
| Barrido de datos privados | Sobre `docs-as-code/`: sin rutas de host, sin direcciones IP, sin nombres de máquinas (resultado en la descripción del PR) |
| Compuerta sobre A-02/A-03 y par rojo/verde | pendiente (PR 2). En este PR se corrió, sólo localmente y sin versionar, una copia de `docs-guard` (`esquema`, `trazas`) sobre `docs-as-code/` |
| Autoprueba de la copia | pendiente (PR 2) |
