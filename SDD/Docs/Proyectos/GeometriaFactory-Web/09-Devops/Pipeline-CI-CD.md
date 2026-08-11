# Pipeline CI/CD — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Pipeline-CI-CD.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3, §3.1 y §3.2 (los **once** quality gates y las **tres** puertas técnicas); [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.1 §3 (los **once** criterios de salida); [`../08-Calidad-Y-Pruebas/Definition-Of-Done.md`](../08-Calidad-Y-Pruebas/Definition-Of-Done.md) 1.1 §1.3 y §1.4; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) 1.2 (las **61** sondas); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §14, §15, §16, §17.4.P.3, §17.6.P.1 a §17.6.P.12, §17.7.P.7 y §17.7.P.8; [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) 1.0 §6 y §10 (`PD-01`, elevado a esta categoría); [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) 1.0 §2
**Trazabilidad downstream:** [`Estrategia-Versionado.md`](Estrategia-Versionado.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `11-Documentacion` cuando se emita; `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Alcance: acá sí hay despliegue](#1-alcance-acá-sí-hay-despliegue)
- [2. Stages](#2-stages)
  - [2.1 Los ocho pasos del flujo de publicación](#21-los-ocho-pasos-del-flujo-de-publicación)
  - [2.2 Tabla de gates](#22-tabla-de-gates)
  - [2.3 Ningún gate de este proyecto de código es condicionado](#23-ningún-gate-de-este-proyecto-de-código-es-condicionado)
  - [2.4 Los stages del catálogo que no existen acá](#24-los-stages-del-catálogo-que-no-existen-acá)
- [3. Triggers, y la resolución de `PD-01`](#3-triggers-y-la-resolución-de-pd-01)
  - [3.1 El hallazgo que Contracts elevó, leído entero](#31-el-hallazgo-que-contracts-elevó-leído-entero)
  - [3.2 Qué decide esta categoría](#32-qué-decide-esta-categoría)
- [4. Matriz de plataformas](#4-matriz-de-plataformas)
- [5. Caché y artefactos](#5-caché-y-artefactos)
- [6. Promoción](#6-promoción)
- [7. Reversión](#7-reversión)
- [8. Notificaciones](#8-notificaciones)
- [9. Las tres puertas técnicas dentro de la canalización](#9-las-tres-puertas-técnicas-dentro-de-la-canalización)
- [10. Puntos abiertos](#10-puntos-abiertos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Alcance: acá sí hay despliegue

A diferencia de los tres proyectos de código de nivel topológico 0 y de las bibliotecas del backend, **`GeometriaFactory-Web` tiene unidad de despliegue propia**: `05` §5 la declara como «la publicación de la aplicación en el hosting público, con dominio y transporte seguro», y una de las **dos** unidades desplegables del producto.

Tres rasgos ordenan este documento, y los tres son de la fuente:

1. **Es el anfitrión del bundle del visor**, de modo que su canalización incluye la cadena de herramientas del navegador además de la de la plataforma. `05` §5 declara qué viaja adentro: la aplicación, los tipos de `GeometriaFactory-Contracts` compilados y **el bundle del visor como recurso estático generado**, que se copia al directorio de recursos estáticos y **nunca se edita a mano**.
2. **Su canal de entrega no es un registro de artefactos: es una subida por FTP**, que el intake §17.6.P.7 declara y que **no es transaccional** (`R-03`). De ahí que el flujo no termine en la subida.
3. **No tiene proyecto de pruebas propio.** El intake §17.6.P.6 lo declara: su verificación es el guion de demostración de cada etapa, acumulativo, más las pruebas de integración que ejercitan el servicio que consume. Por eso **acá no hay stage de `test` con batería propia**, y decirlo es más honesto que inventarle uno.

Lo que este pipeline ejecuta y bloquea son **los once quality gates** de [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 y **las tres puertas técnicas** de su §3.2. **Esta categoría no los redefine, no los relaja y no agrega ninguno.**

## 2. Stages

### 2.1 Los ocho pasos del flujo de publicación

Los pasos son los que el intake §17.6.P.8 declara, **en su orden y sin agregar ninguno**. `05` §5 enumera **el mismo conjunto en siete**, porque agrupa las dos preparaciones de cadena de herramientas en una; el conjunto de actos es idéntico.

| # | Paso | Qué hace | Gate que verifica |
| --- | --- | --- | --- |
| 1 | Obtención del código | Trae el estado del repositorio que se va a publicar | Ninguno propio |
| 2 | Preparación de la cadena de la plataforma | Deja disponible la plataforma objetivo del front | Ninguno propio. Su falla es la señal de `PT-01.a` cuando la versión del hosting no acompaña |
| 3 | Preparación de la cadena del navegador e **instalación reproducible** de dependencias en `visor/` | Instala desde el archivo de bloqueo, no por resolución libre | Ninguno propio |
| 4 | **Empaquetado del bundle y copia** al directorio de recursos estáticos | Genera el bundle en este mismo flujo y lo copia a `src/GeometriaFactory.Web/wwwroot/js/` | **`QG-02`**: el bundle se genera **en el mismo flujo**, nunca se toma de un artefacto viejo |
| 5 | Publicación de la aplicación | Produce la salida publicable del front | **`QG-01`**: la construcción termina **sin advertencias** |
| 6 | Inyección de la dirección del servicio de datos desde secretos | Toma el valor del almacén de secretos del repositorio; **la dirección real no se versiona** | Ninguno propio. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |
| 7 | Subida por FTP | Deja la publicación en el hosting. **No es transaccional** (`R-03`) | Ninguno propio |
| 8 | **Verificación de que la dirección pública responde** | Cierra el flujo comprobando el resultado, no la acción | **`QG-03`**: el flujo **no termina en la subida** |

**El paso 8 es el que define el carácter de esta canalización.** El intake §17.6.P.8 lo funda sin ambigüedad: **«una subida por FTP que deja la aplicación caída y se reporta como exitosa es peor que una falla visible»**. Un flujo que terminara en el paso 7 dejaría sin detección el modo de falla más caro del producto.

**El paso 4 es el que hace de este flujo el único lugar donde el bundle existe para un usuario.** [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2 cerró el punto abierto `PA-05` de aquel proyecto de código decidiendo que **el bundle se ignora en el repositorio y lo genera la canalización**, y uno de sus cuatro fundamentos es precisamente este paso: la canalización ya lo genera, y `QG-02` prohíbe tomarlo de un artefacto viejo. Esta categoría **adopta esa decisión sin reabrirla** y resuelve su consecuencia operativa pendiente en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2.

### 2.2 Tabla de gates

| Gate | Dónde corre | Qué verifica | Umbral | Carácter |
| --- | --- | --- | --- | --- |
| `QG-01` | Paso 5 del flujo | La construcción termina **sin advertencias** | 0 advertencias | **Bloquea la fusión** |
| `QG-02` | Paso 4, e inspección de la definición del flujo | El bundle se genera **en el mismo flujo**, y el paso de generación **precede** al de publicación sin artefacto cacheado de por medio | 0 bundles tomados de un artefacto anterior | **Bloquea la publicación** |
| `QG-03` | Paso 8 | La dirección pública **responde** | La dirección pública responde | **Bloquea el flujo** |
| `QG-04` | Ejecución del guion en el navegador del equipo anfitrión (`TC-35`) | **100 %** de los pasos del guion de la etapa **y de todas las anteriores** | 100 % | **Bloquea el punto de control.** No es condicionado: ver §2.3 |
| `QG-05` | `TC-29`, conteo en la pestaña de red | **0** peticiones del navegador hacia el servicio de datos, **con los dos movimientos automáticos prendidos** | 0 | **Bloqueante, sin gradación.** Es `RA-01` |
| `QG-06` | `TC-30`, inspección del árbol de fuentes y de las dependencias de guion | **1** sola salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta | 1 y 0 | **Bloqueante** |
| `QG-07` | `TC-03`, inspección del almacenamiento, de las marcas de sesión y del contenido servido | **0** apariciones de la credencial de sesión en el navegador | 0 | **Bloqueante.** Criterio de aceptación de la etapa `c` |
| `QG-08` | `TC-31`, sobre el traductor de condiciones | **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **quince** códigos vivos **y** sobre el camino de ausencia de respuesta | 0 | **Bloqueante.** Es `RA-03` |
| `QG-09` | `TC-32`, inspección del árbol de fuentes | **0** invocaciones al interior del bundle: las **6** funciones de la fachada son la única vía, y **0** accesos al elemento de dibujo fuera del anfitrión | 0, 6 y 0 | **Bloqueante.** Es `RA-02` sostenida desde este lado |
| `QG-10` | `TC-33`, conteo en la pestaña de red mientras se rota y se acerca | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja **una sola vez por trabajo** | 0 y 1 | **Bloqueante** |
| `QG-11` | Recorrido de la matriz al cerrar la etapa | Las filas de [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md) que la etapa toca, verificadas con estado y fecha, y **ninguna deriva mayor sin resolver** | 0 derivas mayores abiertas | **Bloquea el cierre de la etapa** |

**Tres gates corren dentro del flujo de publicación y ocho no.** `QG-01`, `QG-02` y `QG-03` son del flujo; los otros ocho son inspecciones y recorridos que corren en el pull request de la etapa o al cerrarla. Mezclarlos en una sola columna daría la impresión falsa de que una publicación verde equivale a una etapa cerrada, y no lo es: **la publicación verifica que la aplicación quedó en pie, no que hace lo que debe**.

**Una medición de ausencia hecha sin su condición no cuenta como medición.** `QG-05` se mide **con los dos movimientos automáticos prendidos**: un conteo hecho con los movimientos apagados daría cero sin haber ejercitado nunca el bucle de dibujo, que es donde una petición se colaría. Es el mismo criterio que [`../../GeometriaFactory-Visor/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Visor/09-Devops/Pipeline-CI-CD.md) §2.1 hace cumplir del otro lado de la fachada.

