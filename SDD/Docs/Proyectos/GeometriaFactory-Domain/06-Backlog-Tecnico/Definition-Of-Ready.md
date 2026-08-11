# Definition of Ready — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Definition-Of-Ready.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5 (refinamiento); [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1 (criterios comunes a toda transición); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15 (reglas de delivery y punto de control bloqueante)
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Domain, que sólo compromete ítems que cumplen esta DoR

---

## Tabla de contenido

- [1. Criterios DoR para historias de usuario](#1-criterios-dor-para-historias-de-usuario)
- [2. Criterios DoR para tareas técnicas](#2-criterios-dor-para-tareas-técnicas)
- [3. Excepciones admitidas](#3-excepciones-admitidas)
- [4. Aprobador](#4-aprobador)
- [5. Qué no es esta DoR](#5-qué-no-es-esta-dor)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Criterios DoR para historias de usuario

Seis criterios, todos respondibles con sí o no. Una historia que no los cumpla no entra a la etapa.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 en su tabla de trazabilidad.
2. **Declara su necesidad de negocio y su etapa.** La historia nombra la `NB-XX` que sostiene y la etapa del producto en la que se ejerce, de las **ocho** comprometidas.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Cita por identificador toda regla e invariante que ejerce**, sin volver a enunciarla. El enunciado vive en `Reglas-De-Negocio/` y en `Definicion-Modelo-De-Dominio.md`, y una historia que lo reescriba abre una segunda fuente de verdad.
5. **Toda condición de rechazo que produce existe en el catálogo** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Una historia que necesite una condición nueva no está lista: primero se da de alta en 03.
6. **Sus tareas técnicas están identificadas y ninguna está bloqueada.** Si una `BT-XX` de la que depende cierra un punto abierto que sigue abierto, la historia no entra.

## 2. Criterios DoR para tareas técnicas

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, una ADR, un NFR de su §8, un punto abierto de su §11 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR o la puerta que la sostiene.
3. **Sus criterios de aceptación son verificables** por inspección, por prueba automatizada o por medición de una puerta declarada. «Queda bien hecho» no es un criterio.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular.**
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas**, no en horas: la unidad de planificación del producto es la etapa (`Roadmap-Producto.md` §1.2), y una caja temporal en días sería un plazo que ninguna fuente da.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el resultado esperado en lugar de con un criterio verificable de antemano: el objeto de la tarea es producir la decisión que hoy no existe | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya condición de rechazo todavía no está en el catálogo de 03 | El criterio 5 de §1 se difiere **una sola vez**, con el alta en 03 comprometida antes de cerrar la etapa | El Product Owner, con constancia escrita en el informe de cierre de la etapa |
| Historia que la etapa vigente sólo ejerce parcialmente | Ninguno: **no se admite**. Una historia que no cabe entera en su etapa está mal cortada y se redivide, por el mismo criterio con el que el intake §15 obliga a redividir una etapa mal cortada | — |

**Ninguna excepción alcanza al criterio 1 ni al criterio 4 de §1.** Una historia sin caso de uso o que reescriba una regla no entra bajo ninguna circunstancia: son los dos defectos que este corpus tiene documentados como los que más veces volvieron.

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar a la etapa, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona**, y eso hay que declararlo en lugar de simularlo: no hay una segunda persona que actúe de filtro. Lo que reemplaza a ese filtro es el **punto de control bloqueante** de cada etapa, que el intake §15 declara como regla de delivery: el orquestador se detiene, presenta el guion y espera OK explícito. La DoR dice cuándo se puede empezar; el punto de control es donde alguien distinto del código verifica que se empezó bien.

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que hace las veces de criterio de cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5, que son de nivel producto y no de proyecto de código. Esta DoR habla de **cuándo empezar** y no toca ninguna condición de cierre: no menciona cobertura, ni pruebas que pasen, ni documentación al día, que son de la DoD.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **seis** criterios de entrada para las historias y **cinco** para las tareas técnicas, todos respondibles con sí o no; tres casos de excepción, uno de ellos negativo; el aprobador con la constancia de que los dos papeles los ejerce la misma persona por `equipo_n = 1` y de que el filtro real es el punto de control bloqueante de cada etapa; y la delimitación contra la Definition of Done, que vive en 08 y todavía no está emitida. |
