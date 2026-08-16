# Mini-plan — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Mini-Plan.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las cuatro capas planifican sobre las mismas secciones**, y el plan de la unidad es la unión: el orden entre ellas lo fija el grafo de compilación del manifiesto, no este documento.

---

## 1. Información general

### 1.1 `GeometriaFactory-Api`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f` y `h` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **3**, el último: es el **único de los siete que ensambla a los demás** (`PRODUCT-INTAKE` §13) |
| Unidad de despliegue | **Una imagen de contenedor**, y es la unidad desplegable del backend. Es una de las **dos** del producto |
| Puertas técnicas propias | **`PT-04`**, medida en la etapa `a`: la imagen se construye, arranca, aplica las transformaciones sobre almacén vacío y responde salud |
| Paralelismo entre etapas | **Ninguno** (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`.

**Y hay un segundo motivo**: este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona.

Y hay un motivo propio, que en este proyecto de código es el más pesado del producto: de los **diecisiete** requerimientos no funcionales de `05` §8, **cinco vienen rotulados como asunción** y siguen pendientes de confirmación —latencia, caudal, arranque en frío, cobertura y **la forma misma de la pirámide de pruebas**—. Es la **mayor concentración de valores sin confirmar de los siete proyectos de código**. Este plan **los usa como vigentes porque no los inventó**, y agregarle una capacidad en puntos sería inventar el sexto.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa (`PRODUCT-INTAKE` §10).

### 1.2 `GeometriaFactory-Domain`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f`, `h` |
| Duración de cada etapa | **Sin fecha.** El intake declara sin plazo calendario y el avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **0**, sin dependencias salientes (`Vista-Producto.md` §3) |
| Paralelismo entre etapas | **Ninguno.** Las etapas son estrictamente secuenciales y sin OK explícito no se avanza (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`. `Roadmap-Producto.md` lo declara tres veces —en su §2.1, en su §3 y en su §6— y el propio intake lo repite en su tabla de trazabilidad downstream. En consecuencia **no se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo, que conviene decir aparte**: aunque el equipo creciera, este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión, y su métrica de avance es la etapa cerrada y demostrada. Un archivo de velocidad sobre un producto sin iteraciones cerradas y sin plazo calendario contendría números que ninguna fuente sostiene.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** La regla de la categoría pide capacidad en puntos o en horas con un factor de foco; ninguna fuente de este producto da base para ninguno de los dos. El intake declara sin plazo calendario, no hay historial de iteraciones y el equipo es de una persona que además es el Product Owner y el docente de la cátedra.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa, que el intake §10 nombra como el que fija el ritmo. Ese es el gobierno real del avance, y ponerle un número de puntos al lado lo volvería menos legible, no más.

### 1.3 `GeometriaFactory-Application`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Seis**: `a`, `c`, `d`, `e`, `f` y `h` |
| Duración de cada etapa | **Sin fecha.** El intake declara sin plazo calendario y el avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **1**, con una sola dependencia saliente: `GeometriaFactory-Domain` (`Vista-Producto.md` §3) |
| Etapas del pipeline | `restore` → `build` → `test`, con las puertas bloqueantes de `05` §8 (`05` §5) |
| Paralelismo entre etapas | **Ninguno.** Las etapas son estrictamente secuenciales y sin OK explícito no se avanza (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`. `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. En consecuencia **no se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo, que conviene decir aparte**: aunque el equipo creciera, este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión, y su métrica de avance es la etapa cerrada y demostrada.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: el intake declara sin plazo calendario, no hay iteraciones cerradas y el equipo es de una persona que además es el Product Owner.

Y hay un motivo propio de este proyecto de código: **el único NFR de tiempo que lo alcanza ya viene rotulado como asunción sin confirmar** —los 500 ms del caso de uso más pesado, `05` §8 y §11 `PA-05`—. Declarar acá una capacidad en puntos agregaría un segundo número sin respaldo al lado del primero, en lugar de reemplazarlo.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa, que el intake §10 nombra como el que fija el ritmo.

### 1.4 `GeometriaFactory-Infrastructure`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Cinco**: `a`, `c`, `d`, `e` y `f` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **2**, con dos dependencias de compilación y **un solo consumidor**: la composición de raíz de `GeometriaFactory-Api` |
| Etapas del pipeline | `restore` → `build` → `test` → **verificación de transformaciones de esquema**, que es **propia de este proyecto de código** (`05` §5) |
| Puertas técnicas del producto que lo alcanzan | **`PT-04`**, en su parte de que la imagen **aplique las actualizaciones de esquema sobre base vacía**, medida en la etapa `a` |
| Paralelismo entre etapas | **Ninguno** (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`, y su ausencia es decisión declarada y no omisión.

**Y hay un segundo motivo**: este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona.

Y hay un motivo propio de este proyecto de código: de los **catorce** requerimientos no funcionales de `05` §8, **tres vienen rotulados como asunción** desde el intake y siguen pendientes de confirmación —los 200 ms de la interpretación y las **tres** coberturas, incluida la de **95 %** del validador, que es el número más alto del producto—. Declarar acá una capacidad en puntos agregaría un cuarto número sin respaldo, y este plan **usa los tres primeros como vigentes precisamente porque no los inventó**.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa (`PRODUCT-INTAKE` §10).

## 2. Objetivo de cada tramo

### 2.1 `GeometriaFactory-Api`

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El servicio arranca en dos fases con los **cuatro** puertos conectados, deja el almacén en condiciones o **se detiene**, responde salud sin exigir acceso, y su imagen se construye y arranca: **`PT-04` medida**. Y queda verificado que **la sesión interactiva del front no llega hasta acá**. |
| `c` | El canje de credenciales funciona con sus dos respuestas —la genérica y la que declara el motivo—, la guardia admite los once puntos que exigen acceso, y las **dos** traducciones ocurren en una tabla única sin códigos inventados. |
| `d` | El administrador gobierna la comisión desde la superficie: listado, cambio de situación con la provisoria devuelta, baja con el correo escrito y reseteo, y **ningún punto queda fuera de la guardia del cambio pendiente salvo uno**. |
| `e` | Los cinco puntos sobre trabajos están en pie, con el texto que **no se normaliza en el borde**, la eliminación verificada **forzando la petición** y el listado sin parámetro para pedir borradores ajenos. |
| `f` | El envío y el reenvío **responden con éxito** transportando el estado que la interpretación decidió, y el texto viaja byte a byte. |
| `h` | El desenlace está expuesto con su terminalidad, y **la colección de peticiones se reproduce en cinco pasos o menos sin datos inventados**. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. El motivo está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2: la `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

### 2.2 `GeometriaFactory-Domain`

Una frase por etapa, orientada a lo que queda disponible al cerrarla. Las frases son de este proyecto de código: el objetivo de la etapa a nivel producto vive en `Roadmap-Producto.md` §2.1 y no se reescribe acá.

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | La biblioteca de dominio existe, compila con cero dependencias salientes y sus nombres y su cálculo de versión quedaron fijados en el punto de control. |
| `c` | El consumidor puede constituir la única cuenta de administrador, evaluar la admisibilidad de una cuenta y reemplazar su credencial exigiendo la vigente. |
| `d` | El consumidor puede recorrer el ciclo de vida completo de una cuenta de alumno, incluido el reseteo que conserva sus trabajos y la marca que le impide operar hasta cambiar la provisoria. |
| `e` | El consumidor puede constituir y reeditar un trabajo con dueño, y resolver qué ve el alumno y qué ve el administrador. |
| `f` | El consumidor puede adoptar el conjunto de piezas y las observaciones de un trabajo, y resolver si el envío lo lleva a estado `Pendiente` o lo deja en `Borrador`. |
| `h` | El consumidor puede resolver el desenlace de un trabajo en estado `Pendiente` y la eliminación por el administrador, con los dos estados terminales operativos. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. No es un hueco: es lo que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara con su motivo.

### 2.3 `GeometriaFactory-Application`

Una frase por etapa, orientada a lo que queda disponible al cerrarla. El objetivo de la etapa a nivel producto vive en `Roadmap-Producto.md` §2.1 y no se reescribe acá.

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | La biblioteca de casos de uso existe, compila con una sola dependencia saliente, y los nombres de sus tipos y **el del cuarto puerto** quedaron fijados en el punto de control. |
| `c` | El consumidor puede configurar la única cuenta de administrador, consultar la admisibilidad de una cuenta con su motivo y pedir el reemplazo de una credencial exigiendo la vigente, con la guarda de autorización ya en pie. |
| `d` | El consumidor puede recorrer el ciclo de vida completo de una cuenta de alumno, incluidos la provisoria que produce la habilitación, el reseteo que conserva todos sus trabajos y la comprobación que impide operar hasta cambiarla. |
| `e` | El consumidor puede constituir, reeditar y retirar un trabajo con dueño, y resolver las dos consultas con su predicado de alcance ya aplicado y sin componentes en el listado. |
| `f` | El consumidor puede enviar un trabajo, interpretarlo por el puerto y dejar que el dominio resuelva entre `Borrador` y estado `Pendiente`, sin tocar la base de datos en ninguna prueba. |
| `h` | El consumidor puede resolver el desenlace de un trabajo en estado `Pendiente`, eliminarlo con el alcance del administrador y devolverle al alumno su desenlace y su comentario. |

**Las etapas `b` y `g` no producen trabajo en este proyecto de código**, y por eso no tienen fila. No es un hueco: es lo que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara con su motivo.

### 2.4 `GeometriaFactory-Infrastructure`

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El proyecto de código existe, **la función de derivación de clave está anclada con sus parámetros versionados**, y el almacén se crea y se transforma solo al arrancar, deteniendo el arranque antes que operar sobre un almacén en el que no se puede confiar. |
| `c` | El almacén sostiene por sí mismo las dos unicidades y responde las dos preguntas sobre el conjunto; la contraseña se deriva y se verifica sin quedar nunca en claro; y el acceso firmado se emite con la clave que se recibe y no se busca. |
| `d` | La contraseña provisoria la produce el sistema, no es adivinable y no se repite; la marca viaja con la cuenta sin ser un estado; y la baja arrastra todos los trabajos, todo o nada. |
| `e` | El trabajo se materializa con su texto literal y todo lo que cuelga de él, la consulta se resuelve sólo con su recorte declarado y el listado no arrastra componentes ni texto. |
| `f` | El texto real del alumno se interpreta con sus cuatro trampas, los valores se verifican con tolerancia **0.01** y operador **estricto**, y **la batería de diez casos pasa con los ocho escenarios del intake como entrada**. |

**Las etapas `b`, `g` y `h` no producen trabajo en este proyecto de código**, y por eso no tienen fila. El motivo, incluido el de la `h` —cuyo aporte ya está construido en la `e`—, está en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2.

## 3. Ítems comprometidos por tramo

### 3.1 `GeometriaFactory-Api`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-00001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas de integración | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00005 | Tarea técnica | Anclar nombres, espacios de nombres y versiones de paquetes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00002 | Tarea técnica | Composición de raíz con los cuatro puertos y sus adaptadores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00007 | Tarea técnica | Fijar rutas y verbos de los quince puntos en el punto de control | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00008 | Tarea técnica | Fijar el formato de intercambio para los dos extremos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00009 | Tarea técnica | Fijar el límite de cuerpo que rechaza y nunca trunca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00010 | Tarea técnica | Fijar la vigencia del acceso firmado | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00003 | Tarea técnica | Arranque en dos fases con el punto de salud sin acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00006 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-00004 | Tarea técnica | Imagen multietapa y medición de `PT-04` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00026 | Historia | Conectar cada puerto con su adaptador y tomar la configuración | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00027 | Historia | Aplicar las transformaciones de esquema al arrancar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00028 | Historia | Detener el arranque en lugar de atender sobre un almacén dudoso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-00029 | Historia | Responder por el estado del servicio sin exigir acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00011 | Tarea técnica | Guardia de admisión transversal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00013 | Tarea técnica | Traductor con la tabla única, sin códigos inventados | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00016 | Tarea técnica | Superficie de acceso y credencial propia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00022 | Tarea técnica | Batería de integración con la pirámide invertida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00014 | Tarea técnica | Prueba de las tres familias deliberadamente empobrecidas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00012 | Tarea técnica | Inspección de los quince puntos contra la guardia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-00015 | Tarea técnica | Elevar los dos huecos del conjunto cerrado de códigos | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00001 | Historia | Canjear correo y contraseña por un acceso firmado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00002 | Historia | Responder credenciales inválidas sin declarar qué campo falló | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00003 | Historia | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00004 | Historia | Rechazar toda petición sin acceso, vencido o con firma ajena | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00005 | Historia | Exigir el papel declarado por cada punto de acceso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00008 | Historia | Configurar la cuenta de administrador sólo mientras no exista ninguna | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00010 | Historia | Cambiar la contraseña propia exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00024 | Historia | Traducir cada código del contrato al código de respuesta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-00025 | Historia | Responder sin exponer direcciones internas y registrar en el servidor | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-00017 | Tarea técnica | Superficie de gobierno de la comisión | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-00025 | Tarea técnica | Confirmar los cinco valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00006 | Historia | Guardia del cambio pendiente en todos los puntos salvo uno | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00007 | Historia | Registrar una cuenta de alumno sin campo de contraseña | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00009 | Historia | Cambiar la contraseña propia con la provisoria como vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00011 | Historia | Listar las cuentas de la comisión con su situación y su marca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00012 | Historia | Cambiar la situación de una cuenta con verificación de papel | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00013 | Historia | Dar de baja transportando el correo escrito como confirmación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00014 | Historia | Resetear y devolver la provisoria una sola vez | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00015 | Historia | No exigir ni comprobar la situación de la cuenta al resetear | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-00016 | Historia | No registrar la provisoria en ninguna traza | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00018 | Tarea técnica | Superficie de trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00024 | Tarea técnica | Prueba del texto byte a byte y del rechazo sin truncamiento | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-00023 | Tarea técnica | Prueba de eliminación forzada contra la superficie | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00019 | Historia | Transportar el texto original sin normalizarlo en el borde | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00020 | Historia | Eliminar con los dos alcances, verificado forzando la petición | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00021 | Historia | Listar sin parámetro para pedir borradores ajenos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-00022 | Historia | Detalle con piezas, componentes, observaciones y comentario | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-00017 | Historia | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-00018 | Historia | Reenviar un trabajo en `Borrador` con el texto corregido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00019 | Tarea técnica | Superficie de desenlace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00020 | Tarea técnica | Colección de peticiones reproducible | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00021 | Tarea técnica | Elevar el alcance de la colección de peticiones | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-00026 | Tarea técnica | Probar una vez la construcción de la imagen en destino | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-00023 | Historia | Aprobar o rechazar un trabajo en estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-00030 | Historia | Ejercitar la superficie con una colección reproducible | Media | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 30 historias y 26 tareas técnicas**, repartidas en seis etapas. La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog.

**US-00030 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`: es la única historia de este backlog donde las dos coinciden en señalar lo mismo.

### 3.2 `GeometriaFactory-Domain`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**. La columna de estimación queda vacía por §1.2 y la de asignación es la única persona del equipo.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-02001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, sin dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02002 | Tarea técnica | Fijar los nombres de tipos y de espacios de nombres, y validarlos en el punto de control | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02003 | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02004 | Tarea técnica | Puerta bloqueante de cero dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-02005 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-02006 | Tarea técnica | Núcleo de entidades con las cinco entidades del modelo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-02007 | Tarea técnica | Superficie pública de guardas con resultado tipado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-02010 | Tarea técnica | Guardas de cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-02011 | Tarea técnica | Evaluador de admisibilidad como puerta única | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-02024 | Historia | Configurar la cuenta de administrador en el primer arranque | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-02025 | Historia | Rechazar la configuración de un segundo administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-02008 | Historia | Evaluar la admisibilidad de la cuenta y devolver su motivo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-02007 | Historia | Reemplazar la credencial derivada exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-02009 | Tarea técnica | Momento y unicidad por parámetro | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-02014 | Tarea técnica | Matriz de ejercicio de los nueve invariantes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-02015 | Tarea técnica | Confirmar los dos valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-02016 | Tarea técnica | Decidir el criterio de comparación de dos correos | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02001 | Historia | Constituir un alumno con cuenta `Pendiente` y sin credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02002 | Historia | Rechazar el alta con datos obligatorios ausentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02003 | Historia | Exigir la unicidad del correo verificada en el alta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02004 | Historia | Habilitar, bloquear y rehabilitar una cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02005 | Historia | Dar de baja una cuenta arrastrando sus trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02006 | Historia | Fijar la credencial derivada provisoria en el acto de habilitación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02026 | Historia | Resetear la contraseña conservando cuenta y trabajos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-02027 | Historia | Exigir el cambio de la provisoria antes de toda otra capacidad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-02012 | Tarea técnica | Máquina de estados del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-02009 | Historia | Constituir un trabajo con dueño, identidad y texto original | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-02010 | Historia | Reeditar un trabajo en `Borrador` descartando la interpretación anterior | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-02018 | Historia | Resolver la pertenencia de un trabajo a su dueño | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-02019 | Historia | Acotar al estado `Borrador` lo que el alumno reedita y elimina | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-02022 | Historia | Excluir los trabajos en `Borrador` del alcance del administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-02008 | Tarea técnica | Cerrar el catálogo de las 42 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-02013 | Tarea técnica | Adopción de la interpretación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02011 | Historia | Reconstruir el conjunto de piezas con identidad posicional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02012 | Historia | Derivar la familia plana o volumétrica desde el tipo | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02013 | Historia | Registrar advertencias con el valor declarado y el derivado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02014 | Historia | Registrar errores de validación con posición de pieza y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02015 | Historia | Enviar un trabajo que verifica y pasa a estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02016 | Historia | Enviar un trabajo que no verifica y queda en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-02017 | Historia | Rechazar toda transición desde un estado terminal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-02020 | Historia | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-02021 | Historia | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-02023 | Historia | Eliminar por el administrador en los tres estados que ve | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 27 historias y 16 tareas técnicas, repartidas en seis etapas.** La prioridad de la columna es de ejecución dentro de la etapa y no reemplaza a la MoSCoW del backlog, que vive en 06.

### 3.3 `GeometriaFactory-Application`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**. La columna de estimación queda sin valor por §1.2 y la de asignación es la única persona del equipo.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-04001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas, con una sola dependencia saliente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04002 | Tarea técnica | Fijar los nombres de tipos, de espacios de nombres y el del cuarto puerto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04003 | Tarea técnica | Elegir y anclar la herramienta que calcula la versión | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04004 | Tarea técnica | Puerta bloqueante de dependencias salientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04005 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-04006 | Tarea técnica | Puerta propia de cero pruebas que tocan la base de datos real | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04007 | Tarea técnica | Declarar los cuatro puertos como frontera | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04010 | Tarea técnica | Guarda de autorización con las cuatro comprobaciones en orden fijo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04012 | Tarea técnica | Orquestación del alta de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-04014 | Tarea técnica | Orquestación del ingreso y la credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04003 | Historia | Configurar la cuenta de administrador con su ventana de alta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04028 | Historia | Rechazar la configuración de un segundo administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04007 | Historia | Devolver el motivo de una cuenta que no admite ingreso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-04009 | Historia | Reemplazar la credencial derivada exigiendo la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04011 | Tarea técnica | Matriz de ejercicio de las cuatro comprobaciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04013 | Tarea técnica | Orquestación del gobierno de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04018 | Tarea técnica | Confirmar los dos valores rotulados como asunción | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04020 | Tarea técnica | Elevar los sellos de alta, modificación y desenlace | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-04021 | Tarea técnica | Acompañar la decisión del criterio de comparación de correos | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04001 | Historia | Constituir una cuenta de alumno `Pendiente` y sin credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04002 | Historia | Rechazar el alta con un correo ya registrado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04004 | Historia | Habilitar, bloquear y rehabilitar con verificación de facultad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04005 | Historia | Dar de baja exigiendo el correo escrito como confirmación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04006 | Historia | Arrastrar en la baja todos los trabajos de la cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04008 | Historia | Fijar la credencial derivada provisoria dentro de la habilitación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04029 | Historia | Resetear la contraseña de un alumno con verificación de facultad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04030 | Historia | Impedir que una cuenta marcada ejerza cualquier otra capacidad | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04031 | Historia | Conservar la cuenta, su estado y todos sus trabajos tras el reseteo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-04032 | Historia | Levantar la marca con el cambio hecho por la propia cuenta | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04009 | Tarea técnica | Fijar el alcance de la unidad de trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04015 | Tarea técnica | Orquestación del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-04016 | Tarea técnica | Orquestación de la consulta, con la proyección sin componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04010 | Historia | Cargar un trabajo con dueño, identificador propio y sello del reloj | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04011 | Historia | Conservar el texto original íntegro al cargar y al reeditar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04012 | Historia | Reeditar sólo un trabajo propio en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04017 | Historia | Listar los trabajos propios con los cuatro estados distinguibles | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04019 | Historia | Detalle con piezas y componentes, y listado sin componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04020 | Historia | Listar los trabajos de la comisión excluyendo los borradores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04021 | Historia | Filtrar el listado de la comisión por alumno | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04022 | Historia | Abrir el detalle de un trabajo de la comisión | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-04026 | Historia | Eliminar un trabajo propio sólo en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-04019 | Tarea técnica | Medir el tiempo del caso de uso más pesado sobre `E-1`, sin base | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-04008 | Tarea técnica | Resultado tipado y catálogo de las 36 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04013 | Historia | Enviar un trabajo con advertencias y que pase a estado `Pendiente` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04014 | Historia | Enviar un trabajo con errores y que quede en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04015 | Historia | Interpretar el texto por el puerto, sin tocar la base de datos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-04016 | Historia | Terminar de forma controlada cuando la interpretación no está disponible | Media | Sin fijar | Equipo (1) | Pendiente |
| `h` | BT-04017 | Tarea técnica | Orquestación del desenlace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04018 | Historia | Ver el desenlace y el comentario del trabajo propio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04023 | Historia | Aprobar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04024 | Historia | Rechazar un trabajo en estado `Pendiente`, con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04025 | Historia | Rechazar toda transición sin facultad o desde un estado terminal | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-04027 | Historia | Eliminar por el administrador en los tres estados que ve | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 32 historias y 21 tareas técnicas, repartidas en seis etapas.** La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog, que vive en 06.

**US-04016 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`. Es la única historia de este backlog donde las dos coinciden en señalar lo mismo: si la etapa `f` aprieta, es la primera candidata a diferirse.

### 3.4 `GeometriaFactory-Infrastructure`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-06001 | Tarea técnica | Crear el proyecto de código y su proyecto de pruebas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06002 | Tarea técnica | Fijar nombres y el criterio de nombrado del adaptador de cuentas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06003 | Tarea técnica | Anclar la función de derivación de clave y sus parámetros versionados | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06005 | Tarea técnica | Contexto de persistencia y mapeo de las cinco entidades | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06008 | Tarea técnica | Fijar la zona horaria y la precisión de los sellos | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06006 | Tarea técnica | Preparación del almacén con linaje inmutable y arranque detenido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06004 | Tarea técnica | Puerta de construcción con cero advertencias | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-06007 | Tarea técnica | Puerta de transformaciones sobre un almacén inexistente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-06024 | Historia | Aplicar las transformaciones de esquema al arrancar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | US-06025 | Historia | Detener el arranque en lugar de operar sobre un almacén dudoso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06009 | Tarea técnica | Adaptador de repositorio de cuentas con el índice único | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06012 | Tarea técnica | Adaptador de reloj del sistema | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06013 | Tarea técnica | Mecanismo de derivación y verificación de credenciales | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-06015 | Tarea técnica | Mecanismo de acceso firmado con la clave que recibe y no busca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06014 | Historia | Sostener en el almacén la unicidad del correo y la del administrador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06015 | Historia | Responder las dos preguntas sobre el conjunto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06017 | Historia | Derivar una contraseña sin guardarla ni registrarla en claro | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06018 | Historia | Verificar una credencial y distinguir el derivado ilegible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06021 | Historia | Emitir el acceso firmado con sus cuatro reclamos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06022 | Historia | Rechazar la emisión sin clave de firma | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-06023 | Historia | Proveer el sello por un puerto | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06014 | Tarea técnica | Producción de la contraseña provisoria, no adivinable y sin repetirse | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06021 | Tarea técnica | Cerrar el catálogo de las 17 condiciones en las dos direcciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06022 | Tarea técnica | Inspección de que ningún mensaje ni traza lleva secreto, ruta ni texto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06023 | Tarea técnica | Confirmar los valores rotulados como asunción y las tres coberturas | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06025 | Tarea técnica | Elevar la forma de sostener que la provisoria no se repite | Media | Sin fijar | Equipo (1) | Pendiente |
| `d` | BT-06026 | Tarea técnica | Elevar la frecuencia del respaldo y la fecha de última modificación | Baja | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-06013 | Historia | Arrastrar todos los trabajos de una cuenta dada de baja | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-06016 | Historia | Conservar y transportar la marca sin alterar el estado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-06019 | Historia | Producir una provisoria no adivinable y sin repetirse | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-06020 | Historia | Terminar sin producir valor cuando la aleatoriedad no responde | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-06010 | Tarea técnica | Adaptador de repositorio de trabajos con la proyección separada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-06011 | Tarea técnica | Retiro físico con todo o nada y arrastre de la baja | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06008 | Historia | Conservar el texto original literal y rechazar toda escritura que lo reemplace | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06009 | Historia | Materializar el trabajo con sus piezas, componentes y observaciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06010 | Historia | Resolver la consulta con el recorte ya trasladado al pedido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06011 | Historia | Excluir componentes y texto original del resultado de un listado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-06012 | Historia | Retirar físicamente un trabajo con todo lo que cuelga de él | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06016 | Tarea técnica | Motor de interpretación con las cuatro trampas del formato | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06019 | Tarea técnica | Fijar la tabla de derivación por tipo | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06017 | Tarea técnica | Motor de verificación con tolerancia 0.01 y operador estricto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06020 | Tarea técnica | Puerta de cero peticiones de red de los dos motores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06024 | Tarea técnica | Elevar hasta dónde llega el conjunto de tipos reconstruibles | Media | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-06018 | Tarea técnica | Batería de diez casos con los ocho escenarios como entrada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06001 | Historia | Leer el texto real con tolerancia a comas finales y claves sinónimas | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06002 | Historia | Devolver la cantidad de figuras del conjunto raíz | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06003 | Historia | Reconstruir las piezas con su posición y sus componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06004 | Historia | Emitir el error de validación con posición de figura y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06005 | Historia | Derivar el valor desde las dimensiones y los componentes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06006 | Historia | Comparar con tolerancia absoluta y operador estricto | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-06007 | Historia | Emitir la advertencia con el valor declarado y el derivado | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 25 historias y 26 tareas técnicas**, repartidas en cinco etapas. La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog.

**US-06023 figura con prioridad de ejecución `Media`**, y su MoSCoW en 06 es `Should`: es la única historia de este backlog donde las dos coinciden en señalar lo mismo.

## 4. Alcance técnico y orden de construcción

### 4.1 `GeometriaFactory-Api`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md), ni redefine la superficie: los quince puntos están en la categoría 02 y su contrato en [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md).

**Orden**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-00001 y BT-00005 primero; BT-00002 sobre ellos; **BT-00007, BT-00008, BT-00009 y BT-00010 en el mismo tramo**, porque las cuatro son decisiones que se validan en el punto de control y **dos de ellas obligan o afectan a otro proyecto de código**; BT-00003 sobre BT-00002; las cuatro historias; **BT-00006 y BT-00004 al cerrar**, porque son puertas y BT-00004 es `PT-04`.
2. `c`: BT-00011 y BT-00013 primero —la guardia y el traductor son transversales—; BT-00016 sobre ellos; BT-00022 se abre acá y **acompaña todas las etapas siguientes**; las nueve historias; **BT-00014, BT-00012 y BT-00015 al cerrar**, porque son inspecciones y elevaciones sobre algo ya construido.
3. `d`: BT-00017 sobre BT-00011 y BT-00013; las nueve historias; **BT-00012 se vuelve a correr** por los puntos que la etapa agrega; BT-00025 antes del punto de control.
4. `e`: BT-00018 sobre BT-00008, BT-00011 y BT-00013; las cuatro historias; **BT-00023 y BT-00024 al cerrar**, porque son las dos pruebas de criterio propio del producto.
5. `f`: las dos historias sobre BT-00018; **BT-00012 se vuelve a correr**.
6. `h`: BT-00019 sobre BT-00011 y BT-00013; US-00023 después; **BT-00020 al final**, porque la colección recorre la superficie entera e incluye la aprobación y el rechazo; BT-00021 y BT-00026 antes del punto de control.

**Reglas de dependencia interna que ninguna tarea puede cruzar** (`05` §3.2): **ninguna superficie depende de otra superficie** —un punto que invocara a otro sería una petición encadenada, y **una petición ejerce a lo sumo un caso de uso**—; **el traductor está después de las cinco superficies**, incluidas las que no exigen acceso, de modo que **ningún camino de fallo sale sin pasar por la tabla única**; y **la composición de raíz no atiende peticiones**: construye el grafo y desaparece.

**Consecuencia del nivel topológico 3**: dentro de cada etapa, el trabajo de este proyecto de código va **último**. Y hay una consecuencia que no es de compilación: **`GeometriaFactory-Web` lo alcanza por HTTP en tiempo de ejecución**, de modo que una etapa no se demuestra sin que los dos extremos estén en pie.

### 4.2 `GeometriaFactory-Domain`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-02001 primero; BT-02002 y BT-02003 en paralelo sobre él; BT-02004 y BT-02005 al cerrar, porque son puertas y no se pueden medir sobre un proyecto que todavía no compila.
2. `c`: BT-02006 y BT-02007 antes que BT-02010 y BT-02011; las cuatro historias de la etapa después de las cuatro tareas.
3. `d`: BT-02009 sobre BT-02006; las ocho historias después; BT-02014 se abre con la primera historia y se cierra con la última; BT-02015 y BT-02016 antes del punto de control.
4. `e`: BT-02012 sobre BT-02006 y BT-02007; las cinco historias después.
5. `f`: BT-02013 sobre BT-02012; BT-02008 al cerrar, porque necesita el catálogo ejercido.
6. `h`: las tres historias sobre BT-02012 ya construida; BT-02014 se revisa por última vez.

**Consecuencia del nivel topológico 0, y es lo que más condiciona el orden del producto**: dentro de cada etapa, el trabajo de este proyecto de código va **antes** que el de `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api`, que compilan contra él. Una guarda que acá no exista es una guarda que allá no se puede invocar. Lo que **no** habilita el nivel 0 es adelantar etapas: siguen siendo secuenciales.

### 4.3 `GeometriaFactory-Application`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-04001 primero; BT-04002 y BT-04003 sobre él; BT-04004, BT-04005 y BT-04006 al cerrar, porque son puertas y no se pueden medir sobre un proyecto que todavía no compila. **BT-04002 termina en el punto de control**, y ahí queda fijado el nombre del cuarto puerto.
2. `c`: BT-04007 antes que todo lo demás —los orquestadores consumen puertos—; después BT-04010, después BT-04012 y BT-04014; las cuatro historias de la etapa sobre esas tareas.
3. `d`: BT-04013 sobre BT-04010 y BT-04007; las diez historias después; **BT-04011 se abre con US-04030 y se cierra con la última historia de la etapa**, porque la cuarta comprobación no tiene sobre qué decidir hasta que exista la marca; BT-04018, BT-04020 y BT-04021 antes del punto de control.
4. `e`: BT-04009 sobre BT-04007; después BT-04015 y BT-04016; las nueve historias después.
5. `f`: las cuatro historias sobre BT-04015; **BT-04008 al cerrar**, porque el catálogo de condiciones no se puede recorrer en las dos direcciones hasta que el conjunto esté entero producido; BT-04019 al final, porque mide sobre algo terminado.
6. `h`: BT-04017 sobre BT-04010 y BT-04007; las cinco historias después.

**Regla de dependencias interna que ninguna tarea puede cruzar** (`05` §3.2): **ningún orquestador depende de otro orquestador**, la guarda **no lee conjuntos y no escribe**, y la flecha hacia `GeometriaFactory-Infrastructure` es de implementación y va al revés que la de dependencia —este proyecto de código **no la nombra ni la referencia**—.

**Consecuencia del nivel topológico 1, y es lo que más condiciona el orden dentro de cada etapa**: el trabajo de `GeometriaFactory-Domain` va **antes** que el de este proyecto de código, y el de este proyecto de código va **antes** que el de `GeometriaFactory-Infrastructure` y el de `GeometriaFactory-Api`. Lo que **no** cambia es el orden de las etapas: siguen siendo secuenciales.

### 4.4 `GeometriaFactory-Infrastructure`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden dentro de cada etapa**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-06001 primero; **BT-06003 temprano**, porque el anclaje de la derivación de clave condiciona los dos mecanismos y es una decisión que el intake asigna a este proyecto de código sin elegir por él; BT-06002 en paralelo; después BT-06005, BT-06008 y BT-06006; las dos historias sobre ellos; **BT-06004 y BT-06007 al cerrar**, porque son puertas y no se miden sobre algo que todavía no compila ni arranca.
2. `c`: BT-06009 sobre BT-06005; BT-06012, BT-06013 y BT-06015 en paralelo, porque **no dependen del contexto de persistencia**; las siete historias después.
3. `d`: BT-06014 sobre BT-06013; las cuatro historias; BT-06021 y BT-06022 al cerrar, porque el catálogo y la inspección de secretos necesitan el conjunto ya producido; BT-06023, BT-06025 y BT-06026 antes del punto de control.
4. `e`: BT-06010 y BT-06011 sobre BT-06005 y BT-06009; las cinco historias después.
5. `f`: BT-06016 primero; BT-06019 y BT-06017 sobre él; las siete historias; **BT-06018 y BT-06020 al cerrar**, porque son la batería y la inspección, y sólo tienen sentido sobre algo terminado.

**Reglas de dependencia interna que ninguna tarea puede cruzar** (`05` §3.2): **ningún adaptador depende de otro adaptador** —el único par acoplado son los dos motores, en una sola dirección: la verificación exige las piezas ya reconstruidas—; **los dos motores, el reloj y el mecanismo de credenciales no dependen del contexto de persistencia**; y **la composición de raíz no es de acá**: este proyecto de código declara sus adaptadores y `GeometriaFactory-Api` los conecta.

**Consecuencia del nivel topológico 2**: dentro de cada etapa, el trabajo de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` va **antes** que el de este proyecto de código —un puerto que allá no exista es un adaptador que acá no se puede escribir— y el de `GeometriaFactory-Api` va **después**. Lo que **no** cambia es el orden de las etapas.

**Y una consecuencia que abarata todo el tramo `f`**: los dos motores **no tocan el almacén y no hacen red**, de modo que la épica del validador entera se puede construir y correr sin base y sin ninguna otra pieza del producto en pie.

## 5. Definition of Done aplicada

### 5.1 `GeometriaFactory-Api`

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las dos inspecciones se corren en cada etapa que agregue un punto o un código**, no sólo en la que las introdujo: los quince puntos contra la guardia y los diecisiete códigos contra la tabla de traducción, **las dos en las dos direcciones**.
3. **Las dos pruebas de criterio propio del producto se ejecutan y pasan**: la **eliminación forzada contra la superficie** —el único criterio que la fuente exige ejercer así— y la del **texto byte a byte con rechazo sin truncamiento**.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: los cuerpos son los escenarios `E-1` a `E-8` del intake §20.
5. **Los cinco valores rotulados [ASUNCIÓN] se usan como vigentes y la puerta de cobertura no se declara bloqueante en 09** hasta que BT-00025 cierre.
6. **La imagen se construye con el archivo multietapa, arranca, aplica las transformaciones sobre almacén vacío y responde salud** antes de considerar cerrada la etapa `a`. Es `PT-04`.

### 5.2 `GeometriaFactory-Domain`

**La DoD canónica del proyecto de código vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5, que son de nivel producto.

Criterios específicos que este plan agrega, y que no reemplazan a los anteriores:

1. **La actualización de la categoría 11 forma parte del cierre.** Ninguna etapa se declara cerrada con documentos del cuerpo documental de entrega afectados por sus ítems y sin revisar. **La categoría 11 de este proyecto de código todavía no está emitida**, de modo que hasta su emisión la condición se cumple de forma vacía y se registra así en el informe de cierre, en lugar de darse por cumplida en silencio.
2. **Las puertas de construcción se miden en cada etapa y no sólo en la que las introdujo**: cero dependencias salientes (BT-02004) y cero advertencias (BT-02005).
3. **La matriz de ejercicio de los nueve invariantes se revisa al cerrar cada etapa** que introduzca o toque una guarda.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.

### 5.3 `GeometriaFactory-Application`

**La DoD canónica del proyecto de código vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega, y que no reemplazan a los anteriores:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código **todavía no está emitida**, de modo que hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**, en lugar de darse por cumplida en silencio.
2. **Las tres puertas de construcción se miden en cada etapa y no sólo en la que las introdujo**: una sola dependencia saliente (BT-04004), cero advertencias (BT-04005) y **cero pruebas que toquen la base de datos real** (BT-04006).
3. **La matriz de ejercicio de las cuatro comprobaciones se revisa al cerrar cada etapa** que introduzca o toque un camino de lectura o de escritura.
4. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.
5. **Los dos valores rotulados [ASUNCIÓN] se usan como vigentes y la puerta de cobertura no se declara bloqueante en 09** hasta que BT-04018 cierre. Ninguna etapa se cierra declarando cumplido un número que el Product Owner no confirmó.

### 5.4 `GeometriaFactory-Infrastructure`

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las dos puertas propias del pipeline se miden en cada etapa**: construcción sin advertencias y **transformaciones aplicadas solas sobre un almacén inexistente**, que es la cuarta etapa y es propia de acá.
3. **Ningún guion de prueba que involucre el texto de figuras usa datos inventados**: el material son los escenarios `E-1` a `E-8` del intake §20, por la regla de delivery 5 de su §15.
4. **Los tres valores rotulados [ASUNCIÓN] se usan como vigentes y las puertas de cobertura no se declaran bloqueantes en 09** hasta que BT-06023 cierre.
5. **La etapa `f` no se cierra sin la batería de diez casos pasando entera.** Es la mitigación declarada del único riesgo de negocio del producto, y su cobertura es la más alta del producto.
6. **Ningún mensaje ni ninguna traza lleva un secreto, la ruta del almacén o el texto del alumno**, verificado **en las dos direcciones** al cerrar cada etapa que agregue condiciones.

## 6. Riesgos y mitigaciones

### 6.1 `GeometriaFactory-Api`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que un punto de acceso nuevo quede fuera de la guardia del cambio de contraseña pendiente | **Alta**: es un defecto de omisión, y **los defectos de omisión no se ven leyendo el punto nuevo** | **Muy alto**: `RN-00013` e `INV-09` dejan de valer y **nada falla**; una cuenta con la marca puesta ejercería una capacidad y ninguna capa de adentro se enteraría | BT-00012, con el NFR de **exactamente 4** puntos fuera de la guardia y la inspección que recorre los quince **en las dos direcciones**, corrida en cada etapa que agregue un punto |
| Que el trabajo ajeno responda «no autorizado» en lugar de «no encontrado» | Media: es la traducción que **parece más informativa** y por eso es la tentadora | **Muy alto**: confirma la existencia de un recurso ajeno y **ninguna capa de adentro puede repararlo** | BT-00013 con su fila única en la tabla de traducción, y BT-00014, que compara las **dos** respuestas y verifica que son indistinguibles en cuerpo y en código |
| Que el límite de tamaño del cuerpo trunque el texto de un alumno en lugar de rechazarlo | Media: **truncar es el comportamiento por defecto de varias capas de transporte** | Alto: **rompe `RN-00008` en silencio**; el trabajo se guarda, el texto queda mutilado y el alumno lo descubre al ver el dibujo | BT-00009, con la forma de rechazo **no configurable**, y BT-00024, con **0** truncamientos y la comparación byte a byte |
| Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Media, **y es el trade-off que el ensamblado de contratos aceptó por escrito** al no imponer formato | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red que este producto tiene | BT-00008, con **una sola** configuración declarada para los dos extremos, y BT-00022, que la verifica **golpeando el servicio real** |
| Que un envío cuyo texto no verifica responda con un código de fallo | Media: **es la lectura intuitiva de «no verificó»** | Medio: le diría a la persona que su petición estaba mal cuando **el trabajo ya quedó guardado** | US-00017 y la declaración de la superficie: **es una respuesta exitosa**, con el estado y las observaciones en el cuerpo |
| Que se agregue un punto pensado para el navegador o se configure el intercambio de origen cruzado | Baja, **pero el costo de equivocarse es de rediseño** | **Muy alto**: reabre las tres propiedades de la topología y rompe `RA-01` | Las **tres ausencias declaradas** de la superficie de 02, que dejan escrito lo que las repone, y el hecho de que **el único cliente legítimo esté declarado en el manifiesto y en el grafo** |
| Que la composición de raíz deje un puerto sin adaptador y el fallo aparezca en la primera petición | Media | Medio: el servicio arranca y falla al primer uso, **en producción y sin nadie mirando** | BT-00002, con composición **única**, resolución verificada en el arranque, NFR de **4 de 4** y **fallo en construcción** |
| Que el listado de la comisión crezca por encima de lo que el requerimiento de tiempo sostiene | Baja en el alcance declarado —una comisión durante una clase— | Medio: la pantalla más pesada del producto deja de cumplir su percentil | La decisión de no paginar está tomada **con condición de reingreso escrita**: cuando la medición deje de cumplirse, entra paginación, y **es un cambio del ensamblado de contratos** |
| Que el mecanismo de construcción de la imagen en destino no funcione y el despliegue quede sin camino | Media, **y la fuente lo rotula [A VERIFICAR]** por su cuenta | Alto: **es el único canal de entrega declarado** | BT-00026, que lo prueba **una vez antes de depender de él**, tal como el intake exige; la salida documentada y **no adoptada** es el túnel saliente |

### 6.2 `GeometriaFactory-Domain`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que una dependencia se cuele en el nivel 0 —una anotación de mapeo, un atributo de serialización— y el dominio deje de ser probable sin infraestructura | Media | Alto | BT-02004, puerta bloqueante de cero dependencias salientes, medida en **cada** etapa y no sólo en la `a` (`05` §9, primer riesgo) |
| Que un invariante se ejerza en un componente y no en otro, y quede una puerta por la que se lo saltea | Media, **y con precedente registrado** en el audit de la categoría 02 de este proyecto de código | Alto | BT-02011, puerta única de admisibilidad, y BT-02014, matriz de ejercicio de los nueve invariantes (`05` §9, segundo riesgo) |
| Que los nombres de tipos y de espacios de nombres se fijen sin punto de control y después haya que renombrarlos | Media | Bajo: costo de retrabajo, no de corrección | BT-02002, con caja temporal en la etapa `a` y validación en su punto de control (`05` §9, quinto riesgo) |
| Que las etapas `c` a `h` avancen con los dos valores rotulados como asunción sin confirmar, y que la puerta de cobertura se declare bloqueante sobre un número que nadie aprobó | Media | Medio | BT-02015, con la condición explícita de que la puerta **no se declara bloqueante en 09** hasta que el Product Owner confirme (`05` §11 PA-02, `PRODUCT-INTAKE` §22) |
| Que el punto de control de una etapa se demore y el trabajo de los proyectos de código de nivel 1 a 3 se adelante sobre una superficie todavía no aprobada | Media | Alto: rompe la regla de etapas en serie del intake §10 | La regla de delivery 4 del intake §15 —una rama y una solicitud de incorporación por etapa, y no se abre la siguiente antes de fusionar— se aplica también a este proyecto de código, aunque su trabajo esté terminado antes |

### 6.3 `GeometriaFactory-Application`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca un camino que ejerza una capacidad **sin** resolver antes la marca de cambio de contraseña pendiente | Media, **y es una dependencia de disciplina heredada**: [`Domain ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 declaró que el dominio no puede impedirla | **Muy alto**: `INV-09` deja de valer y una clave que el administrador conoce queda sirviendo para operar como el alumno | BT-04010 con el orden fijo en un único componente, BT-04011 con la prueba específica de que la cuarta corta primero, y el criterio 5 de la DoR, que no admite excepción (`05` §9, segundo riesgo) |
| Que un caso de uso consulte la base por su cuenta y deje de ser probable con dobles | Media: es la presión natural cuando una pantalla pide un dato que la proyección no trae | Alto: se pierde la propiedad que justifica el estilo entero y la autorización por pertenencia deja de poder verificarse sin base | BT-04004 y **BT-04006**, la puerta propia de cero pruebas que tocan la base real, medidas en cada etapa (`05` §9, primer riesgo) |
| Que la negativa por pertenencia y la negativa por facultad se confundan, y un trabajo ajeno responda «no autorizado» | Media: es un error de lectura fácil, y la categoría 03 lo llama «el error más caro que un consumidor puede cometer contra esta capa» | Alto: permite averiguar por tanteo qué identificadores existen, que es lo que `RN-04003` viene a cerrar | BT-04011, con la prueba que pide un trabajo ajeno y compara el motivo emitido; y la tabla de traducciones prohibidas de 03 (`05` §9, tercer riesgo) |
| Que el nombre del cuarto puerto se fije sin punto de control y haya que renombrarlo en los cuatro componentes que lo consumen | **Alta**: hoy no tiene nombre declarado en ninguna fuente (`05` §9, sexto riesgo) | Bajo: costo de retrabajo, no de corrección | BT-04002, con caja temporal en la etapa `a` y validación en su punto de control, y el nombramiento en lenguaje de dominio mientras tanto |
| Que un caso de uso reparta su efecto entre dos unidades de trabajo y la baja deje trabajos huérfanos | Baja | Alto: `RN-04007` deja de valer y el arrastre se vuelve parcial | BT-04009, con la baja como caso testigo y el NFR de **0** casos de uso que repartan su efecto (`05` §9, cuarto riesgo) |
| Que la etapa `f` avance con los valores rotulados como asunción sin confirmar y la puerta de cobertura se declare bloqueante sobre un número que nadie aprobó | Media | Medio | BT-04018, con la condición explícita de que la puerta **no se declara bloqueante en 09** hasta que el Product Owner confirme (`05` §11 `PA-05`; `PRODUCT-INTAKE` §22) |
| Que el punto de control de una etapa se demore y el trabajo de los niveles 2 y 3 se adelante sobre una superficie todavía no aprobada | Media | Alto: rompe la regla de etapas en serie del intake §10 | La regla de delivery 4 del intake §15 —una rama y una solicitud de incorporación por etapa, y no se abre la siguiente antes de fusionar—, que se aplica también a este proyecto de código aunque su trabajo esté terminado antes |

### 6.4 `GeometriaFactory-Infrastructure`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que el validador se escriba sin leer el análisis y no sirva para el dato que existe | **Alta si no se controla**, así lo declara la fuente | **Muy alto**: es el **único riesgo de negocio del producto cuya mitigación declarada es una batería de pruebas**, y su materialización deja el producto inútil para el dato real | Las **cuatro** trampas escritas **antes de leer texto** (BT-06016), la batería de **10** casos con los ocho escenarios (BT-06018), la cobertura más alta del producto y la tabla de derivación por tipo (BT-06019) |
| Que la provisoria se componga por un contador, la fecha o el correo cuando la fuente de aleatoriedad no responde | Media | **Muy alto**: produce una provisoria adivinable **y el reseteo parece haber funcionado**. Un reseteo que no se completa es recuperable; una provisoria adivinable **no se nota hasta que alguien la usa** | BT-06014, con el atajo **escrito como prohibido**, la condición propia y el NFR de **0** provisorias repetidas; y US-06020, cuyo entregable es la terminación |
| Que ante la ausencia de clave de firma se genere una al vuelo o se emita sin firmar | Media | **Muy alto**: el sistema arranca, emite accesos y **nadie lo nota hasta que alguien falsifica uno** | BT-06015, con la clave que **se recibe y no se busca**, y US-06022, con **0** accesos emitidos sin clave |
| Que la preparación del almacén descarte el almacén y lo cree de nuevo ante un esquema que no corresponde | Baja, **pero es el atajo más destructivo del producto** | **Muy alto**: deja el servicio impecable y **sin los trabajos de nadie** | BT-06006 y US-06025, con arranque detenido y la regla de que **una transformación ya fusionada no se edita** |
| Que la ubicación del almacén caiga hacia una ruta dentro de la imagen cuando el volumen no está montado | Media, **porque es el comportamiento por defecto de casi cualquier biblioteca de acceso a archivos** | Alto: el servicio arranca, acepta trabajos de la comisión entera y **los pierde en el siguiente reemplazo de versión** | BT-06006 y la regla de que **la configuración se recibe y no se busca** |
| Que un texto ilegible devuelva la condición de servicio no disponible en lugar de una observación | **Alta**: la categoría 03 declara que ésa es la garantía que más veces se rompe al implementar | Alto: el alumno esperaría a que se recupere **de un problema que no tiene** | US-06004 con su tercer criterio, BT-06021 con la separación entre **resultado** y **fallo** ejercida, y la segunda regla de refinamiento del backlog |
| Que una consulta de listado arrastre los componentes de cada pieza o el texto original | **Media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad | Medio: rompe el requerimiento de tiempo del listado del administrador | BT-06010 y US-06011, con **0** componentes cargados verificados sobre la proyección devuelta |
| Que la unicidad del correo se sostenga sólo con la consulta previa del consumidor | Media, **porque la consulta previa no es una garantía por sí sola** | Alto: dos cuentas con el mismo correo hacen que el ingreso deje de ser determinista e `INV-01` deja de valer | BT-06009 y US-06014, con el índice único como **segunda línea** y su condición declarada como camino |

## 7. Criterios de hecho de cada tramo

### 7.1 `GeometriaFactory-Api`

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] **Las dos inspecciones en las dos direcciones pasan**: los quince puntos contra la guardia y los diecisiete códigos contra la tabla de traducción.
- [ ] La batería de integración corre entera contra el servicio real y el almacén real.
- [ ] Para la etapa `a`: **`PT-04` está medida** y está verificado que **la sesión interactiva del front no llega hasta acá**.
- [ ] Para la etapa `e`: la **eliminación forzada** y la prueba del **texto byte a byte** pasan.
- [ ] Para la etapa `h`: la **colección de peticiones se reproduce en cinco pasos o menos, sin datos inventados**.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

### 7.2 `GeometriaFactory-Domain`

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión: los guiones de todas las etapas anteriores vuelven a pasar **sin correcciones**.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] La etapa incorporó pruebas automatizadas de las reglas de negocio que introdujo.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

