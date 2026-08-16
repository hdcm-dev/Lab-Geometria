# US-08011 — Transportar el detalle del trabajo interpretado con sus piezas y componentes

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08011-Transportar-El-Detalle-Con-Sus-Piezas-Y-Componentes.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja el trabajo ya interpretado: sus piezas, sus componentes y sus observaciones**, para **que la pantalla y el bundle del visor tengan de dónde sacar lo que muestran, sin volver a interpretar el texto**.

## 2. Contexto

El contrato de uso es [`CU-08005`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08005-Contrato-De-Detalle-Del-Trabajo-Interpretado.md). Es la contracara de [`ADR-08005`](../../05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md): lo que el listado no lleva, lo lleva el detalle. `05` §3.1 lo declara como familia propia.

## 3. Criterios de aceptación

- Given un trabajo interpretado, When se arma su detalle, Then transporta la colección de piezas con su índice, sus componentes y sus observaciones.
- Given una pieza cuya posición en el texto del alumno tiene un hueco, When viaja en el detalle, Then conserva su índice y **el conjunto no se renumera**.
- Given el detalle de un trabajo, When lo pide el alumno y cuando lo pide el administrador, Then es **el mismo**: el detalle no cambia según el papel.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00005, NB-00006, NB-00007 |
| CU cubiertos | CU-08005 |
| Familia de tipos de `05` §3.1 | Familia de detalle |
| Restricciones transversales de `02` §6 | RT-04, RT-09 |
| RN que refiere por identificador | RN-08003, RN-08009, RN-08010 |
| BT derivadas | BT-08014 |
| Etapa del producto | `f`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del detalle con los escenarios `E-1` y `E-7` del intake §20 como material. |

## 5. Prioridad y estimación

`Must` por derivar de `F-09` y de `F-11`, las dos `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El índice de la pieza es su identidad** porque el texto no trae identificador (`PRODUCT-INTAKE` §17.1.P.11 punto 2), y es lo que después permite sincronizar el árbol y la escena. Que la disposición se derive de ese índice es de `GeometriaFactory-Visor`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
