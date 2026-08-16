# Informe de auditoría — Fase B · GeometriaFactory-Domain · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Proyecto de código auditado | `GeometriaFactory-Domain` |
| `tipo_proyecto_codigo` (D8) | `library` |
| Fase | B — categorías **02-Especificacion-Funcional** y **03-UX-UI-DX** |
| Alcance | Fase completa (primera auditoría de este proyecto de código): 26 documentos de 02 y 5 de 03, más sus 20 copias de `_legacy/2026-08-09/` |
| Categoría 04 | Omitida por gating (`usa_llm` == false, `PRODUCT-MANIFEST` 1.1 §5). Su ausencia **no** es hallazgo |
| Insumos normativos | `Rules-Especificacion-Funcional.md` §2, §3, §4, §5.2, §6; `Rules-UX-UI-DX.md` §2.1, §2.2, §4, §6; `Vocabulario-Rules.md` §9 y §10; `Master-Prompt.md` §5 y §10 |
| Insumos de contexto | `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.3; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.1; `00-Contexto/` 1.1; `01-Necesidades-Negocio/` 1.1 (con `NB-00009` 1.0) |
| Auditor | Auditor independiente de fase — Arquitecto de Soluciones + QA Senior. Sin participación en la generación de la Fase B |
| Fecha | 2026-08-09 |
| Ronda | r1 |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Matriz D1-D9 por documento](#2-matriz-d1-d9-por-documento)
- [3. Matriz de estructura obligatoria por documento](#3-matriz-de-estructura-obligatoria-por-documento)
- [4. Verificación de reglas e invariantes](#4-verificación-de-reglas-e-invariantes)
- [5. Verificación mecánica del catálogo de errores](#5-verificación-mecánica-del-catálogo-de-errores)
- [6. Coherencia cross-doc y gobierno del glosario](#6-coherencia-cross-doc-y-gobierno-del-glosario)
- [7. Hallazgos](#7-hallazgos)
- [8. Puntos abiertos registrados, que no son hallazgo](#8-puntos-abiertos-registrados-que-no-son-hallazgo)
- [9. Veredicto y condiciones para promover](#9-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Se auditaron los treinta y un documentos vivos de las categorías 02 y 03 de `GeometriaFactory-Domain`, más los veinte snapshots de `_legacy/2026-08-09/`. **La verificación central de esta auditoría —que no haya reglas ni invariantes inventados— dio resultado limpio**: las once reglas y los siete invariantes emitidos coinciden en sustancia con el enunciado del intake 1.3, no hay ninguno de más, ninguno de menos, y la corrección de la atribución errónea de INV-04 está completa en los seis artefactos que la propagaban. La verificación mecánica del catálogo de errores, rehecha desde cero contra la §6 de los once casos de uso, **confirma exactamente 37 condiciones distintas sobre 40 filas declaradas**, sin inventadas ni faltantes.

Total de hallazgos: **13** — **P0: 0 · P1: 1 · P2: 2 · P3: 10**. El único P1 es una contradicción de especificación sobre el escenario canónico E-5, entre `CU-06` FA-03, `CU-07` CA-03 y la causa declarada de `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` en el catálogo; los dos P2 son una remisión cruzada errónea en `CU-10` FA-02 y un código de rechazo huérfano en `RN-05` §4 que falsifica la exhaustividad que el catálogo afirma. Ninguno bloquea.

**Veredicto: APROBADO CON OBSERVACIONES.**

---

## 2. Matriz D1-D9 por documento

Convención de las invariantes globales, según `Master-Prompt.md` §5: **D1** idioma español rioplatense neutro técnico, sin emojis ni negritas decorativas · **D2** encoding, tildes y eñes en el cuerpo · **D3** slug Título-Con-Guiones, filename ASCII · **D4** versión en cabecera, sin sufijo en el archivo vivo, sufijo con guion medio en `_legacy/` · **D5** una sola versión vigente por nombre lógico, archivado en `_legacy/<fecha>/` · **D6** trazabilidad upstream y downstream con secciones concretas · **D7** sin vocabulario ni ejemplos del dominio fuente del framework · **D8** conjunto cerrado de tipos de proyecto de código respetado · **D9** evidencia verificable para toda afirmación sobre el estado del sistema.

Leyenda: **OK** cumple · **n/a** no aplica.

### 2.1 Categoría 02

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `Especificacion-Funcional.md` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `Definicion-Modelo-De-Dominio.md` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `Glosario-Funcional.md` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `README.md` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-01` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-02` 1.1 | OK | OK | OK | OK | OK | **P3-01** | OK | OK | n/a |
| `CU-03` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-04` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-05` 1.1 | OK | OK | OK | OK | OK | **P3-01** | OK | OK | n/a |
| `CU-06` 1.1 | OK | OK | OK | OK | OK | **P3-01** | OK | OK | n/a |
| `CU-07` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-08` 1.1 | OK | OK | OK | OK | OK | **P3-08** | OK | OK | n/a |
| `CU-09` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `CU-10` 1.0 | OK | OK | OK | OK | OK | **P2-01** | OK | OK | n/a |
| `CU-11` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-01` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-02` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-03` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-04` 1.1 | OK | OK | OK | OK · ver P3-04b | OK | OK | OK | OK | n/a |
| `RN-05` 1.1 | OK | OK | OK | OK · ver P3-04b | OK | OK | OK | OK | n/a |
| `RN-06` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-07` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-08` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-09` 1.1 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-10` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `RN-11` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |

### 2.2 Categoría 03

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `DX-Developer-Experience.md` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `Guia-Onboarding-Developer.md` 1.0 | OK | OK | OK | OK | OK | **P3-05** | OK · ver P3-06 | OK | n/a |
| `DX-Error-Messages.md` 1.0 | OK | OK | OK | OK | OK | **P3-02** | OK | OK | n/a |
| `Glosario-UX.md` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |
| `README.md` 1.0 | OK | OK | OK | OK | OK | OK | OK | OK | n/a |

### 2.3 Notas de la matriz

- **D9 — n/a en toda la fase, y es lo correcto.** No hay sistema construido: la Fase B especifica, no documenta hechos. Las afirmaciones de los treinta y un documentos son de especificación, de diseño o de contexto, y `Master-Prompt.md` §15 excluye esas tres clases del alcance de D9. **No se convirtió ninguna afirmación de especificación en hallazgo por no citar evidencia.** La única afirmación de esta fase que sí habría caído bajo D9 —la de que el catálogo de 03 deriva de la §6 de los once casos de uso sin inventar ni omitir— la verificó el propio documento en su §6.1 y la volvió a verificar esta auditoría en §5 de este informe.
- **D4 y D5 — verificados por ejecución.** Ningún archivo vivo lleva sufijo de versión; los veinte snapshots lo llevan con guion medio (`-v1.0.md`). Las veinte copias de `_legacy/2026-08-09/` conservan `Versión: 1.0` y `Fecha: 2026-08-08`, y su marca de tiempo de escritura (00:40:47) es **anterior** a la del primer documento vivo (00:45:24): ningún subagente las tocó. Ver P3-07 sobre el estado de cabecera de esas copias.
- **D6 — nivel de detalle verificado documento por documento.** Ninguna cabecera cita «PRODUCT-INTAKE» a secas. Todas nombran secciones concretas —el mínimo observado es `RN-02`, con «§4.1 (enunciado de RN-02), §4 (F-02), §17.1.P.2 (INV-01), §6 (flujo 1), §7 (CL-6 y CL-7)»— y las que citan 00 y 01 lo hacen por sección (`Vision-Producto.md` §9.1, `NB-00004` §1, §4 y §5). `NB-00009`, que es nuevo, aparece como upstream en `RN-04`, `RN-10`, `RN-11`, `CU-10`, `CU-11`, `Especificacion-Funcional.md` §5.1 y `Definicion-Modelo-De-Dominio.md` §9. Las marcas P2/P3 en la columna D6 señalan remisiones cruzadas incompletas o erróneas dentro del cuerpo, no cabeceras defectuosas.
- **D8 — conjunto cerrado respetado.** Los treinta y un documentos declaran o presuponen `library`, coherente con `PRODUCT-MANIFEST` 1.1 §5. `Rules-UX-UI-DX.md` §1.2 asigna variante **DX** a `library`, y los cinco artefactos de 03 la declaran en su cabecera, como pide §4.1.
- **D7 — sin vocabulario del dominio fuente.** La única mención de formato de serialización en toda la categoría 02 es la columna de alias de `Glosario-Funcional.md` §2 —«"JSON original" en las fuentes técnicas»—, que es exactamente la forma que la regla pide: declarar el alias y no adoptarlo.

---

## 3. Matriz de estructura obligatoria por documento

### 3.1 Categoría 02, contra `Rules-Especificacion-Funcional.md` §4

**§2.1 y §2.2 — artefactos que corresponden a `library`:**

