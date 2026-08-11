# Observación sobre la ejecución de la orquestación

| Campo | Valor |
|---|---|
| Versión | 1.2 |
| Fecha | 2026-08-11 |
| Estado | **Aprobado** |
| Autor | Orquestador SDD |
| Origen | Observación del Product Owner, 2026-08-11 |
| Alcance | La ejecución del tramo de especificación completo, fases A a H |

---

## 1. Qué se observa

El Product Owner señaló un defecto en **cómo se ejecutó la orquestación**, no en lo que se produjo. El defecto tiene dos mitades y las dos se confirman con evidencia del propio repositorio.

**Primera mitad: el orquestador no verificó las entregas de los subagentes antes de incorporarlas.** El ciclo aplicado fue *despachar → recibir informe → comitear*. Cuando un subagente declaraba «corregí quince documentos y los recuentos cierran», esa declaración se incorporó sin abrir ningún archivo y sin contar nada. El primer control efectivo fue siempre la auditoría siguiente.

**Segunda mitad: el orquestador no llevó registro del estado del proceso.** No existió, durante trece cortes de fase, una tabla de *fase × proyecto de código × ronda × veredicto* que se consultara antes de abrir la rama siguiente.

## 2. Por qué es grave

El tramo de especificación tiene una propiedad que lo distingue del tramo de construcción: **su producto es texto que nadie ejecuta**. Un defecto en el código se manifiesta al correrlo; un defecto en la especificación sólo se manifiesta cuando alguien lo lee y actúa sobre él, que puede ser meses después y en otra cabeza.

Por eso la verificación de la entrega no es una formalidad de gestión: **es el único mecanismo de detección que existe** antes de la auditoría. Delegarla entera en la auditoría convierte a ésta en el primer par de ojos en vez del segundo, y el resultado es medible en este repositorio.

## 3. Evidencia

| # | Hecho | Dónde consta |
|---|---|---|
| E-1 | **Dos controles obligatorios no se ejecutaron y el orquestador no lo advirtió**: la auditoría de Fase B de `GeometriaFactory-Api` —el proyecto **principal**— y la ronda 2 de la Fase B2. Los detectó el inventario de traspaso, no la cadena de fases | `Handoff-Checkout.md` §6.1 `B-1` y `B-2` |
| E-2 | La Fase B de `GeometriaFactory-Api` **rechazó con un P0** cuando por fin se auditó. El proyecto principal llegó a la Fase H sin control de su fase | `B-02-03-GeometriaFactory-Api-r1.md` |
| E-3 | **Siete fases se generaron y auditaron sobre una línea de base cuya fase estaba formalmente rechazada** | `B2-Maqueta-GeometriaFactory-Web-r2.md` §0 |
| E-4 | En **trece tandas consecutivas** los hallazgos de auditoría resultaron **subcontados sin una sola excepción**: el corrector encontraba más ocurrencias que las que el informe listaba. El orquestador despachaba la lista del informe sin verificarla | Informes de corrección de las fases B2, C, D, E, F, G y del cierre de huecos |
| E-5 | Un hallazgo reportado **con número de línea** fue declarado «no reproducible» por el orquestador, que miró líneas distintas de las citadas. Sobrevivió una ronda completa | `Coherencia-Corpus-r2.md` `N-01`; reaparece en `H-Final-Consolidado-r1.md` `P2-1` |
| E-6 | Un script de corrección masiva se ejecutó **sin revisar el diff**: modificó 26 archivos convirtiendo filas de tablas que legítimamente tienen cuatro columnas. Se revirtió en el momento, sin pérdida | Sesión del 2026-08-11 |
| E-7 | El orquestador escribió en un mensaje de commit una cifra falsa —«232 historias» donde hay 180— que **viajó dentro del propio encargo de auditoría** de la fase siguiente | `D-06-07-Backlog-Siete-Proyectos-r1.md` `D-06-01` |
| E-8 | Una corrección del intake se comiteó **con su fila de control de cambios escrita y la sustitución sin aplicar**: durante un commit, el documento afirmó haber corregido algo que seguía mal | Commit `ee73c99`, corregido en `27e186b` |

## 4. Qué no falló

Se deja constancia para que la observación sea proporcionada, no para atenuarla.

