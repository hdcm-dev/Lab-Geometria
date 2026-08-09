# Auditoría B · 02-03 · GeometriaFactory-Contracts · ronda 3

| Campo | Valor |
| --- | --- |
| Fase | B — Especificación por proyecto de código |
| Producto | Fábrica de Geometría |
| Proyecto de código | GeometriaFactory-Contracts (`GeometriaFactory.Contracts`, `tipo_proyecto_codigo` = `library`) |
| Alcance auditado | Categoría **02-Especificacion-Funcional** (10 artefactos: `Especificacion-Funcional.md`, `Glosario-Funcional.md`, `README.md` y `CU-01` a `CU-07`) y categoría **03-UX-UI-DX** (5 artefactos: `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `Glosario-UX.md`, `README.md`). Total: **15 artefactos vivos** más **14 snapshots** en `_legacy/2026-08-09/` |
| Motivo de la ronda | **No es una verificación de correcciones.** Las rondas 1 y 2 cerraron con APROBADO CON OBSERVACIONES y sus dieciocho hallazgos están cerrados. Esta ronda verifica la **absorción de un cambio de alcance**: el Product Owner incorporó el circuito de revisión del administrador, el `PRODUCT-INTAKE` pasó a 1.3, `00-Contexto` y `01-Necesidades-Negocio` a 1.1 con `NB-09` nueva, y este proyecto de código —que es el que transporta todo lo que cambió— subió sus diez artefactos preexistentes a 1.1 y emitió `CU-07` en 1.0 |
| Fuera de alcance | La categoría 04 (omitida por gating, `usa_llm` == false): su ausencia **no es hallazgo**. `GeometriaFactory-Domain` se consultó **sólo** para verificar que los enlaces resuelvan; no se audita ni se modifica acá |
| Auditor | Arquitecto de Soluciones + QA Senior (invocación desde cero, sin participación en la generación ni en las rondas 1 y 2) |
| Fecha | 2026-08-09 |
| Informes anteriores | `B-02-03-GeometriaFactory-Contracts-r1.md`, `B-02-03-GeometriaFactory-Contracts-r2.md` (leídos, **no modificados**) |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Tabla de absorción del cambio de alcance](#2-tabla-de-absorción-del-cambio-de-alcance)
- [3. Verificación mecánica del catálogo de códigos](#3-verificación-mecánica-del-catálogo-de-códigos)
  - [3.1 Identificadores extraídos de las §6 de los siete casos de uso](#31-identificadores-extraídos-de-las-6-de-los-siete-casos-de-uso)
  - [3.2 Contraste contra el catálogo de 03](#32-contraste-contra-el-catálogo-de-03)
  - [3.3 Identificadores reciclados](#33-identificadores-reciclados)
- [4. Conformidad D1-D9 y estructura](#4-conformidad-d1-d9-y-estructura)
  - [4.1 Verificaciones de forma de la pasada](#41-verificaciones-de-forma-de-la-pasada)
  - [4.2 Enlaces a GeometriaFactory-Domain](#42-enlaces-a-geometriafactory-domain)
  - [4.3 La corrección tardía de «finalizar»](#43-la-corrección-tardía-de-finalizar)
- [5. Coherencia cross-doc y gobierno del glosario](#5-coherencia-cross-doc-y-gobierno-del-glosario)
  - [5.1 Los cuatro criterios de `Vocabulario-Rules.md` §10](#51-los-cuatro-criterios-de-vocabulario-rules-10)
  - [5.2 Polisemias evaluadas y descartadas](#52-polisemias-evaluadas-y-descartadas)
- [6. Evaluación de las decisiones de diseño declaradas](#6-evaluación-de-las-decisiones-de-diseño-declaradas)
- [7. Hallazgos](#7-hallazgos)
- [8. Veredicto y condiciones para promover](#8-veredicto-y-condiciones-para-promover)
- [9. Lo que esta auditoría no reporta](#9-lo-que-esta-auditoría-no-reporta)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Resumen ejecutivo

La absorción del cambio de alcance es **materialmente completa y correcta**: los catorce códigos del conjunto cerrado se verificaron uno por uno contra las §6 de los siete casos de uso, el identificador retirado no fue reciclado, las tres señales declaradas están en sus tres §6.1, `CU-07` está emitido con las once secciones obligatorias más la §17, el comentario del administrador queda separado de las observaciones en los cuatro planos y en los cinco artefactos que lo tocan, y las veintisiete previsiones de 01 están mapeadas sin huecos.

Esta ronda levanta **siete hallazgos: ningún P0, un P1, cuatro P2 y dos P3**. Todos son de la misma familia y ninguno toca una decisión de contrato: la actualización **propagó el contenido pero no terminó de propagar los conteos**. Quedaron once lugares que siguen diciendo «los seis casos de uso», «CU-01 a CU-06» o «los otros cinco» después de que el catálogo pasara a siete, y el más caro está en `CU-06`, que es el transversal y que en tres frases se declara compartido por «los seis» casos de uso mientras su propia §10 dice que el conjunto es la unión de **siete**. Se suma la evidencia de colisión de la polisemia «error» en `Glosario-UX.md`, que quedó apoyada en una entrada retirada, y la discrepancia con `NB-09` §5, que no está declarada en ningún artefacto.

**Veredicto: APROBADO CON OBSERVACIONES.** Ningún hallazgo es bloqueante y ninguno obliga a rehacer una decisión; todos se corrigen editando conteos y una evidencia, sin subir versión.

---

## 2. Tabla de absorción del cambio de alcance

Enum de `estado`: `absorbido` / `absorbido parcialmente` / `no absorbido`. Cada fila cita texto verificado en el archivo, con archivo y sección.

| # | Elemento del cambio | Estado | Evidencia textual verificada |
| --- | --- | --- | --- |
| 1 | Código nuevo `CONTRATO_ESTADO_NO_PERMITE_DESENLACE` | **absorbido** | `CU-07` §6, fila 1: «El trabajo no está en estado `Pendiente`: o nunca lo estuvo, o ya recibió su desenlace y está en un estado terminal». `CU-06` §6 lo declara en el conjunto y `CU-06` CA-06 lo verifica sobre un trabajo en estado `Rechazado` con «**0 campos** que sugieran una forma de revertirlo». En 03: `DX-Error-Messages.md` §3.2 `DXT-14`, categoría «Conflicto de estado», texto neutro «El trabajo no está en condiciones de recibir una decisión.» |
| 2 | Código nuevo `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | **absorbido** | `CU-07` §6 fila 2 y CA-04: «**0 desenlaces ejecutados por un alumno**». `CU-06` §6 lo declara. En 03: `DXT-15`, única entrada de la categoría nueva, con la acción «**No convertirlo en un texto que sugiera reintentar ni pedir permiso**: no hay elevación de facultad en este producto, los dos papeles son fijos» |
| 3 | Retiro de `CONTRATO_TEXTO_NO_INTERPRETABLE` del conjunto cerrado | **absorbido** | `CU-06` §10: «**Por qué salió `CONTRATO_TEXTO_NO_INTERPRETABLE`.** Con el envío como acción única de guardado, un texto que no verifica ya no hace fallar ninguna operación». `CU-03` §6.1 y `CU-05` §6.1 lo alojan y se remiten mutuamente. En 03: fila de retiro `~~DXT-09~~` con categoría «**Retirado del conjunto cerrado**» y las tres celdas de contenido en «—» |
| 4 | `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` con enunciado acotado al camino del alumno | **absorbido** | `CU-03` §6: «**El alumno** pide eliminar un trabajo suyo que no está en estado `Borrador`. Enunciado revisado: … y **sólo se produce en el camino del alumno**». `DXT-08` lo reproduce con la misma acotación y `CU-03` CA-07 verifica el reverso: la misma solicitud, para el administrador, procede |
| 5 | `CONTRATO_TRABAJO_NO_ENCONTRADO` con causa ampliada al borrador que el administrador no ve | **absorbido** | `CU-06` §6: «Recurso inexistente, ajeno, o fuera de lo que el solicitante ve —como un trabajo en estado `Borrador` pedido por el administrador—». `CU-07` CA-06 lo verifica «con el mismo texto que produce un identificador inexistente». `DXT-07`: «Tres causas con una sola respuesta … 0 campos permiten distinguirlas» |
| 6 | Tres señales declaradas que no son error (listado vacío + texto que no verifica en dos contratos de uso) | **absorbido** | Tres §6.1 verificadas: `CU-04` §6.1 (`CONTRATO_LISTADO_VACIO`), `CU-03` §6.1 y `CU-05` §6.1 (`CONTRATO_TEXTO_NO_INTERPRETABLE`, al enviar y al pedir el detalle). `CU-06` §10: «**Tres señales declaradas** quedan deliberadamente fuera del conjunto, en las subsecciones §6.1 de CU-03, CU-04 y CU-05». En 03: `DX-Error-Messages.md` §3.3 con `DXT-N1`, `DXT-N2` y `DXT-N3`, y el criterio de no renumerar declarado en su encabezado |
| 7 | Caso de uso nuevo `CU-07`, contrato del desenlace | **absorbido** | Archivo emitido en 1.0 / 2026-08-09, con las once secciones obligatorias más §17, seis criterios Given/When/Then con valores concretos, tres flujos alternativos y cinco filas de error. `Especificacion-Funcional.md` §3 lo cataloga como séptimo y declara «Siete casos de uso, sobre el mínimo de cinco que `Rules-Especificacion-Funcional.md` §2.2 fija para `library`» |
| 8 | Recorte: aprobar y rechazar **no** se separan en dos casos de uso | **absorbido** | Doble declaración coherente. `Especificacion-Funcional.md` §3.1: «Comparten tipo de solicitud, resultado, precondición, errores y regla de dominio: se distinguen sólo por el valor de un campo de conjunto cerrado». `CU-07` §10, primer punto, agrega el precedente: «que es el criterio de fusión que `Especificacion-Funcional.md` §3.1 ya aplicó al listado propio y al de la comisión». Materialmente sostenido por FA-01, que sólo cambia el valor del campo, y por CA-05, que fija el conjunto en **2 valores** |
| 9 | Recorte: la eliminación por el administrador se absorbe en `CU-03` como flujo alternativo | **absorbido** | `CU-03` FA-04: «El contrato usa **la misma** solicitud de eliminación de FA-02, con el mismo campo único. Lo que cambia no es el tipo sino la regla que lo acota». `CU-07` FA-03 cierra el reenvío desde el otro lado: «la eliminación por el administrador es la solicitud de eliminación de CU-03, FA-04». `Especificacion-Funcional.md` §3.1 y §4.2 (`P·CU-27`) coinciden |
| 10 | Código único para los dos caminos de eliminación, con el fundamento de no filtración | **absorbido** | Documentado en las **dos** categorías con el mismo razonamiento y sin contradicción. `CU-03` §6, párrafo posterior a la tabla: «Al administrador **no lo acota ningún estado** … un trabajo en estado `Borrador` le resulta indistinguible de uno inexistente … sí agregaría superficie donde el contrato puede filtrar información sobre recursos que el solicitante no debería saber que existen». `DX-Error-Messages.md` §3.2, nota final: mismo argumento, anclado a `RT-01` y `RT-02` y con remite explícito a `CU-03` §6 |
| 11 | Campo de comentario del administrador, y su distinción de las observaciones elevada a restricción transversal | **absorbido** | `RT-09` en `Especificacion-Funcional.md` §6: «viaja en el detalle como bloque propio y **nunca** como elemento de la colección de observaciones: no comparten ni un campo». Los cuatro planos están en `CU-05` §10 —cardinalidad, origen, forma, ubicación— y se verifican en CA-08. Ningún artefacto los mezcla: `CU-04` §10 lo excluye del listado, `DXC-06` y `DXC-11` lo rechazan en revisión, `DXC-12` rechaza volverlo calificación, `DX-Error-Messages.md` §4 lo prohíbe como regla de voz y `Guia-Onboarding-Developer.md` §1.3 y §3.1 pregunta 5 lo enseñan. Barrido propio: **0 ocurrencias** donde el comentario aparezca dentro de la colección de observaciones |
| 12 | Categoría de error nueva «facultad no habilitada» | **absorbido** | `DX-Error-Messages.md` §2.2, fila propia y párrafo de fundamento: «Meterla en “entrada inválida” diría que la solicitud está mal, que no lo está; meterla en “conflicto de estado” diría que el trabajo está en el estado equivocado, que tampoco». Declara además la premisa correcta: «`Rules-UX-UI-DX.md` §4.2.5 enumera cinco categorías **a título de ejemplo**». Cobertura verificada: 3+2+6+1+1+1 = **14**, sin huecos y sin superposición |
| 13 | Tabla de correspondencia con 01: de 22 a **27** previsiones, con `NB-09` nueva | **absorbido** | `Especificacion-Funcional.md` §4.2: barrido propio cuenta **27 filas** y los identificadores `P·CU-01` a `P·CU-27` aparecen **exactamente una vez cada uno** en la tabla. Ninguna queda colgando: veintiséis tienen caso de uso local y `P·CU-21` «verificar el acceso al laboratorio desde la red de la facultad» se declara **Ninguno. No toca el contrato**, con destino explícito «Verificación de campo y despliegue: 09», que es fundamento correcto para un ensamblado de tipos sin comportamiento |
| 14 | Transversalidad de `CU-06` extendida al caso de uso nuevo | **absorbido parcialmente** | El contenido está: `CU-06` §6 declara los dos códigos nuevos y §10 dice «Es la unión de los que declaran los **siete** casos de uso». Pero §1, §2 y §10 del mismo archivo siguen diciendo «los otros **cinco** lo referencian» y «el conjunto de códigos que los **seis** casos de uso usan». Ver hallazgo **H-01** |

---

## 3. Verificación mecánica del catálogo de códigos

Extracción propia por barrido sobre las §6 (y §6.1) de los siete casos de uso vivos, sin apoyarse en ningún conteo declarado.

### 3.1 Identificadores extraídos de las §6 de los siete casos de uso

| Caso de uso | Códigos de error declarados en su §6 | §6.1 (señal, no error) |
| --- | --- | --- |
| `CU-01` | `CREDENCIAL_INVALIDA`, `CUENTA_NO_HABILITADA`, `CONTRASENA_NO_ESTABLECIDA`, `CAMPO_REQUERIDO_AUSENTE`, `SERVICIO_NO_DISPONIBLE` (5) | — |
| `CU-02` | `CAMPO_REQUERIDO_AUSENTE`, `CORREO_YA_REGISTRADO`, `CONFIRMACION_NO_COINCIDE`, `CREDENCIAL_INVALIDA`, `ADMINISTRADOR_YA_CONFIGURADO`, `SERVICIO_NO_DISPONIBLE` (6) | — |
| `CU-03` | `CAMPO_REQUERIDO_AUSENTE`, `TRABAJO_NO_ENCONTRADO`, `ESTADO_NO_PERMITE_ELIMINAR`, `SERVICIO_NO_DISPONIBLE` (4) | `TEXTO_NO_INTERPRETABLE` |
| `CU-04` | `ALUMNO_NO_ENCONTRADO`, `SERVICIO_NO_DISPONIBLE` (2) | `LISTADO_VACIO` |
| `CU-05` | `TRABAJO_NO_ENCONTRADO`, `SERVICIO_NO_DISPONIBLE` (2) | `TEXTO_NO_INTERPRETABLE` |
| `CU-06` | `CAMPO_REQUERIDO_AUSENTE`, `TRABAJO_NO_ENCONTRADO`, `ESTADO_NO_PERMITE_DESENLACE`, `DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR`, `CONTRASENA_NO_ESTABLECIDA`, `SERVICIO_NO_DISPONIBLE`, `ERROR_NO_CLASIFICADO` (7) | — |
| `CU-07` | `ESTADO_NO_PERMITE_DESENLACE`, `DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR`, `TRABAJO_NO_ENCONTRADO`, `CAMPO_REQUERIDO_AUSENTE`, `SERVICIO_NO_DISPONIBLE` (5) | — |

**Unión de los siete: 14 códigos distintos.** Coincide con el conteo que `CU-06` §10 declara. `CONTRATO_ERROR_NO_CLASIFICADO` aparece sólo en `CU-06`, que es correcto: es el cierre del conjunto y no lo declara ninguna familia. `CONTRATO_TEXTO_NO_INTERPRETABLE` **no aparece en ninguna §6**, sólo en §6.1: el retiro es efectivo y no quedó residuo.

### 3.2 Contraste contra el catálogo de 03

`DX-Error-Messages.md` §3.2 declara «**Catorce entradas de código** … más una **fila de retiro**». Contraste uno a uno:

| Código del conjunto (02) | Entrada en 03 | Categoría asignada | Coincide |
| --- | --- | --- | --- |
| `CAMPO_REQUERIDO_AUSENTE` | `DXT-01` | Entrada inválida | Sí |
| `CREDENCIAL_INVALIDA` | `DXT-02` | Entrada inválida | Sí |
| `CUENTA_NO_HABILITADA` | `DXT-03` | Conflicto de estado | Sí |
| `CORREO_YA_REGISTRADO` | `DXT-04` | Conflicto de estado | Sí |
| `CONFIRMACION_NO_COINCIDE` | `DXT-05` | Entrada inválida | Sí |
| `ADMINISTRADOR_YA_CONFIGURADO` | `DXT-06` | Conflicto de estado | Sí |
| `TRABAJO_NO_ENCONTRADO` | `DXT-07` | Recurso ausente | Sí |
| `ESTADO_NO_PERMITE_ELIMINAR` | `DXT-08` | Conflicto de estado | Sí |
| `ALUMNO_NO_ENCONTRADO` | `DXT-10` | Recurso ausente | Sí |
| `SERVICIO_NO_DISPONIBLE` | `DXT-11` | Error transitorio | Sí |
| `ERROR_NO_CLASIFICADO` | `DXT-12` | Error interno | Sí |
| `CONTRASENA_NO_ESTABLECIDA` | `DXT-13` | Conflicto de estado | Sí |
| `ESTADO_NO_PERMITE_DESENLACE` | `DXT-14` | Conflicto de estado | Sí |
| `DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` | `DXT-15` | Facultad no habilitada | Sí |

**Resultado del contraste: 14 de 14, sin sobrantes y sin faltantes.** Ningún código de 02 falta en 03 y ninguna entrada de 03 nombra un código que 02 no declare. Las tres señales están fuera del conjunto y catalogadas aparte en §3.3, con `DXT-N2` y `DXT-N3` apuntando al mismo código en sus dos contratos de uso.

Las seis categorías de §2.2 suman exactamente 14 y cada código aparece en una sola: 3 (entrada inválida) + 2 (recurso ausente) + 6 (conflicto de estado) + 1 (facultad no habilitada) + 1 (transitorio) + 1 (interno).

### 3.3 Identificadores reciclados

**No hay ninguno.** `DXT-09` aparece en la tabla como fila tachada, con categoría «Retirado del conjunto cerrado» y sin código sustituto; los identificadores nuevos entraron **al final** de la serie, como `DXT-14` y `DXT-15`, aunque pertenezcan a familias anteriores. La regla está escrita antes de la tabla: «El identificador de un código que sale del conjunto **no se reasigna a otro** … Reciclarlo haría que una cita anterior apuntara en silencio a un código distinto, que es peor que un hueco visible». La misma disciplina se aplicó a las señales: «`DXT-N2` conserva el referente que ya tenía —el detalle— y la señal nueva del envío entra como `DXT-N3`, en lugar de correr los identificadores». Verificado por barrido: **0 ocurrencias** de `DXT-09` asociadas a un código distinto de `CONTRATO_TEXTO_NO_INTERPRETABLE`.

Lo mismo vale en 02: `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` conservó su identificador y cambió su enunciado, en lugar de emitir uno nuevo, y eso está declarado en el control de cambios de `CU-03`.

---

## 4. Conformidad D1-D9 y estructura

Sólo donde el cambio la pueda haber alterado. No se reaudita lo que las rondas 1 y 2 verificaron y el cambio no toca.

| Dimensión | Estado | Verificación acotada al cambio |
| --- | --- | --- |
| **D1** trazabilidad a origen | Cumple | `CU-07` cabecera traza a `NB-09` §1 y §5, `NB-07` §5, `Vision-Producto.md` §9.1 y §9.2, `Alcance-Producto.md` §4.1 y ocho secciones del intake 1.3. Los diez artefactos actualizados citan `PRODUCT-INTAKE` **1.3** y el 01 en **1.1**. `RT-08` y `RT-09` declaran origen (`§4.2` y `§12`/`§4 F-21`) y punto de verificación |
| **D2** decisiones con fundamento | Cumple, por encima del piso | Los tres recortes nuevos de `Especificacion-Funcional.md` §3.1 traen fundamento propio, no una fórmula. El código único para los dos caminos de eliminación tiene fundamento **doble y consistente** en 02 y 03. La categoría de error nueva funda por qué no fuerza ninguna de las cinco |
| **D3** ausencia de invención | Cumple | Ninguna capacidad, prioridad ni exclusión se origina acá. `CU-07` §10 último punto delimita: «La forma del punto de acceso de desenlace pertenece a `GeometriaFactory-Api`; quién puede ejercerlo se verifica en la pieza de datos. El contrato transporta el papel, no lo hace cumplir» |
| **D4** nomenclatura y versionado | Cumple | Ningún archivo vivo lleva sufijo de versión; los catorce snapshots lo llevan y viven en `_legacy/2026-08-09/`. Slugs sin mayúsculas irregulares, espacios ni acentos |
| **D5** consistencia interna | **Incumple parcialmente** | Es la dimensión donde caen los siete hallazgos: once conteos de casos de uso no se propagaron. Ver §7 |
| **D6** completitud estructural | Cumple | `CU-07` tiene las once secciones obligatorias de `Rules-Especificacion-Funcional.md` §4.2 más la §17 opcional de `library`, con el hueco §11→§17 declarado en su encabezado, igual que los otros seis. Seis criterios Given/When/Then con valores concretos (mínimo tres). Tabla de contenido presente |
| **D7** separación de responsabilidades | Cumple | El desenlace transporta; la transición y su exclusividad quedan en `RN-10` de Domain; el panel queda en `GeometriaFactory-Web`; el código de estado de la respuesta, en `GeometriaFactory-Api` |
| **D8** tipo de proyecto de código | Cumple | Sigue siendo `library`: siete casos de uso sobre el mínimo de cinco; `RN-XX` y modelo de datos omitidos con la celda de la regla que lo admite; **cero wireframes**, que es el mínimo de `Rules-UX-UI-DX.md` §2.2 para `library`; variante **DX** declarada en las cinco cabeceras de 03 |
| **D9** afirmaciones sobre el estado del sistema | Cumple, y con la salvedad de siempre | **No hay sistema construido**, de modo que D9 alcanza sólo a las afirmaciones sobre el estado. Las que aparecen están condicionadas y no afirmadas: el quick-start declara «Si la etapa `a` no está cerrada, este quick-start no aplica todavía»; el gate del 100 % de tipos ejercitados se rotula `[ASUNCIÓN]` con remite a §22 del intake en los cuatro lugares donde aparece; `Guia-Onboarding-Developer.md` §3.2 aclara que «en las etapas tempranas la cobertura es parcial por construcción y no por defecto» |

**Criterios de `Rules-Especificacion-Funcional.md` §6 revisados por el cambio:** cantidad de CU sobre el mínimo del tipo D8 (7 ≥ 5) ✔; once secciones por CU ✔; ≥3 Given/When/Then por CU ✔; glosario con las cinco secciones de §4.2.4 y tabla no vacía ✔; términos en más de un artefacto declarados ✔; sin redefinición del glosario raíz ✔; sin sufijo de versión en archivos vivos y `_legacy/` con sufijo ✔; tabla de contenido ✔.

**Criterios de `Rules-UX-UI-DX.md` §6 revisados por el cambio:** variante declarada en cada cabecera ✔; `DX-Developer-Experience.md` con las nueve secciones de §4.2.3, Diátaxis y tramos 5/30/60 ✔; quick-start reproducible en los tres documentos DX, con el mismo bloque sin variantes ✔; trazabilidad upstream/downstream por artefacto ✔; `Glosario-UX.md` no vacío y sin duplicar 02 con semántica distinta ✔; mínimo de wireframes **0** para `library`, cumplido y declarado ✔. Los seis criterios de variante UX/UI siguen declarados no aplicables con motivo, y el cambio no altera ningún flag (`tiene_ui_final`, `tiene_portal_developers`, `requiere_maqueta` siguen en false).

### 4.1 Verificaciones de forma de la pasada

| Verificación | Resultado |
| --- | --- |
| Los nueve documentos preexistentes de 02 en **1.1** con fecha 2026-08-09 | **Cumple.** `Especificacion-Funcional.md`, `Glosario-Funcional.md`, `README.md`, `CU-01` a `CU-06`: nueve archivos, los nueve en `Versión: 1.1` y `Fecha: 2026-08-09` |
| Los cinco documentos de 03 en **1.1** con fecha 2026-08-09 | **Cumple.** Los cinco verificados en cabecera |
| `CU-07` nace en **1.0** | **Cumple.** `Versión: 1.0`, `Fecha: 2026-08-09`, una sola fila de control de cambios que dice «Emisión inicial» |
| Snapshots en `_legacy/2026-08-09/` con sufijo `-v1.0.md` | **Cumple.** **14 snapshots**, uno por cada documento preexistente que cambió: seis en `Casos-De-Uso/_legacy/2026-08-09/`, tres en `02-Especificacion-Funcional/_legacy/2026-08-09/` y cinco en `03-UX-UI-DX/_legacy/2026-08-09/`. `CU-07` **no** tiene snapshot, que es lo correcto para un documento nuevo |
| Los snapshots los tomó el orquestador y ningún subagente los tocó | **Cumple.** Los catorce archivos tienen marca temporal **01:06:47**, dentro del mismo segundo y en una sola operación, y todos los archivos vivos son **posteriores** (01:10:39 a 01:34:29). Ningún snapshot fue reescrito después de tomarse |
| Los snapshots corresponden a documentos que efectivamente cambiaron | **Cumple.** Los catorce difieren de su archivo vivo; los dos casos de menor cambio, `CU-01` y `CU-02`, cambiaron su §9 y su §10 y lo declaran en su fila 1.1 con la frase «**Ningún tipo, campo ni criterio de aceptación de este contrato cambia**» |
| La corrección tardía se agregó a filas existentes, sin filas nuevas y sin subir versión | **Cumple.** Verificado en los cuatro documentos afectados: la frase «**Precisión de la misma intervención**» aparece **dentro** de la fila 1.1 de `Especificacion-Funcional.md`, `Glosario-Funcional.md`, `CU-03` y `CU-05`. Ninguna fila nueva y ninguna versión adicional |

### 4.2 Enlaces a `GeometriaFactory-Domain`

**Once enlaces relativos distintos, los once resuelven.** Verificación por resolución de ruta desde `02-Especificacion-Funcional/Casos-De-Uso/`, no por inspección visual:

`RN-01` ✔ `RN-02` ✔ `RN-03` ✔ `RN-04` ✔ `RN-05` ✔ `RN-06` ✔ `RN-07` ✔ `RN-08` ✔ `RN-09` ✔ `RN-10` ✔ `RN-11` ✔ — **11 de 11 existentes**, sobre 22 ocurrencias de enlace repartidas en los siete casos de uso (`CU-01` 3, `CU-02` 4, `CU-03` 4, `CU-04` 2, `CU-05` 3, `CU-06` 4, `CU-07` 2). Contra los siete de la versión anterior, entran `RN-02`, `RN-06`, `RN-10` y `RN-11`.

**Ningún archivo de `GeometriaFactory-Domain` fue modificado por esta pasada.** Las once reglas tienen marca temporal entre 00:55:15 y 00:59:30, **anteriores** al primer archivo tocado por esta pasada (el snapshot inicial a las 01:06:47). La pasada leyó y enlazó; no escribió.

**Los dos nombres desactualizados a propósito: decisión evaluada y correcta.** `RN-04-Eliminacion-Acotada-Al-Borrador.md` cubre hoy los dos caminos de eliminación y `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` corta hoy en el envío. **No lo reporto como incumplimiento de nomenclatura**, y el motivo es material y no de cortesía: la advertencia está declarada dos veces y en el lugar donde un lector aguas abajo la va a encontrar —`Especificacion-Funcional.md` §5 («Se cita el contenido vigente, no el que sugiere el nombre») y la propia celda de `CU-03` §9, junto a cada enlace («aunque el slug del archivo nombre sólo el primero», «aunque el slug del archivo diga “finalización”»)—; la decisión de no renombrar se tomó **aguas arriba**, no acá, y renombrar habría roto los once enlaces que esta sección acaba de verificar. El coste de la advertencia declarada es menor que el de la ruptura, y este proyecto de código no tiene autoridad para renombrar archivos de otro. Queda registrado para que una ronda posterior no lo levante.

### 4.3 La corrección tardía de «finalizar»

Barrido propio sobre los quince artefactos vivos buscando la raíz `finaliz` en cualquier forma. **No queda ninguna mención a «finalizar» como acción del alumno.** Las siete ocurrencias que existen son todas admisibles y las clasifico una por una:

| Ocurrencia | Clase | Admisible |
| --- | --- | --- |
| `CU-03` FA-03: «no hay una solicitud aparte de **finalización**, porque el envío es la única acción de guardado» | **Negación explícita declarada a propósito en un flujo alternativo** | Sí — es exactamente la excepción prevista |
| `CU-03` §9, dos veces: el slug `RN-05-Finalizacion-…` y su advertencia «aunque el slug del archivo diga “finalización”» | Cita de nombre de archivo ajeno con advertencia | Sí |
| `Especificacion-Funcional.md` §5: `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` **corta hoy en el envío** | Ídem | Sí |
| `CU-03` control de cambios, fila 1.0: «tipos de alta, edición, eliminación y **finalización**» | Fila histórica de control de cambios | Sí |
| `CU-03`, `CU-05`, `Glosario-Funcional.md`, `Especificacion-Funcional.md`, filas 1.1: las cuatro frases «**Precisión de la misma intervención**» que registran la corrección | Registro de la propia corrección | Sí |

El valor de estado `Finalizado` aparece en su forma de valor del conjunto cerrado, siempre entre comillas invertidas, y no se confunde con la acción. Los cinco lugares corregidos son verificables por su registro: §3.1 de `Especificacion-Funcional.md`, la fila de necesidad de negocio de `CU-03` §9, §6.1 y §17 de `CU-05`, y las glosas de «advertencia» y «error de validación» de `Glosario-Funcional.md` §4 —estas últimas dicen hoy «**impide que el trabajo pase a estado `Pendiente`**», que es el corte vigente—.

---

## 5. Coherencia cross-doc y gobierno del glosario

**03 se construyó sobre los siete casos de uso de 02.** Contraste de lo que 03 afirma contra lo que 02 especificó:

| Afirmación de 03 | Sostenida por 02 | Resultado |
| --- | --- | --- |
| «el conjunto cerrado pasa a **catorce** códigos» (`DX-Error-Messages.md` §2.2, §3.2; `DX-Developer-Experience.md` §5.1; los dos README) | `CU-06` §10 y §9 (US-16) | Coincide, y coincide con mi conteo independiente |
| «**tres** señales declaradas» (§3.3, §2.2, los dos README, `DX-Developer-Experience.md` §5.1) | Tres §6.1 en `CU-03`, `CU-04`, `CU-05`; `CU-06` §10 | Coincide |
| «**doce** entradas de construcción» (§3.1, `README.md` de 03 §1) | Derivadas de `RT-01` a `RT-09` de `Especificacion-Funcional.md` §6 | Coincide: la tabla trae `DXC-01` a `DXC-12` |
| «`DXC-03` … estado del trabajo —que ahora tiene **cuatro** valores—» | `CU-03` §3, `CU-04` §3, `CU-05` §3, `RT-08` | Coincide |
| «**siete** contratos de uso y **nueve** restricciones» (`README.md` de 03 §6, `DX-Developer-Experience.md` §4.1, `Guia-Onboarding-Developer.md` §5) | `Especificacion-Funcional.md` §3 y §6 | Coincide |
| «las seis familias de tipos de transferencia de `CU-01` a `CU-05` y `CU-07`, y el tipo único de error de `CU-06`» (`DX-Developer-Experience.md` §8) | Catálogo de §3 | Coincide |
| `Glosario-UX.md` cabecera y §1: «veintidós términos acuñados … **veinticuatro** referenciados» en 02 | Conteo propio sobre `Glosario-Funcional.md`: §2 = **22** filas, §4 = **24** filas | Coincide |
| `Glosario-UX.md` §4 «pasa de veintitrés a **treinta y una** entradas» | Conteo propio: **31** filas | Coincide |

**Conteos desactualizados fuera de las filas históricas de control de cambios: once, todos de la misma familia** —«seis casos de uso», «CU-01 a CU-06», «los otros cinco», «ocho filas» de la matriz que hoy tiene nueve, «veintidós previsiones» donde hoy hay veintisiete—. Son los hallazgos H-01 a H-04 y H-06. Las filas históricas de control de cambios que dicen «trece códigos», «doce códigos», «los seis casos de uso» o «tres preguntas de superficie pública» **no son hallazgo**: son registro de lo que la versión anterior decía y no se reescriben.

**Trazabilidad upstream/downstream.** Los quince artefactos declaran secciones concretas, no archivos enteros. El upstream incorpora `NB-09` en los siete lugares donde corresponde —`CU-04`, `CU-05`, `CU-06`, `CU-07`, `Especificacion-Funcional.md`, los dos README y los tres documentos DX— y siempre con sección: `NB-09` §1 y §5, y en `CU-05` con el criterio nominado («§5, sexto criterio»). El downstream sigue apuntando a `05`, `06`, `08`, `09`, `10` y `11` del mismo proyecto de código, con `US-17` a `US-20` nuevas previstas en `CU-04`, `CU-05` y `CU-07` y recogidas en la matriz de `Especificacion-Funcional.md` §4, que pasa a veinte historias.

### 5.1 Los cuatro criterios de `Vocabulario-Rules.md` §10

Las tres capas —`Vision-Producto.md` §9 raíz, `Glosario-Funcional.md` de 02 y `Glosario-UX.md` de 03— mantienen la regla de **referenciar, no redefinir**.

**Criterio 1 — todo término que la fase acuña y aparece en más de un artefacto está en el glosario de su categoría. Cumple.** Los tres términos nuevos que el cambio introdujo se dieron de alta en `Glosario-Funcional.md` §2 —«desenlace», «estado terminal» y «señal declarada que no es error»— y los tres aparecen efectivamente en más de un artefacto, verificado. `Glosario-UX.md` **no acuñó ninguno**, que es lo correcto: los cuatro términos que 03 usa y no tenía —desenlace, estado terminal, señal declarada, comentario— ya están declarados aguas arriba y entran en su §4 como referenciados.

**Criterio 2 — todo término con más de un referente tiene entrada de glosario o forma calificada en las ocurrencias que colisionan. Cumple.** La única polisemia nueva que el cambio introdujo en la cadena es **`Pendiente`**, y su tratamiento es el correcto por la escalera de `Vocabulario-Rules.md` §9.3: la resolución **ya estaba decidida aguas arriba** (`PRODUCT-INTAKE` §4.2, vinculante, recogida en `Vision-Producto.md` §9.2), de modo que `Glosario-Funcional.md` §3.3 la **referencia y la cumple** en lugar de volver a decidirla, y `Glosario-UX.md` ni siquiera abre entrada: la enumera en §4 como referenciada. La evidencia de colisión que §9.4 exige está y es la mejor posible: «este proyecto de código es el único del producto donde los dos referentes cruzan **el mismo contrato**». Barrido propio de formas desnudas: **ninguna en prosa**. Las que existen son menciones metalingüísticas del término —«tres términos con más de un referente … y `Pendiente`»— o enumeraciones del conjunto cerrado.

Las decisiones cerradas se respetan: los **tres** referentes de «contrato» en `Glosario-Funcional.md` §3.1 siguen siendo tres y con la misma forma asignada; los **dos** de «pieza» con el desplegable siempre calificado; «trabajo» **no** es «unidad de entrega», declarado en las dos entradas de §4 de los dos glosarios; «observación» sigue siendo superordinado de «advertencia» y «error de validación», y `DX-Error-Messages.md` §4 lo blinda como regla de voz: «“Observación” **sólo como superordinado** de las dos, nunca como sinónimo de ninguna».

**Criterio 3 — las excepciones declaradas de `Pendiente` no son hallazgo. Respetado.** Las dos —enumeraciones del conjunto cerrado e identificadores literales de código— están declaradas en `Glosario-Funcional.md` §3.3 con la cita correcta («Calificarlas sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica como defecto») y repetidas en `DX-Error-Messages.md` §4 y en el `README.md` de 03 §6. Esta auditoría **no las levanta**.

**Criterio 4 — criterio negativo: ninguna polisemia con contextos disjuntos se reporta como defecto. Respetado por los documentos y por este informe.** Los dos glosarios enuncian la prohibición antes de aplicarla y **no agregaron ninguna entrada de §3 por el cambio**, salvo la de `Pendiente`, que sí colisiona y está fundada. La fila de control de cambios de `Glosario-UX.md` lo declara explícitamente: «**No se acuñó ningún término nuevo en §2 y no se agregó ninguna entrada a §3**».

La única falla de gobierno que encuentro es de **evidencia**, no de decisión: la entrada de «error» de `Glosario-UX.md` §3.1 apoya su colisión en `DXT-09`, que en esta versión quedó retirado. Es el hallazgo **H-04**; no invalida la entrada, que sigue siendo correcta, pero deja la justificación apoyada en una fila que ya no describe un error transportado.

### 5.2 Polisemias evaluadas y descartadas

Reevaluadas contra el corpus **vigente**, incluido `CU-07`, para verificar que el cambio no las volvió colisionantes. Las ocho que `B-02-03-GeometriaFactory-Contracts-r1.md` §5.3 evaluó y descartó **siguen con contextos disjuntos y no se reportan**: **papel, estado, tipo, campo, detalle, superficie, trabajo, comisión**. Verificación material de las tres que el cambio podía haber alterado:

- **papel** — «papel en la pieza» sigue apareciendo **una sola vez** en todo el corpus, en `CU-05` §4 paso 3, y calificado. `CU-07` usa «papel» sólo en su referente de persona («una sesión de papel administrador»). Contextos disjuntos: se mantiene el descarte.
- **estado** — el cambio agregó «estado terminal» y «estado alcanzado», que son formas calificadas de la familia «estado del trabajo», y «situación de cuenta» sigue reservada para el otro referente por decisión explícita del glosario. No hay forma desnuda ambigua.
- **detalle** — el cambio agregó el bloque de comentario **dentro** del detalle, junto al «detalle de ubicación» del tipo de error. Los dos referentes siguen sin compartir sección: el detalle del trabajo vive en `CU-05` y el detalle de ubicación en `CU-06` §4. Se mantiene el descarte.

Exigir que cualquiera de las ocho se califique sería un defecto de este informe por el criterio negativo de `Vocabulario-Rules.md` §9.1 y §10.

---

## 6. Evaluación de las decisiones de diseño declaradas

Cuatro decisiones que el enunciado de la ronda pide evaluar, no sólo constatar.

**Los dos recortes de `CU-07` se sostienen, y por motivos distintos.** La fusión de aprobar y rechazar es la aplicación consistente de un criterio que la sección ya tenía: la unidad de recorte declarada en §3.1 es la **familia de tipos de transferencia**, no la capacidad `F-XX`, y dos decisiones que comparten tipo de solicitud, tipo de resultado, precondición, conjunto de errores y regla de dominio son una sola familia. La prueba de que el criterio se aplica y no se invoca está en que produce el resultado inverso donde corresponde: `CU-04` y `CU-05` se separaron —mismo dominio, familias distintas— y ahí el fundamento es opuesto. La absorción de la eliminación del administrador en `CU-03` FA-04 se sostiene por el mismo criterio y con un argumento verificable: **reutiliza el mismo tipo con el mismo campo único**, y lo que difiere —la regla que lo acota— vive en `GeometriaFactory-Domain` y este proyecto de código no la redacta. Un caso de uso nuevo habría declarado la misma superficie dos veces. Los dos recortes están además reflejados de forma bidireccional, con `CU-07` FA-03 remitiendo a `CU-03` FA-04 y `CU-03` §7 remitiendo a `CU-07`, de modo que ningún lector que entre por cualquiera de los dos se pierde el otro.

**El razonamiento del código único para los dos caminos de eliminación se sostiene, y está bien documentado en las dos categorías.** El argumento tiene dos mitades y las dos son verificables. La primera es de cobertura: al administrador no lo acota ningún estado, así que `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no puede producirse** en su camino, y lo único que lo acota —la visibilidad— produce un caso que `CONTRATO_TRABAJO_NO_ENCONTRADO` ya expresa exactamente. `CU-03` CA-03 y CA-07 lo verifican como par: la misma solicitud falla para el alumno y procede para el administrador. La segunda es de seguridad, y es la que hace que el argumento no sea sólo economía: un código por papel **agregaría superficie donde el contrato puede filtrar la existencia de recursos que el solicitante no debería conocer**, que es la clase de fuga que `RT-01` y `RT-02` cierran y que `CU-06` CA-05 verifica con «0 campos permiten distinguir los dos casos». Documentación: el fundamento completo está en `CU-03` §6 y **replicado, no resumido**, en `DX-Error-Messages.md` §3.2, que además explica por qué se escribió («Es la pregunta que más veces se va a querer reabrir al leer `DXT-07` y `DXT-08` juntos»), y `DXT-07` lo remata en su columna de acción: «**No agregar un texto que distinga las causas**». No hay contradicción entre las dos categorías.

**La categoría de error nueva está justificada y no forzaba ninguna de las cinco.** El caso es material: `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` se produce con la solicitud bien formada, el recurso existente y visible, y el estado correcto —`CU-07` CA-04 lo verifica sobre «un trabajo propio en estado `Pendiente`»—. «Entrada inválida» afirmaría un defecto de la solicitud que no existe; «conflicto de estado» afirmaría un estado equivocado que tampoco, y además lo confundiría con `DXT-14`, que sí es conflicto de estado y convive con él en el mismo caso de uso. Meterlo en «error interno» sería tratar como imprevisto lo que el contrato prevé. El apoyo normativo es correcto: `Rules-UX-UI-DX.md` §4.2.5 enumera las cinco **a título de ejemplo**, no como conjunto cerrado, y el documento lo cita así en lugar de pedir permiso. Agregar la categoría es la respuesta correcta; forzar una de las cinco habría producido una clasificación falsa que el diagnóstico accionable de esa entrada tendría que desmentir.

**La discrepancia con `NB-09` §5 sexto criterio es conciliable, pero está sin declarar.** El criterio de 01 dice «Trabajos con desenlace cuyo estado **y comentario** el alumno ve **en su propio listado**», y el contrato puso el estado en el listado y el comentario en el detalle. Verifiqué las dos puntas. La restricción que el contrato invoca es **preexistente y anterior a `NB-09`**: `RT-04` sale de `PRODUCT-INTAKE` §17.4 P.10 y existe desde la versión 1.0 de esta sección, con el propósito declarado de que el listado no arrastre texto libre de cada trabajo; `CU-04` §10 y CA-01 la sostienen y `DXC-06` la rechaza en revisión. Del otro lado, lo que `NB-09` mide es la **devolución visible**, y esa finalidad se satisface: el estado viaja en el listado y expresa el desenlace por sí solo —`CU-05` FA-04 lo dice explícitamente para el caso sin comentario—, y el comentario se alcanza abriendo el trabajo desde el mismo listado, sin segunda vía ni permiso adicional. **No hay contradicción real**: hay una diferencia entre el lugar literal que el criterio nombra y el lugar donde el contrato lo pone, resoluble reformulando «en su propio listado» como «desde su propio listado» en `NB-09`, que es intervención de 01 y no de esta sección. Lo que sí falta es la **declaración**: ningún artefacto de 02 ni de 03 nombra la divergencia, y `CU-05` cita en su cabecera «`NB-09` §5 (sexto criterio)» como upstream sin advertir que se aparta de su letra. Es el hallazgo **H-05**, clasificado **P2**.

---

## 7. Hallazgos

Siete hallazgos: **0 P0, 1 P1, 4 P2, 2 P3.**

### H-01 · P1 · `CU-06` sigue declarándose transversal a «los seis» casos de uso y excluye a `CU-07`

- **Archivo:** `02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md`
- **Secciones:** §1 (Propósito), §2 (Actores), §10 (Notas y supuestos, primer punto)
- **Evidencia:** §1: «Es el caso de uso transversal del ensamblado: **los otros cinco** lo referencian en lugar de declarar cada uno su propia forma de error». §2, fila «Ensamblado de contratos»: «Declara el tipo de error y el conjunto de códigos que **los seis casos de uso** usan». §10: «El tipo de error es el mismo para **los seis casos de uso** del ensamblado». La misma §10, cinco puntos más abajo, se contradice: «El conjunto cerrado tiene catorce códigos. Es la unión de los que declaran los **siete** casos de uso». Materialmente, `CU-07` §6 declara cinco filas que resuelven todas «Respuesta de error de CU-06», de modo que el texto excluye al caso de uso que más depende de él.
- **Por qué P1 y no P2:** es el caso de uso **transversal**, el que 05, 06 y 08 leen para derivar la superficie del tipo de error, y la afirmación no es un conteo decorativo sino el enunciado de su alcance. Un lector automatizado que reciba `CU-06` §2 como sección suelta —que es el modo de despacho declarado en `Vocabulario-Rules.md` §9.2— concluye que el desenlace no usa el tipo de error del ensamblado, que es falso, y que la superficie a cubrir es de seis familias y no de siete. Se suma que la contradicción es **interna al mismo archivo**, lo que impide resolverla por lectura del contexto.
- **Recomendación:** en §1 «los otros **seis** lo referencian»; en §2 y §10 «los **siete** casos de uso». Sin subir versión (documento en estado `Propuesto`, `Master-Prompt.md` §5), registrando la corrección en la fila 1.1 existente.

### H-02 · P2 · Seis conteos desactualizados en el índice maestro de 02

- **Archivo:** `02-Especificacion-Funcional/Especificacion-Funcional.md`
- **Secciones y evidencia:**
  1. §3.2, última línea: «La numeración de esta sección es contigua de `CU-01` a **`CU-06`**, sin huecos» — contradice el §3 inmediatamente anterior, que cataloga siete.
  2. §3.1, fila «Se hizo transversal CU-06»: «**Los cinco** casos de uso anteriores comparten sus caminos de error» — hoy son seis los que comparten.
  3. §5, primera línea: «La columna de reglas de negocio de §4 está vacía en las **ocho** filas» — la matriz de §4 tiene **nueve** filas desde que se agregó `NB-09`. El `README.md` §4 arrastra el mismo conteo.
  4. §6, `RT-05`, columna «Dónde se verifica»: «Precondición de **los seis** casos de uso» — `CU-07` §3 declara la misma precondición.
  5. §8, primer punto: «el ensamblado no tiene un concepto técnico central separable de **los seis** contratos de uso».
  6. §4.2, párrafo introductorio: «`Necesidades-Negocio.md` §5.3 previó **veintidós** casos de uso … la previsión de 01 se escribe acá con el prefijo `P·` —**`P·CU-01` a `P·CU-22`**—», y párrafo de cierre: «La correspondencia de **las veintidós** con el resto de los proyectos de código no se decide acá». La tabla que esos dos párrafos enmarcan trae **veintisiete** filas, `P·CU-01` a `P·CU-27`, y el propio cierre dice tres líneas antes «Veintiséis de **las veintisiete** previsiones».
- **Recomendación:** actualizar los seis a siete / seis / nueve / siete / siete / veintisiete y `P·CU-01` a `P·CU-27`. El punto 6 es el más urgente de los seis, porque el rango declarado del prefijo es lo que un lector usa para saber si un identificador `P·CU-24` pertenece a la serie.

### H-03 · P2 · Ocho referencias «CU-01 a CU-06» y «los seis casos de uso» en el glosario de 02

- **Archivo:** `02-Especificacion-Funcional/Glosario-Funcional.md`
- **Secciones:** §2 (columna «Artefactos de 02 donde aparece», seis filas), §3.1 (dos celdas), §3.2 (una celda)
- **Evidencia:** las entradas «Ensamblado de contratos», «Tipo de transferencia», «Consumidor del contrato», «Respuesta de error neutra», «Cambio incompatible de contrato» y «Despliegue conjunto» declaran su ámbito como «`Especificacion-Funcional.md`, **CU-01 a CU-06**», cuando los seis términos aparecen también en `CU-07` —verificado: `CU-07` §2 nombra el ensamblado de contratos y los dos consumidores, §6 remite cinco veces a la respuesta de error, §17 declara dos cambios incompatibles—. §3.1 dice «**los seis** casos de uso» dos veces y §3.2 «**CU-01 a CU-06**». La incoherencia es visible dentro del mismo documento: §3.3, agregada por esta misma pasada, sí escribe «CU-03, CU-04, CU-05, CU-06, **CU-07**», y las entradas nuevas «desenlace» y «estado terminal» también incluyen `CU-07`.
- **Por qué importa:** la columna de ámbito del glosario es lo que sostiene la regla de inclusión de `Rules-Especificacion-Funcional.md` §3.3 —«entra todo término que aparece en más de un artefacto»— y es lo que una ronda posterior usa para verificar completitud. Un ámbito que subdeclara los artefactos deja el criterio sin base verificable.
- **Recomendación:** sustituir por «CU-01 a CU-07» / «los siete casos de uso» en las ocho celdas, verificando cada una por ocurrencia y **sin sustitución global** (`Vocabulario-Rules.md` §9.5).

### H-04 · P2 · La evidencia de colisión de «error» en `Glosario-UX.md` se apoya en una entrada retirada

- **Archivo:** `03-UX-UI-DX/Glosario-UX.md`
- **Sección:** §3.1 «Error», columna «Dónde se lee» del primer referente y párrafo de evidencia
- **Evidencia:** el primer referente remite a «los detalles de ubicación de **`DXT-09`**», y el párrafo de evidencia dice «en `DX-Error-Messages.md` §3.2, la entrada **`DXT-09`** es un **error transportado** cuya causa es un **error de validación** del texto del alumno». Pero `DXT-09` es hoy la **fila de retiro**: su categoría es «Retirado del conjunto cerrado», su columna «Detalle de ubicación» dice «—» y su columna de acción dice «**No se usa como error.**». La evidencia describe un estado que la propia pasada eliminó.
- **Agravante de contexto:** `Vocabulario-Rules.md` §9.4 exige que toda invariante de desambiguación **cite la verificación de colisión que la justifica**, y la ronda 1 destacó esta evidencia como «la mejor de las tres». La entrada sigue siendo correcta —los tres referentes de «error» siguen colisionando— pero su justificación quedó apoyada en una fila que ya no la sostiene.
- **Recomendación:** reapuntar la evidencia a un par vigente. El más limpio es `DXT-N3` de §3.3, que es una **señal** cuya causa es un **error de validación**, contra `DXC-11` o `DXC-04` de §3.1, que son **errores de construcción**; o `DXT-14`, error transportado, contra la misma vecindad. La colisión sigue siendo verificable en la misma tabla y a pocas líneas.

### H-05 · P2 · La divergencia con `NB-09` §5 sexto criterio no está declarada en ningún artefacto

- **Archivos:** `02-Especificacion-Funcional/Casos-De-Uso/CU-04-Contrato-De-Listado-De-Trabajos.md` §10; `02-Especificacion-Funcional/Casos-De-Uso/CU-05-Contrato-De-Detalle-Del-Trabajo-Interpretado.md`, cabecera de trazabilidad
- **Evidencia:** `NB-09` §5, sexto criterio, mide «Trabajos con desenlace cuyo **estado y comentario** el alumno ve **en su propio listado**», con target 100 %. El contrato pone el estado en el listado y el comentario en el detalle: `CU-04` §10 dice «**El comentario del administrador no viaja en el listado.** El elemento transporta el estado … el texto del comentario viaja en el detalle de CU-05», y `RT-04` lo eleva a restricción transversal. La justificación del contrato es buena, pero **nombra la restricción y no el criterio del que se aparta**: en ninguno de los quince artefactos aparece una mención al sexto criterio de `NB-09` §5, ni una advertencia de divergencia, ni el registro de la elevación al Product Owner. `CU-05` va más lejos y cita en su cabecera «`NB-09` §1 y §5 (**sexto criterio**)» como upstream, lo que un lector aguas abajo interpreta como conformidad plena.
- **Clasificación de la discrepancia:** **conciliable, no contradicción real**, y **P2**. No es P1 porque la finalidad del criterio —que el alumno reciba una devolución visible— se satisface con el estado en el listado y el comentario a un clic, y porque la restricción que el contrato invoca es **preexistente** a `NB-09` y viene del intake, no una preferencia de esta sección. No es P3 porque, tal como está redactado, el criterio **no es verificable** contra el contrato vigente: 08 no puede derivar de él una prueba que pase, y una lectura literal en 05 o en `GeometriaFactory-Web` induciría a poblar el listado con texto libre, que es exactamente lo que `DXC-06` rechaza en revisión.
- **Recomendación:** que `CU-04` §10 agregue un párrafo que **nombre** el sexto criterio de `NB-09` §5, declare que el contrato lo satisface por la vía del estado en el listado más el comentario en el detalle, cite `RT-04` como la restricción preexistente que impide la lectura literal, y registre que la reformulación del criterio está elevada al Product Owner y sin decidir. Es tratamiento de advertencia declarada, no corrección de contrato: la decisión de reformular `NB-09` pertenece a 01 y este proyecto de código no la puede tomar. El mismo párrafo cierra la exposición de `CU-05`, que puede remitir a él.

### H-06 · P3 · Cuatro conteos desactualizados en el `README.md` de 02

- **Archivo:** `02-Especificacion-Funcional/README.md`
- **Secciones y evidencia:** §3 punto 1: «Sin su §2 y su §5, **los seis** casos de uso se leen como una lista de tipos»; §3 punto 2: «CU-06 … **los otros cinco** lo referencian en todos sus caminos de error» (mismo defecto que H-01, por arrastre); §4, fila de `Definicion-<Concepto-Central>.md`: «separable de **los seis** contratos de uso»; §4, línea de cierre: «la columna RN de la matriz del índice maestro esté vacía en sus **ocho** filas», hoy nueve. Se suma §5, nota de autoridad: «todo se deriva del `PRODUCT-INTAKE`, de `00-Contexto/` y de las **ocho** `NB-XX` de `01-Necesidades-Negocio/`», cuando hoy son **nueve** con `NB-09`.
- **Por qué P3 y no P2:** el mismo `README.md` corrigió lo que importa —su §1, su §2, su tabla de siete casos de uso, su orden de lectura con `CU-07` en el punto 6 y su nota de actor «el actor de **los siete** casos de uso»—, de modo que ningún lector queda sin la información correcta; lo que quedó son cinco frases de prosa auxiliar en un documento que es índice y no fuente normativa.
- **Recomendación:** actualizar los cinco.

### H-07 · P3 · Cuatro conteos desactualizados en los artefactos DX del recorrido

- **Archivos:** `03-UX-UI-DX/Guia-Onboarding-Developer.md` y `03-UX-UI-DX/DX-Developer-Experience.md`
- **Evidencia:**
  1. `Guia-Onboarding-Developer.md` §3.1, encabezado: «el recorrido de la superficie … se verifica contra **tres preguntas**», seguido de una tabla de **cinco** filas y del cierre «Si **las cinco** respuestas salieron». El propio control de cambios declara el paso de cuatro a cinco.
  2. §3.3, título de sección y entrada de la tabla de contenido: «**Tres** cambios de control», seguido de una lista de **cuatro** y del cierre «**Los cuatro** salieron bien».
  3. §3.3, cierre: «las **tres** rechazadas de la tabla **compilan igual**», cuando la tabla marca «Se rechaza» en **dos** filas —la 3 y la 4— y «Incompatible, aunque compile» en la 2. Las tres que compilan igual son las filas 2, 3 y 4, pero sólo dos son rechazadas: la frase mezcla dos clasificaciones que el propio ejercicio enseña a distinguir, que es el peor lugar para mezclarlas.
  4. `DX-Developer-Experience.md` §2, fila «1 hora», y §6, fila TTFV: «Clasificar correctamente **los tres** cambios de control de `Guia-Onboarding-Developer.md` §3.3». La misma §2, fila «30 minutos», sí dice «**las cinco** preguntas», de modo que el documento corrigió una remisión y no la otra. Como TTFV es una **métrica con objetivo declarado**, su definición operativa queda apuntando a un conjunto de tamaño equivocado.
  5. Menor, en `DX-Error-Messages.md` §2.2: «**Tres códigos** quedan fuera de las seis categorías», cuando son tres **entradas** y **dos** códigos —la propia §3.3 lo aclara dos párrafos después: «las otras dos son el mismo código»—.
- **Recomendación:** «cinco preguntas»; título y tabla de contenido a «**Cuatro** cambios de control»; «las **dos** rechazadas» o, mejor, «los **tres** últimos de la tabla compilan igual» si lo que se quiere decir es que ninguno da señal automática; «los **cuatro** cambios de control» en las dos remisiones de `DX-Developer-Experience.md`; y «tres **entradas**» en `DX-Error-Messages.md` §2.2.

---

## 8. Veredicto y condiciones para promover

> ## APROBADO CON OBSERVACIONES

**Ningún hallazgo es P0.** La absorción del cambio de alcance es sustantivamente correcta: los catorce códigos verificados uno por uno contra las siete §6, el retirado sin reciclar y con fila de retiro propia, las tres señales en sus tres §6.1, `CU-07` completo y con los dos recortes fundados, el comentario separado de las observaciones en los cuatro planos y sin una sola mezcla en quince artefactos, la categoría de error nueva justificada sobre una premisa correctamente leída, las veintisiete previsiones mapeadas sin huecos y con la única que queda fuera declarada con destino, los once enlaces a Domain resolviendo y ese proyecto de código sin tocar, los catorce snapshots tomados en una sola operación por el orquestador y nunca reescritos, y la corrección tardía aplicada en cinco lugares sin filas nuevas ni versión de más.

Lo que falta es propagación de conteos, y cae toda en D5. La pasada actualizó el contenido con cuidado y dejó atrás once frases que siguen describiendo un catálogo de seis casos de uso. El único que llega a P1 es el de `CU-06`, porque es el transversal, porque la contradicción es interna al archivo y porque el enunciado de su alcance es lo que las categorías 05, 06 y 08 leen para dimensionar la superficie del tipo de error.

**Condiciones para promover a la fase siguiente:**

1. **Bloqueantes de promoción: ninguna.** No hay P0 y `Master-Prompt.md` §10 no condiciona la promoción a la ausencia de P1.
2. **Antes de que 05 y 06 consuman esta sección** —y son los dos consumidores inmediatos— corregir **H-01**: es el único hallazgo que puede inducir una decisión equivocada aguas abajo, y son tres frases.
3. **En la misma intervención**, corregir H-02, H-03, H-04 y H-06, que son conteos y una evidencia. H-04 conviene resolverlo junto con H-02 y H-03 porque los tres son gobierno de glosario y se verifican de una sola pasada.
4. **H-05** requiere decisión ajena a esta sección. Lo que corresponde acá es **declarar la divergencia y la elevación**; la reformulación del sexto criterio de `NB-09` §5 pertenece a 01 y al Product Owner. La promoción no debería esperar esa decisión, pero 08 no puede derivar una prueba de ese criterio hasta que llegue, y conviene que quede anotado en el traspaso.
5. **Ninguna corrección sube versión.** Los quince artefactos están en estado `Propuesto` y `Master-Prompt.md` §5 admite absorber correcciones de auditoría sin subir versión, registrándolas en la fila de control de cambios que la pasada ya abrió. **No corresponde tomar snapshots nuevos**: los de `_legacy/2026-08-09/` ya archivan el estado 1.0 y estas correcciones no producen un 1.2.
6. **Ninguna corrección toca `GeometriaFactory-Domain`**, cuyos once archivos quedan tal como esta auditoría los encontró.

---

## 9. Lo que esta auditoría no reporta

Se deja constancia para que una ronda posterior no lo levante:

- **Las ocho polisemias con contextos disjuntos** que `B-02-03-GeometriaFactory-Contracts-r1.md` §5.3 evaluó y descartó —papel, estado, tipo, campo, detalle, superficie, trabajo, comisión—, reverificadas acá contra el corpus vigente incluido `CU-07`. Exigir que se califiquen sería un defecto de este informe por el criterio negativo de `Vocabulario-Rules.md` §9.1 y §10.
- **Las dos excepciones declaradas de la forma calificada de `Pendiente`**: las enumeraciones del conjunto cerrado y los identificadores literales de código.
- **La ausencia de la categoría 04**, omitida por gating (`usa_llm` == false).
- **Las once omisiones declaradas de artefactos** de las dos categorías —cuatro en 02, ocho en 03, con la salvedad de que `Definicion-<Concepto-Central>.md` está declarado como recomendación no seguida y no como omisión autorizada— y **el mínimo de cero wireframes** para `library`.
- **Los dos nombres de archivo desactualizados de `GeometriaFactory-Domain`** (`RN-04` y `RN-05`), evaluados en §4.2 y no reportados como incumplimiento de nomenclatura: la decisión de no renombrar se tomó aguas arriba, la advertencia está declarada dos veces y renombrar rompería los once enlaces.
- **El hueco de numeración entre §11 y §17** en los siete casos de uso, que es lo que `Rules-Especificacion-Funcional.md` §4.3 manda y está declarado con su motivo en el encabezado de cada §17, incluido el de `CU-07`.
- **Las filas históricas de control de cambios** que dicen «doce códigos», «trece códigos», «los seis casos de uso», «tres preguntas de superficie pública» o «nueve entradas `DXC-XX`»: son registro de lo que la versión anterior decía y no se reescriben.
- **El destinatario de la documentación DX** —mantenedor futuro y agente de construcción por etapas en lugar de integrador externo— y la ausencia de samples propios, las dos fundadas en `PRODUCT-INTAKE` §16.1.
- **El orden no numérico de `P·CU-23`** dentro de la tabla de §4.2, agrupado por `NB` en lugar de por identificador, y el de la fila `NB-09` antes de `NB-08` en §4.1: son elecciones de agrupación, no errores de contenido, y las veintisiete previsiones y las nueve necesidades están completas.

---

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Ronda 3 de auditoría de la Fase B de `GeometriaFactory-Contracts`, categorías 02 y 03, sobre quince artefactos vivos y catorce snapshots. A diferencia de las dos rondas anteriores, verifica la absorción de un cambio de alcance y no correcciones de auditoría: tabla de absorción de catorce elementos con evidencia textual, verificación mecánica del conjunto cerrado por extracción propia de las §6 de los siete casos de uso y contraste contra las quince entradas del catálogo de 03, verificación de no reciclado de identificadores, resolución de los once enlaces a `GeometriaFactory-Domain` y confirmación de que ese proyecto de código no fue tocado, verificación de forma de versiones, fechas y snapshots por marca temporal, barrido de la corrección tardía de «finalizar» con clasificación de sus siete ocurrencias admisibles, gobierno del glosario con los cuatro criterios y reverificación de las ocho polisemias descartadas, evaluación de los dos recortes de `CU-07`, del código único de eliminación, de la categoría de error nueva y de la discrepancia con `NB-09` §5, y siete hallazgos: ningún P0, un P1, cuatro P2 y dos P3. Veredicto: APROBADO CON OBSERVACIONES. | Arquitecto de Soluciones + QA Senior |