### 7.3 `GeometriaFactory-Application`

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión: los guiones de todas las etapas anteriores vuelven a pasar **sin correcciones**.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] La etapa incorporó pruebas automatizadas de las reglas de negocio que introdujo, **todas sin base de datos**.
- [ ] Las tres puertas del pipeline pasan: una sola dependencia saliente, cero advertencias y cero pruebas que toquen la base real.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

### 7.4 `GeometriaFactory-Infrastructure`

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 que alcanzan a este proyecto de código se cumplen.
- [ ] Las **dos** puertas propias del pipeline pasan: cero advertencias y transformaciones aplicadas solas sobre un almacén inexistente.
- [ ] Ninguna condición nueva quedó fuera del catálogo, y ningún mensaje ni traza lleva secreto, ruta del almacén ni texto del alumno.
- [ ] Para la etapa `a`: la parte de `PT-04` que alcanza a este proyecto de código está medida.
- [ ] Para la etapa `f`: **la batería de diez casos pasa entera**, con los ocho escenarios del intake como entrada y **sin datos inventados**.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

### 8.1 `GeometriaFactory-Api`

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | NB-00003, **NB-00008**, que recibe acá su primer tramo propio y no parcial | CU-00010, CU-00011 | ADR-00001, ADR-00002, ADR-00006, ADR-00007, ADR-00008 |
| `c` | NB-00001, NB-00002, NB-00004, NB-00008 | CU-00001, CU-00002, CU-00003, CU-00009 | ADR-00001, ADR-00003, ADR-00004 |
| `d` | NB-00001, NB-00002 | CU-00002, CU-00003, CU-00004, CU-00005 | ADR-00003, ADR-00004 |
| `e` | NB-00003, NB-00005 (parcial), NB-00006 (parcial), NB-00007 (parcial), NB-00009 (parcial) | CU-00006, CU-00007 | ADR-00002, ADR-00004, ADR-00005 |
| `f` | NB-00003, NB-00004 | CU-00006 | ADR-00002, ADR-00004 |
| `h` | NB-00009 | CU-00008, CU-00012 | ADR-00004, ADR-00008 |

