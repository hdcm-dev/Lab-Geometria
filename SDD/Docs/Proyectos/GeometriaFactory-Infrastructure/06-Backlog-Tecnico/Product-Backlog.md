# Product Backlog — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Product-Backlog.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.3 §3 (los **cuatro** puertos, los **dos** mecanismos y la responsabilidad de arranque), §4 (lo que hace y lo que no decide), §5 (los **diez** casos de uso), §6 (las **dieciséis** reglas y dónde se ejerce cada una), §7.1 (matriz NB → CU → RN → US), §7.3 (las **veinticinco** historias previstas) y §11 (los **quince** puntos abiertos); [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) y sus **siete** reglas conceptuales; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1 (los **ocho** componentes), §8 (los **catorce** NFR), §9 (los **ocho** riesgos), §10.5 (los ocho escenarios contra la batería de diez casos) y §11 (sus **once** filas: diez abiertas y una resuelta); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) (las **17** condiciones); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4, §15, §17.3 y §20 (los **ocho** escenarios `E-1` a `E-8`); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §3, §4 y §5
**Trazabilidad downstream:** [`Backlog-Tecnico.md`](Backlog-Tecnico.md), [`Definition-Of-Ready.md`](Definition-Of-Ready.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Objetivos del producto](#1-objetivos-del-producto)
  - [1.1 Qué significa nivel topológico 2 para este backlog](#11-qué-significa-nivel-topológico-2-para-este-backlog)
  - [1.2 Qué es una historia en la capa que toca el mundo](#12-qué-es-una-historia-en-la-capa-que-toca-el-mundo)
- [2. Épicas](#2-épicas)
- [3. Historias por épica](#3-historias-por-épica)
  - [3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad](#31-ep-01--esqueleto-ambulante-y-verificación-de-viabilidad)
  - [3.2 EP-02 · Identidad del administrador y sesión](#32-ep-02--identidad-del-administrador-y-sesión)
  - [3.3 EP-03 · Ciclo de vida de la cuenta de alumno](#33-ep-03--ciclo-de-vida-de-la-cuenta-de-alumno)
  - [3.4 EP-04 · Gestión del trabajo](#34-ep-04--gestión-del-trabajo)
  - [3.5 EP-05 · Interpretación y verificación del dato del alumno](#35-ep-05--interpretación-y-verificación-del-dato-del-alumno)
- [4. Métricas de avance](#4-métricas-de-avance)
  - [4.1 Por qué la unidad de estimación queda abierta](#41-por-qué-la-unidad-de-estimación-queda-abierta)
  - [4.2 Por qué la distribución MoSCoW es la que es](#42-por-qué-la-distribución-moscow-es-la-que-es)
- [5. Refinamiento](#5-refinamiento)
- [6. Puntos abiertos de este backlog](#6-puntos-abiertos-de-este-backlog)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Objetivos del producto

Este backlog convierte en trabajo planificable los **diez** contratos de uso de `GeometriaFactory-Infrastructure`, la capa donde el producto **toca el mundo**: los **cuatro** puertos que implementa, los **dos** mecanismos de seguridad que las capas de adentro delegaron y la responsabilidad de dejar el almacén en condiciones antes de la primera petición.

**El MVP de este proyecto de código no se define acá.** Lo define el tramo comprometido —las **ocho** etapas `a` a `h` de `PRODUCT-INTAKE` §15— y el objetivo de avance de **8 de 8 etapas** (§22, asunción `A-2`). **Ninguna historia de este backlog cae fuera de ese tramo.**

**Este backlog no reordena las etapas ni las renombra.** Las **cinco** épicas de §2 son la partición de las etapas del roadmap que tocan a este proyecto de código, con el nombre de épica candidata que [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §3 ya declaró para cada una.

### 1.1 Qué significa nivel topológico 2 para este backlog

`02` §1 ubica a este proyecto de código en el **nivel 2**: depende de `GeometriaFactory-Application` y de `GeometriaFactory-Domain`, y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Tres consecuencias operativas:

1. **Ninguna historia se puede cerrar antes que el puerto que implementa esté declarado.** Dentro de cada etapa, el trabajo de las dos capas de adentro va primero: un puerto que allá no exista es un adaptador que acá no se puede escribir.
2. **Este proyecto de código no registra sus propios adaptadores.** Los declara y `GeometriaFactory-Api` los conecta; un registro automático desde acá haría que la frontera dejara de ser contable (`05` §3.2 punto 4).
3. **La mitad de este backlog no espera a nada.** Los dos motores del validador, el reloj y el mecanismo de credenciales **no tocan el almacén ni hacen red** (`05` §2 propiedad 2), de modo que se pueden construir y probar unitariamente sin base, sin frontera de proceso y sin ningún otro proyecto de código en pie.

### 1.2 Qué es una historia en la capa que toca el mundo

- **El rol de las veinticinco historias es el mismo**: el **código consumidor de la biblioteca**, que en el producto es la composición de raíz de `GeometriaFactory-Api` y, a través de ella, los casos de uso que la necesitan. El alumno y el administrador aparecen como sujetos de las reglas y nunca como actores.
- **Acá está el riesgo del producto, y el backlog tiene que reflejarlo.** `02` §1 declara que el intake asigna probabilidad **alta** e impacto **alto** a que **el validador se escriba sin leer el análisis**, porque el texto del alumno no es texto estrictamente válido. Es el **único** riesgo de negocio cuya mitigación declarada es una batería de pruebas, y esa batería vive acá: es la épica EP-05 entera.
- **Ninguna historia toma una decisión de negocio.** `02` §4 lo enuncia en una línea: **esta capa provee el mecanismo y no toma ninguna decisión de negocio**. Una historia que decidiera un estado, una autorización o una admisibilidad estaría mal ubicada.
- **Ninguna historia acuña una condición.** Las condiciones son **17**, su fuente es [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), **ninguna es un código de protocolo** y su traducción pertenece a `GeometriaFactory-Api`.
- **Varias historias entregan una terminación y no un efecto**, y es deliberado: cuando un mecanismo no puede cumplir su promesa, **se detiene y lo dice**; no la cumple a medias, no compone un valor por otro medio y no cae hacia un sustituto (`05` §2 propiedad 4).

## 2. Épicas

| Épica | Nombre | Etapa del producto | Descripción breve | Historias | Tareas técnicas |
| --- | --- | --- | --- | --- | --- |
| EP-01 | Esqueleto ambulante y verificación de viabilidad | `a` | El proyecto de código existe, el almacén se crea y se transforma al arrancar, y el arranque se detiene antes que operar sobre un almacén en el que no se puede confiar. **`PT-04` se mide acá** | US-24, US-25 | BT-01 a BT-08 |
| EP-02 | Identidad del administrador y sesión | `c` | El almacén sostiene la unicidad, responde las dos preguntas sobre el conjunto, deriva y verifica credenciales, y emite el acceso firmado con la clave que recibe y no busca | US-14, US-15, US-17, US-18, US-21, US-22, US-23 | BT-05, BT-09, BT-12, BT-13, BT-15, BT-21 |
| EP-03 | Ciclo de vida de la cuenta de alumno | `d` | La provisoria que el sistema produce, la marca que viaja sin ser un estado de cuenta, y el arrastre de la baja como única operación destructiva | US-13, US-16, US-19, US-20 | BT-09, BT-11, BT-14, BT-25 |
| EP-04 | Gestión del trabajo | `e` | El trabajo se materializa con su texto literal, la consulta se resuelve con el recorte ya trasladado y el retiro es físico y todo o nada | US-08, US-09, US-10, US-11, US-12 | BT-05, BT-10, BT-11 |
| EP-05 | Interpretación y verificación del dato del alumno | `f` | El validador de figuras: lectura tolerante con las **cuatro** trampas, derivación por tipo, tolerancia de **0.01** con operador estricto y la batería de **10** casos sobre los **ocho** escenarios | US-01 a US-07 | BT-16 a BT-20, BT-24 |

**Las etapas `b`, `g` y `h` no producen épica en este proyecto de código, y es declaración y no olvido.** La `b` construye la cáscara del front y la `g` la visualización; ninguna de las dos toca el almacén, los motores ni los mecanismos. La `h` es el circuito de revisión, y lo que esta capa aporta a él —guardar el estado terminal y el comentario del administrador— **ya está construido en la etapa `e`**: el comentario es **campo y no entidad, y sin historial** (`RC-07`), y la columna existe desde la transformación inicial del esquema. Agregar una épica en `h` habría creado trabajo que no existe.

## 3. Historias por épica

Las **veinticinco** historias son las que [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó —`US-01` a `US-25`, sin huecos—, con el mismo identificador y el mismo contenido; esta categoría las **confirma y las redacta**. Cada una vive en su archivo bajo [`historias-usuario/`](historias-usuario/), porque el proyecto de código supera las veinte historias.

### 3.1 EP-01 · Esqueleto ambulante y verificación de viabilidad

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-24](historias-usuario/US-24-Aplicar-Las-Transformaciones-De-Esquema-Al-Arrancar.md) | Aplicar las transformaciones de esquema al arrancar, sobre base inexistente | Must | Sin fijar (§4.1) | Propuesta | CU-10 | EP-01 |
| [US-25](historias-usuario/US-25-Detener-El-Arranque-En-Lugar-De-Operar-Sobre-Un-Almacen-Dudoso.md) | Detener el arranque en lugar de operar sobre un almacén en el que no se puede confiar | Must | Sin fijar (§4.1) | Propuesta | CU-10 | EP-01 |

**Es la única de las cinco épicas de etapa `a` del producto que tiene historias**, y el motivo es que `PT-04` se mide en esa etapa: la imagen del servicio de datos **aplica sus actualizaciones de esquema sobre base vacía y responde salud** (`Roadmap-Producto.md` §5.2, transición `a` → `b`). Sin estas dos historias, esa puerta no se puede medir.

### 3.2 EP-02 · Identidad del administrador y sesión

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-14](historias-usuario/US-14-Sostener-En-El-Almacen-La-Unicidad-Del-Correo-Y-La-Del-Administrador.md) | Sostener en el almacén la unicidad del correo y la del administrador | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-02 |
| [US-15](historias-usuario/US-15-Responder-Las-Dos-Preguntas-Sobre-El-Conjunto.md) | Responder si un correo está registrado y si ya existe una cuenta con papel `Administrador` | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-02 |
| [US-17](historias-usuario/US-17-Derivar-Una-Contrasena-Sin-Guardarla-Ni-Registrarla-En-Claro.md) | Derivar una contraseña sin guardarla ni registrarla en claro | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-02 |
| [US-18](historias-usuario/US-18-Verificar-Una-Credencial-Y-Distinguir-El-Derivado-Ilegible.md) | Verificar una credencial y distinguir el valor derivado ilegible de la contraseña equivocada | Must | Sin fijar (§4.1) | Propuesta | CU-06 | EP-02 |
| [US-21](historias-usuario/US-21-Emitir-El-Acceso-Firmado-Con-Sus-Cuatro-Reclamos.md) | Emitir el acceso firmado con sus cuatro reclamos | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-02 |
| [US-22](historias-usuario/US-22-Rechazar-La-Emision-Sin-Clave-De-Firma.md) | Rechazar la emisión sin clave de firma, sin generar una al vuelo | Must | Sin fijar (§4.1) | Propuesta | CU-08 | EP-02 |
| [US-23](historias-usuario/US-23-Proveer-El-Sello-Por-Un-Puerto-Para-Que-Las-Pruebas-Lo-Puedan-Fijar.md) | Proveer el sello por un puerto, para que las pruebas lo puedan fijar | **Should** | Sin fijar (§4.1) | Propuesta | CU-09 | EP-02 |

### 3.3 EP-03 · Ciclo de vida de la cuenta de alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-13](historias-usuario/US-13-Arrastrar-Todos-Los-Trabajos-De-Una-Cuenta-Dada-De-Baja.md) | Arrastrar todos los trabajos de una cuenta dada de baja, todo o nada | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-03 |
| [US-16](historias-usuario/US-16-Conservar-Y-Transportar-La-Marca-Sin-Alterar-El-Estado.md) | Conservar y transportar la marca de cambio de contraseña pendiente sin alterar el estado | Must | Sin fijar (§4.1) | Propuesta | CU-05 | EP-03 |
| [US-19](historias-usuario/US-19-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | Producir una contraseña provisoria no adivinable y sin repetirse | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-03 |
| [US-20](historias-usuario/US-20-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md) | Terminar sin producir valor cuando la fuente de aleatoriedad no responde | Must | Sin fijar (§4.1) | Propuesta | CU-07 | EP-03 |

### 3.4 EP-04 · Gestión del trabajo

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-08](historias-usuario/US-08-Conservar-El-Texto-Original-Literal-Y-Rechazar-Toda-Escritura-Que-Lo-Reemplace.md) | Conservar el texto original literal y rechazar toda escritura que lo reemplace | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-09](historias-usuario/US-09-Materializar-El-Trabajo-Con-Sus-Piezas-Componentes-Y-Observaciones.md) | Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-10](historias-usuario/US-10-Resolver-La-Consulta-Con-El-Recorte-Ya-Trasladado-Al-Pedido.md) | Resolver la consulta con el recorte ya trasladado al pedido | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-11](historias-usuario/US-11-Excluir-Componentes-Y-Texto-Original-Del-Resultado-De-Un-Listado.md) | Excluir componentes y texto original del resultado de un listado | Must | Sin fijar (§4.1) | Propuesta | CU-03 | EP-04 |
| [US-12](historias-usuario/US-12-Retirar-Fisicamente-Un-Trabajo-Con-Todo-Lo-Que-Cuelga-De-El.md) | Retirar físicamente un trabajo con todo lo que cuelga de él | Must | Sin fijar (§4.1) | Propuesta | CU-04 | EP-04 |

### 3.5 EP-05 · Interpretación y verificación del dato del alumno

| US | Título | MoSCoW | Estimación | Estado | CU relacionados | Épica |
| --- | --- | --- | --- | --- | --- | --- |
| [US-01](historias-usuario/US-01-Leer-El-Texto-Real-Con-Tolerancia-A-Comas-Finales-Y-Claves-Sinonimas.md) | Leer el texto real del alumno con tolerancia a comas finales y a las claves sinónimas | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-05 |
| [US-02](historias-usuario/US-02-Devolver-La-Cantidad-De-Figuras-Del-Conjunto-Raiz.md) | Devolver la cantidad de figuras del conjunto raíz, incluidas las no reconstruidas | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-05 |
| [US-03](historias-usuario/US-03-Reconstruir-Las-Piezas-Con-Su-Posicion-Y-Sus-Componentes.md) | Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-05 |
| [US-04](historias-usuario/US-04-Emitir-El-Error-De-Validacion-Con-Posicion-De-Figura-Y-Campo.md) | Emitir el error de validación con posición de figura y campo | Must | Sin fijar (§4.1) | Propuesta | CU-01 | EP-05 |
| [US-05](historias-usuario/US-05-Derivar-El-Valor-Desde-Las-Dimensiones-Y-Los-Componentes.md) | Derivar el valor desde las dimensiones y los componentes | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-05 |
| [US-06](historias-usuario/US-06-Comparar-Con-Tolerancia-Absoluta-Y-Operador-Estricto.md) | Comparar con tolerancia absoluta y **operador estricto** | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-05 |
| [US-07](historias-usuario/US-07-Emitir-La-Advertencia-Con-El-Valor-Declarado-Y-El-Derivado.md) | Emitir la advertencia con el valor declarado y el derivado, sin corregir ninguno | Must | Sin fijar (§4.1) | Propuesta | CU-02 | EP-05 |

## 4. Métricas de avance

| Prioridad | Cantidad de historias | Porcentaje | Estimación acumulada |
| --- | --- | --- | --- |
| Must | 24 | 96,0 % | Sin fijar (§4.1) |
| Should | 1 | 4,0 % | Sin fijar (§4.1) |
| Could | 0 | 0 % | — |
| Won't (v1.0) | 0 | 0 % | — |
| **Total** | **25** | **100 %** | **Sin fijar** |

| Métrica | Valor al 2026-08-10 |
| --- | --- |
| Historias en estado `Propuesta` | 25 de 25 |
| Historias cerradas | 0 de 25 |
| Porcentaje cerrado | 0 % |
| Historias dentro del tramo comprometido | **25 de 25**: este proyecto de código no tiene ninguna historia de la fase `i…` |
| Tareas técnicas declaradas | 26 |
| Tareas técnicas cerradas | 0 de 26 |
| Etapas del producto que este proyecto de código toca | 5 de las 8 comprometidas: `a`, `c`, `d`, `e` y `f` |
| Casos de la batería obligatoria del validador | 0 de **10**, con los **ocho** escenarios `E-1` a `E-8` del intake §20 como entrada |
| Deuda declarada en el backlog | **7** tareas técnicas que cierran o elevan un punto abierto: BT-02, BT-03, BT-19, BT-23, BT-24, BT-25 y BT-26 |

**El porcentaje cerrado no es una medida de avance del producto.** El avance se mide por **etapas cerradas y demostradas** (`Roadmap-Producto.md` §1.1).

### 4.1 Por qué la unidad de estimación queda abierta

**Este backlog no fija técnica de estimación, y lo declara en lugar de inventarla**, por los mismos tres motivos que los proyectos de código ya emitidos: sin plazo calendario y avance por etapas cerradas; unidad de planificación la **etapa** y no el sprint; y `equipo_n = 1`.

**Y hay un motivo propio, más fuerte todavía**: de los **catorce** NFR de `05` §8, **tres vienen rotulados [ASUNCIÓN]** desde el intake y siguen pendientes de confirmación del Product Owner —los 200 ms de la interpretación y las **tres** coberturas, incluida la de **95 %** del validador, que es el número más alto del producto—. Un backlog que usa como vigentes tres números sin confirmar, y que además inventara puntos de historia, tendría cuatro números sin respaldo en lugar de tres.

En consecuencia la columna `Estimación` dice **«Sin fijar»** en las veinticinco historias y en las veintiséis tareas técnicas, y la decisión queda como `PA-01` de §6.

### 4.2 Por qué la distribución MoSCoW es la que es

**24 `Must` y 1 `Should`**:

1. **La prioridad la declara el Product Owner en el intake y esta categoría no reprioriza.** Todas las capacidades que bajan a esta capa son `Must Have`: `F-01` a `F-12`, `F-22` a `F-24` y `F-26`.
2. **Las capacidades `Should`, `Could` y `Won't` del intake no bajan acá.** Son **siete** desde el 2026-08-10, y no ocho: `F-14` es del despliegue, `F-15` a `F-17` son de la fase `i…` y `F-18` a `F-20` están fuera del alcance de la primera versión. **`F-13` estaba en esta enumeración y ya no está**: el Product Owner la promovió a `Must Have` en `PRODUCT-INTAKE` **1.19**. Esta capa no la toca ni antes ni después —es de la visualización y de la presentación—, pero contar a una `Must Have` entre las de prioridad menor sería una afirmación falsa sobre la fuente.
3. **La única historia `Should` es US-23**, proveer el sello por un puerto. Y lo es por una razón que la propia categoría 02 ya dejó escrita: **`CU-09` es el único de los diez casos de uso que no traza a ninguna necesidad de negocio** (`02` §7.2). Su origen no es una capacidad sino **una decisión de testabilidad** —que los sellos sean verificables en prueba, `PRODUCT-INTAKE` §17.2.P.11 punto 3—. El producto funciona sin ella; lo que se pierde es que las pruebas de las capas de adentro sean reproducibles sin fijar el reloj del entorno.

**Lo que reemplaza acá al recorte por prioridad es el recorte por etapa.**

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

- **De una capacidad de la fase `i…`**, que este backlog no planifica pero que la frontera de tipos sí tiene que transportar: es el único caso, `US-10` de `GeometriaFactory-Contracts`, que deriva de `F-15`, `Could Have`.
- **De una decisión que no tomó el Product Owner sino la categoría 02 o la 05** de ese proyecto de código: `US-12` de Domain (una decisión técnica pre-tomada del intake §17.1.P.11), `US-16` de Application (`05` §4, la indisponibilidad de un puerto como condición), `US-23` de Infrastructure (testabilidad del sello, con el caso de uso que su `02` §7.2 declara sin necesidad de negocio) y `US-30` de Api (la estrategia de demostración de §16.1 y §18). Son **cuatro**, una por cada proyecto de código que **no toca la visualización**, y ésa es toda la regularidad: cada una de esas cuatro capas tomó exactamente una decisión propia que no responde a una capacidad, y esa decisión es lo que puede diferirse.

**Los dos proyectos de código que hoy quedan en 100 % `Must` son exactamente los dos cuya única no-`Must` derivaba de `F-13`** —el Visor y Web, desde los dos lados de la fachada—. No llegaron ahí eligiendo: llegaron porque la capacidad de la que dependían subió de prioridad, después de que los dos elevaran la tensión y **se negaran a repriorizarla por su cuenta**.

**La consecuencia hay que decirla y es incómoda**: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog**. No hay una lista de historias que se puedan soltar si el trabajo aprieta, porque el Product Owner ya priorizó aguas arriba y lo que quedó del lado de este backlog está comprometido. Lo que reemplaza a esa señal es el **orden de etapas**, que es la unidad de planificación que este producto sí tiene: si algo aprieta, se difiere una etapa entera, con su punto de control, y no una historia suelta.

## 5. Refinamiento

| Aspecto | Decisión |
| --- | --- |
| Cadencia | Una sesión de refinamiento **por etapa**, al abrir la rama de la etapa. No hay sprints (`Roadmap-Producto.md` §1.2) |
| Segunda sesión obligatoria | **Al abrir la etapa `f`**, sobre las siete historias del validador, con el análisis de las **cuatro trampas** del formato y los **ocho escenarios** sobre la mesa. Es la sesión que mitiga el riesgo de negocio del producto |
| Responsable | La única persona del equipo, con el papel de AG-06 |
| Formato | Revisión de la historia contra su contrato de uso de 02, contra el componente de `05` §3.1 que la sostiene y contra las **siete** reglas conceptuales de modelo cuando toca el almacén |
| Entrada obligatoria a la sesión | Las **17** condiciones del catálogo de 03 con su distinción entre **resultado** y **fallo**, las **cuatro** trampas del formato, y la lista de las **cinco** cosas que nunca entran en un mensaje ni en una traza, más el texto original del alumno |
| Qué produce la sesión | Historias en estado `Ready` según [`Definition-Of-Ready.md`](Definition-Of-Ready.md), o el registro de qué le falta a cada una |

**Dos reglas propias de este refinamiento.** La primera: cada vez que una historia agrega un camino de fallo, la sesión pregunta **si ese camino podría componer el valor por otro medio en lugar de detenerse**. `05` §9 declara como riesgo de impacto **muy alto** que la provisoria se componga por un contador, la fecha o el correo cuando la fuente de material impredecible no responde, porque **el reseteo parece haber funcionado**. La segunda: cada vez que una historia devuelve una condición, la sesión pregunta **si es un resultado o un fallo**; `05` §9 declara con probabilidad **alta** que un texto ilegible termine devolviendo la condición de servicio no disponible, y el alumno esperaría a que se recupere de un problema que no tiene.

## 6. Puntos abiertos de este backlog

| Id | Punto abierto | Quién lo cierra | Cuándo |
| --- | --- | --- | --- |
| PA-01 | **La unidad de estimación**, por lo declarado en §4.1 | El Product Owner, que es también quien ejecuta | Al cerrar la etapa `c` |
| PA-02 | **Los nombres definitivos de tipos y de espacios de nombres, y el criterio de nombrado del adaptador de cuentas** (`05` §11 `PA-01` y `PA-02`). El **identificador del cuarto puerto no se fija acá**: lo declara `GeometriaFactory-Application` y su ADR-02 lo ató al punto de control de la etapa `a`. Convertido en trabajo como BT-02 | El equipo en el punto de control de la etapa `a`, sobre la superficie de `GeometriaFactory-Application` | Etapa `a` |
| PA-03 | **Cuál de las dos funciones de derivación de clave se ancla, y con qué parámetros** (`05` §11 `PA-03`). El intake declara dos candidatas y **no elige**; `ADR-04` fija la **forma** —parámetros versionados junto al valor derivado, sin valor por defecto silencioso— y el **criterio de elección**. **La decisión es de este proyecto de código** (`PRODUCT-INTAKE` §17.3.P.1). Convertido en trabajo como BT-03 | El equipo en la etapa `a`, aplicando el criterio de `ADR-04` §7 | Etapa `a` |
| PA-04 | **Hasta dónde llega el conjunto de tipos reconstruibles** (`05` §11 `PA-04`). Los **seis** que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar, y **ninguna fuente enumera las clases de la actividad**. Convertido en trabajo como BT-24 | El Product Owner, con la enumeración de las clases de la actividad | Sin fecha comprometida |
| PA-05 | **El límite de tamaño del texto que se acepta** (`05` §11 `PA-05`). **No es de este proyecto de código decidirlo**: `ADR-06` §2 decide que el motor **no impone límite propio** y el valor y su forma de rechazo los fija la categoría 05 de `GeometriaFactory-Api`, que ya lo tomó. **No se convierte en trabajo acá** | La categoría 05 de `GeometriaFactory-Api` | Ya reasignado |
| PA-06 | **Cómo se sostiene que la provisoria «no se repite»** (`05` §11 `PA-06`). `CU-07` §10 adopta que la sostiene la impredecibilidad y **descarta** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas. Convertido en trabajo como BT-25 | El Product Owner, para confirmarla o reemplazarla | Sin fecha comprometida |
| PA-07 | **La frecuencia del respaldo y la fecha de última modificación de la cuenta** (`05` §11 `PA-07` y `PA-09`). La primera la fuente la declara «a definir por el docente»; la segunda **entraría por el dominio y no por acá**. Convertidos en trabajo como BT-26 | El Product Owner, con `09-Devops` y con `GeometriaFactory-Domain` | Sin fecha comprometida |
| PA-08 | **La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`** (`05` §11 `PA-10`), que **ninguna fuente enuncia** y que la categoría 02 declaró con su fundamento. Este backlog **la hereda y no la reabre**: su prueba forma parte de BT-21 | El Product Owner, para confirmarla o reemplazarla | Sin fecha comprometida |
| PA-09 | **Los valores rotulados [ASUNCIÓN]** de `05` §8 —los 200 ms y las **tres** coberturas—, pendientes en `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` (`05` §11 `PA-11`). Convertido en trabajo como BT-23 | El Product Owner sobre su propio documento | Antes de fijar las puertas de cobertura en 09 |
| PA-10 | **De dónde sale el valor derivado del área de una pieza volumétrica.** `CU-02` §10 adopta la **suma de los componentes** y lo declara como derivación, porque el intake la muestra dos veces así y una vez como fórmula, y las dos formas coinciden en el caso donde se cruzan. Convertido en trabajo como BT-19 | `05-Arquitectura-Tecnica` ya fijó la tabla; el Product Owner puede confirmarla | Al abrir la etapa `f` |

**`PA-08` de la categoría 05 no figura acá porque está resuelto**: los dos recuentos de escenarios que aquella categoría había levantado quedaron corregidos en `PRODUCT-INTAKE` **1.18**, y son **ocho**. Este backlog usa ocho y **no reabre** el punto.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del product backlog de `GeometriaFactory-Infrastructure`. Declara **cinco** épicas como partición de las etapas del producto que este proyecto de código toca, con el nombre de épica candidata que el roadmap §3 ya había declarado, y las tres etapas que no producen épica —`b`, `g` y `h`— con su motivo, incluido el de la `h`, cuyo aporte ya está construido en la `e`. Confirma y redacta las **veinticinco** historias que la categoría 02 previó, cada una en su archivo bajo `historias-usuario/`. Declara qué es una historia en la capa que toca el mundo, que **acá vive el único riesgo de negocio cuya mitigación declarada es una batería de pruebas**, y que varias historias entregan una terminación y no un efecto. Declara la unidad de estimación como **punto abierto**, con el fundamento propio de que tres de los catorce NFR ya vienen rotulados como asunción sin confirmar, y **US-23 como única `Should`** por derivar de una decisión de testabilidad y no de una capacidad —el único caso de uso del proyecto de código que no traza a ninguna necesidad—. Deja **diez** puntos abiertos, siete de ellos convertidos en tareas técnicas, uno reasignado a otro proyecto de código y uno heredado sin reabrir. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios, y **cierra el hallazgo `D-06-03`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **Ninguna historia de este backlog cambia de prioridad**: `F-13` es de la visualización y no baja a este proyecto de código, de modo que el reparto MoSCoW de §4 no se toca. **§4.2**: la enumeración de capacidades de prioridad menor del punto 2 pasa de **ocho a siete** y deja de incluir a `F-13`. **§4.2 (`D-06-03`)**: entra el bloque «Sobre la regularidad de esta distribución», que declara lo que hasta ahora estaba implícito —el recuento de los siete proyectos de código, contado de nuevo sobre las fichas y los índices inline: **175 `Must`, 4 `Should` y 1 `Could`** sobre 180— y lo explica sin forzar ninguna redistribución: como las **diecinueve** capacidades del tramo comprometido son hoy todas `Must Have`, toda historia no-`Must` tiene que venir o de una capacidad de la fase `i…` o de una decisión propia de las categorías 02 o 05, y se enumeran las cinco una por una. Se declara además la consecuencia: la señal de recorte que MoSCoW normalmente da **no está disponible en este backlog** y la reemplaza íntegramente el orden de etapas. Sube minor. |
