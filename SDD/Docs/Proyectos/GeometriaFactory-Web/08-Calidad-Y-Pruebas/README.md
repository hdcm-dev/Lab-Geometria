# 08 · Calidad y pruebas — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. La matriz de sensado de deriva, que ya existía](#3-la-matriz-de-sensado-de-deriva-que-ya-existía)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Quality gates configurados](#5-quality-gates-configurados)
- [6. Recuentos que esta sección sostiene](#6-recuentos-que-esta-sección-sostiene)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Emitido por | Propósito |
| --- | --- | --- | --- | --- |
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.1 | Propuesto | Fase E, AG-08 | Definición de calidad, atributos ISO 25010, **once** quality gates, **tres** puertas técnicas, papeles y cadencia |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Fase E, AG-08 | Pirámide objetivo con su apartamiento declarado, cobertura por unidades contables, tooling, datos, y la **relación con la matriz de sensado** con la resolución de sus 61 filas |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.1 | Propuesto | Fase E, AG-08 | Alcance, criterios de entrada y salida, **diez** riesgos de calidad y plan por etapa sobre las ocho |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Fase E, AG-08 | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, restricción transversal ↔ tests y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.1 | Propuesto | Fase E, AG-08 | Catálogo de **treinta y cinco** casos de verificación, `TC-01` a `TC-35` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.1 | Propuesto | Fase E, AG-08 | **Treinta y cinco** criterios, `CV-01` a `CV-35`, con su carácter |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.1 | Propuesto | Fase E, AG-08 | **DoD canónica** del proyecto de código, en cuatro capas |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **1.2** | Propuesto | **Fase B2, AG-03M** | **Ya existía antes de esta fase.** Las **61** sondas `SD-01` a `SD-61` contra la línea de base visual aprobada. Ver §3 |

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá, qué gates y qué puertas técnicas existen.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — con qué se verifica, con qué datos, y **cómo se integra la matriz de sensado**.
3. [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) — las 61 sondas contra la línea de base aprobada.
4. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los treinta y cinco casos de verificación, uno por uno.
5. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra: qué caso de verificación cubre qué.
6. [`Plan-Pruebas.md`](Plan-Pruebas.md) — cuándo se ejecuta cada cosa, por etapa.
7. [`Criterios-Validacion.md`](Criterios-Validacion.md) — cuándo se declara validado.
8. [`Definition-Of-Done.md`](Definition-Of-Done.md) — cuándo se declara terminado.

## 3. La matriz de sensado de deriva, que ya existía

**Esta carpeta no nació con la Fase E.** [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) se emitió al cerrar la **Fase B2**, con la maqueta aprobada por el Product Owner, y su propia §2 declara que era el **único** artefacto de la categoría emitido por esa fase y que «cuando AG-08 genere la categoría, incorpora esta matriz en lugar de crear una nueva».

**Eso es lo que hizo esta Fase E, y conviene que quede dicho en el índice:**

| Qué se hizo | Dónde |
| --- | --- |
| Se **incorporó** como artefacto vigente, sin duplicarla ni reescribirla | §1 de este índice |
| Se **resolvió el método de verificación** de sus **61** filas, por familia, con el `TC-XX` que la ejerce y la etapa en que entra | [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.1 |
| Se declaró la **frontera** entre sonda y caso de verificación: la sonda aporta el **umbral de deriva**, el caso aporta el **criterio de aceptación**, y ninguno redefine al otro | [`Estrategia-Testing.md`](Estrategia-Testing.md) §8 y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §7 |
| Se convirtió su verificación en **gate** y en criterio de cierre de etapa | `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3; [`Plan-Pruebas.md`](Plan-Pruebas.md) §3; [`Definition-Of-Done.md`](Definition-Of-Done.md) §1.3 |
| Se **verificó desde este lado** la tabla de correspondencia que [`../../GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) declara contra ésta: **las ocho correspondencias son verdaderas** | [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.2 |
| **No se modificó ninguna fila, ningún umbral ni el recuento de 61** | — |
| **No se abrieron filas para la capacidad `F-26`**, porque sus elementos de interfaz no tienen identificador en la línea de base. Se verifican con `TC-06`, `TC-07` y `TC-10` contra los criterios de aceptación de `CU-03` y `CU-04`, **sin umbral de deriva**, y así queda declarado como hueco | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |

## 4. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 la exige para `library` con plugins y para `web-microservices` con plugins, y la omite para los tipos sin puntos de extensión. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, y su guía vive en la categoría 08 de `GeometriaFactory-Visor`. Lo que sí hace este proyecto de código es **consumir** ese punto de extensión, y `TC-32` verifica que lo consuma **sólo por sus seis funciones** |

**Ninguna omisión más.** A diferencia de los proyectos de código de biblioteca del producto, acá **la matriz de sensado de deriva no se omite**: `requiere_maqueta` es **true** (`PRODUCT-MANIFEST` §5), la Fase B2 se ejecutó con maqueta propia y la matriz existe desde entonces.

## 5. Quality gates configurados

Los once de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3, resumidos acá para que se lean de un vistazo. **El texto vinculante es el de esa sección**, no éste. Las **tres** puertas técnicas van aparte, en §3.2 de ese documento, porque su consecuencia es distinta: detienen la planificación.

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Construcción sin advertencias | Bloqueante |
| QG-02 | Bundle generado en el mismo flujo, nunca de un artefacto viejo | Bloqueante |
| QG-03 | El flujo termina comprobando que la dirección pública responde | Bloqueante |
| QG-04 | 100 % de los pasos del guion de la etapa **y de todas las anteriores** **[ASUNCIÓN del intake en cuanto a la forma de la puerta]** | **Bloqueante** |
| QG-05 | **0** peticiones del navegador hacia el servicio de datos, con los dos movimientos prendidos | Bloqueante, sin gradación |
| QG-06 | **1** sola salida y **0** bibliotecas de guion que consulten | Bloqueante |
| QG-07 | **0** apariciones de la credencial de sesión en el navegador | Bloqueante |
| QG-08 | **0** mensajes que expongan dirección, ruta o traza, sobre 15 códigos y el camino de ausencia | Bloqueante |
| QG-09 | **0** invocaciones al interior del bundle; 6 de 6 funciones como única vía | Bloqueante |
| QG-10 | **0** tráfico de circuito durante la interacción; texto una sola vez por trabajo | Bloqueante |
| QG-11 | Filas de la matriz de sensado verificadas y **ninguna deriva mayor abierta** | Bloqueante al cierre de etapa |

**Ningún gate de este proyecto de código es condicionado.** `QG-04` lleva el valor rotulado **[ASUNCIÓN]** del intake §22, asunción `A-4`: **lo rotulado es expresar la regla acumulativa como puerta con umbral del 100 %, no la regla en sí**, y la columna «Si el Product Owner la cambia» declara que «cambia la forma del gate, no su carácter bloqueante». La regla acumulativa rige igual, y el gate **bloquea**.

## 6. Recuentos que esta sección sostiene

Se declaran acá para que cualquier lectura posterior pueda verificarlos contra su fuente sin recorrer los ocho documentos.

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **10** | `02` §3 |
| Superficies | **11** | `03` `Linea-Base-Visual.md`; `05` §3.4 |
| Restricciones transversales | **13** | `02` §6; `05` §10.2 |
| Reglas de negocio del producto | **16** | `05` §10.3; se enuncian en `GeometriaFactory-Domain`. **Ninguna se hace cumplir acá** |
| Códigos vivos del contrato | **15** | `GeometriaFactory-Contracts`; `05` §3.1, traductor de condiciones |
| Historias de usuario | **30** | `06` `Product-Backlog.md` §3 |
| Tareas técnicas | **23** | `06` `Backlog-Tecnico.md` |
| Componentes | **8**, en tres capas | `05` §3.1 |
| NFR | **14** | `05` §8 |
| Sondas de la matriz de sensado | **61** | [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) §4 |
| Elementos de la línea de base sensados | **211** — 11 superficies, 73 componentes, 74 estados, 24 rutas y 29 campos | Ídem, tabla de cobertura de §4 |
| Casos de verificación de esta categoría | **35** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **35** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **11**, más **3** puertas técnicas | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 y §3.2 |
| Escenarios del intake usados como dato | **8 de 8**, en su forma original y completa | `PRODUCT-INTAKE` §20 |
| Etapas que este proyecto de código toca | **8** — `a` a `h`, **todas las comprometidas** | `06` `Product-Backlog.md` §2 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** La tabla de gates de §5 declaraba a `QG-04` **condicionado**. Pasa a **bloqueante**: §17.6.P.6 lo escribe como «gate bloqueante y numérico» y §22 `A-4` declara que lo que puede cambiar es la forma del gate y no su carácter. Se actualizan las versiones de los artefactos revisados. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Web`. Lista los **ocho** artefactos vigentes, **siete** emitidos por esta Fase E y uno —[`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2— **emitido por la Fase B2 y ya existente**, con la columna que declara quién emitió cada uno. Su §3 declara qué hizo esta fase con esa matriz y qué no hizo, incluida la verificación desde este lado de la tabla de correspondencia de `GeometriaFactory-Visor`. Declara **un** artefacto omitido con su motivo, los **once** quality gates y las **tres** puertas técnicas con su carácter, y la tabla de recuentos con la fuente de cada uno. |