**Las nueve necesidades de negocio avanzan en alguna etapa de este proyecto de código**, y **`NB-00008` recibe acá su primer tramo propio y no parcial**: `GeometriaFactory-Application` declara que no la toca y `GeometriaFactory-Infrastructure` la declara parcial. Su dolor es de acceso y de despliegue, y **es acá donde el producto se vuelve alcanzable**.

**Puertas técnicas del producto y este proyecto de código.** **`PT-04` es de esta pieza** y se mide en la etapa `a`. `PT-01` es del front —y su parte `PT-01.d` consulta el punto de salud que esta pieza expone—, `PT-02` y `PT-03` son del bundle del visor y de su anfitrión, y `PT-05` es del despliegue real de la fase `i`. Lo que alcanza a este proyecto de código de las otras es la consecuencia: **una puerta que no pasa detiene la planificación de las etapas que dependen de ella**.

### 8.2 `GeometriaFactory-Domain`

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-02001, ADR-02003 |
| `c` | NB-00001, NB-00002 | CU-02003, CU-02004, CU-02012 | ADR-02001, ADR-02002, ADR-02004, ADR-02005 |
| `d` | NB-00001, NB-00002 | CU-02001, CU-02002, CU-02003, CU-02004, CU-02013 | ADR-02001, ADR-02004, ADR-02005, ADR-02006 |
| `e` | NB-00003, NB-00007 | CU-02005, CU-02009, CU-02011 | ADR-02002, ADR-02006 |
| `f` | NB-00004, NB-00005, NB-00003 | CU-02006, CU-02007, CU-02008 | ADR-02001, ADR-02002 |
| `h` | NB-00009 | CU-02010, CU-02011 | ADR-02002 |

