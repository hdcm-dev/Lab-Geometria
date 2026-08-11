# US-20 — Transportar el desenlace al alumno: el estado en el listado y el comentario en el detalle

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-20-Transportar-El-Desenlace-Al-Alumno-Estado-Y-Comentario.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-07 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que el alumno reciba el desenlace de su trabajo en su listado y el comentario al abrir el trabajo**, para **que se entere del resultado sin que el listado tenga que arrastrar el texto libre de cada trabajo**.

## 2. Contexto

La capacidad `F-24` y el circuito de revisión declaran que el alumno ve el desenlace y el comentario. El roadmap 1.1 precisó las dos apariciones de ese enunciado para que **el desenlace se vea en el listado y el comentario al abrir el trabajo**, cerrando la lectura literal que exigiría texto libre dentro del listado, «que la capa de contratos prohíbe por diseño» —ése es el reparto que `RT-04` fija.

## 3. Criterios de aceptación

- Given un trabajo con desenlace, When el alumno consulta su listado, Then ve el **estado** del trabajo y no el comentario.
- Given ese mismo trabajo, When el alumno lo abre desde el listado, Then el detalle trae el **comentario** como bloque propio.
- Given la proyección de listado, When se busca el comentario, Then no está, por `RT-04`: el listado no arrastra el texto libre de cada trabajo.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09 |
| CU cubiertos | CU-04, CU-05 |
| Familia de tipos de `05` §3.1 | Familia de listado, Familia de detalle |
| Restricciones transversales de `02` §6 | RT-04, RT-09 |
| RN que refiere por identificador | RN-10, RN-11 |
| BT derivadas | BT-13, BT-14 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del recorrido completo del alumno: listado con estado, detalle con comentario. |

## 5. Prioridad y estimación

`Must` por derivar de `F-21`, `F-23` y `F-24`, las tres `Must Have` en `PRODUCT-INTAKE` §4, y porque es criterio de la transición `h` → `i…` del roadmap §5.2.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

Esta historia es donde se ve por qué [`ADR-05`](../../05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) importa más allá del peso: el reparto entre listado y detalle **no es una optimización**, es lo que hace que el enunciado del roadmap sea realizable tal como está escrito.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
