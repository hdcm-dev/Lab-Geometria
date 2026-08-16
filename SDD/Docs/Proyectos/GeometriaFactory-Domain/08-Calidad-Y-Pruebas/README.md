# 08 · Calidad y pruebas — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Quality gates configurados](#4-quality-gates-configurados)
- [5. Recuentos que esta sección sostiene](#5-recuentos-que-esta-sección-sostiene)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.0 | Propuesto | Definición de calidad, atributos ISO 25010 priorizados, **ocho** quality gates, papeles y cadencia |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Pirámide objetivo, cobertura por componente, tooling, fixtures y el uso de los **ocho** escenarios reales del intake |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.0 | Propuesto | Alcance, criterios de entrada y salida, **siete** riesgos de calidad y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, invariante ↔ tests y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **veintisiete** casos de prueba, `TC-02001` a `TC-02027` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.0 | Propuesto | **Veintidós** criterios numéricos, `CV-01` a `CV-22`, con su carácter bloqueante, condicionado o no exigible |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.0 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | 1.0 | Propuesto | **Tres** sondas `VER-XX` tomadas de los contratos de verificación de `10-Examples`, sin ninguna fila de línea de base visual |

**Ocho artefactos.** Los siete de la emisión inicial más la matriz de sensado de deriva, que se abrió el 2026-08-11 al emitirse `10-Examples`; §3 conserva el motivo por el que estuvo omitida.

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá y qué gates existen.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — con qué se prueba, con qué datos y con qué umbrales.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los veintisiete casos, uno por uno.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra: qué caso de prueba cubre qué.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — cuándo se ejecuta cada cosa, por etapa.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) — cuándo se declara validado.
7. [`Definition-Of-Done.md`](Definition-Of-Done.md) — cuándo se declara terminado.

## 3. Artefactos omitidos y su motivo, y los que dejaron de estarlo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo exige para `library` **con plugins** y lo omite para los tipos sin puntos de extensión. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, y su guía vive en la categoría 08 de `GeometriaFactory-Visor` |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Emitido el 2026-08-11**, en 1.0 | **La omisión que esta fila declaraba quedó cerrada.** Se omitía porque `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10», y este proyecto de código cumplía las dos condiciones: `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5) y su `10-Examples` no estaba emitida. **La segunda condición dejó de cumplirse**: [`../10-Examples/README.md`](../10-Examples/README.md) 1.0 declara **tres** contratos de verificación, `VER-02001` a `VER-02003`, y de ellos se toman las **tres** sondas de la matriz. La primera condición sigue en pie, y por eso la matriz **no tiene ninguna fila de línea de base visual**. Es exactamente el caso que `Deriva-Rules.md` §2.3 prevé —«cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: […] poblada solo con sondas `VER-XX`», con la elisión marcada; el fragmento suprimido asigna la apertura a AG-08 en la Fase E, y la matriz declara en su §1 por qué acá la abre AG-10— y que §6 exige. La fila se conserva con su desenlace, en lugar de retirarse, para que el motivo de la omisión y el de su cierre queden legibles juntos |

## 4. Quality gates configurados

Los ocho de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3, resumidos acá para que se lean de un vistazo. **El texto vinculante es el de esa sección**, no éste.

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Construcción en 0 y sin advertencias | Bloqueante |
| QG-02 | Batería entera en verde, sin pruebas deshabilitadas sin motivo | Bloqueante |
| QG-03 | Cobertura 90 % líneas y 85 % ramas **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-04 | 0 dependencias salientes | Bloqueante |
| QG-05 | 42 de 42 condiciones alcanzadas y 0 fuera del catálogo | Bloqueante |
| QG-06 | 9 de 9 invariantes ejercidos sin dobles | Bloqueante al cierre de etapa |
| QG-07 | Batería completa en menos de 10 segundos **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-08 | Ninguna condición prevista viaja como excepción | Se rechaza en revisión |

**Los dos gates condicionados dependen de valores rotulados [ASUNCIÓN] en el intake §22** —asunción `A-3` para la cobertura y `A-5` para el tiempo—: se miden y se registran, y no bloquean la fusión hasta que el Product Owner los confirme (`BT-02015`).

## 5. Recuentos que esta sección sostiene

Se declaran acá para que cualquier lectura posterior pueda verificarlos contra su fuente sin recorrer los siete documentos.

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **13** | `02` §3 |
| Reglas de negocio | **16** | `02` §4 |
| Invariantes vigentes | **9** | `02` §4 y `Definicion-Modelo-De-Dominio.md` §4 |
| Condiciones de error distintas | **42** | `03` §6.1 |
| Historias de usuario | **27** | `06` `Product-Backlog.md` §3 |
| Tareas técnicas | **16** | `06` `Product-Backlog.md` §4 |
| Componentes | **5** | `05` §3.1 |
| NFR | **6** | `05` §8 |
| Casos de prueba de esta categoría | **27** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **22** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **8** | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Escenarios del intake usados como fixture | **8 de 8** | `PRODUCT-INTAKE` §20 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-11 | **Corrección del hallazgo P2-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** La fila de `Matriz-Sensado-Deriva.md` de §3 citaba `Deriva-Rules.md` §2.3 **elidiendo sin marca** dos fragmentos, uno de ellos «la abre AG-08 en la Fase E», que asigna la titularidad. La elisión queda ahora **marcada** y el fragmento suprimido se dice en texto llano, con la remisión a la §1 de la matriz, que declara por qué acá la abre AG-10. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia.** Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor: corrige la forma de una cita. |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`.** Se emitió [`../10-Examples/`](../10-Examples/) en su pasada de diseño, con **tres** contratos de verificación, y con ellos se abrió [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que pasa a ser el **octavo** artefacto de la sección. La fila de §3 se **conserva** con su desenlace y su fecha, en lugar de retirarse, para que el motivo de la omisión y el de su cierre queden legibles juntos: la condición «sin categoría 10» dejó de cumplirse y la condición «sin Fase B2» sigue en pie, de modo que la matriz nace **sin ninguna fila de línea de base visual**, que es el caso de `Deriva-Rules.md` §2.3. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia.** |
| 1.1 | 2026-08-11 | Actualiza la tabla de artefactos: [`Estrategia-Testing.md`](Estrategia-Testing.md) y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) suben a **1.1**. Los dos son de redacción y de trazabilidad: §6 de la estrategia decía que §21 del intake cruza la batería contra **nueve** casos y son **diez**, y la matriz declaraba que ningún `TC-XX` deja de referenciar un `CU-XX`, una `RN-XX`, un `INV-XX` o un NFR cuando `TC-02025` y `TC-02027` trazan a una ADR, a una tarea técnica y a un gate. **Ningún gate, umbral, caso ni recuento de esta sección cambia.** Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Domain`. Lista los **siete** artefactos emitidos con su versión y su estado, el orden de lectura, los **dos** artefactos omitidos con su motivo —la guía de extensibilidad por `tiene_extensibilidad` false y la matriz de sensado de deriva por no haber Fase B2 ni categoría 10, con la condición de reapertura declarada—, los **ocho** quality gates con su carácter, y la tabla de recuentos que esta sección sostiene con la fuente de cada uno. |
