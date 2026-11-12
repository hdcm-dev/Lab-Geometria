# EXP-0005 · Actuación 001 — Constancia: los destinos abren la acción

| Campo | Valor |
|---|---|
| Fecha | 2026-11-12 |
| Tipo | constancia |
| Autor | banco de traslado de Lab-Documentos (agente) |

## 1. Qué se construyó

- **Catálogo** (`src/GeometriaFactory.Application/Concepts/DestinationCatalog.cs`): `Destination.OpensWith` nombra el
  parámetro que el chip emite siempre con valor fijo `1`; `ver-pistas` → `?pistas=1`, `ejemplo-formato` → `?ejemplo=1`,
  los dos con `OpensAction = true`. `Validate` rechaza un `OpensWith` que no esté en `QueryParameters`, y la conformidad
  de arranque ya contrasta cada parámetro con un `[SupplyParameterFromQuery]` de la página. La revalidación por rol e
  instancia y el texto del chip según el rol no cambian.
- **`WorkView.razor`**: `?pistas=1` abre el nivel 1 de la **primera** advertencia como si se hubiera tocado su «Ver
  pistas»; el foco lo lleva el `autofocus` que el panel ya tenía. Usa la misma condición que dibuja el botón
  (`CanAskForHints`): sin advertencias, con pistas apagadas, para el docente o con otro valor, la página queda igual.
  Un envío del formulario manda sobre el enlace.
- **`WorkSubmission.razor`**: `?ejemplo=1` dibuja «Ver un ejemplo» desplegado (`open`) y con el foco en su rótulo.
- **Batería v3** (`ai-layer/ensayos/bateria-conceptos-v3.jsonl`): la v2 sin reescribir más un campo `ruta` en los
  nueve casos con chip `ver-pistas` o `ejemplo-formato`; `veredicto_conceptos.py` suma la falla `ruta`.
  `conceptos.py` lee la v3 por omisión y acepta `GEO_SEMILLA` para barajar el orden de los casos.
- La especificación 06 marca D-2 y D-3 como resueltas. **Ningún `appsettings` ni el manifiesto se tocaron:**
  `AI__ConceptsEnabled`, `AI__ConceptsModelWriter` y `AI__HintsEnabled` siguen en `false`.

## 2. Pruebas (E-001)

Nuevas: `Catalogo_PistasYEjemplo_AbrenLaAccion`, `Catalogo_AbreConParametroNoDeclarado_EsInvalido` (Application) y
`WorkView_ConPistas1_AbrePanelYEnfoca`, `…_SinAdvertencias_IgualQueSinParametro`, `…_PistasApagadas_IgualQueSinParametro`,
`WorkView_ConPistasDeOtroValor_IgualQueSinParametro`, `WorkSubmission_ConEjemplo1_DespliegaYEnfoca`,
`WorkSubmission_SinEjemplo1_IgualQueSinParametro` (Integration). Las de «igual que sin parámetro» comparan el marcado
entero, sin las marcas protegidas ni el eco de la dirección; las de pistas cuentan los pedidos al redactor (cero cuando
no corresponde).

- Con el catálogo y las páginas de `a672f85`: **3 rojas** (`Catalogo_PistasYEjemplo_AbrenLaAccion`,
  `WorkView_ConPistas1_AbrePanelYEnfoca`, `WorkSubmission_ConEjemplo1_DespliegaYEnfoca`). Las de «igual» pasan ahí
  por construcción: protegen contra una regresión, no prueban lo nuevo.
- Mutación (el catálogo emite, las páginas ignoran el parámetro): **3 rojas**, incluida la conformidad de arranque
  `Catalogo_RutasYParametros_ConformanConLasPaginas`.
- `scripts/test.sh` en contenedor (SDK 10.0.403, usuario no root, `HOME`, `NUGET_PACKAGES` y `/tmp` en el disco del
  sistema, `SkipVisorBuild=true`): Domain 95/95, Application 170 aprobadas y 16 omitidas (los casos conocidos de la
  act. 003 del EXP-0004, sin cambios), Integration 715/716 (1 omitida, la previa). **0 rojas.**

## 3. Batería v3 ×3 sin modelo (E-002)

Instancia propia del servicio de datos en contenedor, publicada sólo en `127.0.0.1`, corpus de ensayo con las nueve
fichas en `vigente` fuera del repositorio, `AI__ConceptsEnabled=true`, `AI__ConceptsModelWriter=false` y **sin
dirección de servidor de modelos**; 65 s entre corridas por el límite de tasa. Variación: orden de los casos barajado
con semillas 7, 14 y 21.

| Carpeta | Corrida | Código | p95 |
|---|---|---|---|
| [`v3-sin-modelo`](../../../../ai-layer/ensayos/corrida-conceptos-2026-11-12-v3-sin-modelo/) | 1 · 2 · 3 | 24/24 · 24/24 · 24/24 | 57 · 37 · 42 ms |

Rojo de la v3: los registros de `control-sin-modelo` (código previo) juzgados con la v3 dan **15/24** en las tres
corridas (9 fallas `ruta`), RECHAZADA. Veredicto de la v3: **PENDIENTE DE LECTURA DOCENTE** (0/27).
`veredicto_conceptos.py --verificar` reproduce las nueve carpetas.

## 4. Capturas (E-003)

`/trabajos/{id}?pistas=1` y `/trabajo-nuevo?ejemplo=1` a 1280 y 390, en esquema claro y oscuro. En las ocho, el foco
queda en la pista o en el rótulo del ejemplo y `scrollWidth == clientWidth`. El laboratorio no tiene tema oscuro
(act. 003 del EXP-0004): las capturas «oscuro» se ven iguales a las claras. La escena 3D no se dibuja porque la Web se
publicó con `SkipVisorBuild=true` (imagen sin Node); no afecta a lo medido.

## 5. Trazabilidad (regla → código)

| Regla | Código | Prueba |
|---|---|---|
| K2 §5.4: el chip abre la acción, no sólo la pantalla | `Destination.OpensWith`; `Resolve` agrega `?pistas=1` / `?ejemplo=1` | `Catalogo_PistasYEjemplo_AbrenLaAccion` |
| K2 §5: el chip depende del estado y del rol; ver no es hacer | revalidación por rol e instancia sin cambios; `CanAskForHints` en la página | mismo test (docente y sin instancia → nulo), `…_PistasApagadas_…` |
| K1 / límite didáctico: el destino no abre lo que la pantalla no ofrece ni da números | `OpenHintsFromLinkAsync` usa la condición del botón; valor fijo `1`, nada del texto viaja a la dirección | `…_SinAdvertencias_…`, `…_DeOtroValor_…` |
| Conformidad catálogo ↔ páginas (validar al arrancar) | `Validate`: `OpensWith` ∈ `QueryParameters` ∈ `[SupplyParameterFromQuery]` | `Catalogo_AbreConParametroNoDeclarado_EsInvalido`, `Catalogo_RutasYParametros_ConformanConLasPaginas` |
| K7 R9.1: batería como datos versionados, sin reescribir la anterior | `bateria-conceptos-v3.jsonl`, falla `ruta` en `veredicto_conceptos.py` | E-002 (24/24 y rojo 15/24) |
| K3 R2.9: la garantía es la configuración por omisión | ningún `appsettings` ni manifiesto tocado; batería sin modelo | diff vacío; registro con `FaltaEndpoint` |
