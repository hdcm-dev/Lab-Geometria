# 08 · Calidad y pruebas — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.1 | Propuesto | Definición de calidad, atributos ISO 25010, **catorce** quality gates, y la declaración de por qué la batería tiene **diez** casos |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Pirámide objetivo, cobertura por componente con el piso propio del validador, tooling, dobles mínimos y los **ocho** escenarios reales del intake como texto literal |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.1 | Propuesto | Alcance, criterios de entrada y salida, **diez** riesgos de calidad y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, regla conceptual ↔ tests, batería ↔ escenarios y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **treinta y cinco** casos, `TC-06001` a `TC-06035`, cuyos **diez primeros son los diez de la batería** |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.1 | Propuesto | **Treinta y cinco** criterios, `CV-01` a `CV-35`, con su carácter bloqueante, condicionado o no exigible |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.1 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | 1.0 | Propuesto | **Tres** sondas `VER-XX` tomadas de los contratos de verificación de [`../10-Examples/`](../10-Examples/), sin ninguna fila de línea de base visual |

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá, qué gates existen y por qué la batería tiene diez casos.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — con qué se prueba, con qué datos y con qué umbrales.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los treinta y cinco casos, uno por uno.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra: qué caso de prueba cubre qué.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — cuándo se ejecuta cada cosa, por etapa.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) — cuándo se declara validado.
7. [`Definition-Of-Done.md`](Definition-Of-Done.md) — cuándo se declara terminado.
8. [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) — qué se sensa durante la codificación, y con qué comando. **Se lee al final** porque su insumo es [`../10-Examples/`](../10-Examples/) y no esta categoría.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo exige para `library` **con plugins** y lo omite para los tipos sin puntos de extensión. El flag `tiene_extensibilidad` de este proyecto de código es **false** (`PRODUCT-MANIFEST` §5): el punto de extensión del producto es el contrato de la fachada del visor, y su guía vive en la categoría 08 de `GeometriaFactory-Visor` |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Emitido el 2026-08-11**, en 1.0 | **La omisión que esta fila declaraba quedó cerrada.** Se omitía porque `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10», y este proyecto de código cumplía las dos condiciones: `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5) y su `10-Examples` no estaba emitida. **La segunda condición dejó de cumplirse**: [`../10-Examples/README.md`](../10-Examples/README.md) 1.0 declara **tres** contratos de verificación, `VER-06001` a `VER-06003`, y de ellos se toman las **tres** sondas de la matriz. La primera condición sigue en pie, y por eso la matriz **no tiene ninguna fila de línea de base visual**. Es el caso que `Deriva-Rules.md` §2.3 prevé —«cuando el proyecto de código no ejecuta Fase B2 pero sí tiene categoría 10, la matriz se emite igual: […] poblada solo con sondas `VER-XX`», con la elisión marcada; el fragmento suprimido asigna la apertura a AG-08 en la Fase E, y la matriz declara en su §1 por qué acá la abre AG-10—. La fila se conserva con su desenlace, en lugar de retirarse, para que el motivo de la omisión y el de su cierre queden legibles juntos |

## 4. Quality gates configurados

Los catorce de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3, resumidos acá para que se lean de un vistazo. **El texto vinculante es el de esa sección**, no éste.

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Construcción en 0 y sin advertencias | Bloqueante |
| QG-02 | Batería entera en verde, sin pruebas deshabilitadas sin motivo | Bloqueante |
| QG-03 | **Batería del validador: 10 de 10**, con los ocho escenarios como entrada | Bloqueante |
| QG-04 | Transformaciones aplicadas **solas** sobre almacén inexistente | Bloqueante; criterio de aceptación de la etapa `c` |
| QG-05 | Cobertura 85 % líneas y 80 % ramas **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-06 | Cobertura del validador 95 % de líneas **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-07 | Tolerancia **0.01** estricta: `E-1` da exactamente **2** advertencias | Bloqueante. **No es asunción** |
| QG-08 | **0** peticiones de red de los dos motores | Bloqueante |
| QG-09 | **0** provisorias repetidas y **0** derivables de un dato conocido | Bloqueante |
| QG-10 | **0** componentes y **0** texto original en una proyección de listado | Bloqueante |
| QG-11 | **0** escrituras que reemplacen el texto original y **0** retiros parciales | Bloqueante |
| QG-12 | **0** emisiones de acceso sin clave de firma | Bloqueante |
| QG-13 | 17 de 17 condiciones alcanzadas, 0 fuera, y **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno | Bloqueante |
| QG-14 | Interpretación de `E-1` en menos de 200 ms, sin almacén **[ASUNCIÓN del intake]** | **Condicionado** |

