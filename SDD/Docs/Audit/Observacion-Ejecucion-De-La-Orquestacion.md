# Observación sobre la ejecución de la orquestación

| Campo | Valor |
|---|---|
| Versión | 1.0 |
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

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-11 | Emisión inicial, a pedido del Product Owner, que observó el defecto al revisar la coordinación general del tramo de especificación. | Orquestador SDD |