| Artefacto | Regla | Emitido | Veredicto |
| --- | --- | --- | --- |
| `Especificacion-Funcional.md` | Obligatorio para todos los D8 | Sí, 1.1 | OK |
| `Definicion-<Concepto-Central>.md` | Recomendado para `library` con superficie estrecha | Sí, `Definicion-Modelo-De-Dominio.md` 1.1 | OK. El nombre respeta el patrón de §3.1 |
| `Casos-De-Uso/CU-XX` | Mínimo 5 para `library` | 11 | OK sobre el piso. Apartamiento de la guía de §5.2 evaluado en §6.4 de este informe |
| `Reglas-De-Negocio/RN-XX` | Recomendadas para `library` con reglas de dominio | 11, serie contigua RN-01 a RN-11 | OK |
| `Modelo-Datos/Modelo-Conceptual.md` | **Se omite** para `library` sin persistencia | Omitido | OK. Omisión declarada con motivo en `Especificacion-Funcional.md` §7 y en `README.md` §5, citando `tiene_persistencia` == false y §17.1.P.4 |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX` | **Se omite** para `library` | Omitidas | OK. Omisión declarada en los mismos dos lugares |
| `Glosario-Funcional.md` | Obligatorio para los ocho D8 | Sí, 1.1, tabla de 14 términos | OK |
| `README.md` de la sección | Recomendado | Sí, 1.1 | OK |

**§4.2 — las once secciones obligatorias de cada CU:**

| CU | 1 Propósito | 2 Actores | 3 Precond. | 4 Flujo ppal. | 5 Flujos alt. | 6 Excep. | 7 Postcond. | 8 CA G/W/T | 9 Trazab. | 10 Notas | 11 Ctrl. cambios | Extra |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| CU-01 a CU-11 | OK | OK | OK | OK | OK | OK | OK | OK (5 CA cada uno, con valores concretos) | OK | OK | OK | §12 «Compatibilidad de la superficie pública» · **P3-04** |

Verificación por ejecución: los once archivos tienen exactamente los encabezados `## 1.` a `## 12.` en ese orden, con tabla de contenido inmediatamente después de la cabecera, como pide §4.1. Los criterios de aceptación son cinco por caso de uso sobre un mínimo de tres, todos con valores concretos tomados de los escenarios E-1 a E-7 del intake (áreas 36.00 / 54.00, volúmenes 343.00 / 1029.00, posición de pieza 1, campo `Tipo`). Anti-patrón «CU sin escenarios de error» de §4.5: ninguno lo comete, todos tienen §6 poblada.

**§4.2.1 — las siete secciones obligatorias de cada RN:**

| RN | 1 Enunciado | 2 Justificación | 3 Ámbito | 4 Consecuencia | 5 CU afectados | 6 Pruebas | 7 Ctrl. cambios |
| --- | --- | --- | --- | --- | --- | --- | --- |
| RN-01 a RN-11 | OK | OK | OK | OK | OK, lista explícita con enlaces | OK, referencia a 08 | OK |

Anti-patrón «RN escrita como CU» de §4.5: ninguna lo comete; las once enuncian una restricción declarativa. Anti-patrón «RN ambigua o subjetiva»: ninguna; las once declaran consecuencia con código de rechazo y criterio de prueba.

**§4.2.4 — las cinco secciones obligatorias del glosario funcional:** cabecera con upstream al glosario de 00 (OK), tabla de términos (OK, 14 filas, no vacía), términos con más de un referente (OK, §3 con tres subsecciones más §3.4 de casos descartados), términos referenciados y no redefinidos (OK, §4 con 24 filas), control de cambios (OK).

**§4.2.2 no aplica**: `Modelo-Conceptual.md` está omitido. Se verificó igualmente que `Definicion-Modelo-De-Dominio.md`, que ocupa su lugar como documento de concepto central, cubre entidades con ejemplo de instancia (§2.1 a §2.5), atributos sin tipos físicos, relaciones verbalizadas y cardinalidades con notación uniforme (§3), referencia al glosario (§8), diagrama Mermaid (§3 y §5) y trazabilidad (§9). No introduce ningún tipo físico: el anti-patrón «modelo conceptual con `varchar(255)`» no se comete.

### 3.2 Categoría 03, contra `Rules-UX-UI-DX.md` §4

**§2.1 y §2.2 — artefactos que corresponden a `library`, variante DX, con el mínimo de wireframes en cero:**

| Artefacto | Regla | Emitido | Veredicto |
| --- | --- | --- | --- |
| `DX-Developer-Experience.md` | Obligatorio para `library` | Sí, 1.0 | OK |
| `Guia-Onboarding-Developer.md` | Obligatorio para `library` | Sí, 1.0 | OK · **P3-03** |
| `DX-Error-Messages.md` | Obligatorio para `library` | Sí, 1.0 | OK |
| `Glosario-UX.md` | Obligatorio para los ocho D8 | Sí, 1.0, tabla de 16 términos | OK |
| `README.md` de la sección | Recomendado | Sí, 1.0 | OK |
| `Experiencia-De-Uso.md` | Variante UX/UI, se omite para `library` | Omitido | OK. **Omisión declarada con motivo** en `README.md` §4 |
| `wireframes-<superficie>.md` | Mínimo 0 para `library` | Omitidos | OK. **Omisión declarada con motivo** en `README.md` §4 |
| `representacion-<concepto>.md` | Condicional | Omitido | OK. **Omisión declarada con motivo** |
| `DX-Portal-Developers.md` | `tiene_portal_developers` == false | Omitido | OK. **Omisión declarada con motivo** |
| `DX-Operability.md` | Obligatorio sólo para `worker-service` | Omitido | OK. **Omisión declarada con motivo** |
| `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md`, `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` == false | Omitidos | OK. **Omisión declarada con motivo** |

La condición que el encargo señala como hallazgo —«que el README no declare la omisión con su motivo»— **no se produce**: `README.md` §4 declara las ocho omisiones una por una y, además, enumera los siete criterios de aceptación de `Rules-UX-UI-DX.md` §6 propios de la variante UX/UI como no aplicables, **sin darlos por cumplidos**. El tratamiento del criterio de accesibilidad es correcto y merece destacarse: no lo declara cumplido, lo remite al proyecto de código de la pieza pública.

**§4.2.3 a §4.2.5 — secciones obligatorias:**

| Documento | Secciones que la regla exige | Emitidas | Veredicto |
| --- | --- | --- | --- |
| `DX-Developer-Experience.md` | 9 (§4.2.3): rol de intervención, onboarding por tramos, quick-start, Diátaxis, mensajes de error, métricas DX, feedback loop, trazabilidad, control de cambios | 9, en el mismo orden y con los mismos títulos | **OK exacto** |
| `Guia-Onboarding-Developer.md` | 6 (§4.2.4): audiencia y prerrequisitos, instalación o acceso, primer ejemplo ejecutable, diagnóstico de problemas frecuentes, próximos pasos, control de cambios | 7: inserta «§4 Dónde va una regla nueva» y desplaza las tres últimas obligatorias a §5, §6 y §7 | Contenido completo; numeración desplazada · **P3-03** |
| `DX-Error-Messages.md` | 6 (§4.2.5): principios de redacción, taxonomía, catálogo, tono y voz, localización, control de cambios | 7: las cinco primeras conservan su número; agrega «§6 Cobertura y trazabilidad» y el control de cambios pasa a §7 | OK. La sección agregada es la que satisface el criterio de trazabilidad de §6 y no desplaza a ninguna obligatoria de contenido |
| `Glosario-UX.md` | §2.1 lo exige; la regla no fija lista de secciones | 5, espejando `Rules-Especificacion-Funcional.md` §4.2.4 | OK |

**§6 de `Rules-UX-UI-DX.md`, ítem por ítem sobre los criterios aplicables a la variante DX:**

| Criterio | Resultado |
| --- | --- |
| Variante declarada en la cabecera de cada artefacto y coherente con el D8 | OK. Los cinco declaran `**Variante:** DX`; `library` → DX por §1.2 |
| Existe `DX-Developer-Experience.md` con las nueve secciones, con Diátaxis y tramos 5/30/60 verificables | OK. §2 declara los tres tramos con objetivo verificable —ejecutar el ciclo, escribir la tríada código→CU→RN/INV de tres rechazos, clasificar las cuatro reglas sin invariante— y §4 ubica los cuatro modos sin duplicarlos |
| Cada `dx-` doc presenta un quick-start verificable con snippet ejecutable y reproducible | Parcial · **P3-09** |
| Cada artefacto declara trazabilidad upstream y downstream | OK en los cinco |
| Sin sufijo de versión en el archivo vivo; slug Título-Con-Guiones | OK |
| Un solo archivo por nombre lógico; versiones superadas en `_legacy/` | OK. **No hay `_legacy/` en 03 y es correcto**: la categoría nunca se había emitido, y `README.md` §2 lo declara explícitamente |
| Existe `Glosario-UX.md` y su tabla no está vacía | OK, 16 términos |
| Todo término que aparece en más de un artefacto de 03 está declarado, con sus referentes | OK. Ver §6.5 de este informe |
| El glosario de 03 no duplica términos de `Glosario-Funcional.md` con semántica distinta | OK. §4 los referencia en catorce filas; ninguna entrada de §2 los pisa |
| Ninguna polisemia con contextos disjuntos reportada como defecto ni corregida calificando todo | OK. `Glosario-UX.md` §3.3 declara los dos casos descartados con su motivo |
| Sin menciones a stacks concretos, productos comerciales ni protocolos | Parcial · **P3-06** |
| `requiere_maqueta` == true | n/a, es false |
| Tabla de contenido en documentos de más de tres secciones de primer nivel | OK en los cinco |

---

## 4. Verificación de reglas e invariantes

Es la verificación central del encargo. Se contrastó **enunciado por enunciado** el intake 1.3 §4.1 (once reglas) y §17.1.P.2 (siete invariantes) contra los archivos `RN-XX` de 02 y contra las declaraciones de invariante de `Definicion-Modelo-De-Dominio.md` §4.1.

### 4.1 Las once reglas de negocio

