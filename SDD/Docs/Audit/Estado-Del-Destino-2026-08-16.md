# Estado del destino — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Estado-Del-Destino-2026-08-16.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-16
**Autor:** Orquestador de reanudación SDD
**Instrumento normativo:** `SDD/Devs/Orchestrator/Master-Prompt-Reanudacion.md` **1.1** (repositorio del framework, sólo lectura)
**Prompt de entrada:** `PROMPTS/PROMPT-Agente-Reanudacion-SDD.md` 1.0
**Lectura:** desde el árbol, sin memoria de sesiones anteriores

---

## 0. Qué es este documento

Es el informe de la fase **R3** del orquestador de reanudación: reconstruye en qué estado quedó este
destino leyendo el árbol, declara sus divergencias, registra la decisión del humano y deja el punto
de continuación para que el trabajo siguiente **no vuelva a deducir lo que acá está deducido**.

**No es un audit.** No abre ninguna categoría documental para juzgar su contenido, no emite veredicto
y no tiene niveles de hallazgo. Declara estado.

**Es la única escritura de la reanudación sobre este destino.**

---

## 1. Estado, por las seis dimensiones

| # | Dimensión | Fuente declarativa | Lectura | Contraste observable | Resultado |
| --- | --- | --- | --- | --- | --- |
| 1 | ¿Hay documentación generada? | — | — | `SDD/Docs/` con nivel Producto, dos unidades de entrega y nueve categorías por unidad | **Sí.** Hay destino que reanudar |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **2.1** §1.1 | Conjunto **8.6** | `IA.SDD/CHANGELOG.md`, conjunto vigente **8.11** | **DIVERGE por diseño.** Cinco versiones publicadas después del cierre de M5, todas el 2026-08-16 |
| 3 | ¿La migración terminó? | `Audit/Informe-Migracion-6.0-a-8.6.md` **6.0** — veredicto **APROBADO**, «COMPLETA Y CERRADA EN SUS DOS EJES» | Fases M1 a M7 hechas; fusión consolidada | `find . -type d -name "_fusion*"` → **0 carpetas** | **Coinciden.** La fusión terminó: 67 grupos consolidados, 18 carpetas retiradas, 9726 líneas absorbidas con 0 sin correspondencia |
| 4 | ¿Qué quedó abierto? | Hallazgos del informe de migración 6.0 | **0 P0, 0 P1, 4 P2 abiertos** | Sin enlaces rotos ni identificadores de forma anterior declarados pendientes | **Coinciden.** Detalle en §3 |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` del producto | Etapa **`b`** | Historial de `git`: etapa **`e`** | **DIVERGEN.** Detalle en §2 |
| 6 | ¿Qué falta para la siguiente? | `SDD/Docs/00-Contexto/Roadmap-Producto.md` **1.6** §2.1 y §5.2 | Etapa **`f`** · Importación y validación | — | Punto de continuación en §6 |

---

## 2. Divergencias

### D-01 · Dimensión 5 · El registro de cambios quedó tres etapas atrás

| | |
| --- | --- |
| **Lectura declarativa** | Etapa **`b`**. `changelog.md` tiene dos encabezados de etapa —`a` y `b`— y termina en «Etapa `b` — Navegación y sistema visual» |
| **Lectura observable** | Etapa **`e`**. Tres etapas fusionadas a `main` sin que el registro las incorpore |
| **Evidencia declarativa** | `changelog.md` líneas 5 y 51, los dos únicos `## Etapa`. Su segunda línea declara la regla que no se cumplió: «Se actualiza **en la rama de la etapa, no después de la fusión** (intake §16 y §17.5.P.7)» |
| **Evidencia observable** | `git log`: `c038687` «etapa c: identidad del administrador y sesión» (PR #34); `06113fd` y `88032c6` etapa `d` (PR #39 y #40); `7ede130` y `e20cc1e` etapa `e` (PR #44 y #45, 2026-08-15). En el código: `WorkEndpoints.cs`, `StudentWorkPanel.razor`, `ClassSubmissionList.razor`, `WorkSubmission.razor`, `WorkView.razor`, `AccountsPanel.razor`, `CommissionAccountEndpoints.cs` |
| **Resolución** | **Gana el observable**: el producto está en la etapa `e`. La divergencia **se declara y no se repara acá** |
| **Consecuencia si no se repara** | Una sesión limpia sin este informe concluye que falta arrancar la etapa `c`, y reconstruye lo que ya está construido |

**No se reparó, y es deliberado.** El humano eligió la salida **B**, que no incluye la reparación.
La divergencia queda abierta y este informe es su registro.

### D-02 · Dimensión 2 · La procedencia declara 8.6 y el framework vigente es 8.11

| | |
| --- | --- |
| **Lectura declarativa** | Conjunto **8.6**, escrito por la fase M5 el 2026-08-16 con la cadena completa verificada |
| **Lectura observable** | Conjunto **8.11** |
| **Evidencia** | `PRODUCT-MANIFEST` 2.1 §1.1 contra `IA.SDD/CHANGELOG.md`, cinco entradas nuevas: 8.7, 8.8, 8.9, 8.10 y 8.11 |
| **Resolución** | **No es un defecto.** Es la divergencia que ocurre por diseño cada vez que el framework publica. La procedencia sigue diciendo la verdad: el destino se migró contra 8.6 |
| **Alcance real** | §4. Ninguna regla de categoría cambió de versión |

---

## 3. Pendientes declarados

Del informe de migración `Informe-Migracion-6.0-a-8.6.md` **6.0**: **0 P0, 0 P1, 4 P2 abiertos**.

| Hallazgo | Nivel | Naturaleza | Qué dice |
| --- | --- | --- | --- |
| **M-04** | P2 | propio, sólo por lectura | El orden de las fases de la migración no se respetó: los documentos se migraron antes que el intake y el manifiesto |
| **M-05** | P2 | propio, por guion | Nueve identificadores de la unidad `Api` sin usar entre `CU-00013` y `CU-00020`, por la consolidación de casos de uso. Declarado **deliberado y correcto**: los identificadores absorbidos no se reciclan |
| **M-06** | P2 | propio, por guion | Dos enlaces rotos por nombre ambiguo, en un informe de `Audit/` |
| **M-07** | P2 | aguas arriba, sólo por lectura | Los casos de uso no habían absorbido el cierre del intake 1.29 sobre dos contratos de operación |

**Cerrados en rondas anteriores:** M-01 (reemplazado por M-08 y M-09), M-02 (absorbido por M-08),
M-03 (`ADR-14001`), M-08, M-09 (`ADR-14002`) y M-10 (los 67 grupos de la fusión).

**Documento superado y declarado como tal:** `SDD/Docs/Handoff-Checkout.md` 1.5, con su cartel de
«superado por la migración 6.0 → 8.6» y su tabla de dónde está hoy lo que inventariaba. No se
reconectó **a propósito**, por el hallazgo M-08: sus recuentos también están viejos y migrarlo a
medias lo habría vuelto falso.

**Carpetas `_legacy/` vivas:** tres archivos de migración —`2026-08-15-migracion-8.2`,
`2026-08-16-consolidacion-8.5` y `2026-08-16-consolidacion-m10`— más los `_legacy/` por carpeta de
las categorías. Son snapshots y no trabajo pendiente.

---

## 4. Diff normativo 8.6 → 8.11, artefacto por artefacto

**Este bloque es el que consume la fase M1 del orquestador de migración**, que lo **verifica** en
lugar de reconstruirlo (`Master-Prompt-Migracion.md` 2.3).

**Método.** Se comparó la tabla de procedencia de `PRODUCT-MANIFEST` 2.1 §1.1, artefacto por
artefacto, contra la versión que hoy declara cada archivo del framework, y se leyó cada entrada del
`CHANGELOG` de 8.7 a 8.11 para determinar el alcance. No se dedujo nada del número de conjunto.

### 4.1 Tabla completa

| Artefacto del framework | Procedencia (8.6) | Vigente (8.11) | Cambió | Severidad para este destino |
| --- | --- | --- | --- | --- |
| `Master-Prompt` | 7.4 | **7.7** | Sí | **Nula sobre artefactos.** Proceso de generación y de audit |
| `Master-Prompt-Migracion` | 2.0 | **2.3** | Sí | **Nula sobre artefactos.** Proceso de migración |
| `Master-Prompt-Reanudacion` | — (no existía) | **1.1** | Alta | **Nula sobre artefactos.** Es el instrumento que produce este informe |
| `Migracion-Rules` | 3.2 | **3.4** | Sí | **Nula sobre artefactos.** Reglas de cómo migrar |
| `PRODUCT-INTAKE-template` | 3.0 | **3.1** | Sí | **Alcanza un artefacto y el destino ya cumple.** Ver §4.3 |
| `PRODUCT-MANIFEST-template` | 5.0 | 5.0 | **No** | Nula |
| `Root-Rules` | 5.2 | 5.2 | **No** | Nula |
| `Rules-Contexto` | 4.1 | 4.1 | **No** | Nula |
| `Rules-Necesidades-Negocio` | 4.0 | 4.0 | **No** | Nula |
| `Rules-Especificacion-Funcional` | 5.0 | 5.0 | **No** | Nula |
| `Rules-UX-UI-DX` | 5.0 | 5.0 | **No** | Nula |
| `Rules-Arquitectura-Tecnica` | 4.0 | 4.0 | **No** | Nula |
| `Rules-Backlog-Tecnico` | 4.0 | 4.0 | **No** | Nula |
| `Rules-Plan-Sprint` | 5.0 | 5.0 | **No** | Nula |
| `Rules-Calidad-Y-Pruebas` | 4.1 | 4.1 | **No** | Nula |
| `Rules-Devops` | 4.0 | 4.0 | **No** | Nula |
| `Rules-Examples` | 6.0 | 6.0 | **No** | Nula |
| `Rules-Documentacion` | 5.0 | 5.0 | **No** | Nula |
| `Intake-Rules` | 4.0 | 4.0 | **No** | Nula |
| `Vocabulario-Rules` | 3.0 | 3.0 | **No** | Nula |
| `Maqueta-Rules` | 4.0 | 4.0 | **No** | Nula |
| `Deriva-Rules` | 5.0 | 5.0 | **No** | Nula |

**Las catorce reglas de categoría y las cuatro transversales están en la misma versión que la
procedencia declara.** Es el renglón que decide el alcance: una regla de categoría que no cambia no
puede obligar a reemitir ningún documento de su categoría. **Cero documentos generados alcanzados.**

### 4.2 Qué trajo cada versión, y por qué no toca a este destino

| Versión | Qué cambió | Por qué no alcanza a este destino |
| --- | --- | --- |
| **8.7** | `PRODUCT-INTAKE-template` 3.0 → 3.1: §17 pasa a **dos tablas de identidad** —la de la unidad de entrega con su D8 y su `redistribuible`, y la de los proyectos de código **sin** esos dos campos— y ocho instrucciones P.1 a P.12 dejan de decir «del proyecto de código» bajo un encabezado que dice «por unidad de entrega» | **El intake de este destino ya cumple.** Ver §4.3 |
| **8.8** | `Migracion-Rules` 3.2 → 3.3, `Master-Prompt-Migracion` 2.0 → 2.1, `Master-Prompt` 7.4 → 7.5 | Son **lecciones de esta misma migración**, escritas después de ejecutarla. Reglas de cómo migrar y dos criterios de audit; ningún artefacto del destino se predica de ellas |
| **8.9** | `SDD-Development-Guide` 1.6 → 1.7, `README` del framework, `Coherencia-Plantilla-Intake-Identidad` 1.0 → 1.1 | Gobiernan **cómo se interviene el framework**. No son instrumentos de ningún destino |
| **8.10** | `Master-Prompt-Reanudacion` 1.0 y `PROMPT-Agente-Reanudacion-SDD` 1.0 nuevos; `Master-Prompt` 7.5 → 7.6 | Agrega el tercer orquestador. Lo estamos ejecutando; no reescribe artefactos |
| **8.11** | `Master-Prompt-Reanudacion` 1.0 → 1.1 (entra R4), `Master-Prompt` 7.6 → 7.7, `Master-Prompt-Migracion` 2.2 → 2.3 | Cómo se entrega el contexto entre orquestadores. Ningún artefacto del destino |

**Ninguna invariante del framework se modificó entre 8.6 y 8.11.** Las cinco entradas lo declaran.

### 4.3 El único punto de contacto: la plantilla de intake 3.1

La 8.7 corrige una contradicción interna de `PRODUCT-INTAKE-template` 3.0: la tabla de identidad de
§17 seguía pidiéndole `tipo_unidad_entrega` (D8) y `redistribuible` **al proyecto de código**, contra
lo que §13.2 de la misma plantilla declara.

**Este intake no tiene el defecto**, verificado sobre el archivo:

| Verificación | Resultado |
| --- | --- |
| `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1 y §17.2 | Declaran `tipo_unidad_entrega` y `redistribuible` **en la unidad de entrega** —`rest-api` para `GeometriaFactory-Api`, `web-monolith` para `GeometriaFactory-Web`, las dos `redistribuible: false`— |
| Tabla de proyectos de código de §17.1 y §17.2 | Lleva la constancia explícita: «`tipo_unidad_entrega` y `redistribuible` **no figuran acá**: son atributos de la unidad de entrega, según §13.1 y §13.2» |
| §13.2 del intake | «**Los proyectos de código no llevan valor D8**, y esta emisión los deja sin él» |

**Por qué ya cumplía.** El `CHANGELOG` 8.7 lo dice: el defecto **se descubrió migrando este
destino**, en la fase M2, y el agente que completaba el intake «emitió la contradicción como hallazgo
aguas arriba en lugar de copiarla». La plantilla 3.1 recoge lo que este intake ya había resuelto.

**Consecuencia para M1:** la diferencia entre la plantilla 3.0 y la 3.1 **no genera trabajo de
migración sobre este intake**. Lo que sí corresponde es hacer verificable la afirmación: que la
procedencia declare 3.1 requiere el paso por M5, no la afirmación de que no hacía falta.

---

## 5. Decisión

| | |
| --- | --- |
| **Salida elegida** | **B · Migrar a la vigente** |
| **Quién la eligió** | El Product Owner |
| **Fecha** | 2026-08-16 |
| **Alcance del salto** | 8.6 → 8.11 |
| **Qué se le entrega al orquestador de migración** | El diff normativo de §4, artefacto por artefacto y con su severidad, para que su fase **M1 lo verifique en lugar de reconstruirlo** (`Master-Prompt-Migracion.md` 2.3) |

**La decisión viaja con este informe.** El orquestador de migración no vuelve a preguntar el salto ni
a rehacer el diff: lo lee de §4 y lo verifica.

**Salvedad declarada por el orquestador de reanudación, y anotada porque el informe es el registro
del estado y no sólo de la decisión.** El diff de §4 no alcanza ningún artefacto de este destino:
las catorce reglas de categoría y las cuatro transversales están en la misma versión que la
procedencia, y el único cambio con superficie —la plantilla de intake 3.1— encuentra al intake ya
conforme. La salvedad se planteó antes de la decisión y el Product Owner la reafirmó. La migración
se ejecuta con su alcance real, que es la actualización de la procedencia con verificación artefacto
por artefacto.

**La divergencia D-01 queda abierta.** La salida **A** —reparar primero— es la única que las demás
dan por hecha, y no se eligió. Migrar con el registro de cambios declarando la etapa `b` no falsea
la migración —la procedencia no se predica del registro de construcción—, pero deja en el árbol la
contradicción que produjo este orquestador. Queda declarada acá.

---

## 6. Punto de continuación

### 6.1 Documental — lo que sigue por la salida elegida

| Qué | Dónde |
| --- | --- |
| Orquestador a invocar | `SDD/Devs/Orchestrator/Master-Prompt-Migracion.md` **2.3** del repositorio del framework |
| Qué recibe | El diff normativo de §4 de este informe, para verificar en M1 |
| Qué gobierna la ejecución | `Migracion-Rules.md` **3.4** |
| Dónde se cierra la procedencia | Fase **M5**, que reescribe `PRODUCT-MANIFEST` §1.1 sólo con la cadena verificada |
| Dónde se audita | Fase **M6**, que emite o actualiza el informe de `SDD/Docs/Audit/` |

### 6.2 De construcción — lo que sigue con independencia del framework

**El avance del código no depende de la versión del framework**, y por eso se declara acá aunque la
salida elegida sea la migración.

| Qué | Detalle |
| --- | --- |
| **Etapa en curso** | `e` · Alta de trabajo y vista de trabajos — F-06, F-07, F-08, F-12. Construida y fusionada; **sin registrar en `changelog.md`** (D-01) |
| **Etapa siguiente** | **`f` · Importación y validación** — F-09, F-10, F-22 |
| **Objetivo de `f`** | Que el texto real del alumno se interprete **al enviar**, muestre sus advertencias y el trabajo pase a estado `Pendiente`, o quede en `Borrador` con sus errores localizados |
| **Puerta de entrada** | La transición **`e` → `f`** de `Roadmap-Producto.md` §5.2, con sus cinco criterios verificables |
| **Puerta de salida** | La transición **`f` → `g`** de §5.2, con sus ocho criterios, incluidas **PT-02 y PT-03 medidas antes de comprometer `g`** |

**Los cinco criterios de la puerta de entrada `e` → `f`:**

- [ ] Un trabajo se carga con nombre, fecha, descripción y texto, y recibe identificador propio y estado
- [ ] Un trabajo queda en estado `Borrador` **con el texto inválido** y se reedita
- [ ] La eliminación por el alumno sólo procede en estado `Borrador` y sólo sobre trabajos propios, verificado **forzando la petición al servicio de datos**, no sólo por la interfaz
- [ ] Un alumno que pide el trabajo de otro recibe «no encontrado»
- [ ] El administrador ve los trabajos agrupados y filtrados por alumno, y su listado **no incluye los que están en estado `Borrador`**

**Documentos que gobiernan la etapa `f`:**

| Documento | Qué aporta |
| --- | --- |
| `SDD/Docs/00-Contexto/Roadmap-Producto.md` **1.6** §2.1 y §5.2 | Objetivo, entregable y los criterios de las dos transiciones |
| `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` §20 | Los **ocho** escenarios de datos que la etapa tiene que ejercer, `E-1` a `E-8` |
| `SDD/Docs/Producto/Pipeline-Producto.md` | Orden topológico de los proyectos de código |
| `SDD/Docs/Producto/Norma-De-Nomenclatura.md` | Los identificadores se declaran **antes** de escribirlos (corolario 4 de §6.1) |
| `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/` | Los casos de uso de la interpretación y la verificación |
| `SDD/Docs/Producto/Adrs/` y `SDD/Docs/Producto/Contratos-Inter-Unidad/` | Decisiones y contratos entre las dos unidades |
| `changelog.md` | **Se actualiza en la rama de la etapa, no después de la fusión** (intake §16 y §17.5.P.7). Es la regla que D-01 registra incumplida tres veces |

**Observación sobre las puertas, verificada sobre `scripts/`.** Existen `verify-stage-c.sh`,
`verify-navigation.sh`, `verify-visual-system.sh` y `verify-explicit-configuration.sh`. **No hay guion
para las etapas `d` ni `e`**: sus criterios de transición no tienen verificación automatizada en el
árbol. Se declara como estado observado, no como hallazgo: este informe no audita.

---

## 7. Criterios de aceptación de la reanudación

- [x] Las **seis dimensiones** están resueltas, cada una con su fuente citada (§1).
- [x] Las tres dimensiones con contraste observable **se contrastaron**, y el resultado está declarado aunque coincidan (§1, dimensiones 2, 3 y 5).
- [x] Toda divergencia está declarada con **las dos lecturas y la evidencia de cada una**, y ninguna se resolvió en este prompt (§2).
- [x] El informe existe, declara la salida elegida y lleva su **bloque de punto de continuación completo** (§5 y §6).
- [x] La salida es **B**, y el informe lleva el **diff normativo** que el orquestador siguiente consume (§4), con la decisión viajando con él (§5).
- [x] **No se escribió nada del destino fuera de este informe.**
- [x] La salida la eligió el humano, sobre las cuatro presentadas.

---

## 8. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Primera reanudación de este destino, con `Master-Prompt-Reanudacion.md` 1.1. Seis dimensiones resueltas y las tres con contraste observable contrastadas. **Dos divergencias declaradas**: `D-01`, el registro de cambios en la etapa `b` con el código en la `e` —tres etapas fusionadas sin actualizar el único documento que declara el avance—, y `D-02`, la procedencia en 8.6 con el framework en 8.11, que no es defecto. **Diff normativo 8.6 → 8.11 artefacto por artefacto**: las catorce reglas de categoría y las cuatro transversales **sin cambio de versión**, cambian los tres orquestadores, `Migracion-Rules` y `PRODUCT-INTAKE-template` 3.0 → 3.1, cuya corrección **este intake ya cumple** porque el defecto se descubrió migrándolo. Salida elegida por el Product Owner: **B, migrar a la vigente**, con la salvedad de alcance declarada en §5 y con `D-01` abierta. | Orquestador de reanudación SDD |
