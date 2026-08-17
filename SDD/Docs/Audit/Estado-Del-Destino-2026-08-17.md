# Estado del destino — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Estado-Del-Destino-2026-08-17.md
**Versión:** 1.0
**Estado:** Emitido
**Fecha:** 2026-08-17
**Autor:** Orquestador de reanudación SDD
**Responsable de mantenerlo:** el orquestador de reanudación que lo emite; una vez emitido no se
actualiza, se lo supera con el informe de la reanudación siguiente
**Instrumento normativo:** `SDD/Devs/Orchestrator/Master-Prompt-Reanudacion.md` **1.2** (repositorio del framework, sólo lectura)
**Prompt de entrada:** `PROMPTS/PROMPT-Agente-Reanudacion-SDD.md` 1.1
**Informe anterior:** `Estado-Del-Destino-2026-08-16.md` 1.0, superado por éste
**Lectura:** desde el árbol, sin memoria de sesiones anteriores

---

## 0. Qué es este documento

Es el informe de la fase **R3** del orquestador de reanudación: reconstruye en qué estado quedó este
destino leyendo el árbol, declara sus divergencias, registra la decisión del humano y deja el punto
de continuación para que el trabajo siguiente **no vuelva a deducir lo que acá está deducido**.

**No es un audit.** No abre ninguna categoría documental para juzgar su contenido, no emite veredicto
y no tiene niveles de hallazgo. Declara estado.

**Es la única escritura de la reanudación sobre este destino**, salvo lo que la salida elegida ejecute
con su propia confirmación —acá, la reparación de la salida **A**, registrada en §7—.

---

## 1. Estado, por las seis dimensiones

| # | Dimensión | Fuente declarativa | Quién la mantiene | Lectura | Contraste observable | Resultado |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | ¿Hay documentación generada? | — | — | — | `SDD/Docs/` con nivel Producto, dos unidades de entrega y nueve categorías por unidad | **Sí.** Hay destino que reanudar |
| 2 | ¿Contra qué versión del framework? | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **2.2** §1.1 | La generación y la migración | Conjunto **8.11** | `IA.SDD/CHANGELOG.md`, conjunto vigente **9.1** | **DIVERGE, y esta vez sí alcanza artefactos.** `D-03` a `D-06` en §2 |
| 3 | ¿La migración terminó? | `Audit/Informe-Migracion-8.6-a-8.11.md` **2.0** — veredicto **APROBADO**, ronda 2 | La migración | Fases M1 a M7 hechas, migración **cerrada** | `find . -type d -name "_fusion*"` → **0 carpetas** | **Coinciden.** La fusión terminó |
| 4 | ¿Qué quedó abierto? | Hallazgos del informe 8.6 → 8.11 §10, más los del 6.0 → 8.6 | Quien cierra cada hallazgo, nombrado en el hallazgo | **0 P0, 0 P1, 7 P2 abiertos** | Sin enlaces rotos fuera de `Audit/`: 4654 de 4658 resuelven | **Coinciden.** Detalle en §3 |
| 5 | ¿En qué etapa de construcción va? | `changelog.md` del producto | **Sin responsable nombrado** — es la divergencia `D-06` | Etapa **`g`**, declarada «en curso» | Historial de `git`: último commit `aacfc93`, etapa `g` | **Coinciden.** `D-01` del informe anterior quedó **reparada** |
| 6 | ¿Qué falta para la siguiente? | `SDD/Docs/00-Contexto/Roadmap-Producto.md` **1.7** §2.1 y §5.2 | Quien cierra cada etapa | Cerrar `g`, después `h` | — | Punto de continuación en §6 |

**La dimensión 5 dejó de divergir.** El informe anterior la declaró tres etapas atrás; el commit
`a159b6e` repuso `c`, `d` y `e` desde los mensajes de confirmación, **marcadas como repuestas**, y
`f` y `g` se escribieron en su rama. El contraste de esta reanudación las encuentra alineadas.

---

## 2. Divergencias

### D-03 · Dimensión 2 · La procedencia declara 8.11 y el framework vigente es 9.1

| | |
| --- | --- |
| **Lectura declarativa** | Conjunto **8.11**, escrito por la fase M5 de la migración anterior el 2026-08-16 |
| **Lectura observable** | Conjunto **9.1** |
| **Evidencia** | `PRODUCT-MANIFEST` 2.2 §1.1 contra `IA.SDD/CHANGELOG.md`: seis entradas nuevas —8.12, 8.13, 8.14, 8.15, 8.16, 8.17, 9.0 y 9.1— |
| **Resolución** | **Es la divergencia por diseño**, la que ocurre cada vez que el framework publica. La procedencia sigue diciendo la verdad: el destino se migró contra 8.11 |
| **Alcance real** | §4. **Y acá está la diferencia con el salto anterior**: el 8.6 → 8.11 no tocaba ningún artefacto porque ninguna regla de categoría había cambiado de versión; en el 8.11 → 9.1 **cambiaron todas**, y tres cambios tienen superficie medida sobre este árbol —`D-04`, `D-05` y `D-06`— |

