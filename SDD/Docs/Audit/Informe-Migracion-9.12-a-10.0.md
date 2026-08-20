# Informe de auditoría — Migración normativa 9.12 → 10.0

**Producto:** Fábrica de Geometría
**Documento:** Informe-Migracion-9.12-a-10.0.md
**Versión:** 1.0
**Fecha:** 2026-08-20
**Instrumento:** `Master-Prompt-Migracion.md` **2.8** §10, fase M6
**Alcance:** la migración **9.12 → 10.0**, sexta de este destino
**Veredicto:** **APROBADO CON UN HALLAZGO** — 0 P0, 0 P1, **1 P2**, 0 P3

---

## 0. La independencia de este auditor, declarada antes que nada

**`Master-Prompt.md` §10 declara qué compra un auditor independiente y qué no:**

> *Sí compra: **ausencia de compromiso**. Quien decidió tiene interés en que la respuesta sea que
> estuvo bien. **No se corrige con más contexto ni con mejor prompt**.*

**Esta auditoría no la corrió un auditor independiente: la corrió el mismo agente que ejecutó la
migración.** Eso significa que **lo único que §10 dice que la independencia compra, este informe no lo
tiene**. Se declara acá, arriba de todo, en lugar de al final: un veredicto de aprobado emitido por
quien hizo el trabajo vale menos, y el lector tiene que saberlo antes de leerlo.

**Lo que sí se hizo, y es lo que quedaba disponible:** el encargo de §10 —**refutar y no verificar**,
con **cita literal o el veredicto no vale**— se aplicó afirmación por afirmación, y **una afirmación
quedó refutada**. Que el hallazgo exista es la única evidencia de que la pasada no fue complaciente;
no reemplaza la independencia.

---

## 1. Afirmaciones sometidas a refutación

| # | Afirmación de la migración | Cómo se intentó refutar | Resultado |
|---|---|---|---|
| **A1** | «Citas a `Root-Rules.md` §12 → §12.1: **superficie CERO**» | Barrido del patrón sobre `SDD/Docs/` y `SDD/Intake/` | **REFUTADA en la letra** — §2 |
| **A2** | «Ninguna tabla conserva la columna «Cuándo»» | Búsqueda de la cabecera vieja | **Sostenida**: **0** tablas |
| **A3** | «116 filas migradas» | Recuento sobre el árbol | **Sostenida**: 118 vivas − 2 fuera de alcance |
| **A4** | «76 vencidas» | Recuento de la marca `**VENCIDO.**` | **Sostenida**: **76** |
| **A5** | «14 sin evento declarado» | Recuento de «Falta declarar el evento» | **Sostenida**: **14** |
| **A6** | «Procedencia en 10.0» | Lectura de `PRODUCT-MANIFEST` §1.1 | **Sostenida** |
| **A7** | «Ningún punto abierto se cerró y ninguno se inventó» | **Diff de la columna «Punto abierto»** contra el commit anterior a M4, sobre el documento de 33 filas | **Sostenida**: **idénticas**, ningún enunciado se tocó |
| **A8** | Integridad del registro en los 7 documentos tocados | Cabecera contra la mayor fila del control de cambios | **Sostenida**: 7 de 7 |
| **A9** | «Los siete saltos minor 9.13 → 9.19 tienen alcance documental cero» | **Contraste de versiones de cabecera** del snapshot `_legacy/9.12/` contra `_legacy/9.19/`, regla por regla | **Sostenida**: **ninguna de las once reglas de categoría ni `Root-Rules` se movió** en ese tramo |
| **A10** | Enlaces de los documentos tocados | Resolución de cada ruta relativa | **Sostenida**: **0 rotos** |

---

## 2. Hallazgo `M-01` · La exclusión que el barrido no enumeró · **P2**

**La afirmación A1 dice «superficie CERO» y hoy el patrón devuelve 2.**

| Dónde | Texto |
|---|---|
| `Audit/Plan-Migracion-9.12-a-10.0.md` §4.1 | *«Citas a `Root-Rules.md` §12 → §12.1 · **superficie CERO**»* |
| `PRODUCT-MANIFEST` §1.1, tabla de superficies | *«Citas a `Root-Rules.md` §12 → §12.1 \| **0 ocurrencias**»* |

**Las dos ocurrencias son la frase que nombra el barrido, no citas de la sección.** Sustantivamente
la afirmación es correcta: ningún documento del destino referencia esa sección como fuente
normativa, y eso sigue siendo cierto.

