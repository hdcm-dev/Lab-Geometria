# US-18 — Transportar el comentario del administrador como bloque propio y nunca como observación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-18-Transportar-El-Comentario-Como-Bloque-Propio.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-07 Desenlace de la entrega
**Etapa del producto:** `h`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que el comentario del administrador viaje en el detalle como bloque propio, sin compartir ni un campo con las observaciones**, para **que nadie confunda un texto que escribió una persona con una observación que produjo la verificación**.

## 2. Contexto

La capacidad `F-21` del intake §4 declara el comentario escrito del administrador como opcional en los dos desenlaces, y explícitamente que **no es calificación**: es texto libre, sin nota ni escala. Es la restricción transversal `RT-09` de `02` §6 y `05` §6 la declara como una de las dos concreciones de forma que aguas abajo no se pueden invertir.

## 3. Criterios de aceptación

- Given un trabajo con comentario del administrador, When se arma su detalle, Then el comentario viaja como **bloque propio** y no como elemento de la colección de observaciones.
- Given ese bloque y una observación, When se comparan sus campos, Then **no comparten ninguno**.
- Given un desenlace sin comentario, When se arma el detalle, Then el bloque va vacío y el desenlace sigue siendo válido: el comentario es opcional en los dos casos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-09 |
| CU cubiertos | CU-07, CU-05 |
| Familia de tipos de `05` §3.1 | Familia de desenlace, Familia de detalle |
| Restricciones transversales de `02` §6 | RT-09 |
| RN que refiere por identificador | RN-10 |
| BT derivadas | BT-14, BT-15 |
| Etapa del producto | `h`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de inspección de la superficie del detalle, más prueba de integración de los dos desenlaces con y sin comentario. |

## 5. Prioridad y estimación

`Must` por derivar de `F-21`, `Must Have` en `PRODUCT-INTAKE` §4 desde que el Product Owner la pidió y se retiró su exclusión.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

`PRODUCT-INTAKE` §4.2 declara como consecuencia aceptada que **un alumno puede recibir un rechazo sin explicación escrita**: el estado le informa que no fue aceptado y el motivo queda a criterio del administrador en cada caso. El contrato no fuerza el comentario, y eso es intencional.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