| Id | Enunciado del intake §4.1 (resumen literal) | Emitido en `Reglas-De-Negocio/` | ¿Coincide en sustancia? | ¿Inventado? |
| --- | --- | --- | --- | --- |
| RN-01 | «Existe **exactamente un** administrador. Su alta sólo es posible mientras no exista ninguno» | `RN-01` §1: «Existe **exactamente un** administrador, y su alta sólo es posible mientras no exista ninguno. El conjunto de papeles es cerrado y de dos valores» | **Sí.** La segunda oración no agrega regla: es X-3 y F-19 del intake, citados en §2 | No |
| RN-02 | «El correo del alumno es **único**» | `RN-02` §1: «El correo del alumno es único en todo el sistema: dos cuentas no comparten correo en ningún momento» | **Sí** | No |
| RN-03 | «Un alumno **sólo ve y opera sus propios trabajos**» | `RN-03` §1: la formulación de la fuente, seguida de la indistinguibilidad de CL-5 | **Sí** | No |
| RN-04 | «El alumno elimina sus trabajos **sólo en `Borrador`**. El **administrador elimina cualquier trabajo que ve**, en cualquier estado, con borrado físico» | `RN-04` §1: las dos mitades, literales | **Sí** | No |
| RN-05 | «Un trabajo **no pasa a `Pendiente` con errores de interpretación** del JSON. Las advertencias **sí** lo permiten» | `RN-05` §1: idéntico, con «texto» donde el intake dice «JSON» | **Sí.** La sustitución léxica está justificada: el dominio no conoce formatos de serialización (`Glosario-Funcional.md` §2, entrada «texto original») | No |
| RN-06 | «Una cuenta `Pendiente` o `Bloqueada` **no obtiene sesión**» | `RN-06` §1: «no obtiene acceso al laboratorio, y el motivo se le informa» | **Sí.** «Acceso» por «sesión» es sustitución declarada en el control de cambios: «sesión» es vocabulario de otro proyecto de código. La segunda mitad deriva de NB-00002 §2 y §5, citado | No |
| RN-07 | «La **baja física** elimina la cuenta y **todos sus trabajos**, y exige confirmación explícita escribiendo el correo» | `RN-07` §1: las tres partes, más «es irreversible» | **Sí** | No |
| RN-08 | «El **JSON original del alumno se conserva íntegro** y nunca se reescribe» | `RN-08` §1: «se conserva íntegro, carácter por carácter, y el producto no lo reescribe, no lo normaliza y no lo corrige» | **Sí** | No |
| RN-09 | «Los mensajes de error de validación indican **índice de figura y campo**, nunca un texto genérico» | `RN-09` §1: «posición de esa figura en el conjunto raíz y el campo», más la prohibición del texto genérico | **Sí.** «Posición de pieza» por «índice de figura» es alias declarado en `Glosario-Funcional.md` §2 | No |
| RN-10 | «**Sólo el administrador aprueba o rechaza**, y sólo desde `Pendiente`. `Finalizado` y `Rechazado` son terminales» | `RN-10` §1: literal | **Sí** | No |
| RN-11 | «El administrador **no ve los trabajos en `Borrador`**: no forman parte de su flujo de trabajo» | `RN-11` §1: literal, ampliado a «ni para verlo ni para operar sobre él» | **Sí.** La ampliación es coherencia con RN-04 y está justificada en §2 | No |

**Reglas de más: ninguna.** La carpeta contiene exactamente once archivos, serie contigua RN-01 a RN-11, sin RN-12 ni variantes. **Reglas faltantes: ninguna.**

### 4.2 Los siete invariantes

| Id | Enunciado del intake §17.1.P.2 | Emitido en `Definicion-Modelo-De-Dominio.md` §4.1 | ¿Coincide? | Regla que sostiene (intake → emitido) |
| --- | --- | --- | --- | --- |
| INV-01 | «El correo del alumno es único en todo el sistema» | Idéntico | **Sí** | RN-02 → RN-02 ✔ |
| INV-02 | «Un alumno sólo accede a sus propios trabajos. No existe consulta que devuelva trabajos de otro alumno a un rol de alumno» | Idéntico, con «papel» por «rol» en `RN-03` §2 | **Sí** | RN-03 → RN-03 ✔ |
| INV-03 | «Un trabajo **eliminado por un alumno** estaba en `Borrador` y le pertenecía» | «Un trabajo eliminado por un alumno estaba en `Borrador` y le pertenecía» | **Sí, con el recorte intacto** | RN-04 → RN-04 ✔ |
| INV-04 | «Un trabajo `Finalizado` tiene JSON interpretado sin errores (puede tener advertencias)» | «Un trabajo `Finalizado` tiene el texto interpretado sin errores, y puede tener advertencias» | **Sí** | RN-05 → RN-05 ✔ |
| INV-05 | «Existe exactamente un administrador configurado; su alta sólo es posible mientras no exista ninguno» | Idéntico | **Sí** | RN-01 → RN-01 ✔ |
| INV-06 | «Un alumno en estado `Pendiente` o `Bloqueado` no obtiene token» | «Un alumno con cuenta `Pendiente` o `Bloqueado` no obtiene acceso» | **Sí.** «Token» por «acceso» es la misma sustitución declarada de RN-06, y `Definicion-Modelo-De-Dominio.md` §4.1 aclara que el dominio modela la condición y no el mecanismo | RN-06 → RN-06 ✔ |
| INV-07 | «Un trabajo en `Finalizado` o en `Rechazado` no cambia de estado ni de contenido» | Idéntico | **Sí** | RN-10 → RN-10 ✔ |

**Invariantes de más: ninguno.** No existe INV-08 en ningún artefacto vivo. **Invariantes faltantes: ninguno.** Los siete se declaran en `Definicion-Modelo-De-Dominio.md` §4.1 y se citan en las cabeceras de los casos de uso que los ejercen.

### 4.3 Correspondencia regla ↔ invariante

| Regla | Invariante según el intake | Declarado en 02 (`Especificacion-Funcional.md` §4, `README.md` §3, `Definicion-Modelo-De-Dominio.md` §4.2) | Declarado en 03 (`Guia-Onboarding-Developer.md` §4.1) | Veredicto |
| --- | --- | --- | --- | --- |
| RN-01 | INV-05 | INV-05 | INV-05 | ✔ |
| RN-02 | INV-01 | INV-01 | INV-01 | ✔ |
| RN-03 | INV-02 | INV-02 | INV-02 | ✔ |
| RN-04 | INV-03 | INV-03 | INV-03 | ✔ |
| RN-05 | INV-04 | INV-04 | INV-04 | ✔ |
| RN-06 | INV-06 | INV-06 | INV-06 | ✔ |
| RN-07 | **Ninguno** (comportamiento) | Ninguno, con motivo declarado en `RN-07` §3 | Ninguno, con motivo en §4.2 | ✔ |
| RN-08 | **Ninguno** (comportamiento) | Ninguno, con motivo declarado en `RN-08` §2 | Ninguno, con motivo en §4.2 | ✔ |
| RN-09 | **Ninguno** (comportamiento) | Ninguno, con motivo declarado en `RN-09` §3 | Ninguno, con motivo en §4.2 | ✔ |
| RN-10 | INV-07 | INV-07 | INV-07 | ✔ |
| RN-11 | **Ninguno** (alcance de consulta) | Ninguno, con motivo declarado en `RN-11` §2 | Ninguno, con motivo en §4.2 | ✔ |

**Siete con invariante, cuatro sin él, con la causa correcta en cada caso.** La correspondencia declarada es correcta y completa, y se declara de forma consistente en cinco lugares distintos de la fase.

### 4.4 Estado de la corrección de la atribución errónea de INV-04

El intake anterior afirmaba «RN-08 / INV-04 — el JSON original se conserva íntegro», y era falso. La emisión 1.0 propagó el error a **seis artefactos**. Estado de la corrección, verificado archivo por archivo sobre el árbol vivo:

| # | Artefacto | Dónde estaba el error en 1.0 (verificable en `_legacy/2026-08-09/`) | Estado en la versión viva | Veredicto |
| --- | --- | --- | --- | --- |
| 1 | `Definicion-Modelo-De-Dominio.md` | §2.2, atributo «Texto original»: «(RN-08, INV-04)»; §4, tabla: «INV-04 \| El texto original del alumno se conserva íntegro y nunca se reescribe \| … \| CU-05, CU-06; RN-08» | §2.2 cita sólo «(RN-08)»; §4.1 enuncia INV-04 como «un trabajo `Finalizado` tiene el texto interpretado sin errores» y lo asocia a RN-05; §4.2 pone RN-08 en la fila «Ninguno» | **Corregido** |
| 2 | `README.md` de 02 | §3, fila «RN-08 … \| INV-04» | §3, fila «RN-08 … \| —»; el control de cambios 1.1 lo declara: «**Corrige la atribución de INV-04**, que la versión anterior daba como el invariante de RN-08» | **Corregido** |
| 3 | `RN-08-Texto-Original-Conservado-Integro.md` | Cabecera: «§21 (RN-08 / INV-04)»; §2: «El invariante INV-04 lo expresa como propiedad permanente del trabajo» | Cabecera cita «§17.1.P.2 (reglas sin invariante asociado)»; §2 dice «**Esta regla no tiene invariante asociado** … En particular **no la expresa INV-04**, que enuncia otra cosa … y que sostiene a RN-05» | **Corregido, y con la declaración explícita que el encargo pide** |
| 4 | `CU-05-Crear-Y-Reeditar-Un-Trabajo.md` | §9, «Invariantes \| INV-02 …, INV-04» | §9, «INV-02 …, INV-07 …»; control de cambios 1.1 lo declara | **Corregido** |
| 5 | `CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md` | §9, «Invariantes \| INV-04» | §9, «INV-07 …; **RN-08 no tiene invariante asociado**»; control de cambios 1.1 lo declara | **Corregido** |
| 6 | `CU-07-Registrar-Las-Observaciones-Del-Trabajo.md` | §9, «Invariantes \| INV-04» (con el sentido del texto íntegro) | §9, «INV-04, en cuanto la especie de cada observación … sostiene que un trabajo `Finalizado` tenga el texto interpretado sin errores. **RN-08 y RN-09 no tienen invariante asociado**» | **Corregido**: conserva la cita de INV-04 pero con su enunciado verdadero |

