# 08 · Calidad y pruebas — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.2
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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.1 | Propuesto | Definición de calidad, atributos ISO 25010, **nueve** quality gates y el carácter vinculante de las dos puertas técnicas |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Pirámide con su apartamiento doble, cobertura por componente, tooling, fixtures y los **ocho** escenarios reales |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.0 | Propuesto | Alcance por **momento del producto**, criterios de entrada y salida, **ocho** riesgos |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, garantía ↔ tests, código ↔ tests y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **veintiún** casos de prueba, `TC-12001` a `TC-12021` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.0 | Propuesto | **Treinta y cuatro** criterios, `CV-01` a `CV-34`, incluidos los **seis** tramos de `PT-02` y `PT-03` |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.0 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |
| [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) | 1.0 | Propuesto | Batería de aceptación de un reemplazo de la capa 3, y los **cinco** errores de prueba que romperían el punto de extensión |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | 1.1 | Propuesto | **Quince** sondas: **doce** ancladas en el contrato de la fachada, con su correspondencia con la matriz de `GeometriaFactory-Web`, y **tres** `VER-XX` tomadas de los contratos de verificación de `10-Examples` |

**Nueve artefactos: los siete obligatorios para todo tipo D8, más los dos que los flags de este proyecto de código activan.** Es el único de los tres proyectos de código de nivel topológico 0 que emite los nueve.

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá y por qué las dos puertas técnicas mandan.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — por qué la pirámide se aparta en dos direcciones.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los veintiún casos.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra, con sus cinco tablas.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — qué se verifica en cada momento del producto.
6. [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) — el instrumento de sensado, y contra qué sensa.
7. [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) — cómo se evalúa un reemplazo.
8. [`Criterios-Validacion.md`](Criterios-Validacion.md) y [`Definition-Of-Done.md`](Definition-Of-Done.md).

## 3. Artefactos omitidos y su motivo

**Ninguno.** Esta categoría emite los **siete** artefactos obligatorios para todo tipo D8 y los **dos** condicionados, porque los dos flags que los activan están en true:

| Artefacto condicionado | Flag que lo activa | Fundamento |
| --- | --- | --- |
| [`Guia-Testing-Extensibilidad.md`](Guia-Testing-Extensibilidad.md) | `tiene_extensibilidad` == **true** | `PRODUCT-MANIFEST` §5 lo declara true **sólo en este proyecto de código** de los siete del producto: el punto de extensión del producto es el contrato de la fachada del visor (`PRODUCT-INTAKE` §18) |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | `requiere_maqueta` == **true** | `PRODUCT-MANIFEST` §5 lo declara true. Este proyecto de código **ejecutó su Fase B2 y quedó aprobada**, aunque **sin maqueta propia**: la validación de la fachada se integró en la maqueta de `GeometriaFactory-Web` por decisión del Product Owner, y sus tres artefactos de línea de base viven en la categoría 03 de ese proyecto de código. La matriz la abre AG-08 en la Fase E, con sus filas ancladas en el contrato |

