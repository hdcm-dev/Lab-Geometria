# US-08 — Transportar la proyección de listado sin la carga del detalle

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08-Transportar-La-Proyeccion-De-Listado-Sin-La-Carga-Del-Detalle.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **una proyección de listado que **no** lleve el texto original, ni los componentes de las piezas, ni el comentario del administrador**, para **que el listado del administrador no arrastre el texto completo de cada trabajo de la comisión**.

## 2. Contexto

[`ADR-05`](../../05-Arquitectura-Tecnica/Adrs/ADR-05-Proyeccion-De-Listado-Separada-Del-Detalle.md) declara que la proyección existe precisamente para **no** ser el detalle, y `05` §8 fija el NFR con umbral cero en las tres cosas. `05` §9 declara además que incorporar un campo del detalle «porque hace falta en una pantalla» es un riesgo de **probabilidad alta**: es la presión natural de la capa de presentación.

## 3. Criterios de aceptación

- Given un conjunto de trabajos, When se arma la proyección de listado, Then cada entrada trae identificador, nombre, fecha, dueño y estado.
- Given la inspección de la superficie pública de esta familia, When se cuentan las ocurrencias del texto original, de componentes de pieza y del comentario del administrador, Then son exactamente **cero** en las tres.
- Given una pantalla que necesita un dato que el listado no trae, When se resuelve, Then se pide el detalle y **no** se agrega el campo al listado.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-03, NB-07 |
| CU cubiertos | CU-04 |
| Familia de tipos de `05` §3.1 | Familia de listado |
| Restricciones transversales de `02` §6 | RT-04, RT-09 |
| RN que refiere por identificador | RN-03, RN-11 |
| BT derivadas | BT-13 |
| Etapa del producto | `e`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de inspección de la superficie pública de la familia de listado, en las tres dimensiones del NFR de `05` §8. |

## 5. Prioridad y estimación

`Must` por derivar de `F-08` y de `F-12`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

Este NFR viene rotulado **[ASUNCIÓN derivada]** en `05` §8 y su confirmación es parte de `PA-05` del backlog. El **carácter** de la restricción no está en duda —`ADR-05` la decide—; lo que está pendiente de confirmación es su expresión como puerta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