**Barrido de confirmación.** Se recorrieron las veinte ocurrencias vivas de la cadena `INV-04` en 02 y 03: ninguna la asocia al texto conservado íntegro. Las tres del catálogo de 03 (`ESPECIE_DE_OBSERVACION_DESCONOCIDA`, `ENVIO_FUERA_DE_BORRADOR`, `ENVIO_SIN_INTERPRETACION`) la asocian a RN-05, que es correcto. Las cinco ocurrencias erróneas restantes viven **sólo** en `_legacy/2026-08-09/`, donde corresponde que sigan.

**RN-08 declara explícitamente que no tiene invariante** en tres lugares vivos: `RN-08` §2, `CU-06` §9 y `CU-07` §9, más las dos tablas de correspondencia. La corrección está completa.

---

## 5. Verificación mecánica del catálogo de errores

Se extrajeron los identificadores de condición de la §6 de los once casos de uso **sin leer previamente el recuento del catálogo**, y recién después se contrastaron contra `DX-Error-Messages.md` §3 y §6.2.

### 5.1 Extracción por caso de uso

| CU | Filas en su §6 | Identificadores |
| --- | --- | --- |
| CU-01 | 4 | `DATO_OBLIGATORIO_AUSENTE`, `UNICIDAD_DE_CORREO_NO_VERIFICADA`, `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`, `ESTADO_INICIAL_NO_NEGOCIABLE` |
| CU-02 | 3 | `TRANSICION_DE_CUENTA_NO_ADMITIDA`, `BAJA_SIN_ARRASTRE_DE_TRABAJOS`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` |
| CU-03 | 4 | `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`, `CREDENCIAL_YA_FIJADA`, `CREDENCIAL_VIGENTE_NO_VERIFICADA`, `VALOR_DERIVADO_VACIO` |
| CU-04 | 3 | `CUENTA_PENDIENTE`, `CUENTA_BLOQUEADA`, `CREDENCIAL_NO_ESTABLECIDA` |
| CU-05 | 4 | `TRABAJO_SIN_DUENO`, `DATO_OBLIGATORIO_AUSENTE` *(2.ª aparición)*, `REEDICION_FUERA_DE_BORRADOR`, `TEXTO_ORIGINAL_ALTERADO` |
| CU-06 | 4 | `POSICION_DE_PIEZA_NO_CONTIGUA`, `TIPO_DE_PIEZA_DESCONOCIDO`, `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`, `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` |
| CU-07 | 4 | `ESPECIE_DE_OBSERVACION_DESCONOCIDA`, `ERROR_SIN_UBICACION`, `ADVERTENCIA_SIN_LOS_DOS_VALORES`, `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` |
| CU-08 | 4 | `ENVIO_FUERA_DE_BORRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `ENVIO_SIN_INTERPRETACION`, `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` |
| CU-09 | 3 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `OPERACION_DESCONOCIDA` |
| CU-10 | 4 | `DESENLACE_FUERA_DE_PENDIENTE`, `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL` *(2.ª aparición)*, `DESENLACE_DESCONOCIDO` |
| CU-11 | 3 | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR`, `OPERACION_DESCONOCIDA` *(2.ª aparición)* |
| **Total** | **40 filas** | **37 identificadores distintos** |

### 5.2 Resultado del contraste

| Magnitud | Valor obtenido por esta auditoría | Valor que declara `DX-Error-Messages.md` §6.1 | Coincide |
| --- | --- | --- | --- |
| Filas de condición en la §6 de los once CU | 40 | 40 | ✔ |
| Condiciones declaradas en dos CU cada una | 3: `DATO_OBLIGATORIO_AUSENTE` (CU-01, CU-05), `TRANSICION_DESDE_ESTADO_TERMINAL` (CU-08, CU-10), `OPERACION_DESCONOCIDA` (CU-09, CU-11) | 3, exactamente esas | ✔ |
| Condiciones distintas | **37** | **37** | ✔ |
| Entradas del catálogo §3 sin fila en ninguna §6 (**inventadas**) | **0** | 0 | ✔ |
| Filas de §6 sin entrada en el catálogo (**faltantes**) | **0** | 0 | ✔ |
| Filas de la tabla de cobertura §6.2 | 37, una por identificador distinto, sin repetidos | — | ✔ |

**Verificación adicional de la taxonomía.** El recuento por categoría de §2.1 —17 entrada inválida, 3 recurso ausente, 13 conflicto de estado, 4 conflicto de facultad— se recontó entrada por entrada sobre §3 y da exactamente esos valores, con suma 37. La categoría agregada «conflicto de facultad» está declarada y justificada en §2.1, y las dos categorías vacías de la enumeración de referencia —error transitorio y error interno— se declaran vacías con motivo en §2.2 en lugar de omitirse, que es la conducta correcta.

**Verificación del falso faltante.** `RECONSTRUCCION_SOBRE_TRABAJO_FINALIZADO` aparece únicamente en `_legacy/2026-08-09/CU-06-…-v1.0.md` y en las dos notas que lo declaran (`DX-Error-Messages.md` §6.1 y el control de cambios de `CU-06` 1.1). No es una condición faltante y el catálogo hizo bien en dejar constancia.

**Único desvío detectado en el barrido de identificadores del árbol vivo.** Un barrido de todas las cadenas en mayúsculas con guion bajo sobre 02 y 03 devuelve 38 identificadores vivos, no 37. El trigésimo octavo es `TRANSICION_DE_TRABAJO_NO_ADMITIDA`, declarado en `RN-05` §4 y en ningún caso de uso. Ver **P2-02**.

**Cobertura de reglas e invariantes por el catálogo.** La tabla §6.2 alcanza a las once reglas (RN-01 a RN-11, todas presentes en la columna) y a los siete invariantes (INV-01 a INV-07, todos presentes). Dos atribuciones de esa columna son discutibles: ver **P3-02**.

---

## 6. Coherencia cross-doc y gobierno del glosario

### 6.1 Coherencia entre 03 y los once casos de uso de 02

| Afirmación de 03 | Verificación contra 02 | Resultado |
| --- | --- | --- |
| «Once casos de uso» y «once reglas» (`README.md` §2, `DX-Developer-Experience.md` §8) | 11 archivos `CU-XX`, 11 archivos `RN-XX` | ✔ |
| «37 condiciones derivadas una por una de la §6 de los once CU» | Recontado en §5 de este informe | ✔ |
| «Siete invariantes y once reglas de negocio» (`DX-Developer-Experience.md` §1.2) | `Definicion-Modelo-De-Dominio.md` §4.1 y §4.2 | ✔ |
| Correspondencia regla ↔ invariante de `Guia-Onboarding-Developer.md` §4.1 | Idéntica a `Definicion-Modelo-De-Dominio.md` §4.2 en las once filas | ✔ · la columna «Dónde se ejerce» difiere para tres reglas sin invariante: **P3-05** |
| «Cuatro reglas sin invariante: RN-07, RN-08, RN-09 por comportamiento y RN-11 por alcance de consulta» (`DX-Developer-Experience.md` §2, `Guia-Onboarding-Developer.md` §4.2) | Idéntico al intake §17.1.P.2 y a 02 | ✔ |
| Las dos máquinas de estado resumidas en `Guia-Onboarding-Developer.md` §3.4 | Coinciden con `Definicion-Modelo-De-Dominio.md` §5.1 y §5.2, incluidas las transiciones inadmisibles | ✔ |
| CA-01 y CA-02 de CU-01 transcriptos en `Guia-Onboarding-Developer.md` §3.2 | Coinciden literalmente con `CU-01` §8 | ✔ |
| Causa de `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`: «una posición que la reconstrucción no adoptó» | Contradice `CU-07` CA-03 sobre el escenario E-5 | **P1-01** |
| «Lo que el administrador sí puede hacer es eliminar el trabajo, por CU-11» (`DX-Error-Messages.md` §3.10) | Correcto en 03, **incorrecto en `CU-10` §5 FA-02**, que dice CU-09 | **P2-01** |
| Dos puntos abiertos citados sin reabrirlos (`README.md` §5, `Guia-Onboarding-Developer.md` §5) | Coinciden con `Especificacion-Funcional.md` §9 | ✔ |

### 6.2 El circuito de revisión, verificado extremo a extremo

| Propiedad exigida | Dónde se sostiene | Contradicciones halladas |
| --- | --- | --- |
| `Rechazado` es terminal | `Definicion-Modelo-De-Dominio.md` §5.2 (diagrama y tabla), INV-07, `RN-10` §1 y §3, `CU-08` FA-03 y CA-05, `CU-10` FA-02 y FA-03, `CU-09` CA-03 | Ninguna |
| Envío como acción única, con `Borrador` = «el texto no verificó» | `CU-05` §1 y FA-02, `CU-08` §1 y §10, `Definicion-Modelo-De-Dominio.md` §5.2 propiedad 1, `RN-05` §2, `Guia-Onboarding-Developer.md` §3.4 | Ninguna. `CU-05` §10 declara además que no existe «guardar sin enviar» |
| Aprobar y rechazar, facultad exclusiva del administrador | `RN-10` §1, `CU-10` §3 y CA-03, `CU-08` §6 (`DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO`), `DX-Error-Messages.md` §3.8 y §3.10 | Ninguna |
| El administrador no ve borradores pero elimina cualquier trabajo que ve | `RN-11` §1, `RN-04` §1 y §3, `CU-11` §4, FA-01 a FA-03 y CA-01 a CA-05, `Definicion-Modelo-De-Dominio.md` §5.2 | Ninguna. `CU-11` CA-05 verifica los 3 de 3 estados |
| Comentario opcional, a lo sumo uno por trabajo | `Definicion-Modelo-De-Dominio.md` §2.2 y §3 (cardinalidad 0..1), `CU-10` FA-01, CA-01 y CA-02, `RN-10` §3 | Ninguna |
| **INV-03 acotado a la eliminación por parte de un alumno** | `Definicion-Modelo-De-Dominio.md` §4.1 (enunciado) y su nota de §4.1 in fine; `RN-04` §2; `CU-09` §10; `CU-11` §9 («INV-03, por complemento: el recorte … es lo que deja lugar a este alcance») | **Ninguna. El recorte está intacto en los cuatro lugares.** No aparece en ningún artefacto vivo la formulación «un trabajo sólo se elimina en `Borrador`», que el borrado del administrador volvería falsa |

### 6.3 La distinción de tres términos

| Término | Definición emitida | Dónde se declara la distinción | ¿Algún artefacto los mezcla? |
| --- | --- | --- | --- |
| Condición de error del dominio | Guarda que impide una operación ilegítima del consumidor; una por invocación rechazada; no se guarda | `DX-Error-Messages.md` §1.2 (tabla de tres filas), `Glosario-UX.md` §2 y §3.1 | No |
| Observación | Entidad con dos especies, varias filas por trabajo, emitida al interpretar el texto del alumno | `Definicion-Modelo-De-Dominio.md` §2.5 y §3 (cardinalidad 0..N), `Glosario-Funcional.md` §3.4, `Guia-Onboarding-Developer.md` §3.4 | No |
| Comentario | Texto libre del administrador, a lo sumo uno por trabajo, no es calificación | `Definicion-Modelo-De-Dominio.md` §2.2 y §3 (cardinalidad 0..1), `CU-07` §10, `Glosario-Funcional.md` §3.4, `Glosario-UX.md` §3.1 in fine | No |

La distinción está declarada en **seis artefactos** y ninguno la contamina. `DX-Error-Messages.md` §1.2 va más lejos y resuelve el caso difícil en los dos sentidos: un trabajo que vuelve en `Borrador` por un error de validación **no** produce condición de catálogo, y `ERROR_SIN_UBICACION` / `ADVERTENCIA_SIN_LOS_DOS_VALORES` **sí** son condiciones aunque hablen de observaciones. Es el tratamiento correcto.

### 6.4 Los once casos de uso frente a la guía de `Rules-Especificacion-Funcional.md` §5.2

La guía es orientativa —«library con menos de diez»— y la propia regla declara en §2.2 que «el mínimo es piso, no techo. La cota superior queda definida por la cobertura completa de las NB-XX declaradas en 01». `Especificacion-Funcional.md` §6 declara el apartamiento con su causa: el alcance del producto creció, `01-Necesidades-Negocio` 1.1 emitió `NB-00009` y pasó de 22 a 27 los casos de uso previstos a nivel producto, verificado contra `Necesidades-Negocio.md` §5.3 y su control de cambios.

**Evaluación de si algún caso de uso debió fusionarse**, uno por uno, contra el criterio «¿es un sub-flujo de otro?» de §5.2:

| Par candidato | ¿Debió fusionarse? | Fundamento |
| --- | --- | --- |
| CU-01 y CU-02 | No | Sujetos y momentos distintos: constitución por el registro, gobierno posterior por el administrador. Sus §6 no comparten ninguna condición |
| CU-03 y CU-04 | No | CU-03 muta la credencial, CU-04 es una consulta que no modifica nada. Formas de terminación distintas: rechazo contra motivo de resultado |
| CU-05 y CU-08 | No | La partición es la que el modelo de estados obliga: la constitución no decide estado y el envío sí. Fusionarlos reintroduciría el «guardar sin enviar» que F-22 eliminó |
| CU-06 y CU-07 | No | Trazan a NB-00004 y NB-00005 con métricas distintas; es la misma partición que 01 §3.2 ya justificó |
| CU-08 y CU-10 | No | Los tres criterios de partición se sostienen: sujetos distintos (alumno que envía / administrador que decide), reglas distintas (RN-05 / RN-10) y momentos distintos. Además sus §6 no comparten ninguna condición salvo `TRANSICION_DESDE_ESTADO_TERMINAL`, que el catálogo trata como entrada única |
| CU-09 y CU-11 | No | Las reglas que los gobiernan son opuestas —al alumno lo acota la pertenencia y el borrador; al administrador, todo menos el borrador— y sus tablas de condiciones son disjuntas salvo `OPERACION_DESCONOCIDA` |
| Las cuatro operaciones de cuenta dentro de CU-02 | Fusión **correcta**, no partición faltante | NB-00001 §5 las trata como un conjunto único de cobertura |
| Aprobar y rechazar dentro de CU-10 | Fusión **correcta** | Mismo acto con dos desenlaces, misma precondición, mismo comentario, misma terminalidad |

**Conclusión: la justificación se sostiene y no hay casos de uso que debieran fusionarse.** El apartamiento está declarado con su causa, como pide el anti-patrón de §4.5 sobre numeración y recorte. No es hallazgo.

### 6.5 Gobierno del glosario — `Vocabulario-Rules.md` §10

**Los cuatro criterios de §10 sobre desambiguación léxica, más el criterio negativo:**

| Criterio de §10 | Resultado en 02 | Resultado en 03 |
| --- | --- | --- |
| Todo término que la fase acuña y aparece en más de un artefacto está declarado en el glosario de su categoría | **Cumple.** Los 14 términos de `Glosario-Funcional.md` §2 declaran su columna «artefactos de 02 donde aparece», y todos aparecen en dos o más | **Cumple.** Los 16 términos de `Glosario-UX.md` §2 con la misma columna |
| Todo término con más de un referente dentro de la fase tiene entrada de glosario o forma calificada en todas las ocurrencias que colisionan | **Cumple con una salvedad**: «trabajo», «pieza» y `Pendiente` tienen entrada con sus referentes y su evidencia de colisión (§3.1 a §3.3). Ver **P3-10** sobre «rol» | **Cumple.** «Error» con tres referentes y «mensaje» con dos, cada uno con su evidencia de colisión por sección (§3.1 y §3.2) |
| Ninguna forma desnuda de una familia calificada queda sin resolver en una sección que se despacha por separado (§9.2) | **Cumple.** Barrido de las 37 ocurrencias vivas de `Pendiente`: todas caen dentro de las tres excepciones declaradas o llevan su calificador | **Cumple.** Barrido de «error» en los cinco artefactos: **cero** ocurrencias de la forma desnuda con referente de dominio; las 100 % restantes son «condición de error», «error de validación», «error transitorio», «error interno», «código de error», «catálogo de errores» o «tasa de error» |
| Toda invariante de desambiguación declarada cita la verificación de colisión que la justifica (§9.4) | **Cumple.** `Glosario-Funcional.md` §3 abre declarando que los tres se verificaron contra §9.1 y §9.2 y que «los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía» | **Cumple.** `Glosario-UX.md` §3 abre con la misma verificación y cierra con «ningún término se declara acá por analogía con otro» |
| **Criterio negativo**: ninguna polisemia con contextos disjuntos se reporta como defecto | **Este informe no reporta ninguna.** Ver la enumeración de §6.6 | ídem |

**Las tres capas y la regla de referenciar, no redefinir.** `Vision-Producto.md` §9 es el raíz; `Glosario-Funcional.md` §4 referencia 24 términos suyos sin redefinirlos; `Glosario-UX.md` §4 referencia 14 filas, del raíz y del funcional de 02, sin redefinirlos. Se verificó término por término que **ninguna entrada de §2 de ninguno de los dos glosarios pisa una definición aguas arriba**. El movimiento de 1.1 en `Glosario-Funcional.md` —devolver «estado del trabajo» al glosario raíz y dar de alta «desenlace» y «alcance del administrador», que sí acuña esta categoría— es exactamente lo que la regla de no duplicación pide.

**Decisiones cerradas que la fase debía respetar:**

| Decisión | Verificación |
| --- | --- |
| Los dos referentes de «pieza», con el desplegable siempre calificado | ✔ `Glosario-Funcional.md` §3.2 y `Glosario-UX.md` §4. Barrido: las ocurrencias del segundo referente en los artefactos vivos son «pieza pública» y «piezas desplegables» (`RN-07` §3, `RN-11` §3), siempre calificadas |
| «Trabajo» no es «unidad de entrega» | ✔ `Definicion-Modelo-De-Dominio.md` §2.2, `Glosario-Funcional.md` §3.1 y `Glosario-UX.md` §4 in fine, los tres remitiendo a `Vision-Producto.md` §9 y a `PRODUCT-INTAKE` §12.1 |
| «Observación» es el superordinado de «advertencia» y «error de validación» | ✔ `Definicion-Modelo-De-Dominio.md` §2.5, `Glosario-Funcional.md` §3.4, `Glosario-UX.md` §3.3 y `Guia-Onboarding-Developer.md` §3.4 |
| `Pendiente` siempre calificado, con sus excepciones declaradas | ✔ `Glosario-Funcional.md` §3.3 y `Glosario-UX.md` §3.3 |
| «Proyecto» a secas no se usa | ✔ Barrido sobre los treinta y un artefactos: no aparece «el proyecto» ni «los proyectos» designando unidad de compilación. Se usa «proyecto de código» sin excepción |

### 6.6 Polisemias evaluadas y **descartadas** — no son hallazgo

`Vocabulario-Rules.md` §10 declara que reportar una polisemia de contextos disjuntos es un defecto del informe de auditoría. Se enumeran las que se evaluaron y **por qué se descartaron**:

| Término | Referentes considerados | Por qué **no** es hallazgo |
| --- | --- | --- |
| **Observación** | Entidad del dominio / discrepancia de valor | **No es polisemia**: es hiperonimia. Un solo referente con dos especies, declarado como superordinado en cuatro artefactos. Corregirlo sería el falso positivo típico |
| **Comentario** | Texto del administrador / comentario tolerado dentro del texto del alumno | **Contextos disjuntos.** El segundo no aparece en ningún artefacto de 02 ni de 03: la tolerancia de formato vive en `GeometriaFactory-Infrastructure`. `Glosario-Funcional.md` §3.4 ya lo declara verificado y descartado |
| **Pendiente**, en enumeraciones de conjunto cerrado | «`Borrador`, `Pendiente`, `Finalizado` o `Rechazado`» | El atributo enunciado ya fija el referente. Excepción declarada por el proyecto de código y admitida por §9.1. **Exigir la calificación sería el falso positivo que el framework tipifica** |
| **Pendiente**, en filas de tabla de transición | `Definicion-Modelo-De-Dominio.md` §5.1 y §5.2 | El encabezado de la tabla ya fija el referente. Excepción declarada |
| **Pendiente**, en identificadores literales | `CUENTA_PENDIENTE`, `DESENLACE_FUERA_DE_PENDIENTE` | Identificadores del contrato, no prosa. Excepción declarada en `Glosario-Funcional.md` §3.3 y en `DX-Error-Messages.md` §4 |
| **Estado** | Estado de cabecera del documento («Propuesto») / estado de cuenta / estado del trabajo | **Contextos disjuntos.** El primero vive sólo en el bloque de metadatos y en la columna «Estado» de los catálogos de `Especificacion-Funcional.md` §3 y §4; los otros dos, en prosa de dominio y siempre en forma calificada («estado de cuenta», «estado del trabajo»), que `Glosario-Funcional.md` §2 y §3.3 declaran. No colisionan en la lectura |
| **Pieza**, forma desnuda | Figura del conjunto raíz / artefacto desplegable | La forma desnuda está **asignada** al referente del dominio por decisión cerrada aguas arriba, y el otro va siempre calificado. Es una familia calificada correctamente resuelta, no un defecto |
| **Migración** | — | No aparece en ningún artefacto de la fase. Sin objeto |
| **Guarda**, **rechazo**, **motivo de resultado** | — | Un solo referente cada uno, declarado en `Glosario-UX.md` §2 |
| **Mensaje**, en la columna del catálogo | Enunciado canónico / texto que lee una persona | Colisiona, y **está resuelto**: `Glosario-UX.md` §3.2 declara los dos referentes y la forma que corresponde a cada uno, admitiendo la forma desnuda sólo dentro de la columna cuyo encabezado fija el referente. No es hallazgo |

---

## 7. Hallazgos

### P0 — bloqueantes

**Ninguno.**

### P1 — altos

#### P1-01 · Contradicción sobre el escenario canónico E-5 entre CU-06, CU-07 y el catálogo

- **Archivos y secciones:**
  - `SDD/Docs/Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md` §5, FA-03.
  - `SDD/Docs/Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md` §6 y §8, CA-03.
  - `SDD/Docs/Proyectos/GeometriaFactory-Domain/03-UX-UI-DX/DX-Error-Messages.md` §3.7.
- **Evidencia:**
  - `CU-06` FA-03: «El resultado de la interpretación trae una pieza de tipo desconocido → **El dominio no adopta esa pieza** y deja constancia de que la posición correspondiente quedó sin reconstruir; la observación … se registra por CU-07».
  - `CU-07` §6: «`OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | La posición de pieza indicada **no existe en el conjunto de piezas del trabajo** | **Rechaza el conjunto**».
  - `CU-07` CA-03: «Given un trabajo con el texto del escenario **E-5** y su observación de tipo desconocido … Then el dominio **adopta 1 observación** de especie error de validación, **con posición de pieza 1** y campo `Tipo`».
  - `DX-Error-Messages.md` §3.7, causa probable de esa condición: «La observación referencia **una posición que la reconstrucción no adoptó**».