**Las filas `VER-XX` ya están, desde el 2026-08-11.** Esta sección declaraba que no había ninguna «porque `10-Examples` no está emitida para este proyecto de código». Esa categoría se emitió en su pasada de diseño, con **tres** contratos de verificación —`VER-12001` a `VER-12003`, las tres partes del sample **S-1**—, y [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.1 los dio de alta como `SD-12013`, `SD-12014` y `SD-12015`, todas en `Sin verificar`. La matriz pasa de **doce** a **quince** filas. **Ninguna de las doce anteriores cambia.**

## 4. Quality gates configurados

Los nueve de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3. **El texto vinculante es el de esa sección.**

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | El bundle se genera sin errores | Bloqueante |
| QG-02 | **`PT-03`**: el motor dentro del bundle y 0 dependencias de red externa | **Bloqueante, detiene la planificación de `g`** |
| QG-03 | **`PT-02`**: carga, escena, `E-1` con ortoedro, 10 recorridos y sincronización por índice | **Bloqueante, detiene la planificación de `g`** |
| QG-04 | Cero red, medida con los dos movimientos prendidos | Bloqueante, sin gradación |
| QG-05 | Cero persistencia | Bloqueante, sin gradación |
| QG-06 | 6 funciones, 1 nombre global, 0 globales sueltas | Bloqueante |
| QG-07 | 100 % de las piezas no dibujadas enumeradas, 0 sin registro | Bloqueante, sin gradación |
| QG-08 | 7 códigos, ninguno acuñado aguas abajo | Se rechaza en revisión |
| QG-09 | El bundle nunca se edita a mano | Se rechaza en revisión |

**Ningún gate de este proyecto de código es condicionado**, a diferencia de los otros dos de nivel topológico 0. El motivo es que sus umbrales **no salen de valores rotulados [ASUNCIÓN]**: salen del contrato de la fachada y de las dos puertas técnicas, que el intake declara sin rótulo. **La única marca [ASUNCIÓN] que alcanza a este proyecto de código está en §17.7.P.6 y es sobre la forma del gate —expresarlo como automatizable— y no sobre la regla**, que es `RA-02` y ya es criterio de aceptación de la etapa `g`.

**No hay gate de cobertura de líneas ni de mutation score**, y las dos ausencias están declaradas con su motivo en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2. **No hay umbral numérico de fluidez**, y esta categoría **no lo inventa**: es el punto abierto `PA-03` de `05` §11.

## 5. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **7** | `02` §3 |
| Funciones de la fachada | **6** | `Definicion-Contrato-De-Fachada.md` §4; intake §17.7.P.3 |
| Garantías | **7** | `Definicion-Contrato-De-Fachada.md` §3.2 |
| Prohibiciones del contrato | **7** | `Definicion-Contrato-De-Fachada.md` §3.3 |
| Códigos de condición | **7**, en **8** cursos | `Definicion-Contrato-De-Fachada.md` §6 |
| Entradas de diagnóstico de 03 | **13**, `E-VIS-01` a `E-VIS-13` | `03` §3 |
| Propiedades transversales verificables | **6**, con sus condiciones de medición | `02` §6 |
| Tipos de pieza dibujables | **6**: tres volumétricos y tres planos | `Definicion-Contrato-De-Fachada.md` §5.3 |
| Componentes | **6**, dos de ellos fuera de este proyecto de código | `05` §3.1 |
| NFR | **8** | `05` §8 |
| Puntos abiertos de arquitectura | **5** | `05` §11 |
| Historias de usuario | **14**, todas `Must` | `06` `Product-Backlog.md` §3.1 y §4 |
| Tareas técnicas | **18** | `06` `Backlog-Tecnico.md` |
| Puertas técnicas | **2**: `PT-02` y `PT-03` | `PRODUCT-INTAKE` §15 y §17.7.P.8 |
| Casos de prueba de esta categoría | **21** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **34** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Sondas de la matriz de sensado | **12** | [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) §3 |
| Quality gates | **9** | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Escenarios del intake usados como texto | **8 de 8** | `PRODUCT-INTAKE` §20 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`.** Se emitió [`../10-Examples/`](../10-Examples/) en su pasada de diseño, con **tres** contratos de verificación que son las tres partes del sample **S-1**, y [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) sube a **1.1** con las filas `SD-12013` a `SD-12015`, pasando de **doce** a **quince** sondas. La sección §3 conserva su declaración anterior y le agrega el desenlace con su fecha. **Los nueve artefactos y los nueve quality gates siguen siendo los mismos**, y ningún umbral cambia; lo que cambia es que siete de los nueve gates quedan además ejercidos desde afuera del pipeline por un sample, según declara [`../10-Examples/README.md`](../10-Examples/README.md) §3. |
| 1.1 | 2026-08-11 | Actualiza la tabla de artefactos: [`Estrategia-Calidad.md`](Estrategia-Calidad.md), [`Estrategia-Testing.md`](Estrategia-Testing.md) y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) suben a **1.1**. Los tres son de redacción y de trazabilidad: §6 de la estrategia de testing decía que §21 del intake cruza la batería contra **nueve** casos y son **diez**; tres citas entrecomilladas del intake omitían palabras dentro de las comillas; y la matriz no tenía fila para `TC-12020`, que es **la prueba de la puerta `PT-02`**. **Ningún gate, umbral, caso ni recuento de esta sección cambia**, y las dos puertas técnicas siguen siendo vinculantes y no condicionadas. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Visor`. Lista los **nueve** artefactos emitidos —los siete obligatorios más los dos que activan `tiene_extensibilidad` y `requiere_maqueta`—, el orden de lectura, la ausencia de omisiones con el fundamento de cada artefacto condicionado, los **nueve** quality gates con la constancia de que **ninguno es condicionado** y de que la única marca [ASUNCIÓN] que alcanza a este proyecto de código es sobre la forma del gate y no sobre la regla, y la tabla de recuentos con la fuente de cada uno. |