- **Los arreglos sí se planificaron y ejecutaron.** Cada rechazo tuvo su tanda de corrección y su re-auditoría, y ninguna rama se empujó con una auditoría rechazada abierta.
- **La disciplina de no inventar se sostuvo.** Ningún umbral numérico se inventó, ninguna asunción se convirtió en compromiso, y los puntos abiertos se declararon en vez de resolverse por cuenta propia.
- **El defecto se detectó dentro del método**, antes del traspaso a codificación, por un control que el propio framework exige.

## 5. Causa

El orquestador trató el informe del subagente **como si fuera el trabajo**. Un informe es una afirmación sobre el trabajo, y en este producto quedó demostrado —trece veces— que las afirmaciones sobre el trabajo son sistemáticamente optimistas: subcuentan el alcance, dan por corregido lo que corrigieron parcialmente, y heredan cifras sin recontar.

Es exactamente el defecto de fondo que este corpus persiguió toda la sesión —**afirmar sobre una fuente sin abrirla**— aplicado por el orquestador a las entregas de sus propios subagentes.

## 6. Corrección adoptada

Rige desde el 2026-08-11 y **alcanza al tramo de construcción**, donde el mismo patrón es más caro porque el entregable es código.

| # | Medida | Qué previene |
|---|---|---|
| C-1 | **Verificar antes de incorporar.** De cada entrega se comprueban con herramienta las afirmaciones cuantificables: si declara «seis lugares», se cuentan seis | `E-4`, `E-7` |
| C-2 | **Triage de hallazgos antes de despachar la corrección.** El orquestador verifica severidad y alcance, y despacha lo verificado, no lo reportado | `E-4`, `E-5` |
| C-3 | **Registro de proceso vivo**: fase × proyecto × ronda × veredicto, consultado antes de abrir cada rama | `E-1`, `E-2`, `E-3` |
| C-4 | **Ningún cambio masivo sin diff previo revisado** | `E-6` |
| C-5 | **Ninguna afirmación de corrección se comitea sin comprobar que la corrección se aplicó** | `E-8` |

### 6.1 Cómo se decide a dónde va un arreglo

Verificar no alcanza: al encontrar un defecto hay que **diagnosticar su causa**, porque de la causa depende quién lo arregla. Un arreglo mandado al lugar equivocado no arregla nada y además esconde el problema real. El diagnóstico es del orquestador y **no se delega**: el subagente que falló no es buen juez de por qué falló, y el auditor ve el síntoma, no siempre el origen.

| Causa | Cómo se reconoce | A dónde va el arreglo | Ejemplo de esta sesión |
|---|---|---|---|
| **Defecto del subagente** | La fuente decía lo correcto y el entregable la transcribió mal, contó mal, o dejó a medias lo que declaró hacer | Vuelve al mismo alcance, con el defecto nombrado y el patrón a buscar | Un caso de prueba declaraba verificar la puerta técnica equivocada; la tabla tenía las dos definiciones intercambiadas |
| **Ambigüedad de la fuente** | Dos partes del intake o del manifiesto dicen cosas distintas sobre lo mismo, y las dos son defendibles | **Se corrige la fuente primero**, y recién después se propaga. Nunca al revés | §16.1 negaba samples propios a `Infrastructure` mientras §18 le asignaba la muestra `S-3`; convivieron veintitrés versiones |
| **Dato que el producto no tiene** | Ninguna fuente lo declara, y ninguna capa puede derivarlo sin inventarlo | **Se eleva al Product Owner** y se declara abierto. No se resuelve por cuenta propia | El umbral de fluidez del visor, sin valor en ninguna fuente, declarado abierto durante cinco fases |
| **Decisión de producto no tomada** | Hay dos salidas posibles y las dos son coherentes; elegir cambia lo que el usuario ve | **Se eleva al Product Owner con las dos salidas escritas** y su consecuencia | Cómo se identifica quien establece su contraseña por primera vez: un punto anónimo, o una provisoria como en el reseteo |
| **Defecto del propio informe de auditoría** | El hallazgo no se reproduce, su ubicación es falsa, o su recuento no cierra | **Se corrige el informe, no el documento auditado**, y se declara | Un informe declaró quince hallazgos y enumeró diecisiete; otro citó una frase que no existía en la fila que nombraba |
| **Defecto del orquestador** | El encargo llevaba una premisa falsa, o el registro de proceso omitió un control | Se corrige el encargo y se reporta, como hace este documento | Dos controles no ejecutados; una cifra falsa que viajó dentro de un encargo de auditoría |