- **Por qué es un defecto:** en E-5 la pieza de posición 1 es exactamente la de tipo desconocido, y por FA-03 **no se adopta**. La causa que el catálogo declara para `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` —«una posición que la reconstrucción no adoptó»— la hace aplicable palabra por palabra al escenario de CA-03, que sin embargo exige que el dominio adopte esa observación. Las dos lecturas producen implementaciones incompatibles del escenario que el intake declara insignia de RN-09 (§20.E-5), y 08 va a escribir una prueba que falla contra la guarda o una guarda que hace fallar la prueba.
- **Recomendación:** fijar en `CU-06` FA-03 que la posición de una pieza no adoptada **queda reservada** en el conjunto —que es lo que la frase «deja constancia de que la posición correspondiente quedó sin reconstruir» insinúa sin declarar—, y reescribir la causa de `CU-07` §6 y de `DX-Error-Messages.md` §3.7 como «la posición indicada no pertenece al rango de posiciones del conjunto raíz interpretado», que es lo que la guarda quiere impedir. Verificar de paso la interacción con `POSICION_DE_PIEZA_NO_CONTIGUA`, cuya causa actual («posiciones repetidas o con huecos») queda ambigua sobre si mira el conjunto entregado o el adoptado.

### P2 — altos-medios

#### P2-01 · `CU-10` FA-02 remite la eliminación por el administrador a CU-09 en lugar de CU-11

