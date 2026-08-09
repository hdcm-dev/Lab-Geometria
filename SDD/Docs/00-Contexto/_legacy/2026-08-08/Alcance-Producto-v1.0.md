> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Alcance-Producto.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`Alcance-Producto.md`](../../Alcance-Producto.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Alcance del Producto

**Producto:** Fábrica de Geometría
**Documento:** Alcance-Producto.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE §1 (problema), §3 (propuesta de valor), §4 (alcance funcional pretendido con MoSCoW), §5 (historias de usuario), §6 (flujos típicos), §7 (casos límite), §9 (exclusiones), §10 (restricciones del cliente), §12 (glosario), §15 (esquema de descomposición y delivery), §22 (supuestos declarados)
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

Dos personas lo usan: el **alumno**, que se registra, carga, previsualiza y entrega; y el **administrador**, que es el docente, que habilita cuentas y revisa entregas.

### 2.2 Cómo se entrega

El producto se entrega en **dos piezas desplegables** —la **pieza pública** y la **pieza de datos**, en los términos de `Vision-Producto.md` §9.2—, y esa partición no es una preferencia sino la respuesta a dos restricciones de negocio que no se pueden satisfacer con una sola pieza desplegable (PRODUCT-INTAKE §10 y §14):

- La red desde la que trabajan los alumnos **bloquea el acceso al servidor propio del docente**.
- El lugar público donde sí se puede llegar **no conserva los datos**.

De ahí la partición: la pieza pública vive donde no la bloquean, y la pieza de datos vive donde los datos persisten. La persona sólo toca la pieza pública; la pieza de datos no es alcanzable desde el navegador. El detalle técnico de esta separación pertenece a la categoría 05 y no se decide acá.

## 3. Objetivos del producto

Los objetivos, con su métrica, target y plazo, están en `Vision-Producto.md` §5 y no se duplican. En términos de alcance:

| Id | Objetivo | Qué acota del alcance |
|---|---|---|
| OBJ-01 | Cerrar las 7 etapas comprometidas con OK explícito en cada punto de control | Fija que el alcance comprometido es el que cubren las etapas `a` a `g`, y nada más |
| OBJ-02 | Que al menos el 80 % de los alumnos habilitados llegue a un trabajo `Finalizado` | Obliga a que el circuito completo de registro, carga, validación y finalización entre en el alcance comprometido |
| OBJ-03 | Que el producto muestre al menos una advertencia de valor declarado contra derivado por alumno con figuras afectadas | Obliga a que la verificación de valores calculados sea capacidad comprometida y no diferida |

## 4. Alcance incluido

### 4.1 Capacidades comprometidas

Doce capacidades con prioridad **Must Have** declarada en PRODUCT-INTAKE §4. Son la traducción directa de los requerimientos funcionales que la fuente declara cerrados.

| Id | Capacidad incluida | Etapa que la entrega |
|---|---|---|
| F-01 | Configurar la cuenta de administrador en el primer arranque, y sólo mientras no exista ninguna | `c` |
| F-02 | Registro de alumno con correo, nombre y apellido, sin elegir contraseña | `d` |
| F-03 | Habilitar, bloquear, rehabilitar y dar de baja física cuentas de alumno desde el panel del administrador | `d` |
| F-04 | Establecer contraseña en el primer ingreso efectivo del alumno, sin envío de correo | `d` |
| F-05 | Inicio y cierre de sesión, y cambio de contraseña exigiendo la actual | `c` |
| F-06 | Cargar un trabajo con nombre, fecha, descripción y el texto de figuras, con identificador propio | `e` |
| F-07 | Guardar y reeditar como borrador, incluso con texto inválido, y eliminar sólo en estado `Borrador` | `e` |
| F-08 | Listar los trabajos propios con su estado `Borrador`, `Pendiente` o `Finalizado` | `e` |
| F-09 | Validar el texto con la tolerancia de claves del emisor real y reportar errores con índice de figura y campo | `f` |
| F-10 | Verificar área y volumen recalculándolos desde las dimensiones y emitir advertencias que no bloquean | `f` |
| F-11 | Previsualizar el trabajo en tres dimensiones y ver la estructura del texto como árbol colapsable | `g` |
| F-12 | Listado de todos los trabajos para el administrador, con agrupación y filtro por alumno | `e` |

La correspondencia entre capacidad y etapa se lee de PRODUCT-INTAKE §15 y se detalla en `Roadmap-Producto.md` §3.

### 4.2 Capacidades declaradas con prioridad menor

Están en el alcance declarado del producto, con prioridad explícita, y **no están comprometidas para el tramo `a` a `g`**. El intake las ubica en la etapa `h` y siguientes, que se planifican con la plantilla completa cuando `g` esté cerrada y demostrada (PRODUCT-INTAKE §4 y §15).

| Id | Capacidad | Prioridad declarada | Origen |
|---|---|---|---|
| F-13 | Sincronización entre el árbol y la escena por índice de pieza, y disposición determinista entre procesados | Should Have | PRODUCT-INTAKE §4 |
| F-14 | Despliegue real de las dos piezas desplegables, con la verificación de acceso medida desde la red de la facultad | Should Have | PRODUCT-INTAKE §4 |
| F-15 | Panel de resumen del administrador: cantidad de trabajos por alumno y por estado | Could Have | PRODUCT-INTAKE §4 |
| F-16 | Exportar el trabajo: el texto original y una captura de la escena | Could Have | PRODUCT-INTAKE §4 |
| F-17 | Modo despiece: expandir un volumen y ver sus caras separadas | Could Have | PRODUCT-INTAKE §4 |

F-16 y F-17 aparecen además en la exclusión X-6 de §5, y no hay contradicción: la exclusión declara que no entran en el alcance comprometido y que son candidatas de la etapa `h`, que es exactamente la prioridad Could Have que el intake les asigna.

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
| X-5 · Calificación o devolución escrita del administrador sobre el trabajo | No fue pedido | Etapa `h` o posterior, si el docente lo pide |
| X-6 · Modo despiece, exportación de imágenes y dirección compartible de la visualización | Propuestas del análisis previo que no entraron en el alcance comprometido | Candidatos declarados de la etapa `h`, con la prioridad Could Have de F-16 y F-17 |
| X-7 · Ambientación a un problema real, del tipo depósito, costos o capacidades | Propuesta del análisis previo; no forma parte de este producto | Candidato de la etapa `h` |
| X-8 · Segundo factor de autenticación | No fue pedido; la forma de autenticación elegida no lo bloquea | No previsto |
| X-9 · Reenvío de peticiones desde la pieza pública hacia el servicio de datos | Hoy nada del navegador toca el servicio de datos, de modo que ese reenvío sólo consumiría el recurso más escaso del plan gratuito. Queda **especificado** para adoptarlo sin rediseño | Si aparece descarga de archivos, carga directa desde el navegador o un cambio del modelo de la pieza pública |
| X-10 · Canal saliente con dominio propio para el servicio de datos | Resolvería tres riesgos de una vez, pero exige un dominio propio y **debilita la premisa de la partición**: si el servicio de datos queda alcanzable desde la facultad, la pieza pública deja de ser necesaria | Reevaluar si aparece un dominio propio o si la medición temprana obliga a mover la pieza pública |

Ninguna exclusión contradice una capacidad comprometida: X-1, X-2, X-3, X-4 y X-5 se corresponden con las capacidades declaradas Won't Have v1 (F-18 a F-21), y X-6 a X-10 no tocan ninguna capacidad Must Have.

## 6. Supuestos

### 6.1 Supuestos declarados por el intake

El intake los declara completos y utilizables, y **pendientes de confirmación del Product Owner** (PRODUCT-INTAKE §22). Ninguno contradice a las fuentes, y ninguno condiciona qué capacidades entran al alcance: condicionan targets y umbrales de verificación.

| Id | Supuesto | Qué afecta si el Product Owner lo cambia |
|---|---|---|
| A-2 | Los targets de las tres métricas de negocio: 7 de 7 etapas, ≥ 80 % de trabajos finalizados, ≥ 1 advertencia por alumno | Sólo cambian los objetivos y las métricas de `Vision-Producto.md` §5 y §6, y lo que la categoría 01 derive de ahí |
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
- [ ] Un alumno carga un trabajo con nombre, fecha, descripción y el texto de su Actividad 1, lo guarda como borrador con el texto incompleto o roto, lo reedita y lo finaliza (F-06, F-07, F-08).
- [ ] El texto que el programa del alumno emite realmente se interpreta sin pedirle al alumno ninguna corrección, incluidas sus particularidades de formato declaradas (F-09).
- [ ] Un cubo del primer ejemplo de la cátedra produce una advertencia de área con los dos valores expresados, declarado y derivado, y el trabajo **se finaliza igual** (F-10).
- [ ] El mismo cubo emitido por el segundo ejemplo de la cátedra **no** produce ninguna advertencia. Es el criterio negativo: una verificación que advirtiera siempre pasaría el caso anterior y fallaría éste (F-10).
- [ ] Un texto con un tipo de figura desconocido produce un error que indica **índice de figura y campo**, nunca un mensaje genérico; el trabajo se guarda como borrador y no se puede finalizar (F-09).
- [ ] Los ortoedros que emite la aplicación del alumno **se dibujan**, cosa que hoy no ocurre con ninguno (F-11).
- [ ] El alumno y el administrador ven el mismo trabajo con la misma visualización tridimensional y el mismo árbol (F-11, F-12).
- [ ] El administrador ve todos los trabajos de la comisión, agrupados y filtrados por alumno, sin que nadie le mande nada (F-12).
- [ ] Un alumno que pide por dirección el trabajo de otro recibe «no encontrado», y la verificación ocurre del lado del servicio de datos, no ocultando un botón en la interfaz (PRODUCT-INTAKE §7, CL-5).
- [ ] Cuando el servicio de datos no responde, la pieza pública muestra un estado degradado explícito, nunca un error sin manejar (PRODUCT-INTAKE §7, CL-2 y CL-8).
- [ ] Las siete etapas comprometidas están cerradas con OK explícito del Product Owner en su punto de control, y los guiones de todas las etapas anteriores siguen pasando sin correcciones.

## 9. Gestión de cambios de alcance

El alcance lo fija el Product Owner y se cambia con el mismo procedimiento con el que se fijó (PRODUCT-INTAKE §4, §9, §15).

1. **Quién decide.** Toda alta, baja o cambio de prioridad de una capacidad es decisión del Product Owner. Ninguna categoría de esta documentación la origina.
2. **Dónde se registra.** El cambio se registra primero en el `PRODUCT-INTAKE` §4 o §9, que es el origen de trazabilidad; recién después se propaga a este documento y a los que dependen de él.
3. **Cuándo se aplica.** En el punto de control de una etapa, que es la única detención prevista del proceso. Una capacidad nueva no interrumpe una etapa en curso: las etapas van en serie y se cierran completas.
4. **Qué arrastra.** Un cambio de alcance obliga a revisar, como mínimo, `Roadmap-Producto.md` §2, §3 y §5, y los documentos de las categorías 01, 02 y 07 que citen la capacidad afectada.
5. **Qué no se admite.** Arrastrar una puerta técnica que no pasó como deuda: si una puerta no pasa, detiene la planificación de lo que depende de ella y el alcance se replantea en ese punto (PRODUCT-INTAKE §15).

## 10. Trazabilidad

| Contenido de este documento | Origen upstream | Destino downstream |
|---|---|---|
| §2 Descripción general | PRODUCT-INTAKE §1, §3, §10, §14 | 01-Necesidades-Negocio, 05-Arquitectura-Tecnica |
| §3 Objetivos del producto | PRODUCT-INTAKE §8 | 01-Necesidades-Negocio, 08-Calidad-Y-Pruebas |
| §4 Alcance incluido | PRODUCT-INTAKE §4, §15, §18 | 02-Especificacion-Funcional, 07-Plan-Sprint, 10-Examples |
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
