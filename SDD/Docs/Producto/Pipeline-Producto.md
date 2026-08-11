# Pipeline de producto — Fábrica de Geometría

**Producto:** Fábrica de Geometría
**Documento:** Pipeline-Producto.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior, con foco en Release Engineering y Platform Engineering (AG-09)
**Nivel:** Producto
**Trazabilidad upstream:** [`PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **1.3** §2, §3 y §4; [`PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.26** §13, §15 y §17 P.7 y P.8; las **siete** categorías `09-Devops` emitidas bajo `Proyectos/`; [`Vista-Producto.md`](Vista-Producto.md) **1.2**
**Trazabilidad downstream:** `11-Documentacion` de nivel producto —`Guia-Inicio-Rapido` y `Guia-Despliegue`, todavía en estado `Planificado`— y [`../README.md`](../README.md)

---

## Tabla de contenido

- [1. Objetivo y alcance](#1-objetivo-y-alcance)
- [2. Orden de construcción](#2-orden-de-construcción)
- [3. Matriz de build y publicación multi-proyecto](#3-matriz-de-build-y-publicación-multi-proyecto)
- [4. Coordinación inter-proyecto](#4-coordinación-inter-proyecto)
- [5. Versionado del producto](#5-versionado-del-producto)
- [6. Gate de integración de producto](#6-gate-de-integración-de-producto)
- [7. Rollback coordinado](#7-rollback-coordinado)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Puntos abiertos que alcanzan al pipeline](#9-puntos-abiertos-que-alcanzan-al-pipeline)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Objetivo y alcance

Este documento orquesta la construcción y la publicación del producto completo por encima de la canalización de cada proyecto de código. **No duplica ningún `Pipeline-CI-CD.md`: lo referencia.** El detalle de stages, gates y umbrales vive en `Proyectos/<Nombre-Proyecto-Codigo>/09-Devops/`, y cada afirmación de acá apunta al documento que la decide.

Lo que sólo puede documentarse a este nivel son cuatro cosas: en qué orden se construyen siete proyectos de código que dependen unos de otros, qué sale del repositorio y por qué canal, cómo obtiene cada consumidor el artefacto de su productor, y qué pasa cuando hay que volver atrás en las **dos** unidades desplegables a la vez.

**Este producto no publica ningún paquete.** Ninguno de los siete proyectos de código es `redistribuible` (`PRODUCT-MANIFEST` §2), no hay feed de paquetes en ninguna parte y las cinco bibliotecas nunca salen del repositorio como artefacto propio. La confusión entre publicar y desplegar es el anti-patrón que la guía de la categoría marca, y acá la distinción tiene consecuencias concretas: hay artefactos que **se despliegan sin publicarse nunca**.

**Alcance temporal.** El documento describe la canalización tal como las siete categorías `09` la especifican. Ninguna de ellas ha corrido todavía: la Fase I no arrancó y no hay código. Todo umbral citado es el declarado, no el medido.

## 2. Orden de construcción

El orden lo fija el grafo de dependencias del manifiesto, en cuatro niveles topológicos. Cada nivel se construye entero antes que el siguiente; dentro de un nivel, los proyectos de código son paralelizables porque no se referencian entre sí.

| Nivel | Proyectos de código | Paralelizables | Qué habilita al terminar |
| --- | --- | --- | --- |
| 0 | `GeometriaFactory-Domain`, `GeometriaFactory-Contracts`, `GeometriaFactory-Visor` | Sí, los tres | El dominio, los tipos de transferencia y el bundle del visor quedan disponibles para sus consumidores |
| 1 | `GeometriaFactory-Application`, `GeometriaFactory-Web` | Sí, los dos | Los casos de uso y los cuatro puertos, y el front con el bundle ya embebido |
| 2 | `GeometriaFactory-Infrastructure` | — | Los adaptadores de los cuatro puertos, la seguridad y el validador de figuras |
| 3 | `GeometriaFactory-Api` | — | La unidad desplegable del servidor propio, con la composición de raíz conectada |

**El nivel 0 no publica nada y aun así es el que más condiciona.** `GeometriaFactory-Visor` es el único de los tres cuyo artefacto **es un archivo que se entrega**: su bundle se copia al directorio de recursos estáticos del front, de modo que un nivel 1 construido sobre un bundle viejo produce un front que se ve bien y dibuja mal. Por eso el orden dentro del repositorio no es una formalidad de compilación.

**Ningún redistribuible se publica antes que sus consumidores, porque no hay redistribuibles.** La guía de la categoría pide ese orden; acá la exigencia se satisface de forma degenerada y se declara en lugar de dejarse implícita.

## 3. Matriz de build y publicación multi-proyecto

Una fila por proyecto de código. La columna de artefacto publicable dice **qué sale del repositorio**, no qué se compila: las cinco bibliotecas se compilan y ninguna sale.

| Proyecto de código | Tipo D8 | Artefacto que sale del repositorio | Canal o feed | ¿Lo consume otro proyecto de código del producto? | Guía de publicación |
| --- | --- | --- | --- | --- | --- |
| `GeometriaFactory-Domain` | `library` | Ninguno | Sin feed | Sí: `Application` e `Infrastructure`, por referencia dentro del agrupador | — |
| `GeometriaFactory-Contracts` | `library` | Ninguno | Sin feed | Sí: `Api` y `Web`, **los dos a la vez** | — |
| `GeometriaFactory-Visor` | `library` (paquete Node) | El **bundle** del visor | Copia al directorio de recursos estáticos del front, dentro del repositorio | Sí: `Web` | [`Guia-Publicacion-Bundle-Visor.md`](../Proyectos/GeometriaFactory-Visor/09-Devops/Guia-Publicacion-Bundle-Visor.md) |
| `GeometriaFactory-Application` | `library` | Ninguno | Sin feed | Sí: `Infrastructure` y `Api` | — |
| `GeometriaFactory-Web` | `web-monolith` | La **publicación del front** | Transferencia al hosting público, disparada por fusión a la rama principal | No | [`Guia-Publicacion-Front-Ftp.md`](../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md) |
| `GeometriaFactory-Infrastructure` | `library` | Ninguno | Sin feed | Sí: `Api` | — |
| `GeometriaFactory-Api` | `rest-api` | La **imagen de contenedor**, construida en el destino desde el repositorio | Sin registro de imágenes: el destino construye | No por compilación; `Web` lo alcanza en **tiempo de ejecución** | [`Guia-Publicacion-Image-Docker.md`](../Proyectos/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md) |

**Tres guías de publicación para siete proyectos de código, y las cuatro ausencias son decisión declarada.** `Domain`, `Contracts`, `Application` e `Infrastructure` no emiten guía porque no tienen artefacto que salga; sus categorías `09` registran el apartamiento del modelo de canales `preview` / `stable` que la guía fija para el tipo `library`, cada una contra la ADR que lo sostiene.

**El caso de `GeometriaFactory-Contracts` es el que mejor muestra la distinción entre publicar y desplegar**, y su propia categoría `09` lo dice así: no se publica nada —no hay feed— y sin embargo el ensamblado llega a los **dos** ambientes de ejecución del producto, embebido en las dos unidades desplegables. Un lector que confunda las dos cosas concluye que el contrato no llega a producción.

**Dos unidades desplegables, tres artefactos que salen.** El bundle del visor sale del repositorio hacia el directorio del front y viaja adentro de la publicación de `Web`; no es una tercera unidad desplegable.

## 4. Coordinación inter-proyecto

Cómo obtiene cada consumidor el artefacto de su productor, arista por arista. **Ninguna arista se resuelve por paquete publicado**, y ésa es la propiedad de la que depende el versionado de §5.

| Arista | Clase | Cómo la resuelve el consumidor | Documento que lo decide |
| --- | --- | --- | --- |
| `Domain → Application` | Compilación | Build conjunto en el repositorio, por referencia dentro del agrupador | `PRODUCT-MANIFEST` §2 y §3 |
| `Domain → Infrastructure` | Compilación | Build conjunto | `PRODUCT-MANIFEST` §2 y §3 |
| `Application → Infrastructure` | Compilación | Build conjunto | `PRODUCT-MANIFEST` §2 y §3 |
| `Infrastructure → Api` | Compilación | Build conjunto | `PRODUCT-MANIFEST` §2 y §3 |
| `Contracts → Api` | Compilación | Build conjunto | `PRODUCT-MANIFEST` §2 y §3 |
| `Contracts → Web` | Compilación | Build conjunto | `PRODUCT-MANIFEST` §2 y §3 |
| `Visor → Web` | Compilación del empaquetado del front | **Copia del bundle generado** al directorio de recursos estáticos del anfitrión. El bundle **no se versiona en el repositorio**: lo genera la canalización antes de publicar | [`Visor/Entornos-Deploy.md`](../Proyectos/GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2, que cerró el punto abierto `PA-05` de aquel proyecto de código |
| `Application → Api` | Compilación, **en disputa** | Build conjunto bajo las dos lecturas | Ver [`Vista-Producto.md`](Vista-Producto.md) §3.1: el manifiesto declara la arista en §2 y no la dibuja en §3 |
| `Web → Api` | **Tiempo de ejecución**, HTTP servidor a servidor | La dirección del servicio de datos **llega por configuración**, no por referencia | [`Web ADR-07`](../Proyectos/GeometriaFactory-Web/05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) |

**Que todas las aristas de compilación se resuelvan por build conjunto es lo que vuelve inofensiva la discrepancia del grafo.** Con un feed de paquetes de por medio, siete u ocho aristas serían dos configuraciones de publicación distintas; con un solo agrupador y un solo repositorio, la diferencia se reduce a si el archivo de proyecto de la `Api` declara la referencia directa o la recibe transitivamente. **No es motivo para cerrar la discrepancia por conveniencia**: sigue elevada.

**El filtro de rutas del flujo que publica el front incluye las tres entradas de compilación de `GeometriaFactory-Web`.** El intake §17.6.P.7 declara el disparo por fusión a la rama principal restringido a cambios bajo el directorio del front, el del visor y el de los contratos. La tercera ruta entró por una corrección que `GeometriaFactory-Contracts` elevó: con sólo las dos primeras, un cambio del contrato no disparaba la publicación del front y las dos unidades quedaban desalineadas sin que nada fallara.

## 5. Versionado del producto

**El producto se versiona en lockstep de hecho, y el instrumento es una etiqueta del repositorio por etapa cerrada.** Los siete proyectos de código heredan el mismo esquema de ramas, declarado en el intake §15: una rama por etapa a partir de la principal, un pull request por etapa que **es** el punto de control, etapas en serie y etiqueta al fusionar. No hay una versión por proyecto de código que se mueva sola.

| Aspecto | Decisión | Dónde se decide |
| --- | --- | --- |
| Esquema de versión | Versionado semántico 2.0.0, sin excepciones | Intake §17 P.7 de cada proyecto de código |
| Unidad de versionado | La **etapa cerrada** del producto, etiquetada en el repositorio | Intake §15 |
| Sufijos de anticipo | **No se usan**: no hay canal donde publicar un anticipo | [`Web/Estrategia-Versionado.md`](../Proyectos/GeometriaFactory-Web/09-Devops/Estrategia-Versionado.md) |
| Versionado del contrato compartido | **Por compilación compartida**: un cambio incompatible rompe la compilación de los dos extremos antes que el tiempo de ejecución | [`Contracts ADR-03`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) |
| Versionado de la superficie HTTP | **No se versionan las rutas**, y la contrapartida aceptada es el **despliegue conjunto** | [`Api ADR-08`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) |

**Cómo se coordina un cambio del productor con sus consumidores.** Un cambio incompatible de `GeometriaFactory-Contracts` no necesita coordinación de versiones porque no hay versiones que coordinar: rompe la compilación de `Api` y de `Web` en el mismo build. Lo que sí necesita coordinación es el **despliegue**, y ahí la regla es dura: las dos unidades salen desde el mismo estado del repositorio o la etapa no se cierra.

**Tres clases de cambio del contrato compilan igual y no las detecta nada automático**: la configuración de intercambio, el esquema del almacén y las rutas. Cada una tiene su mecanismo propio, enumerado en [`Contratos-REST.md`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §7. Es la razón por la que el marcador de cambio incompatible en el mensaje de confirmación **se escribe a mano y no se deduce de que algo falle al construir**.

**La herramienta que calcula la versión a partir de los mensajes de confirmación no está elegida.** Tres proyectos de código lo declaran abierto y lo atan a la etapa `a`. Ver §9.

## 6. Gate de integración de producto

La verificación de que los proyectos de código integrados funcionan juntos **no vive en la canalización de ninguna biblioteca**: vive en la de `GeometriaFactory-Api`, que es donde el intake §17.5.P.6 pone la batería de integración, y se completa con las cinco puertas técnicas del producto.

| Gate | Qué verifica del producto integrado | Dónde se mide | Carácter |
| --- | --- | --- | --- |
| Batería de integración | La superficie real por su protocolo, contra el almacén real, con los tipos del contrato compartido | Stage `test` de [`Api/Pipeline-CI-CD.md`](../Proyectos/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md) §2.1 | Bloqueante |
| `PT-01`, en sus cuatro partes | El modelo de front entero: transporte, estabilidad y comportamiento en el hosting | Etapa `a`, antes que cualquier otra cosa | Bloqueante para todo lo demás |
| `PT-04` | Que la imagen del backend se construya y arranque desde el contenedor de desarrollo | Etapa `a`, stage `imagen` | Bloqueante |
| `PT-02` y `PT-03` | Que el visor funcione embebido en el anfitrión y que el motor de dibujo quede dentro del bundle, sin depender de una red de distribución | Antes de comprometer la etapa `g` | Bloqueante para esa etapa |
| `PT-05` | La premisa completa de la topología, en el despliegue real | Etapa `i` | Bloqueante para el despliegue |

**Una puerta que no pasa detiene la planificación de las etapas que dependen de ella; no se arrastra como deuda.** Lo declara el intake §15 y esta orquestación lo hereda sin ablandarlo.

**Lo que este producto no tiene, y conviene decirlo antes de que alguien lo busque.** No hay ambiente de ensayo en ninguna de las dos unidades desplegables: las dos declaran dos ambientes —desarrollo y producción— y registran el apartamiento del modelo de cuatro que la guía de la categoría fija, cada una contra su ADR. La consecuencia está escrita en los dos documentos y no se disimula acá: **la primera vez que una versión corre en condiciones reales es en producción**.

## 7. Rollback coordinado

El orden de reversión es el inverso al de construcción, con una asimetría que domina todo lo demás.

| Situación | Procedimiento | Orden |
| --- | --- | --- |
| Una etapa fusionada rompe algo que estaba en verde | Volver a la etiqueta de la etapa anterior | Único punto: la etiqueta gobierna los siete proyectos de código a la vez |
| El servicio del servidor propio está roto | Volver a la etiqueta anterior y **reconstruir en destino**. No hay imagen publicada a la que volver | Backend |
| La publicación del front está rota | Reversión propia del canal de publicación, según su guía | Front |
| Un cambio incompatible del contrato llegó a las dos unidades | **Se revierten las dos juntas.** La reversión desacoplada reproduce el riesgo `RI-02` de [`Vista-Producto.md`](Vista-Producto.md) §7 | Las dos, desde el mismo estado del repositorio |
| Una transformación de esquema del almacén quedó mal | **Volver a la etiqueta no deshace el esquema.** Se corrige con otra transformación | No hay reversión: sólo avance |

**El artefacto compartido que puede romper a varios consumidores es uno solo: el ensamblado de contratos.** Rompe a `Api` y a `Web` al mismo tiempo y en el mismo build, que es exactamente lo que la compilación compartida busca. Su reversión no tiene coordinación especial porque no tiene publicación: se revierte el repositorio.

**El orden de salida cuando front y backend cambian juntos: primero el backend.** Es decisión del Product Owner registrada en el intake §17.6.P.7. El fundamento está escrito allí: un servicio nuevo normalmente acepta lo que mandaba el front anterior, mientras que un front nuevo contra un servicio viejo le pide algo que todavía no existe y el error lo ve el alumno. **El orden no vuelve automático el despliegue conjunto**: el front sale al fusionar y el backend se despliega a mano, de modo que el intervalo entre los dos se minimiza y se registra, no se elimina.

**La asimetría que ningún procedimiento de reversión puede ignorar: el código vuelve atrás y el almacén no.**

## 8. Trazabilidad

Cada elemento de este documento contra la fuente que lo produce.

| Elemento | Liga a |
| --- | --- |
| Orden de construcción de §2 | `PRODUCT-MANIFEST` §3, orden topológico de cuatro niveles |
| Cada arista de §4 | `PRODUCT-MANIFEST` §2, columna `Dependencias`, con la salvedad de §3.1 de la vista de producto |
| Artefacto de `GeometriaFactory-Api` | [`Guia-Publicacion-Image-Docker.md`](../Proyectos/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md), tipo de artefacto `image-docker` |
| Artefacto de `GeometriaFactory-Web` | [`Guia-Publicacion-Front-Ftp.md`](../Proyectos/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md), tipo de artefacto `Front-Ftp` |
| Artefacto de `GeometriaFactory-Visor` | [`Guia-Publicacion-Bundle-Visor.md`](../Proyectos/GeometriaFactory-Visor/09-Devops/Guia-Publicacion-Bundle-Visor.md), tipo de artefacto `Bundle-Visor` |
| Ausencia de guía en los otros cuatro | La sección de ambientes y canales de cada uno, bajo `Proyectos/<Nombre>/09-Devops/Entornos-Deploy.md`, con su apartamiento declarado |
| Gates de §6 | [`Api/Pipeline-CI-CD.md`](../Proyectos/GeometriaFactory-Api/09-Devops/Pipeline-CI-CD.md) §2.1 y §9; intake §15, tabla de puertas técnicas |
| Reglas de despliegue conjunto de §5 y §7 | [`Api ADR-08`](../Proyectos/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) y [`Contracts ADR-03`](../Proyectos/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md) |

**Los 77 quality gates del producto no se reindexan acá.** Están repartidos por proyecto de código —`Api` 15, `Infrastructure` 14, `Application` 11, `Web` 11, `Contracts` 9, `Visor` 9 y `Domain` 8—, cada uno con su umbral y su carácter en la categoría `09` que lo declara. Copiarlos produciría una segunda fuente de verdad sobre setenta y siete umbrales, que es el defecto que este corpus tiene documentado como el más caro.

## 9. Puntos abiertos que alcanzan al pipeline

Este documento **no resuelve ninguno**: los registra con su titular, porque un pipeline que oculta lo que todavía no está decidido se lee como si estuviera completo.

| Punto abierto | Estado | Titular | Dónde está declarado |
| --- | --- | --- | --- |
| Cuántas aristas de compilación tiene el producto, siete u ocho | Abierto y elevado | Product Owner, sobre `PRODUCT-MANIFEST` §2, §3 y §4 | [`Vista-Producto.md`](Vista-Producto.md) §3.1 |
| La herramienta que calcula la versión desde los mensajes de confirmación | Abierto, atado a la etapa `a` | El equipo, en el punto de control de la etapa `a` | `05` §11 de `Domain`, `Application` y `Visor` |
| Que el motor de contenedores del destino resuelva la referencia al repositorio para construir la imagen | **[A VERIFICAR]** en la fuente; hay que probarlo una vez antes de depender del mecanismo | El equipo, antes de la etapa `i` | [`Api/Entornos-Deploy.md`](../Proyectos/GeometriaFactory-Api/09-Devops/Entornos-Deploy.md) §3 |
| Las capacidades del hosting público, incluida la versión de plataforma que soporta | **[A VERIFICAR]**; es `PT-01.a`, y si no pasa se baja la versión objetivo del front y no la del backend | La medición de la etapa `a` | `Web` `05` §11 `PA-02` |
| Los umbrales rotulados `[ASUNCIÓN]` que condicionan gates: coberturas, latencias, caudal y arranque en frío | Abiertos, pendientes de confirmación | Product Owner, sobre `PRODUCT-INTAKE` §22, asunciones `A-3`, `A-4` y `A-5` | `08` y `09` de cada proyecto de código, en los gates rotulados condicionados |
| El alcance de la colección de peticiones reproducible | Abierto: la fuente lo declara en dos lugares con alcances distintos | Product Owner, sobre `PRODUCT-INTAKE` §16.1 y §18 | `Api` `05` §11 `PA-06` |

**Ninguno de los seis bloquea la emisión de este documento**, y por eso se emite. Cuatro de ellos sí bloquean el cierre de la etapa `a` o de la etapa `i`, y ahí están anclados con su punto de control.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial, en la consolidación de la Fase H y **una sola vez para todo el producto**, porque tiene más de un proyecto de código. Declara las **ocho** secciones que la guía de la categoría exige: el alcance y la frontera contra las siete canalizaciones por proyecto de código; el orden de construcción en los cuatro niveles topológicos del manifiesto; la matriz de build y publicación, con **tres** artefactos que salen del repositorio, **cuatro** proyectos de código sin artefacto propio y **cero** feeds de paquetes; la coordinación de las **nueve** aristas, todas por build conjunto salvo la copia del bundle y la resolución por configuración de la arista de tiempo de ejecución; el versionado en lockstep por etiqueta de etapa cerrada, con la compilación compartida y el despliegue conjunto como mecanismos; el gate de integración de producto con la batería de integración y las **cinco** puertas técnicas; la reversión coordinada con el orden de salida decidido por el Product Owner —**primero el backend**— y la asimetría del almacén; y la trazabilidad de cada artefacto contra su guía de publicación. Suma una novena sección con los **seis** puntos abiertos que alcanzan al pipeline, **ninguno de los cuales se resuelve acá**. **No toma ninguna decisión y no reabre ninguna de las 45 ADR emitidas**: referencia, no reescribe. **Autor:** Ingeniero DevOps Senior (AG-09) |