- **Archivo y sección:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Resolver-El-Desenlace-Del-Trabajo.md` §5, fila FA-02.
- **Evidencia textual:** «No hay camino: los dos estados de cierre son terminales y de ellos no sale ninguna transición. El dominio rechaza. **Lo que el administrador sí puede hacer es eliminar el trabajo, que es CU-09**».
- **Por qué es un defecto:** CU-09 es «Resolver el acceso de un alumno a un trabajo» y su §6 devuelve `OPERACION_FUERA_DE_BORRADOR` para toda eliminación fuera del borrador; la eliminación por el administrador es CU-11, como declaran `CU-11` §1 y §4, `RN-04` §5 y el propio `DX-Error-Messages.md` §3.10 («Lo que el administrador sí puede hacer es eliminar el trabajo, **por CU-11**»). La remisión errónea aparece justo en el flujo alternativo que un integrador consulta cuando busca cómo retirar un trabajo terminal.
- **Recomendación:** reemplazar «CU-09» por «CU-11» en `CU-10` §5 FA-02 y subir la versión del documento con su fila de control de cambios.

#### P2-02 · `RN-05` §4 declara un código de rechazo que ningún caso de uso declara

- **Archivo y sección:** `.../02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md` §4, línea 48.
- **Evidencia textual:** «Lo que sí se rechaza, con el motivo **`TRANSICION_DE_TRABAJO_NO_ADMITIDA`**, es forzar el paso a estado `Pendiente` de un trabajo con errores de validación».
- **Por qué es un defecto:** ese identificador no figura en la §6 de ninguno de los once casos de uso ni en las 37 entradas del catálogo. `CU-08` §6, que es donde vive el envío, declara para ese mismo caso `ENVIO_FUERA_DE_BORRADOR` y `TRANSICION_DESDE_ESTADO_TERMINAL`. El código huérfano falsifica la afirmación central de `DX-Error-Messages.md` §6.3 —«la superficie pública que se documenta: **las 37 condiciones de error** de los once contratos de uso»— y de `README.md` §5 de 03 —«no hay ninguna inventada y **no falta ninguna**»—, y contradice el procedimiento que `Guia-Onboarding-Developer.md` §4.3 paso 4 fija: una condición aparece primero en el §6 de un caso de uso y **recién entonces** entra al catálogo.
- **Nota de contraste:** los otros diez archivos de regla citan exclusivamente códigos que sí existen en alguna §6; se verificaron los quince códigos citados en las once RN y catorce resuelven contra el catálogo. Es un desvío puntual, no un patrón.
- **Recomendación:** o bien reemplazar el código en `RN-05` §4 por el que `CU-08` §6 declara, o bien dar de alta la condición en `CU-08` §6 y en el catálogo, en ese orden. Si se elige la segunda vía, actualizar los recuentos de `DX-Error-Messages.md` §6.1, §6.3 y de `DX-Developer-Experience.md` §6.

### P3 — medios y bajos

#### P3-01 · Trazabilidad RN → CU no bidireccional en tres casos de uso

- **Archivos y secciones:** `CU-02` §9, `CU-05` §9, `CU-06` §9.
- **Evidencia:**
  - `CU-02` §9 declara «RN-01, RN-07», pero `RN-06` §5 lista CU-02 y `Especificacion-Funcional.md` §4 pone CU-02 en la fila de RN-06.
  - `CU-05` §9 declara «RN-08, RN-04», pero `RN-10` §5 lista CU-05 («en cuanto a que el contenido de un trabajo terminal tampoco cambia») y §4 del índice lo confirma.
  - `CU-06` §9 declara «RN-08, RN-09», pero `RN-10` §5 lista CU-06 y §4 del índice lo confirma.
- **Por qué es un defecto:** `Rules-Especificacion-Funcional.md` §3.3 exige revisión bidireccional y §5.3 pregunta «¿cada CU enumera las RN que lo restringen?». El sentido RN → CU está completo; el sentido CU → RN tiene tres huecos. En los tres, el invariante correspondiente **sí** figura en la fila «Invariantes» del mismo §9, de modo que la información no se pierde: el hueco es de la fila de reglas.
- **Recomendación:** agregar RN-06 a `CU-02` §9 y RN-10 a `CU-05` §9 y a `CU-06` §9, con la acotación de alcance que `RN-10` §5 ya redactó.

#### P3-02 · Dos atribuciones de regla del catálogo contradicen el ámbito que la propia regla declara

- **Archivo y sección:** `.../03-UX-UI-DX/DX-Error-Messages.md` §6.2.
- **Evidencia:**
  - Fila «`ADVERTENCIA_SIN_LOS_DOS_VALORES` | CU-07 | **RN-09** | — | Rechazo», mientras `RN-09` §3 declara: «**No se aplica** a las observaciones de especie advertencia de discrepancia de valor, que llevan su propia exigencia».
  - Fila «`POSICION_DE_PIEZA_NO_CONTIGUA` | CU-06 | **RN-09** | — | Rechazo», mientras la identidad posicional de la pieza no proviene de RN-09 sino de `PRODUCT-INTAKE` §17.1.P.11 punto 2, que es lo que `CU-06` §4 paso 3 y `Definicion-Modelo-De-Dominio.md` §6 citan.
- **Por qué es un defecto:** la columna de regla del catálogo es la que 06 y 08 van a usar para trazar pruebas a reglas. Atribuir a RN-09 una condición que RN-09 excluye de su ámbito produce una prueba mal trazada.
- **Nota:** el catálogo ya declara en §6.2 in fine que «las columnas con guion no son un vacío a completar» y que inventar una regla sería el defecto contrario. El remedio correcto es poner guion, no cambiar de regla.
- **Recomendación:** dejar la columna de regla en «—» para las dos filas, o remitir a la decisión pre-ADR del intake en el caso de `POSICION_DE_PIEZA_NO_CONTIGUA`.

#### P3-03 · `Guia-Onboarding-Developer.md` inserta una sección que desplaza a tres obligatorias, sin declararlo

- **Archivo:** `.../03-UX-UI-DX/Guia-Onboarding-Developer.md`, encabezados `## 4.` a `## 7.`
- **Evidencia:** `Rules-UX-UI-DX.md` §4.2.4 fija seis secciones en este orden: audiencia y prerrequisitos, instalación o acceso, primer ejemplo ejecutable, **diagnóstico de problemas frecuentes**, **próximos pasos**, **control de cambios**. El documento emitido inserta «## 4. Dónde va una regla nueva» y las tres últimas quedan en §5, §6 y §7.
- **Por qué es un defecto menor:** el contenido obligatorio está íntegro y la sección agregada es de alto valor —es el tramo de una hora que `DX-Developer-Experience.md` §2 declara verificable—. Lo que falta es la declaración: `Rules-UX-UI-DX.md` §4.1 dice que «las secciones obligatorias siguen siendo las que declara §4.2», y la regla de 03, a diferencia de `Rules-Especificacion-Funcional.md` §4.3, no habilita secciones adicionales de forma expresa.
- **Recomendación:** renumerar la sección agregada como §6 —después de «próximos pasos»— o declarar la adición y su motivo en `README.md` §4 de 03.

