# Informe de auditoría de migración — 9.10 → 9.12

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-9.10-a-9.12.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Auditor:** Auditor independiente, invocado desde cero
**Responsable de mantenerlo:** el auditor que lo emite; se supera con el informe de la migración siguiente
**Instrumento normativo:** `Master-Prompt.md` **8.4** §10, con los criterios de `Migracion-Rules.md` **3.9** §6
**Alcance:** la migración normativa 9.10 → 9.12, ejecutada el 2026-08-17
**Veredicto:** **APROBADO** — **0 P0, 0 P1, 0 P2, 1 P3 cerrado**

---

## 1. Qué se auditó, y qué lo hace distinto del salto anterior

**Otra migración de alcance documental cero, pero por un motivo que había que comprobar.** En el
salto 9.9 → 9.10 sólo un artefacto se había movido y era fácil ver que no tocaba nada. Acá **se
movieron dieciocho**, incluidas **las once reglas de categoría** y `Root-Rules` — y aun así el
alcance es cero.

**Ese es exactamente el caso donde un audit sirve.** Una afirmación de «no toca nada» sobre dieciocho
artefactos movidos es la clase de conclusión que hay que poder refutar, y por eso el auditor **no leyó
el resumen del orquestador: rehizo la medición**.

---

## 2. Estado de las fases

| Fase | Qué hizo | Estado |
| --- | --- | --- |
| **M0** | Reconocimiento: procedencia **9.10**, origen disponible en `_legacy/9.10/`, 459 documentos vivos | **Cerrada** |
| **M1** | Diff artefacto por artefacto, con verificación mecánica del alcance. Plan emitido | **Cerrada** |
| **M2**, **M3**, **M4** | **Sin filas**, declarado | **No aplican** |
| **M5** | Procedencia **9.10 → 9.12**, manifiesto 3.1 → 3.2, más una corrección propia declarada | **Cerrada** |
| **M6** | Este informe | **Cerrada** |

---

## 3. La verificación de alcance, rehecha

**El auditor corrió el `diff` por su cuenta**, del snapshot `_legacy/9.10/` contra los archivos vivos,
sobre las quince reglas, filtrando la cabecera de versión, la fila de control de cambios y las filas
de la tabla de anti-patrones:

| Regla | Líneas cambiadas fuera de la tabla de anti-patrones |
| --- | --- |
| `Rules-Contexto`, `Rules-Necesidades-Negocio`, `Rules-Especificacion-Funcional`, `Rules-UX-UI-DX`, `Rules-Arquitectura-Tecnica`, `Rules-Backlog-Tecnico`, `Rules-Plan-Sprint`, `Rules-Calidad-Y-Pruebas`, `Rules-Devops`, `Rules-Examples`, `Rules-Documentacion` | **0** cada una |
| `Root-Rules`, `Deriva-Rules`, `Maqueta-Rules`, `Migracion-Rules` | **0** cada una |

**Cero en las quince.** Lo que cambió en todas es lo mismo: la columna **`Detección`** que la 9.11
agregó a cada fila de anti-patrón, con su marca `[enumerable]` o `[interpretativo]`.

**Qué se verificó específicamente, porque es lo que decidiría lo contrario:**

| Si hubiera cambiado… | ¿Cambió? |
| --- | --- |
| **§4.1**, la cabecera que todo documento generado copia | **No.** Es la sección que en la 8.17 sí cambió y costó 313 documentos |
| **§4.2**, las secciones obligatorias de cada artefacto | **No** |
| Los **criterios de aceptación** de §6 de cada regla | **No** |
| Los **nombres de artefacto** | **No.** Cero renombres; ninguna de las dos entradas es major ni tiene bloque «Impacto sobre destinos existentes» |

**Una tabla de anti-patrones describe qué evitar al generar o auditar, no la forma del artefacto.**
Cambiarla no obliga a reemitir nada ya emitido. **El auditor coincide con el plan, y llegó por
separado: cero documentos alcanzados.**

---

## 4. El artefacto nuevo

**`Catalogo-De-Criterios.md` 1.1** entró en el framework con la 9.11. El plan decidió **listarlo en la
procedencia con su naturaleza declarada** —índice, no regla— en lugar de omitirlo.

**El auditor lo verificó y avala la decisión.** El documento declara de sí mismo que «no define ningún
criterio: dice dónde vive cada uno», y no gobierna ningún artefacto del destino. **Listarlo cuesta una
fila; omitirlo habría obligado a la próxima migración a resolver la misma pregunta, quizá distinto.**

---

## 5. Hallazgos P0 propios de la migración

| P0 posible | Resultado |
| --- | --- |
| Contenido inventado | **No** |
| Sección exigida rellenada con contenido inferido | **No** |
| **Procedencia reescrita con migración parcial** | **No.** No hay filas que migrar; la verificación quedó escrita antes de tocar la tabla |
| Corrección manual pisada sin declarar | **No** |
| Estado previo no archivado | **No.** El manifiesto **3.1** quedó archivado con sus enlaces re-derivados |
| Fila del plan sin resolver y sin declararse | **No** |

