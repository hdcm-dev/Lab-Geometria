# Informe de auditoría B-02-03-GeometriaFactory-Contracts-r1 — Fase B, categorías 02 y 03

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | B-02-03-GeometriaFactory-Contracts-r1.md |
| Versión | 1.0 |
| Fase auditada | Fase B |
| Proyecto de código | GeometriaFactory-Contracts (`GeometriaFactory.Contracts`, `tipo_proyecto_codigo` = `library`) |
| Ámbito | `SDD/Docs/Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/` |
| Alcance | 14 artefactos: 9 de la categoría 02 (índice, README, glosario y 6 CU) y 5 de la categoría 03 (README, glosario y 3 documentos DX). Categoría 04 omitida por gating (`usa_llm` == false): su ausencia no es hallazgo |
| Reglas aplicadas | `Rules-Especificacion-Funcional.md` 4.0 §2, §3, §4 y §6; `Rules-UX-UI-DX.md` 4.0 §1.2, §2, §3, §4 y §6; `Vocabulario-Rules.md` §4, §9 y §10; invariantes D1 a D9 del `README.md` del framework |
| Insumos upstream verificados | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.2 (§13, §14, §15, §16, §16.1, §17.4, §17.5, §20), `PRODUCT-MANIFEST-Fabrica-De-Geometria.md`, `00-Contexto/` y `01-Necesidades-Negocio/` completos |
| Referencia de decisiones de vocabulario | `Audit/A-00-01-r1.md` y `Audit/A-00-01-r2.md` (no modificados) |
| Auditor | Arquitecto de Soluciones + QA Senior, independiente de la generación |
| Fecha | 2026-08-08 |
| Ronda | r1 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Matriz D1-D9 por documento](#2-matriz-d1-d9-por-documento)
- [3. Matriz de estructura obligatoria por documento](#3-matriz-de-estructura-obligatoria-por-documento)
  - [3.1 Categoría 02 contra `Rules-Especificacion-Funcional.md` §4](#31-categoría-02-contra-rules-especificacion-funcionalmd-4)
  - [3.2 Categoría 03 contra `Rules-UX-UI-DX.md` §4](#32-categoría-03-contra-rules-ux-ui-dxmd-4)
  - [3.3 Artefactos correspondientes y omitidos, contra §2.1 y §2.2](#33-artefactos-correspondientes-y-omitidos-contra-21-y-22)
  - [3.4 Criterios de aceptación de §6, ítem por ítem](#34-criterios-de-aceptación-de-6-ítem-por-ítem)
- [4. Coherencia cross-doc y trazabilidad](#4-coherencia-cross-doc-y-trazabilidad)
  - [4.1 Los dos puntos que el orquestador pidió verificar](#41-los-dos-puntos-que-el-orquestador-pidió-verificar)
  - [4.2 Coherencia 03 sobre 02](#42-coherencia-03-sobre-02)
  - [4.3 Fidelidad al upstream](#43-fidelidad-al-upstream)
- [5. Gobierno del glosario](#5-gobierno-del-glosario)
  - [5.1 Los cuatro criterios de `Vocabulario-Rules.md` §10](#51-los-cuatro-criterios-de-vocabulario-rulesmd-10)
  - [5.2 Las tres decisiones cerradas en la Fase A](#52-las-tres-decisiones-cerradas-en-la-fase-a)
  - [5.3 Polisemias evaluadas y descartadas](#53-polisemias-evaluadas-y-descartadas)
- [6. Hallazgos](#6-hallazgos)
- [7. Veredicto y condiciones para promover](#7-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Se auditaron los catorce artefactos de la Fase B de `GeometriaFactory-Contracts` ítem por ítem contra los diecinueve criterios de `Rules-Especificacion-Funcional.md` §6, los dieciocho de `Rules-UX-UI-DX.md` §6, las nueve invariantes globales y los cuatro criterios de gobierno de vocabulario. **Cero P0**: no falta ningún artefacto obligatorio, ninguna cabecera, ninguna sección obligatoria ni ninguna tabla de contenido; las veintiocho declaraciones de trazabilidad de cabecera —upstream y downstream de los catorce artefactos— citan secciones concretas; los catorce archivos cumplen D2, D3, D4 y D5; y no hay stacks del dominio fuente ni emojis.

Total de hallazgos: **14** — 0 P0, 3 P1, 4 P2, 7 P3. Los tres P1 son de tres frentes distintos: una forma léxica prohibida por `Vocabulario-Rules.md` §4 R2 («solución» a secas, tres ocurrencias en 03), una contradicción interna sobre la superficie del tipo de respuesta de sesión en CU-01, y una correspondencia con la previsión de 01 que el índice maestro promete en §3.2 y no entrega en §4.1.

**Veredicto: APROBADO CON OBSERVACIONES.** El cuerpo es de calidad alta y la disciplina de frontera es notable: los seis casos de uso describen contrato y no pantalla, las omisiones están declaradas con motivo una por una, y el corpus resiste el barrido de «proyecto» a secas sin una sola ocurrencia. Lo que falla es de capa léxica y de ajuste entre afirmación y respaldo, y se corrige sin tocar estructura.

---

## 2. Matriz D1-D9 por documento

Leyenda: `C` cumple, `C*` cumple con salvedad registrada, `X` incumple, `n/a` no aplica.

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `02/Especificacion-Funcional.md` | C | C | C | C | C | C* | C | C | n/a |
| `02/README.md` | C | C | C | C | C | C | C | C | n/a |
| `02/Glosario-Funcional.md` | C | C | C | C | C | C | C | n/a | n/a |
| `02/Casos-De-Uso/CU-01-…` | C | C | C | C | C | C | C | C | n/a |
| `02/Casos-De-Uso/CU-02-…` | C | C | C | C | C | C | C | C | n/a |
| `02/Casos-De-Uso/CU-03-…` | C | C | C | C | C | C | C | C | n/a |
| `02/Casos-De-Uso/CU-04-…` | C | C | C | C | C | C | C | C | n/a |
| `02/Casos-De-Uso/CU-05-…` | C | C | C | C | C | C | C | C | n/a |
| `02/Casos-De-Uso/CU-06-…` | C | C | C | C | C | C | C | C | n/a |
| `03/README.md` | C | C | C | C | C | C | C | C | n/a |
| `03/DX-Developer-Experience.md` | C* | C | C | C | C | C | C* | C | n/a |
| `03/Guia-Onboarding-Developer.md` | C* | C | C | C | C | C | C* | C | n/a |
| `03/DX-Error-Messages.md` | C | C | C | C | C | C | C | C | n/a |
| `03/Glosario-UX.md` | C | C | C | C | C | C* | C | n/a | n/a |

Verificaciones que sostienen la matriz:

- **D1.** Rioplatense neutro técnico con tildes y eñes en los catorce cuerpos; los catorce filenames son ASCII sin acentos. Barrido de emojis y pictogramas sobre el rango Unicode completo: **cero ocurrencias**. Sin marketing y sin negrita decorativa. Las dos marcas `C*` son por H-01: `DX-Developer-Experience.md` §3.1 y §3.2 y `Guia-Onboarding-Developer.md` §1.2 usan «solución» a secas designando el agrupador de construcción, forma que `Vocabulario-Rules.md` §4 R2 prohíbe.
- **D2.** Los catorce archivos son UTF-8 sin BOM. Barrido de `CR`: **cero ocurrencias**, fin de línea LF en los catorce. Todas las fechas en `YYYY-MM-DD` (`2026-08-08` en las catorce cabeceras y en las catorce filas de control de cambios).
- **D3.** Título-Con-Guiones estricto en los catorce archivos, en `Casos-De-Uso/` y en las dos carpetas de categoría. Identificadores con prefijo y dos dígitos: `CU-01` a `CU-06`, `US-01` a `US-16`, `RT-01` a `RT-07`, `DXC-01` a `DXC-09`, `DXT-01` a `DXT-12`. Numeración contigua sin huecos en las cinco series.
- **D4.** Ningún archivo vivo lleva sufijo de versión; los catorce declaran `Versión: 1.0` en su cabecera. Verificado contra `Rules-Especificacion-Funcional.md` §3.1 y `Rules-UX-UI-DX.md` §3.1.
- **D5.** Un solo archivo por nombre lógico; no hay `_legacy/` porque es la emisión inicial, y los dos README lo declaran explícitamente. Los catorce llevan sección de control de cambios con la entrada 1.0 fechada.
- **D6.** Los catorce declaran upstream y downstream en cabecera con **secciones concretas**: ninguna cabecera dice «PRODUCT-INTAKE» a secas —las catorce citan §17.4 con sus puntos `P.x`, o §13, §14, §15, §16, §16.1, §20 con su escenario—. Todos los enlaces relativos se abrieron uno por uno y resuelven, incluidos los seis `../02-Especificacion-Funcional/…` de 03, y las anclas de las catorce tablas de contenido resuelven contra sus títulos. Dos salvedades, ambas de contenido y no de sintaxis: `Especificacion-Funcional.md` §3.2 remite a §4.1 por una correspondencia que §4.1 no contiene (H-03), y `Glosario-UX.md` §2 cita `Rules-UX-UI-DX.md` §1.7, sección que no existe en esa regla (H-06).
- **D7.** Ninguna referencia al dominio fuente del bootstrap del framework. Los seis casos de uso hablan de «pieza pública», «pieza de datos», «frontera de servicio» y «tipo de transferencia», y **no** nombran ni el lenguaje, ni la plataforma, ni el formato de serialización, ni el esquema de portador que el intake §17.4.P.1 y P.3 sí nombran: el recorte es deliberado y correcto. Las dos marcas `C*` de 03 son por los bloques de comandos del quick-start, que nombran `scripts/build.sh`, `scripts/test.sh`, `grep` y `.devcontainer/devcontainer.json`. **No se levanta hallazgo**: `Rules-UX-UI-DX.md` §6 exige «quick-start verificable con snippet ejecutable y reproducible» y §4.4 marca como anti-patrón el snippet que no corre, de modo que la concreción es la que la propia regla pide; además los tres identificadores vienen del intake §15 y §16 y no se originan acá.
- **D8.** `library` en los catorce artefactos, coherente con `PRODUCT-INTAKE` §17.4 y con el manifiesto. Se aplicaron las filas `library` de `Rules-Especificacion-Funcional.md` §1.2 y §2.2 (mínimo 5 CU, RN no obligatorias, sin modelo conceptual) y de `Rules-UX-UI-DX.md` §1.2 y §2.2 (variante DX, mínimo 0 wireframes, tres documentos DX obligatorios). Ningún valor fuera del conjunto cerrado.
- **D9.** Alcance acotado a afirmaciones sobre el estado del sistema. **Esta fase produce especificación de contrato y no hay sistema construido**, de modo que casi ninguna afirmación cae bajo D9 y no se levantó ningún hallazgo por una decisión de diseño sin cita. Las únicas afirmaciones de estado son las de 03 sobre el repositorio —que `scripts/` existe y que la construcción termina sin advertencias—, y las dos están correctamente condicionadas: `DX-Developer-Experience.md` §3.1 declara «si la etapa `a` no está cerrada, este quick-start no aplica todavía» y `Guia-Onboarding-Developer.md` §1.2 lo pone como prerrequisito. No hay afirmación de estado sin condición ni sin fuente.

---

## 3. Matriz de estructura obligatoria por documento

### 3.1 Categoría 02 contra `Rules-Especificacion-Funcional.md` §4

| Documento | Cabecera §4.1 | TdC §4.1 | Secciones obligatorias | Resultado |
| --- | --- | --- | --- | --- |
| `Especificacion-Funcional.md` | Completa: Proyecto de código, Documento, Versión, Estado, Fecha, Autor, upstream con 4 fuentes y sus secciones, downstream con 3 categorías | Sí, 9 entradas con anclas de primer y segundo nivel | Índice maestro con catálogo (§3), matriz NB→CU→RN→US (§4), criterio de recorte (§3.1), restricciones transversales (§6), omisiones (§8) y control de cambios (§9) | Cumple |
| `README.md` | Completa en contenido pero en **formato de tabla**, no el bloque de metadatos de §4.1 que usan los otros trece artefactos | Sí, 6 entradas | §3.4: enumera los 4 documentos emitidos con propósito y estado, los 6 CU, orden de lectura, omisiones con motivo y notas de uso | Cumple con salvedad (H-10) |
| `Glosario-Funcional.md` | Completa, con trazabilidad upstream al glosario del dominio de 00 como exige §4.2.4 punto 1 | Sí, 5 entradas | Las cinco de §4.2.4: alcance (§1), tabla de términos con 17 filas y las 4 columnas (§2), términos con más de un referente con 2 subsecciones y su evidencia de colisión (§3), términos referenciados y no redefinidos con 19 filas (§4), control de cambios (§5). Tabla no vacía | Cumple |
| `CU-01` a `CU-06` | Completas en los seis, con upstream a NB, a 00 y al intake con secciones concretas, y downstream a 05, 06 y 08 | Sí en los seis, 12 entradas cada uno | Las **once** de §4.2 presentes y en orden en los seis: Propósito, Actores (tabla actor/tipo/rol), Precondiciones, Flujo principal numerado, Flujos alternativos con disparador y punto de retorno, Excepciones con código/causa/respuesta, Postcondiciones de éxito y de fallo, Criterios Given/When/Then, Trazabilidad con las cinco dimensiones de §4.4, Notas y supuestos, Control de cambios. Suman §12 de compatibilidad de versión pública | Cumplen; ver H-09 por la numeración de la sección opcional |

Tablas tipo de §4.4 verificadas una a una: criterios de aceptación con las columnas ID/Given/When/Then en los seis CU, y tabla de trazabilidad con las cinco dimensiones —necesidad de negocio, reglas de negocio aplicables, historias de usuario, componentes esperados, tests previstos— en los seis. **Ninguna falta.**

Criterios Given/When/Then: mínimo tres por CU. Emitidos **cinco** en CU-01, CU-02, CU-03, CU-04 y CU-06 y **seis** en CU-05, todos con valores concretos y verificables por conteo o por identidad textual (`0 campos`, `3 elementos`, `índice de figura 1`, `campo Tipo`, `valor declarado 36.00`, `alumna@ejemplo.edu`). Ningún criterio narrativo.

Escenarios de error: los seis CU tienen al menos tres filas en §6, con recuperación, handoff o terminación controlada declarada por fila. Ningún CU con sólo flujo feliz. Ningún CU con más de un actor primario: los seis declaran «Código de la pieza pública» como primario único.

### 3.2 Categoría 03 contra `Rules-UX-UI-DX.md` §4

| Documento | Cabecera §4.1 con `Variante` | TdC §4.1 | Secciones obligatorias | Resultado |
| --- | --- | --- | --- | --- |
| `DX-Developer-Experience.md` | Completa, **Variante: DX** | Sí, 9 entradas con segundo nivel | Las **nueve** de §4.2.3 y en orden: §1 rol de intervención (mantenedor/integrador con nivel de experiencia), §2 onboarding 5/30/60 con objetivo verificable por tramo, §3 quick-start ejecutable, §4 Diátaxis con los cuatro modos y su ubicación, §5 mensajes de error con los tres principios, §6 métricas DX, §7 feedback loop, §8 trazabilidad con la tabla de §4.3 completa incluidas las filas de maqueta en N/A, §9 control de cambios | Cumple |
| `Guia-Onboarding-Developer.md` | Completa, **Variante: DX** | Sí, 6 entradas | Las **seis** de §4.2.4: §1 audiencia y prerrequisitos, §2 instalación o acceso con pasos verificables, §3 primer ejemplo ejecutable, §4 diagnóstico de problemas de la primera hora (5 filas), §5 próximos pasos con enlace explícito a los cuatro modos, §6 control de cambios | Cumple |
| `DX-Error-Messages.md` | Completa, **Variante: DX** | Sí, 6 entradas con segundo nivel | Las **seis** de §4.2.5: §1 principios de redacción, §2 taxonomía con las cinco categorías canónicas —entrada inválida, recurso ausente, conflicto de estado, error transitorio, error interno—, §3 catálogo con código/categoría/mensaje/causa/acción, §4 tono y voz, §5 localización, §6 control de cambios | Cumple |
| `Glosario-UX.md` | Completa, **Variante: DX** | Sí, 5 entradas | Alcance (§1), tabla de 16 términos acuñados (§2), términos con más de un referente con evidencia de colisión (§3), términos referenciados y no redefinidos con 21 filas (§4), control de cambios (§5). Tabla no vacía | Cumple |
| `README.md` | Completa, **Variante: DX** | Sí, 7 entradas | §3.4: los 5 artefactos con propósito y estado, la variante aplicada con su fundamento, orden de lectura, las 8 omisiones con su motivo y los 6 criterios de §6 declarados no aplicables | Cumple |

Tabla de trazabilidad de §4.3: `DX-Developer-Experience.md` §8 la reproduce completa, con las trece dimensiones incluidas «Catálogo de diseño aplicado» (N/A por variante DX, como la propia fila admite) y las tres filas condicionadas a `requiere_maqueta`, en N/A con motivo. Los otros cuatro artefactos declaran trazabilidad en cabecera, que es lo que §6 exige de ellos.

### 3.3 Artefactos correspondientes y omitidos, contra §2.1 y §2.2

| Artefacto | Corresponde | Estado | Verificación |
| --- | --- | --- | --- |
| `Especificacion-Funcional.md` | Obligatorio, ocho tipos D8 | Emitido | Con índice maestro y matriz NB→CU→RN→US |
| `Casos-De-Uso/CU-XX` | Mínimo 5 para `library` | 6 emitidos | Sobre el piso, sin huecos de numeración |
| `Glosario-Funcional.md` | Obligatorio, ocho tipos D8 | Emitido | Tabla no vacía, 17 términos |
| `README.md` de 02 | Recomendado | Emitido | — |
| `Definicion-<Concepto-Central>.md` | Condicional | Omitido, declarado | Omisión declarada en `Especificacion-Funcional.md` §8 y `README.md` §4, pero con la celda de la regla mal citada (H-05) |
| `Reglas-De-Negocio/RN-XX` | No obligatorias para `library` | Omitidas, declaradas | `Especificacion-Funcional.md` §5 completo y `README.md` §4. Correcto |
| `Modelo-Datos/` y `RC-XX` | Omitir para `library` puro sin estado | Omitidos, declarados | `README.md` §4 con doble motivo: tipo D8 y `tiene_persistencia` == false |
| `DX-Developer-Experience.md` | Obligatorio para `library` | Emitido | Nueve secciones |
| `Guia-Onboarding-Developer.md` | Obligatorio para `library` | Emitido | Seis secciones |
| `DX-Error-Messages.md` | Obligatorio para `library` | Emitido | Seis secciones |
| `Glosario-UX.md` | Obligatorio, ocho tipos D8 | Emitido | Tabla no vacía, 16 términos |
| `README.md` de 03 | Recomendado | Emitido | Declara variante DX con fundamento |
| `Experiencia-De-Uso.md` | Omitir para `library` | Omitido, declarado | `README.md` §4 y §5, con `tiene_ui_final` == false |
| `wireframes-<superficie>.md` | Mínimo **0** para `library` | Ninguno, declarado | `README.md` §1 y §4 |
| `representacion-<concepto>.md` | Condicional | Omitido, declarado | `README.md` §4 |
| `DX-Portal-Developers.md` | Recomendado para «library con portal hospedado» | Omitido, declarado | `README.md` §4, con `tiene_portal_developers` == false |
| `DX-Operability.md` | Obligatorio para `worker-service` | Omitido, declarado | `README.md` §4 |
| `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md`, `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` == true | Omitidos, declarados | `README.md` §4, tres filas con `requiere_maqueta` == false |

**Las once omisiones están declaradas con motivo y con la regla que las admite.** Es el punto donde este corpus está por encima de lo exigido: el `README.md` de 03 agrega §5, que declara además los seis criterios de aceptación de `Rules-UX-UI-DX.md` §6 que pertenecen a la variante UX/UI como no aplicables con su motivo, en lugar de darlos por cumplidos. Sólo una de las once omisiones cita mal la celda que la habilita (H-05).

### 3.4 Criterios de aceptación de §6, ítem por ítem

`Rules-Especificacion-Funcional.md` §6, diecinueve ítems: cumplen dieciocho. El que no cumple es «Todo término del dominio que aparece en más de un artefacto de 02 está declarado en el glosario» (H-04). Detalle de los que exigen conteo: mínimo de CU para `library` **6 ≥ 5**; once secciones por CU **6 de 6**; tres criterios GWT mínimos **5, 5, 5, 5, 6, 5**; glosario con las cinco secciones de §4.2.4 y tabla no vacía **sí**; ningún archivo con sufijo de versión **14 de 14**; ningún slug con mayúsculas fuera de patrón, espacios o acentos **14 de 14**; un solo archivo por nombre lógico **sí**; tabla de contenido en todo documento de más de tres secciones **14 de 14**.

`Rules-UX-UI-DX.md` §6, dieciocho ítems: seis declarados no aplicables por variante y por `requiere_maqueta` == false —y declarados como tales por el propio `README.md` §5, lo que esta auditoría confirma ítem por ítem—, once cumplen, uno cumple con salvedad. Detalle: variante declarada en cabecera **5 de 5**; `DX-Developer-Experience.md` con las nueve secciones **sí**; quick-start verificable con snippet ejecutable **presente en `DX-Developer-Experience.md` §3.2, en `Guia-Onboarding-Developer.md` §2.2 y §3.2 y en `DX-Error-Messages.md` §3**, con el bloque de comandos reproducido sin variantes en los tres, que es lo que la regla pide; trazabilidad upstream y downstream por artefacto **5 de 5**; glosario existente y no vacío **sí**; no duplicación con `Glosario-Funcional.md` **sí, verificada término por término**; criterio negativo de la polisemia **respetado** (ver §5.3). La salvedad es la del glosario de 03, que hereda de 02 la falta de `estado degradado` (H-04).

---

## 4. Coherencia cross-doc y trazabilidad

### 4.1 Los dos puntos que el orquestador pidió verificar

**Punto 1 — numeración local de los CU.** Verificado, con un defecto.

- La decisión de numerar local **está declarada** y fundada: `Especificacion-Funcional.md` §3.2 dice «Los identificadores `CU-XX` y `US-XX` de esta sección son **locales a `GeometriaFactory-Contracts`**» y lo apoya en el nivel de aplicación de la categoría, que la cabecera de la regla fija en proyecto de código. `README.md` §5 lo repite en la nota de numeración. La previsión de 01 **no queda contradicha en silencio**: §3.2 la nombra —«`Necesidades-Negocio.md` §5.3 prevé veintidós casos de uso `CU-01` a `CU-22` a nivel producto»— y declara que se reparte entre los siete espacios independientes.
- Cobertura bidireccional NB↔CU **completa**: las ocho NB tienen al menos un CU en la matriz de §4 y los seis CU declaran al menos una NB en su §9. Contrastado contra los ocho archivos `NB-XX` de 01: ninguna NB queda sin fila y ningún CU queda huérfano. La matriz US es internamente consistente: las dieciséis `US-01` a `US-16` de §4 son exactamente la unión de las que declaran las seis tablas de trazabilidad, sin ninguna que aparezca en una y falte en la otra.
- **El defecto**: §3.2 cierra con «La correspondencia con la previsión de 01 se lee en §4.1», y §4.1 no la contiene. §4.1 es una tabla de cobertura inversa NB → grado en que este proyecto de código sostiene la necesidad, útil y bien hecha, pero que no dice qué `CU-XX` de la previsión de nivel producto absorbe cada `CU-XX` local. Ningún identificador queda huérfano dentro de la fase, pero la correspondencia prometida no está en ningún artefacto (H-03).

**Punto 2 — columna RN vacía.** Verificado, correcto, con una imprecisión asociada.

- La vacuidad **está declarada con motivo** y no omitida: `Especificacion-Funcional.md` §5 es una sección entera dedicada a eso, y no argumenta que el producto carezca de reglas —enumera cinco que sí existen— sino que argumenta **dónde viven**. El fundamento normativo es correcto: §2.1 de la regla omite las `RN-XX` para «proyectos de código triviales sin estado ni invariantes» y §2.2 no las hace obligatorias para `library`. `README.md` §4 agrega la decisión de **mantener la columna en la matriz** con el motivo declarado en lugar de suprimirla, que es la resolución correcta.
- Las seis tablas de trazabilidad dicen «Ninguna en este proyecto de código» y remiten a §5, con la invariante concreta y su proyecto de código destino en cada caso. Nada inventado: no aparece ninguna `RN-XX` fabricada, que es lo que §5.3 de la regla previene.
- **La imprecisión**: §5 afirma que «los casos de uso de esta sección las **refieren** en su tabla de trazabilidad y no las redactan», y ninguna de las seis tablas nombra un identificador `RN-XX`. Al menos uno es nombrable hoy: el intake §17.4.P.5 ancla el tipo de respuesta de error a **RN-09**, y `RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md` existe en `GeometriaFactory-Domain` (H-07).

### 4.2 Coherencia 03 sobre 02

Se contrastó cada afirmación de 03 sobre el contrato con lo que 02 especificó. **Se abrió una por una cada referencia de 03 a un caso de uso, a un criterio de aceptación, a un flujo alternativo o a una restricción transversal: todas resuelven y todas coinciden con lo que 02 dice, salvo una imprecisión de una fila de control de cambios.**

- **Conjunto cerrado de códigos.** `DX-Error-Messages.md` §2.2 afirma «Las cinco categorías cubren los **doce** códigos del conjunto cerrado, sin huecos y sin superposición» y §3.2 emite doce entradas `DXT-01` a `DXT-12`, una por código. El barrido de los seis CU devuelve exactamente doce códigos distintos —`CAMPO_REQUERIDO_AUSENTE`, `CREDENCIAL_INVALIDA`, `CUENTA_NO_HABILITADA`, `CORREO_YA_REGISTRADO`, `CONFIRMACION_NO_COINCIDE`, `ADMINISTRADOR_YA_CONFIGURADO`, `TRABAJO_NO_ENCONTRADO`, `ESTADO_NO_PERMITE_ELIMINAR`, `TEXTO_NO_INTERPRETABLE`, `ALUMNO_NO_ENCONTRADO`, `SERVICIO_NO_DISPONIBLE`, `ERROR_NO_CLASIFICADO`—. **Coincidencia exacta, sin sobrantes ni faltantes.** `CONTRATO_LISTADO_VACIO`, que CU-04 §6 declara «No es error», se cataloga aparte como `DXT-N1` en §3.3 y no se cuenta entre los doce: la resolución es la correcta.
- **Superficie del tipo de error.** `Guia-Onboarding-Developer.md` §3.1 y `DX-Developer-Experience.md` §2 afirman «exactamente cuatro —código, texto, detalles y momento— y **cero** de la segunda clase», idéntico a CU-06 CA-01 y a CU-06 §4 paso 2. Coincide.
- **Restricción estructural del listado.** `DXC-06` y `Guia` §3.1 y §3.3 afirman «0 campos de texto original y 0 de componente de pieza», idéntico a CU-04 CA-01 y a `RT-04`. Coincide.
- **Detalle único portador del texto original.** `Guia` §3.1 lo afirma citando CU-05 §10 y CU-04 CA-01; las dos citas resuelven y dicen eso.
- **Clasificación de compatibilidad.** Los tres cambios de control de `Guia` §3.3 se contrastaron contra los §12 correspondientes: el campo opcional en la respuesta de sesión es compatible en CU-01 §12; la situación de cuenta agregada es «incompatible de hecho, aunque compile» en CU-02 §12; el texto original en el elemento de listado «compila sin error y aun así se rechaza» en CU-04 §12. **Los tres coinciden literalmente.**
- **Las nueve entradas `DXC`.** Cada «Deriva de» se abrió y resuelve: `DXC-01`→`RT-05` e intake P.8; `DXC-03`→los cinco §12 con conjunto cerrado (papel en CU-01, situación en CU-02, estado en CU-03, severidad en CU-05, código en CU-06), y los cinco los declaran; `DXC-05`→los cuatro criterios que `RT-01` nombra como punto de verificación en `Especificacion-Funcional.md` §6, **los mismos cuatro**; `DXC-07`→CU-05 CA-06, que efectivamente prohíbe la variante enriquecida.
- **Las doce entradas `DXT`.** Cada una cita el CU, el flujo alternativo y el criterio de aceptación de origen; todas las citas resuelven y ninguna atribuye a un CU algo que el CU no dice. `DXT-02` reproduce la decisión de CU-01 CA-03 de no revelar cuál de los dos campos falló; `DXT-07` reproduce la de CU-03 CA-04 de no distinguir ajeno de inexistente; `DXT-11` reproduce la de CU-06 CA-04, con «0 detalles» y sin dirección.
- **La discrepancia.** `DX-Developer-Experience.md` §9 afirma que las nueve entradas `DXC` derivan «de `RT-01` a `RT-06`»; `DXC-09` no deriva de ninguna `RT` sino sólo del intake §17.4.P.8, y `RT-02` no es origen de ninguna `DXC`. Es imprecisión de una fila de control de cambios, no del catálogo, y no se levanta como hallazgo separado.

Enlaces: los seis enlaces relativos de 03 hacia `../02-Especificacion-Funcional/` resuelven a rutas existentes. **Ningún enlace roto en las dos categorías.**

### 4.3 Fidelidad al upstream

- **Intake §17.4 P.1 a P.12.** Los doce puntos están recogidos, sin decisión originada por la fase. P.2 (tipos planos sin comportamiento y descarte de generación de clientes) → `Especificacion-Funcional.md` §2 y §5; P.3 (política de cambios incompatibles y despliegue conjunto) → `RT-06` y los seis §12; P.4 (no aplica) → omisión del modelo conceptual; P.5 (regla de exposición y RA-03) → `RT-01`, `RT-02` y los cuatro criterios de inspección; P.6 (sin pruebas propias) → `RT-07`; P.8 (gate bloqueante) → `RT-05` y `DXC-01`/`DXC-09`; P.10 (NFR estructural) → `RT-04` y CU-04 CA-01; P.11 decisión 2 (texto crudo como cadena) → `RT-03` y CU-03 entero.
- **Rótulos de asunción.** El intake rotula `[ASUNCIÓN]` dos valores de este bloque: el gate del 100 % de tipos ejercitados (P.6) y el NFR estructural del listado (P.10). CU-04 §10 declara el rótulo del segundo —«se rotula ahí como asunción derivada; está completo y se usa como valor vigente»—, que es el tratamiento correcto. El primero se presenta como gate vigente en tres lugares sin el rótulo (H-11).
- **Frontera con los otros proyectos de código.** Correctamente sostenida en los catorce artefactos: la forma de los puntos de acceso se deriva a `GeometriaFactory-Api` citando §17.5 P.3 y P.5, las invariantes a `GeometriaFactory-Domain`, la interpretación y la derivación de clave a `GeometriaFactory-Infrastructure`, el dibujo a `GeometriaFactory-Visor` y la presentación a `GeometriaFactory-Web`. **Ningún caso de uso invade 03 con detalle de interfaz** —el anti-patrón principal de §4.5— ni invade 05 con arquitectura.
- **Escenarios de datos.** CU-03 CA-02 y CA-05, CU-05 CA-01 a CA-04 y `DXT-09` citan E-1, E-2, E-4 y E-5 del intake §20 con sus valores concretos (3 piezas y 2 advertencias en E-1; área declarada 36.00 contra derivada 54.00; figura de tipo desconocido en la posición 1). Los valores coinciden con §20.
- **00 y 01.** Las exclusiones X-1 a X-4 de `Alcance-Producto.md` §5 se citan donde corresponde (CU-02 §10, CU-03 §10) y ninguna capacidad, prioridad o exclusión se origina en la fase.

---

## 5. Gobierno del glosario

### 5.1 Los cuatro criterios de `Vocabulario-Rules.md` §10

**Criterio 1 — sin contradicciones.** Cumple. Se contrastaron los diecisiete términos de `Glosario-Funcional.md` §2 contra los dieciséis de `Glosario-UX.md` §2 y contra las diecisiete entradas de `Vision-Producto.md` §9.1 más las cinco de §9.2: **ninguna definición contradice a otra y ninguna capa redefine lo que otra ya declaró**. La regla de referenciar y no redefinir se cumple en las tres capas: §4 de cada glosario materializa la referencia, con diecinueve entradas en 02 y veintiuna en 03, y `Glosario-UX.md` §1 declara explícitamente las dos fuentes que mandan sobre él. El caso más delicado está bien resuelto: `Glosario-UX.md` §4 declara «Consumidor del contrato … **No se confunde con el rol de intervención developer**, que es humano o agente», que es exactamente la distinción que un lector de una sección suelta podría perder.

**Criterio 2 — completitud.** **No cumple** (H-04). La regla de inclusión de `Rules-Especificacion-Funcional.md` §3.3 —todo término del dominio que aparezca en más de un artefacto de 02— deja fuera dos términos que sí califican: «papel» (cinco artefactos) y «estado degradado» (seis artefactos de 02 y dos de 03). Ninguno está declarado en los dos glosarios de la fase ni en `Vision-Producto.md` §9. El resto de la verificación es positiva: los diecisiete términos declarados en 02 aparecen efectivamente en los artefactos que su columna declara, y los dieciséis de 03 también.

**Criterio 3 — polisemia gobernada, con la sección como contexto de lectura.** Cumple, y por encima del piso. Los tres términos con más de un referente que la cadena tiene están declarados con enumeración de referentes, forma que corresponde a cada uno y **evidencia explícita de colisión**: «contrato» con tres referentes en `Glosario-Funcional.md` §3.1, «pieza» con dos en §3.2, «error» con tres en `Glosario-UX.md` §3.1. La evidencia de colisión de «error» es la mejor de las tres y es verificable: `DXT-09` de `DX-Error-Messages.md` §3.2 es un error transportado cuya causa es un error de validación, y `DXC-04` de §3.1 es un error de construcción. Se abrieron las tres entradas y las tres cumplen el requisito de §9.4 de citar la verificación que las justifica. `Glosario-UX.md` §3.2 hace lo correcto con lo heredado: **remite a la resolución de 02 sin volver a declararla**, que es la conducta que §3.3 de la regla pide.

**Criterio 4 — criterio negativo.** Cumple, y de forma declarada. Los dos glosarios enuncian la prohibición antes de aplicarla: `Glosario-Funcional.md` §3 dice «No se reporta ningún otro caso: los términos cuyos sentidos se distinguen solos quedan fuera, por la prohibición de §9.4», y `Glosario-UX.md` §3 lo repite. **Esta auditoría no encontró ninguna polisemia con contextos disjuntos reportada como defecto ni corregida calificando todas las ocurrencias.** La enumeración de las que esta auditoría evaluó y descartó está en §5.3, para que una ronda posterior no las levante.

### 5.2 Las tres decisiones cerradas en la Fase A

| Decisión de `A-00-01-r2.md` | Estado en esta fase | Evidencia |
| --- | --- | --- |
| «trabajo» **no** es «unidad de entrega» | **Respetada.** Ninguna ocurrencia del término normativo designa el trabajo del alumno | `Glosario-Funcional.md` §4: «Es lo que el alumno entrega en el laboratorio. **No es “unidad de entrega”**: ese término normativo designa a las dos piezas desplegables». `Glosario-UX.md` §4 lo repite. Barrido de «unidad de entrega» en los catorce artefactos: dos ocurrencias, ambas metalingüísticas y en esas dos filas |
| «pieza» va calificada en su referente de artefacto desplegable | **Respetada.** Barrido completo: **cero** formas desnudas con ese referente. Las ocurrencias desnudas designan figuras del trabajo | `Glosario-Funcional.md` §3.2 reproduce la resolución de `Vision-Producto.md` §9.2 y declara «se referencia y se cumple». Las formas usadas son «pieza pública», «pieza de datos» y «piezas desplegables» |
| «observación» es superordinado de «advertencia» y «error de validación» | **Respetada, y aplicada con precisión.** Donde el enunciado habla de discrepancia de valores se usa «advertencia» (CU-04 CA-04, «cantidad de advertencias 2»; CU-05 CA-01, «los dos de severidad de advertencia»); donde habla del conjunto se usa el superordinado | `DX-Error-Messages.md` §4 lo eleva a regla de voz del catálogo: «“Observación” sólo como superordinado de las dos, **nunca como sinónimo de ninguna**» |

### 5.3 Polisemias evaluadas y descartadas

Se evaluaron once términos con más de un referente potencial. Tres están declarados y son correctos; **ocho se descartan y no se reportan**, por el criterio negativo de `Vocabulario-Rules.md` §9.1 y §10. Se enumeran porque descartarlos en silencio deja el trabajo sin evidencia y expone la fase a que una ronda posterior los levante como falso positivo.

| Término | Referentes en la fase | Veredicto | Fundamento |
| --- | --- | --- | --- |
| contrato | Ensamblado / contrato de uso / contrato de verificación `VER-XX` | **Declarado**, correcto | Colisión real en la cadena 02→05→08→10; entrada en `Glosario-Funcional.md` §3.1 con la forma de cada uno |
| pieza | Figura del trabajo / artefacto desplegable | **Declarado**, correcto | Colisión real: los dos referentes conviven en la misma sección de CU-04 y CU-05 |
| error | Error de validación / de construcción / transportado | **Declarado**, correcto | Colisión real y verificable a pocas líneas en `DX-Error-Messages.md` §3 |
| papel | Papel de la persona (alumno, administrador) / papel del componente dentro de la pieza | **Descartado** | El segundo referente aparece **una sola vez** en toda la fase, en CU-05 §4 paso 3, y calificado: «cada componente trae su **papel en la pieza**». Ninguna sección contiene los dos referentes. Contextos disjuntos: calificar todas las ocurrencias sería el anti-patrón de `Rules-Especificacion-Funcional.md` §4.5. Su ausencia del glosario se levanta por **completitud** (H-04), no por polisemia |
| estado | Estado del trabajo / estado degradado / estado de servidor de la pieza pública | **Descartado** | Los tres conviven en la misma sección en CU-03 §6, pero **las tres ocurrencias van calificadas** —«el estado actual del trabajo», «el estado degradado», «el estado de servidor de la propia pieza pública»— y «estado del trabajo» tiene entrada de glosario. No queda ninguna forma desnuda sin resolver, que es lo que §9.2 pide mirar. Se registra además que el corpus **previno** la colisión con la cuenta: `Glosario-Funcional.md` §2 declara «se prefiere “situación” para no colisionar con el estado del trabajo» |
| tipo | Tipo de transferencia / tipo declarado de una figura / tipo D8 | **Descartado** | Los dos primeros conviven en CU-05 §4 y §8, pero cada ocurrencia lleva modificador resolutivo: «el tipo de detalle», «el tipo de error» contra «su tipo declarado», «una figura de tipo desconocido», «campo `Tipo`». La única forma desnuda, CU-05 CA-05 «la superficie pública del tipo», queda resuelta por el Given de su propia fila |
| campo | Campo de un tipo de transferencia / campo señalado del texto del alumno | **Descartado** | La familia calificada está declarada: «campo señalado» tiene entrada propia en `Glosario-Funcional.md` §2. Las ocurrencias desnudas designan siempre el campo del tipo, y donde conviven —CU-03 §6, `DXT-01` contra `DXT-09`— el segundo referente va calificado o acompañado del índice de figura |
| detalle | Detalle del trabajo (CU-05) / detalle de ubicación (CU-06) | **Descartado** | Contextos disjuntos por sección: «detalle del trabajo» vive en CU-05 y en `DXC-06`/`DXC-07` (§3.1 del catálogo), «detalle de ubicación» en CU-06 y en la columna de §3.2. Las dos formas van calificadas en las dos categorías, y la primera tiene entrada de glosario |
| superficie | Superficie pública del contrato / superficie de acceso de la pieza pública | **Descartado** | El segundo referente aparece una vez, en `DXT-02`, y calificado. La primera tiene entrada de glosario en 02 y término derivado en 03 («inspección de superficie pública»). No hay sección con los dos |
| trabajo | Entrega del alumno / esfuerzo de construcción | **Descartado** | `Vision-Producto.md` §9.1 fija que el esfuerzo se nombra «tarea» o «etapa». Barrido: **cero** ocurrencias de «trabajo» sustantivo designando esfuerzo de construcción; las que aparecen son verbales («trabajar contra el ensamblado») y no admiten confusión con el sustantivo del dominio |
| comisión | Grupo de alumnos | **Descartado** | Un solo referente. Aparece en dos artefactos de 02 (CU-04 y el índice) pero viene del intake y de 00 y se resuelve dentro de su oración —«el listado de toda la comisión»—, exactamente el caso que `A-00-01-r1.md` H-06 calificó P3 para «laboratorio» y que acá ni siquiera llega a eso, por ser cita del upstream |

---

## 6. Hallazgos

### P1

**H-01 · P1 · «solución» a secas designando el agrupador de construcción, tres ocurrencias en la categoría 03**

- **Archivo y sección.** `03-UX-UI-DX/DX-Developer-Experience.md` §3.1 y §3.2; `03-UX-UI-DX/Guia-Onboarding-Developer.md` §1.2.
- **Evidencia.** `DX-Developer-Experience.md` §3.1: «Los comandos de construcción del repositorio viven en `scripts/` y existen desde la etapa `a` del plan de entrega, que es **el andamiaje de la solución**». §3.2, comentario del bloque ejecutable: «`# 1. Construir la solución completa.`». `Guia-Onboarding-Developer.md` §1.2, fila de la etapa `a`: «Es **el andamiaje de la solución**: la estructura de proyectos de código y los comandos de `scripts/`». En las tres, el referente es inequívocamente el agrupador de construcción —el artefacto que el comando de construcción toma como entrada única, `Artefacto-Agrupacion` = `GeometriaFactory.sln`—, no el remedio de un problema ni prosa de negocio.
- **Regla violada.** `Vocabulario-Rules.md` §4 **R2**: «**“Solución” a secas no se usa.** El agrupador de construcción se escribe siempre completo: “solución de código”». Y §10, primer bloque, cuarto ítem: «No aparece “solución” a secas designando el agrupador de construcción», criterio declarado verificable «por el auditor de cualquier fase sobre cualquier artefacto».
- **Por qué es P1 y no P0.** Se aplica la calibración de la ronda anterior: `A-00-01-r1.md` clasificó P1 el uso equivalente de otro término normativo de §2 —H-02, «unidad de entrega» con el referente del dominio— y reservó P0 para documento ausente, cabecera ausente, sección obligatoria ausente, trazabilidad rota y vocabulario del dominio fuente del framework. Además, ese mismo informe verificó explícitamente este barrido en la Fase A y lo declaró limpio, de modo que la regresión se introduce acá y no se hereda.
- **Agravante.** El corpus **declara** la regla hermana y la cumple con rigor: `Glosario-Funcional.md` §4 y `Glosario-UX.md` §4 dicen «**La palabra “proyecto” a secas no se usa**», y el barrido confirma cero ocurrencias en catorce artefactos. La disciplina existe; falta extenderla a R2.
- **Recomendación.** Reemplazar por «solución de código» en las tres ocurrencias, o reformular donde la frase venga del intake: «el andamiaje de la solución de código» y «Construir la solución de código completa». Es una corrección léxica de tres líneas que no toca estructura ni conteos. Enumerar y clasificar ocurrencia por ocurrencia según `Vocabulario-Rules.md` §9.5: **queda prohibida la sustitución global de la cadena**.

**H-02 · P1 · CU-01 contradice su propia superficie: el tipo de respuesta de sesión tiene cuatro campos «y ninguno más», y a la vez un quinto que dos criterios exigen**

- **Archivo y sección.** `02-Especificacion-Funcional/Casos-De-Uso/CU-01-Contrato-De-Canje-De-Credenciales-Y-Sesion.md` §4, §5, §8 y §12.
- **Evidencia.** §4 paso 4: «produce el tipo de respuesta de sesión **con cuatro campos**: credencial de sesión, identificador de la persona, correo y papel». §8 CA-01: «La respuesta de sesión trae **exactamente cuatro campos poblados** —credencial de sesión, identificador, correo … y papel `Alumno`— **y ninguno más**». §12: «Quitar o renombrar cualquiera de **los cuatro campos** de la respuesta … es cambio incompatible». Contra eso, §5 FA-02: «La respuesta de sesión llega con **el indicador de contraseña pendiente en verdadero** y sin credencial de sesión utilizable», y §8 CA-05: «La respuesta de sesión trae **el indicador de contraseña pendiente en verdadero**».
- **Por qué es P1.** Este proyecto de código no especifica otra cosa que la superficie de sus tipos: la lista de campos **es** el contenido normativo del artefacto, y es lo que 05 y 06 van a derivar y lo que 08 va a verificar por inspección. Tal como está, CA-01 y CA-05 no pueden satisfacerse a la vez: un implementador que declare cuatro campos falla CA-05 y FA-02, y uno que declare cinco falla CA-01. Es además el único caso de uso donde la contradicción es de conteo, que es la forma en que este corpus escribió sus criterios verificables.
- **Recomendación.** Decidir la forma y propagarla a los cuatro puntos: o el indicador de contraseña pendiente es un quinto campo declarado del tipo de sesión —y entonces §4 paso 4 dice cinco, CA-01 dice «cinco campos y ninguno más» y §12 habla de los cinco—, o es un tipo de respuesta aparte, y entonces FA-02 y CA-05 lo nombran como tal. La segunda opción es más coherente con la separación que CU-04 y CU-05 ya practican entre proyección y detalle.

**H-03 · P1 · El índice maestro promete la correspondencia con la previsión de 01 en una sección que no la contiene**

- **Archivo y sección.** `02-Especificacion-Funcional/Especificacion-Funcional.md` §3.2, que remite a §4.1.
- **Evidencia.** §3.2: «`Necesidades-Negocio.md` §5.3 prevé veintidós casos de uso `CU-01` a `CU-22` a nivel producto; esa previsión se reparte entre las especificaciones funcionales de los siete proyectos de código … **La correspondencia con la previsión de 01 se lee en §4.1** y no obliga a renumerar nada». §4.1 se titula «Cobertura inversa: de NB a CU» y su tabla tiene tres columnas —NB, grado en que este proyecto de código la sostiene, qué queda en otro proyecto de código— y **ninguna referencia a los identificadores `CU-01` a `CU-22` de `Necesidades-Negocio.md` §5.3**. La única mención de esa serie en toda la fase es la de §3.2.
- **Por qué es P1.** La decisión de numerar local es la correcta y está bien fundada, pero deja dos series homónimas en la misma cadena documental: el `CU-01` de esta sección y el `CU-01` que 01 §5.3 asigna a NB-00001 designan cosas distintas. Lo único que evita que un subagente aguas abajo confunda las dos es la correspondencia, y la correspondencia no existe: el remite es a una sección que habla de otra cosa. `Necesidades-Negocio.md` §5.3 declara «La numeración es una previsión de esta categoría y **la confirma la categoría 02 al redactarlos**», y esa confirmación queda pendiente.
- **Recomendación.** Agregar a §4.1 —o como §4.2— una columna o una tabla que declare, por NB, qué `CU-XX` de la previsión de 01 absorbe este proyecto de código y cuáles quedan para los otros seis espacios; o, si la correspondencia uno a uno no es determinable hasta que las siete especificaciones existan, decirlo así y corregir el remite de §3.2 para que no prometa lo que no entrega.

### P2

**H-04 · P2 · El glosario de 02 incumple su propia regla de inclusión: «papel» y «estado degradado» aparecen en cinco y en seis artefactos y no están declarados en ninguna capa**

- **Archivo y sección.** `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §4; se propaga a `03-UX-UI-DX/Glosario-UX.md` §4.
- **Evidencia.** «Papel» aparece en `Especificacion-Funcional.md` §4.1, CU-01 (§3, §4 pasos 4 y 6, FA-03, §7, CA-01, §9, §12), CU-02 §3 y §10, CU-04 (§2, §3, §4, FA-01, §9) y CU-05 §9 —cinco artefactos—, y el contrato lo trata como **campo de conjunto cerrado**: CU-01 §3, «El contrato ya declara los dos papeles del producto, alumno y administrador, **como valores admitidos del campo de papel**», y `DXC-03` lo enumera junto a los otros conjuntos cerrados: «papel, situación de cuenta, estado del trabajo, severidad de observación o código de error». De esos cinco, «situación de cuenta» y «estado del trabajo» **sí** tienen entrada de glosario; «papel» no. «Estado degradado» aparece en los seis casos de uso —seis, cinco, uno y uno respectivamente en CU-06, CU-01 a CU-05— y en `DX-Error-Messages.md` `DXT-11` y §2.1, sin entrada en ninguno de los dos glosarios. Barrido de `Vision-Producto.md` §9.1 y §9.2: ninguno de los dos está declarado aguas arriba, de modo que no hay a qué referenciar.
- **Regla violada.** `Rules-Especificacion-Funcional.md` §3.3, regla de inclusión: «Todo término del dominio que aparezca en **más de un artefacto de 02** … debe estar en `Glosario-Funcional.md`»; y §6, décimo ítem. `Rules-UX-UI-DX.md` §3.3 y §6 para la propagación a 03.
- **Por qué es P2 y no P1.** No hay contradicción ni lectura incorrecta posible: los dos términos se resuelven dentro de su oración y ninguno es polisémico en esta fase (ver §5.3). Es un defecto de completitud del glosario, no de comprensión. Se separa de H-01 porque el remedio es distinto.
- **Recomendación.** Dos filas en `Glosario-Funcional.md` §2: «Papel», con los dos valores admitidos y la nota de que el contrato lo transporta y no lo hace cumplir —que ya está en CU-02 §10—, y «Estado degradado», con la definición operativa que `Alcance-Producto.md` §8 y `NB-00008` §5 ya sostienen. Referenciarlas desde `Glosario-UX.md` §4 en lugar de redefinirlas.

**H-05 · P2 · La omisión de `Definicion-<Concepto-Central>.md` se funda en una celda de la regla que dice lo contrario**

- **Archivo y sección.** `02-Especificacion-Funcional/README.md` §4, primera fila; `Especificacion-Funcional.md` §8, primer ítem.
- **Evidencia.** `README.md` §4: «§2.1 lo marca **recomendado**, y expresamente **omitible** para “library con superficie estrecha”». `Especificacion-Funcional.md` §8: «recomendado, no obligatorio, y **expresamente marcado como omitible** en “library con superficie estrecha”, que es este caso». La tabla de `Rules-Especificacion-Funcional.md` §2.1 tiene cinco columnas —Archivo, Obligatorio para, **Recomendado**, Omitir para, Descripción— y la fila dice: obligatorio para «Proyectos de código con un concepto técnico central», recomendado «**library con superficie estrecha**», omitir para «**Tipos sin concepto central**». La frase citada está en la columna **Recomendado**: lo que la regla dice es que para una `library` con superficie estrecha el documento está **recomendado**, y la celda que habilita la omisión es la otra.
- **Por qué es P2.** La omisión probablemente sea correcta —el motivo material que las dos secciones dan, que los seis contratos de uso ya describen el contrato completo y que un documento aparte duplicaría sus §1 y §12, es un argumento de «tipo sin concepto central» y es bueno—, pero está apoyada en una celda que dice lo contrario. Un revisor que abra la regla encuentra que el artefacto estaba recomendado para este caso exacto. Contrasta con el `README.md` de 03, que lee su tabla homóloga sin un solo error en las ocho omisiones, incluida la celda «Recomendado: library con portal hospedado» de `DX-Portal-Developers.md`, que cita correctamente como recomendación y no como permiso de omisión.
- **Recomendación.** Refundar la omisión sobre la columna «Omitir para: tipos sin concepto central», argumentando —como ya se hace— que la superficie del ensamblado no tiene un concepto técnico central separable de los seis contratos de uso; y corregir el texto de las dos secciones para que no atribuya a la columna «Recomendado» un permiso que no da.

**H-06 · P2 · `Glosario-UX.md` cita una sección que no existe en la regla**

- **Archivo y sección.** `03-UX-UI-DX/Glosario-UX.md` §2, fila «Rol de intervención developer», columna «Sinónimos y alias».
- **Evidencia.** «Sustituye a “audiencia” en los artefactos DX, por `Rules-UX-UI-DX.md` **§1.7**». `Rules-UX-UI-DX.md` tiene §1.1 a §1.5 y ninguna §1.7. La decisión citada existe, pero vive en **§9, fila 1.7 del control de cambios**: «Normalización del vocabulario de actores: “consumidor” pasa a “integrador” u “operador” … y “audiencia” pasa a “rol de intervención” en las secciones DX». La cita confunde el número de versión de la regla con un número de sección.
- **Por qué es P2.** D6 pide que las referencias entre documentos resuelvan, y ésta no resuelve. No rompe la cadena de trazabilidad SDD —no es un eslabón upstream ni downstream, sino una cita normativa dentro de una celda de glosario—, por eso no se escala. Es además la única cita de las varias decenas a archivos de reglas que no resuelve: se verificaron las citas a `Rules-Especificacion-Funcional.md` §2.1, §2.2, §3.3, §4.3, §4.5 y §5.2 y a `Rules-UX-UI-DX.md` §1.2, §2.1, §2.2, §3.3 y §6, y todas resuelven.
- **Recomendación.** Reemplazar por «`Rules-UX-UI-DX.md` §9, entrada 1.7 del control de cambios», o por la referencia funcional a §4.2.3 punto 1, que es la sección vigente que nombra el rol de intervención.

**H-07 · P2 · El índice maestro afirma que los casos de uso «refieren» las reglas de dominio en su tabla de trazabilidad, y ninguna tabla nombra una `RN-XX`**

- **Archivo y sección.** `02-Especificacion-Funcional/Especificacion-Funcional.md` §5, último párrafo; §9 de los seis casos de uso.
- **Evidencia.** §5: «Las reglas de dominio se están documentando en la especificación funcional de `GeometriaFactory-Domain`; los casos de uso de esta sección las **refieren en su tabla de trazabilidad** y no las redactan». Las seis tablas dicen «Ninguna en este proyecto de código», seguido de prosa que nombra la invariante y su proyecto de código destino —«La unicidad del administrador, la transición de situaciones y el arrastre de trabajos en la baja son invariantes de `GeometriaFactory-Domain`»— pero **ningún identificador**. Al menos uno es nombrable hoy: `PRODUCT-INTAKE` §17.4.P.5, que es la fuente declarada de `RT-01` y `RT-02`, dice «El DTO de respuesta de error lleva texto neutro y, cuando corresponde, índice de figura y campo (**RN-09**)», y el archivo `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md` existe. Lo mismo con RN-03 (trabajo ajeno indistinguible de inexistente) para CU-03 CA-04 y CU-06 CA-05, RN-04 (eliminación acotada al borrador) para CU-03 CA-03, RN-05 (finalización sin errores de validación) para CU-03 CA-05 y RN-08 (texto original conservado íntegro) para CU-03 CA-02.
- **Por qué es P2.** La vacuidad de la columna RN está declarada con motivo, que es lo exigido, y la decisión de no redactar `RN-XX` acá es correcta. El defecto es de ajuste entre lo que §5 afirma y lo que las tablas hacen, y cuesta una oportunidad de trazabilidad barata: cinco identificadores existentes que un revisor de 08 podría seguir. No se escala a P1 porque la prosa de cada tabla nombra la invariante en lenguaje natural y su proyecto de código destino, de modo que la información no se pierde, sólo no es seguible por identificador.
- **Recomendación.** O completar la referencia por identificador en las seis tablas —fila «Reglas de negocio aplicables: ninguna propia; aplican `GeometriaFactory-Domain` RN-03, RN-09»— o corregir §5 para que diga que las nombran en lenguaje natural y no por identificador, con el motivo de que la especificación de dominio se está redactando en paralelo.

### P3

**H-08 · P3 · Conteo incorrecto de los artefactos posibles de la categoría 02**

- **Archivo y sección.** `02-Especificacion-Funcional/README.md` §4, párrafo introductorio.
- **Evidencia.** «`Rules-Especificacion-Funcional.md` §2.1 define **siete** artefactos posibles para esta categoría. Se emiten cuatro y se omiten tres». La tabla maestra de §2.1 tiene **ocho** filas: `Especificacion-Funcional.md`, `Definicion-<Concepto-Central>.md`, `Casos-De-Uso/CU-XX`, `Reglas-De-Negocio/RN-XX`, `Modelo-Datos/Modelo-Conceptual.md`, `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX`, `Glosario-Funcional.md` y `README.md`. El conteo colapsa el modelo conceptual y las `RC-XX` en una sola fila de la tabla de omisiones, que es una decisión de presentación razonable, pero el número declarado queda mal.
- **Por qué es P3.** No induce ninguna omisión: las ocho filas están efectivamente cubiertas, cuatro emitidas y cuatro omitidas con motivo. Es un número que no cierra en un documento cuyo valor está en que los números cierren. Contrasta con el `README.md` de 03, que declara «trece artefactos posibles … se emiten cinco y se omiten ocho» y **cierra exacto** contra las trece filas de `Rules-UX-UI-DX.md` §2.1.
- **Recomendación.** «Define ocho artefactos posibles. Se emiten cuatro y se omiten cuatro, agrupados en tres filas porque el modelo conceptual y sus reglas conceptuales se omiten por el mismo motivo».

**H-09 · P3 · La sección opcional de `library` se numera §12 y la regla la numera §17**

- **Archivo y sección.** §12 de `CU-01` a `CU-06`.
- **Evidencia.** Los seis cierran con «## 12. Compatibilidad de versión pública», y su primera línea dice «Sección opcional admitida para `library` por `Rules-Especificacion-Funcional.md` §4.3». §4.3 de la regla enumera las opcionales con número fijo: «§12 Performance esperado del CU, sólo para rest-api, worker-service y mobile-app-maui; §13 Interacción multiusuario …; §16 Contrato de stdout/stderr y exit codes, sólo para cli-tool; **§17 Compatibilidad de versión pública, sólo para library**».
- **Por qué es P3.** No falta ni sobra contenido, las once obligatorias no se desplazan y la sección es la correcta para el tipo D8. El riesgo es de lectura automatizada: un subagente que busque «§12» en un CU de otro proyecto de código del mismo producto encontrará performance en unos y compatibilidad en otros. Se registra como P3 y no más porque el título de la sección desambigua por sí solo y porque las trece referencias cruzadas de 03 la citan por nombre además de por número.
- **Recomendación.** Renumerar a §17 en los seis, o declarar en `Especificacion-Funcional.md` §6 la convención de numerar la única opcional aplicable como la siguiente disponible, con el motivo.

**H-10 · P3 · La cabecera del `README.md` de 02 no usa el bloque de metadatos de §4.1**

- **Archivo y sección.** `02-Especificacion-Funcional/README.md`, líneas 3 a 13.
- **Evidencia.** La cabecera es una tabla de dos columnas «Campo | Valor», mientras `Rules-Especificacion-Funcional.md` §4.1 modela un bloque de líneas `**Campo:** valor` y los otros trece artefactos de la fase —incluido el `README.md` de 03, que es su homólogo exacto— usan esa forma.
- **Por qué es P3.** Los siete campos exigidos están, más el producto y las dos trazabilidades, de modo que no falta información y la cabecera no está ausente. Es inconsistencia de forma dentro de la misma fase.
- **Recomendación.** Unificar con la forma de los otros trece, o dejarlo si se adopta la tabla como convención de README y se declara.

**H-11 · P3 · El gate del 100 % de tipos ejercitados se presenta como valor vigente sin el rótulo de asunción que el intake le pone**

- **Archivo y sección.** `Especificacion-Funcional.md` §6 `RT-07`; `02/README.md` §5, nota de verificación; `DX-Developer-Experience.md` §6, cuarta métrica.
- **Evidencia.** `README.md` §5: «su gate equivalente es que el **cien por ciento** de los tipos de transferencia esté ejercitado por al menos una prueba de integración (`PRODUCT-INTAKE` §17.4 P.6)». `DX-Developer-Experience.md` §6: «100 %, **gate bloqueante**». El intake §17.4.P.6 dice: «el gate equivalente y bloqueante es que el 100 % de los DTOs esté ejercitado por al menos una prueba de integración **[ASUNCIÓN]**», y la nota de cabecera de la Parte C aclara que esos valores «van rotulados [ASUNCIÓN] y se listan en §22 para que el Product Owner los confirme».
- **Por qué es P3.** El valor es el vigente y usarlo es correcto; lo que falta es el rótulo. Se levanta porque el propio corpus fijó el estándar más alto en el caso análogo: CU-04 §10 declara «El requisito estructural de CA-01 es del propio intake y **se rotula ahí como asunción derivada**; está completo y se usa como valor vigente». El mismo tratamiento le corresponde a P.6.
- **Recomendación.** Agregar la misma nota de una línea en `RT-07` y en la fila de la métrica.

**H-12 · P3 · CU-05 CA-06 declara cinco bloques y verifica cuatro**

- **Archivo y sección.** `CU-05` §8, CA-06.
- **Evidencia.** «Son del mismo tipo y traen **los mismos cinco bloques**: **4 de 4** elementos coincidentes entre lo que ve el administrador y lo que ve el alumno —datos, texto, piezas y observaciones—». §4 paso 2 declara los cinco bloques: «datos del trabajo, texto original íntegro, colección de piezas, colección de observaciones **y datos de identificación del alumno dueño**». El quinto queda fuera del conteo verificable sin decir por qué.
- **Por qué es P3.** El criterio es verificable y su intención —que no exista variante enriquecida para el administrador, reforzada por `DXC-07`— se cumple con los cuatro bloques comparados. Es una aritmética que no cierra en un criterio escrito precisamente para cerrar por conteo.
- **Recomendación.** «5 de 5», o declarar que el quinto bloque se excluye de la comparación porque el alumno dueño se ve a sí mismo.

**H-13 · P3 · CU-02 enumera «dar de baja» entre los cambios de situación y a la vez cierra el conjunto en tres valores**

- **Archivo y sección.** `CU-02` §1 contra §3 y FA-01.
- **Evidencia.** §1: «la orden de cambio de situación de una cuenta —**habilitar, bloquear, rehabilitar y dar de baja**—». §3: «El contrato declara el conjunto cerrado de situaciones de cuenta que el producto reconoce: **pendiente, habilitada y bloqueada**». FA-01 trata la baja como **solicitud propia**, con campo de confirmación escrita, y no como valor de situación pretendida.
- **Por qué es P3.** La resolución correcta está en §3 y en FA-01, y `Glosario-Funcional.md` §2 la respalda con los tres valores; sólo el enunciado de §1 lo lee como una cuarta transición. Un implementador que lea §1 suelto podría declarar un cuarto valor del conjunto cerrado, que `DXC-03` clasificaría como cambio incompatible.
- **Recomendación.** Reformular §1: «… la orden de cambio de situación —habilitar, bloquear, rehabilitar— y la solicitud de baja, que es un tipo aparte porque exige la confirmación escrita».

**H-14 · P3 · Dos códigos figuran en la tabla de excepciones de su caso de uso mientras el propio texto declara que no son error**

- **Archivo y sección.** `CU-04` §6, fila `CONTRATO_LISTADO_VACIO`; `CU-05` §6, fila `CONTRATO_TEXTO_NO_INTERPRETABLE`.
- **Evidencia.** CU-04 §6: «`CONTRATO_LISTADO_VACIO` … **No es error**: el contrato devuelve la colección con cero elementos». CU-05 §6: «`CONTRATO_TEXTO_NO_INTERPRETABLE` … **No es una respuesta de error**: el trabajo existe y hay que poder verlo». La sección se titula «Excepciones y errores» y `Rules-Especificacion-Funcional.md` §4.2 punto 6 la define como «cada error con código, causa y respuesta del sistema».
- **Por qué es P3.** La decisión de diseño es buena y el catálogo de 03 la resuelve mejor que 02: `DX-Error-Messages.md` §3.3 saca `CONTRATO_LISTADO_VACIO` de las dos tablas de error y lo cataloga como «señal declarada que no es error», con `DXT-N1` fuera de la serie. Es la ubicación en 02 lo que confunde, y sólo hasta leer la celda.
- **Recomendación.** Mover las dos filas a §5 (flujos alternativos) o a §10 (notas), o adoptar en 02 la solución de 03: una subsección de señales declaradas que no son error.

---

## 7. Veredicto y condiciones para promover

**Veredicto: APROBADO CON OBSERVACIONES.**

Ningún hallazgo es P0. No falta ningún artefacto obligatorio de las dos categorías para `library`, ninguna cabecera, ninguna de las once secciones obligatorias en ninguno de los seis casos de uso, ninguna de las nueve de `DX-Developer-Experience.md`, ninguna de las cinco de los dos glosarios ni ninguna tabla de contenido. La trazabilidad no se rompe: las veintiocho declaraciones de cabecera citan secciones concretas del upstream, las ocho NB tienen cobertura y ninguno de los seis casos de uso queda huérfano. No hay vocabulario del dominio fuente del framework, no hay emojis, no hay sufijos de versión en nombres y las once omisiones están declaradas con motivo. **La cadena no se detiene.**

Conteo final: **0 P0, 3 P1, 4 P2, 7 P3** sobre los catorce entregables. Ninguna observación dirigida al intake ni al framework.

Condiciones para promover la fase a la categoría 05:

1. **Bloqueante para el despacho de 05 y 06 — H-02.** La superficie del tipo de respuesta de sesión tiene que quedar en una sola forma antes de que 05 derive componentes y 06 derive `US-01` y `US-02`, porque las dos categorías consumen exactamente esa lista de campos. Es el único hallazgo que, sin corregir, se materializa en código incorrecto.
2. **Bloqueante para el despacho de 06 — H-03.** La correspondencia con la previsión `CU-01` a `CU-22` de `Necesidades-Negocio.md` §5.3 tiene que existir o el remite de §3.2 tiene que corregirse, antes de que el segundo proyecto de código abra su propia serie local y las homonimias se multipliquen por siete.
3. **Corregir antes de cerrar la fase — H-01.** Tres ocurrencias léxicas, sin efecto estructural, enumeradas y sustituidas una por una según `Vocabulario-Rules.md` §9.5 y **nunca por reemplazo global de la cadena**.
4. **Corregir en la misma pasada — H-04, H-05, H-06, H-07.** Dos filas de glosario, una refundamentación de omisión, una cita de regla y una decisión sobre cómo referir las `RN-XX` del proyecto de código hermano. Ninguna toca estructura.
5. **A criterio del orquestador — H-08 a H-14.** Siete correcciones de una a tres líneas cada una. Ninguna bloquea la cadena y ninguna reabre una decisión de diseño.
6. **Versionado de la corrección.** Los artefactos tocados suben **minor** —1.0 a 1.1— con su entrada de control de cambios, según D5. Ninguno de los catorce requiere subida mayor: no hay cambio de alcance de ningún caso de uso.

Se deja constancia de lo que esta auditoría **no** reportó, para que una ronda posterior no lo levante: las ocho polisemias con contextos disjuntos enumeradas en §5.3, la ausencia de `Experiencia-De-Uso.md`, de wireframes, de `DX-Portal-Developers.md`, de `DX-Operability.md`, de los tres artefactos de maqueta, de `Definicion-<Concepto-Central>.md`, de las `RN-XX` y del modelo conceptual —las once declaradas y todas correctas por gating—, la ausencia de la categoría 04 por `usa_llm` == false, y la concreción del quick-start de 03, que la propia `Rules-UX-UI-DX.md` §6 exige.

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Auditoría de la Fase B de `GeometriaFactory-Contracts`, categorías 02 y 03, sobre catorce artefactos: matriz D1-D9, matriz de estructura obligatoria contra §4 de las dos reglas, verificación ítem por ítem de los treinta y siete criterios de §6, coherencia cross-doc con las referencias de 03 a 02 contrastadas una por una, gobierno del glosario con los cuatro criterios y once polisemias evaluadas, y catorce hallazgos. | Arquitecto de Soluciones + QA Senior |