#### P3-04 · Los once casos de uso numeran §12 una sección opcional que la regla numera §17

- **Archivos:** los once `CU-XX`, encabezado `## 12. Compatibilidad de la superficie pública`.
- **Evidencia:** `Rules-Especificacion-Funcional.md` §4.3 asigna «**§17** Compatibilidad de versión pública, sólo para library» y reserva «**§12** Performance esperado del CU, sólo para rest-api, worker-service y mobile-app-maui».
- **Por qué es un defecto menor:** el contenido corresponde y el tipo D8 es el correcto; lo que no coincide es el número, y §12 está reservado para otro contenido en otros tipos. Un agente aguas abajo que busque «§12» por su significado normativo encuentra otra cosa. Las once secciones obligatorias no quedan desplazadas.
- **Recomendación:** renumerar a §17, o declarar la desviación en `Especificacion-Funcional.md` §8, que ya es el lugar donde esta categoría declara sus decisiones de numeración.

#### P3-04b · Nota sobre los dos nombres de archivo desactualizados a propósito — **no es incumplimiento**

Se evaluó expresamente, y **no se reporta como hallazgo de nomenclatura**:

- `RN-04-Eliminacion-Acotada-Al-Borrador.md`, cuyo enunciado se amplió al borrado del administrador.
- `RN-05-Finalizacion-Sin-Errores-De-Validacion.md`, cuyo corte se adelantó del cierre al envío.

**La decisión está bien fundada y bien declarada.** Está declarada en cuatro lugares: el control de cambios 1.1 de cada uno de los dos archivos («**El nombre del archivo se conserva** aunque el enunciado se amplió: otras categorías ya lo citan por esta ruta y renombrarlo rompería sus enlaces»), `Especificacion-Funcional.md` §8 punto 3 y `README.md` §3 in fine. El fundamento es verificable: `GeometriaFactory-Contracts` los cita por esa ruta. Y no viola ninguna regla de nomenclatura: `Rules-Especificacion-Funcional.md` §3.1 exige `RN-XX-<Nombre>.md` con slug Título-Con-Guiones —los dos lo cumplen— y §3.2 fija que el título lógico no lleva sufijo de versión, no que el slug deba reescribirse cuando el enunciado evoluciona. El H1 y el campo `Documento` de cada archivo llevan el enunciado vigente, que es donde el lector lo resuelve. Estabilidad de citación sobre estética de slug es la decisión correcta.

#### P3-05 · La columna «Dónde se ejerce» de `Guia-Onboarding-Developer.md` §4.1 recorta los CU afectados de tres reglas

- **Archivo y sección:** `.../03-UX-UI-DX/Guia-Onboarding-Developer.md` §4.1.
- **Evidencia:** la tabla da «RN-08 → CU-05», «RN-09 → CU-07» y «RN-11 → CU-11», mientras `RN-08` §5 lista CU-05, CU-06 y CU-07; `RN-09` §5 lista CU-07 y CU-06; y `RN-11` §5 lista CU-11 y CU-10.
- **Por qué es un defecto menor:** la columna no está rotulada como exhaustiva y para las siete reglas con invariante coincide exactamente con `Definicion-Modelo-De-Dominio.md` §4.1, que sí es una tabla de invariantes. El recorte alcanza sólo a las cuatro filas sin invariante, donde la columna cambia de significado sin avisar.
- **Recomendación:** completar las tres filas, o aclarar en el encabezado que para las reglas sin invariante la columna indica el caso de uso principal.

#### P3-06 · Menciones de herramienta concreta en 03, sin declarar la tensión entre dos criterios de la misma regla

- **Archivos:** `.../03-UX-UI-DX/DX-Developer-Experience.md` §3.1 y `.../03-UX-UI-DX/Guia-Onboarding-Developer.md` §2 y §3.2.
- **Evidencia:** `dotnet test tests/GeometriaFactory.Domain.Tests` (dos ocurrencias), `.devcontainer/` (dos ocurrencias), `GeometriaFactory.sln` (dos ocurrencias).
- **Por qué es un defecto menor:** `Rules-UX-UI-DX.md` §6 pide «sin menciones a stacks concretos» y, tres ítems antes, «un quick-start verificable con snippet **ejecutable y reproducible**». Los dos criterios tiran en direcciones opuestas y los documentos resolvieron la tensión con criterio —usan «entorno de desarrollo contenido» en lugar del nombre comercial, «solución de código» en lugar de la extensión, y `./scripts/build.sh` en lugar del comando de compilación—, pero el paso 3 del quick-start rompe la línea sin decir por qué. La categoría 02, por contraste, está enteramente libre de stack.
- **Recomendación:** declarar la tensión en `README.md` §4 de 03, o reescribir el paso 3 como invocación del guion del repositorio, que es lo que `PRODUCT-INTAKE` §16 declara y lo que `DX-Developer-Experience.md` §3.2 promete («los nombres de los scripts y las rutas salen de `PRODUCT-INTAKE` §16 y no se inventan acá»).

#### P3-07 · Las copias de `_legacy/` no llevan el estado `Superado` ni la nota a la versión vigente

- **Archivos:** los veinte de `02-Especificacion-Funcional/_legacy/2026-08-09/`, `Casos-De-Uso/_legacy/2026-08-09/` y `Reglas-De-Negocio/_legacy/2026-08-09/`.
- **Evidencia:** todos conservan `**Estado:** Propuesto` y ninguno lleva la nota inicial que apunte a la versión vigente. `Rules-Especificacion-Funcional.md` §3.5 punto 2 pide «estado `Superado` y una nota al inicio que apunte a la versión vigente».
- **Por qué se reporta con nivel bajo y con atribución explícita:** **no es un defecto de los subagentes auditados.** El encargo declara que los snapshots los tomó el orquestador, y el criterio verificado —que existan y que ningún subagente los haya tocado— **se cumple**: las veinte copias son fieles, con `Versión: 1.0` y `Fecha: 2026-08-08`, escritas antes que cualquier documento vivo. Además, `Master-Prompt.md` §5 prohíbe volver a tocar lo archivado, de modo que corregir el estado ahora colisionaría con esa prohibición. Se registra como desvío de la política de archivado a resolver en el paso de snapshot del orquestador, no en esta fase.
- **Recomendación:** que el orquestador escriba el estado `Superado` y la nota **en el momento del snapshot**, antes de que la copia quede intocable. No modificar las veinte copias existentes.

