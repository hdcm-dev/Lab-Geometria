# 08 · Calidad y pruebas — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.0 | Propuesto | Definición de calidad de un proyecto de código sin comportamiento, atributos ISO 25010 y **nueve** quality gates |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.0 | Propuesto | Apartamiento declarado de la pirámide, cobertura por familia de tipos, tooling, fixtures y datos de prueba |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.0 | Propuesto | Alcance, criterios de entrada y salida, **ocho** riesgos y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.0 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, `RT-XX` ↔ tests y cobertura por familia |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **veintidós** casos de prueba, `TC-01` a `TC-22` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.0 | Propuesto | **Veinticinco** criterios, `CV-01` a `CV-25`, con su carácter |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.0 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad en un ensamblado que no ejecuta nada.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — por qué no hay pirámide clásica ni cobertura de líneas.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los veintidós casos.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — qué se verifica en qué etapa, y qué queda pendiente de la batería de integración.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) y [`Definition-Of-Done.md`](Definition-Of-Done.md).

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo exige para `library` **con plugins**. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, no este ensamblado |
| `Matriz-Sensado-Deriva.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10», y este proyecto de código cumple las dos condiciones: `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5), de modo que no tiene línea de base visual ni contrato de datos de maqueta; y su `10-Examples` **no está emitida**, de modo que no hay contratos de verificación de los que tomar sondas `VER-XX`. **La omisión no es una matriz vacía**: `Deriva-Rules.md` §2.3 declara que una matriz sin filas es un proyecto de código sin instrumento de sensado, y lo que corresponde acá es declarar que las dos fuentes de sondas no existen. **Cuando se emita la categoría 10 la matriz se abre con sus filas `VER-XX`** y esta fila se retira |

**Precedente de forma dentro de esta categoría.** `GeometriaFactory-Web` tiene su `08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md` desde la Fase B2, porque ejecutó la maqueta; ese documento no es de este proyecto de código y no se toca. Su existencia es justamente lo que hace visible que la omisión de acá está condicionada por dos flags y no por una decisión de esta categoría.

## 4. Quality gates configurados

Los nueve de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3. **El texto vinculante es el de esa sección.**

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Compila sin advertencias | Bloqueante |
| QG-02 | 0 referencias hacia `GeometriaFactory-Domain` | Bloqueante |
| QG-03 | 0 campos capaces de filtrar hash, clave de firma, dirección, ruta o traza | Se rechaza aunque compile |
| QG-04 | 15 códigos vivos y 0 producidos fuera del conjunto | Se rechaza aunque compile |
| QG-05 | 100 % de los tipos ejercitados por integración **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-06 | Proyección de listado sin texto original, componentes ni comentario **[ASUNCIÓN derivada]** | **Condicionado** |
| QG-07 | 4 campos en la respuesta de sesión y 0 que impidan operar | Se rechaza aunque compile |
| QG-08 | Despliegue conjunto ante un cambio incompatible | Bloquea la publicación de la etapa |
| QG-09 | 0 tipos que permitan salir de un estado terminal o que habiliten al navegador | Se rechaza aunque compile |

**Los dos gates condicionados dependen de la asunción `A-4` del intake §22**: se miden y se registran, y no bloquean la fusión hasta que el Product Owner los confirme (`BT-18`).

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Contratos de uso | **8** | `02` §3 |
| Restricciones transversales | **11** | `02` §6 |
| Familias de tipos | **8** | `05` §3.1 |
| NFR | **7** | `05` §8 |
| Códigos de error vivos | **15** | `05` §7 y `CU-06` §10 |
| Identificadores de código emitidos | **18**, con **3** retirados | `03` §3.2 |
| Señales declaradas que no son error | **3** | `03` §3.3 |
| Entradas de diagnóstico de construcción | **15**, `DXC-01` a `DXC-15` | `03` §3.1 |
| Historias de usuario | **22**, **21** comprometidas | `06` `Product-Backlog.md` §4 |
| Tareas técnicas | **18** | `06` `Product-Backlog.md` §4 |
| Reglas de negocio del producto | **16**, ninguna redactada acá | `05` §10.3 |
| Casos de prueba de esta categoría | **22** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **25** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **9** | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Escenarios del intake alcanzados | **8 de 8** | `PRODUCT-INTAKE` §20 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Contracts`. Lista los **siete** artefactos emitidos, el orden de lectura, los **dos** omitidos con su motivo y su condición de reapertura, los **nueve** quality gates con su carácter y la tabla de recuentos con la fuente de cada uno. Declara además el precedente de forma que constituye la matriz de sensado de deriva de `GeometriaFactory-Web`, que no es de este proyecto de código y no se toca. |