### D-04 · Dimensión 2, superficie · La cabecera de nivel no llegó al corpus

| | |
| --- | --- |
| **Lectura declarativa** | Las diez reglas de categoría, en su §4.1, definen la cabecera de **todo documento generado** y desde la **8.17** empieza con `**Unidad de entrega:** {{Nombre-Unidad-Entrega}}` |
| **Lectura observable** | **313 documentos vivos** del corpus siguen abriendo con `**Proyecto de código:** <nombre>`. Sólo **71** tienen la forma vigente |
| **Evidencia declarativa** | `Rules-Backlog-Tecnico.md` **4.3** §4.1 y `Rules-Arquitectura-Tecnica.md` **4.3** §4.1, entre las diez; `CHANGELOG` 8.17, «cero de veintiséis usaba `Unidad de entrega:`» |
| **Evidencia observable** | `grep -rl '^\*\*Proyecto de código:\*\*' SDD/Docs` excluido `_legacy/`: **292** bajo `Unidades-Entrega/`, **14** bajo `Producto/` y **7** en `Audit/`. Los valores que lleva la cabecera vieja son los **siete proyectos de código**: `Domain` 152, `Infrastructure` 115, `Contracts` 96, `Application` 95, `Web` 90, `Visor` 62, `Api` 56 |
| **Por qué no es una sustitución de texto** | La cabecera vieja nombra el **eje de construcción** —siete valores— y la vigente el **eje de entrega** —dos—. El valor correcto **no se deriva del viejo**: se deriva de la carpeta en la que el documento vive. `GeometriaFactory-Contracts` es el caso que lo demuestra: el intake §13.3 lo declara compartido por las **dos** unidades, de modo que su nombre viejo no determina ninguna |
| **Resolución** | **Gana el observable.** Se declara acá y se repara en §7 |

### D-05 · Dimensión 2, superficie · El artefacto de arquitectura conserva el nombre anterior

| | |
| --- | --- |
| **Lectura declarativa** | `Rules-Arquitectura-Tecnica.md` **4.3** §2.1 y §4.2 nombran el artefacto `Arquitectura-Unidad-Entrega.md` |
| **Lectura observable** | Las dos unidades tienen `05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`, con **161 referencias** apuntándole fuera de `Audit/` |
| **Evidencia** | `CHANGELOG` 8.17: el renombre se había hecho en §2.1 y no se propagó ni dentro del propio archivo de la regla |
| **Gravedad** | **El criterio de aceptación del audit verifica la existencia del nombre vigente.** Con el nombre actual, el artefacto reprobaría un audit corrido hoy. `Migracion-Rules.md` §111 declara que un renombre de artefacto es el único cambio que **ningún diff de versiones puede inferir**: hay que propagarlo a mano |
| **Resolución** | **Gana el observable.** Se declara acá y se repara en §7 |

### D-06 · Dimensión 2, superficie · Ninguna fuente declarativa del avance nombra a su responsable

| | |
| --- | --- |
| **Lectura declarativa** | `Rules-Devops.md` **4.2** §4.3 suma a `Estrategia-Versionado.md` los ítems **7** —el registro del avance con **responsable nombrado**— y **8** —el instrumento preferido, que es el subproducto del acto—. `Master-Prompt-Reanudacion.md` **1.2** §1.1 R1: toda fuente declarativa nombra a su responsable **en el propio documento** |
| **Lectura observable** | Los dos `Estrategia-Versionado.md` —`GeometriaFactory-Api` y `GeometriaFactory-Web`— **no tienen los ítems 7 ni 8**, y `changelog.md` sigue declarando el **cuándo** —«se actualiza en la rama de la etapa»— sin declarar el **quién** |
| **Evidencia** | `grep -n 'responsable' SDD/Docs/Unidades-Entrega/*/09-Devops/Estrategia-Versionado.md` → sin resultados; `changelog.md` línea 3 |
| **Lo que lo hace notable** | **Es la regla que nació de este mismo destino.** La 8.14 la escribió a partir de la `D-01` del informe anterior —el registro tres etapas atrás—, y el destino que la originó todavía no la cumple |
| **Resolución** | **Gana el observable.** Se declara acá y se repara en §7 |

### Verificado conforme, y conviene decirlo

| Cambio de la 9.0 | Estado en este destino |
| --- | --- |
| Campo `Proyecto de código principal` → **`Unidad de entrega principal`** en `PRODUCT-MANIFEST` §1 | **Ya conforme.** El manifiesto 2.2 §1 lo declara con la forma vigente |
| El mismo campo en el README raíz del corpus | **Ya conforme.** `SDD/Docs/README.md` **1.6** lo declara con la forma vigente, por la reemisión que cerró `N-01` |
| `orden-topologico` partido en `orden-topologico-de-compilacion` y `orden-de-integracion` | **Sin superficie**: el corpus no usa el nombre de campo, cita «orden topológico» como prosa del intake §13 |

