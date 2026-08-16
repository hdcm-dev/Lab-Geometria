# 08 · Calidad y pruebas — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**

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
| [`Estrategia-Calidad.md`](Estrategia-Calidad.md) | 1.1 | Propuesto | Definición de calidad, atributos ISO 25010, **quince** quality gates, **dos** puertas técnicas y la frontera del despliegue |
| [`Estrategia-Testing.md`](Estrategia-Testing.md) | 1.1 | Propuesto | Pirámide **invertida** con su motivo, cobertura por componente, tooling, cero dobles en integración y los **ocho** escenarios como cuerpo de petición |
| [`Plan-Pruebas.md`](Plan-Pruebas.md) | 1.1 | Propuesto | Alcance, criterios de entrada y salida, **once** riesgos de calidad y plan por etapa |
| [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) | 1.1 | Propuesto | Trazabilidad CU ↔ tests, NFR ↔ tests, RN ↔ tests, **punto de acceso ↔ tests**, invariante ↔ tests y cobertura por componente |
| [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) | 1.0 | Propuesto | Catálogo de **treinta y siete** casos de verificación, `TC-00001` a `TC-00037` |
| [`Criterios-Validacion.md`](Criterios-Validacion.md) | 1.1 | Propuesto | **Cuarenta** criterios, `CV-01` a `CV-40`, con su carácter |
| [`Definition-Of-Done.md`](Definition-Of-Done.md) | 1.1 | Propuesto | **DoD canónica** del proyecto de código, en cuatro capas, con la entrega del artefacto |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | 1.0 | Propuesto | **Tres** sondas `VER-XX` tomadas de los contratos de verificación de [`../10-Examples/`](../10-Examples/), una de ellas la **colección de peticiones reproducible**, sin ninguna fila de línea de base visual |

## 2. Orden de lectura

