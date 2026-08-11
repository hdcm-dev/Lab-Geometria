# Product Backlog — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Product-Backlog.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §3 (los **siete** casos de uso), §5.1 (matriz con las historias previstas), §5.3 (cobertura de las nueve necesidades) y §6 (las **seis** propiedades transversales con sus condiciones de medición); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) (las **siete** garantías y los **siete** códigos de condición); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **seis** componentes), §8 (los **ocho** NFR) y §11 (los **cinco** puntos abiertos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4 (capacidades `F-11`, `F-13` y `F-25`), §15 (etapas y puertas técnicas `PT-02` y `PT-03`), §16.1 y §18 (sample `S-1`), §17.7 (P.1 a P.12) y §20 (`E-1` y `E-7`); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §3, §4 y §5; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3 y §4
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `10-Examples` de GeometriaFactory-Visor

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 0 para este backlog](#11-qué-significa-nivel-topológico-0-para-este-backlog)
  - [1.2 Qué es una historia en un visualizador puro](#12-qué-es-una-historia-en-un-visualizador-puro)
- [2. Épicas](#2-épicas)
  - [2.1 Por qué EP-02 no es una etapa nueva](#21-por-qué-ep-02-no-es-una-etapa-nueva)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 Índice de historias](#31-índice-de-historias)
  - [3.2 EP-02 · Medición de las puertas técnicas del visor](#32-ep-02--medición-de-las-puertas-técnicas-del-visor)
  - [3.3 EP-03 · Visualización del trabajo](#33-ep-03--visualización-del-trabajo)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **siete** contratos de uso de `GeometriaFactory-Visor`, que produce el archivo de guion del visualizador tridimensional del producto y cuya fachada es **el punto de extensión declarado del producto** (`PRODUCT-INTAKE` §18).

**El MVP no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). Todo lo de este backlog cae dentro de ese tramo: no hay ninguna historia de la fase `i…`.

**Este backlog no reordena las etapas ni las renombra.** Las tres épicas de §2 se apoyan en las etapas del roadmap y en el **momento de medición** que su §2.2 declara para las dos puertas técnicas de este proyecto de código.

### 1.1 Qué significa nivel topológico 0 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Visor` en el **nivel 0**. Sus consecuencias acá son distintas de las de los otros dos proyectos de código del mismo nivel:

1. **Ninguna historia espera a otro proyecto de código**, y en este caso la independencia es más fuerte que en los otros dos: el bundle **se ejercita sin backend**, con un texto pegado a mano, y eso es una propiedad exigida y no una conveniencia (`PRODUCT-INTAKE` §16.1 y §17.7.P.6).
2. **Su trabajo condiciona el de `GeometriaFactory-Web`**, que lo empaqueta en su directorio de recursos estáticos y que aloja el componente anfitrión.
3. **Su trabajo se puede empezar mucho antes de la etapa en la que se integra**, y el propio roadmap lo obliga: `PT-02` y `PT-03` se miden **antes de comprometer la fase `g`** (§2.2), lo que exige que el bundle ya cargue, ya dibuje y ya libere recursos en ese momento.

### 1.2 Qué es una historia en un visualizador puro

`GeometriaFactory-Visor` no tiene reglas de dominio, no sabe quién mira, no hace red y no persiste nada. En consecuencia:

- **El rol de las catorce historias es el mismo**: el componente anfitrión que embebe el bundle, que vive en `GeometriaFactory-Web` y que el contrato nombra como su actor primario. Ni el alumno ni el administrador son actores acá.
- **Una parte del valor de este proyecto de código es negativo por diseño**: no hacer red es lo que hace imposible violar `RA-01` desde el navegador. Por eso hay historias cuyo entregable es una **ausencia verificable**, y sus criterios se expresan con umbral cero.
- **Ninguna historia acuña un código de condición.** Los códigos son **siete**, su fuente única es [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §6, y un curso nuevo se agrega como fila de curso y no como código (`05` §7 y §9).

## 2. Épicas

| Épica | Nombre | Momento del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-01 | Esqueleto ambulante y verificación de viabilidad | Etapa `a` | El proyecto del bundle existe, su cadena de construcción es reproducible y produce un archivo **vacío pero real** | Ninguna: la etapa `a` no tiene capacidad funcional asociada | BT-01, BT-02, BT-03 |
| EP-02 | Medición de las puertas técnicas del visor | **Antes de comprometer la etapa `g`** (`Roadmap-Producto.md` §2.2) | Lo que `PT-02` y `PT-03` exigen que ya funcione en ese momento: que el bundle cargue, cree la escena, dibuje, sincronice por índice y libere sus recursos | US-01, US-04, US-09, US-11 | BT-04 a BT-10, BT-12, BT-13, BT-14, BT-16 |
| EP-03 | Visualización del trabajo | Etapa `g` | Lo que la etapa integra en el producto: el árbol, el movimiento automático de `F-25`, la tolerancia de claves y la página integradora sin backend | US-02, US-03, US-05, US-06, US-07, US-08, US-10, US-12, US-13, US-14 | BT-06, BT-07, BT-11, BT-15, BT-17, BT-18 |

**Ninguna otra etapa produce épica en este proyecto de código, y es declaración y no olvido.** Las etapas `b` a `f` construyen la cáscara del front, las cuentas, los trabajos y la interpretación; ninguna de ellas dibuja nada. La etapa `h` es el circuito de revisión, y la fachada **dibuja el mismo trabajo para el alumno y para el administrador sin saber cuál de los dos lo mira**, que es exactamente lo que `RA-02` exige (`02` §5.3).

### 2.1 Por qué EP-02 no es una etapa nueva

**EP-02 no crea una etapa, no renombra ninguna y no altera el orden de las ocho comprometidas.** Se apoya en un momento que el roadmap ya declara: su §2.2 ubica a `PT-02` y `PT-03` **antes de comprometer la fase `g`**, y su §5.2 incluye «`PT-02` y `PT-03` medidas antes de comprometer `g`» entre los criterios de la transición `f` → `g`.

De ahí se sigue algo que el backlog tiene que reflejar y que no se lee de la tabla de etapas: **el grueso de este proyecto de código se construye antes de que la etapa `g` se abra**, porque una puerta que no pasa detiene la planificación de la etapa que depende de ella y no se arrastra como deuda. Meter esas cuatro historias dentro de la épica de la etapa `g` habría escondido esa obligación.

Qué exigen exactamente las dos puertas, según `PRODUCT-INTAKE` §17.7.P.8: **`PT-03`**, que el motor de dibujo quede dentro del bundle y que la página funcione sin acceso a redes de distribución externas; **`PT-02`**, que el bundle cargue en una página del anfitrión, que la creación de instancia arme la escena, que la carga del texto dibuje las tres figuras del escenario `E-1` **incluido el ortoedro**, que recorrer diez veces de ida y vuelta no degrade, y que el árbol y la escena se sincronicen por índice.

## 3. Historias por épica

Las **catorce** historias viven **inline** en este documento, porque el proyecto de código está por debajo del umbral de veinte que fija la regla de la categoría. Cada una trae su historia, sus criterios de aceptación en Given/When/Then, su trazabilidad y su verificación de entrada.

La categoría 02 no numeró historias: su §5.1 las describió por contenido —«US de creación de instancia», «US de dibujo del trabajo», «US de gobierno en vivo de los dos movimientos automáticos», y así—. **Esta categoría las numera y las redacta**, que es lo que esa sección deja a la 06, y cada una declara de qué fila de la matriz proviene.

### 3.1 Índice de historias

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| US-01 | Crear una instancia del visor sobre un elemento de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-02 |
| US-02 | Fijar el estado inicial de los dos movimientos al crear la instancia | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-03 |
| US-03 | Informar la ausencia de capacidad gráfica en lugar de fallar en silencio | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-03 |
| US-04 | Dibujar las piezas del texto del trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-02 |
| US-05 | Leer las dimensiones con las variantes de clave del emisor | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| US-06 | Enumerar toda pieza no dibujada con su índice y su condición | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| US-07 | Devolver la estructura del texto para que el anfitrión arme el árbol | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| US-08 | Derivar la disposición de cada pieza de su índice | Should | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| US-09 | Resaltar en exclusiva la pieza del índice indicado | Should | Sin fijar (§4.1) | Propuesta | CU-03 | EP-02 |
| US-10 | Ajustar la escena al tamaño del elemento de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-03 |
| US-11 | Liberar los recursos de la instancia y cortar su bucle de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-02 |
| US-12 | Gobernar en vivo los dos movimientos automáticos sin reconstruir la instancia | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-03 |
| US-13 | Detener el movimiento mientras la persona arrastra y mientras la superficie no está visible | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-03 |
| US-14 | Ejercitar las seis funciones desde una página integradora sin backend | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-03 |

**Rol común a las catorce**: el **componente anfitrión** que embebe el bundle, que vive en `GeometriaFactory-Web` y que el contrato nombra como su actor primario.

### 3.2 EP-02 · Medición de las puertas técnicas del visor

#### US-01 — Crear una instancia del visor sobre un elemento de dibujo

**Historia.** Como componente anfitrión, quiero crear una instancia del visor sobre un elemento de dibujo y recibir su identificador, para tener una escena viva a la que dirigir las otras cinco funciones.

**Contexto.** Contrato de uso [`CU-01`](../02-Especificacion-Funcional/Casos-De-Uso/CU-01-Inicializar-Instancia-Del-Visor.md). Proviene de la primera fila de `02` §5.1, «US de creación de instancia». `PT-02` exige que la creación de instancia arme la escena.

**Criterios de aceptación.**

- Given un elemento de dibujo del anfitrión, When se crea la instancia, Then se devuelve un identificador y la escena queda viva.
- Given dos instancias creadas en la misma página, When se opera sobre una, Then la otra no cambia: no comparten escena, ni selección, ni disposición (garantía `G-4`).
- Given una instancia creada, When se cuentan las peticiones que origina el archivo de guion, Then son exactamente **cero** (garantía `G-1`).

**Trazabilidad.** NB-06 · CU-01 · Garantías `G-1`, `G-3`, `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-04, BT-05, BT-08 · Tests en 08: verificación de las siete garantías y las puertas `PT-02` y `PT-03`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `PT-02` no se puede medir sin ella.

**Verificación de entrada.** Cumple los siete criterios de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) §1.

**Notas.** **El identificador de instancia existe precisamente para que no haya una instancia global única**: `05` §2.1 declara que esa alternativa se descartó porque rompe `G-4` y porque volvería ambigua la liberación de recursos.

#### US-04 — Dibujar las piezas del texto del trabajo

**Historia.** Como componente anfitrión, quiero pasarle a la instancia el texto del trabajo y que dibuje sus piezas, para que la persona vea en tres dimensiones lo que su programa modeló.

**Contexto.** Contrato de uso [`CU-02`](../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md). Proviene de la primera fila de `02` §5.1, «US de dibujo del trabajo». `PT-02` exige que dibuje las tres figuras del escenario `E-1` **incluido el ortoedro**.

**Criterios de aceptación.**

- Given el texto del escenario `E-1`, When se lo carga, Then se dibujan sus **tres** piezas, **ortoedro incluido**.
- Given el texto del escenario `E-7`, When se lo carga, Then se dibujan los **seis** tipos dibujables, tres volumétricos y tres planos.
- Given una pieza de un tipo fuera de esos seis, When se carga el texto, Then no se dibuja y **queda enumerada** con su índice y la condición `TIPO_NO_DIBUJABLE`.

**Trazabilidad.** NB-06, NB-04 (parcial) · CU-02 · Garantías `G-1`, `G-5`, `G-6` · Componentes: lector del texto, servicio de dibujo, motor de dibujo · BT-07, BT-08, BT-09, BT-14 · Tests en 08: escenarios `E-1` y `E-7` del intake §20 como material declarado.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque es el corazón de `PT-02`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El bundle no valida el trabajo ni emite observaciones**: eso es del backend. Que tolere las mismas claves que él **no es duplicar la validación** —el backend decide si el trabajo es válido; el bundle sólo necesita saber de dónde sacar una dimensión para dibujar (`PRODUCT-INTAKE` §17.7.P.11 punto 4).

#### US-09 — Resaltar en exclusiva la pieza del índice indicado

**Historia.** Como componente anfitrión, quiero resaltar la pieza de un índice dado, para que el árbol y la escena señalen lo mismo cuando la persona toca cualquiera de los dos.

**Contexto.** Contrato de uso [`CU-03`](../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Seleccionar-Una-Pieza-Por-Su-Indice.md). Proviene de la tercera fila de `02` §5.1, «US de resaltado exclusivo por índice». `PT-02` exige que **el árbol y la escena se sincronicen por índice**.

**Criterios de aceptación.**

- Given una escena con piezas dibujadas, When se selecciona la pieza de un índice, Then queda resaltada **en exclusiva**: ninguna otra lo está.
- Given un índice que no corresponde a ninguna pieza dibujada, When se lo selecciona, Then la instancia queda como estaba y se informa la condición correspondiente (garantía `G-7`).
- Given un identificador de instancia que no corresponde a una instancia viva, When se invoca la selección, Then se informa `INSTANCIA_DESCONOCIDA`.

**Trazabilidad.** NB-06 · CU-03 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-04, BT-05, BT-08, BT-14 · Tests en 08: `PT-02`, parte de sincronización por índice.

**Prioridad.** `Should` porque su capacidad de origen, `F-13`, es **`Should Have`** en `PRODUCT-INTAKE` §4. **Y sin embargo es bloqueante en la práctica**, porque `PT-02` la incluye entre lo que hay que medir antes de comprometer la etapa `g`, y una puerta que no pasa detiene la planificación de esa etapa. La tensión se declara acá en lugar de resolverse subiéndole la prioridad, que sería reprioritizar una capacidad que el Product Owner clasificó; ver §6, `PA-06`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **La presentación del árbol es del anfitrión** (`05` §3.3): la fachada devuelve la estructura y no dibuja el árbol. Lo que esta historia sincroniza es el índice, que es la identidad de la pieza porque el texto no trae identificador.

#### US-11 — Liberar los recursos de la instancia y cortar su bucle de dibujo

**Historia.** Como componente anfitrión, quiero destruir una instancia y que libere sus recursos, para que recorrer trabajos de ida y vuelta no degrade la aplicación.

**Contexto.** Contrato de uso [`CU-05`](../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Destruir-La-Instancia-Y-Liberar-Recursos.md). Proviene de la primera fila de `02` §5.1, «US de liberación de recursos». `PT-02` exige que recorrer diez veces de ida y vuelta no degrade.

**Criterios de aceptación.**

- Given una instancia viva, When se la destruye, Then libera sus recursos gráficos y **corta su bucle de dibujo**.
- Given diez recorridos de ida y vuelta entre trabajos **con los dos movimientos prendidos**, When se mide la degradación, Then no la hay: ése es el peor caso y es la condición de medición que `02` §6 declara.
- Given una instancia ya destruida, When se la vuelve a usar, Then se informa `INSTANCIA_DESCONOCIDA`: el registro invalidó su identificador.

**Trazabilidad.** NB-06 · CU-05 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-05, BT-12, BT-14 · Tests en 08: propiedad de liberación de recursos con sus condiciones de medición, y `PT-02`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `PT-02` la mide antes de comprometer la etapa `g`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **Un bucle de dibujo que sobreviviera a la destrucción es exactamente la forma de degradación que esta historia tiene que descartar**, y por eso la medición se hace con los movimientos prendidos: con los movimientos apagados no se ejercitaría (`02` §6, `05` §8).

### 3.3 EP-03 · Visualización del trabajo

#### US-02 — Fijar el estado inicial de los dos movimientos al crear la instancia

**Historia.** Como componente anfitrión, quiero fijar al crear la instancia si cada uno de los dos movimientos automáticos arranca prendido o apagado, para respetar la preferencia de movimiento reducido que **yo** consulto, sin que el bundle consulte nada.

**Contexto.** La capacidad `F-25` del intake §4 declara que el anfitrión gobierna los dos movimientos **enviando dos valores de verdad**, y que el bundle **no consulta nada**, en particular no lee la preferencia de movimiento reducido del navegador. Proviene de la quinta fila de `02` §5.1, en su parte de las dos opciones de gobierno de `CU-01`.

**Criterios de aceptación.**

- Given dos valores de verdad pasados al crear la instancia, When la escena arranca, Then cada movimiento arranca en el estado indicado.
- Given una instancia creada, When se inspecciona el bundle, Then **no consulta la preferencia de movimiento reducido del sistema** (garantía `G-3`).
- Given una instancia creada con los dos movimientos apagados, When se recarga la página, Then la preferencia **no se repone** desde el bundle: no la conserva (garantía `G-2`).

**Trazabilidad.** NB-06 · CU-01 · Garantías `G-1`, `G-2`, `G-3` · Componentes: fachada plana, servicio de dibujo · BT-04, BT-11 · Tests en 08: propiedades de cero red y cero persistencia con sus condiciones de medición.

**Prioridad.** `Must` porque `PRODUCT-INTAKE` §4 declara `F-25` como `Must Have`, y porque el roadmap §5.2 incorporó el gobierno independiente de los dos movimientos como criterio de la transición `g` → `h`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** Que el bundle **no** consulte esa preferencia no afloja `RA-02`: la confirma, y además es lo que hace que la prueba de cero red pueda prender los movimientos aunque el entorno de prueba declare movimiento reducido (`02` §6).

#### US-03 — Informar la ausencia de capacidad gráfica en lugar de fallar en silencio

**Historia.** Como componente anfitrión, quiero que la creación de instancia me informe cuando el navegador no tiene capacidad gráfica tridimensional, para poder mostrar una alternativa en lugar de una escena vacía.

**Contexto.** `PRODUCT-INTAKE` §17.7.P.9 declara el requisito **por capacidad y no por versión de navegador**, y que sin esa capacidad el visor no es soportado. `05` §5 declara que la fachada informa `CAPACIDAD_GRAFICA_AUSENTE`.

**Criterios de aceptación.**

- Given un navegador sin capacidad gráfica tridimensional, When se crea la instancia, Then se informa `CAPACIDAD_GRAFICA_AUSENTE` y no se crea ninguna escena.
- Given ese mismo caso, When se inspecciona el estado, Then no queda ninguna instancia a medio construir (garantía `G-7`).
- Given cualquiera de los dos casos, When se cuentan los códigos de condición del contrato, Then siguen siendo **siete**: esta historia no acuña ninguno nuevo.

**Trazabilidad.** NB-06 · CU-01 · Garantías `G-5`, `G-7` · Componentes: fachada plana · BT-04, BT-06 · Tests en 08: verificación de las siete garantías.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque una escena que no aparece sin que nadie se entere es exactamente el problema que `NB-06` viene a cerrar.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **La versión mínima de navegador queda abierta y no se decide acá**: la fuente no la fija y el requisito se declara por capacidad (`05` §11 `PA-04`, recogido como `PA-05` de §6).

#### US-05 — Leer las dimensiones con las variantes de clave del emisor

**Historia.** Como componente anfitrión, quiero que el bundle lea las dimensiones tolerando las variantes de clave que el emisor real produce, para que ninguna pieza que el producto interpreta quede sin dibujar.

**Contexto.** Proviene de la última fila de `02` §5.1, «US de lectura de dimensiones con las variantes de clave del emisor», que traza a `NB-04` **sólo en su parte de piezas efectivamente dibujadas**.

**Criterios de aceptación.**

- Given un texto con las variantes de clave del emisor real, When se lo carga, Then las piezas se dibujan igual.
- Given una pieza a la que le **falta** la clave o el componente del que se lee la medida, When se la procesa, Then no se dibuja y queda enumerada con `DIMENSION_NO_LEGIBLE`.
- Given una dimensión cuyo valor es cero, When se la procesa, Then **la pieza se dibuja**: el cero es una dimensión legible, y lo que produce la condición es la **ausencia** de la clave, nunca el valor que trae.

**Trazabilidad.** NB-06, NB-04 (parcial) · CU-02 · Garantías `G-5` · Componentes: lector del texto · BT-07 · Tests en 08: escenario `E-8` del intake §20, que se incorporó precisamente para `DIMENSION_NO_LEGIBLE`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El tercer criterio existe porque **la visualización previa evaluaba la verdad del número y perdía la figura**, que es lo que la garantía `G-5` viene a impedir (`05` §6).

#### US-06 — Enumerar toda pieza no dibujada con su índice y su condición

**Historia.** Como componente anfitrión, quiero recibir en el resultado de dibujo la lista de las piezas que no se dibujaron, con su índice y su condición, para poder decirle a la persona qué falta y por qué.

**Contexto.** Es la garantía `G-5`, ausencia de fallo silencioso, y `02` §6 la declara como la propiedad **que cierra el problema original de `NB-06`**: hoy, en la visualización previa, la figura simplemente no aparece y nadie se entera.

**Criterios de aceptación.**

- Given un texto con al menos una pieza no dibujable, When se lo carga, Then el resultado de dibujo enumera esa pieza con su índice y su código de condición.
- Given cualquier texto, When se compara la cantidad de piezas del conjunto con las dibujadas más las enumeradas, Then **no falta ninguna**: cero piezas desaparecen sin registro.
- Given los escenarios `E-1` y `E-7`, When se inspecciona el resultado, Then el **100 %** de las piezas no dibujadas está enumerado.

**Trazabilidad.** NB-06 · CU-02 · Garantía `G-5` · Componentes: lector del texto, servicio de dibujo · BT-07, BT-08 · Tests en 08: propiedad de ausencia de fallo silencioso, sin condición adicional de medición.

**Prioridad.** `Must` porque es la garantía que `NB-06` exige y porque `05` §9 declara que una pieza que deje de dibujarse sin quedar enumerada es **exactamente el defecto original** que esa necesidad viene a cerrar.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El bundle no emite observaciones**: ni advertencias ni errores de validación, que son del backend (`02` §2). Lo que enumera son piezas no dibujadas con una **condición del contrato**, que es otra cosa.

#### US-07 — Devolver la estructura del texto para que el anfitrión arme el árbol

**Historia.** Como componente anfitrión, quiero que la fachada me devuelva la estructura del texto, para armar el árbol colapsable en mi propia interfaz.

**Contexto.** Proviene de la segunda fila de `02` §5.1, «US de entrega de la estructura del texto para el árbol». `05` §3.3 declara que **la fachada devuelve la estructura y la presentación del árbol es del anfitrión**, y que el árbol se porta del visualizador previo, al que la fuente califica como su mejor recurso didáctico.

**Criterios de aceptación.**

- Given un texto cargado, When se consulta el resultado, Then trae la estructura del texto con el índice de cada pieza.
- Given esa estructura, When se la compara con el texto original, Then **no lo reescribe ni lo normaliza**: el texto es un dato de entrada opaco.
- Given la estructura devuelta, When se busca en ella cualquier decisión de presentación, Then no hay ninguna: la forma del árbol es del anfitrión.

**Trazabilidad.** NB-06 · CU-02 · Garantías `G-3`, `G-5` · Componentes: fachada plana, lector del texto · BT-04, BT-07 · Tests en 08: recorrido de `CU-06` sobre la página integradora.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, que declara la previsualización **y** el árbol colapsable.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El índice que trae la estructura es el mismo con el que US-09 sincroniza el resaltado: es la identidad de la pieza y no un número de presentación.

#### US-08 — Derivar la disposición de cada pieza de su índice

**Historia.** Como componente anfitrión, quiero que la disposición de las piezas se derive del índice de cada una, para que procesar el mismo trabajo dos veces produzca la misma escena.

**Contexto.** [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Disposicion-Determinista-Derivada-Del-Indice.md) reemplaza el ordenamiento aleatorio del visualizador previo por posición derivada del índice. Proviene de la tercera fila de `02` §5.1, «US de disposición derivada del índice».

**Criterios de aceptación.**

- Given un mismo texto procesado dos veces, When se comparan las dos escenas pieza por pieza, Then la **posición** de cada pieza es la misma.
- Given esa comparación, When se mira la orientación de las piezas en un instante, Then **no se compara**: el determinismo es de la posición y no de la orientación (garantía `G-6`).
- Given cualquier estado de los dos movimientos automáticos, When se repite la comparación, Then el resultado no cambia: prenderlos o apagarlos con la instancia viva no altera la disposición.

**Trazabilidad.** NB-06 · CU-02 · Garantía `G-6` · Componentes: servicio de dibujo · BT-10 · Tests en 08: propiedad de disposición determinista con sus condiciones de medición.

**Prioridad.** `Should` porque su capacidad de origen, `F-13`, es **`Should Have`** en `PRODUCT-INTAKE` §4. Es además criterio de la transición `g` → `h` del roadmap §5.2, con la precisión de que se predica de la posición y no de la orientación.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** La precisión del segundo criterio la introdujo el roadmap 1.2 para que el movimiento automático de `F-25` **no contradijera** el criterio de disposición determinista, y la fuente de esa precisión es `PRODUCT-INTAKE` §17.7.P.10.

#### US-10 — Ajustar la escena al tamaño del elemento de dibujo

**Historia.** Como componente anfitrión, quiero pedirle a la instancia que recalcule su relación de aspecto cuando cambio el tamaño del elemento de dibujo, para que la escena no se deforme.

**Contexto.** Contrato de uso [`CU-04`](../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Redimensionar-La-Escena.md). Proviene de la primera fila de `02` §5.1, «US de ajuste al espacio disponible».

**Criterios de aceptación.**

- Given una instancia viva y un elemento de dibujo que cambió de tamaño, When se pide el ajuste, Then la escena recalcula su relación de aspecto y no se deforma.
- Given un ajuste pedido, When se consulta el estado de la instancia, Then la disposición, la selección vigente y el estado de los movimientos **no cambian**.
- Given un identificador que no corresponde a una instancia viva, When se pide el ajuste, Then se informa `INSTANCIA_DESCONOCIDA`.

**Trazabilidad.** NB-06 · CU-04 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-04, BT-05, BT-08 · Tests en 08: recorrido de `CU-06`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4: una escena deformada no cumple la previsualización que la capacidad declara.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El anfitrión es quien detecta el cambio de tamaño**; la fachada no observa el elemento por su cuenta, porque eso sería conocimiento del sistema que `RA-02` le niega.

#### US-12 — Gobernar en vivo los dos movimientos automáticos sin reconstruir la instancia

**Historia.** Como componente anfitrión, quiero prender y apagar por separado la órbita de la cámara y el giro de las piezas sobre una instancia ya viva, para que la persona controle el movimiento sin perder lo que está mirando.

**Contexto.** Contrato de uso [`CU-07`](../02-Especificacion-Funcional/Casos-De-Uso/CU-07-Gobernar-El-Movimiento-Automatico-De-La-Escena.md), que existe porque `PRODUCT-INTAKE` §17.7.P.3 declara la **sexta función** de la fachada. Proviene de la quinta fila de `02` §5.1. El roadmap §5.2 lo incorporó como **séptimo criterio** de la transición `g` → `h`.

**Criterios de aceptación.**

- Given una instancia viva, When se prende o se apaga cualquiera de los dos movimientos, Then el otro **no cambia**: lo no nombrado conserva su estado.
- Given ese cambio, When se consulta la instancia, Then **no se reconstruyó**: no se recargó el texto, no cambió la disposición y no se perdió la selección vigente.
- Given un cambio de movimiento, When se consulta el resultado, Then devuelve el **estado efectivo de los dos**.

**Trazabilidad.** NB-06 y la capacidad `F-25` del intake §4 · CU-07 · Garantías `G-1`, `G-2`, `G-3`, `G-6` · Componentes: fachada plana, servicio de dibujo · BT-04, BT-11 · Tests en 08: criterios de aceptación de `CU-07` y propiedades de cero red y cero persistencia.

**Prioridad.** `Must` porque `PRODUCT-INTAKE` §4 declara `F-25` como `Must Have` desde su versión 1.7, con el fundamento de que **la órbita de la cámara ya existe en la visualización que la cátedra usa hoy** y de que diferirla sería portar quitando algo que funciona.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El estado de los movimientos **sobrevive a la carga de otro texto**, y es una asimetría deliberada: cargar otro texto reemplaza el contenido dibujado y no el gobierno de la escena; la selección vigente y el resultado de dibujo, en cambio, sí se reemplazan (`05` §6).

#### US-13 — Detener el movimiento mientras la persona arrastra y mientras la superficie no está visible

**Historia.** Como componente anfitrión, quiero que los movimientos automáticos se detengan solos mientras la persona arrastra la cámara y mientras la superficie de dibujo no está visible, para no pelearle el control ni gastar recursos en un movimiento que nadie ve.

**Contexto.** `05` §4 declara las **dos** condiciones de detención del bucle de movimiento y su fundamento. El roadmap §5.2 exige, en la transición `g` → `h`, que los dos se detengan mientras la persona arrastra.

**Criterios de aceptación.**

- Given un movimiento automático prendido, When la persona arrastra la cámara, Then el movimiento se detiene mientras dura el arrastre.
- Given ese mismo movimiento, When la superficie de dibujo deja de estar visible, Then el bucle se detiene.
- Given cualquiera de las dos detenciones, When se consulta el estado gobernado, Then **no cambió**: el anfitrión no tiene que apagar su control porque el bucle se haya detenido solo.

**Trazabilidad.** NB-06 y la capacidad `F-25` · CU-07 · Garantías `G-1`, `G-7` · Componentes: servicio de dibujo · BT-11 · Tests en 08: criterios de aceptación de `CU-07`.

**Prioridad.** `Must` por derivar de `F-25`, `Must Have` en `PRODUCT-INTAKE` §4, que declara explícitamente que los dos se detienen mientras la persona arrastra.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** La distinción entre **detener el bucle** y **cambiar el estado gobernado** es lo que hace que el anfitrión pueda dibujar un control que refleje la intención de la persona y no el instante del bucle.

#### US-14 — Ejercitar las seis funciones desde una página integradora sin backend

**Historia.** Como componente anfitrión —y como cualquier integrador del punto de extensión—, quiero recorrer las seis funciones de la fachada desde una página con un texto pegado a mano y sin ningún servicio del backend disponible, para comprobar que el bundle es de verdad un visualizador puro.

**Contexto.** Contrato de uso [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Ejercitar-La-Fachada-Sin-Backend.md), que es **transversal** y es además el sample `S-1` del producto (`PRODUCT-INTAKE` §16.1 y §18). `PRODUCT-INTAKE` §16.1 declara que es «una propiedad exigida explícitamente» y no un agregado de conveniencia.

**Criterios de aceptación.**

- Given una página que sólo carga el bundle y un texto pegado a mano, When se recorren las **seis** funciones, Then todas responden con **cero** servicios del backend disponibles.
- Given ese recorrido, When se cuentan las peticiones originadas por el archivo de guion **con los dos movimientos prendidos y sostenidos**, Then son exactamente **cero**.
- Given ese recorrido, When se inspecciona el almacenamiento del navegador, Then hay **cero** claves escritas, y recargar la página no repone ninguna preferencia.

**Trazabilidad.** NB-06 y, por contribución negativa, `NB-08` · CU-06 · Garantías `G-1` a `G-7`, las siete · Componentes: la fachada entera · BT-15, BT-16 · Tests en 08: las **seis** propiedades transversales de `02` §6 con sus condiciones de medición.

**Prioridad.** `Must` porque es el sample declarado del producto y porque es donde las seis propiedades transversales se verifican juntas: repartidas entre los otros seis contratos de uso, ninguno las verificaría todas (`02` §3.1 punto 2).

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** Esta historia es la que hace visible el punto de extensión: `Vista-Producto.md` §4 declara que la fachada del bundle es **el punto de extensión declarado del producto**, y una página que la ejercita entera sin backend es la demostración de que ese punto existe.

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 12 | 85,7 % | Sin fijar (§4.1) |
| Should | 2 | 14,3 % | Sin fijar (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **14** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 14 de 14 |
| Historias cerradas | 0 de 14 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **14 de 14**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 18 |
| Tareas técnicas cerradas | 0 de 18 |
| Etapas del producto que este proyecto de código toca | 2 de las 8 comprometidas: `a` y `g`, más el momento de medición de `PT-02` y `PT-03` que precede a la `g` |
| Deuda declarada en el backlog | 4 tareas técnicas que cierran un punto abierto: BT-03, BT-09, BT-17 y BT-18 |

### 4.1 Por qué la unidad de estimación queda abierta

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por el mismo fundamento que los otros dos proyectos de código de nivel 0: el intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas**; la unidad de planificación es la **etapa**; no hay historial del que derivar velocidad; y `equipo_n = 1`.

Hay un motivo propio de este proyecto de código, y es el más fuerte de los tres: **la fuente no fija un umbral numérico de fluidez de la interacción** y `05` §8 declara explícitamente que **esta categoría no inventa uno**, porque un valor inventado se propagaría a 08 como si fuera del producto. Un backlog que se negara a inventar ese número y a la vez inventara puntos de historia sería incoherente consigo mismo.

### 4.2 Por qué la distribución MoSCoW es la que es

**12 `Must` y 2 `Should`**, que son US-08 y US-09:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** `PRODUCT-INTAKE` §4 declara `F-11` y `F-25` como `Must Have` y `F-13` como `Should Have`.
2. **Las dos `Should` derivan de `F-13`** —sincronización árbol ⇄ escena por índice y disposición determinista entre procesados—, que es la única capacidad `Should Have` que toca a este proyecto de código.
3. **Y hay una tensión que corresponde declarar en lugar de taparla**: las dos `Should` están **dentro de lo que `PT-02` mide antes de comprometer la etapa `g`** (`PRODUCT-INTAKE` §17.7.P.8 nombra la sincronización por índice entre lo que la puerta verifica; el roadmap §5.2 nombra la disposición determinista entre los criterios de la transición `g` → `h`). En la práctica no son diferibles, aunque su prioridad declarada admita diferirlas. **No se les sube la prioridad**, porque eso sería reprioritizar una capacidad del Product Owner; se registra la tensión como `PA-06` de §6.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión **antes de abrir el trabajo de EP-02** y otra al abrir la etapa `g`. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Antes de comprometer la etapa `g`, junto con la lectura de `PT-02` y `PT-03`: si una puerta no pasa, el refinamiento de la etapa se detiene y no se arrastra como deuda |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra la garantía de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2 que ejerce y contra el componente de `05` §3.1 que la sostiene |
| Entrada obligatoria a la sesión | Las **siete** garantías, las **siete** prohibiciones y los **siete** códigos de condición del contrato de fachada. Toda historia se refina contra los tres conjuntos |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega comportamiento a la capa 3, la sesión pregunta si ese comportamiento puede originar una petición de red. `05` §9 declara que la causa más probable no es la comodidad del programador sino **una dependencia que la haga por dentro**, y por eso la verificación se hace sobre el **bundle generado** y no sólo sobre el código fuente.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar EP-02 |
| PA-02 | **La versión del motor de dibujo tridimensional** que se adopta, y el cambio de interfaz que exija si es posterior a la del visualizador previo (`05` §11 `PA-01`). Convertido en trabajo como BT-09 | El equipo, al implementar la capa 3 | Antes de comprometer la etapa `g`, que es cuando se miden `PT-02` y `PT-03` |
| PA-03 | **Los nombres definitivos** de las funciones internas, de las clases y de los campos del resultado de dibujo (`05` §11 `PA-02`). **Los nombres de las seis funciones de la fachada no están abiertos**: los fija `PRODUCT-INTAKE` §17.7.P.3. Convertido en trabajo como BT-17 | El equipo, en la etapa que implementa la fachada | Etapa `g` |
| PA-04 | **El umbral numérico de fluidez de la interacción.** Ninguna fuente lo declara y `05` §8 se niega explícitamente a inventarlo. Hasta que exista, la propiedad se verifica de forma cualitativa junto con `PT-02`. Convertido en trabajo como BT-18 | El Product Owner, o la categoría 08 al fijar su guion de medición | Antes de cerrar la etapa `g` |
| PA-05 | **La versión mínima de navegador.** La fuente no la fija: el requisito se declara **por capacidad** —capacidad gráfica tridimensional— y no por versión (`05` §11 `PA-04`). **No se convierte en trabajo**: no hay nada que construir, sólo una declaración que el Product Owner puede querer precisar | El Product Owner sobre su propio documento | Sin fecha comprometida |
| PA-06 | **La tensión entre la prioridad declarada de `F-13` y la puerta `PT-02`**, descrita en §4.2 punto 3: las dos historias `Should` de este backlog están dentro de lo que la puerta mide antes de comprometer la etapa `g`, de modo que en la práctica no son diferibles. Este backlog **no las repriorizó**; se eleva para que el Product Owner decida si `F-13` sigue siendo `Should Have` | El Product Owner sobre `PRODUCT-INTAKE` §4 | Antes de comprometer la etapa `g` |
| PA-07 | **Si el bundle generado se versiona en el repositorio o se ignora** (`05` §11 `PA-05`). Convertido en trabajo como BT-03 | La categoría 09 | Al emitirse 09 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Visor`. Declara **tres** épicas: dos apoyadas en etapas del roadmap y una tercera, EP-02, apoyada en el **momento de medición** que el roadmap §2.2 declara para `PT-02` y `PT-03`, con la constancia explícita de que **no crea una etapa nueva ni renombra ninguna**. Numera y redacta las **catorce** historias que la categoría 02 había descrito por contenido sin numerar, cada una con su fila de origen en la matriz de esa categoría, e inline por estar por debajo del umbral de veinte. Declara qué es una historia en un visualizador puro y por qué algunas tienen entregable de ausencia verificable. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que la categoría 05 ya se negó a inventar el umbral de fluidez. Eleva como `PA-06` la tensión entre la prioridad `Should Have` de `F-13` y la puerta `PT-02`, **sin reprioritizar**. |