---

## 3. Pendientes declarados

**0 P0, 0 P1, 7 P2 abiertos.** Ninguno bloquea.

Del informe `Informe-Migracion-8.6-a-8.11.md` **2.0** §10:

| Hallazgo | Nivel | Naturaleza | Qué dice |
| --- | --- | --- | --- |
| **N-02** | P2 | propio, sólo por lectura | El plan de migración clasificó por categoría y no por documento. Apartamiento **declarado en §4 del plan, con su motivo** |
| **N-03** | P2 | propio, por guion | Cuatro enlaces rotos en `Audit/`, dos de ellos ya titulados por `M-06` |
| **N-05** | P2 | propio, sólo por lectura | Cuatro citas apuntan a documentos que la consolidación absorbió. No se les reescribió la carpeta a propósito: habría convertido un error visible en uno invisible |

Del informe `Informe-Migracion-6.0-a-8.6.md` **6.0**, que el salto siguiente no reabrió:

| Hallazgo | Nivel | Qué dice |
| --- | --- | --- |
| **M-04** | P2 | El orden de las fases de la migración no se respetó: los documentos se migraron antes que el intake y el manifiesto |
| **M-05** | P2 | Nueve identificadores de `Api` sin usar entre `CU-00013` y `CU-00020`. **Deliberado y correcto**: los identificadores absorbidos no se reciclan |
| **M-06** | P2 | Dos enlaces rotos por nombre ambiguo, en un informe de `Audit/` |
| **M-07** | P2 | Aguas arriba: los casos de uso no habían absorbido el cierre del intake 1.29 sobre dos contratos de operación |

**Cerrados desde el informe anterior:** `N-01` por la reemisión del README raíz del corpus a **1.6**;
`N-04` por 41 referencias reconectadas en 27 documentos, con su registro
`Migracion-8.11-Registro-Reconexion-Etiquetas.json`; y **`D-01`**, la divergencia del registro de
cambios, reparada fuera del alcance de la migración.

**Observaciones aguas arriba, las dos cerradas:**
`Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md` **4.0** —las tres decisiones tomadas y las tres
escrituras aplicadas— y `Observacion-Sincronizacion-Escena-Arbol.md` **2.0**, resuelta por
`ADR-08007`.

---

## 4. Diff normativo 8.11 → 9.1, artefacto por artefacto

**Método.** Se comparó la tabla de procedencia de `PRODUCT-MANIFEST` 2.2 §1.1, fila por fila, contra
la versión que hoy declara la cabecera de cada archivo del framework, y se leyó cada entrada del
`CHANGELOG` de 8.12 a 9.1 para determinar el alcance. **No se dedujo nada del número de conjunto**, y
las tres superficies encontradas se **contrastaron sobre el árbol** en lugar de inferirse.

### 4.1 Tabla completa

