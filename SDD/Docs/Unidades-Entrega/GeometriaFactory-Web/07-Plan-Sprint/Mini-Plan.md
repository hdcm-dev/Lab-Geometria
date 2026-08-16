# Mini-plan — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Mini-Plan.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. Información general

### 1.1 `GeometriaFactory-Web`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Las ocho.** Es el único de los siete del que se puede decir |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **1**, con dos dependencias de compilación: `GeometriaFactory-Contracts` y el bundle de `GeometriaFactory-Visor` (`Vista-Producto.md` §3) |
| Unidad de despliegue | **Una propia**: la publicación en el hosting público. Es una de las **dos** unidades desplegables del producto |
| Puertas técnicas propias | **`PT-01`** en sus cuatro partes, medida en la etapa `a` **antes que cualquier otra cosa**; y la parte de **`PT-02`** que se mide sobre una página de esta pieza |
| Paralelismo entre etapas | **Ninguno.** Las etapas son estrictamente secuenciales (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`.

**Y hay un segundo motivo**: este producto **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: sin plazo calendario, sin iteraciones cerradas y con una sola persona.

Y hay un motivo propio de este proyecto de código, que es el más fuerte de todos los planes emitidos hasta acá: **la categoría 05 se negó explícitamente a inventar el único número que le faltaba**, el umbral de tiempo de respuesta, con el fundamento de que un valor puesto ahí se propagaría a 08 como si fuera del producto (`05` §8, cierre). Declarar acá una capacidad en puntos sería hacer exactamente lo que ese documento evitó, un escalón más abajo.

Lo que **sí** limita la capacidad y está declarado es el **cuello de diseño**: el punto de control de cada etapa (`PRODUCT-INTAKE` §10). Y hay un segundo límite, propio y medido: **`PT-01.c`, la estabilidad del proceso del hosting, que la fuente declara sin mitigación en el código**.

### 1.3 Las ocho etapas y el momento de las dos puertas del visor

Este proyecto de código toca **las ocho** etapas comprometidas, y además participa de un **momento** que el roadmap declara y que no es una etapa: `PT-02` y `PT-03` se miden **antes de comprometer la etapa `g`** (`Roadmap-Producto.md` §2.2).

**Este plan no crea una etapa nueva ni renombra ninguna.** Lo que sí declara es que la parte de `PT-02` que se mide **sobre una página del anfitrión** —que es una página de esta pieza— cae en ese momento, y que **una puerta que no pasa detiene la planificación de la etapa `g`** en lugar de arrastrarse como deuda. Por eso BT-10018 tiene su caja temporal ahí y no dentro de la etapa `g`.

### 1.2 `GeometriaFactory-Visor`

| Campo | Valor |
| --- | --- |
| Unidad de planificación | La **etapa** del producto, no el sprint (`Roadmap-Producto.md` §1.2) |
| Etapas comprometidas del producto | **Ocho**, `a` a `h` (`PRODUCT-INTAKE` §15) |
| Etapas que toca este proyecto de código | **Dos**: `a` y `g`, más el **momento de medición** de `PT-02` y `PT-03`, que precede a la `g` |
| Duración de cada etapa | **Sin fecha.** El avance se mide por etapas cerradas (`Roadmap-Producto.md` §1.1) |
| Tamaño del equipo | `equipo_n = 1` (`PRODUCT-INTAKE` §2) |
| Unidad de estimación | **Sin fijar**, por [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1 |
| Nivel topológico | **0**, sin dependencias salientes (`Vista-Producto.md` §3) |
| Etapas del pipeline | Instalación reproducible de dependencias → empaquetado → copia al directorio de recursos estáticos del anfitrión (`05` §5) |
| Paralelismo entre etapas | **Ninguno** entre etapas del producto (`Roadmap-Producto.md` §4) |

### 1.1 Por qué esta categoría emite un mini-plan y no planes de iteración

El intake declara **`equipo_n = 1`** en su §2, y de ese dato el framework deriva que la categoría 07 emita **únicamente** `Mini-Plan.md`; `Roadmap-Producto.md` lo declara en su §2.1, en su §3 y en su §6. **No se emiten** `Plan-Iteracion-Sprint-XX.md`, `Template-Sprint-Review.md`, `Template-Sprint-Retrospectiva.md` ni `Velocidad-Equipo.md`.

El segundo motivo es del producto: **no planifica en sprints**. Su ciclo es etapa, informe de cierre, punto de control bloqueante y fusión.

### 1.2 Capacidad disponible

**No se declara capacidad numérica, y es deliberado.** Ninguna fuente da base: el intake declara sin plazo calendario, no hay iteraciones cerradas y el equipo es de una persona.

Y hay un motivo que en este proyecto de código pesa más que en los otros dos: **la categoría 05 ya se negó a inventar el único umbral numérico que le faltaba**, el de fluidez de la interacción, con el fundamento de que un valor inventado se propagaría a 08 como si fuera del producto (`05` §8, cierre). Declarar acá una capacidad en puntos sería hacer exactamente lo que ese documento evitó, un escalón más abajo.

### 1.3 Los tres tramos de este proyecto de código

Este proyecto de código no reparte su trabajo en seis o siete etapas como los otros dos de nivel 0: lo concentra en **dos etapas y un momento**.

| Tramo | Qué es | Fuente |
| --- | --- | --- |
| Etapa `a` | Etapa del producto. El bundle es **vacío pero real** | `PRODUCT-INTAKE` §15 |
| **Antes de comprometer la etapa `g`** | **Un momento declarado del roadmap, no una etapa.** Es donde se miden `PT-02` y `PT-03`, y una puerta que no pasa **detiene la planificación de la etapa que depende de ella** | `Roadmap-Producto.md` §2.2 y §5.2 |
| Etapa `g` | Etapa del producto. La visualización y el árbol se integran para los dos papeles | `PRODUCT-INTAKE` §15 |

**Este plan no crea una etapa nueva ni renombra ninguna.** El tramo del medio es un momento que el roadmap ya declara, y reflejarlo es lo que impide leer que todo el visor se construye dentro de la etapa `g`, que sería falso y llevaría a comprometer esa etapa sin haber medido lo que la condiciona.

## 2. Objetivo de cada tramo

### 2.1 `GeometriaFactory-Web`

| Etapa | Objetivo de este proyecto de código al cerrar la etapa |
| --- | --- |
| `a` | El front publicado arranca en el hosting, su página de salud muestra datos reales del servidor propio, y las **cuatro** partes de `PT-01` están medidas y documentadas, incluido el repliegue de transporte si ocurre. |
| `b` | Todas las rutas del mapa de navegación son alcanzables, con las **once** superficies en marcador de posición sobre la línea de base visual aprobada, sin estilos improvisados fuera del sistema visual. |
| `c` | El docente configura su cuenta, entra, cambia su contraseña y sale, con la credencial custodiada del lado del servidor; y la aplicación distingue el vacío del fallo y la reconexión de la indisponibilidad. |
| `d` | El alumno se registra, el docente lo habilita y le comunica la provisoria, el alumno entra cambiándola, y una cuenta con la contraseña reseteada no llega a ninguna otra ruta hasta cambiarla. |
| `e` | El alumno carga trabajos con su texto intacto y ve su listado; el administrador recorre la entrega de la comisión agrupada y filtrada, sin borradores. |
| `f` | El alumno envía su trabajo, previsualiza antes de enviarlo, y ve sus advertencias con los dos valores y sus errores con índice de figura y campo. |
| `g` | La persona ve el trabajo en tres dimensiones y como árbol dentro del producto, con la sincronización por índice y los dos movimientos automáticos gobernados por separado, sin una sola petición originada por la visualización. |
| `h` | El administrador resuelve la entrega con comentario opcional y retira lo que ve; el alumno encuentra el desenlace en su listado y el comentario al abrir el trabajo. |

### 2.2 `GeometriaFactory-Visor`

| Tramo | Objetivo de este proyecto de código al cerrarlo |
| --- | --- |
| Etapa `a` | El proyecto del bundle existe, su construcción es reproducible desde el entorno de desarrollo y produce un archivo vacío pero real, copiado al directorio de recursos estáticos del anfitrión. |
| Antes de comprometer `g` | El bundle carga en una página del anfitrión, crea la escena, dibuja las tres figuras del escenario semilla, sincroniza por índice, libera sus recursos y funciona sin acceso a redes externas: las dos puertas están medidas. |
| Etapa `g` | La persona ve el trabajo en tres dimensiones y como árbol dentro del producto, con los dos movimientos automáticos gobernados por separado, y el punto de extensión tiene su demostración sin backend. |

**Ninguna otra etapa produce trabajo en este proyecto de código.** Las etapas `b` a `f` no dibujan nada, y en la `h` la fachada dibuja el mismo trabajo para el alumno y para el administrador **sin saber cuál de los dos lo mira**, que es lo que `RA-02` exige.

## 3. Ítems comprometidos por tramo

### 3.1 `GeometriaFactory-Web`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Etapa | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-10001 | Tarea técnica | Crear el proyecto del front con su flujo de publicación | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10002 | Tarea técnica | Anclar la versión de la biblioteca de componentes de interfaz | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10005 | Tarea técnica | Dirección del servicio de datos desde configuración, con secretos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10003 | Tarea técnica | Página de salud que consume el punto de salud del servicio de datos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10006 | Tarea técnica | Puerta de publicación que comprueba que la dirección pública responde | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10012 | Tarea técnica | Adoptar el formato de intercambio que fija la categoría 05 de la Api | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-10004 | Tarea técnica | Medir `PT-01` en sus cuatro partes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `b` | BT-10007 | Tarea técnica | Los dos shells, el mapa de rutas y los cuatro guardianes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `b` | BT-10008 | Tarea técnica | Las once superficies con marcador de posición | Alta | Sin fijar | Equipo (1) | Pendiente |
| `b` | BT-10009 | Tarea técnica | Las tres representaciones reutilizadas | Media | Sin fijar | Equipo (1) | Pendiente |
| `b` | BT-10019 | Tarea técnica | Ejecutar las 61 filas de la matriz de sensado de deriva | Alta | Sin fijar | Equipo (1) | Pendiente |
| `b` | BT-10020 | Tarea técnica | Guion de demostración acumulativo como puerta del punto de control | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10011 | Tarea técnica | Cliente tipado como única salida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10014 | Tarea técnica | Custodiar la credencial de sesión en el estado del circuito | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10013 | Tarea técnica | Traductor de las diecisiete condiciones vivas a mensaje de superficie | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10015 | Tarea técnica | Puerta de cero peticiones del navegador y una sola salida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | BT-10021 | Tarea técnica | Elevar el umbral numérico de tiempo de respuesta | Media | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10008 | Historia | Configurar la cuenta de administrador una sola vez | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10003 | Historia | Iniciar sesión sin que la credencial llegue al navegador | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10004 | Historia | Informar el motivo cuando la cuenta no admite ingreso | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10005 | Historia | Cerrar sesión y acotar las rutas por papel | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10006 | Historia | Cambiar la contraseña propia presentando la vigente | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10026 | Historia | Distinguir el listado vacío del fallo por el tipo recibido | Alta | Sin fijar | Equipo (1) | Pendiente |
| `c` | US-10027 | Historia | Reconexión y estado degradado como dos tramos independientes | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10001 | Historia | Registrar la cuenta sin campo de contraseña | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10002 | Historia | Rechazar el registro con un correo ya usado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10009 | Historia | Ver la lista de cuentas y habilitar, bloquear y rehabilitar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10030 | Historia | Resetear la contraseña desde el panel | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10010 | Historia | Dar de baja exigiendo el correo escrito | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10007 | Historia | El mismo formulario en los tres cursos de la credencial | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10028 | Historia | Cambiar la contraseña obligada y levantar la marca | Alta | Sin fijar | Equipo (1) | Pendiente |
| `d` | US-10029 | Historia | Confinar la cuenta marcada a una sola ruta, sin sesión de trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | BT-10022 | Tarea técnica | Elevar el volumen de la comisión y la ausencia de paginación | Media | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10011 | Historia | Pegar el texto del trabajo y enviarlo sin reescribir un carácter | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10015 | Historia | Ver los trabajos propios con sus cuatro estados | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10016 | Historia | Reeditar y eliminar sólo en `Borrador` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10022 | Historia | Recorrer la entrega de la comisión agrupada y filtrada | Alta | Sin fijar | Equipo (1) | Pendiente |
| `e` | US-10023 | Historia | No pedir los borradores y responder «no encontrado» | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | BT-10016 | Tarea técnica | Anfitrión del visor con las seis funciones y el ciclo de vida | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-10012 | Historia | Previsualizar antes de enviar, declarando que dibujar no es verificar | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-10013 | Historia | Ver las advertencias con el valor declarado y el derivado | Alta | Sin fijar | Equipo (1) | Pendiente |
| `f` | US-10014 | Historia | Ver los errores con índice de figura y campo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de comprometer `g` | BT-10018 | Tarea técnica | Verificar la liberación con diez recorridos de ida y vuelta (`PT-02`) | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-10017 | Tarea técnica | Leer la preferencia de movimiento reducido y traducirla a dos valores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-10010 | Tarea técnica | Confirmar el punto de quiebre y la proporción de la escena | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-10023 | Tarea técnica | Acompañar la decisión de versionar o ignorar el bundle generado | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-10018 | Historia | Abrir el trabajo y encontrar los mismos cuatro elementos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-10019 | Historia | Ver la lista de observaciones con su severidad y su par de valores | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-10020 | Historia | Explorar la estructura del texto como árbol colapsable | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-10021 | Historia | Sincronizar el árbol y la escena por índice de pieza | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-10017 | Historia | Ver el desenlace en el listado y el comentario al abrir el trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-10024 | Historia | Aprobar o rechazar con comentario opcional | Alta | Sin fijar | Equipo (1) | Pendiente |
| `h` | US-10025 | Historia | Eliminar cualquier trabajo que el administrador ve | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 30 historias y 23 tareas técnicas**, repartidas en las **ocho** etapas más el momento de medición de `PT-02`. La prioridad de la columna es de **ejecución dentro de la etapa** y no reemplaza a la MoSCoW del backlog.

**US-10021 figura con prioridad de ejecución `Alta`**, y su MoSCoW en 06 es **`Must`** desde el 2026-08-10. Era `Should` hasta esa fecha, y la contradicción era visible acá: está dentro de lo que `PT-02` mide y por eso **su ejecución no era diferible**, aunque su prioridad declarada lo admitiera. Es la tensión que 06 elevó como `PA-02` y que este plan **no resolvió** subiéndole la prioridad; la **cerró el Product Owner** promoviendo `F-13` a `Must Have` en `PRODUCT-INTAKE` **1.19**. Con eso, **las treinta historias de este plan son `Must`**, que es coherente con que este proyecto de código toque las ocho etapas comprometidas.

### 3.2 `GeometriaFactory-Visor`

Los identificadores son los del backlog de 06 y **ninguno se inventa acá**.

| Tramo | ID | Tipo | Descripción corta | Prioridad | Estimación | Asignado | Estado |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `a` | BT-12001 | Tarea técnica | Crear el proyecto del bundle con su cadena de construcción reproducible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-12002 | Tarea técnica | Guion de construcción propio del bundle, para el ciclo corto | Media | Sin fijar | Equipo (1) | Pendiente |
| `a` | BT-12003 | Tarea técnica | Decidir si el bundle generado se versiona o se ignora | Media | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12004 | Tarea técnica | Fachada plana con las seis funciones | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12005 | Tarea técnica | Registro de instancias con su invalidación | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12006 | Tarea técnica | Los siete códigos de condición desde su fuente única | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12007 | Tarea técnica | Lector del texto con las variantes de clave del emisor | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12008 | Tarea técnica | Servicio de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12009 | Tarea técnica | Anclar la versión del motor de dibujo y confinarlo a la capa 3 | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12010 | Tarea técnica | Disposición derivada del índice | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12012 | Tarea técnica | Liberar recursos y cortar el bucle al destruir | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12013 | Tarea técnica | Medir la puerta `PT-03` sobre el bundle generado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12014 | Tarea técnica | Medir la puerta `PT-02` sobre una página del anfitrión | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | BT-12016 | Tarea técnica | Inspeccionar la superficie del bundle generado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12001 | Historia | Crear una instancia del visor sobre un elemento de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12004 | Historia | Dibujar las piezas del texto del trabajo | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12009 | Historia | Resaltar en exclusiva la pieza del índice indicado | Alta | Sin fijar | Equipo (1) | Pendiente |
| Antes de `g` | US-12011 | Historia | Liberar los recursos de la instancia y cortar su bucle de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12011 | Tarea técnica | Gobierno de los dos movimientos automáticos en el bucle | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12015 | Tarea técnica | Página integradora sin backend, sample `S-1` | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12017 | Tarea técnica | Fijar los nombres internos de funciones, clases y campos | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | BT-12018 | Tarea técnica | Resolver el umbral de fluidez, o dejarlo declaradamente cualitativo | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12002 | Historia | Fijar el estado inicial de los dos movimientos al crear la instancia | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12003 | Historia | Informar la ausencia de capacidad gráfica en lugar de fallar en silencio | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12005 | Historia | Leer las dimensiones con las variantes de clave del emisor | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12006 | Historia | Enumerar toda pieza no dibujada con su índice y su condición | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12007 | Historia | Devolver la estructura del texto para que el anfitrión arme el árbol | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12008 | Historia | Derivar la disposición de cada pieza de su índice | Media | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12010 | Historia | Ajustar la escena al tamaño del elemento de dibujo | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12012 | Historia | Gobernar en vivo los dos movimientos automáticos | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12013 | Historia | Detener el movimiento al arrastrar y al no estar visible | Alta | Sin fijar | Equipo (1) | Pendiente |
| `g` | US-12014 | Historia | Ejercitar las seis funciones desde una página integradora sin backend | Alta | Sin fijar | Equipo (1) | Pendiente |

**Total comprometido: 14 historias y 18 tareas técnicas**, repartidas en dos etapas y un momento. **Las catorce historias están dentro del tramo comprometido de ocho etapas**: este proyecto de código no tiene ninguna de la fase `i…`.

**US-12008 y US-12009 figuran con prioridad de ejecución `Media` y `Alta` respectivamente**, y su MoSCoW en 06 es **`Must` en las dos** desde el 2026-08-10. La diferencia entre las dos columnas subsiste y tiene el mismo motivo de siempre: la prioridad de ejecución ordena **dentro** de la etapa y no dice qué se difiere, de modo que dos historias igual de comprometidas pueden tener orden distinto. Lo que desapareció es la contradicción: US-12009 está dentro de lo que `PT-02` mide y por eso su ejecución no era diferible **aunque su MoSCoW lo admitiera**, y esa tensión —que 06 elevó como `PA-06` y que este plan se negó a resolver subiéndole la prioridad— la **cerró el Product Owner** promoviendo `F-13` a `Must Have` en `PRODUCT-INTAKE` **1.19**.

## 4. Alcance técnico y orden de construcción

### 4.1 `GeometriaFactory-Web`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md), ni rediseña superficies: ésas están en [`../03-UX-UI-DX/`](../03-UX-UI-DX/), emitidas y validadas contra una maqueta aprobada.

**Orden**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-10001 primero; BT-10002 y BT-10005 sobre él; BT-10003 sobre BT-10005; BT-10006 y BT-10012 después; **BT-10004 al final de la etapa pero antes que cualquier otra cosa del producto**, porque `PT-01` condiciona el modelo entero de esta pieza y su rojo tiene salida declarada.
2. `b`: BT-10007 primero, después BT-10008 y BT-10009; BT-10019 y BT-10020 al cerrar, porque son verificaciones sobre algo ya construido.
3. `c`: BT-10011 primero —es la única salida—, después BT-10014 y BT-10013; las siete historias sobre ellos; **BT-10015 al cerrar**, porque el conteo de red sólo tiene sentido sobre un recorrido completo; BT-10021 antes del punto de control.
4. `d`: el **cuarto guardián** de BT-10007 se completa acá, porque hasta ahora no existía la marca; las ocho historias después.
5. `e`: las cinco historias sobre lo ya construido; BT-10022 antes del punto de control.
6. `f`: BT-10016 primero; las tres historias después.
7. **Antes de comprometer `g`: BT-10018**, que es la parte de `PT-02` que se mide sobre una página de esta pieza. **Una puerta que no pasa detiene la planificación de la etapa `g`** y no se arrastra como deuda.
8. `g`: BT-10017 sobre BT-10016; las cuatro historias; BT-10010 y BT-10023 antes del punto de control.
9. `h`: las tres historias sobre lo ya construido; BT-10019 y BT-10020 se revisan por última vez.

**Reglas de dependencia interna que ninguna tarea puede cruzar** (`05` §3.2): **ninguna superficie invoca al cliente tipado**, **ninguna superficie invoca al interior del bundle**, **el cliente tipado es la única salida** y **el traductor no habla con el servicio de datos**. La quinta precisión de esa sección conviene repetirla: **la flecha punteada del diagrama es la que nunca existe**, y se dibuja porque una prohibición que no se dibuja no se audita.

**Consecuencia del nivel topológico 1**: esta pieza compila contra `GeometriaFactory-Contracts` y contra el bundle del visor, de modo que dentro de cada etapa su trabajo va **después** del de esos dos. Con `GeometriaFactory-Api` la relación es **de tiempo de ejecución y no de compilación**: no depende de él, pero **no puede demostrar una etapa sin que exista el punto de acceso que consume**.

### 4.2 `GeometriaFactory-Visor`

Esta sección **no redefine arquitectura**: referencia la de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md).

**Orden**, derivado de las dependencias de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) §3:

1. `a`: BT-12001 primero; BT-12002 y BT-12003 sobre él.
2. Antes de `g`: **BT-12009 temprano**, porque anclar la versión del motor condiciona toda la capa 3 y `05` §9 le asigna probabilidad **alta** al cambio de interfaz que puede exigir. Después BT-12004 y BT-12005, después BT-12007, después BT-12008 sobre los tres, después BT-12010 y BT-12012. Las cuatro historias del tramo, sobre esas tareas. **Al final BT-12013, BT-12014 y BT-12016**, que son las mediciones y sólo tienen sentido sobre algo terminado.
3. `g`: BT-12011 sobre BT-12004, BT-12008 y BT-12010; las diez historias del tramo; BT-12015 al final, porque el sample recorre las seis funciones; BT-12017 y BT-12018 antes del punto de control.

**Regla de dependencias entre capas, que ninguna tarea puede cruzar**: la capa 1 no conoce el interior, la capa 2 no contiene lógica de dibujo y la capa 3 no conoce al anfitrión (`05` §3.1).

**Consecuencia del nivel topológico 0**, que acá es más fuerte que en los otros dos proyectos de código del mismo nivel: **el bundle se ejercita sin backend**, de modo que todo el tramo del medio se puede construir y medir sin que exista ninguna otra pieza del producto más allá de una página que lo cargue. Es lo que hace realizable la exigencia del roadmap de medir `PT-02` y `PT-03` antes de comprometer la etapa `g`.

## 5. Definition of Done aplicada

### 5.1 `GeometriaFactory-Web`

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **El guion de demostración es acumulativo y es puerta.** El **100 %** de los pasos del guion de la etapa **y de todas las anteriores** se ejecuta y pasa antes del punto de control, en el navegador del equipo anfitrión. **Este proyecto de código no tiene proyecto de pruebas propio** y ésta es su verificación.
3. **Las 61 filas de la matriz de sensado de deriva se verifican al cierre de cada corte**, no una sola vez: **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base aprobada.
4. **Las mediciones de ausencia se hacen con su condición declarada.** El conteo de peticiones del navegador se hace **durante un recorrido completo, incluida la interacción con la escena y con los dos movimientos automáticos prendidos**, que es el peor caso declarado por la Fase C del visor. Una medición sin esa condición no cuenta como hecha.
5. **La etapa `g` no se compromete sin `PT-02` y `PT-03` medidas** (`Roadmap-Producto.md` §2.2). Una puerta que no pasa detiene la planificación y **no se arrastra como deuda**.
6. **Ningún guion que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20.
7. **La publicación no se declara hecha en la subida**: se declara hecha cuando la **dirección pública responde**.

### 5.2 `GeometriaFactory-Visor`

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5 y las dos puertas técnicas.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **Las mediciones de ausencia se hacen con sus condiciones declaradas**, que `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 fija como lugar único: cero red **con los dos movimientos prendidos y sostenidos**, y los diez recorridos **también con los movimientos prendidos**. Una medición sin esas condiciones no cuenta como hecha.
3. **La verificación de cero red se hace sobre el bundle generado y no sólo sobre el código fuente.**
4. **La etapa `g` no se compromete sin `PT-02` y `PT-03` medidas.** Una puerta que no pasa detiene la planificación y **no se arrastra como deuda**.
5. **El material de dibujo son los escenarios `E-1` y `E-7` del intake §20**, y para `DIMENSION_NO_LEGIBLE` el `E-8`. **No se inventan textos de prueba.**
6. **El sample `S-1` está funcionando al cerrar la etapa `g`**: es el punto de extensión declarado del producto y su demostración, no un agregado de conveniencia.

## 6. Riesgos y mitigaciones

### 6.1 `GeometriaFactory-Web`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca un guion del navegador que llame al servicio de datos —una validación mientras se escribe, una biblioteca agregada que consulte por su cuenta— | Media: **es la forma habitual en que este defecto entra, y siempre por una comodidad de interfaz** | **Muy alto**: reabre contenido mixto, restricción de origen cruzado y exposición de la dirección del servidor propio, y rompe `RA-01` | BT-10015, con **0** peticiones y **1** sola salida contadas en la pestaña de red con los movimientos prendidos; y la regla de diseño de 03 de que **ninguna validación consulta al servidor mientras se escribe** (`05` §9, primer riesgo) |
| Que el proceso del hosting recicle y la persona pierda la sesión en mitad de un acto | Media, **y medida**: es `PT-01.c` | Alto: es el peor escenario y la fuente declara que **no tiene mitigación en el código** (`R-06`) | **No hay mitigación técnica que inventar.** Lo que hay es tratamiento: el estado «sesión no restablecible» es un estado propio de la superficie de reconexión —US-10027— y **el envío es la única acción de guardado**, de modo que un corte no deja un trabajo a medias |
| Que un mensaje mostrado lleve una dirección de servicio, una ruta de datos o una traza | Media: entra por el camino de excepción, que es el menos ensayado | Alto: viola `RA-03` y expone la topología que la partición del producto protege | BT-10013, con el traductor como **único** lugar por el que un mensaje llega a la persona, y su NFR de **0** sobre los diecisiete códigos vivos **y** sobre el camino de ausencia de respuesta (`05` §9, tercer riesgo) |
| Que un componente termine tocando el interior del bundle porque la fachada no expone algo que una pantalla necesita | Media: es la presión natural cuando una superficie necesita algo que las seis funciones no dan | Alto: se pierde el punto de extensión declarado del producto | BT-10016, con **0** invocaciones al interior y **0** accesos al elemento de dibujo por fuera del anfitrión, y el procedimiento de [`Visor Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 (`05` §9, cuarto riesgo) |
| Que la liberación de la instancia no se invoque y recorrer trabajos acumule contextos gráficos | Media: **es la clase de omisión que no falla la primera vez** | Alto: degradación progresiva, que es lo que `PT-02` mide | BT-10018, con **10** recorridos de ida y vuelta medidos **con los movimientos prendidos**, y `RT-05`, que declara que la liberación **no es opcional** (`05` §9, quinto riesgo) |
| Que una subida deje la aplicación caída y se reporte como exitosa | Media: **la subida no es transaccional** (`R-03`) | Alto: el producto queda inaccesible sin que nadie se entere | BT-10006, con la puerta que hace que el flujo **no termine en la subida sino en la comprobación de que la dirección pública responde**, y el despliegue fuera del horario de uso |
| Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Media, **y es el trade-off que el ensamblado de contratos aceptó por escrito** al no imponer formato | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red que este producto tiene | BT-10012, que **adopta** la configuración única que fija la categoría 05 de `GeometriaFactory-Api`, verificada **ejerciendo el servicio real** y no comparando dos archivos |
| Que la etapa `g` se comprometa antes de medir `PT-02` y `PT-03` | Media, porque el trabajo del visor se lee fácilmente como si viviera **dentro** de la etapa `g` | Alto: es exactamente lo que la regla de puertas del intake prohíbe | El tramo «antes de comprometer `g`» de §3, con BT-10018, que existe para que ese momento sea visible en la planificación y no una nota al pie |

### 6.2 `GeometriaFactory-Visor`

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca una petición de red en el bundle, por comodidad o **por una dependencia que la haga por dentro** | Baja para la primera causa, **media para la segunda** | **Muy alto**: reabre contenido mixto, restricción de origen cruzado y exposición de la dirección del servidor propio, y rompe `RA-01` a través de `RA-02` | BT-12016, inspección con cero ocurrencias de las tres formas de petición **en el código fuente y en el bundle generado**, más el conteo en la pestaña de red con los movimientos prendidos (`05` §9, primer riesgo) |
| Que la versión del motor de dibujo que se ancle exija una interfaz distinta de la del visualizador previo | **Alta**: el intake ya lo anticipa, porque el visualizador previo reimplementa la cámara orbital a mano por una carencia de su versión | Medio: retrabajo acotado a la capa 3 | BT-12009 **temprano** en el tramo del medio, y el confinamiento del motor a la capa 3 que [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) declara (`05` §9, cuarto riesgo) |
| Que un bucle de dibujo sobreviva a la destrucción y se acumule al recorrer trabajos | Media | Alto: degradación progresiva, que es lo que `PT-02` mide | BT-12012 y BT-12014, con los diez recorridos medidos **con los movimientos prendidos**, que es su peor caso (`05` §9, tercer riesgo) |
| Que el anfitrión termine dependiendo de nombres internos del motor de dibujo y el motor deje de ser reemplazable | Media: es la presión natural cuando una pantalla necesita algo que la fachada no expone | Alto: se pierde el punto de extensión declarado del producto | [`ADR-12001`](../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) y [`Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5, que declara qué se hace cuando falta algo en la fachada (`05` §9, segundo riesgo) |
| Que se acuñe un código de condición fuera de la categoría 02 | Media: el catálogo de 03 ya creció **sin** que creciera el conjunto de códigos, y esa distinción es fácil de perder | Medio: el conjunto deja de ser cerrado y 03 y 08 se desincronizan | BT-12006 y el criterio 6 de la DoR, que no admite excepción: los códigos son siete, su fuente única es el contrato de fachada y un curso nuevo es fila de curso y no código (`05` §9, sexto riesgo) |
| Que la etapa `g` se comprometa antes de medir `PT-02` y `PT-03` | Media, porque el trabajo de este proyecto de código se lee fácilmente como si viviera **dentro** de la etapa `g` | Alto: es exactamente lo que la regla de puertas del intake prohíbe | Los **tres tramos** de §1.3 y la épica EP-12002 del backlog, que existen para que ese momento sea visible en la planificación y no una nota al pie |

## 7. Criterios de hecho de cada tramo

### 7.1 `GeometriaFactory-Web`

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 se cumplen.
- [ ] El **100 %** de los pasos del guion de demostración de la etapa y de todas las anteriores se ejecutó y pasó.
- [ ] Las filas de la matriz de sensado de deriva que la etapa alcanza están verificadas contra la línea de base aprobada.
- [ ] Las mediciones de ausencia se hicieron **con sus condiciones declaradas**, incluida la de los dos movimientos prendidos.
- [ ] Para la etapa `a`: las **cuatro** partes de `PT-01` están medidas y documentadas, y `PT-04` también.
- [ ] Para el tramo previo a `g`: `PT-02` y `PT-03` están medidas y pasan **antes** de que la etapa `g` se comprometa.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

### 7.2 `GeometriaFactory-Visor`

Un tramo de este proyecto de código está hecho cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Las **seis** propiedades transversales se midieron **con sus condiciones declaradas**.
- [ ] Para el tramo del medio: `PT-02` y `PT-03` están medidas y pasan, **antes** de que la etapa `g` se comprometa.
- [ ] Para la etapa `g`: los criterios propios de la transición `g` → `h` de §5.2 que alcanzan a este proyecto de código se cumplen, incluido el gobierno independiente de los dos movimientos automáticos.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

### 8.1 `GeometriaFactory-Web`

| Etapa | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| `a` | NB-00008 en su parte de acceso medido: `PT-01` es la viabilidad del laboratorio desde el aula | Ninguno de los diez: la página de salud no es un caso de uso | ADR-10001, ADR-10002, ADR-10007 |
| `b` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-10004 |
| `c` | NB-00001, NB-00002, NB-00008 | CU-10002, CU-10003, CU-10004 (FA-03), CU-10010 | ADR-10001, ADR-10003, ADR-10005 |
| `d` | NB-00001, NB-00002 | CU-10001, CU-10003, CU-10004 | ADR-10003, ADR-10005 |
| `e` | NB-00003, NB-00007, NB-00009 | CU-10005, CU-10006, CU-10008 | ADR-10002, ADR-10005 |
| `f` | NB-00004, NB-00005 | CU-10005 | ADR-10005, ADR-10006 |
| `g` | NB-00006, NB-00005, NB-00004 | CU-10007 | ADR-10006 |
| `h` | NB-00009, NB-00007 | CU-10006, CU-10007, CU-10009 | ADR-10005 |

**Las nueve necesidades de negocio avanzan en alguna etapa de este proyecto de código.** Es uno de los pocos del producto que las cubre todas, y el grado en que sostiene cada una está declarado en [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §4.1: **cuatro** las sostiene de forma parcial —NB-00004, NB-00005, NB-00008— y `NB-00006` **casi por entero**, con el dibujo del lado del bundle.

**Puertas técnicas del producto y este proyecto de código.** **`PT-01` en sus cuatro partes es de esta pieza** y se mide en la etapa `a` antes que cualquier otra cosa. **`PT-02` la alcanza** en su parte medida sobre una página del anfitrión, antes de comprometer la etapa `g`. `PT-03` es del bundle, `PT-04` del servicio de datos y `PT-05` del despliegue real de la fase `i`.

### 8.2 `GeometriaFactory-Visor`

| Tramo | NB que avanzan | CU que avanzan | ADR que gobiernan las decisiones |
| --- | --- | --- | --- |
| Etapa `a` | Ninguna: es un hito interno sin capacidad funcional asociada | Ninguno | ADR-12006 |
| Antes de comprometer `g` | NB-00006, y NB-00004 en su parte de piezas dibujadas | CU-12001, CU-12002, CU-12003, CU-12005 | ADR-12001, ADR-12002, ADR-12003, ADR-12004, ADR-12005 |
| Etapa `g` | NB-00006, NB-00004 (parcial), y NB-00008 por contribución negativa | CU-12001, CU-12002, CU-12004, CU-12006, CU-12007 | ADR-12001, ADR-12002, ADR-12003, ADR-12006 |

**Este proyecto de código sostiene una sola necesidad de negocio entera, `NB-00006`**, y toca otras dos parcialmente: `NB-00004` sólo en la parte de que las piezas se dibujen, y `NB-00008` por contribución **negativa** —no hacer red—, que se verifica en `CU-12006` pero que no implementa ninguna capacidad de esa necesidad (`02` §5.3). Las **seis** restantes las implementan otros proyectos de código y no quedan sin cubrir por esta declaración.

**Puertas técnicas del producto y este proyecto de código.** `PT-02` y `PT-03` **son de este proyecto de código** y se miden antes de comprometer la etapa `g`; son las dos únicas de las cinco que lo alcanzan. `PT-01` y `PT-04` son del front y del servicio de datos, y `PT-05` del despliegue real de la fase `i`.

## 9. Bitácora de avance

### 9.1 `GeometriaFactory-Web`

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es el **resultado de las cuatro partes de `PT-01`**, incluido el color del semáforo del transporte y la latencia percibida si hubo repliegue; para el tramo previo a `g`, el **resultado de las dos puertas del visor**.

### 9.2 `GeometriaFactory-Visor`

**Sin entradas al 2026-08-10.** Ningún tramo está abierto.

| Fecha | Tramo | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada tramo**. Para el tramo del medio, lo que se registra es el **resultado de las dos puertas**, que es lo que habilita a comprometer la etapa `g`.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
