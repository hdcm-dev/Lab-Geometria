# 09 · DevOps — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 2.3
**Estado:** Aprobado
**Fecha:** 2026-08-16
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith` · **Una de las dos unidades desplegables del producto**

---

## Tabla de contenido

- [1. Artefactos de esta sección](#1-artefactos-de-esta-sección)
- [2. Orden de lectura](#2-orden-de-lectura)
- [3. Artefactos omitidos y su motivo](#3-artefactos-omitidos-y-su-motivo)
- [4. Los once quality gates, y dónde corre cada uno](#4-los-once-quality-gates-y-dónde-corre-cada-uno)
- [5. Las tres decisiones derivadas de esta sección](#5-las-tres-decisiones-derivadas-de-esta-sección)
- [6. Recuentos que esta sección sostiene](#6-recuentos-que-esta-sección-sostiene)
- [7. Control de cambios](#7-control-de-cambios)

---


## 0. Esta categoría es de la unidad de entrega

**Los documentos de esta categoría se consolidaron el 2026-08-16**, absorbiendo los de `GeometriaFactory-Visor`. Cada uno lleva una subsección por proyecto de código, con su texto transpuesto sin reescritura.

**Las dos canalizaciones son distintas y las dos están acá**: el portal se publica por FTP al hosting y el visor produce un bundle que se copia a `wwwroot/js/`. `Entornos-Deploy.md` tiene sólo tres secciones comunes, y es donde eso se ve.

**La carpeta `_fusion/` se retira**: la fusión terminó acá. Lo absorbido está en
[`../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/09-Devops/`](../../../_legacy/2026-08-16-consolidacion-m10/GeometriaFactory-Web/09-Devops/).

## 1. Artefactos de esta sección

| Documento | Versión | Estado | Propósito |
| --- | --- | --- | --- |
| [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) | 3.3 | Propuesto | Los **ocho** pasos del flujo de publicación, los **once** gates con su carácter, el `PD-01` que `GeometriaFactory-Contracts` elevó —hoy **cerrado** por el intake 1.22—, las **tres** plataformas y las **tres** puertas técnicas |
| [`Estrategia-Versionado.md`](Estrategia-Versionado.md) | 3.0 | Propuesto | Versionado semántico, las **seis** clases de cambio decididas sobre lo que la persona ve, modelo de ramas, canales y qué versiona realmente la etiqueta |
| [`Entornos-Deploy.md`](Entornos-Deploy.md) | 3.0 | Propuesto | Los **dos** ambientes con el apartamiento del modelo de cuatro declarado, el tramo local de la decisión sobre el bundle, configuración, secretos y qué pasa cuando la dirección del servidor propio cambia |
| [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md) | 1.2 | Propuesto | Pre-requisitos, invocación del flujo, **cuatro** verificaciones posteriores, reversión y las **seis** métricas de `ADR-10007` §8 |
| [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) | 3.0 | Propuesto | Inventario sobre las dos cadenas, la firma con su brecha, nivel de integridad, análisis de dependencias, análisis dinámico y las **tres** reglas de arquitectura como preocupación de cadena de suministro |
| [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) | 1.0 | Propuesto | Cómo se produce el bundle del punto de extensión y cómo llega a los recursos estáticos del anfitrión. **Faltaba en esta tabla desde la emisión del índice**, y el audit del corte 09 lo levantó |

## 2. Orden de lectura

1. [`Entornos-Deploy.md`](Entornos-Deploy.md) — dónde se despliega, con qué configuración y con qué secretos, y por qué hay dos ambientes y no cuatro.
2. [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) — qué corre, qué bloquea, cómo se resolvió el despliegue conjunto con `GeometriaFactory-Api` y en qué orden salen las dos unidades.
3. [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md) — cómo se publica, cómo se comprueba y cómo se vuelve atrás.
4. [`Estrategia-Versionado.md`](Estrategia-Versionado.md) — qué clase de cambio es cada cosa cuando nadie compila contra vos.
5. [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) — dónde está el riesgo real de este proyecto de código.

**El orden es distinto al de los proyectos de código de biblioteca**, y a propósito: en aquéllos el documento bisagra es el de versionado, porque su artefacto se referencia; acá el documento bisagra es el de entornos, porque **su artefacto se despliega**.

**El acuerdo de equipo que `Rules-Devops.md` §3.5 sugiere leer primero no existe como documento propio de este producto**: sus reglas —una rama y un pull request por etapa, etapas en serie, punto de control bloqueante— las declara el intake §15 y §10, y las tres se citan desde [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4.

## 3. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Guia-Publicacion-Openapi.md` | **Omitido** | `Rules-Devops.md` §2.2 lo admite como artefacto secundario para servicios, no para `web-monolith`, y acá no tendría sujeto por partida doble: esta unidad **no expone contrato a nadie** (intake §14) y el contrato del producto es un **ensamblado compartido**, no una descripción publicada. El intake §17.1.P.3 · GeometriaFactory-Api declara que **no hay versionado de rutas porque no hay clientes de terceros** |
| `Guia-Publicacion-Image-Docker.md` | **No es de esta sección** | La única imagen de contenedor del producto es la del backend. Su guía vive en la categoría 09 de `GeometriaFactory-Api` |
| `Pipeline-Producto.md` | **No es de esta sección** | Artefacto de nivel producto (`Rules-Devops.md` §2.1 y §4.9), emitido una sola vez bajo `Producto/` al cierre del bucle de proyectos de código |

