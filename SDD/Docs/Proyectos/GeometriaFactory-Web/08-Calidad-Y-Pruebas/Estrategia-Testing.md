# Estrategia de testing — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Estrategia-Testing.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §5, §6 y §7; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §3.2, §4 y §8; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**; [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md) y [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §17.6.P.6, §20 (los **ocho** escenarios `E-1` a `E-8`), §21 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md); `09-Devops` y `11-Documentacion`

---

## Tabla de contenido

- [1. Pirámide de testing deseada](#1-pirámide-de-testing-deseada)
- [2. Cobertura mínima por capa](#2-cobertura-mínima-por-capa)
- [3. Tooling](#3-tooling)
- [4. Especificaciones Given-When-Then](#4-especificaciones-given-when-then)
- [5. Mocks y fixtures](#5-mocks-y-fixtures)
- [6. Datos de prueba](#6-datos-de-prueba)
- [7. Ambiente de testing](#7-ambiente-de-testing)
- [8. Relación con la matriz de sensado de deriva ya emitida](#8-relación-con-la-matriz-de-sensado-de-deriva-ya-emitida)
  - [8.1 Resolución del método de verificación por familia de filas](#81-resolución-del-método-de-verificación-por-familia-de-filas)
  - [8.2 Correspondencia con la matriz de `GeometriaFactory-Visor`](#82-correspondencia-con-la-matriz-de-geometriafactory-visor)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Pirámide de testing deseada

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `web-monolith` la distribución **70 / 20 / 10** entre unitario, integración y extremo a extremo. Este proyecto de código **se aparta del reparto y declara el motivo**, que no es de esta categoría sino del intake: §17.6.P.6 declara que **no tiene proyecto de pruebas propio** en el árbol del repositorio y que su verificación es **el guion de demostración de cada etapa**, ejecutado en el navegador del equipo anfitrión y acumulativo por la regla de no-regresión, más las pruebas de integración que ejercitan el servicio que consume.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | — | **0 %** | **No aplica hoy, y se declara así en lugar de omitirse.** No hay proyecto de pruebas propio. El intake §17.6.P.6 lo deja abierto en una sola dirección: «si en alguna etapa se agregan pruebas automatizadas de componentes, su cobertura mínima se fija en ese momento y se registra». Hasta que eso ocurra, un porcentaje unitario sería una medición sin sujeto |
| Integración | Las pruebas que ejercitan el servicio de datos que esta pieza consume | **0 % acá** | Existen y son necesarias, pero **son de `GeometriaFactory-Api`**: `GeometriaFactory.Integration.Tests` pertenece a ese proyecto de código (intake §17.5.P.6). Esta pieza las consume como contexto y no las posee |
| Extremo a extremo observado | **El guion de demostración**, acumulativo, ejecutado en el navegador; las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md); y las **tres** puertas técnicas | **100 %** | Es lo que la fuente declara como verificación de este proyecto de código, y es lo único que puede observar lo que la persona observa |

**El apartamiento es de forma y no de rigor, y tiene una consecuencia que conviene decir sin adornos.** Un guion observado es más caro de ejecutar y menos reproducible que una batería automatizada: por eso esta estrategia lo compensa con **dos instrumentos que sí son enumerables y verificables uno por uno** —las 61 filas de la matriz de sensado y los **35** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md)—, y con **seis inspecciones estructurales** cuyo umbral es exactamente cero y que no dependen de que alguien mire bien.

**Contra la pirámide invertida**: acá la pirámide **es** de extremo a extremo, y no por descuido sino por decisión de la fuente. Lo que la mantiene sana es que las propiedades críticas —`RA-01`, `RA-02`, `RA-03`, la credencial fuera del navegador— **no se verifican mirando la pantalla** sino contando y forzando: `TC-29` a `TC-33`. **Contra la pirámide aplanada**: §2 no reporta ningún número global de cobertura, porque no hay ninguno que reportar.

**Tres clases de verificación que conviene nombrar aparte:**

- **Paso de guion.** Una acción de la persona en el navegador, con su resultado observable. Se ejecuta acumulativamente: el guion de la etapa **y los de todas las anteriores**.
- **Inspección estructural.** Comprueba una propiedad del árbol de fuentes o del tráfico observado, con umbral cero: peticiones del navegador, salidas hacia el servicio de datos, invocaciones al interior del bundle, mensajes que exponen la topología.
- **Verificación forzando la solicitud.** La que comprueba una acotación **sin pasar por la pantalla**. Es obligatoria porque esta pieza **no hace cumplir reglas** (`02` §5): que un control no se dibuje no prueba nada, y la Definition of Ready §1 criterio 7 lo exige explícitamente.

## 2. Cobertura mínima por capa

**No hay umbral de cobertura de líneas, y no es una omisión.** El intake §17.6.P.6 declara un **«gate bloqueante y numérico en lugar de cobertura de líneas»**, y el intake §22 lo rotula como asunción `A-4` en cuanto a su forma. Lo que sí se cubre, y se cuenta, es esto:

| Dimensión | Unidad de cobertura | Umbral | Fuente |
| --- | --- | --- | --- |
| Pasos del guion de demostración | Paso ejecutado y pasado | **100 %** de la etapa y de todas las anteriores [ASUNCIÓN del intake §17.6.P.6 en cuanto a la forma de la puerta] | Intake §17.6.P.6; `05` §8 |
| Superficies de la línea de base | Superficie sensada | **11 de 11** | [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) §4, filas `SD-01` a `SD-11` |
| Componentes de la línea de base | Componente sensado | **73 de 73** | Ídem, tabla de cobertura de §4 |
| Estados de la línea de base | Estado sensado | **74 de 74** | Ídem |
| Rutas de la línea de base | Ruta sensada | **24 de 24** | Ídem |
| Campos del contrato de datos de maqueta | Campo sensado | **29 de 29** | Ídem |
| Casos de uso | Caso de uso con verificación | **10 de 10** | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 |
| Restricciones transversales | Restricción con verificación | **13 de 13** | Matriz §5 |
| Códigos vivos del contrato | Código traducido a presentación **sin exponer nada** | **15 de 15** | Matriz §3, `TC-31` |
| Superficies con recorrido por teclado y contraste medido | Superficie | **11 de 11** | `SD-51` y `SD-52` de la matriz de sensado |

**Los tres componentes de capa 1, los tres de capa 2 y los dos de capa 3 de `05` §3.1 no llevan umbral numérico propio**, y decirlo es más honesto que repartir un porcentaje inventado entre ocho módulos que ninguna herramienta va a medir. Lo que sí tiene cada componente es **al menos un caso de verificación que lo ejerce**, y eso sí se declara en [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6.

**Si en alguna etapa se agregan pruebas automatizadas de componentes**, su cobertura mínima se fija en ese momento y se registra acá con su fila de control de cambios. Es la única puerta que el intake §17.6.P.6 deja abierta, y esta estrategia la deja abierta igual, sin anticipar un número.

## 3. Tooling

Se nombran por función y no por producto. La elección concreta y su anclaje de versión son de la etapa `a` (intake, regla de anclaje de versiones), y la biblioteca de componentes de interfaz está explícitamente **[A VERIFICAR]** en la fuente (`05` §11 `PA-01`).

| Propósito | Herramienta, por su función |
| --- | --- |
| Ejecución del guion de demostración | El navegador del equipo anfitrión, con su panel de herramientas de desarrollo abierto. **No es un marco: es una persona ejecutando pasos**, y declararlo así evita que se lea como automatización |
| Conteo de peticiones y de tráfico de circuito | Pestaña de red del panel de herramientas de desarrollo |
| Inspección del almacenamiento y de las marcas de sesión | Panel de aplicación del navegador |
| Inspección estructural del árbol de fuentes | Búsqueda sobre el repositorio y revisión del archivo de proyecto y del manifiesto de dependencias de guion |
| Recorrido por teclado y lectura asistida | Navegación por teclado sin ratón, y un lector de pantalla del sistema |
| Medición de contraste | Herramienta de medición de contraste sobre los pares de color del catálogo de diseño |
| Verificación forzando la solicitud | Un cliente de peticiones que arma la solicitud **sin pasar por la pantalla**, contra el servicio de datos, con la credencial de una sesión válida |
| Comprobación de la dirección pública | El paso final del flujo de publicación, que verifica que la dirección responde |

**No se nombra ningún producto comercial.** La biblioteca de componentes es la única pieza que la fuente nombra, y su versión es un punto abierto declarado.

## 4. Especificaciones Given-When-Then

**Los criterios de aceptación de las treinta historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos dos escenarios ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio.

**Y hay una razón adicional propia de este proyecto de código**: los pasos del guion de demostración ya son, en la práctica, especificaciones ejecutables observadas. Duplicarlos en un formato de escenario produciría **tres** enunciados del mismo criterio —la historia, el guion y el archivo de escenario— y ninguno sería la fuente.

## 5. Mocks y fixtures

**Política de dobles: ninguno del lado del servicio de datos, a partir de la etapa `c`.** El guion de demostración se ejecuta contra el servicio de datos real levantado en el contenedor de desarrollo, porque lo que verifica es el recorrido completo de la persona. Un doble del servicio de datos convertiría el guion en una demostración de la maqueta.

**Lo que sí se sustituye, y es la única sustitución admitida:**

| Sustitución | Cuándo | Por qué |
| --- | --- | --- |
| **Servicio de datos caído** | `TC-27`, `TC-28`, y las filas `SD-14` de la matriz de sensado | El estado degradado y la reconexión **no se pueden observar** con el servicio disponible. Se provoca deteniendo el servicio, no simulándolo |
| **Circuito cortado y restablecido** | `TC-27`, y `PT-01.c` | Ídem: se provoca cortando la red del navegador |
| **Preferencia de movimiento reducido declarada por el sistema** | `TC-22`, y `SD-48` | Se declara en el sistema operativo o en el navegador. **No se simula desde el código**, porque lo que se verifica es que esta pieza la **lea** y la traduzca a dos valores de verdad |
| **Sin capacidad gráfica tridimensional** | `TC-23`, y `RT-11` | Se deshabilita la capacidad en el navegador, para verificar que el resto del producto sigue disponible |

**Los datos de las etapas `b` y anteriores** salen de [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md), y ese documento declara en su §5 **valores compuestos para la maqueta que no se propagan al producto** —la credencial de prueba y la cuarta cuenta de ejemplo—. La fila `SD-60` de la matriz de sensado los sensa: **un dato compuesto para la maqueta que figure como dato del producto es deriva mayor**.

## 6. Datos de prueba

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado. §21 los cruza contra la batería obligatoria de **nueve** casos de prueba de RT §11, más un décimo que esa misma sección agrega.

**Cómo los usa este proyecto de código.** Acá los escenarios entran **como texto que la persona pega en el formulario de envío**, que es exactamente la forma en que el alumno los produce. Es el único proyecto de código del producto donde el escenario se usa en su forma original y completa, carácter por carácter: los demás reciben su interpretación ya producida.

| Escenario | Qué verifica en esta pieza | Fuente del valor |
| --- | --- | --- |
| `E-1` | **3 piezas y 2 advertencias**; las tres figuras se dibujan en la escena; procesar dos veces produce la **misma disposición**. Es el texto semilla y el caso canónico | §20.E-1, «Qué verificar» puntos 5 y 7; `SD-37`, `SD-38`, `SD-41` |
| `E-2` | **El texto se envía carácter por carácter con sus dos comas finales**, y se muestra sin reescribirlo. Es el material de `RN-08` y de la fila `SD-36` | §20.E-2, punto 1; `SD-36` |
| `E-3` | La advertencia de área con el par **36.00 declarado y 54.00 derivado**, mostrada **exactamente como llega, sin reformatear** | §20.E-3, punto 2; `SD-33` |
| `E-4` | **Cero observaciones**: la lista de observaciones se dibuja como línea explícita y no como hueco | §20.E-4, punto 4; `SD-21` |
| `E-5` | El error con **índice de figura 1** y **campo `Tipo`**, nunca un texto genérico; y **ninguna pieza desaparece sin quedar enumerada** | §20.E-5, puntos 1 a 4; `SD-30`, `SD-40` |
| `E-6` | La pieza con dimensión **`0.00` se dibuja** y no produce condición de dibujo | §20.E-6, puntos 1 y 4; `SD-39` |
| `E-7` | Los **seis** tipos dibujables, con la clave `Bases` en el ortoedro | §20.E-7, puntos 1 a 3; `SD-40` |
| `E-8` | La pieza no se dibuja y **queda enumerada con su índice y su código**; **el árbol muestra las dos piezas**, incluida la que no se dibujó; y el desenlace del envío **es error**: el trabajo queda en `Borrador` | §20.E-8, puntos 2, 5 y 6 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un dato de prueba de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización.

**Lo que no se inventa.** Ningún caso de verificación introduce un texto de figuras que no esté en §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un comentario del administrador— se usa un valor evidentemente ficticio y se declara como tal, con la salvedad de `SD-60`: **los valores compuestos para la maqueta no viajan al producto**.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre el guion | En el **navegador del equipo anfitrión**, contra la aplicación levantada desde el contenedor de desarrollo (intake §17.6.P.6) |
| Dónde corren las puertas | `PT-01` contra el **hosting público**, porque lo que mide son las capacidades de ese hosting y no las del contenedor. `PT-02` y `PT-03` en el navegador del equipo anfitrión |
| Servicio de datos | **Real y levantado**, no simulado, a partir de la etapa `c`. Su indisponibilidad se provoca deteniéndolo |
| Estado propio | **Ninguno que preparar.** `RT-06` declara que esta pieza no guarda estado propio: ni copia local, ni caché, ni réplica. No hay almacén que sembrar de este lado |
| Secretos | La dirección del servicio de datos viene de configuración y **la dirección real del servidor propio no se versiona**. Ningún secreto entra al repositorio |
| Aislamiento entre verificaciones | Cada paso del guion arranca desde una sesión conocida. Los pasos **sí tienen orden**, porque el guion es un recorrido; lo que no puede haber es dependencia de una ejecución anterior **de otra etapa** |
| Duración | **No se declara ningún tiempo de ejecución del guion ni de la suite.** Ninguna fuente lo da. Los únicos tiempos declarados son los **20 minutos** de `PT-01.c` —que es una duración de la prueba, no un plazo— y las tolerancias percibidas de 400 ms, que `05` §8 declara **de diseño de la espera y no compromisos de tiempo de respuesta** |

## 8. Relación con la matriz de sensado de deriva ya emitida

**[`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) ya existe en esta carpeta desde la Fase B2, en versión 1.2, y esta Fase E no la reemplaza, no la duplica y no la reescribe.** La emitió AG-03M al cerrar la Fase B2 con la maqueta aprobada por el Product Owner, y su propia §2 declara que era **el único artefacto de la categoría emitido por esa fase** y que «cuando AG-08 genere la categoría, incorpora esta matriz en lugar de crear una nueva». Esto es exactamente eso.

**Qué hace esta Fase E con ella, y qué no hace:**

| Acción | Estado |
| --- | --- |
| Incorporarla como artefacto vigente de la categoría, listado en [`README.md`](README.md) §1 | **Hecho** |
| Resolver el **método de verificación** de sus filas, que es lo que su §1 le asigna al cierre de la Fase E | **Hecho en §8.1**, por familia de filas y con el `TC-XX` que la ejerce |
| Exigir su actualización al cerrar cada etapa, con estado y fecha | **Hecho** en [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 y en [`Definition-Of-Done.md`](Definition-Of-Done.md) §1.3 |
| Convertirla en gate | **Hecho**: `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 |
| Modificar sus filas, sus umbrales o su recuento de línea de base, que sigue siendo **61** | **No se hace.** Un umbral de deriva se cambia con aprobación humana explícita, no desde esta categoría |
| Abrir filas nuevas para la capacidad **F-26** | **No se hace**, y el motivo lo declara la propia matriz en su §4: los elementos de interfaz que arrastra esa capacidad **no tienen identificador en la línea de base**, porque son posteriores a la aprobación de la maqueta. Sus sondas nacen con la **iteración 5** de maqueta y la reemisión de la línea de base. **Que no tengan sonda no significa que no se verifiquen**: `TC-06`, `TC-07` y `TC-10` de esta categoría las cubren contra los criterios de aceptación de `CU-03` y `CU-04`, que es lo que la propia matriz declara que gobierna esa construcción mientras tanto |
| Abrir filas `VER-XX` | **No se hizo en la Fase E, y quedó hecho el 2026-08-11 por AG-10**: en ese momento `10-Examples` no estaba emitida y la matriz lo declaraba. Al emitirse, la matriz sumó la fila **`SD-62`** desde el único contrato de verificación de [`../10-Examples/`](../10-Examples/), en `Sin verificar`. **El alta no es de esta categoría**: `Deriva-Rules.md` §4 se la asigna a AG-10 en el momento que cierra la categoría 10, y las **61** filas que esta categoría sí resolvió en §8.1 no cambian |

**La frontera entre los dos instrumentos, dicha una sola vez.** La matriz de sensado responde «¿lo construido se sigue pareciendo a lo que el humano aprobó mirando, y sigue respetando el contrato?». El catálogo de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) responde «¿el sistema hace lo que las historias dicen?». Cuando los dos miran lo mismo, la matriz aporta el **umbral de deriva** y el caso de prueba aporta el **criterio de aceptación**; ninguno de los dos reemplaza al otro y **ninguno redefine el umbral del otro**.

### 8.1 Resolución del método de verificación por familia de filas

Las **61** filas de la matriz agrupadas por su método resuelto. La agrupación es la que la propia matriz declara en su §3, y esta tabla **no cambia ninguna fila**: declara con qué se ejerce cada familia y en qué etapa entra.

| Familia | Filas | Cuántas | Método resuelto | Casos de verificación que la ejercen | Etapa en que entra |
| --- | --- | --- | --- | --- | --- |
| Superficies | `SD-01` a `SD-11` | 11 | **Inspección visual** contra la maqueta aprobada, superficie por superficie | `TC-35`, y los `TC-XX` de la superficie correspondiente | `b`, y se reverifica cuando la superficie recibe capacidad |
| Familias de estados | `SD-12` a `SD-22` | 11 | **Conmutación de estado** en el sistema construido, contra la tabla de la línea de base | `TC-04`, `TC-13`, `TC-14`, `TC-15`, `TC-24`, `TC-27`, `TC-28` | `c` a `h`, según la superficie |
| Familias de rutas | `SD-23` a `SD-27` | 5 | **Recorrido completo**, más revisión de la tabla de rutas del sistema construido para `SD-27` | `TC-05`, `TC-07`, `TC-26`, `TC-35` | `b` para el mapa, `c` a `h` para los recorridos |
| Modelo de datos y formatos | `SD-28` a `SD-36` | 9 | **Inspección visual con escenario del intake**, más comparación carácter por carácter para `SD-36` | `TC-11`, `TC-13`, `TC-14`, `TC-18` | `e` y `f` |
| Comportamiento verificable por ejecución | `SD-37` a `SD-42` | 6 | **Ejecución automatizable** con los escenarios `E-1`, `E-5`, `E-6` y `E-7`, y los **10** recorridos de ida y vuelta | `TC-13`, `TC-14`, `TC-17`, `TC-20`, `TC-21` | `f` y `g` |
| Contrato de fachada y movimiento | `SD-43` a `SD-48` | 6 | **Inspección del árbol de fuentes** más **conteo en la pestaña de red**, e inspección visual para los controles | `TC-22`, `TC-29`, `TC-32`, `TC-33` | `g` |
| Accesibilidad | `SD-49` a `SD-53` | 5 | **Recorrido por teclado**, **lectura asistida** y **medición de contraste**, las tres observadas | `TC-19`, `TC-23`, y revisión de accesibilidad de las once superficies | `b` para el armazón, `g` para el árbol y la escena |
| Tokens, ancho angosto y sello | `SD-54` a `SD-56` | 3 | **Revisión de las hojas de estilo** e **inspección visual** en ancho angosto | `TC-35` | `b` |
| Barridos de microcopy | `SD-57`, `SD-58` | 2 | **Barrido de los mensajes visibles**, con umbral 0 | `TC-31` | `c` |
| Barridos de residuos de maqueta y componentes transversales | `SD-59` a `SD-61` | 3 | **Barrido del sistema construido** buscando cada instrumento y cada valor compuesto, e inspección superficie por superficie para `SD-61` | `TC-35`, y la revisión de la etapa `b` | `b`, y se reverifica al cerrar `h` |

**Once más once más cinco más nueve más seis más seis más cinco más tres más dos más tres son sesenta y una.** El recuento cierra contra el de la matriz, y ninguna fila queda sin método resuelto ni sin etapa asignada.

**Cuántas quedan como inspección observada y cuántas se automatizan.** Las **seis** filas de `SD-37` a `SD-42` y las de conteo de `SD-43`, `SD-45` y `SD-47` admiten ejecución automatizable; **las demás quedan como inspección**, y decirlo es más honesto que declarar una automatización que este proyecto de código no tiene proyecto de pruebas donde alojar.

### 8.2 Correspondencia con la matriz de `GeometriaFactory-Visor`

`GeometriaFactory-Visor` emitió su propia matriz de sensado en su Fase E y declaró en su §4 una tabla de correspondencia contra ésta, «para que ningún elemento se sense dos veces con umbrales distintos». **Esta categoría la verificó fila por fila desde este lado y la confirma**: las **ocho** correspondencias que esa tabla declara son verdaderas contra el texto de las filas citadas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) 1.2.

| Fila de la matriz del `Visor` | Fila de ésta que la correspondencia cita | Verificación desde este lado |
| --- | --- | --- |
| `SD-01` seis funciones de la fachada | `SD-43` | **Verdadera.** `SD-43` afirma que la escena se opera **exclusivamente por las seis funciones** desde el componente anfitrión |
| `SD-02` cero red | `SD-43` | **Verdadera.** `SD-43` incluye el recuento de peticiones en la pestaña de red **durante la interacción con la escena** |
| `SD-03` cero persistencia | `SD-47` | **Verdadera.** `SD-47` afirma que **la preferencia de cada movimiento es del componente anfitrión** y que la fachada no escribe ninguna clave |
| `SD-06` sin fallo silencioso | `SD-39` y `SD-40` | **Verdadera.** `SD-39` sensa que la pieza con dimensión `0.00` **se dibuja**; `SD-40` sensa que el recuento de piezas sin registro es **0** |
| `SD-07` determinismo | `SD-41` y `SD-45` | **Verdadera.** `SD-41` compara dos cargas del mismo texto; `SD-45` compara las disposiciones en las **cuatro** combinaciones de movimiento |
| `SD-09` siete códigos en ocho cursos | `SD-18` | **Verdadera.** `SD-18` sensa los **ocho** estados que materializan las **siete** condiciones del contrato y que **usan los códigos sin renombrarlos** |
| `SD-11` gobierno del movimiento | `SD-44`, `SD-46` y `SD-48` | **Verdadera.** `SD-44` sensa los dos controles independientes; `SD-46`, la reposición de la orientación de partida; `SD-48`, el arranque destildado con preferencia de movimiento reducido declarada |
| `SD-12` puertas `PT-02` y `PT-03` | `SD-42` | **Verdadera en lo que afirma, y parcial en su alcance.** `SD-42` sensa los **diez recorridos de ida y vuelta sin degradación**, que es lo que la correspondencia le atribuye. **Lo que esa fila no cubre es la otra puerta de la correspondencia, `PT-03`** —que el motor de dibujo quede **dentro** del bundle y la página funcione **sin acceso a CDN externos**, que es como el intake §17.7.P.8 define `PT-03`—, que es propiedad del bundle y **se sensa sólo del lado del `Visor`**. La correspondencia no afirma lo contrario: dice «acá las dos puertas enteras, allá los diez recorridos» |

**No hay doble sensado con umbrales distintos.** Las filas de esta matriz se anclan en identificadores de línea de base **validados visualmente**; las de la matriz del `Visor` se anclan en elementos del **contrato de la fachada**. Cuando las dos miran lo mismo, lo miran desde lados distintos, y los umbrales que declaran son compatibles: **deriva mayor sin gradación en las dos** para cero red, cero persistencia, fallo silencioso, determinismo y las puertas técnicas.

**Un punto que conviene dejar escrito porque las dos matrices lo tratan.** La **sexta función**, `establecerMovimiento`, se incorporó al contrato **después** de que el Product Owner aprobó la maqueta y **no fue validada visualmente**; por eso `SD-43` de esta matriz la sensa **contra el contrato y no contra la maqueta**, y así lo dice su columna de umbral y la nota al pie de su §4. La matriz del `Visor` lo declara del mismo modo. **Las dos coinciden y ninguna afirma que la maqueta la haya validado.**

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Cierre del hueco de filas `VER-XX`** declarado en §8. La fila de la tabla de §8 que decía «no se hace: `10-Examples` no está emitida» se **conserva** con su desenlace y su fecha: la categoría 10 se emitió y AG-10 dio de alta la fila **`SD-62`** en [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.3**, desde el único contrato de verificación de esa categoría. Se precisa que el recuento **61** que esta categoría no modifica es el de filas de **línea de base**, y que `SD-62` no pertenece a ese conjunto. **Ninguna resolución de método de §8.1, ninguna correspondencia de §8.2, ningún umbral y ningún caso de prueba cambian.** Sube minor. |
| 1.1 | 2026-08-11 | **`H-03`.** La última fila de §8.2 atribuía a `PT-02` el contenido de `PT-03` en el párrafo que certifica la correspondencia con la matriz de sensado del `Visor`: lo que `SD-42` no cubre es **`PT-03`** —el motor de dibujo dentro del bundle y la página sin acceso a CDN—, que es como el intake §17.7.P.8 define esa puerta. **`H-02`.** §2 cita ahora §17.6.P.6 como «gate bloqueante y numérico». **Las ocho correspondencias siguen siendo verdaderas y ninguna se toca**; la matriz de sensado 1.2 no se modifica. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo con su apartamiento del reparto de `Rules-Calidad-Y-Pruebas.md` §2.2 y el motivo —el intake §17.6.P.6 declara que este proyecto de código **no tiene proyecto de pruebas propio** y que su verificación es el guion de demostración acumulativo—, con la consecuencia de ese apartamiento dicha sin adornos y los dos instrumentos enumerables que la compensan. Declara la cobertura por unidades contables en lugar de por líneas, el tooling nombrado por función, la política de una sola clase de sustitución admitida, el uso de los **ocho** escenarios del intake §20 **en su forma original y completa**, que es propio de este proyecto de código, y el ambiente. Su §8 declara la **relación con la matriz de sensado de deriva ya emitida en la Fase B2**: qué hace esta fase con ella y qué no, la **resolución del método de verificación de sus 61 filas por familia** con el recuento cerrado, y la **verificación desde este lado de las ocho correspondencias** que la matriz de `GeometriaFactory-Visor` declara, todas confirmadas, con la precisión de alcance de la última. |