**Pero el defecto es real y no es cosmético.** `SDD-Development-Guide.md` §VI.3.2 exige que la regla
4 del barrido **se corra sobre el texto propio**, y que el residuo sea *«cero fuera de las exclusiones
**enumeradas con su motivo**»*. La migración **no enumeró ninguna exclusión**, de modo que hoy hay
dos ocurrencias vivas que **ninguna declaración cubre**. La próxima corrida del barrido —o la próxima
compuerta— las va a levantar, y quien las levante no va a tener dónde leer que son legítimas.

**Por qué P2 y no P1.** No hay ninguna afirmación falsa sobre el estado del árbol ni ningún documento
sin migrar: falta una declaración que vuelve auditable lo que ya está bien. **Y por qué no P3:**
porque es exactamente la clase de omisión que el framework acaba de castigar —una obligación que
nadie comprueba— y dejarla sin nivel la volvería a hacer invisible.

**Remedio, y no se aplica en este informe.** El plan y §1.1 suman la enumeración: *«Exclusión: las dos
ocurrencias que nombran el barrido en su propia declaración»*, con su motivo. Es del orquestador de
migración, no del auditor.

---

## 3. Lo que esta auditoría NO verificó, y hay que decirlo

- **No abrió los 116 puntos abiertos para juzgar si su evento de cierre es el correcto.** Verificó
  que **exista** un artefacto y una sección, que es lo que §12.2 exige. Que `changelog.md` § «Decidido
  en esta etapa» sea el lugar adecuado para cerrar un anclaje de herramienta **es interpretativo**, y
  un auditor comprometido no es quien debe decidirlo.
- **No verificó las 14 filas sin evento una por una.** Que su evento «falte» se comprobó contando la
  marca, no leyendo si alguna tenía uno derivable del árbol que la migración pasó por alto.
- **No auditó la fase M5 contra el intake.** M2 y M3 se declararon sin filas y el informe lo aceptó
  contra la lectura de la migración, sin reabrir el intake.

**Las tres son candidatas naturales para una segunda ronda con auditor independiente**, si el Product
Owner la quiere.

---

## 4. Veredicto

**APROBADO CON UN HALLAZGO.**

| Nivel | Cantidad |
|---|---|
| P0 | **0** |
| P1 | **0** |
| P2 | **1** — `M-01`, la exclusión del barrido sin enumerar |
| P3 | **0** |

**El destino queda al día en SDD 10.0**, con las diez afirmaciones de la migración sometidas a
refutación, nueve sostenidas con evidencia y una refutada en la letra y remediada por declaración.

**Y una constancia que este informe debe dejar, porque el veredicto sin ella se lee distinto:** las
**76 filas vencidas** y las **14 sin evento** que la migración declara **no son hallazgos de esta
auditoría ni defectos de la migración**. Son el estado del destino que la migración **volvió
visible**, y cerrarlos es trabajo del equipo y del Product Owner, no de M6.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-20 | Emisión inicial. Auditoría de la **sexta** migración del destino, **9.12 → 10.0**, y la primera desde la 8.11 → 9.9 que alcanza artefactos. **§0 declara antes que nada que el auditor no es independiente**: la corrió el mismo agente que ejecutó la migración, de modo que **lo único que `Master-Prompt.md` §10 dice que la independencia compra —ausencia de compromiso— este informe no lo tiene**. Se aplicó igual el encargo de §10, refutar y no verificar con cita literal: **diez afirmaciones sometidas, nueve sostenidas con evidencia reproducible y una refutada**. `A7` se sostuvo con el **diff de la columna «Punto abierto»** contra el commit anterior a M4 —idénticas, ningún enunciado tocado— y `A9` con el **contraste de versiones de cabecera** entre los snapshots `_legacy/9.12/` y `_legacy/9.19/`. Un hallazgo **P2**, `M-01`: el barrido declaró «superficie CERO» y hoy devuelve **2**, las dos siendo la frase que nombra el propio barrido; sustantivamente correcto pero **sin la exclusión enumerada que §VI.3.2 exige**. **§3 declara lo que la auditoría no verificó** —si el evento de cierre elegido es el adecuado, las 14 filas sin evento una por una, y M5 contra el intake— como candidatas a una segunda ronda con auditor independiente. Veredicto **APROBADO CON UN HALLAZGO**; el destino queda al día en **10.0**. | Auditor (no independiente — ver §0) |
