# Pipeline CI/CD — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3 y §3.1 (los **nueve** quality gates y el carácter vinculante de las dos puertas técnicas); [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) 1.0 §3, §4 y §6; [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.0 §1.3; [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.0 §2, §3 y §5; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §14, §15, §16, §16.1, §17.6.P.7, §17.6.P.8, §17.7.P.1 a §17.7.P.11 y §18
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `10-Examples` (sample S-1) y `11-Documentacion` cuando se emitan; `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Alcance y qué no es este pipeline](#1-alcance-y-qué-no-es-este-pipeline)
- [2. Stages](#2-stages)
  - [2.1 Tabla de stages y gates](#21-tabla-de-stages-y-gates)
  - [2.2 El momento de medición de las dos puertas técnicas](#22-el-momento-de-medición-de-las-dos-puertas-técnicas)
  - [2.3 Los stages del catálogo que no existen acá](#23-los-stages-del-catálogo-que-no-existen-acá)
- [3. Triggers](#3-triggers)
- [4. Matriz de plataformas](#4-matriz-de-plataformas)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción](#6-promoción)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Qué aporta a la canalización del front](#9-qué-aporta-a-la-canalización-del-front)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance y qué no es este pipeline

`GeometriaFactory-Visor` **no es un servicio desplegable y tampoco es una biblioteca compilada como las otras dos de nivel topológico 0**: es un proyecto de la cadena de herramientas del navegador que produce **un archivo de guion generado**, el bundle. `05` §5 lo declara sin unidad de despliegue propia: su artefacto se copia al directorio de recursos estáticos de `GeometriaFactory-Web` y **viaja dentro del despliegue de esa unidad**. `redistribuible` es false y el intake §17.7.P.7 declara que **no se publica** en ningún repositorio de paquetes del ecosistema del navegador.

De ahí que su DevOps sea **construcción reproducible, medición sobre el artefacto generado y entrega al anfitrión**. Hay tres rasgos que lo distinguen de los otros dos proyectos de código de nivel 0, y los tres ordenan este documento:

1. **Tiene dependencias externas reales.** El motor de dibujo tridimensional entra como dependencia declarada y **termina dentro del bundle**, no por red de distribución externa (intake §17.7.P.1, puerta `PT-03`). Es el único de los tres con superficie de cadena de suministro que analizar.
2. **Sus gates se miden sobre el artefacto generado y no sobre el fuente.** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara la revisión sobre el bundle generado ante todo cambio, con el fundamento de que una dependencia que hace una petición por dentro **no aparece en el código fuente y sí en el bundle**.
3. **Dos puertas técnicas del producto se miden acá**, `PT-02` y `PT-03`, y **detienen la planificación de la etapa `g`** si no pasan (intake §15).

## 2. Stages

Los stages son los que declaran el intake §17.7.P.8 y `05` §5: **instalación reproducible de dependencias → empaquetado → copia al directorio de recursos estáticos del anfitrión**. Los guiones son los que el intake §16 y §17.7.P.8 declaran: `scripts/build-visor.sh` hace **sólo** el bundle, para el ciclo corto de trabajo sobre el visor, y `scripts/build.sh` lo encadena con la compilación del resto del producto.

**Todo corre dentro del contenedor de desarrollo**, incluido el gestor de paquetes del ecosistema del navegador (intake §17.7.P.1 y §10).

### 2.1 Tabla de stages y gates

| Stage | Qué ejecuta | Gate que verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `instalar` | Instalación **reproducible** de dependencias desde el archivo de bloqueo, dentro del contenedor de desarrollo | Ninguno propio: su falla detiene la construcción | — | Bloqueante por construcción |
| `empaquetar` | `scripts/build-visor.sh`: el empaquetador produce el bundle | `QG-01`: el bundle **se genera sin errores** | 0 errores | **Bloqueante** |
| `inspeccionar` | Recuentos sobre el **bundle generado** y sobre el fuente (`TC-16`, `TC-18`) | `QG-04`: **0** peticiones originadas por el archivo de guion y **0** ocurrencias de las tres formas de petición en el fuente **y en el bundle** | 0 y 0, **medido con los dos movimientos prendidos y sostenidos** | **Bloqueante, sin gradación** |
| `inspeccionar` | Lectura del almacenamiento del navegador (`TC-17`) | `QG-05`: **0** claves escritas y ningún estado conservado entre páginas | 0 | **Bloqueante, sin gradación** |
| `inspeccionar` | Recuento de la superficie expuesta por el bundle (`TC-18`) | `QG-06`: exactamente **6** funciones, **1** nombre propio en el objeto global y **0** identificadores globales sueltos | 6, 1 y 0 | **Bloqueante** |
| `probar` | Batería del proyecto de código: unitario, integración y extremo a extremo en página | `QG-07`: **100 %** de las piezas no dibujadas enumeradas con su índice y su código, y **0** sin registro (`TC-06`) | 100 % y 0 | **Bloqueante, sin gradación** |
| `probar` | `TC-21`, contra §6 del contrato de fachada | `QG-08`: los códigos de condición son exactamente **siete** y **ninguno se acuña aguas abajo** | 7 y 0 | **Se rechaza en revisión** |
| **Medición de puertas** | `TC-19` | `QG-02` (**`PT-03`**): el motor de dibujo queda **dentro** del bundle y la página funciona **sin acceso a redes de distribución externas**; **0** dependencias traídas de una red externa en tiempo de ejecución | 0 | **Bloqueante, y detiene la planificación de la etapa `g`** |
| **Medición de puertas** | `TC-20` | `QG-03` (**`PT-02`**): el bundle carga en una página del anfitrión, la creación de instancia arma la escena, la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**, **diez** recorridos de ida y vuelta no degradan, y el árbol y la escena **se sincronizan por índice** | 3 de 3 figuras; 10 recorridos sin degradación, **con los dos movimientos prendidos** | **Bloqueante, y detiene la planificación de la etapa `g`** |
| `copiar` | Copia del bundle a `src/GeometriaFactory.Web/wwwroot/js/` | Ninguno propio. Su verificación es la del anfitrión: ver [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) §3 | — | Bloqueante por construcción |
| Revisión del pull request | Comparación del bundle contra el fuente que lo generó | `QG-09`: el bundle **nunca se edita a mano**; es artefacto generado y reproducible | 0 ediciones manuales | **Se rechaza en revisión** |

**Ningún gate de este proyecto de código es condicionado**, y no es una decisión de esta categoría: [`../08-Calidad-Y-Pruebas/README.md`](../08-Calidad-Y-Pruebas/README.md) §4 lo declara y da el motivo —sus umbrales no salen de valores rotulados **[ASUNCIÓN]**, salen del contrato de la fachada y de las dos puertas técnicas—. **La única marca [ASUNCIÓN] que alcanza a este proyecto de código está en el intake §17.7.P.6 y es sobre la forma del gate —expresarlo como automatizable— y no sobre la regla**, que es `RA-02` y ya es criterio de aceptación de la etapa `g`. Por la regla que la Fase E fijó, una asunción sobre la forma **no condiciona**: `QG-04` bloquea.

**Una medición de ausencia hecha sin su condición no cuenta como medición.** Es el criterio más importante de [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3 y el pipeline lo hace cumplir: `QG-04` se mide **con los dos movimientos automáticos prendidos y sostenidos**, y el ejecutor de las pruebas de extremo a extremo tiene que poder prenderlos **aunque el entorno declare preferencia de movimiento reducido** ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2). Un pipeline que midiera cero peticiones con los movimientos apagados quedaría en verde **sin haber ejercitado nunca el bucle de dibujo**, que es donde una petición se colaría.

### 2.2 El momento de medición de las dos puertas técnicas

`PT-02` y `PT-03` **no son criterios de esta categoría ni de la 08**: las declara el intake §15 y §17.7.P.8, y se miden **antes de comprometer la etapa `g`**. [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1 declara que esta cadena **no puede convertirlas en gates condicionados, ni cambiar lo que miden, ni agregarles criterios**, y esta categoría lo adopta sin tocar nada.

Lo que sí le corresponde a 09 es declarar **cómo se ejecutan dentro de la canalización**:

| Aspecto | Decisión |
| --- | --- |
| Cuándo corre la medición | En un momento propio del producto, que **no es una etapa**: el que [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §5 llama momento de medición, ubicado antes de comprometer la etapa `g` |
| Sobre qué corre | Sobre el **bundle generado** por el stage `empaquetar`, cargado en una página real con capacidad gráfica tridimensional. Nunca sobre el fuente |
| Qué pasa si una no pasa | **La etapa `g` no se compromete.** No hay diferimiento, no hay deuda y no hay carácter condicionado ([`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §7) |
| Qué **no** hace el pipeline | No mide fluidez con un número: ninguna fuente lo da y `PA-03` de `05` §11 lo deja abierto. La verificación es **cualitativa declarada** junto con `PT-02`, y **no se reporta como si fuera un número** |