**Las seis etapas declaran al menos una necesidad de negocio en avance, salvo la `a`**, y esa excepción es del propio roadmap: `a` es un hito interno sin capacidad funcional asociada (§2.1). Lo que la `a` sí produce y es verificable son las mediciones de las puertas técnicas del producto.

**Puertas técnicas del producto y este proyecto de código.** `PT-01` y `PT-04` se miden en la etapa `a` y `PT-02` y `PT-03` antes de comprometer la `g`; **ninguna de las cuatro se mide sobre este proyecto de código** —son del front, del servicio de datos y del bundle del visor—, y por eso no figuran como ítem de §3. Lo que sí lo alcanza es la consecuencia: una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**, incluidas las de este proyecto de código.

### 8.3 `GeometriaFactory-Application`

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-04001, ADR-04003 |
| `c` | NB-00001, NB-00002 | CU-04003, CU-04010 | ADR-04001, ADR-04002, ADR-04004, ADR-04006 |
| `d` | NB-00001, NB-00002 | CU-04001, CU-04002, CU-04003, CU-04011 | ADR-04002, ADR-04004, ADR-04005 |
| `e` | NB-00003, NB-00006 (parcial), NB-00007 | CU-04004, CU-04006, CU-04007, CU-04009 | ADR-04001, ADR-04004, ADR-04005 |
| `f` | NB-00004, NB-00005, NB-00003 | CU-04005 | ADR-04001, ADR-04002, ADR-04006 |
| `h` | NB-00009 | CU-04006, CU-04008, CU-04009 | ADR-04004, ADR-04005, ADR-04006 |

