# E-024 · Audit de cierre de M4, informe íntegro del auditor independiente

**Origen:** agente auditor independiente, invocado desde cero el 2026-09-13 con el encargo de refutar (`Master-Prompt.md` 8.19 §10), sobre `c14bf3b` y la compuerta E-022. **Transcripción literal**; el orquestador no editó el texto. Las correcciones que el orquestador aplicó están en la actuación 012 y en `Informe-Migracion-13.7-a-13.16.md` 1.1 §9.

---

# Audit de cierre de M4 — migración normativa `Lab-Geometria` SDD 13.7 → 13.16 (M6, tercera instancia, sobre lo ESCRITO)

## 1. Cabecera

| Campo | Valor |
|---|---|
| Alcance | Lo escrito por `c14bf3b` en `SDD/Intake/` y `SDD/Docs/` (24 documentos, 24 snapshots), más el estado de plan, mesa, informe y expediente tras esa escritura. Los siete P0 de `Master-Prompt-Migracion.md` 2.10 §10; los criterios de `Migracion-Rules.md` 3.20 §6 que alcanzan un salto sin migración estructural ni renumeración; comprobaciones 2, 6 y 8 de `Master-Prompt.md` 8.19 §10.0 que E-022 declaró no mirar |
| Base | `b9675d8` (corrida); comparación `255b55c` → `c14bf3b` (HEAD), rama `migracion/a-13.16-aplicacion`, worktree `/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria-mig1316b` |
| Commits mirados | `1d4afc4` (main = PR #205, corrida anterior), `255b55c` (actuación 011), `c14bf3b` (aplicación); framework `IA.SDD` `8c55a1e` (13.16) y su HEAD `650053e` (sin cambios en `SDD/Devs/`, sólo un expediente) |
| Auditor | Independiente, invocado desde cero, sin participación en la migración |
| Lo que no repetí | La reproducción byte a byte, los 7 diffs en reversa, los 24 snapshots, 24/24 versión de cabecera y 0 enlaces rotos de E-022 (`SDD/Expedientes/0001-Migracion-Normativa-A-13.16/evidencia/E-022-verificacion-de-lo-aplicado.txt`). No las refuto, salvo la comprobación (5), ver `M6-30` |

## 2. Resumen ejecutivo

**0 P0 · 2 P1 · 3 P2 · 8 P3.** Total 13 hallazgos, `M6-19` a `M6-31`.

**VEREDICTO: APROBADO CON OBSERVACIONES.** Lo escrito proviene de sus fuentes, nada se rellenó por inferencia, la procedencia sigue en 13.7, el estado previo está archivado y ningún parche se aplicó aguas abajo. Los dos P1 no están en el contenido migrado sino en **lo que el árbol afirma sobre sí mismo después de la escritura**: la deuda de cabeceras de `Mesa-2026-08-27.md` §8 tiene su evento **ocurrido** y el plan, la mesa y el informe siguen diciendo que no ocurrió (`M6-19`); y el manifiesto 7.0 afirma en su línea 3 y en §1.1 dos cosas que su propio §1 y su §1.1 desmienten (`M6-20`). Los dos se corrigen en la corrida y **antes de M5**, porque el paso 1 de `Master-Prompt-Migracion.md` §9 se contestaría en falso con el estado actual.

## 3. Los siete P0 de `Master-Prompt-Migracion.md` §10, sobre lo escrito

| P0 | Resultado | Cita y evidencia |
|---|---|---|
| Contenido inventado | **No** | Cada sección nueva remite a intake, manifiesto, `Web ADR-10008`, `ejemplo-*.md` o respuesta del PO. Verifiqué contra el árbol lo que E-022 no miró: `samples/` son 20 carpetas, 9 `Microsoft.NET.Sdk` y 11 `Microsoft.Build.NoTargets` (comando `find samples -name '*.csproj'` + `grep Sdk=`), coincide con `/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria-mig1316b/SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md:679` («**Los veinte** … nueve **con construcción** … once **sin construcción**») y con el manifiesto :150; ningún `src/*/*.csproj` referencia `visor/geometriafactory-visor.csproj` (manifiesto :247 «Cumple»); `visor/package.json:2` `"name": "geometriafactory-visor"` sostiene el perfil npm. Única fuente fuera de las tres de §4.1: el nivel de tres samples de Contracts, ver `M6-25` (P3) |
| Sección exigida rellenada con inferencia | **No** | Plantilla 3.6 §13.2 pregunta por el proyecto de otro ecosistema y §13.3 por capitalización: intake :426 y :471-477 contestan desde manifiesto-template 6.1 §1.2 (ejemplo de `/home/fernando/workspaces/workspace-dev/IA/SDD/IA.SDD/SDD/Devs/Intake/PRODUCT-MANIFEST-template.md:94-101`) y del árbol. Batería vacía sigue siendo cierta |
| Procedencia reescrita con migración parcial | **No** | `/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria-mig1316b/SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md:78` «Framework SDD (conjunto) \| **13.7**»; :98-99 plantillas 3.5 / 6.0. Pero la **línea 3** del mismo documento afirma que la 6.1 «es la que declara la procedencia de §1.1»: falso hoy, ver `M6-20` (P1, no P0: el bloque §1.1 no se tocó) |
| Corrección manual pisada | **No** | `git diff --stat b9675d8 255b55c -- SDD/Intake SDD/Docs ':!SDD/Docs/Audit/*'` → sólo `Roadmap-Producto.md` y `Mini-Plan.md`, ninguno de los 24 tocados; ninguna edición humana entre la base y la aplicación |
| Estado previo sin archivar | **No** | E-022 (4): 24 snapshots idénticos al blob de `255b55c`, en el `_legacy/2026-09-13/` de cada carpeta |
| Fila del plan sin resolver y sin declarar | **No, con reserva** | Las doce filas tienen estado (`Plan-Migracion-13.7-a-13.16.md:41-52`). **PM-11 está declarada con una premisa que `c14bf3b` volvió falsa** (:51 «**Esta M4 no escribe** (D-1): el evento no ocurre»): no es «sin declarar», es «mal declarada» → P1 `M6-19`, no P0. Si M5 arranca sin corregirla, ahí sí sería P0 por §9 paso 1 |
| Parche aguas abajo del defecto | **No** | La marca `(insumo de construcción)` nace en intake §13.2 (:418) y baja a manifiesto §2.B (:152), Vista §3 (:113) y Pipeline §4 (:127). Las dos correcciones `[CORREGIDO …]` de `Pipeline-Producto.md` :127 y :210 arreglan un defecto del propio documento contra su fila 1.6 |

## 4. Criterios de `Migracion-Rules.md` 3.20 §6 alcanzados por este salto

| Criterio | Resultado | Cita |
|---|---|---|
| Clasificación de proyectos confirmada / todo proyecto clasificado / CU no fusionados / `_fusion/` / solapamiento / cifras no promediadas / reconexión desde registro / cuatro pasos §4.3.2 / preservación de grupos / §4.3.1 por documento movido / árbol de migración / residuos de forma vieja / `_legacy/` no renombrado | **No aplica** | Plan :29 «**Ningún salto es major**», :33 «Renombres: **Ninguno**». Sin migración estructural ni renumeración |
| Contenido sin destino declarado | **Cumple** | Informe :95 (nota «2.0» a la fila 5.1); manifiesto :298 «**§2 deja de repetir la nota sobre la cita «2.0»**… registro de la fila 5.1» |
| Encabezados separados a los dos lados (E4) | **No aplica** | No hubo consolidación |
| Ninguna ancla apunta a encabezado inexistente | **Cumple (trivial)** | Los 24 documentos tocados tienen **0** enlaces con `#ancla` (script propio sobre `git diff --name-only 255b55c HEAD`); E-022 (8): 984 enlaces relativos, 0 rotos |
| Dos secciones con el mismo título | **No aplica** | Sin consolidación |
| Versión de cabecera = última fila, filas ordenadas (E7) | **No cumple, declarado** | Cabecera ≠ última fila en `Vista-Producto.md` (1.11 / 1.9), `Api/05/Arquitectura-Unidad-Entrega.md` (3.15 / 3.12), `Web/09/Pipeline-CI-CD.md` (3.9 / 3.6); orden mixto preexistente. Declarado en `Mesa-2026-09-13.md:208` `DD-7`. La aplicación no lo agravó. E-022 lo reporta como coherente: `M6-30` |
| Todo apartamiento vigente revisado con resultado | **Cumple** | Plan :60-63, cuatro «**No contemplado**»; `ADR-14001…14004` fila nueva «**Resultado: no contemplado.**» |
| Ningún apartamiento re-fundamentado | **Cumple** | `git diff 255b55c HEAD -- SDD/Docs/Producto/Adrs/` sobre los cuatro: sólo `Versión`, campo 6, campo 7 y fila de control. Campos 1 a 5 byte a byte iguales al snapshot `_legacy/2026-09-13/ADR-1400N-v1.x.md`. Contadores 4/4/3/1. Altas verificadas con `git log --diff-filter=A`: `f0f30ca`, `f0f30ca`, `4cc596b`, `407240a` |
| Todo hueco con ciclo clasificado; elevados < total | **Cumple parcial** | E-015 v3: 20 abiertas de 118 → 15/0/3/2, 2 < 20. **Los 8 ítems de `CU-0800N`, ahora con ciclo `aa3abd3` y abiertos, no están clasificados**: `M6-22` (P2) |
| Todo hueco sin ciclo con uno de los dos resultados de §4.9 | **Cumple** | Recuento propio sobre HEAD: 118 celdas, **95** con `base \`<hash>\`` y **23** `no derivable — anterior al mecanismo`, 0 con otra forma; por documento coincide con lo que cada fila de control declara (31/23/8, 33/28/5, 16/15/1, 1/0/1, 12/9/3, 16/13/3, 8/7/1, 1/0/1). Muestra: `PA-09` Api/06 :749 base `818997f` → `git grep -c … 818997f` encuentra el enunciado en `Proyectos/GeometriaFactory-Api/06-…/Product-Backlog.md` (proyecto = unidad de la sección, como D-5 manda) |
| Fuente declarada con uno de los tres valores de §2.1 | **Cumple** | Plan :37 y columna «Fuente» :41-49 |
| Ninguna sección con contenido fuera de las tres fuentes | **Cumple con observación** | `M6-25` |
| Ninguna sección exigida sin fuente rellenada | **Cumple** | Ver tabla §3 |
| Estado previo archivado antes de sobrescribir | **Cumple** | E-022 (4) |
| Corrección manual no pisada | **Cumple** | Ver tabla §3 |
| Cada documento del plan con clasificación §4.3, incluidos «no tocar» | **Cumple** | Plan :41-52, columna «Clasificación» |
| Intake verificado contra plantilla 3.6 completa, bump major | **Cumple** | Intake :3 «versión **3.6** … re-expresó»; :17 `5.0`; checklist :1818-1819 reescrito por unidad de entrega; fila 5.0 :1962 «Sube **major** por la tercera condición del caso (b)» |
| Orden intake → manifiesto → docs | **Cumple** | Guion por fases (`aplicar-propuestas.sh:16`, `--fase`), E-022 (1) reproduce las tres en ese orden. Un solo commit para las tres: `M6-29` (P3) |
| Degradación sin procedencia | **No aplica** | Procedencia 13.7 declarada |
| Procedencia sólo con cadena completa; parcial declarado documento por documento | **Cumple hoy** | Manifiesto :78 sigue 13.7. El informe 1.0 declara el parcial **de la corrida anterior** (:21 «Nada del corpus se escribió»); el estado tras `c14bf3b` no está declarado en ningún documento del árbol todavía: `M6-21` |
| Ninguna fila sin resolver y sin declarar | **Cumple con reserva** | `M6-19` |
| Renombres no inferidos | **Cumple** | Plan :33 «Leídos los ocho bloques» |
| Sin sustitución global de cadena | **Cumple** | 15 ocurrencias de «activo de construcción» siguen en registro (`git grep -i 'activo de construcci' HEAD -- . ':!*/_legacy/*' ':!SDD/Docs/Audit/*' ':!SDD/Expedientes/*'` → 15, idénticas a E-022 (9)); `Vista-Producto.md:65` conserva el nombre viejo junto al vigente (D-3) |
| Enlaces no reconectados por patrón / citas ambiguas / marcas del verificador | **No aplica** | No hubo reconexión ni verificador de preservación en este salto |

## 5. Hallazgos

### `M6-19` · P1 · **PM-11: el evento de cierre de la deuda de cabeceras ocurrió y el árbol dice que no**
- **Origen:** propio de la aplicación `c14bf3b` (la premisa «esta M4 no escribe» era cierta en `1d4afc4` y dejó de serlo al escribir). **Detectabilidad:** guion.
- **Citas.**
  - `/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria-mig1316b/SDD/Docs/Audit/Mesa-2026-08-27.md:249-250`: «**3 · Quién lo cierra** | El **orquestador de migración**, en la fase M4 de la próxima migración normativa, que ya reescribe cabeceras» / «**4 · En qué evento se cierra** | La **próxima migración normativa** de este destino que alcance artefactos, en su fase M4».
  - `…/SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md:51`: «Evento: la M4 de esta migración. **Esta M4 no escribe** (D-1): el evento no ocurre; P-03 y P-04 aplican la corrección a sus cabeceras y E-011 mide el resto».
  - `…/SDD/Docs/Audit/Mesa-2026-09-13.md:210`: «Esta M4 **no escribe** (D-1), de modo que el evento **no ocurrió** y el ítem sigue en forma».
  - `…/SDD/Docs/Audit/Informe-Migracion-13.7-a-13.16.md:90`: «PM-11 | Evento no ocurrido (esta M4 no escribe); declarado».
  - Commit `c14bf3b`, mensaje: «docs(migracion): M2 a M4 aplicadas» — 24 archivos de `SDD/Intake` y `SDD/Docs` reescritos con fila de control de cambios fechada 2026-09-13.
- **Evidencia.** Recuento de E-011 repetido sobre HEAD (mismo comando, `git grep -n -E "PRODUCT-(INTAKE|MANIFEST)[^|]{0,120}\*\*[0-9]+\.[0-9]+\*\*" HEAD -- "SDD/Docs/*.md" ":!*/_legacy/*" ":!SDD/Docs/Audit/*" | grep -i "Trazabilidad upstream" | wc -l`) → **116** (118 − las 2 de P-03/P-04). De esas 116, **11 están en documentos que esta M4 abrió, archivó y versionó** y cuya cabecera reescribió sin quitar el número: `…/SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/06-Backlog-Tecnico/Product-Backlog.md:11` «**Trazabilidad upstream:** […] **2.1**» (ídem `Api/05/Arquitectura-Unidad-Entrega.md:11`, `Api/09/Pipeline-CI-CD.md`, `Api/09/Supply-Chain-Seguridad.md`, `CU-08001` **1.13**, `CU-08002` **1.34**, `CU-08004` **1.14**, `CU-08005` **1.14**, `CU-08006` **1.13**, `CU-08007` **1.14**, `CU-08008` **1.13**). Es exactamente el caso que el campo 3 de la deuda anticipaba («que ya reescribe cabeceras»).
- **Norma.** `Root-Rules.md` 8.7 §12.2, tabla de escalamiento: «Ítem diferido **cuyo evento de cierre ya ocurrió** y sigue abierto | **Hallazgo P1**». `Master-Prompt.md` 8.19 §10.0 comprobación 6: «**es hallazgo el que nombre un evento de cierre que ya ocurrió**». Dictamen completo en §6.
- **Recomendación.** Ver §6.

### `M6-20` · P1 · **El manifiesto 7.0 afirma sobre sí mismo dos cosas que sus propias líneas desmienten**
- **Origen:** corrida anterior `1d4afc4` (propuesta `P-02-manifiesto-7.0.diff`), aplicada por `c14bf3b`. **Detectabilidad:** guion para (a) (grep de «declara la procedencia» contra la fila de plantilla de §1.1); lectura para (b).
- **Citas.**
  - (a) `…/SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md:3`: «**Plantilla de referencia:** `PRODUCT-MANIFEST-template.md` versión **6.1** (Framework SDD), **que es la que declara la procedencia de §1.1**» — contra `:99` «| `PRODUCT-MANIFEST-template` | 6.0 |». P-02 cambió el número y dejó la subordinada; el intake, en cambio, sí la retiró (`PRODUCT-INTAKE-…md:3` ya no dice «que es la que declara la procedencia»).
  - (b) `…/PRODUCT-MANIFEST-Fabrica-De-Geometria.md:107-109`: «**Y una constancia sobre el intake** […] El campo «Intake (origen)» de §1 y la §2 de este documento citan `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **3.0**, y el intake está en **3.1** […] **La cita es correcta y no se toca**» — contra `:18` «| Intake (origen) | — | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **5.0**» y `:298` «**re-derivado del intake 5.0**». La constancia vive dentro de §1.1, que la fila 7.0 declara «**no se toca**: es de M5» (:298), y por eso quedó contradicha en vez de retirada.
- **Por qué P1 y no P0.** El bloque de procedencia (tabla :76-99) no se reescribió; las dos frases son afirmaciones **sobre el documento** que hoy son falsas, y `Migracion-Rules.md` §4.6 punto 1 sólo eleva a P0 la procedencia vigente sobre árbol a medias. Pero si el PO no confirma M5 (`Master-Prompt-Migracion.md` §9 paso 3: «la procedencia **no se toca**»), las dos falsedades quedan indefinidamente.
- **Recomendación.** En M5, al reescribir §1.1 completo, retirar (b) al control de cambios (como la propia §1.1 :64-66 manda: «una procedencia que arrastra la historia […] deja de decir contra qué rige el destino hoy») y dejar (a) cierto. Si M5 no procede, corregir las dos líneas con una fila 7.1 (parche de cabecera con el precedente de la 3.1 del intake).

### `M6-21` · P2 · **Plan, expediente y README no registran que se escribió**
- **Origen:** propio de `c14bf3b` (omisión). **Detectabilidad:** lectura (índice contra archivos: guion).
- **Citas.** `…/SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md:8`: «**Estado:** **Presentado al Product Owner, sin aprobar** […] **Mientras no se apruebe, nada se escribe en `SDD/Intake/` ni en `SDD/Docs/`**» (aprobado en 011, escrito en `c14bf3b`). `…/SDD/Expedientes/0001-Migracion-Normativa-A-13.16/README.md:7` «espera la aprobación del Product Owner de los puntos A y B»; `:10` «`migracion/a-13.16`, worktree `Lab-Geometria-mig1316` […] **Sin push, sin pull request, sin merge**» (el commit `1d4afc4` es «Merge pull request #205», la rama actual es `migracion/a-13.16-aplicacion`); índice §1 sin la actuación 011 (`git diff --stat 1d4afc4 HEAD -- SDD/Expedientes/` → sólo `011-…md`); `:88` «**Propuestos y no escritos**»; `:96` «M2 a M4 hechas como siete propuestas verificadas y **no escritas**»; §3 sin filas de `c14bf3b`; §2 sin `E-022`, que además está sin commitear (`git status` → `?? …/E-022-verificacion-de-lo-aplicado.txt`). La aplicación misma (el acto que escribe el corpus) **no tiene actuación foliada**, contra la forma del expediente (`README.md:14-17` «actuaciones foliadas que no se reescriben»).
- **Recomendación.** Plan 1.4 (estado «Aprobado, actuación 011; aplicado en `c14bf3b`»; PM-11 corregida por `M6-19`); actuación 012 «informe: aplicación de M2 a M4» con E-022 indexado y con SHA-256; carátula con rama, PR #205 y estado; informe 1.1 con esta ronda. `Master-Prompt-Migracion.md` §10 «Salida»: path único, de modo que esta ronda es una versión nueva del mismo informe (observación `M6-18` ya declarada al framework).

### `M6-22` · P2 · **Los ocho ítems de `CU-0800N` tienen ciclo de origen y no tienen clasificación §4.8**
- **Origen:** corrida anterior `1d4afc4` (`M6-01` agregó `P-06b` y el universo 126; E-015 v3 no se extendió). **Detectabilidad:** guion.
- **Citas.** `…/SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md:46`: «universo **126** […] Clasificación §4.8 de las **20 abiertas**». `…/evidencia/E-015-clasificacion-4.8.txt:4` «Totales sobre las 20 filas abiertas». `…/SDD/Docs/Producto/Contratos-Inter-Unidad/CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md:112`: «**5 · Ciclo de origen:** derivado […] (2026-08-29) · producto · base `aa3abd3`», con el ítem abierto («**4 · En qué evento se cierra:** la **próxima emisión de la 06**, o la **Fase J**», :111).
- **Norma.** `Migracion-Rules.md` §6: «**Todo hueco con ciclo de origen quedó clasificado** como hueco del ciclo o hueco de norma posterior (§4.8)».
- **Recomendación.** E-015 v4 con las 8 filas (paso 3 sobre `aa3abd3`: el destino regía 13.7 desde el 2026-08-27, de modo que cualquier exigencia ≤ 13.7 es hueco del ciclo); plan 1.4 PM-06 con 28 abiertas clasificadas, elevadas 2 < 28.

### `M6-23` · P2 · **Nueve ítems abiertos que M4 tocó nombran un momento y no un artefacto, y no se puede decidir si el evento ocurrió**
- **Origen:** aguas arriba (`Mesa-2026-08-29.md` parche `P-02`, commit `aa3abd3`, para los 8 `CU`; `aa154ef` 2026-08-11 para `PD-05`). **Detectabilidad:** lectura. **No concluyente** en cuanto a la ocurrencia.
- **Citas.** `…/SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md:598`: «`05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`, su próxima emisión | **Vigente.** No hubo emisión nueva de la 05». Ese documento pasó por 2.0 (consolidación 2026-08-16), 3.0 (M4 de 9.12→10.0, 2026-08-19) y hasta 3.15 (hoy) desde el alta del ítem (`Api/05/Arquitectura-Unidad-Entrega.md:1252-1267`). `CU-08001…08008` §9 campo 4: «la **próxima emisión de la 06**, o la **Fase J**»; la 06 de las dos unidades lleva 4.4, 4.5 (parches) y 4.6 (esta M4) desde el 2026-08-29 (`Api/06/Product-Backlog.md:810-812`).
- **Norma.** `Root-Rules.md` §12.2 punto 4: «**En qué evento se cierra, nombrando un artefacto y su sección** — no un momento»; §10.0 comprobación 6 sólo es enumerable «porque el evento se declara como **artefacto y sección**». Si «emisión» incluye una versión nueva por migración, los 9 están vencidos (P1); si sólo la emisión por el titular de la categoría, siguen conformes. El árbol no lo decide.
- **Recomendación.** No reescribir el evento en esta migración (§4.1: nadie lo decidió). Declararlo en el informe 1.1 como observación al titular de la 05 y de la 06, y en `DD-6` o una `DD-8` con evento abrible. Los 19 restantes están atados a `Roadmap-Producto.md` §2.1 fase `i`, que `Estado-Del-Destino-2026-09-12.md:72` declara «sigue abierta»: conformes.

### `M6-24` · P3 · **Doce documentos subieron de versión sin mover su fecha de cabecera; los otros doce sí**
- **Origen:** corrida anterior `1d4afc4` (los diffs `P-06b` y `P-08` no tocan `Fecha`; `P-03`, `P-04`, `P-06` sí). **Detectabilidad:** guion.
- **Citas.** `…/SDD/Docs/Producto/Adrs/ADR-14001-Archivado-Central-De-La-Migracion.md:7` «**Fecha:** 2026-08-16» con fila 1.3 fechada 2026-09-13; `CU-08001…md:7` «**Fecha:** 2026-08-11» con fila 1.9 de 2026-09-13; contra `Vista-Producto.md:8` «**Fecha:** 2026-09-13». Patrón preexistente en los CU (la 1.8 de 2026-08-29 tampoco la movió), pero la aplicación lo consolidó de forma despareja.
- **Recomendación.** Anotarlo en `DD-7` (registros de cambios) como parte de la misma pasada de ordenamiento; no tocar ahora.

### `M6-25` · P3 · **El nivel de los tres samples de Contracts sale de una fuente que el plan no declara**
- **Origen:** corrida anterior `1d4afc4` (`P-01`). **Detectabilidad:** lectura.
- **Citas.** `…/SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md:1962` (fila 5.0): «el nivel sale del `ejemplo-*.md` de `10-Examples` que gobierna cada sample (documento hermano)». `Mesa-2026-09-13.md:204` `DD-3`: «Documento de ejemplos de `GeometriaFactory-Contracts` […] Se perdieron en la consolidación». Para `samples/contracts/01-basico` (intake :690-692) no existe `ejemplo-*.md`; el valor «Básico» está en `samples/contracts/01-basico/README.md:5` «**Nivel:** Básico», que es código, no una de las tres fuentes de `Migracion-Rules.md` §4.1. Los 17 restantes sí tienen su `ejemplo-*.md` (Api: 13 archivos; Web: `ejemplo-01-basico.md:10-11`, `02`, `03`, `01-datos-seed`).
- **Recomendación.** Declararlo en el plan 1.4 PM-01 («nivel de Contracts: del README del sample, hasta que `DD-3` emita el documento»). Sin cambio de contenido.

### `M6-26` · P3 · **Residuos del modelo por proyecto en documentos que sus filas de control declaran limpios**
- **Origen:** aguas arriba (prosa previa) dejado por `P-03`/`P-04` (`1d4afc4`). **Detectabilidad:** guion (grep).
- **Citas.** `…/SDD/Docs/Producto/Pipeline-Producto.md:70`: «sus categorías `09` registran el apartamiento del modelo de canales `preview` / `stable` que la guía fija para el tipo `library`» — los proyectos de código no tienen categorías `09` propias ni tipo D8, y la fila 1.9 (:232) dice que «la columna «Tipo D8» por proyecto de código pasa a stack y unidad». `…/SDD/Docs/Producto/Vista-Producto.md:11`: «**Trazabilidad downstream:** `06-Backlog-Tecnico`, […] `11-Documentacion` **de los siete proyectos de código**», en la cabecera que P-03 reescribió una línea más arriba.
- **Recomendación.** Dos líneas en la próxima edición de cada documento; anotar en `DD-6` (forma previa que este salto no alcanza).

### `M6-27` · P3 · **`Pipeline-Producto.md` afirma una conducta del build sin captura versionada**
- **Origen:** corrida anterior `1d4afc4` (`P-04`). **Detectabilidad:** lectura.
- **Citas.** `…/SDD/Docs/Producto/Pipeline-Producto.md:127`: «**Sin el modo y sin Node, la construcción falla** (`MSB3073`, código 127), sin nombrar la cadena que falta» — la celda cita `E-012` sólo para la otra conducta («medición de la construcción sin bundle con el modo»). La única traza de `MSB3073` en el expediente es el informe del panel, `…/evidencia/E-019-panel-consolidado.txt:36` («sin bandera y sin Node, MSB3073 código 127»), sin salida de comando. `grep MSB3073 E-012-build-y-tests-base.txt` → vacío.
- **Recomendación.** Una evidencia `E-023` con la corrida sin modo y sin Node (dos líneas), o citar `E-019:36` desde la celda. La afirmación es sobre el estado del sistema (D9).

### `M6-28` · P3 · **El lote preguntó por el plan 1.2 y la aprobación asienta el 1.3**
- **Origen:** corrida anterior `1d4afc4` (`0f26c2b`). **Detectabilidad:** lectura.
- **Citas.** `…/actuaciones/010-resolucion-lote-al-product-owner.md:11` «plan 1.2, mesa 1.1»; `:17` «A · ¿Aprobás el plan de migración **1.2**?» — en ese mismo commit `git show 0f26c2b:SDD/Docs/Audit/Plan-Migracion-13.7-a-13.16.md | grep Versión` → `1.3`. `…/actuaciones/011-testimonio-aprobacion-plan-e-intake.md:23` «**A · Aprobado el plan de migración 1.3**». La diferencia 1.2 → 1.3 son `M6-14`/`M6-15`, cuyos efectos (puntos D y E) sí estaban en el lote que el PO leyó: sin efecto material, pero el objeto aprobado quedó nombrado con dos números.
- **Recomendación.** Una línea en la actuación 012 que lo deje asentado; no reescribir 010 ni 011.

### `M6-29` · P3 · **Un commit para tres fases, y M4 sin sus cortes, sin declararlo**
- **Origen:** propio de `c14bf3b`. **Detectabilidad:** lectura.
- **Citas.** `…/README.md:97`: «`--fase intake` […] `--fase manifiesto`, `--fase docs`; **commit por fase**; audit de cierre». `git log --oneline 255b55c..HEAD` → un solo commit. `Master-Prompt-Migracion.md` §8 «**Cortes y audit.** M4 se corta por categoría de nivel producto y por unidad de entrega. En cada corte se invoca el audit». El expediente eligió un audit de cierre único (actuación 005 :24 «un audit de cierre») sin nombrarlo como apartamiento del §8.
- **Recomendación.** Declararlo en la actuación 012 (origen del hecho: propio de la corrida, calculado). El orden se preserva dentro del guion y E-022 (1) lo reproduce, así que no hay efecto sobre D6.

### `M6-30` · P3 · **E-022 (5) declara «versión de cabecera = última fila» computando el máximo**
- **Origen:** propio de `c14bf3b` (evidencia). **Detectabilidad:** guion.
- **Citas.** `…/evidencia/E-022-verificacion-de-lo-aplicado.txt:44-45`: «# (5) Versión de cabecera = última fila del control de cambios […] coherentes: 24 de 24». Sobre HEAD, última fila literal: `Vista-Producto.md` 1.9 (cabecera 1.11), `Api/05/Arquitectura-Unidad-Entrega.md` 3.12 (3.15), `Web/09/Pipeline-CI-CD.md` 3.6 (3.9). `Migracion-Rules.md` §6 E7: «coincide con **la última fila** de su registro, y las filas están ordenadas». Es `DD-7`, ya declarada; la compuerta la reporta como cumplida en lugar de como declarada.
- **Recomendación.** Que E-022 diga «máximo de la tabla» o remita a `DD-7`.

### `M6-31` · P3 · **Fase `k` cerrada sobre una dependencia (`i`) que el propio destino declara abierta** — fuera del salto, **no concluyente**
- **Origen:** aguas arriba (`Roadmap-Producto.md` 1.13/1.14, actuación 002). **Detectabilidad:** lectura.
- **Citas.** `…/SDD/Docs/00-Contexto/Roadmap-Producto.md:132`: «| `k` | `i` | Exponer la API a terceros sobre una topología que todavía no está desplegada de verdad no tiene sentido»; `:97` «**fase cerrada con OK explícito del Product Owner del 2026-09-13**»; `…/SDD/Docs/Audit/Estado-Del-Destino-2026-09-12.md:72` «la fase `i` sigue abierta». 19 de los 20 ítems abiertos de los 8 documentos tocados dependen de `i`.
- **Recomendación.** No es de la migración; que la próxima reanudación lo lea como origen del hecho ajeno a la corrida. Lo asiento porque el audit de cierre es el que debe contar los ítems atados a `i`.

## 6. Dictámenes

### 6.1 PM-11 (punto 3 del encargo)

**El evento ocurrió.** El campo 4 de `Mesa-2026-08-27.md:250` no dice «una M4 que reescriba estas cabeceras»: dice «la **próxima migración normativa** de este destino **que alcance artefactos**, en su fase M4». Esta migración alcanzó artefactos (plan :29: superficies en cinco reglas y dos plantillas) y su M4 escribió 24 documentos con fila de control fechada. La condición de la mesa del 2026-09-13 para decir «no ocurrió» era D-1 (`Mesa-2026-09-13.md:210` «Esta M4 **no escribe** (D-1)»), y D-1 se levantó con la actuación 011. Nada más sostiene la negación.

**Qué corresponde**, por `Root-Rules.md` §12.2 y `Master-Prompt.md` §10.0 comprobación 6, y sin escribir lo que nadie decidió (`Migracion-Rules.md` §4.1):

1. **Registrar el P1** (`M6-19`) en el informe 1.1 y en el plan 1.4 PM-11, con el recuento sobre HEAD (116; 11 en documentos que esta M4 abrió).
2. **Cerrar en forma parcial, declarada** — que es lo que la mesa aprobó por 4-1 en el veredicto 21 (`Mesa-2026-09-13.md:162`: «Fila de plan en M4, con medición previa de qué celdas son registro; **cierre parcial declarado si no se aplica entero**»). Es una decisión ya tomada, con fuente: no es invención aplicarla. Cerrado: 2 de 118 (P-03, P-04). Abierto: 116.
3. **Reasignar el evento con artefacto y sección** (§12.2 punto 4), porque repetir «la M4 de la próxima migración» después de que una M4 pasó sin cerrarlo es la promesa que §12.2 califica de no contable. El lugar es una fila `DD-8` de `Mesa-2026-09-13.md` §8 (registro vivo de esta corrida; `Mesa-2026-08-27.md` es registro fechado y no se reescribe), con campo 2 verdadero: «el plan 1.3 aprobado no incluye las 116 y §4.1 impide escribir fuera de él».
4. **No tratar las 116 cabeceras en esta corrida por cuenta propia.** El plan que el PO aprobó (actuación 011, punto A) las excluye expresamente (:51). Sí cabe **ofrecérselo en el lote de la detención de M5** (`Master-Prompt-Migracion.md` §9 «Se presenta el resultado de la verificación […] antes de escribirla»), con «si no respondés» = punto 3: quitar el número de 116 celdas es la corrección de fondo que `Root-Rules.md` §10 R1 pide y que `Mesa-2026-08-27.md:248` ya identificó («no es escribir el número vigente: es dejar de escribirlo»), no cambia contenido declarado, y las 11 de los documentos ya abiertos hoy costarían un minor más por documento. Si el PO aprueba, entra como fila nueva del plan (1.4) con su propuesta; si no, rige la deuda reasignada.
5. **Antes de M5**, no después: `Master-Prompt-Migracion.md` §9 paso 1 exige «ninguna fila del plan sin resolver»; con PM-11 en su redacción actual el paso se contestaría en falso y la procedencia a 13.16 sería una afirmación sobre un plan que dice algo que no pasó.

Nivel del hallazgo: **P1**, con el fundamento literal de la tabla de §12.2. No P0: no hay contenido inventado ni procedencia falsa, y la fila está declarada —mal, pero declarada—. Que el plan 1.3 con ese tratamiento haya sido aprobado por el PO no lo salva: el PO aprobó una premisa («no escribe»), no una conclusión desprendida de ella, y la premisa la cambió el propio orquestador al aplicar.

### 6.2 Lo que M5 necesita (punto 4 del encargo)

**Filas del plan.** Con `M6-19` corregida, ninguna queda sin resolver: PM-01 a PM-04, PM-06 y PM-08 escritas; PM-05, PM-07 y PM-12 «no tocar» con evidencia; PM-09 con la ocurrencia 9 en `DD-6` (declarada); PM-10 es M5 mismo. `M6-22` (8 filas sin clasificar) es corrección del expediente y del plan, no del corpus, y se cierra en la misma pasada.

**Secciones pendientes sin respuesta.** Batería vacía (plan :41). Los puntos C, D y E del lote quedaron sin contestar y rigen sus defaults (`actuaciones/011…md:29-30`); ninguno escribe contenido: C es `DD-5`, D y E son clasificación. `Master-Prompt.md` §8.1 admite el «si no respondés» declarado. Sin obstáculo.

**Documentos «revisar» sin tocar.** Ninguno: los 24 de las propuestas están escritos (E-022 (4)).

**Lo que M5 debe reescribir en §1.1 del manifiesto**, porque quedó contradictorio o va a quedarlo al declarar 13.16:
- `:30` «**Actualizada el 2026-08-27 por la salida `C` del orquestador de reanudación, y NO por una migración.**» y `:37-55` (la narrativa del salto 13.3 → 13.7 y la tabla de ocho artefactos movidos): dejan de describir la procedencia vigente; el propio §1.1 `:64-66` fija el criterio para moverlo al control de cambios.
- `:107-111` la constancia «3.0 / 3.1» (`M6-20` b) y `:3` (`M6-20` a).
- `:22` «**procedencia actualizada a 13.7 sin migrar** el 2026-08-27» (fila Estado) y `:95` «**`Migracion-Rules` 2.9 no aplica hoy**: no hay migración en curso, y este árbol atravesó **siete**» → ocho, con esta.
- Tabla `:76-99` completa con las versiones de `E-002` (`…/evidencia/E-002-versiones-13.7-contra-13.16.txt`: `Master-Prompt` 8.19, `Master-Prompt-Migracion` 2.10, `Root-Rules` 8.7, `Intake-Rules` 4.3, `Migracion-Rules` 3.20, `Mesa-Rules` 1.3, plantillas 3.6 y 6.1, etc.), **no** de lo que hoy muestre el framework: `IA.SDD` HEAD `650053e` no toca `SDD/Devs/` respecto de `8c55a1e` (`git diff --stat 8c55a1e HEAD -- SDD/Devs` vacío) y el `CHANGELOG.md` sigue encabezado por `## [13.16]`, así que hoy coinciden; la fuente que se cita es E-002.

**¿Puede M5 escribir la procedencia sin caer en P0?** **Sí**, con dos condiciones: (1) `M6-19` corregido antes (plan 1.4, mesa 1.3 `DD-8`, informe 1.1), porque §9 paso 1 se contesta contra el plan; (2) la detención de §9 presentada al PO con «qué se va a hacer con la procedencia» y el lote que incluya la opción de las 116 cabeceras. Las deudas `DD-1` a `DD-8` no impiden M5: `Migracion-Rules.md` §4.6 sólo condiciona la procedencia a que **la cadena** (intake → manifiesto → docs del plan) esté migrada, y lo está.

## 7. Proporción detectable por guion

**6 de 13** (`M6-19`, `M6-20`(a), `M6-22`, `M6-24`, `M6-26`, `M6-30`) = **46 %**. Ronda 2 del informe 1.0: 5 hallazgos (`M6-14` a `M6-18`), de los cuales 3 por guion (60 %). La baja es esperable: el cierre sobre lo escrito deja menos forma y más afirmaciones que sólo se leen.

## 8. Lo que no miré

- No repetí la reproducción de E-022 ni los `git apply -R`; tomé sus resultados (1) a (4) y (6) a (9) como ciertos.
- Los 95 ciclos de origen derivados: verifiqué forma y recuento de los 118 y **una** fila contra el commit (`818997f`), más los cuatro ADR y `aa3abd3`; no recorrí los 32 de E-020 elegidos por linaje.
- El contenido de los 551 documentos «no tocar» (PM-12), incluidas sus cabeceras con versión citada más allá del recuento.
- `Handoff-Checkout.md`, `11-Documentacion/README.md` y `Norma-De-Nomenclatura.md`, salvo las ocurrencias de «activo de construcción».
- La corrección técnica de las dos conductas del build (E-012 / `MSB3073`): no corrí `dotnet`.
- El estado real de la fase `i` fuera del árbol (`M6-31` queda no concluyente a propósito).
- La procedencia del framework más allá de `git diff 8c55a1e HEAD -- SDD/Devs`.
