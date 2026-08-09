> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md`](../../NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# NB-07 — Revisión de la comisión desde un solo lugar

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md |
| Versión | 1.0 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (diferenciador D-5), §4 (capacidades F-12 y F-15), §6 (flujo 3), §8 (métrica de cierre del circuito didáctico), §9 (exclusión X-5); `Vision-Producto.md` §1, §3 y §6; `Alcance-Producto.md` §4.1, §4.2, §5 y §8 |
| Trazabilidad downstream | CU-18, CU-19, CU-20 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

El docente no puede revisar hoy las entregas de su comisión. La única forma disponible es mirar la pantalla del alumno mientras el alumno le muestra lo que hizo (PRODUCT-INTAKE §1): no hay lugar donde estén los trabajos de todos, no hay forma de saber quién entregó y quién no, y no hay manera de volver sobre una entrega después de la clase. La revisión, que es una tarea que el docente hace por comisión entera, está hoy fragmentada en tantas sesiones individuales como alumnos haya.

La necesidad es que exista un único lugar donde el docente vea todos los trabajos, los agrupe por alumno y filtre por el alumno que quiere mirar, sin pedirle a nadie que le mande nada (PRODUCT-INTAKE §3, diferenciador D-5). Y que al abrir un trabajo vea exactamente lo mismo que vio quien lo entregó: los datos, el texto, la escena y el árbol, con sus advertencias (PRODUCT-INTAKE §6, flujo 3). Que la vista sea la misma no es un ahorro de esfuerzo de construcción: es lo que garantiza que docente y alumno estén discutiendo sobre lo mismo.

La necesidad se detiene deliberadamente antes de la calificación. El producto no incorpora nota ni devolución escrita del docente sobre el trabajo, porque no fue pedido (PRODUCT-INTAKE §9, exclusión X-5): lo que se necesita es ver la entrega de la comisión, no evaluarla dentro del laboratorio. Sobre esa misma base se apoya después el recuento por alumno y por estado, declarado con prioridad menor.

## 2. Ejemplo de uso desde la perspectiva del negocio

Termina la semana de entrega. El docente entra al laboratorio, abre el listado y agrupa por alumno: de un vistazo ve quiénes cargaron trabajos y quiénes no. Filtra por una alumna, abre su trabajo y ve los mismos datos, el mismo texto, la misma escena y el mismo árbol que ella vio, con las dos advertencias de valor que el producto le señaló. Cierra y pasa al siguiente. En una sola sesión recorre toda la comisión, sin haber pedido un solo archivo por ningún canal y sin haber mirado la pantalla de nadie.

## 3. Impacto

- Si se resuelve: la revisión de la comisión pasa de ser una tarea alumno por alumno a ser una sola sesión de trabajo del docente.
- Si se resuelve: el docente puede responder quién entregó y quién no, que es hoy una pregunta sin fuente de dato.
- Si se resuelve: docente y alumno miran exactamente la misma vista del mismo trabajo, y la discusión deja de depender de descripciones.
- Si se resuelve: queda la base de dato sobre la que se mide el cierre del circuito didáctico de la cursada.
- Si queda sin resolver: el trabajo puede quedar guardado y con dueño, pero la cátedra sigue sin poder revisarlo, que es la mitad del problema declarado.
- Si queda sin resolver: no hay forma de calcular ninguna métrica de entrega, porque falta la vista agregada de la comisión.

## 4. Problema específico que resuelve

- El docente sólo puede revisar mirando la pantalla del alumno, en el momento en que el alumno está presente.
- No existe una vista de todos los trabajos de la comisión.
- No hay forma de agrupar ni de filtrar por alumno para revisar de a uno de manera ordenada.
- El docente no puede volver sobre una entrega después de la clase.
- Lo que ve el docente podría no coincidir con lo que vio el alumno si la vista fuera otra.
- No hay recuento de cuántos trabajos cargó cada alumno ni en qué estado están.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Alcance de la vista del administrador | Alumnos de la comisión cuyos trabajos el administrador ve desde el listado, sobre el total de alumnos habilitados con trabajos cargados | 100 % | Punto de control de la etapa `e` |
| Autonomía de la revisión | Envíos, pedidos o archivos que el administrador necesita solicitar fuera del producto para revisar la comisión | 0 | Punto de control de la etapa `e` |
| Organización del listado | Criterios de organización disponibles sobre el listado, sobre los 2 declarados: agrupación por alumno y filtro por alumno | 2 de 2 | Punto de control de la etapa `e` |
| Coincidencia de la vista entre los dos papeles | Elementos del trabajo que el administrador ve al abrirlo, sobre los 4 que ve el alumno: datos, texto, escena y árbol | 4 de 4 | Punto de control de la etapa `g` |
| Concentración de la revisión | Sesiones de trabajo del docente necesarias para recorrer la entrega de toda la comisión | 1 | Primera entrega de la cursada |
| Recuento de la entrega | Recuentos disponibles en el panel de resumen, sobre los 2 declarados: por alumno y por estado | 2 de 2 | Punto de control de la etapa `h` |

Origen de cada criterio: el primero, el segundo y el tercero derivan de PRODUCT-INTAKE §4 (F-12) y §3 (diferenciador D-5); el cuarto, de PRODUCT-INTAKE §6 (flujo 3) y de la transición `g` a `h` de `Roadmap-Producto.md` §5.2; el quinto, de PRODUCT-INTAKE §3 (diferenciador D-5); el sexto, de PRODUCT-INTAKE §4 (F-15), capacidad de prioridad menor cuyo plazo es la etapa `h`. Ninguno depende de la asunción A-2 del intake.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Comprometió el listado del administrador dentro del alcance y dejó la calificación fuera; valida el punto de control de la etapa `e` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Padece hoy no poder revisar las entregas de la comisión salvo mirando la pantalla del alumno |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye el listado agrupado y filtrado y demuestra que el administrador ve lo mismo que vio el alumno |
| El mismo docente, en su papel de administrador del laboratorio, con la cuenta única de administrador | Beneficiario y operador | Recorre la entrega de toda la comisión en una sola sesión, sin pedirle nada a nadie |
| Alumno de la comisión | Beneficiario | Sabe que lo que entregó es exactamente lo que el docente va a ver, sin intermediarios ni descripciones |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-07 | CU-18 listar los trabajos de la comisión agrupados y filtrados por alumno | a generar |
| NB-07 | CU-19 abrir un trabajo de un alumno para revisarlo | a generar |
| NB-07 | CU-20 consultar el panel de resumen por alumno y por estado | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-01 (el administrador es quien revisa y existe recién cuando esa NB está resuelta), NB-03 (la revisión recorre trabajos guardados con dueño y estado) y NB-06 (la revisión visual reutiliza la misma vista que ve el alumno).
- Es prerequisito de: ninguna otra NB. Es una hoja de la cadena de dependencias.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: la NB agrupa F-12, declarada Must Have, y F-15, declarada Could Have, de modo que la prioridad de la NB es la de su capacidad más alta, y así queda declarado. El recorte agrupa las dos porque comparten el mismo dolor central —el docente no puede ver el estado de la entrega de su comisión— y se distinguen sólo por el grado de agregación del dato. La parte correspondiente a F-15 se identifica en el sexto criterio de §5 y en CU-20, con plazo en la etapa `h`, para que la categoría 07 pueda diferirla sin tocar el resto de la NB.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de revisar la comisión desde un solo lugar a partir de las capacidades F-12 y F-15 del intake, con seis criterios de éxito trazados a su sección de origen, tres casos de uso previstos y la declaración de agregación de prioridades de §9. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgo **H-03**: las dos ocurrencias de «observación» de §1 y §2 pasan a **«advertencia»**, que es el término específico que el glosario raíz reserva a la discrepancia entre valor declarado y derivado (`Vision-Producto.md` §9.1). Ningún criterio, target ni trazabilidad cambia. La corrección se absorbe **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