| Artefacto del framework | Procedencia (8.11) | Vigente (9.1) | Cambió | Severidad para este destino |
| --- | --- | --- | --- | --- |
| `Master-Prompt` | 7.7 | **8.0** | Sí | **Nula sobre artefactos.** §3.4 pasa a tres bloques; gobierna la generación, no el árbol generado |
| `Master-Prompt-Migracion` | 2.3 | 2.3 | **No** | Nula |
| `Master-Prompt-Reanudacion` | 1.1 | **1.2** | Sí | **Nula sobre artefactos.** Es el instrumento que produce este informe. Su §1.1 R1 sí se refleja en `D-06` |
| `Root-Rules` | 5.2 | **6.0** | Sí | **Alcanzaba el README raíz, y el destino ya cumple.** Ver §4.3 |
| `Rules-Contexto` | 4.1 | **4.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-Necesidades-Negocio` | 4.0 | **4.2** | Sí | **Cabecera** (`D-04`) |
| `Rules-Especificacion-Funcional` | 5.0 | **5.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-UX-UI-DX` | 5.0 | **5.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-Arquitectura-Tecnica` | 4.0 | **4.3** | Sí | **Cabecera** (`D-04`) **y renombre de artefacto** (`D-05`) |
| `Rules-Backlog-Tecnico` | 4.0 | **4.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-Plan-Sprint` | 5.0 | **5.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-Calidad-Y-Pruebas` | 4.1 | **4.4** | Sí | **Cabecera** (`D-04`) |
| `Rules-Devops` | 4.0 | **4.5** | Sí | **Cabecera** (`D-04`) **y los ítems 7 y 8 de `Estrategia-Versionado.md`** (`D-06`) |
| `Rules-Examples` | 6.0 | **6.3** | Sí | **Cabecera** (`D-04`) |
| `Rules-Documentacion` | 5.0 | **5.3** | Sí | **Cabecera** (`D-04`) |
| `Intake-Rules` | 4.0 | **4.1** | Sí | **Nula sobre artefactos.** Mapeo de derivación en tres tablas; el manifiesto derivado ya está conforme |
| `Vocabulario-Rules` | 3.0 | **3.1** | Sí | **Nula sobre artefactos.** Glosario y cita del despacho |
| `Maqueta-Rules` | 4.0 | **4.1** | Sí | **Nula sobre artefactos.** La Fase B2 está confirmada y cerrada |
| `Deriva-Rules` | 5.0 | **5.2** | Sí | **Nula sobre artefactos.** Ruta de salida del prompt de despacho |
| `Migracion-Rules` | 3.4 | **3.5** | Sí | **Nula sobre artefactos.** Reglas de cómo migrar |
| `PRODUCT-INTAKE-template` | 3.1 | **3.4** | Sí | **Nula sobre este intake.** §16 y el checklist de la Parte C; el intake ya declara los dos ejes separados |
| `PRODUCT-MANIFEST-template` | 5.0 | **6.0** | Sí | **Alcanzaba el campo del principal, y el destino ya cumple.** Ver §4.3 |

**Todo cambió de versión salvo `Master-Prompt-Migracion`.** Es exactamente el renglón inverso al del
salto anterior, y por eso la salida de esta reanudación no puede ser la de la anterior: allá el diff
no tenía superficie, acá tiene tres.

### 4.2 Qué trajo cada versión, y qué toca

| Versión | Qué cambió | Qué alcanza a este destino |
| --- | --- | --- |
| **8.12** | `Intake-Rules` 4.0 → 4.1, `Master-Prompt` 7.7 → 7.8, `PRODUCT-INTAKE-template` 3.1 → 3.2, `Vocabulario-Rules` 3.0 → 3.1. Además, `_legacy/` corrido un lugar en cuatro carpetas del framework | **Nada.** El mapeo de derivación en tres tablas describe lo que este manifiesto ya declara. El archivo `_legacy/` es del framework |
| **8.13** | `Master-Prompt` 7.8 → 7.9: la tabla del plan maestro de §7 emitía a `SDD/Docs/Proyectos/`; once reglas de categoría y `Deriva-Rules`, patch; `Root-Rules` 5.2 → 5.3; 39 concordancias de género | **Nada.** El propio `CHANGELOG` lo acota: «la migración no lee esta tabla; un destino migrado quedó correctamente bajo `Unidades-Entrega/`». Verificado: este árbol está bajo `Unidades-Entrega/` |
| **8.14** | `Master-Prompt-Reanudacion` 1.1 → 1.2 con sus tres reglas; **`Rules-Devops` 4.1 → 4.2, §4.3 ítems 7 y 8**; `SDD-Development-Guide` 1.12 → 1.13 | **`D-06`.** Los dos `Estrategia-Versionado.md` y el `changelog.md` |
| **8.15** | `Master-Prompt` 7.9 → 7.10 (ocho citas a una sección inexistente, cinco de ellas origen de flags de gating); `Rules-Devops` 4.2 → 4.3; `SDD-User-Guide`; `Marco-Teorico-SDD` | **Nada sobre artefactos.** Los flags de gating de este destino ya están resueltos y su documentación emitida |
| **8.16** | `Coherencia-Referencias-Derivadas` 1.1 → 1.2, con la medición de 759 documentos | **Nada.** Ninguna regla, plantilla ni orquestador cambia |
| **8.17** | **Las diez reglas de categoría, patch: la cabecera de §4.1 pasa a `**Unidad de entrega:**`**; **`Rules-Arquitectura-Tecnica`: el artefacto se llama `Arquitectura-Unidad-Entrega.md`**; `Master-Prompt` 7.10 → 7.11; `PRODUCT-INTAKE-template` 3.2 → 3.3; `Root-Rules` 5.3 → 5.4 | **`D-04` y `D-05`.** Son los dos cambios con superficie más grande del salto |
| **9.0** | `PRODUCT-MANIFEST-template` 5.0 → **6.0** y `Root-Rules` 5.4 → **6.0**: el campo pasa a `Unidad de entrega principal`; `Master-Prompt` 7.11 → **8.0**; once reglas y `Deriva`/`Maqueta`, patch; `PRODUCT-INTAKE-template` 3.3 → 3.4; `Migracion-Rules` 3.4 → 3.5 | **Ya conforme**, verificado artefacto por artefacto en §4.3 |
| **9.1** | `SDD-Development-Guide` 1.14 → 1.15: el barrido por forma anterior se declara como patrón y se corre | **Nada.** Gobierna cómo se interviene el framework |

**Ninguna invariante del framework se modificó entre 8.11 y 9.1.** Las ocho entradas lo declaran.

### 4.3 Lo que la 9.0 tocaba y este destino ya cumplía

| Verificación | Resultado |
| --- | --- |
| `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §1, campo del principal | Declara `Unidad de entrega principal` → `GeometriaFactory-Api`. **Conforme a la plantilla 6.0** |
| `SDD/Docs/README.md` **1.6**, tabla de cabecera | Declara `Unidad de entrega principal` → `GeometriaFactory-Api`. **Conforme a `Root-Rules` 6.0** |
| `orden-topologico` partido en dos campos | El corpus **no usa el nombre de campo**: `Pipeline-Producto.md` §193 y el intake §15 citan «orden topológico» como prosa. Sin superficie de renombre |