**Diecisiete de los veintiún casos de prueba de este proyecto de código se ejecutan en ese momento**, antes de que la etapa `g` se abra ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §5). La canalización lo refleja en lugar de esconderlo: el grueso del costo de ejecución de este proyecto de código cae antes de la etapa que lo necesita.

### 2.3 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en `empaquetar`** | La verificación de tipos del lenguaje fuente ocurre en el empaquetado y su falla es falla de `QG-01`. Ninguna fuente declara un linter separado; su elección concreta es de la etapa `a` |
| SCA | **Existe, y acá sí tiene sujeto** | Es el único de los tres proyectos de código de nivel topológico 0 con dependencias externas. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría**, con su alcance acotado | El motor de dibujo queda **dentro** del bundle por `PT-03`, de modo que un inventario tomado sobre las dependencias del anfitrión **no lo vería**. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma acá** | El bundle no viaja por ningún canal hacia un integrador: se copia al anfitrión y se despliega dentro de su publicación. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **No existe como publicación externa.** Lo que existe es la **entrega al anfitrión**, y tiene documento propio | Intake §17.7.P.7: no se publica. Ver [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md) |

## 3. Triggers

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa que toca `visor/` | `instalar` → `empaquetar` → `inspeccionar` → `probar` | Nada por sí solo |
| **Todo cambio del bundle** | `inspeccionar` entero —`QG-04`, `QG-05` y `QG-06`— **sobre el bundle generado y no sólo sobre la fuente** | La fusión. Es la cadencia propia que [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §5 declara |
| Apertura o actualización del pull request de la etapa | Todos los stages, más la revisión de `QG-08` y `QG-09` | La fusión |
| **Antes de comprometer la etapa `g`** | La medición de `PT-02` y `PT-03` completa, con `TC-19` y `TC-20` | **El compromiso de la etapa `g`** |
| Fusión a la rama principal | Todos los stages sobre el estado fusionado, **incluida la copia al anfitrión** | El cierre de la etapa |
| Fusión a la rama principal con cambios bajo `visor/` | Además, el flujo de trabajo de publicación del front, que el intake §17.6.P.7 declara restringido a cambios bajo `src/GeometriaFactory.Web/`, `visor/` y `src/GeometriaFactory.Contracts/` | La publicación del front |
| Etiqueta de cierre de etapa | Todos los stages sobre el estado etiquetado | La declaración de etapa cerrada |

**La anteúltima fila es propia de este proyecto de código**: el filtro de rutas de ese flujo de trabajo incluye `visor/`. **Desde el intake 1.22 ya no es el único caso** entre los tres proyectos de código de nivel topológico 0 —el filtro incluye también `src/GeometriaFactory.Contracts/`, de modo que un cambio del ensamblado de contratos dispara la misma publicación—; lo que sigue siendo propio de acá es que **el disparo existe porque el bundle se genera dentro de ese flujo**, y no porque el cambio haya que volver a desplegarlo junto con otra cosa.

**No hay trigger por calendario**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

## 4. Matriz de plataformas

Este proyecto de código tiene **dos plataformas y no una**, y confundirlas sería el error característico acá:

| Momento | Plataforma | Fundamento |
| --- | --- | --- |
| **Construcción** | El entorno de ejecución de la cadena de herramientas, en versión de soporte prolongado **anclada**, provista por el contenedor de desarrollo. El gestor de paquetes corre dentro del contenedor | Intake §17.7.P.1 y §17.7.P.9 |
| **Ejecución** | El navegador, **con capacidad gráfica tridimensional**. Sin ella el visor **no es soportado**, y la fachada informa la condición correspondiente. **En tiempo de ejecución no hay entorno de la cadena de herramientas**: hay un archivo servido como recurso estático | Intake §17.7.P.9; `05` §5 |

**El requisito de navegador se declara por capacidad y no por versión**, porque la fuente no fija versiones mínimas (intake §17.7.P.9). Es el punto abierto `PA-04` de `05` §11 y **esta categoría no lo cierra inventando una versión**.

**Consecuencia sobre la matriz del pipeline:** la construcción corre en una sola plataforma —la del contenedor— y la medición de extremo a extremo exige **un navegador con capacidad gráfica tridimensional y un conductor capaz de contar peticiones de red y de leer el almacenamiento del navegador** ([`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2). **Sin ese conductor, `QG-04`, `QG-05` y las dos puertas técnicas no se pueden medir**, y un ejecutor que no lo provea deja al proyecto de código sin sus gates más importantes. Queda registrado como `PD-02` en §10.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Instalación de dependencias | **Reproducible desde el archivo de bloqueo**, no resolución libre de versiones | Intake, encabezado de la Parte C: toda versión se fija explícitamente y un cambio mayor se documenta, **nunca como efecto colateral de una actualización** |
| Caché | Caché del gestor de paquetes, con llave derivada del archivo de bloqueo. **Sin expiración por tiempo** | Decisión de esta categoría; un plazo en días no lo da ninguna fuente |
| Artefacto principal | **El bundle**, `visor.bundle.js`, producido por el stage `empaquetar` y copiado a `src/GeometriaFactory.Web/wwwroot/js/` | Intake §13 y §16 |
| Artefacto de inspección | Los **recuentos** de `QG-04`, `QG-05` y `QG-06`, tomados sobre el bundle generado | [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3 |
| Artefacto del momento de medición | El registro de `PT-02` y `PT-03`, **con la condición en que se midió cada ausencia** | Definition of Done §1.3 |
| Inventario de componentes del bundle | Emitido en el stage `empaquetar`. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 | Decisión de esta categoría |
| Retención | Mientras dure el punto de control; los registros se adjuntan al informe de cierre | Intake §15 |
| Reproducibilidad exigida | **Dos construcciones desde el mismo estado producen el mismo artefacto** | [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §8, segunda métrica |

**El bundle no se versiona en el repositorio.** Es la resolución del punto abierto `PA-05`, y su fundamento completo está en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2: se genera en cada canalización que lo necesita y se ignora en el control de versiones.

## 6. Promoción

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Los gates bloqueantes de §2.1 y los **diez** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con OK explícito (intake §15) |
| **Momento de medición → compromiso de la etapa `g`** | Las dos puertas técnicas pasadas enteras, en sus **seis** tramos `CV-18` a `CV-23` | `TC-19` y `TC-20`, con sus condiciones de medición registradas | El mismo, con el registro de la medición |
| Bundle construido → bundle en el anfitrión | El stage `copiar` | Los gates de `inspeccionar` en verde sobre ese mismo bundle | Automático dentro de la construcción |

**La segunda fila no tiene equivalente en ningún otro proyecto de código de nivel topológico 0.** Es una promoción cuyo prerrequisito no es un gate de esta cadena sino una **puerta técnica del producto**, y su incumplimiento no bloquea una fusión: bloquea que una etapa **se planifique**.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| El bundle desplegado rompe la visualización | Volver a la **etiqueta de la etapa anterior** y **regenerar** el bundle desde ese estado. No se restaura un archivo guardado: se reconstruye | Intake §17.1.P.8, modelo del producto; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §5, punto 2 |
| El bundle desplegado en el hosting está roto | La reversión efectiva es la del front: **volver a publicar desde la etiqueta anterior**, que es el procedimiento que el intake §17.6.P.8 declara para esa unidad. La regeneración del bundle es parte de esa publicación | Intake §17.6.P.8 |
| Un cambio mayor del punto de extensión rompió al anfitrión | **Ninguna compilación lo detectó**, y es la asimetría que `ADR-06` §6 acepta. Se revierte por etiqueta y la mitigación previa es la revisión más el sample **S-1**, que ejerce el contrato entero | `ADR-06` §2 y §5, punto 3 |

**Que el bundle se regenere y no se restaure es consecuencia directa de resolver `PA-05` como se resolvió**, y es una propiedad y no un costo: un archivo restaurado desde el control de versiones podría no corresponder al fuente, que es exactamente lo que `QG-09` y `CV-30` prohíben.

## 8. Notificaciones

| Canal | Qué comunica |
| --- | --- |
| La salida del pipeline sobre el pull request de la etapa | El resultado del empaquetado y **los recuentos** de las tres inspecciones sobre el bundle generado |
| El registro del momento de medición | `PT-02` y `PT-03` tramo por tramo, **con la condición de medición junto a cada resultado** |
| El informe de cierre de la etapa | Lo anterior, más la verificación cualitativa de fluidez **rotulada como cualitativa** |
| El registro de cambios del producto | Toda fila de cambio mayor del punto de extensión (`ADR-06` §7) |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

## 9. Qué aporta a la canalización del front

Este proyecto de código no se despliega, pero **su artefacto viaja dentro del despliegue del front**, y eso lo hace parte de esa canalización:

| Aporte | Efecto | Fundamento |
| --- | --- | --- |
| El flujo de trabajo del front **genera el bundle en su propio interior** —instalación de dependencias, empaquetado y copia a los recursos estáticos— antes de publicar | La canalización del front **no toma el bundle del repositorio**: lo construye | Intake §17.6.P.8, pasos del flujo de trabajo |
| Ese flujo declara como **gate bloqueante** que el bundle se genere en el mismo flujo y **nunca se tome de un artefacto viejo** | Es la razón operativa por la que versionar el bundle no aportaría nada: el artefacto versionado no se usaría | Intake §17.6.P.8, quality gates |
| Un cambio bajo `visor/` **dispara** ese flujo | El filtro de rutas lo incluye desde el principio; desde el intake **1.22** también incluye `src/GeometriaFactory.Contracts/`, así que **dos** de los tres proyectos de código de nivel 0 disparan esa publicación, y `GeometriaFactory-Domain` ninguno | Intake §17.6.P.7 |
| `PT-03` garantiza que el motor de dibujo esté **dentro** del bundle | El front funciona sin acceso a redes de distribución externas, que es lo que la puerta mide | Intake §17.7.P.8 |
| `RA-02`, sostenida por `QG-04` | El bundle no hace red, y por eso `RA-01` **no se puede violar desde el navegador** | Intake §14 |

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | La **herramienta concreta** de cada stage —empaquetador, ejecutor de pruebas, conductor de navegador y generador del inventario de componentes— y su anclaje de versión | El equipo, en el punto de control de la etapa `a` | Etapa `a`, por la regla de anclaje de versiones del intake |
| PD-02 | Que el ejecutor de la canalización provea **navegador con capacidad gráfica tridimensional y conductor capaz de contar peticiones y leer el almacenamiento**. Sin eso, `QG-04`, `QG-05` y las dos puertas técnicas no se pueden medir en la canalización y quedarían como medición manual registrada | El equipo, antes del momento de medición | Antes de comprometer la etapa `g` |
| PD-03 | La **versión del motor de dibujo tridimensional** que se ancla, y el cambio de interfaz que exija si es posterior a la del visualizador previo | El equipo, por `BT-09` | Antes del momento de medición (`PA-01` de `05` §11) |

**`PA-05` de `05` §11 queda cerrado por esta categoría**, con su desenlace y su fundamento en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2. **`PA-03` —el umbral numérico de fluidez— sigue abierto y esta categoría no lo cierra**: inventar un número acá lo propagaría como si fuera del producto.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los stages que el intake §17.7.P.8 fija —instalación reproducible, empaquetado, inspección, prueba y copia al anfitrión— y los **nueve** quality gates de `08` §3 materializados uno por uno, **ninguno condicionado**, con la constancia de que la única marca [ASUNCIÓN] que alcanza a este proyecto de código es sobre la forma del gate y no sobre la regla. Declara que las inspecciones corren **sobre el bundle generado y no sobre el fuente**, y que una medición de ausencia sin su condición **no cuenta**. Declara el momento de medición de `PT-02` y `PT-03` como transición propia de la promoción, sin convertirlas en condicionadas ni cambiar lo que miden, y que si una no pasa **la etapa `g` no se compromete**. Declara las **dos** plataformas —construcción y ejecución— y que sin conductor de navegador los gates principales no se pueden medir. Declara que el bundle **se regenera y no se restaura** en la reversión, y **tres** puntos abiertos, dejando constancia de que `PA-05` queda cerrado y `PA-03` sigue abierto. |
| 1.1 | 2026-08-11 | **Propagación de la primera decisión de despliegue del Product Owner** del intake **1.22** §17.6.P.7: el filtro de rutas del flujo que publica el front incluye ahora `src/GeometriaFactory.Contracts/`, además de `src/GeometriaFactory.Web/` y `visor/`. Se corrige la enumeración del filtro en la tabla de triggers de §3 y se **reescribe el fundamento** de las dos afirmaciones que se apoyaban en que el filtro tenía dos rutas: este proyecto de código ya no es el único de nivel topológico 0 cuyo cambio dispara una publicación —ahora son **dos** de los tres—, y lo que le queda de propio es que el disparo existe porque el bundle se genera dentro de ese flujo. Sube la trazabilidad upstream del intake de **1.20** a **1.22**. |
