# ADR-08005 — Proyección de listado separada del detalle, y el comentario como bloque propio

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

`PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Contracts declara el único requerimiento no funcional propio de este proyecto de código, y es estructural: **el payload de listado de trabajos no incluye ni el texto original ni los componentes de las piezas**, para que el listado del administrador no arrastre el texto completo de cada trabajo. El valor viene rotulado `[ASUNCIÓN]` derivada de la fuente técnica.

La categoría 02 lo amplió en su restricción transversal `RT-04`: la proyección tampoco lleva **el comentario del administrador**. Y separó CU-08004 de CU-08005 con un fundamento explícito: la proyección de listado existe precisamente para **no** ser el detalle, y con un único contrato de uso de lectura la restricción no tendría dónde verificarse ([`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §3.1).

Hay además una separación de forma que se decide en el mismo lugar: **el comentario del administrador viaja en el detalle como bloque propio y nunca como elemento de la colección de observaciones**, y no comparten ni un campo (`RT-09`). Son cosas distintas: la observación la emite el producto al interpretar el texto y hay tantas como defectos; el comentario lo escribe una persona y hay a lo sumo uno.

Motivación upstream: NB-00003, NB-00005, NB-00007, NB-00009; RN-08008, RN-08009, RN-08011.

## 2. Decisión

**El listado y el detalle son dos familias de tipos distintas, y la del listado es una proyección estricta**: no lleva el texto original, no lleva los componentes de las piezas y no lleva el comentario del administrador. Lo que lleva es lo que el listado necesita para agrupar, filtrar y mostrar estado.

**El comentario del administrador viaja en el detalle como bloque propio**, sin ningún campo compartido con la colección de observaciones.

**Hay un solo tipo de detalle para los dos papeles.** Lo que difiere entre el alumno y el administrador no es la forma del detalle sino el alcance de lo que cada uno puede pedir, y ese alcance es una regla de `GeometriaFactory-Domain`, no una forma del contrato.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Proyección estricta separada del detalle (**adoptada**) | El listado no crece con el tamaño del texto de cada trabajo; la restricción es verificable por inspección; el criterio de qué es listado y qué es detalle queda escrito | Dos familias de tipos que mantener; una pantalla que necesite un dato del detalle en el listado tiene que pedir el detalle |
| Un único tipo de lectura, con los campos pesados opcionales | Un solo tipo que mantener; la pantalla pide lo que necesita | La restricción de `PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Contracts dejaría de ser verificable por inspección: un campo opcional **puede** venir poblado, y el defecto no se vería en la superficie sino en cada llamada |
| Un tipo de listado por papel, uno para el alumno y otro para el administrador | Cada papel recibiría exactamente lo suyo | Es el mismo tipo con distinto alcance de datos, y dos tipos habrían duplicado la superficie sin declarar ninguna decisión de contrato. Es el criterio de fusión que la categoría 02 aplicó en CU-08004 |
| El comentario del administrador como una observación más | Una sola colección que recorrer en la presentación | Un comentario no es una observación: no tiene especie, no tiene posición de pieza y no tiene par de valores. Contarlo entre las observaciones haría que un rechazo comentado se leyera como un defecto más del trabajo |

## 5. Consecuencias positivas

1. El listado del administrador **no crece con el tamaño del texto de cada trabajo**, que es el escenario que el requerimiento del intake anticipa: una comisión entera con un trabajo por alumno.
2. La restricción es verificable **por inspección de la superficie pública**, sin ejecutar nada.
3. La separación entre observación y comentario queda garantizada por la forma de los tipos, y no por una convención de presentación.
4. Un solo tipo de detalle evita que la unidad pública tenga dos caminos de lectura que mantener sincronizados.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta un viaje adicional** cuando una pantalla necesita un dato del detalle sobre un elemento del listado.
2. **Se acepta la presión constante de la capa de presentación** para incorporar un campo del detalle al listado. Está registrada como riesgo en [`../Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, con probabilidad alta.
3. **Se acepta que el alcance de lo que cada papel ve no se pueda verificar acá.** El contrato transporta lo mismo para los dos y la acotación es del dominio; la verificación de que el administrador no ve los borradores pertenece a la batería de integración.

## 7. Implementación

- La familia de listado declara los campos necesarios para agrupar y filtrar por alumno y para mostrar el estado, y **ninguno** de los tres prohibidos.
- La familia de detalle declara las piezas con sus componentes, las observaciones con su especie y su par de valores, el texto original y el comentario del administrador **como bloque propio**.
- El estado del trabajo viaja en las dos familias, como conjunto cerrado de cuatro valores con dos terminales.
- Convención impuesta a la capa de presentación: **si un dato hace falta en el listado, se discute como cambio de contrato**, no se agrega al tipo por conveniencia.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Texto original en la proyección de listado | Exactamente **0** ocurrencias | CA-01 y CA-04 de CU-08004, por inspección de la superficie pública |
| Componentes de pieza en la proyección de listado | Exactamente **0** ocurrencias | Inspección de la superficie pública |
| Comentario del administrador en la proyección de listado | Exactamente **0** ocurrencias | Inspección de la superficie pública |
| Campos compartidos entre el comentario y la colección de observaciones | Exactamente **0** | CA-07, CA-08 y CA-09 de CU-08005 |
| Tipos de detalle | Exactamente **1**, común a los dos papeles | Inspección de la superficie pública |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.10 · GeometriaFactory-Contracts, §12 (entrada «comentario») y §4 (F-21).
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §3.1 y §6 (`RT-04`, `RT-09`).
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-08004-Contrato-De-Listado-De-Trabajos.md`](../Contratos-Inter-Unidad/CU-08004-Contrato-De-Listado-De-Trabajos.md) y [`CU-08005`](../Contratos-Inter-Unidad/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md).
- ADR relacionadas: [`ADR-08001`](ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md), [`ADR-08003`](ADR-08003-Versionado-Por-Compilacion-Compartida.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la proyección de listado como familia separada y estricta, el comentario del administrador como bloque propio sin campos compartidos con las observaciones, el tipo único de detalle para los dos papeles, cuatro alternativas evaluadas y cinco métricas verificables por inspección. |
