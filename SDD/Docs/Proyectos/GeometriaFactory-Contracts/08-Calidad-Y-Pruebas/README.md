# 08 · Calidad y pruebas — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** README.md
**Versión:** 1.2
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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.1 | Propuesto | Definición de calidad de un proyecto de código sin comportamiento, atributos ISO 25010 y **nueve** quality gates |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Apartamiento declarado de la pirámide, cobertura por familia de tipos, tooling, fixtures y datos de prueba |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.0 | Propuesto | Alcance, criterios de entrada y salida, **ocho** riesgos y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, `RT-XX` ↔ tests y cobertura por familia |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.1 | Propuesto | Catálogo de **veintidós** casos de prueba, `TC-01` a `TC-22` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.1 | Propuesto | **Veinticinco** criterios, `CV-01` a `CV-25`, con su carácter |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.1 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | 1.0 | Propuesto | **Tres** sondas `VER-XX` tomadas de los contratos de verificación de `10-Examples`, sin ninguna fila de línea de base visual |

**Ocho artefactos.** Los siete de la emisión inicial más la matriz de sensado de deriva, abierta el 2026-08-11 al emitirse `10-Examples`; §3 conserva el motivo por el que estuvo omitida.

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad en un ensamblado que no ejecuta nada.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — por qué no hay pirámide clásica ni cobertura de líneas.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los veintidós casos.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — qué se verifica en qué etapa, y qué queda pendiente de la batería de integración.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) y [`Definition-Of-Done.md`](Definition-Of-Done.md).

## 3. Artefactos omitidos y su motivo, y los que dejaron de estarlo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo exige para `library` **con plugins**. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, no este ensamblado |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Emitido el 2026-08-11**, en 1.0 | **La omisión que esta fila declaraba quedó cerrada.** Se omitía porque `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10», y este proyecto de código cumplía las dos condiciones. **La segunda dejó de cumplirse**: [`../10-Examples/README.md`](../10-Examples/README.md) 1.0 declara **tres** contratos de verificación, `VER-01` a `VER-03`, y de ellos salen las **tres** sondas. La primera sigue en pie —`requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5)—, y por eso la matriz **no tiene ninguna fila de línea de base visual**: es el caso que `Deriva-Rules.md` §2.3 prevé y que §6 exige. La fila se conserva con su desenlace, en lugar de retirarse, para que el motivo de la omisión y el de su cierre queden legibles juntos |

**Precedente de forma dentro de esta categoría.** `GeometriaFactory-Web` tiene su `08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md` desde la Fase B2, porque ejecutó la maqueta; ese documento no es de este proyecto de código y no se toca. Su existencia es justamente lo que hace visible que la omisión de acá está condicionada por dos flags y no por una decisión de esta categoría.

## 4. Quality gates configurados

Los nueve de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3. **El texto vinculante es el de esa sección.**

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Compila sin advertencias | Bloqueante |
| QG-02 | 0 referencias hacia `GeometriaFactory-Domain` | Bloqueante |
| QG-03 | 0 campos capaces de filtrar hash, clave de firma, dirección, ruta o traza | Se rechaza aunque compile |
| QG-04 | 15 códigos vivos y 0 producidos fuera del conjunto | Se rechaza aunque compile |
| QG-05 | 100 % de los tipos ejercitados por integración **[ASUNCIÓN del intake, sobre la forma del gate]** | **Bloqueante** |
| QG-06 | Proyección de listado sin texto original, componentes ni comentario **[ASUNCIÓN derivada]** | **Condicionado** |
| QG-07 | 4 campos en la respuesta de sesión y 0 que impidan operar | Se rechaza aunque compile |
| QG-08 | Despliegue conjunto ante un cambio incompatible | Bloquea la publicación de la etapa |
| QG-09 | 0 tipos que permitan salir de un estado terminal o que habiliten al navegador | Se rechaza aunque compile |

**El único gate condicionado es `QG-06`**, cuyo valor viene rotulado [ASUNCIÓN derivada] de §17.4.P.10: se mide y se registra, y no bloquea la fusión hasta que el Product Owner lo confirme (`BT-18`). **`QG-05` bloquea**: la fila `A-4` del intake §22 declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante», y §17.4.P.6 lo llama «el gate equivalente y bloqueante».

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
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`.** Se emitió [`../10-Examples/`](../10-Examples/) en su pasada de diseño, con **tres** contratos de verificación, y con ellos se abrió [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que pasa a ser el **octavo** artefacto de la sección. La fila de §3 se **conserva** con su desenlace y su fecha en lugar de retirarse. Vale una precisión propia de este proyecto de código: como su verificación no vive en `tests/` de acá sino en la batería de integración de `GeometriaFactory-Api` (`PRODUCT-INTAKE` §17.4.P.6), las tres sondas son hoy el **único** instrumento ejecutable que sensa esta superficie. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia**, y `QG-05` sigue siendo bloqueante y sin sonda que lo sustituya. |
| 1.1 | 2026-08-11 | **`H-02` y `H-08`.** La tabla de gates de §4 declaraba a `QG-05` **condicionado** y atribuía los dos condicionados a la asunción `A-4`. `QG-05` pasa a **bloqueante** —§17.4.P.6 lo llama «equivalente y bloqueante» y §22 `A-4` deja a salvo su carácter— y el único condicionado es `QG-06`, respaldado por §17.4.P.10. Se actualizan las versiones de los artefactos revisados. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Contracts`. Lista los **siete** artefactos emitidos, el orden de lectura, los **dos** omitidos con su motivo y su condición de reapertura, los **nueve** quality gates con su carácter y la tabla de recuentos con la fuente de cada uno. Declara además el precedente de forma que constituye la matriz de sensado de deriva de `GeometriaFactory-Web`, que no es de este proyecto de código y no se toca. |
