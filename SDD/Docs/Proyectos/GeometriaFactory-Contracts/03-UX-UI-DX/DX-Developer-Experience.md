# DX — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** DX-Developer-Experience.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §2, §3.1, §4.2 y §6 (`RT-01` a `RT-11`); los **ocho** contratos de uso `CU-01` a `CU-08`, con sus §17 de compatibilidad de versión pública, con `CU-01` §10, `CU-06` §10, `CU-07` §10 y `CU-08` §10; `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3.1, §3.2 y §3.3; `00-Contexto/Vision-Producto.md` §3.2, §7, §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §2.2, §4.3, §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2 y las necesidades NB-02, NB-03, NB-04, NB-08, **NB-09**; `PRODUCT-INTAKE` 1.7 §4 (F-21 a F-24, **F-26**), §4.1 (RN-10, RN-11, **RN-12**, **RN-13**), §4.2 (modelo de estados), §17.1.P.2 (**INV-09**), §17.4 P.1 a P.12, §14 (**RA-01**, RA-03), §15 (etapas y puntos de control), §16 y §16.1
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `10-Examples` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Rol de intervención developer](#1-rol-de-intervención-developer)
  - [1.1 Quién interviene sobre este proyecto de código](#11-quién-interviene-sobre-este-proyecto-de-código)
  - [1.2 Lo primero que hay que entender](#12-lo-primero-que-hay-que-entender)
- [2. Onboarding por tramos](#2-onboarding-por-tramos)
- [3. Quick-start](#3-quick-start)
  - [3.1 Prerrequisito único](#31-prerrequisito-único)
  - [3.2 Pasos](#32-pasos)
  - [3.3 Qué cuenta como primer resultado exitoso](#33-qué-cuenta-como-primer-resultado-exitoso)
- [4. Diátaxis](#4-diátaxis)
  - [4.1 Los cuatro modos y dónde vive cada uno](#41-los-cuatro-modos-y-dónde-vive-cada-uno)
  - [4.2 Cómo se enlazan](#42-cómo-se-enlazan)
- [5. Mensajes de error y diagnóstico](#5-mensajes-de-error-y-diagnóstico)
  - [5.1 Dos clases de error, no una](#51-dos-clases-de-error-no-una)
  - [5.2 Principios de redacción](#52-principios-de-redacción)
- [6. Métricas DX](#6-métricas-dx)
- [7. Feedback loop](#7-feedback-loop)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Rol de intervención developer

### 1.1 Quién interviene sobre este proyecto de código

`GeometriaFactory-Contracts` no tiene integradores externos. No se publica en ningún feed —`redistribuible` es false (`PRODUCT-INTAKE` §13)— y sus dos únicos consumidores del contrato son `GeometriaFactory-Api` y `GeometriaFactory-Web`, del mismo producto y compilados contra el mismo ensamblado. El equipo del producto es de una persona más un agente de IA que construye por etapas (`PRODUCT-INTAKE` §15). De ahí que el rol de intervención de este documento no sea el integrador hipotético de una biblioteca pública, sino tres figuras concretas y verificables.

| Rol de intervención | Tipo | Qué necesita de esta documentación | Nivel de experiencia esperado |
| --- | --- | --- | --- |
| **Mantenedor presente** | Mantenedor | Saber qué puede tocar sin romper el otro extremo, y qué se rechaza en revisión aunque compile | Es quien escribió el ensamblado; conoce el dominio del producto y las restricciones de la topología |
| **Mantenedor futuro** | Mantenedor | La misma persona, meses después, sin el contexto en la cabeza. Necesita reconstruir en una hora por qué la proyección de listado es pobre a propósito y por qué el texto original viaja como cadena | El mismo, con memoria caducada. Es el lector para el que se escribe el «por qué» y no sólo el «qué» |
| **Agente de construcción por etapas** | Integrador | Recorre la cadena documental acumulando contexto y escribe el código de la etapa en curso. Necesita reglas enunciadas como predicados verificables, no como recomendaciones | Sin memoria entre sesiones. No infiere una prohibición que no esté escrita |

Los tres comparten herramienta y entorno: el contenedor de desarrollo del repositorio. **El host de desarrollo no tiene instaladas las herramientas de construcción y no va a tenerlas**, de modo que ningún paso de esta documentación asume nada fuera del contenedor.

Lo que este rol de intervención **no** es: no hay developer de terceros que descubra el ensamblado y lo adopte, no hay portal de documentación (`tiene_portal_developers` es false) y no hay canal público de issues. La ausencia de terceros es lo que sostiene tres decisiones del intake que esta documentación no reabre: no hay generación de clientes a partir de una descripción formal del servicio, no hay versionado de rutas, y la respuesta a un cambio incompatible es el despliegue conjunto de las dos piezas desplegables (`PRODUCT-INTAKE` §17.4 P.2, P.3).

### 1.2 Lo primero que hay que entender

Antes de cualquier tramo de onboarding, quien toca este proyecto de código tiene que haber entendido una sola frase, que es la que le da razón de existir:

> **La superficie pública de este ensamblado es donde se decide qué se expone.**

Ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno (`RT-01`, regla de arquitectura RA-03 de `PRODUCT-INTAKE` §14). No es una recomendación de estilo: es la vía por la que el acoplamiento y la filtración vuelven al producto. Un campo agregado acá compila sin protestar y llega hasta el otro extremo.

De esa frase se desprenden las otras dos restricciones que gobiernan el recorrido de integración:

- **El ensamblado no declara ninguna referencia hacia `GeometriaFactory-Domain`** (`RT-05`, quality gate bloqueante de `PRODUCT-INTAKE` §17.4 P.8). Es lo que impide que la pieza pública conozca las entidades del dominio.
- **La proyección de listado no arrastra el texto original, ni los componentes de las piezas, ni el comentario del administrador** (`RT-04`, `PRODUCT-INTAKE` §17.4 P.10). El listado existe precisamente para no ser el detalle: transporta el estado, que es lo que expresa el desenlace, y no el texto libre de cada trabajo.
- **El comentario del administrador y las observaciones del validador no comparten ni un campo** (`RT-09`). Los dos viajan en el mismo tipo de detalle y los dos son texto sobre el trabajo, y ahí termina el parecido: la observación la emite el producto al interpretar el texto y es una colección con severidad, índice de figura, campo señalado y par de valores; el comentario lo escribe una persona, es a lo sumo uno y no tiene ninguno de esos campos. **Es el error más fácil de cometer contra este contrato**, y por eso tiene restricción transversal propia.

## 2. Onboarding por tramos

Los tres tramos son acumulativos y cada uno tiene un objetivo verificable, no una sensación de comprensión. El recorrido completo está desarrollado paso a paso en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md); acá se declaran los hitos y su forma de verificación.

| Tramo | Objetivo | Verificación |
| --- | --- | --- |
| **5 minutos** | El ensamblado de contratos se construye dentro del contenedor de desarrollo | El comando de construcción del repositorio termina en 0 **y sin advertencias**, que es el quality gate de `PRODUCT-INTAKE` §17.4 P.8 |
| **30 minutos** | Ubicar cualquier tipo de transferencia en su familia y saber qué transporta y qué no | Responder sin abrir el código las cinco preguntas de [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §3.1: dónde vive el texto original completo, por qué la proyección de listado no lo trae, qué campos tiene la respuesta de error, qué recibe quien todavía no estableció su contraseña, y **en qué se diferencia el comentario del administrador de una observación** |
| **1 hora** | Clasificar un cambio propuesto sobre la superficie pública en **una de tres salidas** —compatible; incompatible, que obliga al despliegue conjunto; o rechazado aunque compile— y saber cuál es la respuesta operativa de cada una | Clasificar correctamente los **cuatro** cambios de control de [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §3.3, con su salida y su acción, y enunciar que ante un cambio incompatible las dos piezas desplegables se despliegan juntas (`RT-06`), sin versionar rutas |

El tramo de una hora es el que define el valor de este proyecto de código para el equipo. Un mantenedor que no sabe clasificar un cambio incompatible no puede tocar el ensamblado: la señal más valiosa del diseño —que la incompatibilidad rompe la compilación antes que el tiempo de ejecución— sólo sirve si quien la recibe sabe leerla.

## 3. Quick-start

### 3.1 Prerrequisito único

El repositorio abierto en el **contenedor de desarrollo** declarado en `.devcontainer/devcontainer.json` (`PRODUCT-INTAKE` §16). No hace falta nada más, y no se admite nada más: el ciclo entero ocurre dentro del contenedor.

Los comandos de construcción del repositorio viven en `scripts/` y existen desde la etapa `a` del plan de entrega, que es el andamiaje de la solución de código (`PRODUCT-INTAKE` §15). Si la etapa `a` no está cerrada, este quick-start no aplica todavía y no hay forma de sustituirlo con herramientas del host.

### 3.2 Pasos

```bash
# Todo lo que sigue se ejecuta DENTRO del contenedor de desarrollo,
# desde la raíz del repositorio.

# 1. Construir la solución de código completa. El ensamblado es nivel 0
#    del orden topológico: se construye primero y sin depender de nadie.
bash scripts/build.sh

# 2. Verificar el quality gate bloqueante RT-05: el ensamblado de contratos
#    no declara ninguna referencia hacia el proyecto de código de dominio.
#    El resultado esperado es SIN COINCIDENCIAS.
grep -R "GeometriaFactory.Domain" src/GeometriaFactory.Contracts/ || echo "OK: RT-05 se cumple"

# 3. Ejercitar los tipos de transferencia de punta a punta. Este proyecto de
#    código no tiene pruebas propias (RT-07): se verifica desde las pruebas
#    de integración que golpean el servicio real.
bash scripts/test.sh
```

### 3.3 Qué cuenta como primer resultado exitoso

El paso 1 terminando en 0 **y sin advertencias**. Es el resultado mínimo reproducible y es exactamente el quality gate del pipeline (`PRODUCT-INTAKE` §17.4 P.8): si la construcción emite una advertencia, el paso no está superado, aunque el ensamblado se haya generado.

El paso 2 es el que hace tangible la propiedad central del proyecto de código, y conviene ejecutarlo la primera vez aunque parezca ceremonial: es la única verificación de `RT-05` que un mantenedor puede hacer con un solo comando, y su automatización pertenece a 09.

El paso 3 es el primer valor real: los tipos de transferencia ejercitados contra el servicio real. Es más lento que los dos anteriores y depende de que las etapas correspondientes estén cerradas; por eso no se cuenta como primer éxito sino como primer valor (ver §6).

**Este quick-start no incluye un fragmento de código que instancie un tipo de transferencia**, y la omisión es deliberada: `PRODUCT-INTAKE` §16.1 declara que este proyecto de código no produce samples propios, porque no lo consumen integradores externos. Un fragmento inventado acá se desincronizaría de la superficie real en la primera etapa. El fragmento ejecutable del recorrido es el bloque de comandos de arriba, que corre sin modificaciones.

## 4. Diátaxis

### 4.1 Los cuatro modos y dónde vive cada uno

La documentación de este proyecto de código no vive en un portal —`tiene_portal_developers` es false— sino en la propia cadena documental del producto. El plan de Diátaxis consiste, entonces, en declarar qué categoría cumple cada modo y no mezclarlos.

| Modo | Orientación | Dónde vive | Qué contiene |
| --- | --- | --- | --- |
| **Tutorial** | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md), en esta misma sección | El recorrido de la primera hora, con pasos en orden y un solo camino. No enumera alternativas: enseña |
| **How-to** | Tarea | [`DX-Error-Messages.md`](DX-Error-Messages.md) para diagnosticar un error concreto; §17 de cada contrato de uso en `02-Especificacion-Funcional/Casos-De-Uso/` para decidir si un cambio es compatible; `11-Documentacion` para el cuerpo documental de entrega | Respuestas a «cómo hago X», sin explicar el modelo entero |
| **Reference** | Información | Los ocho contratos de uso `CU-01` a `CU-08` de `02-Especificacion-Funcional/`, que son la descripción normativa de qué viaja en cada familia de tipos; `Especificacion-Funcional.md` §6 para las once restricciones transversales | Lo consultable punto por punto. **Esta categoría no lo duplica**: lo referencia |
| **Explanation** | Comprensión | §1.2 y §5.1 de este documento; `Especificacion-Funcional.md` §2 y §5; los ADR de `05-Arquitectura-Tecnica` | El porqué: por qué existe un ensamblado separado, por qué el texto original viaja como cadena, por qué la incompatibilidad se detecta compilando |

### 4.2 Cómo se enlazan

El punto de entrada es el [`README.md`](README.md) de esta sección. Desde ahí:

- Quien llega por primera vez entra por el **tutorial** y sale de él sabiendo construir y clasificar un cambio.
- Quien llega con un síntoma —un error de construcción, un código de error transportado— entra por el **how-to** del catálogo de errores, que en cada fila remite a la **reference** del contrato de uso que lo origina.
- Quien llega con una discusión de diseño —«¿por qué no versionamos rutas?»— entra por la **explanation**, que remite al intake y a los ADR de 05 sin reproducirlos.

La regla de no duplicación es explícita y es la que sostiene el plan: **ningún modo reescribe el contenido de otro**. Un contrato de uso no se explica acá; un principio de diseño no se enumera en la guía de onboarding. Lo que esta sección aporta es el recorrido y el diagnóstico; la descripción normativa de la superficie es de 02, y el cuerpo documental de entrega es de 11.

## 5. Mensajes de error y diagnóstico

### 5.1 Dos clases de error, no una

Un ensamblado de contratos no produce excepciones en tiempo de ejecución: no tiene comportamiento. Los errores que le corresponden son de dos clases, y confundirlas es lo que lleva a diagnósticos inútiles.

**Clase C — errores de construcción del contrato.** Son los que rompen la compilación de al menos uno de los dos consumidores del contrato, o los que compilan y aun así se rechazan en revisión. Son la señal más temprana posible y **la propiedad de diseño más valiosa de este proyecto de código**: como los dos extremos compilan contra el mismo ensamblado, un cambio incompatible se detecta antes del tiempo de ejecución (`PRODUCT-INTAKE` §17.4 P.3). El diagnóstico accionable de esta clase termina siempre en el mismo lugar: la respuesta correcta es **desplegar las dos piezas desplegables juntas**, nunca versionar rutas ni introducir un contrato paralelo (`RT-06`).

**Clase T — errores transportados.** Son las respuestas de error de la pieza de datos, expresadas como tipo de transferencia. No los produce el ensamblado: los transporta. Su forma única es la de `CU-06`: código de un conjunto cerrado de **dieciséis**, texto neutro, colección de detalles de ubicación y momento. Fuera de ese conjunto quedan **tres señales declaradas** que no son error y que se catalogan aparte, porque tratarlas como fallo es un defecto tan caro como no representar un fallo real. El diagnóstico tiene que quedar accionable **sin filtrar nada**: con índice de figura y campo señalado cuando corresponde, y nunca con la dirección de un servicio interno (`RT-02`).

El catálogo completo de las dos clases, con causa probable y acción sugerida por entrada, está en [`DX-Error-Messages.md`](DX-Error-Messages.md).

### 5.2 Principios de redacción

Los tres, aplicados a las dos clases: **qué pasó, por qué pasó, qué hacer al respecto**. Y dos precisiones propias de este proyecto de código:

- Un mensaje de la clase T no se redacta acá para la persona: lo que este ensamblado transporta es texto neutro. Cómo se le presenta a un alumno o a un administrador es decisión de `GeometriaFactory-Web` y de su propia categoría 03 (`CU-06` §10).
- Un mensaje de la clase T nunca distingue casos que el contrato decidió no distinguir. Un recurso ajeno y un recurso inexistente producen el mismo código y el mismo texto (`CU-06` FA-03, CA-05); un canje fallido no revela si falló el correo o la contraseña (`CU-01` CA-03). Redactar un texto más «útil» ahí es una filtración, no una mejora.

## 6. Métricas DX

Las métricas estándar de una biblioteca pública —telemetría de adopción, encuestas a developers de adopción reciente, tasa de abandono— **no aplican**: no hay adopción externa que medir y el universo de developers es de una persona más un agente. Se conservan las dos métricas de tiempo, que sí son medibles por observación directa, y se sustituye la tasa de error de onboarding por una métrica que el producto ya recoge en cada punto de control.

| Métrica | Definición en este proyecto de código | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| **TTFS** (time-to-first-success) | Desde abrir el repositorio en el contenedor de desarrollo hasta la construcción terminando en 0 y sin advertencias | <= 5 minutos | Observación directa una vez por etapa cerrada, anotada en el informe de cierre de la etapa |
| **TTFV** (time-to-first-value) | Desde el mismo punto de partida hasta clasificar correctamente un cambio propuesto en una de las **tres salidas** —compatible; incompatible, con despliegue conjunto; o rechazado aunque compile— con su acción operativa | <= 1 hora | Los **cuatro** cambios de control de [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §3.3, resueltos sin abrir el código. Los cuatro, y no una muestra: la métrica pierde sentido si el conjunto no cubre las tres salidas |
| **Tasa de reincidencia de una filtración** | Proporción de revisiones de la superficie pública en las que aparece un campo prohibido por `RT-01` o por `RT-04` | 0 | Revisión del pull request de la etapa, que **es** el punto de control (`PRODUCT-INTAKE` §15) |
| **Cobertura de tipos ejercitados** | Proporción de tipos de transferencia cubiertos por al menos una prueba de integración | 100 %, gate bloqueante. **El intake rotula ese valor `[ASUNCIÓN]` y lo lista en §22**: está completo y se usa como valor vigente hasta que el Product Owner lo confirme | `PRODUCT-INTAKE` §17.4 P.6; `Especificacion-Funcional.md` §6 `RT-07`. Su verificación pertenece a `08-Calidad-Y-Pruebas` |

La tercera métrica es la única que este proyecto de código no puede permitirse ver degradada: un tipo con un campo de más ya cruzó la frontera de servicio, y lo que se filtró no se recupera.

## 7. Feedback loop

No hay issues públicos, ni discusiones abiertas, ni telemetría con consentimiento: no hay a quién pedírselo. El lazo de retroalimentación de este proyecto de código es el que el propio plan de entrega ya impone, y esta categoría se cuelga de él en lugar de inventar uno paralelo.

| Vía | Cuándo | Qué se recoge | Dónde queda |
| --- | --- | --- | --- |
| **Punto de control de la etapa** | Al cerrar cada etapa; es una detención bloqueante a la espera del OK explícito | Toda fricción encontrada al tocar el ensamblado durante la etapa | Informe de cierre de la etapa, en `Avances/<orden>-<etapa>.md` (`PRODUCT-INTAKE` §15) |
| **Pull request de la etapa** | Una rama y un pull request por etapa; el pull request es el punto de control | Aparición de un campo prohibido, o de un cambio incompatible no declarado | La revisión del pull request, que es donde `RT-05` y `RT-01` se rechazan |
| **Fricción del agente de construcción** | Cuando el agente pide un dato que la documentación no da, o infiere algo que no está escrito | Un hueco de esta documentación, no un error del agente | Entrada de control de cambios del documento afectado de esta sección |

La tercera vía es la más específica de este producto y la que conviene tomarse en serio: si el agente de construcción tuvo que inferir una prohibición, la prohibición estaba mal escrita. La corrección se aplica acá, subiendo versión, y no se resuelve con una instrucción suelta en la conversación.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Rol de intervención developer: mantenedor presente, mantenedor futuro y agente de construcción por etapas (§1.1). Deriva de `PRODUCT-INTAKE` §15 y de `Vision-Producto.md` §2.2 (concentración de roles en una persona) |
| Superficie pública que documenta | El ensamblado de contratos completo: las siete familias de tipos de transferencia de `CU-01` a `CU-05`, `CU-07` y `CU-08`, y el tipo único de error de `CU-06` |
| CU origen | `CU-01` a `CU-08` de `02-Especificacion-Funcional/`, con sus §17 de compatibilidad de versión pública |
| Reglas de negocio relevantes | Ninguna en este proyecto de código; ver `Especificacion-Funcional.md` §5. Rigen en su lugar las restricciones transversales `RT-01` a `RT-09` §6 y la regla de arquitectura RA-03 de `PRODUCT-INTAKE` §14. Las reglas del circuito de revisión, `RN-10` y `RN-11`, viven en `GeometriaFactory-Domain` |
| Wireframes asociados | N/A. Variante DX sin UI final; el mínimo de la regla para `library` es cero |
| US a generar en 06 | US-01 a US-20, previstas en `Especificacion-Funcional.md` §4. Esta categoría aporta criterios de ergonomía de superficie a US-02, US-05, US-08, US-11, US-14, US-15, US-16, US-17, US-18 y US-19 |
| Tests previstos en 08 | Inspección de superficie pública para `RT-01`, `RT-04` y `RT-09` —que el comentario y la observación no compartan ni un campo—; verificación de ausencia de referencia hacia el dominio para `RT-05`; verificación de que no exista transición de salida de un estado terminal (`RT-08`); cobertura del 100 % de tipos de transferencia por prueba de integración (`RT-07`) |
| Catálogo de diseño aplicado | N/A en variante DX: no hay superficie visual que herede tokens |
| Configuración dirigida por esquema aplicada | N/A. El ensamblado no tiene parámetros configurables |
| Primer arranque aplicado | N/A. No se despliega por instancia; es nivel 0 del orden topológico |
| Acceso de operador único aplicado | N/A. No declara identidad de operación; transporta la sesión de `CU-01`, no la gobierna |
| Identidad de versión aplicada | N/A en esta categoría. La identidad de versión del producto se sella en las superficies de `GeometriaFactory-Web` |
| Modelo UX-UI aplicado en la Fase B2 | N/A. `requiere_maqueta` es false |
| Validación visual de maqueta | N/A. `requiere_maqueta` es false |
| Línea de base emitida | N/A. `requiere_maqueta` es false |

## 9. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.1 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` 1.3 incorporó el circuito de revisión del administrador, 00 y 01 pasaron a 1.1 con `NB-09`, y la categoría 02 emitió `CU-07` y dos restricciones transversales nuevas. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **§1.2** suma la tercera regla que hay que entender antes de tocar el proyecto de código: el comentario del administrador y las observaciones del validador no comparten ni un campo (`RT-09`), y amplía la del listado con el comentario (`RT-04` en su enunciado nuevo). **§2** remite a las cinco preguntas del tramo de 30 minutos, que ahora incluyen la distinción entre comentario y observación. **§4** pasa a siete contratos de uso y nueve restricciones. **§5.1** declara el conjunto cerrado de catorce códigos y las tres señales que quedan fuera. **§8** actualiza superficie documentada, CU origen, restricciones, historias previstas y pruebas, con `RT-08` y `RT-09`. El quick-start, el plan de Diátaxis y el lazo de retroalimentación no cambian: no dependen del alcance funcional. **Corrección de la ronda 3 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r3.md`), absorbida en esta misma versión sin subir a 1.2 y sin snapshot nuevo, por `Master-Prompt.md` §5 y por el punto 5 de §8 del informe. H-07**: §2 y §6 seguían remitiendo a «los tres cambios de control» cuando ya son cuatro. Al corregir el conteo se corrigió también el **significado**, que era el riesgo real: el hito de 1 hora y la definición de TTFV enunciaban una clasificación **binaria** —compatible o incompatible— y el conjunto de control tiene desde esta versión **tres salidas**, porque dos de los cuatro cambios se rechazan aunque compilen y no son ni una cosa ni la otra. Las dos definiciones pasan a nombrar las tres salidas, y TTFV declara que se mide sobre los cuatro y no sobre una muestra, porque con menos deja de cubrirlas. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Emisión inicial. Declara el rol de intervención en sus tres figuras concretas —mantenedor presente, mantenedor futuro y agente de construcción por etapas—, la regla de exposición como primera cosa a entender, los tres tramos de onboarding con hito verificable, el quick-start íntegramente dentro del contenedor de desarrollo, el plan de Diátaxis sobre la cadena documental sin portal, la separación de los errores en clase de construcción y clase transportada, cuatro métricas DX con dos sustituciones declaradas y el lazo de retroalimentación colgado del punto de control de etapa. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: dos ocurrencias de «solución» a secas designando el agrupador de construcción, corregidas una por una a «solución de código» en §3.1 y en el comentario del bloque ejecutable de §3.2, según `Vocabulario-Rules.md` §4 R2 y sin sustitución global (§9.5). **H-09**: las tres referencias a la sección opcional de los contratos de uso pasan de §12 a §17 —cabecera, §4 y §8—, alineadas con la renumeración que AG-02 aplicó. **H-11**: la cuarta métrica de §6 suma el rótulo `[ASUNCIÓN]` que el intake §17.4 P.6 le pone al gate del 100 %, con el mismo tratamiento que `RT-07`. **Alineación con el upstream**: la cabecera suma `CU-01` §10, que es donde AG-02 dejó el fundamento de que la contraseña no establecida viaje como respuesta de error con código propio. | DX Lead (AG-03) |
| 1.2 | 2026-08-09 | **Actualización por contenido nuevo aguas arriba**: `PRODUCT-INTAKE` **1.7** incorpora la capacidad **F-26** —reseteo de contraseña por el administrador—, las reglas **RN-12** y **RN-13** y el invariante **INV-09**, y la categoría 02 emite **CU-08** y dos restricciones transversales nuevas. Los contratos de uso pasan de siete a **ocho** y las restricciones transversales de nueve a **once**, con `RT-10` —ninguna condición que impida operar viaja como campo de la respuesta de sesión— y `RT-11` —ningún tipo habilita a que el navegador invoque la API, que es **RA-01**—. §5.1 declara el conjunto cerrado de **dieciséis** códigos y registra que **las tres señales no cambian**. §8 actualiza superficie documentada y CU origen. El quick-start, el plan de Diátaxis y el lazo de retroalimentación no cambian: no dependen del alcance funcional. | DX Lead (AG-03) |