**Los tres gates condicionados dependen de valores rotulados [ASUNCIÓN] en el intake §22** —asunción `A-3` para las dos coberturas y `A-5` para los 200 ms—: se miden y se registran, y no bloquean la fusión hasta que el Product Owner los confirme.

**`QG-07` no es condicionado y conviene no confundirlo**: el intake §22 enumera la tolerancia de 0.01 entre «lo que NO es asunción», con su fundamento.

**Una puerta técnica del producto se mide en la etapa `a` de este proyecto de código**: [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 asigna `PT-04` a su épica `EP-06001`. Su consecuencia es la del intake §15: **una puerta que no pasa detiene la planificación de las etapas que dependen de ella**.

## 5. Recuentos que esta sección sostiene

Se declaran acá para que cualquier lectura posterior pueda verificarlos contra su fuente sin recorrer los ocho documentos.

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **10** | `02` §5 |
| Puertos implementados | **4**, más **2** mecanismos y **1** responsabilidad de arranque | `02` §3 |
| Reglas de negocio del producto | **16**, con **14** con tramo acá y **2** sin él | `02` §6; se enuncian en `GeometriaFactory-Domain` |
| Reglas con tramo **principal** acá | **3** — `RN-06008`, `RN-06009` y `RN-06014` | `02` §6 |
| Reglas conceptuales de modelo | **7** | `02` `Modelo-Datos/reglas-conceptuales-de-modelo/` |
| Condiciones distintas catalogadas | **17** | `03` §7.1 |
| Historias de usuario | **25** | `06` `Product-Backlog.md` §3 |
| Tareas técnicas | **26** | `06` `Backlog-Tecnico.md` |
| Componentes | **8** | `05` §3.1 |
| NFR | **14** | `05` §8 |
| Casos de la batería del validador | **10** | `05` §10.5; intake §21 |
| Escenarios del intake usados como texto literal | **8 de 8** | `PRODUCT-INTAKE` §20 |
| Casos de prueba de esta categoría | **35** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **35** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **14** | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Etapas que este proyecto de código toca | **5** — `a`, `c`, `d`, `e`, `f` | `06` `Product-Backlog.md` §2 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.3 | 2026-08-11 | **Corrección del hallazgo P2-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** La fila de `Matriz-Sensado-Deriva.md` de §3 citaba `Deriva-Rules.md` §2.3 **elidiendo sin marca** dos fragmentos, uno de ellos «la abre AG-08 en la Fase E», que asigna la titularidad. La elisión queda ahora **marcada** y el fragmento suprimido se dice en texto llano, con la remisión a la §1 de la matriz, que declara por qué acá la abre AG-10. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia.** Contrastado contra el texto vivo del `PRODUCT-INTAKE` **1.25**, en particular §16.1 y §18, y no contra lo que otro documento dice de ellas. Sube minor: corrige la forma de una cita. |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`.** Se emitió [`../10-Examples/`](../10-Examples/) en su pasada de diseño, con **tres** contratos de verificación, y con ellos se abrió [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que pasa a ser el **octavo** artefacto de la sección. La fila de §3 se **conserva** con su desenlace y su fecha, en lugar de retirarse: la condición «sin categoría 10» dejó de cumplirse y la condición «sin Fase B2» sigue en pie, de modo que la matriz nace **sin ninguna fila de línea de base visual**, que es el caso de `Deriva-Rules.md` §2.3. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia**, y los **diez** casos de la batería obligatoria siguen siendo los de `tests/`: las sondas no los reemplazan. |
| 1.1 | 2026-08-11 | Actualiza la tabla de artefactos: **seis** de los siete suben a **1.1**. Todos por `H-01`: los documentos afirmaban **en presente** que el intake escribe «nueve pruebas del validador» en §17.3.P.8 y §17.5.P.8, y el **intake 1.20** dice **diez** en esos dos gates, en §17.3.P.6, en §17.2.P.11 y en el encabezado de §21 —lo corrigió en el mismo commit que emitió esta categoría, sobre el hallazgo que esta categoría levantó—. El hueco de la matriz **se conserva y queda cerrado** con su desenlace, en lugar de abierto con remediación pendiente del Product Owner. **La batería era y sigue siendo de diez**, y ningún gate, umbral, caso ni recuento de esta sección cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Infrastructure`. Lista los **siete** artefactos emitidos con su versión y su estado, el orden de lectura, los **dos** artefactos omitidos con su motivo, los **catorce** quality gates con su carácter —con la constancia de que `QG-07` **no es condicionado** porque la tolerancia de 0.01 no es asunción— y la puerta técnica que el backlog asigna a la etapa `a` de este proyecto de código, y la tabla de recuentos que esta sección sostiene con la fuente de cada uno. |
