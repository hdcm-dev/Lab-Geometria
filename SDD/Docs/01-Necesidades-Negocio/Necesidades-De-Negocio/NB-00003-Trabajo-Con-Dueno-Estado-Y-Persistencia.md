# NB-00003 — Trabajo con dueño, estado y persistencia

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md |
| Versión | 1.1 |
| Estado | Aprobado |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (promesa central), §4 (capacidades F-06, F-07 y F-08), §4.1 (reglas RN-02003, RN-02004 y RN-02005), §4.2 (modelo de estados del trabajo), §6 (flujo 2), §7 (casos límite CL-3 y CL-5), §8 (métrica de entrega del alumno), §12 (glosario del dominio), §22 (asunción A-2); `Vision-Producto.md` §1, §3, §5 y §9; `Alcance-Producto.md` §4.1 y §8 |
| Trazabilidad downstream | `CU-00006`, `CU-00007`, `CU-00011`, `CU-02005`, `CU-02008`, `CU-02009`, `CU-02010`, `CU-04004`, `CU-04006`, `CU-04009`, `CU-06001`, `CU-06003`, `CU-06004`, `CU-06010` en `GeometriaFactory-Api`; `CU-10005`, `CU-10006` en `GeometriaFactory-Web` (emitidos en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

El punto delicado es que el alumno trabaja iterando. Su programa todavía está a medio hacer, su texto está incompleto o roto, y aun así no puede perder lo hecho mientras corrige. El trabajo cuyo texto no verifica **queda conservado en estado `Borrador`, con su texto tal como el alumno lo pegó**, y vuelve a abrirse tal cual quedó (PRODUCT-INTAKE §4, F-07, y §7, CL-3). Ese estado no es una concesión: es donde el alumno pasa la mayor parte del tiempo, y es el único que él edita y elimina.

El recorrido del trabajo termina fuera de las manos del alumno. El conjunto de estados es cerrado y tiene cuatro valores —`Borrador`, `Pendiente`, `Finalizado` y `Rechazado`—, y los dos últimos son terminales y los decide el administrador (PRODUCT-INTAKE §4.2). Lo que esta necesidad cubre es que el trabajo **tenga** estado, que el alumno lo vea y que el recorrido sea el declarado; el desenlace en sí es materia de otra necesidad. La consecuencia para el alumno es concreta: sólo puede volver sobre lo que está en `Borrador`, y un trabajo ya enviado deja de estar bajo su control.

La necesidad incluye además que cada alumno vea lo suyo y sólo lo suyo. Un trabajo tiene dueño, y pedir el trabajo de otro no debe devolver información sobre su existencia (PRODUCT-INTAKE §7, CL-5). Y la eliminación por parte del alumno tiene que estar acotada al estado `Borrador`, para que un trabajo ya enviado no desaparezca por su mano (PRODUCT-INTAKE §4.1, RN-02004).

## 2. Ejemplo de uso desde la perspectiva del negocio

Un alumno abre el laboratorio y crea un trabajo nuevo: le pone nombre, fecha y una descripción de qué modeló, y pega el texto que le devolvió su programa. El texto todavía está a medio terminar, así que el trabajo queda en su lista en estado `Borrador`, con su identificador y con el texto tal como lo pegó. Dos días después vuelve, lo abre tal cual lo dejó y reemplaza el texto por la salida corregida; esta vez el trabajo queda en estado `Pendiente`, esperando la revisión del docente, y él ya no lo puede tocar. En su lista ve, de un vistazo, cuáles siguen en `Borrador` y cuáles entregó, y más adelante cuáles quedaron `Finalizado` o `Rechazado`. Un compañero que probó a cambiar la dirección para ver ese trabajo recibió «no encontrado».

## 3. Impacto

- Si se resuelve: el esfuerzo de la Actividad 1 se convierte en una entrega verificable, con dueño, fecha e identificador.
- Si se resuelve: el alumno deja de perder lo hecho al cerrar la página y puede iterar sobre su propio material.
- Si se resuelve: aparece el estado, que es lo que permite después medir cuántos alumnos llegaron efectivamente a entregar.
- Si se resuelve: existe la unidad sobre la que después se aplican la interpretación del dato, la visualización, la revisión del docente y su desenlace.
- Si se resuelve: el alumno sabe en todo momento qué trabajos siguen bajo su control y cuáles ya salieron de sus manos.
- Si queda sin resolver: no hay nada que validar, nada que visualizar dentro del producto y nada que revisar, porque las cuatro cosas se aplican sobre un trabajo guardado.
- Si queda sin resolver: la métrica de entrega del alumno no se puede medir, porque no existe el estado que expresa la entrega efectiva.

## 4. Problema específico que resuelve

- El trabajo del alumno se pierde al cerrar la página.
- No hay forma de identificar un trabajo ni de distinguirlo de otro del mismo alumno.
- Un texto incompleto o roto no se puede conservar mientras el alumno corrige su programa.
- No hay noción de avance: no se distingue lo que está en curso, lo que fue entregado y lo que ya tuvo desenlace.
- Un alumno podría ver el trabajo de otro si la separación se resolviera sólo ocultando un botón en la pantalla.
- Un trabajo ya enviado podría eliminarse o reescribirse por accidente si el alumno conservara el control sobre él.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Trabajo con existencia propia | Trabajos cargados que quedan guardados con dueño, identificador propio, fecha y estado, sobre los trabajos cargados | 100 % | Punto de control de la etapa `e` |
| Conservación del trabajo que no verifica | Trabajos cuyo texto no verifica que quedan en estado `Borrador` con su texto conservado y se reeditan tal como quedaron, sobre los del guion de demostración | 100 % | Punto de control de la etapa `e` |
| Visibilidad del avance | Estados distinguibles en el listado propio, sobre los 4 del conjunto cerrado: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado` | 4 de 4 | Punto de control de la etapa `e` |
| Separación entre alumnos y acotación de la eliminación | Eliminaciones ejecutadas por el alumno que proceden fuera del estado `Borrador` o sobre trabajos de otro alumno, forzando la petición y no sólo desde la pantalla | 0 | Punto de control de la etapa `e` |
| Entrega del alumno | Alumnos habilitados que llegan a tener al menos un trabajo en estado `Pendiente` o posterior, sobre el total de alumnos habilitados | ≥ 80 % | Al cierre de la primera cursada en que se use el producto |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §4 (F-06) y §12 (definición de trabajo); el segundo, de PRODUCT-INTAKE §4 (F-07), §4.2 (significado de `Borrador`) y §7 (CL-3); el tercero, de PRODUCT-INTAKE §4 (F-08) y §4.2 (conjunto cerrado de estados); el cuarto, de PRODUCT-INTAKE §7 (CL-5), §4.1 (RN-02004) y de la transición `e` a `f` de `Roadmap-Producto.md` §5.2; el quinto, de PRODUCT-INTAKE §8 y de `Vision-Producto.md` §5 (OBJ-02). **El target del quinto criterio está rotulado como asunción A-2 en PRODUCT-INTAKE §22 y está pendiente de confirmación del Product Owner**; se usa como valor vigente hasta que la confirmación llegue.

El quinto criterio corta en estado `Pendiente` y no en `Finalizado`, y el motivo es de atribución: con el modelo de estados vigente, `Finalizado` significa «el administrador lo aprobó», de modo que medirlo acá mediría el trabajo del docente y no la entrega del alumno. Lo que depende del alumno termina en el envío, y `Pendiente` es el primer estado que expresa una entrega efectiva porque exige texto interpretado sin errores (PRODUCT-INTAKE §4.1, RN-02005). La mitad que mide al administrador vive en NB-00009.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Fijó que el trabajo es lo que el alumno entrega en el laboratorio, el conjunto cerrado de sus cuatro estados y que el trabajo cuyo texto no verifica se conserva igual; confirma el target de la métrica de entrega del alumno |
| Cátedra de Programación 2, como dueño del problema | Propietario | Padece que la Actividad 1 termine hoy en un portapapeles y no en una entrega |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye la carga, el borrador, el listado y la eliminación acotada, y los demuestra en el punto de control de la etapa `e` |
| Alumno de la comisión | Beneficiario | Deja de perder lo hecho, itera sobre su propio material y ve el avance de cada uno de sus trabajos |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Obtiene la unidad sobre la que después revisa la entrega de toda la comisión |

## 7. Trazabilidad a CU

| NB | Casos de uso emitidos | Estado |
| --- | --- | --- |
| NB-00003 | `CU-00006`, `CU-00007`, `CU-00011`, `CU-02005`, `CU-02008`, `CU-02009`, `CU-02010`, `CU-04004`, `CU-04006`, `CU-04009`, `CU-06001`, `CU-06003`, `CU-06004`, `CU-06010` en `GeometriaFactory-Api`; `CU-10005`, `CU-10006` en `GeometriaFactory-Web` cargar un trabajo con nombre, fecha, descripción y texto de figuras | Emitidos |
| NB-00003 | `CU-00006`, `CU-00007`, `CU-00011`, `CU-02005`, `CU-02008`, `CU-02009`, `CU-02010`, `CU-04004`, `CU-04006`, `CU-04009`, `CU-06001`, `CU-06003`, `CU-06004`, `CU-06010` en `GeometriaFactory-Api`; `CU-10005`, `CU-10006` en `GeometriaFactory-Web` reeditar un trabajo en estado `Borrador` | Emitidos |
| NB-00003 | `CU-00006`, `CU-00007`, `CU-00011`, `CU-02005`, `CU-02008`, `CU-02009`, `CU-02010`, `CU-04004`, `CU-04006`, `CU-04009`, `CU-06001`, `CU-06003`, `CU-06004`, `CU-06010` en `GeometriaFactory-Api`; `CU-10005`, `CU-10006` en `GeometriaFactory-Web` eliminar un trabajo propio en estado `Borrador` | Emitidos |
| NB-00003 | `CU-00006`, `CU-00007`, `CU-00011`, `CU-02005`, `CU-02008`, `CU-02009`, `CU-02010`, `CU-04004`, `CU-04006`, `CU-04009`, `CU-06001`, `CU-06003`, `CU-06004`, `CU-06010` en `GeometriaFactory-Api`; `CU-10005`, `CU-10006` en `GeometriaFactory-Web` listar los trabajos propios con su estado | Emitidos |

## 8. Dependencias con otras NB

- Depende de: NB-00002, porque un trabajo sin dueño no es un trabajo, y el dueño existe recién cuando el alumno tiene identidad propia.
- Es prerequisito de: NB-00004 (la interpretación se aplica sobre un trabajo cargado), NB-00006 (la visualización dibuja un trabajo guardado) y NB-00007 (la revisión recorre trabajos existentes).

La acción de envío que decide entre `Borrador` y `Pendiente` no pertenece a esta necesidad: vive en NB-00004, porque lo que decide el estado es el resultado de interpretar el texto. El desenlace de un trabajo en estado `Pendiente` tampoco: vive en NB-00009.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: las tres capacidades que esta NB agrupa —F-06, F-07 y F-08— están declaradas Must Have, todas con la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de que el trabajo del alumno tenga dueño, estado y persistencia a partir de las capacidades F-06, F-07 y F-08 del intake, con cinco criterios de éxito trazados a su sección de origen —uno de ellos con target rotulado como asunción A-2— y cuatro casos de uso previstos. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgo **H-02**: §6 dejó de nombrar al trabajo con el término normativo «unidad de entrega», que `Vocabulario-Rules.md` §2 reserva a lo que se despliega de forma independiente, y adopta el enunciado del glosario raíz (`Vision-Producto.md` §9.1): «es lo que el alumno entrega en el laboratorio». Absorbida sin subir versión por `Master-Prompt.md` §5, con el documento todavía sin ser citado como insumo. |
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). **§1** incorpora el conjunto cerrado de cuatro estados y el significado vigente de `Borrador` —«el texto no verificó»—, y declara que el desenlace pertenece a otra necesidad. **§2** reescribe el ejemplo sobre el envío como única acción de guardado. **§3** y **§4** ajustan las viñetas al recorrido nuevo. **§5**: el segundo criterio se reescribe sobre el trabajo que no verifica; el tercero pasa de 3 a **4 de 4** estados; el cuarto acota la eliminación a la que ejecuta el alumno (RN-02004); el quinto pasa de «trabajos que llegan a `Finalizado` sobre alumnos habilitados» a la **métrica de entrega del alumno** —alumnos habilitados con al menos un trabajo en estado `Pendiente` o posterior, ≥ 80 %— por la partición de la métrica declarada en `PRODUCT-INTAKE` §8 y `Vision-Producto.md` §5, con su fundamento de atribución explicitado. **§7** renombra CU-09 y CU-10. **§8** declara dónde viven el envío y el desenlace. La prioridad, las dependencias y el resto de los targets no cambian. |