**Ninguna omisión más, y a diferencia de los cinco proyectos de código que no se despliegan, acá la guía de publicación no se omite**: hay un artefacto que sale del repositorio y llega a un servidor de terceros.

## 4. Los once quality gates, y dónde corre cada uno

Resumen de lectura rápida. **El texto vinculante sobre el carácter de cada gate es el de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3**; el de dónde corre, el de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2. Esta categoría **no cambió el carácter de ninguno**. Las **tres** puertas técnicas van aparte: su consecuencia es detener la planificación.

| Gate | Dónde corre | Carácter |
| --- | --- | --- |
| QG-01 | Paso 5 del flujo de publicación | Bloquea la fusión |
| QG-02 | Paso 4, e inspección de la definición del flujo | Bloquea la publicación |
| QG-03 | Paso 8 del flujo | Bloquea el flujo |
| QG-04 | Guion de demostración acumulativo, antes del punto de control (`TC-10035`) | **Bloqueante**, no condicionado |
| QG-05 | Pull request, conteo en la pestaña de red con los movimientos prendidos (`TC-10029`) | Bloqueante, sin gradación |
| QG-06 | Pull request, inspección del árbol de fuentes y de las dependencias de guion (`TC-10030`) | Bloqueante |
| QG-07 | Pull request, inspección del almacenamiento y del contenido servido (`TC-10003`) | Bloqueante |
| QG-08 | Pull request, inspección del traductor de condiciones (`TC-10031`) | Bloqueante |
| QG-09 | Pull request, inspección del árbol de fuentes (`TC-10032`) | Bloqueante |
| QG-10 | Pull request, conteo del tráfico de circuito (`TC-10033`) | Bloqueante |
| QG-11 | Cierre de la etapa, recorrido de la matriz de sensado | Bloquea el cierre de etapa |

**Ningún gate de este proyecto de código es condicionado.** El único con valor rotulado **[ASUNCIÓN]** es `QG-10004`, y lo rotulado es **expresar la regla acumulativa como puerta**, no la regla: el intake §17.2.P.6 · GeometriaFactory-Web lo escribe como «gate bloqueante y numérico» y §22 `A-4` declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante». Es la distinción que la Fase E fijó y esta categoría la materializa sin reabrirla.

**Tres gates corren dentro del flujo de publicación y ocho no**, y la distinción importa: **una publicación verde significa que la aplicación quedó en pie, no que hace lo que debe**.

## 5. Las tres decisiones derivadas de esta sección

Se listan acá porque son lo que esta categoría agregó al corpus, y para que se puedan auditar sin recorrer los cinco documentos. **Las tres van declaradas como derivadas y ninguna se atribuye a la fuente.**

| # | Decisión | Dónde está su fundamento | Estado |
| --- | --- | --- | --- |
| 1 | **El filtro de rutas del flujo de publicación incluye `src/GeometriaFactory.Contracts/`**, que quedaba fuera aunque es entrada de compilación de esta unidad | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2, decisión 1 | Se elevó al Product Owner como `PD-01`, y **él la confirmó en el intake 1.22**: §17.2.P.7 · GeometriaFactory-Web enumera hoy las **tres** rutas. `PD-01` **cerrado** |
| 2 | **El despliegue conjunto lo sostiene `QG-08008` de `GeometriaFactory-Contracts` y no el filtro de rutas**, porque el filtro dispara una construcción y no coordina dos despliegues, y uno de los dos es manual por decisión del Product Owner | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.1 y §3.2 | Adoptada |
| 3 | **La exclusión del bundle del control de versiones queda asignada a `BT-10001`** de la etapa `a`, con el estado del repositorio verificado y fechado | [`Entornos-Deploy.md`](Entornos-Deploy.md) §2 | Asignada, registrada como `PD-02` |

**Y un hallazgo que esta sección deja escrito, y que el intake 1.22 confirmó en lugar de derogar**: con el front publicándose automáticamente al fusionar y el backend desplegándose a mano, **el despliegue conjunto es siempre un acto humano coordinado**, y ninguna decisión sobre el filtro de rutas lo vuelve automático. Tampoco lo vuelve automático el orden de salida que el intake §17.2.P.7 · GeometriaFactory-Web fija desde 1.22 —**primero el backend**—: el orden reduce el daño del intervalo, **no lo elimina**, y el mecanismo que queda sigue siendo la constancia escrita antes de cerrar la etapa.

## 6. Recuentos que esta sección sostiene

