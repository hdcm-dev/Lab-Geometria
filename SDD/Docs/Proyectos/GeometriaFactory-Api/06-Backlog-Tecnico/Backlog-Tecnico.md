# Backlog técnico — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Backlog-Tecnico.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Tipo de proyecto de código (D8):** `rest-api`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §2.2 (las **siete** decisiones que hereda de los cuatro proyectos de código que ensambla), §3.1 (los **ocho** componentes), §3.4 (los **quince** puntos de acceso contra la guardia), §4 (arranque en dos fases), §5 (etapas del pipeline y puertas), §7 (cross-cutting), §8 (los **diecisiete** NFR), §9 (los **nueve** riesgos) y §11 (sus diez filas); las **ocho** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md); [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) 1.3; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §14, §15, §16.1, §17.5, §18 y §20
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones, composición de raíz y arranque](#21-ep-t01--fundaciones-composición-de-raíz-y-arranque)
  - [2.2 EP-T02 · Superficie y formato de intercambio](#22-ep-t02--superficie-y-formato-de-intercambio)
  - [2.3 EP-T03 · Guardia y traducción](#23-ep-t03--guardia-y-traducción)
  - [2.4 EP-T04 · Las cuatro superficies de acceso](#24-ep-t04--las-cuatro-superficies-de-acceso)
  - [2.5 EP-T05 · Verificación, muestras y despliegue](#25-ep-t05--verificación-muestras-y-despliegue)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **veintiséis** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de `05` §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11, de un punto de acceso de la superficie de 02 o de una regla de delivery del intake §15. **Ocho** convierten en trabajo un punto abierto: BT-05, BT-07, BT-09, BT-10, BT-15, BT-21, BT-25 y BT-26.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Dos tareas son inspecciones en las dos direcciones y no funcionalidades.** BT-12 recorre los **quince** puntos de acceso contra la lista de la guardia, y BT-13 recorre el conjunto cerrado de **quince** códigos contra la tabla de traducción. Existen porque **el defecto característico de esta capa es de omisión**: un punto sin guardia o un código sin destino **no se ven leyendo el punto nuevo**, se ven comparando contra una lista.
2. **Una tarea fija una decisión que obliga a otro proyecto de código.** BT-08 fija el **formato de intercambio para los dos extremos**, porque el ensamblado de contratos no lo impone y `GeometriaFactory-Web` declaró que **no lo decide de un solo lado** y que lo adopta. La coincidencia **se verifica ejerciendo el servicio real**, no comparando dos archivos de configuración.
3. **La pirámide de pruebas de este proyecto de código está invertida a propósito**: **60 % de integración y 40 % unitarias**, porque lo que esta capa aporta es cableado. BT-22 es esa batería, y **golpea el servicio real contra el almacén real**.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones, composición de raíz y arranque

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el servicio exista, que **los cuatro puertos queden conectados con sus cuatro adaptadores en un solo lugar**, que el arranque prepare el almacén antes de atender y **se detenga antes que atender mal**, y que la imagen se construya y responda salud |
| Alcance | Proyecto y batería de integración, composición de raíz, arranque en dos fases, imagen multietapa, anclajes y la puerta de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`, puerta `PT-04`), §16, §17.5.P.7 a P.9; `05` §3.1, §4, §5, §11 `PA-07`; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md), [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) |
| Etapa | `a` |
| BT contenidas | BT-01, BT-02, BT-03, BT-04, BT-05, BT-06 |

### 2.2 EP-T02 · Superficie y formato de intercambio

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las rutas y los verbos de los **quince** puntos queden fijados en el punto de control, y que el **formato de intercambio quede declarado una sola vez para los dos extremos**, con el límite de cuerpo que **rechaza y nunca trunca** |
| Alcance | Rutas y verbos, formato de intercambio, límite de cuerpo y vigencia del acceso |
| Fuente upstream | `05` §3.4, §7 filas de formato y de configuración, §11 `PA-01`, `PA-04` y `PA-05`; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 |
| Etapa | `a` |
| BT contenidas | BT-07, BT-08, BT-09, BT-10 |

### 2.3 EP-T03 · Guardia y traducción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que **ningún punto de acceso quede fuera de la guardia** salvo los cuatro declarados, y que las **dos** traducciones ocurran en una tabla única **sin inventar códigos**, con las **tres** familias deliberadamente empobrecidas verificadas |
| Alcance | Guardia de admisión, traductor, inspecciones en las dos direcciones y los dos huecos del conjunto cerrado |
| Fuente upstream | `05` §3.1 (los dos componentes transversales), §7 filas de autorización, guardia y manejo de errores, §8 filas de puntos fuera de la guardia, de códigos con traducción y de respuestas indistinguibles, §9 riesgos primero y segundo; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) |
| Etapa | `c` |
| BT contenidas | BT-11, BT-12, BT-13, BT-14, BT-15 |

### 2.4 EP-T04 · Las cuatro superficies de acceso

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **quince** puntos vivan repartidos en las **cuatro** superficies que `05` §3.1 declara, sin que ninguna dependa de otra |
| Alcance | Acceso y credencial propia, gobierno de la comisión, trabajos y desenlace |
| Fuente upstream | `05` §3.1 y §3.4; `05` §3.2 punto 1 (ninguna superficie depende de otra); [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) |
| Etapa | `c` a `h`, según la historia que la consuma |
| BT contenidas | BT-16, BT-17, BT-18, BT-19 |

### 2.5 EP-T05 · Verificación, muestras y despliegue

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la batería de integración ejercite el cableado contra el servicio real, que la colección de peticiones se reproduzca **en cinco pasos o menos y sin datos inventados**, y que los valores rotulados como asunción y el mecanismo de despliegue queden elevados con su plazo |
| Alcance | Colección reproducible, batería de integración, las dos pruebas de criterio propio, y los tres puntos abiertos del Product Owner y de 09 |
| Fuente upstream | `05` §8 filas de la pirámide, de eliminaciones forzadas, de textos alterados y de pasos de la colección; `05` §11 `PA-06`, `PA-08` y `PA-09`; `PRODUCT-INTAKE` §16.1, §17.5.P.6 y §18 |
| Etapa | `e` a `h`, y las elevaciones antes del punto de control de su etapa |
| BT contenidas | BT-20, BT-21, BT-22, BT-23, BT-24, BT-25, BT-26 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-01 | Crear el proyecto de código y su proyecto de pruebas de integración | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.5.P.1; `05` §5 | Ninguna | El proyecto de código compila dentro del artefacto de agrupación con sus **tres** dependencias de compilación; **el proyecto de pruebas de integración existe acá y es el que golpea el servicio real**, incluido el de las capas de adentro que no pueden tocar la base | **Infraestructura compartida**: habilita a las 30 |
| BT-02 | Construir la composición de raíz con los cuatro puertos y sus adaptadores | feature | EP-T01 | `a` | Alta | Sin fijar | `05` §3.1, componente «Composición de raíz»; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md); `05` §9, séptimo riesgo | BT-01 | **4 de 4** puertos conectados a su adaptador, y **0** puertos sin adaptador o con más de uno; la composición es **única** y no se reparte en módulos por área, porque **la frontera tiene que ser contable en un solo lugar**; si falta una dependencia, **falla en construcción** y no hay petición que responder; toda la configuración del despliegue entra **por acá y por ningún otro lado** | US-26 |
| BT-03 | Construir el arranque en dos fases con el punto de salud sin acceso | feature | EP-T01 | `a` | Alta | Sin fijar | `05` §4, quinta viñeta; [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md); `PRODUCT-INTAKE` §17.5.P.4 | BT-02 | Primero se construye el grafo —si falla, falla en construcción—, después se **dispara** la preparación del almacén —si falla, **el arranque se detiene**— y **recién entonces el servicio escucha**; **0** peticiones atendidas con la preparación incompleta; el punto de salud **no exige acceso**, porque tiene que poder responder cuando nadie puede autenticarse | US-27, US-28, US-29 |
| BT-04 | Construir la imagen multietapa y medir `PT-04` | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.5.P.8; `05` §5, etapas del pipeline y contenido de la imagen | BT-03 | La imagen se construye con el archivo de construcción **multietapa**, lleva **sólo el entorno de ejecución** —sin kit de desarrollo ni depurador— y **no tiene linaje con la imagen del contenedor de desarrollo**; arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. **Una puerta que no pasa detiene la planificación de las etapas que dependen de ella** | **Infraestructura compartida**: es `PT-04`, puerta del producto |
| BT-05 | Anclar nombres de tipos, espacios de nombres y versiones de paquetes | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-07`; `PRODUCT-INTAKE` §17.5.P.11, que declara la versión de los paquetes abierta y anclada en la primera etapa | BT-01 | Los nombres y las versiones quedan decididos y anclados según la regla de anclaje del producto, y registrados. **Caja temporal: la etapa `a`** | **Infraestructura compartida** |
| BT-06 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, última fila; `PRODUCT-INTAKE` §17.5.P.8 | BT-01 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-07 | Fijar las rutas y los verbos de los quince puntos de acceso en el punto de control | indagación | EP-T02 | `a` | Alta | Sin fijar | `05` §3.4 y §11 `PA-01`; [`Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 | BT-01 | Las rutas y los verbos quedan validados en el punto de control de la etapa `a`. Las **dos** únicas cosas que una fuente declara son el punto de canje, con su ruta, y la **existencia** de un punto de salud, cuya ruta la fuente **no da**; las quince filas son **propuesta derivada rotulada fila por fila** y esta tarea las confirma o las corrige. **`A-04` queda retirado y no se recicla.** **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los quince puntos dependen de ella |
| BT-08 | Fijar el formato de intercambio para los dos extremos | feature | EP-T02 | `a` | Alta | Sin fijar | [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md); `05` §2.2, quinta y sexta decisión heredada; `05` §9, cuarto riesgo | BT-02 | **Exactamente 1** configuración de intercambio declarada en el producto, en la **composición de raíz** y en ningún otro lado; campos con **nombre literal**, valores de conjunto cerrado **por su nombre y nunca por su posición**, campos nulos **emitidos**, números **sin cultura** y **lectura estricta**; **ningún punto de acceso configura la serialización por su cuenta**. La coincidencia con el otro extremo **se verifica ejerciendo el servicio real** y no comparando dos archivos. **Esta decisión obliga a `GeometriaFactory-Web`, que declaró que la adopta** | US-19, US-22, US-24 |
| BT-09 | Fijar el límite de tamaño de cuerpo que rechaza y nunca trunca | indagación | EP-T02 | `a` | Alta | Sin fijar | [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6; `05` §11 `PA-05`; [`Infrastructure ADR-06`](../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3 | BT-08 | **Un solo** límite para todo el producto, tomado de configuración; el cuerpo que lo excede **se rechaza y nunca se trunca**, y **la forma de rechazo no es configurable**; el número se calibra **sobre el texto más grande que la fuente documenta**. Es el hueco que `GeometriaFactory-Infrastructure` **reasignó acá** porque el corte pertenece al borde del proceso. **Caja temporal: la etapa `a`** | US-19 |
| BT-10 | Fijar la vigencia del acceso firmado | indagación | EP-T02 | `a` | Media | Sin fijar | `05` §11 `PA-04`; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `PRODUCT-INTAKE` §17.5.P.5, que declara «corta» y **sin acceso de refresco** | BT-02 | El número queda tomado **de configuración** aplicando el criterio ya fijado: que caduque **dentro de la sesión de trabajo de una clase** y que **la renovación sea reingreso**. **Ninguna fuente da el número**, y esta tarea no lo inventa: lo elige aplicando el criterio y lo registra. **Caja temporal: la etapa `a`** | US-01, US-04 |
| BT-11 | Construir la guardia de admisión transversal | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guardia de admisión»; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md); `05` §7, filas de autenticación, autorización y guardia | BT-02, BT-07 | Verifica **firma y expiración** del acceso, exige el **papel** que cada punto declara y aplica la **guardia del cambio de contraseña pendiente**; es transversal a los **once** puntos que exigen acceso; **exigir el papel no es autorizar**: la verificación de pertenencia y de facultad se hace sobre el dato recuperado y es de la capa de aplicación, y **duplicarla acá crearía un segundo lugar donde la regla puede decir otra cosa** | US-04, US-05, US-06 |
| BT-12 | Puerta de inspección de los quince puntos contra la guardia, en las dos direcciones | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §8, fila de puntos fuera de la guardia; `05` §9, primer riesgo; `RN-13`, `INV-09` | BT-11 | Exactamente **4** puntos fuera de la guardia, **ni uno más**, y son los declarados: canje, registro, configuración del administrador y salud; la inspección **recorre los quince y compara contra la lista en las dos direcciones**; y **0** puntos que fijen una contraseña sobre una cuenta existente sin credencial. **Se mide en cada etapa que agregue un punto**, no sólo en la que la introdujo | **Infraestructura compartida**: es el defecto de omisión más caro de esta capa. Un punto nuevo fuera de la guardia rompe `RN-13` **sin que nada falle** |
| BT-13 | Construir el traductor con la tabla única, sin códigos inventados | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Traductor de motivos y códigos»; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md); `05` §8, fila de códigos con traducción; [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §5 | BT-02 | Las **dos** traducciones en ese orden: motivo interno a código del contrato, y código del contrato a código de respuesta; **14 de 15** códigos con traducción declarada y **1** declarado **sin destino con su motivo**; **0** códigos inventados y **0** renombrados; la inspección recorre el conjunto cerrado contra la tabla **en las dos direcciones**; **ningún camino de fallo sale sin pasar por acá** | US-24, US-25 |
| BT-14 | Prueba de las tres familias deliberadamente empobrecidas | devops | EP-T03 | `c` | Alta | Sin fijar | `05` §7, fila de familias empobrecidas; `05` §8, fila de respuestas indistinguibles; `05` §9, segundo riesgo | BT-13 | **3 de 3** comparaciones dan **idénticas, cuerpo y código**: trabajo ajeno contra inexistente, correo inválido contra contraseña inválida, y correo ocupado por cuenta habilitada contra ocupado por cuenta bloqueada. **En las tres es la decisión y no el defecto**, y la primera es la que rompe `RN-03` hacia afuera **sin que ninguna capa de adentro se entere** | US-02, US-20, US-21 |
| BT-15 | Elevar los dos huecos del conjunto cerrado de códigos | indagación | EP-T03 | `c` | Media | Sin fijar | `05` §11 `PA-02` y `PA-03`; `02` §11 | BT-13 | Quedan registrados los **dos** huecos: qué código recibe **una operación de administrador pedida por quien no lo es fuera del desenlace**, y qué código recibe **un envío o una reedición forzados fuera de `Borrador`**. En los dos casos se usa el **genérico** con su respuesta correspondiente y **se declara el hueco**; **esta categoría no inventa un código**, porque los códigos son del ensamblado de contratos. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner y de `GeometriaFactory-Contracts` |
| BT-16 | Construir la superficie de acceso y credencial propia | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4 | BT-07, BT-11, BT-13 | Los **cuatro** puntos que se ejercen **sin acceso firmado o sobre la propia cuenta**: canje, registro de cuenta, configuración del administrador y cambio de la propia contraseña. **El registro es anónimo por diseño y así debe seguir**; el cambio de la propia contraseña es **la única excepción de la guardia del cambio pendiente**; **ninguno de los cuatro que no exigen acceso fija una contraseña sobre una cuenta existente** | US-01, US-02, US-03, US-07, US-08, US-09, US-10 |
| BT-17 | Construir la superficie de gobierno de la comisión | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §10.2, filas de RN-07, RN-12, RN-15 y RN-16 | BT-11, BT-13, BT-16 | Los **cuatro** puntos del administrador sobre cuentas ajenas: listado, cambio de situación, baja **transportando el correo escrito** y reseteo; **el cambio de situación devuelve la provisoria** en su resultado y **el reseteo la devuelve una sola vez**; el punto de reseteo **no declara ningún parámetro de situación** y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe; **el reseteo no toca ninguna ruta de retiro** | US-11, US-12, US-13, US-14, US-15, US-16 |
| BT-18 | Construir la superficie de trabajos | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §6, las dos decisiones de frontera | BT-08, BT-11, BT-13 | Los **cinco** puntos sobre trabajos: envío, reenvío, eliminación con sus **dos** alcances, listado y detalle; **el texto original no se normaliza en el borde**; el listado **no arrastra el texto ni los componentes** y esta capa **no recompone la proyección**; **la superficie no declara ningún parámetro con el que se puedan pedir borradores ajenos** | US-17, US-18, US-19, US-20, US-21, US-22 |
| BT-19 | Construir la superficie de desenlace | feature | EP-T04 | `h` | Alta | Sin fijar | `05` §3.1, componente correspondiente, y §3.4; `05` §10.3 `INV-07` | BT-11, BT-13 | El punto de aprobar o rechazar **desde el estado `Pendiente`**, con comentario opcional; la traducción del estado que no admite desenlace **incluido el terminal**, y **sin sugerir ninguna forma de revertirlo** | US-23 |
| BT-20 | Construir la colección de peticiones reproducible | docs | EP-T05 | `h` | Media | Sin fijar | `PRODUCT-INTAKE` §16.1 y §18 `S-2`; `05` §8, última fila; [`ADR-08`](../05-Arquitectura-Tecnica/Adrs/ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | BT-16, BT-17, BT-18, BT-19 | Se reproduce en **5 pasos o menos**, con **0** datos de prueba inventados; los cuerpos son los escenarios del intake §20; incluye alta de trabajo, envío con texto que verifica y que no verifica, y **aprobación y rechazo por el administrador**, que es lo que la ubica en la etapa `h`; **no implementa nada: demuestra**, y vive en el árbol de muestras del repositorio | US-30 |
| BT-21 | Elevar el alcance de la colección de peticiones | indagación | EP-T05 | `h` | Media | Sin fijar | `05` §11 `PA-06`; `02` §11 | BT-20 | Queda registrado que la fuente declara el alcance en **dos lugares con alcances distintos** —§16.1 con los **ocho** escenarios y §18 `S-2` con **dos**—, que **los dos textos están al día** y que **la fuente no declara cuál manda**. La categoría 02 adopta los **ocho** con su fundamento y esta tarea **hereda esa lectura y no la reabre**: eleva para que el Product Owner declare cuál rige y alinee los dos lugares. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-22 | Construir la batería de integración con la pirámide invertida | devops | EP-T05 | `c` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.5.P.6; `05` §8, filas de cobertura y de forma de la pirámide | BT-01, BT-03 | La batería **golpea el servicio real por su superficie contra el almacén real**; la forma declarada es **60 %** de integración y **40 %** unitarias, **invertida a propósito porque lo que esta capa aporta es cableado y el cableado se verifica ejerciéndolo**; cubre además el contrato con el ensamblado de tipos **de extremo a extremo**. Los dos porcentajes vienen **rotulados como asunción** y se usan como vigentes | **Infraestructura compartida**: es la verificación de esta capa y también la de las capas de adentro que no pueden tocar la base |
| BT-23 | Prueba de eliminación forzada contra la superficie, en sus dos alcances | devops | EP-T05 | `e` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.5.P.6, criterio bloqueante tomado de la fuente; `05` §8, fila correspondiente; `Roadmap-Producto.md` §5.2, transición `e` → `f` | BT-18, BT-22 | **0** eliminaciones fuera de alcance aceptadas al **forzar la petición**: un trabajo que no está en `Borrador` y uno que no pertenece al solicitante. **Es el único criterio de verificación del producto que la fuente exige ejercer forzando la petición contra esta superficie**, y no sólo por la interfaz | US-20 |
| BT-24 | Prueba del texto original byte a byte y del rechazo sin truncamiento | devops | EP-T05 | `e` | Alta | Sin fijar | `05` §8, fila de textos alterados; `05` §9, tercer riesgo; `RN-08` | BT-09, BT-18, BT-22 | **0** caracteres de diferencia entre lo enviado y lo guardado, comparado **byte a byte** con el texto de `E-1`; y **0** truncamientos silenciosos: un cuerpo por encima del límite **se rechaza y no se trunca**. **Truncar rompe `RN-08` en silencio**, con el trabajo guardado y el texto mutilado, y el alumno lo descubre al ver el dibujo | US-19 |
| BT-25 | Confirmar los cinco valores rotulados como asunción | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, cinco primeras filas; `05` §11 `PA-09`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-22 | El Product Owner confirma o corrige **latencia, caudal, arranque en frío, cobertura y la forma de la pirámide** sobre su propio documento; hasta entonces se usan como vigentes y la puerta de cobertura **no se declara bloqueante** en 09. **Es la mayor concentración de valores sin confirmar de los siete proyectos de código**, y ninguna salida consiste en inventar un número acá. **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida** |
| BT-26 | Probar una vez la construcción de la imagen en destino desde el repositorio | indagación | EP-T05 | `h` | Media | Sin fijar | `05` §11 `PA-08`; `PRODUCT-INTAKE` §17.5.P.11 punto 5, rotulado **[A VERIFICAR]** | BT-04 | El mecanismo queda **probado una vez antes de depender de él**, tal como el intake exige: el motor de contenedores del destino resuelve la referencia al repositorio y tiene credenciales si es privado. **No es una asunción de esta categoría** y **la decisión de medirlo es de `09-Devops`**; esta tarea la eleva con su plazo. **Caja temporal: antes de la etapa de despliegue real** | **Infraestructura compartida**: es el único canal de entrega declarado |

**Once tareas se justifican como infraestructura compartida** —BT-01, BT-04, BT-05, BT-06, BT-07, BT-12, BT-15, BT-21, BT-22, BT-25 y BT-26— y las **quince** restantes declaran al menos una historia consumidora —BT-02, BT-03, BT-08, BT-09, BT-10, BT-11, BT-13, BT-14, BT-16, BT-17, BT-18, BT-19, BT-20, BT-23 y BT-24—. **Once más quince son veintiséis**, y ninguna queda sin una cosa ni la otra.

## 4. Trazabilidad BT ↔ US ↔ CU

Las veintiséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5 y los puntos de acceso los de `05` §3.4.

| BT | US que la consumen | CU upstream | Puntos de acceso que toca | Fuente de arquitectura |
| --- | --- | --- | --- | --- |
| BT-01 | Infraestructura compartida (habilita a las 30) | CU-01 a CU-12 | Los quince | `05` §5 |
| BT-02 | US-26 | CU-10 | Ninguno: construye el grafo y desaparece | ADR-06 |
| BT-03 | US-27, US-28, US-29 | CU-11 | A-16 | ADR-07 |
| BT-04 | Infraestructura compartida | CU-11 | A-16 | `05` §5, `PT-04` |
| BT-05 | Infraestructura compartida | CU-01 a CU-12 | Los quince | `05` §11 `PA-07` |
| BT-06 | Infraestructura compartida | — (puerta de construcción) | Ninguno | `05` §8 |
| BT-07 | Infraestructura compartida | CU-01, CU-03 a CU-08, CU-11 | Los quince | `05` §11 `PA-01` |
| BT-08 | US-19, US-22, US-24 | CU-06, CU-07, CU-09 | Los quince | ADR-02 |
| BT-09 | US-19 | CU-06 | A-10, A-11 | ADR-02 §2 punto 6 |
| BT-10 | US-01, US-04 | CU-01, CU-02 | A-01, y los once bajo la guardia | ADR-03 |
| BT-11 | US-04, US-05, US-06 | CU-02 | Los **once** bajo la guardia | ADR-03 |
| BT-12 | Infraestructura compartida | CU-02 | Los quince, en las dos direcciones | `05` §8, ADR-03 |
| BT-13 | US-24, US-25 | CU-09 | Los quince | ADR-04 |
| BT-14 | US-02, US-20, US-21 | CU-01, CU-06, CU-07, CU-09 | A-01, A-02, A-12, A-13, A-14 | ADR-04 |
| BT-15 | Infraestructura compartida | CU-09 | A-06, A-07, A-08, A-09, A-10, A-11, A-13, A-14 | `05` §11 `PA-02` y `PA-03` |
| BT-16 | US-01, US-02, US-03, US-07, US-08, US-09, US-10 | CU-01, CU-03 | A-01, A-02, A-03, A-05 | `05` §3.1, superficie de acceso |
| BT-17 | US-11, US-12, US-13, US-14, US-15, US-16 | CU-04, CU-05 | A-06, A-07, A-08, A-09 | `05` §3.1, superficie de gobierno |
| BT-18 | US-17, US-18, US-19, US-20, US-21, US-22 | CU-06, CU-07 | A-10, A-11, A-12, A-13, A-14 | `05` §3.1, superficie de trabajos |
| BT-19 | US-23 | CU-08 | A-15 | `05` §3.1, superficie de desenlace |
| BT-20 | US-30 | CU-12 | Los quince, por ejercicio | ADR-08, `PRODUCT-INTAKE` §16.1 |
| BT-21 | Infraestructura compartida | CU-12 | Ninguno | `05` §11 `PA-06` |
| BT-22 | Infraestructura compartida | CU-01 a CU-11 | Los quince | `05` §8, ADR-01 |
| BT-23 | US-20 | CU-06 | A-12 | `05` §8, criterio bloqueante de la fuente |
| BT-24 | US-19 | CU-06 | A-10, A-11 | ADR-02, `RN-08` |
| BT-25 | Infraestructura compartida | — (puertas de cobertura y de tiempo) | Ninguno | `05` §11 `PA-09` |
| BT-26 | Infraestructura compartida | — (canal de entrega) | Ninguno | `05` §11 `PA-08` |

**Cobertura inversa: los doce casos de uso tienen al menos una tarea técnica que los realiza.** CU-01 en BT-10, BT-14, BT-16 y BT-22; CU-02 en BT-10, BT-11, BT-12 y BT-22; CU-03 en BT-07, BT-16 y BT-22; CU-04 en BT-07, BT-17 y BT-22; CU-05 en BT-07, BT-17 y BT-22; CU-06 en BT-08, BT-09, BT-14, BT-18, BT-23 y BT-24; CU-07 en BT-08, BT-14, BT-18 y BT-22; CU-08 en BT-07, BT-19 y BT-22; CU-09 en BT-08, BT-13, BT-14 y BT-15; CU-10 en BT-02; CU-11 en BT-03 y BT-04; CU-12 en BT-20 y BT-21.

**Cobertura de los quince puntos de acceso.** A-01, A-02, A-03 y A-05 en BT-16; A-06, A-07, A-08 y A-09 en BT-17; A-10, A-11, A-12, A-13 y A-14 en BT-18; A-15 en BT-19; A-16 en BT-03. **Los quince tienen tarea técnica**, y **los quince** quedan además recorridos por BT-12 —contra la guardia— y por BT-13 —contra la tabla de traducción—. **`A-04` no figura porque está retirado y no se recicla**: establecía la contraseña del primer ingreso sin credencial, y `RN-16` suprimió esa operación en lugar de resolverla.

**Cobertura de los ocho componentes de `05` §3.1.** Composición de raíz en BT-02; Guardia de admisión en BT-11 y BT-12; Traductor de motivos y códigos en BT-13, BT-14 y BT-15; Superficie de acceso y credencial propia en BT-16; Superficie de gobierno de la comisión en BT-17; Superficie de trabajos en BT-18; Superficie de desenlace en BT-19; Arranque y salud en BT-03 y BT-04. **Los ocho tienen tarea técnica**, y `CU-12` sigue **sin componente**, como `05` §3.3 declara: BT-20 produce un artefacto del árbol de muestras y no código de producción.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Api`. Declara **cinco** épicas técnicas —fundaciones y composición de raíz, superficie y formato, guardia y traducción, las cuatro superficies de acceso, y verificación con muestras y despliegue— y **veintiséis** tareas técnicas inline, cada una con tipo, fuente upstream por identificador, etapa, dependencias, criterios de aceptación verificables y las historias que la consumen. Declara las tres particularidades del proyecto de código: que **dos tareas son inspecciones en las dos direcciones** porque el defecto característico de esta capa es de omisión, que **BT-08 fija una decisión que obliga a otro proyecto de código** —el formato de intercambio, que `GeometriaFactory-Web` declaró que adopta—, y que **la pirámide de pruebas está invertida a propósito**. Convierte en trabajo los ocho puntos abiertos que lo admiten, incluido el límite de cuerpo que `GeometriaFactory-Infrastructure` reasignó acá. Emite la matriz BT ↔ US ↔ CU con sus veintiséis filas, la cobertura inversa sobre los doce casos de uso, la de los **quince** puntos de acceso —con la constancia de que `A-04` está retirado y no se recicla— y la de los **ocho** componentes. |
