# Criterios de validación — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Criterios-Validacion.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §11; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.6.P.6, §17.6.P.8, §17.6.P.10 y §22
**Trazabilidad downstream:** [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Criterios funcionales](#2-criterios-funcionales)
- [3. Criterios no funcionales](#3-criterios-no-funcionales)
- [4. Criterios de regresión y de deriva](#4-criterios-de-regresión-y-de-deriva)
- [5. Criterios de calidad de código](#5-criterios-de-calidad-de-código)
- [6. Excepciones documentadas](#6-excepciones-documentadas)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito

Define qué significa que `GeometriaFactory-Web` está **validado**. A diferencia de los proyectos de código de biblioteca del producto, éste **sí es una unidad de entrega**: se publica en el hosting público y es el único punto de contacto del navegador. Por eso «validado» acá quiere decir **que la etapa puede demostrarse a la persona y publicarse sin dejar la aplicación caída**.

El momento en que se aplican estos criterios es el **punto de control de cada etapa**, que el intake §15 declara bloqueante, y **el final del flujo de publicación**, que el intake §17.6.P.8 declara que no termina en la subida.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **diez** casos de uso tienen al menos un caso de verificación pasado, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **10 de 10** |
| CV-02 | Las **treinta** historias de usuario tienen su caso de verificación | Matriz §2, columna de historias | **30 de 30** |
| CV-03 | Las **trece** restricciones transversales tienen caso de verificación | Matriz §5 | **13 de 13** |
| CV-04 | Las **dieciséis** reglas de negocio tienen verificado **lo que esta pieza hace por ellas**, y ninguna afirmación depende de que esta pieza las haga cumplir | Matriz §4 | **16 de 16** |
| CV-05 | **Toda acotación se verificó forzando la solicitud sin pasar por la pantalla**, y no mirando que el control no se dibuja | `TC-01`, `TC-05`, `TC-07`, `TC-15`, `TC-25`, `TC-26` | **6 de 6** casos ejecutados sobre las acotaciones vigentes |
| CV-06 | Los **quince** códigos vivos del contrato **más** el camino de ausencia de respuesta tienen mensaje de superficie, y **ninguno** expone dirección, ruta de datos ni traza | `TC-31` | **16 de 16** mensajes, con **0** exposiciones |
| CV-07 | Los **ocho** escenarios del intake §20 están ejercitados **en su forma original y completa**, sin sustituirlos por datos sintéticos | `TC-11` a `TC-14`, `TC-17` a `TC-20`, verificados uno por uno en [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) §3 | **8 de 8** |
| CV-08 | El escenario `E-1` produce **exactamente 3 piezas y 2 advertencias**, y el cilindro **no produce ninguna observación** | `TC-13` | 3 y 2, con **0** observaciones del cilindro. **Una tercera advertencia significa que el operador de tolerancia dejó de ser estricto** |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8. El de los pasos del guion lleva su rótulo **[ASUNCIÓN]** porque así viene del intake en cuanto a su **forma de puerta**.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-09 | `PT-01.a`: el front publicado arranca y sirve la página inicial | **200** en la dirección pública | `TC-34` | **Puerta técnica**: si no pasa, se baja la versión objetivo del front |
| CV-10 | `PT-01.b`: transporte del circuito | Semáforo; **amarillo aceptable** documentando la latencia percibida | `TC-34` | **Puerta técnica**: sólo el rojo obliga a cambiar el modelo de front. **Un repliegue de mayor latencia no es motivo de rediseño** |
| CV-11 | `PT-01.c`: estabilidad del proceso | **20 minutos** continuos sin reciclado, con reconexión funcional | `TC-34` | **Puerta técnica**: es el peor escenario y **no tiene mitigación en el código** |
| CV-12 | `PT-01.d`: salida hacia el backend | Una llamada de salud devuelve **datos reales** | `TC-34` | **Puerta técnica**: si no pasa, publicar el servicio de datos en un puerto convencional |
| CV-13 | Pasos del guion de demostración de la etapa **y de todas las anteriores** | **100 %** **[ASUNCIÓN del intake §17.6.P.6 en cuanto a expresarlo como puerta; asunción `A-4` de §22, que declara que cambia la forma del gate y no su carácter bloqueante]** | `TC-35` | **Bloqueante.** Lo rotulado [ASUNCIÓN] es **la forma de la puerta**, y §22 declara que un cambio del Product Owner no toca su carácter. **La regla acumulativa rige igual**: no es asunción de nadie |
| CV-14 | Peticiones del navegador hacia el servicio de datos | **0**, medidas **con los dos movimientos prendidos** | `TC-29` | **Bloqueante, sin gradación**. Una medición hecha sin la condición **no cuenta como medición** |
| CV-15 | Salidas hacia el servicio de datos y bibliotecas de guion que consulten | **1** y **0** | `TC-30` | **Bloqueante** |
| CV-16 | Apariciones de la credencial de sesión en el navegador | **0** | `TC-03` | **Bloqueante**. Criterio de aceptación de la etapa `c` |
| CV-17 | Mensajes que exponen dirección, ruta de datos o traza | **0** sobre los quince códigos y el camino de ausencia | `TC-31` | **Bloqueante** |
| CV-18 | Tráfico de circuito durante la interacción con la escena | **0**, y el texto viaja **1** sola vez por trabajo | `TC-33` | **Bloqueante** |
| CV-19 | Instancias del visor no liberadas tras **10** recorridos, con los dos movimientos prendidos | **0** | `TC-21`, puerta `PT-02` | **Puerta técnica**: si no pasa, **detiene la planificación de la etapa `g`** y no se arrastra como deuda |
| CV-20 | Invocaciones al interior del bundle | **0**, con **6 de 6** funciones como única vía | `TC-32` | **Bloqueante** |
| CV-21 | Elementos de la línea de base demostrados | **11 de 11** superficies, **73 de 73** componentes, **74 de 74** estados, **24 de 24** rutas y **29 de 29** campos | Las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) | **Bloqueante al cierre de la etapa** para los elementos que la etapa toca |
| CV-22 | Advertencias de construcción | **0** | Etapa de construcción del flujo de publicación | **Bloqueante** |

**No hay criterio de cobertura de líneas ni de tiempo de respuesta, y las dos ausencias tienen fundamento declarado.** La primera, porque no hay proyecto de pruebas propio (intake §17.6.P.6). La segunda, porque las tolerancias de **400 ms** son de **diseño de la espera** y no compromisos de tiempo de respuesta (`05` §8 y `PA-04` de su §11). **Inventar cualquiera de las dos sería inventar una medición sin sujeto o un compromiso sobre un hosting cuya latencia la propia fuente declara incógnita.**

## 4. Criterios de regresión y de deriva

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-23 | El guion de demostración se ejecuta **entero y acumulativo** al cerrar cada etapa: la de la etapa y las de todas las anteriores, **sin correcciones** | 100 % de los pasos escritos hasta ese momento |
| CV-24 | **Ningún paso que pasaba en la etapa anterior deja de pasar** sin justificación escrita en el informe de cierre | 0 regresiones sin justificar |
| CV-25 | Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca quedan con **estado y fecha actualizados**, y ninguna vuelve a `Sin verificar` sin que algo se haya regenerado | 61 filas con estado coherente con lo construido |
| CV-26 | **Ninguna deriva mayor queda sin resolver.** Se corrige lo construido, o se actualiza la línea de base con aprobación humana explícita | 0 derivas mayores abiertas al cerrar la etapa |
| CV-27 | Toda deriva **menor** queda registrada aunque no bloquee | 100 % de las menores registradas |
| CV-28 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso de verificación por defecto cerrado, como mínimo |
| CV-29 | Las cinco inspecciones estructurales —`TC-29` a `TC-33`— se ejecutan en **todas** las etapas a partir de aquella en que su sujeto existe | Presentes en cada ejecución |

**La regla de no regresión es acumulativa por diseño y es la única red de seguridad que este proyecto de código tiene**, porque no tiene batería automatizada propia. El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, **sin correcciones**.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-30 | Cobertura de líneas | **No aplica**: no hay proyecto de pruebas propio (intake §17.6.P.6) | **No exigible.** Si en alguna etapa se agregan pruebas automatizadas de componentes, su umbral se fija en ese momento y se registra en [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 |
| CV-31 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-22` |
| CV-32 | Todo valor visual sale de un token del catálogo de diseño; no hay literales visuales ad hoc | 0 literales fuera del catálogo | **Bloqueante**, por la sonda `SD-54` |
| CV-33 | Ningún instrumento de la maqueta ni valor compuesto para la maqueta llega al sistema construido | 0 instrumentos y 0 valores | **Bloqueante, sin gradación**, por las sondas `SD-59` y `SD-60` |
| CV-34 | Recorrido completo por teclado, foco visible y contraste de **4.5:1** en las once superficies | 11 de 11 | **Bloqueante**, por las sondas `SD-51` y `SD-52` |
| CV-35 | Ninguna superficie invoca al cliente tipado: entre una superficie y la salida hay siempre un servicio de aplicación de front | 0 invocaciones directas | **Bloqueante**, por `TC-30`. Es lo que hizo posible la Fase B2 y lo que mantiene maquetable cada superficie |

## 6. Excepciones documentadas

**Un criterio no cumplido no se acepta en silencio.** Las cuatro únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| **Ningún criterio de este proyecto de código es condicionado.** `CV-13` lleva un valor rotulado [ASUNCIÓN], pero lo rotulado es **la forma de la puerta** y el intake §22 `A-4` declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante» | **No hay salida admitida**: `CV-13` no alcanzado **bloquea el cierre** como cualquier otro criterio bloqueante. Ejecutar sólo el guion de la etapa en curso no es una excepción admitida | El Product Owner, con constancia escrita, como en cualquier criterio bloqueante |
| Criterio **no exigible** —`CV-30`— | Se declara «no aplica» con el fundamento citado. **No se reporta un número inventado** | — |
| **Puerta técnica** que no pasa —`CV-09` a `CV-12`, `CV-19`— | **No hay excepción.** El intake §15 declara que una puerta que no pasa **detiene la planificación de las etapas que dependen de ella** y no se arrastra como deuda. La salida es la que cada puerta declara: bajar la versión objetivo del front, cambiar el modelo de front, publicar el servicio en un puerto convencional, o detener la etapa `g` | El Product Owner decide la salida, no la excepción |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita en el informe de cierre |

**Lo que no es una excepción admitida:** ejecutar el guion sólo de la etapa en curso; dar por verificada una acotación mirando que el control no se dibuja; dejar una deriva mayor en `Sin verificar` y seguir; contar peticiones del navegador **sin los dos movimientos prendidos**; sustituir un escenario del intake por un texto que dé el resultado esperado; publicar sin comprobar que la dirección pública responde.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** `CV-13` pasa de **condicionado** a **bloqueante**, y la salida admitida correspondiente de §6 se reemplaza por la declaración de que **ningún criterio de este proyecto de código es condicionado**: lo rotulado [ASUNCIÓN] es la forma de la puerta, y §22 `A-4` deja a salvo su carácter. **El umbral del 100 % acumulativo no cambia.** Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **treinta y cinco** criterios de validación numerados `CV-01` a `CV-35`, repartidos en funcionales, no funcionales, de regresión y deriva, y de calidad de código, cada uno con su umbral y su forma de medición. Distingue cuatro caracteres —bloqueante, condicionado, **puerta técnica sin excepción posible** y no exigible— y ata el único condicionado al valor rotulado **[ASUNCIÓN]** del intake §22, precisando que lo rotulado es la **forma de la puerta** y no la regla acumulativa. Declara que no hay criterio de cobertura de líneas ni de tiempo de respuesta, con el fundamento de cada ausencia. Incorpora los criterios de deriva sobre las **61** filas de la matriz de sensado y el criterio de que **toda acotación se verifica forzando la solicitud**. Declara las cuatro salidas admitidas y seis situaciones que explícitamente no lo son. |
