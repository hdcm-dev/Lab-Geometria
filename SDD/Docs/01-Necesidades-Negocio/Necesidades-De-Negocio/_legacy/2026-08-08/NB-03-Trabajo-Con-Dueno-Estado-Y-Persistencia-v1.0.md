> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md`](../../NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# NB-03 — Trabajo con dueño, estado y persistencia

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md |
| Versión | 1.0 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (promesa central), §4 (capacidades F-06, F-07 y F-08), §6 (flujo 2), §7 (casos límite CL-3 y CL-5), §8 (métrica de cierre del circuito didáctico), §22 (asunción A-2); `Vision-Producto.md` §1, §3, §5 y §9; `Alcance-Producto.md` §4.1 y §8 |
| Trazabilidad downstream | CU-08, CU-09, CU-10, CU-11 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

La Actividad 1 no termina hoy en una entrega. El alumno modela sus figuras, copia el texto que produce su programa y lo pega en una página suelta para mirarlo; si cierra esa página, no queda nada (PRODUCT-INTAKE §1). El trabajo no tiene identificador, no tiene fecha, no tiene descripción, no tiene estado y no tiene historial. La cátedra necesita que ese esfuerzo se convierta en una unidad con existencia propia: un trabajo que se guarda, que le pertenece a un alumno y que atraviesa estados hasta quedar entregado.

El punto delicado es que el alumno trabaja iterando. Su programa todavía está a medio hacer, su texto está incompleto o roto, y aun así no puede perder lo hecho mientras corrige. Por eso el trabajo tiene que poder guardarse como borrador **incluso cuando el texto no se puede interpretar**, y volver a abrirse tal cual quedó (PRODUCT-INTAKE §4, F-07, y §7, CL-3). El borrador no es una concesión: es el estado en el que el alumno pasa la mayor parte del tiempo.

La necesidad incluye además que cada alumno vea lo suyo y sólo lo suyo. Un trabajo tiene dueño, y pedir el trabajo de otro no debe devolver información sobre su existencia (PRODUCT-INTAKE §7, CL-5). Y la eliminación tiene que estar acotada al estado en el que todavía no hubo entrega, para que un trabajo ya presentado no desaparezca (PRODUCT-INTAKE §4, F-07).

## 2. Ejemplo de uso desde la perspectiva del negocio

Un alumno abre el laboratorio y crea un trabajo nuevo: le pone nombre, fecha y una descripción de qué modeló, y pega el texto que le devolvió su programa. El texto todavía está a medio terminar, así que lo guarda como borrador. El trabajo aparece en su lista con su identificador y su estado. Dos días después vuelve, lo abre tal cual lo dejó, reemplaza el texto por la salida corregida y vuelve a guardar. Cuando está conforme, lo entrega: el trabajo cambia de estado y queda como entregado. En la lista ve, de un vistazo, cuáles tiene todavía en borrador, cuál entregó y cuál quedó finalizado. Un compañero que probó a cambiar la dirección para ver ese trabajo recibió «no encontrado».

## 3. Impacto

- Si se resuelve: el esfuerzo de la Actividad 1 se convierte en una entrega verificable, con dueño, fecha e identificador.
- Si se resuelve: el alumno deja de perder lo hecho al cerrar la página y puede iterar sobre su propio material.
- Si se resuelve: aparece el estado, que es lo que permite después medir cuántos alumnos llegaron efectivamente a entregar.
- Si se resuelve: existe la unidad sobre la que después se aplican la interpretación del dato, la visualización y la revisión del docente.
- Si queda sin resolver: no hay nada que validar, nada que visualizar dentro del producto y nada que revisar, porque las tres cosas se aplican sobre un trabajo guardado.
- Si queda sin resolver: la métrica de cierre del circuito didáctico no se puede medir, porque no existe el estado que expresa la entrega.

## 4. Problema específico que resuelve

- El trabajo del alumno se pierde al cerrar la página.
- No hay forma de identificar un trabajo ni de distinguirlo de otro del mismo alumno.
- Un texto incompleto o roto no se puede conservar mientras el alumno corrige su programa.
- No hay noción de avance: no se distingue lo que está en curso de lo que fue entregado.
- Un alumno podría ver el trabajo de otro si la separación se resolviera sólo ocultando un botón en la pantalla.
- Un trabajo ya entregado podría eliminarse por accidente si la eliminación no estuviera acotada.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Trabajo con existencia propia | Trabajos cargados que quedan guardados con dueño, identificador propio, fecha y estado, sobre los trabajos cargados | 100 % | Punto de control de la etapa `e` |
| Conservación del borrador inválido | Borradores guardados con el texto incompleto o roto que se recuperan y se reeditan tal como quedaron, sobre los guardados en el guion de demostración | 100 % | Punto de control de la etapa `e` |
| Visibilidad del avance | Estados distinguibles en el listado propio, sobre los 3 declarados: `Borrador`, `Pendiente` y `Finalizado` | 3 de 3 | Punto de control de la etapa `e` |
| Separación entre alumnos y acotación de la eliminación | Eliminaciones que proceden fuera del estado `Borrador` o sobre trabajos de otro alumno, forzando la petición y no sólo desde la pantalla | 0 | Punto de control de la etapa `e` |
| Cierre del circuito de entrega | Trabajos que llegan a estado `Finalizado`, sobre el total de alumnos registrados y habilitados | ≥ 80 % | Al cierre de la primera cursada en que se use el producto |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §4 (F-06) y §12 (definición de trabajo); el segundo, de PRODUCT-INTAKE §4 (F-07) y §7 (CL-3); el tercero, de PRODUCT-INTAKE §4 (F-08); el cuarto, de PRODUCT-INTAKE §7 (CL-5) y de la transición `e` a `f` de `Roadmap-Producto.md` §5.2; el quinto, de PRODUCT-INTAKE §8. **El target del quinto criterio está rotulado como asunción A-2 en PRODUCT-INTAKE §22 y está pendiente de confirmación del Product Owner**; se usa como valor vigente hasta que la confirmación llegue.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Fijó que el trabajo es lo que el alumno entrega en el laboratorio y que el borrador acepta texto inválido; confirma el target de la métrica de cierre del circuito |
| Cátedra de Programación 2, como dueño del problema | Propietario | Padece que la Actividad 1 termine hoy en un portapapeles y no en una entrega |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye la carga, el borrador, el listado y la eliminación acotada, y los demuestra en el punto de control de la etapa `e` |
| Alumno de la comisión | Beneficiario | Deja de perder lo hecho, itera sobre su propio material y ve el avance de cada uno de sus trabajos |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Obtiene la unidad sobre la que después revisa la entrega de toda la comisión |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-03 | CU-08 cargar un trabajo con nombre, fecha, descripción y texto de figuras | a generar |
| NB-03 | CU-09 guardar y reeditar un trabajo como borrador | a generar |
| NB-03 | CU-10 eliminar un trabajo en estado `Borrador` | a generar |
| NB-03 | CU-11 listar los trabajos propios con su estado | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-02, porque un trabajo sin dueño no es un trabajo, y el dueño existe recién cuando el alumno tiene identidad propia.
- Es prerequisito de: NB-04 (la interpretación se aplica sobre un trabajo cargado), NB-06 (la visualización dibuja un trabajo guardado) y NB-07 (la revisión recorre trabajos existentes).

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: las tres capacidades que esta NB agrupa —F-06, F-07 y F-08— están declaradas Must Have, todas con la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de que el trabajo del alumno tenga dueño, estado y persistencia a partir de las capacidades F-06, F-07 y F-08 del intake, con cinco criterios de éxito trazados a su sección de origen —uno de ellos con target rotulado como asunción A-2— y cuatro casos de uso previstos. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgo **H-02**: §6 dejó de nombrar al trabajo con el término normativo «unidad de entrega», que `Vocabulario-Rules.md` §2 reserva a lo que se despliega de forma independiente, y adopta el enunciado del glosario raíz actualizado (`Vision-Producto.md` §9.1): «es lo que el alumno entrega en el laboratorio». El sentido de la frase no cambia. La corrección se absorbe **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
