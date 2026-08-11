# Backlog técnico — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Backlog-Tecnico.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §2.2 (lo que hereda de los niveles 0 y 1), §3.1 (los **ocho** componentes), §3.4 (las **siete** fronteras), §5 (etapas del pipeline y puertas propias), §6 (vista de datos), §7 (cross-cutting), §8 (los **catorce** NFR), §9 (los **ocho** riesgos), §10.5 (los ocho escenarios contra la batería de diez casos) y §11 (sus once filas); las **siete** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md), [`../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) y [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) y las **siete** reglas conceptuales de [`../02-Especificacion-Funcional/Modelo-Datos/`](../02-Especificacion-Funcional/Modelo-Datos/); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) (las **17** condiciones); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15, §17.3 y §20
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones y anclajes](#21-ep-t01--fundaciones-y-anclajes)
  - [2.2 EP-T02 · Almacén, contexto y preparación](#22-ep-t02--almacén-contexto-y-preparación)
  - [2.3 EP-T03 · Adaptadores de puerto](#23-ep-t03--adaptadores-de-puerto)
  - [2.4 EP-T04 · Mecanismos de seguridad](#24-ep-t04--mecanismos-de-seguridad)
  - [2.5 EP-T05 · Validador de figuras](#25-ep-t05--validador-de-figuras)
  - [2.6 EP-T06 · Verificación y puntos abiertos](#26-ep-t06--verificación-y-puntos-abiertos)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **veintiséis** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de `05` §3.1, de una ADR, de un NFR de su §8, de un riesgo de su §9, de un punto abierto de su §11, de una regla conceptual de modelo de la categoría 02 o de una puerta del intake §17.3.P.8. **Siete** convierten en trabajo un punto abierto: BT-02, BT-03, BT-19, BT-23, BT-24, BT-25 y BT-26.

**Tres particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **La mitad de las tareas no toca el almacén, y eso es una decisión de arquitectura y no una casualidad.** Los dos motores del validador, el reloj y el mecanismo de credenciales **no abren el archivo de datos y no hacen red** (`05` §2 propiedad 2), y es lo que hace que la batería obligatoria del producto sea barata de correr y que el NFR de los **200 ms** sea atribuible a esta capa.
2. **La épica del validador es la mitigación del único riesgo de negocio del producto.** El intake declara con probabilidad **alta** e impacto **alto** que el validador se escriba sin leer el análisis; su mitigación declarada es una batería de pruebas, y es EP-T05 entera. Las **cuatro trampas del formato se escriben antes de leer texto**, no después de que algo falle.
3. **Una tarea cierra un punto abierto que ninguna otra capa puede cerrar: la función de derivación de clave.** El intake §17.3.P.1 la asigna a este proyecto de código y declara dos candidatas **sin elegir**; `ADR-04` fija la forma y el criterio, y BT-03 fija la elección concreta en la etapa `a`. **No es una decisión que se pueda delegar hacia arriba ni hacia abajo.**

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones y anclajes

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, que sus nombres queden cerrados en el punto de control y que **la función de derivación de clave quede anclada con sus parámetros versionados** |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, derivación de clave y la puerta de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16, §17.3.P.1, §17.3.P.7 y §17.3.P.8; `05` §11 `PA-01`, `PA-02` y `PA-03`; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) |
| Etapa | `a` |
| BT contenidas | BT-01, BT-02, BT-03, BT-04 |

### 2.2 EP-T02 · Almacén, contexto y preparación

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el mapa entre las **cinco** entidades y el esquema físico exista, que las transformaciones se apliquen solas sobre un almacén inexistente y que el arranque **se detenga antes que operar sobre un almacén en el que no se puede confiar** |
| Alcance | Contexto de persistencia y mapeo, preparación del almacén con linaje inmutable, puerta de transformaciones y la zona horaria de los sellos |
| Fuente upstream | `05` §3.1 (componente transversal y mecanismo de arranque), §5 (cuarta etapa del pipeline), §6, §7 fila de zona horaria; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md), [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md) |
| Etapa | `a`, porque `PT-04` se mide ahí |
| BT contenidas | BT-05, BT-06, BT-07, BT-08 |

### 2.3 EP-T03 · Adaptadores de puerto

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los **cuatro** puertos tengan **cuatro** adaptadores separados, que ninguno dependa de otro y que la proyección de listado no arrastre lo que el detalle sí lleva |
| Alcance | Adaptador de cuentas con su índice único, adaptador de trabajos con sus dos formas de lectura, retiro físico con todo o nada, y adaptador de reloj |
| Fuente upstream | `05` §3.1 y §3.4; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md), [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md); `RC-01`, `RC-02`, `RC-05`, `RC-06`, `RC-07` |
| Etapa | `c` el de cuentas y el de reloj, `d` la marca, `e` el de trabajos y el retiro |
| BT contenidas | BT-09, BT-10, BT-11, BT-12 |

### 2.4 EP-T04 · Mecanismos de seguridad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las **dos** piezas sensibles del producto vivan acá y sólo acá, y que **la producción de la contraseña provisoria** —la delegación explícita de las tres capas de arriba— no se pueda componer por otro medio |
| Alcance | Derivación y verificación de credenciales, producción de la provisoria, emisión y verificación del acceso firmado |
| Fuente upstream | `05` §3.1 (mecanismo de credenciales, mecanismo de acceso firmado), §7 filas de autenticación y de producción de la provisoria, §9 riesgos tercero y cuarto; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md), [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md); `RN-14`, `RN-16` |
| Etapa | `c` la derivación y el acceso, `d` la provisoria |
| BT contenidas | BT-13, BT-14, BT-15 |

### 2.5 EP-T05 · Validador de figuras

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el texto real del alumno se interprete con sus **cuatro** trampas, que los valores se verifiquen con tolerancia **0.01** y operador **estricto**, y que la batería de **10** casos pase con los **ocho** escenarios como entrada. **Es la mitigación del riesgo de negocio del producto** |
| Alcance | Motor de interpretación, motor de verificación, tabla de derivación por tipo, batería obligatoria y la puerta de cero red |
| Fuente upstream | `05` §3.1 (los dos motores), §8 filas de tiempo, de cobertura del validador, de tolerancia, de la batería y de peticiones de red, §9 riesgos primero y segundo, §10.5; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md); [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md); [`Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); `PRODUCT-INTAKE` §20 y §21 |
| Etapa | `f` |
| BT contenidas | BT-16, BT-17, BT-18, BT-19, BT-20 |

### 2.6 EP-T06 · Verificación y puntos abiertos

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el catálogo de **17** condiciones se cierre en las dos direcciones, que ningún mensaje ni traza lleve un secreto, la ruta del almacén o el texto del alumno, y que los cuatro puntos abiertos que quedan elevados tengan plazo |
| Alcance | Catálogo cerrado, prueba de inspección de secretos, valores rotulados como asunción y los tres puntos abiertos del Product Owner |
| Fuente upstream | `05` §7 fila de secretos y datos que no se registran, §8 filas de cobertura del catálogo y de mensajes con secretos; `05` §11 `PA-04`, `PA-06`, `PA-07`, `PA-09` y `PA-11`; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §1.4 |
| Etapa | `d` los tres primeros, y las elevaciones antes del punto de control de la etapa que las contiene |
| BT contenidas | BT-21, BT-22, BT-23, BT-24, BT-25, BT-26 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-01 | Crear el proyecto de código y su proyecto de pruebas | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.3.P.1; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación, con sus **dos** dependencias de compilación y ninguna más; el proyecto de pruebas existe y corre vacío; **la integración contra el almacén real pertenece a `GeometriaFactory-Api`** y no a este proyecto de pruebas | **Infraestructura compartida**: habilita a las 25 |
| BT-02 | Fijar los nombres de tipos y de espacios de nombres, y el criterio de nombrado del adaptador de cuentas | indagación | EP-T01 | `a` | Alta | Sin fijar | `05` §11 `PA-01` y `PA-02`; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6 | BT-01 | Los nombres quedan decididos y registrados en el punto de control. **El identificador del cuarto puerto no se fija acá**: lo declara `GeometriaFactory-Application` y su ADR-02 lo ató a ese mismo punto de control; esta tarea aporta el **criterio de nombrado del adaptador**, que es lo que sí le corresponde. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: los adaptadores dependen de que el nombre del puerto esté fijado |
| BT-03 | Anclar la función de derivación de clave y sus parámetros versionados | indagación | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.3.P.1, que declara **dos candidatas y no elige**; `05` §11 `PA-03`; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) §7 | BT-01 | La función queda **elegida** y su versión anclada según la regla de anclaje del producto; **los parámetros se versionan junto al valor derivado** y **no hay valor por defecto silencioso**; la elección aplica el criterio que `ADR-04` §7 fija. **Es una decisión de este proyecto de código y no se delega**: el intake se la asigna. **Caja temporal: la etapa `a`** | US-17, US-18 |
| BT-04 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §5, puertas propias; `05` §8, última fila; `PRODUCT-INTAKE` §17.3.P.8 | BT-01 | La etapa de construcción del pipeline termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-05 | Construir el contexto de persistencia y el mapeo de las cinco entidades | feature | EP-T02 | `a` | Alta | Sin fijar | `05` §3.1, componente transversal; `05` §6; [`Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md); [`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) | BT-01, BT-02 | Las **cinco** entidades del modelo conceptual tienen su correspondencia en el esquema físico, con sus tipos, sus índices y sus restricciones; **el esquema no lleva ninguna columna de pertenencia a instancia** —una instancia, un curso, un administrador—; el modo de diario con registro por delante y el **escritor único** quedan declarados; una escritura concurrente rechazada termina en su condición y **no en espera activa** | US-09, US-14, US-16, US-24 |
| BT-06 | Construir la preparación del almacén con linaje inmutable y arranque detenido | feature | EP-T02 | `a` | Alta | Sin fijar | `05` §3.1 (mecanismo de arranque), §4 última viñeta; [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); `05` §9, riesgos quinto y sexto | BT-05 | Las transformaciones **se aplican solas al arrancar** sobre un almacén inexistente o desactualizado; **una transformación ya fusionada no se edita**; ante un esquema que no corresponde **el arranque se detiene** y **jamás se descarta el almacén para crearlo de nuevo**; ante una ruta no disponible el arranque también se detiene y **no cae hacia ninguna ruta alternativa dentro de la imagen**. **No hay modo de sólo lectura ni arranque parcial** | US-24, US-25 |
| BT-07 | Puerta de transformaciones aplicadas sobre un almacén inexistente | devops | EP-T02 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.3.P.8; `05` §5 y §8, fila correspondiente; `Roadmap-Producto.md` §5.2, transición `a` → `b` (`PT-04`) | BT-06 | **1 de 1** intento exitoso, **sin paso manual**, sobre un almacén recién creado; es la cuarta etapa del pipeline y es **propia de este proyecto de código**; forma parte de lo que `PT-04` mide en la etapa `a` | **Infraestructura compartida**: es una puerta del producto |
| BT-08 | Fijar la zona horaria y la precisión de los sellos | feature | EP-T02 | `a` | Media | Sin fijar | `05` §7, fila de zona horaria, que **cierra un punto abierto de la categoría 02**; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) §2; `RC-06` | BT-05 | Los sellos **se producen y se guardan en tiempo universal coordinado**, con la precisión que el puerto de reloj entrega y **sin truncarla**; la conversión a la zona de quien lee **es de la superficie que lo muestra** y no de acá; los **tres** sellos de tiempo del trabajo se distinguen y no se confunden | US-09, US-23 |
| BT-09 | Construir el adaptador de repositorio de cuentas con el índice único | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente correspondiente; [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md); `05` §9, último riesgo; `RC-07` | BT-05, BT-02 | Recupera una cuenta por su correo, responde las **dos** preguntas sobre el conjunto y materializa el resultado **incluida la marca**; el **índice único sobre la forma normalizada del correo** es la segunda línea de la unicidad, con su condición declarada como camino y no como accidente; **el criterio de comparación de dos correos queda decidido acá**, que es lo que las dos capas de adentro derivaron a esta categoría | US-14, US-15, US-16 |
| BT-10 | Construir el adaptador de repositorio de trabajos con la proyección separada del detalle | feature | EP-T03 | `e` | Alta | Sin fijar | `05` §3.1, componente correspondiente; `05` §8, fila de componentes cargados; [`Contracts ADR-05`](../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md); `RC-01`, `RC-02` | BT-05 | Resuelve la consulta **ya acotada** y **no resuelve ninguna sin recorte declarado**; tiene **dos** formas de lectura, proyección y detalle; **0** componentes cargados y **0** apariciones del texto original en la proyección; **0** escrituras aceptadas que reemplacen el texto original conservado | US-08, US-09, US-10, US-11 |
| BT-11 | Construir el retiro físico con todo o nada y el arrastre de la baja | feature | EP-T03 | `e` | Alta | Sin fijar | `05` §3.1; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md); `RC-05`; `05` §8, fila de retiros parciales | BT-09, BT-10 | El retiro es **físico**, sin marca de borrado lógico; **0** retiros parciales tras una baja interrumpida: o se retira la cuenta con todos sus trabajos, o no se retira nada; es **la única operación destructiva del producto** y por eso su criterio es que **no queda nada** | US-12, US-13 |
| BT-12 | Construir el adaptador de reloj del sistema | feature | EP-T03 | `c` | Media | Sin fijar | `05` §3.1, componente correspondiente; `PRODUCT-INTAKE` §17.2.P.11 punto 3 | BT-01 | Devuelve el momento actual y **no depende del contexto de persistencia**; es el contrato más corto de la capa y **el que hace reproducibles los sellos en prueba**: con un doble, la batería de las capas de adentro no necesita fijar el reloj del entorno | US-23 |
| BT-13 | Construir el mecanismo de derivación y verificación de credenciales | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1, componente «Mecanismo de credenciales»; [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md); `05` §7, filas de autenticación y de secretos | BT-03 | La contraseña **nunca se guarda ni se registra en claro**; la verificación distingue el **valor derivado ilegible** de la contraseña equivocada; **no depende del contexto de persistencia** y se prueba unitariamente; los parámetros de derivación **llegan desde la composición de raíz y no se buscan** | US-17, US-18 |
| BT-14 | Construir la producción de la contraseña provisoria, no adivinable y sin repetirse | feature | EP-T04 | `d` | Alta | Sin fijar | `05` §3.1; [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md); `05` §7, fila de producción de la provisoria; `05` §9, tercer riesgo; `RN-14`, `RN-16` | BT-13 | El valor sale **íntegramente de la fuente de material impredecible del sistema**, con la longitud y el alfabeto que `ADR-05` fija; **0** provisorias iguales en dos producciones consecutivas sobre la misma cuenta y entre cuentas distintas, y **ninguna derivable del nombre, del correo ni de la fecha**; **la invocación no lleva ningún dato del acto que la motiva**, de modo que no puede distinguir habilitación de reseteo; **el valor no se registra en ninguna traza**. **Atajo prohibido y escrito: componer el valor por un contador, la fecha o el correo cuando la fuente no responde** | US-19, US-20 |
| BT-15 | Construir el mecanismo de acceso firmado con la clave que recibe y no busca | feature | EP-T04 | `c` | Alta | Sin fijar | `05` §3.1; `05` §7, fila de configuración; `05` §9, cuarto riesgo; `PRODUCT-INTAKE` §17.3.P.5 | BT-01 | Emite y verifica el acceso con sus **cuatro** reclamos; la clave de firma **se recibe desde afuera y no se busca**: si no llega, la condición correspondiente y **0** accesos emitidos; **jamás se genera una clave al vuelo y jamás se emite sin firmar**; la clave **no entra a ningún mensaje ni a ninguna traza** | US-21, US-22 |
| BT-16 | Construir el motor de interpretación con las cuatro trampas del formato | feature | EP-T05 | `f` | Alta | Sin fijar | `05` §3.1, componente correspondiente; [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md); [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md); `Definicion-Contrato-Del-Validador-De-Figuras.md`; `PRODUCT-INTAKE` §17.3.P.11 punto 1 | BT-01 | Las **cuatro** trampas `T1` a `T4` están escritas **antes de leer texto**: claves sinónimas del ortoedro, comas finales y omisión de comentarios, caras admitidas en sus dos formas, y **los valores calculados erróneos no se rechazan: se señalan**. Devuelve la **cantidad de figuras del conjunto raíz** incluidas las no reconstruidas, y **reserva la posición** de las que no pudo reconstruir. **No abre el almacén y no hace red** | US-01, US-02, US-03, US-04 |
| BT-17 | Construir el motor de verificación con tolerancia 0.01 y operador estricto | feature | EP-T05 | `f` | Alta | Sin fijar | `05` §3.1; `05` §7, fila de comparación de valores; `05` §8, fila de tolerancia; `PRODUCT-INTAKE` §17.3.P.10 | BT-16 | Se advierte cuando la diferencia absoluta es **mayor** que **0.01**, **nunca mayor o igual**. **No es asunción**: la fuente lo fija con su fundamento, y con «mayor o igual» el escenario `E-1` daría **3** advertencias en lugar de las **2** documentadas. Exige **las piezas ya reconstruidas**: sin ellas devuelve su condición y no «0 advertencias» | US-05, US-06, US-07 |
| BT-18 | Correr la batería de diez casos con los ocho escenarios como entrada | devops | EP-T05 | `f` | Alta | Sin fijar | `05` §8, fila de casos que pasan, y §10.5; `PRODUCT-INTAKE` §17.3.P.6, §17.3.P.8, §20 y §21 | BT-16, BT-17 | **10 de 10** casos pasan, con los **ocho** escenarios `E-1` a `E-8` como entrada; la batería es **unitaria y sin almacén**; **la cobertura del validador alcanza el mínimo declarado, que es el número más alto del producto**; **no se inventa ningún texto de prueba**. `E-7` no respalda ninguno de los diez y **se usa igual como cobertura adicional declarada**, porque es el único texto que ejercita los **seis** tipos reconstruibles | US-01 a US-07 |
| BT-19 | Fijar la tabla de derivación por tipo, incluida el área de una pieza volumétrica | indagación | EP-T05 | `f` | Media | Sin fijar | [`Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5; `CU-02` §10; `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | BT-17 | La tabla queda escrita, tipo por tipo. Para el área de una pieza volumétrica se adopta la **suma de los componentes**, que es lo que la fuente muestra dos veces, y **se declara como derivación** de la categoría 02 y no como transcripción; las dos formas coinciden en el caso donde se cruzan. **Caja temporal: al abrir la etapa `f`** | US-05, US-07 |
| BT-20 | Puerta de cero peticiones de red originadas por los dos motores | devops | EP-T05 | `f` | Alta | Sin fijar | `05` §8, fila correspondiente; `PRODUCT-INTAKE` §17.3.P.3, que declara que **el validador no hace red** | BT-16, BT-17 | Exactamente **0** peticiones de red originadas por los dos motores; se verifica por **inspección de sus dependencias** y con el criterio de aceptación correspondiente del contrato de uso. Es el **reflejo estructural** de `RA-02` en esta capa, que **no la alcanza** pero que la respeta desde afuera | **Infraestructura compartida**: sostiene que el validador reciba texto y devuelva observaciones, y nada más |
| BT-21 | Cerrar el catálogo de las 17 condiciones en las dos direcciones | feature | EP-T06 | `d` | Alta | Sin fijar | `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §1.2 y §1.3 | BT-09, BT-13, BT-15 | **100 %** de las **17** condiciones alcanzadas por al menos una prueba y **0** condiciones emitidas que no figuren en el catálogo, comparado **en las dos direcciones**; **ninguna condición es un código de protocolo**: su traducción es de `GeometriaFactory-Api`; la separación entre **resultado** y **fallo** queda ejercida, porque confundirlos haría que un texto ilegible pareciera un servicio caído | US-04, US-18, US-20, US-22, US-25 |
| BT-22 | Prueba de inspección de que ningún mensaje ni traza lleva secreto, ruta ni texto del alumno | devops | EP-T06 | `d` | Alta | Sin fijar | `05` §7, fila de secretos y datos que no se registran; `05` §8, fila correspondiente; `05` §10.4, `RA-03` | BT-21 | Exactamente **0** mensajes y **0** trazas contienen la clave de firma, la contraseña en claro, el valor derivado de una credencial, la contraseña provisoria producida o la ruta del almacén; y **0** contienen el **texto original del alumno**, que no es secreto y tampoco entra. Se verifica **en las dos direcciones**, sobre las 17 condiciones y sobre el registro del servidor. **`RA-03` es la única de las tres reglas de arquitectura con tramo acá, y es de disciplina y no de ignorancia**: esta capa **conoce** las tres cosas | **Infraestructura compartida**: es la contracara de que todo error mostrado quede registrado del lado del servidor |
| BT-23 | Confirmar los valores rotulados como asunción y fijar las tres puertas de cobertura | indagación | EP-T06 | `d` | Media | Sin fijar | `05` §8, tres primeras filas; `05` §11 `PA-11`; `PRODUCT-INTAKE` §22, asunciones `A-3` y `A-5` | BT-04, BT-18 | El Product Owner confirma o corrige los **200 ms** y las **tres** coberturas **sobre su propio documento**; hasta entonces se usan como vigentes y las puertas **no se declaran bloqueantes** en 09. **Ninguna de las salidas es inventar un número acá.** **Caja temporal: antes de fijar las puertas en 09** | **Infraestructura compartida**: condiciona las puertas del pipeline de todas las historias |
| BT-24 | Elevar hasta dónde llega el conjunto de tipos reconstruibles | indagación | EP-T06 | `f` | Media | Sin fijar | `05` §11 `PA-04`; `02` §11 | BT-16 | Queda declarado si alguna clase de la actividad emite un tipo fuera de los **seis** que los escenarios ejercitan. Hoy **ninguna fuente enumera las clases**, y un tipo fuera del conjunto produce error de validación, que es correcto **pero puede no ser lo deseado**. **Esta tarea eleva y no decide.** **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-25 | Elevar la forma de sostener que la provisoria «no se repite» | indagación | EP-T06 | `d` | Media | Sin fijar | `05` §11 `PA-06`; `CU-07` §10 | BT-14 | Queda registrado que la propiedad la sostiene **la impredecibilidad** y que se **descartó** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas y **el producto no guarda contraseñas en claro**. Es una **decisión derivada y no una transcripción**, y se eleva para que el Product Owner la confirme o la reemplace. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: la decisión es del Product Owner |
| BT-26 | Elevar la frecuencia del respaldo y la fecha de última modificación de la cuenta | indagación | EP-T06 | `d` | Baja | Sin fijar | `05` §11 `PA-07` y `PA-09`; `PRODUCT-INTAKE` §17.3.P.4 | BT-05 | Queda registrado que la **frecuencia del respaldo** la fuente la declara explícitamente «a definir por el docente» —**no es una omisión de esta categoría**— y que la **fecha de última modificación de la cuenta** el modelo del dominio **no la declara**, de modo que si el Product Owner la quisiera **entraría por el dominio y no por acá**. **Caja temporal: ninguna comprometida** | **Infraestructura compartida**: las dos decisiones son del Product Owner, con `09-Devops` y con `GeometriaFactory-Domain` |

**Diez tareas se justifican como infraestructura compartida** —BT-01, BT-02, BT-04, BT-07, BT-20, BT-22, BT-23, BT-24, BT-25 y BT-26— y las **dieciséis** restantes declaran al menos una historia consumidora. **Diez más dieciséis son veintiséis**, y ninguna queda sin una cosa ni la otra.

## 4. Trazabilidad BT ↔ US ↔ CU

Las veintiséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-01 | Infraestructura compartida (habilita a las 25) | CU-01 a CU-10 | ADR-01 |
| BT-02 | Infraestructura compartida | CU-01 a CU-10 | `05` §11 `PA-01` y `PA-02`, ADR-03 |
| BT-03 | US-17, US-18 | CU-06 | ADR-04, `05` §11 `PA-03` |
| BT-04 | Infraestructura compartida | — (puerta de construcción) | `05` §5 y §8 |
| BT-05 | US-09, US-14, US-16, US-24 | CU-03, CU-05, CU-10 | `05` §3.1, contexto de persistencia |
| BT-06 | US-24, US-25 | CU-10 | ADR-07 |
| BT-07 | Infraestructura compartida | CU-10 | `05` §5, cuarta etapa del pipeline |
| BT-08 | US-09, US-23 | CU-03, CU-09 | ADR-02 §2, `RC-06` |
| BT-09 | US-14, US-15, US-16 | CU-05 | ADR-03 |
| BT-10 | US-08, US-09, US-10, US-11 | CU-03 | `05` §3.1, adaptador de trabajos |
| BT-11 | US-12, US-13 | CU-04 | ADR-02, `RC-05` |
| BT-12 | US-23 | CU-09 | `05` §3.1, adaptador de reloj |
| BT-13 | US-17, US-18 | CU-06 | ADR-04 |
| BT-14 | US-19, US-20 | CU-07 | ADR-05 |
| BT-15 | US-21, US-22 | CU-08 | `05` §3.1, mecanismo de acceso firmado |
| BT-16 | US-01, US-02, US-03, US-04 | CU-01 | ADR-06, `Flujo-Ejecucion.md` |
| BT-17 | US-05, US-06, US-07 | CU-02 | ADR-06 |
| BT-18 | US-01 a US-07 | CU-01, CU-02 | `05` §8 y §10.5 |
| BT-19 | US-05, US-07 | CU-02 | `Flujo-Ejecucion.md` §5 |
| BT-20 | Infraestructura compartida | CU-01, CU-02 | `05` §8, fila de peticiones de red |
| BT-21 | US-04, US-18, US-20, US-22, US-25 | CU-01, CU-06, CU-07, CU-08, CU-10 | `05` §8, cobertura del catálogo |
| BT-22 | Infraestructura compartida | CU-01 a CU-10 | `05` §7 y §10.4, `RA-03` |
| BT-23 | Infraestructura compartida | — (puertas de cobertura y de tiempo) | `05` §11 `PA-11` |
| BT-24 | Infraestructura compartida | CU-01 | `05` §11 `PA-04` |
| BT-25 | Infraestructura compartida | CU-07 | `05` §11 `PA-06` |
| BT-26 | Infraestructura compartida | CU-03, CU-05 | `05` §11 `PA-07` y `PA-09` |

**Cobertura inversa: los diez casos de uso tienen al menos una tarea técnica que los realiza.** CU-01 en BT-16, BT-18, BT-20, BT-21, BT-22 y BT-24; CU-02 en BT-17, BT-18, BT-19, BT-20 y BT-22; CU-03 en BT-05, BT-08, BT-10, BT-22 y BT-26; CU-04 en BT-11 y BT-22; CU-05 en BT-05, BT-09, BT-22 y BT-26; CU-06 en BT-03, BT-13, BT-21 y BT-22; CU-07 en BT-14, BT-21, BT-22 y BT-25; CU-08 en BT-15, BT-21 y BT-22; CU-09 en BT-08 y BT-12; CU-10 en BT-05, BT-06, BT-07, BT-21 y BT-22.

**Cobertura de los ocho componentes de `05` §3.1.** Contexto de persistencia y mapeo en BT-05 y BT-08; Adaptador de repositorio de trabajos en BT-10 y BT-11; Adaptador de repositorio de cuentas en BT-09 y BT-11; Motor de interpretación de figuras en BT-16; Motor de verificación de valores en BT-17 y BT-19; Adaptador de reloj del sistema en BT-12; Mecanismo de credenciales en BT-13 y BT-14; Mecanismo de acceso firmado y preparación del almacén en BT-15 y BT-06. **Los ocho tienen tarea técnica.**

**Cobertura de las siete reglas conceptuales de modelo.** `RC-01` en BT-10; `RC-02` en BT-10 y BT-16; `RC-03` en BT-05 y BT-17; `RC-04` en BT-05; `RC-05` en BT-11; `RC-06` en BT-08; `RC-07` en BT-09 y BT-05. **Las siete quedan materializadas y ninguna se enuncia acá**: las enuncia la categoría 02.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Infrastructure`. Declara **seis** épicas técnicas —fundaciones, almacén, adaptadores, mecanismos de seguridad, validador y verificación— y **veintiséis** tareas técnicas inline, cada una con tipo, fuente upstream por identificador, etapa, dependencias, criterios de aceptación verificables y las historias que la consumen. Declara las tres particularidades del proyecto de código: que **la mitad de las tareas no toca el almacén** y por eso la batería obligatoria es barata de correr, que **la épica del validador es la mitigación del único riesgo de negocio del producto**, y que **BT-03 cierra un punto abierto que ninguna otra capa puede cerrar**, la función de derivación de clave que el intake §17.3.P.1 asigna a este proyecto de código declarando dos candidatas sin elegir. Deja escritos los dos atajos prohibidos que `05` §9 identifica como de impacto muy alto: componer la provisoria por otro medio y descartar el almacén ante un esquema que no corresponde. Emite la matriz BT ↔ US ↔ CU con sus veintiséis filas, la cobertura inversa sobre los diez casos de uso, la de los **ocho** componentes y la de las **siete** reglas conceptuales de modelo. |