**Las seis etapas declaran al menos una necesidad de negocio en avance, salvo la `a`**, y esa excepción es del propio roadmap: `a` es un hito interno sin capacidad funcional asociada (§2.1).

**`NB-00008` no aparece en ninguna fila, y es declaración y no olvido.** `02` §7.2 declara que este proyecto de código **no la toca**: su dolor es de acceso y de despliegue, y esta capa no atiende peticiones, no abre conexiones y no conoce la frontera de proceso. Se cubre en 02 de `GeometriaFactory-Web` y de `GeometriaFactory-Api` y en `09-Devops`.

**Puertas técnicas del producto y este proyecto de código.** `PT-01` y `PT-04` se miden en la etapa `a` y `PT-02` y `PT-03` antes de comprometer la `g`; **ninguna de las cuatro se mide sobre este proyecto de código** —son del front, del servicio de datos y del bundle del visor—, y por eso no figuran como ítem de §3. Lo que sí lo alcanza es la consecuencia: una puerta que no pasa **detiene la planificación de las etapas que dependen de ella**.

### 8.4 `GeometriaFactory-Infrastructure`

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | NB-00003, NB-00008 (parcial) | CU-06010 | ADR-06001, ADR-06002, ADR-06004, ADR-06007 |
| `c` | NB-00001, NB-00002 | CU-06005, CU-06006, CU-06008, CU-06009 | ADR-06001, ADR-06002, ADR-06003, ADR-06004 |
| `d` | NB-00001, NB-00002 | CU-06004, CU-06005, CU-06007 | ADR-06002, ADR-06005 |
| `e` | NB-00003, NB-00007 (parcial), NB-00009 | CU-06003, CU-06004 | ADR-06001, ADR-06002 |
| `f` | NB-00004, NB-00005, NB-00006 (parcial) | CU-06001, CU-06002 | ADR-06006 |

