# Alcance del Producto

**Producto:** Fábrica de Geometría
**Documento:** Alcance-Producto.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE 1.5 §1 (problema), §3 (propuesta de valor), §4 (alcance funcional pretendido con MoSCoW), §4.1 (reglas de negocio declaradas), §4.2 (modelo de estados del trabajo), §5 (historias de usuario), §6 (flujos típicos), §7 (casos límite), §9 (exclusiones), §10 (restricciones del cliente), §12 (glosario), §15 (esquema de descomposición y delivery), §22 (supuestos declarados)
**Trazabilidad downstream:** 01-Necesidades-Negocio, 02-Especificacion-Funcional, 03-UX-UI-DX, 05-Arquitectura-Tecnica, 07-Plan-Sprint, 10-Examples

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Descripción general](#2-descripción-general)
  - [2.1 Qué hace el producto](#21-qué-hace-el-producto)
  - [2.2 Cómo se entrega](#22-cómo-se-entrega)
- [3. Objetivos del producto](#3-objetivos-del-producto)
- [4. Alcance incluido](#4-alcance-incluido)
  - [4.1 Capacidades comprometidas](#41-capacidades-comprometidas)
  - [4.2 Capacidades declaradas con prioridad menor](#42-capacidades-declaradas-con-prioridad-menor)
  - [4.3 Entregables](#43-entregables)
  - [4.4 Ambientes](#44-ambientes)
- [5. Alcance excluido](#5-alcance-excluido)
- [6. Supuestos](#6-supuestos)
  - [6.1 Supuestos declarados por el intake](#61-supuestos-declarados-por-el-intake)
  - [6.2 Incógnitas que se resuelven midiendo](#62-incógnitas-que-se-resuelven-midiendo)
- [7. Restricciones](#7-restricciones)
- [8. Criterios de aceptación del producto](#8-criterios-de-aceptación-del-producto)
- [9. Gestión de cambios de alcance](#9-gestión-de-cambios-de-alcance)
- [10. Trazabilidad](#10-trazabilidad)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Propósito

Este documento fija qué entra y qué no entra en **Fábrica de Geometría**, con la justificación de cada exclusión y los supuestos de los que depende el alcance. Su función aguas abajo es que la categoría 02 no genere casos de uso para lo que está excluido y que la categoría 07 no planifique lo que no está comprometido.

El alcance no se origina acá: se formaliza. La priorización de las capacidades y las exclusiones son decisiones del Product Owner, declaradas en PRODUCT-INTAKE §4 y §9, y este documento las deriva y las traza a su sección de origen.

## 2. Descripción general

### 2.1 Qué hace el producto

Fábrica de Geometría toma el texto que el alumno produce con su Actividad 1, lo guarda como un trabajo con dueño, fecha y estado, lo interpreta, le señala las discrepancias entre los valores que el alumno declara y los que se derivan de las dimensiones, lo muestra en tres dimensiones y como árbol de su estructura, y lo pone a disposición del docente para que revise a toda la comisión desde un solo lugar.

Cerrado el circuito, cada trabajo recibido tiene desenlace: el administrador lo **aprueba** o lo **rechaza**, y puede dejarle al alumno un comentario escrito.

Dos personas lo usan: el **alumno**, que se registra, carga, previsualiza y envía; y el **administrador**, que es el docente, que habilita cuentas, revisa las entregas y decide sobre cada una.

### 2.2 Cómo se entrega

El producto se entrega en **dos piezas desplegables** —la **pieza pública** y la **pieza de datos**, en los términos de `Vision-Producto.md` §9.2—, y esa partición no es una preferencia sino la respuesta a dos restricciones de negocio que no se pueden satisfacer con una sola pieza desplegable (PRODUCT-INTAKE §10 y §14):

- La red desde la que trabajan los alumnos **bloquea el acceso al servidor propio del docente**.
- El lugar público donde sí se puede llegar **no conserva los datos**.

De ahí la partición: la pieza pública vive donde no la bloquean, y la pieza de datos vive donde los datos persisten. La persona sólo toca la pieza pública; la pieza de datos no es alcanzable desde el navegador. El detalle técnico de esta separación pertenece a la categoría 05 y no se decide acá.

## 3. Objetivos del producto

Los objetivos, con su métrica, target y plazo, están en `Vision-Producto.md` §5 y no se duplican. En términos de alcance:

| Id | Objetivo | Qué acota del alcance |
|---|---|---|
| OBJ-01 | Cerrar cada etapa comprometida con OK explícito en su punto de control | Fija que el alcance comprometido es el que cubren las etapas `a` a `h`, y nada más. El target declarado del objetivo cuenta siete etapas y tiene un punto abierto escalado al Product Owner, declarado en `Vision-Producto.md` §5; el conjunto de etapas comprometidas, en cambio, lo declara el intake §15 y son ocho |
| OBJ-02 | Que al menos el 80 % de los alumnos habilitados llegue a tener un trabajo en estado `Pendiente` o posterior | Obliga a que el circuito completo de registro, carga, validación y envío entre en el alcance comprometido |
| OBJ-03 | Que el 100 % de los trabajos que llegan a estado `Pendiente` reciba desenlace | Obliga a que el circuito de revisión del administrador —aprobar, rechazar y comentar— entre en el alcance comprometido y no quede diferido |
| OBJ-04 | Que el producto muestre al menos una advertencia de valor declarado contra derivado por alumno con figuras afectadas | Obliga a que la verificación de valores calculados sea capacidad comprometida y no diferida |

## 4. Alcance incluido

### 4.1 Capacidades comprometidas

Dieciséis capacidades con prioridad **Must Have** declarada en PRODUCT-INTAKE §4, de origen mixto: doce son la traducción directa de los requerimientos funcionales que la fuente declara cerrados, y las cuatro del circuito de revisión —F-21, F-22, F-23 y F-24— no vienen de esos requerimientos sino de la decisión del Product Owner del 2026-08-08.

| Id | Capacidad incluida | Etapa que la entrega |
|---|---|---|
| F-01 | Configurar la cuenta de administrador en el primer arranque, y sólo mientras no exista ninguna | `c` |
| F-02 | Registro de alumno con correo, nombre y apellido, sin elegir contraseña | `d` |
| F-03 | Habilitar, bloquear, rehabilitar y dar de baja física cuentas de alumno desde el panel del administrador | `d` |
| F-04 | Establecer contraseña en el primer ingreso efectivo del alumno, sin envío de correo | `d` |
| F-05 | Inicio y cierre de sesión, y cambio de contraseña exigiendo la actual | `c` |
| F-06 | Cargar un trabajo con nombre, fecha, descripción y el texto de figuras, con identificador propio | `e` |
| F-07 | Reeditar y eliminar el trabajo **sólo mientras está en estado `Borrador`**, que es el estado en el que queda cuando el texto no verifica | `e` |
| F-08 | Listar los trabajos propios con su estado `Borrador`, `Pendiente`, `Finalizado` o `Rechazado` | `e` |
| F-09 | Validar el texto con la tolerancia de claves del emisor real y reportar errores con índice de figura y campo | `f` |
| F-10 | Verificar área y volumen recalculándolos desde las dimensiones y emitir advertencias que no bloquean | `f` |
| F-11 | Previsualizar el trabajo en tres dimensiones y ver la estructura del texto como árbol colapsable | `g` |
| F-12 | Listado para el administrador de todos los trabajos **excepto los que están en estado `Borrador`**, con agrupación y filtro por alumno | `e` |
| F-22 | **Enviar** el trabajo: acción única que interpreta el texto y, si verifica, lo pasa a estado `Pendiente`; si no verifica, lo deja en `Borrador` con sus errores localizados | `f` |
| F-23 | **Aprobar o rechazar** un trabajo en estado `Pendiente`, facultad exclusiva del administrador: aprobar lo pasa a `Finalizado` y rechazar a `Rechazado`, ambos terminales | `h` |
| F-21 | **Comentario escrito del administrador** sobre el trabajo, opcional tanto al aprobar como al rechazar. Es texto libre, sin nota ni escala | `h` |
| F-24 | **Eliminar cualquier trabajo que el administrador ve**, en cualquier estado, con borrado físico | `h` |

La correspondencia entre capacidad y etapa se lee de PRODUCT-INTAKE §15 y se detalla en `Roadmap-Producto.md` §3.

El recuento de este apartado cubre **sólo las Must Have**. Las capacidades de prioridad menor están enumeradas en §4.2, no forman parte del alcance comprometido, y el hecho de que dos de ellas se entreguen sobre la superficie de una etapa comprometida no las incorpora a este recuento ni las vuelve bloqueantes.

### 4.2 Capacidades declaradas con prioridad menor

Están en el alcance declarado del producto, con prioridad explícita, y **ninguna está comprometida para el tramo `a` a `h`**: que una capacidad de esta tabla no se entregue no impide cerrar ninguna etapa ni el alcance comprometido.

Prioridad y etapa son dos cosas distintas y conviene no confundirlas. La prioridad la decide el Product Owner en PRODUCT-INTAKE §4 y dice **qué se difiere primero** si la etapa aprieta. La etapa la fija el corte vertical del intake §15 y dice **sobre qué superficie se construye la capacidad**. Por eso F-14 a F-17 caen en la etapa `i` y siguientes —necesitan superficie que el tramo comprometido no levanta—, mientras que **F-13 y F-25 se ubican en la etapa `g`**, que es la que construye la visualización tridimensional y el árbol: las dos son agregados sobre esa misma superficie y no tienen dónde existir antes ni motivo para esperar después. El fundamento completo de la etapa de F-25 está en `Roadmap-Producto.md` §3.

| Id | Capacidad | Prioridad declarada | Etapa | Origen |
|---|---|---|---|---|
| F-13 | Sincronización entre el árbol y la escena por índice de pieza, y disposición determinista entre procesados | Should Have | `g` | PRODUCT-INTAKE §4 |
| F-25 | Movimiento automático de la escena, opcional y con dos controles independientes: **órbita de la cámara** alrededor del conjunto y **giro de cada pieza sobre su eje**. Los dos se detienen mientras la persona arrastra, y arrancan apagados si el sistema declara preferencia de movimiento reducido | Should Have | `g` | PRODUCT-INTAKE §4 [DECISIÓN 2026-08-09, originada en la validación visual de la Fase B2] |
| F-14 | Despliegue real de las dos piezas desplegables, con la verificación de acceso medida desde la red de la facultad | Should Have | `i` | PRODUCT-INTAKE §4 |
| F-15 | Panel de resumen del administrador: cantidad de trabajos por alumno y por estado | Could Have | `i` | PRODUCT-INTAKE §4 |
| F-16 | Exportar el trabajo: el texto original y una captura de la escena | Could Have | `i` | PRODUCT-INTAKE §4 |
| F-17 | Modo despiece: expandir un volumen y ver sus caras separadas | Could Have | `i` | PRODUCT-INTAKE §4 |

F-16 y F-17 aparecen además en la exclusión X-6 de §5, y no hay contradicción: la exclusión declara que no entran en el alcance comprometido y que son candidatas de la etapa posterior al cierre, que es exactamente la prioridad Could Have que el intake les asigna.

**Sobre F-25 y su origen.** Entra al alcance declarado el 2026-08-09, por decisión del Product Owner registrada en `PRODUCT-INTAKE` 1.5 §4 al mirar la maqueta de la Fase B2. Sus dos movimientos tienen procedencia distinta y conviene conservarla: la **órbita de la cámara ya existe en la visualización que la cátedra usa hoy** y se porta al producto, y el **giro de las piezas no existe** y es capacidad nueva. Su prioridad `Should Have` no es una concesión sino la clasificación correcta: es **comodidad de lectura, no capacidad de entrega**, y sin ella el trabajo se carga, se valida, se ve y se resuelve igual. Por eso no agrega ningún criterio de aceptación del producto a §8: un criterio de §8 compromete, y esta capacidad no está comprometida.

### 4.3 Entregables

| Entregable | Descripción | Origen |
|---|---|---|
| Producto en funcionamiento | Las dos piezas desplegables, operativas y demostradas de punta a punta | PRODUCT-INTAKE §13, §15 |
| Guion de demostración por etapa | Recorrido ejecutable delante del Product Owner en cada punto de control, acumulativo por la regla de no regresión | PRODUCT-INTAKE §15 |
| Informe de cierre por etapa | Documento autocontenido por etapa, con su índice, que se lee sin abrir el análisis ni el código | PRODUCT-INTAKE §15 |
| Material de prueba con datos reales | Los siete escenarios de datos verificados del intake, usados como material fijo de prueba y de demostración. **No se inventan datos de prueba** | PRODUCT-INTAKE §15, §20, §21 |
| Ejemplos de uso | Página de prueba de la visualización sin la pieza de datos, colección de peticiones del servicio y juego de datos de los siete escenarios | PRODUCT-INTAKE §18 |
| Documentación de especificación | El árbol `SDD/Docs/` que produce este framework | PRODUCT-INTAKE, trazabilidad downstream |

### 4.4 Ambientes

| Ambiente | Alcance | Nota |
|---|---|---|
| Desarrollo | Entorno de trabajo contenido y reproducible, definido en el propio repositorio. Todo el ciclo ocurre dentro de él | El equipo de desarrollo trabaja exclusivamente ahí (PRODUCT-INTAKE §10) |
| Producción | Dos destinos: el lugar público donde vive la pieza pública y el servidor propio del docente donde vive la pieza de datos | El despliegue lo ejecuta el docente a mano; no hay entrega automática al servidor propio (PRODUCT-INTAKE §10) |

No hay ambiente de preproducción ni de pruebas compartido: el alcance es de aula y el intake no declara ninguno.

## 5. Alcance excluido

Diez exclusiones declaradas por el Product Owner en PRODUCT-INTAKE §9, con su justificación. La columna de versión futura reproduce la condición de reingreso que el propio intake declara; donde el intake dice que no está previsto, acá también.

| Funcionalidad excluida | Justificación | Versión futura tentativa |
|---|---|---|
| X-1 · Notificaciones por correo | El flujo de contraseña está diseñado para **evitar** el envío de correo: la contraseña no se transporta nunca, la elige el alumno en su primer ingreso efectivo | No previsto. Incorporarlo cambiaría la capacidad F-04 |
| X-2 · Recuperación de contraseña olvidada | Consecuencia directa de X-1: sin correo no hay canal de recuperación. La resuelve el administrador dando de baja y volviendo a dar de alta | Sólo si se incorpora correo |
| X-3 · Múltiples administradores, roles configurables y permisos finos | El producto es deliberadamente básico: dos papeles fijos y un único administrador | No previsto en este alcance |
| X-4 · Corrección o edición del texto del alumno desde el producto | El texto original se conserva íntegro y nunca se reescribe: es la única fuente fiel del trabajo del alumno, y su formato es premisa fija | No previsto: contradice la premisa |
| ~~X-5 · Calificación o devolución escrita del administrador sobre el trabajo~~ · **Exclusión retirada el 2026-08-08** | Su justificación declarada era «no fue pedido» y su condición de reingreso era «si el docente lo pide». El Product Owner lo pidió, de modo que la condición se cumplió y la exclusión cayó por su propio enunciado. La devolución escrita entra al alcance comprometido como capacidad F-21, Must Have. **Lo que sigue excluido es la calificación con nota o escala**: el comentario es texto libre y no lleva puntaje | Retirada, no diferida. La fila se conserva porque una exclusión que desaparece sin registro es indistinguible de una que nunca existió |
| X-6 · Modo despiece, exportación de imágenes y dirección compartible de la visualización | Propuestas del análisis previo que no entraron en el alcance comprometido | Candidatos declarados de la etapa `i`, con la prioridad Could Have de F-16 y F-17 |
| X-7 · Ambientación a un problema real, del tipo depósito, costos o capacidades | Propuesta del análisis previo; no forma parte de este producto | Candidato de la etapa `i` |
| X-8 · Segundo factor de autenticación | No fue pedido; la forma de autenticación elegida no lo bloquea | No previsto |
| X-9 · Reenvío de peticiones desde la pieza pública hacia el servicio de datos | Hoy nada del navegador toca el servicio de datos, de modo que ese reenvío sólo consumiría el recurso más escaso del plan gratuito. Queda **especificado** para adoptarlo sin rediseño | Si aparece descarga de archivos, carga directa desde el navegador o un cambio del modelo de la pieza pública |
| X-10 · Canal saliente con dominio propio para el servicio de datos | Resolvería tres riesgos de una vez, pero exige un dominio propio y **debilita la premisa de la partición**: si el servicio de datos queda alcanzable desde la facultad, la pieza pública deja de ser necesaria | Reevaluar si aparece un dominio propio o si la medición temprana obliga a mover la pieza pública |

Ninguna exclusión vigente contradice una capacidad comprometida: X-1, X-2, X-3 y X-4 se corresponden con las capacidades declaradas Won't Have v1 (F-18, F-19 y F-20), y X-6 a X-10 no tocan ninguna capacidad Must Have. X-5 dejó de ser exclusión el 2026-08-08 por cumplirse la condición de reingreso que ella misma declaraba, y su capacidad correspondiente, F-21, pasó de Won't Have v1 a Must Have.

## 6. Supuestos

### 6.1 Supuestos declarados por el intake

El intake los declara completos y utilizables, y **pendientes de confirmación del Product Owner** (PRODUCT-INTAKE §22). Ninguno contradice a las fuentes, y ninguno condiciona qué capacidades entran al alcance: condicionan targets y umbrales de verificación.

| Id | Supuesto | Qué afecta si el Product Owner lo cambia |
|---|---|---|
| A-2 | Los targets de las **cuatro** métricas de negocio: 7 de 7 etapas; ≥ 80 % de alumnos habilitados con al menos un trabajo en estado `Pendiente` o posterior; 100 % de trabajos en estado `Pendiente` que reciben desenlace; ≥ 1 advertencia por alumno | Sólo cambian los objetivos y las métricas de `Vision-Producto.md` §5 y §6, y lo que la categoría 01 derive de ahí |
| A-3 | Las coberturas mínimas de prueba por proyecto de código | Cambia el criterio de verificación de esos proyectos de código; no cambia el alcance funcional |
| A-4 | Los criterios de verificación que no se basan en cobertura de líneas para tres de los proyectos de código | Cambia la forma del criterio, no su carácter bloqueante |
| A-5 | Los umbrales numéricos de tiempo de respuesta y de volumen de peticiones | Cambia lo que la categoría 08 verifique; no cambia el alcance funcional |

### 6.2 Incógnitas que se resuelven midiendo

El intake distingue explícitamente estas de las anteriores: **no son asunciones sino incógnitas que se resuelven midiendo, no decidiendo** (PRODUCT-INTAKE §22). Se listan acá porque el alcance de la etapa `a` incluye medirlas.

| Incógnita | Cómo se resuelve | Efecto sobre el alcance si sale mal |
|---|---|---|
| Qué versión de la plataforma soporta el lugar público gratuito | Puerta técnica PT-01.a, medida en la etapa `a` | Se baja la versión objetivo de la pieza pública, **no la del servicio de datos**: son dos artefactos independientes |
| Si el lugar público sostiene la sesión interactiva y el proceso sin reciclarlo | Puerta técnica PT-01.b y PT-01.c, medidas en la etapa `a` | Sólo el peor resultado obliga a cambiar el modelo de la pieza pública; un repliegue de transporte no es motivo de rediseño |
| Si la pieza pública alcanza al servicio de datos | Puerta técnica PT-01.d, medida en la etapa `a` | Publicar el servicio en un puerto convencional |
| Si el mecanismo de despliegue elegido funciona en el servidor propio | Prueba única antes de depender de él | Cambia el procedimiento de despliegue, no el alcance funcional |
| Qué versión exacta de un componente de interfaz se adopta | Se ancla al crear el andamiaje, en la etapa `a` | Ninguno sobre el alcance |

## 7. Restricciones

Las nueve restricciones del cliente están enumeradas en `Vision-Producto.md` §7 y no se duplican. Las tres que acotan directamente este alcance:

- **Sin fecha objetivo y etapas en serie.** El alcance comprometido se cierra etapa por etapa, con punto de control bloqueante. No se abre una etapa antes de cerrar la anterior, y sin OK explícito no se avanza.
- **Sin presupuesto monetario.** Ninguna capacidad puede apoyarse en un servicio pago.
- **Formato de entrada no negociable.** El producto se adapta al texto que el alumno ya produce; pedirle al alumno que cambie su salida está fuera de alcance por definición.

## 8. Criterios de aceptación del producto

Verificables, derivados de PRODUCT-INTAKE §4, §6, §7 y §15.

- [ ] Un alumno se registra, es habilitado por el administrador, establece su contraseña en el primer ingreso y accede a su panel, sin que intervenga ningún correo (F-02, F-03, F-04).
- [ ] Un alumno carga un trabajo con nombre, fecha, descripción y el texto de su Actividad 1, lo **envía**, y el trabajo pasa a estado `Pendiente` porque el texto verifica (F-06, F-22).
- [ ] Un envío cuyo texto no verifica deja el trabajo en estado `Borrador` con sus errores localizados, y el alumno lo reedita y lo vuelve a enviar cuantas veces haga falta (F-07, F-22).
- [ ] El texto que el programa del alumno emite realmente se interpreta sin pedirle al alumno ninguna corrección, incluidas sus particularidades de formato declaradas (F-09).
- [ ] Un cubo del primer ejemplo de la cátedra produce una advertencia de área con los dos valores expresados, declarado y derivado, y el trabajo **pasa a estado `Pendiente` igual** (F-10).
- [ ] El mismo cubo emitido por el segundo ejemplo de la cátedra **no** produce ninguna advertencia. Es el criterio negativo: una verificación que advirtiera siempre pasaría el caso anterior y fallaría éste (F-10).
- [ ] Un texto con un tipo de figura desconocido produce un error que indica **índice de figura y campo**, nunca un mensaje genérico; el trabajo queda en estado `Borrador` y no pasa a estado `Pendiente` (F-09, F-22).
- [ ] Los ortoedros que emite la aplicación del alumno **se dibujan**, cosa que hoy no ocurre con ninguno (F-11).
- [ ] El alumno y el administrador ven el mismo trabajo con la misma visualización tridimensional y el mismo árbol (F-11, F-12).
- [ ] El administrador ve los trabajos de la comisión agrupados y filtrados por alumno, sin que nadie le mande nada, y **no ve los que están en estado `Borrador`** (F-12).
- [ ] El administrador aprueba un trabajo en estado `Pendiente` y pasa a `Finalizado`; rechaza otro y pasa a `Rechazado`. En los dos casos puede dejar un comentario escrito, y en los dos casos puede no dejarlo (F-21, F-23).
- [ ] Ni el alumno ni ninguna petición forzada cambian el estado de un trabajo `Finalizado` o `Rechazado`: los dos son terminales, y corregir un rechazo significa cargar un trabajo nuevo (F-23).
- [ ] El administrador elimina cualquier trabajo que ve, en cualquier estado, y el trabajo desaparece (F-24).
- [ ] Un alumno que pide por dirección el trabajo de otro recibe «no encontrado», y la verificación ocurre del lado del servicio de datos, no ocultando un botón en la interfaz (PRODUCT-INTAKE §7, CL-5).
- [ ] Cuando el servicio de datos no responde, la pieza pública muestra un estado degradado explícito, nunca un error sin manejar (PRODUCT-INTAKE §7, CL-2 y CL-8).
- [ ] Las ocho etapas comprometidas, `a` a `h`, están cerradas con OK explícito del Product Owner en su punto de control, y los guiones de todas las etapas anteriores siguen pasando sin correcciones.

## 9. Gestión de cambios de alcance

El alcance lo fija el Product Owner y se cambia con el mismo procedimiento con el que se fijó (PRODUCT-INTAKE §4, §9, §15).

1. **Quién decide.** Toda alta, baja o cambio de prioridad de una capacidad es decisión del Product Owner. Ninguna categoría de esta documentación la origina.
2. **Dónde se registra.** El cambio se registra primero en el `PRODUCT-INTAKE` §4 o §9, que es el origen de trazabilidad; recién después se propaga a este documento y a los que dependen de él.
3. **Cuándo se aplica.** En el punto de control de una etapa, que es la única detención prevista del proceso. Una capacidad nueva no interrumpe una etapa en curso: las etapas van en serie y se cierran completas.
4. **Qué arrastra.** Un cambio de alcance obliga a revisar, como mínimo, `Roadmap-Producto.md` §2, §3 y §5, y los documentos de las categorías 01, 02 y 07 que citen la capacidad afectada.
5. **Qué no se admite.** Arrastrar una puerta técnica que no pasó como deuda: si una puerta no pasa, detiene la planificación de lo que depende de ella y el alcance se replantea en ese punto (PRODUCT-INTAKE §15).
6. **Una exclusión retirada se registra, no se borra.** Cuando se cumple la condición de reingreso que la propia exclusión declaraba, la fila se conserva en §5 marcada como retirada, con la fecha y el motivo, y la capacidad correspondiente cambia de prioridad en §4. Borrar la fila haría indistinguible una exclusión retirada de una que nunca existió. El caso ocurrido el 2026-08-08 con X-5 es el precedente.

## 10. Trazabilidad

| Contenido de este documento | Origen upstream | Destino downstream |
|---|---|---|
| §2 Descripción general | PRODUCT-INTAKE §1, §3, §10, §14 | 01-Necesidades-Negocio, 05-Arquitectura-Tecnica |
| §3 Objetivos del producto | PRODUCT-INTAKE §8 | 01-Necesidades-Negocio, 08-Calidad-Y-Pruebas |
| §4 Alcance incluido | PRODUCT-INTAKE §4, §4.2, §15, §18 | 02-Especificacion-Funcional, 07-Plan-Sprint, 10-Examples |
| §5 Alcance excluido | PRODUCT-INTAKE §9 | 02-Especificacion-Funcional (para no generar casos de uso excluidos), 07-Plan-Sprint |
| §6 Supuestos | PRODUCT-INTAKE §22 | 05-Arquitectura-Tecnica, 08-Calidad-Y-Pruebas, 09-Devops |
| §7 Restricciones | PRODUCT-INTAKE §10 | 05-Arquitectura-Tecnica, 09-Devops |
| §8 Criterios de aceptación | PRODUCT-INTAKE §4, §6, §7, §15 | 02-Especificacion-Funcional, 08-Calidad-Y-Pruebas, 10-Examples |
| §9 Gestión de cambios de alcance | PRODUCT-INTAKE §4, §9, §15 | 07-Plan-Sprint |

Vocabulario: los términos del dominio usados acá están definidos en `Vision-Producto.md` §9 y no se redefinen.

## 11. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial. Formaliza doce capacidades comprometidas y cinco declaradas con prioridad menor, seis clases de entregable, dos ambientes, las diez exclusiones del Product Owner con su justificación y su condición de reingreso, los cuatro supuestos pendientes de confirmación separados de las cinco incógnitas que se resuelven midiendo, doce criterios de aceptación verificables y el procedimiento de cambio de alcance. | Product Manager Senior (AG-00) |
| 1.0 | 2026-08-08 | Corrección absorbida del audit A-00-01-r1, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: se califican las cinco ocurrencias desnudas de «pieza» en su referente de artefacto desplegable —§2.2 (tres), §4.2 F-14 y §4.4—, sobre la familia que declara `Vision-Producto.md` §9.2, y §2.2 remite a esa entrada de glosario. | Product Manager Senior (AG-00) |
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. Sube minor y archiva el estado anterior porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§2.1**: el producto cierra con desenlace y el alumno envía en lugar de guardar. **§3**: los objetivos pasan de tres a cuatro, con OBJ-03 de revisión, y OBJ-01 declara que el alcance comprometido cubre las etapas `a` a `h`. **§4.1**: entran F-21, F-22, F-23 y F-24 como comprometidas, y se reescriben F-07, F-08 y F-12 sobre el modelo de estados nuevo. **§4.2**: el tramo comprometido pasa a `a`–`h` y los candidatos a la etapa `i`. **§5**: X-5 queda registrada como **exclusión retirada** el 2026-08-08 por cumplirse su propia condición de reingreso, con la calificación con nota o escala como lo que sigue excluido; la nota de cierre recuenta las Won't Have v1 en F-18 a F-20. **§8**: los criterios de aceptación se reescriben sobre enviar y estado `Pendiente`, y entran tres criterios del circuito de revisión. **§9**: entra la regla 6, que fija que una exclusión retirada se registra y no se borra. · **Correcciones de la ronda r3 del audit, absorbidas en esta misma versión.** **H-01**: la fila A-2 de §6.1 conservaba el enunciado retirado de la métrica de cierre —«≥ 80 % de trabajos finalizados»— y contaba tres métricas; pasa a las cuatro vigentes, con la de entrega alineada literalmente a `Vision-Producto.md` §6 y con la de aprobación del administrador y su target de 100 %. Es el punto al que remite `README.md` §5. **H-02**: el párrafo introductorio de §4.1 contaba doce capacidades comprometidas sobre una tabla de dieciséis; pasa a dieciséis y declara su origen mixto, doce de los requerimientos funcionales cerrados y cuatro de la decisión del Product Owner del 2026-08-08. | Product Manager Senior (AG-00) |
| 1.2 | 2026-08-09 | Absorbe la capacidad **F-25** incorporada por el Product Owner en `PRODUCT-INTAKE` 1.5 §4, **originada en la validación visual de la Fase B2** de la maqueta de la pieza pública, aprobada tras cuatro iteraciones. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§4.1**: el enunciado de dieciséis capacidades Must Have **no cambia y sigue siendo verdadero**, porque F-25 es `Should Have`; se agrega la nota que declara que ese recuento cubre sólo las Must Have y que entregar una capacidad de prioridad menor sobre la superficie de una etapa comprometida no la incorpora al recuento ni la vuelve bloqueante. **§4.2**: entra F-25 con su prioridad `Should Have` y su origen; la tabla suma la columna de etapa; el preámbulo deja de afirmar que todas estas capacidades caen en la etapa `i` y siguientes —que era falso para F-13 desde la emisión inicial— y pasa a distinguir prioridad de etapa, con F-13 y F-25 ubicadas en la etapa `g`; se agrega la nota de origen de F-25, que conserva las dos precisiones informativas del intake: la órbita de la cámara existe en la visualización actual y se porta, el giro de las piezas es capacidad nueva, y la capacidad entera es comodidad de lectura y no capacidad de entrega. **§8**: sin cambios, deliberadamente. Un criterio de aceptación del producto compromete, y F-25 no está comprometida. Ninguna exclusión, prioridad, target ni supuesto cambia; el punto abierto de las siete u ocho etapas del objetivo de avance sigue esperando decisión del Product Owner y no se toca acá. | Product Manager Senior (AG-00) |
