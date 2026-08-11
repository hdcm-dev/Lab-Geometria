# Estrategia de calidad — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Estrategia-Calidad.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §2, §3 y §6; [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §9 y §11; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**, emitida en la Fase B2; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.6.P.6, §17.6.P.8, §17.6.P.10 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`, que materializa como etapas del flujo de publicación los quality gates de §3; `11-Documentacion`, que cita esta estrategia sin redefinirla

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Ningún gate de este proyecto de código queda condicionado](#31-ningún-gate-de-este-proyecto-de-código-queda-condicionado)
  - [3.2 Las tres puertas técnicas que alcanzan a este proyecto de código](#32-las-tres-puertas-técnicas-que-alcanzan-a-este-proyecto-de-código)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Web` tiene calidad cuando **las tres reglas de arquitectura del producto se sostienen desde acá y son verificables en un punto observable cada una**, cuando **lo construido no se aparta de la línea de base visual que el Product Owner aprobó**, y cuando **ninguna interrupción del servicio de datos ni del circuito deja una pantalla rota**.

Las tres partes no son intercambiables. La primera es la razón de ser de la topología entera: `RA-01` sólo se puede violar desde acá, porque éste es el único proyecto de código que sirve el navegador ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §10.4). La segunda tiene instrumento propio y ya emitido: [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), con sus **61** filas. La tercera es la única necesidad de negocio que este proyecto de código sostiene y ningún otro puede sostener del lado de la persona.

**Lo que esta definición deliberadamente no dice es «que las reglas de negocio se cumplan».** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5 declara que esta pieza **no hace cumplir ninguna**: ocultar un control, no armar una ruta o no ofrecer una acción **acotan lo que se ofrece y no hacen cumplir nada**. La consecuencia para esta categoría es directa y está en §3: toda acotación se verifica **forzando la solicitud sin pasar por la pantalla**, y no mirando la pantalla.

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. El valor rotulado **[ASUNCIÓN]** viene así desde el intake y **su forma no es un compromiso**: se usa como vigente hasta que el Product Owner lo confirme (§22 del intake, asunción `A-4`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica** | **0** apariciones de la credencial de sesión en el navegador, verificable con las herramientas de desarrollo (`05` §8; criterio de aceptación de la etapa `c`); **0** peticiones del navegador hacia el servicio de datos; **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **quince** códigos vivos del contrato **y** sobre el camino de ausencia de respuesta |
| Adecuación funcional | **Crítica** | **100 %** de los pasos del guion de demostración de la etapa **y de todas las anteriores** [ASUNCIÓN del intake §17.6.P.6 en cuanto a expresarlo como puerta; la regla acumulativa es de la fuente]; **10 de 10** casos de uso con verificación |
| Fiabilidad | **Crítica** | **0** instancias del visor no liberadas tras **10** recorridos de ida y vuelta (`PT-02`); el estado degradado y la reconexión como **dos tramos** distintos; el listado vacío distinguido del fallo **por el tipo recibido y no por el conteo** (`RT-07`) |
| Usabilidad | **Alta** | **11 de 11** superficies, **73 de 73** componentes, **74 de 74** estados y **24 de 24** rutas de la línea de base visual aprobada, sensados por las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); contraste de **4.5:1** y recorrido completo por teclado en las once superficies |
| Compatibilidad | **Alta** | `PT-01` en sus **cuatro** partes, que el intake §17.6.P.10 declara como los NFR de este proyecto de código; capacidad gráfica tridimensional requerida **por capacidad y no por número de versión**, con el resto del producto disponible sin ella (`RT-11`) |
| Eficiencia de desempeño | **Media** | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viajando **una sola vez por trabajo**. **No hay umbral de tiempo de respuesta**, y esta categoría no lo inventa: ver §3 y `PA-04` de `05` §11 |
| Mantenibilidad | **Alta** | **1** sola salida hacia el servicio de datos y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta; **0** invocaciones al interior del bundle, con las **6** funciones de la fachada como única vía; **0** advertencias de construcción |
| Portabilidad | **Media** | Servidor: el hosting público, con su versión de plataforma **[A VERIFICAR]** en la fuente y medida por `PT-01.a`. Navegador: cualquiera con capacidad gráfica tridimensional y circuito, persistente o replegado |

**El atributo que este proyecto de código no puede delegar es la seguridad de la topología.** No porque maneje secretos —la clave de firma es de `GeometriaFactory-Infrastructure`— sino porque es el único punto de contacto del navegador: si acá aparece una petición del navegador hacia el servicio de datos, la partición del producto deja de existir.

## 3. Quality gates

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los tres primeros los declara el intake §17.6.P.8; el cuarto, §17.6.P.6; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR. Las tres puertas técnicas van aparte, en §3.2.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | La construcción termina **sin advertencias** | Etapa de construcción del flujo de publicación | **Bloquea la fusión** (intake §17.6.P.8) |
| QG-02 | El bundle del visor se genera **en el mismo flujo de publicación**, nunca se toma de un artefacto viejo | Inspección del flujo: el paso de generación precede al de publicación y no hay artefacto cacheado | Bloquea la publicación (intake §17.6.P.8) |
| QG-03 | El flujo **no termina en la subida**: termina comprobando que la dirección pública responde | Comprobación al final del flujo de publicación | **Bloquea el flujo.** El intake §17.6.P.8 declara que «una subida **por FTP** que deja la aplicación caída y se reporta como exitosa es peor que una falla visible» |
| QG-04 | **100 %** de los pasos del guion de demostración de la etapa **y de todas las anteriores** se ejecutan y pasan antes del punto de control **[ASUNCIÓN del intake §17.6.P.6 en cuanto a expresarlo como gate; sobre la forma, no sobre el carácter]** | Ejecución del guion en el navegador del equipo anfitrión (`TC-35`) | **Bloquea el punto de control, y no es condicionado.** Lo sujeto a confirmación es **la forma**, ver §3.1 |
| QG-05 | **0** peticiones del navegador hacia el servicio de datos, contadas durante un recorrido completo **con los dos movimientos automáticos prendidos** | `TC-29`, conteo en la pestaña de red | Bloquea la fusión. Es `RA-01`, la regla que sostiene la topología |
| QG-06 | **1** sola salida hacia el servicio de datos —el cliente tipado— y **0** bibliotecas de guion agregadas que consulten servicios por su cuenta | `TC-30`, inspección del árbol de fuentes y de las dependencias de guion | Bloquea la fusión |
| QG-07 | **0** apariciones de la credencial de sesión en el navegador | `TC-03`, inspección del almacenamiento, de las marcas de sesión y del contenido servido | Bloquea la fusión. Es criterio de aceptación de la etapa `c` |
| QG-08 | **0** mensajes que expongan dirección de servicio, ruta de datos o traza, sobre los **quince** códigos vivos **y** sobre el camino de ausencia de respuesta | `TC-31`, inspección del traductor de condiciones, que es el único lugar por el que un mensaje llega a la persona | Bloquea la fusión. Es `RA-03` |
| QG-09 | **0** invocaciones al interior del bundle: las **6** funciones de la fachada son la única vía y hay **0** accesos al elemento de dibujo fuera del anfitrión | `TC-32`, inspección del árbol de fuentes | Bloquea la fusión. Es `RA-02` sostenida desde este lado |
| QG-10 | **0** tráfico de circuito durante la interacción con la escena, y el texto del trabajo viaja **una sola vez por trabajo** | `TC-33`, conteo en la pestaña de red mientras se rota y se acerca | Bloquea la fusión |
| QG-11 | Las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, con estado y fecha, y **ninguna deriva mayor queda sin resolver** | Recorrido de la matriz al cerrar la etapa | Bloquea el cierre de la etapa. Una deriva mayor se resuelve corrigiendo lo construido o actualizando la línea de base con aprobación humana, **nunca por omisión** |

**Once gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8. **No se declara ningún gate de cobertura de líneas**, y el motivo lo da la fuente: este proyecto de código **no tiene proyecto de pruebas propio** en el árbol del repositorio (intake §17.6.P.6). Inventarle un umbral de cobertura sería inventar una medición sin sujeto.

**Tampoco se declara ningún gate de tiempo de respuesta.** `05` §8 lo declara expresamente: las tolerancias de **400 ms** de [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md) §7 son de **diseño de la espera** —dicen a partir de cuándo se muestra un indicador— y no compromisos de tiempo de respuesta. Esta categoría hereda esa distinción y no la convierte en umbral. Queda como `PA-04` de `05` §11.

### 3.1 Ningún gate de este proyecto de código queda condicionado

**Los once gates de §3 bloquean** —cada uno lo que su columna de consecuencia declara: la fusión, la publicación, el flujo, el punto de control o el cierre de la etapa— **y ninguno es condicionado**. El único que lleva un valor rotulado **[ASUNCIÓN]** es `QG-04`, y **no por eso queda condicionado**.

El intake §17.6.P.6 lo escribe así: **«Gate bloqueante y numérico en lugar de cobertura de líneas: el 100 % de los pasos del guion de demostración de la etapa y de todas las anteriores se ejecuta y pasa antes del punto de control»**, con el rótulo **[ASUNCIÓN en cuanto a expresarlo como gate; la regla acumulativa es de RF §9.4]**. Y el intake §22, fila `A-4`, columna «Si el Product Owner la cambia», dice: **«Cambia la forma del gate, no su carácter bloqueante»**.

Las dos fuentes dicen lo mismo y dicen exactamente qué está en duda: **cómo se expresa la puerta**, no si detiene. **La regla acumulativa es de la fuente y no está en duda**, y el carácter bloqueante tampoco. Condicionar `QG-04` habría suspendido justamente lo que la fuente puso a salvo, en el único proyecto de código del producto **sin batería automatizada propia**: sería la diferencia entre que el guion acumulativo detenga un punto de control o no lo detenga.

**Qué se hace con la asunción, entonces.** El valor y la forma se usan como vigentes y **la puerta se materializa en `09-Devops` como bloqueante desde la primera etapa que la alcanza**. Si el Product Owner cambia la forma —otro umbral, otro instrumento de medición—, cambia la condición que se mide y el gate **sigue bloqueando**. En particular, nada de esto habilita a ejecutar el guion de la etapa sin los de las anteriores: eso es la regla de no-regresión del intake §15 y de RF §9.4, que no es asunción de nadie.

### 3.2 Las tres puertas técnicas que alcanzan a este proyecto de código

Se declaran aparte de los gates porque su consecuencia es distinta: el intake §15 declara que **una puerta que no pasa detiene la planificación de las etapas que dependen de ella y no se arrastra como deuda**.

| Puerta | Qué mide | Dónde se mide | Qué condiciona |
| --- | --- | --- | --- |
| `PT-01`, en sus **cuatro** partes | Arranque en la dirección pública, transporte del circuito, estabilidad del proceso durante **20 minutos** y salida hacia el servicio de datos | Etapa `a`, **antes que cualquier otra cosa** | El modelo de front entero. **Sólo el rojo en el transporte o la falla de estabilidad obligan a cambiarlo**; un repliegue de mayor latencia **no es motivo de rediseño** |
| `PT-02` | Que el visor funcione embebido: el bundle **carga en una página del anfitrión**, la escena se crea, las tres figuras de `E-1` se dibujan —ortoedro incluido—, **navegar y volver 10 veces no degrada** —**0** instancias no liberadas, medidas **con los dos movimientos prendidos**— y **el árbol y la escena se sincronizan por índice** (intake §17.7.P.8) | Antes de comprometer la etapa `g` | La etapa `g` entera |
| `PT-03` | Que el **motor de dibujo quede dentro del bundle** y la página **funcione sin acceso a CDN externos** (intake §17.7.P.8) | Antes de comprometer la etapa `g` | La etapa `g` entera. **No tiene caso de verificación propio acá**: es propiedad del bundle y se verifica del lado de `GeometriaFactory-Visor` |

**Los umbrales de las tres puertas no son asunciones**, y el intake §22 lo declara expresamente: los 20 minutos de `PT-01.c`, el semáforo de `PT-01.b` y los umbrales de las cinco puertas técnicas «están declarados en las fuentes y se transcriben sin cambio». Esta categoría los transcribe y no los mueve.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2): la misma persona diseña las verificaciones, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de verificación, mantener la matriz de cobertura, **resolver el método de verificación de las 61 filas de la matriz de sensado** y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, confirmar el valor rotulado [ASUNCIÓN] y **decidir ante toda deriva mayor**: se corrige lo construido o se actualiza la línea de base |
| Revisión mecánica | El flujo de publicación | Los gates `QG-01`, `QG-02`, `QG-03` y las mediciones automatizables de §3 |
| Verificación observada | La persona, en el navegador del equipo anfitrión | El guion de demostración y las filas de la matriz de sensado cuyo método es inspección visual. **No todo acá se automatiza, y decirlo es más honesto que declarar una automatización que no existe** |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2). Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) y qué filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) entran en alcance | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera y **las filas de la matriz de sensado que la etapa tocó**, con estado y fecha | Matriz actualizada, filas sensadas y la constancia de los gates medidos |
| Antes de comprometer la etapa `g` | `PT-02` y `PT-03` | La medición de las dos puertas, o la detención de la planificación de `g` |
| Ante toda deriva mayor | Si se corrige lo construido o se actualiza la línea de base | La decisión del Product Owner, con constancia escrita. **Nunca se resuelve por omisión** |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa. **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

