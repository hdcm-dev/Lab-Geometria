# Product Backlog — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §3 (los **siete** casos de uso), §5.1 (matriz con las historias previstas), §5.3 (cobertura de las nueve necesidades) y §6 (las **seis** propiedades transversales con sus condiciones de medición); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) (las **siete** garantías y los **siete** códigos de condición); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **seis** componentes), §8 (los **ocho** NFR) y §11 (los **cinco** puntos abiertos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.19** §4 (capacidades `F-11`, `F-13` y `F-25`, **las tres `Must Have`**), §15 (etapas y puertas técnicas `PT-02` y `PT-03`), §16.1 y §18 (sample `S-1`), §17.7 (P.1 a P.12, con **P.8** como fundamento de la promoción de `F-13`) y §20 (`E-1` y `E-7`); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §3, §4 y §5; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3 y §4
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `10-Examples` de GeometriaFactory-Visor

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 0 para este backlog](#11-qué-significa-nivel-topológico-0-para-este-backlog)
  - [1.2 Qué es una historia en un visualizador puro](#12-qué-es-una-historia-en-un-visualizador-puro)
- [2. Épicas](#2-épicas)
  - [2.1 Por qué EP-12002 no es una etapa nueva](#21-por-qué-ep-02-no-es-una-etapa-nueva)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 Índice de historias](#31-índice-de-historias)
  - [3.2 EP-12002 · Medición de las puertas técnicas del visor](#32-ep-02--medición-de-las-puertas-técnicas-del-visor)
  - [3.3 EP-12003 · Visualización del trabajo](#33-ep-03--visualización-del-trabajo)
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
| EP-12001 | Esqueleto ambulante y verificación de viabilidad | Etapa `a` | El proyecto del bundle existe, su cadena de construcción es reproducible y produce un archivo **vacío pero real** | Ninguna: la etapa `a` no tiene capacidad funcional asociada | BT-12001, BT-12002, BT-12003 |
| EP-12002 | Medición de las puertas técnicas del visor | **Antes de comprometer la etapa `g`** (`Roadmap-Producto.md` §2.2) | Lo que `PT-02` y `PT-03` exigen que ya funcione en ese momento: que el bundle cargue, cree la escena, dibuje, sincronice por índice y libere sus recursos | US-12001, US-12004, US-12009, US-12011 | BT-12004 a BT-12010, BT-12012, BT-12013, BT-12014, BT-12016 |
| EP-12003 | Visualización del trabajo | Etapa `g` | Lo que la etapa integra en el producto: el árbol, el movimiento automático de `F-25`, la tolerancia de claves y la página integradora sin backend | US-12002, US-12003, US-12005, US-12006, US-12007, US-12008, US-12010, US-12012, US-12013, US-12014 | BT-12006, BT-12007, BT-12011, BT-12015, BT-12017, BT-12018 |

**Ninguna otra etapa produce épica en este proyecto de código, y es declaración y no olvido.** Las etapas `b` a `f` construyen la cáscara del front, las cuentas, los trabajos y la interpretación; ninguna de ellas dibuja nada. La etapa `h` es el circuito de revisión, y la fachada **dibuja el mismo trabajo para el alumno y para el administrador sin saber cuál de los dos lo mira**, que es exactamente lo que `RA-02` exige (`02` §5.3).

### 2.1 Por qué EP-12002 no es una etapa nueva

**EP-12002 no crea una etapa, no renombra ninguna y no altera el orden de las ocho comprometidas.** Se apoya en un momento que el roadmap ya declara: su §2.2 ubica a `PT-02` y `PT-03` **antes de comprometer la fase `g`**, y su §5.2 incluye «`PT-02` y `PT-03` medidas antes de comprometer `g`» entre los criterios de la transición `f` → `g`.

De ahí se sigue algo que el backlog tiene que reflejar y que no se lee de la tabla de etapas: **el grueso de este proyecto de código se construye antes de que la etapa `g` se abra**, porque una puerta que no pasa detiene la planificación de la etapa que depende de ella y no se arrastra como deuda. Meter esas cuatro historias dentro de la épica de la etapa `g` habría escondido esa obligación.

Qué exigen exactamente las dos puertas, según `PRODUCT-INTAKE` §17.7.P.8: **`PT-03`**, que el motor de dibujo quede dentro del bundle y que la página funcione sin acceso a redes de distribución externas; **`PT-02`**, que el bundle cargue en una página del anfitrión, que la creación de instancia arme la escena, que la carga del texto dibuje las tres figuras del escenario `E-1` **incluido el ortoedro**, que recorrer diez veces de ida y vuelta no degrade, y que el árbol y la escena se sincronicen por índice.

## 3. Historias por épica

Las **catorce** historias viven **inline** en este documento. Cada una trae su historia, sus criterios de aceptación en Given/When/Then, su trazabilidad y su verificación de entrada.

**Por qué inline y no un archivo por historia.** La regla de la categoría fija el archivo propio como **obligatorio** a partir de **veinte** historias, lo **recomienda** en la banda de diez a veinte y admite el modo inline por debajo de diez (`Rules-Backlog-Tecnico.md` §2.1 y §3.3). Con catorce, este proyecto de código cae en la banda **recomendada**, de modo que el modo inline es una **elección** y no la aplicación de un umbral: corresponde declararla con su motivo, y no darla por evidente. Los motivos son tres. **Primero**, lo que el archivo propio compra —versionar cada historia por separado, asignarla a otro autor y revisarla sin abrir el resto— no se cobra acá: `equipo_n = 1`, no hay segundo autor a quien asignar ni revisión concurrente que separar. **Segundo**, las catorce historias de este proyecto de código comparten un único rol —el componente anfitrión— y se refinan **contra los mismos tres conjuntos cerrados** —las siete garantías, las siete prohibiciones y los siete códigos de condición—, según la entrada obligatoria de la sesión de §5; leerlas juntas es lo que hace visible que ninguna acuñe un código nuevo, y catorce archivos separados esconderían justamente esa propiedad. **Tercero**, la banda es recomendada y no obligatoria, y la regla exige lo mismo en los dos modos: criterios de aceptación, trazabilidad y verificación de entrada, que las catorce tienen. **La condición para revisar esta elección** es que el backlog llegue a veinte historias, donde el archivo propio pasa a ser obligatorio, o que el equipo deje de ser de una persona.

La categoría 02 no numeró historias: su §5.1 las describió por contenido —«US de creación de instancia», «US de dibujo del trabajo», «US de gobierno en vivo de los dos movimientos automáticos», y así—. **Esta categoría las numera y las redacta**, que es lo que esa sección deja a la 06, y cada una declara de qué fila de la matriz proviene.

### 3.1 Índice de historias

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| US-12001 | Crear una instancia del visor sobre un elemento de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-12001 | EP-12002 |
| US-12002 | Fijar el estado inicial de los dos movimientos al crear la instancia | Must | Sin fijar (§4.1) | Propuesta | CU-12001 | EP-12003 |
| US-12003 | Informar la ausencia de capacidad gráfica en lugar de fallar en silencio | Must | Sin fijar (§4.1) | Propuesta | CU-12001 | EP-12003 |
| US-12004 | Dibujar las piezas del texto del trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-12002 | EP-12002 |
| US-12005 | Leer las dimensiones con las variantes de clave del emisor | Must | Sin fijar (§4.1) | Propuesta | CU-12002 | EP-12003 |
| US-12006 | Enumerar toda pieza no dibujada con su índice y su condición | Must | Sin fijar (§4.1) | Propuesta | CU-12002 | EP-12003 |
| US-12007 | Devolver la estructura del texto para que el anfitrión arme el árbol | Must | Sin fijar (§4.1) | Propuesta | CU-12002 | EP-12003 |
| US-12008 | Derivar la disposición de cada pieza de su índice | Must | Sin fijar (§4.1) | Propuesta | CU-12002 | EP-12003 |
| US-12009 | Resaltar en exclusiva la pieza del índice indicado | Must | Sin fijar (§4.1) | Propuesta | CU-12003 | EP-12002 |
| US-12010 | Ajustar la escena al tamaño del elemento de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-12004 | EP-12003 |
| US-12011 | Liberar los recursos de la instancia y cortar su bucle de dibujo | Must | Sin fijar (§4.1) | Propuesta | CU-12005 | EP-12002 |
| US-12012 | Gobernar en vivo los dos movimientos automáticos sin reconstruir la instancia | Must | Sin fijar (§4.1) | Propuesta | CU-12007 | EP-12003 |
| US-12013 | Detener el movimiento mientras la persona arrastra y mientras la superficie no está visible | Must | Sin fijar (§4.1) | Propuesta | CU-12007 | EP-12003 |
| US-12014 | Ejercitar las seis funciones desde una página integradora sin backend | Must | Sin fijar (§4.1) | Propuesta | CU-12006 | EP-12003 |

**Rol común a las catorce**: el **componente anfitrión** que embebe el bundle, que vive en `GeometriaFactory-Web` y que el contrato nombra como su actor primario.

### 3.2 EP-12002 · Medición de las puertas técnicas del visor

#### US-12001 — Crear una instancia del visor sobre un elemento de dibujo

**Historia.** Como componente anfitrión, quiero crear una instancia del visor sobre un elemento de dibujo y recibir su identificador, para tener una escena viva a la que dirigir las otras cinco funciones.

**Contexto.** Contrato de uso [`CU-12001`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12001-Inicializar-Instancia-Del-Visor.md). Proviene de la primera fila de `02` §5.1, «US de creación de instancia». `PT-02` exige que la creación de instancia arme la escena.

**Criterios de aceptación.**

- Given un elemento de dibujo del anfitrión, When se crea la instancia, Then se devuelve un identificador y la escena queda viva.
- Given dos instancias creadas en la misma página, When se opera sobre una, Then la otra no cambia: no comparten escena, ni selección, ni disposición (garantía `G-4`).
- Given una instancia creada, When se cuentan las peticiones que origina el archivo de guion, Then son exactamente **cero** (garantía `G-1`).

**Trazabilidad.** NB-00006 · CU-12001 · Garantías `G-1`, `G-3`, `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-12004, BT-12005, BT-12008 · Tests en 08: verificación de las siete garantías y las puertas `PT-02` y `PT-03`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `PT-02` no se puede medir sin ella.

**Verificación de entrada.** Cumple los siete criterios de [`Definition-Of-Ready.md`](Definition-Of-Ready.md) §1.

**Notas.** **El identificador de instancia existe precisamente para que no haya una instancia global única**: `05` §2.1 declara que esa alternativa se descartó porque rompe `G-4` y porque volvería ambigua la liberación de recursos.

#### US-12004 — Dibujar las piezas del texto del trabajo

**Historia.** Como componente anfitrión, quiero pasarle a la instancia el texto del trabajo y que dibuje sus piezas, para que la persona vea en tres dimensiones lo que su programa modeló.

**Contexto.** Contrato de uso [`CU-12002`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md). Proviene de la primera fila de `02` §5.1, «US de dibujo del trabajo». `PT-02` exige que dibuje las tres figuras del escenario `E-1` **incluido el ortoedro**.

**Criterios de aceptación.**

- Given el texto del escenario `E-1`, When se lo carga, Then se dibujan sus **tres** piezas, **ortoedro incluido**.
- Given el texto del escenario `E-7`, When se lo carga, Then se dibujan los **seis** tipos dibujables, tres volumétricos y tres planos.
- Given una pieza de un tipo fuera de esos seis, When se carga el texto, Then no se dibuja y **queda enumerada** con su índice y la condición `TIPO_NO_DIBUJABLE`.

**Trazabilidad.** NB-00006, NB-00004 (parcial) · CU-12002 · Garantías `G-1`, `G-5`, `G-6` · Componentes: lector del texto, servicio de dibujo, motor de dibujo · BT-12007, BT-12008, BT-12009, BT-12014 · Tests en 08: escenarios `E-1` y `E-7` del intake §20 como material declarado.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque es el corazón de `PT-02`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El bundle no valida el trabajo ni emite observaciones**: eso es del backend. Que tolere las mismas claves que él **no es duplicar la validación** —el backend decide si el trabajo es válido; el bundle sólo necesita saber de dónde sacar una dimensión para dibujar (`PRODUCT-INTAKE` §17.7.P.11 punto 4).

#### US-12009 — Resaltar en exclusiva la pieza del índice indicado

**Historia.** Como componente anfitrión, quiero resaltar la pieza de un índice dado, para que el árbol y la escena señalen lo mismo cuando la persona toca cualquiera de los dos.

**Contexto.** Contrato de uso [`CU-12003`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12003-Seleccionar-Una-Pieza-Por-Su-Indice.md). Proviene de la tercera fila de `02` §5.1, «US de resaltado exclusivo por índice». `PT-02` exige que **el árbol y la escena se sincronicen por índice**.

**Criterios de aceptación.**

- Given una escena con piezas dibujadas, When se selecciona la pieza de un índice, Then queda resaltada **en exclusiva**: ninguna otra lo está.
- Given un índice que no corresponde a ninguna pieza dibujada, When se lo selecciona, Then la instancia queda como estaba y se informa la condición correspondiente (garantía `G-7`).
- Given un identificador de instancia que no corresponde a una instancia viva, When se invoca la selección, Then se informa `INSTANCIA_DESCONOCIDA`.

**Trazabilidad.** NB-00006 · CU-12003 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-12004, BT-12005, BT-12008, BT-12014 · Tests en 08: `PT-02`, parte de sincronización por índice.

**Prioridad.** `Must` porque su capacidad de origen, `F-13`, es **`Must Have`** en `PRODUCT-INTAKE` §4 desde la versión **1.19** de esa fuente. Hasta esa decisión esta historia era `Should` **y bloqueante en la práctica**, porque `PT-02` la incluye entre lo que hay que medir antes de comprometer la etapa `g` y una puerta que no pasa detiene la planificación de esa etapa. Este backlog **no la repriorizó por su cuenta**: elevó la tensión como `PA-06` y esperó. **El Product Owner la resolvió el 2026-08-10 subiendo la capacidad**, con ese mismo fundamento; ver §6, `PA-06`, cerrado.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **La presentación del árbol es del anfitrión** (`05` §3.3): la fachada devuelve la estructura y no dibuja el árbol. Lo que esta historia sincroniza es el índice, que es la identidad de la pieza porque el texto no trae identificador.

#### US-12011 — Liberar los recursos de la instancia y cortar su bucle de dibujo

**Historia.** Como componente anfitrión, quiero destruir una instancia y que libere sus recursos, para que recorrer trabajos de ida y vuelta no degrade la aplicación.

**Contexto.** Contrato de uso [`CU-12005`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12005-Destruir-La-Instancia-Y-Liberar-Recursos.md). Proviene de la primera fila de `02` §5.1, «US de liberación de recursos». `PT-02` exige que recorrer diez veces de ida y vuelta no degrade.

**Criterios de aceptación.**

- Given una instancia viva, When se la destruye, Then libera sus recursos gráficos y **corta su bucle de dibujo**.
- Given diez recorridos de ida y vuelta entre trabajos **con los dos movimientos prendidos**, When se mide la degradación, Then no la hay: ése es el peor caso y es la condición de medición que `02` §6 declara.
- Given una instancia ya destruida, When se la vuelve a usar, Then se informa `INSTANCIA_DESCONOCIDA`: el registro invalidó su identificador.

**Trazabilidad.** NB-00006 · CU-12005 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-12005, BT-12012, BT-12014 · Tests en 08: propiedad de liberación de recursos con sus condiciones de medición, y `PT-02`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque `PT-02` la mide antes de comprometer la etapa `g`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **Un bucle de dibujo que sobreviviera a la destrucción es exactamente la forma de degradación que esta historia tiene que descartar**, y por eso la medición se hace con los movimientos prendidos: con los movimientos apagados no se ejercitaría (`02` §6, `05` §8).

### 3.3 EP-12003 · Visualización del trabajo

#### US-12002 — Fijar el estado inicial de los dos movimientos al crear la instancia

**Historia.** Como componente anfitrión, quiero fijar al crear la instancia si cada uno de los dos movimientos automáticos arranca prendido o apagado, para respetar la preferencia de movimiento reducido que **yo** consulto, sin que el bundle consulte nada.

**Contexto.** La capacidad `F-25` del intake §4 declara que el anfitrión gobierna los dos movimientos **enviando dos valores de verdad**, y que el bundle **no consulta nada**, en particular no lee la preferencia de movimiento reducido del navegador. Proviene de la quinta fila de `02` §5.1, en su parte de las dos opciones de gobierno de `CU-12001`.

**Criterios de aceptación.**

- Given dos valores de verdad pasados al crear la instancia, When la escena arranca, Then cada movimiento arranca en el estado indicado.
- Given una instancia creada, When se inspecciona el bundle, Then **no consulta la preferencia de movimiento reducido del sistema** (garantía `G-3`).
- Given una instancia creada con los dos movimientos apagados, When se recarga la página, Then la preferencia **no se repone** desde el bundle: no la conserva (garantía `G-2`).

**Trazabilidad.** NB-00006 · CU-12001 · Garantías `G-1`, `G-2`, `G-3` · Componentes: fachada plana, servicio de dibujo · BT-12004, BT-12011 · Tests en 08: propiedades de cero red y cero persistencia con sus condiciones de medición.

**Prioridad.** `Must` porque `PRODUCT-INTAKE` §4 declara `F-25` como `Must Have`, y porque el roadmap §5.2 incorporó el gobierno independiente de los dos movimientos como criterio de la transición `g` → `h`.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** Que el bundle **no** consulte esa preferencia no afloja `RA-02`: la confirma, y además es lo que hace que la prueba de cero red pueda prender los movimientos aunque el entorno de prueba declare movimiento reducido (`02` §6).

#### US-12003 — Informar la ausencia de capacidad gráfica en lugar de fallar en silencio

**Historia.** Como componente anfitrión, quiero que la creación de instancia me informe cuando el navegador no tiene capacidad gráfica tridimensional, para poder mostrar una alternativa en lugar de una escena vacía.

**Contexto.** `PRODUCT-INTAKE` §17.7.P.9 declara el requisito **por capacidad y no por versión de navegador**, y que sin esa capacidad el visor no es soportado. `05` §5 declara que la fachada informa `CAPACIDAD_GRAFICA_AUSENTE`.

**Criterios de aceptación.**

- Given un navegador sin capacidad gráfica tridimensional, When se crea la instancia, Then se informa `CAPACIDAD_GRAFICA_AUSENTE` y no se crea ninguna escena.
- Given ese mismo caso, When se inspecciona el estado, Then no queda ninguna instancia a medio construir (garantía `G-7`).
- Given cualquiera de los dos casos, When se cuentan los códigos de condición del contrato, Then siguen siendo **siete**: esta historia no acuña ninguno nuevo.

**Trazabilidad.** NB-00006 · CU-12001 · Garantías `G-5`, `G-7` · Componentes: fachada plana · BT-12004, BT-12006 · Tests en 08: verificación de las siete garantías.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, y porque una escena que no aparece sin que nadie se entere es exactamente el problema que `NB-00006` viene a cerrar.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **La versión mínima de navegador queda abierta y no se decide acá**: la fuente no la fija y el requisito se declara por capacidad (`05` §11 `PA-04`, recogido como `PA-05` de §6).

#### US-12005 — Leer las dimensiones con las variantes de clave del emisor

**Historia.** Como componente anfitrión, quiero que el bundle lea las dimensiones tolerando las variantes de clave que el emisor real produce, para que ninguna pieza que el producto interpreta quede sin dibujar.

**Contexto.** Proviene de la última fila de `02` §5.1, «US de lectura de dimensiones con las variantes de clave del emisor», que traza a `NB-00004` **sólo en su parte de piezas efectivamente dibujadas**.

**Criterios de aceptación.**

- Given un texto con las variantes de clave del emisor real, When se lo carga, Then las piezas se dibujan igual.
- Given una pieza a la que le **falta** la clave o el componente del que se lee la medida, When se la procesa, Then no se dibuja y queda enumerada con `DIMENSION_NO_LEGIBLE`.
- Given una dimensión cuyo valor es cero, When se la procesa, Then **la pieza se dibuja**: el cero es una dimensión legible, y lo que produce la condición es la **ausencia** de la clave, nunca el valor que trae.

**Trazabilidad.** NB-00006, NB-00004 (parcial) · CU-12002 · Garantías `G-5` · Componentes: lector del texto · BT-12007 · Tests en 08: escenario `E-8` del intake §20, que se incorporó precisamente para `DIMENSION_NO_LEGIBLE`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El tercer criterio existe porque **la visualización previa evaluaba la verdad del número y perdía la figura**, que es lo que la garantía `G-5` viene a impedir (`05` §6).

#### US-12006 — Enumerar toda pieza no dibujada con su índice y su condición

**Historia.** Como componente anfitrión, quiero recibir en el resultado de dibujo la lista de las piezas que no se dibujaron, con su índice y su condición, para poder decirle a la persona qué falta y por qué.

**Contexto.** Es la garantía `G-5`, ausencia de fallo silencioso, y `02` §6 la declara como la propiedad **que cierra el problema original de `NB-00006`**: hoy, en la visualización previa, la figura simplemente no aparece y nadie se entera.

**Criterios de aceptación.**

- Given un texto con al menos una pieza no dibujable, When se lo carga, Then el resultado de dibujo enumera esa pieza con su índice y su código de condición.
- Given cualquier texto, When se compara la cantidad de piezas del conjunto con las dibujadas más las enumeradas, Then **no falta ninguna**: cero piezas desaparecen sin registro.
- Given los escenarios `E-1` y `E-7`, When se inspecciona el resultado, Then el **100 %** de las piezas no dibujadas está enumerado.

**Trazabilidad.** NB-00006 · CU-12002 · Garantía `G-5` · Componentes: lector del texto, servicio de dibujo · BT-12007, BT-12008 · Tests en 08: propiedad de ausencia de fallo silencioso, sin condición adicional de medición.

**Prioridad.** `Must` porque es la garantía que `NB-00006` exige y porque `05` §9 declara que una pieza que deje de dibujarse sin quedar enumerada es **exactamente el defecto original** que esa necesidad viene a cerrar.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El bundle no emite observaciones**: ni advertencias ni errores de validación, que son del backend (`02` §2). Lo que enumera son piezas no dibujadas con una **condición del contrato**, que es otra cosa.

#### US-12007 — Devolver la estructura del texto para que el anfitrión arme el árbol

**Historia.** Como componente anfitrión, quiero que la fachada me devuelva la estructura del texto, para armar el árbol colapsable en mi propia interfaz.

**Contexto.** Proviene de la segunda fila de `02` §5.1, «US de entrega de la estructura del texto para el árbol». `05` §3.3 declara que **la fachada devuelve la estructura y la presentación del árbol es del anfitrión**, y que el árbol se porta del visualizador previo, al que la fuente califica como su mejor recurso didáctico.

**Criterios de aceptación.**

- Given un texto cargado, When se consulta el resultado, Then trae la estructura del texto con el índice de cada pieza.
- Given esa estructura, When se la compara con el texto original, Then **no lo reescribe ni lo normaliza**: el texto es un dato de entrada opaco.
- Given la estructura devuelta, When se busca en ella cualquier decisión de presentación, Then no hay ninguna: la forma del árbol es del anfitrión.

**Trazabilidad.** NB-00006 · CU-12002 · Garantías `G-3`, `G-5` · Componentes: fachada plana, lector del texto · BT-12004, BT-12007 · Tests en 08: recorrido de `CU-12006` sobre la página integradora.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4, que declara la previsualización **y** el árbol colapsable.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El índice que trae la estructura es el mismo con el que US-12009 sincroniza el resaltado: es la identidad de la pieza y no un número de presentación.

#### US-12008 — Derivar la disposición de cada pieza de su índice

**Historia.** Como componente anfitrión, quiero que la disposición de las piezas se derive del índice de cada una, para que procesar el mismo trabajo dos veces produzca la misma escena.

**Contexto.** [`ADR-12005`](../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) reemplaza el ordenamiento aleatorio del visualizador previo por posición derivada del índice. Proviene de la tercera fila de `02` §5.1, «US de disposición derivada del índice».

**Criterios de aceptación.**

- Given un mismo texto procesado dos veces, When se comparan las dos escenas pieza por pieza, Then la **posición** de cada pieza es la misma.
- Given esa comparación, When se mira la orientación de las piezas en un instante, Then **no se compara**: el determinismo es de la posición y no de la orientación (garantía `G-6`).
- Given cualquier estado de los dos movimientos automáticos, When se repite la comparación, Then el resultado no cambia: prenderlos o apagarlos con la instancia viva no altera la disposición.

**Trazabilidad.** NB-00006 · CU-12002 · Garantía `G-6` · Componentes: servicio de dibujo · BT-12010 · Tests en 08: propiedad de disposición determinista con sus condiciones de medición.

**Prioridad.** `Must` porque su capacidad de origen, `F-13`, es **`Must Have`** en `PRODUCT-INTAKE` §4 desde la versión **1.19** de esa fuente. Era `Should` hasta esa decisión, aunque ya fuera criterio de la transición `g` → `h` del roadmap §5.2 —con la precisión de que se predica de la posición y no de la orientación— y estuviera entre lo que `PT-02` mide: esa contradicción es la que el Product Owner resolvió; ver §6, `PA-06`, cerrado.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** La precisión del segundo criterio la introdujo el roadmap 1.2 para que el movimiento automático de `F-25` **no contradijera** el criterio de disposición determinista, y la fuente de esa precisión es `PRODUCT-INTAKE` §17.7.P.10.

#### US-12010 — Ajustar la escena al tamaño del elemento de dibujo

**Historia.** Como componente anfitrión, quiero pedirle a la instancia que recalcule su relación de aspecto cuando cambio el tamaño del elemento de dibujo, para que la escena no se deforme.

**Contexto.** Contrato de uso [`CU-12004`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12004-Redimensionar-La-Escena.md). Proviene de la primera fila de `02` §5.1, «US de ajuste al espacio disponible».

**Criterios de aceptación.**

- Given una instancia viva y un elemento de dibujo que cambió de tamaño, When se pide el ajuste, Then la escena recalcula su relación de aspecto y no se deforma.
- Given un ajuste pedido, When se consulta el estado de la instancia, Then la disposición, la selección vigente y el estado de los movimientos **no cambian**.
- Given un identificador que no corresponde a una instancia viva, When se pide el ajuste, Then se informa `INSTANCIA_DESCONOCIDA`.

**Trazabilidad.** NB-00006 · CU-12004 · Garantías `G-4`, `G-7` · Componentes: fachada plana, registro de instancias, servicio de dibujo · BT-12004, BT-12005, BT-12008 · Tests en 08: recorrido de `CU-12006`.

**Prioridad.** `Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4: una escena deformada no cumple la previsualización que la capacidad declara.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** **El anfitrión es quien detecta el cambio de tamaño**; la fachada no observa el elemento por su cuenta, porque eso sería conocimiento del sistema que `RA-02` le niega.

#### US-12012 — Gobernar en vivo los dos movimientos automáticos sin reconstruir la instancia

**Historia.** Como componente anfitrión, quiero prender y apagar por separado la órbita de la cámara y el giro de las piezas sobre una instancia ya viva, para que la persona controle el movimiento sin perder lo que está mirando.

**Contexto.** Contrato de uso [`CU-12007`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12007-Gobernar-El-Movimiento-Automatico-De-La-Escena.md), que existe porque `PRODUCT-INTAKE` §17.7.P.3 declara la **sexta función** de la fachada. Proviene de la quinta fila de `02` §5.1. El roadmap §5.2 lo incorporó como **séptimo criterio** de la transición `g` → `h`.

**Criterios de aceptación.**

- Given una instancia viva, When se prende o se apaga cualquiera de los dos movimientos, Then el otro **no cambia**: lo no nombrado conserva su estado.
- Given ese cambio, When se consulta la instancia, Then **no se reconstruyó**: no se recargó el texto, no cambió la disposición y no se perdió la selección vigente.
- Given un cambio de movimiento, When se consulta el resultado, Then devuelve el **estado efectivo de los dos**.

**Trazabilidad.** NB-00006 y la capacidad `F-25` del intake §4 · CU-12007 · Garantías `G-1`, `G-2`, `G-3`, `G-6` · Componentes: fachada plana, servicio de dibujo · BT-12004, BT-12011 · Tests en 08: criterios de aceptación de `CU-12007` y propiedades de cero red y cero persistencia.

**Prioridad.** `Must` porque `PRODUCT-INTAKE` §4 declara `F-25` como `Must Have` desde su versión 1.7, con el fundamento de que **la órbita de la cámara ya existe en la visualización que la cátedra usa hoy** y de que diferirla sería portar quitando algo que funciona.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** El estado de los movimientos **sobrevive a la carga de otro texto**, y es una asimetría deliberada: cargar otro texto reemplaza el contenido dibujado y no el gobierno de la escena; la selección vigente y el resultado de dibujo, en cambio, sí se reemplazan (`05` §6).

#### US-12013 — Detener el movimiento mientras la persona arrastra y mientras la superficie no está visible

**Historia.** Como componente anfitrión, quiero que los movimientos automáticos se detengan solos mientras la persona arrastra la cámara y mientras la superficie de dibujo no está visible, para no pelearle el control ni gastar recursos en un movimiento que nadie ve.

**Contexto.** `05` §4 declara las **dos** condiciones de detención del bucle de movimiento y su fundamento. El roadmap §5.2 exige, en la transición `g` → `h`, que los dos se detengan mientras la persona arrastra.

**Criterios de aceptación.**

- Given un movimiento automático prendido, When la persona arrastra la cámara, Then el movimiento se detiene mientras dura el arrastre.
- Given ese mismo movimiento, When la superficie de dibujo deja de estar visible, Then el bucle se detiene.
- Given cualquiera de las dos detenciones, When se consulta el estado gobernado, Then **no cambió**: el anfitrión no tiene que apagar su control porque el bucle se haya detenido solo.

**Trazabilidad.** NB-00006 y la capacidad `F-25` · CU-12007 · Garantías `G-1`, `G-7` · Componentes: servicio de dibujo · BT-12011 · Tests en 08: criterios de aceptación de `CU-12007`.

**Prioridad.** `Must` por derivar de `F-25`, `Must Have` en `PRODUCT-INTAKE` §4, que declara explícitamente que los dos se detienen mientras la persona arrastra.

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** La distinción entre **detener el bucle** y **cambiar el estado gobernado** es lo que hace que el anfitrión pueda dibujar un control que refleje la intención de la persona y no el instante del bucle.

#### US-12014 — Ejercitar las seis funciones desde una página integradora sin backend

**Historia.** Como componente anfitrión —y como cualquier integrador del punto de extensión—, quiero recorrer las seis funciones de la fachada desde una página con un texto pegado a mano y sin ningún servicio del backend disponible, para comprobar que el bundle es de verdad un visualizador puro.

**Contexto.** Contrato de uso [`CU-12006`](../02-Especificacion-Funcional/Casos-De-Uso/CU-12006-Ejercitar-La-Fachada-Sin-Backend.md), que es **transversal** y es además el sample `S-1` del producto (`PRODUCT-INTAKE` §16.1 y §18). `PRODUCT-INTAKE` §16.1 declara que es «una propiedad exigida explícitamente» y no un agregado de conveniencia.

**Criterios de aceptación.**

- Given una página que sólo carga el bundle y un texto pegado a mano, When se recorren las **seis** funciones, Then todas responden con **cero** servicios del backend disponibles.
- Given ese recorrido, When se cuentan las peticiones originadas por el archivo de guion **con los dos movimientos prendidos y sostenidos**, Then son exactamente **cero**.
- Given ese recorrido, When se inspecciona el almacenamiento del navegador, Then hay **cero** claves escritas, y recargar la página no repone ninguna preferencia.

**Trazabilidad.** NB-00006 y, por contribución negativa, `NB-00008` · CU-12006 · Garantías `G-1` a `G-7`, las siete · Componentes: la fachada entera · BT-12015, BT-12016 · Tests en 08: las **seis** propiedades transversales de `02` §6 con sus condiciones de medición.

**Prioridad.** `Must` porque es el sample declarado del producto y porque es donde las seis propiedades transversales se verifican juntas: repartidas entre los otros seis contratos de uso, ninguno las verificaría todas (`02` §3.1 punto 2).

**Verificación de entrada.** Cumple los siete criterios de la DoR.

**Notas.** Esta historia es la que hace visible el punto de extensión: `Vista-Producto.md` §4 declara que la fachada del bundle es **el punto de extensión declarado del producto**, y una página que la ejercita entera sin backend es la demostración de que ese punto existe.

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 14 | 100 % | Sin fijar (§4.1) |
| Should | 0 | 0 % | — |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **14** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-11 |
| --- | --- |
| Historias en estado `Propuesta` | 14 de 14 |
| Historias cerradas | 0 de 14 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **14 de 14**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 18 |
| Tareas técnicas cerradas | 0 de 18 |
| Etapas del producto que este proyecto de código toca | 2 de las 8 comprometidas: `a` y `g`, más el momento de medición de `PT-02` y `PT-03` que precede a la `g` |
| Deuda declarada en el backlog | 4 tareas técnicas que cierran un punto abierto: BT-12003, BT-12009, BT-12017 y BT-12018 |

### 4.1 Por qué la unidad de estimación queda abierta

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por el mismo fundamento que los otros dos proyectos de código de nivel 0: el intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas**; la unidad de planificación es la **etapa**; no hay historial del que derivar velocidad; y `equipo_n = 1`.

Hay un motivo propio de este proyecto de código, y es el más fuerte de los tres: **la fuente no fija un umbral numérico de fluidez de la interacción** y `05` §8 declara explícitamente que **esta categoría no inventa uno**, porque un valor inventado se propagaría a 08 como si fuera del producto. Un backlog que se negara a inventar ese número y a la vez inventara puntos de historia sería incoherente consigo mismo.

### 4.2 Por qué la distribución MoSCoW es la que es

**14 `Must` y ninguna no-`Must`**, desde el 2026-08-10:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** `PRODUCT-INTAKE` §4 declara `F-11`, `F-13` y `F-25` como `Must Have`: `F-25` desde la versión 1.7 de esa fuente y **`F-13` desde la 1.19**. Las tres capacidades que tocan a este proyecto de código son hoy `Must Have`, y por eso sus catorce historias lo son.
2. **Las dos historias que eran `Should` —US-12008 y US-12009— derivan de `F-13`**, sincronización árbol ⇄ escena por índice y disposición determinista entre procesados. Cambiaron de prioridad **porque cambió la de su capacidad de origen**, no porque este backlog las repriorizara.
3. **Cómo se resolvió la tensión que este backlog había elevado.** Las dos historias estaban **dentro de lo que `PT-02` mide antes de comprometer la etapa `g`** (`PRODUCT-INTAKE` §17.7.P.8 nombra la sincronización por índice entre lo que la puerta verifica; el roadmap §5.2 nombra la disposición determinista entre los criterios de la transición `g` → `h`), de modo que en la práctica no eran diferibles aunque su prioridad declarada lo admitiera. Este backlog **no les subió la prioridad**: elevó la tensión como `PA-06` y la dejó en manos de quien podía resolverla. **El Product Owner la resolvió el 2026-08-10 promoviendo `F-13` a `Must Have`**, con ese mismo fundamento, y `PA-06` queda cerrado en §6. `GeometriaFactory-Web` había elevado la misma tensión desde el otro lado de la fachada.
4. **Este backlog queda en 100 % `Must`, y hay que declararlo porque la regla de la categoría lo mira con dureza.** El criterio de aceptación de `Rules-Backlog-Tecnico.md` §6 pide que la distribución **no sea 100 % `Must`**, y ésta lo es. **No es una omisión de priorización sino la consecuencia aritmética de una decisión ajena**: las tres capacidades que bajan a este proyecto de código son `Must Have` en la fuente, y este backlog no puede degradar ninguna sin reprioritizar por su cuenta, que es exactamente lo que se negó a hacer cuando la tensión estaba abierta. Inventar acá una `Should` para cumplir el reparto sería peor que el apartamiento: sería una prioridad falsa. Queda declarado como apartamiento consciente, con su motivo, y la condición para que deje de serlo es que el Product Owner clasifique con prioridad menor alguna capacidad que toque a este proyecto de código.

**Sobre la regularidad de esta distribución** [AGREGADO 2026-08-11, en respuesta al hallazgo `D-06-03` de [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0]. La auditoría observó que la distribución de los siete backlogs es demasiado regular para ser casualidad, y tiene razón en que **la regularidad existe y hasta ahora no estaba declarada**. Se declara acá, con el recuento hecho de nuevo sobre las fichas y sobre los índices inline, y con su explicación.

| Proyecto de código | Historias | `Must` | `Should` | `Could` |
| --- | --- | --- | --- | --- |
| GeometriaFactory-Domain | 27 | 26 | 1 | 0 |
| GeometriaFactory-Contracts | 22 | 21 | 0 | 1 |
| GeometriaFactory-Visor | 14 | 14 | 0 | 0 |
| GeometriaFactory-Application | 32 | 31 | 1 | 0 |
| GeometriaFactory-Web | 30 | 30 | 0 | 0 |
| GeometriaFactory-Infrastructure | 25 | 24 | 1 | 0 |
| GeometriaFactory-Api | 30 | 29 | 1 | 0 |
| **Total** | **180** | **175** | **4** | **1** |

**La explicación no es una cuota, y se puede verificar una por una.** El tramo comprometido —las etapas `c` a `h`— contiene **diecinueve** capacidades del intake §4, y desde `PRODUCT-INTAKE` **1.19** **las diecinueve son `Must Have`**: la única que no lo era, `F-13`, la promovió el Product Owner el 2026-08-10. De ahí se sigue mecánicamente que **ninguna historia que derive de una capacidad del tramo comprometido puede ser no-`Must`**, y que las no-`Must` que existen tienen que venir de otro lado. Vienen de dos lados, y sólo de dos:

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-12010` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-12012` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11), `US-12016` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-12023` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-12030` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión **antes de abrir el trabajo de EP-12002** y otra al abrir la etapa `g`. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Antes de comprometer la etapa `g`, junto con la lectura de `PT-02` y `PT-03`: si una puerta no pasa, el refinamiento de la etapa se detiene y no se arrastra como deuda |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra la garantía de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2 que ejerce y contra el componente de `05` §3.1 que la sostiene |
| Entrada obligatoria a la sesión | Las **siete** garantías, las **siete** prohibiciones y los **siete** códigos de condición del contrato de fachada. Toda historia se refina contra los tres conjuntos |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega comportamiento a la capa 3, la sesión pregunta si ese comportamiento puede originar una petición de red. `05` §9 declara que la causa más probable no es la comodidad del programador sino **una dependencia que la haga por dentro**, y por eso la verificación se hace sobre el **bundle generado** y no sólo sobre el código fuente.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar EP-12002 |
| PA-02 | **La versión del motor de dibujo tridimensional** que se adopta, y el cambio de interfaz que exija si es posterior a la del visualizador previo (`05` §11 `PA-01`). Convertido en trabajo como BT-12009 | El equipo, al implementar la capa 3 | Antes de comprometer la etapa `g`, que es cuando se miden `PT-02` y `PT-03` |
| PA-03 | **Los nombres definitivos** de las funciones internas, de las clases y de los campos del resultado de dibujo (`05` §11 `PA-02`). **Los nombres de las seis funciones de la fachada no están abiertos**: los fija `PRODUCT-INTAKE` §17.7.P.3. Convertido en trabajo como BT-12017 | El equipo, en la etapa que implementa la fachada | Etapa `g` |
| PA-04 | **El umbral numérico de fluidez de la interacción.** Ninguna fuente lo declara y `05` §8 se niega explícitamente a inventarlo. Hasta que exista, la propiedad se verifica de forma cualitativa junto con `PT-02`. Convertido en trabajo como BT-12018 | El Product Owner, o la categoría 08 al fijar su guion de medición | Antes de cerrar la etapa `g` |
| PA-05 | **La versión mínima de navegador.** La fuente no la fija: el requisito se declara **por capacidad** —capacidad gráfica tridimensional— y no por versión (`05` §11 `PA-04`). **No se convierte en trabajo**: no hay nada que construir, sólo una declaración que el Product Owner puede querer precisar | El Product Owner sobre su propio documento | Sin fecha comprometida |
| PA-06 | ~~**La tensión entre la prioridad declarada de `F-13` y la puerta `PT-02`**: las dos historias `Should` de este backlog están dentro de lo que la puerta mide antes de comprometer la etapa `g`, de modo que en la práctica no son diferibles. Este backlog **no las repriorizó**; se elevó para que el Product Owner decidiera si `F-13` seguía siendo `Should Have`.~~ **CERRADO el 2026-08-10.** Desenlace: el Product Owner **promovió `F-13` a `Must Have`** en `PRODUCT-INTAKE` **1.19** §4, con el fundamento que este backlog había elevado —§17.7 P.8 incluye las dos propiedades entre lo que `PT-02` mide antes de comprometer la etapa `g`, y una puerta que no pasa detiene la planificación—. US-12008 y US-12009 pasan a `Must` en consecuencia, y §4.2 recoge el desenlace. `GeometriaFactory-Web` había elevado la misma tensión desde el otro lado de la fachada, como `PA-02` de su backlog, y queda cerrada por la misma decisión. **La fila se conserva para no dejar hueco de numeración** | Cerrado por el Product Owner sobre `PRODUCT-INTAKE` §4 | Cerrado el 2026-08-10 |
| PA-07 | **Si el bundle generado se versiona en el repositorio o se ignora** (`05` §11 `PA-05`). Convertido en trabajo como BT-12003 | La categoría 09 | Al emitirse 09 |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Visor`. Declara **tres** épicas: dos apoyadas en etapas del roadmap y una tercera, EP-12002, apoyada en el **momento de medición** que el roadmap §2.2 declara para `PT-02` y `PT-03`, con la constancia explícita de que **no crea una etapa nueva ni renombra ninguna**. Numera y redacta las **catorce** historias que la categoría 02 había descrito por contenido sin numerar, cada una con su fila de origen en la matriz de esa categoría, e inline por estar por debajo del umbral de veinte. Declara qué es una historia en un visualizador puro y por qué algunas tienen entregable de ausencia verificable. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que la categoría 05 ya se negó a inventar el umbral de fluidez. Eleva como `PA-06` la tensión entre la prioridad `Should Have` de `F-13` y la puerta `PT-02`, **sin reprioritizar**. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra los hallazgos `D-06-03` y `D-06-04`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Cabecera**: la trazabilidad upstream pasa al intake **1.19**, declara las tres capacidades como `Must Have` y señala **§17.7 P.8** como fuente del fundamento. **§3.1 y fichas `US-12008` y `US-12009`**: las dos historias pasan de `Should` a **`Must`**, porque cambió la prioridad de su capacidad de origen y no porque este backlog repriorizara. **§4**: el reparto pasa de 12/2 a **14 `Must` sobre 14**. **§4.2**: se reescriben los tres puntos —la tensión elevada tiene desenlace— y entra un cuarto punto que declara el **100 % `Must`** como apartamiento consciente del criterio de aceptación de `Rules-Backlog-Tecnico.md` §6, con su motivo y su condición de revisión. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», con el recuento de los siete proyectos de código contado de nuevo —**175 `Must`, 4 `Should`, 1 `Could`** sobre 180— y con la explicación de por qué la regularidad no es una cuota. **§3 (`D-06-04`)**: entra la subsección que declara **por qué las catorce historias van inline** y no en archivo propio. La regla `Rules-Backlog-Tecnico.md` §2.1 clasifica la banda de diez a veinte como **recomendada** para archivo propio, de modo que con catorce el modo inline es una elección y no la aplicación de un umbral; se declara con sus tres motivos —`equipo_n = 1`, el refinamiento contra tres conjuntos cerrados comunes a las catorce, y la equivalencia de contenido que la regla exige en los dos modos— y con la condición para revisarla. **§6**: `PA-06` queda **cerrado** con su desenlace y su fecha, conservando la fila para no dejar hueco de numeración. Sube minor. |
