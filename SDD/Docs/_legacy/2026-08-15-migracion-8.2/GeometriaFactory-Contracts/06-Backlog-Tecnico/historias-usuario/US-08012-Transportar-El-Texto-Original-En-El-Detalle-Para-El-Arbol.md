# US-08012 — Transportar el texto original en el detalle, para el árbol y para la escena

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08012-Transportar-El-Texto-Original-En-El-Detalle-Para-El-Arbol.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08006 Visualización del trabajo
**Etapa del producto:** `g`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **que el detalle transporte también el texto original del trabajo, íntegro**, para **que el árbol colapsable y el bundle del visor tengan el texto que despliegan y que dibujan, sin pedirlo por otro camino**.

## 2. Contexto

La capacidad `F-11` del intake §4 declara la previsualización en tres dimensiones y el árbol colapsable del texto. `02` §4.2 declara que la previsión de producto de explorar la estructura como árbol se apoya en **el texto original del detalle**, y que la forma del árbol es presentación y no contrato.

## 3. Criterios de aceptación

- Given un trabajo interpretado, When se arma su detalle, Then transporta el texto original **íntegro** y como cadena, por `RN-08008` y `RT-03`.
- Given ese texto en el detalle, When se lo compara con el que el alumno envió, Then es idéntico: el contrato no lo normaliza en ninguno de los dos sentidos.
- Given la proyección de listado, When se busca el texto original, Then **no está**: viaja sólo en el detalle, por `RT-04`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00006 |
| CU cubiertos | CU-08005 |
| Familia de tipos de `05` §3.1 | Familia de detalle |
| Restricciones transversales de `02` §6 | RT-03, RT-04 |
| RN que refiere por identificador | RN-08008 |
| BT derivadas | BT-08014 |
| Etapa del producto | `g`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración que compara el texto del detalle con el enviado, sobre los escenarios del intake §20. |

## 5. Prioridad y estimación

`Must` por derivar de `F-11`, `Must Have` en `PRODUCT-INTAKE` §4. Vive en la etapa `g` porque es la etapa que integra la visualización y el árbol, aunque la familia de detalle se construya en la `f`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El bundle del visor recibe el texto por parámetro y no lo pide por su cuenta**: es un visualizador puro y no hace red (`RA-02`). Este campo es el que hace posible esa propiedad, y por eso pertenece al contrato y no a la pantalla.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