| Magnitud | Valor | Fuente |
| --- | --- | --- |
| Pasos del flujo de publicación | **8** | Intake §17.2.P.8 · GeometriaFactory-Web; `05` §5 enumera el mismo conjunto en **7** |
| Quality gates materializados | **11**, **ninguno** condicionado | `08` `Estrategia-Calidad.md` §3 y §3.1 |
| Puertas técnicas que alcanzan a este proyecto de código | **3**: `PT-01` en sus **cuatro** partes, `PT-02` y `PT-03` | `08` `Estrategia-Calidad.md` §3.2 |
| Ambientes | **2**: contenedor de desarrollo y hosting público | `05` §5; [`Entornos-Deploy.md`](Entornos-Deploy.md) §1 |
| Unidades desplegables del producto | **2**, y ésta es una | Intake §13 y §14 |
| Artefactos publicables de este proyecto de código | **1**: `Front-Ftp` | Intake §13; [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md) §0 |
| Valores de configuración | **2**, los dos secretos, nombrados por su función | Intake §17.2.P.5 · GeometriaFactory-Web; [`Entornos-Deploy.md`](Entornos-Deploy.md) §4 |
| Casos de uso | **10** | `02` §3, citado por `08` README §6 |
| Códigos vivos del contrato sobre los que se mide `QG-08008` | **15** | `GeometriaFactory-Contracts`; `05` §3.1, traductor de condiciones |
| Funciones de la fachada del visor, única vía de acceso al bundle | **6** | Intake §17.2.P.3 · GeometriaFactory-Visor, citado por §14 y por §17.2.P.3 · GeometriaFactory-Web |
| Sondas de la matriz de sensado que `QG-11` recorre | **61** | `08` `Matriz-Sensado-Deriva.md` §4 |
| Criterios de salida del plan de pruebas | **11** | `08` `Plan-Pruebas.md` §3 |
| Etapas que este proyecto de código toca | **8**: `a` a `h`, **todas las comprometidas** | `06` `Product-Backlog.md` §2, citado por `08` README §6 |
| Puntos abiertos de esta categoría | **5** declarados, `PD-01` a `PD-05`, de los cuales **4 vigentes**: `PD-01` quedó cerrado por el intake 1.22 y conserva su fila | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial del índice de la categoría 09 de `GeometriaFactory-Web`, **una de las dos unidades desplegables del producto**. Lista los **cinco** artefactos emitidos —incluida la guía de publicación, que acá **no se omite**—, el orden de lectura con la precisión de que el documento bisagra es el de entornos y no el de versionado, los **tres** artefactos que no corresponden con su motivo, los **once** quality gates con dónde corre cada uno y la constancia de que **ninguno es condicionado**, las **tres decisiones derivadas** de esta sección con su estado, el hallazgo de que **el despliegue conjunto es siempre un acto humano coordinado**, y la tabla de recuentos con la fuente de cada uno. |
| 1.1 | 2026-08-11 | **Propagación de las dos decisiones de despliegue del Product Owner** del intake **1.22** §17.2.P.7 · GeometriaFactory-Web. **(a)** El filtro de rutas del flujo de publicación incluye `src/GeometriaFactory.Contracts/`: la decisión 1 de §5 pasa de elevada a **confirmada por la fuente** y `PD-01` queda **cerrado**, con lo que la fila de recuentos declara **5** puntos abiertos con **4 vigentes**. **(b)** Cuando front y backend salen juntos, **primero el backend**: se agrega al hallazgo de §5 con la constancia de que el orden **no vuelve automático** el despliegue conjunto y de que el intervalo se minimiza en vez de eliminarse. Actualiza a 1.1 en §1 las versiones de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) y [`Entornos-Deploy.md`](Entornos-Deploy.md). |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a indexar la categoría de la **unidad de entrega**. Entra §0. La carpeta `_fusion/` **se retira**. Sube major. |
| 2.2 | 2026-08-24 | **Ronda 3 del corte 09 de la migración 10.0 → 13.3**, sobre el re-audit independiente, que pasó de RECHAZADO a **APROBADO CON HALLAZGOS**: el P0 y los cinco P1 quedaron cerrados y aparecieron cuatro P2 y tres P3. **Misma reparación que en la unidad hermana** (**P3**): `Pipeline-CI-CD.md` pasa de **1.1** a **3.3** y `Guia-Publicacion-Front-Ftp.md` de **1.0** a **1.2**. **Y entra la fila de `Guia-Publicacion-Bundle-Visor.md`**, que existe en la carpeta y **faltaba en esta tabla desde la emisión del índice**: el audit lo levantó al contar los archivos contra las filas. |
| 2.1 | 2026-08-24 | **Sincronización del índice con la ronda 2 del corte 09 de la migración 10.0 → 13.3.** Las tres filas de la tabla de documentos publicaban versiones **1.0 y 1.1** mientras los documentos iban por **2.x**: el desfasaje era **anterior** al corte, y el audit independiente lo levantó como **P3** porque la ronda 1 lo amplió en seis filas sin tocarlo ni declararlo. Quedan en las versiones que los documentos tienen hoy. **No se toca ninguna otra fila**: un índice que se corrige de más deja de ser comparable con el estado que describía. |
| 2.3 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **3 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
