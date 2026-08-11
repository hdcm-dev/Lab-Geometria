# 09 · DevOps — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto y unidad desplegable del servidor propio**

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Los quince quality gates, y en qué stage corre cada uno](#4-los-quince-quality-gates-y-en-qué-stage-corre-cada-uno)
- [5. La frontera del despliegue, y qué queda de cada lado](#5-la-frontera-del-despliegue-y-qué-queda-de-cada-lado)
- [6. Recuentos que esta sección sostiene](#6-recuentos-que-esta-sección-sostiene)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 1.1 | Propuesto | Los **cinco** stages con el quinto declarado fuera del alcance, los **quince** gates con su carácter, la matriz, la caché, la promoción en cuatro transiciones, la reversión y las **dos** puertas técnicas |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 1.0 | Propuesto | Versionado semántico, las **cinco** reglas con las que `ADR-08` reemplaza al versionado de rutas, y las **tres** clases de cambio que la compilación no detecta con dónde se atrapan |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 1.1 | Propuesto | Los **dos** ambientes con el apartamiento del modelo declarado, cómo llega el código al destino, **la dirección dinámica** en sus tres tramos, configuración y secretos |
| [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) | 1.1 | Propuesto | Pre-requisitos, procedimiento de despliegue en destino, **la prueba única del mecanismo** que la fuente exige, **cinco** verificaciones posteriores, reversión y métricas |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 1.0 | Propuesto | Inventario sobre la imagen, la firma con su brecha y su desplazamiento, nivel de integridad con la brecha propia del canal, análisis, y la **superficie expuesta** como preocupación de cadena de suministro |

## 2. Orden de lectura

1. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde corre, cómo llega el código y qué pasa con la dirección dinámica.
2. [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) — cómo se despliega a mano, cómo se comprueba y cómo se vuelve atrás.
3. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea y dónde termina la canalización.
4. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué reemplaza al versionado de rutas y qué no detecta la compilación.
5. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — dónde está el riesgo real de esta unidad.

**El orden pone primero los dos documentos de despliegue**, y a propósito: éste es el proyecto de código donde el acto que más importa **no lo ejecuta ninguna canalización**, y quien llegue a esta sección lo hace buscando cómo se pone el servicio en pie.

**El acuerdo de equipo que `Rules-Devops.md` §3.5 sugiere leer primero no existe como documento propio de este producto**: sus reglas —una rama y un pull request por etapa, etapas en serie, punto de control bloqueante— las declara el intake §15 y §10, y las tres se citan desde [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-Openapi.md` | **Omitido** | `Rules-Devops.md` §2.2 lo admite como artefacto secundario para el tipo `rest-api`. Acá **no tiene sujeto**: el intake §17.5.P.3 declara que **el versionado del contrato es el del ensamblado de contratos** y que **no hay versionado de rutas porque no hay clientes de terceros**. Una guía de publicación de contrato describiría una entrega que nadie recibe |
| `Guia-Publicacion-Chart-Helm.md` | **Omitido** | `Rules-Devops.md` §4.7 lo admite como artefacto secundario para este tipo. El producto **no tiene orquestador de contenedores**: el despliegue es una composición levantada a mano en un servidor domiciliario (intake §17.5.P.8) |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

**Ninguna omisión más, y la guía de publicación principal no se omite**: hay un artefacto que sale del repositorio y llega a un servidor.

## 4. Los quince quality gates, y en qué stage corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1. Esta categoría **no cambió el carácter de ninguno**. Las **dos** puertas técnicas van aparte: su consecuencia es detener la planificación.

| Gate | Stage donde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `test` | Bloqueante |
| QG-03 | `cobertura`, informe por componente | **Condicionado** |
| QG-04 | `cobertura`, recuento por clase (`TC-37`) | **Condicionado**; la **inversión** no es asunción |
| QG-05 | `test`, con `TC-07` en las dos direcciones | Bloqueante, sin gradación |
| QG-06 | `test`, con `TC-24` y `TC-27` | Bloqueante |
| QG-07 | `test`, con `TC-25` | Bloqueante, sin gradación |
| QG-08 | `test`, con `TC-26` | Bloqueante |
| QG-09 | `test`, con `TC-19` | Bloqueante, sin gradación |
| QG-10 | `build`, con `TC-28` y `TC-29` | Bloqueante, con fallo en construcción |
| QG-11 | `test`, con `TC-31` | Bloqueante |
| QG-12 | `test`, con `TC-20`, **forzando la petición** | Bloqueante |
| QG-13 | `imagen`, con `TC-33` | **Condicionado** |
| QG-14 | `test`, batería de integración, con `TC-34` | **Condicionado** |
| QG-15 | Cierre de la etapa que incorpora la colección, con `TC-35` | Bloqueante al cierre de esa etapa |

**Los cuatro condicionados se miden y se registran igual.** Dependen de valores rotulados **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura y la forma de la pirámide, `A-5` para el percentil, el caudal y el arranque en frío—. **Confirmados, los cuatro pasan a bloqueantes sin ningún otro cambio.** La tarea que los eleva es `BT-25`, al cerrar la etapa `d`.

**Los cuatro rótulos de este proyecto de código son sobre umbrales**, y por eso los cuatro condicionan. Es el caso contrario al de `GeometriaFactory-Web`, donde la única marca es sobre **la forma** del gate y el gate **bloquea**. La regla de reparto está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2, y **no la inventó esta categoría**: la fijó la Fase E.

**Y lo que no es asunción dentro de `QG-04`**: la **inversión** de la pirámide. El intake §17.5.P.6 la declara a propósito, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». Lo rotulado es el reparto numérico.

## 5. La frontera del despliegue, y qué queda de cada lado

Es la particularidad de esta sección y conviene tenerla de un vistazo. El intake §17.5.P.8 declara el despliegue **manual, por el docente**, y que el agente **entrega el archivo de construcción y el de composición y no ejecuta el despliegue**.

| Lado | Qué incluye | Dónde está escrito |
| --- | --- | --- |
| **De este lado de la frontera** —lo que la canalización hace— | Construir, probar, medir cobertura, **construir la imagen y arrancarla** para medir `PT-04`, y **entregar** el archivo de construcción y el de composición | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 y §6 |
| **Del otro lado** —lo que hace el Product Owner— | Levantar la composición en el servidor propio desde la etiqueta, comprobar salud, comprobar que el front lo alcanza, y medir `PT-05` en el despliegue real | [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 y §3 |

**Ningún criterio de la categoría 08 se cumple ejecutando un despliegue**, y esta categoría no lo cambia: lo que hace es **escribir el procedimiento para quien lo ejecuta**, que es lo que `Rules-Devops.md` §4.5 pide de una guía de publicación.

## 6. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **5**, con el quinto **fuera del alcance de la canalización** | Intake §17.5.P.8; `05` §5 |
| Quality gates materializados | **15**, con **4** condicionados | `08` `Estrategia-Calidad.md` §3 |
| Puertas técnicas que alcanzan a este proyecto de código | **2**: `PT-04` en la etapa `a` y `PT-05` en el despliegue real | `08` `Estrategia-Calidad.md` §3.3 |
| Ambientes | **2**: contenedor de desarrollo y servidor propio | Intake §17.5.P.8; `05` §5 |
| Unidades desplegables del producto | **2**, y ésta es una | Intake §13 y §14 |
| Artefactos publicables de este proyecto de código | **1**: `image-docker`, **construida en destino y no publicada en un registro** | Intake §17.5.P.7 |
| Puntos de acceso | **15** — **4** fuera de la guardia y **11** bajo ella | `02` `Definicion-Superficie-HTTP.md` §3; `05` §3.4, citados por `08` README §5 |
| Códigos vivos del contrato | **15** — **14** con destino y **1** sin él | `03` §6.1; `05` `Contratos-REST.md` §5, citados por `08` README §5 |
| Casos de uso | **12** | `02` §5, citado por `08` README §5 |
| Casos de la batería del validador que corre desde acá | **10** | `PRODUCT-INTAKE` §21; `08` README §5 |
| Escenarios del intake usados como cuerpo de petición | **8 de 8** | `PRODUCT-INTAKE` §20 |
| Puertos conectados en la composición de raíz | **4** | `QG-10`; `05` §8 |
| Criterios de salida del plan de pruebas | **12** | `08` `Plan-Pruebas.md` §3 |
| Puntos de la Definition of Done sobre la entrega del artefacto | **7** | `08` `Definition-Of-Done.md` §1.4 |
| Etapas que este proyecto de código toca | **6**: `a`, `c`, `d`, `e`, `f` y `h` | `06` `Product-Backlog.md` §2, citado por `08` README §5 |
| Puntos abiertos de esta categoría | **5** declarados, `PD-01` a `PD-05`, de los cuales **4 vigentes**: `PD-05` quedó cerrado por el intake 1.22 y conserva su fila | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Api`, **proyecto de código principal y unidad desplegable del servidor propio**. Lista los **cinco** artefactos emitidos —incluida la guía de publicación, que acá **no se omite**—, el orden de lectura que pone primero los dos documentos de despliegue con su motivo, los **tres** artefactos que no corresponden con su motivo, los **quince** quality gates con el stage donde corre cada uno y la constancia de que ninguno cambió de carácter, con la precisión de que **los cuatro rótulos de este proyecto de código son sobre umbrales** y por eso condicionan, al revés que en `GeometriaFactory-Web`. Declara **la frontera del despliegue** con qué queda de cada lado, y la tabla de recuentos con la fuente de cada uno. |
| 1.1 | 2026-08-11 | **Propagación del intake 1.22 y constancia de las correcciones de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`.** El intake **1.22** §17.6.P.7 decide que, cuando front y backend salen juntos, sale **primero el backend**, con lo que `PD-05` queda **cerrado** y la fila de recuentos pasa a declarar **5** puntos abiertos con **4 vigentes**. Se actualizan a 1.1 en §1 las versiones de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) —que además corrige `H-01`, `H-03` y `H-05`—, [`Entornos-Deploy.md`](Entornos-Deploy.md) y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md). |
