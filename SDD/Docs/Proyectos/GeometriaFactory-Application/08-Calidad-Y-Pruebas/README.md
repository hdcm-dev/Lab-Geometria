# 08 · Calidad y pruebas — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.0 | Propuesto | Definición de calidad, atributos ISO 25010, **once** quality gates, papeles y cadencia |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.0 | Propuesto | Pirámide objetivo con su apartamiento declarado, cobertura por componente, tooling, dobles de puerto y el uso de los **ocho** escenarios reales del intake |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.0 | Propuesto | Alcance, criterios de entrada y salida, **ocho** riesgos de calidad y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.0 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, comprobación ↔ tests, invariante ↔ tests y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **treinta y un** casos de prueba, `TC-01` a `TC-31` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.0 | Propuesto | **Veintiocho** criterios numéricos, `CV-01` a `CV-28`, con su carácter bloqueante, condicionado o no exigible |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.0 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá y qué gates existen.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — con qué se prueba, con qué datos y con qué umbrales.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los treinta y un casos, uno por uno.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra: qué caso de prueba cubre qué.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — cuándo se ejecuta cada cosa, por etapa.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) — cuándo se declara validado.
7. [`Definition-Of-Done.md`](Definition-Of-Done.md) — cuándo se declara terminado.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo exige para `library` **con plugins** y lo omite para los tipos sin puntos de extensión. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, y su guía vive en la categoría 08 de `GeometriaFactory-Visor` |
| `Matriz-Sensado-Deriva.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10». Este proyecto de código cumple las dos condiciones: `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5), de modo que no ejecutó la Fase B2 y no tiene línea de base visual ni contrato de datos de maqueta; y su `10-Examples` **no está emitida**, de modo que no hay ningún contrato de verificación del que tomar sondas `VER-XX`. **La omisión no es una matriz vacía**: una matriz sin filas sería un proyecto de código sin instrumento de sensado, y lo que corresponde acá es declarar que las dos fuentes de sondas no existen todavía. **Cuando se emita la categoría 10, la matriz se abre con sus filas `VER-XX`** y esta fila del README se retira |

## 4. Quality gates configurados

Los once de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3, resumidos acá para que se lean de un vistazo. **El texto vinculante es el de esa sección**, no éste.

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Construcción en 0 y sin advertencias | Bloqueante |
| QG-02 | Batería entera en verde, sin pruebas deshabilitadas sin motivo | Bloqueante |
| QG-03 | Cobertura 85 % líneas y 80 % ramas **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-04 | **0** pruebas de esta capa tocan la base de datos real | Bloqueante, puerta propia del intake §17.2.P.8 |
| QG-05 | **1** dependencia saliente al producto y **0** a persistencia, transporte, serialización o marco web | Bloqueante |
| QG-06 | 36 de 36 condiciones alcanzadas y 0 fuera del catálogo | Bloqueante |
| QG-07 | 4 de 4 comprobaciones ejercidas sin base, con 1 prueba de orden | Bloqueante al cierre de etapa |
| QG-08 | A lo sumo 1 unidad de trabajo por caso de uso | Bloqueante |
| QG-09 | 0 componentes de pieza en las consultas de listado | Bloqueante |
| QG-10 | Caso de uso más pesado en menos de 500 ms sobre `E-1`, sin base **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-11 | Ninguna condición prevista viaja como excepción | Se rechaza en revisión |

**Los dos gates condicionados dependen de valores rotulados [ASUNCIÓN] en el intake §22** —asunción `A-3` para la cobertura y `A-5` para los 500 ms—: se miden y se registran, y no bloquean la fusión hasta que el Product Owner los confirme (`BT-18`).

## 5. Recuentos que esta sección sostiene

Se declaran acá para que cualquier lectura posterior pueda verificarlos contra su fuente sin recorrer los siete documentos.

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **11** | `02` §5 |
| Puertos | **4** | `02` §3 |
| Comprobaciones de autorización | **4** | `02` §4 |
| Reglas de negocio del producto | **16** | `02` §6; se enuncian en `GeometriaFactory-Domain` |
| Invariantes vigentes | **9** | `05` §10.3; se enuncian en `GeometriaFactory-Domain` |
| Condiciones distintas catalogadas | **36** | `03` §7.1 |
| Historias de usuario | **32** | `06` `Product-Backlog.md` §3 |
| Tareas técnicas | **21** | `06` `Backlog-Tecnico.md` |
| Componentes | **8** | `05` §3.1 |
| NFR | **9** | `05` §8 |
| Casos de prueba de esta categoría | **31** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **28** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **11** | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Escenarios del intake usados como fixture | **8 de 8** | `PRODUCT-INTAKE` §20 |
| Etapas que este proyecto de código toca | **6** — `a`, `c`, `d`, `e`, `f`, `h` | `06` `Product-Backlog.md` §2 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Application`. Lista los **siete** artefactos emitidos con su versión y su estado, el orden de lectura, los **dos** artefactos omitidos con su motivo —la guía de extensibilidad por `tiene_extensibilidad` false y la matriz de sensado de deriva por no haber Fase B2 ni categoría 10, con la condición de reapertura declarada—, los **once** quality gates con su carácter, y la tabla de recuentos que esta sección sostiene con la fuente de cada uno. |
