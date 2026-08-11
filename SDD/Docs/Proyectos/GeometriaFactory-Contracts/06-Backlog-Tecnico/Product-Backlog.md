# Product Backlog — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Product-Backlog.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.6 §3 (los **ocho** contratos de uso), §4 (matriz NB → CU → RN → US con las **veintidós** historias previstas), §4.2 (correspondencia con la previsión de 01), §5 (por qué la columna de reglas está vacía) y §6 (las **once** restricciones transversales); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (las **ocho** familias de tipos), §8 (los **siete** NFR) y §11 (los **cuatro** puntos abiertos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4 (las **veintiséis** capacidades y su prioridad), §15 (las **ocho** etapas comprometidas `a` a `h`) y §17.4 (P.1 a P.12); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §3, §4 y §5; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) 1.1 §3 y §4
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Contracts

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 0 para este backlog](#11-qué-significa-nivel-topológico-0-para-este-backlog)
  - [1.2 Qué es una historia en un proyecto de código sin comportamiento](#12-qué-es-una-historia-en-un-proyecto-de-código-sin-comportamiento)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-02 · Identidad del administrador y sesión](#32-ep-02--identidad-del-administrador-y-sesión)
  - [3.3 EP-03 · Ciclo de vida de la cuenta de alumno](#33-ep-03--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.4 EP-04 · Gestión del trabajo](#34-ep-04--gestión-del-trabajo)
  - [3.5 EP-05 · Interpretación y verificación del dato del alumno](#35-ep-05--interpretación-y-verificación-del-dato-del-alumno)
  - [3.6 EP-06 · Visualización del trabajo](#36-ep-06--visualización-del-trabajo)
  - [3.7 EP-07 · Desenlace de la entrega](#37-ep-07--desenlace-de-la-entrega)
  - [3.8 EP-08 · Capacidades de prioridad menor](#38-ep-08--capacidades-de-prioridad-menor)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **ocho** contratos de uso de `GeometriaFactory-Contracts`, que es el ensamblado de tipos que viajan entre las dos unidades desplegables del producto. Su propósito es que se pueda responder, en cualquier momento, qué parte de la frontera ya está declarada y qué queda de ella por declarar.

**El MVP no se define acá.** Lo define el tramo comprometido del producto —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance que el intake declara, **8 de 8 etapas** (§22, asunción `A-2`).

**Este backlog no reordena las etapas ni las renombra.** Las ocho épicas de §2 son la partición de las etapas del producto que tocan a este proyecto de código, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una, más una octava que agrupa lo que cae fuera del tramo comprometido.

### 1.1 Qué significa nivel topológico 0 para este backlog

`Vista-Producto.md` §3 ubica a `GeometriaFactory-Contracts` en el **nivel 0** del orden topológico. Tres consecuencias, y la tercera es propia de este proyecto de código y no la comparte con los otros dos del mismo nivel:

1. **Ninguna historia espera a otro proyecto de código.** El ensamblado no referencia a ninguno, y en particular **ninguna referencia hacia `GeometriaFactory-Domain`**, que el intake declara como puerta bloqueante (§17.4.P.8).
2. **Su trabajo condiciona el de los dos extremos.** `GeometriaFactory-Api` y `GeometriaFactory-Web` compilan contra el mismo ensamblado, de modo que un tipo que acá no exista es un tipo que ninguno de los dos puede usar.
3. **Es el único contrato del producto que dos proyectos de código compilan a la vez**, y por eso `Vista-Producto.md` §4 lo llama la red del producto. La contracara está declarada y es una regla operativa: un cambio incompatible obliga al **despliegue conjunto** de las dos unidades.

### 1.2 Qué es una historia en un proyecto de código sin comportamiento

Este proyecto de código **no tiene comportamiento**: son tipos de transferencia planos (`05` §1). Su arquitectura no es de ejecución sino **de exposición**, y sus historias tienen la misma forma: lo que cada una entrega es que un dato concreto **pueda cruzar la frontera con su forma**, o que un dato concreto **no pueda cruzarla**.

De ahí dos consecuencias que atraviesan las veintidós:

- **El rol de todas es el mismo**: los dos extremos que compilan contra el contrato. No hay alumno ni administrador como actor, aunque sus datos sean los que viajan.
- **Los criterios de aceptación son en buena parte de inspección de la superficie pública**, y no de ejecución. Es lo que la categoría 02 ya declaró al fijar que lo que este proyecto de código decide baja a criterios verificables por inspección y no a reglas de negocio (`02` §5).

**Ninguna historia de este backlog redacta una regla de negocio.** Las reglas viven en `GeometriaFactory-Domain` y acá se citan por identificador, como hace la categoría 02.

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-01 | Esqueleto ambulante y verificación de viabilidad | `a` | El ensamblado existe, compila sin dependencias y sin referencia al dominio, y sus decisiones abiertas de nombre y de momento quedan encaminadas | Ninguna: la etapa `a` no tiene capacidad funcional asociada | BT-01 a BT-05 |
| EP-02 | Identidad del administrador y sesión | `c` | La familia de sesión y la familia de error, que es transversal a las otras siete | US-01, US-14, US-16 | BT-06, BT-07, BT-08, BT-09 |
| EP-03 | Ciclo de vida de la cuenta de alumno | `d` | La familia de cuentas y la familia de reseteo | US-02, US-03, US-04, US-05, US-21, US-22 | BT-10, BT-11 |
| EP-04 | Gestión del trabajo | `e` | La familia de trabajo y la familia de listado, con su carga útil acotada | US-06, US-07, US-08, US-09, US-19 | BT-12, BT-13 |
| EP-05 | Interpretación y verificación del dato del alumno | `f` | La familia de detalle con sus observaciones y sus pares de valores | US-11, US-13, US-15 | BT-14 |
| EP-06 | Visualización del trabajo | `g` | Lo que el detalle tiene que llevar para que el árbol y la escena existan | US-12 | BT-14 |
| EP-07 | Desenlace de la entrega | `h` | La familia de desenlace y el comentario del administrador como bloque propio | US-17, US-18, US-20 | BT-15 |
| EP-08 | Capacidades de prioridad menor | `i…` | Lo que el tramo comprometido no levanta y el intake declara de prioridad menor | US-10 | — |

**La etapa `b` no produce épica en este proyecto de código, y es declaración y no olvido.** Construye la cáscara del front con pantallas de marcador de posición; no hay ningún dato que cruce la frontera todavía.

**EP-08 está fuera del tramo comprometido**, y por eso su única historia no entra en el objetivo de **8 de 8 etapas**: el roadmap §2.1 declara que las capacidades de la fase `i…` se planifican con la plantilla completa cuando `h` esté cerrada y demostrada. Se declara acá para que la previsión de 02 quede completa y no para comprometerla.

## 3. Historias por épica

Las **veintidós** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó, con el mismo identificador y con la misma pertenencia a necesidades de negocio; esa sección declara que **la categoría 06 las confirma al redactarlas**, y esto es esa confirmación. Ninguna se agrega, ninguna se retira y ninguna se renumera. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad

Sin historias. Todo el trabajo de la etapa `a` en este proyecto de código es técnico y vive en [`Backlog-Tecnico.md`](Backlog-Tecnico.md) §2.1 como BT-01 a BT-05.

### 3.2 EP-02 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-01](historias-usuario/US-01-Transportar-El-Canje-De-Credenciales-Y-La-Respuesta-De-Sesion.md) | Transportar el canje de credenciales y la respuesta de sesión de cuatro campos | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-02 |
| [US-14](historias-usuario/US-14-Transportar-El-Error-Neutro-Con-El-Conjunto-Cerrado-De-Codigos.md) | Transportar el error neutro con el conjunto cerrado de quince códigos | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-02 |
| [US-16](historias-usuario/US-16-Cerrar-El-Conjunto-Con-El-Codigo-No-Clasificado.md) | Cerrar el conjunto con el código no clasificado | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-02 |

### 3.3 EP-03 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-02](historias-usuario/US-02-Transportar-El-Registro-De-Una-Cuenta-De-Alumno.md) | Transportar el registro de una cuenta de alumno | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-03](historias-usuario/US-03-Transportar-El-Listado-De-Cuentas-Del-Panel-Del-Administrador.md) | Transportar el listado de cuentas del panel del administrador | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-04](historias-usuario/US-04-Transportar-El-Cambio-De-Situacion-De-La-Cuenta.md) | Transportar el cambio de situación de la cuenta | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-05](historias-usuario/US-05-Transportar-La-Baja-Con-Su-Confirmacion-Escrita.md) | Transportar la baja con su confirmación escrita | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-03 |
| [US-21](historias-usuario/US-21-Transportar-El-Reseteo-Sin-Campo-De-Contrasena.md) | Transportar el reseteo sin campo de contraseña y con la provisoria producida | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-03 |
| [US-22](historias-usuario/US-22-Reutilizar-La-Solicitud-De-Cambio-Para-El-Cambio-Obligatorio.md) | Reutilizar la solicitud de cambio de contraseña para el cambio obligatorio | Must | Sin fijar (§4.1) | Propuesta | CU-08, CU-02 | EP-03 |

### 3.4 EP-04 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-06](historias-usuario/US-06-Transportar-El-Envio-Del-Trabajo-Con-El-Texto-Original.md) | Transportar el envío del trabajo con el texto original como cadena | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-07](historias-usuario/US-07-Transportar-La-Solicitud-Unica-De-Eliminacion.md) | Transportar la solicitud única de eliminación del trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-08](historias-usuario/US-08-Transportar-La-Proyeccion-De-Listado-Sin-La-Carga-Del-Detalle.md) | Transportar la proyección de listado sin la carga del detalle | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-04 |
| [US-09](historias-usuario/US-09-Transportar-El-Alcance-Del-Listado-Segun-El-Papel.md) | Transportar el alcance del listado según el papel, con los datos para agrupar y filtrar | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-04 |
| [US-19](historias-usuario/US-19-Transportar-El-Conjunto-Cerrado-De-Cuatro-Estados.md) | Transportar el conjunto cerrado de cuatro estados del trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-03, CU-04 | EP-04 |

### 3.5 EP-05 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-11](historias-usuario/US-11-Transportar-El-Detalle-Con-Sus-Piezas-Y-Componentes.md) | Transportar el detalle del trabajo interpretado con sus piezas y componentes | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-05 |
| [US-13](historias-usuario/US-13-Transportar-La-Observacion-Con-Severidad-Y-Par-De-Valores.md) | Transportar la observación con su severidad y su par de valores | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-05 |
| [US-15](historias-usuario/US-15-Transportar-El-Detalle-De-Ubicacion-Con-Indice-Y-Campo.md) | Transportar el detalle de ubicación con índice de figura y campo | Must | Sin fijar (§4.1) | Propuesta | CU-06, CU-05 | EP-05 |

### 3.6 EP-06 · Visualización del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-12](historias-usuario/US-12-Transportar-El-Texto-Original-En-El-Detalle-Para-El-Arbol.md) | Transportar el texto original en el detalle, para el árbol y para la escena | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-06 |

### 3.7 EP-07 · Desenlace de la entrega

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-17](historias-usuario/US-17-Transportar-El-Desenlace-Con-Su-Conjunto-Cerrado-De-Dos-Valores.md) | Transportar el desenlace con su conjunto cerrado de dos valores | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-07 |
| [US-18](historias-usuario/US-18-Transportar-El-Comentario-Como-Bloque-Propio.md) | Transportar el comentario del administrador como bloque propio y nunca como observación | Must | Sin fijar (§4.1) | Propuesta | CU-07, CU-05 | EP-07 |
| [US-20](historias-usuario/US-20-Transportar-El-Desenlace-Al-Alumno-Estado-Y-Comentario.md) | Transportar el desenlace al alumno: el estado en el listado y el comentario en el detalle | Must | Sin fijar (§4.1) | Propuesta | CU-04, CU-05 | EP-07 |

### 3.8 EP-08 · Capacidades de prioridad menor

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-10](historias-usuario/US-10-Transportar-El-Resumen-Por-Alumno-Y-Por-Estado.md) | Transportar el resumen por alumno y por estado del panel del administrador | Could | Sin fijar (§4.1) | Propuesta | CU-04 | EP-08 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 21 | 95,5 % | Sin fijar (§4.1) |
| Should | 0 | 0 % | — |
| Could | 1 | 4,5 % | Sin fijar (§4.1) |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **22** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 22 de 22 |
| Historias cerradas | 0 de 22 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | 21 de 22; la restante, US-10, es de la fase `i…` |
| Tareas técnicas declaradas | 18 |
| Tareas técnicas cerradas | 0 de 18 |
| Etapas del producto que este proyecto de código toca | **7** de las 8 comprometidas: `a`, `c`, `d`, `e`, `f`, `g` y `h`. La única que no toca es la `b` |
| Etapas fuera del tramo comprometido con historias declaradas | 1, la `i…`, con US-10 |
| Deuda declarada en el backlog | 4 tareas técnicas que cierran un punto abierto: BT-04, BT-05, BT-17 y BT-18 |

### 4.1 Por qué la unidad de estimación queda abierta

La regla de la categoría exige declarar una técnica de estimación. **Este backlog no la fija, y lo declara en lugar de inventarla**, por el mismo motivo que los otros dos proyectos de código de nivel 0: el intake declara **sin plazo calendario, y que el avance se mide por etapas cerradas**; la unidad de planificación es la **etapa** y no el sprint (`Roadmap-Producto.md` §1.2); no hay historial del que derivar velocidad; y el equipo es de **una sola persona** (`PRODUCT-INTAKE` §2, `equipo_n = 1`).

Hay además un motivo propio de este proyecto de código: **no tiene comportamiento**, y su pipeline no tiene siquiera etapa de pruebas propias (`05` §5). Estimar esfuerzo relativo sobre declaraciones de tipos, cuyo trabajo real es decidir qué campo existe y cuál no, produciría números sin contenido.

La columna `Estimación` dice **«Sin fijar»** en las veintidós historias y en las dieciocho tareas técnicas. El punto abierto es `PA-01` de §6.

### 4.2 Por qué la distribución MoSCoW es la que es

**21 `Must` sobre 22**, con una única `Could`:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza** (`Rules-Plan-Sprint.md` §1.3 declara esa división de titularidad para AG-06). `PRODUCT-INTAKE` §4 declara **dieciocho** de sus **veintiséis** capacidades como `Must Have`.
2. **Este proyecto de código es una frontera, y una frontera se declara entera o no sirve.** Un tipo que falte no degrada una funcionalidad: impide que la funcionalidad exista, porque los dos extremos compilan contra él. Ese es el motivo estructural por el que acá hay todavía menos margen de recorte que en el resto del producto.
3. **La única `Could` es US-10**, y lo es porque su capacidad de origen, `F-15` —panel de resumen del administrador por alumno y por estado—, es `Could Have` en `PRODUCT-INTAKE` §4 y cae en la fase `i…`. La categoría 02 ya la ubicaba ahí al declarar, en su §4.2, que la previsión de producto correspondiente queda fuera con su prioridad menor.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa**, como en todo el producto: se difiere una etapa entera, no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión **por etapa**, al abrir la rama de la etapa. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | Al cerrar la etapa, sobre las historias de la siguiente |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra la familia de tipos de `05` §3.1 que la sostiene y contra la **regla de exposición** de `05` §3.2 |
| Entrada obligatoria a la sesión | La lista de lo que **nunca cruza la frontera** (`05` §3.2) y las once restricciones transversales de `02` §6. Toda historia se refina contra las dos |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Una regla propia de este refinamiento**: cada vez que una historia agrega un campo a un tipo, la sesión pregunta si ese campo puede transportar una dirección de servicio, una ruta de datos o un secreto. `05` §9 declara que agregar un campo de diagnóstico es **la forma habitual en que ese defecto entra**, y que entra sin que nadie lo note porque compila.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c` |
| PA-02 | **Los nombres de los tipos, de sus campos y de los espacios de nombres**, que ni el intake ni la categoría 02 fijan y que se anclan en la etapa que implementa cada familia (`05` §11 PA-01). Convertido en trabajo como BT-04 | El equipo en el punto de control de la etapa correspondiente | De la etapa `c` en adelante, según la familia |
| PA-03 | **La zona horaria y la precisión del campo de momento** del tipo de error: ninguna fuente las declara (`05` §11 PA-02). Convertido en trabajo como BT-05 | El equipo, junto con la elección del formato de intercambio | Etapa `a` o `c` |
| PA-04 | **El formato de intercambio y su configuración** pertenece a `GeometriaFactory-Api` y a `GeometriaFactory-Web` (`05` §11 PA-03). Convertido en trabajo como BT-17, que es de **adopción** y no de decisión: este proyecto de código sólo exige que los tipos sean serializables sin comportamiento | Las categorías 05 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`, **que ya están emitidas** | Al adoptarlo |
| PA-05 | **Los dos valores rotulados [ASUNCIÓN]** de `05` §8, pendientes de confirmación en `PRODUCT-INTAKE` §22, asunción `A-4` (`05` §11 PA-04). Convertido en trabajo como BT-18 | El Product Owner sobre su propio documento | Antes de fijar la puerta en 09 |

**Sobre `PA-04` hay una constancia que corresponde dejar por escrito.** El punto abierto `PA-03` de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11 dice que las categorías 05 de `GeometriaFactory-Api` y de `GeometriaFactory-Web` **no están emitidas todavía**. Al día de este backlog **sí lo están**: [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §1 declara que las siete están emitidas, y su §5 cita la decisión concreta que cierra el formato de intercambio para los dos extremos. El punto abierto, por lo tanto, **ya tiene respuesta aguas abajo** y lo que queda es adoptarla, que es BT-17. Esta observación se eleva para que `05` la absorba; este backlog no edita ese documento.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Contracts`. Declara **ocho** épicas —siete como partición de las etapas del producto que este proyecto de código toca, con el nombre de épica candidata que el roadmap ya había declarado, y una octava para lo que cae fuera del tramo comprometido— y la etapa que no produce épica con su motivo. Confirma y redacta las **veintidós** historias que la categoría 02 previó, con el mismo identificador y la misma pertenencia a necesidades de negocio, cada una en su archivo bajo `historias-usuario/` por superar el umbral de veinte. Declara qué es una historia en un proyecto de código sin comportamiento, la unidad de estimación como **punto abierto** en lugar de inventarla, y la distribución MoSCoW de 21 `Must` sobre 22 con su fundamento estructural. Deja cinco puntos abiertos, cuatro de ellos convertidos en tareas técnicas, y eleva que el punto abierto `PA-03` de la categoría 05 ya tiene respuesta aguas abajo. |
