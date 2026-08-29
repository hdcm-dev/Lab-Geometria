# 09 · DevOps — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 2.3
**Estado:** Aprobado
**Fecha:** 2026-08-16
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


## 0. Esta categoría es de la unidad de entrega

**Los cuatro documentos de esta categoría se consolidaron el 2026-08-16**, y es la categoría **más
asimétrica** de las consolidadas: [`Entornos-Deploy.md`](Entornos-Deploy.md) tiene sólo **dos de doce
secciones comunes** a las cuatro capas.

El motivo es real y vale la pena tenerlo presente: **tres de las cuatro capas no se despliegan**, de
modo que su «entorno» es el contenedor de desarrollo y poco más. Lo que la consolidación junta por
primera vez son dos cosas que eran la misma preocupación vista desde capas distintas: **la dirección
dinámica del servidor**, que el host declara como la restricción que ordena todo, y **la clave de
firma que se recibe y no se busca**, que declaraba la infraestructura.

Y en [`Estrategia-Versionado.md`](Estrategia-Versionado.md) quedan visibles **los dos linajes que el
producto versiona además del suyo** —las transformaciones de esquema y los parámetros de derivación
de clave—, que ninguna otra capa mencionaba y que **no siguen la versión del producto**.

**La carpeta `_fusion/` de esta categoría se retira**: la fusión terminó acá. Los documentos absorbidos
están en [`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/09-Devops/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Api/09-Devops/).

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 3.3 | Propuesto | Los **cinco** stages con el quinto declarado fuera del alcance, los **quince** gates con su carácter, la matriz, la caché, la promoción en cuatro transiciones, la reversión y las **dos** puertas técnicas |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 4.0 | Propuesto | Versionado semántico, las **cinco** reglas con las que `ADR-00008` reemplaza al versionado de rutas, y las **tres** clases de cambio que la compilación no detecta con dónde se atrapan |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 3.0 | Propuesto | Los **dos** ambientes con el apartamiento del modelo declarado, cómo llega el código al destino, **la dirección dinámica** en sus tres tramos, configuración y secretos |
| [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) | 1.1 | Propuesto | Pre-requisitos, procedimiento de despliegue en destino, **la prueba única del mecanismo** que la fuente exige, **cinco** verificaciones posteriores, reversión y métricas |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 3.0 | Propuesto | Inventario sobre la imagen, la firma con su brecha y su desplazamiento, nivel de integridad con la brecha propia del canal, análisis, y la **superficie expuesta** como preocupación de cadena de suministro |

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
| `Guia-Publicacion-Openapi.md` | **Omitido, y el motivo cambió en la etapa `g`** | Se omitía porque **no tenía sujeto**: el intake §17.1.P.3 · GeometriaFactory-Api declara que el versionado del contrato es el del ensamblado y que **no hay versionado de rutas porque no hay clientes de terceros**, con lo cual una guía de publicación describiría una entrega que nadie recibe. **Eso último sigue siendo cierto y por eso el artefacto sigue omitido.** Lo que cambió es que el servicio **sí describe su superficie**: [`ADR-08008`](../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md) agrega un documento OpenAPI **generado** en `/openapi/v1.json` y un explorador en `/documentacion`. No hay contrato que publicar ni versionar: hay una descripción que se genera de los puntos que ya existen |
| `Guia-Publicacion-Chart-Helm.md` | **Omitido** | `Rules-Devops.md` §4.7 lo admite como artefacto secundario para este tipo. El producto **no tiene orquestador de contenedores**: el despliegue es una composición levantada a mano en un servidor domiciliario (intake §17.1.P.8 · GeometriaFactory-Api) |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

**Ninguna omisión más, y la guía de publicación principal no se omite**: hay un artefacto que sale del repositorio y llega a un servidor.

## 4. Los quince quality gates, y en qué stage corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1. Esta categoría **no cambió el carácter de ninguno**. Las **dos** puertas técnicas van aparte: su consecuencia es detener la planificación.

| Gate | Stage donde corre | Carácter |
| --- | --- | --- |
| QG-01 | `build` | Bloqueante |
| QG-02 | `test` | Bloqueante |
| QG-03 | `cobertura`, informe por componente | **Condicionado** |
| QG-04 | `cobertura`, recuento por clase (`TC-00037`) | **Condicionado**; la **inversión** no es asunción |
| QG-05 | `test`, con `TC-00007` en las dos direcciones | Bloqueante, sin gradación |
| QG-06 | `test`, con `TC-00024` y `TC-00027` | Bloqueante |
| QG-07 | `test`, con `TC-00025` | Bloqueante, sin gradación |
| QG-08 | `test`, con `TC-00026` | Bloqueante |
| QG-09 | `test`, con `TC-00019` | Bloqueante, sin gradación |
| QG-10 | `build`, con `TC-00028` y `TC-00029` | Bloqueante, con fallo en construcción |
| QG-11 | `test`, con `TC-00031` | Bloqueante |
| QG-12 | `test`, con `TC-00020`, **forzando la petición** | Bloqueante |
| QG-13 | `imagen`, con `TC-00033` | **Condicionado** |
| QG-14 | `test`, batería de integración, con `TC-00034` | **Condicionado** |
| QG-15 | Cierre de la etapa que incorpora la colección, con `TC-00035` | Bloqueante al cierre de esa etapa |

**Los cuatro condicionados se miden y se registran igual.** Dependen de valores rotulados **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura y la forma de la pirámide, `A-5` para el percentil, el caudal y el arranque en frío—. **Confirmados, los cuatro pasan a bloqueantes sin ningún otro cambio.** La tarea que los eleva es `BT-00025`, al cerrar la etapa `d`.

**Los cuatro rótulos de este proyecto de código son sobre umbrales**, y por eso los cuatro condicionan. Es el caso contrario al de `GeometriaFactory-Web`, donde la única marca es sobre **la forma** del gate y el gate **bloquea**. La regla de reparto está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2, y **no la inventó esta categoría**: la fijó la Fase E.

**Y lo que no es asunción dentro de `QG-00004`**: la **inversión** de la pirámide. El intake §17.1.P.6 · GeometriaFactory-Api la declara a propósito, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». Lo rotulado es el reparto numérico.

## 5. La frontera del despliegue, y qué queda de cada lado

Es la particularidad de esta sección y conviene tenerla de un vistazo. El intake §17.1.P.8 · GeometriaFactory-Api declara el despliegue **manual, por el docente**, y que el agente **entrega el archivo de construcción y el de composición y no ejecuta el despliegue**.

| Lado | Qué incluye | Dónde está escrito |
| --- | --- | --- |
| **De este lado de la frontera** —lo que la canalización hace— | Construir, probar, medir cobertura, **construir la imagen y arrancarla** para medir `PT-04`, y **entregar** el archivo de construcción y el de composición | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 y §6 |
| **Del otro lado** —lo que hace el Product Owner— | Levantar la composición en el servidor propio desde la etiqueta, comprobar salud, comprobar que el front lo alcanza, y medir `PT-05` en el despliegue real | [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md) §2 y §3 |

**Ningún criterio de la categoría 08 se cumple ejecutando un despliegue**, y esta categoría no lo cambia: lo que hace es **escribir el procedimiento para quien lo ejecuta**, que es lo que `Rules-Devops.md` §4.5 pide de una guía de publicación.

## 6. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Stages del pipeline | **5**, con el quinto **fuera del alcance de la canalización** | Intake §17.1.P.8 · GeometriaFactory-Api; `05` §5 |
| Quality gates materializados | **15**, con **4** condicionados | `08` `Estrategia-Calidad.md` §3 |
| Puertas técnicas que alcanzan a este proyecto de código | **2**: `PT-04` en la etapa `a` y `PT-05` en el despliegue real | `08` `Estrategia-Calidad.md` §3.3 |
| Ambientes | **2**: contenedor de desarrollo y servidor propio | Intake §17.1.P.8 · GeometriaFactory-Api; `05` §5 |
| Unidades desplegables del producto | **2**, y ésta es una | Intake §13 y §14 |
| Artefactos publicables de este proyecto de código | **1**: `image-docker`, **construida en destino y no publicada en un registro** | Intake §17.1.P.7 · GeometriaFactory-Api |
| Puntos de acceso | **15** — **4** fuera de la guardia y **11** bajo ella | `02` `Definicion-Superficie-HTTP.md` §3; `05` §3.4, citados por `08` README §5 |
| Códigos vivos del contrato | **17** — **16** con destino y **1** sin él | `03` §6.1; `05` `Contratos-REST.md` §5, citados por `08` README §5 |
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
| 1.1 | 2026-08-11 | **Propagación del intake 1.22 y constancia de las correcciones de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`.** El intake **1.22** §17.2.P.7 · GeometriaFactory-Web decide que, cuando front y backend salen juntos, sale **primero el backend**, con lo que `PD-05` queda **cerrado** y la fila de recuentos pasa a declarar **5** puntos abiertos con **4 vigentes**. Se actualizan a 1.1 en §1 las versiones de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) —que además corrige `H-01`, `H-03` y `H-05`—, [`Entornos-Deploy.md`](Entornos-Deploy.md) y [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md). |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa de indexar la categoría de un proyecto de código a indexar la de la **unidad de entrega**, con sus documentos consolidados en 2.0. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
| 2.2 | 2026-08-24 | **Ronda 3 del corte 09 de la migración 10.0 → 13.3**, sobre el re-audit independiente, que pasó de RECHAZADO a **APROBADO CON HALLAZGOS**: el P0 y los cinco P1 quedaron cerrados y aparecieron cuatro P2 y tres P3. **La sincronización de la ronda 2 estaba a medias y el mensaje de commit la declaró completa** (**P3**): la fila de `Pipeline-CI-CD.md` publicaba **1.1** con el documento en **3.3**. Queda al día. **Se corrige la afirmación además de la fila**: un índice que dice estar sincronizado y no lo está es peor que uno visiblemente viejo. |
| 2.1 | 2026-08-24 | **Sincronización del índice con la ronda 2 del corte 09 de la migración 10.0 → 13.3.** Las tres filas de la tabla de documentos publicaban versiones **1.0 y 1.1** mientras los documentos iban por **2.x**: el desfasaje era **anterior** al corte, y el audit independiente lo levantó como **P3** porque la ronda 1 lo amplió en seis filas sin tocarlo ni declararlo. Quedan en las versiones que los documentos tienen hoy. **No se toca ninguna otra fila**: un índice que se corrige de más deja de ser comparable con el estado que describía. |
| 2.3 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **1 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