**Las cinco etapas declaran al menos una necesidad de negocio en avance, incluida la `a`**, y en eso este proyecto de código se distingue de los demás: su etapa `a` no es un hito interno vacío, porque la preparación del almacén ya aporta a `NB-00003` y a `NB-00008` en su parte de que el producto quede en un estado que la pieza pública pueda declarar.

**Puertas técnicas del producto y este proyecto de código.** **`PT-04` lo alcanza** en su parte de que las actualizaciones de esquema se apliquen sobre base vacía, y se mide en la etapa `a`. `PT-01` es del front, `PT-02` y `PT-03` del bundle del visor y del anfitrión, y `PT-05` del despliegue real de la fase `i`. Lo que alcanza a este proyecto de código de las otras cuatro es la consecuencia: **una puerta que no pasa detiene la planificación de las etapas que dependen de ella**.

## 9. Bitácora de avance

### 9.1 `GeometriaFactory-Api`

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es el **resultado de `PT-04`** y **las rutas y los verbos que el punto de control validó**; para la `h`, el **resultado de la colección de peticiones**.

### 9.2 `GeometriaFactory-Domain`

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en la fase de especificación y la etapa `a` todavía no arrancó.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre que el intake §15 exige, y no semana a semana: la cadencia de este producto es la etapa y no el calendario.

### 9.3 `GeometriaFactory-Application`

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en la fase de especificación y la etapa `a` todavía no arrancó.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre que el intake §15 exige, y no semana a semana: la cadencia de este producto es la etapa y no el calendario. Para la etapa `a`, lo que se registra es el **resultado del punto de control sobre los nombres**, incluido el del cuarto puerto.

### 9.4 `GeometriaFactory-Infrastructure`

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es **qué función de derivación de clave se ancló y con qué parámetros**; para la `f`, **el resultado de los diez casos de la batería**.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto sin reescritura. Sube **major**. |