### 2.3 Ningún gate de este proyecto de código es condicionado

**Los once bloquean**, cada uno lo que su columna declara: la fusión, la publicación, el flujo, el punto de control o el cierre de la etapa. El único con valor rotulado **[ASUNCIÓN]** es `QG-04`, y **no por eso queda condicionado**.

No lo decide esta categoría: [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1 lo declara y da el fundamento, apoyado en dos textos que dicen lo mismo. El intake §17.6.P.6 escribe la puerta como **«gate bloqueante y numérico en lugar de cobertura de líneas»**, con el rótulo alcanzando a **expresarlo como gate**; y el intake §22, fila `A-4`, columna «Si el Product Owner la cambia», dice **«cambia la forma del gate, no su carácter bloqueante»**.

**La regla que esta ola aplica en todo el producto es la que la Fase E fijó**: una asunción **sobre el umbral mismo** condiciona; una asunción **sobre la forma del gate** no. Acá lo que está en duda es cómo se expresa la puerta, no si detiene, y **la regla acumulativa de no-regresión no es asunción de nadie**: la declara el intake §15 como regla de delivery. Esta categoría materializa `QG-04` como **bloqueante desde la primera etapa que lo alcanza**.

**Contra la lectura opuesta**, que sería condicionarlo por prudencia: este es el único proyecto de código del producto **sin batería automatizada propia**. Condicionar su única puerta acumulativa habría dejado al front sin ningún gate que detenga un punto de control, que es exactamente lo que la fuente puso a salvo.

### 2.4 Los stages del catálogo que no existen acá

| Stage del catálogo | Estado acá | Motivo |
| --- | --- | --- |
| Lint | **Incorporado en el paso 5** | El criterio es «construcción sin advertencias» (`QG-01`), y ninguna fuente declara un linter separado. La verificación de tipos del lenguaje del bundle ocurre en el paso 4 y su falla es falla de ese paso |
| Test | **No existe como stage con batería propia, y está declarado** | Intake §17.6.P.6: este proyecto de código **no tiene proyecto de pruebas propio** en el árbol del repositorio. Su verificación es el guion acumulativo (`QG-04`) más las inspecciones de `QG-05` a `QG-10`, y las pruebas de integración que ejercitan el servicio que consume, que **pertenecen a `GeometriaFactory-Api`** |
| SCA | **Existe, y acá sí tiene sujeto** | Es una de las **dos** unidades desplegables, y arrastra dos cadenas de dependencias: la de la plataforma —con la biblioteca de componentes de interfaz— y la del navegador, con el motor de dibujo dentro del bundle. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §4 |
| SBOM | **Existe como decisión de esta categoría** | Acá sí hay artefacto que sale del repositorio. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 |
| Firma | **No se firma, y la brecha se declara** | El canal de entrega es una subida por FTP a un hosting gratuito, sin mecanismo de verificación de firma por parte de quien recibe. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §2 |
| Publish | **Existe**, y es la subida más su verificación | Intake §17.6.P.7 y §17.6.P.8. Tiene documento propio: [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md) |

## 3. Triggers, y la resolución de `PD-01`

| Evento | Qué corre | Qué bloquea |
| --- | --- | --- |
| Confirmación empujada a la rama de una etapa | Los pasos 1 a 5 del flujo, sin publicar | Nada por sí solo |
| Apertura o actualización del pull request de la etapa | Lo anterior, más las inspecciones de `QG-05` a `QG-10` | **La fusión** |
| Cierre de la etapa | Lo anterior, más `QG-04` sobre el guion acumulativo y `QG-11` sobre las filas de la matriz de sensado que la etapa tocó | El punto de control y el cierre de la etapa |
| **Fusión a la rama principal con cambios bajo `src/GeometriaFactory.Web/` o `visor/`** | El flujo de publicación entero, los ocho pasos | La publicación |
| **Disparo manual** | El mismo flujo | La publicación |
| Etiqueta de cierre de etapa | El flujo sobre el estado etiquetado | La declaración de etapa cerrada |

**No hay trigger por calendario**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas».

Las dos filas del disparo del flujo son literalmente lo que el intake §17.6.P.7 declara: el flujo se dispara **manualmente y por fusión a la rama principal, restringido a cambios bajo `src/GeometriaFactory.Web/` y `visor/`**. Y ahí está el hallazgo que `GeometriaFactory-Contracts` elevó a esta categoría.

### 3.1 El hallazgo que Contracts elevó, leído entero

[`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §6 y §10 lo declaran así: el filtro de rutas de este flujo cubre `src/GeometriaFactory.Web/` y `visor/`, **deja fuera a `src/GeometriaFactory.Contracts/`**, y por eso un cambio de contrato **no dispara la publicación del front por fusión**, aunque el `QG-08` de aquel proyecto de código exija que las dos unidades desplegables salgan juntas ante un cambio incompatible. La regla no queda incumplida —el flujo también se dispara a mano—, pero **el despliegue conjunto queda apoyado en que alguien se acuerde de dispararlo**.

Esta categoría es la dueña del flujo y le corresponde resolverlo. Antes de decidir, hay **tres hechos** que hay que poner juntos, y los tres son verificables:

| Hecho | Dónde se verifica |
| --- | --- |
| El filtro cubre **dos** de las **tres** entradas de compilación de este proyecto de código. El intake §13 declara sus dependencias: `GeometriaFactory-Contracts` y `GeometriaFactory-Visor`; el filtro nombra `visor/` y el directorio propio, y **omite el del contrato** | Intake §13 y §17.6.P.7 |
| **El despliegue del backend no es automático en ningún caso.** El intake §17.5.P.8 declara el despliegue de la Api **manual, por el docente**, y que el agente entrega el archivo de construcción y el de composición **y no ejecuta el despliegue** | Intake §17.5.P.8 y §10 |
| El desfase que `QG-08` teme **ya es posible con el filtro actual, y en la dirección contraria**: una fusión que toca sólo `src/GeometriaFactory.Web/` publica el front automáticamente, mientras la Api espera una acción humana | Intake §17.6.P.7 contra §17.5.P.8 |

**El tercer hecho es el que cambia el problema.** El filtro de rutas no es la pieza que sostiene el despliegue conjunto, y no podría serlo: **con un extremo automático y el otro manual, el despliegue conjunto es siempre un acto humano coordinado**, y agregar una ruta al filtro no lo vuelve automático. Lo que el filtro sí determina es algo más simple y más grave: **si un cambio de contrato llega o no a estar construido y publicado en el front**.

### 3.2 Qué decide esta categoría

**Tres decisiones, y las tres van declaradas como derivadas de esta categoría y no como texto de la fuente.**

| # | Decisión | Fundamento | Estado |
| --- | --- | --- | --- |
| 1 | **El filtro de rutas del flujo incluye `src/GeometriaFactory.Contracts/`**, además de `src/GeometriaFactory.Web/` y `visor/` | Es una entrada de compilación de esta unidad (intake §13). Un cambio de contrato que no reconstruye el front deja publicada una aplicación compilada contra una versión anterior del contrato, que es `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 | **Decisión derivada.** Modifica lo que el intake §17.6.P.7 enumera, y por eso **se eleva al Product Owner** como `PD-01` de §10 |
| 2 | **`QG-08` de `GeometriaFactory-Contracts` sigue siendo el gate que sostiene el despliegue conjunto**, y esta categoría no lo reemplaza por el filtro | El filtro dispara una construcción; **no coordina dos despliegues**, y uno de los dos es manual por decisión del Product Owner | Adopción sin cambios |
| 3 | Ante un cambio **incompatible** del contrato, la publicación del front **no se declara cerrada** hasta que la unidad del servidor propio esté desplegada desde el mismo estado del repositorio, con constancia en el informe de cierre de la etapa | `QG-08` bloquea la **publicación de la etapa**, no la fusión ([`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §6, paso 2) | Materialización en [`Entornos-Deploy.md`](Entornos-Deploy.md) §6 |

**Por qué la primera decisión se eleva en lugar de darse por tomada.** El intake §17.6.P.7 enumera dos rutas y no tres. Agregar la tercera es lo correcto por el grafo de dependencias, pero **cambia lo que la fuente declara**, y la regla de esta cadena es que una decisión así se propone con su fundamento y la confirma el Product Owner sobre su propio documento. Mientras no la confirme, **rige el disparo manual y la constancia del paso 3**, que es lo que hoy sostiene la regla.

**Lo que esta categoría no hace.** No convierte el despliegue del backend en automático —lo prohíbe el intake §10 y §17.5.P.8—, no relaja `QG-08` y no declara resuelto el desfase de momentos: lo declara **irreducible mientras un extremo se despliegue a mano**, y por eso el mecanismo es la constancia escrita y no un disparador.

## 4. Matriz de plataformas

Este proyecto de código tiene **tres plataformas y no una**, y confundirlas sería el error característico acá:

| Momento | Plataforma | Fundamento |
| --- | --- | --- |
| **Construcción** | Las **dos** cadenas de herramientas, la de la plataforma y la del navegador, dentro del contenedor de desarrollo. El equipo anfitrión no las tiene instaladas | Intake §10 y encabezado de la Parte C; `05` §5, fila de etapas |
| **Ejecución del servidor** | El hosting gratuito, con servidor de información, transporte seguro y dominio público. **La versión de plataforma que soporta está [A VERIFICAR]** en la fuente: es `PT-01.a` | Intake §17.6.P.9 |
| **Ejecución del navegador** | Cualquiera con **capacidad gráfica tridimensional** y con conexión persistente o su repliegue. La fuente **no fija versiones mínimas** | Intake §17.6.P.9; `05` §5 |

**El requisito de navegador se declara por capacidad y no por versión**, porque la fuente no fija ninguna, y **toda combinación sin capacidad gráfica se considera no soportada** para el visor —el resto del producto sigue disponible (`05` §5)—. Esta categoría **no cierra ese hueco inventando una versión**.

**Y una consecuencia de la fila del medio que conviene no perder.** Si `PT-01.a` no pasa, la salida declarada por el intake §17.6.P.9 es **bajar la versión objetivo del front, no la del backend**, porque son dos artefactos independientes. `GeometriaFactory-Contracts` registró en su `PD-02` que una bajada así lo alcanza, porque su ensamblado se carga en los dos procesos: **esta canalización es el lugar donde esa bajada se ejecutaría**, y la restricción que hereda es que el ensamblado de contratos tiene que seguir siendo cargable por los dos.

## 5. Caché y artefactos

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Caché de la cadena de la plataforma | Caché del restaurador, con llave derivada de los archivos de proyecto | Decisión de esta categoría |
| Caché de la cadena del navegador | Caché del gestor de paquetes, con llave derivada del archivo de bloqueo de `visor/` | Decisión de esta categoría |
| Invalidación | Al cambiar el archivo de proyecto o el de bloqueo. **Sin expiración por tiempo** | Un plazo en días no lo da ninguna fuente |
| **Prohibición explícita de caché** | **El bundle no se cachea entre ejecuciones.** Se regenera en cada flujo | `QG-02`: nunca se toma de un artefacto viejo. Una caché del bundle sería exactamente el artefacto viejo que el gate prohíbe |
| Artefacto del paso 4 | El bundle, copiado a `src/GeometriaFactory.Web/wwwroot/js/` | Intake §13 y §16 |
| Artefacto del paso 5 | La salida publicable del front, con los tipos del contrato compilados adentro | `05` §5, fila de qué viaja adentro |
| Artefacto del paso 8 | El **registro de la respuesta** de la dirección pública | `QG-03` |
| Artefactos de inspección | Los **recuentos** de `QG-05` a `QG-10`, y el estado y la fecha de las filas de la matriz de sensado que la etapa tocó | `Estrategia-Calidad.md` §3; `Plan-Pruebas.md` §3 |
| Inventario de componentes | Emitido en el flujo de publicación, sobre las **dos** cadenas. Ver [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) §1 | Decisión de esta categoría |
| Retención | Mientras dure el punto de control; los registros se adjuntan al informe de cierre | Intake §15, regla de delivery 3 |

**La cuarta fila es la única prohibición de caché del producto, y no es una precaución de esta categoría**: es la consecuencia directa de `QG-02`. Un flujo que reutilizara el bundle de una ejecución anterior estaría publicando un artefacto que no se generó en ese flujo, que es literalmente lo que el gate mide.

## 6. Promoción

| Transición | Trigger | Prerrequisitos | Aprobador |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Los gates bloqueantes de §2.2 en verde, y los **once** criterios de salida de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §3 | El Product Owner, con **OK explícito** en el punto de control (intake §15) |
| Rama principal → publicación en el hosting | El flujo de §2.1, entero | `QG-01`, `QG-02` y `QG-03`, y la Definition of Done §1.4 en sus **seis** puntos | El mismo, con el registro del flujo |
| **Publicación → etapa cerrada, ante un cambio incompatible del contrato** | La constancia del despliegue conjunto | La unidad del servidor propio desplegada **desde el mismo estado del repositorio** | El mismo, con constancia escrita. Ver §3.2, decisión 3 |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | La Definition of Done §1.3 entera, y `QG-04` y `QG-11` en verde | El mismo |

**La publicación se hace fuera del horario de uso**, y no es una recomendación de esta categoría: el intake §17.6.P.8 lo declara como el tratamiento de una subida que **no es transaccional**, y la Definition of Done §1.4 lo exige con la hora registrada del flujo.

## 7. Reversión

| Situación | Procedimiento | Fundamento |
| --- | --- | --- |
| La publicación dejó la aplicación caída | **Volver a publicar desde la etiqueta anterior.** El flujo entero corre de nuevo, incluido el paso 4, de modo que el bundle también se regenera | Intake §17.6.P.8; `05` §5, fila de reversión; Definition of Done §1.4 |
| El bundle publicado rompe la visualización | El mismo procedimiento: la reversión efectiva del bundle **es la del front**, y su regeneración es parte de esa publicación | [`../../GeometriaFactory-Visor/09-Devops/Guia-Publicacion-Bundle-Visor.md`](../../GeometriaFactory-Visor/09-Devops/Guia-Publicacion-Bundle-Visor.md) §4, segunda fila |
| Un cambio incompatible del contrato llegó a las dos unidades y hay que volver atrás | **Se revierten las dos juntas**, por la misma regla que las obliga a desplegarse juntas | [`../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md`](../../GeometriaFactory-Contracts/09-Devops/Pipeline-CI-CD.md) §7, segunda fila; `RI-02` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 |
| La dirección del servicio de datos cambió y el front dejó de alcanzarlo | **No se revierte nada**: se actualiza el secreto y se vuelve a publicar. Ver [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 | [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §5, consecuencia positiva 2 |

**No hay delist ni retiro de versión publicada**, porque no hay repositorio de paquetes del que retirarla. **Y no hay despliegue con solapamiento**: el canal es una subida sobre el mismo destino, de modo que la reversión es otra publicación y no un cambio de tráfico.

## 8. Notificaciones

| Canal | Qué comunica |
| --- | --- |
| La salida del flujo de publicación | El resultado de los ocho pasos, con el **registro de la respuesta** de la dirección pública y la hora de la publicación |
| La salida de las inspecciones sobre el pull request de la etapa | Los **recuentos** de `QG-05` a `QG-10`, cada uno con la condición en que se midió |
| El informe de cierre de la etapa | Lo anterior, más el resultado del guion acumulativo (`QG-04`), el estado de las filas de la matriz de sensado que la etapa tocó, **la decisión tomada ante toda deriva mayor** y, cuando hubo cambio incompatible del contrato, la constancia del despliegue conjunto |
| El registro de cambios del producto | Toda fila de cambio mayor de esta unidad |

**No se declara ningún canal de mensajería ni ningún tablero**: ninguna fuente lo declara y `equipo_n` es 1.

**Lo que sí se comunica a la persona que usa el producto, y no es una notificación de esta categoría**, es el **estado degradado** cuando el servicio de datos no responde: es una superficie declarada del front ([`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Estado-Degradado-Como-Superficie.md)), y **nunca incluye la dirección del servicio interno** (`QG-08`, `RA-03`).

## 9. Las tres puertas técnicas dentro de la canalización

`PT-01`, `PT-02` y `PT-03` **no son criterios de esta categoría ni de la 08**: las declara el intake §15, y `Estrategia-Calidad.md` §3.2 las transcribe con la constancia de que **sus umbrales no son asunciones**. Lo que le corresponde a 09 es declarar cómo se ejecutan dentro de la canalización:

| Puerta | Cuándo corre | Sobre qué | Qué pasa si no pasa |
| --- | --- | --- | --- |
| `PT-01`, en sus **cuatro** partes | Etapa `a`, **antes que cualquier otra cosa** | Sobre el front **ya publicado** en el hosting, no sobre una ejecución local: `PT-01.a` mide la dirección pública y `PT-01.c` mide **20 minutos** de navegación continua contra el proceso del hosting | **Detiene la planificación** de lo que dependa de ella. Sólo el rojo en el transporte o la falla de estabilidad obligan a cambiar el modelo de front; el repliegue de mayor latencia **no es motivo de rediseño** |
| `PT-02` | Antes de comprometer la etapa `g` | Sobre el bundle generado, cargado en una página del anfitrión | La etapa `g` **no se compromete** |
| `PT-03` | Antes de comprometer la etapa `g` | Sobre el bundle. **No tiene caso de verificación propio acá**: es propiedad del bundle y se verifica del lado de `GeometriaFactory-Visor` | La etapa `g` **no se compromete** |

**La primera fila tiene una consecuencia sobre el orden de la canalización que ninguna otra puerta del producto tiene**: `PT-01` **exige que el flujo de publicación exista y haya corrido** antes de que se pueda medir. Es la única puerta técnica del producto que se mide sobre un artefacto ya desplegado, y por eso `BT-01` —crear el proyecto del front **con su flujo de publicación**— y `BT-06` —la puerta de publicación que termina comprobando— son de la etapa `a` y preceden a `BT-04`, que es la medición.

**`PT-01.c` es el peor escenario declarado y no tiene mitigación en el código** (`R-06`). Esta categoría lo registra y **no le inventa una**: un reintento, un calentamiento periódico o un segundo proceso serían infraestructura que ninguna fuente declara y que el intake §10 no financia.

## 10. Puntos abiertos

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PD-01 | **La inclusión de `src/GeometriaFactory.Contracts/` en el filtro de rutas del flujo de publicación** (§3.2, decisión 1). Es la resolución que esta categoría propone al hallazgo que `GeometriaFactory-Contracts` elevó, y **cambia lo que el intake §17.6.P.7 enumera**, de modo que se eleva en lugar de darse por tomada | El Product Owner, sobre el intake §17.6.P.7 | Antes de la primera etapa que cambie el contrato después de la `a` |
| PD-02 | La **exclusión en el control de versiones** del directorio de salida del empaquetado y del bundle copiado bajo los recursos estáticos. Es la acción pendiente que [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2.2 declaró con su fecha de lectura, y su tramo de este lado lo resuelve [`Entornos-Deploy.md`](Entornos-Deploy.md) §2 | El equipo, con `BT-01` | Etapa `a` |
| PD-03 | La **herramienta concreta** de cada paso del flujo —cadenas de herramientas, empaquetador, cliente de subida y generador del inventario— y su anclaje de versión, incluida la **versión de la biblioteca de componentes de interfaz**, que la fuente deja **[A VERIFICAR]** (`PA-01` de `05` §11) | El equipo, en el punto de control de la etapa `a`, con `BT-02` | Etapa `a` |
| PD-04 | Que el ejecutor del flujo provea **navegador con capacidad gráfica tridimensional y conductor capaz de contar peticiones y leer el almacenamiento**. Sin eso, `QG-05`, `QG-07` y `QG-10` no se pueden medir en la canalización y quedarían como medición manual registrada | El equipo, antes de la etapa `c`, que es donde entra `QG-07` | Antes de la etapa `c` |
| PD-05 | La **versión de plataforma que soporta el hosting**, **[A VERIFICAR]** en la fuente (`PA-02` de `05` §11). Es `PT-01.a`, **se mide y no se decide**, y su desenlace alcanza al ensamblado de contratos | La medición de `PT-01.a`, con `BT-04` | Etapa `a` |

**`PA-07` de `05` §11 queda cerrado por esta categoría.** Preguntaba si el bundle generado se versiona en el repositorio o se ignora, y lo derivaba a 09; la decisión ya está tomada en [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2 —**se ignora y lo genera la canalización**— y esta categoría la adopta desde el lado del anfitrión, con su consecuencia operativa en [`Entornos-Deploy.md`](Entornos-Deploy.md) §2. **`PA-04` —el umbral numérico de tiempo de respuesta— sigue abierto y esta categoría no lo cierra**: inventar un número acá lo propagaría como si fuera del producto.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **ocho** pasos del flujo de publicación que el intake §17.6.P.8 fija, con la constancia de que `05` §5 enumera el mismo conjunto en siete, y los **once** quality gates de `08` §3 materializados uno por uno, **ninguno condicionado**, con el fundamento de que la única marca [ASUNCIÓN] que alcanza a `QG-04` es sobre la **forma** de la puerta y no sobre la regla acumulativa. Declara que **tres gates corren dentro del flujo y ocho no**, y que una publicación verde no equivale a una etapa cerrada. **Resuelve el `PD-01` que `GeometriaFactory-Contracts` elevó** con tres decisiones derivadas: incluir el directorio del contrato en el filtro de rutas —elevado al Product Owner por cambiar lo que la fuente enumera—, sostener el despliegue conjunto en `QG-08` y no en el filtro, y exigir la constancia del despliegue conjunto antes de cerrar la etapa; con el hallazgo propio de que **el desfase de momentos es irreducible mientras un extremo se despliegue a mano**. Declara las **tres** plataformas, la **prohibición explícita de cachear el bundle**, la reversión por republicación desde la etiqueta anterior, y las **tres** puertas técnicas dentro de la canalización, con la precisión de que `PT-01` es la única del producto que se mide sobre un artefacto ya desplegado. Cierra `PA-07` de `05` §11 y declara **cinco** puntos abiertos. |
