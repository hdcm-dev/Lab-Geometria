# Informe de auditoría — Fase B · GeometriaFactory-Application · categorías 02 y 03 · ronda 2

**Producto:** Fábrica de Geometría
**Fase auditada:** B (02-Especificacion-Funcional y 03-UX-UI-DX)
**Unidad de entrega:** GeometriaFactory-Api
**Alcance de la ronda:** verificación de cierre de los catorce hallazgos de `B-02-03-GeometriaFactory-Application-r1.md` sobre los **dieciocho** documentos vivos de `SDD/Docs/Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/` —diecisiete de r1 más `CU-10`—, y verificación de que las correcciones no hayan degradado lo que r1 dio por válido. Contrastado contra D1-D9, §6 de `Rules-Especificacion-Funcional.md`, §6 de `Rules-UX-UI-DX.md`, `Vocabulario-Rules.md` §9 y §10, `Master-Prompt.md` §5, el upstream de nivel producto (00, 01), el intake 1.3, el manifiesto 1.1 y `GeometriaFactory-Domain/02-Especificacion-Funcional/`, **que también cambió y hoy tiene doce casos de uso**
**Auditor:** Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la generación, en la corrección ni en la ronda 1
**Fecha:** 2026-08-09
**Ronda:** 2

**Categoría 04:** omitida por gating (`usa_llm` == false). Su ausencia no es hallazgo y no se evalúa.

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Tabla de cierre de los hallazgos de r1](#2-tabla-de-cierre-de-los-hallazgos-de-r1)
- [3. Verificación mecánica del catálogo](#3-verificación-mecánica-del-catálogo)
- [4. Verificación de la partición del caso de uso y de la fidelidad de la orquestación](#4-verificación-de-la-partición-del-caso-de-uso-y-de-la-fidelidad-de-la-orquestación)
- [5. Gobierno del glosario](#5-gobierno-del-glosario)
- [6. Forma, y lo que r1 validó y no se degradó](#6-forma-y-lo-que-r1-validó-y-no-se-degradó)
- [7. Hallazgos nuevos](#7-hallazgos-nuevos)
- [8. Veredicto y condiciones para promover](#8-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

**Los catorce hallazgos de r1 están cerrados**, verificados uno por uno sobre el archivo y la sección que cada uno citaba. El P0 se resolvió por partición: `CU-01` quedó acotado al auto-registro del alumno y nació `CU-10`, la configuración del administrador, que orquesta el `CU-12` nuevo del dominio; la partición se sostiene, `CU-10` se numeró al final con la decisión declarada en tres lugares, y las dos citas indebidas —RN-01 e INV-05 como fundamento del estado inicial— están retiradas y sustituidas por la declaración expresa de que ninguna de las dos fundamenta estado alguno.

El recuento del catálogo se rehízo de forma independiente y **cuadra exacto**: 48 filas de condición en las §6 de los diez casos de uso, **34 condiciones distintas**, 9 repetidas con 14 reapariciones, 35 filas de tabla en §3 con una sola excedente, **diferencia simétrica vacía** contra el catálogo, 17 citas de motivo en las §5 sin ninguna condición nueva, y los **16** rechazos del dominio de la sección nueva §2.5 verificados como exactamente los dieciséis códigos de dominio sin correspondencia acá, ninguno de ellos contado como condición.

Se emiten **cuatro hallazgos nuevos, todos P3**: dos cabeceras que siguen diciendo «once casos de uso» del dominio, tres residuos de nomenclatura de la corrección de los sellos, un rechazo inalcanzable remitido a una §10 que no cubre su segundo camino, y un argumento auxiliar inexacto en la justificación de la divergencia de clasificación. **Ningún P0, ningún P1, ningún P2.**

**Veredicto: APROBADO CON OBSERVACIONES.**

---

## 2. Tabla de cierre de los hallazgos de r1

Enum: `cerrado` · `cerrado parcialmente` · `abierto` · `cerrado con defecto nuevo`.

| # | Nivel r1 | Enunciado abreviado | Estado | Evidencia textual verificada |
| --- | --- | --- | --- | --- |
| **H-01** | P0 | La cuenta del administrador se constituye habilitada en `CU-01`, que el dominio rechaza | **cerrado con defecto nuevo** (H-15, P3) | `CU-01` §1: «**Este caso de uso no constituye la cuenta del administrador.** El producto tiene **dos caminos de alta** con reglas opuestas … el auto-registro es su CU-01 y la configuración del administrador es su CU-12. Esta capa espeja esa partición». El FA-01 que constituía habilitada desapareció; el FA-01 vigente propaga `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` y remite a CU-10. §7 Éxito: «papel `Alumno`, sello de alta y estado `Pendiente`». CA-03 verifica la negativa, no la habilitación. §10: «RN-01 e INV-05 **no fundamentan ningún estado inicial**». Nace `CU-10`, 1.0, que orquesta `Domain CU-12`. Defecto nuevo: dos cabeceras conservan «once casos de uso» del dominio (H-15) |
| **H-02** | P1 | `ENVIO_FUERA_DE_BORRADOR` propagaba mal el motivo de los dos estados terminales | **cerrado** | `CU-05` §6 acota la causa: «`ENVIO_FUERA_DE_BORRADOR` \| Se envía un trabajo en estado `Pendiente`», y suma fila propia «`TRANSICION_DESDE_ESTADO_TERMINAL` \| Se envía un trabajo en `Finalizado` o en `Rechazado` \| … el invariante de terminalidad no los distingue entre sí». CA-07 lo ancla: «devuelve el motivo `TRANSICION_DESDE_ESTADO_TERMINAL`, y no `ENVIO_FUERA_DE_BORRADOR`». §10: «Esta capa no colapsa los dos motivos del envío». Contrastado contra `Domain CU-08` §6, cuyo `TRANSICION_DESDE_ESTADO_TERMINAL` es el motivo **único** para «cualquier transición sobre un trabajo en estado `Finalizado` o `Rechazado`» |
| **H-03** | P1 | No se aportaba la cantidad de figuras del conjunto raíz que el dominio exige | **cerrado** | Entra al contrato del puerto: `Especificacion-Funcional.md` §3, fila «Validación de figuras \| Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**». Viaja: `CU-05` §4 paso 3 (el puerto la devuelve) y paso 4 («incorpora al trabajo el conjunto de piezas … **y la cantidad de figuras del conjunto raíz**, que es el rango de posiciones válidas»). Declarada no derivable: §3 precondición, «no es derivable de las piezas adoptadas, que admiten huecos», y §3 del índice, ídem. Presente además en §2, §7, §9, CA-02 y §10. Contra el dominio: `Domain CU-06` §3 la exige como precondición y `Definicion-Modelo-De-Dominio.md:87` la declara atributo, con la nota de la posición reservada |
| **H-04** | P2 | El índice se contradecía sobre las once reglas | **cerrado** | `Especificacion-Funcional.md` §6 ya no afirma que dos reglas se ejerzan enteras en otra capa: «**Las once tienen tramo acá**, y en dos el tramo principal está en otra capa: RN-05 … y RN-09 … Las dos filas lo declaran». Verificado que las filas de RN-05 y RN-09 lo declaran en su celda («**con el tramo principal en el dominio**», «**con el tramo principal en el validador**») |
| **H-05** | P2 | El reloj atribuido a `CU-02`, que no lo menciona | **cerrado** | `Especificacion-Funcional.md` §3, fila del reloj: «CU-01, CU-03, CU-04, CU-05, CU-08, CU-10» — `CU-02` retirado. `CU-02` §10: «**Este caso de uso no consume el puerto de reloj.** … el modelo del dominio no declara una fecha de última modificación para esa entidad». Recontado: las cuatro filas de la tabla de puertos coinciden exactamente con las diez celdas «Puertos que consume» de las §9 |
| **H-06** | P2 | Fechas atribuidas a entidades que el modelo del dominio no declara | **cerrado con defecto nuevo** (H-16, P3) | Renombradas «sellos» y declaradas metadatos de orquestación: `Especificacion-Funcional.md` §3, «**Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa** … el modelo del dominio … **no declara** fecha de última modificación de la cuenta ni fecha de creación, de modificación o de desenlace del trabajo». **No se las da por declaradas en el modelo**, y la afirmación es exacta: `Definicion-Modelo-De-Dominio.md:67` declara «Fecha de alta \| … La provee el consumidor» y `:82` «Fecha \| … es dato del alumno, no del reloj», y nada más. Discrepancia elevada al Product Owner en §11. Réplicas en `CU-01` §10, `CU-03` §10, `CU-05` §2, `CU-08` §10 y `Glosario-Funcional.md` §2 («Metadato de orquestación»). Defecto nuevo: tres usos residuales de la nomenclatura anterior (H-16) |
| **H-07** | P2 | Dos reglas omitidas en trazabilidad | **cerrado** | `CU-08` §9 ahora declara RN-01, RN-05, RN-10, RN-11; `CU-02` §9 declara RN-01, RN-04, RN-06, RN-07. Verificado además el criterio completo en las once reglas: **toda regla que §6 declara ejercida en un CU figura en la §9 de ese CU**, incluido CU-10 (RN-01, RN-02, RN-06) |
| **H-08** | P3 | El preámbulo declaraba «una sola entrada» y `DATO_OBLIGATORIO_AUSENTE` tenía dos filas | **cerrado** | La fila de §3.4 pasó a nota de prosa: «**El dato obligatorio ausente en la carga.** `DATO_OBLIGATORIO_AUSENTE` tiene su entrada única en §3.1 y vuelve a declararse acá con otro alcance». El preámbulo de §3 es hoy exacto: «Nueve condiciones se declaran en más de un caso de uso. **Ocho conservan la misma causa** … La novena … lleva **fila completa en §3.1 y en §3.10** … 35 filas de tabla para 34 condiciones», recontado y confirmado |
| **H-09** | P3 | El control de cambios de `DX-Error-Messages.md` quedaba en §7 | **cerrado** | Numeración vigente: §1 Principios · §2 Taxonomía · §3 Catálogo · §4 Tono y voz · §5 Localización · **§6 Control de cambios** · §7 Cobertura y trazabilidad. Coincide con `Rules-UX-UI-DX.md` §4.2.5 y unifica la convención con `Guia-Onboarding-Developer.md`. Las referencias internas se actualizaron: `03/README.md:93` remite a «§7.2» y `:89` a «§7.4» |
| **H-10** | P3 | «Solución» a secas designando el agrupador de construcción | **cerrado** | `DX-Error-Messages.md:85`: «sus contratos son referencias de proyecto de código dentro de la misma **solución de código**». Barrido sobre los dieciocho documentos: **cero** ocurrencias de «solución» a secas con ese referente |
| **H-11** | P3 | «Repositorio» a secas en la única sección donde los dos referentes conviven | **cerrado** | `Guia-Onboarding-Developer.md` §4, primera fila: «Abrir **el repositorio de código** en el entorno contenido que él mismo define». La otra ocurrencia de la sección conserva la forma calificada del otro referente («se **entrega** al **puerto de repositorio**»). Las formas desnudas restantes siguen en secciones donde el referente de código no aparece, tal como r1 las evaluó y descartó |
| **H-12** | P3 | `CU-01` §3 hablaba de «los tres puertos» y consume dos | **cerrado** | `CU-01` §3: «**Los dos puertos que este caso de uso consume** —repositorio de cuentas y reloj del sistema— están provistos por la composición de raíz». `CU-10` §3 usa la misma forma |
| **H-13** | P3 | La equivalencia entre el motivo de facultad de esta capa y los dos del dominio no estaba declarada | **cerrado** | `Especificacion-Funcional.md` §4: «**Una sola negativa de facultad, y dos motivos del dominio detrás.** … esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio … Quien lea las dos capas no debe leer tres negativas de facultad donde hay una». Replicado en `DX-Error-Messages.md` §2.4 y §2.5 y en `DX-Developer-Experience.md` §1.4 |
| **H-14** | P3 | Rechazos del dominio sin camino de propagación declarado | **cerrado con defecto nuevo** (H-17, P3) | Tres tratamientos, todos declarados: (a) condiciones nuevas en §6 —`CU-05` suma `CONJUNTO_DE_PIEZAS_MAL_FORMADO`, `CU-04` suma `TEXTO_ORIGINAL_ALTERADO`, `CU-03` suma `CREDENCIAL_YA_FIJADA` y `VALOR_DERIVADO_VACIO`—; (b) inalcanzables por construcción nombrados en las §10 de `CU-01`, `CU-02`, `CU-04`, `CU-05` y `CU-09`; (c) la sección nueva `DX-Error-Messages.md` §2.5, que los reúne con sus tipos distinguidos y advierte que «**Un rechazo inalcanzable que aparece en ejecución es un defecto de esta capa, no del consumidor**». Defecto nuevo: la fila de `UNICIDAD_DE_CORREO_NO_VERIFICADA` remite a una §10 que sólo cubre uno de sus dos caminos (H-17) |

**Recuento:** 14 de 14 cerrados; 3 de ellos con un defecto nuevo de nivel P3 asociado a la propia corrección. Ninguno abierto ni cerrado parcialmente.

---

## 3. Verificación mecánica del catálogo

Recontado de forma independiente, extrayendo los identificadores de las §6 de los diez casos de uso y contrastándolos contra las tablas de §3 del catálogo, sin apoyarse en las cifras que los documentos declaran.

### 3.1 Identificadores extraídos de las §6

| CU | Filas en su §6 | Identificadores |
| --- | --- | --- |
| CU-01 | 5 | `CORREO_YA_REGISTRADO`, `DATO_OBLIGATORIO_AUSENTE`, `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA`, `ESTADO_INICIAL_NO_NEGOCIABLE`, `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` |
| CU-02 | 5 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CONFIRMACION_DE_BAJA_NO_COINCIDE`, `TRANSICION_DE_CUENTA_NO_ADMITIDA`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`, `CUENTA_INEXISTENTE` |
| CU-03 | 8 | `CUENTA_PENDIENTE`, `CUENTA_BLOQUEADA`, `CREDENCIAL_NO_ESTABLECIDA`, `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`, `CREDENCIAL_VIGENTE_NO_VERIFICADA`, `CREDENCIAL_YA_FIJADA`, `VALOR_DERIVADO_VACIO`, `CUENTA_INEXISTENTE` |
| CU-04 | 5 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `DATO_OBLIGATORIO_AUSENTE`, `TEXTO_ORIGINAL_ALTERADO`, `TRABAJO_SIN_DUENO` |
| CU-05 | 6 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `ENVIO_FUERA_DE_BORRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `INTERPRETACION_NO_DISPONIBLE`, `CONJUNTO_DE_PIEZAS_MAL_FORMADO`, `OBSERVACION_MAL_FORMADA` |
| CU-06 | 2 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `SOLICITANTE_NO_DECLARADO` |
| CU-07 | 3 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `TRABAJO_INEXISTENTE` |
| CU-08 | 5 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `DESENLACE_FUERA_DE_PENDIENTE`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `DESENLACE_DESCONOCIDO` |
| CU-09 | 4 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `PAPEL_NO_RECONOCIDO` |
| CU-10 | 5 | `ADMINISTRADOR_YA_CONFIGURADO`, `CORREO_YA_REGISTRADO`, `DATO_OBLIGATORIO_AUSENTE`, `CONFIGURACION_SIN_CREDENCIAL`, `ESTADO_INICIAL_NO_NEGOCIABLE` |
| **Total** | **48 filas** | **34 identificadores distintos** |

Las **nueve** condiciones declaradas en más de un caso de uso, con su multiplicidad: `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` (4), `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` (3), `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` (3), `DATO_OBLIGATORIO_AUSENTE` (3), `TRANSICION_DESDE_ESTADO_TERMINAL` (2), `OPERACION_FUERA_DE_BORRADOR` (2), `ESTADO_INICIAL_NO_NEGOCIABLE` (2), `CUENTA_INEXISTENTE` (2), `CORREO_YA_REGISTRADO` (2). Reapariciones = 48 − 34 = **14**. **Las cifras que `DX-Error-Messages.md` §7.1 declara —48 filas, 9 repetidas, 14 reapariciones, 34 condiciones distintas, cuadre 34 + 14 = 48— son exactas.**

### 3.2 Contraste contra el catálogo de §3

| Comprobación | Resultado |
| --- | --- |
| Identificadores distintos en las §6 de los diez CU | **34** |
| Identificadores distintos en las tablas de §3.1 a §3.10 | **34** |
| **Diferencia simétrica** | **Vacía.** Ninguna condición del catálogo carece de respaldo en una §6, y ninguna condición de una §6 quedó sin entrada |
| Condiciones inventadas por 03 | **0** |
| Filas de tabla en §3 | **35**, distribuidas 5·5·7·4·5·1·2·2·1·3. La única duplicada es `ESTADO_INICIAL_NO_NEGOCIABLE` (§3.1 y §3.10) |
| Entradas nuevas y reapariciones por CU, contra §7.2 | Recontado fila por fila: 5+5+7+4+5+1+2+2+1+2 = **34** nuevas, y 0+0+1+1+1+1+1+3+3+3 = **14** reapariciones. **La tabla de §7.2 es exacta en sus tres columnas y en sus dos totales** |
| Taxonomía de §2.1, recontada sobre §7.3 | Entrada inválida 13 (12 + la fila rotulada «Entrada inválida (§2.1)»), recurso ausente 4, conflicto de estado 11, conflicto de facultad 2, conflicto de alcance 1, error transitorio 1, error interno 2. **Suman 34 y coinciden con los siete valores declarados** |

### 3.3 Citas de motivo en las §5

| CU | Citas en su §5 | ¿Alguna ausente de su propia §6? |
| --- | --- | --- |
| CU-01 | 2 (`CORREO_YA_REGISTRADO`, `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`) | No |
| CU-02 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`) | No |
| CU-03 | 2 (`CREDENCIAL_NO_ESTABLECIDA`, `CREDENCIAL_VIGENTE_NO_VERIFICADA`) | No |
| CU-04 | 2 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`) | No |
| CU-05 | 1 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`) | No |
| CU-06 | 0 | — |
| CU-07 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`) | No |
| CU-08 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRANSICION_DESDE_ESTADO_TERMINAL`) | No |
| CU-09 | 3 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`) | No |
| CU-10 | 1 (`ADMINISTRADOR_YA_CONFIGURADO`) | No |
| **Total** | **17** | **Ninguna** |

**La cifra de diecisiete que `DX-Error-Messages.md` §7.2 declara es exacta, y ninguna de las diecisiete introduce una condición que la §6 del mismo caso de uso no declare.**

### 3.4 `ESTADO_INICIAL_NO_NEGOCIABLE` con causas opuestas

| Comprobación | Resultado |
| --- | --- |
| ¿Está declarado el tratamiento antes del catálogo? | **Sí.** §1.4, «Un mismo motivo con dos causas opuestas: los dos caminos de alta», con tabla de cinco rasgos opuestos —estado inicial, credencial, ventana de alta, papel, veces que se ejerce— y la conclusión: «No es una inconsistencia y no hay que unificarlo: el enunciado del motivo es “el estado inicial de este camino no se elige”, y cuál es ese estado lo fija el camino» |
| ¿Fila completa en dos subsecciones con remisión mutua? | **Sí.** §3.1: «**Mismo motivo, causa opuesta en CU-10** … ver §3.10 y §1.4». §3.10: «**Mismo motivo, causa opuesta en CU-01** … ver §3.1 y §1.4» |
| ¿Adopta la forma del proyecto de código hermano? | **Sí, y es literalmente la misma.** `GeometriaFactory-Domain/03-UX-UI-DX/DX-Error-Messages.md` §3 declara la misma excepción para el mismo identificador entre su §3.1 y su §3.12, con la misma redacción de causa y de remisión |
| ¿Es comprensible? | **Sí.** El lector encuentra la advertencia en tres lugares antes de la tabla —§1.4, el preámbulo de §3 y la fila misma— y además en `Guia-Onboarding-Developer.md` §4 como diagnóstico frecuente: «Un alta rechaza con `ESTADO_INICIAL_NO_NEGOCIABLE` y la causa parece contradecir a la del otro camino» |
| ¿Altera el recuento de condiciones distintas? | **No.** §7.2 lo cuenta como entrada nueva en CU-01 y como reaparición en CU-10, «igual que las otras ocho repetidas», y lo declara explícitamente: «**la segunda fila de tabla de §3.10 no altera el recuento de condiciones distintas**, sólo el de filas de tabla». **Recontado de forma independiente: 34 distintas sobre 35 filas. Correcto** |

### 3.5 La divergencia deliberada de clasificación

`PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` está clasificado **entrada inválida** acá y **conflicto de facultad** en el proyecto de código hermano, con la divergencia declarada en §2.1 y su motivo: «acá el papel es **un dato del pedido de alta**, no la facultad de quien pide».

**El fundamento se sostiene, y por lo tanto no es hallazgo.** Verificado en tres puntos: (a) la definición que esta capa da a «conflicto de facultad» es «el papel declarado por **quien pide** no la ejerce», y en `CU-01` el papel es un campo del pedido que describe la cuenta a constituir, no el papel del invocante; (b) la definición de «entrada inválida» de esta capa incluye textualmente «no admitido en este camino», que es exactamente la causa; (c) `CU-01` §10 lo confirma: «Este caso de uso **no verifica pertenencia ni facultad**». El hermano, en cambio, amplió su propia definición de la categoría con la cláusula «o el camino por el que se pide no es el suyo», que esta capa no adoptó: las dos clasificaciones son coherentes cada una con su propia taxonomía, y la divergencia está declarada, no silenciada.

Lo único observable es un argumento auxiliar inexacto en esa misma justificación, recogido como **H-18** en §7.

### 3.6 La sección nueva de rechazos inalcanzables (§2.5)

Verificación independiente: se extrajeron los códigos de las §6 de los **doce** casos de uso del dominio —40 identificadores distintos— y se restaron los 34 del catálogo de esta capa.

| Comprobación | Resultado |
| --- | --- |
| Códigos del dominio sin correspondencia en el catálogo de esta capa | **16**, exactamente: `UNICIDAD_DE_CORREO_NO_VERIFICADA`, `BAJA_SIN_ARRASTRE_DE_TRABAJOS`, `REEDICION_FUERA_DE_BORRADOR`, `ENVIO_SIN_INTERPRETACION`, `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO`, `TIPO_DE_PIEZA_DESCONOCIDO`, `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`, `POSICION_DE_PIEZA_INVALIDA`, `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL`, `ESPECIE_DE_OBSERVACION_DESCONOCIDA`, `ERROR_SIN_UBICACION`, `ADVERTENCIA_SIN_LOS_DOS_VALORES`, `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`, `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`, `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR`, `OPERACION_DESCONOCIDA` |
| ¿Coincide con la lista de §2.5? | **Sí, uno a uno. La lista es correcta y completa: ni sobra ni falta ninguno** |
| ¿Están distinguidos sus tipos? | **Sí, tres tipos.** Inalcanzables por construcción (6), equivalentes (1: `REEDICION_FUERA_DE_BORRADOR`, «es la misma negativa que `OPERACION_FUERA_DE_BORRADOR`»), agregados (8, en dos condiciones agregadas simétricas) y no producidos por corte previo (2, los de papel) |
| ¿Alguna de esas filas se cuenta como condición? | **No, ninguna.** El preámbulo de §2.5 lo declara —«**Ninguna fila de esta tabla es una condición de este catálogo**, y por eso ninguna entra en los recuentos de §7»—, §7.1 la registra como magnitud aparte —«Rechazos del dominio sin condición propia acá … 16, ninguno de ellos condición de este catálogo»— y §7.2 lo repite. **Verificado mecánicamente: ninguno de los dieciséis aparece en ninguna §6 de esta capa, de modo que ninguno pudo entrar en el recuento de 48/34** |
| ¿Está la advertencia pedida? | **Sí.** §2.5, consecuencia 2: «**Un rechazo inalcanzable que aparece en ejecución es un defecto de esta capa, no del consumidor.** Si el dominio devuelve `ENVIO_SIN_INTERPRETACION`, el caso de uso saltó un paso propio. Es la mejor señal temprana que ofrece esta frontera» |
| ¿Cada fila remite a su lugar de declaración en la 02? | **Sí, y las ocho remisiones resuelven**, salvo la precisión de H-17 sobre el segundo camino de `UNICIDAD_DE_CORREO_NO_VERIFICADA` |

---

## 4. Verificación de la partición del caso de uso y de la fidelidad de la orquestación

### 4.1 ¿Se sostiene la partición?

| Comprobación | Resultado |
| --- | --- |
| ¿Está declarado el fundamento? | **Sí**, en `Especificacion-Funcional.md` §8: «**Los dos caminos de alta se separaron —CU-10 frente a CU-01—** … no comparten casi nada: el estado inicial es opuesto … la credencial se aporta en uno y se prohíbe en el otro, la ventana de alta existe en uno y no en el otro … **mantenerlos fusionados acá obligaría a un solo caso de uso a orquestar dos casos de uso de dominio con postcondiciones contradictorias**, que es exactamente lo que produjo el defecto que la ronda r1 del audit levantó» |
| ¿El fundamento es verdadero? | **Sí, comprobado contra las fuentes.** `Domain CU-01` §4 paso 6 fija `Pendiente`; `Domain CU-12` §4 paso 7 fija «**`Habilitado`**, no en `Pendiente`». Las dos postcondiciones son incompatibles en el mismo contrato. `Domain CU-12` §11 y `Domain CU-01` §11 registran la misma corrección desde el otro lado |
| ¿Queda residuo del camino viejo en `CU-01`? | **No.** `CU-01` no contiene ninguna mención a constituir una cuenta habilitada; `ADMINISTRADOR_YA_CONFIGURADO` se mudó íntegro a `CU-10` §6 y a `DX-Error-Messages.md` §3.10, y ya no aparece en §3.1 |
| ¿`CU-10` es un caso de uso completo? | **Sí.** Las once secciones obligatorias de `Rules-Especificacion-Funcional.md` §4.2 están presentes y en orden, más la §17 de la variante `library` después de §11; cinco criterios de aceptación con valores concretos (2026-03-01, `docente@ejemplo.edu`, 0 trabajos, 0 motivos); §9 con las siete dimensiones, incluidas «Casos de uso de dominio orquestados» y «Puertos que consume» |
| ¿Las citas retiradas están efectivamente corregidas? | **Sí, las dos.** `CU-01` §10: «RN-01 e INV-05 **no fundamentan ningún estado inicial**: declaran la unicidad del administrador y la ventana en la que su alta es posible». `CU-10` §10 repite la misma advertencia. `DX-Error-Messages.md` §7.3, nota: «**Ninguna de las once reglas enuncia con qué estado nace una cuenta.** La atribución a RN-01 se retiró aguas arriba». Contrastado contra `RN-01`, que efectivamente no enuncia estado inicial alguno |

### 4.2 La numeración al final

| Comprobación | Resultado |
| --- | --- |
| ¿La serie es contigua? | **Sí**, CU-01 a CU-10 sin huecos, un archivo por caso de uso |
| ¿Está declarada la decisión de numerar al final? | **Sí, en tres lugares.** `Especificacion-Funcional.md` §10.2: «**CU-10 se numeró al final y no junto a CU-01**, con el que forma par temático, **para no renumerar los ocho casos de uso intermedios que otras categorías ya citan por su identificador.** Es la misma decisión con la que `GeometriaFactory-Domain` incorporó su CU-12». `02/README.md` §5 y el propio `CU-10` §11 lo repiten |
| ¿Se declara la estabilidad del nombre de archivo de `CU-01`? | **Sí**, §10.3: el archivo conserva su nombre aunque su alcance se acotó, «por estabilidad de citación» |
| ¿El orden de lectura compensa la separación? | **Sí.** `02/README.md` §3.3: «**CU-10 y CU-01 se leen juntos**: son los dos caminos de alta del producto y sus reglas son opuestas, aunque el número los separe» |

### 4.3 Fidelidad de la orquestación contra el dominio corregido

Los **doce** casos de uso del dominio, contrastados uno por uno contra el cuerpo del caso de uso de esta capa que los invoca —no sólo contra la tabla de §7.4—.

| CU de Application | CU de Domain que el **cuerpo** invoca | ¿Respaldado? |
| --- | --- | --- |
| CU-01 Auto-registro | CU-01 (paso 4, constitución del alumno; paso 5, el dominio fija papel y estado) | Sí |
| CU-10 Configuración del administrador | **CU-12** (pasos 5 y 6: invoca la configuración; el dominio fija papel `Administrador`, estado `Habilitado` y adopta la credencial) | **Sí** |
| CU-02 | CU-02 | Sí |
| CU-03 | CU-04 (admisibilidad); CU-03 (fijación y reemplazo) | Sí |
| CU-04 | CU-05; CU-09 | Sí |
| CU-05 | CU-06 (paso 4, piezas + cantidad de figuras); CU-07 (paso 5, observaciones); CU-08 (paso 6, estado); CU-09 (paso 2, pertenencia) | **Sí, y H-03 cerrado** |
| CU-06 | CU-09 | Sí |
| CU-07 | CU-11 | Sí |
| CU-08 | CU-11; CU-10 | Sí |
| CU-09 | CU-09; CU-11 | Sí |

**Huérfanos: ninguno en ninguna de las dos direcciones.** Los doce casos de uso del dominio quedan orquestados por los diez de esta capa; los diez trazan a al menos una necesidad de negocio; NB-00008 sigue declarada como alerta explícita con su lugar de cobertura.

**Fidelidad punto por punto de `CU-10` contra `Domain CU-12`:** papel `Administrador`, estado `Habilitado`, credencial derivada aportada y adoptada, ventana de alta comprobada por esta capa sobre el conjunto de cuentas y declarada al invocar, unicidad del correo ídem, cuatro de los cinco códigos de §6 propagados con el mismo identificador y la misma causa, y el quinto (`CORREO_YA_REGISTRADO`) propio de esta capa por ser el resultado de su propia consulta. `CU-10` CA-02 espeja `Domain CU-12` CA-02 —«devuelve admisible, con 0 motivos»— y sostiene el guion de la etapa `c`. **No se detectó ninguna afirmación de esta capa sobre el dominio corregido que el dominio contradiga.**

---

## 5. Gobierno del glosario

`Vocabulario-Rules.md` §10, con su criterio negativo.

| Criterio | Resultado |
| --- | --- |
| **Tres capas, referenciar y no redefinir** | **Cumple.** Raíz en `Vision-Producto.md` §9; `Glosario-Funcional.md` de 02 con 14 términos acuñados (12 + «metadato de orquestación» y «camino de alta»); `Glosario-UX.md` con 25 (23 + «condición agregada» y «rechazo inalcanzable por construcción»). Las dos altas de 03 se declaran **referenciadas y no redefinidas** en su §4.2 respecto de las de 02 |
| **Sin contradicciones** | **Cumple.** Verificado entrada por entrada sobre los términos nuevos y los tocados por las correcciones. «Cantidad de figuras del conjunto raíz» se declara **referenciada del modelo del dominio** en `Glosario-Funcional.md` §4.2 y no se redefine acá; el enunciado coincide literalmente con `Definicion-Modelo-De-Dominio.md:87` |
| **Completitud** | **Cumple.** Las dos tablas no vacías, con la columna de artefactos donde cada término aparece, actualizada a los diez CU |
| **Polisemia gobernada** | **Cumple.** 02 conserva sus cinco polisemias con referentes, forma obligatoria y evidencia de colisión; 03 conserva sus dos. La forma desnuda que r1 levantó como H-11 quedó calificada |
| **Criterio negativo** | **Cumple, y se conserva íntegro.** `Glosario-Funcional.md` §3.6 y `Glosario-UX.md` §3.3 siguen anticipando los falsos positivos, esta última con la frase «Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica» |
| **Vocabulario normativo de §10** | **Cumple.** «Proyecto» a secas: cero ocurrencias. «Solución» a secas designando el agrupador de construcción: **cero** (H-10 cerrado). «Repositorio» a secas en sección de contextos convivientes: **cero** (H-11 cerrado). `Pendiente` calificado, con las dos excepciones declaradas |

**Las doce polisemias que r1 evaluó y descartó por contextos disjuntos se dan por resueltas y NO se califican en esta ronda**, por el criterio negativo de `Vocabulario-Rules.md` §9.1: puerto contra puerto de red; observación superordinada; comentario; rol; trabajo / unidad de trabajo / flujo de trabajo; categoría de error contra categoría del framework; error dentro de «catálogo de errores»; doble; motivo; alcance; migración; y repositorio en los artefactos de 02. Se reverificó que las correcciones **no crearon colisiones nuevas** en ninguna de las doce, y no las crearon. Exigir su calificación sería un defecto de este informe.

---

## 6. Forma, y lo que r1 validó y no se degradó

| Comprobación | Resultado |
| --- | --- |
| **Versión, estado y fecha** | **Cumple en los dieciocho.** `Versión: 1.0`, `Estado: Propuesto`, `Fecha: 2026-08-09`. `CU-10` **nace en 1.0**, como corresponde a una emisión inicial |
| **Absorción sin subir versión** | **Cumple.** Los trece documentos corregidos llevan una segunda fila de control de cambios en 1.0 que cita el hallazgo de r1 que la origina y el fundamento: «absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`». Es el tratamiento correcto |
| **`_legacy/`** | **No existe** bajo `GeometriaFactory-Application/`, y su ausencia está declarada con motivo en `02/README.md` §5 y `03/README.md` §2. Correcto |
| **Sufijo de versión en archivo vivo** | **Ninguno** |
| **§6 de `Rules-Especificacion-Funcional.md`**, donde las correcciones lo pudieron alterar | Las once secciones obligatorias en los diez CU, con §17 después de §11; mínimo de cinco CU para `library` superado con diez; tres criterios Given/When/Then mínimos superados en los diez; trazabilidad NB→CU→US en las diez §9; matriz de §7.1 y cobertura bidireccional actualizadas con CU-10 y US-28; **114+ enlaces relativos verificados mecánicamente, ninguno roto** |
| **§6 de `Rules-UX-UI-DX.md`**, ídem | Variante **DX** declarada en los cinco artefactos; los tres `dx-` obligatorios presentes; **mínimo de wireframes cero, cumplido con motivo declarado** (`03/README.md` §1); `DX-Developer-Experience.md` con sus nueve secciones intactas; `DX-Error-Messages.md` con sus seis en la numeración correcta tras H-09; quick-start declarado no aplicable en `DX-Error-Messages.md` §7.4 **sin darlo por cumplido**; los ocho artefactos omitidos y los siete criterios no aplicables siguen declarados con su flag |
| **Las tres negativas de autorización** | **No degradadas.** `Especificacion-Funcional.md` §4 conserva las tres filas y las cuatro precisiones; `DX-Error-Messages.md` §2.4 conserva la tabla de cuatro traducciones prohibidas **incluido el error simétrico** —«`FACULTAD_DE_ADMINISTRADOR_REQUERIDA` → “no encontrado” … El error simétrico, y también es un defecto»—, el procedimiento de decisión y los anclajes. Verificados vigentes CA-03 de CU-06, CA-03 de CU-09 («el mismo que para un identificador inexistente»), CA-03 de CU-07 («el repositorio registra 0 consultas»), CA-05 de CU-05 («el validador doble registra 0 invocaciones») y la métrica «Traducciones prohibidas \| 0, sin tolerancia» de `DX-Developer-Experience.md` §6 |
| **D9** | Alcanza al estado del sistema, y no hay sistema construido. Ningún documento afirma que lo haya; las afirmaciones ejecutables van rotuladas como criterio o compromiso verificable en el punto de control. La única X de r1 era H-01, hoy cerrada |
| **Punto abierto del puerto de repositorio de cuentas** | Sigue bien fundado y bien declarado, y ningún artefacto lo usa como si el intake lo hubiera nombrado. Los puntos abiertos pasan de cuatro a **cinco** con el de los sellos, coherente en `Especificacion-Funcional.md` §11 y en `03/README.md` §6 |

---

## 7. Hallazgos nuevos

Cuatro, **todos P3**. Ninguno P0, P1 ni P2.

### H-15 · P3 · Dos cabeceras siguen declarando once casos de uso de dominio

**Archivos:** `02-Especificacion-Funcional/Especificacion-Funcional.md` línea 11; `03-UX-UI-DX/README.md` línea 11

**Evidencia.** La trazabilidad upstream del índice maestro dice todavía:

> «`Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` completo, **cuyos once casos de uso** esta categoría orquesta»

y la de `03/README.md`: «(RN-01 a RN-11 y **los once casos de uso** que esta capa orquesta)».

El dominio tiene hoy **doce**, y el propio cuerpo del índice lo dice: §7.4, «Los **doce** casos de uso de `GeometriaFactory-Domain` quedan orquestados por los diez de esta capa». `DX-Error-Messages.md` línea 11 sí actualizó su cabecera: «las §6 de los **doce** casos de uso». Las dos cabeceras quedaron atrás. Es una afirmación viva y contradice al cuerpo de su propio documento; no es una fila histórica de control de cambios, que sí puede conservar la cifra de entonces.

**Recomendación.** Escribir «doce» en las dos cabeceras.

---

### H-16 · P3 · Residuos de la nomenclatura anterior a la corrección de los sellos

**Archivos:** `03-UX-UI-DX/Guia-Onboarding-Developer.md` líneas 112 y 236; `02-Especificacion-Funcional/Especificacion-Funcional.md` línea 192

**Evidencia.** La corrección de H-06 renombró las tres fechas de reloj como **sellos** y así las declaran §3 y §11 del índice, el glosario de 02 y las §10 de CU-01, CU-03, CU-04, CU-05 y CU-08. Tres lugares conservan la forma anterior:

- `Guia-Onboarding-Developer.md:112`: «Ni siquiera la fecha: **la fecha de modificación** salió del puerto de reloj».
- `Guia-Onboarding-Developer.md:236`: cita el criterio CA-01 de CU-01 como «devuelve la cuenta con **fecha de alta** 2026-03-15», cuando el criterio hoy dice «con **sello de alta** 2026-03-15». La cita textual dejó de ser fiel.
- `Especificacion-Funcional.md:192`, US-10: «Cargar un trabajo con dueño, identificador propio y **fecha tomada del reloj**».

**No hay atribución indebida al modelo del dominio en ninguno de los tres** —que era la sustancia de H-06, y está cerrada—: es sólo nomenclatura desalineada, agravada en el segundo caso por ser una transcripción.

**Recomendación.** Reemplazar por «sello de modificación», «sello de alta» y «sello tomado del reloj».

---

### H-17 · P3 · `UNICIDAD_DE_CORREO_NO_VERIFICADA` remitido a una §10 que sólo cubre uno de sus dos caminos

**Archivos:** `03-UX-UI-DX/DX-Error-Messages.md` §2.5, primera fila; `02-Especificacion-Funcional/Casos-De-Uso/CU-10-Configurar-La-Cuenta-De-Administrador.md` §10

**Evidencia.** La fila de §2.5 declara el origen en los dos caminos y remite a uno solo:

> «`UNICIDAD_DE_CORREO_NO_VERIFICADA` \| Dominio, **auto-registro y configuración** \| **Inalcanzable por construcción.** Los dos caminos de alta consultan el correo antes y declaran siempre la verificación al invocar \| **CU-01 §10**»

`CU-01` §10 argumenta sobre sus propios pasos —«el paso 4 declara siempre la verificación que el paso 2 hizo»— y no alcanza a `CU-10`, cuyos pasos son otros (3 y 5). `CU-10` §10 lo alude sin nombrarlo: «La ventana de alta se comprueba acá … y se le declara al dominio al invocar. Es **la misma división de trabajo que la unicidad del correo**». Es exactamente la forma que r1 objetó en H-14 para `BAJA_SIN_ARRASTRE_DE_TRABAJOS` —«aludido en prosa, sin nombrarlo»—, y que allí se corrigió.

El §2.5 mismo enuncia el principio que la fila no cumple del todo: «la 02 los nombra uno por uno en sus §10».

**Recomendación.** Nombrar el código en `CU-10` §10 con su argumento de inalcanzabilidad propio, y agregar «CU-10 §10» a la última columna de esa fila.

---

### H-18 · P3 · Argumento auxiliar inexacto en la justificación de la divergencia de clasificación

**Archivo:** `03-UX-UI-DX/DX-Error-Messages.md` §2.1, último párrafo

**Evidencia.** La justificación de clasificar `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` como entrada inválida cierra así:

> «Clasificarlo como conflicto de facultad rompería **la correspondencia uno a uno** entre esa categoría y la negativa por facultad de §2.4»

Esa correspondencia uno a uno no existe hoy. La categoría «conflicto de facultad» tiene **dos** miembros según §2.1 y §7.3 —`FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`—, mientras que la negativa por facultad de §2.4 es **una** sola, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`. La segunda es un rechazo del dominio sobre la cuenta destino, no una negativa de autorización sobre quien pide.

**El fundamento principal sí se sostiene y no es hallazgo** —el papel llega como dato del pedido, no como facultad de quien pide, y `CU-01` §10 confirma que ahí no hay verificación de facultad—; lo que falla es el argumento accesorio, que apela a una propiedad que el catálogo no tiene.

**Recomendación.** Sustituir la cláusula por la propiedad verdadera: la categoría agrupa negativas que se resuelven mirando el papel **de quien pide**, y este motivo no es una de ellas.

---

### 7.1 Recuento por nivel

| Nivel | Cantidad | Hallazgos |
| --- | --- | --- |
| **P0** | 0 | — |
| **P1** | 0 | — |
| **P2** | 0 | — |
| **P3** | 4 | H-15, H-16, H-17, H-18 |
| **Total** | **4** | |

---

## 8. Veredicto y condiciones para promover

### Veredicto: **APROBADO CON OBSERVACIONES**

No existe ningún hallazgo P0 ni P1 ni P2. Los catorce hallazgos de la ronda 1 están cerrados con evidencia textual verificada archivo por archivo, y los cuatro hallazgos nuevos son de nivel P3: dos cifras de cabecera, tres términos desalineados con el renombre de los sellos, una remisión incompleta y un argumento accesorio impreciso. Ninguno afecta al comportamiento especificado, a la trazabilidad sustantiva, al recuento del catálogo ni a las tres negativas de autorización.

La corrección del P0 es de buena calidad y no se limitó a tapar el síntoma: la partición está fundada en una propiedad verificable —dos casos de uso de dominio con postcondiciones contradictorias no caben en un contrato—, se declara en el criterio de recorte, se espeja en las dos capas, se numera sin romper citas y se ancla en un criterio de aceptación que encadena la configuración con la admisibilidad, que es la prueba que habría detectado el defecto. Las dos citas indebidas se retiraron y, además, se sustituyeron por la declaración expresa de qué es lo que RN-01 e INV-05 sí dicen, que es la forma que impide que el error vuelva.

### ¿Puede el proyecto de código avanzar a la Fase C?

**Sí.** `GeometriaFactory-Application` **queda habilitado para avanzar a la Fase C** en sus categorías 02 y 03, con la categoría 04 omitida por gating. No hay condición bloqueante. Las cuatro observaciones se corrigen en la misma versión 1.0 en curso, sin subir versión y sin ronda de auditoría nueva.

### Condiciones

**Bloqueantes:** ninguna.

**Antes del cierre de fase, absorbibles en 1.0:**

1. **H-15**, corregir las dos cabeceras a «doce casos de uso».
2. **H-16**, alinear los tres residuos con la nomenclatura de sellos, en particular la transcripción de CA-01 de CU-01.
3. **H-17**, nombrar `UNICIDAD_DE_CORREO_NO_VERIFICADA` en `CU-10` §10 y completar la remisión de §2.5.
4. **H-18**, reformular el argumento auxiliar de §2.1. Ninguno de los cuatro altera el catálogo, ni sus recuentos, ni la verificación mecánica de §7 de `DX-Error-Messages.md`.

**Regla de versionado aplicable.** Los dieciocho documentos siguen en estado `Propuesto`, de modo que por `Master-Prompt.md` §5 estas correcciones **se absorben dentro de la versión 1.0 en curso, sin subir versión**, dejando su fila en el control de cambios con la cita del hallazgo de este informe.

**No se reportan como defecto**, y se dejan asentadas para que una ronda posterior no las levante: las **doce polisemias de contextos disjuntos** que enumeró `B-02-03-GeometriaFactory-Application-r1.md` §6.2, reverificadas acá sin colisiones nuevas; la ausencia de los artefactos de la variante UX/UI, de los de maqueta y de la categoría 04; la ausencia de `_legacy/`; la ausencia de las `RN-XX`, del modelo conceptual y del documento de concepto central; el punto abierto del puerto de repositorio de cuentas y los otros cuatro; la doble fila de `ESTADO_INICIAL_NO_NEGOCIABLE`, que es la forma correcta y la del proyecto de código hermano; la divergencia deliberada de clasificación de `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, cuyo fundamento principal se sostiene; y la divergencia de motivo entre App CU-08 CA-05 y Dom CU-10 CA-05, que sigue siendo consecuencia correcta del orden de orquestación.

---

## Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Ronda 2 de auditoría de la Fase B de `GeometriaFactory-Application`, categorías 02 y 03. Verificación de cierre de los catorce hallazgos de la ronda 1: **catorce cerrados**, tres de ellos con un defecto nuevo P3 asociado a la propia corrección. Cuatro hallazgos nuevos, todos P3, y veredicto **APROBADO CON OBSERVACIONES**, con habilitación explícita para avanzar a la Fase C. Incluye el recuento independiente del catálogo —48 filas, 34 condiciones distintas, 14 reapariciones, 35 filas de tabla y diferencia simétrica vacía—, la verificación de las diecisiete citas de motivo de las §5, la verificación de los dieciséis rechazos inalcanzables de la sección nueva §2.5 contra los cuarenta códigos del dominio, la verificación de la partición de CU-01 y del alta de CU-10 contra el CU-12 nuevo del dominio, y la reverificación de las doce polisemias evaluadas y descartadas en la ronda 1. | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