#### P3-08 · `CU-08` §10 remite la eliminación sólo a CU-09

- **Archivo y sección:** `.../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Gobernar-El-Estado-Del-Trabajo.md` §10, última viñeta.
- **Evidencia:** «La eliminación no es una transición de estado y no vive acá: **está en CU-09**».
- **Por qué es un defecto menor:** es cierto para la eliminación por el alumno e incompleto para la del administrador, que es CU-11. A diferencia de P2-01, acá el contexto de la viñeta es el envío por el alumno, de modo que la lectura no es falsa, sólo parcial.
- **Recomendación:** «está en CU-09 para el alumno y en CU-11 para el administrador».

#### P3-09 · `DX-Error-Messages.md` no presenta quick-start

- **Archivo:** `.../03-UX-UI-DX/DX-Error-Messages.md`.
- **Evidencia:** `Rules-UX-UI-DX.md` §6: «Cada `dx-` doc presenta un quick-start verificable con snippet ejecutable y reproducible». El catálogo no lo tiene; sí lo tienen `DX-Developer-Experience.md` §3.1 y `Guia-Onboarding-Developer.md` §2 y §3.2.
- **Por qué se reporta con nivel bajo:** la lectura literal del criterio no encaja con la naturaleza de un catálogo de referencia, y agregarle un quick-start sería peor que no tenerlo. Se reporta porque el criterio es aplicable a la variante y `README.md` §4 de 03, que declaró siete criterios no aplicables uno por uno, omitió éste.
- **Recomendación:** agregar la fila correspondiente a la tabla de criterios no aplicables de `README.md` §4, con el motivo. No modificar el catálogo.

#### P3-10 · «Rol» tiene dos referentes en la misma sección y el glosario sólo declara uno, como alias descartado

- **Archivos y secciones:** `Glosario-Funcional.md` §2, entrada «Papel»; §2 «Actores» de los once casos de uso.
- **Evidencia:**
  - `Glosario-Funcional.md` §2: «**Papel** | Atributo del alumno que vale `Alumno` o `Administrador` … | Sinónimos y alias: **"Rol" en las fuentes técnicas. Se usa "papel"**».
  - Los once casos de uso encabezan su tabla de actores con la columna «**Rol**», con el sentido de «función del actor en el caso de uso», que es el que `Rules-Especificacion-Funcional.md` §4.2 punto 2 prescribe.
  - Los dos referentes conviven **en la misma tabla y hasta en la misma celda**: `CU-10` §2, columna «Rol», celda «Solicita el desenlace, habiendo comprobado antes que quien lo pide tiene el **papel** `Administrador`».
- **Por qué es un defecto menor y no un falso positivo:** por `Vocabulario-Rules.md` §9.2 el contexto de lectura es la sección, y acá los dos referentes comparten sección. El glosario declara «Rol» únicamente como alias descartado del atributo del alumno, sin declarar el segundo referente, que está en uso activo en once artefactos. Es exactamente el caso que `Rules-Especificacion-Funcional.md` §6 describe: «todo término del dominio que aparece en más de un artefacto de 02 está declarado en el glosario, **con sus referentes cuando tiene más de uno**».
- **Atenuante:** el segundo referente viene impuesto por la propia regla, que fija el encabezado de la tabla de actores; el proyecto de código no lo eligió.
- **Recomendación:** una fila en `Glosario-Funcional.md` §3 que declare los dos referentes —«papel» para el atributo del alumno, «rol» sólo como encabezado normativo de la tabla de actores— y la forma que corresponde a cada uno. Es la forma más barata de la escalera de `Vocabulario-Rules.md` §9.3.

---

## 8. Puntos abiertos registrados, que no son hallazgo

Los dos que `Especificacion-Funcional.md` §9 declara. **No son ambigüedad de este proyecto de código y no se cuentan como defecto:**

| # | Punto abierto | Estado | Quién lo resuelve |
| --- | --- | --- | --- |
| PA-1 | **Nombres de tipos y de espacios de nombres.** Declarados abiertos por `PRODUCT-INTAKE` §17.1.P.11 in fine. La categoría los declara fuera de su alcance y nombra los conceptos en lenguaje de dominio | Abierto, no bloqueante. Citado sin reabrirse en `Definicion-Modelo-De-Dominio.md` §1, `Guia-Onboarding-Developer.md` §3.2 y §5, y `README.md` §5 de 03 | 05-Arquitectura-Tecnica y el punto de control de la etapa `a` |
| PA-2 | **Criterio con el que dos correos se consideran el mismo** —comparación literal o normalizada—, que RN-02 e INV-01 necesitan | Abierto, no bloqueante. Declarado en `RN-02` §3 («el dominio conserva el dato como lo recibe»), `Especificacion-Funcional.md` §9 y `Guia-Onboarding-Developer.md` §5 | 05-Arquitectura-Tecnica, junto con la capa que ejerce la verificación |

Se registra además, como observación favorable y no como hallazgo, que la emisión de 03 detectó y corrigió una divergencia real del `PRODUCT-MANIFEST` —`tiene_auth` en `Domain`, de false a true— con su análisis de impacto declarado en el control de cambios 1.1 del manifiesto y con la justificación de por qué no dispara el retroceso de fase de `Master-Prompt.md` §4. Es el comportamiento que el framework espera de un subagente que encuentra una inconsistencia aguas arriba.

---

## 9. Veredicto y condiciones para promover

### 9.1 Recuento

| Nivel | Cantidad | Identificadores |
| --- | --- | --- |
| **P0** bloqueante | **0** | — |
| **P1** alto | **1** | P1-01 |
| **P2** medio-alto | **2** | P2-01, P2-02 |
| **P3** bajo | **10** | P3-01, P3-02, P3-03, P3-04, P3-05, P3-06, P3-07, P3-08, P3-09, P3-10. **P3-04b no cuenta**: es una evaluación con resultado «no es hallazgo» |
| **Total** | **13** | — |

### 9.2 Veredicto

## **APROBADO CON OBSERVACIONES**

No hay ningún P0. Las tres verificaciones que el encargo señala como críticas dieron resultado limpio y se dejan explícitas:

1. **No hay reglas ni invariantes inventados.** Once reglas, siete invariantes, ninguno de más, ninguno de menos, todos coincidentes en sustancia con el intake 1.3. El subagente que en la emisión anterior se detuvo y preguntó en lugar de inventar no cambió de conducta al recibir los enunciados: los transcribió y declaró en el control de cambios de `RN-02` y de `RN-06` que la regla existía en la fuente pero el intake no la transcribía, «y esta categoría la había elevado como ambigüedad en su versión anterior **en lugar de inventarla**».
2. **La corrección de la atribución de INV-04 está completa en los seis lugares**, con RN-08 declarando explícitamente que no tiene invariante asociado y sin ninguna ocurrencia viva que asocie INV-04 al texto conservado íntegro.
3. **El catálogo de 37 condiciones se verificó mecánicamente y cierra**: 40 filas, 3 duplicadas, 37 distintas, cero inventadas y cero faltantes.

El circuito de revisión se sostiene sin contradicciones en el modelo de dominio, en los casos de uso y en las reglas, e **INV-03 conserva su recorte a la eliminación por parte de un alumno en los cuatro lugares donde aparece**. La distinción entre condición de error, observación y comentario está declarada en seis artefactos y ningún artefacto la mezcla. Los once casos de uso están justificados y ninguno debió fusionarse. Los dos nombres de archivo conservados son una decisión bien fundada y bien declarada, y no se reportan como incumplimiento.

### 9.3 Condiciones para promover a la fase siguiente

**Bloqueantes de la promoción: ninguna.** La fase puede promoverse.

**Condiciones de corrección antes de que 05 y 06 consuman estos artefactos** —porque las tres alimentan decisiones de implementación y de prueba:

1. **Resolver P1-01** fijando qué ocurre con la posición de una pieza no adoptada, y alinear `CU-06` FA-03, `CU-07` §6 y `DX-Error-Messages.md` §3.7. Es la única corrección que cambia una decisión de especificación, no una redacción.
2. **Corregir P2-01**, la remisión de `CU-10` FA-02 a CU-09.
3. **Resolver P2-02**, el código huérfano `TRANSICION_DE_TRABAJO_NO_ADMITIDA` de `RN-05` §4, en el orden que `Guia-Onboarding-Developer.md` §4.3 paso 4 fija: primero el caso de uso, después el catálogo.

**Recomendadas, sin bloquear:** los diez P3, agrupables en tres operaciones —completar las tres filas de trazabilidad de P3-01 y P3-05 y las dos atribuciones de P3-02; declarar en los README las tres desviaciones de forma de P3-03, P3-04, P3-06 y P3-09; y agregar la entrada de glosario de P3-10—. P3-07 no se corrige en esta fase: su remedio pertenece al paso de snapshot del orquestador.

Los documentos que se corrijan suben versión menor con su fila de control de cambios, según `Master-Prompt.md` §5. Ninguna corrección exige archivar de nuevo: las once son de redacción o de completitud de tabla, salvo P1-01, que sí cambia el enunciado de una guarda y por eso conviene tratarla como cambio de contenido con su entrada explícita.

---

**Fin del informe B-02-03-GeometriaFactory-Domain-r1.**
