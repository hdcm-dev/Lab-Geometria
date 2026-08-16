# Mini-Plan — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Mini-Plan.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-07)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) **1.1** (ocho épicas, treinta historias), [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) 1.0 (veintitrés tareas técnicas) y [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) 1.0; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.1, §2.2, §4 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.19** §2, §10, §15, §16.1 y §17.6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §3
**Trazabilidad downstream:** `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Web

---

## Tabla de contenido

- [1. Información general](#1-información-general)
  - [1.1 Por qué esta categoría emite un mini-plan y no planes de iteración](#11-por-qué-esta-categoría-emite-un-mini-plan-y-no-planes-de-iteración)
  - [1.2 Capacidad disponible](#12-capacidad-disponible)
  - [1.3 Las ocho etapas y el momento de las dos puertas del visor](#13-las-ocho-etapas-y-el-momento-de-las-dos-puertas-del-visor)
- [2. Objetivo de cada tramo](#2-objetivo-de-cada-tramo)
- [3. Ítems comprometidos por tramo](#3-ítems-comprometidos-por-tramo)
- [4. Alcance técnico y orden de construcción](#4-alcance-técnico-y-orden-de-construcción)
- [5. Definition of Done aplicada](#5-definition-of-done-aplicada)
- [6. Riesgos y mitigaciones](#6-riesgos-y-mitigaciones)
- [7. Criterios de hecho de cada tramo](#7-criterios-de-hecho-de-cada-tramo)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Bitácora de avance](#9-bitácora-de-avance)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Información general

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

## 2. Objetivo de cada tramo

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

## 3. Ítems comprometidos por tramo

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

## 4. Alcance técnico y orden de construcción

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

## 5. Definition of Done aplicada

**La DoD canónica vive en `08-Calidad-Y-Pruebas` y todavía no está emitida.** Este plan la referencia por destino y **no la redefine**; hasta que exista, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.

Criterios específicos que este plan agrega:

1. **La actualización de la categoría 11 forma parte del cierre.** La categoría 11 de este proyecto de código todavía no está emitida; hasta su emisión la condición se cumple de forma vacía y **se registra así en el informe de cierre**.
2. **El guion de demostración es acumulativo y es puerta.** El **100 %** de los pasos del guion de la etapa **y de todas las anteriores** se ejecuta y pasa antes del punto de control, en el navegador del equipo anfitrión. **Este proyecto de código no tiene proyecto de pruebas propio** y ésta es su verificación.
3. **Las 61 filas de la matriz de sensado de deriva se verifican al cierre de cada corte**, no una sola vez: **74 de 74** estados, **11 de 11** superficies, **73 de 73** componentes y **24 de 24** rutas de la línea de base aprobada.
4. **Las mediciones de ausencia se hacen con su condición declarada.** El conteo de peticiones del navegador se hace **durante un recorrido completo, incluida la interacción con la escena y con los dos movimientos automáticos prendidos**, que es el peor caso declarado por la Fase C del visor. Una medición sin esa condición no cuenta como hecha.
5. **La etapa `g` no se compromete sin `PT-02` y `PT-03` medidas** (`Roadmap-Producto.md` §2.2). Una puerta que no pasa detiene la planificación y **no se arrastra como deuda**.
6. **Ningún guion que involucre el texto de figuras usa datos inventados**: se usan los escenarios `E-1` a `E-8` del intake §20.
7. **La publicación no se declara hecha en la subida**: se declara hecha cuando la **dirección pública responde**.

## 6. Riesgos y mitigaciones

| Riesgo | Probabilidad | Impacto | Mitigación |
| --- | --- | --- | --- |
| Que aparezca un guion del navegador que llame al servicio de datos —una validación mientras se escribe, una biblioteca agregada que consulte por su cuenta— | Media: **es la forma habitual en que este defecto entra, y siempre por una comodidad de interfaz** | **Muy alto**: reabre contenido mixto, restricción de origen cruzado y exposición de la dirección del servidor propio, y rompe `RA-01` | BT-10015, con **0** peticiones y **1** sola salida contadas en la pestaña de red con los movimientos prendidos; y la regla de diseño de 03 de que **ninguna validación consulta al servidor mientras se escribe** (`05` §9, primer riesgo) |
| Que el proceso del hosting recicle y la persona pierda la sesión en mitad de un acto | Media, **y medida**: es `PT-01.c` | Alto: es el peor escenario y la fuente declara que **no tiene mitigación en el código** (`R-06`) | **No hay mitigación técnica que inventar.** Lo que hay es tratamiento: el estado «sesión no restablecible» es un estado propio de la superficie de reconexión —US-10027— y **el envío es la única acción de guardado**, de modo que un corte no deja un trabajo a medias |
| Que un mensaje mostrado lleve una dirección de servicio, una ruta de datos o una traza | Media: entra por el camino de excepción, que es el menos ensayado | Alto: viola `RA-03` y expone la topología que la partición del producto protege | BT-10013, con el traductor como **único** lugar por el que un mensaje llega a la persona, y su NFR de **0** sobre los diecisiete códigos vivos **y** sobre el camino de ausencia de respuesta (`05` §9, tercer riesgo) |
| Que un componente termine tocando el interior del bundle porque la fachada no expone algo que una pantalla necesita | Media: es la presión natural cuando una superficie necesita algo que las seis funciones no dan | Alto: se pierde el punto de extensión declarado del producto | BT-10016, con **0** invocaciones al interior y **0** accesos al elemento de dibujo por fuera del anfitrión, y el procedimiento de [`Visor Extensibilidad.md`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Extensibilidad.md) §5 (`05` §9, cuarto riesgo) |
| Que la liberación de la instancia no se invoque y recorrer trabajos acumule contextos gráficos | Media: **es la clase de omisión que no falla la primera vez** | Alto: degradación progresiva, que es lo que `PT-02` mide | BT-10018, con **10** recorridos de ida y vuelta medidos **con los movimientos prendidos**, y `RT-05`, que declara que la liberación **no es opcional** (`05` §9, quinto riesgo) |
| Que una subida deje la aplicación caída y se reporte como exitosa | Media: **la subida no es transaccional** (`R-03`) | Alto: el producto queda inaccesible sin que nadie se entere | BT-10006, con la puerta que hace que el flujo **no termine en la subida sino en la comprobación de que la dirección pública responde**, y el despliegue fuera del horario de uso |
| Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Media, **y es el trade-off que el ensamblado de contratos aceptó por escrito** al no imponer formato | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red que este producto tiene | BT-10012, que **adopta** la configuración única que fija la categoría 05 de `GeometriaFactory-Api`, verificada **ejerciendo el servicio real** y no comparando dos archivos |
| Que la etapa `g` se comprometa antes de medir `PT-02` y `PT-03` | Media, porque el trabajo del visor se lee fácilmente como si viviera **dentro** de la etapa `g` | Alto: es exactamente lo que la regla de puertas del intake prohíbe | El tramo «antes de comprometer `g`» de §3, con BT-10018, que existe para que ese momento sea visible en la planificación y no una nota al pie |

## 7. Criterios de hecho de cada tramo

Una etapa de este proyecto de código está hecha cuando:

- [ ] Todas sus historias y tareas comprometidas en §3 están en estado terminado.
- [ ] Los criterios comunes a toda transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5.1 se cumplen, incluida la no regresión sin correcciones.
- [ ] Los criterios propios de la transición correspondiente de su §5.2 se cumplen.
- [ ] El **100 %** de los pasos del guion de demostración de la etapa y de todas las anteriores se ejecutó y pasó.
- [ ] Las filas de la matriz de sensado de deriva que la etapa alcanza están verificadas contra la línea de base aprobada.
- [ ] Las mediciones de ausencia se hicieron **con sus condiciones declaradas**, incluida la de los dos movimientos prendidos.
- [ ] Para la etapa `a`: las **cuatro** partes de `PT-01` están medidas y documentadas, y `PT-04` también.
- [ ] Para el tramo previo a `g`: `PT-02` y `PT-03` están medidas y pasan **antes** de que la etapa `g` se comprometa.
- [ ] El informe de cierre de la etapa está escrito y es autocontenido, con su índice.
- [ ] Los documentos de la categoría 11 afectados están revisados, o se registra que la categoría todavía no está emitida.
- [ ] El Product Owner dio **OK explícito** en el punto de control, y la rama está incorporada antes de abrir la siguiente.

## 8. Trazabilidad

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

## 9. Bitácora de avance

**Sin entradas al 2026-08-10.** Ninguna etapa está abierta: el producto está en fase de especificación.

| Fecha | Etapa | Qué se cerró | Qué quedó abierto | Punto de control |
| --- | --- | --- | --- | --- |
| — | — | — | — | — |

La bitácora se completa **al cerrar cada etapa**, junto con el informe de cierre. Para la etapa `a` lo que se registra es el **resultado de las cuatro partes de `PT-01`**, incluido el color del semáforo del transporte y la latencia percibida si hubo repliegue; para el tramo previo a `g`, el **resultado de las dos puertas del visor**.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.2 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial del mini-plan de `GeometriaFactory-Web`. Declara por qué la categoría emite un único artefacto —`equipo_n = 1`, con los cuatro omitidos— y por qué no se declara capacidad numérica, con el fundamento propio de que la categoría 05 ya se negó a inventar el umbral de tiempo de respuesta. Declara que este proyecto de código toca **las ocho** etapas comprometidas y que además participa del **momento** de medición de `PT-02` y `PT-03`, con la constancia explícita de que ese momento **no es una etapa nueva** sino uno que el roadmap §2.2 ya declara. Compromete las **30** historias y las **23** tareas técnicas del backlog de 06 sin inventar ningún identificador, declara el orden de construcción con `PT-01` medida antes que cualquier otra cosa y con las cuatro reglas de dependencia interna que ninguna tarea puede cruzar, referencia la Definition of Done por destino con la constancia de que 08 todavía no está emitida, y declara **ocho** riesgos con mitigación, incluido el que la fuente declara **sin mitigación en el código**. Registra la tensión de `PA-02` sin resolverla reprioritizando. |
| 1.1 | 2026-08-11 | **Absorbe la promoción de `F-13` a `Must Have`** (`PRODUCT-INTAKE` **1.19** §4), que cierra la tensión de `PA-02`. La cabecera pasa a citar el intake **1.19** y la versión **1.1** del `Product-Backlog.md` que este plan consume. **§3**: la nota de US-10021 declara que su MoSCoW es hoy `Must`, conserva la constancia de que este plan **no** resolvió la tensión reprioritizando y registra quién la resolvió y cuándo. **Ninguna etapa, ningún orden de construcción, ningún riesgo y ningún compromiso cambia**: US-10021 ya estaba comprometida con prioridad de ejecución `Alta`, que es lo que hacía visible la contradicción. Sube minor. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