**La regla que ordena la tabla**: un defecto de transcripción se arregla donde se transcribió; un defecto de la fuente se arregla en la fuente **antes** de propagarlo. Cuando este producto invirtió ese orden —propagar una decisión a cuatro proyectos sin escribirla antes en el intake— el resultado fue once documentos citando como ratificado algo que la fuente contradecía.

### 6.2 El plan de arreglos como cierre de tarea

Propuesta del Product Owner, adoptada. Cambia **el ciclo**, no sólo el cuidado con que se ejecuta.

**Cómo se venía haciendo.** Despachar, recibir el informe, incorporar, auditar, despachar la corrección. Los arreglos se despachaban **desde la conversación**: el orquestador armaba el encargo con lo que recordaba del informe y de lo que había pasado antes. Eso tiene dos costos que este producto pagó. El primero es de **deriva**: lo que el orquestador recuerda envejece y se deforma, y cuando ese recuerdo entra en un encargo, entra como si fuera dato verificado — así viajó una cifra falsa *dentro* de un encargo de auditoría (`E-7`). El segundo es de **consumo**: cada ciclo arrastra todo el contexto acumulado de los anteriores, que crece sin que su utilidad crezca.

**Cómo se hace desde ahora.** La tarea **no termina cuando el subagente entrega**. Termina cuando el orquestador la evaluó y **emitió un plan de arreglos**, que es un **artefacto en disco**, no un tramo de conversación. Con el plan emitido, la tarea está cerrada y **el contexto se puede compactar sin pérdida**. El ciclo de arreglos arranca después, **leyendo el plan**, no la memoria de nadie.

**Qué tiene que contener el plan para que eso funcione.** Es el punto: si el plan no se basta a sí mismo, la compactación pierde información y el ciclo siguiente vuelve a inventar.

| Campo | Por qué es obligatorio |
|---|---|
| El hallazgo, con su **ancla verificada** —archivo y línea, o identificador— | Sin ancla, el ciclo siguiente busca de memoria; así se declaró «no reproducible» un hallazgo real (`E-5`) |
| La **causa diagnosticada** según §6.1 | Es lo que determina a dónde va el arreglo, y no se puede reconstruir después |
| El **destino**: qué documento se toca, o a quién se eleva | Evita que un defecto de la fuente se arregle en la hoja |
| El **alcance contado**, no estimado | En trece tandas los alcances estimados quedaron cortos sin excepción (`E-4`) |
| Lo que **se verificó como falso** del informe original | Si no queda escrito, el ciclo siguiente vuelve a corregir algo que estaba bien |

**Criterio de cierre**: la tarea se cierra con el plan emitido, **no con los arreglos hechos**. Son dos ciclos distintos y conviene que lo sean, porque el que evalúa y el que arregla no necesitan el mismo contexto.

**Qué mejora, concretamente.** El encargo del ciclo de arreglos pasa a derivarse de un artefacto verificado en vez de un recuerdo, que es el origen de `E-4`, `E-5` y `E-7`. Y el contexto de cada ciclo pasa a ser **el plan más lo que el plan ancla**, en lugar de todo lo acumulado.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.2 | 2026-08-11 | **§6.2 nueva**, propuesta del Product Owner y adoptada: la tarea no termina cuando el subagente entrega sino cuando el orquestador la evaluó y **emitió un plan de arreglos como artefacto en disco**; con el plan emitido la tarea se cierra y el contexto se puede compactar sin pérdida, y el ciclo de arreglos arranca leyendo el plan y no la memoria. Se declaran los cinco campos que el plan debe traer para bastarse a sí mismo, y los tres hechos de §3 cuyo origen es precisamente haber despachado arreglos desde la conversación. | Product Owner (propuesta) · Orquestador SDD (redacción) |
| 1.1 | 2026-08-11 | **§6.1 nueva**, a pedido del Product Owner: verificar no alcanza si no se diagnostica la causa del defecto, porque de la causa depende quién lo arregla. Seis causas con su señal de reconocimiento, su destino y un ejemplo real de esta sesión, más la regla que las ordena. Sin esta sección la medida `C-2` era una intención sin procedimiento. | Orquestador SDD |
| 1.0 | 2026-08-11 | Emisión inicial, a pedido del Product Owner, que observó el defecto al revisar la coordinación general del tramo de especificación. | Orquestador SDD |