**Cero P0, cero P1, cero P2.**

---

## 6. Hallazgo

### B-01 · P3 · propio · sólo por lectura — Un recuento del manifiesto quedó desactualizado, y lo escribió el orquestador · **CERRADO**

| | |
| --- | --- |
| **Qué se encontró** | La fila de reglas transversales del manifiesto **3.1** decía «este árbol atravesó **tres** migraciones normativas» y a continuación **enumeraba cuatro** |
| **Cuándo entró** | En la M5 del salto **9.9 → 9.10**, el mismo día: se agregó el cuarto salto a la enumeración y **no se actualizó la palabra que los cuenta** |
| **Quién lo cometió** | **El propio orquestador de migración**, en el documento cuya integridad venía auditando |
| **Por qué importa más de lo que parece** | Es la tercera vez que este destino registra la misma forma —«la decisión llega y el recuento sobrevive»—, y las dos anteriores fueron de documentos escritos por humanos. **Ésta la escribió el agente, y la encontró el agente**, contrastando la fila contra su propia enumeración antes de reescribirla |
| **Qué se hizo** | Se corrigió en M5 —pasa a **cinco**, enumeradas— **y se declaró** en `Plan-Migracion-9.10-a-9.12.md` §6 y acá, en lugar de arreglarse en silencio dentro de una fila que de todos modos había que tocar |
| **Por qué P3** | Ningún enlace roto, ninguna afirmación normativa apoyada en el número, y la enumeración adyacente decía la verdad |
| **Estado** | **CERRADO** |

---

## 7. Compuerta mecánica

| Medición | Resultado |
| --- | --- |
| Enlaces relativos del árbol vivo | **4698** |
| Resuelven | **4694** |
| Rotos | **4**, los preexistentes de `Audit/` declarados por `N-03` |
| **Rotos nuevos** | **0** |
| Documentos del destino modificados | **1** — el manifiesto |

---

## 8. Veredicto

**APROBADO. Migración COMPLETA Y CERRADA**, con la procedencia en **9.12**, el conjunto vigente del
framework al momento de esta emisión.

**0 P0, 0 P1, 0 P2, 1 P3 cerrado.** El destino queda **al día**.

---

## 9. Lo que dejan las dos migraciones de alcance cero

| Migración | Artefactos que cambiaron | Documentos tocados |
| --- | --- | --- |
| 6.0 → 8.6 | Muchos, con cambio de nivel | Todo el árbol |
| 8.6 → 8.11 | Ninguna regla de categoría | **0** |
| 8.11 → 9.9 | Los veintidós | **7** |
| 9.9 → 9.10 | Uno | **0** |
| **9.10 → 9.12** | **Dieciocho** | **0** |

**Las cinco filas dicen lo mismo y esta última lo dice mejor que ninguna: el número de artefactos que
se movieron no predice el trabajo.** Dieciocho artefactos movidos produjeron cero documentos tocados,
y un salto anterior con veintidós produjo siete — no por la cantidad, sino porque **una de las
veintidós había cambiado su §4.1**.

**La conclusión operativa, para la próxima reanudación:** mantenerse al día es barato **cuando se
mide**. Lo caro no es migrar seguido: es migrar sin abrir los archivos, o dejar correr el desfase
hasta que una regla de forma cambie sin que nadie lo note. **Este salto costó un documento
modificado; el que traía una regla de forma costó trescientos trece, y lo encontró la reanudación y
no la migración.**

---

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. Auditoría de la migración normativa **9.10 → 9.12**, quinta del destino y **segunda consecutiva de alcance documental cero** — pero por un motivo que **había que comprobar**: se movieron **dieciocho** artefactos, incluidas las once reglas de categoría y `Root-Rules`. El auditor **rehízo el `diff`** del snapshot `_legacy/9.10/` contra los archivos vivos y obtuvo **cero líneas cambiadas fuera de la tabla de anti-patrones en las quince reglas**, verificando específicamente que **§4.1, §4.2, los criterios de aceptación y los nombres de artefacto no se movieron**. **Cero renombres.** Avala listar `Catalogo-De-Criterios` **1.1** en la procedencia con su naturaleza de índice declarada. Un hallazgo: **`B-01` (P3, cerrado)**, un recuento del manifiesto que decía «tres migraciones» y enumeraba cuatro, **escrito por el propio orquestador** en el salto anterior, corregido y declarado en lugar de arreglarse en silencio. Compuerta: **4694 de 4698**, **0 rotos nuevos**, **1 documento modificado**. Veredicto **APROBADO**; el destino queda **al día en 9.12**. §9 deja la comparación de las cinco migraciones: **el número de artefactos movidos no predice el trabajo**. | Auditor independiente |