1. [`Estrategia-Calidad.md`](Estrategia-Calidad.md) — qué se entiende por calidad acá, qué gates y qué puertas técnicas existen, y dónde termina esta categoría respecto del despliegue.
2. [`Estrategia-Testing.md`](Estrategia-Testing.md) — con qué se verifica, con qué datos y con qué umbrales.
3. [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) — los treinta y siete casos, uno por uno.
4. [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) — el documento bisagra, con la tabla de los **quince** puntos de acceso.
5. [`Plan-Pruebas.md`](Plan-Pruebas.md) — cuándo se ejecuta cada cosa, por etapa.
6. [`Criterios-Validacion.md`](Criterios-Validacion.md) — cuándo se declara validado.
7. [`Definition-Of-Done.md`](Definition-Of-Done.md) — cuándo se declara terminado.
8. [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) — qué se sensa durante la codificación, y con qué comando. **Se lee al final** porque su insumo es [`../10-Examples/`](../10-Examples/) y no esta categoría.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Testing-Extensibilidad.md` | **Omitido** | `Rules-Calidad-Y-Pruebas.md` §2.1 lo recomienda para `rest-api` **con handlers externos** y lo omite para los tipos sin puntos de extensión. Este proyecto de código **no admite handlers externos**: su `tiene_extensibilidad` es **false** (`PRODUCT-MANIFEST` §5), su único cliente legítimo es `GeometriaFactory-Web` y **no hay versionado de rutas porque no hay clientes de terceros**. El punto de extensión del producto es el contrato de la fachada del visor, y su guía vive en la categoría 08 de `GeometriaFactory-Visor` |
| [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Emitido el 2026-08-11**, en 1.0 | **La omisión que esta fila declaraba quedó cerrada, y el candidato natural que anticipaba resultó ser exactamente el que entró.** Se omitía porque `Rules-Calidad-Y-Pruebas.md` §2.1 la omite para «proyectos de código sin Fase B2 y sin categoría 10», y este proyecto de código cumplía las dos condiciones: `requiere_maqueta` es **false** (`PRODUCT-MANIFEST` §5) y su `10-Examples` no estaba emitida. **La segunda condición dejó de cumplirse**: [`../10-Examples/README.md`](../10-Examples/README.md) 1.0 declara **tres** contratos de verificación, `VER-00001` a `VER-00003`, y el segundo **es** la colección de peticiones reproducible de `CU-00012`. La primera condición sigue en pie, y por eso la matriz **no tiene ninguna fila de línea de base visual**. Es el caso que `Deriva-Rules.md` §2.3 prevé. La fila se conserva con su desenlace, en lugar de retirarse, para que el motivo de la omisión y el de su cierre queden legibles juntos |

## 4. Quality gates configurados

Los quince de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3, resumidos acá para que se lean de un vistazo. **El texto vinculante es el de esa sección**, no éste. Las **dos** puertas técnicas van aparte, en §3.3 de ese documento.

| Gate | Condición, en una línea | Carácter |
| --- | --- | --- |
| QG-01 | Construcción en 0 y sin advertencias | Bloqueante |
| QG-02 | Batería entera en verde, **incluida la del validador** | Bloqueante |
| QG-03 | Cobertura 75 % líneas y 70 % ramas **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-04 | Pirámide 60 % integración / 40 % unitarias **[ASUNCIÓN en cuanto al reparto]** | **Condicionado**; la **inversión** no es asunción |
| QG-05 | Exactamente **4** puntos fuera de la guardia sobre **15**, ni uno más | Bloqueante, sin gradación |
| QG-06 | **16 de 17** códigos con destino, **1** sin él declarado, **0** inventados y **0** renombrados | Bloqueante |
| QG-07 | **3 de 3** familias empobrecidas indistinguibles | Bloqueante, sin gradación |
| QG-08 | **0** respuestas que expongan dirección, ruta, secreto o traza | Bloqueante |
| QG-09 | **0** caracteres de diferencia y **0** truncamientos silenciosos | Bloqueante, sin gradación |
| QG-10 | **4 de 4** puertos conectados, y **1** sola configuración de intercambio | Bloqueante, con fallo en construcción |
| QG-11 | **0** peticiones atendidas con la preparación del almacén incompleta | Bloqueante |
| QG-12 | **0** eliminaciones fuera de alcance aceptadas **al forzar la petición** | Bloqueante |
| QG-13 | Arranque en frío en menos de 30 segundos **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-14 | Percentil 99 por debajo de 500 ms y 20 peticiones por minuto **[ASUNCIÓN del intake]** | **Condicionado** |
| QG-15 | Colección de peticiones en **5 pasos o menos** con **0** datos inventados | Bloqueante al cierre de la etapa que la incorpora |

**Los cuatro gates condicionados dependen de valores rotulados [ASUNCIÓN] en el intake §22** —asunción `A-3` para la cobertura y la forma de la pirámide, `A-5` para el percentil, el caudal y el arranque en frío—.

**Dos puertas técnicas y una frontera.** `PT-04` se mide en la etapa `a`; `PT-05` corresponde al despliegue real, fuera del tramo comprometido. Y el **despliegue es manual y del Product Owner**: ningún criterio de esta categoría se cumple ejecutándolo.

## 5. Recuentos que esta sección sostiene

Se declaran acá para que cualquier lectura posterior pueda verificarlos contra su fuente sin recorrer los ocho documentos.

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Casos de uso | **12** | `02` §5 |
| Puntos de acceso | **15** — **4** fuera de la guardia y **11** bajo ella | `02` `Definicion-Superficie-HTTP.md` §3; `05` §3.4 |
| Reglas de negocio del producto | **16**, con **13** con tramo acá y **3** sin él | `05` §10.2; se enuncian en `GeometriaFactory-Domain` |
| Reglas que esta capa **puede romper sola** | **2** — `RN-00003` y `RN-00013` | `05` §10.2 |
| Invariantes vigentes | **9** | `05` §10.3 |
| Códigos vivos del contrato | **17** — **16** con destino y **1** sin él | `03` §6.1; `05` `Contratos-REST.md` §5 |
| Entradas del catálogo de condiciones | **18** — los 16 códigos con destino más las 2 respuestas sin código | `03` §6.1 |
| Historias de usuario | **30** | `06` `Product-Backlog.md` §3 |
| Tareas técnicas | **26** | `06` `Backlog-Tecnico.md` |
| Componentes | **8** | `05` §3.1 |
| NFR | **17** | `05` §8 |
| Casos de verificación de esta categoría | **37** | [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 |
| Criterios de validación | **40** | [`Criterios-Validacion.md`](Criterios-Validacion.md) |
| Quality gates | **15**, más **2** puertas técnicas | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 y §3.3 |
| Casos de la batería del validador que corre desde acá | **10** | `PRODUCT-INTAKE` §21; Fase C de `GeometriaFactory-Infrastructure` |
| Escenarios del intake usados como cuerpo de petición | **8 de 8** | `PRODUCT-INTAKE` §20 |
| Etapas que este proyecto de código toca | **6** — `a`, `c`, `d`, `e`, `f`, `h` | `06` `Product-Backlog.md` §2 |

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de sondas `VER-XX`.** Se emitió [`../10-Examples/`](../10-Examples/) en su pasada de diseño, con **tres** contratos de verificación —uno de ellos la **colección de peticiones reproducible** de `CU-00012`, que es el candidato natural que esta sección había anticipado—, y con ellos se abrió [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.0, que pasa a ser el **octavo** artefacto de la sección. La fila de §3 se **conserva** con su desenlace y su fecha: la condición «sin categoría 10» dejó de cumplirse y la condición «sin Fase B2» sigue en pie, de modo que la matriz nace **sin ninguna fila de línea de base visual**, que es el caso de `Deriva-Rules.md` §2.3. **Ningún gate, umbral, caso de prueba ni recuento de esta sección cambia**, y `CU-00012` §9 sigue rigiendo: la colección **no reemplaza a las pruebas de integración y no se cuenta como cobertura**. |
| 1.1 | 2026-08-11 | Actualiza la tabla de artefactos: seis de los siete suben a **1.1**. Por `H-01`, los documentos afirmaban **en presente** que el intake escribe «nueve pruebas del validador» en §17.5.P.8, y el **intake 1.20** dice **diez**; el hueco de la matriz **se conserva y queda cerrado** con su desenlace. Por `H-06`, la estrategia de testing declara ahora que su piso de cobertura de líneas —**75 %**— **baja** el **80 %** que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `rest-api`, con qué autoridad y qué le falta. Por `H-04`, la matriz suma §2.1 con `TC-00036`. Por `H-08`, el mutation score deja de atribuirse a la fila `rest-api` de §2.2, que no lo pide. **Ningún gate, umbral, caso ni recuento de esta sección cambia**: en particular el 75/70 **no se sube**. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 08 de `GeometriaFactory-Api`, proyecto de código **principal** del producto. Lista los **siete** artefactos emitidos con su versión y su estado, el orden de lectura, los **dos** artefactos omitidos con su motivo —con la constancia de que la matriz de sensado tendría un candidato natural en la colección de peticiones cuando se emita la categoría 10—, los **quince** quality gates con su carácter, las **dos** puertas técnicas y **la frontera del despliegue**, y la tabla de recuentos que esta sección sostiene con la fuente de cada uno. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **3**. Sube minor. |