**Una precisión sobre la matriz de sensado.** Su §1 declara como cuarto momento el «cierre de cada sprint de codificación». Este documento lo lee como **cierre de cada etapa**, que es la unidad que el producto tiene, y no cambia el texto de la matriz: la palabra «sprint» de ese documento es de la mecánica genérica de `Deriva-Rules.md`, no una unidad de planificación de este producto.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** `QG-04` estaba **condicionado**, y no correspondía: el intake §17.6.P.6 lo escribe como «**gate bloqueante y numérico** en lugar de cobertura de líneas», con el rótulo [ASUNCIÓN] alcanzando sólo a **expresarlo como gate**, y §22 `A-4` declara que un cambio del Product Owner «cambia la forma del gate, **no su carácter bloqueante**». Vuelve a **bloqueante**, con la forma sujeta a confirmación; §3.1 se reescribe entera y declara que **ningún gate de este proyecto de código queda condicionado**. **`H-03`.** Las filas de `PT-02` y `PT-03` de §3.2 tenían **el contenido cruzado** respecto del intake §17.7.P.8: se reescriben las dos con la definición de la fuente. **`H-09`.** La cita de `QG-03` restituye «una subida **por FTP** que deja la aplicación caída…». Ningún umbral, caso ni decisión de prueba cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad en sus tres partes —las tres reglas de arquitectura sostenidas desde acá, la línea de base visual no derivada y ninguna pantalla rota—, con la constancia explícita de que este proyecto de código **no hace cumplir reglas de negocio** y de que toda acotación se verifica forzando la solicitud. Declara los ocho atributos ISO 25010 con su métrica de origen, los **once** quality gates con condición, verificación y consecuencia —uno condicionado por el rótulo [ASUNCIÓN] del intake §22 sobre la **forma** de la puerta y no sobre la regla acumulativa—, las **tres** puertas técnicas que alcanzan a este proyecto de código con la constancia de que sus umbrales **no son asunciones**, el reparto de papeles con la verificación observada declarada como tal, y la cadencia por etapa, incluida la lectura de «sprint» de la matriz de sensado como etapa. **No declara ningún gate de cobertura de líneas ni de tiempo de respuesta**, con el fundamento de cada ausencia. |