**Por qué ya cumplía.** No por casualidad: la reemisión del README a 1.6 que cerró `N-01` en la ronda
2 de la migración anterior partió §7 en tres y reescribió §2 **sobre los dos ejes**, y el manifiesto
se había reescrito en la M5 del mismo salto. Los dos quedaron escritos con el vocabulario que la 9.0
formalizó un día después.

---

## 5. Decisión

| | |
| --- | --- |
| **Salida elegida** | **A · Reparar primero** |
| **Quién la eligió** | El Product Owner |
| **Fecha** | 2026-08-17 |
| **Qué abarca** | `D-04`, `D-05` y `D-06`, las tres divergencias con superficie sobre el árbol |
| **Qué sigue después** | **Volver a R0** sobre el árbol reparado, con este informe ya escrito |

**Por qué A y no B.** Las tres divergencias son trabajo documental real, no un número de versión. La
salida **A** es la única que las otras tres dan por hecha: elegir **B**, **C** o **D** con `D-04`,
`D-05` y `D-06` abiertas significa trabajar sobre un árbol que declara el nivel equivocado en 313
documentos y publica un artefacto con un nombre que el audit vigente reprueba.

**Lo que la reparación NO hace, y es la línea que la separa de una migración.** No toca
`PRODUCT-MANIFEST` §1.1: **la procedencia sigue declarando 8.11 al terminar**, porque actualizarla
exige la fase **M5** con su verificación, y esta reanudación no es una migración. Reparar la forma de
los artefactos y declarar la procedencia son dos actos distintos, y confundirlos es lo que
`Master-Prompt-Reanudacion.md` §4 tipifica como afirmar algo que nadie comprobó.

---

## 6. Punto de continuación

### 6.1 Documental — lo que sigue por la salida elegida

| Qué | Dónde |
| --- | --- |
| Reparación | §7 de este informe, con su alcance acordado y su registro |
| Después de reparar | **Volver a R0** sobre el árbol reparado. La segunda pasada es barata: este informe ya está |
| Lo que quedará abierto igual | `D-03`, la procedencia en 8.11 contra el framework 9.1. Su cierre es de la fase **M5** de `Master-Prompt-Migracion.md` **2.3**, con el diff de §4 como insumo verificable |

### 6.2 De construcción — lo que sigue con independencia del framework

**El avance del código no depende de la versión del framework**, y por eso se declara acá aunque la
salida elegida sea la reparación documental.

| Qué | Detalle |
| --- | --- |
| **Etapa en curso** | **`g` · Visualización y árbol** — F-11, F-13, F-25. Declarada «en curso» en `changelog.md`, coincidente con el historial |
| **Lo que ya está cumplido de `g`** | **`PT-02` y `PT-03` miden y PASAN** (`Medicion-Puertas-Tecnicas-PT-02-PT-03.md` **2.0**), que era la condición de `f` → `g` para comprometer la etapa. `F-11` cumplida —el árbol colapsable, armado del **texto** y no de las piezas—; `F-13` intacta en las dos direcciones; `ADR-08008` con la superficie HTTP descrita y el explorador que no se publica solo |
| **Lo que falta para cerrar `g`** | Los **siete criterios** de la transición `g` → `h` de `Roadmap-Producto.md` **1.7** §5.2 |
| **Etapa siguiente** | **`h` · Circuito de revisión del administrador** — F-21, F-23, F-24. **Cierra el alcance comprometido** |

**Los siete criterios de la puerta `g` → `h`:**

- [ ] Las tres figuras del escenario semilla se dibujan, **ortoedro incluido**
- [ ] Navegar entre trabajos ida y vuelta diez veces no degrada la visualización
- [ ] Procesar el mismo trabajo dos veces produce la misma disposición, predicada de la **posición derivada del índice** y no de la orientación en un instante
- [ ] Durante la interacción tridimensional **no hay ni una sola petición** originada por la visualización
- [ ] El árbol y la escena se sincronizan por índice de pieza
- [ ] El administrador abre cualquier trabajo que ve y encuentra **exactamente lo mismo** que vio el alumno
- [ ] **Los dos movimientos automáticos de `F-25` se gobiernan por separado**: órbita de cámara y giro de piezas se encienden y apagan de forma independiente, los dos se detienen mientras se arrastra, y su estado inicial lo fija la pieza pública pasando dos valores de verdad

