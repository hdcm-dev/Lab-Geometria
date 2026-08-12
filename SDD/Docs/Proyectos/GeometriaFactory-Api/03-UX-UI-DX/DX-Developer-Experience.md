# DX — La superficie HTTP como producto de developer

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** DX-Developer-Experience.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4, §6, §8 y §11; `02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` completo; §6 de los doce casos de uso CU-01 a CU-12, y sus §3, §5, §9 y §10; `02-Especificacion-Funcional/Glosario-Funcional.md`; `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md`; `01-Necesidades-Negocio/Necesidades-Negocio.md` (NB-01 a NB-09); RN-01 a RN-16 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`; `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/` §6 y su `CU-06`; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §10, §14, §16, §18 y §17.5 íntegro
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops`, `10-Examples` y `11-Documentacion` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Rol de intervención developer](#1-rol-de-intervención-developer)
  - [1.1 Quién interviene acá](#11-quién-interviene-acá)
  - [1.2 Qué es la superficie pública de este proyecto de código](#12-qué-es-la-superficie-pública-de-este-proyecto-de-código)
  - [1.3 La frontera entre lo que se decide y lo que se transporta](#13-la-frontera-entre-lo-que-se-decide-y-lo-que-se-transporta)
  - [1.4 Las dos cosas que sólo se rompen acá](#14-las-dos-cosas-que-sólo-se-rompen-acá)
- [2. Onboarding por tramos](#2-onboarding-por-tramos)
- [3. Quick-start](#3-quick-start)
  - [3.1 Pasos](#31-pasos)
  - [3.2 Verificación del quick-start](#32-verificación-del-quick-start)
- [4. Diátaxis](#4-diátaxis)
- [5. Mensajes de error y diagnóstico](#5-mensajes-de-error-y-diagnóstico)
- [6. Métricas DX](#6-métricas-dx)
- [7. Feedback loop](#7-feedback-loop)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Rol de intervención developer

### 1.1 Quién interviene acá

No hay integradores externos y no los va a haber: el intake declara que **no hay clientes de terceros** y que por eso no hay versionado de rutas. Pero este proyecto de código tiene, a diferencia de las tres capas que ensambla, **un consumidor real que no es él mismo**: la pieza pública, que lo alcanza por HTTP y que se compila contra el mismo ensamblado de contratos.

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Implementador de la superficie | La persona que sostiene el producto, o el agente de IA que construye por etapas, agregando o cambiando un punto de acceso | **Qué puntos existen**, qué papel exige cada uno, qué códigos de respuesta declara y **qué guardia tiene que atravesar** |
| **Consumidor de la superficie** | Quien escribe el cliente tipado de la pieza pública. Es la misma persona, con otro sombrero, y **es el único consumidor legítimo** | Qué recibe ante cada fallo, cómo distingue un listado vacío de un servicio caído, y **qué respuestas nunca le van a decir nada más de lo que dicen** |
| Mantenedor de la capa | La misma persona, semanas después, sin el contexto de la etapa en que lo escribió | Por qué un código de respuesta es el que es, dónde va un punto nuevo y **qué se rompe agregándolo mal** |
| **Operador del despliegue** | El docente, que **despliega a mano** el contenedor del servicio | Qué significa un arranque que no atiende, qué revisar del lado del despliegue, y **por qué el mensaje no le dice la ruta** |

**El consumidor de la superficie es lo que hace distinta a esta sección.** En las capas de adentro ese papel se declara no aplicable, porque nadie las invoca por su superficie. Acá hay alguien del otro lado de un salto de red, y **todo lo que reciba es lo único que va a tener**: no puede leer un motivo interno, no puede inspeccionar el almacén y no puede preguntar de nuevo con más detalle.

Nivel de experiencia esperado: quien ya escribe servicios HTTP, pero **no** necesariamente conoce las tres reglas del producto que se rompen desde acá sin que nada falle. Esa parte no se supone conocida: se enseña en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.

### 1.2 Qué es la superficie pública de este proyecto de código

> **Este proyecto de código no tiene otra superficie que su superficie HTTP.** No lo referencia nadie por compilación —es el nivel 3, el último del orden topológico— y no expone ningún tipo propio: los tipos son del ensamblado de contratos. **Lo único que existe de él hacia afuera son sus quince puntos de acceso.**

Cinco consecuencias operativas, que gobiernan todo lo demás:

1. **Lo que no está en la superficie, no existe para nadie.** Una capacidad implementada en las tres capas de adentro y no expuesta acá es una capacidad que el producto no tiene. El mapa completo está en [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, y **es la primera lectura de esta sección**.
2. **Catorce de las quince rutas son propuesta derivada.** Las únicas cosas que una fuente declara son el punto de canje de credenciales, con su ruta, y la existencia de un punto de salud, cuya ruta **la fuente no da**. Leer la tabla sin haber leído §2 de aquel documento es el error de lectura más probable de todo este proyecto de código.
3. **Acá se traduce dos veces, y traducir es decidir.** De motivo interno a código del contrato, y de código del contrato a código de respuesta. La segunda traducción es la que puede romper una regla hacia afuera sin que nada falle.
4. **Acá está la única puerta.** Un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio. Todo lo demás del backend está detrás.
5. **Acá se aplica RA-03 en el único lugar donde se puede violar hacia afuera.** Es la última vez que un dato del backend se toca antes de salir del servidor propio.

**Tres ausencias que no son olvidos y que se reponen por comodidad**: no hay CORS, no hay WebSockets y no hay ningún punto pensado para que lo invoque un navegador. Las tres salen de RA-01, y reponerlas reabre las tres propiedades de la topología del producto: contenido mixto, CORS y exposición de la dirección del servidor propio.

### 1.3 La frontera entre lo que se decide y lo que se transporta

**Enunciado en una línea: esta capa decide cómo se dice, y no decide qué se dice.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Qué punto de acceso existe, con qué verbo y con qué código de respuesta | **Sí** | — |
| Verificar el acceso firmado y exigir el papel que el punto declara | **Sí.** El mecanismo de verificación es de la capa que toca el mundo; **exigirlo en cada punto es de acá** | — |
| Que **ningún punto** quede fuera de la guardia del cambio de contraseña pendiente | **Sí.** La comprobación es de la capa de aplicación | — |
| Elegir el código de respuesta de cada código del contrato | **Sí** | — |
| Conectar cada puerto con su adaptador y tomar la configuración del despliegue | **Sí** | — |
| Decidir si una cuenta admite el acceso, la pertenencia de un trabajo o la facultad sobre el dato | **No.** Llegan resueltas | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Llega decidido y viaja en una respuesta **exitosa** | `GeometriaFactory-Domain` |
| Interpretar el texto del alumno o verificar sus valores | **No.** El texto viaja como cadena y **no se normaliza en el borde** | `GeometriaFactory-Infrastructure` |
| Declarar qué campos cruzan la frontera y qué códigos existen | **No.** **Esta capa no agrega ningún código al conjunto cerrado** | `GeometriaFactory-Contracts` |
| Presentar el estado degradado a una persona | **No** | `GeometriaFactory-Web` |

Tres precisiones que la tabla no alcanza a decir sola:

1. **Exigir el papel no es autorizar**, y duplicar la autorización acá sería peor que no hacerla: crearía un segundo lugar donde la regla puede decir otra cosa. Lo que la guardia aporta es cortar temprano **lo que ningún dato podría autorizar**.
2. **RA-02 no tiene tramo acá, y se declara.** Esta capa no compone el bundle del visor, no lo sirve y no lo configura. Su contribución es negativa y estructural: **al no existir ningún punto pensado para el navegador, no hay nada que el bundle pudiera llamar aunque quisiera**. No tener tramo no es incumplirla.
3. **Sin estado.** Ningún punto depende de lo que ocurrió en la petición anterior. Lo que se parece a una sesión vive en el circuito de la pieza pública, del lado de su servidor, y **el acceso firmado nunca llega al navegador**.

### 1.4 Las dos cosas que sólo se rompen acá

De las **dieciséis** reglas de negocio del producto, **dos se pueden romper desde esta capa hacia afuera sin que ninguna capa de adentro se entere**, porque las de adentro habrían hecho su parte bien.

| Regla | Qué se rompe si acá se hace mal | Dónde se verifica |
| --- | --- | --- |
| **RN-03** — el trabajo ajeno es indistinguible del inexistente | Responder «no autorizado» donde la regla exige «no encontrado» **confirma la existencia de un recurso ajeno**, y permite averiguar por tanteo qué identificadores existen. Nada falla: la capa de aplicación devolvió el motivo correcto y esta capa lo tradujo mal | `CU-06` CA-07, `CU-07` CA-07 y CA-08, `CU-09` CA-03 |
| **RN-13** — con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | **Agregar un punto de acceso y olvidarse de la guardia** la incumple sin que nada falle: el punto funciona, responde bien y deja operar a una cuenta que no debería. El defecto no está en lo que el punto hace, está en lo que no atraviesa | `CU-02` CA-01 y CA-05 |

**Las dos se rompen produciendo algo válido**, y ése es el patrón. Por eso sus criterios de aceptación **comparan respuestas** y **cuentan puntos**, en lugar de esperar que algo falle.

Y una tercera, que no es una regla de negocio sino de arquitectura, y que tiene el mismo patrón: **RA-03**. Un mensaje que incluya la ruta del almacén o la dirección de un servicio interno no rompe nada visible; simplemente le entrega a quien mire la respuesta algo que no debería tener.

## 2. Onboarding por tramos

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y el servicio arranca sobre un almacén vacío | `./scripts/build.sh` termina en 0 y sin advertencias, `./scripts/test.sh` pasa entero y el punto de salud responde |
| 30 minutos | **Sabe qué existe hacia afuera.** Dado un pedido cualquiera del producto, nombra el punto de acceso que lo atiende, el papel que exige y **si atraviesa la guardia o no** | Reproduce, sin abrirla, la partición de la tabla de puntos de acceso: **cuatro sin acceso firmado y once bajo la guardia. Cuatro más once son quince**, y ninguno queda con su forma de identificación abierta |
| 1 hora | **Corre la colección entera y entiende por qué los ocho escenarios responden con éxito.** Explica por qué un envío que no verifica **no es un fallo de protocolo**, y qué pasaría si lo fuera | La colección ejecutada, con sus **8** envíos, **8** respuestas de éxito y **2** trabajos en `Borrador` |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

**El tramo de 1 hora es el que más rinde de esta capa**, y su objetivo no es casual: la confusión que evita —creer que un texto que no verifica es un fallo de la petición— es la que convertiría el mayor valor didáctico del producto en un mensaje de error.

## 3. Quick-start

Objetivo del quick-start: **el primer resultado exitoso**, que acá es **la colección de peticiones corriendo entera contra el servicio real**. Es el resultado que mejor explica la capa: no hay pantalla, no hay circuito y no hay visor.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas. Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio de código en el entorno de desarrollo contenido, que el
#    propio repositorio define en `.devcontainer/`. Todo lo demás corre adentro.

# 1. Guion de reinicio del almacén: deja el estado de primer arranque.
#    Criterio de éxito: el almacén queda vacío y con su esquema al día.
./scripts/reset-db.sh

# 2. Guion de ejecución del servicio.
#    Criterio de éxito: arranca, aplica las transformaciones y el punto de salud responde.
./scripts/run-api.sh

# 3. Ejecutar la colección de peticiones contra el servicio.
#    Criterio de éxito: los 8 envíos responden con éxito, 6 trabajos en estado
#    `Pendiente` y 2 en `Borrador`.
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, guion de reinicio del almacén, guion de ejecución del servicio, colección de peticiones— y conservan su forma literal porque el lector los tiene que poder ejecutar. **Las rutas y los nombres de guion salen del intake §16 y §18: no se eligen acá.**

**Tres pasos, sobre el máximo de cinco** que el intake exige a las muestras del producto.

Lo que el quick-start deliberadamente **no** incluye: publicar la imagen, alcanzar la red desde afuera, configurar un dominio. Ninguna hace falta para el primer resultado, y si un paso futuro las pidiera, **el paso está mal ubicado**.

### 3.2 Verificación del quick-start

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los guiones y las rutas salen del intake y **no se inventan acá**.

## 4. Diátaxis

Los cuatro modos existen, y **tres de ellos ya viven en artefactos de la cadena**: este documento no los duplica, los ubica y los enlaza.

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca toqué esta superficie; llevame de la mano una hora» |
| How-to | Tarea | Los doce casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) | «Tengo que agregar un punto / traducir un motivo / arrancar el servicio: qué tengo que sostener» |
| Reference | Información | [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, §4 y §6; [`DX-Error-Messages.md`](DX-Error-Messages.md) para el catálogo; los dos glosarios | «Qué punto atiende esto» / «qué código de respuesta le corresponde a este código del contrato» |
| Explanation | Comprensión | §1.2, §1.3 y §1.4 de este documento; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §1, §2, §5 y §7; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué un envío que no verifica responde con éxito» / «por qué quince rutas están sin decidir» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

Regla de mantenimiento: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza.

## 5. Mensajes de error y diagnóstico

Principio de redacción, aplicado sin excepción a las **18** entradas del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. Acá la tercera parte tiene dos destinatarios distintos, y esa es la particularidad de esta capa:

> **Casi todo lo que responde esta superficie lo lee un programa, no una persona.** El consumidor es el código de la pieza pública, y lo que necesita no es un texto: es **saber qué hacer**, que es siempre una de cuatro cosas —corregir y reintentar, derivar a otra pantalla, mostrar lo que pasó, o pasar a estado degradado—. Las únicas dos entradas cuyo destinatario es una persona directamente son las del arranque detenido, y esa persona es **el operador que despliega a mano**.

Cinco precisiones que el catálogo hace cumplir:

1. **Dos resultados que parecen fallos son el funcionamiento normal del producto**, y ninguno tiene entrada en el catálogo: **el texto que no verifica** y **el listado vacío**. Los dos viajan en respuestas exitosas, y [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 los reúne.
2. **La confusión más cara de esta capa es una sola**, y conviene poder recitarla: si un envío cuyo texto no verifica respondiera con un código de fallo, el producto le diría a la persona que su petición estaba mal **cuando su trabajo se guardó y sus errores están localizados por figura y por campo**. Vería un fallo y no vería lo único que le sirve.
3. **Tres familias de respuestas están deliberadamente empobrecidas**, y no es un defecto: la respuesta genérica de credenciales inválidas, la del recurso que no se ve y la del correo ya registrado. Las tres dicen **menos** de lo que saben, y las tres tienen la misma razón: no confirmar la existencia de algo que el solicitante no debería saber que existe.
4. **Ningún mensaje incluye la ruta del almacén, la clave de firma, una contraseña, la provisoria, el texto del alumno ni la dirección de un servicio interno.** Es RA-03, y su contracara obligatoria es que **todo error respondido queda registrado del lado del servidor**, junto con **todo intento de acceso rechazado**.
5. **Esta capa no reintenta.** Devuelve el código y quien decida reintentar es la pieza pública, que es la que sabe qué estaba haciendo la persona.

## 6. Métricas DX

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: no hay developers externos a quienes encuestar y el equipo es de una persona más un agente de IA.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio de código hasta la colección corriendo entera | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio |
| TTFV | Tiempo hasta el primer valor: haber corrido la colección y saber por qué los ocho escenarios responden con éxito | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 3 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de respuestas | Códigos del conjunto cerrado del contrato con destino declarado, más las respuestas sin código | **16 de 16**, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §6 |
| **Puntos de acceso bajo la guardia** | Puntos que exigen acceso firmado y atraviesan la guardia de admisión | **11 de 11, sin tolerancia.** Un punto nuevo entra a la cuenta el mismo día que se agrega | Recuento de la tabla de puntos contra la lista de puntos guardados, en cada punto de control |
| **Códigos inventados** | Códigos del contrato que esta capa produce y que no pertenecen al conjunto cerrado del ensamblado | **0, sin tolerancia** | Recuento de los códigos que la superficie emite contra el conjunto cerrado |
| **Secretos, rutas y textos filtrados** | Respuestas o trazas que contengan la clave de firma, una contraseña, la provisoria, la ruta del almacén, el texto del alumno o la dirección de un servicio interno | **0, sin tolerancia** | Inspección de las respuestas de error y del registro del servidor en cada punto de control |
| **Respuestas que distinguen lo ajeno de lo inexistente** | Pares de respuestas —recurso ajeno contra recurso inexistente— que difieran en algo | **0, sin tolerancia.** Es RN-03 medida directamente | Comparación byte a byte de los pares de CA-03 de `CU-09` |
| **Textos de prueba inventados** | Cuerpos de la colección que no salgan de los escenarios declarados | **0, sin tolerancia.** Es una regla de delivery del producto | Comparación de los cuerpos contra el intake §20, en cada punto de control |

Las tres primeras son las métricas DX canónicas. **Las seis últimas son propias de este proyecto de código**, y cinco de ellas tienen tolerancia cero porque miden exactamente las cosas que se rompen produciendo algo válido.

## 7. Feedback loop

No hay canal de issues externo ni encuesta a developers de adopción. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control. Un cambio incompatible en el ensamblado de contratos **rompe la compilación de los dos extremos**, que es la señal más temprana posible | Una compilación rota es retroalimentación inmediata, no un accidente de construcción |
| **La colección de peticiones** | Es la demostración ejecutable de la superficie. Cuando una de sus respuestas esperadas deja de darse, la señal no es «una prueba rota»: es **que la superficie cambió sin que nadie lo declarara** | Se corrige antes de fusionar, y si el cambio era deliberado, se declara en la superficie y en la colección a la vez |
| **El consumidor de la superficie** | Quien escribe el cliente tipado de la pieza pública es el primero que descubre que una respuesta no le alcanza para saber qué hacer. **Una respuesta que lo obliga a adivinar es un defecto de esta sección** | Se corrige el catálogo, no el cliente |
| **El despliegue a mano** | El docente es el primero que ve un arranque que no atiende. **Un mensaje que no le alcanza para saber qué revisar es un defecto de esta sección**, no del despliegue | Se corrige el diagnóstico accionable de esa entrada |
| Informe de cierre por etapa | Documento autocontenido por etapa | Lo que costó entender baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Implementador de la superficie, **consumidor de la superficie**, mantenedor de la capa y **operador del despliegue**, los cuatro internos al producto (§1.1) |
| Superficie pública que se documenta | Los **quince** puntos de acceso de [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, y las dos traducciones |
| CU origen | CU-01 a CU-12 de este proyecto de código |
| Reglas de negocio relevantes | RN-01 a RN-16, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6: **trece con tramo acá, tres sin él, y dos que esta capa puede romper hacia afuera sola** —RN-03 y RN-13— |
| Necesidades de negocio | NB-01 a NB-09, **las nueve**, tres de ellas parcialmente, y **NB-08 con su primer tramo propio del producto** |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US de la guardia sobre los once puntos, **con el recuento como criterio de aceptación**; US de las dos traducciones; US del arranque detenido; US de la colección reproducible en tres pasos; US del quick-start verificable en el punto de control |
| Tests previstos en 08 | Integración contra el servicio real, con la pirámide invertida que el intake declara a propósito; **una prueba por punto y por condición de la guardia**; **una prueba por código del conjunto cerrado**; y las inspecciones de secretos, rutas y textos inventados |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema aplicada | **Parcialmente pertinente y no aplicable como extensión.** La configuración —ubicación del almacén y clave de firma— **entra por acá**, y lo que esta sección declara sobre ella es qué pasa cuando falta: el servicio no atiende. Su forma es de `05-Arquitectura-Tecnica` y su provisión, de `09-Devops` |
| Primer arranque aplicado | **Pertinente y acotado.** El primer arranque de la instancia existe —el punto de configuración de la cuenta de administrador, que sólo procede mientras no exista ninguna— pero **no es una superficie de aprovisionamiento**: es un punto de acceso más. La superficie de aprovisionamiento que una persona recorre vive en la categoría 03 de la pieza pública |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; lo que declara es el papel que cada punto exige |
| Identidad de versión aplicada | **Pertinente.** Este proyecto de código **sí** produce un artefacto desplegable identificable —la imagen del servicio— y el producto etiqueta cada etapa cerrada para poder volver a cualquier demostración. Qué informa el punto de salud sobre la versión **no está declarado por ninguna fuente** y es de `05-Arquitectura-Tecnica` |
| Modelo UX-UI aplicado en la Fase B2, validación visual y línea de base | N/A. `requiere_maqueta` == false |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial de la categoría para este proyecto de código. Declara el rol de intervención con **cuatro tipos internos**, entre ellos el **consumidor de la superficie**, que las capas de adentro declaran no aplicable, y el **operador del despliegue**; el enunciado de que **este proyecto de código no tiene otra superficie que su superficie HTTP**, con sus cinco consecuencias y las tres ausencias que RA-01 produce; la frontera entre lo que se decide y lo que se transporta, con sus tres precisiones, incluida la no aplicabilidad declarada de RA-02; **las dos reglas que esta capa puede romper hacia afuera sola** —RN-03 y RN-13— con lo que se rompe en cada caso y dónde se verifica; el onboarding en tres tramos, con el de una hora dedicado a por qué los ocho escenarios responden con éxito; el quick-start de tres pasos entero dentro del entorno de desarrollo contenido; la ubicación de los cuatro modos de Diátaxis; los principios de error con sus cinco precisiones, entre ellas la confusión más cara de esta capa y las tres familias de respuestas deliberadamente empobrecidas; **nueve métricas DX** medibles a mano, cinco con tolerancia cero; y el lazo de retroalimentación apoyado en el punto de control, en la colección, en el consumidor de la superficie y en el despliegue a mano del docente. |
| 1.1 | 2026-08-10 | Actualización por `PRODUCT-INTAKE` **1.13** §4.1 (**RN-16**) y la precisión de **F-04**: el punto de acceso `A-04` se retira de la superficie, porque la escritura anónima que exponía dejó de existir. §1 y §8 actualizan los recuentos —de dieciséis a **quince** puntos, y de quince a **catorce** rutas derivadas—. **Ninguna métrica, ningún compromiso de verificación y ningún principio de esta guía cambia.** Sube minor. |
| 1.2 | 2026-08-11 | **Cierra los hallazgos `B-API-01` (P0), `B-API-05` (P1), `B-API-06` (P1) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§2**, tramo de **30 minutos**, columna «Cómo se verifica»: el objetivo describía una partición de **dieciséis** puntos —«cuatro sin acceso, uno con identidad abierta y once bajo la guardia»— y pasa a la vigente, «**cuatro sin acceso firmado y once bajo la guardia**, cuatro más once son quince, y ninguno queda con su forma de identificación abierta», que es literalmente lo que declara `../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3 tras el retiro de `A-04`, recorridas sus quince filas. Era el único **objetivo verificable** de la sección que **daba falso al reproducirlo bien**. **§5**, primera línea, y **§6**, métrica «Cobertura del catálogo de respuestas»: las entradas del catálogo pasan de **18** a **16** y el objetivo de «18 de 18» a «**16 de 16**». La métrica declara como forma de medición el recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §6, y ese recuento da **16 = 14 + 2**: **medida como manda su propio texto, la métrica se incumplía a sí misma**. **§8**, fila «Reglas de negocio relevantes»: el reparto pasa de «trece con tramo acá, **dos** sin él» a «**tres** sin él», que es lo que dice la fuente citada, `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, con las tres sin tramo contadas sobre su tabla: `RN-05`, `RN-14` y `RN-16`. **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26** y `PRODUCT-MANIFEST` **1.3**. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**, según la condición de método del informe: el recuento del catálogo se citaba mal en **seis lugares vivos de cuatro documentos** de esta categoría —dos de ellos en este archivo— y los seis se corrigen en esta tanda; la partición de dieciséis puntos **no sobrevive en ningún otro lugar vivo**, verificado contra `../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md` §3, que ya declara «quince puntos: cuatro sin acceso firmado y once bajo la guardia». **Ningún tramo, quick-start, principio de error ni métrica se agrega o se retira.** Sube minor. **Enmienda de esta misma fila, 2026-08-11**, absorbida en la versión en curso sin subir —la política de versionado del framework absorbe dentro de la versión vigente las correcciones derivadas del audit de la propia fase de emisión mientras el documento está en `Propuesto`—: el alcance de propagación declaraba «cinco documentos» donde son **cuatro**, contados sobre la enumeración misma —`README.md`, `Glosario-UX.md`, `DX-Developer-Experience.md` y `Guia-Onboarding-Developer.md`—; el número venía heredado sin recontar de la ronda 1. **Los seis lugares siguen siendo seis y ningún recuento del producto se mueve.** Cierra el hallazgo `N-01` (P2) de [`B-02-03-GeometriaFactory-Api-r2.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r2.md) 1.0. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