**Documentos que gobiernan el cierre de `g` y la etapa `h`:**

| Documento | Qué aporta |
| --- | --- |
| `SDD/Docs/00-Contexto/Roadmap-Producto.md` **1.7** §2.1 y §5.2 | Objetivo, entregable y los criterios de las dos transiciones |
| `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.7 P.1 y P.10, §19, §20 | Los criterios de aceptación por bloque técnico y los ocho escenarios de datos |
| `SDD/Docs/Producto/Medicion-Puertas-Tecnicas-PT-02-PT-03.md` **2.0** | Las dos puertas medidas, con `scripts/verify-viewer-lifecycle.sh` como reejecutable |
| `SDD/Docs/Producto/Adrs/` | `ADR-08006` el visor recibe piezas, `ADR-08007` el aviso de selección, `ADR-08008` la superficie HTTP |
| `SDD/Docs/Producto/Norma-De-Nomenclatura.md` | Los identificadores se declaran **antes** de escribirlos (corolario 4 de §6.1) |
| `changelog.md` | Se actualiza **en la rama de la etapa, no después de la fusión**. Tras `D-06` reparada, además **nombra a su responsable** |

**Observación sobre las puertas, verificada sobre `scripts/`.** Hay `verify-stage-c.sh`,
`verify-navigation.sh`, `verify-visual-system.sh`, `verify-explicit-configuration.sh` y
`verify-viewer-lifecycle.sh`. **No hay guion para las etapas `d`, `e`, `f` ni `g`**: los siete
criterios de la puerta `g` → `h` no tienen verificación automatizada en el árbol. Se declara como
estado observado, no como hallazgo: este informe no audita.

---

## 7. Reparación de la salida A

**Ejecutada el 2026-08-17**, en la rama `docs/reparacion-reanudacion-9.1`, con el alcance acordado
con el Product Owner antes de tocar nada. **No se tocó `PRODUCT-MANIFEST` §1.1**: la procedencia
sigue declarando **8.11**, porque actualizarla es acto de la fase M5 y no de esta reanudación.

### 7.1 Lo acordado, y lo que se decidió de cada divergencia

| Decisión | Qué se acordó |
| --- | --- |
| **Forma de `D-04`** | **Reemplazar sin conservar**: la cabecera queda con un solo campo, exactamente el que las diez reglas §4.1 definen. El proyecto de código sale de la cabecera |
| **Alcance de `D-04`** | **Los 313**, `Audit/` incluido |
| **Entrega** | Rama y confirmaciones, una por divergencia. La fusión es del Product Owner |

### 7.2 `D-04` — la cabecera de nivel · **313 documentos, residuo cero**

| Zona | Documentos | Cabecera resultante | Cómo se resolvió el valor |
| --- | --- | --- | --- |
| `Unidades-Entrega/<U>/` | **292** | `**Unidad de entrega:** <U>` | **De la carpeta, no del valor anterior.** Es la única forma correcta: `GeometriaFactory-Contracts` compone las **dos** unidades (intake §13.3), de modo que su nombre viejo no determinaba ninguna |
| `Producto/` | **14** | `**Producto:** Fábrica de Geometría` | Son de **nivel Producto** —`Contratos-Inter-Unidad/` y las ADR de producto— y ninguna regla de categoría los gobierna: ponerles `Unidad de entrega:` habría sido declarar algo falso |
| `Audit/` | **7** | `**Unidad de entrega:** <U>` | Por la matriz de composición §13.3: `Domain`, `Application` e `Infrastructure` → `GeometriaFactory-Api`; `Web` → `GeometriaFactory-Web`. **Ninguno era `Contracts`**, el único compartido, así que no hubo ambigüedad que resolver |

**Verificación:** `grep -rl '^\*\*Proyecto de código:\*\*' SDD/Docs` excluido `_legacy/` → **0
archivos**.

**Apartamiento declarado: se barrió `Audit/`.** Las seis clases de exclusión del framework declaran
intocables los registros históricos fechados, y estos siete son informes de auditoría con su fecha.
**Se barrieron por decisión explícita del Product Owner**, planteada la salvedad antes de ejecutar.
Lo que acota el daño es que **el nombre de archivo conserva el proyecto de código** que cada informe
auditó —`B-02-03-GeometriaFactory-Domain-r3.md`—, de modo que el alcance del informe no se perdió:
cambió de lugar.

### 7.3 `D-05` — el renombre del artefacto de arquitectura · **2 archivos, 284 referencias**

| Qué | Resultado |
| --- | --- |
| Renombre | `Arquitectura-Proyecto-Codigo.md` → **`Arquitectura-Unidad-Entrega.md`** en las dos unidades, con `git mv` para que el historial lo siga |
| Referencias en enlace reconectadas | **278** |
| Referencias en prosa reconectadas | **6**: la autorreferencia `**Documento:**` de los dos artefactos, los dos `README.md` de `05-Arquitectura-Tecnica/`, `Producto/Norma-De-Nomenclatura.md` §807 y `Producto/Vista-Producto.md` §35 |
| **Referencias deliberadamente NO tocadas** | **17 hacia `_legacy/`**, en 9 documentos vivos de `Producto/`. Apuntan a `_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`, **que conserva su nombre**: reescribirlas habría roto 17 enlaces que hoy resuelven |
| Menciones en prosa de `Audit/` y de `Handoff-Checkout.md` | **No tocadas.** Describen dónde estaba el documento cuando se auditó, con rutas bajo `SDD/Docs/Proyectos/` que ya no existen. Renombrarles el archivo sin poder arreglarles la carpeta las dejaría **ni ciertas ni históricas** |
| **Verificación mecánica** | **143 enlaces** apuntan al nombre nuevo y **los 143 resuelven**. **0 rotos** |

**El reemplazo se hizo consciente del destino del enlace y no por patrón.** Es la lección de `N-04`
del salto anterior: resolver por destino y no por cadena. Un `sed` global habría roto las 17
referencias al snapshot.

### 7.4 `D-06` — el responsable de la fuente declarativa · **3 documentos**

| Documento | Qué se escribió |
| --- | --- |
| `GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md` **2.0 → 2.1** | Nueva **§11**, con los ítems 7 y 8 de `Rules-Devops.md` §4.3 |
| `GeometriaFactory-Web/09-Devops/Estrategia-Versionado.md` **2.0 → 2.1** | Nueva **§9**, con el mismo contenido ajustado a la unidad |
| `changelog.md` | La cabecera pasa de declarar **sólo el cuándo** a declarar **quién actualiza, quién verifica y cuándo**, y que **manda el historial** cuando no coinciden |

**Lo que la sección nueva declara, y que no es cómodo:**

- **El responsable** es el equipo de desarrollo en la rama de la etapa; **lo verifica el Product
  Owner** en la revisión del pull request, que el intake §15 ya declaraba punto de control
  bloqueante. La obligación deja de ser una oración sin sujeto.
- **El instrumento que manda es el historial del repositorio**, por ser el único subproducto del
  acto —fusionar escribe el nombre de la rama sin que nadie se acuerde—. `changelog.md` se conserva
  porque dice *qué significó* lo que se fusionó, pero deja de ser la fuente que decide.
- **Se declara un incumplimiento que la reparación no cierra:** las dos estrategias fijan «etiqueta
  por etapa cerrada» con objetivo del 100 %, y **`git tag` devuelve cero en todo el repositorio**.
  Cerrarlo es crear las etiquetas o retirar el objetivo, y las dos son decisiones de la categoría 09
  con su propio acto. Se declara en lugar de enumerar un instrumento sin decir que no existe.

### 7.5 Observado al reparar, y no reparado

| Qué | Dónde | Por qué no se tocó |
| --- | --- | --- |
| `Vista-Producto.md` §35 sigue ubicando los artefactos bajo `Proyectos/<Nombre-Proyecto-Codigo>/` | `SDD/Docs/Producto/Vista-Producto.md` | Es la **forma anterior del layout de la 8.0**, no una de las tres divergencias acordadas. Se le corrigió el nombre de archivo porque estaba en la misma oración; la ruta queda declarada acá para el R0 siguiente |
| Los 7 P2 abiertos de §3 | `Audit/` | Ninguno es de las tres divergencias, y ninguno bloquea |

**Ninguno de los dos se reparó al pasar**, que es el anti-patrón que `Master-Prompt-Reanudacion.md`
§7 nombra: retomar y corregir en el mismo acto deja al humano sin la foto de cómo estaba.

---

## 8. R0, segunda pasada sobre el árbol reparado

**La salida A vuelve a R0**, y esta pasada es barata porque el informe ya está. Se corre sobre el
resultado de §7, con la misma tabla de seis dimensiones y los mismos contrastes.

| # | Dimensión | Lectura | Contraste observable | Resultado |
| --- | --- | --- | --- | --- |
| 1 | ¿Hay documentación generada? | — | `SDD/Docs/` poblada | **Sí** |
| 2 | ¿Contra qué versión? | `PRODUCT-MANIFEST` **8.11** | Framework **9.1** | **`D-03` sigue abierta, y es correcto**: la procedencia no se toca sin M5. **`D-04`, `D-05` y `D-06` cerradas** |
| 3 | ¿La migración terminó? | Informe 8.6 → 8.11 **APROBADO** | `_fusion/` → **0** | **Coinciden** |
| 4 | ¿Qué quedó abierto? | 0 P0, 0 P1, **7 P2** | 4 enlaces rotos, **los 4 en `Audit/`** y los 4 ya declarados por `N-03` y `M-06` | **Coinciden.** Sin hallazgos nuevos |
| 5 | ¿En qué etapa va? | `changelog.md`: etapa **`g`**, en curso | `git log`: etapa **`g`** | **Coinciden** |
| 6 | ¿Qué falta? | Cerrar `g`, después `h` | — | §6.2 |

**Verificación de las tres reparaciones, medida y no afirmada:**

| Divergencia | Contraste | Resultado |
| --- | --- | --- |
| `D-04` | `grep -rl '^**Proyecto de código:**' SDD/Docs` sin `_legacy/` | **0 archivos** |
| `D-05` | `find SDD/Docs -name 'Arquitectura-Proyecto-Codigo.md'` sin `_legacy/` | **0 archivos** |
| `D-06` | La sección nueva en los dos `Estrategia-Versionado.md`; el responsable en `changelog.md` | **2 de 2** y **presente** |
| **Compuerta de enlaces** | Los **4693** enlaces relativos vivos de `SDD/Docs/` | **4689 resuelven, 4 rotos**, los cuatro preexistentes en `Audit/`. **Ninguno roto al reparar** |

**Lo que la segunda pasada deja abierto, y por qué se detiene acá.** Queda `D-03`: la procedencia
declara 8.11 y el framework vigente es 9.1. **Ya no tiene superficie sobre ningún artefacto** —las
tres que tenía se repararon—, de modo que cerrarla es exactamente el caso que §4 del orquestador
nombra: **actualizar la procedencia con verificación artefacto por artefacto**, que es acto de la
fase **M5** y no de esta reanudación. El diff de §4 y la verificación de §7 son su insumo.

**La reanudación termina acá por decisión de alcance.** Retomar R2 y elegir **B** sobre el árbol
reparado es la continuación natural, y es una decisión nueva del Product Owner sobre un estado que
ahora sí está limpio.

---

## 9. Criterios de aceptación de la reanudación

- [x] Las **seis dimensiones** están resueltas, cada una con su fuente y su responsable citados (§1).
- [x] Las tres dimensiones con contraste observable **se contrastaron**, y el resultado está declarado aunque coincidan (§1, dimensiones 2, 3 y 5).
- [x] Toda divergencia está declarada con **las dos lecturas y la evidencia de cada una** (§2), y ninguna se resolvió en la fase de diagnóstico.
- [x] El informe existe, declara la salida elegida y lleva su **bloque de punto de continuación completo** (§5 y §6).
- [x] El **diff normativo** 8.11 → 9.1 está artefacto por artefacto y con su severidad (§4), disponible para el orquestador de migración cuando corresponda.
- [x] **No se escribió nada del destino fuera de este informe** en las fases R0 a R3.
- [x] La salida la eligió el humano, sobre las cuatro presentadas.
- [x] La reparación de la salida **A** ejecutada y registrada en §7, y R0 corrido de nuevo sobre el árbol reparado (§8).

---

## 10. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-17 | Emisión inicial. **Segunda reanudación** de este destino, con `Master-Prompt-Reanudacion.md` **1.2**. Supera a `Estado-Del-Destino-2026-08-16.md`. Seis dimensiones resueltas; **`D-01` quedó reparada** y la dimensión 5 coincide. **Cuatro divergencias**: `D-03`, la procedencia en 8.11 contra el framework en **9.1**, y sus tres superficies medidas sobre el árbol —`D-04`, la cabecera de nivel en **313 documentos vivos**; `D-05`, `Arquitectura-Proyecto-Codigo.md` contra el nombre vigente, con **161 referencias**; y `D-06`, ninguna fuente declarativa del avance nombra a su responsable, que es **la regla nacida de la `D-01` de este mismo destino**—. **Diff normativo 8.11 → 9.1 artefacto por artefacto**: todo cambió de versión salvo `Master-Prompt-Migracion`, el renglón inverso al del salto anterior. Se declara conforme sin trabajo lo que la 9.0 tocaba —el campo del principal en manifiesto y README raíz—, verificado y no supuesto. Salida elegida por el Product Owner: **A, reparar primero**, **ejecutada en la misma sesión** (§7): la cabecera de nivel barrida en los 313 con residuo cero, el artefacto de arquitectura renombrado con **284 referencias reconectadas por destino** y **17 preservadas hacia el snapshot**, y los ítems 7 y 8 escritos en las dos estrategias de versionado y en `changelog.md`, con el incumplimiento de las etiquetas —**cero en todo el árbol**— declarado y no cerrado. **Segunda pasada de R0** en §8: las tres divergencias cierran, la compuerta da **4689 de 4693 enlaces** y los 4 rotos son los preexistentes de `Audit/`. Queda abierta `D-03`, ya **sin superficie**, para la fase M5. | Orquestador de reanudación SDD |
